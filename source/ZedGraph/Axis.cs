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
		/// <summary> Private fields for the <see cref="Axis"/> scale definitions.
		/// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
		/// <see cref="Step"/>, and <see cref="MinorStep"/> for access to these values.
		/// </summary>
		private		 double	min,
							max,
							step,
							minorStep;
		/// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
		/// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
		/// <see cref="StepAuto"/>, <see cref="MinorStepAuto"/>, <see cref="MinorStepAuto"/>,
		/// <see cref="NumDecAuto"/>, and <see cref="ScaleMagAuto"/> for access to these values.
		/// </summary>
		private	 bool		minAuto,
							maxAuto,
							stepAuto,
							minorStepAuto,
							numDecAuto,
							scaleMagAuto;
		/// <summary> Private fields for the <see cref="Axis"/> scale value display.
		/// Use the public properties <see cref="NumDec"/> and <see cref="ScaleMag"/>
		/// for access to these values. </summary>
		private	 int		numDec,
							scaleMag;
		/// <summary> Private fields for the <see cref="Axis"/> attributes.
		/// Use the public properties <see cref="IsVisible"/>, <see cref="IsShowGrid"/>,
		/// <see cref="IsTic"/>, <see cref="IsInsideTic"/>, <see cref="IsOppositeTic"/>,
		/// <see cref="IsMinorTic"/>, <see cref="IsMinorInsideTic"/>,
		/// <see cref="IsMinorOppositeTic"/>, <see cref="IsLog"/>, <see cref="IsReverse"/>,
		/// and <see cref="IsOmitMag"/> for access to these values.
		/// </summary>
		private bool		isVisible,
							isShowGrid,
							isTic,
							isInsideTic,
							isOppositeTic,
							isMinorTic,
							isMinorInsideTic,
							isMinorOppositeTic,
							isLog,
							isReverse,
							isOmitMag;
		/// <summary> Private field for the <see cref="Axis"/> title string.
		/// Use the public property <see cref="Title"/>
		/// for access to this value. </summary>
		private		 string	title;
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
		/// Scale values for calculating transforms.  These are temporary values
		/// used only during the Draw process.
		/// </summary>
		private double	minScale,
						maxScale;
							
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
		
			this.numDec = 0;
			this.scaleMag = 0;

			this.ticSize = Def.Ax.TicSize;
			this.minorTicSize = Def.Ax.MinorTicSize;
			this.gridDashOn = Def.Ax.GridDashOn;
			this.gridDashOff = Def.Ax.GridDashOff;
			this.gridPenWidth = Def.Ax.GridPenWidth;
		
			this.isVisible = true;
			this.isShowGrid = Def.Ax.IsShowGrid;
			this.isLog = Def.Ax.IsLog;
			this.isReverse = Def.Ax.IsReverse;
			this.isOmitMag = false;
			this.isTic = Def.Ax.IsTic;
			this.isInsideTic = Def.Ax.IsInsideTic;
			this.isOppositeTic = Def.Ax.IsOppositeTic;
			this.isMinorTic = Def.Ax.IsMinorTic;
			this.isMinorInsideTic = Def.Ax.IsMinorInsideTic;
			this.isMinorOppositeTic = Def.Ax.IsMinorOppositeTic;
		
			this.title = "";

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
		/// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the scale values
		/// </summary>
		public FontSpec ScaleFontSpec
		{
			get { return scaleFontSpec; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the <see cref="Axis"/> <see cref="Title"/>,
		/// </summary>
		public FontSpec TitleFontSpec
		{
			get { return titleFontSpec; }
		}
		/// <summary>
		/// The minimum scale value for this axis.  This value can be set
		/// automatically based on the state of <see cref="MinAuto"/>.  If
		/// this value is set manually, then <see cref="MinAuto"/> will
		/// also be set to false.
		/// </summary>
		/// <value> The value is defined in user scale units </value>
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
		/// <value> The value is defined in user scale units </value>
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
		/// also be set to false.
		/// </summary>
		/// <value> The value is defined in user scale units </value>
		public double Step
		{
			get { return step; }
			set { step = value; this.stepAuto = false; }
		}
		/// <summary>
		/// The scale minor step size for this axis (the spacing between
		/// minor tics).  This value can be set
		/// automatically based on the state of <see cref="MinorStepAuto"/>.  If
		/// this value is set manually, then <see cref="MinorStepAuto"/> will
		/// also be set to false.
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
		/// The length of the <see cref="Axis"/> tic marks.  This length will be scaled
		/// according to the <see cref="GraphPane.CalcScaleFactor"/> for the
		/// <see cref="GraphPane"/>
		/// </summary>
		/// <value>The tic size is measured in pixels</value>
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
		/// This property determines whether or not the <see cref="Axis"/> is shown.
		/// Note that even if
		/// the axis is not visible, it can still be actively used to draw curves on a
		/// graph, it will just be invisible to the user
		/// </summary>
		/// <value>true to show the axis, false to disable all drawing of this axis</value>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		/// <summary>
		/// This property determines whether or not the major outside tic marks
		/// are shown.  These are the tic marks on the outside of the <see cref="Axis"/> frame.
		/// The major tic spacing is controlled by <see cref="Step"/>.
		/// </summary>
		/// <value>true to show the major outside tic marks, false otherwise</value>
		public bool IsTic
		{
			get { return isTic; }
			set { isTic = value; }
		}
		/// <summary>
		/// This property determines whether or not the minor outside tic marks
		/// are shown.  These are the tic marks on the outside of the <see cref="Axis"/> frame.
		/// The minor tic spacing is controlled by <see cref="MinorStep"/>.
		/// </summary>
		/// <value>true to show the minor outside tic marks, false otherwise</value>
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
		public bool IsMinorOppositeTic
		{
			get { return isMinorOppositeTic; }
			set { isMinorOppositeTic = value; }
		}
		/// <summary>
		/// Determines if the major <see cref="Axis"/> gridlines (at each labeled value) will be shown
		/// </summary>
		/// <value>true to show the gridlines, false otherwise</value>
		public bool IsShowGrid
		{
			get { return isShowGrid; }
			set { isShowGrid = value; }
		}
		/// <summary>
		/// Determines if this <see cref="Axis"/> is logarithmic (base 10)
		/// </summary>
		/// <value>true for a logarithmic axis, false for a cartesian axis</value>
		public bool IsLog
		{
			get { return isLog; }
			set { isLog= value; }
		}
		/// <summary>
		/// Determines if the scale values are reversed for this <see cref="Axis"/>
		/// </summary>
		/// <value>true for the X values to decrease to the right or the Y values to
		/// decrease upwards, false otherwise</value>
		public bool IsReverse
		{
			get { return isReverse; }
			set { isReverse = value; }
		}
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
		/// The pen width to be used when drawing the tic marks for this <see cref="Axis"/>
		/// </summary>
		/// <value>The pen width is defined in pixels</value>
		public float TicPenWidth
		{
			get { return ticPenWidth; }
			set { ticPenWidth = value; }
		}
		/// <summary>
		/// The "Dash On" mode for drawing the grid.  This is the distance,
		/// in pixels, of the dash segments that make up the dashed grid lines.
		/// </summary>
		/// <value>The dash on length is defined in pixel units</value>
		/// <seealso cref="GridDashOff"/>
		/// <seealso cref="IsShowGrid"/>
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
		public float GridDashOff
		{
			get { return gridDashOff; }
			set { gridDashOff = value; }
		}
		/// <summary>
		/// The default pen width used for drawing the grid lines.
		/// </summary>
		/// <seealso cref="IsShowGrid"/>
		public float GridPenWidth
		{
			get { return gridPenWidth; }
			set { gridPenWidth = value; }
		}
		/// <summary>
		/// The color to use for drawing this <see cref="Axis"/>.  This affects only the tic
		/// marks, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </summary>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
		/// <summary>
		/// The color to use for drawing this <see cref="Axis"/> grid.  This affects only the grid
		/// lines, since the <see cref="TitleFontSpec"/> and
		/// <see cref="ScaleFontSpec"/> both have their own color specification.
		/// </summary>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class</value>
		public Color GridColor
		{
			get { return gridColor; }
			set { gridColor = value; }
		}
		
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

			if ( this.isLog )
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

/*
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
		protected float GetScaleMaxWidth( Graphics g, GraphPane pane, double scaleFactor )
		{
			string tmpStr;
			double	dVal,
					scaleMult = Math.Pow( (double) 10.0, this.scaleMag );
			int		i, iStart, iEnd;

			if ( this.isLog )
			{
				iStart = (int) ( Math.Ceiling( SafeLog(this.min) - 0.000001 ) + 0.000001 );
				iEnd = (int) ( Math.Floor( SafeLog(this.max) + 0.000001 ) + 0.000001 );
			}
			else
			{
				iStart = 0;
				iEnd = (int) (( this.max - this.min ) / this.step + 0.01);
			}

			float width;
			float maxWidth = 0;

			// Only worry about the first and the last value on the scale
			for ( i=iStart; i<=iEnd; i+=iEnd-iStart)
			{
				if ( this.isLog )
					dVal = Math.Pow( (double) 10.0, (double) i );
				else
					dVal = (double) this.min + (double) this.step * (double) i;

				// draw the label
				if ( isLog )
					tmpStr = String.Format( "10^{0}", i );
				else
				{
					tmpStr = "{0:F*}";
					tmpStr = tmpStr.Replace("*", this.numDec.ToString("D") );
					tmpStr = String.Format( tmpStr, dVal / scaleMult );
				}

				width = this.ScaleFontSpec.MeasureString( g, tmpStr, scaleFactor ).Width;
				if ( width > maxWidth )
					maxWidth = width;
			}

			return maxWidth;
		}
*/
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
			int		i, iStart, iEnd;

			if ( this.isLog )
			{
				iStart = (int) ( Math.Ceiling( SafeLog(this.min) - 0.000001 ) + 0.000001 );
				iEnd = (int) ( Math.Floor( SafeLog(this.max) + 0.000001 ) + 0.000001 );
			}
			else
			{
				iStart = 0;
				iEnd = (int) (( this.max - this.min ) / this.step + 0.01);
			}

			SizeF maxSpace = new SizeF( 0, 0 );

			// Only worry about the first and the last value on the scale
			for ( i=iStart; i<=iEnd; i+=iEnd-iStart)
			{
				if ( this.isLog )
					dVal = Math.Pow( (double) 10.0, (double) i );
				else
					dVal = (double) this.min + (double) this.step * (double) i;

				// draw the label
				if ( isLog )
				{
					if ( i >= -3 && i <= 4 )
						tmpStr = string.Format( "{0}", Math.Pow( 10.0, i ) );
					else
						tmpStr = string.Format( "1e{0}", i );
				}
				else
				{
					tmpStr = "{0:F*}";
					tmpStr = tmpStr.Replace("*", this.numDec.ToString("D") );
					tmpStr = String.Format( tmpStr, dVal / scaleMult );
				}

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
			
			int		iStart, iEnd;

			// sanity check
			if ( this.min >= this.max )
				return;

			// if the step size is outrageous, then quit
			// (step size not used for log scales)
			if ( !this.isLog )
			{
				if ( this.step <= 0 ||
					( this.max - this.min ) / this.step > 1000 ||
					( ( this.isMinorTic || this.isMinorInsideTic || this.isMinorOppositeTic )
					&& ( this.max - this.minScale ) / this.minorStep > 5000 ) )
					return;
			}

			if ( this.isLog )
			{
				iStart = (int) ( Math.Ceiling( this.minScale - 1.0e-12 ) );
				iEnd = (int) ( Math.Floor( this.maxScale + 1.0e-12 ) );
			}
			else
			{
				iStart = 0;
				iEnd = (int) (( this.maxScale - this.minScale ) / this.step + 0.01);
			}

			Pen pen = new Pen( this.color, this.ticPenWidth  );
			
			// redraw the axis border
			g.DrawLine( pen, 0.0F, 0.0F, rightPix, 0.0F );

			DrawLabels( g, pane, iStart, iEnd, topPix, scaleFactor );
		
			DrawMinorTics( g, iStart, scaleFactor );
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
		/// <param name="iStart">
		/// The starting ordinal for the value labels of this axis.  This value is
		/// always zero for a linear scale, or the starting power of 10 for a log
		/// scale.
		/// </param>
		/// <param name="iEnd">
		/// The ending ordinal for the value labels of this axis.  This value is
		/// always the total number of labels for a linear scale, or the ending
		/// power of 10 for a log scale.
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
		public void DrawLabels( Graphics g, GraphPane pane, int iStart, int iEnd,
						float topPix, double scaleFactor )
		{
			double	dVal;
			float	pixVal;
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
			for ( int i=iStart; i<=iEnd; i++ )
			{
				if ( this.isLog )
					dVal = Math.Pow( (double) 10.0, (double) i );
				else
					dVal = (double) this.minScale + (double) this.step * (double) i;
		
				pixVal = this.LocalTransform( dVal );
		
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

					// draw the label
					if ( isLog )
					{
						if ( i >= -3 && i <= 4 )
							tmpStr = string.Format( "{0}", Math.Pow( 10.0, i ) );
						else
							tmpStr = string.Format( "1e{0}", i );
					}
					else
					{
						tmpStr = "{0:F*}";
						tmpStr = tmpStr.Replace("*", this.numDec.ToString("D") );
						tmpStr = String.Format( tmpStr, dVal / scaleMult );
					}
					
					this.ScaleFontSpec.Draw( g, tmpStr,
						pixVal, 0.0F + textCenter,
						FontAlignH.Center, FontAlignV.Center, scaleFactor );

				}
		
				// draw the grid
				if ( this.isVisible && this.isShowGrid && i > iStart && i < iEnd )
					g.DrawLine( dottedPen, pixVal, 0.0F, pixVal, topPix );
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
		/// <param name="iStart">
		/// The starting ordinal for the value labels of this axis.  This value is
		/// always zero for a linear scale, or the starting power of 10 for a log
		/// scale.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawMinorTics( Graphics g, int iStart, double scaleFactor )
		{
			if ( this.isMinorTic && this.isVisible )
			{
				if ( this.isLog || this.minorStep < this.step )
				{
					float minorScaledTic = this.ScaledMinorTic( scaleFactor );
					int count = 0;

					// Minor tics start at the minimum value and step all the way thru
					// the full scale.  This means that if the minor step size is not
					// an even division of the major step size, the minor tics won't
					// line up with all of the scale labels and major tics.
					double	dVal = this.min;
					float	pixVal;
					Pen		pen = new Pen( this.color, this.ticPenWidth  );
					
					// Draw the minor tic marks for linear scales
					while ( dVal < this.max && count < 5000 )
					{
						if ( this.isLog )
							dVal = Math.Pow( 10.0, (double) iStart +
									(double) ( count / 10 )) * (double) ( count % 10 );
						else
							dVal += this.minorStep;

						if ( dVal > this.min && dVal < this.max )
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
								g.DrawLine( pen, pixVal, 0.0F, pixVal, 0.0F + minorScaledTic );
						}

						count++;  // a paranoid check to avoid infinite loops
					}
				}
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
		
			if ( this.isLog )	// Log Scale
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
			if ( this.isLog )
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

			if ( this.isLog )
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

	}
}
