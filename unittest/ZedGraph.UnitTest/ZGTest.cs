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
	/// <version> $Revision: 1.2 $ $Date: 2004-08-26 05:49:11 $ </version>
	[TestFixture]
	public class BaseZGTest
	{
		Form form;
		Form form2;
		GraphPane testee;
		ZedGraphControl control;
		static System.Windows.Forms.Timer myTimer;
       
		[SetUp]
		public void SetUp()
		{
			form2	= new Form();
			form2.Size = new Size( 500, 500 );
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
			form2.Refresh();
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

			Assertion.Assert( TestUtils.promptIfTestWorked( "Did the graph resize ok?" ) );
		}
		#endregion
		
		#region Animated Date Graph
		[Test]
		public void AnimatedDateGraph()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"My Test Date Graph", "X AXIS", "Y Value" );

			// start with an empty list for testing
			PointPairList pointList = new PointPairList();
			
			//MessageBox.Show( "Im here" );

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				pointList, Color.Red, SymbolType.Diamond );

			// Set the XAxis to date type
			testee.XAxis.Type = AxisType.Date;
			
			// make the symbols filled blue
			myCurve.Symbol.IsFilled = true;
			myCurve.Symbol.Color = Color.Blue;
			
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			// Draw a sinusoidal curve, adding one point at a time
			// and refiguring/redrawing each time. (a stress test)
			// redo creategraphics() each time to stress test
			for ( int i=0; i<300; i++ )
			{
				double x = (double) new XDate( 1995, i+1, 1 );
				double y = Math.Sin( (double) i * Math.PI / 30.0 );
				
				myCurve.AddPoint( x, y );
				testee.AxisChange( form2.CreateGraphics() );
				form2.Refresh();
				
				// delay for 10 ms
				//DelaySeconds( 50 );
			}
			
			while ( myCurve.Points.Count > 0 )
			{
				// remove the first point in the list
				myCurve.Points.RemoveAt( 0 );
				testee.AxisChange( form2.CreateGraphics() );
				form2.Refresh();
				
				// delay for 10 ms
				//DelaySeconds( 50 );
			}
			
			Assertion.Assert( TestUtils.promptIfTestWorked( "Did you see points added one by one, then deleted one by one?" ) );
		}
		#endregion
		
		#region Missing Values test
		[Test]
		public void MissingValues()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Years\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
				
			SetSize();
			form2.Show();

			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, PointPair.Missing, PointPair.Missing, 35, 60, 90, 25, 48, PointPair.Missing };
			double[] x2 = { 300, 400, 500, 600, 700, 800, 900 };
			double[] y2 = { PointPair.Missing, 43, 27, 62, 89, 73, 12 };
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, PointPair.Missing, 88.57, 99.9, 36.8 };

			double[] x4 = { 150, 250, 400, 520, 780, 940 };
			double[] y4 = { .03, .054, .011, .02, .14, .38 };
			double[] x5 = { 1.5, 2.5, 4, 5.2, 7.8, 9.4 };
			double[] y5 = { 157, 458, 1400, 100000, 10290, 3854 };

			CurveItem curve;
			curve = testee.AddCurve( "Larry", x, y, Color.Red, SymbolType.Circle );
			curve.Line.Width = 2.0F;
			curve.Symbol.IsFilled = true;
			curve = testee.AddCurve( "Moe", x3, y3, Color.Green, SymbolType.Triangle );
			curve.Symbol.IsFilled = true;
			curve = testee.AddCurve( "Curly", x2, y2, Color.Blue, SymbolType.Diamond );
			curve.Symbol.IsFilled = true;
			curve.Symbol.Size = 12;

			testee.PaneBackColor = Color.WhiteSmoke;
			testee.AxisBackColor = Color.LightGoldenrodYellow;
			testee.XAxis.IsShowGrid = true;
			testee.XAxis.ScaleFontSpec.Angle = 0;

			testee.YAxis.IsShowGrid = true;
			testee.YAxis.ScaleFontSpec.Angle = 90;

			TextItem text = new TextItem("First Prod\n21-Oct-93", 100F, 50.0F );
			text.AlignH = FontAlignH.Center;
			text.AlignV = FontAlignV.Bottom;
			text.FontSpec.FillColor = Color.PowderBlue;
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
			text.FontSpec.IsFramed = false;
			text.FontSpec.FrameColor = Color.Red;
			text.FontSpec.IsFilled = false;

			text.AlignH = FontAlignH.Left;
			text.AlignV = FontAlignV.Bottom;
			testee.TextList.Add( text );
			
			for ( int i=0; i<curve.Points.Count; i++ )
			{
				PointPair point = curve.Points[i];
				if ( i % 3 == 0 )
					point.Y = PointPair.Missing;
				else if ( i % 3 == 1 )
					point.Y = System.Double.NaN;
				else if ( i % 3 == 2 )
					point.Y = System.Double.PositiveInfinity;

				form2.Refresh();
				
				// delay for 10 ms
				DelaySeconds( 100 );
			}
			
			Assertion.Assert( TestUtils.promptIfTestWorked( "Did you see an initial graph, with points disappearing one by one?" ) );
			
			// Go ahead and refigure the axes with the invalid data just to check
			testee.AxisChange( form2.CreateGraphics() );
		}
		#endregion

			
		#region A dual Y test
		[Test]
		public void DualY()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"My Test Dual Y Graph", "Date", "My Y Axis" );
				
			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			double[] y2 = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, i+1, 1 );
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
				y2[i] = y[i] * 3.6178;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			testee.XAxis.Type = AxisType.Date;

			// Generate a blue curve with diamond
			// symbols, and "My Curve" in the legend
			myCurve = testee.AddCurve( "My Curve 1",
				x, y2, Color.Blue, SymbolType.Circle );
			myCurve.IsY2Axis = true;
			testee.YAxis.IsVisible = true;
			testee.Y2Axis.IsVisible = true;
			testee.Y2Axis.IsShowGrid = true;
			testee.XAxis.IsShowGrid = true;
			testee.YAxis.IsOppositeTic = false;
			testee.YAxis.IsMinorOppositeTic = false;
			testee.YAxis.IsZeroLine = false;
			
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			Assertion.Assert( TestUtils.promptIfTestWorked( "Do you see a dual Y graph?" ) );
		}
		#endregion
		
		#region Stress test with all NaN's
		[Test]
		public void AllNaN()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"My Test NaN Graph", "Date", "My Y Axis" );
				
			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, i+1, 1 );
				y[i] = System.Double.NaN;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Circle );
			// Set the XAxis to date type
			testee.XAxis.Type = AxisType.Date;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			Assertion.Assert( TestUtils.promptIfTestWorked( "Do you see a graph with all values missing (NaN's)?" ) );
		}
		#endregion
		
		#region the date label-width test
		[Test]
		public void LabelWidth()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"My Test Label Width", "Date", "My Y Axis" );
				
			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) new XDate( 1995, 1, i+1 );
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 );
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis to date type
			testee.XAxis.Type = AxisType.Date;
			testee.XAxis.ScaleFormat = "&dd-&mmm-&yyyy";


			// Tell ZedGraph to refigure the
			// axes since the data have changed
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			Assertion.Assert( TestUtils.promptIfTestWorked( "If you see a date graph, resize it and make" +
									" sure the label count is reduced to avoid overlap" ) );

			DelaySeconds( 3000 );
			
			Assertion.Assert( TestUtils.promptIfTestWorked( "Did the anti-overlap work?" ) );
		}
		#endregion

		#region text axis sample
		[Test]
		public void TextAxis()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"Text Graph", "Label", "Y Value" );
				
			// Make up some random data points
			string[] labels = { "USA", "Spain", "Qatar", "Morocco", "UK", "Uganda",
								  "Cambodia", "Malaysia", "Australia", "Ecuador" };
								  
			double[] y = new double[10];
			for ( int i=0; i<10; i++ )
				y[i] = Math.Sin( (double) i * Math.PI / 2.0 );
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				null, y, Color.Red, SymbolType.Diamond );
			// Set the XAxis labels
			testee.XAxis.TextLabels = labels;
			// Set the XAxis to Text type
			testee.XAxis.Type = AxisType.Text;
			// Set the labels at an angle so they don't overlap
			testee.XAxis.ScaleFontSpec.Angle = 0;
			// Tell ZedGraph to refigure the
			// axes since the data have changed
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			Assertion.Assert( TestUtils.promptIfTestWorked( "Did you get an X Text axis?" ) );
			
			myCurve.Points.Clear();
			for ( double i=0; i<100; i++ )
				myCurve.AddPoint( i / 10.0, Math.Sin( i * Math.PI / 20.0 ) );
				
			testee.AxisChange( form2.CreateGraphics() );
			form2.Refresh();
			
			Assertion.Assert( TestUtils.promptIfTestWorked( "Did the points fill in between the labels?" ) );
		}
		#endregion

		static bool exitFlag;

		// This is the method to run when the timer is raised.
		private static void TimerEventProcessor( Object myObject,
			EventArgs myEventArgs ) 
		{
			myTimer.Stop();
			exitFlag = true;
 		}

		public void DelaySeconds( int sec )
		{
			myTimer = new System.Windows.Forms.Timer();
			/* Adds the event and the event handler for the method that will 
				process the timer event to the timer. */
			myTimer.Tick += new EventHandler( TimerEventProcessor );
			// Sets the timer interval to 3 seconds.
			myTimer.Interval = sec;
			myTimer.Start();

			// Runs the timer, and raises the event.
			exitFlag = false;
			while( !exitFlag ) 
			{
				// Processes all the events in the queue.
				Application.DoEvents();
			}
		}
	}
}
