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

			// Set the titles and axis labels
			myPane.Title.Text = "Error Bar Demo Chart";
			myPane.XAxis.Title.Text = "Label";
			myPane.YAxis.Title.Text = "My Y Axis";
			
			// Make up some data points based on the Sine function
			PointPairList list = new PointPairList();
			for ( int i=0; i<44; i++ )
			{
				double x = i / 44.0;
				double y = Math.Sin( (double) i * Math.PI / 15.0 );
				double yBase = y - 0.4;
				list.Add( x, y, yBase );
			}

			// Generate a red bar with "Curve 1" in the legend
			ErrorBarItem myCurve = myPane.AddErrorBar( "Curve 1", list, Color.Red );
			// Make the X axis the base for this curve (this is the default)
			myPane.BarSettings.Base = BarBase.X;
			myCurve.Bar.PenWidth = 1f;
			// Use the HDash symbol so that the error bars look like I-beams
			myCurve.Bar.Symbol.Type = SymbolType.HDash;
			myCurve.Bar.Symbol.Border.Width = .1f;
			myCurve.Bar.Symbol.IsVisible = true;
			myCurve.Bar.Symbol.Size = 4;
			
			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.LightGoldenrodYellow, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
