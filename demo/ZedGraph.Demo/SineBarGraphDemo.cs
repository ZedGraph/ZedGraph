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
	public class SineBarGraphDemo : DemoBase
	{
		public SineBarGraphDemo() : base( "A bar graph displaying values of a sine wave.",
											"Sine Bar Graph Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane.Title = "A Sine wave displayed by a bar graph.";
			myPane.XAxis.Title = "Label";
			myPane.YAxis.Title = "My Y Axis";

			const int size = 41;
			string[] labels = new string[size];
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();

			for ( int i=0; i<size; i++ )
			{
				double x = i + 1;
				double y = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 5.0;
				double y2 = Math.Sin( (double) i / 30.0 * 2.0 * Math.PI ) * 50.0 + 25.0;
				labels[i] = "lab" + (i+1).ToString();
				list.Add( x, y );
				list2.Add( x, y2 );
			}

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			BarItem myBar = myPane.AddBar( "Curve 1", list, Color.Red );

			// Generate a blue bar with "Curve 2" in the legend
			myBar = myPane.AddBar( "Curve 2", list2, Color.Blue );

			// Set the XAxis labels
			myPane.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Ordinal;

			myPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );

			base.ZedGraphControl.AxisChange();
		}

	}
}
