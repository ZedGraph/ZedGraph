//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2006 John Champion and Jerry Vos
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
	public class OHLCBarDemo : DemoBase
	{

		public OHLCBarDemo()
			: base( "Demonstration of the OHLCBar Chart Type",
									"OHLCBar Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title.Text = "Open-High-Low-Close Bar Chart Demo";
			myPane.XAxis.Title.Text = "Trading Date";
			myPane.YAxis.Title.Text = "Share Price, $US";

			StockPointList spl = new StockPointList();
			Random rand = new Random();

			// First day is jan 1st
			XDate xDate = new XDate( 2006, 1, 1 );
			double open = 50.0;

			for ( int i = 0; i < 50; i++ )
			{
				double x = xDate.XLDate;
				double close = open + rand.NextDouble() * 10.0 - 5.0;
				double hi = Math.Max( open, close ) + rand.NextDouble() * 5.0;
				double low = Math.Min( open, close ) - rand.NextDouble() * 5.0;

				StockPt pt = new StockPt( x, hi, low, open, close, 100000 );
				spl.Add( pt );

				open = close;
				// Advance one day
				xDate.AddDays( 1.0 );
				// but skip the weekends
				if ( XDate.XLDateToDayOfWeek( xDate.XLDate ) == 6 )
					xDate.AddDays( 2.0 );
			}

			OHLCBarItem myCurve = myPane.AddOHLCBar( "trades", spl, Color.Black );
			myCurve.Bar.IsAutoSize = true;
			myCurve.Bar.Color = Color.Blue;

			// Use DateAsOrdinal to skip weekend gaps
			myPane.XAxis.Type = AxisType.DateAsOrdinal;
			myPane.XAxis.Scale.Min = new XDate( 2006, 1, 1 );

			// pretty it up a little
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );

			base.ZedGraphControl.AxisChange();
		}
	}
}
