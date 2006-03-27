//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005  John Champion
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

namespace ZedGraph
{
	/// <summary>
	/// A simple struct to store minimum and maximum <see cref="double" /> type
	/// values for the scroll range
	/// </summary>
	public struct ScrollRange
	{
		private bool isScrollable;
		private double min;
		private double max;

		/// <summary>
		/// Construct a <see cref="ScrollRange" /> object given the specified data values.
		/// </summary>
		/// <param name="min">The minimum axis value limit for the scroll bar</param>
		/// <param name="max">The maximum axis value limit for the scroll bar</param>
		/// <param name="isScrollable">true to make this item scrollable, false otherwise</param>
		public ScrollRange( double min, double max, bool isScrollable )
		{
			this.min = min;
			this.max = max;
			this.isScrollable = isScrollable;
		}

		/// <summary>
		/// Sets the scroll range to default values of zero, and sets the <see cref="IsScrollable" />
		/// property as specified.
		/// </summary>
		/// <param name="isScrollable">true to make this item scrollable, false otherwise</param>
		public ScrollRange( bool isScrollable )
		{
			this.min = 0.0;
			this.max = 0.0;
			this.isScrollable = isScrollable;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ScrollRange"/> object from which to copy</param>
		public ScrollRange( ScrollRange rhs )
		{
			this.min = rhs.min;
			this.max = rhs.max;
			this.isScrollable = rhs.isScrollable;
		}
				
		/// <summary>
		/// Gets or sets a property that determines if the <see cref="Axis" /> corresponding to
		/// this <see cref="ScrollRange" /> object can be scrolled.
		/// </summary>
		public bool IsScrollable
		{
			get { return isScrollable; }
			set { isScrollable = value; }
		}

		/// <summary>
		/// The minimum axis value limit for the scroll bar.
		/// </summary>
		public double Min
		{
			get { return min; }
			set { min = value; }
		}
		/// <summary>
		/// The maximum axis value limit for the scroll bar.
		/// </summary>
		public double Max
		{
			get { return max; }
			set { max = value; }
		}
	}
}
