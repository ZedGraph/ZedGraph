using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using GDIDB;
using ZedGraph;
using System.Diagnostics;
using System.Threading;

namespace ZedGraph.LibTest
{
	public partial class Form1 : Form
	{
		private DBGraphics memGraphics;
		protected GraphPane myPane, myPane2;
		protected MasterPane master = null;
		private Axis _crossAxis;
		private bool isBinarySerialize = false;
		private static bool isFirst = true;
		private static PointF startPt;

		public Form1()
		{
			InitializeComponent();
			memGraphics = new DBGraphics();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Trace.Listeners.Add(new TextWriterTraceListener( @"myTrace.txt" ) );
			Trace.AutoFlush = true;

			memGraphics.CreateDoubleBuffer(this.CreateGraphics(),
				this.ClientRectangle.Width, this.ClientRectangle.Height);

#if true	// Multi Y Axis demo
			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Demonstration of Multi Y Graph",
				"Time, s",
				"Velocity, m/s" );
			
			// Set the titles and axis labels
			myPane.Y2Axis.Title.Text = "Acceleration, m/s2";

			// Make up some data _points based on the Sine function
			PointPairList vList = new PointPairList();
			PointPairList aList = new PointPairList();
			PointPairList dList = new PointPairList();
			PointPairList eList = new PointPairList();

			for ( int i=0; i<30; i++ )
			{
				double time = (double) i;
				double acceleration = 2.0;
				double velocity = acceleration * time;
				double distance = acceleration * time * time / 2.0;
				double energy = 100.0 * velocity * velocity / 2.0;
				aList.Add( time, acceleration );
				vList.Add( time, velocity );
				eList.Add( time, energy );
				dList.Add( time, distance );
			}

			// Generate a red curve with diamond symbols, and "Velocity" in the _legend
			LineItem myCurve = myPane.AddCurve( "Velocity",
				vList, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with circle symbols, and "Acceleration" in the _legend
			myCurve = myPane.AddCurve( "Acceleration",
				aList, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;

			// Generate a green curve with square symbols, and "Distance" in the _legend
			myCurve = myPane.AddCurve( "Distance",
				dList, Color.Green, SymbolType.Square );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the second Y axis
			myCurve.YAxisIndex = 1;

			// Generate a Black curve with triangle symbols, and "Energy" in the _legend
			myCurve = myPane.AddCurve( "Energy",
				eList, Color.Black, SymbolType.Triangle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;
			// Associate this curve with the second Y2 axis
			myCurve.YAxisIndex = 1;

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
			myPane.YAxis.Scale.Max = 100;

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
			myPane.Y2Axis.Scale.Max = 3;

			myPane.YAxis.IsVisible = true;
			//myPane.YAxis.IsTic = false;
			myPane.YAxis.MinorTic.IsOutside = false;
			myPane.YAxis.MajorTic.IsCrossOutside = false;
			myPane.YAxis.MinorTic.IsCrossOutside = false;
			myPane.YAxis.MajorTic.IsInside = false;
			myPane.YAxis.MinorTic.IsInside = false;
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;

			// Create a second Y Axis, green
			YAxis yAxis3b = new YAxis( "Test Axis" );
			myPane.YAxisList.Add( yAxis3b );
			yAxis3b.Scale.FontSpec.FontColor = Color.Brown;
			yAxis3b.Title.FontSpec.FontColor = Color.Brown;
			yAxis3b.Color = Color.Brown;
			yAxis3b.MajorTic.IsOutside = false;
			yAxis3b.MinorTic.IsOutside = false;
			yAxis3b.MajorTic.IsOpposite = false;
			yAxis3b.MinorTic.IsOpposite = false;
			//yAxis3b.IsScaleLabelsInside = true;
			yAxis3b.Title.IsTitleAtCross = false;
			yAxis3b.MajorTic.IsInside = false;
			yAxis3b.MinorTic.IsInside = false;
			yAxis3b.MajorTic.IsOpposite = false;
			yAxis3b.MinorTic.IsOpposite = false;

			// Create a second Y Axis, green
			YAxis yAxis3c = new YAxis( "Test 2 Axis" );
			myPane.YAxisList.Add( yAxis3c );
			yAxis3c.Scale.FontSpec.FontColor = Color.Brown;
			yAxis3c.Title.FontSpec.FontColor = Color.Brown;
			yAxis3c.Color = Color.Brown;
			yAxis3c.MajorTic.IsOutside = false;
			yAxis3c.MinorTic.IsOutside = false;
			yAxis3c.MajorTic.IsOpposite = false;
			yAxis3c.MinorTic.IsOpposite = false;
			//yAxis3c.IsScaleLabelsInside = true;
			yAxis3c.Title.IsTitleAtCross = false;
			yAxis3c.MajorTic.IsInside = false;
			yAxis3c.MinorTic.IsInside = false;
			yAxis3c.MajorTic.IsOpposite = false;
			yAxis3c.MinorTic.IsOpposite = false;

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
			//yAxis3.AxisGap = 0;

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
			

			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
#endif

#if false	// SampleMultiPointList Demo
			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Demo for SampleMultiPointList",
				"Time",
				"Distance Traveled" );
			SetSize();

			SampleMultiPointList myList = new SampleMultiPointList();
			myList.YData = PerfDataType.Distance;

			// note how it does not matter that we created the second list before actually
			// adding the data -- this is because the cloned list shares data with the
			// original
			SampleMultiPointList myList2 = new SampleMultiPointList( myList );
			myList2.YData = PerfDataType.Velocity;

			for ( int i=0; i<20; i++ )
			{
				double time = (double) i;
				double acceleration = 1.0;
				double velocity = acceleration * time;
				double distance = acceleration * time * time / 2.0;
				PerformanceData perfData = new PerformanceData( time, distance, velocity, acceleration );
				myList.Add( perfData );
			}


			myPane.AddCurve( "Distance", myList, Color.Blue );
			myPane.AddCurve( "Velocity", myList2, Color.Red );

#endif

#if false	// GradientByZ

			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			string[] ystr = { "one", "two", "three", "four", "five" };

			double[] x = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			double[] y = { .1, .2, .3, .4, .5, .4, .3, .2, .1, .2 };
			//double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			double[] z = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
			PointPairList list = new PointPairList( x, y, z );

			Color[] colors = { Color.Red, Color.Green, Color.Blue,
								Color.Yellow, Color.Orange };
			Fill fill = new Fill( colors );
			fill.Type = FillType.GradientByZ;
			fill.RangeMin = 1;
			fill.RangeMax = 5;

			BarItem myBar = myPane.AddBar( "My Bar", list, Color.Tomato );
			myBar.Bar.Fill = fill;
			myPane.XAxis.Type = AxisType.Ordinal;
			//myPane.YAxis.Type = AxisType.Text;
			//myPane.YAxis.TextLabels = ystr;
			//myPane.ClusterScaleWidth = 1;

			//myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// GradientByZ dual bars
			myPane = new GraphPane( new RectangleF(0,0,300,400), "Title", "X Label", "Y Label" );

			double[] xx = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; 
			double[] yy = { 1, 2, 3, 4, 5, 4, 3, 2, 1, 2 };
			double[] yy2 = { 4, 5, 7, 8, 1, 3, 5, 2, 4, 9 };
			double[] zz = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 }; 
			double[] zz2 = { 5, 1, 4, 2, 3, 4, 2, 1, 5, 5 }; 
			PointPairList list = new PointPairList( xx, yy, zz ); 
			PointPairList list2 = new PointPairList( xx, yy2, zz2 ); 
 
			Color[] colors = { Color.Red, Color.Green, Color.Blue,  
								 Color.Yellow, Color.Orange }; 
			Fill fill = new Fill( colors ); 
			fill.Type = FillType.GradientByZ; 
			fill.RangeMin = 1; 
			fill.RangeMax = 5; 
 
			BarItem myBar = myPane.AddBar( "My Bar", list, Color.Tomato ); 
			myBar.Bar.Fill = fill;
			BarItem myBar2 = myPane.AddBar( "My Bar 2", list2, Color.Tomato ); 
			myBar2.Bar.Fill = fill;
			myPane.XAxis.Type = AxisType.Ordinal;
			myPane.MinBarGap = 0.1f;
			//myPane.MinClusterGap = 0;
			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// stacked bars

			Random rand = new Random();

			myPane = new GraphPane();
//			myPane.Title.Text = "My Title";
//			myPane.XAxis.Title.Text = "X Axis";
//			myPane.YAxis.Title.Text = "Y Axis";

			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			PointPairList list4 = new PointPairList();

			for ( int i=1; i<5; i++ )
			{
				double y = (double) i;
				double x1 = 100.0 + rand.NextDouble() * 100.0;
				double x2 = 100.0 + rand.NextDouble() * 100.0;
				double x3 = 100.0 + rand.NextDouble() * 100.0;
				double x4 = 100.0 + rand.NextDouble() * 100.0;

				list1.Add( x1, y );
				list2.Add( x2, y );
				list3.Add( x3, y );
				list4.Add( x4, y );
			}


			BarItem bar1 = myPane.AddBar( "Bar 1", list1, Color.Red );
			BarItem bar2 = myPane.AddBar( "Bar 2", list2, Color.Blue );
			BarItem bar3 = myPane.AddBar( "Bar 3", list3, Color.Green );
			BarItem bar4 = myPane.AddBar( "Bar 4", list4, Color.Beige );

			myPane.BarBase = BarBase.Y;
			myPane.BarType = BarType.Stack;

			myPane.AxisChange( this.CreateGraphics() );

			this.CreateStackBarLabels( myPane );
#endif

#if false	// Bars and Dates

//			Color color = Color.FromArgb( 123, 45, 67, 89 );
//			HSBColor hsbColor = new HSBColor( color );
//			Color color2 = hsbColor;



			Random rand = new Random();

			myPane = new GraphPane();
			myPane.Title.Text = "My Title";
			myPane.XAxis.Title.Text = "X Axis";
			myPane.YAxis.Title.Text = "Y Axis";
			//myPane.XAxis.Type = AxisType.Ordinal;
			//myPane.XAxis.Type = AxisType.Date;
			//myPane.ClusterScaleWidth = 0.75 / 1440.0;
			//myPane.XAxis.MinorStep = 1;
			//myPane.XAxis.MinorUnit = DateUnit.Minute;

			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			for ( int i=1; i<10; i++ )
			{
				//double x = new XDate( 1995, 5, 10, 12, i+1, 0 );
				double x = (double) i;
				double y1 = rand.NextDouble() * 100.0;
				double y2 = rand.NextDouble() * 100.0;

				list1.Add( x-0.25, y1, 0 );
				list2.Add( x+0.17, y2, 0 );
			}

			//myPane.AddCurve( "junk", list1, Color.Green );

			HiLowBarItem bar1 = myPane.AddHiLowBar( "Bar 1", list1, Color.Red );
			//bar1.Bar.Border.IsVisible = false;
			bar1.Bar.Size = 15;
			//bar1.Bar.Fill = new Fill( Color.Red );
			HiLowBarItem bar2 = myPane.AddHiLowBar( "Bar 2", list2, Color.Blue );
			//bar2.Bar.Border.IsVisible = false;
			//bar2.Bar.Fill = new Fill( Color.Blue );
			bar2.Bar.Size = 10;

			MasterPane mPane = new MasterPane();
			mPane.Add( myPane );

			myPane.AxisChange( this.CreateGraphics() );

			//this.CreateBarLabels(mPane);
#endif

#if false	// bar test with no gap
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 300 ),
				"Score Report", "", "" );
			// Make up some random data points
			string[] labels = { "" };
			double[] y = { 800, 900 };
			double[] y2 = { 500 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = myPane.AddBar( null, y, null, Color.RoyalBlue );
			
			// Generate a blue bar with "Curve 2" in the legend
			myBar = myPane.AddBar( null, y2, null, Color.Red );
			
			// Draw the X tics between the labels instead of at the labels
			myPane.YAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.YAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.YAxis.Type = AxisType.Text;
			// Fill the Axis and Pane backgrounds
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );
			myPane.PaneFill = new Fill( Color.FromArgb( 250, 250, 255) );

