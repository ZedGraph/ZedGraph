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
	public class DualYDemo : DemoBase
	{

		public DualYDemo() : base( "A simple line graph with dual Y axes",
			"Dual Y Demo", DemoType.Line )
		{
			GraphPane	myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "Demonstration of Dual Y Graph";
			myPane.XAxis.Title = "Time, Days";
			myPane.YAxis.Title = "Parameter A";
			myPane.Y2Axis.Title = "Parameter B";
			
			// Make up some data points based on the Sine function
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				double x = (double) i * 5.0;
				double y = Math.Sin( (double) i * Math.PI / 15.0 ) * 16.0;
				double y2 = y * 13.5;
				list.Add( x, y );
				list2.Add( x, y2 );
			}

			// Generate a red curve with diamond symbols, and "Alpha" in the legend
			LineItem myCurve = myPane.AddCurve( "Alpha",
				list, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with circle symbols, and "Beta" in the legend
			myCurve = myPane.AddCurve( "Beta",
				list2, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;

			// Show the x axis grid
			myPane.XAxis.IsShowGrid = true;

			// Make the Y axis scale red
			myPane.YAxis.ScaleFontSpec.FontColor = Color.Red;
			myPane.YAxis.TitleFontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			// Don't display the Y zero line
			myPane.YAxis.IsZeroLine = false;
			// Align the Y axis labels so they are flush to the axis
			myPane.YAxis.ScaleAlign = AlignP.Inside;
			// Manually set the axis range
			myPane.YAxis.Min = -30;
			myPane.YAxis.Max = 30;

			// Enable the Y2 axis display
			myPane.Y2Axis.IsVisible = true;
			// Make the Y2 axis scale blue
			myPane.Y2Axis.ScaleFontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.TitleFontSpec.FontColor = Color.Blue;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			myPane.Y2Axis.IsOppositeTic = false;
			myPane.Y2Axis.IsMinorOppositeTic = false;
			// Display the Y2 axis grid lines
			myPane.Y2Axis.IsShowGrid = true;
			// Align the Y2 axis labels so they are flush to the axis
			myPane.Y2Axis.ScaleAlign = AlignP.Inside;

			// Fill the axis background with a gradient
			myPane.AxisFill = new Fill( Color.White, Color.LightGray, 45.0f );

			base.ZedGraphControl.AxisChange();
		}
	}
}
