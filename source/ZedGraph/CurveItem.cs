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
	/// This class contains the data and methods for an individual curve within
	/// a graph pane.  It carries the settings for the curve including the
	/// key and item names, colors, symbols and sizes, linetypes, etc.
	/// </summary>
	/// 
	/// <author> John Champion
	/// modified by Jerry Vos </author>
	/// <version> $Revision: 1.13 $ $Date: 2004-08-31 05:26:20 $ </version>
	public class CurveItem : ICloneable
	{
	
		#region Fields
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
		/// The <see cref="PointPairList"/> of independent value pairs that
		/// represent this <see cref="CurveItem"/>.
		/// The size of this list determines the number of points that are
		/// plotted.  Note that values defined as
		/// System.Double.MaxValue are considered "missing" values
		/// (see <see cref="PointPair.Missing"/>),
		/// and are not plotted.  The curve will have a break at these points
		/// to indicate the values are missing.
		/// </summary>
		private PointPairList points;
		#endregion
	
		#region Constructors
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label and the
		/// x and y data values as double arrays.  All other properties of the curve are
		/// defaulted to the values in the <see cref="GraphPane.Default"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		public CurveItem( string label, double[] x, double[] y )
			: this( label, x, y, false, Color.Empty, SymbolType.Empty )
		{
		}
								
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label and the
		/// x and y data values as a <see cref="PointPairList"/>.  All other properties of the curve are
		/// defaulted to the values in the <see cref="GraphPane.Default"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		public CurveItem( string label, PointPairList points )
			: this( label, points, false, Color.Empty, SymbolType.Empty )
		{
		}
								
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label, the
		/// x and y data values as a <see cref="PointPairList"/>, the curve
		/// type (Bar or Line/Symbol), the <see cref="Color"/>, and the
		/// <see cref="SymbolType"/>. Other properties of the curve are
		/// defaulted to the values in the <see cref="GraphPane.Default"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		/// <param name="isBar">A boolean value, true to indicate that this curve
		/// is a bar type, false to indicate a line/symbol type.
		/// </param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/>, <see cref="Symbol"/>, and <see cref="Bar"/>
		/// see cref="Bar.FillColor"/>.
		/// </param>
		/// <param name="symbol">
		/// A <see cref="SymbolType"/> enum value indicating the symbol shape that
		/// will be used for this <see cref="CurveItem"/>.
		/// </param>
		public CurveItem( string label, double[] x, double[] y, bool isBar,
							Color color, SymbolType symbol )
		{
			Init( label, isBar, color, symbol );
			
			this.points = new PointPairList( x, y );
		}
		
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label, the
		/// x and y data values as a <see cref="PointPairList"/>, the curve
		/// type (Bar or Line/Symbol), the <see cref="Color"/>, and the
		/// <see cref="SymbolType"/>. Other properties of the curve are
		/// defaulted to the values in the <see cref="GraphPane.Default"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="isBar">A boolean value, true to indicate that this curve
		/// is a bar type, false to indicate a line/symbol type.
		/// </param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/>, <see cref="Symbol"/>, and <see cref="Bar"/>
		/// see cref="Bar.FillColor"/>.
		/// </param>
		/// <param name="symbol">
		/// A <see cref="SymbolType"/> enum value indicating the symbol shape that
		/// will be used for this <see cref="CurveItem"/>.
		/// </param>
		public CurveItem( string label, PointPairList points, bool isBar, Color color, SymbolType symbol )
		{
			Init( label, isBar, color, symbol );
			
			if ( points == null )
				this.points = new PointPairList();
			else
				this.points = (PointPairList) points.Clone();
		}
		
		/// <summary>
		/// Initialization of various <see cref="CurveItem"/> properties.  This common routine
		/// just avoid duplicate code in the various constructors.  The initialization of
		/// <see cref="Points"/> is handled within the constructor to avoid problems with
		/// multiple generation of the PointPairList.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="isBar">A boolean value, true to indicate that this curve
		/// is a bar type, false to indicate a line/symbol type.
		/// </param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/>, <see cref="Symbol"/>, and <see cref="Bar"/>
		/// see cref="Bar.FillColor"/>.
		/// </param>
		/// <param name="symbol">
		/// A <see cref="SymbolType"/> enum value indicating the symbol shape that
		/// will be used for this <see cref="CurveItem"/>.
		/// </param>
		protected void Init( string label, bool isBar, Color color, SymbolType symbol )
		{
			this.line = new Line( color );
			this.symbol = new Symbol( symbol, color );
			this.bar = new Bar( color );
			this.label = label == null ? "" : label;
			this.isY2Axis = false;
			this.isBar = isBar;
		}
			
		/// <summary>
		/// <see cref="CurveItem"/> constructor that specifies the label of the CurveItem.
		/// This is the same as <c>CurveItem(label, null, null)</c>.
		/// <seealso cref="CurveItem( string, double[], double[] )"/>
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		public CurveItem( string label ): this( label, null, null)
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The CurveItem object from which to copy</param>
		public CurveItem( CurveItem rhs )
		{
			symbol = new Symbol( rhs.Symbol );
			line = new Line( rhs.Line );
			bar = new Bar( rhs.Bar );
			label = rhs.Label;
			isY2Axis = rhs.IsY2Axis;
			isBar = rhs.IsBar;
			
			this.points = (PointPairList) rhs.Points.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the CurveItem</returns>
		public object Clone()
		{ 
			return new CurveItem( this ); 
		}

		#endregion
	
		#region Properties
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
		/// The <see cref="Line"/>/<see cref="Symbol"/>/<see cref="Bar"/> 
		/// color (FillColor for the Bar).  This is a common access to
		/// <see cref="ZedGraph.Line.Color"/>, <see cref="ZedGraph.Symbol.Color"/>, and
		/// <see cref="ZedGraph.Bar.FillColor"/> properties for this curve.
		/// </summary>
		public Color Color
		{
			get
			{
				if ( this.IsBar )
					return Bar.FillColor;
				else if ( this.Line.IsVisible )
					return this.Line.Color;
				else
					return this.Symbol.Color;
			}
			set 
			{ 
				Line.Color		= value;
				Bar.FillColor	= value;
				Symbol.Color	= value;
			}
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
		/// <see cref="CurveItem"/> object, which is the number of points in the
		/// <see cref="Points"/> data collection.
		/// </summary>
		public int NPts
		{
			get 
			{
				if ( this.points == null )
						return 0;
					else
						return this.points.Count;
			}
		}
		
		/// <summary>
		/// The <see cref="PointPairList"/> of X,Y point pairs that represent this
		/// <see cref="CurveItem"/>.
		/// </summary>
		public PointPairList Points
		{
			get { return points; }
			set { points = value; }
		}

		/// <summary>
		/// An accessor for the <see cref="PointPair"/> datum for this <see cref="CurveItem"/>.
		/// Index is the ordinal reference (zero based) of the point.
		/// </summary>
		public PointPair this[int index]
		{
			get
			{
				if ( this.points == null )
					return new PointPair( PointPair.Missing, PointPair.Missing );
				else
					return this.points[index];
			}
		}
	#endregion
	
	#region Rendering Methods
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
		/// only draws the line segments; the symbols are drawn by the
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
			double	curX, curY;
			bool	broke = true;
			
			// Loop over each point in the curve
			for ( int i=0; i<this.NPts; i++ )
			{
				curX = this.points[i].X;
				curY = this.points[i].Y;
				
				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				if ( 	curX == PointPair.Missing ||
						curY == PointPair.Missing ||
						System.Double.IsNaN( curX ) ||
						System.Double.IsNaN( curY ) ||
						System.Double.IsInfinity( curX ) ||
						System.Double.IsInfinity( curY ) ||
					( pane.XAxis.IsLog && curX <= 0.0 ) ||
					( this.isY2Axis && pane.Y2Axis.IsLog && curY <= 0.0 ) ||
					( !this.isY2Axis && pane.YAxis.IsLog && curY <= 0.0 ) )
				{
					broke = true;
				}
				else
				{
					// Transform the current point from user scale units to
					// screen coordinates
					tmpX = pane.XAxis.Transform( curX );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( curY );
					else
						tmpY = pane.YAxis.Transform( curY );
					
					// off-scale values "break" the line
					if ( tmpX < -100000 || tmpX > 100000 ||
						tmpY < -100000 || tmpY > 100000 )
						broke = true;
					else
					{
						// If the last two points are valid, draw a line segment
						if ( !broke || ( pane.IsIgnoreMissing && lastX != 0 ) )
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
			float	tmpX, tmpY;
			double	curX, curY;
			
			// Loop over each defined point							
			for ( int i=0; i<this.NPts; i++ )
			{
				curX = this.points[i].X;
				curY = this.points[i].Y;
				
				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				
				if (	curX != PointPair.Missing &&
						curY != PointPair.Missing &&
						!System.Double.IsNaN( curX ) &&
						!System.Double.IsNaN( curY ) &&
						!System.Double.IsInfinity( curX ) &&
						!System.Double.IsInfinity( curY ) &&
					( curX > 0 || !pane.XAxis.IsLog ) &&
					( this.isY2Axis || !pane.YAxis.IsLog || curY > 0.0 ) &&
					( !this.isY2Axis || !pane.Y2Axis.IsLog || curY > 0.0 ) )
				{
					tmpX = pane.XAxis.Transform( curX );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( curY );
					else
						tmpY = pane.YAxis.Transform( curY );

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
			float 	tmpX, tmpY;
			float 	clusterWidth = pane.XAxis.GetClusterWidth( pane );
			float 	clusterGap = pane.MinClusterGap * barWidth;
			float 	barGap = barWidth * pane.MinBarGap;
			
			float	basePix;
		
			double	curX, curY;
			
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
				curX = this.points[i].X;
				curY = this.points[i].Y;
				
				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				
				if (	curY != PointPair.Missing &&
						!System.Double.IsNaN( curY ) &&
						!System.Double.IsInfinity( curY ) )
				{
					//tmpX = pane.XAxis.GetOrdinalPosition( pane, i );
					tmpX = pane.XAxis.Transform( i, curX );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( i, curY );
					else
						tmpY = pane.YAxis.Transform( i, curY );

					float left = tmpX - clusterWidth / 2.0F + clusterGap / 2.0F +
								pos * ( barWidth + barGap );

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

			return pane.XAxis.Transform( iCluster, (double) iCluster + 1.0 )
						- clusterWidth / 2.0F + clusterGap / 2.0F +
						iOrdinal * ( barWidth + barGap ) + 0.5F * barWidth;
		}
	#endregion

	#region Utility Methods

		/// <summary>
		/// Add a single x,y coordinate point to the end of the points collection for this curve.
		/// </summary>
		/// <param name="x">The X coordinate value</param>
		/// <param name="y">The Y coordinate value</param>
		public void AddPoint( double x, double y )
		{
			this.AddPoint( new PointPair( x, y ) );
		}

		/// <summary>
		/// Add a <see cref="PointPair"/> object to the end of the points collection for this curve.
		/// </summary>
		/// <param name="point">A reference to the <see cref="PointPair"/> object to
		/// be added</param>
		public void AddPoint( PointPair point )
		{
			if ( this.points == null )
				this.Points = new PointPairList();
			this.points.Add( point );
		}

		/// <summary>
		/// Clears the points from this <see cref="CurveItem"/>.  This is the same
		/// as <c>CurveItem.Points.Clear()</c>.
		/// </summary>
		public void Clear()
		{
			points.Clear();
		}

		/// <summary>
		/// Loads some pseudo unique colors/symbols into this CurveItem.  This
		/// is the same as <c>MakeUnique(ColorSymbolRotator.StaticInstance)</c>.
		/// <seealso cref="ColorSymbolRotator.StaticInstance"/>
		/// <seealso cref="ColorSymbolRotator"/>
		/// <seealso cref="MakeUnique(ColorSymbolRotator)"/>
		/// </summary>
		public void MakeUnique()
		{
			this.MakeUnique( ColorSymbolRotator.StaticInstance );
		}

		/// <summary>
		/// Loads some pseudo unique colors/symbols into this CurveItem.  This
		/// is mainly useful for differentiating a set of new CurveItems without
		/// having to pick your own colors/symbols.
		/// <seealso cref="MakeUnique(ColorSymbolRotator)"/>
		/// </summary>
		/// <param name="rotator">
		/// The <see cref="ColorSymbolRotator"/> that is used to pick the color
		///  and symbol for this method call.
		/// </param>
		public void MakeUnique( ColorSymbolRotator rotator )
		{
			this.Color			= rotator.NextColor;
			this.Symbol.Type	= rotator.NextSymbol;
		}
	
	#endregion
	}
}



