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
			this.zedGraphControl1.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler( this.zedGraphControl1_ContextMenuBuilder );
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

#if false // Normal pane
			myPane.Title = "Test Graph";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			zedGraphControl1.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler( MyContextMenuHandler );
			zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler( MyPointValueHandler );
			zedGraphControl1.IsShowPointValues = true;

			for( int i = 0; i < 18; i++ )
			{
				x = new XDate( 1995, i, i+5, i, i*2, i*3 );
				//x = (double) i;
				
				y1 = (Math.Sin( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				y2 = (Math.Cos( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			LineItem myCurve = myPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = myPane.AddCurve("Cos", list2, Color.Blue, SymbolType.Circle);
			myCurve2.YAxisIndex = 1;
			myCurve2.IsY2Axis = true;

			zedGraphControl1.GraphPane.AddYAxis( "Another Y" );
			zedGraphControl1.GraphPane.AddY2Axis( "Another Y2" );
			zedGraphControl1.GraphPane.Y2AxisList[1].IsVisible = true;
			zedGraphControl1.Y2ScrollRangeList.Add( new ScrollRange( true ) );

			int count = zedGraphControl1.Y2ScrollRangeList.Count;
			
			//myPane.YAxis.Type = AxisType.Log;
			//myPane.YAxis.IsReverse = true;
			zedGraphControl1.IsShowPointValues = true;
			zedGraphControl1.IsShowCursorValues = true;
			//zedGraphControl1.PointDateFormat = "hh:MM:ss";
			zedGraphControl1.PointValueFormat = "f2";

			//double[] xx = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			//double[] yy = { 1, 2, 3, 4, 5, 4, 3, 2, 1, 2 };
			//double[] zz = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
			//PointPairList list = new PointPairList( xx, yy, zz );

			//Color[] colors = { Color.Red, Color.Green, Color.Blue,
			//					Color.Yellow, Color.Orange };
			//Fill fill = new Fill( colors );
			//fill.Type = FillType.GradientByZ;
			//fill.RangeMin = 1;
			//fill.RangeMax = 5;

			myPane.XAxis.ScaleFormat = "yyyy-MM-dd HH:MM";
			//myPane.XAxis.ScaleMag = 0;
			myPane.XAxis.Type = AxisType.Date;
			//myPane.YAxis.Max = 2499.9;
			//myPane.YAxis.IsScaleVisible = false;
			//myPane.YAxis.IsTic = false;
			//myPane.YAxis.IsMinorTic = false;


			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.Title = "Y2 Axis";

			myPane.Legend.Position = LegendPos.TopFlushLeft;

			//myPane.Y2Axis.IsInsideTic = false;
			//myPane.Y2Axis.IsMinorInsideTic = false;
			//myPane.Y2Axis.BaseTic = 500;
			//myPane.Y2Axis.Max = 2499.9;
			//myPane.Y2Axis.Cross = 34601;
			//myPane.Y2Axis.IsAxisSegmentVisible = false;
			//myPane.YAxis.Is

			/*
			zedGraphControl1.IsEnableVPan = false;
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.ScrollMinX = 33000;
			zedGraphControl1.ScrollMaxX = 37000;

			zedGraphControl1.IsShowVScrollBar = true;
			zedGraphControl1.ScrollMinY = -5000;
			zedGraphControl1.ScrollMaxY = 5000;

			zedGraphControl1.ScrollMinY2 = -2;
			zedGraphControl1.ScrollMaxY2 = 2;
			zedGraphControl1.IsScrollY2 = true; 
			*/

			zedGraphControl1.IsAutoScrollRange = true;
			zedGraphControl1.IsShowVScrollBar = true;
			zedGraphControl1.IsShowHScrollBar = true;
			
			/*
			GraphPane myPane2 = new GraphPane( myPane );
			zedGraphControl1.MasterPane.Add( myPane2 );
			zedGraphControl1.MasterPane.MarginAll = 20;
			zedGraphControl1.MasterPane.Title = "Master Pane Title";
			//zedGraphControl1.MasterPane.AutoPaneLayout( this.CreateGraphics(), 2, 1 );


			GraphPane myPane3 = new GraphPane( myPane );
			zedGraphControl1.MasterPane.Add( myPane3 );
			GraphPane myPane4 = new GraphPane( myPane );
			zedGraphControl1.MasterPane.Add( myPane4 );
			GraphPane myPane5 = new GraphPane( myPane );
			zedGraphControl1.MasterPane.Add( myPane5 );
			zedGraphControl1.MasterPane.AutoPaneLayout( this.CreateGraphics(), PaneLayout.ExplicitRow32 );
			*/
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

#if true			// spline test

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
			//myPane.XAxis.ScaleFormat = "dd/MM HH:mm";
			myPane.XAxis.Type = AxisType.Date;

			zedGraphControl1.IsAutoScrollRange = true;
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.IsShowVScrollBar = true;

			XDate now = new XDate( DateTime.Now );
			ArrowItem arrow = new ArrowItem( Color.Black,
					2.0f, (float) now.XLDate, 0.0f, (float) now.XLDate, 1.0f );
			arrow.IsArrowHead = false;
			arrow.Location.CoordinateFrame = CoordType.XScaleYAxisFraction;
			arrow.IsClippedToAxisRect = true;
			myPane.GraphItemList.Add( arrow );

#endif

#if false	// Basic curve test - Linear Axis

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

			//zedGraphControl1.GraphPane.XAxis.Min = 1;
			//zedGraphControl1.GraphPane.XAxis.Max = 100;
			//zedGraphControl1.GraphPane.XAxis.IsReverse = true;
			//zedGraphControl1.GraphPane.XAxis.Type = AxisType.Log;
			//zedGraphControl1.IsAutoScrollRange = true;
			//zedGraphControl1.ScrollMinX = 1;
			//zedGraphControl1.ScrollMaxX = 100;
			//zedGraphControl1.IsShowHScrollBar = true;
			//zedGraphControl1.IsEnableVZoom = false;
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

		private bool MyMouseDownEventHandler( ZedGraphControl sender, MouseEventArgs e )
		{
			if ( e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control )
			{
				this.zedGraphControl1.ZoomPane( this.zedGraphControl1.GraphPane, 0.5,
					new PointF( e.X, e.Y ), true );

				return true;
			}

			return false;
		}


		PointF		startPt;
		double		startX, startY, startY2;
		bool		isDragPoint = false;
		CurveItem	dragCurve;
		int			dragIndex;
		PointPair	startPair;

		private void zedGraphControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control )
			{
				this.zedGraphControl1.ZoomPane( this.zedGraphControl1.GraphPane, 0.5,
					new PointF( e.X, e.Y ), true );
			}
			return;

			Image image = zedGraphControl1.MasterPane.ScaledImage( 300, 200, 72 );
			image.Save( @"c:\zedgraph.png", ImageFormat.Png );
			return;

			LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
			for ( int i=0; i<500; i++ )
			{
				double x = new XDate( 1997, i, i );
				double y = (Math.Sin( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				(curve.Points as PointPairList).Add( x, y );
				zedGraphControl1.AxisChange();

				this.Refresh();
			}

			if ( Control.ModifierKeys == Keys.Alt )
			{
				GraphPane myPane = zedGraphControl1.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );

				if ( myPane.FindNearestPoint( mousePt, out dragCurve, out dragIndex ) )
				{
					startPt = mousePt;
					startPair = dragCurve.Points[dragIndex];
					isDragPoint = true;
					myPane.ReverseTransform( mousePt, out startX, out startY, out startY2 );
				}
			}

			/*
			if ( myPane.YAxis.Type == AxisType.Linear )
				myPane.YAxis.Type = AxisType.Log;
			else
				myPane.YAxis.Type = AxisType.Linear;

			zedGraphControl4.AxisChange();
			Refresh();
			*/

			/*
			double rangeX = myPane.XAxis.Max - myPane.XAxis.Min;
			myPane.XAxis.Max -= rangeX/20.0;
			myPane.XAxis.Min += rangeX/20.0;
			double rangeY = myPane.YAxis.Max - myPane.YAxis.Min;
			myPane.YAxis.Max -= rangeY/20.0;
			myPane.YAxis.Min += rangeY/20.0;
			zedGraphControl4.AxisChange();
			zedGraphControl4.Refresh();
			//Invalidate();
			*/
		}

		private void zedGraphControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( isDragPoint )
			{
				// move the point
				double curX, curY, curY2;
				GraphPane myPane = zedGraphControl1.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );
				myPane.ReverseTransform( mousePt, out curX, out curY, out curY2 );
				PointPair newPt = new PointPair( startPair.X + curX - startX, startPair.Y + curY - startY );
				(dragCurve.Points as PointPairList)[dragIndex] = newPt;
				zedGraphControl1.Refresh();
			}
		}

		private void zedGraphControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( isDragPoint )
			{
				// finalize the move
				isDragPoint = false;
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

		private void zedGraphControl1_Load( object sender, EventArgs e )
		{

		}

		private void zedGraphControl1_ContextMenuBuilder( ZedGraphControl sender, ContextMenu menu, Point mousePt )
		{

		}

	}
}
