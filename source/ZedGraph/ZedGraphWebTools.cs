//============================================================================
//ZedGraphWebTools Class
//Copyright (C) 2005  Darren Martz
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
using System.Reflection;

namespace ZedGraph
{	
	#region Generic View State Assistant

	/// <summary>
	/// Assists in managing viewstate sub-objects. The host of the assistant is
	/// responsible for managing an array within the statebag. This assistant
	/// helps deal with that by simply registering each hosted object with a
	/// unique code value. It also simplies the on demand creation of the sub object
	/// by creating the instance only when the getobject method is called.
	/// </summary>
	/// <author>Darren Martz</author>
	public class GenericViewStateAssistant
	{
		/// <summary>
		/// Array of child class instances managed in the viewstate
		/// </summary>
		private ArrayList list = new ArrayList();

		/// <summary>
		/// Internal child class instance node, identified by a code,
		/// its datatype, and the class instance.
		/// </summary>
		protected class AssistNode
		{
			/// <summary>
			/// Creates an assistant node given a character key and
			/// the hosted class type.
			/// </summary>
			/// <param name="code">Identifier code</param>
			/// <param name="type">Class data type hosted by the node</param>
			public AssistNode(char code, Type type)
			{
				Code = code;
				Key = type;
				Value = null;
			}

			/// <summary>
			/// Code to uniquely identify this node entry
			/// </summary>
			public char	  Code;

			/// <summary>
			/// Object type identifying hosted value type.
			/// </summary>
			public Type   Key;

			/// <summary>
			/// Object instance based on Key definition. May be null
			/// </summary>
			public object Value;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public GenericViewStateAssistant()
		{
		}

		/// <summary>
		/// Registers a code with a datatype in the assistance. Once registered
		/// the datatype will be available for retrievable using the same code value.
		/// </summary>
		/// <param name="code">Type identifier</param>
		/// <param name="type">Object type being hosted</param>
		public void Register(char code, Type type)
		{
			list.Add( new AssistNode(code,type) );
		}

		/// <summary>
		/// Retrieves instance based on the registered code and datatype.
		/// If the value is null, the datatype is used to create a new instance
		/// that is cached in the assistant node.
		/// </summary>
		/// <param name="code">code to search on</param>
		/// <param name="IsTrackingViewState">Indicates if the parent is currently
		/// tracking viewstate</param>
		/// <returns>class instance</returns>
		public object GetValue(char code, bool IsTrackingViewState)
		{
			AssistNode test = null;
			AssistNode node = null;
			
			for ( int i=0; i<list.Count; i++ )
			{
				test = (AssistNode)list[i];
				if ( test.Code == code )
				{
					node = test;
					break;
				}
			}

			if ( node.Value == null )
			{
				node.Value = Activator.CreateInstance(node.Key);					
				if (IsTrackingViewState) 
				{	
					((IStateManager)node.Value).TrackViewState();						
				}	
			}
			return node.Value;
		}

		/// <summary>
		/// Retrieves the object instance for an assistant node given an
		/// index value.
		/// </summary>
		/// <param name="index">Index value in the assistants node collection</param>
		/// <param name="IsTrackingViewState">Indicates if the parent is currently
		/// tracking viewstate changes</param>
		/// <returns>Object instance based on the node found in the collection</returns>
		protected object GetValue(int index, bool IsTrackingViewState)
		{			
			AssistNode node = (AssistNode)list[index];
			
			if ( node.Value == null )
			{
				node.Value = Activator.CreateInstance(node.Key);					
				if (IsTrackingViewState) 
				{	
					((IStateManager)node.Value).TrackViewState();						
				}	
			}
			return node.Value;
		}
		
		/// <summary>
		/// Returns the current viewstate of every object hosted in
		/// the assistants collection.
		/// </summary>
		/// <param name="BaseState">parents viewstate bag</param>
		/// <returns>Combined saved viewstate for the parent and all hosted
		/// objects in the assistant collection</returns>
		public object SaveViewState(object BaseState)
		{			
			object[] myState = new object[list.Count+1];			
			myState[0] = BaseState;
			AssistNode node;

			for (int i=0; i<list.Count; i++)
			{
				node = (AssistNode)list[i];
				if ( node.Value != null )
				{					
					myState[i+1] = ((IStateManager)node.Value).SaveViewState();	
				}
			}