			myPane.BarBase = BarBase.Y;

			myPane.MinBarGap = 0;
			myPane.MinClusterGap = 1;
    
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
#endif

#if false	// Standard Sample Graph
            myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			// Set the titles and axis labels
			myPane.Title.Text = "Wacky Widget Company\nProduction Report";
			myPane.XAxis.Title.Text = "Time, Days\n(Since Plant Construction Startup)";
			myPane.YAxis.Title.Text = "Widget Production\n(units/hour)";
    
			LineItem curve;
    
			// Set up curve "Larry"
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			// Use green, with circle symbols
			curve = myPane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve with a white-green gradient
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			// Make it a smooth line
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			// Fill the symbols with white
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
    
			// Second curve is "moe"
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			// Use a red color with triangle symbols
			curve = myPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve with semi-transparent pink using the alpha value
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			// Fill the symbols with white
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
    
			// Third Curve is a bar, called "Wheezy"
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = myPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			// Fill the bars with a RosyBrown-White-RosyBrown gradient
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
    
			// Fourth curve is a bar
			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			// Fill the bars with a RoyalBlue-White-RoyalBlue gradient
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
    
			// Fill the pane background with a gradient
			myPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.FromArgb( 255, 255, 245),
				Color.FromArgb( 255, 255, 190), 90F );
    
    
			// Make each cluster 100 user scale units wide.  This is needed because the X Axis
			// type is Linear rather than Text or Ordinal
			myPane.ClusterScaleWidth = 100;
			// Bars are stacked
			myPane.BarType = BarType.Stack;
    
			// Enable the X and Y axis grids
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsShowGrid = true;
    
			// Manually set the scale maximums according to user preference
			myPane.XAxis.Max = 1200;
			myPane.YAxis.Max = 120;
    
			// Add a text item to decorate the graph
			TextItem text = new TextItem("First Prod\n21-Oct-93", 175F, 80.0F );
			// Align the text such that the Bottom-Center is at (175, 80) in user scale coordinates
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Near;
			myPane.GraphItemList.Add( text );
    
			// Add an arrow pointer for the above text item
			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			myPane.GraphItemList.Add( arrow );
    
			// Add a another text item to to point out a graph feature
			text = new TextItem("Upgrade", 700F, 50.0F );
			// rotate the text 90 degrees
			text.FontSpec.Angle = 90;
			// Align the text such that the Right-Center is at (700, 50) in user scale coordinates
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			// Disable the border and background fill options for the text
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			myPane.GraphItemList.Add( text );
    
			// Add an arrow pointer for the above text item
			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			myPane.GraphItemList.Add( arrow );
    
			// Add a text "Confidential" stamp to the graph
			text = new TextItem("Confidential", 0.85F, -0.03F );
			// use AxisFraction coordinates so the text is placed relative to the ChartRect
			text.Location.CoordinateFrame = CoordType.AxisFraction;
			// rotate the text 15 degrees
			text.FontSpec.Angle = 15.0F;
			// Text will be red, bold, and 16 point
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			// Disable the border and background fill options for the text
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			// Align the text such the the Left-Bottom corner is at the specified coordinates
			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			myPane.GraphItemList.Add( text );
    
			// Add a BoxItem to show a colored band behind the graph data
			BoxItem box = new BoxItem( new RectangleF( 0, 110, 1200, 10 ),
				Color.Empty, Color.FromArgb( 225, 245, 225) );
			box.Location.CoordinateFrame = CoordType.AxisXYScale;
			// Align the left-top of the box to (0, 110)
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			// place the box behind the axis items, so the grid is drawn on top of it
			box.ZOrder = ZOrder.E_BehindAxis;
			myPane.GraphItemList.Add( box );
    
			// Add some text inside the above box to indicate "Peak Range"
			TextItem myText = new TextItem( "Peak Range", 1170, 105 );
			myText.Location.CoordinateFrame = CoordType.AxisXYScale;
			myText.Location.AlignH = AlignH.Right;
			myText.Location.AlignV = AlignV.Center;
			myText.FontSpec.IsItalic = true;
			myText.FontSpec.IsBold = false;
			myText.FontSpec.Fill.IsVisible = false;
			myText.FontSpec.Border.IsVisible = false;
			myPane.GraphItemList.Add( myText );
    
			// Calculate the Axis Scale Ranges
			Graphics g = this.CreateGraphics();
			myPane.AxisChange( g );
			g.Dispose();
#endif

#if false	// MasterPane
			master = new MasterPane( "ZedGraph MasterPane Example", new Rectangle( 10, 10, 10, 10 ) );

			master.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			//master.IsShowTitle = true;

			//master.MarginAll = 10;
			//master.InnerPaneGap = 10;
			//master.Legend.IsVisible = true;
			//master.Legend.Position = LegendPos.TopCenter;
			
/*
			TextItem text = new TextItem( "Priority", 0.88F, 0.12F );
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
			master.GraphItemList.Add( text );

			text = new TextItem("DRAFT", 0.5F, 0.5F );
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
			
			master.GraphItemList.Add( text );
*/
			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j=0; j<6; j++ )
			{
				// Create a new graph with topLeft at (40,40) and size 600x400
				GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"Case #" + (j+1).ToString(),
					"Time, Days",
					"Rate, m/s" );

				myPaneT.PaneFill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPaneT.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i=0; i<36; i++ )
				{
					x = (double) i + 5;
					y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 + (double) j ) );
					list.Add( x, y );
				}

				LineItem myCurve = myPaneT.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( myPaneT );
			}

			myPane = master[0];

			Graphics g = this.CreateGraphics();
			
			//master.AutoPaneLayout( g, PaneLayout.ExplicitRow32 );
			//master.AutoPaneLayout( g, 2, 4 );
			master.AutoPaneLayout( g, false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
			master.AxisChange( g );

			g.Dispose();
#endif


#if false	// MasterPane - Single Pane
			master = new MasterPane( "ZedGraph MasterPane Single Pane Example", new Rectangle( 10, 10, 10, 10 ) );

			master.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"Case 1",
				"Time, Days",
				"Rate, m/s" );

			myPaneT.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
			myPaneT.BaseDimension = 6.0F;

			// Make up some data arrays based on the Sine function
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				x = (double) i + 5;
				y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
				list.Add( x, y );
			}

			LineItem myCurve = myPaneT.AddCurve( "Type 1",
				list, Color.Blue, SymbolType.Circle );
			myCurve.Symbol.Fill = new Fill( Color.White );

			master.Add( myPaneT );

			Graphics g = this.CreateGraphics();

			master.Title.IsVisible = false;
			master.Margin.All = 0;
			//master.AutoPaneLayout( g, PaneLayout.ExplicitRow32 );
			//master.AutoPaneLayout( g, 2, 4 );
			master.AutoPaneLayout( g );
			//master.AutoPaneLayout( g, false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
			master.AxisChange( g );

			g.Dispose();
#endif

