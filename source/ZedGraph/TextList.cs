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
	/// <version> $Revision: 2.0 $ $Date: 2004-09-02 06:24:59 $ </version>
	public class TextList : CollectionBase, ICloneable
	{
	#region Constructors
		/// <summary>
		/// Default constructor for the <see cref="TextList"/> collection class
		/// </summary>
		public TextList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The TextList object from which to copy</param>
		public TextList( TextList rhs )
		{
			foreach ( TextItem item in rhs )
				this.Add( new TextItem( item ) );
		}
	#endregion

	#region Methods
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the TextList</returns>
		public object Clone()
		{ 
			return new TextList( this ); 
		}
		
		/// <summary>
		/// Indexer to access the specified <see cref="TextItem"/> object by its ordinal
		/// position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="TextItem"/> object to be accessed.</param>
		/// <value>A <see cref="TextItem"/> object reference.</value>
		public TextItem this[ int index ]  
		{
			get { return( (TextItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="TextItem"/> object to the <see cref="TextList"/>
		/// collection at the end of the list.
		/// </summary>
		/// <param name="item">A reference to the <see cref="TextItem"/> object to
		/// be added</param>
		public void Add( TextItem item )
		{
			List.Add( item );
		}

		/// <summary>
		/// Remove a <see cref="TextItem"/> object from the <see cref="TextList"/>
		/// collection at the specified ordinal location.
		/// </summary>
		/// <param name="index">An ordinal position in the list at which
		/// the object to be removed is located. </param>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Render text to the specified <see cref="Graphics"/> device
		/// by calling the Draw method of each <see cref="TextItem"/> object in
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
			foreach ( TextItem item in this )
				item.Draw( g, pane, scaleFactor );
		}
	#endregion
	}
}