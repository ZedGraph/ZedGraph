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
	public class CrossLineDemo : DemoBase
	{

		public CrossLineDemo() : base( "Simple Graph with Y Axis in Alternate Location",
			"Axis Cross Demo", DemoType.Line, DemoType.Special )
		{
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "Axis Cross Demo";
			myPane.XAxis.Title = "My X Axis";
			myPane.YAxis.Title = "My Y Axis";
			
			// Make up some data arrays based on the Sine function
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i=0; i<37; i++ )
			{
				x = ((double) i - 18.0 ) / 5.0;
				y = x * x + 1.0;
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = myPane.AddCurve( "Parabola",
				list, Color.Green, SymbolType.Diamond );

			// Set the Y axis intersect the X axis at an X value of 0.0
			myPane.YAxis.Cross = 0.0;
			// Turn off the axis frame and all the opposite side tics
			myPane.AxisBorder.IsVisible = false;
			myPane.XAxis.IsOppositeTic = false;
			myPane.XAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed

			base.ZedGraphControl.AxisChange();
		}
	}
}
