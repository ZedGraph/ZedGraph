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

#region Using directives

using System;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// An abstract base class that defines basic functionality for handling a pane.  This class is the
	/// parent class for <see cref="MasterPane"/> and <see cref="GraphPane"/>.
	/// </summary>
	public class PaneBase : ICloneable
	{

	#region Fields

		/// <summary>
		/// The rectangle that defines the full area into which the pane is rendered.  Units are pixels.
		/// Use the public property <see cref="PaneRect"/> to access this value.
		/// </summary>
		protected RectangleF	paneRect;
		
		/// <summary>Private field that holds the main title of the pane.  Use the
		/// public property <see cref="Title"/> to access this value.
		/// </summary>
		protected string		title;
		
		/// <summary>
		/// Private field that stores the user-defined tag for this <see cref="PaneBase"/>.  This tag
		/// can be any user-defined value.  If it is a <see cref="String"/> type, it can be used as
		/// a parameter to the <see cref="PaneList.IndexOfTag"/> method.  Use the public property
		/// <see cref="Tag"/> to access this value.
		/// </summary>
		protected object tag;

		/// <summary>Private field that determines whether or not the title
		/// will be drawn.  Use the public property <see cref="IsShowTitle"/> to
		/// access this value.
		/// </summary>
		protected bool		isShowTitle;
		/// <summary>Private field that determines whether or not the fonts, tics, gaps, etc.
		/// will be scaled according to the actual graph size.  true for font and feature scaling
		/// with graph size, false for fixed font sizes (scaleFactor = 1.0 constant).
		/// Use the public property <see cref="IsFontsScaled"/> to access this value. </summary>
		/// <seealso cref="CalcScaleFactor"/>
		/// <seealso cref="IsPenWidthScaled"/>
		protected bool		isFontsScaled;
		/// <summary>
		/// Private field that controls whether or not pen widths are scaled according to the
		/// size of the graph.  This value is only applicable if <see cref="IsFontsScaled"/>
		/// is true.  If <see cref="IsFontsScaled"/> is false, then no scaling will be done,
		/// regardless of the value of <see cref="IsPenWidthScaled"/>.
		/// </summary>
		/// <value>true to scale the pen widths according to the size of the graph,
		/// false otherwise.</value>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		protected bool isPenWidthScaled;
		/// <summary>
		/// Private field instance of the <see cref="FontSpec"/> class, which maintains the font attributes
		/// for the <see cref="Title"/>. Use the public property <see cref="FontSpec"/> to access this class.
		/// </summary>
		protected FontSpec	fontSpec;

		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for the
		/// <see cref="PaneRect"/> background.  Use the public property <see cref="PaneFill"/> to
		/// access this value.
		/// </summary>
		protected Fill	paneFill;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for the
		/// <see cref="PaneRect"/> border.  Use the public property <see cref="PaneBorder"/> to
		/// access this value.
		/// </summary>
		protected Border	paneBorder;

		/// <summary>Private field instance of the <see cref="ZedGraph.GraphItemList"/> class.  Use the
		/// public property <see cref="GraphItemList"/> to access this class.</summary>
		protected GraphItemList	graphItemList;

		/// <summary>Private field that determines the base size of the pane, in inches.
		/// Fonts, tics, gaps, etc. are scaled according to this base size.
		/// Use the public property <see cref="BaseDimension"/> to access this value. </summary>
		/// <seealso cref="isFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		protected float		baseDimension;

		/// <summary>
		/// Private fields that store the size of the margin around the edge of the pane which will be
		/// kept blank.  Use the public properties <see cref="MarginLeft"/>, <see cref="MarginRight"/>,
		/// <see cref="MarginTop"/>, <see cref="MarginBottom"/> to access these values.
		/// </summary>
		/// <value>Units are points (1/72 inch)</value>
		protected float	marginLeft,
						marginRight,
						marginTop,
						marginBottom;

	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the default property values for the <see cref="PaneBase"/> class.
		/// </summary>
		public struct Default
		{
			// Default GraphPane properties
			/// <summary>
			/// The default display mode for the title at the top of the pane
			/// (<see cref="PaneBase.IsShowTitle"/> property).  true to
			/// display a title, false otherwise.
			/// </summary>
			public static bool IsShowTitle = true;
			
			/// <summary>
			/// The default font family for the title
			/// (<see cref="PaneBase.Title"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size (points) for the
			/// <see cref="PaneBase.Title"/> (<see cref="ZedGraph.FontSpec.Size"/> property).
			/// </summary>
			public static float FontSize = 16;
			/// <summary>
			/// The default font color for the
			/// <see cref="PaneBase.Title"/>
			/// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the
			/// <see cref="PaneBase.Title"/>
			/// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = true;
			/// <summary>
			/// The default font italic mode for the
			/// <see cref="PaneBase.Title"/>
			/// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
			/// <summary>
			/// The default font underline mode for the
			/// <see cref="PaneBase.Title"/>
			/// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			
			/// <summary>
			/// The default border mode for the <see cref="PaneBase"/>.
			/// (<see cref="PaneBase.PaneBorder"/> property). true
			/// to draw a border around the <see cref="PaneBase.PaneRect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsBorderVisible = true;
			/// <summary>
			/// The default color for the <see cref="PaneBase"/> border.
			/// (<see cref="PaneBase.PaneBorder"/> property). 
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="PaneBase.PaneRect"/> background.
			/// (<see cref="PaneBase.PaneFill"/> property). 
			/// </summary>
			public static Color FillColor = Color.White;

			/// <summary>
			/// The default pen width for the <see cref="PaneBase"/> border.
			/// (<see cref="PaneBase.PaneBorder"/> property).  Units are in points (1/72 inch).
			/// </summary>
			public static float BorderPenWidth = 1;

			/// <summary>
			/// The default dimension of the <see cref="PaneBase.PaneRect"/>, which
			/// defines a normal sized plot.  This dimension is used to scale the
			/// fonts, symbols, etc. according to the actual size of the
			/// <see cref="PaneBase.PaneRect"/>.
			/// </summary>
			/// <seealso cref="PaneBase.CalcScaleFactor"/>
			public static float BaseDimension = 8.0F;
			
			/// <summary>
			/// The default value for the <see cref="PaneBase.MarginLeft"/> property, which is
			/// the size of the space on the left side of the <see cref="PaneBase.PaneRect"/>.
			/// </summary>
			/// <value>Units are points (1/72 inch)</value>
			public static float MarginLeft = 20.0F;
			/// <summary>
			/// The default value for the <see cref="PaneBase.MarginRight"/> property, which is
			/// the size of the space on the right side of the <see cref="PaneBase.PaneRect"/>.
			/// </summary>
			/// <value>Units are points (1/72 inch)</value>
			public static float MarginRight = 20.0F;
			/// <summary>
			/// The default value for the <see cref="PaneBase.MarginTop"/> property, which is
			/// the size of the space on the top side of the <see cref="PaneBase.PaneRect"/>.
			/// </summary>
			/// <value>Units are points (1/72 inch)</value>
			public static float MarginTop = 20.0F;
			/// <summary>
			/// The default value for the <see cref="PaneBase.MarginBottom"/> property, which is
			/// the size of the space on the bottom side of the <see cref="PaneBase.PaneRect"/>.
			/// </summary>
			/// <value>Units are points (1/72 inch)</value>
			public static float MarginBottom = 20.0F;

			/// <summary>
			/// The default setting for the <see cref="PaneBase.IsPenWidthScaled"/> option.
			/// true to have all pen widths scaled according to <see cref="PaneBase.BaseDimension"/>,
			/// false otherwise.
			/// </summary>
			/// <seealso cref="PaneBase.CalcScaleFactor"/>
			public static bool IsPenWidthScaled = false;
			/// <summary>
			/// The default setting for the <see cref="PaneBase.IsFontsScaled"/> option.
			/// true to have all fonts scaled according to <see cref="PaneBase.BaseDimension"/>,
			/// false otherwise.
			/// </summary>
			/// <seealso cref="PaneBase.CalcScaleFactor"/>
			public static bool IsFontsScaled = false;
		}
	#endregion

	#region Properties

		/// <summary>
		/// The rectangle that defines the full area into which all graphics
		/// will be rendered.
		/// </summary>
		/// <remarks>Note that this rectangle has x, y, width, and height.  Most of the
		/// GDI+ graphic primitive actually draw one pixel beyond those dimensions.  For
		/// example, for a rectangle of ( X=0, Y=0, Width=100, Height=100 ), GDI+ would
		/// draw into pixels 0 through 100, which is actually 101 pixels.  For the
		/// ZedGraph PaneRect, a Width of 100 pixels means that pixels 0 through 99 are used</remarks>
		/// <value>Units are pixels.</value>
		/// <seealso cref="ReSize"/>
		public RectangleF PaneRect
		{
			get { return paneRect; }
			set { paneRect = value; }
		}

		/// <summary>
		/// IsShowTitle is a boolean value that determines whether or not the
		/// <see cref="Title"/> is displayed on the graph.
		/// </summary>
		/// <remarks>If true, the title is displayed.  If false, the title is omitted, and the
		/// display space that would be occupied by the title is made available for other graphics.
		/// </remarks>
		/// <seealso cref="Default.IsShowTitle"/>
		public bool IsShowTitle
		{
			get { return isShowTitle; }
			set { isShowTitle = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="FontSpec"/> class used to render
		/// the <see cref="PaneBase.Title"/>
		/// </summary>
		/// <seealso cref="Default.FontColor"/>
		/// <seealso cref="Default.FontBold"/>
		/// <seealso cref="Default.FontItalic"/>
		/// <seealso cref="Default.FontUnderline"/>
		/// <seealso cref="Default.FontFamily"/>
		/// <seealso cref="Default.FontSize"/>
		public FontSpec FontSpec
		{
			get { return fontSpec; }
			set { fontSpec = value; }
		}
		/// <summary>
		/// Title is a string representing the title text.  This text can be multiple lines,
		/// separated by newline characters ('\n').
		/// </summary>
		/// <seealso cref="FontSpec"/>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		/// <summary>
		/// Gets or sets the user-defined tag for this <see cref="PaneBase"/>.  This tag
		/// can be any user-defined value.  If it is a <see cref="String"/> type, it can be used as
		/// a parameter to the <see cref="PaneList.IndexOfTag"/> method.
		/// </summary>
		/// <remarks>
		/// Note that, if you are going to Serialize ZedGraph data, then any type
		/// that you store in <see cref="Tag"/> must be a serializable type (or
		/// it will cause an exception).
		/// </remarks>
		public object Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> class for drawing the border
		/// border around the <see cref="PaneRect"/>
		/// </summary>
		/// <seealso cref="Default.BorderColor"/>
		/// <seealso cref="Default.BorderPenWidth"/>
		public Border PaneBorder
		{
			get { return paneBorder; }
			set { paneBorder = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for the
		/// filling the background of the <see cref="PaneRect"/>.
		/// </summary>
		public Fill	PaneFill
		{
			get { return paneFill; }
			set { paneFill = value; }
		}

		/// <summary>
		/// Gets or sets a float value that determines the margin area between the left edge of the
		/// <see cref="PaneRect"/> rectangle and the features of the graph.
		/// </summary>
		/// <value>This value is in units of points (1/72 inch), and is scaled
		/// linearly with the graph size.</value>
		/// <seealso cref="Default.MarginLeft"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="MarginRight"/>
		/// <seealso cref="MarginTop"/>
		/// <seealso cref="MarginBottom"/>
		public float MarginLeft
		{
			get { return marginLeft; }
			set { marginLeft = value; }
		}
		/// <summary>
		/// Gets or sets a float value that determines the margin area between the right edge of the
		/// <see cref="PaneRect"/> rectangle and the features of the graph.
		/// </summary>
		/// <value>This value is in units of points (1/72 inch), and is scaled
		/// linearly with the graph size.</value>
		/// <seealso cref="Default.MarginRight"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="MarginLeft"/>
		/// <seealso cref="MarginTop"/>
		/// <seealso cref="MarginBottom"/>
		public float MarginRight
		{
			get { return marginRight; }
			set { marginRight = value; }
		}
		/// <summary>
		/// Gets or sets a float value that determines the margin area between the top edge of the
		/// <see cref="PaneRect"/> rectangle and the features of the graph.
		/// </summary>
		/// <value>This value is in units of points (1/72 inch), and is scaled
		/// linearly with the graph size.</value>
		/// <seealso cref="Default.MarginTop"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="MarginLeft"/>
		/// <seealso cref="MarginRight"/>
		/// <seealso cref="MarginBottom"/>
		public float MarginTop
		{
			get { return marginTop; }
			set { marginTop = value; }
		}
		/// <summary>
		/// Gets or sets a float value that determines the margin area between the bottom edge of the
		/// <see cref="PaneRect"/> rectangle and the features of the graph.
		/// </summary>
		/// <value>This value is in units of points (1/72 inch), and is scaled
		/// linearly with the graph size.</value>
		/// <seealso cref="Default.MarginBottom"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="MarginLeft"/>
		/// <seealso cref="MarginRight"/>
		/// <seealso cref="MarginTop"/>
		public float MarginBottom
		{
			get { return marginBottom; }
			set { marginBottom = value; }
		}

		/// <summary>
		/// Gets or sets the list of <see cref="GraphItem"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="GraphItemList"/> collection object</value>
		public GraphItemList GraphItemList
		{
			get { return graphItemList; }
			set { graphItemList = value; }
		}

		/// <summary>
		/// BaseDimension is a double precision value that sets "normal" pane size on
		/// which all the settings are based.  The BaseDimension is in inches.  For
		/// example, if the BaseDimension is 8.0 inches and the
		/// <see cref="Title"/> size is 14 points.  Then the pane title font
		/// will be 14 points high when the <see cref="PaneRect"/> is approximately 8.0
		/// inches wide.  If the PaneRect is 4.0 inches wide, the pane title font will be
		/// 7 points high.  Most features of the graph are scaled in this manner.
		/// </summary>
		/// <value>The base dimension reference for the <see cref="PaneRect"/>, in inches</value>
		/// <seealso cref="Default.BaseDimension"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		public float BaseDimension
		{
			get { return baseDimension; }
			set { baseDimension = value; }
		}

		/// <summary>
		/// Determines if the font sizes, tic sizes, gap sizes, etc. will be scaled according to
		/// the size of the <see cref="PaneRect"/> and the <see cref="BaseDimension"/>.  If this
		/// value is set to false, then the font sizes and tic sizes will always be exactly as
		/// specified, without any scaling.
		/// </summary>
		/// <value>True to have the fonts and tics scaled, false to have them constant</value>
		/// <seealso cref="PaneBase.CalcScaleFactor"/>
		public bool IsFontsScaled
		{
			get { return isFontsScaled; }
			set { isFontsScaled = value; }
		}
		/// <summary>
		/// Gets or sets the property that controls whether or not pen widths are scaled for this
		/// <see cref="PaneBase"/>.
		/// </summary>
		/// <remarks>This value is only applicable if <see cref="IsFontsScaled"/>
		/// is true.  If <see cref="IsFontsScaled"/> is false, then no scaling will be done,
		/// regardless of the value of <see cref="IsPenWidthScaled"/>.  Note that scaling the pen
		/// widths can cause "artifacts" to appear at typical screen resolutions.  This occurs
		/// because of roundoff differences; in some cases the pen width may round to 1 pixel wide
		/// and in another it may round to 2 pixels wide.  The result is typically undesirable.
		/// Therefore, this option defaults to false.  This option is primarily useful for high
		/// resolution output, such as printer output or high resolution bitmaps (from
		/// <see cref="ScaledImage"/>) where it is desirable to have the pen width
		/// be consistent with the screen image.
		/// </remarks>
		/// <value>true to scale the pen widths according to the size of the graph,
		/// false otherwise.</value>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		public bool IsPenWidthScaled
		{
			get { return isPenWidthScaled; }
			set { isPenWidthScaled = value; }
		}

		/// <summary>
		/// Build a <see cref="Bitmap"/> object containing the graphical rendering of
		/// all the <see cref="GraphPane"/> objects in this list.
		/// </summary>
		/// <value>A <see cref="Bitmap"/> object rendered with the current graph.</value>
		public Bitmap Image
		{
			get
			{
				Bitmap bitmap = new Bitmap( (int) this.paneRect.Width,
						(int) this.paneRect.Height );
				Graphics bitmapGraphics = Graphics.FromImage( bitmap );

				bitmapGraphics.TranslateTransform( -this.paneRect.Left,
						-this.paneRect.Top );

				this.Draw( bitmapGraphics );

				bitmapGraphics.Dispose();

				return bitmap;
			}
		}

	#endregion
	
	#region Constructors

		/// <summary>
		/// Default constructor for the <see cref="PaneBase"/> class.  Leaves the <see cref="PaneRect"/> empty.
		/// </summary>
		public PaneBase() : this( "", new RectangleF( 0, 0, 0, 0 ) )
		{
		}
		
		/// <summary>
		/// Default constructor for the <see cref="PaneBase"/> class.  Specifies the <see cref="Title"/> of
		/// the <see cref="PaneBase"/>, and the size of the <see cref="PaneRect"/>.
		/// </summary>
		public PaneBase( string title, RectangleF paneRect )
		{
			this.paneRect = paneRect;
			if ( title == null )
				this.title = "";
			else
				this.title = title;
				
			if ( this.title.Length == 0 )
				this.IsShowTitle = false;
			else
				this.IsShowTitle = true;

			this.baseDimension = Default.BaseDimension;
			this.marginLeft = Default.MarginLeft;
			this.marginRight = Default.MarginRight;
			this.marginTop = Default.MarginTop;
			this.marginBottom = Default.MarginBottom;
						
			this.isFontsScaled = Default.IsFontsScaled;
			this.isPenWidthScaled = Default.IsPenWidthScaled;
			this.paneFill = new Fill( Default.FillColor );
			this.paneBorder = new Border( Default.IsBorderVisible, Default.BorderColor,
				Default.BorderPenWidth );
			this.fontSpec = new FontSpec( Default.FontFamily,
				Default.FontSize, Default.FontColor, Default.FontBold,
				Default.FontItalic, Default.FontUnderline,
				Color.White, null, FillType.None );
			this.fontSpec.Border.IsVisible = false;

			graphItemList = new GraphItemList();
			
			this.tag = null;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="PaneBase"/> object from which to copy</param>
		public PaneBase( PaneBase rhs )
		{
			this.paneFill = (Fill) rhs.paneFill.Clone();
			this.paneBorder = (Border) rhs.paneBorder.Clone();
			this.title = (string) rhs.title.Clone();
			this.IsShowTitle = rhs.isShowTitle;
			this.isFontsScaled = rhs.isFontsScaled;
			this.isPenWidthScaled = rhs.isPenWidthScaled;

			this.marginLeft = rhs.marginLeft;
			this.marginRight = rhs.marginRight;
			this.marginTop = rhs.marginTop;
			this.marginBottom = rhs.marginBottom;

			this.paneRect = rhs.paneRect;
			this.fontSpec = (FontSpec) rhs.fontSpec.Clone();
			this.graphItemList = (GraphItemList) rhs.graphItemList.Clone();
			
			if ( rhs.tag is ICloneable )
				this.tag = ((ICloneable) rhs.tag).Clone();
			else
				this.tag = rhs.tag;
					
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="PaneBase"/></returns>
		public virtual object Clone()
		{ 
			return new PaneBase( this ); 
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected PaneBase( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			this.paneRect = (RectangleF) info.GetValue( "paneRect", typeof(RectangleF) );
			this.title = info.GetString( "title" );
			this.isShowTitle = info.GetBoolean( "isShowTitle" );
			this.isFontsScaled = info.GetBoolean( "isFontsScaled" );
			this.isPenWidthScaled = info.GetBoolean( "isPenWidthScaled" );
			this.fontSpec = (FontSpec) info.GetValue( "fontSpec" , typeof(FontSpec) );

			this.paneFill = (Fill) info.GetValue( "paneFill", typeof(Fill) );
			this.paneBorder = (Border) info.GetValue( "paneBorder", typeof(Border) );
			this.baseDimension = info.GetSingle( "baseDimension" );
			this.graphItemList = (GraphItemList) info.GetValue( "graphItemList", typeof(GraphItemList) );

			this.marginLeft = info.GetSingle( "marginLeft" );
			this.marginRight = info.GetSingle( "marginRight" );
			this.marginTop = info.GetSingle( "marginTop" );
			this.marginBottom = info.GetSingle( "marginBottom" );
			this.tag = info.GetValue( "tag", typeof(object) );

		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );

			info.AddValue( "paneRect", paneRect );
			info.AddValue( "title", title );
			info.AddValue( "isShowTitle", isShowTitle );
			info.AddValue( "isFontsScaled", isFontsScaled );
			info.AddValue( "isPenWidthScaled", isPenWidthScaled );
			info.AddValue( "fontSpec", fontSpec );
			info.AddValue( "paneFill", paneFill );
			info.AddValue( "paneBorder", paneBorder );
			info.AddValue( "baseDimension", baseDimension );
			info.AddValue( "graphItemList", graphItemList );

			info.AddValue( "marginLeft", marginLeft );
			info.AddValue( "marginRight", marginRight );
			info.AddValue( "marginTop", marginTop );
			info.AddValue( "marginBottom", marginBottom );

			info.AddValue( "tag", tag );
		}
	#endregion

	#region Methods

		/// <summary>
		/// Do all rendering associated with this <see cref="PaneBase"/> to the specified
		/// <see cref="Graphics"/> device.  This abstract method is implemented by the child
		/// classes.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public virtual void Draw( Graphics g  )
		{
			// calculate scaleFactor on "normal" pane size (BaseDimension)
			float scaleFactor = this.CalcScaleFactor();

			// Fill the pane background and draw a border around it			
			DrawPaneFrame( g, scaleFactor );

			// Clip everything to the paneRect
			g.SetClip( this.paneRect );

			// Draw the GraphItems that are behind everything
			this.graphItemList.Draw( g, this, scaleFactor, ZOrder.G_BehindAll );

			// Draw the Pane Title
			DrawTitle( g, scaleFactor );

			// Reset the clipping
			g.ResetClip();
		}

		/// <summary>
		/// Draw the border border around the <see cref="PaneRect"/> area.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>		
		public void DrawPaneFrame( Graphics g, float scaleFactor )
		{
			// Reduce the paneRect width and height by 1 pixel so that for a paneRect of
			// new RectangleF( 0, 0, 100, 100 ), which should be 100 pixels wide, we cover
			// from 0 through 99.  The draw routines normally cover from 0 through 100, which is
			// actually 101 pixels wide.
			RectangleF rect = new RectangleF( paneRect.X, paneRect.Y, paneRect.Width - 1, paneRect.Height - 1 );

			// Erase the pane background, filling it with the specified brush
			Brush brush = this.paneFill.MakeBrush( rect );
			g.FillRectangle( brush, rect );
			brush.Dispose();

			this.paneBorder.Draw( g, isPenWidthScaled, scaleFactor, rect );
		}

		/// <summary>
		/// Draw the <see cref="Title"/> on the graph, centered at the top of the pane.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>		
		public void DrawTitle( Graphics g, float scaleFactor )
		{	
			// only draw the title if it's required
			if ( this.isShowTitle )
			{
				SizeF size = this.FontSpec.BoundingBox( g, this.title, scaleFactor );
				
				// use the internal fontSpec class to draw the text using user-specified and/or
				// default attributes.
				this.FontSpec.Draw( g, this.isPenWidthScaled, this.title,
					( this.paneRect.Left + this.paneRect.Right ) / 2,
					this.paneRect.Top + this.marginTop * (float) scaleFactor + size.Height / 2.0F,
					AlignH.Center, AlignV.Center, scaleFactor );
			}
		}

		/// <summary>
		/// Change the size of the <see cref="PaneRect"/>.  Override this method to handle resizing the contents
		/// as required.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="paneRect">The new size for the <see cref="PaneRect"/>.</param>
		public virtual void ReSize( Graphics g, RectangleF paneRect )
		{
			this.paneRect = paneRect;
		}

		/// <summary>
		/// Calculate the scaling factor based on the ratio of the current <see cref="PaneRect"/> dimensions and
		/// the <see cref="Default.BaseDimension"/>.
		/// </summary>
		/// <remarks>This scaling factor is used to proportionally scale the
		/// features of the <see cref="MasterPane"/> so that small graphs don't have huge fonts, and vice versa.
		/// The scale factor represents a linear multiple to be applied to font sizes, symbol sizes, tic sizes,
		/// gap sizes, pen widths, etc.  The units of the scale factor are "World Pixels" per "Standard Point".
		/// If any object size, in points, is multiplied by this scale factor, the result is the size, in pixels,
		/// that the object should be drawn using the standard GDI+ drawing instructions.  A "Standard Point"
		/// is a dimension based on points (1/72nd inch) assuming that the <see cref="PaneRect"/> size
		/// matches the <see cref="Default.BaseDimension"/>.
		/// Note that "World Pixels" will still be transformed by the GDI+ transform matrices to result
		/// in "Output Device Pixels", but "World Pixels" are the reference basis for the drawing commands.
		/// </remarks>
		/// <returns>
		/// A <see cref="Single"/> value representing the scaling factor to use for the rendering calculations.
		/// </returns>
		/// <seealso cref="PaneBase.BaseDimension"/>
		public float CalcScaleFactor()
		{
			float scaleFactor; //, xInch, yInch;
			const float ASPECTLIMIT = 1.5F;
			
			// Assume the standard width (BaseDimension) is 8.0 inches
			// Therefore, if the paneRect is 8.0 inches wide, then the fonts will be scaled at 1.0
			// if the paneRect is 4.0 inches wide, the fonts will be half-sized.
			// if the paneRect is 16.0 inches wide, the fonts will be double-sized.
		
			// Scale the size depending on the client area width in linear fashion
			if ( paneRect.Height <= 0 )
				return 1.0F;
			float length = paneRect.Width;
			float aspect = paneRect.Width / paneRect.Height;
			if ( aspect > ASPECTLIMIT )
				length = paneRect.Height * ASPECTLIMIT;
			if ( aspect < 1.0F / ASPECTLIMIT )
				length = paneRect.Width * ASPECTLIMIT;

			scaleFactor = length / ( baseDimension * 72F );

			// Don't let the scaleFactor get ridiculous
			if ( scaleFactor < 0.1F )
				scaleFactor = 0.1F;
						
			return scaleFactor;
		}

		/// <summary>
		/// Calculate the scaled pen width, taking into account the scaleFactor and the
		/// setting of the <see cref="IsPenWidthScaled"/> property of the pane.
		/// </summary>
		/// <param name="penWidth">The pen width, in points (1/72 inch)</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>The scaled pen width, in world pixels</returns>
		public float ScaledPenWidth( float penWidth, float scaleFactor )
		{
			if ( isPenWidthScaled )
				return (float)( penWidth * scaleFactor );
			else
				return penWidth;
		}

		/// <summary>
		/// Gets an image for the current GraphPane, scaled to the specified size and resolution.
		/// </summary>
		/// <param name="width">The scaled width of the bitmap in pixels</param>
		/// <param name="height">The scaled height of the bitmap in pixels</param>
		/// <param name="dpi">The resolution of the bitmap, in dots per inch</param>
		/// <seealso cref="Image"/>
		/// <seealso cref="Bitmap"/>
		public Bitmap ScaledImage( int width, int height, float dpi )
		{
			Bitmap bitmap = new Bitmap( width, height );
			bitmap.SetResolution( dpi, dpi );
			Graphics bitmapGraphics = Graphics.FromImage( bitmap );
			
			// Clone the GraphPane so we don't mess up the minPix and maxPix values or
			// the paneRect/axisRect calculations of the original

			PaneBase tempPane = (PaneBase) this.Clone();

			tempPane.ReSize( bitmapGraphics, new RectangleF( 0, 0, width, height ) );

			//tempPane.AxisChange( bitmapGraphics );
			tempPane.Draw( bitmapGraphics );
			//this.Draw( bitmapGraphics );
			bitmapGraphics.Dispose();

			return bitmap;
		}

	#endregion

	}
}
