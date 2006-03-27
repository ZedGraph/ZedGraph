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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZedGraph.Demo
{
	/// <summary>
	/// A form that displays a tree, a location for displaying charts, and
	/// a text box for describing the currently showing chart.
	/// </summary>
	/// 
	/// <author> Jerry Vos with mods by John Champion</author>
	/// <version> $Revision: 1.9 $ $Date: 2006-03-27 01:31:36 $ </version>
	public abstract partial class ChartTabForm : Form
	{
		private const string TitlePrefix = "ZedGraph Demos : ";
		private Hashtable demos;
		private Hashtable typeToNodeTable;

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

			this.demos = new Hashtable();
			this.typeToNodeTable = new Hashtable();

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
			foreach ( string name in Enum.GetNames( typeof( DemoType ) ) )
			{
				buildPrimaryNode( (DemoType)Enum.Parse( typeof( DemoType ), name ) );
			}
		}

		/// <summary>
		/// Builds a top level node in the tree for a DemoType.
		/// </summary>
		/// <param name="type">The type of demo.</param>
		private void buildPrimaryNode( DemoType type )
		{
			TreeNode currNode = new TreeNode( TypeToName( type ) );
			typeToNodeTable[type] = currNode;

			this.demoTree.Nodes.Add( currNode );
		}
	#endregion

	#region Demo loading related methods
		/// <summary>
		/// Loads a demo into the correct place in the tree.
		/// </summary>
		/// <param name="demo">The demo to load.</param>
		protected void loadDemo( ZedGraphDemo demo )
		{
			foreach ( DemoType type in demo.Types )
			{
				TreeNode demosNode;

				TreeNode typeNode = (TreeNode)typeToNodeTable[type];
				if ( typeNode == null )
				{
					// error, this shouldn't be reached
					// TODO: do something about this
				}
				else
				{
					demosNode = new TreeNode( demo.Title );

					typeNode.Nodes.Add( demosNode );
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
		private string TypeToName( DemoType type )
		{
			switch ( type )
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
		private void Init( object key )
		{
			ZedGraphDemo demo = (ZedGraphDemo)this.demos[key];

			if ( demo == null )
				return;

			this.tabDemo.Controls.Clear();
			this.tabDemo.Controls.Add( demo.ZedGraphControl );

			demo.ZedGraphControl.Width = tabDemo.Width;
			demo.ZedGraphControl.Height = tabDemo.Height;

			demo.ZedGraphControl.Anchor = AnchorStyles.Left | AnchorStyles.Top
												| AnchorStyles.Right | AnchorStyles.Bottom;

			this.Text = TitlePrefix + demo.Title;

			descriptionBox.Text = demo.Description;

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
			if ( disposing )
			{
				if ( components != null )
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
		private void demoTree_AfterSelect( object sender, TreeViewEventArgs e )
		{
			if ( demos[e.Node.Text] != null )
				Init( e.Node.Text );
		}

		/// <summary>
		/// Ends the application.
		/// </summary>
		/// <param name="sender">Ignored.</param>
		/// <param name="e">Ignored.</param>
		private void mnuFExit_Click( object sender, System.EventArgs e )
		{
			System.Environment.Exit( 0 );
		}

		private void mnuHAbout_Click( object sender, System.EventArgs e )
		{
			Form frmAbout = new AboutForm();

			frmAbout.ShowDialog( this );
		}

		private void menuItem1_Click( object sender, System.EventArgs e )
		{
			Form frmLicense = new LicenseForm();

			frmLicense.ShowDialog( this );
		}

		private void mnuHWeb_Click( object sender, System.EventArgs e )
		{
			// open up the sourceforge site
			mnuHWSF_Click( sender, e );
		}

		private void mnuHWSF_Click( object sender, System.EventArgs e )
		{
			Help.ShowHelp( this, "http://zedgraph.sourceforge.net" );
		}

		private void mnuHWCP_Click( object sender, System.EventArgs e )
		{
			Help.ShowHelp( this, "http://codeproject.com/csharp/zedgraph.asp" );
		}
	#endregion

		private void ChartTabForm_Load( object sender, System.EventArgs e )
		{
			Init( "Combo Demo" );
		}

		private void menuFileExit_Click( object sender, EventArgs e )
		{
			System.Environment.Exit( 0 );
		}

		private void menuHelpAbout_Click( object sender, EventArgs e )
		{
			Form frmAbout = new AboutForm();

			frmAbout.ShowDialog( this );
		}

		private void menuHelpLicense_Click( object sender, EventArgs e )
		{
			Form frmLicense = new LicenseForm();

			frmLicense.ShowDialog( this );
		}

		private void menuHelpWebPage_Click( object sender, EventArgs e )
		{
			Help.ShowHelp( this, "http://zedgraph.sourceforge.net" );
		}

		private void ChartTabForm_Load_1( object sender, EventArgs e )
		{
			Init( "Combo Demo" );
		}
	}
}