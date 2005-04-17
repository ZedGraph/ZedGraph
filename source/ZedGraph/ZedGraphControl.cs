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
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel;
using System.IO;

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
	/// <version> $Revision: 3.17 $ $Date: 2005-04-17 05:30:21 $ </version>
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
		/// private field that determines whether or not the context menu will be available.  Use the
		/// public property <see cref="IsShowContextMenu"/> to access this value.
		/// </summary>
		private bool isShowContextMenu;
		
		/// <summary>
		/// private field that determines the format for displaying tooltip date values.
		/// This format is passed to <see cref="XDate.ToString(string)"/>.
		/// Use the public property <see cref="pointDateFormat"/> to access this
		/// value.
		/// </summary>
		private string pointDateFormat;
		
		/// <summary>
		/// Internal variable that indicates the control is currently being zoomed. 
		/// </summary>
		private bool		isZooming = false;
		/// <summary>
		/// Internal variable that indicates the control is currently being panned.
		/// </summary>
		private bool		isPanning = false;
		
		/// <summary>
		/// Private value that determines whether or not zooming is allowed for the control.  Use the
		/// public property <see cref="IsEnableZoom"/> to access this value.
		/// </summary>
		private bool isEnableZoom = true;
		/// <summary>
		/// Private value that determines whether or not panning is allowed for the control.  Use the
		/// public property <see cref="IsEnablePan"/> to access this value.
		/// </summary>
		private bool isEnablePan = true;
		
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
		/// <summary>
		/// private field that stores the state of the scale ranges prior to starting a panning action.
		/// </summary>
		private ZoomState	zoomState;

		private double		zoomStepFraction = 0.1;

		private bool		isZoomOnMouseCenter = false;

		/// <summary>
		/// A delegate that allows subscribing methods to append or modify the context menu.
		/// </summary>
		/// <remarks>
		/// The context menu is built on the fly after a right mouse click.  You can add menu items
		/// to this menu by simply modifying the <see paramref="menu"/> parameter.
		/// </remarks>
		public delegate void ContextMenuBuilderEventHandler( object sender, ContextMenu menu );
		/// <summary>
		/// Subscribe to this event to be able to modify the ZedGraph context menu.
		/// </summary>
		/// <seealso cref="ContextMenuBuilderEventHandler"/>
		public event ContextMenuBuilderEventHandler ContextMenuBuilder;


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
			this.contextMenu = new System.Windows.Forms.ContextMenu();
// 
// pointToolTip
// 
			this.pointToolTip.AutoPopDelay = 5000;
			this.pointToolTip.InitialDelay = 500;
			this.pointToolTip.ReshowDelay = 0;
// 
// contextMenu
// 
			//this.contextMenu.Name = "contextMenu";
			this.contextMenu.Popup += new System.EventHandler( this.ContextMenu_Popup );
