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
	public class HiLowCloseDemo : DemoBase
	{

		public HiLowCloseDemo() : base( "A demo demonstrating HiLowClose",
										"Hi-Low-Close", DemoType.Bar, DemoType.Special )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "ZedgroSoft, International\nHi-Low-Close Daily Stock Chart";
			myPane.XAxis.Title.Text = "";
			myPane.YAxis.Title.Text = "Trading Price, $US";
			
			// Set the title font characteristics
			myPane.Title.FontSpec.Family = "Arial";
			myPane.Title.FontSpec.IsItalic = true;
			myPane.Title.FontSpec.Size = 18;


			// Generate some random stock price data
			PointPairList hList = new PointPairList();
			PointPairList cList = new PointPairList();
			Random rand = new Random();
			// initialize the starting close price
			double close = 45;

			for ( int i=45;	i<65; i++	)
			{
				double x = (double) new XDate( 2004, 12, i-30 );
				close = close + 2.0 * rand.NextDouble() - 0.5;
				double hi = close + 2.0 * rand.NextDouble();
				double low = close - 2.0 * rand.NextDouble();
				hList.Add( x, hi, low );
				cList.Add( x, close );
			}


			// Make a new curve with a "Closing Price" label
			LineItem curve;
			curve = myPane.AddCurve( "Closing Price", cList, Color.Black,
				SymbolType.Diamond );
			// Turn off the line display, symbols only
			curve.Line.IsVisible = false ;
			// Fill the symbols with solid red color
			curve.Symbol.Fill = new Fill( Color.Red );
			curve.Symbol.Size = 7;

			// Add a blue error bar to the graph
			ErrorBarItem myCurve = myPane.AddErrorBar(	"Price Range", hList,
				Color.Blue );
			myCurve.Bar.PenWidth = 3;
			myCurve.Bar.Symbol.IsVisible = false;
			
			// Set the XAxis to date type
			myPane.XAxis.Type =	AxisType.Date;
			// X axis step size is 1 day
			myPane.XAxis.Scale.MajorStep = 1;
			myPane.XAxis.Scale.MajorUnit = DateUnit.Day ;
			myPane.XAxis.Scale.MinorStep = 0.5;
			myPane.XAxis.Scale.MajorUnit = DateUnit.Day;
			// tilt the x axis labels to an angle of 65 degrees
			myPane.XAxis.Scale.FontSpec.Angle = 65 ;
			myPane.XAxis.Scale.FontSpec.IsBold = true ;
			myPane.XAxis.Scale.FontSpec.Size = 12 ;
			myPane.XAxis.Scale.Format = "d MMM" ;
			// make the x axis scale minimum 1 step less than the minimum data value
			myPane.XAxis.Scale.Min = hList[0].X - 1 ;

			// Display the Y axis grid
			myPane.YAxis.MajorGrid.IsVisible = true ;
			myPane.YAxis.Scale.MinorStep = 0.5;

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
