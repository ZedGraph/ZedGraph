using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class SmoothChartDemo : DemoBase
	{

		public SmoothChartDemo() : base( "A Chart Demonstrating the different line types",
									"Smooth Chart Demo", DemoType.General, DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Smooth Line Demo";
			myPane.XAxis.Title = "Value";
			myPane.YAxis.Title = "Time";
			
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };

			LineItem curve = myPane.AddCurve( "Smooth (Tension=0.5)", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.5F;

			curve = myPane.AddCurve( "Forward Step", x, y, Color.Green, SymbolType.Circle );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.StepType = StepType.ForwardStep;

			curve = myPane.AddCurve( "Rearward Step", x, y, Color.Gold, SymbolType.Square );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.StepType = StepType.RearwardStep;

			curve = myPane.AddCurve( "Non-Step", x, y, Color.Blue, SymbolType.Triangle );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			base.ZedGraphControl.AxisChange();
		}
	}
}
