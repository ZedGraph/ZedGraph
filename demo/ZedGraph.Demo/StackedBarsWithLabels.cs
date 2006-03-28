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
	public class StackedBarsWithLabelsDemo : DemoBase
	{
		public StackedBarsWithLabelsDemo() : base( "A stacked bar graph that includes a text label inside each bar",
			"Stacked Bars with Labels Demo", DemoType.Bar )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "Stacked Bars with Value Labels Inside Each Bar";
			myPane.XAxis.Title.Text = "Position Number";
			myPane.YAxis.Title.Text = "Some Random Thing";

			// Create points for three BarItems
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();

			// Use random data values
			Random rand = new Random();
			for ( int i=1; i<5; i++ )
			{
				double y = (double) i;
				double x1 = 100.0 + rand.NextDouble() * 100.0;
				double x2 = 100.0 + rand.NextDouble() * 100.0;
				double x3 = 100.0 + rand.NextDouble() * 100.0;

				list1.Add( x1, y );
				list2.Add( x2, y );
				list3.Add( x3, y );
			}

			// Create the three BarItems, change the fill properties so the angle is at 90
			// degrees for horizontal bars
			BarItem bar1 = myPane.AddBar( "Bar 1", list1, Color.Red );
			bar1.Bar.Fill = new Fill( Color.Red, Color.White, Color.Red, 90 );
			BarItem bar2 = myPane.AddBar( "Bar 2", list2, Color.Blue );
			bar2.Bar.Fill = new Fill( Color.Blue, Color.White, Color.Blue, 90 );
			BarItem bar3 = myPane.AddBar( "Bar 3", list3, Color.Green );
			bar3.Bar.Fill = new Fill( Color.Green, Color.White, Color.Green, 90 );

			// Set BarBase to the YAxis for horizontal bars
			myPane.BarSettings.Base = BarBase.Y;
			// Make the bars stack instead of cluster
			myPane.BarSettings.Type = BarType.Stack;

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White,
				Color.FromArgb( 255, 255, 166), 45.0F );

			base.ZedGraphControl.AxisChange();

			// Create TextObj's to provide labels for each bar
			CreateBarLabels( myPane, true, "N0" );
		}

		/// <summary>
		/// Create a TextLabel for each bar in the GraphPane.
		/// Call this method only after calling AxisChange()
		/// </summary>
		/// <remarks>
		/// This method will go through the bars, create a label that corresponds to the bar value,
		/// and place it on the graph depending on user preferences.  This works for horizontal or
		/// vertical bars in clusters or stacks.</remarks>
		/// <param name="pane">The GraphPane in which to place the text labels.</param>
		/// <param name="isBarCenter">true to center the labels inside the bars, false to
		/// place the labels just above the top of the bar.</param>
		/// <param name="valueFormat">The double.ToString string format to use for creating
		/// the labels
		/// </param>
		private void CreateBarLabels( GraphPane pane, bool isBarCenter, string valueFormat )
		{
			bool isVertical = pane.BarSettings.Base == BarBase.X;

			// Make the gap between the bars and the labels = 2% of the axis range
			float labelOffset;
			if ( isVertical )
				labelOffset = (float) ( pane.YAxis.Scale.Max - pane.YAxis.Scale.Min ) * 0.02f;
			else
				labelOffset = (float) ( pane.XAxis.Scale.Max - pane.XAxis.Scale.Min ) * 0.02f;

			// keep a count of the number of BarItems
			int curveIndex = 0;

			// Get a valuehandler to do some calculations for us
			ValueHandler valueHandler = new ValueHandler( pane, true );

			// Loop through each curve in the list
			foreach ( CurveItem curve in pane.CurveList )
			{
				// work with BarItems only
				BarItem bar = curve as BarItem;
				if ( bar != null )
				{
					IPointList points = curve.Points;

					// Loop through each point in the BarItem
					for ( int i=0; i<points.Count; i++ )
					{
						// Get the high, low and base values for the current bar
						// note that this method will automatically calculate the "effective"
						// values if the bar is stacked
						double baseVal, lowVal, hiVal;
						valueHandler.GetValues( curve, i, out baseVal, out lowVal, out hiVal );

						// Get the value that corresponds to the center of the bar base
						// This method figures out how the bars are positioned within a cluster
						float centerVal = (float) valueHandler.BarCenterValue( bar, 
							bar.GetBarWidth( pane ), i, baseVal, curveIndex );

						// Create a text label -- note that we have to go back to the original point
						// data for this, since hiVal and lowVal could be "effective" values from a bar stack
						string barLabelText	= ( isVertical ? points[i].Y : points[i].X ).ToString( valueFormat );

						// Calculate the position of the label -- this is either the X or the Y coordinate
						// depending on whether they are horizontal or vertical bars, respectively
						float position;
						if ( isBarCenter )
							position = (float) (hiVal + lowVal) / 2.0f;
						else
							position = (float) hiVal + labelOffset;

						// Create the new TextObj
						TextObj label;
						if ( isVertical )
							label = new TextObj( barLabelText, centerVal, position );
						else
							label = new TextObj( barLabelText, position, centerVal );

						// Configure the TextObj
						label.Location.CoordinateFrame	= CoordType.AxisXYScale;
						label.FontSpec.Size					= 12;
						label.FontSpec.FontColor			= Color.Black;
						label.FontSpec.Angle					= isVertical ? 90 : 0;
						label.Location.AlignH				= isBarCenter ? AlignH.Center : AlignH.Left;
						label.Location.AlignV				= AlignV.Center;
						label.FontSpec.Border.IsVisible	= false;
						label.FontSpec.Fill.IsVisible		= false;

						// Add the TextObj to the GraphPane
						pane.GraphObjList.Add( label );
					}
				}
				curveIndex++;
			}
		}
	}
}
