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

namespace ZedGraph
{
	/// <summary>
	/// A class representing all the characteristics of the bar
	/// segments that make up a curve on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.3 $ $Date: 2004-10-29 03:12:14 $ </version>
	public class Bar : ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Bar"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill	fill;
		/// <summary>
		/// Private field that stores the <see cref="Border"/> class that defines the
		/// properties of the border around this <see cref="BarItem"/>.  Use the public
		/// property <see cref="Border"/> to access this value.
		/// </summary>
		private Border	border;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Bar"/> class.
		/// </summary>
		public struct Default
		{
			// Default Bar properties
			/// <summary>
			/// The default pen width to be used for drawing the border around the bars
			/// (<see cref="ZedGraph.Border.PenWidth"/> property).  Units are points.
			/// </summary>
			public static float BorderWidth = 1.0F;
			/// <summary>
			/// The default fill mode for bars (<see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FillType = FillType.Brush;
			/// <summary>
			/// The default border mode for bars (<see cref="ZedGraph.Border.IsVisible"/> property).
			/// true to display frames around bars, false otherwise
			/// </summary>
			public static bool IsBorderVisible = true;
			/// <summary>
			/// The default color for drawing frames around bars
			/// (<see cref="ZedGraph.Border.Color"/> property).
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color for filling in the bars
			/// (<see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FillColor = Color.Red;
			/// <summary>
			/// The default custom brush for filling in the bars
			/// (<see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FillBrush = null; //new LinearGradientBrush( new Rectangle(0,0,100,100),
				// Color.White, Color.Red, 0F );
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Default constructor that sets all <see cref="Bar"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		public Bar() : this( Color.Empty )
		{
		}

