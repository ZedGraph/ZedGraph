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

namespace ZedGraph
{
	/// <summary>
	/// This class encapsulates the chart <see cref="Legend"/> that is displayed
	/// in the <see cref="GraphPane"/>
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.2 $ $Date: 2004-09-30 05:03:42 $ </version>
	public class Legend : ICloneable
	{
	#region private Fields
	
		/// <summary> Private field to hold the bounding rectangle around the legend.
		/// This bounding rectangle varies with the number of legend entries, font sizes,
		/// etc., and is re-calculated by <see cref="Legend.CalcRect"/> at each redraw.
		/// Use the public readonly property <see cref="Legend.Rect"/> to access this
		/// rectangle.
		/// </summary>
		private RectangleF	rect;
		/// <summary>Private field to hold the legend location setting.  This field
		/// contains the <see cref="LegendLoc"/> enum type to specify the area of
		/// the graph where the legend will be positioned.  Use the public property
		/// <see cref="LegendLoc"/> to access this value.
		/// </summary>
		/// <seealso cref="Default.Location"/>
		private LegendLoc	location;
		/// <summary>
		/// Private field to enable/disable the drawing of the frame around the
		/// legend bounding box.  Use the public property <see cref="IsFramed"/>
		/// to access this value.
		/// </summary>
		/// <seealso cref="Default.IsFramed"/>
		private bool		isFramed;
		/// <summary>
		/// Private field to enable/disable horizontal stacking of the legend entries.
		/// If this value is false, then the legend entries will always be a single column.
		/// Use the public property <see cref="IsHStack"/> to access this value.
		/// </summary>
		/// <seealso cref="Default.IsHStack"/>
		private bool		isHStack;
		/// <summary>
		/// Private field to enable/disable drawing of the entire legend.
		/// If this value is false, then the legend will not be drawn.
		/// Use the public property <see cref="IsVisible"/> to access this value.
		/// </summary>
		private bool		isVisible;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Legend"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill		fill;
		/// <summary>
		/// Private field to store the color of the frame around the legend bounding box.
		/// This value only applies if <see cref="IsFramed"/> is true.
		/// Use the public property <see cref="FrameColor"/> to access this value.
		/// </summary>
		/// <seealso cref="frameWidth"/>
		private Color		frameColor;
		/// <summary>
		/// Private field to store the width (pixels) of the frame around the legend bounding box.
		/// This value only applies if <see cref="IsFramed"/> is true.
		/// Use the public property <see cref="FrameWidth"/> to access this value.
		/// </summary>
		/// <seealso cref="frameColor"/>
		private float		frameWidth;
		
		/// <summary>
		/// Private field to maintain the <see cref="FontSpec"/> class that
		/// maintains font attributes for the entries in this legend.  Use
		/// the <see cref="FontSpec"/> property to access this class.
		/// </summary>
		private FontSpec	fontSpec;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Legend"/> class.
		/// </summary>
		public struct Default
		{
			// Default Legend properties
			/// <summary>
			/// The default pen width for the <see cref="Legend"/> frame border.
			/// (<see cref="Legend.FrameWidth"/> property).  Units are in pixels.
			/// </summary>
			public static float FrameWidth = 1;
			/// <summary>
			/// The default color for the <see cref="Legend"/> frame border.
			/// (<see cref="Legend.FrameColor"/> property). 
			/// </summary>
			public static Color FrameColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="Legend"/> background.
			/// (<see cref="ZedGraph.Fill.Color"/> property).  Use of this
			/// color depends on the status of the <see cref="ZedGraph.Fill.Type"/>
			/// property.
			/// </summary>
			public static Color FillColor = Color.White;
			/// <summary>
			/// The default custom brush for filling in this <see cref="Legend"/>.
			/// </summary>
			public static Brush FillBrush = null;
			/// <summary>
			/// The default fill mode for the <see cref="Legend"/> background.
			/// </summary>
			public static FillType FillType = FillType.Brush;
			/// <summary>
			/// The default location for the <see cref="Legend"/> on the graph
			/// (<see cref="Legend.Location"/> property).  This property is
			/// defined as a <see cref="LegendLoc"/> enumeration.
			/// </summary>
			public static LegendLoc Location = LegendLoc.Top;
			/// <summary>
			/// The default frame mode for the <see cref="Legend"/>.
			/// (<see cref="Legend.IsFramed"/> property). true
			/// to draw a frame around the <see cref="Legend.Rect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsFramed = true;
			/// <summary>
			/// The default display mode for the <see cref="Legend"/>.
			/// (<see cref="Legend.IsVisible"/> property). true
			/// to show the legend,
			/// false to hide it.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default fill mode for the <see cref="Legend"/> background
			/// (<see cref="ZedGraph.Fill.Type"/> property).
			/// true to fill-in the background with color,
			/// false to leave the background transparent.
			/// </summary>
			public static bool IsFilled = true;
			/// <summary>
			/// The default horizontal stacking mode for the <see cref="Legend"/>
			/// (<see cref="Legend.IsHStack"/> property).
			/// true to allow horizontal legend item stacking, false to allow
			/// only vertical legend orientation.
			/// </summary>
			public static bool IsHStack = true;

