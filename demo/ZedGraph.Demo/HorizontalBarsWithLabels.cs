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
	/// Summary description for BarGraphBandDemo.
	/// </summary>
	public class HorizontalBarsWithLabelsDemo : DemoBase
	{
		public HorizontalBarsWithLabelsDemo() : base( "A bar graph that includes a text label next to each bar",
			"Horizontal Bars with Labels Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title = "Horizontal Bars with Value Labels Above Each Bar";
			myPane.XAxis.Title = "Some Random Thing";
			myPane.YAxis.Title = "Position Number";

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			Random rand = new Random();

			for ( int i=0; i<4; i++ )
			{
				double y = (double) i+5;
				double x = rand.NextDouble() * 1000;
				double x2 = rand.NextDouble() * 1000;
				double x3 = rand.NextDouble() * 1000;
				list.Add( x, y );
				list2.Add( x2, y );
				list3.Add( x3, y );
			}

			BarItem myCurve = myPane.AddBar( "curve 1", list, Color.Blue );
			myCurve.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90 );
			BarItem myCurve2 = myPane.AddBar( "curve 2", list2, Color.Red );
			myCurve2.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90 );
			BarItem myCurve3 = myPane.AddBar( "curve 3", list3, Color.Green );
			myCurve3.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90 );

			myPane.BarBase = BarBase.Y;

			// Fill the axis background with a color gradient
			myPane.AxisFill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );

			base.ZedGraphControl.AxisChange();

			CreateBarLabels( myPane, false, "N0" );
		}

		// Call this method after calling AxisChange()
		private void CreateBarLabels( GraphPane pane, bool isBarCenter, string valueFormat )
		{
			bool isVertical = pane.BarBase == BarBase.X;

			// Make the gap between the bars and the labels = 2% of the axis range
			float labelOffset;
			
			if ( isVertical )
				labelOffset = (float) ( pane.YAxis.Max - pane.YAxis.Min ) * 0.02f;
			else
				labelOffset = (float) ( pane.XAxis.Max - pane.XAxis.Min ) * 0.02f;


			foreach ( CurveItem curve in pane.CurveList )
			{
				BarItem bar = curve as BarItem;

				if ( bar != null )
				{
					IPointList points = curve.Points;

					for ( int i=0; i<points.Count; i++ )
					{
						ValueHandler valueHandler = new ValueHandler( pane, true );

						int curveIndex = pane.CurveList.IndexOf( curve );

						double baseVal, lowVal, hiVal;
						valueHandler.GetValues( curve, i, out baseVal, out lowVal, out hiVal );

						float centerVal = (float) valueHandler.BarCenterValue( bar, 
							bar.GetBarWidth( pane ), i, baseVal, curveIndex );

						string barLabelText	= ( isVertical ? points[i].Y : points[i].X ).ToString( valueFormat );

						float position;
						if ( isBarCenter )
							position = (float) (hiVal + lowVal) / 2.0f;
						else
							position = (float) hiVal + labelOffset;

						TextItem label;
						
						if ( isVertical )
							label = new TextItem( barLabelText, centerVal, position );
						else
							label = new TextItem( barLabelText, position, centerVal );

						label.Location.CoordinateFrame	= CoordType.AxisXYScale;
						label.FontSpec.Size					= 12;
						label.FontSpec.FontColor			= Color.Black;
						label.FontSpec.Angle					= isVertical ? 90 : 0;
						label.Location.AlignH				= isBarCenter ? AlignH.Center : AlignH.Left;
						label.Location.AlignV				= AlignV.Center;
						label.FontSpec.Border.IsVisible	= false;
						label.FontSpec.Fill.IsVisible		= false;

						pane.GraphItemList.Add( label );
					}
				}
			}
		}
	}
}
