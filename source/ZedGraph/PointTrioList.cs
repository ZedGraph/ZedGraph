//============================================================================
//PointTrioList Class
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
	/// A collection class containing a list of <see cref="PointTrio objects
	/// that define the set of points to be displayed on the curve.
	/// </summary>
	/// 
	/// <author> John Champion based on code by Jerry Vos
	/// </author>
	/// <version> $Revision: 3.1 $ $Date: 2004-10-22 23:50:42 $ </version>
	public class PointTrioList : CollectionBase, ICloneable
	{
	#region Fields
		/// <summary>Private field to maintain the sort status of this
		/// <see cref="PointTrioList"/>.  Use the public property
		/// <see cref="Sorted"/> to access this value.
		/// </summary>
		private bool sorted = true;
	#endregion

	#region Properties
		/// <summary>
		/// true if the list is currently sorted.
		/// </summary>
		/// <seealso cref="Sort"/>
		public bool Sorted
		{
			get { return sorted; }
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Default constructor for the collection class
		/// </summary>
		public PointTrioList()
		{
		}

		/// <summary>
		/// Constructor to initialize the PointTrioList from three arrays of
		/// type double.
		/// </summary>
		public PointTrioList( double[] x, double[] y1, double[] y2 )
		{
			Add( x, y1, y2 );
			
			sorted = false;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The PointTrioList from which to copy</param>
		public PointTrioList( PointTrioList rhs )
		{
			Add( rhs );

			sorted = false;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the PointTrioList</returns>
		public object Clone()
		{ 
			return new PointTrioList( this ); 
		}
		
	#endregion

	#region Methods
		/// <summary>
		/// Indexer to access the specified <see cref="PointTrio"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="PointTrio"/> object to be accessed.</param>
		/// <value>A <see cref="PointTrio"/> object reference.</value>
		public PointTrio this[ int index ]  
		{
			get { return( (PointTrio) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="PointTrio"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="point">A reference to the <see cref="PointTrio"/> object to
		/// be added</param>
		/// <returns>The zero-based ordinal index where the point was added in the list.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( PointTrio point )
		{
			sorted = false;
			return List.Add( point );
		}

		/// <summary>
		/// Add a <see cref="PointTrioList"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="pointList">A reference to the <see cref="PointTrioList"/> object to
		/// be added</param>
		/// <returns>The zero-based ordinal index where the last point was added in the list,
		/// or -1 if no points were added.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( PointTrioList pointList )
		{
			int rv = -1;
			
			foreach ( PointTrio point in pointList )
				rv = this.Add( point );
				
			sorted = false;
			return rv;
		}

		/// <summary>
		/// Add a set of points to the PointTrioList from three arrays of type double.
		/// If any array is null, then a set of ordinal values is automatically
		/// generated in its place (see <see cref="AxisType.Ordinal"/>.
		/// If the arrays are of different size, then the larger array prevails and the
		/// smaller array is padded with <see cref="PointTrio.Missing"/> values.
		/// </summary>
		/// <param name="x">A double[] array of X values</param>
		/// <param name="y1">A double[] array of Y1 values</param>
		/// <param name="y2">A double[] array of Y2 values</param>
		/// <returns>The zero-based ordinal index where the last point was added in the list,
		/// or -1 if no points were added.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( double[] x, double[] y1, double[] y2 )
		{
			PointTrio	point;
			int 		len = 0,
						rv = -1;
			
			if ( x != null )
				len = x.Length;
			if ( y1 != null && y1.Length > len )
				len = y1.Length;
			if ( y2 != null && y2.Length > len )
				len = y2.Length;
						
			for ( int i=0; i<len; i++ )
			{
				if ( x == null )
					point.X = (double) i + 1.0;
				else if ( i < x.Length )
					point.X = x[i];
				else
					point.X = PointPair.Missing;
					
				if ( y1 == null )
					point.Y1 = (double) i + 1.0;
				else if ( i < y1.Length )
					point.Y1 = y1[i];
				else
					point.Y1 = PointPair.Missing;
					
				if ( y2 == null )
					point.Y2 = (double) i + 1.0;
				else if ( i < y2.Length )
					point.Y2 = y2[i];
				else
					point.Y2 = PointPair.Missing;
					
				rv = List.Add( point );
			}
			
			sorted = false;
			return rv;
		}

		/// <summary>
		/// Add a single point to the PointTrioList from values of type double.
		/// </summary>
		/// <param name="x">The X value</param>
		/// <param name="y1">The Y1 value</param>
		/// <param name="y2">The Y2 value</param>
		/// <returns>The zero-based ordinal index where the point was added in the list.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( double x, double y1, double y2 )
		{
			sorted = false;
			PointTrio	point = new PointTrio( x, y1, y2 );
			return List.Add( point );
		}

		/// <summary>
		/// Remove a <see cref="PointTrio"/> object from the collection at the
		/// specified ordinal location.
		/// </summary>
		/// <param name="index">
		/// An ordinal position in the list at which the object to be removed 
		/// is located.
		/// </param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Remove the specified <see cref="PointTrio"/> object from the collection based
		/// the point values (must match exactly).
		/// </summary>
		/// <param name="pt">
		/// A <see cref="PointTrio"/> that is to be removed by value.
		/// </param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( PointTrio pt )
		{
			List.Remove( pt );
		}

		/// <summary>
		/// Insert a <see cref="PointTrio"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="pt">The <see cref="PointTrio"/> object that is to be inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, PointTrio pt )
		{
			List.Insert( index, pt );
		}

		/// <summary>
		/// Return the zero-based position index of the specified <see cref="PointTrio"/> in the collection.
		/// </summary>
		/// <param name="pt">A reference to the <see cref="PointTrio"/> object that is to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="PointTrio"/>, or -1 if the <see cref="PointTrio"/>
		/// is not in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		public int IndexOf( PointTrio pt )
		{
			return List.IndexOf( pt );
		}

		/// <summary>
		/// Sorts the list according to the point x values. Will not sort the 
		/// list if the list is already sorted.
		/// </summary>
		/// <returns>If the list was sorted before sort was called</returns>
		public bool Sort()
		{
			// if it is already sorted we don't have to sort again
			if ( sorted )
				return true;

			InnerList.Sort( new PointTrio.PointTrioComparer() );
			return false;
		}
		
		/// <summary>
		/// Go through the list of <see cref="PointTrio"/> data values
		/// and determine the minimum and maximum values in the data.
		/// </summary>
		/// <param name="xMin">The minimum X value in the range of data</param>
		/// <param name="xMax">The maximum X value in the range of data</param>
		/// <param name="yMin">The minimum Y1 or Y2 value in the range of data</param>
		/// <param name="yMax">The maximum Y1 or Y2 value in the range of data</param>
		/// <param name="ignoreInitial">ignoreInitial is a boolean value that
		/// affects the data range that is considered for the automatic scale
		/// ranging (see <see cref="GraphPane.IsIgnoreInitial"/>).  If true, then initial
		/// data points where the Y1 or Y2 value is zero are not included when
		/// automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.  All data after
		/// the first non-zero Y1 or Y2 value are included.
		/// </param>
		public void GetRange(	ref double xMin, ref double xMax,
								ref double yMin, ref double yMax,
								bool ignoreInitial )
		{
			// initialize the values to outrageous ones to start
			xMin = yMin = Double.MaxValue;
			xMax = yMax = Double.MinValue;
			
			// Loop over each point in the arrays
			foreach ( PointTrio point in this )
			{
				double curX = point.X;
				double curY1 = point.Y1;
				double curY2 = point.Y2;
				
				// ignoreInitial becomes false at the first non-zero
				// Y value
				if (	ignoreInitial &&
						( ( curY1 != 0 && curY1 != PointPair.Missing ) ||
						  ( curY2 != 0 && curY2 != PointPair.Missing ) ) )
					ignoreInitial = false;
				
				if ( 	!ignoreInitial &&
						curX != PointPair.Missing &&
						curY1 != PointPair.Missing &&
						curY2 != PointPair.Missing )
				{
					if ( curX < xMin )
						xMin = curX;
					if ( curX > xMax )
						xMax = curX;
					if ( curY1 < yMin )
						yMin = curY1;
					if ( curY1 > yMax )
						yMax = curY1;
					if ( curY2 < yMin )
						yMin = curY2;
					if ( curY2 > yMax )
						yMax = curY2;
				}
			}	
		}
	#endregion
	}
}


