using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class HorizontalBarSampleDemo : DemoBase
	{

		public HorizontalBarSampleDemo() : base( "Code Project Horizontal Bar Chart Sample",
			"Horizontal Bar Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "A Horizontal Percent Stack Graph";
			base.GraphPane.XAxis.Title = "Stuff";
			base.GraphPane.YAxis.Title = "";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"A Horizontal Percent Stack Graph", "Stuff", "" );
			// Make up some random data points
			double[] y = { 100, 115, 15, 22, 98 };
			double[] y2 = { 90, 60, 95, 35, 30 };
			double[] y3 = { 20, 40, 105, 15, 30 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = base.GraphPane.AddBar( "Nina", y, null, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90F );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddBar( "Pinta", y2, null, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90F );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddBar( "Santa Maria", y3, null, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90F );

			// Draw the Y tics between the labels instead of at the labels
			base.GraphPane.YAxis.IsTicsBetweenLabels = true;

			// Set the YAxis to Ordinal type
			base.GraphPane.YAxis.Type = AxisType.Text;
			string[] labels = { "Australia", "Africa", "America", "Asia", "Antartica" };
			base.GraphPane.YAxis.TextLabels = labels;
			base.GraphPane.XAxis.Max = 110;

			// Make the bars horizontal by setting bar base axis to Y
			base.GraphPane.BarBase = BarBase.Y;
			base.GraphPane.BarType = BarType.PercentStack;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//base.GraphPane.AxisChange( CreateGraphics() );

			base.GraphPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );
			base.GraphPane.Legend.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 250), 90F );
			base.GraphPane.PaneFill = new Fill( Color.FromArgb( 250, 250, 255) );

			base.ZedGraphControl.AxisChange();
		}
	}
}
