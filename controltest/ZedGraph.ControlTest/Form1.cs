using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using ZedGraph;
using System.Drawing.Imaging;

namespace ZedGraph.ControlTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private PropertyGrid propertyGrid1;
		private System.Windows.Forms.Splitter splitter1;
		private ZedGraphControl zedGraphControl1;
		private bool _isShowPropertyGrid = false;

		private Timer myTimer;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point( 622, 0 );
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size( 240, 461 );
			this.propertyGrid1.TabIndex = 2;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler( this.propertyGrid1_PropertyValueChanged );
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point( 617, 0 );
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size( 5, 461 );
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.IsAutoScrollRange = false;
			this.zedGraphControl1.IsEnableHPan = true;
			this.zedGraphControl1.IsEnableHZoom = true;
			this.zedGraphControl1.IsEnableVPan = true;
			this.zedGraphControl1.IsEnableVZoom = true;
			this.zedGraphControl1.IsPrintFillPage = true;
			this.zedGraphControl1.IsPrintKeepAspectRatio = true;
			this.zedGraphControl1.IsScrollY2 = false;
			this.zedGraphControl1.IsShowContextMenu = true;
			this.zedGraphControl1.IsShowCopyMessage = true;
			this.zedGraphControl1.IsShowCursorValues = false;
			this.zedGraphControl1.IsShowHScrollBar = false;
			this.zedGraphControl1.IsShowPointValues = false;
			this.zedGraphControl1.IsShowVScrollBar = false;
			this.zedGraphControl1.IsZoomOnMouseCenter = false;
			this.zedGraphControl1.Location = new System.Drawing.Point( 8, 8 );
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.PanButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			this.zedGraphControl1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.PointDateFormat = "g";
			this.zedGraphControl1.PointValueFormat = "G";
			this.zedGraphControl1.ScrollMaxX = 0;
			this.zedGraphControl1.ScrollMaxY = 0;
			this.zedGraphControl1.ScrollMaxY2 = 0;
			this.zedGraphControl1.ScrollMinX = 0;
			this.zedGraphControl1.ScrollMinY = 0;
			this.zedGraphControl1.ScrollMinY2 = 0;
			this.zedGraphControl1.Size = new System.Drawing.Size( 600, 448 );
			this.zedGraphControl1.TabIndex = 5;
			this.zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomStepFraction = 0.1;
			this.zedGraphControl1.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler( this.zedGraphControl1_MouseDownEvent );
			this.zedGraphControl1.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler( this.zedGraphControl1_ContextMenuBuilder );
			this.zedGraphControl1.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler( this.zedGraphControl1_MouseUpEvent );
			this.zedGraphControl1.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler( this.zedGraphControl1_MouseMoveEvent );
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 862, 461 );
			this.Controls.Add( this.zedGraphControl1 );
			this.Controls.Add( this.splitter1 );
			this.Controls.Add( this.propertyGrid1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler( this.Form1_Resize );
			this.KeyDown += new System.Windows.Forms.KeyEventHandler( this.Form1_KeyDown );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );
			this.Load += new System.EventHandler( this.Form1_Load );
			this.ResumeLayout( false );

		}
