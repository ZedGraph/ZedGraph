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
using IComparer	= System.Collections.IComparer;

namespace ZedGraph
{
	/// <summary>
	/// A simple point represented by an (X,Y) pair of
	/// double values.
	/// </summary>
	/// 
	/// <author> Jerry Vos modified by John Champion </author>
	/// <version> $Revision: 1.2 $ $Date: 2004-08-23 20:27:45 $ </version>
	public struct PointPair
	{
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
		/// Creates a point pair with the specified X and Y.
		/// </summary>
		/// <param name="x">This pair's x coordinate.</param>
		/// <param name="y">This pair's y coordinate.</param>
		public PointPair( double x, double y )
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// The PointPair copy constructor.
		/// </summary>
		/// <param name="rhs">The basis for the copy.</param>
		public PointPair( PointPair rhs )
		{
			this.X = rhs.X;
			this.Y = rhs.Y;
		}
		
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
		/// Compares points based on their x values.  Is setup to be used in an
		/// ascending order sort.
		/// <seealso cref="System.Collections.ArrayList.Sort()"/>
		/// <seealso cref="PointPairList.Sort()"/>
		/// </summary>
		public class PointPairComparer : IComparer 
		{
		
			/// <summary>
			/// Compares two <see cref="PointPair"/>s.
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

				double lX = ((PointPair) l).X;
				double rX = ((PointPair) r).X;

				if (System.Math.Abs(lX - rX) < .000000001)
					return 0;
				
				return lX < rX ? -1 : 1;
			}
		}

		/// <summary>
		/// Format this PointPair value using the default format.  Example:  "( 12.345, -16.876)".
		/// The two double values are formatted with the "g" format type.
		/// </summary>
		/// <returns>A string representation of the PointPair</returns>
		public override string ToString()
		{
			return this.ToString( "G" );
		}

		/// <summary>
		/// Format this PointPair value using a general format string.
		/// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
		/// </summary>
		/// <param name="format">A format string that will be used to format each of
		/// the two double type values (see <see cref="System.Double.ToString"/>).</param>
		/// <returns>A string representation of the PointPair</returns>
		public string ToString( string format )
		{
			return "( " + this.X.ToString( format ) + ", " + this.Y.ToString( format ) + " )";
		}
	}
}
