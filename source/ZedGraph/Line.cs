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
	/// A class representing all the characteristics of the Line
	/// segments that make up a curve on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.4.2.1 $ $Date: 2005-01-16 04:11:47 $ </version>
	public class Line : ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the pen width for this
		/// <see cref="Line"/>.  Use the public
		/// property <see cref="Width"/> to access this value.
		/// </summary>
		private float width;
		/// <summary>
		/// Private field that stores the <see cref="DashStyle"/> for this
		/// <see cref="Line"/>.  Use the public
		/// property <see cref="Style"/> to access this value.
		/// </summary>
		private DashStyle style;
		/// <summary>
		/// Private field that stores the visibility of this
		/// <see cref="Line"/>.  Use the public
		/// property <see cref="IsVisible"/> to access this value.
		/// </summary>
		private bool isVisible;
		/// <summary>
		/// Private field that stores the smoothing flag for this
		/// <see cref="Line"/>.  Use the public
		/// property <see cref="IsSmooth"/> to access this value.
		/// </summary>
		private bool isSmooth;
		/// <summary>
		/// Private field that stores the smoothing tension
		/// for this <see cref="Line"/>.  Use the public property
		/// <see cref="SmoothTension"/> to access this value.
		/// </summary>
		/// <value>A floating point value indicating the level of smoothing.
		/// 0.0F for no smoothing, 1.0F for lots of smoothing, >1.0 for odd
		/// smoothing.</value>
		/// <seealso cref="IsSmooth"/>
		/// <seealso cref="Default.IsSmooth"/>
		/// <seealso cref="Default.SmoothTension"/>
		private float smoothTension;
		/// <summary>
		/// Private field that stores the color of this
		/// <see cref="Line"/>.  Use the public
		/// property <see cref="Color"/> to access this value.  If this value is
		/// false, the line will not be shown (but the <see cref="Symbol"/> may
		/// still be shown).
		/// </summary>
		private Color color;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.StepType"/> for this
		/// <see cref="CurveItem"/>.  Use the public
		/// property <see cref="StepType"/> to access this value.
		/// </summary>
		private StepType	stepType;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Line"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill		fill;

	#endregion
	
	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Line"/> class.
		/// </summary>
		public struct Default
		{
			// Default Line properties
			/// <summary>
			/// The default color for curves (line segments connecting the points).
			/// This is the default value for the <see cref="Line.Color"/> property.
			/// </summary>
			public static Color Color = Color.Red;
			/// <summary>
			/// The default color for filling in the area under the curve
			/// (<see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FillColor = Color.Red;
			/// <summary>
			/// The default custom brush for filling in the area under the curve
			/// (<see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FillBrush = null;
			/// <summary>
			/// The default fill mode for the curve (<see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FillType = FillType.None;
			/// <summary>
			/// The default mode for displaying line segments (<see cref="Line.IsVisible"/>
			/// property).  True to show the line segments, false to hide them.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default width for line segments (<see cref="Line.Width"/> property).
            /// Units are points (1/72 inch).
            /// </summary>
			public static float Width = 1;
			/// <summary>
			/// The default value for the <see cref="Line.IsSmooth"/>
			/// property.
			/// </summary>
			public static bool IsSmooth = false;
			/// <summary>
			/// The default value for the <see cref="Line.SmoothTension"/> property.
			/// </summary>
			public static float SmoothTension = 0.5F;
			/// <summary>
			/// The default drawing style for line segments (<see cref="Line.Style"/> property).
			/// This is defined with the <see cref="DashStyle"/> enumeration.
			/// </summary>
			public static DashStyle Style = DashStyle.Solid;
			/// <summary>
			/// Default value for the curve type property
			/// (<see cref="Line.StepType"/>).  This determines if the curve
			/// will be drawn by directly connecting the points from the
			/// <see cref="CurveItem.Points"/> data collection,
			/// or if the curve will be a "stair-step" in which the points are
			/// connected by a series of horizontal and vertical lines that
			/// represent discrete, staticant values.  Note that the values can
			/// be forward oriented <code>ForwardStep</code> (<see cref="StepType"/>) or
			/// rearward oriented <code>RearwardStep</code>.
			/// That is, the points are defined at the beginning or end
			/// of the staticant value for which they apply, respectively.
			/// </summary>
			/// <value><see cref="StepType"/> enum value</value>
			public static StepType StepType = StepType.NonStep;
		}
	#endregion

	#region Properties
		/// <summary>
		/// The color of the <see cref="Line"/>
		/// </summary>
		/// <seealso cref="Default.Color"/>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		/// <summary>
		/// The style of the <see cref="Line"/>, defined as a <see cref="DashStyle"/> enum.
		/// This allows the line to be solid, dashed, or dotted.
		/// </summary>
		/// <seealso cref="Default.Style"/>
		public DashStyle Style
		{
			get { return style; }
			set { style = value;}
		}
		/// <summary>
        /// The pen width used to draw the <see cref="Line"/>, in points (1/72 inch)
        /// </summary>
		/// <seealso cref="Default.Width"/>
		public float Width
		{
			get { return width; }
			set { width = value; }
		}
		/// <summary>
		/// Gets or sets a property that shows or hides the <see cref="Line"/>.
		/// </summary>
		/// <value>true to show the line, false to hide it</value>
		/// <seealso cref="Default.IsVisible"/>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines if this <see cref="Line"/>
		/// will be drawn smooth.  The "smoothness" is controlled by
		/// the <see cref="SmoothTension"/> property.
		/// </summary>
		/// <value>true to smooth the line, false to just connect the dots
		/// with linear segments</value>
		/// <seealso cref="SmoothTension"/>
		/// <seealso cref="Default.IsSmooth"/>
		/// <seealso cref="Default.SmoothTension"/>
		public bool IsSmooth
		{
			get { return isSmooth; }
			set { isSmooth = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines the smoothing tension
		/// for this <see cref="Line"/>.  This property is only used if
		/// <see cref="IsSmooth"/> is true.  A tension value 0.0 will just
		/// draw ordinary line segments like an unsmoothed line.  A tension
		/// value of 1.0 will be smooth.  Values greater than 1.0 will generally
		/// give odd results.
		/// </summary>
		/// <value>A floating point value indicating the level of smoothing.
		/// 0.0F for no smoothing, 1.0F for lots of smoothing, >1.0 for odd
		/// smoothing.</value>
		/// <seealso cref="IsSmooth"/>
		/// <seealso cref="Default.IsSmooth"/>
		/// <seealso cref="Default.SmoothTension"/>
		public float SmoothTension
		{
			get { return smoothTension; }
			set { smoothTension = value; }
		}
		/// <summary>
		/// Determines if the <see cref="CurveItem"/> will be drawn by directly connecting the
		/// points from the <see cref="CurveItem.Points"/> data collection,
		/// or if the curve will be a "stair-step" in which the points are
		/// connected by a series of horizontal and vertical lines that
		/// represent discrete, constant values.  Note that the values can
		/// be forward oriented <c>ForwardStep</c> (<see cref="ZedGraph.StepType"/>) or
		/// rearward oriented <c>RearwardStep</c>.
		/// That is, the points are defined at the beginning or end
		/// of the constant value for which they apply, respectively.
		/// The <see cref="StepType"/> property is ignored for lines
		/// that have <see cref="IsSmooth"/> set to true.
		/// </summary>
		/// <value><see cref="ZedGraph.StepType"/> enum value</value>
		/// <seealso cref="Default.StepType"/>
		public StepType StepType
		{
			get { return stepType; }
			set { stepType = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Line"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}

	#endregion
	
	#region Constructors
		/// <summary>
		/// Default constructor that sets all <see cref="Line"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		public Line() : this( Color.Empty )
		{
		}

		/// <summary>
		/// Constructor that sets the color property to the specified value, and sets
		/// the remaining <see cref="Line"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="color">The color to assign to this new Line object</param>
		public Line( Color color )
		{
			this.width = Default.Width;
			this.style = Default.Style;
			this.isVisible = Default.IsVisible;
			this.color = color.IsEmpty ? Default.Color : color;
			this.stepType = Default.StepType;
			this.isSmooth = Default.IsSmooth;
			this.smoothTension = Default.SmoothTension;
			this.fill = new Fill( Default.FillColor, Default.FillBrush, Default.FillType );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Line object from which to copy</param>
		public Line( Line rhs )
		{
			width = rhs.Width;
			style = rhs.Style;
			isVisible = rhs.IsVisible;
			color = rhs.Color;
			stepType = rhs.StepType;
			isSmooth = rhs.IsSmooth;
			smoothTension = rhs.SmoothTension;
			fill = (Fill) rhs.Fill.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Line</returns>
		public object Clone()
		{ 
			return new Line( this ); 
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="Line"/> to the specified
		/// <see cref="Graphics"/> device.  This method is normally only
		/// called by the Draw method of the parent <see cref="LineItem"/> object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="points">A <see cref="PointPairList"/> of point values representing this
		/// curve.</param>
		/// <param name="isY2Axis">A value indicating to which Y axis this curve is assigned.
		/// true for the "Y2" axis, false for the "Y" axis.</param>
		public void Draw( Graphics g, GraphPane pane, PointPairList points,
                        bool isY2Axis, double scaleFactor)
        {
			// If the line is being shown, draw it
			if ( this.IsVisible )
			{
				if ( this.IsSmooth || this.Fill.IsVisible )
                    DrawSmoothFilledCurve(g, pane, points, isY2Axis, scaleFactor);
                else
                    DrawCurve(g, pane, points, isY2Axis, scaleFactor);
            }
		}		

		/// <summary>
		/// Render a single <see cref="Line"/> segment to the specified
		/// <see cref="Graphics"/> device.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="x1">The x position of the starting point that defines the
        /// line segment in screen pixel units</param>
		/// <param name="y1">The y position of the starting point that defines the
		/// line segment in screen pixel units</param>
		/// <param name="x2">The x position of the ending point that defines the
		/// line segment in screen pixel units</param>
		/// <param name="y2">The y position of the ending point that defines the
		/// line segment in screen pixel units</param>
        public void DrawSegment(Graphics g, GraphPane pane, float x1, float y1,
                            float x2, float y2, double scaleFactor)
        {
			if ( this.isVisible && !this.Color.IsEmpty )
			{
                Pen pen = new Pen(this.color, pane.ScaledPenWidth(width, scaleFactor));
                pen.DashStyle = this.Style;
				g.DrawLine( pen, x1, y1, x2, y2 );
			}
		}

		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device using the specified smoothing property (<see cref="ZedGraph.Line.SmoothTension"/>).
		/// The routine draws the line segments and the area fill (if any, see <see cref="FillType"/>;
		/// the symbols are drawn by the <see cref="Symbol.Draw"/> method.  This method
		/// is normally only called by the Draw method of the
		/// <see cref="CurveItem"/> object.  Note that the <see cref="StepType"/> property
		/// is ignored for smooth lines (e.g., when <see cref="ZedGraph.Line.IsSmooth"/> is true).
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="points">A <see cref="PointPairList"/> of point values representing this
		/// curve.</param>
		/// <param name="isY2Axis">A value indicating to which Y axis this curve is assigned.
		/// true for the "Y2" axis, false for the "Y" axis.</param>
		public void DrawSmoothFilledCurve( Graphics g, GraphPane pane,
                                PointPairList points, bool isY2Axis, double scaleFactor)
        {
			PointF[]	arrPoints;
			int			count;

			if ( this.IsVisible && !this.Color.IsEmpty && points != null &&
				BuildPointsArray( pane, points, isY2Axis, out arrPoints, out count ) &&
				count > 2 )
			{
                Pen pen = new Pen(this.Color, pane.ScaledPenWidth(width, scaleFactor));
                pen.DashStyle = this.Style;
				float tension = this.isSmooth ? this.smoothTension : 0f;
				
				// Fill the curve if needed
				if ( this.Fill.IsVisible )
				{
					GraphicsPath path = new GraphicsPath( FillMode.Winding );
					path.AddCurve( arrPoints, 0, count-2, tension );

					double yMin = pane.YAxis.Min < 0 ? 0.0 : pane.YAxis.Min;
					//double yMin = pane.YAxis.Min;

					CloseCurve( pane, arrPoints, isY2Axis, count, yMin, path );
				
					Brush brush = this.fill.MakeBrush( path.GetBounds() );
					g.FillPath( brush, path );

					brush.Dispose();
				}

				// Stroke the curve
				g.DrawCurve( pen, arrPoints, 0, count-2, tension );
			}
		}

		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device.  The format (stair-step or line) of the curve is
		/// defined by the <see cref="StepType"/> property.  The routine
		/// only draws the line segments; the symbols are drawn by the
		/// <see cref="Symbol.Draw"/> method.  This method
		/// is normally only called by the Draw method of the
		/// <see cref="CurveItem"/> object
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="points">A <see cref="PointPairList"/> of point values representing this
		/// curve.</param>
		/// <param name="isY2Axis">A value indicating to which Y axis this curve is assigned.
		/// true for the "Y2" axis, false for the "Y" axis.</param>
		public void DrawCurve( Graphics g, GraphPane pane,
                                PointPairList points, bool isY2Axis, double scaleFactor)
        {
			float	tmpX, tmpY,
					lastX = 0,
					lastY = 0;
			double	curX, curY;
			bool	broke = true;

            Pen pen = new Pen(this.color, pane.ScaledPenWidth(width, scaleFactor));
            pen.DashStyle = this.Style;

			if ( points != null && !this.color.IsEmpty && this.IsVisible )
			{
				// Loop over each point in the curve
				for ( int i=0; i<points.Count; i++ )
				{
					curX = points[i].X;
					curY = points[i].Y;
					
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
							( isY2Axis && pane.Y2Axis.IsLog && curY <= 0.0 ) ||
							( !isY2Axis && pane.YAxis.IsLog && curY <= 0.0 ) )
					{
						broke = true;
					}
					else
					{
						// Transform the current point from user scale units to
						// screen coordinates
						tmpX = pane.XAxis.Transform( i, curX );
						if ( isY2Axis )
							tmpY = pane.Y2Axis.Transform( i, curY );
						else
							tmpY = pane.YAxis.Transform( i, curY );
						
						// off-scale values "break" the line
						if ( tmpX < -100000 || tmpX > 100000 ||
							tmpY < -100000 || tmpY > 100000 )
							broke = true;
						else
						{
							// If the last two points are valid, draw a line segment
							if ( !broke || ( pane.IsIgnoreMissing && lastX != 0 ) )
							{
								if ( this.StepType == StepType.ForwardStep )
								{
									g.DrawLine( pen, lastX, lastY, tmpX, lastY );
									g.DrawLine( pen, tmpX, lastY, tmpX, tmpY );
								}
								else if ( this.StepType == StepType.RearwardStep )
								{
									g.DrawLine( pen, lastX, lastY, lastX, tmpY );
									g.DrawLine( pen, lastX, tmpY, tmpX, tmpY );
								}
								else 		// non-step
									g.DrawLine( pen, lastX, lastY, tmpX, tmpY );

							}

							// Save some values for the next point
							broke = false;
							lastX = tmpX;
							lastY = tmpY;
						}
					}
				}
			}
		}

		/// <summary>
		/// Build an array of <see cref="PointF"/> values (pixel coordinates) that represents
		/// the current curve.  Note that this drawing routine ignores <see cref="PointPair.Missing"/>
		/// values, but it does not "break" the line to indicate values are missing.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.</param>
		/// <param name="points">A <see cref="PointPairList"/> of point values representing this
		/// curve.</param>
		/// <param name="isY2Axis">A value indicating to which Y axis this curve is assigned.
		/// true for the "Y2" axis, false for the "Y" axis.</param>
		/// <param name="arrPoints">An array of <see cref="PointF"/> values in pixel
		/// coordinates representing the current curve.</param>
		/// <param name="count">The number of points contained in the "arrPoints"
		/// parameter.</param>
		/// <returns>true for a successful points array build, false for data problems</returns>
		public bool BuildPointsArray( GraphPane pane, PointPairList points, bool isY2Axis,
									out PointF[] arrPoints, out int count )
		{
			arrPoints = null;
			count = 0;

			if ( this.IsVisible && !this.Color.IsEmpty && points != null )
			{
				int		index = 0;
				float	curX, curY,
						lastX = 0,
						lastY = 0;

				// Step type plots get twice as many points.  Always add three points so there is
				// room to close out the curve for area fills.
				arrPoints = new PointF[ ( this.stepType == ZedGraph.StepType.NonStep ? 1 : 2 )
											* points.Count + 1 ];

				for ( int i=0; i<points.Count; i++ )
				{
					if ( !points[i].IsInvalid )
					{
						curX = pane.XAxis.Transform( i, points[i].X );
						if ( isY2Axis )
							curY = pane.Y2Axis.Transform( i, points[i].Y );
						else
							curY = pane.YAxis.Transform( i, points[i].Y );

						// ignore step-type setting for smooth curves
						if ( this.isSmooth || index == 0 || this.StepType == StepType.NonStep )
						{
							arrPoints[index].X = curX;
							arrPoints[index].Y = curY;
						}
						if ( this.StepType == StepType.ForwardStep )
						{
							arrPoints[index].X = curX;
							arrPoints[index].Y = lastY;
							index++;
							arrPoints[index].X = curX;
							arrPoints[index].Y = curY;
						}
						else if ( this.StepType == StepType.RearwardStep )
						{
							arrPoints[index].X = lastX;
							arrPoints[index].Y = curY;
							index++;
							arrPoints[index].X = curX;
							arrPoints[index].Y = curY;
						}

						lastX = curX;
						lastY = curY;
						index++;
						
					}

				}

				// Make sure there is at least one valid point
				if ( index == 0 )
					return false;
					
				// Add an extra point at the end, since the smoothing algorithm requires it
				arrPoints[index] = arrPoints[index-1];
				index++;
												
				count = index;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.</param>
		/// <param name="arrPoints">An array of <see cref="PointF"/> values in screen pixel
		/// coordinates representing the current curve.</param>
		/// <param name="isY2Axis">A value indicating to which Y axis this curve is assigned.
		/// true for the "Y2" axis, false for the "Y" axis.</param>
		/// <param name="count">The number of points contained in the "arrPoints"
		/// parameter.</param>
		/// <param name="yMin">The Y axis value location where the X axis crosses.</param>
		/// <param name="path">The <see cref="GraphicsPath"/> class that represents the curve.</param>
		public void CloseCurve( GraphPane pane, PointF[] arrPoints, bool isY2Axis,
									int count, double yMin, GraphicsPath path )
		{
			float yBase;
			if ( isY2Axis )
				yBase = pane.Y2Axis.Transform( yMin );
			else
				yBase = pane.YAxis.Transform( yMin );
			
			path.AddLine( arrPoints[count-1].X, arrPoints[count-1].Y, arrPoints[count-1].X, yBase );
			path.AddLine( arrPoints[count-1].X, yBase, arrPoints[0].X, yBase );
			path.AddLine( arrPoints[0].X, yBase, arrPoints[0].X, arrPoints[0].Y );
			
		}

	#endregion
	}
}
