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
	/// <version> $Revision: 3.6 $ $Date: 2004-11-17 03:35:39 $ </version>
	public class Fill
	{
	#region Fields

		/// <summary>
		/// Private field that stores the fill color.  Use the public
		/// property <see cref="Color"/> to access this value.  This property is
		/// only applicable if the <see cref="Type"/> is not <see cref="ZedGraph.FillType.None"/>.
		/// </summary>
		private Color		color;
		/// <summary>
		/// Private field that stores the custom fill brush.  Use the public
		/// property <see cref="Brush"/> to access this value.  This property is
		/// only applicable if the 
		/// <see cref="Type"/> property is set to <see cref="ZedGraph.FillType.Brush"/>.
		/// </summary>
		private Brush		brush;
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

		private double	rangeMin;
		private double	rangeMax;
		private Bitmap	gradientBM;

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
		/// Generic initializer to default values
		/// </summary>
		private void Init()
		{
			color = Color.White;
			brush = null;
			type = FillType.None;
			this.isScaled = Default.IsScaled;
			this.alignH = Default.AlignH;
			this.alignV = Default.AlignV;
			this.rangeMin = 0.0;
			this.rangeMax = 1.0;
			gradientBM = null;
		}

		/// <summary>
		/// The default constructor.  Initialized to no fill.
		/// </summary>
		public Fill()
		{
			Init();
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
			Init();
			this.color = color;
			this.brush = brush;
			this.type = type;
		}
		
		/// <summary>
		/// Constructor that creates a solid color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Solid"/>, and setting <see cref="Color"/> to the
		/// specified color value.
		/// </summary>
		/// <param name="color">The color of the solid fill</param>
		public Fill( Color color )
		{
			Init();
			this.color = color;
			this.type = FillType.Solid;
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
			Init();
			this.color = color2;
			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				color1, color2, angle );
				this.type = FillType.Brush;
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		public Fill( Color color1, Color color2 )
		{
			Init();
			this.color = color2;
			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				color1, color2, 0F );
			this.type = FillType.Brush;
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of three colors.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		/// <param name="color3">The third color for the gradient fill</param>
		public Fill( Color color1, Color color2, Color color3 ) :
			this( color1, color2, color3, 0.0f )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of three colors
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		/// <param name="color3">The third color for the gradient fill</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color color1, Color color2, Color color3, float angle )
		{
			Init();
			ColorBlend blend = new ColorBlend( 3 );
			blend.Colors[0] = color1;
			blend.Colors[1] = color2;
			blend.Colors[2] = color3;
			blend.Positions[0] = 0.0f;
			blend.Positions[1] = 0.5f;
			blend.Positions[2] = 1.0f;
			
			this.color = color3;
			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				color1, color2, angle );
			((LinearGradientBrush)this.brush).InterpolationColors = blend;
			this.type = FillType.Brush;
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on a <see cref="ColorBlend"/> object.  The gradient
		/// angle is defaulted to zero.
		/// </summary>
		/// <param name="blend">The <see cref="ColorBlend"/> object that defines the colors
		/// and positions along the gradient.</param>
		public Fill( ColorBlend blend ) :
			this( blend, 0.0F )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on a <see cref="ColorBlend"/> object, drawn at the
		/// specified angle (degrees).
		/// </summary>
		/// <param name="blend">The <see cref="ColorBlend"/> object that defines the colors
		/// and positions along the gradient.</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( ColorBlend blend, float angle )
		{
			Init();
			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				Color.Red, Color.White, angle );
			((LinearGradientBrush)this.brush).InterpolationColors = blend;
			this.type = FillType.Brush;
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at an
		/// angle of zero (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		public Fill( Color[] colors ) :
			this( colors, 0.0F )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at the
		/// specified angle (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color[] colors, float angle )
		{
			Init();
			ColorBlend blend = new ColorBlend();
			blend.Colors = colors;
			blend.Positions = new float[colors.Length];
			blend.Positions[0] = 0.0F;
			for ( int i=1; i<colors.Length; i++ )
				blend.Positions[i] = (float) i / (float)( colors.Length - 1 );

			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				Color.Red, Color.White, angle );
			((LinearGradientBrush)this.brush).InterpolationColors = blend;
			this.type = FillType.Brush;
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at the
		/// an angle of zero (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		/// <param name="positions">The array of floating point values that defines the color
		/// positions along the gradient.  Values should range from 0 to 1.</param>
		public Fill( Color[] colors, float[] positions ) :
			this( colors, positions, 0.0F )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at the
		/// specified angle (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		/// <param name="positions">The array of floating point values that defines the color
		/// positions along the gradient.  Values should range from 0 to 1.</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color[] colors, float[] positions, float angle )
		{
			Init();
			ColorBlend blend = new ColorBlend();
			blend.Colors = colors;
			blend.Positions = positions;

			this.brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				Color.Red, Color.White, angle );
			((LinearGradientBrush)this.brush).InterpolationColors = blend;
			this.type = FillType.Brush;
		}

		/// <summary>
		/// Constructor that creates a texture fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> and using the specified image.
		/// </summary>
		/// <param name="image">The <see cref="Image"/> to use for filling</param>
		/// <param name="wrapMode">The <see cref="WrapMode"/> class that controls the image wrapping properties</param>
		public Fill( Image image, WrapMode wrapMode )
		{
			Init();
			this.color = Color.White;
			this.brush = new TextureBrush( image, wrapMode );
			this.type = FillType.Brush;
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
			Init();
			this.color = Color.White;
			this.brush = (Brush) brush.Clone();
			this.type = FillType.Brush;
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
			Init();
			this.isScaled = isScaled;
			this.color = Color.White;
			this.brush = (Brush) brush.Clone();
			this.type = FillType.Brush;
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
			Init();
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
            isScaled = rhs.IsScaled;
			rangeMin = rhs.RangeMin;
			rangeMax = rhs.RangeMax;
			gradientBM = null;
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

		/// <summary>
		/// Returns a boolean value indicating whether or not this fill is a "Gradient-By-Value"
		/// type.  This is true for <see cref="FillType.GradientByX"/>, <see cref="FillType.GradientByY"/>,
		/// or <see cref="FillType.GradientByZ"/>.
		/// </summary>
		/// <remarks>
		/// The gradient by value fill method allows the fill color for each point or bar to
		/// be based on a value for that point (either X, Y, or Z in the <see cref="PointPairList"/>.
		/// For example, assume a <see cref="Fill"/> class is defined with a linear gradient ranging from
		/// <see cref="System.Drawing.Color.Blue"/> to <see cref="System.Drawing.Color.Red"/> and the <see cref="Fill.Type"/>
		/// is set to <see cref="FillType.GradientByY"/>.  If <see cref="RangeMin"/> is set to 
		/// 100.0 and <see cref="RangeMax"/> is set to 200.0, then a point that has a Y value of
		/// 100 or less will be colored blue, a point with a Y value of 200 or more will be
		/// colored red, and a point between 100 and 200 will have a color based on a linear scale
		/// between blue and red.  Note that the fill color is always solid for any given point.
		/// You can use the Z value from <see cref="PointPairList"/> along with
		/// <see cref="FillType.GradientByZ"/> to color individual points according to some
		/// property that is independent of the X,Y point pair.
		/// </remarks>
		/// <value>true if this is a Gradient-by-value type, false otherwise</value>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		public bool IsGradientValueType
		{
			get { return type == FillType.GradientByX || type == FillType.GradientByY ||
					type == FillType.GradientByZ; }
		}

		/// <summary>
		/// The minimum user-scale value for the gradient-by-value determination.  This defines
		/// the user-scale value for the start of the gradient.
		/// </summary>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="IsGradientValueType"/>
		/// <value>A double value, in user scale unit</value>
		public double RangeMin
		{
			get { return rangeMin; }
			set { rangeMin = value; }
		}
		/// <summary>
		/// The maximum user-scale value for the gradient-by-value determination.  This defines
		/// the user-scale value for the end of the gradient.
		/// </summary>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="IsGradientValueType"/>
		/// <value>A double value, in user scale unit</value>
		public double RangeMax
		{
			get { return rangeMax; }
			set { rangeMax = value; }
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
			// just provide a default value for the valueFraction
			return MakeBrush( rect, new PointPair( 0.5, 0.5, 0.5 ) );
		}

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
		/// <param name="dataValue">The data value to be used for a value-based
		/// color gradient.  This is only applicable for <see cref="FillType.GradientByX"/>,
		/// <see cref="FillType.GradientByY"/> or <see cref="FillType.GradientByZ"/>.</param>
		/// <returns>A <see cref="System.Drawing.Brush"/> class representing the fill brush</returns>
		public Brush MakeBrush( RectangleF rect, PointPair dataValue )
		{
			// get a brush
			if ( this.IsVisible && ( !this.color.IsEmpty || this.brush != null ) )
			{
				//Brush	brush;
				if ( this.type == FillType.Brush )
				{
					if ( rect.Height < 1.0F )
						rect.Height = 1.0F;
					if ( rect.Width < 1.0F )
						rect.Width = 1.0F;
					
					return ScaleBrush( rect, this.brush, this.isScaled );
				}
				else if ( IsGradientValueType )
				{
					return new SolidBrush( GetGradientColor( dataValue ) );
				}
				else
					return new SolidBrush( this.color );
			}

			// Always return a suitable default
			return new SolidBrush( Color.White );
		}

		private Color GetGradientColor( PointPair dataValue )
		{
			double val, valueFraction;

			if ( this.type == FillType.GradientByZ )
				val = dataValue.Z;
			else if ( this.type == FillType.GradientByY )
				val = dataValue.Y;
			else
				val = dataValue.X;

			if ( Double.IsInfinity( val ) || double.IsNaN( val ) || val == PointPair.Missing ||
					this.rangeMax - this.rangeMin < 1e-20 )
				valueFraction = 0.5;
			else
				valueFraction = ( val - this.rangeMin ) / ( rangeMax - rangeMin );

			if ( valueFraction < 0.0 )
				valueFraction = 0.0;
			else if ( valueFraction > 1.0 )
				valueFraction = 1.0;

			if ( gradientBM == null )
			{
				RectangleF rect = new RectangleF( 0, 0, 100, 1 );
				gradientBM = new Bitmap( 100, 1 );
				Graphics gBM = Graphics.FromImage( gradientBM );

				Brush tmpBrush = ScaleBrush( rect, this.brush, true );
				gBM.FillRectangle( tmpBrush, rect );
			}

			return gradientBM.GetPixel( (int) (99.9 * valueFraction), 0 );
		}

		private Brush ScaleBrush( RectangleF rect, Brush brush, bool isScaled )
		{
			if ( brush != null )
			{
				if ( brush is SolidBrush )
				{
					return (Brush) brush.Clone();
				}
				else if ( brush is LinearGradientBrush )
				{
					LinearGradientBrush linBrush = (LinearGradientBrush) brush.Clone();
					
					if ( isScaled )
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
				else if ( brush is TextureBrush )
				{
					TextureBrush texBrush = (TextureBrush) brush.Clone();
					
					if ( isScaled )
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
				else // other brush type
				{
					return (Brush) brush.Clone();
				}
			}
			else
				// If they didn't provide a brush, make one using the fillcolor gradient to white
				return new LinearGradientBrush( rect, Color.White, this.color, 0F );
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