#if false	// Pie
            myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"2004 ZedGraph Sales by Region\n($M)",
				"",
				"" );

			myPane.FontSpec.IsItalic = true;
			myPane.FontSpec.Size = 24f;
			myPane.FontSpec.Family = "Times";
			myPane.PaneFill = new Fill( Color.White, Color.Goldenrod, 45.0f );
			myPane.Chart.Fill.Type = FillType.None;
			myPane.Legend.Position = LegendPos.Float ;
			myPane.Legend.Location = new Location( 0.95f, 0.15f, CoordType.PaneFraction,
								AlignH.Right, AlignV.Top );
			myPane.Legend.FontSpec.Size = 10f;
			myPane.Legend.IsHStack = false;
			
			PieItem segment1 = myPane.AddPieSlice( 20, Color.Navy, Color.White, 45f, 0, "North" );
			PieItem segment3 = myPane.AddPieSlice( 30, Color.Purple, Color.White, 45f, .0, "East" );
			PieItem segment4 = myPane.AddPieSlice( 10.21, Color.LimeGreen, Color.White, 45f, 0, "West" );
			PieItem segment2 = myPane.AddPieSlice( 40, Color.SandyBrown, Color.White, 45f, 0.2, "South" );
			PieItem segment6 = myPane.AddPieSlice( 250, Color.Red, Color.White, 45f, 0, "Europe" );
			PieItem segment7 = myPane.AddPieSlice( 50, Color.Blue, Color.White, 45f, 0.2, "Pac Rim" );
			PieItem segment8 = myPane.AddPieSlice( 400, Color.Green, Color.White, 45f, 0, "South America" );
			PieItem segment9 = myPane.AddPieSlice( 50, Color.Yellow, Color.White, 45f, 0.2, "Africa" );
			
			segment2.LabelDetail.FontSpec.FontColor = Color.Red ;
																																				
			CurveList curves = myPane.CurveList ;
			double total = 0 ;
			for ( int x = 0 ; x <  curves.Count ; x++ )
				total += ((PieItem)curves[x]).Value ;

			TextItem text = new TextItem( "Total 2004 Sales\n" + "$" + total.ToString () + "M",
								0.18F, 0.40F, CoordType.PaneFraction );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Border.IsVisible = false ;
			text.FontSpec.Fill = new Fill( Color.White, Color.FromArgb( 255, 100, 100 ), 45F );
			text.FontSpec.StringAlignment = StringAlignment.Center ;
			myPane.GraphItemList.Add( text );

			TextItem text2 = new TextItem( text );
			text2.FontSpec.Fill = new Fill( Color.Black );
			text2.Location.X += 0.008f;
			text2.Location.Y += 0.01f;
			myPane.GraphItemList.Add( text2 );
			 
			myPane.AxisChange( this.CreateGraphics() );
#endif

#if false	// simple pie
            myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"People Signed Up",
				"",
				"" );
			// Create some pie slices 
			PieItem segment1 = myPane.AddPieSlice(8, Color.Green, .3, "Signed Up"); 
			PieItem segment2 = myPane.AddPieSlice(5,Color.Red, 0.0, "Still Needed"); 
			 
			//segment1.FontSpec = new FontSpec("GenericSansSerif", 35, Color.Black, true, false, false); 
			// Sum up the values  
			CurveList curves = myPane.CurveList; 
			double total = 0; 
			for (int x = 0; x < curves.Count; x++) 
				total += ((PieItem)curves[x]).Value;

			myPane.PaneBorder.IsVisible = false;
			myPane.Legend.Border.IsVisible = false;
			myPane.Legend.Position = LegendPos.TopCenter;
			
//			ArrowItem arrow = new ArrowItem( (float) new XDate(2007,1,1), 0, (float) new XDate(2007,1,1), 50 );

#endif

#if false	// Commerical Sales Graph
			myPane = new GraphPane( new RectangleF(0,0,10,10),
						"Sales Growth Compared to\nActual Sales by Store Size - Rank Order Low to High",
						"", "" );
			myPane.MarginAll = 20;
			myPane.FontSpec.Size = 10;
			myPane.Legend.Position = LegendPos.BottomCenter;
			myPane.Legend.FontSpec.Size = 7;

			Random rand = new Random();
			double y1 = 184;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList trendList = new PointPairList();
			PointPairList projList = new PointPairList();
			string[] labels = new string[26];

			for ( int i=0; i<26; i++ )
			{
				double h1 = rand.NextDouble() * 10 - 2;
				double h2 = rand.NextDouble() * 10 - 2;
				double h3 = rand.NextDouble() * 8;
				double y2 = y1 + ( rand.NextDouble() * 2 - 1 );

				trendList.Add( i + 1.0 - 0.2, y1 );
				trendList.Add( i + 1.0 + 0.2, y2 );
				list1.Add( (double) i, y1+h1, y1 );
				list2.Add( (double) i, y2+h2, y2 );
				projList.Add( (double) i + 1.0 - 0.35, y1 + h3 );
				projList.Add( (double) i + 1.0 + 0.35, y1 + h3 );
				projList.Add( PointPair.Missing, PointPair.Missing );

				labels[i] = "Store " + (i+1).ToString();

				y1 += rand.NextDouble() * 4.0;
			}

			PointPairList minTargList = new PointPairList();
			minTargList.Add( 0.7, 218 );
			minTargList.Add( 26.3, 218 );
			PointPairList prefTargList = new PointPairList();
			prefTargList.Add( 0.7, 228 );
			prefTargList.Add( 26.3, 228 );
			
			LineItem minCurve = myPane.AddCurve("Minimum Target", minTargList, Color.Green, SymbolType.None );
			minCurve.Line.Width = 3.0f;
			minCurve.IsOverrideOrdinal = true;
			LineItem prefCurve = myPane.AddCurve("Preferred Target", prefTargList, Color.Blue, SymbolType.None );
			prefCurve.Line.Width = 3.0f;
			prefCurve.IsOverrideOrdinal = true;

			LineItem projCurve = myPane.AddCurve( "Projected Sales", projList, Color.Orange, SymbolType.None );
			projCurve.IsOverrideOrdinal = true;
			projCurve.Line.Width = 3.0f;

			LineItem myCurve = myPane.AddCurve( "Trendline", trendList,
							Color.FromArgb( 50, 50, 50 ), SymbolType.None );
			myCurve.Line.Width = 2.5f;
			myCurve.Line.IsSmooth = true;
			myCurve.Line.SmoothTension = 0.3f;
			myCurve.IsOverrideOrdinal = true;

			BarItem myBar = myPane.AddBar( "Store Growth", list1, Color.Black );
			//myBar.Bar.Fill = new Fill( Color.Black );
			BarItem myBar2 = myPane.AddBar( "Average Growth", list2, Color.LightGray );
			//myBar2.Bar.Fill = new Fill( Color.LightGray );

			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.TextLabels = labels;
			myPane.XAxis.Scale.FontSpec.Angle = -90;
			myPane.XAxis.Scale.FontSpec.Size = 8;
			myPane.XAxis.Scale.FontSpec.IsBold = true;
			myPane.XAxis.IsTicsBetweenLabels = true;
			myPane.XAxis.IsInsideTic = false;
			myPane.XAxis.IsOppositeTic = false;
			myPane.XAxis.IsMinorInsideTic = false;
			myPane.XAxis.IsMinorOppositeTic = false;

			myPane.YAxis.Scale.FontSpec.Size = 8;
			myPane.YAxis.Scale.FontSpec.IsBold = true;
			myPane.YAxis.IsShowGrid = true;
			myPane.YAxis.GridDashOn = 1.0f;
			myPane.YAxis.GridDashOff = 0.0f;

			myPane.BarType = BarType.ClusterHiLow;

			myPane.AxisChange( this.CreateGraphics() );
			myPane.YAxis.MinorStep = myPane.YAxis.Step;

#endif

