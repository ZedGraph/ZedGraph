using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class SimpleDemo : DemoBase
	{

		public SimpleDemo() : base( "A simple demo\nwith multi-line desciption\nhttp://zedgraph.sourceforge.net",
									"Simple Demo", DemoType.General, DemoType.Line )
		{
			base.GraphPane.Title = "Wacky Widget Company\nProduction Report";
			base.GraphPane.XAxis.Title = "Time, Days\n(Since Plant Construction Startup)";
			base.GraphPane.YAxis.Title = "Widget Production\n(units/hour)";
			
			//base.GraphPane.XAxis.ScaleFontSpec.Size = 8;

			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			LineItem curve;
			curve = base.GraphPane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
			
			//MessageBox.Show( curve.Points.InterpolateX( 450 ).ToString() );

			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = base.GraphPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			
			//Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			//Image image = Image.FromHbitmap( bm.GetHbitmap() );
			//TextureBrush tBrush = new TextureBrush( image, WrapMode.Tile );
			//LinearGradientBrush tBrush = new LinearGradientBrush( new Rectangle(0, 0, 100, 100), Color.Blue, Color.Red, 45.0F );
			//curve.Line.Fill = new Fill( tBrush );
			//curve.Line.Fill = new Fill(image, WrapMode.Tile );
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			curve.Symbol.Size = 10;
			
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = base.GraphPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			base.GraphPane.ClusterScaleWidth = 100;
			base.GraphPane.BarType = BarType.Stack;
			//curve.Bar.Fill = new Fill( Color.Blue );
			//curve.Symbol.Size = 12;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = base.GraphPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			//bar.Bar.Border.IsVisible = false;
			base.GraphPane.ClusterScaleWidth = 100;
			//Brush brush = new HatchBrush( HatchStyle.Cross, Color.AliceBlue, Color.Red );
			//GraphicsPath path = new GraphicsPath();
			//path.AddLine( 10, 10, 20, 20 );
			//path.AddLine( 20, 20, 30, 0 );
			//path.AddLine( 30, 0, 10, 10 );
			
			//brush = new PathGradientBrush( path );
			//bar.Bar.Fill = new Fill( brush );
			
			//PointPairList junk = new PointPairList();
			//base.GraphPane.AddCurve( "Hi There", junk, Color.Blue, SymbolType.None );
			
			base.GraphPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			base.GraphPane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
						Color.FromArgb( 255, 255, 190), 90F );
			
			//base.GraphPane.PaneBorder.InflateFactor = -4.0f;

			base.GraphPane.XAxis.IsShowGrid = true;
			base.GraphPane.XAxis.Max = 1200;
			//base.GraphPane.IsPenWidthScaled = false;
			//base.GraphPane.XAxis.ScaleFontSpec.Angle = 90;
			//base.GraphPane.XAxis.ScaleAlign = AlignP.Inside;
			//base.GraphPane.XAxis.IsShowMinorGrid = true;
			//base.GraphPane.XAxis.MinorGridColor = Color.Red;

			base.GraphPane.YAxis.IsShowGrid = true;
			//base.GraphPane.YAxis.ScaleFontSpec.Angle = 90;
			base.GraphPane.YAxis.Max = 120;
			//base.GraphPane.YAxis.ScaleAlign = AlignP.Inside;
			//base.GraphPane.YAxis.ScaleFontSpec.Border.IsVisible = true;
			//base.GraphPane.YAxis.Type = AxisType.Log;
			//base.GraphPane.YAxis.IsUseTenPower = false;
			//base.GraphPane.YAxis.IsShowMinorGrid = true;
			//base.GraphPane.YAxis.MinorGridColor = Color.Red;

			//base.GraphPane.Y2Axis.IsVisible = true;
			//base.GraphPane.Y2Axis.Max = 120;
			//base.GraphPane.Y2Axis.ScaleAlign = AlignP.Outside;
			
			TextItem text = new TextItem("First Prod\n21-Oct-93", 175F, 80.0F );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Near;
			base.GraphPane.GraphItemList.Add( text );

			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			base.GraphPane.GraphItemList.Add( arrow );

			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.Fill.IsVisible = false;
			//text.FontSpec.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, -45F );
			text.FontSpec.Border.IsVisible = false;
			base.GraphPane.GraphItemList.Add( text );

			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			base.GraphPane.GraphItemList.Add( arrow );

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
			base.GraphPane.GraphItemList.Add( text );

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
			base.GraphPane.GraphItemList.Add( box );
			
			TextItem myText = new TextItem( "Peak Range", 1170, 105 );
			myText.Location.CoordinateFrame = CoordType.AxisXYScale;
			myText.Location.AlignH = AlignH.Right;
			myText.Location.AlignV = AlignV.Center;
			myText.FontSpec.IsItalic = true;
			myText.FontSpec.IsBold = false;
			myText.FontSpec.Fill.IsVisible = false;
			myText.FontSpec.Border.IsVisible = false;
			base.GraphPane.GraphItemList.Add( myText );
			
			
			//base.GraphPane.LineType = LineType.Stack;
			//base.GraphPane.PaneBorder.IsVisible= false;

			RectangleF rect = new RectangleF( .5F, .05F, .2F, .2F );
			EllipseItem ellipse = new EllipseItem( rect, Color.Black, Color.Blue );
			ellipse.Location.CoordinateFrame = CoordType.PaneFraction;
			ellipse.ZOrder = ZOrder.G_BehindAll;
			base.GraphPane.GraphItemList.Add( ellipse );

			//base.GraphPane.CurveList.Remove( base.GraphPane.CurveList.IndexOf( bar ) );

//			Bitmap bm = new Bitmap( @"c:\temp\sunspot.jpg" );
			/*
			Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			Image image = Image.FromHbitmap( bm.GetHbitmap() );
			ImageItem imageItem = new ImageItem( image,
				new RectangleF( 0.2F, 0.6F, 0.2F, 0.2F ),
				CoordType.AxisFraction, AlignH.Left, AlignV.Top );
			//imageItem.IsScaled = false;
			imageItem.ZOrder = ZOrder.C_BehindAxisBorder;
			base.GraphPane.GraphItemList.Add( imageItem );
			*/

			base.ZedGraphControl.AxisChange();
		}
	}
}
