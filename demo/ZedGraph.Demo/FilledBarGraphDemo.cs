using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for BarGraphBandDemo.
	/// </summary>
	public class FilledBarGraphDemo : DemoBase
	{
		public FilledBarGraphDemo() : base( "A demonstration of using a custom fill on a bar graph.",
											"Filled Bar Graph Demo", DemoType.Bar, DemoType.Special )
		{
			// Create a new graph with topLeft at (40,40) and size 600x400
			base.GraphPane.Title = "Cheesy Texture Fill Sample";
			// Make up some random data points
			double[] y = { 80, 70, 65, 78, 40 };
			double[] y2 = { 70, 50, 85, 54, 63 };
			string[] str = { "North", "South", "West", "East", "Central" };


			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = base.GraphPane.AddBar( "Curve 1", null, y, Color.Red );

			Image image = 
				Bitmap.FromStream(
				GetType().Assembly.GetManifestResourceStream("ZedGraph.Demo.background.jpg"));
			
			TextureBrush brush = new TextureBrush( image );

			//brush.WrapMode = WrapMode.Clamp;
			
			myCurve.Bar.Fill = new Fill( brush );
			myCurve.Bar.Border.IsVisible = false;

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = base.GraphPane.AddBar( "Curve 2", null, y2, Color.Blue );

//			Bitmap bm2 = new Bitmap( @"c:\shark.gif" );
			//Image image2 = Image.FromHbitmap( bm2.GetHbitmap() );
			TextureBrush brush2 = new TextureBrush( image );			
			myCurve.Bar.Fill = new Fill( brush2 );
			//myCurve.Bar.Fill = new Fill( image2, WrapMode.Tile );
			myCurve.Bar.Border.IsVisible = false;
			
			// Generate a green bar with "Curve 3" in the legend
			//myCurve = base.GraphPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			base.GraphPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			base.GraphPane.XAxis.TextLabels = str;
			// Set the XAxis to Text type
			base.GraphPane.XAxis.Type = AxisType.Text;

			base.GraphPane.XAxis.IsReverse = false;
			base.GraphPane.ClusterScaleWidth = 1;
			base.GraphPane.Legend.IsVisible = false;
			
		}
	}
}
