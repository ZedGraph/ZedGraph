using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class StackedBarSampleDemo : DemoBase
	{

		public StackedBarSampleDemo() : base( "Code Project Stacked Bar Chart Sample",
			"Stacked Bar Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "Cat Stats";
			base.GraphPane.XAxis.Title = "Big Cats";
			base.GraphPane.YAxis.Title = "Population";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"Cat Stats", "Big Cats", "Population" );
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 120, 175, 95, 57, 113, 110 };
			double[] y3 = { 204, 192, 119, 80, 134, 156 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = base.GraphPane.AddBar( "Here", null, y, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddBar( "There", null, y2, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddBar( "Elsewhere", null, y3, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			base.GraphPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Text;

			base.GraphPane.BarType = BarType.Stack;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//myPane.AxisChange( CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
