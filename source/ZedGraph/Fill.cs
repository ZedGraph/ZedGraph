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
	/// <version> $Revision: 2.1 $ $Date: 2004-09-13 06:51:43 $ </version>
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

	#endregion

	#region Constructors
		/// <summary>
		/// The default constructor.  Some typical values are initialized, but these values will get
		/// overridden anyway.
		/// </summary>
		public Fill()
		{
			color = Color.Red;
			brush = null;
			type = FillType.None;
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
		/// Read-Only property that determines the type of color fill. 
		/// Returns true if the <see cref="Type"/> property is either
		/// <see cref="FillType.Solid"/> or
		/// <see cref="FillType.Brush"/>.
		/// </summary>
		/// <seealso cref="Color"/>
		/// <seealso cref="Brush"/>
		/// <seealso cref="Type"/>
		public bool IsFilled
		{
			get { return type != FillType.None; }
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
			if ( this.IsFilled && ( !this.color.IsEmpty || this.brush != null ) )
			{
				Brush	brush;
				if ( this.type == FillType.Brush )
				{
					if ( this.brush != null )
					{
						brush = this.brush;
						if ( brush is LinearGradientBrush || brush is TextureBrush )
						{
							LinearGradientBrush linBrush = (LinearGradientBrush) this.brush.Clone();
							linBrush.ScaleTransform( rect.Width / linBrush.Rectangle.Width,
								rect.Height / linBrush.Rectangle.Height, MatrixOrder.Append );
							linBrush.TranslateTransform( rect.Left - linBrush.Rectangle.Left,
								rect.Top - linBrush.Rectangle.Top, MatrixOrder.Append );
							brush = (Brush) linBrush;
						}
					}
					else
						// If they didn't provide a brush, make one using the fillcolor gradient to white
						brush = new LinearGradientBrush( rect,
							Color.White, this.color, 0F );
				}
				else
					brush = (Brush) new SolidBrush( this.color );

				return brush;
			}

			return null;
		}
	#endregion
	}
}
