using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="ZedGraph.ArrowItem"/> type graphic objects
	/// to be displayed on the graph.
	/// </summary>
	public class ArrowList : CollectionBase, ICloneable
	{
		/// <summary>
		/// Default constructor for the collection class
		/// </summary>
		public ArrowList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The ArrowList object from which to copy</param>
		public ArrowList( ArrowList rhs )
		{
			foreach ( ArrowItem item in rhs )
				this.Add( new ArrowItem( item ) );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the ArrowList</returns>
		public object Clone()
		{ 
			return new ArrowList( this ); 
		}
		
		/// <summary>
		/// Indexer to access the specified <see cref="ZedGraph.ArrowItem"/> object by its ordinal
		/// position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="ZedGraph.ArrowItem"/> object to be accessed.</param>
		/// <value>An <see cref="ZedGraph.ArrowItem"/> object reference.</value>
		public ArrowItem this[ int index ]  
		{
			get { return( (ArrowItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add an <see cref="ArrowItem"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="item">A reference to the <see cref="ArrowItem"/> object to
		/// be added</param>
		public void Add( ArrowItem item )
		{
			List.Add( item );
		}

		/// <summary>
		/// Remove an <see cref="ArrowItem"/> object from the collection at the
		/// specified ordinal location.
		/// </summary>
		/// <param name="index">An ordinal position in the list at which
		/// the object to be removed is located. </param>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Render all objects to the specified <see cref="Graphics"/> device
		/// by calling the Draw method of each <see cref="ArrowItem"/> object in
		/// the collection.  This method is normally only called by the Draw method
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
		public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			foreach ( ArrowItem item in this )
				item.Draw( g, pane, scaleFactor );
		}
	}

	/// <summary>
	/// A class that represents a graphic arrow or line object on the graph.  A list of
	/// ArrowItem objects is maintained by the <see cref="ArrowList"/> collection class.
	/// </summary>
	public class ArrowItem : ICloneable
	{
		/// <summary>
		/// Private field that stores the X location of the starting point
		/// that defines the arrow segment.  Use the public property
		/// <see cref="X1"/> to access this value.
		/// </summary>
		/// <value>The units are defined as per the <see cref="coordinateFrame"/> setting</value>
		private float x1;
		/// <summary>
		/// Private field that stores the Y location of the starting point
		/// that defines the arrow segment.  Use the public property
		/// <see cref="Y1"/> to access this value.
		/// </summary>
		/// <value>The units are defined as per the <see cref="coordinateFrame"/> setting</value>
		private float y1;
		/// <summary>
		/// Private field that stores the X location of the ending point
		/// that defines the arrow segment.  Use the public property
		/// <see cref="X2"/> to access this value.
		/// </summary>
		/// <value>The units are defined as per the <see cref="coordinateFrame"/> setting</value>
		private float x2;
		/// <summary>
		/// Private field that stores the Y location of the ending point
		/// that defines the arrow segment.  Use the public property
		/// <see cref="Y2"/> to access this value.
		/// </summary>
		/// <value>The units are defined as per the <see cref="coordinateFrame"/> setting</value>
		private float y2;

		/// <summary>
		/// Private field that stores the arrowhead size, measured in points.
		/// Use the public property <see cref="Size"/> to access this value.
		/// </summary>
		private float		size;
		/// <summary>
		/// Private field that stores the color of the arrow.
		/// Use the public property <see cref="Color"/> to access this value.
		/// </summary>
		/// <value>The color value is declared with a <see cref="System.Drawing.Color"/>
		/// specification</value>
		private Color		color;
		/// <summary>
		/// Private field that stores the width of the pen for drawing the line
		/// segment of the arrow.
		/// Use the public property <see cref="PenWidth"/> to access this value.
		/// </summary>
		/// <value> The width is defined in pixel units </value>
		private float		penWidth;
		/// <summary>
		/// Private boolean field that stores the arrowhead state.
		/// Use the public property <see cref="IsArrowHead"/> to access this value.
		/// </summary>
		/// <value> true if an arrowhead is to be drawn, false otherwise </value>
		private bool		isArrowHead;
		/// <summary>
		/// Private field that stores the coordinate system to be used for
		/// defining the <see cref="ArrowItem"/> position.  Use the public
		/// property <see cref="CoordinateFrame"/> to access this value.
		/// </summary>
		/// <value> The coordinate system is defined with the <see cref="CoordType"/>
		/// enum</value>
		private CoordType	coordinateFrame;

		/// <overloads>Constructors for the <see cref="ArrowItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the position, color, and size of the
		/// <see cref="ArrowItem"/> to be pre-specified.
		/// </summary>
		/// <param name="color">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the arrow</param>
		/// <param name="size">The size of the arrowhead, measured in points.</param>
		/// <param name="x1">The x position of the starting point that defines the
		/// arrow.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		/// <param name="y1">The y position of the starting point that defines the
		/// arrow.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		/// <param name="x2">The x position of the ending point that defines the
		/// arrow.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		/// <param name="y2">The y position of the ending point that defines the
		/// arrow.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		public ArrowItem( Color color, float size, float x1, float y1,
			float x2, float y2 )
		{
			Init();
			this.color = color;
			this.size = size;
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
		}

		/// <summary>
		/// A constructor that allows only the position of the
		/// arrow to be pre-specified.  All other properties are set to
		/// default values
		/// </summary>
		/// <param name="x1">The x position of the starting point that defines the
		/// <see cref="ArrowItem"/>.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		/// <param name="y1">The y position of the starting point that defines the
		/// <see cref="ArrowItem"/>.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		/// <param name="x2">The x position of the ending point that defines the
		/// <see cref="ArrowItem"/>.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		/// <param name="y2">The y position of the ending point that defines the
		/// <see cref="ArrowItem"/>.  The units of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.</param>
		public ArrowItem( float x1, float y1, float x2, float y2 )
		{
			Init();
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
		}

		/// <summary>
		/// This method initializes the ArrowItem properties to default values
		/// as defined in class <see cref="Def"/>
		/// </summary>
		protected void Init()
		{
			this.color = Def.Arr.Color;
			this.size = Def.Arr.Size;
			this.penWidth = Def.Arr.PenWidth;
			this.x1 = 0F;
			this.y1 = 0F;
			this.x2 = 0.2F;
			this.y2 = 0.2F;
			isArrowHead = Def.Arr.IsArrowHead;
			coordinateFrame = Def.Arr.CoordFrame;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The ArrowItem object from which to copy</param>
		public ArrowItem( ArrowItem rhs )
		{
			x1 = rhs.X1;
			y1 = rhs.Y1;
			x2 = rhs.X2;
			y2 = rhs.Y2;
			size = rhs.Size;
			color = rhs.Color;
			penWidth = rhs.PenWidth;
			isArrowHead = rhs.IsArrowHead;
			coordinateFrame = rhs.CoordinateFrame;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the ArrowItem</returns>
		public object Clone()
		{ 
			return new ArrowItem( this ); 
		}

		/// <summary>
		/// X1 is the X value of the starting point that defines the
		/// <see cref="ArrowItem"/> segment </summary>
		/// <value> The units are defined by the <see cref="CoordinateFrame"/>
		/// property </value>
		public float X1
		{
			get { return x1; }
			set { x1 = value; }
		}
		/// <summary>
		/// X2 is the X value of the ending point that defines the
		/// <see cref="ArrowItem"/> segment </summary>
		/// <value> The units are defined by the <see cref="CoordinateFrame"/>
		/// property </value>
		public float X2
		{
			get { return x2; }
			set { x2 = value; }
		}
		/// <summary>
		/// Y1 is the Y value of the starting point that defines the
		/// <see cref="ArrowItem"/> segment </summary>
		/// <value> The units are defined by the <see cref="CoordinateFrame"/>
		/// property </value>
		public float Y1
		{
			get { return y1; }
			set { y1 = value; }
		}
		/// <summary>
		/// Y2 is the Y value of the ending point that defines the
		/// <see cref="ArrowItem"/> segment </summary>
		/// <value> The units are defined by the <see cref="CoordinateFrame"/>
		/// property </value>
		public float Y2
		{
			get { return y2; }
			set { y2 = value; }
		}
		/// <summary>
		/// The size of the arrowhead.  The display of the arrowhead can be
		/// enabled or disabled with the <see cref="IsArrowHead"/> property.
		/// </summary>
		/// <value> The size is defined in pixel units </value>
		public float Size
		{
			get { return size; }
			set { size = value; }
		}
		/// <summary>
		/// The width of the line segment for the <see cref="ArrowItem"/>
		/// </summary>
		/// <value> The width is defined in pixel units </value>
		public float PenWidth
		{
			get { return penWidth; }
			set { penWidth = value; }
		}
		/// <summary>
		/// The <see cref="System.Drawing.Color"/> of the arrowhead and line segment
		/// </summary>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class </value>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		/// <summary>
		/// The coordinate system to be used for defining the <see cref="ArrowItem"/> position
		/// </summary>
		/// <value> The coordinate system is defined with the <see cref="CoordType"/>
		/// enum</value>
		public CoordType CoordinateFrame
		{
			get { return coordinateFrame; }
			set { coordinateFrame = value; }
		}
		/// <summary>
		/// Determines whether or not to draw an arrowhead
		/// </summary>
		/// <value> true to show the arrowhead, false to show the line segment
		/// only</value>
		public bool IsArrowHead
		{
			get { return isArrowHead; }
			set { isArrowHead = value; }
		}

		/// <summary>
		/// Render this object to the specified <see cref="Graphics"/> device
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="ArrowList"/> collection object.
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
		public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			// Convert the arrow coordinates from the user coordinate system
			// to the screen coordinate system
			PointF pix1 = pane.GeneralTransform( new PointF(this.x1, this.y1),
				this.coordinateFrame );
			PointF pix2 = pane.GeneralTransform( new PointF(this.x2, this.y2),
				this.coordinateFrame );

			if ( pix1.X > -10000 && pix1.X < 100000 && pix1.Y > -100000 && pix1.Y < 100000 &&
				pix2.X > -10000 && pix2.X < 100000 && pix2.Y > -100000 && pix2.Y < 100000 )
			{
				// get a scaled size for the arrowhead
				float scaledSize = (float) ( this.size * scaleFactor );

				// calculate the length and the angle of the arrow "vector"
				double dy = pix2.Y - pix1.Y;
				double dx = pix2.X - pix1.X;
				float angle = (float) Math.Atan2( dy, dx ) * 180.0F / (float) Math.PI;
				float length = (float) Math.Sqrt( dx*dx + dy*dy );

				// Save the old transform matrix
				Matrix transform = g.Transform;
				// Move the coordinate system so it is located at the starting point
				// of this arrow
				g.TranslateTransform( pix1.X, pix1.Y );
				// Rotate the coordinate system according to the angle of this arrow
				// about the starting point
				g.RotateTransform( angle );

				// get a pen according to this arrow properties
				Pen pen = new Pen( this.color, this.penWidth );
				
				// Draw the line segment for this arrow
				g.DrawLine( pen, 0, 0, length, 0 );

				// Only show the arrowhead if required
				if ( this.isArrowHead )
				{
					SolidBrush brush = new SolidBrush( this.color );
					
					// Create a polygon representing the arrowhead based on the scaled
					// size
					PointF[] polyPt = new PointF[4];
					float hsize = scaledSize / 3.0F;
					polyPt[0].X = length;
					polyPt[0].Y = 0;
					polyPt[1].X = length-size;
					polyPt[1].Y = hsize;
					polyPt[2].X = length-size;
					polyPt[2].Y = -hsize;
					polyPt[3] = polyPt[0];
					
					// render the arrowhead
					g.FillPolygon( brush, polyPt );
				}
				
				// Restore the transform matrix back to its original state
				g.Transform = transform;
			}
		}
	}
}
