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
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.zedGraphControl6 = new ZedGraph.ZedGraphControl();
			this.zedGraphControl4 = new ZedGraph.ZedGraphControl();
			this.zedGraphControl5 = new ZedGraph.ZedGraphControl();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
// 
// tabControl1
// 
			this.tabControl1.Controls.Add( this.tabPage1 );
			this.tabControl1.Controls.Add( this.tabPage2 );
			this.tabControl1.Controls.Add( this.tabPage3 );
			this.tabControl1.Controls.Add( this.tabPage4 );
			this.tabControl1.Location = new System.Drawing.Point( 0, 0 );
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size( 536, 336 );
			this.tabControl1.TabIndex = 1;
// 
// tabPage1
// 
			this.tabPage1.Controls.Add( this.zedGraphControl6 );
			this.tabPage1.Controls.Add( this.label1 );
			this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size( 528, 310 );
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.Paint += new System.Windows.Forms.PaintEventHandler( this.tabPage1_Paint );
// 
// label1
// 
			this.label1.Location = new System.Drawing.Point( 152, 16 );
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Here\'s Tab Page 1";
// 
// tabPage2
// 
			this.tabPage2.Controls.Add( this.zedGraphControl4 );
			this.tabPage2.Location = new System.Drawing.Point( 4, 22 );
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size( 528, 310 );
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
// 
// tabPage3
// 
			this.tabPage3.Controls.Add( this.zedGraphControl5 );
			this.tabPage3.Location = new System.Drawing.Point( 4, 22 );
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size( 528, 310 );
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "tabPage3";
// 
// tabPage4
// 
			this.tabPage4.Location = new System.Drawing.Point( 4, 22 );
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size( 528, 310 );
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
// 
// zedGraphControl6
// 
			this.zedGraphControl6.IsShowPointValues = false;
			this.zedGraphControl6.Location = new System.Drawing.Point( 24, 63 );
			this.zedGraphControl6.Name = "zedGraphControl6";
			this.zedGraphControl6.PointDateFormat = "g";
			this.zedGraphControl6.PointValueFormat = "G";
			this.zedGraphControl6.Size = new System.Drawing.Size( 461, 209 );
			this.zedGraphControl6.TabIndex = 1;
			this.zedGraphControl6.Paint += new System.Windows.Forms.PaintEventHandler( this.zedGraphControl6_Paint );
// 
// zedGraphControl4
// 
			this.zedGraphControl4.IsShowPointValues = false;
			this.zedGraphControl4.Location = new System.Drawing.Point( 24, 25 );
			this.zedGraphControl4.Name = "zedGraphControl4";
			this.zedGraphControl4.PointDateFormat = "g";
			this.zedGraphControl4.PointValueFormat = "G";
			this.zedGraphControl4.Size = new System.Drawing.Size( 474, 247 );
			this.zedGraphControl4.TabIndex = 0;
// 
// zedGraphControl5
// 
			this.zedGraphControl5.IsShowPointValues = false;
			this.zedGraphControl5.Location = new System.Drawing.Point( 64, 40 );
			this.zedGraphControl5.Name = "zedGraphControl5";
			this.zedGraphControl5.PointDateFormat = "g";
			this.zedGraphControl5.PointValueFormat = "G";
			this.zedGraphControl5.Size = new System.Drawing.Size( 328, 224 );
			this.zedGraphControl5.TabIndex = 0;
// 
// Form1
// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 680, 414 );
			this.Controls.Add( this.tabControl1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler( this.Form1_Load );
			this.tabControl1.ResumeLayout( false );
			this.tabPage1.ResumeLayout( false );
			this.tabPage2.ResumeLayout( false );
			this.tabPage3.ResumeLayout( false );
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
			zedGraphControl4.GraphPane.Title = "Test Graph for Tab 2";
			zedGraphControl5.GraphPane.Title = "Test Graph for Tab 3";
			zedGraphControl6.GraphPane.Title = "Test Graph for Tab 1";
			double x, y1, y2;
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();

			for( int i = 0; i < 18; i++ )
			{
				x = new XDate( 2005, i, i+5, i, i*2, i*3 );
				
				y1 = Math.Sin( i / 9.0 * Math.PI );
				y2 = Math.Cos( i / 9.0 * Math.PI );
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			LineItem myCurve = zedGraphControl4.GraphPane.AddCurve("Sine", list1, Color.Red, SymbolType.Circle);
			LineItem myCurve2 = zedGraphControl5.GraphPane.AddCurve("Cosine", list2, Color.Blue, SymbolType.Circle);
			LineItem myCurve3 = zedGraphControl6.GraphPane.AddCurve("Sine", list1, Color.Blue, SymbolType.Circle);
			LineItem myCurve4 = zedGraphControl6.GraphPane.AddCurve("Cosine", list2, Color.Red, SymbolType.Circle);
			myCurve3.Line.StepType = StepType.ForwardStep;
			myCurve4.Line.StepType = StepType.RearwardStep;
			
			zedGraphControl6.GraphPane.XAxis.Type = AxisType.Date;
			zedGraphControl6.IsShowPointValues = true;
			zedGraphControl6.PointDateFormat = "hh:MM";
			zedGraphControl6.PointValueFormat = "f4";
			zedGraphControl4.AxisChange();
			zedGraphControl5.AxisChange();
			zedGraphControl6.AxisChange();
		}

		private void tabPage1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void zedGraphControl6_Paint(object sender, PaintEventArgs e)
		{
		}
	}
}