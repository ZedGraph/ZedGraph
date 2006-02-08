//============================================================================
//IPointListEdit interface
//Copyright (C) 2006  John Champion
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
using System.Text;

namespace ZedGraph
{
	/// <summary>
	/// An interface to a collection class containing data
	/// that define the set of points to be displayed on the curve.
	/// </summary>
	/// <remarks>
	/// This interface is designed to allow customized data abstraction.  The default data
	/// collection class is <see cref="PointPairList" />, however, you can define your own
	/// data collection class using the <see cref="IPointList" /> interface.  This
	/// <see cref="IPointListEdit" /> interface adds the ability to remove and add points
	/// to the list, and so is used by the <see cref="CurveItem" /> class for the
	/// <see cref="CurveItem.Add" />, <see cref="CurveItem.Remove" />, and
	/// <see cref="CurveItem.Clear" /> methods.
	/// </remarks>
	/// <seealso cref="PointPairList" />
	/// <seealso cref="BasicArrayPointList" />
	/// 
	/// <author> John Champion</author>
	/// <version> $Revision: 3.1 $ $Date: 2006-02-08 05:35:12 $ </version>
	public interface IPointListEdit
	{
		/// <summary>
		/// Appends a point to the end of the list.  The data are passed in as a <see cref="PointPair" />
		/// object.
		/// </summary>
		/// <param name="point">The <see cref="PointPair" /> object containing the data to be added.</param>
		/// <returns>The ordinal position (zero-based), at which the new point was added.</returns>
		int Add( PointPair point );

		/// <summary>
		/// Removes a single data point from the list at the specified ordinal location
		/// (zero based).
		/// </summary>
		void Remove( int index );

		/// <summary>
		/// Clears all data points from the list.  After calling this method,
		/// <see cref="IPointList.Count" /> will be zero.
		/// </summary>
		void Clear();

	}
}