			/// <summary>
			/// The default font family for the <see cref="Legend"/> entries
			/// (<see cref="ZedGraph.FontSpec.Family"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size for the <see cref="Legend"/> entries
			/// (<see cref="ZedGraph.FontSpec.Size"/> property).  Units are
			/// in points (1/72 inch).
			/// </summary>
			public static float FontSize = 12;
			/// <summary>
			/// The default font color for the <see cref="Legend"/> entries
			/// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the <see cref="Legend"/> entries
			/// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = false;
			/// <summary>
			/// The default font italic mode for the <see cref="Legend"/> entries
			/// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
			/// <summary>
			/// The default font underline mode for the <see cref="Legend"/> entries
			/// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			/// <summary>
			/// The default color for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FontFillColor = Color.White;
			/// <summary>
			/// The default custom brush for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FontFillBrush = null;
			/// <summary>
			/// The default fill mode for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FontFillType = FillType.None;
		}
	#endregion

	#region Properties
		/// <summary>
		/// Get the bounding rectangle for the <see cref="Legend"/> in screen coordinates
		/// </summary>
		/// <value>A screen rectangle in pixel units</value>
		public RectangleF Rect
		{
			get { return rect; }
		}
		/// <summary>
		/// Access to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the <see cref="Legend"/> entries
		/// </summary>
		/// <value>A reference to a <see cref="Legend"/> object</value>
		/// <seealso cref="Default.FontColor"/>
		/// <seealso cref="Default.FontBold"/>
		/// <seealso cref="Default.FontItalic"/>
		/// <seealso cref="Default.FontUnderline"/>
		/// <seealso cref="Default.FontFamily"/>
		/// <seealso cref="Default.FontSize"/>
		public FontSpec FontSpec
		{
			get { return fontSpec; }
		}
		/// <summary>
		/// Gets or sets a property that shows or hides the <see cref="Legend"/> entirely
		/// </summary>
		/// <value> true to show the <see cref="Legend"/>, false to hide it </value>
		/// <seealso cref="Default.IsVisible"/>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		/// <summary>
		/// Set to true to display a frame around the text using the
		/// <see cref="FrameColor"/> color and <see cref="FrameWidth"/>
		/// pen width, or false for no frame
		/// </summary>
		/// <seealso cref="Default.IsFramed"/>
		public bool IsFramed
		{
			get { return isFramed; }
			set { isFramed = value; }
		}
		/// <summary>
		/// The pen width used for drawing the frame around the text
		/// </summary>
		/// <value>A pen width in pixel units</value>
		/// <seealso cref="Default.FrameWidth"/>
		public float FrameWidth
		{
			get { return frameWidth; }
			set { frameWidth = value; }
		}
		/// <summary>
		/// Sets or gets the color of the frame around the <see cref="Legend"/>.  This
		/// frame is turned on or off using the <see cref="IsFramed"/>
		/// property and the pen width is specified with the
		/// <see cref="FrameWidth"/> property
		/// </summary>
		/// <value>A system <see cref="System.Drawing.Color"/> specification.</value>
		/// <seealso cref="Default.FrameColor"/>
		public Color FrameColor
		{
			get { return frameColor; }
			set { frameColor = value; }
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Legend"/> background.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}
		
		/// <summary>
		/// Sets or gets a property that allows the <see cref="Legend"/> items to
		/// stack horizontally in addition to the vertical stacking
		/// </summary>
		/// <value>true to allow horizontal stacking, false otherwise
		/// </value>
		/// <seealso cref="Default.IsHStack"/>
		public bool IsHStack
		{
			get { return isHStack; }
			set { isHStack = value; }
		}
		/// <summary>
		/// Sets or gets the location of the <see cref="Legend"/> on the
		/// <see cref="GraphPane"/> using the <see cref="LegendLoc"/> enum type
		/// </summary>
		/// <seealso cref="Default.Location"/>
		public LegendLoc Location
		{
			get { return location; }
			set { location = value; }
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// Default constructor that sets all <see cref="Legend"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		public Legend()
		{
			this.location = Default.Location;
			this.isFramed = Default.IsFramed;
			this.frameColor = Default.FrameColor;
			this.frameWidth = Default.FrameWidth;
			this.isHStack = Default.IsHStack;
			this.isVisible = Default.IsVisible;
			
			this.fontSpec = new FontSpec( Default.FontFamily, Default.FontSize,
									Default.FontColor, Default.FontBold,
									Default.FontItalic, Default.FontUnderline,
									Default.FontFillColor, Default.FontFillBrush,
									Default.FontFillType );						
			this.fontSpec.IsFramed = false;
			
			this.fill = new Fill( Default.FillColor, Default.FillBrush, Default.FillType );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The XAxis object from which to copy</param>
		public Legend( Legend rhs )
		{
			rect = rhs.Rect;
			location = rhs.Location;
			isFramed = rhs.isFramed;
			isHStack = rhs.IsHStack;
			isVisible = rhs.IsVisible;
			frameColor = rhs.FrameColor;
			frameWidth = rhs.FrameWidth;
			
			this.fill = (Fill) rhs.Fill.Clone();
			
			fontSpec = (FontSpec) rhs.FontSpec.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Legend</returns>
		public object Clone()
		{ 
			return new Legend( this ); 
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Render the <see cref="Legend"/> to the specified <see cref="Graphics"/> device
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphPane"/> object.
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
		/// <param name="hStack">The number of columns (horizontal stacking) to be used
		/// for drawing the legend</param>
		/// <param name="legendWidth">The width of each column in the legend</param>
		public void Draw( Graphics g, GraphPane pane, double scaleFactor,
							int hStack, float legendWidth )
		{
			// if the legend is not visible, do nothing
			if ( ! this.isVisible )
				return;
								
			// Fill the background with the specified color if required
			if ( this.fill.IsFilled )
			{
				Brush brush = this.fill.MakeBrush( this.Rect );
				g.FillRectangle( brush, this.rect );
				brush.Dispose();
			}
		
			// Set up some scaled dimensions for calculating sizes and locations
			float	charHeight = this.FontSpec.GetHeight( scaleFactor ),
					halfCharHeight = charHeight / 2.0F;
			float charWidth = this.FontSpec.GetWidth( g, scaleFactor );
			float gap = pane.ScaledGap( scaleFactor );

			int		iEntry = 0;
			float	x, y;
			
			// Get a brush for the legend label text
			SolidBrush brushB = new SolidBrush( Color.Black );
			
			// Loop for each curve in the CurveList collection
			foreach( CurveItem curve in pane.CurveList )
			{	
				// Calculate the x,y (TopLeft) location of the current
				// curve legend label
				// assuming:
				//  charHeight/2 for the left margin, plus legendWidth for each
				//    horizontal column
				//  charHeight is the line spacing, with no extra margin above
				x = this.rect.Left + halfCharHeight +
							( iEntry % hStack ) * legendWidth;
				y = this.rect.Top + (int)( iEntry / hStack ) * charHeight;
				// Draw the legend label for the current curve
				this.FontSpec.Draw( g, curve.Label, x + 2.5F * charHeight, y,
								AlignH.Left, AlignV.Top, scaleFactor );
				
				if ( curve.IsBar )
				{
					curve.Bar.Draw( g, x, x + 2 * charHeight, y + charHeight / 4.0F,
								y + 3.0F * charHeight / 4.0F, scaleFactor, true );
				}
				else
				{
					// Draw a sample curve to the left of the label text
					curve.Line.DrawSegment( g, x, y + charHeight / 2,
						x + 2 * charHeight, y + halfCharHeight );
					// Draw a sample symbol to the left of the label text				
					curve.Symbol.DrawSymbol( g, x + charHeight, y + halfCharHeight,
						scaleFactor );
				}
									
				// maintain a curve count for positioning
				iEntry++;
			}
		
		
			// Draw a frame around the legend if required
			if ( iEntry > 0 && this.isFramed )
			{
				Pen pen = new Pen( this.frameColor, this.frameWidth );
				g.DrawRectangle( pen, Rectangle.Round( this.rect ) );
			}
		}

		/// <summary>
		/// Determine if a mouse point is within the legend, and if so, which legend
		/// entry (<see cref="CurveItem"/>) is nearest.
		/// </summary>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="hStack">The number of columns (horizontal stacking) to be used
		/// for drawing the legend</param>
		/// <param name="legendWidth">The width of each column in the legend</param>
		/// <param name="index">The index number of the <see cref="CurveItem"/> legend
		/// entry that is under the mouse point.  The <see cref="CurveItem"/> object is
		/// accessible via <see cref="GraphPane.CurveList">CurveList[index]</see>.
		/// </param>
		/// <returns>true if the mouse point is within the <see cref="Legend"/> bounding
		/// box, false otherwise.</returns>
		/// <seealso cref="GraphPane.FindNearestObject"/>
		public bool FindPoint( PointF mousePt, double scaleFactor, int hStack,
							float legendWidth, out int index )
		{
			index = -1;
			
			if ( this.rect.Contains( mousePt ) )
			{
				float	charHeight = this.FontSpec.GetHeight( scaleFactor ),
						halfCharHeight = charHeight / 2.0F;
				int j = (int) ( ( mousePt.Y - this.rect.Top ) / charHeight );
				int i = (int) ( ( mousePt.X - this.rect.Left - halfCharHeight ) / legendWidth );
				if ( i < 0 )
					i = 0;
				if ( i >= hStack )
					i = hStack - 1;
					
				index = i + j * hStack;
				return true;				
			}
			else
				return false;
		}
		
		/// <summary>
		/// Calculate the <see cref="Legend"/> rectangle (<see cref="Rect"/>),
		/// taking into account the number of required legend
		/// entries, and the legend drawing preferences.  Adjust the size of the
		/// <see cref="GraphPane.AxisRect"/> for the parent <see cref="GraphPane"/> to accomodate the
		/// space required by the legend.
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
		/// <param name="tAxisRect">
		/// The rectangle that contains the area bounded by the axes, in pixel units.
		/// <seealso cref="GraphPane.AxisRect">AxisRect</seealso>
		/// </param>
		/// <param name="hStack">The number of columns (horizontal stacking) to be used
		/// for drawing the legend</param>
		/// <param name="legendWidth">The width of each column in the legend (pixels)</param>
		public void CalcRect( Graphics g, GraphPane pane, double scaleFactor,
								ref RectangleF tAxisRect, out int hStack,
								out float legendWidth )
		{
			// Start with an empty rectangle
			this.rect = Rectangle.Empty;
			hStack = 1;
			legendWidth = 1;
		
			// If the legend is invisible, don't do anything
			if ( !this.isVisible )
				return;
		
			int		nCurve = 0;
			float	charHeight = this.FontSpec.GetHeight( scaleFactor ),
					halfCharHeight = charHeight / 2.0F,
					charWidth = this.FontSpec.GetWidth( g, scaleFactor ),
					gap = pane.ScaledGap( scaleFactor ),
					maxWidth = 0,
					tmpWidth;
							
			// Loop through each curve in the curve list
			// Find the maximum width of the legend labels
			foreach( CurveItem curve in pane.CurveList )
			{
				// Calculate the width of the label save the max width

				tmpWidth = this.FontSpec.GetWidth( g, curve.Label, scaleFactor );

				if ( tmpWidth > maxWidth )
					maxWidth = tmpWidth;
	
				nCurve++;
			}
		
			float widthAvail;
		
			// Is this legend horizontally stacked?
			if ( this.isHStack )
			{
				// Determine the available space for horizontal stacking
				switch( this.location )
				{
					// Never stack if the legend is to the right or left
					case LegendLoc.Right:
					case LegendLoc.Left:
						widthAvail = 0;
						break;
		
					// for the top & bottom, the axis frame width is available
					case LegendLoc.Top:
					case LegendLoc.Bottom:
						widthAvail = tAxisRect.Width;
						break;
		
					// for inside the axis area, use 1/2 of the axis frame width
					case LegendLoc.InsideTopRight:
					case LegendLoc.InsideTopLeft:
					case LegendLoc.InsideBotRight:
					case LegendLoc.InsideBotLeft:
						widthAvail = tAxisRect.Width / 2;
						break;
		
					// shouldn't ever happen
					default:
						widthAvail = 0;
						break;
				}
		
				// width of one legend entry
				legendWidth = 3 * charHeight + maxWidth;

				// Calculate the number of columns in the legend
				// Normally, the legend is:
				//     available width / ( max width of any entry + space for line&symbol )
				if ( maxWidth > 0 )
					hStack = (int) ( (widthAvail - halfCharHeight) / legendWidth );
		
				// You can never have more columns than legend entries
				if ( hStack > nCurve )
					hStack = nCurve;
		
				// a saftey check
				if ( hStack == 0 )
					hStack = 1;
			}
			else
				legendWidth = 3.5F * charHeight + maxWidth;
		
			// legend is:
			//   item:     space  line  space  text   space
			//   width:     wid  4*wid   wid  maxWid   wid 
			// The symbol is centered on the line
			//
			// legend begins 3 * wid to the right of the plot rect
			//
			// The height of the legend is the actual height of the lines of text
			//   (nCurve * hite) plus wid on top and wid on the bottom
		
			// total legend width
			float totLegWidth = hStack * legendWidth;	
		
			// The total legend height
			float legHeight = (float) Math.Ceiling( (double) nCurve / (double) hStack )
									* charHeight;
			
			RectangleF newRect = new RectangleF();
			
			// Now calculate the legend rect based on the above determined parameters
			// Also, adjust the axisRect to reflect the space for the legend
			if ( nCurve > 0 )
			{
				// The switch statement assigns the left and top edges, and adjusts the axisRect
				// as required.  The right and bottom edges are calculated at the bottom of the switch.
				switch( this.location )
				{
					case LegendLoc.Right:
						newRect.X = pane.PaneRect.Right - totLegWidth - gap;
						newRect.Y = tAxisRect.Top;
		
						tAxisRect.Width -= totLegWidth + halfCharHeight;
						break;
					case LegendLoc.Top:
						newRect.X = tAxisRect.Left;
						newRect.Y = tAxisRect.Top;
						
						tAxisRect.Y += legHeight + halfCharHeight;
						tAxisRect.Height -= legHeight + halfCharHeight;
						break;
					case LegendLoc.Bottom:
						newRect.X = tAxisRect.Left;
						newRect.Y = pane.PaneRect.Bottom - legHeight - gap;
						
						tAxisRect.Height -= legHeight + halfCharHeight;
						break;
					case LegendLoc.Left:
						newRect.X = pane.PaneRect.Left + gap;
						newRect.Y = tAxisRect.Top;
						
						tAxisRect.X += totLegWidth + halfCharHeight;
						tAxisRect.Width -= totLegWidth + halfCharHeight;
						break;
					case LegendLoc.InsideTopRight:
						newRect.X = tAxisRect.Right - totLegWidth;
						newRect.Y = tAxisRect.Top;
						break;
					case LegendLoc.InsideTopLeft:
						newRect.X = tAxisRect.Left;
						newRect.Y = tAxisRect.Top;
						break;
					case LegendLoc.InsideBotRight:
						newRect.X = tAxisRect.Right - totLegWidth;
						newRect.Y = tAxisRect.Bottom - legHeight;
						break;
					case LegendLoc.InsideBotLeft:
						newRect.X = tAxisRect.Left;
						newRect.Y = tAxisRect.Bottom - legHeight;
						break;
				}
				
				// Calculate the Right and Bottom edges of the rect
				newRect.Width = totLegWidth;
				newRect.Height = legHeight;
			}
			
			this.rect = newRect;
		}
	#endregion
	}
}

