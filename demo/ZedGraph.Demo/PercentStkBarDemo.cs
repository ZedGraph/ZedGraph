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

			string [] quarters = {"Q1-'04", "Q2-'04", "Q3-'04", "Q4-'04" } ;          
			double[] y4 = { 20, 15, 90, 70 };      
			double[] y3 = { 0, 35, 40,10 };             
			double[] y2 = { 60, 70, 20,30 };               
			double[] y5 = new double [4] ;

			myPane.Title = "% Product Sales by Quarter by Type";
			myPane.Legend.Position = LegendPos.TopCenter ;
			myPane.Legend.Fill.Color = Color.LightCyan ;
			myPane.PaneFill.Color = Color.Cornsilk ;
			myPane.AxisFill.Type = FillType.Solid ;
			myPane.AxisFill.Color = Color.LightCyan ;
			myPane.BarBase=BarBase.X ;
			myPane.BarType = BarType.PercentStack ;

			myPane.XAxis.Title = "Quarter";
			myPane.XAxis.Type = AxisType.Text ;
			myPane.XAxis.TextLabels=quarters ;   
      
			myPane.Y2Axis.Title = "2004 Total Sales ($M)"  ;
			myPane.Y2Axis.IsVisible = true;
			myPane.Y2Axis.IsMinorOppositeTic = false;
			myPane.Y2Axis.IsOppositeTic = false;
			myPane.Y2Axis.ScaleFontSpec.FontColor = Color.Red ;

			myPane.YAxis.Title = "" ;
			myPane.YAxis.Max = 120 ;
			myPane.YAxis.IsMinorOppositeTic = false;
			myPane.YAxis.IsOppositeTic = false;
							 //get total values into array for Line
			for ( int x = 0 ; x < 4 ; x++ )
				y5[x] = y4[x] + y3[x] + y2[x] ;

			LineItem curve;
			curve = myPane.AddCurve( "Total Sales", null, y5, Color.Black, SymbolType.Circle );
			curve.IsY2Axis = true ;
			curve.Line.Width = 1.5F;
			curve.Line.Color = Color.Red ;
			curve.Symbol.Fill = new Fill( Color.Red );
			myPane.Y2Axis.TitleFontSpec.FontColor = Color.Red ;
			curve.Symbol.Size = 8;

			//           vertical stacked bars
			BarItem bar = myPane.AddBar( "Components", null, y4, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );

			bar = myPane.AddBar( "Misc",  null, y3, Color.LimeGreen );
			bar.Bar.Fill = new Fill( Color.LimeGreen, Color.White, Color.LimeGreen );

			bar = myPane.AddBar( "Assemblies",null, y2, Color.Yellow );
			bar.Bar.Fill = new Fill( Color.Yellow, Color.White, Color.Yellow );

			base.ZedGraphControl.AxisChange();

		}
	}	
}
