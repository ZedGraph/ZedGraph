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

#region Using directives

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A class that represents an image object on the graph.  A list of
	/// <see cref="GraphItem"/> objects is maintained by the <see cref="GraphItemList"/>
	/// collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.2 $ $Date: 2004-12-03 13:31:28 $ </version>
	public class ImageItem : GraphItem, ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the image.  Use the public property <see cref="Image"/>
		/// to access this value.
		/// </summary>
		private Image		image;
		/// <summary>
		/// Private field that determines if the image will be scaled to the output rectangle.
		/// </summary>
		/// <value>true to scale the image, false to draw the image unscaled, but clipped
		/// to the destination rectangle</value>
		private bool		isScaled;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="ImageItem"/> class.
		/// </summary>
		new public struct Default
		{
			// Default text item properties
			/// <summary>
			/// Default value for the <see cref="ImageItem"/>
			/// <see cref="ImageItem.IsScaled"/> property.
			/// </summary>
			public static bool IsScaled = true;
		}
	#endregion

	#region Properties
		/// <summary>
		/// The <see cref="System.Drawing.Image"/> object.
		/// </summary>
        /// <value> A <see cref="System.Drawing.Image"/> class reference. </value>
		public Image Image
		{
			get { return image; }
			set { image = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines if the image will be scaled to the
		/// output rectangle (see <see cref="Location"/>).
		/// </summary>
		/// <value>true to scale the image, false to draw the image unscaled, but clipped
		/// to the destination rectangle</value>
		public bool IsScaled
		{
			get { return isScaled; }
			set { isScaled = value; }
		}
	#endregion
	
	#region Constructors
		/// <overloads>Constructors for the <see cref="ImageItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the <see cref="System.Drawing.Image"/> and
		/// <see cref="RectangleF"/> location for the
		/// <see cref="ImageItem"/> to be pre-specified.
		/// </summary>
		/// <param name="image">A <see cref="System.Drawing.Image"/> class that defines
		/// the image</param>
		/// <param name="rect">A <see cref="RectangleF"/> struct that defines the
		/// image location, specifed in units based on the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		public ImageItem( Image image, RectangleF rect ) :
				this( image, rect.X, rect.Y, rect.Right, rect.Bottom )
		{
		}

		/// <overloads>Constructors for the <see cref="ImageItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the <see cref="System.Drawing.Image"/> and
		/// <see cref="RectangleF"/> location for the
		/// <see cref="ImageItem"/> to be pre-specified.
		/// </summary>
		/// <param name="image">A <see cref="System.Drawing.Image"/> class that defines
		/// the image</param>
		/// <param name="rect">A <see cref="RectangleF"/> struct that defines the
		/// image location, specifed in units based on the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="coordType">The <see cref="CoordType"/> enum value that
		/// indicates what type of coordinate system the x and y parameters are
		/// referenced to.</param>
		/// <param name="alignH">The <see cref="ZedGraph.AlignH"/> enum that specifies
		/// the horizontal alignment of the object with respect to the (x,y) location</param>
		/// <param name="alignV">The <see cref="ZedGraph.AlignV"/> enum that specifies
		/// the vertical alignment of the object with respect to the (x,y) location</param>
		public ImageItem( Image image, RectangleF rect, CoordType coordType,
					AlignH alignH, AlignV alignV ) :
				base( rect.X, rect.Y, rect.Right, rect.Bottom, coordType, alignH, alignV )
		{
			this.image = image;
			isScaled = Default.IsScaled;
		}

		/// <overloads>Constructors for the <see cref="ImageItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the <see cref="System.Drawing.Image"/> and
		/// individual <see cref="System.Single"/> coordinate locations for the
		/// <see cref="ImageItem"/> to be pre-specified.
		/// </summary>
		/// <param name="image">A <see cref="System.Drawing.Image"/> class that defines
		/// the image</param>
		/// <param name="left">The position of the left side of the rectangle that defines the
		/// <see cref="ImageItem"/> location.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="top">The position of the top side of the rectangle that defines the
		/// <see cref="ImageItem"/> location.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="right">The position of the right side of the rectangle that defines the
		/// <see cref="ImageItem"/> location.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="bottom">The position of the bottom side of the rectangle that defines the
		/// <see cref="ImageItem"/> location.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		public ImageItem( Image image, float left, float top, float right, float bottom ) :
				base( left, top, right, bottom )
		{
			this.image = image;
			isScaled = Default.IsScaled;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ImageItem"/> object from which to copy</param>
		public ImageItem( ImageItem rhs ) : base( rhs )
		{
			image = rhs.image;
			isScaled = rhs.IsScaled;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="ImageItem"/></returns>
		override public object Clone()
		{ 
			return new ImageItem( this ); 
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Render this object to the specified <see cref="Graphics"/> device
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphItemList"/> collection object.
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
		override public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			if ( this.image != null )
			{
				// Convert the rectangle coordinates from the user coordinate system
				// to the screen coordinate system
				RectangleF tmpRect = this.location.TransformRect( pane );

				if ( isScaled )
					g.DrawImage( this.image, tmpRect );
				else
				{
					Region clip = g.Clip;
					g.SetClip( tmpRect );
					g.DrawImageUnscaled( image, Rectangle.Round( tmpRect ) );
					g.SetClip( clip, CombineMode.Replace );
					//g.DrawImageUnscaledAndClipped( image, Rectangle.Round( tmpRect ) );
				}
			}

		}
		
		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="ArrowItem"/>.  The bounding box is calculated assuming a distance
		/// of <see cref="GraphPane.Default.NearestTol"/> pixels around the arrow segment.
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
			if ( this.image != null )
			{
				// transform the x,y location from the user-defined
				// coordinate frame to the screen pixel location
				RectangleF tmpRect = this.location.TransformRect( pane );

				return tmpRect.Contains( pt );
			}
			else
				return false;
		}
		
	#endregion
	}
}
