namespace ZedGraph.ControlTest
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusXY = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelLastClick = new System.Windows.Forms.ToolStripStatusLabel();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusXY,
            this.statusLabelLastClick});
			this.statusStrip1.Location = new System.Drawing.Point(0, 393);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(520, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 17);
			this.toolStripStatusLabel1.Text = "Program Status";
			// 
			// toolStripStatusXY
			// 
			this.toolStripStatusXY.Name = "toolStripStatusXY";
			this.toolStripStatusXY.Size = new System.Drawing.Size(47, 17);
			this.toolStripStatusXY.Text = "Location";
			// 
			// statusLabelLastClick
			// 
			this.statusLabelLastClick.Name = "statusLabelLastClick";
			this.statusLabelLastClick.Size = new System.Drawing.Size(103, 17);
			this.statusLabelLastClick.Text = "statusLabelLastClick";
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.EditButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.ScrollGrace = 0;
			this.zedGraphControl1.ScrollMaxX = 0;
			this.zedGraphControl1.ScrollMaxY = 0;
			this.zedGraphControl1.ScrollMaxY2 = 0;
			this.zedGraphControl1.ScrollMinX = 0;
			this.zedGraphControl1.ScrollMinY = 0;
			this.zedGraphControl1.ScrollMinY2 = 0;
			this.zedGraphControl1.Size = new System.Drawing.Size(496, 367);
			this.zedGraphControl1.TabIndex = 0;
			this.zedGraphControl1.DoubleClickEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.z1_DoubleClickEvent);
			this.zedGraphControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.zedGraphControl1_Paint);
			this.zedGraphControl1.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraphControl1_MouseDownEvent);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(520, 415);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.zedGraphControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusXY;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelLastClick;
	}
}

