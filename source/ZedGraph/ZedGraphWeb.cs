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
	/// <version> $Revision: 3.21 $ $Date: 2005-03-17 04:32:12 $ </version>
	[	
	ParseChildren(true),
	PersistChildren(false),
	//DefaultProperty("Title"),
	ToolboxData("<{0}:ZedGraphWeb runat=server></{0}:ZedGraphWeb>")
	]
	public class ZedGraphWeb : Control, INamingContainer, IDisposable
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
			vsassist.Register('B',typeof(ZedGraphWebBorder));
			vsassist.Register('F',typeof(ZedGraphWebFill));
			vsassist.Register('s',typeof(ZedGraphWebFontSpec));
			vsassist.Register('c',typeof(ZedGraphWebCurveCollection));
			vsassist.Register('g',typeof(ZedGraphWebGraphItemCollection));
			vsassist.Register('r',typeof(ZedGraphWebRect));
			vsassist.Register('R',typeof(ZedGraphWebRect));	

			this.AxisBorder.Color = ZedGraph.GraphPane.Default.AxisBorderColor;
			this.AxisBorder.PenWidth = ZedGraph.GraphPane.Default.AxisBorderPenWidth;
			this.AxisBorder.IsVisible = true;
			this.AxisFill.Brush = ZedGraph.GraphPane.Default.AxisBackBrush;
			this.AxisFill.Color = ZedGraph.GraphPane.Default.AxisBackColor;
			this.AxisFill.Type  = ZedGraph.GraphPane.Default.AxisBackType;
		
			this.PaneBorder.Color = ZedGraph.PaneBase.Default.BorderColor;
			this.PaneBorder.PenWidth = ZedGraph.PaneBase.Default.BorderPenWidth;
			this.PaneBorder.IsVisible = true;
			this.PaneFill.Color = ZedGraph.PaneBase.Default.FillColor;

			this.FontSpec.IsBold = ZedGraph.PaneBase.Default.FontBold;
			this.FontSpec.FontColor = ZedGraph.PaneBase.Default.FontColor;
			this.FontSpec.Family = ZedGraph.PaneBase.Default.FontFamily;
			this.FontSpec.IsItalic = ZedGraph.PaneBase.Default.FontItalic;
			this.FontSpec.Size = ZedGraph.PaneBase.Default.FontSize;
			this.FontSpec.IsUnderline = ZedGraph.PaneBase.Default.FontUnderline;
			this.FontSpec.Fill.Type = FillType.None;	// no fill
			
			this.XAxis.IsVisible = ZedGraph.XAxis.Default.IsVisible;
			this.XAxis.IsZeroLine = ZedGraph.XAxis.Default.IsZeroLine;

			this.YAxis.IsVisible = ZedGraph.YAxis.Default.IsVisible;
			this.YAxis.IsZeroLine = ZedGraph.YAxis.Default.IsZeroLine;

			this.Y2Axis.IsVisible = ZedGraph.Y2Axis.Default.IsVisible;
			this.Y2Axis.IsZeroLine = ZedGraph.Y2Axis.Default.IsZeroLine;
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
			// Set the titles and axis labels
			pane.Title = "Wacky Widget Company\nProduction Report";
			pane.XAxis.Title = "Time, Days\n(Since Plant Construction Startup)";
			pane.YAxis.Title = "Widget Production\n(units/hour)";
			
			LineItem curve;

			// Set up curve "Larry"
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			// Use green, with circle symbols
			curve = pane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve with a white-green gradient
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50), 90F );
			// Make it a smooth line
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			// Fill the symbols with white
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
			
			// Second curve is "moe"
			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			// Use a red color with triangle symbols
			curve = pane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve with semi-transparent pink using the alpha value
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205), 90F );
			// Fill the symbols with white
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;
			
			// Third Curve is a bar, called "Wheezy"
			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = pane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			// Fill the bars with a RosyBrown-White-RosyBrown gradient
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );

			// Fourth curve is a bar
			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = pane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			// Fill the bars with a RoyalBlue-White-RoyalBlue gradient
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			
			// Fill the pane background with a gradient
			pane.PaneFill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			// Fill the axis background with a gradient
			pane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245),
				Color.FromArgb( 255, 255, 190), 90F );
			

			// Make each cluster 100 user scale units wide.  This is needed because the X Axis
			// type is Linear rather than Text or Ordinal
			pane.ClusterScaleWidth = 100;
			// Bars are stacked
			pane.BarType = BarType.Stack;

			// Enable the X and Y axis grids
			pane.XAxis.IsShowGrid = true;
			pane.YAxis.IsShowGrid = true;

			// Manually set the scale maximums according to user preference
			pane.XAxis.Max = 1200;
			pane.YAxis.Max = 120;
			
			// Add a text item to decorate the graph
			TextItem text = new TextItem("First Prod\n21-Oct-93", 175F, 80.0F );
			// Align the text such that the Bottom-Center is at (175, 80) in user scale coordinates
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Near;
			pane.GraphItemList.Add( text );

			// Add an arrow pointer for the above text item
			ArrowItem arrow = new ArrowItem( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			pane.GraphItemList.Add( arrow );

			// Add a another text item to to point out a graph feature
			text = new TextItem("Upgrade", 700F, 50.0F );
			// rotate the text 90 degrees
			text.FontSpec.Angle = 90;
			// Align the text such that the Right-Center is at (700, 50) in user scale coordinates
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			// Disable the border and background fill options for the text
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			pane.GraphItemList.Add( text );

			// Add an arrow pointer for the above text item
			arrow = new ArrowItem( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			pane.GraphItemList.Add( arrow );

			// Add a text "Confidential" stamp to the graph
			text = new TextItem("Confidential", 0.85F, -0.03F );
			// use AxisFraction coordinates so the text is placed relative to the AxisRect
			text.Location.CoordinateFrame = CoordType.AxisFraction;
			// rotate the text 15 degrees
			text.FontSpec.Angle = 15.0F;
			// Text will be red, bold, and 16 point
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			// Disable the border and background fill options for the text
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			// Align the text such the the Left-Bottom corner is at the specified coordinates
			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			pane.GraphItemList.Add( text );

			// Add a BoxItem to show a colored band behind the graph data
			BoxItem box = new BoxItem( new RectangleF( 0, 110, 1200, 10 ),
				Color.Empty, Color.FromArgb( 225, 245, 225) );
			box.Location.CoordinateFrame = CoordType.AxisXYScale;
			// Align the left-top of the box to (0, 110)
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			// place the box behind the axis items, so the grid is drawn on top of it
			box.ZOrder = ZOrder.E_BehindAxis;
			pane.GraphItemList.Add( box );
			
			// Add some text inside the above box to indicate "Peak Range"
			TextItem myText = new TextItem( "Peak Range", 1170, 105 );
			myText.Location.CoordinateFrame = CoordType.AxisXYScale;
			myText.Location.AlignH = AlignH.Right;
			myText.Location.AlignV = AlignV.Center;
			myText.FontSpec.IsItalic = true;
			myText.FontSpec.IsBold = false;
			myText.FontSpec.Fill.IsVisible = false;
			myText.FontSpec.Border.IsVisible = false;
			pane.GraphItemList.Add( myText );

			pane.AxisChange( g );
		}
		#endregion

	#region Attributes

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		[Category("Page Cache"),NotifyParentProperty(true),
		Description("Optional output caching parameter in seconds. A zero value ignores cache settings. " +
			"For more advanced caching see microsoft documentation")]
		public int CacheDuration
		{
			get 
			{ 
				object x = ViewState["CacheDuration"]; 
				return (null == x) ? 0 : (int)x;
			}
			set { ViewState["CacheDuration"] = value; }
		}

		/// <summary>
		/// The <see cref="String"/> name of the data member that contains the data to be
		/// bound to the graph.
		/// </summary>
		[Category("Data"),NotifyParentProperty(true),
		Description("Optional binding member name for populating curve items with values")]
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
		/// The object reference that points to a data source from which to bind curve data.
		/// </summary>
		[Bindable(true),Category("Data"),NotifyParentProperty(true)]
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
		/// Proxy property that gets or sets the value of the <see cref="PaneBase.BaseDimension"/>.
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
		/// Proxy property that gets or sets the width of the <see cref="PaneBase.PaneRect"/>.
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
		/// Proxy property that gets or sets the height of the <see cref="PaneBase.PaneRect"/>.
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
		/// Proxy property that gets or sets the Title of the <see cref="ZedGraph.GraphPane"/>.
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
		/// Proxy property that gets or sets the value of <see cref="PaneBase.IsShowTitle"/>, which
		/// determines if the <see cref="ZedGraph.PaneBase.Title"/> is visible.
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
		/// Proxy property that gets or sets the value of <see cref="GraphPane.IsIgnoreInitial"/>.
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
		/// Proxy property that gets or sets the value of <see cref="GraphPane.IsIgnoreMissing"/>.
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
		/// Proxy property that gets or sets the value of <see cref="PaneBase.IsFontsScaled"/>.
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
		/// Proxy property that gets or sets the value of <see cref="GraphPane.BarBase"/>.
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
		/// Proxy property that gets or sets the value of <see cref="GraphPane.IsAxisRectAuto"/>.
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
		/// Proxy property that gets or sets the value of <see cref="PaneBase.IsPenWidthScaled"/>.
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
		/// Gets or sets a boolean flag value that, if true, will cause the
		/// <see cref="ZedGraph.GraphPane.AxisChange"/> method to be called when
		/// <see cref="CreateGraph"/> is called.
		/// </summary>
		/// <value>A boolean value, true to call <see cref="GraphPane.AxisChange"/>, false otherwise</value>
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
		/// Proxy property that gets or sets the value that determines the output format for the control, in the
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
		/// Proxy property that gets or sets the value of the <see cref="GraphPane.ClusterScaleWidth"/>.
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
		/// Proxy property that gets or sets the value of the <see cref="GraphPane.BarType"/>.
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

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="GraphPane.LineType"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Appearance")]
		public LineType LineType
		{
			get 
			{ 
				object x = ViewState["LineType"]; 
				return (null == x) ? GraphPane.Default.LineType : (LineType)x;
			}
			set { ViewState["LineType"] = value; }
		} 
	
		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="GraphPane.MinClusterGap"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Appearance")]
		public float MinClusterGap
		{
			get 
			{ 
				object x = ViewState["MinClusterGap"]; 
				return (null == x) ? GraphPane.Default.MinClusterGap : (float)x;
			}
			set { ViewState["MinClusterGap"] = value; }
		} 

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="GraphPane.MinBarGap"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Appearance")]
		public float MinBarGap
		{
			get 
			{ 
				object x = ViewState["MinBarGap"]; 
				return (null == x) ? GraphPane.Default.MinBarGap : (float)x;
			}
			set { ViewState["MinBarGap"] = value; }
		} 

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="PaneBase.MarginLeft"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Pane")]
		public float MarginLeft
		{
			get 
			{ 
				object x = ViewState["MarginLeft"]; 
				return (null == x) ? ZedGraph.PaneBase.Default.MarginLeft : (float)x;
			}
			set { ViewState["MarginLeft"] = value; }
		} 

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="PaneBase.MarginRight"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Pane")]
		public float MarginRight
		{
			get 
			{ 
				object x = ViewState["MarginRight"]; 
				return (null == x) ? ZedGraph.PaneBase.Default.MarginRight : (float)x;
			}
			set { ViewState["MarginRight"] = value; }
		} 

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="PaneBase.MarginTop"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Pane")]
		public float MarginTop
		{
			get 
			{ 
				object x = ViewState["MarginTop"]; 
				return (null == x) ? ZedGraph.PaneBase.Default.MarginTop : (float)x;
			}
			set { ViewState["MarginTop"] = value; }
		} 

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="PaneBase.MarginBottom"/>.
		/// </summary>
		[NotifyParentProperty(true),Category("Pane")]
		public float MarginBottom
		{
			get 
			{ 
				object x = ViewState["MarginBottom"]; 
				return (null == x) ? ZedGraph.PaneBase.Default.MarginBottom : (float)x;
			}
			set { ViewState["MarginBottom"] = value; }
		} 
	
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.CurveList"/>.
		/// </summary>
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebCurveCollection CurveList
		{
			get { return (ZedGraphWebCurveCollection)vsassist.GetValue('c',this.IsTrackingViewState); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.GraphItemList"/>.
		/// </summary>
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebGraphItemCollection GraphItemList
		{
			get {  return (ZedGraphWebGraphItemCollection)vsassist.GetValue('g',this.IsTrackingViewState); }				
		}
							
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.AxisRect"/>.
		/// </summary>
		[
		Category("Axis"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebRect AxisRect
		{
			get { return (ZedGraphWebRect)vsassist.GetValue('r',this.IsTrackingViewState); }
		}
		
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.PieRect"/>.
		/// </summary>
		[
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebRect PieRect
		{
			get { return (ZedGraphWebRect)vsassist.GetValue('R',this.IsTrackingViewState); }
		}		
				
		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.FontSpec"/>.
		/// </summary>
		[
		Category("Pane"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFontSpec FontSpec
		{
			get { return (ZedGraphWebFontSpec)vsassist.GetValue('s',this.IsTrackingViewState); }
		}
			
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.AxisBorder"/>.
		/// </summary>
		[		
		Category("Axis"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder AxisBorder
		{
			get { return (ZedGraphWebBorder)vsassist.GetValue('b',this.IsTrackingViewState); }
		}
				
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.AxisFill"/>.
		/// </summary>
		[
		Category("Axis"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill AxisFill
		{
			get { return (ZedGraphWebFill)vsassist.GetValue('f',this.IsTrackingViewState); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.PaneBorder"/>.
		/// </summary>
		[		
		Category("Pane"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebBorder PaneBorder
		{
			get { return (ZedGraphWebBorder)vsassist.GetValue('B',this.IsTrackingViewState); }
		}
				
		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.PaneFill"/>.
		/// </summary>
		[
		Category("Pane"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebFill PaneFill
		{
			get { return (ZedGraphWebFill)vsassist.GetValue('F',this.IsTrackingViewState); }
		}
			
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.XAxis"/>.
		/// </summary>
		[		
		Category("Axis"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebAxis XAxis
		{
			get { return (ZedGraphWebAxis)vsassist.GetValue('x',this.IsTrackingViewState); }
		}
			
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.YAxis"/>.
		/// </summary>
		[		
		Category("Axis"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebAxis YAxis
		{
			get { return (ZedGraphWebAxis)vsassist.GetValue('y',this.IsTrackingViewState); }
		}
			
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.Y2Axis"/>.
		/// </summary>
		[		
		Category("Axis"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebAxis Y2Axis
		{
			get { return (ZedGraphWebAxis)vsassist.GetValue('z',this.IsTrackingViewState); }
		}
					
		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.Legend"/>.
		/// </summary>
		[	
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebLegend Legend
		{
			get { return (ZedGraphWebLegend)vsassist.GetValue('l',this.IsTrackingViewState); }
		}
	#endregion

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

			if ( (handler == null) && (CurveList.Count == 0) && (GraphItemList.Count == 0) )
			{
				// default with the sample graph if no callback provided
				ZedGraphWeb.RenderDemo(g,pane);
			}
			else
			{
				try
				{
					// Add visual designer influences here - first!!
					MapWebContent(g,pane);

					// Add DataSource values if available before the callback
					PopulateByDataSource(g,pane);

					// Add custom callback tweeking next
					handler( g, pane );
				}
				catch(Exception)
				{
					//TODO: what now, callback for errors? ;)
				}
			}
		}		
	#endregion
		
	#region Map Embedded Content

		/// <summary>
		/// Adds content to the <see cref="GraphPane"/> instance based on the web controls state elements.
		/// This requires applying each <see cref="ZedGraphWebCurveItem"/> to the <see cref="GraphPane"/> 
		/// including all the values and sub objects.
		/// </summary>
		/// <param name="g"><see cref="Graphics"/></param>
		/// <param name="pane"><see cref="GraphPane"/></param>
		protected void MapWebContent( Graphics g, GraphPane pane )
		{
			try 
			{
				pane.IsShowTitle = this.IsShowTitle;
				pane.BarType = this.BarType;
				pane.ClusterScaleWidth = this.ClusterScaleWidth;								
				XAxis.CopyTo( pane.XAxis );
				YAxis.CopyTo( pane.YAxis );
				Y2Axis.CopyTo( pane.Y2Axis );				
				pane.IsIgnoreInitial = this.IsIgnoreInitial;
				pane.IsIgnoreMissing = this.IsIgnoreMissing;
				pane.LineType = this.LineType;
				this.AxisRect.CopyTo(pane.AxisRect);
				pane.IsAxisRectAuto = this.IsAxisRectAuto;
				this.PieRect.CopyTo(pane.PieRect);
				this.AxisBorder.CopyTo(pane.AxisBorder);
				this.AxisFill.CopyTo(pane.AxisFill);
				pane.MinClusterGap = this.MinClusterGap;
				pane.MinBarGap = this.MinBarGap;
				pane.BarBase = this.BarBase;				
				this.Legend.CopyTo(pane.Legend);
				this.FontSpec.CopyTo(pane.FontSpec);
				pane.Title = this.Title;
				this.PaneBorder.CopyTo(pane.PaneBorder);
				this.PaneFill.CopyTo(pane.PaneFill);
				pane.MarginLeft = this.MarginLeft;
				pane.MarginRight = this.MarginRight;
				pane.MarginTop = this.MarginTop;
				pane.MarginBottom = this.MarginBottom;
				pane.BaseDimension = this.BaseDimension;
				pane.IsFontsScaled = this.IsFontsScaled;
				pane.IsPenWidthScaled = this.IsPenWidthScaled;
			}
			catch(Exception)
			{
				//base mapping
			}

			try
			{
				ZedGraphWebCurveItem curve;
				for (int i=0; i<CurveList.Count; i++)
				{
					curve = CurveList[i];
					if ( curve is ZedGraphWebBarItem )
					{
						ZedGraphWebBarItem item = (ZedGraphWebBarItem)curve;
						BarItem x = pane.AddBar(item.Label,new PointPairList(),item.Color);
						item.CopyTo(x);					
					}
					else if ( curve is ZedGraphWebLineItem )
					{
						ZedGraphWebLineItem item = (ZedGraphWebLineItem)curve;
						LineItem x = pane.AddCurve(item.Label,new PointPairList(),item.Color);
						item.CopyTo(x);
					}
					else if ( curve is ZedGraphWebErrorBarItem )
					{
						ZedGraphWebErrorBarItem item = (ZedGraphWebErrorBarItem)curve;
						ErrorBarItem x = pane.AddErrorBar(item.Label,new PointPairList(),item.Color);
						item.CopyTo(x);
					}
					else if ( curve is ZedGraphWebHiLowBarItem )
					{
						ZedGraphWebHiLowBarItem item = (ZedGraphWebHiLowBarItem)curve;
						HiLowBarItem x = pane.AddHiLowBar(item.Label,new PointPairList(),item.Color);
						item.CopyTo(x);
					}
					else if ( curve is ZedGraphWebPieItem )
					{
						ZedGraphWebPieItem item = (ZedGraphWebPieItem)curve;
						PieItem x = pane.AddPieSlice(item.Value, item.Color, item.Displacement, item.Label);					
						item.CopyTo(x);
					}				
				}
			}
			catch(Exception)
			{
				//curveitems
			}
						
			try
			{
				ZedGraphWebGraphItem draw;
				for (int i=0; i<GraphItemList.Count; i++)
				{
					draw = GraphItemList[i];
					if ( draw is ZedGraphWebTextItem )
					{
						ZedGraphWebTextItem item = (ZedGraphWebTextItem)draw;
						TextItem x = new TextItem();
						item.CopyTo(x);
						pane.GraphItemList.Add(x);
					}
					else if ( draw is ZedGraphWebArrowItem )
					{
						ZedGraphWebArrowItem item = (ZedGraphWebArrowItem)draw;					
						ArrowItem x = new ArrowItem();
						item.CopyTo(x);
						pane.GraphItemList.Add(x);
					}
					else if ( draw is ZedGraphWebImageItem )
					{
						ZedGraphWebImageItem item = (ZedGraphWebImageItem)draw;					
						ImageItem x = new ImageItem();
						item.CopyTo(x);
						pane.GraphItemList.Add(x);
					}
					else if ( draw is ZedGraphWebBoxItem )
					{
						ZedGraphWebBoxItem item = (ZedGraphWebBoxItem)draw;					
						BoxItem x = new BoxItem();
						item.CopyTo(x);
						pane.GraphItemList.Add(x);
					}
					else if ( draw is ZedGraphWebEllipseItem )
					{
						ZedGraphWebEllipseItem item = (ZedGraphWebEllipseItem)draw;					
						EllipseItem x = new EllipseItem();
						item.CopyTo(x);
						pane.GraphItemList.Add(x);
					}
				}
			}
			catch(Exception)
			{
				//graphitems
			}						
		}
		#endregion

	#region Process DataSource

		/// <summary>
		/// Provides binding between <see cref="DataSource"/> and the specified pane.  Extracts the
		/// data from <see cref="DataSource"/> and copies it into the appropriate
		/// <see cref="ZedGraph.PointPairList"/> for each <see cref="ZedGraph.CurveItem"/> in the
		/// specified <see cref="ZedGraph.GraphPane"/>.
		/// </summary>
		/// <param name="g">The <see cref="Graphics"/> object to be used for rendering the data.</param>
		/// <param name="pane">The <see cref="ZedGraph.GraphPane"/> object which will receive the data.</param>
		protected void PopulateByDataSource( Graphics g, GraphPane pane )
		{
			if ( this.DataSource == null ) return;

			//TODO: verify datasource type and count
			//TODO: match column names to curveitems
			//TODO: foreach row populate columns
			
			//NOTE: ZedGraphWeb.DataMember = X (or Y if reversed?)
			//NOTE: ZedGraphCurveItem.DataMember = Y (or X if reversed?)
			//NOTE: Z values are only supported via the callback

			//TODO: cache the data-map table before processing rows

			/*
			while ( false == true )
			{
				double x,y,z = 0;
				//TODO: match incoming datatype with expected datatype
				pane.CurveList[0].Points.Add(x,y,z);
			}
			*/

			try
			{
			}
			catch(Exception)
			{
				//databinding
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
				
			if ( this.CacheDuration > 0 )
			{
				System.Web.HttpContext context = System.Web.HttpContext.Current;
				if ( context != null )
				{
					System.Web.HttpCachePolicy policy = context.Response.Cache;
					if ( policy != null )
					{
						policy.SetExpires( DateTime.Now.AddSeconds(this.CacheDuration) );
					}
				}
			}
			
			Draw( true );
		}
		
		/// <summary>
		/// Method to create a <see cref="ZedGraph.GraphPane"/> class for the control.
		/// </summary>
		/// <param name="OutputStream">A <see cref="Stream"/> in which to output the ZedGraph
		/// <see cref="System.Drawing.Image"/>.</param>
		/// <param name="Format">The <see cref="ImageFormat"/> type to be output.</param>
		protected void CreateGraph( System.IO.Stream OutputStream, ImageFormat Format )
		{			
			RectangleF rect = new RectangleF( 0, 0, this.Width-1, this.Height-1 );
			GraphPane pane = new GraphPane( rect, Title, string.Empty, string.Empty );
												
			Bitmap image = new Bitmap( this.Width, this.Height ); 			
			Graphics g = Graphics.FromImage( image );					

			// Use callback to gather more settings and data values
			OnDrawPane( g, pane );			

			// Allow designer control of axischange
			if ( this.AxisChanged ) pane.AxisChange(g);
			
			// Render the graph to a bitmap
			g.Clear(Color.FromArgb(255, 255, 255, 255)); 
			pane.Draw( g ); 
        
			// Stream the graph out				
			MemoryStream ms = new MemoryStream(); 
			image.Save( ms, Format );							

			//TODO: provide caching options
			ms.WriteTo(OutputStream);
		}

		private System.IO.FileStream DesignTimeFileStream = null;

		/// <summary>
		/// Override the Render() method with a do-nothing method.
		/// </summary>
		/// <param name="output"></param>
		protected override void Render( HtmlTextWriter output )
		{	
			try
			{
				if ( null == DesignTimeFileStream )
				{					
					string path = Path.GetTempPath();
					string name = string.Empty;
					int pid = System.Diagnostics.Process.GetCurrentProcess().Id;
					string test;

					for ( int i=0; i<100; i++ )
					{
						test = string.Format("{0:X}-{1:X}.jpg",i,pid);
						test = Path.Combine(path,test);
						if ( !File.Exists(test) )
						{
							name = test;
							break;
						}
					}
					if ( name == string.Empty ) 
					{
						throw new Exception("unable to create temp file for ZedGraph rendering");
					}
					
					DesignTimeFileStream = new FileStream(name,FileMode.CreateNew,
						FileAccess.ReadWrite, FileShare.ReadWrite, 512);
				}

				DesignTimeFileStream.SetLength(0);
				DesignTimeFileStream.Seek(0,SeekOrigin.Begin);
				CreateGraph( DesignTimeFileStream, ImageFormat.Jpeg );			
				DesignTimeFileStream.Flush();
				
				string url = "file://" + DesignTimeFileStream.Name;
				output.AddAttribute(HtmlTextWriterAttribute.Width,this.Width.ToString());
				output.AddAttribute(HtmlTextWriterAttribute.Height,this.Height.ToString());
				output.AddAttribute(HtmlTextWriterAttribute.Src,url);
				output.AddAttribute(HtmlTextWriterAttribute.Alt,url);
				output.RenderBeginTag(HtmlTextWriterTag.Img);
				output.RenderEndTag();
			}
			catch(Exception e)
			{				
				output.AddAttribute(HtmlTextWriterAttribute.Width,this.Width.ToString());
				output.AddAttribute(HtmlTextWriterAttribute.Height,this.Height.ToString());				
				output.RenderBeginTag(HtmlTextWriterTag.Span);
				output.Write(e.ToString());
				output.RenderEndTag();
			}

			
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
			CreateGraph( ctx.Response.OutputStream, this.ImageFormat );
			ctx.Response.ContentType = this.ContentType;
			if ( end ) ctx.ApplicationInstance.CompleteRequest();
				
		}

		/// <summary>
		/// Draws graph on stream object
		/// </summary>
		/// <param name="stream"></param>
		public void Draw( System.IO.Stream stream )
		{
			if ( null == stream )
				throw new Exception( "stream parameter cannot be null" );
			CreateGraph( stream, this.ImageFormat );
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
			object state = vsassist.LoadViewState(savedState,this.IsTrackingViewState);
			if ( state != null ) base.LoadViewState(state);																			
		}

		/// <summary>
		/// Used by asp.net to save the viewstate to the class instance given a portable state object.
		/// </summary>
		/// <returns>portable state object</returns>
		protected override object SaveViewState() 
		{			
			return vsassist.SaveViewState(base.SaveViewState());	
		}

		/// <summary>
		/// Used by asp.net to inform the viewstate to start tracking changes.
		/// </summary>
		protected override void TrackViewState() 
		{	
			base.TrackViewState();
			vsassist.TrackViewState();			
		}
		#endregion

	#region IDisposable Members

		/// <summary>
		/// Free up resources associated with the FileStream
		/// </summary>
		public override void Dispose()
		{
			try
			{
				if ( DesignTimeFileStream != null )
				{
					string name = DesignTimeFileStream.Name;
					DesignTimeFileStream.Close();
					DesignTimeFileStream = null;
					File.Delete(name);
				}
			}
			catch(Exception)
			{
			}
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
