//============================================================================
//PointPair Class
//Copyright (C) 2004  Jerry Vos
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
using IComparer	= System.Collections.IComparer;

namespace ZedGraph
{
	/// <summary>
	/// A simple point represented by an (X,Y) pair of
	/// double values.
	/// </summary>
	/// 
	/// <author> John Champion from PointPair code by Jerry Vos</author>
	/// <version> $Revision: 3.1 $ $Date: 2004-10-22 23:50:42 $ </version>
	public struct PointTrio
	{
	#region Member variables
		/// <summary>
		/// This PointTrio's X coordinate
		/// </summary>
		public double X;

		/// <summary>
		/// This PointTrio's first Y coordinate
		/// </summary>
		public double Y1;

		/// <summary>
		/// This PointTrio's second Y coordinate
		/// </summary>
		public double Y2;
	#endregion

	#region Constructors
		/// <summary>
		/// Creates a point trio with the specified X, Y1, and Y2.
		/// </summary>
		/// <param name="x">This pair's x coordinate.</param>
		/// <param name="y1">This pair's y1 coordinate.</param>
		/// <param name="y2">This pair's y2 coordinate.</param>
		public PointTrio( double x, double y1, double y2 )
		{
			this.X = x;
			this.Y1 = y1;
			this.Y2 = y2;
		}

		/// <summary>
		/// The PointTrio copy constructor.
		/// </summary>
		/// <param name="rhs">The basis for the copy.</param>
		public PointTrio( PointTrio rhs )
		{
			this.x = rhs.X;
			this.Y1 = rhs.Y1;
			this.Y2 = rhs.Y2;
		}
	#endregion

	#region Properties
		/// <summary>
		/// Readonly value that determines if the X, the Y1, or the Y2
		/// coordinate in this PointTrio is a missing value.
		/// </summary>
		/// <returns>true if any value is missing</returns>
		public bool IsMissing
		{
			get { return this.X == PointPair.Missing || this.Y1 == PointPair.Missing ||
					this.Y2 == PointPair.Missing; }
		}

		/// <summary>
		/// Readonly value that determines if either the X, Y1, or Y2
		/// coordinate in this PointTrio is an invalid (not plotable) value.
		/// It is considered invalid if it is missing (equal to System.Double.Max),
		/// Infinity, or NaN.
		/// </summary>
		/// <returns>true if either value is invalid</returns>
		public bool IsInvalid
		{
			get { return	this.X == PointPair.Missing ||
							this.Y1 == PointPair.Missing ||
							this.Y2 == PointPair.Missing ||
							Double.IsInfinity( this.X ) ||
							Double.IsInfinity( this.Y1 ) ||
							Double.IsInfinity( this.Y2 ) ||
							Double.IsNaN( this.X ) ||
							Double.IsNaN( this.Y1 ) ||
							Double.IsNaN( this.Y2 );
				}
		}
		
		/// <summary>
		/// Readonly property that returns a <see cref="PointPair"/> struct corresponding
		/// to the (X,Y1) values of this <see cref="PointTrio"/>.
		/// </summary>
		/// <returns>The first <see cref="PointPair"/> struct.</returns>
		public PointPair PointPair1
		{
			get { return new PointPair( this.X, this.Y1 ); }
		}
		/// <summary>
		/// Readonly property that returns a <see cref="PointPair"/> struct corresponding
		/// to the (X,Y2) values of this <see cref="PointTrio"/>.
		/// </summary>
		/// <returns>The second <see cref="PointPair"/> struct.</returns>
		public PointPair PointPair2
		{
			get { return new PointPair( this.X, this.Y2 ); }
		}
		
		/// <summary>
		/// Readonly property that returns a <see cref="PointF"/> struct corresponding
		/// to the (X,Y1) values of this <see cref="PointTrio"/>.  This routine can
		/// result in data loss since it reduces precision from <see cref="System.Double"/>
		/// to <see cref="System.Single"/>.
		/// </summary>
		/// <returns>The first <see cref="PointF"/> struct.</returns>
		public PointF Point1
		{
			get { return new PointF( (float) this.X, (float) this.Y1 ); }
		}
		/// <summary>
		/// Readonly property that returns a <see cref="PointF"/> struct corresponding
		/// to the (X,Y2) values of this <see cref="PointTrio"/>.  This routine can
		/// result in data loss since it reduces precision from <see cref="System.Double"/>
		/// to <see cref="System.Single"/>.
		/// </summary>
		/// <returns>The second <see cref="PointF"/> struct.</returns>
		public PointF Point2
		{
			get { return new PointF( (float) this.X, (float) this.Y2 ); }
		}
		
	#endregion

	#region Inner classes
		/// <summary>
		/// Compares points based on their x values.  Is setup to be used in an
		/// ascending order sort.
		/// <seealso cref="System.Collections.ArrayList.Sort()"/>
		/// <seealso cref="PointPairList.Sort()"/>
		/// </summary>
		public class PointTrioComparer : IComparer 
		{
		
			/// <summary>
			/// Compares two <see cref="PointTrio"/>s.
			/// </summary>
			/// <param name="l">Point to the left.</param>
			/// <param name="r">Point to the right.</param>
			/// <returns>-1, 0, or 1 depending on l.X's relation to r.X</returns>
			int IComparer.Compare( object l, object r ) 
			{
				if (l == null && r == null) 
				{
					return 0;
				} 
				else if (l == null && r != null) 
				{
					return -1;
				} 
				else if (l != null && r == null) 
				{
					return 1;
				} 

				double lX = ((PointTrio) l).X;
				double rX = ((PointTrio) r).X;

				if (System.Math.Abs(lX - rX) < .000000001)
					return 0;
				
				return lX < rX ? -1 : 1;
			}
		}
	#endregion

	#region Methods
		/// <summary>
		/// Format this PointTrio value using the default format.  Example:  "( 12.345, -16.876, 6.789 )".
		/// The three double values are formatted with the "g" format type.
		/// </summary>
		/// <returns>A string representation of the PointTrio</returns>
		public override string ToString()
		{
			return this.ToString( "G" );
		}

		/// <summary>
		/// Format this PointTrio value using a general format string.
		/// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001, 6.79e+000 )".
		/// </summary>
		/// <param name="format">A format string that will be used to format each of
		/// the three double type values (see <see cref="System.Double.ToString"/>).</param>
		/// <returns>A string representation of the PointTrio</returns>
		public string ToString( string format )
		{
			return "( " + this.X.ToString( format ) + ", " +
					this.Y1.ToString( format ) + ", " +
					this.Y2.ToString( format ) + " )";
		}
	#endregion
	}
}
