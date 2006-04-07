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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// Encapsulates an "High-Low" Bar curve type that displays a bar in which both
	/// the bottom and the top of the bar are set by data valuesfrom the
	/// <see cref="PointPair"/> struct.
	/// </summary>
	/// <remarks>The <see cref="HiLowBarItem"/> type is intended for displaying
	/// bars that cover a band of data, such as a confidence interval, "waterfall"
	/// chart, etc.  The width of the bar can be set in two ways.  First,
	/// <see cref="HiLowBar.Size"/> can be used to set a width in points (1/72nd inch),
	/// that is scaled using the regular scalefactor method (see
	/// <see cref="PaneBase.CalcScaleFactor"/>).  In this manner, the bar widths
	/// are set similar to symbol sizes.  The other method is to set
	/// <see cref="HiLowBar.IsMaximumWidth"/> to true, which will cause the bars
	/// to be scaled just like a <see cref="BarItem"/> in which only one
	/// bar series is present.  That is, the bars width will be the width of
	/// a cluster less the clustergap (see <see cref="BarSettings.GetClusterWidth"/>
	/// and <see cref="BarSettings.MinClusterGap"/>). The position of each bar is set
	/// according to the <see cref="PointPair"/> values.  The independent axis
	/// is assigned with <see cref="BarSettings.Base"/>, and is a
	/// <see cref="BarBase"/> enum type.  If <see cref="BarSettings.Base"/>
	/// is set to <see cref="ZedGraph.BarBase.Y"/> or <see cref="ZedGraph.BarBase.Y2"/>, then
	/// the bars will actually be horizontal, since the X axis becomes the
	/// value axis and the Y or Y2 axis becomes the independent axis.</remarks>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.12.2.4 $ $Date: 2006-04-07 06:14:02 $ </version>
	[Serializable]
	public class HiLowBarItem : CurveItem, ICloneable, ISerializable
	{

	#region Fields
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.HiLowBar"/>
		/// class defined for this <see cref="HiLowBarItem"/>.  Use the public
		/// property <see cref="Bar"/> to access this value.
		/// </summary>
		private HiLowBar _bar;
	#endregion

	#region Constructors
		/// <summary>
		/// Create a new <see cref="HiLowBarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		/// <param name="baseVal">An array of double precision values that define the
		/// base value (the bottom) of the bars for this curve.
		/// </param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="ZedGraph.Bar.Fill"/> and <see cref="ZedGraph.Bar.Border"/> properties.
		/// </param>
		public HiLowBarItem( string label, double[] x, double[] y, double[] baseVal, Color color ) :
			this( label, new PointPairList( x, y, baseVal ), color )
		{
		}
		
		/// <summary>
		/// Create a new <see cref="HiLowBarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="points">A <see cref="IPointList"/> of double precision value trio's that define
		/// the X, Y, and lower dependent values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="ZedGraph.Bar.Fill"/> and <see cref="ZedGraph.Bar.Border"/> properties.
		/// </param>
		public HiLowBarItem( string label, IPointList points, Color color )
			: base( label, points )
		{
			_bar = new HiLowBar( color );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="HiLowBarItem"/> object from which to copy</param>
		public HiLowBarItem( HiLowBarItem rhs ) : base( rhs )
		{
			this._bar = rhs._bar.Clone(); // new HiLowBar( rhs.Bar );
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
		public HiLowBarItem Clone()
		{
			return new HiLowBarItem( this );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema2 = 10;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected HiLowBarItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			_bar = (HiLowBar) info.GetValue( "bar", typeof(HiLowBar) );
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
			info.AddValue( "bar", _bar );

		}
	#endregion

	#region Properties
		/// <summary>
		/// Gets a reference to the <see cref="HiLowBar"/> class defined
		/// for this <see cref="HiLowBarItem"/>.
		/// </summary>
		public HiLowBar Bar
		{
			get { return _bar; }
		}

		/// <summary>
		/// Gets a flag indicating if the Z data range should be included in the axis scaling calculations.
		/// </summary>
		/// <param name="pane">The parent <see cref="GraphPane" /> of this <see cref="CurveItem" />.
		/// </param>
		/// <value>true if the Z data are included, false otherwise</value>
		override internal bool IsZIncluded( GraphPane pane )
		{
			return true;
		}

		/// <summary>
		/// Gets a flag indicating if the X axis is the independent axis for this <see cref="CurveItem" />
		/// </summary>
		/// <param name="pane">The parent <see cref="GraphPane" /> of this <see cref="CurveItem" />.
		/// </param>
		/// <value>true if the X axis is independent, false otherwise</value>
		override internal bool IsXIndependent( GraphPane pane )
		{
			return pane._barSettings.Base == BarBase.X;
		}
			
	#endregion

	#region Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="HiLowBarItem"/> to the specified
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
		/// <param name="pos">The ordinal position of the current <see cref="ErrorBarItem"/>
		/// curve.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, GraphPane pane, int pos, float scaleFactor  )
		{
			if ( this._isVisible )
				_bar.DrawBars( g, pane, this, BaseAxis( pane ), ValueAxis( pane ),
								GetBarWidth( pane ), pos, scaleFactor );
		}		

		/// <summary>
		/// Draw a legend key entry for this <see cref="HiLowBarItem"/> at the specified location
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
		override public void DrawLegendKey( Graphics g, GraphPane pane, RectangleF rect,
									float scaleFactor )
		{
			this._bar.Draw( g, pane, rect, scaleFactor, true, null );
		}

	#endregion

	}
}
