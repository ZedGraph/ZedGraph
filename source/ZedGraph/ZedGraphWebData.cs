//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005  John Champion
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
using System.Web.UI;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZedGraph
{
	#region ZedGraphWebCurveItem
	/// <summary>
	/// Baseclass for curve items in the web control
	/// <seealso cref="CurveItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebCurveItem : GenericItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "CurveItem: " + Label;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebCurveItem() : base()
		{
		}

		/// <summary>
		/// Constructor that accepts a default label value
		/// </summary>
		/// <param name="label">Curve item label</param>
		public ZedGraphWebCurveItem(string label) : base()
		{
			Label = label;
		}
		
		/// <summary>
		/// <seealso cref="CurveItem.Label"/>
		/// </summary>
		[Category("Curve Details"),NotifyParentProperty(true),
		Description("A text string that represents the Legend entry for the "+
			"this CurveItem object")]
		public string Label
		{
			get 
			{ 
				object x = ViewState["Label"]; 
				return (null == x) ? String.Empty : (string)x;
			}
			set { ViewState["Label"] = value; }
		}

		/// <summary>
		/// <seealso cref="CurveItem.Color"/>
		/// </summary>
		[Category("Curve Details"),NotifyParentProperty(true),
		Description("The Line/Symbol/Bar color (FillColor for the Bar). "+
			"This is a common access to Color, Color, and Color properties for this curve.")]
		public Color Color
		{
			get 
			{ 
				object x = ViewState["Color"]; 
				return (null == x) ? Color.Empty : (Color)x;
			}
			set { ViewState["Color"] = value; }
		}

		/// <summary>
		/// <seealso cref="CurveItem.IsVisible"/>
		/// </summary>
		[Category("Curve Details"),NotifyParentProperty(true),
		Description("Determines whether this CurveItem is visible on the graph. "+
			"Note that this value turns the curve display on or off, but it does not "+
			"affect the display of the legend entry. To hide the legend entry, you have "+
			"to set IsLegendLabelVisible to false.")]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		}

		/// <summary>
		/// <seealso cref="CurveItem.IsLegendLabelVisible"/>
		/// </summary>
		[Category("Curve Details"),NotifyParentProperty(true),
		Description("Determines whether the label for this CurveItem is visible in "+
			"the legend. Note that this value turns the legend entry display on or off, "+
			"but it does not affect the display of the curve on the graph. To hide the "+
			"curve, you have to set IsVisible to false. ")]		
		public bool IsLegendLabelVisible
		{
			get 
			{ 
				object x = ViewState["IsLegendLabelVisible"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsLegendLabelVisible"] = value; }
		}

		/// <summary>
		/// <seealso cref="CurveItem.IsY2Axis"/>
		/// </summary>
		[Category("Curve Details"),NotifyParentProperty(true),
		Description("Determines which Y axis this CurveItem is assigned to. The "+
			"YAxis is on the left side of the graph and the Y2Axis is on the right "+
			"side. Assignment to an axis determines the scale that is used to draw the "+
			"curve on the graph.")]
		public bool IsY2Axis
		{
			get 
			{ 
				object x = ViewState["IsY2Axis"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsY2Axis"] = value; }
		}
	}
	#endregion

	#region ZedGraphWebBorder

	/// <summary>
	/// Web control state management class for a <see cref="Border"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebBorder : GenericItem
	{
		public override string ToString()
		{
			return "Border";
		}

		[NotifyParentProperty(true)]
		public Color Color
		{
			get 
			{ 
				object x = ViewState["Color"]; 
				return (null == x) ? Color.Empty : (Color)x;
			}
			set { ViewState["Color"] = value; }
		} 

		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		}

		[NotifyParentProperty(true)]
		public float PenWidth
		{
			get 
			{ 
				object x = ViewState["PenWidth"]; 
				return (null == x) ? 1 : (float)x;
			}
			set { ViewState["PenWidth"] = value; }
		}
	}

	#endregion

	#region ZedGraphWebFill
	/// <summary>
	/// Web control state management class for a <see cref="Fill"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebFill : GenericItem
	{
		public override string ToString()
		{
			return "Fill";
		}

		[NotifyParentProperty(true)]
		public Color Color
		{
			get 
			{ 
				object x = ViewState["Color"]; 
				return (null == x) ? Color.Empty : (Color)x;
			}
			set { ViewState["Color"] = value; }
		} 

		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		}

		[NotifyParentProperty(true)]
		public double RangeMax
		{
			get 
			{ 
				object x = ViewState["RangeMax"]; 
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["RangeMax"] = value; }
		}

		[NotifyParentProperty(true)]
		public double RangeMin
		{
			get 
			{ 
				object x = ViewState["RangeMin"]; 
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["RangeMin"] = value; }
		}

		[NotifyParentProperty(true)]
		public FillType FillType
		{
			get 
			{ 
				object x = ViewState["FillType"]; 
				return (null == x) ? FillType.Solid : (FillType)x;
			}
			set { ViewState["FillType"] = value; }
		}

		[NotifyParentProperty(true)]
		public AlignH AlignH
		{
			get 
			{ 
				object x = ViewState["AlignH"]; 
				return (null == x) ? AlignH.Left : (AlignH)x;
			}
			set { ViewState["AlignH"] = value; }
		}

		[NotifyParentProperty(true)]
		public AlignV AlignV
		{
			get 
			{ 
				object x = ViewState["AlignV"]; 
				return (null == x) ? AlignV.Center : (AlignV)x;
			}
			set { ViewState["AlignV"] = value; }
		}

		[NotifyParentProperty(true)]
		public bool IsScaled
		{
			get 
			{ 
				object x = ViewState["IsScaled"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsScaled"] = value; }
		}

		/*
		[NotifyParentProperty(true)]
		public Brush Brush
		{
			get 
			{ 
				object x = ViewState["Brush"]; 
				return (null == x) ? Brushes.Black : (Brush)x;
			}
			set { ViewState["Brush"] = value; }
		}
		*/	
	}

	#endregion

	#region ZedGraphWebSymbol
	/// <summary>
	/// Web control state management class for a <see cref="Symbol"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebSymbol : GenericItem
	{
		public override string ToString()
		{
			return "Symbol";
		}

		#region Properties
		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		} 
		
		[NotifyParentProperty(true)]
		public float Size
		{
			get 
			{ 
				object x = ViewState["Size"]; 
				return (null == x) ? 1 : (float)x;
			}
			set { ViewState["Size"] = value; }
		} 
		
		[NotifyParentProperty(true)]
		public SymbolType Type
		{
			get 
			{ 
				object x = ViewState["Type"]; 
				return (null == x) ? SymbolType.Default : (SymbolType)x;
			}
			set { ViewState["Type"] = value; }
		} 
		#endregion	
	
		#region Border
		public ZedGraphWebBorder _border;
		/// <summary>
		/// <seealso cref="ZedGraphWebBorder"/>
		/// </summary>
		[		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get
			{
				if ( null == _border )
				{
					_border = new ZedGraphWebBorder();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_border).TrackViewState();						
					}	
				}
				return _border;				
			}
		}
		#endregion

		#region Fill
		public ZedGraphWebFill _fill;
		/// <summary>
		/// <seealso cref="ZedGraphWebFill"/>
		/// </summary>
		[
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get
			{
				if ( null == _fill )
				{
					_fill = new ZedGraphWebFill();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_fill).TrackViewState();						
					}	
				}
				return _fill;				
			}
		}
		#endregion

		#region State Management

		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;
				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)Border).LoadViewState(myState[1]);
			if (myState[2] != null)
				((IStateManager)Fill).LoadViewState(myState[2]);
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[3];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_border != null) ? ((IStateManager)_border).SaveViewState() : null;
			myState[2] = (_fill != null) ? ((IStateManager)_fill).SaveViewState() : null;

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_border != null)
				((IStateManager)_border).TrackViewState();
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();
		}
		#endregion
	}
	#endregion

	#region ZedGraphWebBar
	/// <summary>
	/// Web control state management class for a <see cref="BarItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebBar : ZedGraphWebCurveItem
	{
		public override string ToString()
		{
			return "Bar: " + Label;
		}	

		#region Border
		public ZedGraphWebBorder _border;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get
			{
				if ( null == _border )
				{
					_border = new ZedGraphWebBorder();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_border).TrackViewState();						
					}	
				}
				return _border;				
			}
		}
		#endregion

		#region Fill
		public ZedGraphWebFill _fill;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get
			{
				if ( null == _fill )
				{
					_fill = new ZedGraphWebFill();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_fill).TrackViewState();						
					}	
				}
				return _fill;				
			}
		}
		#endregion

		#region State Management

		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;
				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)Border).LoadViewState(myState[1]);
			if (myState[2] != null)
				((IStateManager)Fill).LoadViewState(myState[2]);
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[3];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_border != null) ? ((IStateManager)_border).SaveViewState() : null;
			myState[2] = (_fill != null) ? ((IStateManager)_fill).SaveViewState() : null;

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_border != null)
				((IStateManager)_border).TrackViewState();
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();
		}
		#endregion
	}
	
	#endregion

	#region ZedGraphWebErrorBar
	/// <summary>
	/// Web control state management class for a <see cref="ErrorBarItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebErrorBar : ZedGraphWebCurveItem
	{
		public override string ToString()
		{
			return "ErrorBar: " + Label;
		}	

		#region Properties
		[NotifyParentProperty(true)]
		public BarBase BarBase
		{
			get 
			{ 
				object x = ViewState["BarBase"]; 
				return (null == x) ? BarBase.X : (BarBase)x;
			}
			set { ViewState["BarBase"] = value; }
		}
 		
		[NotifyParentProperty(true)]
		public float PenWidth
		{
			get 
			{ 
				object x = ViewState["PenWidth"]; 
				return (null == x) ? 1 : (float)x;
			}
			set { ViewState["PenWidth"] = value; }
		}
		#endregion

		#region Symbol
		public ZedGraphWebSymbol _symbol;
		[
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebSymbol Symbol
		{
			get
			{
				if ( null == _symbol )
				{
					_symbol = new ZedGraphWebSymbol();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_symbol).TrackViewState();						
					}	
				}
				return _symbol;				
			}
		}
		#endregion

		#region State Management

		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;
				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)Symbol).LoadViewState(myState[1]);			
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[2];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_symbol != null) ? ((IStateManager)_symbol).SaveViewState() : null;			

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_symbol != null)
				((IStateManager)_symbol).TrackViewState();			
		}
		#endregion
	}

	#endregion

	#region ZedGraphWebHiLowBar
	/// <summary>
	/// Web control state management class for a <see cref="HiLowBarItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebHiLowBar : ZedGraphWebCurveItem
	{
		public override string ToString()
		{
			return "HiLowBar: " + Label;
		}

		#region Properties
		[NotifyParentProperty(true)]
		public BarBase BarBase
		{
			get 
			{ 
				object x = ViewState["BarBase"]; 
				return (null == x) ? BarBase.X : (BarBase)x;
			}
			set { ViewState["BarBase"] = value; }
		} 

		[NotifyParentProperty(true)]
		public bool IsMaximumWidth
		{
			get 
			{ 
				object x = ViewState["IsMaximumWidth"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsMaximumWidth"] = value; }
		} 
		
		[NotifyParentProperty(true)]
		public float Size
		{
			get 
			{ 
				object x = ViewState["Size"]; 
				return (null == x) ? 1 : (float)x;
			}
			set { ViewState["Size"] = value; }
		} 		
		#endregion

		#region Border
		public ZedGraphWebBorder _border;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get
			{
				if ( null == _border )
				{
					_border = new ZedGraphWebBorder();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_border).TrackViewState();						
					}	
				}
				return _border;				
			}
		}
		#endregion

		#region Fill
		public ZedGraphWebFill _fill;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get
			{
				if ( null == _fill )
				{
					_fill = new ZedGraphWebFill();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_fill).TrackViewState();						
					}	
				}
				return _fill;				
			}
		}
		#endregion

		#region State Management

		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;
				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)Border).LoadViewState(myState[1]);
			if (myState[2] != null)
				((IStateManager)Fill).LoadViewState(myState[2]);
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[3];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_border != null) ? ((IStateManager)_border).SaveViewState() : null;
			myState[2] = (_fill != null) ? ((IStateManager)_fill).SaveViewState() : null;

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_border != null)
				((IStateManager)_border).TrackViewState();
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();
		}
		#endregion
	}	

	#endregion
	
	#region ZedGraphWebLine
	/// <summary>
	/// Web control state management class for a <see cref="LineItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebLine : ZedGraphWebCurveItem
	{
		public override string ToString()
		{
			return "Line: " + Label;
		}	
	
		#region Properties
		[NotifyParentProperty(true)]
		public bool IsSmooth
		{
			get 
			{ 
				object x = ViewState["IsSmooth"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsSmooth"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Width
		{
			get 
			{ 
				object x = ViewState["Width"]; 
				return (null == x) ? 1 : (float)x;
			}
			set { ViewState["Width"] = value; }
		}

		[NotifyParentProperty(true)]
		public float SmoothTension
		{
			get 
			{ 
				object x = ViewState["SmoothTension"]; 
				return (null == x) ? 1 : (float)x;
			}
			set { ViewState["SmoothTension"] = value; }
		}

		[NotifyParentProperty(true)]
		public DashStyle DashStyle
		{
			get 
			{ 
				object x = ViewState["DashStyle"]; 
				return (null == x) ? DashStyle.Solid : (DashStyle)x;
			}
			set { ViewState["DashStyle"] = value; }
		}

		[NotifyParentProperty(true)]
		public StepType StepType
		{
			get 
			{ 
				object x = ViewState["StepType"]; 
				return (null == x) ? StepType.NonStep : (StepType)x;
			}
			set { ViewState["StepType"] = value; }
		}
		#endregion

		#region Fill
		public ZedGraphWebFill _fill;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get
			{
				if ( null == _fill )
				{
					_fill = new ZedGraphWebFill();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_fill).TrackViewState();						
					}	
				}
				return _fill;				
			}
		}
		#endregion
		
		#region Symbol
		public ZedGraphWebSymbol _symbol;
		[
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebSymbol Symbol
		{
			get
			{
				if ( null == _symbol )
				{
					_symbol = new ZedGraphWebSymbol();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_symbol).TrackViewState();						
					}	
				}
				return _symbol;				
			}
		}
		#endregion

		#region State Management

		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;
				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)Symbol).LoadViewState(myState[1]);			
			if (myState[2] != null)
				((IStateManager)Fill).LoadViewState(myState[2]);			
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[3];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_symbol != null) ? ((IStateManager)_symbol).SaveViewState() : null;			
			myState[2] = (_fill != null) ? ((IStateManager)_fill).SaveViewState() : null;			

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_symbol != null)
				((IStateManager)_symbol).TrackViewState();			
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();			
		}
		#endregion
	}
	#endregion

	#region ZedGraphWebPieSlice
	/// <summary>
	/// Web control state management class for a <see cref="PieSlice"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebPieSlice : GenericItem
	{
		public override string ToString()
		{
			return "PieItem: " + Label;
		}

		public ZedGraphWebPieSlice() : base()
		{
		}

		public ZedGraphWebPieSlice(string label) : base()
		{
			Label = label;
		}

		#region Properties
		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		}
		
		[NotifyParentProperty(true)]
		public string Label
		{
			get 
			{ 
				object x = ViewState["Label"]; 
				return (null == x) ? String.Empty : (string)x;
			}
			set { ViewState["Label"] = value; }
		}

		[NotifyParentProperty(true)]
		public double Displacement
		{
			get 
			{ 
				object x = ViewState["Displacement"]; 
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["Displacement"] = value; }
		}

		[NotifyParentProperty(true)]
		public double Value
		{
			get 
			{ 
				object x = ViewState["Value"]; 
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["Value"] = value; }
		}
		#endregion

		#region Border
		public ZedGraphWebBorder _border;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get
			{
				if ( null == _border )
				{
					_border = new ZedGraphWebBorder();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_border).TrackViewState();						
					}	
				}
				return _border;				
			}
		}
		#endregion

		#region Fill
		public ZedGraphWebFill _fill;
		[
		Category("Bar Details"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get
			{
				if ( null == _fill )
				{
					_fill = new ZedGraphWebFill();
					if (((IStateManager)this).IsTrackingViewState) 
					{	
						((IStateManager)_fill).TrackViewState();						
					}	
				}
				return _fill;				
			}
		}
		#endregion

		#region State Management

		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;
				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)Border).LoadViewState(myState[1]);
			if (myState[2] != null)
				((IStateManager)Fill).LoadViewState(myState[2]);
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[3];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_border != null) ? ((IStateManager)_border).SaveViewState() : null;
			myState[2] = (_fill != null) ? ((IStateManager)_fill).SaveViewState() : null;

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_border != null)
				((IStateManager)_border).TrackViewState();
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();
		}
		#endregion
	}
	#endregion

	#region ZedGraphWebPie
	/// <summary>
	/// Web control state management class for a <see cref="PieItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebPie : ZedGraphWebCurveItem
	{
		public override string ToString()
		{
			return "Pie: " + Label;
		}
		
		#region Properties
		[NotifyParentProperty(true)]
		public PieLabelType LabelType
		{
			get 
			{ 
				object x = ViewState["LabelType"]; 
				return (null == x) ? PieLabelType.Name_Percent : (PieLabelType)x;
			}
			set { ViewState["LabelType"] = value; }
		} 

		[NotifyParentProperty(true)]
		public PieType PieType
		{
			get 
			{ 
				object x = ViewState["PieType"]; 
				return (null == x) ? PieType.Pie2D : (PieType)x;
			}
			set { ViewState["PieType"] = value; }
		} 

		[NotifyParentProperty(true)]
		public string PieTitle
		{
			get 
			{ 
				object x = ViewState["PieTitle"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["PieTitle"] = value; }
		} 
		#endregion

		#region Pie Slice Collection
		protected ZedGraphWebSliceCollection _slicelist = null;
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebSliceCollection SliceList
		{
			get 
			{				
				if ( null == _slicelist )
				{
					_slicelist = new ZedGraphWebSliceCollection();
					if (((IStateManager)this).IsTrackingViewState)  
					{
						((IStateManager)_slicelist).TrackViewState();
					}					
				}
				return _slicelist;
			}			
		}
		#endregion

		#region State Management
		protected override void LoadViewState(object savedState) 
		{
			object baseState = null;
			object[] myState = null;

			if (savedState != null) 
			{
				myState = (object[])savedState;

				baseState = myState[0];
			}

			// Always call the base class, even if the saved state is null, so
			// that the base class gets a chance implement its LoadViewState
			// functionality.
			base.LoadViewState(baseState);
            
			if (myState == null) 
			{
				return;
			}

			// NOTE: Accessing a style causes the style to be created if it
			//       is null. For perf reasons, a style should be created 
			//       only if there is saved state for that style.           

			if (myState[1] != null)
				((IStateManager)SliceList).LoadViewState(myState[1]);			
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[2];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_slicelist != null) ? ((IStateManager)_slicelist).SaveViewState() : null;			

			// NOTE: We don't check for all nulls, because the control is almost certain to
			//       have some view state. Most data-bound controls save state information 
			//       to recreate themselves without a live data source on postback.
			return myState;
		}

		protected override void TrackViewState() 
		{
			base.TrackViewState();

			// NOTE: Start tracking state on styles that have been created.
			//       New styles created hereafter will start
			//       tracking view state when they are demand created.

			if (_slicelist != null)
				((IStateManager)_slicelist).TrackViewState();			
		}
		#endregion
	}
	#endregion

	#region ZedGraphWebSliceCollection
	/// <summary>
	/// Manages a collection of <see cref="ZedGraphWebPieSlice"/> objects that are 
	/// state management aware.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebSliceCollection : GenericCollection
	{	
		public override string ToString(){return String.Empty;}		

		public ZedGraphWebSliceCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[1];
			Schema[0].code = 'p';
			Schema[0].type = typeof(ZedGraphWebPieSlice);			
		}

		public void Add(ZedGraphWebPieSlice item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		[NotifyParentProperty(true)]
		public ZedGraphWebPieSlice this [int index]
		{
			get 
			{
				return (ZedGraphWebPieSlice)ListGet(index);
			}
			set
			{
				ListInsert(index,value);
			}
		}			
	}

	#endregion

	#region ZedGraphWebCurveCollection
	/// <summary>
	/// Manages a collection of <see cref="ZedGraphWebCurveItem"/> objects that are 
	/// state management aware.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebCurveCollection : GenericCollection
	{	
		public override string ToString(){return String.Empty;}		

		public ZedGraphWebCurveCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[5];
			Schema[0].code = 'b';
			Schema[0].type = typeof(ZedGraphWebBar);
			Schema[1].code = 'l';
			Schema[1].type = typeof(ZedGraphWebLine);
			Schema[2].code = 'e';
			Schema[2].type = typeof(ZedGraphWebErrorBar);
			Schema[3].code = 'h';
			Schema[3].type = typeof(ZedGraphWebHiLowBar);
			Schema[4].code = 'p';
			Schema[4].type = typeof(ZedGraphWebPie);
		}

		public void Add(ZedGraphWebCurveItem item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		[NotifyParentProperty(true)]
		public ZedGraphWebCurveItem this [int index]
		{
			get 
			{
				return (ZedGraphWebCurveItem)ListGet(index);
			}
			set
			{
				ListInsert(index,value);
			}
		}			
	}

	#endregion
}
