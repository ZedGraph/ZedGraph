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
			base.MasterPane.Title = "Multiple Pie Charts on a MasterPane";
			base.MasterPane.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			base.MasterPane.MarginAll = 10;
			base.MasterPane.InnerPaneGap = 10;
			
			// Create a new graph with topLeft at (40,40) and size 600x400

			double [] values =   { 15, 15, 40, 20 } ;
			double [] values2 =   { 250, 50, 400, 50 } ;
			Color [] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow } ;
			double [] displacement = {	.0,.0,.0,.0 } ;
			string [] labels = { "East", "West", "Central", "Canada" } ;
			
			Graphics g = this.ZedGraphControl.CreateGraphics();
			base.MasterPane.PaneList.Remove( 0 );
			
			for (int x = 0 ; x < 3 ; x++ )
			{
				GraphPane myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"2003 Regional Sales", "", "" );
				myPane.PaneFill = new Fill( Color.Cornsilk );
				myPane.AxisFill = new Fill( Color.Cornsilk );
				myPane.Legend.Position = LegendPos.Right ;
				myPane.Legend.IsVisible = false	 ;
				PieItem segment1 = myPane.AddPieSlice ( 20, Color.Blue, .10, "North") ;
				PieItem segment2 = myPane.AddPieSlice ( 40, Color.Red, 0, "South") ;
				PieItem segment3 = myPane.AddPieSlice ( 30, Color.Yellow,.0, "East") ;
				PieItem segment4 = myPane.AddPieSlice ( 10.21, Color.Green, .20, "West") ;
				PieItem segment5 = myPane.AddPieSlice ( 10.5, Color.Aquamarine, .0, "Canada") ;
				segment1.LabelType = PieLabelType.Name_Value ;
				segment2.LabelType = PieLabelType.Name_Value ;
				segment3.LabelType = PieLabelType.Name_Value ;
				segment4.LabelType = PieLabelType.Name_Value ;
				segment5.LabelType = PieLabelType.Name_Value ;

				base.MasterPane.Add( myPane );
			}
			
			base.MasterPane.AutoPaneLayout( g, PaneLayout.ExplicitRow12 );

			base.ZedGraphControl.AxisChange();
			
			g.Dispose();
		}
	}
}
