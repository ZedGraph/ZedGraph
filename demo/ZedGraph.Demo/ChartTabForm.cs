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

		private Hashtable demos;

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

			Init("Simple Demo");
		}

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

			this.tabDemo.Controls.Clear();
			this.tabDemo.Controls.Add(demo.ZedGraphControl);

			demo.ZedGraphControl.Width		= tabDemo.Width;
			demo.ZedGraphControl.Height	= tabDemo.Height;

			demo.ZedGraphControl.Anchor	= AnchorStyles.Left | AnchorStyles.Top  
												| AnchorStyles.Right | AnchorStyles.Bottom;

			this.Text				= TitlePrefix + demo.Title;

			descriptionBox.Text	= demo.Description;

			demo.ZedGraphControl.AxisChange();

			this.Invalidate();
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
			this.demoTree = new System.Windows.Forms.TreeView();
			this.splitterHoriz = new System.Windows.Forms.Splitter();
			this.chartDescGB = new System.Windows.Forms.GroupBox();
			this.displayTC = new System.Windows.Forms.TabControl();
			this.tabDemo = new System.Windows.Forms.TabPage();
			this.splitterVert = new System.Windows.Forms.Splitter();
			this.descriptionBox = new System.Windows.Forms.RichTextBox();
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
			// splitterVert
			// 
			this.splitterVert.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterVert.Location = new System.Drawing.Point(3, 360);
			this.splitterVert.Name = "splitterVert";
			this.splitterVert.Size = new System.Drawing.Size(487, 3);
			this.splitterVert.TabIndex = 11;
			this.splitterVert.TabStop = false;
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
			// ChartTabForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 470);
			this.Controls.Add(this.chartDescGB);
			this.Controls.Add(this.splitterHoriz);
			this.Controls.Add(this.demoTree);
			this.Name = "ChartTabForm";
			this.Text = "DemoForm";
			this.chartDescGB.ResumeLayout(false);
			this.displayTC.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

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
	}
}
