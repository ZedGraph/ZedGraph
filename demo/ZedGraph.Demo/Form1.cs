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
using GDIDB;
using ZedGraph;

namespace ZedGraphTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DBGraphics memGraphics;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuItemHowdy;
		private bool sideWays = false;

		/// <summary>
		/// 
		/// </summary>
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			memGraphics = new  DBGraphics();
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuItemHowdy = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.menuFile } );
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.menuItemHowdy } );
			this.menuFile.Text = "File";
			// 
			// menuItemHowdy
			// 
			this.menuItemHowdy.Index = 0;
			this.menuItemHowdy.Text = "Howdy";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(500, 329);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		/// <summary>
		/// 
		/// </summary>
		protected GraphPane myPane, myPane2;

		double[] gx = new double[20];
		double[] gy = new double[20];
		//		int	nPts = 20;

		private void Form1_Load(object sender, System.EventArgs e)
		{
			memGraphics.CreateDoubleBuffer(this.CreateGraphics(),
				this.ClientRectangle.Width, this.ClientRectangle.Height);

			//myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
			//	"Wacky Widget Company\nProduction Report",
			//	"Time, Days\n(Since Plant Construction Startup)",
			//	"Widget Production\n(units/hour)" );
			//myPane.AxisBackColor = Color.LightGoldenrodYellow;

			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Text Graph", "Label", "My Y Axis" );

#if false	// Empty PointPairList
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Graph\n(For CodeProject Sample)",
				"My X Axis",
				"My Y Axis" );
			// Make up some random data points
			PointPairList pointList = new PointPairList();
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				pointList, Color.Red, SymbolType.Diamond );

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// One Point Test
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Graph\n(For CodeProject Sample)",
				"My X Axis",
				"My Y Axis" );
			// Make up some random data points
			double[] x = { 100 };
			double[] y = { 20 };
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange();

			myPane.XAxis.Min -= 30.0 / 1440.0;
#endif

#if false	// single value test
			// Create a new graph
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Graph\n(For CodeProject Sample)",
				"My X Axis",
				"My Y Axis" );
				
			double[] x = { 0.4875 };
			double[] y = { -123456 };

			LineItem curve;
			curve = myPane.AddCurve( "One Value", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill.IsVisible = false;

			//myPane.XAxis.IsShowGrid = true;
			//myPane.YAxis.IsShowGrid = true;

#endif

#if false	// 1000 values test
			// Create a new graph
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Graph\n(For CodeProject Sample)",
				"My X Axis",
				"My Y Axis" );
				
			double[] x = new double[1000];
			double[] y = new double[1000];
			Random rand = new Random();
			
			for ( int i=0; i<1000; i++ )
			{
				x[i] = (double) i;
				y[i] = rand.NextDouble() * 100.0;
			}

			LineItem curve;
			curve = myPane.AddCurve( "Many Values", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill.IsVisible = false;
			curve.Symbol.IsVisible = true;
			curve.Symbol.Type = SymbolType.Square;
			curve.Symbol.Border.IsVisible = false;
			curve.Symbol.Fill = new Fill( Color.Red );
			curve.Symbol.Size = 2.0F;
			curve.Line.IsVisible = false;

			//myPane.XAxis.IsShowGrid = true;
			//myPane.YAxis.IsShowGrid = true;

#endif

#if false	// The Oct-2004 initial sample

			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Graph\n(For CodeProject Sample)",
				"My X Axis",
				"My Y Axis" );

			// Make up some data arrays based on the Sine function
			double[] x = new double[36];
			double[] y1 = new double[36];
			double[] y2 = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) i + 5;
				y1[i] = 1.5 + Math.Sin( (double) i * 0.2 );
				y2[i] = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			CurveItem myCurve = myPane.AddCurve( "Porsche",
				x, y1, Color.Red, SymbolType.Diamond );

			// Generate a blue curve with circle
			// symbols, and "Piper" in the legend
			CurveItem myCurve2 = myPane.AddCurve( "Piper",
				x, y2, Color.Blue, SymbolType.Circle );
				
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
#endif	
			
#if false	// The Oct-2004 modified initial sample
			// Change the color of the title
			myPane.FontSpec.FontColor = Color.Green;

			// Add gridlines to the plot, and make them gray
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsShowGrid = true;
			myPane.XAxis.GridColor = Color.LightGray;
			myPane.YAxis.GridColor = Color.LightGray;


			// Move the legend location
			myPane.Legend.Location = ZedGraph.LegendLoc.Bottom;

			// Make both curves thicker
			myCurve.Line.Width = 2.0F;
			myCurve2.Line.Width = 2.0F;

			// Fill the area under the curves
			myCurve.Line.Fill = new Fill( Color.White, Color.Red, 45F );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Blue, 45F );

			// Increase the symbol sizes, and fill them with solid white
			myCurve.Symbol.Size = 10.0F;
			myCurve2.Symbol.Size = 10.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve2.Symbol.Fill = new Fill( Color.White );

			// Add a background gradient fill to the axis frame
			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, -45F );

			// Add a caption and an arrow
			TextItem myText = new TextItem("Interesting\nPoint", 230F, 70F );
			myText.FontSpec.FontColor = Color.Red;
			myText.AlignH = AlignH.Center;
			myText.AlignV = AlignV.Top;
			myPane.TextList.Add( myText );
			ArrowItem myArrow = new ArrowItem( Color.Red, 12F, 230F, 70F, 280F, 55F );
			myPane.ArrowList.Add( myArrow );
