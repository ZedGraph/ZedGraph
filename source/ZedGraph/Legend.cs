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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// This class encapsulates the chart <see cref="Legend"/> that is displayed
	/// in the <see cref="GraphPane"/>
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.23 $ $Date: 2005-03-01 01:27:27 $ </version>
	[Serializable]
	public class Legend : ICloneable, ISerializable
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
		/// contains the <see cref="LegendPos"/> enum type to specify the area of
		/// the graph where the legend will be positioned.  Use the public property
		/// <see cref="LegendPos"/> to access this value.
		/// </summary>
		/// <seealso cref="Default.Position"/>
		private LegendPos	position;
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
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for this
		/// <see cref="Legend"/>.  Use the public property <see cref="Border"/> to
		/// access this value.
		/// </summary>
		private Border		border;		
		/// <summary>
		/// Private field to maintain the <see cref="FontSpec"/> class that
		/// maintains font attributes for the entries in this legend.  Use
		/// the <see cref="FontSpec"/> property to access this class.
		/// </summary>
		private FontSpec	fontSpec;
		/// <summary>
		/// Private field to maintain the <see cref="Legend"/> location.  This object
		/// is only applicable if the <see cref="Position"/> property is set to
		/// <see cref="LegendPos.Float"/>.
		/// </summary>
		private Location	location;

		/// <summary>
		/// Private temporary field to maintain the number of columns (horizontal stacking) to be used
		/// for drawing the <see cref="Legend"/>.  This value is only valid during a draw operation.
		/// </summary>
		private int			hStack;
		/// <summary>
		/// Private temporary field to maintain the width of each column in the
		/// <see cref="Legend"/>.  This value is only valid during a draw operation.
		/// </summary>
		private float		legendItemWidth;
		/// <summary>
		/// Private temporary field to maintain the height of each row in the
		/// <see cref="Legend"/>.  This value is only valid during a draw operation.
		/// </summary>
		private float		legendItemHeight;

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
			/// The default pen width for the <see cref="Legend"/> border border.
			/// (<see cref="ZedGraph.Border.PenWidth"/> property).  Units are in pixels.
			/// </summary>
			public static float BorderWidth = 1;
			/// <summary>
			/// The default color for the <see cref="Legend"/> border border.
			/// (<see cref="ZedGraph.Border.Color"/> property). 
			/// </summary>
			public static Color BorderColor = Color.Black;
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
			/// defined as a <see cref="LegendPos"/> enumeration.
			/// </summary>
			public static LegendPos Position = LegendPos.Top;
			/// <summary>
			/// The default border mode for the <see cref="Legend"/>.
			/// (<see cref="ZedGraph.Border.IsVisible"/> property). true
			/// to draw a border around the <see cref="Legend.Rect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsBorderVisible = true;
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
		/// The <see cref="Border"/> class used to draw the border border around this <see cref="Legend"/>.
		/// </summary>
		public Border Border
		{
			get { return border; }
			set { border = value; }
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
		/// <see cref="GraphPane"/> using the <see cref="LegendPos"/> enum type
		/// </summary>
		/// <seealso cref="Default.Position"/>
		public LegendPos Position
		{
			get { return position; }
			set { position = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="Location"/> data for the <see cref="Legend"/>.
		/// This property is only applicable if <see cref="Position"/> is set
		/// to <see cref="LegendPos.Float"/>.
		/// </summary>
		public Location Location
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
			this.position = Default.Position;
			this.isHStack = Default.IsHStack;
			this.isVisible = Default.IsVisible;
			this.Location = new Location( 0, 0, CoordType.PaneFraction );
			
			this.fontSpec = new FontSpec( Default.FontFamily, Default.FontSize,
									Default.FontColor, Default.FontBold,
									Default.FontItalic, Default.FontUnderline,
									Default.FontFillColor, Default.FontFillBrush,
									Default.FontFillType );						
			this.fontSpec.Border.IsVisible = false;
			
			this.border = new Border( Default.IsBorderVisible, Default.BorderColor, Default.BorderWidth );
			this.fill = new Fill( Default.FillColor, Default.FillBrush, Default.FillType );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The XAxis object from which to copy</param>
		public Legend( Legend rhs )
		{
			rect = rhs.Rect;
			position = rhs.Position;
			isHStack = rhs.IsHStack;
			isVisible = rhs.IsVisible;
			
			this.location = (Location) rhs.Location;
			this.border = (Border) rhs.Border.Clone();
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
		protected Legend( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			position = (LegendPos) info.GetValue( "position", typeof(LegendPos) );
			isHStack = info.GetBoolean( "isHStack" );
			isVisible = info.GetBoolean( "isVisible" );
			fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			border = (Border) info.GetValue( "border", typeof(Border) );
			fontSpec = (FontSpec) info.GetValue( "fontSpec", typeof(FontSpec) );
			location = (Location) info.GetValue( "location", typeof(Location) );
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
			info.AddValue( "position", position );
			info.AddValue( "isHStack", isHStack );
			info.AddValue( "isVisible", isVisible );
			info.AddValue( "fill", fill );
			info.AddValue( "border", border );
			info.AddValue( "fontSpec", fontSpec );
			info.AddValue( "location", location );
		}
	#endregion

	#region Rendering Methods
		/// <summary>
		/// Render the <see cref="Legend"/> to the specified <see cref="Graphics"/> device.
		/// </summary>
		/// <remarks>
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphPane"/> object.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
        public void Draw( Graphics g, PaneBase pane, float scaleFactor )
		{
			// if the legend is not visible, do nothing
			if ( ! this.isVisible )
				return;
								
			// Fill the background with the specified color if required
			this.fill.Draw( g, this.rect );
		
			// Set up some scaled dimensions for calculating sizes and locations
			float	charHeight = this.FontSpec.GetHeight( scaleFactor ),
					halfCharHeight = charHeight / 2.0F;
			float charWidth = this.FontSpec.GetWidth( g, scaleFactor );

			// Check for bad data values
			if ( this.hStack <= 0 )
				this.hStack = 1;
			if ( this.legendItemWidth <= 0 )
				this.legendItemWidth = 100;
			if ( this.legendItemHeight <= 0 )
				this.legendItemHeight = charHeight;

			//float gap = pane.ScaledGap( scaleFactor );

			int		iEntry = 0;
			float	x, y;
			
			// Get a brush for the legend label text
			SolidBrush brushB = new SolidBrush( Color.Black );
			

			PaneList paneList = GetPaneList( pane );

			foreach ( GraphPane tmpPane in paneList )
			{
				// Loop for each curve in the CurveList collection
				foreach ( CurveItem curve in tmpPane.CurveList )
				{
					if ( curve.Label != "" )
					{
						// Calculate the x,y (TopLeft) location of the current
						// curve legend label
						// assuming:
						//  charHeight/2 for the left margin, plus legendWidth for each
						//    horizontal column
						//  legendHeight is the line spacing, with no extra margin above

						x = this.rect.Left + halfCharHeight / 2.0F +
									( iEntry % hStack ) * this.legendItemWidth;
						y = this.rect.Top + (int) ( iEntry / hStack ) * this.legendItemHeight;

						// Draw the legend label for the current curve
						this.FontSpec.Draw( g, pane.IsPenWidthScaled, curve.Label,
										x + 2.5F * charHeight, y + this.legendItemHeight / 2.0F,
										AlignH.Left, AlignV.Center, scaleFactor );

						RectangleF rect = new RectangleF( x, y + this.legendItemHeight / 4.0F,
												2 * charHeight, this.legendItemHeight / 2.0F );
						curve.DrawLegendKey( g, tmpPane, rect, scaleFactor );

						// maintain a curve count for positioning
						iEntry++;
					}
				}
				if ( pane is MasterPane && ((MasterPane)pane).HasUniformLegendEntries )	
					break ;
			}

			// Draw a border around the legend if required
			if ( iEntry > 0 )
				this.Border.Draw( g, pane.IsPenWidthScaled, scaleFactor, this.rect );
		}

		/// <summary>
		/// Determine if a mouse point is within the legend, and if so, which legend
		/// entry (<see cref="CurveItem"/>) is nearest.
		/// </summary>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="index">The index number of the <see cref="CurveItem"/> legend
		/// entry that is under the mouse point.  The <see cref="CurveItem"/> object is
		/// accessible via <see cref="GraphPane.CurveList">CurveList[index]</see>.
		/// </param>
		/// <returns>true if the mouse point is within the <see cref="Legend"/> bounding
		/// box, false otherwise.</returns>
		/// <seealso cref="GraphPane.FindNearestObject"/>
		public bool FindPoint( PointF mousePt, PaneBase pane, float scaleFactor, out int index )
		{
			index = -1;
			
			if ( this.rect.Contains( mousePt ) )
			{
				float	charHeight = this.FontSpec.GetHeight( scaleFactor ),
						halfCharHeight = charHeight / 2.0F;
				int j = (int) ( ( mousePt.Y - this.rect.Top ) / charHeight );
				int i = (int) ( ( mousePt.X - this.rect.Left - halfCharHeight ) / this.legendItemWidth );
				if ( i < 0 )
					i = 0;
				if ( i >= hStack )
					i = hStack - 1;
					
				int pos = i + j * hStack;
				index = 0;

				PaneList paneList = GetPaneList( pane );

				foreach ( GraphPane tmpPane in paneList )
				{
					foreach ( CurveItem curve in tmpPane.CurveList )
					{
						if ( curve.IsLegendLabelVisible && curve.Label != "" )
						{
							if ( pos == 0 )
								return true;
							pos--;
						}
						index++;
					}
				}

				return true;
			}
			else
				return false;
		}
		
		private PaneList GetPaneList( PaneBase pane )
		{
			// For a single GraphPane, create a PaneList to contain it
			// Otherwise, just use the paneList from the MasterPane
			PaneList paneList;
			
			if ( pane is GraphPane )
			{
				paneList = new PaneList();
				paneList.Add( (GraphPane) pane );
			}
			else
				paneList = ((MasterPane)pane).PaneList;
			
			return paneList;
		}

		/// <summary>
		/// Calculate the <see cref="Legend"/> rectangle (<see cref="Rect"/>),
		/// taking into account the number of required legend
		/// entries, and the legend drawing preferences.
		/// </summary>
		/// <remarks>Adjust the size of the
		/// <see cref="GraphPane.AxisRect"/> for the parent <see cref="GraphPane"/> to accomodate the
		/// space required by the legend.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="tAxisRect">
		/// The rectangle that contains the area bounded by the axes, in pixel units.
		/// <seealso cref="GraphPane.AxisRect">AxisRect</seealso>
		/// </param>
        public void CalcRect( Graphics g, PaneBase pane, float scaleFactor,
                                        ref RectangleF tAxisRect )
		{
			// Start with an empty rectangle
			this.rect = Rectangle.Empty;
			this.hStack = 1;
			this.legendItemWidth = 1;
			this.legendItemHeight = 0;

            // If the legend is invisible, don't do anything
			if ( !this.isVisible )
				return;
		
			int		nCurve = 0;
			float	charHeight = this.FontSpec.GetHeight( scaleFactor ),
					halfCharHeight = charHeight / 2.0F,
					charWidth = this.FontSpec.GetWidth( g, scaleFactor ),
					maxWidth = 0,
					tmpWidth;

			PaneList paneList = GetPaneList( pane );

			foreach ( GraphPane tmpPane in paneList )
			{
				// Loop through each curve in the curve list
				// Find the maximum width of the legend labels
				foreach ( CurveItem curve in tmpPane.CurveList )
				{
					if ( curve.Label != "" && curve.IsLegendLabelVisible )
					{
						// Calculate the width of the label save the max width

						tmpWidth = this.FontSpec.GetWidth( g, curve.Label, scaleFactor );

						if ( tmpWidth > maxWidth )
							maxWidth = tmpWidth;

						// Save the maximum symbol height for line-type curves
						if ( curve is LineItem && ( (LineItem) curve ).Symbol.Size > this.legendItemHeight )
							this.legendItemHeight = ( (LineItem) curve ).Symbol.Size;

						nCurve++;
					}
				}
				if ( pane is MasterPane && ((MasterPane)pane).HasUniformLegendEntries )				
						break ;
			}

			float widthAvail;
		
			// Is this legend horizontally stacked?
			
			if ( this.isHStack )
			{
				// Determine the available space for horizontal stacking
				switch( this.position )
				{
					// Never stack if the legend is to the right or left
					case LegendPos.Right:
					case LegendPos.Left:
						widthAvail = 0;
						break;
		
					// for the top & bottom, the axis border width is available
					case LegendPos.Top:
					case LegendPos.TopCenter:
					case LegendPos.Bottom:
					case LegendPos.BottomCenter :
						widthAvail = tAxisRect.Width;
						break;
		
					// for inside the axis area or Float, use 1/2 of the axis border width
					case LegendPos.InsideTopRight:
					case LegendPos.InsideTopLeft:
					case LegendPos.InsideBotRight:
					case LegendPos.InsideBotLeft:
					case LegendPos.Float:
						widthAvail = tAxisRect.Width / 2;
						break;
		
					// shouldn't ever happen
					default:
						widthAvail = 0;
						break;
				}
		
				// width of one legend entry
				this.legendItemWidth = 3 * charHeight + maxWidth;

				// Calculate the number of columns in the legend
				// Normally, the legend is:
				//     available width / ( max width of any entry + space for line&symbol )
				if ( maxWidth > 0 )
					this.hStack = (int) ( (widthAvail - halfCharHeight) / this.legendItemWidth );
		
				// You can never have more columns than legend entries
				if ( this.hStack > nCurve )
					this.hStack = nCurve;
		
				// a saftey check
				if ( this.hStack == 0 )
					this.hStack = 1;
			}
			else
				this.legendItemWidth = 3.5F * charHeight + maxWidth;
		
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
			float totLegWidth = this.hStack * this.legendItemWidth;	
		
			// The total legend height
            this.legendItemHeight = this.legendItemHeight * (float) scaleFactor + halfCharHeight;
            if ( charHeight > this.legendItemHeight )
                this.legendItemHeight = charHeight;
            float totLegHeight = (float) Math.Ceiling( (double) nCurve / (double) hStack )
									* this.legendItemHeight;
			
			RectangleF newRect = new RectangleF();
			
			// Now calculate the legend rect based on the above determined parameters
			// Also, adjust the axisRect to reflect the space for the legend
			if ( nCurve > 0 )
			{
				newRect = new RectangleF( 0, 0, totLegWidth, totLegHeight );
				
				// The switch statement assigns the left and top edges, and adjusts the axisRect
				// as required.  The right and bottom edges are calculated at the bottom of the switch.
				switch( this.position )
				{
					case LegendPos.Right:
						newRect.X = pane.PaneRect.Right - totLegWidth - pane.MarginRight * (float) scaleFactor;
						newRect.Y = tAxisRect.Top;
		
						tAxisRect.Width -= totLegWidth + halfCharHeight;
						break;
					case LegendPos.Top:
						newRect.X = tAxisRect.Left;
						newRect.Y = tAxisRect.Top;
						
						tAxisRect.Y += totLegHeight + halfCharHeight;
						tAxisRect.Height -= totLegHeight + halfCharHeight;
						break;
					case LegendPos.TopCenter:
						newRect.X = tAxisRect.Left + ( tAxisRect.Width - totLegWidth ) / 2;
						newRect.Y = tAxisRect.Top;
						
						tAxisRect.Y += totLegHeight + halfCharHeight;
						tAxisRect.Height -= totLegHeight + halfCharHeight;
						break;
					case LegendPos.Bottom:
						newRect.X = tAxisRect.Left + ( tAxisRect.Width - totLegWidth ) /2 ;
						newRect.Y = pane.PaneRect.Bottom - totLegHeight - pane.MarginBottom * (float) scaleFactor;
						
						tAxisRect.Height -= totLegHeight + halfCharHeight;
						break;
					case LegendPos.BottomCenter:
						newRect.X = tAxisRect.Left + ( tAxisRect.Width - totLegWidth ) / 2;
						newRect.Y = tAxisRect.Bottom;
						
						tAxisRect.Height -= totLegHeight + halfCharHeight;
						break;
					case LegendPos.Left:
						newRect.X = pane.PaneRect.Left + pane.MarginLeft * (float) scaleFactor;
						newRect.Y = tAxisRect.Top;
						
						tAxisRect.X += totLegWidth + halfCharHeight;
						tAxisRect.Width -= totLegWidth + halfCharHeight;
						break;
					case LegendPos.InsideTopRight:
						newRect.X = tAxisRect.Right - totLegWidth;
						newRect.Y = tAxisRect.Top;
						break;
					case LegendPos.InsideTopLeft:
						newRect.X = tAxisRect.Left;
						newRect.Y = tAxisRect.Top;
						break;
					case LegendPos.InsideBotRight:
						newRect.X = tAxisRect.Right - totLegWidth;
						newRect.Y = tAxisRect.Bottom - totLegHeight;
						break;
					case LegendPos.InsideBotLeft:
						newRect.X = tAxisRect.Left;
						newRect.Y = tAxisRect.Bottom - totLegHeight;
						break;
					case LegendPos.Float:
						newRect.Location = this.Location.TransformTopLeft( pane, totLegWidth, totLegHeight );
						break;
				}
			}
			
			this.rect = newRect;
		}
	#endregion
	}
}

