using System;
using System.Drawing;

namespace ZedGraph
{
	
	/// <summary>
	/// This class contains the data and methods for an individual curve within
	/// a graph pane.  It carries the settings for the curve including the
	/// key and item names, colors, symbols and sizes, linetypes, etc.
	/// </summary>
	public class CurveItem
	{
		private Symbol		symbol;
		private Line		line;
		
		private string		label;
		private StepType	stepType;
		private bool		isY2Axis;
		
		/// <summary>
		/// <see cref="CurveItem"/> constructor the pre-specifies the curve label and the
		/// x and y data arrays.  All other properties of the curve are
		/// defaulted to the values in the <see cref="Def"/> class.
		/// </summary>
		/// <param name="label">A string label (legend entry) for this curve</param>
		/// <param name="x">A array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="y">A array of double precision values that define
		/// the dependent (Y axis) values for this curve</param>
		public CurveItem( string label, double[] x, double[] y )
		{
			this.line = new Line();
			this.symbol = new Symbol();
			this.label = label;
			this.stepType = Def.Curve.Type;
			this.isY2Axis = false;
			
			int count = x.Length;
			if ( y.Length < count )
				count = y.Length;
			this.X = new double[ count ];
			this.Y = new double[ count ];
			for ( int i=0; i<count; i++ )
			{
				this.X[i] = x[i];
				this.Y[i] = y[i];
			}
		}
								
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Symbol"/> class defined
		/// for this <see cref="CurveItem"/>.
		/// </summary>
		public Symbol Symbol
		{
			get { return symbol; }
		}
		/// <summary>
		/// Gets a reference to the <see cref="ZedGraph.Line"/> class defined
		/// for this <see cref="CurveItem"/>.
		/// </summary>
		public Line Line
		{
			get { return line; }
		}
		/// <summary>
		/// Determines if the <see cref="CurveItem"/> will be drawn by directly connecting the
		/// points from the <see cref="X"/> and <see cref="Y"/> data arrays,
		/// or if the curve will be a "stair-step" in which the points are
		/// connected by a series of horizontal and vertical lines that
		/// represent discrete, constant values.  Note that the values can
		/// be forward oriented <c>ForwardStep</c> (<see cref="ZedGraph.StepType"/>) or
		/// rearward oriented <c>RearwardStep</c>.
		/// That is, the points are defined at the beginning or end
		/// of the constant value for which they apply, respectively.
		/// </summary>
		/// <value><see cref="ZedGraph.StepType"/> enum value</value>
		public StepType StepType
		{
			get { return stepType; }
			set { stepType = value;}
		}		
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
		/// Determines which Y axis this <see cref="CurveItem"/>
		///  is assigned to.  The
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
		/// The array of independent (X Axis) values that define this
		/// <see cref="CurveItem"/>.
		/// The size of this array determines the number of points that are
		/// plotted.  Note that values defined as
		/// System.Double.MaxValue are considered "missing" values,
		/// and are not plotted.  The curve will have a break at these points
		/// to indicate values are missing.
		/// </summary>
		public double[] X;
		
		/// <summary>
		/// The array of dependent (Y Axis) values that define this
		/// <see cref="CurveItem"/>.
		/// The size of this array determines the number of points that are
		/// plotted.  Note that values defined as
		/// System.Double.MaxValue are considered "missing" values,
		/// and are not plotted.  The curve will have a break at these points
		/// to indicate values are missing.
		/// </summary>
		public double[] Y;
		
		/// <summary>
		/// Readonly property that gives the number of points that define this
		/// <see cref="CurveItem"/> object, which is the number of points in the
		/// <see cref="X"/> and <see cref="Y"/> data arrays.
		/// </summary>
		public int NPts
		{
			get { return X.GetLength(0); }
		}

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
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void Draw( Graphics g, GraphPane pane, double scaleFactor  )
		{		
			// If the line is being shown, draw it
			if ( this.Line.IsVisible )
					DrawCurve( g, pane );

			// If symbols are being shown, then draw them
			if ( this.Symbol.IsVisible )
				DrawSymbols( g, pane, scaleFactor );
		}		
		
		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device.  The format (stair-step or line) of the curve is
		/// defined by the <see cref="StepType"/> property.  The routine
		/// only draws the line segments; the symbols are draw by the
		/// <see cref="DrawSymbols"/> method.  This method
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
		protected void DrawCurve( Graphics g, GraphPane pane )
		{
			float	tmpX, tmpY,
					lastX = 0,
					lastY = 0;
			bool	broke = true;
			
			// Loop over each point in the curve
			for ( int i=0; i<this.NPts; i++ )
			{
				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				if ( 	this.X[i] == System.Double.MaxValue ||
						this.Y[i] == System.Double.MaxValue ||
						( pane.XAxis.IsLog && this.X[i] <= 0.0 ) ||
						( this.isY2Axis && pane.Y2Axis.IsLog && this.Y[i] <= 0.0 ) ||
						( !this.isY2Axis && pane.YAxis.IsLog && this.Y[i] <= 0.0 ) )
				{
					broke = true;
				}
				else
				{
					// Transform the current point from user scale units to
					// screen coordinates
					tmpX = pane.XAxis.Transform( this.X[i] );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( this.Y[i] );
					else
						tmpY = pane.YAxis.Transform( this.Y[i] );
					
					// off-scale values "break" the line
					if ( tmpX < -100000 || tmpX > 100000 ||
							tmpY < -100000 || tmpY > 100000 )
						broke = true;
					else
					{
						// If the last two points are valid, draw a line segment
						if ( !broke )
						{
							if ( this.stepType == StepType.ForwardStep )
							{
								this.Line.Draw( g, lastX, lastY, tmpX, lastY );
								this.Line.Draw( g, tmpX, lastY, tmpX, tmpY );
							}
							else if ( this.stepType == StepType.RearwardStep )
							{
								this.Line.Draw( g, lastX, lastY, lastX, tmpY );
								this.Line.Draw( g, lastX, tmpY, tmpX, tmpY );
							}
							else 		// non-step
								this.Line.Draw( g, lastX, lastY, tmpX, tmpY );

						}

						// Save some values for the next point
						broke = false;
						lastX = tmpX;
						lastY = tmpY;
					}
				}
			}
		}