#endif

#if false	// The Oct-2004 Date axis sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );
   
			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, 5, i+11 );
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
			}

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
      
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
#endif

#if false	// The Oct-2004 Text axis sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
							"My Test Text Graph", "Label", "My Y Axis" );

			// Make up some random data points
			string[] labels = { "USA", "Spain", "Qatar", "Morocco", "UK", "Uganda",
						"Cambodia", "Malaysia", "Australia", "Ecuador" };
			double[] y = new double[10];
			for ( int i=0; i<10; i++ )
			y[i] = Math.Sin( (double) i * Math.PI / 2.0 );
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
							null, y, Color.Red, SymbolType.Diamond );
			//Make the curve smooth
			myCurve.Line.IsSmooth = true;
			
			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;
			// Set the labels at an angle so they don't overlap
			myPane.XAxis.ScaleFontSpec.Angle = 40;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
#endif

#if false	// The Oct-2004 Bar Chart sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 90, 100, 95, 35, 80, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67 };
			double[] y4 = { 120, 125, 100, 40, 105, 75 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Generate a black line with "Curve 4" in the legend
			myCurve = myPane.AddCurve( "Curve 4",
				null, y4, Color.Black, SymbolType.Circle );
			myCurve.Line.Fill = new Fill( Color.White, Color.LightSkyBlue, -45F );

			// Fix up the curve attributes a little
			myCurve.Symbol.Size = 10.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
#endif

#if false	// The Oct-2004 Horizontal Bar Chart sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Horizontal Bar Graph", "My X Axis", "My Y Axis" );
			// Make up some random data points
			double[] y = { 100, 115, 75, 22, 98 };
			double[] y2 = { 90, 100, 95, 35, 80 };
			double[] y3 = { 80, 110, 65, 15, 54 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", y, null, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Red, 90F );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", y2, null, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Blue, 90F );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", y3, null, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Green, 90F );

			// Fix up the curve attributes a little
			myCurve.Symbol.Size = 10.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Draw the Y tics between the labels instead of at the labels
			myPane.YAxis.IsTicsBetweenLabels = true;

			// Set the YAxis to Ordinal type
			myPane.YAxis.Type = AxisType.Ordinal;

			// Make the bars horizontal by setting bar base axis to Y
			myPane.BarBase = BarBase.Y;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
#endif

#if false	// The Oct-2004 Stacked Bar Chart sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Stacked Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 120, 175, 95, 57, 113, 110 };
			double[] y3 = { 204, 192, 119, 80, 134, 156 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Fix up the curve attributes a little
			myCurve.Symbol.Size = 10.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			myPane.IsBarStacked = true;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
#endif

#if false	// The initial sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Graph\n(For CodeProject Sample)",
				"My X Axis",
				"My Y Axis" );
			// Make up some random data points
			double[] x = { 0.2, 2, 20, 80, 150, 400, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 40, 35, 60, 90, 25, 48, 75 };
			string[] labels = { "point\none", "two", "point\nthree", "four", "5", "6", "Point\n7", "8", "9", "Point\nten" };
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( CreateGraphics() );
		
			myPane.YAxis.Type = AxisType.Text;
			myPane.YAxis.TextLabels = labels;
			myPane.YAxis.ScaleAlign = AlignP.Inside;
			myPane.YAxis.ScaleFontSpec.StringAlignment = StringAlignment.Far;
			
			//myPane.YAxis.MaxAuto = false;
			//myPane.YAxis.MinAuto = false;
			//myPane.YAxis.StepAuto = false;
			//myPane.YAxis.Min = 50;
			//double[] x2 = { 200, 400, System.Double.MaxValue, 600, 800 };
			//double[] y2 = { 110, 110, 110, 110, 110 };
			
			//myCurve = myPane.AddCurve( "", x2, y2, Color.Black );
			//myCurve.Line.Fill = new Fill( Color.White, Color.Blue );
			//myCurve.Line.IsVisible = false;
			//myCurve.Symbol.IsVisible = false;
			// First set up your main curve, etc.
			//<your code goes here>
									// Have ZedGraph pick the scales
									myPane.AxisChange( CreateGraphics() );
									
			// Now lock down the Y axis range so it does not get modified by the fake curves that follow
			myPane.YAxis.MaxAuto = false;
			myPane.YAxis.MinAuto = false;
			myPane.YAxis.StepAuto = false;
			// Get the value that is just above the Y scale range
			double yVal = myPane.YAxis.Max + 1;
			// Make a curve for the first band
			double[] xBand = { 100, 200 };
			double[] yBand = { yVal, yVal };
			CurveItem bandCurve = myPane.AddCurve( "", xBand, yBand, Color.Black );
			// Fill the curve with the band color
			bandCurve.Line.Fill = new Fill( Color.PaleGreen );
			// Make another band from X=50 to 78
			xBand[0] = 400;
			xBand[1] = 500;
			bandCurve = myPane.AddCurve( "", xBand, yBand, Color.Black );
			bandCurve.Line.Fill = new Fill( Color.Green );


			
#endif

#if false	// the modified initial sample
			// Change the color of the title
			myPane.FontSpec.FontColor = Color.Green;

			// Add gridlines to the plot, and make them gray
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsShowGrid = true;
			myPane.XAxis.GridColor = Color.LightGray;
			myPane.YAxis.GridColor = Color.LightGray;

			// Reverse the direction of the Y Axis scale
			myPane.YAxis.IsReverse = true;

			// Move the legend location
			myPane.Legend.Location = ZedGraph.LegendLoc.Bottom;

			// Make the curve thicker
			myCurve.Line.Width = 2.0F;
			// Make the symbols blue, increase the size, and fill them with color
			myCurve.Symbol.Color = Color.Blue;
			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.IsFilled = true;

			// Add a background color to the axis frame
			myPane.AxisBackColor = Color.LightCyan;

			// Add a caption and an arrow
			TextItem myText = new TextItem("Interesting\nPoint", 230F, 70F );
			myText.FontSpec.FontColor = Color.Red;
			myText.AlignH = AlignH.Center;
			myText.AlignV = AlignV.Top;
			myPane.TextList.Add( myText );
			ArrowItem myArrow = new ArrowItem( Color.Red, 12F, 230F, 70F, 280F, 55F );
			myPane.ArrowList.Add( myArrow );

			myPane.XAxis.Type = AxisType.Log;
			myPane.XAxis.IsReverse = true;
			myPane.YAxis.Type = AxisType.Linear;
			myPane.AxisChange();
#endif

#if false	// A dual Y test
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			double[] y2 = new double[36];
			for ( int i=0; i<36; i++ )
			{
				// x[i] = (double) new XDate( 1995, i+1, 1 );
				x[i] = (double) i * 5.0;
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 ) * 16.0;
				y2[i] = y[i] * 10.5;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			//myPane.XAxis.Type = AxisType.Date;

			// Generate a blue curve with diamond
			// symbols, and "My Curve" in the legend
			myCurve = myPane.AddCurve( "My Curve 1",
				x, y2, Color.Blue, SymbolType.Circle );
			myCurve.IsY2Axis = true;
			myPane.YAxis.IsVisible = true;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.IsShowGrid = true;
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsZeroLine = false;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );

			//myPane.YAxis.Type = AxisType.Log;

