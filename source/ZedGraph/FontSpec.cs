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
	public class FontSpec
	{
		private Color fontColor;
		private string family;
		private bool isBold;
		private bool isItalic;
		private bool isUnderline;
		private bool isFilled;
		private Color fillColor;
		private bool isFramed;
		private Color frameColor;
		private float frameWidth;

		private float angle;
		private float size;
		
		private Font font;
		private float scaledSize;
		
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
			
			Remake( 1.0 );
		}
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
		/// A text string with the font family name, e.g., "Arial"
		/// </summary>
		public string Family
		{
			get { return family; }
			set
			{ 
				if ( value != family )
				{
					family = value;
					Remake( (double) scaledSize / size );
				}
			}
		}
		/// <summary>
		/// true for a bold typeface, false otherwise
		/// </summary>
		public bool IsBold
		{
			get { return isBold; }
			set
			{ 
				if ( value != isBold )
				{
					isBold = value;
					Remake( (double) scaledSize / size );
				}
			}
		}
		/// <summary>
		/// true for an italic typeface, false otherwise
		/// </summary>
		public bool IsItalic
		{
			get { return isItalic; }
			set
			{ 
				if ( value != isItalic )
				{
					isItalic = value;
					Remake( (double) scaledSize / size );
				}
			}
		}
		/// <summary>
		/// true for an underlined font, false otherwise
		/// </summary>
		public bool IsUnderline
		{
			get { return isUnderline; }
			set
			{ 
				if ( value != isUnderline )
				{
					isUnderline = value;
					Remake( (double) scaledSize / size );
				}
			}
		}
		/// <summary>
		/// The angle of the font, measured in anti-clockwise degrees.
		/// Negative values are permitted.
		/// </summary>
		public float Angle
		{
			get { return angle; }
			set { angle = value; }
		}
		/// <summary>
		/// The size of the font in points (1/72 inch).
		/// </summary>
		public float Size
		{
			get { return size; }
			set
			{ 
				if ( value != size )
				{
					Remake( (double) scaledSize / size * value );
					size = value;
				}
			}
		}
		/// <summary>
		/// The pen width used for drawing the frame around the text
		/// </summary>
		/// <value>Pen width in pixel units</value>
		public float FrameWidth
		{
			get { return frameWidth; }
			set { frameWidth = value; }
		}
		/// <summary>
		/// Set to true to display a frame around the text using the
		/// <see cref="FrameColor"/> color and <see cref="FrameWidth"/>
		/// pen width, or false for no frame
		/// </summary>
		public bool IsFramed
		{
			get { return isFramed; }
			set { isFramed = value; }
		}
		/// <summary>
		/// Set to true to fill the area behind the text using the
		/// <see cref="FillColor"/> color, or false for a transparent
		/// background
		/// </summary>
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
		private void Remake( double scaleFactor )
		{
			float newSize = (float) ( this.size * scaleFactor );
			
			// Regenerate the font only if the size has changed significantly
			if ( Math.Abs( newSize - this.scaledSize ) > 0.1 )
			{
				FontStyle style = FontStyle.Regular;
				style = ( this.isBold ? FontStyle.Bold : style ) |
							( this.isItalic ? FontStyle.Italic : style ) |
							 ( this.isUnderline ? FontStyle.Underline : style );
				
				this.scaledSize = size * (float) scaleFactor;
				this.font = new Font( this.family, this.scaledSize, style );
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
		public Font GetFont( double scaleFactor )
		{
			Remake( scaleFactor );
			return this.font;
		}		

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
			Remake( scaleFactor );
			
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
			Remake( scaleFactor );
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
			Remake( scaleFactor );
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
			Remake( scaleFactor );
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
			Remake( scaleFactor );
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
			Remake( scaleFactor );
			SizeF s = g.MeasureString( text, this.font );
			
			float cs = (float) Math.Abs( Math.Cos( this.angle * Math.PI / 180.0 ) );
			float sn = (float) Math.Abs( Math.Sin( this.angle * Math.PI / 180.0 ) );
			
			SizeF s2 = new SizeF( s.Width * cs + s.Height * sn,
									s.Width * sn + s.Height * cs );
			
			return s2;
		}
	}
}
