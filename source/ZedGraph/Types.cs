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
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ZedGraph
{	
	/// <summary>
	/// Enumeration type for the various axis types that are available
	/// </summary>
	/// <seealso cref="ZedGraph.Axis.Type"/>
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
		/// <seealso cref="Axis.Default.MaxTextLabels"/>
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
	/// Enumeration type for the various types of fills that can be used with <see cref="Bar"/>
	/// charts.
	/// </summary>
	public enum FillType
	{
		/// <summary> No fill </summary>
		None,
		/// <summary> A solid fill using <see cref="System.Drawing.SolidBrush"/> </summary>
		Solid,
		/// <summary> A custom fill using either <see cref="LinearGradientBrush"/> or
		/// <see cref="TextureBrush"/></summary>
		Brush,
		/// <summary>
		/// Fill with a single solid color based on the X value of the data.</summary>
		/// <remarks>The X value is
		/// used to determine the color value based on a gradient brush, and using a data range
		/// of <see cref="Fill.RangeMin"/> and <see cref="Fill.RangeMax"/>.  You can create a multicolor
		/// range by initializing the <see cref="Fill"/> class with your own custom
		/// <see cref="Brush"/> object based on a <see cref="ColorBlend"/>.  In cases where a
		/// data value makes no sense (<see cref="GraphPane.PaneFill"/>, <see cref="Legend.Fill"/>,
		/// etc.), a default value of 50% of the range is assumed.  The default range is 0 to 1.
		/// </remarks>
		GradientByX,
		/// <summary>
		/// Fill with a single solid color based on the Z value of the data.</summary>
		/// <remarks>The Z value is
		/// used to determine the color value based on a gradient brush, and using a data range
		/// of <see cref="Fill.RangeMin"/> and <see cref="Fill.RangeMax"/>.  You can create a multicolor
		/// range by initializing the <see cref="Fill"/> class with your own custom
		/// <see cref="Brush"/> object based on a <see cref="ColorBlend"/>.  In cases where a
		/// data value makes no sense (<see cref="GraphPane.PaneFill"/>, <see cref="Legend.Fill"/>,
		/// etc.), a default value of 50% of the range is assumed.  The default range is 0 to 1.
		/// </remarks>
		GradientByY,
		/// <summary>
		/// Fill with a single solid color based on the Z value of the data.</summary>
		/// <remarks>The Z value is
		/// used to determine the color value based on a gradient brush, and using a data range
		/// of <see cref="Fill.RangeMin"/> and <see cref="Fill.RangeMax"/>.  You can create a multicolor
		/// range by initializing the <see cref="Fill"/> class with your own custom
		/// <see cref="Brush"/> object based on a <see cref="ColorBlend"/>.  In cases where a
		/// data value makes no sense (<see cref="GraphPane.PaneFill"/>, <see cref="Legend.Fill"/>,
		/// etc.), a default value of 50% of the range is assumed.  The default range is 0 to 1.
		/// </remarks>
		GradientByZ
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
	/// <seealso cref="ZedGraph.Symbol.Fill"/>
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
		TriangleDown,
		/// <summary>
		/// Horizontal dash <see cref="ZedGraph.Symbol"/>.  This symbol cannot be
		/// filled since it has no outline.
		/// </summary>
		HDash,
		/// <summary>
		/// Vertical dash <see cref="ZedGraph.Symbol"/>.  This symbol cannot be
		/// filled since it has no outline.
		/// </summary>
		VDash,
		/// <summary> A Default symbol type (the symbol type will be obtained
		/// from <see cref="Symbol.Default.Type"/>. </summary>
		Default,
		/// <summary> No symbol is shown (this is equivalent to using
		/// <see cref="Symbol.IsVisible"/> = false.</summary>
		None
	};

	/// <summary>
	/// Enumeration type that defines the possible legend locations
	/// </summary>
	/// <seealso cref="Legend.Position"/>
	public enum LegendPos
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
		/// top-left corner.  
		/// </summary>
		InsideTopLeft,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// top-right corner. 
		/// </summary>
		InsideTopRight,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// bottom-left corner.
		/// </summary>
		InsideBotLeft,
		/// <summary>
		/// Locate the <see cref="Legend"/> inside the <see cref="GraphPane.AxisRect"/> in the
		/// bottom-right corner. 
		/// </summary>
		InsideBotRight,
		/// <summary>
		/// Locate the <see cref="Legend"/> as a floating object above the graph at the
		/// location specified by <see cref="Legend.Location"/>.
		/// </summary>
		Float,
		/// <summary>
		/// Locate the <see cref="Legend"/> centered above the <see cref="GraphPane.AxisRect"/>
		/// </summary>
		TopCenter,
		/// <summary>
		/// Locate the <see cref="Legend"/> centered below the <see cref="GraphPane.AxisRect"/>
		/// </summary>
		BottomCenter
	};

	/// <summary>
	/// Enumeration type for the different horizontal text alignment options
	/// </summary>
	/// <seealso cref="FontSpec"/>
	public enum AlignH
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
	/// Enumeration type for the different proximal alignment options
	/// </summary>
	/// <seealso cref="FontSpec"/>
	/// <seealso cref="Axis.ScaleAlign"/>
	public enum AlignP
	{
		/// <summary>
		/// Position the text so that its "inside" edge (the edge that is
		/// nearest to the alignment reference point or object) is aligned.
		/// Used by the <see cref="Axis.ScaleAlign"/> method to align text
		/// to the axis.
		/// </summary>
		Inside,
		/// <summary>
		/// Position the text so that its center is aligned with the
		/// reference object or point.
		/// Used by the <see cref="Axis.ScaleAlign"/> method to align text
		/// to the axis.
		/// </summary>
		Center,
		/// <summary>
		/// Position the text so that its right edge (the edge that is
		/// farthest from the alignment reference point or object) is aligned.
		/// Used by the <see cref="Axis.ScaleAlign"/> method to align text
		/// to the axis.
		/// </summary>
		Outside
	};
	
	/// <summary>
	/// Enumeration type for the different vertical text alignment options
	/// </summary>
	/// specified X,Y location.  Used by the
	/// <see cref="FontSpec.Draw"/> method.
	public enum AlignV
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
	/// <seealso cref="ZedGraph.Location.CoordinateFrame"/>
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
	/// Enumeration type that defines how a curve is drawn.  Curves can be drawn
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
	
	/// <summary>
	/// Enumeration type that defines the base axis from which <see cref="Bar"/> graphs
	/// are displayed. The bars can be drawn on any of the three axes (<see cref="XAxis"/>,
	/// <see cref="YAxis"/>, and <see cref="Y2Axis"/>).
	/// </summary>
	/// <seealso cref="GraphPane.BarBase"/>
	public enum BarBase
	{
		/// <summary>
		/// Draw the <see cref="Bar"/> chart based from the <see cref="XAxis"/>.
		/// </summary>
		X,
		/// <summary>
		/// Draw the <see cref="Bar"/> chart based from the <see cref="YAxis"/>.
		/// </summary>
		Y,
		/// <summary>
		/// Draw the <see cref="Bar"/> chart based from the <see cref="Y2Axis"/>.
		/// </summary>
		Y2
	};
	
	/// <summary>
	/// Enumeration type that defines the available types of <see cref="LineItem"/> graphs.
	/// </summary>
	/// <seealso cref="GraphPane.LineType"/>
	public enum LineType
	{
		/// <summary>
		/// Draw the lines as normal.  Any fill area goes from each line down to the X Axis.
		/// </summary>
		Normal,
		/// <summary>
		/// Draw the lines stacked on top of each other, accumulating values to a total value.
		/// </summary>
		Stack
	}
	
	/// <summary>
	/// Enumeration type that defines the available types of <see cref="BarItem"/> graphs.
	/// </summary>
	/// <seealso cref="GraphPane.BarType"/>
	public enum BarType
	{
		/// <summary>
		/// Draw each <see cref="BarItem"/> side by side in clusters.
		/// </summary>
		Cluster,
		/// <summary>
		/// Draw the <see cref="BarItem"/> bars one on top of the other.  The bars will
		/// be drawn such that the last bar in the <see cref="CurveList"/> will be behind
		/// all other bars.  Note that the bar values are not summed up for the overlay
		/// mode.  The data values must be summed before being passed
		/// to <see cref="GraphPane.AddBar(string,PointPairList,Color)"/>.
		/// For example, if the first bar of
		/// the first <see cref="BarItem"/> has a value of 100, and the first bar of
		/// the second <see cref="BarItem"/> has a value of 120, then that bar will
		/// appear to be 20 units on top of the first bar.
		/// </summary>
		Overlay,
		/// <summary>
		/// Draw the <see cref="BarItem"/> bars one on top of the other.  The bars will
		/// be drawn such that the bars are sorted according to the maximum value, with
		/// the tallest bar at each point at the back and the shortest bar at the front.
		/// This is similar to the <see cref="Overlay"/> mode, but the bars are sorted at
		/// each base value.
		/// The data values must be summed before being passed
		/// to <see cref="GraphPane.AddBar(string,PointPairList,Color)"/>.  For example, if the first bar of
		/// the first <see cref="BarItem"/> has a value of 100, and the first bar of
		/// the second <see cref="BarItem"/> has a value of 120, then that bar will
		/// appear to be 20 units on top of the first bar.
		/// </summary>
		SortedOverlay,
		/// <summary>
		/// Draw the <see cref="BarItem"/> bars in an additive format so that they stack on
		/// top of one another.  The value of the last bar drawn will be the sum of the values
		/// of all prior bars.
		/// </summary>
		Stack,
		/// <summary>
		/// Draw the <see cref="BarItem"/> bars in a format whereby the height of each
		/// represents the percentage of the total each one represents.  Negative values
		///are displayed below the zero line as percentages of the absolute total of all values. 
		/// </summary>
		PercentStack 
	};
	
	/// <summary>
	/// Enumeration type that defines which set of data points - X or Y - is used  
	/// <seealso cref="System.Collections.ArrayList.Sort()"/> to perform the sort.
	/// </summary>
	public enum SortType
	{
	   /// <summary>
	   /// Use the Y values to sort the list.
	   /// </summary>
	   YValues,
	   /// <summary>
	   /// Use the X values to sort the list.
	   /// </summary>
	   XValues
	};

	/// <summary>
	/// Enumeration that specifies a Z-Order position for <see cref="GraphItem"/>
	/// objects.
	/// </summary>
	/// <remarks>The order of the graph elements is normally (front to back):
	/// the <see cref="Legend"/> object, the <see cref="Axis"/> border,
	/// the <see cref="CurveItem"/> objects, the <see cref="Axis"/> objects,
	/// and the <see cref="GraphPane"/> title object.
	/// </remarks>
	public enum ZOrder
	{
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be behind all other
	   /// objects (including the <see cref="Axis"/> rectangle fill).
	   /// </summary>
	   G_BehindAll,
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be behind the
	   /// <see cref="GraphPane"/> title.
	   /// </summary>
	   F_BehindTitle,
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be behind the
	   /// <see cref="Axis"/> objects.
	   /// </summary>
	   E_BehindAxis,
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be behind the
	   /// <see cref="CurveItem"/> objects.
	   /// </summary>
	   D_BehindCurves,
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be behind the
	   /// <see cref="Axis"/> border.
	   /// </summary>
	   C_BehindAxisBorder,
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be behind the
	   /// <see cref="Legend"/> object.
	   /// </summary>
	   B_BehindLegend,
	   /// <summary>
	   /// Specifies that the <see cref="GraphItem"/> will be in front of
	   /// all other objects, except for the other <see cref="GraphItem"/>
	   /// objects that have the same <see cref="ZOrder"/> and are before
	   /// this object in the <see cref="GraphItemList"/>.
	   /// </summary>
	   A_InFront
	};
/*
	/// <summary>
	/// Enumeration for the type of pie to be displayed
	/// </summary>
	public enum PieType
	{
		/// <summary>
		///	Specifies that the <see cref="Pie"/> chart will be displayed in two dimensions
		/// </summary>
		Pie2D,

		/// <summary>
		///	Specifies that the <see cref="Pie"/> chart will be displayed in three dimensions
		/// </summary>
		Pie3D
	} ;
*/
	/// <summary>
	/// Enumeration that determines the type of label that is displayed for each pie slice
	/// (see <see cref="PieItem.LabelType"/>).
	/// </summary>
	public enum PieLabelType
	{
		/// <summary>
		/// 
		/// </summary>
		Name_Value,

		/// <summary>
		/// 
		/// </summary>
		Name_Percent,		

		/// <summary>
		/// 
		/// </summary>
		Value,

		/// <summary>
		/// 
		/// </summary>
		Percent,

		/// <summary>
		/// 
		/// </summary>
		Name,

		/// <summary>
		/// 
		/// </summary>
		None
	};
}