#endif

#if false	// Test Hour Format
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			

			list1.Add( new XDate(2004, 10, 26, 10, 0, 0), 67 );
			list1.Add( new XDate(2004, 10, 26, 16, 0, 0), 63 );
			list1.Add( new XDate(2004, 10, 26, 23, 0, 0), 65 );

			list2.Add( new XDate(2004, 10, 26, 2, 0, 0), 49 );
			list2.Add( new XDate(2004, 10, 26, 14, 0, 0), 56 );
			list2.Add( new XDate(2004, 10, 26, 20, 0, 0), 65 );

			list3.Add( new XDate(2004, 10, 26, 10, 0, 0), 43 );
			list3.Add( new XDate(2004, 10, 26, 16, 0, 0), 55 );
			list3.Add( new XDate(2004, 10, 26, 23, 0, 0), 60 );

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				list1, Color.Red, SymbolType.Diamond );
			myCurve = myPane.AddCurve( "My Curve 2",
				list2, Color.Blue, SymbolType.Square );
			myCurve = myPane.AddCurve( "My Curve 3",
				list3, Color.Green, SymbolType.Circle );
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
			myPane.XAxis.ScaleFormat = "&mm-&dd &hh:&nn";
			myPane.XAxis.MajorUnit = DateUnit.Day;
			myPane.XAxis.MinorUnit = DateUnit.Hour;
			myPane.XAxis.Step = 0.5;
			myPane.XAxis.MinorStep = 2;
			myPane.XAxis.Min = new XDate(2004,10,26);
			myPane.XAxis.Max = new XDate(2004,10,27,4,0,0);
			myPane.AxisChange( this.CreateGraphics() );
			
#endif

#if false	// the date axis sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, i+1, 1 );
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//			myPane.AxisChange();
			
			myPane.FontSpec.Family = "Times New Roman";
			
			InstalledFontCollection inst = new InstalledFontCollection();
			FontFamily[] famList = inst.Families;

#endif

#if false	// the filled curve sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			double[] y2 = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, i+1, 1 );
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
				y2[i] = 2 * Math.Sin( (double) i * Math.PI / 15.0 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve2 = myPane.AddCurve( "My Curve 2",
				x, y, Color.Blue, SymbolType.Circle );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Red, 45F );
			myCurve2.Symbol.Fill = new Fill( Color.White );
			//myCurve2.Line.IsSmooth = true;

			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y2, Color.MediumVioletRed, SymbolType.Diamond );
			myCurve.Line.Fill = new Fill( Color.White, Color.Green );
			myCurve.Symbol.Fill = new Fill( Color.White );
			//myCurve.Line.IsSmooth = true;
			
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;

			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45F );

			myPane.Y2Axis.MinSpace = 100;
			
			//myPane.PaneFill.Color = Color.MediumTurquoise;
			//myPane.PaneFill.Type = FillType.Brush;

			//myPane.Legend.Fill.Color = Color.Fuchsia;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//			myPane.AxisChange();

#endif

#if false	// the date axis sample (stress test with NaN)
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, i+1, 1 );
				y[i] = System.Double.NaN;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//			myPane.AxisChange();

#endif

#if false	// the date label-width test
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, 1, i+1 );
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.ScaleFormat = "&dd-&mmm-&yyyy";
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// the text axis sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Text Graph", "Label", "My Y Axis" );

			// Make up some random data points
			string[] labels = { "USA", "Spain", "Qatar", "Morocco", "UK", "Uganda",
								  "Cambodia", "Malaysia", "Australia", "Ecuador" };
