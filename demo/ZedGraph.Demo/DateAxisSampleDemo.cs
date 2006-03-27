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
	public class DateAxisSampleDemo : DemoBase
	{

		public DateAxisSampleDemo() : base( "Code Project Date Axis Sample",
			"Date Axis Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "My Test Date Graph";
			myPane.XAxis.Title = "Date";
			myPane.YAxis.Title = "My Y Axis";
			   
			// Make up some data points based on the Sine function
			PointPairList list = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				double x = (double) new XDate( 1995, 5, i+11 );
				double y = Math.Sin( (double) i * Math.PI / 15.0 );
				list.Add( x, y );
			}

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "My Curve",
				list, Color.Red, SymbolType.Diamond );
      
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;

			base.ZedGraphControl.AxisChange();
		}
	}
}
