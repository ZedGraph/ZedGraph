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

using System;

namespace ZedGraph
{
	/// <summary>
	/// Enumeration type for the various axis types that are available
	/// </summary>
	/// <seealso cref="ZedGraph.Axis.Type"/>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.5 $ $Date: 2004-08-23 20:22:26 $ </version>
	public enum AxisType
	{
		/// <summary> An ordinary, cartesian axis </summary>
		Linear,
		/// <summary> A base 10 log axis </summary>
		Log,
		/// <summary> A cartesian axis with calendar dates or times </summary>
		Date,
		/// <summary> An ordinal axis with user-defined text labels.  An ordinal axis means that
		/// all data points are evenly spaced at integral values, and the actual coordinate values
		/// for points corresponding to that axis are ignored.  That is, if the X axis is an
		/// ordinal type, then all X values associated with the curves are ignored.</summary>
		/// <seealso cref="AxisType.Ordinal"/>
		/// <seealso cref="Axis.IsText"/>
		/// <seealso cref="Def.Ax.MaxTextLabels"/>
		Text,
		/// <summary> An ordinal axis with regular numeric labels.  An ordinal axis means that
		/// all data points are evenly spaced at integral values, and the actual coordinate values
		/// for points corresponding to that axis are ignored.  That is, if the X axis is an
		/// ordinal type, then all X values associated with the curves are ignored. </summary>
		/// <seealso cref="AxisType.Text"/>
		/// <seealso cref="Axis.IsOrdinal"/>
		Ordinal
};

	/// <summary>
	/// Enumeration type for the various axis date and time unit types that are available
	/// </summary>
	public enum DateUnit
	{
		/// <summary> Yearly units <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/> </summary>
		Year,
		/// <summary> Monthly units <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/> </summary>
		Month,
		/// <summary> Daily units <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/> </summary>
		Day,
		/// <summary> Hourly units <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/> </summary>
		Hour,
		/// <summary> Minute units <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/> </summary>
		Minute,
		/// <summary> Second units <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/> </summary>
		Second
	};

	/// <summary>
	/// Enumeration type for the various symbol shapes that are available
	/// </summary>
	/// <seealso cref="ZedGraph.Symbol.IsFilled"/>
	public enum SymbolType 
	{
		/// <summary> Square-shaped <see cref="ZedGraph.Symbol"/> </summary>
		Square,
		/// <summary> Rhombus-shaped <see cref="ZedGraph.Symbol"/> </summary>
		Diamond,
		/// <summary> Equilateral triangle <see cref="ZedGraph.Symbol"/> </summary>
		Triangle,
		/// <summary> Uniform circle <see cref="ZedGraph.Symbol"/> </summary>
		Circle,
		/// <summary> "X" shaped <see cref="ZedGraph.Symbol"/>.  This symbol cannot
		/// be filled since it has no outline. </summary>
		XCross,
		/// <summary> "+" shaped <see cref="ZedGraph.Symbol"/>.  This symbol cannot
		/// be filled since it has no outline. </summary>
		Plus,
		/// <summary> Asterisk-shaped <see cref="ZedGraph.Symbol"/>.  This symbol
		/// cannot be filled since it has no outline. </summary>
		Star,
		/// <summary> Unilateral triangle <see cref="ZedGraph.Symbol"/>, pointing
		/// down. </summary>
		TriangleDown
	};

	/// <summary>
	/// Enumeration type that defines the possible legend locations
	/// </summary>
	/// <seealso cref="Legend.Location"/>
	public enum LegendLoc
	{
		/// <summary>
		/// Locate the <see cref="Legend"/> above the <see cref="GraphPane.AxisRect"/>
		/// </summary>
		Top,
		/// <summary>
		/// Locate the <see cref="Legend"/> on the left side of the <see cref="GraphPane.AxisRect"/>
		/// </summary>
		Left,
		/// <summary>
		/// Locate the <see cref="Legend"/> on the right side of the <see cref="GraphPane.AxisRect"/>
		/// </summary>
		Right,
		/// <summary>
		/// Locate the <see cref="Legend"/> below the <see cref="GraphPane.AxisRect"/>
		/// </summary>
		Bottom,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// top-left corner
		/// </summary>
		InsideTopLeft,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// top-right corner
		/// </summary>
		InsideTopRight,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// bottom-left corner
		/// </summary>
		InsideBotLeft,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// bottom-right corner
		/// </summary>
		InsideBotRight
	};

