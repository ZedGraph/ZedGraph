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
using System.ComponentModel;

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
	/// <version> $Revision: 3.9 $ $Date: 2005-02-19 19:01:13 $ </version>
	public class ZedGraphControl : UserControl
	{
		private System.ComponentModel.IContainer components;
		
	#region Fields


		/// <summary>
		/// private variable for displaying point-by-point tooltips on
		/// mouseover events.
		/// </summary>
		private ToolTip pointToolTip;

		/// <summary>
		/// This private field contains the instance for the MasterPane object of this control.
		/// You can access the MasterPane object through the public property
		/// <see cref="ZedGraphControl.MasterPane"/>. This is nulled when this Control is
		/// disposed.
		/// </summary>
		private MasterPane masterPane;

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
		/// Stores the <see cref="ContextMenu"/> reference for internal use.
		/// </summary>
		private ContextMenu contextMenu;
		
		/// <summary>
		/// private field that determines the format for displaying tooltip date values.
		/// This format is passed to <see cref="XDate.ToString(string)"/>.
		/// Use the public property <see cref="pointDateFormat"/> to access this
		/// value.
		/// </summary>
		private string pointDateFormat;
		
		/// <summary>
		/// The mouse position at any given point in time.  This needs to be replaced when I find
		/// out how to get the mouse position from inside a context menu handler.
		/// </summary>
		private PointF mousePt;
		
		/// <summary>
		/// Internal variable that indicates the user has selected Zoom mode from the context menu.  This
		/// affects the mouse cursor type, and it causes a zoom operation when the user left clicks.
		/// </summary>
		private bool		isZoomMode = false;
		/// <summary>
		/// Internal variable that indicates the user has selected Pan mode from the context menu.  This
		/// affects the mouse cursor type, and it causes a pan operation when the user left clicks.
		/// </summary>
		private bool		isPanMode = false;
		/// <summary>
		/// Internal variable that indicates the user is currently dragging the mouse with the left button
		/// down, associated with either a Zoom or Pan operation.
		/// </summary>
		private bool		isDragging = false;
		/// <summary>
		/// Internal variable that stores the <see cref="GraphPane"/> reference for the Pane that is
		/// currently being zoomed or panned.
		/// </summary>
		private GraphPane	dragPane = null;
		/// <summary>
		/// Internal variable that stores a rectangle which is either the zoom rectangle, or the incremental
		/// pan amount since the last mousemove event.
		/// </summary>
		private Rectangle	dragRect;

	#endregion

	#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pointToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			// 
			// pointToolTip
			// 
			this.pointToolTip.AutoPopDelay = 50000;
			this.pointToolTip.InitialDelay = 500;
			this.pointToolTip.ReshowDelay = 0;
			// 
			// contextMenu1
			// 
			this.contextMenu.Popup += new System.EventHandler(this.ContextMenu_Popup);
			// 
			// ZedGraphControl
			// 
			this.ContextMenu = this.contextMenu;
			this.Name = "ZedGraphControl";
			this.Resize += new System.EventHandler(this.ChangeSize);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseUp);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseDown);

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

			Rectangle rect = new Rectangle( 0, 0, this.Size.Width, this.Size.Height );
			masterPane = new MasterPane( "", rect );
			masterPane.MarginLeft = 0;
			masterPane.MarginRight = 0;
			masterPane.MarginTop = 0;
			masterPane.MarginBottom = 0;

			GraphPane graphPane = new GraphPane( rect, "Title", "X-Axis", "Y-Axis" );
			Graphics g = this.CreateGraphics();
			graphPane.AxisChange( g );
			g.Dispose();
			masterPane.Add( graphPane );

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

				masterPane = null;
			}
		}
	#endregion

	#region Properties
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.MasterPane"/> property for the control
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MasterPane MasterPane
		{
			get
			{
				lock( this ) return masterPane;
			}
			
			set
			{ 
				lock( this ) masterPane = value;
			}
		}
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.GraphPane"/> property for the control
		/// </summary>
		/// <remarks>
		/// <see cref="ZedGraphControl"/> actually uses a <see cref="MasterPane"/> object
		/// to hold a list of <see cref="GraphPane"/> objects.  This property really only
		/// accesses the first <see cref="GraphPane"/> in the list.  If there is more
		/// than one <see cref="GraphPane"/>, use the <see cref="MasterPane"/>
		/// indexer property to access any of the <see cref="GraphPane"/> objects.</remarks>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GraphPane GraphPane
		{
			get
			{
				// Just return the first GraphPane in the list
				lock( this ) return masterPane[0];
			}
			
			set
			{ 
				lock( this )
				{
					//Clear the list, and replace it with the specified Graphpane
					masterPane.PaneList.Clear();
					masterPane.Add( value );
				}
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
		public Image Image
		{
			get
			{
				lock( this )
				{
					if ( BeenDisposed || this.masterPane == null || this.masterPane[0] == null )
						throw new ZedGraphException( "The control has been disposed" );

					return this.masterPane.Image;
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
				lock( this ) return masterPane == null; 
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

				this.masterPane.Draw( e.Graphics );
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

				Graphics g = this.CreateGraphics();
				this.masterPane.ReSize( g, new RectangleF( 0, 0, this.Size.Width, this.Size.Height ) );
				g.Dispose();
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

				masterPane.AxisChange( g );

				g.Dispose();
			}
		}
	#endregion
	
	#region Mouse Events
		private void ZedGraphControl_MouseDown( object sender, MouseEventArgs e )
		{
			this.isDragging = false;
			
			GraphPane pane = this.MasterPane.FindAxisRect( new PointF( e.X, e.Y ) );
			
			if ( pane != null && this.isZoomMode && e.Button == MouseButtons.Left )
			{
				isDragging = true;
				// Calculate the startPoint by using the PointToScreen
				// method.
				this.dragRect = new Rectangle( ((Control)sender).PointToScreen( new Point(e.X, e.Y) ),
					new Size( 1, 1 ) );
				this.dragPane = pane;
			}
			else if ( pane != null && this.isPanMode && e.Button == MouseButtons.Left )
			{
				isDragging = true;
				// Calculate the startPoint by using the PointToScreen
				// method.
				this.dragRect = new Rectangle( ((Control)sender).PointToScreen( new Point(e.X, e.Y) ),
					new Size( 1, 1 ) );
				this.dragPane = pane;
			}
		}

		private void ZedGraphControl_MouseUp( object sender, MouseEventArgs e )
		{
			
			// If the MouseUp event occurs, the user is done dragging.
			if ( this.isDragging && this.isZoomMode )
			{
				// Only accept a drag if it covers at least 5 pixels in each direction
				Point curPt = ((Control)sender).PointToScreen( new Point(e.X, e.Y) );
				if ( Math.Abs( curPt.X - this.dragRect.X ) > 4 && Math.Abs( curPt.Y - this.dragRect.Y ) > 4 )
				{
					// Draw the rectangle to be evaluated. Set a dashed frame style
					// using the FrameStyle enumeration.
					ControlPaint.DrawReversibleFrame( this.dragRect,
						this.BackColor, FrameStyle.Dashed );

					double x1, x2, y1, y2, yy1, yy2;
					PointF endPoint = new PointF(e.X, e.Y); // ((Control)sender).PointToScreen( new Point(e.X, e.Y) );
					PointF startPoint =((Control)sender).PointToClient( this.dragRect.Location );

					this.dragPane.ReverseTransform( startPoint, out x1, out y1, out yy1 );
					this.dragPane.ReverseTransform( endPoint, out x2, out y2, out yy2 );

					this.dragPane.XAxis.Min = Math.Min( x1, x2 );
					this.dragPane.XAxis.Max = Math.Max( x1, x2 );
					this.dragPane.YAxis.Min = Math.Min( y1, y2 );
					this.dragPane.YAxis.Max = Math.Max( y1, y2 );

					Graphics g = this.CreateGraphics();
					this.dragPane.AxisChange( g );
					g.Dispose();
					Refresh();
				}
			}

			// Reset the rectangle.
			this.dragRect = new Rectangle(0, 0, 0, 0);
			isDragging = false;
			isZoomMode = false;
			isPanMode = false;
			Cursor.Current = Cursors.Default;

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
		private void ZedGraphControl_MouseMove( object sender, MouseEventArgs e )
		{
			this.mousePt = new PointF( e.X, e.Y );
			GraphPane pane = this.MasterPane.FindAxisRect( this.mousePt );
			if ( isZoomMode && ( pane != null || isDragging ) )
				Cursor.Current = Cursors.Cross;
			else if ( isPanMode && ( pane != null || isDragging ) )
				Cursor.Current = Cursors.Hand;
			else if ( isZoomMode || isPanMode )
				Cursor.Current = Cursors.No;
			
			// If the mouse is being dragged,
			// undraw and redraw the rectangle as the mouse moves.
			if ( this.isDragging && this.isZoomMode )
			{
				// Hide the previous rectangle by calling the
				// DrawReversibleFrame method with the same parameters.
				ControlPaint.DrawReversibleFrame( this.dragRect,
					this.BackColor, FrameStyle.Dashed);

				// Calculate the endpoint and dimensions for the new
				// rectangle, again using the PointToScreen method.
				Point curPt = ((Control)sender).PointToScreen( new Point( e.X, e.Y ) );
				this.dragRect.Width = curPt.X - this.dragRect.X;
				this.dragRect.Height = curPt.Y - this.dragRect.Y;

				// Draw the new rectangle by calling DrawReversibleFrame
				// again.
				ControlPaint.DrawReversibleFrame( this.dragRect,
					this.BackColor, FrameStyle.Dashed );
			}
			else if ( this.isDragging && this.isPanMode )
			{
				double x1, x2, y1, y2, yy1, yy2;
				PointF endPoint = new PointF(e.X, e.Y);
				PointF startPoint =((Control)sender).PointToClient( this.dragRect.Location );

				this.dragPane.ReverseTransform( startPoint, out x1, out y1, out yy1 );
				this.dragPane.ReverseTransform( endPoint, out x2, out y2, out yy2 );

				this.dragPane.XAxis.Min += x1 - x2;
				this.dragPane.XAxis.Max += x1 - x2;
				this.dragPane.YAxis.Min += y1 - y2;
				this.dragPane.YAxis.Max += y1 - y2;
				Refresh();
				
				this.dragRect.Location = ((Control)sender).PointToScreen( new Point(e.X, e.Y) );
				
			}
			else if ( isShowPointValues )
			{
				int			iPt;
				//GraphPane	pane;
				object		nearestObj;
	 
				Graphics g = this.CreateGraphics();
				
				if ( masterPane.FindNearestPaneObject( new PointF( e.X, e.Y ),
							g, out pane, out nearestObj, out iPt ) )
				{
					if ( nearestObj is CurveItem && iPt >= 0 )
					{
						CurveItem curve = (CurveItem) nearestObj;
						PointPair pt = curve.Points[iPt];
						
						if ( pt.Tag is string )
							this.pointToolTip.SetToolTip( this, (string) pt.Tag );
						else
						{
							string xStr, yStr;

							if ( pane.XAxis.IsDate )
								xStr = XDate.ToString( pt.X, this.pointDateFormat );
							else if ( pane.XAxis.IsText && pane.XAxis.TextLabels != null &&
											iPt >= 0 && iPt < pane.XAxis.TextLabels.Length )
								xStr = pane.XAxis.TextLabels[iPt];
							else
								xStr = pt.X.ToString( this.pointValueFormat );

							Axis yAxis = curve.IsY2Axis ? (Axis) pane.Y2Axis : (Axis) pane.YAxis;
								
							if ( yAxis.IsDate )
								yStr = XDate.ToString( pt.Y, this.pointDateFormat );
							else if ( yAxis.IsText && yAxis.TextLabels != null &&
											iPt >= 0 && iPt < yAxis.TextLabels.Length )
								yStr = yAxis.TextLabels[iPt];
							else
								yStr = pt.Y.ToString( this.pointValueFormat );

							this.pointToolTip.SetToolTip( this, "( " + xStr + ", " + yStr + " )" );

							//this.pointToolTip.SetToolTip( this,
							//	curve.Points[iPt].ToString( this.pointValueFormat ) );
						}
						this.pointToolTip.Active = true;
					}
					else
						this.pointToolTip.Active = false;
				}
				
				g.Dispose();
			}
		}
		
	#endregion

	#region ContextMenu
	
		private void ContextMenu_Popup( object sender, System.EventArgs e )
		{
			contextMenu.MenuItems.Clear();
			this.isDragging = false;
			this.isZoomMode = false;
			this.isPanMode = false;
			Cursor.Current = Cursors.Default;
			
			MenuItem menuItem;
			int index = 0;

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "Copy";
			this.contextMenu.MenuItems.Add( menuItem );
			menuItem.Click += new System.EventHandler( this.MenuClick_Copy );

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "Show Point Values";
			menuItem.Checked = this.IsShowPointValues;
			this.contextMenu.MenuItems.Add( menuItem );
			menuItem.Click += new System.EventHandler( this.MenuClick_ShowValues );

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "AutoScale";
			this.contextMenu.MenuItems.Add( menuItem );
			menuItem.Click += new EventHandler( this.MenuClick_AutoScale );

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "Zoom";
			this.contextMenu.MenuItems.Add( menuItem );
			menuItem.Click += new System.EventHandler( this.MenuClick_Zoom );

			menuItem = new MenuItem();
			menuItem.Index = index++;
			menuItem.Text = "Pan";
			this.contextMenu.MenuItems.Add( menuItem );
			menuItem.Click += new System.EventHandler( this.MenuClick_Pan );

		}
		
		protected void MenuClick_Copy( System.Object sender, System.EventArgs e )
		{
			Clipboard.SetDataObject( this.MasterPane.Image, true );
			MessageBox.Show( "Image Copied to ClipBoard" );
		}

		protected void MenuClick_ShowValues( System.Object sender, System.EventArgs e )
		{
			this.IsShowPointValues = ! ((MenuItem)sender).Checked;
		}

		protected void MenuClick_AutoScale( System.Object sender, EventArgs e )
		{
			GraphPane pane = this.MasterPane.FindPane( this.mousePt );
			if ( pane != null )
			{
				Graphics g = this.CreateGraphics();
				pane.XAxis.ResetAutoScale( pane, g );
				pane.YAxis.ResetAutoScale( pane, g );
				pane.Y2Axis.ResetAutoScale( pane, g );
				g.Dispose();
				Refresh();
			}
		}

		protected void MenuClick_Zoom( System.Object sender, System.EventArgs e )
		{
			GraphPane pane = this.MasterPane.FindPane( this.mousePt );
			if ( pane != null )
			{
				this.dragPane = null;
				this.isZoomMode = true;
				this.isDragging = false;
			}
		}

		protected void MenuClick_Pan( System.Object sender, EventArgs e )
		{
			GraphPane pane = this.MasterPane.FindPane( this.mousePt );
			if ( pane != null )
			{
				this.dragPane = null;
				this.isPanMode = true;
				this.isDragging = false;
			}
		}
		
	#endregion

	}
}
