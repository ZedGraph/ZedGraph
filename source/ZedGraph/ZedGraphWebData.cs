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
	[DefaultProperty("Label")]
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

		#region Properties
		
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
		#endregion
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

		public ZedGraphWebSymbol() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
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
				object x = ViewState["SymbolType"]; 
				return (null == x) ? SymbolType.Default : (SymbolType)x;
			}
			set { ViewState["SymbolType"] = value; }
		} 
		#endregion	
	
		#region Border
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebBorder"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get { return (ZedGraphWebBorder)base.GetValue('b'); }
		}
		#endregion

		#region Fill
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebFill"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get { return (ZedGraphWebFill)base.GetValue('f'); }
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

		public ZedGraphWebBar() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
		}

		#region Border
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebBorder"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get { return (ZedGraphWebBorder)base.GetValue('b'); }
		}
		#endregion

		#region Fill
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebFill"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get { return (ZedGraphWebFill)base.GetValue('f'); }
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

		public ZedGraphWebErrorBar() : base()
		{
			Register('s',typeof(ZedGraphWebSymbol));			
		}

		#region Symbol
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebSymbol"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebSymbol Symbol
		{
			get { return (ZedGraphWebSymbol)base.GetValue('s'); }
		}
		#endregion

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

		public ZedGraphWebHiLowBar() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
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
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebBorder"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get { return (ZedGraphWebBorder)base.GetValue('b'); }
		}
		#endregion

		#region Fill
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebFill"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get { return (ZedGraphWebFill)base.GetValue('f'); }
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
	
		public ZedGraphWebLine() : base()
		{
			Register('s',typeof(ZedGraphWebSymbol));
			Register('f',typeof(ZedGraphWebFill));
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

		#region Symbol
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebSymbol"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebSymbol Symbol
		{
			get { return (ZedGraphWebSymbol)GetValue('s'); }
		}
		#endregion

		#region Fill
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebFill"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get { return (ZedGraphWebFill)GetValue('f'); }
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

		public ZedGraphWebPie() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));			
		}	

		#region Properties
		[NotifyParentProperty(true)]
		public double Value
		{
			get 
			{ 
				object x = ViewState["Value"]; 
				return (null == x) ? PieSlice.Default.value : (double)x;
			}
			set { ViewState["Value"] = value; }
		}

		public	double Displacement
		{
			get 
			{ 
				object x = ViewState["Displacement"]; 
				return (null == x) ? PieSlice.Default.displacement : (double)x;
			}
			set { ViewState["Displacement"] = value; }
		}

		public	PieLabelType LabelType
		{
			get 
			{ 
				object x = ViewState["LabelType"]; 
				return (null == x) ? PieLabelType.None : (PieLabelType)x;
			}
			set { ViewState["LabelType"] = value; }
		}

