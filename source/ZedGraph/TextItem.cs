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
using System.Drawing;
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A class that represents a text object on the graph.  A list of
	/// <see cref="TextItem"/> objects is maintained by the
	/// <see cref="TextList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.1 $ $Date: 2004-08-23 20:27:45 $ </version>
	public class TextItem : ICloneable
	{
	#region Fields
		/// <summary> Private field to store the actual text string for this
		/// <see cref="TextItem"/>.  Use the public property <see cref="TextItem.Text"/>
		/// to access this value.
		/// </summary>
		private string		text;
		/// <summary> Private field to store the vertical Font alignment property for
		/// this <see cref="TextItem"/>.  Use the public property <see cref="TextItem.AlignV"/>
		/// to access this value.  The value of this field is a <see cref="FontAlignV"/> enum.
		/// </summary>
		private FontAlignV	alignV;
		/// <summary> Private field to store the horizontal Font alignment property for
		/// this <see cref="TextItem"/>.  Use the public property <see cref="TextItem.AlignH"/>
		/// to access this value.  The value of this field is a <see cref="FontAlignH"/> enum.
		/// </summary>
		private FontAlignH	alignH;

		/// <summary> Private fields to store the X and Y coordinate positions for
		/// this <see cref="TextItem"/>.  Use the public properties <see cref="X"/> and
		/// <see cref="Y"/> to access these values.  The coordinate type stored here is
		/// dependent upon the setting of <see cref="coordinateFrame"/>.
		/// </summary>
		private float		x,
							y;
		/// <summary>
		/// Private field to store the coordinate system to be used for defining the
		/// <see cref="TextItem"/> position.  Use the public property
		/// <see cref="CoordinateFrame"/> to access this value. The coordinate system
		/// is defined with the <see cref="CoordType"/> enum
		/// </summary>
		/// <seealso cref="Def.Text.CoordFrame"/>
		private CoordType	coordinateFrame;
		/// <summary>
		/// Private field to store the <see cref="FontSpec"/> class used to render
		/// this <see cref="TextItem"/>.  Use the public property <see cref="FontSpec"/>
		/// to access this class.
		/// </summary>
		private FontSpec	fontSpec;
	#endregion
	
	#region Properties
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
	#endregion
	
	#region Constructors
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
	#endregion
	
	#region Rendering Methods
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
	#endregion
	
	}
}
