//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005 Jerry Vos
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 1.1 $ $Date: 2005-03-08 05:48:09 $ </version>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox2;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBox2
			// 
			this.richTextBox2.Location = new System.Drawing.Point(8, 144);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ReadOnly = true;
			this.richTextBox2.Size = new System.Drawing.Size(336, 112);
			this.richTextBox2.TabIndex = 7;
			this.richTextBox2.Text = "ZedGraph was written and is maintained by the following individuals:\n\nJohn Champi" +
				"on\nJerry Vos\nBob Kaye\nDarren Martz\n\nZedGraph is Copyright © 2003-2005 by the aut" +
				"hors.";
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(40, 112);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(248, 23);
			this.linkLabel2.TabIndex = 6;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "http://codeproject.com/csharp/zedgraph.asp";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(40, 88);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(168, 23);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://zedgraph.sourceforge.net";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(8, 8);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(336, 72);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "ZedGraph is an open-source charting library, released under the LGPL license (see" +
				" the next tab).  Latest updates, samples, forums, and downloads are available on" +
				" SourceForge.  An extensive tutorial article is available on CodeProject as well" +
				".";
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 267);
			this.Controls.Add(this.richTextBox2);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.richTextBox1);
			this.Name = "AboutForm";
			this.Text = "About ZedGraph";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