#if false	// Basic curve test - Images as symbols
			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();

			for ( int i=0; i<10; i++ )
			{
				double x = (double) i;
				double y = Math.Sin( x / 8.0 );
				list.Add( x, y );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			Image image = Image.FromHbitmap( bm.GetHbitmap() );

			myCurve.Line.IsVisible = false;
			myCurve.Symbol.Type = SymbolType.Square;
			myCurve.Symbol.Size = 16;
			myCurve.Symbol.Border.IsVisible = false;
			myCurve.Symbol.Fill = new Fill( image, WrapMode.Clamp );

			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 359;
			trackBar1.Value = 0;

#endif

#if false	// Stick Item Test
			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();
			for ( int i=0; i<100; i++ )
			{
				double x = (double) i;
				double y = Math.Sin( i / 8.0 );
				double z = Math.Abs(Math.Cos( i / 8.0 )) * y;
				list.Add( x, y, z );
			}

			StickItem myCurve = myPane.AddStick( "curve", list, Color.Blue );
			myCurve.Line.Width = 2.0f;
			myPane.XAxis.IsShowGrid = true;
			myPane.XAxis.Max = 100;

			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 359;
			trackBar1.Value = 0;

#endif

#if false	// Basic curve test - Dual Y axes

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			myPane.Y2Axis.Title.Text = "My Y2 Axis";

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				double x = (double) i;
				double y = Math.Sin( i / 8.0 ) * 100000 + 150000;
				double y2 = Math.Sin( i / 3.0 ) * 300 - 400;
				list.Add( x, y );
				list2.Add( x, y2 );
				//double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "curve2", list2, Color.Red, SymbolType.Diamond );
			myCurve2.IsY2Axis = true;

			myPane.Y2Axis.IsVisible = true;

			AlignYZeroLines( myPane, 12 );
			myPane.YAxis.IsMinorOppositeTic = false;
			myPane.Y2Axis.IsMinorOppositeTic = false;

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif


#if false	// Basic curve test - Multi-Y axes

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			myPane.AddYAxis( "Another Y Axis" );
			myPane.AddY2Axis( "Another Y2 Axis" );
			myPane.Y2Axis.Title.Text = "My Y2 Axis";
			myPane.Y2AxisList[0].IsVisible = true;
			myPane.Y2AxisList[1].IsVisible = true;

			PointPairList list = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				//double x = (double) i;
				double x = new XDate( 2001, 1, i*3 );
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
				double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myCurve.YAxisIndex = 1;

			myPane.XAxis.IsSkipLastLabel = false;
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic curve test - Date Axis

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				//double x = (double) i;
				double x = new XDate( 2005, 12, 25, 4, 5, 6, i );
				double y = Math.Sin( i / 8.0 ) * 1 + 1;
				list.Add( x, y );
				double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myPane.XAxis.IsSkipLastLabel = false;
			//myPane.XAxis.IsPreventLabelOverlap = false;
			myPane.XAxis.ScaleFormat = "dd/MM HH:mm:ss.ff";
			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.Min = new XDate( 2005, 12, 25, 4, 5, 6, 0 );
			myPane.XAxis.Max = new XDate( 2005, 12, 25, 4, 5, 6, 100 );
			myPane.XAxis.Step = 0.025;
			myPane.XAxis.MinorStep = 0.005;
			myPane.XAxis.MajorUnit = DateUnit.Second;
			myPane.XAxis.MinorUnit = DateUnit.Second;
			myPane.AxisChange( this.CreateGraphics() );

			myPane.YAxis.ScaleFormat = "0.0'%'";
			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic curve test - Date Axis w/ Time Span

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

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
			myPane.XAxis.Type = AxisType.Date;
			myPane.AxisChange( this.CreateGraphics() );

			myPane.YAxis.ScaleFormat = "0.0'%'";
			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

			myPane.PaneFill = new Fill( Color.FromArgb( 100, Color.Blue ), Color.FromArgb( 100, Color.White ), 45.0f );

#endif

#if false	// Basic curve test - DateAsOrdinal

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				//double x = (double) i;
				double x = new XDate( 2001, 1, i*3 );
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
				double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			myPane.XAxis.IsSkipLastLabel = false;
			myPane.XAxis.IsPreventLabelOverlap = false;
			myPane.XAxis.ScaleFormat = ZedGraph.Axis.Default.FormatDayDay;
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic curve test - Linear Axis

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();

			for ( int i=0; i<20; i++ )
			{
				double x = (double) i;
				double y = Math.Sin( x / 8.0 );
				double z = Math.Abs(Math.Cos( i / 8.0 )) * y;

				list.Add( x, y, z );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );

			//myPane.XAxis.Min = 1;
			//myPane.XAxis.Max = 100;
			//myPane.XAxis.IsReverse = true;
			//myPane.XAxis.Type = AxisType.Log;

			//RectangleF rect = new RectangleF( 3, 0.7, 8, 0.2 );
			myPane.AxisChange( this.CreateGraphics() );

			BoxItem m_selectionBox = new BoxItem(); // rect );
			m_selectionBox.Border.Color = Color.Orange;
			m_selectionBox.Border.IsVisible = true;
			m_selectionBox.Fill.Color = Color.LightYellow;
			m_selectionBox.Fill.Type = FillType.Solid;
			m_selectionBox.Fill.RangeMin = 1.0;
			m_selectionBox.Fill.RangeMax = 1.0;
			m_selectionBox.Fill.IsVisible = true;

			m_selectionBox.Location = new Location(
				(float)3,
				(float)myPane.YAxis.Max,
				(float)(8 - 3),
				(float)myPane.YAxis.Max - (float)myPane.YAxis.Min,
				CoordType.AxisXYScale,
				AlignH.Left,
				AlignV.Top);
			    
			m_selectionBox.IsClippedToChartRect = true;
			m_selectionBox.ZOrder = ZOrder.E_BehindAxis;
			m_selectionBox.IsVisible = true;

			myPane.GraphItemList.Add( m_selectionBox );

#endif


#if false	// Box and Whisker diagram

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			// Throw some data points on the chart for good looks
			PointPairList list = new PointPairList();
			for ( int i=0; i<20; i++ )
			{
				double x = (double) i * 5;
				double y = Math.Sin( x / 8.0 );
				list.Add( x, y );
			}
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Line.IsVisible = false;

			// Horizontal box and whisker chart
			// yval is the vertical position of the box & whisker
			double yval = 0.3;
			// pct5 = 5th percentile value
			double pct5 = 5;
			// pct25 = 25th percentile value
			double pct25 = 40;
			// median = median value
			double median = 55;
			// pct75 = 75th percentile value
			double pct75 = 80;
			// pct95 = 95th percentile value
			double pct95 = 95;

			// Draw the box
			PointPairList list2 = new PointPairList();
			list2.Add( pct25, yval, median );
			list2.Add( median, yval, pct75 );
			HiLowBarItem myBar = myPane.AddHiLowBar( "box", list2, Color.Black );
			// set the size of the box (in points, scaled to graph size)
			myBar.Bar.Size = 20;
			myBar.Bar.Fill.IsVisible = false;
			myPane.BarBase = BarBase.Y;

			// Draw the whiskers
			double[] xwhisk = { pct5, pct25, PointPair.Missing, pct75, pct95 };
			double[] ywhisk = { yval, yval, yval, yval, yval };
			PointPairList list3 = new PointPairList();
			list3.Add( xwhisk, ywhisk );
			LineItem mywhisk = myPane.AddCurve( "whisker", list3, Color.Black, SymbolType.None );

			myPane.AxisChange( this.CreateGraphics() );

#endif


#if false	// Basic curve test - Linear Axis with Many Points

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();

			for ( int i=0; i<100000; i++ )
			{
				double x = (double) i;
				double y = Math.Sin( x / 8.0 );
				double z = Math.Abs(Math.Cos( i / 8.0 )) * y;

				list.Add( x, y, z );
			}

			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.HDash );
			myCurve.Symbol.IsVisible = false;
			//myPane.XAxis.Min = 1;
			//myPane.XAxis.Max = 100;
			//myPane.XAxis.IsReverse = true;
			//myPane.XAxis.Type = AxisType.Log;

			//RectangleF rect = new RectangleF( 3, 0.7, 8, 0.2 );
			Graphics g = this.CreateGraphics();
			myPane.AxisChange( g );
			SetSize();

			int startTick = Environment.TickCount;

			myPane.Draw( g );

			int endTick = Environment.TickCount;

			MessageBox.Show( "ticks = " + ( endTick - startTick ).ToString() );
#endif

#if false	// Gantt Chart
			myPane = new GraphPane();

			myPane.Title.Text = "Gantt Chart";
			myPane.XAxis.Title.Text = "Date";
			myPane.YAxis.Title.Text = "Project";

			myPane.XAxis.Type = AxisType.Date; 
			myPane.YAxis.Type = AxisType.Text;
			myPane.BarBase = BarBase.Y;
			
			string[] labels = { "Project 1", "Project 2" };
			myPane.YAxis.TextLabels = labels;
			myPane.YAxis.IsTicsBetweenLabels = true;

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
			ppl.Add( new XDate( 2005, 11,16 ), 2.0, new XDate( 2005, 11, 26 ) );
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
#endif

#if false		// DeSerialize
				DeSerialize( out myPane, @"c:\temp\myZedGraphFile" );

				trackBar1.Minimum = 0;
				trackBar1.Maximum = 100;
				trackBar1.Value = 50;

				myPane.AxisChange( this.CreateGraphics() );
#endif

#if false			// spline test

			myPane = new GraphPane();
			PointPairList ppl = new PointPairList();

			ppl.Add( 0, 713 );
			ppl.Add( 7360, 333 );
			ppl.Add( 10333.333, 45.333336 );
			ppl.Add( 11666.667, 5 );
			ppl.Add( 12483.333, 45.333336 );
			ppl.Add( 13600, 110 );
			ppl.Add( 15800, 184.66667 );
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
			ppl.Add( 36000, 713 );

			LineItem curve = myPane.AddCurve( "test", ppl, Color.Green, SymbolType.Default );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.4F;

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;
#endif

#if false	// hilowbar test

			myPane = new GraphPane();

			myPane.Title.Text = "Bar Type Sample";
			myPane.XAxis.Title.Text = "Text Axis";
			myPane.YAxis.Title.Text = "Some Data Value";
			myPane.XAxis.Type = AxisType.Text;
			myPane.ClusterScaleWidth = 1.0;
			//myPane.BarType = BarType.Overlay;

			myPane.FontSpec.Size = 18;
			myPane.YAxis.TitleFontSpec.Size = 16;
			myPane.XAxis.TitleFontSpec.Size = 16;

			string[] labels = { "North", "South", "East", "West", "Up", "Down" };
			myPane.XAxis.TextLabels = labels;
			//Random rand = new Random();

			double[] xArray = { 3, 5, 9, 11, 16, 18 };
			double[] xArray2 = { 10, 12, 13, 15, 17, 19 };
			double[] yArray = { 10, 45, 78, 34, 15, 26 };
			double[] yArray2 = { 54, 34, 64, 24, 44, 74 };
			PointPairList list1 = new PointPairList( xArray, yArray );
			PointPairList list2 = new PointPairList( xArray2, yArray2 );
