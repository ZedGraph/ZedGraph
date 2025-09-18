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
using System.Text;
using System.Drawing;
using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for MultiYDemo.
	/// </summary>
	public class MultiYDemo : DemoBase
	{

		public MultiYDemo() : base( "A line graph with four Y axes",
			"Multi Y Demo", DemoType.Line )
		{
			GraphPane	myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Demonstration of Multi Y Graph";
			myPane.XAxis.Title.Text = "Time, s";
			myPane.YAxis.Title.Text = "Velocity, m/s";
			myPane.Y2Axis.Title.Text = "Acceleration, m/s2";

			// Make up some data points based on the Sine function
			PointPairList vList = new PointPairList();
			PointPairList aList = new PointPairList();
			PointPairList dList = new PointPairList();
			PointPairList eList = new PointPairList();

			// Fabricate some data values
			for ( int i=0; i<30; i++ )
			{
				double time = (double) i;
				double acceleration = 2.0;
				double velocity = acceleration * time;
				double distance = acceleration * time * time / 2.0;
				double energy = 100.0 * velocity * velocity / 2.0;
				aList.Add( time, acceleration );
				vList.Add( time, velocity );
				eList.Add( time, energy );
				dList.Add( time, distance );
			}

			// Generate a red curve with diamond symbols, and "Velocity" in the legend
			LineItem myCurve = myPane.AddCurve( "Velocity",
				vList, Color.Red, SymbolType.Diamond );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with circle symbols, and "Acceleration" in the legend
			myCurve = myPane.AddCurve( "Acceleration",
				aList, Color.Blue, SymbolType.Circle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;

			// Generate a green curve with square symbols, and "Distance" in the legend
			myCurve = myPane.AddCurve( "Distance",
				dList, Color.Green, SymbolType.Square );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the second Y axis
			myCurve.YAxisIndex = 1;

			// Generate a Black curve with triangle symbols, and "Energy" in the legend
			myCurve = myPane.AddCurve( "Energy",
				eList, Color.Black, SymbolType.Triangle );
			// Fill the symbols with white
			myCurve.Symbol.Fill = new Fill( Color.White );
			// Associate this curve with the Y2 axis
			myCurve.IsY2Axis = true;
			// Associate this curve with the second Y2 axis
			myCurve.YAxisIndex = 1;

			// Show the x axis grid
			myPane.XAxis.MajorGrid.IsVisible = true;

			// Make the Y axis scale red
			myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
			myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;
			// Don't display the Y zero line
			myPane.YAxis.MajorGrid.IsZeroLine = false;
			// Align the Y axis labels so they are flush to the axis
			myPane.YAxis.Scale.Align = AlignP.Inside;
			myPane.YAxis.Scale.Max = 100;

			// Enable the Y2 axis display
			myPane.Y2Axis.IsVisible = true;
			// Make the Y2 axis scale blue
			myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			myPane.Y2Axis.MajorTic.IsOpposite = false;
			myPane.Y2Axis.MinorTic.IsOpposite = false;
			// Display the Y2 axis grid lines
			myPane.Y2Axis.MajorGrid.IsVisible = true;
			// Align the Y2 axis labels so they are flush to the axis
			myPane.Y2Axis.Scale.Align = AlignP.Inside;
			myPane.Y2Axis.Scale.Min = 1.5;
			myPane.Y2Axis.Scale.Max = 3;

			// Create a second Y Axis, green
			YAxis yAxis3 = new YAxis( "Distance, m" );
			myPane.YAxisList.Add( yAxis3 );
			yAxis3.Scale.FontSpec.FontColor = Color.Green;
			yAxis3.Title.FontSpec.FontColor = Color.Green;
			yAxis3.Color = Color.Green;
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxis3.MajorTic.IsInside = false;
			yAxis3.MinorTic.IsInside = false;
			yAxis3.MajorTic.IsOpposite = false;
			yAxis3.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxis3.Scale.Align = AlignP.Inside;

			Y2Axis yAxis4 = new Y2Axis( "Energy" );
			yAxis4.IsVisible = true;
			myPane.Y2AxisList.Add( yAxis4 );
			// turn off the opposite tics so the Y2 tics don't show up on the Y axis
			yAxis4.MajorTic.IsInside = false;
			yAxis4.MinorTic.IsInside = false;
			yAxis4.MajorTic.IsOpposite = false;
			yAxis4.MinorTic.IsOpposite = false;
			// Align the Y2 axis labels so they are flush to the axis
			yAxis4.Scale.Align = AlignP.Inside;
			yAxis4.Type = AxisType.Log;
			yAxis4.Scale.Min = 100;

			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );

			base.ZedGraphControl.AxisChange();
		}
	}
}
