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
										"Pie Chart Demo 1", DemoType.Pie )
		{
			base.GraphPane.Title = "2003 Regional Sales";

			double [] values =   { 15, 15, 40, 20 } ;
			double [] values2 =   { 250, 50, 400, 50 } ;
			Color [] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow } ;
			double [] displacement = {	.0,.0,.0,.0 } ;
			string [] labels = { "East", "West", "Central", "Canada" } ;

			base.GraphPane.PaneFill = new Fill( Color.Cornsilk );
			base.GraphPane.AxisFill = new Fill( Color.Cornsilk );
			base.GraphPane.Legend.Position = LegendPos.Right ;
			
			PieItem segment1 = base.GraphPane.AddPieSlice ( 20, Color.Blue, .20, "North") ;
			PieItem segment2 = base.GraphPane.AddPieSlice ( 40, Color.Red, 0, "South") ;
			PieItem segment3 = base.GraphPane.AddPieSlice ( 30, Color.Yellow,.0, "East") ;
			PieItem segment4 = base.GraphPane.AddPieSlice ( 10.21, Color.Green, 0, "West") ;
			PieItem segment5 = base.GraphPane.AddPieSlice ( 10.5, Color.Aquamarine, .1, "Canada") ;
			//			 PieItem segment5 = new PieItem(myPane, 20, Color.Aquamarine, 0, "Canada" ) ;
			//			segment5.IsVisible = false ;
					
			
			PieItem [] slices = new PieItem[values2.Length] ;
			slices = base.GraphPane.AddPieSlices ( values2, labels ) ;
			((PieItem)slices[3]).LabelType = PieLabelType.None ;
			((PieItem)slices[1]).Displacement = .1 ;
			((PieItem)slices[1]).LabelType = PieLabelType.Name ;

			/*
						foreach (PieItem segment in myPane.CurveList)
						{
							segment.Displacement = .1 ;			
						}
			*/


			/*
						int x = 0 ;
						foreach (PieItem segment in myPane.CurveList)
						{
							segment.Value = values[x] ;
							x++ ;
						}
			*/

			//			myPane.IsAxisRectAuto = false ;

			segment1.LabelType = PieLabelType.Name_Percent ;
			segment2.LabelType = PieLabelType.Name_Value ;
			segment3.LabelType = PieLabelType.Percent ;
			segment4.LabelType = PieLabelType.Value ;
			segment5.LabelType = PieLabelType.Name_Value ;
			segment2.LabelDetail.FontSpec.FontColor = Color.Red ;
																																				
			TextItem text = new TextItem("First Prod 21-Oct-93", 0.85F, 0.80F,CoordType.PaneFraction );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Border.IsVisible = false ;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Center ;
			base.GraphPane.GraphItemList.Add( text );


			/*
						PieItem myPie1 = myPane.AddPie ( "2002", values, colors, displacement, labels ) ;
						myPie1.AddSlice	( values[0], Color.Coral, displacement[2], "SE" );
						myPie1.AddSlice	( 10, Color.Coral, 0 , "x" );
						myPie1.AddSlice	( 10, Color.Coral, 0 , "x" );
						myPie1.AddSlice	( 10, Color.Coral, 0 , "x" );
						myPie1.AddSlice	( 10, Color.Coral, 0 , "x" );
						myPie1.AddSlice	( 10, Color.Coral, 0 , "x" );
						myPie1.AddSlice	( 10, Color.Coral, 0 , "x" );
						((PieSlice)myPie1.SliceList[3]).Border.Color = Color.Blue ;
						((PieSlice)myPie1.SliceList[3]).Border.PenWidth = 2 ;
						((PieSlice)myPie1.SliceList[2]).Fill = new Fill( Color.Red, Color.White, 45F );
			
						myPie1.IsVisible = false;
						myPie1.Label = "JUnk";
						myPie1.Color = Color.Red;
			*/
			//MessageBox.Show( "Start" );
			//for ( int i=0; i<10000000; i++ )
			//	myPie1.RecalculateSliceAngles();
			//MessageBox.Show( "Done" );

			//			((PieSlice)myPie1.SliceList[1]).Border.IsVisible = false ;
			//			((PieSlice)myPie1.SliceList[4]).IsVisible = false ;
         
			//generate defaults
			//				PieItem myPie3 = myPane.AddPie ( "2002", null, null, null, null) ;
			//generate defaults except for values
			//				PieItem myPie2 = myPane.AddPie ( "2003", values) ;


																																						

			 
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
