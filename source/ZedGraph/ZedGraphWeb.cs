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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZedGraph;

[assembly: TagPrefix("ZedGraph","zgw")]

namespace ZedGraph
{
	/// <summary>
	/// The ZedGraphWeb class provides a web control interface to the
	/// <see cref="ZedGraph"/> class library.  This allows ZedGraph to be used
	/// from a web page with ASP.net.  All graph
	/// attributes are accessible via the <see cref="ZedGraphControl.GraphPane"/>
	/// property.
	/// </summary>
	/// <author> Darren Martz  revised by John Champion </author>
	/// <version> $Revision: 3.8 $ $Date: 2005-02-12 23:22:51 $ </version>
	[	
	ParseChildren(true),
	PersistChildren(false),
	//DefaultProperty("Title"),
	ToolboxData("<{0}:ZedGraphWeb runat=server></{0}:ZedGraphWeb>")
	]
	public class ZedGraphWeb : Control
	{
		/// <summary>
		/// Override the <see cref="ToString"/> method to do nothing.
		/// </summary>
		/// <returns>An empty string</returns>
		public override string ToString()
		{
			return String.Empty;
		}

		#region Constructors
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ZedGraphWeb()
		{				
			vsassist = new GenericViewStateAssistant();									
			vsassist.Register('x',typeof(ZedGraphWebAxis));
			vsassist.Register('y',typeof(ZedGraphWebAxis));
			vsassist.Register('z',typeof(ZedGraphWebAxis));
			vsassist.Register('l',typeof(ZedGraphWebLegend));
			vsassist.Register('b',typeof(ZedGraphWebBorder));
			vsassist.Register('f',typeof(ZedGraphWebFill));
			vsassist.Register('s',typeof(ZedGraphWebFontSpec));
			vsassist.Register('c',typeof(ZedGraphWebCurveCollection));
			vsassist.Register('g',typeof(ZedGraphWebGraphItemCollection));
		}
		#endregion

	#region RenderDemo
		/// <summary>
		/// Renders the demo graph with one call.
		/// </summary>
		/// <param name="g">A <see cref="Graphics"/> object for which the drawing will be done.</param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/></param>
		static public void RenderDemo( Graphics g, ZedGraph.GraphPane pane )
		{
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			LineItem curve;
			curve = pane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;

			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = pane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			curve.Symbol.Size = 10;

			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = pane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			pane.ClusterScaleWidth = 100;
			pane.BarType = BarType.Stack;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = pane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			pane.ClusterScaleWidth = 100;

			pane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );			

			pane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
				Color.FromArgb( 255, 255, 190), 90F );

			pane.XAxis.IsShowGrid = true;
			pane.YAxis.IsShowGrid = true;
			pane.YAxis.Max = 120;

			TextItem text = new TextItem("First Prod\n21-Oct-93", 175F, 80.0F );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			pane.GraphItemList.Add( text );

			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			pane.GraphItemList.Add( arrow );

			text = new TextItem("Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			pane.GraphItemList.Add( text );

			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			pane.GraphItemList.Add( arrow );

			text = new TextItem("Confidential", 0.85F, -0.03F );
			text.Location.CoordinateFrame = CoordType.AxisFraction;

			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;

			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			pane.GraphItemList.Add( text );

			BoxItem box = new BoxItem( new RectangleF( 0, 110, 1200, 10 ),
				Color.Empty, Color.FromArgb( 225, 245, 225) );
			box.Location.CoordinateFrame = CoordType.AxisXYScale;
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			box.ZOrder = ZOrder.E_BehindAxis;
			pane.GraphItemList.Add( box );

			text = new TextItem( "Peak Range", 1170, 105 );
			text.Location.CoordinateFrame = CoordType.AxisXYScale;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.IsItalic = true;
			text.FontSpec.IsBold = false;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			pane.GraphItemList.Add( text );

			pane.AxisChange( g );
		}
		#endregion

	#region Attributes

