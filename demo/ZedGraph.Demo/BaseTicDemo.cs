using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for BaseTicDemo
	/// </summary>
	public class BaseTicDemo : DemoBase
	{

		public BaseTicDemo() : base( "Demo for the Axis.BaseTic property\n" +
				"BaseTic allows you to begin the major tics at an arbitrary value (such as 50, in this case)",
				"BaseTic Demo", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Demo of BaseTic property";
			myPane.XAxis.Title = "Time, Days";
			myPane.YAxis.Title = "Widget Production (units/hour)";
			PointPairList list = new PointPairList();

			for ( double i=0; i<36; i++ )
			{
				double x = i * 10.0 + 50.0;
				double y = Math.Sin( i * Math.PI / 15.0 ) * 16.0;
				list.Add( x, y );
			}

			myPane.Legend.IsVisible = false;
			LineItem curve = myPane.AddCurve( "label", list, Color.Red, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			myPane.XAxis.BaseTic = 50;
			 
			base.ZedGraphControl.AxisChange();
		}
	}
}
