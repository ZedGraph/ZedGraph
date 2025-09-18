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
	public class VerticalBarsWithLabelsDemo : DemoBase
	{
		public VerticalBarsWithLabelsDemo() : base( "A bar graph that includes a text label above each bar",
			"Vertical Bars with Labels Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Vertical Bars with Value Labels Above Each Bar";
			myPane.XAxis.Title.Text = "Position Number";
			myPane.YAxis.Title.Text = "Some Random Thing";

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			// Generate random data for three curves
			for ( int i=0; i<5; i++ )
			{
				double x = (double) i;
				double y = rand.NextDouble() * 1000;
				double y2 = rand.NextDouble() * 1000;
				double y3 = rand.NextDouble() * 1000;
				list.Add( x, y );
				list2.Add( x, y2 );
				list3.Add( x, y3 );
			}

			// create the curves
			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );

			base.ZedGraphControl.AxisChange();

			// expand the range of the Y axis slightly to accommodate the labels
			myPane.YAxis.Scale.Max += myPane.YAxis.Scale.MajorStep;

			// Create TextObj's to provide labels for each bar
			BarItem.CreateBarLabels( myPane, false, "f0" );


			// Create a label for each bar
//			CreateBarLabels( myPane, false, "N0" );
		}
	}
}
