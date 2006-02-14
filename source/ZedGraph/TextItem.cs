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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// A class that represents a text object on the graph.  A list of
	/// <see cref="GraphItem"/> objects is maintained by the
	/// <see cref="GraphItemList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.20 $ $Date: 2006-02-14 06:14:22 $ </version>
	[Serializable]
	public class TextItem : GraphItem, ICloneable, ISerializable
	{
	#region Fields
		/// <summary> Private field to store the actual text string for this
		/// <see cref="TextItem"/>.  Use the public property <see cref="TextItem.Text"/>
		/// to access this value.
		/// </summary>
		private string text;
		/// <summary>
		/// Private field to store the <see cref="FontSpec"/> class used to render
		/// this <see cref="TextItem"/>.  Use the public property <see cref="FontSpec"/>
		/// to access this class.
		/// </summary>
		private FontSpec	fontSpec;

		/*
		/// <summary>
		/// Private field to indicate whether this <see cref="TextItem"/> is to be
		/// wrapped when rendered.  Wrapping is to be done within <see cref="TextItem.wrappedRect"/>.
		/// Use the public property <see cref="TextItem.IsWrapped"/>
		/// to access this value.
		/// </summary>
		private bool isWrapped;
		*/

		/// <summary>
		/// Private field holding the SizeF into which this <see cref="TextItem"/>
		/// should be rendered. Use the public property <see cref="TextItem.LayoutArea"/>
		/// to access this value.
		/// </summary>
		private SizeF layoutArea;


		#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="TextItem"/> class.
		/// </summary>
		new public struct Default
		{
			/*
			/// <summary>
			/// The default wrapped flag for rendering this <see cref="TextItem,Text"/>. 
			/// </summary>
			public static bool IsWrapped = false ;
			/// <summary>
			/// The default RectangleF for rendering this <see cref="TextItem.Text"/> 
			/// </summary>
			public static SizeF WrappedSize = new SizeF( 0,0 );
			*/

			/// <summary>
			/// The default font family for the <see cref="TextItem"/> text
			/// (<see cref="ZedGraph.FontSpec.Family"/> property).
			/// </summary>
			public static string FontFamily = "Arial";
			/// <summary>
			/// The default font size for the <see cref="TextItem"/> text
			/// (<see cref="ZedGraph.FontSpec.Size"/> property).  Units are
			/// in points (1/72 inch).
			/// </summary>
			public static float FontSize = 12.0F;
			/// <summary>
			/// The default font color for the <see cref="TextItem"/> text
			/// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
			/// </summary>
			public static Color FontColor = Color.Black;
			/// <summary>
			/// The default font bold mode for the <see cref="TextItem"/> text
			/// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
			/// for a bold typeface, false otherwise.
			/// </summary>
			public static bool FontBold = false;
			/// <summary>
			/// The default font underline mode for the <see cref="TextItem"/> text
			/// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
			/// for an underlined typeface, false otherwise.
			/// </summary>
			public static bool FontUnderline = false;
			/// <summary>
			/// The default font italic mode for the <see cref="TextItem"/> text
			/// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
			/// for an italic typeface, false otherwise.
			/// </summary>
			public static bool FontItalic = false;
		}
	#endregion

	#region Properties
		
		/*
		/// <summary>
		/// 
		/// </summary>
		internal bool IsWrapped
		{
			get { return (this.isWrapped); }
			set { this.isWrapped = value; } 
		}
		*/

		/// <summary>
		/// 
		/// </summary>
		public SizeF LayoutArea
		{
			get { return this.layoutArea; }
			set { this.layoutArea = value; } 
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
	#endregion
	
	#region Constructors
		/// <summary>
		/// Constructor that sets all <see cref="TextItem"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="text">The text to be displayed.</param>
		/// <param name="x">The x position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		public TextItem( string text, float x, float y ) :
			base( x, y )
		{
			Init( text );
		}

		private void Init( string text )
		{
			if ( text != null )
				this.text = text;
			else
				text = "Text";
			
			this.fontSpec = new FontSpec(
				Default.FontFamily, Default.FontSize,
				Default.FontColor, Default.FontBold,
				Default.FontItalic, Default.FontUnderline );
			
			//this.isWrapped = Default.IsWrapped ;
			this.layoutArea = new SizeF( 0, 0 );
		}
		
		/// <summary>
		/// Constructor that sets all <see cref="TextItem"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="text">The text to be displayed.</param>
		/// <param name="x">The x position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		/// <param name="coordType">The <see cref="CoordType"/> enum value that
		/// indicates what type of coordinate system the x and y parameters are
		/// referenced to.</param>
		public TextItem( string text, float x, float y, CoordType coordType ) :
			base( x, y, coordType )
		{
			Init( text );
		}

		/// <summary>
		/// Constructor that sets all <see cref="TextItem"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="text">The text to be displayed.</param>
		/// <param name="x">The x position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the <see cref="AlignH"/>
		/// property.</param>
		/// <param name="y">The y position of the text.  The units
		/// of this position are specified by the
		/// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
		/// aligned to this position based on the
		/// <see cref="AlignV"/> property.</param>
		/// <param name="coordType">The <see cref="CoordType"/> enum value that
		/// indicates what type of coordinate system the x and y parameters are
		/// referenced to.</param>
		/// <param name="alignH">The <see cref="ZedGraph.AlignH"/> enum that specifies
		/// the horizontal alignment of the object with respect to the (x,y) location</param>
		/// <param name="alignV">The <see cref="ZedGraph.AlignV"/> enum that specifies
		/// the vertical alignment of the object with respect to the (x,y) location</param>
		public TextItem( string text, float x, float y, CoordType coordType, AlignH alignH, AlignV alignV ) :
			base( x, y, coordType, alignH, alignV )
		{
			Init( text );
		}

		/// <summary>
		/// Parameterless constructor that initializes a new <see cref="TextItem"/>.
		/// </summary>
		public TextItem() : base( 0, 0 )
		{
			Init( "" );
		}
		
		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="TextItem"/> object from which to copy</param>
		public TextItem( TextItem rhs ) : base( rhs )
		{
			text = rhs.Text;
			fontSpec = new FontSpec( rhs.FontSpec );
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typesafe, deep-copy clone method.
		/// </summary>
		/// <returns>A new, independent copy of this class</returns>
		public TextItem Clone()
		{
			return new TextItem( this );
		}
	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema2 = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected TextItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			text = info.GetString( "text" );
			fontSpec = (FontSpec) info.GetValue( "fontSpec", typeof(FontSpec) );
			//isWrapped = info.GetBoolean ("isWrapped") ;
			layoutArea = (SizeF) info.GetValue( "layoutArea", typeof(SizeF) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );
			info.AddValue( "schema2", schema2 );
			info.AddValue( "text", text );
			info.AddValue( "fontSpec", fontSpec );
			//info.AddValue( "isWrapped", isWrapped );
			info.AddValue( "layoutArea", layoutArea );
		}
	#endregion

	#region Rendering Methods
		/// <summary>
		/// Render this <see cref="TextItem"/> object to the specified <see cref="Graphics"/> device
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphItemList"/> collection object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, PaneBase pane, float scaleFactor )
		{
			// transform the x,y location from the user-defined
			// coordinate frame to the screen pixel location
			PointF pix = this.location.Transform( pane );
			
			// Draw the text on the screen, including any frame and background
			// fill elements
			if ( pix.X > -100000 && pix.X < 100000 && pix.Y > -100000 && pix.Y < 100000 )
			{
				//if ( this.layoutSize.IsEmpty )
				//	this.FontSpec.Draw( g, pane.IsPenWidthScaled, this.text, pix.X, pix.Y,
				//		this.location.AlignH, this.location.AlignV, scaleFactor );
				//else
					this.FontSpec.Draw( g, pane.IsPenWidthScaled, this.text, pix.X, pix.Y,
						this.location.AlignH, this.location.AlignV, scaleFactor, this.layoutArea );

			}
		}
		
		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="TextItem"/>.  This method takes into account rotation and alignment
		/// parameters of the text, as specified in the <see cref="FontSpec"/>.
		/// </summary>
		/// <param name="pt">The screen point, in pixels</param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>true if the point lies in the bounding box, false otherwise</returns>
		override public bool PointInBox( PointF pt, PaneBase pane, Graphics g, float scaleFactor )
		{
			// transform the x,y location from the user-defined
			// coordinate frame to the screen pixel location
			PointF pix = this.location.Transform( pane );
			
			return this.fontSpec.PointInBox( pt, g, this.text, pix.X, pix.Y,
								this.location.AlignH, this.location.AlignV, scaleFactor, this.LayoutArea );
		}
		
	#endregion
	
	}
}
