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
			GraphPane	myPane = base.GraphPane;

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
				// x[i] = (double) new XDate( 1995, i+1, 1 );
				double x = (double) i * 5.0;
				double y = Math.Sin( (double) i * Math.PI / 15.0 ) * 16.0;
				double y2 = y * 13.5;
				list.Add( x, y );
				list2.Add( x, y2 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "First",
				list, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			//myPane.XAxis.Type = AxisType.Date;

			// Generate a blue curve with diamond
			// symbols, and "My Curve" in the legend
			myCurve = myPane.AddCurve( "Second",
				list2, Color.Blue, SymbolType.Circle );
			myCurve.IsY2Axis = true;
			myPane.YAxis.IsVisible = true;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.IsShowGrid = true;
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsZeroLine = false;
			myPane.YAxis.ScaleAlign = AlignP.Inside;

			myPane.YAxis.Min = -30;
			myPane.YAxis.Max = 30;
			myPane.Y2Axis.Title = "Y2 Axis";
			myPane.Y2Axis.ScaleAlign = AlignP.Inside;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//myPane.AxisChange( this.CreateGraphics() );

			//myPane.YAxis.Type = AxisType.Log;

			base.ZedGraphControl.AxisChange();
		}
	}
}
