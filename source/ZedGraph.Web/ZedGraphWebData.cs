//============================================================================
//ZedGraphWebData Class
//Copyright © 2005  Darren Martz
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
using System.Drawing;
using System.Drawing.Drawing2D;

//
// The following are PROXY classes to the real parts of ZedGraph. This was done
// because of the generic nature of ZedGraph support WebForms, WinForms, and 
// Console applications. The specific requirements for building the webcontrol are
// unique enough to conflict the generic requirements of ZedGraph, so we have
// proxy classes.
//

namespace ZedGraph.Web
{
	#region ZedGraphWebCurveItem
	/// <summary>
	/// Base class for curve items in the web control
	/// <seealso cref="CurveItem"/>
	/// </summary>
	/// <author>Darren Martz</author>
	[DefaultProperty("Label")]
	public abstract class ZedGraphWebCurveItem : GenericItem
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
			Register('0',typeof(ZedGraphWebPointPairCollection));
		}

		/// <summary>
		/// Constructor that accepts a default label value
		/// </summary>
		/// <param name="label">Curve item label</param>
		public ZedGraphWebCurveItem(string label) : base()
		{
			Label = label;
		}

		#region Web specific Methods
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebCurveItem"/> to the specified
		/// <see cref="CurveItem"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="CurveItem"/> object</param>
		public void CopyTo( CurveItem item )
		{
			item.Label.Text = this.Label;
			item.Color = this.Color;
			item.Label.IsVisible = this.IsLegendLabelVisible;
			item.IsY2Axis = this.IsY2Axis;
			item.IsVisible = this.IsVisible;
		}

		/// <summary>
		/// Creates a new CurveItem using the PointPairList and add it the the given pane.
		/// </summary>
		/// <param name="pane">the GraphPane object to which to add the new curve</param>
		/// <param name="points">a PointPairList collection defining the points for this curve</param>
		/// <returns>the newly created CurveItem added to the given GraphPane</returns>
		/// <remarks>This method must be overriden by childs</remarks>
		public abstract CurveItem CreateInPane( GraphPane pane, PointPairList points );
		#endregion

		#region Web Specific Properties

		/// <summary>
		/// The object reference that points to a data source from which to bind curve data.
		/// </summary>
		[Bindable(true),Category("Data"),NotifyParentProperty(true)]
		[Description("Overrides the main DataSource. This data source contains the data for this curve.")]
		public object DataSource
		{
			get 
			{ 
				object x = ViewState["DataSource"]; 
				return (null == x) ? null : (object)x;
			}
			set { ViewState["DataSource"] = value; }
		}

		/// <summary>
		/// The <see cref="String"/> name of the data member that contains the data to be
		/// bound to this <see cref="ZedGraph.CurveItem"/>.
		/// </summary>
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
		[Category("Curve Details")]
		[NotifyParentProperty(true)]
		[Description("A text string that represents the Legend entry for this CurveItem object")]
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
		/// <seealso cref="CurveItem.IsVisible">CurveItem.IsVisible</seealso>
		/// <seealso cref="ZedGraph.Label.IsVisible">Label.IsVisible</seealso>
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
		/// Proxy property that gets or sets the value of
		/// <seealso cref="ZedGraph.Label.IsVisible">Label.IsVisible</seealso>.
		/// </summary>
		/// <remarks> Determines whether the label for this <see cref="CurveItem"/> is
		/// visible in the legend.
		/// Note that this value turns the legend entry display on or off, but it does not
		/// affect the display of the curve on the graph.  To hide the curve, you
		/// have to set <see cref="IsVisible"/> to false.
		/// </remarks>
		/// <seealso cref="CurveItem.IsVisible"/>
		/// <seealso cref="ZedGraph.Label.IsVisible">Label.IsVisible</seealso>
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

		/// <summary>
		/// Proxy property that gets the value of the <see cref="CurveItem.Points"/>.
		/// </summary>
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebPointPairCollection Points
		{			
			get {  return (ZedGraphWebPointPairCollection)GetValue('0');} 
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
			string text;
			if( !this.IsVisible )
				text = "none";
			else
				text = Color.Name;
			return text;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebBorder"/> to the specified
		/// <see cref="ZedGraph.Border"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.Border"/> object</param>
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
				return (null == x) ? Border.Default.IsVisible : (bool) x;
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Border.InflateFactor"/>.
		/// </summary>
		/// <remarks> Gets or sets the amount of inflation to be done on the rectangle
		/// before rendering.
		/// </remarks>
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

	/// <summary>
	/// Todo: idem ZedGraphWebFill2
	/// </summary>
	public class ZedGraphWebBorder2 : ZedGraphWebBorder
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebBorder2() : base()
		{
			this.IsVisible = true;
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
			string text = this.Type.ToString();
			if( this.Type != ZedGraph.FillType.None )
				text += " (" + this.Color.Name + ")";
			return text;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebFill"/> to the specified
		/// <see cref="ZedGraph.Fill"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.Fill"/> object</param>
		public void CopyTo( Fill item )
		{
			item.Color = System.Drawing.Color.FromArgb( (int)Math.Floor(this.ColorOpacity*2.55+.5), this.Color );
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
		/// Opacity of <see cref="Color"/>, range from 0 to 100.
		/// 100 is opaque, 0 is invisible.
		/// </summary>
		/// <remarks>
		/// To be replaced by a best color designer which enables the selection of a transparency level.
		/// </remarks>
		[NotifyParentProperty(true)]
		[Description("Color Opacity between 0 and 100. 100 is opaque. 0 is invisible.")]
		public float ColorOpacity
		{
			get 
			{ 
				object x = ViewState["ColorOpacity"];
				return (null == x) ? 100 : (float)x;
			}
			set { ViewState["ColorOpacity"] = Math.Max(0,Math.Min(100,(float)value)); }
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
		
		/// <summary>
		/// The <see cref="Brush"/> object associated with this <see cref="Fill"/> object.
		/// Not accessible via webcontrol properties.
		/// </summary>
		[Browsable(false)]
		public Brush Brush = null;			
	}


	/// <summary>
	/// TODO: change the vsassist system so different default constructors or initializers can be called,
	/// or to be able to know if a the object is already initialized or not (new).
	/// </summary>
	public class ZedGraphWebFill2 : ZedGraphWebFill
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebFill2()
		{
			this.Color = Color.White;
		}
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

			this.Fill.Color = ZedGraph.Symbol.Default.FillColor;
			this.Fill.Brush = ZedGraph.Symbol.Default.FillBrush;
			this.Fill.Type  = ZedGraph.Symbol.Default.FillType;

			this.Border.IsVisible = ZedGraph.Symbol.Default.IsBorderVisible;
			this.Border.Color = ZedGraph.Symbol.Default.BorderColor;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebSymbol"/> to the specified
		/// <see cref="Symbol"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="Symbol"/> object</param>
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

			this.Border.PenWidth = ZedGraph.Bar.Default.BorderWidth;
			this.Border.Color = ZedGraph.Bar.Default.BorderColor;
			this.Fill.Color = ZedGraph.Bar.Default.FillColor;
			this.Fill.Type = ZedGraph.Bar.Default.FillType;
			this.Fill.Brush = ZedGraph.Bar.Default.FillBrush;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebBarItem"/> to the specified
		/// <see cref="ZedGraph.BarItem"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.BarItem"/> object</param>
		public void CopyTo( BarItem item )
		{
			base.CopyTo( item );
			this.Border.CopyTo( item.Bar.Border );
			this.Fill.CopyTo( item.Bar.Fill );
		}

		/// <summary>
		/// Creates a new CurveItem using the PointPairList and add it the the given pane.
		/// </summary>
		/// <param name="pane">the GraphPane object to which to add the new curve</param>
		/// <param name="points">a PointPairList collection defining the points for this curve</param>
		/// <returns>the newly created CurveItem added to the given GraphPane</returns>
		/// <remarks>This method must be overriden by childs</remarks>
		public override CurveItem CreateInPane( GraphPane pane, PointPairList points )
		{
			BarItem x = pane.AddBar(this.Label,points,this.Color);
			this.CopyTo(x);
			return x;
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
			
			this.Symbol.SymbolType = ZedGraph.ErrorBar.Default.Type;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebErrorBarItem"/> to the specified
		/// <see cref="ZedGraph.ErrorBarItem"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.ErrorBarItem"/> object</param>
		public void CopyTo( ErrorBarItem item )
		{
			base.CopyTo(item);
			this.Symbol.CopyTo(item.Bar.Symbol);
			//item.BarBase = this.BarBase;
			item.Bar.PenWidth = this.PenWidth;
		}

		/// <summary>
		/// Creates a new CurveItem using the PointPairList and add it the the given pane.
		/// </summary>
		/// <param name="pane">the GraphPane object to which to add the new curve</param>
		/// <param name="points">a PointPairList collection defining the points for this curve</param>
		/// <returns>the newly created CurveItem added to the given GraphPane</returns>
		/// <remarks>This method must be overriden by childs</remarks>
		public override CurveItem CreateInPane( GraphPane pane, PointPairList points )
		{
			ErrorBarItem x = pane.AddErrorBar(this.Label,points,this.Color);
			this.CopyTo(x);
			return x;
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
				return (null == x) ? ErrorBar.Default.PenWidth : (float)x;
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

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebHiLowBarItem"/> to the specified
		/// <see cref="ZedGraph.HiLowBarItem"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.HiLowBarItem"/> object</param>
		public void CopyTo( HiLowBarItem item )
		{
			base.CopyTo(item);
		}

		/// <summary>
		/// Creates a new CurveItem using the PointPairList and add it the the given pane.
		/// </summary>
		/// <param name="pane">the GraphPane object to which to add the new curve</param>
		/// <param name="points">a PointPairList collection defining the points for this curve</param>
		/// <returns>the newly created CurveItem added to the given GraphPane</returns>
		/// <remarks>This method must be overriden by childs</remarks>
		public override CurveItem CreateInPane( GraphPane pane, PointPairList points )
		{
			HiLowBarItem x = pane.AddHiLowBar(this.Label,points,this.Color);
			this.CopyTo(x);
			return x;
		}


		#region Properties
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.HiLowBar.IsMaximumWidth"/>.
		/// </summary>
		/// <remarks> Determines whether the bar width will be based on
		/// the <see cref="Size"/> value, or it will be based on available
		/// space similar to <see cref="BarItem"/> objects.
		/// </remarks>
		/// <value>If true, then the value
		/// of <see cref="Size"/> is ignored.  If this value is true, then
		/// <see cref="BarSettings.MinClusterGap"/> will be used to determine the total
		/// space between each bar.
		/// </value>
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
		/// the cluster gap (see <see cref="BarSettings.GetClusterWidth"/>
		/// and <see cref="BarSettings.MinClusterGap"/>).  That is, if
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
				return (null == x) ? ZedGraph.HiLowBar.Default.Size : (float)x;
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

			this.Fill.Color = Line.Default.FillColor;
			this.Fill.Brush = Line.Default.FillBrush;
			this.Fill.Type  = Line.Default.FillType;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebLineItem"/> to the specified
		/// <see cref="ZedGraph.LineItem"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.LineItem"/> object</param>
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

		/// <summary>
		/// Creates a new CurveItem using the PointPairList and add it the the given pane.
		/// </summary>
		/// <param name="pane">the GraphPane object to which to add the new curve</param>
		/// <param name="points">a PointPairList collection defining the points for this curve</param>
		/// <returns>the newly created CurveItem added to the given GraphPane</returns>
		/// <remarks>This method must be overriden by childs</remarks>
		public override CurveItem CreateInPane( GraphPane pane, PointPairList points )
		{
			LineItem x = pane.AddCurve(this.Label,points,this.Color);
			this.CopyTo(x);
			return x;
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
			Register('t',typeof(ZedGraphWebTextItem));

			this.Border.IsVisible = ZedGraph.PieItem.Default.IsBorderVisible;
			this.Border.PenWidth = ZedGraph.PieItem.Default.BorderWidth;
			this.Border.Color = ZedGraph.PieItem.Default.BorderColor;
			this.LabelDetail.FontSpec.Size = ZedGraph.PieItem.Default.FontSize;
		}	

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebPieItem"/> to the specified
		/// <see cref="ZedGraph.PieItem"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.PieItem"/> object</param>
		public void CopyTo( PieItem item )
		{
			base.CopyTo(item);
			this.Border.CopyTo(item.Border);
			this.LabelDetail.CopyTo(item.LabelDetail);
			item.Value = this.Value;
			item.Displacement = this.Displacement;
			item.LabelType = this.LabelType;
			item.PercentDecimalDigits = this.PercentDecimalDigits;
			item.ValueDecimalDigits = this.ValueDecimalDigits;			
		}

		/// <summary>
		/// Creates a new CurveItem using the PointPairList and add it the the given pane.
		/// </summary>
		/// <param name="pane">the GraphPane object to which to add the new curve</param>
		/// <param name="points">a PointPairList collection defining the points for this curve</param>
		/// <returns>the newly created CurveItem added to the given GraphPane</returns>
		/// <remarks>This method must be overriden by childs</remarks>
		public override CurveItem CreateInPane( GraphPane pane, PointPairList points )
		{
			PieItem x = pane.AddPieSlice(this.Value, this.Color, this.Displacement, this.Label);
			this.CopyTo(x);
			return x;
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
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["Value"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.Displacement"/>.
		/// </summary>
		/// <remarks> Gets or sets the a value which determines the amount, if any, of this <see cref="PieItem"/>  
		/// displacement.
		/// </remarks>
		[NotifyParentProperty(true)]
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
		[NotifyParentProperty(true)]
		public	PieLabelType LabelType
		{
			get 
			{ 
				object x = ViewState["LabelType"]; 
				return (null == x) ? PieItem.Default.LabelType : (PieLabelType)x;
			}
			set { ViewState["LabelType"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.ValueDecimalDigits"/>.
		/// </summary>
		/// <remarks> Gets or sets the number of decimal digits to be displayed in a <see cref="PieItem"/> 
		/// value label.
		/// </remarks>
		[NotifyParentProperty(true)]
		public int ValueDecimalDigits
		{
			get 
			{ 
				object x = ViewState["ValueDecimalDigits"]; 
				return (null == x) ? PieItem.Default.ValueDecimalDigits : (int)x;
			}
			set { ViewState["ValueDecimalDigits"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.PercentDecimalDigits"/>.
		/// </summary>
		/// <remarks> Gets or sets the number of decimal digits to be displayed in a <see cref="PieItem"/> 
		/// percent label.
		/// </remarks>
		[NotifyParentProperty(true)]
		public int PercentDecimalDigits
		{
			get 
			{ 
				object x = ViewState["PercentDecimalDigits"]; 
				return (null == x) ? PieItem.Default.PercentDecimalDigits : (int)x;
			}
			set { ViewState["PercentDecimalDigits"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.PieItem.LabelDetail"/>.
		/// </summary>
		/// <remarks> Gets or sets the <see cref="TextObj"/> to be used
		/// for displaying this <see cref="PieItem"/>'s label.
		/// </remarks>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebTextItem LabelDetail
		{
			get { return (ZedGraphWebTextItem)base.GetValue('t'); }
		}

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
	/// <seealso cref="ZedGraph.GraphObj"/>
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebGraphItem : GenericItem
	{
		/// <summary>
		/// Override the ToString() method.
		/// </summary>
		/// <returns>Always returns the string "GraphObj".</returns>
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
	
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebGraphItem"/> to the specified
		/// <see cref="ZedGraph.GraphObj"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.GraphObj"/> object</param>
		public void CopyTo(GraphObj item)
		{
			item.ZOrder = this.ZOrder;
			item.IsVisible = this.IsVisible;
			this.Location.CopyTo(item.Location);
		}

		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.GraphObj.IsVisible"/>
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
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.GraphObj.Location"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.GraphObj.ZOrder"/>
		/// </summary>
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
		/// <seealso cref="ZedGraph.GraphObj"/>
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
		/// <seealso cref="ZedGraph.GraphObj"/>
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

	#region ZedGraphWebStringCollection
	/// <summary>
	/// Manages a collection of <see cref="ZedGraphWebGraphItem"/> objects that are 
	/// state management aware.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebStringCollection : GenericCollection
	{	
		/// <summary>
		/// Override the ToString() method.
		/// </summary>
		/// <returns>Always returns String.Empty</returns>
		public override string ToString(){return String.Empty;}		

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebStringCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[1];
			Schema[0].code = 's';
			Schema[0].type = typeof(ZedGraphWebString);						
		}		

		/// <summary>
		/// Add a <see cref="ZedGraphWebString"/> to the collection.
		/// </summary>
		/// <param name="item">The <see cref="ZedGraphWebString"/> object to be added</param>
		public void Add(ZedGraphWebString item)
		{
			if ( null != item )
				ListAdd( item );
		}

		/// <summary>
		/// Indexer to access the specified <see cref="string"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="System.String"/> object to be accessed.</param>
		/// <value>A <see cref="string"/> object reference.</value>
		/// <seealso cref="System.String"/>
		[NotifyParentProperty(true)]
		public ZedGraphWebString this [int index]
		{
			get 
			{
				return (ZedGraphWebString)ListGet(index);
			}
			set
			{
				ListInsert(index,value);
			}
		}			
	}

	#endregion

	#region ZedGraphWebPointPairCollection
	/// <summary>
	/// Manages a collection of <see cref="ZedGraphWebPointPair"/> objects that are 
	/// state management aware.
	/// </summary>
	/// <author>Darren Martz</author>
	public class ZedGraphWebPointPairCollection : GenericCollection
	{	
		/// <summary>
		/// Override the ToString() method.
		/// </summary>
		/// <returns>Always returns String.Empty</returns>
		public override string ToString(){return String.Empty;}		

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZedGraphWebPointPairCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[1];
			Schema[0].code = 'p';
			Schema[0].type = typeof(ZedGraphWebPointPair);			
		}

		/// <summary>
		/// Add a <see cref="ZedGraphWebPointPair"/> to this collection.
		/// </summary>
		/// <param name="item">The <see cref="ZedGraphWebPointPair"/> to be added.</param>
		/// <seealso cref="ZedGraph.PointPair"/>
		public void Add(ZedGraphWebPointPair item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		/// <summary>
		/// Indexer to access the specified <see cref="ZedGraphWebPointPair"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="ZedGraphWebPointPair"/> object to be accessed.</param>
		/// <value>A <see cref="ZedGraphWebPointPair"/> object reference.</value>
		/// <seealso cref="ZedGraph.PointPair"/>
		[NotifyParentProperty(true)]
		public ZedGraphWebPointPair this [int index]
		{
			get 
			{
				return (ZedGraphWebPointPair)ListGet(index);
			}
			set
			{
				ListInsert(index,value);
			}
		}			
	}

	#endregion

	#region ZedGraphWebPointPair
	/// <summary>
	/// A Web PointPair class
	/// </summary>
	/// <author>Darren Martz</author>
	[DefaultProperty("Value")]
	public class ZedGraphWebPointPair : GenericItem
	{
		/// <summary>
		/// Identifies PointPair instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{						
			return string.Format( "PointPair({0},{1})", X, Y );
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebPointPair() : base()
		{				
		}		
		
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebPointPair"/> to the specified
		/// <see cref="PointPair"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="PointPair"/> object</param>
		public void CopyTo( PointPair item )
		{
			item.X = this.X;
			item.Y = this.Y;			
			item.Z = this.Z;			
		}
		
		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PointPair.X"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public double X
		{
			get 
			{ 
				object x = ViewState["X"]; 
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["X"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PointPair.Y"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public double Y
		{
			get 
			{ 
				object y = ViewState["Y"]; 
				return (null == y) ? 0 : (double)y;
			}
			set { ViewState["Y"] = value; }
		}
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PointPair.Z"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public double Z
		{
			get 
			{ 
				object z = ViewState["Z"]; 
				return (null == z) ? 0 : (double)z;
			}
			set { ViewState["Z"] = value; }
		}
		
		#endregion
	}
	#endregion

	#region ZedGraphWebString
	/// <summary>
	/// A Web String class
	/// </summary>
	/// <author>Darren Martz</author>
	[DefaultProperty("Value")]
	public class ZedGraphWebString : GenericItem
	{
		/// <summary>
		/// Identifies fontspec instance
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{						
			return this.Value;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebString() : base()
		{				
		}		
		
		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="SizeF.Height"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public string Value
		{
			get 
			{ 
				object x = ViewState["Value"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["Value"] = value; }
		}		
		
		#endregion
	}
	#endregion

	#region ZedGraphWebXAxis
	/// <summary>
	/// Web control state management class for a <see cref="ZedGraph.XAxis"/> object
	/// </summary>
	/// <author>Darren Martz</author>	
	public class ZedGraphWebXAxis : ZedGraphWebAxis
	{
		/// <summary>
		/// Identifies <see cref="XAxis"/> by the <see cref="Axis.Title"/> value
		/// </summary>
		/// <returns>A string containing "XAxis: title", where 'title' is the
		/// <see cref="Axis.Title"/> property of the <see cref="XAxis"/>
		/// </returns>
		public override string ToString()
		{
			if (Title == string.Empty) return string.Empty;
			return "XAxis: " + Title;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ZedGraphWebXAxis() : base()
		{
		}
	}
	#endregion

	#region ZedGraphWebYAxis
	/// <summary>
	/// Web control state management class for a <see cref="ZedGraph.YAxis"/> object
	/// </summary>
	/// <author>Darren Martz</author>	
	public class ZedGraphWebYAxis : ZedGraphWebAxis
	{
		/// <summary>
		/// Identifies <see cref="YAxis"/> by the <see cref="Axis.Title"/> value
		/// </summary>
		/// <returns>A string containing "YAxis: title", where 'title' is the
		/// <see cref="Axis.Title"/> property of the <see cref="YAxis"/>
		/// </returns>
		public override string ToString()
		{
			if (Title == string.Empty) return string.Empty;
			return "YAxis: " + Title;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ZedGraphWebYAxis() : base()
		{
			this.ScaleFontSpec.Angle = 90.0F;
			this.TitleFontSpec.Angle = -180F;
		}
	}
	#endregion

	#region ZedGraphWebY2Axis
	/// <summary>
	/// Web control state management class for a <see cref="ZedGraph.Y2Axis"/> object
	/// </summary>
	/// <author>Darren Martz</author>	
	public class ZedGraphWebY2Axis : ZedGraphWebAxis
	{
		/// <summary>
		/// Identifies <see cref="Y2Axis"/> by the <see cref="Axis.Title"/> value
		/// </summary>
		/// <returns>A string containing "Y2Axis: title", where 'title' is the
		/// <see cref="Axis.Title"/> property of the <see cref="Y2Axis"/>
		/// </returns>
		public override string ToString()
		{
			if (Title == string.Empty) return string.Empty;
			return "Y2Axis: " + Title;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ZedGraphWebY2Axis() : base()
		{
			this.ScaleFontSpec.Angle = -90.0F;
		}
	}
	#endregion

	#region ZedGraphWebGrid

	/// <summary>
	/// Proxy class to manage all properties associated with a grid
	/// </summary>
	public class ZedGraphWebGrid : GenericItem
	{
		/// <summary>
		/// Returns a string description of the class
		/// </summary>
		public override string ToString()
		{
			return "ZedGraphWebGrid";
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebGrid() : base()
		{
			this.StepUnit = DateUnit.Day;
		}

		/// <summary>
		/// Set the default properties associated with the major grid.
		/// </summary>
		public void SetDefaultsForMajorGrid()
		{
			
		}

		/// <summary>
		/// Set the default properties associated with the minor grid.
		/// </summary>
		public void SetDefaultsForMinorGrid()
		{
			this.GridColor = ZedGraph.MinorGrid.Default.Color;
			this.TicSize = ZedGraph.MinorTic.Default.Size;
			this.IsTicsOutside = ZedGraph.MinorTic.Default.IsOutside;
			this.IsTicsInside = ZedGraph.MinorTic.Default.IsInside;
			this.IsTicsOpposite = ZedGraph.MinorTic.Default.IsOpposite;
			this.TicPenWidth = ZedGraph.MinorTic.Default.PenWidth;
			this.IsGridVisible = ZedGraph.MinorGrid.Default.IsVisible;
			this.GridDashOn = ZedGraph.MinorGrid.Default.DashOn;
			this.GridDashOff = ZedGraph.MinorGrid.Default.DashOff;
			this.GridPenWidth = ZedGraph.MinorGrid.Default.PenWidth;
		}

		#region properties
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Color"/>.
		/// </summary>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public Color GridColor
		{
			get 
			{ 
				object x = ViewState["GridColor"]; 
				return (null == x) ? ZedGraph.MajorGrid.Default.Color : (Color)x;
			}
			set { ViewState["GridColor"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MajorStepAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the scale major step size value
		/// <see cref="ZedGraph.Scale.MajorStep"/>
		/// is set automatically.
		/// </remarks>
		[Category("Behaviour")]
		[NotifyParentProperty(true)]
		public bool StepAuto
		{
			get 
			{ 
				object x = ViewState["StepAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["StepAuto"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MajorStepAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the scale major step size value
		/// <see cref="ZedGraph.Scale.MajorStep"/>
		/// is set automatically.
		/// </remarks>
		[Category("Behaviour")]
		[NotifyParentProperty(true)]
		public double Step
		{
			get 
			{ 
				object x = ViewState["Step"]; 
				return (null == x) ? 1.0 : (double)x;
			}
			set 
			{ 
				ViewState["Step"] = value; 
				ViewState["StepAuto"] = false;
			}
		}
		
		/// <summary>
		/// Proxy property to handle the units for the <see cref="Scale.MajorStep" /> or
		/// <see cref="Scale.MinorStep" />
		/// properties.
		/// </summary>
		[Category("Behaviour")]
		[NotifyParentProperty(true)]
		[Description("Defines in which unit the Step property is. Applies to AxisType.Date only.")]
		public DateUnit StepUnit
		{
			get { return (DateUnit)ViewState["DateUnit"]; }
			set { ViewState["DateUnit"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorTic.Size"/>.
		/// </summary>
		/// <remarks> The length of the <see cref="ZedGraph.Axis"/> major tic marks, expressed in points
		/// (1/72nd inch).
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public float TicSize
		{
			get 
			{ 
				object x = ViewState["TicSize"]; 
				return (null == x) ? ZedGraph.MajorTic.Default.Size : (float)x;
			}
			set { ViewState["TicSize"] = value; }
		}


		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorTic.IsOutside"/>.
		/// </summary>
		/// <remarks> Determines whether or not the <see cref="ZedGraph.Axis"/> major tics (where the
		/// scale labels are located) will be displayed.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Show tics outside")]
		public bool IsTicsOutside
		{
			get 
			{ 
				object x = ViewState["IsTicsOutside"]; 
				return (null == x) ? ZedGraph.MajorTic.Default.IsOutside : (bool)x;
			}
			set { ViewState["IsTicsOutside"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorTic.IsInside"/>.
		/// </summary>
		/// <remarks> This value determines whether or not the major inside tic marks
		/// are shown.  These are the tic marks on the inside of the <see cref="ZedGraph.Axis"/> border.
		/// The major tic spacing is controlled by <see cref="ZedGraph.Scale.MajorStep"/>.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Show tics inside")]
		public bool IsTicsInside
		{
			get 
			{ 
				object x = ViewState["IsTicsInside"]; 
				return (null == x) ? ZedGraph.MajorTic.Default.IsInside : (bool)x;
			}
			set { ViewState["IsTicsInside"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorTic.IsOpposite"/>.
		/// </summary>
		/// <remarks> This value determines whether or not the major opposite tic marks
		/// are shown.  These are the tic marks on the inside of the <see cref="ZedGraph.Axis"/> border on
		/// the opposite side from the axis.
		/// The major tic spacing is controlled by <see cref="ZedGraph.Scale.MajorStep"/>.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public bool IsTicsOpposite
		{
			get 
			{ 
				object x = ViewState["IsTicsOpposite"]; 
				return (null == x) ? ZedGraph.MajorTic.Default.IsOpposite : (bool)x;
			}
			set { ViewState["IsTicsOpposite"] = value; }
		}
		

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorTic.PenWidth"/>.
		/// </summary>
		/// <remarks> This property determines the pen width to be used when drawing the tic marks for
		/// this <see cref="ZedGraph.Axis"/>.  The pen width is expressed in points (1/72nd inch).
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public float TicPenWidth
		{
			get 
			{ 
				object x = ViewState["TicPenWidth"]; 
				return (null == x) ? ZedGraph.MajorTic.Default.PenWidth : (float)x;
			}
			set { ViewState["TicPenWidth"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorGrid.IsVisible"/>.
		/// </summary>
		/// <remarks> This property determines if the major <see cref="Axis"/> gridlines
		/// (at each labeled value) will be visible.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public bool IsGridVisible
		{
			get 
			{ 
				object x = ViewState["IsShowGrid"]; 
				return (null == x) ? ZedGraph.MajorGrid.Default.IsVisible : (bool)x;
			}
			set { ViewState["IsShowGrid"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorGrid.DashOn"/>.
		/// </summary>
		/// <remarks> This is the distance,
		/// in points (1/72 inch), of the dash segments that make up the dashed grid lines.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public float GridDashOn
		{
			get 
			{ 
				object x = ViewState["GridDashOn"]; 
				return (null == x) ? ZedGraph.MajorGrid.Default.DashOn : (float)x;
			}
			set { ViewState["GridDashOn"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorGrid.DashOff"/>.
		/// </summary>
		/// <remarks> This is the distance,
		/// in points (1/72 inch), of the spaces between the dash segments that make up
		/// the dashed grid lines.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public float GridDashOff
		{
			get 
			{ 
				object x = ViewState["GridDashOff"]; 
				return (null == x) ? ZedGraph.MajorGrid.Default.DashOff : (float)x;
			}
			set { ViewState["GridDashOff"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MinorGrid.PenWidth"/>.
		/// </summary>
		/// <remarks> The pen width used for drawing the grid lines, expressed in points (1/72nd inch).
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public float GridPenWidth
		{
			get 
			{ 
				object x = ViewState["GridPenWidth"]; 
				return (null == x) ? ZedGraph.MajorGrid.Default.PenWidth : (float)x;
			}
			set { ViewState["GridPenWidth"] = value; }
		}
		#endregion

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
			if (Title == string.Empty) return string.Empty;
			return "Axis: " + Title;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebAxis() : base()
		{
			Register('z',typeof(ZedGraphWebFontSpec));
			Register('Z',typeof(ZedGraphWebFontSpec));
			Register('y',typeof(ZedGraphWebGrid));
			Register('Y',typeof(ZedGraphWebGrid));

			this.MajorGrid.SetDefaultsForMajorGrid();
			this.MinorGrid.SetDefaultsForMinorGrid();

			this.ScaleFontSpec.Family = ZedGraph.Scale.Default.FontFamily;
			this.ScaleFontSpec.Size = ZedGraph.Scale.Default.FontSize;
			this.ScaleFontSpec.IsBold = ZedGraph.Scale.Default.FontBold;
			this.ScaleFontSpec.FontColor = ZedGraph.Scale.Default.FontColor;
			this.ScaleFontSpec.IsItalic = ZedGraph.Scale.Default.FontItalic;
			this.ScaleFontSpec.IsUnderline = ZedGraph.Scale.Default.FontUnderline;
			this.ScaleFontSpec.Fill.Color = ZedGraph.Scale.Default.FillColor;
			this.ScaleFontSpec.Fill.Type = ZedGraph.Scale.Default.FillType;

			this.TitleFontSpec.Family = ZedGraph.Axis.Default.TitleFontFamily;
			this.TitleFontSpec.Size = ZedGraph.Axis.Default.TitleFontSize;
			this.TitleFontSpec.IsBold = ZedGraph.Axis.Default.TitleFontBold;
			this.TitleFontSpec.FontColor = ZedGraph.Axis.Default.TitleFontColor;
			this.TitleFontSpec.IsItalic = ZedGraph.Axis.Default.TitleFontItalic;
			this.TitleFontSpec.IsUnderline = ZedGraph.Axis.Default.TitleFontUnderline;
			this.TitleFontSpec.Fill.Color = ZedGraph.Axis.Default.TitleFillColor;
			this.TitleFontSpec.Fill.Type = ZedGraph.Axis.Default.TitleFillType;
		}	
	
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebAxis"/> to the specified
		/// <see cref="ZedGraph.Axis"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.Axis"/> object</param>
		public void CopyTo(Axis item)
		{				
			this.ScaleFontSpec.CopyTo( item.Scale.FontSpec );
			this.TitleFontSpec.CopyTo( item.Title.FontSpec );

			item.IsVisible = this.IsVisible;
			item.Scale.IsReverse = this.IsReverse;
			item.Type = this.Type;
			item.Color = this.AxisColor;
			item.Title.Text = this.Title;
			item.Title.IsVisible = this.IsShowTitle;
			item.Title.IsOmitMag = this.IsOmitMag;
			item.Scale.IsUseTenPower = this.IsUseTenPower;
			item.Scale.IsPreventLabelOverlap = this.IsPreventLabelOverlap;
			item.Scale.Format = this.ScaleFormat;
            item.Scale.Align = this.ScaleAlign;
            item.Scale.Mag = this.ScaleMag;
            item.Scale.Min = this.Min;
            item.Scale.Max = this.Max;
            item.Scale.MinGrace = this.MinGrace;
            item.Scale.MaxGrace = this.MaxGrace;
            item.Scale.MinAuto = this.MinAuto;
            item.Scale.MaxAuto = this.MaxAuto;
			//item.NumDecAuto = this.NumDecAuto;
			//item.NumDec = this.NumDec;
            item.Scale.MagAuto = this.ScaleMagAuto;
            item.Scale.FormatAuto = this.ScaleFormatAuto;

			item.Cross = this.Cross;
			item.CrossAuto = this.CrossAuto;
			item.MajorGrid.IsZeroLine = this.IsZeroLine;
			item.MinSpace = this.MinSpace;
			item.MajorTic.IsBetweenLabels = this.IsTicsBetweenLabels;

            item.MajorGrid.IsVisible = this.MajorGrid.IsGridVisible;
            item.MajorGrid.Color = this.MajorGrid.GridColor;
			item.MajorTic.Size = this.MajorGrid.TicSize;
			item.MajorTic.IsOutside = this.MajorGrid.IsTicsOutside;
			item.MajorTic.IsInside = this.MajorGrid.IsTicsInside;
			item.MajorTic.IsOpposite = this.MajorGrid.IsTicsOpposite;
			item.MajorTic.PenWidth = this.MajorGrid.TicPenWidth;
            item.MajorGrid.DashOn = this.MajorGrid.GridDashOn;
            item.MajorGrid.DashOff = this.MajorGrid.GridDashOff;
            item.MajorGrid.PenWidth = this.MajorGrid.GridPenWidth;
            item.Scale.MajorUnit = this.MajorGrid.StepUnit;
            item.Scale.MajorStep = this.MajorGrid.Step;
            item.Scale.MajorStepAuto = this.MajorGrid.StepAuto;
			
			item.MinorGrid.IsVisible = this.MinorGrid.IsGridVisible;
			item.MinorGrid.Color = this.MinorGrid.GridColor;
			item.MajorTic.Size = this.MinorGrid.TicSize;
			item.MinorTic.IsOutside = this.MinorGrid.IsTicsOutside;
			item.MinorTic.IsInside = this.MinorGrid.IsTicsInside;
			item.MinorTic.IsOpposite = this.MinorGrid.IsTicsOpposite;
			item.MinorGrid.DashOn = this.MinorGrid.GridDashOn;
            item.MinorGrid.DashOff = this.MinorGrid.GridDashOff;
            item.MinorGrid.PenWidth = this.MinorGrid.GridPenWidth;
            item.Scale.MinorUnit = this.MinorGrid.StepUnit;
            item.Scale.MinorStep = this.MinorGrid.Step;
            item.Scale.MinorStepAuto = this.MinorGrid.StepAuto;
		}

		#region Properties	

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Color"/>.
		/// </summary>
		/// <remarks> This affects only the grid lines, since the <see cref="Axis.Title"/> and
		/// <see cref="Axis.Scale"/> both have their own color specification.
		/// </remarks>
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public Color AxisColor
		{
			get 
			{ 
				object x = ViewState["AxisColor"]; 
				return (null == x) ? ZedGraph.Axis.Default.Color : (Color)x;
			}
			set { ViewState["AxisColor"] = value; }
		}
		

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.Min"/>.
		/// </summary>
		[NotifyParentProperty(true)]
		public double Min
		{
			get 
			{ 
				object x = ViewState["Min"]; 
				return (null == x) ? 0.0 : (double)x;
			}
			set 
			{ 
				ViewState["Min"] = value; 
				ViewState["MinAuto"] = false; 
			}
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.Max"/>.
		/// </summary>
		[NotifyParentProperty(true)]
		public double Max
		{
			get 
			{ 
				object x = ViewState["Max"]; 
				return (null == x) ? 0.0 : (double)x;
			}
			set 
			{ 
				ViewState["Max"] = value; 
				ViewState["MaxAuto"] = false; 
			}
		}

		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Axis.Title"/>.
		/// </summary>
		/// <remarks> The title normally shows the basis and dimensions of
		/// the scale range, such as "Time (Years)".  The title is only shown if the
		/// <see cref="Label.IsVisible"/> property is set to true.  If the Title text is empty,
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
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Cross"/>.
		/// </summary>
		/// <remarks>This property allows the axis to be shifted away from its default location.
		/// For example, for a graph with an X range from -100 to +100, the Y Axis can be located
		/// at the X=0 value rather than the left edge of the ChartRect.  This value can be set
		/// automatically based on the state of <see cref="CrossAuto"/>.  If
		/// this value is set manually, then <see cref="CrossAuto"/> will
		/// also be set to false.  The "other" axis is the axis the handles the second dimension
		/// for the graph.  For the XAxis, the "other" axis is the YAxis.  For the YAxis or
		/// Y2Axis, the "other" axis is the XAxis.
		/// </remarks>
		/// <value> The value is defined in user scale units </value>
		[NotifyParentProperty(true)]
		public double Cross
		{
			get 
			{ 
				object x = ViewState["Cross"]; 
				return (null == x) ? 0 : (double)x;
			}
			set { ViewState["Cross"] = value; ViewState["CrossAuto"] = false; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MinAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the minimum scale value <see cref="Scale.Min"/>
		/// is set automatically.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool MinAuto
		{
			get 
			{ 
				object x = ViewState["MinAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["MinAuto"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MaxAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the maximum scale value <see cref="Scale.Max"/>
		/// is set automatically.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool MaxAuto
		{
			get 
			{ 
				object x = ViewState["MaxAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["MaxAuto"] = value; }
		}


		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.CrossAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the axis intersection point <see cref="ZedGraph.Axis.Cross"/>
		/// is set automatically.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool CrossAuto
		{
			get 
			{ 
				object x = ViewState["CrossAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["CrossAuto"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MinGrace"/>.
		/// </summary>
		/// <remarks> This is the "grace" value applied to the minimum data range.
		/// This value is expressed as a fraction of the total data range.  For example, assume the data
		/// range is from 4.0 to 16.0, leaving a range of 12.0.  If MinGrace is set to
		/// 0.1, then 10% of the range, or 1.2 will be subtracted from the minimum data value.
		/// The scale will then be ranged to cover at least 2.8 to 16.0.
		/// </remarks>
		[NotifyParentProperty(true)]
		[Description("Fraction of data range to substract to Min. For example 0.1 means 10% of (Max-Min).")]
		public double MinGrace
		{
			get 
			{ 
				object x = ViewState["MinGrace"];
				return (null == x) ? ZedGraph.Scale.Default.MinGrace : (double)x;
			}
			set { ViewState["MinGrace"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MaxGrace"/>.
		/// </summary>
		/// <remarks> This is the "grace" value applied to the maximum data range.
		/// This value is expressed as a fraction of the total data range.  For example, assume the data
		/// range is from 4.0 to 16.0, leaving a range of 12.0.  If MaxGrace is set to
		/// 0.1, then 10% of the range, or 1.2 will be added to the maximum data value.
		/// The scale will then be ranged to cover at least 4.0 to 17.2.
		/// </remarks>
		[NotifyParentProperty(true)]
		[Description("Fraction of data range to add to Max. For example 0.1 means 10% of (Max-Min).")]
		public double MaxGrace
		{
			get 
			{ 
				object x = ViewState["MaxGrace"]; 
				return (null == x) ? ZedGraph.Scale.Default.MaxGrace : (double)x;
			}
			set { ViewState["MaxGrace"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.MinSpace"/>.
		/// </summary>
		/// <remarks> This is the minimum axis space allocation.
		/// This term, expressed in points (1/72 inch) and scaled according to <see cref="PaneBase.CalcScaleFactor"/>
		/// for the <see cref="ZedGraph.GraphPane"/>, determines the minimum amount of space
		/// an axis must have between the <see cref="Chart.Rect"/> and the
		/// <see cref="PaneBase.Rect"/>.  This minimum space
		/// applies whether <see cref="ZedGraph.Axis.IsVisible"/> is true or false.
		/// </remarks>
		[NotifyParentProperty(true)]
		[Description("Minimum amount of space in points between this axis and the pane border. Applies even if IsVisible is false.")]
		public float MinSpace
		{
			get 
			{ 
				object x = ViewState["MinSpace"]; 
				return (null == x) ? ZedGraph.Axis.Default.MinSpace : (float)x;
			}
			set { ViewState["MinSpace"] = value; }
		}



		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="MajorTic.IsBetweenLabels"/>.
		/// </summary>
		/// <remarks> This property determines whether or not the major tics will be drawn
		/// inbetween the labels, rather than right at the labels.  Note that this setting is only
		/// applicable if <see cref="ZedGraph.Axis.Type"/> = <see cref="AxisType.Text"/>.
		/// </remarks>
		[NotifyParentProperty(true)]
		[Description("If true, draws major tics inbetween labels rather than right at the labels. Only for AxisType.Text.")]
		public bool IsTicsBetweenLabels
		{
			get 
			{ 
				object x = ViewState["IsTicsBetweenLabels"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsTicsBetweenLabels"] = value; }
		}


		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.MajorGrid.IsZeroLine"/>.
		/// </summary>
		/// <remarks> This boolean value determines if a line will be drawn at the
		/// zero value for the axis.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsZeroLine
		{
			get 
			{ 
				object x = ViewState["IsZeroLine"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsZeroLine"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.IsVisible"/>.
		/// </summary>
		/// <remarks> Determines whether or not the <see cref="ZedGraph.Axis"/> is shown.
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
		/// Proxy property that gets or sets the value of <see cref="Scale.IsReverse"/>.
		/// </summary>
		/// <remarks> Determines if the scale values are reversed for this <see cref="ZedGraph.Axis"/>.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsReverse
		{
			get 
			{ 
				object x = ViewState["IsReverse"]; 
				return (null == x) ? ZedGraph.Scale.Default.IsReverse : (bool)x;
			}
			set { ViewState["IsReverse"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.Type"/>.
		/// </summary>
		/// <remarks> Determines the <see cref="AxisType"/> for this <see cref="ZedGraph.Axis"/>.
		/// The type can be either <see cref="AxisType.Linear"/>,
		/// <see cref="AxisType.Log"/>, <see cref="AxisType.Date"/>,
		/// or <see cref="AxisType.Text"/>.
		/// </remarks>
		[NotifyParentProperty(true)]
		public AxisType Type
		{
			get 
			{ 
				object x = ViewState["Type"]; 
				return (null == x) ? ZedGraph.Axis.Default.Type : (AxisType)x;
			}
			set { ViewState["Type"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="AxisLabel.IsOmitMag"/>.
		/// </summary>
		/// <remarks> This property controls whether or not the magnitude factor (power of 10) for
		/// this scale will be included in the label.
		/// For large scale values, a "magnitude" value (power of 10) is automatically
		/// used for scaling the graph.  This magnitude value is automatically appended
		/// to the end of the Axis <see cref="ZedGraph.Axis.Title"/> (e.g., "(10^4)") to indicate
		/// that a magnitude is in use.  This property controls whether or not the
		/// magnitude is included in the title.  Note that it only affects the axis
		/// title; a magnitude value may still be used even if it is not shown in the title.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsOmitMag
		{
			get 
			{ 
				object x = ViewState["IsOmitMag"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsOmitMag"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Axis.Title"/>
		/// <see cref="Label.IsVisible" />.
		/// </summary>
		/// <remarks> Determines whether or not the <see cref="ZedGraph.Axis"/>
		/// <see cref="ZedGraph.Axis.Title"/> will be displayed.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsShowTitle
		{
			get 
			{ 
				object x = ViewState["IsShowTitle"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsShowTitle"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.IsUseTenPower"/>.
		/// </summary>
		/// <remarks> Determines if powers-of-ten notation will be used for the numeric value labels.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsUseTenPower
		{
			get 
			{ 
				object x = ViewState["IsUseTenPower"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsUseTenPower"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.IsPreventLabelOverlap"/>.
		/// </summary>
		/// <remarks> This <see cref="bool"/> value determines if ZedGraph will check to
		/// see if the <see cref="ZedGraph.Axis"/> scale labels are close enough to overlap.  If so,
		/// ZedGraph will adjust the step size to prevent overlap.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsPreventLabelOverlap
		{
			get 
			{ 
				object x = ViewState["IsPreventLabelOverlap"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsPreventLabelOverlap"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.FormatAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the scale label format <see cref="Scale.Format"/>
		/// is determined automatically based on the range of data values.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool ScaleFormatAuto
		{
			get 
			{ 
				object x = ViewState["ScaleFormatAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["ScaleFormatAuto"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.Format"/>.
		/// </summary>
		/// <remarks> The format of the <see cref="ZedGraph.Axis"/> tic labels.
		/// This property is only used if the <see cref="Type"/> is set to <see cref="AxisType.Date"/>.
		/// This property may be set automatically by ZedGraph, depending on the state of
		/// <see cref="Scale.FormatAuto"/>.
		/// </remarks>
		[Description("Date format for labels. Only for AxisType.Date.")]
		[NotifyParentProperty(true)]
		public string ScaleFormat
		{
			get 
			{ 
				object x = ViewState["ScaleFormat"]; 
				return (null == x) ? ZedGraph.Scale.Default.Format : (string)x;
			}
			set { ViewState["ScaleFormat"] = value; ViewState["ScaleFormatAuto"] = false; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.Align"/>.
		/// </summary>
		/// <remarks> Controls the alignment of the <see cref="ZedGraph.Axis"/> tic labels.
		/// </remarks>
		[NotifyParentProperty(true)]
		[Description("Controls the aligment of the tic labels")]
		public AlignP ScaleAlign
		{
			get 
			{ 
				object x = ViewState["ScaleAlign"]; 
				return (null == x) ? ZedGraph.Scale.Default.Align : (AlignP)x;
			}
			set { ViewState["ScaleAlign"] = value; }
		}
/*
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.NumDecAuto"/>.
		/// </summary>
		/// <remarks> Determines whether or not the number of decimal places for value
		/// labels <see cref="ZedGraph.Axis.NumDec"/> is determined automatically based
		/// on the magnitudes of the scale values.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool NumDecAuto
		{
			get 
			{ 
				object x = ViewState["NumDecAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["NumDecAuto"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Axis.NumDec"/>.
		/// </summary>
		/// <remarks> The number of decimal places displayed for axis value labels.
		/// This value can be determined automatically depending on the state of
		/// <see cref="ZedGraph.Axis.NumDecAuto"/>.  If this value is set manually by the user,
		/// then <see cref="ZedGraph.Axis.NumDecAuto"/> will also be set to false.
		/// </remarks>
		[NotifyParentProperty(true)]
		public int NumDec
		{
			get 
			{ 
				object x = ViewState["NumDec"]; 
				return (null == x) ? 0 : (int)x;
			}
			set { ViewState["NumDec"] = value; ViewState["NumDecAuto"] = false; }
		}
*/
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.Mag"/>.
		/// </summary>
		/// <remarks> The magnitude multiplier for scale values.
		/// This is used to limit
		/// the size of the displayed value labels.  For example, if the value
		/// is really 2000000, then the graph will display 2000 with a 10^3
		/// magnitude multiplier.  This value can be determined automatically
		/// depending on the state of <see cref="Scale.MagAuto"/>.
		/// If this value is set manually by the user,
		/// then <see cref="Scale.MagAuto"/> will also be set to false.
		/// </remarks>
		[NotifyParentProperty(true)]
		public int ScaleMag
		{
			get 
			{ 
				object x = ViewState["ScaleMag"]; 
				return (null == x) ? 0 : (int)x;
			}
			set { ViewState["ScaleMag"] = value; ViewState["ScaleMagAuto"] = false; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.MagAuto"/>.
		/// </summary>
		/// <remarks> Determines whether the <see cref="Scale.Mag"/> value will be set
		/// automatically based on the data, or manually by the user.
		/// If the user manually sets the <see cref="Scale.Mag"/> value, then this
		/// flag will be set to false.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool ScaleMagAuto
		{
			get 
			{ 
				object x = ViewState["ScaleMagAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["ScaleMagAuto"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Scale.FontSpec"/>.
		/// </summary>
		/// <remarks> Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the scale values.
		/// </remarks>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec ScaleFontSpec
		{
			get { return (ZedGraphWebFontSpec)base.GetValue('z'); }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Axis.Title" /> <see cref="FontSpec"/>.
		/// </summary>
		/// <remarks> Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
		/// the <see cref="ZedGraph.Axis.Title"/>
		/// </remarks>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec TitleFontSpec
		{
			get { return (ZedGraphWebFontSpec)base.GetValue('Z'); }
		}

		/// <summary>
		/// Proxy property to get or set the values for the <see cref="GraphPane" /> major grid.
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebGrid MinorGrid
		{
			get { return (ZedGraphWebGrid)base.GetValue('y'); }
		}

		/// <summary>
		/// Proxy property to get or set the values for the <see cref="GraphPane" /> minor grid.
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebGrid MajorGrid
		{
			get { return (ZedGraphWebGrid)base.GetValue('Y'); }
		}

		#endregion		
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
			Register('F',typeof(ZedGraphWebFontSpec));
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
			Register('l',typeof(ZedGraphWebLocation));

			this.Border.Color = Legend.Default.BorderColor;
			this.Border.PenWidth = Legend.Default.BorderWidth;
			this.Border.IsVisible = Legend.Default.IsBorderVisible;
			this.Fill.Brush = Legend.Default.FillBrush;
			this.Fill.Color = Legend.Default.FillColor;
			this.Fill.Type = Legend.Default.FillType;
			this.FontSpec.IsBold = Legend.Default.FontBold;
			this.FontSpec.FontColor = Legend.Default.FontColor;
			this.FontSpec.Family = Legend.Default.FontFamily;
			this.FontSpec.Fill.Brush = Legend.Default.FontFillBrush;
			this.FontSpec.Fill.Color = Legend.Default.FontFillColor;
			this.FontSpec.IsItalic = Legend.Default.FontItalic;
			this.FontSpec.Size = Legend.Default.FontSize;					
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebLegend"/> to the specified
		/// <see cref="ZedGraph.Legend"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.Legend"/> object</param>
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

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Legend.Location"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Legend.FontSpec"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec FontSpec
		{
			get { return (ZedGraphWebFontSpec)base.GetValue('F'); }
		}

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Legend.Fill"/>
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

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Legend.Border"/>
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

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Legend.Rect"/>
		/// </summary>
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
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Legend.IsHStack"/>
		/// </summary>
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
			string color = this.FontColor.Name + " " + this.Size + "pts";
			string fill;
			if( !this.Fill.IsVisible || this.Fill.Type == FillType.None )
				fill = "none";
			else
				fill = this.Fill.Color.Name;
			string border;
			if( !this.Border.IsVisible )
				border = "none";
			else
				border = this.Border.Color.Name;

			return color + " " + fill + " " + border;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebFontSpec() : base()
		{
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));	
		
			this.Fill.Color = ZedGraph.FontSpec.Default.FillColor;
			this.Fill.Brush = ZedGraph.FontSpec.Default.FillBrush;
			this.Fill.Type  = ZedGraph.FontSpec.Default.FillType;
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebFontSpec"/> to the specified
		/// <see cref="ZedGraph.FontSpec"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.FontSpec"/> object</param>
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

	#region ZedGraphWebRect (X,Y,Width,Height)
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
			return string.Format("Rect({0},{1},{2},{3})", X,Y,Height,Width);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebRect() : base()
		{				
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebRect"/> to the specified
		/// <see cref="RectangleF"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="RectangleF"/> object</param>
		public void CopyTo( RectangleF item )
		{
			item.X = this.X;
			item.Y = this.Y;
			item.Width = this.Width;
			item.Height = this.Height;
		}
		
	#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="RectangleF.Height"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="RectangleF.Width"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="RectangleF.Y"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="RectangleF.X"/>
		/// </summary>
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

	#region ZedGraphWebRect2 (Left, Top, Right, Bottom)
	/// <summary>
	/// Rectangle class for margins
	/// </summary>
	/// <author>Benjamin Mayrargue</author>
	public class ZedGraphWebRect2 : GenericItem
	{
		/// <summary>
		/// Override ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{		
			return string.Format("Rect({0},{1},{2},{3})", Left, Top, Right, Bottom);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebRect2() : base()
		{				
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebRect2"/> to the specified
		/// <see cref="RectangleF"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="RectangleF"/> object</param>
		public void CopyTo( RectangleF item )
		{
			item.X = this.Left;
			item.Y = this.Top;
			item.Width = this.Right - this.Left;
			item.Height = this.Bottom - this.Top;
		}
		
	#region Properties

		/// <summary>
		/// </summary>
		[NotifyParentProperty(true)]
		public float Left
		{
			get 
			{ 
				object x = ViewState["Left"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Left"] = value; }
		}

		/// <summary>
		/// </summary>
		[NotifyParentProperty(true)]
		public float Top
		{
			get 
			{ 
				object x = ViewState["Top"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Top"] = value; }
		}

		/// <summary>
		/// </summary>
		[NotifyParentProperty(true)]
		public float Right
		{
			get 
			{ 
				object x = ViewState["Right"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Right"] = value; }
		}

		/// <summary>
		/// </summary>
		[NotifyParentProperty(true)]
		public float Bottom
		{
			get 
			{ 
				object x = ViewState["Bottom"]; 
				return (null == x) ? 0 : (float)x;
			}
			set { ViewState["Bottom"] = value; }
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
			if ( Height==0 && Width==0 ) return string.Empty;			
			return string.Format("Size({0},{1})",
				Height,Width);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebSize() : base()
		{				
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebSize"/> to the specified
		/// <see cref="SizeF"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="SizeF"/> object</param>
		public void CopyTo( SizeF item )
		{
			item.Width = this.Width;
			item.Height = this.Height;
		}
		
		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="SizeF.Height"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="SizeF.Width"/>
		/// </summary>
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
			if ( X==0 && Y==0 ) return string.Empty;
			return string.Format("Point({0},{1})",
				X,Y);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ZedGraphWebPoint() : base()
		{
		}

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebPoint"/> to the specified
		/// <see cref="PointF"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="PointF"/> object</param>
		public void CopyTo( PointF item )
		{
			item.X = this.X;
			item.Y = this.Y;			
		}
		
		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PointF.X"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PointF.Y"/>
		/// </summary>
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
	/// <seealso cref="ZedGraph.GraphObj"/>
	/// </summary>
	/// <author>Darren Martz</author>
	/// <remarks>
	/// A TextObj uses only:
	///		FontSpec
	///		Location
	///			.CoordinateFrame (<see cref="CoordType"/>)
	///			.X (between 0 and 1 for fraction CoordTypes) 0=left, 1=right
	///			.Y (between 0 and 1 for fraction CoordTypes) 0=top, 1=bottom
	///			.AlignH
	///			.AlignV
	///			.layoutArea
	/// </remarks>
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

			this.FontSpec.Family = ZedGraph.TextObj.Default.FontFamily;
			this.FontSpec.Size = ZedGraph.TextObj.Default.FontSize;
			this.FontSpec.FontColor = ZedGraph.TextObj.Default.FontColor;
			this.FontSpec.IsBold = ZedGraph.TextObj.Default.FontBold;
			this.FontSpec.IsUnderline = ZedGraph.TextObj.Default.FontUnderline;
			this.FontSpec.IsItalic = ZedGraph.TextObj.Default.FontItalic;			
		}
	
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebTextItem"/> to the specified
		/// <see cref="ZedGraph.TextObj"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.TextObj"/> object</param>
		public void CopyTo( TextObj item )
		{
			base.CopyTo(item);
			this.LayoutArea.CopyTo(item.LayoutArea); //Width, height
			item.Text = this.Text;
			this.FontSpec.CopyTo(item.FontSpec);
		}
		
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.TextObj.Text"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.TextObj.LayoutArea"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebSize LayoutArea
		{
			get { return (ZedGraphWebSize)GetValue('a'); }			
		} 

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.TextObj.FontSpec"/>
		/// </summary>
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
	/// <seealso cref="ZedGraph.GraphObj"/>
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
		
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebArrowItem"/> to the specified
		/// <see cref="ZedGraph.ArrowObj"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.ArrowObj"/> object</param>
		public void CopyTo( ArrowObj item )
		{
			base.CopyTo(item);
			item.Size = this.Size;
			item.PenWidth = this.PenWidth;
			item.Color = this.Color;
			item.IsArrowHead = this.IsArrowHead;
		}

		#region Properties

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.ArrowObj.Size"/>.
		/// </summary>
		/// <remarks> The size of the arrowhead, expressed in points (1/72nd inch).
		/// </remarks>
		[NotifyParentProperty(true)]
		public float Size
		{
			get 
			{ 
				object x = ViewState["Size"]; 
				return (null == x) ? ArrowObj.Default.Size : (float)x;
			}
			set { ViewState["Size"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.LineObj.PenWidth"/>.
		/// </summary>
		/// <remarks> The width of the pen, expressed in points (1/72nd inch), used to draw the
		/// arrow line segment.
		/// </remarks>
		[NotifyParentProperty(true)]
		public float PenWidth
		{
			get 
			{ 
				object x = ViewState["PenWidth"]; 
				return (null == x) ? LineObj.Default.PenWidth : (float)x;
			}
			set { ViewState["PenWidth"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.LineObj.Color"/>.
		/// </summary>
		/// <remarks> The <see cref="System.Drawing.Color"/> value used to draw the <see cref="ZedGraph.ArrowObj"/>.
		/// </remarks>
		[NotifyParentProperty(true)]
		public Color Color
		{
			get 
			{ 
				object x = ViewState["Color"]; 
				return (null == x) ? LineObj.Default.Color : (Color)x;
			}
			set { ViewState["Color"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.ArrowObj.IsArrowHead"/>.
		/// </summary>
		/// <remarks> Determines whether or not an arrowhead will be draw.  If false, only a line segment
		/// will be drawn.
		/// </remarks>
		[NotifyParentProperty(true)]
		public bool IsArrowHead
		{
			get 
			{ 
				object x = ViewState["IsArrowHead"]; 
				return (null == x) ? ArrowObj.Default.IsArrowHead : (bool)x;
			}
			set { ViewState["IsArrowHead"] = value; }
		}

		#endregion
	}
	#endregion

	#region ZedGraphWebImageItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphObj"/>
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
		
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebImageItem"/> to the specified
		/// <see cref="ZedGraph.ImageObj"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.ImageObj"/> object</param>
		public void CopyTo( ImageObj item )
		{
			base.CopyTo(item);
			item.IsScaled = this.IsScaled;					

			item.Image = null;
			try
			{
				if ( (this.ImageUrl != null) && (this.ImageUrl != string.Empty) )
				{
					string path = System.AppDomain.CurrentDomain.BaseDirectory;
					path = System.IO.Path.Combine(path,this.ImageUrl);						
					item.Image = Image.FromFile(path);
				}
			}
			catch(Exception)
			{
				//TODO: deal with failure?
			}			
		}

		#region Properties

		/// <summary>
		/// The <see cref="String"/> url reference from which to get the <see cref="Image"/>
		/// data for this <see cref="ZedGraph.ImageObj"/>.
		/// </summary>
		[NotifyParentProperty(true)]
		public string ImageUrl
		{
			get 
			{ 
				object x = ViewState["ImageUrl"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["ImageUrl"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.ImageObj.IsScaled"/>.
		/// </summary>
		/// <remarks>
		/// Determines if the image will be scaled to the output rectangle (see <see cref="Location"/>).
		/// </remarks>
		/// <value>true to scale the image, false to draw the image unscaled, but clipped
		/// to the destination rectangle</value>
		[NotifyParentProperty(true)]
		public bool IsScaled
		{
			get 
			{ 
				object x = ViewState["IsScaled"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsScaled"] = value; }
		}

		#endregion
	}
	#endregion

	#region ZedGraphWebBoxItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphObj"/>
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
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));

			this.Border.Color = ZedGraph.BoxObj.Default.BorderColor;
			this.Fill.Color = ZedGraph.BoxObj.Default.FillColor;
		}			
	
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebBoxItem"/> to the specified
		/// <see cref="ZedGraph.BoxObj"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.BoxObj"/> object</param>
		public void CopyTo( BoxObj item )
		{
			base.CopyTo(item);
			this.Border.CopyTo(item.Border);
			this.Fill.CopyTo(item.Fill);
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

	#region ZedGraphWebEllipseItem
	/// <summary>
	/// Baseclass for graph items in the web control
	/// <seealso cref="ZedGraph.GraphObj"/>
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
			Register('b',typeof(ZedGraphWebBorder));
			Register('f',typeof(ZedGraphWebFill));
		}
		
		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebEllipseItem"/> to the specified
		/// <see cref="ZedGraph.EllipseObj"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.EllipseObj"/> object</param>
		public void CopyTo( EllipseObj item )
		{
			base.CopyTo(item);
			this.Border.CopyTo(item.Border);
			this.Fill.CopyTo(item.Fill);
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

		/// <summary>
		/// Copy the properties of this <see cref="ZedGraphWebLocation"/> to the specified
		/// <see cref="ZedGraph.Location"/> object.
		/// </summary>
		/// <param name="item">The destination <see cref="ZedGraph.Location"/> object</param>
		public void CopyTo( Location item )
		{
			item.X = this.X;
			item.Y = this.Y;
			item.Width = this.Width;
			item.Height = this.Height;
			item.AlignH = this.AlignH;
			item.AlignV = this.AlignV;
			item.CoordinateFrame = this.CoordinateFrame;
			this.Rect.CopyTo(item.Rect);
			this.TopLeft.CopyTo(item.TopLeft);
			this.BottomRight.CopyTo(item.BottomRight);
		}

		#region Properties

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Location.Rect"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Location.TopLeft"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebPoint TopLeft
		{
			get { return (ZedGraphWebPoint)GetValue('t'); }
		}

		/// <summary>
		/// Proxy property that gets the value of <see cref="ZedGraph.Location.BottomRight"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.Height"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.Width"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.Y"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.X"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.AlignH"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.AlignV"/>
		/// </summary>
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

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraph.Location.CoordinateFrame"/>
		/// </summary>
		[NotifyParentProperty(true)]
		public CoordType CoordinateFrame
		{
			get 
			{ 
				object x = ViewState["CoordinateFrame"]; 
				return (null == x) ? CoordType.ChartFraction : (CoordType)x;
			}
			set { ViewState["CoordinateFrame"] = value; }
		}

		#endregion
	}
	#endregion
}
