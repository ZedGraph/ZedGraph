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
	/// associated with an individual graph.
	/// </summary>
	/// <remarks>This class is the outside "wrapper"
	/// for the ZedGraph classes, and provides the interface to access the attributes
	/// of the graph.  You can have multiple graphs in the same document or form,
	/// just instantiate multiple GraphPane's.
	/// </remarks>
	/// 
	/// <author> John Champion modified by Jerry Vos </author>
	/// <version> $Revision: 3.15 $ $Date: 2004-12-03 13:31:28 $ </version>
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
		/// <summary>Private field instance of the <see cref="ZedGraph.GraphItemList"/> class.  Use the
		/// public property <see cref="GraphPane.GraphItemList"/> to access this class.</summary>
		private GraphItemList	graphItemList;
		
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
		
		// Pane Border Properties ///////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="PaneRect"/>.  Use the public property <see cref="PaneFill"/> to
		/// access this value.
		/// </summary>
		private Fill		paneFill;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for this
		/// <see cref="PaneRect"/>.  Use the public property <see cref="PaneBorder"/> to
		/// access this value.
		/// </summary>
		private Border		paneBorder;
		
		// Axis Border Properties //////////////////////////////////////////////////////////////
		
		/// <summary>Private field that determines if the <see cref="AxisRect"/> will be
		/// sized automatically.  Use the public property <see cref="IsAxisRectAuto"/> to access
		/// this value. </summary>
		private bool isAxisRectAuto;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="AxisRect"/>.  Use the public property <see cref="AxisFill"/> to
		/// access this value.
		/// </summary>
		private Fill axisFill;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for this
		/// <see cref="AxisRect"/>.  Use the public property <see cref="AxisBorder"/> to
		/// access this value.
		/// </summary>
		private Border axisBorder;
		
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
        /// edges of the pane, in points (1/72 inch).  This value is scaled according to the graph size.
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
		/// <summary>Private field that determines the base axis from which <see cref="Bar"/>
		/// graphs will be displayed.  The base axis is the axis from which the bars grow with
		/// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
		/// To access this value, use the public property <see cref="BarBase"/>.
		/// </summary>
		/// <seealso cref="Default.BarBase"/>
		private BarBase		barBase;
		/// <summary>Private field that determines how the <see cref="BarItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
		/// for the individual types available.
		/// To access this value, use the public property <see cref="BarType"/>.
		/// </summary>
		/// <seealso cref="Default.BarType"/>
		private BarType barType;
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
		/// <seealso cref="IsPenWidthScaled"/>
		private bool		isFontsScaled;
        /// <summary>
        /// Private field that controls whether or not pen widths are scaled according to the
        /// size of the graph.  This value is only applicable if <see cref="IsFontsScaled"/>
        /// is true.  If <see cref="IsFontsScaled"/> is false, then no scaling will be done,
        /// regardless of the value of <see cref="IsPenWidthScaled"/>.
        /// </summary>
        /// <value>true to scale the pen widths according to the size of the graph,
        /// false otherwise.</value>
        /// <seealso cref="IsFontsScaled"/>
        /// <seealso cref="CalcScaleFactor"/>
        private bool isPenWidthScaled;

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
			/// (<see cref="ZedGraph.FontSpec.Size"/> property).
			/// </summary>
			public static float FontSize = 16;
			/// <summary>
			/// The default font color for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = true;
			/// <summary>
			/// The default font italic mode for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
			/// <summary>
			/// The default font underline mode for the
			/// <see cref="GraphPane"/> pane title
			/// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			/// <summary>
			/// The default color for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FontFillColor = Color.White;
			/// <summary>
			/// The default custom brush for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FontFillBrush = null;
			/// <summary>
			/// The default fill mode for filling in the scale text background
			/// (see <see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FontFillType = FillType.None;
			
			//		public static bool stepPlot = false;
			/// <summary>
			/// The default border mode for the <see cref="GraphPane"/>.
			/// (<see cref="GraphPane.PaneBorder"/> property). true
			/// to draw a border around the <see cref="GraphPane.PaneRect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsPaneBorderVisible = true;
			/// <summary>
			/// The default color for the <see cref="GraphPane"/> border border.
			/// (<see cref="GraphPane.PaneBorder"/> property). 
			/// </summary>
			public static Color PaneBorderColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="GraphPane.PaneRect"/> background.
			/// (<see cref="GraphPane.PaneFill"/> property). 
			/// </summary>
			public static Color PaneBackColor = Color.White;
			/// <summary>
			/// The default brush for the <see cref="GraphPane.PaneRect"/> background.
			/// (<see cref="GraphPane.PaneFill"/> property). 
			/// </summary>
			public static Brush PaneBackBrush = null;
			/// <summary>
			/// The default <see cref="FillType"/> for the <see cref="GraphPane.PaneRect"/> background.
			/// (<see cref="GraphPane.PaneFill"/> property). 
			/// </summary>
			public static FillType PaneBackType = FillType.Brush;
			/// <summary>
			/// The default pen width for the <see cref="GraphPane"/> border border.
            /// (<see cref="GraphPane.PaneBorder"/> property).  Units are in points (1/72 inch).
            /// </summary>
			public static float PaneBorderPenWidth = 1;

			/// <summary>
			/// The default color for the <see cref="Axis"/> border border.
			/// (<see cref="GraphPane.AxisBorder"/> property). 
			/// </summary>
			public static Color AxisBorderColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="GraphPane.AxisRect"/> background.
			/// (<see cref="GraphPane.AxisFill"/> property). 
			/// </summary>
			public static Color AxisBackColor = Color.White;
			/// <summary>
			/// The default brush for the <see cref="GraphPane.AxisRect"/> background.
			/// (<see cref="ZedGraph.Fill.Brush"/> property of <see cref="GraphPane.AxisFill"/>). 
			/// </summary>
			public static Brush AxisBackBrush = null;
			/// <summary>
			/// The default <see cref="FillType"/> for the <see cref="GraphPane.AxisRect"/> background.
			/// (<see cref="ZedGraph.Fill.Type"/> property of <see cref="GraphPane.AxisFill"/>). 
			/// </summary>
			public static FillType AxisBackType = FillType.Brush;
			/// <summary>
			/// The default pen width for drawing the 
			/// <see cref="GraphPane.AxisRect"/> border border
			/// (<see cref="GraphPane.AxisBorder"/> property).
            /// Units are in points (1/72 inch).
            /// </summary>
			public static float AxisBorderPenWidth = 1F;
			/// <summary>
			/// The default display mode for the <see cref="Axis"/> border border
			/// (<see cref="GraphPane.AxisBorder"/> property). true
			/// to show the border border, false to omit the border
			/// </summary>
			public static bool IsAxisBorderVisible = true;

            /// <summary>
            /// The default settings for the <see cref="Axis"/> scale ignore initial
            /// zero values option (<see cref="GraphPane.IsIgnoreInitial"/> property).
            /// true to have the auto-scale-range code ignore the initial data points
            /// until the first non-zero Y value, false otherwise.
            /// </summary>
            public static bool IsIgnoreInitial = false;
            /// <summary>
            /// The default setting for the <see cref="GraphPane.IsPenWidthScaled"/> option.
            /// true to have all pen widths scaled according to <see cref="GraphPane.BaseDimension"/>,
            /// false otherwise.
            /// </summary>
            /// <seealso cref="GraphPane.CalcScaleFactor"/>
            public static bool IsPenWidthScaled = false;

            /// <summary>
			/// The default value for the <see cref="GraphPane.PaneGap"/> property.
			/// This is the size of the margin around the edge of the
            /// <see cref="GraphPane.PaneRect"/>, in units of points (1/72 inch).
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
			/// <summary>The default value for the <see cref="BarBase"/>, which determines the base
			/// <see cref="Axis"/> from which the <see cref="Bar"/> graphs will be displayed.
			/// </summary>
			/// <seealso cref="GraphPane.BarBase"/>
			public static BarBase BarBase = BarBase.X;
			/// <summary>The default value for the <see cref="GraphPane.BarType"/> property, which
			/// determines if the bars are drawn overlapping eachother in a "stacked" format,
			/// or side-by-side in a "cluster" format.  See the <see cref="ZedGraph.BarType"/>
			/// for more information.
			/// </summary>
			/// <seealso cref="GraphPane.BarType"/>
			public static BarType BarType = BarType.Cluster;
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
			/// The tolerance that is applied to the
			/// <see cref="GraphPane.FindNearestPoint(PointF,out CurveItem,out int)"/> routine.
			/// If a given curve point is within this many pixels of the mousePt, the curve
			/// point is considered to be close enough for selection as a nearest point
			/// candidate.
			/// </summary>
			public static double NearestTol = 7.0;
		}
	#endregion

	#region public Class Instance Properties
		/// <summary>
		/// Gets or sets the list of <see cref="GraphItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="GraphItemList"/> collection object</value>
		public GraphItemList GraphItemList
		{
			get { return graphItemList; }
			set { graphItemList = value; }
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
		/// Gets or sets a boolean value that affects the data range that is considered
		/// for the automatic scale ranging.
		/// </summary>
		/// <remarks>If true, then initial data points where the Y value
		/// is zero are not included when automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.
		/// All data after the first non-zero Y value are included.
		/// </remarks>
		/// <seealso cref="Default.IsIgnoreInitial"/>
		public bool IsIgnoreInitial
		{
			get { return isIgnoreInitial; }
			set { isIgnoreInitial = value; }
		}
		/// <summary>Gets or sets a value that determines whether or not initial
		/// <see cref="PointPair.Missing"/> values will cause the line segments of
		/// a curve to be discontinuous.
		/// </summary>
		/// <remarks>If this field is true, then the curves
		/// will be plotted as continuous lines as if the Missing values did not exist.
		/// Use the public property <see cref="IsIgnoreMissing"/> to access
		/// this value. </remarks>
		public bool IsIgnoreMissing
		{
			get { return isIgnoreMissing; }
			set { isIgnoreMissing = value; }
		}
		/// <summary>
		/// IsShowTitle is a boolean value that determines whether or not the pane title is displayed
		/// on the graph.
		/// </summary>
		/// <remarks>If true, the title is displayed.  If false, the title is omitted, and the
		/// screen space that would be occupied by the title is added to the axis area.
		/// </remarks>
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
		/// <remarks>Note that this image will be 1 pixel larger than the <see cref="PaneRect"/>
		/// size in order to fully contain the <see cref="PaneRect"/>.  To get a precise bitmap
		/// size, use <see cref="ScaledImage"/>.</remarks>
		/// <seealso cref="ScaledImage"/>
		public Bitmap Image
		{
			get
			{
				// Need to make the bitmap 1 pixel larger than the image for proper containment
				Bitmap bitmap = new Bitmap( (int) this.paneRect.Width+1, (int) this.paneRect.Height+1 );
				Graphics bitmapGraphics = Graphics.FromImage( bitmap );
				bitmapGraphics.TranslateTransform( -this.PaneRect.Left, -this.PaneRect.Top );
				this.Draw( bitmapGraphics );
				bitmapGraphics.Dispose();

				return bitmap;
			}
		}

		/// <summary>
		/// Gets an image for the current GraphPane, scaled to the specified size and resolution.
		/// </summary>
		/// <param name="width">The scaled width of the bitmap in pixels</param>
		/// <param name="height">The scaled height of the bitmap in pixels</param>
		/// <param name="dpi">The resolution of the bitmap, in dots per inch</param>
		/// <seealso cref="Image"/>
		/// <seealso cref="Bitmap"/>
		public Bitmap ScaledImage( int width, int height, float dpi )
		{
			Bitmap bitmap = new Bitmap( width, height );
			bitmap.SetResolution( dpi, dpi );
			Graphics bitmapGraphics = Graphics.FromImage( bitmap );
			//bitmapGraphics.TranslateTransform( -this.PaneRect.Left, -this.PaneRect.Top );
			//bitmapGraphics.ScaleTransform( width/this.PaneRect.Width, width/this.PaneRect.Width );
			
			// Clone the GraphPane so we don't mess up the minPix and maxPix values or
			// the paneRect/axisRect calculations of the original
			GraphPane tempPane = (GraphPane) this.Clone();
			// Make the paneRect 1 pixel smaller than the actual bitmap size to fully contain the image
			tempPane.PaneRect = new RectangleF( 0, 0, width-1, height-1 );
			//tempPane.AxisChange( bitmapGraphics );
			tempPane.Draw( bitmapGraphics );
			//this.Draw( bitmapGraphics );
			bitmapGraphics.Dispose();

			return bitmap;
		}

	#endregion
	
	#region PaneRect Properties
		/// <summary>
		/// Gets or sets the rectangle that defines the full area into which the
		/// <see cref="GraphPane"/> can be rendered.
		/// </summary>
		/// <value>The rectangle units are in pixels</value>
		public RectangleF PaneRect
		{
			get { return paneRect; }
			set { paneRect = value; }
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> class for drawing the border
		/// border around the <see cref="PaneRect"/>
		/// </summary>
		/// <seealso cref="Default.PaneBorderColor"/>
		/// <seealso cref="Default.PaneBorderPenWidth"/>
		public Border PaneBorder
		{
			get { return paneBorder; }
			set { paneBorder = value; }
		}		
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="PaneRect"/>.
		/// </summary>
		public Fill	PaneFill
		{
			get { return paneFill; }
			set { paneFill = value; }
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
		/// calculated by calling the <see cref="CalcAxisRect(Graphics)"/> method, which returns
		/// an axis rect sized for the current data range, scale sizes, etc.
		/// </summary>
		/// <value>true to have ZedGraph calculate the axisRect, false to do it yourself</value>
		public bool IsAxisRectAuto
		{
			get { return isAxisRectAuto; }
			set { isAxisRectAuto = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> class for drawing the border
		/// border around the <see cref="AxisRect"/>
		/// </summary>
		/// <seealso cref="Default.AxisBorderColor"/>
		/// <seealso cref="Default.AxisBorderPenWidth"/>
		public Border AxisBorder
		{
			get { return axisBorder; }
			set { axisBorder = value; }
		}				
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="AxisRect"/>.
		/// </summary>
		public Fill	AxisFill
		{
			get { return axisFill; }
			set { axisFill = value; }
		}
	#endregion
	
	#region Pane Scaling Properties
		/// <summary>
		/// PaneGap is a float value that sets the margin area between the edge of the
		/// <see cref="GraphPane"/> rectangle (<see cref="PaneRect"/>)
		/// and the features of the graph.
		/// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
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

        /// <summary>
        /// Calculate the scaled pen width, taking into account the scaleFactor and the
        /// setting of the <see cref="IsPenWidthScaled"/> property of the pane.
        /// </summary>
        /// <param name="penWidth">The pen width, in points (1/72 inch)</param>
        /// <param name="scaleFactor">
        /// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
        /// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
        /// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
        /// </param>
        /// <returns>The scaled pen width, in world pixels</returns>
        public float ScaledPenWidth(float penWidth, double scaleFactor)
        {
            if (isPenWidthScaled)
                return (float)(penWidth * scaleFactor);
            else
                return penWidth;
        }

        /// <summary>
        /// Gets or sets the property that controls whether or not pen widths are scaled for this
        /// <see cref="GraphPane"/>.
        /// </summary>
        /// <remarks>This value is only applicable if <see cref="IsFontsScaled"/>
        /// is true.  If <see cref="IsFontsScaled"/> is false, then no scaling will be done,
        /// regardless of the value of <see cref="IsPenWidthScaled"/>.  Note that scaling the pen
        /// widths can cause "artifacts" to appear at typical screen resolutions.  This occurs
        /// because of roundoff differences; in some cases the pen width may round to 1 pixel wide
        /// and in another it may round to 2 pixels wide.  The result is typically undesirable.
        /// Therefore, this option defaults to false.  This option is primarily useful for high
        /// resolution output, such as printer output or high resolution bitmaps (from
        /// <see cref="GraphPane.ScaledImage"/>) where it is desirable to have the pen width
        /// be consistent with the screen image.
        /// </remarks>
        /// <value>true to scale the pen widths according to the size of the graph,
        /// false otherwise.</value>
        /// <seealso cref="IsFontsScaled"/>
        /// <seealso cref="CalcScaleFactor"/>
        public bool IsPenWidthScaled
        {
            get { return isPenWidthScaled; }
            set { isPenWidthScaled = value; }
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
		/// <summary>Determines the base axis from which <see cref="Bar"/>
		/// graphs will be displayed.  The base axis is the axis from which the bars grow with
		/// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
		/// </summary>
		/// <seealso cref="Default.BarBase"/>
		public BarBase BarBase
		{
			get { return barBase; }
			set { barBase = value; }
		}
		/// <summary>Private field that determines how the <see cref="BarItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
		/// for the individual types available.
		/// To access this value, use the public property <see cref="BarType"/>.
		/// </summary>
		/// <seealso cref="Default.BarType"/>
		public BarType BarType
		{
			get { return barType; }
			set { barType = value; }
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
			graphItemList = new GraphItemList();
			
			this.title = paneTitle;
			this.isShowTitle = Default.IsShowTitle;
			this.fontSpec = new FontSpec( Default.FontFamily,
				Default.FontSize, Default.FontColor, Default.FontBold,
				Default.FontItalic, Default.FontUnderline,
				Default.FontFillColor, Default.FontFillBrush,
				Default.FontFillType );
			this.fontSpec.Border.IsVisible = false;
					
			this.isIgnoreInitial = Default.IsIgnoreInitial;
			
			this.paneBorder = new Border( Default.IsPaneBorderVisible, Default.PaneBorderColor, Default.PaneBorderPenWidth );
			this.paneFill = new Fill( Default.PaneBackColor, Default.PaneBackBrush, Default.PaneBackType );

			this.isAxisRectAuto = true;
			this.axisBorder = new Border( Default.IsAxisBorderVisible, Default.AxisBorderColor, Default.AxisBorderPenWidth );
			this.axisFill = new Fill( Default.AxisBackColor, Default.AxisBackBrush, Default.AxisBackType );

			this.baseDimension = Default.BaseDimension;
			this.paneGap = Default.PaneGap;
			this.isFontsScaled = true;
            this.isPenWidthScaled = Default.IsPenWidthScaled;

            this.minClusterGap = Default.MinClusterGap;
			this.minBarGap = Default.MinBarGap;
			this.clusterScaleWidth = Default.ClusterScaleWidth;
			this.barBase = Default.BarBase;
			this.barType = Default.BarType;
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
			graphItemList = new GraphItemList( rhs.GraphItemList );
			
			this.title = rhs.Title;
			this.isShowTitle = rhs.IsShowTitle;
			this.fontSpec = (FontSpec) rhs.FontSpec.Clone();
					
			this.isIgnoreInitial = rhs.IsIgnoreInitial;
			
			this.paneBorder = (Border) rhs.PaneBorder.Clone();
			this.paneFill = (Fill) rhs.PaneFill.Clone();

			this.isAxisRectAuto = rhs.IsAxisRectAuto;
			this.axisBorder = (Border) rhs.AxisBorder.Clone();
			this.axisFill = (Fill) rhs.AxisFill.Clone();

			this.baseDimension = rhs.BaseDimension;
			this.isFontsScaled = rhs.isFontsScaled;
            this.isPenWidthScaled = rhs.isPenWidthScaled;
            this.paneGap = rhs.PaneGap;
			this.minClusterGap = rhs.MinClusterGap;
			this.minBarGap = rhs.MinBarGap;
			this.clusterScaleWidth = rhs.ClusterScaleWidth;
			this.barBase = rhs.BarBase;
			this.barType = rhs.BarType;
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
		/// </summary>
		/// <remarks>
		/// There is no obligation to call AxisChange() for manually scaled axes.  AxisChange() is only
		/// intended to handle auto scaling operations.  Call this function anytime you change, add, or
		/// remove curve data to insure that the scale range of the axes are appropriate for the data range.
		/// This method calculates
		/// a scale minimum, maximum, and step size for each axis based on the current curve data.
		/// Only the axis attributes (min, max, step) that are set to auto-range (<see cref="Axis.MinAuto"/>,
		/// <see cref="Axis.MaxAuto"/>, <see cref="Axis.StepAuto"/>) will be modified.  You must call
		/// <see cref="Control.Invalidate()"/> after calling AxisChange to make sure the display gets updated.
		/// </remarks>
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
			double	scaleFactor = this.CalcScaleFactor();

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
		/// Draw all elements in the <see cref="GraphPane"/> to the specified graphics device.
		/// </summary>
		/// <remarks>This method
		/// should be part of the Paint() update process.  Calling this routine will redraw all
		/// features of the graph.  No preparation is required other than an instantiated
		/// <see cref="GraphPane"/> object.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        public void Draw(Graphics g)
        {			
			// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
			double	scaleFactor;
			int		hStack;
			float	legendWidth, legendHeight;

			// if the size of the axisRect is determined automatically, then do so
			// otherwise, calculate the legendrect, scalefactor, hstack, and legendwidth parameters
			// but leave the axisRect alone
			if ( this.isAxisRectAuto )
                this.axisRect = CalcAxisRect(g, out scaleFactor, out hStack, out legendWidth, out legendHeight);
            else
				CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth, out legendHeight );

			// Border the whole pane
			DrawPaneFrame( g, this, scaleFactor );

			// do a sanity check on the axisRect
			if ( this.axisRect.Width < 1 || this.axisRect.Height < 1 )
				return;
			
			// Fill the axis background
			this.axisFill.Draw( g, this.axisRect );
			
			// Draw the graph features only if there is at least one curve with data
			//			if (	this.curveList.HasData() &&
			// Go ahead and draw the graph, even without data.  This makes the control
			// version still look like a graph before it is fully set up
			bool showGraf = this.xAxis.Min < this.xAxis.Max &&
							this.yAxis.Min < this.yAxis.Max &&
							this.y2Axis.Min < this.y2Axis.Max;
			if ( showGraf )
			{
				// Clip everything to the paneRect
				g.SetClip( this.paneRect );

				// Draw the Pane Title
				DrawTitle( g, this, scaleFactor );
			
				// Setup the axes from graphing
				//this.xAxis.SetupScaleData( this );
				//this.yAxis.SetupScaleData( this );
				//this.y2Axis.SetupScaleData( this );

				// Draw the Axes
				this.xAxis.Draw( g, this, scaleFactor );
				this.yAxis.Draw( g, this, scaleFactor );
				this.y2Axis.Draw( g, this, scaleFactor );
				
				// Clip the points to the actual plot area
				g.SetClip( this.axisRect );
				this.curveList.Draw( g, this, scaleFactor );
				g.SetClip( this.paneRect );
			}
				
			// Border the axis itself
			this.axisBorder.Draw( g, this, scaleFactor, this.axisRect );
			
			if ( showGraf )
			{
				// Draw the Legend
				this.legend.Draw( g, this, scaleFactor, hStack, legendWidth, legendHeight );
				
				// Draw the Text and Arrow Items
				this.graphItemList.Draw( g, this, scaleFactor );
				
				// Reset the clipping
				g.ResetClip();
			}
			

		}

		/// <summary>
		/// Calculate the <see cref="AxisRect"/> based on the <see cref="PaneRect"/>.
		/// </summary>
		/// <remarks>The axisRect
		/// is the plot area bounded by the axes, and the paneRect is the total area as
		/// specified by the client application.
		/// </remarks>
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
			float	legendWidth, legendHeight;
			
			return CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth, out legendHeight );
		}

		/// <summary>
		/// Calculate the <see cref="AxisRect"/> based on the <see cref="PaneRect"/>.
		/// </summary>
		/// <remarks>The axisRect
		/// is the plot area bounded by the axes, and the paneRect is the total area as
		/// specified by the client application.
		/// </remarks>
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
        /// The width of a single legend entry, in pixel units.  This is a temporary
        /// variable calculated by the routine for use in the Legend.Draw method.
        /// </param>
        /// <param name="legendHeight">
        /// The height of a single legend entry, in pixel units.  This is a temporary
        /// variable calculated by the routine for use in the Legend.Draw method.
        /// </param>
        /// <returns>The calculated axis rect, in pixel coordinates.</returns>
        public RectangleF CalcAxisRect( Graphics g, out double scaleFactor,
									out int hStack, out float legendWidth, out float legendHeight )
		{
			// calculate scaleFactor on "normal" pane size (BaseDimension)
			scaleFactor = this.CalcScaleFactor();

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
				SizeF titleSize = this.FontSpec.BoundingBox( g, this.title, scaleFactor );
				// Leave room for the title height, plus a line spacing of charHeight/2
				tmpRect.Y += titleSize.Height + charHeight / 2.0F;
				tmpRect.Height -= titleSize.Height + charHeight / 2.0F;
			}
			
			// Calculate the legend rect, and back it out of the current axisRect
			this.legend.CalcRect( g, this, scaleFactor, ref tmpRect,
								out hStack, out legendWidth, out legendHeight );

			return tmpRect;
		}

		/// <summary>
		/// This method will set the <see cref="Axis.MinSpace"/> property for all three axes;
		/// <see cref="XAxis"/>, <see cref="YAxis"/>, and <see cref="Y2Axis"/>.
		/// </summary>
		/// <remarks>The <see cref="Axis.MinSpace"/>
		/// is calculated using the currently required space multiplied by a fraction
		/// (<paramref>bufferFraction</paramref>).
		/// The currently required space is calculated using <see cref="Axis.CalcSpace"/>, and is
		/// based on current data ranges, font sizes, etc.  The "space" is actually the amount of space
		/// required to fit the tic marks, scale labels, and axis title.
		/// The calculation is done by calling the <see cref="Axis.SetMinSpaceBuffer"/> method for
		/// each <see cref="Axis"/>.
		/// </remarks>
		/// <param name="g">A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.</param>
		/// <param name="bufferFraction">The amount of space to allocate for the axis, expressed
		/// as a fraction of the currently required space.  For example, a value of 1.2 would
		/// allow for 20% extra above the currently required space.</param>
		/// <param name="isGrowOnly">If true, then this method will only modify the <see cref="Axis.MinSpace"/>
		/// property if the calculated result is more than the current value.</param>
		public void SetMinSpaceBuffer( Graphics g, float bufferFraction, bool isGrowOnly )
		{
			this.xAxis.SetMinSpaceBuffer( g, this, bufferFraction, isGrowOnly );
			this.yAxis.SetMinSpaceBuffer( g, this, bufferFraction, isGrowOnly  );
			this.y2Axis.SetMinSpaceBuffer( g, this, bufferFraction, isGrowOnly  );
		}

		/// <summary>
		/// Draw the <see cref="GraphPane"/> <see cref="Title"/> on the graph
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>		
		public void DrawTitle( Graphics g, GraphPane pane, double scaleFactor )
		{	
			// only draw the title if it's required
			if ( this.isShowTitle )
			{
				SizeF size = this.FontSpec.BoundingBox( g, this.title, scaleFactor );
				
				// use the internal fontSpec class to draw the text using user-specified and/or
				// default attributes.
				this.FontSpec.Draw( g, pane, this.title,
							( this.paneRect.Left + this.paneRect.Right ) / 2,
							this.paneRect.Top + this.ScaledGap( scaleFactor ) + size.Height / 2.0F,
							AlignH.Center, AlignV.Center, scaleFactor );
			}
		}
		
		/// <summary>
		/// Draw the border border around the <see cref="PaneRect"/> area.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
        /// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
        /// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
        /// </param>		
        public void DrawPaneFrame(Graphics g, GraphPane pane, double scaleFactor)
        {
			// Erase the pane background
			Brush brush = this.paneFill.MakeBrush( this.paneRect );
			//SolidBrush brush = new SolidBrush( this.paneBackColor );
			g.FillRectangle( brush, this.paneRect );
			brush.Dispose();

			this.paneBorder.Draw( g, pane, scaleFactor, paneRect );
		}

		/// <summary>
		/// Calculate the scaling factor based on the ratio of the current <see cref="PaneRect"/> dimensions and
		/// the <see cref="BaseDimension"/>.
		/// </summary>
		/// <remarks>This scaling factor is used to proportionally scale the
		/// features of the <see cref="GraphPane"/> so that small graphs don't have huge fonts, and vice versa.
		/// The scale factor represents a linear multiple to be applied to font sizes, symbol sizes, tic sizes,
		/// gap sizes, pen widths, etc.  The units of the scale factor are "World Pixels" per "Standard Point".
		/// If any object size, in points, is multiplied by this scale factor, the result is the size, in pixels,
		/// that the object should be drawn using the standard GDI+ drawing instructions.  A "Standard Point"
		/// is a dimension based on points (1/72nd inch) assuming that the <see cref="PaneRect"/> size
		/// matches the <see cref="BaseDimension"/>.
		/// Note that "World Pixels" will still be transformed by the GDI+ transform matrices to result
		/// in "Output Device Pixels", but "World Pixels" are the reference basis for the drawing commands.
		/// The use of the scale factor depends upon the settings of <see cref="GraphPane.IsFontsScaled"/> and
		/// <see cref="GraphPane.IsPenWidthScaled"/>.
        /// </remarks>
		/// <returns>
		/// A double precision value representing the scaling factor to use for the rendering calculations.
		/// </returns>
		/// <seealso cref="GraphPane.IsFontsScaled"/>
		/// <seealso cref="GraphPane.IsPenWidthScaled"/>
		/// <seealso cref="GraphPane.BaseDimension"/>
		public double CalcScaleFactor()
		{
			double scaleFactor; //, xInch, yInch;
			const double ASPECTLIMIT = 1.5;
			
			// Assume the standard width (BaseDimension) is 8.0 inches
			// Therefore, if the paneRect is 8.0 inches wide, then the fonts will be scaled at 1.0
			// if the paneRect is 4.0 inches wide, the fonts will be half-sized.
			// if the paneRect is 16.0 inches wide, the fonts will be double-sized.
		
			// if font scaling is turned off, then always return a 1.0 scale factor
			if ( !this.isFontsScaled )
				return 1.0;

			// Scale the size depending on the client area width in linear fashion
            if (paneRect.Height <= 0)
                return 1.0;
            double length = paneRect.Width;
            double aspect = paneRect.Width / paneRect.Height;
            if ( aspect > ASPECTLIMIT )
                length = paneRect.Height * ASPECTLIMIT;
            if ( aspect < 1.0 / ASPECTLIMIT )
                length = paneRect.Width * ASPECTLIMIT;

            scaleFactor = length / (this.baseDimension * 72);

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
		/// are not defined as arguments to the
		/// <see cref="AddCurve(string,double[],double[],Color)"/> method.</returns>
		public LineItem AddCurve( string label, double[] x, double[] y, Color color )
		{
			LineItem curve = new LineItem( label, x, y, color, SymbolType.Default );
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
		/// are not defined as arguments to the
		/// <see cref="AddCurve(string,PointPairList,Color)"/> method.</returns>
		public LineItem AddCurve( string label, PointPairList points, Color color )
		{
			LineItem curve = new LineItem( label, points, color, SymbolType.Default );
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
		/// are not defined as arguments to the
		/// <see cref="AddCurve(string,double[],double[],Color,SymbolType)"/> method.</returns>
		public LineItem AddCurve( string label, double[] x, double[] y,
			Color color, SymbolType symbolType )
		{
			LineItem curve = new LineItem( label, x, y, color, symbolType );
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
		/// are not defined as arguments to the
		/// <see cref="AddCurve(string,PointPairList,Color,SymbolType)"/> method.</returns>
		public LineItem AddCurve( string label, PointPairList points,
			Color color, SymbolType symbolType )
		{
			LineItem curve = new LineItem( label, points, color, symbolType );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add an error bar set (<see cref="ErrorBarItem"/> object) to the plot with
		/// the given data points (<see cref="PointPairList"/>) and properties.
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
		/// <param name="baseValue">An array of double precision values that define the
		/// base value (the bottom) of the bars for this curve.
		/// </param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <returns>An <see cref="ErrorBarItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddErrorBar(string,PointPairList,Color)"/> method.</returns>
		public ErrorBarItem AddErrorBar( string label, double[] x, double[] y,
										double[] baseValue, Color color )
		{
			ErrorBarItem curve = new ErrorBarItem( label, new PointPairList( x, y, baseValue),
													color );
			this.curveList.Add( curve );
			
			return curve;
		}
		/// <summary>
		/// Add an error bar set (<see cref="ErrorBarItem"/> object) to the plot with
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
		/// <returns>An <see cref="ErrorBarItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddErrorBar(string,PointPairList,Color)"/> method.</returns>
		public ErrorBarItem AddErrorBar( string label, PointPairList points, Color color )
		{
			ErrorBarItem curve = new ErrorBarItem( label, points, color );
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
		/// <returns>A <see cref="CurveItem"/> class for the newly created bar curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddBar(string,PointPairList,Color)"/> method.</returns>
		public BarItem AddBar( string label, PointPairList points, Color color )
		{
			BarItem curve = new BarItem( label, points, color );
			this.curveList.Add( curve );
			
			return curve;
		}
		
		/// <summary>
		/// Add a bar type curve (<see cref="CurveItem"/> object) to the plot with
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
		/// <param name="color">The color to used for the bars</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created bar curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddBar(string,double[],double[],Color)"/> method.</returns>
		public BarItem AddBar( string label, double[] x, double[] y, Color color )
		{
			BarItem curve = new BarItem( label, x, y, color );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a "High-Low" bar type curve (<see cref="HiLowBarItem"/> object) to the plot with
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
		/// <param name="baseVal">An array of double precision values that define the
		/// base value (the bottom) of the bars for this curve.
		/// </param>
		/// <param name="color">The color to used for the bars</param>
		/// <returns>A <see cref="HiLowBarItem"/> class for the newly created bar curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddHiLowBar(string,double[],double[],double[],Color)"/> method.</returns>
		public HiLowBarItem AddHiLowBar( string label, double[] x, double[] y,
											double[] baseVal, Color color )
		{
			HiLowBarItem curve = new HiLowBarItem( label, x, y, baseVal, color );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a hi-low bar type curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (<see cref="PointPairList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value Trio's that define
		/// the X, Y, and lower dependent values for this curve</param>
		/// <param name="color">The color to used to fill the bars</param>
		/// <returns>A <see cref="HiLowBarItem"/> class for the newly created bar curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddHiLowBar(string,PointPairList,Color)"/> method.</returns>
		public HiLowBarItem AddHiLowBar( string label, PointPairList points, Color color )
		{
			HiLowBarItem curve = new HiLowBarItem( label, points, color );
			this.curveList.Add( curve );
			
			return curve;
		}
		
		#endregion

	#region General Utility Methods
		/// <summary>
		/// Transform a data point from the specified coordinate type
		/// (<see cref="CoordType"/>) to screen coordinates (pixels).
		/// </summary>
		/// <remarks>This method implicitly assumes that <see cref="AxisRect"/>
		/// has already been calculated via <see cref="AxisChange"/> or
		/// <see cref="Draw"/> methods, or the <see cref="AxisRect"/> is
		/// set manually (see <see cref="IsAxisRectAuto"/>).</remarks>
		/// <param name="ptF">The X,Y pair that defines the point in user
		/// coordinates.</param>
		/// <param name="coord">A <see cref="CoordType"/> type that defines the
		/// coordinate system in which the X,Y pair is defined.</param>
		/// <returns>A point in screen coordinates that corresponds to the
		/// specified user point.</returns>
		public PointF GeneralTransform( PointF ptF, CoordType coord )
		{
			// Setup the scaling data based on the axis rect
			this.xAxis.SetupScaleData( this );
			this.yAxis.SetupScaleData( this );
			this.y2Axis.SetupScaleData( this );

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
		/// <remarks>This method implicitly assumes that <see cref="AxisRect"/>
		/// has already been calculated via <see cref="AxisChange"/> or
		/// <see cref="Draw"/> methods, or the <see cref="AxisRect"/> is
		/// set manually (see <see cref="IsAxisRectAuto"/>).</remarks>
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
			// Setup the scaling data based on the axis rect
			this.xAxis.SetupScaleData( this );
			this.yAxis.SetupScaleData( this );
			this.y2Axis.SetupScaleData( this );

			x = this.XAxis.ReverseTransform( ptF.X );
			y = this.YAxis.ReverseTransform( ptF.Y );
			y2 = this.Y2Axis.ReverseTransform( ptF.Y );
		}

		/// <summary>
		/// Find the object that lies closest to the specified mouse (screen) point.
		/// </summary>
		/// <remarks>
		/// This method will search through all of the graph objects, such as
		/// <see cref="Axis"/>, <see cref="Legend"/>, <see cref="GraphPane.Title"/>,
		/// <see cref="GraphItem"/>, and <see cref="CurveItem"/>.
		/// If the mouse point is within the bounding box of the items (or in the case
		/// of <see cref="ArrowItem"/> and <see cref="CurveItem"/>, within
		/// <see cref="Default.NearestTol"/> pixels), then the object will be returned.
		/// You must check the type of the object to determine what object was
		/// selected (for example, "if ( object is Legend ) ...").  The
		/// <see paramref="index"/> parameter returns the index number of the item
		/// within the selected object (such as the point number within a
		/// <see cref="CurveItem"/> object.
		/// </remarks>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="nearestObj">A reference to the nearest object to the
		/// specified screen point.  This can be any of <see cref="Axis"/>,
		/// <see cref="Legend"/>, <see cref="GraphPane.Title"/>,
		/// <see cref="TextItem"/>, <see cref="ArrowItem"/>, or <see cref="CurveItem"/>.
		/// Note: If the pane title is selected, then the <see cref="GraphPane"/> object
		/// will be returned.
		/// </param>
		/// <param name="index">The index number of the item within the selected object
		/// (where applicable).  For example, for a <see cref="CurveItem"/> object,
		/// <see paramref="index"/> will be the index number of the nearest data point,
		/// accessible via <see cref="CurveItem.Points">CurveItem.Points[index]</see>.
		/// index will be -1 if no data points are available.</param>
		/// <returns>true if an object was found, false otherwise.</returns>
		/// <seealso cref="FindNearestObject"/>
		public bool FindNearestObject( PointF mousePt, Graphics g, 
										out object nearestObj, out int index )
		{
			nearestObj = null;
			index = -1;

			// Make sure that the axes & data are being drawn
			if ( 	this.xAxis.Min < this.xAxis.Max &&
					this.yAxis.Min < this.yAxis.Max &&
					this.y2Axis.Min < this.y2Axis.Max )
			{
				double		scaleFactor;
				int			hStack;
				float		legendWidth, legendHeight;
				RectangleF	tmpRect;
	
				// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
				RectangleF tmpAxisRect = CalcAxisRect( g, out scaleFactor, out hStack, out legendWidth,
                                            out legendHeight );
	
				// See if the point is in a GraphItem
				if ( this.GraphItemList.FindPoint( mousePt, this, g, scaleFactor, out index ) )
				{
					nearestObj = this.GraphItemList[index];
					return true;
				}
								
				// See if the point is in the legend
				if ( this.Legend.FindPoint( mousePt, this, scaleFactor, hStack, legendWidth, out index ) )
				{
					nearestObj = this.Legend;
					return true;
				}
				
				// See if the point is in the Pane Title
				if ( this.isShowTitle )
				{
					SizeF size = this.FontSpec.BoundingBox( g, this.title, scaleFactor );
					tmpRect = new RectangleF( ( this.paneRect.Left + this.paneRect.Right - size.Width ) / 2,
												this.paneRect.Top + this.ScaledGap( scaleFactor ),
												size.Width, size.Height );
					if ( tmpRect.Contains( mousePt ) )
					{
						nearestObj = this;
						return true;
					}
				}

				// See if the point is in the Y Axis
				tmpRect = new RectangleF( this.paneRect.Left, tmpAxisRect.Top,
										tmpAxisRect.Left - this.paneRect.Left, tmpAxisRect.Height );
				if ( tmpRect.Contains( mousePt ) )
				{
					nearestObj = this.YAxis;
					return true;
				}
				
				// See if the point is in the Y2 Axis
				tmpRect = new RectangleF( tmpAxisRect.Right, tmpAxisRect.Top,
										this.paneRect.Right - tmpAxisRect.Right, tmpAxisRect.Height );
				if ( tmpRect.Contains( mousePt ) )
				{
					nearestObj = this.Y2Axis;
					return true;
				}
				
				// See if the point is in the X Axis
				tmpRect = new RectangleF( tmpAxisRect.Left, tmpAxisRect.Bottom,
										tmpAxisRect.Width, this.paneRect.Bottom - tmpAxisRect.Bottom );
				if ( tmpRect.Contains( mousePt ) )
				{
					nearestObj = this.XAxis;
					return true;
				}
				
				CurveItem curve;
				// See if it's a data point
				if ( FindNearestPoint( mousePt, out curve, out index ) )
				{
					nearestObj = curve;
					return true;
				}
			}
			
			return false;
		}

		/// <summary>
		/// Find the data point that lies closest to the specified mouse (screen)
		/// point.
		/// </summary>
		/// <remarks>
		/// This method will search only through the points for the specified
		/// curve to determine which point is
		/// nearest the mouse point.  It will only consider points that are within
		/// <see cref="Default.NearestTol"/> pixels of the screen point.
		/// </remarks>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="targetCurve">A <see cref="CurveItem"/> object containing
		/// the data points to be searched.</param>
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
		public bool FindNearestPoint( PointF mousePt, CurveItem targetCurve,
			out CurveItem nearestCurve, out int iNearest )
		{
			CurveList targetCurveList = new CurveList();
			targetCurveList.Add( targetCurve );
			return FindNearestPoint( mousePt, targetCurveList,
					out nearestCurve, out iNearest );
		}

		/// <summary>
		/// Find the data point that lies closest to the specified mouse (screen)
		/// point.
		/// </summary>
		/// <remarks>
		/// This method will search through all curves in
		/// <see cref="GraphPane.CurveList"/> to find which point is
		/// nearest.  It will only consider points that are within
		/// <see cref="Default.NearestTol"/> pixels of the screen point.
		/// </remarks>
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
			return FindNearestPoint( mousePt, this.curveList,
					out nearestCurve, out iNearest );
		}

		/// <summary>
		/// Find the data point that lies closest to the specified mouse (screen)
		/// point.
		/// </summary>
		/// <remarks>
		/// This method will search through the specified list of curves to find which point is
		/// nearest.  It will only consider points that are within
		/// <see cref="Default.NearestTol"/> pixels of the screen point, and it will
		/// only consider <see cref="CurveItem"/>'s that are in 
		/// <paramref name="targetCurveList"/>.
		/// </remarks>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="targetCurveList">A <see cref="CurveList"/> object containing
		/// a subset of <see cref="CurveItem"/>'s to be searched.</param>
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
		public bool FindNearestPoint( PointF mousePt, CurveList targetCurveList,
			out CurveItem nearestCurve, out int iNearest )
		{
			CurveItem nearestBar = null;
			int iNearestBar = -1;
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

			BarValueHandler valueHandler = new BarValueHandler( this );

			double	xPixPerUnit = axisRect.Width / ( xAxis.Max - xAxis.Min );
			double	yPixPerUnit = axisRect.Height / ( yAxis.Max - yAxis.Min );
			double	y2PixPerUnit = axisRect.Height / ( y2Axis.Max - y2Axis.Min );

			double	yPixPerUnitAct, yAct, yMinAct, yMaxAct;
			double	minDist = 1e20;
			double	xVal, yVal, dist=99999, distX, distY;
			double	tolSquared = Default.NearestTol * Default.NearestTol;

			int		iBar = 0;

			foreach ( CurveItem curve in targetCurveList )
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
				float barWidth = curve.GetBarWidth( this );
				double barWidthUserHalf;
				if ( curve.BaseAxis(this) == XAxis )
					barWidthUserHalf = barWidth / xPixPerUnit / 2.0;
				else
					barWidthUserHalf = barWidth / yPixPerUnit / 2.0;

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

							if ( curve.IsBar || curve is ErrorBarItem ||
									curve is HiLowBarItem )
							{
								double baseVal, lowVal, hiVal;
								valueHandler.GetBarValues( curve, iPt, out baseVal,
										out lowVal, out hiVal );
								if ( curve.BaseAxis( this ) is XAxis )
								{
									
									double centerVal = valueHandler.BarCenterValue( curve, barWidth, iPt, xVal, iBar );
									
									if (	x < centerVal - barWidthUserHalf ||
											x > centerVal + barWidthUserHalf ||
											y < lowVal || y > hiVal )
										continue;
								}
								else
								{
									double centerVal = valueHandler.BarCenterValue( curve, barWidth, iPt, yVal, iBar );
									
									if (	y < centerVal - barWidthUserHalf ||
											y > centerVal + barWidthUserHalf ||
											x < lowVal || x > hiVal )
										continue;
								}

								if ( nearestBar == null )
								{
									iNearestBar = iPt;
									nearestBar = curve;
								}
							}
							else
							{
								distX = (xVal - x) * xPixPerUnit;
								distY = (yVal - yAct) * yPixPerUnitAct;
								dist = distX * distX + distY * distY;

								if ( dist >= minDist )
									continue;
									
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

			if ( nearestCurve is LineItem )
			{
				float halfSymbol = (float) ( ((LineItem)nearestCurve).Symbol.Size *
						CalcScaleFactor() / 2 );
				minDist -= halfSymbol * halfSymbol;
				if ( minDist < 0 )
					minDist = 0;
			}

			if ( minDist >= tolSquared && nearestBar != null )
			{
				// if no point met the tolerance, but a bar was found, use it
				nearestCurve = nearestBar;
				iNearest = iNearestBar;
				return true;
			}
			else if ( minDist < tolSquared )
			{
				// Did we find a close point, and is it within the tolerance?
				// (minDist is the square of the distance in pixel units)
				return true;
			}
			else  // otherwise, no valid point found
				return false;
		}

		/// <summary>
		/// Determine the width, in screen pixel units, of each bar cluster including
		/// the cluster gaps and bar gaps.
		/// </summary>
		/// <remarks>This method calls the <see cref="Axis.GetClusterWidth"/>
		/// method for the base <see cref="Axis"/> for <see cref="Bar"/> graphs
		/// (the base <see cref="Axis"/> is assigned by the <see cref="GraphPane.BarBase"/>
		/// property).
		/// </remarks>
		/// <seealso cref="ZedGraph.BarBase"/>
		/// <seealso cref="GraphPane.BarBase"/>
		/// <seealso cref="Axis.GetClusterWidth"/>
		/// <seealso cref="GraphPane.BarType"/>
		/// <returns>The width of each bar cluster, in pixel units</returns>
		public float GetClusterWidth()
		{
			return BarBaseAxis().GetClusterWidth( this );
		}
		
		/// <summary>
		/// Determine the <see cref="Axis"/> from which the <see cref="Bar"/> charts are based.
		/// </summary>
		/// <seealso cref="ZedGraph.BarBase"/>
		/// <seealso cref="GraphPane.BarBase"/>
		/// <seealso cref="Axis.GetClusterWidth"/>
		/// <returns>The <see cref="Axis"/> class for the axis from which the bars are based</returns>
		public Axis BarBaseAxis()
		{
			Axis barAxis;
			if ( this.BarBase == BarBase.Y )
				barAxis = this.YAxis;
			else if ( this.BarBase == BarBase.Y2 )
				barAxis = this.Y2Axis;
			else
				barAxis = this.XAxis;
			
			return barAxis;
		}

	#endregion
	}
}

