using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class ModInitialSampleDemo : DemoBase
	{

		public ModInitialSampleDemo() : base( "Code Project Modified Initial Sample",
			"Modified Initial Sample", DemoType.Tutorial )
		{
			base.GraphPane.Title = "My Test Graph\n(For CodeProject Sample)";
			base.GraphPane.XAxis.Title = "My X Axis";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Graph\n(For CodeProject Sample)",
			//	"My X Axis",
			//	"My Y Axis" );

			// Make up some data arrays based on the Sine function
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				x = (double) i + 5;
				y1 = 1.5 + Math.Sin( (double) i * 0.2 );
				y2 = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = base.GraphPane.AddCurve( "Porsche",
				list1, Color.Red, SymbolType.Diamond );

			// Generate a blue curve with circle
			// symbols, and "Piper" in the legend
			LineItem myCurve2 = base.GraphPane.AddCurve( "Piper",
				list2, Color.Blue, SymbolType.Circle );

			// Change the color of the title
			base.GraphPane.FontSpec.FontColor = Color.Green;

			// Add gridlines to the plot, and make them gray
			base.GraphPane.XAxis.IsShowGrid = true;
			base.GraphPane.YAxis.IsShowGrid = true;
			base.GraphPane.XAxis.GridColor = Color.LightGray;
			base.GraphPane.YAxis.GridColor = Color.LightGray;

			// Move the legend location
			base.Pane.Legend.Position = ZedGraph.LegendPos.Bottom;

			// Make both curves thicker
			myCurve.Line.Width = 2.0F;
			myCurve2.Line.Width = 2.0F;

			// Fill the area under the curves
			myCurve.Line.Fill = new Fill( Color.White, Color.Red, 45F );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Blue, 45F );

			// Increase the symbol sizes, and fill them with solid white
			myCurve.Symbol.Size = 8.0F;
			myCurve2.Symbol.Size = 8.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve2.Symbol.Fill = new Fill( Color.White );

			// Add a background gradient fill to the axis frame
			//myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, -45F );
			base.GraphPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 210), -45F );

			// Add a caption and an arrow
			TextItem myText = new TextItem( "Interesting\nPoint", 230F, 70F );
			myText.FontSpec.FontColor = Color.Red;
			myText.Location.AlignH = AlignH.Center;
			myText.Location.AlignV = AlignV.Top;
			base.GraphPane.GraphItemList.Add( myText );
			ArrowItem myArrow = new ArrowItem( Color.Red, 12F, 230F, 70F, 280F, 55F );
			base.GraphPane.GraphItemList.Add( myArrow );

			// Tell ZedGraph to refigure the
			// axes since the data have changed

			base.ZedGraphControl.AxisChange();
		}
	}
}
