#region Using directives

using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="GraphPane"/> objects
	/// organized together in some form.
	/// </summary>
	/// 
	/// <author>John Champion</author>
	/// <version> $Revision: 3.1 $ $Date: 2005-01-21 05:05:07 $ </version>
	[Serializable]
	public class MasterPane : CollectionPlus, ICloneable
	{
	#region Fields

		/// <summary>
		/// The rectangle that defines the full area into which all the
		/// <see cref="GraphPane"/> objects will be rendered.  Units are pixels.
		/// Use the public property <see cref="Rect"/> to access this value.
		/// </summary>
		private RectangleF	rect;
		
		/// <summary>Private field that holds the main title of the <see cref="MasterPane"/>.  Use the
		/// public property <see cref="MasterPane.Title"/> to access this value.</summary>
		private string		title;
		
		/// <summary>Private field that determines whether or not the title
		/// will be drawn.  Use the
		/// public property <see cref="MasterPane.IsShowTitle"/> to access this value.</summary>
		private bool		isShowTitle;
		/// <summary>
		/// Private field instance of the <see cref="FontSpec"/> class, which maintains the font attributes
		/// for the <see cref="Title"/>. Use the public property
		/// <see cref="FontSpec"/> to access this class.
		/// </summary>
		private FontSpec	fontSpec;

		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for the
		/// <see cref="Rect"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill	fill;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for the
		/// <see cref="Rect"/>.  Use the public property <see cref="Border"/> to
		/// access this value.
		/// </summary>
		private Border	border;

	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="MasterPane"/> class.
		/// </summary>
		public struct Default
		{
			// Default GraphPane properties
			/// <summary>
			/// The default display mode for the title at the top of the pane
			/// (<see cref="GraphPane.IsShowTitle"/> property).  true to
			/// display a title, false otherwise.
			/// </summary>
			public static bool IsShowTitle = true;
			
			/// <summary>
			/// The default font family for the title
			/// (<see cref="MasterPane.Title"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size (points) for the
			/// <see cref="MasterPane"/> title
			/// (<see cref="ZedGraph.FontSpec.Size"/> property).
			/// </summary>
			public static float FontSize = 16;
			/// <summary>
			/// The default font color for the
			/// <see cref="MasterPane"/> title
			/// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the
			/// <see cref="MasterPane"/> title
			/// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = true;
			/// <summary>
			/// The default font italic mode for the
			/// <see cref="MasterPane"/> title
			/// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
			/// <summary>
			/// The default font underline mode for the
			/// <see cref="MasterPane"/> title
			/// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			
			/// <summary>
			/// The default border mode for the <see cref="GraphPane"/>.
			/// (<see cref="GraphPane.PaneBorder"/> property). true
			/// to draw a border around the <see cref="GraphPane.PaneRect"/>,
			/// false otherwise.
			/// </summary>
			public static bool IsBorderVisible = true;
			/// <summary>
			/// The default color for the <see cref="MasterPane"/> border.
			/// (<see cref="MasterPane.Border"/> property). 
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color for the <see cref="MasterPane.Rect"/> background.
			/// (<see cref="MasterPane.Fill"/> property). 
			/// </summary>
			public static Color FillColor = Color.White;

			/// <summary>
			/// The default pen width for the <see cref="MasterPane"/> border.
			/// (<see cref="MasterPane.Border"/> property).  Units are in points (1/72 inch).
			/// </summary>
			public static float BorderPenWidth = 1;

			/// <summary>
			/// The default value for the <see cref="Default.OuterPaneGap"/> property.
			/// This is the size of the margin around the edge of the
			/// <see cref="MasterPane.Rect"/>, in units of points (1/72 inch).
			/// </summary>
			public static float OuterPaneGap = 20;
			/// <summary>
			/// The default value for the <see cref="Default.InnerPaneGap"/> property.
			/// This is the size of the margin inbetween adjacent <see cref="GraphPane"/>
			/// objects, in units of points (1/72 inch).
			/// </summary>
			public static float InnerPaneGap = 20;
			/// <summary>
			/// The default dimension of the <see cref="MasterPane.Rect"/>, which
			/// defines a normal sized plot.  This dimension is used to scale the
			/// fonts, symbols, etc. according to the actual size of the
			/// <see cref="MasterPane.Rect"/>.
			/// </summary>
			/// <seealso cref="MasterPane.CalcScaleFactor"/>
			public static double BaseDimension = 8.0;
			
		}
	#endregion

	#region Properties

		/// <summary>
		/// The rectangle that defines the full area into which all the
		/// <see cref="GraphPane"/> objects will be rendered.  Units are pixels.
		/// </summary>
		public RectangleF Rect
		{
			get { return rect; }
			set { rect = value; }
		}

		/// <summary>
		/// IsShowTitle is a boolean value that determines whether or not the
		/// <see cref="Title"/> is displayed on the graph.
		/// </summary>
		/// <remarks>If true, the title is displayed.  If false, the title is omitted, and the
		/// screen space that would be occupied by the title is added to the axis area.
		/// </remarks>
		/// <seealso cref="Default.IsShowTitle"/>
		public bool IsShowTitle
		{
			get { return isShowTitle; }
			set { isShowTitle = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="FontSpec"/> class used to render
		/// the <see cref="MasterPane"/> <see cref="Title"/>
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
		/// Gets or sets the <see cref="ZedGraph.Border"/> class for drawing the border
		/// border around the <see cref="Rect"/>
		/// </summary>
		/// <seealso cref="Default.BorderColor"/>
		/// <seealso cref="Default.BorderPenWidth"/>
		public Border Border
		{
			get { return border; }
			set { border = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for the
		/// <see cref="Rect"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}

		/// <summary>
		/// Build an <see cref="Image"/> object containing the graphical rendering of
		/// all the <see cref="GraphPane"/> objects in this list.
		/// </summary>
		/// <value>An <see cref="Image"/> object, which is a <see cref="Bitmap"/> class.</value>
		public Image Image
		{
			get
			{
				// Need to make the bitmap 1 pixel larger than the image for
				// proper containment
				Bitmap bitmap = new Bitmap( (int) this.rect.Width+1,
						(int) this.rect.Height+1 );
				Graphics bitmapGraphics = Graphics.FromImage( bitmap );

				bitmapGraphics.TranslateTransform( -this.rect.Left,
						-this.rect.Top );

				this.Draw( bitmapGraphics );

				bitmapGraphics.Dispose();

				return bitmap;
			}
		}

	#endregion
	
	#region Constructors

		/// <summary>
		/// Default constructor for the collection class.  Leaves the <see cref="Rect"/> empty.
		/// </summary>
		public MasterPane() : this( "", new RectangleF( 0, 0, 0, 0 ) )
		{
		}
		
		/// <summary>
		/// Default constructor for the collection class.  Leaves the <see cref="Rect"/> empty.
		/// </summary>
		public MasterPane( string title, RectangleF rect )
		{
			this.rect = rect;
			if ( title == null )
				this.title = "";
			else
				this.title = title;
				
			if ( this.title.Length == 0 )
				this.IsShowTitle = false;
			else
				this.IsShowTitle = true;
						
			this.Fill = new Fill( Default.FillColor );
			this.Border = new Border( Default.IsBorderVisible, Default.BorderColor,
				Default.BorderPenWidth );
			this.fontSpec = new FontSpec( Default.FontFamily,
				Default.FontSize, Default.FontColor, Default.FontBold,
				Default.FontItalic, Default.FontUnderline,
				Color.White, null, FillType.None );
			this.fontSpec.Border.IsVisible = false;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="MasterPane"/> object from which to copy</param>
		public MasterPane( MasterPane rhs )
		{
			this.fill = (Fill) rhs.fill.Clone();
			this.border = (Border) rhs.border.Clone();
			this.title = (string) rhs.title.Clone();
			this.IsShowTitle = rhs.IsShowTitle;
			this.rect = rhs.Rect;
			this.fontSpec = (FontSpec) rhs.fontSpec.Clone();

			foreach ( GraphPane pane in rhs )
			{
				this.Add( (GraphPane) pane.Clone() );
			}
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="MasterPane"/></returns>
		public object Clone()
		{ 
			return new MasterPane( this ); 
		}
		
	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int master_schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected MasterPane( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "master_schema" );

			this.rect = (RectangleF) info.GetValue( "paneRect", typeof(RectangleF) );
			this.title = info.GetString( "title" );
			this.isShowTitle = info.GetBoolean( "isShowTitle" );
			this.fontSpec = (FontSpec) info.GetValue( "fontSpec" , typeof(FontSpec) );

			this.fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			this.border = (Border) info.GetValue( "border", typeof(Border) );

		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "master_schema", master_schema );

			info.AddValue( "rect", rect );
			info.AddValue( "title", title );
			info.AddValue( "isShowTitle", isShowTitle );
			info.AddValue( "fontSpec", fontSpec );
			info.AddValue( "fill", fill );
			info.AddValue( "border", border );

		}
	#endregion

	#region List Methods
		/// <summary>
		/// Indexer to access the specified <see cref="GraphPane"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="GraphPane"/> object to be accessed.</param>
		/// <value>A <see cref="GraphPane"/> object reference.</value>
		public GraphPane this[ int index ]  
		{
			get { return( (GraphPane) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Indexer to access the specified <see cref="GraphPane"/> object by
		/// its <see cref="GraphPane.Title"/> string.
		/// </summary>
		/// <param name="title">The string title of the
		/// <see cref="GraphPane"/> object to be accessed.</param>
		/// <value>A <see cref="GraphPane"/> object reference.</value>
		public GraphPane this[ string title ]  
		{
			get
			{
				int index = IndexOf( title );
				if ( index >= 0 )
					return( (GraphPane) List[index]  );
				else
					return null;
			}
		}

		/// <summary>
		/// Add a <see cref="GraphPane"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public void Add( GraphPane pane )
		{
			List.Add( pane );
		}

		/// <summary>
		/// Remove a <see cref="GraphPane"/> object from the collection based on an object reference.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is to be
		/// removed.</param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( GraphPane pane )
		{
			List.Remove( pane );
		}

		/// <summary>
		/// Insert a <see cref="GraphPane"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is to be
		/// inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, GraphPane pane )
		{
			List.Insert( index, pane );
		}

		/// <summary>
		/// Return the zero-based position index of the
		/// <see cref="GraphPane"/> with the specified <see cref="GraphPane.Title"/>.
		/// </summary>
		/// <remarks>The comparison of titles is not case sensitive, but it must include
		/// all characters including punctuation, spaces, etc.</remarks>
		/// <param name="title">The <see cref="String"/> label that is in the
		/// <see cref="GraphPane.Title"/> attribute of the item to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="GraphPane"/>,
		/// or -1 if the <see cref="GraphPane.Title"/> was not found in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		/// <seealso cref="IndexOfTag"/>
		public int IndexOf( string title )
		{
			int index = 0;
			foreach ( GraphPane pane in this )
			{
				if ( String.Compare( pane.Title, title, true ) == 0 )
					return index;
				index++;
			}

			return -1;
		}

		/// <summary>
		/// Return the zero-based position index of the
		/// <see cref="GraphPane"/> with the specified <see cref="GraphPane.Tag"/>.
		/// </summary>
		/// <remarks>In order for this method to work, the <see cref="GraphPane.Tag"/>
		/// property must be of type <see cref="String"/>.</remarks>
		/// <param name="tagStr">The <see cref="String"/> tag that is in the
		/// <see cref="GraphPane.Tag"/> attribute of the item to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="GraphPane"/>,
		/// or -1 if the <see cref="GraphPane.Tag"/> string is not in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		/// <seealso cref="IndexOf"/>
		public int IndexOfTag( string tagStr )
		{
			int index = 0;
			foreach ( GraphPane pane in this )
			{
				if ( pane.Tag is string &&
						String.Compare( (string) pane.Tag, tagStr, true ) == 0 )
					return index;
				index++;
			}

			return -1;
		}

		/// <summary>
		/// Call <see cref="GraphPane.AxisChange"/> for all <see cref="GraphPane"/> objects in this
		/// <see cref="MasterPane"/> list.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void AxisChange( Graphics g )
		{
			foreach ( GraphPane pane in this )
				pane.AxisChange( g );
		}

		/// <summary>
		/// Render all the <see cref="GraphPane"/> objects in the list to the specified graphics device.
		/// </summary>
		/// <remarks>This method should be part of the Paint() update process.  Calling this routine
		/// will redraw all
		/// features of all the <see cref="GraphPane"/> items.  No preparation is required other than
		/// instantiated <see cref="GraphPane"/> objects that have been added to the list with the
		/// <see cref="Add"/> method.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void Draw( Graphics g )
		{
			DrawFrame( g );
			
			DrawTitle( g );
			
			foreach ( GraphPane pane in this )
				pane.Draw( g );
		}

		/// <summary>
		/// Draw the <see cref="MasterPane"/> <see cref="Title"/> on the graph
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void DrawTitle( Graphics g )
		{	
			// only draw the title if it's required
			if ( this.isShowTitle )
			{
				double scaleFactor = CalcScaleFactor();
				float scaledOuterGap = (float) ( Default.OuterPaneGap * scaleFactor );
				
				SizeF size = this.fontSpec.BoundingBox( g, this.title, scaleFactor );
				
				// use the internal fontSpec class to draw the text using user-specified and/or
				// default attributes.
				this.fontSpec.Draw( g, false, this.title,
					( this.rect.Left + this.rect.Right ) / 2,
					this.rect.Top + scaledOuterGap + size.Height / 2.0F,
					AlignH.Center, AlignV.Center, scaleFactor );
			}
		}
		
		/// <summary>
		/// Draw the border around the <see cref="Rect"/> area.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>		
		public void DrawFrame( Graphics g )
		{
			RectangleF rect = this.rect;

			// Erase the background, filling it with the specified brush
			Brush brush = this.fill.MakeBrush( rect );
			g.FillRectangle( brush, rect );
			brush.Dispose();

			rect.Width = rect.Width - 1;
			rect.Height = rect.Height - 1;

			// Draw a border around the Rect
			// The Destination image is one pixel larger than the paneRect, because DrawRectangle() draws one
			// pixel beyond the specified rectangle.
			this.border.Draw( g, false, 1.0, rect );
		}

		/// <summary>
		/// Calculate the innerRect rectangle based on the <see cref="Rect"/>.
		/// </summary>
		/// <remarks>The innerRect is the actual area available for <see cref="GraphPane"/>
		/// items after taking out space for the margins and the title.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="Default.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>The calculated axis rect, in pixel coordinates.</returns>
		private RectangleF CalcInnerRect( Graphics g, double scaleFactor )
		{
			// get scaled values for the paneGap and character height
			float scaledOuterGap = (float) ( Default.OuterPaneGap * scaleFactor );
			float charHeight = this.FontSpec.GetHeight( scaleFactor );
				
			// Axis rect starts out at the full pane rect.  It gets reduced to make room for the legend,
			// scales, titles, etc.
			RectangleF innerRect = this.rect;

			innerRect.Inflate( -scaledOuterGap, -scaledOuterGap );

			// Leave room for the title
			if ( this.isShowTitle )
			{
				SizeF titleSize = this.fontSpec.BoundingBox( g, this.title, scaleFactor );
				// Leave room for the title height, plus a line spacing of charHeight/2
				innerRect.Y += titleSize.Height + charHeight / 2.0F;
				innerRect.Height -= titleSize.Height + charHeight / 2.0F;
			}
			
			return innerRect;
		}

		/// <summary>
		/// Automatically set all of the <see cref="GraphPane"/> <see cref="GraphPane.PaneRect"/>'s in
		/// the list to a reasonable configuration.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="paneLayout">A <see cref="PaneLayout"/> enumeration that describes how
		/// the panes should be laid out within the <see cref="Rect"/>.</param>
		public void AutoPaneLayout( Graphics g, PaneLayout paneLayout )
		{
			// calculate scaleFactor on "normal" pane size (BaseDimension)
			double scaleFactor = this.CalcScaleFactor();

			// innerRect is the area for the GraphPane's
			RectangleF innerRect = CalcInnerRect( g, scaleFactor );			

			// scaled InnerGap is the area between the GraphPane.PaneRect's
			float scaledInnerGap = (float) ( Default.InnerPaneGap * scaleFactor );

			int count = this.Count;
			if ( count == 0 )
				return;
			int	rows,
				cols,
				root = (int) (Math.Sqrt( (double) count ) + 0.9999999);
			
			switch ( paneLayout )
			{
				default:
				case PaneLayout.ForceSquare:
					rows = root;
					cols = root;
					break;
				case PaneLayout.SingleColumn:
					rows = count;
					cols = 1;
					break;
				case PaneLayout.SingleRow:
					rows = 1;
					cols = count;
					break;
				case PaneLayout.SquareColPreferred:
					rows = root;
					cols = root;
					if ( count <= root * (root - 1) )
						rows--;
					break;
				case PaneLayout.SquareRowPreferred:
					rows = root;
					cols = root;
					if ( count <= root * (root - 1) )
						cols--;
					break;
			}

			float width = ( innerRect.Width - (float)(cols - 1) * scaledInnerGap ) / (float) cols;
			float height = ( innerRect.Height - (float)(rows - 1) * scaledInnerGap ) / (float) rows;

			int i = 0;
			foreach ( GraphPane pane in this )
			{
				float rowNum = (float) ( i / cols );
				float colNum = (float) ( i % cols );

				pane.PaneRect = new RectangleF(
									innerRect.X + colNum * ( width + scaledInnerGap ),
									innerRect.Y + rowNum * ( height + scaledInnerGap ),
									width,
									height );

				i++;
			}
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
		/// A double precision value representing the scaling factor to use for the rendering calculations.
		/// </returns>
		/// <seealso cref="GraphPane.BaseDimension"/>
		public double CalcScaleFactor()
		{
			double scaleFactor; //, xInch, yInch;
			const double ASPECTLIMIT = 1.5;
			
			// Assume the standard width (BaseDimension) is 8.0 inches
			// Therefore, if the paneRect is 8.0 inches wide, then the fonts will be scaled at 1.0
			// if the paneRect is 4.0 inches wide, the fonts will be half-sized.
			// if the paneRect is 16.0 inches wide, the fonts will be double-sized.
		
			// Scale the size depending on the client area width in linear fashion
			if ( rect.Height <= 0 )
				return 1.0;
			double length = rect.Width;
			double aspect = rect.Width / rect.Height;
			if ( aspect > ASPECTLIMIT )
				length = rect.Height * ASPECTLIMIT;
			if ( aspect < 1.0 / ASPECTLIMIT )
				length = rect.Width * ASPECTLIMIT;

			scaleFactor = length / ( Default.BaseDimension * 72 );

			// Don't let the scaleFactor get ridiculous
			if ( scaleFactor < 0.1 )
				scaleFactor = 0.1;
						
			return scaleFactor;
		}

		/// <summary>
		/// Find the pane and the object within that pane that lies closest to the specified
		/// mouse (screen) point.
		/// </summary>
		/// <remarks>
		/// This method first finds the <see cref="GraphPane"/> within the list that contains
		/// the specified mouse point.  It then calls the <see cref="GraphPane.FindNearestObject"/>
		/// method to determine which object, if any, was clicked.  With the exception of the
		/// <see paramref="pane"/>, all the parameters in this method are identical to those
		/// in the <see cref="GraphPane.FindNearestObject"/> method.
		/// If the mouse point lies within the <see cref="GraphPane.PaneRect"/> of any 
		/// <see cref="GraphPane"/> item, then that pane will be returned (otherwise it will be
		/// null).  Further, within the selected pane, if the mouse point is within the
		/// bounding box of any of the items (or in the case
		/// of <see cref="ArrowItem"/> and <see cref="CurveItem"/>, within
		/// <see cref="GraphPane.Default.NearestTol"/> pixels), then the object will be returned.
		/// You must check the type of the object to determine what object was
		/// selected (for example, "if ( object is Legend ) ...").  The
		/// <see paramref="index"/> parameter returns the index number of the item
		/// within the selected object (such as the point number within a
		/// <see cref="CurveItem"/> object.
		/// </remarks>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that was clicked.</param>
		/// <param name="nearestObj">A reference to the nearest object to the
		/// specified screen point.  This can be any of <see cref="Axis"/>,
		/// <see cref="Legend"/>, <see cref="GraphPane.Title"/>,
		/// <see cref="TextItem"/>, <see cref="ArrowItem"/>, or <see cref="CurveItem"/>.
		/// Note: If the pane title is selected, then the <see cref="GraphPane"/> object
		/// will be returned.
		/// </param>
		/// <param name="index">The index number of the item within the selected object
		/// (where applicable).  For example, for a <see cref="CurveItem"/> object,
		/// <see paramref="index"/> will be the index number of the nearest data point,
		/// accessible via <see cref="CurveItem.Points">CurveItem.Points[index]</see>.
		/// index will be -1 if no data points are available.</param>
		/// <returns>true if a <see cref="GraphPane"/> was found, false otherwise.</returns>
		/// <seealso cref="GraphPane.FindNearestObject"/>
		public bool FindNearestPaneObject( PointF mousePt, Graphics g, out GraphPane pane,
			out object nearestObj, out int index )
		{
			pane = null;
			nearestObj = null;
			index = -1;

			foreach ( GraphPane tPane in this )
			{
				if ( tPane.PaneRect.Contains( mousePt ) )
				{
					pane = tPane;
					return tPane.FindNearestObject( mousePt, g, out nearestObj, out index );
				}
			}

			return false;
		}

	#endregion

	}
}
