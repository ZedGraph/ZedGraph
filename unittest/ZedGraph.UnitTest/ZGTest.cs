using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using NUnit.Framework;

namespace ZedGraph.UnitTest
{
	class TestUtils
	{
		public static bool waitForUserOK = true;
		public static int delayTime = 500;
		static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
		static bool exitFlag;

		public static void SetUp()
		{
			/* Adds the event and the event handler for the method that will 
				process the timer event to the timer. */
			myTimer.Tick += new EventHandler( TimerEventProcessor );
		}
			
		public static bool promptIfTestWorked(string msg)
		{
			Console.WriteLine( msg );

			// just act like the test worked
			if ( !waitForUserOK )
			{
				if ( delayTime > 0 )
					DelaySeconds( delayTime );
				return true;
			}

			if (DialogResult.Yes == MessageBox.Show(msg, "ZedGraph Test", MessageBoxButtons.YesNo))
				return true;
			else
				return false;
		}

		// This is the method to run when the timer is raised.
		private static void TimerEventProcessor( Object myObject,
			EventArgs myEventArgs ) 
		{
			myTimer.Stop();
			exitFlag = true;
		}

		public static void DelaySeconds( int sec )
		{
			// Sets the timer interval to 3 seconds.
			myTimer.Interval = sec;
			myTimer.Start();

			// Runs the timer, and raises the event.
			exitFlag = false;
			while( exitFlag == false ) 
			{
				// Processes all the events in the queue.
				Application.DoEvents();
			}
		}
	}

	#region ControlTest
	/// <summary>
	/// Basically the initial graph given in the ZedGraph example
	/// <a href="http://www.codeproject.com/csharp/ZedGraph.asp">
	/// http://www.codeproject.com/csharp/ZedGraph.asp</a>
	/// </summary>
	/// 
	/// <author> Jerry Vos revised by John Champion </author>
	/// <version> $Revision: 2.4 $ $Date: 2004-09-19 06:12:08 $ </version>
	[TestFixture]
	public class ControlTest
	{
		Form form;
		GraphPane testee;
		ZedGraphControl control;
       
		[SetUp]
		public void SetUp()
		{
			TestUtils.SetUp();

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
			form.Dispose();
		}


		#region Empty UserControl
		[Test]
		public void EmptyUserControl()
		{
			form.Show();
			
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Is an empty graph visible?" ) );
		}
		#endregion

		#region Standard Sample UserControl
		[Test]
		public void StandardUserControl()
		{
			testee.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow );

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
			curve.Symbol.Fill.Type = FillType.Solid;
			curve = testee.AddCurve( "Moe", x3, y3, Color.Blue, SymbolType.Diamond );
			curve.Line.IsVisible = false;
			curve.Symbol.Fill.Type = FillType.Solid;
			curve.Symbol.Size = 14;
			
			testee.XAxis.IsShowGrid = true;
			testee.XAxis.ScaleFontSpec.Angle = 60;
			
			testee.YAxis.IsShowGrid = true;
			
			TextItem text = new TextItem("First Prod\n21-Oct-99", 100F, 50.0F );
			text.AlignH = AlignH.Center;
			text.AlignV = AlignV.Bottom;
			text.FontSpec.Fill.Color = Color.LightBlue;
			text.FontSpec.Fill.Type = FillType.Brush;
			text.FontSpec.IsItalic = true;
			testee.TextList.Add( text );
			
			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 100F, 47F, 72F, 25F );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			testee.ArrowList.Add( arrow );
			
			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.AlignH = AlignH.Right;
			text.AlignV = AlignV.Center;
			text.FontSpec.Fill.Color = Color.LightGoldenrodYellow;
			text.FontSpec.Fill.Type = FillType.Brush;
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
			text.AlignH = AlignH.Left;
			text.AlignV = AlignV.Bottom;
			testee.TextList.Add( text );
			
			testee.AxisChange( control.CreateGraphics() );

