using System;
using System.Drawing;
using System.Collections;
using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for HiLowBarDemo.
	/// </summary>
	public class HiLowBarDemo : DemoBase
	{
		public HiLowBarDemo() : base( "A demo demonstrating HiLow Bars",
										"Hi-Low Bar", DemoType.Bar )
		{
			base.GraphPane.Title = "My Test Bar Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
				
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Bar Graph", "Label", "My Y Axis" );
			
			// Make up some random data points

			double[] y = new double[44];
			double[] yBase = new double[44];

			for ( int i=0; i<44; i++ )
			{
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
				yBase[i] = y[i] - 0.4;
			}

			// Generate a red bar with "Curve 1" in the legend
			HiLowBarItem myCurve = base.GraphPane.AddHiLowBar( "Curve 1", null, y, yBase, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
			myCurve.Bar.IsMaximumWidth = true;
			//myPane.BarType = BarType.HiLow;
			
			//myPane.AxisChange( this.CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
