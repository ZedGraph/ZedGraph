
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace ZedGraph.ControlTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			//CreateGraph_32kPoints( zedGraphControl1 );
			//CreateGraph_BarJunk( zedGraphControl1 );
			//CreateGraph_BarJunk2( zedGraphControl1 );
			CreateGraph_BasicLinear( zedGraphControl1 );
			//CreateGraph_BasicLinear3Curve( zedGraphControl1 );
			//CreateGraph_BasicLinearReverse( zedGraphControl1 );
			//CreateGraph_BasicLinearScroll( zedGraphControl1 );
			//CreateGraph_BasicLog( zedGraphControl1 );
			//CreateGraph_BasicStick( zedGraphControl2 );
			//CreateGraph_CandleStick( zedGraphControl1 );
			//CreateGraph_ClusteredStackBar( zedGraphControl1 );
			//CreateGraph_Contour( zedGraphControl2 );
			//CreateGraph_DateAxis( zedGraphControl1 );
			//CreateGraph_DateAxisTutorial( zedGraphControl1 );
			//CreateGraph_DataSource( zedGraphControl1 );
			//CreateGraph_DateWithTimeSpan( zedGraphControl1 );
			//CreateGraph_DualYDemo( zedGraphControl1 );
			//CreateGraph_FilteredPointList( zedGraphControl1 );
			//CreateGraph_GradientByZBars( zedGraphControl1 );
			//CreateGraph_GrowingData( zedGraphControl1 );
			//CreateGraph_HiLowBarDemo( zedGraphControl1 );
			//CreateGraph_HorizontalBars( zedGraphControl1 );
			//CreateGraph_Histogram( zedGraphControl1 );
			//CreateGraph_ImageSymbols( zedGraphControl1 );
			//CreateGraph_JapaneseCandleStick( zedGraphControl1 );
			//CreateGraph_Junk( zedGraphControl1 );
			//CreateGraph_Junk2( zedGraphControl1 );
			//CreateGraph_Junk4( zedGraphControl1 );
			//CreateGraph_junk5( zedGraphControl1 );
			//CreateGraph_junk6( zedGraphControl1 );
			//CreateGraph_junk7( zedGraphControl1 );
			//CreateGraph_MasterPane( zedGraphControl1 );
			//CreateGraph_MasterPane_Tutorial( zedGraphControl1 );
			//CreateGraph_MasterPane_Square( zedGraphControl1 );
			//CreateGraph_MasterWithPies( zedGraphControl1 );
			//CreateGraph_MultiYDemo( zedGraphControl1 );
			//CreateGraph_NoDupePointList( zedGraphControl1 );
			//CreateGraph_NormalPane( zedGraphControl1 );
			//CreateGraph_OnePoint( zedGraphControl1 );
			//CreateGraph_OverlayBarDemo( zedGraphControl1 );
			//CreateGraph_Pie( zedGraphControl1 );
			//CreateGraph_PolyTest( zedGraphControl2 );
			//CreateGraph_RadarPlot( zedGraphControl1 );
			//CreateGraph_SamplePointListDemo( zedGraphControl1 );
			//CreateGraph_ScrollTest( zedGraphControl1 );
			//CreateGraph_ScrollProblem( zedGraphControl1 );
			//CreateGraph_SpiderPlot( zedGraphControl1 );
			//CreateGraph_SplineTest( zedGraphControl1 );
			//CreateGraph_StackedBars( zedGraphControl1 );
			//CreateGraph_StackedMultiBars( zedGraphControl1 );
			//CreateGraph_StackLine( zedGraphControl1 );
			//CreateGraph_StickToCurve( zedGraphControl1 );
			//CreateGraph_TestScroll( zedGraphControl1 );
			//CreateGraph_TextBasic( zedGraphControl2 );
			//CreateGraph_ThreeVerticalPanes( zedGraphControl1 );
			//CreateGraph_TwoTextAxes( zedGraphControl1 );
			//CreateGraph_VerticalBars( zedGraphControl1 );
			//CreateGraph_DualY( zedGraphControl1 );


			zedGraphControl1.AxisChange();
			SetSize();
		}

		private void Form1_Resize( object sender, EventArgs e )
		{
			SetSize();
		}

		private void SetSize()
		{
			zedGraphControl1.Location = new Point( 10, 10 );
			// Leave a small margin around the outside of the control
			zedGraphControl1.Size = new Size( this.ClientRectangle.Width - 20,
					this.ClientRectangle.Height - 40 );

			/*
			Rectangle pageRect = this.ClientRectangle;
			pageRect.Inflate( -10, -10 );
			pageRect.Height -= 20;
			//tabControl1.Size = formRect.Size;


			//Rectangle pageRect = tabControl1.SelectedTab.ClientRectangle;
			//pageRect.Inflate( -10, -10 );

			if ( zedGraphControl1.Size != pageRect.Size )
				zedGraphControl1.Size = pageRect.Size;

			double junk = DateTime.Now.ToOADate();
			// Fix the ellipseItem to a perfect circle by using a fixed height, but a variable
			// width
			if ( zedGraphControl1.GraphPane.GraphObjList.Count > 0 )
			{
				EllipseObj ellipse = zedGraphControl1.GraphPane.GraphObjList[0] as EllipseObj;
				if ( ellipse != null )
				{
					GraphPane myPane = zedGraphControl1.GraphPane;
					float dx = (float)( myPane.XAxis.Scale.Max - myPane.XAxis.Scale.Min );
					float dy = (float)( myPane.YAxis.Scale.Max - myPane.YAxis.Scale.Min );
					float xPix = myPane.Chart.Rect.Width * (float)ellipse.Location.Width / dx;
					float yPix = myPane.Chart.Rect.Height * (float)ellipse.Location.Height / dy;

					ellipse.Location.Width *= yPix / xPix;

					// alternatively, use this to vary the height but fix the width
					// (comment out the width line above)
					//ellipse.Location.Height *= xPix / yPix;
				}
			}
			*/
		}

		private void Serialize( ZedGraphControl z1, string fileName )
		{
			if ( z1 != null && !String.IsNullOrEmpty( fileName ) )
			{
				BinaryFormatter mySerializer = new BinaryFormatter();
				Stream myWriter = new FileStream( fileName, FileMode.Create,
						FileAccess.Write, FileShare.None );

				mySerializer.Serialize( myWriter, z1.MasterPane );
				//MessageBox.Show( "Serialized output created" );
				myWriter.Close();
			}
		}


		private void SoapSerialize( ZedGraphControl z1, string fileName )
		{
			if ( z1 != null && !String.IsNullOrEmpty( fileName ) )
			{
				SoapFormatter mySerializer = new SoapFormatter();
				Stream myWriter = new FileStream( fileName, FileMode.Create,
						FileAccess.Write, FileShare.None );

				mySerializer.Serialize( myWriter, z1.MasterPane );
				//MessageBox.Show( "Serialized output created" );
				myWriter.Close();
			}
		}

		private void SoapDeSerialize( ZedGraphControl z1, string fileName )
		{
			if ( z1 != null && !String.IsNullOrEmpty( fileName ) )
			{
				SoapFormatter mySerializer = new SoapFormatter();
				Stream myReader = new FileStream( fileName, FileMode.Open,
					FileAccess.Read, FileShare.Read );

				MasterPane master = (MasterPane)mySerializer.Deserialize( myReader );
				z1.Refresh();

				myReader.Close();

				z1.MasterPane = master;
				//trigger a resize event
				z1.Size = z1.Size;
			}
		}


		private void DeSerialize( ZedGraphControl z1, string fileName )
		{
			if ( z1 != null && !String.IsNullOrEmpty( fileName ) )
			{
				BinaryFormatter mySerializer = new BinaryFormatter();
				Stream myReader = new FileStream( fileName, FileMode.Open,
					FileAccess.Read, FileShare.Read );

				MasterPane master = (MasterPane)mySerializer.Deserialize( myReader );
				z1.Refresh();

				myReader.Close();

				z1.MasterPane = master;
				//trigger a resize event
				z1.Size = z1.Size;
			}
		}

		private void CreateGraph_NormalPane( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Test Graph";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			//z1.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler( MyContextMenuHandler );
			//z1.PointValueEvent += new ZedGraphControl.PointValueHandler( MyPointValueHandler );
			z1.IsShowPointValues = true;
			z1.IsEnableHZoom = true;
			z1.IsEnableVZoom = true;

			for ( int i = 11; i < 16; i++ )
			{
				//x = new XDate( 1995, 8, 5, i, i * 2, i * 3 );
				x = (double) i;

				y1 = ( Math.Sin( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				y2 = ( Math.Cos( i / 9.0 * Math.PI ) + 1.1 ) * 20.0 + 5.0;
				double z = i > 9 ? 1 : 0;
				list1.Add( x, y1, z );
				list2.Add( x, y2, z );

			}

			LineItem myCurve = myPane.AddCurve( "Sine", list1, Color.Red, SymbolType.Circle );
			LineItem myCurve2 = myPane.AddCurve( "Cos", list2, Color.Blue, SymbolType.Circle );
			myCurve2.YAxisIndex = 0;
			myCurve2.IsY2Axis = true;

			z1.GraphPane.AddYAxis( "Another Y" );
			z1.GraphPane.AddY2Axis( "Another Y2" );
			z1.GraphPane.Y2AxisList[1].IsVisible = true;
			z1.Y2ScrollRangeList.Add( new ScrollRange( true ) );

			int count = z1.Y2ScrollRangeList.Count;

			//myPane.YAxis.Type = AxisType.Log;
			//myPane.YAxis.IsReverse = true;
			z1.IsShowPointValues = true;
			z1.IsShowCursorValues = true;
			//z1.PointDateFormat = "hh:MM:ss";
			z1.PointValueFormat = "f2";

			//double[] xx = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			//double[] yy = { 1, 2, 3, 4, 5, 4, 3, 2, 1, 2 };
			//double[] zz = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
			//PointPairList list = new PointPairList( xx, yy, zz );

			Color[] colors = { Color.Red, Color.Transparent };
			Fill fill = new Fill( colors );
			fill.Type = FillType.GradientByZ;
			fill.SecondaryValueGradientColor = Color.Empty;
			fill.RangeMin = 0;
			fill.RangeMax = 1;
			myCurve.Symbol.Fill = fill;
			myCurve.Symbol.Border.IsVisible = false;

			//myPane.XAxis.Scale.Format = "HHMM";
			//myPane.XAxis.ScaleMag = 0;
			//myPane.XAxis.Type = AxisType.Date;
			//myPane.YAxis.Max = 2499.9;
			//myPane.YAxis.IsScaleVisible = false;
			//myPane.YAxis.IsTic = false;
			//myPane.YAxis.IsMinorTic = false;

			myPane.XAxis.Scale.BaseTic = 10;
			myPane.XAxis.Scale.Min = 10;
			myPane.XAxis.Scale.Max = 16;
			myPane.XAxis.Scale.MajorStep = 3;


			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.Title.Text = "Y2 Axis";

			myPane.Legend.Position = LegendPos.TopFlushLeft;

			//myPane.Y2Axis.IsInsideTic = false;
			//myPane.Y2Axis.IsMinorInsideTic = false;
			//myPane.Y2Axis.BaseTic = 500;
			//myPane.Y2Axis.Max = 2499.9;
			//myPane.Y2Axis.Cross = 34601;
			//myPane.Y2Axis.IsAxisSegmentVisible = false;
			//myPane.YAxis.Is

			/*
			z1.IsEnableVPan = false;
			z1.IsShowHScrollBar = true;
			z1.ScrollMinX = 33000;
			z1.ScrollMaxX = 37000;

			z1.IsShowVScrollBar = true;
			z1.ScrollMinY = -5000;
			z1.ScrollMaxY = 5000;

			z1.ScrollMinY2 = -2;
			z1.ScrollMaxY2 = 2;
			z1.IsScrollY2 = true; 
			*/

			z1.IsAutoScrollRange = true;
			z1.IsShowVScrollBar = true;
			z1.IsShowHScrollBar = true;
			z1.IsScrollY2 = true; 

			/*
			GraphPane myPane2 = new GraphPane( myPane );
			z1.MasterPane.Add( myPane2 );
			z1.MasterPane.MarginAll = 20;
			z1.MasterPane.Title = "Master Pane Title";
			//z1.MasterPane.AutoPaneLayout( this.CreateGraphics(), 2, 1 );


			GraphPane myPane3 = new GraphPane( myPane );
			z1.MasterPane.Add( myPane3 );
			GraphPane myPane4 = new GraphPane( myPane );
			z1.MasterPane.Add( myPane4 );
			GraphPane myPane5 = new GraphPane( myPane );
			z1.MasterPane.Add( myPane5 );
			z1.MasterPane.AutoPaneLayout( this.CreateGraphics(), PaneLayout.ExplicitRow32 );
			*/

		}
		private void CreateGraph_MasterWithPies( ZedGraphControl z1 )
		{
			GraphPane pane = z1.GraphPane;

			pane.Title.Text = "Test Pie";

			PieItem myPie = pane.AddPieSlice( 10, Color.Red, 0.0, "First" );
			myPie = pane.AddPieSlice( 20, Color.Blue, 0.0, "Second" );
			myPie = pane.AddPieSlice( 15, Color.Green, 0.0, "Third" );
			myPie = pane.AddPieSlice( 40, Color.Purple, 0.0, "Fourth" );
			myPie = pane.AddPieSlice( 27, Color.Pink, 0.0, "Fifth" );

			z1.MasterPane[0] = pane;
			z1.MasterPane.Add( pane.Clone() as GraphPane );
			z1.MasterPane.Add( pane.Clone() as GraphPane );
			z1.MasterPane.Add( pane.Clone() as GraphPane );
			z1.MasterPane.Add( pane.Clone() as GraphPane );

			using ( Graphics g = z1.CreateGraphics() )
				z1.MasterPane.SetLayout( g, PaneLayout.ExplicitRow32 );

		}

		// Basic curve test - Date Axis w/ Time Span
		private void CreateGraph_DateWithTimeSpan( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i = 0; i < 100; i++ )
			{
				double x = (double)i / 123.0;
				//double x = new XDate( 0, 0, i, i*3, i*2, i );
				double y = Math.Sin( i / 8.0 ) * 1 + 1;
				list.Add( x, y );
				double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			//myPane.XAxis.Scale.IsSkipLastLabel = false;
			myPane.XAxis.Scale.Format = "[d].[h]:[m]:[s]";
			z1.PointDateFormat = "[d].[hh]:[mm]:[ss]";
			myPane.XAxis.Type = AxisType.Date;
			z1.AxisChange();

			//myPane.YAxis.Scale.Format = "0.0'%'";
		}

		private void CreateGraph_SamplePointListDemo( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Demonstration of SamplePointList";
			myPane.XAxis.Title.Text = "Time since start, days";
			myPane.YAxis.Title.Text = "Postion (meters) or\nAverage Velocity (meters/day)";
			myPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 230, 230, 255 ), 45.0f );

			SamplePointList spl = new SamplePointList();

			// Calculate sample data _points
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

		}

		private void CreateGraph_RadarPlot( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

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

			myPane.XAxis.MajorGrid.IsZeroLine = true;

			for ( int i = 0; i < 7; i++ )
			{
				ArrowObj arrow = new ArrowObj( 0, 0, (float)rpl[i].X, (float)rpl[i].Y );
				arrow.IsArrowHead = false;
				arrow.Color = Color.LightGray;
				arrow.ZOrder = ZOrder.D_BehindCurves;
				myPane.GraphObjList.Add( arrow );
			}

			myPane.XAxis.MajorTic.IsAllTics = true;
			myPane.XAxis.MinorTic.IsAllTics = true;
			myPane.XAxis.Title.IsTitleAtCross = false;
			myPane.XAxis.Cross = 0;
			myPane.XAxis.Scale.IsSkipFirstLabel = true;
			myPane.XAxis.Scale.IsSkipLastLabel = true;
			myPane.XAxis.Scale.IsSkipCrossLabel = true;

			myPane.YAxis.MajorTic.IsAllTics = true;
			myPane.YAxis.MinorTic.IsAllTics = true;
			myPane.YAxis.Title.IsTitleAtCross = false;
			myPane.YAxis.Cross = 0;
			myPane.YAxis.Scale.IsSkipFirstLabel = true;
			myPane.YAxis.Scale.IsSkipLastLabel = true;
			myPane.YAxis.Scale.IsSkipCrossLabel = true;
		}

		// Traditional Candlestick
		private void CreateGraph_CandleStick( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Candlestick Chart Demo";
			myPane.XAxis.Title.Text = "Trading Date";
			myPane.YAxis.Title.Text = "Share Price, $US";

			StockPointList spl = new StockPointList();
			Random rand = new Random();

			// First day is feb 1st
			XDate xDate = new XDate( 2006, 2, 1 );
			double open = 50.0;

			for ( int i = 0; i < 20; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;

				StockPt pt = new StockPt( x, hi, low, open, close, 100000 );
				spl.Add( pt );

				open = close;
				// Advance one day
				xDate.AddDays( 1.0 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			//CandleStickItem myCurve = myPane.AddCandleStick( "trades", spl, Color.Black );
			CandleStickItem myCurve = myPane.AddCandleStick( "trades", spl, Color.Blue );
			//myCurve.Stick.Size = 20;
			myCurve.Stick.IsAutoSize = true;
			//myCurve.CandleStick.PenWidth = 2;
			//myCurve.CandleStick.IsOpenCloseVisible = false;
			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			//myPane.XAxis.Type = AxisType.Date;
			//myPane.XAxis.Scale.MajorStep = 1.0;

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			BoxObj box = new BoxObj( 4, 60, 5, 50000 );
			myPane.GraphObjList.Add( box );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();

		}

		// Japanese Candlestick
		private void CreateGraph_JapaneseCandleStick( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Japanese Candlestick Chart Demo";
			myPane.XAxis.Title.Text = "Trading Date";
			myPane.YAxis.Title.Text = "Share Price, $US";

			StockPointList spl = new StockPointList();
			Random rand = new Random();

			// First day is jan 1st
			XDate xDate = new XDate( 2006, 1, 1 );
			double open = 50.0;

			for ( int i = 0; i < 30; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;

				StockPt pt = new StockPt( x, hi, low, open, close, 100000 );
				spl.Add( pt );

				open = close;
				// Advance one day
				xDate.AddDays( 1.0 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick( "trades", spl );
			myCurve.Stick.IsAutoSize = true;
			myCurve.Stick.Color = Color.Blue;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();
		}

		//Timer myTimer;

		private void MyTimer_Tick( object sender, EventArgs e )
		{
			// Get the first CurveItem in the graph
			LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
			// Get the PointPairList
			PointPairList list = curve.Points as PointPairList;
			//list.Add( xvalue, (double)cpuUsagePerformanceCounter.NextValue() );
			list.Add( 1, 1 );
		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinear( ZedGraphControl z1 )
		{
			Color rgb = Color.FromArgb( 123, 240, 098 );
			HSBColor hsb = HSBColor.FromRGB( rgb );
			Color rgb2 = hsb.ToRGB();


			GraphPane myPane = z1.GraphPane;

			//myTimer = new Timer();
			//myTimer.Enabled = true;
			//myTimer.Tick += new EventHandler( MyTimer_Tick );
			//myTimer.Interval = 500;
			//myTimer.Start();
			myPane.XAxis.Type = AxisType.Date;

			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = XDate.CalendarDateToXLDate( 2006, i, 1, 0, 0, 0 );

				double y = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.2 ) );
				list.Add( x, y, 0 );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myCurve.Symbol.Fill = new Fill( Color.White, Color.Red );
			myCurve.Symbol.Fill.Type = FillType.GradientByZ;
			myCurve.Symbol.Fill.RangeMin = 0;
			myCurve.Symbol.Fill.RangeMax = 1;
			myCurve.Symbol.Fill.RangeDefault = 0;
			myCurve.Symbol.Fill.SecondaryValueGradientColor = Color.Empty;

			//z1.IsShowHScrollBar = true;
			//z1.IsShowVScrollBar = true;
			//z1.IsAutoScrollRange = true;

			z1.IsEnableHEdit = true;
			z1.IsEnableVEdit = true;
			//z1.IsEnableVEdit = false;

			z1.IsEnableVZoom = false;
			z1.GraphPane.IsBoundedRanges = false;

			//z1.GraphPane.IsBoundedRanges = false;
			//z1.ScrollMinX = 0;
			//z1.ScrollMaxX = 100;

			// line is black color, width is 2.0
			// The line is located at X value = 10.0
			// The line runs from 0 to 1 (chart fraction)
			LineObj line = new LineObj();
			line.Color = Color.Black;
			line.Location.X1 = XDate.CalendarDateToXLDate( 2007, 1, 1, 0, 0, 0 );
			line.Location.Y1 = 0;
			line.Location.Width = 0;
			line.Location.Height = 1;
			line.PenWidth = 5.0f;
			line.IsClippedToChartRect = true;
			line.ZOrder = ZOrder.E_BehindAxis;
			line.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
			myPane.GraphObjList.Add( line );

			myPane.XAxis.Title.FontSpec.Family = "Tahoma";

			//myPane.YAxis.Scale.IsReverse = true;

			myPane.Margin.Left = 40;
			myPane.Margin.Bottom = 40;
			myPane.XAxis.Title.IsVisible = false;
			myPane.YAxis.Title.IsVisible = false;
			myPane.Fill = new Fill( Color.White, Color.SkyBlue, 45.0f );

			BoxObj box2 = new BoxObj( 0.4, 0.02, 0.2, 0.2, Color.Black, Color.Red );
			box2.Location.CoordinateFrame = CoordType.PaneFraction;
			box2.ZOrder = ZOrder.G_BehindAll;
			z1.GraphPane.GraphObjList.Add( box2 );

			BoxObj box = new BoxObj( 0, 0, 1, 1, Color.Black, Color.Empty );
			box.Location.CoordinateFrame = CoordType.PaneFraction;
			box.ZOrder = ZOrder.F_BehindChartFill;
			myPane.GraphObjList.Add( box );

			box = new BoxObj( .05, .0, 1.0, 0.92, Color.Empty, Color.LightGoldenrodYellow );
			box.Location.CoordinateFrame = CoordType.PaneFraction;
			box.ZOrder = ZOrder.G_BehindAll;
			myPane.GraphObjList.Add( box );
			box.Fill = new Fill( new Color[] { 
    Color.FromArgb(0, Color.Black), 
    Color.FromArgb(20, Color.Black), 
    Color.FromArgb(36, 219, 170), 
    Color.FromArgb(38, 225, 175), 
    Color.FromArgb(33, 204, 157), 
    Color.FromArgb(32, 194, 149),
    Color.FromArgb(60, Color.Black), 
    Color.FromArgb(0, Color.Black)
}, new float[] {
    0f,
    0.1428f,
    0.1785f,
    0.3214f,
    0.5714f,
    0.75f,
    0.7857f,
    1f 
} );

			//myPane.XAxis.Type = AxisType.Linear;
			myPane.XAxis.Scale.Format = "HH:mm:ss.fff";
			//myPane.XAxis.Scale.Format = "HH:mm";
			//myPane.XAxis.Scale.Format = "h tt";
			//myPane.XAxis.Scale.Format = "dd-MMM-yyyy";
			//myPane.XAxis.Scale.Format = "y";
			//myPane.XAxis.Scale.Format = "T";
			//myPane.XAxis.Scale.Format = "g";
			//myPane.XAxis.Scale.Format = "f";
			//myPane.XAxis.Scale.Format = "D";
			//myPane.XAxis.Scale.Format = "d";
			//myPane.XAxis.Scale.Format = "e";
			//myPane.XAxis.Scale.Format = "e2";
			//myPane.XAxis.Scale.Format = "c0";
			//myPane.XAxis.Scale.Format = "f2";
			//myPane.XAxis.Scale.Format = "0.00'%'";
			z1.AxisChange();
			z1.IsAntiAlias = true;

			z1.GraphPane.XAxis.Color = Color.Red;
			z1.GraphPane.XAxis.MajorTic.Color = Color.Blue;
			z1.GraphPane.XAxis.MajorTic.PenWidth = 1.0f;
			//z1.GraphPane.Chart.Border.IsVisible = false;
			//z1.GraphPane.XAxis.Type = AxisType.Log;

			z1.GraphPane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler( XScaleFormatEvent );
			//z1.MasterPane[0].YAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler( YScaleFormatEvent );

		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinear3Curve( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = i + 100;

				double y = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.2 ) );
				double y2 = 200.0 * ( 2.0 + Math.Sin( (double)i * 0.3 ) );
				double y3 = 100.0 * ( 3.0 + Math.Sin( (double)i * 0.4 ) );
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
			}
			//list = new PointPairList();
			//list2 = new PointPairList();
			//list3 = new PointPairList();
			LineItem myCurve = myPane.AddCurve( "curve 1", list, Color.Blue, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "Here's a really long curve name that should stand out quite a bit", list2, Color.Red, SymbolType.Diamond );
			LineItem myCurve3 = myPane.AddCurve( "curve 3", list3, Color.Green, SymbolType.Diamond );

			myPane.Legend.Position = LegendPos.Bottom;
			myPane.Legend.IsHStack = false;

			z1.AxisChange();

		}

		public string XScaleFormatEvent( GraphPane pane, Axis axis, double val, int index )
		{
			return val.ToString("f2") + " cm";
		}

		public string YScaleFormatEvent( GraphPane pane, Axis axis, double val, int index )
		{
			return "( Y= " + val.ToString() + ")";
		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinearReverse( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i + 5;
				double y = 300.0 * ( 1.5 + Math.Sin( (double)i * 0.2 ) );
				list.Add( x, y, 0 );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myPane.Legend.IsVisible = false;
			myPane.XAxis.Cross = 0;
			myPane.XAxis.Scale.IsLabelsInside = true;
			myPane.YAxis.Scale.IsReverse = true;

			z1.AxisChange();
		}

		// Basic curve test - Log Axis
		private void CreateGraph_BasicLog( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i * 1000 + 100;
				double y = 3000.0 * ( 1.5 + Math.Sin( (double)i * 0.2 ) ) + 1.0;
				list.Add( x, y );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myPane.XAxis.Type = AxisType.Log;
			myPane.YAxis.Type = AxisType.Log;
			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;
			z1.IsAutoScrollRange = true;

			z1.IsEnableVEdit = true;
			z1.IsEnableHEdit = true;

			//z1.IsEnableZoom = true;
			myPane.IsBoundedRanges = false;

			z1.AxisChange();

			myPane.XAxis.Scale.MajorStep = 100;
		}

		// Basic curve test with images for symbols
		private void CreateGraph_ImageSymbols( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Demonstration Chart with an Image for Symbols";
			myPane.XAxis.Title.Text = "Some Independent Value";
			myPane.YAxis.Title.Text = "The Dependent Axis";

			PointPairList list = new PointPairList();
			for ( int i = 0; i < 20; i++ )
			{
				double x = (double)i + 5;
				double y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.4 ) );
				list.Add( x, y );
			}
			LineItem myCurve = myPane.AddCurve( "Smile! It's only data", list, Color.Black, SymbolType.Square );

			Bitmap bm = new Bitmap( @"..\..\teeth.png" );
			Image image = Image.FromHbitmap( bm.GetHbitmap() );

			myCurve.Symbol.Type = SymbolType.Circle;
			myCurve.Symbol.Size = 18;
			myCurve.Symbol.Border.IsVisible = false;
			myCurve.Symbol.Fill = new Fill( image, WrapMode.Clamp );

			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 255, 255 ), 45.0f );

		}

		// Basic curve test with images for symbols
		private void CreateGraph_OnePoint( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			list.Add( 0.5, 0.5 );
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Black, SymbolType.Circle );

			myCurve.Symbol.Size = 16;
			myCurve.Symbol.Fill = new Fill( Color.White, Color.FromArgb( 120, 120, 255 ), 45.0f );
			myCurve.Symbol.Fill.IsScaled = true;
			myCurve.Line.IsVisible = false;

		}

		// Multiple stacked lines test
		private void CreateGraph_StackLine( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			PointPairList list4 = new PointPairList();
			for ( int i = 0; i < 36; i+=5 )
			{
				double x = (double)i + 5;
				double y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 ) ) - 3.0;
				if ( i == 15 )
				{
					list.Add( x, y );
					//list2.Add( x, PointPair.Missing );
					list2.Add( x, 1.0 );
					list3.Add( x, 2 + Math.Sin( i * 0.2 + Math.PI ) );
					list4.Add( x, 7.0 );
				}
				else
				{
					list.Add( x, y );
					list2.Add( x, 1.0 );
					list3.Add( x, 2 + Math.Sin( i * 0.2 + Math.PI ) );
					list4.Add( x, 7.0 );
				}

			}
			LineItem myCurve = myPane.AddCurve( "line 1", list, Color.Black, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "line 2", list2, Color.Black, SymbolType.Square );
			//LineItem myCurve3 = myPane.AddCurve( "line 3", list3, Color.Black, SymbolType.Circle );

			myPane.LineType = LineType.Stack;
			myPane.YAxis.Color = Color.Red;
			myPane.XAxis.Scale.Align = AlignP.Inside;
			myPane.XAxis.Color = Color.Blue;
			myPane.YAxis.Scale.IsReverse = true;
			z1.AxisChange();


			myPane.YAxis.Scale.MaxAuto = false;
			//int l = ColorSymbolRotator.COLORS.Length;
			//LineObj line = new LineObj();
			//line.IsClippedToChartRect = true;

			LineItem myCurve4 = myPane.AddCurve( "   line 4", list4, Color.Black, SymbolType.Triangle );
			myCurve4.Label.Text = myCurve4.Label.Text.Trim();

			//myCurve2.IsY2Axis = true;

			myCurve.Line.Fill = new Fill( Color.White, Color.Maroon, 45.0f );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Blue, 45.0f );
			//myCurve3.Line.Fill = new Fill( Color.White, Color.Green, 45.0f );
			myCurve4.Line.Fill = new Fill( Color.White, Color.Red, 45.0f );
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve2.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.SmoothTension = 0.5f;
			myCurve2.Line.SmoothTension = 0.5f;
			myCurve.Line.IsSmooth = true;
			myCurve2.Line.IsSmooth = true;
			//myCurve3.Symbol.Fill = new Fill( Color.White );
			//myCurve4.Symbol.Fill = new Fill( Color.White );

			//EllipseObj ellipse = new EllipseObj( 20, 8, 10, 3, Color.Black, Color.White, Color.Blue );
			//myPane.GraphObjList.Add( ellipse );

			myPane.Legend.IsHStack = false;
			myPane.Legend.Position = LegendPos.Left;
			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();

		}

		// masterpane test
		private void CreateGraph_MasterPane( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			master.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			master.PaneList.Clear();

			master.Title.IsVisible = true;
			master.Title.Text = "My MasterPane Title";

			master.Margin.All = 10;
			//master.InnerPaneGap = 10;
			//master.Legend.IsVisible = true;
			//master.Legend.Position = LegendPos.TopCenter;

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 6; j++ )
			{
				master.Add( AddGraph( j, rotator ) );
			}

			using ( Graphics g = this.CreateGraphics() )
			{

				//master.PaneLayoutMgr.SetLayout( PaneLayout.ExplicitRow32 );
				//master.PaneLayoutMgr.SetLayout( 2, 4 );
				master.SetLayout( g, false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
				//master.SetLayout( PaneLayout.SingleColumn );
				master.IsCommonScaleFactor = true;
				z1.AxisChange();

			}

		}

		public GraphPane AddGraph( int j, ColorSymbolRotator rotator )
		{
			// Create a new graph with topLeft at (40,40) and size 600x400
			GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"Case #" + ( j + 1 ).ToString(),
				"Time, Days",
				"Rate, m/s" );

			myPaneT.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
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

			return myPaneT;
		}

		public void SwitchPanes()
		{
			MasterPane master = zedGraphControl1.MasterPane;

			master.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			master.PaneList.Clear();

			master.Title.IsVisible = true;
			master.Title.Text = "My MasterPane Title";
			master.Margin.All = 10;

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 4; j++ )
				master.Add( AddGraph( j, rotator ) );

			using ( Graphics g = this.CreateGraphics() )
			{

				//master.PaneLayoutMgr.SetLayout( PaneLayout.ExplicitRow32 );
				//master.PaneLayoutMgr.SetLayout( 2, 4 );
				master.SetLayout( g, PaneLayout.SquareColPreferred );
				//master.DoLayout( g );
				master.IsCommonScaleFactor = true;
				zedGraphControl1.AxisChange();
				zedGraphControl1.Invalidate();
			}
		}

		// Call this method from the Form_Load method, passing your ZedGraphControl
		public void CreateGraph_MasterPane_Tutorial( ZedGraphControl zgc )
		{
			MasterPane myMaster = zgc.MasterPane;

			myMaster.PaneList.Clear();

			// Set the masterpane title
			myMaster.Title.Text = "ZedGraph MasterPane Example";
			myMaster.Title.IsVisible = true;

			// Fill the masterpane background with a color gradient
			myMaster.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );

			// Set the margins to 10 points
			myMaster.Margin.All = 10;

			// Enable the masterpane legend
			myMaster.Legend.IsVisible = true;
			myMaster.Legend.Position = LegendPos.TopCenter;

			// Add a priority stamp
			TextObj text = new TextObj( "Priority", 0.88F, 0.12F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;
			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;
			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			myMaster.GraphObjList.Add( text );

			// Add a draft watermark
			text = new TextObj( "DRAFT", 0.5F, 0.5F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;
			text.FontSpec.Angle = 30.0F;
			text.FontSpec.FontColor = Color.FromArgb( 70, 255, 100, 100 );
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 100;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Center;
			text.ZOrder = ZOrder.A_InFront;
			myMaster.GraphObjList.Add( text );

			// Initialize a color and symbol type rotator
			ColorSymbolRotator rotator = new ColorSymbolRotator();

			// Create some new GraphPanes
			for ( int j = 0; j < 5; j++ )
			{
				// Create a new graph - rect dimensions do not matter here, since it
				// will be resized by MasterPane.AutoPaneLayout()
				GraphPane myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
					"Case #" + ( j + 1 ).ToString(),
					"Time, Days",
					"Rate, m/s" );

				// Fill the GraphPane background with a color gradient
				myPane.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPane.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				PointPairList list = new PointPairList();
				for ( int i = 0; i < 36; i++ )
				{
					double x = (double)i + 5;
					double y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
					list.Add( x, y );
				}

				// Add a curve to the Graph, use the next sequential color and symbol
				LineItem myCurve = myPane.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				// Fill the symbols with white to make them opaque
				myCurve.Symbol.Fill = new Fill( Color.White );

				// Add the GraphPane to the MasterPane
				myMaster.Add( myPane );
			}

			using ( Graphics g = this.CreateGraphics() )
			{
				// Tell ZedGraph to auto layout the new GraphPanes
				myMaster.SetLayout( g, PaneLayout.ExplicitRow32 );
			}
			zgc.AxisChange();
		}


		// masterpane test
		private void CreateGraph_MasterPane_Square( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			master.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			master.PaneList.Clear();

			master.Title.IsVisible = true;
			master.Title.Text = "My MasterPane Title";

			master.Margin.All = 10;
			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 4; j++ )
			{
				// Create a new graph with topLeft at (40,40) and size 600x400
				GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"Case #" + ( j + 1 ).ToString(),
					"Time, Days",
					"Rate, m/s" );

				myPaneT.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
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

			using ( Graphics g = this.CreateGraphics() )
			{
				master.SetLayout( g, PaneLayout.SquareRowPreferred );
				//master.IsCommonScaleFactor = true;
				z1.AxisChange();
			}
		}


		// masterpane test
		private void CreateGraph_Junk( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			int j = 5;

			myPane.Title.Text = "Case #6";
			myPane.XAxis.Title.Text = "Time, Days";
			myPane.YAxis.Title.Text = "Rate, m/s";

			myPane.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
			myPane.BaseDimension = 6.0F;

			// Make up some data arrays based on the Sine function
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				x = (double)i + 5;
				y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "Type 5",
				list, Color.Pink, SymbolType.Triangle );
			myCurve.Symbol.Fill = new Fill( Color.White );


			z1.AxisChange();
		}

		public void CreateGraph_Junk3( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Log Book";
			myPane.LineType = LineType.Stack;
			myPane.XAxis.Scale.Max = 24;
			myPane.YAxis.Scale.Max = 4.5;

			string[] labels = { "Off Duty", "Sleeper Berth", "On Duty Driving", "On Duty Not Driving" };

			PointPairList ppl = new PointPairList();
			ppl.Add( 0, 0 );
			ppl.Add( 2, 2 );
			ppl.Add( 9, 4 );
			ppl.Add( 13, 2 );
			ppl.Add( 24, 1 );

			LineItem line = myPane.AddCurve( "Driver1", ppl, Color.Red );

			//line.Line.StepType = StepType.ForwardStep;
			line.Symbol.IsVisible = true;

			//pane.YAxis. = true;
			myPane.YAxis.Scale.TextLabels = labels;
			myPane.YAxis.Type = AxisType.Text;

			//pane.Chart.Fill = new Fill( Color.White, Color.LightSkyBlue, 45.0F );

			z1.AxisChange();
		}

		public void CreateGraph_Junk2( ZedGraphControl z1 )
		{
			DateTime dateTime = new DateTime( 2006, 6, 16 );
			int hours = 1;

			double d1 = new XDate( dateTime - new TimeSpan( hours, 59, 59 ) );
			double d2 = new XDate( dateTime + new TimeSpan( 0, 0, 59 ) );

			//XDate x1 = d1 );
			//XDate x2 = new XDate( d2 );

			GraphPane pane = z1.GraphPane;
			pane.Title.Text = "Log Book";

			pane.LineType = LineType.Stack;
			pane.XAxis.Scale.Max = 24;

			pane.YAxis.Scale.Max = 4.5;
			string[] labels = { "Off Duty", "Sleeper Berth", "On Duty Driving", "On Duty Not Driving" };
			double[] x = { };
			double[] y = { };
			LineItem line = pane.AddCurve( "Driver1", x, y, Color.Red );

			line.Line.StepType = StepType.ForwardStep;

			line.Symbol.IsVisible = false;

			line.AddPoint( 0, 0 );
			line.AddPoint( 2, 2 );
			line.AddPoint( 9, 4 );
			line.AddPoint( 13, 2 );
			line.AddPoint( 24, 1 );
			pane.YAxis.MajorTic.IsBetweenLabels = true;
			pane.YAxis.Scale.TextLabels = labels;
			pane.YAxis.Type = AxisType.Text;
			pane.Chart.Fill = new Fill( Color.White, Color.LightSkyBlue, 45.0F );
			z1.AxisChange();
		}

		private void CreateGraph_Junk4( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			myPane.XAxis.Title.Text = "date time";
			myPane.YAxis.Title.Text = "Rate";
			Random rd = new Random( 1 );
			RollingPointPairList list = new RollingPointPairList( 1200 );
			DateTime mynow2 = DateTime.Now;

			list.Add( mynow2.ToOADate(), 15 );
			list.Add( mynow2.AddSeconds(10).ToOADate(), 25 );
			LineItem myCurve = myPane.AddCurve( "Rate", list, Color.Green, SymbolType.None );

			myPane.Legend.IsVisible = false;
			myPane.Title.IsVisible = false;

			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.XAxis.MajorGrid.Color = Color.Green;
			myPane.XAxis.MajorGrid.PenWidth = 1;

			myPane.XAxis.Scale.FontSpec.Angle = 65;

			myPane.YAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.Color = Color.Green;
			myPane.YAxis.MajorGrid.PenWidth = 1;

			myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
			myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;
			myPane.YAxis.MajorGrid.IsZeroLine = false;
			myPane.YAxis.Scale.Align = AlignP.Inside;
			myPane.YAxis.MajorGrid.IsVisible = true;

			myPane.Chart.Fill = new Fill( Color.Black );
			//timer1.Interval = 500;
			//timer1.Enabled = true;
			//timer1.Start();

			//double gMinTime = new XDate( mynow );
			//double gMaxTime = new XDate( mynow.AddSeconds( 30 ) );
			//double PerSecd = gMaxTime - gMinTime;

			DateTime mynow = DateTime.Now;
			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.Scale.Format = "T";
			myPane.XAxis.Scale.MajorUnit = DateUnit.Second;
			myPane.XAxis.Scale.MinorUnit = DateUnit.Second;
			myPane.XAxis.Scale.Min = mynow.ToOADate();
			myPane.XAxis.Scale.Max = mynow.AddSeconds(30).ToOADate();
			myPane.XAxis.Scale.MinorStep = 1;	// 1 second
			myPane.XAxis.Scale.MajorStep = 5;   // 5 seconds
			//mynow.AddMilliseconds( -mynow.Millisecond );
			mynow.AddSeconds( -5 );
			myPane.XAxis.Scale.BaseTic = mynow.ToOADate();

			z1.AxisChange();

			foreach ( GraphObj obj in myPane.GraphObjList )
			{
				if ( obj is TextObj )
				{
					TextObj text = obj as TextObj;
					text.FontSpec.Size = 12;
					text.FontSpec.IsBold = true;
				}
			}

		}

		public void CreateGraph_junk5( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the title and axis labels 
			myPane.Title.Text = "Japanese Candlestick Chart Demo";
			myPane.XAxis.Title.Text = "Trading Date";
			myPane.YAxis.Title.Text = "Share Price, $US";

			StockPointList spl = new StockPointList();
			Random rand = new Random();

			// First day is jan 1st 
			XDate xDate = new XDate( 2006, 1, 1 );
			double open = 50.0;

			for ( int i = 0; i < 50; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;

				StockPt pt = new StockPt( x, hi, low, open, close, 100000 );
				spl.Add( pt );

				open = close;
				// Advance one day 
				xDate.AddDays( 1.0 );
				// but skip the weekends 
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick( "trades", spl );
			myCurve.Stick.IsAutoSize = true;
			myCurve.Stick.Color = Color.Blue;

			// Use DateAsOrdinal to skip weekend gaps 
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );

			// pretty it up a little 
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			PointPairList ppl = new PointPairList();

			for ( int i = 19; i < spl.Count; i++ )
			{
				double avg = 0.0;
				for ( int j = 0; j < 20; j++ )
					avg += spl.GetAt( i - j ).Close;
				ppl.Add( i + 1, avg / 20.0 );
			}
			LineItem item = myPane.AddCurve( "MA-20", ppl, Color.Red );
			item.IsOverrideOrdinal = true;
			item.Line.Width = 3;
			item.Symbol.Type = SymbolType.None;
			item.Line.IsSmooth = true;

			// Tell ZedGraph to calculate the axis ranges 
			zgc.AxisChange();
			zgc.Invalidate();

		}

		private void CreateGraph_junk6( ZedGraphControl z1 )
		{
			z1.GraphPane.Title.Text = "My Test Graph\n(For CodeProject Sample)";
			z1.GraphPane.XAxis.Title.Text = "My X-Axis";
			z1.GraphPane.YAxis.Title.Text = "My Y-Axis";
			z1.Refresh();
		}

		private void CreateGraph_junk7( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			PointPairList list = new PointPairList();
			double x, y;
			double minX, maxX;

			double curDate = DateTime.Now.ToOADate();
			minX = curDate - 550; //labels are displayed properly 
			//MinX = New XDate(DateAdd(DateInterval.Day, -550, Now)) 'not displayed properly 
			maxX = curDate;

			x = minX;
			for ( int i=1; i<700; i++ )
			{
				x++;
				y = Math.Sin(i * Math.PI / 45.0) * 16.0; 
				list.Add(x, y) ;
			}
 
			LineItem myCurve = myPane.AddCurve("", list, Color.Blue, SymbolType.None);
			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.Scale.Min = minX; 
			myPane.XAxis.Scale.Max = maxX; 
			myPane.XAxis.Scale.MajorUnit = DateUnit.Month; 
			myPane.XAxis.Scale.MinorUnit = DateUnit.Month; 
			myPane.XAxis.Scale.MajorStep = 1.0; 
			myPane.XAxis.Scale.MinorStep = 1.0; 
			myPane.XAxis.Scale.Format = "MMM-yy"; 
			z1.AxisChange(); 
		} 
 
 
		// masterpane with three vertical panes
		private void CreateGraph_ThreeVerticalPanes( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			// Fill the background
			master.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );
			// Clear out the initial GraphPane
			master.PaneList.Clear();

			// Show the masterpane title
			master.Title.IsVisible = true;
			master.Title.Text = "Synchronized Panes Demo";

			// Leave a margin around the masterpane, but only a small gap between panes
			master.Margin.All = 10;
			master.InnerPaneGap = 5;

			// The titles for the individual GraphPanes
			string[] yLabels = { "Rate, m/s", "Pressure, dynes/cm", "Count, units/hr" };

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 3; j++ )
			{
				// Create a new graph -- dimensions to be set later by MasterPane Layout
				GraphPane myPaneT = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
					"",
					"Time, Days",
					yLabels[j] );

				//myPaneT.Fill = new Fill( Color.FromArgb( 230, 230, 255 ) );
				myPaneT.Fill.IsVisible = false;

				// Fill the Chart background
				myPaneT.Chart.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
				// Set the BaseDimension, so fonts are scale a little bigger
				myPaneT.BaseDimension = 3.0F;

				// Hide the XAxis scale and title
				myPaneT.XAxis.Title.IsVisible = false;
				myPaneT.XAxis.Scale.IsVisible = false;
				// Hide the legend, border, and GraphPane title
				myPaneT.Legend.IsVisible = false;
				myPaneT.Border.IsVisible = false;
				myPaneT.Title.IsVisible = false;
				// Get rid of the tics that are outside the chart rect
				myPaneT.XAxis.MajorTic.IsOutside = false;
				myPaneT.XAxis.MinorTic.IsOutside = false;
				// Show the X grids
				myPaneT.XAxis.MajorGrid.IsVisible = true;
				myPaneT.XAxis.MinorGrid.IsVisible = true;
				// Remove all margins
				myPaneT.Margin.All = 0;
				// Except, leave some top margin on the first GraphPane
				if ( j == 0 )
					myPaneT.Margin.Top = 20;
				// And some bottom margin on the last GraphPane
				// Also, show the X title and scale on the last GraphPane only
				if ( j == 2 )
				{
					myPaneT.XAxis.Title.IsVisible = true;
					myPaneT.XAxis.Scale.IsVisible = true;
					myPaneT.Margin.Bottom = 10;
				}

				if ( j > 0 )
					myPaneT.YAxis.Scale.IsSkipLastLabel = true;

				// This sets the minimum amount of space for the left and right side, respectively
				// The reason for this is so that the ChartRect's all end up being the same size.
				myPaneT.YAxis.MinSpace = 80;
				myPaneT.Y2Axis.MinSpace = 20;

				myPaneT.XAxis.Scale.FontSpec.Angle = 90;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i = 0; i < 36; i++ )
				{
					x = (double)i + 5 + j * 3;
					y = ( j + 1 ) * ( j + 1 ) * 10 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
					list.Add( x, y );
				}

				LineItem myCurve = myPaneT.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( myPaneT );
			}

			using ( Graphics g = this.CreateGraphics() )
			{

				master.SetLayout( g, PaneLayout.SingleColumn );
				//master.SetLayout( PaneLayout.ExplicitRow32 );
				//master.SetLayout( 2, 4 );
				//master.SetLayout( false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
				master.AxisChange( g );

				// Synchronize the Axes
				z1.IsAutoScrollRange = true;
				z1.IsShowHScrollBar = true;
				//z1.IsShowVScrollBar = true;
				z1.IsSynchronizeXAxes = true;

				//g.Dispose();
			}

		}

		private void CreateGraph_StackedBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 5; i++ )
			{
				double x = (double)i / 3 + 4;
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

			myCurve.Link.IsEnabled = true;
			myCurve.Link.Target = "_blank";
			myCurve.Link.Title = "Curve 1";
			myCurve.Link.Url = "http://zedgraph.org/wiki/";
			myCurve2.Link.IsEnabled = true;
			myCurve2.Link.Target = "_blank";
			myCurve2.Link.Title = "Curve 2";
			myCurve2.Link.Url = "http://zedgraph.org/wiki/";
			myCurve3.Link.IsEnabled = true;
			myCurve3.Link.Target = "_blank";
			myCurve3.Link.Title = "Curve 3";
			myCurve3.Link.Url = "http://zedgraph.org/wiki/";


			myPane.BarSettings.Type = BarType.Stack;
			myPane.BarSettings.Base = BarBase.X;

			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five", "six" };
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();

			//z1.LinkEvent += new ZedGraphControl.LinkEventHandler(z1_LinkEvent);

			z1.DoubleClickEvent += new ZedGraphControl.ZedMouseEventHandler( z1_DoubleClickEvent );
		}

		private bool z1_DoubleClickEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			ZedGraphControl zg1 = zedGraphControl1;

			float pixValX = e.X;
			double xVal = zg1.GraphPane.XAxis.Scale.ReverseTransform( pixValX );
			float pixValY = e.Y;
			double yVal = zg1.GraphPane.YAxis.Scale.ReverseTransform( pixValY );
			Image poop;
