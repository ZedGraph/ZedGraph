using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class SortedOverlayBarDemo : DemoBase
	{

		public SortedOverlayBarDemo() : base( "An Sorted Overlay Bar Chart",
			"Sorted Overlay Bar Demo", DemoType.Bar )
		{
			base.GraphPane.Title = "My Test Sorted Overlay Bar Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Overlay Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 104, 67, 18 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = base.GraphPane.AddBar( "Curve 1",
				null, y, Color.Red );
			// Make it a bar

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 2",
				null, y2, Color.Blue );
			// Make it a bar

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 3",
				null, y3, Color.Green );
			// Make it a bar

			// Draw the X tics between the labels instead of at the labels
			base.GraphPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Text;
			base.GraphPane.XAxis.ScaleFontSpec.Size = 9.0F ;
			
			base.GraphPane.BarBase = BarBase.X;
			base.GraphPane.BarType = BarType.SortedOverlay;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//base.GraphPane.AxisChange( this.CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
