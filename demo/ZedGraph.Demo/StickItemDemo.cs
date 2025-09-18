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
	/// Summary description for StickItemDemo.
	/// </summary>
	public class StickItemDemo : DemoBase
	{

		public StickItemDemo() : base( "A demonstration of the 'StickItem', which is a single line for " +
			"each point running from the XAxis to the data value",
			"Stick Item Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "StickItem Demo Chart";
			myPane.XAxis.Title.Text = "X Label";
			myPane.YAxis.Title.Text = "My Y Axis";
			
			PointPairList list = new PointPairList();
			for ( int i=0; i<100; i++ )
			{
				double x = (double) i;
				double y = Math.Sin( i / 8.0 );
				double z = Math.Abs(Math.Cos( i / 8.0 )) * y;
				list.Add( x, y, z );
			}

			StickItem myCurve = myPane.AddStick( "Some Sticks", list, Color.Blue );
			myCurve.Line.Width = 2.0f;
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.XAxis.Scale.Max = 100;
			
			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.LightGoldenrodYellow, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
