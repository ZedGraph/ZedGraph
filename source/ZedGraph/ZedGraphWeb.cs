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
	/// <version> $Revision: 3.2 $ $Date: 2005-01-07 07:19:10 $ </version>
	[
	ParseChildren(true),
	PersistChildren(false),
	DefaultProperty("Title"),
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

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ZedGraphWeb()
		{
		}

		#region Chart Item Collection
		protected ZedGraphWebCurveCollection _curves = null;
		[
		Category("Data"),		
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ZedGraphWebCurveCollection Curves
		{
			get 
			{				
				if ( null == _curves )
				{
					_curves = new ZedGraphWebCurveCollection();
					if (IsTrackingViewState) 
					{
						((IStateManager)_curves).TrackViewState();
					}					
				}
				return _curves;
			}			
		}
		#endregion

	#region Built in demo code
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
		/// private field to store the control width.  Use the public property
		/// <see cref="Width"/> to access this value.
		/// </summary>
		private int width = 400;
		/// <summary>
		/// Gets or sets the width of the <see cref="ZedGraph.GraphPane.PaneRect"/>.
		/// </summary>
		/// <value>The width in output device pixels</value>
		[Bindable(true),Category("Layout"),NotifyParentProperty(true),DefaultValue(400)]
		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		/// <summary>
		/// private field to store the control height.  Use the public property
		/// <see cref="Height"/> to access this value.
		/// </summary>
		private int height = 250;
		/// <summary>
		/// Gets or sets the Height of the <see cref="ZedGraph.GraphPane.PaneRect"/>.
		/// </summary>
		/// <value>The height in output device pixels</value>
		[Bindable(true),Category("Layout"),NotifyParentProperty(true),DefaultValue(250)]
		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		/// <summary>
		/// private field to store the graph title.  Use the public property
		/// <see cref="Title"/> to access this value.
		/// </summary>
		private string title = string.Empty;
		/// <summary>
		/// Gets or sets the Title of the <see cref="ZedGraph.GraphPane"/>.
		/// </summary>
		/// <value>A title <see cref="string"/></value>
		[Bindable(true),Category("Appearance"),NotifyParentProperty(true),DefaultValue("")]
		public string Title
		{
			get { return title; }
			set { title = value; }			
		}

		/// <summary>
		/// private field to store the X axis label.  Use the public property
		/// <see cref="XLabel"/> to access this value.
		/// </summary>
		private string xlabel = string.Empty;
		/// <summary>
		/// Gets or sets the X Axis label of the <see cref="ZedGraph.GraphPane.XAxis"/>.
		/// </summary>
		/// <value>An X axis label <see cref="string"/></value>
		[Bindable(true),Category("Appearance"),NotifyParentProperty(true),DefaultValue("")]
		public string XLabel
		{
			get { return xlabel; }
			set { xlabel = value; }			
		}

		/// <summary>
		/// private field to store the Y axis label.  Use the public property
		/// <see cref="YLabel"/> to access this value.
		/// </summary>
		private string ylabel = string.Empty;
		/// <summary>
		/// Gets or sets the Y Axis label of the <see cref="ZedGraph.GraphPane.YAxis"/>.
		/// </summary>
		/// <value>A Y axis label <see cref="string"/></value>
		[Bindable(true),Category("Appearance"),NotifyParentProperty(true),DefaultValue("")]
		public string YLabel
		{
			get { return ylabel; }
			set { ylabel = value; }			
		}

		/// <summary>
		/// private field that determines if the pane title is visible.  Use the public property
		/// <see cref="IsShowTitle"/> to access this value.
		/// </summary>
		private bool isshowtitle = true;
		/// <summary>
		/// Gets or sets the value that determines if the <see cref="ZedGraph.GraphPane.Title"/>
		/// is visible.
		/// </summary>
		/// <value>true to show the pane title, false otherwise</value>
		[Bindable(true),Category("Appearance"),NotifyParentProperty(true),DefaultValue("true")]
		public bool IsShowTitle
		{
			get { return isshowtitle; }
			set { isshowtitle = value; }			
		}

		/// <summary>
		/// private field that determines the output format for the control.  Use the public property
		/// <see cref="OutputFormat"/> to access this value.
		/// </summary>
		private ZedGraphWebFormat outputformat = ZedGraphWebFormat.Jpeg;
		/// <summary>
		/// Gets or sets the value that determines the output format for the control, in the
		/// form of a <see cref="ZedGraphWebFormat"/> enumeration.  This is typically Gif, Jpeg,
		/// Png, or Icon.
		/// </summary>
		/// <value>A <see cref="ZedGraphWebFormat"/> enumeration.</value>
		[Bindable(true),Category("Appearance"),NotifyParentProperty(true),DefaultValue("Jpeg")]
		public ZedGraphWebFormat OutputFormat
		{
			get { return outputformat; }
			set { outputformat = value; }			
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
			if ( handler != null )
			{
				handler( g, pane );
			}
			else
			{// default with the sample graph if no callback provided
				ZedGraphWeb.RenderDemo(g,pane);
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
			GraphPane pane = new GraphPane( rect, title, xlabel, ylabel );
						
			//TODO: add all the goodies from visual mode
			pane.IsShowTitle = this.IsShowTitle;

			Bitmap image = new Bitmap( this.Width, this.Height ); 			
			Graphics g = Graphics.FromImage( image );

			//TODO: add visual designer influences here - first!!

			// Use callback to gather more settings
			OnDrawPane( g, pane );			
			
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
				switch( outputformat )
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
				switch( outputformat )
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

	#region Custom state management
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
				((IStateManager)Curves).LoadViewState(myState[1]);			
		}

		protected override object SaveViewState() 
		{
			object[] myState = new object[2];

			// NOTE: Styles are only saved only if they have been created.

			myState[0] = base.SaveViewState();
			myState[1] = (_curves != null) ? ((IStateManager)_curves).SaveViewState() : null;			

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

			if (_curves != null)
				((IStateManager)_curves).TrackViewState();			
		}
		#endregion Custom state management
	}


	
	/// <summary>
	/// A delegate to handle the rendering event for this control.
	/// </summary>
	/// <param name="g">A <see cref="Graphics"/> object for which the drawing will be done.</param>
	/// <param name="pane">A reference to the <see cref="GraphPane"/>
	/// class to be rendered.</param>
	public delegate void ZedGraphWebControlEventHandler( Graphics g, GraphPane pane );
}
