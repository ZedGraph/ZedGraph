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
	/// <version> $Revision: 3.26 $ $Date: 2005-07-15 16:50:37 $ </version>
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
		/// Private value that determines whether or not panning is allowed for the control in the
		/// horizontal direction.  Use the
		/// public property <see cref="IsEnableHPan"/> to access this value.
		/// </summary>
		private bool isEnableHPan = true;
		/// <summary>
		/// Private value that determines whether or not panning is allowed for the control in the
		/// vertical direction.  Use the
		/// public property <see cref="IsEnableVPan"/> to access this value.
		/// </summary>
		private bool isEnableVPan = true;

        /// <summary>
        /// Private values that determine which mouse button and key combinations trigger pan and zoom
        /// events.  Use the public properties <see cref="ZoomButtons"/>, <see cref="ZoomButtons2"/>,
        /// <see cref="PanButtons2"/>, <see cref="PanButtons2"/>, <see cref="ZoomModifierKeys"/>,
        /// <see cref="ZoomModifierKeys2"/>, <see cref="PanModifierKeys"/>, and
        /// <see cref="ZoomModifierKeys2"/> to access these values.
        /// </summary>
        private MouseButtons zoomButtons = MouseButtons.Left;
        private Keys zoomModifierKeys = Keys.None;
        private MouseButtons zoomButtons2 = MouseButtons.None;
        private Keys zoomModifierKeys2 = Keys.None;
        private MouseButtons panButtons = MouseButtons.Left;
        private Keys panModifierKeys = Keys.Shift;
        private MouseButtons panButtons2 = MouseButtons.Middle;
        private Keys panModifierKeys2 = Keys.None;

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

		private double		scrollMinX = 0;
		private double		scrollMaxX = 0;
		private double		scrollMinY = 0;
		private double		scrollMaxY = 0;
		private double		scrollMinY2 = 0;
		private double		scrollMaxY2 = 0;
		private bool		isShowHScrollBar = false;
		private bool		isShowVScrollBar = false;
		private bool		isScrollY2 = false;

		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.VScrollBar vScrollBar1;

		private const int	ScrollRange = 1000;

		private bool		isZoomOnMouseCenter = false;

		/// <summary>
		/// A delegate that allows subscribing methods to append or modify the context menu.
		/// </summary>
		/// <remarks>
		/// The context menu is built on the fly after a right mouse click.  You can add menu items
		/// to this menu by simply modifying the <see paramref="menu"/> parameter.
		/// </remarks>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="menu">A reference to the <see cref="ContextMenu"/> object that contains
		/// the context menu.</param>
		public delegate void ContextMenuBuilderEventHandler( object sender, ContextMenu menu );
		/// <summary>
		/// Subscribe to this event to be able to modify the ZedGraph context menu.
		/// </summary>
		public event ContextMenuBuilderEventHandler ContextMenuBuilder;

		/// <summary>
		/// A delegate that allows notification of zoom and pan events.
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> before the zoom or pan event.</param>
		/// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> after the zoom or pan event</param>
		public delegate void ZoomEventHandler( object sender, ZoomState oldState, ZoomState newState );

		/// <summary>
		/// Subscribe to this event to be notified when the <see cref="GraphPane"/> is zoomed or panned by the user,
		/// either via a mouse drag operation or by the context menu commands.
		/// </summary>
		public event ZoomEventHandler ZoomEvent;

		/// <summary>
		/// A delegate that allows custom formatting of the point value tooltips
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="pane">The <see cref="GraphPane"/> object that contains the point value of interest</param>
		/// <param name="curve">The <see cref="CurveItem"/> object that contains the point value of interest</param>
		/// <param name="iPt">The integer index of the selected <see cref="PointPair"/> within the
		/// <see cref="PointPairList"/> of the selected <see cref="CurveItem"/></param>
		public delegate string PointValueHandler( object sender, GraphPane pane, CurveItem curve, int iPt );

		/// <summary>
		/// Subscribe to this event to provide custom formatting for the tooltips
		/// </summary>
		public event PointValueHandler PointValueEvent;

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
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
// 
// pointToolTip
// 
			this.pointToolTip.AutoPopDelay = 5000;
			this.pointToolTip.InitialDelay = 100;
			this.pointToolTip.ReshowDelay = 0;
