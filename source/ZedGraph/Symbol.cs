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
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// This class handles the drawing of the curve <see cref="Symbol"/> objects.
	/// The symbols are the small shapes that appear over each defined point
	/// along the curve.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.19 $ $Date: 2005-05-20 16:32:28 $ </version>
	[Serializable]
	public class Symbol : ICloneable, ISerializable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the size of this
        /// <see cref="Symbol"/> in points (1/72 inch).  Use the public
        /// property <see cref="Size"/> to access this value.
		/// </summary>
		private float		size;
		/// <summary>
		/// Private field that stores the <see cref="SymbolType"/> for this
		/// <see cref="Symbol"/>.  Use the public
		/// property <see cref="Type"/> to access this value.
		/// </summary>
		private SymbolType	type;
		/// <summary>
		/// Private field that stores the visibility of this
		/// <see cref="Symbol"/>.  Use the public
		/// property <see cref="IsVisible"/> to access this value.  If this value is
		/// false, the symbols will not be shown (but the <see cref="Line"/> may
		/// still be shown).
		/// </summary>
		private bool		isVisible;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Symbol"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill		fill;
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Border"/> data for this
		/// <see cref="Symbol"/>.  Use the public property <see cref="Border"/> to
		/// access this value.
		/// </summary>
		private Border		border;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="ZedGraph.Symbol"/> class.
		/// </summary>
		public struct Default
		{
			// Default Symbol properties
			/// <summary>
			/// The default size for curve symbols (<see cref="Symbol.Size"/> property),
			/// in units of points.
			/// </summary>
			public static float Size = 7;
			/// <summary>
			/// The default pen width to be used for drawing curve symbols
			/// (<see cref="ZedGraph.Border.PenWidth"/> property).  Units are points.
			/// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default color for filling in this <see cref="Symbol"/>
			/// (<see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FillColor = Color.Red;
			/// <summary>
			/// The default custom brush for filling in this <see cref="Symbol"/>
			/// (<see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FillBrush = null;
			/// <summary>
			/// The default fill mode for the curve (<see cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FillType = FillType.None;
			/// <summary>
			/// The default symbol type for curves (<see cref="Symbol.Type"/> property).
			/// This is defined as a <see cref="ZedGraph.SymbolType"/> enumeration.
			/// </summary>
			public static SymbolType Type = SymbolType.Square;
			/// <summary>
			/// The default display mode for symbols (<see cref="Symbol.IsVisible"/> property).
			/// true to display symbols, false to hide them.
			/// </summary>
			public static bool IsVisible = true;
			/// <summary>
			/// The default for drawing frames around symbols (<see cref="ZedGraph.Border.IsVisible"/> property).
			/// true to display symbol frames, false to hide them.
			/// </summary>
			public static bool IsBorderVisible = true;
			/// <summary>
			/// The default color for drawing symbols (<see cref="ZedGraph.Border.Color"/> property).
			/// </summary>
			public static Color BorderColor = Color.Red;
		}
	#endregion

	#region Properties
		/// <summary>
		/// Gets or sets the size of the <see cref="Symbol"/>
		/// </summary>
        /// <value>Size in points (1/72 inch)</value>
        /// <seealso cref="Default.Size"/>
		public float Size
		{
			get { return size; }
			set { size = value; }
		}
		/// <summary>
		/// Gets or sets the type (shape) of the <see cref="Symbol"/>
		/// </summary>
		/// <value>A <see cref="SymbolType"/> enum value indicating the shape</value>
		/// <seealso cref="Default.Type"/>
		public SymbolType Type
		{
			get { return type; }
			set { type = value; }
		}
		/// <summary>
		/// Gets or sets a property that shows or hides the <see cref="Symbol"/>.
		/// </summary>
		/// <value>true to show the symbol, false to hide it</value>
		/// <seealso cref="Default.IsVisible"/>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="Symbol"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> data for this
		/// <see cref="Symbol"/>, which controls the border outline of the symbol.
		/// </summary>
		public Border Border
		{
			get { return border; }
			set { border = value; }
		}

		#endregion
	
	#region Constructors
		/// <summary>
		/// Default constructor that sets all <see cref="Symbol"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		public Symbol() : this( SymbolType.Default, Color.Empty )
		{
		}

		/// <summary>
		/// Default constructor that sets the <see cref="SymbolType"/> and
		/// <see cref="Color"/> as specified, and the remaining
		/// <see cref="Symbol"/> properties to default
		/// values as defined in the <see cref="Default"/> class.
		/// </summary>
		/// <param name="type">A <see cref="SymbolType"/> enum value
		/// indicating the shape of the symbol</param>
		/// <param name="color">A <see cref="Color"/> value indicating
		/// the color of the symbol
		/// </param>
		public Symbol( SymbolType type, Color color )
		{
			this.size = Default.Size;
			this.type = type;
			this.isVisible = Default.IsVisible;
			this.border = new Border( Default.IsBorderVisible, color, Default.PenWidth );
			this.fill = new Fill( color, Default.FillBrush, Default.FillType );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Symbol object from which to copy</param>
		public Symbol( Symbol rhs )
		{
			size = rhs.Size;
			type = rhs.Type;
			isVisible = rhs.IsVisible;
			fill = (Fill) rhs.Fill.Clone();
			border = (Border) rhs.Border.Clone();
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Symbol</returns>
		public object Clone()
		{ 
			return new Symbol( this ); 
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
		protected Symbol( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			size = info.GetSingle( "size" );
			type = (SymbolType) info.GetValue( "type", typeof(SymbolType) );
			isVisible = info.GetBoolean( "isVisible" );
			fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			border = (Border) info.GetValue( "border", typeof(Border) );
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
			info.AddValue( "size", size );
			info.AddValue( "type", type );
			info.AddValue( "isVisible", isVisible );
			info.AddValue( "fill", fill );
			info.AddValue( "border", border );
		}
	#endregion

	#region Rendering Methods

		/// <summary>
		/// Draw the <see cref="Symbol"/> to the specified <see cref="Graphics"/> device
		/// at the specified location.  This routine draws a single symbol.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="x">The x position of the center of the symbol in
		/// pixel units</param>
		/// <param name="y">The y position of the center of the symbol in
		/// pixel units</param>
		/// <param name="path">A <see cref="GraphicsPath"/> previously constructed by
		/// <see cref="MakePath"/> for this symbol</param>
		/// <param name="pen">A <see cref="Pen"/> class representing the standard pen for this symbol</param>
		/// <param name="brush">A <see cref="Brush"/> class representing a default solid brush for this symbol
		/// If this symbol uses a <see cref="LinearGradientBrush"/>, it will be created on the fly for
		/// each point, since it has to be scaled to the individual point coordinates.</param>
		public void DrawSymbol( Graphics g, float x, float y, GraphicsPath path,
							Pen pen, Brush brush )
		{
			// Only draw if the symbol is visible
			if (	this.isVisible &&
					this.Type != SymbolType.None &&
					x < 100000 && x > -100000 &&
					y < 100000 && y > -100000 )
			{
				Matrix saveMatrix = g.Transform;
				g.TranslateTransform( x, y );
			
				// Fill or draw the symbol as required
				if ( this.fill.IsVisible)
					g.FillPath( brush, path );
					//FillPoint( g, x, y, scaleFactor, pen, brush );
				
				if ( this.border.IsVisible )
					g.DrawPath( pen, path );
					//DrawPoint( g, x, y, scaleFactor, pen );
				
				g.Transform = saveMatrix;
			}
		}

		/// <summary>
		/// Draw the <see cref="Symbol"/> to the specified <see cref="Graphics"/> device
		/// at the specified location.  This routine draws a single symbol.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="x">The x position of the center of the symbol in
        /// pixel units</param>
		/// <param name="y">The y position of the center of the symbol in
		/// pixel units</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="dataValue">The data value to be used for a value-based
		/// color gradient.  This is only applicable for <see cref="FillType.GradientByX"/>,
		/// <see cref="FillType.GradientByY"/> or <see cref="FillType.GradientByZ"/>.</param>
		public void DrawSymbol( Graphics g, GraphPane pane, float x, float y,
							float scaleFactor, PointPair dataValue )
		{
			// Only draw if the symbol is visible
			if (	this.isVisible &&
					this.Type != SymbolType.None &&
					x < 100000 && x > -100000 &&
					y < 100000 && y > -100000 )
			{
				//SolidBrush	brush = new SolidBrush( this.fill.Color );
                Pen pen = border.MakePen( pane.IsPenWidthScaled, scaleFactor );
				GraphicsPath path = this.MakePath( g, scaleFactor );
				Brush brush = this.Fill.MakeBrush( path.GetBounds(), dataValue );

				DrawSymbol( g, x, y, path, pen, brush );
			}
		}

		/// <summary>
		/// Create a <see cref="GraphicsPath"/> struct for the current symbol based on the
		/// specified scaleFactor and assuming the symbol will be centered at position 0,0.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.</param>
		/// <returns>Returns the <see cref="GraphicsPath"/> for the current symbol</returns>
		public GraphicsPath MakePath( Graphics g, float scaleFactor )
		{
			float	scaledSize = (float) ( this.size * scaleFactor );
			float	hsize = scaledSize / 2,
					hsize1 = hsize + 1;
			
			GraphicsPath path = new GraphicsPath();
			
			switch( this.type == SymbolType.Default ? Default.Type : this.type )
			{
			case SymbolType.Square:
				path.AddLine( -hsize, -hsize, hsize, -hsize );
				path.AddLine( hsize, -hsize, hsize, hsize );
				path.AddLine( hsize, hsize, -hsize, hsize );
				path.AddLine( -hsize, hsize, -hsize, -hsize );
				break;
			case SymbolType.Diamond:
				path.AddLine( 0, -hsize, hsize, 0 );
				path.AddLine( hsize, 0, 0, hsize );
				path.AddLine( 0, hsize, -hsize, 0 );
				path.AddLine( -hsize, 0, 0, -hsize );
				break;
			case SymbolType.Triangle:
				path.AddLine( 0, -hsize, hsize, hsize );
				path.AddLine( hsize, hsize, -hsize, hsize );
				path.AddLine( -hsize, hsize, 0, -hsize );
				break;
			case SymbolType.Circle:
				path.AddEllipse( -hsize, -hsize, scaledSize, scaledSize );
				break;
			case SymbolType.XCross:
				path.AddLine( -hsize, -hsize, hsize1, hsize1 );
				path.AddLine( hsize, -hsize, -hsize1, hsize1 );
				break;
			case SymbolType.Plus:
				path.AddLine( 0, -hsize, 0, hsize1 );
				path.AddLine( -hsize, 0, hsize1, 0 );
				break;
			case SymbolType.Star:
				path.AddLine( 0, -hsize, 0, hsize1 );
				path.AddLine( -hsize, 0, hsize1, 0 );
				path.AddLine( -hsize, -hsize, hsize1, hsize1 );
				path.AddLine( hsize, -hsize, -hsize1, hsize1 );
				break;
			case SymbolType.TriangleDown:
				path.AddLine( 0, hsize, hsize, -hsize );
				path.AddLine( hsize, -hsize, -hsize, -hsize );
				path.AddLine( -hsize, -hsize, 0, hsize );
				break;
			case SymbolType.HDash:
				path.AddLine( -hsize, 0, hsize1, 0 );
				break;
			case SymbolType.VDash:
				path.AddLine( 0, -hsize, 0, hsize1 );
				break;
			}
			
			return path;
		}

		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device as a symbol at each defined point.  The routine
		/// only draws the symbols; the lines are draw by the
		/// <see cref="Line.DrawCurve"/> method.  This method
		/// is normally only called by the Draw method of the
		/// <see cref="CurveItem"/> object
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="curve">A <see cref="LineItem"/> representing this
		/// curve.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, LineItem curve, float scaleFactor )
		{
			float	tmpX, tmpY;
			double	curX, curY, lowVal;
			PointPairList points = curve.Points;
		
			if ( points != null && ( this.border.IsVisible || this.fill.IsVisible ) )
			{
				// For the sake of speed, go ahead and create a solid brush and a pen
				// If it's a gradient fill, it will be created on the fly for each symbol
				//SolidBrush	brush = new SolidBrush( this.fill.Color );
                Pen pen = this.border.MakePen( pane.IsPenWidthScaled, scaleFactor );
                //Pen pen = new Pen( this.border.Color, pane.ScaledPenWidth(border.PenWidth * scaleFactor) );
				
				GraphicsPath path = MakePath( g, scaleFactor );
				RectangleF rect = path.GetBounds();
				Brush brush = this.Fill.MakeBrush( rect );
				ValueHandler valueHandler = new ValueHandler( pane, false );
				Axis yAxis = curve.IsY2Axis ? (Axis) pane.Y2Axis : (Axis) pane.YAxis;

				// Loop over each defined point							
				for ( int i=0; i<points.Count; i++ )
				{
					// Get the user scale values for the current point
					// use the valueHandler only for stacked types
					if ( pane.LineType == LineType.Stack )
					{
						valueHandler.GetValues( curve, i, out curX, out lowVal, out curY );
					}
					// otherwise, just access the values directly.  Avoiding the valueHandler for
					// non-stacked types is an optimization to minimize overhead in case there are
					// a large number of points.
					else
					{
						curX = points[i].X;
						curY = points[i].Y;
					}
				
					// Any value set to double max is invalid and should be skipped
					// This is used for calculated values that are out of range, divide
					//   by zero, etc.
					// Also, any value <= zero on a log scale is invalid
				
					if (	curX != PointPair.Missing &&
							curY != PointPair.Missing &&
							!System.Double.IsNaN( curX ) &&
							!System.Double.IsNaN( curY ) &&
							!System.Double.IsInfinity( curX ) &&
							!System.Double.IsInfinity( curY ) &&
							( curX > 0 || !pane.XAxis.IsLog ) &&
							( !yAxis.IsLog || curY > 0.0 ) )
					{
						// Transform the user scale values to pixel locations
						tmpX = pane.XAxis.Transform( curve.IsOverrideOrdinal, i, curX );
						tmpY = yAxis.Transform( curve.IsOverrideOrdinal, i, curY );

						// If the fill type for this symbol is a Gradient by value type,
						// the make a brush corresponding to the appropriate current value
						if ( this.fill.IsGradientValueType )
							brush = fill.MakeBrush( rect, points[i] );
						// Otherwise, the brush is already defined
						// Draw the symbol at the specified pixel location
						this.DrawSymbol( g, tmpX, tmpY, path, pen, brush );		
					}
				}
			}
		}
		#endregion
	
	}
}

