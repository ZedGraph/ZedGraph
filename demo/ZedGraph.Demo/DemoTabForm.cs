using System;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for DemoTabForm.
	/// </summary>
	public class DemoTabForm : ChartTabForm
	{
		public DemoTabForm()
		{
		}

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
			
			loadDemo(new InitialSampleDemo());
			loadDemo(new ModInitialSampleDemo());
			loadDemo(new DateAxisSampleDemo());
			loadDemo(new TextAxisSampleDemo());
			loadDemo(new BarChartSampleDemo());
			loadDemo(new HorizontalBarSampleDemo());
			loadDemo(new StackedBarSampleDemo());
			loadDemo(new PercentStkBarDemo());
			
			// TODO: add more demos here
			// ...
		}

		[STAThread]
		static void Main() 
		{
			System.Windows.Forms.Application.Run(new DemoTabForm());
		}
	}
}
