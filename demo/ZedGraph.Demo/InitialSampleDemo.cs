using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class InitialSampleDemo : DemoBase
	{

		public InitialSampleDemo() : base( "Code Project Initial Sample",
			"Initial Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "My Test Graph\n(For CodeProject Sample)";
			base.GraphPane.XAxis.Title = "My X Axis";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Graph\n(For CodeProject Sample)",
			//	"My X Axis",
			//	"My Y Axis" );

			// Make up some data arrays based on the Sine function
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				x = (double) i + 5;
				y1 = 1.5 + Math.Sin( (double) i * 0.2 );
				y2 = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "Porsche",
				list1, Color.Red, SymbolType.Diamond );

			// Generate a blue curve with circle
			// symbols, and "Piper" in the legend
			LineItem myCurve2 = base.GraphPane.AddCurve( "Piper",
				list2, Color.Blue, SymbolType.Circle );

			// Tell ZedGraph to refigure the
			// axes since the data have changed

			base.ZedGraphControl.AxisChange();
		}
	}
}
