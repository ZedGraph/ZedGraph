using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class TextAxisSampleDemo : DemoBase
	{

		public TextAxisSampleDemo() : base( "Code Project Text Axis Sample",
			"Text Axis Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "My Test Date Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Text Graph", "Label", "My Y Axis" );

			// Make up some random data points
			string[] labels = { "USA", "Spain\nMadrid", "Qatar", "Morocco", "UK", "Uganda",
								  "Cambodia", "Malaysia", "Australia", "Ecuador" };
			double[] y = new double[10];
			for ( int i=0; i<10; i++ )
				y[i] = Math.Sin( (double) i * Math.PI / 2.0 );
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "My Curve",
				null, y, Color.Red, SymbolType.Diamond );
			//Make the curve smooth
			myCurve.Line.IsSmooth = true;
			
			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Text;
			// Set the labels at an angle so they don't overlap
			base.GraphPane.XAxis.ScaleFontSpec.Angle = 40;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//base.GraphPane.AxisChange( CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
