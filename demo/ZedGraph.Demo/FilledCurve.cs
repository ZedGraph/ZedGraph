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
	public class FilledCurveDemo : DemoBase
	{

		public FilledCurveDemo() : base( "A Line Graph with the Area Under the Curves Filled",
			"Filled Curve Demo", DemoType.General, DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "My Test Date Graph";
			myPane.XAxis.Title = "Date";
			myPane.YAxis.Title = "My Y Axis";
			
			// Make up some random data points
			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				double x = new XDate( 1995, i+1, 1 );
				double y = Math.Sin( (double) i * Math.PI / 15.0 );
				double y2 = 2 * y;

				list.Add( x, y );
				list2.Add( x, y2 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve2 = myPane.AddCurve( "My Curve 2",
				list, Color.Blue, SymbolType.Circle );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Red, 45F );
			myCurve2.Symbol.Fill = new Fill( Color.White );

			LineItem myCurve = myPane.AddCurve( "My Curve",
				list2, Color.MediumVioletRed, SymbolType.Diamond );
			myCurve.Line.Fill = new Fill( Color.White, Color.Green );
			myCurve.Symbol.Fill = new Fill( Color.White );
			
			// Set the XAxis to date type
			myPane.XAxis.Type = AxisType.Date;

			myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
