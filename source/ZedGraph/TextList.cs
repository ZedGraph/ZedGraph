using System;
using System.Drawing;
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="TextItem"/> objects
	/// to be displayed on the graph.
	/// </summary>
	public class TextList : CollectionBase, ICloneable
	{
		/// <summary>
		/// Default constructor for the <see cref="TextList"/> collection class
		/// </summary>
		public TextList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The TextList object from which to copy</param>
		public TextList( TextList rhs )
		{
			foreach ( TextItem item in rhs )
				this.Add( new TextItem( item ) );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the TextList</returns>
		public object Clone()
		{ 
			return new TextList( this ); 
		}
		
		/// <summary>
		/// Indexer to access the specified <see cref="TextItem"/> object by its ordinal
		/// position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="TextItem"/> object to be accessed.</param>
		/// <value>A <see cref="TextItem"/> object reference.</value>
		public TextItem this[ int index ]  
		{
			get { return( (TextItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="TextItem"/> object to the <see cref="TextList"/>
		/// collection at the end of the list.
		/// </summary>
		/// <param name="item">A reference to the <see cref="TextItem"/> object to
		/// be added</param>
		public void Add( TextItem item )
		{
			List.Add( item );
		}

		/// <summary>
		/// Remove a <see cref="TextItem"/> object from the <see cref="TextList"/>
		/// collection at the specified ordinal location.
		/// </summary>
		/// <param name="index">An ordinal position in the list at which
		/// the object to be removed is located. </param>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Render text to the specified <see cref="Graphics"/> device
		/// by calling the Draw method of each <see cref="TextItem"/> object in
		/// the collection.  This method is normally only called by the Draw method
		/// of the parent <see cref="GraphPane"/> object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			foreach ( TextItem item in this )
				item.Draw( g, pane, scaleFactor );
		}
	}

	/// <summary>
	/// A class that represents a text object on the graph.  A list of
	/// <see cref="TextItem"/> objects is maintained by the
	/// <see cref="TextList"/> collection class.
	/// </summary>
	public class TextItem : ICloneable
	{
		private string		text;
		private FontAlignV	alignV;
		private FontAlignH	alignH;

		private float		x,
							y;
		private CoordType	coordinateFrame;
		private FontSpec	fontSpec;

		/// <overloads>
		/// Constructors for the <see cref="TextItem"/> class.
		/// </overloads>
		/// <summary>
		/// Default constructor that sets all <see cref="TextItem"/> properties to default
		/// values as defined in the <see cref="Def"/> class.
		/// </summary>
		public TextItem()
		{
			Init();
		}

		/// <summary>
		/// Initialization method that sets all <see cref="TextItem"/> properties to default
		/// values as defined in the <see cref="Def"/> class.
		/// </summary>
		protected void Init()
		{
			text = "Text";
			alignV = Def.Text.AlignV;
			alignH = Def.Text.AlignH;
			x = 0;
			y = 0;
			coordinateFrame = Def.Text.CoordFrame;

			this.fontSpec = new FontSpec(
				Def.Text.FontFamily, Def.Text.FontSize,
				Def.Text.FontColor, Def.Text.FontBold,
				Def.Text.FontItalic, Def.Text.FontUnderline );
		}

		/// <summary>
		/// Constructor that sets all <see cref="TextItem"/> properties to default
		/// values as defined in the <see cref="Def"/> class.
		/// </summary>
		/// <param name="text">The text to be displayed.</param>
		/// <param name="x">The x position of the text.  The units
		/// of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the text.  The units
		/// of this position are specified by the
		/// <see cref="CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		public TextItem( string text, float x, float y )
		{
			Init();
			this.text = text;
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The TextItem object from which to copy</param>
		public TextItem( TextItem rhs )
		{
			text = rhs.Text;
			alignV = rhs.AlignV;
			alignH = rhs.AlignH;
			x = rhs.X;
			y = rhs.Y;
			coordinateFrame = rhs.CoordinateFrame;
			fontSpec = new FontSpec( rhs.FontSpec );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the TextItem</returns>
		public object Clone()
		{ 
			return new TextItem( this ); 
		}

		/// <summary>
		/// The <see cref="TextItem"/> to be displayed.  This text can be multi-line by
		/// including newline ('\n') characters between the lines.
		/// </summary>
		public string Text
		{
			get { return text; }
			set { text = value; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="FontSpec"/> class used to render
		/// this <see cref="TextItem"/>
		/// </summary>
		/// <seealso cref="Def.Text.FontColor"/>
		/// <seealso cref="Def.Text.FontBold"/>
		/// <seealso cref="Def.Text.FontItalic"/>
		/// <seealso cref="Def.Text.FontUnderline"/>
		/// <seealso cref="Def.Text.FontFamily"/>
		/// <seealso cref="Def.Text.FontSize"/>
		public FontSpec FontSpec
		{
			get { return fontSpec; }
		}
		/// <summary>
		/// A horizontal alignment parameter for this <see cref="TextItem"/> specified
		/// using the <see cref="FontAlignH"/> enum type
		/// </summary>
		/// <seealso cref="Def.Text.AlignH"/>
		public FontAlignH AlignH
		{
			get { return alignH; }
			set { alignH = value; }
		}
		/// <summary>
		/// A vertical alignment parameter for this <see cref="TextItem"/> specified
		/// using the <see cref="FontAlignV"/> enum type
		/// </summary>
		/// <seealso cref="Def.Text.AlignV"/>
		public FontAlignV AlignV
		{
			get { return alignV; }
			set { alignV = value; }
		}
		/// <summary>
		/// The x position of the <see cref="TextItem"/>.  The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The text will be aligned to this position based on the
		/// <see cref="AlignH"/> property.
		/// </summary>
		public float X
		{
			get { return x; }
			set { x = value; }
		}
		/// <summary>
		/// The x position of the <see cref="TextItem"/>.  The units of this position
		/// are specified by the <see cref="CoordinateFrame"/> property.
		/// The text will be aligned to this position based on the
		/// <see cref="AlignV"/> property.
		/// </summary>
		public float Y
		{
			get { return y; }
			set { y = value; }
		}
		/// <summary>
		/// The coordinate system to be used for defining the <see cref="TextItem"/> position
		/// </summary>
		/// <value> The coordinate system is defined with the <see cref="CoordType"/>
		/// enum</value>
		/// <seealso cref="Def.Text.CoordFrame"/>
		public CoordType CoordinateFrame
		{
			get { return coordinateFrame; }
			set { coordinateFrame = value; }
		}
		
		/// <summary>
		/// Render this <see cref="TextItem"/> object to the specified <see cref="Graphics"/> device
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="TextList"/> collection object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, double scaleFactor )
		{
			// transform the x,y location from the user-defined
			// coordinate frame to the screen pixel location
			PointF pix = pane.GeneralTransform( new PointF(this.x, this.y),
						this.coordinateFrame );
			
			// Draw the text on the screen, including any frame and background
			// fill elements
			if ( pix.X > -10000 && pix.X < 100000 && pix.Y > -100000 && pix.Y < 100000 )
				this.FontSpec.Draw( g, this.text, pix.X, pix.Y,
								this.alignH, this.alignV, scaleFactor );
		}
	}
}
