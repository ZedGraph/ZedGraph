//============================================================================
//FilteredPointList class
//Copyright (C) 2006  John Champion
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
//=============================================================================using System;
using System;

namespace ZedGraph
{
	/// <summary>
	/// An example of an <see cref="IPointList" /> implementation that stores large datasets, and
	/// selectively filters the output data depending on the displayed range.
	/// </summary>
	/// <remarks>
	/// This class will refilter the data points each time <see cref="SetBounds" /> is called.  The
	/// data are filtered down to <see cref="MaxPts" /> points, within the data bounds of
	/// <see cref="XMinBound" /> and <see cref="XMaxBound" />.  The data are filtered by simply skipping
	/// points to achieve the desired total number of _points.  Input arrays are assumed to be
	/// monotonically increasing in X, and evenly spaced in X.
	/// </remarks>
	/// <seealso cref="PointPairList" />
	/// <seealso cref="BasicArrayPointList" />
	/// <seealso cref="IPointList" />
	/// <seealso cref="IPointListEdit" />
	///
	/// <author> John Champion</author>
	/// <version> $Revision: 1.3 $ $Date: 2006-03-27 01:31:37 $ </version>
	[Serializable]
	public class FilteredPointList : IPointList
	{

	#region Fields

		/// <summary>
		/// Instance of an array of x values
		/// </summary>
		private double[] x;
		/// <summary>
		/// Instance of an array of x values
		/// </summary>
		private double[] y;

		/// <summary>
		/// This is the minimum value of the range of interest (typically the minimum of
		/// the range that you have zoomed into)
		/// </summary>
		private double xMinBound = double.MinValue;
		/// <summary>
		/// This is the maximum value of the range of interest (typically the maximum of
		/// the range that you have zoomed into)
		/// </summary>
		private double xMaxBound = double.MaxValue;
		/// <summary>
		/// This is the maximum number of points that you want to see in the filtered dataset
		/// </summary>
		private int maxPts = -1;

		/// <summary>
		/// The index of the xMinBound above
		/// </summary>
		private int minBoundIndex = -1;
		/// <summary>
		/// The index of the xMaxBound above
		/// </summary>
		private int maxBoundIndex = -1;

	#endregion

	#region Properties

		/// <summary>
		/// Indexer to access the specified <see cref="PointPair"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <remarks>
		/// Returns <see cref="PointPair.Missing" /> for any value of <see paramref="index" />
		/// that is outside of its corresponding array bounds.
		/// </remarks>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="PointPair"/> object to be accessed.</param>
		/// <value>A <see cref="PointPair"/> object reference.</value>
		public PointPair this[ int index ]  
		{
			get
			{
				// See if the array should be bounded
				if ( this.minBoundIndex >= 0 && this.maxBoundIndex >= 0 && this.maxPts >= 0 )
				{
					// get number of points in bounded range
					int nPts = maxBoundIndex - minBoundIndex + 1;


					if ( nPts > maxPts )
					{
						// if we're skipping points, then calculate the new index
						index = minBoundIndex + (int) ( (double) index * (double) nPts / (double) maxPts );
					}
					else
					{
						// otherwise, index is just offset by the start of the bounded range
						index += minBoundIndex;
					}
				}

				double xVal, yVal;
				if ( index >= 0 && index < x.Length )
					xVal = x[index];
				else
					xVal = PointPair.Missing;

				if ( index >= 0 && index < y.Length )
					yVal = y[index];
				else
					yVal = PointPair.Missing;



				return new PointPair( xVal, yVal, PointPair.Missing, null );
			}
		}

		/// <summary>
		/// Returns the number of points according to the current state of the filter.
		/// </summary>
		public int Count
		{
			get
			{
				int arraySize = x.Length;

				// Is the filter active?
				if ( minBoundIndex >= 0 && maxBoundIndex >= 0 && maxPts > 0 )
				{
					// get the number of points within the filter bounds
					int boundSize = maxBoundIndex - minBoundIndex + 1;

					// limit the point count to the filter bounds
					if ( boundSize < arraySize )
						arraySize = boundSize;

					// limit the point count to the declared max points
					if ( arraySize > maxPts )
						arraySize = maxPts;
				}

				return arraySize;
			}
		}

		/// <summary>
		/// Gets the desired number of filtered points to output.  You can set this value by
		/// calling <see cref="SetBounds" />.
		/// </summary>
		public int MaxPts
		{
			get { return maxPts; }
		}

		/// <summary>
		/// Gets the minimum value for the range of X data that are included in the filtered result.
		/// You can set this value by calling <see cref="SetBounds" />.
		/// </summary>
		public double XMinBound
		{
			get { return xMinBound; }
		}

		/// <summary>
		/// Gets the maximum value for the range of X data that are included in the filtered result.
		/// You can set this value by calling <see cref="SetBounds" />.
		/// </summary>
		public double XMaxBound
		{
			get { return xMaxBound; }
		}

	#endregion

	#region Constructors

		/// <summary>
		/// Constructor to initialize the PointPairList from two arrays of
		/// type double.
		/// </summary>
		public FilteredPointList( double[] x, double[] y )
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The FilteredPointList from which to copy</param>
		public FilteredPointList( FilteredPointList rhs )
		{
			x = (double[]) rhs.x.Clone();
			y = (double[]) rhs.y.Clone();
			xMinBound = rhs.xMinBound;
			xMaxBound = rhs.xMaxBound;
			minBoundIndex = rhs.minBoundIndex;
			maxBoundIndex = rhs.maxBoundIndex;
			maxPts = rhs.maxPts;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the FilteredPointList</returns>
		virtual public object Clone()
		{ 
			return new FilteredPointList( this ); 
		}
		

	#endregion

	#region Methods

		/// <summary>
		/// Set the data bounds to the specified minimum, maximum, and point count.  Use values of
		/// min=double.MinValue and max=double.MaxValue to get the full range of data.  Use maxPts=-1
		/// to not limit the number of points.  Call this method anytime the zoom range is changed.
		/// </summary>
		/// <param name="min">The lower bound for the X data of interest</param>
		/// <param name="max">The upper bound for the X data of interest</param>
		/// <param name="maxPts">The maximum number of points allowed to be
		/// output by the filter</param>
		public void SetBounds( double min, double max, int maxPts )
		{
			this.maxPts = maxPts;
			
			// assume data points are equally spaced, and calculate the X step size between
			// each data point
			double step = ( this.x[ this.x.Length - 1 ] - this.x[0] ) / (double) this.x.Length;
			// calculate the index of the start of the bounded range
			int first = (int) ( ( min - this.x[0] ) / step );
			// calculate the index of the last point of the bounded range
			int last = (int) ( ( max - min ) / step + first );

			// Make sure the bounded indices are legitimate
			first = Math.Max( Math.Min( first, this.x.Length ), 0 );
			last = Math.Max( Math.Min( last, this.x.Length ), 0 );

			this.minBoundIndex = first;
			this.maxBoundIndex = last;
		}

	#endregion

	}
}