		/// <summary>
		/// Draw the this <see cref="CurveItem"/> to the specified <see cref="Graphics"/>
		/// device as a symbol at each defined point.  The routine
		/// only draws the symbols; the lines are draw by the
		/// <see cref="DrawCurve"/> method.  This method
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
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		public void DrawSymbols( Graphics g, GraphPane pane, double scaleFactor )
		{
			float tmpX, tmpY;
			
			// Loop over each defined point							
			for ( int i=0; i<this.NPts; i++ )
			{
				// Any value set to double max is invalid and should be skipped
				// This is used for calculated values that are out of range, divide
				//   by zero, etc.
				// Also, any value <= zero on a log scale is invalid
				
				if (	this.X[i] != System.Double.MaxValue &&
						this.Y[i] != System.Double.MaxValue &&
						( this.X[i] > 0 || !pane.XAxis.IsLog ) &&
						( this.Y[i] > 0 ||
							(this.isY2Axis && !pane.Y2Axis.IsLog ) ||
							(!this.isY2Axis && !pane.YAxis.IsLog ) ) )
				{
					tmpX = pane.XAxis.Transform( this.X[i] );
					if ( this.isY2Axis )
						tmpY = pane.Y2Axis.Transform( this.Y[i] );
					else
						tmpY = pane.YAxis.Transform( this.Y[i] );

					this.Symbol.Draw( g, tmpX, tmpY, scaleFactor );		
				}
			}
		}

		/// <summary>
		/// Go through <see cref="X"/> and <see cref="Y"/> data arrays
		/// for this <see cref="CurveItem"/>
		/// and determine the minimum and maximum values in the data.
		/// </summary>
		/// <param name="xMin">The minimum X value in the range of data</param>
		/// <param name="xMax">The maximum X value in the range of data</param>
		/// <param name="yMin">The minimum Y value in the range of data</param>
		/// <param name="yMax">The maximum Y value in the range of data</param>
		/// <param name="ignoreInitial">ignoreInitial is a boolean value that
		/// affects the data range that is considered for the automatic scale
		/// ranging (see <see cref="GraphPane.IsIgnoreInitial"/>).  If true, then initial
		/// data points where the Y value is zero are not included when
		/// automatically determining the scale <see cref="Axis.Min"/>,
		/// <see cref="Axis.Max"/>, and <see cref="Axis.Step"/> size.  All data after
		/// the first non-zero Y value are included.
		/// </param>
		public void GetRange( ref double xMin, ref double xMax,
							ref double yMin, ref double yMax,
							bool ignoreInitial )
		{
			// initialize the values to outrageous ones to start
			xMin = yMin = 1.0e20;
			xMax = yMax = -1.0e20;
			
			// Loop over each point in the arrays
			for ( int i=0; i<this.NPts; i++ )
			{
				// ignoreInitial becomes false at the first non-zero
				// Y value
				if ( ignoreInitial && this.Y[i] != 0 &&
							this.Y[i] != System.Double.MaxValue )
					ignoreInitial = false;
				
				if ( !ignoreInitial && this.X[i] != System.Double.MaxValue &&
					this.Y[i] != System.Double.MaxValue )
				{
					if ( this.X[i] < xMin )
						xMin = this.X[i];
					if ( this.X[i] > xMax )
						xMax = this.X[i];
					if ( this.Y[i] < yMin )
						yMin = this.Y[i];
					if ( this.Y[i] > yMax )
						yMax = this.Y[i];
				}
			}	
		}
	}
}



