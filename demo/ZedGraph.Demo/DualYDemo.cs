using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class DualYDemo : DemoBase
	{

		public DualYDemo() : base( "A simple line graph with dual Y axes",
			"Dual Y Demo", DemoType.Line )
		{
			base.GraphPane.Title = "My Test Date Graph";
			base.GraphPane.XAxis.Title = "Date";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Date Graph", "Date", "My Y Axis" );

			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			double[] y2 = new double[36];
			for ( int i=0; i<36; i++ )
			{
				// x[i] = (double) new XDate( 1995, i+1, 1 );
				x[i] = (double) i * 5.0;
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 ) * 16.0;
				y2[i] = y[i] * 10.5;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			//myPane.XAxis.Type = AxisType.Date;

			// Generate a blue curve with diamond
			// symbols, and "My Curve" in the legend
			myCurve = base.GraphPane.AddCurve( "My Curve 1",
				x, y2, Color.Blue, SymbolType.Circle );
			myCurve.IsY2Axis = true;
			base.GraphPane.YAxis.IsVisible = true;
			base.GraphPane.Y2Axis.IsVisible = true;
			base.GraphPane.Y2Axis.IsShowGrid = true;
			base.GraphPane.XAxis.IsShowGrid = true;
			base.GraphPane.YAxis.IsOppositeTic = false;
			base.GraphPane.YAxis.IsMinorOppositeTic = false;
			base.GraphPane.YAxis.IsZeroLine = false;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//myPane.AxisChange( this.CreateGraphics() );

			//myPane.YAxis.Type = AxisType.Log;

			base.ZedGraphControl.AxisChange();
		}
	}
}
