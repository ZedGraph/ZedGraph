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
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
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

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A bar-type <see cref="CurveItem"/> similar to the <see cref="BarType.Cluster"/>,
	/// except that the bottom of the bars is defined by the <see cref="PointPair.LowValue"/>
	/// from the <see cref="PointPair"/> struct.
	/// </summary>
	public class HiLowBarItem : BarItem, ICloneable
	{

	#region Fields
		/// <summary>
		/// Private field that determines which <see cref="Axis"/> is the independent axis
		/// for this <see cref="HiLowBarItem"/>.
		/// </summary>
		protected BarBase	barBase;
	#endregion

	#region Constructors
		/// <summary>
		/// Create a new <see cref="HiLowBarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		/// <param name="baseVal">An array of double precision values that define the
		/// base value (the bottom) of the bars for this curve.
		/// </param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="ZedGraph.Bar.Fill"/> and <see cref="ZedGraph.Bar.Border"/> properties.
		/// </param>
		public HiLowBarItem( string label, double[] x, double[] y, double[] baseVal, Color color ) :
					this( label, new PointPairList( x, y, baseVal ), color )
		{
			
		}
		
		/// <summary>
		/// Create a new <see cref="HiLowBarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value trio's that define
		/// the X, Y, and lower dependent values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="ZedGraph.Bar.Fill"/> and <see cref="ZedGraph.Bar.Border"/> properties.
		/// </param>
		public HiLowBarItem( string label, PointPairList points, Color color )
			: base( label, points, color )
		{
			bar = new HiLowBar( color );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="HiLowBarItem"/> object from which to copy</param>
		public HiLowBarItem( HiLowBarItem rhs ) : base( rhs )
		{
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="HiLowBarItem"/></returns>
		new public object Clone()
		{ 
			return new HiLowBarItem( this ); 
		}
	#endregion

	#region Properties
		/// <summary>
		/// Determines which <see cref="Axis"/> is the independent axis
		/// for this <see cref="HiLowBarItem"/>.
		/// </summary>
		public BarBase	BarBase
		{
			get { return barBase; }
			set { barBase = value; }
		}

		/// <summary>Returns a reference to the <see cref="Axis"/> object that is the "base"
		/// (independent axis) from which the <see cref="Bar"/>'s are drawn.
		/// The base axis is the axis from which the bars grow with increasing value.  This
		/// property is determined by the value of <see cref="BarBase"/>.
		/// </summary>
		/// <remarks>For regular <see cref="BarItem"/>'s, the base axis is determined
		/// by <see cref="ZedGraph.BarBase"/> (a global value).  For <see cref="HiLowBarItem"/>'s, the
		/// base axis is determined by <see cref="HiLowBarItem.BarBase"/>, and can be
		/// different for each curve.</remarks>
		/// <seealso cref="BarBase"/>
		/// <seealso cref="ValueAxis"/>
		override public Axis BaseAxis( GraphPane pane )
		{
			if ( barBase == BarBase.X )
				return pane.XAxis;
			else if ( barBase == BarBase.Y )
				return pane.YAxis;
			else
				return pane.Y2Axis;
		}
		/// <summary>Returns a reference to the <see cref="Axis"/> object that is the "value"
		/// (dependent axis) for the <see cref="Bar"/>'s.
		/// The value axis determines the height of the bars.  This
		/// property is determined by the value of <see cref="BarBase"/>.
		/// </summary>
		/// <seealso cref="BarBase"/>
		/// <seealso cref="BaseAxis"/>
		override public Axis ValueAxis( GraphPane pane, bool isY2Axis )
		{
			if ( barBase == BarBase.X )
			{
				if ( isY2Axis )
					return pane.Y2Axis;
				else
					return pane.YAxis;
			}
			else
				return pane.XAxis;
		}
	#endregion

	#region Methods
		/// <summary>
		/// Go through the list of <see cref="PointPair"/> data values for this
		/// <see cref="HiLowBarItem"/> and determine the minimum and maximum values in the data.
		/// </summary>
		/// <param name="xMin">The minimum X value in the range of data</param>
		/// <param name="xMax">The maximum X value in the range of data</param>
		/// <param name="yMin">The minimum Y value in the range of data</param>
		/// <param name="yMax">The maximum Y value in the range of data</param>
		/// <param name="bIgnoreInitial">ignoreInitial is a boolean value that
		/// affects the data range that is considered for the automatic scale
		/// ranging (see <see cref="GraphPane.IsIgnoreInitial"/>).  If true, then initial
		/// data points where the Y value is zero are not included when
		/// automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.  All data after
		/// the first non-zero Y value are included.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		override public void GetRange( 	ref double xMin, ref double xMax,
										ref double yMin, ref double yMax,
										bool bIgnoreInitial, GraphPane pane )
		{
			// Call a default GetRange() that does not include Z data points
			this.points.GetRange( ref xMin, ref xMax, ref yMin, ref yMax, bIgnoreInitial,
									true, barBase == BarBase.X );
		}

	#endregion


	}
}
