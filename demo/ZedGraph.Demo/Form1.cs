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
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 ) + 0.25;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			
			ColorBlend blend = new ColorBlend();
			blend.Colors = new Color[3];
			blend.Positions = new float[3];
			blend.Colors[0] = Color.Red;
			blend.Colors[1] = Color.White;
			blend.Colors[2] = Color.Red;
			blend.Positions[0] = 0.0F;
			// (ymax - 0 ) / ( ymax - ymin )
			blend.Positions[1] = (1.25F - 0.0F ) / ( 1.25F - -0.75F );
			blend.Positions[2] = 1.0F;

			LinearGradientBrush brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				Color.Red, Color.White, 90.0F );
			brush.InterpolationColors = blend;
			//myCurve.Line.Fill = new Fill( Color.White, Color.Red, -90 );
			myCurve.Line.Fill = new Fill( brush, true );

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

#if false	// The gradient by value test
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			PointPairList pp = new PointPairList();
			double x, y, z;
			for ( int i=0; i<36; i++ )
			{
				x = (double) new XDate( 1995, 1, i+1 );
				y = Math.Sin( (double) i * Math.PI / 15.0 );
				z = Math.Abs(y) > 0.75 ? 1.0 : 0.0;
				pp.Add( x, y, z );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "My Curve",
				pp, Color.Red, SymbolType.Diamond );
			myCurve.Symbol.Size = 14;
			myCurve.Symbol.Fill = new Fill( Color.Red, Color.Blue );

			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;
			myPane.XAxis.ScaleFormat = "dd-MMM-yyyy";
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );

			myCurve.Symbol.Fill.Type = FillType.GradientByY;
			myCurve.Symbol.Fill.RangeMin = myPane.YAxis.Min;
			myCurve.Symbol.Fill.RangeMax = myPane.YAxis.Max;

#endif


