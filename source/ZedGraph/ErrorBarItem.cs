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
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
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
using System.Text;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// Encapsulates an "Error Bar" curve type that displays an I-beam of data.
	/// </summary>
	public class ErrorBarItem : CurveItem, ICloneable
	{
	#region Fields
		/// <summary>
		/// Private field that stores a reference to the <see cref="ZedGraph.ErrorBar"/>
		/// class defined for this <see cref="ErrorBarItem"/>.  Use the public
		/// property <see cref="Line"/> to access this value.
		/// </summary>
		private ErrorBar errorBar;

		/// <summary>
		/// Private field that determines which <see cref="Axis"/> is the independent axis
		/// for this <see cref="ErrorBarItem"/>.
		/// </summary>
		private BarBase	barBase;
	#endregion

	#region Properties
		/// <summary>
		/// Gets a reference to the <see cref="ErrorBar"/> class defined
		/// for this <see cref="ErrorBarItem"/>.
		/// </summary>
		public ErrorBar ErrorBar
		{
			get { return errorBar; }
		}

		/// <summary>
		/// Determines which <see cref="Axis"/> is the independent axis
		/// for this <see cref="ErrorBarItem"/>.
		/// </summary>
		public BarBase	BarBase
		{
			get { return barBase; }
			set { barBase = value; }
		}

		/// <summary>Returns a reference to the <see cref="Axis"/> object that is the "base"
		/// (independent axis) from which the <see cref="ErrorBar"/>'s are drawn.
		/// The base axis is the axis from which the bars grow with increasing value.  This
		/// property is determined by the value of <see cref="BarBase"/>.
		/// </summary>
		/// <remarks>For regular <see cref="BarItem"/>'s, the base axis is determined
		/// by <see cref="ZedGraph.BarBase"/> (a global value).  For <see cref="ErrorBarItem"/>'s, the
		/// base axis is determined by <see cref="ErrorBarItem.BarBase"/>, and can be
		/// different for each curve.</remarks>
		/// <seealso cref="BarBase"/>
		/// <seealso cref="ValueAxis"/>
		public Axis BaseAxis( GraphPane pane )
		{
			if ( barBase == BarBase.X )
				return pane.XAxis;
			else if ( barBase == BarBase.Y )
				return pane.YAxis;
			else
				return pane.Y2Axis;
		}
		/// <summary>Returns a reference to the <see cref="Axis"/> object that is the "value"
		/// (dependent axis) for the <see cref="ErrorBar"/>'s.
		/// The value axis determines the height of the bars.  This
		/// property is determined by the value of <see cref="BarBase"/>.
		/// </summary>
		/// <seealso cref="BarBase"/>
		/// <seealso cref="BaseAxis"/>
		public Axis ValueAxis( GraphPane pane, bool isY2Axis )
		{
			if ( barBase == BarBase.X )
			{
				if ( isY2Axis )
					return pane.Y2Axis;
				else
					return pane.YAxis;
			}
			else
				return pane.XAxis;
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Create a new <see cref="ErrorBarItem"/>, specifying only the legend label.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		public ErrorBarItem( string label ) : base( label )
		{
			this.errorBar = new ErrorBar();
		}
		
		/// <summary>
		/// Create a new <see cref="ErrorBarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the X axis values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the Y axis values for this curve</param>
		/// <param name="lowValue">An array of double precision values that define
		/// the lower dependent values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/> properties.
		/// </param>
		public ErrorBarItem( string label, double[] x, double[] y, double[] lowValue,
							System.Drawing.Color color )
			: this( label, new PointPairList( x, y, lowValue ), color )
		{
		}

		/// <summary>
		/// Create a new <see cref="ErrorBarItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The label that will appear in the legend.</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision values that define
		/// the X, Y and lower dependent values for this curve</param>
		/// <param name="color">A <see cref="Color"/> value that will be applied to
		/// the <see cref="Line"/> properties.
		/// </param>
		public ErrorBarItem( string label, PointPairList points, Color color )
			: base( label, points )
		{
			errorBar = new ErrorBar( color );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ErrorBarItem"/> object from which to copy</param>
		public ErrorBarItem( ErrorBarItem rhs ) : base( rhs )
		{
			errorBar = new ErrorBar( rhs.ErrorBar );
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="ErrorBarItem"/></returns>
		override public object Clone()
		{ 
			return new ErrorBarItem( this ); 
		}
	#endregion

	#region Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="ErrorBarItem"/> to the specified
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
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, GraphPane pane, int pos, double scaleFactor  )
		{
			if ( this.isVisible )
			{
				ErrorBar.Draw( g, pane, points, this.BaseAxis( pane ),
								this.ValueAxis( pane,isY2Axis ), scaleFactor );
			}
		}		

		/// <summary>
		/// Draw a legend key entry for this <see cref="ErrorBarItem"/> at the specified location
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
		override public void DrawLegendKey( Graphics g, GraphPane pane, RectangleF rect,
									double scaleFactor )
		{
			float pixBase, pixValue, pixLowValue;

			if ( barBase == BarBase.X )
			{
				pixBase = rect.Left + rect.Width / 2.0F;
				pixValue = rect.Top;
				pixLowValue = rect.Bottom;
			}
			else
			{
				pixBase = rect.Top + rect.Height / 2.0F;
				pixValue = rect.Right;
				pixLowValue = rect.Left;
			}

			Pen pen = new Pen( errorBar.Color, errorBar.PenWidth );
			this.ErrorBar.Draw( g, barBase == BarBase.X, pixBase, pixValue,
								pixLowValue, scaleFactor, pen );
		}

		/// <summary>
		/// Go through the list of <see cref="PointPair"/> data values for this
		/// <see cref="ErrorBarItem"/> and determine the minimum and maximum values in the data.
		/// </summary>
		/// <param name="xMin">The minimum X value in the range of data</param>
		/// <param name="xMax">The maximum X value in the range of data</param>
		/// <param name="yMin">The minimum Y value in the range of data</param>
		/// <param name="yMax">The maximum Y value in the range of data</param>
		/// <param name="bIgnoreInitial">ignoreInitial is a boolean value that
		/// affects the data range that is considered for the automatic scale
		/// ranging (see <see cref="GraphPane.IsIgnoreInitial"/>).  If true, then initial
		/// data points where the Y value is zero are not included when
		/// automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.  All data after
		/// the first non-zero Y value are included.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		override public void GetRange( 	ref double xMin, ref double xMax,
										ref double yMin, ref double yMax,
										bool bIgnoreInitial, GraphPane pane )
		{
			// Call a default GetRange() that does not include Z data points
			this.points.GetRange( ref xMin, ref xMax, ref yMin, ref yMax, bIgnoreInitial,
									true, barBase == BarBase.X );
		}

	#endregion

	}
}
