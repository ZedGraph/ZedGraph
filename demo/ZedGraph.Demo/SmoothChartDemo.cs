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

			myPane.Title = "Smooth Line Demo";
			myPane.XAxis.Title = "Value";
			myPane.YAxis.Title = "Time";
			
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };

			LineItem curve = myPane.AddCurve( "Smooth (Tension=0.5)", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.5F;

			curve = myPane.AddCurve( "Forward Step", x, y, Color.Green, SymbolType.Circle );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.StepType = StepType.ForwardStep;

			curve = myPane.AddCurve( "Rearward Step", x, y, Color.Gold, SymbolType.Square );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;
			curve.Line.StepType = StepType.RearwardStep;

			curve = myPane.AddCurve( "Non-Step", x, y, Color.Blue, SymbolType.Triangle );
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			myPane.AxisFill = new Fill( Color.White, Color.LightGray, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
