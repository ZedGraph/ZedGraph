//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2004  John Champion
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
	/// <author> John Champion revised by Jerry Vos </author>
	/// <version> $Revision: 1.8 $ $Date: 2004-08-30 17:39:02 $ </version>
	public class ZedGraphControl : UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// This private field contains the instance for the GraphPane object of this control.
		/// You can access the GraphPane object through the public property
		/// <see cref="ZedGraphControl.GraphPane"/>. This is nulled when this Control is
		/// disposed.
		/// </summary>
		private GraphPane graphPane;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Use double-buffering for flicker-free updating:
			SetStyle( ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
				| ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);

			Rectangle rect = new Rectangle( 0, 0, this.Size.Width, this.Size.Height );
			graphPane = new GraphPane( rect, "Title", "X-Axis", "Y-Axis" );
			graphPane.AxisChange( this.CreateGraphics() );
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.GraphPane"/> property for the control
		/// </summary>
		public GraphPane GraphPane
		{
			get
			{ 
				lock( this ) return graphPane;
			}
			
			set
			{ 
				lock( this ) graphPane = value; 
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if the components should be
		/// disposed, false otherwise</param>
		protected override void Dispose( bool disposing )
		{
			lock( this )
			{
				if( disposing )
				{
					if( components != null )
						components.Dispose();
				}
				base.Dispose( disposing );

				graphPane = null;
			}
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
			lock( this )
			{
				if ( BeenDisposed )
					return;

				base.OnPaint( e );

				this.graphPane.Draw( e.Graphics );
			}
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
		private void ChangeSize( object sender, System.EventArgs e )
		{
			lock( this )
			{
				if ( BeenDisposed )
					return;

				this.graphPane.PaneRect = new RectangleF( 0, 0, this.Size.Width, this.Size.Height );
				this.Invalidate();
			}
		}

		/// <summary>This performs an axis change command on the graphPane.  
		/// This is the same as 
		/// <c>ZedGraphControl.GraphPane.AxisChange( ZedGraphControl.CreateGraphics() )</c>.
		/// <seealso cref="GraphPane.AxisChange( Graphics )"/>.
		/// </summary>
		public virtual void AxisChange()
		{
			lock( this )
			{
				if ( BeenDisposed )
					return;

				Graphics g = this.CreateGraphics();

				graphPane.AxisChange( g );

				g.Dispose();
			}
		}

		/// <summary>
		/// Gets the graph pane's current image.
		/// <seealso cref="Bitmap"/>
		/// </summary>
		/// <exception cref="ZedGraphException">
		/// When the control has been disposed before this call.
		/// </exception>
		public Bitmap Image
		{
			get
			{
				lock( this )
				{
					if ( BeenDisposed )
						throw new ZedGraphException( "The control has been disposed" );

					Bitmap bitmap = new Bitmap( this.Width, this.Height );
					Graphics bitmapGraphics = Graphics.FromImage( bitmap );
					this.graphPane.Draw( bitmapGraphics );
					bitmapGraphics.Dispose();

					return bitmap;
				}
			}
		}

		/// <summary>
		/// This checks if the control has been disposed.  This is synonymous with
		/// the graph pane having been nulled or disposed.  Therefore this is the
		/// same as <c>ZedGraphControl.GraphPane == null</c>.
		/// </summary>
		public bool BeenDisposed
		{
			get
			{ 
				lock( this ) return graphPane == null; 
			}
		}
	}
}
