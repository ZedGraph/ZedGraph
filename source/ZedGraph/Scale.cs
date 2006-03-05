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
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZedGraph
{
	/// <summary>
	/// The Scale class is an abstract base class that encompasses the properties
	/// and methods associated with a scale of data.
	/// </summary>
	/// <remarks>This class is inherited by the
	/// <see cref="LinearScale"/>, <see cref="LogScale"/>, <see cref="OrdinalScale"/>,
	/// <see cref="TextScale"/>, <see cref="DateScale"/>, <see cref="ExponentScale"/>,
	/// <see cref="DateAsOrdinalScale"/>, and <see cref="LinearAsOrdinalScale"/>
	/// classes to define specific characteristics for those types.
	/// </remarks>
	/// 
	/// <author> John Champion  </author>
	/// <version> $Revision: 1.7 $ $Date: 2006-03-05 07:28:16 $ </version>
	abstract public class Scale : ISerializable, ICloneable
	{
	#region Fields

		/// <summary> Private fields for the <see cref="Axis"/> scale definitions.
		/// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
		/// <see cref="Step"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
		/// for access to these values.
		/// </summary>
		protected double	min,
								max,
								step,
								minorStep,
								exponent,
								baseTic;

		/// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
		/// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// <see cref="StepAuto"/>, <see cref="MinorStepAuto"/>, 
		/// <see cref="ScaleMagAuto"/> and <see cref="ScaleFormatAuto"/>
		/// for access to these values.
		/// </summary>
		protected bool		minAuto,
								maxAuto,
								stepAuto,
								minorStepAuto,
								scaleMagAuto,
								scaleFormatAuto;

		/// <summary> Private fields for the <see cref="Axis"/> "grace" settings.
		/// These values determine how much extra space is left before the first data value
		/// and after the last data value.
		/// Use the public properties <see cref="MinGrace"/> and <see cref="MaxGrace"/>
		/// for access to these values.
		/// </summary>
		protected double	minGrace,
								maxGrace;


		/// <summary> Private field for the <see cref="Axis"/> scale value display.
		/// Use the public property <see cref="ScaleMag"/> for access to this value.
		/// </summary>
		protected int		scaleMag;

		/// <summary> Private fields for the <see cref="Scale"/> attributes.
		/// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
		/// for access to these values.
		/// </summary>
		protected bool		isReverse,
								isPreventLabelOverlap,
								isUseTenPower;

		/// <summary> Private <see cref="System.Collections.ArrayList"/> field for the <see cref="Axis"/> array of text labels.
		/// This property is only used if <see cref="Type"/> is set to
		/// <see cref="AxisType.Text"/> </summary>
		protected string[] textLabels = null;

		/// <summary> Private field for the format of the <see cref="Axis"/> tic labels.
		/// Use the public property <see cref="ScaleFormat"/> for access to this value. </summary>
		/// <seealso cref="ScaleFormatAuto"/>
		protected string	scaleFormat;

		/// <summary>
		/// Private fields for Unit types to be used for the major and minor tics.
		/// See <see cref="MajorUnit"/> and <see cref="MinorUnit"/> for the corresponding
		/// public properties.
		/// These types only apply for date-time scales (<see cref="IsDate"/>).
		/// </summary>
		/// <value>The value of these types is of enumeration type <see cref="DateUnit"/>
		/// </value>
		protected DateUnit	majorUnit,
									minorUnit;

		/// <summary>
		/// Determines if the scale values are reversed for this <see cref="Axis"/>
		/// </summary>
		/// <value>true for the X values to decrease to the right or the Y values to
		/// decrease upwards, false otherwise</value>
		/// <seealso cref="ZedGraph.Scale.Default.IsReverse"/>.
		public bool IsReverse
		{
			get { return isReverse; }
			set { isReverse = value; }
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
			get { return isPreventLabelOverlap; }
			set { isPreventLabelOverlap = value; }
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
		/// Data range temporary values, used by GetRange().
		/// </summary>
		internal double	rangeMin,
								rangeMax,
								lBound,
								uBound;

		/// <summary>
		/// Scale values for calculating transforms.  These are temporary values
		/// used only during the Draw process.
		/// </summary>
		protected float	minPix,
								maxPix;
	
		/// <summary>
		/// Scale values for calculating transforms.  These are temporary values
		/// used only during the Draw process.
		/// </summary>
		/// <remarks>
		/// These values are just <see cref="Scale.Min" /> and <see cref="Scale.Max" />
		/// for normal linear scales, but for log or exponent scales they will be a
		/// linear representation.  For <see cref="LogScale" />, it is the <see cref="Math.Log" />
		/// of the value, and for <see cref="ExponentScale" />, it is the <see cref="Math.Exp" />
		/// of the value.
		/// </remarks>
		internal double	minScale,
								maxScale;

		internal Axis		parentAxis;

	#endregion

	#region Defaults

		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Scale"/> class.
		/// </summary>
		public struct Default
		{
			/// <summary>
			/// The default "zero lever" for automatically selecting the axis
			/// scale range (see <see cref="PickScale"/>). This number is
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
			/// scale step size (see <see cref="PickScale"/>).
			/// This number is an initial target value for the number of major steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetXSteps = 7.0;
			/// <summary>
			/// The default target number of steps for automatically selecting the Y or Y2 axis
			/// scale step size (see <see cref="PickScale"/>).
			/// This number is an initial target value for the number of major steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetYSteps = 7.0;
			/// <summary>
			/// The default target number of minor steps for automatically selecting the X axis
			/// scale minor step size (see <see cref="PickScale"/>).
			/// This number is an initial target value for the number of minor steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetMinorXSteps = 5.0;
			/// <summary>
			/// The default target number of minor steps for automatically selecting the Y or Y2 axis
			/// scale minor step size (see <see cref="PickScale"/>).
			/// This number is an initial target value for the number of minor steps
			/// on an axis.  This value is maintained only in the
			/// <see cref="Default"/> class, and cannot be changed after compilation.
			/// </summary>
			public static double TargetMinorYSteps = 5.0;
			/// <summary>
			/// The default reverse mode for the <see cref="Axis"/> scale
			/// (<see cref="Axis.IsReverse"/> property). true for a reversed scale
			/// (X decreasing to the left, Y/Y2 decreasing upwards), false otherwise.
			/// </summary>
			public static bool IsReverse = false;
			/// <summary>
			/// The default setting for the <see cref="Axis"/> scale format string
			/// (<see cref="Axis.ScaleFormat"/> property).  For numeric values, this value is
			/// setting according to the <see cref="String.Format(string,object)"/> format strings.  For date
			/// type values, this value is set as per the <see cref="XDate.ToString()"/> function.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeYearYear = 1825;  // 5 years
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Year"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Month"/>.
			/// This value normally defaults to 365 days (1 year).
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeYearMonth = 365;  // 1 year
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Month"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Month"/>.
			/// This value normally defaults to 90 days (3 months).
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeMonthMonth = 90;  // 3 months
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Day"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Day"/>.
			/// This value normally defaults to 10 days.
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeDayDay = 10;  // 10 days
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Day"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Hour"/>.
			/// This value normally defaults to 3 days.
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeDayHour = 3;  // 3 days
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Hour"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Hour"/>.
			/// This value normally defaults to 0.4167 days (10 hours).
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeHourHour = 0.4167;  // 10 hours
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Hour"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Minute"/>.
			/// This value normally defaults to 0.125 days (3 hours).
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeHourMinute = 0.125;  // 3 hours
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Minute"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Minute"/>.
			/// This value normally defaults to 6.94e-3 days (10 minutes).
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeMinuteMinute = 6.94e-3;  // 10 Minutes
			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// If the total span of data exceeds this number (in days), then the auto-range
			/// code will select <see cref="Axis.MajorUnit"/> = <see cref="DateUnit.Minute"/>
			/// and <see cref="Axis.MinorUnit"/> = <see cref="DateUnit.Second"/>.
			/// This value normally defaults to 2.083e-3 days (3 minutes).
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
			/// </summary>
			public static double RangeMinuteSecond = 2.083e-3;  // 3 Minutes

			/// <summary>
			/// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
			/// This values applies only to Date-Time type axes.
			/// This is the format used for the scale values when auto-ranging code
			/// selects a <see cref="Axis.ScaleFormat"/> of <see cref="DateUnit.Year"/>
			/// for <see cref="Axis.MajorUnit"/> and <see cref="DateUnit.Year"/> for 
			/// for <see cref="Axis.MinorUnit"/>.
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
			/// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
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
		}

	#endregion

	#region constructors

		/// <summary>
		/// Basic constructor -- requires that the <see cref="Scale" /> object be intialized with
		/// a pre-existing parent <see cref="Axis" />.
		/// </summary>
		/// <param name="parentAxis">The <see cref="Axis" /> object that is the parent of this
		/// <see cref="Scale" /> instance.</param>
		public Scale( Axis parentAxis )
		{
			this.parentAxis = parentAxis;

			this.min = 0.0;
			this.max = 1.0;
			this.step = 0.1;
			this.minorStep = 0.1;
			this.exponent = 1.0;
			this.scaleMag = 0;
			this.baseTic = PointPair.Missing;

			this.minGrace = Default.MinGrace;
			this.maxGrace = Default.MaxGrace;

			this.minAuto = true;
			this.maxAuto = true;
			this.stepAuto = true;
			this.minorStepAuto = true;
			this.scaleMagAuto = true;
			this.scaleFormatAuto = true;

			this.isReverse = Default.IsReverse;
			this.isUseTenPower = true;
			this.isPreventLabelOverlap = true;

			this.majorUnit = DateUnit.Year;
			this.minorUnit = DateUnit.Year;

			this.scaleFormat = null;
		}

		/// <summary>
		/// Copy Constructor.  Create a new <see cref="Scale" /> object based on the specified
		/// existing one.
		/// </summary>
		/// <param name="rhs">The <see cref="Scale" /> object to be copied.</param>
		public Scale( Scale rhs )
		{
			parentAxis = rhs.parentAxis;

			min = rhs.min;
			max = rhs.max;
			step = rhs.step;
			minorStep = rhs.minorStep;
			exponent = rhs.exponent;
			baseTic = rhs.baseTic;

			minAuto = rhs.minAuto;
			maxAuto = rhs.maxAuto;
			stepAuto = rhs.stepAuto;
			minorStepAuto = rhs.minorStepAuto;
			scaleMagAuto = rhs.scaleMagAuto;
			scaleFormatAuto = rhs.scaleFormatAuto;

			minGrace = rhs.minGrace;
			maxGrace = rhs.maxGrace;

			scaleMag = rhs.scaleMag;

			isUseTenPower = rhs.isUseTenPower;
			isReverse = rhs.isReverse;
			isPreventLabelOverlap = rhs.isPreventLabelOverlap;

			majorUnit = rhs.majorUnit;
			minorUnit = rhs.minorUnit;

			scaleFormat = rhs.scaleFormat;

			textLabels = rhs.textLabels;
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of Clone />
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


		/// <summary>
		/// A construction method that creates a new <see cref="Scale"/> object using the
		/// properties of an existing <see cref="Scale"/> object, but specifying a new
		/// <see cref="AxisType"/>.
		/// </summary>
		/// <remarks>
		/// This constructor is used to change the type of an existing <see cref="Axis" />.
		/// By specifying the old <see cref="Scale"/> object, you are giving a set of properties
		/// (which encompasses all fields associated with the scale, since the derived types
		/// have no fields) to be used in creating a new <see cref="Scale"/> object, only this
		/// time having the newly specified object type.</remarks>
		/// <param name="oldScale">The existing <see cref="Scale" /> object from which to
		/// copy the field data.</param>
		/// <param name="type">An <see cref="AxisType"/> representing the type of derived type
		/// of new <see cref="Scale" /> object to create.</param>
		/// <returns>The new <see cref="Scale"/> object.</returns>
		public static Scale MakeNewScale( Scale oldScale, AxisType type )
		{
			switch ( type )
			{
				case AxisType.Linear:
					return new LinearScale( oldScale );
				case AxisType.Date:
					return new DateScale( oldScale );
				case AxisType.Log:
					return new LogScale( oldScale );
				case AxisType.Exponent:
					return new ExponentScale( oldScale );
				case AxisType.Ordinal:
					return new OrdinalScale( oldScale );
				case AxisType.Text:
					return new TextScale( oldScale );
				case AxisType.DateAsOrdinal:
					return new DateAsOrdinalScale( oldScale );
				case AxisType.LinearAsOrdinal:
					return new LinearAsOrdinalScale( oldScale );
				default:
					throw new Exception( "Implementation Error: Invalid AxisType" );
			}
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
		protected Scale( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			min = info.GetDouble( "min" );
			max = info.GetDouble( "max" );
			step = info.GetDouble( "step" );
			minorStep = info.GetDouble( "minorStep" );
			exponent = info.GetDouble( "exponent" );
			baseTic = info.GetDouble( "baseTic" );


			minAuto = info.GetBoolean( "minAuto" );
			maxAuto = info.GetBoolean( "maxAuto" );
			stepAuto = info.GetBoolean( "stepAuto" );
			minorStepAuto = info.GetBoolean( "minorStepAuto" );
			scaleMagAuto = info.GetBoolean( "scaleMagAuto" );
			scaleFormatAuto = info.GetBoolean( "scaleFormatAuto" );
			
			minGrace = info.GetDouble( "minGrace" );
			maxGrace = info.GetDouble( "maxGrace" );

			scaleMag = info.GetInt32( "scaleMag" );

			isReverse = info.GetBoolean( "isReverse" );
			isPreventLabelOverlap = info.GetBoolean( "isPreventLabelOverlap" );
			isUseTenPower = info.GetBoolean( "isUseTenPower" );

			textLabels = (string[]) info.GetValue( "textLabels", typeof(string[]) );
			scaleFormat = info.GetString( "scaleFormat" );

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
			info.AddValue( "exponent", exponent );
			info.AddValue( "baseTic", baseTic );

			info.AddValue( "minAuto", minAuto );
			info.AddValue( "maxAuto", maxAuto );
			info.AddValue( "stepAuto", stepAuto );
			info.AddValue( "minorStepAuto", minorStepAuto );
			info.AddValue( "scaleMagAuto", scaleMagAuto );
			info.AddValue( "scaleFormatAuto", scaleFormatAuto );

			info.AddValue( "minGrace", minGrace );
			info.AddValue( "maxGrace", maxGrace );

			info.AddValue( "scaleMag", scaleMag );
			info.AddValue( "isReverse", isReverse );
			info.AddValue( "isPreventLabelOverlap", isPreventLabelOverlap );
			info.AddValue( "isUseTenPower", isUseTenPower );

			info.AddValue( "textLabels", textLabels );
			info.AddValue( "scaleFormat", scaleFormat );

			info.AddValue( "majorUnit", majorUnit );
			info.AddValue( "minorUnit", minorUnit );

		}
	#endregion

	#region properties

		/// <summary>
		/// Get an <see cref="AxisType" /> enumeration that indicates the type of this scale.
		/// </summary>
		abstract public AxisType Type { get; }

		/// <summary>
		/// True if this scale is <see cref="AxisType.Log" />, false otherwise.
		/// </summary>
		public bool IsLog { get { return this is LogScale; } }
		/// <summary>
		/// True if this scale is <see cref="AxisType.Exponent" />, false otherwise.
		/// </summary>
		public bool IsExponent { get { return this is ExponentScale; } }
		/// <summary>
		/// True if this scale is <see cref="AxisType.Date" />, false otherwise.
		/// </summary>
		public bool IsDate { get { return this is DateScale; } }
		/// <summary>
		/// True if this scale is <see cref="AxisType.Text" />, false otherwise.
		/// </summary>
		public bool IsText { get { return this is TextScale; } }
		/// <summary>
		/// True if this scale is <see cref="AxisType.Ordinal" />, false otherwise.
		/// </summary>
		/// <remarks>
		/// Note that this is only true for an actual <see cref="OrdinalScale" /> class.
		/// This property will be false for other ordinal types such as
		/// <see cref="AxisType.Text" />, <see cref="AxisType.LinearAsOrdinal" />,
		/// or <see cref="AxisType.DateAsOrdinal" />.  Use the <see cref="IsAnyOrdinal" />
		/// as a "catchall" for all ordinal type axes.
		/// </remarks>
		public bool IsOrdinal { get { return this is OrdinalScale; } }

		/// <summary>
		/// Gets a value that indicates if this <see cref="Scale" /> is of any of the
		/// ordinal types in the <see cref="AxisType" /> enumeration.
		/// </summary>
		/// <seealso cref="Type" />
		public bool IsAnyOrdinal
		{
			get
			{
				AxisType type = this.Type;

				return	type == AxisType.Ordinal ||
							type == AxisType.Text ||
							type == AxisType.LinearAsOrdinal ||
							type == AxisType.DateAsOrdinal;
			}
		}

		/// <summary>
		/// The pixel position at the minimum value for this axis.  This read-only
		/// value is used/valid only during the Draw process.
		/// </summary>
		public float MinPix
		{
			get { return minPix; }
		}
		/// <summary>
		/// The pixel position at the maximum value for this axis.  This read-only
		/// value is used/valid only during the Draw process.
		/// </summary>
		public float MaxPix
		{
			get { return maxPix; }
		}

		/// <summary>
		/// Gets or sets the minimum scale value for this <see cref="Scale" />.
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
		public virtual double Min
		{
			get { return min; }
			set { min = value; }
		}
		/// <summary>
		/// Gets or sets the maximum scale value for this <see cref="Scale" />.
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
		public virtual double Max
		{
			get { return max; }
			set { max = value; }
		}
		/// <summary>
		/// Gets or sets the scale step size for this <see cref="Scale" /> (the increment between
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
			get { return step; }
			set { step = value; }
		}
		/// <summary>
		/// Gets or sets the scale minor step size for this <see cref="Scale" /> (the spacing between
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
			set { minorStep = value; }
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
			get { return exponent; }
			set { exponent = value; }
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
		/// <seealso cref="Axis.Cross"/>
		public double BaseTic
		{
			get { return baseTic; }
			set { baseTic = value; }
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
		/// <seealso cref="Axis.ScaleFontSpec"/>
		// /// <seealso cref="NumDec"/>
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
		/// <seealso cref="Axis.ScaleFontSpec"/>
		// /// <seealso cref="NumDec"/>
		public string ScaleFormat
		{
			get { return scaleFormat; }
			set { scaleFormat = value;  }
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
		/// <seealso cref="Axis.IsOmitMag"/>
		/// <seealso cref="Axis.Title"/>
		/// <seealso cref="Axis.ScaleFormat"/>
		/// <seealso cref="Axis.ScaleFontSpec"/>
		// /// <seealso cref="NumDec"/>
		public int ScaleMag
		{
			get { return scaleMag; }
			set { scaleMag = value; }
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
		/// <seealso cref="Axis.IsOmitMag"/>
		/// <seealso cref="Axis.Title"/>
		/// <seealso cref="ScaleMag"/>
		public bool ScaleMagAuto
		{
			get { return scaleMagAuto; }
			set { scaleMagAuto = value; }
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
		/// <seealso cref="ZedGraph.Scale.Default.MaxGrace"/>
		/// <seealso cref="MinGrace"/>
		public double MaxGrace
		{
			get { return maxGrace; }
			set { maxGrace = value; }
		}

	#endregion

	#region events

		/// <summary>
		/// A delegate that allows full custom formatting of the Axis labels
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane" /> for which the label is to be
		/// formatted</param>
		/// <param name="axis">The <see cref="Axis" /> for which the label is to be formatted</param>
		/// <param name="val">The value to be formatted</param>
		/// <param name="index">The zero-based index of the label to be formatted</param>
		/// <returns>
		/// A string value representing the label, or null if the ZedGraph should go ahead
		/// and generate the label according to the current settings</returns>
		/// <seealso cref="ScaleFormatEvent" />
		public delegate string ScaleFormatHandler( GraphPane pane, Axis axis, double val, int index );

		/// <summary>
		/// Subscribe to this event to handle custom formatting of the scale labels.
		/// </summary>
		public event ScaleFormatHandler ScaleFormatEvent;

	#endregion

	#region methods

		/// <summary>
		/// Setup some temporary transform values in preparation for rendering the
		/// <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// This method is typically called by the parent <see cref="GraphPane"/>
		/// object as part of the <see cref="GraphPane.Draw"/> method.  It is also
		/// called by <see cref="GraphPane.GeneralTransform"/> and
		/// <see cref="GraphPane.ReverseTransform( PointF, out double, out double, out double )"/>
		/// methods to setup for coordinate transformations.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="axis">
		/// The parent <see cref="Axis" /> for this <see cref="Scale" />
		/// </param>
		virtual public void SetupScaleData( GraphPane pane, Axis axis )
		{
			// save the axisRect data for transforming scale values to pixels
			if ( axis is XAxis )
			{
				this.minPix = pane.AxisRect.Left;
				this.maxPix = pane.AxisRect.Right;
			}
			else
			{
				this.minPix = pane.AxisRect.Top;
				this.maxPix = pane.AxisRect.Bottom;
			}

			this.minScale = this.min;
			this.maxScale = this.max;

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
		virtual internal void MakeLabel( GraphPane pane, int index, double dVal, out string label )
		{
			if ( this.ScaleFormatEvent != null )
			{
				label = this.ScaleFormatEvent( pane, this.parentAxis, dVal, index );
				if ( label != null )
					return;
			}

			if ( this.scaleFormat == null )
				this.scaleFormat = Scale.Default.ScaleFormat;

			// linear or ordinal is the default behavior
			// this method is overridden for other Scale types

			double scaleMult = Math.Pow( (double) 10.0, this.scaleMag );

			label = ( dVal / scaleMult ).ToString( this.scaleFormat );
		}

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
		/// <param name="applyAngle">
		/// true to get the bounding box of the text using the <see cref="FontSpec.Angle" />,
		/// false to just get the bounding box without rotation
		/// </param>
		/// <returns>the maximum width of the text in pixel units</returns>
		internal SizeF GetScaleMaxSpace( Graphics g, GraphPane pane, float scaleFactor,
							bool applyAngle )
		{
			string tmpStr;
			double dVal,
				scaleMult = Math.Pow( (double) 10.0, this.scaleMag );
			int i;

			float saveAngle = parentAxis.ScaleFontSpec.Angle;
			if ( !applyAngle )
				parentAxis.ScaleFontSpec.Angle = 0;

			int nTics = CalcNumTics();

			double startVal = CalcBaseTic();

			SizeF maxSpace = new SizeF( 0, 0 );

			// Repeat for each tic
			for ( i = 0; i < nTics; i++ )
			{
				dVal = CalcMajorTicValue( startVal, i );

				// draw the label
				MakeLabel( pane, i, dVal, out tmpStr );

				SizeF sizeF;
				if ( this.IsLog && this.isUseTenPower )
					sizeF = parentAxis.ScaleFontSpec.BoundingBoxTenPower( g, tmpStr,
						scaleFactor );
				else
					sizeF = parentAxis.ScaleFontSpec.BoundingBox( g, tmpStr,
						scaleFactor );

				if ( sizeF.Height > maxSpace.Height )
					maxSpace.Height = sizeF.Height;
				if ( sizeF.Width > maxSpace.Width )
					maxSpace.Width = sizeF.Width;
			}

			parentAxis.ScaleFontSpec.Angle = saveAngle;

			return maxSpace;
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
		virtual internal double CalcMajorTicValue( double baseVal, double tic )
		{
			// Default behavior is a normal linear scale (also works for ordinal types)
			return baseVal + (double) this.step * tic;
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
		virtual internal double CalcMinorTicValue( double baseVal, int iTic )
		{
			// default behavior is a linear axis (works for ordinal types too
			return baseVal + (double) this.minorStep * (double) iTic;
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
		virtual internal int CalcMinorStart( double baseVal )
		{
			// Default behavior is for a linear scale (works for ordinal as well
			return (int) ( ( this.min - baseVal ) / this.minorStep );
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
		virtual internal double CalcBaseTic()
		{
			if ( this.baseTic != PointPair.Missing )
				return this.baseTic;
			else
				// default behavior is linear or ordinal type
				// go to the nearest even multiple of the step size
				return Math.Ceiling( (double) this.min / (double) this.step - 0.00000001 )
														* (double) this.step;
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
		/// <para><see cref="ScaleFormat"/> is set to the display format for the values (this controls the
		/// number of decimal places, whether there are thousands separators, currency types, etc.)</para>
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
		virtual public void PickScale( GraphPane pane, Graphics g, float scaleFactor )
		{
			double minVal = this.rangeMin;
			double maxVal = this.rangeMax;

			// Make sure that minVal and maxVal are legitimate values
			if ( Double.IsInfinity( minVal ) || Double.IsNaN( minVal ) || minVal == Double.MaxValue )
				minVal = 0.0;
			if ( Double.IsInfinity( maxVal ) || Double.IsNaN( maxVal ) || maxVal == Double.MaxValue )
				maxVal = 0.0;

			// if the scales are autoranged, use the actual data values for the range
			double range = maxVal - minVal;

			// "Grace" is applied to the numeric axis types only
			bool numType = !this.IsAnyOrdinal;

			// For autoranged values, assign the value.  If appropriate, adjust the value by the
			// "Grace" value.
			if ( this.minAuto )
			{
				this.min = minVal;
				// Do not let the grace value extend the axis below zero when all the values were positive
				if ( numType && ( this.min < 0 || minVal - this.minGrace * range >= 0.0 ) )
					this.min = minVal - this.minGrace * range;
			}
			if ( this.maxAuto )
			{
				this.max = maxVal;
				// Do not let the grace value extend the axis above zero when all the values were negative
				if ( numType && ( this.max > 0 || maxVal + this.maxGrace * range <= 0.0 ) )
					this.max = maxVal + this.maxGrace * range;
			}

			if ( this.max < this.min )
			{
				if ( this.maxAuto )
					this.max = this.min + 1.0;
				else if ( this.minAuto )
					this.min = this.max - 1.0;
			}

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
			SizeF size = this.GetScaleMaxSpace( g, pane, scaleFactor, false );

			// The font angles are already set such that the Width is parallel to the appropriate (X or Y)
			// axis.  Therefore, we always use size.Width.
			// use the minimum of 1/4 the max Width or 1 character space
			//			double allowance = this.ScaleFontSpec.GetWidth( g, scaleFactor );
			//			if ( allowance > size.Width / 4 )
			//				allowance = size.Width / 4;


			float maxWidth = 1000;
			float temp = 1000;
			float costh = (float) Math.Abs( Math.Cos( this.parentAxis.ScaleFontSpec.Angle * Math.PI / 180.0 ) );
			float sinth = (float) Math.Abs( Math.Sin( this.parentAxis.ScaleFontSpec.Angle * Math.PI / 180.0 ) );

			if ( costh > 0.001 )
				maxWidth = size.Width / costh;
			if ( sinth > 0.001 )
				temp = size.Height / sinth;
			if ( temp < maxWidth )
				maxWidth = temp;


			//maxWidth = size.Width;
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
			if ( parentAxis is XAxis )
				width = ( pane.AxisRect.Width == 0 ) ? pane.PaneRect.Width * 0.75 : pane.AxisRect.Width;
			else
				width = ( pane.AxisRect.Height == 0 ) ? pane.PaneRect.Height * 0.75 : pane.AxisRect.Height;

			int maxLabels = (int) ( width / maxWidth );
			if ( maxLabels <= 0 )
				maxLabels = 1;

			return maxLabels;
		}

		internal void SetScaleMag( double min, double max, double step )
		{
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
			if ( this.scaleFormatAuto )
			{
				int numDec = 0 - (int) ( Math.Floor( Math.Log10( this.step ) ) - this.scaleMag );
				if ( numDec < 0 )
					numDec = 0;
				this.scaleFormat = "f" + numDec.ToString();
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
		protected static double CalcStepSize( double range, double targetSteps )
		{
			// Calculate an initial guess at step size
			double tempStep = range / targetSteps;

			// Get the magnitude of the step size
			double mag = Math.Floor( Math.Log10( tempStep ) );
			double magPow = Math.Pow( (double) 10.0, mag );

			// Calculate most significant digit of the new step size
			double magMsd = ( (int) ( tempStep / magPow + .5 ) );

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
			double magMsd = Math.Ceiling( tempStep / magPow );

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
		/// Internal routine to determine the ordinals of the first and last major axis label.
		/// </summary>
		/// <returns>
		/// This is the total number of major tics for this axis.
		/// </returns>
		virtual internal int CalcNumTics()
		{
			int nTics = 1;

			// default behavior is for a linear or ordinal scale
			nTics = (int) ( ( this.max - this.min ) / this.step + 0.01 ) + 1;

			if ( nTics < 1 )
				nTics = 1;
			else if ( nTics > 500 )
				nTics = 500;

			return nTics;
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

			temp = x / y;
			return y * ( temp - Math.Floor( temp ) );
		}

		internal void SetRange( GraphPane pane, Axis axis )
		{
			// Define suitable default ranges in the event that
			// no data were available
			if ( this.rangeMin >= Double.MaxValue || this.rangeMax <= Double.MinValue )
			{
				// If this is a Y axis, and the main Y axis is valid, use it for defaults
				if ( axis != pane.XAxis &&
					pane.YAxis.Scale.rangeMin < double.MaxValue && pane.YAxis.Scale.rangeMax > double.MinValue )
				{
					this.rangeMin = pane.YAxis.Scale.rangeMin;
					this.rangeMax = pane.YAxis.Scale.rangeMax;
				}
				// Otherwise, if this is a Y axis, and the main Y2 axis is valid, use it for defaults
				else if ( axis != pane.XAxis &&
					pane.Y2Axis.Scale.rangeMin < double.MaxValue && pane.Y2Axis.Scale.rangeMax > double.MinValue )
				{
					this.rangeMin = pane.Y2Axis.Scale.rangeMin;
					this.rangeMax = pane.Y2Axis.Scale.rangeMax;
				}
				// Otherwise, just use 0 and 1
				else
				{
					this.rangeMin = 0;
					this.rangeMax = 1;
				}

			}

			/*
				if ( yMinVal >= Double.MaxValue || yMaxVal <= Double.MinValue )
				{
					if ( y2MinVal < Double.MaxValue && y2MaxVal > Double.MinValue )
					{
						yMinVal = y2MinVal;
						yMaxVal = y2MaxVal;
					}
					else
					{
						yMinVal = 0;
						yMaxVal = 0.01;
					}
				}
			
				if ( y2MinVal >= Double.MaxValue || y2MaxVal <= Double.MinValue )
				{
					if ( yMinVal < Double.MaxValue && yMaxVal > Double.MinValue )
					{
						y2MinVal = yMinVal;
						y2MaxVal = yMaxVal;
					}
					else
					{
						y2MinVal = 0;
						y2MaxVal = 1;
					}
				}
				*/
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
				ratio = ( SafeLog( x ) - this.minScale ) / ( this.maxScale - this.minScale );
			else if ( this.IsExponent )
				ratio = ( SafeExp( x, this.exponent ) - this.minScale ) / ( this.maxScale - this.minScale );
			else
				ratio = ( x - this.minScale ) / ( this.maxScale - this.minScale );

			if ( this.isReverse && ( parentAxis is XAxis ) )
				return (float) ( this.maxPix - ( this.maxPix - this.minPix ) * ratio );
			else if ( parentAxis is XAxis )
				return (float) ( this.minPix + ( this.maxPix - this.minPix ) * ratio );
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
		/// <param name="isOverrideOrdinal">true to force the axis to honor the data
		/// value, rather than replacing it with the ordinal value</param>
		/// <param name="i">The ordinal value of this point, just in case
		/// this is an <see cref="AxisType.Ordinal"/> axis</param>
		/// <param name="x">The coordinate value, in user scale units, to
		/// be transformed</param>
		/// <returns>the coordinate value transformed to screen coordinates
		/// for use in calling the <see cref="Graphics"/> draw routines</returns>
		public float Transform( bool isOverrideOrdinal, int i, double x )
		{
			// ordinal types ignore the X value, and just use the ordinal position
			if ( this.IsAnyOrdinal && i >= 0 && !isOverrideOrdinal )
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
			if ( ( this.isReverse ) == ( parentAxis is XAxis ) )
				val = (double) ( pixVal - this.maxPix )
						/ (double) ( this.minPix - this.maxPix )
						* ( this.maxScale - this.minScale ) + this.minScale;
			else
				val = (double) ( pixVal - this.minPix )
						/ (double) ( this.maxPix - this.minPix )
						* ( this.maxScale - this.minScale ) + this.minScale;

			if ( this.IsLog )
				val = Math.Pow( 10.0, val );
			else if ( this.IsExponent )
				val = Math.Pow( val, 1.0 / exponent );

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
		/// for use in calling the <see cref="Axis.DrawScale"/> method</returns>
		public float LocalTransform( double x )
		{
			// Must take into account Log, and Reverse Axes
			double ratio;
			float rv;

			// Coordinate values for log scales are already in exponent form, so no need
			// to take the log here
			ratio = ( x - this.minScale ) / ( this.maxScale - this.minScale );

			if ( ( this.isReverse && !( parentAxis is YAxis ) ) ||
				( !this.isReverse && ( parentAxis is YAxis ) ) )
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
		public static double SafeLog( double x )
		{
			if ( x > 1.0e-20 )
				return Math.Log10( x );
			else
				return 0.0;
		}

		///<summary>
		///Calculate an exponential in a safe manner to avoid math exceptions
		///</summary> 
		/// <param name="x">The value for which the exponential is to be calculated</param>
		/// <param name="exponent">The exponent value to use for calculating the exponential.</param>
		public static double SafeExp( double x, double exponent )
		{
			if ( x > 1.0e-20 )
				return Math.Pow( x, exponent );
			else
				return 0.0;
		}

	#endregion


	}
}
