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
	public class SortedOverlayBarDemo : DemoBase
	{

		public SortedOverlayBarDemo() : base( "The BarType.SortedOverlay is the same as BarType.Overlay, " +
						"except that the values for each cluster are sorted such that the shorter bars " +
						"are drawn in front of the taller bars",
						"Sorted Overlay Bar Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title = "My Test Sorted Overlay Bar Graph";
			myPane.XAxis.Title = "Label";
			myPane.YAxis.Title = "My Y Axis";
			
			// Enter some data values
			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 104, 67, 18 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1", null, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", null, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;
			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			myPane.XAxis.ScaleFontSpec.Size = 10.0F ;
			
			// Make the bars a sorted overlay type so that they are drawn on top of eachother
			// (without summing), and each stack is sorted so the shorter bars are in front
			// of the taller bars
			myPane.BarType = BarType.SortedOverlay;
			
			// Fill the axis background with a color gradient
			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
