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
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A class that represents a bordered and/or filled box (rectangle) object on
	/// the graph.  A list of
	/// BoxItem objects is maintained by the <see cref="GraphItemList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2004-12-03 13:31:28 $ </version>
	public class BoxItem : GraphItem, ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="BoxItem"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		protected Fill fill;
		/// <summary>
		/// Private field that determines the properties of the border around this
		/// <see cref="BoxItem"/>
		/// Use the public property <see cref="Border"/> to access this value.
		/// </summary>
		protected Border border;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="ArrowItem"/> class.
		/// </summary>
		new public struct Default
		{
			/// <summary>
			/// The default pen width used for the <see cref="BoxItem"/> border
            /// (<see cref="ZedGraph.Border.PenWidth"/> property).  Units are points (1/72 inch).
            /// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default color used for the <see cref="BoxItem"/> border
			/// (<see cref="ZedGraph.Border.Color"/> property).
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color used for the <see cref="BoxItem"/> fill
			/// (<see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FillColor = Color.White;
		}
	#endregion

	#region Properties
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="BoxItem"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> object, which
		/// determines the properties of the border around this
		/// <see cref="BoxItem"/>
		/// </summary>
		public Border Border
		{
			get { return border; }
			set { border = value; }
		}
	#endregion
	
	#region Constructors
		/// <overloads>Constructors for the <see cref="BoxItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the position, border color, and solid fill color
		/// of the <see cref="BoxItem"/> to be pre-specified.
		/// </summary>
		/// <param name="borderColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box border</param>
		/// <param name="fillColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box fill (will be a solid color fill)</param>
		/// <param name="rect"/>The <see cref="RectangleF"/> struct that defines
		/// the box.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		public BoxItem( RectangleF rect, Color borderColor, Color fillColor ) :
				base( rect.Left, rect.Top, rect.Left+rect.Width, rect.Top+rect.Height )
		{
			this.Border = new Border( borderColor, Default.PenWidth );
			this.Fill = new Fill( fillColor );
		}

		/// <summary>
		/// A constructor that allows the position, border color, and two-color
		/// gradient fill colors
		/// of the <see cref="BoxItem"/> to be pre-specified.
		/// </summary>
		/// <param name="borderColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box border</param>
		/// <param name="fillColor1">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the start of the box gradient fill</param>
		/// <param name="fillColor2">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the end of the box gradient fill</param>
		/// <param name="rect"/>The <see cref="RectangleF"/> struct that defines
		/// the box.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		public BoxItem( RectangleF rect, Color borderColor,
							Color fillColor1, Color fillColor2 ) :
				base( rect.Left, rect.Top, rect.Left+rect.Width, rect.Top+rect.Height )
		{
			this.Border = new Border( borderColor, Default.PenWidth );
			this.Fill = new Fill( fillColor1, fillColor2 );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="BoxItem"/> object from which to copy</param>
		public BoxItem( BoxItem rhs ) : base( rhs )
		{
			this.Border = (Border) rhs.Border.Clone();
			this.Fill = (Fill) rhs.Fill.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="BoxItem"/></returns>
		override public object Clone()
		{ 
			return new BoxItem( this ); 
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
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			// Convert the arrow coordinates from the user coordinate system
			// to the screen coordinate system
			RectangleF pixRect = this.Location.TransformRect( pane );

			if (	Math.Abs( pixRect.Left ) < 100000 &&
					Math.Abs( pixRect.Top ) < 100000 &&
					Math.Abs( pixRect.Right ) < 100000 &&
					Math.Abs( pixRect.Bottom ) < 100000 )
			{
				// If the box is to be filled, fill it
				this.fill.Draw( g, pixRect );
				
				// Draw the border around the box if required
				this.border.Draw( g, pane, scaleFactor, pixRect );
			}
		}
		
		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="BoxItem"/>.
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
		override public bool PointInBox( PointF pt, GraphPane pane, Graphics g, double scaleFactor )
		{
			// transform the x,y location from the user-defined
			// coordinate frame to the screen pixel location
			RectangleF pixRect = this.location.TransformRect( pane );

			return pixRect.Contains( pt );
		}
		
	#endregion
	
	}
}
