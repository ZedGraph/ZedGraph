using System;
using System.Drawing;
using System.Collections;

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="CurveItem"/> objects
	/// that define the set of curves to be displayed on the graph.
	/// </summary>
	public class CurveList : CollectionBase, ICloneable
	{
		/// <summary>
		/// Default constructor for the collection class
		/// </summary>
		public CurveList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The XAxis object from which to copy</param>
		public CurveList( CurveList rhs )
		{
			foreach ( CurveItem item in rhs )
				this.Add( new CurveItem( item ) );
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the CurveList</returns>
		public object Clone()
		{ 
			return new CurveList( this ); 
		}
		
		/// <summary>
		/// Indexer to access the specified <see cref="CurveItem"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="CurveItem"/> object to be accessed.</param>
		/// <value>A <see cref="CurveItem"/> object reference.</value>
		public CurveItem this[ int index ]  
		{
			get { return( (CurveItem) List[index] ); }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="CurveItem"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="curve">A reference to the <see cref="CurveItem"/> object to
		/// be added</param>
		public void Add( CurveItem curve )
		{
			List.Add( curve );
		}

		/// <summary>
		/// Remove a <see cref="CurveItem"/> object from the collection at the
		/// specified ordinal location.
		/// </summary>
		/// <param name="index">An ordinal position in the list at which
		/// the object to be removed is located. </param>
		public void Remove( int index )
		{
			List.RemoveAt( index );
		}

		/// <summary>
		/// Go through each <see cref="CurveItem"/> object in the collection,
		/// calling the <see cref="CurveItem.GetRange"/> member to 
		/// determine the minimum and maximum values in the
		/// <see cref="CurveItem.X"/> and <see cref="CurveItem.Y"/> data arrays.  In the event that no
		/// data are available, a default range of min=0.0 and max=1.0 are returned.
		/// If any <see cref="CurveItem"/> in the list has a missing
		/// <see cref="CurveItem.X"/> or <see cref="CurveItem.Y"/> data array, and suitable
		/// default array will be created with ordinal values.
		/// </summary>
		/// <param name="xMinVal">The minimun X value in the data range for all curves
		/// in this collection</param>
		/// <param name="xMaxVal">The maximun X value in the data range for all curves
		/// in this collection</param>
		/// <param name="yMinVal">The minimun Y (left Y axis) value in the data range
		/// for all curves in this collection</param>
		/// <param name="yMaxVal">The maximun Y (left Y axis) value in the data range
		/// for all curves in this collection</param>
		/// <param name="y2MinVal">The minimun Y2 (right Y axis) value in the data range
		/// for all curves in this collection</param>
		/// <param name="y2MaxVal">The maximun Y2 (right Y axis) value in the data range
		/// for all curves in this collection</param>
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
		public void GetRange( 	out double xMinVal, out double xMaxVal,
								out double yMinVal, out double yMaxVal,
								out double y2MinVal, out double y2MaxVal,
								bool bIgnoreInitial, GraphPane pane )
		{
			double	tXMinVal,
					tXMaxVal,
					tYMinVal,
					tYMaxVal;
						
			// initialize the values to outrageous ones to start
			xMinVal = yMinVal = y2MinVal = tXMinVal = tYMinVal = 1e20;
			xMaxVal = yMaxVal = y2MaxVal = tXMaxVal = tYMaxVal = -1e20;
		
			// Loop over each curve in the collection
			foreach( CurveItem curve in this )
			{
				// Generate default arrays of ordinal values if any data arrays are missing
				curve.DataCheck( pane );
			
				// Call the GetRange() member function for the current
				// curve to get the min and max values
				curve.GetRange( ref tXMinVal, ref tXMaxVal,
								ref tYMinVal, ref tYMaxVal, bIgnoreInitial );
				
				// If the min and/or max values from the current curve
				// are the absolute min and/or max, then save the values
				// Also, differentiate between Y and Y2 values		
				if ( curve.IsY2Axis )
				{
					if ( tYMinVal < y2MinVal )
						y2MinVal = tYMinVal;
					if ( tYMaxVal > y2MaxVal )
						y2MaxVal = tYMaxVal;
				}
				else
				{
					if ( tYMinVal < yMinVal )
						yMinVal = tYMinVal;
					if ( tYMaxVal > yMaxVal )
						yMaxVal = tYMaxVal;
				}
				
				if ( tXMinVal < xMinVal )
					xMinVal = tXMinVal;
				if ( tXMaxVal > xMaxVal )
					xMaxVal = tXMaxVal;
				
			}
		
			// Define suitable default ranges in the event that
			// no data were available
			if ( xMinVal >= 1e20 || xMaxVal <= -1e20 )
			{
				xMinVal = 0;
				xMaxVal = 1;
			}
		
			if ( yMinVal >= 1e20 || yMaxVal <= -1e20 )
			{
				yMinVal = 0;
				yMaxVal = 1;
			}
		
			if ( y2MinVal >= 1e20 || y2MaxVal <= -1e20 )
			{
				y2MinVal = 0;
				y2MaxVal = 1;
			}
		}
		
		/// <summary>
		/// Determine if there is any data in any of the <see cref="CurveItem"/>
		/// objects for this graph.  This method does not verify valid data, it
		/// only checks to see if <see cref="CurveItem.NPts"/> > 0.
		/// </summary>
		/// <returns>true if there is any data, false otherwise</returns>
		public bool HasData()
		{
			foreach( CurveItem curve in this )
			{
				if ( curve.NPts > 0 )
					return true;
			}
			return false;
		}

		/// <summary>
		/// Render all the <see cref="CurveItem"/> objects in the list to the
		/// specified <see cref="Graphics"/>
		/// device by calling the <see cref="CurveItem.Draw"/> member function of
		/// each <see cref="CurveItem"/> object.
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
			// Loop for each curve
			foreach( CurveItem curve in this )
			{
				// Render the curve
				curve.Draw( g, pane, scaleFactor );
			}
		}
	}
}


