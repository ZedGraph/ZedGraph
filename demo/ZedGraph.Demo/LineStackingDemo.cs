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
using System.Collections;
using System.Drawing;

using ZedGraph;


namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for LineStackingDemo.
	/// </summary>
	public class LineStackingDemo : DemoBase
	{
		public LineStackingDemo() : base( "A demo of line stacking in ZedGraph.\n" +
									"Line stacking sums up the curve Y values, and allows you to fill the area between curves",
									"Line Stacking Demo 1", DemoType.Line, DemoType.Special )
		{
			GraphPane	myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title = "Wacky Widget Company\nProduction Report";
			myPane.XAxis.Title = "Time, Days\n(Since Plant Construction Startup)";
			myPane.YAxis.Title = "Widget Production\n(units/hour)";

			// enter some arbitrary data points
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };

			// Add a green curve
			LineItem curve;
			curve = myPane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			// Make the curve smooth with cardinal splines
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			// Fill the symbols with white to make them opaque
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
			
			// Add a second curve
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8, 22.1, 34.3, 10.4, 17.5 };
			curve = myPane.AddCurve( "Moe", x, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			// Fill the symbols with white to make them opaque
			curve.Symbol.Fill = new Fill( Color.White );
			// Fill the area between the curves with color
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			curve.Symbol.Size = 10;
			
			// Fill the pane background with a color gradient
			myPane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			
			// Fill the axis background with a color gradient
			myPane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
				Color.FromArgb( 255, 255, 190), 90F );
			
			// Show the x and y axis gridlines
			myPane.XAxis.IsShowGrid = true;
			myPane.YAxis.IsShowGrid = true;
			
			// Use the stacked curve type so the curve values sum up
			// this also causes only the area between the curves to be filled, rather than
			// the area between each curve and the x axis
			myPane.LineType = LineType.Stack;

			base.ZedGraphControl.AxisChange();
		}
	}
}
