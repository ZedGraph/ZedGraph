//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2005 John Champion, Jerry Vos and Bob Kaye
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
	public class PercentStkBarDemo : DemoBase
	{

		public PercentStkBarDemo() : base( "	A Stack Bar Chart where a bar's height represents " +
									"its percentage of the total of all bar values.",
			"Percent Stack Bar Chart", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Make up some data points
			string [] quarters = {"Q1-'04", "Q2-'04", "Q3-'04", "Q4-'04" } ;          
			double[] y4 = { 20, 15, 90, 70 };      
			double[] y3 = { 0, 35, 40,10 };             
			double[] y2 = { 60, 70, 20,30 };               
			double[] y5 = new double [4] ;

			// Set the pane title
			myPane.Title.Text = "% Product Sales by Quarter by Type";
			// Position the legend and fill the background
			myPane.Legend.Position = LegendPos.TopCenter ;
			myPane.Legend.Fill.Color = Color.LightCyan ;
			// Fill the pane background with a solid color
			myPane.Fill.Color = Color.Cornsilk ;
			// Fill the axis background with a solid color
			myPane.Chart.Fill.Type = FillType.Solid ;
			myPane.Chart.Fill.Color = Color.LightCyan ;
			// Set the bar type to percent stack, which makes the bars sum up to 100%
			myPane.BarSettings.Type = BarType.PercentStack ;

			// Set the X axis title
			myPane.XAxis.Title.Text = "Quarter";
			myPane.XAxis.Type = AxisType.Text ;
			myPane.XAxis.Scale.TextLabels = quarters ;   
      
			// Set the Y2 axis properties
			myPane.Y2Axis.Title.Text = "2004 Total Sales ($M)"  ;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.MinorTic.IsOpposite = false;
			myPane.Y2Axis.MajorTic.IsOpposite = false;
			myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Red ;

			// Set the Y axis properties
			myPane.YAxis.Title.Text = "" ;
			myPane.YAxis.Scale.Max = 120 ;
			myPane.YAxis.MinorTic.IsOpposite = false;
			myPane.YAxis.MajorTic.IsOpposite = false;

			// get total values into array for Line
			for ( int x = 0 ; x < 4 ; x++ )
				y5[x] = y4[x] + y3[x] + y2[x] ;

			// Add a curve to the graph
			LineItem curve;
			curve = myPane.AddCurve( "Total Sales", null, y5, Color.Black, SymbolType.Circle );
			// Associate the curve with the Y2 axis
			curve.IsY2Axis = true ;
			curve.Line.Width = 1.5F;
			// Make the symbols solid red
			curve.Line.Color = Color.Red ;
			curve.Symbol.Fill = new Fill( Color.Red );
			myPane.Y2Axis.Title.FontSpec.FontColor = Color.Red ;
			curve.Symbol.Size = 8;

			// Add a gradient blue bar
			BarItem bar = myPane.AddBar( "Components", null, y4, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );

			// Add a gradient green bar
			bar = myPane.AddBar( "Misc",  null, y3, Color.LimeGreen );
			bar.Bar.Fill = new Fill( Color.LimeGreen, Color.White, Color.LimeGreen );

			// Add a gradient yellow bar
			bar = myPane.AddBar( "Assemblies",null, y2, Color.Yellow );
			bar.Bar.Fill = new Fill( Color.Yellow, Color.White, Color.Yellow );

			base.ZedGraphControl.AxisChange();

		}
	}	
}
