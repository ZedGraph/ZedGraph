using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZedGraph
{
	/// <summary>
	/// A simple struct of static values that define the default property
	/// values for all aspects of the <see cref="GraphPane"/> class.  The
	/// <see cref="Def"/> class is sub-divided into structs that contain
	/// defaults for each class that makes up the GraphPane.  The following
	/// class defaults are included:
	/// <list type="table">
	/// <item>
	///		<description><see cref="Def.Lin"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.Line"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Sym"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.Symbol"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Arr"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.ArrowItem"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Pane"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.GraphPane"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Leg"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.Legend"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Curve"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.CurveItem"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Ax"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.Axis"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.XAx"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.XAxis"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.YAx"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.YAxis"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Y2Ax"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.Y2Axis"/> class</description>
	///	</item>
	/// <item>
	///		<description><see cref="Def.Text"/></description>
	///		<description>Defaults for the <see cref="ZedGraph.TextItem"/> class</description>
	///	</item>
	/// </list>
	/// Any of these static values can be changed at runtime, which will cause
	/// all subsequently instantiated Graphs to use the next defaults.
	/// </summary>
	public struct Def
	{
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="ZedGraph.Line"/> class.
		/// </summary>
		public struct Lin
		{
			// Default Line properties
			/// <summary>
			/// The default color for curves (line segments connecting the points).
			/// This is the default value for the <see cref="Line.Color"/> property.
			/// </summary>
			public static Color Color = Color.Red;
			/// <summary>
			/// The default mode for displaying line segments (<see cref="Line.IsVisible"/>
			/// property).  True to show the line segments, false to hide them.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default width for line segments (<see cref="Line.Width"/> property).
			/// Units are pixels.
			/// </summary>
			public static float Width = 1;
			/// <summary>
			/// The default drawing style for line segments (<see cref="Line.Style"/> property).
			/// This is defined with the <see cref="DashStyle"/> enumeration.
			/// </summary>
			public static DashStyle Style = DashStyle.Solid;
			/// <summary>
			/// Default value for the curve type property
			/// (<see cref="Line.StepType"/>).  This determines if the curve
			/// will be drawn by directly connecting the points from the
			/// <see cref="CurveItem.X"/> and <see cref="CurveItem.Y"/> data arrays,
			/// or if the curve will be a "stair-step" in which the points are
			/// connected by a series of horizontal and vertical lines that
			/// represent discrete, staticant values.  Note that the values can
			/// be forward oriented <code>ForwardStep</code> (<see cref="StepType"/>) or
			/// rearward oriented <code>RearwardStep</code>.
			/// That is, the points are defined at the beginning or end
			/// of the staticant value for which they apply, respectively.
			/// </summary>
			/// <value><see cref="StepType"/> enum value</value>
			public static StepType Type = StepType.NonStep;
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="ZedGraph.Symbol"/> class.
		/// </summary>
		public struct Sym
		{
			// Default Symbol properties
			/// <summary>
			/// The default size for curve symbols (<see cref="Symbol.Size"/> property),
			/// in units of points.
			/// </summary>
			public static float Size = 7;
			/// <summary>
			/// The default pen width to be used for drawing curve symbols
			/// (<see cref="Symbol.PenWidth"/> property).  Units are points.
			/// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default fill mode for symbols (<see cref="Symbol.IsFilled"/> property).
			/// true to have symbols filled in with color, false to leave them as outlines.
			/// </summary>
			public static bool IsFilled = false;
			/// <summary>
			/// The default symbol type for curves (<see cref="Symbol.Type"/> property).
			/// This is defined as a <see cref="ZedGraph.SymbolType"/> enumeration.
			/// </summary>
			public static SymbolType Type = SymbolType.Square;
			/// <summary>
			/// The default display mode for symbols (<see cref="Symbol.IsVisible"/> property).
			/// true to display symbols, false to hide them.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default color for drawing symbols (<see cref="Symbol.Color"/> property).
			/// </summary>
			public static Color Color = Color.Red;
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="GraphPane"/> class.
		/// </summary>
		public struct Pane
		{
			// Default GraphPane properties
			/// <summary>
			/// The default display mode for the title at the top of the pane
			/// (<see cref="GraphPane.IsShowTitle"/> property).  true to
			/// display a title, false otherwise.
			/// </summary>
			public static bool ShowTitle = true;
			/// <summary>
			/// The default font family for the pane title
			/// (<see cref="GraphPane.Title"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size (points) for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="FontSpec.Size"/> property).
			/// </summary>
			public static float FontSize = 16;
			/// <summary>
			/// The default font color for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = true;
			/// <summary>
			/// The default font italic mode for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
			/// <summary>
			/// The default font underline mode for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			
			//		public static bool stepPlot = false;
			/// <summary>
			/// The default frame mode for the <see cref="GraphPane"/>.
			/// (<see cref="GraphPane.IsPaneFramed"/> property). true
			/// to draw a frame around the <see cref="GraphPane.PaneRect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsFramed = true;
			/// <summary>
			/// The default color for the <see cref="GraphPane"/> frame border.
			/// (<see cref="GraphPane.PaneFrameColor"/> property). 
			/// </summary>
			public static Color FrameColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="GraphPane.PaneRect"/> background.
			/// (<see cref="GraphPane.PaneBackColor"/> property). 
			/// </summary>
			public static Color BackColor = Color.White;
			/// <summary>
			/// The default pen width for the <see cref="GraphPane"/> frame border.
			/// (<see cref="GraphPane.PaneFramePenWidth"/> property).  Units are in pixels.
			/// </summary>
			public static float FramePenWidth = 1;

			/// <summary>
			/// The default value for the <see cref="GraphPane.PaneGap"/> property.
			/// This is the size of the margin around the edge of the
			/// <see cref="GraphPane.PaneRect"/>, in units of pixels.
			/// </summary>
			public static float Gap = 20;
			/// <summary>
			/// The default dimension of the <see cref="GraphPane.PaneRect"/>, which
			/// defines a normal sized plot.  This dimension is used to scale the
			/// fonts, symbols, etc. according to the actual size of the
			/// <see cref="GraphPane.PaneRect"/>.
			/// </summary>
			/// <seealso cref="GraphPane.CalcScaleFactor"/>
			public static double BaseDimension = 8.0;
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="ZedGraph.Legend"/> class.
		/// </summary>
		public struct Leg
		{
			// Default Legend properties
			/// <summary>
			/// The default pen width for the <see cref="Legend"/> frame border.
			/// (<see cref="Legend.FrameWidth"/> property).  Units are in pixels.
			/// </summary>
			public static float FrameWidth = 1;
			/// <summary>
			/// The default color for the <see cref="Legend"/> frame border.
			/// (<see cref="Legend.FrameColor"/> property). 
			/// </summary>
			public static Color FrameColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="Legend"/> background.
			/// (<see cref="Legend.FillColor"/> property).  Use of this
			/// color depends on the status of the <see cref="Legend.IsFilled"/>
			/// property.
			/// </summary>
			public static Color FillColor = Color.White;
			/// <summary>
			/// The default location for the <see cref="Legend"/> on the graph
			/// (<see cref="Legend.Location"/> property).  This property is
			/// defined as a <see cref="LegendLoc"/> enumeration.
			/// </summary>
			public static LegendLoc Location = LegendLoc.Top;
			/// <summary>
			/// The default frame mode for the <see cref="Legend"/>.
			/// (<see cref="Legend.IsFramed"/> property). true
			/// to draw a frame around the <see cref="Legend.Rect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsFramed = true;
			/// <summary>
			/// The default display mode for the <see cref="Legend"/>.
			/// (<see cref="Legend.IsVisible"/> property). true
			/// to show the legend,
			/// false to hide it.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default fill mode for the <see cref="Legend"/> background
			/// (<see cref="Legend.IsFilled"/> property).
			/// true to fill-in the background with color,
			/// false to leave the background transparent.
			/// </summary>
			public static bool IsFilled = true;
			/// <summary>
			/// The default horizontal stacking mode for the <see cref="Legend"/>
			/// (<see cref="Legend.IsHStack"/> property).
			/// true to allow horizontal legend item stacking, false to allow
			/// only vertical legend orientation.
			/// </summary>
			public static bool HStack = true;

			/// <summary>
			/// The default font family for the <see cref="Legend"/> entries
			/// (<see cref="FontSpec.Family"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size for the <see cref="Legend"/> entries
			/// (<see cref="FontSpec.Size"/> property).  Units are
			/// in points (1/72 inch).
			/// </summary>
			public static float FontSize = 12;
			/// <summary>
			/// The default font color for the <see cref="Legend"/> entries
			/// (<see cref="FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the <see cref="Legend"/> entries
			/// (<see cref="FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = false;
			/// <summary>
			/// The default font italic mode for the <see cref="Legend"/> entries
			/// (<see cref="FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
			/// <summary>
			/// The default font underline mode for the <see cref="Legend"/> entries
			/// (<see cref="FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="Axis"/> class.
		/// </summary>
		public struct Ax
		{
			// Default Axis Properties
			/// <summary>
			/// The default size for the <see cref="Axis"/> tic marks.
			/// (<see cref="Axis.TicSize"/> property). Units are pixels.
			/// </summary>
			public static float TicSize = 5;
			/// <summary>
			/// The default size for the <see cref="Axis"/> minor tic marks.
			/// (<see cref="Axis.MinorTicSize"/> property). Units are pixels.
			/// </summary>
			public static float MinorTicSize = 2.5F;
			/// <summary>
			/// The default pen width for drawing the <see cref="Axis"/> tic marks.
			/// (<see cref="Axis.TicPenWidth"/> property). Units are pixels.
			/// </summary>
			public static float TicPenWidth = 1.0F;
			/// <summary>
			/// The default "zero lever" for automatically selecting the axis
			/// scale range (see <see cref="Axis.PickScale"/>). This number is
			/// used to determine when an axis scale range should be extended to
			/// include the zero value.  This value is maintained only in the
			/// <see cref="Def"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double ZeroLever = 0.25;
			/// <summary>
			/// The default target number of steps for automatically selecting the axis
			/// scale step size (see <see cref="Axis.PickScale"/>).
			/// This number is an initial target value for the number of major steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Def"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetSteps = 7.0;
			/// <summary>
			/// The default target number of minor steps for automatically selecting the axis
			/// scale minor step size (see <see cref="Axis.PickScale"/>).
			/// This number is an initial target value for the number of minor steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Def"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetMinorSteps = 5.0;
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
			/// The default "dash on" size for drawing the <see cref="Axis"/> grid
			/// (<see cref="Axis.GridDashOn"/> property). Units are in pixels.
			/// </summary>
			public static float GridDashOn = 1.0F;
			/// <summary>
			/// The default "dash off" size for drawing the <see cref="Axis"/> grid
			/// (<see cref="Axis.GridDashOff"/> property). Units are in pixels.
			/// </summary>
			public static float GridDashOff = 5.0F;
			/// <summary>
			/// The default pen width for drawing the <see cref="Axis"/> grid
			/// (<see cref="Axis.GridPenWidth"/> property). Units are in pixels.
			/// </summary>
			public static float GridPenWidth = 1.0F;
			/// <summary>
			/// The default color for the <see cref="Axis"/> grid lines
			/// (<see cref="Axis.GridColor"/> property).  This color only affects the
			/// grid lines.
			/// </summary>
			public static Color GridColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="Axis"/> itself
			/// (<see cref="Axis.Color"/> property).  This color only affects the
			/// tic marks and the axis border.
			/// </summary>
			public static Color Color = Color.Black;
			/// <summary>
			/// The default color for the <see cref="Axis"/> frame border.
			/// (<see cref="GraphPane.AxisFrameColor"/> property). 
			/// </summary>
			public static Color FrameColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="GraphPane.AxisRect"/> background.
			/// (<see cref="GraphPane.AxisBackColor"/> property). 
			/// </summary>
			public static Color BackColor = Color.White;
			/// <summary>
			/// The default pen width for drawing the 
			/// <see cref="GraphPane.AxisRect"/> frame border
			/// (<see cref="GraphPane.AxisFramePenWidth"/> property).
			/// Units are in pixels.
			/// </summary>
			public static float FramePenWidth = 1F;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> frame border
			/// (<see cref="GraphPane.IsAxisFramed"/> property). true
			/// to show the frame border, false to omit the border
			/// </summary>
			public static bool IsFramed = true;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> grid lines
			/// (<see cref="Axis.IsShowGrid"/> property). true
			/// to show the grid lines, false to hide them.
			/// </summary>
			public static bool IsShowGrid = false;
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
			/// The default settings for the <see cref="Axis"/> scale ignore initial
			/// zero values option (<see cref="GraphPane.IsIgnoreInitial"/> property).
			/// true to have the auto-scale-range code ignore the initial data points
			/// until the first non-zero Y value, false otherwise.
			/// </summary>
			public static bool IgnoreInitial = false;
			/// <summary>
			/// The default setting for the <see cref="Axis"/> scale axis type
			/// (<see cref="Axis.Type"/> property).  This value is set as per
			/// the <see cref="AxisType"/> enumeration
			/// </summary>
			public static AxisType Type = AxisType.Linear;
			/// <summary>
			/// The default setting for the <see cref="Axis"/> scale date format string
			/// (<see cref="Axis.ScaleFormat"/> property).  This value is set as per
			/// the <see cref="XDate.ToString"/> function.
			/// </summary>
			public static string ScaleFormat = "&dd-&mmm-&yy &hh:&nn";
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
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="XAxis"/> class.
		/// </summary>
		public struct XAx
		{
			// Default X Axis properties
			/// <summary>
			/// The default display mode for the <see cref="XAxis"/>
			/// (<see cref="Axis.IsVisible"/> property). true to display the scale
			/// values, title, tic marks, false to hide the axis entirely.
			/// </summary>
			public static bool IsVisible = true;
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="YAxis"/> class.
		/// </summary>
		public struct YAx
		{
			// Default Y Axis properties
			/// <summary>
			/// The default display mode for the <see cref="YAxis"/>
			/// (<see cref="Axis.IsVisible"/> property). true to display the scale
			/// values, title, tic marks, false to hide the axis entirely.
			/// </summary>
			public static bool IsVisible = true;
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="Y2Axis"/> class.
		/// </summary>
		public struct Y2Ax
		{
			// Default Y2 Axis properties
			/// <summary>
			/// The default display mode for the <see cref="Y2Axis"/>
			/// (<see cref="Axis.IsVisible"/> property). true to display the scale
			/// values, title, tic marks, false to hide the axis entirely.
			/// </summary>
			public static bool IsVisible = false;
		}

		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="CurveItem"/> class.
		/// </summary>
		public struct Curve
		{
			// Default curve properties
		}
		
		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="TextItem"/> class.
		/// </summary>
		public struct Text
		{
			// Default text item properties
			/// <summary>
			/// Default value for the vertical <see cref="TextItem"/>
			/// text alignment (<see cref="TextItem.AlignV"/> property).
			/// This is specified
			/// using the <see cref="FontAlignV"/> enum type.
			/// </summary>
			public static FontAlignV AlignV = FontAlignV.Center;
			/// <summary>
			/// Default value for the horizontal <see cref="TextItem"/>
			/// text alignment (<see cref="TextItem.AlignH"/> property).
			/// This is specified
			/// using the <see cref="FontAlignH"/> enum type.
			/// </summary>
			public static FontAlignH AlignH = FontAlignH.Center;
			/// <summary>
			/// The default coordinate system to be used for defining the
			/// <see cref="TextItem"/> location coordinates
			/// (<see cref="TextItem.CoordinateFrame"/> property).
			/// </summary>
			/// <value> The coordinate system is defined with the <see cref="CoordType"/>
			/// enum</value>
			public static CoordType CoordFrame = CoordType.AxisXYScale;
			/// <summary>
			/// The default font family for the <see cref="TextItem"/> text
			/// (<see cref="FontSpec.Family"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size for the <see cref="TextItem"/> text
			/// (<see cref="FontSpec.Size"/> property).  Units are
			/// in points (1/72 inch).
			/// </summary>
			public static float FontSize = 14.0F;
			/// <summary>
			/// The default font color for the <see cref="TextItem"/> text
			/// (<see cref="FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the <see cref="TextItem"/> text
			/// (<see cref="FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = true;
			/// <summary>
			/// The default font underline mode for the <see cref="TextItem"/> text
			/// (<see cref="FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			/// <summary>
			/// The default font italic mode for the <see cref="TextItem"/> text
			/// (<see cref="FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
		}


		/// <summary>
		/// A simple subclass of the <see cref="Def"/> class that defines the
		/// default property values for the <see cref="ArrowItem"/> class.
		/// </summary>
		public struct Arr
		{
			/// <summary>
			/// The default size for the <see cref="ArrowItem"/> item arrowhead
			/// (<see cref="ArrowItem.Size"/> property).  Units are in pixels.
			/// </summary>
			public static float Size = 12.0F;
			/// <summary>
			/// The default coordinate system to be used for defining the
			/// <see cref="ArrowItem"/> location coordinates
			/// (<see cref="ArrowItem.CoordinateFrame"/> property).
			/// </summary>
			/// <value> The coordinate system is defined with the <see cref="CoordType"/>
			/// enum</value>
			public static CoordType CoordFrame = CoordType.AxisXYScale;
			/// <summary>
			/// The default display mode for the <see cref="ArrowItem"/> item arrowhead
			/// (<see cref="ArrowItem.IsArrowHead"/> property).  true to show the
			/// arrowhead, false to hide it.
			/// </summary>
			public static bool IsArrowHead = true;
			/// <summary>
			/// The default pen width used for the <see cref="ArrowItem"/> line segment
			/// (<see cref="ArrowItem.PenWidth"/> property).  Units are pixels.
			/// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default color used for the <see cref="ArrowItem"/> line segment
			/// and arrowhead (<see cref="ArrowItem.Color"/> property).
			/// </summary>
			public static Color Color = Color.Red;
		}
	}
}