#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run( new Form1() );
		}

		private void Form1_Load( object sender, System.EventArgs e )
		{
			GraphPane myPane = zedGraphControl1.GraphPane;

			//CreateGraph_ThreeVerticalPanes( zedGraphControl1 );
			//CreateGraph_NormalPane( zedGraphControl1 );
			//CreateGraph_Contour( zedGraphControl1 );
			//CreateGraph_Linear( zedGraphControl1 );
			CreateGraph_StackLine( zedGraphControl1 );

			//this.zedGraphControl1.MouseDownEvent += new ZedGraphControl.MouseDownEventHandler( MyMouseDownEventHandler );

#if false // masterpane with pies

			GraphPane pane = new GraphPane();

			pane.Title = "Test Pie";

			PieItem myPie = pane.AddPieSlice( 10, Color.Red, 0.0, "First" ); 
			myPie = pane.AddPieSlice( 20, Color.Blue, 0.0, "Second" ); 
			myPie = pane.AddPieSlice( 15, Color.Green, 0.0, "Third" ); 
			myPie = pane.AddPieSlice( 40, Color.Purple, 0.0, "Fourth" ); 
			myPie = pane.AddPieSlice( 27, Color.Pink, 0.0, "Fifth" );

			zedGraphControl1.MasterPane[0] = pane;
			zedGraphControl1.MasterPane.Add( pane.Clone() as GraphPane );
			zedGraphControl1.MasterPane.Add( pane.Clone() as GraphPane );
			zedGraphControl1.MasterPane.Add( pane.Clone() as GraphPane );
			zedGraphControl1.MasterPane.Add( pane.Clone() as GraphPane );

			zedGraphControl1.MasterPane.AutoPaneLayout( this.CreateGraphics(), PaneLayout.ExplicitRow32 );


#endif
#if false	// Basic curve test - Date Axis w/ Time Span



			PointPairList list = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				double x = (double) i/123.0;
				//double x = new XDate( 0, 0, i, i*3, i*2, i );
				double y = Math.Sin( i / 8.0 ) * 1 + 1;
				list.Add( x, y );
				double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myPane.XAxis.IsSkipLastLabel = false;
			//myPane.XAxis.IsPreventLabelOverlap = false;
			myPane.XAxis.ScaleFormat = "[d].[h]:[m]:[s]";
			zedGraphControl1.PointDateFormat = "[d].[hh]:[mm]:[ss]";
			myPane.XAxis.Type = AxisType.Date;
			myPane.AxisChange( this.CreateGraphics() );

			myPane.YAxis.ScaleFormat = "0.0'%'";

#endif

#if false
			myPane = zedGraphControl1.GraphPane;
			myPane.Title = "Demonstration of SamplePointList";
			myPane.XAxis.Title = "Time since start, days";
			myPane.YAxis.Title = "Postion (meters) or\nAverage Velocity (meters/day)";
			myPane.AxisFill = new Fill( Color.White, Color.FromArgb( 230, 230, 255 ), 45.0f );

			SamplePointList spl = new SamplePointList();

			// Calculate sample data points
			// starting position & velocity
			double d0 = 15.0;		// m
			double v0 = 5.2;		// m/d
			double acc = 11.5;	// m/d/d
			for ( int i = 0; i < 10; i++ )
			{
				Sample sample = new Sample();

				// Samples are one day apart
				sample.Time = new DateTime( 2006, 3, i + 1 );
				// velocity in meters per day
				sample.Velocity = v0 + acc * i;
				sample.Position = acc * i * i / 2.0 + v0 * i + d0;
				spl.Add( sample );
			}

			// Create the first curve as Position vs delta time
			spl.XType = SampleType.TimeDiff;
			spl.YType = SampleType.Position;
			LineItem curve = myPane.AddCurve( "Position", spl, Color.Green, SymbolType.Diamond );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Line.Width = 2.0f;

			// create the second curve as Average Velocity vs delta time
			SamplePointList spl2 = new SamplePointList( spl );
			spl2.YType = SampleType.VelocityAvg;
			LineItem curve2 = myPane.AddCurve( "Average Velocity", spl2, Color.Blue, SymbolType.Circle );
			curve2.Symbol.Fill = new Fill( Color.White );
			curve2.Line.Width = 2.0f;

#endif

#if false	// Japanese Candlestick

			myPane.Title = "Japanese Candlestick Chart Demo";
			myPane.XAxis.Title = "Trading Date";
			myPane.YAxis.Title = "Share Price, $US";

			PointPairList blackList = new PointPairList();
			PointPairList whiteList = new PointPairList();
			PointPairList hilowList = new PointPairList();
			Random rand = new Random();

			// First day is feb 1st
			XDate xDate = new XDate( 2006, 2, 1 );
			double open = 50.0;

			// Loop to calculate some random data
			for ( int i=0; i<20; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double top = Math.Max( open, close );
				double bot = Math.Min( open, close );
				double hi = top + rand.NextDouble() * 5.0;
				double low = bot - rand.NextDouble() * 5.0;

				if ( open < close )
				{
					whiteList.Add( x, top, bot );
					blackList.Add( x, PointPair.Missing, PointPair.Missing );
				}
				else
				{
					blackList.Add( x, top, bot );
					whiteList.Add( x, PointPair.Missing, PointPair.Missing );
				}

				hilowList.Add( x, hi, low );

				open = close;
				// Advance one day
				xDate.AddDays( 1.0 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			// Create the black Open-Close Bars
			HiLowBarItem openclose = myPane.AddHiLowBar( "Open-Close", blackList, Color.Black );
			openclose.Bar.Fill = new Fill( Color.Black );
			// Create the white Open-Close bars
			HiLowBarItem openclose2 = myPane.AddHiLowBar( "Open-Close", whiteList, Color.Black );
			openclose2.Bar.Fill = new Fill( Color.White );
			openclose2.IsLegendLabelVisible = false;

			// Create the high-low sticks
			ErrorBarItem hilow = myPane.AddErrorBar( "Hi-Low", hilowList, Color.Black );
			hilow.ErrorBar.Symbol.IsVisible = false;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.Step = 1.0;

			// pretty it up a little
			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.PaneFill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			// Tell ZedGraph to calculate the axis ranges
			zedGraphControl1.AxisChange();
			zedGraphControl1.Invalidate();

#endif


#if false			// radar plot

			myPane = zedGraphControl1.GraphPane;
			RadarPointList rpl = new RadarPointList();

			Random rand = new Random();

			for ( int i = 0; i < 7; i++ )
			{
				double r = rand.NextDouble() * 10.0 + 1.0;
				PointPair pt = new PointPair( PointPair.Missing, r, "r = " + r.ToString( "f1" ) );
				rpl.Add( pt );
			}
			LineItem curve = myPane.AddCurve( "test", rpl, Color.Green, SymbolType.Default );
			//curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.5F;

			myPane.XAxis.IsZeroLine = true;

			for ( int i = 0; i < 7; i++ )
			{
				ArrowItem arrow = new ArrowItem( 0, 0, (float) rpl[i].X, (float) rpl[i].Y );
				arrow.IsArrowHead = false;
				arrow.Color = Color.LightGray;
				arrow.ZOrder = ZOrder.D_BehindCurves;
				myPane.GraphItemList.Add( arrow );
			}

			myPane.XAxis.IsAllTics = true;
			myPane.XAxis.IsTitleAtCross = false;
			myPane.XAxis.Cross = 0;
			myPane.XAxis.IsSkipFirstLabel = true;
			myPane.XAxis.IsSkipLastLabel = true;
			myPane.XAxis.IsSkipCrossLabel = true;

			myPane.YAxis.IsAllTics = true;
			myPane.YAxis.IsTitleAtCross = false;
			myPane.YAxis.Cross = 0;
			myPane.YAxis.IsSkipFirstLabel = true;
			myPane.YAxis.IsSkipLastLabel = true;
			myPane.YAxis.IsSkipCrossLabel = true;
#endif


#if false			// spline test

			myPane = zedGraphControl1.GraphPane;
			PointPairList ppl = new PointPairList();

			ppl.Add( 0, 713 );
			ppl.Add( 7360, 333 );
			ppl.Add( 10333.333, 45.333336 );
			ppl.Add( 11666.667, 5 );
			ppl.Add( 12483.333, 45.333336 );
			ppl.Add( 13600, 110 );
			ppl.Add( 15800, 184.66667 );
//			ppl.Add( 18000, 187.5 );
			ppl.Add( 18644.998, 186.33368 );
			ppl.Add( 18770.002, 186.66664 );
			ppl.Add( 18896.666, 187.08336 );
			ppl.Add( 18993.334, 187.50002 );
			ppl.Add( 19098.332, 188.08334 );
			ppl.Add( 19285.002, 189.41634 );
			ppl.Add( 19443.332, 190.83334 );
			ppl.Add( 19633.334, 193.16634 );
			ppl.Add( 19823.336, 196.49983 );
			ppl.Add( 19940.002, 199.16669 );
			ppl.Add( 20143.303, 204.66566 );
			ppl.Add( 20350, 210.91667 );
//			ppl.Add( 21000, 232 );
//			ppl.Add( 23000, 296 );
			ppl.Add( 36000, 713 );

			double y1 = ppl.SplineInterpolateX( 18000, 0.4 );
			double y2 = ppl.SplineInterpolateX( 21000, 0.4 );
			double y3 = ppl.SplineInterpolateX( 23000, 0.4 );
			ppl.Add( 18000, y1 );
			ppl.Add( 21000, y2 );
			ppl.Add( 23000, y3 );
			ppl.Sort();

			LineItem curve = myPane.AddCurve( "test", ppl, Color.Green, SymbolType.Default );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.2F;
#endif


#if false		// masterpane test

			MasterPane master = zedGraphControl1.MasterPane;

			master.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			master.PaneList.Clear();

			master.IsShowTitle = true;
			master.Title = "My MasterPane Title";

			master.MarginAll = 10;
			//master.InnerPaneGap = 10;
			//master.Legend.IsVisible = true;
			//master.Legend.Position = LegendPos.TopCenter;

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 6; j++ )
			{
				// Create a new graph with topLeft at (40,40) and size 600x400
				GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"Case #" + ( j + 1 ).ToString(),
					"Time, Days",
					"Rate, m/s" );

				myPaneT.PaneFill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPaneT.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i = 0; i < 36; i++ )
				{
					x = (double)i + 5;
					y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
					list.Add( x, y );
				}

				LineItem myCurve = myPaneT.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( myPaneT );
			}

			Graphics g = this.CreateGraphics();

			//master.AutoPaneLayout( g, PaneLayout.ExplicitRow32 );
			//master.AutoPaneLayout( g, 2, 4 );
			master.AutoPaneLayout( g, false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
			zedGraphControl1.AxisChange();

			g.Dispose();
#endif

#if false	// Basic curve test - Date Axis

			PointPairList list = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				double x = new XDate( 2005, 12, i );
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myPane.XAxis.ScaleFormat = "MMM\nyyyy";
			myPane.XAxis.Type = AxisType.Date;

			zedGraphControl1.IsAutoScrollRange = true;
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.IsShowVScrollBar = true;

			BoxItem box = new BoxItem( new RectangleF( 0.4f, 0.4f, 0.2f, 0.2f ),
							Color.Black, Color.White, Color.LightBlue );
			box.Location.CoordinateFrame = CoordType.AxisFraction;

			myPane.GraphItemList.Add( box );

#endif

#if false	// raita test
			PointPairList list1 = new PointPairList();
 
			list1.Add( 67.741935483871, 0.2 );
			list1.Add( 135.483870967742, -0.199999999999999 );
			list1.Add( 203.225806451613, 0.2 );
			list1.Add( 270.967741935484, 0.2 );
			list1.Add( 338.709677419355, -0.399999999999999 );
			list1.Add( 406.451612903226, 0.100000000000001 );
			list1.Add( 474.193548387097, -0.199999999999999 );
			list1.Add( 541.935483870968, -0.600000000000001 );
			list1.Add( 609.677419354839, -0.399999999999999 );
			list1.Add( 677.41935483871, 0.199999999999999 );
			list1.Add( 745.161290322581, -0.199999999999999 );
			list1.Add( 812.903225806452, 0.2 );
			list1.Add( 880.645161290323, -0.199999999999999 );
			list1.Add( 948.387096774194, 0.199999999999999 );
			list1.Add( 1016.12903225806, -0.199999999999999 );
			list1.Add( 1083.87096774194, -0.5 );
			list1.Add( 1151.61290322581, 0.199999999999999 );
			list1.Add( 1219.35483870968, -0.399999999999999 );
			list1.Add( 1287.09677419355, 0.2 );
			list1.Add( 1354.83870967742, 0.2 );
			list1.Add( 1422.58064516129, -0.5 );
			list1.Add( 1490.32258064516, -0.5 );
			list1.Add( 1558.06451612903, -0.399999999999999 );
			list1.Add( 1625.8064516129, -0.199999999999999 );
			list1.Add( 1693.54838709677, 0.2 );
			list1.Add( 1761.29032258065, -0.199999999999999 );
			list1.Add( 1829.03225806452, 0.2 );
			list1.Add( 1896.77419354839, -0.199999999999999 );
			list1.Add( 1964.51612903226, -0.399999999999999 );
			list1.Add( 2032.25806451613, 0.2 );
			list1.Add( 2100, -0.300000000000001 );
			 
			 
			PointPairList list2 = new PointPairList();
			 
			list2.Add( 67.741935483871, 0.3 );
			list2.Add( 203.225806451613, 0.100000000000001 );
			list2.Add( 270.967741935484, 0.100000000000001 );
			list2.Add( 812.903225806452, 0.100000000000001 );
			list2.Add( 1287.09677419355, 0.199999999999999 );
			list2.Add( 1354.83870967742, 0.199999999999999 );
			list2.Add( 1693.54838709677, 0.249999999999999 );
			list2.Add( 1829.03225806452, 0.3 );
			list2.Add( 2032.25806451613, 0.150000000000001 );

			GraphPane raita1 = zedGraphControl1.GraphPane;
			//GraphPane raita1 = new GraphPane( new Rectangle( 10, 10, 10, 10 ), "Title", "X", "Y" );

			raita1.GraphItemList.Clear(); 
			raita1.CurveList.Clear(); 
			raita1.GraphItemList.Clear(); 
			raita1.MinBarGap = 0; 
			BarItem bar0 = raita1.AddBar("Raita1", list1, Color.Blue); 
			bar0.Bar.Fill = new Fill( Color.SeaGreen, Color.SkyBlue, Color.SeaGreen ); 
			BarItem bar1 = raita1.AddBar("halytysraja",list2, Color.Red); 
			bar1.Bar.Fill = new Fill( Color.Plum, Color.Red, Color.Yellow ); 
			 
			raita1.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F ); 
			// Fill the axis background with a gradient 
			raita1.AxisFill = new Fill( Color.FromArgb( 255, 255, 245), 
			Color.FromArgb( 255, 255, 190), 90F ); 
			//raita1.BarType = BarType.Cluster; 
			raita1.BarType = BarType.Stack; 
			raita1.ClusterScaleWidth = 120; 
			 
			//raita1.XAxis.BaseTic=(xvali); 
			//raita1.XAxis.Max=(nomlength); 
			//raita1.YAxis.Max = maxtol2 + 0.1; 
			//raita1.YAxis.Min = -mintol2 - 0.1; 
			 
			//this.zedGraphControl1.GraphPane = raita1;

			zedGraphControl1.IsShowPointValues = true;
			zedGraphControl1.IsShowContextMenu = false;
			raita1.AxisChange( CreateGraphics() ); 

			this.zedGraphControl1.Refresh();  