//			double[] y = new double[10];
//			for ( int i=0; i<10; i++ )
//				y[i] = Math.Sin( (double) i * Math.PI / 2.0 );

			PointPairList points = new PointPairList();
			double numPoints = 10.0;
			for ( double i=0; i<numPoints; i++ )
				points.Add( i / (numPoints / 10.0) + 1.0, Math.Sin( i / (numPoints / 10.0) * Math.PI / 2.0 ) );

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				points, Color.Red, SymbolType.Diamond );
			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;
			// Set the labels at an angle so they don't overlap
			myPane.XAxis.ScaleFontSpec.Angle = 0;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// The HiLow bar graph sample

			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );
			
			// Make up some random data points

			double[] y = new double[44];
			double[] yBase = new double[44];

			for ( int i=0; i<44; i++ )
			{
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
				yBase[i] = y[i] - 0.4;
			}

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, yBase, Color.Red );
			
			myPane.BarType = BarType.HiLow;
			
			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// The sideways bar graph sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", y, null, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Red, 90F );
			
			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", y2, null, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Blue, 90F );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", y3, null, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Green, 90F );

			// Draw the X tics between the labels instead of at the labels
			myPane.YAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.YAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.YAxis.Type = AxisType.Ordinal;

			myPane.YAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				double maxVal = Math.Max( Math.Max( y[i], y2[i] ), y3[i] );
				// format the label string to have 1 decimal place
				string lab = maxVal.ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (maxVal < 0 ? 0.0 : maxVal) + shift, (float) (i+1) );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.AlignH = AlignH.Left;
				text.AlignV = AlignV.Center;
				text.FontSpec.IsFramed = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 0;
				// add the TextItem to the list
				myPane.TextList.Add( text );
			}

			//myPane.XAxis.IsReverse = true;
			myPane.BarBase = BarBase.Y;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			//myPane.XAxis.Max += myPane.XAxis.Step;


//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The overlaid bar graph sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Overlay Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			for ( int i=0; i<y.GetLength(0); i++ )
				y2[i] += y[i];
			for ( int i=0; i<y2.GetLength(0); i++ )
				y3[i] += y2[i];

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1",
				null, y, Color.Red );
			// Make it a bar

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2",
				null, y2, Color.Blue );
			// Make it a bar

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3",
				null, y3, Color.Green );
			// Make it a bar

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			myPane.XAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				// format the label string to have 1 decimal place
				string lab = y3[i].ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (i+1), (float) (y3[i] < 0 ? 0.0 : y3[i]) + shift );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.Location.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.Location.AlignH = AlignH.Left;
				text.Location.AlignV = AlignV.Center;
				text.FontSpec.Border.IsVisible = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 90;
				// add the TextItem to the list
				myPane.TextList.Add( text );
			}
			
			myPane.BarBase = BarBase.X;
			myPane.BarType = BarType.Overlay;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			myPane.YAxis.Max += myPane.YAxis.Step;


//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The bar graph sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };
			/*
			double[] y = { 0, 0, 0, 0, 0, 0 };
			double[] y2 = { 0, 0, 0, 0, 0, 0 };
			double[] y3 = { 0, 0, 0, 0, 0, 0 };
			*/

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );
			//LinearGradientBrush brush = new LinearGradientBrush( new Rectangle( 0, 0, 150, 150 ),
			//	Color.White, Color.Red, 90F, true );

			//Bitmap bm = new Bitmap( @"c:\tree.png" );
			//Image image = Image.FromHbitmap( bm.GetHbitmap() );
			
			//TextureBrush brush = new TextureBrush( image, new Rectangle( 0, 0, 50, 50 ) );
			//TextureBrush brush = new TextureBrush( bm );

			//brush.WrapMode = WrapMode.Clamp;
			
			//myCurve.Bar.Fill = new Fill( brush );

 			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			//Bitmap bm2 = new Bitmap( @"c:\shark.png" );
			//TextureBrush brush2 = new TextureBrush( bm );			
			//myCurve.Bar.Fill = new Fill( brush2 );
			
			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			myPane.XAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				double maxVal = Math.Max( Math.Max( y[i], y2[i] ) , y3[i] );
				
				// format the label string to have 1 decimal place
				string lab = maxVal.ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (i+1), (float) (maxVal < 0 ? 0.0 : maxVal) + shift );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.Location.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.Location.AlignH = AlignH.Left;
				text.Location.AlignV = AlignV.Center;
				text.FontSpec.Border.IsVisible = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 90;
				// add the TextItem to the list
				myPane.TextList.Add( text );
			}
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			myPane.YAxis.Max += myPane.YAxis.Step;


//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The bar graph Texture Fill sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"Cheesy Texture Fill Sample", "", "" );
			// Make up some random data points
			double[] y = { 80, 70, 65, 78, 40 };
			double[] y2 = { 70, 50, 85, 54, 63 };
			string[] str = { "North", "South", "West", "East", "Central" };

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );
			//LinearGradientBrush brush = new LinearGradientBrush( new Rectangle( 0, 0, 150, 150 ),
			//	Color.White, Color.Red, 90F, true );

			Bitmap bm = new Bitmap( @"c:\tree.gif" );
			//Image image = Image.FromHbitmap( bm.GetHbitmap() );
			
			//TextureBrush brush = new TextureBrush( image, new Rectangle( 0, 0, 50, 50 ) );
			TextureBrush brush = new TextureBrush( bm );

			//brush.WrapMode = WrapMode.Clamp;
			
			myCurve.Bar.Fill = new Fill( brush );
			myCurve.Bar.IsFramed = false;

 			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			Bitmap bm2 = new Bitmap( @"c:\shark.gif" );
			//Image image2 = Image.FromHbitmap( bm2.GetHbitmap() );
			TextureBrush brush2 = new TextureBrush( bm2 );			
			myCurve.Bar.Fill = new Fill( brush2 );
			//myCurve.Bar.Fill = new Fill( image2, WrapMode.Tile );
			myCurve.Bar.IsFramed = false;
			
			// Generate a green bar with "Curve 3" in the legend
			//myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = str;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			myPane.XAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;
			myPane.Legend.IsVisible = false;
			
			//myPane.AxisFill = new Fill( Color.White, Color.Orchid, 45F );

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			//const float shift = 5;
						
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			//myPane.YAxis.Max += myPane.YAxis.Step;


