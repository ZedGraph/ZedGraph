using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class HorizontalBarDemo : DemoBase
	{

		public HorizontalBarDemo() : base( "A sideways bar demo",
								"HorizontalBar Demo", DemoType.Bar )
		{
			base.GraphPane.Title = "My Test Bar Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Bar Graph", "Label", "My Y Axis" );

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = base.GraphPane.AddBar( "Curve 1", y, null, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Red, 90F );
			
			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 2", y2, null, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Blue, 90F );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 3", y3, null, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.White, Color.Green, 90F );

			// Draw the X tics between the labels instead of at the labels
			base.GraphPane.YAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.YAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.YAxis.Type = AxisType.Ordinal;

			base.GraphPane.YAxis.IsReverse = false;
			base.GraphPane.ClusterScaleWidth = 1;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				double maxVal = Math.Max( Math.Max( y[i], y2[i] ), y3[i] );
				// format the label string to have 1 decimal place
				string lab = maxVal.ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (maxVal < 0 ? 0.0 : maxVal) + shift, (float) (i+1) );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.Location.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.Location.AlignH = AlignH.Left;
				text.Location.AlignV = AlignV.Center;
				text.FontSpec.Border.IsVisible = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 0;
				// add the TextItem to the list
				base.GraphPane.GraphItemList.Add( text );
			}

			//myPane.XAxis.IsReverse = true;
			base.GraphPane.BarBase = BarBase.Y;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			//myPane.XAxis.Max += myPane.XAxis.Step;

			base.ZedGraphControl.AxisChange();
		}
	}
}