			form.Show();
			
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Is a graph visible with 3 sets of data and text labels?" ) );
		}
		#endregion

	}
	#endregion

	#region Library Test
	/// <summary>
	/// Test code suite for the class library
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 2.4 $ $Date: 2004-09-19 06:12:08 $ </version>
	[TestFixture]
	public class LibraryTest
	{
		Form form2;
		GraphPane testee;
      
		[SetUp]
		public void SetUp()
		{
			TestUtils.SetUp();

			form2	= new Form();
			form2.Size = new Size( 500, 500 );
			form2.Paint += new System.Windows.Forms.PaintEventHandler( this.Form2_Paint );
			form2.Resize += new System.EventHandler( this.Form2_Resize );
			form2.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form2_MouseDown );
			form2.Show();

		}

		[TearDown] 
		public void Terminate()
		{
			form2.Dispose();
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
			testee.XAxis.Type = AxisType.Text;

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
				text.AlignH = AlignH.Left;
				text.AlignV = AlignV.Center;
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
			form2.Refresh();
			
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Is a bar graph visible with 1 set of data and value labels?  <Next Step: Resize the Graph for 3 seconds>" ) );

			TestUtils.DelaySeconds( 3000 );

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did the graph resize ok?" ) );
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
			
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				pointList, Color.Red, SymbolType.Diamond );

			// Set the XAxis to date type
			testee.XAxis.Type = AxisType.Date;
			
			// make the symbols filled blue
			myCurve.Symbol.Fill.Type = FillType.Solid;
			myCurve.Symbol.Fill.Color = Color.Blue;
			
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
			
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did you see points added one by one, then deleted one by one?" ) );
		}
		#endregion
		
		#region Single Value Test
		[Test]
		public void SingleValue()
		{
			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Years\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
				
			double[] x = { 0.4875 };
			double[] y = { -123456 };

			CurveItem curve;
			curve = testee.AddCurve( "One Value", x, y, Color.Red, SymbolType.Diamond );
			curve.Symbol.Fill.Type = FillType.Solid;

			testee.XAxis.IsShowGrid = true;
			testee.YAxis.IsShowGrid = true;

			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Do you see a single value in the middle of the scale ranges?" ) );
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
			curve.Symbol.Fill.Type = FillType.Solid;
			curve = testee.AddCurve( "Moe", x3, y3, Color.Green, SymbolType.Triangle );
			curve.Symbol.Fill.Type = FillType.Solid;
			curve = testee.AddCurve( "Curly", x2, y2, Color.Blue, SymbolType.Diamond );
			curve.Symbol.Fill.Type = FillType.Solid;
			curve.Symbol.Size = 12;

			testee.PaneFill = new Fill( Color.White, Color.WhiteSmoke );
			testee.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow );
			testee.XAxis.IsShowGrid = true;
			testee.XAxis.ScaleFontSpec.Angle = 0;

			testee.YAxis.IsShowGrid = true;
			testee.YAxis.ScaleFontSpec.Angle = 90;

			TextItem text = new TextItem("First Prod\n21-Oct-93", 100F, 50.0F );
			text.AlignH = AlignH.Center;
			text.AlignV = AlignV.Bottom;
			text.FontSpec.Fill.Color = Color.PowderBlue;
			text.FontSpec.Fill.Type = FillType.Brush;
			testee.TextList.Add( text );

			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 100F, 47F, 72F, 25F );
			arrow.CoordinateFrame = CoordType.AxisXYScale;
			testee.ArrowList.Add( arrow );

			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.AlignH = AlignH.Right;
			text.AlignV = AlignV.Center;
			text.FontSpec.Fill.Color = Color.LightGoldenrodYellow;
			text.FontSpec.Fill.Type = FillType.Brush;
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
			text.FontSpec.Fill.Type = FillType.None;

			text.AlignH = AlignH.Left;
			text.AlignV = AlignV.Bottom;
			testee.TextList.Add( text );
			
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			for ( int iCurve=0; iCurve<3; iCurve++ )
				for ( int i=0; i<testee.CurveList[iCurve].Points.Count; i++ )
				{
					PointPair pt = testee.CurveList[iCurve].Points[i];

					if ( i % 3 == 0 )
						pt.Y = PointPair.Missing;
					else if ( i % 3 == 1 )
						pt.Y = System.Double.NaN;
					else if ( i % 3 == 2 )
						pt.Y = System.Double.PositiveInfinity;

					testee.CurveList[iCurve].Points[i] = pt;

					form2.Refresh();
					
					// delay for 10 ms
					TestUtils.DelaySeconds( 300 );
				}

			Assert.IsTrue( TestUtils.promptIfTestWorked( "xDid you see an initial graph, with points disappearing one by one?" ) );
			
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

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Do you see a dual Y graph?" ) );
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

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Do you see a graph with all values missing (NaN's)?" ) );
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

			Assert.IsTrue( TestUtils.promptIfTestWorked( "If you see a date graph, resize it and make" +
									" sure the label count is reduced to avoid overlap" ) );

			TestUtils.DelaySeconds( 3000 );
			
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did the anti-overlap work?" ) );
		}
		#endregion

		#region Smooth Curve Sample
		[Test]
		public void SmoothCurve()
		{
//			memGraphics.CreateDoubleBuffer( form2.CreateGraphics(),
//				form2.ClientRectangle.Width, form2.ClientRectangle.Height );

			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"Text Graph", "Label", "Y Value" );
				
			// Make up some random data points
			string[] labels = { "USA", "Spain", "Qatar", "Morocco", "UK", "Uganda",
								  "Cambodia", "Malaysia", "Australia", "Ecuador" };
								  
			PointPairList points = new PointPairList();
			double numPoints = 10.0;
			for ( double i=0; i<numPoints; i++ )
				points.Add( i / (numPoints / 10.0) + 1.0, Math.Sin( i / (numPoints / 10.0) * Math.PI / 2.0 ) );

			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				points, Color.Red, SymbolType.Diamond );
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
			myCurve.Line.IsSmooth = true;

			for ( float tension=0.0F; tension<3.0F; tension+=0.1F )
			{
				myCurve.Line.SmoothTension = tension;
				form2.Refresh();

				TestUtils.DelaySeconds( 50 );
			}
			for ( float tension=3.0F; tension>=0F; tension-=0.1F )
			{
				myCurve.Line.SmoothTension = tension;
				form2.Refresh();

				TestUtils.DelaySeconds( 50 );
			}

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did you see varying levels of smoothing?" ) );
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

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did you get an X Text axis?" ) );
			
			myCurve.Points.Clear();
			for ( double i=0; i<100; i++ )
				myCurve.AddPoint( i / 10.0, Math.Sin( i / 10.0 * Math.PI / 2.0 ) );
				
			testee.AxisChange( form2.CreateGraphics() );
			form2.Refresh();
			
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did the points fill in between the labels? (Next Resize the graph and check label overlap again)" ) );

			TestUtils.DelaySeconds( 3000 );

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Did the graph resize ok?" ) );
		}
		#endregion

	}
	#endregion

	#region Long Feature Test
	/// <summary>
	/// Test code suite for the class library
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 2.4 $ $Date: 2004-09-19 06:12:08 $ </version>
	[TestFixture]
	public class LongFeatureTest
	{
		Form form2;
		GraphPane testee;
       
		[TestFixtureSetUp]
		public void SetUp()
		{
			TestUtils.SetUp();

			form2	= new Form();
			form2.Size = new Size( 500, 500 );
			form2.Paint += new System.Windows.Forms.PaintEventHandler( this.Form2_Paint );
			form2.Resize += new System.EventHandler( this.Form2_Resize );
			form2.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form2_MouseDown );
		}

		[TearDown] 
		public void Terminate()
		{
			form2.Dispose();
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

		#region The Long Feature Test
		[Test]
		public void LongFeature()
		{
			bool userOK = TestUtils.waitForUserOK;

			if ( MessageBox.Show( "Do you want to prompt at each step (otherwise, I will just run through" +
				" the whole test)?  Pick YES to Prompt at each step", "Test Setup",
				MessageBoxButtons.YesNo ) == DialogResult.No )
			{
				TestUtils.waitForUserOK = false;
			}

			// Create a new graph
			testee = new GraphPane( new Rectangle( 40, 40, form2.Size.Width-80, form2.Size.Height-80 ),
				"My Test Dual Y Graph", "Date", "My Y Axis" );
				
			// Make up some random data points
			double[] x = new double[36];
			double[] y = new double[36];
			double[] y2 = new double[36];
			for ( int i=0; i<36; i++ )
			{
				x[i] = (double) i * 5.0;
				y[i] = Math.Sin( (double) i * Math.PI / 15.0 ) * 16.0;
				y2[i] = y[i] * 10.5;
			}
			// Generate a red curve with diamond
			// symbols, and "My Curve" in the legend
			CurveItem myCurve = testee.AddCurve( "My Curve",
				x, y, Color.Red, SymbolType.Diamond );

			// Generate a blue curve with diamond
			// symbols, and "My Curve" in the legend
			myCurve = testee.AddCurve( "My Curve 1",
				x, y2, Color.Blue, SymbolType.Circle );
			myCurve.IsY2Axis = true;

			testee.XAxis.IsShowGrid = false;
			testee.XAxis.IsVisible = false;
			testee.XAxis.IsZeroLine = false;
			testee.XAxis.IsTic = false;
			testee.XAxis.IsMinorTic = false;
			testee.XAxis.IsInsideTic = false;
			testee.XAxis.IsMinorInsideTic = false;
			testee.XAxis.IsMinorOppositeTic = false;
			testee.XAxis.IsOppositeTic = false;
			testee.XAxis.IsReverse = false;
			// testee.XAxis.IsLog = false;
			testee.XAxis.Title = "";

			testee.YAxis.IsShowGrid = false;
			testee.YAxis.IsVisible = false;
			testee.YAxis.IsZeroLine = false;
			testee.YAxis.IsTic = false;
			testee.YAxis.IsMinorTic = false;
			testee.YAxis.IsInsideTic = false;
			testee.YAxis.IsMinorInsideTic = false;
			testee.YAxis.IsMinorOppositeTic = false;
			testee.YAxis.IsOppositeTic = false;
			testee.YAxis.IsReverse = false;
			//testee.YAxis.IsLog = false;
			testee.YAxis.Title = "";

			testee.Y2Axis.IsShowGrid = false;
			testee.Y2Axis.IsVisible = false;
			testee.Y2Axis.IsZeroLine = false;
			testee.Y2Axis.IsTic = false;
			testee.Y2Axis.IsMinorTic = false;
			testee.Y2Axis.IsInsideTic = false;
			testee.Y2Axis.IsMinorInsideTic = false;
			testee.Y2Axis.IsMinorOppositeTic = false;
			testee.Y2Axis.IsOppositeTic = false;
			testee.Y2Axis.IsReverse = false;
			//testee.Y2Axis.IsLog = false;
			testee.Y2Axis.Title = "";

			testee.IsAxisFramed = false;
			testee.IsPaneFramed = false;

			testee.IsShowTitle = false;
			testee.Legend.IsHStack = false;
			testee.Legend.IsVisible = false;
			testee.Legend.IsFramed = false;
			testee.Legend.Fill.Type = FillType.None;
			testee.Legend.Location = LegendLoc.Bottom;

			// Tell ZedGraph to refigure the
			// axes since the data have changed
			testee.AxisChange( form2.CreateGraphics() );
			SetSize();
			form2.Show();

			Assert.IsTrue( TestUtils.promptIfTestWorked( "Do you see a dual Y graph with no axes?" ) );
			
			testee.IsPaneFramed = true;
			testee.PaneFrameColor = Color.Red;
			testee.PaneFramePenWidth = 3.0F;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Pane Frame Added?" ) );
			
			testee.PaneFrameColor = Color.Black;
			testee.PaneFramePenWidth = 1.0F;

			testee.IsAxisFramed = true;
			testee.AxisFrameColor = Color.Red;
			testee.AxisFramePenWidth = 3.0F;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Axis Frame Added?" ) );
			
			testee.AxisFrameColor = Color.Black;
			testee.AxisFramePenWidth = 1.0F;

			testee.PaneFill = new Fill( Color.White, Color.LightGoldenrodYellow );
			testee.PaneGap = 50.0F;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Pane Background Filled?" ) );
			
			testee.PaneGap = 20.0F;
			testee.PaneFill.IsFilled = false;
			testee.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow );

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Axis Background Filled?" ) );
			
			testee.AxisFill.IsFilled = false;
			
			testee.IsShowTitle = true;
			testee.FontSpec.FontColor = Color.Red;
			testee.Title = "The Title";

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Title Added?" ) );
			
			testee.FontSpec.FontColor = Color.Black;

			testee.Legend.IsVisible = true;
			testee.Legend.FontSpec.FontColor = Color.Red;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Added?" ) );
			
			testee.Legend.FontSpec.FontColor = Color.Black;

			testee.Legend.IsFramed = true;
			testee.Legend.FrameColor = Color.Red;
			testee.Legend.FrameWidth = 3.0F;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Frame Added?" ) );

			testee.Legend.FrameColor = Color.Black;
			testee.Legend.FrameWidth = 1.0F;

			testee.Legend.Fill.Type = FillType.Brush;
			testee.Legend.Fill.Color = Color.LightGoldenrodYellow;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Fill Added?" ) );

			testee.Legend.Location = LegendLoc.InsideBotLeft;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Inside Bottom Left?" ) );

			testee.Legend.Location = LegendLoc.InsideBotRight;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Inside Bottom Right?" ) );

			testee.Legend.Location = LegendLoc.InsideTopLeft;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Inside Top Left?" ) );

			testee.Legend.Location = LegendLoc.InsideTopRight;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Inside Top Right?" ) );

			testee.Legend.Location = LegendLoc.Left;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Left?" ) );

			testee.Legend.Location = LegendLoc.Right;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Right?" ) );

			testee.Legend.Location = LegendLoc.Top;
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Moved to Top?" ) );

			testee.Legend.IsHStack = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Legend Horizontal Stacked?" ) );
			testee.Legend.Fill.Type = FillType.None;

			/////////  X AXIS /////////////////////////////////////////////////////////////////////////

			testee.XAxis.IsVisible = true;
			testee.XAxis.Color = Color.Red;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Visible?" ) );

			testee.XAxis.Title = "X Axis Title";

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Title Visible?" ) );

			//testee.XAxis.TicPenWidth = 3.0F;
			testee.XAxis.IsZeroLine = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis ZeroLine Visible?" ) );

			testee.XAxis.IsZeroLine = false;
			//testee.XAxis.TicPenWidth = 1.0F;
			testee.XAxis.IsTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis major tics Visible?" ) );

			testee.XAxis.IsMinorTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis minor tics Visible?" ) );

			testee.XAxis.IsInsideTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Inside tics Visible?" ) );

			testee.XAxis.IsOppositeTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Opposite tics Visible?" ) );

			testee.XAxis.IsMinorInsideTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Minor Inside tics Visible?" ) );

			testee.XAxis.IsMinorOppositeTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Minor Opposite tics Visible?" ) );

			testee.XAxis.TicPenWidth = 1.0F;
			testee.XAxis.Color = Color.Black;
			testee.XAxis.IsShowGrid = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Grid Visible?" ) );

			testee.XAxis.GridPenWidth = 1.0F;
			testee.XAxis.GridColor = Color.Black;
			testee.XAxis.IsReverse = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Reversed?" ) );

			testee.XAxis.IsReverse = false;
			testee.XAxis.Type = AxisType.Log;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "X Axis Log?" ) );

			testee.XAxis.Type = AxisType.Linear;

			///////////////////////////////////////////////////////////////////////////////

			/////////  Y AXIS /////////////////////////////////////////////////////////////////////////

			testee.YAxis.IsVisible = true;
			testee.YAxis.Color = Color.Red;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Visible?" ) );

			testee.YAxis.Title = "Y Axis Title";

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Title Visible?" ) );

			//testee.YAxis.TicPenWidth = 3.0F;
			testee.YAxis.IsZeroLine = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis ZeroLine Visible?" ) );

			testee.YAxis.IsZeroLine = false;
			//testee.YAxis.TicPenWidth = 1.0F;
			testee.YAxis.IsTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis major tics Visible?" ) );

			testee.YAxis.IsMinorTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis minor tics Visible?" ) );

			testee.YAxis.IsInsideTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Inside tics Visible?" ) );

			testee.YAxis.IsOppositeTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Opposite tics Visible?" ) );

			testee.YAxis.IsMinorInsideTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Minor Inside tics Visible?" ) );

			testee.YAxis.IsMinorOppositeTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Minor Opposite tics Visible?" ) );

			testee.YAxis.TicPenWidth = 1.0F;
			testee.YAxis.Color = Color.Black;
			testee.YAxis.IsShowGrid = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Grid Visible?" ) );

			testee.YAxis.GridPenWidth = 1.0F;
			testee.YAxis.GridColor = Color.Black;
			testee.YAxis.IsReverse = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Reversed?" ) );

			testee.YAxis.IsReverse = false;
			testee.YAxis.Type = AxisType.Log;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y Axis Log?" ) );

			testee.YAxis.Type = AxisType.Linear;

			///////////////////////////////////////////////////////////////////////////////

			/////////  Y2 AXIS /////////////////////////////////////////////////////////////////////////

			testee.Y2Axis.IsVisible = true;
			testee.Y2Axis.Color = Color.Red;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Visible?" ) );

			testee.Y2Axis.Title = "Y2 Axis Title";

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Title Visible?" ) );

			//testee.Y2Axis.TicPenWidth = 3.0F;
			testee.Y2Axis.IsZeroLine = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis ZeroLine Visible?" ) );

			testee.Y2Axis.IsZeroLine = false;
			//testee.Y2Axis.TicPenWidth = 1.0F;
			testee.Y2Axis.IsTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis major tics Visible?" ) );

			testee.Y2Axis.IsMinorTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis minor tics Visible?" ) );

			testee.Y2Axis.IsInsideTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Inside tics Visible?" ) );

			testee.Y2Axis.IsOppositeTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Opposite tics Visible?" ) );

			testee.Y2Axis.IsMinorInsideTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Minor Inside tics Visible?" ) );

			testee.Y2Axis.IsMinorOppositeTic = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Minor Opposite tics Visible?" ) );

			testee.Y2Axis.TicPenWidth = 1.0F;
			testee.Y2Axis.Color = Color.Black;
			testee.Y2Axis.IsShowGrid = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Grid Visible?" ) );

			testee.Y2Axis.GridPenWidth = 1.0F;
			testee.Y2Axis.GridColor = Color.Black;
			testee.Y2Axis.IsReverse = true;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Reversed?" ) );

			testee.Y2Axis.IsReverse = false;
			testee.Y2Axis.Type = AxisType.Log;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked( "Y2 Axis Log?" ) );

			testee.Y2Axis.Type = AxisType.Linear;

			///////////////////////////////////////////////////////////////////////////////

			for ( float angle=0.0F; angle<=360.0F; angle+= 10.0F )
			{
				testee.XAxis.ScaleFontSpec.Angle = angle;
				testee.YAxis.ScaleFontSpec.Angle = -angle + 90.0F;
				testee.Y2Axis.ScaleFontSpec.Angle = -angle - 90.0F;
				//testee.XAxis.TitleFontSpec.Angle = -angle;
				//testee.YAxis.TitleFontSpec.Angle = angle + 180.0F;
				//testee.Y2Axis.TitleFontSpec.Angle = angle;
				//testee.Legend.FontSpec.Angle = angle;
				//testee.FontSpec.Angle = angle;
				
				form2.Refresh();
				TestUtils.DelaySeconds( 50 );
			}

			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Did Fonts Rotate & Axes Accomodate them?" ) );
					
			testee.XAxis.ScaleFontSpec.Angle = 0;
			testee.YAxis.ScaleFontSpec.Angle = 90.0F;
			testee.Y2Axis.ScaleFontSpec.Angle = -90.0F;
			
			for ( float angle=0.0F; angle<=360.0F; angle+= 10.0F )
			{
				testee.XAxis.TitleFontSpec.Angle = -angle;
				testee.YAxis.TitleFontSpec.Angle = angle + 180.0F;
				testee.Y2Axis.TitleFontSpec.Angle = angle;
				//testee.Legend.FontSpec.Angle = angle;
				testee.FontSpec.Angle = angle;
				
				form2.Refresh();
				TestUtils.DelaySeconds( 50 );
			}

			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Did Axis Titles Rotate and the AxisRect adjust properly?" ) );
					
			testee.XAxis.ScaleFontSpec.Angle = 0;
			testee.YAxis.ScaleFontSpec.Angle = 90.0F;
			testee.Y2Axis.ScaleFontSpec.Angle = -90.0F;
			testee.XAxis.TitleFontSpec.Angle = 0;
			testee.YAxis.TitleFontSpec.Angle = 180.0F;
			testee.Y2Axis.TitleFontSpec.Angle = 0;
			//testee.Legend.FontSpec.Angle = 0;
			testee.FontSpec.Angle = 0;

			///////////////////////////////////////////////////////////////////////////////

			TextItem text = new TextItem( "ZedGraph TextItem", 0.5F, 0.5F );
			testee.TextList.Add( text );
			
			text.CoordinateFrame = CoordType.AxisFraction;
			text.FontSpec.IsItalic = false;
			text.FontSpec.IsUnderline = false;
			text.FontSpec.Angle = 0.0F;
			text.FontSpec.IsFramed = false;
			text.FontSpec.Fill.Type = FillType.None;
			text.FontSpec.FontColor = Color.Red;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Is TextItem Centered on Graph?" ) );
			
			text.FontSpec.FontColor = Color.Black;
			text.FontSpec.IsFramed = true;
			text.FontSpec.FrameWidth = 3.0F;
			text.FontSpec.FrameColor = Color.Red;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Does TextItem have a Frame?" ) );
			
			text.FontSpec.FrameWidth = 1.0F;
			text.FontSpec.FrameColor = Color.Black;
			
			text.FontSpec.Fill.Color = Color.LightGoldenrodYellow;
			text.FontSpec.Fill.Type = FillType.Brush;

			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Is TextItem background filled?" ) );
			
			text.FontSpec.Size = 20.0F;
			text.FontSpec.Family = "Garamond";
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Large Garamond Font?" ) );
			
			text.FontSpec.IsUnderline = true;
			text.FontSpec.IsItalic = true;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Text Underlined & italic?" ) );
			
			text.FontSpec.IsItalic = false;
			text.FontSpec.IsUnderline = false;
			
			text.X = 75.0F;
			text.Y = 0.0F;
			text.CoordinateFrame = CoordType.AxisXYScale;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Centered at (75, 0.0)?" ) );
			
			text.AlignH = AlignH.Right;
			text.AlignV = AlignV.Top;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Top-Right at (75, 0.0)?" ) );
			
			text.AlignH = AlignH.Left;
			text.AlignV = AlignV.Bottom;
			
			form2.Refresh();
			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Bottom-Left at (75, 0.0)?" ) );
					
			for ( float angle=0.0F; angle<=360.0F; angle+= 10.0F )
			{
				text.FontSpec.Angle = angle;
				
				form2.Refresh();
				TestUtils.DelaySeconds( 50 );
			}

			Assert.IsTrue( TestUtils.promptIfTestWorked(
				"Text Rotate with Bottom-Left at (75, 0.5)?" ) );
				
			testee.PaneFill.Type = FillType.Brush;
			testee.AxisFill.Type = FillType.Brush;
			testee.Legend.Fill.Type = FillType.Brush;

			for ( float angle=0.0F; angle<=360.0F; angle+= 10.0F )
			{
				testee.PaneFill.Brush = new LinearGradientBrush( testee.PaneRect, Color.White,
					Color.Red, angle, true );
				testee.AxisFill.Brush = new LinearGradientBrush( testee.AxisRect, Color.White,
					Color.Blue, -angle, true );
				testee.Legend.Fill.Brush = new LinearGradientBrush( testee.Legend.Rect, Color.White,
					Color.Green, -angle, true );
				
				form2.Refresh();
				TestUtils.DelaySeconds( 50 );
			}

			TestUtils.waitForUserOK = userOK;

			Assert.IsTrue(  TestUtils.promptIfTestWorked(
				"Did Everything look ok?" ) );
		}
		#endregion
	}
	#endregion

}
