using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ZedGraph
{
	/// <summary>
	/// A class that encapsulates color-fill properties for an object.  The <see cref="Fill"/> class
	/// is used in <see cref="GraphPane.PaneRect"/>, <see cref="GraphPane.AxisRect"/>, <see cref="Legend"/>,
	/// <see cref="Bar"/>, and <see cref="Line"/> objects.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2004-10-13 04:52:53 $ </version>
	public class Fill
	{
	#region Fields

		/// <summary>
		/// Private field that stores the fill color.  Use the public
		/// property <see cref="Color"/> to access this value.  This property is
		/// only applicable if the <see cref="Type"/> is not <see cref="ZedGraph.FillType.None"/>.
		/// </summary>
		private Color	color;
		/// <summary>
		/// Private field that stores the custom fill brush.  Use the public
		/// property <see cref="Brush"/> to access this value.  This property is
		/// only applicable if the 
		/// <see cref="Type"/> property is set to <see cref="ZedGraph.FillType.Brush"/>.
		/// </summary>
		private Brush	brush;
		/// <summary>
		/// Private field that determines the type of color fill.  Use the public
		/// property <see cref="Type"/> to access this value.  The fill color
		/// is determined by the property <see cref="Color"/> or
		/// <see cref="Brush"/>.
		/// </summary>
		private FillType	type;
		/// <summary>
		/// Private field that determines if the brush will be scaled to the bounding box
		/// of the filled object.  If this value is false, then the brush will only be aligned
		/// with the filled object based on the <see cref="AlignH"/> and <see cref="AlignV"/>
		/// properties.
		/// </summary>
		private bool		isScaled;
		/// <summary>
		/// Private field that determines how the brush will be aligned with the filled object
		/// in the horizontal direction.  This value is a <see cref="ZedGraph.AlignH"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// properties.
		/// </summary>
		/// <seealso cref="AlignH"/>
		/// <seealso cref="AlignV"/>
		private AlignH		alignH;
		/// <summary>
		/// Private field that determines how the brush will be aligned with the filled object
		/// in the vertical direction.  This value is a <see cref="ZedGraph.AlignV"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// properties.
		/// </summary>
		/// <seealso cref="AlignH"/>
		/// <seealso cref="AlignV"/>
		private AlignV		alignV;

	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Fill"/> class.
		/// </summary>
		public struct Default
		{
			// Default Fill properties
			/// <summary>
			/// The default scaling mode for <see cref="Brush"/> fills.
			/// This is the default value for the <see cref="Fill.IsScaled"/> property.
			/// </summary>
			public static bool IsScaled = true;
			/// <summary>
			/// The default horizontal alignment for <see cref="Brush"/> fills.
			/// This is the default value for the <see cref="Fill.AlignH"/> property.
			/// </summary>
			public static AlignH AlignH = AlignH.Center;
			/// <summary>
			/// The default vertical alignment for <see cref="Brush"/> fills.
			/// This is the default value for the <see cref="Fill.AlignV"/> property.
			/// </summary>
			public static AlignV AlignV = AlignV.Center;
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// The default constructor.  Initialized to no fill.
		/// </summary>
		public Fill()
		{
			color = Color.Red;
			brush = null;
			type = FillType.None;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that specifies the color, brush, and type for this fill.
		/// </summary>
		/// <param name="color">The color of the fill for solid fills</param>
		/// <param name="brush">A custom brush for fills.  Can be a <see cref="SolidBrush"/>,
		/// <see cref="LinearGradientBrush"/>, or <see cref="TextureBrush"/>.</param>
		/// <param name="type">The <see cref="FillType"/> for this fill.</param>
		public Fill( Color color, Brush brush, FillType type )
		{
			this.color = color;
			this.brush = brush;
			this.type = type;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a solid color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Solid"/>, and setting <see cref="Color"/> to the
		/// specified color value.
		/// </summary>
		/// <param name="color">The color of the solid fill</param>
		public Fill( Color color )
		{
			this.color = color;
			this.brush = null;
			this.type = FillType.Solid;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors and angle.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color color1, Color color2, float angle )
		{
			this.color = color2;
			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				color1, color2, angle );
				this.type = FillType.Brush;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		public Fill( Color color1, Color color2 )
		{
			this.color = color2;
			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				color1, color2, 0F );
				this.type = FillType.Brush;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a texture fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> and using the specified image.
		/// </summary>
		/// <param name="image">The <see cref="Image"/> to use for filling</param>
		/// <param name="wrapMode">The <see cref="WrapMode"/> class that controls the image wrapping properties</param>
		public Fill( Image image, WrapMode wrapMode )
		{
			this.color = Color.White;
			this.brush = new TextureBrush( image, wrapMode );
			this.type = FillType.Brush;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="Brush"/> fill, using a user-supplied, custom
		/// <see cref="Brush"/>.  The brush will be scaled to fit the destination screen object
		/// unless you manually change <see cref="IsScaled"/> to false;
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to use for fancy fills.  Typically, this would
		/// be a <see cref="LinearGradientBrush"/> or a <see cref="TextureBrush"/> class</param>
		public Fill( Brush brush )
		{
			this.color = Color.White;
			this.brush = (Brush) brush.Clone();
			this.type = FillType.Brush;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="Brush"/> fill, using a user-supplied, custom
		/// <see cref="Brush"/>.  The brush will be scaled to fit the destination screen object
		/// according to the <see paramref="isScaled"/> parameter.
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to use for fancy fills.  Typically, this would
		/// be a <see cref="LinearGradientBrush"/> or a <see cref="TextureBrush"/> class</param>
		/// <param name="isScaled">Determines if the brush will be scaled to fit the bounding box
		/// of the destination object.  true to scale it, false to leave it unscaled</param>
		public Fill( Brush brush, bool isScaled )
		{
			this.isScaled = isScaled;
			this.color = Color.White;
			this.brush = (Brush) brush.Clone();
			this.type = FillType.Brush;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="Brush"/> fill, using a user-supplied, custom
		/// <see cref="Brush"/>.  This constructor will make the brush unscaled (see <see cref="IsScaled"/>),
		/// but it provides <see paramref="alignH"/> and <see paramref="alignV"/> parameters to control
		/// alignment of the brush with respect to the filled object.
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to use for fancy fills.  Typically, this would
		/// be a <see cref="LinearGradientBrush"/> or a <see cref="TextureBrush"/> class</param>
		/// <param name="alignH">Controls the horizontal alignment of the brush within the filled object
		/// (see <see cref="AlignH"/></param>
		/// <param name="alignV">Controls the vertical alignment of the brush within the filled object
		/// (see <see cref="AlignV"/></param>
		public Fill( Brush brush, AlignH alignH, AlignV alignV )
		{
			this.alignH = alignH;
			this.alignV = alignV;
			this.isScaled = false;
			this.color = Color.White;
			this.brush = (Brush) brush.Clone();
			this.type = FillType.Brush;
		}
		
		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Fill object from which to copy</param>
		public Fill( Fill rhs )
		{
			color = rhs.color;
			if ( rhs.brush != null )
				brush = (Brush) rhs.Brush.Clone();
			else
				brush = null;
			type = rhs.type;
			alignH = rhs.AlignH;
			alignV = rhs.AlignV;
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Fill class</returns>
		public object Clone()
		{ 
			return new Fill( this ); 
		}
	#endregion

	#region Properties

		/// <summary>
		/// The fill color.  This property is used as a single color to make a solid fill
		/// (<see cref="Type"/> is <see cref="FillType.Solid"/>), or it can be used in 
		/// combination with <see cref="System.Drawing.Color.White"/> to make a
		/// <see cref="LinearGradientBrush"/>
		/// when <see cref="Type"/> is <see cref="FillType.Brush"/> and <see cref="Brush"/>
		/// is null.
		/// </summary>
		/// <seealso cref="Type"/>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		/// <summary>
		/// The custom fill brush.  This can be a <see cref="SolidBrush"/>, a
		/// <see cref="LinearGradientBrush"/>, or a <see cref="TextureBrush"/>.  This property is
		/// only applicable if the <see cref="Type"/> property is set
		/// to <see cref="FillType.Brush"/>.
		/// </summary>
		public Brush Brush
		{
			get { return brush; }
			set { brush = value; }
		}
		/// <summary>
		/// Determines the type of fill, which can be either solid
		/// color (<see cref="ZedGraph.FillType.Solid"/>) or a custom brush
		/// (<see cref="ZedGraph.FillType.Brush"/>).  See <see cref="Type"/> for
		/// more information.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill.Color"/>
		public FillType Type
		{
			get { return type; }
			set { type = value; }
		}
		/// <summary>
		/// This property determines the type of color fill. 
		/// Returns true if the <see cref="Type"/> property is either
		/// <see cref="FillType.Solid"/> or
		/// <see cref="FillType.Brush"/>.  If set to true, this property
		/// will automatically set the <see cref="Type"/> to
		/// <see cref="FillType.Brush"/>.  If set to false, this property
		/// will automatically set the <see cref="Type"/> to
		/// <see cref="FillType.None"/>.  In order to get a regular
		/// solid-color fill, you have to manually set <see cref="Type"/>
		/// to <see cref="FillType.Solid"/>.
		/// </summary>
		/// <seealso cref="Color"/>
		/// <seealso cref="Brush"/>
		/// <seealso cref="Type"/>
		public bool IsVisible
		{
			get { return type != FillType.None; }
			set { type = value ? FillType.Brush : FillType.None; }
		}

		/// <summary>
		/// Determines if the brush will be scaled to the bounding box
		/// of the filled object.  If this value is false, then the brush will only be aligned
		/// with the filled object based on the <see cref="AlignH"/> and <see cref="AlignV"/>
		/// properties.
		/// </summary>
		public bool IsScaled
		{
			get { return isScaled; }
			set { isScaled = value; }
		}
		
		/// <summary>
		/// Determines how the brush will be aligned with the filled object
		/// in the horizontal direction.  This value is a <see cref="ZedGraph.AlignH"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// properties.
		/// </summary>
		/// <seealso cref="AlignV"/>
		public AlignH AlignH
		{
			get { return alignH; }
			set { alignH = value; }
		}
		
		/// <summary>
		/// Determines how the brush will be aligned with the filled object
		/// in the vertical direction.  This value is a <see cref="ZedGraph.AlignV"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// properties.
		/// </summary>
		/// <seealso cref="AlignH"/>
		public AlignV AlignV
		{
			get { return alignV; }
			set { alignV = value; }
		}

	#endregion

	#region Methods

		/// <summary>
		/// Create a fill brush using current properties.  This method will construct a brush based on the
		/// settings of <see cref="ZedGraph.Fill.Type"/>, <see cref="ZedGraph.Fill.Color"/>
		/// and <see cref="ZedGraph.Fill.Brush"/>.  If
		/// <see cref="ZedGraph.Fill.Type"/> is set to <see cref="ZedGraph.FillType.Brush"/> and
		/// <see cref="ZedGraph.Fill.Brush"/>
		/// is null, then a <see cref="LinearGradientBrush"/> will be created between the colors of
		/// <see cref="System.Drawing.Color.White"/> and <see cref="ZedGraph.Fill.Color"/>.
		/// </summary>
		/// <param name="rect">A rectangle that bounds the object to be filled.  This determines
		/// the start and end of the gradient fill.</param>
		/// <returns>A <see cref="System.Drawing.Brush"/> class representing the fill brush</returns>
		public Brush MakeBrush( RectangleF rect )
		{
			// get a brush
			if ( this.IsVisible&& ( !this.color.IsEmpty || this.brush != null ) )
			{
				//Brush	brush;
				if ( this.type == FillType.Brush )
				{
					if ( rect.Height < 1.0F )
						rect.Height = 1.0F;
					if ( rect.Width < 1.0F )
						rect.Width = 1.0F;
						
					if ( this.brush != null )
					{
						if ( this.brush is SolidBrush )
						{
							return (Brush) this.Brush.Clone();
						}
						else if ( this.brush is LinearGradientBrush )
						{
							LinearGradientBrush linBrush = (LinearGradientBrush) this.brush.Clone();
							
							if ( this.isScaled )
							{
								linBrush.ScaleTransform( rect.Width / linBrush.Rectangle.Width,
									rect.Height / linBrush.Rectangle.Height, MatrixOrder.Append );
								linBrush.TranslateTransform( rect.Left - linBrush.Rectangle.Left,
									rect.Top - linBrush.Rectangle.Top, MatrixOrder.Append );
							}
							else
							{
								float	dx = 0,
										dy = 0;
								switch ( this.alignH )
								{
								case AlignH.Left:
									dx = rect.Left - linBrush.Rectangle.Left;
									break;
								case AlignH.Center:
									dx = ( rect.Left + rect.Width / 2.0F ) - linBrush.Rectangle.Left;
									break;
								case AlignH.Right:
									dx = ( rect.Left + rect.Width ) - linBrush.Rectangle.Left;
									break;
								}
								
								switch ( this.alignV )
								{
								case AlignV.Top:
									dy = rect.Top - linBrush.Rectangle.Top;
									break;
								case AlignV.Center:
									dy = ( rect.Top + rect.Height / 2.0F ) - linBrush.Rectangle.Top;
									break;
								case AlignV.Bottom:
									dy = ( rect.Top + rect.Height) - linBrush.Rectangle.Top;
									break;
								}

								linBrush.TranslateTransform( dx, dy, MatrixOrder.Append );
							}
							
							return linBrush;
							
						} // LinearGradientBrush
						else // TextureBrush
						{
							TextureBrush texBrush = (TextureBrush) this.brush.Clone();
							
							if ( this.isScaled )
							{
								texBrush.ScaleTransform( rect.Width / texBrush.Image.Width,
									rect.Height / texBrush.Image.Height, MatrixOrder.Append );
								texBrush.TranslateTransform( rect.Left, rect.Top, MatrixOrder.Append );
							}
							else
							{
								float	dx = 0,
									dy = 0;
								switch ( this.alignH )
								{
								case AlignH.Left:
									dx = rect.Left;
									break;
								case AlignH.Center:
									dx = ( rect.Left + rect.Width / 2.0F );
									break;
								case AlignH.Right:
									dx = ( rect.Left + rect.Width );
									break;
								}
								
								switch ( this.alignV )
								{
								case AlignV.Top:
									dy = rect.Top;
									break;
								case AlignV.Center:
									dy = ( rect.Top + rect.Height / 2.0F );
									break;
								case AlignV.Bottom:
									dy = ( rect.Top + rect.Height);
									break;
								}

								texBrush.TranslateTransform( dx, dy, MatrixOrder.Append );
							}
							
							return texBrush;
						}
					}
					else
						// If they didn't provide a brush, make one using the fillcolor gradient to white
						return new LinearGradientBrush( rect, Color.White, this.color, 0F );
				}
				else
					return new SolidBrush( this.color );
			}

			// Always return a suitable default
			return new SolidBrush( Color.White );
		}

		/// <summary>
		/// Fill the background of the <see cref="RectangleF"/> area, using the
		/// fill type from this <see cref="Fill"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">The <see cref="RectangleF"/> struct specifying the area
		/// to be filled</param>
		public void Draw( Graphics g, RectangleF rect )
		{
			if ( this.IsVisible )
			{
				Brush brush = this.MakeBrush( rect );
				g.FillRectangle( brush, rect );
				brush.Dispose();
			}
		}
	#endregion
	}
}
