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
	public class ErrorBarDemo : DemoBase
	{

		public ErrorBarDemo() : base( "An Error Bar Chart",
							"Error Bar Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Error Bar Demo Chart";
			myPane.XAxis.Title = "Label";
			myPane.YAxis.Title = "My Y Axis";
			
			double x, y, yBase;
			PointPairList list = new PointPairList();
			for ( int i=0; i<44; i++ )
			{
				x = i / 44.0;
				y = Math.Sin( (double) i * Math.PI / 15.0 );
				yBase = y - 0.4;
				list.Add( x, y, yBase );
			}

			// Generate a red bar with "Curve 1" in the legend
			ErrorBarItem myCurve = myPane.AddErrorBar( "Curve 1", list, Color.Red );
			myCurve.BarBase = BarBase.X;
			
			myCurve.ErrorBar.PenWidth = 1f;
			myCurve.ErrorBar.Symbol.Type = SymbolType.HDash;
			myCurve.ErrorBar.Symbol.Border.PenWidth = .1f;
			myCurve.ErrorBar.Symbol.IsVisible = true;
			myCurve.ErrorBar.Symbol.Size = 4;
			
			myPane.AxisFill = new Fill( Color.White,
				Color.LightGoldenrodYellow, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