#if false	// The gradient by value test - big data
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"PVT Properties", "Pressure (atm)", "Temperature (C)" );

			// Make up some random data points
			double[] x = new double[84];
			double[] y = new double[84];
			double[] z = new double[84];

			x[0]=59.2690155133587;y[0]=13.0347597737329; z[0]=31.6707334770289;
			x[1]=75.6256579235177;y[1]=18.7338681246069; z[1]=23.3487523321607;
			x[2]=11.0948941053594;y[2]=16.9475522014175; z[2]=21.0418171109274;
			x[3]=29.6801815855515;y[3]=11.662785883584; z[3]=32.1907600255431;
			x[4]=27.7373465648711;y[4]=12.9414407814019; z[4]=28.9406131925813;
			x[5]=54.7165315342683;y[5]=18.0457514573456; z[5]=22.7676972564456;
			x[6]=66.923156520351;y[6]=17.363090132363; z[6]=24.5190833698874;
			x[7]=27.6551890140726;y[7]=13.1950716547497; z[7]=28.3905250001784;
			x[8]=41.9314339753887;y[8]=12.7919501577703; z[8]=30.6041524149724;
			x[9]=52.2144011580139;y[9]=19.6627270331092; z[9]=20.7998377171663;
			x[10]=41.8296445362445;y[10]=17.1755481566211; z[10]=22.9846226717416;
			x[11]=25.5938166635454;y[11]=12.5781665952824; z[11]=29.5391446510843;
			x[12]=45.8612662166761;y[12]=18.7490742644499; z[12]=21.3683243810907;
			x[13]=8.73577887496437;y[13]=12.6696395860345; z[13]=27.7148002883588;
			x[14]=26.4947518019312;y[14]=13.2910895071743; z[14]=28.0759037761891;
			x[15]=22.5832577514905;y[15]=12.4706834693471; z[15]=29.4834881991168;
			x[16]=38.8753015409025;y[16]=14.7696594261642; z[16]=26.3690410146156;
			x[17]=75.9330743341926;y[17]=22.5403081165937; z[17]=19.5013833703907;
			x[18]=72.008149687096;y[18]=14.382014161109; z[18]=29.8868255540371;
			x[19]=12.9547022830521;y[19]=12.0168064974744; z[19]=29.5860421565501;
			x[20]=8.99777600571237;y[20]=14.9609989137124; z[20]=23.6021107891377;
			x[21]=88.9954276654712;y[21]=17.5845342833154; z[21]=25.7685413526421;
			x[22]=34.6618137487389;y[22]=15.4061070279849; z[22]=24.9778781907313;
			x[23]=19.740116723097;y[23]=15.3967277989848; z[23]=23.7938322539581;
			x[24]=43.6384494575732;y[24]=19.8899396485174; z[24]=20.0346951788851;
			x[25]=26.1757787219191;y[25]=12.4288925985614; z[25]=29.9318994219954;
			x[26]=21.1744580740721;y[26]=12.918732213922; z[26]=28.3788937275709;
			x[27]=62.6761477197907;y[27]=16.4830150736975; z[27]=25.4722300412052;
			x[28]=21.3324403527083;y[28]=12.0528983268088; z[28]=30.3628512364698;
			x[29]=95.9188601750263;y[29]=17.4042657887662; z[29]=26.5208990663358;
			x[30]=63.0143677474208;y[30]=17.9216500282948; z[30]=23.5198374325909;
			x[31]=21.0829415516912;y[31]=15.7440225795778; z[31]=23.4069563361563;
			x[32]=20.996284938157;y[32]=14.574398709823; z[32]=25.2117710065702;
			x[33]=30.1204176704613;y[33]=16.0888404829054; z[33]=23.5817768409804;
			x[34]=31.2332045358083;y[34]=17.4424762259568; z[34]=21.907872875214;
			x[35]=44.7070230501362;y[35]=15.7937812238067; z[35]=25.1922860062714;
			x[36]=42.4034870623767;y[36]=13.6701040625502; z[36]=28.7482163481671;
			x[37]=55.9615354787538;y[37]=15.9828116744902; z[37]=25.7470615191364;
			x[38]=33.0683563132001;y[38]=15.4163646549692; z[38]=24.8341248897715;
			x[39]=41.3163203523396;y[39]=14.374576097864; z[39]=27.2893539150593;
			x[40]=59.2746567931748;y[40]=13.9984483279329; z[40]=29.5867777430474;
			x[41]=47.1373089783433;y[41]=11.4369128784326; z[41]=34.6594675796569;
			x[42]=46.2096024807582;y[42]=15.2154216802627; z[42]=26.2190276042839;
			x[43]=59.6693658721222;y[43]=20.0889628842732; z[43]=20.8275401603438;
			x[44]=42.4229368808933;y[44]=16.4586382134001; z[44]=24.0011435929977;
			x[45]=6.28541292465883;y[45]=11.7604210432682; z[45]=29.5180605118776;
			x[46]=62.3428668960823;y[46]=18.6650870471943; z[46]=22.5652333157332;
			x[47]=32.1489362138958;y[47]=14.9140468037734; z[47]=25.5810482471077;
			x[48]=50.8524048477699;y[48]=14.2046535903614; z[48]=28.4339748337717;
			x[49]=46.3889651354548;y[49]=14.9901012116761; z[49]=26.607881958616;
			x[50]=80.7758299108841;y[50]=23.5546357777381; z[50]=18.9308584812203;
			x[51]=21.1413422902109;y[51]=13.402239456366; z[51]=27.3476978415455;
			x[52]=33.1616752873533;y[52]=15.3973653024138; z[52]=24.887405625999;
			x[53]=38.2291950107296;y[53]=20.4027576108462; z[53]=19.2107430865102;
			x[54]=58.7854496029097;y[54]=13.6339407530609; z[54]=30.2896404063085;
			x[55]=70.0374387920014;y[55]=13.2373834887901; z[55]=32.201978817743;
			x[56]=39.3428635398272;y[56]=15.2504660520163; z[56]=25.6243825489507;
			x[57]=13.6835201822122;y[57]=14.9213810181659; z[57]=24.0463347946148;
			x[58]=85.4195771181699;y[58]=23.8191912472925; z[58]=18.9370362721485;
			x[59]=28.8644071788187;y[59]=18.5056427685091; z[59]=20.4846114675371;
			x[60]=98.5140770963972;y[60]=14.1154361783096; z[60]=32.7257494620468;
			x[61]=94.7034168810299;y[61]=17.2286769056374; z[61]=26.6806281654792;
			x[62]=87.2159567122151;y[62]=22.9479051380357; z[62]=19.7499247716626;
			x[63]=72.772248881859;y[63]=12.3868408971676; z[63]=34.616613174359;
			x[64]=71.6931418204309;y[64]=13.267945990599; z[64]=32.262816914466;
			x[65]=5.3721372219111;y[65]=15.0745408851388; z[65]=23.1175918138848;
			x[66]=70.1076986886622;y[66]=16.2095545747361; z[66]=26.4579025801831;
			x[67]=16.6127346545559;y[67]=12.8171274280432; z[67]=28.1400579198932;
			x[68]=10.0663248757239;y[68]=11.6343795145634; z[68]=30.2336564148105;
			x[69]=85.767706624334;y[69]=13.8406804186684; z[69]=32.2314015091442;
			x[70]=6.93979112610956;y[70]=14.8019927727885; z[70]=23.6724228970852;
			x[71]=26.6884614273252;y[71]=13.6019285616846; z[71]=27.5036370517293;
			x[72]=63.2241783216606;y[72]=13.6511646638392; z[72]=30.6464508638642;
			x[73]=97.2050756555494;y[73]=24.8563763295124; z[73]=18.7746279525116;
			x[74]=44.9931625296034;y[74]=13.8039983573787; z[74]=28.7140700312222;
			x[75]=96.9960673373471;y[75]=20.0237443863215; z[75]=23.1907128075457;
			x[76]=50.4709737973869;y[76]=16.9122212266641; z[76]=23.9469087680024;
			x[77]=32.2337485763923;y[77]=11.9434358288873; z[77]=31.7289273140442;
			x[78]=77.0916428623078;y[78]=19.9702389392266; z[78]=22.0098048379942;
			x[79]=98.4810050748141;y[79]=14.2133739141374; z[79]=32.499977272364;
			x[80]=38.7647703587753;y[80]=17.6645010401759; z[80]=22.1546224634088;
			x[81]=42.5020779370493;y[81]=17.2326352856953; z[81]=22.9651817724226;
			x[82]=8.97791512776777;y[82]=14.4327202032572; z[82]=24.4349622392316;
			x[83]=60.7691328464917;y[83]=14.9453915601231; z[83]=27.8770175020912;

			PointPairList pp = new PointPairList( y, x, z );
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "Gas Data",
				pp, Color.Red, SymbolType.Diamond );
			myCurve.Symbol.Size = 12;
			myCurve.Symbol.Fill = new Fill( Color.Red, Color.Blue );
			myCurve.Symbol.Border.IsVisible = false;
			myCurve.Symbol.Fill.Type = FillType.GradientByZ;
			myCurve.Symbol.Fill.RangeMin = 19;
			myCurve.Symbol.Fill.RangeMax = 34;
			myCurve.Line.IsVisible = false;

			TextItem text = new TextItem( "MW=34", 14, 110, CoordType.AxisXYScale );
			text.FontSpec.FontColor = Color.Blue;
			text.FontSpec.Border.IsVisible = false;
			myPane.TextList.Add( text );
			text = new TextItem( "MW=19", 25, 110, CoordType.AxisXYScale );
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.Border.IsVisible = false;
			myPane.TextList.Add( text );
			
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsShowGrid = true;
			
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
			HiLowBarItem myCurve = myPane.AddHiLowBar( "Curve 1", null, y, yBase, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
			myCurve.Bar.IsMaximumWidth = true;
			//myPane.BarType = BarType.HiLow;
			
			myPane.AxisChange( this.CreateGraphics() );

#endif

#if false	// The Error bar graph sample

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
			ErrorBarItem myCurve = myPane.AddErrorBar( "Curve 1", null, y, yBase,
						Color.Red );
			//myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
			
			//myPane.BarType = BarType.HiLow;
			myCurve.ErrorBar.Size = 0;
			myCurve.ErrorBar.PenWidth = 4;
			
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

#if false // The sorted overlay bar graph sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Overlay Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 104, 67, 18 };

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
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.ScaleFontSpec.Size = 9.0F ;
			
			myPane.BarBase = BarBase.X;
			myPane.BarType = BarType.SortedOverlay;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
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
				myPane.GraphItemList.Add( text );
			}
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			myPane.YAxis.Max += myPane.YAxis.Step;


