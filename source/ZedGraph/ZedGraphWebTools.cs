//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005  John Champion
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

using System;
using System.Web.UI;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace ZedGraph
{
	#region Generic Data Schema
	public struct GenericCollectionItemSchema
	{
		public GenericCollectionItemSchema(char c, Type t)
		{
			code = c;
			type = t;
		}
		public char code;
		public Type type;
	}
	#endregion

	#region Generic Data Item
	[	
		Bindable(true), 
		PersistenceMode(PersistenceMode.InnerProperty),
		TypeConverter(typeof(ExpandableObjectConverter))
	]
	public class GenericItem : IStateManager
	{		
		public GenericItem()
		{			
		}
						
		private bool _isTrackingViewState;
		private StateBag _viewState;		

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		protected StateBag ViewState
		{
			get 
			{
				if ( null == _viewState )
				{
					_viewState = new StateBag(false);
					if (_isTrackingViewState)
					{
						((IStateManager)_viewState).TrackViewState();
					}
				}
				return _viewState;
			}			
		}

		internal void SetDirty()
		{
			if ( null != _viewState )
			{
				foreach( string key in _viewState.Keys)
				{
					_viewState.SetItemDirty(key,true);
				}
			}
		}

		bool IStateManager.IsTrackingViewState
		{
			get { return _isTrackingViewState; }
		}

		protected virtual void LoadViewState(object savedState)
		{
			if ( savedState == null ) return;
			((IStateManager)ViewState).LoadViewState(savedState);
		}

		void IStateManager.LoadViewState(object savedState)
		{
			LoadViewState(savedState);
		}

		protected virtual object SaveViewState()
		{
			if ( null == _viewState ) return null;
			return ((IStateManager)_viewState).SaveViewState();
		}

		object IStateManager.SaveViewState()
		{
			return SaveViewState();
		}

		protected virtual void TrackViewState()
		{
			_isTrackingViewState = true;
			if ( null != _viewState ) ((IStateManager)_viewState).TrackViewState();
		}

		void IStateManager.TrackViewState()
		{
			TrackViewState();
		}
	}

	#endregion

	#region Generic Collection Editor
	public class GenericCollectionEditor : CollectionEditor 
	{

		public GenericCollectionEditor(Type type) : base(type) 
		{			
		}

		protected override Type[] CreateNewItemTypes() 
		{
			GenericCollection x = (GenericCollection)Activator.CreateInstance(CollectionType);
			
			Type[] types = new Type[x.Schema.Length];
			for (int i=0; i<x.Schema.Length; i++ )
			{
				types[i] = x.Schema[i].type;
			}
			
			return types;           
		}
	}
	#endregion

	/// <summary>
	/// Summary description for GenericCollection.
	/// </summary>
	[
		DefaultProperty("Item"),				
		ParseChildren(true, "Item"),				
		Editor(typeof(ZedGraph.GenericCollectionEditor), typeof(UITypeEditor))
	]
	public class GenericCollection : IStateManager, IList
	{
		protected GenericCollection()
		{			
			List = new ArrayList();			
			_saveAll = true;
			_isTrackingViewState = false;
		}		

		internal GenericCollectionItemSchema[] Schema = null;
		private ArrayList List = null;

		#region Helper Methods
		public void Clear()
		{
			List.Clear();
			if (_isTrackingViewState)
			{
				_saveAll = true;
			}
		}

		protected bool SupportedType( object item )
		{
			if (item == null) return false;		
	
			Type type = item.GetType();			
			foreach( GenericCollectionItemSchema gcis in Schema )
			{
				if ( type == gcis.type )
				{
					return true;
				}
			}
			return false;
		}

		protected void ListInsert(int index, object item) 
		{
			if (item == null) 
			{
				throw new ArgumentNullException("item");
			}

			if ( SupportedType(item) )
			{
				List.Insert(index,item);
				if (_isTrackingViewState) 
				{
					((IStateManager)item).TrackViewState();
					//((GenericItem)item).SetDirty();
					_saveAll = true;
				}
				return;
			}	
			
			throw new ArgumentException("item type not supported");			
		}

		protected object ListGet(int index)
		{
			return List[index];
		}

		protected int ListAdd(object item)
		{
			if (item == null) 
			{
				throw new ArgumentNullException("item");
			}

			if ( SupportedType(item) )
			{
				List.Add(item);					
				if (_isTrackingViewState) 
				{
					((IStateManager)item).TrackViewState();
					((GenericItem)item).SetDirty();						
					//_saveAll = true;
				}
				return List.Count - 1;
			}	
					
			throw new ArgumentException("item type not supported");		
		}

		protected void ListRemoveAt(int index)
		{
			List.RemoveAt(index);
			if (_isTrackingViewState) 
			{
				_saveAll = true;
			}
		}

		protected void ListRemove(object item) 
		{
			if (item == null) 
			{
				throw new ArgumentNullException("item");
			}

			if ( false == SupportedType(item) )
			{
				throw new ArgumentException("item type not supported");		
			}

			int index = IndexOf(item);
			if (index >= 0) 
			{
				ListRemoveAt(index);
			}
		}

		protected int IndexOf(object item) 
		{
			if (item == null) 
			{
				throw new ArgumentNullException("item");
			}
			return List.IndexOf(item);
		}
		#endregion
				
		#region IEnumerable Implementation
		public IEnumerator GetEnumerator() 
		{
			return List.GetEnumerator();
		}
        #endregion IEnumerable Implementation

        #region ICollection Implementation
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public int Count 
		{
			get 
			{
				return List.Count;
			}
		}

		public void CopyTo(Array array, int index) 
		{
			List.CopyTo(array,index);
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public bool IsSynchronized 
		{
			get 
			{
				return false;
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public object SyncRoot 
		{
			get 
			{
				return this;
			}
		}
        #endregion ICollection Implementation

        #region IList Implementation

		// Privately implement those members of IList that take or 
		// return the object type and expose equivalent public members
		// that take or return an item instance instead. Also
		// implement privately those members of IList that are not meaninful
		// to expose in the public object model of this Collection.
		object IList.this[int index] 
		{
			get 
			{
				return List[index];
			}
			set 
			{
				ListInsert(index,value);
			}
		}

		bool IList.IsFixedSize 
		{
			get 
			{
				return false;
			}
		}

		bool IList.IsReadOnly 
		{
			get 
			{
				return false;
			}
		}		

		int IList.Add(object item) 
		{
			return ListAdd(item);			
		}

		void IList.Clear() 
		{
			Clear();
		}

		bool IList.Contains(object item) 
		{
			if ( SupportedType(item) )
			{
				return List.Contains(item);
			}	
			return false;
		}

		int IList.IndexOf(object item) 
		{
			if (item == null) 
			{
				throw new ArgumentNullException("item");
			}
			if ( SupportedType(item) )
			{
				return List.IndexOf(item);			
			}	
			throw new ArgumentException("item type not supported");			
		}

		void IList.Insert(int index, object item) 
		{
			ListInsert(index,item);			
		}

		void IList.Remove(object item) 
		{						
			ListRemove(item);			
		}

		void IList.RemoveAt(int index) 
		{
			ListRemoveAt(index);
		}
        #endregion IList Implementation
		
		#region IStateManager Implementation

		private bool _isTrackingViewState;
		private bool _saveAll;

		bool IStateManager.IsTrackingViewState
		{
			get { return _isTrackingViewState; }
		}

		void IStateManager.TrackViewState()
		{
			_isTrackingViewState = true;
			foreach( object item in List )
			{
				((IStateManager)item).TrackViewState();
			}
		}

		void IStateManager.LoadViewState(object savedState)
		{
			GenericItem item;

			if ( null == savedState ) return;

			if ( savedState is Pair )
			{
				_saveAll = true;
				Pair p = (Pair)savedState;
				ArrayList types  = (ArrayList)p.First;
				ArrayList states = (ArrayList)p.Second;
				
				List.Clear();
				for (int i=0; i<types.Count; i++)
				{
					item = null;

					foreach ( GenericCollectionItemSchema gcis in Schema )
					{
						if ( ((char)types[i]).Equals(gcis.code) )
						{
							item = (GenericItem)Activator.CreateInstance(gcis.type);							
							break;
						}
					}
					
					if ( null != item )
					{
						List.Add(item);
						((IStateManager)item).LoadViewState(states[i]);
					}
				}
			}
			else
			{
				Triplet t = (Triplet)savedState;
				ArrayList indices = (ArrayList)t.First;
				ArrayList types   = (ArrayList)t.Second;
				ArrayList states  = (ArrayList)t.Third;

				for( int i=0; i<indices.Count; i++)
				{
					int index = (int)indices[i];
					if ( index < List.Count )
					{
						((IStateManager)List[index]).LoadViewState(states[i]);
					}
					else
					{
						item = null;

						foreach ( GenericCollectionItemSchema gcis in Schema )
						{
							if ( ((char)types[i]).Equals(gcis.code) )
							{
								item = (GenericItem)Activator.CreateInstance(gcis.type);							
								break;
							}
						}
						
						if ( null != item )
						{
							List.Add(item);
							((IStateManager)item).LoadViewState(states[i]);
						}	
					}
				}
			}
		}

		object IStateManager.SaveViewState()
		{
			GenericItem item;
			if ( _saveAll )
			{
				ArrayList types  = new ArrayList(List.Count);
				ArrayList states = new ArrayList(List.Count);
				for ( int i=0; i<List.Count; i++ )
				{
					item = (GenericItem)List[i];
					item.SetDirty();

					foreach ( GenericCollectionItemSchema gcis in Schema )
					{
						if ( gcis.type == item.GetType() )
						{
							types.Add( gcis.code );
							break;
						}
					}
					
					states.Add( ((IStateManager)item).SaveViewState() );					
				}

				if ( types.Count > 0 ) return new Pair(types,states);				
			}
			else
			{
				ArrayList indices = new ArrayList();
				ArrayList types   = new ArrayList();
				ArrayList states  = new ArrayList();
				object state;

				for ( int i=0; i<List.Count; i++ )
				{
					item = (GenericItem)List[i];
					state = ((IStateManager)item).SaveViewState();
					if ( state == null ) continue;

					foreach ( GenericCollectionItemSchema gcis in Schema )
					{
						if ( gcis.type == item.GetType() )
						{
							types.Add( gcis.code );
							break;
						}
					}
									
					states.Add(state);
					indices.Add(i);
				}

				if ( indices.Count > 0 ) return new Triplet(indices,types,states);
			}

			return null;
		}
		#endregion
	}
}
