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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// The Axis class is an abstract base class that encompasses all properties
	/// and methods required to define a graph Axis.
	/// </summary>
	/// <remarks>This class is inherited by the
	/// <see cref="XAxis"/>, <see cref="YAxis"/>, and <see cref="Y2Axis"/> classes
	/// to define specific characteristics for those types.
	/// </remarks>
	/// 
	/// <author> John Champion modified by Jerry Vos </author>
	/// <version> $Revision: 3.17 $ $Date: 2005-01-22 06:20:49 $ </version>
	[Serializable]
	abstract public class Axis : ISerializable
	{
	#region Class Fields
		/// <summary> Private fields for the <see cref="Axis"/> scale definitions.
		/// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
		/// <see cref="Step"/>, and <see cref="MinorStep"/> for access to these values.
		/// </summary>
		private	double		min,
							max,
							step,
							minorStep;
		/// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
		/// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// <see cref="StepAuto"/>, <see cref="MinorStepAuto"/>, <see cref="MinorStepAuto"/>,
		/// <see cref="NumDecAuto"/>, <see cref="ScaleMagAuto"/>, , <see cref="ScaleFormatAuto"/>
		/// for access to these values.
		/// </summary>
		private	 bool		minAuto,
							maxAuto,
							stepAuto,
							minorStepAuto,
							numDecAuto,
							scaleMagAuto,
							scaleFormatAuto;
		/// <summary> Private fields for the <see cref="Axis"/> "grace" settings.
		/// These values determine how much extra space is left before the first data value
		/// and after the last data value.
		/// Use the public properties <see cref="MinGrace"/> and <see cref="MaxGrace"/>
		/// for access to these values.
		/// </summary>
		private double		minGrace,
							maxGrace;
		/// <summary> Private fields for the <see cref="Axis"/> scale value display.
		/// Use the public properties <see cref="NumDec"/> and <see cref="ScaleMag"/>
		/// for access to these values. </summary>
		private	 int		numDec,
							scaleMag;
		/// <summary> Private fields for the <see cref="Axis"/> attributes.
		/// Use the public properties <see cref="IsVisible"/>, <see cref="IsShowGrid"/>,
		/// <see cref="IsShowMinorGrid"/>, <see cref="IsZeroLine"/>,  <see cref="IsShowTitle"/>,
		/// <see cref="IsTic"/>, <see cref="IsInsideTic"/>, <see cref="IsOppositeTic"/>,
		/// <see cref="IsMinorTic"/>, <see cref="IsMinorInsideTic"/>,
		/// <see cref="IsMinorOppositeTic"/>, <see cref="IsTicsBetweenLabels"/>,
		/// <see cref="IsLog"/>, <see cref="IsReverse"/>,
		/// <see cref="IsOmitMag"/>, <see cref="IsText"/>, <see cref="IsUseTenPower"/>,
		/// and <see cref="IsDate"/> for access to these values.
		/// </summary>
		protected bool		isVisible,
							isShowGrid,
							isShowTitle,
							isZeroLine,
							isTic,
							isInsideTic,
							isOppositeTic,
							isMinorTic,
							isShowMinorGrid,
							isMinorInsideTic,
							isMinorOppositeTic,
							isTicsBetweenLabels,
							isReverse,
							isOmitMag,
							isUseTenPower;
		/// <summary> Private field for the <see cref="Axis"/> type.  This can be one of the
		/// types as defined in the <see cref="AxisType"/> enumeration.
		/// Use the public property <see cref="Type"/>
		/// for access to this value. </summary>
		private AxisType	type;
		/// <summary> Private field for the <see cref="Axis"/> title string.
		/// Use the public property <see cref="Title"/>
		/// for access to this value. </summary>
		protected	string	title;
		/// <summary> Private field for the format of the <see cref="Axis"/> tic labels.
		/// This field is only used if the <see cref="Type"/> is set to <see cref="AxisType.Date"/>.
		/// Use the public property <see cref="ScaleFormat"/>
		/// for access to this value. </summary>
		/// <seealso cref="ScaleFormatAuto"/>
		private		 string	scaleFormat;
		/// <summary> Private field for the alignment of the <see cref="Axis"/> tic labels.
		/// This fields controls whether the inside, center, or outside edges of the text labels are aligned.
		/// Use the public property <see cref="ScaleAlign"/>
		/// for access to this value. </summary>
		/// <seealso cref="ScaleFormatAuto"/>
		private		 AlignP	scaleAlign;
		/// <summary> Private <see cref="System.Collections.ArrayList"/> field for the <see cref="Axis"/> array of text labels.
		/// This property is only used if <see cref="Type"/> is set to
		/// <see cref="AxisType.Text"/> </summary>
		private		 string[]	textLabels = null;
		/// <summary> Private fields for the <see cref="Axis"/> font specificatios.
		/// Use the public properties <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> for access to these values. </summary>
		protected FontSpec	titleFontSpec,
							scaleFontSpec;
		/// <summary> Private fields for the <see cref="Axis"/> drawing dimensions.
		/// Use the public properties <see cref="TicPenWidth"/>, <see cref="TicSize"/>,
		/// <see cref="MinorTicSize"/>,
		/// <see cref="GridDashOn"/>, <see cref="GridDashOff"/>,
		/// <see cref="GridPenWidth"/>,
		/// <see cref="MinorGridDashOn"/>, <see cref="MinorGridDashOff"/>,
		/// and <see cref="MinorGridPenWidth"/> for access to these values. </summary>
		private float		ticPenWidth,
							ticSize,
							minorTicSize,
							gridDashOn,
							gridDashOff,
							gridPenWidth,
							minorGridDashOn,
							minorGridDashOff,
							minorGridPenWidth;
		
		/// <summary>
		/// Private field for the <see cref="Axis"/> minimum allowable space allocation.
		/// Use the public property <see cref="MinSpace"/> to access this value.
		/// </summary>
		/// <seealso cref="Default.MinSpace"/>
		private float		minSpace;
		
		/// <summary> Private fields for the <see cref="Axis"/> colors.
		/// Use the public properties <see cref="Color"/> and
		/// <see cref="GridColor"/> for access to these values. </summary>
		private Color	color,
						gridColor,
						minorGridColor;
		
		/// <summary>
		/// Private fields for Unit types to be used for the major and minor tics.
		/// See <see cref="MajorUnit"/> and <see cref="MinorUnit"/> for the corresponding
		/// public properties.
		/// These types only apply for date-time scales (<see cref="IsDate"/>).
		/// </summary>
		/// <value>The value of these types is of enumeration type <see cref="DateUnit"/>
		/// </value>
		private DateUnit	majorUnit,
							minorUnit;

		/// <summary>
		/// Scale values for calculating transforms.  These are temporary values
		/// used only during the Draw process.
		/// </summary>
		private float	minPix,
						maxPix;

		/// <summary>
		/// The pixel position at the minimum value for this axis.  This read-only
		/// value is used/valid only during the Draw process.
		/// </summary>
		public float	MinPix
		{
			get { return minPix; }
		}
		/// <summary>
		/// The pixel position at the maximum value for this axis.  This read-only
		/// value is used/valid only during the Draw process.
		/// </summary>
		public float	MaxPix
		{
			get { return maxPix; }
		}

		/// <summary>
		/// Scale values for calculating transforms.  These are temporary values
		/// used only during the Draw process.
		/// </summary>
		private double	minScale,
						maxScale;
		
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Axis"/> class.
		/// </summary>
		public struct Default
		{
			// Default Axis Properties
			/// <summary>
			/// The default size for the <see cref="Axis"/> tic marks.
            /// (<see cref="Axis.TicSize"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float TicSize = 5;
			/// <summary>
			/// The default size for the <see cref="Axis"/> minor tic marks.
            /// (<see cref="Axis.MinorTicSize"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float MinorTicSize = 2.5F;
			/// <summary>
			/// The default pen width for drawing the <see cref="Axis"/> tic marks.
            /// (<see cref="Axis.TicPenWidth"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float TicPenWidth = 1.0F;
			/// <summary>
			/// The default "zero lever" for automatically selecting the axis
			/// scale range (see <see cref="Axis.PickScale"/>). This number is
			/// used to determine when an axis scale range should be extended to
			/// include the zero value.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double ZeroLever = 0.25;
			/// <summary> The default "grace" value applied to the minimum data range.
			/// This value is
			/// expressed as a fraction of the total data range.  For example, assume the data
			/// range is from 4.0 to 16.0, leaving a range of 12.0.  If MinGrace is set to
			/// 0.1, then 10% of the range, or 1.2 will be subtracted from the minimum data value.
			/// The scale will then be ranged to cover at least 2.8 to 16.0.
			/// </summary>
			/// <seealso cref="Axis.MinGrace"/>
			public static double MinGrace = 0.1;
			/// <summary> The default "grace" value applied to the maximum data range.
			/// This value is
			/// expressed as a fraction of the total data range.  For example, assume the data
			/// range is from 4.0 to 16.0, leaving a range of 12.0.  If MaxGrace is set to
			/// 0.1, then 10% of the range, or 1.2 will be added to the maximum data value.
			/// The scale will then be ranged to cover at least 4.0 to 17.2.
			/// </summary>
			/// <seealso cref="Axis.MinGrace"/>
			/// <seealso cref="MaxGrace"/>
			public static double MaxGrace = 0.1;
			/// <summary>
			/// The maximum number of text labels (major tics) that will be allowed on the plot by
			/// the automatic scaling logic.  This value applies only to <see cref="AxisType.Text"/>
			/// axes.  If there are more than MaxTextLabels on the plot, then
			/// <see cref="Axis.Step"/> will be increased to reduce the number of labels.  That is,
			/// the step size might be increased to 2.0 to show only every other label.
			/// </summary>
			public static double MaxTextLabels = 12.0;
			/// <summary>
			/// The default target number of steps for automatically selecting the X axis
			/// scale step size (see <see cref="Axis.PickScale"/>).
			/// This number is an initial target value for the number of major steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetXSteps = 7.0;
			/// <summary>
			/// The default target number of steps for automatically selecting the Y or Y2 axis
			/// scale step size (see <see cref="Axis.PickScale"/>).
			/// This number is an initial target value for the number of major steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetYSteps = 7.0;
			/// <summary>
			/// The default target number of minor steps for automatically selecting the X axis
			/// scale minor step size (see <see cref="Axis.PickScale"/>).
			/// This number is an initial target value for the number of minor steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetMinorXSteps = 5.0;
			/// <summary>
			/// The default target number of minor steps for automatically selecting the Y or Y2 axis
			/// scale minor step size (see <see cref="Axis.PickScale"/>).
			/// This number is an initial target value for the number of minor steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetMinorYSteps = 5.0;
			/// <summary> The default alignment of the <see cref="Axis"/> tic labels.
			/// This value controls whether the inside, center, or outside edges of the text labels are aligned.
			/// </summary>
			/// <seealso cref="Axis.ScaleAlign"/>
			public static AlignP ScaleAlign = AlignP.Center;
			/// <summary>
			/// The default font family for the <see cref="Axis"/> scale values
			/// font specification <see cref="Axis.ScaleFontSpec"/>
			/// (<see cref="FontSpec.Family"/> property).
			/// </summary>
			public static string ScaleFontFamily = "Arial";
			/// <summary>
			/// The default font size for the <see cref="Axis"/> scale values
			/// font specification <see cref="Axis.ScaleFontSpec"/>
			/// (<see cref="FontSpec.Size"/> property).  Units are
			/// in points (1/72 inch).
			/// </summary>
			public static float ScaleFontSize = 14;
			/// <summary>
			/// The default font color for the <see cref="Axis"/> scale values
			/// font specification <see cref="Axis.ScaleFontSpec"/>
			/// (<see cref="FontSpec.FontColor"/> property).
			/// </summary>
			public static Color ScaleFontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the <see cref="Axis"/> scale values
			/// font specification <see cref="Axis.ScaleFontSpec"/>
			/// (<see cref="FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool ScaleFontBold = false;
			/// <summary>
			/// The default font italic mode for the <see cref="Axis"/> scale values
			/// font specification <see cref="Axis.ScaleFontSpec"/>
			/// (<see cref="FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool ScaleFontItalic = false;
			/// <summary>
			/// The default font underline mode for the <see cref="Axis"/> scale values
			/// font specification <see cref="Axis.ScaleFontSpec"/>
			/// (<see cref="FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool ScaleFontUnderline = false;
			/// <summary>
			/// The default color for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color ScaleFillColor = Color.White;
			/// <summary>
			/// The default custom brush for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush ScaleFillBrush = null;
			/// <summary>
			/// The default fill mode for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType ScaleFillType = FillType.None;
			
			/// <summary>
			/// The default display mode for the <see cref="Axis"/>
			/// <see cref="Title"/> property
			/// (<see cref="Axis.IsShowGrid"/> property). true
			/// to show the title, false to hide it.
			/// </summary>
			public static bool IsShowTitle = true;
			/// <summary>
			/// The default font family for the <see cref="Axis"/> title text
			/// font specification <see cref="Axis.TitleFontSpec"/>
			/// (<see cref="FontSpec.Family"/> property).
			/// </summary>
			public static string TitleFontFamily = "Arial";
			/// <summary>
			/// The default font size for the <see cref="Axis"/> title text
			/// font specification <see cref="Axis.TitleFontSpec"/>
			/// (<see cref="FontSpec.Size"/> property).  Units are
			/// in points (1/72 inch).
			/// </summary>
			public static float TitleFontSize = 14;
			/// <summary>
			/// The default font color for the <see cref="Axis"/> title text
			/// font specification <see cref="Axis.TitleFontSpec"/>
			/// (<see cref="FontSpec.FontColor"/> property).
			/// </summary>
			public static Color TitleFontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the <see cref="Axis"/> title text
			/// font specification <see cref="Axis.TitleFontSpec"/>
			/// (<see cref="FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool TitleFontBold = true;
			/// <summary>
			/// The default font italic mode for the <see cref="Axis"/> title text
			/// font specification <see cref="Axis.TitleFontSpec"/>
			/// (<see cref="FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool TitleFontItalic = false;
			/// <summary>
			/// The default font underline mode for the <see cref="Axis"/> title text
			/// font specification <see cref="Axis.TitleFontSpec"/>
			/// (<see cref="FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool TitleFontUnderline = false;
			/// <summary>
			/// The default color for filling in the title text background
			/// (see <see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color TitleFillColor = Color.White;
			/// <summary>
			/// The default custom brush for filling in the title text background
			/// (see <see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush TitleFillBrush = null;
			/// <summary>
			/// The default fill mode for filling in the title text background
			/// (see <see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType TitleFillType = FillType.None;

			/// <summary>
			/// The default "dash on" size for drawing the <see cref="Axis"/> grid
            /// (<see cref="Axis.GridDashOn"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float GridDashOn = 1.0F;
			/// <summary>
			/// The default "dash off" size for drawing the <see cref="Axis"/> grid
            /// (<see cref="Axis.GridDashOff"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float GridDashOff = 5.0F;
			/// <summary>
			/// The default pen width for drawing the <see cref="Axis"/> grid
            /// (<see cref="Axis.GridPenWidth"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float GridPenWidth = 1.0F;
			/// <summary>
			/// The default color for the <see cref="Axis"/> grid lines
			/// (<see cref="Axis.GridColor"/> property).  This color only affects the
			/// grid lines.
			/// </summary>
			public static Color GridColor = Color.Black;
			/// <summary>
			/// The default "dash on" size for drawing the <see cref="Axis"/> minor grid
            /// (<see cref="Axis.MinorGridDashOn"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float MinorGridDashOn = 1.0F;
			/// <summary>
			/// The default "dash off" size for drawing the <see cref="Axis"/> minor grid
            /// (<see cref="Axis.MinorGridDashOff"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float MinorGridDashOff = 10.0F;
			/// <summary>
			/// The default pen width for drawing the <see cref="Axis"/> minor grid
            /// (<see cref="Axis.MinorGridPenWidth"/> property). Units are in points (1/72 inch).
            /// </summary>
			public static float MinorGridPenWidth = 1.0F;
			/// <summary>
			/// The default color for the <see cref="Axis"/> minor grid lines
			/// (<see cref="Axis.MinorGridColor"/> property).  This color only affects the
			/// minor grid lines.
			/// </summary>
			public static Color MinorGridColor = Color.Gray;
			/// <summary>
			/// The default color for the <see cref="Axis"/> itself
			/// (<see cref="Axis.Color"/> property).  This color only affects the
			/// tic marks and the axis border.
			/// </summary>
			public static Color Color = Color.Black;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> grid lines
			/// (<see cref="Axis.IsShowGrid"/> property). true
			/// to show the grid lines, false to hide them.
			/// </summary>
			public static bool IsShowGrid = false;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> minor grid lines
			/// (<see cref="Axis.IsShowMinorGrid"/> property). true
			/// to show the minor grid lines, false to hide them.
			/// </summary>
			public static bool IsShowMinorGrid = false;
			/// <summary>
			/// The display mode for the <see cref="Axis"/> major outside tic marks
			/// (<see cref="Axis.IsTic"/> property).
			/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
			/// </summary>
			/// <value>true to show the major tic marks (outside the axis),
			/// false otherwise</value>
			public static bool IsTic = true;
			/// <summary>
			/// The display mode for the <see cref="Axis"/> minor outside tic marks
			/// (<see cref="Axis.IsMinorTic"/> property).
			/// The minor tic spacing is controlled by <see cref="Axis.MinorStep"/>.
			/// </summary>
			/// <value>true to show the minor tic marks (outside the axis),
			/// false otherwise</value>
			public static bool IsMinorTic = true;
			/// <summary>
			/// The display mode for the <see cref="Axis"/> major inside tic marks
			/// (<see cref="Axis.IsInsideTic"/> property).
			/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
			/// </summary>
			/// <value>true to show the major tic marks (inside the axis),
			/// false otherwise</value>
			public static bool IsInsideTic = true;
			/// <summary>
			/// The display mode for the <see cref="Axis"/> major opposite tic marks
			/// (<see cref="Axis.IsOppositeTic"/> property).
			/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
			/// </summary>
			/// <value>true to show the major tic marks
			/// (inside the axis on the opposite side),
			/// false otherwise</value>
			public static bool IsOppositeTic = true;
			/// <summary>
			/// The display mode for the <see cref="Axis"/> minor inside tic marks
			/// (<see cref="Axis.IsMinorTic"/> property).
			/// The minor tic spacing is controlled by <see cref="Axis.MinorStep"/>.
			/// </summary>
			/// <value>true to show the minor tic marks (inside the axis),
			/// false otherwise</value>
			public static bool IsMinorInsideTic = true;
			/// <summary>
			/// The display mode for the <see cref="Axis"/> minor opposite tic marks
			/// (<see cref="Axis.IsMinorOppositeTic"/> property).
			/// The minor tic spacing is controlled by <see cref="Axis.MinorStep"/>.
			/// </summary>
			/// <value>true to show the minor tic marks
			/// (inside the axis on the opposite side),
			/// false otherwise</value>
			public static bool IsMinorOppositeTic = true;
			/// <summary>
			/// The default logarithmic mode for the <see cref="Axis"/> scale
			/// (<see cref="Axis.IsLog"/> property). true for a logarithmic scale,
			/// false for a cartesian scale.
			/// </summary>
			public static bool IsLog = false;
			/// <summary>
			/// The default reverse mode for the <see cref="Axis"/> scale
			/// (<see cref="Axis.IsReverse"/> property). true for a reversed scale
			/// (X decreasing to the left, Y/Y2 decreasing upwards), false otherwise.
			/// </summary>
			public static bool IsReverse = false;
			/// <summary>
			/// The default setting for the <see cref="Axis"/> scale axis type
			/// (<see cref="Axis.Type"/> property).  This value is set as per
			/// the <see cref="AxisType"/> enumeration
			/// </summary>
			public static AxisType Type = AxisType.Linear;
			/// <summary>
			/// The default setting for the <see cref="Axis"/> scale date format string
			/// (<see cref="Axis.ScaleFormat"/> property).  This value is set as per
			/// the <see cref="XDate.ToString()"/> function.
			/// </summary>
			//public static string ScaleFormat = "&dd-&mmm-&yy &hh:&nn";
			public static string ScaleFormat = "g";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Year"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Year"/>.
			/// This value normally defaults to 1825 days (5 years).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeYearYear = 1825;  // 5 years
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Year"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Month"/>.
			/// This value normally defaults to 365 days (1 year).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeYearMonth = 365;  // 1 year
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Month"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Month"/>.
			/// This value normally defaults to 90 days (3 months).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeMonthMonth = 90;  // 3 months
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Day"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Day"/>.
			/// This value normally defaults to 10 days.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeDayDay = 10;  // 10 days
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Day"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Hour"/>.
			/// This value normally defaults to 3 days.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeDayHour = 3;  // 3 days
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Hour"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Hour"/>.
			/// This value normally defaults to 0.4167 days (10 hours).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeHourHour = 0.4167;  // 10 hours
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Hour"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Minute"/>.
			/// This value normally defaults to 0.125 days (3 hours).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeHourMinute = 0.125;  // 3 hours
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Minute"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Minute"/>.
			/// This value normally defaults to 6.94e-3 days (10 minutes).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeMinuteMinute = 6.94e-3;  // 10 Minutes
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Minute"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Second"/>.
			/// This value normally defaults to 2.083e-3 days (3 minutes).
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeMinuteSecond = 2.083e-3;  // 3 Minutes
			
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Year"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Year"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatYearYear = "yyyy";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Year"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Month"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatYearMonth = "MMM-yyyy";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Month"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Month"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatMonthMonth = "MMM-yyyy";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Day"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Day"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatDayDay = "d-MMM";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Day"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Hour"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatDayHour = "d-MMM HH:mm";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Hour"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Hour"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatHourHour = "HH:mm";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Hour"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Minute"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatHourMinute = "HH:mm";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Minute"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Minute"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatMinuteMinute = "HH:mm";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Minute"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Second"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatMinuteSecond = "mm:ss";
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Second"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Second"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="Axis.CalcDateStepSize"/> method.
			/// </summary>
			/// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
			public static string FormatSecondSecond = "mm:ss";

			/*  Prior format assignments using original XDate.ToString()
					this.scaleFormat = "&yyyy";
					this.scaleFormat = "&mmm-&yy";
					this.scaleFormat = "&mmm-&yy";
					scaleFormat = "&d-&mmm";
					this.scaleFormat = "&d-&mmm &hh:&nn";
					scaleFormat = "&hh:&nn";
					scaleFormat = "&hh:&nn";
					scaleFormat = "&hh:&nn";
					scaleFormat = "&nn:&ss";
					scaleFormat = "&nn:&ss";
			*/

			/// <summary>
			/// The default setting for the axis space allocation.  This term, expressed in
            /// points (1/72 inch) and scaled according to <see cref="PaneBase.CalcScaleFactor"/> for the
            /// <see cref="GraphPane"/>, determines the minimum amount of space an axis must
			/// have between the <see cref="GraphPane.AxisRect"/> and the
			/// <see cref="PaneBase.PaneRect"/>.  This minimum space
			/// applies whether <see cref="Axis.IsVisible"/> is true or false.
			/// </summary>
			public static float MinSpace = 0f;
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Default constructor for <see cref="Axis"/> that sets all axis properties
		/// to default values as defined in the <see cref="Default"/> class.
		/// </summary>
		public Axis()
		{
			this.min = 0.0;
			this.max = 1.0;
			this.step = 0.1;
			this.minorStep = 0.1;

			this.minGrace = Default.MinGrace;
			this.maxGrace = Default.MaxGrace;
		
			this.minAuto = true;
			this.maxAuto = true;
			this.stepAuto = true;
			this.minorStepAuto = true;
			this.numDecAuto = true;
			this.scaleMagAuto = true;
			this.scaleFormatAuto = true;
		
			this.numDec = 0;
			this.scaleMag = 0;

			this.ticSize = Default.TicSize;
			this.minorTicSize = Default.MinorTicSize;
			this.gridDashOn = Default.GridDashOn;
			this.gridDashOff = Default.GridDashOff;
			this.gridPenWidth = Default.GridPenWidth;
			this.minorGridDashOn = Default.MinorGridDashOn;
			this.minorGridDashOff = Default.MinorGridDashOff;
			this.minorGridPenWidth = Default.MinorGridPenWidth;
		
			this.minSpace = Default.MinSpace;
			this.isVisible = true;
			this.isShowTitle = Default.IsShowTitle;
			this.isShowGrid = Default.IsShowGrid;
			this.isShowMinorGrid = Default.IsShowMinorGrid;
			this.isReverse = Default.IsReverse;
			this.isOmitMag = false;
			this.isTic = Default.IsTic;
			this.isInsideTic = Default.IsInsideTic;
			this.isOppositeTic = Default.IsOppositeTic;
			this.isMinorTic = Default.IsMinorTic;
			this.isMinorInsideTic = Default.IsMinorInsideTic;
			this.isMinorOppositeTic = Default.IsMinorOppositeTic;
			this.isTicsBetweenLabels = false;
			this.isUseTenPower = true;
		
			this.type = Default.Type;
			this.title = "";
			this.TextLabels = null;
			this.scaleFormat = null;
			this.scaleAlign = Default.ScaleAlign;
			
			this.majorUnit = DateUnit.Year;
			this.minorUnit = DateUnit.Year;

			this.ticPenWidth = Default.TicPenWidth;
			this.color = Default.Color;
			this.gridColor = Default.GridColor;
			this.minorGridColor = Default.MinorGridColor;
			
			this.titleFontSpec = new FontSpec(
					Default.TitleFontFamily, Default.TitleFontSize,
					Default.TitleFontColor, Default.TitleFontBold,
					Default.TitleFontUnderline, Default.TitleFontItalic,
					Default.TitleFillColor, Default.TitleFillBrush,
					Default.TitleFillType );

			this.titleFontSpec.Border.IsVisible = false;

			this.scaleFontSpec = new FontSpec(
				Default.ScaleFontFamily, Default.ScaleFontSize,
				Default.ScaleFontColor, Default.ScaleFontBold,
				Default.ScaleFontUnderline, Default.ScaleFontItalic,
				Default.ScaleFillColor, Default.ScaleFillBrush,
				Default.ScaleFillType );
			this.scaleFontSpec.Border.IsVisible = false;
		}

		/// <summary>
		/// The Copy Constructor.
		/// </summary>
		/// <param name="rhs">The Axis object from which to copy</param>
		public Axis( Axis rhs )
		{
			min = rhs.Min;
			max = rhs.Max;
			step = rhs.Step;
			minorStep = rhs.MinorStep;
			minAuto = rhs.MinAuto;
			maxAuto = rhs.MaxAuto;
			stepAuto = rhs.StepAuto;
			minorStepAuto = rhs.MinorStepAuto;
			numDecAuto = rhs.NumDecAuto;
			scaleMagAuto = rhs.ScaleMagAuto;
			scaleFormatAuto = rhs.ScaleFormatAuto;

			minGrace = rhs.MinGrace;
			maxGrace = rhs.MaxGrace;

			numDec = rhs.numDec;
			scaleMag = rhs.scaleMag;
			isVisible = rhs.IsVisible;
			isShowTitle = rhs.IsShowTitle;
			isShowGrid = rhs.IsShowGrid;
			isShowMinorGrid = rhs.IsShowMinorGrid;
			isZeroLine = rhs.IsZeroLine;
			isTic = rhs.IsTic;
			isInsideTic = rhs.IsInsideTic;
			isOppositeTic = rhs.IsOppositeTic;
			isMinorTic = rhs.IsMinorTic;
			isMinorInsideTic = rhs.IsMinorInsideTic;
			isMinorOppositeTic = rhs.IsMinorOppositeTic;
			isTicsBetweenLabels = rhs.IsTicsBetweenLabels;
			isUseTenPower = rhs.IsUseTenPower;

			isReverse = rhs.IsReverse;
			isOmitMag = rhs.IsOmitMag;
			title = rhs.Title;
			
			type = rhs.Type;
			
			majorUnit = rhs.MajorUnit;
			minorUnit = rhs.MinorUnit;
			
			if ( rhs.TextLabels != null )
				TextLabels = (string[]) rhs.TextLabels.Clone();
			else
				TextLabels = null;

			scaleFormat = rhs.scaleFormat;
			scaleAlign = rhs.scaleAlign;

			titleFontSpec = (FontSpec) rhs.TitleFontSpec.Clone();
			scaleFontSpec = (FontSpec) rhs.ScaleFontSpec.Clone();

			ticPenWidth = rhs.TicPenWidth;
			ticSize = rhs.TicSize;
			minorTicSize = rhs.MinorTicSize;
			gridDashOn = rhs.GridDashOn;
			gridDashOff = rhs.GridDashOff;
			gridPenWidth = rhs.GridPenWidth;
			minorGridDashOn = rhs.MinorGridDashOn;
			minorGridDashOff = rhs.MinorGridDashOff;
			minorGridPenWidth = rhs.MinorGridPenWidth;
			
			minSpace = rhs.MinSpace;

			color = rhs.Color;
			gridColor = rhs.GridColor;
			minorGridColor = rhs.MinorGridColor;
		} 
	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected Axis( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			min = info.GetDouble( "min" );
			max = info.GetDouble( "max" );
			step = info.GetDouble( "step" );
			minorStep = info.GetDouble( "minorStep" );

			minAuto = info.GetBoolean( "minAuto" );
			maxAuto = info.GetBoolean( "maxAuto" );
			stepAuto = info.GetBoolean( "stepAuto" );
			minorStepAuto = info.GetBoolean( "minorStepAuto" );
			numDecAuto = info.GetBoolean( "numDecAuto" );
			scaleMagAuto = info.GetBoolean( "scaleMagAuto" );
			scaleFormatAuto = info.GetBoolean( "scaleFormatAuto" );
			
			minGrace = info.GetDouble( "minGrace" );
			maxGrace = info.GetDouble( "maxGrace" );

			numDec = info.GetInt32( "numDec" );
			scaleMag = info.GetInt32( "scaleMag" );

			isVisible = info.GetBoolean( "isVisible" );
			isShowGrid = info.GetBoolean( "isShowGrid" );
			isShowTitle = info.GetBoolean( "isShowTitle" );
			isZeroLine = info.GetBoolean( "isZeroLine" );
			isTic = info.GetBoolean( "isTic" );
			isInsideTic = info.GetBoolean( "isInsideTic" );
			isOppositeTic = info.GetBoolean( "isOppositeTic" );
			isMinorTic = info.GetBoolean( "isMinorTic" );
			isShowMinorGrid = info.GetBoolean( "isShowMinorGrid" );
			isMinorInsideTic = info.GetBoolean( "isMinorInsideTic" );
			isMinorOppositeTic = info.GetBoolean( "isMinorOppositeTic" );
			isTicsBetweenLabels = info.GetBoolean( "isTicsBetweenLabels" );
			isReverse = info.GetBoolean( "isReverse" );
			isOmitMag = info.GetBoolean( "isOmitMag" );
			isUseTenPower = info.GetBoolean( "isUseTenPower" );

			type = (AxisType) info.GetValue( "type", typeof(AxisType) );
			title = info.GetString( "title" );
			scaleFormat = info.GetString( "scaleFormat" );

			scaleAlign = (AlignP) info.GetValue( "scaleAlign", typeof(AlignP) );
			textLabels = (string[]) info.GetValue( "textLabels", typeof(string[]) );

			titleFontSpec = (FontSpec) info.GetValue( "titleFontSpec", typeof(FontSpec) );
			scaleFontSpec = (FontSpec) info.GetValue( "scaleFontSpec", typeof(FontSpec) );

			ticPenWidth = info.GetSingle( "ticPenWidth" );
			ticSize = info.GetSingle( "ticSize" );
			minorTicSize = info.GetSingle( "minorTicSize" );
			gridDashOn = info.GetSingle( "gridDashOn" );
			gridDashOff = info.GetSingle( "gridDashOff" );
			gridPenWidth = info.GetSingle( "gridPenWidth" );
			minorGridDashOn = info.GetSingle( "minorGridDashOn" );
			minorGridDashOff = info.GetSingle( "minorGridDashOff" );
			minorGridPenWidth = info.GetSingle( "minorGridPenWidth" );
			minSpace = info.GetSingle( "minSpace" );

			color = (Color) info.GetValue( "color", typeof(Color) );
			gridColor = (Color) info.GetValue( "gridColor", typeof(Color) );
			minorGridColor = (Color) info.GetValue( "minorGridColor", typeof(Color) );

			majorUnit = (DateUnit) info.GetValue( "majorUnit", typeof(DateUnit) );
			minorUnit = (DateUnit) info.GetValue( "minorUnit", typeof(DateUnit) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );
			info.AddValue( "min", min );
			info.AddValue( "max", max );
			info.AddValue( "step", step );
			info.AddValue( "minorStep", minorStep );

			info.AddValue( "minAuto", minAuto );
			info.AddValue( "maxAuto", maxAuto );
			info.AddValue( "stepAuto", stepAuto );
			info.AddValue( "minorStepAuto", minorStepAuto );
			info.AddValue( "numDecAuto", numDecAuto );
			info.AddValue( "scaleMagAuto", scaleMagAuto );
			info.AddValue( "scaleFormatAuto", scaleFormatAuto );

			info.AddValue( "minGrace", minGrace );
			info.AddValue( "maxGrace", maxGrace );

			info.AddValue( "numDec", numDec );
			info.AddValue( "scaleMag", scaleMag );

			info.AddValue( "isVisible", isVisible );
			info.AddValue( "isShowGrid", isShowGrid );
			info.AddValue( "isShowTitle", isShowTitle );
			info.AddValue( "isZeroLine", isZeroLine );
			info.AddValue( "isTic", isTic );
			info.AddValue( "isInsideTic", isInsideTic );
			info.AddValue( "isOppositeTic", isOppositeTic );
			info.AddValue( "isMinorTic", isMinorTic );
			info.AddValue( "isShowMinorGrid", isShowMinorGrid );
			info.AddValue( "isMinorInsideTic", isMinorInsideTic );
			info.AddValue( "isMinorOppositeTic", isMinorOppositeTic );
			info.AddValue( "isTicsBetweenLabels", isTicsBetweenLabels );
			info.AddValue( "isReverse", isReverse );
			info.AddValue( "isOmitMag", isOmitMag );
			info.AddValue( "isUseTenPower", isUseTenPower );

			info.AddValue( "type", type );
			info.AddValue( "title", title );
			info.AddValue( "scaleFormat", scaleFormat );
			info.AddValue( "scaleAlign", scaleAlign );
			info.AddValue( "textLabels", textLabels );
			info.AddValue( "titleFontSpec", titleFontSpec );
			info.AddValue( "scaleFontSpec", scaleFontSpec );

			info.AddValue( "ticPenWidth", ticPenWidth );
			info.AddValue( "ticSize", ticSize );
			info.AddValue( "minorTicSize", minorTicSize );
			info.AddValue( "gridDashOn", gridDashOn );
			info.AddValue( "gridDashOff", gridDashOff );
			info.AddValue( "gridPenWidth", gridPenWidth );
			info.AddValue( "minorGridDashOn", minorGridDashOn );
			info.AddValue( "minorGridDashOff", minorGridDashOff );
			info.AddValue( "minorGridPenWidth", minorGridPenWidth );
			info.AddValue( "minSpace", minSpace );

			info.AddValue( "color", color );
			info.AddValue( "gridColor", gridColor );
			info.AddValue( "minorGridColor", minorGridColor );

			info.AddValue( "majorUnit", majorUnit );
			info.AddValue( "minorUnit", minorUnit );
		}
	#endregion

	#region Scale Properties
		/// <summary>
		/// Gets or sets the minimum scale value for this axis.
		/// </summary>
		/// <remarks>This value can be set
		/// automatically based on the state of <see cref="MinAuto"/>.  If
		/// this value is set manually, then <see cref="MinAuto"/> will
		/// also be set to false.
		/// </remarks>
		/// <value> The value is defined in user scale units for <see cref="AxisType.Log"/>
		/// and <see cref="AxisType.Linear"/> axes. For <see cref="AxisType.Text"/>
		/// and <see cref="AxisType.Ordinal"/> axes,
		/// this value is an ordinal starting with 1.0.  For <see cref="AxisType.Date"/>
		/// axes, this value is in XL Date format (see <see cref="XDate"/>, which is the
		/// number of days since the reference date of January 1, 1900.</value>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="MinAuto"/>
		public double Min
		{
			get { return min; }
			set { min = value; this.minAuto = false; }
		}
		/// <summary>
		/// Gets or sets the maximum scale value for this axis.
		/// </summary>
		/// <remarks>
		/// This value can be set
		/// automatically based on the state of <see cref="MaxAuto"/>.  If
		/// this value is set manually, then <see cref="MaxAuto"/> will
		/// also be set to false.
		/// </remarks>
		/// <value> The value is defined in user scale units for <see cref="AxisType.Log"/>
		/// and <see cref="AxisType.Linear"/> axes. For <see cref="AxisType.Text"/>
		/// and <see cref="AxisType.Ordinal"/> axes,
		/// this value is an ordinal starting with 1.0.  For <see cref="AxisType.Date"/>
		/// axes, this value is in XL Date format (see <see cref="XDate"/>, which is the
		/// number of days since the reference date of January 1, 1900.</value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="MaxAuto"/>
		public double Max
		{
			get { return max; }
			set { max = value; this.maxAuto = false; }
		}
		/// <summary>
		/// Gets or sets the scale step size for this axis (the increment between
		/// labeled axis values).
		/// </summary>
		/// <remarks>
		/// This value can be set
		/// automatically based on the state of <see cref="StepAuto"/>.  If
		/// this value is set manually, then <see cref="StepAuto"/> will
		/// also be set to false.  This value is ignored for <see cref="AxisType.Log"/>
		/// axes.  For <see cref="AxisType.Date"/> axes, this
		/// value is defined in units of <see cref="MajorUnit"/>.
		/// </remarks>
		/// <value> The value is defined in user scale units </value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="StepAuto"/>
		/// <seealso cref="Default.TargetXSteps"/>
		/// <seealso cref="Default.TargetYSteps"/>
		/// <seealso cref="Default.ZeroLever"/>
		/// <seealso cref="Default.MaxTextLabels"/>
		public double Step
		{
			get { return step; }
			set { step = value; this.stepAuto = false; }
		}
		/// <summary>
		/// Gets or sets the type of units used for the major step size (<see cref="Step"/>).
		/// </summary>
		/// <remarks>
		/// This unit type only applies to Date-Time axes (<see cref="AxisType.Date"/> = true).
		/// The axis is set to date type with the <see cref="Type"/> property.
		/// The unit types are defined as <see cref="DateUnit"/>.
		/// </remarks>
		/// <value> The value is a <see cref="DateUnit"/> enum type </value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="StepAuto"/>
		public DateUnit MajorUnit
		{
			get { return majorUnit; }
			set { majorUnit = value; }
		}
		/// <summary>
		/// Gets or sets the type of units used for the minor step size (<see cref="MinorStep"/>).
		/// </summary>
		/// <remarks>
		/// This unit type only applies to Date-Time axes (<see cref="AxisType.Date"/> = true).
		/// The axis is set to date type with the <see cref="Type"/> property.
		/// The unit types are defined as <see cref="DateUnit"/>.
		/// </remarks>
		/// <value> The value is a <see cref="DateUnit"/> enum type </value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="MinorStepAuto"/>
		public DateUnit MinorUnit
		{
			get { return minorUnit; }
			set { minorUnit = value; }
		}
		/// <summary>
		/// Gets or sets the scale minor step size for this axis (the spacing between
		/// minor tics).
		/// </summary>
		/// <remarks>This value can be set
		/// automatically based on the state of <see cref="MinorStepAuto"/>.  If
		/// this value is set manually, then <see cref="MinorStepAuto"/> will
		/// also be set to false.  This value is ignored for <see cref="AxisType.Log"/> and
		/// <see cref="AxisType.Text"/> axes.  For <see cref="AxisType.Date"/> axes, this
		/// value is defined in units of <see cref="MinorUnit"/>.
		/// </remarks>
		/// <value> The value is defined in user scale units </value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="MinorStepAuto"/>
		public double MinorStep
		{
			get { return minorStep; }
			set { minorStep = value; this.minorStepAuto = false; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not the minimum scale value <see cref="Min"/>
		/// is set automatically.
		/// </summary>
		/// <remarks>
		/// This value will be set to false if
		/// <see cref="Min"/> is manually changed.
		/// </remarks>
		/// <value>true for automatic mode, false for manual mode</value>
		/// <seealso cref="Min"/>
		public bool MinAuto
		{
			get { return minAuto; }
			set { minAuto = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not the maximum scale value <see cref="Max"/>
		/// is set automatically.
		/// </summary>
		/// <remarks>
		/// This value will be set to false if
		/// <see cref="Max"/> is manually changed.
		/// </remarks>
		/// <value>true for automatic mode, false for manual mode</value>
		/// <seealso cref="Max"/>
		public bool MaxAuto
		{
			get { return maxAuto; }
			set { maxAuto = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not the scale step size <see cref="Step"/>
		/// is set automatically.
		/// </summary>
		/// <remarks>
		/// This value will be set to false if
		/// <see cref="Step"/> is manually changed.
		/// </remarks>
		/// <value>true for automatic mode, false for manual mode</value>
		/// <seealso cref="Step"/>
		public bool StepAuto
		{
			get { return stepAuto; }
			set { stepAuto = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not the minor scale step size <see cref="MinorStep"/>
		/// is set automatically.
		/// </summary>
		/// <remarks>
		/// This value will be set to false if
		/// <see cref="MinorStep"/> is manually changed.
		/// </remarks>
		/// <value>true for automatic mode, false for manual mode</value>
		/// <seealso cref="MinorStep"/>
		public bool MinorStepAuto
		{
			get { return minorStepAuto; }
			set { minorStepAuto = value; }
		}
		/// <summary> Gets or sets the "grace" value applied to the minimum data range.
		/// </summary>
		/// <remarks>
		/// This value is
		/// expressed as a fraction of the total data range.  For example, assume the data
		/// range is from 4.0 to 16.0, leaving a range of 12.0.  If MinGrace is set to
		/// 0.1, then 10% of the range, or 1.2 will be subtracted from the minimum data value.
		/// The scale will then be ranged to cover at least 2.8 to 16.0.
		/// </remarks>
		/// <seealso cref="Min"/>
		/// <seealso cref="Default.MinGrace"/>
		/// <seealso cref="MaxGrace"/>
		public double MinGrace
		{
			get { return minGrace; }
			set { minGrace = value; }
		}
		/// <summary> Gets or sets the "grace" value applied to the maximum data range.
		/// </summary>
		/// <remarks>
		/// This values determines how much extra space is left after the last data value.
		/// This value is
		/// expressed as a fraction of the total data range.  For example, assume the data
		/// range is from 4.0 to 16.0, leaving a range of 12.0.  If MaxGrace is set to
		/// 0.1, then 10% of the range, or 1.2 will be added to the maximum data value.
		/// The scale will then be ranged to cover at least 4.0 to 17.2.
		/// </remarks>
		/// <seealso cref="Max"/>
		/// <seealso cref="Default.MaxGrace"/>
		/// <seealso cref="MinGrace"/>
		public double MaxGrace
		{
			get { return maxGrace; }
			set { maxGrace = value; }
		}
		
		/// <summary>
		/// Gets or sets the minimum axis space allocation.
		/// </summary>
		/// <remarks>
		/// This term, expressed in
        /// points (1/72 inch) and scaled according to <see cref="PaneBase.CalcScaleFactor"/>
        /// for the <see cref="GraphPane"/>, determines the minimum amount of space
		/// an axis must have between the <see cref="GraphPane.AxisRect"/> and the
		/// <see cref="PaneBase.PaneRect"/>.  This minimum space
		/// applies whether <see cref="IsVisible"/> is true or false.
		/// </remarks>
		public float MinSpace
		{
			get { return minSpace; }
			set { minSpace = value; }
		}

	#endregion

	#region Tic Properties
		/// <summary>
		/// The color to use for drawing this <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// This affects only the tic
		/// marks, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </remarks>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		/// <seealso cref="Default.Color"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsVisible"/>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		/// <summary>
		/// The length of the <see cref="Axis"/> tic marks.
		/// </summary>
		/// <remarks>
		/// This length will be scaled
		/// according to the <see cref="PaneBase.CalcScaleFactor"/> for the
		/// <see cref="GraphPane"/>
		/// </remarks>
        /// <value>The tic size is measured in points (1/72 inch)</value>
        /// <seealso cref="Default.TicSize"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsVisible"/>
		/// <seealso cref="Color"/>
		public float TicSize
		{
			get { return ticSize; }
			set { ticSize = value; }
		}
		/// <summary>
		/// The length of the <see cref="Axis"/> minor tic marks.
		/// </summary>
		/// <remarks>
		/// This length will be scaled
		/// according to the <see cref="PaneBase.CalcScaleFactor"/> for the
		/// <see cref="GraphPane"/>
		/// </remarks>
        /// <value>The tic size is measured in points (1/72 inch)</value>
        /// <seealso cref="Default.MinorTicSize"/>.
		/// <seealso cref="IsMinorTic"/>
		public float MinorTicSize
		{
			get { return minorTicSize; }
			set { minorTicSize = value; }
		}
		/// <summary>
		/// Calculate the scaled tic size for this <see cref="Axis"/>
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
        /// <returns>The scaled tic size, in points (1/72 inch)</returns>
        /// <seealso cref="TicSize"/>
		/// <seealso cref="MinorTicSize"/>
		/// <seealso cref="TitleFontSpec"/>
		/// <seealso cref="ScaleFontSpec"/>
		/// <seealso cref="PaneBase.CalcScaleFactor"/>
		public float ScaledTic( float scaleFactor )
		{
			return (float) ( this.ticSize * scaleFactor + 0.5 );
		}
		/// <summary>
		/// Calculate the scaled minor tic size for this <see cref="Axis"/>
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
        /// <returns>The scaled tic size, in points (1/72 inch)</returns>
        /// <seealso cref="MinorTicSize"/>
		/// <seealso cref="PaneBase.CalcScaleFactor"/>
		public float ScaledMinorTic( float scaleFactor )
		{
			return (float) ( this.minorTicSize * scaleFactor + 0.5 );
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the major outside tic marks
		/// are shown.
		/// </summary>
		/// <remarks>
		/// These are the tic marks on the outside of the <see cref="Axis"/> border.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </remarks>
		/// <value>true to show the major outside tic marks, false otherwise</value>
		/// <seealso cref="Default.IsTic"/>.
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		public bool IsTic
		{
			get { return isTic; }
			set { isTic = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the minor outside tic marks
		/// are shown.
		/// </summary>
		/// <remarks>
		/// These are the tic marks on the outside of the <see cref="Axis"/> border.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.  This setting is
		/// ignored (no minor tics are drawn) for text axes (see <see cref="IsText"/>).
		/// </remarks>
		/// <value>true to show the minor outside tic marks, false otherwise</value>
		/// <seealso cref="Default.IsMinorTic"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		public bool IsMinorTic
		{
			get { return isMinorTic; }
			set { isMinorTic = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the major inside tic marks
		/// are shown.
		/// </summary>
		/// <remarks>
		/// These are the tic marks on the inside of the <see cref="Axis"/> border.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </remarks>
		/// <value>true to show the major inside tic marks, false otherwise</value>
		/// <seealso cref="Default.IsInsideTic"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		public bool IsInsideTic
		{
			get { return isInsideTic; }
			set { isInsideTic = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the major opposite tic marks
		/// are shown.
		/// </summary>
		/// <remarks>
		/// These are the tic marks on the inside of the <see cref="Axis"/> border on
		/// the opposite side from the axis.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </remarks>
		/// <value>true to show the major opposite tic marks, false otherwise</value>
		/// <seealso cref="Default.IsOppositeTic"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		public bool IsOppositeTic
		{
			get { return isOppositeTic; }
			set { isOppositeTic = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the minor inside tic marks
		/// are shown.
		/// </summary>
		/// <remarks>
		/// These are the tic marks on the inside of the <see cref="Axis"/> border.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.
		/// </remarks>
		/// <value>true to show the minor inside tic marks, false otherwise</value>
		/// <seealso cref="Default.IsMinorInsideTic"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		public bool IsMinorInsideTic
		{
			get { return isMinorInsideTic; }
			set { isMinorInsideTic = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the minor opposite tic marks
		/// are shown.
		/// </summary>
		/// <remarks>
		/// These are the tic marks on the inside of the <see cref="Axis"/> border on
		/// the opposite side from the axis.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.
		/// </remarks>
		/// <value>true to show the minor opposite tic marks, false otherwise</value>
		/// <seealso cref="Default.IsMinorOppositeTic"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		public bool IsMinorOppositeTic
		{
			get { return isMinorOppositeTic; }
			set { isMinorOppositeTic = value; }
		}
		/// <summary>
		/// Gets or sets a property that determines whether or not the major tics will be drawn
		/// inbetween the labels, rather than right at the labels.
		/// </summary>
		/// <remarks>
		/// Note that this setting is only
		/// applicable if <see cref="Axis.Type"/> = <see cref="AxisType.Text"/>.
		/// </remarks>
		/// <value>true to place the text between the labels for text axes, false otherwise</value>
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		public bool IsTicsBetweenLabels
		{
			get { return isTicsBetweenLabels; }
			set { isTicsBetweenLabels = value; }
		}
		/// <summary>
		/// Gets or sets the pen width to be used when drawing the tic marks for
		/// this <see cref="Axis"/>
		/// </summary>
        /// <value>The pen width is defined in points (1/72 inch)</value>
        /// <seealso cref="Default.TicPenWidth"/>.
		/// <seealso cref="IsTic"/>
		/// <seealso cref="Color"/>
		public float TicPenWidth
		{
			get { return ticPenWidth; }
			set { ticPenWidth = value; }
		}
	#endregion

	#region Grid Properties

		/// <summary>
		/// Gets or sets a value that determines if the major <see cref="Axis"/> gridlines
		/// (at each labeled value) will be visible
		/// </summary>
		/// <value>true to show the gridlines, false otherwise</value>
		/// <seealso cref="Default.IsShowGrid">Default.IsShowGrid</seealso>.
		/// <seealso cref="GridColor"/>
		/// <seealso cref="GridPenWidth"/>
		/// <seealso cref="GridDashOn"/>
		/// <seealso cref="GridDashOff"/>
		/// <seealso cref="IsVisible"/>
		public bool IsShowGrid
		{
			get { return isShowGrid; }
			set { isShowGrid = value; }
		}

		/// <summary>
		/// Gets or sets a boolean value that determines if a line will be drawn at the
		/// zero value for the axis.
		/// </summary>
		/// <remarks>
		/// The zero line is a line that divides the negative values from the positive values.
		/// The default is set according to
		/// <see cref="XAxis.Default.IsZeroLine"/>, <see cref="YAxis.Default.IsZeroLine"/>,
		/// <see cref="Y2Axis.Default.IsZeroLine"/>,
		/// </remarks>
		/// <value>true to show the zero line, false otherwise</value>
		public bool IsZeroLine
		{
			get { return isZeroLine; }
			set { isZeroLine = value; }
		}

		/// <summary>
		/// The "Dash On" mode for drawing the grid.
		/// </summary>
		/// <remarks>
		/// This is the distance,
        /// in points (1/72 inch), of the dash segments that make up the dashed grid lines.
        /// </remarks>
        /// <value>The dash on length is defined in points (1/72 inch)</value>
        /// <seealso cref="GridDashOff"/>
		/// <seealso cref="IsShowGrid"/>
		/// <seealso cref="Default.GridDashOn"/>.
		public float GridDashOn
		{
			get { return gridDashOn; }
			set { gridDashOn = value; }
		}
		/// <summary>
		/// The "Dash Off" mode for drawing the grid.
		/// </summary>
		/// <remarks>
		/// This is the distance,
        /// in points (1/72 inch), of the spaces between the dash segments that make up
        /// the dashed grid lines.
		/// </remarks>
        /// <value>The dash off length is defined in points (1/72 inch)</value>
        /// <seealso cref="GridDashOn"/>
		/// <seealso cref="IsShowGrid"/>
		/// <seealso cref="Default.GridDashOff"/>.
		public float GridDashOff
		{
			get { return gridDashOff; }
			set { gridDashOff = value; }
		}
		/// <summary>
		/// The default pen width used for drawing the grid lines.
		/// </summary>
        /// <value>The grid pen width is defined in points (1/72 inch)</value>
        /// <seealso cref="IsShowGrid"/>
		/// <seealso cref="Default.GridPenWidth"/>.
		/// <seealso cref="GridColor"/>
		public float GridPenWidth
		{
			get { return gridPenWidth; }
			set { gridPenWidth = value; }
		}
		/// <summary>
		/// The color to use for drawing this <see cref="Axis"/> grid.
		/// </summary>
		/// <remarks>
		/// This affects only the grid
		/// lines, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </remarks>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		/// <seealso cref="Default.GridColor"/>.
		/// <seealso cref="GridPenWidth"/>
		public Color GridColor
		{
			get { return gridColor; }
			set { gridColor = value; }
		}
	#endregion

	#region Minor Grid Properties

		/// <summary>
		/// Gets or sets a value that determines if the minor <see cref="Axis"/> gridlines
		/// (in between each labeled value) will be visible
		/// </summary>
		/// <value>true to show the minor gridlines, false otherwise</value>
		/// <seealso cref="Default.IsShowMinorGrid">Default.IsShowMinorGrid</seealso>.
		/// <seealso cref="MinorGridColor"/>
		/// <seealso cref="MinorGridPenWidth"/>
		/// <seealso cref="MinorGridDashOn"/>
		/// <seealso cref="MinorGridDashOff"/>
		/// <seealso cref="IsVisible"/>
		public bool IsShowMinorGrid
		{
			get { return isShowMinorGrid; }
			set { isShowMinorGrid = value; }
		}

		/// <summary>
		/// Gets or sets the "Dash On" mode for drawing the minor grid.
		/// </summary>
		/// <remarks>
		/// This is the distance,
        /// in points (1/72 inch), of the dash segments that make up the dashed grid lines.
        /// </remarks>
        /// <value>The dash on length is defined in points (1/72 inch)</value>
        /// <seealso cref="MinorGridDashOff"/>
		/// <seealso cref="IsShowMinorGrid"/>
		/// <seealso cref="Default.MinorGridDashOn"/>.
		public float MinorGridDashOn
		{
			get { return minorGridDashOn; }
			set { minorGridDashOn = value; }
		}
		/// <summary>
		/// Gets or sets the "Dash Off" mode for drawing the minor grid.
		/// </summary>
		/// <remarks>
		/// This is the distance,
        /// in points (1/72 inch), of the spaces between the dash segments that make up
        /// the dashed grid lines.
		/// </remarks>
        /// <value>The dash off length is defined in points (1/72 inch)</value>
        /// <seealso cref="MinorGridDashOn"/>
		/// <seealso cref="IsShowMinorGrid"/>
		/// <seealso cref="Default.MinorGridDashOff"/>.
		public float MinorGridDashOff
		{
			get { return minorGridDashOff; }
			set { minorGridDashOff = value; }
		}
		/// <summary>
		/// Gets or sets the default pen width used for drawing the minor grid lines.
		/// </summary>
        /// <value>The grid pen width is defined in points (1/72 inch)</value>
        /// <seealso cref="IsShowMinorGrid"/>
		/// <seealso cref="Default.MinorGridPenWidth"/>.
		/// <seealso cref="MinorGridColor"/>
		public float MinorGridPenWidth
		{
			get { return minorGridPenWidth; }
			set { minorGridPenWidth = value; }
		}
		/// <summary>
		/// Gets or sets the color to use for drawing this <see cref="Axis"/> minor grid.
		/// </summary>
		/// <remarks>
		/// This affects only the minor grid
		/// lines, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </remarks>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		/// <seealso cref="Default.MinorGridColor"/>.
		/// <seealso cref="MinorGridPenWidth"/>
		public Color MinorGridColor
		{
			get { return minorGridColor; }
			set { minorGridColor = value; }
		}
	#endregion

	#region Type Properties
		/// <summary>
		/// This property determines whether or not the <see cref="Axis"/> is shown.
		/// </summary>
		/// <remarks>
		/// Note that even if
		/// the axis is not visible, it can still be actively used to draw curves on a
		/// graph, it will just be invisible to the user
		/// </remarks>
		/// <value>true to show the axis, false to disable all drawing of this axis</value>
		/// <seealso cref="XAxis.Default.IsVisible"/>.
		/// <seealso cref="YAxis.Default.IsVisible"/>.
		/// <seealso cref="Y2Axis.Default.IsVisible"/>.
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		/// <summary>
		/// Determines if the scale values are reversed for this <see cref="Axis"/>
		/// </summary>
		/// <value>true for the X values to decrease to the right or the Y values to
		/// decrease upwards, false otherwise</value>
		/// <seealso cref="Default.IsReverse"/>.
		public bool IsReverse
		{
			get { return isReverse; }
			set { isReverse = value; }
		}
		/// <summary>
		/// Gets a property that indicates if this <see cref="Axis"/> is logarithmic (base 10).
		/// </summary>
		/// <remarks>
		/// To make this property
		/// true, set <see cref="Type"/> to <see cref="AxisType.Log"/>.
		/// </remarks>
		/// <value>true for a logarithmic axis, false for a linear, date, or text axis</value>
		/// <seealso cref="Type"/>
		/// <seealso cref="AxisType"/>
		public bool IsLog
		{
			get { return type == AxisType.Log; }
		}
		/// <summary>
		/// Determines if this <see cref="Axis"/> is of the date-time type.
		/// </summary>
		/// <remarks>
		/// To make this property
		/// true, set <see cref="Type"/> to <see cref="AxisType.Date"/>.
		/// </remarks>
		/// <value>true for a date axis, false for a linear, log, or text axis</value>
		/// <seealso cref="Type"/>
		/// <seealso cref="AxisType"/>
		public bool IsDate
		{
			get { return type == AxisType.Date; }
		}
		/// <summary>
		/// Tests if this <see cref="Axis"/> is labeled with user provided text
		/// labels rather than calculated numeric values.
		/// </summary>
		/// <remarks>
		/// The text labels are provided via the
		/// <see cref="TextLabels"/> property.  Internally, the axis is still handled with ordinal values
		/// such that the axis <see cref="Min"/> is set to 1.0, and the axis <see cref="Max"/> is set
		/// to the number of labels.  To make this property true, set <see cref="Type"/> to
		/// <see cref="AxisType.Text"/>.
		/// </remarks>
		/// <value>true for a text-based axis, false for a linear, log, or date axes.
		/// If this property is true, then you should also provide
		/// an array of labels via <see cref="TextLabels"/>.
		/// </value>
		/// <seealso cref="Type"/>
		/// <seealso cref="AxisType"/>
		public bool IsText
		{
			get { return type == AxisType.Text; }
		}
		/// <summary>
		/// Tests if this <see cref="Axis"/> is an <see cref="AxisType.Ordinal"/> type axis
		/// with numeric labels.
		/// </summary>
		/// <remarks>
		/// This is similar to a <see cref="AxisType.Text"/> axis, but the labels are numeric
		/// rather than user-defined text.  An ordinal axis will cause the associated values for the
		/// curves to be ignored, and replaced by sequential integer values.
		/// For example, if the X Axis is ordinal, then the X values for each curve will be ignored, and the
		/// first point will be plotted at an ordinal value of 1.0, the second value at 2.0, etc.
		/// To make this property true, set <see cref="Type"/> to
		/// <see cref="AxisType.Ordinal"/>.
		/// </remarks>
		/// <value>true for an ordinal axis, false for a linear, log, text, or date axes.
		/// </value>
		/// <seealso cref="Type"/>
		/// <seealso cref="AxisType"/>
		public bool IsOrdinal
		{
			get { return type == AxisType.Ordinal; }
		}
		/// <summary>
		/// Gets or sets the <see cref="AxisType"/> for this <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// The type can be either <see cref="AxisType.Linear"/>,
		/// <see cref="AxisType.Log"/>, <see cref="AxisType.Date"/>,
		/// or <see cref="AxisType.Text"/>.
		/// </remarks>
		/// <seealso cref="Default.Type"/>.
		/// <seealso cref="IsLog"/>
		/// <seealso cref="IsText"/>
		/// <seealso cref="IsOrdinal"/>
		/// <seealso cref="IsDate"/>
		/// <seealso cref="IsReverse"/>
		public AxisType Type
		{
			get { return type; }
			set { type = value; }
		}
	#endregion

	#region Label Properties
		/// <summary>
		/// Gets or sets the property that controls the magnitude factor (power of 10) for
		/// this scale.
		/// </summary>
		/// <remarks>
		/// For large scale values, a "magnitude" value (power of 10) is automatically
		/// used for scaling the graph.  This magnitude value is automatically appended
		/// to the end of the Axis <see cref="Title"/> (e.g., "(10^4)") to indicate
		/// that a magnitude is in use.  This property controls whether or not the
		/// magnitude is included in the title.  Note that it only affects the axis
		/// title; a magnitude value may still be used even if it is not shown in the title.
		/// </remarks>
		/// <value>true to show the magnitude value, false to hide it</value>
		/// <seealso cref="Title"/>
		/// <seealso cref="ScaleMag"/>
		/// <seealso cref="ScaleFormat"/>
		/// <seealso cref="NumDec"/>
		public bool IsOmitMag
		{
			get { return isOmitMag; }
			set { isOmitMag = value; }
		}
		/// <summary>
		/// Gets or sets the text title of this <see cref="Axis"/>.
		/// </summary>
		/// <remarks>The title normally shows the basis and dimensions of
		/// the scale range, such as "Time (Years)".  The title is only shown if the
		/// <see cref="IsShowTitle"/> property is set to true.  If the Title text is empty,
		/// then no title is shown, and no space is "reserved" for the title on the graph.
		/// </remarks>
		/// <value>the title is a string value</value>
		/// <seealso cref="IsOmitMag"/>
		/// <seealso cref="IsShowTitle"/>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}
		
		/// <summary>
		/// The display mode for the <see cref="Axis"/>
		/// <see cref="Title"/> property.  The default
		/// value comes from <see cref="Default.IsShowTitle"/>.
		/// </summary>
		/// <value> boolean value; true to show the title, false to hide it.</value>
		/// <seealso cref="Title"/>
		public bool IsShowTitle
		{
			get { return isShowTitle; }
			set { isShowTitle = value; }
		}
		
		/// <summary>
		/// Determines if powers-of-ten notation will be used for the numeric value labels.
		/// </summary>
		/// <remarks>
		/// The powers-of-ten notation is just the text "10" followed by a superscripted value
		/// indicating the magnitude.  This mode is only valid for log scales (see
		/// <see cref="IsLog"/> and <see cref="Type"/>).
		/// </remarks>
		/// <value> boolean value; true to show the title as a power of ten, false to
		/// show a regular numeric value (e.g., "0.01", "10", "1000")</value>
		public bool IsUseTenPower
		{
			get { return isUseTenPower; }
			set { isUseTenPower = value; }
		}
		
		/// <summary>
		/// The text labels for this <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// This property is only
		/// applicable if <see cref="Type"/> is set to <see cref="AxisType.Text"/>.
		/// </remarks>
		public string[] TextLabels
		{
			get { return textLabels; }
			set { textLabels = value; }
		}

		/// <summary>
		/// Determines whether or not the scale label format <see cref="ScaleFormat"/>
		/// is determined automatically based on the range of data values.
		/// </summary>
		/// <remarks>
		/// This value will be set to false if
		/// <see cref="ScaleFormat"/> is manually changed.
		/// </remarks>
		/// <value>true if <see cref="ScaleFormat"/> will be set automatically, false
		/// if it is to be set manually by the user</value>
		/// <seealso cref="ScaleMag"/>
		/// <seealso cref="ScaleFormat"/>
		/// <seealso cref="ScaleFontSpec"/>
		/// <seealso cref="NumDec"/>
		public bool ScaleFormatAuto
		{
			get { return scaleFormatAuto; }
			set { scaleFormatAuto = value; }
		}
		/// <summary>
		/// The format of the <see cref="Axis"/> tic labels.
		/// </summary>
		/// <remarks>
		/// This property is only used if the <see cref="Type"/> is set to <see cref="AxisType.Date"/>.
		/// This property may be set automatically by ZedGraph, depending on the state of
		/// <see cref="ScaleFormatAuto"/>.
		/// </remarks>
		/// <value>This format string is as defined for the <see cref="XDate.ToString()"/> function</value>
		/// <seealso cref="ScaleMag"/>
		/// <seealso cref="ScaleFormatAuto"/>
		/// <seealso cref="ScaleFontSpec"/>
		/// <seealso cref="NumDec"/>
		public string ScaleFormat
		{
			get { return scaleFormat; }
			set { scaleFormat = value; this.ScaleFormatAuto = false; }
		}
		/// <summary> Controls the alignment of the <see cref="Axis"/> tic labels.
		/// </summary>
		/// <remarks>
		/// This property controls whether the inside, center, or outside edges of the
		/// text labels are aligned.
		/// </remarks>
		public AlignP ScaleAlign
		{
			get { return scaleAlign; }
			set { scaleAlign = value; }
		}
		/// <summary>
		/// Determines whether or not the number of decimal places for value
		/// labels <see cref="NumDec"/> is determined automatically based
		/// on the magnitudes of the scale values.
		/// </summary>
		/// <remarks>This value will be set to false if
		/// <see cref="NumDec"/> is manually changed.
		/// </remarks>
		/// <value>true if <see cref="NumDec"/> will be set automatically, false
		/// if it is to be set manually by the user</value>
		/// <seealso cref="ScaleMag"/>
		/// <seealso cref="ScaleFormat"/>
		/// <seealso cref="ScaleFontSpec"/>
		/// <seealso cref="NumDec"/>
		public bool NumDecAuto
		{
			get { return numDecAuto; }
			set { numDecAuto = value; }
		}
		/// <summary>
		/// The number of decimal places displayed for axis value labels.
		/// </summary>
		/// <remarks>
		/// This value can be determined automatically depending on the state of
		/// <see cref="NumDecAuto"/>.  If this value is set manually by the user,
		/// then <see cref="NumDecAuto"/> will also be set to false.
		/// </remarks>
		/// <value>the number of decimal places to be displayed for the axis
		/// scale labels</value>
		/// <seealso cref="ScaleMag"/>
		/// <seealso cref="ScaleFormat"/>
		/// <seealso cref="ScaleFontSpec"/>
		/// <seealso cref="NumDecAuto"/>
		public int NumDec
		{
			get { return numDec; }
			set { numDec = value; this.numDecAuto = false; }
		}
		/// <summary>
		/// The magnitude multiplier for scale values.
		/// </summary>
		/// <remarks>
		/// This is used to limit
		/// the size of the displayed value labels.  For example, if the value
		/// is really 2000000, then the graph will display 2000 with a 10^3
		/// magnitude multiplier.  This value can be determined automatically
		/// depending on the state of <see cref="ScaleMagAuto"/>.
		/// If this value is set manually by the user,
		/// then <see cref="ScaleMagAuto"/> will also be set to false.
		/// </remarks>
		/// <value>The magnitude multiplier (power of 10) for the scale
		/// value labels</value>
		/// <seealso cref="IsOmitMag"/>
		/// <seealso cref="Title"/>
		/// <seealso cref="ScaleFormat"/>
		/// <seealso cref="ScaleFontSpec"/>
		/// <seealso cref="NumDec"/>
		public int ScaleMag
		{
			get { return scaleMag; }
			set { scaleMag = value; this.scaleMagAuto = false; }
		}
		/// <summary>
		/// Determines whether the <see cref="ScaleMag"/> value will be set
		/// automatically based on the data, or manually by the user.
		/// </summary>
		/// <remarks>
		/// If the user manually sets the <see cref="ScaleMag"/> value, then this
		/// flag will be set to false.
		/// </remarks>
		/// <value>true to have <see cref="ScaleMag"/> set automatically,
		/// false otherwise</value>
		/// <seealso cref="IsOmitMag"/>
		/// <seealso cref="Title"/>
		/// <seealso cref="ScaleMag"/>
		public bool ScaleMagAuto
		{
			get { return scaleMagAuto; }
			set { scaleMagAuto = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the scale values
		/// </summary>
		/// <seealso cref="Default.ScaleFontFamily"/>
		/// <seealso cref="Default.ScaleFontSize"/>
		/// <seealso cref="Default.ScaleFontColor"/>
		/// <seealso cref="Default.ScaleFontBold"/>
		/// <seealso cref="Default.ScaleFontUnderline"/>
		/// <seealso cref="Default.ScaleFontItalic"/>
		public FontSpec ScaleFontSpec
		{
			get { return scaleFontSpec; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the <see cref="Axis"/> <see cref="Title"/>,
		/// </summary>
		/// <seealso cref="Default.TitleFontFamily"/>
		/// <seealso cref="Default.TitleFontSize"/>
		/// <seealso cref="Default.TitleFontColor"/>
		/// <seealso cref="Default.TitleFontBold"/>
		/// <seealso cref="Default.TitleFontUnderline"/>
		/// <seealso cref="Default.TitleFontItalic"/>
		public FontSpec TitleFontSpec
		{
			get { return titleFontSpec; }
		}
	#endregion

	#region Rendering Methods
		/// <summary>
		/// Restore the scale ranging to automatic mode, and recalculate the
		/// <see cref="Axis"/> scale ranges
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <seealso cref="MinAuto"/>
		/// <seealso cref="MaxAuto"/>
		/// <seealso cref="StepAuto"/>
		/// <seealso cref="ScaleMagAuto"/>
		/// <seealso cref="ScaleFormatAuto"/>
		/// <seealso cref="NumDecAuto"/>
		public void ResetAutoScale( GraphPane pane, Graphics g )
		{
			this.minAuto = true;
			this.maxAuto = true;
			this.stepAuto = true;
			this.scaleMagAuto = true;
			this.numDecAuto = true;
			this.scaleFormatAuto = true;
			pane.AxisChange( g );
		}

		/// <summary>
		/// Do all rendering associated with this <see cref="Axis"/> to the specified
		/// <see cref="Graphics"/> device.
		/// </summary>
		/// <remarks>
		/// This method is normally only
		/// called by the Draw method of the parent <see cref="GraphPane"/> object.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, float scaleFactor )
		{
			Matrix saveMatrix = g.Transform;

			SetupScaleData( pane );

			SetTransformMatrix( g, pane, scaleFactor );

			DrawScale( g, pane, scaleFactor );
		
			DrawTitle( g, pane, scaleFactor );

			g.Transform = saveMatrix;
		}
		
		/// <summary>
		/// Setup some temporary transform values in preparation for rendering the
		/// <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// This method is typically called by the parent <see cref="GraphPane"/>
		/// object as part of the <see cref="GraphPane.Draw"/> method.  It is also
		/// called by <see cref="GraphPane.GeneralTransform"/> and
		/// <see cref="GraphPane.ReverseTransform"/> methods to setup for
		/// coordinate transformations.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		public void SetupScaleData( GraphPane pane )
		{
			// save the axisRect data for transforming scale values to pixels
			if ( this is XAxis )
			{
				this.minPix = pane.AxisRect.Left;
				this.maxPix = pane.AxisRect.Right;
			}
			else
			{
				this.minPix = pane.AxisRect.Top;
				this.maxPix = pane.AxisRect.Bottom;
			}

			if ( this.type == AxisType.Log )
			{
				this.minScale = SafeLog( this.min );
				this.maxScale = SafeLog( this.max );
			}
			else
			{
				this.minScale = this.min;
				this.maxScale = this.max;
			}
		}
		
		/// <summary>
		/// This method will set the <see cref="MinSpace"/> property for this <see cref="Axis"/>
		/// using the currently required space multiplied by a fraction (<paramref>bufferFraction</paramref>).
		/// </summary>
		/// <remarks>
		/// The currently required space is calculated using <see cref="CalcSpace"/>, and is
		/// based on current data ranges, font sizes, etc.  The "space" is actually the amount of space
		/// required to fit the tic marks, scale labels, and axis title.
		/// </remarks>
		/// <param name="g">A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.</param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.</param>
		/// <param name="bufferFraction">The amount of space to allocate for the axis, expressed
		/// as a fraction of the currently required space.  For example, a value of 1.2 would
		/// allow for 20% extra above the currently required space.</param>
		/// <param name="isGrowOnly">If true, then this method will only modify the <see cref="MinSpace"/>
		/// property if the calculated result is more than the current value.</param>
		public void SetMinSpaceBuffer( Graphics g, GraphPane pane, float bufferFraction,
										bool isGrowOnly )
		{
			// save the original value of minSpace
			float oldSpace = this.MinSpace;
			// set minspace to zero, since we don't want it to affect the CalcSpace() result
			this.MinSpace = 0;
			// Calculate the space required for the current graph assuming scalefactor = 1.0
			// and apply the bufferFraction
			float space = this.CalcSpace( g, pane, 1.0F ) * bufferFraction;
			// isGrowOnly indicates the minSpace can grow but not shrink
			if ( isGrowOnly )
				space = Math.Max( oldSpace, space );
			// Set the minSpace
			this.MinSpace = space;
		}

		/// <summary>
		/// Setup the Transform Matrix to handle drawing of this <see cref="Axis"/>
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		abstract public void SetTransformMatrix( Graphics g, GraphPane pane, float scaleFactor );


		/// <summary>
		/// Get the maximum width of the scale value text that is required to label this
		/// <see cref="Axis"/>.
		/// The results of this method are used to determine how much space is required for
		/// the axis labels.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>the maximum width of the text in pixel units</returns>
		protected SizeF GetScaleMaxSpace( Graphics g, GraphPane pane, float scaleFactor )
		{
			string tmpStr;
			double	dVal,
				scaleMult = Math.Pow( (double) 10.0, this.scaleMag );
			int		i;

			int nTics = CalcNumTics();
			
			double startVal = CalcBaseTic();

			SizeF maxSpace = new SizeF( 0, 0 );

			// Repeat for each tic
			for ( i=0; i<nTics; i++)
			{
				dVal = CalcMajorTicValue( startVal, i );

				// draw the label
				MakeLabel( pane, i, dVal, out tmpStr );

				SizeF sizeF;
				if ( this.IsLog && this.isUseTenPower )
					sizeF = this.ScaleFontSpec.BoundingBoxTenPower( g, tmpStr,
						scaleFactor );
				else
					sizeF = this.ScaleFontSpec.BoundingBox( g, tmpStr,
						scaleFactor );

				if ( sizeF.Height > maxSpace.Height )
					maxSpace.Height = sizeF.Height;
				if ( sizeF.Width > maxSpace.Width )
					maxSpace.Width = sizeF.Width;
			}

			return maxSpace;
		}

		/// <summary>
		/// Calculate the space required for this <see cref="Axis"/>
		/// object.  This is the space between the paneRect and the axisRect for
		/// this particular axis.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>Returns the space, in pixels, required for this axis (between the
		/// paneRect and axisRect)</returns>
		public float CalcSpace( Graphics g, GraphPane pane, float scaleFactor )
		{
			float charHeight = this.ScaleFontSpec.GetHeight( scaleFactor );
			//float gap = pane.ScaledGap( scaleFactor );
			float ticSize = this.ScaledTic( scaleFactor );
		
			// axisRect is the actual area of the plot as bounded by the axes
			
			// Always leave 1xgap space, even if no axis is displayed
			float space;
			if ( this is XAxis )
				space = pane.MarginBottom * scaleFactor;
			else if ( this is YAxis )
				space = pane.MarginLeft * scaleFactor;
			else
				space = pane.MarginRight * scaleFactor;

			// Account for the Axis
			if ( this.isVisible )
			{
				// tic takes up 1x tic
				// space between tic and scale label is 0.5 tic
				// scale label is GetScaleMaxSpace()
				// space between scale label and axis label is 0.5 tic
				space += this.GetScaleMaxSpace( g, pane, scaleFactor ).Height +
							ticSize * 2.0F;
		
				// Only add space for the label if there is one
				// Axis Title gets actual height plus 1x gap
				if ( this.title.Length > 0 && this.isShowTitle )
				{
					space += this.TitleFontSpec.BoundingBox( g, this.title, scaleFactor ).Height;
				}
			}

			// for the Y axes, make sure that enough space is left to fit the first
			// and last X axis scale label
			if ( ( ( this is YAxis ) || ( this is Y2Axis ) ) && pane.XAxis.IsVisible )
			{
				float tmpSpace =
					pane.XAxis.GetScaleMaxSpace( g, pane, scaleFactor ).Width / 2.0F +
						charHeight;
				if ( tmpSpace > space )
					space = tmpSpace;
			}
			
			// Verify that the minSpace property was satisfied
			space = Math.Max( space, this.minSpace * (float) scaleFactor );

			return space;
		}

		/// <summary>
		/// Draw the scale, including the tic marks, value labels, and grid lines as
		/// required for this <see cref="Axis"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawScale( Graphics g, GraphPane pane, float scaleFactor )
		{
			float	rightPix,
					topPix;
					
			if ( this is XAxis )
			{
				rightPix = pane.AxisRect.Width;
				topPix = -pane.AxisRect.Height;
			}
			else
			{
				rightPix = pane.AxisRect.Height;
				topPix = -pane.AxisRect.Width;
			}

			// sanity check
			if ( this.min >= this.max )
				return;

			// if the step size is outrageous, then quit
			// (step size not used for log scales)
			if ( !this.IsLog  )
			{
				if ( this.step <= 0 || this.minorStep <= 0 )
					return;

				double tMajor = ( this.max - this.min ) / this.step,
						tMinor = ( this.max - this.min ) / this.minorStep;
				if ( IsDate )
				{
					tMajor /= GetUnitMultiple( majorUnit );
					tMinor /= GetUnitMultiple( minorUnit );
				}
				if ( tMajor > 1000 ||
					( ( this.isMinorTic || this.isMinorInsideTic || this.isMinorOppositeTic )
					&& tMinor > 5000 ) )
					return;
			}

			// calculate the total number of major tics required
			int nTics = CalcNumTics();
			// get the first major tic value
			double baseVal = CalcBaseTic();

			if ( this.IsVisible )
			{
                Pen pen = new Pen(this.color, pane.ScaledPenWidth(ticPenWidth, scaleFactor));

                // redraw the axis border
				g.DrawLine( pen, 0.0F, 0.0F, rightPix, 0.0F );

				// Draw a zero-value line if needed
				if ( this.isZeroLine && this.min < 0.0 && this.max > 0.0 )
				{
					float zeroPix = LocalTransform( 0.0 );
					g.DrawLine( pen, zeroPix, 0.0F, zeroPix, topPix );
				}

				// draw the major tics and labels
				DrawLabels( g, pane, baseVal, nTics, topPix, scaleFactor );
			
				DrawMinorTics( g, pane, baseVal, scaleFactor, topPix );
			}
		}
	
		/// <summary>
		/// Internal routine to calculate a multiplier to the selected unit back to days.
		/// </summary>
		/// <param name="unit">The unit type for which the multiplier is to be
		/// calculated</param>
		/// <returns>
		/// This is ratio of days/selected unit
		/// </returns>
		private double GetUnitMultiple( DateUnit unit )
		{
			switch( unit )
			{
				case DateUnit.Year:
				default:
					return 365.0;
				case DateUnit.Month:
					return 30.0;
				case DateUnit.Day:
					return 1.0;
				case DateUnit.Hour:
					return 1.0 / XDate.HoursPerDay;
				case DateUnit.Minute:
					return 1.0 / XDate.MinutesPerDay;
				case DateUnit.Second:
					return 1.0 / XDate.SecondsPerDay;
			}
		}

		/// <summary>
		/// Internal routine to determine the ordinals of the first and last major axis label.
		/// </summary>
		/// <returns>
		/// This is the total number of major tics for this axis.
		/// </returns>
		private int CalcNumTics()
		{
			if ( this.IsText ) // text labels (ordinal scale)
			{
				// If no array of labels is available, just assume 10 labels so we don't blow up.
				if ( this.TextLabels == null )
					return 10;
				else
					return this.TextLabels.Length;
			}
			else if ( this.IsDate )  // Date-Time scale
			{
				int year1, year2, month1, month2, day1, day2, hour1, hour2, minute1, minute2, second1, second2;
				int nTics;

				XDate.XLDateToCalendarDate( this.min, out year1, out month1, out day1,
											out hour1, out minute1, out second1 );
				XDate.XLDateToCalendarDate( this.max, out year2, out month2, out day2,
											out hour2, out minute2, out second2 );
				
				switch ( this.majorUnit )
				{
					case DateUnit.Year:
					default:
						nTics = (int) ( ( year2 - year1 ) / this.step + 1.0 );
						break;
					case DateUnit.Month:
						nTics = (int) ( ( month2 - month1 + 12.0 * (year2 - year1) ) / this.step + 1.0 );
						break;
					case DateUnit.Day:
						nTics = (int) ( ( this.max - this.min ) / this.step + 1.0 );
						break;
					case DateUnit.Hour:
						nTics = (int) ( ( this.max - this.min ) * XDate.HoursPerDay + 1.0 );
						break;
					case DateUnit.Minute:
						nTics = (int) ( ( this.max - this.min ) * XDate.MinutesPerDay + 1.0 );
						break;
					case DateUnit.Second:
						nTics = (int) ( ( this.max - this.min ) * XDate.SecondsPerDay + 1.0 );
						break;
				}
		
				if ( nTics < 1 )
					nTics = 1;

				return nTics;
			}
			else if ( this.IsLog )  // log scale
			{
				//iStart = (int) ( Math.Ceiling( SafeLog( this.min ) - 1.0e-12 ) );
				//iEnd = (int) ( Math.Floor( SafeLog( this.max ) + 1.0e-12 ) );
				return  (int) ( Math.Floor( SafeLog( this.max ) + 1.0e-12 ) ) -
						(int) ( Math.Ceiling( SafeLog( this.min ) - 1.0e-12 ) ) + 1;
			}
			else  // regular linear or ordinal scale
			{
				return (int) (( this.max - this.min ) / this.step + 0.01) + 1;
			}
		}

		/// <summary>
		/// Draw the value labels, tic marks, and grid lines as
		/// required for this <see cref="Axis"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="baseVal">
		/// The first major tic value for the axis
		/// </param>
		/// <param name="nTics">
		/// The total number of major tics for the axis
		/// </param>
		/// <param name="topPix">
		/// The pixel location of the far side of the axisRect from this axis.
		/// This value is the axisRect.Height for the XAxis, or the axisRect.Width
		/// for the YAxis and Y2Axis.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawLabels( Graphics g, GraphPane pane, double baseVal, int nTics,
						float topPix, float scaleFactor )
		{
			double	dVal, dVal2;
			float	pixVal, pixVal2;
			string	tmpStr;
			float	scaledTic = this.ScaledTic( scaleFactor );
			double	scaleMult = Math.Pow( (double) 10.0, this.scaleMag );
            Pen pen = new Pen(this.color, pane.ScaledPenWidth(ticPenWidth, scaleFactor));
            Pen dottedPen = new Pen(this.gridColor, pane.ScaledPenWidth(gridPenWidth, scaleFactor));

            dottedPen.DashStyle = DashStyle.Custom;
			float[] pattern = new float[2];
			pattern[0] = this.gridDashOn;
			pattern[1] = this.gridDashOff;
			dottedPen.DashPattern = pattern;

			// get the Y position of the center of the axis labels
			// (the axis itself is referenced at zero)
			float maxSpace = this.GetScaleMaxSpace( g, pane, scaleFactor ).Height;
			
			float textTop = (float) scaleFactor * ( ticSize * 1.5F );
			float textCenter;

			double rangeTol = ( this.maxScale - this.minScale ) * 0.00001;
			
			// loop for each major tic
			for ( int i=0; i<nTics; i++ )
			{
				dVal = CalcMajorTicValue( baseVal, i );
				
				// If we're before the start of the scale, just go to the next tic
				if ( dVal < this.minScale )
					continue;
				// if we've already past the end of the scale, then we're done
				if ( dVal > this.maxScale + rangeTol )
					break;

				// convert the value to a pixel position
				pixVal = this.LocalTransform( dVal );

				// see if the tic marks will be draw between the labels instead of at the labels
				// (this applies only to AxisType.Text
				if ( this.isTicsBetweenLabels && this.type == AxisType.Text )
				{
					// We need one extra tic in order to draw the tics between labels
					// so provide an exception here
					if ( i == 0 )
					{
						dVal2 = CalcMajorTicValue( baseVal, -0.5 );
						if ( dVal2 >= this.minScale )
						{
							pixVal2 = this.LocalTransform( dVal2 );
							DrawATic( g, pen, pixVal2, topPix, scaledTic );
							// draw the grid
							if ( this.isVisible && this.isShowGrid )
								g.DrawLine( dottedPen, pixVal2, 0.0F, pixVal2, topPix );
						}
					}

					dVal2 = CalcMajorTicValue( baseVal, (double) i + 0.5 );
					if ( dVal2 > this.maxScale )
						break;
					pixVal2 = this.LocalTransform( dVal2 );
				}
				else
					pixVal2 = pixVal;

				DrawATic( g, pen, pixVal2, topPix, scaledTic );
				
				// draw the grid
				if ( this.isVisible && this.isShowGrid )
					g.DrawLine( dottedPen, pixVal2, 0.0F, pixVal2, topPix );

				if ( this.isVisible )
				{
					// draw the label
					MakeLabel( pane, i, dVal, out tmpStr );
					
					float height = ScaleFontSpec.BoundingBox( g, tmpStr, scaleFactor ).Height;
					if ( this.ScaleAlign == AlignP.Center )
						textCenter = textTop + maxSpace / 2.0F;
					else if ( this.ScaleAlign == AlignP.Outside )
						textCenter = textTop + maxSpace - height / 2.0F;
					else	// inside
						textCenter = textTop + height / 2.0F;
					
					
					if ( this.IsLog && this.isUseTenPower )
						this.ScaleFontSpec.DrawTenPower( g, pane, tmpStr,
							pixVal, textCenter,
							AlignH.Center, AlignV.Center,
							scaleFactor );
					else
						this.ScaleFontSpec.Draw( g, pane.IsPenWidthScaled, tmpStr,
							pixVal, textCenter,
							AlignH.Center, AlignV.Center,
							scaleFactor );

				}
			}
		}

		/// <summary>
		/// Draw a tic mark at the specified single position.  This includes the inner, outer
		/// and opposite tic marks as required.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pen">Graphic <see cref="Pen"/> with which to draw the tic mark.</param>
		/// <param name="pixVal">The pixel location of the tic mark on this
		/// <see cref="Axis"/></param>
		/// <param name="topPix">The pixel value of the top of the axis border</param>
        /// <param name="scaledTic">The length of the tic mark, in points (1/72 inch)</param>
        void DrawATic( Graphics g, Pen pen, float pixVal, float topPix, float scaledTic )
		{
			if ( this.isVisible )
			{
				// draw the outside tic
				if ( this.isTic )
					g.DrawLine( pen, pixVal, 0.0F, pixVal, 0.0F + scaledTic );

				// draw the inside tic
				if ( this.isInsideTic )
					g.DrawLine( pen, pixVal, 0.0F, pixVal, 0.0F - scaledTic );

				// draw the opposite tic
				if ( this.isOppositeTic )
					g.DrawLine( pen, pixVal, topPix, pixVal, topPix + scaledTic );
			}
		}

		/// <summary>
		/// Determine the value for the first major tic.
		/// </summary>
		/// <remarks>
		/// This is done by finding the first possible value that is an integral multiple of
		/// the step size, taking into account the date/time units if appropriate.
		/// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <returns>
		/// First major tic value (floating point double).
		/// </returns>
		private double CalcBaseTic()
		{
			if ( this.IsDate )
			{
				int year, month, day, hour, minute, second;
				XDate.XLDateToCalendarDate( this.min, out year, out month, out day, out hour, out minute,
											out second );
				switch ( this.majorUnit )
				{
					case DateUnit.Year:
					default:
						month = 1; day = 1; hour = 0; minute = 0; second = 0;
						break;
					case DateUnit.Month:
						day = 1; hour = 0; minute = 0; second = 0;
						break;
					case DateUnit.Day:
						hour = 0; minute = 0; second = 0;
						break;
					case DateUnit.Hour:
						minute = 0; second = 0;
						break;
					case DateUnit.Minute:
						second = 0;
						break;
					case DateUnit.Second:
						break;
						
				}
				
				double xlDate = XDate.CalendarDateToXLDate( year, month, day, hour, minute, second );
				if ( xlDate < this.min )
				{
					switch ( this.majorUnit )
					{
						case DateUnit.Year:
						default:
							year++;
							break;
						case DateUnit.Month:
							month++;
							break;
						case DateUnit.Day:
							day++;
							break;
						case DateUnit.Hour:
							hour++;
							break;
						case DateUnit.Minute:
							minute++;
							break;
						case DateUnit.Second:
							second++;
							break;
							
					}
					
					xlDate = XDate.CalendarDateToXLDate( year, month, day, hour, minute, second );
				}
				
				return xlDate;
			}
			else if ( this.IsLog )
			{
				// go to the nearest even multiple of the step size
				return Math.Ceiling( SafeLog( this.min ) - 0.00000001 );
			}
			else if ( this.IsText )
			{
				return 1.0;
			}
			else
			{
				// go to the nearest even multiple of the step size
				return Math.Ceiling( (double) this.min / (double) this.step - 0.00000001 )
														* (double) this.step;
			}

		}
		
		/// <summary>
		/// Determine the value for any major tic.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <param name="baseVal">
		/// The value of the first major tic (floating point double)
		/// </param>
		/// <param name="tic">
		/// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
		/// </param>
		/// <returns>
		/// The specified major tic value (floating point double).
		/// </returns>
		private double CalcMajorTicValue( double baseVal, double tic )
		{
			if ( this.IsDate ) // date scale
			{
				XDate xDate = new XDate( baseVal );
				
				switch ( this.majorUnit )
				{
					case DateUnit.Year:
					default:
						xDate.AddYears( tic * this.step );
						break;
					case DateUnit.Month:
						xDate.AddMonths( tic * this.step );
						break;
					case DateUnit.Day:
						xDate.AddDays( tic * this.step );
						break;
					case DateUnit.Hour:
						xDate.AddHours( tic * this.step );
						break;
					case DateUnit.Minute:
						xDate.AddMinutes( tic * this.step );
						break;
					case DateUnit.Second:
						xDate.AddSeconds( tic * this.step );
						break;	
				}
				
				return xDate.XLDate;
			}
			else if ( this.IsLog ) // log scale
			{
				return baseVal + (double) tic;
			}
			else // regular linear scale
			{
				return baseVal + (double) this.step * tic;
			}
		}
		
		/// <summary>
		/// Make a value label for the axis at the specified ordinal position.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="index">
		/// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
		/// cause the third value label on the axis to be generated.
		/// </param>
		/// <param name="dVal">
		/// The numeric value associated with the label.  This value is ignored for log (<see cref="IsLog"/>)
		/// and text (<see cref="IsText"/>) type axes.
		/// </param>
		/// <param name="label">
		/// Output only.  The resulting value label.
		/// </param>
		private void MakeLabel( GraphPane pane, int index, double dVal, out string label )
		{
			// draw the label
			if ( this.IsText )
			{
				index *= (int) this.step;
				if ( this.TextLabels == null || index < 0 || index >= TextLabels.Length )
					label = "";
				else
					label = TextLabels[index];
			}
			else if ( this.IsDate )
			{
				if ( this.scaleFormat == null )
					this.scaleFormat = Default.ScaleFormat;
				label = XDate.ToString( dVal, this.scaleFormat );
			}
			else if ( this.IsLog && this.isUseTenPower )
			{
				label = string.Format( "{0:F0}", dVal );
			}
			else if ( this.IsLog )
			{
				int tmpNum = 0;
				if ( dVal < 0 )
					tmpNum = (int) Math.Abs( dVal );
					
				string tmpStr = "{0:F*}";
				
				tmpStr = tmpStr.Replace("*", tmpNum.ToString("D") );
								
				label = String.Format( tmpStr, Math.Pow( 10.0, dVal ) );
			}
			else // linear or ordinal
			{
				double	scaleMult = Math.Pow( (double) 10.0, this.scaleMag );

				string tmpStr = "{0:F*}";
				tmpStr = tmpStr.Replace("*", this.numDec.ToString("D") );
				label = String.Format( tmpStr, dVal / scaleMult );

				if ( pane.BarType == BarType.PercentStack )															//rpk
				{
					if (( this is YAxis && pane.BarBase == BarBase.X  ) ||
						( this is XAxis && pane.BarBase == BarBase.Y ) )
						label = label + "%" ;
				}
			}
		}

		/// <summary>
		/// Draw the minor tic marks as required for this <see cref="Axis"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="baseVal">
        /// The scale value for the first major tic position.  This is the reference point
		/// for all other tic marks.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="topPix">
		/// The pixel location of the far side of the axisRect from this axis.
		/// This value is the axisRect.Height for the XAxis, or the axisRect.Width
		/// for the YAxis and Y2Axis.
		/// </param>
		public void DrawMinorTics( Graphics g, GraphPane pane, double baseVal, float scaleFactor, float topPix )
		{
			if ( this.isMinorTic && this.isVisible )
			{
				double tMajor = this.step * ( IsDate ? GetUnitMultiple( majorUnit ) : 1.0 ),
					tMinor = this.minorStep * ( IsDate ? GetUnitMultiple( minorUnit ) : 1.0 );

				if ( this.IsLog || tMinor < tMajor )
				{
					float minorScaledTic = this.ScaledMinorTic( scaleFactor );

					// Minor tics start at the minimum value and step all the way thru
					// the full scale.  This means that if the minor step size is not
					// an even division of the major step size, the minor tics won't
					// line up with all of the scale labels and major tics.
					double	first = this.min,
							last = this.max;
					
					if ( this.IsLog )
					{
						first = SafeLog( this.min );
						last = SafeLog( this.max );
					}
					
					double	dVal = first;
					float	pixVal;
                    Pen pen = new Pen(this.color, pane.ScaledPenWidth(ticPenWidth, scaleFactor));
                    Pen		minorGridPen = new Pen( this.minorGridColor,
                                    pane.ScaledPenWidth(minorGridPenWidth, scaleFactor));

                    minorGridPen.DashStyle = DashStyle.Custom;
					float[] pattern = new float[2];
					pattern[0] = this.minorGridDashOn;
					pattern[1] = this.minorGridDashOff;
					minorGridPen.DashPattern = pattern;
					
					int iTic = CalcMinorStart( baseVal );
					int majorTic = 0;
					double majorVal = CalcMajorTicValue( baseVal, majorTic );

					// Draw the minor tic marks
					while ( dVal < last && iTic < 5000 )
					{
						// Calculate the scale value for the current tic
						dVal = CalcMinorTicValue( baseVal, iTic );
						// Maintain a value for the current major tic
						if ( dVal > majorVal )
							majorVal = CalcMajorTicValue( baseVal, ++majorTic );
						
						// Make sure that the current value does not match up with a major tic
						if ( ( Math.Abs(dVal) < 1e-20 && Math.Abs( dVal - majorVal ) > 1e-20 ) ||
							( Math.Abs(dVal) > 1e-20 && Math.Abs( (dVal - majorVal) / dVal ) > 1e-10 ) &&
							( dVal >= first && dVal <= last ) )
						{
							pixVal = this.LocalTransform( dVal );

							// draw the minor grid
							if ( this.isShowMinorGrid )
								g.DrawLine( minorGridPen, pixVal, 0.0F, pixVal, topPix );
								
							// draw the outside tic
							if ( this.isMinorTic )
								g.DrawLine( pen, pixVal, 0.0F, pixVal, 0.0F + minorScaledTic );

							// draw the inside tic
							if ( this.isMinorInsideTic )
								g.DrawLine( pen, pixVal, 0.0F, pixVal, 0.0F - minorScaledTic );

							// draw the opposite tic
							if ( this.isMinorOppositeTic )
								g.DrawLine( pen, pixVal, topPix, pixVal, topPix + minorScaledTic );
						}

						iTic++;
					}
				}
			}
		}

		/// <summary>
		/// Determine the value for any minor tic.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <param name="baseVal">
		/// The value of the first major tic (floating point double).  This tic value is the base
		/// reference for all tics (including minor ones).
		/// </param>
		/// <param name="iTic">
		/// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
		/// </param>
		/// <returns>
		/// The specified minor tic value (floating point double).
		/// </returns>
		private double CalcMinorTicValue( double baseVal, int iTic )
		{
			double[] dLogVal = { 0, 0.301029995663981, 0.477121254719662, 0.602059991327962,
									0.698970004336019, 0.778151250383644, 0.845098040014257,
									0.903089986991944, 0.954242509439325, 1 };


			if ( this.IsDate ) // date scale
			{
				XDate xDate= new XDate( baseVal );
				
				switch ( this.minorUnit )
				{
					case DateUnit.Year:
					default:
						xDate.AddYears( (double) iTic * this.minorStep );
						break;
					case DateUnit.Month:
						xDate.AddMonths( (double) iTic * this.minorStep );
						break;
					case DateUnit.Day:
						xDate.AddDays( (double) iTic * this.minorStep );
						break;
					case DateUnit.Hour:
						xDate.AddHours( (double) iTic * this.minorStep );
						break;
					case DateUnit.Minute:
						xDate.AddMinutes( (double) iTic * this.minorStep );
						break;
					case DateUnit.Second:
						xDate.AddSeconds( (double) iTic * this.minorStep );
						break;	
				}
				
				return xDate.XLDate;
			}
			else if ( this.IsLog ) // log scale
			{
				return baseVal + Math.Floor( (double) iTic / 9.0 ) + dLogVal[ ( iTic + 9 ) % 9 ];
			}
			else // regular linear scale
			{
				return baseVal + (double) this.minorStep * (double) iTic;
			}
		}
		
		/// <summary>
		/// Internal routine to determine the ordinals of the first minor tic mark
		/// </summary>
		/// <param name="baseVal">
		/// The value of the first major tic for the axis.
		/// </param>
		/// <returns>
		/// The ordinal position of the first minor tic, relative to the first major tic.
		/// This value can be negative (e.g., -3 means the first minor tic is 3 minor step
		/// increments before the first major tic.
		/// </returns>
		private int CalcMinorStart( double baseVal )
		{
			if ( this.IsText ) // text labels (ordinal scale)
			{
				// This should never happen (no minor tics for text labels)
				return 0;
			}
			else if ( this.IsDate )  // Date-Time scale
			{
				switch ( this.majorUnit )
				{
					case DateUnit.Year:
					default:
						return (int) ( ( this.min - baseVal ) / 365.0 );
					case DateUnit.Month:
						return (int) ( ( this.min - baseVal ) / 28.0 );
					case DateUnit.Day:
						return (int) ( this.min - baseVal );
					case DateUnit.Hour:
						return (int) ( ( this.min - baseVal ) * XDate.HoursPerDay );
					case DateUnit.Minute:
						return (int) ( ( this.min - baseVal ) * XDate.MinutesPerDay );
					case DateUnit.Second:
						return (int) ( ( this.min - baseVal ) * XDate.SecondsPerDay );
				}				
			}
			else if ( this.IsLog )  // log scale
			{
				return -9;
			}
			else  // regular linear scale
			{
				return (int) ( ( this.min - baseVal ) / this.minorStep );
			}
		}

		/// <summary>
		/// Draw the title for this <see cref="Axis"/>.
		/// </summary>
		/// <remarks>On entry, it is assumed that the
		/// graphics transform has been configured so that the origin is at the left side
		/// of this axis, and the axis is aligned along the X coordinate direction.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawTitle( Graphics g, GraphPane pane, float scaleFactor )
		{
			string str;

			if ( this.ScaleMag != 0 && ! this.IsOmitMag )
				str = this.title + String.Format( " (10^{0})", this.ScaleMag );
			else
				str = this.title;

			// If the Axis is visible, draw the title
			if ( this.isVisible && this.isShowTitle && str.Length > 0 )
			{		
				// Calculate the title position in screen coordinates
				float x = ( this.maxPix - this.minPix ) / 2;
				float y = ScaledTic( scaleFactor ) * 2.0F +
							GetScaleMaxSpace( g, pane, scaleFactor ).Height
							+ this.TitleFontSpec.BoundingBox( g, str, scaleFactor ).Height / 2.0F;

				AlignV alignV = AlignV.Center;
				//				AlignV alignV = AlignV.Top;
				//if ( this is YAxis )
				//	alignV = AlignV.Bottom;

				// Draw the title
				this.TitleFontSpec.Draw( g, pane.IsPenWidthScaled, str, x, y,
							AlignH.Center, alignV, scaleFactor );
			}
		}
		
		/// <summary>
		/// Determine the width, in pixel units, of each bar cluster including
		/// the cluster gaps and bar gaps.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <returns>The width of each bar cluster, in pixel units</returns>
		public float GetClusterWidth( GraphPane pane )
		{
			return Math.Abs( this.Transform( 1.0 +
					((this.IsOrdinal || this.IsText) ? 1.0 : pane.ClusterScaleWidth ) ) -
					this.Transform( 1.0 ) );
		}
	#endregion
	
	#region Scale Picker Methods
		/// <summary>
		/// Select a reasonable scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// The scale range is chosen
		/// based on increments of 1, 2, or 5 (because they are even divisors of 10).  This
		/// routine honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings as well as the <see cref="IsLog"/>
		/// setting.  In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  The basic defaults for
		/// scale selection are defined using <see cref="Default.ZeroLever"/>,
		/// <see cref="Default.TargetXSteps"/>, and <see cref="Default.TargetYSteps"/>
		/// from the <see cref="Default"/> default class.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </remarks>
		/// <param name="minVal">The minimum value of the data range for setting this
		/// <see cref="Axis"/> scale range</param>
		/// <param name="maxVal">The maximum value of the data range for setting this
		/// <see cref="Axis"/> scale range</param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void PickScale( double minVal, double maxVal, GraphPane pane, Graphics g, float scaleFactor )
		{
			// Make sure that minVal and maxVal are legitimate values
			if ( Double.IsInfinity( minVal ) || Double.IsNaN( minVal ) || minVal == Double.MaxValue )
				minVal = 0.0;
			if ( Double.IsInfinity( maxVal ) || Double.IsNaN( maxVal ) || maxVal == Double.MaxValue )
				maxVal = 0.0;

			// if the scales are autoranged, use the actual data values for the range
			double range = maxVal - minVal;

			// "Grace" is applied to the numeric axis types only
			bool numType = (type == AxisType.Log || type == AxisType.Date || type == AxisType.Linear );

			// For autoranged values, assign the value.  If appropriate, adjust the value by the
			// "Grace" value.
			if ( this.minAuto )
			{
				this.min = minVal;
				// Do not let the grace value extend the axis below zero when all the values were positive
				if ( numType && ( this.min < 0 || minVal - this.MinGrace * range >= 0.0 ) )
						this.min = minVal - this.MinGrace * range;
			}
			if ( this.maxAuto )
			{
				this.max = maxVal;
				// Do not let the grace value extend the axis above zero when all the values were negative
				if ( numType && ( this.max > 0 || maxVal + this.MaxGrace * range <= 0.0 ) )
					this.max = maxVal + this.MaxGrace * range;
			}
				
			switch( this.type )
			{
				case AxisType.Text:
					PickTextScale( g, pane, scaleFactor );
					break;
				case AxisType.Ordinal:
					PickOrdinalScale( g, pane, scaleFactor );
					break;
				case AxisType.Log:
					PickLogScale();
					break;
				case AxisType.Date:
					PickDateScale( g, pane, scaleFactor );
					break;
				case AxisType.Linear:
					PickLinearScale( g, pane, scaleFactor );
					break;
			}
		}
		
		/// <summary>
		/// Select a reasonable text axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Text"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  This is an ordinal
		/// type, such that the labeled values start at 1.0 and increment by 1.0 for
		/// each successive label.  The maximum number of labels on the graph is
		/// determined by <see cref="Default.MaxTextLabels"/>.  If necessary, this method will
		/// set the <see cref="Step"/> value to greater than 1.0 in order to keep the total
		/// labels displayed below <see cref="Default.MaxTextLabels"/>.  For example, a
		/// <see cref="Step"/> size of 2.0 would only display every other label on the
		/// axis.  The <see cref="Step"/> value calculated by this routine is always
		/// an integral value.  This
		/// method honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Text"/>
		public void PickTextScale( Graphics g, GraphPane pane, float scaleFactor )
		{
			// if text labels are provided, then autorange to the number of labels
			if ( this.TextLabels != null )
			{
				if ( this.minAuto )
					this.min = 0.5;
				if ( this.maxAuto )
					this.max = this.TextLabels.Length + 0.5;
			}
			else
			{
				if ( this.minAuto )
					this.min -= 0.5;
				if ( this.maxAuto )
					this.max += 0.5;
			}
			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < .1 )
			{
				if ( this.maxAuto )
					this.max = this.min + 10.0;
				else
					this.min = this.max - 10.0;
			}
		
			if ( this.stepAuto )
			{
				if ( this.TextLabels != null )
				{
					// Calculate the maximum number of labels
					double maxLabels = (double) this.CalcMaxLabels( g, pane, scaleFactor );

					// Calculate a step size based on the width of the labels
					double tmpStep = Math.Ceiling( ( this.max - this.min ) / maxLabels );

					// Use the lesser of the two step sizes
					//if ( tmpStep < this.step )
						this.step = tmpStep;
				}
				else
					this.step = (int) ( ( this.max - this.min - 1.0 ) / Default.MaxTextLabels ) + 1.0;
				
			}
			else
			{
				this.step = (int) this.step;
				if ( this.step <= 0 )
					this.step = 1.0;
			}
		
			if ( this.minorStepAuto )
				this.minorStep = 1;
			this.numDec = 0;
			this.scaleMag = 0;
		}
		
		/// <summary>
		/// Calculate the maximum number of labels that will fit on this axis.
		/// </summary>
		/// <remarks>
		/// This method works for
		/// both X and Y direction axes, and it works for angled text (assuming that a bounding box
		/// is an appropriate measure).  Technically, labels at 45 degree angles could fit better than
		/// the return value of this method since the bounding boxes can overlap without the labels actually
		/// overlapping.
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public int CalcMaxLabels( Graphics g, GraphPane pane, float scaleFactor )
		{
			SizeF size = this.GetScaleMaxSpace( g, pane, scaleFactor );
			double maxWidth;
			
			// The font angles are already set such that the Width is parallel to the appropriate (X or Y)
			// axis.  Therefore, we always use size.Width.
			// use the minimum of 1/4 the max Width or 1 character space
//			double allowance = this.ScaleFontSpec.GetWidth( g, scaleFactor );
//			if ( allowance > size.Width / 4 )
//				allowance = size.Width / 4;

			maxWidth = size.Width;
/*
			if ( this is XAxis )
				// Add an extra character width to leave a minimum of 1 character space between labels
				maxWidth = size.Width + this.ScaleFontSpec.GetWidth( g, scaleFactor );
			else
				// For vertical spacing, we only need 1/2 character
				maxWidth = size.Width + this.ScaleFontSpec.GetWidth( g, scaleFactor ) / 2.0;
*/
			if ( maxWidth <= 0 )
				maxWidth = 1;
				

			// Calculate the maximum number of labels
			double width;
			if ( this is XAxis )
				width = ( pane.AxisRect.Width == 0 ) ? pane.PaneRect.Width * 0.75 : pane.AxisRect.Width;
			else
				width = ( pane.AxisRect.Height == 0 ) ? pane.PaneRect.Height * 0.75 : pane.AxisRect.Height;

			int maxLabels = (int) ( width / maxWidth );
			if ( maxLabels <= 0 )
				maxLabels = 1;

			return maxLabels;
		}

		/// <summary>
		/// Select a reasonable ordinal axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Ordinal"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  The scale range is chosen
		/// based on increments of 1, 2, or 5 (because they are even divisors of 10).
		/// Being an ordinal axis type, the <see cref="Step"/> value will always be integral.  This
		/// method honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  The basic defaults for
		/// scale selection are defined using <see cref="Default.ZeroLever"/>,
		/// <see cref="Default.TargetXSteps"/>, and <see cref="Default.TargetYSteps"/>
		/// from the <see cref="Default"/> default class.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Ordinal"/>
		public void PickOrdinalScale( Graphics g, GraphPane pane, float scaleFactor )
		{
			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < 1.0 )
			{
				if ( this.maxAuto )
					this.max = this.min + 0.5;
				else
					this.min = this.max - 0.5;
			}
			else
			{
				// Calculate the new step size
				if ( this.stepAuto )
				{
					// Calculate the step size based on targetSteps
					this.step = CalcStepSize( this.max - this.min,
						(this is XAxis) ? Default.TargetXSteps : Default.TargetYSteps );

					// Calculate the maximum number of labels
					double maxLabels = (double) this.CalcMaxLabels( g, pane, scaleFactor );

					// Calculate a step size based on the width of the labels
					double tmpStep = Math.Ceiling( ( this.max - this.min ) / maxLabels );

					// Use the greater of the two step sizes
					if ( tmpStep > this.step )
						this.step = tmpStep;

				}
	
				this.step = (int) this.Step;
				if ( this.step < 1.0 )
					this.step = 1.0;

				// Calculate the new minor step size
				if ( this.minorStepAuto )
					this.minorStep = CalcStepSize( this.step, 
						(this is XAxis) ? Default.TargetMinorXSteps : Default.TargetMinorYSteps );

				if ( this.minAuto )
					this.min -= 0.5;
				if ( this.maxAuto )
					this.max += 0.5;
			}
		}
		
		/// <summary>
		/// Select a reasonable base 10 logarithmic axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Log"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  The scale range is chosen
		/// based always on powers of 10 (full log cycles).  This
		/// method honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  For log axes, the MinorStep
		/// value is not used.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </remarks>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Log"/>
		public void PickLogScale()
		{
			this.scaleMag = 0;		// Never use a magnitude shift for log scales
			this.numDec = 0;		// The number of decimal places to display is not used
	
			// Check for bad data range
			if (this.min <= 0.0 && this.max <= 0.0 )
			{
				this.min = 1.0;
				this.max = 10.0;
			}
			else if ( this.min <= 0.0 )
			{
				this.min = this.max / 10.0;
			}
			else if ( this.max <= 0.0 )
			{
				this.max = this.min * 10.0;
			}
	
			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < 1.0e-20 )
			{
				if ( this.maxAuto )
					this.max = this.max * 2.0;
				if ( this.minAuto )
					this.min = this.min / 2.0;
			}
			
			// Get the nearest power of 10 (no partial log cycles allowed)
			if ( this.minAuto )
				this.min = Math.Pow( (double) 10.0,
					Math.Floor( Math.Log10( this.min ) ) );
			if ( this.maxAuto )
				this.max = Math.Pow( (double) 10.0,
					Math.Ceiling( Math.Log10( this.max ) ) );
	
		}

		/// <summary>
		/// Select a reasonable date-time axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Date"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  The scale range is chosen
		/// based on increments of 1, 2, or 5 (because they are even divisors of 10).
		/// Note that the <see cref="Step"/> property setting can have multiple unit
		/// types (<see cref="DateUnit"/>), but the <see cref="Min"/> and
		/// <see cref="Max"/> units are always days (<see cref="XDate"/>).  This
		/// method honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  The basic default for
		/// scale selection is defined with
		/// <see cref="Default.TargetXSteps"/> and <see cref="Default.TargetYSteps"/>
		/// from the <see cref="Default"/> default class.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Date"/>
		/// <seealso cref="MajorUnit"/>
		/// <seealso cref="MinorUnit"/>
		public void PickDateScale( Graphics g, GraphPane pane, float scaleFactor )
		{
			
			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < 1.0e-20 )
			{
				if ( this.maxAuto )
					this.max = this.max + 0.2 * ( this.max == 0 ? 1.0 : Math.Abs( this.max ) );
				if ( this.minAuto )
					this.min = this.min - 0.2 * ( this.min == 0 ? 1.0 : Math.Abs( this.min ) );
			}
	
			// Calculate the new step size
			if ( this.stepAuto )
			{
				double targetSteps = (this is XAxis) ? Default.TargetXSteps : Default.TargetYSteps;

				// Calculate the step size based on target steps
				this.step = CalcDateStepSize( this.max - this.min, targetSteps );

				// Calculate the maximum number of labels
				double maxLabels = (double) this.CalcMaxLabels( g, pane, scaleFactor );

				if ( maxLabels < this.CalcNumTics() )
					this.step = CalcDateStepSize( this.max - this.min, maxLabels );
			}
			
			// Calculate the scale minimum
			if ( this.minAuto )
				this.min = CalcEvenStepDate( this.min, -1 );
	
			// Calculate the scale maximum
			if ( this.maxAuto )
				this.max = CalcEvenStepDate( this.max, 1 );

			this.scaleMag = 0;		// Never use a magnitude shift for date scales
			this.numDec = 0;		// The number of decimal places to display is not used
	
		}

		/// <summary>
		/// Select a reasonable linear axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Linear"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  The scale range is chosen
		/// based on increments of 1, 2, or 5 (because they are even divisors of 10).  This
		/// method honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  The basic defaults for
		/// scale selection are defined using <see cref="Default.ZeroLever"/>,
		/// <see cref="Default.TargetXSteps"/>, and <see cref="Default.TargetYSteps"/>
		/// from the <see cref="Default"/> default class.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Linear"/>
		public void PickLinearScale( Graphics g, GraphPane pane, float scaleFactor )
		{
			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < 1.0e-20 )
			{
				if ( this.maxAuto )
					this.max = this.max + 0.2 * ( this.max == 0 ? 1.0 : Math.Abs( this.max ) );
				if ( this.minAuto )
					this.min = this.min - 0.2 * ( this.min == 0 ? 1.0 : Math.Abs( this.min ) );
			}
	
			// This is the zero-lever test.  If minVal is within the zero lever fraction
			// of the data range, then use zero.
	
			if ( this.minAuto && this.min > 0 &&
				this.min / ( this.max - this.min ) < Default.ZeroLever )
				this.min = 0;
	
			// Repeat the zero-lever test for cases where the maxVal is less than zero
			if ( this.maxAuto && this.max < 0 &&
				Math.Abs( this.max / ( this.max - this.min )) <
				Default.ZeroLever )
				this.max = 0;
	
			// Calculate the new step size
			if ( this.stepAuto )
			{
				double targetSteps = (this is XAxis) ? Default.TargetXSteps : Default.TargetYSteps;

				// Calculate the step size based on target steps
				this.step = CalcStepSize( this.max - this.min, targetSteps );

				// Calculate the maximum number of labels
				double maxLabels = (double) this.CalcMaxLabels( g, pane, scaleFactor );

				if ( maxLabels < ( this.max - this.min ) / this.step )
					this.step = CalcBoundedStepSize( this.max - this.min, maxLabels );
			}
	
			// Calculate the new step size
			if ( this.minorStepAuto )
				this.minorStep = CalcStepSize( this.step, 
					(this is XAxis) ? Default.TargetMinorXSteps : Default.TargetMinorYSteps );
	
			// Calculate the scale minimum
			if ( this.minAuto )
				this.min = this.min - MyMod( this.min, this.step );
	
			// Calculate the scale maximum
			if ( this.maxAuto )
				this.max = MyMod( this.max, this.step ) == 0.0 ? this.max :
					this.max + this.step - MyMod( this.max, this.step );
	
			// set the scale magnitude if required
			if ( this.scaleMagAuto )
			{
				// Find the optimal scale display multiple
				double mag = 0;
				double mag2 = 0;

				if ( Math.Abs( this.min ) > 1.0e-10 )
					mag = Math.Floor( Math.Log10( Math.Abs( this.min ) ) );
				if ( Math.Abs( this.max ) > 1.0e-10 )
					mag2 = Math.Floor( Math.Log10( Math.Abs( this.max ) ) );
				if ( Math.Abs( mag2 ) > Math.Abs( mag ) )
					mag = mag2;
		
				// Do not use scale multiples for magnitudes below 4
				if ( Math.Abs( mag ) <= 3 )
					mag = 0;
		
				// Use a power of 10 that is a multiple of 3 (engineering scale)
				this.scaleMag = (int) ( Math.Floor( mag / 3.0 ) * 3.0 );
			}
			
			// Calculate the appropriate number of dec places to display if required
			if ( this.numDecAuto )
			{
				this.numDec = 0 - (int) ( Math.Floor( Math.Log10( this.step ) ) - this.scaleMag );
				if ( this.numDec < 0 )
					this.numDec = 0;
			}
		}

		/// <summary>
		/// Calculate a step size based on a data range.
		/// </summary>
		/// <remarks>
		/// This utility method
		/// will try to honor the <see cref="Default.TargetXSteps"/> and
		/// <see cref="Default.TargetYSteps"/> number of
		/// steps while using a rational increment (1, 2, or 5 -- which are
		/// even divisors of 10).  This method is used by <see cref="PickScale"/>.
		/// </remarks>
		/// <param name="range">The range of data in user scale units.  This can
		/// be a full range of the data for the major step size, or just the
		/// value of the major step size to calculate the minor step size</param>
		/// <param name="targetSteps">The desired "typical" number of steps
		/// to divide the range into</param>
		/// <returns>The calculated step size for the specified data range.</returns>
		protected double CalcStepSize( double range, double targetSteps )
		{
			// Calculate an initial guess at step size
			double tempStep = range / targetSteps;

			// Get the magnitude of the step size
			double mag = Math.Floor( Math.Log10( tempStep ) );
			double magPow = Math.Pow( (double) 10.0, mag );
	
			// Calculate most significant digit of the new step size
			double magMsd =  ( (int) (tempStep / magPow + .5) );
	
			// promote the MSD to either 1, 2, or 5
			if ( magMsd > 5.0 )
				magMsd = 10.0;
			else if ( magMsd > 2.0 )
				magMsd = 5.0;
			else if ( magMsd > 1.0 )
				magMsd = 2.0;

			return magMsd * magPow;
		}

		/// <summary>
		/// Calculate a step size based on a data range, limited to a maximum number of steps.
		/// </summary>
		/// <remarks>
		/// This utility method
		/// will calculate a step size, of no more than maxSteps,
		/// using a rational increment (1, 2, or 5 -- which are
		/// even divisors of 10).  This method is used by <see cref="PickScale"/>.
		/// </remarks>
		/// <param name="range">The range of data in user scale units.  This can
		/// be a full range of the data for the major step size, or just the
		/// value of the major step size to calculate the minor step size</param>
		/// <param name="maxSteps">The maximum allowable number of steps
		/// to divide the range into</param>
		/// <returns>The calculated step size for the specified data range.</returns>
		protected double CalcBoundedStepSize( double range, double maxSteps )
		{
			// Calculate an initial guess at step size
			double tempStep = range / maxSteps;

			// Get the magnitude of the step size
			double mag = Math.Floor( Math.Log10( tempStep ) );
			double magPow = Math.Pow( (double) 10.0, mag );
	
			// Calculate most significant digit of the new step size
			double magMsd =  Math.Ceiling( tempStep / magPow );
	
			// promote the MSD to either 1, 2, or 5
			if ( magMsd > 5.0 )
				magMsd = 10.0;
			else if ( magMsd > 2.0 )
				magMsd = 5.0;
			else if ( magMsd > 1.0 )
				magMsd = 2.0;

			return magMsd * magPow;
		}

		/// <summary>
		/// Calculate a step size for a <see cref="AxisType.Date"/> scale.
		/// This method is used by <see cref="PickScale"/>.
		/// </summary>
		/// <param name="range">The range of data in units of days</param>
		/// <param name="targetSteps">The desired "typical" number of steps
		/// to divide the range into</param>
		/// <returns>The calculated step size for the specified data range.  Also
		/// calculates and sets the values for <see cref="MajorUnit"/>,
		/// <see cref="MinorUnit"/>, <see cref="MinorStep"/>, and
		/// <see cref="ScaleFormat"/></returns>
		protected double CalcDateStepSize( double range, double targetSteps )
		{
			// Calculate an initial guess at step size
			double tempStep = range / targetSteps;

			if ( range > Default.RangeYearYear )
			{
				majorUnit = DateUnit.Year;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatYearYear;					
					
				tempStep = Math.Ceiling( tempStep / 365.0 );
				if ( tempStep < 1.0 )
					tempStep = 1.0;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Year;
					if ( tempStep == 1.0 )
						minorStep = 0.25;
					else
						minorStep = CalcStepSize( tempStep, targetSteps );
				}
			}
			else if ( range > Default.RangeYearMonth )
			{
				majorUnit = DateUnit.Year;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatYearMonth;
				tempStep = 1.0;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Month;
					// Calculate the minor steps to give an estimated 4 steps
					// per major step.
					minorStep = Math.Ceiling( range / ( targetSteps * 3 ) / 30.0 );
					// make sure the minorStep is 1, 2, 3, 6, or 12 months
					if ( minorStep > 6 )
						minorStep = 12;
					else if ( minorStep > 3 )
						minorStep = 6;
				}
			}
			else if ( range > Default.RangeMonthMonth )
			{
				majorUnit = DateUnit.Month;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatMonthMonth;
				tempStep = Math.Ceiling( tempStep / 30.0 );
				if ( tempStep < 1.0 )
					tempStep = 1.0;
				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Month;
					minorStep = 0.25;
				}
			}
			else if ( range > Default.RangeDayDay )
			{
				majorUnit = DateUnit.Day;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatDayDay;
				tempStep = Math.Ceiling( tempStep );
				if ( tempStep < 1.0 )
					tempStep = 1.0;
				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Day;
					minorStep = 1.0;
				}
			}
			else if ( range > Default.RangeDayHour )
			{
				majorUnit = DateUnit.Day;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatDayHour;
				tempStep = 1.0;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Hour;
					// Calculate the minor steps to give an estimated 4 steps
					// per major step.
					minorStep = Math.Ceiling( range / ( targetSteps * 3 ) * XDate.HoursPerDay );
					// make sure the minorStep is 1, 2, 3, 6, or 12 hours
					if ( minorStep > 6 )
						minorStep = 12;
					else if ( minorStep > 3 )
						minorStep = 6;
					else if ( minorStep < 1 )
						minorStep = 1;
				}
			}
			else if ( range > Default.RangeHourHour )
			{
				majorUnit = DateUnit.Hour;
				tempStep = Math.Ceiling( tempStep * XDate.HoursPerDay );
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatHourHour;

				if ( tempStep > 12.0 )
					tempStep = 24.0;
				else if ( tempStep > 6.0 )
					tempStep = 12.0;
				else if ( tempStep > 3.0 )
					tempStep = 6.0;
				else if ( tempStep < 1.0 )
					tempStep = 1.0;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Hour;
					minorStep = 0.25;
				}
			}
			else if ( range > Default.RangeHourMinute )
			{
				majorUnit = DateUnit.Hour;
				tempStep = 1.0;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatHourMinute;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Minute;
					// Calculate the minor steps to give an estimated 4 steps
					// per major step.
					minorStep = Math.Ceiling( range / ( targetSteps * 3 ) * XDate.MinutesPerDay );
					// make sure the minorStep is 1, 5, 15, or 30 minutes
					if ( minorStep > 15.0 )
						minorStep = 30.0;
					else if ( minorStep > 5.0 )
						minorStep = 15.0;
					else if ( minorStep > 1.0 )
						minorStep = 5.0;
					else if ( minorStep < 1.0 )
						minorStep = 1.0;
				}
			}
			else if ( range > Default.RangeMinuteMinute )
			{
				majorUnit = DateUnit.Minute;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatMinuteMinute;

				tempStep = Math.Ceiling( tempStep * XDate.MinutesPerDay );
				// make sure the minute step size is 1, 5, 15, or 30 minutes
				if ( tempStep > 15.0 )
					tempStep = 30.0;
				else if ( tempStep > 5.0 )
					tempStep = 15.0;
				else if ( tempStep > 1.0 )
					tempStep = 5.0;
				else if ( tempStep < 1.0 )
					tempStep = 1.0;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Minute;
					minorStep = 0.25;
				}
			}
			else if ( range > Default.RangeMinuteSecond )
			{
				majorUnit = DateUnit.Minute;
				tempStep = 1.0;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatMinuteSecond;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Second;
					// Calculate the minor steps to give an estimated 4 steps
					// per major step.
					minorStep = Math.Ceiling( range / ( targetSteps * 3 ) * XDate.SecondsPerDay );
					// make sure the minorStep is 1, 5, 15, or 30 seconds
					if ( minorStep > 15.0 )
						minorStep = 30.0;
					else if ( minorStep > 5.0 )
						minorStep = 15.0;
					else if ( minorStep > 1.0 )
						minorStep = 5.0;
					else if ( minorStep < 1.0 )
						minorStep = 1.0;
				}
			}
			else // SecondSecond
			{
				majorUnit = DateUnit.Second;
				if ( this.scaleFormatAuto )
					this.scaleFormat = Default.FormatSecondSecond;

				tempStep = Math.Ceiling( tempStep * XDate.SecondsPerDay );
				// make sure the second step size is 1, 5, 15, or 30 seconds
				if ( tempStep > 15.0 )
					tempStep = 30.0;
				else if ( tempStep > 5.0 )
					tempStep = 15.0;
				else if ( tempStep > 1.0 )
					tempStep = 5.0;
				else if ( tempStep < 1.0 )
					tempStep = 1.0;

				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Second;
					minorStep = 0.25;
				}
			}

			return tempStep;
		}

		/// <summary>
		/// Calculate a date that is close to the specified date and an
		/// even multiple of the selected
		/// <see cref="MajorUnit"/> for a <see cref="AxisType.Date"/> scale.
		/// This method is used by <see cref="PickScale"/>.
		/// </summary>
		/// <param name="date">The date which the calculation should be close to</param>
		/// <param name="direction">The desired direction for the date to take.
		/// 1 indicates the result date should be greater than the specified
		/// date parameter.  -1 indicates the other direction.</param>
		/// <returns>The calculated date</returns>
		protected double CalcEvenStepDate( double date, int direction )
		{
			int year, month, day, hour, minute, second;

			XDate.XLDateToCalendarDate( date, out year, out month, out day,
										out hour, out minute, out second );

			// If the direction is -1, then it is sufficient to go to the beginning of
			// the current time period, .e.g., for 15-May-95, and monthly steps, we
			// can just back up to 1-May-95
			if ( direction < 0 )
				direction = 0;

			switch ( majorUnit )
			{
				case DateUnit.Year:
				default:
					// If the date is already an exact year, then don't step to the next year
					if ( direction == 1 && month == 1 && day == 1 && hour == 0
						&& minute == 0 && second == 0 )
						return date;
					else
						return XDate.CalendarDateToXLDate( year+direction, 1, 1,
														0, 0, 0 );
				case DateUnit.Month:
					// If the date is already an exact month, then don't step to the next month
					if ( direction == 1 && day == 1 && hour == 0
						&& minute == 0 && second == 0 )
						return date;
					else
						return XDate.CalendarDateToXLDate( year, month+direction, 1,
												0, 0, 0 );
				case DateUnit.Day:
					// If the date is already an exact Day, then don't step to the next day
					if ( direction == 1 && hour == 0 && minute == 0 && second == 0 )
						return date;
					else
						return XDate.CalendarDateToXLDate( year, month,
											day+direction, 0, 0, 0 );
				case DateUnit.Hour:
					// If the date is already an exact hour, then don't step to the next hour
					if ( direction == 1 && minute == 0 && second == 0 )
						return date;
					else
						return XDate.CalendarDateToXLDate( year, month, day,
													hour+direction, 0, 0 );
				case DateUnit.Minute:
					// If the date is already an exact minute, then don't step to the next minute
					if ( direction == 1 && second == 0 )
						return date;
					else
						return XDate.CalendarDateToXLDate( year, month, day, hour,
													minute+direction, 0 );
				case DateUnit.Second:
					return XDate.CalendarDateToXLDate( year, month, day, hour,
													minute, second+direction );

			}
		}

		/// <summary>
		/// Calculate the modulus (remainder) in a safe manner so that divide
		/// by zero errors are avoided
		/// </summary>
		/// <param name="x">The divisor</param>
		/// <param name="y">The dividend</param>
		/// <returns>the value of the modulus, or zero for the divide-by-zero
		/// case</returns>
		protected double MyMod( double x, double y )
		{
			double temp;
			
			if ( y == 0 )
				return 0;
		
			temp = x/y;
			return y*(temp - Math.Floor(temp));
		}
	#endregion
	
	#region Coordinate Transform Methods
		/// <summary>
		/// Transform the coordinate value from user coordinates (scale value)
		/// to graphics device coordinates (pixels).
		/// </summary>
		/// <remarks>This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).
		/// Note that the <see cref="GraphPane.AxisRect"/> must be valid, and
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method (this is called everytime
		/// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
		/// </remarks>
		/// <param name="x">The coordinate value, in user scale units, to
		/// be transformed</param>
		/// <returns>the coordinate value transformed to screen coordinates
		/// for use in calling the <see cref="Graphics"/> draw routines</returns>
		public float Transform( double x )
		{
			// Must take into account Log, and Reverse Axes
			double ratio;
			if ( this.IsLog )
				ratio = ( SafeLog( x ) - this.minScale ) / ( this.maxScale  - this.minScale );
			else
				ratio = ( x - this.minScale ) / ( this.maxScale - this.minScale );
				
			if ( this.isReverse && (this is XAxis)  )
				return (float) ( this.maxPix - ( this.maxPix - this.minPix ) * ratio );
			else if ( this is XAxis )
				return (float) ( this.minPix +  ( this.maxPix - this.minPix ) * ratio );
			else if ( this.isReverse )
				return (float) ( this.minPix + ( this.maxPix - this.minPix ) * ratio );
			else
				return (float) ( this.maxPix - ( this.maxPix - this.minPix ) * ratio );
		}

		/// <summary>
		/// Transform the coordinate value from user coordinates (scale value)
		/// to graphics device coordinates (pixels).
		/// </summary>
		/// <remarks>
		/// This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).
		/// Note that the <see cref="GraphPane.AxisRect"/> must be valid, and
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method (this is called everytime
		/// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
		/// </remarks>
		/// <param name="i">The ordinal value of this point, just in case
		/// this is an <see cref="AxisType.Ordinal"/> axis</param>
		/// <param name="x">The coordinate value, in user scale units, to
		/// be transformed</param>
		/// <returns>the coordinate value transformed to screen coordinates
		/// for use in calling the <see cref="Graphics"/> draw routines</returns>
		public float Transform( int i, double x )
		{
			// ordinal types ignore the X value, and just use the ordinal position
			if ( ( this.IsOrdinal || this.IsText ) && i >= 0 )
				x = (double) i + 1.0;
			return Transform( x );

		}

		/// <summary>
		/// Reverse transform the user coordinates (scale value)
		/// given a graphics device coordinate (pixels).
		/// </summary>
		/// <remarks>
		/// This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).
		/// Note that the <see cref="GraphPane.AxisRect"/> must be valid, and
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method (this is called everytime
		/// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
		/// </remarks>
		/// <param name="pixVal">The screen pixel value, in graphics device coordinates to
		/// be transformed</param>
		/// <returns>The user scale value that corresponds to the screen pixel location</returns>
		public double ReverseTransform( float pixVal )
		{
			double val;
			
			// see if the sign of the equation needs to be reversed
			if ( (this.isReverse) == (this is XAxis) )
				val = (double) ( pixVal - this.maxPix )
						/ (double) ( this.minPix - this.maxPix )
						* ( this.maxScale - this.minScale ) + this.minScale;
			else
				val = (double) ( pixVal - this.minPix )
						/ (double) ( this.maxPix - this.minPix )
						* ( this.maxScale - this.minScale ) + this.minScale;
			
			if ( this.IsLog )
				val = Math.Pow( 10.0, val );
			
			return val;
		}
				

		/// <summary>
		/// Transform the coordinate value from user coordinates (scale value)
		/// to graphics device coordinates (pixels).
		/// </summary>
		/// <remarks>Assumes that the origin
		/// has been set to the "left" of this axis, facing from the label side.
		/// Note that the left side corresponds to the scale minimum for the X and
		/// Y2 axes, but it is the scale maximum for the Y axis.
		/// This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).  Note that
		/// the <see cref="GraphPane.AxisRect"/> must be valid, and
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method.
		/// </remarks>
		/// <param name="x">The coordinate value, in user scale units, to
		/// be transformed</param>
		/// <returns>the coordinate value transformed to screen coordinates
		/// for use in calling the <see cref="DrawScale"/> method</returns>
		public float LocalTransform( double x )
		{
			// Must take into account Log, and Reverse Axes
			double	ratio;
			float	rv;

			// Coordinate values for log scales are already in exponent form, so no need
			// to take the log here
			ratio = ( x - this.minScale ) / ( this.maxScale - this.minScale );
			
			if ( ( this.isReverse && !(this is YAxis) ) ||
				( !this.isReverse && (this is YAxis ) ) )
				rv = (float) ( ( this.maxPix - this.minPix ) * ( 1.0F - ratio ) );
			else
				rv = (float) ( ( this.maxPix - this.minPix ) * ratio );

			return rv;
		}

		/// <summary>
		/// Calculate a base 10 logarithm in a safe manner to avoid math exceptions
		/// </summary>
		/// <param name="x">The value for which the logarithm is to be calculated</param>
		/// <returns>The value of the logarithm, or 0 if the <paramref name="x"/>
		/// argument was negative or zero</returns>
		public double SafeLog( double x )
		{
			if ( x > 1.0e-20 )
				return Math.Log10( x );
			else
				return 0.0;
		}
	#endregion
	}
}

