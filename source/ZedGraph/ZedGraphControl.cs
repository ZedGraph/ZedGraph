//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright © 2004  John Champion
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Text;
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
	/// <version> $Revision: 3.59.2.12 $ $Date: 2006-05-14 03:31:19 $ </version>
	public partial class ZedGraphControl : UserControl
	{

	#region Private Fields

		/// <summary>
		/// This private field contains the instance for the MasterPane object of this control.
		/// You can access the MasterPane object through the public property
		/// <see cref="ZedGraphControl.MasterPane"/>. This is nulled when this Control is
		/// disposed.
		/// </summary>
		private MasterPane _masterPane;

		/// <summary>
		/// private field that determines whether or not tooltips will be displayed
		/// when the mouse hovers over data values.  Use the public property
		/// <see cref="IsShowPointValues"/> to access this value.
		/// </summary>
		private bool _isShowPointValues;
		/// <summary>
		/// private field that determines whether or not tooltips will be displayed
		/// showing the scale values while the mouse is located within the ChartRect.
		/// Use the public property <see cref="IsShowCursorValues"/> to access this value.
		/// </summary>
		private bool _isShowCursorValues;
		/// <summary>
		/// private field that determines the format for displaying tooltip values.
		/// This format is passed to <see cref="PointPair.ToString(string)"/>.
		/// Use the public property <see cref="PointValueFormat"/> to access this
		/// value.
		/// </summary>
		private string _pointValueFormat;

		/// <summary>
		/// private field that determines whether or not the context menu will be available.  Use the
		/// public property <see cref="IsShowContextMenu"/> to access this value.
		/// </summary>
		private bool _isShowContextMenu;

		/// <summary>
		/// private field that determines whether or not a message box will be shown in response to
		/// a context menu "Copy" command.  Use the
		/// public property <see cref="IsShowCopyMessage"/> to access this value.
		/// </summary>
		/// <remarks>
		/// Note that, if this value is set to false, the user will receive no indicative feedback
		/// in response to a Copy action.
		/// </remarks>
		private bool _isShowCopyMessage;

		/// <summary>
		/// private field that determines whether the settings of
		/// <see cref="ZedGraph.PaneBase.IsFontsScaled" /> and <see cref="PaneBase.IsPenWidthScaled" />
		/// will be overridden to true during printing operations.
		/// </summary>
		/// <remarks>
		/// Printing involves pixel maps that are typically of a dramatically different dimension
		/// than on-screen pixel maps.  Therefore, it becomes more important to scale the fonts and
		/// lines to give a printed image that looks like what is shown on-screen.  The default
		/// setting for <see cref="ZedGraph.PaneBase.IsFontsScaled" /> is true, but the default
		/// setting for <see cref="PaneBase.IsPenWidthScaled" /> is false.
		/// </remarks>
		/// <value>
		/// A value of true will cause both <see cref="ZedGraph.PaneBase.IsFontsScaled" /> and
		/// <see cref="PaneBase.IsPenWidthScaled" /> to be temporarily set to true during
		/// printing operations.
		/// </value>
		private bool _isPrintScaleAll;
		/// <summary>
		/// private field that determines whether or not the visible aspect ratio of the
		/// <see cref="MasterPane" /> <see cref="PaneBase.Rect" /> will be preserved
		/// when printing this <see cref="ZedGraphControl" />.
		/// </summary>
		private bool _isPrintKeepAspectRatio;
		/// <summary>
		/// private field that determines whether or not the <see cref="MasterPane" />
		/// <see cref="PaneBase.Rect" /> dimensions will be expanded to fill the
		/// available space when printing this <see cref="ZedGraphControl" />.
		/// </summary>
		/// <remarks>
		/// If <see cref="IsPrintKeepAspectRatio" /> is also true, then the <see cref="MasterPane" />
		/// <see cref="PaneBase.Rect" /> dimensions will be expanded to fit as large
		/// a space as possible while still honoring the visible aspect ratio.
		/// </remarks>
		private bool _isPrintFillPage;

		/// <summary>
		/// private field that determines the format for displaying tooltip date values.
		/// This format is passed to <see cref="XDate.ToString(string)"/>.
		/// Use the public property <see cref="PointDateFormat"/> to access this
		/// value.
		/// </summary>
		private string _pointDateFormat;

		/// <summary>
		/// private value that determines whether or not zooming is enabled for the control in the
		/// vertical direction.  Use the public property <see cref="IsEnableVZoom"/> to access this
		/// value.
		/// </summary>
		private bool _isEnableVZoom = true;
		/// <summary>
		/// private value that determines whether or not zooming is enabled for the control in the
		/// horizontal direction.  Use the public property <see cref="IsEnableHZoom"/> to access this
		/// value.
		/// </summary>
		private bool _isEnableHZoom = true;

		/// <summary>
		/// private value that determines whether or not point editing is enabled in the
		/// vertical direction.  Use the public property <see cref="IsEnableVEdit"/> to access this
		/// value.
		/// </summary>
		private bool _isEnableVEdit = false;
		/// <summary>
		/// private value that determines whether or not point editing is enabled in the
		/// horizontal direction.  Use the public property <see cref="IsEnableHEdit"/> to access this
		/// value.
		/// </summary>
		private bool _isEnableHEdit = false;

		/// <summary>
		/// private value that determines whether or not panning is allowed for the control in the
		/// horizontal direction.  Use the
		/// public property <see cref="IsEnableHPan"/> to access this value.
		/// </summary>
		private bool _isEnableHPan = true;
		/// <summary>
		/// private value that determines whether or not panning is allowed for the control in the
		/// vertical direction.  Use the
		/// public property <see cref="IsEnableVPan"/> to access this value.
		/// </summary>
		private bool _isEnableVPan = true;

		private double _zoomStepFraction = 0.1;

		private ScrollRange _xScrollRange;

		private ScrollRangeList _yScrollRangeList;
		private ScrollRangeList _y2ScrollRangeList;

		private bool _isShowHScrollBar = false;
		private bool _isShowVScrollBar = false;
		//private bool		isScrollY2 = false;
		private bool _isAutoScrollRange = false;

		private bool _isSynchronizeXAxes = false;
		private bool _isSynchronizeYAxes = false;

		//private System.Windows.Forms.HScrollBar hScrollBar1;
		//private System.Windows.Forms.VScrollBar vScrollBar1;

		// The range of values to use the scroll control bars
		private const int _ScrollControlSpan = int.MaxValue;
		// The ratio of the largeChange to the smallChange for the scroll bars
		private const int _ScrollSmallRatio = 10;

		private bool _isZoomOnMouseCenter = false;

		private ResourceManager _resourceManager;

		/// <summary>
		/// private field that stores a <see cref="PrintDocument" /> instance, which maintains
		/// a persistent selection of printer options.
		/// </summary>
		/// <remarks>
		/// This is needed so that a "Print" action utilizes the settings from a prior
		/// "Page Setup" action.</remarks>
		private PrintDocument _pdSave = null;
		//private PrinterSettings printSave = null;
		//private PageSettings pageSave = null;

	#endregion

	#region Fields: Buttons & Keys Properties

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used to click on
		/// linkable objects
		/// </summary>
		/// <seealso cref="LinkModifierKeys" />
		private MouseButtons _linkButtons = MouseButtons.Left;
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used to click
		/// on linkable objects
		/// </summary>
		/// <seealso cref="LinkButtons" />
		private Keys _linkModifierKeys = Keys.Alt;

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used to edit point
		/// data values
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHEdit" /> and/or
		/// <see cref="IsEnableVEdit" /> are true.
		/// </remarks>
		/// <seealso cref="EditModifierKeys" />
		private MouseButtons _editButtons = MouseButtons.Left;
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used to edit point
		/// data values
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHEdit" /> and/or
		/// <see cref="IsEnableVEdit" /> are true.
		/// </remarks>
		/// <seealso cref="EditButtons" />
		private Keys _editModifierKeys = Keys.Alt;

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used to perform
		/// zoom operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHZoom" /> and/or
		/// <see cref="IsEnableVZoom" /> are true.
		/// </remarks>
		/// <seealso cref="ZoomModifierKeys" />
		/// <seealso cref="ZoomButtons2" />
		/// <seealso cref="ZoomModifierKeys2" />
		private MouseButtons _zoomButtons = MouseButtons.Left;
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used to perform
		/// zoom operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHZoom" /> and/or
		/// <see cref="IsEnableVZoom" /> are true.
		/// </remarks>
		/// <seealso cref="ZoomButtons" />
		/// <seealso cref="ZoomButtons2" />
		/// <seealso cref="ZoomModifierKeys2" />
		private Keys _zoomModifierKeys = Keys.None;

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used as a
		/// secondary option to perform zoom operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHZoom" /> and/or
		/// <see cref="IsEnableVZoom" /> are true.
		/// </remarks>
		/// <seealso cref="ZoomModifierKeys2" />
		/// <seealso cref="ZoomButtons" />
		/// <seealso cref="ZoomModifierKeys" />
		private MouseButtons _zoomButtons2 = MouseButtons.None;
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used as a
		/// secondary option to perform zoom operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHZoom" /> and/or
		/// <see cref="IsEnableVZoom" /> are true.
		/// </remarks>
		/// <seealso cref="ZoomButtons" />
		/// <seealso cref="ZoomButtons2" />
		/// <seealso cref="ZoomModifierKeys2" />
		private Keys _zoomModifierKeys2 = Keys.None;

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used to perform
		/// panning operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHPan" /> and/or
		/// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
		/// the mouse) should not be confused with a scroll operation (using a scroll bar to
		/// move the graph).
		/// </remarks>
		/// <seealso cref="PanModifierKeys" />
		/// <seealso cref="PanButtons2" />
		/// <seealso cref="PanModifierKeys2" />
		private MouseButtons _panButtons = MouseButtons.Left;

		// Setting this field to Keys.Shift here
		// causes an apparent bug to crop up in VS 2003, by which it will have the value:
		// "System.Windows.Forms.Keys.Shift+None", which won't compile
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used to perform
		/// panning operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHPan" /> and/or
		/// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
		/// the mouse) should not be confused with a scroll operation (using a scroll bar to
		/// move the graph).
		/// </remarks>
		/// <seealso cref="PanButtons" />
		/// <seealso cref="PanButtons2" />
		/// <seealso cref="PanModifierKeys2" />
		private Keys _panModifierKeys = Keys.Shift;

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used as a
		/// secondary option to perform panning operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHPan" /> and/or
		/// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
		/// the mouse) should not be confused with a scroll operation (using a scroll bar to
		/// move the graph).
		/// </remarks>
		/// <seealso cref="PanModifierKeys2" />
		/// <seealso cref="PanButtons" />
		/// <seealso cref="PanModifierKeys" />
		private MouseButtons _panButtons2 = MouseButtons.Middle;

		// Setting this field to Keys.Shift here
		// causes an apparent bug to crop up in VS 2003, by which it will have the value:
		// "System.Windows.Forms.Keys.Shift+None", which won't compile
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used as a
		/// secondary option to perform panning operations
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHPan" /> and/or
		/// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
		/// the mouse) should not be confused with a scroll operation (using a scroll bar to
		/// move the graph).
		/// </remarks>
		/// <seealso cref="PanButtons2" />
		/// <seealso cref="PanButtons" />
		/// <seealso cref="PanModifierKeys" />
		private Keys _panModifierKeys2 = Keys.None;

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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which mouse button is used as the primary for zooming" )]
		public MouseButtons ZoomButtons
		{
			get { return _zoomButtons; }
			set { _zoomButtons = value; }
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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which mouse button is used as the secondary for zooming" )]
		public MouseButtons ZoomButtons2
		{
			get { return _zoomButtons2; }
			set { _zoomButtons2 = value; }
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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which modifier key used as the primary for zooming" )]
		public Keys ZoomModifierKeys
		{
			get { return _zoomModifierKeys; }
			set { _zoomModifierKeys = value; }
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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which modifier key used as the secondary for zooming" )]
		public Keys ZoomModifierKeys2
		{
			get { return _zoomModifierKeys2; }
			set { _zoomModifierKeys2 = value; }
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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which mouse button is used as the primary for panning" )]
		public MouseButtons PanButtons
		{
			get { return _panButtons; }
			set { _panButtons = value; }
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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which mouse button is used as the secondary for panning" )]
		public MouseButtons PanButtons2
		{
			get { return _panButtons2; }
			set { _panButtons2 = value; }
		}

		// NOTE: The default value of PanModifierKeys is Keys.Shift. Because of an apparent bug in
		// VS 2003, the initial value set in InitializeComponent by the code wizard is "Keys.Shift+None"
		// which will not compile.  As a temporary workaround, I've hidden the value so that it won't
		// have compile errors.  This problem does not exist in VS 2005.

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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which modifier key is used as the primary for panning" )]
		public Keys PanModifierKeys
		{
			get { return _panModifierKeys; }
			set { _panModifierKeys = value; }
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
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Determines which modifier key is used as the secondary for panning" )]
		public Keys PanModifierKeys2
		{
			get { return _panModifierKeys2; }
			set { _panModifierKeys2 = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used to edit point
		/// data values
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHEdit" /> and/or
		/// <see cref="IsEnableVEdit" /> are true.
		/// </remarks>
		/// <seealso cref="EditModifierKeys" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Specify mouse button for point editing" )]
		public MouseButtons EditButtons
		{
			get { return _editButtons; }
			set { _editButtons = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used to edit point
		/// data values
		/// </summary>
		/// <remarks>
		/// This setting only applies if <see cref="IsEnableHEdit" /> and/or
		/// <see cref="IsEnableVEdit" /> are true.
		/// </remarks>
		/// <seealso cref="EditButtons" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Specify modifier key for point editing" )]
		public Keys EditModifierKeys
		{
			get { return _editModifierKeys; }
			set { _editModifierKeys = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines which Mouse button will be used to click
		/// on linkable objects
		/// </summary>
		/// <seealso cref="LinkModifierKeys" />
		/// <seealso cref="LinkEvent"/>
		// /// <seealso cref="ZedGraph.Web.IsImageMap"/>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Specify mouse button for clicking on linkable objects" )]
		public MouseButtons LinkButtons
		{
			get { return _linkButtons; }
			set { _linkButtons = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines which modifier keys will be used to click
		/// on linkable objects
		/// </summary>
		/// <seealso cref="LinkButtons" />
		/// <seealso cref="LinkEvent"/>
		// /// <seealso cref="ZedGraph.Web.IsImageMap"/>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Specify modifier key for clicking on linkable objects" )]
		public Keys LinkModifierKeys
		{
			get { return _linkModifierKeys; }
			set { _linkModifierKeys = value; }
		}

	#endregion

	#region Fields: Temporary state variables

		/// <summary>
		/// Internal variable that indicates the control is currently being zoomed. 
		/// </summary>
		private bool _isZooming = false;
		/// <summary>
		/// Internal variable that indicates the control is currently being panned.
		/// </summary>
		private bool _isPanning = false;
		/// <summary>
		/// Internal variable that indicates a point value is currently being edited.
		/// </summary>
		private bool _isEditing = false;

		/// <summary>
		/// Internal variable that stores the <see cref="GraphPane"/> reference for the Pane that is
		/// currently being zoomed or panned.
		/// </summary>
		private GraphPane _dragPane = null;
		/// <summary>
		/// Internal variable that stores a rectangle which is either the zoom rectangle, or the incremental
		/// pan amount since the last mousemove event.
		/// </summary>
		private Point _dragStartPt;
		private Point _dragEndPt;

		private int _dragIndex;
		private CurveItem _dragCurve;
		private PointPair _dragStartPair;
		/// <summary>
		/// private field that stores the state of the scale ranges prior to starting a panning action.
		/// </summary>
		private ZoomState _zoomState;
		private ZoomStateStack _zoomStateStack;

		//temporarily save the location of a context menu click so we can use it for reference
		// Note that Control.MousePosition ends up returning the position after the mouse has
		// moved to the menu item within the context menu.  Therefore, this point is saved so
		// that we have the point at which the context menu was first right-clicked
		internal Point _menuClickPt;

	#endregion

	#region Events

		/// <summary>
		/// A delegate that allows subscribing methods to append or modify the context menu.
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="menuStrip">A reference to the <see cref="ContextMenuStrip"/> object
		/// that contains the context menu.
		/// </param>
		/// <param name="mousePt">The point at which the mouse was clicked</param>
		/// <seealso cref="ContextMenuBuilder" />
		public delegate void ContextMenuBuilderEventHandler( ZedGraphControl sender,
			ContextMenuStrip menuStrip, Point mousePt );
		/// <summary>
		/// Subscribe to this event to be able to modify the ZedGraph context menu.
		/// </summary>
		/// <remarks>
		/// The context menu is built on the fly after a right mouse click.  You can add menu items
		/// to this menu by simply modifying the <see paramref="menu"/> parameter.
		/// </remarks>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe to this event to be able to modify the ZedGraph context menu" )]
		public event ContextMenuBuilderEventHandler ContextMenuBuilder;

		/// <summary>
		/// A delegate that allows notification of zoom and pan events.
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> before the zoom or pan event.</param>
		/// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> after the zoom or pan event</param>
		/// <seealso cref="ZoomEvent" />
		public delegate void ZoomEventHandler( ZedGraphControl sender, ZoomState oldState,
			ZoomState newState );

		/// <summary>
		/// Subscribe to this event to be notified when the <see cref="GraphPane"/> is zoomed or panned by the user,
		/// either via a mouse drag operation or by the context menu commands.
		/// </summary>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe to this event to be notified when the graph is zoomed or panned" )]
		public event ZoomEventHandler ZoomEvent;

		/// <summary>
		/// A delegate that allows notification of scroll events.
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="scrollBar">The source <see cref="ScrollBar"/> object</param>
		/// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> before the scroll event.</param>
		/// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> after the scroll event</param>
		/// <seealso cref="ZoomEvent" />
		public delegate void ScrollDoneHandler( ZedGraphControl sender, ScrollBar scrollBar,
			ZoomState oldState, ZoomState newState );

		/// <summary>
		/// Subscribe to this event to be notified when the <see cref="GraphPane"/> is scrolled by the user
		/// using the scrollbars.
		/// </summary>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe this event to be notified when a scroll operation using the scrollbars is completed" )]
		public event ScrollDoneHandler ScrollDoneEvent;

		/// <summary>
		/// A delegate that allows notification of scroll events.
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="scrollBar">The source <see cref="ScrollBar"/> object</param>
		/// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> before the scroll event.</param>
		/// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
		/// <see cref="GraphPane"/> after the scroll event</param>
		/// <seealso cref="ZoomEvent" />
		public delegate void ScrollProgressHandler( ZedGraphControl sender, ScrollBar scrollBar,
			ZoomState oldState, ZoomState newState );

		/// <summary>
		/// Subscribe to this event to be notified when the <see cref="GraphPane"/> is scrolled by the user
		/// using the scrollbars.
		/// </summary>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe this event to be notified continuously as a scroll operation is taking place" )]
		public event ScrollProgressHandler ScrollProgressEvent;

		/// <summary>
		/// A delegate that allows custom formatting of the point value tooltips
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="pane">The <see cref="GraphPane"/> object that contains the point value of interest</param>
		/// <param name="curve">The <see cref="CurveItem"/> object that contains the point value of interest</param>
		/// <param name="iPt">The integer index of the selected <see cref="PointPair"/> within the
		/// <see cref="IPointList"/> of the selected <see cref="CurveItem"/></param>
		/// <seealso cref="PointValueEvent" />
		public delegate string PointValueHandler( ZedGraphControl sender, GraphPane pane,
			CurveItem curve, int iPt );

		/// <summary>
		/// Subscribe to this event to provide custom formatting for the tooltips
		/// </summary>
		/// <example>
		/// <para>To subscribe to this event, use the following in your FormLoad method:</para>
		/// <code>zedGraphControl1.PointValueEvent +=
		/// new ZedGraphControl.PointValueHandler( MyPointValueHandler );</code>
		/// <para>Add this method to your Form1.cs:</para>
		/// <code>
		///    private string MyPointValueHandler( object sender, GraphPane pane, CurveItem curve, int iPt )
		///    {
		///        PointPair pt = curve[iPt];
		///        return "This value is " + pt.Y.ToString("f2") + " gallons";
		///    }</code>
		/// </example>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe to this event to provide custom-formatting for data point tooltips" )]
		public event PointValueHandler PointValueEvent;

		/// <summary>
		/// A delegate that allows notification of mouse events on Graph objects.
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="e">A <see cref="MouseEventArgs" /> corresponding to this event</param>
		/// <seealso cref="MouseDownEvent" />
		/// <returns>
		/// Return true if you have handled the mouse event entirely, and you do not
		/// want the <see cref="ZedGraphControl"/> to do any further action (e.g., starting
		/// a zoom operation).  Return false if ZedGraph should go ahead and process the
		/// mouse event.
		/// </returns>
		public delegate bool ZedMouseEventHandler( ZedGraphControl sender, MouseEventArgs e );

		/// <summary>
		/// Subscribe to this event to provide notification of MouseDown clicks on graph
		/// objects
		/// </summary>
		/// <remarks>
		/// This event provides for a notification when the mouse is clicked on an object
		/// within any <see cref="GraphPane"/> of the <see cref="MasterPane"/> associated
		/// with this <see cref="ZedGraphControl" />.  This event will use the
		/// <see cref="ZedGraph.MasterPane.FindNearestPaneObject"/> method to determine which object
		/// was clicked.  The boolean value that you return from this handler determines whether
		/// or not the <see cref="ZedGraphControl"/> will do any further handling of the
		/// MouseDown event (see <see cref="ZedMouseEventHandler" />).  Return true if you have
		/// handled the MouseDown event entirely, and you do not
		/// want the <see cref="ZedGraphControl"/> to do any further action (e.g., starting
		/// a zoom operation).  Return false if ZedGraph should go ahead and process the
		/// MouseDown event.
		/// </remarks>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe to be notified when the left mouse button is clicked down" )]
		public event ZedMouseEventHandler MouseDownEvent;

		/// <summary>
		/// Hide the standard control MouseDown event so that the ZedGraphControl.MouseDownEvent
		/// can be used.  This is so that the user must return true/false in order to indicate
		/// whether or not we should respond to the event.
		/// </summary>
		[Bindable( false ), Browsable( false )]
		public new event MouseEventHandler MouseDown;
		/// <summary>
		/// Hide the standard control MouseUp event so that the ZedGraphControl.MouseUpEvent
		/// can be used.  This is so that the user must return true/false in order to indicate
		/// whether or not we should respond to the event.
		/// </summary>
		[Bindable( false ), Browsable( false )]
		public new event MouseEventHandler MouseUp;
		/// <summary>
		/// Hide the standard control MouseMove event so that the ZedGraphControl.MouseMoveEvent
		/// can be used.  This is so that the user must return true/false in order to indicate
		/// whether or not we should respond to the event.
		/// </summary>
		[Bindable( false ), Browsable( false )]
		private new event MouseEventHandler MouseMove;
		/// <summary>
		/// Subscribe to this event to provide notification of MouseUp clicks on graph
		/// objects
		/// </summary>
		/// <remarks>
		/// This event provides for a notification when the mouse is clicked on an object
		/// within any <see cref="GraphPane"/> of the <see cref="MasterPane"/> associated
		/// with this <see cref="ZedGraphControl" />.  This event will use the
		/// <see cref="ZedGraph.MasterPane.FindNearestPaneObject"/> method to determine which object
		/// was clicked.  The boolean value that you return from this handler determines whether
		/// or not the <see cref="ZedGraphControl"/> will do any further handling of the
		/// MouseUp event (see <see cref="ZedMouseEventHandler" />).  Return true if you have
		/// handled the MouseUp event entirely, and you do not
		/// want the <see cref="ZedGraphControl"/> to do any further action (e.g., starting
		/// a zoom operation).  Return false if ZedGraph should go ahead and process the
		/// MouseUp event.
		/// </remarks>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe to be notified when the left mouse button is released" )]
		public event ZedMouseEventHandler MouseUpEvent;
		/// <summary>
		/// Subscribe to this event to provide notification of MouseMove events over graph
		/// objects
		/// </summary>
		/// <remarks>
		/// This event provides for a notification when the mouse is moving over on the control.
		/// The boolean value that you return from this handler determines whether
		/// or not the <see cref="ZedGraphControl"/> will do any further handling of the
		/// MouseMove event (see <see cref="ZedMouseEventHandler" />).  Return true if you
		/// have handled the MouseMove event entirely, and you do not
		/// want the <see cref="ZedGraphControl"/> to do any further action.
		/// Return false if ZedGraph should go ahead and process the MouseMove event.
		/// </remarks>
		[Bindable( true ), Category( "Events" )]
		[Description( "Subscribe to be notified when the mouse is moved inside the control" )]
		public event ZedMouseEventHandler MouseMoveEvent;

		/// <summary>
		/// A delegate that allows notification of clicks on ZedGraph objects that have
		/// active links enabled
		/// </summary>
		/// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
		/// <param name="pane">The source <see cref="GraphPane" /> in which the click
		/// occurred.
		/// </param>
		/// <param name="source">The source object which was clicked.  This is typically
		/// a type of <see cref="CurveItem" /> if a curve point was clicked, or
		/// a type of <see cref="GraphObj" /> if a graph object was clicked.
		/// </param>
		/// <param name="link">The <see cref="Link" /> object, belonging to
		/// <paramref name="source" />, that contains the link information
		/// </param>
		/// <param name="index">An index value, typically used if a <see cref="CurveItem" />
		/// was clicked, indicating the ordinal value of the actual point that was clicked.
		/// </param>
		/// <returns>
		/// Return true if you have handled the LinkEvent entirely, and you do not
		/// want the <see cref="ZedGraphControl"/> to do any further action.
		/// Return false if ZedGraph should go ahead and process the LinkEvent.
		/// </returns>
		public delegate bool LinkEventHandler( ZedGraphControl sender, GraphPane pane,
			object source, Link link, int index );

		/// <summary>
		/// Subscribe to this event to be able to respond to mouse clicks within linked
		/// objects.
		/// </summary>
		/// <remarks>
		/// Linked objects are typically either <see cref="GraphObj" /> type objects or
		/// <see cref="CurveItem" /> type objects.  These object types can include
		/// hyperlink information allowing for "drill-down" type operation.  
		/// </remarks>
		/// <seealso cref="LinkEventHandler"/>
		/// <seealso cref="Link" />
		/// <seealso cref="CurveItem.Link">CurveItem.Link</seealso>
		/// <seealso cref="GraphObj.Link">GraphObj.Link</seealso>
		// /// <seealso cref="ZedGraph.Web.IsImageMap" />
		public event LinkEventHandler LinkEvent;

	#endregion

	#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphControl()
		{
			InitializeComponent();

			// Link in these events from the base class, since we disable them from this class.
			base.MouseDown += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseDown );
			base.MouseUp += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseUp );
			base.MouseMove += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseMove );

			//this.MouseWheel += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseWheel );

			// Use double-buffering for flicker-free updating:
			SetStyle( ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
				| ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true );
			//isTransparentBackground = false;
			//SetStyle( ControlStyles.Opaque, false );
			SetStyle( ControlStyles.SupportsTransparentBackColor, true );
			//this.BackColor = Color.Transparent;

			_resourceManager = new ResourceManager( "ZedGraph.ZedGraph.ZedGraphLocale",
				Assembly.GetExecutingAssembly() );

			Rectangle rect = new Rectangle( 0, 0, this.Size.Width, this.Size.Height );
			_masterPane = new MasterPane( "", rect );
			_masterPane.Margin.All = 0;
			_masterPane.Title.IsVisible = false;

			string titleStr = _resourceManager.GetString( "title_def" );
			string xStr = _resourceManager.GetString( "x_title_def" );
			string yStr = _resourceManager.GetString( "y_title_def" );

			//GraphPane graphPane = new GraphPane( rect, "Title", "X Axis", "Y Axis" );
			GraphPane graphPane = new GraphPane( rect, titleStr, xStr, yStr );
			Graphics g = this.CreateGraphics();
			graphPane.AxisChange( g );
			g.Dispose();
			_masterPane.Add( graphPane );

			this.hScrollBar1.Minimum = 0;
			this.hScrollBar1.Maximum = 100;
			this.hScrollBar1.Value = 0;

			this.vScrollBar1.Minimum = 0;
			this.vScrollBar1.Maximum = 100;
			this.vScrollBar1.Value = 0;

			_isShowPointValues = false;
			_isShowCursorValues = false;
			_isShowContextMenu = true;
			_isShowCopyMessage = true;
			_isPrintFillPage = true;
			_isPrintKeepAspectRatio = true;
			_isPrintScaleAll = true;

			_pointValueFormat = PointPair.DefaultFormat;
			_pointDateFormat = XDate.DefaultFormatStr;

			_xScrollRange = new ScrollRange( true );
			_yScrollRangeList = new ScrollRangeList();
			_y2ScrollRangeList = new ScrollRangeList();

			_yScrollRangeList.Add( new ScrollRange( true ) );
			_y2ScrollRangeList.Add( new ScrollRange( false ) );

			_zoomState = null;
			_zoomStateStack = new ZoomStateStack();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if the components should be
		/// disposed, false otherwise</param>
		protected override void Dispose( bool disposing )
		{
			lock ( this )
			{
				if ( disposing )
				{
					if ( components != null )
						components.Dispose();
				}
				base.Dispose( disposing );

				_masterPane = null;
			}
		}
	
	#endregion

	#region Properties

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.MasterPane"/> property for the control
		/// </summary>
		[Bindable( false ), Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public MasterPane MasterPane
		{
			get { lock ( this ) return _masterPane; }
			set { lock ( this ) _masterPane = value; }
		}

		// Testing for Designer attribute
		/*
		Class1 _class1 = null;
		[ Bindable( true ), Browsable( true ), Category( "Data" ), NotifyParentProperty( true ),
			DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
			Description( "My Class1 Test" )]
		public Class1 Class1
		{
			get { if ( _class1 == null ) _class1 = new Class1(); return _class1; }
			set { _class1 = value; }
		}
		*/
	
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.GraphPane"/> property for the control
		/// </summary>
		/// <remarks>
		/// <see cref="ZedGraphControl"/> actually uses a <see cref="MasterPane"/> object
		/// to hold a list of <see cref="GraphPane"/> objects.  This property really only
		/// accesses the first <see cref="GraphPane"/> in the list.  If there is more
		/// than one <see cref="GraphPane"/>, use the <see cref="MasterPane"/>
		/// indexer property to access any of the <see cref="GraphPane"/> objects.</remarks>
		[
			Bindable( false ), Browsable( false ),
			DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )
		]
		//[
		//	Bindable( true ), Browsable( true ), Category( "Data" ), NotifyParentProperty( true ),
		//	AttributeProvider( typeof( GraphPane ) ),
		//	Description("Access to the primary GraphPane object associated with this control")
		//]
		public GraphPane GraphPane
		{
			get
			{
				// Just return the first GraphPane in the list
				lock ( this )
				{
					if ( _masterPane != null && _masterPane.PaneList.Count > 0 )
						return _masterPane[0];
					else
						return null;
				}
			}

			set
			{
				lock ( this )
				{
					//Clear the list, and replace it with the specified Graphpane
					if ( _masterPane != null )
					{
						_masterPane.PaneList.Clear();
						_masterPane.Add( value );
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not tooltips will be displayed
		/// when the mouse hovers over data values.
		/// </summary>
		/// <remarks>The displayed values are taken from <see cref="PointPair.Tag"/>
		/// if it is a <see cref="System.String"/> type, or <see cref="PointPair.ToString()"/>
		/// otherwise (using the <see cref="PointValueFormat" /> as a format string).
		/// Additionally, the user can custom format the values using the
		/// <see cref="PointValueEvent" /> event.  Note that <see cref="IsShowPointValues" />
		/// may be overridden by <see cref="IsShowCursorValues" />.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to display tooltips when the mouse hovers over data points" )]
		public bool IsShowPointValues
		{
			get { return _isShowPointValues; }
			set { _isShowPointValues = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not tooltips will be displayed
		/// showing the current scale values when the mouse is within the
		/// <see cref="Chart.Rect" />.
		/// </summary>
		/// <remarks>The displayed values are taken from the current mouse position, and formatted
		/// according to <see cref="PointValueFormat" /> and/or <see cref="PointDateFormat" />.  If this
		/// value is set to true, it overrides the <see cref="IsShowPointValues" /> setting.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to display tooltips showing the current mouse position within the Chart area" )]
		public bool IsShowCursorValues
		{
			get { return _isShowCursorValues; }
			set { _isShowCursorValues = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not editing of point data is allowed in
		/// the horizontal direction.
		/// </summary>
		/// <remarks>
		/// Editing is done by holding down the Alt key, and left-clicking on an individual point of
		/// a given <see cref="CurveItem" /> to drag it to a new location.  The Mouse and Key
		/// combination for this mode are modifiable using <see cref="EditButtons" /> and
		/// <see cref="EditModifierKeys" />.
		/// </remarks>
		/// <seealso cref="EditButtons" />
		/// <seealso cref="EditModifierKeys" />
		/// <seealso cref="IsEnableVEdit" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow horizontal editing by alt-left-click-drag" )]
		public bool IsEnableHEdit
		{
			get { return _isEnableHEdit; }
			set { _isEnableHEdit = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not editing of point data is allowed in
		/// the vertical direction.
		/// </summary>
		/// <remarks>
		/// Editing is done by holding down the Alt key, and left-clicking on an individual point of
		/// a given <see cref="CurveItem" /> to drag it to a new location.  The Mouse and Key
		/// combination for this mode are modifiable using <see cref="EditButtons" /> and
		/// <see cref="EditModifierKeys" />.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow vertical editing by alt-left-click-drag" )]
		public bool IsEnableVEdit
		{
			get { return _isEnableVEdit; }
			set { _isEnableVEdit = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not zooming is allowed for the control.
		/// </summary>
		/// <remarks>
		/// Zooming is done by left-clicking inside the <see cref="Chart.Rect"/> to drag
		/// out a rectangle, indicating the new scale ranges that will be part of the graph.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow horizontal and vertical zooming by left-click-drag" )]
		public bool IsEnableZoom
		{
			set { _isEnableHZoom = value; _isEnableVZoom = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not zooming is allowed for the control in
		/// the horizontal direction.
		/// </summary>
		/// <remarks>
		/// Zooming is done by left-clicking inside the <see cref="Chart.Rect"/> to drag
		/// out a rectangle, indicating the new scale ranges that will be part of the graph.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow horizontal zooming by left-click-drag" )]
		public bool IsEnableHZoom
		{
			get { return _isEnableHZoom; }
			set { _isEnableHZoom = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not zooming is allowed for the control in
		/// the vertical direction.
		/// </summary>
		/// <remarks>
		/// Zooming is done by left-clicking inside the <see cref="Chart.Rect"/> to drag
		/// out a rectangle, indicating the new scale ranges that will be part of the graph.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow vertical zooming by left-click-drag" )]
		public bool IsEnableVZoom
		{
			get { return _isEnableVZoom; }
			set { _isEnableVZoom = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether or not panning is allowed for the control in
		/// the horizontal direction.
		/// </summary>
		/// <remarks>
		/// Panning is done by clicking the middle mouse button (or holding down the shift key
		/// while clicking the left mouse button) inside the <see cref="Chart.Rect"/> and
		/// dragging the mouse around to shift the scale ranges as desired.
		/// </remarks>
		/// <seealso cref="IsEnableVPan"/>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow horizontal panning by middle-mouse-drag or shift-left-drag" )]
		public bool IsEnableHPan
		{
			get { return _isEnableHPan; }
			set { _isEnableHPan = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not panning is allowed for the control in
		/// the vertical direction.
		/// </summary>
		/// <remarks>
		/// Panning is done by clicking the middle mouse button (or holding down the shift key
		/// while clicking the left mouse button) inside the <see cref="Chart.Rect"/> and
		/// dragging the mouse around to shift the scale ranges as desired.
		/// </remarks>
		/// <seealso cref="IsEnableHPan"/>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to allow vertical panning by middle-mouse-drag or shift-left-drag" )]
		public bool IsEnableVPan
		{
			get { return _isEnableVPan; }
			set { _isEnableVPan = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not the context menu will be available.
		/// </summary>
		/// <remarks>The context menu is a menu that appears when you right-click on the
		/// <see cref="ZedGraphControl"/>.  It provides options for Zoom, Pan, AutoScale, Clipboard
		/// Copy, and toggle <see cref="IsShowPointValues"/>.
		/// </remarks>
		/// <value>true to allow the context menu, false to disable it</value>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to enable the right mouse button context menu" )]
		public bool IsShowContextMenu
		{
			get { return _isShowContextMenu; }
			set { _isShowContextMenu = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not a message box will be shown
		/// in response to a context menu "Copy" command.
		/// </summary>
		/// <remarks>
		/// Note that, if this property is set to false, the user will receive no
		/// indicative feedback in response to a Copy action.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to show a message box after a 'Copy' context menu action completes" )]
		public bool IsShowCopyMessage
		{
			get { return _isShowCopyMessage; }
			set { _isShowCopyMessage = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not the visible aspect ratio of the
		/// <see cref="MasterPane" /> <see cref="PaneBase.Rect" /> will be preserved
		/// when printing this <see cref="ZedGraphControl" />.
		/// </summary>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to preserve the displayed aspect ratio when printing" )]
		public bool IsPrintKeepAspectRatio
		{
			get { return _isPrintKeepAspectRatio; }
			set { _isPrintKeepAspectRatio = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines whether or not the <see cref="MasterPane" />
		/// <see cref="PaneBase.Rect" /> dimensions will be expanded to fill the
		/// available space when printing this <see cref="ZedGraphControl" />.
		/// </summary>
		/// <remarks>
		/// If <see cref="IsPrintKeepAspectRatio" /> is also true, then the <see cref="MasterPane" />
		/// <see cref="PaneBase.Rect" /> dimensions will be expanded to fit as large
		/// a space as possible while still honoring the visible aspect ratio.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to resize to fill the page when printing" )]
		public bool IsPrintFillPage
		{
			get { return _isPrintFillPage; }
			set { _isPrintFillPage = value; }
		}
		/// <summary>
		/// Gets or sets a value that determines whether the settings of
		/// <see cref="ZedGraph.PaneBase.IsFontsScaled" /> and <see cref="PaneBase.IsPenWidthScaled" />
		/// will be overridden to true during printing operations.
		/// </summary>
		/// <remarks>
		/// Printing involves pixel maps that are typically of a dramatically different dimension
		/// than on-screen pixel maps.  Therefore, it becomes more important to scale the fonts and
		/// lines to give a printed image that looks like what is shown on-screen.  The default
		/// setting for <see cref="ZedGraph.PaneBase.IsFontsScaled" /> is true, but the default
		/// setting for <see cref="PaneBase.IsPenWidthScaled" /> is false.
		/// </remarks>
		/// <value>
		/// A value of true will cause both <see cref="ZedGraph.PaneBase.IsFontsScaled" /> and
		/// <see cref="PaneBase.IsPenWidthScaled" /> to be temporarily set to true during
		/// printing operations.
		/// </value>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to force font and pen width scaling when printing" )]
		bool IsPrintScaleAll
		{
			get { return _isPrintScaleAll; }
			set { _isPrintScaleAll = value; }
		}

		/// <summary>
		/// Gets or sets a value that controls whether or not the axis value range for the scroll
		/// bars will be set automatically.
		/// </summary>
		/// <remarks>
		/// If this value is set to true, then the range of the scroll bars will be set automatically
		/// to the actual range of the data as returned by <see cref="CurveList.GetRange" /> at the
		/// time that <see cref="AxisChange" /> was last called.  Note that a value of true
		/// can override any setting of <see cref="ScrollMinX" />, <see cref="ScrollMaxX" />,
		/// <see cref="ScrollMinY" />, <see cref="ScrollMaxY" />, 
		/// <see cref="ScrollMinY2" />, and <see cref="ScrollMaxY2" />.  Note also that you must
		/// call <see cref="AxisChange" /> from the <see cref="ZedGraphControl" /> for this to
		/// work properly (e.g., don't call it directly from the <see cref="GraphPane" />.
		/// Alternatively, you can call <see cref="SetScrollRangeFromData" /> at anytime to set
		/// the scroll bar range.<br />
		/// <b>In most cases, you will probably want to disable
		/// <see cref="ZedGraph.GraphPane.IsBoundedRanges" /> before activating this option.</b>
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to automatically set the scroll bar range to the actual data range" )]
		public bool IsAutoScrollRange
		{
			get { return _isAutoScrollRange; }
			set { _isAutoScrollRange = value; }
		}

		/// <summary>
		/// Gets or sets a value that determines if the horizontal scroll bar will be visible.
		/// </summary>
		/// <remarks>This scroll bar allows the display to be scrolled in the horizontal direction.
		/// Another option is display panning, in which the user can move the display around by
		/// clicking directly on it and dragging (see <see cref="IsEnableHPan"/> and <see cref="IsEnableVPan"/>).
		/// You can control the available range of scrolling with the <see cref="ScrollMinX"/> and
		/// <see cref="ScrollMaxX"/> properties.  Note that the scroll range can be set automatically by
		/// <see cref="IsAutoScrollRange" />.<br />
		/// <b>In most cases, you will probably want to disable
		/// <see cref="ZedGraph.GraphPane.IsBoundedRanges" /> before activating this option.</b>
		/// </remarks>
		/// <value>A boolean value.  true to display a horizontal scrollbar, false otherwise.</value>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to display the horizontal scroll bar" )]
		public bool IsShowHScrollBar
		{
			get { return _isShowHScrollBar; }
			set { _isShowHScrollBar = value; ZedGraphControl_ReSize( this, new EventArgs() ); }
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
		/// <see cref="Y2Axis"/>.  Note also that the scroll range can be set automatically by
		/// <see cref="IsAutoScrollRange" />.<br />
		/// <b>In most cases, you will probably want to disable
		/// <see cref="ZedGraph.GraphPane.IsBoundedRanges" /> before activating this option.</b>
		/// </remarks>
		/// <value>A boolean value.  true to display a vertical scrollbar, false otherwise.</value>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to display the vertical scroll bar" )]
		public bool IsShowVScrollBar
		{
			get { return _isShowVScrollBar; }
			set { _isShowVScrollBar = value; ZedGraphControl_ReSize( this, new EventArgs() ); }
		}

		/// <summary>
		/// Gets or sets a value that determines if the <see cref="XAxis" /> <see cref="Scale" />
		/// ranges for all <see cref="GraphPane" /> objects in the <see cref="MasterPane" /> will
		/// be forced to match.
		/// </summary>
		/// <remarks>
		/// If set to true (default is false), then all of the <see cref="GraphPane" /> objects
		/// in the <see cref="MasterPane" /> associated with this <see cref="ZedGraphControl" />
		/// will be forced to have matching scale ranges for the x axis.  That is, zoom, pan,
		/// and scroll operations will result in zoom/pan/scroll for all graphpanes simultaneously.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to force the X axis ranges for all GraphPanes to match" )]
		public bool IsSynchronizeXAxes
		{
			get { return _isSynchronizeXAxes; }
			set
			{
				if ( _isSynchronizeXAxes != value )
					ZoomStatePurge();
				_isSynchronizeXAxes = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that determines if the <see cref="YAxis" /> <see cref="Scale" />
		/// ranges for all <see cref="GraphPane" /> objects in the <see cref="MasterPane" /> will
		/// be forced to match.
		/// </summary>
		/// <remarks>
		/// If set to true (default is false), then all of the <see cref="GraphPane" /> objects
		/// in the <see cref="MasterPane" /> associated with this <see cref="ZedGraphControl" />
		/// will be forced to have matching scale ranges for the y axis.  That is, zoom, pan,
		/// and scroll operations will result in zoom/pan/scroll for all graphpanes simultaneously.
		/// </remarks>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to force the Y axis ranges for all GraphPanes to match" )]
		public bool IsSynchronizeYAxes
		{
			get { return _isSynchronizeYAxes; }
			set
			{
				if ( _isSynchronizeYAxes != value )
					ZoomStatePurge();
				_isSynchronizeYAxes = value;
			}
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
		/// to the actual <see cref="Scale.Min"/> and <see cref="Scale.Max" />.  Also note that
		/// this property is actually just an alias to the <see cref="ScrollRange.IsScrollable" />
		/// property of the first element of <see cref="YScrollRangeList" />.
		/// </remarks>
		/// <seealso cref="IsShowVScrollBar"/>
		/// <seealso cref="ScrollMinY2"/>
		/// <seealso cref="ScrollMaxY2"/>
		/// <seealso cref="YScrollRangeList" />
		/// <seealso cref="Y2ScrollRangeList" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to scroll the Y2 axis along with the Y axis" )]
		public bool IsScrollY2
		{
			get
			{
				if ( _y2ScrollRangeList != null && _y2ScrollRangeList.Count > 0 )
					return _y2ScrollRangeList[0].IsScrollable;
				else
					return false;
			}
			set
			{
				if ( _y2ScrollRangeList != null && _y2ScrollRangeList.Count > 0 )
				{
					ScrollRange tmp = _y2ScrollRangeList[0];
					tmp.IsScrollable = value;
					_y2ScrollRangeList[0] = tmp;
				}
			}
		}

		/// <summary>
		/// Access the <see cref="ScrollRangeList" /> for the Y axes.
		/// </summary>
		/// <remarks>
		/// This list maintains the user scale ranges for the scroll bars for each axis
		/// in the <see cref="ZedGraph.GraphPane.YAxisList" />.  Each ordinal location in
		/// <see cref="YScrollRangeList" /> corresponds to an equivalent ordinal location
		/// in <see cref="ZedGraph.GraphPane.YAxisList" />.
		/// </remarks>
		/// <seealso cref="ScrollMinY" />
		/// <seealso cref="ScrollMaxY" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll bar ranges for the collection of Y axes" )]
		public ScrollRangeList YScrollRangeList
		{
			get { return _yScrollRangeList; }
		}

		/// <summary>
		/// Access the <see cref="ScrollRangeList" /> for the Y2 axes.
		/// </summary>
		/// <remarks>
		/// This list maintains the user scale ranges for the scroll bars for each axis
		/// in the <see cref="ZedGraph.GraphPane.Y2AxisList" />.  Each ordinal location in
		/// <see cref="Y2ScrollRangeList" /> corresponds to an equivalent ordinal location
		/// in <see cref="ZedGraph.GraphPane.Y2AxisList" />.
		/// </remarks>
		/// <seealso cref="ScrollMinY2" />
		/// <seealso cref="ScrollMaxY2" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll bar ranges for the collection of Y2 axes" )]
		public ScrollRangeList Y2ScrollRangeList
		{
			get { return _y2ScrollRangeList; }
		}

		/// <summary>
		/// The minimum value for the X axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the minimum endpoint of the scroll range will cause the
		/// <see cref="Scale.Min"/> value to be set to <see cref="ScrollMinX"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableHPan"/>)
		/// is not affected by this value.  Note that this value can be overridden by
		/// <see cref="IsAutoScrollRange" /> and <see cref="SetScrollRangeFromData" />.
		/// </remarks>
		/// <value>A double value indicating the minimum axis value</value>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll minimum value for the X axis" )]
		public double ScrollMinX
		{
			get { return _xScrollRange.Min; }
			set { _xScrollRange.Min = value; }
		}
		/// <summary>
		/// The maximum value for the X axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the maximum endpoint of the scroll range will cause the
		/// <see cref="Scale.Max"/> value to be set to <see cref="ScrollMaxX"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableHPan"/>)
		/// is not affected by this value.  Note that this value can be overridden by
		/// <see cref="IsAutoScrollRange" /> and <see cref="SetScrollRangeFromData" />.
		/// </remarks>
		/// <value>A double value indicating the maximum axis value</value>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll maximum value for the X axis" )]
		public double ScrollMaxX
		{
			get { return _xScrollRange.Max; }
			set { _xScrollRange.Max = value; }
		}
		/// <summary>
		/// The minimum value for the Y axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the minimum endpoint of the scroll range will cause the
		/// <see cref="Scale.Min"/> value to be set to <see cref="ScrollMinY"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.  Note that this value can be overridden by
		/// <see cref="IsAutoScrollRange" /> and <see cref="SetScrollRangeFromData" />.  Also note that
		/// this property is actually just an alias to the <see cref="ScrollRange.Min" />
		/// property of the first element of <see cref="YScrollRangeList" />.
		/// </remarks>
		/// <value>A double value indicating the minimum axis value</value>
		/// <seealso cref="YScrollRangeList" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll minimum value for the Y axis" )]
		public double ScrollMinY
		{
			get
			{
				if ( _yScrollRangeList != null && _yScrollRangeList.Count > 0 )
					return _yScrollRangeList[0].Min;
				else
					return double.NaN;
			}
			set
			{
				if ( _yScrollRangeList != null && _yScrollRangeList.Count > 0 )
				{
					ScrollRange tmp = _yScrollRangeList[0];
					tmp.Min = value;
					_yScrollRangeList[0] = tmp;
				}
			}
		}
		/// <summary>
		/// The maximum value for the Y axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the maximum endpoint of the scroll range will cause the
		/// <see cref="Scale.Max"/> value to be set to <see cref="ScrollMaxY"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.  Note that this value can be overridden by
		/// <see cref="IsAutoScrollRange" /> and <see cref="SetScrollRangeFromData" />.  Also note that
		/// this property is actually just an alias to the <see cref="ScrollRange.Max" />
		/// property of the first element of <see cref="YScrollRangeList" />.
		/// </remarks>
		/// <value>A double value indicating the maximum axis value</value>
		/// <seealso cref="YScrollRangeList" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll maximum value for the Y axis" )]
		public double ScrollMaxY
		{
			get
			{
				if ( _yScrollRangeList != null && _yScrollRangeList.Count > 0 )
					return _yScrollRangeList[0].Max;
				else
					return double.NaN;
			}
			set
			{
				if ( _yScrollRangeList != null && _yScrollRangeList.Count > 0 )
				{
					ScrollRange tmp = _yScrollRangeList[0];
					tmp.Max = value;
					_yScrollRangeList[0] = tmp;
				}
			}
		}
		/// <summary>
		/// The minimum value for the Y2 axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the minimum endpoint of the scroll range will cause the
		/// <see cref="Scale.Min"/> value to be set to <see cref="ScrollMinY2"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.  Note that this value can be overridden by
		/// <see cref="IsAutoScrollRange" /> and <see cref="SetScrollRangeFromData" />.  Also note that
		/// this property is actually just an alias to the <see cref="ScrollRange.Min" />
		/// property of the first element of <see cref="Y2ScrollRangeList" />.
		/// </remarks>
		/// <value>A double value indicating the minimum axis value</value>
		/// <seealso cref="Y2ScrollRangeList" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll minimum value for the Y2 axis" )]
		public double ScrollMinY2
		{
			get
			{
				if ( _y2ScrollRangeList != null && _y2ScrollRangeList.Count > 0 )
					return _y2ScrollRangeList[0].Min;
				else
					return double.NaN;
			}
			set
			{
				if ( _y2ScrollRangeList != null && _y2ScrollRangeList.Count > 0 )
				{
					ScrollRange tmp = _y2ScrollRangeList[0];
					tmp.Min = value;
					_y2ScrollRangeList[0] = tmp;
				}
			}
		}
		/// <summary>
		/// The maximum value for the Y2 axis scroll range.
		/// </summary>
		/// <remarks>
		/// Effectively, the maximum endpoint of the scroll range will cause the
		/// <see cref="Scale.Max"/> value to be set to <see cref="ScrollMaxY2"/>.  Note that this
		/// value applies only to the scroll bar settings.  Axis panning (see <see cref="IsEnableVPan"/>)
		/// is not affected by this value.  Note that this value can be overridden by
		/// <see cref="IsAutoScrollRange" /> and <see cref="SetScrollRangeFromData" />.  Also note that
		/// this property is actually just an alias to the <see cref="ScrollRange.Max" />
		/// property of the first element of <see cref="Y2ScrollRangeList" />.
		/// </remarks>
		/// <value>A double value indicating the maximum axis value</value>
		/// <seealso cref="Y2ScrollRangeList" />
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the manual scroll maximum value for the Y2 axis" )]
		public double ScrollMaxY2
		{
			get
			{
				if ( _y2ScrollRangeList != null && _y2ScrollRangeList.Count > 0 )
					return _y2ScrollRangeList[0].Max;
				else
					return double.NaN;
			}
			set
			{
				if ( _y2ScrollRangeList != null && _y2ScrollRangeList.Count > 0 )
				{
					ScrollRange tmp = _y2ScrollRangeList[0];
					tmp.Max = value;
					_y2ScrollRangeList[0] = tmp;
				}
			}
		}


		/// <summary>
		/// Gets or sets the format for displaying tooltip values.
		/// This format is passed to <see cref="PointPair.ToString(string)"/>.
		/// </summary>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the numeric display format string for the point value tooltips" )]
		public string PointValueFormat
		{
			get { return _pointValueFormat; }
			set { _pointValueFormat = value; }
		}

		/// <summary>
		/// Gets or sets the format for displaying tooltip values.
		/// This format is passed to <see cref="XDate.ToString(string)"/>.
		/// </summary>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the date display format for the point value tooltips" )]
		public string PointDateFormat
		{
			get { return _pointDateFormat; }
			set { _pointDateFormat = value; }
		}

		/// <summary>
		/// Gets or sets the step size fraction for zooming with the mouse wheel.
		/// A value of 0.1 will result in a 10% zoom step for each mouse wheel movement.
		/// </summary>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "Sets the step size fraction for zooming with the mouse wheel" )]
		public double ZoomStepFraction
		{
			get { return _zoomStepFraction; }
			set { _zoomStepFraction = value; }
		}

		/// <summary>
		/// Gets or sets a boolean value that determines if zooming with the wheel mouse
		/// is centered on the mouse location, or centered on the existing graph.
		/// </summary>
		[Bindable( true ), Category( "Display" ), NotifyParentProperty( true )]
		[Description( "true to center the mouse wheel zoom at the current mouse location" )]
		public bool IsZoomOnMouseCenter
		{
			get { return _isZoomOnMouseCenter; }
			set { _isZoomOnMouseCenter = value; }
		}

		/// <summary>
		/// Gets the graph pane's current image.
		/// <seealso cref="Bitmap"/>
		/// </summary>
		/// <exception cref="ZedGraphException">
		/// When the control has been disposed before this call.
		/// </exception>
		[Bindable( false ), Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Image GetImage()
		{
			lock ( this )
			{
				if ( BeenDisposed || _masterPane == null || _masterPane[0] == null )
					throw new ZedGraphException( "The control has been disposed" );

				return _masterPane.GetImage();
			}
		}

		/// <summary>
		/// This checks if the control has been disposed.  This is synonymous with
		/// the graph pane having been nulled or disposed.  Therefore this is the
		/// same as <c>ZedGraphControl.GraphPane == null</c>.
		/// </summary>
		[Bindable( false ), Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public bool BeenDisposed
		{
			get
			{
				lock ( this ) return _masterPane == null;
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
			lock ( this )
			{
				if ( BeenDisposed || _masterPane == null || this.GraphPane == null )
					return;

				if ( hScrollBar1 != null && this.GraphPane != null &&
					vScrollBar1 != null && _yScrollRangeList != null )
				{
					SetScroll( hScrollBar1, this.GraphPane.XAxis, _xScrollRange.Min, _xScrollRange.Max );
					SetScroll( vScrollBar1, this.GraphPane.YAxis, _yScrollRangeList[0].Min,
						_yScrollRangeList[0].Max );
				}

				base.OnPaint( e );

				// Add a try/catch pair since the users of the control can't catch this one
				try { _masterPane.Draw( e.Graphics ); }
				catch { }
			}
		}

		/// <summary>
		/// Called when the control has been resized.
		/// </summary>
		/// <param name="sender">
		/// A reference to the control that has been resized.
		/// </param>
		/// <param name="e">
		/// An EventArgs object.
		/// </param>
		protected void ZedGraphControl_ReSize( object sender, System.EventArgs e )
		{
			lock ( this )
			{
				if ( BeenDisposed || _masterPane == null )
					return;

				Size newSize = this.Size;

				if ( _isShowHScrollBar )
				{
					hScrollBar1.Visible = true;
					newSize.Height -= this.hScrollBar1.Size.Height;
					hScrollBar1.Location = new Point( 0, newSize.Height );
					hScrollBar1.Size = new Size( newSize.Width, hScrollBar1.Height );
				}
				else
					hScrollBar1.Visible = false;

				if ( _isShowVScrollBar )
				{
					vScrollBar1.Visible = true;
					newSize.Width -= this.vScrollBar1.Size.Width;
					vScrollBar1.Location = new Point( newSize.Width, 0 );
					vScrollBar1.Size = new Size( vScrollBar1.Width, newSize.Height );
				}
				else
					vScrollBar1.Visible = false;

				Graphics g = this.CreateGraphics();
				_masterPane.ReSize( g, new RectangleF( 0, 0, newSize.Width, newSize.Height ) );
				g.Dispose();
				this.Invalidate();
			}
		}
		/// <summary>This performs an axis change command on the graphPane.
		/// </summary>
		/// <remarks>
		/// This is the same as
		/// <c>ZedGraphControl.GraphPane.AxisChange( ZedGraphControl.CreateGraphics() )</c>, however,
		/// this method also calls <see cref="SetScrollRangeFromData" /> if <see cref="IsAutoScrollRange" />
		/// is true.
		/// </remarks>
		public virtual void AxisChange()
		{
			lock ( this )
			{
				if ( BeenDisposed || _masterPane == null )
					return;

				Graphics g = this.CreateGraphics();

				_masterPane.AxisChange( g );

				g.Dispose();

				if ( _isAutoScrollRange )
					SetScrollRangeFromData();
			}
		}
	#endregion

	#region Mouse Events

		/// <summary>
		/// Handle a MouseDown event in the <see cref="ZedGraphControl" />
		/// </summary>
		/// <param name="sender">A reference to the <see cref="ZedGraphControl" /></param>
		/// <param name="e">A <see cref="MouseEventArgs" /> instance</param>
		protected void ZedGraphControl_MouseDown( object sender, MouseEventArgs e )
		{
			_isPanning = false;
			_isZooming = false;
			_isEditing = false;
			_dragPane = null;

			Point mousePt = new Point( e.X, e.Y );

			// Provide Callback for MouseDown events
			if ( _masterPane != null && this.MouseDownEvent != null )
			{
				if ( this.MouseDownEvent( this, e ) )
					return;
			}

			if ( e.Clicks > 1 || _masterPane == null )
				return;

			// First, see if the click is within a Linkable object within any GraphPane
			GraphPane pane = this.MasterPane.FindPane( mousePt );
			if (	pane != null && this.LinkEvent != null &&
					e.Button == _linkButtons && Control.ModifierKeys == _linkModifierKeys )
			{
				object source;
				Link link;
				int index;
				Graphics g = this.CreateGraphics();
				float scaleFactor = pane.CalcScaleFactor();
				if ( pane.FindLinkableObject( mousePt, g, scaleFactor, out source, out link, out index ) )
				{
					if ( LinkEvent( this, pane, source, link, index ) )
						return;

					if ( link._url != string.Empty )
					{
						System.Diagnostics.Process.Start( link._url );
						// linkable objects override any other actions with mouse
						return;
					}
				}
				g.Dispose();
			}

			// Second, Check to see if it's within a Chart Rect
			pane = this.MasterPane.FindChartRect( mousePt );
			//Rectangle rect = new Rectangle( mousePt, new Size( 1, 1 ) );

			if ( pane != null &&
				( _isEnableHPan || _isEnableVPan ) &&
				( ( e.Button == _panButtons && Control.ModifierKeys == _panModifierKeys ) ||
				( e.Button == _panButtons2 && Control.ModifierKeys == _panModifierKeys2 ) ) )
			{
				_isPanning = true;
				_dragStartPt = mousePt;
				_dragPane = pane;
				//_zoomState = new ZoomState( _dragPane, ZoomState.StateType.Pan );
				ZoomStateSave( _dragPane, ZoomState.StateType.Pan );
			}
			else if ( pane != null && ( _isEnableHZoom || _isEnableVZoom ) &&
				( ( e.Button == _zoomButtons && Control.ModifierKeys == _zoomModifierKeys ) ||
				( e.Button == _zoomButtons2 && Control.ModifierKeys == _zoomModifierKeys2 ) ) )
			{
				_isZooming = true;
				_dragStartPt = mousePt;
				_dragEndPt = mousePt;
				_dragEndPt.Offset( 1, 1 );
				_dragPane = pane;
				ZoomStateSave( _dragPane, ZoomState.StateType.Zoom );
			}
			else if ( pane != null && ( _isEnableHEdit || _isEnableVEdit ) &&
				 ( e.Button == EditButtons && Control.ModifierKeys == EditModifierKeys ) )
			{

				// find the point that was clicked, and make sure the point list is editable
				// and that it's a primary Y axis (the first Y or Y2 axis)
				if ( pane.FindNearestPoint( mousePt, out _dragCurve, out _dragIndex ) &&
							_dragCurve.Points is IPointListEdit )
				{
					_isEditing = true;
					_dragPane = pane;
					_dragStartPt = mousePt;
					_dragStartPair = _dragCurve[_dragIndex];
				}
			}
		}

		/// <summary>
		/// Set the cursor according to the current mouse location.
		/// </summary>
		protected void SetCursor()
		{
			SetCursor( this.PointToClient( Control.MousePosition ) );
		}

		/// <summary>
		/// Set the cursor according to the current mouse location.
		/// </summary>
		protected void SetCursor( Point mousePt )
		{
			if ( _masterPane != null )
			{
				GraphPane pane = _masterPane.FindChartRect( mousePt );
				if ( ( _isEnableHPan || _isEnableVPan ) && ( Control.ModifierKeys == Keys.Shift || _isPanning ) &&
					( pane != null || _isPanning ) )
					Cursor.Current = Cursors.Hand;
				else if ( ( _isEnableVZoom || _isEnableHZoom ) && ( pane != null || _isZooming ) )
					Cursor.Current = Cursors.Cross;

				//			else if ( isZoomMode || isPanMode )
				//				Cursor.Current = Cursors.No;
			}
		}

		/// <summary>
		/// Handle a KeyUp event
		/// </summary>
		/// <param name="sender">The <see cref="ZedGraphControl" /> in which the KeyUp occurred.</param>
		/// <param name="e">A <see cref="KeyEventArgs" /> instance.</param>
		protected void ZedGraphControl_KeyUp( object sender, KeyEventArgs e )
		{
			SetCursor();
		}

		/// <summary>
		/// Handle the Key Events so ZedGraph can Escape out of a panning or zooming operation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ZedGraphControl_KeyDown( object sender, System.Windows.Forms.KeyEventArgs e )
		{
			SetCursor();

			if ( e.KeyCode == Keys.Escape )
			{
				if ( _isPanning )
					HandlePanCancel();
				if ( _isZooming )
					HandleZoomCancel();
				if ( _isEditing )
					HandleEditCancel();

				_isZooming = false;
				_isPanning = false;
				_isEditing = false;

				Refresh();
			}
		}

		/// <summary>
		/// Handle a MouseUp event in the <see cref="ZedGraphControl" />
		/// </summary>
		/// <param name="sender">A reference to the <see cref="ZedGraphControl" /></param>
		/// <param name="e">A <see cref="MouseEventArgs" /> instance</param>
		protected void ZedGraphControl_MouseUp( object sender, MouseEventArgs e )
		{
			// Provide Callback for MouseUp events
			if ( _masterPane != null && this.MouseUpEvent != null )
			{
				if ( this.MouseUpEvent( this, e ) )
					return;
			}

			if ( _masterPane != null && _dragPane != null )
			{
				// If the MouseUp event occurs, the user is done dragging.
				if ( _isZooming )
					HandleZoomFinish( sender, e );
				else if ( _isPanning )
					HandlePanFinish();
				else if ( _isEditing )
					HandleEditFinish();
			}

			// Reset the rectangle.
			//dragStartPt = new Rectangle( 0, 0, 0, 0 );
			_dragPane = null;
			_isZooming = false;
			_isPanning = false;
			_isEditing = false;
			Cursor.Current = Cursors.Default;

		}

		/// <summary>
		/// Make a string label that corresponds to a user scale value.
		/// </summary>
		/// <param name="axis">The axis from which to obtain the scale value.  This determines
		/// if it's a date value, linear, log, etc.</param>
		/// <param name="val">The value to be made into a label</param>
		/// <param name="iPt">The ordinal position of the value</param>
		/// <param name="isOverrideOrdinal">true to override the ordinal settings of the axis,
		/// and prefer the actual value instead.</param>
		/// <returns>The string label.</returns>
		protected string MakeValueLabel( Axis axis, double val, int iPt, bool isOverrideOrdinal )
		{
			if ( axis != null )
			{
				if ( axis.Scale.IsDate )
				{
					return XDate.ToString( val, _pointDateFormat );
				}
				else if ( axis._scale.IsText && axis._scale._textLabels != null )
				{
					int i = iPt;
					if ( isOverrideOrdinal )
						i = (int)( val - 0.5 );

					if ( i >= 0 && i < axis._scale._textLabels.Length )
						return axis._scale._textLabels[i];
					else
						return ( i + 1 ).ToString();
				}
				else if ( axis.Scale.IsAnyOrdinal && !isOverrideOrdinal )
				{
					return iPt.ToString( _pointValueFormat );
				}
				else
					return val.ToString( _pointValueFormat );
			}
			else
				return "";
		}

		/// <summary>
		/// protected method for handling MouseMove events to display tooltips over
		/// individual datapoints.
		/// </summary>
		/// <param name="sender">
		/// A reference to the control that has the MouseMove event.
		/// </param>
		/// <param name="e">
		/// A MouseEventArgs object.
		/// </param>
		protected void ZedGraphControl_MouseMove( object sender, MouseEventArgs e )
		{
			if ( _masterPane != null )
			{
				// Provide Callback for MouseMove events
				if ( this.MouseMoveEvent != null && this.MouseMoveEvent( this, e ) )
					return;

				Point mousePt = new Point( e.X, e.Y );

				//Point tempPt = this.PointToClient( Control.MousePosition );

				SetCursor( mousePt );

				// If the mouse is being dragged,
				// undraw and redraw the rectangle as the mouse moves.
				if ( _isZooming )
					HandleZoomDrag( mousePt );
				else if ( _isPanning )
					HandlePanDrag( mousePt );
				else if ( _isEditing )
					HandleEditDrag( mousePt );
				else if ( _isShowCursorValues )
					HandleCursorValues( mousePt );
				else if ( _isShowPointValues )
					HandlePointValues( mousePt );
			}
		}

		private Point HandlePointValues( Point mousePt )
		{
			int iPt;
			GraphPane pane;
			object nearestObj;

			Graphics g = this.CreateGraphics();

			if ( _masterPane.FindNearestPaneObject( mousePt,
				g, out pane, out nearestObj, out iPt ) )
			{
				if ( nearestObj is CurveItem && iPt >= 0 )
				{
					CurveItem curve = (CurveItem)nearestObj;
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
								( (PieItem)curve ).Value.ToString( _pointValueFormat ) );
						}
						else
						{
							PointPair pt = curve.Points[iPt];

							if ( pt.Tag is string )
								this.pointToolTip.SetToolTip( this, (string)pt.Tag );
							else
							{
								double xVal, yVal, lowVal;
								ValueHandler valueHandler = new ValueHandler( pane, false );
								if ( ( curve is BarItem || curve is ErrorBarItem || curve is HiLowBarItem )
										&& pane.BarSettings.Base != BarBase.X )
									valueHandler.GetValues( curve, iPt, out yVal, out lowVal, out xVal );
								else
									valueHandler.GetValues( curve, iPt, out xVal, out lowVal, out yVal );

								string xStr = MakeValueLabel( pane.XAxis, xVal, iPt,
									curve.IsOverrideOrdinal );
								string yStr = MakeValueLabel( curve.GetYAxis( pane ), yVal, iPt,
									curve.IsOverrideOrdinal );

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
			return mousePt;
		}

		private Point HandleCursorValues( Point mousePt )
		{
			GraphPane pane = _masterPane.FindPane( mousePt );
			if ( pane != null && pane.Chart._rect.Contains( mousePt ) )
			{
				double x, y, y2;
				pane.ReverseTransform( mousePt, out x, out y, out y2 );
				string xStr = MakeValueLabel( pane.XAxis, x, -1, true );
				string yStr = MakeValueLabel( pane.YAxis, y, -1, true );
				string y2Str = MakeValueLabel( pane.Y2Axis, y2, -1, true );

				this.pointToolTip.SetToolTip( this, "( " + xStr + ", " + yStr + ", " + y2Str + " )" );
				this.pointToolTip.Active = true;
			}
			else
				this.pointToolTip.Active = false;
			return mousePt;
		}


	#endregion

	#region Mouse Wheel Zoom Events

		/// <summary>
		/// Handle a MouseWheel event in the <see cref="ZedGraphControl" />
		/// </summary>
		/// <param name="sender">A reference to the <see cref="ZedGraphControl" /></param>
		/// <param name="e">A <see cref="MouseEventArgs" /> instance</param>
		protected void ZedGraphControl_MouseWheel( object sender, MouseEventArgs e )
		{
			if ( ( _isEnableVZoom || _isEnableHZoom ) && _masterPane != null )
			{
				GraphPane pane = this.MasterPane.FindChartRect( new PointF( e.X, e.Y ) );
				if ( pane != null && e.Delta != 0 )
				{
					ZoomState oldState = ZoomStateSave( pane, ZoomState.StateType.WheelZoom );
					//ZoomState oldState = pane.ZoomStack.Push( pane, ZoomState.StateType.Zoom );

					PointF centerPoint = new PointF( e.X, e.Y );
					double zoomFraction = ( 1 + ( e.Delta < 0 ? 1.0 : -1.0 ) * ZoomStepFraction );

					ZoomPane( pane, zoomFraction, centerPoint, _isZoomOnMouseCenter, false );

					ApplyToAllPanes( pane );

					ZoomStatePush( pane );

					// Provide Callback to notify the user of zoom events
					if ( this.ZoomEvent != null )
						this.ZoomEvent( this, oldState, new ZoomState( pane, ZoomState.StateType.WheelZoom ) );

					this.Refresh();

				}
			}
		}

		/// <summary>
		/// Zoom a specified pane in or out according to the specified zoom fraction.
		/// </summary>
		/// <remarks>
		/// The zoom will occur on the <see cref="XAxis" />, <see cref="YAxis" />, and
		/// <see cref="Y2Axis" /> only if the corresponding flag, <see cref="IsEnableHZoom" /> or
		/// <see cref="IsEnableVZoom" />, is true.  Note that if there are multiple Y or Y2 axes, all of
		/// them will be zoomed.
		/// </remarks>
		/// <param name="pane">The <see cref="GraphPane" /> instance to be zoomed.</param>
		/// <param name="zoomFraction">The fraction by which to zoom, less than 1 to zoom in, greater than
		/// 1 to zoom out.  For example, 0.9 will zoom in such that the scale is 90% of what it was
		/// originally.</param>
		/// <param name="centerPt">The screen position about which the zoom will be centered.  This
		/// value is only used if <see paramref="isZoomOnCenter" /> is true.
		/// </param>
		/// <param name="isZoomOnCenter">true to cause the zoom to be centered on the point
		/// <see paramref="centerPt" />, false to center on the <see cref="Chart.Rect" />.
		/// </param>
		/// <param name="isRefresh">true to force a refresh of the control, false to leave it unrefreshed</param>
		protected void ZoomPane( GraphPane pane, double zoomFraction, PointF centerPt,
					bool isZoomOnCenter, bool isRefresh )
		{
			double x;
			double[] y;
			double[] y2;

			pane.ReverseTransform( centerPt, out x, out y, out y2 );

			if ( _isEnableHZoom )
				ZoomScale( pane.XAxis, zoomFraction, x, isZoomOnCenter );
			if ( _isEnableVZoom )
			{
				for ( int i = 0; i < pane.YAxisList.Count; i++ )
					ZoomScale( pane.YAxisList[i], zoomFraction, y[i], isZoomOnCenter );
				for ( int i = 0; i < pane.Y2AxisList.Count; i++ )
					ZoomScale( pane.Y2AxisList[i], zoomFraction, y2[i], isZoomOnCenter );
			}

			Graphics g = this.CreateGraphics();
			pane.AxisChange( g );
			g.Dispose();


			this.SetScroll( this.hScrollBar1, pane.XAxis, _xScrollRange.Min, _xScrollRange.Max );
			this.SetScroll( this.vScrollBar1, pane.YAxis, _yScrollRangeList[0].Min,
				_yScrollRangeList[0].Max );

			if ( isRefresh )
				Refresh();
		}

		/// <summary>
		/// Zoom a specified pane in or out according to the specified zoom fraction.
		/// </summary>
		/// <remarks>
		/// The zoom will occur on the <see cref="XAxis" />, <see cref="YAxis" />, and
		/// <see cref="Y2Axis" /> only if the corresponding flag, <see cref="IsEnableHZoom" /> or
		/// <see cref="IsEnableVZoom" />, is true.  Note that if there are multiple Y or Y2 axes, all of
		/// them will be zoomed.
		/// </remarks>
		/// <param name="pane">The <see cref="GraphPane" /> instance to be zoomed.</param>
		/// <param name="zoomFraction">The fraction by which to zoom, less than 1 to zoom in, greater than
		/// 1 to zoom out.  For example, 0.9 will zoom in such that the scale is 90% of what it was
		/// originally.</param>
		/// <param name="centerPt">The screen position about which the zoom will be centered.  This
		/// value is only used if <see paramref="isZoomOnCenter" /> is true.
		/// </param>
		/// <param name="isZoomOnCenter">true to cause the zoom to be centered on the point
		/// <see paramref="centerPt" />, false to center on the <see cref="Chart.Rect" />.
		/// </param>
		public void ZoomPane( GraphPane pane, double zoomFraction, PointF centerPt, bool isZoomOnCenter )
		{
			ZoomPane( pane, zoomFraction, centerPt, isZoomOnCenter, true );
		}


		/// <summary>
		/// Zoom the specified axis by the specified amount, with the center of the zoom at the
		/// (optionally) specified point.
		/// </summary>
		/// <remarks>
		/// This method is used for MouseWheel zoom operations</remarks>
		/// <param name="axis">The <see cref="Axis" /> to be zoomed.</param>
		/// <param name="zoomFraction">The zoom fraction, less than 1.0 to zoom in, greater than 1.0 to
		/// zoom out.  That is, a value of 0.9 will zoom in such that the scale length is 90% of what
		/// it previously was.</param>
		/// <param name="centerVal">The location for the center of the zoom.  This is only used if
		/// <see paramref="IsZoomOnMouseCenter" /> is true.</param>
		/// <param name="isZoomOnCenter">true if the zoom is to be centered at the
		/// <see paramref="centerVal" /> screen position, false for the zoom to be centered within
		/// the <see cref="Chart.Rect" />.
		/// </param>
		protected void ZoomScale( Axis axis, double zoomFraction, double centerVal, bool isZoomOnCenter )
		{
			if ( axis != null && zoomFraction > 0.0001 && zoomFraction < 1000.0 )
			{
				Scale scale = axis._scale;
/*
				if ( axis.Scale.IsLog )
				{
					double ratio = Math.Sqrt( axis._scale._max / axis._scale._min * zoomFraction );

					if ( !isZoomOnCenter )
						centerVal = Math.Sqrt( axis._scale._max * axis._scale._min );

					axis._scale._min = centerVal / ratio;
					axis._scale._max = centerVal * ratio;
				}
				else
				{
*/
					double minLin = axis._scale._minLinearized;
					double maxLin = axis._scale._maxLinearized;
					double range = ( maxLin - minLin ) * zoomFraction / 2.0;

					if ( !isZoomOnCenter )
						centerVal = ( maxLin + minLin ) / 2.0;

					axis._scale._minLinearized = centerVal - range;
					axis._scale._maxLinearized = centerVal + range;
//				}

				axis._scale._minAuto = false;
				axis._scale._maxAuto = false;
			}
		}

	#endregion

	#region Pan Events

		private Point HandlePanDrag( Point mousePt )
		{
			double x1, x2;
			double[] y1, y2, yy1, yy2;
			//PointF endPoint = mousePt;
			//PointF startPoint = ( (Control)sender ).PointToClient( this.dragRect.Location );

			_dragPane.ReverseTransform( _dragStartPt, out x1, out y1, out yy1 );
			_dragPane.ReverseTransform( mousePt, out x2, out y2, out yy2 );

			if ( _isEnableHPan )
			{
				PanScale( _dragPane.XAxis, x1, x2 );
				this.SetScroll( this.hScrollBar1, _dragPane.XAxis, _xScrollRange.Min, _xScrollRange.Max );
			}
			if ( _isEnableVPan )
			{
				for ( int i = 0; i < y1.Length; i++ )
					PanScale( _dragPane.YAxisList[i], y1[i], y2[i] );
				for ( int i = 0; i < yy1.Length; i++ )
					PanScale( _dragPane.Y2AxisList[i], yy1[i], yy2[i] );
				this.SetScroll( this.vScrollBar1, _dragPane.YAxis, _yScrollRangeList[0].Min,
					_yScrollRangeList[0].Max );
			}

			ApplyToAllPanes( _dragPane );

			Refresh();

			_dragStartPt = mousePt;

			return mousePt;
		}

		private void HandlePanFinish()
		{
			// push the prior saved zoomstate, since the scale ranges have already been changed on
			// the fly during the panning operation
			if ( _zoomState != null && _zoomState.IsChanged( _dragPane ) )
			{
				//_dragPane.ZoomStack.Push( _zoomState );
				ZoomStatePush( _dragPane );

				// Provide Callback to notify the user of pan events
				if ( this.ZoomEvent != null )
					this.ZoomEvent( this, _zoomState,
						new ZoomState( _dragPane, ZoomState.StateType.Pan ) );

				_zoomState = null;
			}
		}

		private void HandlePanCancel()
		{
			if ( _isPanning )
			{
				if ( _zoomState != null && _zoomState.IsChanged( _dragPane ) )
				{
					ZoomStateRestore( _dragPane );
					//_zoomState.ApplyState( _dragPane );
					//_zoomState = null;
				}
				_isPanning = false;
				Refresh();

				ZoomStateClear();
			}
		}

		/// <summary>
		/// Handle a panning operation for the specified <see cref="Axis" />.
		/// </summary>
		/// <param name="axis">The <see cref="Axis" /> to be panned</param>
		/// <param name="startVal">The value where the pan started.  The scale range
		/// will be shifted by the difference between <see paramref="startVal" /> and
		/// <see paramref="endVal" />.
		/// </param>
		/// <param name="endVal">The value where the pan ended.  The scale range
		/// will be shifted by the difference between <see paramref="startVal" /> and
		/// <see paramref="endVal" />.
		/// </param>
		protected void PanScale( Axis axis, double startVal, double endVal )
		{
			if ( axis != null )
			{
				Scale scale = axis._scale;
				double delta = scale.Linearize( startVal ) - scale.Linearize( endVal );

				scale._minLinearized += delta;
				scale._maxLinearized += delta;

				scale._minAuto = false;
				scale._maxAuto = false;

/*
				if ( axis.Type == AxisType.Log )
				{
					axis._scale._min *= startVal / endVal;
					axis._scale._max *= startVal / endVal;
				}
				else
				{
					axis._scale._min += startVal - endVal;
					axis._scale._max += startVal - endVal;
				}
*/
			}
		}

	#endregion

	#region Edit Point Events

		private void HandleEditDrag( Point mousePt )
		{
			// get the scale values that correspond to the current point
			double curX, curY;
			_dragPane.ReverseTransform( mousePt, _dragCurve.IsY2Axis, _dragCurve.YAxisIndex,
					out curX, out curY );
			double startX, startY;
			_dragPane.ReverseTransform( _dragStartPt, _dragCurve.IsY2Axis, _dragCurve.YAxisIndex,
					out startX, out startY );

			// calculate the new scale values for the point
			PointPair newPt = new PointPair( _dragStartPair );

			Scale xScale = _dragPane.XAxis._scale;
			if ( _isEnableHEdit )
				newPt.X = xScale.DeLinearize( xScale.Linearize( newPt.X ) +
							xScale.Linearize( curX )- xScale.Linearize( startX ) );

			Scale yScale = _dragCurve.GetYAxis( _dragPane )._scale;
			if ( _isEnableVEdit )
				newPt.Y = yScale.DeLinearize( yScale.Linearize( newPt.Y ) +
							yScale.Linearize( curY ) - yScale.Linearize( startY ) );

			// save the data back to the point list
			IPointListEdit list = _dragCurve.Points as IPointListEdit;
			if ( list != null )
				list[_dragIndex] = newPt;

			// force a redraw
			Refresh();
		}

		private void HandleEditFinish()
		{
			//Nothing to do here -- May want to add an undo option
		}

		private void HandleEditCancel()
		{
			if ( _isEditing )
			{
				IPointListEdit list = _dragCurve.Points as IPointListEdit;
				if ( list != null )
					list[_dragIndex] = _dragStartPair;
				_isEditing = false;
				Refresh();
			}
		}

	#endregion

	#region Zoom Events

		private void HandleZoomDrag( Point mousePt )
		{
			// Hide the previous rectangle by calling the
			// DrawReversibleFrame method with the same parameters.
			Rectangle rect = CalcScreenRect( _dragStartPt, _dragEndPt );
			ControlPaint.DrawReversibleFrame( rect, this.BackColor, FrameStyle.Dashed );

			// Bound the zoom to the ChartRect
			_dragEndPt = Point.Round( BoundPointToRect( mousePt, _dragPane.Chart._rect ) );
			rect = CalcScreenRect( _dragStartPt, _dragEndPt );
			// Draw the new rectangle by calling DrawReversibleFrame again.
			ControlPaint.DrawReversibleFrame( rect, this.BackColor, FrameStyle.Dashed );
		}

		private void HandleZoomFinish( object sender, MouseEventArgs e )
		{
			PointF mousePtF = BoundPointToRect( new Point( e.X, e.Y ), _dragPane.Chart._rect );

			// Only accept a drag if it covers at least 5 pixels in each direction
			//Point curPt = ( (Control)sender ).PointToScreen( Point.Round( mousePt ) );
			if ( ( Math.Abs( mousePtF.X - _dragStartPt.X ) > 4 || !_isEnableHZoom ) &&
					( Math.Abs( mousePtF.Y - _dragStartPt.Y ) > 4 || !_isEnableVZoom ) )
			{
				// Draw the rectangle to be evaluated. Set a dashed frame style
				// using the FrameStyle enumeration.
				//ControlPaint.DrawReversibleFrame( this.dragRect,
				//	this.BackColor, FrameStyle.Dashed );

				double x1, x2;
				double[] y1, y2, yy1, yy2;
				//PointF startPoint = ( (Control)sender ).PointToClient( this.dragRect.Location );

				_dragPane.ReverseTransform( _dragStartPt, out x1, out y1, out yy1 );
				_dragPane.ReverseTransform( mousePtF, out x2, out y2, out yy2 );

				ZoomStatePush( _dragPane );
				//ZoomState oldState = _dragPane.ZoomStack.Push( _dragPane,
				//			ZoomState.StateType.Zoom );

				if ( _isEnableHZoom )
				{
					_dragPane.XAxis._scale._min = Math.Min( x1, x2 );
					_dragPane.XAxis._scale._minAuto = false;

					_dragPane.XAxis._scale._max = Math.Max( x1, x2 );
					_dragPane.XAxis._scale._maxAuto = false;
				}
				if ( _isEnableVZoom )
				{
					for ( int i = 0; i < y1.Length; i++ )
					{
						_dragPane.YAxisList[i]._scale._min = Math.Min( y1[i], y2[i] );
						_dragPane.YAxisList[i]._scale._max = Math.Max( y1[i], y2[i] );
						_dragPane.YAxisList[i]._scale._minAuto = false;
						_dragPane.YAxisList[i]._scale._maxAuto = false;
					}
					for ( int i = 0; i < yy1.Length; i++ )
					{
						_dragPane.Y2AxisList[i]._scale._min = Math.Min( yy1[i], yy2[i] );
						_dragPane.Y2AxisList[i]._scale._max = Math.Max( yy1[i], yy2[i] );
						_dragPane.Y2AxisList[i]._scale._minAuto = false;
						_dragPane.Y2AxisList[i]._scale._maxAuto = false;
					}
				}

				this.SetScroll( this.hScrollBar1, _dragPane.XAxis, _xScrollRange.Min, _xScrollRange.Max );
				this.SetScroll( this.vScrollBar1, _dragPane.YAxis, _yScrollRangeList[0].Min,
					_yScrollRangeList[0].Max );

				ApplyToAllPanes( _dragPane );

				// Provide Callback to notify the user of zoom events
				if ( this.ZoomEvent != null )
					this.ZoomEvent( this, _zoomState, //oldState,
						new ZoomState( _dragPane, ZoomState.StateType.Zoom ) );

				Graphics g = this.CreateGraphics();
				_dragPane.AxisChange( g );
				g.Dispose();
			}

			Refresh();
		}

		private void HandleZoomCancel()
		{
			if ( _isZooming )
			{
				_isZooming = false;
				Refresh();

				ZoomStateClear();
			}
		}

		private PointF BoundPointToRect( Point mousePt, RectangleF rect )
		{
			PointF newPt = new PointF( mousePt.X, mousePt.Y );

			if ( mousePt.X < rect.X ) newPt.X = rect.X;
			if ( mousePt.X > rect.Right ) newPt.X = rect.Right;
			if ( mousePt.Y < rect.Y ) newPt.Y = rect.Y;
			if ( mousePt.Y > rect.Bottom ) newPt.Y = rect.Bottom;

			return newPt;
		}

		private Rectangle CalcScreenRect( Point mousePt1, Point mousePt2 )
		{
			Point screenPt = PointToScreen( mousePt1 );
			Size size = new Size( mousePt2.X - mousePt1.X, mousePt2.Y - mousePt1.Y );
			Rectangle rect = new Rectangle( screenPt, size );

			Rectangle chartRect = Rectangle.Round( _dragPane.Chart._rect );

			Point chartPt = PointToScreen( chartRect.Location );
			if ( !_isEnableVZoom )
			{
				rect.Y = chartPt.Y;
				rect.Height = chartRect.Height + 1;
			}
			else if ( !_isEnableHZoom )
			{
				rect.X = chartPt.X;
				rect.Width = chartRect.Width + 1;
			}

			return rect;
		}

	#endregion

	#region ScrollBars

		private void vScrollBar1_Scroll( object sender, ScrollEventArgs e )
		{
			if ( this.GraphPane != null )
			{
				for ( int i = 0; i < this.GraphPane.YAxisList.Count; i++ )
				{
					ScrollRange scroll = _yScrollRangeList[i];
					if ( scroll.IsScrollable )
					{
						Axis axis = this.GraphPane.YAxisList[i];
						HandleScroll( axis, e.NewValue, scroll.Min, scroll.Max, vScrollBar1.LargeChange,
										!axis.Scale.IsReverse );
					}
				}

				for ( int i = 0; i < this.GraphPane.Y2AxisList.Count; i++ )
				{
					ScrollRange scroll = _y2ScrollRangeList[i];
					if ( scroll.IsScrollable )
					{
						Axis axis = this.GraphPane.Y2AxisList[i];
						HandleScroll( axis, e.NewValue, scroll.Min, scroll.Max, vScrollBar1.LargeChange,
										!axis.Scale.IsReverse );
					}
				}
			}

			ApplyToAllPanes( this.GraphPane );

			if ( _zoomState != null && this.GraphPane != null )
			{
				// Provide Callback to notify the user of pan events
				if ( this.ScrollProgressEvent != null )
					this.ScrollProgressEvent( this, vScrollBar1, _zoomState,
								new ZoomState( this.GraphPane, ZoomState.StateType.Scroll ) );
			}
		}

		private void ApplyToAllPanes( GraphPane primaryPane )
		{
			foreach ( GraphPane pane in _masterPane._paneList )
			{
				if ( pane != primaryPane )
				{
					if ( _isSynchronizeXAxes )
						Synchronize( primaryPane.XAxis, pane.XAxis );
					if ( _isSynchronizeYAxes )
						Synchronize( primaryPane.YAxis, pane.YAxis );
				}
			}
		}

		private void Synchronize( Axis source, Axis dest )
		{
			dest._scale._min = source._scale._min;
			dest._scale._max = source._scale._max;
			dest._scale._majorStep = source._scale._majorStep;
			dest._scale._minorStep = source._scale._minorStep;
			dest._scale._minAuto = source._scale._minAuto;
			dest._scale._maxAuto = source._scale._maxAuto;
			dest._scale._majorStepAuto = source._scale._majorStepAuto;
			dest._scale._minorStepAuto = source._scale._minorStepAuto;
		}

		private void hScrollBar1_Scroll( object sender, ScrollEventArgs e )
		{
			if ( this.GraphPane != null )
			{
				HandleScroll( this.GraphPane.XAxis, e.NewValue, _xScrollRange.Min, _xScrollRange.Max,
							hScrollBar1.LargeChange, this.GraphPane.XAxis.Scale.IsReverse );
			}

			ApplyToAllPanes( this.GraphPane );

			if ( _zoomState != null && this.GraphPane != null )
			{
				// Provide Callback to notify the user of pan events
				if ( this.ScrollProgressEvent != null )
					this.ScrollProgressEvent( this, hScrollBar1, _zoomState,
								new ZoomState( this.GraphPane, ZoomState.StateType.Scroll ) );
			}
		}

		/// <summary>
		/// Use the MouseCaptureChanged as an indicator for the start and end of a scrolling operation
		/// </summary>
		private void ScrollBarMouseCaptureChanged( object sender, EventArgs e )
		{
			ScrollBar scrollBar = sender as ScrollBar;
			if ( scrollBar != null )
			{
				// If this is the start of a new scroll, then Capture will be true
				if ( scrollBar.Capture )
				{
					// save the original zoomstate
					//_zoomState = new ZoomState( this.GraphPane, ZoomState.StateType.Scroll );
					ZoomStateSave( this.GraphPane, ZoomState.StateType.Scroll );
				}
				else
				{
					// push the prior saved zoomstate, since the scale ranges have already been changed on
					// the fly during the scrolling operation
					if ( _zoomState != null && _zoomState.IsChanged( this.GraphPane ) )
					{
						//this.GraphPane.ZoomStack.Push( _zoomState );
						ZoomStatePush( this.GraphPane );

						// Provide Callback to notify the user of pan events
						if ( this.ScrollDoneEvent != null )
							this.ScrollDoneEvent( this, scrollBar, _zoomState,
										new ZoomState( this.GraphPane, ZoomState.StateType.Scroll ) );

						_zoomState = null;
					}
				}
			}
		}

		private void HandleScroll( Axis axis, int newValue, double scrollMin, double scrollMax,
									int largeChange, bool reverse )
		{
			if ( axis != null )
			{
				if ( scrollMin > axis._scale._min )
					scrollMin = axis._scale._min;
				if ( scrollMax < axis._scale._max )
					scrollMax = axis._scale._max;

				int span = _ScrollControlSpan - largeChange;
				if ( span <= 0 )
					return;

				if ( reverse )
					newValue = span - newValue;

				Scale scale = axis._scale;

				double delta = scale._maxLinearized - scale._minLinearized;
				double scrollMin2 = scale.Linearize( scrollMax ) - delta;
				scrollMin = scale.Linearize( scrollMin );
				//scrollMax = scale.Linearize( scrollMax );
				double val = scrollMin + (double)newValue / (double)span *
						( scrollMin2 - scrollMin );
				scale._minLinearized = val;
				scale._maxLinearized = val + delta;
/*
				if ( axis.Scale.IsLog )
				{
					double ratio = axis._scale._max / axis._scale._min;
					double scrollMin2 = scrollMax / ratio;

					double val = scrollMin * Math.Exp( (double)newValue / (double)span *
								( Math.Log( scrollMin2 ) - Math.Log( scrollMin ) ) );
					axis._scale._min = val;
					axis._scale._max = val * ratio;
				}
				else
				{
					double delta = axis._scale._max - axis._scale._min;
					double scrollMin2 = scrollMax - delta;

					double val = scrollMin + (double)newValue / (double)span *
								( scrollMin2 - scrollMin );
					axis._scale._min = val;
					axis._scale._max = val + delta;
				}
*/
				this.Invalidate();
			}
		}

		/// <summary>
		/// Sets the value of the scroll range properties (see <see cref="ScrollMinX" />,
		/// <see cref="ScrollMaxX" />, <see cref="YScrollRangeList" />, and 
		/// <see cref="Y2ScrollRangeList" /> based on the actual range of the data for
		/// each corresponding <see cref="Axis" />.
		/// </summary>
		/// <remarks>
		/// This method is called automatically by <see cref="AxisChange" /> if <see cref="IsAutoScrollRange" />
		/// is true.  Note that this will not be called if you call AxisChange directly from the
		/// <see cref="GraphPane" />.  For example, zedGraphControl1.AxisChange() works properly, but
		/// zedGraphControl1.GraphPane.AxisChange() does not.</remarks>
		public void SetScrollRangeFromData()
		{
			if ( this.GraphPane != null )
			{
				_xScrollRange.Min = this.GraphPane.XAxis.Scale._rangeMin;
				_xScrollRange.Max = this.GraphPane.XAxis.Scale._rangeMax;
				_xScrollRange.IsScrollable = true;

				for ( int i = 0; i < this.GraphPane.YAxisList.Count; i++ )
				{
					Axis axis = this.GraphPane.YAxisList[i];
					ScrollRange range = new ScrollRange( axis.Scale._rangeMin, axis.Scale._rangeMax,
															_yScrollRangeList[i].IsScrollable );
					if ( i >= _yScrollRangeList.Count )
						_yScrollRangeList.Add( range );
					else
						_yScrollRangeList[i] = range;
				}

				for ( int i = 0; i < this.GraphPane.Y2AxisList.Count; i++ )
				{
					Axis axis = this.GraphPane.Y2AxisList[i];
					ScrollRange range = new ScrollRange( axis.Scale._rangeMin, axis.Scale._rangeMax,
															_y2ScrollRangeList[i].IsScrollable );
					if ( i >= _y2ScrollRangeList.Count )
						_y2ScrollRangeList.Add( range );
					else
						_y2ScrollRangeList[i] = range;
				}

				//this.GraphPane.CurveList.GetRange( out scrollMinX, out scrollMaxX,
				//		out scrollMinY, out scrollMaxY, out scrollMinY2, out scrollMaxY2, false, false,
				//		this.GraphPane );
			}
		}

		private void SetScroll( ScrollBar scrollBar, Axis axis, double scrollMin, double scrollMax )
		{
			if ( scrollBar != null && axis != null )
			{
				scrollBar.Minimum = 0;
				scrollBar.Maximum = _ScrollControlSpan - 1;

				if ( scrollMin > axis._scale._min )
					scrollMin = axis._scale._min;
				if ( scrollMax < axis._scale._max )
					scrollMax = axis._scale._max;

				int val = 0;

				Scale scale = axis._scale;
				double minLinearized = scale._minLinearized;
				double maxLinearized = scale._maxLinearized;
				scrollMin = scale.Linearize( scrollMin );
				scrollMax = scale.Linearize( scrollMax );

				double scrollMin2 = scrollMax - ( maxLinearized - minLinearized );
				/*
				if ( axis.Scale.IsLog )
					scrollMin2 = scrollMax / ( axis._scale._max / axis._scale._min );
				else
					scrollMin2 = scrollMax - ( axis._scale._max - axis._scale._min );
				*/
				if ( scrollMin >= scrollMin2 )
				{
					//scrollBar.Visible = false;
					scrollBar.Enabled = false;
					scrollBar.Value = 0;
				}
				else
				{
					double ratio = ( maxLinearized - minLinearized ) / ( scrollMax - scrollMin );

					/*
					if ( axis.Scale.IsLog )
						ratio = ( Math.Log( axis._scale._max ) - Math.Log( axis._scale._min ) ) /
									( Math.Log( scrollMax ) - Math.Log( scrollMin ) );
					else
						ratio = ( axis._scale._max - axis._scale._min ) / ( scrollMax - scrollMin );
					*/

					int largeChange = (int)( ratio * _ScrollControlSpan + 0.5 );
					if ( largeChange < 1 )
						largeChange = 1;
					scrollBar.LargeChange = largeChange;

					int smallChange = largeChange / _ScrollSmallRatio;
					if ( smallChange < 1 )
						smallChange = 1;
					scrollBar.SmallChange = smallChange;

					int span = _ScrollControlSpan - largeChange;

					val = (int)( ( minLinearized - scrollMin ) / ( scrollMin2 - scrollMin ) *
									span + 0.5 );
					/*
					if ( axis.Scale.IsLog )
						val = (int)( ( Math.Log( axis._scale._min ) - Math.Log( scrollMin ) ) /
								( Math.Log( scrollMin2 ) - Math.Log( scrollMin ) ) * span + 0.5 );
					else
						val = (int)( ( axis._scale._min - scrollMin ) / ( scrollMin2 - scrollMin ) *
								span + 0.5 );
					*/
					if ( val < 0 )
						val = 0;
					else if ( val > span )
						val = span;

					//if ( ( axis is XAxis && axis.IsReverse ) || ( ( ! axis is XAxis ) && ! axis.IsReverse ) )
					if ( ( axis is XAxis ) == axis.Scale.IsReverse )
						val = span - val;

					if ( val < scrollBar.Minimum )
						val = scrollBar.Minimum;
					if ( val > scrollBar.Maximum )
						val = scrollBar.Maximum;

					scrollBar.Value = val;
					scrollBar.Enabled = true;
					//scrollBar.Visible = true;
				}
			}
		}

	#endregion

	#region ContextMenu

		/// <summary>
		/// protected method to handle the popup context menu in the <see cref="ZedGraphControl"/>.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void contextMenuStrip1_Opening( object sender, CancelEventArgs e )
		{
			// disable context menu by default
			e.Cancel = true;
			ContextMenuStrip menuStrip = sender as ContextMenuStrip;

			if ( _masterPane != null && menuStrip != null )
			{
				menuStrip.Items.Clear();

				_isZooming = false;
				_isPanning = false;
				Cursor.Current = Cursors.Default;

				_menuClickPt = this.PointToClient( Control.MousePosition );
				GraphPane pane = _masterPane.FindPane( _menuClickPt );

				if ( _isShowContextMenu )
				{
					string menuStr = string.Empty;

					ToolStripMenuItem item = new ToolStripMenuItem();
					item.Name = "copy";
					item.Text = _resourceManager.GetString( "copy" );
					item.Click += new System.EventHandler( this.MenuClick_Copy );
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "save_as";
					item.Text = _resourceManager.GetString( "save_as" );
					item.Click += new System.EventHandler( this.MenuClick_SaveAs );
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "page_setup";
					item.Text = _resourceManager.GetString( "page_setup" );
					item.Click += new System.EventHandler( this.MenuClick_PageSetup );
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "print";
					item.Text = _resourceManager.GetString( "print" );
					item.Click += new System.EventHandler( this.MenuClick_Print );
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "show_val";
					item.Text = _resourceManager.GetString( "show_val" );
					item.Click += new System.EventHandler( this.MenuClick_ShowValues );
					item.Checked = this.IsShowPointValues;
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "unzoom";

					if ( pane == null || pane.ZoomStack.IsEmpty )
						menuStr = _resourceManager.GetString( "unzoom" );
					else
					{
						switch ( pane.ZoomStack.Top.Type )
						{
							case ZoomState.StateType.Zoom:
								menuStr = _resourceManager.GetString( "unzoom" );
								break;
							case ZoomState.StateType.Pan:
								menuStr = _resourceManager.GetString( "unpan" );
								break;
							case ZoomState.StateType.Scroll:
								menuStr = _resourceManager.GetString( "unscroll" );
								break;
						}
					}

					//menuItem.Text = "Un-" + ( ( pane == null || pane.zoomStack.IsEmpty ) ?
					//	"Zoom" : pane.zoomStack.Top.TypeString );
					item.Text = menuStr;
					item.Click += new EventHandler( this.MenuClick_ZoomOut );
					if ( pane == null || pane.ZoomStack.IsEmpty )
						item.Enabled = false;
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "undo_all";
					menuStr = _resourceManager.GetString( "undo_all" );
					item.Text = menuStr;
					item.Click += new EventHandler( this.MenuClick_ZoomOutAll );
					if ( pane == null || pane.ZoomStack.IsEmpty )
						item.Enabled = false;
					menuStrip.Items.Add( item );

					item = new ToolStripMenuItem();
					item.Name = "set_default";
					menuStr = _resourceManager.GetString( "set_default" );
					item.Text = menuStr;
					item.Click += new EventHandler( this.MenuClick_RestoreScale );
					if ( pane == null )
						item.Enabled = false;
					menuStrip.Items.Add( item );

					// if e.Cancel is set to false, the context menu does not display
					// it is initially set to false because the context menu has no items
					e.Cancel = false;

					// Provide Callback for User to edit the context menu
					if ( this.ContextMenuBuilder != null )
						this.ContextMenuBuilder( this, menuStrip, _menuClickPt );
				}
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
			Copy( _isShowCopyMessage );
		}

		/// <summary>
		/// Handler for the "Copy" context menu item.  Copies the current image to a bitmap on the
		/// clipboard.
		/// </summary>
		/// <param name="isShowMessage">boolean value that determines whether or not a prompt will be
		/// displayed.  true to show a message of "Image Copied to ClipBoard".</param>
		public void Copy( bool isShowMessage )
		{
			if ( _masterPane != null )
			{
				Clipboard.SetDataObject( _masterPane.GetImage(), true );
				if ( isShowMessage )
				{
					string str = _resourceManager.GetString( "copied_to_clip" );
					//MessageBox.Show( "Image Copied to ClipBoard" );
					MessageBox.Show( str );
				}
			}
		}

		/// <summary>
		/// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
		/// file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_SaveAs( System.Object sender, System.EventArgs e )
		{
			SaveAs();
		}

		/// <summary>
		/// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
		/// file.
		/// </summary>
		public void SaveAs()
		{
			if ( _masterPane != null )
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
					if ( myStream != null )
					{
						_masterPane.GetImage().Save( myStream, format );
						myStream.Close();
					}
				}
			}
		}

		/// <summary>
		/// Handler for the "Show Values" context menu item.  Toggles the <see cref="IsShowPointValues"/>
		/// property, which activates the point value tooltips.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_ShowValues( object sender, System.EventArgs e )
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if ( item != null )
				this.IsShowPointValues = !item.Checked;
		}

		/// <summary>
		/// Handler for the "Set Scale to Default" context menu item.  Sets the scale ranging to
		/// full auto mode for all axes.
		/// </summary>
		/// <remarks>
		/// This method differs from the <see cref="ZoomOutAll" /> method in that it sets the scales
		/// to full auto mode.  The <see cref="ZoomOutAll" /> method sets the scales to their initial
		/// setting prior to any user actions (which may or may not be full auto mode).
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_RestoreScale( object sender, EventArgs e )
		{
			if ( _masterPane != null )
			{
				GraphPane pane = _masterPane.FindPane( _menuClickPt );
				RestoreScale( pane );
			}
		}

		/// <summary>
		/// Handler for the "Set Scale to Default" context menu item.  Sets the scale ranging to
		/// full auto mode for all axes.
		/// </summary>
		/// <remarks>
		/// This method differs from the <see cref="ZoomOutAll" /> method in that it sets the scales
		/// to full auto mode.  The <see cref="ZoomOutAll" /> method sets the scales to their initial
		/// setting prior to any user actions (which may or may not be full auto mode).
		/// </remarks>
		/// <param name="pane">The <see cref="GraphPane" /> object which is to have the scale restored</param>
		public void RestoreScale( GraphPane pane )
		{
			if ( pane != null )
			{
				//Go ahead and save the old zoomstates, which provides an "undo"-like capability
				ZoomState oldState = pane.ZoomStack.Push( pane, ZoomState.StateType.Zoom );
				//ZoomState oldState = new ZoomState( pane, ZoomState.StateType.Zoom );

				Graphics g = this.CreateGraphics();
				pane.XAxis.ResetAutoScale( pane, g );
				foreach ( YAxis axis in pane.YAxisList )
					axis.ResetAutoScale( pane, g );
				foreach ( Y2Axis axis in pane.Y2AxisList )
					axis.ResetAutoScale( pane, g );

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
			if ( _masterPane != null )
			{
				GraphPane pane = _masterPane.FindPane( _menuClickPt );
				ZoomOut( pane );
			}
		}

		/// <summary>
		/// Handler for the "UnZoom/UnPan" context menu item.  Restores the scale ranges to the values
		/// before the last zoom, pan, or scroll operation.
		/// </summary>
		/// <remarks>
		/// Triggers a <see cref="ZoomEvent" /> for any type of undo (including pan, scroll, zoom, and
		/// wheelzoom).  This method will affect all the
		/// <see cref="GraphPane" /> objects in the <see cref="MasterPane" /> if
		/// <see cref="IsSynchronizeXAxes" /> or <see cref="IsSynchronizeYAxes" /> is true.
		/// </remarks>
		/// <param name="primaryPane">The primary <see cref="GraphPane" /> object which is to be
		/// zoomed out</param>
		public void ZoomOut( GraphPane primaryPane )
		{
			if ( primaryPane != null && !primaryPane.ZoomStack.IsEmpty )
			{
				ZoomState.StateType type = primaryPane.ZoomStack.Top.Type;

				ZoomState oldState = new ZoomState( primaryPane, type );
				ZoomState newState = null;
				if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
				{
					foreach ( GraphPane pane in _masterPane._paneList )
					{
						ZoomState state = pane.ZoomStack.Pop( pane );
						if ( pane == primaryPane )
							newState = state;
					}
				}
				else
					newState = primaryPane.ZoomStack.Pop( primaryPane );

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
		/// <remarks>
		/// This method differs from the <see cref="RestoreScale" /> method in that it sets the scales
		/// to their initial setting prior to any user actions.  The <see cref="RestoreScale" /> method
		/// sets the scales to full auto mode (regardless of what the initial setting may have been).
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_ZoomOutAll( System.Object sender, System.EventArgs e )
		{
			if ( _masterPane != null )
			{
				GraphPane pane = _masterPane.FindPane( _menuClickPt );
				ZoomOutAll( pane );
			}
		}

		/// <summary>
		/// Handler for the "Undo All Zoom/Pan" context menu item.  Restores the scale ranges to the values
		/// before all zoom and pan operations
		/// </summary>
		/// <remarks>
		/// This method differs from the <see cref="RestoreScale" /> method in that it sets the scales
		/// to their initial setting prior to any user actions.  The <see cref="RestoreScale" /> method
		/// sets the scales to full auto mode (regardless of what the initial setting may have been).
		/// </remarks>
		/// <param name="primaryPane">The <see cref="GraphPane" /> object which is to be zoomed out</param>
		public void ZoomOutAll( GraphPane primaryPane )
		{
			if ( primaryPane != null && !primaryPane.ZoomStack.IsEmpty )
			{
				ZoomState.StateType type = primaryPane.ZoomStack.Top.Type;

				ZoomState oldState = new ZoomState( primaryPane, type );
				//ZoomState newState = pane.ZoomStack.PopAll( pane );
				ZoomState newState = null;
				if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
				{
					foreach ( GraphPane pane in _masterPane._paneList )
					{
						ZoomState state = pane.ZoomStack.PopAll( pane );
						if ( pane == primaryPane )
							newState = state;
					}
				}
				else
					newState = primaryPane.ZoomStack.PopAll( primaryPane );

				// Provide Callback to notify the user of zoom events
				if ( this.ZoomEvent != null )
					this.ZoomEvent( this, oldState, newState );

				Refresh();
			}
		}

	#endregion

	#region Printing

		/// <summary>
		/// Handler for the "Page Setup..." context menu item.   Displays a
		/// <see cref="PageSetupDialog" />.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_PageSetup( object sender, EventArgs e )
		{
			DoPageSetup();
		}

		/// <summary>
		/// Handler for the "Print..." context menu item.   Displays a
		/// <see cref="PrintDialog" />.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void MenuClick_Print( object sender, EventArgs e )
		{
			DoPrint();
		}

		/// <summary>
		/// Rendering method used by the print context menu items
		/// </summary>
		/// <param name="sender">The applicable <see cref="PrintDocument" />.</param>
		/// <param name="e">A <see cref="PrintPageEventArgs" /> instance providing
		/// page bounds, margins, and a Graphics instance for this printed output.
		/// </param>
		private void Graph_PrintPage( object sender, PrintPageEventArgs e )
		{
			PrintDocument pd = sender as PrintDocument;

			MasterPane mPane = this.MasterPane;
			bool[] isPenSave = new bool[mPane.PaneList.Count + 1];
			bool[] isFontSave = new bool[mPane.PaneList.Count + 1];
			isPenSave[0] = mPane.IsPenWidthScaled;
			isFontSave[0] = mPane.IsFontsScaled;
			for ( int i = 0; i < mPane.PaneList.Count; i++ )
			{
				isPenSave[i + 1] = mPane[i].IsPenWidthScaled;
				isFontSave[i + 1] = mPane[i].IsFontsScaled;
				if ( _isPrintScaleAll )
				{
					mPane[i].IsPenWidthScaled = true;
					mPane[i].IsFontsScaled = true;
				}
			}

			RectangleF saveRect = mPane.Rect;
			SizeF newSize = mPane.Rect.Size;
			if ( _isPrintFillPage && _isPrintKeepAspectRatio )
			{
				float xRatio = (float)e.MarginBounds.Width / (float)newSize.Width;
				float yRatio = (float)e.MarginBounds.Height / (float)newSize.Height;
				float ratio = Math.Min( xRatio, yRatio );

				newSize.Width *= ratio;
				newSize.Height *= ratio;
			}
			else if ( _isPrintFillPage )
				newSize = e.MarginBounds.Size;

			mPane.ReSize( e.Graphics, new RectangleF( e.MarginBounds.Left,
				e.MarginBounds.Top, newSize.Width, newSize.Height ) );
			mPane.Draw( e.Graphics );

			Graphics g = this.CreateGraphics();
			mPane.ReSize( g, saveRect );
			g.Dispose();

			mPane.IsPenWidthScaled = isPenSave[0];
			mPane.IsFontsScaled = isFontSave[0];
			for ( int i = 0; i < mPane.PaneList.Count; i++ )
			{
				mPane[i].IsPenWidthScaled = isPenSave[i + 1];
				mPane[i].IsFontsScaled = isFontSave[i + 1];
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.Printing.PrintDocument" /> instance
		/// that is used for all of the context menu printing functions.
		/// </summary>
		public PrintDocument PrintDocument
		{
			get
			{
				if ( _pdSave == null )
					_pdSave = new PrintDocument();
				return _pdSave;
			}
			set { _pdSave = value; }
		}

		/// <summary>
		/// Display a <see cref="PageSetupDialog" /> to the user, allowing them to modify
		/// the print settings for this <see cref="ZedGraphControl" />.
		/// </summary>
		public void DoPageSetup()
		{
			PrintDocument pd = PrintDocument;

			if ( pd != null )
			{
				//pd.PrintPage += new PrintPageEventHandler( GraphPrintPage );
				PageSetupDialog setupDlg = new PageSetupDialog();
				setupDlg.Document = pd;
				if ( setupDlg.ShowDialog() == DialogResult.OK )
				{
					pd.PrinterSettings = setupDlg.PrinterSettings;
					pd.DefaultPageSettings = setupDlg.PageSettings;

					// BUG in PrintDocument!!!  Converts in/mm repeatedly
					// http://support.microsoft.com/?id=814355
					// from http://www.vbinfozine.com/tpagesetupdialog.shtml, by Palo Mraz
					//if ( System.Globalization.RegionInfo.CurrentRegion.IsMetric )
					//{
					//	setupDlg.Document.DefaultPageSettings.Margins = PrinterUnitConvert.Convert(
					//	setupDlg.Document.DefaultPageSettings.Margins,
					//	PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter );
					//}
				}
			}
		}

		/// <summary>
		/// Display a <see cref="PrintDialog" /> to the user, allowing them to select a
		/// printer and print the <see cref="MasterPane" /> contained in this
		/// <see cref="ZedGraphControl" />.
		/// </summary>
		public void DoPrint()
		{
			PrintDocument pd = PrintDocument;

			if ( pd != null )
			{
				pd.PrintPage += new PrintPageEventHandler( Graph_PrintPage );
				PrintDialog pDlg = new PrintDialog();
				pDlg.Document = pd;
				if ( pDlg.ShowDialog() == DialogResult.OK )
					pd.Print();
			}
		}

		/// <summary>
		/// Display a <see cref="PrintPreviewDialog" />, allowing the user to preview and
		/// subsequently print the <see cref="MasterPane" /> contained in this
		/// <see cref="ZedGraphControl" />.
		/// </summary>
		public void DoPrintPreview()
		{
			PrintDocument pd = PrintDocument;

			if ( pd != null )
			{
				PrintPreviewDialog ppd = new PrintPreviewDialog();
				pd.PrintPage += new PrintPageEventHandler( Graph_PrintPage );
				ppd.Document = pd;
				ppd.Show();
			}
		}

	#endregion

	#region Zoom States

		/// <summary>
		/// Save the current states of the GraphPanes to a separate collection.  Save a single
		/// (<see paramref="primaryPane" />) GraphPane if the panes are not synchronized
		/// (see <see cref="IsSynchronizeXAxes" /> and <see cref="IsSynchronizeYAxes" />),
		/// or save a list of states for all GraphPanes if the panes are synchronized.
		/// </summary>
		/// <param name="primaryPane">The primary GraphPane on which zoom/pan/scroll operations
		/// are taking place</param>
		/// <param name="type">The <see cref="ZoomState.StateType" /> that describes the
		/// current operation</param>
		/// <returns>The <see cref="ZoomState" /> that corresponds to the
		/// <see paramref="primaryPane" />.
		/// </returns>
		private ZoomState ZoomStateSave( GraphPane primaryPane, ZoomState.StateType type )
		{
			ZoomStateClear();

			if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
			{
				foreach ( GraphPane pane in _masterPane._paneList )
				{
					ZoomState state = new ZoomState( pane, type );
					if ( pane == primaryPane )
						_zoomState = state;
					_zoomStateStack.Add( state );
				}
			}
			else
				_zoomState = new ZoomState( primaryPane, type );

			return _zoomState;
		}

		/// <summary>
		/// Restore the states of the GraphPanes to a previously saved condition (via
		/// <see cref="ZoomStateSave" />.  This is essentially an "undo" for live
		/// pan and scroll actions.  Restores a single
		/// (<see paramref="primaryPane" />) GraphPane if the panes are not synchronized
		/// (see <see cref="IsSynchronizeXAxes" /> and <see cref="IsSynchronizeYAxes" />),
		/// or save a list of states for all GraphPanes if the panes are synchronized.
		/// </summary>
		/// <param name="primaryPane">The primary GraphPane on which zoom/pan/scroll operations
		/// are taking place</param>
		private void ZoomStateRestore( GraphPane primaryPane )
		{
			if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
			{
				for ( int i = 0; i < _masterPane._paneList.Count; i++ )
				{
					if ( i < _zoomStateStack.Count )
						_zoomStateStack[i].ApplyState( _masterPane._paneList[i] );
				}
			}
			else if ( _zoomState != null )
				_zoomState.ApplyState( primaryPane );

			ZoomStateClear();
		}

		/// <summary>
		/// Place the previously saved states of the GraphPanes on the individual GraphPane
		/// <see cref="ZedGraph.GraphPane.ZoomStack" /> collections.  This provides for an
		/// option to undo the state change at a later time.  Save a single
		/// (<see paramref="primaryPane" />) GraphPane if the panes are not synchronized
		/// (see <see cref="IsSynchronizeXAxes" /> and <see cref="IsSynchronizeYAxes" />),
		/// or save a list of states for all GraphPanes if the panes are synchronized.
		/// </summary>
		/// <param name="primaryPane">The primary GraphPane on which zoom/pan/scroll operations
		/// are taking place</param>
		/// <returns>The <see cref="ZoomState" /> that corresponds to the
		/// <see paramref="primaryPane" />.
		/// </returns>
		private void ZoomStatePush( GraphPane primaryPane )
		{
			if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
			{
				for ( int i = 0; i < _masterPane._paneList.Count; i++ )
				{
					if ( i < _zoomStateStack.Count )
						_masterPane._paneList[i].ZoomStack.Add( _zoomStateStack[i] );
				}
			}
			else if ( _zoomState != null )
				primaryPane.ZoomStack.Add( _zoomState );

			ZoomStateClear();
		}

		/// <summary>
		/// Clear the collection of saved states.
		/// </summary>
		private void ZoomStateClear()
		{
			_zoomStateStack.Clear();
			_zoomState = null;
		}

		/// <summary>
		/// Clear all states from the undo stack for each GraphPane.
		/// </summary>
		private void ZoomStatePurge()
		{
			foreach ( GraphPane pane in _masterPane._paneList )
				pane.ZoomStack.Clear();
		}

	#endregion

	}
}
