using System;
using System.Collections;
using System.Drawing;

using ZedGraph;


namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for LineStackingDemo.
	/// </summary>
	public class LineStackingDemo : DemoBase
	{
		public LineStackingDemo() : base( "A demo of line stacking in ZedGraph",
											"Line Stacking Demo 1", DemoType.Line, DemoType.Special )
		{

			base.GraphPane.Title = "Wacky Widget Company\nProduction Report";
			base.GraphPane.XAxis.Title = "Time, Days\n(Since Plant Construction Startup)";
			base.GraphPane.YAxis.Title = "Widget Production\n(units/hour)";

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
			curve.Line.Fill.IsVisible = false;
			
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8, 22.1, 34.3, 10.4, 17.5 };
			curve = base.GraphPane.AddCurve( "Moe", x, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			curve.Symbol.Size = 10;
			
			base.GraphPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			base.GraphPane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
				Color.FromArgb( 255, 255, 190), 90F );
			
			base.GraphPane.XAxis.IsShowGrid = true;

			base.GraphPane.YAxis.IsShowGrid = true;
			//base.GraphPane.YAxis.Max = 120;						
			
			base.GraphPane.LineType = LineType.Stack;

			base.ZedGraphControl.AxisChange();
		}
	}
}