//			int tick = Environment.TickCount;
//			for ( int i = 0; i < 300; i++ )
//			{
				poop = zg1.GraphPane.GetImage( 500, 500, 96 );
//			}

//			MessageBox.Show( "Ticks = " + (Environment.TickCount - tick).ToString() );

			double xVal2 = zg1.GraphPane.XAxis.Scale.ReverseTransform( pixValX );
			double yVal2 = zg1.GraphPane.YAxis.Scale.ReverseTransform( pixValY );

			MessageBox.Show( "x1 = " + xVal.ToString() + "  x2 = " + xVal2.ToString() +
				"   y1 = " + yVal.ToString() + "  y2 = " + yVal2.ToString()  );
			return true;

			if ( sender == zg1 )
			{
				GraphPane myPane = zg1.GraphPane;
				// int x = MousePosition.X;
				// int y = MousePosition.Y;
				PointF point = new PointF( e.X, e.Y );
				CurveItem nearestCurve = null;
				int index = -1;
				object obj = null;
				Graphics grap = this.CreateGraphics();
				myPane.FindNearestObject( point, grap, out obj, out index );

				if ( obj != null && obj is Legend )
				{
					Legend legend = myPane.Legend;
					if ( legend != null )
					{
						if ( index >= 0 )
							nearestCurve = myPane.CurveList[index];
					}
				}
				else if ( obj is CurveItem )
					nearestCurve = (CurveItem)obj;

				if ( nearestCurve != null )
				{
					nearestCurve.IsVisible = false;
					//nearestCurve.Color = Color.Green;
					//if ( nearestCurve is LineItem )
					//	( (LineItem)nearestCurve ).Line.Width = 5;

					zg1.Invalidate();
				}
			}

			return false ;
		}

		bool xz1_DoubleClickEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			PointF pt = new PointF( e.X, e.Y );
			GraphPane pane;
			string thing = "nothing";

			using ( Graphics g = CreateGraphics() )
			{
				object obj;
				int i;

				if ( sender.MasterPane.FindNearestPaneObject( pt, g, out pane, out obj, out i ) )
				{
					if ( obj != null )
						thing = obj.ToString();
				}
				else
				{
					pane = sender.MasterPane.FindPane( pt );
					if ( pane != null )
						thing = "GraphPane";
				}
			}


			MessageBox.Show( "You double clicked a " + thing );

			return false;
		}

		private bool z1_LinkEvent( ZedGraphControl sender, GraphPane pane,
			object source, Link link, int index )
		{
			return false;
		}

		private void CreateGraph_StackedMultiBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList[] list = new PointPairList[7];
			for ( int i = 0; i < 7; i++ )
				list[i] = new PointPairList();

			for ( int i = 0; i < 3; i++ )
			{
				double x = i + 1;
				double y = 1;
				double tot = 7;
				if ( i == 1 )
					tot = 3.4;
				if ( i == 2 )
					tot = 5.6;

				for ( int j = 0; j < 7; j++ )
				{
					y = Math.Max( Math.Min( tot - j, 1 ), 0 );
					list[j].Add( x, y );
				}
			}


			BarItem myCurve1 = myPane.AddBar( "Data 1", list[0], Color.Blue );
			BarItem myCurve2 = myPane.AddBar( "Data 2", list[1], Color.Red );
			BarItem myCurve3 = myPane.AddBar( "Data 3", list[2], Color.Green );
			BarItem myCurve4 = myPane.AddBar( "Data 4", list[3], Color.Orange );
			BarItem myCurve5 = myPane.AddBar( "Data 5", list[4], Color.Gray );
			BarItem myCurve6 = myPane.AddBar( "Data 6", list[5], Color.Fuchsia );
			BarItem myCurve7 = myPane.AddBar( "Data 7", list[6], Color.Navy );
			//myCurve7.IsOverrideOrdinal = true;
			//list[6][0].X = 1.5;

			myPane.BarSettings.Type = BarType.Stack;
			myPane.BarSettings.Base = BarBase.X;

			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			string[] labels = { "one", "two", "three" };
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
		}

		private void CreateGraph_VerticalBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 5; i++ )
			{
				double x = (double)i / 3 + 4;
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
			//myCurve.IsOverrideOrdinal = true;

			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();


		}

		private void CreateGraph_Histogram( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 20; i++ )
			{
				list.Add( (double)i + 1.0, rand.NextDouble() * 100.0 + 20.0 );
			}

			BarItem myBar = myPane.AddBar( "histogram", list, Color.Blue );
			myBar.Bar.Fill = new Fill( Color.Blue );
			myBar.Bar.Border.IsVisible = false;
			myPane.BarSettings.MinClusterGap = 0;

			myPane.XAxis.Scale.Max = 20;

			z1.AxisChange();
		}

		private void CreateGraph_HorizontalBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 5; i++ )
			{
				double y = (double)i;
				double x = rand.NextDouble() * 1000;
				double x2 = rand.NextDouble() * 1000;
				double x3 = rand.NextDouble() * 1000;
				list.Add( x, y );
				list2.Add( x2, y );
				list3.Add( x3, y );
			}

			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90.0f );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			myCurve2.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90.0f );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );
			myCurve3.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90.0f );

			myPane.YAxis.MajorTic.IsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five" };
			myPane.YAxis.Scale.Align = AlignP.Outside;
			myPane.YAxis.Scale.TextLabels = labels;
			//myPane.YAxis.Scale.LabelGap = 2.0f;
			//myPane.YAxis.Title.Gap = 2.0f;
			//myPane.Legend.Gap = 2.0f;
			myPane.YAxis.Type = AxisType.Text;
			myPane.BarSettings.Base = BarBase.Y;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();


		}

		private void CreateGraph_FilteredPointList( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			const int count = 40;
			double[] x = new double[count];
			double[] y = new double[count];
			Random rand = new Random();

			for ( int i = 0; i < count; i++ )
			{
				double val = rand.NextDouble();
				x[i] = (double)i;
				y[i] = x[i] + val * val * val * 10;
			}

			FilteredPointList list = new FilteredPointList( x, y );
			LineItem myCurve = z1.GraphPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.IsVisible = false;

			list.IsApplyHighLowLogic = true;
			list.SetBounds( -1000, 1000, 50 );
			z1.AxisChange();
		}

		private void CreateGraph_GradientByZBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			myPane.Title.Text = "Demonstration of Multi-Colored Bars with a Single BarItem";
			myPane.XAxis.Title.Text = "Bar Number";
			myPane.YAxis.Title.Text = "Value";

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 16; i++ )
			{
				double x = (double)i + 1;
				double y = rand.NextDouble() * 1000;
				double z = i / 4.0;
				list.Add( x, y, z );
			}

			BarItem myCurve = myPane.AddBar( "Multi-Colored Bars", list, Color.Blue );
			Color[] colors = { Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Purple };
			//Color[] colors = { Color.Red, Color.Orange, Color.White, Color.Green };
			//float[] spans = { 0.89999f, 0.1f, 0.00001f, 
			myCurve.Bar.Fill = new Fill( colors );
			myCurve.Bar.Fill.Type = FillType.GradientByZ;

			myCurve.Bar.Fill.RangeMin = 0;
			myCurve.Bar.Fill.RangeMax = 4;
			myCurve.Bar.Fill.SecondaryValueGradientColor = Color.Empty;

			myPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45 );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 225 ), 45 );
			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
		}

		private void CreateGraph_Pie( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			// Set the GraphPane title
			myPane.Title.Text = "2004 ZedGraph Sales by Region\n($M)";
			myPane.Title.FontSpec.IsItalic = true;
			myPane.Title.FontSpec.Size = 24f;
			myPane.Title.FontSpec.Family = "Times New Roman";

			// Fill the pane background with a color gradient
			myPane.Fill = new Fill( Color.White, Color.Goldenrod, 45.0f );
			// No fill for the axis background
			myPane.Chart.Fill.Type = FillType.None;

			// Set the legend to an arbitrary location
			myPane.Legend.Position = LegendPos.Float;
			myPane.Legend.Location = new Location( 0.95f, 0.15f, CoordType.PaneFraction,
								AlignH.Right, AlignV.Top );
			myPane.Legend.FontSpec.Size = 10f;
			myPane.Legend.IsHStack = false;

			// Add some pie slices
			PieItem segment1 = myPane.AddPieSlice( 20, Color.Navy, Color.White, 45f, 0, "North" );
			//segment1.Label = "North";
			segment1.LabelType = PieLabelType.Name_Value_Percent;

			PieItem segment3 = myPane.AddPieSlice( 30, Color.Purple, Color.White, 45f, .0, "East" );
			segment3.LabelType = PieLabelType.Name_Percent;

			PieItem segment4 = myPane.AddPieSlice( 10.21, Color.LimeGreen, Color.White, 45f, 0, "West" );
			segment4.LabelType = PieLabelType.Percent;

			PieItem segment2 = myPane.AddPieSlice( 40, Color.SandyBrown, Color.White, 45f, 0.2, "South" );
			segment2.LabelType = PieLabelType.Value;
			segment2.Link = new Link( "Poop", "http://yahoo.com", "_blank" );

			PieItem segment6 = myPane.AddPieSlice( 250, Color.Red, Color.White, 45f, 0, "Europe" );
			segment6.LabelType = PieLabelType.Name_Value;

			PieItem segment7 = myPane.AddPieSlice( 50, Color.Blue, Color.White, 45f, 0.2, "Pac Rim" );
			segment7.LabelType = PieLabelType.Name;

			PieItem segment8 = myPane.AddPieSlice( 400, Color.Green, Color.White, 45f, 0, "South America" );
			segment8.LabelType = PieLabelType.None;

			PieItem segment9 = myPane.AddPieSlice( 50, Color.Yellow, Color.White, 45f, 0.2, "Africa" );

			segment2.LabelDetail.FontSpec.FontColor = Color.Red;

			// Sum up the pie values																					
			CurveList curves = myPane.CurveList;
			double total = 0;
			for ( int x = 0; x < curves.Count; x++ )
				total += ( (PieItem)curves[x] ).Value;

			// Make a text label to highlight the total value
			TextObj text = new TextObj( "Total 2004 Sales\n" + "$" + total.ToString() + "M",
								0.18F, 0.40F, CoordType.PaneFraction );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill = new Fill( Color.White, Color.FromArgb( 255, 100, 100 ), 45F );
			text.FontSpec.StringAlignment = StringAlignment.Center;
			myPane.GraphObjList.Add( text );

			// Create a drop shadow for the total value text item
			TextObj text2 = new TextObj( text );
			text2.FontSpec.Fill = new Fill( Color.Black );
			text2.Location.X += 0.008f;
			text2.Location.Y += 0.01f;
			myPane.GraphObjList.Add( text2 );

			z1.LinkEvent += new ZedGraphControl.LinkEventHandler(z1_LinkEvent);

			z1.AxisChange();
		}

		private void CreateGraph_PolyTest( ZedGraphControl z1 )
		{
			GraphPane junk = z1.GraphPane.Clone();


			// Get a reference to the GraphPane instance in the ZedGraphControl
			GraphPane myPane = z1.GraphPane;

			PointD[] corners = new PointD[4];
			corners[0] = new PointD( 300.0f, 85.0f );
			corners[1] = new PointD( 400.0f, 85.0f );
			corners[2] = new PointD( 400.0f, 95.0f );
			corners[3] = new PointD( 300.0f, 95.0f );
			PolyObj poly1 = new PolyObj( corners, Color.Empty, Color.Red );

			PointD[] corners3 = new PointD[3];
			corners3[0] = new PointD( 300.0f, 88.0f );
			corners3[1] = new PointD( 375.0f, 95.0f );
			corners3[2] = new PointD( 300.0f, 95.0f );
			PolyObj poly3 = new PolyObj( corners3, Color.Empty, Color.LightGreen );

			PointD[] corners2 = new PointD[3];
			corners2[0] = new PointD( 333.0f, 85.0f );
			corners2[1] = new PointD( 400.0f, 91.0f );
			corners2[2] = new PointD( 400.0f, 85.0f );
			PolyObj poly2 = new PolyObj( corners2, Color.Empty, Color.Cyan );

			myPane.GraphObjList.Add( poly3 );
			myPane.GraphObjList.Add( poly2 );
			myPane.GraphObjList.Add( poly1 );

			myPane.XAxis.Scale.Min = 250;
			myPane.XAxis.Scale.Max = 450;
			myPane.YAxis.Scale.Min = 80;
			myPane.YAxis.Scale.Max = 100;

			z1.AxisChange();
			z1.Refresh();

			Serialize( z1, "junk.bin" );
			z1.MasterPane.PaneList.Clear();

			DeSerialize( z1, "junk.bin" );
		}

		private void CreateGraph_DualYDemo( ZedGraphControl z1 )
		{

			// Get a reference to the GraphPane instance in the ZedGraphControl
			GraphPane myPane = z1.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Demonstration of Dual Y Graph";
			myPane.XAxis.Title.Text = "Time, Days";
			myPane.YAxis.Title.Text = "Parameter A";
			myPane.Y2Axis.Title.Text = "Parameter B";

			// Make up some data _points based on the Sine function
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			PointPairList list4 = new PointPairList();
			for ( int i = 0; i < 100; i++ )
			{
				double x = (double)i * 5.0;
				double y = Math.Sin( (double)i * Math.PI / 15.0 ) * 16.0;
				double y2 = Math.Sin( (double)i * Math.PI / 15.0 + Math.PI * 0.5 ) * 0.01;
				double y3 = Math.Sin( (double)i * Math.PI / 15.0 + Math.PI ) * 16.0;
				double y4 = Math.Sin( (double)i * Math.PI / 15.0 + Math.PI * 1.5 ) * 16.0;
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
				list4.Add( x, y4 );
			}

			// Generate a red curve with diamond symbols, and "Alpha" in the _legend
			LineItem myCurve = myPane.AddCurve( "Alpha", list, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with circle symbols, and "Beta" in the _legend
			myCurve = myPane.AddCurve( "Beta", list2, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;

			// Generate a blue curve with circle symbols, and "Beta" in the _legend
		//	myCurve = myPane.AddCurve( "Gamma", list3, Color.Green, SymbolType.Square );
		//	myCurve = myPane.AddCurve( "Delta", list4, Color.Violet, SymbolType.Triangle );

			myCurve.Line.Style = DashStyle.DashDotDot;
			//myCurve.Line.

			// Show the x axis grid
			myPane.XAxis.MajorGrid.IsVisible = true;

			// Make the Y axis scale red
			myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
			myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;
			// Don't display the Y zero line
			myPane.YAxis.MajorGrid.IsZeroLine = false;
			// Align the Y axis labels so they are flush to the axis
			myPane.YAxis.Scale.Align = AlignP.Inside;
			// Manually set the axis range
		//	myPane.YAxis.Scale.Min = -30;
		//	myPane.YAxis.Scale.Max = 30;
			// Enable the Y2 axis display
			myPane.Y2Axis.IsVisible = true;
			// Make the Y2 axis scale blue
			myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			myPane.Y2Axis.MajorTic.IsOpposite = false;
			myPane.Y2Axis.MinorTic.IsOpposite = false;
			// Display the Y2 axis grid lines
			myPane.Y2Axis.MajorGrid.IsVisible = true;
			// Align the Y2 axis labels so they are flush to the axis
			myPane.Y2Axis.Scale.Align = AlignP.Inside;

			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGray, 45.0f );

			//myPane.CurveList.Move( 0, 3 );

			myPane.XAxis.Scale.IsReverse = true;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			// Make sure the Graph gets redrawn
			z1.Invalidate();

			// Enable scrollbars if needed
			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;
			z1.IsAutoScrollRange = true;

			// Show tooltips when the mouse hovers over a point
			z1.IsShowPointValues = true;
			z1.PointValueFormat = "f2";
			//z1.PointValueEvent += new ZedGraphControl.PointValueHandler( MyPointValueHandler );

			// Add a custom context menu item
			z1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler( MyContextMenuBuilder );

			//z1.ScrollEvent += new ZedGraph.ZedGraphControl.ScrollEventHandler( zedGraphControl1_ScrollEvent );
		}

		public void MyContextMenuBuilder( ZedGraphControl sender,
			ContextMenuStrip menuStrip, Point mousePt )
		{
			foreach ( ToolStripMenuItem item in menuStrip.Items )
			{
				if ( (string)item.Tag == "set_default" )
				{
					// remove the menu item
					menuStrip.Items.Remove( item );

					break;
				}
			}
		}


		private void CreateGraph_ClusteredStackBar( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList ppla1 = new PointPairList();
			PointPairList pplb1 = new PointPairList();
			PointPairList pplc1 = new PointPairList();
			PointPairList ppla2 = new PointPairList();
			PointPairList pplb2 = new PointPairList();
			PointPairList pplc2 = new PointPairList();

			double[] a1 = { 10, 15, 20, 25 };
			double[] b1 = { 15, 30, 12, 8 };
			double[] c1 = { 25, 5, 35, 10 };

			double[] a2 = { 25, 40, 5, 18 };
			double[] b2 = { 15, 15, 25, 20 };
			double[] c2 = { 5, 25, 10, 25 };

			double shift = 0.15;
			for ( int i = 0; i < 4; i++ )
			{
				ppla1.Add( i + 1 - shift, a1[i], 0 );
				pplb1.Add( i + 1 - shift, a1[i] + b1[i], a1[i] );
				pplc1.Add( i + 1 - shift, a1[i] + b1[i] + c1[i], a1[i] + b1[i] );

				ppla2.Add( i + 1 + shift, a2[i], 0 );
				pplb2.Add( i + 1 + shift, a2[i] + b2[i], a2[i] );
				pplc2.Add( i + 1 + shift, a2[i] + b2[i] + c2[i], a2[i] + b2[i] );
			}

			myPane.AddHiLowBar( "2004 New", ppla1, Color.Red );
			myPane.AddHiLowBar( "2004 Return", pplb1, Color.Green );
			myPane.AddHiLowBar( "2004 Refer", pplc1, Color.Blue );
			myPane.AddHiLowBar( "2005 New", ppla2, Color.Yellow );
			myPane.AddHiLowBar( "2005 Return", pplb2, Color.Cyan );
			myPane.AddHiLowBar( "2005 Refer", pplc2, Color.Magenta );

			foreach ( HiLowBarItem curve in myPane.CurveList )
			{
				curve.IsOverrideOrdinal = true;
				curve.Bar.Size = 30;
			}

			string[] labels = { "Q1", "Q2", "Q3", "Q4" };
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.Legend.Position = LegendPos.Right;

			z1.AxisChange();
			z1.Invalidate();
		}

		private void CreateGraph_GrowingData( ZedGraphControl z1 )
		{
			/*
			myTimer = new Timer();
			myTimer.Enabled = true;
			myTimer.Tick += new EventHandler( myTimer_Tick );
			myTimer.Interval = 100;
			myTimer.Start();
			*/

			GraphPane myPane = z1.GraphPane;

			PointPairList list1 = new PointPairList();
			//list1.Add( startTime, Vin );

			LineItem curve = z1.GraphPane.AddCurve( "Vin", list1, Color.Red, SymbolType.None );
			curve.Line.Width = 1.5F;
			//myPane.XAxis.MajorGrid.IsVisible = true;
			//myPane.YAxis.MajorGrid.IsVisible = true;
			myPane.Title.Text = "Input Voltage";
			myPane.XAxis.Title.Text = "Time (Now)";
			myPane.YAxis.Title.Text = "Voltage";
			//z1.GraphPane.XAxis.Max = tick + totalticks;
			//z1.GraphPane.XAxis.Min = tick;
			//myPane.YAxis.Scale.Max = 250;
			//myPane.YAxis.Scale.Min = -250;
			//Time(Now)
			//myPane.XAxis.Scale.MajorStep = 2.0;
			myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
			// Set the minor step to 1 hour
			//myPane.XAxis.Scale.MinorStep = 1.0;
			myPane.XAxis.Scale.MinorUnit = DateUnit.Minute;
			myPane.XAxis.Scale.Format = "HH:mm:ss";
			myPane.XAxis.Type = AxisType.Date;

			z1.AxisChange();
			z1.Invalidate();
		}

		private void CreateGraph_SpiderPlot( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			RadarPointList rpl = new RadarPointList();

			Random rand = new Random();

			for ( int i = 0; i < 7; i++ )
			{
				double r = rand.NextDouble() * 10.0 + 1.0;
				PointPair pt = new PointPair( PointPair.Missing, r, "r = " + r.ToString( "f1" ) );
				rpl.Add( pt );
			}
			LineItem curve = myPane.AddCurve( "test", rpl, Color.Green, SymbolType.Default );

			// Add the spokes as GraphItems 
			for ( int i = 0; i < 7; i++ )
			{
				ArrowObj arrow = new ArrowObj( 0, 0, (float)rpl[i].X, (float)rpl[i].Y );
				arrow.IsArrowHead = false;
				arrow.Color = Color.LightGray;
				arrow.ZOrder = ZOrder.D_BehindCurves;
				myPane.GraphObjList.Add( arrow );
			}

			myPane.XAxis.MajorGrid.IsZeroLine = true;
			myPane.XAxis.MajorTic.IsAllTics = true;
			myPane.XAxis.Title.IsTitleAtCross = false;
			myPane.XAxis.Cross = 0;
			myPane.XAxis.Scale.IsSkipFirstLabel = true;
			myPane.XAxis.Scale.IsSkipLastLabel = true;
			myPane.XAxis.Scale.IsSkipCrossLabel = true;

			myPane.YAxis.MajorTic.IsAllTics = true;
			myPane.YAxis.Title.IsTitleAtCross = false;
			myPane.YAxis.Cross = 0;
			myPane.YAxis.Scale.IsSkipFirstLabel = true;
			myPane.YAxis.Scale.IsSkipLastLabel = true;
			myPane.YAxis.Scale.IsSkipCrossLabel = true;
		}

		private void CreateGraph_SplineTest( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			PointPairList ppl = new PointPairList();

			ppl.Add( 4000, 150 );
			ppl.Add( 7360, 333 );
			ppl.Add( 10333.333, 45.333336 );
			ppl.Add( 11666.667, 5 );
			ppl.Add( 12483.333, 45.333336 );
			ppl.Add( 13600, 110 );
			ppl.Add( 15800, 184.66667 );
			//			ppl.Add( 18000, 187.5 );
			ppl.Add( 18644.998, 186.33368 );
			//ppl.Add( 18770.002, 186.66664 );
			//ppl.Add( 18896.666, 187.08336 );
			//ppl.Add( 18993.334, 187.50002 );
			ppl.Add( 19098.332, 188.08334 );
			//ppl.Add( 19285.002, 189.41634 );
			//ppl.Add( 19443.332, 190.83334 );
			ppl.Add( 19633.334, 193.16634 );
			//ppl.Add( 19823.336, 196.49983 );
			//ppl.Add( 19940.002, 199.16669 );
			//ppl.Add( 20143.303, 204.66566 );
			ppl.Add( 20350, 210.91667 );
			//			ppl.Add( 21000, 232 );
			//			ppl.Add( 23000, 296 );
			ppl.Add( 24000, 100 );

			//double y1 = ppl.SplineInterpolateX( 18000, 0.2 );
			//double y2 = ppl.SplineInterpolateX( 21000, 0.2 );
			//double y3 = ppl.SplineInterpolateX( 23000, 0.2 );
			//ppl.Add( 18000, y1 );
			//ppl.Add( 21000, y2 );
			//ppl.Add( 23000, y3 );
			//ppl.Sort();

			LineItem curve = myPane.AddCurve( "test", ppl, Color.Green, SymbolType.Default );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.5F;

			PointPairList ppl2 = new PointPairList();
			for ( double x = 4100; x < 24000; x += 100 )
			{
				double y = ppl.SplineInterpolateX( x, 0.5 );
				ppl2.Add( x, y );
			}

			LineItem curve2 = myPane.AddCurve( "interp", ppl2, Color.Red, SymbolType.Circle );

			z1.ZoomButtons2 = MouseButtons.Left;
			z1.ZoomModifierKeys2 = Keys.Control;

			z1.MouseDownEvent += new ZedGraphControl.ZedMouseEventHandler( Spline_MouseDownEvent );
			z1.MouseUpEvent += new ZedGraphControl.ZedMouseEventHandler( Spline_MouseUpEvent );
		}

		private bool Spline_MouseDownEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			if ( Control.ModifierKeys == Keys.Control )
				sender.IsEnableHZoom = false;
			return false;
		}

		private bool Spline_MouseUpEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			sender.IsEnableHZoom = true;
			return false;
		}


		// Basic curve test - Date Axis
		private void CreateGraph_DateAxis( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();

			for ( int i = 0; i < 50; i++ )
			{
				double x = new XDate( 2005, 12 + i, 15 );
				double y = Math.Sin( i / 8.0 ) * 1000 + 1001;
				list.Add( x, y, 1500 );
				list2.Add( x, y * 1.2, 1500 );
			}

			LineItem line = myPane.AddCurve( "Line", list, Color.Blue, SymbolType.Diamond );
			//myPane.XAxis.Scale.Format = "MMM\nyyyy";
			myPane.XAxis.Type = AxisType.Date;

			//myPane.XAxis.Scale.MajorStep = 1;
			//myPane.XAxis.Scale.MajorUnit = DateUnit.Year;
			myPane.XAxis.Scale.MinorStep = 1;
			myPane.XAxis.Scale.MinorUnit = DateUnit.Month;

			/*
			BarItem myBar = myPane.AddBar( "Bar", list, Color.Blue );
			BarItem myBar2 = myPane.AddBar( "Bar2", list2, Color.Green );
			//LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myPane.BarSettings.Type = BarType.ClusterHiLow;
			myPane.BarSettings.ClusterScaleWidth = 20.0;
			*/

			z1.IsAutoScrollRange = true;
			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;

			XDate now = new XDate( DateTime.Now );
			ArrowObj arrow = new ArrowObj( Color.Black,
					2.0f, (float)now.XLDate, 0.0f, (float)now.XLDate, 1.0f );
			arrow.IsArrowHead = false;
			arrow.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
			arrow.IsClippedToChartRect = true;
			myPane.GraphObjList.Add( arrow );

			// Make the first year line
			double xDate = new XDate( 2006, 1, 1 ).XLDate;
			LineObj myLine = new LineObj();
			myLine.Location.X1 = xDate;
			myLine.Location.Y1 = 0.0;
			myLine.Location.Width = 0.0;
			myLine.Location.Height = 1.0;
			myLine.IsClippedToChartRect = true;
			myLine.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
			myLine.PenWidth = 2.0f;
			myLine.Color = Color.Red;
			myPane.GraphObjList.Add( myLine );

			// Repeat for each Grid by cloning
			xDate = new XDate( 2007, 1, 1 ).XLDate; ;
			myLine = new LineObj( myLine );
			myLine.Location.X1 = xDate;
			myPane.GraphObjList.Add( myLine );

		}

		private void CreateGraph_DateAxisTutorial( ZedGraphControl z1 )
		{
			// Get a reference to the GraphPane
			GraphPane myPane = z1.GraphPane;

			// Set the titles
			myPane.Title.Text = "My Test Date Graph";
			myPane.XAxis.Title.Text = "Date";
			myPane.XAxis.Title.Text = "My Y Axis";

			// Make up some random data points
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				x = (double)new XDate( 1995, 5, i + 11 );
				y = Math.Sin( (double)i * Math.PI / 15.0 );
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
					list, Color.Red, SymbolType.Diamond );

			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.Scale.Min = new XDate( 1995, 5, 20 );
			myPane.XAxis.Scale.Max = new XDate( 1995, 6, 10 ); 

			// Tell ZedGraph to refigure the axes since the data 
			// have changed
			z1.AxisChange();
		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinearScroll( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i = 1; i < 500; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			z1.IsShowHScrollBar = true;
			//z1.ScrollMinX = 0;
			//z1.ScrollMaxX = 550;
			//z1.IsShowVScrollBar = true;
			//z1.IsAutoScrollRange = true;

			//z1.GraphPane.IsBoundedRanges = false;
			//z1.GraphPane.XAxis.Scale.Min = 450;
			//z1.GraphPane.XAxis.Scale.MajorStep = 10;
			//z1.GraphPane.XAxis.Scale.Max = 550;

			z1.AxisChange();
			//z1.GraphPane.XAxis.IsReverse = true;
			z1.GraphPane.XAxis.Type = AxisType.Date;
			//z1.IsAutoScrollRange = true;
			//z1.ScrollMinX = 1;
			//z1.ScrollMaxX = 100;
			//z1.IsShowHScrollBar = true;
			//z1.IsEnableVZoom = false;
		}

		// Basic stick test - Linear Axis
		private void CreateGraph_BasicStick( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i = 1; i < 500; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
			}

			StickItem myCurve = myPane.AddStick( "curve", list, Color.Blue );

			z1.IsShowHScrollBar = true;
			z1.ScrollMinX = 0;
			z1.ScrollMaxX = 550;
			//z1.IsShowVScrollBar = true;
			//z1.IsAutoScrollRange = true;

			//z1.GraphPane.IsBoundedRanges = false;
			z1.GraphPane.XAxis.Scale.Min = 450;
			z1.GraphPane.XAxis.Scale.MajorStep = 10;
			z1.GraphPane.XAxis.Scale.Max = 550;

			z1.AxisChange();
			//z1.GraphPane.XAxis.IsReverse = true;
			//z1.GraphPane.XAxis.Type = AxisType.Log;
			//z1.IsAutoScrollRange = true;
			//z1.ScrollMinX = 1;
			//z1.ScrollMaxX = 100;
			//z1.IsShowHScrollBar = true;
			//z1.IsEnableVZoom = false;
		}

		private void CreateGraph_ScrollTest( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			z1.IsAutoScrollRange = true;
			z1.IsEnableHPan = false;
			z1.IsEnableHZoom = true;
			z1.IsEnableVPan = false;
			z1.IsEnableVZoom = false;
			z1.IsScrollY2 = false;
			z1.IsShowContextMenu = true;
			z1.IsShowCursorValues = false;
			z1.IsShowHScrollBar = true;
			z1.IsShowPointValues = false;
			z1.IsShowVScrollBar = false;
			z1.IsZoomOnMouseCenter = false;
			z1.Location = new System.Drawing.Point( 3, 18 );
			z1.Name = "countGraph";
			z1.PanButtons = System.Windows.Forms.MouseButtons.Left;
			z1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			z1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			z1.PointDateFormat = "g";
			z1.PointValueFormat = "G";
			z1.ScrollMaxX = 0;
			z1.ScrollMaxY = 0;
			z1.ScrollMaxY2 = 0;
			z1.ScrollMinX = 0;
			z1.ScrollMinY = 0;
			z1.ScrollMinY2 = 0;
			z1.Size = new System.Drawing.Size( 559, 350 );
			z1.TabIndex = 0;
			z1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			z1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			z1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			z1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			z1.ZoomStepFraction = 0.1;

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
			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.BarSettings.MinClusterGap = 2;
			z1.AxisChange();

			//this.Refresh();
		}

		private void CreateGraph_ScrollProblem( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;
			zgc.IsShowHScrollBar = true;
			zgc.IsShowVScrollBar = true;
			zgc.IsScrollY2 = true;
			zgc.IsAutoScrollRange = true;

			YAxis y3axis = new YAxis( "Y3" );
			myPane.YAxisList.Add( y3axis );
			myPane.Y2Axis.Title.Text = "Y2";
			myPane.Y2Axis.IsVisible = true;
			zgc.YScrollRangeList.Add( new ScrollRange( true ) );
			//ScrollRange sl = zgc.YScrollRangeList[1];
			//sl.IsScrollable = true;
			//zgc.YScrollRangeList[1] = sl;
			//zgc.YScrollRangeList[1].IsScrollable = true;

			
			myPane.XAxis.Scale.Min = 0;
			myPane.YAxis.Scale.Min = 0;
			myPane.Y2Axis.Scale.Min = 0;
			y3axis.Scale.Min = 0;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			/*
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i * 5.0;
				double y = Math.Abs( Math.Sin( (double)i * Math.PI / 15.0 ) * 16.0 );
				double y2 = Math.Abs( y + 10 );
				double y3 = Math.Abs( y * 13.5 );
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
			} 
			*/
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i * 5.0;
				double y = Math.Sin( (double)i * Math.PI / 15.0 ) * 16.0;
				double y2 = x * 13.5;
				double y3 = Math.Log( 100 * x );
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
			}
			
			LineItem myCurve = myPane.AddCurve( "Alpha", list, Color.Red, SymbolType.Diamond );
			myCurve = myPane.AddCurve( "Beta", list2, Color.Blue, SymbolType.Plus );
			myCurve.IsY2Axis = true;
			myCurve = myPane.AddCurve( "Sigma", list3, Color.Green, SymbolType.XCross );
			myCurve.YAxisIndex = 1;
			zgc.AxisChange();

			zgc.ScrollProgressEvent += new ZedGraphControl.ScrollProgressHandler( zgc_ScrollProgressEvent );
			zgc.ScrollDoneEvent += new ZedGraphControl.ScrollDoneHandler( zgc_ScrollDoneEvent );
			zgc.Scroll += new ScrollEventHandler( zgc_Scroll );
		}

		void zgc_Scroll( object sender, ScrollEventArgs e )
		{
		}

		void zgc_ScrollDoneEvent( ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState )
		{
		}

		void zgc_ScrollProgressEvent( ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState )
		{
		}

		// Basic curve test - two text axes
		private void CreateGraph_TwoTextAxes( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			double[] y = { 2, 4, 1, 5, 3 };

			LineItem myCurve = myPane.AddCurve( "curve 1", null, y, Color.Blue, SymbolType.Diamond );
			myCurve.IsOverrideOrdinal = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.YAxis.Type = AxisType.Text;

			//string[] xLabels = { "one", "two", "three", "four", "five" };
			string[] yLabels = { "alpha", "bravo", "charlie", "delta", "echo" };
			string[] xLabels = { "one", "two" };
			myPane.XAxis.Scale.Max = 5;
			myPane.XAxis.Scale.TextLabels = xLabels;
			myPane.YAxis.Scale.TextLabels = yLabels;

			z1.AxisChange();

			z1.IsShowPointValues = true;
		}

		// Basic curve test - large dataset with NoDupePointList
		private void CreateGraph_NoDupePointList( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			NoDupePointList list = new NoDupePointList();
			Random rand = new Random();

			for ( int i = 0; i < 100000; i++ )
			{
				double x = rand.NextDouble() * 1000;
				double y = rand.NextDouble() * 1000;

				list.Add( x, y );
			}

			LineItem myCurve = z1.GraphPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.IsVisible = false;

			z1.AxisChange();
			list.FilterMode = 3;
			list.FilterData( myPane, myPane.YAxis );

			MessageBox.Show( list.Count.ToString() );
			int count = list.Count;
		}

		// Basic curve test - 32000 points
		private void CreateGraph_32kPoints( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 100000; i++ )
			{
				double val = rand.NextDouble();
				double x = (double)i;
				double y = x + val * val * val * 10;

				list.Add( x, y );
			}

			LineItem myCurve = z1.GraphPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.IsVisible = false;
			z1.IsShowCopyMessage = false;

			z1.AxisChange();
		}

		private void CreateGraph_OverlayBarDemo( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			// Set the title and axis label
			myPane.Title.Text = "Overlay Bar Graph Demo";
			myPane.YAxis.Title.Text = "Value";

			// Enter some data values
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			// Manually sum up the curves
			for ( int i = 0; i < y.GetLength( 0 ); i++ )
				y2[i] += y[i];
			for ( int i = 0; i < y2.GetLength( 0 ); i++ )
				y3[i] += y2[i];


			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.MajorTic.IsBetweenLabels = true;

			// Set the XAxis to the ordinal type
			myPane.XAxis.Type = AxisType.Ordinal;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;

			for ( int i = 0; i < y.Length; i++ )
			{
				// format the label string to have 1 decimal place
				string lab = y3[i].ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextObj text = new TextObj( lab, (float)( i + 1 ), (float)( y3[i] < 0 ? 0.0 : y3[i] ) + shift );
				// tell Zedgraph to use user scale units for locating the TextObj
				text.Location.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.Location.AlignH = AlignH.Left;
				text.Location.AlignV = AlignV.Center;
				text.FontSpec.Border.IsVisible = false;
				text.FontSpec.Fill.IsVisible = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 90;
				// add the TextObj to the list
				myPane.GraphObjList.Add( text );
			}

			// Indicate that the bars are overlay type, which are drawn on top of eachother
			myPane.BarSettings.Type = BarType.Overlay;

			// Fill the axis background with a color gradientC:\Documents and Settings\champioj\Desktop\ZedGraph-4.9-CVS\demo\ZedGraph.Demo\StepChartDemo.cs
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0F );

			z1.AxisChange();

			// Add one step to the max scale value to leave room for the labels
			myPane.YAxis.Scale.Max += myPane.YAxis.Scale.MajorStep;
		}

		private void CreateGraph_HiLowBarDemo( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Hi-Low Bar Graph Demo";
			myPane.XAxis.Title.Text = "Event";
			myPane.YAxis.Title.Text = "Range of Values";

			// Make up some data points based on the Sine function
			PointPairList list = new PointPairList();
			for ( int i = 1; i < 45; i++ )
			{
				double y = Math.Sin( (double)i * Math.PI / 15.0 );
				double yBase = y - 0.4;
				list.Add( (double)i * 100, y, yBase );
			}

			// Generate a red bar with "Curve 1" in the legend
			HiLowBarItem myCurve = myPane.AddHiLowBar( "Curve 1", list, Color.Red );
			// Fill the bar with a red-white-red gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
			// Make the bar width based on the available space, rather than a size in points
			myCurve.Bar.IsAutoSize = true;
			myPane.BarSettings.ClusterScaleWidthAuto = true;
			//myPane.XAxis.Type = AxisType.Ordinal;

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166 ), 45.0F );

			z1.AxisChange();
		}

		// Basic curve test - text
		private void CreateGraph_TextBasic( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();
			string[] labels = new string[10];

			for ( int i = 0; i < 10; i++ )
			{
				double val = rand.NextDouble();
				double x = (double)i;
				double y = x + val * val * val * 10;

				list.Add( x, y );

				labels[i] = "Text " + ( i + 1 ).ToString();
			}

			LineItem myCurve = z1.GraphPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.IsVisible = false;
			z1.IsShowCopyMessage = false;
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.MajorGrid.IsVisible = true;

			z1.AxisChange();
		}

		//using ZedGraph.ControlTest.DataSet2TableAdapters;

		private void CreateGraph_DataSource( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "DataSourcePointList Test";
			myPane.XAxis.Title.Text = "Date";
			myPane.YAxis.Title.Text = "Freight Charges ($US)";

			// Create a new DataSourcePointList to handle the database connection
			DataSourcePointList dspl = new DataSourcePointList();
			// Create a TableAdapter instance to access the database
			ZedGraph.ControlTest.NorthwindDataSetTableAdapters.OrdersTableAdapter adapter =
						new ZedGraph.ControlTest.NorthwindDataSetTableAdapters.OrdersTableAdapter();
			// Create a DataTable and fill it with data from the database
			ZedGraph.ControlTest.NorthwindDataSet.OrdersDataTable table = adapter.GetData();

			//NorthwindDataSet ds = ZedGraph.ControlTest.NorthwindDataSet;
			//DataTable dt = ds.Tables["Orders"];
			//dt.Select( "SELECT *" );

			//DataTable dt = ds.Tables[
			//dt.

			//string strConn = "Provider=SQLOLEDB;Data Source=(local)\\NetSDK;" +
			//		"Initial Catalog=Northwind;Trusted_Connection=Yes;";
			//string strSQL = "SELECT CustomerID, CompanyName, ContactName, Phone " +
			//		"FROM Customers";
			//OleDbDataAdapter da = new OleDbDataAdapter( strSQL, strConn );
			//DataSet ds = new DataSet();
			//da.Fill( ds, "Customers" );

//			string cn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = Northwind.mdb;Persist Security Info=False";
//			string sql = "SELECT * FROM Orders";
//			OleDbConnection con = new OleDbConnection( cn );
//			OleDbDataAdapter adapt = new OleDbDataAdapter();
//			adapt.SelectCommand = new OleDbCommand( sql, con );
			
//			DataSet ds = new DataSet(); ;
			//DataTable dt = new DataTable();

//			adapt.Fill( ds, "Orders" );
			
			// Specify the table as the data source
			dspl.DataSource = table;
			// The X data will come from the "OrderDate" column
			dspl.XDataMember = "OrderDate";
			// The Y data will come from the "Freight" column
			dspl.YDataMember = "Freight";
			// The Z data are not used
			dspl.ZDataMember = null;
			// The Tag data will come from the "ShipName" column
			// (note that this will just set PointPair.Tag = ShipName)
			dspl.TagDataMember = "ShipName";

			//List<Sample> sampleList = new List<Sample>();

			//ArrayList arrayList = new ArrayList();
			//DateTime dt = new DateTime( 2006, 3, 1 );
			//for ( int i = 0; i < 50; i++ )
			//{
			//	double x = i;
			//	double y = Math.Sin( i / 5 );
			//	Sample sample = new Sample();
			//	sample.Time = dt.AddDays( i );
			//	sample.Position = x;
			//	sample.Velocity = y;
			//arrayList.Add( sample );
			//	sampleList.Add( sample );
			//}

			//dspl.DataSource = sampleList;
			//dspl.XDataMember = "Time";
			//dspl.YDataMember = "Position";
			//dspl.ZDataMember = "Velocity";
			//dspl.TagDataMember = null;



			//int count = table.Count;

			// X axis will be dates
			z1.GraphPane.XAxis.Type = AxisType.Date;

			// Make a curve
			LineItem myCurve = z1.GraphPane.AddCurve( "Orders", dspl, Color.Blue );
			// Turn off the line so it's a scatter plot
			myCurve.Line.IsVisible = false;

			// Show the point values.  These are derived from the "ShipName" database column,
			// which is set as the "Tag" property.
			z1.IsShowPointValues = true;

			// Auto set the scale ranges
			z1.AxisChange();
		}

		private void CreateGraph_BarJunk( ZedGraphControl z1 )
		{
			GraphPane grPane = z1.GraphPane;

			grPane.Legend.IsVisible = false;

			PointPairList list = new PointPairList();
			list.Add( 10, 10, 1 );
			list.Add( 20, 20, 1 );
			list.Add( 30, 15, 1 );
			list.Add( 40, 35, 2 );
			list.Add( 50, 22, 1 );
			list.Add( 100, 8, 1 );
			list.Add( 150, 12, 1 );
			list.Add( 200, 29, 1 );

			BarItem myCurve = grPane.AddBar( "My Curve", list, Color.Blue );

			myCurve.Bar.Fill = new Fill( Color.Blue, Color.Red );
			myCurve.Bar.Fill.RangeMin = 1;
			myCurve.Bar.Fill.RangeMax = 2;
			myCurve.Bar.Fill.RangeDefault = 1;
			myCurve.Bar.Fill.Type = FillType.GradientByZ;
			myCurve.Bar.Fill.SecondaryValueGradientColor = Color.White;

			grPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166 ), 45.0F );

			grPane.XAxis.Type = AxisType.LinearAsOrdinal;
			z1.AxisChange();
		}

		private void CreateGraph_BarJunk2( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			myPane.CurveList.Clear();
			PointPairList list = new PointPairList();

			list.Clear();
			list.Add( 4, 0 );
			list.Add( -2, 1 );
			list.Add( 5, 2 );
			list.Add( 3, 3 );
			string[] labels = { "bar one", "bar two", "bar three", "bar four" };
			myPane.YAxis.Scale.TextLabels = labels;
			myPane.YAxis.Type = AxisType.Text;

			//myPane.YAxis.Cross = 0.0;
			myPane.XAxis.MajorGrid.IsZeroLine = true;

			BarItem myCurve2 = myPane.AddBar( "curve 2", list, Color.Red );
			myCurve2.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90 );

			// Set BarBase to the YAxis for horizontal bars 
			myPane.BarSettings.Base = BarBase.Y;

			z1.AxisChange();
			z1.Refresh();
		}

		public void CreateGraph_Contour( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "Sample Contour Plot";
			myPane.XAxis.Title.Text = "X Coordinate (m)";
			myPane.YAxis.Title.Text = "Y Coordinate (m)";

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
						new Fill( Color.White, Color.FromArgb( 255, 255, 150 ), 45.0f );
			( myPane.CurveList[2] as LineItem ).Line.Fill =
						new Fill( Color.White, Color.FromArgb( 150, 255, 150 ), 45.0f );
			( myPane.CurveList[3] as LineItem ).Line.Fill =
						new Fill( Color.White, Color.FromArgb( 150, 255, 255 ), 45.0f );

			//myPane.Legend.IsVisible = false;
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGray, 45.0f );
			//myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );
			myPane.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );

			// Manually set the axis ranges
			myPane.XAxis.Scale.Min = 150;
			myPane.XAxis.Scale.Max = 350;
			myPane.YAxis.Scale.Min = 150;
			myPane.YAxis.Scale.Max = 350;


			BarItem myBar = myPane.AddBar( "bar", myPane.CurveList[0].Points, Color.Green );

			z1.AxisChange();
		}

		private void CreateGraph_StickToCurve( ZedGraphControl z1 )
		{
			PointPairList listCurve = new PointPairList();
			PointPairList listPts = new PointPairList();

			Random rand = new Random();
			double val = 155.0;
			XDate date = new XDate( 2005, 7, 1 );

			for ( int iDay = 0; iDay < 60; iDay++ )
			{
				double dv = rand.NextDouble() * 3 - 1.5;
				listCurve.Add( date, val );
				listPts.Add( date, val + dv, val );

				val += rand.NextDouble() * 0.4 - 0.3;
				date.AddDays( 1 );
			}

			GraphPane myPane = z1.GraphPane;
			myPane.XAxis.Type = AxisType.Date;

			myPane.AddCurve( "val", listCurve, Color.Red, SymbolType.None );
			LineItem scatter = myPane.AddCurve( "pts", listPts, Color.Blue, SymbolType.Diamond );
			scatter.Line.IsVisible = false;
			scatter.Symbol.Fill = new Fill( Color.White );
			scatter.Symbol.Size = 5;

			ErrorBarItem myBar = myPane.AddErrorBar( "bars", listPts, Color.Green );
			myBar.Bar.Symbol.IsVisible = false;

			z1.AxisChange();
		}

		public void CreateGraph_DualY( ZedGraphControl z1 )
		{

			bool isAssociateWithAxis = true;

			GraphPane myPane = z1.GraphPane;
			// Make up some data points based on the Sine function
			PointPairList vList = new PointPairList();
			PointPairList aList = new PointPairList();
			PointPairList dList = new PointPairList();
			PointPairList eList = new PointPairList();

			for ( int i = 0; i < 30; i++ )
			{
				double xFactor = (double)i;
				vList.Add( xFactor, 15 );
				aList.Add( xFactor, 150 );
				dList.Add( xFactor, 1500 );
				eList.Add( xFactor, 15000 );
			}

			//Form oForm = new Form3();
			//oForm.ShowDialog();

			// Generate a blue curve with "Curve 2" in the legend
			LineItem myCurve = myPane.AddCurve( "Curve 1", vList, Color.Blue );


			Y2Axis yAxisCur = new Y2Axis( "Curve 2" );
			yAxisCur.IsVisible = true;
			myPane.Y2AxisList.Add( yAxisCur );
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxisCur.MajorTic.IsInside = false;
			yAxisCur.MinorTic.IsInside = false;
			yAxisCur.MajorTic.IsOpposite = false;
			yAxisCur.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxisCur.Scale.Align = AlignP.Inside;
			yAxisCur.Type = AxisType.Linear;
			yAxisCur.Scale.FontSpec.FontColor = Color.Green;
			yAxisCur.Title.FontSpec.FontColor = Color.Green;
			yAxisCur.Scale.Min = 100;
			yAxisCur.Scale.Max = 500;


			yAxisCur = new Y2Axis( "Curve 3" );
			yAxisCur.IsVisible = true;
			myPane.Y2AxisList.Add( yAxisCur );
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxisCur.MajorTic.IsInside = false;
			yAxisCur.MinorTic.IsInside = false;
			yAxisCur.MajorTic.IsOpposite = false;
			yAxisCur.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxisCur.Scale.Align = AlignP.Inside;
			yAxisCur.Type = AxisType.Linear;
			yAxisCur.Scale.FontSpec.FontColor = Color.Red;
			yAxisCur.Title.FontSpec.FontColor = Color.Red;
			yAxisCur.Scale.Min = 1000;
			yAxisCur.Scale.Max = 1700;

			yAxisCur = new Y2Axis( "Curve 4" );
			yAxisCur.IsVisible = true;
			myPane.Y2AxisList.Add( yAxisCur );
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxisCur.MajorTic.IsInside = false;
			yAxisCur.MinorTic.IsInside = false;
			yAxisCur.MajorTic.IsOpposite = false;
			yAxisCur.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxisCur.Scale.Align = AlignP.Inside;
			yAxisCur.Type = AxisType.Linear;
			yAxisCur.Scale.FontSpec.FontColor = Color.Orange;
			yAxisCur.Title.FontSpec.FontColor = Color.Orange;
			yAxisCur.Scale.Min = 13000;
			yAxisCur.Scale.Max = 18000;


			myCurve = myPane.AddCurve( "Curve 2", aList, Color.Green );
			myCurve.IsY2Axis = true;
			if ( isAssociateWithAxis )
				myCurve.YAxisIndex = 1;


			myCurve = myPane.AddCurve( "Curve 3", dList, Color.Red );
			myCurve.IsY2Axis = true;
			if ( isAssociateWithAxis )
				myCurve.YAxisIndex = 2;


			myCurve = myPane.AddCurve( "Curve 4", eList, Color.Orange );
			myCurve.IsY2Axis = true;
			if ( isAssociateWithAxis )
				myCurve.YAxisIndex = 3;


			z1.AxisChange();
			z1.Invalidate();

			MessageBox.Show( "now removing curve 3" );

			removeGraphByName( zedGraphControl1, "Curve 3" );


			z1.AxisChange();
			z1.Invalidate();

		}

		private void CreateGraph_MultiYDemo( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Demonstration of Multi Y Graph";
			myPane.XAxis.Title.Text = "Time, s";
			myPane.YAxis.Title.Text = "Velocity, m/s";
			myPane.Y2Axis.Title.Text = "Acceleration, m/s2";

			// Make up some data points based on the Sine function
			PointPairList vList = new PointPairList();
			PointPairList aList = new PointPairList();
			PointPairList dList = new PointPairList();
			PointPairList eList = new PointPairList();

			// Fabricate some data values
			for ( int i = 0; i < 30; i++ )
			{
				double time = (double)i;
				double acceleration = 2.0;
				double velocity = acceleration * time;
				double distance = acceleration * time * time / 2.0;
				double energy = 100.0 * velocity * velocity / 2.0;
				aList.Add( time, acceleration );
				vList.Add( time, velocity );
				eList.Add( time, energy );
				dList.Add( time, distance );
			}

			// --------------------------------------------------------------------
			// Velocity Curve
			// Generate a red curve with diamond symbols, and "Velocity" in the legend
			LineItem myCurve = myPane.AddCurve( "Velocity",
				vList, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Generate a blue curve with circle symbols, and "Acceleration" in the legend
			myCurve = myPane.AddCurve( "Acceleration",
				aList, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;
			// -------------------------------------------------------------------

			// --------------------------------------------------------------------
			// Distance Curve
			// Generate a green curve with square symbols, and "Distance" in the legend
			myCurve = myPane.AddCurve( "Distance", dList, Color.Green, SymbolType.Square );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the second Y axis
			myCurve.YAxisIndex = 1;
			// --------------------------------------------------------------------

			// --------------------------------------------------------------------
			// Energy Curve
			// Generate a Black curve with triangle symbols, and "Energy" in the legend
			myCurve = myPane.AddCurve( "Energy",
				eList, Color.Black, SymbolType.Triangle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;
			// Associate this curve with the second Y2 axis
			myCurve.YAxisIndex = 1;
			// --------------------------------------------------------------------

			// Show the x axis grid
			myPane.XAxis.MajorGrid.IsVisible = true;

			// Make the Y axis scale red
			myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
			myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;
			// Don't display the Y zero line
			myPane.YAxis.MajorGrid.IsZeroLine = false;
			// Align the Y axis labels so they are flush to the axis
			myPane.YAxis.Scale.Align = AlignP.Inside;

			// Enable the Y2 axis display
			myPane.Y2Axis.IsVisible = true;
			// Make the Y2 axis scale blue
			myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			myPane.Y2Axis.MajorTic.IsOpposite = false;
			myPane.Y2Axis.MinorTic.IsOpposite = false;
			// Display the Y2 axis grid lines
			myPane.Y2Axis.MajorGrid.IsVisible = true;
			// Align the Y2 axis labels so they are flush to the axis
			myPane.Y2Axis.Scale.Align = AlignP.Inside;
			myPane.Y2Axis.Scale.Min = 1.5;

			///////////////////////////////////////////////////////////////////
			// Create a second Y Axis, green
			YAxis yAxis3 = new YAxis( "Distance, m" );
			myPane.YAxisList.Add( yAxis3 );
			yAxis3.Scale.FontSpec.FontColor = Color.Green;
			yAxis3.Title.FontSpec.FontColor = Color.Green;
			yAxis3.Color = Color.Green;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxis3.MajorTic.IsInside = false;
			yAxis3.MinorTic.IsInside = false;
			yAxis3.MajorTic.IsOpposite = false;
			yAxis3.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxis3.Scale.Align = AlignP.Inside;
			//////////////////////////////////////////////////////////////////

			//////////////////////////////////////////////////////////////////
			Y2Axis yAxis4 = new Y2Axis( "Energy" );
			yAxis4.IsVisible = true;
			myPane.Y2AxisList.Add( yAxis4 );
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxis4.MajorTic.IsInside = false;
			yAxis4.MinorTic.IsInside = false;
			yAxis4.MajorTic.IsOpposite = false;
			yAxis4.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxis4.Scale.Align = AlignP.Inside;
			yAxis4.Type = AxisType.Log;
			yAxis4.Scale.Min = 100;
			/////////////////////////////////////////////////////////////////

			//////////////////////////////////////////////////////////////////
			Y2Axis yAxis5 = new Y2Axis( "Another one" );
			yAxis5.IsVisible = true;
			myPane.Y2AxisList.Add( yAxis5 );
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxis5.MajorTic.IsInside = false;
			yAxis5.MinorTic.IsInside = false;
			yAxis5.MajorTic.IsOpposite = false;
			yAxis5.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxis5.Scale.Align = AlignP.Inside;
			//yAxis5.Type = AxisType.Log;
			/////////////////////////////////////////////////////////////////

			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );

			myPane.IsAlignGrids = true;


			// Assume this is an old one
			Y2Axis test = new Y2Axis( "test" );
			// save the scale
			Scale saveScale = test.Scale;
			// throw away the "old" Y2 axis
			myPane.Y2AxisList.RemoveAt( 1 );

			// Create a new YAxis
			int index = myPane.YAxisList.Add( "test" );
			// replace the actual scale data
			foreach ( YAxis axis in myPane.YAxisList )
			{
				if ( String.Compare( axis.Title.Text, "test", true ) == 0 )
				{
					// make a new scale
					axis.Scale.MakeNewScale( saveScale, AxisType.Linear );
				}
			}


			//int index2 = myPane.YAxisList.IndexOf( "test" );

			//int x = myPane.YAxisList.IndexOf( "test" );

			//int poop = x;


			//YAxis axis = myPane.YAxisList.Find( null );


			//myPane.YAxisList.Add( test );

			z1.AxisChange();

			z1.Refresh();

			for ( int i = 0; i < 100000000; i++ )
				;

			myPane.Y2AxisList.RemoveAt( myPane.Y2AxisList.Count - 1 );
			//myPane.Y2AxisList.RemoveAt( myPane.Y2AxisList.Count - 1 );


		}

		private void CreateGraph_TestScroll( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			// Make up some data points based on the Sine function 
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i * 5.0;
				double y = Math.Sin( (double)i * Math.PI / 15.0 ) * 16.0;
				double y2 = y * 13.5;
				list.Add( x, y );
				list2.Add( x, y2 );
			}
			// Generate a red curve with diamond symbols, and "Alpha" in the legend 
			LineItem myCurve = myPane.AddCurve( "Alpha", list, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white 
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Generate a blue curve with circle symbols, and "Beta" in the legend 
			myCurve = myPane.AddCurve( "Beta", list2, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white 
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis 
			myCurve.IsY2Axis = true;
			// Show the x axis grid 
			myPane.XAxis.MajorGrid.IsVisible = true;
			// Activate the horizontal scroll bar  
			z1.IsShowHScrollBar = true;
			// Set the scrollable range  
			z1.ScrollMinX = 0;
			z1.ScrollMaxX = 36;
			// Set the current range for the axis scale  
			z1.GraphPane.XAxis.Scale.Min = 26;
			z1.GraphPane.XAxis.Scale.MajorStep = 10;
			z1.GraphPane.XAxis.Scale.Max = 36;
			z1.GraphPane.IsBoundedRanges = false;

			z1.AxisChange();
		}

		private Boolean removeGraphByName( ZedGraphControl z1, string sOpName )
		{
			try
			{
				GraphPane gpane = z1.GraphPane;


				for ( int j = 0; j < gpane.CurveList.Count; j++ )
				{
					string sCurCurveName = gpane.CurveList[j].Label.Text;

					if ( sCurCurveName == sOpName )
					{
						gpane.CurveList.RemoveAt( j );
					}
				}

				for ( int k = 0; k < gpane.Y2AxisList.Count; k++ )
				{
					if ( gpane.Y2AxisList[k].Title.Text == sOpName )
						gpane.Y2AxisList.RemoveAt( k );
				}

				z1.AxisChange();
				z1.Invalidate();
				return false;
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message );
				return false;
			}
		}
		private void zedGraphControl1_Paint( object sender, PaintEventArgs e )
		{
			//e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		}

		private void Form1_MouseDown( object sender, MouseEventArgs e )
		{
		}

		private bool zedGraphControl1_MouseMoveEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			// Save the mouse location
			PointF mousePt = new PointF( e.X, e.Y );

			// Find the Chart rect that contains the current mouse location
			GraphPane pane = sender.MasterPane.FindChartRect( mousePt );

			// If pane is non-null, we have a valid location.  Otherwise, the mouse is not
			// within any chart rect.
			if ( pane != null )
			{
				double x, y, y2;
				// Convert the mouse location to X, Y, and Y2 scale values
				pane.ReverseTransform( mousePt, out x, out y, out y2 );
				// Format the status label text
				toolStripStatusXY.Text = "(" + x.ToString( "f2" ) + ", " + y.ToString( "f2" ) + ")";
			}
			else
				// If there is no valid data, then clear the status label text
				toolStripStatusXY.Text = string.Empty;

			// Return false to indicate we have not processed the MouseMoveEvent
			// ZedGraphControl should still go ahead and handle it
			return false;
		}

		private bool zedGraphControl1_MouseDownEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			double x, y, y2;
			sender.GraphPane.ReverseTransform( new PointF( e.X, e.Y ), out x, out y, out y2 );

			return false;

			CurveItem dragCurve;
			int dragIndex;
			GraphPane myPane = this.zedGraphControl1.GraphPane;
			PointPair startPair;
			PointF mousePt = new PointF( e.X, e.Y );

			// find the point that was clicked, and make sure the point list is editable
			// and that it's a primary Y axis (the first Y or Y2 axis)
			if ( myPane.FindNearestPoint( mousePt, out dragCurve, out dragIndex ) &&
						dragCurve.Points is PointPairList )
			{
				startPair = dragCurve.Points[dragIndex];

				//this is working with the main curve
				//PointF ptScrXY = myPane.GeneralTransform(startPair, CoordType.AxisXY2Scale);
				PointF ptScrXY = new PointF();
				Double dx, dy;

				// setup to transform the data to a pixel location
				myPane.XAxis.Scale.SetupScaleData( myPane, myPane.XAxis );
				Axis yAxis = dragCurve.GetYAxis( myPane );
				yAxis.Scale.SetupScaleData( myPane, yAxis );

				ptScrXY.X = myPane.XAxis.Scale.Transform( dragCurve.IsOverrideOrdinal, dragIndex,
							startPair.X );
				ptScrXY.Y = yAxis.Scale.Transform( dragCurve.IsOverrideOrdinal, dragIndex,
							startPair.Y );

				// shift the pixel location up and to the left
				ptScrXY.X += 5;
				ptScrXY.Y -= 5;

				// Convert the pixel location back to an axis, but this time make sure it
				// is always assigned to the primary Y axis no matter which curve the data came from
				myPane.ReverseTransform( ptScrXY, false, 0, out dx, out dy );

				TextObj text = new TextObj( "Test Label", (float)dx, (float)dy, CoordType.AxisXYScale,
					AlignH.Left, AlignV.Bottom );

				myPane.GraphObjList.Add( text );

				return true;
			}

			return false;
		}

	}
}




