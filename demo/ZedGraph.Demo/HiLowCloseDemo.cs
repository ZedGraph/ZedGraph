using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class HiLowCloseDemo : DemoBase
	{

		public HiLowCloseDemo() : base( "A demo demonstrating HiLowClose",
										"Hi-Low-Close", DemoType.Bar, DemoType.Special )
		{
			base.GraphPane.Title = "ZedgroSoft, International\nHi-Low-Close Daily Stock Chart";
			base.GraphPane.XAxis.Title = "";
			base.GraphPane.YAxis.Title = "Trading Price, $US";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"ZedgroSoft, International\nHi-Low-Close Daily Stock Chart",
			//	"",
			//	"Trading Price, $US" );

			base.GraphPane.FontSpec.Family = "Arial";
			base.GraphPane.FontSpec.IsItalic = true;
			base.GraphPane.FontSpec.Size = 18;

			double hi, low, close, x;
			PointPairList hList = new PointPairList();
			PointPairList cList = new PointPairList();
			Random rand = new Random();
			close = 45;

			for ( int i=45;	i<65; i++	)
			{
				x = (double) new XDate( 2004, 12, i-30 );
				close = close + 2.0 * rand.NextDouble() - 0.5;
				hi = close + 2.0 * rand.NextDouble();
				low = close - 2.0 * rand.NextDouble();
				hList.Add( x, hi, low );
				cList.Add( x, close );
			}


			LineItem curve;
			curve = base.GraphPane.AddCurve( "Closing Price", cList, Color.Black,
				SymbolType.Diamond );
			curve.Line.IsVisible = false ;
			curve.Symbol.Fill = new Fill( Color.Red );
			curve.Symbol.Size = 7;

			ErrorBarItem myCurve = base.GraphPane.AddErrorBar(	"Price Range", hList,
				Color.Blue );

			//	Set the XAxis	to date type
			base.GraphPane.XAxis.Type =	AxisType.Date;
			base.GraphPane.XAxis.Step = 1 ;
			base.GraphPane.XAxis.ScaleFontSpec.Size = 12 ;
			base.GraphPane.XAxis.ScaleFontSpec.Angle = 65 ;
			base.GraphPane.XAxis.MajorUnit = DateUnit.Day ;
			base.GraphPane.XAxis.ScaleFontSpec.IsBold = true ;
			base.GraphPane.XAxis.ScaleFormat = "d MMM" ;
			base.GraphPane.XAxis.Min = hList[0].X - 1 ;

			myCurve.ErrorBar.PenWidth = 3;
			myCurve.ErrorBar.Symbol.IsVisible = false;
			
			base.GraphPane.YAxis.IsShowGrid = true ;
			//myPane.YAxis.IsShowMinorGrid = true ;
			base.GraphPane.YAxis.MinorStep = 0.5;

			base.GraphPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
