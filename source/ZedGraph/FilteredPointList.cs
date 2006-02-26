using System;

namespace ZedGraph
{
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
