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
	/// A collection class containing a list of <see cref="CurveItem"/> objects
	/// that define the set of curves to be displayed on the graph.
	/// </summary>
	/// 
	/// <author> John Champion
	/// modified by Jerry Vos</author>
	/// <version> $Revision: 3.30 $ $Date: 2005-12-26 11:09:10 $ </version>
	[Serializable]
	public class CurveList : CollectionPlus, ICloneable
	{
	#region Properties
		// internal temporary value that keeps
		// the max number of points for any curve
		// associated with this curveList
		private int	maxPts;

		/// <summary>
		/// Read only value for the maximum number of points in any of the curves
		/// in the list.
		/// </summary>
		public int MaxPts
		{
			get { return maxPts; }
		}

		/// <summary>
		/// Read only property that returns the number of curves in the list that are of
		/// type <see cref="Bar"/>.
		/// </summary>
		public int NumBars
		{
			get
			{
				int count = 0;
				foreach ( CurveItem curve in this )
				{
					if ( curve.IsBar )
						count++;
				}

				return count;
			}
		}

		/// <summary>
		/// Read only property that returns the number of pie slices in the list (class type is
		/// <see cref="PieItem"/> ).
		/// </summary>
		public int NumPies
		{
			get
			{
				int count = 0;
				foreach ( CurveItem curve in this )
				{
					if ( curve.IsPie )
						count++;
				}

				return count;
			}
		}

		/// <summary>
		/// Read only property that determines if all items in the <see cref="CurveList"/> are
		/// Pies.
		/// </summary>
		public bool IsPieOnly
		{
 			get
 			{
				bool hasPie = false;
 				foreach ( CurveItem curve in this )
 				{
 					if ( !curve.IsPie )
 						return false;
					else
						hasPie = true;
 				}
				return hasPie;
 			}
		}

		/// <summary>
		/// Determine if there is any data in any of the <see cref="CurveItem"/>
		/// objects for this graph.  This method does not verify valid data, it
		/// only checks to see if <see cref="CurveItem.NPts"/> > 0.
		/// </summary>
		/// <returns>true if there is any data, false otherwise</returns>
		public bool HasData()
		{
			foreach( CurveItem curve in this )
			{
				if ( curve.Points.Count > 0 )
					return true;
			}
			return false;
		}
	#endregion
	
	#region Constructors
	
		/// <summary>
		/// Default constructor for the collection class
		/// </summary>
		public CurveList()
		{
			maxPts = 1;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the CurveList</returns>
		public object Clone()
		{ 
			return new CurveList( this ); 
		}
		
		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The XAxis object from which to copy</param>
		public CurveList( CurveList rhs )
		{
			this.maxPts = rhs.maxPts;

			foreach ( CurveItem item in rhs )
			{
				this.Add( (CurveItem) item.Clone() );
			}
		}
		
	#endregion
	
	#region List Methods
		/// <summary>
		/// Indexer to access the specified <see cref="CurveItem"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="CurveItem"/> object to be accessed.</param>
		/// <value>A <see cref="CurveItem"/> object reference.</value>
		public CurveItem this[ int index ]  
		{
			get { return( (CurveItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Indexer to access the specified <see cref="CurveItem"/> object by
		/// its <see cref="CurveItem.Label"/> string.
		/// </summary>
		/// <param name="label">The string label of the
		/// <see cref="CurveItem"/> object to be accessed.</param>
		/// <value>A <see cref="CurveItem"/> object reference.</value>
		public CurveItem this[ string label ]  
		{
			get
			{
				int index = IndexOf( label );
				if ( index >= 0 )
					return( (CurveItem) List[index]  );
				else
					return null;
			}
		}

		/// <summary>
		/// Add a <see cref="CurveItem"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="curve">A reference to the <see cref="CurveItem"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public void Add( CurveItem curve )
		{
			List.Add( curve );
		}

		/// <summary>
		/// Remove a <see cref="CurveItem"/> object from the collection based on an object reference.
		/// </summary>
		/// <param name="curve">A reference to the <see cref="CurveItem"/> object that is to be
		/// removed.</param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( CurveItem curve )
		{
			List.Remove( curve );
		}

		/// <summary>
		/// Insert a <see cref="CurveItem"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="curve">A reference to the <see cref="CurveItem"/> object that is to be
		/// inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, CurveItem curve )
		{
			List.Insert( index, curve );
		}

		/// <summary>
		/// Return the zero-based position index of the
		/// <see cref="CurveItem"/> with the specified <see cref="CurveItem.Label"/>.
		/// </summary>
		/// <param name="label">The <see cref="String"/> label that is in the
		/// <see cref="CurveItem.Label"/> attribute of the item to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="CurveItem"/>,
		/// or -1 if the <see cref="CurveItem"/> is not in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		/// <seealso cref="IndexOfTag"/>
		public int IndexOf( string label )
		{
			int index = 0;
			foreach ( CurveItem p in this )
			{
				if ( String.Compare( p.Label, label, true ) == 0 )
					return index;
				index++;
			}

			return -1;
		}

		/// <summary>
		/// Return the zero-based position index of the
		/// <see cref="CurveItem"/> with the specified <see cref="CurveItem.Tag"/>.
		/// </summary>
		/// <remarks>In order for this method to work, the <see cref="CurveItem.Tag"/>
		/// property must be of type <see cref="String"/>.</remarks>
		/// <param name="label">The <see cref="String"/> label that is in the
		/// <see cref="CurveItem.Tag"/> attribute of the item to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="CurveItem"/>,
		/// or -1 if the <see cref="CurveItem"/> is not in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		/// <seealso cref="IndexOf"/>
		public int IndexOfTag( string label )
		{
			int index = 0;
			foreach ( CurveItem p in this )
			{
				if ( p.Tag is string &&
							String.Compare( (string) p.Tag, label, true ) == 0 )
					return index;
				index++;
			}

			return -1;
		}

		/// <summary>
		/// Sorts the list according to the point values at the specified index and
		/// for the specified axis.
		/// </summary>
		public void Sort( SortType type, int index )
		{
			InnerList.Sort( new CurveItem.Comparer( type, index ) );
		}
		
		/// <summary>
		/// Go through each <see cref="CurveItem"/> object in the collection,
		/// calling the <see cref="CurveItem.GetRange"/> member to 
		/// determine the minimum and maximum values in the
		/// <see cref="CurveItem.Points"/> list of data value pairs.  If the curves include 
		/// a stack bar, handle within the current GetRange method. In the event that no
		/// data are available, a default range of min=0.0 and max=1.0 are returned.
		/// If the Y axis has a valid data range and the Y2 axis not, then the Y2
		/// range will be a duplicate of the Y range.  Vice-versa for the Y2 axis
		/// having valid data when the Y axis does not.
		/// If any <see cref="CurveItem"/> in the list has a missing
		/// <see cref="PointPairList"/>, a new empty one will be generated.
		/// </summary>
		/// <param name="bIgnoreInitial">ignoreInitial is a boolean value that
		/// affects the data range that is considered for the automatic scale
		/// ranging (see <see cref="GraphPane.IsIgnoreInitial"/>).  If true, then initial
		/// data points where the Y value is zero are not included when
		/// automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.  All data after
		/// the first non-zero Y value are included.
		/// </param>
		/// <param name="isBoundedRanges">
		/// Determines if the auto-scaled axis ranges will subset the
		/// data points based on any manually set scale range values.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <seealso cref="GraphPane.IsBoundedRanges"/>
		public void GetRange( bool bIgnoreInitial, bool isBoundedRanges, GraphPane pane )
		{
			double	tXMinVal,
					tXMaxVal,
					tYMinVal,
					tYMaxVal;

			Scale scale = pane.XAxis.Scale;
			scale.rangeMin = double.MaxValue;
			scale.rangeMax = double.MinValue;
			scale.lBound = ( isBoundedRanges && pane.XAxis.MinAuto ) ?
					double.MinValue : pane.XAxis.Min;
			scale.uBound = ( isBoundedRanges && pane.XAxis.MaxAuto ) ?
					double.MaxValue : pane.XAxis.Max;

			
			foreach ( YAxis axis in pane.YAxisList )
			{
				scale = axis.Scale;
				scale.rangeMin = double.MaxValue;
				scale.rangeMax = double.MinValue;
				scale.lBound = ( isBoundedRanges && scale.MinAuto ) ? double.MinValue : scale.Min;
				scale.uBound = ( isBoundedRanges && scale.MaxAuto ) ? double.MaxValue : scale.Max;
			}

			foreach ( Y2Axis axis in pane.Y2AxisList )
			{
				scale = axis.Scale;
				scale.rangeMin = double.MaxValue;
				scale.rangeMax = double.MinValue;
				scale.lBound = ( isBoundedRanges && scale.MinAuto ) ? double.MinValue : scale.Min;
				scale.uBound = ( isBoundedRanges && scale.MaxAuto ) ? double.MaxValue : scale.Max;
			}

			// initialize the values to outrageous ones to start
			//xMinVal = yMinVal = y2MinVal = tXMinVal = tYMinVal = Double.MaxValue;
			//xMaxVal = yMaxVal = y2MaxVal = tXMaxVal = tYMaxVal = Double.MinValue;
			maxPts = 1;
			
			// The bounds provide a means to subset the data.  For example, if all the axes are set to
			// autoscale, then the full range of data are used.  But, if the XAxis.Min and XAxis.Max values
			// are manually set, then the Y data range will reflect the Y values within the bounds of
			// XAxis.Min and XAxis.Max.
			//double	xLBound = System.Double.MinValue;
			//double	xUBound = System.Double.MaxValue;
			//double	yLBound = System.Double.MinValue;
			//double	yUBound = System.Double.MaxValue;
			//double	y2LBound = System.Double.MinValue;
			//double	y2UBound = System.Double.MaxValue;


			// Loop over each curve in the collection and examine the data ranges
			foreach( CurveItem curve in this )
			{
				// For stacked types, use the GetStackRange() method which accounts for accumulated values
				// rather than simple curve values.
				if ( ( ( curve is BarItem ) && ( pane.BarType == BarType.Stack || pane.BarType == BarType.PercentStack ) ) ||
					( ( curve is LineItem ) && pane.LineType == LineType.Stack ) )
				{
					GetStackRange( pane, curve, out tXMinVal, out tYMinVal,
									out tXMaxVal, out tYMaxVal );
									//xLBound, xUBound,
									//curve.IsY2Axis ? y2LBound : yLBound,
									//curve.IsY2Axis ? y2UBound : yUBound );
				}
				else
				{
					// Call the GetRange() member function for the current
					// curve to get the min and max values
					curve.GetRange( out tXMinVal, out tXMaxVal,
									out tYMinVal, out tYMaxVal, bIgnoreInitial, true, pane );
									//xLBound, xUBound,
									//curve.IsY2Axis ? y2LBound : yLBound,
									//curve.IsY2Axis ? y2UBound : yUBound,
									//pane );
				}
   				
				// isYOrd is true if the Y axis is an ordinal type
				Scale yScale = curve.GetYAxis( pane ).Scale;
				Scale xScale = pane.XAxis.Scale;
				bool isYOrd = yScale.IsAnyOrdinal;
				// isXOrd is true if the X axis is an ordinal type
				bool isXOrd = xScale.IsAnyOrdinal;
   							
				// For ordinal Axes, the data range is just 1 to Npts
				if ( isYOrd )
				{
					tYMinVal = 1.0;
					tYMaxVal = curve.NPts;
				}
				if ( isXOrd )
				{
					tXMinVal = 1.0;
					tXMaxVal = curve.NPts;
				}

				// Bar types always include the Y=0 value
				if ( curve.IsBar )
				{
					if ( pane.BarBase == BarBase.X )
					{
						if ( pane.BarType != BarType.ClusterHiLow )
						{
							if ( tYMinVal > 0 )
								tYMinVal = 0;
							else if ( tYMaxVal < 0 )
								tYMaxVal = 0;
						}
   					
						// for non-ordinal axes, expand the data range slightly for bar charts to
						// account for the fact that the bar clusters have a width
						if ( !isXOrd )
						{
							tXMinVal -= pane.ClusterScaleWidth / 2.0;
							tXMaxVal += pane.ClusterScaleWidth / 2.0;
						}
					}
					else
					{
						if ( pane.BarType != BarType.ClusterHiLow )
						{
							if ( tXMinVal > 0 )
								tXMinVal = 0;
							else if ( tXMaxVal < 0 )
								tXMaxVal = 0;
						}
   						
						// for non-ordinal axes, expand the data range slightly for bar charts to
						// account for the fact that the bar clusters have a width
						if ( !isYOrd )
						{
							tYMinVal -= pane.ClusterScaleWidth / 2.0;
							tYMaxVal += pane.ClusterScaleWidth / 2.0;
						}
					}
				}

				// determine which curve has the maximum number of points
				if ( curve.NPts > maxPts )
					maxPts = curve.NPts;

				// If the min and/or max values from the current curve
				// are the absolute min and/or max, then save the values
				// Also, differentiate between Y and Y2 values
				
				if ( tYMinVal < yScale.rangeMin )
					yScale.rangeMin = tYMinVal;
				if ( tYMaxVal > yScale.rangeMax )
					yScale.rangeMax = tYMaxVal;
					
				
				if ( tXMinVal < xScale.rangeMin )
					xScale.rangeMin = tXMinVal;
				if ( tXMaxVal > xScale.rangeMax )
					xScale.rangeMax = tXMaxVal;
					
				/*	
				if ( curve.IsY2Axis )
				{
					if ( tYMinVal < y2MinVal )
						y2MinVal = tYMinVal;
					if ( tYMaxVal > y2MaxVal )
						y2MaxVal = tYMaxVal;
				}
				else
				{
					if ( tYMinVal < yMinVal )
						yMinVal = tYMinVal;
					if ( tYMaxVal > yMaxVal )
						yMaxVal = tYMaxVal;
				}
   			
				if ( tXMinVal < xMinVal )
					xMinVal = tXMinVal;
				if ( tXMaxVal > xMaxVal )
					xMaxVal = tXMaxVal;
				*/
			}
		
			pane.XAxis.Scale.SetRange( pane, pane.XAxis );
			foreach ( YAxis axis in pane.YAxisList )
				axis.Scale.SetRange( pane, axis );
			foreach ( Y2Axis axis in pane.Y2AxisList )
				axis.Scale.SetRange( pane, axis );

		}
		

		/// <summary>
		/// Calculate the range for stacked bars and lines.
		/// </summary>
		/// <remarks>This method is required for the stacked
		/// types because (for bars), the negative values are a separate stack than the positive
		/// values.  If you just sum up the bars, you will get the sum of the positive plus negative,
		/// which is less than the maximum positive value and greater than the maximum negative value.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="curve">The <see cref="CurveItem"/> for which to calculate the range</param>
		/// <param name="tXMinVal">The minimum X value so far</param>
		/// <param name="tYMinVal">The minimum Y value so far</param>
		/// <param name="tXMaxVal">The maximum X value so far</param>
		/// <param name="tYMaxVal">The maximum Y value so far</param>
		/// <seealso cref="GraphPane.IsBoundedRanges"/>
		private void GetStackRange( GraphPane pane, CurveItem curve, out double tXMinVal,
									out double tYMinVal, out double tXMaxVal, out double tYMaxVal )
		{
			// initialize the values to outrageous ones to start
			tXMinVal = tYMinVal = Double.MaxValue;
			tXMaxVal = tYMaxVal = Double.MinValue;

			ValueHandler valueHandler = new ValueHandler( pane, false );
			bool isXBase = curve.BaseAxis(pane) is XAxis;

			double lowVal, baseVal, hiVal;

			for ( int i=0; i<curve.Points.Count; i++ )
			{
				valueHandler.GetValues( curve, i, out baseVal, out lowVal, out hiVal );
				double x = isXBase ? baseVal : hiVal;
				double y = isXBase ? hiVal : baseVal;

				if ( x < tXMinVal )
					tXMinVal = x;
				if ( x > tXMaxVal )
					tXMaxVal = x;
				if ( y < tYMinVal )
					tYMinVal = y;
				if ( y > tYMaxVal )
					tYMaxVal = y;

				if ( !isXBase )
				{
					if ( lowVal < tXMinVal )
						tXMinVal = lowVal;
					if ( lowVal > tXMaxVal )
						tXMaxVal = lowVal;
				}
				else
				{
					if ( lowVal < tYMinVal )
						tYMinVal = lowVal;
					if ( lowVal > tYMaxVal )
						tYMaxVal = lowVal;
				}
			}
		}

		/// <summary>
		/// Render all the <see cref="CurveItem"/> objects in the list to the
		/// specified <see cref="Graphics"/>
		/// device by calling the <see cref="CurveItem.Draw"/> member function of
		/// each <see cref="CurveItem"/> object.
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
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, float scaleFactor )
		{
			// Configure the accumulator for stacked bars
			//Bar.ResetBarStack();

			// Count the number of BarItems in the curvelist
			int pos = this.NumBars;
			
			// sorted overlay bars are a special case, since they are sorted independently at each
			// ordinal position.
			if ( pane.BarType == BarType.SortedOverlay )
			{
				// First, create a new curveList with references (not clones) of the curves
				CurveList tempList = new CurveList();
				foreach ( CurveItem curve in this )
					if ( curve.IsBar )
						tempList.Add( (CurveItem) curve );
				
				// Loop through the bars, graphing each ordinal position separately
				for ( int i=0; i<this.maxPts; i++ )
				{
					// At each ordinal position, sort the curves according to the value axis value
					tempList.Sort( pane.BarBase == BarBase.X ? SortType.YValues : SortType.XValues, i );
					// plot the bars for the current ordinal position, in sorted order
					foreach ( BarItem barItem in tempList )
						barItem.Bar.DrawSingleBar( g, pane, barItem,
							((BarItem)barItem).BaseAxis( pane ),
							((BarItem)barItem).ValueAxis( pane ),
							0, i, scaleFactor );
				}
			}

			// Loop for each curve in reverse order to pick up the remaining curves
			// The reverse order is done so that curves that are later in the list are plotted behind
			// curves that are earlier in the list
			for ( int i=this.Count-1; i>=0; i-- )
			{
				CurveItem curve = this[i];
				
				if ( curve.IsBar )
					pos--;
					
				// Render the curve

				//	if it's a sorted overlay bar type, it's already been done above
				if	( !( curve.IsBar && pane.BarType == BarType.SortedOverlay ) ) 
					curve.Draw( g, pane, pos, scaleFactor );
			}
		}
	#endregion

	}
}


