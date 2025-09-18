namespace ZedGraph.LibTest
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
			this.CrossAutoBox = new System.Windows.Forms.CheckBox();
			this.AxisSelection = new System.Windows.Forms.ComboBox();
			this.LabelsInsideBox = new System.Windows.Forms.CheckBox();
			this.ReverseBox = new System.Windows.Forms.CheckBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).BeginInit();
			this.SuspendLayout();
			// 
			// CrossAutoBox
			// 
			this.CrossAutoBox.Location = new System.Drawing.Point( 427, 6 );
			this.CrossAutoBox.Name = "CrossAutoBox";
			this.CrossAutoBox.Size = new System.Drawing.Size( 69, 17 );
			this.CrossAutoBox.TabIndex = 10;
			this.CrossAutoBox.Text = "crossAuto";
			this.CrossAutoBox.CheckedChanged += new System.EventHandler( this.CrossAutoBox_CheckedChanged );
			// 
			// AxisSelection
			// 
			this.AxisSelection.Items.AddRange( new object[] {
            "X Axis",
            "Y Axis",
            "Y2 Axis"} );
			this.AxisSelection.Location = new System.Drawing.Point( 428, 25 );
			this.AxisSelection.Name = "AxisSelection";
			this.AxisSelection.Size = new System.Drawing.Size( 121, 21 );
			this.AxisSelection.TabIndex = 9;
			this.AxisSelection.SelectedIndexChanged += new System.EventHandler( this.AxisSelection_SelectedIndexChanged );
			// 
			// LabelsInsideBox
			// 
			this.LabelsInsideBox.Location = new System.Drawing.Point( 327, 29 );
			this.LabelsInsideBox.Name = "LabelsInsideBox";
			this.LabelsInsideBox.Size = new System.Drawing.Size( 84, 17 );
			this.LabelsInsideBox.TabIndex = 8;
			this.LabelsInsideBox.Text = "Labels Inside";
			this.LabelsInsideBox.CheckedChanged += new System.EventHandler( this.LabelsInsideBox_CheckedChanged );
			// 
			// ReverseBox
			// 
			this.ReverseBox.Location = new System.Drawing.Point( 327, 6 );
			this.ReverseBox.Name = "ReverseBox";
			this.ReverseBox.Size = new System.Drawing.Size( 70, 17 );
			this.ReverseBox.TabIndex = 7;
			this.ReverseBox.Text = "IsReverse";
			this.ReverseBox.CheckedChanged += new System.EventHandler( this.ReverseBox_CheckedChanged );
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point( 9, 6 );
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size( 311, 45 );
			this.trackBar1.TabIndex = 6;
			this.trackBar1.Scroll += new System.EventHandler( this.trackBar1_Scroll );
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 570, 438 );
			this.Controls.Add( this.CrossAutoBox );
			this.Controls.Add( this.AxisSelection );
			this.Controls.Add( this.LabelsInsideBox );
			this.Controls.Add( this.ReverseBox );
			this.Controls.Add( this.trackBar1 );
			this.Name = "Form1";
			this.Text = "Form1";
			this.Paint += new System.Windows.Forms.PaintEventHandler( this.Form1_Paint );
			this.Resize += new System.EventHandler( this.Form1_Resize );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseDown );
			this.Load += new System.EventHandler( this.Form1_Load );
			( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox CrossAutoBox;
		private System.Windows.Forms.ComboBox AxisSelection;
		private System.Windows.Forms.CheckBox LabelsInsideBox;
		private System.Windows.Forms.CheckBox ReverseBox;
		private System.Windows.Forms.TrackBar trackBar1;
	}
}