//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The bar graph sample - color band
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			
			double[] y = { 100, 115, 75, 22, 98, 40, 10 };
			double[] y2 = { 90, 100, 95, 35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67, 18 };
			double[] x = { 100, 200, 300, 400, 500, 600, 700 };

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddCurve( "Curve 1", x, y, Color.Red );

 			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddCurve( "Curve 2", x, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddCurve( "Curve 3", x, y3, Color.Green );

			myPane.ClusterScaleWidth = 100f;
			myPane.XAxis.Min = 0;
			myPane.XAxis.Max = 800;
			myPane.YAxis.Min = 0;
			myPane.YAxis.Max = 140;
			double[] xg = { 0, 800 };
			double[] yg = { 0, 0 };
			LineItem myCurve2;
			for ( double i=10; i<=130; i+=10 )
			{
				yg[0] = i; yg[1] = i;
				myCurve2 = myPane.AddCurve( "", xg, yg, Color.Gray, SymbolType.None );
				myCurve2.Line.Style = DashStyle.Dot;
			}
			
			yg[0] = 0; yg[1] = 140;
			for ( double i=100; i<=700; i+=100 )
			{
				xg[0] = i; xg[1] = i;
				myCurve2 = myPane.AddCurve( "", xg, yg, Color.Gray, SymbolType.None );
				myCurve2.Line.Style = DashStyle.Dot;
			}

			double[] xx = { 0, 800 };
			double[] yy = { 100, 100 };
			double[] yy2 = { 50, 50 };
			
			myCurve2 = myPane.AddCurve( "", xx, yy2, Color.White, SymbolType.None );
			myCurve2.Line.Fill = new Fill( Color.White );
			myCurve2 = myPane.AddCurve( "", xx, yy, Color.PaleGreen, SymbolType.None );
			myCurve2.Line.Fill = new Fill( Color.PaleGreen );
			
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange( this.CreateGraphics() );

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
			BarItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );
			//LinearGradientBrush brush = new LinearGradientBrush( new Rectangle( 0, 0, 150, 150 ),
			//	Color.White, Color.Red, 90F, true );

			Bitmap bm = new Bitmap( @"c:\tree.gif" );
			//Image image = Image.FromHbitmap( bm.GetHbitmap() );
			
			//TextureBrush brush = new TextureBrush( image, new Rectangle( 0, 0, 50, 50 ) );
			TextureBrush brush = new TextureBrush( bm );

			//brush.WrapMode = WrapMode.Clamp;
			
			myCurve.Bar.Fill = new Fill( brush );
			myCurve.Bar.Border.IsVisible = false;

 			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			Bitmap bm2 = new Bitmap( @"c:\shark.gif" );
			//Image image2 = Image.FromHbitmap( bm2.GetHbitmap() );
			TextureBrush brush2 = new TextureBrush( bm2 );			
			myCurve.Bar.Fill = new Fill( brush2 );
			//myCurve.Bar.Fill = new Fill( image2, WrapMode.Tile );
			myCurve.Bar.Border.IsVisible = false;
			
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
			LineItem myCurve = myPane.AddCurve( "Curve 4",
				x, y4, Color.Black, SymbolType.Circle );
			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = myPane.AddBar( "Curve 1", x, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = myPane.AddBar( "Curve 2", x, y2, Color.Blue );


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

#if false	// Ceatly sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );

			const int size = 10;
			double[] x1 = new double[size];
			double[] y1 = new double[size];

			for ( int i=0; i<size; i++ )
			{
				y1[i] = i + 1;
				x1[i] = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 5.0;
			}

			myPane.MinClusterGap = 0.1f;
			myPane.BarBase = BarBase.Y;

			myPane.ClusterScaleWidth = 20;
			double right = 1000;
			double[] x = new double[]{right, right, right, right};

			double[] y = new double[]{90, 110, 130, 150};
			BarItem curve = myPane.AddBar( "Normal Values", x, y, Color.PaleGreen );
			curve.Bar.Fill = new Fill( Color.PaleGreen );
			curve.Bar.Border.IsVisible = false;

			myPane.YAxis.Max = 300;
			myPane.YAxis.Min = 0;
			myPane.YAxis.Step = 20;
			myPane.YAxis.MinorStep = 4;
			myPane.XAxis.Max = 1000;

			// Generate a red bar with "Curve 1" in the legend
			//BarItem myBar = myPane.AddBar( "Curve 1", x, y, Color.Red );

//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if true	// The main example

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
			curve.Symbol.Size = 10;
			
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = myPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			curve.Symbol.Size = 10;
			
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = myPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			myPane.ClusterScaleWidth = 100;
			myPane.BarType = BarType.Stack;
			//curve.Bar.Fill = new Fill( Color.Blue );
			//curve.Symbol.Size = 12;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			myPane.ClusterScaleWidth = 100;
			//Brush brush = new HatchBrush( HatchStyle.Cross, Color.AliceBlue, Color.Red );
			//GraphicsPath path = new GraphicsPath();
			//path.AddLine( 10, 10, 20, 20 );
			//path.AddLine( 20, 20, 30, 0 );
			//path.AddLine( 30, 0, 10, 10 );
			
			//brush = new PathGradientBrush( path );
			//bar.Bar.Fill = new Fill( brush );
			
			
			myPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			myPane.AxisFill = new Fill( Color.White, Color.FromArgb( 255, 255, 166), 90F );
			
			myPane.XAxis.IsShowGrid = true;
			//myPane.IsPenWidthScaled = false;
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
			myPane.GraphItemList.Add( text );

			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			myPane.GraphItemList.Add( arrow );

			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.Fill.IsVisible = false;
			//text.FontSpec.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, -45F );
			text.FontSpec.Border.IsVisible = false;
			myPane.GraphItemList.Add( text );

			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			myPane.GraphItemList.Add( arrow );

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
			myPane.GraphItemList.Add( text );
			
			myPane.IsPenWidthScaled = false ;

			RectangleF rect = new RectangleF( 500, 50, 200, 20 );
			EllipseItem ellipse = new EllipseItem( rect, Color.Blue, 
				Color.Goldenrod );
			ellipse.Location.CoordinateFrame = CoordType.AxisXYScale;
			myPane.GraphItemList.Add( ellipse );

//			Bitmap bm = new Bitmap( @"c:\temp\sunspot.jpg" );
			//Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			//Image image = Image.FromHbitmap( bm.GetHbitmap() );
			//ImageItem imageItem = new ImageItem( image, new RectangleF( 0.8F, 0.8F, 0.2F, 0.2F ),
			//	CoordType.AxisFraction, AlignH.Left, AlignV.Top );
			//imageItem.IsScaled = false;
			//myPane.GraphItemList.Add( imageItem );

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

#if false	// Stacked Bar Example - RPK
         myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),"2003 Wacky Widget Sales\nBy Product",
            "Quarter",
            "Sales (KUSD)");
         SetSize();
         
			
         string [] quarters = {"Q1", "Q2", "Q3", "Q4", "Q5" } ;          
         double[] y4 = { 30, -20, -15, 90, 70 };         //blue
         double[] y3 = { 20, 0, -35, 40,-10 };             //green
         double[] y2 = { -40, 60, -70, 20,-30 };               // red - three segments per bar
 //        double[] y3 = { 2, 3, 3.5, 4 };

