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
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusXY = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.EditButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.EditModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None ) ) );
			this.zedGraphControl1.IsAutoScrollRange = false;
			this.zedGraphControl1.IsEnableHEdit = false;
			this.zedGraphControl1.IsEnableHPan = true;
			this.zedGraphControl1.IsEnableHZoom = true;
			this.zedGraphControl1.IsEnableVEdit = false;
			this.zedGraphControl1.IsEnableVPan = true;
			this.zedGraphControl1.IsEnableVZoom = true;
			this.zedGraphControl1.IsPrintFillPage = true;
			this.zedGraphControl1.IsPrintKeepAspectRatio = true;
			this.zedGraphControl1.IsScrollY2 = false;
			this.zedGraphControl1.IsShowContextMenu = true;
			this.zedGraphControl1.IsShowCopyMessage = true;
			this.zedGraphControl1.IsShowCursorValues = false;
			this.zedGraphControl1.IsShowHScrollBar = false;
			this.zedGraphControl1.IsShowPointValues = false;
			this.zedGraphControl1.IsShowVScrollBar = false;
			this.zedGraphControl1.IsSynchronizeXAxes = false;
			this.zedGraphControl1.IsSynchronizeYAxes = false;
			this.zedGraphControl1.IsZoomOnMouseCenter = false;
			this.zedGraphControl1.LinkButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.LinkModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None ) ) );
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
			this.zedGraphControl1.Size = new System.Drawing.Size( 636, 409 );
			this.zedGraphControl1.TabIndex = 0;
			this.zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomStepFraction = 0.1;
			this.zedGraphControl1.Paint += new System.Windows.Forms.PaintEventHandler( this.zedGraphControl1_Paint );
			this.zedGraphControl1.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler( this.zedGraphControl1_MouseMoveEvent );
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusXY} );
			this.statusStrip1.Location = new System.Drawing.Point( 0, 434 );
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size( 662, 22 );
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size( 81, 17 );
			this.toolStripStatusLabel1.Text = "Program Status";
			// 
			// toolStripStatusXY
			// 
			this.toolStripStatusXY.Name = "toolStripStatusXY";
			this.toolStripStatusXY.Size = new System.Drawing.Size( 47, 17 );
			this.toolStripStatusXY.Text = "Location";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 662, 456 );
			this.Controls.Add( this.statusStrip1 );
			this.Controls.Add( this.zedGraphControl1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler( this.Form1_Resize );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );
			this.Load += new System.EventHandler( this.Form1_Load );
			this.statusStrip1.ResumeLayout( false );
			this.statusStrip1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusXY;
	}
}

