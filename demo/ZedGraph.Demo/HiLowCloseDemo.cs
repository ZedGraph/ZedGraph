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

			myPane.Title = "ZedgroSoft, International\nHi-Low-Close Daily Stock Chart";
			myPane.XAxis.Title = "";
			myPane.YAxis.Title = "Trading Price, $US";
			
			myPane.FontSpec.Family = "Arial";
			myPane.FontSpec.IsItalic = true;
			myPane.FontSpec.Size = 18;

			double hi, low, close, x;
			PointPairList hList = new PointPairList();
			PointPairList cList = new PointPairList();
			Random rand = new Random();
			close = 45;

			for ( int i=45;	i<65; i++	)
			{
				x = (double) new XDate( 2004, 12, i-30 );
				close = close + 2.0 * rand.NextDouble() - 0.5;
				hi = close + 2.0 * rand.NextDouble();
				low = close - 2.0 * rand.NextDouble();
				hList.Add( x, hi, low );
				cList.Add( x, close );
			}


			LineItem curve;
			curve = myPane.AddCurve( "Closing Price", cList, Color.Black,
				SymbolType.Diamond );
			curve.Line.IsVisible = false ;
			curve.Symbol.Fill = new Fill( Color.Red );
			curve.Symbol.Size = 7;

			ErrorBarItem myCurve = myPane.AddErrorBar(	"Price Range", hList,
				Color.Blue );

			//	Set the XAxis	to date type
			myPane.XAxis.Type =	AxisType.Date;
			myPane.XAxis.Step = 1 ;
			myPane.XAxis.ScaleFontSpec.Size = 12 ;
			myPane.XAxis.ScaleFontSpec.Angle = 65 ;
			myPane.XAxis.MajorUnit = DateUnit.Day ;
			myPane.XAxis.ScaleFontSpec.IsBold = true ;
			myPane.XAxis.ScaleFormat = "d MMM" ;
			myPane.XAxis.Min = hList[0].X - 1 ;

			myCurve.ErrorBar.PenWidth = 3;
			myCurve.ErrorBar.Symbol.IsVisible = false;
			
			myPane.YAxis.IsShowGrid = true ;
			//myPane.YAxis.IsShowMinorGrid = true ;
			myPane.YAxis.MinorStep = 0.5;

			myPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 90F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
