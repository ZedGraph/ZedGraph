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
using System.Resources;
using System.Reflection;
using System.IO;

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

			// Set the title and axis labels
			myPane.XAxis.Title.Text = "Time, Years";
			myPane.YAxis.Title.Text = "Rainfall, mm/yr";
			
			// Enter some data points
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };

			// Make a bar
			BarItem bar = myPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			// Fill the bar with a color gradient
			bar.Bar.Fill = new Fill( Color.FromArgb( 100, 130, 255, 130 ), Color.FromArgb( 100, 255, 255, 255 ),
				Color.FromArgb( 100, 130, 255, 130 ) );

			// Make a second bar
			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			// Fill the bar with a color gradient
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );

			// Get an image for the background
			//ResourceManager resourceManager = new ResourceManager( "ZedGraph.Demo",
			//				Assembly.GetExecutingAssembly() );

			Assembly a = Assembly.GetExecutingAssembly();
			string[] resNames = a.GetManifestResourceNames();

			Stream imgStream = a.GetManifestResourceStream( "ZedGraph.Demo.Resources.ngc4414.jpg" );

			Image image = Bitmap.FromStream( imgStream ) as Bitmap;

			//Image image = Resources.ngc4414;

			//Image image = Bitmap.FromStream(
			//	GetType().Assembly.GetManifestResourceStream( "ZedGraph.Demo.ngc4414.jpg" ) );
	
			// Fill the pane background with the image
			TextureBrush texBrush = new TextureBrush( image );
			myPane.Fill = new Fill( texBrush );

			// Turn off the axis background fill
			myPane.Chart.Fill.IsVisible = false;
			// Hide the legend
			myPane.Legend.IsVisible = false;
			
			// Add a text label
			TextObj text = new TextObj( "Desert Rainfall", 0.5F, -0.05F, CoordType.ChartFraction,
				AlignH.Center, AlignV.Bottom );
			text.FontSpec.FontColor = Color.Black;
			text.FontSpec.Size = 20F;
			text.FontSpec.IsBold = true;
			text.FontSpec.IsItalic = true;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			myPane.GraphObjList.Add( text );

			// Hide the title
			myPane.Title.IsVisible = false;

			// Set the colors to white show it shows up on a dark background
			myPane.XAxis.Color = Color.White;
			myPane.YAxis.Color = Color.White;
			myPane.XAxis.Scale.FontSpec.FontColor = Color.White;
			myPane.XAxis.Title.FontSpec.FontColor = Color.White;
			myPane.YAxis.Scale.FontSpec.FontColor = Color.White;
			myPane.YAxis.Title.FontSpec.FontColor = Color.White;
			myPane.Chart.Border.Color = Color.White;
			myPane.XAxis.MajorGrid.Color = Color.White;
			myPane.YAxis.MajorGrid.Color = Color.White;
			
			// Show the grid lines
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.Scale.Max = 120;

			// Set the cluster with to 100 user units
			// this is necessary since the scale is not an ordinal type
			myPane.BarSettings.ClusterScaleWidth = 100;

			// Make it a stacked bar
			myPane.BarSettings.Type = BarType.Stack;

			base.ZedGraphControl.AxisChange();
		}
	}
}
