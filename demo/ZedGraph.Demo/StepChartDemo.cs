using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class StepChartDemo : DemoBase
	{

		public StepChartDemo() : base( "A sample step chart",
									"Step Chart Demo", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Demo for Step Charts";
			myPane.XAxis.Title = "Time, Days";
			myPane.YAxis.Title = "Widget Production (units/hour)";
			PointPairList list = new PointPairList();

			for ( double i=0; i<36; i++ )
			{
				double x = i * 5.0;
				double y = Math.Sin( i * Math.PI / 15.0 ) * 16.0;
				list.Add( x, y );
			}

			LineItem curve = myPane.AddCurve( "Step", list, Color.Red, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			curve.Line.StepType = StepType.ForwardStep;
			

			base.ZedGraphControl.AxisChange();
		}
	}
}