#if true                                 //           vertical stacked bars
		LineItem curve;
		curve = myPane.AddCurve( "Larry", null, y4, Color.Black, SymbolType.Circle );
		curve.Line.Width = 1.5F;
		curve.Line.IsSmooth = true;
		curve.Line.SmoothTension = 0.6F;
		curve.Symbol.Fill = new Fill( Color.White );
		curve.Symbol.Size = 12;

		BarItem bar = myPane.AddBar( "Widget", null, y4, Color.RoyalBlue );
		//Color[] colors = { Color.LimeGreen, Color.RoyalBlue, Color.Red };
		//bar.Bar.Fill = new Fill( colors, 90F );
		bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
		bar = myPane.AddBar( "Stridget",  null, y3, Color.LimeGreen );
		bar.Bar.Fill = new Fill( Color.LimeGreen, Color.White, Color.LimeGreen );
		bar = myPane.AddBar( "Bridget",null, y2, Color.Red );
		bar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );
		myPane.XAxis.Type = AxisType.Text ;
		myPane.XAxis.TextLabels=quarters ;         
		myPane.BarBase=BarBase.X ;
		//myPane.BarType=BarType.Stack;
		//myPane.BarType=BarType.Cluster ;
		//myPane.BarType=BarType.Overlay ;
		//myPane.BarType = BarType.SortedOverlay ;
		myPane.BarType = BarType.PercentStack ;
