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
	public class StackedBarSampleDemo : DemoBase
	{

		public StackedBarSampleDemo() : base( "Code Project Stacked Bar Chart Sample",
			"Stacked Bar Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title = "Cat Stats";
			myPane.XAxis.Title = "Big Cats";
			myPane.YAxis.Title = "Population";
			
			// Make up some data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] y = { 100, 115, 75, 22, 98, 40 };
			double[] y2 = { 120, 175, 95, 57, 113, 110 };
			double[] y3 = { 204, 192, 119, 80, 134, 156 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = myPane.AddBar( "Here", null, y, Color.Red );
			// Fill the bar with a red-white-red color gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "There", null, y2, Color.Blue );
			// Fill the bar with a Blue-white-Blue color gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Elsewhere", null, y3, Color.Green );
			// Fill the bar with a Green-white-Green color gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;

			// Set the bar type to stack, which stacks the bars by automatically accumulating the values
			myPane.BarType = BarType.Stack;
			
			base.ZedGraphControl.AxisChange();
		}
	}
}