			return myState;
		}

		/// <summary>
		/// Loads the viewstate from the provided statebag into each of the
		/// registered objects hosted in the assistants collection.
		/// </summary>
		/// <param name="savedState">statebag provided by parent</param>
		/// <param name="IsTrackingViewState">indicates if the parent is currently
		/// tracking viewstate changes</param>
		/// <returns>The parents individual statebag</returns>
		public object LoadViewState(object savedState, bool IsTrackingViewState)
		{
			if ( savedState == null ) return null;
			object[] myState = (object[])savedState;
			            
#if DEBUG
			if ( myState.Length != list.Count+1 )
			{
				System.Diagnostics.Debugger.Break();
			}
#endif
			
			IStateManager state;				
			for ( int i=0; i<list.Count; i++ )
			{	
				if ( myState[i+1] != null )
				{
					state = (IStateManager)GetValue(i,IsTrackingViewState);					
					state.LoadViewState(myState[i+1]);
				}
			}

			return myState[0];
		}

		/// <summary>
		/// Triggers the assistant to begin tracking viewstate changes
		/// </summary>
		public void TrackViewState()
		{			
			AssistNode node;
			for ( int i=0; i<list.Count; i++ )
			{	
				node = (AssistNode)list[i];
				if ( node.Value != null )
				{
					((IStateManager)node.Value).TrackViewState();				
				}
			}				
		}
	}
	#endregion

	#region Generic Data Schema
	/// <summary>
	/// Identifies state management items by a unique code and its datatype. The code is defined 
	/// in the implementation of a GenericCollection class constructor.
	/// </summary>
	/// <author>Darren Martz</author>
	/// <version> $ </version>
	public struct GenericCollectionItemSchema
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="c">Single character code used to identify the state item datatype</param>
		/// <param name="t">Item datatype</param>
		public GenericCollectionItemSchema(char c, Type t)
		{
			code = c;
			type = t;
		}
		
		/// <summary>
		/// The state item datatype character code
		/// </summary>
		public char code;
		/// <summary>
		/// The state item datatype
		/// </summary>
		public Type type;
	}
	#endregion

	#region Generic Data Item
	/// <summary>
	/// Generic state management item used in a state management aware collection.
	/// This class is intended to be subclasses with public property values added.
	/// Property values should be added in the full {get/set} format reading and writing
	/// the current state from the ViewState array.
	/// </summary>
	/// <example>
	/// public class MyItem : GenericItem
	/// {
	///		[NotifyParentProperty(true)]
	///		public bool IsVisible
	///		{
	///			get 
	///			{ 
	///				object x = ViewState["IsVisible"]; 
	///				return (null == x) ? true : (bool)x;
	///			}
	///			set { ViewState["IsLegendLabelVisible"] = value; }
	///		}
	///	}
	/// </example>
	/// <author>Darren Martz</author>
	[	
		Bindable(true), 
		PersistenceMode(PersistenceMode.InnerProperty),
		TypeConverter(typeof(ExpandableObjectConverter))
	]
	public class GenericItem : IStateManager
	{				
		/// <summary>
		/// Default constructor that does nothing
		/// </summary>
		public GenericItem()
		{			
			_isTrackingViewState = false;
			_viewState = null;
			_subitemlist = new ArrayList();
		}
		
		/// <summary>
		/// Internal indicator of the current tracking state
		/// </summary>
		private bool _isTrackingViewState = false;

		/// <summary>
		/// Internal view state used by the asp.net infrastructure
		/// </summary>
		private StateBag _viewState = null;		

