using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ZedGraph;

namespace ZedGraph.ControlTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private TabControl tabControl1;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private TabPage tabPage3;
		private TabPage tabPage4;
		private Label label1;
		private ZedGraphControl zedGraphControl6;
		private ZedGraphControl zedGraphControl4;
		private ZedGraphControl zedGraphControl5;
		private PropertyGrid propertyGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.zedGraphControl4 = new ZedGraph.ZedGraphControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.zedGraphControl6 = new ZedGraph.ZedGraphControl();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.zedGraphControl5 = new ZedGraph.ZedGraphControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(424, 336);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
			this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.zedGraphControl4);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(416, 310);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// zedGraphControl4
			// 
			this.zedGraphControl4.IsEnableHPan = true;
			this.zedGraphControl4.IsEnableVPan = true;
			this.zedGraphControl4.IsEnableZoom = true;
			this.zedGraphControl4.IsShowContextMenu = true;
			this.zedGraphControl4.IsShowHScrollBar = false;
			this.zedGraphControl4.IsShowPointValues = false;
			this.zedGraphControl4.IsShowVScrollBar = false;
			this.zedGraphControl4.IsZoomOnMouseCenter = false;
			this.zedGraphControl4.Location = new System.Drawing.Point(8, 8);
			this.zedGraphControl4.Name = "zedGraphControl4";
			this.zedGraphControl4.PointDateFormat = "g";
			this.zedGraphControl4.PointValueFormat = "G";
			this.zedGraphControl4.ScrollMaxX = 0;
			this.zedGraphControl4.ScrollMaxY = 0;
			this.zedGraphControl4.ScrollMinX = 0;
			this.zedGraphControl4.ScrollMinY = 0;
			this.zedGraphControl4.Size = new System.Drawing.Size(400, 296);
			this.zedGraphControl4.TabIndex = 0;
			this.zedGraphControl4.ZoomStepFraction = 0.1;
			this.zedGraphControl4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl4_MouseUp);
			this.zedGraphControl4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl4_MouseMove);
			this.zedGraphControl4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl4_MouseDown);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.zedGraphControl6);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(416, 310);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPage1_Paint);
			// 
			// zedGraphControl6
			// 
			this.zedGraphControl6.IsEnableHPan = true;
			this.zedGraphControl6.IsEnableVPan = true;
			this.zedGraphControl6.IsEnableZoom = true;
			this.zedGraphControl6.IsShowContextMenu = true;
			this.zedGraphControl6.IsShowHScrollBar = false;
			this.zedGraphControl6.IsShowPointValues = false;
			this.zedGraphControl6.IsShowVScrollBar = false;
			this.zedGraphControl6.IsZoomOnMouseCenter = false;
			this.zedGraphControl6.Location = new System.Drawing.Point(8, 8);
			this.zedGraphControl6.Name = "zedGraphControl6";
			this.zedGraphControl6.PointDateFormat = "g";
			this.zedGraphControl6.PointValueFormat = "G";
			this.zedGraphControl6.ScrollMaxX = 0;
			this.zedGraphControl6.ScrollMaxY = 0;
			this.zedGraphControl6.ScrollMinX = 0;
			this.zedGraphControl6.ScrollMinY = 0;
			this.zedGraphControl6.Size = new System.Drawing.Size(400, 296);
			this.zedGraphControl6.TabIndex = 1;
			this.zedGraphControl6.ZoomStepFraction = 0.1;
			this.zedGraphControl6.Paint += new System.Windows.Forms.PaintEventHandler(this.zedGraphControl6_Paint);
			this.zedGraphControl6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl6_MouseDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(152, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Here\'s Tab Page 1";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.zedGraphControl5);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(416, 310);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "tabPage3";
			// 
			// zedGraphControl5
			// 
			this.zedGraphControl5.IsEnableHPan = true;
			this.zedGraphControl5.IsEnableVPan = true;
			this.zedGraphControl5.IsEnableZoom = true;
			this.zedGraphControl5.IsShowContextMenu = true;
			this.zedGraphControl5.IsShowHScrollBar = false;
			this.zedGraphControl5.IsShowPointValues = false;
			this.zedGraphControl5.IsShowVScrollBar = false;
			this.zedGraphControl5.IsZoomOnMouseCenter = false;
			this.zedGraphControl5.Location = new System.Drawing.Point(8, 8);
			this.zedGraphControl5.Name = "zedGraphControl5";
			this.zedGraphControl5.PointDateFormat = "g";
			this.zedGraphControl5.PointValueFormat = "G";
			this.zedGraphControl5.ScrollMaxX = 0;
			this.zedGraphControl5.ScrollMaxY = 0;
			this.zedGraphControl5.ScrollMinX = 0;
			this.zedGraphControl5.ScrollMinY = 0;
			this.zedGraphControl5.Size = new System.Drawing.Size(400, 296);
			this.zedGraphControl5.TabIndex = 0;
			this.zedGraphControl5.ZoomStepFraction = 0.1;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(416, 310);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(432, 24);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(424, 504);
			this.propertyGrid1.TabIndex = 2;
			this.propertyGrid1.Text = "ZedGraph GraphPane Properties";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(864, 534);
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run( new Form1() );
		}

		private void Form1_Load( object sender, System.EventArgs e )
		{			
			zedGraphControl4.GraphPane.Title = "Test Graph for Tab 2";
			zedGraphControl5.GraphPane.Title = "Test Graph for Tab 3";
			zedGraphControl6.GraphPane.Title = "Test Graph for Tab 1";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();

			zedGraphControl4.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler( poop );

			for( int i = 0; i < 18; i++ )
			{
				x = new XDate( 1995, i, i+5, i, i*2, i*3 );
				//x = (double) i;
				
				y1 = (Math.Sin( i / 9.0 * Math.PI ) + 1.0 ) * 1000.0;
				y2 = Math.Cos( i / 9.0 * Math.PI );
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			list2[0].Tag = "hello";

			GraphPane testPane = new GraphPane( new RectangleF( 0, 0, 100, 100 ), "Second Test Pane", "X", "Y" );
			testPane.AddCurve( "Another", list1, Color.Green, SymbolType.Diamond );

			LineItem myCurve = zedGraphControl4.GraphPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = zedGraphControl4.GraphPane.AddCurve("Cos", list2, Color.Blue, SymbolType.Circle);
			myCurve2.IsY2Axis = true;
			//			LineItem myCurve2 = zedGraphControl5.GraphPane.AddCurve("Cosine", list3, Color.Blue, SymbolType.Circle);
			LineItem myCurve3 = zedGraphControl6.GraphPane.AddCurve("Sine", list1, Color.Blue, SymbolType.Circle);
			myCurve3.Line.StepType = StepType.ForwardStep;
			
			//zedGraphControl4.GraphPane.YAxis.Type = AxisType.Log;
			//zedGraphControl4.GraphPane.YAxis.IsReverse = true;
			//zedGraphControl6.IsShowPointValues = true;
			//zedGraphControl6.PointDateFormat = "hh:MM:ss";
			//zedGraphControl6.PointValueFormat = "f4";

			double[] xx = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			double[] yy = { 1, 2, 3, 4, 5, 4, 3, 2, 1, 2 };
			double[] zz = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
			PointPairList list = new PointPairList( xx, yy, zz );

			Color[] colors = { Color.Red, Color.Green, Color.Blue,
								Color.Yellow, Color.Orange };
			Fill fill = new Fill( colors );
			fill.Type = FillType.GradientByZ;
			fill.RangeMin = 1;
			fill.RangeMax = 5;

			BarItem myBar = zedGraphControl5.GraphPane.AddBar( "My Bar", list, Color.Tomato );
			myBar.Bar.Fill = fill;
			zedGraphControl5.GraphPane.XAxis.Type = AxisType.Ordinal;

			zedGraphControl5.PointValueEvent += new ZedGraph.ZedGraphControl.PointValueHandler( MyPointValueHandler );
			zedGraphControl5.IsShowPointValues = true;

			//zedGraphControl4.GraphPane.XAxis.ScaleFormat = "n1";
			//zedGraphControl4.GraphPane.XAxis.ScaleMag = 0;
			//zedGraphControl4.GraphPane.XAxis.Type = AxisType.Date;
			//zedGraphControl4.GraphPane.YAxis.Max = 2499.9;
			//zedGraphControl4.GraphPane.YAxis.IsScaleVisible = false;
			//zedGraphControl4.GraphPane.YAxis.IsTic = false;
			//zedGraphControl4.GraphPane.YAxis.IsMinorTic = false;


			zedGraphControl4.GraphPane.Y2Axis.IsVisible = true;
			//zedGraphControl4.GraphPane.Y2Axis.IsInsideTic = false;
			//zedGraphControl4.GraphPane.Y2Axis.IsMinorInsideTic = false;
			//zedGraphControl4.GraphPane.Y2Axis.BaseTic = 500;
			//zedGraphControl4.GraphPane.Y2Axis.Max = 2499.9;
			//zedGraphControl4.GraphPane.Y2Axis.Cross = 34601;
			//zedGraphControl4.GraphPane.Y2Axis.IsAxisSegmentVisible = false;
			//zedGraphControl4.GraphPane.YAxis.Is

			zedGraphControl4.AxisChange();
			zedGraphControl5.AxisChange();

			zedGraphControl4.IsEnableVPan = false;
			zedGraphControl4.IsShowHScrollBar = true;
			zedGraphControl4.ScrollMinX = 33000;
			zedGraphControl4.ScrollMaxX = 37000;

			zedGraphControl4.IsShowVScrollBar = true;
			zedGraphControl4.ScrollMinY = -5000;
			zedGraphControl4.ScrollMaxY = 5000;

			zedGraphControl4.ScrollMinY2 = -2;
			zedGraphControl4.ScrollMaxY2 = 2;
			zedGraphControl4.IsScrollY2 = true;

			this.zedGraphControl6.GraphPane.Title = "ZedgroSoft, International\nHi-Low-Close Daily Stock Chart"; 
 
			this.zedGraphControl6.GraphPane.XAxis.Title = ""; 
			this.zedGraphControl6.GraphPane.YAxis.Title = "Trading Price, $US"; 
			this.zedGraphControl6.GraphPane.FontSpec.Family = "Arial"; 
			this.zedGraphControl6.GraphPane.FontSpec.IsItalic = true; 
			this.zedGraphControl6.GraphPane.FontSpec.Size = 18; 
 
			PointPairList hList = new PointPairList(); 
 
			PointPairList cList = new PointPairList(); 
			Random rand = new Random(); 
 
			double x0 = 10; 
			double hi0 = 5; 
			double low0 = 6; 
 
			hList.Add( x0, hi0, low0 ); 
 
			HiLowBarItem myCurve0 = this.zedGraphControl6.GraphPane.AddHiLowBar( "Price Range", hList,Color.Blue ); 
 
			this.zedGraphControl6.GraphPane.XAxis.Type = AxisType.Linear; 
			this.zedGraphControl6.GraphPane.XAxis.ScaleFontSpec.Angle = 65 ; 
 
			this.zedGraphControl6.GraphPane.XAxis.ScaleFontSpec.IsBold = true ; 
			this.zedGraphControl6.GraphPane.XAxis.ScaleFontSpec.Size = 12 ; 
 
			this.zedGraphControl6.GraphPane.XAxis.Min = 0; 
			this.zedGraphControl6.GraphPane.XAxis.Max= 20; 
 
			this.zedGraphControl6.GraphPane.YAxis.IsShowGrid = true ; 
 
			this.zedGraphControl6.GraphPane.YAxis.MinorStep = 0.5; 
 
			this.zedGraphControl6.GraphPane.AxisFill = new Fill( Color.White,Color.FromArgb( 255, 255, 166), 90F ); 
  
			this.zedGraphControl6.AxisChange(); 
			this.zedGraphControl6.IsShowPointValues = true;
 

			SetSize();
			
			propertyGrid1.SelectedObject = zedGraphControl4.GraphPane;
		}

		private string MyPointValueHandler( object sender, GraphPane pane, CurveItem curve, int iPt )
		{
			PointPair pt = curve[iPt];
			return "This value is " + pt.Y.ToString() + " gallons";
		}

		private void poop( object sender, ContextMenu menu )
		{
			MenuItem menuItem;
			int index = menu.MenuItems.Count;

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "My New Item";
			menu.MenuItems.Add( menuItem );
			//menuItem.Click += new System.EventHandler( this.MenuClick_Copy );

			//menu.MenuItems.RemoveAt(2);
		}

		private void tabPage1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void zedGraphControl6_Paint(object sender, PaintEventArgs e)
		{
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			SetSize();
		}

		private void SetSize()
		{
			Size size = new Size( this.Size.Width - tabControl1.Left - 10,
									this.Size.Height - tabControl1.Top - 40 );
			tabControl1.Size = size;
			zedGraphControl6.Size = new Size( size.Width - zedGraphControl6.Left - 20,
												size.Height - zedGraphControl6.Top - 30 );
		}

		private void zedGraphControl6_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
		}

		PointF		startPt;
		double		startX, startY, startY2;
		bool		isDragPoint = false;
		CurveItem	dragCurve;
		int			dragIndex;
		PointPair	startPair;

		private void zedGraphControl4_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( Control.ModifierKeys == Keys.Alt )
			{
				GraphPane myPane = zedGraphControl4.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );

				if ( myPane.FindNearestPoint( mousePt, out dragCurve, out dragIndex ) )
				{
					startPt = mousePt;
					startPair = dragCurve.Points[dragIndex];
					isDragPoint = true;
					myPane.ReverseTransform( mousePt, out startX, out startY, out startY2 );
				}
			}

			/*
			if ( zedGraphControl4.GraphPane.YAxis.Type == AxisType.Linear )
				zedGraphControl4.GraphPane.YAxis.Type = AxisType.Log;
			else
				zedGraphControl4.GraphPane.YAxis.Type = AxisType.Linear;

			zedGraphControl4.AxisChange();
			Refresh();
			*/

			/*
			double rangeX = zedGraphControl4.GraphPane.XAxis.Max - zedGraphControl4.GraphPane.XAxis.Min;
			zedGraphControl4.GraphPane.XAxis.Max -= rangeX/20.0;
			zedGraphControl4.GraphPane.XAxis.Min += rangeX/20.0;
			double rangeY = zedGraphControl4.GraphPane.YAxis.Max - zedGraphControl4.GraphPane.YAxis.Min;
			zedGraphControl4.GraphPane.YAxis.Max -= rangeY/20.0;
			zedGraphControl4.GraphPane.YAxis.Min += rangeY/20.0;
			zedGraphControl4.AxisChange();
			zedGraphControl4.Refresh();
			//Invalidate();
			*/
		}

		private void zedGraphControl4_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( isDragPoint )
			{
				// move the point
				double curX, curY, curY2;
				GraphPane myPane = zedGraphControl4.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );
				myPane.ReverseTransform( mousePt, out curX, out curY, out curY2 );
				PointPair newPt = new PointPair( startPair.X + curX - startX, startPair.Y + curY - startY );
				dragCurve.Points[dragIndex] = newPt;
				zedGraphControl4.Refresh();
			}
		}

		private void zedGraphControl4_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( isDragPoint )
			{
				// finalize the move
				isDragPoint = false;
			}
		}

		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			//clone the pane so the paneRect can be changed for printing
			//PaneBase printPane = (PaneBase) master.Clone();
			GraphPane printPane = (GraphPane) zedGraphControl4.GraphPane.Clone();
			printPane.PaneRect = new RectangleF( 50, 50, 400, 300 );

			//printPane.Legend.IsVisible = true;
			//printPane.PaneRect = new RectangleF( 50, 50, 300, 300 );
			//printPane.ReSize( e.Graphics, new RectangleF( 50, 50, 300, 300 ) );
				
			//e.Graphics.PageScale = 1.0F;
			//printPane.BaseDimension = 2.0F;
			printPane.Draw( e.Graphics );
		}

		private void DoPrint()
		{
			PrintDocument pd = new PrintDocument();
			//PrintPreviewDialog ppd = new PrintPreviewDialog();
			pd.PrintPage += new PrintPageEventHandler( Graph_PrintPage );
			//ppd.Document = pd;
			//ppd.Show();
			pd.Print();
		}

		private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}

		private void tabControl1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
		}

		private void tabControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//MessageBox.Show( "Howdy" );
			DoPrint();
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			Refresh();
		}

	}
}