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
	/// A class representing all the characteristics of the <see cref="Line"/>
	/// segments that make up a curve on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 2.0 $ $Date: 2004-09-02 06:24:59 $ </version>
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
			/// The default mode for displaying line segments (<see cref="Line.IsVisible"/>
			/// property).  True to show the line segments, false to hide them.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default width for line segments (<see cref="Line.Width"/> property).
			/// Units are pixels.
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
		/// The pen width used to draw the <see cref="Line"/>, in pixel units
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
		/// Render a single <see cref="Line"/> segment to the specified
		/// <see cref="Graphics"/> device.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="x1">The x position of the starting point that defines the
		/// line segment in screen pixel units</param>
		/// <param name="y1">The y position of the starting point that defines the
		/// line segment in screen pixel units</param>
		/// <param name="x2">The x position of the ending point that defines the
		/// line segment in screen pixel units</param>
		/// <param name="y2">The y position of the ending point that defines the
		/// line segment in screen pixel units</param>
		public void Draw( Graphics g, float x1, float y1, float x2, float y2 )
		{
			if ( this.isVisible && !this.Color.IsEmpty )
			{
				Pen pen = new Pen( this.color, this.width );
				pen.DashStyle = this.Style;
				g.DrawLine( pen, x1, y1, x2, y2 );
			}
		}
	#endregion
	}
}
