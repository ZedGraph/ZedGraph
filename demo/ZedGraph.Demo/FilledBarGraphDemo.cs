//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2005 John Champion and Jerry Vos
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================
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
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "Image Fill Example";
			myPane.XAxis.Title = "Region";
			myPane.YAxis.Title = "Astronomy Sector Sales";

			// Make up some random data points
			double[] y = { 80, 70, 65, 78, 40 };
			double[] y2 = { 70, 50, 85, 54, 63 };
			string[] str = { "North", "South", "West", "East", "Central" };

			// Add a bar to the graph
			BarItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.White );
			// Access a image from the resources
			Image image = Bitmap.FromStream(
				GetType().Assembly.GetManifestResourceStream("ZedGraph.Demo.ngc4414.jpg") );
			// create a brush with the image
			TextureBrush brush = new TextureBrush( image );
			// use the image for the bar fill
			myCurve.Bar.Fill = new Fill( brush );
			// turn off the bar border
			myCurve.Bar.Border.IsVisible = false;

			// Add a second bar to the graph
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.White );
			// Access a image from the resources
			Image image2 = Bitmap.FromStream(
				GetType().Assembly.GetManifestResourceStream("ZedGraph.Demo.ngc4261.gif") );
			// create a brush with the image
			TextureBrush brush2 = new TextureBrush( image2 );			
			// use the image for the bar fill
			myCurve.Bar.Fill = new Fill( brush2 );
			// turn off the bar border
			myCurve.Bar.Border.IsVisible = false;
			
			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = str;

			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			// Fill the axis background with a color gradient
			myPane.AxisFill = new Fill( Color.White, Color.SteelBlue, 45.0f );

			// disable the legend
			myPane.Legend.IsVisible = false;
			
			base.ZedGraphControl.AxisChange();
		}
	}
}
