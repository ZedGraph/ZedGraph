//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright © 2006  John Champion
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
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
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// This class handles the drawing of the curve <see cref="CandleStick"/> objects.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.1.2.5 $ $Date: 2006-04-27 06:50:11 $ </version>
	[Serializable]
	public class CandleStick : ICloneable, ISerializable
	{
	#region Fields

		/// <summary>
		/// Private field that stores the visibility of the <see cref="CandleStick"/> open and
		/// close line segments ("wings").  Use the public
		/// property <see cref="IsOpenCloseVisible"/> to access this value.  If this value is
		/// false, the wings will not be shown.
		/// </summary>
		protected bool _isOpenCloseVisible;
		/// <summary>
		/// Private field that stores the CandleStick color.  Use the public
		/// property <see cref="Color"/> to access this value.
		/// </summary>
		protected Color _color;
		/// <summary>
		/// Private field that stores the pen width for this CandleStick.  Use the public
		/// property <see cref="PenWidth"/> to access this value.
		/// </summary>
		protected float _penWidth;
		/// <summary>
		/// Private field that stores the total width for the Opening/Closing line
		/// segments.  Use the public property <see cref="Size"/> to access this value.
		/// </summary>
		protected float _size;

	#endregion

	#region Defaults

		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="ZedGraph.CandleStick"/> class.
		/// </summary>
		public struct Default
		{
			// Default Symbol properties
			/// <summary>
			/// The default width for the candlesticks (see <see cref="CandleStick.Size" />),
			/// in units of points.
			/// </summary>
			public static float Size = 6;
			/// <summary>
			/// The default pen width to be used for drawing candlesticks
			/// (<see cref="CandleStick.PenWidth"/> property).  Units are points.
			/// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default display mode for symbols (<see cref="CandleStick.IsOpenCloseVisible"/> property).
			/// true to display symbols, false to hide them.
			/// </summary>
			public static bool IsOpenCloseVisible = true;
			/// <summary>
			/// The default color for drawing CandleSticks (<see cref="CandleStick.Color"/> property).
			/// </summary>
			public static Color Color = Color.Black;
		}

	#endregion

	#region Properties

		/// <summary>
		/// Gets or sets a property that shows or hides the <see cref="CandleStick"/> open/close "wings".
		/// </summary>
		/// <value>true to show the CandleStick wings, false to hide them</value>
		/// <seealso cref="Default.IsOpenCloseVisible"/>
		public bool IsOpenCloseVisible
		{
			get { return _isOpenCloseVisible; }
			set { _isOpenCloseVisible = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.Color"/> data for this
		/// <see cref="CandleStick"/>.
		/// </summary>
		/// <remarks>This property only controls the color of
		/// the vertical line.  The symbol color is controlled separately in
		/// the <see cref="Symbol"/> property.
		/// </remarks>
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}
		/// <summary>
		/// The pen width to be used for drawing <see cref="CandleStick" /> items.
		/// Units are points.
		/// </summary>
		/// <remarks>This property only controls the pen width for the
		/// vertical line.  The pen width for the symbol outline is
		/// controlled separately by the <see cref="Symbol"/> property.
		/// </remarks>
		public float PenWidth
		{
			get { return _penWidth; }
			set { _penWidth = value; }
		}
		/// <summary>
		/// The total width to be used for drawing the opening/closing line segments ("wings")
		/// of the <see cref="CandleStick" /> items.
		/// Units are points.
		/// </summary>
		public float Size
		{
			get { return _size; }
			set { _size = value; }
		}

	#endregion

	#region Constructors

		/// <summary>
		/// Default constructor that sets all <see cref="CandleStick"/> properties to
		/// default values as defined in the <see cref="Default"/> class.
		/// </summary>
		public CandleStick()
			: this( Default.Color )
		{
		}

		/// <summary>
		/// Default constructor that sets the
		/// <see cref="Color"/> as specified, and the remaining
		/// <see cref="CandleStick"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="color">A <see cref="Color"/> value indicating
		/// the color of the symbol
		/// </param>
		public CandleStick( Color color )
		{
			_size = Default.Size;
			_color = color;
			_penWidth = Default.PenWidth;
			_isOpenCloseVisible = Default.IsOpenCloseVisible;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="CandleStick"/> object from which to copy</param>
		public CandleStick( CandleStick rhs )
		{
			_color = rhs._color;
			_isOpenCloseVisible = rhs._isOpenCloseVisible;
			_penWidth = rhs._penWidth;
			_size = rhs._size;
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
		public CandleStick Clone()
		{
			return new CandleStick( this );
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
		protected CandleStick( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			_isOpenCloseVisible = info.GetBoolean( "isOpenCloseVisible" );
			_color = (Color)info.GetValue( "color", typeof( Color ) );
			_penWidth = info.GetSingle( "penWidth" );
			_size = info.GetSingle( "size" );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute( SecurityAction.Demand, SerializationFormatter = true )]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );
			info.AddValue( "isOpenCloseVisible", _isOpenCloseVisible );
			info.AddValue( "color", _color );
			info.AddValue( "penWidth", _penWidth );
			info.AddValue( "size", _size );
		}

	#endregion

	#region Rendering Methods

		/// <summary>
		/// Draw the <see cref="CandleStick"/> to the specified <see cref="Graphics"/>
		/// device at the specified location.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="isXBase">boolean value that indicates if the "base" axis for this
		/// <see cref="CandleStick"/> is the X axis.  True for an <see cref="XAxis"/> base,
		/// false for a <see cref="YAxis"/> or <see cref="Y2Axis"/> base.</param>
		/// <param name="pixBase">The independent axis position of the center of the candlestick in
		/// pixel units</param>
		/// <param name="pixHigh">The dependent axis position of the top of the candlestick in
		/// pixel units</param>
		/// <param name="pixLow">The dependent axis position of the bottom of the candlestick in
		/// pixel units</param>
		/// <param name="pixOpen">The dependent axis position of the opening value of the candlestick in
		/// pixel units</param>
		/// <param name="pixClose">The dependent axis position of the closing value of the candlestick in
		/// pixel units</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.</param>
		/// <param name="pen">A pen with attributes of <see cref="Color"/> and
		/// <see cref="PenWidth"/> for this <see cref="CandleStick"/></param>
		public void Draw( Graphics g, GraphPane pane, bool isXBase,
								float pixBase, float pixHigh, float pixLow,
								float pixOpen, float pixClose,
								float scaleFactor, Pen pen  )
		{
			float halfSize = _size * scaleFactor;

			if ( pixBase != PointPair.Missing )
			{
				if ( isXBase )
				{
					if ( Math.Abs( pixLow ) < 1000000 && Math.Abs( pixHigh ) < 1000000 )
						g.DrawLine( pen, pixBase, pixHigh, pixBase, pixLow );
					if ( _isOpenCloseVisible && Math.Abs( pixOpen ) < 1000000 )
						g.DrawLine( pen, pixBase - halfSize, pixOpen, pixBase, pixOpen );
					if ( _isOpenCloseVisible && Math.Abs( pixClose ) < 1000000 )
						g.DrawLine( pen, pixBase, pixClose, pixBase + halfSize, pixClose );
				}
				else
				{
					if ( Math.Abs( pixLow ) < 1000000 && Math.Abs( pixHigh ) < 1000000 )
						g.DrawLine( pen, pixHigh, pixBase, pixLow, pixBase );
					if ( _isOpenCloseVisible && Math.Abs( pixOpen ) < 1000000 )
						g.DrawLine( pen, pixOpen, pixBase - halfSize, pixOpen, pixBase );
					if ( _isOpenCloseVisible && Math.Abs( pixClose ) < 1000000 )
						g.DrawLine( pen, pixClose, pixBase, pixClose, pixBase + halfSize );
				}
			}
		}


		/// <summary>
		/// Draw all the <see cref="CandleStick"/>'s to the specified <see cref="Graphics"/>
		/// device as a candlestick at each defined point.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="curve">A <see cref="CandleStickItem"/> object representing the
		/// <see cref="CandleStick"/>'s to be drawn.</param>
		/// <param name="baseAxis">The <see cref="Axis"/> class instance that defines the base (independent)
		/// axis for the <see cref="CandleStick"/></param>
		/// <param name="valueAxis">The <see cref="Axis"/> class instance that defines the value (dependent)
		/// axis for the <see cref="CandleStick"/></param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, CandleStickItem curve,
							Axis baseAxis, Axis valueAxis, float scaleFactor )
		{
			//ValueHandler valueHandler = new ValueHandler( pane, false );

			float pixBase, pixHigh, pixLow, pixOpen, pixClose;

			if ( curve.Points != null )
			{
				Pen pen = new Pen( _color, _penWidth );
				float halfSize = _size * scaleFactor;

				// Loop over each defined point							
				for ( int i = 0; i < curve.Points.Count; i++ )
				{
					PointPair pt = curve.Points[i];
					double date = pt.X;
					double high = pt.Y;
					double low = pt.Z;
					double open = PointPair.Missing;
					double close = PointPair.Missing;
					if ( pt is StockPt )
					{
						open = ( pt as StockPt ).Open;
						close = ( pt as StockPt ).Close;
					}

					// Any value set to double max is invalid and should be skipped
					// This is used for calculated values that are out of range, divide
					//   by zero, etc.
					// Also, any value <= zero on a log scale is invalid

					if ( !curve.Points[i].IsInvalid3D &&
							( date > 0 || !baseAxis._scale.IsLog ) &&
							( ( high > 0 && low > 0 ) || !valueAxis._scale.IsLog ) )
					{
						pixBase = baseAxis.Scale.Transform( curve.IsOverrideOrdinal, i, date );
						pixHigh = valueAxis.Scale.Transform( curve.IsOverrideOrdinal, i, high );
						pixLow = valueAxis.Scale.Transform( curve.IsOverrideOrdinal, i, low );
						if ( PointPair.IsValueInvalid( open ) )
							pixOpen = Single.MaxValue;
						else
							pixOpen = valueAxis.Scale.Transform( curve.IsOverrideOrdinal, i, open );

						if ( PointPair.IsValueInvalid( close ) )
							pixClose = Single.MaxValue;
						else
							pixClose = valueAxis.Scale.Transform( curve.IsOverrideOrdinal, i, close );

						Draw( g, pane, baseAxis is XAxis, pixBase, pixHigh, pixLow, pixOpen,
								pixClose, scaleFactor, pen );
					}
				}
			}
		}

	#endregion

	}
}
