
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
using ZedGraph;

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
			//CreateGraph_AxisCrossDemo( zedGraphControl1 );
			//CreateGraph_BarJunk( zedGraphControl1 );
			//CreateGraph_BarJunk2( zedGraphControl1 );
			//CreateGraph_BarJunk3( zedGraphControl1 );
			//CreateGraph_BasicLinear( zedGraphControl1 );
			//CreateGraph_BasicLinearSimple( zedGraphControl1 );
			//CreateGraph_BasicLinearSimpleUserSymbol( zedGraphControl1 );
			//CreateGraph_BasicLinear3Curve( zedGraphControl1 );
			//CreateGraph_BasicLinearReverse( zedGraphControl1 );
			//CreateGraph_BasicLinearScroll( zedGraphControl1 );
			//CreateGraph_BasicLog( zedGraphControl1 );
			//CreateGraph_BasicStick( zedGraphControl2 );
			//CreateGraph_ClusteredStackBar( zedGraphControl1 );
			//CreateGraph_Clone( zedGraphControl1 );
			//CreateGraph_Contour( zedGraphControl2 );
			//CreateGraph_DateAsOrdinal( zedGraphControl1 );
			//CreateGraph_DateAsOrdinal2( zedGraphControl1 );
			//CreateGraph_DateAxis( zedGraphControl1 );
			//CreateGraph_DateAxisTutorial( zedGraphControl1 );
			//CreateGraph_DataSource( zedGraphControl1 );
			//CreateGraph_DataSourcePointList( zedGraphControl1 );
			CreateGraph_DataSourcePointListTest( zedGraphControl1 );
			//CreateGraph_DataSourcePointListArrayList( zedGraphControl1 );
			//CreateGraph_DateWithTimeSpan( zedGraphControl1 );
			//CreateGraph_DualYDemo( zedGraphControl1 );
			//CreateGraph_FilteredPointList( zedGraphControl1 );
			//CreateGraph_FlatLine( zedGraphControl1 );
			//CreateGraph_Gantt( zedGraphControl1 );
			//CreateGraph_GasGauge( zedGraphControl1 );
			//CreateGraph_GradientByZBars( zedGraphControl1 );
			//CreateGraph_GradientByZPoints( zedGraphControl1 );
			//CreateGraph_GrowingData( zedGraphControl1 );
			//CreateGraph_HiLowBarDemo( zedGraphControl1 );
			//CreateGraph_HorizontalBars( zedGraphControl1 );
			//CreateGraph_Histogram( zedGraphControl1 );
			//CreateGraph_ImageSymbols( zedGraphControl1 );
			//CreateGraph_ImageObj( zedGraphControl1 );
			//CreateGraph_JapaneseCandleStick( zedGraphControl1 );
			//CreateGraph_JapaneseCandleStickDemo( zedGraphControl1 );
			//CreateGraph_JapaneseCandleStickDemo2( zedGraphControl1 );
			//CreateGraph_Junk( zedGraphControl1 );
			//CreateGraph_Junk2( zedGraphControl1 );
			//CreateGraph_Junk4( zedGraphControl1 );
			//CreateGraph_junk5( zedGraphControl1 );
			//CreateGraph_junk6( zedGraphControl1 );
			//CreateGraph_junk7( zedGraphControl1 );
			//CreateGraph_junk8( zedGraphControl1 );
			//CreateGraph_junk9( zedGraphControl1 );
			//CreateGraph_junk10( zedGraphControl1 );
			//CreateGraph_junk11( zedGraphControl1 );
			//CreateGraph_LabeledPointsDemo( zedGraphControl1 );
			//CreateGraph_LineWithBandDemo( zedGraphControl1 );
			//CreateGraph_LineColorGradient( zedGraphControl1 );
			//CreateGraph_MasterPane( zedGraphControl1 );
			//CreateGraph_MasterPane_Tutorial( zedGraphControl1 );
			//CreateGraph_MasterPane_Square( zedGraphControl1 );
			//CreateGraph_MasterWithPies( zedGraphControl1 );
			//CreateGraph_MultiYDemo( zedGraphControl1 );
			//CreateGraph_NegativeBars( zedGraphControl1 );
			//CreateGraph_NegativeHorizontalBars( zedGraphControl1 );
			//CreateGraph_NoDupePointList( zedGraphControl1 );
			//CreateGraph_NormalPane( zedGraphControl1 );
			//CreateGraph_OHLCBar( zedGraphControl1 );
			//CreateGraph_OHLCBarGradient( zedGraphControl1 );
			//CreateGraph_OHLCBarMaster( zedGraphControl1 );
			//CreateGraph_OnePoint( zedGraphControl1 );
			//CreateGraph_OverlayBarDemo( zedGraphControl1 );
			//CreateGraph_Pie( zedGraphControl1 );
			//CreateGraph_PolyTest( zedGraphControl1 );
			//CreateGraph_RadarPlot( zedGraphControl1 );
			//CreateGraph_SamplePointListDemo( zedGraphControl1 );
			//CreateGraph_ScatterPlot( zedGraphControl1 );
			//CreateGraph_ScrollTest( zedGraphControl1 );
			//CreateGraph_ScrollProblem( zedGraphControl1 );
			//CreateGraph_ScrollSample( zedGraphControl1 );
			//CreateGraph_SortedOverlayBars( zedGraphControl1 );
			//CreateGraph_SpiderPlot( zedGraphControl1 );
			//CreateGraph_SplineTest( zedGraphControl1 );
			//CreateGraph_StackedBars( zedGraphControl1 );
			//CreateGraph_StackedLinearBars( zedGraphControl1 );
			//CreateGraph_StackedMultiBars( zedGraphControl1 );
			//CreateGraph_StackLine( zedGraphControl1 );
			//CreateGraph_StepChartDemo( zedGraphControl1 );
			//CreateGraph_StickToCurve( zedGraphControl1 );
			//CreateGraph_TestScroll( zedGraphControl1 );
			//CreateGraph_TextBasic( zedGraphControl2 );
			//CreateGraph_ThreeVerticalPanes( zedGraphControl1 );
			//CreateGraph_TwoTextAxes( zedGraphControl1 );
			//CreateGraph_VerticalBars( zedGraphControl1 );
			//CreateGraph_VerticalLinearBars( zedGraphControl1 );
			//CreateGraph_DualY( zedGraphControl1 );
			//CreateGraph_X2Axis( zedGraphControl1 );

			SetSize();
			zedGraphControl1.AxisChange();
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

			zedGraphControl1.AxisChange();

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

				myReader.Close();

				z1.MasterPane = master;
				z1.Refresh();
				//trigger a resize event
				z1.Size = z1.Size;
			}
		}

		public void CreateGraph_AxisCrossDemo( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Axis Cross Demo";
			myPane.XAxis.Title.Text = "My X Axis";
			myPane.YAxis.Title.Text = "My Y Axis";

			// Make up some data arrays based on the Sine function
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i = 0; i < 37; i++ )
			{
				x = ( (double)i - 18.0 ) / 5.0;
				y = x * x + 1.0;
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = myPane.AddCurve( "Parabola",
				list, Color.Green, SymbolType.Diamond );

			// Set the Y axis intersect the X axis at an X value of 0.0
			myPane.YAxis.Cross = 0.0;
			// Turn off the axis frame and all the opposite side tics
			myPane.Chart.Border.IsVisible = false;
			myPane.XAxis.MajorTic.IsOpposite = false;
			myPane.XAxis.MinorTic.IsOpposite = false;
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;

			// Calculate the Axis Scale Ranges
			zgc.AxisChange();
			//			zgc.Refresh();
		}

		private void CreateGraph_Clone( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			Border myBorder = new Border();
			myBorder.IsVisible = false;
			master.Border = myBorder;
			master.PaneList.Clear();
			master.Title.Text = "First try";
			master.Margin.All = 10;
			master.InnerPaneGap = 10;
			// Enable the master pane legend 
			master.Legend.IsVisible = true;
			master.Legend.Position = ZedGraph.LegendPos.TopCenter;
			GraphPane[] myPane = new GraphPane[3];
			for ( int j = 0; j < 3; j++ )
			{
				if ( j < 2 )
					myPane[j] = new GraphPane( new RectangleF( 40, 40, 150, 100 ), "", "", "" );
				else
					myPane[j] = new GraphPane( new RectangleF( 40, 40, 300, 100 ), "", "", "" );

				myPane[j].Fill = new Fill( Color.BlanchedAlmond );

				// myPane(j).AxisFill = New ZedGraph.Fill(Color.SeaGreen) 
				myPane[j].BaseDimension = 6.0F;
				myPane[j].XAxis.Title.IsVisible = true;
				myPane[j].XAxis.Scale.IsVisible = true;
				myPane[j].Legend.IsVisible = false;
				myPane[j].Border.IsVisible = false;
				myPane[j].Title.IsVisible = true;
				myPane[j].Margin.All = 0;

				// This sets the minimum amount of space for the left and right side, respectively 
				// The reason for this is so that the AxisRects all end up being the same size. 

				myPane[j].YAxis.MinSpace = 50;
				myPane[j].Y2Axis.MinSpace = 20;
				master.Add( myPane[j] );
			}

			using ( Graphics g = CreateGraphics() )
				master.SetLayout( g, PaneLayout.ExplicitCol21 );
			//Call GetMonthlist() 
			//myPane[0] = zgHistogram.GraphPane.Clone;
			//myPane[1] = zgProbability.GraphPane.Clone;
			//myPane[2] = zgTimeSeries.GraphPane.Clone; 

			// instead of  

			// Call CreateGraph_TimeSeries(zgM, myPane(2)) 
			// Call CreateGraph_Probability(zgM, myPane(1)) 
			// Call CreateGraph_Histogram(zgM, myPane(0)) 
			z1.AxisChange();
			z1.Refresh();
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
				x = (double)i;

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

		// Basic curve test - Line Graph with DateAsOrdinal
		private void CreateGraph_DateAsOrdinal( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i = 0; i < 100; i++ )
			{
				double x = new XDate( 2007, 6, 3 + i );
				double y = Math.Sin( i / 8.0 ) * 1 + 1;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myPane.XAxis.Type = AxisType.DateAsOrdinal;
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

			for ( int i = 0; i < 10; i++ )
			{
				double r = rand.NextDouble() * 10.0 + 10.0;
				PointPair pt = new PointPair( PointPair.Missing, r, "r = " + r.ToString( "f1" ) );
				rpl.Add( pt );
			}
			LineItem curve = myPane.AddCurve( "test", rpl, Color.Blue, SymbolType.Default );

			for ( int i = 0; i < 10; i++ )
			{
				LineObj line = new LineObj( 0, 0, (float)rpl[i].X, (float)rpl[i].Y );
				line.Line.Color = Color.LightBlue;
				line.ZOrder = ZOrder.E_BehindCurves;
				myPane.GraphObjList.Add( line );
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

		// Traditional Open-High-Low-Close Bar chart
		private void CreateGraph_OHLCBar( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			myPane.Title.Text = "OHLC Chart Demo";
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

			//OHLCBarItem myCurve = myPane.AddOHLCBar( "trades", spl, Color.Black );
			OHLCBarItem myCurve = myPane.AddOHLCBar( "trades", spl, Color.Blue );
			//myCurve.Bar.Size = 20;
			myCurve.Bar.IsAutoSize = true;
			//myCurve.Bar.PenWidth = 2;
			//myCurve.Bar.IsOpenCloseVisible = false;

			Fill fill = new Fill( Color.Red, Color.Yellow, Color.Blue );
			fill.RangeMin = 40;
			fill.RangeMax = 70;
			fill.Type = FillType.GradientByY;
			myCurve.Bar.GradientFill = fill;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			//myPane.XAxis.Type = AxisType.Date;
			//myPane.XAxis.Scale.MajorStep = 1.0;

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			//BoxObj box = new BoxObj( 4, 60, 5, 50000 );
			//myPane.GraphObjList.Add( box );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();

		}

		public void CreateGraph_OHLCBarGradient( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the title and axis labels   
			myPane.Title.Text = "OHLC Chart Demo";
			myPane.XAxis.Title.Text = "Date";
			myPane.YAxis.Title.Text = "Price";

			//Load a StockPointList with random data.........................
			StockPointList spl = new StockPointList();
			Random rand = new Random();

			// First day is jan 1st
			XDate xDate = new XDate( 2006, 1, 1 );
			double open = 50.0;
			double prevClose = 0;

			// Loop to make 50 days of data
			for ( int i = 0; i < 50; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;

				// Create a StockPt instead of a PointPair so we can carry 6 properties
				StockPt pt = new StockPt( x, hi, low, open, close, 100000 );

				//if price is increasing color=black, else color=red
				pt.ColorValue = close > prevClose ? 2 : 1;
				spl.Add( pt );

				prevClose = close;
				open = close;
				// Advance one day
				xDate.AddDays( 1.0 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			// Setup the gradient fill...
			// Use Red for negative days and black for positive days
			Color[] colors = { Color.Red, Color.Black };
			Fill myFill = new Fill( colors );
			myFill.Type = FillType.GradientByColorValue;
			myFill.SecondaryValueGradientColor = Color.Empty;
			myFill.RangeMin = 1;
			myFill.RangeMax = 2;

			//Create the OHLC and assign it a Fill
			OHLCBarItem myCurve = myPane.AddOHLCBar( "Price", spl, Color.Empty );
			myCurve.Bar.GradientFill = myFill;
			myCurve.Bar.IsAutoSize = true;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			//myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );
			myPane.Title.FontSpec.Size = 20.0f;
			myPane.XAxis.Title.FontSpec.Size = 18.0f;
			myPane.XAxis.Scale.FontSpec.Size = 16.0f;
			myPane.YAxis.Title.FontSpec.Size = 18.0f;
			myPane.YAxis.Scale.FontSpec.Size = 16.0f;
			myPane.Legend.IsVisible = false;

			//			BoxObj box = new BoxObj( 4.5, 0.0, 1.0, 1.0, Color.Transparent, 
			//					Color.FromArgb( 100, Color.LightBlue ) );
			//			box.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
			//			myPane.GraphObjList.Add( box );

			// Tell ZedGraph to calculate the axis ranges
			zgc.AxisChange();
			zgc.Invalidate();
		}

		// Make a masterpane with 3 charts
		// Top = OHLC Bar Chart
		// Mid = Volume Chart
		// Bot = Price Change
		public void CreateGraph_OHLCBarMaster( ZedGraphControl zgc )
		{
			// ================================================
			// First, set up some lists with random data...
			// ================================================
			StockPointList spl = new StockPointList();
			PointPairList volList = new PointPairList();
			PointPairList changeList = new PointPairList();

			Random rand = new Random();

			// First day is jan 1st
			XDate xDate = new XDate( 2006, 1, 1 );
			double open = 50.0;
			double prevClose = 50.0;
			const int numDays = 365;

			// Loop to make 365 days of data
			for ( int i = 0; i < numDays; i++ )
			{
				double x = xDate.XLDate;
				//double close = open + rand.NextDouble() * 10.0 - 5.0;
				double close = open * ( 0.95 + rand.NextDouble() * 0.1 );
				//double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				//double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;
				double hi = Math.Max( open, close ) * ( 1.0 + rand.NextDouble() * 0.05 );
				double low = Math.Min( open, close ) * ( 0.95 + rand.NextDouble() * 0.05 );
				double vol = 25.0 + rand.NextDouble() * 100.0;
				double change = close - prevClose;

				// Create a StockPt instead of a PointPair so we can carry 6 properties
				StockPt pt = new StockPt( x, hi, low, open, close, vol );

				//if price is increasing color=black, else color=red
				pt.ColorValue = close > prevClose ? 2 : 1;
				spl.Add( pt );

				volList.Add( x, vol );
				changeList.Add( x, change );

				prevClose = close;
				open = close;
				// Advance one day
				xDate.AddDays( 1.0 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			// ================================================
			// Create 3 GraphPanes to display the data
			// ================================================

			// get a reference to the masterpane
			MasterPane master = zgc.MasterPane;

			// The first chart is already in the MasterPane, so add the other two charts
			master.Add( new GraphPane() );
			master.Add( new GraphPane() );

			// ================================================
			// The first pane is an OHLCBarItem
			// ================================================

			// Get a reference to the pane
			GraphPane pane = master[0];

			// Set the title and axis labels   
			pane.Title.Text = "Open-High-Low-Close History";
			pane.XAxis.Title.Text = "Date";
			pane.YAxis.Title.Text = "Price";

			// Setup the gradient fill...
			// Use Red for negative days and black for positive days
			Color[] colors = { Color.Red, Color.Black };
			Fill myFill = new Fill( colors );
			myFill.Type = FillType.GradientByColorValue;
			myFill.SecondaryValueGradientColor = Color.Empty;
			myFill.RangeMin = 1;
			myFill.RangeMax = 2;

			//Create the OHLC and assign it a Fill
			OHLCBarItem ohlcCurve = pane.AddOHLCBar( "Price", spl, Color.Empty );
			ohlcCurve.Bar.GradientFill = myFill;
			ohlcCurve.Bar.IsAutoSize = true;
			// Create a JapaneseCandleStick
			//JapaneseCandleStickItem jcsCurve = pane.AddJapaneseCandleStick( "Price", spl );
			//jcsCurve.Stick.IsAutoSize = false;

			// ================================================
			// The second pane is a regular BarItem to show daily volume
			// ================================================

			// Get a reference to the pane
			pane = master[1];

			// Set the title and axis labels   
			pane.Title.Text = "Daily Volume";
			pane.XAxis.Title.Text = "Date";
			pane.YAxis.Title.Text = "Volume, thousands";

			BarItem volBar = pane.AddBar( "Volume", volList, Color.Blue );

			// ================================================
			// The third pane is a LineItem to show daily price change
			// ================================================

			// Get a reference to the pane
			pane = master[2];

			// Set the title and axis labels   
			pane.Title.Text = "Price Change";
			pane.XAxis.Title.Text = "Date";
			pane.YAxis.Title.Text = "Price Change, $";

			LineItem changeCurve = pane.AddCurve( "Price Change", changeList, Color.Green, SymbolType.None );

			// ================================================
			// These settings are common to all three panes
			// ================================================

			foreach ( GraphPane paneT in master.PaneList )
			{
				// Use DateAsOrdinal to skip weekend gaps
				paneT.XAxis.Type = AxisType.DateAsOrdinal;
				// Use only visible data to define Y scale range
				paneT.IsBoundedRanges = true;
				// Define a minimum buffer space to the axes can be aligned
				paneT.YAxis.MinSpace = 80;
				paneT.Y2Axis.MinSpace = 50;

				// pretty it up a little
				paneT.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
				paneT.Title.FontSpec.Size = 20.0f;
				paneT.XAxis.Title.FontSpec.Size = 18.0f;
				paneT.XAxis.Scale.FontSpec.Size = 16.0f;
				paneT.YAxis.Title.FontSpec.Size = 18.0f;
				paneT.YAxis.Scale.FontSpec.Size = 16.0f;
				paneT.Legend.IsVisible = false;
				paneT.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

				// Set the initial scroll position and range
				// Note that the min and max for DateAsOrdinal scale will be ordinal values, not dates
				paneT.XAxis.Scale.Min = 1.0;
				// default range is 30 days
				paneT.XAxis.Scale.Max = 30.0;

			}

			// ================================================
			// Set up the MasterPane Layout
			// ================================================

			// Make sure that fonts and dimensions are the same for all three charts
			master.IsCommonScaleFactor = true;

			// Show the masterpane title
			master.Title.IsVisible = true;
			master.Title.Text = "Wacky Widget Company Stock Performance";
			master.Fill = new Fill( Color.White, Color.SlateBlue, 45.0f );

			// Leave a margin around the masterpane, but only a small gap between panes
			master.Margin.All = 10;
			master.InnerPaneGap = 5;

			using ( Graphics g = this.CreateGraphics() )
			{

				master.SetLayout( g, PaneLayout.SingleColumn );

				// Synchronize the Axes
				zgc.IsAutoScrollRange = true;
				zgc.IsShowHScrollBar = true;
				zgc.IsSynchronizeXAxes = true;
				// Scale range will extend about 1 day before and after the actual data range
				zgc.ScrollGrace = 1.0 / numDays;
			}

			// Tell ZedGraph to calculate the axis ranges
			zgc.AxisChange();

			//			master[0].XAxis.Scale.Min = new XDate( 2006, 1, 1 ).XLDate;
			//			master[0].XAxis.Scale.Max = master[0].XAxis.Scale.Min + 30.0;
			//			master[1].XAxis.Scale.Min = new XDate( 2006, 1, 1 ).XLDate;
			//			master[1].XAxis.Scale.Max = master[1].XAxis.Scale.Min + 30.0;
			//			master[2].XAxis.Scale.Min = new XDate( 2006, 1, 1 ).XLDate;
			//			master[2].XAxis.Scale.Max = master[2].XAxis.Scale.Min + 30.0;

			//zgc.ScrollDoneEvent += new ZedGraphControl.ScrollDoneHandler( zgc_ScrollDoneEvent );
			zgc.ScrollProgressEvent += new ZedGraphControl.ScrollProgressHandler( zgc_ScrollProgressEvent );
		}

		void zgc_ScrollProgressEvent( ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState,
						ZoomState newState )
		{
			//this.toolStripStatusLabel1.Text = sender.GraphPane.XAxis.Scale.Max.ToString();
			// When scroll action is finished, recalculate the axis ranges
			sender.AxisChange();
			sender.Refresh();
		}

		void zgc_ScrollDoneEvent( ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState,
						ZoomState newState )
		{
			// When scroll action is finished, recalculate the axis ranges
			sender.AxisChange();
			sender.Refresh();
		}

		private void CreateGraph_DateAsOrdinal2( ZedGraphControl z1 )
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

			for ( int i = 0; i < 100; i++ )
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
				//if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
				//	xDate.AddDays( 2.0 );
			}

			//			JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick( "trades", spl );
			//			myCurve.Stick.IsAutoSize = true;
			//			myCurve.Stick.Color = Color.Blue;

			LineItem myCurve = myPane.AddCurve( "trades", spl, Color.Red );

			// Use DateAsOrdinal to skip weekend gaps
			//myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Type = AxisType.Date;
			//myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );
			myCurve.Line.IsOptimizedDraw = false;
			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();

			z1.Invalidate();

			z1.PointValueEvent += new ZedGraphControl.PointValueHandler( z1_PointValueEvent );
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

			for ( int i = 0; i < 100; i++ )
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

			z1.PointValueEvent += new ZedGraphControl.PointValueHandler( z1_PointValueEvent );
		}

		// Call this method from the Form_Load method, passing your ZedGraphControl
		public void CreateGraph_JapaneseCandleStickDemo( ZedGraphControl zgc )
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
				//xDate.AddDays( 1 + 0.4 * rand.NextDouble() - 0.2 );
				xDate.AddDays( 1 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick( "trades", spl );
			myCurve.Stick.IsAutoSize = true;
			myCurve.Stick.Color = Color.Blue;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			//myPane.XAxis.Type = AxisType.Date;
			//myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			// Tell ZedGraph to calculate the axis ranges
			zgc.AxisChange();
			zgc.Invalidate();

		}

		private void CreateGraph_JapaneseCandleStickDemo2( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

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

			zgc.AxisChange();
		}

		Timer myTimer;

		private void MyTimer_Tick( object sender, EventArgs e )
		{
			// Get the first CurveItem in the graph
			LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
			// Get the PointPairList
			PointPairList list = curve.Points as PointPairList;
			//list.Add( xvalue, (double)cpuUsagePerformanceCounter.NextValue() );
			list.Add( list.Count + 1, Math.Sin( list.Count / 6.0 ) );
			zedGraphControl1.AxisChange();
			Refresh();
		}

		// Basic curve test - Line Color Gradient
		private void CreateGraph_LineColorGradient( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			PointPairList list = new PointPairList();
			const int count = 36;

			for ( int i = 0; i < count; i++ )
			{
				double x = i + 1;

				double y = 5 * Math.Sin( (double)i * Math.PI * 3 / count );

				list.Add( x, y, y < 0 ? -1.0 : 1.0 );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			Fill fill = new Fill( Color.Red, Color.Green );
			fill.RangeMin = -1;
			fill.RangeMax = 1;
			fill.Type = FillType.GradientByZ;
			myCurve.Line.GradientFill = fill;
			myCurve.Symbol.IsVisible = false;
			myCurve.Line.Fill = fill;

			myCurve.Line.StepType = StepType.ForwardStep;

			zgc.AxisChange();
		}



		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinear( ZedGraphControl z1 )
		{
			//			Color rgb = Color.FromArgb( 123, 240, 098 );
			//			HSBColor hsb = HSBColor.FromRGB( rgb );
			//			Color rgb2 = hsb.ToRGB();


			GraphPane myPane = z1.GraphPane;
			z1.IsEnableSelection = true;
			z1.IsZoomOnMouseCenter = true;

			Selection.Fill = new Fill( Color.Red );
			Selection.Line.Color = Color.Red;
			Selection.Line.DashOn = 1;
			Selection.Line.DashOff = 1;
			Selection.Line.Width = 3;

			//myTimer = new Timer();
			//myTimer.Enabled = true;
			//myTimer.Tick += new EventHandler( MyTimer_Tick );
			//myTimer.Interval = 500;
			//myTimer.Start();

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			const int count = 10;

			for ( int i = 0; i < count; i++ )
			{
				double x = XDate.CalendarDateToXLDate( 2006, 4, 15, 3+i, 0, 0 );

				double y = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.2 ) );

				string tag;
				tag = "Point #" + i.ToString() + "\n" + XDate.ToString( x, "g" ) + "\n" + y.ToString() +
					"Line 4\nLine 5\nLine 6";

				if ( i == 10 )
					y = PointPair.Missing;
				list.Add( x, y, i / 36.0, tag );
				list2.Add( x, y + 50, ( count - i ) * 70.0 );
				list3.Add( x, y + 150, i / 36.0 );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "curve2", list2, Color.Red, SymbolType.Circle );
			Fill fill = new Fill( Color.Red, Color.Yellow, Color.Blue );
			fill.RangeMin = 300;
			fill.RangeMax = 700;
			fill.Type = FillType.GradientByColorValue;
			myCurve2.Line.GradientFill = fill;
			LineItem myCurve3 = myPane.AddCurve( "curve3", list3, Color.Green, SymbolType.Square );

			myPane.IsIgnoreMissing = true;
			//myPane.XAxis.Type = AxisType.Ordinal;

			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.Scale.BaseTic = XDate.CalendarDateToXLDate( 2006, 4, 15, 3, 0, 0 );
			myPane.XAxis.Scale.MajorStep = 1;
			myPane.XAxis.Scale.MajorUnit = DateUnit.Hour;
			//myPane.XAxis.Scale.Format

			myPane.XAxis.Scale.Min = XDate.CalendarDateToXLDate( 2006, 4, 15, 0, 0, 0 );

			//The first tic that appears on the x axis is not on the first x value with
			//Minute == 0 and Second == 0, but on the first x value at midnight (i.e.
			//Hour == 0 and Minute == 0 and Second == 0).

			//When using other values for MajorStep, the first tic is always at the fist
			//x value at midnight.

			myCurve.Symbol.Fill = new Fill( Color.White, Color.Red );
			myCurve.Symbol.Fill.Type = FillType.GradientByZ;
			myCurve.Symbol.Fill.RangeMin = 0;
			myCurve.Symbol.Fill.RangeMax = 4;
			myCurve.Symbol.Fill.RangeDefault = 0;
			myCurve.Symbol.Fill.SecondaryValueGradientColor = Color.Empty;

			for ( int i = 0; i < count; i++ )
			{
				PointPair pt = myCurve.Points[i];

				TextObj text = new TextObj( pt.Y.ToString( "f2" ), pt.X, pt.Y,
					CoordType.AxisXYScale, AlignH.Right, AlignV.Bottom );
				text.ZOrder = ZOrder.A_InFront;
				text.FontSpec.Border.IsVisible = false;
				text.FontSpec.Fill.IsVisible = false;

				myPane.GraphObjList.Add( text );
			}


			foreach ( GraphObj obj in myPane.GraphObjList )
			{
				if ( obj is TextObj )
					( obj as TextObj ).FontSpec.Angle = 90;
			}

			z1.GraphPane.IsBoundedRanges = false;
			z1.AxisChange();

			z1.PointValueEvent += new ZedGraphControl.PointValueHandler( z1_PointValueEvent );

			box = new BoxObj( 0, 0, 1, 10, Color.Empty, Color.FromArgb( 200, Color.LightGreen ) );
			box.Location.CoordinateFrame = CoordType.XChartFractionYScale;
			box.IsVisible = false;
			myPane.GraphObjList.Add( box );

		}


		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinearSimple( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			const int count = 10;

			for ( int i = 0; i < count; i++ )
			{
				double x = i;

				double y = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.2 ) );

				list.Add( x, y, i / 36.0 );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );


			//myPane.XAxis.Scale.MajorStep = 1e-301;

			z1.AxisChange();


		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinearSimpleUserSymbol( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			const int count = 10;

			for ( int i = 0; i < count; i++ )
			{
				double x = i;

				double y = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.2 ) );

				list.Add( x, y, i / 36.0 );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.UserDefined );
			LineItem myCurve2 = myCurve.Clone();
			GraphicsPath path = new GraphicsPath();
			path.AddLine( -0.5f, -0.5f, 0.5f, 0.5f );
			path.AddLine( 0.5f, 0.5f, 0.5f, -0.5f );
			path.AddLine( 0.5f, -0.5f, -0.5f, 0.5f );
			path.AddLine( -0.5f, 0.5f, -0.5f, -0.5f );
			myCurve.Symbol.UserSymbol = path;
			myCurve.Symbol.Fill = new Fill( Color.LightGoldenrodYellow );

			//myPane.XAxis.Scale.MajorStep = 1e-301;

			z1.AxisChange();


		}

		BoxObj box;

		private string z1_PointValueEvent( ZedGraphControl z1, GraphPane pane,
			CurveItem curve, int index )
		{
			StockPt spt = curve.Points[index] as StockPt;
			if ( spt != null )
			{
				return XDate.ToString( spt.Date, "g" ) + "\n" +
						"Open = " + spt.Open.ToString( "f2" ) + "\n" +
						"High = " + spt.High.ToString( "f2" ) + "\n" +
						"Low = " + spt.Low.ToString( "f2" ) + "\n" +
						"Close = " + spt.Close.ToString( "f2" );
			}
			else
				return "Invalid Data";

			PointPair pt = curve.Points[index];

			box.IsVisible = true;
			box.Location.Y = pt.Y + 5;

			z1.Refresh();

			return "Point #" + index.ToString() + "\n" + XDate.ToString( pt.X, "g" ) + "\n" +
				pt.Y.ToString() + "Line 4\nLine 5\nLine 6\nLine 7";
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
			LineItem myCurve2 = myPane.AddCurve( "Here's a really long curve name", list2, Color.Red, SymbolType.Diamond );
			LineItem myCurve3 = myPane.AddCurve( "curve 3", list3, Color.Green, SymbolType.Diamond );

			myPane.Legend.Position = LegendPos.Bottom;
			myPane.Legend.IsHStack = false;
			//myPane.Legend.IsShowLegendSymbols = false;

			z1.AxisChange();

		}

		public string XScaleFormatEvent( GraphPane pane, Axis axis, double val, int index )
		{
			return val.ToString( "f2" ) + " cm";
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
			myPane.YAxis.Scale.IsUseTenPower = false;

			myPane.YAxis.Scale.Min = 1000;
			myPane.YAxis.Scale.MinAuto = false;
			myPane.YAxis.Scale.MajorStep = 1;
			z1.AxisChange();

			//myPane.XAxis.Scale.MajorStep = 100;

			//myPane.YAxis.ScaleFormatEvent +=new Axis.ScaleFormatHandler( YScaleLog_FormatEvent );
		}

		public string YScaleLog_FormatEvent( GraphPane pane, Axis axis, double val, int index )
		{
			return "( Y= " + val.ToString() + ")";
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

		// Basic curve test with no data Range
		private void CreateGraph_FlatLine( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			const double val = -2;

			PointPairList list = new PointPairList();
			list.Add( 1, val );
			list.Add( 2, val );
			list.Add( 3, val );
			list.Add( 4, val );
			list.Add( 5, val );
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Black, SymbolType.Circle );

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
			for ( int i = 0; i < 36; i += 5 )
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

			int count = 36;

			myPane.Title.Text = "Case #6";
			myPane.XAxis.Title.Text = "Time, Days";
			myPane.YAxis.Title.Text = "Rate, m/s";

			myPane.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
			myPane.BaseDimension = 6.0F;

			// Make up some data arrays based on the Sine function
			double angle = 0;
			double radius = 5;
			PointPairList list = new PointPairList();

			for ( int i = 0; i < count; i++ )
			{
				double x = radius * Math.Cos( angle );
				double y = radius * Math.Sin( angle );
				list.Add( 10.0, 10.0 );
				list.Add( 10.0 + x, 10.0 + y );
				list.Add( PointPair.Missing, PointPair.Missing );
				angle += 2 * Math.PI / count;
			}

			LineItem myCurve = myPane.AddCurve( "Type 5", list, Color.Pink, SymbolType.Triangle );
			//myCurve.Symbol.Fill = new Fill( Color.White );
			//myCurve.Line.IsOptimizedDraw = false;

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
			list.Add( mynow2.AddSeconds( 10 ).ToOADate(), 25 );
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
			myPane.XAxis.Scale.Max = mynow.AddSeconds( 30 ).ToOADate();
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

			for ( int i = 0; i < 1000; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;

				StockPt pt = new StockPt( x, hi, low, open, close, 100000 );
				spl.Add( pt );

				open = close;
				if ( xDate.DateTime.Hour < 23 )
					xDate.AddHours( 1.0 );
				else
				{
					// Advance one day 
					xDate.AddHours( 1.0 );
					// but skip the weekends 
					if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
						xDate.AddDays( 2.0 );
				}
			}

			JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick( "trades", spl );
			myCurve.Stick.IsAutoSize = true;
			myCurve.Stick.Color = Color.Blue;

			// Use DateAsOrdinal to skip weekend gaps 
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );
			myPane.XAxis.Scale.Format = "dd-MMM-yy hh:mm";

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
			for ( int i = 1; i < 700; i++ )
			{
				x++;
				y = Math.Sin( i * Math.PI / 45.0 ) * 16.0;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "", list, Color.Blue, SymbolType.None );
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

		private void CreateGraph_junk8( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			double[] x6 = { 65, 80 };
			double[] y6 = { 5000, 5000 };
			LineItem lineAOW = myPane.AddCurve( "AOW", x6, y6, Color.Blue );
			lineAOW.Line.Width = 2;
			lineAOW.Line.Fill = new Fill( Color.LightGreen );
			lineAOW.Symbol.IsVisible = false;
			myPane.YAxis.Scale.Min = 4990;
			myPane.YAxis.Scale.Max = 5010;
		}

		public void CreateGraph_junk9( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			myPane.BarSettings.Base = BarBase.Y;
			PointPairList ppl = new PointPairList();
			PointPairCV pp1 = new PointPairCV( 20, 25, 10 );
			PointPairCV pp2 = new PointPairCV( 30, 30, 20 );
			PointPairCV pp3 = new PointPairCV( 40, 35, 15 );
			PointPairCV pp4 = new PointPairCV( 50, 40, 30 );
			PointPairCV pp5 = new PointPairCV( 60, 45, 10 );
			pp1.ColorValue = 1;
			pp2.ColorValue = 2;
			pp3.ColorValue = 3;
			pp4.ColorValue = 4;
			pp5.ColorValue = 5;
			ppl.Add( pp1 );
			ppl.Add( pp2 );
			ppl.Add( pp3 );
			ppl.Add( pp4 );
			ppl.Add( pp5 );

			XDate myXDate = new XDate( 2007, 5, 6 );
			double poop = myXDate.XLDate;

			//			double cv = ( ppl[3] as PointPairCV ).ColorValue;

			Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Cyan, Color.LightGreen };
			Fill myFill = new Fill( colors );
			myFill.Type = FillType.GradientByColorValue;
			myFill.SecondaryValueGradientColor = Color.White;
			myFill.RangeMin = 1;
			myFill.RangeMax = 5;

			HiLowBarItem myCurve = myPane.AddHiLowBar( "Curve 1", ppl, Color.Empty );
			myCurve.Bar.Fill = myFill;
			myCurve.Bar.Size = 20;

		}

		private void CreateGraph_junk10( ZedGraphControl zgc )
		{
			//Create a Masterpane
			MasterPane myMaster = zgc.MasterPane;
			myMaster.SetLayout( zgc.CreateGraphics(), false, new int[] { 3 }, new float[] { 1, 1, 4 } );
			myMaster.IsFontsScaled = false;

			myMaster.PaneList.Clear();

			GraphPane myPaneII = new GraphPane( new Rectangle( new Point( 0, 0 ),
				new Size( 400, 350 / 2 ) ), "", "", "" );
			GraphPane myPane = new GraphPane( new Rectangle( new Point( 0, 0 ),
				new Size( 400, 350 / 2 ) ), "", "", "" );

			myPane.Margin.All = 0;
			myPaneII.Margin.All = 0;
			myMaster.Margin.All = 0;

			myPane.Border.IsVisible = false;
			myPaneII.Border.IsVisible = false;
			myMaster.Border.IsVisible = false;


			//myPane 
			//General
			myPane.Title.Text = "TLV-Resolver";
			myPane.Title.FontSpec.Size = 50;
			myPane.Legend.FontSpec.Size = 50;

			//XAxis
			myPane.XAxis.Title.Text = "Date";
			myPane.XAxis.Title.IsVisible = false;

			myPane.XAxis.Type = AxisType.Date;
			//myPane.XAxis.Scale.MinAuto = true;
			//myPane.XAxis.Scale.MinorUnit = DateUnit.Millisecond;
			//myPane.XAxis.Scale.MajorUnit = DateUnit.Millisecond;
			//myPane.XAxis.Scale.Format = "mm:ss.fff";
			myPane.XAxis.Scale.FontSpec.Size = 50;
			//myPane.XAxis.Scale.MajorStep = 5;
			//myPane.XAxis.Scale.MinorStep = 0.5;
			myPane.XAxis.Scale.IsVisible = true;

			myPane.XAxis.IsVisible = true;
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.XAxis.MinorGrid.IsVisible = true;
			myPane.XAxis.Scale.IsPreventLabelOverlap = false;

			//YAxis
			myPane.YAxis.Title.Text = "Status";
			myPane.YAxis.Scale.Max = 1;
			myPane.YAxis.Scale.FontSpec.Size = 50;
			myPane.YAxis.Scale.MajorStep = 2;
			myPane.YAxis.Scale.MinorStep = 1;
			myPane.YAxis.IsVisible = true;
			myPane.YAxis.Title.FontSpec.Size = 50;
			myPane.YAxis.MinorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;



			//2.Pane
			//General
			myPaneII.Title.Text = "COM-Resolver";
			myPaneII.Title.IsVisible = true;
			myPaneII.Title.FontSpec.Size = 50;
			myPaneII.Legend.FontSpec.Size = 50;


			//XAxis
			myPaneII.XAxis.Title.Text = "Date";
			myPaneII.XAxis.Title.IsVisible = false;
			myPaneII.XAxis.Type = AxisType.Date;
			//myPaneII.XAxis.Scale.MinorUnit = DateUnit.Millisecond;
			//myPaneII.XAxis.Scale.MajorUnit = DateUnit.Millisecond;
			//myPaneII.XAxis.Scale.Format = "mm:ss.fff";

			myPaneII.XAxis.MajorGrid.IsVisible = true;
			myPaneII.XAxis.MinorGrid.IsVisible = true;
			//myPaneII.XAxis.Scale.MajorStep = 5;
			//myPaneII.XAxis.Scale.MinorStep = 0.5;
			myPaneII.XAxis.Scale.FontSpec.Size = 50;
			myPaneII.XAxis.IsVisible = true;
			myPaneII.XAxis.Scale.IsVisible = true;

			//YAxis
			myPaneII.YAxis.Title.Text = "Status";
			myPaneII.YAxis.Title.FontSpec.Size = 50;
			myPaneII.YAxis.Scale.MajorStep = 1;
			myPaneII.YAxis.Scale.MinorStep = 1;
			myPaneII.YAxis.Scale.Max = 1;
			myPaneII.YAxis.Scale.FontSpec.Size = 50;





			// Make up some data arrays based on the Sine function
			double x, x2;
			int y1 = 0, y2 = 1;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i = 0; i < 1000; i++ )
			{
				int year = DateTime.Now.Year, month = DateTime.Now.Month, day = DateTime.Now.Day, hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second, mil = DateTime.Now.Millisecond;

				//System.Threading.Thread.Sleep( 10 );

				//Getting just time value exapmle(0.1248874895d)
				double myDate = DateTime.Now.ToOADate();
				double mySubtrahend = Convert.ToDouble( DateTime.Now.ToOADate().ToString().Trim().Substring( 0, myDate.ToString().Trim().IndexOf( "." ) ) );
				myDate = myDate - mySubtrahend;

				x = myDate;

				x2 = DateTime.FromOADate( myDate ).AddMilliseconds( 347 ).ToOADate();

				switch ( y1 )
				{
					case 0:
						y1 = 1;
						break;
					case 1:
						y1 = 0;
						break;
					default:
						y1 = 0;
						break;
				}

				switch ( y2 )
				{
					case 0:
						y2 = 1;
						break;
					case 1:
						y2 = 0;
						break;
					default:
						y2 = 0;
						break;
				}

				list1.Add( x, y1, DateTime.FromOADate( x ).ToLongTimeString() + ";" + DateTime.FromOADate( x ).Millisecond.ToString() );
				list2.Add( x2, y2, DateTime.FromOADate( x2 ).ToLongTimeString() + ";" + DateTime.FromOADate( x2 ).Millisecond.ToString() );
			}

			// Generate a red curve with diamond
			// symbols, and "Laser" in the legend
			LineItem myCurve = myPane.AddCurve( "Laser",
				list1, Color.Red, SymbolType.Diamond );

			//myCurve.Label.FontSpec = new FontSpec( "Arial", 20, Color.Black, true, false, false );
			myPane.Legend.FontSpec.Size = 100;

			// Generate a blue curve with circle
			// symbols, and "Shutter" in the legend
			LineItem myCurveII = myPaneII.AddCurve( "Shutter", list2, Color.Blue, SymbolType.Circle );

			zgc.MasterPane.InnerPaneGap = 0;
			myMaster.PaneList.Add( myPane );
			myMaster.PaneList.Add( myPaneII );

			zgc.IsShowHScrollBar = false;

			zgc.IsSynchronizeXAxes = true;
			zgc.IsSynchronizeYAxes = true;

			zgc.IsEnableHZoom = true;

			zgc.IsEnableVZoom = false;
			zgc.IsEnableVPan = false;

			//zgc.ContextMenu.MenuItems.Clear();

			zgc.IsZoomOnMouseCenter = false;
			zgc.AxisChange();
		}

		public void CreateGraph_junk11( ZedGraphControl zgc )
		{
			string[] labels = { "BLACKBERRY SERVICES", "DNS ADD/CHANGE", "FILE SERVER CONSOLIDATED", "FIREWALL RULE CHANGE", "GROUPWISE--CONSOLIDATED", "INFOPAC SERVICES", "TELWFAC" };

			//PaneBase pb = new PaneBase(); 

			GraphPane myGraphPane = zgc.GraphPane;
			myGraphPane.XAxis.Title.Text = "SLA's";
			myGraphPane.YAxis.Title.Text = "Hours";




			//Set bars 

			BarItem myBar;
			BarItem myBar2;


			//HoldGraphValues();
			double[] axValues = { 0.5, 1.5, 2.5, 3.5, 4.5, 5.5, 6.5 };
			double[] ayValues1 = { 1, 2, 3, 4, 5, 6, 7 };
			double[] ayValues2 = { 2, 3, 4, 5, 6, 7, 8 };
			double[] ayValues3 = { 3, 4, 5, 6, 7, 8, 9 };
			double[] ayValues4 = { 4, 5, 6, 7, 8, 9, 10 };

			PointPairList List1 = new PointPairList( ayValues1, axValues );
			PointPairList List2 = new PointPairList( ayValues2, axValues );
			PointPairList List3 = new PointPairList( ayValues3, axValues );
			PointPairList List4 = new PointPairList( ayValues4, axValues );



			myBar = myGraphPane.AddBar( "Goals", List1, Color.Red );
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );

			myBar2 = myGraphPane.AddBar( "Average Time", List2, Color.Blue );
			myBar2.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 00 );

			//myBar.GetBarWidth(myGraphPane); 
			//myBar2.GetBarWidth(myGraphPane); 



			// //set X axis 

			myGraphPane.X2Axis.IsVisible = false;
			myGraphPane.X2Axis.IsAxisSegmentVisible = false;
			myGraphPane.XAxis.Scale.Min = 0;
			myGraphPane.XAxis.Scale.MaxAuto = true;
			//myGraphPane.XAxis.Scale.TextLabels = labels; 
			myGraphPane.XAxis.Scale.MinAuto = false;
			//myGraphPane.XAxis.Scale.Min = 0; 
			//myGraphPane.XAxis.Type = AxisType.Text; 
			//myGraphPane.XAxis.Scale.FontSpec.Size = 6; 
			//myGraphPane.XAxis.Scale.FontSpec.Angle = 85; 
			//myGraphPane.XAxis.MajorTic.IsCrossOutside=false; 
			//myGraphPane.XAxis.MinorTic.IsCrossOutside = false; 
			myGraphPane.XAxis.MajorGrid.IsZeroLine = true;
			zgc.AxisChange();
			myGraphPane.XAxis.Scale.Max += myGraphPane.YAxis.Scale.MajorStep;

			// //set Y axis 
			myGraphPane.YAxis.Scale.MajorStep = 1;
			myGraphPane.YAxis.Scale.Min = 0;
			myGraphPane.YAxis.Scale.MaxAuto = true;
			myGraphPane.YAxis.Scale.MinAuto = false;
			myGraphPane.YAxis.Scale.FontSpec.Size = 6;
			myGraphPane.YAxis.Scale.FontSpec.Angle = 90;
			myGraphPane.YAxis.Scale.TextLabels = labels;
			myGraphPane.YAxis.Type = AxisType.Text;
			myGraphPane.Chart.IsRectAuto = true;
			myGraphPane.Y2Axis.IsVisible = false;
			myGraphPane.Y2Axis.IsAxisSegmentVisible = false;
			//myGraphPane.YAxis.Scale.Max += myGraphPane.YAxis.Scale.MajorStep; 
			myGraphPane.YAxis.MajorGrid.IsZeroLine = true;
			myGraphPane.BarSettings.Base = BarBase.Y;


			myGraphPane.YAxis.Scale.FontSpec.Size = 9;

			myGraphPane.YAxis.MajorGrid.IsZeroLine = true;

			myGraphPane.ScaledPenWidth( 1, 8 );



			myGraphPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 255, 255 ), 90f );
			myGraphPane.Fill = new Fill( Color.FromArgb( 250, 250, 255 ) );

			zgc.AxisChange(); 
			BarItem.CreateBarLabels( myGraphPane, false, "f2", "Arial", 9, Color.Black, false, false, false );
			//myMaster.Add(myGraphPane); 


			//zgc.AxisChange(); 

			//SetSize( zd );
			//Controls.Add(zd); 
		}


		// Call this method from the Form_Load method, passing your ZedGraphControl
		public void CreateGraph_LabeledPointsDemo( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Demo of Labeled Points";
			myPane.XAxis.Title.Text = "Time, Seconds";
			myPane.YAxis.Title.Text = "Pressure, Psia";

			// Build a PointPairList with points based on Sine wave
			PointPairList list = new PointPairList();
			const int count = 15;
			for ( int i = 0; i < count; i++ )
			{
				double x = i + 1;

				double y = 21.1 * ( 1.0 + Math.Sin( (double)i * 0.15 ) );

				list.Add( x, y );
			}

			// Hide the legend
			myPane.Legend.IsVisible = false;

			// Add a curve
			LineItem curve = myPane.AddCurve( "label", list, Color.Red, SymbolType.Circle );
			curve.Line.Width = 2.0F;
			curve.Line.IsAntiAlias = true;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 7;

			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, Color.ForestGreen ), 45.0F );

			// Offset Y space between point and label
			// NOTE:  This offset is in Y scale units, so it depends on your actual data
			const double offset = 1.0;

			// Loop to add text labels to the points
			for ( int i = 0; i < count; i++ )
			{
				// Get the pointpair
				PointPair pt = curve.Points[i];

				// Create a text label from the Y data value
				TextObj text = new TextObj( pt.Y.ToString( "f2" ), pt.X, pt.Y + offset,
					CoordType.AxisXYScale, AlignH.Left, AlignV.Center );
				text.ZOrder = ZOrder.A_InFront;
				// Hide the border and the fill
				text.FontSpec.Border.IsVisible = false;
				text.FontSpec.Fill.IsVisible = false;
				//text.FontSpec.Fill = new Fill( Color.FromArgb( 100, Color.White ) );
				// Rotate the text to 90 degrees
				text.FontSpec.Angle = 90;

				myPane.GraphObjList.Add( text );
			}

			// Leave some extra space on top for the labels to fit within the chart rect
			myPane.YAxis.Scale.MaxGrace = 0.2;

			// Calculate the Axis Scale Ranges
			zgc.AxisChange();
		}

		public void CreateGraph_LineWithBandDemo( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Line Graph with Band Demo";
			myPane.Fill = new Fill( Color.LightBlue, Color.White );
			myPane.XAxis.Title.Text = "Sequence";
			myPane.YAxis.Title.Text = "Temperature, C";

			// Enter some random data values
			double[] y = { 100, 115, 75, 22, 98, 40, 10 };
			double[] y2 = { 90, 100, 95, 35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67, 18 };
			double[] x = { 100, 200, 300, 400, 500, 600, 700 };

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.FromArgb( 255, 255, 245 ), Color.FromArgb( 255, 255, 190 ), 90F );

			// Generate a red curve with "Curve 1" in the legend
			LineItem myCurve = myPane.AddCurve( "Curve 1", x, y, Color.Red );
			// Make the symbols opaque by filling them with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Style = DashStyle.DashDot;
			myCurve.Line.DashOn = 3.0f;
			myCurve.Line.DashOff = 5.0f;


			// Generate a blue curve with "Curve 2" in the legend
			myCurve = myPane.AddCurve( "Curve 2", x, y2, Color.Blue );
			// Make the symbols opaque by filling them with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a green curve with "Curve 3" in the legend
			myCurve = myPane.AddCurve( "Curve 3", x, y3, Color.Green );
			// Make the symbols opaque by filling them with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 4.0f;

			// Manually set the x axis range
			myPane.XAxis.Scale.Min = 0;
			myPane.XAxis.Scale.Max = 800;
			// Display the Y axis grid lines
			myPane.YAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MinorGrid.IsVisible = true;

			myPane.YAxis.MajorGrid.DashOff = 0;
			myPane.YAxis.MinorGrid.DashOff = 0;

			// Draw a box item to highlight a value range
			BoxObj box = new BoxObj( 0.2, 100, 900, 30, Color.Empty,
					Color.FromArgb( 150, Color.LightGreen ) );
			box.Fill = new Fill( Color.White, Color.FromArgb( 255, Color.LightGreen ), 45.0F );
			// Use the BehindAxis zorder to draw the highlight beneath the grid lines
			box.ZOrder = ZOrder.F_BehindGrid;
			//box.Location.CoordinateFrame = CoordType.XChartFractionYScale;
			//box.IsClippedToChartRect = true;
			myPane.GraphObjList.Add( box );

			// Add a text item to label the highlighted range
			TextObj text = new TextObj( "Optimal\nRange", .95, 85, CoordType.XChartFractionYScale,
									AlignH.Right, AlignV.Center );
			text.IsClippedToChartRect = true;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.IsBold = true;
			text.FontSpec.IsItalic = true;
			myPane.GraphObjList.Add( text );

			// Calculate the Axis Scale Ranges
			z1.AxisChange();
		}


		public void CreateGraph_SortedOverlayBars( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "My Test Sorted Overlay Bar Graph";
			myPane.XAxis.Title.Text = "Label";
			myPane.YAxis.Title.Text = "My Y Axis";

			// Enter some data values
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, /*80,*/ 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 104, 67, 18 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.MajorTic.IsBetweenLabels = true;

			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;
			// Set the XAxis labels
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Scale.FontSpec.Size = 10.0F;

			// Make the bars a sorted overlay type so that they are drawn on top of eachother
			// (without summing), and each stack is sorted so the shorter bars are in front
			// of the taller bars
			myPane.BarSettings.Type = BarType.SortedOverlay;

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0F );

			// Calculate the Axis Scale Ranges
			zgc.AxisChange();
		}

		// masterpane with three vertical panes
		private void CreateGraph_ThreeVerticalPanes( ZedGraphControl z1 )
		{
			MasterPane master = z1.MasterPane;

			GraphPane myPane = z1.MasterPane[0];

			// Fill the background
			master.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );
			// Clear out the initial GraphPane
			master.PaneList.Clear();

			// Show the masterpane title
			master.Title.IsVisible = true;
			master.Title.Text = "Synchronized Panes Demo";

			// Leave a margin around the masterpane, but only a small gap between panes
			master.Margin.All = 10;
			master.InnerPaneGap = -5;

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

				//myPaneT.XAxis.Scale.FontSpec.Angle = 90;
				//myPaneT.XAxis.Scale.IsVisible = true;

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

			z1.LinkEvent += new ZedGraphControl.LinkEventHandler( z1_LinkEvent );

			//z1.DoubleClickEvent += new ZedGraphControl.ZedMouseEventHandler( z1_DoubleClickEvent );
		}

		private bool z1_LinkEvent( ZedGraphControl sender, GraphPane pane,
			object source, Link link, int index )
		{
			if ( source is CurveItem )
				MessageBox.Show( "You clicked on point #" + index.ToString() + " of curve '" + link.Title + "'" );
			else
				MessageBox.Show( "You clicked on a non-curve item" );

			return false;
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
				"   y1 = " + yVal.ToString() + "  y2 = " + yVal2.ToString() );
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

			return false;
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

		private void CreateGraph_StackedLinearBars( ZedGraphControl z1 )
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
				list3.Add( x + 0.1, y3 );
			}

			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );

			myPane.BarSettings.Type = BarType.Stack;
			myPane.BarSettings.Base = BarBase.X;

			//myPane.XAxis.MajorTic.IsBetweenLabels = true;
			//string[] labels = { "one", "two", "three", "four", "five", "six" };
			//myPane.XAxis.Scale.TextLabels = labels;
			//myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			//			z1.Invalidate();

			//			z1.LinkEvent += new ZedGraphControl.LinkEventHandler( z1_LinkEvent );

			//z1.DoubleClickEvent += new ZedGraphControl.ZedMouseEventHandler( z1_DoubleClickEvent );
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

		public void CreateGraph_StepChartDemo( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Demo for Step Charts";
			myPane.XAxis.Title.Text = "Time, Days";
			myPane.YAxis.Title.Text = "Widget Production (units/hour)";

			// Generate some sine-based data values
			PointPairList list = new PointPairList();
			for ( double i = 0; i < 36; i++ )
			{
				double x = ( i - 10.0 ) * 5.0;
				double y = Math.Sin( i * Math.PI / 15.0 ) * 16.0;
				list.Add( x, y );
			}

			// Add a red curve with circle symbols
			LineItem curve = myPane.AddCurve( "Step", list, Color.Red, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50 ), 90F );
			// Fill the symbols with white to make them opaque
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			// Set the curve type to forward steps
			curve.Line.StepType = StepType.RearwardSegment;
			//curve.Line.IsSmooth = true;

			myPane.Legend.Position = LegendPos.Float;
			myPane.Legend.Location.AlignH = AlignH.Left;
			myPane.Legend.Location.AlignV = AlignV.Top;
			myPane.Legend.Location.X = 0.05;
			myPane.Legend.Location.Y = 0.95;
			myPane.Legend.Location.CoordinateFrame = CoordType.PaneFraction;

			// Calculate the Axis Scale Ranges
			zgc.AxisChange();
		}

		private void CreateGraph_VerticalBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 9; i++ )
			{
				double x = (double)i / 3 + 4;
				double y = rand.NextDouble() * .1;
				double y2 = rand.NextDouble() * .1;
				double y3 = rand.NextDouble() * .1;
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

			z1.AxisChange();
			BarItem.CreateBarLabels( myPane, false, "f2" );

			// Tell ZedGraph to calculate the axis ranges
			z1.AxisChange();
			z1.Invalidate();


		}


		private void CreateGraph_VerticalLinearBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 9; i++ )
			{
				double x = (double)i / 3 + 4;
				double x2 = x + .2;
				double x3 = x + .4;
				double y = rand.NextDouble() * .1;
				double y2 = rand.NextDouble() * .1;
				double y3 = rand.NextDouble() * .1;
				list.Add( x, y );
				list2.Add( x2, y2 );
				list3.Add( x3, y3 );
			}

			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );
			//myCurve.IsOverrideOrdinal = true;

			//myPane.XAxis.MajorTic.IsBetweenLabels = true;
			//string[] labels = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
			//myPane.XAxis.Scale.TextLabels = labels;
			//myPane.XAxis.Type = AxisType.Text;

			z1.AxisChange();
			//BarItem.CreateBarLabels( myPane, false, "f2" );

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

			const int count = 10000;
			double[] x = new double[count];
			double[] y = new double[count];
			Random rand = new Random();

			for ( int i = 0; i < count; i++ )
			{
				double val = rand.NextDouble();
				x[i] = (double)i + 50.0;
				//y[i] = x[i] * x[i] * x[i] + val * x[i];
				y[i] = Math.Log( x[i] ) * ( 1 + ( val - 0.5 ) * 0.1 );
			}

			// FilteredPointList requires that the data are monotonically increasing in X, and the
			// X values are evenly spaced.
			FilteredPointList list = new FilteredPointList( x, y );
			LineItem myCurve = z1.GraphPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.IsVisible = false;


			// Apply logic that alternately keeps the highest or lowest point within each "bin" of data
			//list.IsApplyHighLowLogic = true;

			// Set the range of data of interest.  In effect you can limit the plotted data to only a
			// certain window within the total data range.  In this case, I set the minimum and maximum
			// bounds to include all data points.  Also, only show 100 data points on the plot.
			list.SetBounds( -1e20, 1e20, 100 );

			z1.AxisChange();
		}

		private void CreateGraph_Gantt( ZedGraphControl zg1 )
		{
			GraphPane myPane = zg1.GraphPane;

			// Setup the titles
			myPane.Title.Text = "Gantt Chart";
			myPane.XAxis.Title.Text = "Date";
			myPane.YAxis.Title.Text = "Project";

			// XAxis is Date type
			myPane.XAxis.Type = AxisType.Date;
			// Y Axis is Text type (ordinal)
			myPane.YAxis.Type = AxisType.Text;
			// Bars will be horizontal
			myPane.BarSettings.Base = BarBase.Y;

			// Set the Y axis text labels
			string[] labels = { "Project 1", "Project 2" };
			myPane.YAxis.Scale.TextLabels = labels;
			myPane.YAxis.MajorTic.IsBetweenLabels = true;

			// First, define all the bars that you want to be red
			PointPairList ppl = new PointPairList();
			XDate start = new XDate( 2005, 10, 31 );
			XDate end = new XDate( 2005, 11, 15 );
			// x is start of bar, y is project number, z is end of bar
			// Define this first one using start/end variables for illustration
			ppl.Add( start, 1.0, end );
			// add another red bar, assigned to project 2
			// Didn't use start/end variables here, but it's the same concept
			ppl.Add( new XDate( 2005, 12, 16 ), 2.0, new XDate( 2005, 12, 31 ) );
			HiLowBarItem myBar = myPane.AddHiLowBar( "job 1", ppl, Color.Red );
			// This tells the bar that we want to manually define the Y position
			// Y is AxisType.Text, which is ordinal, so a Y value of 1.0 goes with the first label,
			// 2.0 with the second, etc.
			myBar.IsOverrideOrdinal = true;
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90.0f );
			// This size is the width of the bar
			myBar.Bar.Size = 20f;

			// Now, define all the bars that you want to be Green
			ppl = new PointPairList();
			ppl.Add( new XDate( 2005, 11, 16 ), 2.0, new XDate( 2005, 11, 26 ) );
			myBar = myPane.AddHiLowBar( "job 2", ppl, Color.Green );
			myBar.IsOverrideOrdinal = true;
			myBar.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90.0f );
			myBar.Bar.Size = 20f;

			// Define all the bars that you want to be blue
			ppl = new PointPairList();
			ppl.Add( new XDate( 2005, 11, 27 ), 1.0, new XDate( 2005, 12, 15 ) );
			myBar = myPane.AddHiLowBar( "job 3", ppl, Color.Blue );
			myBar.IsOverrideOrdinal = true;
			myBar.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90.0f );
			myBar.Bar.Size = 20f;

			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );

			zg1.AxisChange();
		}

		private void CreateGraph_GasGauge( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Define the title
			myPane.Title.Text = "Gas Gauge Demo";

			// Fill the pane with gray
			myPane.Fill = new Fill( Color.LightGray, Color.White, 45.0f );
			// Fill the chart rect with blue
			myPane.Chart.Fill = new Fill( Color.White, Color.SkyBlue, 45.0f );

			// Don't show any axes for the gas gauge
			myPane.XAxis.IsVisible = false;
			myPane.Y2Axis.IsVisible = false;
			myPane.YAxis.IsVisible = false;

			//Define needles; can add more than one
			GasGaugeNeedle gg1 = new GasGaugeNeedle( "Cereal", 30.0f, Color.Black );
			GasGaugeNeedle gg2 = new GasGaugeNeedle( "Milk", 80.0f, Color.DarkGreen );
			myPane.CurveList.Add( gg1 );
			myPane.CurveList.Add( gg2 );

			//Define all regions
			GasGaugeRegion ggr1 = new GasGaugeRegion( "Red", 20f, 33.0f, Color.Red );
			GasGaugeRegion ggr2 = new GasGaugeRegion( "Yellow", 33.0f, 66.0f, Color.Yellow );
			GasGaugeRegion ggr3 = new GasGaugeRegion( "Green", 66.0f, 100.0f, Color.Green );

			// Add the curves
			myPane.CurveList.Add( ggr1 );
			myPane.CurveList.Add( ggr2 );
			myPane.CurveList.Add( ggr3 );

			zgc.AxisChange();
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


		private void CreateGraph_GradientByZPoints( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "PVT Properties";
			myPane.XAxis.Title.Text = "Pressure (atm)";
			myPane.YAxis.Title.Text = "Temperature (C)";

			// Enter some calculated data constants
			PointPairList list1 = new PointPairList();

			list1.Add( 13.03, 59.26, 31.67 );
			list1.Add( 18.73, 75.62, 23.34 );
			list1.Add( 16.94, 11.09, 21.04 );
			list1.Add( 11.66, 29.68, 32.19 );
			list1.Add( 12.94, 27.73, 28.94 );
			list1.Add( 18.04, 54.71, 22.76 );
			list1.Add( 17.36, 66.92, 24.51 );
			list1.Add( 13.19, 27.65, 28.39 );
			list1.Add( 12.79, 41.93, 30.6 );
			list1.Add( 19.66, 52.21, 20.79 );
			list1.Add( 17.17, 41.82, 22.98 );
			list1.Add( 12.57, 25.59, 29.53 );
			list1.Add( 18.74, 45.86, 21.36 );
			list1.Add( 12.66, 8.73, 27.71 );
			list1.Add( 13.29, 26.49, 28.07 );
			list1.Add( 12.47, 22.58, 29.48 );
			list1.Add( 14.76, 38.87, 26.36 );
			list1.Add( 22.54, 75.93, 19.5 );
			list1.Add( 14.38, 72.0, 29.88 );
			list1.Add( 12.01, 12.95, 29.58 );
			list1.Add( 14.96, 8.99, 23.6 );
			list1.Add( 17.58, 88.99, 25.76 );
			list1.Add( 15.4, 34.66, 24.97 );
			list1.Add( 15.39, 19.74, 23.79 );
			list1.Add( 19.88, 43.63, 20.03 );
			list1.Add( 12.42, 26.17, 29.93 );
			list1.Add( 12.91, 21.17, 28.37 );
			list1.Add( 16.48, 62.67, 25.47 );
			list1.Add( 12.05, 21.33, 30.36 );
			list1.Add( 17.4, 95.91, 26.52 );
			list1.Add( 17.92, 63.01, 23.51 );
			list1.Add( 15.74, 21.08, 23.4 );
			list1.Add( 14.57, 20.99, 25.21 );
			list1.Add( 16.08, 30.12, 23.58 );
			list1.Add( 17.44, 31.23, 21.9 );
			list1.Add( 15.79, 44.7, 25.19 );
			list1.Add( 13.67, 42.4, 28.74 );
			list1.Add( 15.98, 55.96, 25.74 );
			list1.Add( 15.41, 33.06, 24.83 );
			list1.Add( 14.37, 41.31, 27.28 );
			list1.Add( 13.99, 59.27, 29.58 );
			list1.Add( 11.43, 47.13, 34.65 );
			list1.Add( 15.21, 46.2, 26.21 );
			list1.Add( 20.08, 59.66, 20.82 );
			list1.Add( 16.45, 42.42, 24 );
			list1.Add( 11.76, 6.28, 29.51 );
			list1.Add( 18.66, 62.34, 22.56 );
			list1.Add( 14.91, 32.14, 25.58 );
			list1.Add( 14.2, 50.85, 28.43 );
			list1.Add( 14.99, 46.38, 26.6 );
			list1.Add( 23.55, 80.77, 18.93 );
			list1.Add( 13.4, 21.14, 27.34 );
			list1.Add( 15.39, 33.16, 24.88 );
			list1.Add( 20.4, 38.22, 19.21 );
			list1.Add( 13.63, 58.78, 30.28 );
			list1.Add( 13.23, 70.03, 32.2 );
			list1.Add( 15.25, 39.34, 25.62 );
			list1.Add( 14.92, 13.68, 24.04 );
			list1.Add( 23.81, 85.41, 18.93 );
			list1.Add( 18.5, 28.86, 20.48 );
			list1.Add( 14.11, 98.51, 32.72 );
			list1.Add( 17.22, 94.7, 26.68 );
			list1.Add( 22.94, 87.21, 19.74 );
			list1.Add( 12.38, 72.77, 34.61 );
			list1.Add( 13.26, 71.69, 32.26 );
			list1.Add( 15.07, 5.37, 23.11 );
			list1.Add( 16.2, 70.1, 26.45 );
			list1.Add( 12.81, 16.61, 28.14 );
			list1.Add( 11.63, 10.06, 30.23 );
			list1.Add( 13.84, 85.76, 32.23 );
			list1.Add( 14.8, 6.93, 23.67 );
			list1.Add( 13.6, 26.68, 27.5 );
			list1.Add( 13.65, 63.22, 30.64 );
			list1.Add( 24.85, 97.2, 18.77 );
			list1.Add( 13.8, 44.99, 28.71 );
			list1.Add( 20.02, 96.99, 23.19 );
			list1.Add( 16.91, 50.47, 23.94 );
			list1.Add( 11.94, 32.23, 31.72 );
			list1.Add( 19.97, 77.09, 22 );
			list1.Add( 14.21, 98.48, 32.49 );
			list1.Add( 17.66, 38.76, 22.15 );
			list1.Add( 17.23, 42.5, 22.96 );
			list1.Add( 14.43, 8.97, 24.43 );
			list1.Add( 14.94, 60.76, 27.87 );

			// Generate a red curve with diamond symbols, and "Gas Data" in the legend
			LineItem myCurve = myPane.AddCurve( "Gas Data", list1, Color.Red,
										SymbolType.Diamond );
			myCurve.Symbol.Size = 12;
			// Set up a red-blue color gradient to be used for the fill
			myCurve.Symbol.Fill = new Fill( Color.Red, Color.Blue );
			// Turn off the symbol borders
			myCurve.Symbol.Border.IsVisible = false;
			// Instruct ZedGraph to fill the symbols by selecting a color out of the
			// red-blue gradient based on the Z value.  A value of 19 or less will be red,
			// a value of 34 or more will be blue, and values in between will be a
			// linearly apportioned color between red and blue.
			myCurve.Symbol.Fill.Type = FillType.GradientByZ;
			myCurve.Symbol.Fill.RangeMin = 19;
			myCurve.Symbol.Fill.RangeMax = 34;
			// Turn off the line, so the curve will by symbols only
			myCurve.Line.IsVisible = false;

			// Display a text item with "MW = 34" on the graph
			TextObj text = new TextObj( "MW = 34", 12.9F, 110, CoordType.AxisXYScale );
			text.FontSpec.FontColor = Color.Blue;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Size = 14;
			myPane.GraphObjList.Add( text );

			// Display a text item with "MW = 19" on the graph
			text = new TextObj( "MW = 19", 25, 110, CoordType.AxisXYScale );
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Size = 14;
			myPane.GraphObjList.Add( text );


			// Show the X and Y grids
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;

			// Set the x and y scale and title font sizes to 14
			myPane.XAxis.Scale.FontSpec.Size = 14;
			myPane.XAxis.Title.FontSpec.Size = 14;
			myPane.YAxis.Scale.FontSpec.Size = 14;
			myPane.YAxis.Title.FontSpec.Size = 14;
			// Set the GraphPane title font size to 16
			myPane.Title.FontSpec.Size = 16;
			// Turn off the legend
			//myPane.Legend.IsVisible = false;
			myCurve.Label.IsVisible = false;
			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166 ), 90F );

			// Add BarItem without any bars to make a pseudo legend entry
			PointPairList ppl = new PointPairList();
			BarItem myBar = myPane.AddBar( "Gradient Data", ppl, Color.Blue );
			Color[] colors = { Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Purple };
			myBar.Bar.Fill = new Fill( colors );
			myBar.Bar.Fill.Type = FillType.GradientByZ;

			myBar.Bar.Fill.RangeMin = 0;
			myBar.Bar.Fill.RangeMax = 4;
			myBar.Bar.Fill.SecondaryValueGradientColor = Color.Empty;

			// Calculate the Axis Scale Ranges
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
			segment1.Link = new Link( "Poop 1", "http://yahoo.com", "_blank" );

			PieItem segment3 = myPane.AddPieSlice( 30, Color.Purple, Color.White, 45f, .0, "East" );
			segment3.LabelType = PieLabelType.Name_Percent;
			segment3.Link = new Link( "Poop 3", "http://yahoo.com", "_blank" );

			PieItem segment4 = myPane.AddPieSlice( 10.21, Color.LimeGreen, Color.White, 45f, 0, "West" );
			segment4.LabelType = PieLabelType.Percent;
			segment4.Link = new Link( "Poop 4", "http://yahoo.com", "_blank" );

			PieItem segment2 = myPane.AddPieSlice( 40, Color.SandyBrown, Color.White, 45f, 0.2, "South" );
			segment2.LabelType = PieLabelType.Value;
			segment2.Link = new Link( "Poop 2", "http://yahoo.com", "_blank" );

			PieItem segment6 = myPane.AddPieSlice( 250, Color.Red, Color.White, 45f, 0, "Europe" );
			segment6.LabelType = PieLabelType.Name_Value;
			segment6.Link = new Link( "Poop 6", "http://yahoo.com", "_blank" );

			PieItem segment7 = myPane.AddPieSlice( 50, Color.Blue, Color.White, 45f, 0.2, "Pac Rim" );
			segment7.LabelType = PieLabelType.Name;
			segment7.Link = new Link( "Poop 7", "http://yahoo.com", "_blank" );

			PieItem segment8 = myPane.AddPieSlice( 400, Color.Green, Color.White, 45f, 0, "South America" );
			segment8.LabelType = PieLabelType.None;
			segment8.Link = new Link( "Poop 8", "http://yahoo.com", "_blank" );

			PieItem segment9 = myPane.AddPieSlice( 50, Color.Yellow, Color.White, 45f, 0.2, "Africa" );
			segment9.Link = new Link( "Poop 9", "http://yahoo.com", "_blank" );

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

			z1.LinkEvent += new ZedGraphControl.LinkEventHandler( z1_LinkEvent );

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

			//Serialize( z1, "junk.bin" );
			//z1.MasterPane.PaneList.Clear();

			//DeSerialize( z1, "junk.bin" );
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
			myCurve.Line.Style = DashStyle.Custom;
			myCurve.Line.DashOff = 5;
			myCurve.Line.DashOn = 5;

			// Generate a blue curve with circle symbols, and "Beta" in the _legend
			myCurve = myPane.AddCurve( "Beta", list2, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			//myCurve.IsY2Axis = true;

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
			myPane.Y2Axis.Scale.Min = -10;
			myPane.Y2Axis.Scale.Max = 10;
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
			ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState )
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
			myTimer = new Timer();
			myTimer.Enabled = true;
			myTimer.Tick += new EventHandler( MyTimer_Tick );
			myTimer.Interval = 50;
			myTimer.Start();

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
			//myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
			// Set the minor step to 1 hour
			//myPane.XAxis.Scale.MinorStep = 1.0;
			//myPane.XAxis.Scale.MinorUnit = DateUnit.Minute;
			//myPane.XAxis.Scale.Format = "HH:mm:ss";
			//myPane.XAxis.Type = AxisType.Date;

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
				arrow.Line.Color = Color.LightGray;
				arrow.ZOrder = ZOrder.E_BehindCurves;
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
			myLine.Line.Width = 2.0f;
			myLine.Line.Color = Color.Red;
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
				x = (double)new XDate( 1995, 5, 11, 15, 27, 10 + i );
				y = 15 + i; // Math.Sin( (double)i * Math.PI / 15.0 );
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
					list, Color.Red, SymbolType.Diamond );

			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			//			myPane.XAxis.Scale.Min = new XDate( 1995, 5, 20 );
			//			myPane.XAxis.Scale.Max = new XDate( 1995, 6, 10 );

			IPointListEdit listE = myCurve.Points as IPointListEdit;
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 46 ), 51 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 47 ), 52 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 48 ), 53 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 49 ), 54 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 50 ), 55 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 51 ), 56 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 52 ), 57 );
			listE.Add( new XDate( 1995, 5, 11, 15, 27, 53 ), 58 );


			// Tell ZedGraph to refigure the axes since the data 
			// have changed
			z1.AxisChange();
		}

		// Basic curve test - Linear Axis
		private void CreateGraph_BasicLinearScroll( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();

			for ( int i = 1; i < 50; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 8.0 ) * 100 + 101;
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.Width = 3.0f;
			//myCurve.Line.Fill = new Fill( Color.Red, Color.White );
			myPane.Chart.Border.IsVisible = false;

			//LineObj line = new LineObj( Color.Black, 0, 0, 1, 0 );
			//line.Location.CoordinateFrame = CoordType.ChartFraction;
			//line.ZOrder = ZOrder.A_InFront;

			//myPane.YAxis.Cross = 20;

			// Show the horizontal scrollbar
			z1.IsShowHScrollBar = true;
			//z1.ScrollMinX = 20;
			//z1.ScrollMaxX = 30;
			//z1.IsShowVScrollBar = true;
			// Tell ZedGraph to automatically set the range of the scrollbar according to the data range
			z1.IsAutoScrollRange = true;
			// Make the scroll cover slightly more than the range of the data
			z1.ScrollGrace = .05;


			//z1.GraphPane.IsBoundedRanges = true;
			//z1.GraphPane.XAxis.Scale.MajorStep = 10;

			// Set the initial x range
			z1.GraphPane.XAxis.Scale.Min = 20;
			z1.GraphPane.XAxis.Scale.Max = 30;

			z1.AxisChange();
			//z1.GraphPane.XAxis.IsReverse = true;
			//z1.GraphPane.XAxis.Type = AxisType.Date;
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

		// Basic curve test with images for symbols
		private void CreateGraph_ScatterPlot( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the titles
			myPane.Title.Text = "Scatter Plot Demo";
			myPane.XAxis.Title.Text = "Pressure, Atm";
			myPane.YAxis.Title.Text = "Flow Rate, cc/hr";

			// Get a random number generator
			Random rand = new Random();

			// Populate a PointPairList with a log-based function and some random variability
			PointPairList list = new PointPairList();
			for ( int i = 0; i < 200; i++ )
			{
				double x = rand.NextDouble() * 20.0 + 1;
				double y = Math.Log( 10.0 * ( x - 1.0 ) + 1.0 ) * ( rand.NextDouble() * 0.2 + 0.9 );
				list.Add( x, y );
			}

			// Add the curve
			LineItem myCurve = myPane.AddCurve( "Performance", list, Color.Black, SymbolType.Diamond );
			// Don't display the line (This makes a scatter plot)
			myCurve.Line.IsVisible = false;
			// Hide the symbol outline
			myCurve.Symbol.Border.IsVisible = false;
			// Fill the symbol interior with color
			myCurve.Symbol.Fill = new Fill( Color.Firebrick );

			// Fill the background of the chart rect and pane
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.SlateGray, 45.0f );

			zgc.AxisChange();
		}

		private void CreateGraph_ScrollTest( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			z1.IsAutoScrollRange = true;
			z1.IsEnableHPan = true;
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
				labels[i] = i.ToString();
				y[i] = random.NextDouble() * 50;
			}
			BarItem myBar = myPane.AddBar( "Testing", null, y, Color.Red );
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );
			myPane.XAxis.MajorTic.IsBetweenLabels = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Scale.Min = 20;
			myPane.XAxis.Scale.Max = 30;
			myPane.BarSettings.MinClusterGap = 2;
			z1.AxisChange();

			z1.ScrollEvent += new ScrollEventHandler( ScrollTest_Scroll );
			z1.ScrollDoneEvent += new ZedGraphControl.ScrollDoneHandler( ScrollTest_ScrollDoneEvent );
			z1.ScrollProgressEvent += new ZedGraphControl.ScrollProgressHandler( ScrollTest_ScrollProgressEvent );

			//this.Refresh();
		}

		void ScrollTest_ScrollProgressEvent( ZedGraphControl sender, ScrollBar scrollBar,
					ZoomState oldState, ZoomState newState )
		{
			//MessageBox.Show( "ScrollProgressEvent" );
		}

		void ScrollTest_ScrollDoneEvent( ZedGraphControl sender, ScrollBar scrollBar,
					ZoomState oldState, ZoomState newState )
		{
			//MessageBox.Show( "ScrollDoneEvent" );
		}

		void ScrollTest_Scroll( object sender, ScrollEventArgs e )
		{
			//MessageBox.Show( "Scroll" );
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

		// Sample scrollable graph
		private void CreateGraph_ScrollSample( ZedGraphControl zgc )
		{
			// show the horizontal scroll bar
			zgc.IsShowHScrollBar = true;

			// manually set the scroll range
			zgc.ScrollMinX = -50;
			zgc.ScrollMaxX = 550;

			// automatically set the scrollable range to cover the data range from the curves
			//zgc.IsAutoScrollRange = true;
			// Add 10% to scale range
			//zgc.ScrollGrace = 0.1;

			// Horizontal pan and zoom allowed
			zgc.IsEnableHPan = true;
			zgc.IsEnableHZoom = true;

			// Vertical pan and zoom not allowed
			zgc.IsEnableVPan = false;
			zgc.IsEnableVZoom = false;

			// Set the initial viewed range
			zgc.GraphPane.XAxis.Scale.Min = 50.0;
			zgc.GraphPane.XAxis.Scale.Max = 250.0;

			ScrollSample_Setup( zgc );

			TextObj text = new TextObj( "Hi there", 50.0, 0.0 );
			text.IsClippedToChartRect = true;
			zgc.GraphPane.GraphObjList.Add( text );

		}

		private void ScrollSample_Setup( ZedGraphControl zgc )
		{
			// get a reference to the GraphPane
			GraphPane myPane = zgc.GraphPane;

			// Set the titles
			myPane.Title.Text = "Sample ScrollBar Graph";
			myPane.XAxis.Title.Text = "Index";
			myPane.XAxis.Title.Text = "Phased Sine Data";

			myPane.YAxis.MajorGrid.IsZeroLine = false;

			// Generate some sample sine data in PointPairList's
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();

			for ( int i = 0; i < 100; i++ )
			{
				double x = (double)i * 5.0 + 25.0;
				double y = Math.Sin( (double)i * Math.PI / 25.0 ) * 16.0;
				double y2 = Math.Sin( (double)i * Math.PI / 25.0 + 30.0 ) * 12.0;
				double y3 = Math.Sin( (double)i * Math.PI / 25.0 + 60.0 ) * 8.0;
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
			}

			// create three curves from the above data sets
			LineItem myCurve = myPane.AddCurve( "Alpha", list, Color.Red, SymbolType.Diamond );
			myCurve = myPane.AddCurve( "Beta", list2, Color.Blue, SymbolType.Plus );
			myCurve = myPane.AddCurve( "Sigma", list3, Color.Green, SymbolType.XCross );

			// scale the axes base on the data
			//zgc.AxisChange();
			zgc.GraphPane.AxisChange();

			zgc.ScrollProgressEvent += new ZedGraphControl.ScrollProgressHandler( zgc_ScrollProgressEvent );
			zgc.ScrollDoneEvent += new ZedGraphControl.ScrollDoneHandler( zgc_ScrollDoneEvent );
			//zgc.Scroll += new ScrollEventHandler( zgc_Scroll );
		}

		void zgc_Scroll( object sender, ScrollEventArgs e )
		{
			this.toolStripStatusLabel1.Text = e.NewValue.ToString();
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

		private void CreateGraph_NegativeBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 20; i++ )
			{
				list.Add( (double)i + 1.0, rand.NextDouble() * 200.0 - 100.0, 50.0 );
			}

			HiLowBarItem myBar = myPane.AddHiLowBar( "histogram", list, Color.Blue );
			myBar.Bar.Fill = new Fill( Color.Blue );
			myPane.BarSettings.MinClusterGap = 0.0f;
			myPane.YAxis.MajorGrid.IsZeroLine = false;

			LineObj line = new LineObj( Color.Black, 0, 50, 1, 50 );
			line.Location.CoordinateFrame = CoordType.XChartFractionYScale;
			myPane.GraphObjList.Add( line );


			z1.AxisChange();
		}

		private void CreateGraph_NegativeHorizontalBars( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < 20; i++ )
			{
				list.Add( rand.NextDouble() * 200.0 - 100.0, (double)i + 1.0, 50.0 );
			}

			list.Sort( SortType.XValues );

			HiLowBarItem myBar = myPane.AddHiLowBar( "histogram", list, Color.Blue );
			myBar.Bar.Fill = new Fill( Color.Blue );
			myPane.BarSettings.MinClusterGap = 0.0f;
			myPane.YAxis.MajorGrid.IsZeroLine = false;

			myPane.BarSettings.Base = BarBase.Y;

			LineObj line = new LineObj( Color.Black, 0, 50, 1, 50 );
			line.Location.CoordinateFrame = CoordType.XChartFractionYScale;
			myPane.GraphObjList.Add( line );

			z1.AxisChange();
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
			list.FilterData( myPane, myPane.XAxis, myPane.YAxis );

			MessageBox.Show( list.Count.ToString() );
			int count = list.Count;
		}

		// Basic curve test - 32000 points
		private void CreateGraph_32kPoints( ZedGraphControl z1 )
		{
			const int numPoints = 1000000;

			GraphPane myPane = z1.GraphPane;

			PointPairList list = new PointPairList();
			Random rand = new Random();

			for ( int i = 0; i < numPoints; i++ )
			{
				double val = Math.Sin( Math.PI * 10.0 * (double)i / (double)numPoints ); // rand.NextDouble();
				double x = (double)i;
				double y = val; // x + val * val * val * 10;

				list.Add( x, y );
			}

			//myPane.XAxis.Scale.Min = 400000;
			//myPane.XAxis.Scale.Max = 600000;

			//myPane.XAxis.Type = AxisType.Log;

			LineItem myCurve = z1.GraphPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			//myCurve.Line.IsVisible = false;
			//myCurve.Symbol.IsVisible = false;
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

			myPane.BarSettings.Base = BarBase.Y;

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166 ), 45.0F );

			z1.AxisChange();
		}

		// Build the Chart
		private void CreateGraph_ImageObj( ZedGraphControl zg1 )
		{
			// get a reference to the GraphPane
			GraphPane myPane = zg1.GraphPane;

			// Set the Titles
			myPane.Title.Text = "My Test Bar Graph";
			myPane.XAxis.Title.Text = "Label";
			myPane.YAxis.Title.Text = "My Y Axis";
			myPane.X2Axis.Title.Text = "The Top X Axis";
			myPane.Y2Axis.Title.Text = "Y2 Axis";

			myPane.X2Axis.IsVisible = true;
			myPane.X2Axis.Scale.IsReverse = true;
			myPane.Y2Axis.IsVisible = true;

			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", 
                      "Cougar", "Tiger", "Leopard" };
			double[] x = { 1, 2, 3, 4, 5, 6 };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 90, 100, 95, 35, 80, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67 };
			double[] y4 = { 120, 125, 100, 40, 105, 75 };

			// Generate a red bar with "Curve 1" in the legend
			//BarItem myBar = myPane.AddBar( "Curve 1 label is long\nsecond line", null, y, Color.Red );
			BarItem myBar = myPane.AddBar( "Curve 1", null, y, Color.Red );
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = myPane.AddBar( "Curve 2", null, y2, Color.Blue );
			myBar.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			//myBar = myPane.AddBar( "Curve 3\ntwo lines", null, y3, Color.Green );
			myBar = myPane.AddBar( "Curve 3", null, y3, Color.Green );
			myBar.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green );

			// Generate a black line with "Curve 4" in the legend
			LineItem myCurve = myPane.AddCurve( "Curve 4",
					x, y4, Color.Black, SymbolType.Circle );
			myCurve.IsX2Axis = true;

			myCurve.Line.Fill = new Fill( Color.White,
										 Color.LightSkyBlue, -45F );

			// Fix up the curve attributes a little
			myCurve.Symbol.Size = 8.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Draw the X tics between the labels instead of 
			// at the labels
			myPane.XAxis.MajorTic.IsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.Scale.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			// Fill the Axis and Pane backgrounds
			myPane.Chart.Fill = new Fill( Color.White,
					Color.FromArgb( 255, 255, 166 ), 90F );
			myPane.Fill = new Fill( Color.FromArgb( 250, 250, 255 ) );

			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;

			LineObj line = new LineObj( Color.Black, 2.3, 0, 2.3, 1 );
			line.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
			line.Line.Width = 2.0f;
			myPane.GraphObjList.Add( line );


			//Image image = Image.FromFile( @"c:\temp\vtx1330.jpg" );
			Image image = Bitmap.FromFile( @"c:\temp\vtx1300crop.jpg" );

			ImageObj imageObj = new ImageObj( image, new RectangleF( 3, 120, 2, 100 ) );
			imageObj.IsScaled = false;
			//			imageObj.IsVisible = true;
			imageObj.ZOrder = ZOrder.E_BehindCurves;
			//			imageObj.Location.CoordinateFrame = CoordType.ChartFraction;
			myPane.GraphObjList.Add( imageObj );

			//			myPane.Chart.Fill = new Fill( image, WrapMode.Tile );

			//            myPane.Title.Text = null;

			//          GraphPane newOne = myPane.Clone();

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			zg1.AxisChange();
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
		}

		/*
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
		*/
		private void CreateGraph_DataSourcePointList( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.MasterPane[0];

			myPane.Title.Text = "Statistics";
			myPane.XAxis.Title.Text = "Job";
			myPane.Y2Axis.Title.Text = "Count";

			DataSourcePointList dsp = new DataSourcePointList();

			//Person bllPerson = new Person();

			DataTable dt = new DataTable();
			dt.Columns.Add( "Job", typeof( string ) );
			dt.Columns.Add( "Count", typeof( Int32 ) );
			DataRow dr = dt.NewRow();
			dr["Job"] = 1;
			dr["Count"] = 100;

			dt.Rows.Add( dr );
			dsp.DataSource = dt;
			dsp.XDataMember = "Job";
			dsp.YDataMember = "Count";
			dsp.ZDataMember = null;
			dsp.TagDataMember = "Job";

			myPane.XAxis.Type = AxisType.Text;

			BarItem myBar = myPane.AddBar( "Statistics", dsp, Color.Blue );

			z1.AxisChange();
		}

		private void CreateGraph_DataSourcePointListTest( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			myPane.Title.Text = "DataSourcePointList Test";
			myPane.XAxis.Title.Text = "Item";
			myPane.YAxis.Title.Text = "Quantity";

			DataSourcePointList dspl = new DataSourcePointList();
			DataTable productsTable = new DataTable();
			productsTable.Columns.Add( "Item", typeof( double ) );
			productsTable.Columns.Add( "Quantity", typeof( double ) );
			productsTable.Rows.Add( 1, 10.0 );
			productsTable.Rows.Add( 2, 20.0 );
			productsTable.Rows.Add( 3, 50.0 );
			productsTable.Rows.Add( 4, 40.0 );
			productsTable.Rows.Add( 5, 10.0 );
			productsTable.Rows.Add( 6, 70.0 );
			productsTable.Rows.Add( 7, 30.0 );
			productsTable.Rows.Add( 8, 20.0 );
			productsTable.Rows.Add( 9, 40.0 );

			//Dim ProductsTable As DataTable = GetProductsData()

			DataRow dr = productsTable.Rows[0];
			//DataRowView drv = productsTable.Rows[2] as DataRowView;
			//System.Reflection.PropertyInfo pInfo = dr.GetType().GetProperty( "Item" );

			//System.Reflection.PropertyInfo pInfo = dr[0].GetType();

			double x1 = (double)dr["Item"];

			double x = (double)dr[0];
			double y = (double)dr[1];

			dspl.DataSource = productsTable;
			dspl.XDataMember = "Item";
			dspl.YDataMember = "Quantity";
			dspl.ZDataMember = null;
			//dspl.TagDataMember = "Item";

			myPane.XAxis.Type = AxisType.Text;
			LineItem myCurve = myPane.AddCurve( "Item", dspl, Color.Blue );
			myCurve.Line.IsVisible = false;
			z1.IsShowPointValues = true;
			z1.AxisChange();
		}

		private void CreateGraph_DataSourcePointListArrayList( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;
			myPane.Title.Text = "DataSourcePointList Test";
			myPane.XAxis.Title.Text = "Item";
			myPane.YAxis.Title.Text = "Quantity";

			DataSourcePointList dspl = new DataSourcePointList();
			List<MySimplePoint> list = new List<MySimplePoint>();
			list.Add( new MySimplePoint( 1, 10.0, "Point #1" ) );
			list.Add( new MySimplePoint( 2, 20.0, "Point #2" ) );
			list.Add( new MySimplePoint( 3, 50.0, "Point #3" ) );
			list.Add( new MySimplePoint( 4, 40.0, "Point #4" ) );
			list.Add( new MySimplePoint( 5, 10.0, "Point #5" ) );
			list.Add( new MySimplePoint( 6, 70.0, "Point #6" ) );
			list.Add( new MySimplePoint( 7, 30.0, "Point #7" ) );
			list.Add( new MySimplePoint( 8, 20.0, "Point #8" ) );
			list.Add( new MySimplePoint( 9, 40.0, "Point #9" ) );

			dspl.DataSource = list;
			dspl.XDataMember = "X";
			dspl.YDataMember = "Y";
			dspl.ZDataMember = null;
			dspl.TagDataMember = "Tag";

			//System.Reflection.PropertyInfo pInfo = dr.GetType().GetProperty( "ItemX" );

			//System.Reflection.PropertyInfo pInfo = dr[0].GetType();

			//double x1 = (double)dr["Itemx"];

			//double x = (double)dr[0];
			//double y = (double)dr[1];

			myPane.XAxis.Type = AxisType.Text;
			LineItem myCurve = myPane.AddCurve( "Item", dspl, Color.Blue );
			myCurve.Line.IsVisible = false;
			z1.IsShowPointValues = true;
			z1.AxisChange();
		}

		private void CreateGraph_BarJunk( ZedGraphControl z1 )
		{
			GraphPane grPane = z1.GraphPane;

			grPane.Legend.IsVisible = false;

			PointPairList list = new PointPairList();
			list.Add( 10, 1, 1 );
			//			list.Add( 20, 20, 1 );
			//			list.Add( 30, 15, 1 );
			//			list.Add( 40, 35, 2 );
			//			list.Add( 50, 22, 1 );
			//			list.Add( 100, -5, 1 );
			//			list.Add( 150, 12, 1 );
			//			list.Add( 200, 29, 1 );

			//			grPane.YAxis.Scale.Min = 0;
			//			grPane.YAxis.Scale.Max = 0;

			BarItem myCurve = grPane.AddBar( "My Curve", list, Color.Blue );

			myCurve.Bar.Fill = new Fill( Color.Blue, Color.Red );
			myCurve.Bar.Fill.RangeMin = 1;
			myCurve.Bar.Fill.RangeMax = 2;
			myCurve.Bar.Fill.RangeDefault = 1;
			myCurve.Bar.Fill.Type = FillType.GradientByZ;
			myCurve.Bar.Fill.SecondaryValueGradientColor = Color.White;

			grPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166 ), 45.0F );

			grPane.XAxis.Type = AxisType.Text;
			grPane.YAxis.Type = AxisType.Log;
			string[] labels = { "Hey" };
			grPane.XAxis.Scale.TextLabels = labels;
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

		private void CreateGraph_BarJunk3( ZedGraphControl zg1 )
		{
			GraphPane myPane = zg1.GraphPane;

			string[] labels = { "Bar 1", "Bar 2", "Bar 3" };
			double[] x1 = { 1.0 };
			double[] y1 = { .3 };
			double[] x2 = { 2.0 };
			double[] y2 = { .2 };
			double[] x3 = { 3.0 };
			double[] y3 = { .7 };

			BarItem myBar = myPane.AddBar( "Bar 1", x1, y1, Color.Red );
			myBar.IsOverrideOrdinal = true;
			myBar = myPane.AddBar( "Bar 2", x2, y2, Color.Blue );
			myBar.IsOverrideOrdinal = true;
			myBar = myPane.AddBar( "Bar 3", x3, y3, Color.Green );
			myBar.IsOverrideOrdinal = true;

			myPane.XAxis.MajorTic.IsAllTics = false;
			myPane.XAxis.Scale.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;

			zg1.AxisChange();

			BarItem.CreateBarLabels( myPane, false, "f2" );
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

			//for ( int i = 0; i < 10000000; i++ )
			//	;

			//myPane.Y2AxisList.RemoveAt( myPane.Y2AxisList.Count - 1 );
			//myPane.Y2AxisList.RemoveAt( myPane.Y2AxisList.Count - 1 );
			//z1.Refresh();

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

		// Basic curve test - Linear Axis
		private void CreateGraph_X2Axis( ZedGraphControl z1 )
		{
			GraphPane myPane = z1.GraphPane;

			PointPairList listX = new PointPairList();
			PointPairList listX2 = new PointPairList();
			const int count = 20;

			for ( int i = 0; i < count; i++ )
			{
				double x = i;
				double y = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.2 ) );

				double x2 = i * 100;
				double y2 = 300.0 * ( 1.0 + Math.Sin( (double)i * 0.4 + Math.PI ) );

				listX.Add( x, y );
				listX2.Add( x2, y2 );
			}

			LineItem myCurve = myPane.AddCurve( "X Axis Curve", listX, Color.Blue, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "X2 Axis Curve", listX2, Color.Red, SymbolType.Diamond );

			myPane.X2Axis.IsVisible = true;
			myCurve2.IsX2Axis = true;

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
			ZedGraphControl zg1 = zedGraphControl1;

			int start = Environment.TickCount;
			Invalidate();
			Refresh();

			MessageBox.Show( "time= " + ( Environment.TickCount - start ).ToString() + " msec" );

			return;

			//zg1.GraphPane = new GraphPane();
			//return;

			Serialize( zg1, "junk.bin" );
			zg1.MasterPane.PaneList.Clear();
			zg1.Refresh();
			MessageBox.Show( "PaneList Cleared" );

			DeSerialize( zg1, "junk.bin" );

			return;

			Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
			double range = xScale.Max - xScale.Min;
			xScale.Min = 10;
			xScale.Max = 10 + range;
			Refresh();

			return;

			zedGraphControl1.MasterPane.GetMetafile().Save( "poop.emf" );
			return;

			this.zedGraphControl1.GraphPane.Y2AxisList.Clear();
			this.zedGraphControl1.GraphPane.Y2AxisList.Add( "My Title" );
			//this.zedGraphControl1.GraphPane.Y2AxisList.RemoveAt( this.zedGraphControl1.GraphPane.Y2AxisList.Count - 1 );
			//myPane.Y2AxisList.RemoveAt( myPane.Y2AxisList.Count - 1 );
			this.zedGraphControl1.Refresh();

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
				double x, x2, y, y2;
				// Convert the mouse location to X, Y, and Y2 scale values
				pane.ReverseTransform( mousePt, out x, out x2, out y, out y2 );
				// Format the status label text
				toolStripStatusXY.Text = "(" + x.ToString( "f2" ) + ", " + y.ToString( "f2" ) + ")";
			}
			else
				// If there is no valid data, then clear the status label text
				toolStripStatusXY.Text = string.Empty;

			object nearestObj;
			int index;
			if ( sender.GraphPane.FindNearestObject( mousePt, this.CreateGraphics(), out nearestObj, out index ) )
				statusLabelLastClick.Text = nearestObj.ToString();
			else
				statusLabelLastClick.Text = "";

			// Return false to indicate we have not processed the MouseMoveEvent
			// ZedGraphControl should still go ahead and handle it
			return false;
		}

		private bool zedGraphControl1_MouseDownEvent( ZedGraphControl sender, MouseEventArgs e )
		{
			// Save the mouse location
			PointF mousePt = new PointF( e.X, e.Y );


			return false;

			double xvalue1 = zedGraphControl1.GraphPane.XAxis.Scale.ReverseTransform( 245 );
			float xpix1 = zedGraphControl1.GraphPane.XAxis.Scale.Transform( xvalue1 );

			zedGraphControl1.MasterPane.GetImage( 6400, 4800, 72 ).Save( "poop.jpg", ImageFormat.Gif );

			double xvalue2 = zedGraphControl1.GraphPane.XAxis.Scale.ReverseTransform( 245 );
			float xpix2 = zedGraphControl1.GraphPane.XAxis.Scale.Transform( xvalue2 );

			//output:
			//xvalue1=19,89
			//xpic1 = 245
			//xvalue2= 47,17
			//output xpic2 = 245

			return false;

			sender.GraphPane.GetImage( 640, 480, 96 ).Save( "myfile.jpg", ImageFormat.Jpeg );
			//sender.GraphPane.CurveList.Clear();
			//CreateGraph_DualYDemo( sender );
			//sender.Invalidate();
			//return true;

			sender.GraphPane = new GraphPane();
			CreateGraph_DualYDemo( sender );
			//SetSize();
			sender.Refresh();
			return false;


			double x, y, x2, y2;
			sender.GraphPane.ReverseTransform( new PointF( e.X, e.Y ), out x, out x2, out y, out y2 );

			return false;

			CurveItem dragCurve;
			int dragIndex;
			GraphPane myPane = this.zedGraphControl1.GraphPane;
			PointPair startPair;

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
				myPane.ReverseTransform( ptScrXY, false, false, 0, out dx, out dy );

				TextObj text = new TextObj( "Test Label", (float)dx, (float)dy, CoordType.AxisXYScale,
					AlignH.Left, AlignV.Bottom );

				myPane.GraphObjList.Add( text );

				return true;
			}

			return false;
		}

		PointF startPt;
		double startX, startY;
		bool isDragPoint = false;
		CurveItem dragCurve;
		int dragIndex;
		PointPair startPair;

		private bool old_zedGraphControl1_MouseDownEvent( ZedGraphControl control, MouseEventArgs e )
		{
			// point-dragging is activated by an 'Alt' key and mousedown combination
			if ( Control.ModifierKeys == Keys.Alt )
			{
				GraphPane myPane = control.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );

				// find the point that was clicked, and make sure the point list is editable
				// and that it's a primary Y axis (the first Y or Y2 axis)
				if ( myPane.FindNearestPoint( mousePt, out dragCurve, out dragIndex ) &&
							dragCurve.Points is PointPairList &&
							dragCurve.YAxisIndex == 0 )
				{
					// save the starting point information
					startPt = mousePt;
					startPair = dragCurve.Points[dragIndex];
					// indicate a drag operation is in progress
					isDragPoint = true;
					// get the scale values for the start of the drag
					double startX2, startY2;
					myPane.ReverseTransform( mousePt, out startX, out startX2, out startY, out startY2 );
					// if it's a Y2 axis, use that value instead of Y
					if ( dragCurve.IsY2Axis )
						startY = startY2;

					return true;
				}
			}

			return false;
		}

		private bool old_zedGraphControl1_MouseMoveEvent( ZedGraphControl control, MouseEventArgs e )
		{
			GraphPane myPane = control.GraphPane;
			PointF mousePt = new PointF( e.X, e.Y );

			// see if a dragging operation is underway
			if ( isDragPoint )
			{
				// get the scale values that correspond to the current point
				double curX, curX2, curY, curY2;
				myPane.ReverseTransform( mousePt, out curX, out curX2, out curY, out curY2 );
				// if it's a Y2 axis, use that value instead of Y
				if ( dragCurve.IsY2Axis )
					curY = curY2;
				// calculate the new scale values for the point
				PointPair newPt = new PointPair( startPair.X + curX - startX,
						(int)( startPair.Y + curY - startY + 0.5 ) );
				// save the data back to the point list
				( dragCurve.Points as PointPairList )[dragIndex] = newPt;
				// force a redraw
				control.Refresh();
				// tell the ZedGraphControl not to do anything else with this event
				return true;
			}
			else
			{
				//change the cursor if the mouse is sufficiently close to a point
				if ( myPane.FindNearestPoint( mousePt, out dragCurve, out dragIndex ) &&
							dragCurve.Points is PointPairList &&
							dragCurve.YAxisIndex == 0 )
				{
					control.Cursor = Cursors.SizeAll;
				}
				else
				{
					control.Cursor = Cursors.Default;
				}
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
	}

	public struct MySimplePoint
	{
		private double _x;
		private double _y;
		private string _tag;

//		public MySimplePoint() : this( PointPair.Missing, PointPair.Missing, null )
//		{
//		}

		public MySimplePoint( double x, double y )
			: this( x, y, null )
		{
		}

		public MySimplePoint( double x, double y, string tag )
		{
			_x = x;
			_y = y;
			_tag = tag;
		}

		public double X
		{
			get { return _x; }
			set { _x = value; }
		}

		public double Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public string Tag
		{
			get { return _tag; }
			set { _tag = value; }
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