#endif

#if false	// scroll test

			zedGraphControl1.IsAutoScrollRange = true;
			zedGraphControl1.IsEnableHPan = false;
			zedGraphControl1.IsEnableHZoom = true;
			zedGraphControl1.IsEnableVPan = false;
			zedGraphControl1.IsEnableVZoom = false;
			zedGraphControl1.IsScrollY2 = false;
			zedGraphControl1.IsShowContextMenu = true;
			zedGraphControl1.IsShowCursorValues = false;
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.IsShowPointValues = false;
			zedGraphControl1.IsShowVScrollBar = false;
			zedGraphControl1.IsZoomOnMouseCenter = false;
			zedGraphControl1.Location = new System.Drawing.Point( 3, 18 );
			zedGraphControl1.Name = "countGraph";
			zedGraphControl1.PanButtons = System.Windows.Forms.MouseButtons.Left;
			zedGraphControl1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			zedGraphControl1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			zedGraphControl1.PointDateFormat = "g";
			zedGraphControl1.PointValueFormat = "G";
			zedGraphControl1.ScrollMaxX = 0;
			zedGraphControl1.ScrollMaxY = 0;
			zedGraphControl1.ScrollMaxY2 = 0;
			zedGraphControl1.ScrollMinX = 0;
			zedGraphControl1.ScrollMinY = 0;
			zedGraphControl1.ScrollMinY2 = 0;
			zedGraphControl1.Size = new System.Drawing.Size( 559, 350 );
			zedGraphControl1.TabIndex = 0;
			zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			zedGraphControl1.ZoomStepFraction = 0.1;

			string[] labels = new string[60];
			double[] y = new double[labels.Length];
			Random random = new Random();
			for ( int i = 0; i < labels.Length; i++ )
			{
				labels[i] = "A";
				y[i] = random.NextDouble() * 50;
			}
			BarItem myBar = myPane.AddBar( "Testing", null, y, Color.Red );
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );
			myPane.XAxis.IsTicsBetweenLabels = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.TextLabels = labels;
			myPane.MinClusterGap = 2;
			myPane.AxisChange( CreateGraphics() );
			//this.Refresh();
