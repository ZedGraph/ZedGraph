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
	/// Encapsulates a curve type that is displayed as a line and/or a set of
	/// symbols at each point.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.8 $ $Date: 2005-02-11 05:20:43 $ </version>
	[Serializable]
	public class LineItem : CurveItem, ICloneable, ISerializable
	{
	#region Fields
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.Symbol"/>
		/// class defined for this <see cref="LineItem"/>.  Use the public
		/// property <see cref="Symbol"/> to access this value.
		/// </summary>
		private Symbol		symbol;
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.Line"/>
		/// class defined for this <see cref="LineItem"/>.  Use the public
		/// property <see cref="Line"/> to access this value.
		/// </summary>
		private Line		line;
	#endregion

	#region Properties
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Symbol"/> class defined
		/// for this <see cref="LineItem"/>.
		/// </summary>
		public Symbol Symbol
		{
			get { return symbol; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Line"/> class defined
		/// for this <see cref="LineItem"/>.
		/// </summary>
		public Line Line
		{
			get { return line; }
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// Create a new <see cref="LineItem"/>, specifying only the legend label for the bar.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		public LineItem( string label ) : base( label )
		{
			this.symbol = new Symbol();
			this.line = new Line();
		}
		
		/// <summary>
		/// Create a new <see cref="LineItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/> and <see cref="Symbol"/> properties.
		/// </param>
		/// <param name="symbolType">A <see cref="SymbolType"/> enum specifying the
		/// type of symbol to use for this <see cref="LineItem"/> </param>
		public LineItem( string label, double[] x, double[] y, Color color, SymbolType symbolType )
			: this( label, new PointPairList( x, y ), color, symbolType )
		{
		}

		/// <summary>
		/// Create a new <see cref="LineItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/> and <see cref="Symbol"/> properties.
		/// </param>
		/// <param name="symbolType">A <see cref="SymbolType"/> enum specifying the
		/// type of symbol to use for this <see cref="LineItem"/> </param>
		public LineItem( string label, PointPairList points, Color color, SymbolType symbolType )
			: base( label, points )
		{
			line = new Line( color );
			this.symbol = new Symbol( symbolType, color );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="LineItem"/> object from which to copy</param>
		public LineItem( LineItem rhs ) : base( rhs )
		{
			symbol = new Symbol( rhs.Symbol );
			line = new Line( rhs.Line );
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the LineItem</returns>
		override public object Clone()
		{ 
			return new LineItem( this ); 
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
		protected LineItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			symbol = (Symbol) info.GetValue( "symbol", typeof(Symbol) );
			line = (Line) info.GetValue( "line", typeof(Line) );
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
			info.AddValue( "symbol", symbol );
			info.AddValue( "line", line );
		}
	#endregion

	#region Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="LineItem"/> to the specified
		/// <see cref="Graphics"/> device.  This method is normally only
		/// called by the Draw method of the parent <see cref="ZedGraph.CurveList"/>
		/// collection object.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="pos">The ordinal position of the current <see cref="Bar"/>
		/// curve.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, GraphPane pane, int pos, float scaleFactor  )
		{
			if ( this.isVisible )
			{
				Line.Draw( g, pane, this, isY2Axis, scaleFactor );
				
				Symbol.Draw( g, pane, this, isY2Axis, scaleFactor );
			}
		}		

		/// <summary>
		/// Draw a legend key entry for this <see cref="LineItem"/> at the specified location
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="rect">The <see cref="RectangleF"/> struct that specifies the
        /// location for the legend key</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void DrawLegendKey( Graphics g, GraphPane pane, RectangleF rect, float scaleFactor )
		{
			// Draw a sample curve to the left of the label text
			float yMid = rect.Top + rect.Height / 2.0F;
            this.Line.DrawSegment( g, pane, rect.Left, yMid, rect.Right, yMid, scaleFactor );

            // Draw a sample symbol to the left of the label text				
			this.Symbol.DrawSymbol( g, pane, rect.Left + rect.Width / 2.0F, yMid, scaleFactor );
		}
	
		/// <summary>
		/// Loads some pseudo unique colors/symbols into this LineItem.  This
		/// is mainly useful for differentiating a set of new LineItems without
		/// having to pick your own colors/symbols.
		/// <seealso cref="CurveItem.MakeUnique( ColorSymbolRotator )"/>
		/// </summary>
		/// <param name="rotator">
		/// The <see cref="ColorSymbolRotator"/> that is used to pick the color
		///  and symbol for this method call.
		/// </param>
		override public void MakeUnique( ColorSymbolRotator rotator )
		{
			this.Color			= rotator.NextColor;
			this.Symbol.Type	= rotator.NextSymbol;
		}
	#endregion
	}
}
