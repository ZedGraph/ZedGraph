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
			GraphPane myPane = base.GraphPane;

			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane.Title = "A Sine wave displayed by a bar graph.";
			myPane.XAxis.Title = "Label";
			myPane.YAxis.Title = "My Y Axis";

			const int size = 41;
			string[] labels = new string[size];
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();

			for ( int i=0; i<size; i++ )
			{
				double x = i + 1;
				double y = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 5.0;
				double y2 = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 25.0;
				labels[i] = "lab" + (i+1).ToString();
				list.Add( x, y );
				list2.Add( x, y2 );
			}

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a black line with "Curve 4" in the legend
			LineItem myCurve = myPane.AddCurve( "Curve 4",
				null, y4, Color.Black, SymbolType.Circle );
			myCurve.Symbol.Size = 14.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = myPane.AddBar( "Curve 1", list, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = myPane.AddBar( "Curve 2", list2, Color.Blue );


			// Draw the X tics between the labels instead of at the labels
			//myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			myPane.XAxis.IsReverse = false;
			myPane.ClusterScaleWidth = 1;
		}

	}
}