#if false

	public partial class Form1 : Form
	{
		const int NUMPOINTS = 10000;
		public Form1()
		{
			InitializeComponent();

			zedGraphControl1.GraphPane.CurveList.Clear();
			zedGraphControl1.MasterPane.SetLayout( PaneLayout.SingleColumn );

			//These curves will be missing the last few points 
			AddCurve( 0, 0 );
			AddCurve( 0, 1 );

			//These curves will be missing all points 
			AddCurve( 1, 0 );
			AddCurve( 1, 1 );

			//If you comment out these two lines, the curves on pane 0 will look ok 
			UpdateAxes();
			FilterAllCurves();

			zedGraphControl1.Refresh();
		}

		private void AddCurve( int pane, int curve )
		{
			if ( pane > zedGraphControl1.MasterPane.PaneList.Count )
			{
				throw ( new System.Exception( "You can only add a pane to the end of the list" ) );
			}
			int i;
			if ( pane >= zedGraphControl1.MasterPane.PaneList.Count )
			{
				GraphPane pane1 = new GraphPane();
				zedGraphControl1.MasterPane.PaneList.Add( pane1 );

				//Is this correct for the new layout? 
				Graphics g = zedGraphControl1.CreateGraphics();
				zedGraphControl1.MasterPane.DoLayout( g );
				g.Dispose();

				//New pane layout to refresh all the axes and filtered data 
				//UpdateAxes();
				//FilterAllCurves();
			}

			//Create the data 
			NoDupePointList ndp1 = new NoDupePointList();
			for ( i = 0; i < NUMPOINTS; i++ )
			{
				if ( ( curve & 1 ) == 0 )
					ndp1.Add( i, -( NUMPOINTS / 2 ) + i );
				else
					ndp1.Add( i, ( NUMPOINTS / 2 ) - i );
			}

			//Create the curve 
			zedGraphControl1.MasterPane.PaneList[pane].AddCurve( "ndp" + curve.ToString(), ndp1, Color.Red );

			//New curve so update the axes and filtering for this pane 
			UpdateAxes();
			FilterCurves( pane );
		}

		private void FilterCurves( int pane )
		{
			int i;
			for ( i = 0; i < zedGraphControl1.MasterPane.PaneList[pane].CurveList.Count; i++ )
			{
				NoDupePointList ndp = zedGraphControl1.MasterPane.PaneList[pane].CurveList[i].Points as NoDupePointList;
				if ( ndp != null )
				{
					//If you comment this out you will see what it is supposed to look like 
					ndp.FilterData( zedGraphControl1.MasterPane.PaneList[pane], zedGraphControl1.MasterPane.PaneList[pane].YAxis );
				}
			}
		}

		private void FilterAllCurves()
		{
			for ( int pane = 0; pane < zedGraphControl1.MasterPane.PaneList.Count; pane++ )
			{
				FilterCurves( pane );
			}
		}

		private void UpdateAxes()
		{
			//Clear the filtering so that AxisChange has all the data to work with 
			int curve, pane;
			for ( pane = 0; pane < zedGraphControl1.MasterPane.PaneList.Count; pane++ )
			{
				for ( curve = 0; curve < zedGraphControl1.MasterPane.PaneList[pane].CurveList.Count; curve++ )
				{
					NoDupePointList ndp = zedGraphControl1.MasterPane.PaneList[pane].CurveList[curve].Points as NoDupePointList;
					if ( ndp != null )
					{
						ndp.ClearFilter();
					}
				}
			}
			//Calculate new axes 
			zedGraphControl1.AxisChange();
		}

		private void Form1_ResizeEnd( object sender, EventArgs e )
		{
			UpdateAxes();
			FilterAllCurves();
			zedGraphControl1.Refresh();
		}

		private void Form1_Load( object sender, EventArgs e )
		{
		}

		private void zedGraphControl1_Paint( object sender, PaintEventArgs e )
		{
		}

		private void Form1_MouseDown( object sender, MouseEventArgs e )
		{
			ZedGraph.ControlTest.Form2 form2 = new ZedGraph.ControlTest.Form2();
			form2.Show();

			form2.zg1 = zedGraphControl1;
		}

		private bool zedGraphControl1_MouseMoveEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			// Save the mouse location
			PointF mousePt = new PointF( e.X, e.Y );

			// Find the Chart rect that contains the current mouse location
			GraphPane pane = sender.MasterPane.FindChartRect( mousePt );

			// If pane is non-null, we have a valid location.  Otherwise, the mouse is not
			// within any chart rect.
			if ( pane != null )
			{
				double x, y, y2;
				// Convert the mouse location to X, Y, and Y2 scale values
				pane.ReverseTransform( mousePt, out x, out y, out y2 );
				// Format the status label text
				toolStripStatusXY.Text = "(" + x.ToString( "f2" ) + ", " + y.ToString( "f2" ) + ")";
			}
			else
				// If there is no valid data, then clear the status label text
				toolStripStatusXY.Text = string.Empty;

			// Return false to indicate we have not processed the MouseMoveEvent
			// ZedGraphControl should still go ahead and handle it
			return false;
		}

		private bool zedGraphControl1_MouseDownEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			Point mousePt = new Point( e.X, e.Y );
			CurveItem curve;
			int iPt;
			if ( sender.GraphPane.FindNearestPoint( mousePt, out curve, out iPt ) &&
					Control.ModifierKeys == Keys.Alt )
			{
				IPointListEdit list = curve.Points as IPointListEdit;
				if ( list == null )
					return false;

				for ( int i = 0; i < list.Count; i++ )
					list[i].Z = 0;

				list[iPt].Z = 1;
				sender.Refresh();

				return false;
			}

			return false;
		}
		private void Form1_Resize( object sender, EventArgs e )
		{
			SetSize();
		}

		private void SetSize()
		{
			Rectangle pageRect = this.ClientRectangle;
			pageRect.Inflate( -10, -10 );
			pageRect.Height -= 20;
			//tabControl1.Size = formRect.Size;


			//Rectangle pageRect = tabControl1.SelectedTab.ClientRectangle;
			//pageRect.Inflate( -10, -10 );

			if ( zedGraphControl1.Size != pageRect.Size )
				zedGraphControl1.Size = pageRect.Size;

			double junk = DateTime.Now.ToOADate();
			// Fix the ellipseItem to a perfect circle by using a fixed height, but a variable
			// width
			if ( zedGraphControl1.GraphPane.GraphObjList.Count > 0 )
			{
				EllipseObj ellipse = zedGraphControl1.GraphPane.GraphObjList[0] as EllipseObj;
				if ( ellipse != null )
				{
					GraphPane myPane = zedGraphControl1.GraphPane;
					float dx = (float)( myPane.XAxis.Scale.Max - myPane.XAxis.Scale.Min );
					float dy = (float)( myPane.YAxis.Scale.Max - myPane.YAxis.Scale.Min );
					float xPix = myPane.Chart.Rect.Width * (float)ellipse.Location.Width / dx;
					float yPix = myPane.Chart.Rect.Height * (float)ellipse.Location.Height / dy;

					ellipse.Location.Width *= yPix / xPix;

					// alternatively, use this to vary the height but fix the width
					// (comment out the width line above)
					//ellipse.Location.Height *= xPix / yPix;
				}
			}
		}
	}
#endif