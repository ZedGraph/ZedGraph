using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for BarGraphBandDemo.
	/// </summary>
	public class SineBarGraphDemo : DemoBase
	{
		public SineBarGraphDemo() : base( "A bar graph displaying values of a sine wave.",
											"Sine Bar Graph Demo", DemoType.Bar )
		{
			// Create a new graph with topLeft at (40,40) and size 600x400
			base.GraphPane.Title = "A Sine wave displayed by a bar graph.";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";

			const int size = 41;
			double[] x = new double[size];
			double[] y = new double[size];
			double[] y2 = new double[size];
			string[] labels = new string[size];

			for ( int i=0; i<size; i++ )
			{
				x[i] = i + 1;
				y[i] = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 5.0;
				y2[i] = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 25.0;
				labels[i] = "lab" + (i+1).ToString();
			}

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a black line with "Curve 4" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "Curve 4",
				x, y4, Color.Black, SymbolType.Circle );
			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = base.GraphPane.AddBar( "Curve 1", x, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = base.GraphPane.AddBar( "Curve 2", x, y2, Color.Blue );


			// Draw the X tics between the labels instead of at the labels
			//base.GraphPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Ordinal;

			base.GraphPane.XAxis.IsReverse = false;
			base.GraphPane.ClusterScaleWidth = 1;
		}

	}
}
