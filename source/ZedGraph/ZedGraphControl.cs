using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZedGraph
{
	/// <summary>
	/// The ZedGraphControl class provides a UserControl interface to the
	/// <see cref="ZedGraph"/> class library.  This allows ZedGraph to be installed
	/// as a control in the Visual Studio toolbox.  You can use the control by simply
	/// dragging it onto a form in the Visual Studio form editor.  All graph
	/// attributes are accessible via the <see cref="ZedGraphControl.GraphPane"/>
	/// property.
	/// </summary>
	public class ZedGraphControl : UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// This private field contains the instance for the GraphPane object of this control.
		/// You can access the GraphPane object through the public property
		/// <see cref="ZedGraphControl.GraphPane"/>.
		/// </summary>
		private GraphPane graphPane;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Rectangle rect = new Rectangle( 0, 0, this.Size.Width, this.Size.Height );
			graphPane = new GraphPane( rect, "Title", "X-Axis", "Y-Axis" );
			graphPane.AxisChange();

		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.GraphPane"/> property for the control
		/// </summary>
		public GraphPane GraphPane
		{
			get { return graphPane; }
			set { graphPane = value; }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if the components should be
		/// disposed, false otherwise</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// GraphPane
			// 
			this.Name = "GraphPane";
			this.Resize += new System.EventHandler(this.ChangeSize);

		}
		#endregion

		/// <summary>
		/// Called by the system to update the control on-screen
		/// </summary>
		/// <param name="e">
		/// A PaintEventArgs object containing the Graphics specifications
		/// for this Paint event.
		/// </param>
		protected override void OnPaint( PaintEventArgs e )
		{
			base.OnPaint( e );

			this.graphPane.Draw( e.Graphics );
		}

		/// <summary>
		/// Called when the control has been resized.
		/// </summary>
		/// <param name="sender">
		/// A reference to the control that has been resized.
		/// </param>
		/// <param name="e">
		/// A PaintEventArgs object containing the Graphics specifications
		/// for this Paint event.
		/// </param>
		private void ChangeSize(object sender, System.EventArgs e)
		{
			this.graphPane.paneRect = new RectangleF( 0, 0, this.Size.Width, this.Size.Height );
			this.Invalidate();
		}
	}
}
