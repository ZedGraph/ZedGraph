using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 358);
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
		protected GraphPane myPane;

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

			CurveItem curve;
			curve = myPane.AddCurve( "One Value", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.IsFilled = true;

			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsShowGrid = true;

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
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange();
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
			myText.AlignH = FontAlignH.Center;
			myText.AlignV = FontAlignV.Top;
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

#endif

#if true	// the filled curve sample
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
				x, y2, Color.Blue, SymbolType.Circle );
			myCurve2.Line.Fill.Type = FillType.Brush;
			myCurve2.Line.Fill.Color = Color.Red;
			myCurve2.Symbol.Fill.Type = FillType.Solid;
			myCurve2.Symbol.Fill.Color = Color.White;
			//myCurve2.Line.IsSmooth = true;

			CurveItem myCurve = myPane.AddCurve( "My Curve",
				x, y, Color.MediumVioletRed, SymbolType.Diamond );
			myCurve.Line.Fill.Type = FillType.Brush;
			myCurve.Line.Fill.Color = Color.Green;
			myCurve.Symbol.Fill.Type = FillType.Solid;
			myCurve.Symbol.Fill.Color = Color.White;
			//myCurve.Line.IsSmooth = true;
			
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;

			myPane.AxisFill.Color = Color.LightGoldenrodYellow;
			myPane.AxisFill.Type = FillType.Brush;

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

#if false	// The sideways bar graph sample

			sideWays = true;

			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			//string[] labels = { "Panther", "Lion", "Cheetah" };
			//double[] y = { 100, 115, 75, -22, 98, 40 };
			//double[] y2 = { 90, 100, 95, -35, 80, 35 };
			//double[] y3 = { 80, 110, 65 };
			//double[] y4 = { 120, 125, 100 };

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddCurve( "Curve 1", null, y, Color.Red );
			// Make it a bar
			myCurve.IsBar = true;
			myCurve.IsY2Axis = true;
			//myCurve.Bar.FillBrush = new LinearGradientBrush( new Point(0,0), new Point(100,0), Color.White,
			//						Color.Blue );
			//myCurve.Bar.FillBrush = new LinearGradientBrush( new Rectangle( 0, 0, 400, 400 ), Color.Blue,
			//							Color.White, 90F );
			//myCurve.Bar.FillType = FillType.Brush;


			myPane.XAxis.IsTicsBetweenLabels = true;
			myPane.XAxis.TextLabels = labels;
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.ScaleFontSpec.Angle = 90;
			myPane.XAxis.IsOppositeTic = false;
			myPane.XAxis.IsMinorOppositeTic = false;
			//myPane.XAxis.IsShowTitle = false;
			//myPane.XAxis.TitleFontSpec.Angle = 90;

			myPane.YAxis.IsVisible = false;

			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.ScaleFontSpec.Angle = 0;
			myPane.Y2Axis.IsOppositeTic = false;
			myPane.Y2Axis.IsMinorOppositeTic = false;
			myPane.Y2Axis.Title = "Y Title";

			TextItem text = new TextItem( "Title", 0.06F, 0.5F );
			text.CoordinateFrame = CoordType.PaneFraction;
			text.FontSpec.IsFramed = false;
			text.FontSpec.Angle = 90;
			text.FontSpec.Size = 18;
			text.FontSpec.IsBold = true;

			myPane.TextList.Add( text );

			myPane.Legend.IsVisible = false;

			myPane.PaneGap = 80;
			myPane.IsShowTitle = false;
			myPane.ClusterScaleWidth = 1;

			myPane.AxisChange( this.CreateGraphics() );


			//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The bar graph sample
			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"My Test Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			//string[] labels = { "Panther", "Lion", "Cheetah" };
			//double[] y = { 100, 115, 75, -22, 98, 40 };
			//double[] y2 = { 90, 100, 95, -35, 80, 35 };
			//double[] y3 = { 80, 110, 65 };
			//double[] y4 = { 120, 125, 100 };

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddCurve( "Curve 1",
				null, y, Color.Red );
			// Make it a bar
			myCurve.IsBar = true;


 			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddCurve( "Curve 2",
				null, y2, Color.Blue );
			// Make it a bar
			myCurve.IsBar = true;

