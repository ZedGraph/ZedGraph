using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class ErrorBarDemo : DemoBase
	{

		public ErrorBarDemo() : base( "An Error Bar Chart",
			"Error Bar Demo", DemoType.Bar )
		{
			base.GraphPane.Title = "My Test Bar Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Bar Graph", "Label", "My Y Axis" );
			
			// Make up some random data points

			double x, y, yBase;
			PointPairList list = new PointPairList();
			for ( int i=0; i<44; i++ )
			{
				x = i / 44.0;
				y = Math.Sin( (double) i * Math.PI / 15.0 );
				yBase = y - 0.4;
				list.Add( y, x, yBase );
			}

			// Generate a red bar with "Curve 1" in the legend
			ErrorBarItem myCurve = base.GraphPane.AddErrorBar( "Curve 1", list,
				Color.Red );
			myCurve.BarBase = BarBase.Y;
			//myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
			
			//myPane.BarType = BarType.HiLow;
			//myCurve.ErrorBar.Size = 0;
			myCurve.ErrorBar.PenWidth = 1f;
			myCurve.ErrorBar.Symbol.Type = SymbolType.VDash;
			myCurve.ErrorBar.Symbol.Border.PenWidth = .1f;
			myCurve.ErrorBar.Symbol.IsVisible = false;
			
			//myPane.AxisChange( this.CreateGraphics() );

			base.ZedGraphControl.AxisChange();
		}
	}
}
