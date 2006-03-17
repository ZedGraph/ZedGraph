//============================================================================
//RadarPointList Class
//Copyright (C) 2006  John Champion, Jerry Vos
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
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A class containing a set of data values to be plotted as a RadarPlot.
	/// This class will effectively convert the data into <see cref="PointPair" /> objects
	/// by converting the polar coordinates to rectangular coordinates
	/// </summary>
	/// <seealso cref="BasicArrayPointList" />
	/// <seealso cref="IPointList" />
	/// <seealso cref="IPointListEdit" />
	/// 
	/// <author>Jerry Vos and John Champion</author>
	/// <version> $Revision: 3.1 $ $Date: 2006-03-17 06:21:14 $ </version>
	[Serializable]
	public class RadarPointList : CollectionPlus, IPointList, IPointListEdit
	{

	#region Properties

		/// <summary>
		/// Indexer to access the specified <see cref="PointPair"/> object by
		/// its ordinal position in the list.  This method does the calculations
		/// to convert the data from polar to rectangular coordinates.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="PointPair"/> object to be accessed.</param>
		/// <value>A <see cref="PointPair"/> object reference.</value>
		public PointPair this[int index]
		{
			get
			{
				int count = List.Count;
				// The last point is a repeat of the first point
				if ( index == count )
					index = 0;

				if ( index < 0 || index >= count )
					return null;

				PointPair pt = (PointPair)List[index];
				double theta = (double) index / (double) count * 2.0 * Math.PI;
				double x = pt.Y * Math.Cos( theta );
				double y = pt.Y * Math.Sin( theta );
				return new PointPair( x, y, pt.Z, (string) pt.Tag );
			}
			set
			{
				int count = List.Count;
				// The last point is a repeat of the first point
				if ( index == count )
					index = 0;

				if ( index < 0 || index >= count )
					return;

				PointPair pt = (PointPair)List[index];
				pt.Y = Math.Sqrt( value.X * value.X + value.Y * value.Y );
			}
		}

		public new int Count
		{
			get { return List.Count + 1; }
		}

	#endregion

	#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RadarPointList() : base()
		{
		}

		/// <summary>
		/// Constructor to initialize the RadarPointList from an array of doubles.
		/// </summary>
		public RadarPointList( RadarPointList rhs )
		{
			for ( int i = 0; i < rhs.List.Count; i++ )
				this.Add( (PointPair) rhs.List[i] );
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
		public RadarPointList Clone()
		{
			return new RadarPointList( this );
		}


	#endregion

	#region Methods
		/// <summary>
		/// Add the specified PointPair to the collection.
		/// </summary>
		/// <param name="pt">The PointPair to be added</param>
		/// <returns>The ordinal position in the list where the point was added</returns>
		public int Add( PointPair pt )
		{
			return List.Add( pt );
		}

		/// <summary>
		/// Add a single point to the <see cref="RadarPointList"/> from a value of type double.
		/// </summary>
		/// <param name="r">The radial coordinate value</param>
		/// <returns>The zero-based ordinal index where the point was added in the list.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( double r )
		{
			return List.Add( new PointPair( PointPair.Missing, r ) );
		}

		/// <summary>
		/// Add a single point to the <see cref="RadarPointList"/> from two values of type double.
		/// </summary>
		/// <param name="r">The radial coordinate value</param>
		/// <param name="z">The 'Z' coordinate value, which is not normally used for plotting,
		/// but can be used for <see cref="FillType.GradientByZ" /> type fills</param>
		/// <returns>The zero-based ordinal index where the point was added in the list.</returns>
		/// <seealso cref="IList.Add"/>
		public int Add( double r, double z )
		{
			return List.Add( new PointPair( PointPair.Missing, r, z ) );
		}

	#endregion
	}
}