		private ArrayList _subitemlist = null;
		private class AssistNode
		{
			public AssistNode(char code, Type type)
			{
				Code = code;
				Key = type;
				Value = null;
			}
			public char	  Code;
			public Type   Key;
			public object Value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <param name="type"></param>
		protected void Register(char code, Type type)
		{
#if DEBUG
			for (int i=0; i<_subitemlist.Count; i++)
			{
				if ( ((AssistNode)_subitemlist[i]).Code == code )
				{
					throw new Exception(string.Format(
						"duplicate register code '{0}' found on {1} for type {2}",
						code,this.GetType().Name, type.Name));
				}
			}
#endif			
			_subitemlist.Add( new AssistNode(code,type) );			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		protected object GetValue(char code)
		{
			AssistNode test = null;
			AssistNode node = null;

			for ( int i=0; i<_subitemlist.Count; i++ )
			{
				test = (AssistNode)_subitemlist[i];
				if ( test.Code == code )
				{
					node = test;
					break;
				}
			}

			if ( node.Value == null )
			{
				node.Value = Activator.CreateInstance(node.Key);					
				if (((IStateManager)this).IsTrackingViewState) 
				{	
					((IStateManager)node.Value).TrackViewState();						
				}	
			}			
			return node.Value;
		}

		private object GetValue(int index)
		{					
			AssistNode node = (AssistNode)_subitemlist[index];
			
			if ( node.Value == null )
			{
				node.Value = Activator.CreateInstance(node.Key);					
				if (((IStateManager)this).IsTrackingViewState) 
				{	
					((IStateManager)node.Value).TrackViewState();						
				}	
			}
			return node.Value;
		}

		/// <summary>
		/// Internal access to the viewstate array. Subclassed objects can access this
		/// to read/write changes to the objects view state.
		/// </summary>
		/// <example>
		/// ViewState["myelement"] = "value";
		/// string val = (string)ViewState["myelement"];
		/// </example>
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

		/// <summary>
		/// Internal method to mark the statebag as dirty so it can be resaved if necessary
		/// </summary>
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

		/// <summary>
		/// Implementation of the IStateManager.IsTrackingViewState property.
		/// </summary>
		bool IStateManager.IsTrackingViewState
		{
			get { return _isTrackingViewState; }
		}

		/// <summary>
		/// Loads the viewstate into the local statebag given a viewstate collection object
		/// </summary>
		/// <param name="savedState">object containing asp.net page viewstate</param>
		protected virtual void LoadViewState(object savedState)
		{
			if ( savedState == null ) return;			
			object[] myState = (object[])savedState;
			            
#if DEBUG
			if ( myState.Length != _subitemlist.Count+1 )
			{
				System.Diagnostics.Debugger.Break();
			}
#endif
			((IStateManager)ViewState).LoadViewState(myState[0]);

			IStateManager state;				
			for ( int i=0; i<_subitemlist.Count; i++ )
			{	
				if ( myState[i+1] != null )
				{
					state = (IStateManager)GetValue(i);					
					state.LoadViewState(myState[i+1]);
				}
			}
		}

		/// <summary>
		/// Implementation of the IStateManager.LoadViewState method
		/// </summary>
		/// <param name="savedState">object containing asp.net page viewstate</param>
		void IStateManager.LoadViewState(object savedState)
		{
			LoadViewState(savedState);
		}

		/// <summary>
		/// Saves the current viewstate into a portable object
		/// </summary>
		/// <returns>object containing classes viewstate</returns>
		protected virtual object SaveViewState()
		{
			if ( null == _viewState ) return null;

			object[] myState = new object[_subitemlist.Count+1];			
			myState[0] = ((IStateManager)_viewState).SaveViewState();
			AssistNode node;

			for (int i=0; i<_subitemlist.Count; i++)
			{
				node = (AssistNode)_subitemlist[i];
				if ( node.Value != null )
				{					
					myState[i+1] = ((IStateManager)node.Value).SaveViewState();	
				}
			}

			return myState;
		}

		/// <summary>
		/// Implementation of the IStateManager.SaveViewState method
		/// </summary>
		/// <returns>object containing classes viewstate</returns>
		object IStateManager.SaveViewState()
		{
			return SaveViewState();
		}

		/// <summary>
		/// Tells the statebag to begin tracking changes to state values
		/// </summary>
		protected virtual void TrackViewState()
		{
			_isTrackingViewState = true;
			if ( null != _viewState ) ((IStateManager)_viewState).TrackViewState();

			AssistNode node;
			for ( int i=0; i<_subitemlist.Count; i++ )
			{	
				node = (AssistNode)_subitemlist[i];
				if ( node.Value != null )
				{
					((IStateManager)node.Value).TrackViewState();				
				}
			}	
		}

		/// <summary>
		/// Implementation of the IStateManager.TrackViewState method
		/// </summary>
		void IStateManager.TrackViewState()
		{
			TrackViewState();
		}
	}

	#endregion

