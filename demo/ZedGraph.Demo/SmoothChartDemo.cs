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
	public class SmoothChartDemo : DemoBase
	{

		public SmoothChartDemo() : base( "A Chart Demonstrating the different line types",
									"Smooth Chart Demo", DemoType.General, DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			// set the title and axis labels
			myPane.Title.Text = "Smooth Line Demo";
			myPane.XAxis.Title.Text = "Value";
			myPane.YAxis.Title.Text = "Time";
			
			// Enter some arbitrary data
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };

			// Add a smoothed curve
			LineItem curve = myPane.AddCurve( "Smooth (Tension=0.5)", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			// activate the cardinal spline smoothing
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.5F;

			// Add a forward step type curve
			curve = myPane.AddCurve( "Forward Step", x, y, Color.Green, SymbolType.Circle );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.StepType = StepType.ForwardStep;

			// add a rearward step type curve
			curve = myPane.AddCurve( "Rearward Step", x, y, Color.Gold, SymbolType.Square );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.StepType = StepType.RearwardStep;

			// add a regular non-step, non-smooth curve
			curve = myPane.AddCurve( "Non-Step", x, y, Color.Blue, SymbolType.Triangle );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGray, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
