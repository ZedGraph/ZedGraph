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
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ZedGraph
{
	// <summary>
	// <c>ZedGraph</c> is a class library and UserControl (<see cref="ZedGraphControl"/>) that display
	// 2D line graphs of user specified data.  The <c>ZedGraph</c> namespace includes all functionality
	// required to draw, modify, and update the graph.
	// </summary>
	
	/// <summary>
	/// Class <see cref="GraphPane"/> encapsulates the graph pane, which is all display elements
	/// associated with an individual graph.  This class is the outside "wrapper"
	/// for the ZedGraph classes, and provides the interface to access the attributes
	/// of the graph.  You can have multiple graphs in the same document or form,
	/// just instantiate multiple GraphPane's.
	/// </summary>
	/// 
	/// <author> John Champion modified by Jerry Vos </author>
	/// <version> $Revision: 1.13 $ $Date: 2004-08-27 06:50:10 $ </version>
	public class GraphPane : ICloneable
	{
	#region Private Fields
	
		// Item subclasses ////////////////////////////////////////////////////////////////////
		
		/// <summary>Private field instance of the <see cref="ZedGraph.XAxis"/> class.  Use the
		/// public property <see cref="GraphPane.XAxis"/> to access this class.</summary>
		private XAxis		xAxis;
		/// <summary>Private field instance of the <see cref="ZedGraph.YAxis"/> class.  Use the
		/// public property <see cref="GraphPane.YAxis"/> to access this class.</summary>
		private YAxis		yAxis;
		/// <summary>Private field instance of the <see cref="ZedGraph.Y2Axis"/> class.  Use the
		/// public property <see cref="GraphPane.Y2Axis"/> to access this class.</summary>
		private Y2Axis		y2Axis;
		/// <summary>Private field instance of the <see cref="ZedGraph.Legend"/> class.  Use the
		/// public property <see cref="GraphPane.Legend"/> to access this class.</summary>
		private Legend		legend;
		/// <summary>Private field instance of the <see cref="ZedGraph.CurveList"/> class.  Use the
		/// public property <see cref="GraphPane.CurveList"/> to access this class.</summary>
		private CurveList	curveList;
		/// <summary>Private field instance of the <see cref="ZedGraph.TextList"/>t class.  Use the
		/// public property <see cref="GraphPane.TextList"/> to access this class.</summary>
		private TextList	textList;
		/// <summary>Private field instance of the <see cref="ZedGraph.ArrowList"/> class.  Use the
		/// public property <see cref="GraphPane.ArrowList"/> to access this class.</summary>
		private ArrowList	arrowList;
		
		// Pane Title Properties /////////////////////////////////////////////////////////////
		
		/// <summary>Private field that holds the main title of the graph.  Use the
		/// public property <see cref="GraphPane.Title"/> to access this value.</summary>
		private string		title;
		/// <summary>Private field that determines whether or not the graph main title
		/// will be drawn.  Use the
		/// public property <see cref="GraphPane.IsShowTitle"/> to access this value.</summary>
		private bool		isShowTitle;
		/// <summary>
		/// Private field instance of the <see cref="FontSpec"/> class, which maintains the font attributes
		/// for the main graph <see cref="Title"/>. Use the public property
		/// <see cref="FontSpec"/> to access this class.
		/// </summary>
		private FontSpec	fontSpec;
		
		// Pane Frame Properties ///////////////////////////////////////////////////////////////
		
		/// <summary>Private field that determines whether the <see cref="PaneRect"/> has
		/// a frame border.  Use the public property <see cref="IsPaneFramed"/> to access
		/// this value. </summary>
		private bool		isPaneFramed;
		/// <summary>Private field that determines the color of the <see cref="PaneRect"/>
		/// frame border.  Use the public property <see cref="PaneFrameColor"/> to access
		/// this value. </summary>
		/// <seealso cref="isPaneFramed"/>
		private Color		paneFrameColor;
		/// <summary>Private field that determines the width (pixels) of the <see cref="PaneRect"/>
		/// frame border.  Use the public property <see cref="PaneFramePenWidth"/> to access
		/// this value. </summary>
		/// <seealso cref="isPaneFramed"/>
		private float		paneFramePenWidth;
		/// <summary>Private field that determines the color of the <see cref="PaneRect"/>
		/// background fill.  Use the public property <see cref="PaneBackColor"/> to access
		/// this value. </summary>
		/// <seealso cref="isPaneFramed"/>
		private Color		paneBackColor;
		
		// Axis Frame Properties //////////////////////////////////////////////////////////////
		
		/// <summary>Private field that determines if the <see cref="AxisRect"/> will be
		/// sized automatically.  Use the public property <see cref="IsAxisRectAuto"/> to access
		/// this value. </summary>
		private bool		isAxisRectAuto;
		/// <summary>Private field that determines if the <see cref="AxisRect"/> will have a
		/// frame border.  Use the public property <see cref="IsAxisFramed"/> to access
		/// this value. </summary>
		private bool		isAxisFramed;
		/// <summary>Private field that determines the color of the <see cref="AxisRect"/>
		/// frame border.  Use the public property <see cref="AxisFrameColor"/> to access
		/// this value. </summary>
		/// <seealso cref="isAxisFramed"/>
		private Color		axisFrameColor;
		/// <summary>Private field that determines the width of the <see cref="AxisRect"/>
		/// frame border.  Use the public property <see cref="AxisFramePenWidth"/> to access
		/// this value. </summary>
		/// <seealso cref="isAxisFramed"/>
		private float		axisFramePenWidth;
		/// <summary>Private field that determines the color of the <see cref="AxisRect"/>
		/// background fill.  Use the public property <see cref="AxisBackColor"/> to access
		/// this value. </summary>
		/// <seealso cref="isAxisFramed"/>
		private Color		axisBackColor;
		
		/// <summary>Private field that determines whether or not initial zero values will
		/// be included or excluded when determining the Y or Y2 axis scale range.
		/// Use the public property <see cref="IsIgnoreInitial"/> to access
		/// this value. </summary>
		private bool		isIgnoreInitial;
		/// <summary>Private field that determines whether or not initial
		/// <see cref="PointPair.Missing"/> values will cause the line segments of
		/// a curve to be discontinuous.  If this field is true, then the curves
		/// will be plotted as continuous lines as if the Missing values did not
		/// exist.
		/// Use the public property <see cref="IsIgnoreMissing"/> to access
		/// this value. </summary>
		private bool		isIgnoreMissing;
		/// <summary>Private field that determines the size of the gap (margin) around the
		/// edges of the pane, in pixel units.  This value is scaled according to the graph size.
		/// Use the public property <see cref="PaneGap"/> to access this value. </summary>
		/// <seealso cref="isFontsScaled"/>
		/// <seealso cref="GraphPane.CalcScaleFactor"/>
		private float		paneGap;
		/// <summary>Private field that determines the base size of the graph, in inches.
		/// Fonts, tics, gaps, etc. are scaled according to this base size.
		/// Use the public property <see cref="BaseDimension"/> to access this value. </summary>
		/// <seealso cref="isFontsScaled"/>
		/// <seealso cref="GraphPane.CalcScaleFactor"/>
		private double		baseDimension;
		/// <summary>Private field that determines the dots per inch basis for the
		/// <see cref="PaneRect"/>.  This is typically used for printing opertions.
		/// Leave the value set to zero for normal conditions (i.e., the output device
		/// resolution matches the definition of the PaneRect).
		/// Use the public property <see cref="BaseDPI"/> to access this value. </summary>
		/// <seealso cref="isFontsScaled"/>
		/// <seealso cref="GraphPane.CalcScaleFactor"/>
		private double		baseDPI;
		/// <summary>Private field that determines the size of the gap between bar clusters
		/// for bar charts.  This gap is expressed as a fraction of the bar size (1.0 means
		/// leave a 1-barwidth gap between clusters).
		/// Use the public property <see cref="MinClusterGap"/> to access this value. </summary>
		private float		minClusterGap;
		/// <summary>Private field that determines the size of the gap between individual bars
		/// within a bar cluster for bar charts.  This gap is expressed as a fraction of the
		/// bar size (1.0 means leave a 1-barwidth gap between each bar).
		/// Use the public property <see cref="MinBarGap"/> to access this value. </summary>
		private float		minBarGap;
		/// <summary>Private field that determines the width of a bar cluster (for bar charts)
		/// in user scale units.  Normally, this value is 1.0 because bar charts are typically
		/// <see cref="AxisType.Ordinal"/> or <see cref="AxisType.Text"/>, and the bars are
		/// defined at ordinal values (1.0 scale units apart).  For <see cref="AxisType.Linear"/>
		/// or other scale types, you can use this value to scale the bars to an arbitrary
		/// user scale. Use the public property <see cref="ClusterScaleWidth"/> to access this
		/// value. </summary>
		private double		clusterScaleWidth;
		/// <summary>Private field that determines whether or not the fonts, tics, gaps, etc.
		/// will be scaled according to the actual graph size.  true for font and feature scaling
		/// with graph size, false for fixed font sizes (scaleFactor = 1.0 constant).
		/// Use the public property <see cref="IsFontsScaled"/> to access this value. </summary>
		/// <seealso cref="CalcScaleFactor"/>
		private bool		isFontsScaled;
		
		/// <summary>
		/// The rectangle that defines the full area into which the
		/// graph can be rendered.  Units are pixels.
		/// </summary>
		private RectangleF	paneRect;			// The full area of the graph pane
		/// <summary>
		/// The rectangle that contains the area bounded by the axes, in
		/// pixel units
		/// </summary>
		private RectangleF	axisRect;			// The area of the pane defined by the axes
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="GraphPane"/> class.
		/// </summary>
		public struct Default
		{
			// Default GraphPane properties
			/// <summary>
			/// The default display mode for the title at the top of the pane
			/// (<see cref="GraphPane.IsShowTitle"/> property).  true to
			/// display a title, false otherwise.
			/// </summary>
			public static bool IsShowTitle = true;
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
			public static bool IsPaneFramed = true;
			/// <summary>
			/// The default color for the <see cref="GraphPane"/> frame border.
			/// (<see cref="GraphPane.PaneFrameColor"/> property). 
			/// </summary>
			public static Color PaneFrameColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="GraphPane.PaneRect"/> background.
			/// (<see cref="GraphPane.PaneBackColor"/> property). 
			/// </summary>
			public static Color PaneBackColor = Color.White;
			/// <summary>
			/// The default pen width for the <see cref="GraphPane"/> frame border.
			/// (<see cref="GraphPane.PaneFramePenWidth"/> property).  Units are in pixels.
			/// </summary>
			public static float PaneFramePenWidth = 1;

			/// <summary>
			/// The default color for the <see cref="Axis"/> frame border.
			/// (<see cref="GraphPane.AxisFrameColor"/> property). 
			/// </summary>
			public static Color AxisFrameColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="GraphPane.AxisRect"/> background.
			/// (<see cref="GraphPane.AxisBackColor"/> property). 
			/// </summary>
			public static Color AxisBackColor = Color.White;
			/// <summary>
			/// The default pen width for drawing the 
			/// <see cref="GraphPane.AxisRect"/> frame border
			/// (<see cref="GraphPane.AxisFramePenWidth"/> property).
			/// Units are in pixels.
			/// </summary>
			public static float AxisFramePenWidth = 1F;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> frame border
			/// (<see cref="GraphPane.IsAxisFramed"/> property). true
			/// to show the frame border, false to omit the border
			/// </summary>
			public static bool IsAxisFramed = true;

			/// <summary>
			/// The default settings for the <see cref="Axis"/> scale ignore initial
			/// zero values option (<see cref="GraphPane.IsIgnoreInitial"/> property).
			/// true to have the auto-scale-range code ignore the initial data points
			/// until the first non-zero Y value, false otherwise.
			/// </summary>
			public static bool IsIgnoreInitial = false;

			/// <summary>
			/// The default value for the <see cref="GraphPane.PaneGap"/> property.
			/// This is the size of the margin around the edge of the
			/// <see cref="GraphPane.PaneRect"/>, in units of pixels.
			/// </summary>
			public static float PaneGap = 20;
			/// <summary>
			/// The default dimension of the <see cref="GraphPane.PaneRect"/>, which
			/// defines a normal sized plot.  This dimension is used to scale the
			/// fonts, symbols, etc. according to the actual size of the
			/// <see cref="GraphPane.PaneRect"/>.
			/// </summary>
			/// <seealso cref="GraphPane.CalcScaleFactor"/>
			public static double BaseDimension = 8.0;
			/// <summary>
			/// The default dimension gap between clusters of bars on a
			/// <see cref="Bar"/> graph.
			/// This dimension is expressed in terms of the normal bar width.
			/// </summary>
			/// <seealso cref="Default.MinBarGap"/>
			/// <seealso cref="GraphPane.MinClusterGap"/>
			public static float MinClusterGap = 1.0F;
			/// <summary>
			/// The default dimension gap between each individual bar within a bar cluster
			/// on a <see cref="Bar"/> graph.
			/// This dimension is expressed in terms of the normal bar width.
			/// </summary>
			/// <seealso cref="Default.MinClusterGap"/>
			/// <seealso cref="GraphPane.MinBarGap"/>
			public static float MinBarGap = 0.2F;
			/// <summary>
			/// The default width of a bar cluster 
			/// on a <see cref="Bar"/> graph.  This value only applies to
			/// <see cref="Bar"/> graphs, and only when the
			/// <see cref="Axis.Type"/> is <see cref="AxisType.Linear"/>,
			/// <see cref="AxisType.Log"/> or <see cref="AxisType.Date"/>.
			/// This dimension is expressed in terms of X scale user units.
			/// </summary>
			/// <seealso cref="Default.MinClusterGap"/>
			/// <seealso cref="GraphPane.MinBarGap"/>
			public static double ClusterScaleWidth = 1.0;
			/// <summary>
			/// The tolerance that is applied to the <see cref="GraphPane.FindNearestPoint"/> routine.
			/// If a given curve point is within this many pixels of the mousePt, the curve
			/// point is considered to be close enough for selection as a nearest point
			/// candidate.
			/// </summary>
			public static double NearestTol = 7.0;
		}
	#endregion

	#region public Class Instance Properties
		/// <summary>
		/// Gets or sets the list of <see cref="ArrowItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to an <see cref="ArrowList"/> collection object</value>
		public ArrowList ArrowList
		{
			get { return arrowList; }
			set { arrowList = value; }
		}
		/// <summary>
		/// Gets or sets the list of <see cref="TextItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="TextList"/> collection object</value>
		public TextList TextList
		{
			get { return textList; }
			set { textList = value; }
		}
		/// <summary>
		/// Gets or sets the list of <see cref="CurveItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="CurveList"/> collection object</value>
		public CurveList CurveList
		{
			get { return curveList; }
			set { curveList = value; }
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
	#endregion
	
	#region General Properties
		/// <summary>
		/// A boolean value that affects the data range that is considered
		/// for the automatic scale ranging.  If true, then initial data points where the Y value
		/// is zero are not included when automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.
		/// All data after the first non-zero Y value are included.
		/// </summary>
		/// <seealso cref="Default.IsIgnoreInitial"/>
		public bool IsIgnoreInitial
		{
			get { return isIgnoreInitial; }
			set { isIgnoreInitial = value; }
		}
		/// <summary>Private field that determines whether or not initial
		/// <see cref="PointPair.Missing"/> values will cause the line segments of
		/// a curve to be discontinuous.  If this field is true, then the curves
		/// will be plotted as continuous lines as if the Missing values did not
		/// exist.
		/// Use the public property <see cref="IsIgnoreMissing"/> to access
		/// this value. </summary>
		public bool IsIgnoreMissing
		{
			get { return isIgnoreMissing; }
			set { isIgnoreMissing = value; }
		}
		/// <summary>
		/// IsShowTitle is a boolean value that determines whether or not the pane title is displayed
		/// on the graph.  If true, the title is displayed.  If false, the title is omitted, and the
		/// screen space that would be occupied by the title is added to the axis area.
		/// </summary>
		/// <seealso cref="Default.IsShowTitle"/>
		public bool IsShowTitle
		{
			get { return isShowTitle; }
			set { isShowTitle = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="FontSpec"/> class used to render
		/// the <see cref="GraphPane"/> <see cref="Title"/>
		/// </summary>
		/// <seealso cref="Default.FontColor"/>
		/// <seealso cref="Default.FontBold"/>
		/// <seealso cref="Default.FontItalic"/>
		/// <seealso cref="Default.FontUnderline"/>
		/// <seealso cref="Default.FontFamily"/>
		/// <seealso cref="Default.FontSize"/>
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
		/// Gets the graph pane's current image.
		/// <seealso cref="Bitmap"/>
		/// </summary>
		public Bitmap Image
		{
			get
			{
				Bitmap bitmap = new Bitmap( (int) this.paneRect.Width, (int) this.paneRect.Height );
				Graphics bitmapGraphics = Graphics.FromImage( bitmap );
				this.Draw( bitmapGraphics );
				bitmapGraphics.Dispose();

				return bitmap;
			}
		}

	#endregion
	
	#region PaneRect Properties
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
		/// IsShowPaneFrame is a boolean value that determines whether or not a frame border is drawn
		/// around the <see cref="GraphPane"/> area (<see cref="PaneRect"/>).
		/// True to draw the frame, false otherwise.
		/// </summary>
		/// <seealso cref="Default.IsPaneFramed"/>
		public bool IsPaneFramed
		{
			get { return isPaneFramed; }
			set { isPaneFramed = value; }
		}
		/// <summary>
		/// Frame color is a <see cref="System.Drawing.Color"/> specification
		/// for the <see cref="GraphPane"/> frame border.
		/// </summary>
		/// <seealso cref="Default.PaneFrameColor"/>
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
		/// <seealso cref="Default.PaneBackColor"/>
		public Color PaneBackColor
		{
			get { return paneBackColor; }
			set { paneBackColor = value; }
		}
		/// <summary>
		/// FrameWidth is a float value indicating the width (thickness) of the
		/// <see cref="GraphPane"/> frame border.
		/// </summary>
		/// <seealso cref="Default.PaneFramePenWidth"/>
		public float PaneFramePenWidth
		{
			get { return paneFramePenWidth; }
			set { paneFramePenWidth = value; }
		}
	#endregion
	
	#region AxisRect Properties
		/// <summary>
		/// Gets or sets the rectangle that contains the area bounded by the axes
		/// (<see cref="XAxis"/>, <see cref="YAxis"/>, and <see cref="Y2Axis"/>).
		/// If you set this value manually, then the <see cref="IsAxisRectAuto"/>
		/// value will automatically be set to false.
		/// </summary>
		/// <value>The rectangle units are in screen pixels</value>
		public RectangleF AxisRect
		{
			get { return axisRect; }
			set { axisRect = value; this.isAxisRectAuto = false; }
		}
		/// <summary>
		/// IsAxisRectAuto is a boolean value that determines whether or not the 
		/// <see cref="AxisRect"/> will be calculated automatically (almost always true).
		/// If you have a need to set the axisRect manually, such as you have multiple graphs
		/// on a page and you want to line up the edges perfectly, you can set this value
		/// to false.  If you set this value to false, you must also manually set
		/// the <see cref="AxisRect"/> property.
		/// You can easily determine the axisRect that ZedGraph would have
		/// calculated by calling the <see cref="CalcAxisRect"/> method, which returns
		/// an axis rect sized for the current data range, scale sizes, etc.
		/// </summary>
		/// <value>true to have ZedGraph calculate the axisRect, false to do it yourself</value>
		public bool IsAxisRectAuto
		{
			get { return isAxisRectAuto; }
			set { isAxisRectAuto = value; }
		}
		/// <summary>
		/// IsAxisFramed is a boolean value that determines whether or not a frame border is drawn
		/// around the axis area (<see cref="AxisRect"/>).
		/// </summary>
		/// <value>true to draw the frame, false otherwise. </value>
		/// <seealso cref="Default.IsAxisFramed"/>
		public bool IsAxisFramed
		{
			get { return isAxisFramed; }
			set { isAxisFramed = value; }
		}
		/// <summary>
		/// Frame color is a <see cref="System.Drawing.Color"/> specification
		/// for the axis frame border.
		/// </summary>
		/// <seealso cref="Default.AxisFrameColor"/>
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
		/// <seealso cref="Default.AxisBackColor"/>
		public Color AxisBackColor
		{
			get { return axisBackColor; }
			set { axisBackColor = value; }
		}
		/// <summary>
		/// FrameWidth is a float value indicating the width (thickness) of the axis frame border.
		/// </summary>
		/// <value>A pen width dimension in pixel units</value>
		/// <seealso cref="Default.AxisFramePenWidth"/>
		public float AxisFramePenWidth
		{
			get { return axisFramePenWidth; }
			set { axisFramePenWidth = value; }
		}
	#endregion
	
	#region Pane Scaling Properties
		/// <summary>
		/// PaneGap is a float value that sets the margin area between the edge of the
		/// <see cref="GraphPane"/> rectangle (<see cref="PaneRect"/>)
		/// and the features of the graph.
		/// </summary>
		/// <value>This value is in units of pixels, and is scaled
		/// linearly with the graph size.</value>
		/// <seealso cref="Default.PaneGap"/>
		/// <seealso cref="IsFontsScaled"/>
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
		/// <seealso cref="Default.BaseDimension"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="GraphPane.CalcScaleFactor"/>
		public double BaseDimension
		{
			get { return baseDimension; }
			set { baseDimension = value; }
		}
		/// <summary>
		/// Determines if the font sizes, tic sizes, gap sizes, etc. will be scaled according to
		/// the size of the <see cref="PaneRect"/> and the <see cref="BaseDimension"/>.  If this
		/// value is set to false, then the font sizes and tic sizes will always be exactly as
		/// specified, without any scaling.
		/// </summary>
		/// <value>True to have the fonts and tics scaled, false to have them constant</value>
		/// <seealso cref="GraphPane.CalcScaleFactor"/>
		public bool IsFontsScaled
		{
			get { return isFontsScaled; }
			set { isFontsScaled = value; }
		}
		/// <summary>
		/// BaseDPI is a double precision value that overrides the default DPI setting for this
		/// <see cref="GraphPane"/>.  Normally, you won't need to use this value for screen displays.
		/// However, when printing, the print device often scales the output internally such that
		/// the paneRect is the same size as the screen display, but the DPI goes up to 600.  Therefore,
		/// in order to scale the output properly, you need to override the scale calculations
		/// made by ZedGraph.  Leave this value at 0 to not override, (i.e., the output device
		/// resolution matches the definition of the PaneRect).
		/// </summary>
		/// <seealso cref="Default.BaseDimension"/>
		/// <seealso cref="GraphPane.CalcScaleFactor"/>
		/// <seealso cref="IsFontsScaled"/>
		public double BaseDPI
		{
			get { return baseDPI; }
			set { baseDPI = value; }
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
		/// <returns>Returns the paneGap size, in pixels, after scaling according to
		/// <paramref name="scalefactor"/></returns>
		public float ScaledGap( double scaleFactor )
		{
			return (float) ( this.paneGap * scaleFactor );
		}
	#endregion
	
	#region Bar Properties
		/// <summary>
		/// The minimum space between <see cref="Bar"/> clusters, expressed as a
		/// fraction of the bar size.
		/// </summary>
		/// <seealso cref="Default.MinClusterGap"/>
		/// <seealso cref="MinBarGap"/>
		/// <seealso cref="ClusterScaleWidth"/>
		public float MinClusterGap
		{
			get { return minClusterGap; }
			set { minClusterGap = value; }
		}
		/// <summary>
		/// The minimum space between individual <see cref="Bar">Bars</see>
		/// within a cluster, expressed as a
		/// fraction of the bar size.
		/// </summary>
		/// <seealso cref="Default.MinBarGap"/>
		/// <seealso cref="MinClusterGap"/>
		/// <seealso cref="ClusterScaleWidth"/>
		public float MinBarGap
		{
			get { return minBarGap; }
			set { minBarGap = value; }
		}
		/// <summary>
		/// The width of an individual bar cluster on a <see cref="Bar"/> graph.
		/// This value only applies to bar graphs plotted on non-ordinal X axis
		/// types (<see cref="AxisType.Linear"/>, <see cref="AxisType.Log"/>, and
		/// <see cref="AxisType.Date"/>.
		/// </summary>
		/// <seealso cref="Default.ClusterScaleWidth"/>
		/// <seealso cref="MinBarGap"/>
		/// <seealso cref="MinClusterGap"/>
		public double ClusterScaleWidth
		{
			get { return clusterScaleWidth; }
			set { clusterScaleWidth = value; }
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// Constructor for the <see cref="GraphPane"/> object.  This routine will
		/// initialize all member variables and classes, setting appropriate default
		/// values as defined in the <see cref="Default"/> class.
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
			this.isShowTitle = Default.IsShowTitle;
			this.fontSpec = new FontSpec( Default.FontFamily,
				Default.FontSize, Default.FontColor, Default.FontBold,
				Default.FontItalic, Default.FontUnderline );
			this.fontSpec.IsFilled = false;
			this.fontSpec.IsFramed = false;
					
			this.isIgnoreInitial = Default.IsIgnoreInitial;
			
			this.isPaneFramed = Default.IsPaneFramed;
			this.paneFrameColor = Default.PaneFrameColor;
			this.paneFramePenWidth = Default.PaneFramePenWidth;
			this.paneBackColor = Default.PaneBackColor;

			this.isAxisRectAuto = true;
			this.isAxisFramed = Default.IsAxisFramed;
			this.axisFrameColor = Default.AxisFrameColor;
			this.axisFramePenWidth = Default.AxisFramePenWidth;
			this.axisBackColor = Default.AxisBackColor;

			this.baseDimension = Default.BaseDimension;
			this.baseDPI = 0;
			this.paneGap = Default.PaneGap;
			this.isFontsScaled = true;

			this.minClusterGap = Default.MinClusterGap;
			this.minBarGap = Default.MinBarGap;
			this.clusterScaleWidth = Default.ClusterScaleWidth;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The GraphPane object from which to copy</param>
		public GraphPane( GraphPane rhs )
		{
			paneRect = rhs.PaneRect;
			xAxis = new XAxis( rhs.XAxis );
			yAxis = new YAxis( rhs.YAxis );
			y2Axis = new Y2Axis( rhs.Y2Axis );
			legend = new Legend( rhs.Legend);
			curveList = new CurveList( rhs.CurveList );
			textList = new TextList( rhs.TextList );
			arrowList = new ArrowList(rhs.ArrowList );
			
			this.title = rhs.Title;
			this.isShowTitle = rhs.IsShowTitle;
			this.fontSpec = new FontSpec( rhs.FontSpec );
					
			this.isIgnoreInitial = rhs.IsIgnoreInitial;
			
			this.isPaneFramed = rhs.IsPaneFramed;
			this.paneFrameColor = rhs.PaneFrameColor;
			this.paneFramePenWidth = rhs.PaneFramePenWidth;
			this.paneBackColor = rhs.PaneBackColor;

			this.isAxisRectAuto = rhs.IsAxisRectAuto;
			this.isAxisFramed = rhs.IsAxisFramed;
			this.axisFrameColor = rhs.AxisFrameColor;
			this.axisFramePenWidth = rhs.AxisFramePenWidth;
			this.axisBackColor = rhs.AxisBackColor;

			this.baseDimension = rhs.BaseDimension;
			this.isFontsScaled = rhs.isFontsScaled;
			this.baseDPI = rhs.BaseDPI;
			this.paneGap = rhs.PaneGap;
			this.minClusterGap = rhs.MinClusterGap;
			this.minBarGap = rhs.MinBarGap;
			this.clusterScaleWidth = rhs.ClusterScaleWidth;
		} 

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the GraphPane</returns>
		public object Clone()
		{ 
			return new GraphPane( this ); 
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// AxisChange causes the axes scale ranges to be recalculated based on the current data range.
		/// Call this function anytime you change, add, or remove curve data.  This routine calculates
		/// a scale minimum, maximum, and step size for each axis based on the current curve data.
		/// Only the axis attributes (min, max, step) that are set to auto-range (<see cref="Axis.MinAuto"/>,
		/// <see cref="Axis.MaxAuto"/>, <see cref="Axis.StepAuto"/>) will be modified.  You must call
		/// Invalidate() after calling AxisChange to make sure the display gets updated.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void AxisChange( Graphics g )
		{
			double	xMin, xMax, yMin, yMax, y2Min, y2Max;

			// Get the scale range of the data (all curves)
			this.curveList.GetRange( out xMin, out xMax, out yMin,
				out yMax, out y2Min, out y2Max,
				this.isIgnoreInitial, this );

			// Determine the scale factor
			double	scaleFactor = this.CalcScaleFactor( g );

/*
 * 			int		hStack;
			float	legendWidth;
			if ( this.isAxisRectAuto )
				this.axisRect = CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth );
			else
				CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth );
*/

			// if the AxisRect is not yet determined, then pick a scale based on a default AxisRect
			// size (using 75% of PaneRect -- code is in Axis.CalcMaxLabels() )
			// With the scale picked, call CalcAxisRect() so calculate a real AxisRect
			// then let the scales re-calculate to make sure that the assumption was ok
			if ( this.isAxisRectAuto )
			{
				if ( AxisRect.Width == 0 || AxisRect.Height == 0 )
				{
					// Pick new scales based on the range
					this.xAxis.PickScale( xMin, xMax, this, g, scaleFactor );
					this.yAxis.PickScale( yMin, yMax, this, g, scaleFactor );
					this.y2Axis.PickScale( y2Min, y2Max, this, g, scaleFactor );
	  
				}

				this.axisRect = CalcAxisRect( g );
			}
 
			// Pick new scales based on the range
			this.xAxis.PickScale( xMin, xMax, this, g, scaleFactor );
			this.yAxis.PickScale( yMin, yMax, this, g, scaleFactor );
			this.y2Axis.PickScale( y2Min, y2Max, this, g, scaleFactor );
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
			// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
			double	scaleFactor;
			int		hStack;
			float	legendWidth;

			// if the size of the axisRect is determined automatically, then do so
			// otherwise, calculate the legendrect, scalefactor, hstack, and legendwidth parameters
			// but leave the axisRect alone
			if ( this.isAxisRectAuto )
				this.axisRect = CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth );
			else
				CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth );

			// Frame the whole pane
			DrawPaneFrame( g );

			// do a sanity check on the axisRect
			if ( this.axisRect.Width < 1 || this.axisRect.Height < 1 )
				return;
			
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
		/// <returns>The calculated axis rect, in pixel coordinates.</returns>
		public RectangleF CalcAxisRect( Graphics g )
		{
			// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
			double	scaleFactor;
			int		hStack;
			float	legendWidth;
			
			return CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth );
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
		/// <returns>The calculated axis rect, in pixel coordinates.</returns>
		public RectangleF CalcAxisRect( Graphics g, out double scaleFactor,
									out int hStack, out float legendWidth )
		{
			// calculate scaleFactor on "normal" pane size (BaseDimension)
			scaleFactor = this.CalcScaleFactor( g );

			// get scaled values for the paneGap and character height
			float gap = this.ScaledGap( scaleFactor );
			float charHeight = this.FontSpec.GetHeight( scaleFactor );
				
			// Axis rect starts out at the full pane rect.  It gets reduced to make room for the legend,
			// scales, titles, etc.
			RectangleF tmpRect = this.paneRect;

			float space = this.xAxis.CalcSpace( g, this, scaleFactor );
			tmpRect.Height -= space;
			space = this.yAxis.CalcSpace( g, this, scaleFactor );
			tmpRect.X += space;
			tmpRect.Width -= space;
			space = this.y2Axis.CalcSpace( g, this, scaleFactor );
			tmpRect.Width -= space;
	
			// Always leave a gap on top, even with no title
			tmpRect.Y += gap;
			tmpRect.Height -= gap;

			// Leave room for the pane title
			if ( this.isShowTitle )
			{
				SizeF titleSize = this.FontSpec.MeasureString( g, this.title, scaleFactor );
				// Leave room for the title height, plus a line spacing of charHeight/2
				tmpRect.Y += titleSize.Height + charHeight / 2.0F;
				tmpRect.Height -= titleSize.Height + charHeight / 2.0F;
			}
			
			// Calculate the legend rect, and back it out of the current axisRect
			this.legend.CalcRect( g, this, scaleFactor, ref tmpRect,
								out hStack, out legendWidth );

			return tmpRect;
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
			//tempRect.Width -= 1;
			//tempRect.Height -= 1;

			if ( this.isPaneFramed )
			{
				Pen pen = new Pen( this.paneFrameColor, this.paneFramePenWidth );
				//g.DrawRectangle( pen, Rectangle.Round( tempRect ) );
				
				// FrameRect draws one pixel short of the bottom/right border, so
				//  just draw it manually
				g.DrawLine( pen, paneRect.Left, paneRect.Bottom,
									paneRect.Left, paneRect.Top );
				g.DrawLine( pen, paneRect.Left, paneRect.Top,
									paneRect.Right, paneRect.Top );
				g.DrawLine( pen, paneRect.Right, paneRect.Top,
									paneRect.Right, paneRect.Bottom );
				g.DrawLine( pen, paneRect.Right, paneRect.Bottom,
									paneRect.Left, paneRect.Bottom );
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
				//g.DrawRectangle( pen, Rectangle.Round( this.axisRect ) );
				// FrameRect draws one pixel short of the bottom/right border, so
				//  just draw it manually
				g.DrawLine( pen, axisRect.Left, axisRect.Bottom,
					axisRect.Left, axisRect.Top );
				g.DrawLine( pen, axisRect.Left, axisRect.Top,
					axisRect.Right, axisRect.Top );
				g.DrawLine( pen, axisRect.Right, axisRect.Top,
					axisRect.Right, axisRect.Bottom );
				g.DrawLine( pen, axisRect.Right, axisRect.Bottom,
					axisRect.Left, axisRect.Bottom );
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
		/// <returns>
		/// A double precision value representing the scaling factor to use for the rendering calculations.
		/// </returns>
		protected double CalcScaleFactor( Graphics g )
		{
			double scaleFactor, xInch, yInch;
			const double ASPECTLIMIT = 2.0;
			
			// Assume the standard width (BaseDimension) is 8.0 inches
			// Therefore, if the paneRect is 8.0 inches wide, then the fonts will be scaled at 1.0
			// if the paneRect is 4.0 inches wide, the fonts will be half-sized.
			// if the paneRect is 16.0 inches wide, the fonts will be double-sized.
		
			// if font scaling is turned off, then always return a 1.0 scale factor
			if ( !this.isFontsScaled )
				return 1.0;

			// determine the size of the paneRect in inches
			if ( this.baseDPI == 0 )
			{
				xInch = (double) this.paneRect.Width / (double) g.DpiX;
				yInch = (double) this.paneRect.Height / (double) g.DpiY;
			}
			else
			{
				xInch = (double) this.paneRect.Width / this.baseDPI;
				yInch = (double) this.paneRect.Height / this.baseDPI;
			}
					
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
	#endregion
	
	#region AddCurve Methods
		/// <summary>
		/// Add a curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (double arrays) and properties.
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
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the <see cref="AddCurve"/> method.</returns>
		public CurveItem AddCurve( string label, double[] x, double[] y, Color color )
		{
			CurveItem curve = new CurveItem( label, x, y, false, color, SymbolType.Empty );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (<see cref="PointPairList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the <see cref="AddCurve"/> method.</returns>
		public CurveItem AddCurve( string label, PointPairList points, Color color )
		{
			CurveItem curve = new CurveItem( label, points, false, color, SymbolType.Empty );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (double arrays) and properties.
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
			CurveItem curve = new CurveItem( label, x, y, false, color, symbolType );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (<see cref="PointPairList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <param name="symbolType">A symbol type (<see cref="SymbolType"/>)
		/// that will be used for this curve.</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the <see cref="AddCurve"/> method.</returns>
		public CurveItem AddCurve( string label, PointPairList points,
			Color color, SymbolType symbolType )
		{
			CurveItem curve = new CurveItem( label, points, false, color, symbolType );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a bar type curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (<see cref="PointPairList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used to fill the bars</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the <see cref="AddCurve"/> method.</returns>
		public CurveItem AddBar( string label, PointPairList points, Color color )
		{
			CurveItem curve = new CurveItem( label, points, true, color, SymbolType.Empty );
			this.curveList.Add( curve );
			
			return curve;
		}
		#endregion

		#region General Utility Methods
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

		/// <summary>
		/// Return the user scale values that correspond to the specified screen
		/// coordinate position (pixels).
		/// </summary>
		/// <param name="ptF">The X,Y pair that defines the screen coordinate
		/// point of interest</param>
		/// <param name="x">The resultant value in user coordinates from the
		/// <see cref="XAxis"/></param>
		/// <param name="y">The resultant value in user coordinates from the
		/// <see cref="YAxis"/></param>
		/// <param name="y2">The resultant value in user coordinates from the
		/// <see cref="Y2Axis"/></param>
		public void ReverseTransform( PointF ptF, out double x, out double y,
			out double y2 )
		{
			x = this.XAxis.ReverseTransform( ptF.X );
			y = this.YAxis.ReverseTransform( ptF.Y );
			y2 = this.Y2Axis.ReverseTransform( ptF.Y );
		}

		/// <summary>
		/// Find the data point that lies closest to the specified mouse (screen) point.
		/// This method will search through the list of curves to find which point is
		/// nearest.  It will only consider points that are within
		/// <see cref="Default.NearestTol"/> pixels of the screen point.
		/// </summary>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="nearestCurve">A reference to the <see cref="CurveItem"/>
		/// instance that contains the closest point.  nearestCurve will be null if
		/// no data points are available.</param>
		/// <param name="iNearest">The index number of the closest point.  The
		/// actual data vpoint will then be <see cref="CurveItem.Points">CurveItem.Points[iNearest]</see>
		/// .  iNearest will
		/// be -1 if no data points are available.</param>
		/// <returns>true if a point was found and that point lies within
		/// <see cref="Default.NearestTol"/> pixels
		/// of the screen point, false otherwise.</returns>
		public bool FindNearestPoint( PointF mousePt,
			out CurveItem nearestCurve, out int iNearest )
		{
			nearestCurve = null;
			iNearest = -1;

			// If the point is outside the axisRect, always return false
			if ( ! axisRect.Contains( mousePt ) )
				return false;

			double	x, y, y2;
			ReverseTransform( mousePt, out x, out y, out y2 );

			if ( xAxis.Min == xAxis.Max || yAxis.Min == yAxis.Max ||
				y2Axis.Min == y2Axis.Max )
				return false;

			float	barWidth = CalcBarWidth();

			double	xPixPerUnit = axisRect.Width / ( xAxis.Max - xAxis.Min );
			double	yPixPerUnit = axisRect.Height / ( yAxis.Max - yAxis.Min );
			double	y2PixPerUnit = axisRect.Height / ( y2Axis.Max - y2Axis.Min );

			double	yPixPerUnitAct, yAct, yMinAct, yMaxAct;
			double	minDist = 1e20;
			double	xVal, yVal, dist, distX, distY;
			double	tolSquared = Default.NearestTol * Default.NearestTol;

			double	barWidthUserHalf = barWidth / xPixPerUnit / 2.0;

			int		iBar = 0;

			foreach ( CurveItem curve in curveList )
			{
				if ( curve.IsY2Axis )
				{
					yAct = y2;
					yMinAct = y2Axis.Min;
					yMaxAct = y2Axis.Max;
					yPixPerUnitAct = y2PixPerUnit;
				}
				else
				{
					yAct = y;
					yMinAct = yAxis.Min;
					yMaxAct = yAxis.Max;
					yPixPerUnitAct = yPixPerUnit;
				}

				PointPairList points = curve.Points;
				
				if ( points != null )
				{
					for ( int iPt=0; iPt<curve.NPts; iPt++ )
					{
						if ( xAxis.IsOrdinal )
							xVal = (double) iPt + 1.0;
						else
							xVal = points[iPt].X;

						if ( yAxis.IsOrdinal )
							yVal = (double) iPt + 1.0;
						else
							yVal = points[iPt].Y;

						if (	xVal != PointPair.Missing &&
								xVal >= xAxis.Min && xVal <= xAxis.Max &&
								yVal != PointPair.Missing &&
								yVal >= yMinAct && yVal <= yMaxAct )
						{

							if ( curve.IsBar )
							{
								float xPix = curve.CalcBarCenter( this, barWidth, iPt, iBar );
								xVal = this.xAxis.ReverseTransform( xPix );

								if (	( x < xVal - barWidthUserHalf ) ||
										( x > xVal + barWidthUserHalf ) ||
										( yVal >=0 && ( y < 0 || y > yVal ) ) ||
										( yVal < 0 && ( y >= 0 || y < yVal ) ) )
									continue;

								dist = 0;
							}
							else
							{
								distX = (xVal - x) * xPixPerUnit;
								distY = (yVal - yAct) * yPixPerUnitAct;
								dist = distX * distX + distY * distY;

								if ( dist > minDist )
									continue;
							}

							minDist = dist;
							iNearest = iPt;
							nearestCurve = curve;
						}
					}

					if ( curve.IsBar )
						iBar++;
				}
			}

			// Did we find a close point, and is it within the tolerance?
			// (minDist is the square of the distance in pixel units)
			if ( minDist < tolSquared )
			{
				return true;
			}

			return false;
		}
/*
		public bool FindNearestPoint( PointF mousePt,
								  out CurveItem nearestCurve, out int iNearest )
		{
			nearestCurve = null;
			iNearest = -1;

			// If the point is outside the axisRect, always return false
			if ( ! axisRect.Contains( mousePt ) )
				return false;

			double x, y, y2;
			ReverseTransform( mousePt, out x, out y, out y2 );

			if ( xAxis.Min == xAxis.Max || yAxis.Min == yAxis.Max ||
						y2Axis.Min == y2Axis.Max )
				return false;

			float barWidth = CalcBarWidth();

			double xPixPerUnit = axisRect.Width / ( xAxis.Max - xAxis.Min );
			double yPixPerUnit = axisRect.Height / ( yAxis.Max - yAxis.Min );
			double y2PixPerUnit = axisRect.Height / ( y2Axis.Max - y2Axis.Min );

			double		yPixPerUnitAct, yAct, yMinAct, yMaxAct;
			double		minDist = 1e20;
			double		xVal, yVal, dist, distX, distY;
			double		tolSquared = Default.NearestTol * Default.NearestTol;

			int			iBar = 0;

			foreach ( CurveItem curve in curveList )
			{
				if ( curve.IsY2Axis )
				{
					yAct = y2;
					yMinAct = y2Axis.Min;
					yMaxAct = y2Axis.Max;
					yPixPerUnitAct = y2PixPerUnit;
				}
				else
				{
					yAct = y;
					yMinAct = yAxis.Min;
					yMaxAct = yAxis.Max;
					yPixPerUnitAct = yPixPerUnit;
				}

				PointPairList points = curve.Points;
				
				if ( points != null )
				{
					for ( int iPt=0; iPt<curve.NPts; iPt++ )
					{
						if ( curve.IsBar )
						{
							float xPix = curve.CalcBarCenter( this, barWidth, iPt, iBar );
							xVal = this.xAxis.ReverseTransform( xPix );
						}
						else
							xVal = points[iPt].X;

						yVal = points[iPt].Y;

						if (	xVal != PointPair.Missing &&
								xVal >= xAxis.Min && xVal <= xAxis.Max &&
								yVal != PointPair.Missing &&
								yVal >= yMinAct && yVal <= yMaxAct )
						{
							distX = (xVal - x) * xPixPerUnit;
							distY = (yVal - yAct) * yPixPerUnitAct;
							dist = distX * distX + distY * distY;

							if ( dist < minDist )
							{
								minDist = dist;
								iNearest = iPt;
								nearestCurve = curve;
							}
						}
					}

					if ( curve.IsBar )
						iBar++;
				}
			}

			// Did we find a close point, and is it within the tolerance?
			// (minDist is the square of the distance in pixel units)
			if ( minDist < tolSquared )
			{
				return true;
			}

			return false;
		}
*/
		/// <summary>
		/// Calculate the width of each bar
		/// </summary>
		/// <returns>The width for an individual bar, in pixel units</returns>
		public float CalcBarWidth()
		{
			// Total axis width = 
			// npts * ( nbars * ( bar + bargap ) - bargap + clustgap )
			// cg * bar = cluster gap
			// npts = max number of points in any curve
			// nbars = total number of curves that are of type IsBar
			// bar = bar width
			// bg * bar = bar gap
			// therefore:
			// totwidth = npts * ( nbars * (bar + bg*bar) - bg*bar + cg*bar )
			// totwidth = bar * ( npts * ( nbars * ( 1 + bg ) - bg + cg ) )
			// solve for bar

			float denom = CurveList.NumBars * ( 1.0F + MinBarGap ) -
							MinBarGap + MinClusterGap;

			float barWidth = this.XAxis.GetClusterWidth( this ) / denom;

			if ( barWidth <= 0 )
				return 1;

			return barWidth;
		}
	#endregion
	
	}
}