	#region Generic Collection Editor
	/// <summary>
	/// Provides a few hints to the development editor on how to create
	/// child items in the collection. This class uses the schema defined
	/// inside the collection implementation.
	/// </summary>
	/// <example>
	/// public class MyCollection : GenericCollection
	/// {
	///		public MyCollection() : base()
	///		{
	///			Schema = new GenericCollectionItemSchema[1];
	///			Schema[0].code = 'b';
	///			Schema[1].type = typeof(MyItem);
	///		}
	///		public void Add(MyItem item)
	///		{
	///			if ( null != item ) ListAdd( item );
	///			else throw new ArgumentException("parameter cannot be null","item");
	///		}	
	///		
	///		[NotifyParentProperty(true)]
	///		public ZedGraphWebCurveItem this [int index]
	///		{
	///			get { return (MyItem)ListGet(index); }
	///			set { ListInsert(index,value); }
	///		}
	///	}
	/// </example>
	/// <author>Darren Martz</author>
	public class GenericCollectionEditor : CollectionEditor 
	{
		/// <summary>
		/// Default constructor based on CollectionEditor contructor
		/// </summary>
		/// <param name="type">Datetype of the collection to manage</param>
		public GenericCollectionEditor(Type type) : base(type) 
		{			
		}

		/// <summary>
		/// Informs the visual editor what kinds of classes are accepted as elements
		/// within the collection.
		/// </summary>
		/// <returns>Array of datatypes supported as items within the collection</returns>
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

	#region Generic Collection
	/// <summary>
	/// Provides array services in a web state engine environment. This collection can
	/// support 1:N datatypes as items provided they are based on the GenericItem class.
	/// </summary>
	/// <author>Darren Martz</author>
	[
		DefaultProperty("Item"),				
		ParseChildren(true, "Item"),				
		Editor(typeof(ZedGraph.GenericCollectionEditor), typeof(UITypeEditor))
	]
	public class GenericCollection : IStateManager, IList
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		protected GenericCollection()
		{			
			List = new ArrayList();			
			_saveAll = true;
			_isTrackingViewState = false;
		}		

		#region Properties
		/// <summary>
		/// Internal collection item schema table. All supported data types must be
		/// identified in this array. The default constructor should populate this
		/// value.
		/// </summary>
		internal GenericCollectionItemSchema[] Schema = null;

		/// <summary>
		/// Internal array of items. Some public access to the array is provided by default.
		/// Additional public access should be added when subclassing.
		/// </summary>
		private ArrayList List = null;

		#endregion

		#region Helper Methods
		/// <summary>
		/// Empty the array list and marks the viewstate.
		/// </summary>
		public void Clear()
		{
			List.Clear();
			if (_isTrackingViewState)
			{
				_saveAll = true;
			}
		}

		/// <summary>
		/// Verifies if the object is a supported datatype
		/// </summary>
		/// <param name="item">object to verify</param>
		/// <returns>true if supported</returns>
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

		/// <summary>
		/// Internal insert item method that also tracks the change in the viewstate
		/// </summary>
		/// <param name="index">location in the array to insert the object</param>
		/// <param name="item">object to insert</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
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

		/// <summary>
		/// Retrieves an item by index
		/// </summary>
		/// <param name="index">array index</param>
		/// <returns>array item</returns>
		protected object ListGet(int index)
		{
			return List[index];
		}

		/// <summary>
		/// Appends item to array and tracks the change in the viewstate
		/// </summary>
		/// <param name="item">Item to append</param>
		/// <returns>new index of item</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
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
					//saveAll = true;
				}
				return List.Count - 1;
			}	
					
			throw new ArgumentException("item type not supported");		
		}

		/// <summary>
		/// Removes the item from the array and tracks the change in the viewstate
		/// </summary>
		/// <param name="index">item index to remove</param>
		protected void ListRemoveAt(int index)
		{
			List.RemoveAt(index);
			if (_isTrackingViewState) 
			{
				_saveAll = true;
			}
		}

		/// <summary>
		/// Removes the item from the array and tracks the change in the viewstate
		/// </summary>
		/// <param name="item">object to remove</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
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

		/// <summary>
		/// Identifies the items index in the list given the item object
		/// </summary>
		/// <param name="item">item to locate in list</param>
		/// <returns>index of item</returns>
		/// <exception cref="ArgumentNullException"></exception>		
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
		/// <summary>
		/// <see cref="System.Collections.IEnumerable.GetEnumerator"/> 
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator() 
		{
			return List.GetEnumerator();
		}
        #endregion IEnumerable Implementation

