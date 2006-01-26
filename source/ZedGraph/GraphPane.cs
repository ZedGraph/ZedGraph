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
using System.Collections ;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ComponentModel;

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
	/// <version> $Revision: 3.56 $ $Date: 2006-01-26 05:46:03 $ </version>
	[Serializable]
	public class GraphPane : PaneBase, ICloneable, ISerializable
	{
	#region Private Fields
	
		// Item subclasses ////////////////////////////////////////////////////////////////////
		
		/// <summary>Private field instance of the <see cref="ZedGraph.XAxis"/> class.  Use the
		/// public property <see cref="GraphPane.XAxis"/> to access this class.</summary>
		private XAxis		xAxis;
		/// <summary>Private field instance of the <see cref="ZedGraph.AxisList"/> class.  Use the
		/// public property <see cref="GraphPane.YAxisList"/> to access this class.</summary>
		private YAxisList	yAxisList;
		/// <summary>Private field instance of the <see cref="ZedGraph.AxisList"/> class.  Use the
		/// public property <see cref="GraphPane.Y2AxisList"/> to access this class.</summary>
		private Y2AxisList	y2AxisList;
		// / <summary>Private field instance of the <see cref="ZedGraph.Y2Axis"/> class.  Use the
		// / public property <see cref="GraphPane.Y2Axis"/> to access this class.</summary>
		//private Y2Axis		y2Axis;
		/// <summary>Private field instance of the <see cref="ZedGraph.CurveList"/> class.  Use the
		/// public property <see cref="GraphPane.CurveList"/> to access this class.</summary>
		private CurveList	curveList;
						
		/// <summary>
		/// private value that contains a <see cref="ZoomStateStack"/>, which stores prior
		/// <see cref="ZoomState"/> objects containing scale range information.  This enables
		/// zooming and panning functionality for the <see cref="ZedGraphControl"/>.
		/// </summary>
		private ZoomStateStack zoomStack;
		
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
		/// <summary> private field that determines if the auto-scaled axis ranges will subset the
		/// data points based on any manually set scale range values.  Use the public property
		/// <see cref="IsBoundedRanges"/> to access this value.</summary>
		/// <remarks>The bounds provide a means to subset the data.  For example, if all the axes are set to
		/// autoscale, then the full range of data are used.  But, if the XAxis.Min and XAxis.Max values
		/// are manually set, then the Y data range will reflect the Y values within the bounds of
		/// XAxis.Min and XAxis.Max.</remarks>
		private bool		isBoundedRanges;
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

		/// <summary>Private field that determines how the <see cref="LineItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.LineType"/> enum
		/// for the individual types available.
		/// To access this value, use the public property <see cref="LineType"/>.
		/// </summary>
		/// <seealso cref="Default.LineType"/>
		private LineType lineType;
		
		/// <summary>
		/// The rectangle that contains the area bounded by the axes, in
		/// pixel units
		/// </summary>
		private RectangleF	axisRect;			// The area of the pane defined by the axes
		/// <summary>
		/// The largest square contained in <see cref="GraphPane.axisRect"/>, in
		/// pixel units, used for drawing <see cref="PieItem"/> object.
		/// Use the public property <see cref="PieRect"/> to access this value.
		/// </summary>
		private RectangleF	pieRect;			
		
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="GraphPane"/> class.
		/// </summary>
		public new struct Default
		{			
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
			/// The default settings for the <see cref="Axis"/> scale bounded ranges option
			/// (<see cref="GraphPane.IsBoundedRanges"/> property).
			/// true to have the auto-scale-range code subset the data according to any
			/// manually set scale values, false otherwise.
			/// </summary>
			public static bool IsBoundedRanges = true;

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
			/// <summary>The default value for the <see cref="GraphPane.LineType"/> property, which
			/// determines if the lines are drawn in normal or "stacked" mode.  See the
			/// <see cref="ZedGraph.LineType"/> for more information.
			/// </summary>
			/// <seealso cref="GraphPane.LineType"/>
			public static LineType LineType = LineType.Normal;
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
		/// Gets or sets the list of <see cref="CurveItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="CurveList"/> collection object</value>
		public CurveList CurveList
		{
			get { return curveList; }
			set { curveList = value; }
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
		/// Accesses the primary <see cref="YAxis"/> for this graph
		/// </summary>
		/// <value>A reference to a <see cref="YAxis"/> object</value>
		/// <seealso cref="YAxisList" />
		/// <seealso cref="Y2AxisList" />
		public YAxis YAxis
		{
			get { return yAxisList[0] as YAxis; }
		}
		/// <summary>
		/// Accesses the primary <see cref="Y2Axis"/> for this graph
		/// </summary>
		/// <value>A reference to a <see cref="Y2Axis"/> object</value>
		/// <seealso cref="YAxisList" />
		/// <seealso cref="Y2AxisList" />
		public Y2Axis Y2Axis
		{
			get { return y2AxisList[0] as Y2Axis; }
		}

		/// <summary>
		/// 
		/// </summary>
		public YAxisList YAxisList
		{
			get { return yAxisList; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Y2AxisList Y2AxisList
		{
			get { return y2AxisList; }
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
		/// <summary> Gets or sets a boolean value that determines if the auto-scaled axis ranges will
		/// subset the data points based on any manually set scale range values.</summary>
		/// <remarks>The bounds provide a means to subset the data.  For example, if all the axes are set to
		/// autoscale, then the full range of data are used.  But, if the XAxis.Min and XAxis.Max values
		/// are manually set, then the Y data range will reflect the Y values within the bounds of
		/// XAxis.Min and XAxis.Max.  Set to true to subset the data, or false to always include
		/// all data points when calculating scale ranges.</remarks>
		public bool IsBoundedRanges
		{
			get { return isBoundedRanges; }
			set { isBoundedRanges = value; }
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

		/// <summary>Determines how the <see cref="LineItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.LineType"/> enum
		/// for the individual types available.
		/// </summary>
		/// <seealso cref="Default.LineType"/>
		public LineType LineType
		{
			get { return lineType; }
			set { lineType = value; }
		}

		/// <summary>
		/// Gets a value that indicates whether or not the <see cref="ZoomStateStack" /> for
		/// this <see cref="GraphPane" /> is empty.  Note that this value is only used for
		/// the <see cref="ZedGraphControl" />.
		/// </summary>
		public bool IsZoomed
		{
			get { return !zoomStack.IsEmpty; }
		}

		/// <summary>
		/// Gets a reference to the <see cref="ZoomStateStack" /> for this <see cref="GraphPane" />.
		/// </summary>
		public ZoomStateStack ZoomStack
		{
			get { return zoomStack; }
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
		/// </summary>
		/// <remarks>
		/// If you have a need to set the axisRect manually, such as you have multiple graphs
		/// on a page and you want to line up the edges perfectly, you can set this value
		/// to false.  If you set this value to false, you must also manually set
		/// the <see cref="AxisRect"/> property.  Note that the <see cref="PieRect"/> (for Pie
		/// charts) is a function of the <see cref="AxisRect"/>.  Therefore, <see cref="PieRect"/>
		/// will also have to be manually calculated if <see cref="IsAxisRectAuto"/> is false.
		/// You can easily determine the axisRect that ZedGraph would have
		/// calculated by calling the <see cref="CalcAxisRect(Graphics)"/> method, which returns
		/// an axis rect sized for the current data range, scale sizes, etc.
		/// </remarks>
		/// <value>true to have ZedGraph calculate the axisRect, false to do it yourself</value>
		/// <seealso cref="PieItem.CalcPieRect"/>
		public bool IsAxisRectAuto
		{
			get { return isAxisRectAuto; }
			set { isAxisRectAuto = value; }
		}
		/// <summary>
		/// Gets or sets a <see cref="RectangleF"/> that determines the size of the Pie.
		/// </summary>
		/// <remarks>This rectangle is normally square, and slightly smaller than the <see cref="AxisRect"/>.
		/// If you want to set this rectangle manually, you will need to set <see cref="IsAxisRectAuto"/> to
		/// false as well.
		/// </remarks>
		/// <value>The rectangle units are in screen pixels</value>
		public RectangleF PieRect
		{
			get { return pieRect; }
			set { pieRect = value; }
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
		/// graphs will be displayed.
		/// </summary>
		/// <remarks>The base axis is the axis from which the bars grow with
		/// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
		/// </remarks>
		/// <seealso cref="Default.BarBase"/>
		public BarBase BarBase
		{
			get { return barBase; }
			set { barBase = value; }
		}
		/// <summary>Determines how the <see cref="BarItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
		/// for the individual types available.
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
		/// Default Constructor.  Sets the <see cref="PaneBase.PaneRect"/> to (0, 0, 500, 375), and
		/// sets the <see cref="PaneBase.Title"/> and <see cref="Axis.Title"/> values to empty
		/// strings.
		/// </summary>
		public GraphPane() : this( new RectangleF( 0, 0, 500, 375 ), "", "", "" )
		{
		}

		/// <summary>
		/// Constructor for the <see cref="GraphPane"/> object.  This routine will
		/// initialize all member variables and classes, setting appropriate default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="paneRect"> A rectangular screen area where the graph is to be displayed.
		/// This area can be any size, and can be resize at any time using the
		/// <see cref="PaneBase.PaneRect"/> property.
		/// </param>
		/// <param name="paneTitle">The <see cref="PaneBase.Title"/> for this <see cref="GraphPane"/></param>
		/// <param name="xTitle">The <see cref="Axis.Title"/> for the <see cref="XAxis"/></param>
		/// <param name="yTitle">The <see cref="Axis.Title"/> for the <see cref="YAxis"/></param>
		public GraphPane( RectangleF paneRect, string paneTitle,
			string xTitle, string yTitle ) : base( paneTitle, paneRect )
		{
			xAxis = new XAxis( xTitle );

			yAxisList = new YAxisList();
			y2AxisList = new Y2AxisList();

			yAxisList.Add( new YAxis( yTitle ) );
			y2AxisList.Add( new Y2Axis( string.Empty ) );

			curveList = new CurveList();
			zoomStack = new ZoomStateStack();
								
			this.isIgnoreInitial = Default.IsIgnoreInitial;
			this.isBoundedRanges = Default.IsBoundedRanges;
			
			this.isAxisRectAuto = true;
			this.axisBorder = new Border( Default.IsAxisBorderVisible, Default.AxisBorderColor, Default.AxisBorderPenWidth );
			this.axisFill = new Fill( Default.AxisBackColor, Default.AxisBackBrush, Default.AxisBackType );

			this.minClusterGap = Default.MinClusterGap;
			this.minBarGap = Default.MinBarGap;
			this.clusterScaleWidth = Default.ClusterScaleWidth;
			this.barBase = Default.BarBase;
			this.barType = Default.BarType;
			
			this.lineType = Default.LineType;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The GraphPane object from which to copy</param>
		public GraphPane( GraphPane rhs ) : base( rhs )
		{
			xAxis = new XAxis( rhs.XAxis );

			yAxisList = new YAxisList( rhs.yAxisList );
			y2AxisList = new Y2AxisList( rhs.y2AxisList );

			curveList = new CurveList( rhs.CurveList );
			zoomStack = new ZoomStateStack( rhs.zoomStack );
			
			this.isIgnoreInitial = rhs.IsIgnoreInitial;
			this.isBoundedRanges = rhs.isBoundedRanges;
			
			this.isAxisRectAuto = rhs.IsAxisRectAuto;
			this.axisBorder = (Border) rhs.AxisBorder.Clone();
			this.axisFill = (Fill) rhs.AxisFill.Clone();

			this.minClusterGap = rhs.MinClusterGap;
			this.minBarGap = rhs.MinBarGap;
			this.clusterScaleWidth = rhs.ClusterScaleWidth;
			this.barBase = rhs.BarBase;
			this.barType = rhs.BarType;
			
			this.lineType = rhs.LineType;
		} 

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the GraphPane</returns>
		public override object Clone()
		{ 
			return new GraphPane( this ); 
		}
	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		//changed to 2 when yAxisList and y2AxisList were added
		public const int schema2 = 2;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected GraphPane( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			xAxis = (XAxis) info.GetValue( "xAxis", typeof(XAxis) );
			if ( sch < 2 )
			{
				YAxis yx = (YAxis) info.GetValue( "yAxis", typeof(YAxis) );
				Y2Axis y2x = (Y2Axis) info.GetValue( "y2Axis", typeof(Y2Axis) );
				yAxisList = new YAxisList();
				yAxisList.Add( yx );
				y2AxisList = new Y2AxisList();
				y2AxisList.Add( y2x );
			}
			else
			{
				yAxisList = (YAxisList) info.GetValue( "yAxisList", typeof(YAxisList) );
				y2AxisList = (Y2AxisList) info.GetValue( "y2AxisList", typeof(Y2AxisList) );
			}

			curveList = (CurveList) info.GetValue( "curveList", typeof(CurveList) );

			isAxisRectAuto = info.GetBoolean( "isAxisRectAuto" );
			axisFill = (Fill) info.GetValue( "axisFill", typeof(Fill) );
			axisBorder = (Border) info.GetValue( "axisBorder", typeof(Border) );

			isIgnoreInitial = info.GetBoolean( "isIgnoreInitial" );
			isBoundedRanges = info.GetBoolean( "isBoundedRanges" );
			isIgnoreMissing = info.GetBoolean( "isIgnoreMissing" );

			minClusterGap = info.GetSingle( "minClusterGap" );
			minBarGap = info.GetSingle( "minBarGap" );

			barBase = (BarBase) info.GetValue( "barBase", typeof(BarBase) );
			barType = (BarType) info.GetValue( "barType", typeof(BarType) );

			clusterScaleWidth = info.GetDouble( "clusterScaleWidth" );
			axisRect = (RectangleF) info.GetValue( "axisRect", typeof(RectangleF) );
			lineType = (LineType) info.GetValue( "lineType", typeof(LineType) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );

			info.AddValue( "schema2", schema2 );

			info.AddValue( "xAxis", xAxis );
			info.AddValue( "yAxisList", yAxisList );
			info.AddValue( "y2AxisList", y2AxisList );
			info.AddValue( "curveList", curveList );

			info.AddValue( "isAxisRectAuto", isAxisRectAuto );
			info.AddValue( "axisFill", axisFill );
			info.AddValue( "axisBorder", axisBorder );

			info.AddValue( "isIgnoreInitial", isIgnoreInitial );
			info.AddValue( "isBoundedRanges", isBoundedRanges );
			info.AddValue( "isIgnoreMissing", isIgnoreMissing );

			info.AddValue( "minClusterGap", minClusterGap );
			info.AddValue( "minBarGap", minBarGap );

			info.AddValue( "barBase", barBase );
			info.AddValue( "barType", barType );

			info.AddValue( "clusterScaleWidth", clusterScaleWidth );

			info.AddValue( "axisRect", axisRect );

			info.AddValue( "lineType", lineType );
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
			//double	xMin, xMax, yMin, yMax, y2Min, y2Max;

			// Get the scale range of the data (all curves)
			this.curveList.GetRange( /* out xMin, out xMax, out yMin,
				out yMax, out y2Min, out y2Max, */
				this.isIgnoreInitial, this.isBoundedRanges, this );

			// Determine the scale factor
			float	scaleFactor = this.CalcScaleFactor();

			// For pie charts, go ahead and turn off the axis displays if it's only pies
			if ( this.CurveList.IsPieOnly )
			{
				//don't want to display axis or border if there's only pies
				this.XAxis.IsVisible = false ;				
				this.YAxis.IsVisible = false ;
				this.Y2Axis.IsVisible = false ;
				this.axisBorder.IsVisible = false ;
				//this.Legend.Position = LegendPos.TopCenter;
			}

			// if the AxisRect is not yet determined, then pick a scale based on a default AxisRect
			// size (using 75% of PaneRect -- code is in Axis.CalcMaxLabels() )
			// With the scale picked, call CalcAxisRect() so calculate a real AxisRect
			// then let the scales re-calculate to make sure that the assumption was ok
			if ( this.isAxisRectAuto )
			{
				this.xAxis.Scale.PickScale( this, g, scaleFactor );
				foreach ( Axis axis in yAxisList )
					axis.Scale.PickScale( this, g, scaleFactor );
				foreach ( Axis axis in y2AxisList )
					axis.Scale.PickScale( this, g, scaleFactor );
	  
				this.axisRect = CalcAxisRect( g );
				this.pieRect = PieItem.CalcPieRect( g, this, scaleFactor, this.axisRect );
			}
 
			// Pick new scales based on the range
			this.xAxis.Scale.PickScale( this, g, scaleFactor );
			foreach ( Axis axis in yAxisList )
				axis.Scale.PickScale( this, g, scaleFactor );
			foreach ( Axis axis in y2AxisList )
				axis.Scale.PickScale( this, g, scaleFactor );
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
		public override void Draw( Graphics g )
		{			
			// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
			//int		hStack;
			//float	legendWidth, legendHeight;

			// Draw the pane border & background fill, the title, and the GraphItem objects that lie at
			// ZOrder.G_BehindAll
			base.Draw( g );

			if ( paneRect.Width <= 1 || paneRect.Height <= 1 )
				return;

			// Clip everything to the paneRect
			g.SetClip( this.paneRect );

			// calculate scaleFactor on "normal" pane size (BaseDimension)
			float scaleFactor = this.CalcScaleFactor();


			// if the size of the axisRect is determined automatically, then do so
			// otherwise, calculate the legendrect, scalefactor, hstack, and legendwidth parameters
			// but leave the axisRect alone
			if ( this.isAxisRectAuto )
			{
				this.axisRect = CalcAxisRect( g, scaleFactor );
				this.pieRect = PieItem.CalcPieRect( g, this, scaleFactor, this.axisRect );
			}
			else
				CalcAxisRect( g, scaleFactor  );

			// do a sanity check on the axisRect
			if ( this.axisRect.Width < 1 || this.axisRect.Height < 1 )
				return;
			
			// Draw the graph features only if there is at least one curve with data
			// if ( this.curveList.HasData() &&
			// Go ahead and draw the graph, even without data.  This makes the control
			// version still look like a graph before it is fully set up
			bool showGraf = AxisRangesValid();

			// Setup the axes from graphing - This setup must be done before
			// the GraphItem's are drawn so that the Transform functions are
			// ready.  Also, this should be done before CalcAxisRect so that the
			// Axis.Cross - shift parameter can be calculated.
			this.xAxis.Scale.SetupScaleData( this, this.xAxis );
			foreach ( Axis axis in yAxisList )
				axis.Scale.SetupScaleData( this, axis );
			foreach ( Axis axis in y2AxisList )
				axis.Scale.SetupScaleData( this, axis );

			// Draw the GraphItems that are behind the Axis objects
			if ( showGraf )
				this.graphItemList.Draw( g, this, scaleFactor, ZOrder.F_BehindAxisFill );

			// Fill the axis background
			this.axisFill.Draw( g, this.axisRect );
			
			if ( showGraf )
			{		
				// Draw the GraphItems that are behind the Axis objects
				this.graphItemList.Draw( g, this, scaleFactor, ZOrder.E_BehindAxis );

				// Draw the Axes
				this.xAxis.Draw( g, this, scaleFactor, 0.0f );

				float yPos = 0;
				foreach ( Axis axis in yAxisList )
				{
					axis.Draw( g, this, scaleFactor, yPos );
					yPos += axis.tmpSpace;
				}

				yPos = 0;
				foreach ( Axis axis in y2AxisList )
				{
					axis.Draw( g, this, scaleFactor, yPos );
					yPos += axis.tmpSpace;
				}
				
				// Draw the GraphItems that are behind the CurveItems
				this.graphItemList.Draw( g, this, scaleFactor, ZOrder.D_BehindCurves );

				// Clip the points to the actual plot area
				g.SetClip( this.axisRect );
				this.curveList.Draw( g, this, scaleFactor );
				g.SetClip( this.paneRect );

				// Draw the GraphItems that are behind the Axis border
				this.graphItemList.Draw( g, this, scaleFactor, ZOrder.C_BehindAxisBorder );
			}
				
			// Border the axis itself
			this.axisBorder.Draw( g, this.IsPenWidthScaled, scaleFactor, this.axisRect );
			
			if ( showGraf )
			{
				// Draw the GraphItems that are behind the Legend object
				this.graphItemList.Draw( g, this, scaleFactor, ZOrder.B_BehindLegend );

				this.legend.Draw( g, this, scaleFactor );
			
				// Draw the GraphItems that are in front of all other items
				this.graphItemList.Draw( g, this, scaleFactor, ZOrder.A_InFront );
			}

			// Reset the clipping
			g.ResetClip();
		}

		private bool AxisRangesValid()
		{
			bool showGraf = this.xAxis.Min < this.xAxis.Max;
			foreach ( Axis axis in yAxisList )
				if ( axis.Min > axis.Max )
					showGraf = false;
			foreach ( Axis axis in y2AxisList )
				if ( axis.Min > axis.Max )
					showGraf = false;

			return showGraf;
		}

		/// <summary>
		/// Calculate the <see cref="AxisRect"/> based on the <see cref="PaneBase.PaneRect"/>.
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
			//int		hStack;
			//float	legendWidth, legendHeight;
			
			return CalcAxisRect( g, CalcScaleFactor() );
		}

		/// <summary>
		/// Calculate the <see cref="AxisRect"/> based on the <see cref="PaneBase.PaneRect"/>.
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
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>The calculated axis rect, in pixel coordinates.</returns>
        
		public RectangleF CalcAxisRect( Graphics g, float scaleFactor )
		{				
			// Axis rect starts out at the full pane rect less the margins
            //   and less space for the Pane title
			RectangleF clientRect = this.CalcClientRect( g, scaleFactor );

            //float minSpaceX = 0;
			//float minSpaceY = 0;
			//float minSpaceY2 = 0;
			float totSpaceY = 0;
			//float spaceY2 = 0;

			// actual minimum axis space for the left side of the Axis rect
			float minSpaceL = 0;
			// actual minimum axis space for the right side of the Axis rect
			float minSpaceR = 0;
			// actual minimum axis space for the bottom side of the Axis rect
			float minSpaceB = 0;
			// actual minimum axis space for the top side of the Axis rect
			float minSpaceT = 0;

			this.xAxis.CalcSpace( g, this, scaleFactor );
			minSpaceB = xAxis.tmpMinSpace;

			foreach ( Axis axis in yAxisList )
			{
				float tmp = axis.CalcSpace( g, this, scaleFactor );
				//if ( !axis.CrossAuto || axis.Cross < xAxis.Min )
				if ( axis.IsCrossShifted( this ) )
					totSpaceY += tmp;

				minSpaceL += axis.tmpMinSpace;
			}
			foreach ( Axis axis in y2AxisList )
			{
				float tmp = axis.CalcSpace( g, this, scaleFactor );
				//if ( !axis.CrossAuto || axis.Cross < xAxis.Min )
				if ( axis.IsCrossShifted( this ) )
					totSpaceY += tmp;

				minSpaceR += axis.tmpMinSpace;
			}

			float spaceB, spaceT, spaceL, spaceR;

            SetSpace( this.xAxis, clientRect.Height - this.xAxis.tmpSpace, out spaceB, out spaceT );
			minSpaceT = Math.Max( minSpaceT, spaceT );
			this.xAxis.tmpSpace = spaceB;

			float totSpaceL = 0;
			float totSpaceR = 0;

			foreach ( Axis axis in yAxisList )
			{
				SetSpace( axis, clientRect.Width - totSpaceY, out spaceL, out spaceR );
				minSpaceR = Math.Max( minSpaceR, spaceR );
				totSpaceL += spaceL;
				axis.tmpSpace = spaceL;
			}
			foreach ( Axis axis in y2AxisList )
			{
				SetSpace( axis, clientRect.Width - totSpaceY, out spaceR, out spaceL );
				minSpaceL = Math.Max( minSpaceL, spaceL );
				totSpaceR += spaceR;
				axis.tmpSpace = spaceR;
			}

			RectangleF tmpRect = clientRect;

			totSpaceL = Math.Max( totSpaceL, minSpaceL );
			totSpaceR = Math.Max( totSpaceR, minSpaceR );
			spaceB = Math.Max( spaceB, minSpaceB );

			tmpRect.X += totSpaceL;
			tmpRect.Width -= totSpaceL + totSpaceR;
			tmpRect.Height -= spaceT + spaceB;
			tmpRect.Y += spaceT;

            this.legend.CalcRect( g, this, scaleFactor, ref tmpRect );

			return tmpRect;
		}

		private void SetSpace( Axis axis, float clientSize, out float spaceNorm, out float spaceAlt )
		{
			spaceNorm = 0;
			spaceAlt = 0;

			float crossFrac = axis.CalcCrossFraction( this );
			float crossPix = crossFrac * ( 1 + crossFrac ) * ( 1 + crossFrac * crossFrac ) * clientSize;

			if ( !axis.IsPrimary( this ) && axis.IsCrossShifted( this ) )
				axis.tmpSpace = 0;

			if ( axis.tmpSpace < crossPix )
				axis.tmpSpace = 0;
			else if ( crossPix > 0 )
				axis.tmpSpace -= crossPix;

			if ( axis.IsScaleLabelsInside && ( axis.IsPrimary(this) || ( crossFrac != 0.0 && crossFrac != 1.0 ) ) )
				spaceAlt = axis.tmpSpace;
			else
				spaceNorm = axis.tmpSpace;
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
			foreach ( Axis axis in yAxisList )
				axis.SetMinSpaceBuffer( g, this, bufferFraction, isGrowOnly  );
			foreach ( Axis axis in y2AxisList )
				axis.SetMinSpaceBuffer( g, this, bufferFraction, isGrowOnly  );
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
		/// the given data points (<see cref="IPointList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddCurve(string,IPointList,Color)"/> method.</returns>
		public LineItem AddCurve( string label, IPointList points, Color color )
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
		/// the given data points (<see cref="IPointList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <param name="symbolType">A symbol type (<see cref="SymbolType"/>)
		/// that will be used for this curve.</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddCurve(string,IPointList,Color,SymbolType)"/> method.</returns>
		public LineItem AddCurve( string label, IPointList points,
			Color color, SymbolType symbolType )
		{
			LineItem curve = new LineItem( label, points, color, symbolType );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a stick graph (<see cref="StickItem"/> object) to the plot with
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
		/// <returns>A <see cref="StickItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddStick(string,double[],double[],Color)"/> method.</returns>
		public StickItem AddStick( string label, double[] x, double[] y, Color color )
		{
			StickItem curve = new StickItem( label, x, y, color );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a stick graph (<see cref="StickItem"/> object) to the plot with
		/// the given data points (<see cref="IPointList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddStick(string,IPointList,Color)"/> method.</returns>
		public StickItem AddStick( string label, IPointList points, Color color )
		{
			StickItem curve = new StickItem( label, points, color );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add an error bar set (<see cref="ErrorBarItem"/> object) to the plot with
		/// the given data points (<see cref="IPointList"/>) and properties.
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
		/// <see cref="AddErrorBar(string,IPointList,Color)"/> method.</returns>
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
		/// the given data points (<see cref="IPointList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used for the curve line,
		/// symbols, etc.</param>
		/// <returns>An <see cref="ErrorBarItem"/> class for the newly created curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddErrorBar(string,IPointList,Color)"/> method.</returns>
		public ErrorBarItem AddErrorBar( string label, IPointList points, Color color )
		{
			ErrorBarItem curve = new ErrorBarItem( label, points, color );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a bar type curve (<see cref="CurveItem"/> object) to the plot with
		/// the given data points (<see cref="IPointList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">The color to used to fill the bars</param>
		/// <returns>A <see cref="CurveItem"/> class for the newly created bar curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddBar(string,IPointList,Color)"/> method.</returns>
		public BarItem AddBar( string label, IPointList points, Color color )
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
		/// the given data points (<see cref="IPointList"/>) and properties.
		/// This is simplified way to add curves without knowledge of the
		/// <see cref="CurveList"/> class.  An alternative is to use
		/// the <see cref="ZedGraph.CurveList.Add"/> method.
		/// </summary>
		/// <param name="label">The text label (string) for the curve that will be
		/// used as a <see cref="Legend"/> entry.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value Trio's that define
		/// the X, Y, and lower dependent values for this curve</param>
		/// <param name="color">The color to used to fill the bars</param>
		/// <returns>A <see cref="HiLowBarItem"/> class for the newly created bar curve.
		/// This can then be used to access all of the curve properties that
		/// are not defined as arguments to the
		/// <see cref="AddHiLowBar(string,IPointList,Color)"/> method.</returns>
		public HiLowBarItem AddHiLowBar( string label, IPointList points, Color color )
		{
			HiLowBarItem curve = new HiLowBarItem( label, points, color );
			this.curveList.Add( curve );
			
			return curve;
		}

		/// <summary>
		/// Add a <see cref="PieItem"/> to the display.
		/// </summary>
		/// <param name="value">The value associated with this <see cref="PieItem"/>item.</param>
		/// <param name="color">The display color for this <see cref="PieItem"/>item.</param>
		/// <param name="displacement">The amount this <see cref="PieItem"/>item will be 
		/// displaced from the center of the <see cref="PieItem"/>.</param>
		/// <param name="label">Text label for this <see cref="PieItem"/></param>
		/// <returns>a reference to the <see cref="PieItem"/> constructed</returns>
		public PieItem AddPieSlice ( double value, Color color, double displacement, string label )
		{
			PieItem slice = new PieItem( value, color, displacement, label );
			this.CurveList.Add( slice );
			return slice;
		}

		/// <summary>
		/// Add a <see cref="PieItem"/> to the display, providing a gradient fill for the pie color.
		/// </summary>
		/// <param name="value">The value associated with this <see cref="PieItem"/> instance.</param>
		/// <param name="color1">The starting display color for the gradient <see cref="Fill"/> for this
		/// <see cref="PieItem"/> instance.</param>
		/// <param name="color2">The ending display color for the gradient <see cref="Fill"/> for this
		/// <see cref="PieItem"/> instance.</param>
		/// <param name="fillAngle">The angle for the gradient <see cref="Fill"/>.</param>
		/// <param name="displacement">The amount this <see cref="PieItem"/>  instance will be 
		/// displaced from the center point.</param>
		/// <param name="label">Text label for this <see cref="PieItem"/> instance.</param>
		public PieItem AddPieSlice ( double value, Color color1, Color color2, float fillAngle,
						double displacement, string label )
		{
			PieItem slice = new PieItem( value, color1, color2, fillAngle, displacement, label ) ;
			this.CurveList.Add( slice );
			return slice;
		}

		/// <summary>
		///Creates all the <see cref="PieItem"/>s for a single Pie Chart. 
		/// </summary>
		/// <param name="values">double array containing all <see cref="PieItem.Value"/>s
		/// for a single PieChart.
		/// </param>
		/// <param name="labels"> string array containing all <see cref="CurveItem.Label"/>s
		/// for a single PieChart.
		/// </param>
		/// <returns>an array containing references to all <see cref="PieItem"/>s comprising
		/// the Pie Chart.</returns>
		public PieItem [] AddPieSlices ( double [] values, string [] labels )
		{
			PieItem [] slices = new PieItem[values.Length] ;
			for ( int x = 0 ; x < values.Length ; x++ )
			{
				slices[x]= new PieItem(  values[x],  labels [x] ) ;
				this.CurveList.Add (slices[x]) ;																			  
			}
			return slices ;
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
			this.xAxis.Scale.SetupScaleData( this, this.xAxis );
			foreach ( Axis axis in yAxisList )
				axis.Scale.SetupScaleData( this, axis );
			foreach ( Axis axis in y2AxisList )
				axis.Scale.SetupScaleData( this, axis );
		
			return this.TransformCoord( ptF, coord );
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
		/// primary <see cref="YAxis"/></param>
		/// <param name="y2">The resultant value in user coordinates from the
		/// primary <see cref="Y2Axis"/></param>
		public void ReverseTransform( PointF ptF, out double x, out double y,
			out double y2 )
		{
			// Setup the scaling data based on the axis rect
			this.xAxis.Scale.SetupScaleData( this, this.xAxis );
			this.YAxis.Scale.SetupScaleData( this, this.YAxis );
			this.Y2Axis.Scale.SetupScaleData( this, this.Y2Axis );

			x = this.XAxis.Scale.ReverseTransform( ptF.X );
			y = this.YAxis.Scale.ReverseTransform( ptF.Y );
			y2 = this.Y2Axis.Scale.ReverseTransform( ptF.Y );
		}

		/// <summary>
		/// Return the user scale values that correspond to the specified screen
		/// coordinate position (pixels) for all y axes.
		/// </summary>
		/// <remarks>This method implicitly assumes that <see cref="AxisRect"/>
		/// has already been calculated via <see cref="AxisChange"/> or
		/// <see cref="Draw"/> methods, or the <see cref="AxisRect"/> is
		/// set manually (see <see cref="IsAxisRectAuto"/>).</remarks>
		/// <param name="ptF">The X,Y pair that defines the screen coordinate
		/// point of interest</param>
		/// <param name="x">The resultant value in user coordinates from the
		/// <see cref="XAxis"/></param>
		/// <param name="y">An array of resultant values in user coordinates from the
		/// list of <see cref="YAxis"/> instances.  This method allocates the
		/// array for you, according to the number of <see cref="YAxis" /> objects
		/// in the list.</param>
		/// <param name="y2">An array of resultant values in user coordinates from the
		/// list of <see cref="Y2Axis"/> instances.  This method allocates the
		/// array for you, according to the number of <see cref="Y2Axis" /> objects
		/// in the list.</param>
		public void ReverseTransform( PointF ptF, out double x, out double[] y,
			out double[] y2 )
		{
			// Setup the scaling data based on the axis rect
			this.xAxis.Scale.SetupScaleData( this, xAxis );
			x = this.XAxis.Scale.ReverseTransform( ptF.X );

			y = new double[ this.yAxisList.Count ];
			y2 = new double[ this.y2AxisList.Count ];

			for ( int i=0; i<this.yAxisList.Count; i++ )
			{
				Axis axis = this.yAxisList[i];
				axis.Scale.SetupScaleData( this, axis  );
				y[i] = axis.Scale.ReverseTransform( ptF.Y );
			}
			for ( int i=0; i<this.y2AxisList.Count; i++ )
			{
				Axis axis = this.y2AxisList[i];
				axis.Scale.SetupScaleData( this, axis );
				y2[i] = axis.Scale.ReverseTransform( ptF.Y );
			}
		}

		/// <summary>
		/// Add a secondary <see cref="YAxis" /> (left side) to the list of axes
		/// in the Graph.
		/// </summary>
		/// <remarks>
		/// Note that the primary <see cref="YAxis" /> is always included by default.
		/// This method turns off the <see cref="Axis.IsOppositeTic" />,
		/// <see cref="Axis.IsMinorOppositeTic" />, <see cref="Axis.IsInsideTic" />,
		/// and <see cref="Axis.IsMinorInsideTic" /> properties by default.
		/// </remarks>
		/// <param name="title">The title for the <see cref="YAxis" />.</param>
		/// <returns>the ordinal position (index) in the <see cref="YAxisList" />.</returns>
		public int AddYAxis( string title )
		{
			YAxis axis = new YAxis( title );
			axis.IsOppositeTic = false;
			axis.IsMinorOppositeTic = false;
			axis.IsInsideTic = false;
			axis.IsMinorInsideTic = false;
			return this.yAxisList.Add( axis );
		}

		/// <summary>
		/// Add a secondary <see cref="Y2Axis" /> (right side) to the list of axes
		/// in the Graph.
		/// </summary>
		/// <remarks>
		/// Note that the primary <see cref="Y2Axis" /> is always included by default.
		/// This method turns off the <see cref="Axis.IsOppositeTic" />,
		/// <see cref="Axis.IsMinorOppositeTic" />, <see cref="Axis.IsInsideTic" />,
		/// and <see cref="Axis.IsMinorInsideTic" /> properties by default.
		/// </remarks>
		/// <param name="title">The title for the <see cref="Y2Axis" />.</param>
		/// <returns>the ordinal position (index) in the <see cref="Y2AxisList" />.</returns>
		public int AddY2Axis( string title )
		{
			Y2Axis axis = new Y2Axis( title );
			axis.IsOppositeTic = false;
			axis.IsMinorOppositeTic = false;
			axis.IsInsideTic = false;
			axis.IsMinorInsideTic = false;
			return this.y2AxisList.Add( axis );
		}

		/// <summary>
		/// Find the object that lies closest to the specified mouse (screen) point.
		/// </summary>
		/// <remarks>
		/// This method will search through all of the graph objects, such as
		/// <see cref="Axis"/>, <see cref="Legend"/>, <see cref="PaneBase.Title"/>,
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
		/// <see cref="Legend"/>, <see cref="PaneBase.Title"/>,
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
			if ( AxisRangesValid() )
			{
				float		scaleFactor = CalcScaleFactor();
				//int			hStack;
				//float		legendWidth, legendHeight;
				RectangleF	tmpRect;
				GraphItem	saveGraphItem = null;
				int			saveIndex = -1;
				ZOrder		saveZOrder = ZOrder.G_BehindAll;
	
				// Calculate the axis rect, deducting the area for the scales, titles, legend, etc.
				RectangleF tmpAxisRect = CalcAxisRect( g, scaleFactor );
	
				// See if the point is in a GraphItem
				// If so, just save the object and index so we can see if other overlying objects were
				// intersected as well.
				if ( this.GraphItemList.FindPoint( mousePt, this, g, scaleFactor, out index ) )
				{
					saveGraphItem = this.GraphItemList[index];
					saveIndex = index;
					saveZOrder = saveGraphItem.ZOrder;
				}
				// See if the point is in the legend
				if ( saveZOrder <= ZOrder.B_BehindLegend &&
					this.Legend.FindPoint( mousePt, this, scaleFactor, out index ) )
				{
					nearestObj = this.Legend;
					return true;
				}
				
				// See if the point is in the Pane Title
				if ( saveZOrder <= ZOrder.G_BehindAll && this.isShowTitle )
				{
					SizeF size = this.FontSpec.BoundingBox( g, this.title, scaleFactor );
					tmpRect = new RectangleF( ( this.paneRect.Left + this.paneRect.Right - size.Width ) / 2,
						this.paneRect.Top + this.marginTop * scaleFactor,
						size.Width, size.Height );
					if ( tmpRect.Contains( mousePt ) )
					{
						nearestObj = this;
						return true;
					}
				}

				float left = tmpAxisRect.Left;

				// See if the point is in one of the Y Axes
				for ( int yIndex=0; yIndex<this.yAxisList.Count; yIndex++ )
				{
					Axis yAxis = this.yAxisList[yIndex];
					float width = yAxis.tmpSpace;
					if ( width > 0 )
					{
						tmpRect = new RectangleF( left - width, tmpAxisRect.Top,
							width, tmpAxisRect.Height );
						if ( saveZOrder <= ZOrder.E_BehindAxis && tmpRect.Contains( mousePt ) )
						{
							nearestObj = yAxis;
							index = yIndex;
							return true;
						}

						left -= width;
					}
				}
				
				left = tmpAxisRect.Right;

				// See if the point is in one of the Y2 Axes
				for ( int yIndex=0; yIndex<this.y2AxisList.Count; yIndex++ )
				{
					Axis y2Axis = this.y2AxisList[yIndex];
					float width = y2Axis.tmpSpace;
					if ( width > 0 )
					{
						tmpRect = new RectangleF( left, tmpAxisRect.Top,
							left + width, tmpAxisRect.Height );
						if ( saveZOrder <= ZOrder.E_BehindAxis && tmpRect.Contains( mousePt ) )
						{
							nearestObj = y2Axis;
							index = yIndex;
							return true;
						}

						left += width;
					}
				}
								
				// See if the point is in the X Axis
				tmpRect = new RectangleF( tmpAxisRect.Left, tmpAxisRect.Bottom,
					tmpAxisRect.Width, this.paneRect.Bottom - tmpAxisRect.Bottom );
				if ( saveZOrder <= ZOrder.E_BehindAxis && tmpRect.Contains( mousePt ) )
				{
					nearestObj = this.XAxis;
					return true;
				}
				
				CurveItem curve;
				// See if it's a data point
				if ( saveZOrder <= ZOrder.D_BehindCurves && FindNearestPoint( mousePt, out curve, out index ) )
				{
					nearestObj = curve;
					return true;
				}
				
				if ( saveGraphItem != null )
				{
					index = saveIndex;
					nearestObj = saveGraphItem;
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

			double	x;
			double[] y;
			double[] y2;

			//ReverseTransform( mousePt, out x, out y, out y2 );
			ReverseTransform( mousePt, out x, out y, out y2 );

			if ( !AxisRangesValid() )
				return false;

			ValueHandler valueHandler = new ValueHandler( this, false );

			double	xPixPerUnit = axisRect.Width / ( xAxis.Max - xAxis.Min );
			//double	yPixPerUnit = axisRect.Height / ( yAxis.Max - yAxis.Min );
			//double	y2PixPerUnit; // = axisRect.Height / ( y2Axis.Max - y2Axis.Min );

			double	yPixPerUnitAct, yAct, yMinAct, yMaxAct;
			double	minDist = 1e20;
			double	xVal, yVal, dist=99999, distX, distY;
			double	tolSquared = Default.NearestTol * Default.NearestTol;

			int		iBar = 0;

			foreach ( CurveItem curve in targetCurveList )
			{
				//test for pie first...if it's a pie rest of method superfluous
				if ( curve is PieItem && curve.IsVisible )
				{
					if ( ((PieItem)curve).SlicePath != null &&
							((PieItem)curve).SlicePath.IsVisible (mousePt) )
					{
						nearestBar = curve;
						iNearestBar = 0;
					}

					continue;
				}
				else if ( curve.IsVisible )
				{
					int yIndex = curve.GetYAxisIndex( this );
					Axis yAxis = curve.GetYAxis( this );

					if ( curve.IsY2Axis )
					{
						yAct = y2[yIndex];
						yMinAct = y2AxisList[yIndex].Min;
						yMaxAct = y2AxisList[yIndex].Max;
					}
					else
					{
						yAct = y[yIndex];
						yMinAct = yAxisList[yIndex].Min;
						yMaxAct = yAxisList[yIndex].Max;
					}

					yPixPerUnitAct = axisRect.Height / ( yMaxAct - yMinAct );

					IPointList points = curve.Points;
					float barWidth = curve.GetBarWidth( this );
					double barWidthUserHalf;
					bool isXBaseAxis = ( curve.BaseAxis( this ) == XAxis );
					if ( isXBaseAxis )
						barWidthUserHalf = barWidth / xPixPerUnit / 2.0;
					else
						barWidthUserHalf = barWidth / yPixPerUnitAct / 2.0;

					if ( points != null )
					{
						for ( int iPt=0; iPt<curve.NPts; iPt++ )
						{
							// xVal is the user scale X value of the current point
							if ( xAxis.IsAnyOrdinal && ! curve.IsOverrideOrdinal )
								xVal = (double) iPt + 1.0;
							else
								xVal = points[iPt].X;

							// yVal is the user scale Y value of the current point
							if ( yAxis.IsAnyOrdinal && ! curve.IsOverrideOrdinal )
								yVal = (double) iPt + 1.0;
							else
								yVal = points[iPt].Y;

							if (	xVal != PointPair.Missing &&
									yVal != PointPair.Missing )
							{

								if ( curve.IsBar || curve is ErrorBarItem ||
									curve is HiLowBarItem )
								{
									double baseVal, lowVal, hiVal;
									valueHandler.GetValues( curve, iPt, out baseVal,
											out lowVal, out hiVal );

									if ( lowVal > hiVal )
									{
										double tmpVal = lowVal;
										lowVal = hiVal;
										hiVal = tmpVal;
									}

									if ( isXBaseAxis )
									{
										
										double centerVal = valueHandler.BarCenterValue( curve, barWidth, iPt, xVal, iBar );
										
										if (	x < centerVal - barWidthUserHalf ||
												x > centerVal + barWidthUserHalf ||
												yAct < lowVal || yAct > hiVal )
											continue;
									}
									else
									{
										double centerVal = valueHandler.BarCenterValue( curve, barWidth, iPt, yVal, iBar );
										
										if (	yAct < centerVal - barWidthUserHalf ||
												yAct > centerVal + barWidthUserHalf ||
												x < lowVal || x > hiVal )
											continue;
									}

									if ( nearestBar == null )
									{
										iNearestBar = iPt;
										nearestBar = curve;
									}
								}
								else if (	xVal >= xAxis.Min && xVal <= xAxis.Max &&
											yVal >= yMinAct && yVal <= yMaxAct )

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

