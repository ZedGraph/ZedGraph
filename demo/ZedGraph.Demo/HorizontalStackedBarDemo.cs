//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2005 John Champion, Jerry Vos and Bob Kaye
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
#region Using directives

using System;
using System.Text;
using System.Drawing;

#endregion

namespace ZedGraph.Demo
{
	public class HorizontalStackedBarDemo : DemoBase
	{
		public HorizontalStackedBarDemo() : base( "A demo of stacking bars horizontally",
				"Horizontal Stacked Bar Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Cat Stats";
			myPane.YAxis.Title.Text = "Big Cats";
			myPane.XAxis.Title.Text = "Population";
			
			// Make up some data points
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
			double[] x = { 100, 115, 75, 22, 98, 40 };
			double[] x2 = { 120, 175, 95, 57, 113, 110 };
			double[] x3 = { 204, 192, 119, 80, 134, 156 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = myPane.AddBar( "Here", x, null, Color.Red );
			// Fill the bar with a red-white-red color gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90f );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "There", x2, null, Color.Blue );
			// Fill the bar with a Blue-white-Blue color gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90f );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Elsewhere", x3, null, Color.Green );
			// Fill the bar with a Green-white-Green color gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90f );

			// Draw the Y tics between the labels instead of at the labels
			myPane.YAxis.MajorTic.IsBetweenLabels = true;

			// Set the YAxis labels
			myPane.YAxis.Scale.TextLabels = labels;
			// Set the YAxis to Text type
			myPane.YAxis.Type = AxisType.Text;

			// Set the bar type to stack, which stacks the bars by automatically accumulating the values
			myPane.BarSettings.Type = BarType.Stack;

			// Make the bars horizontal by setting the BarBase to "Y"
			myPane.BarSettings.Base = BarBase.Y;
			
			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