        #region ICollection Implementation
		/// <summary>
		/// <see cref="ICollection.Count"/>
		/// </summary>
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

		/// <summary>
		/// <see cref="ICollection.CopyTo"/>
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(Array array, int index) 
		{
			List.CopyTo(array,index);
		}

		/// <summary>
		/// <see cref="ICollection.IsSynchronized"/>
		/// </summary>
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

		/// <summary>
		/// <see cref="ICollection.SyncRoot"/>		
		/// </summary>
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

		/// <summary>
		/// Privately implement those members of IList that take or 
		/// return the object type and expose equivalent public members
		/// that take or return an item instance instead. Also
		/// implement privately those members of IList that are not meaninful
		/// to expose in the public object model of this Collection.		
		/// </summary>
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

		/// <summary>
		/// <see cref="IList.IsFixedSize"/>
		/// </summary>
		bool IList.IsFixedSize 
		{
			get 
			{
				return false;
			}
		}

		/// <summary>
		/// <see cref="IList.IsReadOnly"/>
		/// </summary>
		bool IList.IsReadOnly 
		{
			get 
			{
				return false;
			}
		}		

		/// <summary>
		/// Adds item based on the rules of ListAdd
		/// <see cref="IList.Add"/>
		/// <seealso cref="GenericCollection.ListAdd"/>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		int IList.Add(object item) 
		{
			return ListAdd(item);			
		}

		/// <summary>
		/// <see cref="IList.Clear"/>
		/// </summary>
		void IList.Clear() 
		{
			Clear();
		}

		/// <summary>
		/// <see cref="IList.Contains"/>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		bool IList.Contains(object item) 
		{
			if ( SupportedType(item) )
			{
				return List.Contains(item);
			}	
			return false;
		}

		/// <summary>
		/// <see cref="IList.IndexOf"/>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Inserts item according to the rules of ListInsert
		/// <see cref="IList.Insert"/>
		/// <seealso cref="GenericCollection.ListInsert"/>
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		void IList.Insert(int index, object item) 
		{
			ListInsert(index,item);			
		}

		/// <summary>
		/// Removes item based on ListRemove
		/// <see cref="IList.Remove"/>
		/// <seealso cref="GenericCollection.ListRemove"/>
		/// </summary>
		/// <param name="item"></param>
		void IList.Remove(object item) 
		{						
			ListRemove(item);			
		}

		/// <summary>
		/// Removes item based on RemoveAt
		/// <see cref="IList.RemoveAt"/>
		/// <seealso cref="CollectionBase.RemoveAt"/>
		/// </summary>
		/// <param name="index"></param>
		void IList.RemoveAt(int index) 
		{
			ListRemoveAt(index);
		}
        #endregion IList Implementation
		
		#region IStateManager Implementation

		/// <summary>
		/// preserves internal tracking state
		/// </summary>
		private bool _isTrackingViewState;

		/// <summary>
		/// marks the entire statebag as dirty requiring the entire state to be saved
		/// </summary>
		private bool _saveAll;

		/// <summary>
		/// <see cref="IStateManager.IsTrackingViewState"/>
		/// </summary>
		bool IStateManager.IsTrackingViewState
		{
			get { return _isTrackingViewState; }
		}

		/// <summary>
		/// <see cref="IStateManager.TrackViewState"/>
		/// </summary>
		void IStateManager.TrackViewState()
		{
			_isTrackingViewState = true;
			foreach( object item in List )
			{
				((IStateManager)item).TrackViewState();
			}
		}

		/// <summary>
		/// Loads the view state. This involves reading each state item pair, verifying
		/// schema support for each datatype, creating each item instance, and loading
		/// the state into each newly created instance.
		/// <seealso cref="IStateManager.LoadViewState"/>
		/// </summary>
		/// <param name="savedState">state management object</param>
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

		/// <summary>
		/// Saves the viewstate into a portable object format. This involves
		/// saving the state of each item in the list.
		/// <seealso cref="IStateManager.SaveViewState"/>
		/// </summary>
		/// <returns>portable state object</returns>
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
	#endregion
}
