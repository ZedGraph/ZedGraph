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
	/// Summary description for HiLowBarDemo.
	/// </summary>
	public class HiLowBarDemo : DemoBase
	{
		public HiLowBarDemo() : base( "A demo demonstrating HiLow Bars.\n" +
									"These are bars in which the top and the bottom of the bar is defined with user data",
										"Hi-Low Bar", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Hi-Low Bar Graph Demo";
			myPane.XAxis.Title = "Event";
			myPane.YAxis.Title = "Range of Values";

			PointPairList list = new PointPairList();

			for ( int i=1; i<45; i++ )
			{
				double y = Math.Sin( (double) i * Math.PI / 15.0 );
				double yBase = y - 0.4;
				list.Add( (double) i, y, yBase );
			}

			// Generate a red bar with "Curve 1" in the legend
			HiLowBarItem myCurve = myPane.AddHiLowBar( "Curve 1", list, Color.Red );
			myCurve.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 0 );
			myCurve.Bar.IsMaximumWidth = true;

			myPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );
			
			base.ZedGraphControl.AxisChange();
		}
	}
}
