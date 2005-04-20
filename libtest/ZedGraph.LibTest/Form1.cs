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
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuItemHowdy;

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuItemHowdy = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																					   this.menuFile
																				   } );
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																					  this.menuItemHowdy
																				  } );
			this.menuFile.Text = "File";
			// 
			// menuItemHowdy
			// 
			this.menuItemHowdy.Index = 0;
			this.menuItemHowdy.Text = "Howdy";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 507, 342 );
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Paint += new System.Windows.Forms.PaintEventHandler( this.Form1_Paint );
			this.Resize += new System.EventHandler( this.Form1_Resize );
			this.Load += new System.EventHandler( this.Form1_Load );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );

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

		private void Form1_Load(object sender, System.EventArgs e)
		{			
			Trace.Listeners.Add(new TextWriterTraceListener( @"myTrace.txt" ) );
			Trace.AutoFlush = true;

			memGraphics.CreateDoubleBuffer(this.CreateGraphics(),
				this.ClientRectangle.Width, this.ClientRectangle.Height);

#if false

			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			string[] ystr = { "one", "two", "three", "four", "five" };

			double[] x = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			double[] y = { 1, 2, 3, 4, 5, 4, 3, 2, 1, 2 };
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
			myPane.YAxis.Type = AxisType.Text;
			myPane.YAxis.TextLabels = ystr;
			//myPane.ClusterScaleWidth = 1;

			myPane.AxisChange( this.CreateGraphics() );

#endif

#if true
			Random rand = new Random();

			myPane = new GraphPane();
			myPane.Title = "My Title";
			myPane.XAxis.Title = "X Axis";
			myPane.YAxis.Title = "Y Axis";
			myPane.XAxis.Type = AxisType.Date;
			myPane.ClusterScaleWidth = 0.75 / 1440.0;
			myPane.XAxis.MinorStep = 1;
			myPane.XAxis.MinorUnit = DateUnit.Minute;

			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			for ( int i=0; i<80; i++ )
			{
				double x = new XDate( 1995, 5, 10, 12, i+1, 0 );
				double y1 = rand.NextDouble() * 100.0;
				double y2 = rand.NextDouble() * 100.0;

				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}

			BarItem bar1 = myPane.AddBar( "Bar 1", list1, Color.Red );
			bar1.Bar.Border.IsVisible = false;
			bar1.Bar.Fill = new Fill( Color.Red );
			BarItem bar2 = myPane.AddBar( "Bar 2", list2, Color.Blue );
			bar2.Bar.Border.IsVisible = false;
			bar2.Bar.Fill = new Fill( Color.Blue );

			myPane.AxisChange( this.CreateGraphics() );
#endif

#if false
            myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			//myPane.IsFontsScaled = true;
			//myPane.XAxis.ScaleFontSpec.Size = 8;

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
			curve.IsLegendLabelVisible = false;
			
			//MessageBox.Show( curve.Points.InterpolateX( 450 ).ToString() );

			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = myPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.IsLegendLabelVisible = false;
			
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
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			myPane.ClusterScaleWidth = 100;
			myPane.BarType = BarType.Stack;
			//curve.Bar.Fill = new Fill( Color.Blue );
			//curve.Symbol.Size = 12;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			bar.Bar.Border.IsVisible = false;
			myPane.ClusterScaleWidth = 100;
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
			myPane.XAxis.Max = 1200;
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

#if false
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
				GraphPane myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"Case #" + (j+1).ToString(),
					"Time, Days",
					"Rate, m/s" );

				myPane.PaneFill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPane.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i=0; i<36; i++ )
				{
					x = (double) i + 5;
					y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 + (double) j ) );
					list.Add( x, y );
				}

				LineItem myCurve = myPane.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( myPane );
			}

			Graphics g = this.CreateGraphics();
			
			master.AutoPaneLayout( g, PaneLayout.ExplicitRow32 );
			master.AxisChange( g );

			g.Dispose();
#endif

#if false
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

			SetSize();

			//this.WindowState = FormWindowState.Maximized ;
			if ( this.myPane != null )
				this.myPane.AxisChange( this.CreateGraphics() );
      
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
			//DoPrint();
			//Serialize( master );
			//DeSerialize( out master );

			//object obj;
			//int index;
			//if ( myPane.FindNearestObject( new PointF( e.X, e.Y ), this.CreateGraphics(), out obj, out index ) )
			//	MessageBox.Show( obj.ToString() + " index=" + index );
			//else
			//	MessageBox.Show( "No Object Found" );


			//myPane.XAxis.PickScale( 250, 900, myPane, this.CreateGraphics(), myPane.CalcScaleFactor() );
			//Invalidate();
			
			//showTicks = true;
			//Invalidate();
			/*			
						if ( myText != null )
						{
							CurveItem curve;
							int		iPt;
							if ( myPane.FindNearestPoint( new PointF( e.X, e.Y ), out curve, out iPt ) )
								myText.Text = String.Format( "label = {0}  X = {1}",
									curve.Label, curve.Points[iPt].ToString("e2") );
							else
								myText.Text = "none";
				
							if ( curve is LineItem )
								((LineItem) curve).Line.IsVisible = false;
							Invalidate();
						}
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

	}
}