/*
			for ( int i = 0; i < 6; i++ )
			{
				double x = xArray[i];
				double y1 = rand.NextDouble() * 1.0 + .00001;
				double y2 = rand.NextDouble() * 1.0 + .00001;

				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}
*/
			HiLowBarItem bar1 = myPane.AddHiLowBar( "First", list1, Color.Blue );
			HiLowBarItem bar2 = myPane.AddHiLowBar( "Second", list2, Color.Red );
			//myPane.YAxis.Type = AxisType.Log;
			//myPane.BarType = BarType.ClusterHiLow;
			//myPane.XAxis.Scale.ScaleFormatEvent += new Scale.ScaleFormatHandler( CustomFormatter );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// Basic bar test - Linear

			myPane = new GraphPane();

			myPane.Title.Text = "BarItem Sample (BarType.ClusterHiLow)";
			myPane.XAxis.Title.Text = "Text Axis";
			myPane.YAxis.Title.Text = "Some Data Value";
			myPane.XAxis.Type = AxisType.Text;
			myPane.ClusterScaleWidth = 2.0;
			myPane.BarType = BarType.ClusterHiLow;

			myPane.FontSpec.Size = 18;
			myPane.YAxis.TitleFontSpec.Size = 16;
			myPane.XAxis.TitleFontSpec.Size = 16;

			string[] labels = { "North", "South", "East", "West", "Up", "Down" };
			myPane.XAxis.TextLabels = labels;
			//Random rand = new Random();

			//double[] xArray = { 3, 5, 9, 11, 16, 18 };
			double[] xArray = { 1, 1.8, 3.2, 4, 5, 6 };
			double[] xArray2 = { 10, 12, 13, 15, 17, 19 };
			double[] yArray = { 10, 75, 25, 16, 15, 26 };
			double[] yArray2 = { 54, 62, 44, 24, 44, 74 };
			double[] ylArray2 = { 34, 42, 15, 0, 5, 20 };
			double[] yArray3 = { 54, 62, 44, 24, 34, 74 };
			double[] ylArray3 = { 44, 42, 14, 14, 14, 34 };
			PointPairList list1 = new PointPairList( xArray, yArray, ylArray2 );
			PointPairList list2 = new PointPairList( xArray, yArray2, ylArray2 );
			PointPairList list3 = new PointPairList( xArray, yArray3, ylArray3 );
			/*
			for ( int i = 0; i < 6; i++ )
			{
				double x = xArray[i];
				double y1 = rand.NextDouble() * 1.0 + .00001;
				double y2 = rand.NextDouble() * 1.0 + .00001;

				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}
*/
			//ErrorBarItem bar1 = myPane.AddErrorBar( "First", list3, Color.Blue );
			//bar1.ErrorBar.Symbol.Size = 12;
			//bar1.ErrorBar.PenWidth = 2;
			//HiLowBarItem bar1 = myPane.AddHiLowBar( "First", list3, Color.Blue );
			//bar1.Bar.Size = 20;
			BarItem bar1 = myPane.AddBar( "First", list1, Color.Blue );
			BarItem bar2 = myPane.AddBar( "Second", list2, Color.Red );
			//myPane.YAxis.Type = AxisType.Log;
			//myPane.BarType = BarType.ClusterHiLow;
			//myPane.XAxis.Scale.ScaleFormatEvent += new Scale.ScaleFormatHandler( CustomFormatter );

			myPane.YAxis.IsTitleAtCross = false;

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// Bars - different colors thru IsOverrideOrdinal

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

/*			PointPairList list1 = new PointPairList();
			list1.Add(1,13);
			HiLowBarItem bar1 = myPane.AddHiLowBar( "First", list1, Color.Blue );
			PointPairList list2 = new PointPairList();
			list2.Add(2,22);
			HiLowBarItem bar2 = myPane.AddHiLowBar( "Second", list2, Color.Red );

			bar1.Bar.Size = 30;
			bar1.IsOverrideOrdinal = true;
			bar2.Bar.Size = 30;
			bar2.IsOverrideOrdinal = true;
*/
			PointPairList list1 = new PointPairList();
			list1.Add(1,13);
			BarItem bar1 = myPane.AddBar( "First", list1, Color.Blue );
			PointPairList list2 = new PointPairList();
			list2.Add(2,22);
			BarItem bar2 = myPane.AddBar( "Second", list2, Color.Red );

			bar1.IsOverrideOrdinal = true;
			bar2.IsOverrideOrdinal = true;

			myPane.Legend.Position = LegendPos.TopFlushLeft;

			myPane.BarType = BarType.Overlay;
			myPane.XAxis.Type = AxisType.Text;
			string[] labels = { "Label1", "Label2" };
			myPane.XAxis.TextLabels = labels;
			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic curve test - two text axes

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			double[] y = { 2, 4, 1, 5, 3 };

			LineItem myCurve = myPane.AddCurve( "curve 1", null, y, Color.Blue, SymbolType.Diamond );
			myCurve.IsOverrideOrdinal = true;
			myPane.XAxis.Type = AxisType.Text;
			myPane.YAxis.Type = AxisType.Text;

			string[] xLabels = { "one", "two", "three", "four", "five" };
			string[] yLabels = { "alpha", "bravo", "charlie", "delta", "echo" };
			//myPane.XAxis.TextLabels = xLabels;
			//myPane.YAxis.TextLabels = yLabels;

			myPane.AxisChange( this.CreateGraphics() );


			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic horizontal bar test

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList list = new PointPairList();

			for ( int i=0; i<5; i++ )
			{
				double y = (double) i;
				//double x = new XDate( 2001, 1, i*3 );
				double x = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
				//double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
			}

			PointPairList list2 = new PointPairList( list );
			PointPairList list3 = new PointPairList( list );
			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );

			//myPane.XAxis.IsSkipLastLabel = false;
			//myPane.XAxis.IsPreventLabelOverlap = false;
			//myPane.XAxis.ScaleFormat = "dd/MM HH:mm";
			//myPane.XAxis.Type = AxisType.Date;
			myPane.BarType = BarType.PercentStack;
			myPane.BarBase = BarBase.Y;
			myPane.AxisChange( this.CreateGraphics() );

			ValueHandler valueHandler = new ValueHandler(myPane, true);
			const float shift = 0;
			int iOrd = 0;
			foreach (CurveItem oCurveItem in myPane.CurveList)
			{
				BarItem oBarItem = oCurveItem as BarItem;
				if (oBarItem != null)
				{
					PointPairList oPointPairList = oCurveItem.Points as PointPairList;
					for (int i=0; i<oPointPairList.Count; i++)
					{
						double xVal = oPointPairList[i].X;
						string sLabel = string.Concat(xVal.ToString("F0"), "%");

						double yVal = valueHandler.BarCenterValue(oCurveItem, oCurveItem.GetBarWidth(myPane), i, oPointPairList[i].Y, iOrd);
						double x1, x2, y;
						valueHandler.GetValues( oCurveItem, i, out y, out x1, out x2 );

						xVal = ( x1 + x2 ) / 2.0;

						TextItem oTextItem = new TextItem(sLabel, (float) xVal + (xVal > 0 ? shift : -shift ), (float) yVal);
						oTextItem.Location.CoordinateFrame = CoordType.AxisXYScale;
						oTextItem.Location.AlignH =  AlignH.Center;
						oTextItem.Location.AlignV = AlignV.Center;
						oTextItem.FontSpec.Border.IsVisible = true;
						oTextItem.FontSpec.Angle = 0;
						oTextItem.FontSpec.Fill.IsVisible = false;
						myPane.GraphItemList.Add(oTextItem);
					}
				}

				iOrd++;
			}

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// vertical bars with labels

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

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

			//myPane.XAxis.IsReverse = true;
			myPane.AxisChange( this.CreateGraphics() );
			myPane.XAxis.IsTicsBetweenLabels = true;
			string[] labels = { "one", "two", "three", "four", "five" };
			myPane.XAxis.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;
			//myPane.XAxis.Step = 3;
			myPane.XAxis.IsAllTics = false;

			ArrowItem tic = new ArrowItem( Color.Black, 1.0f, 2.5f, 0.99f, 2.5f, 1.01f );
			tic.IsArrowHead = false;
			tic.Location.CoordinateFrame = CoordType.XScaleYAxisFraction;
			myPane.GraphItemList.Add( tic );


			CreateBarLabels( myPane );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic curve test - log/exponential axis
			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			PointPairList ppl1 = new PointPairList();
			PointPairList ppl2 = new PointPairList();

			for ( int i=0; i<100; i++ )
			{
				double x = (double) i * 1.52 + 0.001;
				double x2 = x*10000;
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				double y2 = Math.Sin( i / 8.0 ) * 100000 + 100001;
				double z = Math.Abs( Math.Cos( i / 8.0 ) ) * y;
				ppl1.Add( x, y, z );
				ppl2.Add( x2, y2, z );
			}

			LineItem myCurve = myPane.AddCurve( "curve", ppl1, Color.Blue, SymbolType.Diamond );
			LineItem myCurve2 = myPane.AddCurve( "curve2", ppl2, Color.Red, SymbolType.Triangle );

			myPane.XAxis.IsUseTenPower = false;
			myPane.XAxis.Type = AxisType.Log;
			myPane.XAxis.Exponent = 0.3;

			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// Basic curve test
			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			double[] x = new double[100];
			double[] y = new double[100];

			for ( int i=0; i<100; i++ )
			{
				x[i] = (double) i;
				y[i] = Math.Sin( i / 8.0 ) * 100000 + 100001;
				double z = Math.Abs(Math.Cos( i / 8.0 )) * y[i];
			}
			BasicArrayPointList list = new BasicArrayPointList( x, y );

			//LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			//myCurve.Symbol.IsVisible = true;
			//myCurve.IsY2Axis = true;
			//myPane.Y2Axis.IsVisible = true;
			//myPane.YAxis.Type = AxisType.Log;
			//myPane.YAxis.IsScaleVisible = false;
			//myPane.YAxis.IsShowTitle = false;
			//myPane.MarginLeft = 50;

			//TextItem text = new TextItem("5000", -0.01f, 5000f, CoordType.XAxisFractionYScale, AlignH.Right, AlignV.Center );
			//text.FontSpec.Border.IsVisible = false;
			//text.FontSpec.Fill.IsVisible = false;
			//myPane.GraphItemList.Add( text );

			//TextItem text2 = new TextItem( "My Title", 0.01f, 0.5f, CoordType.XPaneFractionYAxisFraction,
			//	AlignH.Center, AlignV.Top );
			//text2.FontSpec.Border.IsVisible = false;
			//text2.FontSpec.Fill.IsVisible = false;
			//text2.FontSpec.Angle = 90f;
			//myPane.GraphItemList.Add( text2 );

			//myPane.YAxis.IsVisible = false;
			//myPane.Y2Axis.Title.Text = "Y2 Axis";
			//myPane.XAxis.BaseTic = 1;
			//myPane.XAxis.Step = 5;
			//myPane.Y2Axis.Cross = 60;
            //myPane.YAxis.IsScaleLabelsInside = true;
			//myPane.Y2Axis.IsShowGrid = true;
			//myPane.XAxis.IsShowGrid = true;
			//myPane.YAxis.IsSkipFirstLabel = true;
			myPane.XAxis.IsSkipLastLabel = true;
			//myPane.XAxis.IsSkipLastLabel = true;
			//myPane.XAxis.IsReverse = true;
			//myPane.AxisBorder.IsVisible = false;
			//myPane.XAxis.Type = AxisType.Log;

			PointF[] polyPts = new PointF[7];
			polyPts[0] = new PointF( 30f, 0.2f );
			polyPts[1] = new PointF( 25f, 0.4f );
			polyPts[2] = new PointF( 27f, 0.6f );
			polyPts[3] = new PointF( 30f, 0.8f );
			polyPts[4] = new PointF( 35f, 0.6f );
			polyPts[5] = new PointF( 37f, 0.4f );
			polyPts[6] = new PointF( 30f, 0.2f );
			PolyItem poly = new PolyItem( polyPts, Color.Red, Color.LightSeaGreen, Color.White );
			myPane.GraphItemList.Add( poly );

			myPane.AxisChange( this.CreateGraphics() );

			myPane.FontSpec.IsDropShadow = true;
			myPane.FontSpec.DropShadowColor = Color.Red;
			myPane.FontSpec.Border.IsVisible = true;
			myPane.FontSpec.Fill = new Fill( Color.White, Color.LightGoldenrodYellow );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

