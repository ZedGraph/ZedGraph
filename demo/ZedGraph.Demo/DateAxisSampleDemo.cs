using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class DateAxisSampleDemo : DemoBase
	{

		public DateAxisSampleDemo() : base( "Code Project Date Axis Sample",
			"Date Axis Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "My Test Date Graph";
			base.GraphPane.XAxis.Title = "Date";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Date Graph", "Date", "My Y Axis" );
   
			// Make up some random data points
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				x = (double) new XDate( 1995, 5, i+11 );
				y = Math.Sin( (double) i * Math.PI / 15.0 );
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "My Curve",
				list, Color.Red, SymbolType.Diamond );
      
			// Set the XAxis to date type
			base.GraphPane.XAxis.Type = AxisType.Date;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//base.GraphPane.AxisChange( CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
