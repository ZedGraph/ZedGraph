//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2006 John Champion and Jerry Vos
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
	public class SynchronizedPanes : DemoBase
	{

		public SynchronizedPanes()
			: base( "A demo that shows how multiple GraphPanes can be synchronized to scroll together." +
						"  Test it by zooming in on one pane (all panes will zoom together).  Then, scroll to" +
						" see all panes move together.",
						"Synchronized Panes Demo", DemoType.Line )
		{
			MasterPane master = base.MasterPane;

			master.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45.0f );
			master.PaneList.Clear();

			master.Title.IsVisible = true;
			master.Title.Text = "Synchronized Graph Demo";

			master.Margin.All = 10;
			master.InnerPaneGap = 0;

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 3; j++ )
			{
				// Create a new graph with topLeft at (40,40) and size 600x400
				GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"Case #" + ( j + 1 ).ToString(),
					"Time, Days",
					"Rate, m/s" );

				myPaneT.Fill.IsVisible = false;

				myPaneT.Chart.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPaneT.BaseDimension = 3.0F;
				myPaneT.XAxis.Title.IsVisible = false;
				myPaneT.XAxis.Scale.IsVisible = false;
				myPaneT.Legend.IsVisible = false;
				myPaneT.Border.IsVisible = false;
				myPaneT.Title.IsVisible = false;
				myPaneT.XAxis.MajorTic.IsOutside = false;
				myPaneT.XAxis.MinorTic.IsOutside = false;
				myPaneT.XAxis.MajorGrid.IsVisible = true;
				myPaneT.XAxis.MinorGrid.IsVisible = true;
				myPaneT.Margin.All = 0;
				if ( j == 0 )
					myPaneT.Margin.Top = 20;
				if ( j == 2 )
				{
					myPaneT.XAxis.Title.IsVisible = true;
					myPaneT.XAxis.Scale.IsVisible = true;
					myPaneT.Margin.Bottom = 10;
				}

				if ( j > 0 )
					myPaneT.YAxis.Scale.IsSkipLastLabel = true;

				// This sets the minimum amount of space for the left and right side, respectively
				// The reason for this is so that the ChartRect's all end up being the same size.
				myPaneT.YAxis.MinSpace = 60;
				myPaneT.Y2Axis.MinSpace = 20;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i = 0; i < 36; i++ )
				{
					x = (double)i + 5 + j * 3;
					y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
					list.Add( x, y );
				}

				LineItem myCurve = myPaneT.AddCurve( "Type " + j.ToString(),
					list, rotator.NextColor, rotator.NextSymbol );
				myCurve.Symbol.Fill = new Fill( Color.White );

				master.Add( myPaneT );
			}

			using ( Graphics g = base.ZedGraphControl.CreateGraphics() )
			{
				ZedGraphControl z1 = base.ZedGraphControl;

				master.SetLayout( PaneLayout.SingleColumn );
				z1.AxisChange();

				z1.IsAutoScrollRange = true;
				z1.IsShowHScrollBar = true;
				z1.IsShowVScrollBar = true;
				z1.IsSynchronizeXAxes = true;

				g.Dispose();
			}

			base.ZedGraphControl.AxisChange();
		}
	}
}
