using System;
using System.Drawing;
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
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label1;
		private ZedGraphControl zedGraphControl6;
		private ZedGraphControl zedGraphControl4;
		private ZedGraphControl zedGraphControl5;
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
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.zedGraphControl6 = new ZedGraph.ZedGraphControl();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.zedGraphControl4 = new ZedGraph.ZedGraphControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.zedGraphControl5 = new ZedGraph.ZedGraphControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(536, 336);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.zedGraphControl6);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(528, 310);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPage1_Paint);
			// 
			// zedGraphControl6
			// 
			this.zedGraphControl6.IsShowPointValues = false;
			this.zedGraphControl6.Location = new System.Drawing.Point(24, 63);
			this.zedGraphControl6.Name = "zedGraphControl6";
			this.zedGraphControl6.PointDateFormat = "g";
			this.zedGraphControl6.PointValueFormat = "G";
			this.zedGraphControl6.Size = new System.Drawing.Size(461, 209);
			this.zedGraphControl6.TabIndex = 1;
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
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.zedGraphControl4);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(528, 310);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// zedGraphControl4
			// 
			this.zedGraphControl4.IsShowPointValues = false;
			this.zedGraphControl4.Location = new System.Drawing.Point(24, 25);
			this.zedGraphControl4.Name = "zedGraphControl4";
			this.zedGraphControl4.PointDateFormat = "g";
			this.zedGraphControl4.PointValueFormat = "G";
			this.zedGraphControl4.Size = new System.Drawing.Size(474, 247);
			this.zedGraphControl4.TabIndex = 0;
			this.zedGraphControl4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl4_MouseDown);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.zedGraphControl5);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(528, 310);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "tabPage3";
			// 
			// zedGraphControl5
			// 
			this.zedGraphControl5.IsShowPointValues = false;
			this.zedGraphControl5.Location = new System.Drawing.Point(64, 40);
			this.zedGraphControl5.Name = "zedGraphControl5";
			this.zedGraphControl5.PointDateFormat = "g";
			this.zedGraphControl5.PointValueFormat = "G";
			this.zedGraphControl5.Size = new System.Drawing.Size(328, 224);
			this.zedGraphControl5.TabIndex = 0;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(528, 310);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 414);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
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
			//XDate junk = new XDate( 1, 1, 1 );
			//junk.AddDays( -1 );
			//int year, month, day;
			//junk.GetDate( out year, out month, out day );
			//MessageBox.Show( year + "/" + month + "/" + day );
			//MessageBox.Show( junk.JulianDay + " " + junk.XLDate );

			zedGraphControl4.GraphPane.Title = "Test Graph for Tab 2";
			zedGraphControl5.GraphPane.Title = "Test Graph for Tab 3";
			zedGraphControl6.GraphPane.Title = "Test Graph for Tab 1";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			for( int i = 0; i < 18; i++ )
			{
				//x = new XDate( -325, i, i+5, i, i*2, i*3 );
				x = (double) i;
				
				y1 = Math.Sin( i / 9.0 * Math.PI );
				y2 = Math.Cos( i / 9.0 * Math.PI );
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			GraphPane testPane = new GraphPane( new RectangleF( 0, 0, 100, 100 ), "Second Test Pane", "X", "Y" );
			testPane.AddCurve( "Another", list1, Color.Green, SymbolType.Diamond );
			zedGraphControl6.MasterPane.Add( testPane );
			zedGraphControl6.MasterPane.Add( (GraphPane) testPane.Clone() );
			zedGraphControl6.MasterPane.Add( (GraphPane) testPane.Clone() );

			LineItem myCurve = zedGraphControl4.GraphPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = zedGraphControl5.GraphPane.AddCurve("Cosine", list2, Color.Blue, SymbolType.Circle);
			LineItem myCurve3 = zedGraphControl6.GraphPane.AddCurve("Sine", list1, Color.Blue, SymbolType.Circle);
			LineItem myCurve4 = zedGraphControl6.GraphPane.AddCurve("Cosine", list2, Color.Red, SymbolType.Circle);
			myCurve3.Line.StepType = StepType.ForwardStep;
			myCurve4.Line.StepType = StepType.RearwardStep;
			
			zedGraphControl4.GraphPane.XAxis.IsReverse = true;
			zedGraphControl4.GraphPane.YAxis.IsReverse = true;
			zedGraphControl6.GraphPane.AxisBorder.IsVisible = false;
			zedGraphControl6.GraphPane.XAxis.Type = AxisType.Date;
			zedGraphControl6.IsShowPointValues = true;
			zedGraphControl6.PointDateFormat = "hh:MM";
			zedGraphControl6.PointValueFormat = "f4";
			zedGraphControl4.AxisChange();
			zedGraphControl5.AxisChange();
			zedGraphControl6.AxisChange();

			SetSize();
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

		private void zedGraphControl4_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
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
	}
}