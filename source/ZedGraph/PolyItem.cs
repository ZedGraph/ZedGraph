//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005  John Champion
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
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// A class that represents a bordered and/or filled polygon object on
	/// the graph.  A list of <see cref="PolyItem"/> objects is maintained by
	/// the <see cref="GraphItemList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.2 $ $Date: 2006-04-22 08:43:17 $ </version>
	[Serializable]
	public class PolyItem : BoxItem, ICloneable, ISerializable
	{

	#region Fields
		private PointF[] points;
	#endregion

	#region Properties

		/// <summary>
		/// Gets or sets the <see cref="PointF"/> array that defines
		/// the polygon.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		/// </summary>
		public PointF[] Points
		{
			get { return points; }
			set { points = value; }
		}

	#endregion
	
	#region Constructors
		/// <overloads>Constructors for the <see cref="PolyItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the position, border color, and solid fill color
		/// of the <see cref="PolyItem"/> to be pre-specified.
		/// </summary>
		/// <param name="borderColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box border</param>
		/// <param name="fillColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box fill (will be a solid color fill)</param>
		/// <param name="points">The <see cref="PointF"/> array that defines
		/// the polygon.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		/// </param>
		public PolyItem( PointF[] points, Color borderColor, Color fillColor ) :
				base( new RectangleF(), borderColor, fillColor )
		{
			this.points = points;
		}

		/// <summary>
		/// A constructor that allows the position
		/// of the <see cref="PolyItem"/> to be pre-specified.  Other properties are defaulted.
		/// </summary>
		/// <param name="points">The <see cref="PointF"/> array that defines
		/// the polygon.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		/// </param>
		public PolyItem( PointF[] points ) :
			base( new RectangleF() )
		{
			this.points = points;
		}

		/// <summary>
		/// A default constructor that creates a <see cref="PolyItem"/> from an empty
		/// <see cref="PointF"/> array.  Other properties are defaulted.
		/// </summary>
		public PolyItem() : this( new PointF[0] )
		{
		}

		/// <summary>
		/// A constructor that allows the position, border color, and two-color
		/// gradient fill colors
		/// of the <see cref="PolyItem"/> to be pre-specified.
		/// </summary>
		/// <param name="borderColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box border</param>
		/// <param name="fillColor1">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the start of the box gradient fill</param>
		/// <param name="fillColor2">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the end of the box gradient fill</param>
		/// <param name="points">The <see cref="PointF"/> array that defines
		/// the polygon.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		/// </param>
		public PolyItem( PointF[] points, Color borderColor,
							Color fillColor1, Color fillColor2 ) :
				base( new RectangleF(), borderColor, fillColor1, fillColor2 )
		{
			this.points = points;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="PolyItem"/> object from which to copy</param>
		public PolyItem( PolyItem rhs ) : base( rhs )
		{
			rhs.points = (PointF[]) this.points.Clone();
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typesafe, deep-copy clone method.
		/// </summary>
		/// <returns>A new, independent copy of this class</returns>
		public new PolyItem Clone()
		{
			return new PolyItem( this );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema3 = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected PolyItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema3" );

			this.points = (PointF[]) info.GetValue( "points", typeof(PointF[]) );

		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );
			info.AddValue( "schema3", schema3 );

			info.AddValue( "points", this.points );
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Render this object to the specified <see cref="Graphics"/> device.
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
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, PaneBase pane, float scaleFactor )
		{
			if ( this.points != null && this.points.Length > 1 )
			{
				GraphicsPath path = MakePath( pane );

				// Fill or draw the symbol as required
				if ( this.fill.IsVisible)
				{
					Brush brush = this.Fill.MakeBrush( path.GetBounds() );
					g.FillPath( brush, path );
				}
				
				if ( this.border.IsVisible )
				{
					Pen pen = border.MakePen( pane.IsPenWidthScaled, scaleFactor );
					g.DrawPath( pen, path );
				}

				path.Dispose();
			}
		}
		
		internal GraphicsPath MakePath( PaneBase pane )
		{
			GraphicsPath path = new GraphicsPath();
			bool first = true;
			PointF lastPt = new PointF();

			foreach( PointF pt in this.points )
			{
				// Convert the coordinates from the user coordinate system
				// to the screen coordinate system
				// Offset the points by the location value
				PointF pixPt = Location.Transform( pane,
						new PointF( pt.X + this.location.X, pt.Y + this.location.Y),
						this.location.CoordinateFrame );

				if (	Math.Abs( pixPt.X ) < 100000 &&
						Math.Abs( pixPt.Y ) < 100000 )
				{
					if ( first )
						first = false;
					else
						path.AddLine( lastPt, pixPt );

					lastPt = pixPt;
				}
			}

			path.CloseFigure();

			return path;
		}

		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="PolyItem"/>.
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
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>true if the point lies in the bounding box, false otherwise</returns>
		override public bool PointInBox( PointF pt, PaneBase pane, Graphics g, float scaleFactor )
		{
			if ( this.points != null && this.points.Length > 1 )
			{
				GraphicsPath path = MakePath( pane );

				return path.IsVisible( pt );
			}
			else
				return false;
		}
		
	#endregion
	
	}
}
