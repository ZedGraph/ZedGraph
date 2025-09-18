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
	public class TextAxisSampleDemo : DemoBase
	{

		public TextAxisSampleDemo() : base( "Code Project Text Axis Sample",
			"Text Axis Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "My Test Date Graph";
			myPane.XAxis.Title.Text = "Label";
			myPane.YAxis.Title.Text = "My Y Axis";
			
			// Make up some data points
			string[] labels = { "USA", "Spain\nMadrid", "Qatar", "Morocco", "UK", "Uganda",
								  "Cambodia", "Malaysia", "Australia", "Ecuador" };
			double[] y = new double[10];
			for ( int i=0; i<10; i++ )
				y[i] = Math.Sin( (double) i * Math.PI / 2.0 );

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			LineItem myCurve = myPane.AddCurve( "My Curve",
				null, y, Color.Red, SymbolType.Diamond );

			//Make the curve smooth
			myCurve.Line.IsSmooth = true;
			
			// Set the XAxis to Text type
			myPane.XAxis.Type = AxisType.Text;
			// Set the XAxis labels
			myPane.XAxis.Scale.TextLabels = labels;
			// Set the labels at an angle so they don't overlap
			myPane.XAxis.Scale.FontSpec.Angle = 40;

			base.ZedGraphControl.AxisChange();
		}
	}
}
