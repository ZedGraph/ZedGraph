namespace ZedGraph.Demo
{
	partial class LicenseForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( LicenseForm ) );
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point( 12, 12 );
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size( 438, 208 );
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = resources.GetString( "richTextBox1.Text" );
			// 
			// LicenseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 462, 236 );
			this.Controls.Add( this.richTextBox1 );
			this.Name = "LicenseForm";
			this.Text = "LicenseForm";
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox1;
	}
}