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
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
   /// <summary>
   /// A class representing all the characteristics of the bar
   /// segments that make up a curve on the graph.
   /// </summary>
   /// 
   /// <author> John Champion </author>
   /// <version> $Revision: 3.18 $ $Date: 2005-03-25 16:19:56 $ </version>
   [Serializable]
   public class Bar : ICloneable, ISerializable
   {
	#region Fields
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Bar"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill  fill;
		/// <summary>
		/// Private field that stores the <see cref="Border"/> class that defines the
		/// properties of the border around this <see cref="BarItem"/>. Use the public
		/// property <see cref="Border"/> to access this value.
		/// </summary>
		private Border border;
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

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected Bar( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			border = (Border) info.GetValue( "border", typeof(Border) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );
			info.AddValue( "fill", fill );
			info.AddValue( "border", border );
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
		public Fill Fill
		{
			get { return fill; }
			set { fill = value; }
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
		/// <param name="pane">
		/// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="left">The x position of the left side of the bar in
		/// pixel units</param>
		/// <param name="right">The x position of the right side of the bar in
		/// pixel units</param>
		/// <param name="top">The y position of the top of the bar in
		/// pixel units</param>
		/// <param name="bottom">The y position of the bottom of the bar in
		/// pixel units</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="fullFrame">true to draw the bottom portion of the border around the
		/// bar (this is for legend entries)</param> 
		/// <param name="dataValue">The data value to be used for a value-based
		/// color gradient.  This is only applicable for <see cref="FillType.GradientByX"/>,
		/// <see cref="FillType.GradientByY"/> or <see cref="FillType.GradientByZ"/>.</param>
		public void Draw( Graphics g, GraphPane pane, float left, float right, float top,
							float bottom, float scaleFactor, bool fullFrame,
							PointPair dataValue )
		{
			// Do a sanity check to make sure the top < bottom.  If not, reverse them
			if ( top > bottom )
			{
				float junk = top;
				top = bottom;
				bottom = junk;
			}

			// Do a sanity check to make sure the left < right.  If not, reverse them
			if ( left > right )
			{
				float junk = right;
				right = left;
				left = junk;
			}

			// Make a rectangle for the bar and draw it
			RectangleF rect = new RectangleF( left, top, right - left, bottom - top );

			Draw( g, pane, rect, scaleFactor, fullFrame, dataValue );      
		}

		/// <summary>
		/// Draw the <see cref="Bar"/> to the specified <see cref="Graphics"/> device
		/// at the specified location.  This routine draws a single bar.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="rect">The rectangle (pixels) to contain the bar</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="fullFrame">true to draw the bottom portion of the border around the
		/// bar (this is for legend entries)</param> 
		/// <param name="dataValue">The data value to be used for a value-based
		/// color gradient.  This is only applicable for <see cref="FillType.GradientByX"/>,
		/// <see cref="FillType.GradientByY"/> or <see cref="FillType.GradientByZ"/>.</param>
		public void Draw( Graphics g, GraphPane pane, RectangleF rect, float scaleFactor,
							bool fullFrame, PointPair dataValue )
		{
			// Fill the Bar
			if ( this.fill.IsVisible )
			{
				// just avoid height/width being less than 0.1 so GDI+ doesn't cry
				Brush brush = this.fill.MakeBrush( rect, dataValue );
				g.FillRectangle( brush, rect );
				brush.Dispose();
			}

			// Border the Bar
			if ( !this.border.Color.IsEmpty )
				this.border.Draw( g, pane.IsPenWidthScaled, scaleFactor, rect );

		}

		/// <summary>
		/// Draw the this <see cref="Bar"/> to the specified <see cref="Graphics"/>
		/// device as a bar at each defined point. This method
		/// is normally only called by the <see cref="BarItem.Draw"/> method of the
		/// <see cref="BarItem"/> object
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="curve">A <see cref="CurveItem"/> object representing the
		/// <see cref="Bar"/>'s to be drawn.</param>
		/// <param name="baseAxis">The <see cref="Axis"/> class instance that defines the base (independent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="valueAxis">The <see cref="Axis"/> class instance that defines the value (dependent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="barWidth">
		/// The width of each bar, in pixels.
		/// </param>
		/// <param name="pos">
		/// The ordinal position of the this bar series (0=first bar, 1=second bar, etc.)
		/// in the cluster of bars.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawBars( Graphics g, GraphPane pane, CurveItem curve,
								Axis baseAxis, Axis valueAxis,
								float barWidth, int pos, float scaleFactor )
		{
			// For Overlay and Stack bars, the position is always zero since the bars are on top
			// of eachother
			if	( pane.BarType == BarType.Overlay || pane.BarType == BarType.Stack ||
					pane.BarType == BarType.PercentStack )
				pos = 0;

			// Loop over each defined point and draw the corresponding bar                
			for ( int i=0; i<curve.Points.Count; i++ )
				DrawSingleBar( g, pane, curve, i, pos, baseAxis, valueAxis, scaleFactor );
		}

		/// <summary>
		/// Draw the specified single bar (an individual "point") of this series to the specified
		/// <see cref="Graphics"/> device.  This method is not as efficient as
		/// <see cref="DrawBars"/>, which draws the bars for all points.  It is intended to be used
		/// only for <see cref="BarType.SortedOverlay"/>, which requires special handling of each bar.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="curve">A <see cref="CurveItem"/> object representing the
		/// <see cref="Bar"/>'s to be drawn.</param>
		/// <param name="baseAxis">The <see cref="Axis"/> class instance that defines the base (independent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="valueAxis">The <see cref="Axis"/> class instance that defines the value (dependent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="pos">
		/// The ordinal position of the this bar series (0=first bar, 1=second bar, etc.)
		/// in the cluster of bars.
		/// </param>
		/// <param name="index">
		/// The zero-based index number for the single bar to be drawn.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawSingleBar( Graphics g, GraphPane pane, CurveItem curve,
									Axis baseAxis, Axis valueAxis,
									int pos, int index, float scaleFactor )
		{
			// Make sure that a bar value exists for the current curve and current ordinal position
			if ( index >= curve.Points.Count )
				return;

			// For Overlay and Stack bars, the position is always zero since the bars are on top
			// of eachother
			if	( pane.BarType == BarType.Overlay || pane.BarType == BarType.Stack ||
					pane.BarType == BarType.PercentStack )
				pos = 0;

			// Draw the specified bar
			DrawSingleBar( g, pane, curve, index, pos, baseAxis, valueAxis, scaleFactor );
		}

		/// <summary>
		/// Protected internal routine that draws the specified single bar (an individual "point")
		/// of this series to the specified <see cref="Graphics"/> device.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="curve">A <see cref="CurveItem"/> object representing the
		/// <see cref="Bar"/>'s to be drawn.</param>
		/// <param name="index">
		/// The zero-based index number for the single bar to be drawn.
		/// </param>
		/// <param name="pos">
		/// The ordinal position of the this bar series (0=first bar, 1=second bar, etc.)
		/// in the cluster of bars.
		/// </param>
		/// <param name="baseAxis">The <see cref="Axis"/> class instance that defines the base (independent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="valueAxis">The <see cref="Axis"/> class instance that defines the value (dependent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		virtual protected void DrawSingleBar( Graphics g, GraphPane pane,
										CurveItem curve,
										int index, int pos, Axis baseAxis, Axis valueAxis,
										float scaleFactor )
		{
			// pixBase = pixel value for the bar center on the base axis
			// pixHiVal = pixel value for the bar top on the value axis
			// pixLowVal = pixel value for the bar bottom on the value axis
			float pixBase, pixHiVal, pixLowVal;

			float clusterWidth = pane.GetClusterWidth();
			float barWidth = curve.GetBarWidth( pane );
			float clusterGap = pane.MinClusterGap * barWidth;
			float barGap = barWidth * pane.MinBarGap;

			// curBase = the scale value on the base axis of the current bar
			// curHiVal = the scale value on the value axis of the current bar
			// curLowVal = the scale value of the bottom of the bar
			double curBase, curLowVal, curHiVal;
			ValueHandler valueHandler = new ValueHandler( pane, false );
			valueHandler.GetValues( curve, index, out curBase, out curLowVal, out curHiVal );

			// Any value set to double max is invalid and should be skipped
			// This is used for calculated values that are out of range, divide
			//   by zero, etc.
			// Also, any value <= zero on a log scale is invalid

			if ( !curve.Points[index].IsInvalid )
			{
				// calculate a pixel value for the top of the bar on value axis
				pixLowVal = valueAxis.Transform( index, curLowVal );
				pixHiVal = valueAxis.Transform( index, curHiVal );
				// calculate a pixel value for the center of the bar on the base axis
				pixBase = baseAxis.Transform( index, curBase );

				// Calculate the pixel location for the side of the bar (on the base axis)
				float pixSide = pixBase - clusterWidth / 2.0F + clusterGap / 2.0F +
								pos * ( barWidth + barGap );

				// Draw the bar
				if ( pane.BarBase == BarBase.X )
					this.Draw( g, pane, pixSide, pixSide + barWidth, pixLowVal,
							pixHiVal, scaleFactor, true,
							curve.Points[index] );
				else
					this.Draw( g, pane, pixLowVal, pixHiVal, pixSide, pixSide + barWidth,
							scaleFactor, true,
							curve.Points[index] );
			}
		}

	#endregion
	}
}
