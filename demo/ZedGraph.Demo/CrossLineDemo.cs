using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class CrossLineDemo : DemoBase
	{

		public CrossLineDemo() : base( "Simple Graph with Y Axis in Alternate Location",
			"Axis Cross Demo", DemoType.Line, DemoType.Special )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Axis Cross Demo";
			myPane.XAxis.Title = "My X Axis";
			myPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Graph\n(For CodeProject Sample)",
			//	"My X Axis",
			//	"My Y Axis" );

			// Make up some data arrays based on the Sine function
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i=0; i<37; i++ )
			{
				x = ((double) i - 18.0 ) / 5.0;
				y = x * x + 1.0;
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = myPane.AddCurve( "Parabola",
				list, Color.Green, SymbolType.Diamond );

			myPane.YAxis.Cross = 0.0;
			myPane.AxisBorder.IsVisible = false;
			myPane.XAxis.IsOppositeTic = false;
			myPane.XAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed

			base.ZedGraphControl.AxisChange();
		}
	}
}
