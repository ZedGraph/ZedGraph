using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZedGraph
{
	
	/// <summary>
	/// Class <see cref="GraphPane"/> encapsulates the graph pane, which is all display elements
	/// associated with an individual graph.  This class is the outside "wrapper"
	/// for the ZedGraph classes, and provides the interface to access the attributes
	/// of the graph.  You can have multiple graphs in the same document or form,
	/// just instantiate multiple GraphPane's.
	/// </summary>
	public class GraphPane
	{
		// Item subclasses
		private XAxis		xAxis;			// A class representing the X axis
		private YAxis		yAxis;			// A class for the left Y axis
		private Y2Axis		y2Axis;			// A class for the right Y axis
		private Legend		legend;			// A class for the graph legend
		private CurveList	curveList;		// A collection class for the curves on the graph
		private TextList	textList;		// A collection class for user text items on the graph
		private ArrowList	arrowList;		// A collection class for lines/arrows on the graph
		
		// Pane Title Properties
		private string		title;			// The main title of the graph
		private bool		isShowTitle;	// true to show the title
		private FontSpec	fontSpec;		// Describes the title font characteristics
		
		// Pane Frame Properties
		private bool		isPaneFramed;	// True if the GraphPane has a frame border
		private Color		paneFrameColor;		// Color of the pane frame border
		private float		paneFramePenWidth;		// Width of the pane frame border
		private Color		paneBackColor;			// Color of the background behind paneRect
		
		// Pane Frame Properties
		private bool		isAxisFramed;	// True if the GraphPane has a frame border
		private Color		axisFrameColor;		// Color of the axis frame border
		private float		axisFramePenWidth;		// Width of the axis frame border
		private Color		axisBackColor;			// Color of the background behind axisRect
		
		private bool		isIgnoreInitial;	// true to ignore initial zero values for auto scale selection
		private float		paneGap;			// Size of the gap (margin) around the edges of the pane
		private double		baseDimension;		// Basic length scale (inches) of the plot for scaling features

		/// <summary>
		/// The rectangle that defines the full area into which the
		/// graph can be rendered.  Units are pixels.
		/// </summary>
		public RectangleF	paneRect;			// The full area of the graph pane
		/// <summary>
		/// The rectangle that contains the area bounded by the axes, in
		/// pixel units
		/// </summary>
		public RectangleF	axisRect;			// The area of the pane defined by the axes
		
		/// <summary>
		/// Constructor for the <see cref="GraphPane"/> object.  This routine will
		/// initialize all member variables and classes, setting appropriate default
		/// values as defined in the <see cref="Def"/> class.
		/// </summary>
		/// <param name="paneRect"> A rectangular screen area where the graph is to be displayed.
		/// This area can be any size, and can be resize at any time using the
		/// <see cref="PaneRect"/> property.
		/// </param>
		/// <param name="paneTitle">The <see cref="Axis.Title"/> for this <see cref="GraphPane"/></param>
		/// <param name="xTitle">The <see cref="Axis.Title"/> for the <see cref="XAxis"/></param>
		/// <param name="yTitle">The <see cref="Axis.Title"/> for the <see cref="YAxis"/></param>
		public GraphPane( RectangleF paneRect, string paneTitle,
			string xTitle, string yTitle )
		{
			this.paneRect = paneRect;
			
			xAxis = new XAxis( xTitle );
			yAxis = new YAxis( yTitle );
			y2Axis = new Y2Axis( "" );
			legend = new Legend();
			curveList = new CurveList();
			textList = new TextList();
			arrowList = new ArrowList();
			
			this.title = paneTitle;
			this.isShowTitle = Def.Pane.ShowTitle;
			this.fontSpec = new FontSpec( Def.Pane.FontFamily,
				Def.Pane.FontSize, Def.Pane.FontColor, Def.Pane.FontBold,
				Def.Pane.FontItalic, Def.Pane.FontUnderline );
			this.fontSpec.IsFilled = false;
			this.fontSpec.IsFramed = false;
					
			this.isIgnoreInitial = Def.Ax.IgnoreInitial;
			
			this.isPaneFramed = Def.Pane.IsFramed;
			this.paneFrameColor = Def.Pane.FrameColor;
			this.paneFramePenWidth = Def.Pane.FramePenWidth;
			this.paneBackColor = Def.Pane.BackColor;

			this.isAxisFramed = Def.Ax.IsFramed;
			this.axisFrameColor = Def.Ax.FrameColor;
			this.axisFramePenWidth = Def.Ax.FramePenWidth;
			this.axisBackColor = Def.Ax.BackColor;

			this.baseDimension = Def.Pane.BaseDimension;
			this.paneGap = Def.Pane.Gap;
		}

		/// <summary>
		/// Gets or sets the rectangle that defines the full area into which the
		/// <see cref="GraphPane"/> can be rendered.
		/// </summary>
		/// <value>The rectangle units are in screen pixels</value>
		public RectangleF PaneRect
		{
			get { return paneRect; }
			set { paneRect = value; }
		}
		/// <summary>
		/// Gets the rectangle that contains the area bounded by the axes
		/// (<see cref="XAxis"/>, <see cref="YAxis"/>, and <see cref="Y2Axis"/>)
		/// </summary>
		/// <value>The rectangle units are in screen pixels</value>
		public RectangleF AxisRect
		{
			get { return axisRect; }
		}
		/// <summary>
		/// Accesses the list of <see cref="ArrowItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to an <see cref="ArrowList"/> collection object</value>
		public ArrowList ArrowList
		{
			get { return arrowList; }
		}
		/// <summary>
		/// Accesses the list of <see cref="TextItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="TextList"/> collection object</value>
		public TextList TextList
		{
			get { return textList; }
		}
		/// <summary>
		/// Accesses the list of <see cref="CurveItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="CurveList"/> collection object</value>
		public CurveList CurveList
		{
			get { return curveList; }
		}
		/// <summary>
		/// Accesses the <see cref="Legend"/> for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="Legend"/> object</value>
		public Legend Legend
		{
			get { return legend; }
		}
		/// <summary>
		/// Accesses the <see cref="XAxis"/> for this graph
		/// </summary>
		/// <value>A reference to a <see cref="XAxis"/> object</value>
		public XAxis XAxis
		{
			get { return xAxis; }
		}
		/// <summary>
		/// Accesses the <see cref="YAxis"/> for this graph
		/// </summary>
		/// <value>A reference to a <see cref="YAxis"/> object</value>
		public YAxis YAxis
		{
			get { return yAxis; }
		}
		/// <summary>
		/// Accesses the <see cref="Y2Axis"/> for this graph
		/// </summary>
		/// <value>A reference to a <see cref="Y2Axis"/> object</value>
		public Y2Axis Y2Axis
		{
			get { return y2Axis; }
		}

		/// <summary>
		/// A boolean value that affects the data range that is considered
		/// for the automatic scale ranging.  If true, then initial data points where the Y value
		/// is zero are not included when automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.
		/// All data after the first non-zero Y value are included.
		/// </summary>
		public bool IsIgnoreInitial
		{
			get { return isIgnoreInitial; }
			set { isIgnoreInitial = value; }
		}
		/// <summary>
		/// IsShowTitle is a boolean value that determines whether or not the pane title is displayed
		/// on the graph.  If true, the title is displayed.  If false, the title is omitted, and the
		/// screen space that would be occupied by the title is added to the axis area.
		/// </summary>
		public bool IsShowTitle
		{
			get { return isShowTitle; }
			set { isShowTitle = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="FontSpec"/> class used to render
		/// the <see cref="GraphPane"/> <see cref="Title"/>
		/// </summary>
		public FontSpec FontSpec
		{
			get { return fontSpec; }
		}
		/// <summary>
		/// Title is a string representing the pane title text.  This text can be multiple lines,
		/// separated by newline characters ('\n').
		/// </summary>
		/// <seealso cref="FontSpec"/>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}
		/// <summary>
		/// IsShowPaneFrame is a boolean value that determines whether or not a frame border is drawn
		/// around the <see cref="GraphPane"/> area (<see cref="PaneRect"/>).
		/// True to draw the frame, false otherwise.
		/// </summary>
		public bool IsPaneFramed
		{
			get { return isPaneFramed; }
			set { isPaneFramed = value; }
		}
		/// <summary>
		/// Frame color is a <see cref="System.Drawing.Color"/> specification
		/// for the <see cref="GraphPane"/> frame border.
		/// </summary>
		public Color PaneFrameColor
		{
			get { return paneFrameColor; }
			set { paneFrameColor = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.Color"/> specification
		/// for the <see cref="GraphPane"/> pane background, which is the
		/// area behind the <see cref="GraphPane.PaneRect"/>.
		/// </summary>
		public Color PaneBackColor
		{
			get { return paneBackColor; }
			set { paneBackColor = value; }
		}
		/// <summary>
		/// FrameWidth is a float value indicating the width (thickness) of the
		/// <see cref="GraphPane"/> frame border.
		/// </summary>
		public float PaneFramePenWidth
		{
			get { return paneFramePenWidth; }
			set { paneFramePenWidth = value; }
		}
		/// <summary>
		/// IsAxisFramed is a boolean value that determines whether or not a frame border is drawn
		/// around the axis area (<see cref="AxisRect"/>).
		/// </summary>
		/// <value>true to draw the frame, false otherwise. </value>
		public bool IsAxisFramed
		{
			get { return isAxisFramed; }
			set { isAxisFramed = value; }
		}
		/// <summary>
		/// Frame color is a <see cref="System.Drawing.Color"/> specification
		/// for the axis frame border.
		/// </summary>
		public Color AxisFrameColor
		{
			get { return axisFrameColor; }
			set { axisFrameColor = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.Color"/> specification
		/// for the <see cref="Axis"/> background, which is the
		/// area behind the <see cref="GraphPane.AxisRect"/>.
		/// </summary>
		public Color AxisBackColor
		{
			get { return axisBackColor; }
			set { axisBackColor = value; }
		}
		/// <summary>
		/// FrameWidth is a float value indicating the width (thickness) of the axis frame border.
		/// </summary>
		/// <value>A pen width dimension in pixel units</value>
		public float AxisFramePenWidth
		{
			get { return axisFramePenWidth; }
			set { axisFramePenWidth = value; }
		}
		/// <summary>
		/// PaneGap is a float value that sets the margin area between the edge of the
		/// <see cref="GraphPane"/> rectangle (<see cref="PaneRect"/>)
		/// and the features of the graph.
		/// </summary>
		/// <value>This value is in units of pixels, and is scaled
		/// linearly with the graph size.</value>
		public float PaneGap
		{
			get { return paneGap; }
			set { paneGap = value; }
		}
		/// <summary>
		/// BaseDimension is a double precision value that sets "normal" plot size on
		/// which all the settings are based.  The BaseDimension is in inches.  For
		/// example, if the BaseDimension is 8.0 inches and the <see cref="GraphPane"/>
		/// <see cref="Title"/> size is 14 points.  Then the pane title font
		/// will be 14 points high when the <see cref="PaneRect"/> is approximately 8.0
		/// inches wide.  If the PaneRect is 4.0 inches wide, the pane title font will be
		/// 7 points high.  Most features of the graph are scaled in this manner.
		/// </summary>
		/// <value>The base dimension reference for the <see cref="GraphPane"/>, in inches</value>
		public double BaseDimension
		{
			get { return baseDimension; }
			set { baseDimension = value; }
		}
		/// <summary>
		/// ScaledGap is a simple utility routine that calculates the <see cref="PaneGap"/> scaled
		/// to the "scaleFactor" fraction.  That is, ScaledGap = PaneGap * scaleFactor
		/// </summary>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		public float ScaledGap( double scaleFactor )
		{
			return (float) ( this.paneGap * scaleFactor );
		}
		
		/// <summary>
		/// AxisChange causes the axes scale ranges to be recalculated based on the current data range.
		/// Call this function anytime you change, add, or remove curve data.  This routine calculates
		/// a scale minimum, maximum, and step size for each axis based on the current curve data.
		/// Only the axis attributes (min, max, step) that are set to auto-range (<see cref="Axis.MinAuto"/>,
		/// <see cref="Axis.MaxAuto"/>, <see cref="Axis.StepAuto"/>) will be modified.  You must call
		/// Invalidate() after calling AxisChange to make sure the display gets updated.
		/// </summary>
		public void AxisChange()
		{
			double	xMin, xMax, yMin, yMax, y2Min, y2Max;
		
			// Get the scale range of the data (all curves)
			this.curveList.GetRange( out xMin, out xMax, out yMin,
									out yMax, out y2Min, out y2Max,
									this.isIgnoreInitial );
		
			// Pick new scales based on the range
			this.xAxis.PickScale( xMin, xMax );
			this.yAxis.PickScale( yMin, yMax );
			this.y2Axis.PickScale( y2Min, y2Max );
		}
		
		/// <summary>
		/// Draw all elements in the <see cref="GraphPane"/> to the specified graphics device.  This routine
		/// should be part of the Paint() update process.  Calling this routine will redraw all
		/// features of the graph.  No preparation is required other than an instantiated
		/// <see cref="GraphPane"/> object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void Draw( Graphics g )
		{
			// calculate scaleFactor on "normal" pane size (BaseDimension)
			double scaleFactor = this.CalcScaleFactor( g );
			
			// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
			int		hStack;
			float	legendWidth;

			this.CalcAxisRect( g, scaleFactor, out hStack, out legendWidth );
			
			// Frame the whole pane
			DrawPaneFrame( g );

			// Frame the axis itself
			DrawAxisFrame( g );

			// Draw the graph features only if there is at least one curve with data
//			if (	this.curveList.HasData() &&
			// Go ahead and draw the graph, even without data.  This makes the control
			// version still look like a graph before it is fully set up
			if ( 	this.xAxis.Min < this.xAxis.Max &&
					this.yAxis.Min < this.yAxis.Max &&
					this.y2Axis.Min < this.y2Axis.Max )
			{
				// Clip everything to the paneRect
				g.SetClip( this.paneRect );

				// Draw the Pane Title
				DrawTitle( g, scaleFactor );
			
				// Draw the Axes
				this.xAxis.Draw( g, this, scaleFactor );
				this.yAxis.Draw( g, this, scaleFactor );
				this.y2Axis.Draw( g, this, scaleFactor );
				
				// Clip the points to the actual plot area
				g.SetClip( this.axisRect );
				this.curveList.Draw( g, this, scaleFactor );
				g.SetClip( this.paneRect );
				
				// Draw the Legend
				this.legend.Draw( g, this, scaleFactor, hStack, legendWidth );
				
				// Draw the Text Items
				this.textList.Draw( g, this, scaleFactor );
				
				// Draw the Arrows
				this.arrowList.Draw( g, this, scaleFactor );

				// Reset the clipping
				g.ResetClip();
			}
		}

		/// <summary>
		/// Calculate the <see cref="AxisRect"/> based on the <see cref="PaneRect"/>.  The axisRect
		/// is the plot area bounded by the axes, and the paneRect is the total area as
		/// specified by the client application.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="hStack">
		/// The number of legend columns to use for horizontal legend stacking.  This is a temporary
		/// variable calculated by the routine for use in the Legend.Draw method.
		/// </param>
		/// <param name="legendWidth">
		/// The wide of a single legend entry, in pixel units.  This is a temporary
		/// variable calculated by the routine for use in the Legend.Draw method.
		/// </param>
		public void CalcAxisRect( Graphics g, double scaleFactor,
									out int hStack, out float legendWidth )
		{
			// get scaled values for the paneGap and character height
			float gap = this.ScaledGap( scaleFactor );
			float charHeight = this.FontSpec.GetHeight( scaleFactor );
				
			// Axis rect starts out at the full pane rect.  It gets reduced to make room for the legend,
			// scales, titles, etc.
			this.axisRect = this.paneRect;

			// Calculate the areas required for the X, Y, and Y2 axes, and reduce the AxisRect by
			// these amounts.
//			this.xAxis.CalcRect( g, this, scaleFactor );
//			this.yAxis.CalcRect( g, this, scaleFactor );
//			this.y2Axis.CalcRect( g, this, scaleFactor );

			float space = this.xAxis.CalcSpace( g, this, scaleFactor );
			this.axisRect.Height -= space;
			space = this.yAxis.CalcSpace( g, this, scaleFactor );
			this.axisRect.X += space;
			this.axisRect.Width -= space;
			space = this.y2Axis.CalcSpace( g, this, scaleFactor );
			this.axisRect.Width -= space;
	
			// Always leave a gap on top, even with no title
			this.axisRect.Y += gap;
			this.axisRect.Height -= gap;

			// Leave room for the pane title
			if ( this.isShowTitle )
			{
				SizeF titleSize = this.FontSpec.MeasureString( g, this.title, scaleFactor );
				// Leave room for the title height, plus a line spacing of charHeight/2
				this.axisRect.Y += titleSize.Height + charHeight / 2.0F;
				this.axisRect.Height -= titleSize.Height + charHeight / 2.0F;
			}
			
			// Calculate the legend rect, and back it out of the current axisRect
			this.legend.CalcRect( g, this, scaleFactor, ref this.axisRect,
								out hStack, out legendWidth );
		}

		/// <summary>
		/// Draw the <see cref="GraphPane"/> <see cref="Title"/> on the graph
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>		
		public void DrawTitle( Graphics g, double scaleFactor )
		{	
			// only draw the title if it's required
			if ( this.isShowTitle )
			{
				// use the internal fontSpec class to draw the text using user-specified and/or
				// default attributes.
				this.FontSpec.Draw( g, this.title,
							( this.paneRect.Left + this.paneRect.Right ) / 2,
							this.paneRect.Top + this.ScaledGap( scaleFactor ),
							FontAlignH.Center, FontAlignV.Top, scaleFactor );
			}
		}
		
		/// <summary>
		/// Draw the frame border around the <see cref="PaneRect"/> area.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void DrawPaneFrame( Graphics g )
		{
			// Erase the pane background
			SolidBrush brush = new SolidBrush( this.paneBackColor );
			g.FillRectangle( brush, this.paneRect );

			RectangleF tempRect = this.paneRect;
			tempRect.Width -= 1;
			tempRect.Height -= 1;

			if ( this.isPaneFramed )
			{
				Pen pen = new Pen( this.paneFrameColor, this.paneFramePenWidth );
				g.DrawRectangle( pen, Rectangle.Round( tempRect ) );
				
				// FrameRect draws one pixel short of the bottom/right border, so
				//  just draw it manually
				//				g.DrawLine( pen, rect.Left, rect.Bottom,
				//									rect.Left, rect.Top );
				//				g.DrawLine( pen, rect.Left, rect.Top,
				//									rect.Right, rect.Top );
				//				g.DrawLine( pen, rect.Right, rect.Top,
				//									rect.Right, rect.Bottom );
				//				g.DrawLine( pen, rect.Right, rect.Bottom,
				//									rect.Left, rect.Bottom );
			}
		}

		/// <summary>
		/// Draw the frame border around the <see cref="AxisRect"/> area.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void DrawAxisFrame( Graphics g )
		{
			// Erase the axis background
			SolidBrush brush = new SolidBrush( this.axisBackColor );
			g.FillRectangle( brush, this.axisRect );

			if ( this.isAxisFramed )
			{
				Pen pen = new Pen( this.axisFrameColor, this.axisFramePenWidth );
				g.DrawRectangle( pen, Rectangle.Round( this.axisRect ) );
			}
		}

		/// <summary>
		/// Calculate the scaling factor based on the ratio of the current <see cref="PaneRect"/> dimensions and
		/// the <see cref="BaseDimension"/>.  This scaling factor is used to proportionally scale the
		/// features of the <see cref="GraphPane"/> so that small graphs don't have huge fonts, and vice versa.
		/// The scale factor represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <return>
		/// A double precision value representing the scaling factor to use for the rendering calculations.
		/// </return>
		protected double CalcScaleFactor( Graphics g )
		{
			double scaleFactor;
			const double ASPECTLIMIT = 2.0;
			
			// Assume the standard width (BaseDimension) is 8.0 inches
			// Therefore, if the paneRect is 8.0 inches wide, then the fonts will be scaled at 1.0
			// if the paneRect is 4.0 inches wide, the fonts will be half-sized.
			// if the paneRect is 16.0 inches wide, the fonts will be double-sized.
		
			// determine the size of the paneRect in inches
			double xInch = (double) this.paneRect.Width / (double) g.DpiX;
			double yInch = (double) this.paneRect.Height / (double) g.DpiY;
					
			// Limit the aspect ratio so long plots don't have outrageous font sizes
			double aspect = (double) xInch / (double) yInch;
			if ( aspect > ASPECTLIMIT )
				xInch = yInch * ASPECTLIMIT;
		
			// Scale the size depending on the client area width in linear fashion
			scaleFactor = (double) xInch / this.baseDimension;
		
			// Don't let the scaleFactor get ridiculous
			if ( scaleFactor < 0.1 )
				scaleFactor = 0.1;
						
			return scaleFactor;
		}

		/// <summary>
		/// Add a curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="x">An array of double precision X values (the
		/// independent values) that define the curve.</param>
		/// <param name="y">An array of double precision Y values (the
		/// dependent values) that define the curve.</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <param name="symbolType">A symbol type (<see cref="SymbolType"/>)
		/// that will be used for this curve.</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the <see cref="AddCurve"/> method.</returns>
		public CurveItem AddCurve( string label, double[] x, double[] y,
								Color color, SymbolType symbolType )
		{
			CurveItem curve = new CurveItem( label, x, y );
			curve.Line.Color = color;
			curve.Symbol.Color = color;
			curve.Symbol.Type = symbolType;
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Transform a data point from the specified coordinate type
		/// (<see cref="CoordType"/>) to screen coordinates (pixels).
		/// </summary>
		/// <param name="ptF">The X,Y pair that defines the point in user
		/// coordinates.</param>
		/// <param name="coord">A <see cref="CoordType"/> type that defines the
		/// coordinate system in which the X,Y pair is defined.</param>
		/// <returns>A point in screen coordinates that corresponds to the
		/// specified user point.</returns>
		public PointF GeneralTransform( PointF ptF, CoordType coord )
		{
			PointF ptPix = new PointF();

			if ( coord == CoordType.AxisFraction )
			{
				ptPix.X = this.axisRect.Left + ptF.X * this.axisRect.Width;
				ptPix.Y = this.axisRect.Top + ptF.Y * this.axisRect.Height;
			}
			else if ( coord == CoordType.AxisXYScale )
			{
				ptPix.X = this.xAxis.Transform( ptF.X );
				ptPix.Y = this.yAxis.Transform( ptF.Y );
			}
			else if ( coord == CoordType.AxisXY2Scale )
			{
				ptPix.X = this.xAxis.Transform( ptF.X );
				ptPix.Y = this.y2Axis.Transform( ptF.Y );
			}
			else	// PaneFraction
			{
				ptPix.X = this.paneRect.Left + ptF.X * this.paneRect.Width;
				ptPix.Y = this.paneRect.Top + ptF.Y * this.paneRect.Height;
			}

			return ptPix;
		}
	}
}

