using System;
using System.Drawing;
using System.Windows.Forms;

using NUnit.Framework;

namespace ZedGraph.UnitTest
{
	class TestUtils
	{
		public static bool waitForUserOK = true;

		public static bool promptIfTestWorked(string msg)
		{
			// just act like the test worked
			if (!waitForUserOK)
				return true;

			if (DialogResult.Yes == MessageBox.Show(msg, "ZedGraph Test", MessageBoxButtons.YesNo))
				return true;
			else
				return false;
		}
	}

	/// <summary>
	/// Basically the initial graph given in the ZedGraph example
	/// <a href="http://www.codeproject.com/csharp/ZedGraph.asp">
	/// http://www.codeproject.com/csharp/ZedGraph.asp</a>
	/// </summary>
	/// 
	/// <author> Jerry Vos revised by John Champion </author>
	/// <version> $Revision: 1.1 $ $Date: 2004-08-24 06:27:44 $ </version>
	[TestFixture]
	public class BaseZGTest
	{
		Form form;
		Form form2;
		GraphPane testee;
		ZedGraphControl control;
        
		[SetUp]
		public void SetUp()
		{
			form2	= new Form();
			form2.Paint += new System.Windows.Forms.PaintEventHandler( this.Form2_Paint );
			form2.Resize += new System.EventHandler( this.Form2_Resize );
			form2.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form2_MouseDown );

			form	= new Form();
			control	= new ZedGraphControl();

			control.GraphPane = new GraphPane( new System.Drawing.Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );

			control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

			control.Size = form.ClientSize;

			testee	= control.GraphPane;

			form.Controls.Add( control );
		}

		[TearDown] 
		public void Terminate()
		{
			form2.Dispose();
			form.Dispose();
		}

		private void Form2_Paint( object sender, System.Windows.Forms.PaintEventArgs e )
		{
			SolidBrush brush = new SolidBrush( Color.Gray );
			e.Graphics.FillRectangle( brush, form2.ClientRectangle );
			testee.Draw( e.Graphics );
		}

		private void Form2_Resize(object sender, System.EventArgs e)
		{
			SetSize();
			testee.AxisChange( form2.CreateGraphics() );
			form2.Invalidate();
		}

		private void SetSize()
		{
			Rectangle paneRect = form2.ClientRectangle;
			paneRect.Inflate( -10, -10 );
			testee.PaneRect = paneRect;
		}

		private void Form2_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
		{
		}
		
		#region Empty UserControl
		[Test]
		public void EmptyUserControl()
		{
			form.Show();
			
			Assertion.Assert( TestUtils.promptIfTestWorked( "Is an empty graph visible?" ) );
		}
		#endregion

		#region Standard Sample UserControl
		[Test]
		public void StandardUserControl()
		{
			testee.AxisBackColor = Color.LightGoldenrodYellow;

			double[] x = { 72, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 40, 35, 60, 90, 25, 48, 75 };
			double[] x2 = { 300, 400, 500, 600, 700, 800, 900 };
			double[] y2 = { 75, 43, 27, 62, 89, 73, 12 };
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			
			CurveItem curve;
			curve = testee.AddCurve( "Larry", x, y, Color.Red, SymbolType.Circle );
			curve.Symbol.Size = 14;
			curve.Line.Width = 2.0F;
			curve = testee.AddCurve( "Curly", x2, y2, Color.Green, SymbolType.Triangle );
			curve.Symbol.Size = 14;
			curve.Line.Width = 2.0F;
			curve.Symbol.IsFilled = true;
			curve = testee.AddCurve( "Moe", x3, y3, Color.Blue, SymbolType.Diamond );
			curve.Line.IsVisible = false;
			curve.Symbol.IsFilled = true;
			curve.Symbol.Size = 14;
			
			testee.XAxis.IsShowGrid = true;
			testee.XAxis.ScaleFontSpec.Angle = 60;
			
			testee.YAxis.IsShowGrid = true;
			
			TextItem text = new TextItem("First Prod\n21-Oct-99", 100F, 50.0F );
			text.AlignH = FontAlignH.Center;
			text.AlignV = FontAlignV.Bottom;
			text.FontSpec.FillColor = Color.LightBlue;
			text.FontSpec.IsItalic = true;
			testee.TextList.Add( text );
			
			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 100F, 47F, 72F, 25F );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			testee.ArrowList.Add( arrow );
			
			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.AlignH = FontAlignH.Right;
			text.AlignV = FontAlignV.Center;
			text.FontSpec.IsFilled = true;
			text.FontSpec.FillColor = Color.LightGoldenrodYellow;
			text.FontSpec.IsFramed = false;
			testee.TextList.Add( text );
			
			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			testee.ArrowList.Add( arrow );
			text = new TextItem("Confidential", 0.8F, -0.03F );
			text.CoordinateFrame = CoordType.AxisFraction;
			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.IsFramed = true;
			text.FontSpec.FrameColor = Color.Red;
			text.AlignH = FontAlignH.Left;
			text.AlignV = FontAlignV.Bottom;
			testee.TextList.Add( text );
			