		/// <summary>
		/// Gets or sets the value of the <see cref="PaneBase.BaseDimension"/>.
		/// </summary>
		[Bindable(true),Category("Layout"),NotifyParentProperty(true)]
		public float BaseDimension
		{
			get 
			{ 
				object x = ViewState["BaseDimension"]; 
				return (null == x) ? PaneBase.Default.BaseDimension : (float)x;
			}
			set { ViewState["BaseDimension"] = value; }
		}
		
		/// <summary>
		/// Gets or sets the width of the <see cref="PaneBase.PaneRect"/>.
		/// </summary>
		/// <value>The width in output device pixels</value>
		[Bindable(true),Category("Layout"),NotifyParentProperty(true),DefaultValue(400)]
		public int Width
		{
			get 
			{ 
				object x = ViewState["Width"]; 
				return (null == x) ? 400 : (int)x;
			}
			set { ViewState["Width"] = value; }
		}
		
		/// <summary>
		/// Gets or sets the Height of the <see cref="PaneBase.PaneRect"/>.
		/// </summary>
		/// <value>The height in output device pixels</value>
		[Bindable(true),Category("Layout"),NotifyParentProperty(true),DefaultValue(250)]
		public int Height
		{
			get 
			{ 
				object x = ViewState["Height"]; 
				return (null == x) ? 250 : (int)x;
			}
			set { ViewState["Height"] = value; }
		}
		
		/// <summary>
		/// Gets or sets the Title of the <see cref="ZedGraph.GraphPane"/>.
		/// </summary>
		/// <value>A title <see cref="string"/></value>
		[Bindable(true),Category("Appearance"),NotifyParentProperty(true),DefaultValue("")]
		public string Title
		{
			get 
			{ 
				object x = ViewState["Title"]; 
				return (null == x) ? string.Empty : (string)x;
			}
			set { ViewState["Title"] = value; }			
		}		
		