#endif

#if false                                       //horizontal stacked bars         
         
		BarItem bar = myPane.AddBar( "Widget", y4, null, Color.RoyalBlue );
		bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue, 90F );
		bar = myPane.AddBar( "Stridget",  y3, null, Color.LimeGreen );
		bar.Bar.Fill = new Fill( Color.LimeGreen, Color.White, Color.LimeGreen, 90F );
		bar = myPane.AddBar( "Bridget", y2, null, Color.Red );
		bar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90F );
		myPane.BarBase=BarBase.Y ;
		myPane.YAxis.Type = AxisType.Text ;
		myPane.YAxis.TextLabels=quarters ;      
		myPane.YAxis.IsTicsBetweenLabels = true ;   
		myPane.BarType=BarType.Stack ;
		//myPane.BarType=BarType.Cluster ;
		//myPane.BarType=BarType.Overlay ;
		//myPane.BarType = BarType.SortedOverlay ;
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

#if false  // Will's Test
			// Create a new graph
			myPane = new GraphPane( new Rectangle( 40, 40, 300, 200 ),
				"My Pane",
				"My X Axis",
				"My Y Axis" );
				
			double[] x = new double[52];
			double[] y = new double[52];

			x[0]=38272.5652314815;	y[0]=18.8888888889;
			x[1]=38287.9129976852;	y[1]=7.22222222222;
			x[2]=38289.0180555556;	y[2]=9;
			x[3]=38289.025;	y[3]=9;
			x[4]=38289.0368055556;	y[4]=9;
			x[5]=38289.0631944444;	y[5]=8;
			x[6]=38289.0784722222;	y[6]=9;
			x[7]=38289.0861111111;	y[7]=9;
			x[8]=38289.0909722222;	y[8]=9;
			x[9]=38289.1041666667;	y[9]=9;
			x[10]=38289.1118055556;	y[10]=9;
			x[11]=38289.1201388889;	y[11]=9;
			x[12]=38289.1263888889;	y[12]=9;
			x[13]=38289.1416666667;	y[13]=9;
			x[14]=38289.15625;	y[14]=9;
			x[15]=38289.1618055556;	y[15]=9;
			x[16]=38289.1722222222;	y[16]=9;
			x[17]=38289.1791666667;	y[17]=9;
			x[18]=38289.1854166667;	y[18]=9;
			x[19]=38289.1916666667;	y[19]=9;
			x[20]=38289.2034722222;	y[20]=9;
			x[21]=38289.2222222222;	y[21]=9;
			x[22]=38289.2284722222;	y[22]=9;
			x[23]=38289.2368055556;	y[23]=9;
			x[24]=38289.2451388889;	y[24]=9;
			x[25]=38289.25;	y[25]=9;
			x[26]=38289.2868055556;	y[26]=9;
			x[27]=38289.3111111111;	y[27]=9;
			x[28]=38289.31875;	y[28]=9;
			x[29]=38289.3284722222;	y[29]=9;
			x[30]=38289.3333333333;	y[30]=9;
			x[31]=38289.3479166667;	y[31]=9;
			x[32]=38289.3701388889;	y[32]=9;
			x[33]=38289.4118055556;	y[33]=9;
			x[34]=38289.4534722222;	y[34]=10;
			x[35]=38289.4951388889;	y[35]=11;
			x[36]=38289.5368055556;	y[36]=12;
			x[37]=38289.5604166667;	y[37]=11;
			x[38]=38289.5784722222;	y[38]=11;
			x[39]=38289.6201388889;	y[39]=12;
			x[40]=38289.6618055556;	y[40]=13;
			x[41]=38289.6666666667;	y[41]=13;
			x[42]=38289.7034722222;	y[42]=13;
			x[43]=38289.7451388889;	y[43]=12;
			x[44]=38289.75;	y[44]=12;
			x[45]=38289.7590277778;	y[45]=12;
			x[46]=38289.7868055556;	y[46]=12;
			x[47]=38289.8284722222;	y[47]=8;
			x[48]=38289.8701388889;	y[48]=11;
			x[49]=38289.9118055556;	y[49]=12;
			x[50]=38289.9534722222;	y[50]=11;
			x[51]=38289.9951388889;	y[51]=11;

			LineItem curve = myPane.AddCurve( "One Value", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill.IsVisible = false;
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.3F;

			//myPane.XAxis.IsShowGrid = true;
			//myPane.YAxis.IsShowGrid = true;
			
			myPane.AxisChange( this.CreateGraphics() );

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
			//clone the pane so the paneRect can be changed for printing
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
		
#if true
		private void Form1_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
		{
/*
			CurveItem curve;
			int	iPt;

			if ( myPane.FindNearestPoint( new PointF( e.X, e.Y ), out curve, out iPt ) )
				MessageBox.Show( String.Format( "label = {0}  X = {1}",
						curve.Label, curve.Points[iPt].ToString("e2") ) );
			else
				MessageBox.Show( "No Point Found" );
*/
			object	obj;
			int		index;

			if ( myPane.FindNearestObject( new PointF( e.X, e.Y ), this.CreateGraphics(),
					out obj, out index ) )
			{
				if ( obj is CurveItem )
					MessageBox.Show( String.Format( "label = {0}  X = {1}",
						((CurveItem)obj).Label,
						((CurveItem)obj).Points[index].ToString("e2") ) );
				else
					MessageBox.Show( String.Format( "type is {0}", obj.ToString() ) );
			}
			else
				MessageBox.Show( "No Object Found" );
		}
#endif

#if false
		private void Form1_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
		{
			//DoPrint();
			CopyToPNG( myPane );
			
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
#endif

	}
}
