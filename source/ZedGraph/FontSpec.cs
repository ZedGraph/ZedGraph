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
	/// The <see cref="FontSpec"/> class is a generic font class that maintains the font family,
	/// attributes, colors, border and fill modes, font size, and angle information.
	/// This class can render text with a variety of alignment options using the
	/// <see cref="AlignH"/> and <see cref="AlignV"/> parameters in the
	/// <see cref="Draw"/> method.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.6 $ $Date: 2004-10-14 04:06:01 $ </version>
	public class FontSpec : ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the color of the font characters for this
		/// <see cref="FontSpec"/>.  Use the public property <see cref="FontColor"/>
		/// to access this value.
		/// </summary>
		/// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
		private Color fontColor;
		/// <summary>
		/// Private field that stores the font family name for this <see cref="FontSpec"/>.
		/// Use the public property <see cref="Family"/> to access this value.
		/// </summary>
		/// <value>A text string with the font family name, e.g., "Arial"</value>
		private string family;
		/// <summary>
		/// Private field that determines whether this <see cref="FontSpec"/> is
		/// drawn with bold typeface.
		/// Use the public property <see cref="IsBold"/> to access this value.
		/// </summary>
		/// <value>A boolean value, true for bold, false for normal</value>
		private bool isBold;
		/// <summary>
		/// Private field that determines whether this <see cref="FontSpec"/> is
		/// drawn with italic typeface.
		/// Use the public property <see cref="IsItalic"/> to access this value.
		/// </summary>
		/// <value>A boolean value, true for italic, false for normal</value>
		private bool isItalic;
		/// <summary>
		/// Private field that determines whether this <see cref="FontSpec"/> is
		/// drawn with underlined typeface.
		/// Use the public property <see cref="IsUnderline"/> to access this value.
		/// </summary>
		/// <value>A boolean value, true for underline, false for normal</value>
		private bool isUnderline;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="FontSpec"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill		fill;
		/// <summary>
		/// Private field that determines the properties of the border around the text.
		/// Use the public property <see cref="Border"/> to access this value.
		/// </summary>
		private Border border;

		/// <summary>
		/// Private field that determines the angle at which this
		/// <see cref="FontSpec"/> object is drawn.  Use the public property
		/// <see cref="Angle"/> to access this value.
		/// </summary>
		/// <value>The angle of the font, measured in anti-clockwise degrees from
		/// horizontal.  Negative values are permitted.</value>
		private float angle;
		
		/// <summary>
		/// Private field that determines the alignment with which this
		/// <see cref="FontSpec"/> object is drawn.  This alignment really only
		/// affects multi-line strings.  Use the public property
		/// <see cref="StringAlignment"/> to access this value.
		/// </summary>
		/// <value>A <see cref="StringAlignment"/> enumeration.</value>
		private StringAlignment stringAlignment;
		
		/// <summary>
		/// Private field that determines the size of the font for this
		/// <see cref="FontSpec"/> object.  Use the public property
		/// <see cref="Size"/> to access this value.
		/// </summary>
		/// <value>The size of the font, measured in points (1/72 inch).</value>
		private float size;
		
		/// <summary>
		/// Private field that stores a reference to the <see cref="Font"/>
		/// object for this <see cref="FontSpec"/>.  This font object will be at
		/// the actual drawn size <see cref="scaledSize"/> according to the current
		/// size of the <see cref="GraphPane"/>.  Use the public method
		/// <see cref="GetFont"/> to access this font object.
		/// </summary>
		/// <value>A reference to a <see cref="Font"/> object</value>
		private Font font;

		/// <summary>
		/// Private field that stores a reference to the <see cref="Font"/>
		/// object that will be used for superscripts.  This font object will be a
		/// fraction of the <see cref="FontSpec"/> <see cref="scaledSize"/>,
		/// based on the value of <see cref="Default.SuperSize"/>.  This
		/// property is internal, and has no public access.
		/// </summary>
		/// <value>A reference to a <see cref="Font"/> object</value>
		private Font superScriptFont;

		/// <summary>
		/// Private field that temporarily stores the scaled size of the font for this
		/// <see cref="FontSpec"/> object.  This represents the actual on-screen
		/// size, rather than the <see cref="Size"/> that represents the reference
		/// size for a "full-sized" <see cref="GraphPane"/>.
		/// </summary>
		/// <value>The size of the font, measured in points (1/72 inch).</value>
		private float scaledSize;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="FontSpec"/> class.
		/// </summary>
		public struct Default
		{
			/// <summary>
			/// The default size fraction of the superscript font, expressed as a fraction
			/// of the size of the main font.
			/// </summary>
			public static float SuperSize = 0.85F;
			/// <summary>
			/// The default shift fraction of the superscript, expressed as a
			/// fraction of the superscripted character height.  This is the height
			/// above the main font (a zero shift means the main font and the superscript
			/// font have the tops aligned).
			/// </summary>
			public static float SuperShift = 0.4F;
			/// <summary>
			/// The default color for filling in the background of the text block
			/// (<see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FillColor = Color.White;
			/// <summary>
			/// The default custom brush for filling in this <see cref="FontSpec"/>
			/// (<see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FillBrush = null;
			/// <summary>
			/// The default fill mode for this <see cref="FontSpec"/>
			/// (<see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FillType = FillType.Solid;
			/// <summary>
			/// Default value for the alignment with which this
			/// <see cref="FontSpec"/> object is drawn.  This alignment really only
			/// affects multi-line strings.
			/// </summary>
			/// <value>A <see cref="StringAlignment"/> enumeration.</value>
			public static StringAlignment StringAlignment = StringAlignment.Center;
		
		}
	#endregion
	
	#region Properties
		/// <summary>
		/// The color of the font characters for this <see cref="FontSpec"/>.
		/// Note that the border and background
		/// colors are set using the <see cref="ZedGraph.Border.Color"/> and
		/// <see cref="ZedGraph.Fill.Color"/> properties, respectively.
		/// </summary>
		/// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
		public Color FontColor
		{
			get { return fontColor; }
			set { fontColor = value; }
		}
		/// <summary>
		/// The font family name for this <see cref="FontSpec"/>.
		/// </summary>
		/// <value>A text string with the font family name, e.g., "Arial"</value>
		public string Family
		{
			get { return family; }
			set
			{ 
				if ( value != family )
				{
					family = value;
					Remake( (double) scaledSize / size, this.Size, ref this.scaledSize, ref this.font );
				}
			}
		}
		/// <summary>
		/// Determines whether this <see cref="FontSpec"/> is
		/// drawn with bold typeface.
		/// </summary>
		/// <value>A boolean value, true for bold, false for normal</value>
		public bool IsBold
		{
			get { return isBold; }
			set
			{ 
				if ( value != isBold )
				{
					isBold = value;
					Remake( (double) scaledSize / size, this.Size, ref this.scaledSize, ref this.font );
				}
			}
		}
		/// <summary>
		/// Determines whether this <see cref="FontSpec"/> is
		/// drawn with italic typeface.
		/// </summary>
		/// <value>A boolean value, true for italic, false for normal</value>
		public bool IsItalic
		{
			get { return isItalic; }
			set
			{ 
				if ( value != isItalic )
				{
					isItalic = value;
					Remake( (double) scaledSize / size, this.Size, ref this.scaledSize, ref this.font );
				}
			}
		}
		/// <summary>
		/// Determines whether this <see cref="FontSpec"/> is
		/// drawn with underlined typeface.
		/// </summary>
		/// <value>A boolean value, true for underline, false for normal</value>
		public bool IsUnderline
		{
			get { return isUnderline; }
			set
			{ 
				if ( value != isUnderline )
				{
					isUnderline = value;
					Remake( (double) scaledSize / size, this.Size, ref this.scaledSize, ref this.font );
				}
			}
		}
		/// <summary>
		/// The angle at which this <see cref="FontSpec"/> object is drawn.
		/// </summary>
		/// <value>The angle of the font, measured in anti-clockwise degrees from
		/// horizontal.  Negative values are permitted.</value>
		public float Angle
		{
			get { return angle; }
			set { angle = value; }
		}
		
		/// <summary>
		/// Determines the alignment with which this
		/// <see cref="FontSpec"/> object is drawn.  This alignment really only
		/// affects multi-line strings.
		/// </summary>
		/// <value>A <see cref="StringAlignment"/> enumeration.</value>
		public StringAlignment StringAlignment
		{
			get { return stringAlignment; }
			set { stringAlignment = value; }
		}
		
		/// <summary>
		/// The size of the font for this <see cref="FontSpec"/> object.
		/// </summary>
		/// <value>The size of the font, measured in points (1/72 inch).</value>
		public float Size
		{
			get { return size; }
			set
			{ 
				if ( value != size )
				{
					Remake( (double) scaledSize / size * (double) value, size, ref scaledSize,
								ref this.font );
					size = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets the <see cref="Border"/> class used to draw the border border
		/// around this text.
		/// </summary>
		public Border Border
		{
			get { return border; }
			set { border = value; }
		}
				
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="FontSpec"/>, which controls how the background
		/// behind the text is filled.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}

	#endregion
	
	#region Constructors
		/// <summary>
		/// Construct a <see cref="FontSpec"/> object with the given properties.  All other properties
		/// are defaulted according to the values specified in the <see cref="Default"/>
		/// default class.
		/// </summary>
		/// <param name="family">A text string representing the font family
		/// (default is "Arial")</param>
		/// <param name="size">A size of the font in points.  This size will be scaled
		/// based on the ratio of the <see cref="GraphPane.PaneRect"/> dimension to the
		/// <see cref="GraphPane.BaseDimension"/> of the <see cref="GraphPane"/> object. </param>
		/// <param name="color">The color with which to render the font</param>
		/// <param name="isBold">true for a bold typeface, false otherwise</param>
		/// <param name="isItalic">true for an italic typeface, false otherwise</param>
		/// <param name="isUnderline">true for an underlined font, false otherwise</param>
		public FontSpec( string family, float size, Color color, bool isBold,
			bool isItalic, bool isUnderline )
		{
			this.fontColor = color;
			this.family = family;
			this.isBold = isBold;
			this.isItalic = isItalic;
			this.isUnderline = isUnderline;
			this.size = size;
			this.scaledSize = -1;
			this.angle = 0F;
			this.stringAlignment = Default.StringAlignment;

			this.border = new Border( true, Color.Black, 1.0F );
			this.fill = new Fill( Default.FillColor, Default.FillBrush, Default.FillType );
			
			Remake( 1.0, this.Size, ref this.scaledSize, ref this.font );
		}

		/// <summary>
		/// Construct a <see cref="FontSpec"/> object with the given properties.  All other properties
		/// are defaulted according to the values specified in the <see cref="Default"/>
		/// default class.
		/// </summary>
		/// <param name="family">A text string representing the font family
		/// (default is "Arial")</param>
		/// <param name="size">A size of the font in points.  This size will be scaled
		/// based on the ratio of the <see cref="GraphPane.PaneRect"/> dimension to the
		/// <see cref="GraphPane.BaseDimension"/> of the <see cref="GraphPane"/> object. </param>
		/// <param name="color">The color with which to render the font</param>
		/// <param name="isBold">true for a bold typeface, false otherwise</param>
		/// <param name="isItalic">true for an italic typeface, false otherwise</param>
		/// <param name="isUnderline">true for an underlined font, false otherwise</param>
		/// <param name="fillColor">The <see cref="Color"/> to use for filling in the text background</param>
		/// <param name="fillBrush">The <see cref="Brush"/> to use for filling in the text background</param>
		/// <param name="fillType">The <see cref="ZedGraph.FillType"/> to use for the
		/// text background</param>
		public FontSpec( string family, float size, Color color, bool isBold,
			bool isItalic, bool isUnderline, Color fillColor, Brush fillBrush,
			FillType fillType )
		{
			this.fontColor = color;
			this.family = family;
			this.isBold = isBold;
			this.isItalic = isItalic;
			this.isUnderline = isUnderline;
			this.size = size;
			this.scaledSize = -1;
			this.angle = 0F;
			this.stringAlignment = Default.StringAlignment;


			this.fill = new Fill( fillColor, fillBrush, fillType );
			this.border = new Border( true, Color.Black, 1.0F );
			
			Remake( 1.0, this.Size, ref this.scaledSize, ref this.font );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The FontSpec object from which to copy</param>
		public FontSpec( FontSpec rhs )
		{
			fontColor = rhs.FontColor;
			family = rhs.Family;
			isBold = rhs.IsBold;
			isItalic = rhs.IsItalic;
			isUnderline = rhs.IsUnderline;
			fill = (Fill) rhs.Fill.Clone();
			border = (Border) rhs.Border.Clone();

			stringAlignment = rhs.StringAlignment;
			angle = rhs.Angle;
			size = rhs.Size;
		
			scaledSize = rhs.scaledSize;
			Remake( 1.0, this.Size, ref this.scaledSize, ref this.font );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the FontSpec</returns>
		public object Clone()
		{ 
			return new FontSpec( this ); 
		}
	#endregion
	
	#region Font Construction Methods
		/// <summary>
		/// Recreate the font based on a new scaled size.  The font
		/// will only be recreated if the scaled size has changed by
		/// at least 0.1 points.
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="size">The unscaled size of the font, in points</param>
		/// <param name="scaledSize">The scaled size of the font, in points</param>
		/// <param name="font">A reference to the <see cref="Font"/> object</param>
		private void Remake( double scaleFactor, float size, ref float scaledSize, ref Font font )
		{
			float newSize = (float) ( size * scaleFactor );
			
			// Regenerate the font only if the size has changed significantly
			if ( font == null || Math.Abs( newSize - scaledSize ) > 0.1 )
			{
				FontStyle style = FontStyle.Regular;
				style = ( this.isBold ? FontStyle.Bold : style ) |
							( this.isItalic ? FontStyle.Italic : style ) |
							 ( this.isUnderline ? FontStyle.Underline : style );
				
				scaledSize = size * (float) scaleFactor;
				font = new Font( this.family, scaledSize, style );
			}
		}
		
		/// <summary>
		/// Get the <see cref="Font"/> class for the current scaled font.
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>Returns a reference to a <see cref="Font"/> object
		/// with a size of <see cref="scaledSize"/>, and font <see cref="Family"/>.
		/// </returns>
		public Font GetFont( double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			return this.font;
		}		
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Render the specified <paramref name="text"/> to the specifed
		/// <see cref="Graphics"/> device.  The text, border, and fill options
		/// will be rendered as required.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">A string value containing the text to be
		/// displayed.  This can be multiple lines, separated by newline ('\n')
		/// characters</param>
		/// <param name="x">The X location to display the text, in screen
		/// coordinates, relative to the horizontal (<see cref="AlignH"/>)
		/// alignment parameter <paramref name="alignH"/></param>
		/// <param name="y">The Y location to display the text, in screen
		/// coordinates, relative to the vertical (<see cref="AlignV"/>
		/// alignment parameter <paramref name="alignV"/></param>
		/// <param name="alignH">A horizontal alignment parameter specified
		/// using the <see cref="AlignH"/> enum type</param>
		/// <param name="alignV">A vertical alignment parameter specified
		/// using the <see cref="AlignV"/> enum type</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, string text, float x,
			float y, AlignH alignH, AlignV alignV,
			double scaleFactor )
		{
			// make sure the font size is properly scaled
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			
			// Get the width and height of the text
			SizeF sizeF = g.MeasureString( text, this.font );

			// Save the old transform matrix for later restoration
			Matrix matrix = g.Transform;
		
			// Move the coordinate system to local coordinates
			// of this text object (that is, at the specified
			// x,y location)
			g.TranslateTransform( x, y, MatrixOrder.Prepend );
			
			// Rotate the coordinate system according to the 
			// specified angle of the FontSpec
			if ( angle != 0.0F )
				g.RotateTransform( -angle, MatrixOrder.Prepend );

			// Since the text will be drawn by g.DrawString()
			// assuming the location is the TopCenter
			// (the Font is aligned using StringFormat to the
			// center so multi-line text is center justified),
			// shift the coordinate system so that we are
			// actually aligned per the caller specified position
			float xa, ya;
			if ( alignH == AlignH.Left )
				xa = sizeF.Width / 2.0F;
			else if ( alignH == AlignH.Right )
				xa = -sizeF.Width / 2.0F;
			else
				xa = 0.0F;
				
			if ( alignV == AlignV.Center )
				ya = -sizeF.Height / 2.0F;
			else if ( alignV == AlignV.Bottom )
				ya = -sizeF.Height;
			else
				ya = 0.0F;
			
			// Shift the coordinates to accomodate the alignment
			// parameters
			g.TranslateTransform( xa, ya, MatrixOrder.Prepend );

			// make a solid brush for rendering the font itself
			SolidBrush brush = new SolidBrush( this.fontColor );
			
			// make a center justified StringFormat alignment
			// for drawing the text
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = this.stringAlignment;
			if ( this.stringAlignment == StringAlignment.Far )
				g.TranslateTransform( sizeF.Width / 2.0F, 0F, MatrixOrder.Prepend );
			else if ( this.stringAlignment == StringAlignment.Near )
				g.TranslateTransform( -sizeF.Width / 2.0F, 0F, MatrixOrder.Prepend );
			
			// Create a rectangle representing the border around the
			// text.  Note that, while the text is drawn based on the
			// TopCenter position, the rectangle is drawn based on
			// the TopLeft position.  Therefore, move the rectangle
			// width/2 to the left to align it properly
			RectangleF rectF = new RectangleF( -sizeF.Width / 2.0F, 0.0F,
								sizeF.Width, sizeF.Height );

			// If the background is to be filled, fill it
			this.fill.Draw( g, rectF );
			
			// Draw the border around the text if required
			this.border.Draw( g, rectF );

			// Draw the actual text.  Note that the coordinate system
			// is set up such that 0,0 is at the location where the
			// CenterTop of the text needs to be.
			g.DrawString( text, this.font, brush, 0.0F, 0.0F, strFormat );

			// Restore the transform matrix back to original
			g.Transform = matrix;

		}

		/// <summary>
		/// Determines if the specified screen point lies within the bounding box of
		/// the text, taking into account alignment and rotation parameters.
		/// </summary>
		/// <param name="pt">The screen point, in pixel units</param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">A string value containing the text to be
		/// displayed.  This can be multiple lines, separated by newline ('\n')
		/// characters</param>
		/// <param name="x">The X location to display the text, in screen
		/// coordinates, relative to the horizontal (<see cref="AlignH"/>)
		/// alignment parameter <paramref name="alignH"/></param>
		/// <param name="y">The Y location to display the text, in screen
		/// coordinates, relative to the vertical (<see cref="AlignV"/>
		/// alignment parameter <paramref name="alignV"/></param>
		/// <param name="alignH">A horizontal alignment parameter specified
		/// using the <see cref="AlignH"/> enum type</param>
		/// <param name="alignV">A vertical alignment parameter specified
		/// using the <see cref="AlignV"/> enum type</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>true if the point lies within the bounding box, false otherwise</returns>
		public bool PointInBox( PointF pt, Graphics g, string text, float x,
			float y, AlignH alignH, AlignV alignV,
			double scaleFactor )
		{
			// make sure the font size is properly scaled
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			
			// Get the width and height of the text
			SizeF sizeF = g.MeasureString( text, this.font );

			// Create a bounding box rectangle for the text
			RectangleF rect = new RectangleF( new PointF( -sizeF.Width / 2.0F, 0.0F), sizeF );
			
			// Build a transform matrix that inverts that drawing transform
			// in this manner, the point is brought back to the box, rather
			// than vice-versa.  This allows the container check to be a simple
			// RectangleF.Contains, since the rectangle won't be rotated.
			Matrix matrix = new Matrix();
			
			// In this case, the bounding box is anchored to the
			// top-left of the text box.  Handle the alignment
			// as needed.
			float	xa, ya;
			if ( alignH == AlignH.Left )
				xa = sizeF.Width / 2.0F;
			else if ( alignH == AlignH.Right )
				xa = -sizeF.Width / 2.0F;
			else
				xa = 0.0F;
				
			if ( alignV == AlignV.Center )
				ya = -sizeF.Height / 2.0F;
			else if ( alignV == AlignV.Bottom )
				ya = -sizeF.Height;
			else
				ya = 0.0F;

			// Shift the coordinates to accomodate the alignment
			// parameters
			matrix.Translate( -xa, -ya, MatrixOrder.Prepend );

			// Rotate the coordinate system according to the 
			// specified angle of the FontSpec
			if ( angle != 0.0F )
				matrix.Rotate( angle, MatrixOrder.Prepend );

			// Move the coordinate system to local coordinates
			// of this text object (that is, at the specified
			// x,y location)
			matrix.Translate( -x, -y, MatrixOrder.Prepend );
			
			PointF[] pts = new PointF[1];
			pts[0] = pt;
			matrix.TransformPoints( pts );
			
			return rect.Contains( pts[0] );
		}
		
		/// <summary>
		/// Render the specified <paramref name="text"/> to the specifed
		/// <see cref="Graphics"/> device.  The text, border, and fill options
		/// will be rendered as required.  This special case method will show the
		/// specified text as a power of 10, using the <see cref="Default.SuperSize"/>
		/// and <see cref="Default.SuperShift"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">A string value containing the text to be
		/// displayed.  This can be multiple lines, separated by newline ('\n')
		/// characters</param>
		/// <param name="x">The X location to display the text, in screen
		/// coordinates, relative to the horizontal (<see cref="AlignH"/>)
		/// alignment parameter <paramref name="alignH"/></param>
		/// <param name="y">The Y location to display the text, in screen
		/// coordinates, relative to the vertical (<see cref="AlignV"/>
		/// alignment parameter <paramref name="alignV"/></param>
		/// <param name="alignH">A horizontal alignment parameter specified
		/// using the <see cref="AlignH"/> enum type</param>
		/// <param name="alignV">A vertical alignment parameter specified
		/// using the <see cref="AlignV"/> enum type</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawTenPower( Graphics g, string text, float x,
			float y, AlignH alignH, AlignV alignV,
			double scaleFactor )
		{
			// make sure the font size is properly scaled
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			float scaledSuperSize = this.scaledSize * Default.SuperSize;
			Remake( scaleFactor, this.Size * Default.SuperSize, ref scaledSuperSize,
				ref this.superScriptFont );
			
			// Get the width and height of the text
			SizeF size10 = g.MeasureString( "10", this.font );
			SizeF sizeText = g.MeasureString( text, this.superScriptFont );
			SizeF totSize = new SizeF( size10.Width + sizeText.Width,
									size10.Height + sizeText.Height * Default.SuperShift );
			float charWidth = g.MeasureString( "x", this.superScriptFont ).Width;

			// Save the old transform matrix for later restoration
			Matrix matrix = g.Transform;
			
			// Move the coordinate system to local coordinates
			// of this text object (that is, at the specified
			// x,y location)
			g.TranslateTransform( x, y, MatrixOrder.Prepend );
			
			// Rotate the coordinate system according to the 
			// specified angle of the FontSpec
			if ( angle != 0.0F )
				g.RotateTransform( -angle, MatrixOrder.Prepend );

			// Since the text will be drawn by g.DrawString()
			// assuming the location is the TopCenter
			// (the Font is aligned using StringFormat to the
			// center so multi-line text is center justified),
			// shift the coordinate system so that we are
			// actually aligned per the caller specified position
			float xa, ya;
			if ( alignH == AlignH.Left )
				xa = totSize.Width / 2.0F;
			else if ( alignH == AlignH.Right )
				xa = -totSize.Width / 2.0F;
			else
				xa = 0.0F;
				
			if ( alignV == AlignV.Center )
				ya = -totSize.Height / 2.0F;
			else if ( alignV == AlignV.Bottom )
				ya = -totSize.Height;
			else
				ya = 0.0F;
			
			// Shift the coordinates to accomodate the alignment
			// parameters
			g.TranslateTransform( xa, ya, MatrixOrder.Prepend );

			// make a solid brush for rendering the font itself
			SolidBrush brush = new SolidBrush( this.fontColor );
			
			// make a center justified StringFormat alignment
			// for drawing the text
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = this.stringAlignment;
			
			// Create a rectangle representing the border around the
			// text.  Note that, while the text is drawn based on the
			// TopCenter position, the rectangle is drawn based on
			// the TopLeft position.  Therefore, move the rectangle
			// width/2 to the left to align it properly
			RectangleF rectF = new RectangleF( -totSize.Width / 2.0F, 0.0F,
				totSize.Width, totSize.Height );

			// If the background is to be filled, fill it
			this.fill.Draw( g, rectF );
			
			// Draw the border around the text if required
			this.border.Draw( g, rectF );

			// Draw the actual text.  Note that the coordinate system
			// is set up such that 0,0 is at the location where the
			// CenterTop of the text needs to be.
			g.DrawString( "10", this.font, brush,
							( -totSize.Width + size10.Width ) / 2.0F,
							sizeText.Height * Default.SuperShift, strFormat );
			g.DrawString( text, this.superScriptFont, brush,
							( totSize.Width - sizeText.Width - charWidth ) / 2.0F,
							0.0F,
							strFormat );

			// Restore the transform matrix back to original
			g.Transform = matrix;

		}
	#endregion
	
	#region Sizing Methods
		/// <summary>
		/// Get the height of the scaled font
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled font height, in pixels</returns>
		public float GetHeight( double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			return this.font.Height;
		}
		/// <summary>
		/// Get the average character width of the scaled font.  The average width is
		/// based on the character 'x'
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
		/// <returns>The scaled font width, in pixels</returns>
		public float GetWidth( Graphics g, double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			return g.MeasureString( "x", this.font ).Width;
		}
		
		/// <summary>
		/// Get the total width of the specified text string
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">The text string for which the width is to be calculated
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled text width, in pixels</returns>
		public float GetWidth( Graphics g, string text, double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			return g.MeasureString( text, this.font ).Width;
		}
		/// <summary>
		/// Get a <see cref="SizeF"/> struct representing the width and height
		/// of the specified text string, based on the scaled font size
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">The text string for which the width is to be calculated
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled text dimensions, in pixels, in the form of
		/// a <see cref="SizeF"/> struct</returns>
		public SizeF MeasureString( Graphics g, string text, double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			return g.MeasureString( text, this.font );
		}
		
		/// <summary>
		/// Get a <see cref="SizeF"/> struct representing the width and height
		/// of the bounding box for the specified text string, based on the scaled font size.
		/// This routine differs from <see cref="MeasureString"/> in that it takes into
		/// account the rotation angle of the font, and gives the dimensions of the
		/// bounding box that encloses the text at the specified angle.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">The text string for which the width is to be calculated
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled text dimensions, in pixels, in the form of
		/// a <see cref="SizeF"/> struct</returns>
		public SizeF BoundingBox( Graphics g, string text, double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			SizeF s = g.MeasureString( text, this.font );
			
			float cs = (float) Math.Abs( Math.Cos( this.angle * Math.PI / 180.0 ) );
			float sn = (float) Math.Abs( Math.Sin( this.angle * Math.PI / 180.0 ) );
			
			SizeF s2 = new SizeF( s.Width * cs + s.Height * sn,
				s.Width * sn + s.Height * cs );
			
			return s2;
		}

		/// <summary>
		/// Get a <see cref="SizeF"/> struct representing the width and height
		/// of the bounding box for the specified text string, based on the scaled font size.
		/// This special case method will show the specified string as a power of 10,
		/// superscripted and downsized according to the
		/// <see cref="Default.SuperSize"/> and <see cref="Default.SuperShift"/>.
		/// This routine differs from <see cref="MeasureString"/> in that it takes into
		/// account the rotation angle of the font, and gives the dimensions of the
		/// bounding box that encloses the text at the specified angle.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">The text string for which the width is to be calculated
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled text dimensions, in pixels, in the form of
		/// a <see cref="SizeF"/> struct</returns>
		public SizeF BoundingBoxTenPower( Graphics g, string text, double scaleFactor )
		{
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			float scaledSuperSize = this.scaledSize * Default.SuperSize;
			Remake( scaleFactor, this.Size * Default.SuperSize, ref scaledSuperSize,
				ref this.superScriptFont );

			// Get the width and height of the text
			SizeF size10 = g.MeasureString( "10", this.font );
			SizeF sizeText = g.MeasureString( text, this.superScriptFont );
			SizeF totSize = new SizeF( size10.Width + sizeText.Width,
				size10.Height + sizeText.Height * Default.SuperShift );

			
			float cs = (float) Math.Abs( Math.Cos( this.angle * Math.PI / 180.0 ) );
			float sn = (float) Math.Abs( Math.Sin( this.angle * Math.PI / 180.0 ) );
			
			SizeF s2 = new SizeF( totSize.Width * cs + totSize.Height * sn,
				totSize.Width * sn + totSize.Height * cs );
			
			return s2;
		}
	#endregion
	
	}
}