#endif

#if false	// Basic curve test - two text axes

			double[] y = { 2, 4, 1, 5, 3 };

			LineItem myCurve = myPane.AddCurve( "curve 1", null, y, Color.Blue, SymbolType.Diamond );
			myCurve.IsOverrideOrdinal = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.YAxis.Type = AxisType.Text;

			//string[] xLabels = { "one", "two", "three", "four", "five" };
			string[] yLabels = { "alpha", "bravo", "charlie", "delta", "echo" };
			string[] xLabels = { "one", "two" };
			myPane.XAxis.Max = 5;
			myPane.XAxis.TextLabels = xLabels;
			myPane.YAxis.TextLabels = yLabels;

			myPane.AxisChange( this.CreateGraphics() );

			zedGraphControl1.IsShowPointValues = true;

#endif

#if false	// Basic curve test - 32000 points

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i=0; i<32000; i++ )
			{
				double val = rand.NextDouble();
				double x = (double) i;
				double y = x + val * val * val * 10;

				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.HDash );
			myCurve.Line.IsVisible = false;
			zedGraphControl1.IsShowCopyMessage = false;

			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false		// test
			PointPairList hList = new PointPairList();
			PointPairList cList = new PointPairList();
			string[] labels = new string[11];

			for ( int i=0; i<10; i++ )
			{
				labels[i] = "Non Renseigné";
			 
				double min = 100;
				double max = 200;
				double mediane = (min + max) / 2;

				hList.Add(i + 5, min, max);
				cList.Add(i+50, mediane);
			}
			 

			LineItem curve = myPane.AddCurve("Mediane", cList, Color.Black, ZedGraph.SymbolType.Diamond);
			//Turn off the line display, symbols only 
			curve.Line.IsVisible = false;
			//Fill the symbols with solid red color 
			curve.Symbol.Fill = new ZedGraph.Fill(Color.Red) ;
			curve.Symbol.Size = 7;
			 
			//Add a blue error bar to the graph 
			ErrorBarItem myCurve = myPane.AddErrorBar("SAT", hList, Color.Blue);
			myCurve.ErrorBar.PenWidth = 3;
			myCurve.ErrorBar.Symbol.IsVisible = false;
			 
			myPane.XAxis.Type = ZedGraph.AxisType.Ordinal;			 
			myPane.XAxis.TextLabels = labels;
 
