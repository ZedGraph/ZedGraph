//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright © 2004  John Champion
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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// A class that encapsulates Border (frame) properties for an object.  The <see cref="Border"/> class
	/// is used in a variety of ZedGraph objects to handle the drawing of the Border around the object.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.16 $ $Date: 2006-10-19 04:40:14 $ </version>
	[Serializable]
	public class Border : ISerializable, ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the Border color.  Use the public
		/// property <see cref="Color"/> to access this value.
		/// </summary>
		private Color	_color;
		/// <summary>
		/// Private field that stores the pen width for this Border.  Use the public
		/// property <see cref="PenWidth"/> to access this value.
		/// </summary>
		private float	_penWidth;

		/// <summary>
		/// Private field that stores the amount of inflation to be done on the rectangle
		/// before rendering.  This allows the border to be inset or outset relative to
		/// the actual rectangle area.  Use the public property <see cref="InflateFactor"/>
		/// to access this value.
		/// </summary>
		private float	_inflateFactor;
		/// <summary>
		/// Private field that determines if the Border will be drawn.  The Border will only
		/// be drawn if this value is true.  Use the public property <see cref="IsVisible"/>
		/// for access to this value.
		/// </summary>
		private bool	_isVisible;
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
            /// The default pen width, in points (1/72 inch), for the Border.  See the <see cref="PenWidth"/>
            /// property.
			/// </summary>
			public static float PenWidth = 1.0F;

			/// <summary>
			/// The default value for <see cref="Border.InflateFactor"/>, in units of points (1/72 inch).
			/// </summary>
			/// <seealso cref="Border.InflateFactor"/>
			public static float InflateFactor = 0.0F;
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
			_isVisible = Default.IsVisible;
			_color = Default.Color;
			_penWidth = Default.PenWidth;
			_inflateFactor = Default.InflateFactor;
		}

		/// <summary>
		/// Constructor that specifies the visibility, color and penWidth of the Border.
		/// </summary>
		/// <param name="isVisible">Determines whether or not the Border will be drawn.</param>
		/// <param name="color">The color of the Border</param>
        /// <param name="penWidth">The width, in points (1/72 inch), for the Border.</param>
        public Border( bool isVisible, Color color, float penWidth )
		{
			_color = color.IsEmpty ? Default.Color : color;
			_penWidth = penWidth;
			_isVisible = isVisible;
			_inflateFactor = Default.InflateFactor;
		}

		/// <summary>
		/// Constructor that specifies the color and penWidth of the Border.
		/// </summary>
		/// <param name="color">The color of the Border</param>
        /// <param name="penWidth">The width, in points (1/72 inch), for the Border.</param>
        public Border( Color color, float penWidth ) :
				this( !color.IsEmpty, color, penWidth )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Border object from which to copy</param>
		public Border( Border rhs )
		{
			_color = rhs.Color;
			_penWidth = rhs.PenWidth;
			_isVisible = rhs.IsVisible;
			_inflateFactor = rhs._inflateFactor;
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
		public Border Clone()
		{
			return new Border( this );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 10;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected Border( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			_color = (Color) info.GetValue( "color", typeof(Color) );
			_penWidth = info.GetSingle( "penWidth" );
			_isVisible = info.GetBoolean( "isVisible" );
			_inflateFactor = info.GetSingle( "inflateFactor" );
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
			info.AddValue( "color", _color );
			info.AddValue( "penWidth", _penWidth );
			info.AddValue( "isVisible", _isVisible );
			info.AddValue( "inflateFactor", _inflateFactor );
		}
	#endregion
		
	#region Properties
		/// <summary>
		/// Determines the <see cref="System.Drawing.Color"/> of the <see cref="Pen"/> used to
		/// draw this Border.
		/// </summary>
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}
		/// <summary>
        /// Gets or sets the width, in points (1/72 inch), of the <see cref="Pen"/> used to draw this Border.
        /// </summary>
		public float PenWidth
		{
			get { return _penWidth; }
			set { _penWidth = value; }
		}
		/// <summary>
		/// Gets or sets the amount of inflation to be done on the rectangle
		/// before rendering.
		/// </summary>
		/// <remarks>This allows the border to be inset or outset relative to
		/// the actual rectangle area.
		/// </remarks>
		public float InflateFactor
		{
			get { return _inflateFactor; }
			set { _inflateFactor = value; }
		}
		/// <summary>
		/// Determines whether or not the Border will be drawn.  true to draw the Border,
		/// false otherwise
		/// </summary>
		public bool IsVisible
		{
			get { return _isVisible; }
			set { _isVisible = value; }
		}
	#endregion

	#region Methods
		/// <summary>
		/// Create a new <see cref="Pen"/> object from the properties of this
		/// <see cref="Border"/> object.
		/// </summary>
		/// <param name="isPenWidthScaled">
		/// Set to true to have the <see cref="Border"/> pen width scaled with the
		/// scaleFactor.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>A <see cref="Pen"/> object with the proper color and pen width.</returns>
		public Pen MakePen( bool isPenWidthScaled, float scaleFactor )
		{
			float scaledPenWidth = _penWidth;
			if ( isPenWidthScaled )
				scaledPenWidth = (float)(_penWidth * scaleFactor);
			
			return new Pen( _color, scaledPenWidth );
		}
				
		/// <summary>
		/// Draw the specified Border (<see cref="RectangleF"/>) using the properties of
		/// this <see cref="Border"/> object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="isPenWidthScaled">
		/// Set to true to have the <see cref="Border"/> pen width scaled with the
		/// scaleFactor.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="rect">A <see cref="RectangleF"/> struct to be drawn.</param>
		public void Draw( Graphics g, bool isPenWidthScaled, float scaleFactor, RectangleF rect )
		{
			// Need to use the RectangleF props since rounding it can cause the axisFrame to
			// not line up properly with the last tic mark
			if ( _isVisible )
			{
				RectangleF tRect = rect;

				float		scaledInflate = (float) ( _inflateFactor * scaleFactor );
				tRect.Inflate( scaledInflate, scaledInflate );

				using ( Pen pen = MakePen(isPenWidthScaled, scaleFactor) )
					g.DrawRectangle( pen, tRect.X, tRect.Y, tRect.Width, tRect.Height );
			}
		}

	#endregion
	}
}