	/// <summary>
	/// Enumeration type for the different horizontal text alignment options
	/// </summary>
	/// <seealso cref="FontSpec"/>
	public enum FontAlignH
	{
		/// <summary>
		/// Position the text so that its left edge is aligned with the
		/// specified X,Y location.  Used by the
		/// <see cref="FontSpec.Draw"/> method.
		/// </summary>
		Left,
		/// <summary>
		/// Position the text so that its center is aligned (horizontally) with the
		/// specified X,Y location.  Used by the
		/// <see cref="FontSpec.Draw"/> method.
		/// </summary>
		Center,
		/// <summary>
		/// Position the text so that its right edge is aligned with the
		/// specified X,Y location.  Used by the
		/// <see cref="FontSpec.Draw"/> method.
		/// </summary>
		Right
	};
	
	/// <summary>
	/// Enumeration type for the different vertical text alignment options
	/// </summary>
	/// specified X,Y location.  Used by the
	/// <see cref="FontSpec.Draw"/> method.
	public enum FontAlignV
	{
		/// <summary>
		/// Position the text so that its top edge is aligned with the
		/// specified X,Y location.  Used by the
		/// <see cref="FontSpec.Draw"/> method.
		/// </summary>
		Top,
		/// <summary>
		/// Position the text so that its center is aligned (vertically) with the
		/// specified X,Y location.  Used by the
		/// <see cref="FontSpec.Draw"/> method.
		/// </summary>
		Center,
		/// <summary>
		/// Position the text so that its bottom edge is aligned with the
		/// specified X,Y location.  Used by the
		/// <see cref="FontSpec.Draw"/> method.
		/// </summary>
		Bottom
	};

	/// <summary>
	/// Enumeration type for the user-defined coordinate types available.
	/// These coordinate types are used the <see cref="ArrowItem"/> objects
	/// and <see cref="TextItem"/> objects only.
	/// </summary>
	/// <seealso cref="TextItem.CoordinateFrame"/>
	/// <seealso cref="ArrowItem.CoordinateFrame"/>
	public enum CoordType
	{
		/// <summary>
		/// Coordinates are specified as a fraction of the
		/// <see cref="GraphPane.AxisRect"/>.  That is, for the X coordinate, 0.0
		/// is at the left edge of the AxisRect and 1.0
		/// is at the right edge of the AxisRect. A value less
		/// than zero is left of the AxisRect and a value
		/// greater than 1.0 is right of the AxisRect.  For the Y coordinate, 0.0
		/// is the bottom and 1.0 is the top.
		/// </summary>
		AxisFraction,
		/// <summary>
		/// Coordinates are specified as a fraction of the
		/// <see cref="GraphPane.PaneRect"/>.  That is, for the X coordinate, 0.0
		/// is at the left edge of the PaneRect and 1.0
		/// is at the right edge of the PaneRect. A value less
		/// than zero is left of the PaneRect and a value
		/// greater than 1.0 is right of the PaneRect.  For the Y coordinate, 0.0
		/// is the bottom and 1.0 is the top.  Note that
		/// any value less than zero or greater than 1.0 will be outside
		/// the PaneRect, and therefore clipped.
		/// </summary>
		PaneFraction,
		/// <summary>
		/// Coordinates are specified according to the user axis scales
		/// for the <see cref="GraphPane.XAxis"/> and <see cref="GraphPane.YAxis"/>.
		/// </summary>
		AxisXYScale,
		/// <summary>
		/// Coordinates are specified according to the user axis scales
		/// for the <see cref="GraphPane.XAxis"/> and <see cref="GraphPane.Y2Axis"/>.
		/// </summary>
		AxisXY2Scale
	};
	
	/// <summary>
	/// Enumeration type that defines how a curve is draw.  Curves can be drawn
	/// as ordinary lines by connecting the points directly, or in a stair-step
	/// fashion as a series of discrete, constant values.  In a stair step plot,
	/// all lines segments are either horizontal or vertical.  In a non-step (line)
	/// plot, the lines can be any angle.
	/// </summary>
	/// <seealso cref="Line.StepType"/>
	public enum StepType
	{
		/// <summary>
		/// Draw the <see cref="CurveItem"/> as a stair-step in which each
		/// point defines the
		/// beginning (left side) of a new stair.  This implies the points are
		/// defined at the beginning of an "event."
		/// </summary>
		ForwardStep,
		/// <summary>
		/// Draw the <see cref="CurveItem"/> as a stair-step in which each
		/// point defines the end (right side) of a new stair.  This implies
		/// the points are defined at the end of an "event."
		/// </summary>
		RearwardStep,
		/// <summary>
		/// Draw the <see cref="CurveItem"/> as an ordinary line, in which the
		/// points are connected directly by line segments.
		/// </summary>
		NonStep
	};
}