//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The bar graph sine sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );

			const int size = 41;
			double[] x = new double[size];
			double[] y = new double[size];
			double[] y2 = new double[size];
			string[] labels = new string[size];

			for ( int i=0; i<size; i++ )
			{
				x[i] = i + 1;
				y[i] = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 5.0;
				y2[i] = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 25.0;
				labels[i] = "lab" + (i+1).ToString();
			}

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a black line with "Curve 4" in the legend
			CurveItem myCurve = myPane.AddCurve( "Curve 4",
				x, y4, Color.Black, SymbolType.Circle );
			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Generate a red bar with "Curve 1" in the legend
			myCurve = myPane.AddBar( "Curve 1", x, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", x, y2, Color.Blue );


			// Draw the X tics between the labels instead of at the labels
			//myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			myPane.XAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;

//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The main example
			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			LineItem curve;
			curve = myPane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 12;
			
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = myPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 230, 145, 205), 90F );
			curve.Symbol.Size = 12;
			
			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			BarItem bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			myPane.ClusterScaleWidth = 100;
			
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			bar = myPane.AddBar( "Curly", x4, y4, Color.SteelBlue );
			myPane.ClusterScaleWidth = 100;
			myPane.BarType = BarType.Stack;
			
			//curve.Bar.Fill = new Fill( Color.Blue );
			//curve.Symbol.Size = 12;
			
			myPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			myPane.AxisFill = new Fill( Color.White, Color.FromArgb( 255, 255, 166), 90F );
			
			myPane.XAxis.IsShowGrid = true;
			//myPane.XAxis.ScaleFontSpec.Angle = 90;
			//myPane.XAxis.ScaleAlign = AlignP.Inside;
			//myPane.XAxis.IsShowMinorGrid = true;
			//myPane.XAxis.MinorGridColor = Color.Red;

			myPane.YAxis.IsShowGrid = true;
			//myPane.YAxis.ScaleFontSpec.Angle = 90;
			myPane.YAxis.Max = 120;
			//myPane.YAxis.ScaleAlign = AlignP.Center;
			//myPane.YAxis.Type = AxisType.Log;
			//myPane.YAxis.IsUseTenPower = false;
			//myPane.YAxis.IsShowMinorGrid = true;
			//myPane.YAxis.MinorGridColor = Color.Red;

			//myPane.Y2Axis.IsVisible = true;
			//myPane.Y2Axis.Max = 120;
			//myPane.Y2Axis.ScaleAlign = AlignP.Outside;
			
			TextItem text = new TextItem("First Prod\n21-Oct-93", 175F, 80.0F );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			myPane.TextList.Add( text );

			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			myPane.ArrowList.Add( arrow );

			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.Fill.IsVisible = false;
			//text.FontSpec.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, -45F );
			text.FontSpec.Border.IsVisible = false;
			myPane.TextList.Add( text );

			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			myPane.ArrowList.Add( arrow );

			text = new TextItem("Confidential", 0.8F, -0.03F );
			text.Location.CoordinateFrame = CoordType.AxisFraction;

			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;

			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			myPane.TextList.Add( text );
#endif

#if false	// second graph test
			// Create a new graph
			myPane2 = new GraphPane( new Rectangle( 40, 40, 300, 200 ),
				"My Pane 2",
				"My X Axis 2",
				"My Y Axis 2" );
				
			double[] xx = { 0.4875 };
			double[] yy = { -123456 };

			LineItem curve2;
			curve2 = myPane2.AddCurve( "One Value", xx, yy, Color.Red, SymbolType.Diamond );
			curve2.Symbol.Fill.IsVisible = false;

			//myPane.XAxis.IsShowGrid = true;
			//myPane.YAxis.IsShowGrid = true;
			
			myPane2.AxisChange( this.CreateGraphics() );

#endif

#if true	// Stacked Bar Example - RPK
         myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),"2003 Wacky Widget Sales\nBy Product",
            "Quarter",
            "Sales (KUSD)");
         SetSize();
         
			
         string [] quarters = {"Q1", "Q2", "Q3", "Q4" } ;          
         double[] y2 = { 40, 60, 70, 20 };               // red - three segments per bar
         double[] y3 = { 20, 0, 35, 40 };             //green
 //        double[] y3 = { 2, 3, 3.5, 4 };
         double[] y4 = { 30, 15, 15, 90 };         //blue

