using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ZedGraph
{
	/// <summary>
	/// A class that encapsulates Border (frame) properties for an object.  The <see cref="Border"/> class
	/// is used in a variety of ZedGraph objects to handle the drawing of the Border around the object.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2004-10-14 04:06:01 $ </version>
	public class Border
	{
		#region Fields
		/// <summary>
		/// Private field that stores the Border color.  Use the public
		/// property <see cref="Color"/> to access this value.
		/// </summary>
		private Color	color;
		/// <summary>
		/// Private field that stores the pen width for this Border.  Use the public
		/// property <see cref="PenWidth"/> to access this value.
		/// </summary>
		private float	penWidth;
		/// <summary>
		/// Private field that determines if the Border will be drawn.  The Border will only
		/// be drawn if this value is true.  Use the public property <see cref="IsVisible"/>
		/// for access to this value.
		/// </summary>
		private bool	isVisible;
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
			/// The default visibility for drawing the Border.
			/// See the <see cref="IsVisible"/> property.
			/// </summary>
			public static bool IsVisible = false;
			/// <summary>
			/// The default pen width (pixels) for the Border.  See the <see cref="PenWidth"/>
			/// property.
			/// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default color for the Border.  See the <see cref="Color"/> property.
			/// </summary>
			public static Color Color = Color.Black;
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// The default constructor.  Initialized to default values.
		/// </summary>
		public Border()
		{
			isVisible = Default.IsVisible;
			color = Default.Color;
			penWidth = Default.PenWidth;
		}

		/// <summary>
		/// Constructor that specifies the visibility, color and penWidth of the Border.
		/// </summary>
		/// <param name="isVisible">Determines whether or not the Border will be drawn.</param>
		/// <param name="color">The color of the Border</param>
		/// <param name="penWidth">The width, in pixels, for the Border.</param>
		public Border( bool isVisible, Color color, float penWidth )
		{
			this.color = color.IsEmpty ? Default.Color : color;
			this.penWidth = penWidth;
			this.isVisible = isVisible;
		}

		/// <summary>
		/// Constructor that specifies the color and penWidth of the Border.
		/// </summary>
		/// <param name="color">The color of the Border</param>
		/// <param name="penWidth">The width, in pixels, for the Border.</param>
		public Border( Color color, float penWidth ) : this( true, color, penWidth )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Border object from which to copy</param>
		public Border( Border rhs )
		{
			color = rhs.Color;
			penWidth = rhs.PenWidth;
			isVisible = rhs.IsVisible;
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Border class</returns>
		public object Clone()
		{ 
			return new Border( this ); 
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Determines the <see cref="System.Drawing.Color"/> of the <see cref="Pen"/> used to
		/// draw this Border.
		/// </summary>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		/// <summary>
		/// Gets or sets the width of the <see cref="Pen"/> used to draw this Border.
		/// </summary>
		public float PenWidth
		{
			get { return penWidth; }
			set { penWidth = value; }
		}
		/// <summary>
		/// Determines whether or not the Border will be drawn.  true to draw the Border,
		/// false otherwise
		/// </summary>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// Create a new <see cref="Pen"/> object from the properties of this
		/// <see cref="Border"/> object.
		/// </summary>
		/// <returns>A <see cref="Pen"/> object with the proper color and pen width.</returns>
		public Pen MakePen()
		{
			return new Pen( color, penWidth );
		}
		
		/// <summary>
		/// Draw the specified Border (<see cref="RectangleF"/>) using the properties of
		/// this <see cref="Border"/> object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">A <see cref="RectangleF"/> struct to be drawn.</param>
		public void Draw( Graphics g, RectangleF rect )
		{
			if ( this.isVisible )
				g.DrawRectangle( MakePen(), Rectangle.Round( rect ) );
		}
		#endregion
	}
}




