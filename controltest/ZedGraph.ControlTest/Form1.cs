using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
			//CreateGraph_MasterWithPies( zedGraphControl1 );
			//CreateGraph_NormalPane( zedGraphControl1 );
			//CreateGraph_DateWithTimeSpan( zedGraphControl1 );
			//CreateGraph_SamplePointListDemo( zedGraphControl1 );
			//CreateGraph_RadarPlot( zedGraphControl1 );
			//CreateGraph_CandleStick( zedGraphControl1 );
			CreateGraph_JapaneseCandleStick( zedGraphControl1 );
			//CreateGraph_BasicLinear( zedGraphControl2 );
			//CreateGraph_BasicLog( zedGraphControl2 );
			//CreateGraph_StackLine( zedGraphControl1 );
			//CreateGraph_MasterPane( zedGraphControl1 );
			//CreateGraph_VerticalBars( zedGraphControl1 );
			//CreateGraph_HorizontalBars( zedGraphControl1 );
			//CreateGraph_MasterPane( zedGraphControl1 );
			//CreateGraph_GradientByZBars( zedGraphControl2 );
			//CreateGraph_DualYDemo( zedGraphControl1 );
			//CreateGraph_ClusteredStackBar( zedGraphControl1 );
			//CreateGraph_GrowingData( zedGraphControl1 );
			//CreateGraph_SplineTest( zedGraphControl1 );
			//CreateGraph_DateAxis( zedGraphControl1 );
			//CreateGraph_BasicLinearScroll( zedGraphControl1 );
			//CreateGraph_ScrollTest( zedGraphControl1 );
			//CreateGraph_TwoTextAxes( zedGraphControl1 );
			//CreateGraph_ThreeVerticalPanes( zedGraphControl1 );
			//CreateGraph_32kPoints( zedGraphControl2 );
			//CreateGraph_ImageSymbols( zedGraphControl1 );
			//CreateGraph_OnePoint( zedGraphControl1 );
			//CreateGraph_StackedBars( zedGraphControl2 );
			//CreateGraph_StickToCurve( zedGraphControl1 );
			CreateGraph_TextBasic( zedGraphControl2 );

			//CreateGraph_DataSource( zedGraphControl1 );
			//CreateGraph_PolyTest( zedGraphControl1 );
			//CreateGraph_BarJunk( zedGraphControl2 );
			//CreateGraph_Contour( zedGraphControl2 );
			//CreateGraph_Junk( zedGraphControl2 );

			zedGraphControl1.AxisChange();
			zedGraphControl2.AxisChange();
			SetSize();
		}

		private void Form1_Resize( object sender, EventArgs e )
		{
			SetSize();
		}

		private void SetSize()
		{
			Rectangle formRect = this.ClientRectangle;
			formRect.Inflate( -10, -10 );
			tabControl1.Size = formRect.Size;


			Rectangle pageRect = tabControl1.SelectedTab.ClientRectangle;
			pageRect.Inflate( -10, -10 );
			if ( zedGraphControl1.Size != pageRect.Size )
			{
				zedGraphControl1.Size = pageRect.Size;
				zedGraphControl2.Size = pageRect.Size;
			}

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
					float xPix = myPane.Chart.Rect.Width * ellipse.Location.Width / dx;
					float yPix = myPane.Chart.Rect.Height * ellipse.Location.Height / dy;

					ellipse.Location.Width *= yPix / xPix;

					// alternatively, use this to vary the height but fix the width
					// (comment out the width line above)
					//ellipse.Location.Height *= xPix / yPix;
				}
			}
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

				MasterPane master = (MasterPane) mySerializer.Deserialize( myReader );
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

			for ( int i = 0; i < 18; i++ )
			{
				x = new XDate( 1995, i, i + 5, i, i * 2, i * 3 );
				//x = (double) i;

				y1 = ( Math.Sin( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				y2 = ( Math.Cos( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}

			LineItem myCurve = myPane.AddCurve( "Sine", list1, Color.Red, SymbolType.Circle );
			LineItem myCurve2 = myPane.AddCurve( "Cos", list2, Color.Blue, SymbolType.Circle );
			myCurve2.YAxisIndex = 1;
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

			//Color[] colors = { Color.Red, Color.Green, Color.Blue,
			//					Color.Yellow, Color.Orange };
			//Fill _fill = new Fill( colors );
			//_fill.Type = FillType.GradientByZ;
			//_fill.RangeMin = 1;
			//_fill.RangeMax = 5;

			myPane.XAxis.Scale.Format = "yyyy-MM-dd HH:MM";
			//myPane.XAxis.ScaleMag = 0;
			myPane.XAxis.Type = AxisType.Date;
			//myPane.YAxis.Max = 2499.9;
			//myPane.YAxis.IsScaleVisible = false;
			//myPane.YAxis.IsTic = false;
			//myPane.YAxis.IsMinorTic = false;


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

			z1.MasterPane.SetLayout( PaneLayout.ExplicitRow32 );
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

			myPane.XAxis.Scale.IsSkipLastLabel = false;
			//myPane.XAxis.IsPreventLabelOverlap = false;
			myPane.XAxis.Scale.Format = "[d].[h]:[m]:[s]";
			z1.PointDateFormat = "[d].[hh]:[mm]:[ss]";
			myPane.XAxis.Type = AxisType.Date;
			z1.AxisChange();

			myPane.YAxis.Scale.Format = "0.0'%'";
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
			myCurve.Stick.Size = 5;
			//myCurve.CandleStick.PenWidth = 2;
			//myCurve.CandleStick.IsOpenCloseVisible = false;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.MajorStep = 1.0;

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

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
			JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick( "trades", spl );
			//myCurve.Stick.Size = 5;
			//myCurve.CandleStick.PenWidth = 2;
			myCurve.Stick.Color = Color.Blue;
			//myCurve.CandleStick.IsOpenCloseVisible = false;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.MajorStep = 1.0;

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();
		}

		Timer myTimer;

		private void MyTimer_Tick( object sender, EventArgs e )
		{
			// Get the first CurveItem in the graph
			LineItem curve = zedGraphControl2.GraphPane.CurveList[0] as LineItem;
			// Get the PointPairList
			PointPairList list = curve.Points as PointPairList;
			//list.Add( xvalue, (double)cpuUsagePerformanceCounter.NextValue() );
			list.Add( 1, 1 );
		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinear( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myTimer = new Timer();
			myTimer.Enabled = true;
			myTimer.Tick += new EventHandler( MyTimer_Tick );
			myTimer.Interval = 500;
			myTimer.Start();


			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i + 5;
				double y = 300.0 * ( 1.5 + Math.Sin( (double)i * 0.2 ) );
				list.Add( x, y );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;
			z1.IsAutoScrollRange = true;

			z1.IsEnableVEdit = true;
			//z1.IsEnableVEdit = false;

			z1.IsEnableVZoom = false;
			z1.GraphPane.IsBoundedRanges = false;

			//z1.GraphPane.IsBoundedRanges = false;
			//z1.ScrollMinX = 0;
			//z1.ScrollMaxX = 100;

			myPane.XAxis.Title.FontSpec.Family = "Tahoma";

			z1.AxisChange();
		}

		// Basic curve test - Log Axis
		private void CreateGraph_BasicLog( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				double x = (double)i + 5;
				double y = 3000.0 * ( 1.5 + Math.Sin( (double)i * 0.2 ) ) + 1.0;
				list.Add( x, y );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myPane.YAxis.Type = AxisType.Log;
			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;
			z1.IsAutoScrollRange = true;

			z1.IsEnableVEdit = true;
			z1.IsEnableHEdit = true;

			//z1.IsEnableZoom = true;
			myPane.IsBoundedRanges = false;

			z1.AxisChange();
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
			EllipseObj ellipse = new EllipseObj( rect, Color.Black, Color.White, Color.Blue );
			myPane.GraphObjList.Add( ellipse );

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

			Graphics g = this.CreateGraphics();

			//master.PaneLayoutMgr.SetLayout( PaneLayout.ExplicitRow32 );
			//master.PaneLayoutMgr.SetLayout( 2, 4 );
			master.SetLayout( false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
			z1.AxisChange();

			g.Dispose();
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

		// masterpane with three vertical panes
		private void CreateGraph_ThreeVerticalPanes( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			master.Fill = new Fill( Color.FromArgb( 230, 230, 255 ) );
			master.PaneList.Clear();

			master.Title.IsVisible = true;
			master.Title.Text = "My MasterPane Title";

			master.Margin.All = 10;
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

				myPaneT.Fill = new Fill( Color.FromArgb( 230, 230, 255 ) );
				myPaneT.Chart.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPaneT.BaseDimension = 6.0F;
				myPaneT.XAxis.Title.IsVisible = false;
				myPaneT.XAxis.Scale.IsVisible = false;
				myPaneT.Legend.IsVisible = false;
				myPaneT.Border.IsVisible = false;
				myPaneT.Title.IsVisible = false;
				myPaneT.XAxis.MajorTic.IsOutside = false;
				myPaneT.XAxis.MinorTic.IsOutside = false;
				myPaneT.XAxis.MajorGrid.IsVisible = true;
				myPaneT.XAxis.MinorGrid.IsVisible = true;
				myPaneT.Margin.All = 0;
				if ( j == 0 )
					myPaneT.Margin.Top = 20;
				if ( j == 2 )
				{
					myPaneT.XAxis.Title.IsVisible = true;
					myPaneT.XAxis.Scale.IsVisible = true;
					myPaneT.Margin.Bottom = 10;
				}

				// This sets the minimum amount of space for the left and right side, respectively
				// The reason for this is so that the ChartRect's all end up being the same size.
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

			master.SetLayout( PaneLayout.SingleColumn );
			//master.SetLayout( PaneLayout.ExplicitRow32 );
			//master.SetLayout( 2, 4 );
			//master.SetLayout( false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
			z1.AxisChange();

			z1.IsAutoScrollRange = true;
			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;
			z1.IsSynchronizeXAxes = true;

			g.Dispose();
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
			myPane.BarSettings.Type = BarType.Stack;
			myPane.BarSettings.Base = BarBase.X;

			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five", "six" };
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();


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

		private void CreateGraph_GradientByZBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 5; i++ )
			{
				double x = (double)i;
				double y = rand.NextDouble() * 1000;
				double z = (y<300) ? 0 : ( (y<600) ? 0.5 : 1.0 );
				list.Add( x, y, z );
			}

			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.Green, Color.Blue, 0.0f );
			myCurve.Bar.Fill.Type = FillType.GradientByZ;
			myCurve.Bar.Fill.RangeMin = 0;
			myCurve.Bar.Fill.RangeMax = 1;

			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five" };
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();


		}

		private void CreateGraph_PolyTest( ZedGraphControl z1 )
		{
			GraphPane junk = z1.GraphPane.Clone();


			// Get a reference to the GraphPane instance in the ZedGraphControl
			GraphPane myPane = z1.GraphPane;

			PointF[] corners = new PointF[4];
			corners[0] = new PointF( 300.0f, 85.0f );
			corners[1] = new PointF( 400.0f, 85.0f );
			corners[2] = new PointF( 400.0f, 95.0f );
			corners[3] = new PointF( 300.0f, 95.0f );
			PolyObj poly1 = new PolyObj( corners, Color.Empty, Color.Red );

			PointF[] corners3 = new PointF[3];
			corners3[0] = new PointF( 300.0f, 88.0f );
			corners3[1] = new PointF( 375.0f, 95.0f );
			corners3[2] = new PointF( 300.0f, 95.0f );
			PolyObj poly3 = new PolyObj( corners3, Color.Empty, Color.LightGreen );

			PointF[] corners2 = new PointF[3];
			corners2[0] = new PointF( 333.0f, 85.0f );
			corners2[1] = new PointF( 400.0f, 91.0f );
			corners2[2] = new PointF( 400.0f, 85.0f );
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
			for ( int i = 0; i < 100; i++ )
			{
				double x = (double)i * 5.0;
				double y = Math.Sin( (double)i * Math.PI / 15.0 ) * 16.0;
				double y2 = y * 13.5;
				list.Add( x, y );
				list2.Add( x, y2 );
			}

			// Generate a red curve with diamond symbols, and "Alpha" in the _legend
			LineItem myCurve = myPane.AddCurve( "Alpha",
				list, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with circle symbols, and "Beta" in the _legend
			myCurve = myPane.AddCurve( "Beta",
				list2, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;

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
			myPane.YAxis.Scale.Min = -30;
			myPane.YAxis.Scale.Max = 30;

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
			//z1.PointValueEvent += new ZedGraphControl.PointValueHandler( MyPointValueHandler );

			// Add a custom context menu item
			//z1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler( MyContextMenuBuilder );

			//z1.ScrollEvent += new ZedGraph.ZedGraphControl.ScrollEventHandler( zedGraphControl1_ScrollEvent );
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

		private void CreateGraph_SplineTest( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
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
		}

		// Basic curve test - Date Axis
		private void CreateGraph_DateAxis( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();

			double x = new XDate( 1990, 1, 2 );
			double y = Math.Sin( i / 8.0 ) * 1000 + 1001;
			list.Add( x, y );
			x = new XDate( 1990, 1, 3 );
			list.Add( x, y );
			x = new XDate( 1990, 1, 8 );
			list.Add( x, y );
			x = new XDate( 1990, 1, 28 );
			list.Add( x, y );


			/*			for ( int i = 0; i < 20; i++ )
						{
							double x = new XDate( 2005, 12, 30+i );
							double y = Math.Sin( i / 8.0 ) * 1000 + 1001;
							list.Add( x, y, 1500 );
							list2.Add( x, y * 1.2, 1500 );
						}
			*/
			LineItem line = myPane.AddCurve( "Line", list, Color.Blue, SymbolType.Diamond );
			//myPane.XAxis.Scale.Format = "MMM\nyyyy";
			myPane.XAxis.Type = AxisType.Date;

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

		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinearScroll( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i = 1; i < 100; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			z1.IsShowHScrollBar = true;
			z1.IsShowVScrollBar = true;
			z1.IsAutoScrollRange = true;

			//z1.GraphPane.XAxis.Min = 1;
			//z1.GraphPane.XAxis.Max = 100;
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

		// Basic curve test - 32000 points
		private void CreateGraph_32kPoints( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 70000; i++ )
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

			DataSourcePointList dspl = new DataSourcePointList();
			//ZedGraph.ControlTest.NorthwindDataSetTableAdapters.OrdersTableAdapter adapter =
			//			new ZedGraph.ControlTest.NorthwindDataSetTableAdapters.OrdersTableAdapter();
			//ZedGraph.ControlTest.NorthwindDataSet.OrdersDataTable table = adapter.MyQuery();

			//dspl.DataSource = table;
			//dspl.XDataMember = "OrderDate";
			//dspl.YDataMember = "Freight";
			//dspl.ZDataMember = null;
			//dspl.TagDataMember = "ShipName";
			
			List<Sample> sampleList = new List<Sample>(); 
			
			//ArrayList arrayList = new ArrayList();
			DateTime dt = new DateTime( 2006, 3, 1 );
			for ( int i = 0; i < 50; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 5 );
				Sample sample = new Sample();
				sample.Time = dt.AddDays( i );
				sample.Position = x;
				sample.Velocity = y;
				//arrayList.Add( sample );
				sampleList.Add( sample );
			}

			dspl.DataSource = sampleList;
			dspl.XDataMember = "Time";
			dspl.YDataMember = "Position";
			dspl.ZDataMember = "Velocity";
			dspl.TagDataMember = null;
			
			

			//int count = table.Count;


			PointPair pt = dspl[5];

			LineItem myCurve = z1.GraphPane.AddCurve( "curve", dspl, Color.Blue );
			z1.AxisChange();

		}

		private void CreateGraph_BarJunk( ZedGraphControl z1 )
		{
			GraphPane grPane = z1.GraphPane;

			// X axis
			grPane.XAxis.Title.Text = "Bins";
			grPane.XAxis.Type = AxisType.LinearAsOrdinal;

			// Y axis
			grPane.YAxis.Title.Text = "Counts";
			grPane.YAxis.Type = AxisType.Linear;

			// Graph
			grPane.Title.Text = "Histogram";
			grPane.Legend.IsVisible = false;

			PointPairList pointPairList = new PointPairList();
			pointPairList.Add( Convert.ToDouble( 0 ), Convert.ToDouble( 20 ) );
			pointPairList.Add( Convert.ToDouble( 1 ), Convert.ToDouble( 75 ) );
			pointPairList.Add( Convert.ToDouble( 2 ), Convert.ToDouble( 98 ) );
			pointPairList.Add( Convert.ToDouble( 3 ), Convert.ToDouble( 450 ) );
			pointPairList.Add( Convert.ToDouble( 4 ), Convert.ToDouble( 578 ) );
			pointPairList.Add( Convert.ToDouble( 5 ), Convert.ToDouble( 64 ) );
			pointPairList.Add( Convert.ToDouble( 6 ), Convert.ToDouble( 90 ) );
			pointPairList.Add( Convert.ToDouble( 7 ), Convert.ToDouble( 17 ) );
			pointPairList.Add( Convert.ToDouble( 8 ), Convert.ToDouble( 2 ) );
			pointPairList.Add( Convert.ToDouble( 9 ), Convert.ToDouble( 1 ) );

			CurveItem myCurve = grPane.AddBar( "My Curve", pointPairList, Color.Red );

			grPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166 ), 45.0F );

			z1.AxisChange();
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
						new Fill( Color.White, Color.FromArgb( 255, 255, 150), 45.0f );
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

		private void zedGraphControl1_Paint( object sender, PaintEventArgs e )
		{
			//e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		}

		private void tabPage1_Click( object sender, EventArgs e )
		{

		}

		private void button1_Click( object sender, EventArgs e )
		{
			tabControl1.SelectedTab = tabPage2;
		}

		int i = 0;
		private void zedGraphControl2_Scroll( object sender, ScrollEventArgs e )
		{
			i++;
		}

		int j = 0;
		private void zedGraphControl2_ScrollEvent( ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState )
		{
			j++;
		}

		private void Form1_MouseDown( object sender, MouseEventArgs e )
		{
			if ( Control.ModifierKeys == Keys.Shift )
				SoapDeSerialize( zedGraphControl1, "savefile.soap" );
			else
				SoapSerialize( zedGraphControl1, "savefile.soap" );
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
				toolStripStatusXY.Text = "(" + x.ToString("f2") + ", " + y.ToString("f2") + ")";
			}
			else
				// If there is no valid data, then clear the status label text
				toolStripStatusXY.Text = string.Empty;

			// Return false to indicate we have not processed the MouseMoveEvent
			// ZedGraphControl should still go ahead and handle it
			return false;
		}
	}
}