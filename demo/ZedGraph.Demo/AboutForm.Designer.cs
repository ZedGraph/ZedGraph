namespace ZedGraph.Demo
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AboutForm ) );
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point( 12, 12 );
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size( 340, 75 );
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = resources.GetString( "richTextBox1.Text" );
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point( 24, 95 );
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size( 159, 13 );
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://zedgraph.sourceforge.net";
			// 
			// richTextBox2
			// 
			this.richTextBox2.Location = new System.Drawing.Point( 12, 121 );
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new System.Drawing.Size( 340, 131 );
			this.richTextBox2.TabIndex = 3;
			this.richTextBox2.Text = "ZedGraph was written and is maintained by the following individuals:\n\nJohn Champi" +
				 "on\nJerry Vos\nBob Kaye\nDarren Martz\nBenjamin Mayargue\n\nZedGraph is Copyright © 20" +
				 "03-2006 by the authors.";
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 365, 293 );
			this.Controls.Add( this.richTextBox2 );
			this.Controls.Add( this.linkLabel1 );
			this.Controls.Add( this.richTextBox1 );
			this.Name = "AboutForm";
			this.Text = "Form1";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.RichTextBox richTextBox2;
	}
}

