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

			myPane.Title = "Demonstration of Dual Y Graph";
			myPane.XAxis.Title = "Time, Days";
			myPane.YAxis.Title = "Parameter A";
			myPane.Y2Axis.Title = "Parameter B";
			
			// Make up some random data points
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

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "Alpha",
				list, Color.Red, SymbolType.Diamond );
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue curve with diamond
			// symbols, and "My Curve" in the legend
			myCurve = myPane.AddCurve( "Beta",
				list2, Color.Blue, SymbolType.Circle );
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve.IsY2Axis = true;

			myPane.XAxis.IsShowGrid = true;

			myPane.YAxis.ScaleFontSpec.FontColor = Color.Red;
			myPane.YAxis.TitleFontSpec.FontColor = Color.Red;
			myPane.YAxis.IsOppositeTic = false;
			myPane.YAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsZeroLine = false;
			myPane.YAxis.ScaleAlign = AlignP.Inside;
			myPane.YAxis.Min = -30;
			myPane.YAxis.Max = 30;

			myPane.Y2Axis.ScaleFontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.TitleFontSpec.FontColor = Color.Blue;
			myPane.Y2Axis.IsOppositeTic = false;
			myPane.Y2Axis.IsMinorOppositeTic = false;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.IsShowGrid = true;
			myPane.Y2Axis.ScaleAlign = AlignP.Inside;

			myPane.AxisFill = new Fill( Color.White, Color.LightGray, 45.0f );

			base.ZedGraphControl.AxisChange();
		}
	}
}
