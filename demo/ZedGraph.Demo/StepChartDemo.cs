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
	public class StepChartDemo : DemoBase
	{

		public StepChartDemo() : base( "A sample step chart",
									"Step Chart Demo", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Demo for Step Charts";
			myPane.XAxis.Title.Text = "Time, Days";
			myPane.YAxis.Title.Text = "Widget Production (units/hour)";

			// Generate some sine-based data values
			PointPairList list = new PointPairList();
			for ( double i=0; i<36; i++ )
			{
				double x = i * 5.0;
				double y = Math.Sin( i * Math.PI / 15.0 ) * 16.0;
				list.Add( x, y );
			}

			// Add a red curve with circle symbols
			LineItem curve = myPane.AddCurve( "Step", list, Color.Red, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			// Fill the symbols with white to make them opaque
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 5;

			// Set the curve type to forward steps
			curve.Line.StepType = StepType.ForwardStep;
			
			base.ZedGraphControl.AxisChange();
		}
	}
}