/*
		public	PieType PieType
		{
			get 
			{ 
				object x = ViewState["PieType"]; 
				return (null == x) ? PieType.Pie2D : (PieType)x;
			}
			set { ViewState["PieType"] = value; }
		}
*/
	#endregion

	#region Border
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebBorder"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get { return (ZedGraphWebBorder)base.GetValue('b'); }
		}
		#endregion		
	}
	#endregion

	#region ZedGraphWebGraphItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebGraphItem : GenericItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "GraphItem";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebGraphItem() : base()
		{
		}	
		
		[NotifyParentProperty(true)]
		public string Label
		{
			get 
			{ 
				object x = ViewState["Label"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["Label"] = value; }
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

	#region ZedGraphWebGraphItemCollection
	/// <summary>
	/// Manages a collection of <see cref="ZedGraphWebGraphItem"/> objects that are 
	/// state management aware.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebGraphItemCollection : GenericCollection
	{	
		public override string ToString(){return String.Empty;}		

		public ZedGraphWebGraphItemCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[1];
			Schema[0].code = 'x';
			Schema[0].type = typeof(ZedGraphWebGraphItem);			
		}

		public void Add(ZedGraphWebGraphItem item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		[NotifyParentProperty(true)]
		public ZedGraphWebGraphItem this [int index]
		{
			get 
			{
				return (ZedGraphWebGraphItem)ListGet(index);
			}
			set
			{
				ListInsert(index,value);
			}
		}			
	}

	#endregion

	#region ZedGraphWebAxis
	/// <summary>
	/// Web control state management class for a <see cref="ZedGraph.Axis"/> object
	/// </summary>
	/// <author>Darren Martz</author>	
	public class ZedGraphWebAxis : GenericItem
	{
		/// <summary>
		/// Identifies axis by the title value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Axis: " + Title;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebAxis() : base()
		{
		}		

		#region Properties
		/// <summary>
		/// <seealso cref="ZedGraph.Axis.Title"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public string Title
		{
			get 
			{ 
				object x = ViewState["Title"]; 
				return (null == x) ? String.Empty : (string)x;
			}
			set { ViewState["Title"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.Axis.Color"/>
		/// </summary>
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

		/// <summary>
		/// <seealso cref="ZedGraph.Axis.GridColor"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public Color GridColor
		{
			get 
			{ 
				object x = ViewState["GridColor"]; 
				return (null == x) ? Color.Empty : (Color)x;
			}
			set { ViewState["GridColor"] = value; }
		}
		#endregion
		//TODO: complete
	}
	#endregion

	#region ZedGraphWebLegend
	/// <summary>
	/// Web control state management class for a <see cref="ZedGraph.Legend"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebLegend : GenericItem
	{
		/// <summary>
		/// Identifies legend instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Legend";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebLegend() : base()
		{
		}
		
		#region Properties
	
		/// <summary>
		/// <seealso cref="ZedGraph.Legend.IsVisible"/>
		/// </summary>
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

		/*
		/// <summary>
		/// <seealso cref="ZedGraph.Legend.Rect"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public RectangleF Rect
		{
			get 
			{ 
				object x = ViewState["Rect"]; 
				return (null == x) ? RectangleF.Empty : (RectangleF)x;
			}
			set { ViewState["Rect"] = value; }
		}
		*/

		/// <summary>
		/// <seealso cref="ZedGraph.Legend.Position"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public LegendPos Position
		{
			get 
			{ 
				object x = ViewState["Position"]; 
				return (null == x) ? LegendPos.Top : (LegendPos)x;
			}
			set { ViewState["Position"] = value; }
		}

		#endregion

		//TODO: complete
	}
	#endregion

	#region ZedGraphWebFontSpec
	/// <summary>
	/// Web control state management class for a <see cref="ZedGraph.FontSpec"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebFontSpec : GenericItem
	{
		/// <summary>
		/// Identifies fontspec instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "FontSpec";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebFontSpec() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
		}
		
		#region Properties

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.Angle"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public float Angle
		{
			get 
			{ 
				object x = ViewState["Angle"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Angle"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.Size"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public float Size
		{
			get 
			{ 
				object x = ViewState["Size"]; 
				return (null == x) ? 10 : (float)x;
			}
			set { ViewState["Size"] = value; }
		}
		
		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.Family"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public string Family
		{
			get 
			{ 
				object x = ViewState["Family"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["Family"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.FontColor"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public Color FontColor
		{
			get 
			{ 
				object x = ViewState["FontColor"]; 
				return (null == x) ? Color.Empty : (Color)x;
			}
			set { ViewState["FontColor"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.StringAlignment"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public StringAlignment StringAlignment
		{
			get 
			{ 
				object x = ViewState["StringAlignment"]; 
				return (null == x) ? FontSpec.Default.StringAlignment : (StringAlignment)x;
			}
			set { ViewState["StringAlignment"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.IsBold"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public bool IsBold
		{
			get 
			{ 
				object x = ViewState["IsBold"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsBold"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.IsItalic"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public bool IsItalic
		{
			get 
			{ 
				object x = ViewState["IsItalic"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsItalic"] = value; }
		}

		/// <summary>
		/// <seealso cref="ZedGraph.FontSpec.IsUnderline"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public bool IsUnderline
		{
			get 
			{ 
				object x = ViewState["IsUnderline"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsUnderline"] = value; }
		}

		#endregion

		#region Border
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebBorder"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder Border
		{
			get { return (ZedGraphWebBorder)GetValue('b'); }
		}
		#endregion

		#region Fill
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebFill"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill Fill
		{
			get { return (ZedGraphWebFill)GetValue('f'); }
		}
		#endregion		
	}
	#endregion
}
