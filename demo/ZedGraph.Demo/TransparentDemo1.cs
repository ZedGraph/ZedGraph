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
using System.Collections;
using System.Drawing;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for TransparentDemo1.
	/// </summary>
	public class TransparentDemo1 : DemoBase
	{
		public TransparentDemo1() : base( "A transparency demo", "Transparency Demo 1", DemoType.Special )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title = "Desert Rainfall";
			myPane.XAxis.Title = "Time, Years";
			myPane.YAxis.Title = "Rainfall, mm/yr";
			
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = myPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );

			bar.Bar.Fill = new Fill( Color.FromArgb( 100, 130, 255, 130 ), Color.FromArgb( 100, 255, 255, 255 ),
				Color.FromArgb( 100, 130, 255, 130 ) );
			myPane.ClusterScaleWidth = 100;
			myPane.BarType = BarType.Stack;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			myPane.ClusterScaleWidth = 100;

			Image image = 
				Bitmap.FromStream(
					GetType().Assembly.GetManifestResourceStream("ZedGraph.Demo.ngc4414.jpg"));
	
			TextureBrush texBrush = new TextureBrush( image );
			myPane.PaneFill = new Fill( texBrush );

			myPane.AxisFill.IsVisible = false;
			myPane.Legend.IsVisible = false;
			
			TextItem text = new TextItem( "Desert Rainfall", 0.5F, -0.05F, CoordType.AxisFraction,
				AlignH.Center, AlignV.Bottom );
			text.FontSpec.FontColor = Color.Black;
			text.FontSpec.Size = 20F;
			text.FontSpec.IsBold = true;
			text.FontSpec.IsItalic = true;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			myPane.GraphItemList.Add( text );
			myPane.IsShowTitle = false;
			myPane.FontSpec.FontColor = Color.Black;
			myPane.XAxis.Color = Color.White;
			myPane.YAxis.Color = Color.White;
			myPane.XAxis.ScaleFontSpec.FontColor = Color.White;
			myPane.XAxis.TitleFontSpec.FontColor = Color.White;
			myPane.YAxis.ScaleFontSpec.FontColor = Color.White;
			myPane.YAxis.TitleFontSpec.FontColor = Color.White;
			myPane.AxisBorder.Color = Color.White;
			myPane.XAxis.GridColor = Color.White;
			myPane.YAxis.GridColor = Color.White;
			
			myPane.XAxis.IsShowGrid = true;

			myPane.YAxis.IsShowGrid = true;
			myPane.YAxis.Max = 120;

			base.ZedGraphControl.AxisChange();
		}
	}
}
