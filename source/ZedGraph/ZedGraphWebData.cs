//============================================================================
//ZedGraphWebData Class
//Copyright (C) 2005  Darren Martz
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

//
// The following are PROXY classes to the real parts of ZedGraph. This was done
// because of the generic nature of ZedGraph support WebForms, WinForms, and 
// Console applications. The specific requirements for building the webcontrol are
// unique enough to conflict the generic requirements of ZedGraph, so we have
// proxy classes.
//

namespace ZedGraph
{
	#region ZedGraphWebCurveItem
	/// <summary>
	/// Base class for curve items in the web control
	/// <seealso cref="CurveItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	[DefaultProperty("Label")]
	public class ZedGraphWebCurveItem : GenericItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns>A string containing "CurveItem: label", where 'label' is the
		/// <see cref="CurveItem.Label"/> property of the <see cref="CurveItem"/>
		/// </returns>
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

		public void CopyTo( CurveItem item )
		{
			item.Label = this.Label;
			item.Color = this.Color;
			item.IsLegendLabelVisible = this.IsLegendLabelVisible;
			item.IsY2Axis = this.IsY2Axis;
			item.IsVisible = this.IsVisible;
		}

		#region Properties
		
		[Category("Data"),NotifyParentProperty(true),
		Description("Optional binding member name for populating this curve item with values")]
		public string DataMember
		{
			get 
			{ 
				object x = ViewState["DataMember"]; 
				return (null == x) ? String.Empty : (string)x;
			}
			set { ViewState["DataMember"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="CurveItem.Label"/>.
		/// </summary>
		/// <remarks> A text string that represents the <see cref="ZedGraph.Legend"/>
		/// entry for the the <see cref="CurveItem"/> object.
		/// </remarks>
		/// <seealso cref="CurveItem.Label"/>
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
		/// Proxy property that gets or sets the value of <see cref="CurveItem.Color"/>.
		/// </summary>
		/// <remarks> The <see cref="LineItem.Line"/>/<see cref="LineItem.Symbol"/>/<see cref="BarItem.Bar"/> 
		/// color (FillColor for the Bar).  This is a common access to
		/// <see cref="ZedGraph.Line.Color"/>, <see cref="ZedGraph.Border.Color"/>, and
		/// <see cref="ZedGraph.Fill.Color"/> properties for this curve
		/// </remarks>
		/// <seealso cref="CurveItem.Color"/>
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
		/// Proxy property that gets or sets the value of <see cref="CurveItem.IsVisible"/>.
		/// </summary>
		/// <remarks> Determines whether the <see cref="CurveItem"/> is visible on the graph.
		/// Note that this value turns the curve display on or off, but it does not
		/// affect the display of the legend entry.  To hide the legend entry, you
		/// have to set <see cref="IsLegendLabelVisible"/> to false.
		/// </remarks>
		/// <seealso cref="CurveItem.IsVisible"/>
		/// <seealso cref="CurveItem.IsLegendLabelVisible"/>
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
		/// Proxy property that gets or sets the value of <see cref="CurveItem.IsLegendLabelVisible"/>.
		/// </summary>
		/// <remarks> Determines whether the label for this <see cref="CurveItem"/> is visible in the legend.
		/// Note that this value turns the legend entry display on or off, but it does not
		/// affect the display of the curve on the graph.  To hide the curve, you
		/// have to set <see cref="IsVisible"/> to false.
		/// </remarks>
		/// <seealso cref="CurveItem.IsVisible"/>
		/// <seealso cref="CurveItem.IsLegendLabelVisible"/>
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
		/// Proxy property that gets or sets the value of <see cref="CurveItem.IsY2Axis"/>.
		/// </summary>
		/// <remarks> Determines which Y axis this <see cref="CurveItem"/>
		/// is assigned to.  The
		/// <see cref="ZedGraph.YAxis"/> is on the left side of the graph and the
		/// <see cref="ZedGraph.Y2Axis"/> is on the right side.  Assignment to an axis
		/// determines the scale that is used to draw the curve on the graph.
		/// </remarks>
		/// <seealso cref="CurveItem.IsY2Axis"/>
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
	/// Web control state management class for a <see cref="Border"/> object.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebBorder : GenericItem
	{
		/// <summary>
		/// Override ToString function
		/// </summary>
		/// <returns>Always returns "Border"</returns>
		public override string ToString()
		{
			return "Border";
		}

		public void CopyTo( Border item )
		{
			item.Color = this.Color;
			item.IsVisible = this.IsVisible;
			item.PenWidth = this.PenWidth;
			item.InflateFactor = this.InflateFactor;
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Border.Color"/>.
		/// </summary>
		/// <remarks> Determines the <see cref="System.Drawing.Color"/> of the <see cref="Pen"/> used to
		/// draw this Border.
		/// </remarks>
		[NotifyParentProperty(true)]
		public Color Color
		{
			get 
			{ 
				object x = ViewState["Color"]; 
				return (null == x) ? Border.Default.Color : (Color)x;
			}
			set { ViewState["Color"] = value; }
		} 

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Border.IsVisible"/>.
		/// </summary>
		/// <remarks> Determines whether or not the Border will be drawn.  true to draw the Border,
		/// false otherwise.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? Border.Default.IsVisible : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Border.PenWidth"/>.
		/// </summary>
		/// <remarks> Gets or sets the width, in points (1/72 inch), of the <see cref="Pen"/>
		/// used to draw this Border.
		/// </remarks>
		[NotifyParentProperty(true)]
		public float PenWidth
		{
			get 
			{ 
				object x = ViewState["PenWidth"]; 
				return (null == x) ? Border.Default.PenWidth : (float)x;
			}
			set { ViewState["PenWidth"] = value; }
		}

		[NotifyParentProperty(true)]
		public float InflateFactor
		{
			get 
			{ 
				object x = ViewState["InflateFactor"]; 
				return (null == x) ? Border.Default.InflateFactor : (float)x;
			}
			set { ViewState["InflateFactor"] = value; }
		}
	}

	#endregion

	#region ZedGraphWebFill
	/// <summary>
	/// Web control state management class for a <see cref="Fill"/> object.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebFill : GenericItem
	{
		/// <summary>
		/// Override ToString() function
		/// </summary>
		/// <returns>Always returns "Fill"</returns>
		public override string ToString()
		{
			return "Fill";
		}

		public void CopyTo( Fill item )
		{
			item.Color = this.Color;
			item.IsVisible = this.IsVisible;
			item.RangeMax = this.RangeMax;
			item.RangeMin = this.RangeMin;
			item.Type = this.Type;
			item.AlignH = this.AlignH;
			item.AlignV = this.AlignV;
			item.IsScaled = this.IsScaled;
			item.Brush = this.Brush;
		}
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.Color"/>.
		/// </summary>
		/// <remarks> The fill color.  This property is used as a single color to make a solid fill
		/// (<see cref="Type"/> is <see cref="FillType.Solid"/>), or it can be used in 
		/// combination with <see cref="System.Drawing.Color.White"/> to make a
		/// <see cref="LinearGradientBrush"/>
		/// when <see cref="Type"/> is <see cref="FillType.Brush"/> and <see cref="Brush"/>
		/// is null.
		/// </remarks>
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
		/// Proxy property that gets or sets the value of <see cref="Fill.IsVisible"/>.
		/// </summary>
		/// <remarks> This property determines the type of color fill. 
		/// Returns true if the <see cref="Type"/> property is either
		/// <see cref="FillType.Solid"/> or
		/// <see cref="FillType.Brush"/>.  If set to true, this property
		/// will automatically set the <see cref="Type"/> to
		/// <see cref="FillType.Brush"/>.  If set to false, this property
		/// will automatically set the <see cref="Type"/> to
		/// <see cref="FillType.None"/>.  In order to get a regular
		/// solid-color fill, you have to manually set <see cref="Type"/>
		/// to <see cref="FillType.Solid"/>.
		/// </remarks>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.RangeMax"/>.
		/// </summary>
		/// <remarks> The maximum user-scale value for the gradient-by-value determination.  This defines
		/// the user-scale value for the end of the gradient.
		/// </remarks>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="ZedGraph.Fill.IsGradientValueType"/>
		/// <value>A double value, in user scale unit</value>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.RangeMin"/>.
		/// </summary>
		/// <remarks> The minimum user-scale value for the gradient-by-value determination.  This defines
		/// the user-scale value for the start of the gradient.
		/// </remarks>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="ZedGraph.Fill.IsGradientValueType"/>
		/// <value>A double value, in user scale unit</value>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.Type"/>.
		/// </summary>
		/// <remarks> Determines the type of fill, which can be either solid
		/// color (<see cref="ZedGraph.FillType.Solid"/>) or a custom brush
		/// (<see cref="ZedGraph.FillType.Brush"/>).  See <see cref="Type"/> for
		/// more information.
		/// </remarks>
		/// <seealso cref="ZedGraph.Fill.Color"/>
		[NotifyParentProperty(true)]
		public FillType Type
		{
			get 
			{ 
				object x = ViewState["Type"]; 
				return (null == x) ? FillType.Solid : (FillType)x;
			}
			set { ViewState["Type"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.AlignH"/>.
		/// </summary>
		/// <remarks> Determines how the brush will be aligned with the filled object
		/// in the horizontal direction.  This value is a <see cref="ZedGraph.AlignH"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// </remarks>
		/// <seealso cref="AlignV"/>
		[NotifyParentProperty(true)]
		public AlignH AlignH
		{
			get 
			{ 
				object x = ViewState["AlignH"]; 
				return (null == x) ? Fill.Default.AlignH : (AlignH)x;
			}
			set { ViewState["AlignH"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.AlignV"/>.
		/// </summary>
		/// <remarks> Determines how the brush will be aligned with the filled object
		/// in the vertical direction.  This value is a <see cref="ZedGraph.AlignV"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// </remarks>
		/// <seealso cref="AlignH"/>
		[NotifyParentProperty(true)]
		public AlignV AlignV
		{
			get 
			{ 
				object x = ViewState["AlignV"]; 
				return (null == x) ? Fill.Default.AlignV : (AlignV)x;
			}
			set { ViewState["AlignV"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Fill.IsScaled"/>.
		/// </summary>
		/// <remarks> Determines if the brush will be scaled to the bounding box
		/// of the filled object.  If this value is false, then the brush will only be aligned
		/// with the filled object based on the <see cref="AlignH"/> and <see cref="AlignV"/>
		/// properties.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsScaled
		{
			get 
			{ 
				object x = ViewState["IsScaled"]; 
				return (null == x) ? Fill.Default.IsScaled : (bool)x;
			}
			set { ViewState["IsScaled"] = value; }
		}
		
		// not accessible via webcontrol properties
		public Brush Brush = null;			
	}

	#endregion

	#region ZedGraphWebSymbol
	/// <summary>
	/// Web control state management class for a <see cref="Symbol"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebSymbol : GenericItem
	{
		/// <summary>
		/// Override ToString() function
		/// </summary>
		/// <returns>Always returns "Symbol"</returns>
		public override string ToString()
		{
			return "Symbol";
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebSymbol() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
		}

		public void CopyTo( Symbol item )
		{
			item.IsVisible = this.IsVisible;
			item.Size = this.Size;
			item.Type = this.SymbolType;
			this.Border.CopyTo(item.Border);
			this.Fill.CopyTo(item.Fill);
		}

		#region Properties
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Symbol.IsVisible"/>.
		/// </summary>
		/// <remarks> Gets or sets a property that shows or hides the <see cref="Symbol"/>.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? Symbol.Default.IsVisible : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		} 
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Symbol.Size"/>.
		/// </summary>
		/// <remarks> Gets or sets the size of the <see cref="Symbol"/> in points (1/72nd inch).
		/// </remarks>
		[NotifyParentProperty(true)]
		public float Size
		{
			get 
			{ 
				object x = ViewState["Size"]; 
				return (null == x) ? Symbol.Default.Size : (float)x;
			}
			set { ViewState["Size"] = value; }
		} 
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Symbol.Type"/>.
		/// </summary>
		/// <remarks> Gets or sets the type (shape) of the <see cref="Symbol"/> using
		/// a <see cref="SymbolType"/> enumeration.
		/// </remarks>
		[NotifyParentProperty(true)]
		public SymbolType SymbolType
		{
			get 
			{ 
				object x = ViewState["SymbolType"]; 
				return (null == x) ? Symbol.Default.Type : (SymbolType)x;
			}
			set { ViewState["SymbolType"] = value; }
		} 
		#endregion	
	
		#region Border
		/// <summary>
		/// Proxy property that gets or sets the <see cref="ZedGraphWebBorder"/> object for
		/// this <see cref="Symbol"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Border"/>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebFill"/> object for
		/// this <see cref="Symbol"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill"/>
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

	#region ZedGraphWebBarItem
	/// <summary>
	/// Web control state management class for a <see cref="BarItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebBarItem : ZedGraphWebCurveItem
	{
		/// <summary>
		/// Identifies <see cref="BarItem"/> by the labels value
		/// </summary>
		/// <returns>A string containing "BarItem: label", where 'label' is the
		/// <see cref="CurveItem.Label"/> property of the <see cref="CurveItem"/>
		/// </returns>
		public override string ToString()
		{
			return "BarItem: " + Label;
		}	

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebBarItem() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
		}

		public void CopyTo( BarItem item )
		{
			base.CopyTo( item );
			this.Border.CopyTo( item.Bar.Border );
			this.Fill.CopyTo( item.Bar.Fill );
		}

		#region Border
		
		/// <summary>
		/// Proxy property that gets or sets the <see cref="ZedGraphWebBorder"/> object for
		/// this <see cref="BarItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Border"/>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebFill"/> object for
		/// this <see cref="BarItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill"/>
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

	#region ZedGraphWebErrorBarItem
	/// <summary>
	/// Web control state management class for a <see cref="ErrorBarItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebErrorBarItem : ZedGraphWebCurveItem
	{
		/// <summary>
		/// Identifies <see cref="ErrorBarItem"/> by the labels value
		/// </summary>
		/// <returns>A string containing "ErrorBarItem: label", where 'label' is the
		/// <see cref="CurveItem.Label"/> property of the <see cref="CurveItem"/>
		/// </returns>
		public override string ToString()
		{
			return "ErrorBarItem: " + Label;
		}	

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebErrorBarItem() : base()
		{
			Register('s',typeof(ZedGraphWebSymbol));			
		}

		public void CopyTo( ErrorBarItem item )
		{
			base.CopyTo(item);
		}

		#region Symbol
		
		/// <summary>
		/// Proxy property that gets or sets the <see cref="ZedGraphWebSymbol"/> object for
		/// this <see cref="ErrorBarItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Symbol"/>
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
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ErrorBarItem.BarBase"/>.
		/// </summary>
		/// <remarks> Determines which <see cref="Axis"/> is the independent axis
		/// for this <see cref="ErrorBarItem"/>.
		/// </remarks>
		/// <seealso cref="ErrorBarItem.BarBase"/>
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
 		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.ErrorBar.PenWidth"/>.
		/// </summary>
		/// <remarks> The pen width to be used for drawing error bars
		/// Units are points.  This property only controls the pen width for the
		/// vertical line.  The pen width for the symbol outline is
		/// controlled separately by the <see cref="Symbol"/> property.
		/// </remarks>
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
	public class ZedGraphWebHiLowBarItem : ZedGraphWebCurveItem
	{
		/// <summary>
		/// Identifies <see cref="HiLowBarItem"/> by the labels value
		/// </summary>
		/// <returns>A string containing "HiLowBarItem: label", where 'label' is the
		/// <see cref="CurveItem.Label"/> property of the <see cref="CurveItem"/>
		/// </returns>
		public override string ToString()
		{
			return "HiLowBarItem: " + Label;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebHiLowBarItem() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
		}

		public void CopyTo( HiLowBarItem item )
		{
			base.CopyTo(item);
		}

		#region Properties
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="HiLowBarItem.BarBase"/>.
		/// </summary>
		/// <remarks> Determines which <see cref="Axis"/> is the independent axis
		/// for this <see cref="HiLowBarItem"/>.
		/// </remarks>
		/// <seealso cref="HiLowBarItem.BarBase"/>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.HiLowBar.IsMaximumWidth"/>.
		/// </summary>
		/// <remarks> Determines whether the bar width will be based on
		/// the <see cref="Size"/> value, or it will be based on available
		/// space similar to <see cref="BarItem"/> objects.
		/// </remarks>
		/// <value>If true, then the value
		/// of <see cref="Size"/> is ignored.  If this value is true, then
		/// <see cref="GraphPane.MinClusterGap"/> will be used to determine the total space between each bar.
		/// </value>
		/// <seealso cref="HiLowBarItem.BarBase"/>
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
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="HiLowBar.Size"/>.
		/// </summary>
		/// <remarks>The size of the bars can be set by this value, which
		/// is then scaled according to the scaleFactor (see
		/// <see cref="PaneBase.CalcScaleFactor"/>).  Alternatively,
		/// if <see cref="IsMaximumWidth"/> is true, the bar width will
		/// be set according to the maximum available cluster width less
		/// the cluster gap (see <see cref="GraphPane.GetClusterWidth"/>
		/// and <see cref="GraphPane.MinClusterGap"/>).  That is, if
		/// <see cref="IsMaximumWidth"/> is true, then the value of
		/// <see cref="Size"/> will be ignored.
		/// </remarks>
		/// <value>Size in points (1/72 inch)</value>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebBorder"/> object for
		/// this <see cref="HiLowBarItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Border"/>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebFill"/> object for
		/// this <see cref="HiLowBarItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill"/>
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
	
	#region ZedGraphWebLineItem
	/// <summary>
	/// Web control state management class for a <see cref="LineItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebLineItem : ZedGraphWebCurveItem
	{
		/// <summary>
		/// Identifies <see cref="LineItem"/> by the labels value
		/// </summary>
		/// <returns>A string containing "LineItem: label", where 'label' is the
		/// <see cref="CurveItem.Label"/> property of the <see cref="CurveItem"/>
		/// </returns>
		public override string ToString()
		{
			return "LineItem: " + Label;
		}
	
		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebLineItem() : base()
		{
			Register('s',typeof(ZedGraphWebSymbol));
			Register('f',typeof(ZedGraphWebFill));
		}

		public void CopyTo( LineItem item )
		{
			base.CopyTo(item);
			this.Symbol.CopyTo(item.Symbol);
			this.Fill.CopyTo(item.Line.Fill);
			item.Line.IsSmooth = this.IsSmooth;
			item.Line.Width = this.Width;
			item.Line.SmoothTension = this.Width;
			item.Line.Style = this.Style;
			item.Line.StepType = this.StepType;
		}
	
		#region Properties
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Line.IsSmooth"/>.
		/// </summary>
		/// <remarks> Gets or sets a property that determines if this <see cref="Line"/>
		/// will be drawn smooth.  The "smoothness" is controlled by
		/// the <see cref="SmoothTension"/> property.
		/// </remarks>
		/// <seealso cref="ZedGraph.Line.IsSmooth"/>
		[NotifyParentProperty(true)]
		public bool IsSmooth
		{
			get 
			{ 
				object x = ViewState["IsSmooth"]; 
				return (null == x) ? Line.Default.IsSmooth : (bool)x;
			}
			set { ViewState["IsSmooth"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Line.Width"/>.
		/// </summary>
		/// <remarks> The pen width used to draw the <see cref="Line"/>, in points (1/72 inch)
		/// </remarks>
		/// <seealso cref="ZedGraph.Line.Width"/>
		[NotifyParentProperty(true)]
		public float Width
		{
			get 
			{ 
				object x = ViewState["Width"]; 
				return (null == x) ? Line.Default.Width : (float)x;
			}
			set { ViewState["Width"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Line.SmoothTension"/>.
		/// </summary>
		/// <remarks> Gets or sets a property that determines the smoothing tension
		/// for this <see cref="Line"/>.  This property is only used if
		/// <see cref="IsSmooth"/> is true.  A tension value 0.0 will just
		/// draw ordinary line segments like an unsmoothed line.  A tension
		/// value of 1.0 will be smooth.  Values greater than 1.0 will generally
		/// give odd results.
		/// </remarks>
		/// <value>A floating point value indicating the level of smoothing.
		/// 0.0F for no smoothing, 1.0F for lots of smoothing, >1.0 for odd
		/// smoothing.</value>
		/// <seealso cref="ZedGraph.Line.IsSmooth"/>
		[NotifyParentProperty(true)]
		public float SmoothTension
		{
			get 
			{ 
				object x = ViewState["SmoothTension"]; 
				return (null == x) ? Line.Default.SmoothTension : (float)x;
			}
			set { ViewState["SmoothTension"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Line.Style"/>.
		/// </summary>
		/// <remarks> The style of the <see cref="Line"/>, defined as a <see cref="DashStyle"/> enum.
		/// This allows the line to be solid, dashed, or dotted.
		/// </remarks>
		/// <seealso cref="ZedGraph.Line.Style"/>
		[NotifyParentProperty(true)]
		public DashStyle Style
		{
			get 
			{ 
				object x = ViewState["DashStyle"]; 
				return (null == x) ? Line.Default.Style : (DashStyle)x;
			}
			set { ViewState["DashStyle"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Line.StepType"/>.
		/// </summary>
		/// <remarks> Determines if the <see cref="LineItem"/> will be drawn by directly connecting the
		/// points from the <see cref="CurveItem.Points"/> data collection,
		/// or if the curve will be a "stair-step" in which the points are
		/// connected by a series of horizontal and vertical lines that
		/// represent discrete, constant values.  Note that the values can
		/// be forward oriented <c>ForwardStep</c> (<see cref="ZedGraph.StepType"/>) or
		/// rearward oriented <c>RearwardStep</c>.
		/// That is, the points are defined at the beginning or end
		/// of the constant value for which they apply, respectively.
		/// The <see cref="StepType"/> property is ignored for lines
		/// that have <see cref="IsSmooth"/> set to true.
		/// </remarks>
		/// <seealso cref="ZedGraph.Line.StepType"/>
		[NotifyParentProperty(true)]
		public StepType StepType
		{
			get 
			{ 
				object x = ViewState["StepType"]; 
				return (null == x) ? Line.Default.StepType : (StepType)x;
			}
			set { ViewState["StepType"] = value; }
		}
		#endregion

		#region Symbol
		/// <summary>
		/// Proxy property that gets or sets the <see cref="ZedGraphWebSymbol"/> object for
		/// this <see cref="LineItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Symbol"/>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebFill"/> object for
		/// this <see cref="LineItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill"/>
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

	#region ZedGraphWebPieItem
	/// <summary>
	/// Web control state management class for a <see cref="PieItem"/> class
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebPieItem : ZedGraphWebCurveItem
	{
		/// <summary>
		/// Identifies <see cref="PieItem"/> by the labels value
		/// </summary>
		/// <returns>A string containing "PieItem: label", where 'label' is the
		/// <see cref="CurveItem.Label"/> property of the <see cref="CurveItem"/>
		/// </returns>
		public override string ToString()
		{
			return "PieItem: " + Label;
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebPieItem() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));			
		}	

		public void CopyTo( PieItem item )
		{
			base.CopyTo(item);
		}

		#region Properties
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.Value"/>.
		/// </summary>
		/// <remarks> Gets or sets the value of this <see cref="PieItem"/>.  
		/// Minimum value is 0. 
		/// </remarks>
		[NotifyParentProperty(true)]
		public double Value
		{
			get 
			{ 
				object x = ViewState["Value"]; 
				return (null == x) ? PointPair.Missing : (double)x;
			}
			set { ViewState["Value"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.Displacement"/>.
		/// </summary>
		/// <remarks> Gets or sets the a value which determines the amount, if any, of this <see cref="PieItem"/>  
		/// displacement.
		/// </remarks>
		public	double Displacement
		{
			get 
			{ 
				object x = ViewState["Displacement"]; 
				return (null == x) ? PieItem.Default.Displacement : (double)x;
			}
			set { ViewState["Displacement"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.LabelType"/>.
		/// </summary>
		/// <remarks> Gets or sets the <see cref="PieLabelType"/> to be used in displaying 
		/// <see cref="PieItem"/> labels.
		/// </remarks>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebBorder"/> object for
		/// this <see cref="PieItem"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Border"/>
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
		/// Override the ToString() method.
		/// </summary>
		/// <returns>Always returns the string "GraphItem".</returns>
		public override string ToString()
		{
			return "GraphItem";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebGraphItem() : base()
		{
			Register('l',typeof(ZedGraphWebLocation));
		}
	
		public void CopyTo(GraphItem item)
		{
			item.ZOrder = this.ZOrder;
			item.IsVisible = this.IsVisible;
			this.Location.CopyTo(item.Location);
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
		
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebLocation Location
		{
			get { return (ZedGraphWebLocation)base.GetValue('l'); }
		}

		public ZOrder ZOrder
		{
			get 
			{ 
				object x = ViewState["ZOrder"]; 
				return (null == x) ? ZOrder.A_InFront : (ZOrder)x;
			}
			set { ViewState["ZOrder"] = value; }
		} 
		#endregion
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
		/// <summary>
		/// Override the ToString() method.
		/// </summary>
		/// <returns>Always returns String.Empty</returns>
		public override string ToString(){return String.Empty;}		

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebCurveCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[5];
			Schema[0].code = 'b';
			Schema[0].type = typeof(ZedGraphWebBarItem);
			Schema[1].code = 'l';
			Schema[1].type = typeof(ZedGraphWebLineItem);
			Schema[2].code = 'e';
			Schema[2].type = typeof(ZedGraphWebErrorBarItem);
			Schema[3].code = 'h';
			Schema[3].type = typeof(ZedGraphWebHiLowBarItem);
			Schema[4].code = 'p';
			Schema[4].type = typeof(ZedGraphWebPieItem);
		}

		/// <summary>
		/// Add a <see cref="ZedGraphWebCurveItem"/> to this collection.
		/// </summary>
		/// <param name="item">The <see cref="ZedGraphWebCurveItem"/> to be added.</param>
		/// <seealso cref="ZedGraph.CurveItem"/>
		public void Add(ZedGraphWebCurveItem item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		/// <summary>
		/// Indexer to access the specified <see cref="ZedGraphWebCurveItem"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="ZedGraphWebCurveItem"/> object to be accessed.</param>
		/// <value>A <see cref="ZedGraphWebCurveItem"/> object reference.</value>
		/// <seealso cref="ZedGraph.CurveItem"/>
		[NotifyParentProperty(true)]
		public ZedGraphWebCurveItem this [int index]
		{
			get { return (ZedGraphWebCurveItem)ListGet(index); }
			set { ListInsert(index, value); }
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
		/// <summary>
		/// Override the ToString() method.
		/// </summary>
		/// <returns>Always returns String.Empty</returns>
		public override string ToString(){return String.Empty;}		

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebGraphItemCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[5];
			Schema[0].code = 't';
			Schema[0].type = typeof(ZedGraphWebTextItem);			
			Schema[1].code = 'a';
			Schema[1].type = typeof(ZedGraphWebArrowItem);			
			Schema[2].code = 'i';
			Schema[2].type = typeof(ZedGraphWebImageItem);			
			Schema[3].code = 'b';
			Schema[3].type = typeof(ZedGraphWebBoxItem);			
			Schema[4].code = 'e';
			Schema[4].type = typeof(ZedGraphWebEllipseItem);			
		}

		/// <summary>
		/// Add a <see cref="ZedGraphWebGraphItem"/> to this collection.
		/// </summary>
		/// <param name="item">The <see cref="ZedGraphWebGraphItem"/> to be added.</param>
		/// <seealso cref="ZedGraph.GraphItem"/>
		public void Add(ZedGraphWebGraphItem item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		/// <summary>
		/// Indexer to access the specified <see cref="ZedGraphWebGraphItem"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="ZedGraphWebGraphItem"/> object to be accessed.</param>
		/// <value>A <see cref="ZedGraphWebGraphItem"/> object reference.</value>
		/// <seealso cref="ZedGraph.GraphItem"/>
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
		/// Identifies <see cref="Axis"/> by the <see cref="Axis.Title"/> value
		/// </summary>
		/// <returns>A string containing "Axis: title", where 'title' is the
		/// <see cref="Axis.Title"/> property of the <see cref="Axis"/>
		/// </returns>
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
	
		public void CopyTo(Axis item)
		{
		}

		#region Properties
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Title"/>.
		/// </summary>
		/// <remarks> The title normally shows the basis and dimensions of
		/// the scale range, such as "Time (Years)".  The title is only shown if the
		/// <see cref="Axis.IsShowTitle"/> property is set to true.  If the Title text is empty,
		/// then no title is shown, and no space is "reserved" for the title on the graph.
		/// </remarks>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Color"/>.
		/// </summary>
		/// <remarks> This affects only the tic
		/// marks, since the <see cref="Axis.TitleFontSpec"/> and
		/// <see cref="Axis.ScaleFontSpec"/> both have their own color specification.
		/// </remarks>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Color"/>.
		/// </summary>
		/// <remarks> This affects only the grid
		/// lines, since the <see cref="Axis.TitleFontSpec"/> and
		/// <see cref="Axis.ScaleFontSpec"/> both have their own color specification.
		/// </remarks>
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
		/// <returns>Always returns the string "Legend".</returns>
		public override string ToString()
		{
			return "Legend";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebLegend() : base()
		{
			Register('r',typeof(ZedGraphWebRect));
			Register('f',typeof(ZedGraphWebFontSpec));
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
			Register('l',typeof(ZedGraphWebLocation));
		}

		public void CopyTo( Legend item )
		{
			item.IsVisible = this.IsVisible;
			item.Position = this.Position;
			item.IsHStack = this.IsHStack;
			this.Rect.CopyTo(item.Rect);
			this.FontSpec.CopyTo(item.FontSpec);
			this.Border.CopyTo(item.Border);
			this.Fill.CopyTo(item.Fill);
			this.Location.CopyTo(item.Location);
		}
		
		#region Properties

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebLocation Location
		{
			get { return (ZedGraphWebLocation)base.GetValue('l'); }
		}

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec FontSpec
		{
			get { return (ZedGraphWebFontSpec)base.GetValue('f'); }
		}

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

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebRect Rect
		{
			get { return (ZedGraphWebRect)base.GetValue('r'); }
		}
	
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Legend.IsVisible"/>.
		/// </summary>
		/// <remarks> This property shows or hides the <see cref="Legend"/> entirely.
		/// </remarks>
		/// <value> true to show the <see cref="Legend"/>, false to hide it </value>
		[NotifyParentProperty(true)]
		public bool IsVisible
		{
			get 
			{ 
				object x = ViewState["IsVisible"]; 
				return (null == x) ? Legend.Default.IsVisible : (bool)x;
			}
			set { ViewState["IsVisible"] = value; }
		}
		
		[NotifyParentProperty(true)]
		public bool IsHStack
		{
			get 
			{ 
				object x = ViewState["IsHStack"]; 
				return (null == x) ? Legend.Default.IsHStack : (bool)x;
			}
			set { ViewState["IsHStack"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Legend.Position"/>.
		/// </summary>
		/// <remarks> Sets or gets the location of the <see cref="Legend"/> on the
		/// <see cref="GraphPane"/> using the <see cref="LegendPos"/> enum type
		/// </remarks>
		[NotifyParentProperty(true)]
		public LegendPos Position
		{
			get 
			{ 
				object x = ViewState["Position"]; 
				return (null == x) ? Legend.Default.Position : (LegendPos)x;
			}
			set { ViewState["Position"] = value; }
		}

		#endregion		
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
		/// <returns>Always returns the string "FontSpec".</returns>
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

		public void CopyTo( FontSpec item )
		{
			this.Border.CopyTo(item.Border);
			this.Fill.CopyTo(item.Fill);
			item.Angle = this.Angle;
			item.Size = this.Size;
			item.Family = this.Family;
			item.FontColor = this.FontColor;
			item.StringAlignment = this.StringAlignment;
			item.IsBold = this.IsBold;
			item.IsItalic = this.IsItalic;
			item.IsUnderline = this.IsUnderline;
		}
		
		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.Angle"/>.
		/// </summary>
		/// <remarks> The angle at which this <see cref="FontSpec"/> object is drawn.
		/// </remarks>
		/// <value>The angle of the font, measured in anti-clockwise degrees from
		/// horizontal.  Negative values are permitted.</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.Size"/>.
		/// </summary>
		/// <remarks> The size of the font for this <see cref="FontSpec"/> object.
		/// </remarks>
		/// <value>The size of the font, measured in points (1/72 inch).</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.Family"/>.
		/// </summary>
		/// <remarks> The font family name for this <see cref="FontSpec"/>.
		/// </remarks>
		/// <value>A text string with the font family name, e.g., "Arial"</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.FontColor"/>.
		/// </summary>
		/// <remarks> The color of the font characters for this <see cref="FontSpec"/>.
		/// Note that the border and background
		/// colors are set using the <see cref="ZedGraph.Border.Color"/> and
		/// <see cref="ZedGraph.Fill.Color"/> properties, respectively.
		/// </remarks>
		/// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.StringAlignment"/>.
		/// </summary>
		/// <remarks> Determines the alignment with which this
		/// <see cref="FontSpec"/> object is drawn.  This alignment really only
		/// affects multi-line strings.
		/// </remarks>
		/// <value>A <see cref="StringAlignment"/> enumeration.</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.IsBold"/>.
		/// </summary>
		/// <remarks> Determines whether this <see cref="FontSpec"/> is
		/// drawn with bold typeface.
		/// </remarks>
		/// <value>A boolean value, true for bold, false for normal</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.IsItalic"/>.
		/// </summary>
		/// <remarks> Determines whether this <see cref="FontSpec"/> is
		/// drawn with an italic typeface.
		/// </remarks>
		/// <value>A boolean value, true for italic, false for normal</value>
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.FontSpec.IsUnderline"/>.
		/// </summary>
		/// <remarks> Determines whether this <see cref="FontSpec"/> is
		/// drawn with an underline typeface.
		/// </remarks>
		/// <value>A boolean value, true for underline, false for normal</value>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebBorder"/> object for
		/// this <see cref="FontSpec"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Border"/>
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
		/// Proxy property that gets or sets the <see cref="ZedGraphWebFill"/> object for
		/// this <see cref="FontSpec"/>.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill"/>
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

	#region ZedGraphWebRect
	/// <summary>
	/// Rectangle class for a <see cref="ZedGraph.FontSpec"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebRect : GenericItem
	{
		/// <summary>
		/// Identifies fontspec instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{						
			return string.Format("Rect({0},{1},{2},{3})",
				X,Y,Height,Width);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebRect() : base()
		{				
		}

		public void CopyTo( RectangleF item )
		{
			item.X = this.X;
			item.Y = this.Y;
			item.Width = this.Width;
			item.Height = this.Height;
		}
		
	#region Properties

		[NotifyParentProperty(true)]
		public float Height
		{
			get 
			{ 
				object x = ViewState["Height"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Height"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Width
		{
			get 
			{ 
				object x = ViewState["Width"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Width"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Y
		{
			get 
			{ 
				object x = ViewState["Y"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Y"] = value; }
		}

		[NotifyParentProperty(true)]
		public float X
		{
			get 
			{ 
				object x = ViewState["X"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["X"] = value; }
		}
		#endregion
	}
	#endregion

	#region ZedGraphWebSize
	/// <summary>
	/// Size class for a <see cref="ZedGraph.FontSpec"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebSize : GenericItem
	{
		/// <summary>
		/// Identifies fontspec instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{						
			return string.Format("Size({0},{1})",
				Height,Width);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebSize() : base()
		{				
		}

		public void CopyTo( SizeF item )
		{
			item.Width = this.Width;
			item.Height = this.Height;
		}
		
		#region Properties

		[NotifyParentProperty(true)]
		public float Height
		{
			get 
			{ 
				object x = ViewState["Height"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Height"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Width
		{
			get 
			{ 
				object x = ViewState["Width"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Width"] = value; }
		}
		
		#endregion
	}
	#endregion

	#region ZedGraphWebPoint
	/// <summary>
	/// Point class for a <see cref="ZedGraph.FontSpec"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebPoint : GenericItem
	{
		/// <summary>
		/// Identifies fontspec instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{						
			return string.Format("Point({0},{1})",
				X,Y);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebPoint() : base()
		{
		}

		public void CopyTo( PointF item )
		{
			item.X = this.X;
			item.Y = this.Y;			
		}
		
		#region Properties

		[NotifyParentProperty(true)]
		public float X
		{
			get 
			{ 
				object x = ViewState["X"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["X"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Y
		{
			get 
			{ 
				object x = ViewState["Y"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Y"] = value; }
		}
		
		#endregion
	}
	#endregion

	#region ZedGraphWebTextItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebTextItem : ZedGraphWebGraphItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "TextItem: " + this.Text;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebTextItem() : base()
		{
			Register('a',typeof(ZedGraphWebSize));
			Register('f',typeof(ZedGraphWebFontSpec));
		}
	
		public void CopyTo( TextItem item )
		{
			base.CopyTo(item);
		}
		
		[NotifyParentProperty(true)]
		public string Text
		{
			get 
			{ 
				object x = ViewState["Text"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["Text"] = value; }
		} 

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.Attribute)
		]
		public ZedGraphWebSize LayoutArea
		{
			get { return (ZedGraphWebSize)GetValue('a'); }			
		} 

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec FontSpec
		{
			get { return (ZedGraphWebFontSpec)GetValue('f'); }			
		} 
	}
	#endregion

	#region ZedGraphWebArrowItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebArrowItem : ZedGraphWebGraphItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "ArrowItem";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebArrowItem() : base()
		{
		}	
		
		public void CopyTo( ArrowItem item )
		{
			base.CopyTo(item);
		}
	}
	#endregion

	#region ZedGraphWebImageItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebImageItem : ZedGraphWebGraphItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "ImageItem";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebImageItem() : base()
		{
		}
		
		public void CopyTo( ArrowItem item )
		{
			base.CopyTo(item);
		}
	}
	#endregion

	#region ZedGraphWebBoxItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebBoxItem : ZedGraphWebGraphItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "BoxItem";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebBoxItem() : base()
		{
		}			
	
		public void CopyTo( BoxItem item )
		{
			base.CopyTo(item);
		}
	}
	#endregion

	#region ZedGraphWebEllipseItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebEllipseItem : ZedGraphWebGraphItem
	{
		/// <summary>
		/// Identifies curve item by the labels value
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "EllipseItem";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebEllipseItem() : base()
		{
		}
		
		public void CopyTo( BoxItem item )
		{
			base.CopyTo(item);
		}
	}
	#endregion

	#region ZedGraphWebLocation
	/// <summary>
	/// Location class for a <see cref="ZedGraph.FontSpec"/> object
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebLocation : GenericItem
	{
		/// <summary>
		/// Identifies location instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{						
			return "Location";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebLocation() : base()
		{	
			Register('r',typeof(ZedGraphWebRect));
			Register('t',typeof(ZedGraphWebPoint));
			Register('b',typeof(ZedGraphWebPoint));
		}

		public void CopyTo( Location item )
		{
			item.X = this.X;
			item.Y = this.Y;
			item.X1 = this.X1;
			item.Y1 = this.Y1;			
			item.Width = this.Width;
			item.Height = this.Height;
			item.AlignH = this.AlignH;
			item.AlignV = this.AlignV;
			item.CoordinateFrame = this.CoordinateFrame;
			this.Rect.CopyTo(item.Rect);
			this.Topleft.CopyTo(item.TopLeft);
			this.BottomRight.CopyTo(item.BottomRight);
		}

		#region Object Properties

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebRect Rect
		{
			get { return (ZedGraphWebRect)GetValue('r'); }
		}

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebPoint Topleft
		{
			get { return (ZedGraphWebPoint)GetValue('t'); }
		}

		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebPoint BottomRight
		{
			get { return (ZedGraphWebPoint)GetValue('b'); }
		}

		#endregion
		
		#region Properties

		[NotifyParentProperty(true)]
		public float Height
		{
			get 
			{ 
				object x = ViewState["Height"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Height"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Width
		{
			get 
			{ 
				object x = ViewState["Width"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Width"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Y
		{
			get 
			{ 
				object x = ViewState["Y"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Y"] = value; }
		}

		[NotifyParentProperty(true)]
		public float X
		{
			get 
			{ 
				object x = ViewState["X"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["X"] = value; }
		}

		[NotifyParentProperty(true)]
		public float Y1
		{
			get 
			{ 
				object x = ViewState["Y1"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Y1"] = value; }
		}

		[NotifyParentProperty(true)]
		public float X1
		{
			get 
			{ 
				object x = ViewState["X1"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["X1"] = value; }
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
		public CoordType CoordinateFrame
		{
			get 
			{ 
				object x = ViewState["CoordinateFrame"]; 
				return (null == x) ? CoordType.AxisFraction : (CoordType)x;
			}
			set { ViewState["CoordinateFrame"] = value; }
		}

		#endregion
	}
	#endregion
}
