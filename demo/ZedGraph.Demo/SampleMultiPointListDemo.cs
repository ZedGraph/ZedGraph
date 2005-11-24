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
	public class SampleMultiPointListDemo : DemoBase
	{

		public SampleMultiPointListDemo() : base( "A demonstration that uses the SampleMultiPointList " +
			"class, which is a custom class that demonstrates the implementation of the IPointList " +
			"interface.  This allows you to store your data in a custom structure, class, database, etc., " +
			"and to use this data directly within ZedGraph.  In this particular demo, that data are stored " +
			"in memory only once, but they are used for multiple curves.",
			"SampleMultiPointList (IPointList) Demo", DemoType.Line, DemoType.Special )
		{
			GraphPane myPane = base.GraphPane;

			// Set the titles and axis labels
			myPane.Title = "SampleMultiPointList (IPointList) Demo";
			myPane.XAxis.Title = "Time, seconds";
			myPane.YAxis.Title = "Distance (m), or Velocity (m/s)";
			
			SampleMultiPointList myList = new SampleMultiPointList();
			myList.YData = PerfDataType.Distance;

			// note how it does not matter that we created the second list before actually
			// adding the data -- this is because the cloned list shares data with the
			// original
			SampleMultiPointList myList2 = new SampleMultiPointList( myList );
			myList2.YData = PerfDataType.Velocity;

			for ( int i=0; i<20; i++ )
			{
				double time = (double) i;
				double acceleration = 1.0;
				double velocity = acceleration * time;
				double distance = acceleration * time * time / 2.0;
				PerformanceData perfData = new PerformanceData( time, distance, velocity, acceleration );
				myList.Add( perfData );
			}

			myPane.AddCurve( "Distance", myList, Color.Blue );
			myPane.AddCurve( "Velocity", myList2, Color.Red );
			
			// Fill the axis background with a color gradient
			myPane.AxisFill = new Fill( Color.White,
				Color.LightGoldenrodYellow, 45.0F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
