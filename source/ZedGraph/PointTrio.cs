//============================================================================
//PointTrio Class
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
using IComparer	= System.Collections.IComparer;

namespace ZedGraph
{
	/// <summary>
	/// A simple point represented by an (X,Y) pair of
	/// double values.
	/// </summary>
	/// 
	/// <author> John Champion from PointPair code by Jerry Vos</author>
	/// <version> $Revision: 3.2 $ $Date: 2004-10-26 05:33:38 $ </version>
	public struct PointTrio
	{
	#region Member variables
		/// <summary>
		/// This PointTrio's X coordinate
		/// </summary>
		public double X;

		/// <summary>
		/// This PointTrio's Y coordinate
		/// </summary>
		public double Y;

		/// <summary>
		/// This PointTrio's base (lower dependent) coordinate
		/// </summary>
		public double BaseVal;
	#endregion

	#region Constructors
		/// <summary>
		/// Creates a point trio with the specified X, Y, and base value.
		/// </summary>
		/// <param name="x">This pair's x coordinate.</param>
		/// <param name="y">This pair's y coordinate.</param>
		/// <param name="baseVal">This pair's base coordinate.</param>
		public PointTrio( double x, double y, double baseVal )
		{
			this.X = x;
			this.Y = y;
			this.BaseVal = baseVal;
		}

		/// <summary>
		/// The PointTrio copy constructor.
		/// </summary>
		/// <param name="rhs">The basis for the copy.</param>
		public PointTrio( PointTrio rhs )
		{
			this.X = rhs.X;
			this.Y = rhs.Y;
			this.BaseVal = rhs.BaseVal;
		}
	#endregion

	#region Properties
	
		/// <summary>
		/// Readonly value that determines if the X1, the Y1, or the X2/Y2
		/// coordinate in this PointTrio is a missing value.
		/// </summary>
		/// <returns>true if any value is missing</returns>
		public bool IsMissing
		{
			get { return this.X == PointPair.Missing || this.Y == PointPair.Missing ||
					this.BaseVal == PointPair.Missing; }
		}

		/// <summary>
		/// Readonly value that determines if either the X, Y, or BaseVal
		/// coordinate in this PointTrio is an invalid (not plotable) value.
		/// It is considered invalid if it is missing (equal to System.Double.Max),
		/// Infinity, or NaN.
		/// </summary>
		/// <returns>true if either value is invalid</returns>
		public bool IsInvalid
		{
			get { return	this.X == PointPair.Missing ||
							this.Y == PointPair.Missing ||
							this.BaseVal == PointPair.Missing ||
							Double.IsInfinity( this.X ) ||
							Double.IsInfinity( this.Y ) ||
							Double.IsInfinity( this.BaseVal ) ||
							Double.IsNaN( this.X ) ||
							Double.IsNaN( this.Y ) ||
							Double.IsNaN( this.BaseVal );
				}
		}
		
		/// <summary>
		/// Readonly property that returns a <see cref="PointPair"/> struct corresponding
		/// to the (X,Y) values of this <see cref="PointTrio"/>.
		/// </summary>
		/// <returns>The first <see cref="PointPair"/> struct.</returns>
		public PointPair PointPair
		{
			get { return new PointPair( this.X, this.Y ); }
		}
		/// <summary>
		/// Readonly property that returns a <see cref="PointPair"/> struct corresponding
		/// to the (X,BaseVal) values of this <see cref="PointTrio"/>.  This property implicitly
		/// assumes that the dependent axis is Y.
		/// </summary>
		/// <returns>The second <see cref="PointPair"/> struct.</returns>
		public PointPair PointPairYBase
		{
			get { return new PointPair( this.X, this.BaseVal ); }
		}
		/// <summary>
		/// Readonly property that returns a <see cref="PointPair"/> struct corresponding
		/// to the (BaseVal,Y) values of this <see cref="PointTrio"/>.  This property implicitly
		/// assumes that the dependent axis is X.
		/// </summary>
		/// <returns>The second <see cref="PointPair"/> struct.</returns>
		public PointPair PointPairXBase
		{
			get { return new PointPair( this.BaseVal, this.Y ); }
		}
		
		/// <summary>
		/// Readonly property that returns a <see cref="PointF"/> struct corresponding
		/// to the (X,Y) values of this <see cref="PointTrio"/>.  This routine can
		/// result in data loss since it reduces precision from <see cref="System.Double"/>
		/// to <see cref="System.Single"/>.
		/// </summary>
		/// <returns>The first <see cref="PointF"/> struct.</returns>
		public PointF PointF
		{
			get { return new PointF( (float) this.X, (float) this.Y ); }
		}
		/// <summary>
		/// Readonly property that returns a <see cref="PointF"/> struct corresponding
		/// to the (X,BaseVal) values of this <see cref="PointTrio"/>.  This routine can
		/// result in data loss since it reduces precision from <see cref="System.Double"/>
		/// to <see cref="System.Single"/>.  Note that this property implicitly assumes
		/// that the dependent axis is Y.
		/// </summary>
		/// <returns>The second <see cref="PointF"/> struct.</returns>
		public PointF PointFYBase
		{
			get { return new PointF( (float) this.X, (float) this.BaseVal ); }
		}
		/// <summary>
		/// Readonly property that returns a <see cref="PointF"/> struct corresponding
		/// to the (BaseVal,Y) values of this <see cref="PointTrio"/>.  This routine can
		/// result in data loss since it reduces precision from <see cref="System.Double"/>
		/// to <see cref="System.Single"/>.  Note that this property implicitly assumes
		/// that the dependent axis is X.
		/// </summary>
		/// <returns>The second <see cref="PointF"/> struct.</returns>
		public PointF PointFXBase
		{
			get { return new PointF( (float) this.BaseVal, (float) this.Y ); }
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
					this.Y.ToString( format ) + ", " +
					this.BaseVal.ToString( format ) + " )";
		}
	#endregion
	}
}