#if true                                 //           vertical stacked bars
         LineItem curve;
         curve = myPane.AddCurve( "Larry", null, y4, Color.Black, SymbolType.Circle );
         curve.Line.Width = 1.5F;
         curve.Line.IsSmooth = true;
         curve.Line.SmoothTension = 0.6F;
         curve.Symbol.Fill = new Fill( Color.White );
         curve.Symbol.Size = 12;
        
         BarItem bar = myPane.AddBar( "Widget", null, y4, Color.RoyalBlue );
         bar = myPane.AddBar( "Stridget",  null, y3, Color.LimeGreen );
         bar = myPane.AddBar( "Bridget",null, y2, Color.Red );
         myPane.XAxis.Type = AxisType.Text ;
         myPane.XAxis.TextLabels=quarters ;         
         myPane.BarBase=BarBase.X ;
         myPane.BarType=BarType.Stack;
 //        myPane.BarType=BarType.Cluster ;
//         myPane.BarType=BarType.Overlay ;
#endif

#if false                                       //horizontal stacked bars
/*
         LineItem curve;
         curve = myPane.AddCurve( "Larry", null, y4, Color.Black, SymbolType.Circle );
         curve.Line.Width = 1.5F;
         curve.Line.IsSmooth = true;
         curve.Line.SmoothTension = 0.6F;
         curve.Symbol.Fill = new Fill( Color.White );
         curve.Symbol.Size = 12;
*/
         
         
         BarItem bar = myPane.AddBar( "Widget", y4,null, Color.RoyalBlue );
         bar = myPane.AddBar( "Stridget",  y3,null, Color.LimeGreen );
         bar = myPane.AddBar( "Bridget",y2,null, Color.Red );
         myPane.BarBase=BarBase.Y ;
         myPane.YAxis.Type = AxisType.Text ;
         myPane.YAxis.TextLabels=quarters ;         
         myPane.BarType=BarType.Stack ;
 //         myPane.BarType=BarType.Cluster ;
//         myPane.BarType=BarType.Overlay ;
  #endif
         myPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
         myPane.AxisFill = new Fill( Color.White, Color.FromArgb( 255, 255, 166), 90F );
         myPane.XAxis.IsShowGrid = true;
         myPane.YAxis.IsShowGrid = true;
			
#endif

#if false	// Missing Values test
			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Years\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, PointPair.Missing, PointPair.Missing, 35, 60, 90, 25, 48, PointPair.Missing };
			double[] x2 = { 300, 400, 500, 600, 700, 800, 900 };
			double[] y2 = { PointPair.Missing, 43, 27, 62, 89, 73, 12 };
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, PointPair.Missing, 88.57, 99.9, 36.8 };

			double[] x4 = { 150, 250, 400, 520, 780, 940 };
			double[] y4 = { .03, .054, .011, .02, .14, .38 };
			double[] x5 = { 1.5, 2.5, 4, 5.2, 7.8, 9.4 };
			double[] y5 = { 157, 458, 1400, 100000, 10290, 3854 };

			CurveItem curve;
			curve = myPane.AddCurve( "Larry", x, y, Color.Red, SymbolType.Circle );
			curve.Line.Width = 2.0F;
			curve.Symbol.IsFilled = true;
			curve = myPane.AddCurve( "Moe", x3, y3, Color.Green, SymbolType.Triangle );
			curve.Symbol.IsFilled = true;
			curve = myPane.AddCurve( "Curly", x2, y2, Color.Blue, SymbolType.Diamond );
			//curve.Line.IsVisible = false;
			curve.Symbol.IsFilled = true;
			curve.Symbol.Size = 12;
/*
			double[] x6 = new double[100];
			double[] y6 = new double[1000];
			Random rand = new Random();

			for ( int i=0; i<100; i++ )
			{
				x6[i] = i / 100.0 * 1000.0;
				y6[i] = 50.0 + 50.0 * Math.Sin( i * 10.0 / 100.0 );
			}

			curve = myPane.AddCurve( "Big", x6, y6, Color.CadetBlue, SymbolType.TriangleDown );
			curve.Symbol.IsFilled = true;

			curve = myPane.AddCurve( "Bill", x4, y4, Color.Orange, SymbolType.Square );
			//			curve = myPane.AddCurve( "Ted", x5, y5, Color.Brown, SymbolType.TriangleDown);
			curve.IsY2Axis = true;
			//			curve.Line.Width = 3.0F;
			//			curve.Line.Style = DashStyle.Dot;
			//			curve.StepType = StepType.NonStep;

			//			myPane.Legend.IsVisible = false;
			//			myPane.Legend.FillColor = Color.LightBlue;
			//			myPane.Legend.IsFilled = false;
			//			myPane.Legend.FrameColor = Color.Red;
			//			myPane.Legend.FrameWidth = 3.0F;
			//			myPane.Legend.IsFramed = true;
			//			myPane.Legend.IsHStack = false;
			//			myPane.Legend.Location = LegendLoc.InsideBotRight;
*/

			myPane.PaneBackColor = Color.WhiteSmoke;
			myPane.AxisBackColor = Color.LightGoldenrodYellow;
			myPane.XAxis.IsShowGrid = true;
			myPane.XAxis.ScaleFontSpec.Angle = 0;

			//			myPane.XAxis.IsLog = true;
			//			myPane.XAxis.IsReverse = true;
			myPane.YAxis.IsShowGrid = true;
			myPane.YAxis.ScaleFontSpec.Angle = 90;
			//			myPane.YAxis.IsOppositeTic = false;
			//			myPane.YAxis.IsMinorOppositeTic = false;
			//			myPane.YAxis.GridPenWidth = 3.0F;
			//			myPane.YAxis.GridColor = Color.Blue;
			//			myPane.YAxis.GridDashOn = 3.0F;
			//			myPane.YAxis.GridDashOff = 20.0F;
			//			myPane.YAxis.Color = Color.Red;
			//			myPane.YAxis.ScaleMag = 3;

			//			myPane.YAxis.IsLog = true;
			//			myPane.YAxis.IsReverse = true;
