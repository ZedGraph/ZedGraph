using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZedGraph
{
	/// <summary>
	/// Summary description for ZedGraphControl.
	/// </summary>
	public class ZedGraphControl : UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		GraphPane graphPane;

		public ZedGraphControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Rectangle rect = new Rectangle( 0, 0, this.Size.Width, this.Size.Height );
			graphPane = new GraphPane( rect, "Title", "X-Axis", "Y-Axis" );
			graphPane.AxisChange();

		}

		public GraphPane GraphPane
		{
			get { return graphPane; }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		protected override void OnPaint( PaintEventArgs e )
		{
			base.OnPaint( e );

			this.graphPane.Draw( e.Graphics );
		}

		private void ChangeSize(object sender, System.EventArgs e)
		{
			this.graphPane.paneRect = new RectangleF( 0, 0, this.Size.Width, this.Size.Height );
			this.Invalidate();
		}
	}
}
