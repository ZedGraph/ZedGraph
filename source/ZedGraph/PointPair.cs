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
	/// <author> Jerry Vos modified by John Champion </author>
	/// <version> $Revision: 3.5 $ $Date: 2004-11-10 04:36:52 $ </version>
	public struct PointPair
	{
	#region Member variables
		/// <summary>
		/// Missing values are represented internally using <see cref="System.Double.MaxValue"/>.
		/// </summary>
		public const double Missing = Double.MaxValue;

		/// <summary>
		/// This PointPair's X coordinate
		/// </summary>
		public double X;

		/// <summary>
		/// This PointPair's Y coordinate
		/// </summary>
		public double Y;
		
		/// <summary>
		/// This PointPair's Z coordinate.  Also used for the lower value (dependent axis)
		/// for <see cref="BarType.HiLow"/> charts.
		/// </summary>
		public double Z;
	#endregion

	#region Constructors
		/// <summary>
		/// Creates a point pair with the specified X and Y.
		/// </summary>
		/// <param name="x">This pair's x coordinate.</param>
		/// <param name="y">This pair's y coordinate.</param>
		public PointPair( double x, double y )
		{
			this.X = x;
			this.Y = y;
			this.Z = 0;
		}

		/// <summary>
		/// Creates a point pair with the specified X, Y, and base value.
		/// </summary>
		/// <param name="x">This pair's x coordinate.</param>
		/// <param name="y">This pair's y coordinate.</param>
		/// <param name="z">This pair's z or lower dependent coordinate.</param>
		public PointPair( double x, double y, double z )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Creates a point pair from the specified <see cref="PointF"/> struct.
		/// </summary>
		/// <param name="pt">The <see cref="PointF"/> struct from which to get the
		/// new <see cref="PointPair"/> values.</param>
		public PointPair( PointF pt )
		{
			this.X = pt.X;
			this.Y = pt.Y;
			this.Z = 0;
		}

		/// <summary>
		/// The PointPair copy constructor.
		/// </summary>
		/// <param name="rhs">The basis for the copy.</param>
		public PointPair( PointPair rhs )
		{
			this.X = rhs.X;
			this.Y = rhs.Y;
			this.Z = rhs.Z;
		}
	#endregion

	#region Properties
		/// <summary>
		/// Readonly value that determines if either the X or the Y
		/// coordinate in this PointPair is a missing value.
		/// </summary>
		/// <returns>true if either value is missing</returns>
		public bool IsMissing
		{
			get { return this.X == PointPair.Missing || this.Y == PointPair.Missing; }
		}

		/// <summary>
		/// Readonly value that determines if either the X or the Y
		/// coordinate in this PointPair is an invalid (not plotable) value.
		/// It is considered invalid if it is missing (equal to System.Double.Max),
		/// Infinity, or NaN.
		/// </summary>
		/// <returns>true if either value is invalid</returns>
		public bool IsInvalid
		{
			get { return	this.X == PointPair.Missing ||
							this.Y == PointPair.Missing ||
							Double.IsInfinity( this.X ) ||
							Double.IsInfinity( this.Y ) ||
							Double.IsNaN( this.X ) ||
							Double.IsNaN( this.Y );
				}
		}

		/// <summary>
		/// The "low" value for this point (lower dependent-axis value).
		/// This is really just an alias for <see cref="PointPair.Z"/>.
		/// </summary>
		/// <value>The lower dependent value for this <see cref="PointPair"/>.</value>
		public double LowValue
		{
			get { return this.Z; }
			set { this.Z = value; }
		}

	#endregion

	#region Operator Overloads
		/// <summary>
		/// Implicit conversion from PointPair to PointF.  Note that this conversion
		/// can result in data loss, since the data are being cast from a type
		/// double (64 bit) to a float (32 bit).
		/// </summary>
		/// <param name="pair">The PointPair struct on which to operate</param>
		/// <returns>A PointF struct equivalent to the PointPair</returns>
		public static implicit operator PointF( PointPair pair )
		{
			return new PointF( (float) pair.X, (float) pair.Y );
		}

	#endregion

	#region Inner classes
		/// <summary>
		/// Compares points based on their y values.  Is setup to be used in an
		/// ascending order sort.
		/// <seealso cref="System.Collections.ArrayList.Sort()"/>
		/// </summary>
		public class PointPairComparerY : IComparer 
		{
		
			/// <summary>
			/// Compares two <see cref="PointPair"/>s.
			/// </summary>
			/// <param name="l">Point to the left.</param>
			/// <param name="r">Point to the right.</param>
			/// <returns>-1, 0, or 1 depending on l.Y's relation to r.Y</returns>
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

				double lY = ((PointPair) l).Y;
				double rY = ((PointPair) r).Y;

				if (System.Math.Abs(lY - rY) < .000000001)
					return 0;
				
				return lY < rY ? -1 : 1;
			}
		}
	
		/// <summary>
		/// Compares points based on their x values.  Is setup to be used in an
		/// ascending order sort.
		/// <seealso cref="System.Collections.ArrayList.Sort()"/>
		/// </summary>
		public class PointPairComparer : IComparer 
		{
			private SortType sortType;
			
			/// <summary>
			/// Constructor for PointPairComparer.
			/// </summary>
			/// <param name="type">The axis type on which to sort.</param>
			public PointPairComparer( SortType type )
			{
				this.sortType = type;
			}
			
			/// <summary>
			/// Compares two <see cref="PointPair"/>s.
			/// </summary>
			/// <param name="l">Point to the left.</param>
			/// <param name="r">Point to the right.</param>
			/// <returns>-1, 0, or 1 depending on l.X's relation to r.X</returns>
			int IComparer.Compare( object l, object r ) 
			{				 
				if ( l == null && r == null ) 
					return 0;
				else if ( l == null && r != null ) 
					return -1;
				else if ( l != null && r == null ) 
					return 1;

				double lVal, rVal;
			
				if ( sortType == SortType.XValues )
				{
					lVal = ((PointPair) l).X;
					rVal = ((PointPair) r).X;
				}
				else
				{
					lVal = ((PointPair) l).Y;
					rVal = ((PointPair) r).Y;
				}
				
				if ( lVal == PointPair.Missing || Double.IsInfinity( lVal ) || Double.IsNaN( lVal ) )
					l = null;
				if ( rVal == PointPair.Missing || Double.IsInfinity( rVal ) || Double.IsNaN( rVal ) )
					r = null;

				if ( ( l == null && r == null ) || ( System.Math.Abs( lVal - rVal ) < 1e-10 ) )
					return 0;
				else if ( l == null && r != null ) 
					return -1;
				else if ( l != null && r == null ) 
					return 1;
				else
					return lVal < rVal ? -1 : 1;
			}
		}
	
	#endregion

	#region Methods
		/// <summary>
		/// Format this PointPair value using the default format.  Example:  "( 12.345, -16.876 )".
		/// The two double values are formatted with the "g" format type.
		/// </summary>
		/// <returns>A string representation of the PointPair</returns>
		public override string ToString()
		{
			return this.ToString( "G", false );
		}

		/// <summary>
		/// Format this PointPair value using the default format.  Example:  "( 12.345, -16.876 )".
		/// The two double values are formatted with the "g" format type.
		/// </summary>
		/// <param name="isShowZ">true to show the third "Z" or low dependent value coordinate</param>
		/// <returns>A string representation of the PointPair</returns>
		public string ToString( bool isShowZ )
		{
			return this.ToString( "G", isShowZ );
		}

		/// <summary>
		/// Format this PointPair value using a general format string.
		/// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
		/// </summary>
		/// <param name="format">A format string that will be used to format each of
		/// the two double type values (see <see cref="System.Double.ToString()"/>).</param>
		/// <returns>A string representation of the PointPair</returns>
		public string ToString( string format )
		{
			return this.ToString( format, false );
		}

		/// <summary>
		/// Format this PointPair value using a general format string.
		/// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
		/// If <see paramref="isShowZ"/>
		/// is true, then the third "Z" coordinate is also shown.
		/// </summary>
		/// <param name="format">A format string that will be used to format each of
		/// the two double type values (see <see cref="System.Double.ToString()"/>).</param>
		/// <returns>A string representation of the PointPair</returns>
		/// <param name="isShowZ">true to show the third "Z" or low dependent value coordinate</param>
		public string ToString( string format, bool isShowZ )
		{
			return "( " + this.X.ToString( format ) +
					", " + this.Y.ToString( format ) + 
					( isShowZ ? ( ", " + this.Z.ToString( format ) ) : "" )
					+ " )";
		}
	#endregion
	}
}
