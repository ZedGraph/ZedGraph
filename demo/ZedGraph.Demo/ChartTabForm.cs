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
	/// A form that displays a tree, a location for displaying charts, and
	/// a text box for describing the currently showing chart.
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 1.10 $ $Date: 2006-03-27 03:35:42 $ </version>
	public abstract class ChartTabForm : System.Windows.Forms.Form
	{
		private const string TitlePrefix = "ZedGraph Demos : ";

		
		private System.Windows.Forms.TreeView demoTree;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Hashtable typeToNodeTable;
		private System.Windows.Forms.Splitter splitterHoriz;
		private System.Windows.Forms.TabControl displayTC;
		private System.Windows.Forms.TabPage tabDemo;
		private System.Windows.Forms.Splitter splitterVert;
		private System.Windows.Forms.GroupBox chartDescGB;
		private System.Windows.Forms.RichTextBox descriptionBox;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFExit;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuHWeb;
		private System.Windows.Forms.MenuItem mnuHAbout;
		private System.Windows.Forms.MenuItem mnuHWSF;
		private System.Windows.Forms.MenuItem mnuHWCP;
		private System.Windows.Forms.MenuItem menuItem1;

		private Hashtable demos;

	#region Abstract methods
		/// <summary>
		/// Loads the demos into the form.<p/>
		/// 
		/// Is basically going to be
		/// <code>
		/// loadDemo(new XXXDemo());
		/// loadDemo(new YYYDemo());
		/// </code>
		/// </summary>
		protected abstract void loadDemos();
	#endregion

	#region Constructor
		public ChartTabForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.demos				= new Hashtable();
			this.typeToNodeTable	= new Hashtable();

			buildPrimaryTree();

			loadDemos();
		}
	#endregion

	#region Tree building methods
		/// <summary>
		/// Builds the top level of the tree, one level for each
		/// DemoType.
		/// </summary>
		private void buildPrimaryTree()
		{
			foreach (string name in Enum.GetNames(typeof(DemoType)))
			{
				buildPrimaryNode((DemoType) Enum.Parse(typeof(DemoType), name));
			}
		}

		/// <summary>
		/// Builds a top level node in the tree for a DemoType.
		/// </summary>
		/// <param name="type">The type of demo.</param>
		private void buildPrimaryNode(DemoType type)
		{
			TreeNode currNode = new TreeNode(TypeToName(type));
			typeToNodeTable[type] = currNode;

			this.demoTree.Nodes.Add(currNode);
		}
	#endregion

	#region Demo loading related methods
		/// <summary>
		/// Loads a demo into the correct place in the tree.
		/// </summary>
		/// <param name="demo">The demo to load.</param>
		protected void loadDemo(ZedGraphDemo demo) 
		{
			foreach (DemoType type in demo.Types)
			{
				TreeNode demosNode;

				TreeNode typeNode = (TreeNode) typeToNodeTable[type];
				if (typeNode == null)
				{
					// error, this shouldn't be reached
					// TODO: do something about this
				} 
				else 
				{
					demosNode = new TreeNode(demo.Title);

					typeNode.Nodes.Add(demosNode);
				}
			}
			// store the demo based on it's title
			demos[demo.Title] = demo;
		}

		/// <summary>
		/// Generates a string name for a demo type.
		/// </summary>
		/// 
		/// <param name="type">A DemoType</param>
		/// <returns>A name for the DemoType</returns>
		private string TypeToName(DemoType type)
		{
			switch (type)
			{
				case DemoType.Pie:
					return "Pie";
				case DemoType.General:
				default:
					return "General";
				case DemoType.Bar:
					return "Bar";
				case DemoType.Line:
					return "Line";
				case DemoType.Special:
					return "Special Features";
				case DemoType.Tutorial:
					return "Tutorial";
			}
		}

		/// <summary>
		/// Loads a demo into the frame
		/// </summary>
		/// <param name="key">The key the demo is stored in demos under</param>
		private void Init(object key) 
		{
			ZedGraphDemo demo = (ZedGraphDemo) this.demos[key];
			
			if ( demo == null )
				return;

			this.tabDemo.Controls.Clear();
			this.tabDemo.Controls.Add(demo.ZedGraphControl);

			demo.ZedGraphControl.Width = tabDemo.Width;
			demo.ZedGraphControl.Height	= tabDemo.Height;

			demo.ZedGraphControl.Anchor	= AnchorStyles.Left | AnchorStyles.Top  
												| AnchorStyles.Right | AnchorStyles.Bottom;

			this.Text				= TitlePrefix + demo.Title;

			descriptionBox.Text	= demo.Description;

			// tell the control to rescale itself
			demo.ZedGraphControl.AxisChange();

			// redraw the entire form
			this.Invalidate();
		}
	#endregion

	#region General control methods
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
	#endregion

	#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.demoTree = new System.Windows.Forms.TreeView();
			this.splitterHoriz = new System.Windows.Forms.Splitter();
			this.chartDescGB = new System.Windows.Forms.GroupBox();
			this.descriptionBox = new System.Windows.Forms.RichTextBox();
			this.splitterVert = new System.Windows.Forms.Splitter();
			this.displayTC = new System.Windows.Forms.TabControl();
			this.tabDemo = new System.Windows.Forms.TabPage();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuFExit = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuHWeb = new System.Windows.Forms.MenuItem();
			this.mnuHWSF = new System.Windows.Forms.MenuItem();
			this.mnuHWCP = new System.Windows.Forms.MenuItem();
			this.mnuHAbout = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.chartDescGB.SuspendLayout();
			this.displayTC.SuspendLayout();
			this.SuspendLayout();
			// 
			// demoTree
			// 
			this.demoTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.demoTree.ImageIndex = -1;
			this.demoTree.Location = new System.Drawing.Point(0, 0);
			this.demoTree.Name = "demoTree";
			this.demoTree.SelectedImageIndex = -1;
			this.demoTree.Size = new System.Drawing.Size(160, 470);
			this.demoTree.TabIndex = 0;
			this.demoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.demoTree_AfterSelect);
			// 
			// splitterHoriz
			// 
			this.splitterHoriz.Location = new System.Drawing.Point(160, 0);
			this.splitterHoriz.Name = "splitterHoriz";
			this.splitterHoriz.Size = new System.Drawing.Size(3, 470);
			this.splitterHoriz.TabIndex = 1;
			this.splitterHoriz.TabStop = false;
			// 
			// chartDescGB
			// 
			this.chartDescGB.Controls.Add(this.descriptionBox);
			this.chartDescGB.Controls.Add(this.splitterVert);
			this.chartDescGB.Controls.Add(this.displayTC);
			this.chartDescGB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chartDescGB.Location = new System.Drawing.Point(163, 0);
			this.chartDescGB.Name = "chartDescGB";
			this.chartDescGB.Size = new System.Drawing.Size(493, 470);
			this.chartDescGB.TabIndex = 2;
			this.chartDescGB.TabStop = false;
			// 
			// descriptionBox
			// 
			this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.descriptionBox.Location = new System.Drawing.Point(3, 363);
			this.descriptionBox.Name = "descriptionBox";
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(487, 104);
			this.descriptionBox.TabIndex = 13;
			this.descriptionBox.Text = "In here goes the demo description";
			// 
			// splitterVert
			// 
			this.splitterVert.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterVert.Location = new System.Drawing.Point(3, 360);
			this.splitterVert.Name = "splitterVert";
			this.splitterVert.Size = new System.Drawing.Size(487, 3);
			this.splitterVert.TabIndex = 11;
			this.splitterVert.TabStop = false;
			// 
			// displayTC
			// 
			this.displayTC.Controls.Add(this.tabDemo);
			this.displayTC.Dock = System.Windows.Forms.DockStyle.Top;
			this.displayTC.Location = new System.Drawing.Point(3, 16);
			this.displayTC.Name = "displayTC";
			this.displayTC.SelectedIndex = 0;
			this.displayTC.Size = new System.Drawing.Size(487, 344);
			this.displayTC.TabIndex = 10;
			// 
			// tabDemo
			// 
			this.tabDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tabDemo.Location = new System.Drawing.Point(4, 22);
			this.tabDemo.Name = "tabDemo";
			this.tabDemo.Size = new System.Drawing.Size(479, 318);
			this.tabDemo.TabIndex = 0;
			this.tabDemo.Text = "Demo";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuFile,
																					  this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFExit});
			this.mnuFile.Text = "&File";
			// 
			// mnuFExit
			// 
			this.mnuFExit.Index = 0;
			this.mnuFExit.Text = "E&xit";
			this.mnuFExit.Click += new System.EventHandler(this.mnuFExit_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 1;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuHWeb,
																					this.mnuHAbout,
																					this.menuItem1});
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHWeb
			// 
			this.mnuHWeb.Index = 0;
			this.mnuHWeb.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuHWSF,
																					this.mnuHWCP});
			this.mnuHWeb.Text = "ZedGraph &Webpage";
			this.mnuHWeb.Click += new System.EventHandler(this.mnuHWeb_Click);
			// 
			// mnuHWSF
			// 
			this.mnuHWSF.Index = 0;
			this.mnuHWSF.Text = "SourceForge";
			this.mnuHWSF.Click += new System.EventHandler(this.mnuHWSF_Click);
			// 
			// mnuHWCP
			// 
			this.mnuHWCP.Index = 1;
			this.mnuHWCP.Text = "CodeProject";
			this.mnuHWCP.Click += new System.EventHandler(this.mnuHWCP_Click);
			// 
			// mnuHAbout
			// 
			this.mnuHAbout.Index = 1;
			this.mnuHAbout.Text = "&About";
			this.mnuHAbout.Click += new System.EventHandler(this.mnuHAbout_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "&License";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// ChartTabForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 470);
			this.Controls.Add(this.chartDescGB);
			this.Controls.Add(this.splitterHoriz);
			this.Controls.Add(this.demoTree);
			this.Menu = this.mainMenu1;
			this.Name = "ChartTabForm";
			this.Text = "DemoForm";
			this.Load += new System.EventHandler(this.ChartTabForm_Load);
			this.chartDescGB.ResumeLayout(false);
			this.displayTC.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	#endregion

	#region Control related methods (event handlers and so forth)
		/// <summary>
		/// Loads a new demo up based on which demo's name was clicked on in the
		/// tree.
		/// </summary>
		/// 
		/// <param name="sender">The source of the message (unused)</param>
		/// <param name="e">
		/// Where the name of the clicked demo is retrieved from
		/// </param>
		private void demoTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (demos[e.Node.Text] != null)
				Init(e.Node.Text);
		}

		/// <summary>
		/// Ends the application.
		/// </summary>
		/// <param name="sender">Ignored.</param>
		/// <param name="e">Ignored.</param>
		private void mnuFExit_Click(object sender, System.EventArgs e)
		{
			System.Environment.Exit(0);
		}

		private void mnuHAbout_Click(object sender, System.EventArgs e)
		{
			Form frmAbout = new AboutForm();
			
			frmAbout.ShowDialog(this);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			Form frmLicense = new LicenseForm();

			frmLicense.ShowDialog(this);
		}

		private void mnuHWeb_Click(object sender, System.EventArgs e)
		{
			// open up the sourceforge site
			mnuHWSF_Click(sender, e);
		}

		private void mnuHWSF_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(this, "http://zedgraph.sourceforge.net");
		}

		private void mnuHWCP_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(this, "http://codeproject.com/csharp/zedgraph.asp");
		}
	#endregion

		private void ChartTabForm_Load(object sender, System.EventArgs e)
		{
			Init( "Combo Demo" );
		}
	}
}
