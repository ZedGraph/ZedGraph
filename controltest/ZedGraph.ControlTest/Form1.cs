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
		private ZedGraph.ZedGraphControl zedGraphControl2;
		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.Label label1;
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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.label1 = new System.Windows.Forms.Label();
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
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(528, 310);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.zedGraphControl1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(528, 310);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.zedGraphControl2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(528, 310);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "tabPage3";
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(528, 310);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// zedGraphControl2
			// 
			this.zedGraphControl2.IsShowPointValues = false;
			this.zedGraphControl2.Location = new System.Drawing.Point(0, -1);
			this.zedGraphControl2.Name = "zedGraphControl2";
			this.zedGraphControl2.PointValueFormat = "G";
			this.zedGraphControl2.Size = new System.Drawing.Size(528, 312);
			this.zedGraphControl2.TabIndex = 2;
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.IsShowPointValues = false;
			this.zedGraphControl1.Location = new System.Drawing.Point(0, -1);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.PointValueFormat = "G";
			this.zedGraphControl1.Size = new System.Drawing.Size(528, 312);
			this.zedGraphControl1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(176, 104);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Here\'s Tab Page 1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 414);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
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
			zedGraphControl1.GraphPane.Title = "Test Graph for Tab 2";
			zedGraphControl2.GraphPane.Title = "Test Graph for Tab 3";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			for( int i = 0; i < 36; i++ )
			{
				x = i;
				y1 = Math.Sin( i / 18.0 * Math.PI );
				y2 = Math.Cos( i / 18.0 * Math.PI );
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			LineItem myCurve = zedGraphControl1.GraphPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = zedGraphControl2.GraphPane.AddCurve("Cosine", list2, Color.Blue, SymbolType.Circle);
			
			zedGraphControl1.GraphPane.AxisChange( this.CreateGraphics() );
			zedGraphControl2.GraphPane.AxisChange( this.CreateGraphics() );
			zedGraphControl1.Invalidate();
			zedGraphControl2.Invalidate();
		}
	}
}