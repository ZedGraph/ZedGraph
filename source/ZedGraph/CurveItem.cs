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
	/// This class contains the data and methods for an individual curve within
	/// a graph pane.  It carries the settings for the curve including the
	/// key and item names, colors, symbols and sizes, linetypes, etc.
	/// </summary>
	/// 
	/// <author> John Champion
	/// modified by Jerry Vos </author>
	/// <version> $Revision: 3.9 $ $Date: 2004-11-17 03:35:39 $ </version>
	abstract public class CurveItem
	{
	
	#region Fields		
		/// <summary>
		/// protected field that stores a legend label string for this
		/// <see cref="CurveItem"/>.  Use the public
		/// property <see cref="Label"/> to access this value.
		/// </summary>
		protected string	label;
		/// <summary>
		/// protected field that stores the boolean value that determines whether this
		/// <see cref="CurveItem"/> is on the left Y axis or the right Y axis (Y2).
		/// Use the public property <see cref="IsY2Axis"/> to access this value.
		/// </summary>
		protected bool		isY2Axis;
		/// <summary>
		/// protected field that stores the boolean value that determines whether this
		/// <see cref="CurveItem"/> is visible on the graph.
		/// Use the public property <see cref="IsVisible"/> to access this value.
		/// Note that this value turns the curve display on or off, but it does not
		/// affect the display of the legend entry.  To hide the legend entry, you
		/// have to set <see cref="IsLegendLabelVisible"/> to false.
		/// </summary>
		protected bool		isVisible;
		/// <summary>
		/// protected field that stores the boolean value that determines whether the label
		/// for this <see cref="CurveItem"/> is visible in the legend.
		/// Use the public property <see cref="IsLegendLabelVisible"/> to access this value.
		/// Note that this value turns the legend entry display on or off, but it does not
		/// affect the display of the curve on the graph.  To hide the curve, you
		/// have to set <see cref="IsVisible"/> to false.
		/// </summary>
		protected bool		isLegendLabelVisible;
		
		/// <summary>
		/// The <see cref="PointPairList"/> of value sets that
		/// represent this <see cref="CurveItem"/>.
		/// The size of this list determines the number of points that are
		/// plotted.  Note that values defined as
		/// System.Double.MaxValue are considered "missing" values
		/// (see <see cref="PointPair.Missing"/>),
		/// and are not plotted.  The curve will have a break at these points
		/// to indicate the values are missing.
		/// </summary>
		protected PointPairList points;
	#endregion
	
	#region Constructors
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label, the
		/// x and y data values as a <see cref="PointPairList"/>, the curve
		/// type (Bar or Line/Symbol), the <see cref="Color"/>, and the
		/// <see cref="SymbolType"/>. Other properties of the curve are
		/// defaulted to the values in the <see cref="GraphPane.Default"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">An array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		public CurveItem( string label, double[] x, double[] y ) :
				this( label, new PointPairList( x, y ) )
		{
		}
		
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label, the
		/// x and y data values as a <see cref="PointPairList"/>, the curve
		/// type (Bar or Line/Symbol), the <see cref="Color"/>, and the
		/// <see cref="SymbolType"/>. Other properties of the curve are
		/// defaulted to the values in the <see cref="GraphPane.Default"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="points">A <see cref="PointPairList"/> of double precision value pairs that define
		/// the X and Y values for this curve</param>
		public CurveItem( string label, PointPairList points )
		{
			Init( label );

			if ( points == null )
				this.points = new PointPairList();
			else
				this.points = (PointPairList) points.Clone();
		}
		
		/// <summary>
		/// Internal initialization routine thats sets some initial values to defaults.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		private void Init( string label )
		{
			this.label = label == null ? "" : label;
			this.isY2Axis = false;
			this.isVisible = true;
			this.isLegendLabelVisible = true;			
		}
			
		/// <summary>
		/// <see cref="CurveItem"/> constructor that specifies the label of the CurveItem.
		/// This is the same as <c>CurveItem(label, null, null)</c>.
		/// <seealso cref="CurveItem( string, double[], double[] )"/>
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		public CurveItem( string label ): this( label, null )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The CurveItem object from which to copy</param>
		public CurveItem( CurveItem rhs )
		{
			label = rhs.Label;
			isY2Axis = rhs.IsY2Axis;
			isVisible = rhs.IsVisible;
			isLegendLabelVisible = rhs.IsLegendLabelVisible;
			
			this.points = (PointPairList) rhs.Points.Clone();
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the CurveItem</returns>
		public abstract object Clone();
		
	#endregion
	
	#region Properties
		/// <summary>
		/// A text string that represents the <see cref="ZedGraph.Legend"/>
		/// entry for the this
		/// <see cref="CurveItem"/> object
		/// </summary>
		public string Label
		{
			get { return label; }
			set { label = value;}
		}

		/// <summary>
		/// The <see cref="Line"/>/<see cref="Symbol"/>/<see cref="Bar"/> 
		/// color (FillColor for the Bar).  This is a common access to
		/// <see cref="ZedGraph.Line.Color"/>, <see cref="ZedGraph.Border.Color"/>, and
		/// <see cref="ZedGraph.Fill.Color"/> properties for this curve.
		/// </summary>
		public Color Color
		{
			get
			{
				if ( this.IsBar )
					return ((BarItem) this).Bar.Fill.Color;
				else if ( ((LineItem) this).Line.IsVisible )
					return ((LineItem) this).Line.Color;
				else
					return ((LineItem) this).Symbol.Border.Color;
			}
			set 
			{
				if ( this.IsBar )
				{
					((BarItem) this).Bar.Fill.Color = value;
				}
				else
				{
					((LineItem) this).Line.Color			= value;
					((LineItem) this).Symbol.Border.Color	= value;
					((LineItem) this).Symbol.Fill.Color		= value;
				}
			}
		}

		/// <summary>
		/// Determines whether this <see cref="CurveItem"/> is visible on the graph.
		/// Note that this value turns the curve display on or off, but it does not
		/// affect the display of the legend entry.  To hide the legend entry, you
		/// have to set <see cref="IsLegendLabelVisible"/> to false.
		/// </summary>
		public bool IsVisible
		{
			get { return isVisible; }
			set { isVisible = value; }
		}
		/// <summary>
		/// Determines whether the label for this <see cref="CurveItem"/> is visible in the legend.
		/// Note that this value turns the legend entry display on or off, but it does not
		/// affect the display of the curve on the graph.  To hide the curve, you
		/// have to set <see cref="IsVisible"/> to false.
		/// </summary>
		public bool IsLegendLabelVisible
		{
			get { return isLegendLabelVisible; }
			set { isLegendLabelVisible = value; }
		}

		/// <summary>
		/// Determines which Y axis this <see cref="CurveItem"/>
		/// is assigned to.  The
		/// <see cref="ZedGraph.YAxis"/> is on the left side of the graph and the
		/// <see cref="ZedGraph.Y2Axis"/> is on the right side.  Assignment to an axis
		/// determines the scale that is used to draw the curve on the graph.
		/// </summary>
		/// <value>true to assign the curve to the <see cref="ZedGraph.Y2Axis"/>,
		/// false to assign the curve to the <see cref="ZedGraph.YAxis"/></value>
		public bool IsY2Axis
		{
			get { return isY2Axis; }
			set { isY2Axis = value; }
		}
		
		/// <summary>
		/// Determines whether this <see cref="CurveItem"/>
		/// is a <see cref="BarItem"/>.
		/// </summary>
		/// <value>true for a bar chart, or false for a line graph</value>
		public bool IsBar
		{
			get { return this is BarItem; }
		}
		
		/// <summary>
		/// Determines whether this <see cref="CurveItem"/>
		/// is a <see cref="LineItem"/>.
		/// </summary>
		/// <value>true for a bar chart, or false for a line graph</value>
		public bool IsLine
		{
			get { return this is LineItem; }
		}
		
		/// <summary>
		/// Readonly property that gives the number of points that define this
		/// <see cref="CurveItem"/> object, which is the number of points in the
		/// <see cref="Points"/> data collection.
		/// </summary>
		public int NPts
		{
			get 
			{
				if ( this.points == null )
						return 0;
					else
						return this.points.Count;
			}
		}
		
		/// <summary>
		/// The <see cref="PointPairList"/> of X,Y point sets that represent this
		/// <see cref="CurveItem"/>.
		/// </summary>
		public PointPairList Points
		{
			get { return points; }
			set { points = value; }
		}

		/// <summary>
		/// An accessor for the <see cref="PointPair"/> datum for this <see cref="CurveItem"/>.
		/// Index is the ordinal reference (zero based) of the point.
		/// </summary>
		public PointPair this[int index]
		{
			get
			{
				if ( this.points == null )
					return new PointPair( PointPair.Missing, PointPair.Missing );
				else
					return ( this.points )[index];
			}
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="CurveItem"/> to the specified
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
		abstract public void Draw( Graphics g, GraphPane pane, int pos, double scaleFactor  );
		
		/// <summary>
		/// Draw a legend key entry for this <see cref="CurveItem"/> at the specified location.
		/// This abstract base method passes through to <see cref="BarItem.DrawLegendKey"/> or
		/// <see cref="LineItem.DrawLegendKey"/> to do the rendering.
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
		abstract public void DrawLegendKey( Graphics g, GraphPane pane, RectangleF rect, double scaleFactor );
		
	#endregion

	#region Utility Methods

		/// <summary>
		/// Add a single x,y coordinate point to the end of the points collection for this curve.
		/// </summary>
		/// <param name="x">The X coordinate value</param>
		/// <param name="y">The Y coordinate value</param>
		public void AddPoint( double x, double y )
		{
			this.AddPoint( new PointPair( x, y ) );
		}

		/// <summary>
		/// Add a <see cref="PointPair"/> object to the end of the points collection for this curve.
		/// </summary>
		/// <param name="point">A reference to the <see cref="PointPair"/> object to
		/// be added</param>
		public void AddPoint( PointPair point )
		{
			if ( this.points == null )
				this.Points = new PointPairList();
			
			this.points.Add( point );
		}

		/// <summary>
		/// Clears the points from this <see cref="CurveItem"/>.  This is the same
		/// as <c>CurveItem.Points.Clear()</c>.
		/// </summary>
		public void Clear()
		{
			points.Clear();
		}

		/// <summary>
		/// Loads some pseudo unique colors/symbols into this CurveItem.  This
		/// is the same as <c>MakeUnique(ColorSymbolRotator.StaticInstance)</c>.
		/// <seealso cref="ColorSymbolRotator.StaticInstance"/>
		/// <seealso cref="ColorSymbolRotator"/>
		/// <seealso cref="MakeUnique(ColorSymbolRotator)"/>
		/// </summary>
		public void MakeUnique()
		{
			this.MakeUnique( ColorSymbolRotator.StaticInstance );
		}

		/// <summary>
		/// Loads some pseudo unique colors/symbols into this CurveItem.  This
		/// is mainly useful for differentiating a set of new CurveItems without
		/// having to pick your own colors/symbols.
		/// <seealso cref="MakeUnique(ColorSymbolRotator)"/>
		/// </summary>
		/// <param name="rotator">
		/// The <see cref="ColorSymbolRotator"/> that is used to pick the color
		///  and symbol for this method call.
		/// </param>
		virtual public void MakeUnique( ColorSymbolRotator rotator )
		{
			this.Color			= rotator.NextColor;
		}
	
		/// <summary>
		/// Go through the list of <see cref="PointPair"/> data values for this <see cref="CurveItem"/>
		/// and determine the minimum and maximum values in the data.
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
		virtual public void GetRange( 	ref double xMin, ref double xMax,
										ref double yMin, ref double yMax,
										bool bIgnoreInitial, GraphPane pane )
		{
			// Call a default GetRange() that does not include Z data points
			this.points.GetRange( ref xMin, ref xMax, ref yMin, ref yMax, bIgnoreInitial, false, true );
		}

	#endregion
	
	#region Inner classes	
		/// <summary>
		/// Compares <see cref="CurveItem"/>'s based on the point value at the specified
		/// index and for the specified axis.
		/// <seealso cref="System.Collections.ArrayList.Sort()"/>
		/// </summary>
		public class Comparer : IComparer 
		{
			private int index;
			private SortType sortType;
			
			/// <summary>
			/// Constructor for Comparer.
			/// </summary>
			/// <param name="type">The axis type on which to sort.</param>
			/// <param name="index">The index number of the point on which to sort</param>
			public Comparer( SortType type, int index )
			{
				this.sortType = type;
				this.index = index;
			}
			
			/// <summary>
			/// Compares two <see cref="CurveItem"/>s using the previously specified index value
			/// and axis.  Sorts in descending order.
			/// </summary>
			/// <param name="l">Curve to the left.</param>
			/// <param name="r">Curve to the right.</param>
			/// <returns>-1, 0, or 1 depending on l.X's relation to r.X</returns>
			int IComparer.Compare( object l, object r ) 
			{
				if (l == null && r == null )
					return 0;
				else if (l == null && r != null ) 
					return -1;
				else if (l != null && r == null) 
					return 1;

				CurveItem lc = (CurveItem) l;
				CurveItem rc = (CurveItem) r;
				if ( rc != null && rc.NPts <= index )
					r = null;
				if ( lc != null && lc.NPts <= index )
					l = null;
						
				double lVal, rVal;

				if ( sortType == SortType.XValues )
				{
					lVal = System.Math.Abs( lc[index].X );
					rVal = System.Math.Abs( rc[index].X );
				}
				else
				{
					lVal = System.Math.Abs( lc[index].Y );
					rVal = System.Math.Abs( rc[index].Y );
				}
				
				if ( lVal == PointPair.Missing || Double.IsInfinity( lVal ) || Double.IsNaN( lVal ) )
					l = null;
				if ( rVal == PointPair.Missing || Double.IsInfinity( rVal ) || Double.IsNaN( rVal ) )
					r = null;
					
				if ( (l == null && r == null) || ( System.Math.Abs( lVal - rVal ) < 1e-10 ) )
					return 0;
				else if (l == null && r != null ) 
					return -1;
				else if (l != null && r == null) 
					return 1;
				else
					return rVal < lVal ? -1 : 1;
			}
		}
	
	#endregion

	}
}



