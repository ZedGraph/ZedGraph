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
			base.GraphPane.Title = "2004 ZedGraph Sales by Region\n ($M)";

			double [] values =   { 15, 15, 40, 20 } ;
			double [] values2 =   { 250, 50, 400, 50 } ;
			Color [] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow } ;
			double [] displacement = {	.0,.0,.0,.0 } ;
			string [] labels = { "Europe", "Pac Rim", "South America", "Africa" } ;

			base.GraphPane.PaneFill = new Fill( Color.Cornsilk );
			base.GraphPane.AxisFill = new Fill( Color.Cornsilk );
			base.GraphPane.Legend.Position = LegendPos.Right ;
			
			PieItem segment1 = base.GraphPane.AddPieSlice ( 20, Color.Navy, .20, "North") ;
			PieItem segment2 = base.GraphPane.AddPieSlice ( 40, Color.Salmon, 0, "South") ;
			PieItem segment3 = base.GraphPane.AddPieSlice ( 30, Color.Yellow,.0, "East") ;
			PieItem segment4 = base.GraphPane.AddPieSlice ( 10.21, Color.LimeGreen, 0, "West") ;
			PieItem segment5 = base.GraphPane.AddPieSlice ( 10.5, Color.Aquamarine, .3, "Canada") ;
			
			PieItem [] slices = new PieItem[values2.Length] ;
			slices = base.GraphPane.AddPieSlices ( values2, labels ) ;
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
																																				
			CurveList curves = base.GraphPane.CurveList ;
			double total = 0 ;
			for ( int x = 0 ; x <  curves.Count ; x++ )
				total += ((PieItem)curves[x]).Value ;

			TextItem text = new TextItem("Total 2004 Sales - " + "$" + total.ToString () + "M", 0.85F, 0.80F,CoordType.PaneFraction );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Border.IsVisible = false ;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Center ;
			base.GraphPane.GraphItemList.Add( text );
			 
			BoxItem box = new BoxItem( new RectangleF( 0F, 0F, 1F, 1F ),
				Color.Empty, Color.PeachPuff );
			box.Location.CoordinateFrame = CoordType.AxisFraction;
			box.Border.IsVisible = false;
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			box.ZOrder = ZOrder.E_BehindAxis;
			base.GraphPane.GraphItemList.Add( box );
		}
	}
}
