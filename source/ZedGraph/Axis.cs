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
	/// <version> $Revision: 3.59 $ $Date: 2006-03-17 06:21:14 $ </version>
	[Serializable]
	abstract public class Axis : ISerializable, ICloneable
	{
		#region Class Fields

		/// <summary>
		/// private field that stores the <see cref="ZedGraph.Scale" /> class, which implements all the
		/// calculations and methods associated with the numeric scale for this
		/// <see cref="Axis" />.  See the public property <see cref="Scale" /> to access this class.
		/// </summary>
		internal Scale scale;

		/// <summary> Private fields for the <see cref="Axis"/> scale rendering properties.
		/// Use the public properties <see cref="Cross"/> and <see cref="BaseTic"/>
		/// for access to these values.
		/// </summary>
		protected double cross;

		/// <summary> Private field for the <see cref="Axis"/> automatic cross position mode.
		/// Use the public property <see cref="CrossAuto"/> for access to this value.
		/// </summary>
		protected bool crossAuto;

		/// <summary> Private fields for the <see cref="Axis"/> attributes.
		/// Use the public properties <see cref="IsVisible"/>, <see cref="IsShowGrid"/>,
		/// <see cref="IsShowMinorGrid"/>, <see cref="IsZeroLine"/>,  <see cref="IsShowTitle"/>,
		/// <see cref="IsTic"/>, <see cref="IsInsideTic"/>, <see cref="IsOppositeTic"/>,
		/// <see cref="IsMinorTic"/>, <see cref="IsMinorInsideTic"/>,
		/// <see cref="IsMinorOppositeTic"/>, <see cref="IsTicsBetweenLabels"/> and
		/// <see cref="IsOmitMag"/> for access to these values.
		/// </summary>
		protected bool isVisible,
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
							isCrossTic,
							isInsideCrossTic,
							isMinorCrossTic,
							isMinorInsideCrossTic,
							isTicsBetweenLabels,
							isOmitMag,
							isScaleVisible,
							isAxisSegmentVisible,
							isScaleLabelsInside,
							isSkipFirstLabel,
							isSkipLastLabel,
							isSkipCrossLabel,
							isTitleAtCross;

		/// <summary> Private field for the <see cref="Axis"/> title string.
		/// Use the public property <see cref="Title"/>
		/// for access to this value. </summary>
		protected string title;

		/// <summary>
		/// A tag object for use by the user.  This can be used to store additional
		/// information associated with the <see cref="Axis"/>.  ZedGraph does
		/// not use this value for any purpose.
		/// </summary>
		/// <remarks>
		/// Note that, if you are going to Serialize ZedGraph data, then any type
		/// that you store in <see cref="Tag"/> must be a serializable type (or
		/// it will cause an exception).
		/// </remarks>
		public object Tag;

		/// <summary> Private field for the alignment of the <see cref="Axis"/> tic labels.
		/// This fields controls whether the inside, center, or outside edges of the text labels are aligned.
		/// Use the public property <see cref="ScaleAlign"/>
		/// for access to this value. </summary>
		/// <seealso cref="ScaleFormatAuto"/>
		private AlignP scaleAlign;

		/// <summary> Private fields for the <see cref="Axis"/> font specificatios.
		/// Use the public properties <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> for access to these values. </summary>
		protected FontSpec titleFontSpec,
									scaleFontSpec;

		/// <summary> Private fields for the <see cref="Axis"/> drawing dimensions.
		/// Use the public properties <see cref="TicPenWidth"/>, <see cref="TicSize"/>,
		/// <see cref="MinorTicSize"/>,
		/// <see cref="GridDashOn"/>, <see cref="GridDashOff"/>,
		/// <see cref="GridPenWidth"/>,
		/// <see cref="MinorGridDashOn"/>, <see cref="MinorGridDashOff"/>,
		/// and <see cref="MinorGridPenWidth"/> for access to these values. </summary>
		private float ticPenWidth,
							ticSize,
							minorTicSize,
							gridDashOn,
							gridDashOff,
							gridPenWidth,
							minorGridDashOn,
							minorGridDashOff,
							minorGridPenWidth,
							axisGap;

		/// <summary>
		/// Private field for the <see cref="Axis"/> minimum allowable space allocation.
		/// Use the public property <see cref="MinSpace"/> to access this value.
		/// </summary>
		/// <seealso cref="Default.MinSpace"/>
		private float minSpace;

		/// <summary> Private fields for the <see cref="Axis"/> colors.
		/// Use the public properties <see cref="Color"/> and
		/// <see cref="GridColor"/> for access to these values. </summary>
		private Color color,
							gridColor,
							minorGridColor;

		/// <summary>
		/// Temporary values for axis space calculations (see <see cref="CalcSpace" />).
		/// </summary>
		internal float tmpSpace;
		//tmpMinSpace,


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
			/// The default size for the gap between multiple axes
			/// (<see cref="Axis.AxisGap"/> property). Units are in points (1/72 inch).
			/// </summary>
			public static float AxisGap = 5;
			/// <summary>
			/// Determines the size of the band at the beginning and end of the axis that will have labels
			/// omitted if the axis is shifted due to a non-default location using the <see cref="Axis.Cross"/>
			/// property.
			/// </summary>
			/// <remarks>
			/// This parameter applies only when <see cref="CrossAuto"/> is false.  It is scaled according
			/// to the size of the graph based on <see cref="PaneBase.BaseDimension"/>.  When a non-default
			/// axis location is selected, the first and last labels on that axis will overlap the opposing
			/// axis frame.  This parameter allows those labels to be omitted to avoid the overlap.  Set this
			/// parameter to zero to turn off the effect.
			/// </remarks>
			public static float EdgeTolerance = 6;
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
			/// The default value for <see cref="Axis.IsScaleVisible"/>, which determines
			/// whether or not the scale values are displayed.
			/// </summary>
			public static bool IsScaleVisible = true;
			/// <summary>
			/// The default value for <see cref="Axis.IsAxisSegmentVisible"/>, which determines
			/// whether or not the scale segment itself is visible
			/// </summary>
			public static bool IsAxisSegmentVisible = true;

			/// <summary>
			/// The default value for <see cref="Axis.IsScaleLabelsInside"/>, which determines
			/// whether or not the scale labels and title for the <see cref="Axis"/> will appear
			/// on the opposite side of the <see cref="Axis"/> that it normally appears.
			/// </summary>
			public static bool IsScaleLabelsInside = false;

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
			/// The default display mode for the <see cref="Axis"/> major outside 
			/// "cross" tic marks (<see cref="Axis.IsCrossTic"/> property).
			/// </summary>
			/// <remarks>
			/// The "cross" tics are a special, additional set of tic marks that
			/// always appear on the actual axis, even if it has been shifted due
			/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
			/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
			/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
			/// they will exactly overlay the "normal" and "inside" tics.  If
			/// <see cref="CrossAuto"/> is false, then you will most likely want to
			/// enable the cross tics.
			/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
			/// </remarks>
			/// <value>true to show the major cross tic marks, false otherwise</value>
			public static bool IsCrossTic = false;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> major inside 
			/// "cross" tic marks (<see cref="Axis.IsInsideCrossTic"/> property).
			/// </summary>
			/// <remarks>
			/// The "cross" tics are a special, additional set of tic marks that
			/// always appear on the actual axis, even if it has been shifted due
			/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
			/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
			/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
			/// they will exactly overlay the "normal" and "inside" tics.  If
			/// <see cref="CrossAuto"/> is false, then you will most likely want to
			/// enable the cross tics.
			/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
			/// </remarks>
			/// <value>true to show the major cross tic marks, false otherwise</value>
			public static bool IsInsideCrossTic = false;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> minor outside 
			/// "cross" tic marks (<see cref="Axis.IsMinorCrossTic"/> property).
			/// </summary>
			/// <remarks>
			/// The "cross" tics are a special, additional set of tic marks that
			/// always appear on the actual axis, even if it has been shifted due
			/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
			/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
			/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
			/// they will exactly overlay the "normal" and "inside" tics.  If
			/// <see cref="CrossAuto"/> is false, then you will most likely want to
			/// enable the cross tics.
			/// The minor tic spacing is controlled by <see cref="Axis.MinorStep"/>.
			/// </remarks>
			/// <value>true to show the major cross tic marks, false otherwise</value>
			public static bool IsMinorCrossTic = false;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> minor inside 
			/// "cross" tic marks (<see cref="Axis.IsMinorInsideCrossTic"/> property).
			/// </summary>
			/// <remarks>
			/// The "cross" tics are a special, additional set of tic marks that
			/// always appear on the actual axis, even if it has been shifted due
			/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
			/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
			/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
			/// they will exactly overlay the "normal" and "inside" tics.  If
			/// <see cref="CrossAuto"/> is false, then you will most likely want to
			/// enable the cross tics.
			/// The major tic spacing is controlled by <see cref="Axis.MinorStep"/>.
			/// </remarks>
			/// <value>true to show the major cross tic marks, false otherwise</value>
			public static bool IsMinorInsideCrossTic = false;

			/// <summary>
			/// The default setting for the <see cref="Axis"/> scale axis type
			/// (<see cref="Axis.Type"/> property).  This value is set as per
			/// the <see cref="AxisType"/> enumeration
			/// </summary>
			public static AxisType Type = AxisType.Linear;

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
			this.scale = new LinearScale( this );

			this.cross = 0.0;

			this.crossAuto = true;

			this.ticSize = Default.TicSize;
			this.minorTicSize = Default.MinorTicSize;
			this.gridDashOn = Default.GridDashOn;
			this.gridDashOff = Default.GridDashOff;
			this.gridPenWidth = Default.GridPenWidth;
			this.minorGridDashOn = Default.MinorGridDashOn;
			this.minorGridDashOff = Default.MinorGridDashOff;
			this.minorGridPenWidth = Default.MinorGridPenWidth;

			this.axisGap = Default.AxisGap;

			this.minSpace = Default.MinSpace;
			this.isVisible = true;
			this.isScaleVisible = Default.IsScaleVisible;
			this.isShowTitle = Default.IsShowTitle;
			this.isShowGrid = Default.IsShowGrid;
			this.isShowMinorGrid = Default.IsShowMinorGrid;
			this.isOmitMag = false;

			this.isTic = Default.IsTic;
			this.isInsideTic = Default.IsInsideTic;
			this.isOppositeTic = Default.IsOppositeTic;
			this.isCrossTic = Default.IsCrossTic;
			this.isInsideCrossTic = Default.IsInsideCrossTic;
			this.isMinorTic = Default.IsMinorTic;
			this.isMinorInsideTic = Default.IsMinorInsideTic;
			this.isMinorOppositeTic = Default.IsMinorOppositeTic;
			this.isMinorCrossTic = Default.IsMinorCrossTic;
			this.isMinorInsideCrossTic = Default.IsMinorInsideCrossTic;

			this.isTicsBetweenLabels = false;
			this.isAxisSegmentVisible = Default.IsAxisSegmentVisible;
			this.isScaleLabelsInside = Default.IsScaleLabelsInside;
			this.isSkipFirstLabel = false;
			this.isSkipLastLabel = false;
			this.isSkipCrossLabel = false;
			this.isTitleAtCross = true;

			this.title = "";
			this.TextLabels = null;
			this.scaleAlign = Default.ScaleAlign;

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
		/// Constructor for <see cref="Axis"/> that sets all axis properties
		/// to default values as defined in the <see cref="Default"/> class,
		/// except for the <see cref="Title"/>.
		/// </summary>
		/// <param name="title">A string containing the axis title</param>
		public Axis( string title )
			: this()
		{
			this.title = title;
		}

		/// <summary>
		/// The Copy Constructor.
		/// </summary>
		/// <param name="rhs">The Axis object from which to copy</param>
		public Axis( Axis rhs )
		{
			this.scale = (Scale)( rhs.scale as ICloneable ).Clone();
			cross = rhs.cross;

			crossAuto = rhs.crossAuto;
			//numDecAuto = rhs.NumDecAuto;

			//numDec = rhs.numDec;
			isVisible = rhs.IsVisible;
			isScaleVisible = rhs.isScaleVisible;
			isShowTitle = rhs.IsShowTitle;
			isShowGrid = rhs.IsShowGrid;
			isShowMinorGrid = rhs.IsShowMinorGrid;
			isZeroLine = rhs.IsZeroLine;

			isTic = rhs.IsTic;
			isInsideTic = rhs.IsInsideTic;
			isOppositeTic = rhs.IsOppositeTic;
			isCrossTic = rhs.IsCrossTic;
			isInsideCrossTic = rhs.IsInsideCrossTic;

			isMinorTic = rhs.IsMinorTic;
			isMinorInsideTic = rhs.IsMinorInsideTic;
			isMinorOppositeTic = rhs.IsMinorOppositeTic;
			isMinorCrossTic = rhs.IsMinorCrossTic;
			isMinorInsideCrossTic = rhs.IsMinorInsideCrossTic;

			isTicsBetweenLabels = rhs.IsTicsBetweenLabels;
			isAxisSegmentVisible = rhs.isAxisSegmentVisible;
			isScaleLabelsInside = rhs.isScaleLabelsInside;
			isSkipFirstLabel = rhs.isSkipFirstLabel;
			isSkipLastLabel = rhs.isSkipLastLabel;
			isSkipCrossLabel = rhs.isSkipCrossLabel;
			isTitleAtCross = rhs.isTitleAtCross;

			isOmitMag = rhs.IsOmitMag;
			title = rhs.Title;

			if ( rhs.TextLabels != null )
				TextLabels = (string[])rhs.TextLabels.Clone();
			else
				TextLabels = null;

			scaleAlign = rhs.scaleAlign;

			titleFontSpec = (FontSpec)rhs.TitleFontSpec.Clone();
			scaleFontSpec = (FontSpec)rhs.ScaleFontSpec.Clone();

			ticPenWidth = rhs.TicPenWidth;
			ticSize = rhs.TicSize;
			minorTicSize = rhs.MinorTicSize;
			gridDashOn = rhs.GridDashOn;
			gridDashOff = rhs.GridDashOff;
			gridPenWidth = rhs.GridPenWidth;
			minorGridDashOn = rhs.MinorGridDashOn;
			minorGridDashOff = rhs.MinorGridDashOff;
			minorGridPenWidth = rhs.MinorGridPenWidth;

			axisGap = rhs.axisGap;

			minSpace = rhs.MinSpace;

			color = rhs.Color;
			gridColor = rhs.GridColor;
			minorGridColor = rhs.MinorGridColor;
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of Clone.
		/// </summary>
		/// <remarks>
		/// Note that this method must be called with an explicit cast to ICloneable, and
		/// that it is inherently virtual.  For example:
		/// <code>
		/// ParentClass foo = new ChildClass();
		/// ChildClass bar = (ChildClass) ((ICloneable)foo).Clone();
		/// </code>
		/// Assume that ChildClass is inherited from ParentClass.  Even though foo is declared with
		/// ParentClass, it is actually an instance of ChildClass.  Calling the ICloneable implementation
		/// of Clone() on foo actually calls ChildClass.Clone() as if it were a virtual function.
		/// </remarks>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			throw new NotImplementedException( "Can't clone an abstract base type -- child types must implement ICloneable" );
			//return new PaneBase( this );
		}


		#endregion

		#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		// Schema was changed to 2 when IsScaleVisible was added
		// Schema was changed to 3 when IsAxisSegmentVisible was added
		// Schema was changed to 4 when IsScaleLabelsInside, isSkipFirstLabel, isSkipLastLabel were added
		// Schema was changed to 5 with IsCrossTic, IsInsideCrossTic, IsMinorCrossTic, IsMinorInsideCrossTic
		// Schema was changed to 6 with AxisGap
		// Schema was changed to 7 with Exponent
		// Schema was changed to 8 when "Scale" class was added
		// Schema was changed to 9 when scale was actually output properly
		// Schema was changed to 10 when isSkipCrossLabel, isTitleAtCross were added
		public const int schema = 10;

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

			cross = info.GetDouble( "cross" );
			crossAuto = info.GetBoolean( "crossAuto" );

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

			isOmitMag = info.GetBoolean( "isOmitMag" );

			title = info.GetString( "title" );

			scaleAlign = (AlignP)info.GetValue( "scaleAlign", typeof( AlignP ) );

			titleFontSpec = (FontSpec)info.GetValue( "titleFontSpec", typeof( FontSpec ) );
			scaleFontSpec = (FontSpec)info.GetValue( "scaleFontSpec", typeof( FontSpec ) );

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

			color = (Color)info.GetValue( "color", typeof( Color ) );
			gridColor = (Color)info.GetValue( "gridColor", typeof( Color ) );
			minorGridColor = (Color)info.GetValue( "minorGridColor", typeof( Color ) );

			if ( schema >= 2 )
				isScaleVisible = info.GetBoolean( "isScaleVisible" );

			if ( schema >= 3 )
				isAxisSegmentVisible = info.GetBoolean( "isAxisSegmentVisible" );

			if ( schema >= 4 )
			{
				isScaleLabelsInside = info.GetBoolean( "isScaleLabelsInside" );
				isSkipFirstLabel = info.GetBoolean( "isSkipFirstLabel" );
				isSkipLastLabel = info.GetBoolean( "isSkipLastLabel" );
			}

			if ( schema >= 5 )
			{
				isCrossTic = info.GetBoolean( "isCrossTic" );
				isInsideCrossTic = info.GetBoolean( "isInsideCrossTic" );
				isMinorCrossTic = info.GetBoolean( "isMinorCrossTic" );
				isMinorInsideCrossTic = info.GetBoolean( "isMinorInsideCrossTic" );
			}

			if ( schema >= 6 )
				axisGap = info.GetSingle( "axisGap" );

			if ( schema >= 9 )
			{
				scale = (Scale)info.GetValue( "scale", typeof( Scale ) );
				scale.parentAxis = this;
			}

			if ( schema >= 10 )
			{
				isSkipCrossLabel = info.GetBoolean( "isSkipCrossLabel" );
				isTitleAtCross = info.GetBoolean( "isTitleAtCross" );
			}
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute( SecurityAction.Demand, SerializationFormatter = true )]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );

			info.AddValue( "cross", cross );
			info.AddValue( "crossAuto", crossAuto );

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

			info.AddValue( "isOmitMag", isOmitMag );

			info.AddValue( "title", title );
			info.AddValue( "scaleAlign", scaleAlign );
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

			// New for Schema = 2
			info.AddValue( "isScaleVisible", isScaleVisible );

			// New for schema = 3
			info.AddValue( "isAxisSegmentVisible", isAxisSegmentVisible );

			// New for schema = 4
			info.AddValue( "isScaleLabelsInside", isScaleLabelsInside );
			info.AddValue( "isSkipFirstLabel", isSkipFirstLabel );
			info.AddValue( "isSkipLastLabel", isSkipLastLabel );

			// New for schema = 5
			info.AddValue( "isCrossTic", isCrossTic );
			info.AddValue( "isInsideCrossTic", isInsideCrossTic );
			info.AddValue( "isMinorCrossTic", isMinorCrossTic );
			info.AddValue( "isMinorInsideCrossTic", isMinorInsideCrossTic );

			// new for schema = 6
			info.AddValue( "axisGap", axisGap );

			// new for schema = 9
			info.AddValue( "scale", this.scale );

			// new for schema = 10
			info.AddValue( "isSkipCrossLabel", isSkipCrossLabel );
			info.AddValue( "isTitleAtCross", isTitleAtCross );
		}
		#endregion

		#region Scale Properties

		/// <summary>
		/// Gets the <see cref="Scale" /> instance associated with this <see cref="Axis" />.
		/// </summary>
		public Scale Scale
		{
			get { return this.scale; }
		}

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
			get { return scale.Min; }
			set { scale.Min = value; scale.MinAuto = false; }
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
			get { return scale.Max; }
			set { scale.Max = value; scale.MaxAuto = false; }
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
		/// <seealso cref="ZedGraph.Scale.Default.TargetXSteps"/>
		/// <seealso cref="ZedGraph.Scale.Default.TargetYSteps"/>
		/// <seealso cref="ZedGraph.Scale.Default.ZeroLever"/>
		/// <seealso cref="ZedGraph.Scale.Default.MaxTextLabels"/>
		public double Step
		{
			get { return scale.Step; }
			set { scale.Step = value; scale.StepAuto = false; }
		}
		/// <summary>
		/// Gets or sets the scale exponent value.  This only applies to <see cref="AxisType.Exponent" />. 
		/// </summary>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="StepAuto"/>
		/// <seealso cref="ZedGraph.Scale.Default.TargetXSteps"/>
		/// <seealso cref="ZedGraph.Scale.Default.TargetYSteps"/>
		/// <seealso cref="ZedGraph.Scale.Default.ZeroLever"/>
		/// <seealso cref="ZedGraph.Scale.Default.MaxTextLabels"/>
		public double Exponent
		{
			get { return scale.Exponent; }
			set { scale.Exponent = value; }
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
			get { return scale.MajorUnit; }
			set { scale.MajorUnit = value; }
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
			get { return scale.MinorUnit; }
			set { scale.MinorUnit = value; }
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
			get { return scale.MinorStep; }
			set { scale.MinorStep = value; scale.MinorStepAuto = false; }
		}
		/// <summary>
		/// Gets or sets the scale value at which this axis should cross the "other" axis.
		/// </summary>
		/// <remarks>This property allows the axis to be shifted away from its default location.
		/// For example, for a graph with an X range from -100 to +100, the Y Axis can be located
		/// at the X=0 value rather than the left edge of the axisRect.  This value can be set
		/// automatically based on the state of <see cref="CrossAuto"/>.  If
		/// this value is set manually, then <see cref="CrossAuto"/> will
		/// also be set to false.  The "other" axis is the axis the handles the second dimension
		/// for the graph.  For the XAxis, the "other" axis is the YAxis.  For the YAxis or
		/// Y2Axis, the "other" axis is the XAxis.
		/// </remarks>
		/// <value> The value is defined in user scale units </value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="CrossAuto"/>
		public double Cross
		{
			get { return cross; }
			set { cross = value; this.crossAuto = false; }
		}

		/// <summary>
		/// Gets or sets the scale value at which the first major tic label will appear.
		/// </summary>
		/// <remarks>This property allows the scale labels to start at an irregular value.
		/// For example, on a scale range with <see cref="Min"/> = 0, <see cref="Max"/> = 1000,
		/// and <see cref="Step"/> = 200, a <see cref="BaseTic"/> value of 50 would cause
		/// the scale labels to appear at values 50, 250, 450, 650, and 850.  Note that the
		/// default value for this property is <see cref="PointPair.Missing"/>, which means the
		/// value is not used.  Setting this property to any value other than
		/// <see cref="PointPair.Missing"/> will activate the effect.  The value specified must
		/// coincide with the first major tic.  That is, if <see cref="BaseTic"/> were set to
		/// 650 in the example above, then the major tics would only occur at 650 and 850.  This
		/// setting may affect the minor tics, since the minor tics are always referenced to the
		/// <see cref="BaseTic"/>.  That is, in the example above, if the <see cref="MinorStep"/>
		/// were set to 30 (making it a non-multiple of the major step), then the minor tics would
		/// occur at 20, 50 (so it lines up with the BaseTic), 80, 110, 140, etc.
		/// </remarks>
		/// <value> The value is defined in user scale units </value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="MinorStep"/>
		/// <seealso cref="Cross"/>
		public double BaseTic
		{
			get { return this.scale.BaseTic; }
			set { this.scale.BaseTic = value; }
		}

		//abstract internal bool IsCrossed( GraphPane pane );

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
			get { return scale.MinAuto; }
			set { scale.MinAuto = value; }
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
			get { return scale.MaxAuto; }
			set { scale.MaxAuto = value; }
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
			get { return scale.StepAuto; }
			set { scale.StepAuto = value; }
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
			get { return scale.MinorStepAuto; }
			set { scale.MinorStepAuto = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not the <see cref="Cross"/> value
		/// is set automatically.
		/// </summary>
		/// <value>Set to true to have ZedGraph put the axis in the default location, or false
		/// to specify the axis location manually with a <see cref="Cross"/> value.</value>
		/// <seealso cref="Min"/>
		/// <seealso cref="Max"/>
		/// <seealso cref="Step"/>
		/// <seealso cref="Cross"/>
		public bool CrossAuto
		{
			get { return crossAuto; }
			set { crossAuto = value; }
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
		/// <seealso cref="ZedGraph.Scale.Default.MinGrace"/>
		/// <seealso cref="MaxGrace"/>
		public double MinGrace
		{
			get { return scale.MinGrace; }
			set { scale.MinGrace = value; }
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
		/// <seealso cref="ZedGraph.Scale.Default.MaxGrace"/>
		/// <seealso cref="MinGrace"/>
		public double MaxGrace
		{
			get { return scale.MaxGrace; }
			set { scale.MaxGrace = value; }
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
			return (float)( this.ticSize * scaleFactor );
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
			return (float)( this.minorTicSize * scaleFactor );
		}

		/// <summary>
		/// This is convenience property sets the status of all the different
		/// tic properties to the same value.  true to activate all tics, false to
		/// clear all tics.
		/// </summary>
		/// <remarks>
		/// This setting does not persist.  That is, you can clear all the tics with
		/// <see cref="IsAllTics" /> = false, then activate them individually (example:
		/// <see cref="IsMinorTic" /> = true).
		/// </remarks>
		/// <seealso cref="IsTic"/>
		/// <seealso cref="IsMinorTic"/>
		/// <seealso cref="IsInsideTic"/>
		/// <seealso cref="IsOppositeTic"/>
		/// <seealso cref="IsMinorInsideTic"/>
		/// <seealso cref="IsMinorOppositeTic"/>
		/// <seealso cref="IsCrossTic"/>
		/// <seealso cref="IsInsideCrossTic"/>
		/// <seealso cref="IsMinorInsideCrossTic"/>
		/// <seealso cref="IsMinorCrossTic"/>
		public bool IsAllTics
		{
			set
			{
				this.isTic = value;
				this.isMinorTic = value;
				this.isInsideTic = value;
				this.isOppositeTic = value;
				this.isMinorInsideTic = value;
				this.isMinorOppositeTic = value;

				this.isCrossTic = value;
				this.isInsideCrossTic = value;
				this.isMinorInsideCrossTic = value;
				this.isMinorCrossTic = value;

			}
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
		/// <seealso cref="IsCrossTic"/>
		/// <seealso cref="IsInsideCrossTic"/>
		/// <seealso cref="IsMinorInsideCrossTic"/>
		/// <seealso cref="IsMinorCrossTic"/>
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
		/// Gets or sets the display mode for the <see cref="Axis"/> major outside 
		/// "cross" tic marks.
		/// </summary>
		/// <remarks>
		/// The "cross" tics are a special, additional set of tic marks that
		/// always appear on the actual axis, even if it has been shifted due
		/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
		/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
		/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
		/// they will exactly overlay the "normal" and "inside" tics.  If
		/// <see cref="CrossAuto"/> is false, then you will most likely want to
		/// enable the cross tics.
		/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
		/// </remarks>
		/// <value>true to show the major cross tic marks, false otherwise</value>
		public bool IsCrossTic
		{
			get { return isCrossTic; }
			set { isCrossTic = value; }
		}
		/// <summary>
		/// Gets or sets the display mode for the <see cref="Axis"/> major inside 
		/// "cross" tic marks.
		/// </summary>
		/// <remarks>
		/// The "cross" tics are a special, additional set of tic marks that
		/// always appear on the actual axis, even if it has been shifted due
		/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
		/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
		/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
		/// they will exactly overlay the "normal" and "inside" tics.  If
		/// <see cref="CrossAuto"/> is false, then you will most likely want to
		/// enable the cross tics.
		/// The major tic spacing is controlled by <see cref="Axis.Step"/>.
		/// </remarks>
		/// <value>true to show the major cross tic marks, false otherwise</value>
		public bool IsInsideCrossTic
		{
			get { return isInsideCrossTic; }
			set { isInsideCrossTic = value; }
		}
		/// <summary>
		/// Gets or sets the display mode for the <see cref="Axis"/> minor outside 
		/// "cross" tic marks.
		/// </summary>
		/// <remarks>
		/// The "cross" tics are a special, additional set of tic marks that
		/// always appear on the actual axis, even if it has been shifted due
		/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
		/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
		/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
		/// they will exactly overlay the "normal" and "inside" tics.  If
		/// <see cref="CrossAuto"/> is false, then you will most likely want to
		/// enable the cross tics.
		/// The minor tic spacing is controlled by <see cref="Axis.MinorStep"/>.
		/// </remarks>
		/// <value>true to show the major cross tic marks, false otherwise</value>
		public bool IsMinorCrossTic
		{
			get { return isMinorCrossTic; }
			set { isMinorCrossTic = value; }
		}
		/// <summary>
		/// Gets or sets the display mode for the <see cref="Axis"/> minor inside 
		/// "cross" tic marks.
		/// </summary>
		/// <remarks>
		/// The "cross" tics are a special, additional set of tic marks that
		/// always appear on the actual axis, even if it has been shifted due
		/// to the <see cref="Axis.Cross" /> setting.  The other tic marks are always
		/// fixed to the edges of the <see cref="GraphPane.AxisRect"/>.  The cross tics
		/// are normally not displayed, since, if <see cref="Axis.CrossAuto" /> is true,
		/// they will exactly overlay the "normal" and "inside" tics.  If
		/// <see cref="CrossAuto"/> is false, then you will most likely want to
		/// enable the cross tics.
		/// The major tic spacing is controlled by <see cref="Axis.MinorStep"/>.
		/// </remarks>
		/// <value>true to show the major cross tic marks, false otherwise</value>
		public bool IsMinorInsideCrossTic
		{
			get { return isMinorInsideCrossTic; }
			set { isMinorInsideCrossTic = value; }
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
		/// The pen width used for drawing the grid lines.
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
		/// <seealso cref="Axis.IsScaleVisible"/>.
		/// <seealso cref="XAxis.Default.IsVisible"/>.
		/// <seealso cref="YAxis.Default.IsVisible"/>.
		/// <seealso cref="Y2Axis.Default.IsVisible"/>.
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}

		/// <summary>
		/// Gets or sets a property that determines whether or not the scale values will be shown.
		/// </summary>
		/// <value>true to show the scale values, false otherwise</value>
		/// <seealso cref="Axis.IsVisible"/>.
		public bool IsScaleVisible
		{
			get { return isScaleVisible; }
			set { isScaleVisible = value; }
		}

		/// <summary>
		/// Gets or sets a property that determines whether or not the axis segment (the line that
		/// represents the axis itself) is drawn.
		/// </summary>
		/// <remarks>
		/// Under normal circumstances, this value won't affect the appearance of the display because
		/// the Axis segment is overlain by the Axis border (see <see cref="GraphPane.AxisBorder"/>).
		/// However, when the border is not visible, or when <see cref="Axis.CrossAuto"/> is set to
		/// false, this value will make a difference.
		/// </remarks>
		public bool IsAxisSegmentVisible
		{
			get { return isAxisSegmentVisible; }
			set { isAxisSegmentVisible = value; }
		}

		/// <summary>
		/// Determines if the scale values are reversed for this <see cref="Axis"/>
		/// </summary>
		/// <value>true for the X values to decrease to the right or the Y values to
		/// decrease upwards, false otherwise</value>
		/// <seealso cref="ZedGraph.Scale.Default.IsReverse"/>.
		public bool IsReverse
		{
			get { return scale.IsReverse; }
			set { scale.IsReverse = value; }
		}
		/// <summary>
		/// Gets a property that indicates if this <see cref="Axis"/> is logarithmic (base 10).
		/// </summary>
		/// <remarks>
		/// To make this property true, set <see cref="Type"/> to <see cref="AxisType.Log"/>.
		/// </remarks>
		/// <value>true for a logarithmic axis, false for a linear, date, or text axis</value>
		/// <seealso cref="Type"/>
		/// <seealso cref="AxisType"/>
		public bool IsLog
		{
			get { return scale.IsLog; }
		}
		/// <summary>
		/// Gets a property that indicates if this <see cref="Axis"/> is exponential.
		/// </summary>
		/// <remarks>
		/// To make this property true, set <see cref="Type"/> to <see cref="AxisType.Exponent"/>.
		/// </remarks>
		/// <value>true for an exponential axis, false for a linear, date, log, or text axis</value>
		/// <seealso cref="Type"/>
		/// <seealso cref="AxisType"/>
		public bool IsExponent
		{
			get { return scale.IsExponent; }
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
			get { return scale.IsDate; }
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
			get { return scale.IsText; }
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
			get { return scale.IsOrdinal; }
		}
		/// <summary>
		/// Gets or sets the <see cref="AxisType"/> for this <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// The type can be either <see cref="AxisType.Linear"/>,
		/// <see cref="AxisType.Log"/>, <see cref="AxisType.Date"/>,
		/// or <see cref="AxisType.Text"/>.
		/// </remarks>
		/// <seealso cref="IsLog"/>
		/// <seealso cref="IsText"/>
		/// <seealso cref="IsOrdinal"/>
		/// <seealso cref="IsDate"/>
		/// <seealso cref="IsReverse"/>
		public AxisType Type
		{
			get { return scale.Type; }
			set { this.scale = Scale.MakeNewScale( this.scale, value ); }
		}

		/// <summary>
		/// Gets a value that indicates if this <see cref="Axis" /> is of any of the
		/// ordinal types in the <see cref="AxisType" /> enumeration.
		/// </summary>
		/// <seealso cref="Type" />
		public bool IsAnyOrdinal { get { return this.scale.IsAnyOrdinal; } }

		#endregion

		#region Label Properties
		/// <summary>
		/// Gets or sets the property that controls whether or not the magnitude factor (power of 10) for
		/// this scale will be included in the label.
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
		// /// <seealso cref="NumDec"/>
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
			get { return this.scale.IsUseTenPower; }
			set { this.scale.IsUseTenPower = value; }
		}

		/// <summary>
		/// Gets or sets a <see cref="bool"/> value that determines if ZedGraph will check to
		/// see if the <see cref="Axis"/> scale labels are close enough to overlap.  If so,
		/// ZedGraph will adjust the step size to prevent overlap.
		/// </summary>
		/// <remarks>
		/// The process of checking for overlap is done during the <see cref="GraphPane.AxisChange"/>
		/// method call, and affects the selection of the major step size (<see cref="Step"/>).
		/// </remarks>
		/// <value> boolean value; true to check for overlap, false otherwise</value>
		public bool IsPreventLabelOverlap
		{
			get { return this.scale.IsPreventLabelOverlap; }
			set { this.scale.IsPreventLabelOverlap = value; }
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
			get { return scale.TextLabels; }
			set { scale.TextLabels = value; }
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
		// /// <seealso cref="NumDec"/>
		public bool ScaleFormatAuto
		{
			get { return this.scale.ScaleFormatAuto; }
			set { this.scale.ScaleFormatAuto = value; }
		}
		/// <summary>
		/// The format of the <see cref="Axis"/> tic labels.
		/// </summary>
		/// <remarks>
		/// This property is only used if the <see cref="Type"/> is set to <see cref="AxisType.Date"/>.
		/// This property may be set automatically by ZedGraph, depending on the state of
		/// <see cref="ScaleFormatAuto"/>.
		/// </remarks>
		/// <value>This format string is as defined for the <see cref="XDate.ToString()"/> function,
		/// which uses the <see cref="System.Globalization.DateTimeFormatInfo" /> class for format strings.</value>
		/// <seealso cref="ScaleMag"/>
		/// <seealso cref="ScaleFormatAuto"/>
		/// <seealso cref="ScaleFontSpec"/>
		// /// <seealso cref="NumDec"/>
		public string ScaleFormat
		{
			get { return this.scale.ScaleFormat; }
			set { this.scale.ScaleFormat = value; this.scale.ScaleFormatAuto = false; }
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
		// /// <seealso cref="NumDec"/>
		public int ScaleMag
		{
			get { return this.scale.ScaleMag; }
			set { this.scale.ScaleMag = value; this.scale.ScaleMagAuto = false; }
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
			get { return this.scale.ScaleMagAuto; }
			set { this.scale.ScaleMagAuto = value; }
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

		/// <summary>
		/// Gets or sets a value that causes the axis scale labels and title to appear on the
		/// opposite side of the axis.
		/// </summary>
		/// <remarks>
		/// For example, setting this flag to true for the <see cref="YAxis"/> will shift the
		/// axis labels and title to the right side of the <see cref="YAxis"/> instead of the
		/// normal left-side location.  Set this property to true for the <see cref="XAxis"/>,
		/// and set the <see cref="Cross"/> property for the <see cref="XAxis"/> to an arbitrarily
		/// large value (assuming <see cref="IsReverse"/> is false for the <see cref="YAxis"/>) in
		/// order to have the <see cref="XAxis"/> appear at the top of the <see cref="GraphPane.AxisRect"/>.
		/// </remarks>
		/// <seealso cref="IsReverse"/>
		/// <seealso cref="Cross"/>
		public bool IsScaleLabelsInside
		{
			get { return isScaleLabelsInside; }
			set { isScaleLabelsInside = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether the Axis title is located at the <see cref="Cross" />
		/// value or at the normal position (outside the <see cref="GraphPane.AxisRect" />).
		/// </summary>
		/// <remarks>
		/// This value only applies if <see cref="IsCrossAuto" /> is false.
		/// </remarks>
		public bool IsTitleAtCross
		{
			get { return isTitleAtCross; }
			set { isTitleAtCross = value; }
		}

		/// <summary>
		/// Gets or sets a value that causes the first scale label for this <see cref="Axis"/> to be
		/// hidden.
		/// </summary>
		/// <remarks>
		/// Often, for axis that have an active <see cref="Cross"/> setting (e.g., <see cref="CrossAuto"/>
		/// is false), the first and/or last scale label are overlapped by opposing axes.  Use this
		/// property to hide the first scale label to avoid the overlap.  Note that setting this value
		/// to true will hide any scale label that appears within <see cref="Default.EdgeTolerance"/> of the
		/// beginning of the <see cref="Axis"/>.
		/// </remarks>
		public bool IsSkipFirstLabel
		{
			get { return isSkipFirstLabel; }
			set { isSkipFirstLabel = value; }
		}

		/// <summary>
		/// Gets or sets a value that causes the last scale label for this <see cref="Axis"/> to be
		/// hidden.
		/// </summary>
		/// <remarks>
		/// Often, for axis that have an active <see cref="Cross"/> setting (e.g., <see cref="CrossAuto"/>
		/// is false), the first and/or last scale label are overlapped by opposing axes.  Use this
		/// property to hide the last scale label to avoid the overlap.  Note that setting this value
		/// to true will hide any scale label that appears within <see cref="Default.EdgeTolerance"/> of the
		/// end of the <see cref="Axis"/>.
		/// </remarks>
		public bool IsSkipLastLabel
		{
			get { return isSkipLastLabel; }
			set { isSkipLastLabel = value; }
		}

		/// <summary>
		/// Gets or sets a value that causes the scale label that is located at the <see cref="Cross" />
		/// value for this <see cref="Axis"/> to be hidden.
		/// </summary>
		/// <remarks>
		/// For axes that have an active <see cref="Cross"/> setting (e.g., <see cref="CrossAuto"/>
		/// is false), the scale label at the <see cref="Cross" /> value is overlapped by opposing axes.
		/// Use this property to hide the scale label to avoid the overlap.
		/// </remarks>
		public bool IsSkipCrossLabel
		{
			get { return isSkipCrossLabel; }
			set { isSkipCrossLabel = value; }
		}

		/// <summary>
		/// The size of the gap between multiple axes (see <see cref="GraphPane.YAxisList" /> and
		/// <see cref="GraphPane.Y2AxisList" />).
		/// </summary>
		/// <remarks>
		/// This size will be scaled
		/// according to the <see cref="PaneBase.CalcScaleFactor"/> for the
		/// <see cref="GraphPane"/>
		/// </remarks>
		/// <value>The axis gap is measured in points (1/72 inch)</value>
		/// <seealso cref="Default.AxisGap"/>.
		public float AxisGap
		{
			get { return axisGap; }
			set { axisGap = value; }
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
		// /// <seealso cref="NumDecAuto"/>
		public void ResetAutoScale( GraphPane pane, Graphics g )
		{
			this.scale.MinAuto = true;
			this.scale.MaxAuto = true;
			this.scale.StepAuto = true;
			this.scale.MinorStepAuto = true;
			this.crossAuto = true;
			this.scale.ScaleMagAuto = true;
			//this.numDecAuto = true;
			this.scale.ScaleFormatAuto = true;
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
		/// <param name="shiftPos">
		/// The number of pixels to shift to account for non-primary axis position (e.g.,
		/// the second, third, fourth, etc. <see cref="YAxis" /> or <see cref="Y2Axis" />.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, float scaleFactor, float shiftPos )
		{
			Matrix saveMatrix = g.Transform;

			this.scale.SetupScaleData( pane, this );

			SetTransformMatrix( g, pane, scaleFactor );

			DrawScale( g, pane, scaleFactor, shiftPos );

			//DrawTitle( g, pane, scaleFactor );

			g.Transform = saveMatrix;
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
			float fixedSpace;
			float space = this.CalcSpace( g, pane, 1.0F, out fixedSpace ) * bufferFraction;
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
		/// Calculate the "shift" size, in pixels, in order to shift the axis from its default
		/// location to the value specified by <see cref="Cross"/>.
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <returns>The shift amount measured in pixels</returns>
		abstract internal float CalcCrossShift( GraphPane pane );

		abstract internal Axis GetCrossAxis( GraphPane pane );


		//abstract internal float CalcCrossFraction( GraphPane pane );

		internal double EffectiveCrossValue( GraphPane pane )
		{
			Axis crossAxis = GetCrossAxis( pane );
			double min = crossAxis.Min;
			double max = crossAxis.Max;

			if ( crossAuto )
			{
				if ( crossAxis.IsReverse == ( this is Y2Axis ) )
					return max;
				else
					return min;
			}
			else if ( cross < min )
				return min;
			else if ( cross > max )
				return max;
			else
				return cross;
		}

		internal bool IsCrossShifted( GraphPane pane )
		{
			if ( this.crossAuto )
				return false;
			else
			{
				Axis crossAxis = GetCrossAxis( pane );
				if ( ( ( this is XAxis || this is YAxis ) && !crossAxis.scale.IsReverse ) ||
					( this is Y2Axis && crossAxis.scale.IsReverse ) )
				{
					if ( this.cross <= crossAxis.Min )
						return false;
				}
				else
				{
					if ( this.cross >= crossAxis.Max )
						return false;
				}
			}

			return true;
		}

		internal float CalcCrossFraction( GraphPane pane )
		{
			//if ( this.crossAuto )
			//	return ( isScaleLabelsInside && IsPrimary( pane ) ) ? 1.0f : 0.0f;

			// if this axis is not shifted due to the Cross value
			if ( !this.IsCrossShifted( pane ) )
			{
				// if it's the primary axis and the scale labels are on the inside, then we
				// don't need to save any room for the axis labels (they will be inside the axis rect)
				if ( IsPrimary( pane ) && isScaleLabelsInside )
					return 1.0f;
				// otherwise, it's a secondary (outboard) axis and we always save room for the axis and labels.
				else
					return 0.0f;
			}

			double effCross = EffectiveCrossValue( pane );
			Axis crossAxis = GetCrossAxis( pane );
			if ( crossAxis.IsLog )
				effCross = Scale.SafeLog( effCross );

			double max = crossAxis.scale.maxScale;
			double min = crossAxis.scale.minScale;
			float frac;

			if ( ( ( this is XAxis || this is YAxis ) && isScaleLabelsInside == crossAxis.scale.IsReverse ) ||
				 ( this is Y2Axis && isScaleLabelsInside != crossAxis.scale.IsReverse ) )
				frac = (float)( ( effCross - min ) / ( max - min ) );
			else
				frac = (float)( ( max - effCross ) / ( max - min ) );

			if ( frac < 0.0f )
				frac = 0.0f;
			if ( frac > 1.0f )
				frac = 1.0f;

			return frac;
		}

		/// <summary>
		/// Calculate the space required (pixels) for this <see cref="Axis"/> object.
		/// </summary>
		/// <remarks>
		/// This is the total space (vertical space for the X axis, horizontal space for
		/// the Y axes) required to contain the axis.  If <see cref="Cross" /> is zero, then
		/// this space will be the space required between the <see cref="GraphPane.AxisRect" /> and
		/// the <see cref="PaneBase.PaneRect" />.  This method sets the internal values of
		/// <see cref="tmpMinSpace" /> and <see cref="tmpSpace" /> for use by the
		/// <see cref="GraphPane.CalcAxisRect(Graphics)" /> method.
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
		/// <returns>Returns the space, in pixels, required for this axis (between the
		/// paneRect and axisRect)</returns>
		public float CalcSpace( Graphics g, GraphPane pane, float scaleFactor, out float fixedSpace )
		{
			//fixedSpace = 0;

			//Typical character height for the scale font
			float charHeight = this.ScaleFontSpec.GetHeight( scaleFactor );
			// Scaled size (pixels) of a tic
			float ticSize = this.ScaledTic( scaleFactor );
			// Scaled size (pixels) of the axis gap
			float gap = this.axisGap * scaleFactor;

			// The minimum amount of space to reserve for the NORMAL position of the axis.  This would
			// be the left side of the axis rect for the Y axis, the right side for the Y2 axis, etc.
			// This amount of space is based on the need to reserve space for tics, etc., even if the
			// Axis.Cross property causes the axis to be in a different location.
			fixedSpace = 0;

			// The actual space needed for this axis (ignoring the setting of Axis.Cross)
			tmpSpace = 0;

			// Account for the Axis
			if ( this.isVisible )
			{
				//bool hasTic = ( this.isScaleLabelsInside ?
				//		(this.isInsideTic || this.isInsideCrossTic ||
				//				this.isMinorInsideTic || this.isMinorInsideCrossTic) :
				//		(this.isTic || this.isCrossTic || this.isMinorTic || this.isMinorCrossTic) );

				bool hasTic = this.isTic || this.isCrossTic || this.isMinorTic || this.isMinorCrossTic;

				// account for the tic space.  Leave the tic space for any type of outside tic (Outside Tic Space)
				if ( hasTic )
					tmpSpace += ticSize;

				// if this is not the primary axis
				if ( !IsPrimary( pane ) )
				{
					// always leave an extra tic space for the space between the multi-axes (Axis Gap)
					tmpSpace += gap;

					// if it has inside tics, leave another tic space (Inside Tic Space)
					if ( this.isInsideTic || this.isInsideCrossTic ||
							this.isMinorInsideTic || this.isMinorInsideCrossTic )
						tmpSpace += ticSize;
				}

				// tic takes up 1x tic
				// space between tic and scale label is 0.5 tic
				// scale label is GetScaleMaxSpace()
				// space between scale label and axis label is 0.5 tic
				if ( this.isScaleVisible )
				{
					// account for the tic labels + 1/2 tic gap between the tic and the label
					tmpSpace += this.scale.GetScaleMaxSpace( g, pane, scaleFactor, true ).Height +
							ticSize * 0.5F;
				}

				string str = MakeTitle();

				// Only add space for the title if there is one
				// Axis Title gets actual height
				if ( str.Length > 0 && this.isShowTitle )
				{
					//tmpSpace += this.TitleFontSpec.BoundingBox( g, str, scaleFactor ).Height;
					fixedSpace = this.TitleFontSpec.BoundingBox( g, str, scaleFactor ).Height;
					tmpSpace += fixedSpace;
				}

				if ( hasTic )
					fixedSpace += ticSize * 1.5F;
			}

			// for the Y axes, make sure that enough space is left to fit the first
			// and last X axis scale label
			if ( this.IsPrimary( pane ) && ( (
					( this is YAxis && (
						( !pane.XAxis.isSkipFirstLabel && !pane.XAxis.IsReverse ) ||
						( !pane.XAxis.isSkipLastLabel && pane.XAxis.IsReverse ) ) ) ||
					( this is Y2Axis && (
						( !pane.XAxis.isSkipFirstLabel && pane.XAxis.IsReverse ) ||
						( !pane.XAxis.isSkipLastLabel && !pane.XAxis.IsReverse ) ) ) ) &&
					pane.XAxis.IsVisible && pane.XAxis.IsScaleVisible ) )
			{
				// half the width of the widest item, plus a gap of 1/2 the charheight
				float tmp = pane.XAxis.scale.GetScaleMaxSpace( g, pane, scaleFactor, true ).Width / 2.0F;
				//+ charHeight / 2.0F;
				//if ( tmp > tmpSpace )
				//	tmpSpace = tmp;

				fixedSpace = Math.Max( tmp, fixedSpace );
			}

			// Verify that the minSpace property was satisfied
			tmpSpace = Math.Max( tmpSpace, this.minSpace * (float)scaleFactor );

			fixedSpace = Math.Max( fixedSpace, this.minSpace * (float)scaleFactor );

			return tmpSpace;
		}

		/// <summary>
		/// Determines if this <see cref="Axis" /> object is a "primary" one.
		/// </summary>
		/// <remarks>
		/// The primary axes are the <see cref="XAxis" /> (always), the first
		/// <see cref="YAxis" /> in the <see cref="GraphPane.YAxisList" /> 
		/// (<see cref="CurveItem.YAxisIndex" /> = 0),  and the first
		/// <see cref="Y2Axis" /> in the <see cref="GraphPane.Y2AxisList" /> 
		/// (<see cref="CurveItem.YAxisIndex" /> = 0).  Note that
		/// <see cref="GraphPane.YAxis" /> and <see cref="GraphPane.Y2Axis" />
		/// always reference the primary axes.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <returns>true for a primary <see cref="Axis" /> (for the <see cref="XAxis" />,
		/// this is always true), false otherwise</returns>
		abstract internal bool IsPrimary( GraphPane pane );

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
		/// <param name="shiftPos">
		/// The number of pixels to shift to account for non-primary axis position (e.g.,
		/// the second, third, fourth, etc. <see cref="YAxis" /> or <see cref="Y2Axis" />.
		/// </param>
		public void DrawScale( Graphics g, GraphPane pane, float scaleFactor, float shiftPos )
		{
			float rightPix,
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
			if ( this.scale.Min >= this.scale.Max )
				return;

			// if the step size is outrageous, then quit
			// (step size not used for log scales)
			if ( !this.IsLog )
			{
				if ( this.scale.Step <= 0 || this.scale.MinorStep <= 0 )
					return;

				double tMajor = ( this.scale.Max - this.scale.Min ) / this.scale.Step,
						tMinor = ( this.scale.Max - this.scale.Min ) / this.scale.MinorStep;
				if ( IsDate )
				{
					tMajor /= GetUnitMultiple( scale.MajorUnit );
					tMinor /= GetUnitMultiple( scale.MinorUnit );
				}
				if ( tMajor > 1000 ||
					( ( this.isMinorTic || this.isMinorInsideTic || this.isMinorOppositeTic )
					&& tMinor > 5000 ) )
					return;
			}

			// calculate the total number of major tics required
			int nTics = this.scale.CalcNumTics();

			// get the first major tic value
			double baseVal = this.scale.CalcBaseTic();

			if ( this.IsVisible )
			{
				if ( !IsPrimary( pane ) )
				{
					// if ( CalcCrossFraction( pane ) != 0.0 )
					if ( IsCrossShifted( pane ) )
					{
						shiftPos = 0;
					}
					else
					{
						// Scaled size (pixels) of a tic
						float ticSize = this.ScaledTic( scaleFactor );

						// if the scalelabels are on the inside, shift everything so the axis is drawn,
						// for example, to the left side of the available space for a YAxis type
						if ( isScaleLabelsInside )
						{
							shiftPos += this.tmpSpace;

							// shift the axis to leave room for the outside tics
							if ( this.isTic || this.isCrossTic || this.isMinorTic || this.isMinorCrossTic )
								shiftPos -= ticSize;
						}
						else
						{
							// if it's not the primary axis, add a tic space for the spacing between axes
							shiftPos += this.axisGap * scaleFactor;

							// if it has inside tics, leave another tic space
							if ( this.isInsideTic || this.isInsideCrossTic ||
									this.isMinorInsideTic || this.isMinorInsideCrossTic )
								shiftPos += ticSize;
						}

					}
				}

				// shift is the position of the actual axis line itself
				// everything else is based on that position.
				float crossShift = this.CalcCrossShift( pane );
				shiftPos += crossShift;

				Pen pen = new Pen( this.color, pane.ScaledPenWidth( ticPenWidth, scaleFactor ) );

				// redraw the axis border
				if ( this.isAxisSegmentVisible )
					g.DrawLine( pen, 0.0F, shiftPos, rightPix, shiftPos );

				// Draw a zero-value line if needed
				if ( this.isZeroLine && this.scale.Min < 0.0 && this.scale.Max > 0.0 )
				{
					float zeroPix = this.scale.LocalTransform( 0.0 );
					g.DrawLine( pen, zeroPix, 0.0F, zeroPix, topPix );
				}

				// draw the major tics and labels
				DrawLabels( g, pane, baseVal, nTics, topPix, shiftPos, scaleFactor );

				DrawMinorTics( g, pane, baseVal, shiftPos, scaleFactor, topPix );

				DrawTitle( g, pane, shiftPos, scaleFactor );
			}
		}

		internal void FixZeroLine( Graphics g, GraphPane pane, float scaleFactor,
				float left, float right )
		{
			// restore the zero line if needed (since the fill tends to cover it up)
			if ( this.isVisible && this.isZeroLine &&
					this.Min < 0.0 && this.Max > 0.0 )
			{
				float zeroPix = this.scale.Transform( 0.0 );

				Pen zeroPen = new Pen( this.Color,
						pane.ScaledPenWidth( this.TicPenWidth, scaleFactor ) );
				g.DrawLine( zeroPen, left, zeroPix, right, zeroPix );
				zeroPen.Dispose();
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
			switch ( unit )
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
		/// <param name="shift">The number of pixels to shift this axis, based on the
		/// value of <see cref="Cross"/>.  A positive value is into the axisRect relative to
		/// the default axis position.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawLabels( Graphics g, GraphPane pane, double baseVal, int nTics,
						float topPix, float shift, float scaleFactor )
		{
			double dVal, dVal2;
			float pixVal, pixVal2;
			string tmpStr;
			float scaledTic = this.ScaledTic( scaleFactor );
			double scaleMult = Math.Pow( (double)10.0, this.scale.ScaleMag );
			Pen pen = new Pen( this.color, pane.ScaledPenWidth( ticPenWidth, scaleFactor ) );
			Pen dottedPen = new Pen( this.gridColor, pane.ScaledPenWidth( gridPenWidth, scaleFactor ) );

			float edgeTolerance = Default.EdgeTolerance * scaleFactor;

			if ( this.gridDashOff > 1e-10 && this.gridDashOn > 1e-10 )
			{
				dottedPen.DashStyle = DashStyle.Custom;
				float[] pattern = new float[2];
				pattern[0] = this.gridDashOn;
				pattern[1] = this.gridDashOff;
				dottedPen.DashPattern = pattern;
			}

			// get the Y position of the center of the axis labels
			// (the axis itself is referenced at zero)
			SizeF maxLabelSize = this.scale.GetScaleMaxSpace( g, pane, scaleFactor, true );
			float maxSpace = maxLabelSize.Height;

			float textTop, textCenter;
			if ( this.isTic )
				textTop = scaledTic * 1.5F;
			else
				textTop = scaledTic * 0.5F;

			double rangeTol = ( this.scale.maxScale - this.scale.minScale ) * 0.001;

			int firstTic = (int)( ( this.scale.minScale - baseVal ) / this.scale.Step + 0.99 );
			if ( firstTic < 0 )
				firstTic = 0;

			// save the position of the previous tic
			float lastPixVal = -10000;

			// loop for each major tic
			for ( int i = firstTic; i < nTics + firstTic; i++ )
			{
				dVal = this.scale.CalcMajorTicValue( baseVal, i );

				// If we're before the start of the scale, just go to the next tic
				if ( dVal < this.scale.minScale )
					continue;
				// if we've already past the end of the scale, then we're done
				if ( dVal > this.scale.maxScale + rangeTol )
					break;

				// convert the value to a pixel position
				pixVal = this.scale.LocalTransform( dVal );

				// see if the tic marks will be draw between the labels instead of at the labels
				// (this applies only to AxisType.Text
				if ( this.isTicsBetweenLabels && this.IsText )
				{
					// We need one extra tic in order to draw the tics between labels
					// so provide an exception here
					if ( i == 0 )
					{
						dVal2 = this.scale.CalcMajorTicValue( baseVal, -0.5 );
						if ( dVal2 >= this.scale.minScale )
						{
							pixVal2 = this.scale.LocalTransform( dVal2 );
							DrawATic( g, pane, pen, pixVal2, topPix, shift, scaledTic );
							// draw the grid
							if ( this.isVisible && this.isShowGrid )
								g.DrawLine( dottedPen, pixVal2, 0.0F, pixVal2, topPix );
						}
					}

					dVal2 = this.scale.CalcMajorTicValue( baseVal, (double)i + 0.5 );
					if ( dVal2 > this.scale.maxScale )
						break;
					pixVal2 = this.scale.LocalTransform( dVal2 );
				}
				else
					pixVal2 = pixVal;

				DrawATic( g, pane, pen, pixVal2, topPix, shift, scaledTic );

				// draw the grid
				if ( this.isVisible && this.isShowGrid )
					g.DrawLine( dottedPen, pixVal2, 0.0F, pixVal2, topPix );

				/*
				// See if the axis is shifted (due to CrossAuto = false) and the current label is within
				// the shiftTolerance of the beginning or end of the axis.  This is the zone in which a
				// label will tend to overlap the opposing axis
				bool isOverlapZone = false;
				if ( Math.Abs(shift) > 0 && ( ( pixVal < edgeTolerance && pane.AxisBorder.IsVisible ) ||
							pixVal > this.maxPix - this.minPix - edgeTolerance  ) )
					isOverlapZone = true;
				*/

				bool isMaxValueAtMaxPix = ( ( this is XAxis || this is Y2Axis ) && !this.scale.IsReverse ) ||
											( this is Y2Axis && this.scale.IsReverse );

				bool isSkipZone = ( ( ( this.isSkipFirstLabel && isMaxValueAtMaxPix ) ||
										( isSkipLastLabel && !isMaxValueAtMaxPix ) ) &&
											pixVal < edgeTolerance ) ||
									( ( ( this.isSkipLastLabel && isMaxValueAtMaxPix ) ||
										( isSkipFirstLabel && !isMaxValueAtMaxPix ) ) &&
											pixVal > this.scale.MaxPix - this.scale.MinPix - edgeTolerance );

				bool isSkipCross = this.isSkipCrossLabel &&
							!this.crossAuto && Math.Abs( this.cross - dVal ) < rangeTol * 10.0;

				isSkipZone = isSkipZone || isSkipCross;

				if ( this.isVisible && this.isScaleVisible && !isSkipZone )
				{
					// For exponential scales, just skip any label that would overlap with the previous one
					// This is because exponential scales have varying label spacing
					if ( this.scale.IsPreventLabelOverlap &&
							Math.Abs( pixVal - lastPixVal ) < maxLabelSize.Width )
						continue;

					// draw the label
					this.scale.MakeLabel( pane, i, dVal, out tmpStr );

					float height;
					if ( this.IsLog && this.scale.IsUseTenPower )
						height = ScaleFontSpec.BoundingBoxTenPower( g, tmpStr, scaleFactor ).Height;
					else
						height = ScaleFontSpec.BoundingBox( g, tmpStr, scaleFactor ).Height;

					if ( this.ScaleAlign == AlignP.Center )
						textCenter = textTop + maxSpace / 2.0F;
					else if ( this.ScaleAlign == AlignP.Outside )
						textCenter = textTop + maxSpace - height / 2.0F;
					else	// inside
						textCenter = textTop + height / 2.0F;

					if ( this.isScaleLabelsInside )
						textCenter = shift - textCenter;
					else
						textCenter = shift + textCenter;


					if ( this.IsLog && this.scale.IsUseTenPower )
						this.ScaleFontSpec.DrawTenPower( g, pane, tmpStr,
							pixVal, textCenter,
							AlignH.Center, AlignV.Center,
							scaleFactor );
					else
						this.ScaleFontSpec.Draw( g, pane.IsPenWidthScaled, tmpStr,
							pixVal, textCenter,
							AlignH.Center, AlignV.Center,
							scaleFactor );

					lastPixVal = pixVal;
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
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="pen">Graphic <see cref="Pen"/> with which to draw the tic mark.</param>
		/// <param name="pixVal">The pixel location of the tic mark on this
		/// <see cref="Axis"/></param>
		/// <param name="topPix">The pixel value of the top of the axis border</param>
		/// <param name="shift">The number of pixels to shift this axis, based on the
		/// value of <see cref="Cross"/>.  A positive value is into the axisRect relative to
		/// the default axis position.</param>
		/// <param name="scaledTic">The length of the tic mark, in points (1/72 inch)</param>
		void DrawATic( Graphics g, GraphPane pane, Pen pen, float pixVal, float topPix,
					float shift, float scaledTic )
		{
			if ( this.isVisible )
			{
				// draw the outside tic
				if ( this.isTic )
					g.DrawLine( pen, pixVal, shift, pixVal, shift + scaledTic );

				// draw the cross tic
				if ( this.isCrossTic )
					g.DrawLine( pen, pixVal, 0.0f, pixVal, scaledTic );

				// draw the inside tic
				if ( this.isInsideTic )
					g.DrawLine( pen, pixVal, shift, pixVal, shift - scaledTic );

				// draw the inside cross tic
				if ( this.isInsideCrossTic )
					g.DrawLine( pen, pixVal, 0.0f, pixVal, -scaledTic );

				// draw the opposite tic
				if ( this.isOppositeTic )
					g.DrawLine( pen, pixVal, topPix, pixVal, topPix + scaledTic );
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
		/// <param name="shift">The number of pixels to shift this axis, based on the
		/// value of <see cref="Cross"/>.  A positive value is into the axisRect relative to
		/// the default axis position.</param>
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
		public void DrawMinorTics( Graphics g, GraphPane pane, double baseVal, float shift,
								float scaleFactor, float topPix )
		{
			if ( ( this.isMinorTic || this.IsMinorOppositeTic || this.isMinorInsideTic ||
					this.isMinorCrossTic || this.isMinorInsideCrossTic || this.isShowMinorGrid )
					&& this.isVisible )
			{
				double tMajor = this.scale.Step * ( IsDate ? GetUnitMultiple( scale.MajorUnit ) : 1.0 ),
					tMinor = this.scale.MinorStep * ( IsDate ? GetUnitMultiple( scale.MinorUnit ) : 1.0 );

				if ( this.IsLog || tMinor < tMajor )
				{
					float minorScaledTic = this.ScaledMinorTic( scaleFactor );

					// Minor tics start at the minimum value and step all the way thru
					// the full scale.  This means that if the minor step size is not
					// an even division of the major step size, the minor tics won't
					// line up with all of the scale labels and major tics.
					double first = this.scale.Min,
							last = this.scale.Max;

					if ( this.IsLog )
					{
						first = Scale.SafeLog( this.scale.Min );
						last = Scale.SafeLog( this.scale.Max );
					}

					double dVal = first;
					float pixVal;
					Pen pen = new Pen( this.color, pane.ScaledPenWidth( ticPenWidth, scaleFactor ) );
					Pen minorGridPen = new Pen( this.minorGridColor,
										 pane.ScaledPenWidth( minorGridPenWidth, scaleFactor ) );

					if ( this.minorGridDashOff > 1e-10 && this.minorGridDashOn > 1e-10 )
					{
						minorGridPen.DashStyle = DashStyle.Custom;
						float[] pattern = new float[2];
						pattern[0] = this.minorGridDashOn;
						pattern[1] = this.minorGridDashOff;
						minorGridPen.DashPattern = pattern;
					}

					int iTic = this.scale.CalcMinorStart( baseVal );
					int majorTic = 0;
					double majorVal = this.scale.CalcMajorTicValue( baseVal, majorTic );

					// Draw the minor tic marks
					while ( dVal < last && iTic < 5000 )
					{
						// Calculate the scale value for the current tic
						dVal = this.scale.CalcMinorTicValue( baseVal, iTic );
						// Maintain a value for the current major tic
						if ( dVal > majorVal )
							majorVal = this.scale.CalcMajorTicValue( baseVal, ++majorTic );

						// Make sure that the current value does not match up with a major tic
						if ( ( ( Math.Abs( dVal ) < 1e-20 && Math.Abs( dVal - majorVal ) > 1e-20 ) ||
							( Math.Abs( dVal ) > 1e-20 && Math.Abs( ( dVal - majorVal ) / dVal ) > 1e-10 ) ) &&
							( dVal >= first && dVal <= last ) )
						{
							pixVal = this.scale.LocalTransform( dVal );

							// draw the minor grid
							if ( this.isShowMinorGrid )
								g.DrawLine( minorGridPen, pixVal, 0.0F, pixVal, topPix );

							// draw the outside tic
							if ( this.isMinorTic )
								g.DrawLine( pen, pixVal, shift, pixVal, shift + minorScaledTic );

							// draw the minor outside cross tic
							if ( this.isMinorCrossTic )
								g.DrawLine( pen, pixVal, 0.0f, pixVal, minorScaledTic );

							// draw the inside tic
							if ( this.isMinorInsideTic )
								g.DrawLine( pen, pixVal, shift, pixVal, shift - minorScaledTic );

							// draw the minor inside cross tic
							if ( this.isMinorInsideCrossTic )
								g.DrawLine( pen, pixVal, 0.0f, pixVal, -minorScaledTic );

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
		/// <param name="shift">The number of pixels to shift this axis, based on the
		/// value of <see cref="Cross"/>.  A positive value is into the axisRect relative to
		/// the default axis position.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawTitle( Graphics g, GraphPane pane, float shiftPos, float scaleFactor )
		{
			string str = MakeTitle();

			// If the Axis is visible, draw the title
			if ( this.isVisible && this.isShowTitle && str.Length > 0 )
			{
				bool hasTic = ( this.isScaleLabelsInside ?
						( this.isInsideTic || this.isInsideCrossTic ||
								this.isMinorInsideTic || this.isMinorInsideCrossTic ) :
						( this.isTic || this.isCrossTic || this.isMinorTic || this.isMinorCrossTic ) );

				// Calculate the title position in screen coordinates
				float x = ( this.scale.MaxPix - this.scale.MinPix ) / 2;

				float scaledTic = ScaledTic( scaleFactor );

				// The space for the scale labels is only reserved if the axis is not shifted due to the
				// cross value.  Note that this could be a problem if the axis is only shifted slightly,
				// since the scale value labels may overlap the axis title.  However, it's not possible to
				// calculate that actual shift amount at this point, because the AxisRect rect has not yet been
				// calculated, and the cross value is determined using a transform of scale values (which
				// rely on AxisRect).

				float gap = this.TitleFontSpec.BoundingBox( g, str, scaleFactor ).Height / 2.0F;
				float y = scaledTic * ( hasTic ? 1.0f : 0.0f ) +
							 ( isScaleVisible ? this.scale.GetScaleMaxSpace( g, pane, scaleFactor, true ).Height + scaledTic * 0.5f : 0 );

				if ( this.isScaleLabelsInside )
					y = shiftPos - y - gap;
				else
					y = shiftPos + y + gap;

				if ( !crossAuto && !isTitleAtCross )
					y = Math.Max( y, gap );

				AlignV alignV = AlignV.Center;

				// Draw the title
				this.TitleFontSpec.Draw( g, pane.IsPenWidthScaled, str, x, y,
							AlignH.Center, alignV, scaleFactor );
			}
		}

		private string MakeTitle()
		{
			if ( this.scale.ScaleMag != 0 && !this.isOmitMag && !this.IsLog )
				return this.title + String.Format( " (10^{0})", this.scale.ScaleMag );
			else
				return this.title;

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
			double basisVal = this.Min;
			return Math.Abs( this.scale.Transform( basisVal +
					( this.IsAnyOrdinal ? 1.0 : pane.ClusterScaleWidth ) ) -
					this.scale.Transform( basisVal ) );
		}
		#endregion

	}
}

