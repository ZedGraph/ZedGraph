using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZedGraph
{
	/// <summary>
	/// The Axis class is an abstract base class that encompasses all properties
	/// and methods required to define a graph Axis.  This class is inherited by the
	/// <see cref="XAxis"/>, <see cref="YAxis"/>, and <see cref="Y2Axis"/> classes
	/// to define specific characteristics for those types.
	/// </summary>
	abstract public class Axis
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
		/// <summary> Private fields for the <see cref="Axis"/> scale value display.
		/// Use the public properties <see cref="NumDec"/> and <see cref="ScaleMag"/>
		/// for access to these values. </summary>
		private	 int		numDec,
							scaleMag;
		/// <summary> Private fields for the <see cref="Axis"/> attributes.
		/// Use the public properties <see cref="IsVisible"/>, <see cref="IsShowGrid"/>,
		/// <see cref="IsZeroLine"/>, 
		/// <see cref="IsTic"/>, <see cref="IsInsideTic"/>, <see cref="IsOppositeTic"/>,
		/// <see cref="IsMinorTic"/>, <see cref="IsMinorInsideTic"/>,
		/// <see cref="IsMinorOppositeTic"/>, <see cref="IsTicsBetweenLabels"/>,
		/// <see cref="IsLog"/>, <see cref="IsReverse"/>,
		/// <see cref="IsOmitMag"/>, <see cref="IsText"/>,
		/// and <see cref="IsDate"/> for access to these values.
		/// </summary>
		private bool		isVisible,
							isShowGrid,
							isZeroLine,
							isTic,
							isInsideTic,
							isOppositeTic,
							isMinorTic,
							isMinorInsideTic,
							isMinorOppositeTic,
							isTicsBetweenLabels,
							isReverse,
							isOmitMag;
		/// <summary> Private field for the <see cref="Axis"/> type.  This can be one of the
		/// types as defined in the <see cref="AxisType"/> enumeration.
		/// Use the public property <see cref="Type"/>
		/// for access to this value. </summary>
		private AxisType	type;
		/// <summary> Private field for the <see cref="Axis"/> title string.
		/// Use the public property <see cref="Title"/>
		/// for access to this value. </summary>
		private		 string	title;
		/// <summary> Private field for the format of the <see cref="Axis"/> tic labels.
		/// This field is only used if the <see cref="Type"/> is set to <see cref="AxisType.Date"/>.
		/// Use the public property <see cref="ScaleFormat"/>
		/// for access to this value. </summary>
		/// <seealso cref="ScaleFormatAuto"/>
		private		 string	scaleFormat;
		/// <summary> Public field for the <see cref="Axis"/> array of text labels.
		/// This property is only used if <see cref="Type"/> is set to
		/// <see cref="AxisType.Text"/>.
		/// is set to true. </summary>
		public		 string[]	TextLabels = null;
		/// <summary> Private fields for the <see cref="Axis"/> font specificatios.
		/// Use the public properties <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> for access to these values. </summary>
		private FontSpec	titleFontSpec,
							scaleFontSpec;
		/// <summary> Private fields for the <see cref="Axis"/> drawing dimensions.
		/// Use the public properties <see cref="TicPenWidth"/>, <see cref="TicSize"/>,
		/// <see cref="MinorTicSize"/>, <see cref="GridDashOn"/>, <see cref="GridDashOff"/>,
		/// and <see cref="GridPenWidth"/> for access to these values. </summary>
		private float		ticPenWidth,
							ticSize,
							minorTicSize,
							gridDashOn,
							gridDashOff,
							gridPenWidth;
		/// <summary> Private fields for the <see cref="Axis"/> colors.
		/// Use the public properties <see cref="Color"/> and
		/// <see cref="GridColor"/> for access to these values. </summary>
		private Color	color,
						gridColor;
		
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
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor for <see cref="Axis"/> that sets all axis properties
		/// to default values as defined in the <see cref="Def"/> class.
		/// </summary>
		public Axis()
		{
			this.min = 0.0;
			this.max = 1.0;
			this.step = 0.1;
			this.minorStep = 0.1;
		
			this.minAuto = true;
			this.maxAuto = true;
			this.stepAuto = true;
			this.minorStepAuto = true;
			this.numDecAuto = true;
			this.scaleMagAuto = true;
			this.scaleFormatAuto = true;
		
			this.numDec = 0;
			this.scaleMag = 0;

			this.ticSize = Def.Ax.TicSize;
			this.minorTicSize = Def.Ax.MinorTicSize;
			this.gridDashOn = Def.Ax.GridDashOn;
			this.gridDashOff = Def.Ax.GridDashOff;
			this.gridPenWidth = Def.Ax.GridPenWidth;
		
			this.isVisible = true;
			this.isZeroLine = Def.Ax.IsZeroLine;
			this.isShowGrid = Def.Ax.IsShowGrid;
			this.isReverse = Def.Ax.IsReverse;
			this.isOmitMag = false;
			this.isTic = Def.Ax.IsTic;
			this.isInsideTic = Def.Ax.IsInsideTic;
			this.isOppositeTic = Def.Ax.IsOppositeTic;
			this.isMinorTic = Def.Ax.IsMinorTic;
			this.isMinorInsideTic = Def.Ax.IsMinorInsideTic;
			this.isMinorOppositeTic = Def.Ax.IsMinorOppositeTic;
		
			this.type = Def.Ax.Type;
			this.title = "";
			this.TextLabels = null;
			this.scaleFormat = null;
			
			this.majorUnit = DateUnit.Year;
			this.minorUnit = DateUnit.Year;

			this.ticPenWidth = Def.Ax.TicPenWidth;
			this.color = Def.Ax.Color;
			this.gridColor = Def.Ax.GridColor;
			
			this.titleFontSpec = new FontSpec(
					Def.Ax.TitleFontFamily, Def.Ax.TitleFontSize,
					Def.Ax.TitleFontColor, Def.Ax.TitleFontBold,
					Def.Ax.TitleFontUnderline, Def.Ax.TitleFontItalic );
			this.titleFontSpec.IsFilled = false;
			this.titleFontSpec.IsFramed = false;

			this.scaleFontSpec = new FontSpec(
				Def.Ax.ScaleFontFamily, Def.Ax.ScaleFontSize,
				Def.Ax.ScaleFontColor, Def.Ax.ScaleFontBold,
				Def.Ax.ScaleFontUnderline, Def.Ax.ScaleFontItalic );
			this.scaleFontSpec.IsFilled = false;
			this.scaleFontSpec.IsFramed = false;
		}

		/// <summary>
		/// The Copy Constructor
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

			numDec = rhs.numDec;
			scaleMag = rhs.scaleMag;
			isVisible = rhs.IsVisible;
			isShowGrid = rhs.IsShowGrid;
			isZeroLine = rhs.IsZeroLine;
			isTic = rhs.IsTic;
			isInsideTic = rhs.IsInsideTic;
			isOppositeTic = rhs.IsOppositeTic;
			isMinorTic = rhs.IsMinorTic;
			isMinorInsideTic = rhs.IsMinorInsideTic;
			isMinorOppositeTic = rhs.IsMinorOppositeTic;
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

			titleFontSpec = new FontSpec( rhs.TitleFontSpec );
			scaleFontSpec = new FontSpec( rhs.ScaleFontSpec );

			ticPenWidth = rhs.TicPenWidth;
			ticSize = rhs.TicSize;
			minorTicSize = rhs.MinorTicSize;
			gridDashOn = rhs.GridDashOn;
			gridDashOff = rhs.GridDashOff;
			gridPenWidth = rhs.GridPenWidth;

			color = rhs.Color;
			gridColor = rhs.GridColor;
		} 
		#endregion

		#region Scale Properties
		/// <summary>
		/// The minimum scale value for this axis.  This value can be set
		/// automatically based on the state of <see cref="MinAuto"/>.  If
		/// this value is set manually, then <see cref="MinAuto"/> will
		/// also be set to false.
		/// </summary>
		/// <value> The value is defined in user scale units for <see cref="AxisType.Log"/>
		/// and <see cref="AxisType.Linear"/> axes. For <see cref="AxisType.Text"/> axes,
		/// this value is an ordinal starting with 1.0.  For <see cref="AxisType.Date"/>
		/// axes, this value is in XL Date format (see <see cref="XDate"/>, which is the
		/// number of days since the reference date of January 1, 1900.</value>
		public double Min
		{
			get { return min; }
			set { min = value; this.minAuto = false; }
		}
		/// <summary>
		/// The maximum scale value for this axis.  This value can be set
		/// automatically based on the state of <see cref="MaxAuto"/>.  If
		/// this value is set manually, then <see cref="MaxAuto"/> will
		/// also be set to false.
		/// </summary>
		/// <value> The value is defined in user scale units for <see cref="AxisType.Log"/>
		/// and <see cref="AxisType.Linear"/> axes. For <see cref="AxisType.Text"/> axes,
		/// this value is an ordinal starting with 1.0.  For <see cref="AxisType.Date"/>
		/// axes, this value is in XL Date format (see <see cref="XDate"/>, which is the
		/// number of days since the reference date of January 1, 1900.</value>
		public double Max
		{
			get { return max; }
			set { max = value; this.maxAuto = false; }
		}
		/// <summary>
		/// The scale step size for this axis (the increment between
		/// labeled axis values).  This value can be set
		/// automatically based on the state of <see cref="StepAuto"/>.  If
		/// this value is set manually, then <see cref="StepAuto"/> will
		/// also be set to false.  This value is ignored for <see cref="AxisType.Log"/> and
		/// <see cref="AxisType.Text"/> axes.  For <see cref="AxisType.Date"/> axes, this
		/// value is defined in units of <see cref="MajorUnit"/>.
		/// </summary>
		/// <value> The value is defined in user scale units </value>
		public double Step
		{
			get { return step; }
			set { step = value; this.stepAuto = false; }
		}
		/// <summary>
		/// The type of units used for the major step size (<see cref="Step"/>).
		/// This unit type only applies to Date-Time axes (<see cref="AxisType.Date"/> = true).
		/// The axis is set to date type with the <see cref="Type"/> property.
		/// The unit types are defined as <see cref="DateUnit"/>.
		/// </summary>
		/// <value> The value is a <see cref="DateUnit"/> enum type </value>
		public DateUnit MajorUnit
		{
			get { return majorUnit; }
			set { majorUnit = value; }
		}
		/// <summary>
		/// The type of units used for the minor step size (<see cref="MinorStep"/>).
		/// This unit type only applies to Date-Time axes (<see cref="AxisType.Date"/> = true).
		/// The axis is set to date type with the <see cref="Type"/> property.
		/// The unit types are defined as <see cref="DateUnit"/>.
		/// </summary>
		/// <value> The value is a <see cref="DateUnit"/> enum type </value>
		public DateUnit MinorUnit
		{
			get { return minorUnit; }
			set { minorUnit = value; }
		}
		/// <summary>
		/// The scale minor step size for this axis (the spacing between
		/// minor tics).  This value can be set
		/// automatically based on the state of <see cref="MinorStepAuto"/>.  If
		/// this value is set manually, then <see cref="MinorStepAuto"/> will
		/// also be set to false.  This value is ignored for <see cref="AxisType.Log"/> and
		/// <see cref="AxisType.Text"/> axes.  For <see cref="AxisType.Date"/> axes, this
		/// value is defined in units of <see cref="MinorUnit"/>.
		/// </summary>
		/// <value> The value is defined in user scale units </value>
		public double MinorStep
		{
			get { return minorStep; }
			set { minorStep = value; this.minorStepAuto = false; }
		}
		/// <summary>
		/// Determines whether or not the minimum scale value <see cref="Min"/>
		/// is set automatically.  This value will be set to false if
		/// <see cref="Min"/> is manually changed.
		/// </summary>
		/// <value>true for automatic mode, false for manual mode</value>
		public bool MinAuto
		{
			get { return minAuto; }
			set { minAuto = value; }
		}
		/// <summary>
		/// Determines whether or not the maximum scale value <see cref="Max"/>
		/// is set automatically.  This value will be set to false if
		/// <see cref="Max"/> is manually changed.
		/// </summary>
		/// <value>true for automatic mode, false for manual mode</value>
		public bool MaxAuto
		{
			get { return maxAuto; }
			set { maxAuto = value; }
		}
		/// <summary>
		/// Determines whether or not the scale step size <see cref="Step"/>
		/// is set automatically.  This value will be set to false if
		/// <see cref="Step"/> is manually changed.
		/// </summary>
		/// <value>true for automatic mode, false for manual mode</value>
		public bool StepAuto
		{
			get { return stepAuto; }
			set { stepAuto = value; }
		}
		/// <summary>
		/// Determines whether or not the minor scale step size <see cref="MinorStep"/>
		/// is set automatically.  This value will be set to false if
		/// <see cref="MinorStep"/> is manually changed.
		/// </summary>
		/// <value>true for automatic mode, false for manual mode</value>
		public bool MinorStepAuto
		{
			get { return minorStepAuto; }
			set { minorStepAuto = value; }
		}
		#endregion

		#region Tic Properties
		/// <summary>
		/// The color to use for drawing this <see cref="Axis"/>.  This affects only the tic
		/// marks, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </summary>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		/// <seealso cref="Def.Ax.Color"/>.
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		/// <summary>
		/// The length of the <see cref="Axis"/> tic marks.  This length will be scaled
		/// according to the <see cref="GraphPane.CalcScaleFactor"/> for the
		/// <see cref="GraphPane"/>
		/// </summary>
		/// <value>The tic size is measured in pixels</value>
		/// <seealso cref="Def.Ax.TicSize"/>.
		public float TicSize
		{
			get { return ticSize; }
			set { ticSize = value; }
		}
		/// <summary>
		/// The length of the <see cref="Axis"/> minor tic marks.  This length will be scaled
		/// according to the <see cref="GraphPane.CalcScaleFactor"/> for the
		/// <see cref="GraphPane"/>
		/// </summary>
		/// <value>The tic size is measured in pixels</value>
		/// <seealso cref="Def.Ax.MinorTicSize"/>.
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled tic size, in pixels</returns>
		public float ScaledTic( double scaleFactor )
		{
			return (float) ( this.ticSize * scaleFactor + 0.5 );
		}
		/// <summary>
		/// Calculate the scaled minor tic size for this <see cref="Axis"/>
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>The scaled tic size, in pixels</returns>
		public float ScaledMinorTic( double scaleFactor )
		{
			return (float) ( this.minorTicSize * scaleFactor + 0.5 );
		}
		/// <summary>
		/// This property determines whether or not the major outside tic marks
		/// are shown.  These are the tic marks on the outside of the <see cref="Axis"/> frame.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </summary>
		/// <value>true to show the major outside tic marks, false otherwise</value>
		/// <seealso cref="Def.Ax.IsTic"/>.
		public bool IsTic
		{
			get { return isTic; }
			set { isTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the minor outside tic marks
		/// are shown.  These are the tic marks on the outside of the <see cref="Axis"/> frame.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.  This setting is
		/// ignored (no minor tics are drawn) for text axes (see <see cref="IsText"/>).
		/// </summary>
		/// <value>true to show the minor outside tic marks, false otherwise</value>
		/// <seealso cref="Def.Ax.IsMinorTic"/>.
		public bool IsMinorTic
		{
			get { return isMinorTic; }
			set { isMinorTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the major inside tic marks
		/// are shown.  These are the tic marks on the inside of the <see cref="Axis"/> frame.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </summary>
		/// <value>true to show the major inside tic marks, false otherwise</value>
		/// <seealso cref="Def.Ax.IsInsideTic"/>.
		public bool IsInsideTic
		{
			get { return isInsideTic; }
			set { isInsideTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the major opposite tic marks
		/// are shown.  These are the tic marks on the inside of the <see cref="Axis"/> frame on
		/// the opposite side from the axis.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </summary>
		/// <value>true to show the major opposite tic marks, false otherwise</value>
		/// <seealso cref="Def.Ax.IsOppositeTic"/>.
		public bool IsOppositeTic
		{
			get { return isOppositeTic; }
			set { isOppositeTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the minor inside tic marks
		/// are shown.  These are the tic marks on the inside of the <see cref="Axis"/> frame.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.
		/// </summary>
		/// <value>true to show the minor inside tic marks, false otherwise</value>
		/// <seealso cref="Def.Ax.IsMinorInsideTic"/>.
		public bool IsMinorInsideTic
		{
			get { return isMinorInsideTic; }
			set { isMinorInsideTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the minor opposite tic marks
		/// are shown.  These are the tic marks on the inside of the <see cref="Axis"/> frame on
		/// the opposite side from the axis.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.
		/// </summary>
		/// <value>true to show the minor opposite tic marks, false otherwise</value>
		/// <seealso cref="Def.Ax.IsMinorOppositeTic"/>.
		public bool IsMinorOppositeTic
		{
			get { return isMinorOppositeTic; }
			set { isMinorOppositeTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the major tics will be drawn inbetween the
		/// labels, rather than right at the labels.  Note that this setting is only
		/// applicable if <see cref="Axis.Type"/> = <see cref="AxisType.Text"/>.
		/// </summary>
		/// <value>true to place the text between the labels for text axes, false otherwise</value>
		public bool IsTicsBetweenLabels
		{
			get { return isTicsBetweenLabels; }
			set { isTicsBetweenLabels = value; }
		}
		/// <summary>
		/// The pen width to be used when drawing the tic marks for this <see cref="Axis"/>
		/// </summary>
		/// <value>The pen width is defined in pixels</value>
		/// <seealso cref="Def.Ax.TicPenWidth"/>.
		public float TicPenWidth
		{
			get { return ticPenWidth; }
			set { ticPenWidth = value; }
		}
		#endregion

		#region Grid Properties

		/// <summary>
		/// Determines if the major <see cref="Axis"/> gridlines (at each labeled value) will be shown
		/// </summary>
		/// <value>true to show the gridlines, false otherwise</value>
		/// <seealso cref="Def.Ax.IsShowGrid">Def.Ax.IsShowGrid</seealso>.
		public bool IsShowGrid
		{
			get { return isShowGrid; }
			set { isShowGrid = value; }
		}

		/// <summary>
		/// Determines if a line will be drawn at the zero value for the axis.  That is, a line that
		/// divides the negative values from the positive values.  The default is set according to
		/// <see cref="Def.Ax.IsZeroLine"/>
		/// </summary>
		/// <value>true to show the zero line, false otherwise</value>
		/// <seealso cref="Def.Ax.IsZeroLine"/>.
		public bool IsZeroLine
		{
			get { return isZeroLine; }
			set { isZeroLine = value; }
		}

		/// <summary>
		/// The "Dash On" mode for drawing the grid.  This is the distance,
		/// in pixels, of the dash segments that make up the dashed grid lines.
		/// </summary>
		/// <value>The dash on length is defined in pixel units</value>
		/// <seealso cref="GridDashOff"/>
		/// <seealso cref="IsShowGrid"/>
		/// <seealso cref="Def.Ax.GridDashOn"/>.
		public float GridDashOn
		{
			get { return gridDashOn; }
			set { gridDashOn = value; }
		}
		/// <summary>
		/// The "Dash Off" mode for drawing the grid.  This is the distance,
		/// in pixels, of the spaces between the dash segments that make up
		/// the dashed grid lines.
		/// </summary>
		/// <value>The dash off length is defined in pixel units</value>
		/// <seealso cref="GridDashOn"/>
		/// <seealso cref="IsShowGrid"/>
		/// <seealso cref="Def.Ax.GridDashOff"/>.
		public float GridDashOff
		{
			get { return gridDashOff; }
			set { gridDashOff = value; }
		}
		/// <summary>
		/// The default pen width used for drawing the grid lines.
		/// </summary>
		/// <value>The grid pen width is defined in pixel units</value>
		/// <seealso cref="IsShowGrid"/>
		/// <seealso cref="Def.Ax.GridPenWidth"/>.
		public float GridPenWidth
		{
			get { return gridPenWidth; }
			set { gridPenWidth = value; }
		}
		/// <summary>
		/// The color to use for drawing this <see cref="Axis"/> grid.  This affects only the grid
		/// lines, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </summary>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		/// <seealso cref="Def.Ax.GridColor"/>.
		public Color GridColor
		{
			get { return gridColor; }
			set { gridColor = value; }
		}
		#endregion

		#region Type Properties
		/// <summary>
		/// This property determines whether or not the <see cref="Axis"/> is shown.
		/// Note that even if
		/// the axis is not visible, it can still be actively used to draw curves on a
		/// graph, it will just be invisible to the user
		/// </summary>
		/// <value>true to show the axis, false to disable all drawing of this axis</value>
		/// <seealso cref="Def.XAx.IsVisible"/>.
		/// <seealso cref="Def.YAx.IsVisible"/>.
		/// <seealso cref="Def.Y2Ax.IsVisible"/>.
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
		/// <seealso cref="Def.Ax.IsReverse"/>.
		public bool IsReverse
		{
			get { return isReverse; }
			set { isReverse = value; }
		}
		/// <summary>
		/// Determines if this <see cref="Axis"/> is logarithmic (base 10).  To make this property
		/// true, set <see cref="Type"/> to <see cref="AxisType.Log"/>.
		/// </summary>
		/// <value>true for a logarithmic axis, false for a linear, date, or text axis</value>
		public bool IsLog
		{
			get { return type == AxisType.Log; }
		}
		/// <summary>
		/// Determines if this <see cref="Axis"/> is of the date-time type.  To make this property
		/// true, set <see cref="Type"/> to <see cref="AxisType.Date"/>.
		/// </summary>
		/// <value>true for a date axis, false for a linear, log, or text axis</value>
		public bool IsDate
		{
			get { return type == AxisType.Date; }
		}
		/// <summary>
		/// Tests if this <see cref="Axis"/> is labeled with user provided text
		/// labels rather than calculated numeric values.  The text labels are provided via the
		/// <see cref="TextLabels"/> property.  Internally, the axis is still handled with ordinal values
		/// such that the axis <see cref="Min"/> is set to 1.0, and the axis <see cref="Max"/> is set
		/// to the number of labels.  To make this property true, set <see cref="Type"/> to
		/// <see cref="AxisType.Text"/>.
		/// </summary>
		/// <value>true for a text-based axis, false for a linear, log, or date axes.
		/// If this property is true, then you should also provide
		/// an array of labels via <see cref="TextLabels"/>.
		/// </value>
		public bool IsText
		{
			get { return type == AxisType.Text; }
		}
		/// <summary>
		/// Gets or sets the <see cref="AxisType"/> for this <see cref="Axis"/>.
		/// The type can be either <see cref="AxisType.Linear"/>,
		/// <see cref="AxisType.Log"/>, <see cref="AxisType.Date"/>,
		/// or <see cref="AxisType.Text"/>.
		/// </summary>
		/// <seealso cref="Def.Ax.Type"/>.
		public AxisType Type
		{
			get { return type; }
			set { type = value; }
		}
		#endregion

		#region Label Properties
		/// <summary>
		/// For large scale values, a "magnitude" value (power of 10) is automatically
		/// used for scaling the graph.  This magnitude value is automatically appended
		/// to the end of the <see cref="Axis"/> <see cref="Title"/> (e.g., "(10^4)") to indicate
		/// that a magnitude is in use.  This property controls whether or not the
		/// magnitude is included in the title.  Note that it only affects the axis
		/// title; a magnitude value may still be used even if it is not shown in the title.
		/// </summary>
		/// <value>true to show the magnitude value, false to hide it</value>
		public bool IsOmitMag
		{
			get { return isOmitMag; }
			set { isOmitMag = value; }
		}
		/// <summary>
		/// The text title of this <see cref="Axis"/>.  This normally shows the basis and dimensions of
		/// the scale range, such as "Time (Years)"
		/// </summary>
		/// <value>the title is a string value</value>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}
		/// <summary>
		/// Determines whether or not the scale label format <see cref="ScaleFormat"/>
		/// is determined automatically based on the range of data values.
		/// This value will be set to false if
		/// <see cref="ScaleFormat"/> is manually changed.
		/// </summary>
		/// <value>true if <see cref="ScaleFormat"/> will be set automatically, false
		/// if it is to be set manually by the user</value>
		public bool ScaleFormatAuto
		{
			get { return ScaleFormatAuto; }
			set { ScaleFormatAuto = value; }
		}
		/// <summary>
		/// The format of the <see cref="Axis"/> tic labels.
		/// This property is only used if the <see cref="Type"/> is set to <see cref="AxisType.Date"/>.
		/// This property may be set automatically by ZedGraph, depending on the state of
		/// <see cref="ScaleFormatAuto"/>.
		/// </summary>
		/// <value>This format string is as defined for the <see cref="XDate.ToString"/> function</value>
		public string ScaleFormat
		{
			get { return scaleFormat; }
			set { scaleFormat = value; this.ScaleFormatAuto = false; }
		}
		/// <summary>
		/// Determines whether or not the number of decimal places for value
		/// labels <see cref="NumDec"/> is determined automatically based
		/// on the magnitudes of the scale values.  This value will be set to false if
		/// <see cref="NumDec"/> is manually changed.
		/// </summary>
		/// <value>true if <see cref="NumDec"/> will be set automatically, false
		/// if it is to be set manually by the user</value>
		public bool NumDecAuto
		{
			get { return numDecAuto; }
			set { numDecAuto = value; }
		}
		/// <summary>
		/// The number of decimal places displayed for axis value labels.  This
		/// value can be determined automatically depending on the state of
		/// <see cref="NumDecAuto"/>.  If this value is set manually by the user,
		/// then <see cref="NumDecAuto"/> will also be set to false.
		/// </summary>
		/// <value>the number of decimal places to be displayed for the axis
		/// scale labels</value>
		public int NumDec
		{
			get { return numDec; }
			set { numDec = value; this.numDecAuto = false; }
		}
		/// <summary>
		/// The magnitude multiplier for scale values.  This is used to limit
		/// the size of the displayed value labels.  For example, if the value
		/// is really 2000000, then the graph will display 2000 with a 10^3
		/// magnitude multiplier.  This value can be determined automatically
		/// depending on the state of <see cref="ScaleMagAuto"/>.
		/// If this value is set manually by the user,
		/// then <see cref="ScaleMagAuto"/> will also be set to false.
		/// </summary>
		/// <value>The magnitude multiplier (power of 10) for the scale
		/// value labels</value>
		public int ScaleMag
		{
			get { return scaleMag; }
			set { scaleMag = value; this.scaleMagAuto = false; }
		}
		/// <summary>
		/// Determines whether the <see cref="ScaleMag"/> value will be set
		/// automatically based on the data, or manually by the user.  If the
		/// user manually sets the <see cref="ScaleMag"/> value, then this
		/// flag will be set to false.
		/// </summary>
		/// <value>true to have <see cref="ScaleMag"/> set automatically,
		/// false otherwise</value>
		public bool ScaleMagAuto
		{
			get { return scaleMagAuto; }
			set { scaleMagAuto = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the scale values
		/// </summary>
		/// <seealso cref="Def.Ax.ScaleFontFamily"/>
		/// <seealso cref="Def.Ax.ScaleFontSize"/>
		/// <seealso cref="Def.Ax.ScaleFontColor"/>
		/// <seealso cref="Def.Ax.ScaleFontBold"/>
		/// <seealso cref="Def.Ax.ScaleFontUnderline"/>
		/// <seealso cref="Def.Ax.ScaleFontItalic"/>
		public FontSpec ScaleFontSpec
		{
			get { return scaleFontSpec; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the <see cref="Axis"/> <see cref="Title"/>,
		/// </summary>
		/// <seealso cref="Def.Ax.TitleFontFamily"/>
		/// <seealso cref="Def.Ax.TitleFontSize"/>
		/// <seealso cref="Def.Ax.TitleFontColor"/>
		/// <seealso cref="Def.Ax.TitleFontBold"/>
		/// <seealso cref="Def.Ax.TitleFontUnderline"/>
		/// <seealso cref="Def.Ax.TitleFontItalic"/>
		public FontSpec TitleFontSpec
		{
			get { return titleFontSpec; }
		}
		#endregion

		
		/// <summary>
		/// Restore the scale ranging to automatic mode, and recalculate the
		/// <see cref="Axis"/> scale ranges
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		public void ResetAutoScale( GraphPane pane )
		{
			this.minAuto = true;
			this.maxAuto = true;
			this.stepAuto = true;
			pane.AxisChange();
		}

		/// <summary>
		/// Do all rendering associated with this <see cref="Axis"/> to the specified
		/// <see cref="Graphics"/> device.  This method is normally only
		/// called by the Draw method of the parent <see cref="GraphPane"/> object.
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, double scaleFactor )
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
		/// <see cref="Axis"/>.  This method is only called by the parent
		/// <see cref="GraphPane"/>
		/// object as part of the Draw method.
		/// </summary>
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		abstract public void SetTransformMatrix( Graphics g, GraphPane pane, double scaleFactor );


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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>the maximum width of the text in pixel units</returns>
		protected SizeF GetScaleMaxSpace( Graphics g, GraphPane pane, double scaleFactor )
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
				MakeLabel( i, dVal, out tmpStr );

				SizeF sizeF = this.ScaleFontSpec.BoundingBox( g, tmpStr, scaleFactor );
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>Returns the space, in pixels, required for this axis (between the
		/// paneRect and axisRect)</returns>
		public float CalcSpace( Graphics g, GraphPane pane, double scaleFactor )
		{
			float charHeight = this.ScaleFontSpec.GetHeight( scaleFactor );
			float gap = pane.ScaledGap( scaleFactor );
			float ticSize = this.ScaledTic( scaleFactor );
		
			// axisRect is the actual area of the plot as bounded by the axes
			
			// Always leave 1xgap space, even if no axis is displayed
			float space = gap;

			// Account for the Axis
			if ( this.isVisible )
			{
				// value text gets actual width, gap gets charHeight / 4, tic gets ticSize
				space += this.GetScaleMaxSpace( g, pane, scaleFactor ).Height +
					/* charHeight / 4 + */ ticSize;
		
				// Only add space for the label if there is one
				// Axis Title gets actual height plus 1x gap
				if ( this.title.Length > 0 )
				{
					space += this.TitleFontSpec.MeasureString( g, this.title, scaleFactor ).Height
						/* + charHeight / 2 */;
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawScale( Graphics g, GraphPane pane, double scaleFactor )
		{
			float	rightPix,
					topPix;
					
			if ( this is XAxis )
			{
				rightPix = pane.AxisRect.Width;
				topPix = -pane.AxisRect.Height;
			}
			else if ( this is YAxis )
			{
				rightPix = pane.AxisRect.Height;
				topPix = -pane.AxisRect.Width;
			}
			else  // Y2Axis
			{
				rightPix = pane.AxisRect.Height;
				topPix = pane.AxisRect.Width;
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
				Pen pen = new Pen( this.color, this.ticPenWidth  );
				
				// redraw the axis border
				g.DrawLine( pen, 0.0F, 0.0F, rightPix, 0.0F );

				// Draw a zero-value line if needed
				if ( this.min < 0.0 && this.max > 0.0 )
				{
					float zeroPix = LocalTransform( 0.0 );
					g.DrawLine( pen, zeroPix, 0.0F, zeroPix, topPix );
				}

				// draw the major tics and labels
				DrawLabels( g, pane, baseVal, nTics, topPix, scaleFactor );
			
				DrawMinorTics( g, baseVal, scaleFactor, topPix );
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
						nTics = (int) ( ( year2 - year1 + 1.0 ) / this.step );
						break;
					case DateUnit.Month:
						nTics = (int) ( ( month2 - month1 + 12.0 * (year2 - year1) + 1.0 ) / this.step );
						break;
					case DateUnit.Day:
						nTics = (int) ( ( ( this.max - this.min ) + 1.0 ) / this.step );
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
			else  // regular linear scale
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawLabels( Graphics g, GraphPane pane, double baseVal, int nTics,
						float topPix, double scaleFactor )
		{
			double	dVal, dVal2;
			float	pixVal, pixVal2;
			string	tmpStr;
			float	scaledTic = this.ScaledTic( scaleFactor );
			double	scaleMult = Math.Pow( (double) 10.0, this.scaleMag );
			Pen		pen = new Pen( this.color, this.ticPenWidth  );
			Pen		dottedPen = new Pen( this.gridColor, this.gridPenWidth  );

			dottedPen.DashStyle = DashStyle.Custom;
			float[] pattern = new float[2];
			pattern[0] = this.gridDashOn;
			pattern[1] = this.gridDashOff;
			dottedPen.DashPattern = pattern;

			// get the Y position of the center of the axis labels
			// (the axis itself is referenced at zero)
			float textCenter = ticSize +
				this.GetScaleMaxSpace( g, pane, scaleFactor ).Height / 2.0F;

			// loop for each major tic
			for ( int i=0; i<nTics; i++ )
			{
				dVal = CalcMajorTicValue( baseVal, i );
				
				// If we're before the start of the scale, just go to the next tic
				if ( dVal < this.minScale )
					continue;
				// if we've already past the end of the scale, then we're done
				if ( dVal > this.maxScale )
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

				if ( this.isVisible )
				{
					// draw the label
					MakeLabel( i, dVal, out tmpStr );
					
					this.ScaleFontSpec.Draw( g, tmpStr,
						pixVal, 0.0F + textCenter,
						FontAlignH.Center, FontAlignV.Center, scaleFactor );

				}
		
				// draw the grid
				if ( this.isVisible && this.isShowGrid )
					g.DrawLine( dottedPen, pixVal, 0.0F, pixVal, topPix );
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
		/// <param name="topPix">The pixel value of the top of the axis frame</param>
		/// <param name="scaledTic">The length of the tic mark, in pixels</param>
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
		/// Determine the value for the first major tic.  This is done by finding the first possible
		/// value that is an integral multiple of the step size, taking into account the date/time units
		/// if appropriate.
		/// This routine properly
		/// accounts for <see cref="IsLog"/>, <see cref="IsText"/>, and other axis format settings.
		/// </summary>
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
		/// Determine the value for any major tic.  This routine properly
		/// accounts for <see cref="IsLog"/>, <see cref="IsText"/>, and other axis format settings.
		/// </summary>
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
				return baseVal * Math.Pow( 10.0, tic );
			}
			else // regular linear scale
			{
				return baseVal + (double) this.step * tic;
			}
		}
		
		/// <summary>
		/// Make a value label for the axis at the specified ordinal position.  This routine properly
		/// accounts for <see cref="IsLog"/>, <see cref="IsText"/>, and other axis format settings.
		/// </summary>
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
		private void MakeLabel( int index, double dVal, out string label )
		{
			// draw the label
			if ( this.IsText )
			{
				if ( this.TextLabels == null || index < 0 || index >= TextLabels.Length )
					label = "";
				else
					label = TextLabels[index];
			}
			else if ( this.IsDate )
			{
				if ( this.scaleFormat == null )
					this.scaleFormat = Def.Ax.ScaleFormat;
				label = XDate.ToString( dVal, this.scaleFormat );
			}
			else if ( this.IsLog )
			{
				if ( index >= -3 && index <= 4 )
					label = string.Format( "{0}", Math.Pow( 10.0, index ) );
				else
					label = string.Format( "1e{0}", index );
			}
			else
			{
				double	scaleMult = Math.Pow( (double) 10.0, this.scaleMag );

				string tmpStr = "{0:F*}";
				tmpStr = tmpStr.Replace("*", this.numDec.ToString("D") );
				label = String.Format( tmpStr, dVal / scaleMult );
			}
		}

		/// <summary>
		/// Draw the minor tic marks as
		/// required for this <see cref="Axis"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="baseVal">
		/// The scale value for the first major tic position.  This is the reference point
		/// for all other tic marks.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <param name="topPix">
		/// The pixel location of the far side of the axisRect from this axis.
		/// This value is the axisRect.Height for the XAxis, or the axisRect.Width
		/// for the YAxis and Y2Axis.
		/// </param>
		public void DrawMinorTics( Graphics g, double baseVal, double scaleFactor, float topPix )
		{
			if ( this.isMinorTic && this.isVisible && ! this.IsText )
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
					double	dVal = this.min;
					float	pixVal;
					Pen		pen = new Pen( this.color, this.ticPenWidth  );
					
					int iTic = CalcMinorStart( baseVal );
					
					// Draw the minor tic marks
					while ( dVal < this.max && iTic < 5000 )
					{
						dVal = CalcMinorTicValue( baseVal, iTic );

						if ( dVal >= this.min && dVal <= this.max )
						{
							pixVal = this.LocalTransform( dVal );

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
		/// Determine the value for any minor tic.  This routine properly
		/// accounts for <see cref="IsLog"/>, <see cref="IsText"/>, and other axis format settings.
		/// </summary>
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
				return baseVal * Math.Pow( 10.0, (double) ( iTic / 9 ) ) * (double) ( ( iTic % 9 ) + 1 );
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
		/// Draw the title for this <see cref="Axis"/>.  On entry, it is assumed that the
		/// graphics transform has been configured so that the origin is at the left side
		/// of this axis, and the axis is aligned along the X coordinate direction.
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawTitle( Graphics g, GraphPane pane, double scaleFactor )
		{
			string str;

			if ( this.ScaleMag != 0 && ! this.IsOmitMag )
				str = this.title + String.Format( " (10^{0})", this.ScaleMag );
			else
				str = this.title;

			// If the Axis is visible, draw the title
			if ( this.isVisible && str.Length > 0 )
			{		
				// Calculate the title position in screen coordinates
				float x = ( this.maxPix - this.minPix ) / 2;
				float y = ScaledTic( scaleFactor ) +
							GetScaleMaxSpace( g, pane, scaleFactor ).Height;
				FontAlignV alignV = FontAlignV.Top;
				if ( this is YAxis )
					alignV = FontAlignV.Bottom;

				// Draw the title
				this.TitleFontSpec.Draw( g, str, x, y,
							FontAlignH.Center, alignV, scaleFactor );
			}
		}

		/// <summary>
		/// Select a reasonable scale given a range of data values.  The scale range is chosen
		/// based on increments of 1, 2, or 5 (because they are even divisors of 10).  This
		/// routine honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// and <see cref="StepAuto"/> autorange settings as well as the <see cref="IsLog"/>
		/// setting.  In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  The basic defaults for
		/// scale selection are defined using <see cref="Def.Ax.ZeroLever"/> and
		/// <see cref="Def.Ax.TargetSteps"/> from the <see cref="Def"/> default class.
		/// <para>On Exit:</para>
		/// <para><see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)</para>
		/// <para><see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)</para>
		/// <para><see cref="Step"/> is set to scale step size (if <see cref="StepAuto"/> = true)</para>
		/// <para><see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)</para>
		/// <para><see cref="ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="NumDec"/> is set to the number of decimal places to display (if <see cref="NumDecAuto"/> = true)</para>
		/// </summary>
		/// <param name="minVal">The minimum value of the data range for setting this
		/// <see cref="Axis"/> scale range</param>
		/// <param name="maxVal">The maximum value of the data range for setting this
		/// <see cref="Axis"/> scale range</param>
		public void PickScale( double minVal, double maxVal )
		{
			// if the scales are autoranged, use the actual data values for the range
			if ( this.minAuto )
				this.min = minVal;
			if ( this.maxAuto )
				this.max = maxVal;
		
			// if this is a text-based axis, then ignore all settings and make it simply ordinal
			if ( this.IsText )
			{
				// if text labels are provided, then autorange to the number of labels
				if ( this.TextLabels != null )
				{
					if ( this.minAuto )
						this.min = 0.5;
					if ( this.maxAuto )
						this.max = this.TextLabels.Length + 0.5;
				}

				// Test for trivial condition of range = 0 and pick a suitable default
				if ( this.max - this.min < .1 )
				{
					if ( this.maxAuto )
						this.max = this.min + 10.0;
					else
						this.min = this.max - 10.0;
				}

				this.step = 1.0;
				this.numDec = 0;
				this.scaleMag = 0;
			}	
			else if ( this.IsLog )	// Log Scale
			{
				if ( this.scaleMagAuto )
					this.scaleMag = 0;		// Never use a magnitude shift for log scales
				if ( this.numDecAuto )
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
						this.max = this.min * 10.0;
					else
						this.min = this.max / 10.0;
				}
				
				// Get the nearest power of 10 (no partial log cycles allowed)
				if ( this.minAuto )
					this.min = Math.Pow( (double) 10.0,
								Math.Floor( Math.Log10( this.min ) ) );
				if ( this.maxAuto )
					this.max = Math.Pow( (double) 10.0,
								Math.Ceiling( Math.Log10( this.max ) ) );
		
			}
			else if ( this.IsDate )			// Date Scale
			{
				
				// Test for trivial condition of range = 0 and pick a suitable default
				if ( this.max - this.min < 1.0e-20 )
				{
					if ( this.maxAuto )
						this.max = 1.0 + this.min;
					else
						this.min = this.max - 1.0;
				}
		
				// Calculate the new step size
				if ( this.stepAuto )
					this.step = CalcDateStepSize( this.max - this.min, Def.Ax.TargetSteps );
				
				// Calculate the scale minimum
				if ( this.minAuto )
					this.min = CalcEvenStepDate( this.min, -1 );
		
				// Calculate the scale maximum
				if ( this.maxAuto )
					this.max = CalcEvenStepDate( this.max, 1 );

				this.scaleMag = 0;		// Never use a magnitude shift for date scales
				this.numDec = 0;		// The number of decimal places to display is not used
		
			}
			else			// Linear Scale
			{
				
				// Test for trivial condition of range = 0 and pick a suitable default
				if ( this.max - this.min < 1.0e-20 )
				{
					if ( this.maxAuto )
						this.max = 1.0 + this.min;
					else
						this.min = this.max - 1.0;
				}
		
				// This is the zero-lever test.  If minVal is within the zero lever fraction
				// of the data range, then use zero.
		
				if ( this.minAuto && this.min > 0 &&
						this.min / ( this.max - this.min ) < Def.Ax.ZeroLever )
					this.min = 0;
		
				// Repeat the zero-lever test for cases where the maxVal is less than zero
				if ( this.maxAuto && this.max < 0 &&
						Math.Abs( this.max / ( this.max - this.min )) <
								Def.Ax.ZeroLever )
					this.max = 0;
		
				// Calculate the new step size
				if ( this.stepAuto )
					this.step = CalcStepSize( this.max - this.min, Def.Ax.TargetSteps );
		
				// Calculate the new step size
				if ( this.minorStepAuto )
					this.minorStep = CalcStepSize( this.step, Def.Ax.TargetMinorSteps );
		
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
		
			return;
		}
		
		/// <summary>
		/// Calculate a step size based on a data range.  This utility routine
		/// will try to honor the <see cref="Def.Ax.TargetSteps"/> number of
		/// steps while using a rational increment (1, 2, or 5 -- which are
		/// even divisors of 10).  This method is used by <see cref="PickScale"/>.
		/// </summary>
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

			if ( range > Def.Ax.RangeYearYear )
			{
				majorUnit = DateUnit.Year;
				if ( this.scaleFormatAuto )
					this.scaleFormat = "&yyyy";
				tempStep = Math.Floor( tempStep/365.0 );
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
			else if ( range > Def.Ax.RangeYearMonth )
			{
				majorUnit = DateUnit.Year;
				if ( this.scaleFormatAuto )
					this.scaleFormat = "&mmm-&yy";
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
			else if ( range > Def.Ax.RangeMonthMonth )
			{
				majorUnit = DateUnit.Month;
				if ( this.scaleFormatAuto )
					this.scaleFormat = "&mmm-&yy";
				tempStep = Math.Floor( tempStep / 30.0 );
				if ( tempStep < 1.0 )
					tempStep = 1.0;
				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Month;
					minorStep = 0.25;
				}
			}
			else if ( range > Def.Ax.RangeDayDay )
			{
				majorUnit = DateUnit.Day;
				if ( this.scaleFormatAuto )
					scaleFormat = "&d-&mmm";
				tempStep = Math.Floor( tempStep );
				if ( tempStep < 1.0 )
					tempStep = 1.0;
				if ( minorStepAuto )
				{
					minorUnit = DateUnit.Day;
					minorStep = 1.0;
				}
			}
			else if ( range > Def.Ax.RangeDayHour )
			{
				majorUnit = DateUnit.Day;
				if ( this.scaleFormatAuto )
					this.scaleFormat = "&d-&mmm &hh:&nn";
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
			else if ( range > Def.Ax.RangeHourHour )
			{
				majorUnit = DateUnit.Hour;
				tempStep = Math.Floor( tempStep * XDate.HoursPerDay );
				if ( this.scaleFormatAuto )
					scaleFormat = "&hh:&nn";

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
			else if ( range > Def.Ax.RangeHourMinute )
			{
				majorUnit = DateUnit.Hour;
				tempStep = 1.0;
				if ( this.scaleFormatAuto )
					scaleFormat = "&hh:&nn";

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
			else if ( range > Def.Ax.RangeMinuteMinute )
			{
				majorUnit = DateUnit.Minute;
				if ( this.scaleFormatAuto )
					scaleFormat = "&hh:&nn";

				tempStep = Math.Floor( tempStep * XDate.MinutesPerDay );
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
			else if ( range > Def.Ax.RangeMinuteSecond )
			{
				majorUnit = DateUnit.Minute;
				tempStep = 1.0;
				if ( this.scaleFormatAuto )
					scaleFormat = "&nn:&ss";

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
					scaleFormat = "&nn:&ss";

				tempStep = Math.Floor( tempStep * XDate.SecondsPerDay );
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

		/// <summary>
		/// Transform the coordinate value from user coordinates (scale value)
		/// to graphics device coordinates (pixels).  This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).  Note that
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method.
		/// </summary>
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
		/// Reverse transform the user coordinates (scale value)
		/// given a graphics device coordinate (pixels).  This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).  Note that
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method (this is called everytime
		/// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
		/// </summary>
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
		/// to graphics device coordinates (pixels), assuming that the origin
		/// has been set to the "left" of this axis, facing from the label side.
		/// Note that the left side corresponds to the scale minimum for the X and
		/// Y2 axes, but it is the scale maximum for the Y axis.
		/// This method takes into
		/// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
		/// logarithmic state (<see cref="IsLog"/>), scale reverse state
		/// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
		/// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).  Note that
		/// <see cref="SetupScaleData"/> must be called for the
		/// current configuration before using this method.
		/// </summary>
		/// <param name="x">The coordinate value, in user scale units, to
		/// be transformed</param>
		/// <returns>the coordinate value transformed to screen coordinates
		/// for use in calling the <see cref="DrawScale"/> method</returns>
		public float LocalTransform( double x )
		{
			// Must take into account Log, and Reverse Axes
			double	ratio;
			float	rv;

			if ( this.IsLog )
				ratio = ( SafeLog( x ) - this.minScale ) / ( this.maxScale  - this.minScale );
			else
				ratio = ( x - this.minScale ) / ( this.maxScale - this.minScale );
			
			if ( ( this.isReverse && !(this is YAxis) ) ||
				( !this.isReverse && (this is YAxis ) ) )
				rv = (float) ( ( this.maxPix - this.minPix ) * ( 1.0F - ratio ) );
			else
				rv = (float) ( ( this.maxPix - this.minPix ) * ratio );

			return rv;
		}

		/// <summary>
		/// Determine the width, in screen pixel units, of each bar cluster including
		/// the cluster gaps and bar gaps.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <returns>The width of each bar cluster, in pixel units</returns>
		public float GetClusterWidth( GraphPane pane )
		{
			return this.Transform( 2.0 ) - this.Transform( 1.0 );
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
		
		/// <summary>
		/// Create an array of default values for a curve associated with this axis.  The default
		/// values are simple ordinals based on the number of axis labels.
		/// </summary>
		/// <returns>an ordinal array of floating point double values</returns>
		public double[] MakeDefaultArray()
		{
			int length = 10;
			if ( this.IsText && this.TextLabels != null )
				length = this.TextLabels.Length;

			double[] dArray = new double[length];
			
			for ( int i=0; i<length; i++ )
				dArray[i] = (double) i;
			
			return dArray;
		}

	}
}
