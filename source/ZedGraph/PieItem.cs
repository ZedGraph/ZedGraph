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
	/// <see cref="PieItem"/>s.
	/// </summary>
	/// <author> Bob Kaye </author>
	/// <version> $Revision: 1.8 $ $Date: 2005-01-22 06:20:50 $ </version>
	[Serializable]
	public class PieItem : ZedGraph.CurveItem , ICloneable, ISerializable
	{
		
	#region Fields
/*
		/// <summary>
		/// Private field instance of the <see cref="PieItem"/> class indicating whether
		/// the instance is displayed in 2D or 3D.(see <see cref="PieItem.PieType"/>)
		/// </summary>
		private PieType pieType;
*/
		/// <summary>
		/// Percentage (expressed as #.##) of <see cref="PieItem"/>	diameter  to
		/// which this <see cref="PieItem"/> is to be displaced from the center.
		///   Displacement is done outward  along the radius
		/// bisecting the chord of this <see cref="PieItem"/>.  Maximum allowable value
		/// is 0.5.
		/// </summary>
		private double	displacement;

		/// <summary>
		/// Private field to hold the GraphicsPath of this <see cref="PieItem"/> to be
		/// used for 'hit testing'.
		/// </summary>
		private GraphicsPath slicePath;

		/// <summary>
		/// Private field which holds the angle (in degrees) at which the display of this <see cref="PieItem"/>
		/// object will begin.
		/// </summary>
		private float startAngle;

		/// <summary>
		///Private field which holds the length (in degrees) of the arc representing this <see cref="PieItem"/> 
		///object.
		/// </summary>
		private float sweepAngle;

		/// <summary>
		///Private field which represents the angle (in degrees) of the radius along which this <see cref="PieItem"/>
		///object will be displaced, if desired.
		/// </summary>
		private float midAngle;

		/// <summary>
		/// A <see cref="ZedGraph.TextItem"/> which will customize the label display of this
		/// <see cref="PieItem"/>
		/// </summary>
		private TextItem labelDetail;
	
		/// <summary>
		/// Private	field	that stores the	<see cref="ZedGraph.Fill"/> data for this
		/// <see	cref="PieItem"/>.	 Use	the public property <see	cref="Fill"/> to
		/// access this value.
		/// </summary>
		private Fill  fill;
		
		/// <summary>
		/// Private	field	that stores the	<see cref="Border"/> class that defines	the
		/// properties of the	border around	this <see cref="PieItem"/>. Use the public
		/// property	<see cref="Border"/> to access this value.
		/// </summary>
		private Border border;
		
		/// <summary>
		/// Private field that stores the absolute value of this <see cref="PieItem"/> instance.
		/// Value will be set to zero if submitted value is less than zero. 
		/// </summary>
		private double pieValue;

		/// <summary>
		/// An enum that specifies how each <see cref="CurveItem.Label"/> for this <see cref="PieItem"/> object 
		/// will be displayed.  Use the public property <see cref="LabelType"/> to access this data.  
		/// Use enum <see cref="ZedGraph.PieLabelType"/>.
		/// </summary>
		private PieLabelType labelType ;


		private static ColorSymbolRotator rotator = new ColorSymbolRotator () ;

	#endregion

	#region Defaults
		/// <summary>
		/// Specify the default property values for the <see cref="PieItem"/> class.
		/// </summary>
		public	struct Default
		{
			/// <summary>
			///Default <see cref="PieItem "/> displacement.
			/// </summary>
			public static double Displacement = 0;

			/// <summary>
			/// The default pen width	to be used for drawing the	border around	the PieItem
			/// (<see cref="ZedGraph.Border.PenWidth"/>	property).	 Units	are	points.
			/// </summary>
			public static float BorderWidth = 1.0F;
			/// <summary>
			/// The default fill mode for this PieItem (<see	cref="ZedGraph.Fill.Type"/> property).
			/// </summary>
			public static FillType FillType = FillType.Brush ;
			/// <summary>
			/// The default border	mode	for	PieItem	(<see cref="ZedGraph.Border.IsVisible"/> property).
			/// true to	display frame	around PieItem,	false otherwise
			/// </summary>
			public static bool IsBorderVisible = true;
			/// <summary>
			/// The default color	for	drawing	frames around	PieItem
			/// (<see cref="ZedGraph.Border.Color"/> property).
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color	for	filling in	the PieItem
			/// (<see cref="ZedGraph.Fill.Color"/>	property).
			/// </summary>
			public static Color FillColor = Color.Red;
			/// <summary>
			/// The default custom brush for filling in the PieItem.
			/// (<see cref="ZedGraph.Fill.Brush"/> property).
			/// </summary>
			public static Brush FillBrush = null;	
		
			/// <summary>
			///Default value for controlling <see cref="PieItem"/> display.
			/// </summary>
			public static bool isVisible = true ;

			/// <summary>
			/// Default value for <see cref="PieItem.LabelType"/>.
			/// </summary>
			public static PieLabelType LabelType = PieLabelType.Name;
		}
	#endregion Defaults

	#region PieItem Properties
		/// <summary>
		///Gets or sets the a value which determines the amount, if any, of this <see cref="PieItem"/>  
		///displacement.
		/// </summary>
		public	double Displacement
		{
			get { return (this.displacement); }
			set { this.displacement = value > 1 ? .5 : value ; }
		}

		/// <summary>
		/// Gets or sets a path representing this <see cref="PieItem"/>
		/// </summary>
		public GraphicsPath SlicePath
		{
			get { return (this.slicePath); }
			set { this.slicePath = value; } 
		}

		/// <summary>
		/// Private field holding a <see cref="TextItem"/> to be used
		/// for displaying this <see cref="PieItem"/>'s label.
		/// </summary>
		public TextItem LabelDetail
		{
			get { return (this.labelDetail); }
			set { this.labelDetail = value; } 
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
		///Internal property to get or set the arc length (in degrees) of this <see cref="PieItem"/>.
		/// </summary>
		internal float SweepAngle
		{
			get { return (this.sweepAngle); }
			set { this.sweepAngle = value; }
		}

		/// <summary>
		///Gets or sets the starting angle (in degrees) of this <see cref="PieItem"/>.
		/// </summary>
		internal float StartAngle
		{
			get { return (this.startAngle); }
			set { this.startAngle = value; }
		}

		/// <summary>
		///Internal property to get or set the angle (in degrees) of the radius along which 
		///this <see cref="PieItem"/> will be displaced.
		/// </summary>
		internal float MidAngle
		{
			get { return (this.midAngle); }
			set { this.midAngle = value; }
		}

		/// <summary>
		///   Internal property for getting and setting the value of this <see cref="PieItem"/>.  
		///   Minimum value is 0. 
		/// </summary>
		public	double Value
		{
			get { return (this.pieValue); }
			set { this.pieValue	= value > 0 ? value : 0 ; }
		}
				
		/// <summary>
		/// Gets or sets the <see cref="PieLabelType"/> to be used in displaying 
		/// <see cref="PieItem"/> labels.
		/// </summary>
		public	PieLabelType LabelType
		{
			get { return (this.labelType); }
			set { this.labelType = value; }
		}
/*
		/// <summary>
		/// Getsor sets enum <see cref="PieType"/> to be used	for drawing this <see cref="PieItem"/>.
		/// </summary>
		public PieType PieType
		{
			get { return (this.pieType); }
			set { this.pieType = value; }
		}
 */
 
	#endregion

	#region Constructors
		/// <summary>
		/// Add a <see cref="PieItem"/> to an existing <see cref="PieItem"/>
		/// </summary>
		/// <param name="pieValue">The value associated with this <see cref="PieItem"/>item.</param>
		/// <param name="color">The display color for this <see cref="PieItem"/>item.</param>
		/// <param name="displacement">The amount this <see cref="PieItem"/>  will be 
		/// displaced from the center point.</param>
		/// <param name="label">Text label for this <see cref="PieItem"/></param>
		public PieItem ( double pieValue, Color color,  double displacement, string label ) : base( label )
		{
			this.pieValue = pieValue ;
			this.fill = new Fill( color.IsEmpty ? rotator.NextColor : color ) ;
			this.border = new Border(Default.BorderColor, Default.BorderWidth ) ;
			this.displacement = displacement ;
			this.labelDetail = new TextItem();
			this.labelType = Default.LabelType;
		}

		/// <summary>
		/// Add a <see cref="PieItem"/> to an existing <see cref="PieItem"/>
		/// </summary>
		/// <param name="pieValue">The value associated with this <see cref="PieItem"/>item.</param>
		/// <param name="label">Text label for this <see cref="PieItem"/></param>
		public PieItem ( double pieValue, string label ) : base( label )
		{
			this.pieValue = pieValue ;
			this.fill = new Fill( rotator.NextColor  ) ;
			this.border = new Border(Default.BorderColor, Default.BorderWidth ) ;
			this.displacement =  Default.Displacement ;
			this.labelDetail = new TextItem() ;
			this.labelType = Default.LabelType;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="PieItem"/> object from which to copy</param>
		public PieItem( PieItem rhs ) : base( rhs )
		{
			this.pieValue = rhs.pieValue;
			this.fill = (Fill) rhs.fill.Clone();
			this.Border = (Border) rhs.border.Clone();
			this.displacement = rhs.displacement;
			this.labelDetail = (TextItem) rhs.labelDetail.Clone();
			this.labelType = rhs.labelType;
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
		protected PieItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			displacement = info.GetDouble( "displacement" );
			labelDetail = (TextItem) info.GetValue( "labelDetail", typeof(TextItem) );
			fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			border = (Border) info.GetValue( "border", typeof(Border) );
			pieValue = info.GetDouble( "pieValue" );
			labelType = (PieLabelType) info.GetValue( "labelType", typeof(PieLabelType) );
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
			info.AddValue( "displacement", displacement );
			info.AddValue( "labelDetail", labelDetail );
			info.AddValue( "fill", fill );
			info.AddValue( "border", border );
			info.AddValue( "labelType", labelType );
		}
	
	#endregion
		
	#region Methods
		/// <summary>
		/// Do all rendering associated with this <see cref="PieItem"/> item to the specified
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
		/// <param name="pos">Not used for rendering Pies</param>param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>				
		override public void Draw( Graphics g, GraphPane pane, int pos, float scaleFactor  )
		{

			RectangleF nonExplRect ;
			double maxDisplacement = 0 ;	
			this.slicePath = new GraphicsPath() ;

			if ( !this.isVisible )
				return ;
			
			SmoothingMode sMode = g.SmoothingMode ;
			g.SmoothingMode = SmoothingMode.AntiAlias ;	
			
			if ( pane.CurveList.IsPieOnly )						 //calc pierect here
				nonExplRect = pane.PieRect ;
			else
				nonExplRect = pane.AxisRect ;					  //if isAxisRectAuto = false
			
			CalculatePieChartParams( pane, ref maxDisplacement ) ;

			if ( maxDisplacement != 0 )			 //need new rectangle if any slice exploded	***figure out how to get maxdispl
				CalcNewBaseRect ( maxDisplacement, ref nonExplRect ) ;
			
			Brush brush = this.fill.MakeBrush( nonExplRect );

			RectangleF tRect = nonExplRect;

			if ( displacement != 0 )
				CalcExplodedRect( ref tRect );	//calculate the bounding rectangle for exploded pie

			g.FillPie( brush, tRect.X, tRect.Y, tRect.Width, tRect.Height, this.StartAngle, this.SweepAngle );

			//add GraphicsPath for hit testing
			this.slicePath.AddPie( tRect.X, tRect.Y, tRect.Width, tRect.Height, 
									this.StartAngle, this.SweepAngle);

			if ( this.Border.IsVisible)
			{
				Pen borderPen = this.border.MakePen( pane.IsPenWidthScaled, scaleFactor );
				g.DrawPie( borderPen, tRect.X, tRect.Y, tRect.Width, tRect.Height, 
							this.StartAngle, this.SweepAngle );
				borderPen.Dispose();
			}

			if ( this.labelType != PieLabelType.None )
				DrawLabel( g, pane, tRect, scaleFactor  ) ;

			brush.Dispose () ;
			g.SmoothingMode = sMode ;	
		}

		/// <summary>
		/// Calculate the <see cref="RectangleF"/> that will be used to define the bounding rectangle of
		/// the Pie.
		/// </summary>
		/// <remarks>This rectangle always lies inside of the <see cref="GraphPane.AxisRect"/>, and it is
		/// normally a square so that the pie itself is not oval-shaped.</remarks>
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
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>				
		/// <param name="axisRect">The <see cref="RectangleF"/> (normally the <see cref="GraphPane.AxisRect"/>)
		/// that bounds this pie.</param>
		/// <returns></returns>
		public static RectangleF CalcPieRect( Graphics g, GraphPane pane, float scaleFactor, RectangleF axisRect )
		{
			//want to draw the largest pie possible within axisRect
			//but want to leave  10% slack around the pie so labels will not overrun clip area
			//largest pie is limited by the smaller of axisRect.height or axisRect.width...
			//this rect (nonExplRect)has to be re-positioned so that it's in the center of axisRect.
			RectangleF nonExplRect = axisRect;

			if ( pane.CurveList.IsPieOnly )
			{
				if ( nonExplRect.Width < nonExplRect.Height )
				{
					//create slack rect
					nonExplRect.Inflate (  - (float)0.1F * nonExplRect.Height, - (float)0.1F * nonExplRect.Width);
					//get the difference between dimensions
					float delta =  (nonExplRect.Height - nonExplRect.Width ) / 2 ;
					//make a square	so we end up with circular pie
					nonExplRect.Height = nonExplRect.Width ;
					//keep the center point  the same
					nonExplRect.Y += delta ;
				}
				else
				{
					nonExplRect.Inflate (  - (float)0.1F * nonExplRect.Height, - (float)0.1F * nonExplRect.Width);
					float delta =  (nonExplRect.Width - nonExplRect.Height ) / 2 ;
					nonExplRect.Width = nonExplRect.Height ;
					nonExplRect.X += delta ;
				}
			}

			return nonExplRect;
		}

		/// <summary>
		/// Recalculate the bounding rectangle when a piee slice is displaced.
		/// </summary>
		/// <param name="explRect">rectangle to be used for drawing exploded pie</param>
		private void CalcExplodedRect ( ref RectangleF explRect)
		{
			//pie exploded out along the slice bisector - modify upper left of bounding rect to account for displacement
			//keep height and width same
			explRect.X += (float)(this.Displacement * explRect.Width / 2 * Math.Cos ( this.midAngle * Math.PI /180 )) ;
			explRect.Y += (float) (this.Displacement * explRect.Height / 2 * Math.Sin (this.midAngle * Math.PI /180 )) ; 
		}

		/// <summary>
		/// Calculate the values needed to properly display this <see cref="PieItem"/>.
		/// </summary>
		/// <param name="pane">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="maxDisplacement">maximum slice displacement</param>
		private void CalculatePieChartParams (GraphPane pane, ref double maxDisplacement )
		{
			//loop thru slices and get total value and maxDisplacement
			double pieTotalValue = 0 ;
			foreach ( PieItem curve in pane.CurveList )
				if ( curve.IsPie )
				{
					pieTotalValue += curve.pieValue ;
					if ( curve.Displacement > maxDisplacement)
						maxDisplacement = curve.Displacement ;
				}						 
			
			double nextStartAngle = 0 ;
			//now loop thru and calculate the various angle values
			foreach ( PieItem curve in pane.CurveList )
			{
				curve.StartAngle = (float)nextStartAngle ; 
				curve.SweepAngle = (float)(360 * curve.Value / pieTotalValue) ;
				curve.MidAngle = curve.StartAngle + curve.SweepAngle / 2 ;
				nextStartAngle = curve.startAngle + curve.sweepAngle ;
			}
		}

		/// <summary>
		///Render the label for this <see cref="PieItem"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">Bounding rectangle for this <see cref="PieItem"/>.</param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>				
		public void DrawLabel ( Graphics g, GraphPane pane, RectangleF rect, float scaleFactor  )
		{
			//label line will come off the explosion radius and then pivot to the horizontal right or left, dependent on position.. 
			//text will be at the end of horizontal segment...
			Pen labelPen = this.Border.MakePen (pane.IsPenWidthScaled, scaleFactor ) ;

			//get the point where the explosion radius intersects the pie arc
			PointF rectCenter = new PointF ( (rect.X + rect.Width / 2 ) , (rect.Y + rect.Height / 2 )) ;
			PointF intersectionPoint = new PointF ( (float)(rectCenter.X + ( rect.Width / 2 *  Math.Cos (  (this.midAngle ) * Math.PI /180 ))),
				(float)(rectCenter.Y + ( rect.Height / 2 * Math.Sin (  (  this.midAngle )  * Math.PI / 180 )) )) ;
			
			//draw line from intersection point to pivot point - line to be 20 pixels long
			PointF pivotPoint = new PointF ((float) (intersectionPoint.X + 20*  Math.Cos (  (this.midAngle ) * Math.PI /180 )), 
				(float)(intersectionPoint.Y + 20 *Math.Sin (  (  this.midAngle )  * Math.PI / 180 )) )  ;
			g.DrawLine ( labelPen, intersectionPoint, pivotPoint ) ;

			//draw 20 pixel horizontal	 line to move label away from pie...
			PointF endPoint ;
															 //does line go to left or right....label alignment is to the opposite
			if ( pivotPoint.X >= rectCenter.X)							 //goes to right
			{
				endPoint = new PointF (  pivotPoint.X + 20, pivotPoint.Y)	;
				this.labelDetail.Location.AlignH = AlignH.Left ;
			}
			else
			{
				endPoint = new PointF (  pivotPoint.X - 20, pivotPoint.Y)	;
				this.labelDetail.Location.AlignH = AlignH.Right ;
			}
			g.DrawLine ( labelPen, pivotPoint, endPoint )	;

			//build the label string to be displayed
			String labelStr = " " ;
			BuildLabelString ( ref labelStr ) ;
										//configure the label (TextItem)
			this.labelDetail.Location.AlignV = AlignV.Center ;
			this.labelDetail.Location.CoordinateFrame = CoordType.PaneFraction ;
			this.labelDetail.Location.X =	endPoint.X / pane.PaneRect.Width ;
			this.labelDetail.Location.Y =	endPoint.Y / pane.PaneRect.Height ;
			this.labelDetail.Text = labelStr  ;
			this.labelDetail.FontSpec.Size = 8 ;
			this.labelDetail.Draw ( g, pane, scaleFactor ) ;
		}

		/// <summary>
		/// Build the string that will be displayed as the slice label as determined by 
		/// <see cref="PieItem.labelType"/>.
		/// </summary>
		/// <param name="labelStr">reference to the string to be displayed</param>
		private void BuildLabelString ( ref string labelStr )
		{
			switch( this.labelType ) 
			{		
				case PieLabelType.Value :
					labelStr = this.pieValue.ToString ("#.###") ;
					break;
				case PieLabelType.Percent :
					double pieValue  = this.sweepAngle / 360 ;	
					labelStr = pieValue.ToString ("#.000%") ;
					break;
				case PieLabelType.Name_Value :
					labelStr = this.label + ": " + this.pieValue.ToString ("#.###") ;
					break;
				case PieLabelType.Name_Percent :
					pieValue  = this.sweepAngle / 360 ;	
					labelStr = this.label + ": " + pieValue.ToString ("#.###%") ;
					break;
				case PieLabelType.Name :
					labelStr = this.label ; 
					break ;
				case PieLabelType.None :
				default:
					break ;
			}
		}
		/// <summary>
		/// A method which calculates the size of the bounding rectangle for the non-displaced 
		/// <see cref="PieItem"/>'s in this <see cref="PieItem"/>.  This method is called after it is found
		/// that at least one slice is displaced.
		/// </summary>
		/// <param name="maxDisplacement"></param>
		/// <param name="baseRect"></param>
		private void CalcNewBaseRect ( double maxDisplacement, ref RectangleF baseRect )
		{
			//displacement expressed in terms of % of pie radius	...do not want exploded slice to 
			//go beyond nonExplRect, but want to maintain the same center point...therefore, got to 
			//reduce the diameter of the nonexploded pie by the alue of the displacement

			float xDispl =	(float)((maxDisplacement * baseRect.Width/2)) ; 
			float yDispl	 =  (float)((maxDisplacement * baseRect.Height/2) ); 

			baseRect.Inflate ( -(float)(.90*(xDispl / 2)), -(float)(.90*(xDispl / 2 ) )  );
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
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void DrawLegendKey( Graphics g, GraphPane pane, RectangleF rect, float scaleFactor )
		{
			if ( !this.isVisible )
				return ;
			
			//	Fill	the slice
			if	( this.fill.IsVisible )
			{
				//	just avoid	height/width	being	less than 0.1	so GDI+ doesn't cry
				Brush brush = this.fill.MakeBrush( rect );
				g.FillRectangle( brush, rect );
				brush.Dispose();
			}

			//	Border the bar
			if	( !this.border.Color.IsEmpty )
				this.border.Draw( g, pane.IsPenWidthScaled, scaleFactor, rect );
		}

	#endregion  
	
	}
}