#if false	// repetitive points
			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			double[] Track_DateTime_Xaxis = {1};
			double[] Y_processed_axis = {10};

			LineItem myCurve = myPane.AddCurve( "Curve Legend", Track_DateTime_Xaxis, Y_processed_axis,
				Color.DarkRed );
			myCurve.Symbol.Fill = new Fill( Color.Red );
			myPane.XAxis.Max = 1;
			myPane.YAxis.IsShowGrid = true;
			myPane.YAxis.IsShowMinorGrid = true;
			//myPane.AxisChange( g );

#endif

#if false	// masterpane test

			master = new MasterPane( "ZedGraph MasterPane Example", 
				new Rectangle( 10, 10, this.Width-20, this.Height-100 ) );

			master.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );

			master.Legend.IsVisible = true;
			master.Legend.Position = LegendPos.TopCenter;

			TextItem text = new TextItem( "Priority", 0.88F, 0.12F );
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
			master.GraphItemList.Add( text );

			text = new TextItem( "DRAFT", 0.5F, 0.5F );
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
			master.GraphItemList.Add( text );

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j=0; j<8; j++ )
			{
				// Create a new graph - rect dimensions do not matter here, since it
				// will be resized by MasterPane.AutoPaneLayout()
				GraphPane newPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
					"Case #" + (j+1).ToString(),
					"Time, Days",
					"Rate, m/s" );

				newPane.PaneFill = new Fill( Color.PowderBlue, Color.LightYellow, 45.0F );
				newPane.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i=0; i<36; i++ )
				{
					x = (double) i + 5;
					y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 + (double) j ) );
					list.Add( x, y );
				}

				LineItem myCurve = newPane.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( newPane );
			}

			Graphics g = this.CreateGraphics();

			master.AutoPaneLayout( g, PaneLayout.SquareRowPreferred);
			master.AxisChange( g );
#endif

#if false
// Create a new GraphPane

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] x = { 100, 115, 75, 22, 98, 40 };
			double[] x2 = { 120, 175, 95, 57, 113, 110 };
			double[] x3 = { 204, 192, 119, 80, 134, 156 };
			BarItem myCurve = myPane.AddBar( "Here", x, null, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90f );
			myCurve = myPane.AddBar( "There", x2, null, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90f );
			myCurve = myPane.AddBar( "Elsewhere", x3, null, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90f );
			myPane.YAxis.IsTicsBetweenLabels = true;
			myPane.YAxis.TextLabels = labels;
			myPane.YAxis.Type = AxisType.Text;
			myPane.BarType = BarType.Stack;
			myPane.BarBase = BarBase.Y;
			myPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166), 45.0F );


#endif

#if false	// ordinal demo

			myPane = new GraphPane( new RectangleF( 0, 0, 300, 200 ), "Ordinal Demo", "X Value (ordinal)", "Y Value" );

			PointPairList list = new PointPairList();
			list.Add( 10, 50 );
			list.Add( 11, 24 );
			list.Add( 20, 75 );
			list.Add( 21, 62 );

			LineItem myCurve = myPane.AddCurve( "Curve", list, Color.Blue, SymbolType.Diamond );
			myCurve.Symbol.Fill = new Fill( Color.White );

			myPane.FontSpec.Size = 24;
			myPane.XAxis.TitleFontSpec.Size = 18;
			myPane.XAxis.Scale.FontSpec.Size = 18;
			myPane.YAxis.TitleFontSpec.Size = 18;
			myPane.YAxis.Scale.FontSpec.Size = 18;
			myPane.XAxis.Type = AxisType.Ordinal;
			myPane.AxisChange( this.CreateGraphics() );

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;

