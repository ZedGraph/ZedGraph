//============================================================================
//PointPairList Class
//Copyright (C) 2004  Jerry Vos
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
	/// A collection class containing a list of <see cref="PointPair"/> objects
	/// that define the set of points to be displayed on the curve.
	/// </summary>
	/// 
	/// <author> Jerry Vos based on code by John Champion </author>
	/// <version> $Revision: 1.1 $ $Date: 2004-08-23 20:24:17 $ </version>
	public class PointPairList : CollectionBase, ICloneable
	{
		private bool sorted = true;

		/// <summary>
		/// If the list is currently sorted.  <seealso cref="Sort"/>
		/// </summary>
		public bool Sorted
		{
			get { return sorted; }
		}


		/// <summary>
		/// Default constructor for the collection class
		/// </summary>
		public PointPairList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The PointPairList from which to copy</param>
		public PointPairList( PointPairList rhs )
		{
			foreach ( PointPair item in rhs )
				this.Add( new PointPair( item ) );

			sorted = false;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the CurveList</returns>
		public object Clone()
		{ 
			return new PointPairList( this ); 
		}
		
		/// <summary>
		/// Indexer to access the specified <see cref="PointPair"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="PointPair"/> object to be accessed.</param>
		/// <value>A <see cref="PointPair"/> object reference.</value>
		public PointPair this[ int index ]  
		{
			get { return( (PointPair) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="PointPair"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="point">A reference to the <see cref="PointPair"/> object to
		/// be added</param>
		public void Add( PointPair point )
		{
			List.Add( point );
			sorted = false;
		}

		/// <summary>
		/// Remove a <see cref="PointPair"/> object from the collection at the
		/// specified ordinal location.
		/// </summary>
		/// <param name="index">
		/// An ordinal position in the list at which the object to be removed 
		/// is located.
		/// </param>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Sorts the list according to the point x values. Will not sort the 
		/// list if the list is already sorted.
		/// </summary>
		/// <returns>If the list was sorted before sort was called</returns>
		public bool Sort()
		{
			// if it is already sorted we don't have to sort again
			if (sorted)
				return true;

			InnerList.Sort(new PointPair.PointPairComparer());
			return false;
		}
	}
}


