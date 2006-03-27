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
	/// Summary description for SimpleDemo.
	/// </summary>
	public class BarChartSampleDemo : DemoBase
	{

		public BarChartSampleDemo() : base( "Code Project Bar Chart Sample",
			"Bar Chart Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "My Test Bar Graph";
			myPane.XAxis.Title = "Label";
			myPane.YAxis.Title = "My Y Axis";
			
			// Make up some random data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 90, 100, 95, 35, 80, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67 };
			double[] y4 = { 120, 125, 100, 40, 105, 75 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = myPane.AddBar( "Curve 1", null, y, Color.Red );
			myBar.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = myPane.AddBar( "Curve 2", null, y2, Color.Blue );
			myBar.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myBar = myPane.AddBar( "Curve 3", null, y3, Color.Green );
			myBar.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green );

			// Generate a black line with "Curve 4" in the legend
			LineItem myCurve = myPane.AddCurve( "Curve 4",
				null, y4, Color.Black, SymbolType.Circle );
			myCurve.Line.Fill = new Fill( Color.White, Color.LightSkyBlue, -45F );

			// Fix up the curve attributes a little
			myCurve.Symbol.Size = 8.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.Line.Width = 2.0F;

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			// Fill the axis area with a gradient
			myPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );
			// Fill the pane area with a solid color
			myPane.PaneFill = new Fill( Color.FromArgb( 250, 250, 255) );

			base.ZedGraphControl.AxisChange();
		}
	}
}