#endif

			if ( master != null )
				_crossAxis = master[0].Y2Axis;
			else
				_crossAxis = myPane.YAxisList[1];

			trackBar1.Minimum = 0;
			trackBar1.Maximum = 100;
			trackBar1.Value = 50;
			UpdateControls();
			SetSize();

			//this.WindowState = FormWindowState.Maximized ;
			if ( this.myPane != null )
				this.myPane.AxisChange( this.CreateGraphics() );
      
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			SetSize();
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			Rectangle paneRect = this.ClientRectangle;
			paneRect.Inflate( -20, -20 );
			paneRect.Y += 30;
			paneRect.Height -= 30;

			Graphics g = this.CreateGraphics();
			memGraphics.CreateDoubleBuffer( g, this.ClientRectangle.Width, this.ClientRectangle.Height );

			if ( this.master != null )
				this.master.ReSize( g, paneRect );
			else if ( this.myPane != null )
				this.myPane.ReSize( g, paneRect );

			Invalidate();
			g.Dispose();
		}

		private void SetSize()
		{
			Rectangle paneRect = this.ClientRectangle;
			paneRect.Inflate( -20, -20 );
			paneRect.Y += 30;
			paneRect.Height -= 30;

			Graphics g = this.CreateGraphics();
			memGraphics.CreateDoubleBuffer( g, this.ClientRectangle.Width, this.ClientRectangle.Height );

			if ( this.master != null )
				this.master.ReSize( g, paneRect );
			else if ( this.myPane != null )
				this.myPane.ReSize( g, paneRect );

			Invalidate();
			g.Dispose();
		}

		public string CustomFormatter( GraphPane pane, Axis axis, double val, int index )
		{
			string label = val.ToString( "e1" ) + " gal";
			return label;
		}

		/// <summary>
		/// Align the zero lines and major tics of the Y and Y2 axes.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane" /> that contains the axes to be aligned</param>
		/// <param name="maxSteps">The maximum allowable number of steps.  This is provided to make sure
		/// that the number of steps does not get ridiculous, which can happen if the data values are small
		/// spans which lie a large distance away from the zero line.</param>
		public void AlignYZeroLines( GraphPane pane, int maxSteps )
		{
			Graphics g = this.CreateGraphics();

			// Get references to the Y axes to save some typing
			Axis yAxis = pane.YAxis;
			Axis y2Axis = pane.Y2Axis;

			// Reset both Y axes to autoscaling mode (this also calls AxisChange())
			yAxis.ResetAutoScale( pane, g );
			y2Axis.ResetAutoScale( pane, g );

			g.Dispose();

			//Calculate the number of steps for the Y axis in both directions
			int plusYSteps = (int)( yAxis.Scale.Max > 0 ? yAxis.Scale.Max / yAxis.Scale.MajorStep : 0 );
			int minusYSteps = (int)( yAxis.Scale.Min < 0 ? -yAxis.Scale.Min / yAxis.Scale.MajorStep : 0 );

			//Calculate the number of steps for the Y2 axis in both directions
			int plusY2Steps = (int)( y2Axis.Scale.Max > 0 ? y2Axis.Scale.Max / y2Axis.Scale.MajorStep : 0 );
			int minusY2Steps = (int)( y2Axis.Scale.Min < 0 ? -y2Axis.Scale.Min / y2Axis.Scale.MajorStep : 0 );

			//Make the number of steps above the zero line match
			if ( plusYSteps > plusY2Steps )
				y2Axis.Scale.Max += y2Axis.Scale.MajorStep * ( plusYSteps - plusY2Steps );
			else if ( plusY2Steps > plusYSteps )
				yAxis.Scale.Max += yAxis.Scale.MajorStep * ( plusY2Steps - plusYSteps );

			//Make the number of steps below the zero line match
			if ( minusYSteps > minusY2Steps )
				y2Axis.Scale.Min -= y2Axis.Scale.MajorStep * ( minusYSteps - minusY2Steps );
			else if ( minusY2Steps > minusYSteps )
				yAxis.Scale.Min -= yAxis.Scale.MajorStep * ( minusY2Steps - minusYSteps );

			//Calculate the total number of steps
			int nSteps = (int)( ( yAxis.Scale.Max - yAxis.Scale.Min ) / yAxis.Scale.MajorStep );

			//If the total steps is outrageous (more than maxSteps), then correct it
			//Note that this may cause the graph to include fractional steps such that
			//Axis.Min and/or Axis.Max values do not lie on a even step boundary
			double factor = Math.Ceiling( (double) nSteps / (double) maxSteps );
			if ( factor > 1 )
			{
				yAxis.Scale.MajorStep *= factor;
				y2Axis.Scale.MajorStep *= factor;
			}


			// Make sure that subsequent calls to AxisChange() don't change things
			yAxis.Scale.MinAuto = false;
			yAxis.Scale.MaxAuto = false;
			yAxis.Scale.MajorStepAuto = false;
			y2Axis.Scale.MinAuto = false;
			y2Axis.Scale.MaxAuto = false;
			y2Axis.Scale.MajorStepAuto = false;
		}

		// Call this method only after calling AxisChange()
		private void CreateStackBarLabels( GraphPane graphPane )
		{
			float labelOffset = (float)( 0.02 * ( graphPane.XAxis.Scale.Max - graphPane.XAxis.Scale.Min ) );
			ValueHandler valueHandler = new ValueHandler( graphPane, true );
			int barIndex = -1;

			foreach ( CurveItem curve in graphPane.CurveList )
			{
				if ( curve is BarItem )
				{
					BarItem bar = curve as BarItem;
					barIndex++;
					float barWidth = bar.GetBarWidth( graphPane );

					IPointList points = bar.Points;

					for ( int i = 0; i < points.Count; i++ )
					{
						double labelYCoordinate = valueHandler.BarCenterValue( bar, barWidth, i, points[i].Y, barIndex );

						double baseVal, lowVal, hiVal;

						valueHandler.GetValues( bar, i, out baseVal, out lowVal, out hiVal );

						float labelXCoordinate = (float)( lowVal + hiVal ) / 2.0f;

						string barLabelText = ( points[i].X ).ToString( "N2" );

						TextObj label = new TextObj( barLabelText, (float)labelXCoordinate, (float)labelYCoordinate );

						label.Location.CoordinateFrame = CoordType.AxisXYScale;
						label.FontSpec.Size = 10;
						label.FontSpec.FontColor = Color.Black;
						label.Location.AlignH = AlignH.Left;
						label.Location.AlignV = AlignV.Center;
						label.FontSpec.Border.IsVisible = false;
						label.FontSpec.Fill.IsVisible = false;

						graphPane.GraphObjList.Add( label );
					}
				}
			}
		}

		// Call this method after calling AxisChange()
		private void CreateBarLabels( GraphPane pane )
		{
			// Make the gap between the bars and the labels = 2% of the axis range
			float labelOffset = (float)( pane.YAxis.Scale.Max - pane.YAxis.Scale.Min ) * 0.02f;

			foreach ( CurveItem curve in pane.CurveList )
			{
				BarItem bar = curve as BarItem;

				if ( bar != null )
				{
					IPointList points = curve.Points;

					for ( int i = 0; i < points.Count; i++ )
					{
						ValueHandler valueHandler		= 
							new ValueHandler( pane, true );

						int curveIndex					=
							pane.CurveList.IndexOf( curve );

						double labelXCoordintate			= 
							valueHandler.BarCenterValue( bar, 
							bar.GetBarWidth( pane ), i, points[ i ].X, 
							curveIndex );

						float labelYCoordinate				= 
							( float ) points[ i ].Y + labelOffset;

						string barLabelText				= 
							( points[ i ].Y / 1000 ).ToString( "N2" );

						TextObj label					= 
							new TextObj( barLabelText, ( float ) labelXCoordintate, 
							labelYCoordinate );

						label.Location.CoordinateFrame	= CoordType.AxisXYScale;
						label.FontSpec.Size				= 10;
						label.FontSpec.FontColor		= Color.Black;
						label.FontSpec.Angle = 90;
						label.Location.AlignH			= AlignH.Left;
						label.Location.AlignV			= AlignV.Center;
						label.FontSpec.Border.IsVisible	= false;
						label.FontSpec.Fill.IsVisible	= false;

						pane.GraphObjList.Add( label );
					}
				}
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pevent"></param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			int ticks = Environment.TickCount;

			SolidBrush brush = new SolidBrush( Color.Gray );
			if ( memGraphics.CanDoubleBuffer() )
			{
				//memGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;
				//memGraphics.g.SmoothingMode = SmoothingMode.None;

				// Fill in Background (for effieciency only the area that has been clipped)
				memGraphics.g.FillRectangle( new SolidBrush(SystemColors.Window),
					e.ClipRectangle.X, e.ClipRectangle.Y,
					e.ClipRectangle.Width, e.ClipRectangle.Height);

				// Do our drawing using memGraphics.g instead e.Graphics
		     
				memGraphics.g.FillRectangle( brush, this.ClientRectangle );

				Rectangle rect = new Rectangle( 2, 2, 100, 100 );
				LinearGradientBrush brush2 = new LinearGradientBrush( rect, Color.Red, Color.White, 45.0f );
				memGraphics.g.FillRectangle( brush2, rect );

				Matrix mat = memGraphics.g.Transform;

				if ( master != null )
					master.Draw( memGraphics.g );
				else
					myPane.Draw( memGraphics.g );
		   
				// Render to the form
				memGraphics.Render( e.Graphics );
				memGraphics.g.Transform = mat;
			}
			else	// if double buffer is not available, do without it
			{
				Matrix mat = e.Graphics.Transform;
				e.Graphics.FillRectangle( brush, this.ClientRectangle );
				//e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

				if ( master != null )
					master.Draw( e.Graphics );
				else
					myPane.Draw( e.Graphics );

				e.Graphics.Transform = mat;
			}

			ticks = Environment.TickCount - ticks;
			if ( showTicks )
			{
				showTicks = false;
				MessageBox.Show( "Ticks is " + ticks );
			}
		}

		bool showTicks = false;

		private void CopyToPNG( PaneBase thePane )
		{
			if ( thePane != null )
				thePane.GetImage().Save(@"c:\zedgraph.png", System.Drawing.Imaging.ImageFormat.Png);
		}

		private void CopyToGif( GraphPane thePane )
		{
			if ( thePane != null )
				thePane.GetImage().Save( @"c:\zedgraph.gif", ImageFormat.Gif );
		}

		private void CopyToClip( GraphPane thePane )
		{
			Graphics g = this.CreateGraphics();
			IntPtr hdc = g.GetHdc();
			//metaFile = new Metafile( hdc, EmfType.EmfPlusDual, "ZedGraph" );
			Metafile metaFile = new Metafile( hdc, EmfType.EmfPlusOnly );
			g.ReleaseHdc( hdc );
			g.Dispose();

			Graphics gMeta = Graphics.FromImage( metaFile );
			thePane.Draw( gMeta );
			gMeta.Dispose();


			//You can call this function with code that is similar to the following code:
			//ClipboardMetafileHelper.PutEnhMetafileOnClipboard( this.Handle, metaFile );

			IntPtr hMeta = metaFile.GetHenhmetafile();
			System.Windows.Forms.Clipboard.SetDataObject( hMeta, true  );

			MessageBox.Show( "Copied to ClipBoard" );
		}


		public class ClipboardMetafileHelper
		{
			[DllImport("user32.dll")]
			static extern bool OpenClipboard(IntPtr hWndNewOwner);
			[DllImport("user32.dll")]
			static extern bool EmptyClipboard();
			[DllImport("user32.dll")]
			static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
			[DllImport("user32.dll")]
			static extern bool CloseClipboard();
			[DllImport("gdi32.dll")]
			static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);
			[DllImport("gdi32.dll")]
			static extern bool DeleteEnhMetaFile(IntPtr hemf);
	
			// Metafile mf is set to a state that is not valid inside this function.
			static public bool PutEnhMetafileOnClipboard( IntPtr hWnd, Metafile mf )
			{
				bool bResult = false;
				IntPtr hEMF, hEMF2;
				hEMF = mf.GetHenhmetafile(); // invalidates mf
				if( ! hEMF.Equals( new IntPtr(0) ) )
				{
					hEMF2 = CopyEnhMetaFile( hEMF, new IntPtr(0) );
					if( ! hEMF2.Equals( new IntPtr(0) ) )
					{
						if( OpenClipboard( hWnd ) )
						{
							if( EmptyClipboard() )
							{
								IntPtr hRes = SetClipboardData( 14 /*CF_ENHMETAFILE*/, hEMF2 );
								bResult = hRes.Equals( hEMF2 );
								CloseClipboard();
							}
						}
					}
					DeleteEnhMetaFile( hEMF );
				}
				return bResult;
			}
		}
		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			//clone the pane so the _rect can be changed for printing
			//PaneBase printPane = (PaneBase) master.Clone();
			//GraphPane printPane = (GraphPane) myPane.Clone();
			//printPane.Rect = new RectangleF( 50, 50, 400, 300 );

			//printPane.Legend.IsVisible = true;
			//printPane.Rect = new RectangleF( 50, 50, 300, 300 );
			//printPane.ReSize( e.Graphics, new RectangleF( 50, 50, 300, 300 ) );
				
			//e.Graphics.PageScale = 1.0F;
			//printPane.BaseDimension = 2.0F;
			myPane.Draw( e.Graphics );
		}

		private void DoPrintPreview()
		{
			PrintDocument pd = new PrintDocument();

			PrintPreviewDialog ppd = new
				PrintPreviewDialog();
			pd.PrintPage += new
				PrintPageEventHandler( Graph_PrintPage );
			ppd.Document = pd;
			ppd.Show();
		}

		private void DoPrint()
		{
			PrintDocument pd = new PrintDocument();
			pd.PrintPage += new
				PrintPageEventHandler( Graph_PrintPage );
			pd.Print();
		}

		private void DoPageSetup()
		{
			PrintDocument pd = new PrintDocument();
			pd.PrintPage += new
				PrintPageEventHandler( Graph_PrintPage );
			PageSetupDialog setupDlg = new PageSetupDialog();
			setupDlg.Document = pd;
			setupDlg.ShowDialog();
		}

		private void Serialize( GraphPane myPane, string fileName )
		{
			IFormatter mySerializer;
			Stream myWriter;

			if ( isBinarySerialize )
			{
				mySerializer = new BinaryFormatter();
				myWriter = new FileStream( fileName + ".bin", FileMode.Create,
					FileAccess.Write, FileShare.None );
			}
			else
			{
				mySerializer = new SoapFormatter();
				myWriter = new FileStream( fileName + ".soap", FileMode.Create,
					FileAccess.Write, FileShare.None );
			}

			if ( myPane != null )
			{
				mySerializer.Serialize( myWriter, myPane );
				MessageBox.Show( "Serialized output created" );
			}

			myWriter.Close();
		}


		private void DeSerialize( out GraphPane myPane, string fileName )
		{
			IFormatter mySerializer;
			FileStream myReader;

			if ( isBinarySerialize )
			{
				mySerializer = new BinaryFormatter();
				myReader = new FileStream( fileName + ".bin", FileMode.Open,
					FileAccess.Read, FileShare.Read );
			}
			else
			{
				mySerializer = new SoapFormatter();
				myReader = new FileStream( fileName + ".soap", FileMode.Open,
					FileAccess.Read, FileShare.Read );
			}

			myPane = (GraphPane) mySerializer.Deserialize( myReader );
			Invalidate();

			myReader.Close();
		}

		private void Serialize( MasterPane master )
		{
			//SoapFormatter mySerializer = new SoapFormatter();
			//FileStream myWriter = new FileStream( @"myFileName.soap", FileMode.Create );

			BinaryFormatter mySerializer = new BinaryFormatter();
			Stream myWriter = new FileStream( "c:\\temp\\myFileName.bin", FileMode.Create,
				FileAccess.Write, FileShare.None );

			if ( master != null )
			{
				mySerializer.Serialize( myWriter, master );
				MessageBox.Show( "Serialized output created" );
			}

			myWriter.Close();
		}


		private void DeSerialize( out MasterPane master )
		{
			BinaryFormatter mySerializer = new BinaryFormatter();
			Stream myReader = new FileStream( "c:\\temp\\myFileName.bin", FileMode.Open,
				FileAccess.Read, FileShare.Read );

			master = (MasterPane) mySerializer.Deserialize( myReader );
			Invalidate();

			myReader.Close();
		}

		private void DoAddPoints()
		{
			PointPairList list = myPane.CurveList[0].Points as PointPairList;

			for ( int i=1; i<30; i++ )
			{
				double x = i;
				double y = Math.Sin( i / 8.0 ) * 100000 + 100001;
				list.Add( x, y );
				myPane.AxisChange( this.CreateGraphics() );
				//this.Refresh();
				Invalidate();
				Application.DoEvents();
				Thread.Sleep( 300 );
			}
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			PaneBase tmpPane = myPane;
			if ( master != null )
				tmpPane = master;
			tmpPane.GetImage( 400, 300, 72 ).Save( myPane.Title.Text + ".png", ImageFormat.Png );

			//image.Save( @"c:\zedgraph.png", ImageFormat.Png );
			//master.Image.Save( @"c:\zedgraph.png", ImageFormat.Png );
			return;

			Serialize( myPane, @"c:\temp\myZedGraphFile" );
			//DeSerialize( out master );
			return;

			object obj;
			int index;
			PointF mousePt = new PointF( e.X, e.Y );
			//if ( myPane.FindNearestObject( mousePt, this.CreateGraphics(), out obj, out index ) &&
			//		obj is GraphPane )
			{
				if ( isFirst )
				{
					startPt = mousePt;
					isFirst = false;
				}
				else
				{
					double x1, x2, y1, y2, y3, y4;
					myPane.ReverseTransform( startPt, out x1, out y1, out y3 );
					myPane.ReverseTransform( mousePt, out x2, out y2, out y4 );
					float width = (float)Math.Abs( x1 - x2 );
					float height = (float)Math.Abs( y1 - y2 );
					x1 = Math.Min( x1, x2 );
					y1 = Math.Max( y1, y2 );

					RectangleF rect = new RectangleF( (float)x1, (float)y1, width, height );
					BoxObj box = new BoxObj( rect, Color.Red, Color.Red );
					myPane.GraphObjList.Add( box );
					isFirst = true;
					Invalidate();
				}
			}
			//DoAddPoints();
			return;

			//DoPrint();
			//DoPageSetup();
			//DoPrintPreview();
			//return;

			//Serialize( master );
			//DeSerialize( out master );

			if ( myPane.FindNearestObject( new PointF( e.X, e.Y ), this.CreateGraphics(), out obj, out index ) )
				MessageBox.Show( obj.ToString() + " index=" + index );
			else
				MessageBox.Show( "No Object Found" );

			return;


			//myPane.XAxis.PickScale( 250, 900, myPane, this.CreateGraphics(), myPane.CalcScaleFactor() );
			//Invalidate();
			
			//showTicks = true;
			//Invalidate();

			CurveItem curve;
			int		iPt;
			if ( myPane.FindNearestPoint( new PointF( e.X, e.Y ), out curve, out iPt ) )
			{
				MessageBox.Show( curve.Label + ": " + curve[iPt].ToString() );
			}
			/*
				myText.Text = String.Format( "_label = {0}  X = {1}",
					curve.Label, curve.Points[iPt].ToString("e2") );
			else
				myText.Text = "none";
			*/
			//DoPrint();
			CopyToPNG( master );
			
			//Bitmap image = myPane.ScaledImage( 3000, 2400, 600 );
			//image.Save( @"c:\zedgraph.jpg", ImageFormat.Jpeg );
			
			//MultiImage( myPane, myPane2 );
			
			/*
			myPane.Legend.Position = LegendPos.Float;
			myPane.Legend.Location.CoordinateFrame = CoordType.PaneFraction;
			myPane.Legend.Location.AlignH = AlignH.Right;
			myPane.Legend.Location.AlignV = AlignV.Bottom;
			myPane.AxisChange( this.CreateGraphics() );
			
			this.Refresh();
			for ( float j=0; j<100; j++ )
			{
				for ( int k=0; k<1000000; k++ );
				myPane.Legend.Location.X = j / 100F;
				myPane.Legend.Location.Y = j / 100F;
				this.Refresh();
			}
			*/
			
			
			/*
			const int NUMITER = 100;
			long junk = Environment.TickCount;
			for ( int i=0; i<NUMITER; i++ )
				this.Refresh();
			junk = Environment.TickCount - junk;
			
			MessageBox.Show( "Time = " + (double) junk / (double) NUMITER + " ms/refresh" );
			*/
			
			//myPane.XAxis.TitleFontSpec.Angle += 90;
			//myPane.YAxis.TitleFontSpec.Angle += 90;
			//myPane.Y2Axis.TitleFontSpec.Angle += 90;
			//myPane.AxisChange( this.CreateGraphics() );
			//Invalidate();

			//myPane.IsIgnoreMissing = !myPane.IsIgnoreMissing;
			//			Invalidate();


			//myPane.Image.Save( @"c:\zedgraph.gif", ImageFormat.Gif );

			//myPane.XAxis.Min = 5;
			//myPane.XAxis.Max = 20;

			CopyToPNG( myPane );

			/*
						RectangleF tmpRect = myPane.ChartRect;
						tmpRect.Inflate( -50, -50 );
						myPane.ChartRect = tmpRect;
						myPane.AxisChange();
						Invalidate();
			*/



			/*
									CurveItem curve;
									int	iPt;

									if ( myPane.FindNearestPoint( new PointF( e.X, e.Y ), out curve, out iPt ) )
										MessageBox.Show( String.Format( "_label = {0}  X = {1}",
											curve.Label, curve.Points[iPt].ToString("e2") ) );
									else
										MessageBox.Show( "No Point Found" );
			*/

			/*
						double x, y, y2;

						if ( nPts < 100 && myPane.ChartRect.Contains( e.X, e.Y ) )
						{
							this.myPane.ReverseTransform( new PointF( e.X, e.Y ), out x, out y, out y2 );
							gx[nPts] = x;
							gy[nPts] = y;
							nPts++;
							this.myPane.CurveList[0].X = gx;
							this.myPane.CurveList[0].Y = gy;
							this.myPane.AxisChange();
							Invalidate();
						}
						//MessageBox.Show( "x=" + x.ToString() + "  y=" + y.ToString() + " y2=" + y2.ToString() );
			*/

			//CopyToGif( myPane );
			//CopyToEMF( myPane );
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			Axis controlAxis = myPane.XAxis;
			if ( _crossAxis is XAxis )
				controlAxis = myPane.YAxis;

			_crossAxis.Cross = (double)trackBar1.Value / 100.0 * ( controlAxis.Scale.Max - controlAxis.Scale.Min ) +
							controlAxis.Scale.Min;
			//_crossAxis.Scale.FontSpec.Angle = trackBar1.Value;
			myPane.AxisChange( this.CreateGraphics() );
			Refresh();
		}

		private void ReverseBox_CheckedChanged(object sender, EventArgs e)
		{
			_crossAxis.Scale.IsReverse = ReverseBox.Checked;
			Invalidate();
		}

		private void LabelsInsideBox_CheckedChanged(object sender, EventArgs e)
		{
			_crossAxis.Scale.IsLabelsInside = LabelsInsideBox.Checked;
			Invalidate();
		}

		private void AxisSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( AxisSelection.SelectedIndex == 0 )
				_crossAxis = myPane.XAxis;
			else if ( AxisSelection.SelectedIndex == 1 )
				_crossAxis = myPane.YAxis;
			else
				_crossAxis = myPane.Y2Axis;

			UpdateControls();
		}

		private void CrossAutoBox_CheckedChanged(object sender, EventArgs e)
		{
			_crossAxis.CrossAuto = CrossAutoBox.Checked;
			Invalidate();
		}

		private void UpdateControls()
		{
			GraphPane pane = myPane;
			if ( master != null )
				pane = master[0];

			Axis controlAxis = pane.XAxis;
			if ( _crossAxis is XAxis )
				controlAxis = pane.YAxis;

			if ( _crossAxis != null )
			{
				ReverseBox.Checked = _crossAxis.Scale.IsReverse;
				LabelsInsideBox.Checked = _crossAxis.Scale.IsLabelsInside;
				CrossAutoBox.Checked = _crossAxis.CrossAuto;

				trackBar1.Value = (int) ( Math.Abs( _crossAxis.Scale.FontSpec.Angle ) + 0.5 ) % 360;
			}
		}



	}


}