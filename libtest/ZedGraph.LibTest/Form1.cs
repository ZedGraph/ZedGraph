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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using GDIDB;
using ZedGraph;
using System.Diagnostics;

namespace ZedGraph.LibTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private IContainer components;
		private DBGraphics memGraphics;
		private TrackBar trackBar1;
		private CheckBox ReverseBox;
		private CheckBox LabelsInsideBox;
		private ComboBox AxisSelection;
		private CheckBox CrossAutoBox;

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
			this.components = new System.ComponentModel.Container();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.ReverseBox = new System.Windows.Forms.CheckBox();
			this.LabelsInsideBox = new System.Windows.Forms.CheckBox();
			this.AxisSelection = new System.Windows.Forms.ComboBox();
			this.CrossAutoBox = new System.Windows.Forms.CheckBox();
			( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point( 2, 1 );
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size( 311, 45 );
			this.trackBar1.TabIndex = 0;
			this.trackBar1.Scroll += new System.EventHandler( this.trackBar1_Scroll );
			// 
			// ReverseBox
			// 
			this.ReverseBox.Location = new System.Drawing.Point( 320, 1 );
			this.ReverseBox.Name = "ReverseBox";
			this.ReverseBox.Size = new System.Drawing.Size( 70, 17 );
			this.ReverseBox.TabIndex = 1;
			this.ReverseBox.Text = "IsReverse";
			this.ReverseBox.CheckedChanged += new System.EventHandler( this.ReverseBox_CheckedChanged );
			// 
			// LabelsInsideBox
			// 
			this.LabelsInsideBox.Location = new System.Drawing.Point( 320, 24 );
			this.LabelsInsideBox.Name = "LabelsInsideBox";
			this.LabelsInsideBox.Size = new System.Drawing.Size( 84, 17 );
			this.LabelsInsideBox.TabIndex = 2;
			this.LabelsInsideBox.Text = "Labels Inside";
			this.LabelsInsideBox.CheckedChanged += new System.EventHandler( this.LabelsInsideBox_CheckedChanged );
			// 
			// AxisSelection
			// 
			this.AxisSelection.Items.AddRange( new object[] {
            "X Axis",
            "Y Axis",
            "Y2 Axis"} );
			this.AxisSelection.Location = new System.Drawing.Point( 421, 20 );
			this.AxisSelection.Name = "AxisSelection";
			this.AxisSelection.Size = new System.Drawing.Size( 121, 21 );
			this.AxisSelection.TabIndex = 4;
			this.AxisSelection.SelectedIndexChanged += new System.EventHandler( this.AxisSelection_SelectedIndexChanged );
			// 
			// CrossAutoBox
			// 
			this.CrossAutoBox.Location = new System.Drawing.Point( 420, 1 );
			this.CrossAutoBox.Name = "CrossAutoBox";
			this.CrossAutoBox.Size = new System.Drawing.Size( 69, 17 );
			this.CrossAutoBox.TabIndex = 5;
			this.CrossAutoBox.Text = "crossAuto";
			this.CrossAutoBox.CheckedChanged += new System.EventHandler( this.CrossAutoBox_CheckedChanged );
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 608, 435 );
			this.Controls.Add( this.CrossAutoBox );
			this.Controls.Add( this.AxisSelection );
			this.Controls.Add( this.LabelsInsideBox );
			this.Controls.Add( this.ReverseBox );
			this.Controls.Add( this.trackBar1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler( this.Form1_Load );
			this.Paint += new System.Windows.Forms.PaintEventHandler( this.Form1_Paint );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );
			this.Resize += new System.EventHandler( this.Form1_Resize );
			( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

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
		protected GraphPane		myPane, myPane2;
		protected MasterPane	master = null;
		private Axis _crossAxis;


		private void Form1_Load(object sender, System.EventArgs e)
		{			
			Trace.Listeners.Add(new TextWriterTraceListener( @"myTrace.txt" ) );
			Trace.AutoFlush = true;

			memGraphics.CreateDoubleBuffer(this.CreateGraphics(),
				this.ClientRectangle.Width, this.ClientRectangle.Height);

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

#if false	// Bars and Dates

//			Color color = Color.FromArgb( 123, 45, 67, 89 );
//			HSBColor hsbColor = new HSBColor( color );
//			Color color2 = hsbColor;



			Random rand = new Random();

			myPane = new GraphPane();
			myPane.Title = "My Title";
			myPane.XAxis.Title = "X Axis";
			myPane.YAxis.Title = "Y Axis";
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

#if false
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
			myPane.AxisFill = new Fill( Color.White,
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

			//myPane.IsFontsScaled = true;
			//myPane.XAxis.ScaleFontSpec.Size = 8;

			double[] x = { 100, 400, 500, 600, 900, 1000 };
			double[] y = { 20, 25, 35, 75, 33, 50 };
			LineItem curve;
			curve = myPane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.FontSpec = new FontSpec( "Arial", 16, Color.Green, true, false, false );
			curve.FontSpec.Border.IsVisible = false;
			curve.Line.Width = 1.5F;
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
			//curve.IsLegendLabelVisible = false;
			
			//MessageBox.Show( curve.Points.InterpolateX( 450 ).ToString() );

			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = myPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.FontSpec = new FontSpec( "Times", 12, Color.FromArgb( 200, 55, 135), true, false, false );
			curve.FontSpec.Border.IsVisible = false;
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			//curve.IsLegendLabelVisible = false;
			
			Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			Image image = Image.FromHbitmap( bm.GetHbitmap() );
			//TextureBrush tBrush = new TextureBrush( image, WrapMode.Tile );
			//LinearGradientBrush tBrush = new LinearGradientBrush( new Rectangle(0, 0, 100, 100), Color.Blue, Color.Red, 45.0F );
			//curve.Line.Fill = new Fill( tBrush );
			curve.Line.Fill = new Fill(image, WrapMode.Tile );
			//curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			curve.Symbol.Size = 10;
			
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = myPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			bar.FontSpec = new FontSpec( "Courier", 20, Color.SteelBlue, false, true, false );
			bar.FontSpec.Border.IsVisible = false;
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			myPane.BarType = BarType.Stack;
			bar.IsY2Axis = true;
			//curve.Bar.Fill = new Fill( Color.Blue );
			//curve.Symbol.Size = 12;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			bar.Bar.Border.IsVisible = false;
			myPane.ClusterScaleWidth = 1;
			bar.IsY2Axis = true;
			//Brush brush = new HatchBrush( HatchStyle.Cross, Color.AliceBlue, Color.Red );
			//GraphicsPath path = new GraphicsPath();
			//path.AddLine( 10, 10, 20, 20 );
			//path.AddLine( 20, 20, 30, 0 );
			//path.AddLine( 30, 0, 10, 10 );
			
			//brush = new PathGradientBrush( path );
			//bar.Bar.Fill = new Fill( brush );
			
			//PointPairList junk = new PointPairList();
			//myPane.AddCurve( "Hi There", junk, Color.Blue, SymbolType.None );
			
			myPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			myPane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
						Color.FromArgb( 255, 255, 190), 90F );
			
			//myPane.PaneBorder.InflateFactor = -4.0f;

			myPane.XAxis.IsShowGrid = true;
			//myPane.XAxis.Max = 1200;
			myPane.XAxis.Type = AxisType.LinearAsOrdinal;
			//myPane.XAxis.Cross = 80;
			//myPane.IsPenWidthScaled = false;
			//myPane.XAxis.ScaleFontSpec.Angle = 90;
			//myPane.XAxis.ScaleAlign = AlignP.Inside;
			//myPane.XAxis.IsShowMinorGrid = true;
			//myPane.XAxis.MinorGridColor = Color.Red;

			myPane.YAxis.IsShowGrid = true;
			//myPane.YAxis.ScaleFontSpec.Angle = 90;
			myPane.YAxis.Max = 120;
			//myPane.YAxis.Cross = 1150;
			//myPane.YAxis.ScaleAlign = AlignP.Inside;
			//myPane.YAxis.ScaleFontSpec.Border.IsVisible = true;
			//myPane.YAxis.Type = AxisType.Log;
			//myPane.YAxis.IsUseTenPower = false;
			//myPane.YAxis.IsShowMinorGrid = true;
			//myPane.YAxis.MinorGridColor = Color.Red;

			//myPane.Y2Axis.IsVisible = true;
			//myPane.Y2Axis.Cross = 50;
			//myPane.Y2Axis.Max = 120;
			//myPane.Y2Axis.ScaleAlign = AlignP.Outside;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.Max = 80;
			
			TextItem text = new TextItem("First Prod\n21-Oct-93", 175F, 80.0F );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Near;
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

			text = new TextItem("Confidential", 0.85F, -0.03F );
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

			BoxItem box = new BoxItem( new RectangleF( 0, 110, 1200, 10 ),
					Color.Empty, Color.FromArgb( 225, 245, 225) );
			box.Location.CoordinateFrame = CoordType.AxisXYScale;
			
//			BoxItem box = new BoxItem( new RectangleF( 0F, .2F, 1F, .2F ),
//					Color.Empty, Color.PeachPuff );
//			box.Location.CoordinateFrame = CoordType.AxisFraction;
			//box.Border.IsVisible = false;
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			box.ZOrder = ZOrder.E_BehindAxis;
			myPane.GraphItemList.Add( box );
			
			TextItem myText = new TextItem( "Peak Range", 1170, 105 );
			myText.Location.CoordinateFrame = CoordType.AxisXYScale;
			myText.Location.AlignH = AlignH.Right;
			myText.Location.AlignV = AlignV.Center;
			myText.FontSpec.IsItalic = true;
			myText.FontSpec.IsBold = false;
			myText.FontSpec.Fill.IsVisible = false;
			myText.FontSpec.Border.IsVisible = false;
			myPane.GraphItemList.Add( myText );
			
			
			//myPane.LineType = LineType.Stack;
			//myPane.PaneBorder.IsVisible= false;

			RectangleF rect = new RectangleF( .5F, .05F, .2F, .2F );
			EllipseItem ellipse = new EllipseItem( rect, Color.Black, Color.Blue );
			ellipse.Location.CoordinateFrame = CoordType.PaneFraction;
			ellipse.ZOrder = ZOrder.G_BehindAll;
			myPane.GraphItemList.Add( ellipse );

			RectangleF rectx = new RectangleF( 1000, 20, 400, 40 );
			EllipseItem ellipsex = new EllipseItem( rectx, Color.Black, Color.Red );
			ellipsex.ZOrder = ZOrder.E_BehindAxis;
			ellipsex.IsClippedToAxisRect = true;
			myPane.GraphItemList.Add( ellipsex );

			//myPane.CurveList.Remove( myPane.CurveList.IndexOf( bar ) );

//			Bitmap bm = new Bitmap( @"c:\temp\sunspot.jpg" );
			/*
			Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			Image image = Image.FromHbitmap( bm.GetHbitmap() );
			ImageItem imageItem = new ImageItem( image,
				new RectangleF( 0.2F, 0.6F, 0.2F, 0.2F ),
				CoordType.AxisFraction, AlignH.Left, AlignV.Top );
			//imageItem.IsScaled = false;
			imageItem.ZOrder = ZOrder.C_BehindAxisBorder;
			myPane.GraphItemList.Add( imageItem );
			*/

			float limit = 95F;
			float arrowWidth = (float) myPane.XAxis.Max;
			ArrowItem newArrow = new ZedGraph.ArrowItem( Color.Blue, 5.0F, 0F, limit, arrowWidth, limit );
			//newArrow.Location.Width = arrowWidth;
			newArrow.ZOrder = ZedGraph.ZOrder.E_BehindAxis;
			//newArrow.IsArrowHead = false;
			newArrow.Size = 10;
			myPane.GraphItemList.Add( newArrow );
#endif
	
#if false	// MasterPane
			master = new MasterPane( "ZedGraph MasterPane Example", new Rectangle( 10, 10, 10, 10 ) );

			master.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			//master.IsShowTitle = true;

			//master.MarginAll = 10;
			//master.InnerPaneGap = 10;
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
			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j=0; j<5; j++ )
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
			
			master.AutoPaneLayout( g, PaneLayout.ExplicitRow32 );
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
			myPane.AxisFill.Type = FillType.None;
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
			myPane.XAxis.ScaleFontSpec.Angle = -90;
			myPane.XAxis.ScaleFontSpec.Size = 8;
			myPane.XAxis.ScaleFontSpec.IsBold = true;
			myPane.XAxis.IsTicsBetweenLabels = true;
			myPane.XAxis.IsInsideTic = false;
			myPane.XAxis.IsOppositeTic = false;
			myPane.XAxis.IsMinorInsideTic = false;
			myPane.XAxis.IsMinorOppositeTic = false;

			myPane.YAxis.ScaleFontSpec.Size = 8;
			myPane.YAxis.ScaleFontSpec.IsBold = true;
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

#if false	// Basic curve test - Text Axis
			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			double[] x = new double[20];
			double[] y = new double[20];
			string[] labels = new string[20];

			for ( int i=0; i<20; i++ )
			{
				x[i] = (double) i;
				y[i] = Math.Sin( i / 8.0 );
				double z = Math.Abs(Math.Cos( i / 8.0 )) * y[i];
				labels[i] = "Label" + i.ToString();
			}
			BasicArrayPointList list = new BasicArrayPointList( x, y );

			//LineItem myCurve = myPane.AddCurve( "curve", list, Color.Blue, SymbolType.Diamond );
			StickItem myCurve = myPane.AddStick( "curve", list, Color.Blue );
			//myCurve.Symbol.IsVisible = true;
			myCurve.IsY2Axis = true;
			myPane.Y2Axis.IsVisible = true;
			//myPane.YAxis.IsVisible = false;
			myPane.Y2Axis.Title = "Y2 Axis";
			myPane.Y2Axis.IsShowGrid = true;
			myPane.XAxis.IsShowGrid = true;
			//myPane.YAxis.IsSkipFirstLabel = true;
			myPane.XAxis.IsSkipLastLabel = true;
			//myPane.XAxis.IsSkipLastLabel = true;
			//myPane.XAxis.IsReverse = true;
			//myPane.AxisBorder.IsVisible = false;
			myPane.XAxis.Type = AxisType.Text;
			myPane.XAxis.TextLabels = labels;
			myPane.XAxis.ScaleFontSpec.Angle = 30;

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
			trackBar1.Maximum = 359;
			trackBar1.Value = 0;

#endif

#if false	// Basic curve test - Multi-Y axes

			myPane = new GraphPane( new RectangleF( 0, 0, 640, 480 ), "Title", "XAxis", "YAxis" );

			myPane.AddYAxis( "Another Y Axis" );
			myPane.AddY2Axis( "Another Y2 Axis" );
			myPane.Y2Axis.Title = "My Y2 Axis";
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

#if true	// Bars - different colors thru IsOverrideOrdinal

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
			//myPane.Y2Axis.Title = "Y2 Axis";
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

			_crossAxis = myPane.Y2AxisList[1];
			UpdateControls();
			SetSize();

			//this.WindowState = FormWindowState.Maximized ;
			if ( this.myPane != null )
				this.myPane.AxisChange( this.CreateGraphics() );
      
		}

		private void CreateBarLabels( MasterPane pane )
		{
			const int labelOffset	= 5;
			GraphPane graphPane		= pane.PaneList[ 0 ];

			foreach ( CurveItem curve in graphPane.CurveList )
			{
				BarItem bar = curve as BarItem;

				if ( bar != null )
				{
					IPointList points = curve.Points;

					for ( int i = 0; i < points.Count; i++ )
					{
						ValueHandler valueHandler		= 
							new ValueHandler( graphPane, true );

						int curveIndex					=
							graphPane.CurveList.IndexOf( curve );

						double labelXCoordintate			= 
							valueHandler.BarCenterValue( bar, 
							bar.GetBarWidth( graphPane ), i, points[ i ].X, 
							curveIndex );

						float labelYCoordinate				= 
							( float ) points[ i ].Y + labelOffset;

						string barLabelText				= 
							( points[ i ].Y / 1000 ).ToString( "N2" );

						TextItem label					= 
							new TextItem( barLabelText, ( float ) labelXCoordintate, 
							labelYCoordinate );

						label.Location.CoordinateFrame	= CoordType.AxisXYScale;
						label.FontSpec.Size				= 10;
						label.FontSpec.FontColor		= Color.Black;
						label.Location.AlignH			= AlignH.Left;
						label.Location.AlignV			= AlignV.Center;
						label.FontSpec.Border.IsVisible	= false;
						label.FontSpec.Fill.IsVisible	= false;

						graphPane.GraphItemList.Add( label );
					}
				}
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pevent"></param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		bool showTicks = false;
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
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
		
		private void CopyToPNG( PaneBase thePane )
		{
			if ( thePane != null )
				thePane.Image.Save(@"c:\zedgraph.png", System.Drawing.Imaging.ImageFormat.Png);
		}

		private void CopyToGif( GraphPane thePane )
		{
			if ( thePane != null )
				thePane.Image.Save( @"c:\zedgraph.gif", ImageFormat.Gif );
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
			SetSize();
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

		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			//clone the pane so the paneRect can be changed for printing
			//PaneBase printPane = (PaneBase) master.Clone();
			GraphPane printPane = (GraphPane) myPane.Clone();
			printPane.PaneRect = new RectangleF( 50, 50, 400, 300 );

			//printPane.Legend.IsVisible = true;
			//printPane.PaneRect = new RectangleF( 50, 50, 300, 300 );
			//printPane.ReSize( e.Graphics, new RectangleF( 50, 50, 300, 300 ) );
				
			//e.Graphics.PageScale = 1.0F;
			//printPane.BaseDimension = 2.0F;
			printPane.Draw( e.Graphics );
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

		private void Serialize( GraphPane myPane )
		{
			//XmlSerializer mySerializer = new XmlSerializer( typeof( GraphPane ) );
			//StreamWriter myWriter = new StreamWriter( @"myFileName.xml" );

			//SoapFormatter mySerializer = new SoapFormatter();
			//FileStream myWriter = new FileStream( @"myFileName.soap", FileMode.Create );

			BinaryFormatter mySerializer = new BinaryFormatter();
			Stream myWriter = new FileStream( "c:\\temp\\myFileName.bin", FileMode.Create,
				FileAccess.Write, FileShare.None );

			if ( myPane != null )
			{
				mySerializer.Serialize( myWriter, myPane );
				MessageBox.Show( "Serialized output created" );
			}

			myWriter.Close();
		}


		private void DeSerialize( out GraphPane myPane )
		{
			BinaryFormatter mySerializer = new BinaryFormatter();
			Stream myReader = new FileStream( "c:\\temp\\myFileName.bin", FileMode.Open,
				FileAccess.Read, FileShare.Read );

			myPane = (GraphPane) mySerializer.Deserialize( myReader );
			Invalidate();

			myReader.Close();
		}

		private void Serialize( MasterPane master )
		{
			//XmlSerializer mySerializer = new XmlSerializer( typeof( GraphPane ) );
			//StreamWriter myWriter = new StreamWriter( @"myFileName.xml" );

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

		private void Form1_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
		{
			//Image image = master.ScaledImage( 300, 200, 72 );
			//image.Save( @"c:\zedgraph.png", ImageFormat.Png );

			//DoPrint();
			//DoPageSetup();
			//DoPrintPreview();
			//return;

			//Serialize( master );
			//DeSerialize( out master );

			object obj;
			int index;
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
				myText.Text = String.Format( "label = {0}  X = {1}",
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

		private void trackBar1_Scroll( object sender, EventArgs e )
		{
			Axis controlAxis = myPane.XAxis;
			if ( _crossAxis is XAxis )
				controlAxis = myPane.YAxis;

			_crossAxis.Cross = (double) trackBar1.Value / 100.0 * (controlAxis.Max - controlAxis.Min) +
							controlAxis.Min;
			//_crossAxis.ScaleFontSpec.Angle = trackBar1.Value;
			myPane.AxisChange( this.CreateGraphics() );
			Refresh();
		}

		private void ReverseBox_CheckedChanged( object sender, EventArgs e )
		{
			_crossAxis.IsReverse = ReverseBox.Checked;
			Invalidate();
		}

		private void LabelsInsideBox_CheckedChanged( object sender, EventArgs e )
		{
			_crossAxis.IsScaleLabelsInside = LabelsInsideBox.Checked;
			Invalidate();
		}

		private void AxisSelection_SelectedIndexChanged( object sender, EventArgs e )
		{
			if ( AxisSelection.SelectedIndex == 0 )
				_crossAxis = myPane.XAxis;
			else if ( AxisSelection.SelectedIndex == 1 )
				_crossAxis = myPane.YAxis;
			else
				_crossAxis = myPane.Y2Axis;

			UpdateControls();
		}

		private void UpdateControls()
		{
			Axis controlAxis = myPane.XAxis;
			if ( _crossAxis is XAxis )
				controlAxis = myPane.YAxis;

			ReverseBox.Checked = _crossAxis.IsReverse;
			LabelsInsideBox.Checked = _crossAxis.IsScaleLabelsInside;
			CrossAutoBox.Checked = _crossAxis.CrossAuto;

			/*
			double ratio = (_crossAxis.Cross - controlAxis.Min) / (controlAxis.Max - controlAxis.Min);
			if ( ratio >= 0 && ratio <= 1 )
				trackBar1.Value = (int) (100 * ratio);
			*/

			trackBar1.Value = (int) ( Math.Abs( _crossAxis.ScaleFontSpec.Angle ) + 0.5 ) % 360;
		}

		private void CrossAutoBox_CheckedChanged( object sender, EventArgs e )
		{
			_crossAxis.CrossAuto = CrossAutoBox.Checked;
			Invalidate();
		}

	}
}
