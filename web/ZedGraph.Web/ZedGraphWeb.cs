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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections;
using ZedGraph;

[assembly: TagPrefix( "ZedGraph", "zgw" )]

namespace ZedGraph.Web
{
	/// <summary>
	/// The ZedGraphWeb class provides a web control interface to the
	/// <see cref="ZedGraph"/> class library.  This allows ZedGraph to be used
	/// from a web page with ASP.net.  All graph
	/// attributes are accessible via the <see cref="ZedGraph.GraphPane"/>
	/// property.
	/// </summary>
	/// <author>Darren Martz revised by John Champion revised by Benjamin Mayrargue</author>
	/// <version>$Revision: 1.19 $ $Date: 2007-06-02 06:56:03 $</version>
	[
	ParseChildren( true ),
	PersistChildren( false ),
		//DefaultProperty("Title"),
	ToolboxData( "<{0}:ZedGraphWeb runat=server></{0}:ZedGraphWeb>" )
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

	#region Private Fields

		/// <summary>
		/// This private field contains duration (in hours) of a temp file generated 
		/// by control in mode "ImageTag"
		/// </summary>
		private double _tmpImageDuration = 12;

	#endregion

	#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ZedGraphWeb()
		{
			vsassist = new GenericViewStateAssistant();
			vsassist.Register( 'x', typeof( ZedGraphWebXAxis ) );
			vsassist.Register( 'y', typeof( ZedGraphWebYAxis ) );
			vsassist.Register( 'z', typeof( ZedGraphWebY2Axis ) );
			vsassist.Register( 'l', typeof( ZedGraphWebLegend ) );
			vsassist.Register( 'b', typeof( ZedGraphWebBorder ) );
			vsassist.Register( 'B', typeof( ZedGraphWebBorder ) );
			vsassist.Register( 'f', typeof( ZedGraphWebFill ) );
			vsassist.Register( 'F', typeof( ZedGraphWebFill ) );
			vsassist.Register( 'E', typeof( ZedGraphWebFill2 ) );
			vsassist.Register( 'C', typeof( ZedGraphWebBorder2 ) );
			vsassist.Register( 's', typeof( ZedGraphWebFontSpec ) );
			vsassist.Register( 'c', typeof( ZedGraphWebCurveCollection ) );
			vsassist.Register( 'g', typeof( ZedGraphWebGraphObjCollection ) );
			//vsassist.Register('r',typeof(ZedGraphWebRect));
			//vsassist.Register('R',typeof(ZedGraphWebRect));
			vsassist.Register( 'm', typeof( ZedGraphWebRect ) );

			this.ChartBorder.Color = ZedGraph.Chart.Default.BorderColor;
			this.ChartBorder.Width = ZedGraph.Chart.Default.BorderPenWidth;
			this.ChartBorder.IsVisible = true;
			this.ChartFill.Brush = ZedGraph.Chart.Default.FillBrush;
			this.ChartFill.Color = ZedGraph.Chart.Default.FillColor;
			this.ChartFill.Type = ZedGraph.Chart.Default.FillType;

			this.PaneBorder.Color = ZedGraph.PaneBase.Default.BorderColor;
			this.PaneBorder.Width = ZedGraph.PaneBase.Default.BorderPenWidth;
			this.PaneBorder.IsVisible = true;
			this.PaneFill.Color = ZedGraph.PaneBase.Default.FillColor;

			this.FontSpec.IsBold = ZedGraph.PaneBase.Default.FontBold;
			this.FontSpec.FontColor = ZedGraph.PaneBase.Default.FontColor;
			this.FontSpec.Family = ZedGraph.PaneBase.Default.FontFamily;
			this.FontSpec.IsItalic = ZedGraph.PaneBase.Default.FontItalic;
			this.FontSpec.Size = ZedGraph.PaneBase.Default.FontSize;
			this.FontSpec.IsUnderline = ZedGraph.PaneBase.Default.FontUnderline;
			this.FontSpec.Fill.Type = FillType.None;	// no fill
			this.FontSpec.Border.IsVisible = false;

			this.XAxis.IsVisible = ZedGraph.XAxis.Default.IsVisible;
			this.XAxis.IsZeroLine = ZedGraph.XAxis.Default.IsZeroLine;

			this.YAxis.IsVisible = ZedGraph.YAxis.Default.IsVisible;
			this.YAxis.IsZeroLine = ZedGraph.YAxis.Default.IsZeroLine;

			this.Y2Axis.IsVisible = ZedGraph.Y2Axis.Default.IsVisible;
			this.Y2Axis.IsZeroLine = ZedGraph.Y2Axis.Default.IsZeroLine;

			ZedGraphWebRect margins = this.Margins;
			margins.Left = ZedGraph.Margin.Default.Left;
			margins.Right = ZedGraph.Margin.Default.Right;
			margins.Top = ZedGraph.Margin.Default.Top;
			margins.Bottom = ZedGraph.Margin.Default.Bottom;
		}

	#endregion

	#region RenderDemo
		/// <summary>
		/// Renders the demo graph with one call.
		/// </summary>
		/// <param name="g">A <see cref="Graphics"/> object for which the drawing will be done.</param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/></param>
		static public void RenderDemo( Graphics g, GraphPane pane )
		{
			// Set the titles and axis labels
			pane.Title.Text = "Wacky Widget Company\nProduction Report";
			pane.XAxis.Title.Text = "Time, Days\n(Since Plant Construction Startup)";
			pane.YAxis.Title.Text = "Widget Production\n(units/hour)";

			LineItem curve;

			// Set up curve "Larry"
			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			// Use green, with circle symbols
			curve = pane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve with a white-green gradient
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50 ), 90F );
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
			curve = pane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135 ), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			// Fill the area under the curve with semi-transparent pink using the alpha value
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205 ), 90F );
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
			pane.Fill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			// Fill the axis background with a gradient
			pane.Chart.Fill = new Fill( Color.FromArgb( 255, 255, 245 ),
				Color.FromArgb( 255, 255, 190 ), 90F );


			// Make each cluster 100 user scale units wide.  This is needed because the X Axis
			// type is Linear rather than Text or Ordinal
			pane.BarSettings.ClusterScaleWidth = 100;
			// Bars are stacked
			pane.BarSettings.Type = BarType.Stack;

			// Enable the X and Y axis grids
			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;

			// Manually set the scale maximums according to user preference
			pane.XAxis.Scale.Max = 1200;
			pane.YAxis.Scale.Max = 120;

			// Add a text item to decorate the graph
			TextObj text = new TextObj( "First Prod\n21-Oct-93", 175F, 80.0F );
			// Align the text such that the Bottom-Center is at (175, 80) in user scale coordinates
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			text.FontSpec.StringAlignment = StringAlignment.Near;
			pane.GraphObjList.Add( text );

			// Add an arrow pointer for the above text item
			ArrowObj arrow = new ArrowObj( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			pane.GraphObjList.Add( arrow );

			// Add a another text item to to point out a graph feature
			text = new TextObj( "Upgrade", 700F, 50.0F );
			// rotate the text 90 degrees
			text.FontSpec.Angle = 90;
			// Align the text such that the Right-Center is at (700, 50) in user scale coordinates
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			// Disable the border and background fill options for the text
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			pane.GraphObjList.Add( text );

			// Add an arrow pointer for the above text item
			arrow = new ArrowObj( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.Line.Width = 2.0F;
			pane.GraphObjList.Add( arrow );

			// Add a text "Confidential" stamp to the graph
			text = new TextObj( "Confidential", 0.85F, -0.03F );
			// use ChartFraction coordinates so the text is placed relative to the ChartRect
			text.Location.CoordinateFrame = CoordType.ChartFraction;
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
			pane.GraphObjList.Add( text );

			// Add a BoxObj to show a colored band behind the graph data
			BoxObj box = new BoxObj( 0, 110, 1200, 10,
				Color.Empty, Color.FromArgb( 225, 245, 225 ) );
			box.Location.CoordinateFrame = CoordType.AxisXYScale;
			// Align the left-top of the box to (0, 110)
			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			// place the box behind the axis items, so the grid is drawn on top of it
			box.ZOrder = ZOrder.D_BehindAxis;
			pane.GraphObjList.Add( box );

			// Add some text inside the above box to indicate "Peak Range"
			TextObj myText = new TextObj( "Peak Range", 1170, 105 );
			myText.Location.CoordinateFrame = CoordType.AxisXYScale;
			myText.Location.AlignH = AlignH.Right;
			myText.Location.AlignV = AlignV.Center;
			myText.FontSpec.IsItalic = true;
			myText.FontSpec.IsBold = false;
			myText.FontSpec.Fill.IsVisible = false;
			myText.FontSpec.Border.IsVisible = false;
			pane.GraphObjList.Add( myText );

			pane.AxisChange( g );
		}

	#endregion

	#region Data Properties


		/// <summary>
		/// The <see cref="String"/> name of the data member that contains the data to be
		/// bound to the graph.
		/// </summary>
		[Category( "Data" ), NotifyParentProperty( true ),
		Description( "Optional. Binding member name for populating the base axis" +
			"(X axis) with values." )]
		public string DataMember
		{
			get
			{
				object x = ViewState["DataMember"];
				return ( null == x ) ? String.Empty : (string)x;
			}
			set { ViewState["DataMember"] = value; }
		}

		/// <summary>
		/// The object reference that points to a data source from which to bind curve data.
		/// </summary>
		[
			Bindable( true ), Browsable(true), Category( "Data" ),
			NotifyParentProperty( true ),
			//AttributeProvider(typeof(IListSource)),
			Description( "Data source containing data for the base axis and the curves" +
			"(can be overriden by curve's DataSource property)" )
		]
		public object DataSource
		{
			get
			{
				object x = ViewState["DataSource"];
				return ( null == x ) ? null : (object)x;
			}
			set { ViewState["DataSource"] = value; }
		}
		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.CurveList"/>.
		/// </summary>
		[
		Category( "Data" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description("The list of CurveItem's that will be displayed on this graph")
		]
		public ZedGraphWebCurveCollection CurveList
		{
			get { return (ZedGraphWebCurveCollection)vsassist.GetValue( 'c', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphObjList"/>.
		/// </summary>
		[
		Category( "Data" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description("The list of GraphObj's that will be displayed on this GraphPane")
		]
		public ZedGraphWebGraphObjCollection GraphObjList
		{
			get { return (ZedGraphWebGraphObjCollection)vsassist.GetValue( 'g', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Gets or sets a value that determines the duration (in hours) of a temporary file generated 
		/// by control in mode "ImageTag"
		/// </summary>
		public double TmpImageDuration
		{
			get { return _tmpImageDuration; }
			set { _tmpImageDuration = value; }
		}


	#endregion

	#region MasterPane Properties

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="PaneBase.BaseDimension"/>.
		/// </summary>
		[Bindable( true ), Category( "MasterPane" ), NotifyParentProperty( true ),
			Description("Determines the 'normal' size, in inches, of the graph at which" +
					" the font scaling factor will be 1.0")]
		public float BaseDimension
		{
			get
			{
				object x = ViewState["BaseDimension"];
				return ( null == x ) ? PaneBase.Default.BaseDimension : (float)x;
			}
			set { ViewState["BaseDimension"] = value; }
		}
		/*
		/// <summary>
		/// </summary>
		[Bindable( true ), Category( "MasterPane" ), NotifyParentProperty( true )]
		[Description( "Main background color" )]
		public Color BackgroundColor
		{
			get
			{
				object x = ViewState["BackgroundColor"];
				return ( null == x ) ? Color.White : (Color)x;
			}
			set { ViewState["BackgroundColor"] = value; }
		}
		*/


		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.Fill"/>.
		/// </summary>
		[
		Category( "MasterPane" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "The background fill properties for the MasterPane" )
		]
		public ZedGraphWebFill2 MasterPaneFill
		{
			get { return (ZedGraphWebFill2)vsassist.GetValue( 'E', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="Chart.Border"/>.
		/// </summary>
		[
		Category( "MasterPane" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "The border properties for the MasterPane" )
		]
		public ZedGraphWebBorder2 MasterPaneBorder
		{
			get { return (ZedGraphWebBorder2)vsassist.GetValue( 'C', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets or sets the width of the <see cref="PaneBase.Rect"/>.
		/// </summary>
		/// <value>The width in output device pixels</value>
		[Bindable( true ), Category( "MasterPane" ), NotifyParentProperty( true ),
			DefaultValue( 400 ),
			Description( "The total width of the control" )
		]
		public int Width
		{
			get
			{
				object x = ViewState["Width"];
				return ( null == x ) ? 400 : (int)x;
			}
			set { ViewState["Width"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the height of the <see cref="PaneBase.Rect"/>.
		/// </summary>
		/// <value>The height in output device pixels</value>
		[	Bindable( true ), Category( "MasterPane" ), NotifyParentProperty( true ),
			DefaultValue( 250 ),
			Description( "The total height of the control" )
		]
		public int Height
		{
			get
			{
				object x = ViewState["Height"];
				return ( null == x ) ? 250 : (int)x;
			}
			set { ViewState["Height"] = value; }
		}

		/*
		/// <summary>
		/// Number of graph panes to create for a compound graph
		/// </summary>
		[Category( "Layout of panes" )]
		public int PaneCount
		{
			get
			{
				object x = ViewState["PaneCount"];
				return ( null == x ) ? 1 : (int)x;
			}
			set
			{
				ViewState["PaneCount"] = Math.Max( 1, value );
			}
		}
		
		/// <summary>
		/// The layout format for the compound graph.
		/// </summary>
		[Category( "Layout of panes" ), Bindable( true ), NotifyParentProperty( true )]
		public PaneLayout PaneLayout
		{
			get
			{
				object x = ViewState["PaneLayout"];
				return ( null == x ) ? PaneLayout.SquareRowPreferred : (PaneLayout)x;
			}
			set { ViewState["PaneLayout"] = value; }
		}
		*/

	#endregion

	#region GraphPane properties

		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane" /> Margin
		/// properties (<see cref="Margin.Left" />, <see cref="Margin.Right" />,
		/// <see cref="Margin.Top" /> and <see cref="Margin.Bottom" />).
		/// </summary>
		[
		Category( "GraphPane" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "The margins for the GraphPane, in scaled points (1/72nd inch)" )
		]
		public ZedGraphWebRect Margins
		{
			get { return (ZedGraphWebRect)vsassist.GetValue( 'm', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets or sets the Title of the <see cref="ZedGraph.GraphPane"/>.
		/// </summary>
		/// <value>A title <see cref="string"/></value>
		[
			Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true ),
			DefaultValue( "" ),
			Description( "The title text for the GraphPane" )
		]
		public string Title
		{
			get
			{
				object x = ViewState["Title"];
				return ( null == x ) ? string.Empty : (string)x;
			}
			set { ViewState["Title"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="Label.IsVisible"/>, which
		/// determines if the <see cref="ZedGraph.PaneBase.Title"/> is visible.
		/// </summary>
		/// <value>true to show the pane title, false otherwise</value>
		[
			Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true ),
			Description( "true to display the GraphPane title, false to hide it" )
		]
		public bool IsShowTitle
		{
			get
			{
				object x = ViewState["IsShowTitle"];
				return ( null == x ) ? PaneBase.Default.IsShowTitle : (bool)x;
			}
			set { ViewState["IsShowTitle"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PaneBase.IsFontsScaled"/>.
		/// </summary>
		[
			Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true ),
			Description( "true to scale the font sizes, margin sizes, etc., according to"
				+ " the normal graph size (see BaseDimension property)" )
		]
		public bool IsFontsScaled
		{
			get
			{
				object x = ViewState["IsFontsScaled"];
				return ( null == x ) ? true : (bool)x;
			}
			set { ViewState["IsFontsScaled"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="PaneBase.IsPenWidthScaled"/>.
		/// </summary>
		[
			Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true ),
			Description( "true to scale the pen widths according to"
				+ " the normal graph size (see BaseDimension property)" )
		]
		public bool IsPenWidthScaled
		{
			get
			{
				object x = ViewState["IsPenWidthScaled"];
				return ( null == x ) ? PaneBase.Default.IsPenWidthScaled : (bool)x;
			}
			set { ViewState["IsPenWidthScaled"] = value; }
		}
		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="BarSettings.Type"/>.
		/// </summary>
		[
			NotifyParentProperty( true ), Category( "GraphPane" ),
			Description("The type of bars to be displayed (stacked, overlay, cluster, etc.)")
		]
		public BarType BarType
		{
			get
			{
				object x = ViewState["BarType"];
				return ( null == x ) ? BarSettings.Default.Type : (BarType)x;
			}
			set { ViewState["BarType"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of the <see cref="GraphPane.LineType"/>.
		/// </summary>
		[
			NotifyParentProperty( true ), Category( "GraphPane" ),
			Description( "The type of line to be displayed (normal or stacked)" )
		]
		public LineType LineType
		{
			get
			{
				object x = ViewState["LineType"];
				return ( null == x ) ? GraphPane.Default.LineType : (LineType)x;
			}
			set { ViewState["LineType"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of the
		/// <see cref="BarSettings.MinClusterGap"/>.
		/// </summary>
		[
			NotifyParentProperty( true ), Category( "GraphPane" ),
			Description( "The gap between the bar clusters, expressed as a fraction of" +
				" the actual bar size" )
		]
		public float MinClusterGap
		{
			get
			{
				object x = ViewState["MinClusterGap"];
				return ( null == x ) ? BarSettings.Default.MinClusterGap : (float)x;
			}
			set { ViewState["MinClusterGap"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of the
		/// <see cref="BarSettings.MinBarGap"/>.
		/// </summary>
		[
			NotifyParentProperty( true ), Category( "GraphPane" ),
			Description( "The gap between the individual bars within a cluster, expressed" +
				" as a fraction of the actual bar size" )
		]
		public float MinBarGap
		{
			get
			{
				object x = ViewState["MinBarGap"];
				return ( null == x ) ? BarSettings.Default.MinBarGap : (float)x;
			}
			set { ViewState["MinBarGap"] = value; }
		}
		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="GraphPane.IsIgnoreInitial"/>.
		/// </summary>
		[Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true )]
		[Description( "If true, initial zero values will be excluded when determining the" +
			" Y or Y2 axis scale range." )]
		public bool IsIgnoreInitial
		{
			get
			{
				object x = ViewState["IsIgnoreInitial"];
				return ( null == x ) ? GraphPane.Default.IsIgnoreInitial : (bool)x;
			}
			set { ViewState["IsIgnoreInitial"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="GraphPane.IsIgnoreMissing"/>.
		/// </summary>
		[
			Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true ),
			Description( "true to have lines ignore any 'missing' values in the curve data" +
				" (e.g., the lines will be continuous even if there are missing values)")
		]
		public bool IsIgnoreMissing
		{
			get
			{
				object x = ViewState["IsIgnoreMissing"];
				return ( null == x ) ? false : (bool)x;
			}
			set { ViewState["IsIgnoreMissing"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="BarSettings.Base"/>.
		/// </summary>
		[
			Bindable( true ), Category( "GraphPane" ), NotifyParentProperty( true ),
			Description( "Sets the bar base axis: X for vertical bars, and Y or Y2 for" +
				" horizontal bars" )
		]
		public BarBase BarBase
		{
			get
			{
				object x = ViewState["BarBase"];
				return ( null == x ) ? BarSettings.Default.Base : (BarBase)x;
			}
			set { ViewState["BarBase"] = value; }
		}

		/*
		/// <summary>
		/// Proxy property that gets or sets the value of the
		/// <see cref="BarSettings.ClusterScaleWidth"/>.
		/// </summary>
		[
			NotifyParentProperty( true ), Category( "GraphPane" ),
			Description( "Sets the width of the bar clusters to be used for ordinal scale" +
				" types" )
		]
		public double ClusterScaleWidth
		{
			get
			{
				object x = ViewState["ClusterScaleWidth"];
				return ( null == x ) ? GraphPane.Default.ClusterScaleWidth : (double)x;
			}
			set { ViewState["ClusterScaleWidth"] = value; }
		}
		*/

		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.Title" />
		/// <see cref="FontSpec"/>.
		/// </summary>
		[
		Category( "GraphPane" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the font properties for the GraphPane title" )
		]
		public ZedGraphWebFontSpec FontSpec
		{
			get { return (ZedGraphWebFontSpec)vsassist.GetValue( 's', this.IsTrackingViewState ); }
		}
		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.Border"/>.
		/// </summary>
		[
		Category( "GraphPane" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the border properties for the GraphPane" )
		]
		public ZedGraphWebBorder PaneBorder
		{
			get { return (ZedGraphWebBorder)vsassist.GetValue( 'B', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.Fill"/>.
		/// </summary>
		[
		Category( "GraphPane" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the background fill properties for the GraphPane" )
		]
		public ZedGraphWebFill PaneFill
		{
			get { return (ZedGraphWebFill)vsassist.GetValue( 'F', this.IsTrackingViewState ); }
		}


	#endregion

	#region Chart Properties

		/// <summary>
		/// Proxy property that gets the value of the <see cref="Chart.Border"/>.
		/// </summary>
		[
		Category( "Chart" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the border properties for the Chart area" )
		]
		public ZedGraphWebBorder ChartBorder
		{
			get { return (ZedGraphWebBorder)vsassist.GetValue( 'b', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="Chart.Fill"/>.
		/// </summary>
		[
		Category( "Chart" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the background fill properties for the Chart area" )
		]
		public ZedGraphWebFill ChartFill
		{
			get { return (ZedGraphWebFill)vsassist.GetValue( 'f', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.XAxis"/>.
		/// </summary>
		[
		Category( "Chart" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the properties of the X Axis" )
		]
		public ZedGraphWebXAxis XAxis
		{
			get { return (ZedGraphWebXAxis)vsassist.GetValue( 'x', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.YAxis"/>.
		/// </summary>
		[
		Category( "Chart" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the properties of the Y Axis" )
		]
		public ZedGraphWebYAxis YAxis
		{
			get { return (ZedGraphWebYAxis)vsassist.GetValue( 'y', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="GraphPane.Y2Axis"/>.
		/// </summary>
		[
		Category( "Chart" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the properties of the Y2 Axis" )
		]
		public ZedGraphWebY2Axis Y2Axis
		{
			get { return (ZedGraphWebY2Axis)vsassist.GetValue( 'z', this.IsTrackingViewState ); }
		}

		/// <summary>
		/// Proxy property that gets the value of the <see cref="PaneBase.Legend"/>.
		/// </summary>
		[
		Category( "Chart" ),
		DesignerSerializationVisibility( DesignerSerializationVisibility.Content ),
		NotifyParentProperty( true ),
		PersistenceMode( PersistenceMode.InnerProperty ),
		Description( "Sets the properties of the Legend" )
		]
		public ZedGraphWebLegend Legend
		{
			get { return (ZedGraphWebLegend)vsassist.GetValue( 'l', this.IsTrackingViewState ); }
		}


	#endregion

	#region Behavior Properties

		/// <summary>
		/// Optional setting that determines how long the cached image will remain valid. 
		/// A zero value disables caching.
		/// </summary>
		/// <value></value>
		[Category( "Behavior" ), NotifyParentProperty( true ),
		Description( "Optional output caching parameter in seconds. A zero value disables internal caching. " +
			"For more advanced caching see microsoft documentation" )]
		public int CacheDuration
		{
			get
			{
				object x = ViewState["CacheDuration"];
				return ( null == x ) ? 0 : (int)x;
			}
			set { ViewState["CacheDuration"] = value; }
		}

		/// <summary>
		/// Optional cache file suffix that can be used to modify the output cache file name
		/// to make it unique.
		/// </summary>
		/// <value></value>
		[Category( "Behavior" ), NotifyParentProperty( true ),
		Description( "Optional cache file suffix that can be used to modify the output cache file name" +
					" to make it unique." )]
		public string CacheSuffix
		{
			get
			{
				string x = ViewState["CacheSuffix"] as string;
				return ( x == null ) ? "" : x;
			}
			set { ViewState["CacheSuffix"] = value; }
		}

		/// <summary>
		/// Gets or sets a boolean flag value that, if true, will cause the
		/// <see cref="ZedGraph.GraphPane.AxisChange()"/> method to be called when
		/// <see cref="CreateGraph( Stream, ImageFormat )"/> is called.
		/// </summary>
		/// <value>A boolean value, true to call <see cref="GraphPane.AxisChange()"/>,
		/// false otherwise</value>
		[
		Bindable( true ), Category( "Behavior" ), NotifyParentProperty( true ),
		DefaultValue( "true" ),
		Description( "true to force a call to AxisChange() when the graph is built" )
		]
		public bool AxisChanged
		{
			get
			{
				object x = ViewState["AxisChanged"];
				return ( null == x ) ? true : (bool)x;
			}
			set { ViewState["AxisChanged"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value that determines the output format for the control, in the
		/// form of a <see cref="ZedGraphWebFormat"/> enumeration.  This is typically Gif, Jpeg,
		/// Png, or Icon.
		/// </summary>
		/// <value>A <see cref="ZedGraphWebFormat"/> enumeration.</value>
		[
		Bindable( true ), Category( "Behavior" ),
		NotifyParentProperty( true ), DefaultValue( ZedGraphWebFormat.Png ),
		Description( "Determines the type of image to output (png,gif,jpg,etc)" )
		]
		public ZedGraphWebFormat OutputFormat
		{
			get
			{
				object x = ViewState["OutputFormat"];
				return ( null == x ) ? ZedGraphWebFormat.Png : (ZedGraphWebFormat)x;
			}
			set { ViewState["OutputFormat"] = value; }
		}


		/// <summary>
		/// What to return ?
		/// A raw image or an IMG tag referencing a generated image ?
		/// </summary>
		[
		Category( "Behavior" ),
		DefaultValue( RenderModeType.ImageTag ),
		Description( "What to return ? A raw image or an IMG tag referencing the generated image ?" )
		]
		public RenderModeType RenderMode
		{
			get
			{
				RenderModeType retVal = RenderModeType.ImageTag;
				try
				{
					retVal = (RenderModeType)RenderModeType.Parse( typeof( RenderModeType ), ViewState["RenderMode"].ToString() );
				}
				catch ( System.Exception )
				{
				}

				return retVal;
			}

			set { ViewState["RenderMode"] = value; }
		}

		/// <summary>
		/// What to return ?
		/// A raw image or an IMG tag referencing a generated image ?
		/// </summary>
		[
		Category( "Behavior" ),
		DefaultValue( "~/ZedGraphImages/" ),
		Description( "Web path of the folder which will contain images when RenderMode is" +
			" ImageTag. Don't forget to create this folder and make it writeable !" )
		]
		public string RenderedImagePath
		{
			get
			{
				string x = ViewState["RenderedImagePath"] as string;
				return ( x == null ) ? "~/ZedGraphImages/" : x;
			}
			set { ViewState["RenderedImagePath"] = value; }
		}

		/// <summary>
		/// Proxy property that gets or sets the value of <see cref="ZedGraphWeb.IsImageMap" />.
		/// </summary>
		[
		Bindable( true ), Category( "Appearance" ), NotifyParentProperty( true ),
		Description( "Web path of the folder which will contain images when RenderMode is" )
		]
		public bool IsImageMap
		{
			get
			{
				object x = ViewState["IsImageMap"];
				return ( x == null ) ? false : (bool)x;
			}
			set { ViewState["IsImageMap"] = value; }
		}

	#endregion

	#region Event Handlers

		/// <summary>
		/// Sets the rendering event handler.
		/// </summary>
		/// <value>An event type for the RenderGraph event</value>
		[
		Category( "Action" ),
		Description( "Subscribe to this event in your CodeFile to do the graph construction" )
		]
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
		protected virtual void OnDrawPane( Graphics g, MasterPane mp )
		{
			ZedGraphWebControlEventHandler handler;
			handler = (ZedGraphWebControlEventHandler)Events[_eventRender];

			MasterPaneFill.CopyTo( mp.Fill );
			MasterPaneBorder.CopyTo( mp.Border );

			if ( ( handler == null ) && ( CurveList.Count == 0 ) && ( GraphObjList.Count == 0 ) )
			{
				// default with the sample graph if no callback provided
				foreach ( GraphPane p in mp.PaneList )
				{
					ZedGraphWeb.RenderDemo( g, p );
				}
			}
			else
			{
				foreach ( GraphPane p in mp.PaneList )
				{
					// Add visual designer influences here - first!!
					SetWebProperties( g, p );

					// Add DataSource values if available before the callback
					PopulateByDataSource( g, p );

					//Add Graph Items
					AddWebGraphItems( g, p );
				}

				//TODO: verify callback regression test
				// Add custom callback tweeking next
				if ( handler != null )
					handler( this, g, mp );

				// Set the layout according to user preferences
				mp.ReSize( g );
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
		protected void SetWebProperties( Graphics g, GraphPane pane )
		{
			try
			{
				pane.Title.IsVisible = this.IsShowTitle;
				pane.BarSettings.Type = this.BarType;
				XAxis.CopyTo( pane.XAxis );
				YAxis.CopyTo( pane.YAxis );
				Y2Axis.CopyTo( pane.Y2Axis );
				pane.IsIgnoreInitial = this.IsIgnoreInitial;
				pane.IsIgnoreMissing = this.IsIgnoreMissing;
				pane.LineType = this.LineType;
				this.ChartBorder.CopyTo( pane.Chart.Border );
				this.ChartFill.CopyTo( pane.Chart.Fill );
				pane.BarSettings.MinClusterGap = this.MinClusterGap;
				pane.BarSettings.MinBarGap = this.MinBarGap;
				pane.BarSettings.Base = this.BarBase;
				this.Legend.CopyTo( pane.Legend );
				this.FontSpec.CopyTo( pane.Title.FontSpec );
				pane.Title.Text = this.Title;
				this.PaneBorder.CopyTo( pane.Border );
				this.PaneFill.CopyTo( pane.Fill );
				pane.Margin.Left = this.Margins.Left;
				pane.Margin.Right = this.Margins.Right;
				pane.Margin.Top = this.Margins.Top;
				pane.Margin.Bottom = this.Margins.Bottom;
				pane.BaseDimension = this.BaseDimension;
				pane.IsFontsScaled = this.IsFontsScaled;
				pane.IsPenWidthScaled = this.IsPenWidthScaled;
			}
			catch ( Exception )
			{
				//throw;
			}
		}

		/// <summary>
		/// Add the <see cref="ZedGraphWebGraphObj" /> objects defined in the webcontrol to
		/// the <see cref="GraphPane" /> as <see cref="GraphObj" /> objects.
		/// </summary>
		/// <param name="g">The <see cref="Graphics" /> instance of interest.</param>
		/// <param name="pane">The <see cref="GraphPane" /> object to receive the
		/// <see cref="GraphObj" /> objects.</param>
		protected void AddWebGraphItems( Graphics g, GraphPane pane )
		{
			try
			{
				ZedGraphWebGraphObj draw;
				for ( int i = 0; i < GraphObjList.Count; i++ )
				{
					draw = GraphObjList[i];
					if ( draw is ZedGraphWebTextObj )
					{
						ZedGraphWebTextObj item = (ZedGraphWebTextObj)draw;
						TextObj x = new TextObj();
						item.CopyTo( x );
						pane.GraphObjList.Add( x );
					}
					else if ( draw is ZedGraphWebArrowObj )
					{
						ZedGraphWebArrowObj item = (ZedGraphWebArrowObj)draw;
						ArrowObj x = new ArrowObj();
						item.CopyTo( x );
						pane.GraphObjList.Add( x );
					}
					else if ( draw is ZedGraphWebImageObj )
					{
						ZedGraphWebImageObj item = (ZedGraphWebImageObj)draw;
						ImageObj x = new ImageObj();
						item.CopyTo( x );
						pane.GraphObjList.Add( x );
					}
					else if ( draw is ZedGraphWebBoxObj )
					{
						ZedGraphWebBoxObj item = (ZedGraphWebBoxObj)draw;
						BoxObj x = new BoxObj();
						item.CopyTo( x );
						pane.GraphObjList.Add( x );
					}
					else if ( draw is ZedGraphWebEllipseObj )
					{
						ZedGraphWebEllipseObj item = (ZedGraphWebEllipseObj)draw;
						EllipseObj x = new EllipseObj();
						item.CopyTo( x );
						pane.GraphObjList.Add( x );
					}
				}
			}
			catch ( Exception )
			{
			}
		}
		#endregion

	#region Process DataSource

		/// <summary>
		/// Provides binding between <see cref="DataSource"/> and the specified pane.  Extracts the
		/// data from <see cref="DataSource"/> and copies it into the appropriate
		/// <see cref="ZedGraph.IPointList"/> for each <see cref="ZedGraph.CurveItem"/> in the
		/// specified <see cref="ZedGraph.GraphPane"/>.
		/// </summary>
		/// <param name="g">The <see cref="Graphics"/> object to be used for rendering the data.</param>
		/// <param name="pane">The <see cref="ZedGraph.GraphPane"/> object which will receive the data.</param>
		protected void PopulateByDataSource( Graphics g, GraphPane pane )
		{
			if ( this.CurveList.Count == 0 )
				return;

			//If the Datasource column names are available we can bind them 
			// correctly to their corresponding DataMember.
			if ( this.DataMember != null && this.DataMember != String.Empty
				&& this.DataSource != null
				&& this.DataSource is ITypedList
				&& this.DataSource is IListSource
				)
			{
				ITypedList tlist = this.DataSource as ITypedList;
				IListSource listSource = this.DataSource as IListSource;
				IList list = listSource.GetList();
				PropertyDescriptorCollection pdc = tlist.GetItemProperties( null );
				bool bListContainsList = listSource.ContainsListCollection;

				//Get the DataMember and Type of the base axis in the DataSource
				string baseDataMember = this.DataMember;
				PropertyDescriptor basePd = pdc.Find( baseDataMember, true );
				if ( basePd == null )
					throw new System.Exception( "Can't find DataMember '" + baseDataMember + "' in DataSource for the base axis." );
				baseDataMember = basePd.Name;
				Type baseDataType = basePd.PropertyType;
				int indexBaseColumn = pdc.IndexOf( basePd );

				//Foreach bar/curve
				//  Get its DataMember and Type in the DataSource
				//	Add the curve to the pane
				//  Add all corresponding points(baseAxis,valueAxis,0)
				//Note: Z axis is not supported
				foreach ( ZedGraphWebCurveItem curveItem in this.CurveList )
				{
					//Axis valueAxis = curveItem.ValueAxis;
					PropertyDescriptorCollection pdcValue = pdc;
					IList valueList = list;
					bool bValueListContainsList = bListContainsList;

					//If present, use DataSource of Curve instead of main DataSource
					if ( curveItem.DataSource != null
						&& curveItem.DataSource is ITypedList
						&& curveItem.DataSource is IListSource )
					{
						ITypedList valueTlist = curveItem.DataSource as ITypedList;
						pdcValue = valueTlist.GetItemProperties( null );
						IListSource valueListSource = curveItem.DataSource as IListSource;
						valueList = valueListSource.GetList();
						bValueListContainsList = valueListSource.ContainsListCollection;
					}

					string valueDataMember = curveItem.DataMember;
					PropertyDescriptor pd = pdcValue.Find( valueDataMember, true );
					if ( pd == null )
						throw new System.Exception( "Can't find DataMember '" + valueDataMember + "' in DataSource for the " + curveItem.Label + " axis." );
					valueDataMember = pd.Name; //Get the exact case-dependent name
					Type valueDataType = pd.PropertyType;
					int indexValueColumn = pdcValue.IndexOf( pd );

					//Add points
					PointPairList points = new PointPairList();
					PointPair pair = new PointPair();
					object oColumnValue;

					try
					{
						int nRow = 0;
						foreach ( object row in list )
						{
							//
							// Value axis binding (Y axis)
							//
							object valueRow = valueList[nRow];

							//Get item value in 'row'
							if ( bValueListContainsList )
							{
								if ( !( valueRow is IList ) )
									throw new System.InvalidCastException( "The DataSource contains a list which declares its items as lists, but these don't support the IList interface." );
								oColumnValue = ( valueRow as IList )[indexValueColumn];
							}
							else
							{
								oColumnValue = pd.GetValue( valueRow );
							}

							//Convert value to double (always double)
							double v = 0;
							switch ( oColumnValue.GetType().ToString() )
							{
								case "System.DateTime":
									v = new XDate( Convert.ToDateTime( oColumnValue ) ).XLDate;
									break;
								default:
									try
									{
										v = Convert.ToDouble( oColumnValue );
									}
									catch
									{
										throw new NotImplementedException( "Conversion from " + oColumnValue.GetType() + " to double not implemented." );
									}
									break;
							}

							//
							// Base axis binding (X axis)
							//
							pair.Tag = oColumnValue; //Original typed value
							pair.Y = v;
							if ( this.XAxis.Type == AxisType.DateAsOrdinal
								|| this.XAxis.Type == AxisType.Date )
							{
								pair.X = new XDate( Convert.ToDateTime( basePd.GetValue( row ) ) ).XLDate;
							}
							else
								pair.X = Convert.ToDouble( basePd.GetValue( row ) );

							points.Add( pair );

							nRow++;
						}
					}
					catch ( System.ArgumentOutOfRangeException )
					{
						//A local datasource was set on this curve but it has fewer rows than the axis datasource.
						//So we stop feeding this curve.
					}

					//Create curve in pane with its points
					curveItem.CreateInPane( pane, points );
				}
			}
			else
			{
				//Add curves and values set in designer
				ZedGraphWebCurveItem curve;
				for ( int i = 0; i < CurveList.Count; i++ )
				{
					curve = CurveList[i];

					PointPairList points = new PointPairList();
					PointPair pair = new PointPair();
					for ( int j = 0; j < curve.Points.Count; j++ )
					{
						curve.Points[j].CopyTo( pair );
						points.Add( pair );
					}

					curve.CreateInPane( pane, points );
				}
			}

			//NOTE: ZedGraphWeb.DataMember = base axis
			//NOTE: ZedGraphCurveItem.DataMember = Y
			//NOTE: Z values are only supported via the callback (???)
			//TODO: cache the data-map table before processing rows (???)
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
		}

		/// <summary>
		/// Method to create a <see cref="ZedGraph.GraphPane"/> class for the control.
		/// </summary>
		/// <param name="OutputStream">A <see cref="Stream"/> in which to output the ZedGraph
		/// <see cref="System.Drawing.Image"/>.</param>
		/// <param name="Format">The <see cref="ImageFormat"/> type to be output.</param>
		public MasterPane CreateGraph( System.IO.Stream OutputStream, ImageFormat Format )
		{
			return CreateGraph( OutputStream, Format, false );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="OutputStream"></param>
		/// <param name="Format"></param>
		/// <param name="bShowTransparency">if true, draw squares instead of leaving the
		/// background transparent</param>
		/// <remarks>
		/// bShowTransparency is set to true in design mode, to false otherwise.
		/// </remarks>
		protected MasterPane CreateGraph( System.IO.Stream OutputStream, ImageFormat Format,
				bool bShowTransparency )
		{
			RectangleF rect = new RectangleF( 0, 0, this.Width, this.Height );
			MasterPane mp = new MasterPane( string.Empty, rect );
			mp.Margin.All = 0;
			mp.Fill.IsVisible = false;
			mp.Border.IsVisible = false;

			// create all required panes
			//for ( int i = 0; i < this.PaneCount; i++ )
			//{
			mp.Add( new GraphPane( rect, Title, string.Empty, string.Empty ) );
			//}

			// create output bitmap container						
			Bitmap image = new Bitmap( this.Width, this.Height );
			using ( Graphics g = Graphics.FromImage( image ) )
			{
				// Apply layout plan				
				//mp.SetLayout( this.PaneLayout );
				mp.ReSize( g, rect );

				// Use callback to gather more settings and data values
				OnDrawPane( g, mp );

				// Allow designer control of axischange
				if ( this.AxisChanged ) mp.AxisChange( g );

				// Render the graph to a bitmap
				if ( bShowTransparency && mp.Fill.Color.A != 255 )
				{
					//Show the transparency as white/gray filled squares
					// We need to add the resource namespace to its name
					//string resourceName = string.Format( "{0}.transparency.png", GetType().Namespace );
					string resourceName = "ZedGraph.ZedGraph.transparency.png";
					Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream( resourceName );

					if ( stream == null )
						throw new Exception( "Does the Build Action of the resource " + resourceName + " is set to Embedded Resource ?" );

					using ( System.Drawing.Image brushImage = new Bitmap( stream ) )
					using ( TextureBrush brush = new TextureBrush( brushImage, WrapMode.Tile ) )
					{
						g.FillRectangle( brush, 0, 0, this.Width, this.Height );
					}
					stream.Close();
				}
				mp.Draw( g );
			}

			// Stream the graph out				
			MemoryStream ms = new MemoryStream();
			image.Save( ms, Format );

			//TODO: provide caching options
			ms.WriteTo( OutputStream );

			return mp;
		}

		private System.IO.FileStream DesignTimeFileStream = null;

		/// <summary>
		/// Override the Render() method with a do-nothing method.
		/// </summary>
		/// <param name="output"></param>
		protected override void Render( HtmlTextWriter output )
		{
			MasterPane masterPane = null;

			bool bDesignMode = ( this.Page.Site != null && this.Page.Site.DesignMode );

			if ( !bDesignMode && this.RenderMode == RenderModeType.RawImage )
			{
				//Render on the fly
				if ( this.CacheDuration > 0 )
				{
					System.Web.HttpContext context = System.Web.HttpContext.Current;
					System.Web.HttpCachePolicy policy = context.Response.Cache;
					if ( policy != null )
					{
						policy.SetExpires( DateTime.Now.AddSeconds( this.CacheDuration ) );
					}
				}

				Draw( true );
			}
			else
			{
				//Render as a file and an IMG tag
				try
				{
					string tempFileName, tempFilePathName;

					//In design, we always recreate the file. No caching is allowed.
					if ( bDesignMode )
					{
						//Create temporary file if it does not exists
						if ( DesignTimeFileStream == null )
						{
							tempFilePathName = Path.GetTempFileName();
							DesignTimeFileStream = new FileStream( tempFilePathName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite );
							tempFileName = Path.GetFileName( tempFilePathName );
						}
						else
						{
							tempFilePathName = DesignTimeFileStream.Name;
							tempFileName = Path.GetFileName( tempFilePathName );
							DesignTimeFileStream.SetLength( 0 );
							DesignTimeFileStream.Seek( 0, SeekOrigin.Begin );
						}
					}
					else
					{
						tempFileName = this.ClientID + System.Guid.NewGuid().ToString() +
							( this.CacheSuffix != null && this.CacheSuffix.Length > 0 ? this.CacheSuffix : "" ) +
							"." + this.ImageFormatFileExtension;
						tempFilePathName = Context.Server.MapPath( this.RenderedImagePath );
						tempFilePathName = Path.Combine( tempFilePathName, tempFileName );

						// Insert FileDestructor into cache
						TempFileDestructor tfd = new TempFileDestructor( tempFilePathName );
						System.Web.Caching.CacheItemRemovedCallback onRemove = new System.Web.Caching.CacheItemRemovedCallback( tfd.RemovedCallback );
						Page.Cache.Add( tempFileName, tfd, null, DateTime.Now.AddHours( _tmpImageDuration ), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, onRemove );

						//System.Guid.NewGuid().ToString()
						//tempFileName = this.ClientID +
						//	( this.CacheSuffix != null && this.CacheSuffix.Length > 0 ? this.CacheSuffix : "" ) +
						//	"." + this.ImageFormatFileExtension;

						//Should we use the cached image ?
						if ( this.CacheDuration == 0 ||
							!File.Exists( tempFilePathName ) || // IsImageMap ||
							 File.GetCreationTimeUtc( tempFilePathName ).AddSeconds( this.CacheDuration )
									< DateTime.Now.ToUniversalTime() )
						{
							//No: so recreate the image file
							DesignTimeFileStream = new FileStream( tempFilePathName, FileMode.Create,
													FileAccess.ReadWrite, FileShare.ReadWrite );
							DesignTimeFileStream.SetLength( 0 );
							DesignTimeFileStream.Seek( 0, SeekOrigin.Begin );
							//Because of a bug in .NET (deleting a file and creating a file with the same
							//        name results in the deleted file's CreationTime being returned)
							// we need to explicitely set the creation time.
							File.SetCreationTimeUtc( tempFilePathName, DateTime.Now.ToUniversalTime() );
						}
					}

					//Recreate image if needed (caching expired or no caching)
					if ( DesignTimeFileStream != null )
					{
						ImageFormat imageFormat = this.ImageFormat;
						if ( bDesignMode )
							imageFormat = ImageFormat.Png;

						// Draw the graph
						masterPane = CreateGraph( DesignTimeFileStream, imageFormat, bDesignMode );

						DesignTimeFileStream.Flush();
						if ( !bDesignMode ) //Production mode: close files !
						{
							DesignTimeFileStream.Close();
							DesignTimeFileStream = null;
						}
					}

					//The file should exist at this point
					DateTime tempFileDate = File.GetCreationTimeUtc( tempFilePathName );

					//Write HTML tag: <IMG width="" height="" src="" alt="">
					string src;

					if ( bDesignMode )
						src = "file://" + DesignTimeFileStream.Name;
					else
					{
						src = this.RenderedImagePath;

						if ( !src.EndsWith( "/" ) )
							src += '/';

						src = Page.ResolveUrl( src );

						//if ( src.StartsWith( "~/" ) )
						//	src = src.Substring( 2 );
						//else if ( src.StartsWith( "~" ) )
						//	src = src.Substring( 1 );

						//Add a querystring to defeat browsers cache when our image is recreated
						src += tempFileName + "?" + tempFileDate.ToString( "yyyyMMddhhmmss" );
					}

					output.AddAttribute( HtmlTextWriterAttribute.Width, this.Width.ToString() );
					output.AddAttribute( HtmlTextWriterAttribute.Height, this.Height.ToString() );
					output.AddAttribute( HtmlTextWriterAttribute.Src, src );
					output.AddAttribute( HtmlTextWriterAttribute.Alt, String.Empty );
					output.AddAttribute( HtmlTextWriterAttribute.Border, "0" ); //CJBL

					if ( this.IsImageMap && masterPane != null )
						output.AddAttribute( "usemap", "#" + tempFileName + ".map" );
//						output.AddAttribute( HtmlTextWriterAttribute.Usemap, "#" + tempFileName + ".map" );
					output.RenderBeginTag( HtmlTextWriterTag.Img );
					output.RenderEndTag();

					if ( this.IsImageMap && masterPane != null )
					{
						output.AddAttribute( HtmlTextWriterAttribute.Name, tempFileName + ".map" );
						output.RenderBeginTag( HtmlTextWriterTag.Map );
						MakeImageMap( masterPane, output );
						output.RenderEndTag();
					}
				}
				catch ( Exception e )
				{
					output.AddAttribute( HtmlTextWriterAttribute.Width, this.Width.ToString() );
					output.AddAttribute( HtmlTextWriterAttribute.Height, this.Height.ToString() );
					output.RenderBeginTag( HtmlTextWriterTag.Span );
					output.Write( e.ToString() );
					output.RenderEndTag();
				}

			}
		}

		/// <summary>
		/// Generate an ImageMap as Html tags
		/// </summary>
		/// <param name="masterPane">The source <see cref="MasterPane" /> to be
		/// image mapped.</param>
		/// <param name="output">An <see cref="HtmlTextWriter" /> instance in which
		/// the html tags will be written for the image map.</param>
		public void MakeImageMap( MasterPane masterPane, HtmlTextWriter output )
		{
			string shape;
			string coords;
			Bitmap image = new Bitmap( 1, 1 );
			using ( Graphics g = Graphics.FromImage( image ) )
			{
				float masterScale = masterPane.CalcScaleFactor();
				// Frontmost objects are MasterPane objects with A_InFront
				foreach ( GraphObj obj in masterPane.GraphObjList )
				{
					if ( obj.Link.IsActive && obj.ZOrder == ZOrder.A_InFront )
					{
						obj.GetCoords( masterPane, g, masterScale, out shape, out coords );
						MakeAreaTag( shape, coords, obj.Link.Url, obj.Link.Target,
							obj.Link.Title, obj.Link.Tag, output );
					}
				}

				// Now loop over each GraphPane
				foreach ( GraphPane pane in masterPane.PaneList )
				{
					float scaleFactor = pane.CalcScaleFactor();

					// Next comes GraphPane GraphObjs in front of data points
					foreach ( GraphObj obj in pane.GraphObjList )
					{
						if ( obj.Link.IsActive && obj.IsInFrontOfData )
						{
							obj.GetCoords( pane, g, scaleFactor, out shape, out coords );
							MakeAreaTag( shape, coords, obj.Link.Url, obj.Link.Target,
								obj.Link.Title, obj.Link.Tag, output );
						}
					}

					// Then come the data points (CurveItems)
					foreach ( CurveItem curve in pane.CurveList )
					{
						if ( curve.Link.IsActive && curve.IsVisible )
						{
							for ( int i=0; i < (curve is PieItem ? 1 : curve.Points.Count); i++ )
							{
								//if ( curve.GetCoords( pane, i, pane.Rect.Left, pane.Rect.Top, out coords ) )
								if ( curve.GetCoords( pane, i, out coords ) )
								{
									if ( curve is PieItem )
									{
										MakeAreaTag( "poly", coords, curve.Link.Url,
												curve.Link.Target, curve.Link.Title, curve.Link.Tag, output );
										// only one point for PieItems
										break;
									}
									else
									{
										// Add an "?index=4" type tag to the url to indicate which
										// point was selected
										string url;
										if ( curve.Link.Url != string.Empty )
											url = curve.Link.MakeCurveItemUrl( pane, curve, i );
										else
											url = "";

										string title = curve.Link.Title;
										if ( curve.Points[i].Tag is string )
											title = curve.Points[i].Tag as string;
										MakeAreaTag( "rect", coords, url,
												curve.Link.Target, title, curve.Points[i].Tag, output );
									}
								}
							}
						}
					}

					// Then comes the GraphObjs behind the data points
					foreach ( GraphObj obj in pane.GraphObjList )
					{
						if ( obj.Link.IsActive && !obj.IsInFrontOfData )
						{
							obj.GetCoords( pane, g, scaleFactor, out shape, out coords );
							MakeAreaTag( shape, coords, obj.Link.Url, obj.Link.Target,
								obj.Link.Title, obj.Link.Tag, output );
						}
					}

				}

				// Hindmost objects are MasterPane objects with !A_InFront
				foreach ( GraphObj obj in masterPane.GraphObjList )
				{
					if ( obj.Link.IsActive && obj.ZOrder != ZOrder.A_InFront )
					{
						obj.GetCoords( masterPane, g, masterScale, out shape, out coords );
						MakeAreaTag( shape, coords, obj.Link.Url, obj.Link.Target,
							obj.Link.Title, obj.Link.Tag, output );
					}
				}

			}
		}

		private void MakeAreaTag( string shape, string coords, string url, string target,
			string title, object tag, HtmlTextWriter output )
		{
//			output.AddAttribute( HtmlTextWriterAttribute.Shape, shape );
			output.AddAttribute( "shape", shape );
//			output.AddAttribute( HtmlTextWriterAttribute.Coords, coords );
			output.AddAttribute( "coords", coords );

			if ( url != string.Empty )
			{
				if ( tag is string )
					output.AddAttribute( HtmlTextWriterAttribute.Href, url + "&" + tag );
				else
					output.AddAttribute( HtmlTextWriterAttribute.Href, url );
			}
			if ( target != string.Empty && url != string.Empty )
				output.AddAttribute( HtmlTextWriterAttribute.Target, target );
			if ( title != string.Empty )
				output.AddAttribute( HtmlTextWriterAttribute.Title, title );

			output.RenderBeginTag( HtmlTextWriterTag.Area );
			//output.Write( "Here's some random text" );
			output.RenderEndTag();
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
				switch ( OutputFormat )
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
		/// Gets the current image format file extension
		/// </summary>
		protected string ImageFormatFileExtension
		{
			get
			{
				switch ( OutputFormat )
				{
					case ZedGraphWebFormat.Gif:
						return "gif";
					case ZedGraphWebFormat.Jpeg:
						return "jpg";
					case ZedGraphWebFormat.Icon:
						return "ico";
					case ZedGraphWebFormat.Png:
						return "png";
				}

				throw new System.NotImplementedException( OutputFormat.ToString() + " format is not implemented in ImageFormatFileExtension !" );
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
				switch ( OutputFormat )
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
		protected override void LoadViewState( object savedState )
		{
			object state = vsassist.LoadViewState( savedState, this.IsTrackingViewState );
			if ( state != null ) base.LoadViewState( state );
		}

		/// <summary>
		/// Used by asp.net to save the viewstate to the class instance given a portable state object.
		/// </summary>
		/// <returns>portable state object</returns>
		protected override object SaveViewState()
		{
			return vsassist.SaveViewState( base.SaveViewState() );
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
					File.Delete( name );
				}
			}
			catch ( Exception )
			{
			}
		}

		#endregion

	#region IServiceProvider Members

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns></returns>
		public object GetService( Type serviceType )
		{
			// TODO:  Add ZedGraphWeb.GetService implementation
			return null;
		}

		#endregion
	}



	/// <summary>
	/// A delegate to handle the rendering event for this control.
	/// </summary>
	/// <param name="webObject">A reference to this <see cref="ZedGraphWeb" /> object</param>
	/// <param name="g">A <see cref="Graphics"/> object for which the drawing will be done.</param>
	/// <param name="pane">A reference to the <see cref="GraphPane"/>
	/// class to be rendered.</param>
	public delegate void ZedGraphWebControlEventHandler( ZedGraphWeb webObject,
			Graphics g, MasterPane pane );
}
