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
	public class OverlayBarDemo : DemoBase
	{

		public OverlayBarDemo() : base(
						"The BarType.Overlay 'stacks' the bars without actually summing up the values " +
						"(i.e., they are drawn on top of eachother)\n" +
						"The data must be sorted for this type of chart to be effective\n" +
						"You can also use BarType.Stack and BarType.SortedOverlay for similar effects",
						"Overlay Bar Chart",
						DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;
			myPane.Title = "Overlay Bar Graph Demo";
			myPane.YAxis.Title = "Value";
			
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			for ( int i=0; i<y.GetLength(0); i++ )
				y2[i] += y[i];
			for ( int i=0; i<y2.GetLength(0); i++ )
				y3[i] += y2[i];

			//double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = myPane.AddBar( "Curve 1",
				null, y, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddBar( "Curve 2",
				null, y2, Color.Blue );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddBar( "Curve 3",
				null, y3, Color.Green );

			// Draw the X tics between the labels instead of at the labels
			myPane.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				// format the label string to have 1 decimal place
				string lab = y3[i].ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (i+1), (float) (y3[i] < 0 ? 0.0 : y3[i]) + shift );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.Location.CoordinateFrame = CoordType.AxisXYScale;
				// AlignH the left-center of the text to the specified point
				text.Location.AlignH = AlignH.Left;
				text.Location.AlignV = AlignV.Center;
				text.FontSpec.Border.IsVisible = false;
				text.FontSpec.Fill.IsVisible = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 90;
				// add the TextItem to the list
				myPane.GraphItemList.Add( text );
			}
			
			myPane.BarBase = BarBase.X;
			myPane.BarType = BarType.Overlay;
			
			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0F );

			base.ZedGraphControl.AxisChange();

			// Add one step to the max scale value to leave room for the labels
			myPane.YAxis.Max += myPane.YAxis.Step;
		}
	}
}
