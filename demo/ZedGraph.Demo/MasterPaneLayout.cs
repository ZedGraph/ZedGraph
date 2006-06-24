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
	public class MasterPaneLayout : DemoBase
	{

		public MasterPaneLayout()
			: base( "A Demo of the MasterPane with a complex layout",
									"MasterPane Layout", DemoType.General, DemoType.Special )
		{
			MasterPane master = base.MasterPane;

			master.Fill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			master.PaneList.Clear();

			master.Title.IsVisible = true;
			master.Title.Text = "My MasterPane Title";

			master.Margin.All = 10;
			//master.InnerPaneGap = 10;
			//master.Legend.IsVisible = true;
			//master.Legend.Position = LegendPos.TopCenter;

			ColorSymbolRotator rotator = new ColorSymbolRotator();

			for ( int j = 0; j < 6; j++ )
			{
				master.Add( AddGraph( j, rotator ) );
			}

			using ( Graphics g = base.ZedGraphControl.CreateGraphics() )
			{

				//master.PaneLayoutMgr.SetLayout( PaneLayout.ExplicitRow32 );
				//master.PaneLayoutMgr.SetLayout( 2, 4 );
				master.SetLayout( false, new int[] { 1, 3, 2 }, new float[] { 2, 1, 3 } );
				master.IsCommonScaleFactor = true;
				base.ZedGraphControl.AxisChange();

				//g.Dispose();
			}

		}

		public GraphPane AddGraph( int j, ColorSymbolRotator rotator )
		{
			// Create a new graph with topLeft at (40,40) and size 600x400
			GraphPane myPaneT = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
				"Case #" + ( j + 1 ).ToString(),
				"Time, Days",
				"Rate, m/s" );

			myPaneT.Fill = new Fill( Color.White, Color.LightYellow, 45.0F );
			myPaneT.BaseDimension = 6.0F;

			// Make up some data arrays based on the Sine function
			double x, y;
			PointPairList list = new PointPairList();
			for ( int i = 0; i < 36; i++ )
			{
				x = (double)i + 5;
				y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.2 + (double)j ) );
				list.Add( x, y );
			}

			LineItem myCurve = myPaneT.AddCurve( "Type " + j.ToString(),
				list, rotator.NextColor, rotator.NextSymbol );
			myCurve.Symbol.Fill = new Fill( Color.White );

			return myPaneT;
		}
	}
}
