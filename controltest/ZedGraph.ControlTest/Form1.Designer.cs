using ZedGraph;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Form1 ) );
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
			this.button1 = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusXY = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
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
			this.zedGraphControl1.Location = new System.Drawing.Point( 6, 6 );
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
			this.zedGraphControl1.Size = new System.Drawing.Size( 505, 311 );
			this.zedGraphControl1.TabIndex = 0;
			this.zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl1.ZoomStepFraction = 0.1;
			this.zedGraphControl1.Paint += new System.Windows.Forms.PaintEventHandler( this.zedGraphControl1_Paint );
			this.zedGraphControl1.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler( this.zedGraphControl1_MouseMoveEvent );
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add( this.tabPage1 );
			this.tabControl1.Controls.Add( this.tabPage2 );
			this.tabControl1.Location = new System.Drawing.Point( 12, 12 );
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size( 525, 349 );
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add( this.zedGraphControl1 );
			this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabPage1.Size = new System.Drawing.Size( 517, 323 );
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage1.Click += new System.EventHandler( this.tabPage1_Click );
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add( this.zedGraphControl2 );
			this.tabPage2.Location = new System.Drawing.Point( 4, 22 );
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabPage2.Size = new System.Drawing.Size( 517, 323 );
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// zedGraphControl2
			// 
			this.zedGraphControl2.EditButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl2.EditModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None ) ) );
			this.zedGraphControl2.IsAutoScrollRange = false;
			this.zedGraphControl2.IsEnableHEdit = false;
			this.zedGraphControl2.IsEnableHPan = true;
			this.zedGraphControl2.IsEnableHZoom = true;
			this.zedGraphControl2.IsEnableVEdit = false;
			this.zedGraphControl2.IsEnableVPan = true;
			this.zedGraphControl2.IsEnableVZoom = true;
			this.zedGraphControl2.IsPrintFillPage = true;
			this.zedGraphControl2.IsPrintKeepAspectRatio = true;
			this.zedGraphControl2.IsScrollY2 = false;
			this.zedGraphControl2.IsShowContextMenu = true;
			this.zedGraphControl2.IsShowCopyMessage = true;
			this.zedGraphControl2.IsShowCursorValues = false;
			this.zedGraphControl2.IsShowHScrollBar = false;
			this.zedGraphControl2.IsShowPointValues = false;
			this.zedGraphControl2.IsShowVScrollBar = false;
			this.zedGraphControl2.IsSynchronizeXAxes = false;
			this.zedGraphControl2.IsSynchronizeYAxes = false;
			this.zedGraphControl2.IsZoomOnMouseCenter = false;
			this.zedGraphControl2.LinkButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl2.LinkModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None ) ) );
			this.zedGraphControl2.Location = new System.Drawing.Point( 6, 6 );
			this.zedGraphControl2.Name = "zedGraphControl2";
			this.zedGraphControl2.PanButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl2.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			this.zedGraphControl2.PanModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None ) ) );
			this.zedGraphControl2.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl2.PointDateFormat = "g";
			this.zedGraphControl2.PointValueFormat = "G";
			this.zedGraphControl2.ScrollMaxX = 0;
			this.zedGraphControl2.ScrollMaxY = 0;
			this.zedGraphControl2.ScrollMaxY2 = 0;
			this.zedGraphControl2.ScrollMinX = 0;
			this.zedGraphControl2.ScrollMinY = 0;
			this.zedGraphControl2.ScrollMinY2 = 0;
			this.zedGraphControl2.Size = new System.Drawing.Size( 505, 311 );
			this.zedGraphControl2.TabIndex = 0;
			this.zedGraphControl2.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.zedGraphControl2.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.zedGraphControl2.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.zedGraphControl2.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.zedGraphControl2.ZoomStepFraction = 0.1;
			this.zedGraphControl2.ScrollDoneEvent += new ZedGraph.ZedGraphControl.ScrollDoneHandler( this.zedGraphControl2_ScrollEvent );
			this.zedGraphControl2.Scroll += new System.Windows.Forms.ScrollEventHandler( this.zedGraphControl2_Scroll );
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point( 351, 2 );
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size( 75, 23 );
			this.button1.TabIndex = 2;
			this.button1.Text = "Goto Page 2";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler( this.button1_Click );
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusXY} );
			this.statusStrip1.Location = new System.Drawing.Point( 0, 392 );
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size( 548, 22 );
			this.statusStrip1.TabIndex = 3;
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
			this.toolStripStatusXY.ToolTipText = "X,Y Mouse Location";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 548, 414 );
			this.Controls.Add( this.statusStrip1 );
			this.Controls.Add( this.button1 );
			this.Controls.Add( this.tabControl1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler( this.Form1_Resize );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );
			this.Load += new System.EventHandler( this.Form1_Load );
			this.tabControl1.ResumeLayout( false );
			this.tabPage1.ResumeLayout( false );
			this.tabPage2.ResumeLayout( false );
			this.statusStrip1.ResumeLayout( false );
			this.statusStrip1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private ZedGraphControl zedGraphControl2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusXY;
	}
}