			testee.AxisChange( control.CreateGraphics() );

			form.Show();
			
			Assertion.Assert( TestUtils.promptIfTestWorked(
					"Is a graph visible with 3 sets of data and text labels?" ) );
		}
		#endregion

		#region Standard Bar Graph
		[Test]
		public void StandardBarGraph()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"My Test Bar Graph", "Label", "My Y Axis" );

			string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard", "Kitty" };
			double[] y = { 100, 115, 75, -22, 98, 40, -10 };
			double[] y2 = { 90, 100, 95, -35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, -15, 54, 67, 18 };

			double[] y4 = { 120, 125, 100, 20, 105, 75, -40 };

			// Generate a red bar with "Curve 1" in the legend
			CurveItem myCurve = testee.AddCurve( "Curve 1", null, y, Color.Red );
			// Make it a bar
			myCurve.IsBar = true;

			/*
						// Generate a blue bar with "Curve 2" in the legend
						myCurve = myPane.AddCurve( "Curve 2",
							null, y2, Color.Blue );
						// Make it a bar
						myCurve.IsBar = true;
			*/
			/*
						// Generate a green bar with "Curve 3" in the legend
						myCurve = myPane.AddCurve( "Curve 3",
							null, y3, Color.Green );
						// Make it a bar
						myCurve.IsBar = true;
			*/
			/*
						// Generate a black line with "Curve 4" in the legend
						myCurve = myPane.AddCurve( "Curve 4",
							y4, y4, Color.Black, SymbolType.Circle );

						myCurve.Symbol.Size = 14.0F;
						myCurve.Symbol.IsFilled = true;
						myCurve.Line.Width = 2.0F;
			*/

			// Draw the X tics between the labels instead of at the labels
			testee.XAxis.IsTicsBetweenLabels = true;

			// Set the XAxis labels
			testee.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			testee.XAxis.Type = AxisType.Ordinal;

			testee.XAxis.IsReverse = false;
			testee.ClusterScaleWidth = 1;

			//Add Labels to the curves

			// Shift the text items up by 5 user scale units above the bars
			const float shift = 5;
			
			for ( int i=0; i<y.Length; i++ )
			{
				// format the label string to have 1 decimal place
				string lab = y[i].ToString( "F1" );
				// create the text item (assumes the x axis is ordinal or text)
				// for negative bars, the label appears just above the zero value
				TextItem text = new TextItem( lab, (float) (i+1), (float) (y[i] < 0 ? 0.0 : y[i]) + shift );
				// tell Zedgraph to use user scale units for locating the TextItem
				text.CoordinateFrame = CoordType.AxisXYScale;
				// Align the left-center of the text to the specified point
				text.AlignH = FontAlignH.Left;
				text.AlignV = FontAlignV.Center;
				text.FontSpec.IsFramed = false;
				// rotate the text 90 degrees
				text.FontSpec.Angle = 90;
				// add the TextItem to the list
				testee.TextList.Add( text );
			}

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			testee.AxisChange( form2.CreateGraphics() );
			// Add one step to the max scale value to leave room for the labels
			testee.YAxis.Max += testee.YAxis.Step;

			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();
			
			Assertion.Assert( TestUtils.promptIfTestWorked(
				"Is a bar graph visible with 1 set of data and value labels?  <Next Step: Resize the Graph for 3 seconds>" ) );

			DelaySeconds( 3000 );

			Assertion.Assert( TestUtils.promptIfTestWorked(
				"Did the graph resize ok?" ) );
		}

		static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
		static int alarmCounter = 1;
		static bool exitFlag = false;

		// This is the method to run when the timer is raised.
		private static void TimerEventProcessor( Object myObject,
			EventArgs myEventArgs ) 
		{
			myTimer.Stop();
			exitFlag = true;
 		}

		public void DelaySeconds( int sec )
		{
			/* Adds the event and the event handler for the method that will 
				process the timer event to the timer. */
			myTimer.Tick += new EventHandler( TimerEventProcessor );

			// Sets the timer interval to 3 seconds.
			myTimer.Interval = sec;
			myTimer.Start();

			// Runs the timer, and raises the event.
			while( exitFlag == false ) 
			{
				// Processes all the events in the queue.
				Application.DoEvents();
			}
		}

		#endregion
	}
}