#endif


#if false	// Basic curve test - Linear Axis

			myTimer = new Timer();
			myTimer.Enabled = true;
			myTimer.Tick += new EventHandler( myTimer_Tick );
			myTimer.Interval = 100;
			myTimer.Start();

			PointPairList list = new PointPairList();

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			zedGraphControl1.GraphPane.XAxis.Min = 0;
			zedGraphControl1.GraphPane.XAxis.Max = 100;
			zedGraphControl1.GraphPane.XAxis.Step = 10;
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.IsShowVScrollBar = true;
			zedGraphControl1.IsAutoScrollRange = true;
			//zedGraphControl1.GraphPane.IsBoundedRanges = false;
			//zedGraphControl1.ScrollMinX = 0;
			//zedGraphControl1.ScrollMaxX = 100;
#endif

#if false	// Dual Y demo

			// Get a reference to the GraphPane instance in the ZedGraphControl
			myPane = zedGraphControl1.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "Demonstration of Dual Y Graph";
			myPane.XAxis.Title = "Time, Days";
			myPane.YAxis.Title = "Parameter A";
			myPane.Y2Axis.Title = "Parameter B";
			
			// Make up some data points based on the Sine function
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<100; i++ )
			{
				double x = (double) i * 5.0;
				double y = Math.Sin( (double) i * Math.PI / 15.0 ) * 16.0;
				double y2 = y * 13.5;
				list.Add( x, y );
				list2.Add( x, y2 );
			}

			// Generate a red curve with diamond symbols, and "Alpha" in the legend
			LineItem myCurve = myPane.AddCurve( "Alpha",
				list, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with circle symbols, and "Beta" in the legend
			myCurve = myPane.AddCurve( "Beta",
				list2, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;

			// Show the x axis grid
			myPane.XAxis.IsShowGrid = true;

			// Make the Y axis scale red
			myPane.YAxis.ScaleFontSpec.FontColor = Color.Red;
			myPane.YAxis.TitleFontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			// Don't display the Y zero line
			myPane.YAxis.IsZeroLine = false;
			// Align the Y axis labels so they are flush to the axis
			myPane.YAxis.ScaleAlign = AlignP.Inside;
			// Manually set the axis range
			myPane.YAxis.Min = -30;
			myPane.YAxis.Max = 30;

			// Enable the Y2 axis display
			myPane.Y2Axis.IsVisible = true;
			// Make the Y2 axis scale blue
			myPane.Y2Axis.ScaleFontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.TitleFontSpec.FontColor = Color.Blue;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			myPane.Y2Axis.IsOppositeTic = false;
			myPane.Y2Axis.IsMinorOppositeTic = false;
			// Display the Y2 axis grid lines
			myPane.Y2Axis.IsShowGrid = true;
			// Align the Y2 axis labels so they are flush to the axis
			myPane.Y2Axis.ScaleAlign = AlignP.Inside;

			// Fill the axis background with a gradient
			myPane.AxisFill = new Fill( Color.White, Color.LightGray, 45.0f );

			// Tell ZedGraph to calculate the axis ranges
			zedGraphControl1.AxisChange();
			// Make sure the Graph gets redrawn
			zedGraphControl1.Invalidate();

			// Enable scrollbars if needed
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.IsShowVScrollBar = true;
			zedGraphControl1.IsAutoScrollRange = true;

			// Show tooltips when the mouse hovers over a point
			zedGraphControl1.IsShowPointValues = true;
			//zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler( MyPointValueHandler );

			// Add a custom context menu item
			zedGraphControl1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(MyContextMenuBuilder);

			zedGraphControl1.ScrollEvent += new ZedGraph.ZedGraphControl.ScrollEventHandler(zedGraphControl1_ScrollEvent);
#endif

#if false	// vertical bars

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i=0; i<5; i++ )
			{
				double x = (double) i;
				double y = rand.NextDouble() * 1000;
				double y2 = rand.NextDouble() * 1000;
				double y3 = rand.NextDouble() * 1000;
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
			}

			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );

			myPane.XAxis.IsTicsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five" };
			myPane.XAxis.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			zedGraphControl1.AxisChange();
			zedGraphControl1.Invalidate();
#endif

#if false
			PointPairList ppl = new PointPairList();
			ppl.Add( 0, -5e-7 ); ppl.Add( 1, -1e-4 ); ppl.Add( 2, .1);

			zedGraphControl1.GraphPane.AddBar( "", ppl, Color.Red );

			zedGraphControl1.AxisChange();
			zedGraphControl1.Invalidate(); 
