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
	/// attributes, colors, frame and fill modes, font size, and angle information.
	/// This class can render text with a variety of alignment options using the
	/// <see cref="FontAlignH"/> and <see cref="FontAlignV"/> parameters in the
	/// <see cref="Draw"/> method.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.4 $ $Date: 2004-08-23 20:27:45 $ </version>
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
		/// Private field that determines whether this <see cref="FontSpec"/> is
		/// drawn with filled background.
		/// Use the public property <see cref="IsFilled"/> to access this value.
		/// </summary>
		/// <value>A boolean value, true for a color-filled background,
		/// false for transparent background</value>
		private bool isFilled;
		/// <summary>
		/// Private field that determines the background fill color for this
		/// <see cref="FontSpec"/>.  This color is only used if
		/// <see cref="isFilled"/> is true.
		/// Use the public property <see cref="FillColor"/> to access this value.
		/// </summary>
		/// <value>A <see cref="System.Drawing.Color"/> value</value>
		private Color fillColor;
		/// <summary>
		/// Private field that determines whether this <see cref="FontSpec"/> is
		/// drawn with a frame around it.
		/// Use the public property <see cref="IsFramed"/> to access this value.
		/// </summary>
		/// <value>A boolean value, true for a frame,
		/// false for no frame</value>
		private bool isFramed;
		/// <summary>
		/// Private field that determines the frame color for this
		/// <see cref="FontSpec"/>.  This color is only used if
		/// <see cref="isFramed"/> is true.
		/// Use the public property <see cref="FrameColor"/> to access this value.
		/// </summary>
		/// <value>A <see cref="System.Drawing.Color"/> value</value>
		private Color frameColor;
		/// <summary>
		/// Private field that determines the width of the frame for this
		/// <see cref="FontSpec"/>.  This width is only used if
		/// <see cref="isFramed"/> is true.
		/// Use the public property <see cref="FrameWidth"/> to access this value.
		/// </summary>
		/// <value>The width of the frame, in pixel units</value>
		private float frameWidth;

		/// <summary>
		/// Private field that determines the angle at which this
		/// <see cref="FontSpec"/> object is drawn.  Use the public property
		/// <see cref="Angle"/> to access this value.
		/// </summary>
		/// <value>The angle of the font, measured in anti-clockwise degrees from
		/// horizontal.  Negative values are permitted.</value>
		private float angle;
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
		/// based on the value of <see cref="Def.FontSpc.SuperSize"/>.  This
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
	
	#region Properties
		/// <summary>
		/// The color of the font characters for this <see cref="FontSpec"/>.
		/// Note that the frame and background
		/// colors are set using the <see cref="FrameColor"/> and
		/// <see cref="FillColor"/> properties, respectively.
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
		/// The pen width used for drawing the frame around this
		/// <see cref="FontSpec"/>.  This width is only used if
		/// <see cref="IsFramed"/> is true.
		/// </summary>
		/// <value>The width of the frame, in pixel units</value>
		public float FrameWidth
		{
			get { return frameWidth; }
			set { frameWidth = value; }
		}
		/// <summary>
		/// Determines whether or not to display a frame around the text using the
		/// <see cref="FrameColor"/> color and <see cref="FrameWidth"/>
		/// pen width
		/// </summary>
		/// <value>A boolean value, true for a frame,
		/// false for no frame</value>
		public bool IsFramed
		{
			get { return isFramed; }
			set { isFramed = value; }
		}
		/// <summary>
		/// Determines whether or not the area behind the text of this
		/// <see cref="FontSpec"/> is filled using the
		/// <see cref="FillColor"/> color.
		/// </summary>
		/// <value>A boolean value, true for a color-filled background,
		/// false for transparent background</value>
		public bool IsFilled
		{
			get { return isFilled; }
			set { isFilled = value; }
		}
		/// <summary>
		/// Sets or gets the color of the frame around the text.  This
		/// frame is turned on or off using the <see cref="IsFramed"/>
		/// property and the pen width is specified with the
		/// <see cref="FrameWidth"/> property
		/// </summary>
		/// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
		public Color FrameColor
		{
			get { return frameColor; }
			set { frameColor = value; }
		}
		/// <summary>
		/// Sets or gets the color of the background behind the text.
		/// This background fill option is turned on or off using the
		/// <see cref="IsFilled"/> property.
		/// </summary>
		/// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
		public Color FillColor
		{
			get { return fillColor; }
			set { fillColor = value; }
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// Construct a <see cref="FontSpec"/> object with the given properties.  All other properties
		/// are defaulted according to the values specified in the <see cref="Def"/>
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

			this.isFilled = true;
			this.fillColor = Color.White;
			this.isFramed = true;
			this.frameColor = Color.Black;
			this.frameWidth = 1.0F;
			
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
			isFilled = rhs.IsFilled;
			fillColor = rhs.FillColor;
			isFramed = rhs.IsFramed;
			frameColor = rhs.FrameColor;
			frameWidth = rhs.FrameWidth;

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
		/// <see cref="Graphics"/> device.  The text, frame, and fill options
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
		/// coordinates, relative to the horizontal (<see cref="FontAlignH"/>)
		/// alignment parameter <paramref name="alignH"/></param>
		/// <param name="y">The Y location to display the text, in screen
		/// coordinates, relative to the vertical (<see cref="FontAlignV"/>
		/// alignment parameter <paramref name="alignV"/></param>
		/// <param name="alignH">A horizontal alignment parameter specified
		/// using the <see cref="FontAlignH"/> enum type</param>
		/// <param name="alignV">A vertical alignment parameter specified
		/// using the <see cref="FontAlignV"/> enum type</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, string text, float x,
			float y, FontAlignH alignH, FontAlignV alignV,
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
			g.TranslateTransform( x, y );
			
			// Since the text will be drawn by g.DrawString()
			// assuming the location is the TopCenter
			// (the Font is aligned using StringFormat to the
			// center so multi-line text is center justified),
			// shift the coordinate system so that we are
			// actually aligned per the caller specified position
			if ( alignH == FontAlignH.Left )
				x = sizeF.Width / 2.0F;
			else if ( alignH == FontAlignH.Right )
				x = -sizeF.Width / 2.0F;
			else
				x = 0.0F;
				
			if ( alignV == FontAlignV.Center )
				y = -sizeF.Height / 2.0F;
			else if ( alignV == FontAlignV.Bottom )
				y = -sizeF.Height;
			else
				y = 0.0F;
			
			// Rotate the coordinate system according to the 
			// specified angle of the FontSpec
			if ( angle != 0.0F )
				g.RotateTransform( -angle );

			// Shift the coordinates to accomodate the alignment
			// parameters
			g.TranslateTransform( x, y );

			// make a solid brush for rendering the font itself
			SolidBrush brush = new SolidBrush( this.fontColor );
			
			// make a center justified StringFormat alignment
			// for drawing the text
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			
			// Create a rectangle representing the frame around the
			// text.  Note that, while the text is drawn based on the
			// TopCenter position, the rectangle is drawn based on
			// the TopLeft position.  Therefore, move the rectangle
			// width/2 to the left to align it properly
			RectangleF rectF = new RectangleF( -sizeF.Width / 2.0F, 0.0F,
								sizeF.Width, sizeF.Height );

			// If the background is to be filled, fill it
			if ( isFilled )
			{
				SolidBrush fillBrush = new SolidBrush( this.fillColor );
				g.FillRectangle( fillBrush, rectF );
			}
			
			// Draw the frame around the text if required
			if ( isFramed )
			{
				Pen pen = new Pen( this.frameColor, this.frameWidth );
				g.DrawRectangle( pen, Rectangle.Round( rectF ) );
			}

			// Draw the actual text.  Note that the coordinate system
			// is set up such that 0,0 is at the location where the
			// CenterTop of the text needs to be.
			g.DrawString( text, this.font, brush, 0.0F, 0.0F, strFormat );

			// Restore the transform matrix back to original
			g.Transform = matrix;

		}

		/// <summary>
		/// Render the specified <paramref name="text"/> to the specifed
		/// <see cref="Graphics"/> device.  The text, frame, and fill options
		/// will be rendered as required.  This special case method will show the
		/// specified text as a power of 10, using the <see cref="Def.FontSpc.SuperSize"/>
		/// and <see cref="Def.FontSpc.SuperShift"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="text">A string value containing the text to be
		/// displayed.  This can be multiple lines, separated by newline ('\n')
		/// characters</param>
		/// <param name="x">The X location to display the text, in screen
		/// coordinates, relative to the horizontal (<see cref="FontAlignH"/>)
		/// alignment parameter <paramref name="alignH"/></param>
		/// <param name="y">The Y location to display the text, in screen
		/// coordinates, relative to the vertical (<see cref="FontAlignV"/>
		/// alignment parameter <paramref name="alignV"/></param>
		/// <param name="alignH">A horizontal alignment parameter specified
		/// using the <see cref="FontAlignH"/> enum type</param>
		/// <param name="alignV">A vertical alignment parameter specified
		/// using the <see cref="FontAlignV"/> enum type</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawTenPower( Graphics g, string text, float x,
			float y, FontAlignH alignH, FontAlignV alignV,
			double scaleFactor )
		{
			// make sure the font size is properly scaled
			Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
			float scaledSuperSize = this.scaledSize * Def.FontSpc.SuperSize;
			Remake( scaleFactor, this.Size * Def.FontSpc.SuperSize, ref scaledSuperSize,
				ref this.superScriptFont );
			
			// Get the width and height of the text
			SizeF size10 = g.MeasureString( "10", this.font );
			SizeF sizeText = g.MeasureString( text, this.superScriptFont );
			SizeF totSize = new SizeF( size10.Width + sizeText.Width,
									size10.Height + sizeText.Height * Def.FontSpc.SuperShift );
			float charWidth = g.MeasureString( "x", this.superScriptFont ).Width;

			// Save the old transform matrix for later restoration
			Matrix matrix = g.Transform;
			
			// Move the coordinate system to local coordinates
			// of this text object (that is, at the specified
			// x,y location)
			g.TranslateTransform( x, y );
			
			// Since the text will be drawn by g.DrawString()
			// assuming the location is the TopCenter
			// (the Font is aligned using StringFormat to the
			// center so multi-line text is center justified),
			// shift the coordinate system so that we are
			// actually aligned per the caller specified position
			if ( alignH == FontAlignH.Left )
				x = totSize.Width / 2.0F;
			else if ( alignH == FontAlignH.Right )
				x = -totSize.Width / 2.0F;
			else
				x = 0.0F;
				
			if ( alignV == FontAlignV.Center )
				y = -totSize.Height / 2.0F;
			else if ( alignV == FontAlignV.Bottom )
				y = -totSize.Height;
			else
				y = 0.0F;
			
			// Rotate the coordinate system according to the 
			// specified angle of the FontSpec
			if ( angle != 0.0F )
				g.RotateTransform( -angle );

			// Shift the coordinates to accomodate the alignment
			// parameters
			g.TranslateTransform( x, y );

			// make a solid brush for rendering the font itself
			SolidBrush brush = new SolidBrush( this.fontColor );
			
			// make a center justified StringFormat alignment
			// for drawing the text
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			
			// Create a rectangle representing the frame around the
			// text.  Note that, while the text is drawn based on the
			// TopCenter position, the rectangle is drawn based on
			// the TopLeft position.  Therefore, move the rectangle
			// width/2 to the left to align it properly
			RectangleF rectF = new RectangleF( -totSize.Width / 2.0F, 0.0F,
				totSize.Width, totSize.Height );

			// If the background is to be filled, fill it
			if ( isFilled )
			{
				SolidBrush fillBrush = new SolidBrush( this.fillColor );
				g.FillRectangle( fillBrush, rectF );
			}
			
			// Draw the frame around the text if required
			if ( isFramed )
			{
				Pen pen = new Pen( this.frameColor, this.frameWidth );
				g.DrawRectangle( pen, Rectangle.Round( rectF ) );
			}

			// Draw the actual text.  Note that the coordinate system
			// is set up such that 0,0 is at the location where the
			// CenterTop of the text needs to be.
			g.DrawString( "10", this.font, brush,
							( -totSize.Width + size10.Width ) / 2.0F,
							sizeText.Height * Def.FontSpc.SuperShift, strFormat );
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
		/// <see cref="Def.FontSpc.SuperSize"/> and <see cref="Def.FontSpc.SuperShift"/>.
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
			float scaledSuperSize = this.scaledSize * Def.FontSpc.SuperSize;
			Remake( scaleFactor, this.Size * Def.FontSpc.SuperSize, ref scaledSuperSize,
				ref this.superScriptFont );

			// Get the width and height of the text
			SizeF size10 = g.MeasureString( "10", this.font );
			SizeF sizeText = g.MeasureString( text, this.superScriptFont );
			SizeF totSize = new SizeF( size10.Width + sizeText.Width,
				size10.Height + sizeText.Height * Def.FontSpc.SuperShift );

			
			float cs = (float) Math.Abs( Math.Cos( this.angle * Math.PI / 180.0 ) );
			float sn = (float) Math.Abs( Math.Sin( this.angle * Math.PI / 180.0 ) );
			
			SizeF s2 = new SizeF( totSize.Width * cs + totSize.Height * sn,
				totSize.Width * sn + totSize.Height * cs );
			
			return s2;
		}
	#endregion
	
	}
}
