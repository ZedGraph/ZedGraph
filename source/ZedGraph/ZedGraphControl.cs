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
using System.Text;

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
	/// <version> $Revision: 3.2.2.2 $ $Date: 2005-01-17 01:59:30 $ </version>
	public class ZedGraphControl : UserControl
	{
	#region Fields
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// private variable for displaying point-by-point tooltips on
		/// mouseover events.
		/// </summary>
		private System.Windows.Forms.ToolTip pointToolTip;

		/// <summary>
		/// This private field contains the instance for the GraphPane object of this control.
		/// You can access the GraphPane object through the public property
		/// <see cref="ZedGraphControl.GraphPane"/>. This is nulled when this Control is
		/// disposed.
		/// </summary>
		private GraphPane graphPane;

		/// <summary>
		/// private field that determines whether or not tooltips will be display
		/// when the mouse hovers over data values.  Use the public property
		/// <see cref="IsShowPointValues"/> to access this value.
		/// </summary>
		private bool isShowPointValues;
		/// <summary>
		/// private field that determines the format for displaying tooltip values.
		/// This format is passed to <see cref="PointPair.ToString(string)"/>.
		/// Use the public property <see cref="pointValueFormat"/> to access this
		/// value.
		/// </summary>
		private string pointValueFormat;

		/// <summary>
		/// private field that determines the format for displaying tooltip date values.
		/// This format is passed to <see cref="XDate.ToString(string)"/>.
		/// Use the public property <see cref="pointDateFormat"/> to access this
		/// value.
		/// </summary>
		private string pointDateFormat;

	#endregion

	#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pointToolTip = new System.Windows.Forms.ToolTip( this.components );
			// 
			// PointToolTip
			// 
			this.pointToolTip.AutoPopDelay = 50000;
			this.pointToolTip.InitialDelay = 500;
			this.pointToolTip.ReshowDelay = 0;
			// 
			// GraphPane
			// 
			this.Name = "ZedGraphControl";
			this.Resize += new System.EventHandler( this.ChangeSize );
			this.MouseMove += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseMove );
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Use double-buffering for flicker-free updating:
			SetStyle( ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
				| ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true );
			//isTransparentBackground = false;
			SetStyle( ControlStyles.Opaque, false );
			SetStyle( ControlStyles.SupportsTransparentBackColor, true );

			Rectangle rect = new Rectangle( 0, 0, this.Size.Width-1, this.Size.Height-1 );
			graphPane = new GraphPane( rect, "Title", "X-Axis", "Y-Axis" );
			graphPane.AxisChange( this.CreateGraphics() );

			this.isShowPointValues = false;
			this.pointValueFormat = PointPair.DefaultFormat;
			this.pointDateFormat = XDate.DefaultFormatStr;
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
	#endregion

	#region Properties
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
		/// Gets or sets a value that determines whether or not tooltips will be display
		/// when the mouse hovers over data values.  The displayed values are taken
		/// from <see cref="PointPair.Tag"/> if it is a <see cref="System.String"/> type,
		/// or <see cref="PointPair.ToString()"/> otherwise.
		/// </summary>
		public bool IsShowPointValues
		{
			get { return isShowPointValues; }
			set { isShowPointValues = value; }
		}

		/// <summary>
		/// Gets or sets the format for displaying tooltip values.
		/// This format is passed to <see cref="PointPair.ToString(string)"/>.
		/// </summary>
		public string PointValueFormat
		{
			get { return pointValueFormat; }
			set { pointValueFormat = value; }
		}

		/// <summary>
		/// Gets or sets the format for displaying tooltip values.
		/// This format is passed to <see cref="XDate.ToString(string)"/>.
		/// </summary>
		public string PointDateFormat
		{
			get { return pointDateFormat; }
			set { pointDateFormat = value; }
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
					if ( BeenDisposed || this.graphPane == null )
						throw new ZedGraphException( "The control has been disposed" );
/*
					Bitmap bitmap = new Bitmap( this.Width+1, this.Height+1 );
					Graphics bitmapGraphics = Graphics.FromImage( bitmap );
					this.graphPane.Draw( bitmapGraphics );
					bitmapGraphics.Dispose();

					return bitmap;
*/
                    return this.graphPane.Image;
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
	#endregion

	#region Methods
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

				this.graphPane.PaneRect = new RectangleF( 0, 0, this.Size.Width-1, this.Size.Height-1 );
				this.Invalidate();
			}
		}

		/// <summary>This performs an axis change command on the graphPane.  
		/// This is the same as 
		/// <c>ZedGraphControl.GraphPane.AxisChange( ZedGraphControl.CreateGraphics() )</c>.
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
		/// private method for handling MouseMove events to display tooltips over
		/// individual datapoints.
		/// </summary>
		/// <param name="sender">
		/// A reference to the control that has the MouseMove event.
		/// </param>
		/// <param name="e">
		/// A MouseEventArgs object.
		/// </param>
		private void ZedGraphControl_MouseMove( object sender,
				System.Windows.Forms.MouseEventArgs e )
		{
			if ( isShowPointValues )
			{
				CurveItem curve; 
				int iPt; 
	 
				if ( graphPane.FindNearestPoint( new PointF( e.X, e.Y ),
							out curve, out iPt ) )
				{
					PointPair pt = curve.Points[iPt];
					
					if ( pt.Tag is string )
						this.pointToolTip.SetToolTip( this, (string) pt.Tag );
					else
					{
						string xStr = this.GraphPane.XAxis.IsDate ? XDate.ToString( pt.X, this.pointDateFormat ) :
							pt.X.ToString( this.pointValueFormat );
							
						bool yIsDate = ( curve.IsY2Axis && this.GraphPane.Y2Axis.IsDate ) ||
							( !curve.IsY2Axis && this.GraphPane.YAxis.IsDate );
									
						string yStr = yIsDate ? XDate.ToString( pt.Y, this.pointDateFormat ) :
							pt.Y.ToString( this.pointValueFormat );
							
						this.pointToolTip.SetToolTip( this, "( " + xStr + ", " + yStr + " )" );

						//this.pointToolTip.SetToolTip( this,
						//	curve.Points[iPt].ToString( this.pointValueFormat ) );
					}
					this.pointToolTip.Active = true;
				}
				else
					this.pointToolTip.Active = false;
			}
		}
	#endregion
	}
}
