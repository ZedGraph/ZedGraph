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
	///A	class that encompasses all the characteristics of a single pie slice making up
	///part of a <see cref="PieItem"/> object.
	/// </summary>
	/// <author> Bob Kaye </author>
	/// <version> $Revision: 1.2 $ $Date: 2005-01-16 03:46:12 $ </version>
	public	class PieSlice
	{
	#region Fields
		
		/// <summary>
		///Private field which controls whether this <see cref="PieSlice"/> object
		///is displayed.  When set to true, object will be displayed. 
		/// </summary>
		private bool isVisible;

		/// <summary>
		/// Private field which holds the angle (in degrees) at which the display of this <see cref="PieSlice"/>
		/// object will begin.
		/// </summary>
		private float startAngle;

		/// <summary>
		///Private field which holds the length (in degrees) of the arc representing this <see cref="PieSlice"/> 
		///object.
		/// </summary>
		private float sweepAngle;

			/// <summary>
		///Private field which represents the angle (in degrees) of the radius along which this <see cref="PieSlice"/>
		///object will be displaced, if desired.
		/// </summary>
		private float midAngle;

		/// <summary>
		/// Private	field	that stores the	<see cref="ZedGraph.Fill"/> data for this
		/// <see	cref="PieSlice"/>.	 Use	the public property <see	cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill  fill;
		
		/// <summary>
		/// Private	field	that stores the	<see cref="Border"/> class that defines	the
		/// properties of the	border around	this <see cref="PieSlice"/>. Use the public
		/// property	<see cref="Border"/> to access this value.
		/// </summary>
		private Border border;
		
		/// <summary>
		///A zero based int representing the order in which the <see cref="PieSlice"/> 
		///was constructed. 
		/// </summary>
		/// <remarks>Currently, the order in which the <see cref="PieSlice"/> objects 
		/// are created controls the order in which they are displayed.  The first  
		/// <see cref="PieSlice"/> object created will be displayed at
		/// the three o'clock position...90 degrees in axis terms, but zero
		///  degrees in GDI+ terms.</remarks>
		/// 
		private int	sliceIndex;

		 /// <summary>
		/// Percentage (expressed as #.##) of <see cref="PieItem"/>	diameter  to
		/// which this <see cref="PieSlice"/> is to be displaced from the center of
		/// the <see cref="PieItem"/>.  Displacement is done outward  along the radius
		/// bisecting the chord of this <see cref="PieSlice"/>.  Maximum allowable value
		/// is 1.0.
		/// </summary>
		private double	displacement;

		/// <summary>
		///Private field that stores the absolute value of this <see cref="PieSlice"/> instance.
		///Value will be set to zero is submitted value is less than zero. 
		/// </summary>
		private double	value;

		/// <summary>
		/// Private field that stores the text associated with this <see cref="PieSlice"/>
		/// instance.  This text will appear in the <see cref="GraphPane.Legend"/> as
		/// well as in the chart display
		/// </summary>
		private string	label;

		/// <summary>
		/// Private	field	that stores a	reference	to the <see	cref="ZedGraph.PieItem"/>
		/// class	instance for which this <see	cref="PieSlice"/>is the parent.	 Use	the public
		/// property	<see cref="PieItem"/> to access this value.
		/// </summary>
		private PieItem pieItem;
	#endregion	Fields
 

	#region Defaults
		/// <summary>
		/// A	simple struct	that defines the default property values for 
		/// the	<see cref="ZedGraph.PieSlice"/> class.
		/// </summary>
		public	struct Default
		{
			/// <summary>
			///Default <see cref="PieSlice "/> value.
			/// </summary>
			public	static double	value = 10;

			/// <summary>
			///Default <see cref="PieSlice "/> displacement.
			/// </summary>
			public	static double	displacement =	0	;

			/// <summary>
			/// The default pen width	to be used for drawing the	border around	the slices
			/// (<see cref="ZedGraph.Border.PenWidth"/>	property).	 Units	are	points.
			/// </summary>
			public	static float BorderWidth = 1.0F;
			/// <summary>
			/// The default fill mode for slices (<see	cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public	static FillType FillType = FillType.Brush ;
			/// <summary>
			/// The default border	mode	for	slices	(<see cref="ZedGraph.Border.IsVisible"/> property).
			/// true to	display frames	around slices,	false otherwise
			/// </summary>
			public	static bool IsBorderVisible	= true;
			/// <summary>
			/// The default color	for	drawing	frames around	slices
			/// (<see cref="ZedGraph.Border.Color"/> property).
			/// </summary>
			public	static Color	BorderColor = Color.Black;
			/// <summary>
			/// The default color	for	filling in	the slices
			/// (<see cref="ZedGraph.Fill.Color"/>	property).
			/// </summary>
			public	static Color	FillColor = Color.Red;
			/// <summary>
			/// The default custom brush for filling in the slices.
			/// (<see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public	static Brush FillBrush	=	null;	
		
			/// <summary>
			///Default value for controlling <see cref="PieSlice"/> display.
			/// </summary>
			public static bool isVisible = true ;
		}
	#endregion	 Defaults

	#region Properties

		/// <summary>
		///	Gets or sets the boolean which determines whether or not the <see cref="PieSlice"/> 
		///	is displayed.  If not displayed, <see cref="GraphPane.Legend"/> entry is also
		///	not displayed.
		/// </summary>
		public bool IsVisible
		{
			get { return (this.isVisible); }
			set { this.isVisible = value; }
		}

		/// <summary>
		///Internal property to get or set the arc length (in degrees) of this <see cref="PieSlice"/>.
		/// </summary>
		internal float SweepAngle
		{
			get { return (this.sweepAngle); }
			set { this.sweepAngle = value; }
		}

		/// <summary>
		///Internal property to get or set the label of this <see cref="PieSlice"/>.
		/// </summary>
		public	string Label
		{
			get { return (this.label); }
			set { this.label =	value; }
		}
		
		/// <summary>
		///Gets or sets the starting angle (in degrees) of this <see cref="PieSlice"/>.
		/// </summary>
		internal float StartAngle
		{
			get { return (this.startAngle); }
			set { this.startAngle = value; }
		}

		/// <summary>
		///Internal property to get or set the angle (in degrees) of the radius along which 
		///this <see cref="PieSlice"/> will be displaced.
		/// </summary>
		internal float MidAngle
		{
			get { return (this.midAngle); }
			set { this.midAngle = value; }
		}

		/// <summary>
			///Gets or sets the <see cref="Border"/> object so as to be able to modify
			///its properties.
			/// </summary>
		public	Border Border
		{
			get { return (this.border); }
			set { this.border =	value; }
		}
				
			/// <summary>
			///   Internal property for getting and setting the value of this <see cref="PieSlice"/>.  
			///   Minimum value is 0. 
			/// </summary>
		internal	double Value
		{
			get { return (this.value); }
			set { this.value	=	value > 0 ? value : 0 ; }
		}
				
			/// <summary>
		///   Internal property for getting and setting the parent of this <see cref="PieSlice"/> 
		/// </summary>
		internal	PieItem	PieItem
		{
			get { return this.pieItem; }
			set { this.pieItem = value; }
		}

			/// <summary>
			///Gets or sets the a value which determines the amount, if any, of this <see cref="PieSlice"/>  
			///displacement.
			/// </summary>
		public	double Displacement
		{
			get { return (this.displacement); }
			set { this.displacement = value > 1 ? 1 : value ; }
		}


			/// <summary>
		///Gets or sets the <see cref="Fill"/> object so as to be able to modify
		///its properties.
		/// </summary>
		public	Fill	Fill
		{
			get { return (this.fill); }
			set { this.fill = value; }
		}

			/// <summary>
			/// 
			/// </summary>
		internal	int SliceIndex
		{
			get { return (this.sliceIndex); }
			set { this.sliceIndex = value; }
		}
	#endregion	Properties

	#region Constructors

			/// <summary>
			/// Default constructor that	sets	the	 <see	cref="Color"/>	as specified,	
			/// and the	remaining <see	cref="PieSlice"/>	common properties to their default
			/// values	as defined in	the <see cref="Default"/> class.
			/// The specified	color is only applied to	the <see	cref="ZedGraph.Fill.Color"/>.
			/// </summary>
			/// <param name="color">A <see	cref="Color"/>	value indicating
			/// the	<see cref="ZedGraph.Fill.Color"/>of the <see cref="PieSlice"/>.
			/// </param>
			public	PieSlice	(	Color color)
			{
				this.border =	new Border( true, Default.BorderColor,	Default.BorderWidth );
				this.fill = new	Fill( color.IsEmpty	? Default.FillColor :	color,Default.FillBrush, FillType.Solid );
			}		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pieItem">Parent <see cref="PieItem"/>  for this <see cref="PieSlice"/></param>
		/// <param name="index">SliceIndex for this <see cref="PieSlice"/></param>
		/// <param name="color">Fill.Color for this <see cref="PieSlice"/></param>
		/// <remarks>Other fields are defaulted or derived.</remarks>
			public	PieSlice(	PieItem	pieItem, int	index,	Color color	) : this	(color )
			{
				this.sliceIndex	=	index ;
				this.pieItem = pieItem ;
				this.value	=	Default.value ;
				this.displacement	= Default.displacement ;
				this.label =	sliceIndex.ToString() ;
				this.isVisible = Default.isVisible ;
			}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pieItem">Parent <see cref="PieItem"/>  for this <see cref="PieSlice"/></param>
		/// <param name="index">SliceIndex for this <see cref="PieSlice"/></param>
		/// <param name="value">value for this <see cref="PieSlice"/></param>
		/// <param name="color">Fill.Color for this <see cref="PieSlice"/></param>
		/// <remarks>Other fields are defaulted or derived</remarks>	
		public	PieSlice(	PieItem	pieItem, int	index,	double value, Color	color )	 : this (color )
			{
				this.sliceIndex	=	index ;
				this.pieItem = pieItem ;
				this.value	=	value ;
				this.Displacement	= Default.displacement ;
				this.label =	sliceIndex.ToString() ;
				this.isVisible = Default.isVisible ;
			}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pieItem">Parent <see cref="PieItem"/>  for this <see cref="PieSlice"/></param>
		/// <param name="index">SliceIndex for this <see cref="PieSlice"/></param>
		/// <param name="value">value for this <see cref="PieSlice"/></param>
		/// <param name="color">Fill.Color for this <see cref="PieSlice"/></param>
		/// <param name="displacement">Displacement for this <see cref="PieSlice"/></param>
		/// <param name="label">Text label for this <see cref="PieSlice"/></param>
		public	PieSlice	(	PieItem	pieItem, int	index,	double value, Color	color,  double	displacement,	string label	)	:
																	this (	color )
			{
				this.pieItem = pieItem ;
				this.sliceIndex	=	index ;
				this.value	=	value ;
				this.Displacement	= displacement  ;
				this.label =	label ;
				this.isVisible = Default.isVisible ;
			}

			/// <summary>
			/// Copy constructor (don't want one, I think)
			/// </summary>
			/// <param name="slice"><see cref="PieSlice "/> to be copied.</param>
			public PieSlice ( PieSlice slice )	: this ( slice.Fill.Color )
			{
				this.value = slice.value ;
				this.Displacement = slice.displacement ;
				this.label = slice.label ;
				this.sliceIndex = slice.sliceIndex ;
				this.isVisible = slice.isVisible ;
				this.pieItem = slice.pieItem ;
				this.startAngle = slice.startAngle ;
				this.sweepAngle = slice.sweepAngle ;
				this.midAngle = slice.midAngle ;
			}
		#endregion

		/// <summary>
		/// Draw a legend key entry for this <see cref="PieSlice"/> at the specified location
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
		public	void DrawLegendKey( Graphics	g, GraphPane pane, RectangleF rect,
					double scaleFactor )
		{
			if ( !this.isVisible )
				return ;
			
			//	Fill	the bar
			if	( this.fill.IsVisible )
			{
				//	just avoid	height/width	being	less than 0.1	so GDI+ doesn't cry
				Brush brush = this.fill.MakeBrush( rect );
				g.FillRectangle( brush, rect );
				brush.Dispose();
			}

			//	Border the bar
			if	( !this.border.Color.IsEmpty	)
				this.border.Draw( g,	pane,	scaleFactor, rect );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="baseRect">Bounding rectangle for this <see cref="PieSlice"/></param>
		public void Draw( Graphics g, GraphPane pane, RectangleF baseRect, double scaleFactor  )
		{
			if ( !this.isVisible )
				return ;
			
			//SolidBrush brush = new SolidBrush(this.Fill.Color );
			Brush brush = this.Fill.MakeBrush( baseRect );

				if ( this.displacement == 0 )                                                     //this slice not exploded
				{
					g.FillPie( brush,baseRect.X,baseRect.Y,baseRect.Width,baseRect.Height, this.StartAngle, this.SweepAngle );

					if ( this.Border.IsVisible)
					{
						//Pen borderPen = new Pen(this.Border.Color, this.Border.PenWidth);
						Pen borderPen = this.Border.MakePen( pane, scaleFactor );
						g.DrawPie (borderPen, baseRect.X,baseRect.Y,baseRect.Width,baseRect.Height, this.StartAngle, this.SweepAngle );
						borderPen.Dispose () ;
						
					}
					DrawLabel ( g, baseRect ) ;
				}
				else									  //got an exploded slice
				{
					RectangleF explRect  =baseRect;
 
					CalcExplodedRect (ref explRect ) ;							  //calculate the exploded slice bounding rectangle

					g.FillPie( brush,explRect.X,explRect.Y,explRect.Width,explRect.Height, this.StartAngle, this.SweepAngle );

					if ( this.Border.IsVisible )
					{
						Pen borderPen = new Pen(this.Border.Color, this.Border.PenWidth);
						g.DrawPie (borderPen, explRect.X,explRect.Y,explRect.Width,explRect.Height, this.StartAngle, this.SweepAngle );
						borderPen.Dispose () ;

					}
					DrawLabel ( g, explRect ) ;
				}
			}

		public void DrawLabel ( Graphics g, RectangleF rect )
		{
					//label line will come off the explosion radius and then bend to the horizontal right or left, dependent on position, 
					//text will be at the end of horizontal segment...need to determine point where label line meets periphery
					//of slice....radial line will extend both sides of that point....intersection is along midAngle line at a distance
					//from the center equal to length of radius...
/*
			Pen pen = new Pen(Color.Aqua, 1 );

			PointF rectCenter = new PointF ( (rect.X + rect.Width / 2 ) , (rect.Y + rect.Height / 2 )) ;
			PointF intersectionPoint = new PointF ( (float)(rectCenter.X + ( rect.Width / 2 *  Math.Cos (  ( 90 + this.midAngle ) * Math.PI /180 ))),
														(float)(rect.Y + ( rect.Height / 2 * Math.Sin (  ( 90 + this.midAngle )  * Math.PI / 180 )) )) ;

			g.DrawLine ( pen, rectCenter.X, rectCenter.Y,  rect.X + rect.Width, rect.Y + rect.Height ) ;
//			g.DrawLine ( pen, rectCenter, intersectionPoint ) ;
*/
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseRect"></param>
		private void CalcExplodedRect ( ref RectangleF explRect)
		{
						//pie exploded out along the slice bi-sector - modify upper left to account for displacement
						//keep height and width same
			explRect.X += (float)(this.Displacement * explRect.Width / 2 * Math.Cos ( this.midAngle * Math.PI /180 )) ;
			explRect.Y += (float) (this.Displacement * explRect.Height / 2 * Math.Sin (this.midAngle * Math.PI /180 )) ; 
		}
	}
}
	

