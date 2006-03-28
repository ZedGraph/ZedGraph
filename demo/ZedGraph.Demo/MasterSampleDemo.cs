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
	public class MasterSampleDemo : DemoBase
	{

		public MasterSampleDemo() : base( "Code Project MasterPane Sample",
				"MasterPane Sample", DemoType.Tutorial )
		{
			MasterPane myMaster = base.MasterPane;

			// Remove the default GraphPane that comes with ZedGraphControl
			myMaster.PaneList.Clear();

			// Set the masterpane title
			myMaster.Title.Text = "ZedGraph MasterPane Example";
			myMaster.Title.IsVisible = true;

			// Fill the masterpane background with a color gradient
			myMaster.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );

			// Set the margins to 10 points
			myMaster.Margin.All = 10;

			// Enable the masterpane legend
			myMaster.Legend.IsVisible = true;
			myMaster.Legend.Position = LegendPos.TopCenter;

			// Add a priority stamp
			TextObj text = new TextObj( "Priority", 0.88F, 0.12F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;
			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;
			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			myMaster.GraphObjList.Add( text );

			// Add a draft watermark
			text = new TextObj( "DRAFT", 0.5F, 0.5F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;
			text.FontSpec.Angle = 30.0F;
			text.FontSpec.FontColor = Color.FromArgb( 70, 255, 100, 100 );
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 100;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Center;
			text.ZOrder = ZOrder.B_BehindLegend;
			myMaster.GraphObjList.Add( text );

			// Initialize a color and symbol type rotator
			ColorSymbolRotator rotator = new ColorSymbolRotator();

			// Create some new GraphPanes
			for ( int j=0; j<5; j++ )
			{
				// Create a new graph - rect dimensions do not matter here, since it
				// will be resized by MasterPane.AutoPaneLayout()
				GraphPane myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
					"Case #" + (j+1).ToString(),
					"Time, Days",
					"Rate, m/s" );

				// Fill the GraphPane background with a color gradient
				myPane.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPane.BaseDimension = 6.0F;

				// Make up some data arrays based on the Sine function
				PointPairList list = new PointPairList();
				for ( int i=0; i<36; i++ )
				{
					double x = (double) i + 5;
					double y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 + (double) j ) );
					list.Add( x, y );
				}

				// Add a curve to the Graph, use the next sequential color and symbol
				LineItem myCurve = myPane.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				// Fill the symbols with white to make them opaque
				myCurve.Symbol.Fill = new Fill( Color.White );

				// Add the GraphPane to the MasterPane
				myMaster.Add( myPane );
			}

			Graphics g = this.ZedGraphControl.CreateGraphics();
			// Tell ZedGraph to auto layout the new GraphPanes
			myMaster.PaneLayoutMgr.SetLayout( PaneLayout.ExplicitRow32 );
			myMaster.AxisChange( g );
			g.Dispose();
		}
	}
}