// 
// contextMenu
// 
			//this.contextMenu.Name = "contextMenu";
			this.contextMenu.Popup += new System.EventHandler( this.ContextMenu_Popup );
// 
// hScrollBar1
// 
			this.hScrollBar1.Location = new System.Drawing.Point( 0, 128 );
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size( 128, 17 );
			this.hScrollBar1.TabIndex = 0;
			this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler( this.hScrollBar1_Scroll );
// 
// vScrollBar1
// 
			this.vScrollBar1.Location = new System.Drawing.Point( 128, 0 );
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size( 17, 128 );
			this.vScrollBar1.TabIndex = 1;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler( this.vScrollBar1_Scroll );
// 
// ZedGraphControl
// 
			this.ContextMenu = this.contextMenu;
			this.Controls.Add( this.vScrollBar1 );
			this.Controls.Add( this.hScrollBar1 );
			this.Name = "ZedGraphControl";
			this.Resize += new System.EventHandler( this.ChangeSize );
			this.MouseMove += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseMove );
			this.KeyUp += new System.Windows.Forms.KeyEventHandler( this.ZedGraphControl_KeyUp );
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseWheel );
			this.MouseUp += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseUp );
			this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseDown );
			this.KeyDown += new System.Windows.Forms.KeyEventHandler( this.ZedGraphControl_KeyDown );
			this.ResumeLayout( false );

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
			masterPane.MarginAll = 0;
			masterPane.IsShowTitle = false;

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
		/// Gets or sets a value that determines whether or not panning is allowed for the control in
		/// the horizontal direction.
		/// </summary>
		/// <remarks>
		/// Panning is done by clicking the middle mouse button (or holding down the shift key
		/// while clicking the left mouse button) inside the <see cref="ZedGraph.GraphPane.AxisRect"/> and
		/// dragging the mouse around to shift the scale ranges as desired.
		/// </remarks>
		/// <seealso cref="IsEnableVPan"/>
		public bool IsEnableHPan
		{
			get { return isEnableHPan; }
			set { isEnableHPan = value; }
		}

        /// <summary>
        /// Gets or sets a value that determines which mouse button will be used as a primary option
        /// to trigger a zoom event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="ZoomModifierKeys"/> to determine the actual zoom combination.
        /// A secondary zoom button/key combination option is available via <see cref="ZoomButtons2"/> and
        /// <see cref="ZoomModifierKeys2"/>.  To not use this button/key combination, set the value
        /// of <see cref="ZoomButtons"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public MouseButtons ZoomButtons
        {
            get { return zoomButtons; }
            set { zoomButtons = value; }
        }
        /// <summary>
        /// Gets or sets a value that determines which mouse button will be used as the secondary option
        /// to trigger a zoom event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="ZoomModifierKeys2"/> to determine the actual zoom combination.
        /// The primary zoom button/key combination option is available via <see cref="ZoomButtons"/> and
        /// <see cref="ZoomModifierKeys"/>.  To not use this button/key combination, set the value
        /// of <see cref="ZoomButtons2"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public MouseButtons ZoomButtons2
        {
            get { return zoomButtons2; }
            set { zoomButtons2 = value; }
        }
        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used as a primary option
        /// to trigger a zoom event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="ZoomButtons"/> to determine the actual zoom combination.
        /// A secondary zoom button/key combination option is available via <see cref="ZoomButtons2"/> and
        /// <see cref="ZoomModifierKeys2"/>.  To not use this button/key combination, set the value
        /// of <see cref="ZoomButtons"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public Keys ZoomModifierKeys
        {
            get { return zoomModifierKeys; }
            set { zoomModifierKeys = value; }
        }
        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used as a secondary option
        /// to trigger a zoom event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="ZoomButtons2"/> to determine the actual zoom combination.
        /// A primary zoom button/key combination option is available via <see cref="ZoomButtons"/> and
        /// <see cref="ZoomModifierKeys"/>.  To not use this button/key combination, set the value
        /// of <see cref="ZoomButtons2"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public Keys ZoomModifierKeys2
        {
            get { return zoomModifierKeys2; }
            set { zoomModifierKeys2 = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines which mouse button will be used as a primary option
        /// to trigger a pan event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="PanModifierKeys"/> to determine the actual pan combination.
        /// A secondary pan button/key combination option is available via <see cref="PanButtons2"/> and
        /// <see cref="PanModifierKeys2"/>.  To not use this button/key combination, set the value
        /// of <see cref="PanButtons"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public MouseButtons PanButtons
        {
            get { return panButtons; }
            set { panButtons = value; }
        }
        /// <summary>
        /// Gets or sets a value that determines which mouse button will be used as the secondary option
        /// to trigger a pan event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="PanModifierKeys2"/> to determine the actual pan combination.
        /// The primary pan button/key combination option is available via <see cref="PanButtons"/> and
        /// <see cref="PanModifierKeys"/>.  To not use this button/key combination, set the value
        /// of <see cref="PanButtons2"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public MouseButtons PanButtons2
        {
            get { return panButtons2; }
            set { panButtons2 = value; }
        }
        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used as a primary option
        /// to trigger a pan event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="PanButtons"/> to determine the actual pan combination.
        /// A secondary pan button/key combination option is available via <see cref="PanButtons2"/> and
        /// <see cref="PanModifierKeys2"/>.  To not use this button/key combination, set the value
        /// of <see cref="PanButtons"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public Keys PanModifierKeys
        {
            get { return panModifierKeys; }
            set { panModifierKeys = value; }
        }
        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used as a secondary option
        /// to trigger a pan event.
        /// </summary>
        /// <remarks>
        /// This value is combined with <see cref="PanButtons2"/> to determine the actual pan combination.
        /// A primary pan button/key combination option is available via <see cref="PanButtons"/> and
        /// <see cref="PanModifierKeys"/>.  To not use this button/key combination, set the value
        /// of <see cref="PanButtons2"/> to <see cref="MouseButtons.None"/>.
        /// </remarks>
        public Keys PanModifierKeys2
        {
            get { return panModifierKeys2; }
            set { panModifierKeys2 = value; }
        }

		/// <summary>
		/// Gets or sets a value that determines whether or not panning is allowed for the control in
		/// the vertical direction.
		/// </summary>
		/// <remarks>
		/// Panning is done by clicking the middle mouse button (or holding down the shift key
		/// while clicking the left mouse button) inside the <see cref="ZedGraph.GraphPane.AxisRect"/> and
		/// dragging the mouse around to shift the scale ranges as desired.
		/// </remarks>
		/// <seealso cref="IsEnableHPan"/>
		public bool IsEnableVPan
		{
			get { return isEnableVPan; }
			set { isEnableVPan = value; }
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
		/// Gets or sets a value that determines if the horizontal scroll bar will be visible.
		/// </summary>
		/// <remarks>This scroll bar allows the display to be scrolled in the horizontal direction.
		/// Another option is display panning, in which the user can move the display around by
		/// clicking directly on it and dragging (see <see cref="IsEnableHPan"/> and <see cref="IsEnableVPan"/>).
		/// You can control the available range of scrolling with the <see cref="ScrollMinX"/> and
		/// <see cref="ScrollMaxX"/> properties.
		/// </remarks>
		/// <value>A boolean value.  true to display a horizontal scrollbar, false otherwise.</value>
		public bool IsShowHScrollBar
		{
			get { return isShowHScrollBar; }
			set { isShowHScrollBar = value; ChangeSize( this, new EventArgs() ); }
		}
		/// <summary>
		/// Gets or sets a value that determines if the vertical scroll bar will be visible.
		/// </summary>
		/// <remarks>This scroll bar allows the display to be scrolled in the vertical direction.
		/// Another option is display panning, in which the user can move the display around by
		/// clicking directly on it and dragging (see <see cref="IsEnableHPan"/> and <see cref="IsEnableVPan"/>).
		/// You can control the available range of scrolling with the <see cref="ScrollMinY"/> and
		/// <see cref="ScrollMaxY"/> properties.
		/// Note that the vertical scroll bar only affects the <see cref="YAxis"/>; it has no impact on
		/// the <see cref="Y2Axis"/>.  The panning options affect both the <see cref="YAxis"/> and
		/// <see cref="Y2Axis"/>.
		/// </remarks>
		/// <value>A boolean value.  true to display a vertical scrollbar, false otherwise.</value>
		public bool IsShowVScrollBar
		{
			get { return isShowVScrollBar; }
			set { isShowVScrollBar = value; ChangeSize( this, new EventArgs() ); }
		}

        /// <summary>
        /// Gets or sets a value that determines if the vertical scroll bar will affect the Y2 axis.
        /// </summary>
        /// <remarks>
        /// The vertical scroll bar is automatically associated with the Y axis.  With this value, you
        /// can choose to include or exclude the Y2 axis with the scrolling.  Note that the Y2 axis
        /// scrolling is handled as a secondary.  The vertical scroll bar position always reflects
        /// the status of the Y axis.  This can cause the Y2 axis to "jump" when first scrolled if
        /// the <see cref="ScrollMinY2" /> and <see cref="ScrollMaxY2" /> values are not set to the
        /// same proportions as <see cref="ScrollMinY" /> and <see cref="ScrollMaxY" /> with respect
        /// to the actual <see cref="Axis.Min"/> and <see cref="Axis.Max" />.
        /// </remarks>
        /// <seealso cref="IsShowVScrollBar"/>
        /// <seealso cref="ScrollMinY2"/>
        /// <seealso cref="ScrollMaxY2"/>
        public bool IsScrollY2
        {
            get { return isScrollY2; }
            set { isScrollY2 = value; }
        }

		/// <summary>
		/// The minimum value for the X axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the minimum endpoint of the scroll range will cause the
		/// <see cref="Axis.Min"/> value to be set to <see cref="ScrollMinX"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableHPan"/>)
		/// is not affected by this value.
		/// </remarks>
		/// <value>A double value indicating the minimum axis value</value>
		public double ScrollMinX
		{
			get { return scrollMinX; }
			set { scrollMinX = value; }
		}
		/// <summary>
		/// The maximum value for the X axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the maximum endpoint of the scroll range will cause the
		/// <see cref="Axis.Max"/> value to be set to <see cref="ScrollMaxX"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableHPan"/>)
		/// is not affected by this value.
		/// </remarks>
		/// <value>A double value indicating the maximum axis value</value>
		public double ScrollMaxX
		{
			get { return scrollMaxX; }
			set { scrollMaxX = value; }
		}
		/// <summary>
		/// The minimum value for the Y axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the minimum endpoint of the scroll range will cause the
		/// <see cref="Axis.Min"/> value to be set to <see cref="ScrollMinY"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.
		/// </remarks>
		/// <value>A double value indicating the minimum axis value</value>
		public double ScrollMinY
		{
			get { return scrollMinY; }
			set { scrollMinY = value; }
		}
		/// <summary>
		/// The maximum value for the Y axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the maximum endpoint of the scroll range will cause the
		/// <see cref="Axis.Max"/> value to be set to <see cref="ScrollMaxY"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.
		/// </remarks>
		/// <value>A double value indicating the maximum axis value</value>
		public double ScrollMaxY
		{
			get { return scrollMaxY; }
			set { scrollMaxY = value; }
		}
		/// <summary>
		/// The minimum value for the Y2 axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the minimum endpoint of the scroll range will cause the
		/// <see cref="Axis.Min"/> value to be set to <see cref="ScrollMinY2"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.
		/// </remarks>
		/// <value>A double value indicating the minimum axis value</value>
		public double ScrollMinY2
		{
			get { return scrollMinY2; }
			set { scrollMinY2 = value; }
		}
		/// <summary>
		/// The maximum value for the Y2 axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the maximum endpoint of the scroll range will cause the
		/// <see cref="Axis.Max"/> value to be set to <see cref="ScrollMaxY2"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.
		/// </remarks>
		/// <value>A double value indicating the maximum axis value</value>
		public double ScrollMaxY2
		{
			get { return scrollMaxY2; }
			set { scrollMaxY2 = value; }
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

				SetScroll( hScrollBar1, this.GraphPane.XAxis, scrollMinX, scrollMaxX );
				SetScroll( vScrollBar1, this.GraphPane.YAxis, scrollMinY, scrollMaxY );

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

				Size newSize = this.Size;
				//if ( this.vScrollBar1.Visible )
				//if ( this.hScrollBar1.Visible )

				//this.vScrollBar1.Visible = true;

				if ( isShowHScrollBar )
				{
					hScrollBar1.Visible = true;
					newSize.Height -= this.hScrollBar1.Size.Height;
					hScrollBar1.Location = new Point( 0, newSize.Height );
					hScrollBar1.Size = new Size( newSize.Width, hScrollBar1.Height );
				}
				else
					hScrollBar1.Visible = false;

				if ( isShowVScrollBar )
				{
					vScrollBar1.Visible = true;
					newSize.Width -= this.vScrollBar1.Size.Width;
					vScrollBar1.Location = new Point( newSize.Width, 0 );
					vScrollBar1.Size = new Size( vScrollBar1.Width, newSize.Height );
				}
				else
					vScrollBar1.Visible = false;

				Graphics g = this.CreateGraphics();
				this.masterPane.ReSize( g, new RectangleF( 0, 0, newSize.Width, newSize.Height ) );
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
			
			if ( pane != null &&
                    ( this.isEnableHPan || this.isEnableVPan ) &&
					( ( e.Button == this.panButtons && Control.ModifierKeys == this.panModifierKeys ) ||
                      ( e.Button == this.panButtons2 && Control.ModifierKeys == this.panModifierKeys2 ) ) )
			{
				isPanning = true;
				// Calculate the startPoint by using the PointToScreen
				// method.
				this.dragRect = new Rectangle( ((Control)sender).PointToScreen( new Point(e.X, e.Y) ),
					new Size( 1, 1 ) );
				this.dragPane = pane;
				this.zoomState = new ZoomState( this.dragPane, ZoomState.StateType.Pan );
			}
			else if ( pane != null && this.isEnableZoom &&
                    ( (e.Button == this.zoomButtons && Control.ModifierKeys == this.zoomModifierKeys) ||
                      (e.Button == this.zoomButtons2 && Control.ModifierKeys == this.zoomModifierKeys2)))
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

						ZoomState oldState = this.dragPane.zoomStack.Push( this.dragPane, ZoomState.StateType.Zoom );

						this.dragPane.XAxis.Min = Math.Min( x1, x2 );
						this.dragPane.XAxis.Max = Math.Max( x1, x2 );
						this.dragPane.YAxis.Min = Math.Min( y1, y2 );
						this.dragPane.YAxis.Max = Math.Max( y1, y2 );
						this.dragPane.Y2Axis.Min = Math.Min( yy1, yy2 );
						this.dragPane.Y2Axis.Max = Math.Max( yy1, yy2 );

						// Provide Callback to notify the user of zoom events
						if ( this.ZoomEvent != null )
							this.ZoomEvent( this, oldState, new ZoomState( this.dragPane, ZoomState.StateType.Zoom ) );

						Graphics g = this.CreateGraphics();
						this.dragPane.AxisChange( g );
						g.Dispose();
						Refresh();
					}
				}
				else if ( this.isPanning )
				{
					// push the prior saved zoomstate, since the scale ranges have already been changed on
					// the fly during the panning operation
					if ( this.zoomState.IsChanged( this.dragPane ) )
					{
						this.dragPane.zoomStack.Push( this.zoomState );

						// Provide Callback to notify the user of pan events
						if ( this.ZoomEvent != null )
							this.ZoomEvent( this, this.zoomState, new ZoomState( this.dragPane, ZoomState.StateType.Pan ) );
					}
				}
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

				if ( this.isEnableHPan )
					PanScale( this.dragPane.XAxis, x1, x2 );
				if ( this.isEnableVPan )
				{
					PanScale( this.dragPane.YAxis, y1, y2 );
					PanScale( this.dragPane.Y2Axis, yy1, yy2 );
				}

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
						// Provide Callback for User to customize the tooltips
						if ( this.PointValueEvent != null )
						{
							string label = this.PointValueEvent( this, pane, curve, iPt );
							if ( label != null && label.Length > 0 )
							{
								this.pointToolTip.SetToolTip( this, label );
								this.pointToolTip.Active = true;
							}
							else
								this.pointToolTip.Active = false;
						}
						else
						{
							
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

									this.pointToolTip.SetToolTip( this, "( " + xStr + ", " + yStr + " )" );

									//this.pointToolTip.SetToolTip( this,
									//	curve.Points[iPt].ToString( this.pointValueFormat ) );
								}
							}

							this.pointToolTip.Active = true;
						}
					}
					else
						this.pointToolTip.Active = false;
				}
				else
					this.pointToolTip.Active = false;

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
			if ( ( isEnableHPan || isEnableVPan ) && ( Control.ModifierKeys == Keys.Shift || isPanning ) &&
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
				if ( this.ContextMenuBuilder != null )
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
				//Go ahead and save the old zoomstates, which provides an "undo"-like capability
				ZoomState oldState = pane.zoomStack.Push( pane, ZoomState.StateType.Zoom );
				//ZoomState oldState = new ZoomState( pane, ZoomState.StateType.Zoom );

				Graphics g = this.CreateGraphics();
				pane.XAxis.ResetAutoScale( pane, g );
				pane.YAxis.ResetAutoScale( pane, g );
				pane.Y2Axis.ResetAutoScale( pane, g );

				// Provide Callback to notify the user of zoom events
				if ( this.ZoomEvent != null )
					this.ZoomEvent( this, oldState, new ZoomState( pane, ZoomState.StateType.Zoom ) );

				g.Dispose();
				Refresh();

				//pane.zoomStack.Clear();
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
				ZoomState oldState = new ZoomState( pane, ZoomState.StateType.Zoom );
				ZoomState newState = pane.zoomStack.Pop( pane );

				// Provide Callback to notify the user of zoom events
				if ( this.ZoomEvent != null )
					this.ZoomEvent( this, oldState, newState );

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
				ZoomState oldState = new ZoomState( pane, ZoomState.StateType.Zoom );
				ZoomState newState = pane.zoomStack.PopAll( pane );

				// Provide Callback to notify the user of zoom events
				if ( this.ZoomEvent != null )
					this.ZoomEvent( this, oldState, newState );

				Refresh();
			}
		}

		#endregion

	#region ScrollBars

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
            HandleScroll(this.GraphPane.YAxis, e.NewValue, scrollMinY, scrollMaxY, !this.GraphPane.YAxis.IsReverse);
            if ( this.isScrollY2 )
                HandleScroll(this.GraphPane.Y2Axis, e.NewValue, scrollMinY2, scrollMaxY2, !this.GraphPane.Y2Axis.IsReverse);
        }

		private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			HandleScroll( this.GraphPane.XAxis, e.NewValue, scrollMinX, scrollMaxX, this.GraphPane.XAxis.IsReverse );
		}

		private void HandleScroll( Axis axis, int newValue, double scrollMin, double scrollMax, bool reverse )
		{
			if ( reverse )
				newValue = ScrollRange - newValue;

			if ( axis.IsLog )
			{
				double ratio = axis.Max / axis.Min;
				double scrollMin2 = scrollMax / ratio;

				double value = scrollMin * Math.Exp( (double) newValue / (double) ScrollRange * ( Math.Log( scrollMin2 ) - Math.Log( scrollMin ) ) );
				axis.Min = value;
				axis.Max = value * ratio;
			}
			else
			{
				double delta = axis.Max - axis.Min;
				double scrollMin2 = scrollMax - delta;

				double value = scrollMin + (double) newValue / (double) ScrollRange * ( scrollMin2 - scrollMin );
				axis.Min = value;
				axis.Max = value + delta;
			}

			this.Invalidate();
		}

		private void SetScroll( ScrollBar scrollBar, Axis axis, double scrollMin, double scrollMax )
		{
			int value = 0;
			double scrollMin2;

			if ( axis.IsLog )
				scrollMin2 = scrollMax / ( axis.Max / axis.Min );
			else
				scrollMin2 = scrollMax - ( axis.Max - axis.Min );

			if ( scrollMin >= scrollMin2 )
			{
				scrollBar.Enabled = false;
				scrollBar.Value = 0;
				scrollBar.Minimum = 0;
				scrollBar.Maximum = 0;
			}
			else
			{
				scrollBar.Enabled = true;
				scrollBar.Minimum = 0;
				scrollBar.Maximum = ScrollRange + scrollBar.LargeChange - 1;
				if ( axis.IsLog )
					value = (int) ( ( Math.Log( axis.Min ) - Math.Log( scrollMin ) ) / ( Math.Log( scrollMin2 ) - Math.Log( scrollMin ) ) * ScrollRange );
				else
					value = (int) ( ( axis.Min - scrollMin ) / ( scrollMin2 - scrollMin ) * ScrollRange );

				if ( value < 0 )
					value = 0;
				else if ( value > ScrollRange )
					value = ScrollRange;

				//if ( ( axis is XAxis && axis.IsReverse ) || ( ( ! axis is XAxis ) && ! axis.IsReverse ) )
				if ( (axis is XAxis) == axis.IsReverse )
					value = ScrollRange - value;

				scrollBar.Value = value;
			}
		}

	#endregion

	}
}
