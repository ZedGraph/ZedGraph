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
using ArrayList	= System.Collections.ArrayList;

namespace ZedGraph
{
	
	/// <summary>
	/// This class contains the data and methods for an individual curve within
	/// a graph pane.  It carries the settings for the curve including the
	/// key and item names, colors, symbols and sizes, linetypes, etc.
	/// </summary>
	/// 
	/// <author> John Champion modified by Jerry Vos </author>
	/// <version> $Revision: 1.6 $ $Date: 2004-08-23 20:24:17 $ </version>
	public class CurveItem : ICloneable
	{
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.Symbol"/>
		/// class defined for this <see cref="CurveItem"/>.  Use the public
		/// property <see cref="Symbol"/> to access this value.
		/// </summary>
		private Symbol		symbol;
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.Line"/>
		/// class defined for this <see cref="CurveItem"/>.  Use the public
		/// property <see cref="Line"/> to access this value.
		/// </summary>
		private Line		line;
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.Bar"/>
		/// class defined for this <see cref="CurveItem"/>.  Use the public
		/// property <see cref="Bar"/> to access this value.
		/// </summary>
		private Bar			bar;
		
		/// <summary>
		/// Private field that stores a legend label string for this
		/// <see cref="CurveItem"/>.  Use the public
		/// property <see cref="Label"/> to access this value.
		/// </summary>
		private string		label;
		/// <summary>
		/// Private field that stores the boolean value that determines whether this
		/// <see cref="CurveItem"/> is on the left Y axis or the right Y axis (Y2).
		/// Use the public property <see cref="IsY2Axis"/> to access this value.
		/// </summary>
		private bool		isY2Axis;
		/// <summary>
		/// Private field that stores the boolean value that determines whether this
		/// <see cref="CurveItem"/> is a bar chart or a line graph.
		/// Use the public property <see cref="IsBar"/> to access this value.
		/// </summary>
		private bool		isBar;
		
