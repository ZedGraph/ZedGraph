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
	/// A class than contains information about the position of an object on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.4 $ $Date: 2004-11-19 06:41:05 $ </version>
	public class Location : ICloneable
	{
	#region Private Fields
		/// <summary> Private field to store the vertical alignment property for
		/// this object.  Use the public property <see cref="Location.AlignV"/>
		/// to access this value.  The value of this field is a <see cref="AlignV"/> enum.
		/// </summary>
		private AlignV	alignV;
		/// <summary> Private field to store the horizontal alignment property for
		/// this object.  Use the public property <see cref="Location.AlignH"/>
		/// to access this value.  The value of this field is a <see cref="AlignH"/> enum.
		/// </summary>
		private AlignH	alignH;

		/// <summary> Private fields to store the X and Y coordinate positions for
		/// this object.  Use the public properties <see cref="X"/> and
		/// <see cref="Y"/> to access these values.  The coordinate type stored here is
		/// dependent upon the setting of <see cref="CoordinateFrame"/>.
		/// </summary>
		private float		x,
							y,
							x2,
							y2;
							
		/// <summary>
		/// Private field to store the coordinate system to be used for defining the
		/// object position.  Use the public property
		/// <see cref="CoordinateFrame"/> to access this value. The coordinate system
		/// is defined with the <see cref="CoordType"/> enum.
		/// </summary>
		private CoordType	coordinateFrame;
	#endregion

	#region Properties
		/// <summary>
		/// A horizontal alignment parameter for this object specified
		/// using the <see cref="AlignH"/> enum type.
		/// </summary>
		public AlignH AlignH
		{
			get { return alignH; }
			set { alignH = value; }
		}
		/// <summary>
		/// A vertical alignment parameter for this object specified
		/// using the <see cref="AlignV"/> enum type.
		/// </summary>
		public AlignV AlignV
		{
			get { return alignV; }
			set { alignV = value; }
		}
		/// <summary>
		/// The coordinate system to be used for defining the object position
		/// </summary>
		/// <value> The coordinate system is defined with the <see cref="CoordType"/>
		/// enum</value>
		public CoordType CoordinateFrame
		{
			get { return coordinateFrame; }
			set { coordinateFrame = value; }
		}
		/// <summary>
		/// The x position of the object.
		/// </summary>
		/// <remarks>
		/// The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The object will be aligned to this position based on the
		/// <see cref="AlignH"/> property.
		/// </remarks>
		public float X
		{
			get { return x; }
			set { x = value; }
		}
		/// <summary>
		/// The y position of the object.
		/// </summary>
		/// <remarks>
		/// The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The object will be aligned to this position based on the
		/// <see cref="AlignV"/> property.
		/// </remarks>
		public float Y
		{
			get { return y; }
			set { y = value; }
		}
		/// <summary>
		/// The x1 position of the object (an alias for the x position).
		/// </summary>
		/// <remarks>
		/// The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The object will be aligned to this position based on the
		/// <see cref="AlignH"/> property.
		/// </remarks>
		public float X1
		{
			get { return x; }
			set { x = value; }
		}
		/// <summary>
		/// The y1 position of the object (an alias for the y position).
		/// </summary>
		/// <remarks>
		/// The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The object will be aligned to this position based on the
		/// <see cref="AlignV"/> property.
		/// </remarks>
		public float Y1
		{
			get { return y; }
			set { y = value; }
		}
		/// <summary>
		/// The x2 position of the object.
		/// </summary>
		/// <remarks>
		/// The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The object will be aligned to this position based on the
		/// <see cref="AlignH"/> property.  This position is only used for
		/// objects such as <see cref="ArrowItem"/>, where it makes sense
		/// to have a second coordinate.
		/// </remarks>
		public float X2
		{
			get { return x2; }
			set { x2 = value; }
		}
		/// <summary>
		/// The x position of the object.
		/// </summary>
		/// <remarks>
		/// The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The object will be aligned to this position based on the
		/// <see cref="AlignV"/> property.  This position is only used for
		/// objects such as <see cref="ArrowItem"/>, where it makes sense
		/// to have a second coordinate.
		/// </remarks>
		public float Y2
		{
			get { return y2; }
			set { y2 = value; }
		}

		/// <summary>
		/// The <see cref="RectangleF"/> for this object as defined by the
		/// <see cref="X"/>, <see cref="Y"/>, <see cref="X2"/>, and <see cref="Y2"/>
		/// properties.
		/// </summary>
		/// <value>A <see cref="RectangleF"/> in <see cref="CoordinateFrame"/> units.</value>
		public RectangleF Rect
		{
			get { return new RectangleF( this.x, this.y, this.x2 - this.x, this.y2 - this.y ); }
			set
			{
				this.x = value.X;
				this.y = value.Y;
				this.x2 = value.X + value.Width;
				this.y2 = value.Y + value.Height;
			}
		}

		/// <summary>
		/// The top-left <see cref="PointF"/> for this <see cref="Location"/>.
		/// </summary>
		/// <value>A <see cref="PointF"/> in <see cref="CoordinateFrame"/> units.</value>
		public PointF TopLeft
		{
			get { return new PointF( this.x, this.y ); }
			set { this.x = value.X; this.y = value.Y; }
		}

		/// <summary>
		/// The bottom-right <see cref="PointF"/> for this <see cref="Location"/>.
		/// </summary>
		/// <value>A <see cref="PointF"/> in <see cref="CoordinateFrame"/> units.</value>
		public PointF BottomRight
		{
			get { return new PointF( this.X2, this.Y2 ); }
			set { this.X2 = value.X; this.Y2 = value.Y; }
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// Constructor for the <see cref="Location"/> class that specifies the
		/// x, y position and the <see cref="CoordType"/>.
		/// </summary>
		/// <remarks>
		/// The (x,y) position corresponds to the top-left corner;
		/// </remarks>
		/// <param name="x">The x position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="y">The y position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="coordType">The <see cref="CoordType"/> enum that specifies the
		/// units for <see paramref="x"/> and <see paramref="y"/></param>
		public Location( float x, float y, CoordType coordType ) :
				this( x, y, coordType, AlignH.Left, AlignV.Top )
		{
		}
		
		/// <summary>
		/// Constructor for the <see cref="Location"/> class that specifies the
		/// x, y position and the <see cref="CoordType"/>.
		/// </summary>
		/// <remarks>
		/// The (x,y) position corresponds to the top-left corner;
		/// </remarks>
		/// <param name="x">The x position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="y">The y position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="coordType">The <see cref="CoordType"/> enum that specifies the
		/// units for <see paramref="x"/> and <see paramref="y"/></param>
		/// <param name="alignH">The <see cref="ZedGraph.AlignH"/> enum that specifies
		/// the horizontal alignment of the object with respect to the (x,y) location</param>
		/// <param name="alignV">The <see cref="ZedGraph.AlignV"/> enum that specifies
		/// the vertical alignment of the object with respect to the (x,y) location</param>
		public Location( float x, float y, CoordType coordType, AlignH alignH, AlignV alignV )
		{
			this.x = x;
			this.y = y;
			this.x2 = 0;
			this.y2 = 0;
			this.coordinateFrame = coordType;
			this.alignH = alignH;
			this.alignV = alignV;
		}
		
		/// <summary>
		/// Constructor for the <see cref="Location"/> class that specifies the
		/// (x, y) and (x2, y2) positions and the <see cref="CoordType"/>.
		/// </summary>
		/// <remarks>
		/// The (x,y) position
		/// corresponds to the starting position, the (x2, y2) coorresponds to the ending position
		/// (typically used for <see cref="ArrowItem"/>'s).
		/// </remarks>
		/// <param name="x">The x position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="y">The y position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="x2">The x2 position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="y2">The y2 position, specified in units of <see paramref="coordType"/>.
		/// </param>
		/// <param name="coordType">The <see cref="CoordType"/> enum that specifies the
		/// units for <see paramref="x"/> and <see paramref="y"/></param>
		/// <param name="alignH">The <see cref="ZedGraph.AlignH"/> enum that specifies
		/// the horizontal alignment of the object with respect to the (x,y) location</param>
		/// <param name="alignV">The <see cref="ZedGraph.AlignV"/> enum that specifies
		/// the vertical alignment of the object with respect to the (x,y) location</param>
		public Location( float x, float y, float x2, float y2, CoordType coordType, AlignH alignH, AlignV alignV ) :
			this( x, y, coordType, alignH, alignV )
		{
			this.x2 = x2;
			this.y2 = y2;
		}
		
		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="Location"/> object from which to copy</param>
		public Location( Location rhs )
		{
			this.x = rhs.x;
			this.y = rhs.y;
			this.x2 = rhs.x2;
			this.y2 = rhs.y2;
			this.coordinateFrame = rhs.CoordinateFrame;
			this.alignH = rhs.AlignH;
			this.alignV = rhs.AlignV;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="Location"/> object</returns>
		public object Clone()
		{ 
			return new Location( this ); 
		}
	#endregion
	
	#region Methods
		/// <summary>
		/// Transform this <see cref="Location"/> object to display device
		/// coordinates using the properties of the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that contains
		/// the <see cref="Axis"/> classes which will be used for the transform.
		/// </param>
		/// <returns>A point in display device coordinates that corresponds to the
		/// specified user point.</returns>
		public PointF Transform( GraphPane pane )
		{
			return Transform( pane, new PointF( this.x, this.y ), this.coordinateFrame );
		}
		
		/// <summary>
		/// Transform a data point from the specified coordinate type
		/// (<see cref="CoordType"/>) to display device coordinates (pixels).
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that contains
		/// the <see cref="Axis"/> classes which will be used for the transform.
		/// </param>
		/// <param name="ptF">The X,Y pair that defines the point in user
		/// coordinates.</param>
		/// <param name="coord">A <see cref="CoordType"/> type that defines the
		/// coordinate system in which the X,Y pair is defined.</param>
		/// <returns>A point in display device coordinates that corresponds to the
		/// specified user point.</returns>
		public static PointF Transform( GraphPane pane, PointF ptF, CoordType coord )
		{
			PointF ptPix = new PointF();

			if ( coord == CoordType.AxisFraction )
			{
				ptPix.X = pane.AxisRect.Left + ptF.X * pane.AxisRect.Width;
				ptPix.Y = pane.AxisRect.Top + ptF.Y * pane.AxisRect.Height;
			}
			else if ( coord == CoordType.AxisXYScale )
			{
				ptPix.X = pane.XAxis.Transform( ptF.X );
				ptPix.Y = pane.YAxis.Transform( ptF.Y );
			}
			else if ( coord == CoordType.AxisXY2Scale )
			{
				ptPix.X = pane.XAxis.Transform( ptF.X );
				ptPix.Y = pane.Y2Axis.Transform( ptF.Y );
			}
			else	// PaneFraction
			{
				ptPix.X = pane.PaneRect.Left + ptF.X * pane.PaneRect.Width;
				ptPix.Y = pane.PaneRect.Top + ptF.Y * pane.PaneRect.Height;
			}

			return ptPix;
		}
		
		/// <summary>
		/// Transform this <see cref="Location"/> from the coordinate system
		/// as specified by <see cref="CoordinateFrame"/> to the device coordinates
		/// of the specified <see cref="GraphPane"/> object.
		/// </summary>
		/// <remarks>
		/// The returned
		/// <see cref="PointF"/> struct represents the top-left corner of the
		/// object that honors the <see cref="Location"/> properties.
		/// The <see cref="AlignH"/> and <see cref="AlignV"/> properties are honored in 
		/// this transformation.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that contains
		/// the <see cref="Axis"/> classes which will be used for the transform.
		/// </param>
		/// <param name="width">The width of the object in device pixels</param>
		/// <param name="height">The height of the object in device pixels</param>
		/// <returns>The top-left corner of the object</returns>
		public PointF TransformTopLeft( GraphPane pane, float width, float height )
		{
			PointF pt = Transform( pane );
			
			if ( alignH == AlignH.Right )
				pt.X -= width;
			else if ( alignH == AlignH.Center )
				pt.X -= width / 2.0F;
				
			if ( alignV == AlignV.Bottom )
				pt.Y -= height;
			else if ( alignV == AlignV.Center )
				pt.Y -= height / 2.0F;
			
			return pt;
		}

		/// <summary>
		/// The <see cref="PointF"/> for this object as defined by the
		/// <see cref="X"/> and <see cref="Y"/>
		/// properties.
		/// </summary>
		/// <remarks>
		/// This method transforms the location to output device pixel units.
		/// The <see cref="AlignH"/> and <see cref="AlignV"/> properties are ignored for
		/// this transformation (see <see cref="TransformTopLeft(GraphPane,float,float)"/>).
		/// </remarks>
		/// <value>A <see cref="PointF"/> in pixel units.</value>
		public PointF TransformTopLeft( GraphPane pane )
		{
			return Transform( pane );
		}

		/// <summary>
		/// The <see cref="PointF"/> for this object as defined by the
		/// <see cref="X2"/> and <see cref="Y2"/>
		/// properties.
		/// </summary>
		/// <remarks>
		/// This method transforms the location to output device pixel units.
		/// The <see cref="AlignH"/> and <see cref="AlignV"/> properties are ignored for
		/// this transformation (see <see cref="TransformTopLeft(GraphPane,float,float)"/>).
		/// </remarks>
		/// <value>A <see cref="PointF"/> in pixel units.</value>
		public PointF TransformBottomRight( GraphPane pane )
		{
			return Transform( pane, new PointF( this.x2, this.y2 ),
				this.coordinateFrame );
		}

		/// <summary>
		/// Transform the <see cref="RectangleF"/> for this object as defined by the
		/// <see cref="X"/>, <see cref="Y"/>, <see cref="X2"/>, and <see cref="Y2"/>
		/// properties.
		/// </summary>
		/// <remarks>
		/// This method transforms the location to output device pixel units.
		/// The <see cref="AlignH"/> and <see cref="AlignV"/> properties are honored in 
		/// this transformation.
		/// </remarks>
		/// <value>A <see cref="RectangleF"/> in pixel units.</value>
		public RectangleF TransformRect( GraphPane pane )
		{
			PointF pix1 = TransformTopLeft( pane );
			PointF pix2 = TransformBottomRight( pane );
			PointF pix3 = TransformTopLeft( pane, pix2.X - pix1.X, pix2.Y - pix1.Y );

			return new RectangleF( pix3.X, pix3.Y, pix2.X - pix1.X, pix2.Y - pix1.Y );
		}

	#endregion

	}
}
