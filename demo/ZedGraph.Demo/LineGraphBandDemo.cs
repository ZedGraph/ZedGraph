using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for BarGraphBandDemo.
	/// </summary>
	public class LineGraphBandDemo : DemoBase
	{
		public LineGraphBandDemo() : base( "A demo of a bar graph with a region highlighted.",
											"Line Graph Band Demo", DemoType.Line )
		{
			// Create a new graph with topLeft at (40,40) and size 600x400
			base.GraphPane.Title = "Line Graph with Band Demo";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";

			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			
			double[] y = { 100, 115, 75, 22, 98, 40, 10 };
			double[] y2 = { 90, 100, 95, 35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67, 18 };
			double[] x = { 100, 200, 300, 400, 500, 600, 700 };

			// Generate a red bar with "Curve 1" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "Curve 1", x, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddCurve( "Curve 2", x, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddCurve( "Curve 3", x, y3, Color.Green );

			base.GraphPane.ClusterScaleWidth = 100f;
			base.GraphPane.XAxis.Min = 0;
			base.GraphPane.XAxis.Max = 800;
			base.GraphPane.YAxis.IsShowGrid = true;
			base.GraphPane.YAxis.IsShowMinorGrid = true;
			//base.GraphPane.YAxis.Min = 0;
			//base.GraphPane.YAxis.Max = 140;
/*
			double[] xg = { 0, 800 };
			double[] yg = { 0, 0 };
			LineItem myCurve2;
			for ( double i=10; i<=130; i+=10 )
			{
				yg[0] = i; yg[1] = i;
				myCurve2 = base.GraphPane.AddCurve( "", xg, yg, Color.Gray, SymbolType.None );
				//myCurve2.Line.Style = DashStyle.Dot;
			}
			
			yg[0] = 0; yg[1] = 140;
			for ( double i=100; i<=700; i+=100 )
			{
				xg[0] = i; xg[1] = i;
				myCurve2 = base.GraphPane.AddCurve( "", xg, yg, Color.Gray, SymbolType.None );
				//myCurve2.Line.Style = DashStyle.Dot;
			}
*/
			BoxItem box = new BoxItem( new RectangleF( 0, 100, 800, 50 ), Color.Empty, Color.PaleGreen );
			box.ZOrder = ZOrder.E_BehindAxis;
			base.GraphPane.GraphItemList.Add( box );
/*
			double[] xx = { 0, 800 };
			double[] yy = { 100, 100 };
			double[] yy2 = { 50, 50 };
			
			myCurve2 = base.GraphPane.AddCurve( "", xx, yy2, Color.White, SymbolType.None );
			myCurve2.Line.Fill = new Fill( Color.White );
			myCurve2 = base.GraphPane.AddCurve( "", xx, yy, Color.PaleGreen, SymbolType.None );
			myCurve2.Line.Fill = new Fill( Color.PaleGreen );
*/
			
			base.GraphPane.YAxis.Type = AxisType.Log;
		}
	}
}
