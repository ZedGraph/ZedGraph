using System;
using System.Collections;
using System.Drawing;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for TransparentDemo1.
	/// </summary>
	public class TransparentDemo1 : DemoBase
	{
		public TransparentDemo1() : base( "A transparency demo", "Transparency Demo 1", DemoType.Special )
		{
			base.GraphPane.Title = "Desert Rainfall";
			base.GraphPane.XAxis.Title = "Time, Years";
			base.GraphPane.YAxis.Title = "Rainfall, mm/yr";
			
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = base.GraphPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			//bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			bar.Bar.Fill = new Fill( Color.FromArgb( 100, 130, 255, 130 ), Color.FromArgb( 100, 255, 255, 255 ),
				Color.FromArgb( 100, 130, 255, 130 ) );
			base.GraphPane.ClusterScaleWidth = 100;
			base.GraphPane.BarType = BarType.Stack;
			//base.GraphPane.PaneGap = 60F;
			//curve.Bar.Fill = new Fill( Color.Blue );
			//curve.Symbol.Size = 12;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = base.GraphPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			base.GraphPane.ClusterScaleWidth = 100;
			//Brush brush = new HatchBrush( HatchStyle.Cross, Color.AliceBlue, Color.Red );
			//GraphicsPath path = new GraphicsPath();
			//path.AddLine( 10, 10, 20, 20 );
			//path.AddLine( 20, 20, 30, 0 );
			//path.AddLine( 30, 0, 10, 10 );
			
			//brush = new PathGradientBrush( path );
			//bar.Bar.Fill = new Fill( brush );

			Image image = 
				Bitmap.FromStream(
					GetType().Assembly.GetManifestResourceStream("ZedGraph.Demo.background.jpg"));
	
			TextureBrush texBrush = new TextureBrush( image );
			base.GraphPane.PaneFill = new Fill( texBrush );
			//base.GraphPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			//base.GraphPane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
			//			Color.FromArgb( 255, 255, 190), 90F );
			base.GraphPane.AxisFill.IsVisible = false;
			base.GraphPane.Legend.IsVisible = false;
			
			TextItem text = new TextItem( "Desert Rainfall", 0.5F, -0.05F, CoordType.AxisFraction,
				AlignH.Center, AlignV.Bottom );
			text.FontSpec.FontColor = Color.Black;
			text.FontSpec.Size = 20F;
			text.FontSpec.IsBold = true;
			text.FontSpec.IsItalic = true;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			base.GraphPane.GraphItemList.Add( text );
			base.GraphPane.IsShowTitle = false;
			base.GraphPane.FontSpec.FontColor = Color.Black;
			base.GraphPane.XAxis.Color = Color.White;
			base.GraphPane.YAxis.Color = Color.White;
			base.GraphPane.XAxis.ScaleFontSpec.FontColor = Color.White;
			base.GraphPane.XAxis.TitleFontSpec.FontColor = Color.White;
			base.GraphPane.YAxis.ScaleFontSpec.FontColor = Color.White;
			base.GraphPane.YAxis.TitleFontSpec.FontColor = Color.White;
			base.GraphPane.AxisBorder.Color = Color.White;
			base.GraphPane.XAxis.GridColor = Color.White;
			base.GraphPane.YAxis.GridColor = Color.White;
			
			base.GraphPane.XAxis.IsShowGrid = true;
			//base.GraphPane.IsPenWidthScaled = false;
			//base.GraphPane.XAxis.ScaleFontSpec.Angle = 90;
			//base.GraphPane.XAxis.ScaleAlign = AlignP.Inside;
			//base.GraphPane.XAxis.IsShowMinorGrid = true;
			//base.GraphPane.XAxis.MinorGridColor = Color.Red;

			base.GraphPane.YAxis.IsShowGrid = true;
			//base.GraphPane.YAxis.ScaleFontSpec.Angle = 90;
			base.GraphPane.YAxis.Max = 120;
			//base.GraphPane.YAxis.ScaleAlign = AlignP.Center;
			//base.GraphPane.YAxis.Type = AxisType.Log;
			//base.GraphPane.YAxis.IsUseTenPower = false;
			//base.GraphPane.YAxis.IsShowMinorGrid = true;
			//base.GraphPane.YAxis.MinorGridColor = Color.Red;

			//base.GraphPane.Y2Axis.IsVisible = true;
			//base.GraphPane.Y2Axis.Max = 120;
			//base.GraphPane.Y2Axis.ScaleAlign = AlignP.Outside;
			
			//RectangleF axisRect = base.GraphPane.AxisRect;
			//axisRect.X += axisRect.Width * 0.3F;
			//axisRect.Width *= 0.7F;
			//axisRect.Y += axisRect.Height * 0.35F;
			//axisRect.Height *= 0.65F;
			//base.GraphPane.AxisRect = axisRect;
		}
	}
}
