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
using System.Drawing.Drawing2D;
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="ZedGraph.ArrowItem"/> type graphic objects
	/// to be displayed on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.2 $ $Date: 2004-10-15 05:11:30 $ </version>
	public class ArrowList : CollectionBase, ICloneable
	{
	#region Constructors
		/// <summary>
		/// Default constructor for the collection class
		/// </summary>
		public ArrowList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The ArrowList object from which to copy</param>
		public ArrowList( ArrowList rhs )
		{
			foreach ( ArrowItem item in rhs )
				this.Add( new ArrowItem( item ) );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the ArrowList</returns>
		public object Clone()
		{ 
			return new ArrowList( this ); 
		}
		
	#endregion

	#region Methods
		/// <summary>
		/// Indexer to access the specified <see cref="ZedGraph.ArrowItem"/> object by its ordinal
		/// position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="ZedGraph.ArrowItem"/> object to be accessed.</param>
		/// <value>An <see cref="ZedGraph.ArrowItem"/> object reference.</value>
		public ArrowItem this[ int index ]  
		{
			get { return( (ArrowItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add an <see cref="ArrowItem"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="item">A reference to the <see cref="ArrowItem"/> object to
		/// be added</param>
		/// <returns>The zero-based ordinal index where the <see cref="ArrowItem"/>
		/// was added in the list.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( ArrowItem item )
		{
			return List.Add( item );
		}

		/// <summary>
		/// Remove an <see cref="ArrowItem"/> object from the collection at the
		/// specified ordinal location.
		/// </summary>
		/// <param name="index">An ordinal position in the list at which
		/// the object to be removed is located. </param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Remove a <see cref="ArrowItem"/> object from the collection based on an object reference.
		/// </summary>
		/// <param name="arrow">A reference to the <see cref="ArrowItem"/> object that is to be
		/// removed.</param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( ArrowItem arrow )
		{
			List.Remove( arrow );
		}

		/// <summary>
		/// Insert a <see cref="ArrowItem"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="arrow">A reference to the <see cref="ArrowItem"/> object that is to be
		/// inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, ArrowItem arrow )
		{
			List.Insert( index, arrow );
		}

		/// <summary>
		/// Return the zero-based position index of the specified <see cref="ArrowItem"/> in the collection.
		/// </summary>
		/// <param name="arrow">A reference to the <see cref="ArrowItem"/> object that is to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="ArrowItem"/>, or -1 if the <see cref="ArrowItem"/>
		/// is not in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		public int IndexOf( ArrowItem arrow )
		{
			return List.IndexOf( arrow );
		}

		/// <summary>
		/// Determine if a mouse point is within any <see cref="ArrowItem"/>, and if so, 
		/// return the index number of the the <see cref="ArrowItem"/>.
		/// </summary>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="index">The index number of the <see cref="ArrowItem"/>
		///  that is under the mouse point.  The <see cref="ArrowItem"/> object is
		/// accessible via <see cref="GraphPane.ArrowList">ArrowList[index]</see>.
		/// </param>
		/// <returns>true if the mouse point is within a <see cref="ArrowItem"/> bounding
		/// box, false otherwise.</returns>
		/// <seealso cref="GraphPane.FindNearestObject"/>
		public bool FindPoint( PointF mousePt, GraphPane pane, out int index )
		{
			index = -1;
			
			for ( int i=0; i<Count; i++ )
			{
				ArrowItem arrow = this[i];
				if ( arrow.PointInBox( mousePt, pane ) )
				{
					index = i;
					return true;
				}
			}

			return false;
		}
		
	#endregion

	#region Rendering Method
		/// <summary>
		/// Render all objects to the specified <see cref="Graphics"/> device
		/// by calling the Draw method of each <see cref="ArrowItem"/> object in
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
			foreach ( ArrowItem item in this )
				item.Draw( g, pane, scaleFactor );
		}
	#endregion
	}
}

