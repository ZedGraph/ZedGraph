using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class OverlayBarDemo : DemoBase
	{

		public OverlayBarDemo() : base( "An Overlay Bar Chart",
			"Overlay Bar Demo", DemoType.Bar )
		{
			base.GraphPane.Title = "My Test Overlay Bar Graph";
			base.GraphPane.XAxis.Title = "Label";
			base.GraphPane.YAxis.Title = "My Y Axis";
			
			// Create a new graph with topLeft at (40,40) and size 600x400
			//myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
			//	"My Test Overlay Bar Graph", "Label", "My Y Axis" );
			// Make up some random data points

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			for ( int i=0; i<y.GetLength(0); i++ )
				y2[i] += y[i];
			for ( int i=0; i<y2.GetLength(0); i++ )
				y3[i] += y2[i];

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = base.GraphPane.AddBar( "Curve 1",
				null, y, Color.Red );
			// Make it a bar

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 2",
				null, y2, Color.Blue );
			// Make it a bar

			// Generate a green bar with "Curve 3" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 3",
				null, y3, Color.Green );
			// Make it a bar

			// Draw the X tics between the labels instead of at the labels
			base.GraphPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Ordinal;

			base.GraphPane.XAxis.IsReverse = false;
			base.GraphPane.ClusterScaleWidth = 1;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				// format the label string to have 1 decimal place
				string lab = y3[i].ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (i+1), (float) (y3[i] < 0 ? 0.0 : y3[i]) + shift );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.Location.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.Location.AlignH = AlignH.Left;
				text.Location.AlignV = AlignV.Center;
				text.FontSpec.Border.IsVisible = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 90;
				// add the TextItem to the list
				base.GraphPane.GraphItemList.Add( text );
			}
			
			base.GraphPane.BarBase = BarBase.X;
			base.GraphPane.BarType = BarType.Overlay;
			
			base.ZedGraphControl.AxisChange();
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			//myPane.AxisChange( this.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			base.GraphPane.YAxis.Max += base.GraphPane.YAxis.Step;

			base.ZedGraphControl.AxisChange();
		}
	}
}
