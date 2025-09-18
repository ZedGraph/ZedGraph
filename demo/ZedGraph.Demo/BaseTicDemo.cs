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
	/// Summary description for BaseTicDemo
	/// </summary>
	public class BaseTicDemo : DemoBase
	{

		public BaseTicDemo() : base( "Demo for the Axis.BaseTic property\n" +
				"BaseTic allows you to begin the major tics at an arbitrary value (such as 50, in this case)",
				"BaseTic Demo", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Demo of BaseTic property";
			myPane.XAxis.Title.Text = "Time, Days";
			myPane.YAxis.Title.Text = "Widget Production (units/hour)";

			// Build a PointPairList with points based on Sine wave
			PointPairList list = new PointPairList();
			for ( double i=0; i<36; i++ )
			{
				double x = i * 10.0 + 50.0;
				double y = Math.Sin( i * Math.PI / 15.0 ) * 16.0;
				list.Add( x, y );
			}

			// Hide the legend
			myPane.Legend.IsVisible = false;

			// Add a curve
			LineItem curve = myPane.AddCurve( "label", list, Color.Red, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			// Make the XAxis start with the first label at 50
			myPane.XAxis.Scale.BaseTic = 50;
			
			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.SteelBlue, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