#endif

			zedGraphControl1.AxisChange();
			SetSize();
			
			propertyGrid1.SelectedObject = myPane;
		}

		private void myTimer_Tick( object obj, EventArgs args )
		{
			PointPairList list = zedGraphControl1.GraphPane.CurveList[0].Points as PointPairList;
			int i = list.Count;
			double x = (double) i;
			double y = Math.Sin( x / 3.0 ) * 1 / Math.Pow(x, 0.5);
			list.Add( x, y );
			double newMax = x + 5.0;
			double delta = Math.Max( newMax - zedGraphControl1.GraphPane.XAxis.Max, 0 );
			//double oldMax = zedGraphControl1.GraphPane.XAxis.Max;
			//zedGraphControl1.GraphPane.XAxis.MaxAuto = true;
			//zedGraphControl1.GraphPane.IsBoundedRanges = false;
			zedGraphControl1.GraphPane.XAxis.Min += delta;
			zedGraphControl1.GraphPane.XAxis.Max += delta;
			zedGraphControl1.AxisChange();
			//zedGraphControl1.ScrollMaxX = zedGraphControl1.GraphPane.XAxis.Max;
			//zedGraphControl1.GraphPane.XAxis.Max = Math.Max( zedGraphControl1.GraphPane.XAxis.Max, oldMax );
			//zedGraphControl1.GraphPane.XAxis.Min += zedGraphControl1.GraphPane.XAxis.Max - oldMax;
			zedGraphControl1.Refresh();
			Application.DoEvents();
		}

		private void zedGraphControl1_ScrollEvent(ZedGraphControl control, ScrollBar scrollBar,
			ZoomState oldState, ZoomState newState)
		{
			// Here we get notification everytime the user scrolls
		}

		private string MyPointValueHandler( object sender, GraphPane pane, CurveItem curve, int iPt )
		{
			PointPair pt = curve[iPt];
			return "This value is " + pt.Y.ToString("f2") + " gallons";
		}

		private void AddNewPoint( GraphPane myPane, PointPair pt  )
		{
			// Get the first curve in the curvelist
			CurveItem curve = myPane.CurveList[0];
			// Get the Data class (this assumes that the data are in a PointPairList)
			PointPairList ppl = curve.Points as PointPairList;
			// Add the point to the PointPairList
			ppl.Add( pt );

			// Limit the number of points to 1000
			if ( ppl.Count > 1000 )
				ppl.Remove( 0 );

			// Make sure the graph gets updated
			Invalidate();
		}

		/*
		private void MyContextMenuBuilder( object sender, ContextMenu menu, Point mousePt )
		{
			//MenuItem menuItem = menu.MenuItems..Find( "Set Scale to Default", false );

			foreach( MenuItem item in menu.MenuItems )
			{
				if ( item.Text == "Set Scale to Default" )
				{
					menu.MenuItems.Remove( item );
//					item.Enabled = false;
					break;
				}
			}
		}
		*/
		private void MyContextMenuBuilder( object sender, ContextMenu menu, Point mousePt )
		{
			int count = menu.MenuItems.Count;

			// create a new menu item
			MenuItem menuItem = new MenuItem();
			// assign an index to the item
			menuItem.Index = count;
			// This is the text that will show up in the menu
			menuItem.Text = "Do Something Special";
			// This is the user-defined Tag so you can find this menu item later if necessary
			//menuItem.Tag = "my_special_tag";
			// Add a handler that will respond when that menu item is selected
			menuItem.Click += new System.EventHandler( DoSomethingSpecial );
			// Add the menu item to the menu
			menu.MenuItems.Add( menuItem );
		}

		protected void DoSomethingSpecial( object sender, System.EventArgs e )
		{
			// do something here.  For example, remove all curves from the graph
			zedGraphControl1.GraphPane.CurveList.Clear();
			zedGraphControl1.Refresh();
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			SetSize();
		}

		private void SetSize()
		{
			Size size2 = this.ClientRectangle.Size;
			if ( _isShowPropertyGrid )
			{
				propertyGrid1.Show();
				Size size = new Size( this.Size.Width - propertyGrid1.Width - zedGraphControl1.Left - 20,
										this.Size.Height - zedGraphControl1.Top - 40 );
				zedGraphControl1.Size = size;
				propertyGrid1.Left = this.Size.Width - 10 - propertyGrid1.Width;
				propertyGrid1.Height = Size.Height - 50;
			}
			else
			{
				propertyGrid1.Hide();
				Size size = new Size( this.Size.Width - zedGraphControl1.Left - 10,
										this.Size.Height - zedGraphControl1.Top - 40 );
				zedGraphControl1.Size = size;
			}
		}

		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			//clone the pane so the paneRect can be changed for printing
			//PaneBase printPane = (PaneBase) master.Clone();
			//GraphPane printPane = (GraphPane) zedGraphControl1.GraphPane.Clone();
			//printPane.PaneRect = new RectangleF( 50, 50, 400, 300 );

			//printPane.Legend.IsVisible = true;
			//printPane.PaneRect = new RectangleF( 50, 50, 300, 300 );
			//printPane.ReSize( e.Graphics, new RectangleF( 50, 50, 300, 300 ) );
				
			//e.Graphics.PageScale = 1.0F;
			//printPane.BaseDimension = 2.0F;
			zedGraphControl1.MasterPane.Draw( e.Graphics );
		}

		private void DoPageSetup()
		{
			PrintDocument pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler( Graph_PrintPage );
			PageSetupDialog setupDlg = new PageSetupDialog();
			setupDlg.Document = pd;
			setupDlg.ShowDialog();
		}

		private void DoPrint()
		{
			PrintDocument pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler( Graph_PrintPage );
			PrintDialog pDlg = new PrintDialog();
			pDlg.Document = pd;
			if ( pDlg.ShowDialog() == DialogResult.OK )
				pd.Print();
		}

		private void DoPrintPreview()
		{
			PrintDocument pd = new PrintDocument();

			PrintPreviewDialog ppd = new PrintPreviewDialog();
			pd.PrintPage += new PrintPageEventHandler( Graph_PrintPage );
			ppd.Document = pd;
			ppd.Show();
		}

		private void Form1_KeyDown( object sender, System.Windows.Forms.KeyEventArgs e )
		{
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			zedGraphControl1.GraphPane.ScaledImage( 400, 300, 72 ).Save( "zedgraph.png", ImageFormat.Png );
			return;

			myTimer.Stop();
			Invalidate();
			return;

			Bitmap img = this.zedGraphControl1.GraphPane.ScaledImage( 1000, 1000, 72 );
			img.Save( @"c:\temp\junk.png", ImageFormat.Png );
			//DoPrint();
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			Refresh();
		}

		private void zedGraphControl1_ContextMenuBuilder( ZedGraphControl sender, ContextMenu menu, Point mousePt )
		{

		}

		PointF startPt;
		double startX, startY;
		bool isDragPoint = false;
		CurveItem dragCurve;
		int dragIndex;
		PointPair startPair;

		private bool zedGraphControl1_MouseDownEvent( ZedGraphControl control, MouseEventArgs e )
		{
			// point-dragging is activated by an 'Alt' key and mousedown combination
			if ( Control.ModifierKeys == Keys.Alt )
			{
				GraphPane myPane = control.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );

				// find the point that was clicked, and make sure the point list is editable
				// and that it's a primary Y axis (the first Y or Y2 axis)
				if ( myPane.FindNearestPoint( mousePt, out dragCurve, out dragIndex ) &&
							dragCurve.Points is IPointListEdit &&
							dragCurve.YAxisIndex == 0 )
				{
					// save the starting point information
					startPt = mousePt;
					startPair = dragCurve.Points[dragIndex];
					// indicate a drag operation is in progress
					isDragPoint = true;
					// get the scale values for the start of the drag
					double startY2;
					myPane.ReverseTransform( mousePt, out startX, out startY, out startY2 );
					// if it's a Y2 axis, use that value instead of Y
					if ( dragCurve.IsY2Axis )
						startY = startY2;

					return true;
				}
			}

			return false;
		}

		private bool zedGraphControl1_MouseMoveEvent( ZedGraphControl control, MouseEventArgs e )
		{
			// see if a dragging operation is underway
			if ( isDragPoint )
			{
				// move the point
				GraphPane myPane = control.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );
				// get the scale values that correspond to the current point
				double curX, curY, curY2;
				myPane.ReverseTransform( mousePt, out curX, out curY, out curY2 );
				// if it's a Y2 axis, use that value instead of Y
				if ( dragCurve.IsY2Axis )
					curY = curY2;
				// calculate the new scale values for the point
				PointPair newPt = new PointPair( startPair.X + curX - startX, startPair.Y + curY - startY );
				// save the data back to the point list
				( dragCurve.Points as IPointListEdit )[dragIndex] = newPt;
				// force a redraw
				control.Refresh();
				// tell the ZedGraphControl not to do anything else with this event
				return true;
			}

			// since we didn't handle the event, tell the ZedGraphControl to handle it
			return false;
		}

		private bool zedGraphControl1_MouseUpEvent( ZedGraphControl control, MouseEventArgs e )
		{
			if ( isDragPoint )
			{
				// dragging operation is no longer active
				isDragPoint = false;
			}

			return false;
		}

		private void zedGraphControl1_MouseDown( object sender, MouseEventArgs e )
		{
			MessageBox.Show( "Hi" );
		}

		// masterpane with three vertical panes
		private void CreateGraph_ThreeVerticalPanes( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			master.PaneFill = new Fill( Color.FromArgb( 230, 230, 255 ) );
			master.PaneList.Clear();

			master.IsShowTitle = true;
			master.Title = "My MasterPane Title";

			master.MarginAll = 10;
			master.InnerPaneGap = 0;
			//master.Legend.IsVisible = true;
			//master.Legend.Position = LegendPos.TopCenter;

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 3; j++ )
			{
				// Create a new graph with topLeft at (40,40) and size 600x400
				GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"Case #" + ( j + 1 ).ToString(),
					"Time, Days",
					"Rate, m/s" );

				myPaneT.PaneFill = new Fill( Color.FromArgb( 230, 230, 255 ) );
				myPaneT.AxisFill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPaneT.BaseDimension = 6.0F;
				myPaneT.XAxis.IsShowTitle = false;
				myPaneT.XAxis.IsScaleVisible = false;
				myPaneT.Legend.IsVisible = false;
				myPaneT.PaneBorder.IsVisible = false;
				myPaneT.IsShowTitle = false;
				myPaneT.XAxis.IsTic = false;
				myPaneT.XAxis.IsMinorTic = false;
				myPaneT.XAxis.IsShowGrid = true;
				myPaneT.XAxis.IsShowMinorGrid = true;
				myPaneT.MarginAll = 0;
				if ( j == 0 )
					myPaneT.MarginTop = 20;
				if ( j == 2 )
				{
					myPaneT.XAxis.IsShowTitle = true;
					myPaneT.XAxis.IsScaleVisible = true;
					myPaneT.MarginBottom = 10;
				}

				// This sets the minimum amount of space for the left and right side, respectively
				// The reason for this is so that the AxisRects all end up being the same size.
				myPaneT.YAxis.MinSpace = 50;
				myPaneT.Y2Axis.MinSpace = 20;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i = 0; i < 36; i++ )
				{
					x = (double)i + 5;
					y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
					list.Add( x, y );
				}

				LineItem myCurve = myPaneT.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( myPaneT );
			}

			Graphics g = this.CreateGraphics();

			master.AutoPaneLayout( g, true, new int[] { 1, 1, 1 }, new float[] { 2, 1, 1 } );
			//master.AutoPaneLayout( g, PaneLayout.SingleColumn );
			//master.AutoPaneLayout( g, PaneLayout.ExplicitRow32 );
			//master.AutoPaneLayout( g, 2, 4 );
			//master.AutoPaneLayout( g, false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
			z1.AxisChange();

			g.Dispose();
		}

		public void CreateGraph_Contour( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title = "Sample Contour Plot";
			myPane.XAxis.Title = "X Coordinate (m)";
			myPane.YAxis.Title = "Y Coordinate (m)";

			Random rand = new Random();

			// Generate four contours
			for ( int i = 0; i < 4; i++ )
			{
				PointPairList list = new PointPairList();
				// each contour gets a point every 10 degrees
				for ( int j = 0; j < 36; j++ )
				{
					// the angle, theta, in radians
					double theta = 2.0 * Math.PI * j / 36.0;
					// the radius, with some random variability
					double r = i * 2 + 2 + 0.5 * rand.NextDouble();
					// Convert (r,p) to (x,y)
					double x = r * Math.Cos( theta ) * 10.0 + 250.0;
					double y = r * Math.Sin( theta ) * 10.0 + 250.0;
					list.Add( x, y );
				}

				// duplicate the first point at the end to complete the circle
				list.Add( list[0] );

				// Add a curve with a suitable level, no symbols
				LineItem myCurve = myPane.AddCurve( "Level=" + ( i + 1 ).ToString(), list,
					Color.Black, SymbolType.None );

				// Smooth out the contours a little
				myCurve.Line.IsSmooth = true;
				myCurve.Line.SmoothTension = 0.7f;
			}

			// Fill the curves with a different color for each curve
			( myPane.CurveList[0] as LineItem ).Line.Fill =
				new Fill( Color.White, Color.FromArgb( 255, 150, 255 ), 45.0f );
			( myPane.CurveList[1] as LineItem ).Line.Fill =
				new Fill( Color.White, Color.FromArgb( 255, 255, 150), 45.0f );
			( myPane.CurveList[2] as LineItem ).Line.Fill =
				new Fill( Color.White, Color.FromArgb( 150, 255, 150 ), 45.0f );
			( myPane.CurveList[3] as LineItem ).Line.Fill =
				new Fill( Color.White, Color.FromArgb( 150, 255, 255 ), 45.0f );

			myPane.Legend.IsVisible = false;
			myPane.AxisFill = new Fill( Color.White, Color.LightGray, 45.0f );
			myPane.PaneFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );

			// Manually set the axis ranges
			myPane.XAxis.Min = 150;
			myPane.XAxis.Max = 350;
			myPane.YAxis.Min = 150;
			myPane.YAxis.Max = 350;

			z1.AxisChange();
		}

		public void CreateGraph_Linear( ZedGraphControl z1 )
		{

			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i=1; i<100; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.IsShowVScrollBar = true;
			zedGraphControl1.IsAutoScrollRange = true;

			zedGraphControl1.GraphPane.YAxis.Type = AxisType.Log;
			//zedGraphControl1.GraphPane.XAxis.Min = 1;
			//zedGraphControl1.GraphPane.XAxis.Max = 100;
			//zedGraphControl1.GraphPane.XAxis.IsReverse = true;
			//zedGraphControl1.GraphPane.XAxis.Type = AxisType.Log;
			//zedGraphControl1.IsAutoScrollRange = true;
			//zedGraphControl1.ScrollMinX = 1;
			//zedGraphControl1.ScrollMaxX = 100;
			//zedGraphControl1.IsShowHScrollBar = true;
			//zedGraphControl1.IsEnableVZoom = false;

		}

		// Multiple stacked lines test
		private void CreateGraph_StackLine( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			PointPairList list4 = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i + 5;
				double y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 ) );
				if ( i == 15 )
				{
					list.Add( x, y );
					list2.Add( x, PointPair.Missing );
					list3.Add( x, 2 + Math.Sin( i * 0.2 + Math.PI ) );
					list4.Add( x, 2.0 );
				}
				else
				{
					list.Add( x, y );
					list2.Add( x, 1.0 );
					list3.Add( x, 2 + Math.Sin( i * 0.2 + Math.PI ) );
					list4.Add( x, 2.0 );
				}

			}
			LineItem myCurve = myPane.AddCurve( "line 1", list, Color.Black, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "line 2", list2, Color.Black, SymbolType.Square );
			LineItem myCurve3 = myPane.AddCurve( "line 3", list3, Color.Black, SymbolType.Circle );
			LineItem myCurve4 = myPane.AddCurve( "line 4", list4, Color.Black, SymbolType.Triangle );

			myPane.LineType = LineType.Stack;
			
			myCurve.Line.Fill = new Fill( Color.White, Color.Maroon, 45.0f );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Blue, 45.0f );
			myCurve3.Line.Fill = new Fill( Color.White, Color.Green, 45.0f );
			myCurve4.Line.Fill = new Fill( Color.White, Color.Red, 45.0f );
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve2.Symbol.Fill = new Fill( Color.White );
			myCurve3.Symbol.Fill = new Fill( Color.White );
			myCurve4.Symbol.Fill = new Fill( Color.White );

			RectangleF rect = new RectangleF( 20, 8, 10, 3 );
			EllipseItem ellipse = new EllipseItem( rect, Color.Black, Color.White, Color.Blue );
			myPane.GraphItemList.Add( ellipse );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();

		}


		public void CreateGraph_NormalPane( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title = "Test Graph";
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();

			for( int i = 0; i < 18; i++ )
			{
				double x = i; // new XDate( 1995, i, i+5, i, i*2, i*3 );
				
				double y1 = (Math.Sin( i / 9.0 * Math.PI ) + 1.1 ) * 100.0;
				double y2 = (Math.Cos( i / 9.0 * Math.PI ) + 1.1 ) * 100.0;
				double y3 = y1 * 10;
				list1.Add(x, y1);
				list2.Add(x, y2);
				list3.Add(x, y3);
			}

			LineItem myCurve = myPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = myPane.AddCurve("Cos", list2, Color.Blue, SymbolType.Circle);
			LineItem myCurve3 = myPane.AddCurve("Sine 2", list3, Color.Green, SymbolType.Circle);

			zedGraphControl1.IsShowPointValues = true;
			//zedGraphControl1.IsShowCursorValues = true;
			zedGraphControl1.PointValueFormat = "f2";

			myPane.XAxis.ScaleFormat = "yyyy-MM-dd HH:MM";
			myPane.XAxis.Type = AxisType.Date;
			

			foreach ( CurveItem curve in myPane.CurveList )
			{

				foreach ( PointPair pt in curve.Points as PointPairList )
				{
					double x = pt.X;
					double y = pt.Y;
					double z = pt.Z;

				}
			}
		}
	}
}
