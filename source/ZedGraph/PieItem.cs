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
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// A class representing a pie chart object comprised of one or more
	/// <see cref="PieSlice"/>s.
	/// </summary>
	/// <author> John Champion </author>
	/// <version> $Revision: 1.1 $ $Date: 2005-01-09 04:52:28 $ </version>
	public class PieItem : ZedGraph.CurveItem , ICloneable	
	{
		
	#region Fields
		/// <summary>  Private field instance of the <see cref="ZedGraph.PieItem"/> class 
		/// representing the number of <see cref="PieSlice"/> items in this pie.  Use the
		/// public property <see cref="PieItem.NumbSlices"/> to access this data.
		 /// </summary>
		private int numbSlices;
		/// <summary>
		/// Private field instance of the <see cref="PieItem"/> class indicating whether
		/// the instance is displayed in 2D or 3D.(see <see cref="PieSlice.PieType"/>)
		///   Use the public property <see cref="PieItem.PieType"/> to access this data
		/// </summary>
		private PieType pieType;

		/// <summary>Private field instance of the <see cref="ZedGraph.PieItem"/> class 
		/// representing the Title of this <see cref="PieItem"/>.  This data would be used
		/// where more than one <see cref="PieItem"/> will be displayed.  Use the
		/// public property <see cref="PieItem.PieTitle"/> to access this data.
		/// </summary>
		private string pieTitle;


		/// <summary>Private field instance of the <see cref="ZedGraph.PieItem"/> class 
		/// representing the <see cref="PieSlice"/>items which make up this <see cref="PieItem"/>.  Use the
		/// public property <see cref="PieItem.SliceList"/> to access this data.</summary>
		 private ArrayList sliceList;
	
		/// <summary>
		///An enum that specifies how each <see cref="PieSlice.label"/> for this <see cref="Pie"/> object 
		///will be displayed.  Use the public property <see cref="Pie.LabelType"/> to access this data.  
		/// Use enum <see cref="PieLabelType"/>.
		/// </summary>
		private PieLabelType labelType ;
	#endregion

		private ColorSymbolRotator rotator = new ColorSymbolRotator () ;

	#region PieItem Properties
		/// <summary>
		/// Internal property returning a reference to the <see cref="PieSlice"/> with
		/// the largest displacement.
		/// </summary>
		internal PieSlice MaxDisplacedSlice
		{
			get
			{
				double max = 0 ;
				PieSlice maxSlice = null ;
				foreach (PieSlice slice in sliceList)
				{
					if (slice.Displacement > max)
					{			
						max = slice.Displacement  ;
						maxSlice = slice ;
					}
				}
				return (maxSlice);
			}
		}
		/// <summary>
		/// Gets or sets the list of <see cref="PieSlice"/> items for this <see cref="PieItem"/>
		/// </summary>
		/// <value>A reference to a <see cref="PieSlice"/> collection object</value>
		public ArrayList SliceList
		{
			get	  { return (this.sliceList); }
			set { this.sliceList = value; }
		}

	/// <summary>
	///Gets or sets the <see cref="PieLabelType"/> to be used in display.
	/// </summary>
		public	PieLabelType LabelType
		{
			get { return (this.labelType); }
			set { this.labelType = value; }
		}
		
	/// <summary>
		///Internal property for getting the total value of all <see cref="PieSlices"/> in 
		///this <see cref="PieItem"/> .
		/// </summary>
		internal double PieTotalValue
		{
			get 
			{ 
				double total = 0 ; 
				foreach (PieSlice slice in sliceList) total += slice.Value ;				  
				return (total);
			}
		}

		/// <summary>
		/// Getsor sets enum <see cref="PieType"/> to be used	for drawing this <see cref="PieItem"/>.
		/// </summary>
		public PieType PieType
		{
			get { return (this.pieType); }
			set { this.pieType = value; }
		}
		/// <summary>
		/// Gets or sets a string  representing the  <see cref="PieTitle"/> item for this <see cref="PieItem"/>.
		/// </summary>
		/// <value>A string containing the <see cref="PieTitle"/></value>
		public string PieTitle
		{
			get { return (this.pieTitle); }
			set { this.pieTitle = value; }
		}

		/// <summary>
		/// Gets or sets an int  representing the  number of <see cref="PieSlice"/> items for this <see cref="PieItem"/>
		/// </summary>
		/// <value>An int containing the number of <see cref="PieSlice"/>items.</value>
		internal int NumbSlices
		{
			get { return (this.numbSlices); }
			set { this.numbSlices = value; }
		}
 
	#endregion

	#region	 Constructors
		/// <summary>
		/// The minimal constructor for creating a new <see cref="PieItem"/>.  Default
		/// values for the other parameters will be assigned during the construction of the
		/// <see cref="PieSlices"/>.
		/// </summary>
		/// <param name="title">A string representing the <see cref="PieTitle"/></param>
		/// <param name="slices">An int representing the number of <see cref="PieSlice"/>
		/// items to be added</param>
      public PieItem( string title, int slices )	: base ( title, slices )
		{
			this.NumbSlices = slices ;

			this.PieTitle = title ;

			if ( sliceList == null )	
				sliceList = new ArrayList () ;

			for ( int x = 0 ; x < numbSlices ; x++ )
			{
				PieSlice slice = new PieSlice( this, x, rotator.NextColor ) ;
				sliceList.Add (slice) ;
			}
			RecalculateSliceAngles () ;

/*
			for ( int x = 0 ; x < this.SliceList.Count ; x++ )
			{
				((PieSlice)(sliceList[x])).StartAngle = x == 0? 0 : ((PieSlice)(sliceList[x - 1])).StartAngle + 
					((PieSlice)(sliceList[x - 1 ])).SweepAngle ;
				((PieSlice)(sliceList[x])).SweepAngle = (float)(360 * ((PieSlice)(sliceList[x])).Value / this.PieTotalValue) ;
			}
*/
			this.pieType = PieType.Pie2D ;
			this.labelType = PieLabelType.Value ;

		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pieLabel"></param>
		/// <param name="values"></param>
		public PieItem( string pieLabel, double [] values )	: base ( values )
		{
			NumbSlices = values.Length ;
			pieTitle = pieLabel ;

			if ( sliceList == null )	
				sliceList = new ArrayList () ;

			for ( int x = 0 ; x < numbSlices ; x++ )
			{
				PieSlice slice = new PieSlice( this,x, values[x],  rotator.NextColor ) ;
				sliceList.Add (slice) ;
			}
			
			RecalculateSliceAngles () ;
/*
			for ( int x = 0 ; x < this.SliceList.Count ; x++ )
			{
				((PieSlice)(sliceList[x])).StartAngle = x == 0? 0 : ((PieSlice)(sliceList[x - 1])).StartAngle + 
														((PieSlice)(sliceList[x - 1 ])).SweepAngle ;
				((PieSlice)(sliceList[x])).SweepAngle = (float)(360 * ((PieSlice)(sliceList[x])).Value / this.PieTotalValue) ;
			}
*/
			this.pieType = PieType.Pie2D ;
			this.labelType = PieLabelType.Value ;
		}
		
		/// <summary>
		/// Constructor for creating a <see cref="PieItem"/> item when all, or most, values are known.  
		/// If any of the parameters are null, default values will be assigned.
		/// </summary>
		/// <param name="pieLabel">A string that can be used to assign a specific title
		///  to this <see cref="PieItem"/>instance.</param>
		/// <param name="values">A double [] containing the values of each <see cref="PieSlice"/>/param>
		/// <param name="colors">A Color [] containing the colors of each <see cref="PieSlice"/></param>
		/// <param name="displaced">a bool [] containing values which indicate whether each 
		///   <see cref ="PieSlice"/> is displaced from the center.</param>
		/// <param name="displacements">A double [] containing values which represent the amount, 
		/// expressed as a percentage of the pie radius, a <see cref="PieSlice"/> is displaced from the 
		/// center of the pie.  The maximum value is 25% (0.25)...any value greater than this will be adjusted 
		/// downward. </param>
		/// <param name="labels">A string [] representing the labels assigned to each <see cref="PieSlice"/>.</param>
		public PieItem ( string pieLabel, double [] values, Color [] colors, 
														double [] displacements, string [] labels )  : base ( values )
		{
			if ( values != null)
				NumbSlices = values.Length ; 
			else if ( colors != null)
				NumbSlices = colors.Length ; 
			else if ( displacements != null )
				NumbSlices = displacements.Length ; 
			else if ( labels != null )
				NumbSlices = labels.Length ; 

			this.pieTitle = pieLabel ;

			if ( sliceList == null )	
					sliceList = new ArrayList () ;

			if ( numbSlices > 0)				  //there's some valid values
			{
				for ( int x = 0 ; x < numbSlices ; x++ )
				{
					PieSlice slice = new PieSlice( this,  x , values != null? values[x]: PieSlice.Default.value, 
								colors != null? colors[x]: Color.White, 
								displacements != null? displacements[x]:PieSlice.Default.displacement, 
								labels != null? labels[x]: "Default") ;
					sliceList.Add (slice) ;
				}
			}
			else						//all nulls for data arrays - make a single slice just so that something is displayed
			{
				PieSlice slice = new PieSlice( this,  0 , PieSlice.Default.value, Color.White,				 //display White as indicator..
							PieSlice.Default.displacement ,  "Default") ;
				sliceList.Add (slice) ;
				slice.StartAngle = (float)0;
				slice.SweepAngle = (float)360 ;
				slice.MidAngle = 180 ;
			}
			RecalculateSliceAngles () ;

			this.pieType = PieType.Pie2D ;
			this.labelType = PieLabelType.Value ;
		}
		

			/// <summary>
			/// The Copy Constructor
			/// </summary>
			/// <param name="rhs">The <see cref="PieItem"/> object from which to copy</param>
		public PieItem( PieItem rhs ) : base( rhs )
		{
		}


		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="ErrorBarItem"/></returns>
		override public object Clone()
		{ 
			return new PieItem( this ); 
		}

	#endregion
		
	#region	  Methods
		/// <summary>
		/// Add a <see cref="PieSlice"/> to an existing <see cref="PieItem"/>
		/// </summary>
		/// <param name="value">The value associated with this <see cref="PieSlice"/>item./param>
		/// <param name="color">The display color for this <see cref="PieSlice"/>item.</param>
		/// <param name="displaced">The  associated with this <see cref="PieSlice"/>item.</param>
		/// <param name="displacement">The amount this <see cref="PieSlice"/>item will be 
		/// displaced from the center of the <see cref="PieItem"/>.</param>
		/// <param name="label">Text label for this <see cref="PieSlice"/></param>
		public void AddSlice (  double value, Color color,  double displacement, string label )
		{
			PieSlice slice = new PieSlice( this,  numbSlices , value, color, displacement, label ) ;
			sliceList.Add (slice) ;
			numbSlices++ ;
			this.points.Add (value) ;

			RecalculateSliceAngles () ;
		}
		
		/// <summary>
		/// Change the value associated with a specific <see cref="PieSlice"/>.
		/// </summary>
		/// <param name="index">An int containing the <see cref="PieSlice.sliceIndex"/> for
			/// the <see cref="PieSlice.value"/> to be changed.</param>
	/// <param name="values">A double  containing the new <see cref="PieSlice"/>value.
		/// for this <see cref="PieSlice"/></param>
		public void ChangeSliceValue ( int index, double value )	
		{
			if ( index > this.SliceList.Count - 1 || index < 0  )
				return ;

			((PieSlice)(this.SliceList[index])).Value	= value ;
			
			RecalculateSliceAngles () ;
		}
		/// <summary>
		/// Change all <see cref="PieSlice.value"/>	for this <see cref="PieItem"/>
		/// </summary>
		/// <param name="values">A double [] containing the <see cref="PieSlice"/>values
		/// for this <see cref="PieItem"/></param>
		public void ChangeSliceValues ( double [] values )	
		{
			if ( numbSlices != values.Length )
				return ;

			this.points = new PointPairList(values ) ;
			for ( int x = 0 ; x < this.SliceList.Count ; x++ )
				((PieSlice)this.sliceList[x]).Value = points[x].X ;
			
			RecalculateSliceAngles () ;
		}

		/// <summary>
		/// A method which recalculates the various angle fields in each <see cref="PieSlice"/> objects.  This
		/// method is called when any sort of a change in <see cref="PieSlice.value"/> is experienced.
		/// </summary>
		private void RecalculateSliceAngles ()
		{
			for ( int x = 0 ; x < this.SliceList.Count ; x++ )
			{
				((PieSlice)(sliceList[x])).StartAngle = x == 0? 0 : ((PieSlice)(sliceList[x - 1])).StartAngle + 
					((PieSlice)(sliceList[x - 1 ])).SweepAngle ;
				((PieSlice)(sliceList[x])).SweepAngle = (float)(360 * ((PieSlice)(sliceList[x])).Value / this.PieTotalValue) ;
				((PieSlice)(sliceList[x])).MidAngle = ((PieSlice)(sliceList[x])).StartAngle + ((PieSlice)(sliceList[x])).SweepAngle / 2 ;
			}
		}
		/// <summary>
		/// Do all rendering associated with this <see cref="PieItem"/> item to the specified
		/// <see cref="Graphics"/> device.  This method is normally only
		/// called by the Draw method of the parent <see cref="ZedGraph.CurveList"/>
		/// collection object.  This method will layout the display (multiple <see cref="PieItem"/>
		///  items) , if necessary, draw <see cref="PieItem"/> specific features (<see cref="PieTitle"/> then call 
		///   <see cref="PieSlice"/> Draw methods to actually render the display.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="pos">Not used for rendering Pies</param>param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="GraphPane.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>				
		override public void Draw( Graphics g, GraphPane pane, int pos, double scaleFactor  )
		{
			RectangleF nonExplRect ;

			if ( pane.CurveList.IsPieOnly )
				nonExplRect = pane.PieRect ;
			else
				nonExplRect = pane.AxisRect ;					  //if isAxisRectAuto = false
			
			if ( MaxDisplacedSlice != null )												 //need new rectangle if any slice exploded
				CalcNewBaseRect ( MaxDisplacedSlice, ref nonExplRect ) ;
			
			//now gotta loop thru the slices and draw them
			foreach ( PieSlice slice in this.sliceList)
				slice.Draw (g, pane, nonExplRect ) ;
		}

		
		/// <summary>
		///A method which calculates the size of	 the bounding rectangle for the non-displaced 
		///<see cref="PieSlices"/>s in this <see cref="PieItem"/>.  This method is called after it is found
		///that at least one slice is displaced.
		/// </summary>
		/// <param name="slice">A reference to the <see cref="PieSlice"/> with the 
		/// greatest displacement.</param>
		/// <param name="baseRect">Largest square in axisRect"Pie"/></param>
		private void CalcNewBaseRect ( PieSlice slice, ref RectangleF baseRect )
		{
					//displacement expressed in terms of % of pie radius	...do not want exploded slice to 
					//go beyond nonExplRect, but want to maintain the same center point...therefore, got to 
					//reduce the diameter of the nonexploded pie by the alue of the displacement

			float xDispl =	(float)((slice.Displacement*baseRect.Width/2)) ; 
			float yDispl	 =  (float)((slice.Displacement * baseRect.Height/2) ); 

			baseRect.Inflate ( -(float)((xDispl / 2)), -(float)((xDispl / 2 ) )  );

		}
		
		/// <summary>
		/// Draw a legend key entry for this <see cref="PieItem"/> at the specified location
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
		}

	#endregion  
	
	
	}
}
