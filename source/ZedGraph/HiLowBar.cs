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

#endregion

namespace ZedGraph
{
	/// <summary>
	/// This class handles the drawing of the curve <see cref="HiLowBar"/> objects.
	/// The Hi-Low Bars are the "floating" bars that have a lower and upper value and
	/// appear at each defined point.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2004-11-17 03:35:39 $ </version>
	public class HiLowBar : Bar, ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the size (width) of this
        /// <see cref="HiLowBar"/> in points (1/72 inch).  Use the public
        /// property <see cref="Size"/> to access this value.
		/// </summary>
		private float		size;
	#endregion

	#region Properties
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="ZedGraph.HiLowBar"/> class.
		/// </summary>
		new public struct Default
		{
			// Default HiLowBar properties
			/// <summary>
			/// The default size (width) for the bars (<see cref="HiLowBar.Size"/> property),
			/// in units of points.
			/// </summary>
			public static float Size = 7;
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Default constructor that sets all <see cref="HiLowBar"/> properties to default
		/// values as defined in the <see cref="Bar.Default"/> class.
		/// </summary>
		public HiLowBar() : this( Color.Empty )
		{
		}

		/// <summary>
		/// Default constructor that sets the 
		/// <see cref="Color"/> as specified, and the remaining
		/// <see cref="HiLowBar"/> properties to default
		/// values as defined in the <see cref="Bar.Default"/> class.
		/// The specified color is only applied to the
		/// <see cref="ZedGraph.Fill.Color"/>, and the <see cref="ZedGraph.Border.Color"/>
		/// will be defaulted.
		/// </summary>
		/// <param name="color">A <see cref="Color"/> value indicating
		/// the <see cref="ZedGraph.Fill.Color"/>
		/// of the Bar.
		/// </param>
		public HiLowBar( Color color ) : this( color, Default.Size )
		{
		}

		/// <summary>
		/// Default constructor that sets the 
		/// <see cref="Color"/> and <see cref="Size"/> as specified, and the remaining
		/// <see cref="HiLowBar"/> properties to default
		/// values as defined in the <see cref="Bar.Default"/> class.
		/// The specified color is only applied to the
		/// <see cref="ZedGraph.Fill.Color"/>, and the <see cref="ZedGraph.Border.Color"/>
		/// will be defaulted.
		/// </summary>
		/// <param name="color">A <see cref="Color"/> value indicating
		/// the <see cref="ZedGraph.Fill.Color"/>
		/// of the Bar.
		/// </param>
		/// <param name="size">The size (width) of the <see cref="HiLowBar"/>'s, in points
		/// (1/72nd inch)</param>
		public HiLowBar( Color color, float size ) : base( color )
		{
			this.size = size;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="HiLowBar"/> object from which to copy</param>
		public HiLowBar( HiLowBar rhs ) : base( rhs )
		{
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="HiLowBar"/></returns>
		new public object Clone()
		{ 
			return new HiLowBar( this ); 
		}
	#endregion

	#region Properties
		/// <summary>
		/// Gets or sets the size of the <see cref="HiLowBar"/>
		/// </summary>
        /// <value>Size in points (1/72 inch)</value>
        /// <seealso cref="Default.Size"/>
		public float Size
		{
			get { return size; }
			set { size = value; }
		}
	#endregion

	#region Methods
		/// <summary>
		/// Protected internal routine that draws the specified single bar (an individual "point")
		/// of this series to the specified <see cref="Graphics"/> device.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="points">A <see cref="PointPairList"/> of point values representing this
		/// <see cref="Bar"/>.</param>
		/// <param name="index">
		/// The zero-based index number for the single bar to be drawn.
		/// </param>
		/// <param name="pos">
		/// The ordinal position of the this bar series (0=first bar, 1=second bar, etc.)
		/// in the cluster of bars.
		/// </param>
		/// <param name="baseAxis">The <see cref="Axis"/> class instance that defines the base (independent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="valueAxis">The <see cref="Axis"/> class instance that defines the value (dependent)
		/// axis for the <see cref="Bar"/></param>
		/// <param name="barWidth">
		/// The width of each bar, in pixels.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override protected void DrawSingleBar( Graphics g, GraphPane pane, PointPairList points,
							int index, int pos, Axis baseAxis, Axis valueAxis,
							float barWidth, double scaleFactor )
		{
			float	scaledSize = (float) ( this.size * scaleFactor );

			// pixBase = pixel value for the bar center on the base axis
			// pixValue = pixel value for the bar top on the value axis
			// pixLow = pixel value for the bar bottom on the value axis
			float pixBase, pixValue, pixLow;

			// curBase = the scale value on the base axis of the current bar
			// curValue = the scale value on the value axis of the current bar
			double curBase = ( baseAxis is XAxis ) ? points[index].X : points[index].Y;
			double curValue = ( baseAxis is XAxis ) ? points[index].Y : points[index].X;

			// curLow = the scale value on the value axis for the bottom of the current bar
			// Get a "low" value for the bottom of the bar and verify validity
			double curLow = points[index].LowValue;

			if (	curLow == PointPair.Missing ||
					System.Double.IsNaN( curLow ) ||
					System.Double.IsInfinity( curLow ) )
				curLow = 0;

			// Any value set to double max is invalid and should be skipped
			// This is used for calculated values that are out of range, divide
			//   by zero, etc.
			// Also, any value <= zero on a log scale is invalid

			if ( !points[index].IsInvalid )
			{
				// calculate a pixel value for the top of the bar on value axis
				pixValue = valueAxis.Transform( index, curValue );
				// calculate a pixel value for the center of the bar on the base axis
				pixBase = baseAxis.Transform( index, curBase );

				pixLow = valueAxis.Transform( index, curLow );

				// Calculate the pixel location for the side of the bar (on the base axis)
				float pixSide = pixBase - scaledSize / 2.0F;

				// Draw the bar
				if ( baseAxis is XAxis )
					this.Draw( g, pane, pixSide, pixSide + scaledSize, pixLow,
								pixValue, scaleFactor, true );
				else
					this.Draw( g, pane, pixLow, pixValue, pixSide, pixSide + scaledSize,
								scaleFactor, true );
		   }
	   }
	#endregion

	}
}
