using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class FilledCurveDemo : DemoBase
	{

		public FilledCurveDemo() : base( "A Line Graph with the Area Under the Curves Filled",
			"Filled Curve Demo", DemoType.General, DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "My Test Date Graph";
			myPane.XAxis.Title = "Date";
			myPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				double x = new XDate( 1995, i+1, 1 );
				double y = Math.Sin( (double) i * Math.PI / 15.0 );
				double y2 = 2 * y;

				list.Add( x, y );
				list2.Add( x, y2 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve2 = myPane.AddCurve( "My Curve 2",
				list, Color.Blue, SymbolType.Circle );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Red, 45F );
			myCurve2.Symbol.Fill = new Fill( Color.White );
			//myCurve2.Line.IsSmooth = true;

			LineItem myCurve = myPane.AddCurve( "My Curve",
				list2, Color.MediumVioletRed, SymbolType.Diamond );
			myCurve.Line.Fill = new Fill( Color.White, Color.Green );
			myCurve.Symbol.Fill = new Fill( Color.White );
			//myCurve.Line.IsSmooth = true;
			
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;

			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45F );

			//myPane.Y2Axis.MinSpace = 100;
			
			//myPane.PaneFill.Color = Color.MediumTurquoise;
			//myPane.PaneFill.Type = FillType.Brush;

			//myPane.Legend.Fill.Color = Color.Fuchsia;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//			myPane.AxisChange();

			base.ZedGraphControl.AxisChange();
		}
	}
}
