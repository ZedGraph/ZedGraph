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

using IComparer	= System.Collections.IComparer;

namespace ZedGraph
{
	/// <summary>
	/// A simple point represented by an (X,Y) pair
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 1.1 $ $Date: 2004-08-23 20:24:17 $ </version>
	public struct PointPair
	{
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
		public PointPair(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// The PointPair copy constructor.
		/// </summary>
		/// <param name="rhs">The basis for the copy.</param>
		public PointPair(PointPair rhs)
		{
			this.X = rhs.X;
			this.Y = rhs.Y;
		}

		/// <summary>
		/// Compares points based on their x values.  Is setup to be used in an
		/// ascending order sort.
		/// <seealso cref="ArrayList.Sort()"/>
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
			int IComparer.Compare(object l, object r) 
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
	}
}
