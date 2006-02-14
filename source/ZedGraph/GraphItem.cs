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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// An abstract base class that represents a text object on the graph.  A list of
	/// <see cref="GraphItem"/> objects is maintained by the
	/// <see cref="GraphItemList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.15 $ $Date: 2006-02-14 06:14:22 $ </version>
	[Serializable]
	abstract public class GraphItem : ISerializable, ICloneable
	{
	#region Fields
		/// <summary>
		/// Protected field that stores the location of this <see cref="GraphItem"/>.
		/// Use the public property <see cref="Location"/> to access this value.
		/// </summary>
		protected Location location;

		/// <summary>
		/// Protected field that determines whether or not this <see cref="GraphItem"/>
		/// is visible in the graph.  Use the public property <see cref="IsVisible"/> to
		/// access this value.
		/// </summary>
		protected bool isVisible;
		
		/// <summary>
		/// Protected field that determines whether or not the rendering of this <see cref="GraphItem"/>
		/// will be clipped to the AxisRect.  Use the public property <see cref="IsClippedToAxisRect"/> to
		/// access this value.
		/// </summary>
		protected bool isClippedToAxisRect;
		
		/// <summary>
		/// A tag object for use by the user.  This can be used to store additional
		/// information associated with the <see cref="GraphItem"/>.  ZedGraph does
		/// not use this value for any purpose.
		/// </summary>
		/// <remarks>
		/// Note that, if you are going to Serialize ZedGraph data, then any type
		/// that you store in <see cref="Tag"/> must be a serializable type (or
		/// it will cause an exception).
		/// </remarks>
		public object Tag;

		/// <summary>
		/// Protected field that determines the z-order "depth" of this
		/// item relative to other graphic objects.  Use the public property
		/// <see cref="ZOrder"/> to access this value.
		/// </summary>
		protected ZOrder zOrder;

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
			/// <summary>
			/// The default value for <see cref="GraphItem.IsClippedToAxisRect"/>.
			/// </summary>
			public static bool IsClippedToAxisRect = false;
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

		/// <summary>
		/// Gets or sets a value that determines the z-order "depth" of this
		/// item relative to other graphic objects.
		/// </summary>
		/// <remarks>Note that this controls the z-order with respect to
		/// other elements such as <see cref="CurveItem"/>'s, <see cref="Axis"/>
		/// objects, etc.  The order of <see cref="GraphItem"/> objects having
		/// the same <see cref="ZedGraph.ZOrder"/> value is controlled by their order in
		/// the <see cref="GraphItemList"/>.  The first <see cref="GraphItem"/>
		/// in the list is drawn in front of other <see cref="GraphItem"/>
		/// objects having the same <see cref="ZedGraph.ZOrder"/> value.</remarks>
		public ZOrder ZOrder
		{
			get { return zOrder; }
			set { zOrder = value; }
		}
		
		/// <summary>
		/// Gets or sets a value that determines if this <see cref="GraphItem"/> will be
		/// visible in the graph.  true displays the item, false hides it.
		/// </summary>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not the rendering of this <see cref="GraphItem"/>
		/// will be clipped to the <see cref="GraphPane.AxisRect"/>.
		/// </summary>
		/// <value>true to clip the <see cref="GraphItem"/> to the <see cref="GraphPane.AxisRect"/> bounds,
		/// false to leave it unclipped.</value>
		public bool IsClippedToAxisRect
		{
			get { return isClippedToAxisRect; }
			set { isClippedToAxisRect = value; }
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
			this.isVisible = true;
			this.isClippedToAxisRect = Default.IsClippedToAxisRect;
			this.Tag = null;
			this.zOrder = ZOrder.A_InFront;
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
			this.isVisible = true;
			this.isClippedToAxisRect = Default.IsClippedToAxisRect;
			this.Tag = null;
			this.zOrder = ZOrder.A_InFront;
			this.location = new Location( x, y, x2, y2, coordType, alignH, alignV );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="GraphItem"/> object from which to copy</param>
		public GraphItem( GraphItem rhs )
		{
			// Copy value types
			this.isVisible = rhs.IsVisible;
			this.isClippedToAxisRect = rhs.isClippedToAxisRect;
			this.zOrder = rhs.ZOrder;

			// copy reference types by cloning
			if ( rhs.Tag is ICloneable )
				this.Tag = ((ICloneable) rhs.Tag).Clone();
			else
				this.Tag = rhs.Tag;

			this.location = rhs.Location.Clone();
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <remarks>
		/// Note that this method must be called with an explicit cast to ICloneable, and
		/// that it is inherently virtual.  For example:
		/// <code>
		/// ParentClass foo = new ChildClass();
		/// ChildClass bar = (ChildClass) ((ICloneable)foo).Clone();
		/// </code>
		/// Assume that ChildClass is inherited from ParentClass.  Even though foo is declared with
		/// ParentClass, it is actually an instance of ChildClass.  Calling the ICloneable implementation
		/// of Clone() on foo actually calls ChildClass.Clone() as if it were a virtual function.
		/// </remarks>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			throw new NotImplementedException( "Can't clone an abstract base type -- child types must implement ICloneable" );
			//return new PaneBase( this );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		/// <remarks>
		/// schema changed to 2 when isClippedToAxisRect was added.
		/// </remarks>
		public const int schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected GraphItem( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			location = (Location) info.GetValue( "location", typeof(Location) );
			isVisible = info.GetBoolean( "isVisible" );
			Tag = info.GetValue( "Tag", typeof(object) );
			zOrder = (ZOrder) info.GetValue( "zOrder", typeof(ZOrder) );

			if ( sch >= 2 )
				isClippedToAxisRect = info.GetBoolean( "isClippedToAxisRect" );
			else
				isClippedToAxisRect = Default.IsClippedToAxisRect;
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );
			info.AddValue( "location", location );
			info.AddValue( "isVisible", isVisible );
			info.AddValue( "Tag", Tag );
			info.AddValue( "zOrder", zOrder );

			info.AddValue( "isClippedToAxisRect", isClippedToAxisRect );
		}
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
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="PaneBase"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		abstract public void Draw( Graphics g, PaneBase pane, float scaleFactor );
		
		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="GraphItem"/>.
		/// </summary>
		/// <param name="pt">The screen point, in pixels</param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="PaneBase"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>true if the point lies in the bounding box, false otherwise</returns>
		abstract public bool PointInBox( PointF pt, PaneBase pane, Graphics g, float scaleFactor );		
	#endregion
	
	}
}
