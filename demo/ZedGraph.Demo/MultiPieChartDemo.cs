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
	public class MultiPieChartDemo : DemoBase
	{

		public MultiPieChartDemo() : base( "A Demo of the MasterPane with Pie Charts",
			"Multi-Pie Chart Demo", DemoType.General, DemoType.Special )
		{
			MasterPane myMaster = base.MasterPane;
			// Remove the default GraphPane that comes with ZedGraphControl
			myMaster.PaneList.Clear();

			// Set the master pane title
			myMaster.Title.Text = "Multiple Pie Charts on a MasterPane";
			myMaster.Title.IsVisible = true ;

			// Fill the masterpane background with a color gradient
			myMaster.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );

			// Set the margins and the space between panes to 10 points
			myMaster.Margin.All = 10;
			myMaster.InnerPaneGap = 10;

			// Enable the masterpane legend
			myMaster.Legend.IsVisible = true ;
			myMaster.Legend.Position = LegendPos.TopCenter;
			myMaster.IsUniformLegendEntries = true;

			// Enter some data values
			double [] values = { 15, 15, 40, 20 } ;
			double [] values2 = { 250, 50, 400, 50 } ;
			Color [] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow } ;
			double [] displacement = { .0,.0,.0,.0 } ;
			string [] labels = { "East", "West", "Central", "Canada" } ;
			
			// Create some GraphPanes
			for ( int x=0; x<3; x++ )
			{
				// Create the GraphPane
				GraphPane myPane = new GraphPane();
				myPane.Title.Text = "2003 Regional Sales";

				// Fill the pane background with a solid color
				myPane.Fill = new Fill( Color.Cornsilk );
				// Fill the axis background with a solid color
				myPane.Chart.Fill = new Fill( Color.Cornsilk );

				// Hide the GraphPane legend
				myPane.Legend.IsVisible = false	 ;

				// Add some pie slices
				PieItem segment1 = myPane.AddPieSlice( 20, Color.Blue, .10, "North" );
				PieItem segment2 = myPane.AddPieSlice( 40, Color.Red, 0, "South" );
				PieItem segment3 = myPane.AddPieSlice( 30, Color.Yellow, .0, "East" );
				PieItem segment4 = myPane.AddPieSlice( 10.21, Color.Green, .20, "West" );
				PieItem segment5 = myPane.AddPieSlice( 10.5, Color.Aquamarine, .0, "Canada" );
				segment1.LabelType = PieLabelType.Name_Value;
				segment2.LabelType = PieLabelType.Name_Value;
				segment3.LabelType = PieLabelType.Name_Value;
				segment4.LabelType = PieLabelType.Name_Value;
				segment5.LabelType = PieLabelType.Name_Value;

				// Add the graphpane to the masterpane
				myMaster.Add( myPane );
			}
			
			// Tell ZedGraph to auto layout the graphpanes
			Graphics g = this.ZedGraphControl.CreateGraphics();
			myMaster.AutoPaneLayout( g, PaneLayout.ExplicitRow12 );
			myMaster.AxisChange( g );
			g.Dispose();

		}
	}
}
