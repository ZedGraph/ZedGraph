namespace ZedGraph.Demo
{
	partial class ChartTabForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelpWebPage = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelpLicense = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.demoTree = new System.Windows.Forms.TreeView();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.displayTC = new System.Windows.Forms.TabControl();
			this.tabDemo = new System.Windows.Forms.TabPage();
			this.descriptionBox = new System.Windows.Forms.RichTextBox();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.displayTC.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp} );
			this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size( 544, 24 );
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuFile
			// 
			this.menuFile.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExit} );
			this.menuFile.Name = "menuFile";
			this.menuFile.Size = new System.Drawing.Size( 35, 20 );
			this.menuFile.Text = "File";
			// 
			// menuFileExit
			// 
			this.menuFileExit.Name = "menuFileExit";
			this.menuFileExit.Size = new System.Drawing.Size( 152, 22 );
			this.menuFileExit.Text = "Exit";
			this.menuFileExit.Click += new System.EventHandler( this.menuFileExit_Click );
			// 
			// menuHelp
			// 
			this.menuHelp.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpWebPage,
            this.menuHelpAbout,
            this.menuHelpLicense} );
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Size = new System.Drawing.Size( 40, 20 );
			this.menuHelp.Text = "Help";
			// 
			// menuHelpWebPage
			// 
			this.menuHelpWebPage.Name = "menuHelpWebPage";
			this.menuHelpWebPage.Size = new System.Drawing.Size( 181, 22 );
			this.menuHelpWebPage.Text = "ZedGraph WebPage";
			this.menuHelpWebPage.Click += new System.EventHandler( this.menuHelpWebPage_Click );
			// 
			// menuHelpAbout
			// 
			this.menuHelpAbout.Name = "menuHelpAbout";
			this.menuHelpAbout.Size = new System.Drawing.Size( 181, 22 );
			this.menuHelpAbout.Text = "About";
			this.menuHelpAbout.Click += new System.EventHandler( this.menuHelpAbout_Click );
			// 
			// menuHelpLicense
			// 
			this.menuHelpLicense.Name = "menuHelpLicense";
			this.menuHelpLicense.Size = new System.Drawing.Size( 181, 22 );
			this.menuHelpLicense.Text = "License";
			this.menuHelpLicense.Click += new System.EventHandler( this.menuHelpLicense_Click );
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point( 0, 375 );
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size( 544, 22 );
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point( 0, 24 );
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add( this.demoTree );
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add( this.splitContainer2 );
			this.splitContainer1.Size = new System.Drawing.Size( 544, 351 );
			this.splitContainer1.SplitterDistance = 181;
			this.splitContainer1.TabIndex = 2;
			// 
			// demoTree
			// 
			this.demoTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.demoTree.Location = new System.Drawing.Point( 0, 0 );
			this.demoTree.Name = "demoTree";
			this.demoTree.Size = new System.Drawing.Size( 181, 351 );
			this.demoTree.TabIndex = 3;
			this.demoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.demoTree_AfterSelect );
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.Location = new System.Drawing.Point( 0, 0 );
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add( this.displayTC );
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add( this.descriptionBox );
			this.splitContainer2.Size = new System.Drawing.Size( 359, 351 );
			this.splitContainer2.SplitterDistance = 278;
			this.splitContainer2.TabIndex = 0;
			// 
			// displayTC
			// 
			this.displayTC.Controls.Add( this.tabDemo );
			this.displayTC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.displayTC.Location = new System.Drawing.Point( 0, 0 );
			this.displayTC.Name = "displayTC";
			this.displayTC.SelectedIndex = 0;
			this.displayTC.Size = new System.Drawing.Size( 359, 278 );
			this.displayTC.TabIndex = 0;
			// 
			// tabDemo
			// 
			this.tabDemo.Location = new System.Drawing.Point( 4, 22 );
			this.tabDemo.Name = "tabDemo";
			this.tabDemo.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabDemo.Size = new System.Drawing.Size( 351, 252 );
			this.tabDemo.TabIndex = 0;
			this.tabDemo.Text = "Demo";
			this.tabDemo.UseVisualStyleBackColor = true;
			// 
			// descriptionBox
			// 
			this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.descriptionBox.Location = new System.Drawing.Point( 0, 0 );
			this.descriptionBox.Name = "descriptionBox";
			this.descriptionBox.Size = new System.Drawing.Size( 359, 69 );
			this.descriptionBox.TabIndex = 0;
			this.descriptionBox.Text = "";
			// 
			// ChartTabForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 544, 397 );
			this.Controls.Add( this.splitContainer1 );
			this.Controls.Add( this.statusStrip1 );
			this.Controls.Add( this.menuStrip1 );
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ChartTabForm";
			this.Text = "ChartTabForm";
			this.Load += new System.EventHandler( this.ChartTabForm_Load_1 );
			this.menuStrip1.ResumeLayout( false );
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout( false );
			this.splitContainer1.Panel2.ResumeLayout( false );
			this.splitContainer1.ResumeLayout( false );
			this.splitContainer2.Panel1.ResumeLayout( false );
			this.splitContainer2.Panel2.ResumeLayout( false );
			this.splitContainer2.ResumeLayout( false );
			this.displayTC.ResumeLayout( false );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TreeView demoTree;
		private System.Windows.Forms.TabControl displayTC;
		private System.Windows.Forms.TabPage tabDemo;
		private System.Windows.Forms.RichTextBox descriptionBox;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuFileExit;
		private System.Windows.Forms.ToolStripMenuItem menuHelp;
		private System.Windows.Forms.ToolStripMenuItem menuHelpWebPage;
		private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
		private System.Windows.Forms.ToolStripMenuItem menuHelpLicense;
	}
}