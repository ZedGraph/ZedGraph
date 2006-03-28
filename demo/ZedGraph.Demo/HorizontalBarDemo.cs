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
	public class HorizontalBarDemo : DemoBase
	{
		public HorizontalBarDemo() : base( "A sideways bar demo.\n" +
								"This demo also shows how to add labels to the bars, in this " +
								"case showing the value of each bar",
								"HorizontalBar Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Horizontal Bar Graph";
			myPane.XAxis.Title.Text = "Performance Factor";
			myPane.YAxis.Title.Text = "Grouping";
			
			// Enter some random data values
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			// Generate a bar with "Curve 1" in the legend
			BarItem myCurve = myPane.AddBar( "Curve 1", y, null, Color.Red );
			// Fill the bar with a red-white-red gradient
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90F );
			
			// Generate a bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2", y2, null, Color.Blue );
			// Fill the bar with a blue-white-blue gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90F );

			// Generate a bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3", y3, null, Color.Green );
			// Fill the bar with a Green-white-Green gradient for a 3d look
			myCurve.Bar.Fill = new Fill( Color.White, Color.Green, 90F );

			// Draw the X tics between the labels instead of at the labels
			myPane.YAxis.MajorTic.IsBetweenLabels = true;

			// Set the XAxis to an ordinal type
			myPane.YAxis.Type = AxisType.Ordinal;
			// draw the X axis zero line
			myPane.XAxis.MajorGrid.IsZeroLine = true;

			//This is the part that makes the bars horizontal
			myPane.BarSettings.Base = BarBase.Y;
			
			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );

			base.ZedGraphControl.AxisChange();

			// The ValueHandler is a helper that does some position calculations for us.
			ValueHandler valueHandler = new ValueHandler( myPane, true );

			// Display a value for the maximum of each bar cluster
			// Shift the text items by 5 user scale units above the bars
			const float shift = 3;
			
			int ord = 0;
			foreach ( CurveItem curve in myPane.CurveList )
			{
				BarItem bar = curve as BarItem;

				if ( bar != null )
				{
					IPointList points = curve.Points;

					for ( int i=0; i<points.Count; i++ )
					{
						double xVal = points[i].X;
						// Calculate the Y value at the center of each bar
						double yVal = valueHandler.BarCenterValue( curve, curve.GetBarWidth( myPane ),
									i, points[i].Y, ord );

						// format the label string to have 1 decimal place
						string lab = xVal.ToString( "F0" );

						// create the text item (assumes the x axis is ordinal or text)
						// for negative bars, the label appears just above the zero value
						TextObj text = new TextObj( lab, (float) xVal + ( xVal > 0 ? shift : -shift ),
														(float) yVal );

						// tell Zedgraph to use user scale units for locating the TextObj
						text.Location.CoordinateFrame = CoordType.AxisXYScale;
						text.FontSpec.Size = 10;
						// AlignH the left-center of the text to the specified point
						text.Location.AlignH =  xVal > 0 ? AlignH.Left : AlignH.Right;
						text.Location.AlignV = AlignV.Center;
						text.FontSpec.Border.IsVisible = false;
						// rotate the text 90 degrees
						text.FontSpec.Angle = 0;
						text.FontSpec.Fill.IsVisible = false;
						// add the TextObj to the list
						myPane.GraphObjList.Add( text );
					}
				}

				ord++;
			}
		}
	}
}