/*
			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddCurve( "Curve 3",
				null, y3, Color.Green );
			// Make it a bar
			myCurve.IsBar = true;
*/
/*
			// Generate a black line with "Curve 4" in the legend
			myCurve = myPane.AddCurve( "Curve 4",
				y4, y4, Color.Black, SymbolType.Circle );

			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.IsFilled = true;
			myCurve.Line.Width = 2.0F;
*/

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
				string lab = y[i].ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (i+1), (float) (y[i] < 0 ? 0.0 : y[i]) + shift );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.CoordinateFrame = CoordType.AxisXYScale;
				// Align the left-center of the text to the specified point
				text.AlignH = FontAlignH.Left;
				text.AlignV = FontAlignV.Center;
				text.FontSpec.IsFramed = false;
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

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddCurve( "Curve 1",
				x, y, Color.Red );
			// Make it a bar
			myCurve.IsBar = true;

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddCurve( "Curve 2",
				x, y2, Color.Blue );
			// Make it a bar
			myCurve.IsBar = true;
/*
			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddCurve( "Curve 3",
				null, y3, Color.Green );
			// Make it a bar
			myCurve.IsBar = true;
*/

			// Generate a black line with "Curve 4" in the legend
			myCurve = myPane.AddCurve( "Curve 4",
				y4, y4, Color.Black, SymbolType.Circle );

			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.IsFilled = true;
			myCurve.Line.Width = 2.0F;

			// Draw the X tics between the labels instead of at the labels
			//myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			myPane.XAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			myPane.AxisChange();


//			GraphPane testPane = (GraphPane) myPane.Clone();
#endif

#if false	// The main example
			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Years\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 40, 35, 60, 90, 25, 48, 75 };
			double[] x2 = { 300, 400, 500, 600, 700, 800, 900 };
			double[] y2 = { 75, 43, 27, 62, 89, 73, 12 };
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };

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
			curve.Line.IsVisible = false;
			curve.Symbol.IsFilled = true;
			curve.Symbol.Size = 12;

			//myPane.XAxis.NumDec = 6;
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
			text.AlignH = FontAlignH.Center;
			text.AlignV = FontAlignV.Bottom;
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
			text.AlignH = FontAlignH.Right;
			text.AlignV = FontAlignV.Center;
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
						text.AlignH = FontAlignH.Center;
						text.AlignV = FontAlignV.Top;
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

			text.AlignH = FontAlignH.Left;
			text.AlignV = FontAlignV.Bottom;
			//			text.IsFilled = true;
			//			text.BackgroundColor = Color.White;
			myPane.TextList.Add( text );
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
			text.AlignH = FontAlignH.Center;
			text.AlignV = FontAlignV.Bottom;
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
			text.AlignH = FontAlignH.Right;
			text.AlignV = FontAlignV.Center;
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
						text.AlignH = FontAlignH.Center;
						text.AlignV = FontAlignV.Top;
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

			text.AlignH = FontAlignH.Left;
			text.AlignV = FontAlignV.Bottom;
			//			text.IsFilled = true;
			//			text.BackgroundColor = Color.White;
			myPane.TextList.Add( text );
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
				memGraphics.g.SmoothingMode = SmoothingMode.AntiAlias;

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
//			if ( !sideWays )
				memGraphics.CreateDoubleBuffer( this.CreateGraphics(),
					this.ClientRectangle.Width, this.ClientRectangle.Height );
//			else
//				memGraphics.CreateDoubleBuffer( this.CreateGraphics(),
//					this.ClientRectangle.Height, this.ClientRectangle.Width );
			SetSize();
			myPane.AxisChange( this.CreateGraphics() );
			Invalidate();
		}

		private void SetSize()
		{
			Rectangle paneRect;
			if ( sideWays )
				paneRect = new Rectangle( 0, 0, this.ClientRectangle.Height, this.ClientRectangle.Width );
			else
				paneRect = this.ClientRectangle;

			paneRect.Inflate( -10, -10 );
			this.myPane.PaneRect = paneRect;
		}

		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			// clone the pane so the paneRect can be changed for printing
			GraphPane printPane = (GraphPane) myPane.Clone();

			// duplicate the DPI value used for the screen
			printPane.BaseDPI = this.CreateGraphics().DpiX;

			//printPane.Legend.IsVisible = true;
			printPane.PaneRect = new RectangleF( 50, 50,
				this.Size.Width+300, this.Size.Height+300 );
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

		private void Form1_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
		{
			myPane.XAxis.TitleFontSpec.Angle += 90;
			myPane.YAxis.TitleFontSpec.Angle += 90;
			myPane.Y2Axis.TitleFontSpec.Angle += 90;
			myPane.AxisChange( this.CreateGraphics() );
			Invalidate();

			//myPane.IsIgnoreMissing = !myPane.IsIgnoreMissing;
			//			Invalidate();

			//			DoPrint();

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
