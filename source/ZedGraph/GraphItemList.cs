//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2004  John Champion
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
using System.Drawing;
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="TextItem"/> objects
	/// to be displayed on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2004-11-17 04:38:08 $ </version>
	public class GraphItemList : CollectionBase, ICloneable
	{
	#region Constructors
		/// <summary>
		/// Default constructor for the <see cref="GraphItemList"/> collection class
		/// </summary>
		public GraphItemList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="GraphItemList"/> object from which to copy</param>
		public GraphItemList( GraphItemList rhs )
		{
			foreach ( GraphItemList item in rhs )
				this.Add( (GraphItem) item.Clone() );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="GraphItemList"/></returns>
		public object Clone()
		{ 
			return new GraphItemList( this ); 
		}
		
	#endregion

	#region Methods
		/// <summary>
		/// Indexer to access the specified <see cref="GraphItem"/> object by its ordinal
		/// position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="GraphItem"/> object to be accessed.</param>
		/// <value>A <see cref="GraphItem"/> object reference.</value>
		public GraphItem this[ int index ]  
		{
			get { return( (GraphItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="GraphItem"/> object to the <see cref="GraphItemList"/>
		/// collection at the end of the list.
		/// </summary>
		/// <param name="item">A reference to the <see cref="GraphItem"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public void Add( GraphItem item )
		{
			List.Add( item );
		}

		/// <summary>
		/// Remove a <see cref="GraphItem"/> object from the <see cref="GraphItemList"/>
		/// collection at the specified ordinal location.
		/// </summary>
		/// <param name="index">An ordinal position in the list at which
		/// the object to be removed is located. </param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Remove a <see cref="GraphItem"/> object from the collection based on an object reference.
		/// </summary>
		/// <param name="item">A reference to the <see cref="GraphItem"/> object that is to be
		/// removed.</param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( GraphItem item )
		{
			List.Remove( item );
		}

		/// <summary>
		/// Insert a <see cref="GraphItem"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="item">A reference to the <see cref="GraphItem"/> object that is to be
		/// inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, GraphItem item )
		{
			List.Insert( index, item );
		}

		/// <summary>
		/// Return the zero-based position index of the specified <see cref="GraphItem"/> in the collection.
		/// </summary>
		/// <param name="item">A reference to the <see cref="GraphItem"/> object that is to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="GraphItem"/>, or -1 if the
		/// <see cref="GraphItem"/> is not in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		public int IndexOf( GraphItem item )
		{
			return List.IndexOf( item );
		}

		/// <summary>
		/// Render text to the specified <see cref="Graphics"/> device
		/// by calling the Draw method of each <see cref="GraphItem"/> object in
		/// the collection.  This method is normally only called by the Draw method
		/// of the parent <see cref="GraphPane"/> object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			foreach ( GraphItem item in this )
				item.Draw( g, pane, scaleFactor );
		}

		/// <summary>
		/// Determine if a mouse point is within any <see cref="GraphItem"/>, and if so, 
		/// return the index number of the the <see cref="GraphItem"/>.
		/// </summary>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="index">The index number of the <see cref="TextItem"/>
		///  that is under the mouse point.  The <see cref="TextItem"/> object is
		/// accessible via <see cref="GraphPane.GraphItemList">GraphItemList[index]</see>.
		/// </param>
		/// <returns>true if the mouse point is within a <see cref="GraphItem"/> bounding
		/// box, false otherwise.</returns>
		/// <seealso cref="GraphPane.FindNearestObject"/>
		public bool FindPoint( PointF mousePt, GraphPane pane, Graphics g, double scaleFactor, out int index )
		{
			index = -1;
			
			for ( int i=0; i<Count; i++ )
			{
				if ( this[i].PointInBox( mousePt, pane, g, scaleFactor ) )
				{
					index = i;
					return true;
				}
			}

			return false;
		}
		

	#endregion
	}
}