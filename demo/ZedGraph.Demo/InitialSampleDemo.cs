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
	public class InitialSampleDemo : DemoBase
	{

		public InitialSampleDemo() : base( "Code Project Initial Sample",
			"Initial Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "My Test Graph\n(For CodeProject Sample)";
			myPane.XAxis.Title = "My X Axis";
			myPane.YAxis.Title = "My Y Axis";
			
			// Make up some data arrays based on the Sine function
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				x = (double) i + 5;
				y1 = 1.5 + Math.Sin( (double) i * 0.2 );
				y2 = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
				list1.Add( x, y1 );
				list2.Add( x, y2 );
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = myPane.AddCurve( "Porsche",
				list1, Color.Red, SymbolType.Diamond );

			// Generate a blue curve with circle
			// symbols, and "Piper" in the legend
			LineItem myCurve2 = myPane.AddCurve( "Piper",
				list2, Color.Blue, SymbolType.Circle );

			base.ZedGraphControl.AxisChange();
		}
	}
}