//			myPane.Y2Axis.IsVisible = true;
//			myPane.Y2Axis.Title = "Alternate Axis";
			//			myPane.Y2Axis.Max = 12000;
			//			myPane.Y2Axis.Min = 0;
			//			myPane.Y2Axis.Step = 2000;
			//			myPane.Y2Axis.MinorStep = 200;
			//			myPane.Y2Axis.NumDec = 2;
			//			myPane.Y2Axis.IsVisible = false;
			//			myPane.Y2Axis.IsTic = false;
			//			myPane.Y2Axis.IsInsideTic = false;
			//			myPane.Y2Axis.IsOppositeTic = false;
			//			myPane.Y2Axis.IsMinorTic = false;
			//			myPane.Y2Axis.IsMinorInsideTic = false;
			//			myPane.Y2Axis.IsMinorOppositeTic = false;
			//			myPane.FontSpec.IsItalic = true;
			//			myPane.Y2Axis.IsOmitMag = true;

			TextItem text = new TextItem("First Prod\n21-Oct-93", 100F, 50.0F );
			//			text.CoordinateFrame = CoordType.PaneFraction;
			//			text.FontSpec.Angle = 35;
			//			text.FontSpec.FontColor = Color.Orange;
			text.AlignH = AlignH.Center;
			text.AlignV = AlignV.Bottom;
			//			text.FontSpec.IsFilled = true;
			text.FontSpec.FillColor = Color.PowderBlue;
			//			text.FontSpec.IsBold = false;
			//			text.FontSpec.IsItalic = false;
			//			text.FontSpec.IsUnderline = true;

			//			text.FontSpec.IsFramed = true;
			//			text.FontSpec.FrameColor = Color.Black;
			//			text.FontSpec.FrameWidth = 3.0F;
			myPane.TextList.Add( text );

			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 100F, 47F, 72F, 25F );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			myPane.ArrowList.Add( arrow );

			text = new TextItem("Upgrade", 700F, 50.0F );
			//			text.CoordinateFrame = CoordType.AxisXYScale;
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.AlignH = AlignH.Right;
			text.AlignV = AlignV.Center;
			text.FontSpec.IsFilled = true;
			text.FontSpec.FillColor = Color.LightGoldenrodYellow;
			text.FontSpec.IsFramed = false;
			//			text.FontSpec.FrameColor = Color.Black;
			myPane.TextList.Add( text );
			/*
						text = new TextItem("Text\nBox", 500F, 50.0F );
						//			text.CoordinateFrame = CoordType.AxisXYScale;
						text.FontSpec.Angle = 90;
						text.FontSpec.FontColor = Color.Black;
						text.AlignH = AlignH.Center;
						text.AlignV = AlignV.Top;
						text.FontSpec.IsFilled = true;
						text.FontSpec.FillColor = Color.Pink;
						text.FontSpec.IsFramed = true;
						//			text.FontSpec.FrameColor = Color.Black;
						myPane.TextList.Add( text );
			*/
			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			//			arrow.IsArrowHead = false;
			myPane.ArrowList.Add( arrow );

			text = new TextItem("Confidential", 0.8F, -0.03F );
			text.CoordinateFrame = CoordType.AxisFraction;

			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.IsFramed = false;
			text.FontSpec.FrameColor = Color.Red;
			text.FontSpec.IsFilled = false;

			text.AlignH = AlignH.Left;
			text.AlignV = AlignV.Bottom;
			//			text.IsFilled = true;
			//			text.BackgroundColor = Color.White;
			myPane.TextList.Add( text );
#endif

#if false	// zero value bug test
			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Years\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			double[] x = { 0, 1, 2, 3};
			double[] y = { 3, 4, -9, 10};
			double[] y1 = { 1, 2, -7, 8};


			myPane.AddBar( "tango mango" , x, y, Color.Blue );

			myPane.AddBar( "oggo boggo" , x, y1, Color.Red );
			
			//myPane.XAxis.Type = AxisType.Ordinal;

