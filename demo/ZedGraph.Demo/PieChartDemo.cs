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
using System.Collections;
using System.Drawing;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for PieChartDemo.
	/// </summary>
	public class PieChartDemo : DemoBase
	{
		public PieChartDemo() : base( "A demo showing some pie chart features of ZedGraph",
										"Pie Chart Demo", DemoType.Pie )
		{
			GraphPane myPane = base.GraphPane;

			// Set the pane title
			myPane.Title.Text = "2004 ZedGraph Sales by Region\n ($M)";

			// Enter some data values
			double [] values =   { 15, 15, 40, 20 } ;
			double [] values2 =   { 250, 50, 400, 50 } ;
			Color [] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow } ;
			double [] displacement = {	.0,.0,.0,.0 } ;
			string [] labels = { "Europe", "Pac Rim", "South America", "Africa" } ;

			// Fill the pane and axis background with solid color
			myPane.Fill = new Fill( Color.Cornsilk );
			myPane.Chart.Fill = new Fill( Color.Cornsilk );
			myPane.Legend.Position = LegendPos.Right ;
			
			// Create some pie slices
			PieItem segment1 = myPane.AddPieSlice ( 20, Color.Navy, .20, "North") ;
			PieItem segment2 = myPane.AddPieSlice ( 40, Color.Salmon, 0, "South") ;
			PieItem segment3 = myPane.AddPieSlice ( 30, Color.Yellow,.0, "East") ;
			PieItem segment4 = myPane.AddPieSlice ( 10.21, Color.LimeGreen, 0, "West") ;
			PieItem segment5 = myPane.AddPieSlice ( 10.5, Color.Aquamarine, .3, "Canada") ;
			
			// Add some more slices as an array
			PieItem [] slices = new PieItem[values2.Length] ;
			slices = myPane.AddPieSlices ( values2, labels ) ;

			// Modify the slice label types
			((PieItem)slices[0]).LabelType = PieLabelType.Name_Value ;
			((PieItem)slices[1]).LabelType = PieLabelType.Name_Value_Percent ;
			((PieItem)slices[2]).LabelType = PieLabelType.Name_Value ;
			((PieItem)slices[3]).LabelType = PieLabelType.Name_Value ;
			((PieItem)slices[1]).Displacement = .2 ;
			segment1.LabelType = PieLabelType.Name_Percent ;
			segment2.LabelType = PieLabelType.Name_Value ;
			segment3.LabelType = PieLabelType.Percent ;
			segment4.LabelType = PieLabelType.Value ;
			segment5.LabelType = PieLabelType.Name_Value ;
			segment2.LabelDetail.FontSpec.FontColor = Color.Red ;
			
			// Sum up the values																					
			CurveList curves = myPane.CurveList ;
			double total = 0 ;
			for ( int x = 0 ; x <  curves.Count ; x++ )
				total += ((PieItem)curves[x]).Value ;

			// Add a text item to highlight total sales
			TextObj text = new TextObj("Total 2004 Sales - " + "$" + total.ToString () + "M", 0.85F, 0.80F,CoordType.PaneFraction );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Border.IsVisible = false ;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Center ;
			myPane.GraphObjList.Add( text );
			
			// Add a colored background behind the pie
			BoxObj box = new BoxObj( 0, 0, 1, 1, Color.Empty, Color.PeachPuff );
			box.Location.CoordinateFrame = CoordType.ChartFraction;
			box.Border.IsVisible = false;
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			box.ZOrder = ZOrder.E_BehindAxis;
			myPane.GraphObjList.Add( box );

			base.ZedGraphControl.AxisChange();
		}
	}
}