		/// <summary>
		/// Gets or sets the value that determines if the <see cref="ZedGraph.PaneBase.Title"/>
		/// is visible.
		/// </summary>
		/// <value>true to show the pane title, false otherwise</value>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public bool IsShowTitle
		{
			get 
			{ 
				object x = ViewState["IsShowTitle"]; 
				return (null == x) ? PaneBase.Default.IsShowTitle : (bool)x;
			}
			set { ViewState["IsShowTitle"] = value; }			
		}

		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.IsIgnoreInitial"/>
		/// </summary>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public bool IsIgnoreInitial
		{
			get 
			{ 
				object x = ViewState["IsIgnoreInitial"]; 
				return (null == x) ? GraphPane.Default.IsIgnoreInitial : (bool)x;
			}
			set { ViewState["IsIgnoreInitial"] = value; }			
		}

		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.IsIgnoreMissing"/>
		/// </summary>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public bool IsIgnoreMissing
		{
			get 
			{ 
				object x = ViewState["IsIgnoreMissing"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["IsIgnoreMissing"] = value; }			
		}

		/// <summary>
		/// <seealso cref="ZedGraph.PaneBase.IsFontsScaled"/>
		/// </summary>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public bool IsFontsScaled
		{
			get 
			{ 
				object x = ViewState["IsFontsScaled"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsFontsScaled"] = value; }			
		}

		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.BarBase"/>
		/// </summary>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public BarBase BarBase
		{
			get 
			{ 
				object x = ViewState["BarBase"]; 
				return (null == x) ? GraphPane.Default.BarBase : (BarBase)x;
			}
			set { ViewState["BarBase"] = value; }			
		}

		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.IsAxisRectAuto"/>
		/// </summary>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public bool IsAxisRectAuto
		{
			get 
			{ 
				object x = ViewState["IsAxisRectAuto"]; 
				return (null == x) ? true : (bool)x;
			}
			set { ViewState["IsAxisRectAuto"] = value; }			
		}

		/// <summary>
		/// <seealso cref="ZedGraph.PaneBase.IsPenWidthScaled"/>
		/// </summary>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true)]
		public bool IsPenWidthScaled
		{
			get 
			{ 
				object x = ViewState["IsPenWidthScaled"]; 
				return (null == x) ? PaneBase.Default.IsPenWidthScaled : (bool)x;
			}
			set { ViewState["IsPenWidthScaled"] = value; }			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true),DefaultValue("false")]
		public bool AxisChanged
		{
			get 
			{ 
				object x = ViewState["AxisChanged"]; 
				return (null == x) ? false : (bool)x;
			}
			set { ViewState["AxisChanged"] = value; }			
		}
		
		/// <summary>
		/// Gets or sets the value that determines the output format for the control, in the
		/// form of a <see cref="ZedGraphWebFormat"/> enumeration.  This is typically Gif, Jpeg,
		/// Png, or Icon.
		/// </summary>
		/// <value>A <see cref="ZedGraphWebFormat"/> enumeration.</value>
		[Bindable(true),Category("Behavior"),NotifyParentProperty(true),DefaultValue("Jpeg")]
		public ZedGraphWebFormat OutputFormat
		{
			get 
			{ 
				object x = ViewState["OutputFormat"]; 
				return (null == x) ? ZedGraphWebFormat.Jpeg : (ZedGraphWebFormat)x;
			}
			set { ViewState["OutputFormat"] = value; }						
		}	
	
		/// <summary>
		/// Gets or sets the value of the <see cref="GraphPane.ClusterScaleWidth"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Behavior")]
		public double ClusterScaleWidth
		{
			get 
			{ 
				object x = ViewState["ClusterScaleWidth"]; 
				return (null == x) ? GraphPane.Default.ClusterScaleWidth : (double)x;
			}
			set { ViewState["ClusterScaleWidth"] = value; }
		} 

		/// <summary>
		/// Gets or sets the value of the <see cref="GraphPane.BarType"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Appearance")]
		public BarType BarType
		{
			get 
			{ 
				object x = ViewState["BarType"]; 
				return (null == x) ? GraphPane.Default.BarType : (BarType)x;
			}
			set { ViewState["BarType"] = value; }
		} 

	#endregion

	#region CurveList Property	
	
		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebCurveCollection CurveList
		{
			get { return (ZedGraphWebCurveCollection)vsassist.GetValue(this,'c'); }
		}
		#endregion

#if NOTREADY_FOR_PRODUCTION

	#region GraphItemList Property		
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebGraphItemCollection GraphItemList
		{
			get {  return (ZedGraphWebGraphItemCollection)vsassist.GetValue(this,'g'); }				
		}
		#endregion		

	#region FontSpec Property		
		/// <summary>
		/// <seealso cref="ZedGraph.ZedGraphWebFontSpec"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec FontSpec
		{
			get { return (ZedGraphWebFontSpec)vsassist.GetValue(this,'s'); }
		}
		#endregion

	#region AxisBorder Property		
		/// <summary>
		/// <seealso cref="ZedGraphWebBorder"/>
		/// </summary>
		[		
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder AxisBorder
		{
			get { return (ZedGraphWebBorder)vsassist.GetValue(this,'b'); }
		}
		#endregion

	#region AxisFill Property		
		/// <summary>
		/// <seealso cref="ZedGraphWebFill"/>
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill AxisFill
		{
			get { return (ZedGraphWebFill)vsassist.GetValue(this,'f'); }
		}
		#endregion

	#region XAxis Property		
		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.XAxis"/>
		/// </summary>
		[		
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebAxis XAxis
		{
			get { return (ZedGraphWebAxis)vsassist.GetValue(this,'x'); }
		}
		#endregion

	#region YAxis Property		
		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.YAxis"/>
		/// </summary>
		[		
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebAxis YAxis
		{
			get { return (ZedGraphWebAxis)vsassist.GetValue(this,'y'); }
		}
		#endregion

	#region Y2Axis Property		
		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.Y2Axis"/>
		/// </summary>
		[		
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebAxis Y2Axis
		{
			get { return (ZedGraphWebAxis)vsassist.GetValue(this,'z'); }
		}
		#endregion

	#region Legend Property		
		/// <summary>
		/// <seealso cref="ZedGraph.GraphPane.Legend"/>
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebLegend Legend
		{
			get { return (ZedGraphWebLegend)vsassist.GetValue(this,'l'); }
		}
		#endregion
#endif

	#region Event Handlers
		/// <summary>
		/// Sets the rendering event handler.
		/// </summary>
		/// <value>An event type for the RenderGraph event</value>
		[ Category("Action") ]
		public event ZedGraphWebControlEventHandler RenderGraph
		{
			add { Events.AddHandler( _eventRender, value ); }
			remove { Events.RemoveHandler( _eventRender, value ); }
		}

		private static readonly object _eventRender = new object();

		/// <summary>
		/// stub method that passes control for the render event to the the registered
		/// event handler.
		/// </summary>
		protected virtual void OnDrawPane( Graphics g, GraphPane pane )
		{
			ZedGraphWebControlEventHandler handler;
			handler = (ZedGraphWebControlEventHandler) Events[_eventRender];
			if ( handler != null )
			{
				try
				{
					handler( g, pane );
				}
				catch(Exception)
				{
					//TODO: what now, callback for errors? ;)
				}
			}
			else if (CurveList.Count == 0 )
			{// default with the sample graph if no callback provided
				ZedGraphWeb.RenderDemo(g,pane);
			}
		}		
	#endregion
		
	#region Map Embedded Content

		/// <summary>
		/// transfers values from a <see cref="ZedGraphWebCurveItem"/> instance to a <see cref="CurveItem"/> instance
		/// </summary>
		/// <param name="curve"><see cref="CurveItem"/></param>
		/// <param name="web"><see cref="ZedGraphWebCurveItem"/></param>
		protected void MapWeb2GraphItem( CurveItem curve, ZedGraphWebCurveItem web )
		{
			curve.Color = web.Color;
			curve.IsLegendLabelVisible = web.IsLegendLabelVisible;
			curve.IsVisible = web.IsVisible;
			curve.IsY2Axis = web.IsY2Axis;
			curve.Label = web.Label;
		}

		/// <summary>
		/// Transfers values from a <see cref="ZedGraphWebBorder"/> instance to a <see cref="Border"/> instance.
		/// </summary>
		/// <param name="item"><see cref="Border"/></param>
		/// <param name="web"><see cref="ZedGraphWebBorder"/></param>
		protected void MapWeb2GraphItem( Border item, ZedGraphWebBorder web )
		{
			item.Color = web.Color;
			item.IsVisible = web.IsVisible;
			item.PenWidth = web.PenWidth;
		}

		/// <summary>
		/// Transfers values from a <see cref="ZedGraphWebFill"/> instance to a <see cref="Fill"/> instance.
		/// </summary>
		/// <param name="item"><see cref="Fill"/></param>
		/// <param name="web"><see cref="ZedGraphWebFill"/></param>
		protected void MapWeb2GraphItem( Fill item, ZedGraphWebFill web )
		{
			item = new Fill();			
			item.AlignH = web.AlignH;
			item.AlignV = web.AlignV;			
			//TODO: item.Brush = web.Brush
			item.Color = web.Color;
			item.IsScaled = web.IsScaled;
			item.IsVisible = web.IsVisible;
			item.RangeMax = web.RangeMax;
			item.RangeMin = web.RangeMin;
			item.Type = web.Type;
		}

		/// <summary>
		/// Transfers values from a <see cref="ZedGraphWebSymbol"/> instance to a <see cref="Symbol"/> instance.
		/// </summary>
		/// <param name="item"><see cref="Symbol"/></param>
		/// <param name="web"><see cref="ZedGraphWebSymbol"/></param>
		protected void MapWeb2GraphItem( Symbol item, ZedGraphWebSymbol web )
		{
			MapWeb2GraphItem(item.Border,web.Border);
			MapWeb2GraphItem(item.Fill,web.Fill);
			item.IsVisible = web.IsVisible;
			item.Size = web.Size;
			item.Type = web.Type;
		}

		/// <summary>
		/// Adds content to the <see cref="GraphPane"/> instance based on the web controls state elements.
		/// This requires applying each <see cref="ZedGraphWebCurveItem"/> to the <see cref="GraphPane"/> 
		/// including all the values and sub objects.
		/// </summary>
		/// <param name="g"><see cref="Graphics"/></param>
		/// <param name="pane"><see cref="GraphPane"/></param>
		protected void MapWebContent( Graphics g, GraphPane pane )
		{
			pane.IsShowTitle = this.IsShowTitle;
			pane.BarType = this.BarType;
			pane.ClusterScaleWidth = this.ClusterScaleWidth;
			
			ZedGraphWebCurveItem curve;
			for (int i=0; i<CurveList.Count; i++)
			{
				curve = CurveList[i];
				if ( curve is ZedGraphWebBarItem )
				{
					ZedGraphWebBarItem item = (ZedGraphWebBarItem)curve;
					BarItem x = pane.AddBar(item.Label,new PointPairList(),item.Color);
					MapWeb2GraphItem((CurveItem)x,(ZedGraphWebCurveItem)item);
					MapWeb2GraphItem(x.Bar.Border,item.Border);
					MapWeb2GraphItem(x.Bar.Fill,item.Fill);
				}
				else if ( curve is ZedGraphWebLineItem )
				{
					ZedGraphWebLineItem item = (ZedGraphWebLineItem)curve;
					LineItem x = pane.AddCurve(item.Label,new PointPairList(),item.Color);
					MapWeb2GraphItem((CurveItem)x,(ZedGraphWebCurveItem)item);
					MapWeb2GraphItem(x.Symbol,item.Symbol);
					x.Line.Style = item.Style;
					x.Line.Width = item.Width;
					x.Line.IsSmooth = item.IsSmooth;
					x.Line.SmoothTension = item.SmoothTension;
					x.Line.StepType = item.StepType;
					MapWeb2GraphItem(x.Line.Fill,item.Fill);
				}
				else if ( curve is ZedGraphWebErrorBarItem )
				{
					ZedGraphWebErrorBarItem item = (ZedGraphWebErrorBarItem)curve;
					ErrorBarItem x = pane.AddErrorBar(item.Label,new PointPairList(),item.Color);
					MapWeb2GraphItem((CurveItem)x,(ZedGraphWebCurveItem)item);					
					MapWeb2GraphItem(x.ErrorBar.Symbol,item.Symbol);
					x.BarBase = item.BarBase;
					x.ErrorBar.PenWidth = item.PenWidth;
				}
				else if ( curve is ZedGraphWebHiLowBarItem )
				{
					ZedGraphWebHiLowBarItem item = (ZedGraphWebHiLowBarItem)curve;
					HiLowBarItem x = pane.AddHiLowBar(item.Label,new PointPairList(),item.Color);
					MapWeb2GraphItem((CurveItem)x,(ZedGraphWebCurveItem)item);									
					MapWeb2GraphItem(x.Bar.Border,item.Border);
					MapWeb2GraphItem(x.Bar.Fill,item.Fill);					
					x.BarBase = item.BarBase;
					x.Bar.Size = item.Size;
					x.Bar.IsMaximumWidth = item.IsMaximumWidth;
				}
				else if ( curve is ZedGraphWebPieItem )
				{
					ZedGraphWebPieItem item = (ZedGraphWebPieItem)curve;
					PieItem x = pane.AddPieSlice(item.Value, item.Color, item.Displacement, item.Label);					
					MapWeb2GraphItem(x.Border,item.Border);					
					x.LabelType = item.LabelType;
					//x.PieType = item.PieType;
				}
			}
		}
		#endregion

	#region Render Methods

		/// <summary>
		/// Calls the Draw() method for the control.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> reference for the event.</param>
		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );		
			Draw( true );
		}
		
		/// <summary>
		/// Method to create a <see cref="ZedGraph.GraphPane"/> class for the control.
		/// </summary>
		/// <param name="OutputStream">A <see cref="Stream"/> in which to output the ZedGraph
		/// <see cref="System.Drawing.Image"/>.</param>
		protected void CreateGraph( System.IO.Stream OutputStream )
		{
			//TODO: fix/verify the height/width values are okay like this
			RectangleF rect = new RectangleF( 0, 0, this.Width-1, this.Height-1 );
			GraphPane pane = new GraphPane( rect, Title, string.Empty, string.Empty );
												
			Bitmap image = new Bitmap( this.Width, this.Height ); 			
			Graphics g = Graphics.FromImage( image );		

			//add visual designer influences here - first!!
			MapWebContent(g,pane);

			// Use callback to gather more settings and data values
			OnDrawPane( g, pane );			

			// Allow designer control of axischange
			if ( this.AxisChanged ) pane.AxisChange(g);
			
			// Render the graph to a bitmap
			g.Clear(Color.FromArgb(255, 255, 255, 255)); 
			pane.Draw( g ); 
        
			// Stream the graph out				
			MemoryStream ms = new MemoryStream(); 
			image.Save( ms, this.ImageFormat );				

			//TODO: provide caching options
			ms.WriteTo(OutputStream);
		}

		/// <summary>
		/// Override the Render() method with a do-nothing method.
		/// </summary>
		/// <param name="output"></param>
		protected override void Render( HtmlTextWriter output )
		{	
			//not used
		}

		/// <summary>
		/// Draws graph on HttpResponse object
		/// </summary>
		/// <param name="end"></param>
		public void Draw( bool end )
		{
			System.Web.HttpContext ctx = System.Web.HttpContext.Current;
			if ( null == ctx )
				throw new Exception( "missing context object" );
			CreateGraph( ctx.Response.OutputStream );
			ctx.Response.ContentType = this.ContentType;
			if ( end )
				ctx.Response.End();
		}

		/// <summary>
		/// Draws graph on stream object
		/// </summary>
		/// <param name="stream"></param>
		public void Draw( System.IO.Stream stream )
		{
			if ( null == stream )
				throw new Exception( "stream parameter cannot be null" );
			CreateGraph( stream );
		}

	#endregion

	#region Internal Output Type Helpers

		/// <summary>
		/// An enumeration type that defines the output image types supported by
		/// the ZedGraph Web control.
		/// </summary>
		public enum ZedGraphWebFormat
		{
			/// <summary>
			/// The Gif bitmap format (CompuServe)
			/// </summary>
			Gif,
			/// <summary>
			/// The JPEG format
			/// </summary>
			Jpeg,
			/// <summary>
			/// A windows Icon format
			/// </summary>
			Icon,
			/// <summary>
			/// The portable network graphics format
			/// </summary>
			Png
		}	

		/// <summary>
		/// Gets the <see cref="OutputFormat"/> property, translated to an
		/// <see cref="ImageFormat"/> enumeration.
		/// </summary>
		/// <value>An <see cref="ImageFormat"/> enumeration representing the image type
		/// to be output.</value>
		protected ImageFormat ImageFormat
		{
			get
			{
				switch( OutputFormat )
				{
					case ZedGraphWebFormat.Gif:
						return ImageFormat.Gif;
					case ZedGraphWebFormat.Jpeg:
						return ImageFormat.Jpeg;
					case ZedGraphWebFormat.Icon:
						return ImageFormat.Icon;
					case ZedGraphWebFormat.Png:
						return ImageFormat.Png;
				}
				return ImageFormat.Gif;
			}
		}

		/// <summary>
		/// Gets the <see cref="OutputFormat"/> property, translated to an
		/// html content type string (such as "image/png").
		/// </summary>
		/// <value>A string representing the image type to be output.</value>
		protected string ContentType
		{
			get
			{
				switch( OutputFormat )
				{
					case ZedGraphWebFormat.Gif:
						return "image/gif";
					case ZedGraphWebFormat.Jpeg:
						return "image/jpeg";
					case ZedGraphWebFormat.Icon:
						return "image/icon";
					case ZedGraphWebFormat.Png:
						return "image/png";
				}
				return "image/gif";
			}
		}
	#endregion

	#region State Management	
	
		private GenericViewStateAssistant vsassist;

		/// <summary>
		/// Used by asp.net to load the viewstate values into the web control
		/// </summary>
		/// <param name="savedState">portable view state object</param>
		protected override void LoadViewState(object savedState) 
		{
			object state = vsassist.LoadViewState(this,savedState);
			if ( state != null ) base.LoadViewState(state);											

			/*
			if ( savedState == null ) return;
			object[] state = (object[])savedState;

			if (state[0] != null) base.LoadViewState(state[0]);
			if (state[1] != null) ((IStateManager)XAxis).LoadViewState(state[1]);
			if (state[2] != null) ((IStateManager)YAxis).LoadViewState(state[2]);
			if (state[3] != null) ((IStateManager)Y2Axis).LoadViewState(state[3]);
			if (state[4] != null) ((IStateManager)Legend).LoadViewState(state[4]);
			if (state[5] != null) ((IStateManager)AxisBorder).LoadViewState(state[5]);
			if (state[6] != null) ((IStateManager)AxisFill).LoadViewState(state[6]);
			if (state[7] != null) ((IStateManager)FontSpec).LoadViewState(state[7]);
			if (state[8] != null) ((IStateManager)CurveList).LoadViewState(state[8]);
			*/						
		}

		/// <summary>
		/// Used by asp.net to save the viewstate to the class instance given a portable state object.
		/// </summary>
		/// <returns>portable state object</returns>
		protected override object SaveViewState() 
		{			
			return vsassist.SaveViewState(base.SaveViewState());	
			/*
			object[] state = new object[9];
			state[0] = base.SaveViewState();
			state[1] = (xaxis==null) ? null : ((IStateManager)XAxis).SaveViewState();
			state[2] = (yaxis==null) ? null : ((IStateManager)YAxis).SaveViewState();
			state[3] = (y2axis==null) ? null : ((IStateManager)Y2Axis).SaveViewState();
			state[4] = (legend==null) ? null : ((IStateManager)Legend).SaveViewState();
			state[5] = (axisborder==null) ? null : ((IStateManager)AxisBorder).SaveViewState();
			state[6] = (axisfill==null) ? null : ((IStateManager)AxisFill).SaveViewState();
			state[7] = (fontspec==null) ? null : ((IStateManager)FontSpec).SaveViewState();
			state[8] = (curvelist==null) ? null : ((IStateManager)CurveList).SaveViewState();			
			return state;
			*/
		}

		/// <summary>
		/// Used by asp.net to inform the viewstate to start tracking changes.
		/// </summary>
		protected override void TrackViewState() 
		{	
			base.TrackViewState();
			vsassist.TrackViewState();
			/*
			if (xaxis!=null)     ((IStateManager)xaxis).TrackViewState();
			if (yaxis!=null)     ((IStateManager)yaxis).TrackViewState();
			if (y2axis!=null)    ((IStateManager)y2axis).TrackViewState();
			if (legend!=null)    ((IStateManager)legend).TrackViewState();
			if (axisborder!=null)((IStateManager)axisborder).TrackViewState();
			if (axisfill!=null)  ((IStateManager)axisfill).TrackViewState();
			if (fontspec!=null)  ((IStateManager)fontspec).TrackViewState();
			if (curvelist!=null) ((IStateManager)curvelist).TrackViewState();
			*/
		}
		#endregion
	}


	
	/// <summary>
	/// A delegate to handle the rendering event for this control.
	/// </summary>
	/// <param name="g">A <see cref="Graphics"/> object for which the drawing will be done.</param>
	/// <param name="pane">A reference to the <see cref="GraphPane"/>
	/// class to be rendered.</param>
	public delegate void ZedGraphWebControlEventHandler( Graphics g, GraphPane pane );
}