		/// <summary>
		/// The PointPairList of independent pairs that represent this
		/// <see cref="CurveItem"/>.
		/// The size of this array determines the number of points that are
		/// plotted.  Note that values defined as
		/// System.Double.MaxValue are considered "missing" values,
		/// and are not plotted.  The curve will have a break at these points
		/// to indicate values are missing.
		/// </summary>
		private PointPairList points;
		
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label and the
		/// x and y data arrays.  All other properties of the curve are
		/// defaulted to the values in the <see cref="Def"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="xs">A array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="ys">A array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		public CurveItem( string label, double[] xs, double[] ys )
		{
			this.line	= new Line();
			this.symbol	= new Symbol();
			this.bar	= new Bar();
			this.label	= label;
			this.isY2Axis = false;
			this.isBar	= false;

			this.points	= new PointPairList();

			this.AddPoints( xs, ys );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The CurveItem object from which to copy</param>
		public CurveItem( CurveItem rhs )
		{
			symbol	= new Symbol( rhs.Symbol );
			line	= new Line( rhs.Line );
			bar		= new Bar( rhs.Bar );
			label	= rhs.Label;
			isY2Axis = rhs.IsY2Axis;
			isBar	= rhs.IsBar;

			this.points	= (PointPairList) rhs.points.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the CurveItem</returns>
		public object Clone()
		{ 
			return new CurveItem( this ); 
		}

		/// <summary>
		/// Add a single x,y coordinate point to the end of the points collection for this curve.
		/// </summary>
		/// <param name="x">The X coordinate value</param>
		/// <param name="y">The Y coordinate value</param>
		public void AddPoint( double x, double y )
		{
			this.Points.Add( new PointPair( x, y ) );
		}

		/// <summary>
		/// Adds a set of points to Points.  If neither xs or ys are null and
		/// <c>xs.Length != ys.Length</c> the behavior is undefined.<br/>
		/// If xs is null then this will add PointPairs of the form (0, ys[i]);
		/// if ys is null then this will add PointPairs of the form (xs[i], 0);
		/// if both are null there is no effect.
		/// </summary>
		/// <param name="xs">The x coordinates of the new points.</param>
		/// <param name="ys">The y coordinates of the new points.</param>
		public void AddPoints(double[] xs, double[] ys)
		{
			if (xs != null && ys != null)
				for (int i = 0; i < xs.Length; i++)
				{
					points.Add(new PointPair(xs[i], ys[i]));
				}
			else if (xs == null && ys != null)
				for (int i = 0; i < ys.Length; i++)
				{
					points.Add(new PointPair(0, ys[i]));
				}
			else if (xs != null && ys == null)
				for (int i = 0; i < xs.Length; i++)
				{
					points.Add(new PointPair(xs[i], 0));
				}
		}

		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Symbol"/> class defined
		/// for this <see cref="CurveItem"/>.
		/// </summary>
		public Symbol Symbol
		{
			get { return symbol; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Line"/> class defined
		/// for this <see cref="CurveItem"/>.
		/// </summary>
		public Line Line
		{
			get { return line; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Bar"/> class defined
		/// for this <see cref="CurveItem"/>.
		/// </summary>
		public Bar Bar
		{
			get { return bar; }
		}
		/// <summary>
		/// A text string that represents the <see cref="ZedGraph.Legend"/>
		/// entry for the this
		/// <see cref="CurveItem"/> object
		/// </summary>
		public string Label
		{
			get { return label; }
			set { label = value;}
		}
		/// <summary>
		/// Determines which Y axis this <see cref="CurveItem"/>
		/// is assigned to.  The
		/// <see cref="ZedGraph.YAxis"/> is on the left side of the graph and the
		/// <see cref="ZedGraph.Y2Axis"/> is on the right side.  Assignment to an axis
		/// determines the scale that is used to draw the curve on the graph.
		/// </summary>
		/// <value>true to assign the curve to the <see cref="ZedGraph.Y2Axis"/>,
		/// false to assign the curve to the <see cref="ZedGraph.YAxis"/></value>
		public bool IsY2Axis
		{
			get { return isY2Axis; }
			set { isY2Axis = value; }
		}
		/// <summary>
		/// Determines whether this <see cref="CurveItem"/>
		/// is a bar chart or a line graph.
		/// </summary>
		/// <value>true for a bar chart, or false for a line graph</value>
		public bool IsBar
		{
			get { return isBar; }
			set { isBar = value; }
		}
		
		/// <summary>
		/// Readonly property that gives the number of points that define this
		/// <see cref="CurveItem"/> object, which is the number of points in 
		/// <see cref="Points"/>.
		/// </summary>
		public int NPts
		{
			get {	if ( this.points == null )
					  return 0;
					else
					  return this.points.Count;
				}
		}

		/// <summary>
		/// The <see cref="PointPairList"/> of points that represent this 
		/// <see cref="CurveItem"/>.
		/// </summary>
		public PointPairList Points
		{
			get { return points;	}
			set { points = value;	}
		}
		
		/// <summary>
		/// Do all rendering associated with this <see cref="CurveItem"/> to the specified
		/// <see cref="Graphics"/> device.  This method is normally only
		/// called by the Draw method of the parent <see cref="ZedGraph.CurveList"/>
		/// collection object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="pos">The ordinal position of the current <see cref="Bar"/>
		/// curve.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, int pos, double scaleFactor  )
		{
			if ( this.isBar )
			{
				DrawBars( g, pane, pane.CalcBarWidth(), pos, scaleFactor );
			}
			else
			{
				// If the line is being shown, draw it
				if ( this.Line.IsVisible )
					DrawCurve( g, pane );

				// If symbols are being shown, then draw them
				if ( this.Symbol.IsVisible )
					DrawSymbols( g, pane, scaleFactor );
			}
		}		
		
		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device.  The format (stair-step or line) of the curve is
		/// defined by the <see cref="StepType"/> property.  The routine
		/// only draws the line segments; the symbols are draw by the
		/// <see cref="DrawSymbols"/> method.  This method
		/// is normally only called by the Draw method of the
		/// <see cref="CurveItem"/> object
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		protected void DrawCurve( Graphics g, GraphPane pane )
		{
			float	tmpX, tmpY,
				lastX = 0,
				lastY = 0;
			bool	broke = true;
			
			// Loop over each point in the curve
			for ( int i=0; i<this.NPts; i++ )
			{
				PointPair currPair = (PointPair) points[i];

				Double currX	= (Double) currPair.X;
				double currXVal	= (double) currX;

				Double currY	= (Double) currPair.Y;
				double currYVal	= (double) currY;

				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				if ( 	currX == System.Double.MaxValue ||
					currY == System.Double.MaxValue ||
					( pane.XAxis.IsLog && currXVal <= 0.0 ) ||
					( this.isY2Axis && pane.Y2Axis.IsLog && currYVal <= 0.0 ) ||
					( !this.isY2Axis && pane.YAxis.IsLog && currYVal <= 0.0 ) )
				{
					broke = true;
				}
				else
				{
					// Transform the current point from user scale units to
					// screen coordinates
					tmpX = pane.XAxis.Transform( i, currXVal );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( i, currYVal );
					else
						tmpY = pane.YAxis.Transform( i, currYVal );
					
					// off-scale values "break" the line
					if ( tmpX < -100000 || tmpX > 100000 ||
						tmpY < -100000 || tmpY > 100000 )
						broke = true;
					else
					{
						// If the last two points are valid, draw a line segment
						if ( !broke )
						{
							if ( this.Line.StepType == StepType.ForwardStep )
							{
								this.Line.Draw( g, lastX, lastY, tmpX, lastY );
								this.Line.Draw( g, tmpX, lastY, tmpX, tmpY );
							}
							else if ( this.Line.StepType == StepType.RearwardStep )
							{
								this.Line.Draw( g, lastX, lastY, lastX, tmpY );
								this.Line.Draw( g, lastX, tmpY, tmpX, tmpY );
							}
							else 		// non-step
								this.Line.Draw( g, lastX, lastY, tmpX, tmpY );

						}

						// Save some values for the next point
						broke = false;
						lastX = tmpX;
						lastY = tmpY;
					}
				}
			}
		}

		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device as a symbol at each defined point.  The routine
		/// only draws the symbols; the lines are draw by the
		/// <see cref="DrawCurve"/> method.  This method
		/// is normally only called by the Draw method of the
		/// <see cref="CurveItem"/> object
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
		public void DrawSymbols( Graphics g, GraphPane pane, double scaleFactor )
		{
			float tmpX, tmpY;
			
			// Loop over each defined point							
			for ( int i=0; i<this.NPts; i++ )
			{
				PointPair currPair = (PointPair) points[i];

				Double currX	= (Double) currPair.X;
				double currXVal	= (double) currX;

				Double currY	= (Double) currPair.Y;
				double currYVal	= (double) currY;

				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				
				if (	currX != System.Double.MaxValue &&
					currY != System.Double.MaxValue &&
					( currXVal > 0 || !pane.XAxis.IsLog ) &&
					( currYVal > 0 ||
					(this.isY2Axis && !pane.Y2Axis.IsLog ) ||
					(!this.isY2Axis && !pane.YAxis.IsLog ) ) )
				{
					tmpX = pane.XAxis.Transform( i, currXVal );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( i, currXVal );
					else
						tmpY = pane.YAxis.Transform( i, currYVal );

					this.Symbol.Draw( g, tmpX, tmpY, scaleFactor );		
				}
			}
		}

		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device as a bar at each defined point.  This method
		/// is normally only called by the Draw method of the
		/// <see cref="CurveItem"/> object
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="barWidth">
		/// The width of each bar, in screen pixels.
		/// </param>
		/// <param name="pos">
		/// The ordinal position of the this bar series (0=first bar, 1=second bar, etc.)
		/// in the cluster of bars.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawBars( Graphics g, GraphPane pane, float barWidth,
							int pos, double scaleFactor )
		{
			float tmpX, tmpY;
			float clusterWidth	= pane.XAxis.GetClusterWidth( pane );
			float clusterGap	= pane.MinClusterGap * barWidth;
			float barGap		= barWidth * pane.MinBarGap;
//			float numBars = (float) pane.CurveList.NumBars;
			
			float basePix;
			Axis yAxis;
			if ( this.IsY2Axis )
				yAxis = pane.Y2Axis;
			else
				yAxis = pane.YAxis;

			if ( yAxis.Min < 0.0 && yAxis.Max > 0.0 )
				basePix = yAxis.Transform( 0.0 );
			else if ( yAxis.Min < 0.0 && yAxis.Max <= 0.0 )
				basePix = yAxis.MinPix;
			else
				basePix = yAxis.MaxPix;

			// Loop over each defined point							
			for ( int i=0; i<this.NPts; i++ )
			{
				PointPair currPair = (PointPair) points[i];

				Double currX	= (Double) currPair.X;
				double currXVal	= (double) currX;

				Double currY	= (Double) currPair.Y;
				double currYVal	= (double) currY;

				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				
				if ( currY != System.Double.MaxValue )
				{
					//tmpX = pane.XAxis.GetOrdinalPosition( pane, i );
//					if (currX != Double.NaN)
//						tmpX = pane.XAxis.Transform( i, i + 1.0 );
//					else
						tmpX = pane.XAxis.Transform( i, i + 1.0 );

					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( i, currYVal );
					else
						tmpY = pane.YAxis.Transform( i, currYVal );

					float left = tmpX - clusterWidth / 2.0F + clusterGap / 2.0F +
								pos * ( barWidth + barGap );

//					float left = tmpX - ( pos * ( barWidth + barGap ) - barGap )
//					float left = tmpX - ( clusterWidth -
//								( pos * barWidth * ( 1.0F + pane.MinBarGap )
//									- barWidth * pane.MinBarGap ) )/ 2.0F;

					Bar.Draw( g, left, left + barWidth, basePix,
						tmpY, scaleFactor, true );
				}
			}
		}

		/// <summary>
		/// Calculate the X screen pixel position of the center of the specified bar.  This method is
		/// used primarily by the <see cref="GraphPane.FindNearestPoint"/> method in order to
		/// determine the bar "location,"
		/// which is defined as the center of the top of the individual bar.
		/// </summary>
		/// <param name="pane">The active GraphPane object</param>
		/// <param name="barWidth">The width of each individual bar.  This can be calculated using
		/// the <see cref="GraphPane.CalcBarWidth"/> method.</param>
		/// <param name="iCluster">The cluster number for the bar of interest.  This is the ordinal
		/// position of the current point.  That is, if a particular <see cref="CurveItem"/> has
		/// 10 points, then a value of 3 would indicate the 4th point in the data array.</param>
		/// <param name="iOrdinal">The ordinal position of the <see cref="CurveItem"/> of interest.
		/// That is, the first bar series is 0, the second is 1, etc.  Note that this applies only
		/// to the bars.  If a graph includes both bars and lines, then count only the bars.</param>
		/// <returns>A screen pixel X position of the center of the bar of interest.</returns>
		public float CalcBarCenter( GraphPane pane, float barWidth, int iCluster, int iOrdinal )
		{
			float clusterWidth = pane.XAxis.GetClusterWidth( pane );
			//float barWidth = pane.CalcBarWidth();
			float clusterGap = pane.MinClusterGap * barWidth;
			float barGap = barWidth * pane.MinBarGap;

			return pane.XAxis.Transform( iCluster, iCluster + 1.0 )
						- clusterWidth / 2.0F + clusterGap / 2.0F +
						iOrdinal * ( barWidth + barGap ) + 0.5F * barWidth;
		}

		/// <summary>
		/// Go through <see cref="Points"/> for this <see cref="CurveItem"/>
		/// and determine the minimum and maximum values in the data.
		/// </summary>
		/// <param name="xMin">The minimum X value in the range of data</param>
		/// <param name="xMax">The maximum X value in the range of data</param>
		/// <param name="yMin">The minimum Y value in the range of data</param>
		/// <param name="yMax">The maximum Y value in the range of data</param>
		/// <param name="ignoreInitial">ignoreInitial is a boolean value that
		/// affects the data range that is considered for the automatic scale
		/// ranging (see <see cref="GraphPane.IsIgnoreInitial"/>).  If true, then initial
		/// data points where the Y value is zero are not included when
		/// automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.  All data after
		/// the first non-zero Y value are included.
		/// </param>
		public void GetRange( ref double xMin, ref double xMax,
							ref double yMin, ref double yMax,
							bool ignoreInitial )
		{
			// initialize the values to outrageous ones to start
			xMin = yMin = 1.0e20;
			xMax = yMax = -1.0e20;
			
			// Loop over each point in the arrays
			for ( int i=0; i<this.NPts; i++ )
			{
				PointPair currPair = (PointPair) points[i];

				Double currX	= (Double) currPair.X;
				double currXVal	= (double) currX;

				Double currY	= (Double) currPair.Y;
				double currYVal	= (double) currY;

				// ignoreInitial becomes false at the first non-zero
				// Y value
				if ( ignoreInitial && currYVal != 0 &&
							currY != System.Double.MaxValue )
					ignoreInitial = false;
				
				if ( !ignoreInitial && currX != System.Double.MaxValue &&
					currY != System.Double.MaxValue )
				{
					if ( currXVal < xMin )
						xMin = currXVal;
					if ( currXVal > xMax )
						xMax = currXVal;
					if ( currYVal < yMin )
						yMin = currYVal;
					if ( currYVal > yMax )
						yMax = currYVal;
				}
			}	

			if (isBar)
			{
				xMin = 0;
				xMax = this.NPts + 1;
				yMin = 0;
			}
		}

		/// <summary>
		/// See if the <see cref="Points"/> data array is missing
		/// for this <see cref="CurveItem"/>.  If so, provide a suitable default.
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		public void DataCheck( GraphPane pane )
		{
			// See if a default points array is required
			if ( this.points == null )
			{
				this.points = new PointPairList();
			}
		}
	}
}