#endif

			SetSize();

			//			RectangleF myRect = myPane.CalcAxisRect( memGraphics.g );
			//			myRect.Height -= 100;
			//			myPane.AxisRect = myRect;
			//			myPane.AxisChange( this.CreateGraphics() );

			
			myPane.AxisChange( this.CreateGraphics() );

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pevent"></param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			SolidBrush brush = new SolidBrush( Color.Gray );
			if ( memGraphics.CanDoubleBuffer() )
			{
				//memGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;

				// Fill in Background (for effieciency only the area that has been clipped)
				memGraphics.g.FillRectangle( new SolidBrush(SystemColors.Window),
					e.ClipRectangle.X, e.ClipRectangle.Y,
					e.ClipRectangle.Width, e.ClipRectangle.Height);

				// Do our drawing using memGraphics.g instead e.Graphics
		     
				memGraphics.g.FillRectangle( brush, this.ClientRectangle );
				Matrix mat = memGraphics.g.Transform;
				if ( sideWays )
				{
					memGraphics.g.RotateTransform( 90 );
					memGraphics.g.TranslateTransform( 0, -this.ClientRectangle.Width );
				}

				myPane.Draw( memGraphics.g );
		   
				// Render to the form
				memGraphics.Render( e.Graphics );
				memGraphics.g.Transform = mat;
			}
			else	// if double buffer is not available, do without it
			{
				Matrix mat = e.Graphics.Transform;
				e.Graphics.FillRectangle( brush, this.ClientRectangle );
				if ( sideWays )
				{
					e.Graphics.RotateTransform( 90 );
					e.Graphics.TranslateTransform( 0, -this.ClientRectangle.Width );
				}
				myPane.Draw( e.Graphics );
				e.Graphics.Transform = mat;
			}
		}
		
		private void CopyToEMF( GraphPane myPane )
		{
			Metafile metaFile;
			Graphics g = this.CreateGraphics();
			IntPtr hdc = g.GetHdc();
			metaFile = new Metafile( @"c:\zedgraph.emf", hdc );
			g.ReleaseHdc( hdc );
			g.Dispose();

			Graphics gMeta = Graphics.FromImage( metaFile );

			myPane.Draw( gMeta );
			gMeta.Dispose();
		}

		private void CopyToPNG( GraphPane thePane )
		{
			Metafile metaFile;
			Graphics g = this.CreateGraphics();
			IntPtr hdc = g.GetHdc();
			metaFile = new Metafile( hdc, EmfType.EmfPlusOnly );
			g.ReleaseHdc( hdc );
			g.Dispose();

			Graphics gMeta = Graphics.FromImage( metaFile );

			thePane.Draw( gMeta );
			gMeta.Dispose();

			Bitmap oBitmap = new Bitmap(metaFile);
			oBitmap.Save(@"c:\zedgraph.png", System.Drawing.Imaging.ImageFormat.Png);

		}

		private void CopyToGif( GraphPane myPane )
		{
			Metafile metaFile;
			Graphics g = this.CreateGraphics();
			IntPtr hdc = g.GetHdc();
			metaFile = new Metafile( hdc, EmfType.EmfPlusOnly );
			g.ReleaseHdc( hdc );
			g.Dispose();

			Graphics gMeta = Graphics.FromImage( metaFile );

			myPane.Draw( gMeta );
			gMeta.Dispose();

			Bitmap bmap = new Bitmap( metaFile );
			bmap.Save( @"c:\zedgraph.gif", ImageFormat.Gif );
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

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			if ( this.myPane != null )
			{
				memGraphics.CreateDoubleBuffer( this.CreateGraphics(),
					this.ClientRectangle.Width, this.ClientRectangle.Height );

				SetSize();
				myPane.AxisChange( this.CreateGraphics() );
				Invalidate();
			}
		}

		private void SetSize()
		{
			if ( this.myPane != null )
			{
				Rectangle paneRect;
				if ( sideWays )
					paneRect = new Rectangle( 0, 0, this.ClientRectangle.Height, this.ClientRectangle.Width );
				else
					paneRect = this.ClientRectangle;

				//paneRect.Inflate( -10, -10 );
				this.myPane.PaneRect = paneRect;
			}
		}

		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			// clone the pane so the paneRect can be changed for printing
			GraphPane printPane = (GraphPane) myPane.Clone();

			//printPane.Legend.IsVisible = true;
			//printPane.PaneRect = new RectangleF( 50, 50,
			//	this.Size.Width+300, this.Size.Height+300 );
				
			//e.Graphics.PageScale = 1.0F;
			//printPane.BaseDimension = 2.0F;
			printPane.Draw( e.Graphics );
		}

		private void DoPrint()
		{
			PrintDocument pd = new PrintDocument();
			PrintPreviewDialog ppd = new
				PrintPreviewDialog();
			pd.PrintPage += new
				PrintPageEventHandler( Graph_PrintPage );
			ppd.Document = pd;
			ppd.Show();
		}

		private void MultiImage( GraphPane myPane, GraphPane myPane2 )
		{
			System.Drawing.Bitmap[] images;
			images = new Bitmap[2];
			images[0] = myPane.Image;
			images[1] = myPane2.Image;

			images[0].Save( @"c:\zedgraph1.jpg", ImageFormat.Jpeg );
			images[1].Save( @"c:\zedgraph2.jpg", ImageFormat.Jpeg );
		}

		private void Form1_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
		{
			MultiImage( myPane, myPane2 );
			
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
			
			//DoPrint();
			
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

			//CopyToGif( myPane );

			/*
						RectangleF tmpRect = myPane.AxisRect;
						tmpRect.Inflate( -50, -50 );
						myPane.AxisRect = tmpRect;
						myPane.AxisChange();
						Invalidate();
			*/


	
			/*
			object obj;
			int index;
			
			if ( myPane.FindNearestObject( new PointF( e.X, e.Y ), this.CreateGraphics(), out obj, out index ) )
				MessageBox.Show( obj.ToString() + " index=" + index );
			//else
			//	MessageBox.Show( "No Object Found" );
			
			if ( obj is Legend )
			{
				myPane.CurveList[index].IsVisible = !myPane.CurveList[index].IsVisible;
				Invalidate();
			}
			*/
			
			
			/*
									CurveItem curve;
									int	iPt;

									if ( myPane.FindNearestPoint( new PointF( e.X, e.Y ), out curve, out iPt ) )
										MessageBox.Show( String.Format( "label = {0}  X = {1}",
											curve.Label, curve.Points[iPt].ToString("e2") ) );
									else
										MessageBox.Show( "No Point Found" );
			*/
			
			/*
						double x, y, y2;

						if ( nPts < 100 && myPane.AxisRect.Contains( e.X, e.Y ) )
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

	}
}