// 
// ZedGraphControl
// 
			this.ContextMenu = this.contextMenu;
			this.Name = "ZedGraphControl";
			this.Resize += new System.EventHandler( this.ChangeSize );
			this.MouseMove += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseMove );
			this.KeyUp += new System.Windows.Forms.KeyEventHandler( this.ZedGraphControl_KeyUp );
			this.MouseUp += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseUp );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseDown );
			this.KeyDown += new System.Windows.Forms.KeyEventHandler( this.ZedGraphControl_KeyDown );
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseWheel );
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
			this.isShowContextMenu = true;
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
			get { lock( this ) return masterPane; }
			set { lock( this ) masterPane = value; }
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
		/// Gets or sets a value that determines whether or not zooming is allowed for the control.
		/// </summary>
		/// <remarks>
		/// Zooming is done by left-clicking inside the <see cref="ZedGraph.GraphPane.AxisRect"/> to drag
		/// out a rectangle, indicating the new scale ranges that will be part of the graph.
		/// </remarks>
		public bool IsEnableZoom
		{
			get { return isEnableZoom; }
			set { isEnableZoom = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not panning is allowed for the control.
		/// </summary>
		/// <remarks>
		/// Panning is done by clicking the middle mouse button (or holding down the shift key
		/// while clicking the left mouse button) inside the <see cref="ZedGraph.GraphPane.AxisRect"/> and
		/// dragging the mouse around to shift the scale ranges as desired.
		/// </remarks>
		public bool IsEnablePan
		{
			get { return isEnablePan; }
			set { isEnablePan = value; }
		}
		
		/// <summary>
		/// Gets or sets a value that determines whether or not the context menu will be available.
		/// </summary>
		/// <remarks>The context menu is a menu that appears when you right-click on the
		/// <see cref="ZedGraphControl"/>.  It provides options for Zoom, Pan, AutoScale, Clipboard
		/// Copy, and toggle <see cref="IsShowPointValues"/>.
		/// </remarks>
		/// <value>true to allow the context menu, false to disable it</value>
		public bool IsShowContextMenu
		{
			get { return isShowContextMenu; }
			set { isShowContextMenu = value; }
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
		/// Gets or sets the step size fraction for zooming with the mouse wheel.
		/// A value of 0.1 will result in a 10% zoom step for each mouse wheel movement.
		/// </summary>
		public double ZoomStepFraction
		{
			get { return zoomStepFraction; }
			set { zoomStepFraction = value; }
		}

		/// <summary>
		/// Gets or sets a boolean value that determines if zooming with the wheel mouse
		/// is centered on the mouse location, or centered on the existing graph.
		/// </summary>
		public bool IsZoomOnMouseCenter
		{
			get { return isZoomOnMouseCenter; }
			set { isZoomOnMouseCenter = value; }
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
			this.isPanning = false;
			this.isZooming = false;
			this.dragPane = null;
			
			GraphPane pane = this.MasterPane.FindAxisRect( new PointF( e.X, e.Y ) );
			
			if ( pane != null && this.isEnablePan &&
					( e.Button == MouseButtons.Middle || ( e.Button == MouseButtons.Left &&
							( Control.ModifierKeys == Keys.Shift ) ) ) )
			{
				isPanning = true;
				// Calculate the startPoint by using the PointToScreen
				// method.
				this.dragRect = new Rectangle( ((Control)sender).PointToScreen( new Point(e.X, e.Y) ),
					new Size( 1, 1 ) );
				this.dragPane = pane;
				this.zoomState = new ZoomState( this.dragPane, ZoomState.StateType.Pan );
			}
			else if ( pane != null && this.isEnableZoom && e.Button == MouseButtons.Left )
			{
				isZooming = true;
				// Calculate the startPoint by using the PointToScreen
				// method.
				this.dragRect = new Rectangle( ((Control)sender).PointToScreen( new Point(e.X, e.Y) ),
					new Size( 1, 1 ) );
				this.dragPane = pane;
			}
		}

		private void ZoomScale( Axis axis, int delta, double centerVal )
		{
			if ( axis.IsLog )
			{
				double ratio = Math.Sqrt( axis.Max / axis.Min *
					( 1 + (delta < 0 ? 1.0 : -1.0) * ZoomStepFraction ) );

				if ( !this.isZoomOnMouseCenter )
					centerVal = Math.Sqrt( axis.Max * axis.Min );

				axis.Min = centerVal / ratio;
				axis.Max = centerVal * ratio;
			}
			else
			{
				double range = ( axis.Max - axis.Min ) *
					( 1 + (delta < 0 ? 1.0 : -1.0) * ZoomStepFraction ) / 2.0;

				if ( !this.isZoomOnMouseCenter )
					centerVal = ( axis.Max + axis.Min ) / 2.0;

				axis.Min = centerVal - range;
				axis.Max = centerVal + range;
			}
		}

		private void ZedGraphControl_MouseWheel( object sender, MouseEventArgs e )
		{
			if ( this.isEnableZoom )
			{
				GraphPane pane = this.MasterPane.FindAxisRect( new PointF( e.X, e.Y ) );
				if ( pane != null && this.isEnableZoom && e.Delta != 0 )
				{
					pane.zoomStack.Push( pane, ZoomState.StateType.Zoom );

					PointF centerPoint = new PointF(e.X, e.Y);
					double x, y, y2;
					pane.ReverseTransform( centerPoint, out x, out y, out y2 );

					ZoomScale( pane.XAxis, e.Delta, x );
					ZoomScale( pane.YAxis, e.Delta, y );
					ZoomScale( pane.Y2Axis, e.Delta, y2 );

					Graphics g = this.CreateGraphics();
					pane.AxisChange( g );
					g.Dispose();
					Refresh();
				}
			}
		}

		private void ZedGraphControl_MouseUp( object sender, MouseEventArgs e )
		{
			if ( this.dragPane != null )
			{
				// If the MouseUp event occurs, the user is done dragging.
				if ( this.isZooming )
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

						this.dragPane.zoomStack.Push( this.dragPane, ZoomState.StateType.Zoom );

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
				else if ( this.isPanning )
					// push the prior saved zoomstate, since the scale ranges have already been changed on
					// the fly during the panning operation
					if ( this.zoomState.IsChanged( this.dragPane ) )
						this.dragPane.zoomStack.Push( this.zoomState );
			}

			// Reset the rectangle.
			this.dragRect = new Rectangle(0, 0, 0, 0);
			this.dragPane = null;
			isZooming = false;
			isPanning = false;
			Cursor.Current = Cursors.Default;

		}

		private void PanScale( Axis axis, double startVal, double endVal )
		{
			if ( axis.Type == AxisType.Log )
			{
				axis.Min *= startVal/endVal;
				axis.Max *= startVal/endVal;
			}
			else
			{
				axis.Min += startVal - endVal;
				axis.Max += startVal - endVal;
			}
		}

		private string MakeValueLabel( Axis axis, double val, int iPt )
		{
			if ( axis.IsDate )
				return XDate.ToString( val, this.pointDateFormat );
			else if ( axis.IsText && axis.TextLabels != null &&
						iPt >= 0 && iPt < axis.TextLabels.Length )
				return axis.TextLabels[iPt];
			else
				return val.ToString( this.pointValueFormat );
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
			Point mousePt = new Point( e.X, e.Y );
			SetCursor( mousePt );

			// If the mouse is being dragged,
			// undraw and redraw the rectangle as the mouse moves.
			if ( this.isZooming )
			{
				// Hide the previous rectangle by calling the
				// DrawReversibleFrame method with the same parameters.
				ControlPaint.DrawReversibleFrame( this.dragRect,
					this.BackColor, FrameStyle.Dashed);

				// Calculate the endpoint and dimensions for the new
				// rectangle, again using the PointToScreen method.
				Point curPt = ((Control)sender).PointToScreen( mousePt );
				this.dragRect.Width = curPt.X - this.dragRect.X;
				this.dragRect.Height = curPt.Y - this.dragRect.Y;

				// Draw the new rectangle by calling DrawReversibleFrame
				// again.
				ControlPaint.DrawReversibleFrame( this.dragRect,
					this.BackColor, FrameStyle.Dashed );
			}
			else if ( this.isPanning )
			{
				double x1, x2, y1, y2, yy1, yy2;
				PointF endPoint = new PointF(e.X, e.Y);
				PointF startPoint =((Control)sender).PointToClient( this.dragRect.Location );

				this.dragPane.ReverseTransform( startPoint, out x1, out y1, out yy1 );
				this.dragPane.ReverseTransform( endPoint, out x2, out y2, out yy2 );

				PanScale( this.dragPane.XAxis, x1, x2 );
				PanScale( this.dragPane.YAxis, y1, y2 );
				PanScale( this.dragPane.Y2Axis, yy1, yy2 );

				Refresh();
				
				this.dragRect.Location = ((Control)sender).PointToScreen( mousePt );
				
			}
			else if ( isShowPointValues )
			{
				int			iPt;
				GraphPane	pane;
				object		nearestObj;
	 
				Graphics g = this.CreateGraphics();
				
				if ( masterPane.FindNearestPaneObject( new PointF( e.X, e.Y ),
							g, out pane, out nearestObj, out iPt ) )
				{
					if ( nearestObj is CurveItem && iPt >= 0 )
					{
						CurveItem curve = (CurveItem) nearestObj;
						if ( curve is PieItem )
						{
							this.pointToolTip.SetToolTip( this,
									((PieItem)curve).Value.ToString( this.pointValueFormat ) );
						}
						else
						{
							PointPair pt = curve.Points[iPt];
							
							if ( pt.Tag is string )
								this.pointToolTip.SetToolTip( this, (string) pt.Tag );
							else
							{
								string xStr = MakeValueLabel( pane.XAxis, pt.X, iPt );
								string yStr = MakeValueLabel(
												curve.IsY2Axis ? (Axis) pane.Y2Axis : (Axis) pane.YAxis,
												pt.Y, iPt );
								/*
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
								*/

								this.pointToolTip.SetToolTip( this, "( " + xStr + ", " + yStr + " )" );

								//this.pointToolTip.SetToolTip( this,
								//	curve.Points[iPt].ToString( this.pointValueFormat ) );
							}
						}

						this.pointToolTip.Active = true;
					}
					else
						this.pointToolTip.Active = false;
				}
				
				g.Dispose();
			}
		}
		
		private void SetCursor()
		{
			SetCursor( this.PointToClient( Control.MousePosition ) );
		}

		private void SetCursor( Point mousePt )
		{
			GraphPane pane = this.MasterPane.FindAxisRect( mousePt );
			if ( isEnablePan && ( Control.ModifierKeys == Keys.Shift || isPanning ) &&
								( pane != null || isPanning ) )
				Cursor.Current = Cursors.Hand;
			else if ( isEnableZoom && ( pane != null || isZooming ) )
				Cursor.Current = Cursors.Cross;
				
//			else if ( isZoomMode || isPanMode )
//				Cursor.Current = Cursors.No;
		}

		private void ZedGraphControl_KeyUp(object sender, KeyEventArgs e)
		{
			SetCursor();
		}

		/// <summary>
		/// Handle the Key Events so ZedGraph can Escape out of a panning or zooming operation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZedGraphControl_KeyDown( object sender, System.Windows.Forms.KeyEventArgs e )
		{
			SetCursor();

			if ( e.KeyCode == Keys.Escape )
			{
				this.isZooming = false;
				this.isPanning = false;
				Refresh();
			}
		}

	#endregion

	#region ContextMenu
	
		/// <summary>
		/// private method to handle the popup context menu in the <see cref="ZedGraphControl"/>.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContextMenu_Popup( object sender, System.EventArgs e )
		{
			contextMenu.MenuItems.Clear();
			this.isZooming = false;
			this.isPanning = false;
			Cursor.Current = Cursors.Default;
			GraphPane pane = this.MasterPane.FindPane( this.PointToClient( Control.MousePosition ) );
			
			if ( this.isShowContextMenu )
			{
				MenuItem menuItem;
				int index = 0;

				menuItem = new MenuItem();
				menuItem.Index = index++;
				menuItem.Text = "Copy";
				this.contextMenu.MenuItems.Add( menuItem );
				menuItem.Click += new System.EventHandler( this.MenuClick_Copy );

				menuItem = new MenuItem();
				menuItem.Index = index++;
				menuItem.Text = "Save Image As...";
				this.contextMenu.MenuItems.Add( menuItem );
				menuItem.Click += new System.EventHandler( this.MenuClick_SaveAs );

				menuItem = new MenuItem();
				menuItem.Index = index++;
				menuItem.Text = "Show Point Values";
				menuItem.Checked = this.IsShowPointValues;
				this.contextMenu.MenuItems.Add( menuItem );
				menuItem.Click += new System.EventHandler( this.MenuClick_ShowValues );

				menuItem = new MenuItem();
				menuItem.Index = index++;
				menuItem.Text = "Un-" + ( ( pane == null || pane.zoomStack.IsEmpty ) ?
					"Zoom" : pane.zoomStack.Top.TypeString );
				this.contextMenu.MenuItems.Add( menuItem );
				menuItem.Click += new EventHandler( this.MenuClick_ZoomOut );
				if ( pane == null || pane.zoomStack.IsEmpty )
					menuItem.Enabled = false;

				menuItem = new MenuItem();
				menuItem.Index = index++;
				menuItem.Text = "Undo All Zoom/Pan";
				this.contextMenu.MenuItems.Add( menuItem );
				menuItem.Click += new EventHandler( this.MenuClick_ZoomOutAll );
				if ( pane == null || pane.zoomStack.IsEmpty )
					menuItem.Enabled = false;

				menuItem = new MenuItem();
				menuItem.Index = index++;
				menuItem.Text = "Set Scale to Default";
				this.contextMenu.MenuItems.Add( menuItem );
				menuItem.Click += new EventHandler( this.MenuClick_RestoreScale );
				if ( pane == null )
					menuItem.Enabled = false;

				// Provide Callback for User to edit the context menu
				this.ContextMenuBuilder( this, this.contextMenu );
			}

		}
		
		/// <summary>
		/// Handler for the "Copy" context menu item.  Copies the current image to a bitmap on the
		/// clipboard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_Copy( System.Object sender, System.EventArgs e )
		{
			Clipboard.SetDataObject( this.MasterPane.Image, true );
			MessageBox.Show( "Image Copied to ClipBoard" );
		}

		/// <summary>
		/// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
		/// file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_SaveAs( System.Object sender, System.EventArgs e )
		{
			SaveFileDialog saveDlg = new SaveFileDialog();
			saveDlg.Filter = "PNG Format (*.png)|*.png|" +
							"Gif Format (*.gif)|*.gif|" +
							"Jpeg Format (*.jpg)|*.jpg|" +
							"Tiff Format (*.tif)|*.tif|" +
							"Bmp Format (*.bmp)|*.bmp";

			if ( saveDlg.ShowDialog() == DialogResult.OK )
			{
				ImageFormat format = ImageFormat.Png;
				if ( saveDlg.FilterIndex == 2 )
					format = ImageFormat.Gif;
				else if ( saveDlg.FilterIndex == 3 )
					format = ImageFormat.Jpeg;
				else if ( saveDlg.FilterIndex == 4 )
					format = ImageFormat.Tiff;
				else if ( saveDlg.FilterIndex == 5 )
					format = ImageFormat.Bmp;
				
				Stream myStream = saveDlg.OpenFile();
				if ( myStream != null)
				{
					this.MasterPane.Image.Save( myStream, format );
					myStream.Close();
				}
			}
		}

		/// <summary>
		/// Handler for the "Show Values" context menu item.  Toggles the <see cref="IsShowPointValues"/>
		/// property, which activates the point value tooltips.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_ShowValues( System.Object sender, System.EventArgs e )
		{
			this.IsShowPointValues = ! ((MenuItem)sender).Checked;
		}

		/// <summary>
		/// Handler for the "Set Scale to Default" context menu item.  Sets the scale ranging to
		/// full auto mode for all axes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_RestoreScale( System.Object sender, EventArgs e )
		{
			GraphPane pane = this.MasterPane.FindPane( this.PointToClient( Control.MousePosition ) );
			if ( pane != null )
			{
				Graphics g = this.CreateGraphics();
				pane.XAxis.ResetAutoScale( pane, g );
				pane.YAxis.ResetAutoScale( pane, g );
				pane.Y2Axis.ResetAutoScale( pane, g );
				g.Dispose();
				Refresh();

				pane.zoomStack.Clear();
			}
		}

		/// <summary>
		/// Handler for the "UnZoom/UnPan" context menu item.  Restores the scale ranges to the values
		/// before the last zoom or pan operation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_ZoomOut( System.Object sender, System.EventArgs e )
		{
			GraphPane pane = this.MasterPane.FindPane( this.PointToClient( Control.MousePosition ) );
			if ( pane != null && !pane.zoomStack.IsEmpty )
			{
				pane.zoomStack.Pop( pane );
				Refresh();
			}
		}

		/// <summary>
		/// Handler for the "Undo All Zoom/Pan" context menu item.  Restores the scale ranges to the values
		/// before all zoom and pan operations
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_ZoomOutAll( System.Object sender, System.EventArgs e )
		{
			GraphPane pane = this.MasterPane.FindPane( this.PointToClient( Control.MousePosition ) );
			if ( pane != null && !pane.zoomStack.IsEmpty )
			{
				pane.zoomStack.PopAll( pane );
				Refresh();
			}
		}

		#endregion

	}
}
