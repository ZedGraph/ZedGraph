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

#region Using directives

using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// An abstract base class that defines basic functionality for handling a pane.  This class is the
	/// parent class for <see cref="MasterPane"/> and <see cref="GraphPane"/>.
	/// </summary>
	/// 
	/// <author>John Champion</author>
	/// <version> $Revision: 3.19.2.5 $ $Date: 2006-04-16 07:15:51 $ </version>
	abstract public class PaneBase : ICloneable
	{

	#region Fields

		/// <summary>
		/// The rectangle that defines the full area into which the pane is rendered.  Units are pixels.
		/// Use the public property <see cref="Rect"/> to access this value.
		/// </summary>
		protected RectangleF	_rect;
		
		/// <summary>Private field that holds the main title of the pane.  Use the
		/// public property <see cref="Title"/> to access this value.
		/// </summary>
		protected GapLabel		_title;
		
		/// <summary>Private field instance of the <see cref="ZedGraph.Legend"/> class.  Use the
		/// public property <see cref="PaneBase.Legend"/> to access this class.</summary>
		protected Legend		_legend;

		/// <summary>
		/// Private field that stores the user-defined tag for this <see cref="PaneBase"/>.  This tag
		/// can be any user-defined value.  If it is a <see cref="String"/> type, it can be used as
		/// a parameter to the <see cref="PaneList.IndexOfTag"/> method.  Use the public property
		/// <see cref="Tag"/> to access this value.
		/// </summary>
		protected object _tag;

		/// <summary>
		/// private field to store the margin values for this <see cref="PaneBase" />. Use the
		/// public property <see cref="Margin" /> to access this property.
		/// </summary>
		internal Margin _margin;

		/// <summary>Private field that determines whether or not the fonts, tics, gaps, etc.
		/// will be scaled according to the actual graph size.  true for font and feature scaling
		/// with graph size, false for fixed font sizes (scaleFactor = 1.0 constant).
		/// Use the public property <see cref="IsFontsScaled"/> to access this value. </summary>
		/// <seealso cref="CalcScaleFactor"/>
		/// <seealso cref="IsPenWidthScaled"/>
		protected bool		_isFontsScaled;
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
		protected bool _isPenWidthScaled;

		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for the
		/// <see cref="Rect"/> background.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		protected Fill	_fill;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for the
		/// <see cref="Rect"/> border.  Use the public property <see cref="Border"/> to
		/// access this value.
		/// </summary>
		protected Border	_border;

		/// <summary>Private field instance of the <see cref="ZedGraph.GraphObjList"/> class.  Use the
		/// public property <see cref="GraphObjList"/> to access this class.</summary>
		protected GraphObjList	_graphObjList;

		/// <summary>Private field that determines the base size of the pane, in inches.
		/// Fonts, tics, gaps, etc. are scaled according to this base size.
		/// Use the public property <see cref="BaseDimension"/> to access this value. </summary>
		/// <seealso cref="_isFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		protected float		_baseDimension;

		/// <summary>
		/// private field that stores the gap between the bottom of the pane title and the
		/// client area of the pane.  This is expressed as a fraction of the title character height.
		/// </summary>
		protected float _titleGap;

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
			/// (<see cref="PaneBase.Title"/> <see cref="Label.IsVisible" /> property).  true to
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
			/// (<see cref="PaneBase.Border"/> property). true
			/// to draw a border around the <see cref="PaneBase.Rect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsBorderVisible = true;
			/// <summary>
			/// The default color for the <see cref="PaneBase"/> border.
			/// (<see cref="PaneBase.Border"/> property). 
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="PaneBase.Rect"/> background.
			/// (<see cref="PaneBase.Fill"/> property). 
			/// </summary>
			public static Color FillColor = Color.White;

			/// <summary>
			/// The default pen width for the <see cref="PaneBase"/> border.
			/// (<see cref="PaneBase.Border"/> property).  Units are in points (1/72 inch).
			/// </summary>
			public static float BorderPenWidth = 1;

			/// <summary>
			/// The default dimension of the <see cref="PaneBase.Rect"/>, which
			/// defines a normal sized plot.  This dimension is used to scale the
			/// fonts, symbols, etc. according to the actual size of the
			/// <see cref="PaneBase.Rect"/>.
			/// </summary>
			/// <seealso cref="PaneBase.CalcScaleFactor"/>
			public static float BaseDimension = 8.0F;
			
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
			public static bool IsFontsScaled = true;

			/// <summary>
			/// The default value for the <see cref="PaneBase.TitleGap" /> property, expressed as
			/// a fraction of the scaled <see cref="Title" /> character height.
			/// </summary>
			public static float TitleGap = 0.5f;
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
		/// ZedGraph Rect, a Width of 100 pixels means that pixels 0 through 99 are used</remarks>
		/// <value>Units are pixels.</value>
		/// <seealso cref="ReSize"/>
		public RectangleF Rect
		{
			get { return this._rect; }
			set { this._rect = value; }
		}

		/// <summary>
		/// Accesses the <see cref="Legend"/> for this <see cref="PaneBase"/>
		/// </summary>
		/// <value>A reference to a <see cref="Legend"/> object</value>
		public Legend Legend
		{
			get { return _legend; }
		}

		/// <summary>
		/// Gets the <see cref="Label" /> instance that contains the text and attributes of the title.
		/// This text can be multiple lines separated by newline characters ('\n').
		/// </summary>
		/// <seealso cref="FontSpec"/>
		/// <seealso cref="Default.FontColor"/>
		/// <seealso cref="Default.FontBold"/>
		/// <seealso cref="Default.FontItalic"/>
		/// <seealso cref="Default.FontUnderline"/>
		/// <seealso cref="Default.FontFamily"/>
		/// <seealso cref="Default.FontSize"/>
		public Label Title
		{
			get { return _title; }
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
			get { return _tag; }
			set { _tag = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> class for drawing the border
		/// border around the <see cref="Rect"/>
		/// </summary>
		/// <seealso cref="Default.BorderColor"/>
		/// <seealso cref="Default.BorderPenWidth"/>
		public Border Border
		{
			get { return this._border; }
			set { this._border = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for the
		/// filling the background of the <see cref="Rect"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return this._fill; }
			set { this._fill = value; }
		}

		/// <summary>
		/// Gets or sets the list of <see cref="GraphObj"/> items for this <see cref="GraphPane"/>
		/// </summary>
		/// <value>A reference to a <see cref="GraphObjList"/> collection object</value>
		public GraphObjList GraphObjList
		{
			get { return _graphObjList; }
			set { _graphObjList = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Margin" /> instance that controls the space between
		/// the edge of the <see cref="PaneBase.Rect" /> and the rendered content of the graph.
		/// </summary>
		public Margin Margin
		{
			get { return _margin; }
			set { _margin = value; }
		}

		/// <summary>
		/// BaseDimension is a double precision value that sets "normal" pane size on
		/// which all the settings are based.  The BaseDimension is in inches.  For
		/// example, if the BaseDimension is 8.0 inches and the
		/// <see cref="Title"/> size is 14 points.  Then the pane title font
		/// will be 14 points high when the <see cref="Rect"/> is approximately 8.0
		/// inches wide.  If the Rect is 4.0 inches wide, the pane title font will be
		/// 7 points high.  Most features of the graph are scaled in this manner.
		/// </summary>
		/// <value>The base dimension reference for the <see cref="Rect"/>, in inches</value>
		/// <seealso cref="Default.BaseDimension"/>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		public float BaseDimension
		{
			get { return _baseDimension; }
			set { _baseDimension = value; }
		}

		/// <summary>
		/// Gets or sets the gap between the bottom of the pane title and the
		/// client area of the pane.  This is expressed as a fraction of the scaled
		/// <see cref="Title" /> character height.
		/// </summary>
		public float TitleGap
		{
			get { return _titleGap; }
			set { _titleGap = value; }
		}

		/// <summary>
		/// Determines if the font sizes, tic sizes, gap sizes, etc. will be scaled according to
		/// the size of the <see cref="Rect"/> and the <see cref="BaseDimension"/>.  If this
		/// value is set to false, then the font sizes and tic sizes will always be exactly as
		/// specified, without any scaling.
		/// </summary>
		/// <value>True to have the fonts and tics scaled, false to have them constant</value>
		/// <seealso cref="PaneBase.CalcScaleFactor"/>
		public bool IsFontsScaled
		{
			get { return _isFontsScaled; }
			set { _isFontsScaled = value; }
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
		/// <see cref="GetImage(int,int,float)"/>) where it is desirable to have the pen width
		/// be consistent with the screen image.
		/// </remarks>
		/// <value>true to scale the pen widths according to the size of the graph,
		/// false otherwise.</value>
		/// <seealso cref="IsFontsScaled"/>
		/// <seealso cref="CalcScaleFactor"/>
		public bool IsPenWidthScaled
		{
			get { return _isPenWidthScaled; }
			set { _isPenWidthScaled = value; }
		}

	#endregion
	
	#region Constructors

		/// <summary>
		/// Default constructor for the <see cref="PaneBase"/> class.  Leaves the <see cref="Rect"/> empty.
		/// </summary>
		public PaneBase() : this( "", new RectangleF( 0, 0, 0, 0 ) )
		{
		}
		
		/// <summary>
		/// Default constructor for the <see cref="PaneBase"/> class.  Specifies the <see cref="Title"/> of
		/// the <see cref="PaneBase"/>, and the size of the <see cref="Rect"/>.
		/// </summary>
		public PaneBase( string title, RectangleF paneRect )
		{
			this._rect = paneRect;

			_legend = new Legend();
				
			this._baseDimension = Default.BaseDimension;
			this._margin = new Margin();
			_titleGap = Default.TitleGap;

			this._isFontsScaled = Default.IsFontsScaled;
			this._isPenWidthScaled = Default.IsPenWidthScaled;
			this._fill = new Fill( Default.FillColor );
			this._border = new Border( Default.IsBorderVisible, Default.BorderColor,
				Default.BorderPenWidth );

			this._title = new GapLabel( title, Default.FontFamily,
				Default.FontSize, Default.FontColor, Default.FontBold,
				Default.FontItalic, Default.FontUnderline );
			this._title._fontSpec.Fill.IsVisible = false;
			this._title._fontSpec.Border.IsVisible = false;

			_graphObjList = new GraphObjList();
			
			this._tag = null;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="PaneBase"/> object from which to copy</param>
		public PaneBase( PaneBase rhs )
		{
			// copy over all the value types
			this._isFontsScaled = rhs._isFontsScaled;
			this._isPenWidthScaled = rhs._isPenWidthScaled;

			_titleGap = rhs._titleGap;
			this._baseDimension = rhs._baseDimension;
			this._margin = rhs._margin.Clone();
			this._rect = rhs._rect;

			// Copy the reference types by cloning
			this._fill = rhs._fill.Clone();
			this._border = rhs._border.Clone();
			this._title = rhs._title.Clone();

			_legend = rhs.Legend.Clone();
			this._title = rhs._title.Clone();
			this._graphObjList = rhs._graphObjList.Clone();
			
			if ( rhs._tag is ICloneable )
				this._tag = ((ICloneable) rhs._tag).Clone();
			else
				this._tag = rhs._tag;
		}


		//abstract public object ShallowClone();

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of Clone
		/// </summary>
		/// <remarks>
		/// Note that this method must be called with an explicit cast to ICloneable, and
		/// that it is inherently virtual.  For example:
		/// <code>
		/// ParentClass foo = new ChildClass();
		/// ChildClass bar = (ChildClass) ((ICloneable)foo).Clone();
		/// </code>
		/// Assume that ChildClass is inherited from ParentClass.  Even though foo is declared with
		/// ParentClass, it is actually an instance of ChildClass.  Calling the ICloneable implementation
		/// of Clone() on foo actually calls ChildClass.Clone() as if it were a virtual function.
		/// </remarks>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			throw new NotImplementedException( "Can't clone an abstract base type -- child types must implement ICloneable" );
			//return new PaneBase( this );
		}

		/// <summary>
		/// Create a shallow, memberwise copy of this class.
		/// </summary>
		/// <remarks>
		/// Note that this method uses MemberWiseClone, which will copy all
		/// members (shallow) including those of classes derived from this class.</remarks>
		/// <returns>a new copy of the class</returns>
		public PaneBase ShallowClone()
		{
			// return a shallow copy
			return this.MemberwiseClone() as PaneBase;
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		// schema changed to 2 when Label Class added
		public const int schema = 10;

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

			this._rect = (RectangleF) info.GetValue( "rect", typeof(RectangleF) );
			this._legend = (Legend) info.GetValue( "legend", typeof(Legend) );
			this._title = (GapLabel) info.GetValue( "title", typeof(GapLabel) );
			//this.isShowTitle = info.GetBoolean( "isShowTitle" );
			this._isFontsScaled = info.GetBoolean( "isFontsScaled" );
			this._isPenWidthScaled = info.GetBoolean( "isPenWidthScaled" );
			//this.fontSpec = (FontSpec) info.GetValue( "fontSpec" , typeof(FontSpec) );
			_titleGap = info.GetSingle( "titleGap" );
			this._fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			this._border = (Border) info.GetValue( "border", typeof(Border) );
			this._baseDimension = info.GetSingle( "baseDimension" );
			this._margin = (Margin)info.GetValue( "margin", typeof( Margin ) );
			this._graphObjList = (GraphObjList) info.GetValue( "graphObjList", typeof(GraphObjList) );

			this._tag = info.GetValue( "tag", typeof(object) );

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

			info.AddValue( "rect", _rect );
			info.AddValue( "legend", _legend );
			info.AddValue( "title", _title );
			//info.AddValue( "isShowTitle", isShowTitle );
			info.AddValue( "isFontsScaled", _isFontsScaled );
			info.AddValue( "isPenWidthScaled", _isPenWidthScaled );
			info.AddValue( "titleGap", _titleGap );

			//info.AddValue( "fontSpec", fontSpec );
			info.AddValue( "fill", _fill );
			info.AddValue( "border", _border );
			info.AddValue( "baseDimension", _baseDimension );
			info.AddValue( "margin", _margin );
			info.AddValue( "graphObjList", _graphObjList );

			info.AddValue( "tag", _tag );
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
			if ( this._rect.Width <= 1 || this._rect.Height <= 1 )
				return;

			// calculate scaleFactor on "normal" pane size (BaseDimension)
			float scaleFactor = this.CalcScaleFactor();

			// Fill the pane background and draw a border around it			
			DrawPaneFrame( g, scaleFactor );

			// Clip everything to the rect
			g.SetClip( this._rect );

			// Draw the GraphItems that are behind everything
			this._graphObjList.Draw( g, this, scaleFactor, ZOrder.G_BehindAll );

			// Draw the Pane Title
			DrawTitle( g, scaleFactor );

			// Draw the Legend
			//this.Legend.Draw( g, this, scaleFactor );

			// Reset the clipping
			g.ResetClip();
		}

		/// <summary>
		/// Calculate the client area rectangle based on the <see cref="PaneBase.Rect"/>.
		/// </summary>
		/// <remarks>The client rectangle is the actual area available for <see cref="GraphPane"/>
		/// or <see cref="MasterPane"/> items after taking out space for the margins and the title.
		/// This method does not take out the area required for the <see cref="PaneBase.Legend"/>.
		/// To do so, you must separately call <see cref="ZedGraph.Legend.CalcRect"/>.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.Default.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>The calculated chart rect, in pixel coordinates.</returns>
		public RectangleF CalcClientRect( Graphics g, float scaleFactor )
		{
			// get scaled values for the paneGap and character height
			//float scaledOuterGap = (float) ( Default.OuterPaneGap * scaleFactor );
			float charHeight = this._title._fontSpec.GetHeight( scaleFactor );

			// chart rect starts out at the full pane rect.  It gets reduced to make room for the legend,
			// scales, titles, etc.
			RectangleF innerRect = new RectangleF(
							this._rect.Left + this._margin.Left * scaleFactor,
							this._rect.Top + this._margin.Top * scaleFactor,
							this._rect.Width - scaleFactor * ( this._margin.Left + this._margin.Right ),
							this._rect.Height - scaleFactor * ( this._margin.Top + this._margin.Bottom ) );

			// Leave room for the title
			if ( this._title._isVisible )
			{
				SizeF titleSize = this._title._fontSpec.BoundingBox( g, this._title._text, scaleFactor );
				// Leave room for the title height, plus a line spacing of charHeight * _titleGap
				innerRect.Y += titleSize.Height + charHeight * _titleGap;
				innerRect.Height -= titleSize.Height + charHeight * _titleGap;
			}

			// Calculate the legend rect, and back it out of the current ChartRect
			//this.legend.CalcRect( g, this, scaleFactor, ref innerRect );

			return innerRect;
		}

		/// <summary>
		/// Draw the border _border around the <see cref="Rect"/> area.
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
			// Erase the pane background, filling it with the specified brush
			this._fill.Draw( g, this._rect );

			// Reduce the rect width and height by 1 pixel so that for a rect of
			// new RectangleF( 0, 0, 100, 100 ), which should be 100 pixels wide, we cover
			// from 0 through 99.  The draw routines normally cover from 0 through 100, which is
			// actually 101 pixels wide.
			RectangleF rect = new RectangleF( this._rect.X, this._rect.Y, this._rect.Width - 1, this._rect.Height - 1 );

			this._border.Draw( g, IsPenWidthScaled, scaleFactor, rect );
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
			if ( this._title._isVisible )
			{
				SizeF size = this._title._fontSpec.BoundingBox( g, this._title._text, scaleFactor );
				
				// use the internal fontSpec class to draw the text using user-specified and/or
				// default attributes.
				this._title._fontSpec.Draw( g, this._isPenWidthScaled, this._title._text,
					( this._rect.Left + this._rect.Right ) / 2,
					this._rect.Top + this._margin.Top * (float) scaleFactor + size.Height / 2.0F,
					AlignH.Center, AlignV.Center, scaleFactor );
			}
		}

		/// <summary>
		/// Change the size of the <see cref="Rect"/>.  Override this method to handle resizing the contents
		/// as required.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">The new size for the <see cref="Rect"/>.</param>
		public virtual void ReSize( Graphics g, RectangleF rect )
		{
			this._rect = rect;
		}

		/// <summary>
		/// Calculate the scaling factor based on the ratio of the current <see cref="Rect"/> dimensions and
		/// the <see cref="Default.BaseDimension"/>.
		/// </summary>
		/// <remarks>This scaling factor is used to proportionally scale the
		/// features of the <see cref="MasterPane"/> so that small graphs don't have huge fonts, and vice versa.
		/// The scale factor represents a linear multiple to be applied to font sizes, symbol sizes, tic sizes,
		/// gap sizes, pen widths, etc.  The units of the scale factor are "World Pixels" per "Standard Point".
		/// If any object size, in points, is multiplied by this scale factor, the result is the size, in pixels,
		/// that the object should be drawn using the standard GDI+ drawing instructions.  A "Standard Point"
		/// is a dimension based on points (1/72nd inch) assuming that the <see cref="Rect"/> size
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
			
			// if font scaling is turned off, then always return a 1.0 scale factor
			if ( !this._isFontsScaled )
				return 1.0f;

			// Assume the standard width (BaseDimension) is 8.0 inches
			// Therefore, if the rect is 8.0 inches wide, then the fonts will be scaled at 1.0
			// if the rect is 4.0 inches wide, the fonts will be half-sized.
			// if the rect is 16.0 inches wide, the fonts will be double-sized.
		
			// Scale the size depending on the client area width in linear fashion
			if ( this._rect.Height <= 0 )
				return 1.0F;
			float length = this._rect.Width;
			float aspect = this._rect.Width / this._rect.Height;
			if ( aspect > ASPECTLIMIT )
				length = this._rect.Height * ASPECTLIMIT;
			if ( aspect < 1.0F / ASPECTLIMIT )
				length = this._rect.Width * ASPECTLIMIT;

			scaleFactor = length / ( _baseDimension * 72F );

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
			if ( _isPenWidthScaled )
				return (float)( penWidth * scaleFactor );
			else
				return penWidth;
		}

		/// <summary>
		/// Build a <see cref="Bitmap"/> object containing the graphical rendering of
		/// all the <see cref="GraphPane"/> objects in this list.
		/// </summary>
		/// <value>A <see cref="Bitmap"/> object rendered with the current graph.</value>
		/// <seealso cref="GetImage(int,int,float)"/>
		public Bitmap GetImage()
		{
			Bitmap bitmap = new Bitmap( (int)this._rect.Width, (int)this._rect.Height );
			Graphics bitmapGraphics = Graphics.FromImage( bitmap );

			bitmapGraphics.TranslateTransform( -this._rect.Left, -this._rect.Top );

			this.Draw( bitmapGraphics );

			bitmapGraphics.Dispose();

			return bitmap;
		}

		/// <summary>
		/// Gets an image for the current GraphPane, scaled to the specified size and resolution.
		/// </summary>
		/// <param name="width">The scaled width of the bitmap in pixels</param>
		/// <param name="height">The scaled height of the bitmap in pixels</param>
		/// <param name="dpi">The resolution of the bitmap, in dots per inch</param>
		/// <seealso cref="GetImage()"/>
		/// <seealso cref="Bitmap"/>
		public Bitmap GetImage( int width, int height, float dpi )
		{
			Bitmap bitmap = new Bitmap( width, height );
			bitmap.SetResolution( dpi, dpi );
			Graphics bitmapGraphics = Graphics.FromImage( bitmap );

			//bitmapGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			
			// Clone the GraphPane so we don't mess up the minPix and maxPix values or
			// the rect/ChartRect calculations of the original

			//PaneBase tempPane = (PaneBase) ((ICloneable)this).Clone();
			// This is actually a shallow clone, so we don't duplicate all the data, curveLists, etc.
			PaneBase tempPane = this.ShallowClone();

			tempPane.ReSize( bitmapGraphics, new RectangleF( 0, 0, width, height ) );

			//tempPane.AxisChange( bitmapGraphics );
			tempPane.Draw( bitmapGraphics );
			//this.Draw( bitmapGraphics );

			this.ReSize( bitmapGraphics, this.Rect );

			bitmapGraphics.Dispose();

			return bitmap;
		}

		internal PointF TransformCoord( PointF ptF, CoordType coord )
		{
			// If the Transformation is an illegal type, just stick it in the middle
			if ( !(this is GraphPane) && !( coord == CoordType.PaneFraction ) )
			{
				coord = CoordType.PaneFraction;
				ptF = new PointF( 0.5F, 0.5F );
			}

			// Just to save some casts
			GraphPane gPane = null;
			RectangleF chartRect = new RectangleF( 0, 0, 1, 1 );
			if ( this is GraphPane )
			{
				gPane = this as GraphPane;
				chartRect = gPane.Chart._rect;
			}

			PointF ptPix = new PointF();

			if ( coord == CoordType.ChartFraction )
			{
				ptPix.X = chartRect.Left + ptF.X * chartRect.Width;
				ptPix.Y = chartRect.Top + ptF.Y * chartRect.Height;
			}
			else if ( coord == CoordType.AxisXYScale )
			{
				ptPix.X = gPane.XAxis.Scale.Transform( ptF.X );
				ptPix.Y = gPane.YAxis.Scale.Transform( ptF.Y );
			}
			else if ( coord == CoordType.AxisXY2Scale )
			{
				ptPix.X = gPane.XAxis.Scale.Transform( ptF.X );
				ptPix.Y = gPane.Y2Axis.Scale.Transform( ptF.Y );
			}
			else if ( coord == CoordType.XScaleYChartFraction )
			{
				ptPix.X = gPane.XAxis.Scale.Transform( ptF.X );
				ptPix.Y = chartRect.Top + ptF.Y * chartRect.Height;
			}
			else if ( coord == CoordType.XChartFractionYScale )
			{
				ptPix.X = chartRect.Left + ptF.X * chartRect.Width;
				ptPix.Y = gPane.YAxis.Scale.Transform( ptF.Y );
			}
			else if ( coord == CoordType.XChartFractionY2Scale )
			{
				ptPix.X = chartRect.Left + ptF.X * chartRect.Width;
				ptPix.Y = gPane.Y2Axis.Scale.Transform( ptF.Y );
			}
			else if ( coord == CoordType.XChartFractionYPaneFraction )
			{
				ptPix.X = chartRect.Left + ptF.X * chartRect.Width;
				ptPix.Y = this.Rect.Top + ptF.Y * this._rect.Height;
			}
			else if ( coord == CoordType.XPaneFractionYChartFraction )
			{
				ptPix.X = this.Rect.Left + ptF.X * this._rect.Width;
				ptPix.Y = chartRect.Top + ptF.Y * chartRect.Height;
			}
			else	// PaneFraction
			{
				ptPix.X = this._rect.Left + ptF.X * this._rect.Width;
				ptPix.Y = this._rect.Top + ptF.Y * this._rect.Height;
			}

			return ptPix;
		}

	#endregion

	}
}
