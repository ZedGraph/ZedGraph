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
	public class ModInitialSampleDemo : DemoBase
	{

		public ModInitialSampleDemo() : base( "Code Project Modified Initial Sample",
			"Modified Initial Sample", DemoType.Tutorial )
		{
			GraphPane myPane = base.GraphPane;

			// Set up the title and axis labels
			myPane.Title.Text = "My Test Graph\n(For CodeProject Sample)";
			myPane.XAxis.Title.Text = "My X Axis";
			myPane.YAxis.Title.Text = "My Y Axis";
			
			// Make up some data arrays based on the Sine function
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			for ( int i=0; i<36; i++ )
			{
				double x = (double) i + 5;
				double y1 = 1.5 + Math.Sin( (double) i * 0.2 );
				double y2 = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 ) );
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

			// Change the color of the title
			myPane.Title.FontSpec.FontColor = Color.Green;

			// Add gridlines to the plot, and make them gray
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;
			myPane.XAxis.MajorGrid.Color = Color.LightGray;
			myPane.YAxis.MajorGrid.Color = Color.LightGray;

			// Move the legend location
			base.Pane.Legend.Position = ZedGraph.LegendPos.Bottom;

			// Make both curves thicker
			myCurve.Line.Width = 2.0F;
			myCurve2.Line.Width = 2.0F;

			// Fill the area under the curves
			myCurve.Line.Fill = new Fill( Color.White, Color.Red, 45F );
			myCurve2.Line.Fill = new Fill( Color.White, Color.Blue, 45F );

			// Increase the symbol sizes, and fill them with solid white
			myCurve.Symbol.Size = 8.0F;
			myCurve2.Symbol.Size = 8.0F;
			myCurve.Symbol.Fill = new Fill( Color.White );
			myCurve2.Symbol.Fill = new Fill( Color.White );

			// Add a background gradient fill to the axis frame
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 210), -45F );

			// Add a caption and an arrow
			TextObj myText = new TextObj( "Interesting\nPoint", 230F, 70F );
			myText.FontSpec.FontColor = Color.Red;
			myText.Location.AlignH = AlignH.Center;
			myText.Location.AlignV = AlignV.Top;
			myPane.GraphObjList.Add( myText );
			ArrowObj myArrow = new ArrowObj( Color.Red, 12F, 230F, 70F, 280F, 55F );
			myPane.GraphObjList.Add( myArrow );

			base.ZedGraphControl.AxisChange();
		}
	}
}
