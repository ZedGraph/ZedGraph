using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for BarGraphBandDemo.
	/// </summary>
	public class BarGraphBandDemo : DemoBase
	{
		public BarGraphBandDemo() : base( "Bar Graph Demo with Band",
							"Bar Graph Band Demo", DemoType.Bar )
		{
			// Create a new graph with topLeft at (40,40) and size 600x400
			base.GraphPane.Title.Text = "My Test Bar Graph";
			base.GraphPane.XAxis.Title.Text = "Label";
			base.GraphPane.YAxis.Title.Text = "My Y Axis";

			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			
			double[] y = { 100, 115, 75, 22, 98, 40, 10 };
			double[] y2 = { 90, 100, 95, 35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67, 18 };
			double[] x = { 100, 200, 300, 400, 500, 600, 700 };

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = base.GraphPane.AddCurve( "Curve 1", x, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddCurve( "Curve 2", x, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddCurve( "Curve 3", x, y3, Color.Green );

			base.GraphPane.BarSettings.ClusterScaleWidth = 100f;
			base.GraphPane.XAxis.Scale.Min = 0;
			base.GraphPane.XAxis.Scale.Max = 800;
			base.GraphPane.YAxis.Scale.Min = 0;
			base.GraphPane.YAxis.Scale.Max = 140;
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

			double[] xx = { 0, 800 };
			double[] yy = { 100, 100 };
			double[] yy2 = { 50, 50 };
			
			myCurve2 = base.GraphPane.AddCurve( "", xx, yy2, Color.White, SymbolType.None );
			myCurve2.Line.Fill = new Fill( Color.White );
			myCurve2 = base.GraphPane.AddCurve( "", xx, yy, Color.PaleGreen, SymbolType.None );
			myCurve2.Line.Fill = new Fill( Color.PaleGreen );
		}

		#region ZGDemo Members

		public override string Description
		{
			get
			{
				return "A demo of a bar graph with a region highlighted.";
			}
		}

		public override string Title
		{
			get
			{
				// TODO:  Add BarGraphBandDemo.Title getter implementation
				return "Bar Graph Band Demo";
			}
		}

		public override ICollection Types
		{
			get
			{
				ArrayList types = new ArrayList();
				types.Add(DemoType.Bar);

				return types;
			}
		}

		#endregion
	}
}
