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
using System.Drawing;
using System.Collections;
using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class MasterPaneDemo : DemoBase
	{

		public MasterPaneDemo() : base( "A Demo of the MasterPane",
									"MasterPane Demo", DemoType.General, DemoType.Special )
		{
			MasterPane myMaster = base.MasterPane;

			// Remove the default pane that comes with the ZedGraphControl.MasterPane
			myMaster.PaneList.Clear();
			
			// Set the master pane title
			myMaster.Title = "MasterPane Test";
			myMaster.IsShowTitle = true;

			// Fill the pane background with a color gradient
			myMaster.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			
			// Set the margins and the space between panes to 10 points
			myMaster.MarginAll = 10;
			myMaster.InnerPaneGap = 10;

			// Enable the master pane legend
			myMaster.Legend.IsVisible = true;
			myMaster.Legend.Position = LegendPos.TopCenter;

			// Add a confidential stamp
			TextItem text = new TextItem( "Confidential", 0.80F, 0.12F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;
			// angle the text at 15 degrees
			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;
			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			myMaster.GraphItemList.Add( text );

			// Add a draft watermark
			text = new TextItem("DRAFT", 0.5F, 0.5F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;
			// tilt the text at 30 degrees
			text.FontSpec.Angle = 30.0F;
			// Use the alpha value (70) to make the text semi-transparent (a watermark)
			text.FontSpec.FontColor = Color.FromArgb( 70, 255, 100, 100 );
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 100;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Center;
			text.ZOrder = ZOrder.A_InFront;
			myMaster.GraphItemList.Add( text );

			for ( int j=0; j<6; j++ )
			{
				// Create a new GraphPane
				GraphPane myPane = new GraphPane();
				myPane.Title = "My Test Graph #" + (j+1).ToString();
				myPane.XAxis.Title = "X Axis";
				myPane.YAxis.Title = "Y Axis";

				// Fill the pane background with a color gradient
				myPane.PaneFill = new Fill( Color.White, Color.LightYellow, 45.0F );
				// Reduce the base dimension to 6 inches, since these panes tend to be smaller
				myPane.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				PointPairList list = new PointPairList();
				for ( int i=0; i<36; i++ )
				{
					double x = (double) i + 5;
					double y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 + (double) j ) );
					list.Add( x, y );
				}

				// Generate a red curve with diamond symbols
				LineItem myCurve = myPane.AddCurve( "label" + j.ToString(),
					list, Color.Red, SymbolType.Diamond );

				// Add the new GraphPane to the MasterPane
				myMaster.Add( myPane );
			}

			// Tell ZedGraph to auto layout all the panes
			Graphics g = base.ZedGraphControl.CreateGraphics();
			myMaster.AutoPaneLayout( g, PaneLayout.SquareColPreferred );
			myMaster.AxisChange( g );
			g.Dispose();

		}
	}
}
