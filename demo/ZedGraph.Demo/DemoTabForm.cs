//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005 Jerry Vos
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

namespace ZedGraph.Demo
{
	/// <summary>
	/// An implementation of ChartTabForm that loads up ZedGraph demos.
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 1.4 $ $Date: 2005-03-03 06:13:59 $ </version>
	public class DemoTabForm : ChartTabForm
	{
	#region Constructor
		public DemoTabForm()
		{
		}
	#endregion

	#region Methods
		protected override void loadDemos()
		{
			loadDemo(new SimpleDemo());
			loadDemo(new PieChartDemo());
			loadDemo(new LineStackingDemo());
			loadDemo(new TransparentDemo1());
			loadDemo(new LineGraphBandDemo());
			loadDemo(new FilledBarGraphDemo());
			loadDemo(new SineBarGraphDemo());
			loadDemo(new MasterPaneDemo());
			loadDemo(new MultiPieChartDemo());
			loadDemo(new GradientByValueDemo());
			loadDemo(new HiLowCloseDemo());
			loadDemo(new HiLowBarDemo());
			loadDemo(new HorizontalBarDemo());
			loadDemo(new DualYDemo());
			loadDemo(new FilledCurveDemo());
			loadDemo(new ErrorBarDemo());
			loadDemo(new OverlayBarDemo());
			loadDemo(new SortedOverlayBarDemo());
			loadDemo(new CrossLineDemo());
			loadDemo(new StepChartDemo());
			loadDemo(new SmoothChartDemo());
			loadDemo(new BaseTicDemo());
			
			loadDemo(new InitialSampleDemo());
			loadDemo(new ModInitialSampleDemo());
			loadDemo(new DateAxisSampleDemo());
			loadDemo(new TextAxisSampleDemo());
			loadDemo(new BarChartSampleDemo());
			loadDemo(new HorizontalBarSampleDemo());
			loadDemo(new StackedBarSampleDemo());
			loadDemo(new PercentStkBarDemo());
			loadDemo(new PieSampleDemo());
			loadDemo(new MasterSampleDemo());
			
			// TODO: add more demos here
			// ...
		}
	#endregion

	#region Main
		[STAThread]
		static void Main() 
		{
			System.Windows.Forms.Application.Run(new DemoTabForm());
		}
	#endregion
	}
}
