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
			string [] quarters = {"Q1-'04", "Q2-'04", "Q3-'04", "Q4-'04" } ;          
			double[] y4 = { 20, 15, 90, 70 };      
			double[] y3 = { 0, 35, 40,10 };             
			double[] y2 = { 60, 70, 20,30 };               
			double[] y5 = new double [4] ;

			base.GraphPane.Title = "% Product Sales by Quarter by Type";
			base.GraphPane.Legend.Position = LegendPos.TopCenter ;
			base.GraphPane.Legend.Fill.Color = Color.LightCyan ;
			base.GraphPane.PaneFill.Color = Color.Cornsilk ;
			base.GraphPane.AxisFill.Type = FillType.Solid ;
			base.GraphPane.AxisFill.Color = Color.LightCyan ;
			base.GraphPane.BarBase=BarBase.X ;
			base.GraphPane.BarType = BarType.PercentStack ;

			base.GraphPane.XAxis.Title = "Quarter";
			base.GraphPane.XAxis.Type = AxisType.Text ;
			base.GraphPane.XAxis.TextLabels=quarters ;   
      
			base.GraphPane.Y2Axis.Title = "2004 Total Sales ($M)"  ;
			base.GraphPane.Y2Axis.IsVisible = true;
			base.GraphPane.Y2Axis.IsMinorOppositeTic = false;
			base.GraphPane.Y2Axis.IsOppositeTic = false;
			base.GraphPane.Y2Axis.ScaleFontSpec.FontColor = Color.Red ;

			base.GraphPane.YAxis.Title = "" ;
			base.GraphPane.YAxis.Max = 120 ;
			base.GraphPane.YAxis.IsMinorOppositeTic = false;
			base.GraphPane.YAxis.IsOppositeTic = false;
							 //get total values into array for Line
			for ( int x = 0 ; x < 4 ; x++ )
				y5[x] = y4[x] + y3[x] + y2[x] ;

			LineItem curve;
			curve = base.GraphPane.AddCurve( "Total Sales", null, y5, Color.Black, SymbolType.Circle );
			curve.IsY2Axis = true ;
			curve.Line.Width = 1.5F;
			curve.Line.Color = Color.Red ;
			curve.Symbol.Fill = new Fill( Color.Red );
			base.GraphPane.Y2Axis.TitleFontSpec.FontColor = Color.Red ;
			curve.Symbol.Size = 8;

			//           vertical stacked bars
			BarItem bar = base.GraphPane.AddBar( "Components", null, y4, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );

			bar = base.GraphPane.AddBar( "Misc",  null, y3, Color.LimeGreen );
			bar.Bar.Fill = new Fill( Color.LimeGreen, Color.White, Color.LimeGreen );

			bar = base.GraphPane.AddBar( "Assemblies",null, y2, Color.Yellow );
			bar.Bar.Fill = new Fill( Color.Yellow, Color.White, Color.Yellow );
		}
	}	
}
