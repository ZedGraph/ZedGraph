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
			myPane.Title = "% Product Sales by Quarter by Type";
			// Position the legend and fill the background
			myPane.Legend.Position = LegendPos.TopCenter ;
			myPane.Legend.Fill.Color = Color.LightCyan ;
			// Fill the pane background with a solid color
			myPane.PaneFill.Color = Color.Cornsilk ;
			// Fill the axis background with a solid color
			myPane.AxisFill.Type = FillType.Solid ;
			myPane.AxisFill.Color = Color.LightCyan ;
			// Set the bar type to percent stack, which makes the bars sum up to 100%
			myPane.BarType = BarType.PercentStack ;

			// Set the X axis title
			myPane.XAxis.Title = "Quarter";
			myPane.XAxis.Type = AxisType.Text ;
			myPane.XAxis.TextLabels = quarters ;   
      
			// Set the Y2 axis properties
			myPane.Y2Axis.Title = "2004 Total Sales ($M)"  ;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.IsMinorOppositeTic = false;
			myPane.Y2Axis.IsOppositeTic = false;
			myPane.Y2Axis.ScaleFontSpec.FontColor = Color.Red ;

			// Set the Y axis properties
			myPane.YAxis.Title = "" ;
			myPane.YAxis.Max = 120 ;
			myPane.YAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsOppositeTic = false;

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
			myPane.Y2Axis.TitleFontSpec.FontColor = Color.Red ;
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
