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
		private PropertyGrid propertyGrid1;
		private ZedGraphControl zedGraphControl1;
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point( 593, 12 );
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size( 259, 432 );
			this.propertyGrid1.TabIndex = 2;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler( this.propertyGrid1_PropertyValueChanged );
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.IsEnableHPan = true;
			this.zedGraphControl1.IsEnableVPan = true;
			this.zedGraphControl1.IsEnableZoom = true;
			this.zedGraphControl1.IsScrollY2 = false;
			this.zedGraphControl1.IsShowContextMenu = true;
			this.zedGraphControl1.IsShowHScrollBar = false;
			this.zedGraphControl1.IsShowPointValues = false;
			this.zedGraphControl1.IsShowVScrollBar = false;
			this.zedGraphControl1.IsZoomOnMouseCenter = false;
			this.zedGraphControl1.Location = new System.Drawing.Point( 12, 12 );
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.PanButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			this.zedGraphControl1.PanModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None ) ) );
			this.zedGraphControl1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.PointDateFormat = "g";
			this.zedGraphControl1.PointValueFormat = "G";
			this.zedGraphControl1.ScrollMaxX = 0;
			this.zedGraphControl1.ScrollMaxY = 0;
			this.zedGraphControl1.ScrollMaxY2 = 0;
			this.zedGraphControl1.ScrollMinX = 0;
			this.zedGraphControl1.ScrollMinY = 0;
			this.zedGraphControl1.ScrollMinY2 = 0;
			this.zedGraphControl1.Size = new System.Drawing.Size( 567, 432 );
			this.zedGraphControl1.TabIndex = 3;
			this.zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomStepFraction = 0.1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 862, 461 );
			this.Controls.Add( this.zedGraphControl1 );
			this.Controls.Add( this.propertyGrid1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler( this.Form1_Load );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );
			this.Resize += new System.EventHandler( this.Form1_Resize );
			this.KeyDown += new System.Windows.Forms.KeyEventHandler( this.Form1_KeyDown );
			this.ResumeLayout( false );

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
			GraphPane myPane = zedGraphControl1.GraphPane;

			myPane.Title = "Test Graph";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			zedGraphControl1.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler( MyContextMenuHandler );

			for( int i = 0; i < 18; i++ )
			{
				x = new XDate( 1995, i, i+5, i, i*2, i*3 );
				//x = (double) i;
				
				y1 = (Math.Sin( i / 9.0 * Math.PI ) + 1.1 ) * 1000.0;
				y2 = Math.Cos( i / 9.0 * Math.PI ) + 1.1;
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			LineItem myCurve = myPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = myPane.AddCurve("Cos", list2, Color.Blue, SymbolType.Circle);
			//myCurve2.IsY2Axis = true;
			
			myPane.YAxis.Type = AxisType.Log;
			//myPane.YAxis.IsReverse = true;
			//zedGraphControl6.IsShowPointValues = true;
			//zedGraphControl6.PointDateFormat = "hh:MM:ss";
			//zedGraphControl6.PointValueFormat = "f4";

			//double[] xx = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			//double[] yy = { 1, 2, 3, 4, 5, 4, 3, 2, 1, 2 };
			//double[] zz = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
			//PointPairList list = new PointPairList( xx, yy, zz );

			//Color[] colors = { Color.Red, Color.Green, Color.Blue,
			//					Color.Yellow, Color.Orange };
			//Fill fill = new Fill( colors );
			//fill.Type = FillType.GradientByZ;
			//fill.RangeMin = 1;
			//fill.RangeMax = 5;

			//myPane.XAxis.ScaleFormat = "n1";
			//myPane.XAxis.ScaleMag = 0;
			//myPane.XAxis.Type = AxisType.Date;
			//myPane.YAxis.Max = 2499.9;
			//myPane.YAxis.IsScaleVisible = false;
			//myPane.YAxis.IsTic = false;
			//myPane.YAxis.IsMinorTic = false;


			//myPane.Y2Axis.IsVisible = true;
			//myPane.Y2Axis.IsInsideTic = false;
			//myPane.Y2Axis.IsMinorInsideTic = false;
			//myPane.Y2Axis.BaseTic = 500;
			//myPane.Y2Axis.Max = 2499.9;
			//myPane.Y2Axis.Cross = 34601;
			//myPane.Y2Axis.IsAxisSegmentVisible = false;
			//myPane.YAxis.Is

			zedGraphControl1.IsEnableVPan = false;
			zedGraphControl1.IsShowHScrollBar = true;
			zedGraphControl1.ScrollMinX = 33000;
			zedGraphControl1.ScrollMaxX = 37000;

			zedGraphControl1.IsShowVScrollBar = true;
			zedGraphControl1.ScrollMinY = -5000;
			zedGraphControl1.ScrollMaxY = 5000;

			zedGraphControl1.ScrollMinY2 = -2;
			zedGraphControl1.ScrollMaxY2 = 2;
			zedGraphControl1.IsScrollY2 = true; 

			zedGraphControl1.AxisChange();

			SetSize();
			
			propertyGrid1.SelectedObject = myPane;
		}

		private string MyPointValueHandler( object sender, GraphPane pane, CurveItem curve, int iPt )
		{
			PointPair pt = curve[iPt];
			return "This value is " + pt.Y.ToString() + " gallons";
		}

		private void MyContextMenuHandler( object sender, ContextMenu menu )
		{
			//MenuItem menuItem = menu.MenuItems..Find( "Set Scale to Default", false );

			
			foreach( MenuItem item in menu.MenuItems )
			{
				if ( item.Text == "Set Scale to Default" )
				{
					menu.MenuItems.Remove( item );
					break;
				}
			}
			

			//menu.MenuItems.RemoveByKey( "Set Scale to Default" );

			//menu.MenuItems.RemoveAt( 5 );

			/*
			MenuItem menuItem;
			int index = menu.MenuItems.Count;

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "My New Item";
			menu.MenuItems.Add( menuItem );
			//menuItem.Click += new System.EventHandler( this.MenuClick_Copy );

			//menu.MenuItems.RemoveAt(2);
			 */
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			SetSize();
		}

		private void SetSize()
		{
			Size size = new Size( this.Size.Width - propertyGrid1.Width - zedGraphControl1.Left - 20,
									this.Size.Height - zedGraphControl1.Top - 40 );
			zedGraphControl1.Size = size;
			propertyGrid1.Left = this.Size.Width - 10 - propertyGrid1.Width;
			propertyGrid1.Height = Size.Height - 50;
		}

		PointF		startPt;
		double		startX, startY, startY2;
		bool		isDragPoint = false;
		CurveItem	dragCurve;
		int			dragIndex;
		PointPair	startPair;

		private void zedGraphControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( Control.ModifierKeys == Keys.Alt )
			{
				GraphPane myPane = zedGraphControl1.GraphPane;
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
			if ( myPane.YAxis.Type == AxisType.Linear )
				myPane.YAxis.Type = AxisType.Log;
			else
				myPane.YAxis.Type = AxisType.Linear;

			zedGraphControl4.AxisChange();
			Refresh();
			*/

			/*
			double rangeX = myPane.XAxis.Max - myPane.XAxis.Min;
			myPane.XAxis.Max -= rangeX/20.0;
			myPane.XAxis.Min += rangeX/20.0;
			double rangeY = myPane.YAxis.Max - myPane.YAxis.Min;
			myPane.YAxis.Max -= rangeY/20.0;
			myPane.YAxis.Min += rangeY/20.0;
			zedGraphControl4.AxisChange();
			zedGraphControl4.Refresh();
			//Invalidate();
			*/
		}

		private void zedGraphControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( isDragPoint )
			{
				// move the point
				double curX, curY, curY2;
				GraphPane myPane = zedGraphControl1.GraphPane;
				PointF mousePt = new PointF( e.X, e.Y );
				myPane.ReverseTransform( mousePt, out curX, out curY, out curY2 );
				PointPair newPt = new PointPair( startPair.X + curX - startX, startPair.Y + curY - startY );
				dragCurve.Points[dragIndex] = newPt;
				zedGraphControl1.Refresh();
			}
		}

		private void zedGraphControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
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
			GraphPane printPane = (GraphPane) zedGraphControl1.GraphPane.Clone();
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

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//MessageBox.Show( "Howdy" );
			//DoPrint();
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			Refresh();
		}

	}
}