		/// <summary>
		/// Default constructor that sets the 
		/// <see cref="Color"/> as specified, and the remaining
		/// <see cref="Bar"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// The specified color is only applied to the
		/// <see cref="ZedGraph.Fill.Color"/>, and the <see cref="ZedGraph.Border.Color"/>
		/// will be defaulted.
		/// </summary>
		/// <param name="color">A <see cref="Color"/> value indicating
		/// the <see cref="ZedGraph.Fill.Color"/>
		/// of the Bar.
		/// </param>
		public Bar( Color color )
		{
			this.border = new Border( Default.IsBorderVisible, Default.BorderColor, Default.BorderWidth );
			this.fill = new Fill( color.IsEmpty ? Default.FillColor : color,
						Default.FillBrush, Default.FillType );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Bar object from which to copy</param>
		public Bar( Bar rhs )
		{
			this.border = (Border) rhs.Border.Clone();
			this.fill = (Fill) rhs.Fill.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Bar</returns>
		public object Clone()
		{ 
			return new Bar( this ); 
		}
	#endregion

	#region Properties
		/// <summary>
		/// The <see cref="Border"/> object used to draw the border around the <see cref="Bar"/>.
		/// </summary>
		/// <seealso cref="Default.IsBorderVisible"/>
		/// <seealso cref="Default.BorderWidth"/>
		/// <seealso cref="Default.BorderColor"/>
		public Border Border
		{
			get { return border; }
			set { border = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Bar"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}

		/// <summary>Returns a reference to the <see cref="Axis"/> object that is the "base"
		/// (independent axis) from which the <see cref="Bar"/>'s are drawn.
		/// The base axis is the axis from which the bars grow with increasing value.  This
		/// property is determined by the value of <see cref="GraphPane.BarBase"/>.
		/// </summary>
		/// <seealso cref="GraphPane.Default.BarBase"/>
		/// <seealso cref="ZedGraph.BarBase"/>
		/// <seealso cref="BarValueAxis"/>
		public Axis BarBaseAxis( GraphPane pane )
		{
			if ( pane.BarBase == BarBase.X )
				return pane.XAxis;
			else if ( pane.BarBase == BarBase.Y )
				return pane.YAxis;
			else
				return pane.Y2Axis;
		}
		/// <summary>Returns a reference to the <see cref="Axis"/> object that is the "value"
		/// (dependent axis) for the <see cref="Bar"/>'s.
		/// The value axis determines the height of the bars.  This
		/// property is determined by the value of <see cref="GraphPane.BarBase"/>.
		/// </summary>
		/// <seealso cref="GraphPane.Default.BarBase"/>
		/// <seealso cref="ZedGraph.BarBase"/>
		/// <seealso cref="BarBaseAxis"/>
		public Axis BarValueAxis( GraphPane pane, bool isY2Axis )
		{
			if ( pane.BarBase == BarBase.X )
			{
				if ( isY2Axis )
					return pane.Y2Axis;
				else
					return pane.YAxis;
			}
			else
				return pane.XAxis;
		}

	#endregion

	#region Rendering Methods
		/// <summary>
		/// Draw the <see cref="Bar"/> to the specified <see cref="Graphics"/> device
		/// at the specified location.  This routine draws a single bar.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="left">The x position of the left side of the bar in
		/// screen pixel units</param>
		/// <param name="right">The x position of the right side of the bar in
		/// screen pixel units</param>
		/// <param name="top">The y position of the top of the bar in
		/// screen pixel units</param>
		/// <param name="bottom">The y position of the bottom of the bar in
		/// screen pixel units</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="GraphPane.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="GraphPane.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="fullFrame">true to draw the bottom portion of the border around the
		/// bar (this is for legend entries)</param>	
		public void Draw( Graphics g, float left, float right, float top,
						float bottom, double scaleFactor, bool fullFrame )
		{
			if ( top > bottom )
			{
				float junk = top;
				top = bottom;
				bottom = junk;
			}

			if ( left > right )
			{
				float junk = right;
				right = left;
				left = junk;
			}

			RectangleF rect = new RectangleF( left, top, right - left, bottom - top );
			
			Draw( g, rect, scaleFactor, fullFrame );			
		}

		/// <summary>
		/// Draw the <see cref="Bar"/> to the specified <see cref="Graphics"/> device
		/// at the specified location.  This routine draws a single bar.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">The rectangle (screen pixels) to contain the bar</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="GraphPane.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="GraphPane.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="fullFrame">true to draw the bottom portion of the border around the
		/// bar (this is for legend entries)</param>	
		public void Draw( Graphics g, RectangleF rect, double scaleFactor, bool fullFrame )
		{
			// Fill the Bar
			if ( this.fill.IsVisible )
			{
				// just avoid height/width being less than 0.1 so GDI+ doesn't cry
				Brush brush = this.fill.MakeBrush( rect );
				g.FillRectangle( brush, rect );
				brush.Dispose();
			}

			// Border the Bar
			if ( !this.border.Color.IsEmpty )
				this.border.Draw( g, rect );
							
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
		/// <param name="points">A <see cref="PointPairList"/> of point values representing this
		/// <see cref="Bar"/>.</param>
		/// <param name="isY2Axis">A value indicating to which Y axis this <see cref="Bar"/> is assigned.
		/// true for the "Y2" axis, false for the "Y" axis.</param>
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
		public void DrawBars( Graphics g, GraphPane pane, PointPairList points, bool isY2Axis,
							float barWidth, int pos, double scaleFactor )
		{
			float 	tmpX, tmpY, tmpBase;
			float 	clusterWidth = pane.GetClusterWidth();
			float 	clusterGap = pane.MinClusterGap * barWidth;
			float 	barGap = barWidth * pane.MinBarGap;
			
			float	zeroPix, basePix;
		
			double	curX, curY, curBase;
						
			if ( pane.BarType == BarType.Overlay )
				pos = 0;
				
			// Determine which Axis is the bar base and which is the bar value
			Axis valueAxis = BarValueAxis( pane, isY2Axis );

			// Determine the pixel value where the "base" of the bar lies
			if ( valueAxis.Min < 0.0 && valueAxis.Max > 0.0 )
				zeroPix = valueAxis.Transform( 0.0 );
			else if ( valueAxis.Min < 0.0 && valueAxis.Max <= 0.0 )
				zeroPix = ( pane.BarBase == BarBase.Y ) ? valueAxis.MaxPix : valueAxis.MinPix;
			else
				zeroPix = ( pane.BarBase == BarBase.Y ) ? valueAxis.MinPix : valueAxis.MaxPix;
				
			bool isHiLow = ( pane.BarType == BarType.HiLow ) && ( points is PointTrioList );

			// Loop over each defined point							
			for ( int i=0; i<points.Count; i++ )
			{
				curX = points[i].X;
				curY = points[i].Y;
				
				curBase = 0;
				if ( isHiLow )
					curBase = ( (PointTrioList) points)[i].BaseVal;
					
				if ( curBase == PointPair.Missing ||
						System.Double.IsNaN( curBase ) ||
						System.Double.IsInfinity( curBase ) )
					curBase = 0;
				
				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				
				if (	curY != PointPair.Missing &&
						!System.Double.IsNaN( curY ) &&
						!System.Double.IsInfinity( curY ) )
				{
					tmpX = pane.XAxis.Transform( i, curX );
					if ( isY2Axis )
					{
						tmpY = pane.Y2Axis.Transform( i, curY );
						tmpBase = pane.Y2Axis.Transform( i, curBase );
					}
					else
					{
						tmpY = pane.YAxis.Transform( i, curY );
						tmpBase = pane.YAxis.Transform( i, curBase );
					}
					
					if ( curBase == 0 )
						basePix = zeroPix;
					else
						basePix = tmpBase;

					if ( pane.BarBase == BarBase.X )
					{
						float left = tmpX - clusterWidth / 2.0F + clusterGap / 2.0F +
								pos * ( barWidth + barGap );

						this.Draw( g, left, left + barWidth, basePix,
							tmpY, scaleFactor, true );
					}
					else
					{
						float top = tmpY - clusterWidth / 2.0F + clusterGap / 2.0F +
								pos * ( barWidth + barGap );

						this.Draw( g, basePix, tmpX, top, top + barWidth,
								scaleFactor, true );
					}
				}
			}
		}

		/// <summary>
		/// Calculate the screen pixel position of the center of the specified bar, using the
		/// <see cref="Axis"/> as specified by <see cref="GraphPane.BarBase"/>.  This method is
		/// used primarily by the <see cref="GraphPane.FindNearestPoint"/> method in order to
		/// determine the bar "location," which is defined as the center of the top of the individual bar.
		/// </summary>
		/// <param name="pane">The active GraphPane object</param>
		/// <param name="barWidth">The width of each individual bar.  This can be calculated using
		/// the <see cref="GraphPane.CalcBarWidth"/> method.</param>
		/// <param name="iCluster">The cluster number for the bar of interest.  This is the ordinal
		/// position of the current point.  That is, if a particular <see cref="CurveItem"/> has
		/// 10 points, then a value of 3 would indicate the 4th point in the data array.</param>
		/// <param name="val">The actual independent axis value for the bar of interest.</param>
		/// <param name="iOrdinal">The ordinal position of the <see cref="CurveItem"/> of interest.
		/// That is, the first bar series is 0, the second is 1, etc.  Note that this applies only
		/// to the bars.  If a graph includes both bars and lines, then count only the bars.</param>
		/// <returns>A screen pixel X position of the center of the bar of interest.</returns>
		public static float CalcBarCenter( GraphPane pane, float barWidth, int iCluster, double val, int iOrdinal )
		{
			float clusterWidth = pane.GetClusterWidth();
			float clusterGap = pane.MinClusterGap * barWidth;
			float barGap = barWidth * pane.MinBarGap;

			if ( pane.BarType == BarType.Overlay )
				iOrdinal = 0;

			return pane.BarBaseAxis().Transform( iCluster, val )
						- clusterWidth / 2.0F + clusterGap / 2.0F +
						iOrdinal * ( barWidth + barGap ) + 0.5F * barWidth;
		}
	#endregion
	}
}
