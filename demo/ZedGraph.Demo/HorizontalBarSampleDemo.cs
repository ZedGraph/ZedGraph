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
	public class HorizontalBarSampleDemo : DemoBase
	{

		public HorizontalBarSampleDemo() : base( "Code Project Horizontal Bar Chart Sample",
			"Horizontal Bar Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "A Horizontal Percent Stack Graph";
			myPane.XAxis.Title = "Stuff";
			myPane.YAxis.Title = "";
			
			double[] y = { 100, 115, 15, 22, 98 };
			double[] y2 = { 90, 60, 95, 35, 30 };
			double[] y3 = { 20, 40, 105, 15, 30 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myCurve = myPane.AddBar( "Nina", y, null, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90F );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Pinta", y2, null, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90F );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Santa Maria", y3, null, Color.Green );
			myCurve.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90F );

			// Draw the Y tics between the labels instead of at the labels
			myPane.YAxis.IsTicsBetweenLabels = true;

			// Set the YAxis to Ordinal type
			myPane.YAxis.Type = AxisType.Text;
			string[] labels = { "Australia", "Africa", "America", "Asia", "Antartica" };
			myPane.YAxis.TextLabels = labels;
			myPane.XAxis.Max = 110;

			// Make the bars horizontal by setting bar base axis to Y
			myPane.BarBase = BarBase.Y;
			myPane.BarType = BarType.PercentStack;
			
			myPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );
			myPane.Legend.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 250), 90F );
			myPane.PaneFill = new Fill( Color.FromArgb( 250, 250, 255) );

			base.ZedGraphControl.AxisChange();
		}
	}
}
