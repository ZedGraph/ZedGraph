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
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// An abstract base class that represents a text object on the graph.  A list of
	/// <see cref="GraphItem"/> objects is maintained by the
	/// <see cref="GraphItemList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.4 $ $Date: 2004-12-03 13:31:28 $ </version>
	abstract public class GraphItem
	{
	#region Fields
		/// <summary>
		/// Protected field that stores the location of this <see cref="GraphItem"/>.
		/// Use the public property <see cref="Location"/> to access this value.
		/// </summary>
		protected Location location;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="GraphItem"/> class.
		/// </summary>
		public struct Default
		{
			// Default text item properties
			/// <summary>
			/// Default value for the vertical <see cref="GraphItem"/>
			/// text alignment (<see cref="GraphItem.Location"/> property).
			/// This is specified
			/// using the <see cref="AlignV"/> enum type.
			/// </summary>
			public static AlignV AlignV = AlignV.Center;
			/// <summary>
			/// Default value for the horizontal <see cref="GraphItem"/>
			/// text alignment (<see cref="GraphItem.Location"/> property).
			/// This is specified
			/// using the <see cref="AlignH"/> enum type.
			/// </summary>
			public static AlignH AlignH = AlignH.Center;
			/// <summary>
			/// The default coordinate system to be used for defining the
			/// <see cref="GraphItem"/> location coordinates
			/// (<see cref="GraphItem.Location"/> property).
			/// </summary>
			/// <value> The coordinate system is defined with the <see cref="CoordType"/>
			/// enum</value>
			public static CoordType CoordFrame = CoordType.AxisXYScale;
		}
	#endregion

	#region Properties
		/// <summary>
		/// The <see cref="ZedGraph.Location"/> struct that describes the location
		/// for this <see cref="GraphItem"/>.
		/// </summary>
		public Location Location
		{
			get { return location; }
			set { location = value; }
		}
	#endregion
	
	#region Constructors
		/// <overloads>
		/// Constructors for the <see cref="GraphItem"/> class.
		/// </overloads>
		/// <summary>
		/// Default constructor that sets all <see cref="GraphItem"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		public GraphItem() :
			this( 0, 0, Default.CoordFrame, Default.AlignH, Default.AlignV )
		{
		}

		/// <summary>
		/// Constructor that sets all <see cref="GraphItem"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="x">The x position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		public GraphItem( float x, float y ) :
			this( x, y, Default.CoordFrame, Default.AlignH, Default.AlignV )
		{
		}

		/// <summary>
		/// Constructor that creates a <see cref="GraphItem"/> with the specified
		/// coordinates and all other properties to defaults as specified
		/// in the <see cref="Default"/> class..
		/// </summary>
		/// <remarks>
		/// The four coordinates define the starting point and ending point for
		/// <see cref="ArrowItem"/>'s, or the topleft and bottomright points for
		/// <see cref="ImageItem"/>'s.  For <see cref="GraphItem"/>'s that only require
		/// one point, the <see paramref="x2"/> and <see paramref="y2"/> values
		/// will be ignored.  The units of the coordinates are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.
		/// </remarks>
		/// <param name="x">The x position of the item.</param>
		/// <param name="y">The y position of the item.</param>
		/// <param name="x2">The x2 position of the item.</param>
		/// <param name="y2">The x2 position of the item.</param>
		public GraphItem( float x, float y, float x2, float y2 ) :
			this( x, y, x2, y2, Default.CoordFrame, Default.AlignH, Default.AlignV )
		{
		}

		/// <summary>
		/// Constructor that creates a <see cref="GraphItem"/> with the specified
		/// position and <see cref="CoordType"/>.  Other properties are set to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <remarks>
		/// The two coordinates define the location point for the object.
		/// The units of the coordinates are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.
		/// </remarks>
		/// <param name="x">The x position of the item.  The item will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the item.  The item will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		/// <param name="coordType">The <see cref="CoordType"/> enum value that
		/// indicates what type of coordinate system the x and y parameters are
		/// referenced to.</param>
		public GraphItem( float x, float y, CoordType coordType ) :
			this( x, y, coordType, Default.AlignH, Default.AlignV )
		{
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="GraphItem"/> with the specified
		/// position, <see cref="CoordType"/>, <see cref="AlignH"/>, and <see cref="AlignV"/>.
		/// Other properties are set to default values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <remarks>
		/// The two coordinates define the location point for the object.
		/// The units of the coordinates are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.
		/// </remarks>
		/// <param name="x">The x position of the item.  The item will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		/// <param name="coordType">The <see cref="CoordType"/> enum value that
		/// indicates what type of coordinate system the x and y parameters are
		/// referenced to.</param>
		/// <param name="alignH">The <see cref="ZedGraph.AlignH"/> enum that specifies
		/// the horizontal alignment of the object with respect to the (x,y) location</param>
		/// <param name="alignV">The <see cref="ZedGraph.AlignV"/> enum that specifies
		/// the vertical alignment of the object with respect to the (x,y) location</param>
		public GraphItem( float x, float y, CoordType coordType, AlignH alignH, AlignV alignV )
		{
			this.location = new Location( x, y, coordType, alignH, alignV );
		}

		/// <summary>
		/// Constructor that creates a <see cref="GraphItem"/> with the specified
		/// position, <see cref="CoordType"/>, <see cref="AlignH"/>, and <see cref="AlignV"/>.
		/// Other properties are set to default values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <remarks>
		/// The four coordinates define the starting point and ending point for
		/// <see cref="ArrowItem"/>'s, or the topleft and bottomright points for
		/// <see cref="ImageItem"/>'s.  For <see cref="GraphItem"/>'s that only require
		/// one point, the <see paramref="x2"/> and <see paramref="y2"/> values
		/// will be ignored.  The units of the coordinates are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.
		/// </remarks>
		/// <param name="x">The x position of the item.</param>
		/// <param name="y">The y position of the item.</param>
		/// <param name="x2">The x2 position of the item.</param>
		/// <param name="y2">The x2 position of the item.</param>
		/// <param name="coordType">The <see cref="CoordType"/> enum value that
		/// indicates what type of coordinate system the x and y parameters are
		/// referenced to.</param>
		/// <param name="alignH">The <see cref="ZedGraph.AlignH"/> enum that specifies
		/// the horizontal alignment of the object with respect to the (x,y) location</param>
		/// <param name="alignV">The <see cref="ZedGraph.AlignV"/> enum that specifies
		/// the vertical alignment of the object with respect to the (x,y) location</param>
		public GraphItem( float x, float y, float x2, float y2, CoordType coordType,
					AlignH alignH, AlignV alignV )
		{
			this.location = new Location( x, y, x2, y2, coordType, alignH, alignV );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="GraphItem"/> object from which to copy</param>
		public GraphItem( GraphItem rhs )
		{
			this.location = rhs.Location;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the GraphItem</returns>
		public abstract object Clone();
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Render this <see cref="GraphItem"/> object to the specified <see cref="Graphics"/> device.
		/// </summary>
		/// <remarks>
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphItemList"/> collection object.
		/// </remarks>
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
		abstract public void Draw( Graphics g, GraphPane pane, double scaleFactor );
		
		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="GraphItem"/>.
		/// </summary>
		/// <param name="pt">The screen point, in pixels</param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
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
		/// <returns>true if the point lies in the bounding box, false otherwise</returns>
		abstract public bool PointInBox( PointF pt, GraphPane pane, Graphics g, double scaleFactor );		
	#endregion
	
	}
}
