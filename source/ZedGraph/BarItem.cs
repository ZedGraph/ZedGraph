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
	/// Summary description for BarItem.
	/// </summary>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.9 $ $Date: 2005-01-08 08:28:07 $ </version>
	[Serializable]
	public class BarItem : CurveItem, ICloneable, ISerializable
	{
	#region Fields
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.Bar"/>
		/// class defined for this <see cref="BarItem"/>.  Use the public
		/// property <see cref="Bar"/> to access this value.
		/// </summary>
		protected Bar			bar;
	#endregion
	
	#region Properties
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Bar"/> class defined
		/// for this <see cref="BarItem"/>.
		/// </summary>
		public Bar Bar
		{
			get { return bar; }
		}

	#endregion
	
	#region Constructors
		/// <summary>
		/// Create a new <see cref="BarItem"/>, specifying only the legend label for the bar.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		public BarItem( string label ) : base( label )
		{
			this.bar = new Bar();
		}
		/// <summary>
		/// Create a new <see cref="BarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="ZedGraph.Bar.Fill"/> and <see cref="ZedGraph.Bar.Border"/> properties.
		/// </param>
		public BarItem( string label, double[] x, double[] y, Color color )
			: this( label, new PointPairList( x, y ), color )
		{
		}

		/// <summary>
		/// Create a new <see cref="BarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="ZedGraph.Bar.Fill"/> and <see cref="ZedGraph.Bar.Border"/> properties.
		/// </param>
		public BarItem( string label, PointPairList points, Color color )
			: base( label, points )
		{
			bar = new Bar( color );
		}
		
		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="BarItem"/> object from which to copy</param>
		public BarItem( BarItem rhs ) : base( rhs )
		{
			bar = new Bar( rhs.Bar );
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the BarItem</returns>
		override public object Clone()
		{ 
			return new BarItem( this ); 
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
		protected BarItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			bar = (Bar) info.GetValue( "bar", typeof(Bar) );
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
			info.AddValue( "bar", bar );
		}
	#endregion

	#region Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="BarItem"/> to the specified
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, GraphPane pane, int pos, double scaleFactor  )
		{
			// Pass the drawing onto the bar class
			if ( this.isVisible )
				bar.DrawBars( g, pane, this, BaseAxis(pane), ValueAxis(pane,isY2Axis),
								this.GetBarWidth( pane ), pos, scaleFactor );
		}
		
		/// <summary>
		/// Draw a legend key entry for this <see cref="BarItem"/> at the specified location
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void DrawLegendKey( Graphics g, GraphPane pane, RectangleF rect, double scaleFactor )
		{
			this.bar.Draw( g, pane, rect, scaleFactor, true );
		}

	#endregion
	}
}
