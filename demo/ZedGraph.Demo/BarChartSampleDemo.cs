using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class BarChartSampleDemo : DemoBase
	{

		public BarChartSampleDemo() : base( "Code Project Bar Chart Sample",
			"Bar Chart Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "My Test Bar Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 90, 100, 95, 35, 80, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67 };
			double[] y4 = { 120, 125, 100, 40, 105, 75 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = base.GraphPane.AddBar( "Curve 1", null, y, Color.Red );
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = base.GraphPane.AddBar( "Curve 2", null, y2, Color.Blue );
			myBar.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myBar = base.GraphPane.AddBar( "Curve 3", null, y3, Color.Green );
			myBar.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green );

			// Generate a black line with "Curve 4" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "Curve 4",
				null, y4, Color.Black, SymbolType.Circle );
			myCurve.Line.Fill = new Fill( Color.White, Color.LightSkyBlue, -45F );

			// Fix up the curve attributes a little
			myCurve.Symbol.Size = 8.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Draw the X tics between the labels instead of at the labels
			base.GraphPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Text;

			base.GraphPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );
			base.GraphPane.PaneFill = new Fill( Color.FromArgb( 250, 250, 255) );
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//myPane.AxisChange( CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
