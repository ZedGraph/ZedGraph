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

#region Using directives

using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="GraphPane"/> objects
	/// organized together in some form.
	/// </summary>
	/// 
	/// <author>John Champion</author>
	/// <version> $Revision: 3.4 $ $Date: 2005-02-02 04:52:05 $ </version>
	[Serializable]
	public class MasterPane : PaneBase, ICloneable, ISerializable, IDeserializationCallback
	{

	#region Fields

		/// <summary>
		/// Private field that holds a collection of <see cref="GraphPane"/> objects for inclusion
		/// in this <see cref="MasterPane"/>.  Use the public property <see cref="PaneList"/>
		/// to access this collection.
		/// </summary>
		private PaneList paneList;

		/// <summary>
		/// private field that saves the paneLayout format specified when
		/// <see cref="AutoPaneLayout(Graphics,PaneLayout)"/> was called. This value will
		/// default to <see cref="Default.PaneLayout"/> if <see cref="AutoPaneLayout(Graphics,PaneLayout)"/>
		/// was never called.
		/// </summary>
		private PaneLayout paneLayout;

		/// <summary>
		/// private fields that store the number of rows and columns that were specified to the
		/// <see cref="AutoPaneLayout(Graphics,int,int)"/> method.  These values will both be
		/// zero if <see cref="AutoPaneLayout(Graphics,int,int)"/> was never called.
		/// </summary>
		private int rows;
		private int columns;

		/// <summary>
		/// Private field that stores the boolean value that determines whether <see cref="countList"/>
		/// is specifying rows or columns.
		/// </summary>
		private bool isColumnSpecified;
		/// <summary>
		/// private field that stores the row/column item count that was specified to the
		/// <see cref="AutoPaneLayout(Graphics,bool,int[])"/> method.  This values will be
		/// null if <see cref="AutoPaneLayout(Graphics,bool,int[])"/> was never called.
		/// </summary>
		private int[] countList;

	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="MasterPane"/> class.
		/// </summary>
		public new struct Default
		{
			/// <summary>
			/// The default pane layout for <see cref="MasterPane.AutoPaneLayout(Graphics,PaneLayout)"/> method calls.
			/// </summary>
			public static PaneLayout PaneLayout = PaneLayout.SquareColPreferred;

			/// <summary>
			/// The default value for the <see cref="Default.InnerPaneGap"/> property.
			/// This is the size of the margin inbetween adjacent <see cref="GraphPane"/>
			/// objects, in units of points (1/72 inch).
			/// </summary>
			public static float InnerPaneGap = 20;
			
		}
	#endregion

	#region Properties

		/// <summary>
		/// Gets or sets the <see cref="PaneList"/> collection instance that holds the list of
		/// <see cref="GraphPane"/> objects that are included in this <see cref="MasterPane"/>.
		/// </summary>
		/// <seealso cref="Add"/>
		/// <seealso cref="MasterPane.this[int]"/>
		public PaneList PaneList
		{
			get { return paneList; }
			set { paneList = value; }
		}

	#endregion
	
	#region Constructors

		/// <summary>
		/// Default constructor for the class.  Leaves the <see cref="PaneBase.PaneRect"/> empty.
		/// </summary>
		public MasterPane() : this( "", new RectangleF( 0, 0, 0, 0 ) )
		{
		}
		
		/// <summary>
		/// Default constructor for the class.  Specifies the <see cref="PaneBase.Title"/> of
		/// the <see cref="MasterPane"/>, and the size of the <see cref="PaneBase.PaneRect"/>.
		/// </summary>
		public MasterPane( string title, RectangleF paneRect ) : base( title, paneRect )
		{
			this.paneLayout = Default.PaneLayout;
			this.rows = -1;
			this.columns = -1;
			this.isColumnSpecified = false;
			this.countList = null;

			this.paneList = new PaneList();
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="MasterPane"/> object from which to copy</param>
		public MasterPane( MasterPane rhs ) : base( rhs )
		{
			this.paneLayout = rhs.paneLayout;
			this.rows = rhs.rows;
			this.columns = rhs.rows;
			this.isColumnSpecified = rhs.isColumnSpecified;
			this.countList = rhs.countList;

			this.paneList = (PaneList) rhs.paneList.Clone();
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="MasterPane"/></returns>
		public override object Clone()
		{ 
			return new MasterPane( this ); 
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
		protected MasterPane( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			this.paneList = (PaneList) info.GetValue( "paneList", typeof(PaneList) );
			this.paneLayout = (PaneLayout) info.GetValue( "paneLayout", typeof(PaneLayout) );

			this.rows = info.GetInt32( "rows" );
			this.columns = info.GetInt32( "columns" );
			this.isColumnSpecified = info.GetBoolean( "isColumnSpecified" );
			this.countList = (int[]) info.GetValue( "countList", typeof(int[]) );

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

			info.AddValue( "paneList", paneList );
			info.AddValue( "paneLayout", paneLayout );

			info.AddValue( "rows", rows );
			info.AddValue( "columns", columns );
			info.AddValue( "isColumnSpecified", isColumnSpecified );
			info.AddValue( "countList", countList );

		}

		/// <summary>
		/// Respond to the callback when the MasterPane objects are fully initialized.
		/// </summary>
		/// <param name="sender"></param>
		public void OnDeserialization(object sender)
		{
			Bitmap bitmap = new Bitmap( 10, 10 );
			Graphics g = Graphics.FromImage( bitmap );
			ReSize( g, this.paneRect );
		}
	#endregion

	#region List Methods
		/// <summary>
		/// Indexer to access the specified <see cref="GraphPane"/> object from <see cref="PaneList"/>
		/// by its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="GraphPane"/> object to be accessed.</param>
		/// <value>A <see cref="GraphPane"/> object reference.</value>
		public GraphPane this[ int index ]  
		{
			get { return( (GraphPane) paneList[index] ); }
			set { paneList[index] = value; }
		}

		/// <summary>
		/// Indexer to access the specified <see cref="GraphPane"/> object from <see cref="PaneList"/>
		/// by its <see cref="PaneBase.Title"/> string.
		/// </summary>
		/// <param name="title">The string title of the
		/// <see cref="GraphPane"/> object to be accessed.</param>
		/// <value>A <see cref="GraphPane"/> object reference.</value>
		public GraphPane this[ string title ]  
		{
			get { return paneList[title]; }
		}

		/// <summary>
		/// Add a <see cref="GraphPane"/> object to the <see cref="PaneList"/> collection at the end of the list.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public void Add( GraphPane pane )
		{
			paneList.Add( pane );
		}

		/// <summary>
		/// Call <see cref="GraphPane.AxisChange"/> for all <see cref="GraphPane"/> objects in the
		/// <see cref="PaneList"/> list.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void AxisChange( Graphics g )
		{
			foreach ( GraphPane pane in paneList )
				pane.AxisChange( g );
		}

		/// <summary>
		/// Change the size of the <see cref="PaneBase.PaneRect"/>, and also handle resizing the contents
		/// by calling <see cref="AutoPaneLayout(Graphics,PaneLayout)"/>.
		/// </summary>
		/// <remarks>This method will use the same pane layout
		/// that was specified by the most recent call to <see cref="AutoPaneLayout(Graphics,PaneLayout)"/>.  If
		/// <see cref="AutoPaneLayout(Graphics,PaneLayout)"/> has not previously been called, it will default to
		/// <see cref="Default.PaneLayout"/>.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="paneRect"></param>
		public override void ReSize( Graphics g, RectangleF paneRect )
		{
			this.paneRect = paneRect;

			if ( this.countList != null )
				AutoPaneLayout( g, this.isColumnSpecified, this.countList );
			else if ( this.rows >= 1 && this.columns >= 1 )
				AutoPaneLayout( g, this.rows, this.columns );
			else
				AutoPaneLayout( g, this.paneLayout );
		}

		/// <summary>
		/// Render all the <see cref="GraphPane"/> objects in the <see cref="PaneList"/> to the
		/// specified graphics device.
		/// </summary>
		/// <remarks>This method should be part of the Paint() update process.  Calling this routine
		/// will redraw all
		/// features of all the <see cref="GraphPane"/> items.  No preparation is required other than
		/// instantiated <see cref="GraphPane"/> objects that have been added to the list with the
		/// <see cref="Add"/> method.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public override void Draw( Graphics g )
		{			
			// Draw the pane border & background fill, the title, and the GraphItem objects that lie at
			// ZOrder.G_BehindAll
			base.Draw( g );

			float scaleFactor = CalcScaleFactor();

			// Clip everything to the paneRect
			g.SetClip( this.paneRect );

			// For the MasterPane, All GraphItems go behind the GraphPanes, except those that
			// are explicity declared as ZOrder.A_InFront
			graphItemList.Draw( g, this, scaleFactor, ZOrder.F_BehindAxisFill );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.E_BehindAxis );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.D_BehindCurves );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.C_BehindAxisBorder );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.B_BehindLegend );

			// Reset the clipping
			g.ResetClip();

			foreach ( GraphPane pane in paneList )
				pane.Draw( g );

			// Clip everything to the paneRect
			g.SetClip( this.paneRect );

			graphItemList.Draw( g, this, scaleFactor, ZOrder.A_InFront );
			
			// Reset the clipping
			g.ResetClip();
		}
		
		/// <summary>
		/// Calculate the innerRect rectangle based on the <see cref="PaneBase.PaneRect"/>.
		/// </summary>
		/// <remarks>The innerRect is the actual area available for <see cref="GraphPane"/>
		/// items after taking out space for the margins and the title.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.Default.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>The calculated axis rect, in pixel coordinates.</returns>
		private RectangleF CalcInnerRect( Graphics g, float scaleFactor )
		{
			// get scaled values for the paneGap and character height
			//float scaledOuterGap = (float) ( Default.OuterPaneGap * scaleFactor );
			float charHeight = this.FontSpec.GetHeight( scaleFactor );
				
			// Axis rect starts out at the full pane rect.  It gets reduced to make room for the legend,
			// scales, titles, etc.
			RectangleF innerRect = new RectangleF(
							this.paneRect.Left + this.marginLeft * (float) scaleFactor,
							this.paneRect.Top + this.marginTop * (float) scaleFactor,
							this.paneRect.Width - (float) scaleFactor * ( this.marginLeft + this.marginRight ),
							this.paneRect.Height - (float) scaleFactor * ( this.marginTop + this.marginBottom ) );

			//innerRect.Inflate( -scaledOuterGap, -scaledOuterGap );

			// Leave room for the title
			if ( this.isShowTitle )
			{
				SizeF titleSize = this.fontSpec.BoundingBox( g, this.title, scaleFactor );
				// Leave room for the title height, plus a line spacing of charHeight/2
				innerRect.Y += titleSize.Height + charHeight / 2.0F;
				innerRect.Height -= titleSize.Height + charHeight / 2.0F;
			}
			
			return innerRect;
		}

		/// <summary>
		/// Automatically set all of the <see cref="GraphPane"/> <see cref="PaneBase.PaneRect"/>'s in
		/// the list to a reasonable configuration.
		/// </summary>
		/// <remarks>This method uses a <see cref="PaneLayout"/> enumeration to describe the type of layout
		/// to be used.  An explicit (row,column) overload is also available.</remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="paneLayout">A <see cref="PaneLayout"/> enumeration that describes how
		/// the panes should be laid out within the <see cref="PaneBase.PaneRect"/>.</param>
		public void AutoPaneLayout( Graphics g, PaneLayout paneLayout )
		{
			int count = this.PaneList.Count;
			if ( count == 0 )
				return;

			int		rows,
					cols,
					root = (int) (Math.Sqrt( (double) count ) + 0.9999999);
			float[] widthList = new float[5];
			
			switch ( paneLayout )
			{
				case PaneLayout.ForceSquare:
					rows = root;
					cols = root;
					AutoPaneLayout( g, rows, cols );
					break;
				case PaneLayout.SingleColumn:
					rows = count;
					cols = 1;
					AutoPaneLayout( g, rows, cols );
					break;
				case PaneLayout.SingleRow:
					rows = 1;
					cols = count;
					AutoPaneLayout( g, rows, cols );
					break;
				default:
				case PaneLayout.SquareColPreferred:
					rows = root;
					cols = root;
					if ( count <= root * (root - 1) )
						rows--;
					AutoPaneLayout( g, rows, cols );
					break;
				case PaneLayout.SquareRowPreferred:
					rows = root;
					cols = root;
					if ( count <= root * (root - 1) )
						cols--;
					AutoPaneLayout( g, rows, cols );
					break;
				case PaneLayout.ExplicitCol12:
					AutoPaneLayout( g, true, new int[2]{ 1, 2 } );
					break;
				case PaneLayout.ExplicitCol21:
					AutoPaneLayout( g, true, new int[2]{ 2, 1 } );
					break;
				case PaneLayout.ExplicitCol23:
					AutoPaneLayout( g, true, new int[2]{ 2, 3 } );
					break;
				case PaneLayout.ExplicitCol32:
					AutoPaneLayout( g, true, new int[2]{ 3, 2 } );
					break;
				case PaneLayout.ExplicitRow12:
					AutoPaneLayout( g, false, new int[2]{ 1, 2 } );
					break;
				case PaneLayout.ExplicitRow21:
					AutoPaneLayout( g, false, new int[2]{ 2, 1 } );
					break;
				case PaneLayout.ExplicitRow23:
					AutoPaneLayout( g, false, new int[2]{ 2, 3 } );
					break;
				case PaneLayout.ExplicitRow32:
					AutoPaneLayout( g, false, new int[2]{ 3, 2 } );
					break;
			}

			// save the layout settings for future reference
			this.paneLayout = paneLayout;
			this.countList = null;
			this.rows = -1;
			this.columns = -1;
		}

		/// <summary>
		/// Automatically set all of the <see cref="GraphPane"/> <see cref="PaneBase.PaneRect"/>'s in
		/// the list to the specified configuration.
		/// </summary>
		/// <remarks>This method specifies the number of panes in each row or column, allowing for
		/// irregular layouts.</remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="isColumnSpecified">Specifies whether the number of columns in each row, or
		/// the number of rows in each column will be specified.  A value of true indicates the
		/// number of columns in each row are specified in <see paramref="countList"/>.</param>
		/// <param name="countList">An integer array specifying either the number of columns in
		/// each row or the number of rows in each column, depending on the value of
		/// <see paramref="isColumnSpecified"/>.</param>
		public void AutoPaneLayout( Graphics g, bool isColumnSpecified, int[] countList )
		{
			// save the layout settings for future reference
			this.isColumnSpecified = isColumnSpecified;
			this.countList = countList;
			this.rows = -1;
			this.columns = -1;

			// punt if there are no entries in the countList
			if ( countList.Length <= 0 )
				return;

			// calculate scaleFactor on "normal" pane size (BaseDimension)
			float scaleFactor = this.CalcScaleFactor();

			// innerRect is the area for the GraphPane's
			RectangleF innerRect = CalcInnerRect( g, scaleFactor );			

			// scaled InnerGap is the area between the GraphPane.PaneRect's
			float scaledInnerGap = (float) ( Default.InnerPaneGap * scaleFactor );

			int iPane = 0;

			if ( isColumnSpecified )
			{
				int rows = countList.Length;
				float height = ( innerRect.Height - (float)(rows - 1) * scaledInnerGap ) / (float) rows;

				for ( int rowNum=0; rowNum<rows; rowNum++ )
				{
					int columns = countList[rowNum];
					if ( columns <= 0 )
						columns = 1;
					float width = ( innerRect.Width - (float)(columns - 1) * scaledInnerGap ) / (float) columns;

					if ( iPane >= this.paneList.Count )
						return;

					for ( int colNum=0; colNum<columns; colNum++ )
					{
						this[iPane].PaneRect = new RectangleF(
											innerRect.X + colNum * ( width + scaledInnerGap ),
											innerRect.Y + rowNum * ( height + scaledInnerGap ),
											width,
											height );
						iPane++;
					}
				}
			}
			else
			{
				int columns = countList.Length;
				float width = ( innerRect.Width - (float)(columns - 1) * scaledInnerGap ) / (float) columns;

				for ( int colNum=0; colNum<columns; colNum++ )
				{
					int rows = countList[colNum];
					if ( rows <= 0 )
						rows = 1;
					float height = ( innerRect.Height - (float)(rows - 1) * scaledInnerGap ) / (float) rows;

					for ( int rowNum=0; rowNum<rows; rowNum++ )
					{
						if ( iPane >= this.paneList.Count )
							return;

						this[iPane].PaneRect = new RectangleF(
											innerRect.X + colNum * ( width + scaledInnerGap ),
											innerRect.Y + rowNum * ( height + scaledInnerGap ),
											width,
											height );
						iPane++;
					}
				}
			}
		}

		/// <summary>
		/// Automatically set all of the <see cref="GraphPane"/> <see cref="PaneBase.PaneRect"/>'s in
		/// the list to a reasonable configuration.
		/// </summary>
		/// <remarks>This method explicitly specifies the number of rows and columns to use in the layout.
		/// A more automatic overload, using a <see cref="PaneLayout"/> enumeration, is available.</remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rows">The number of rows of <see cref="GraphPane"/> objects
		/// to include in the layout</param>
		/// <param name="columns">The number of columns of <see cref="GraphPane"/> objects
		/// to include in the layout</param>
		public void AutoPaneLayout( Graphics g, int rows, int columns )
		{
			// save the layout settings for future reference
			this.countList = null;
			this.rows = rows;
			this.columns = columns;

			// calculate scaleFactor on "normal" pane size (BaseDimension)
			float scaleFactor = this.CalcScaleFactor();

			// innerRect is the area for the GraphPane's
			RectangleF innerRect = CalcInnerRect( g, scaleFactor );			

			// scaled InnerGap is the area between the GraphPane.PaneRect's
			float scaledInnerGap = (float) ( Default.InnerPaneGap * scaleFactor );

			float width = ( innerRect.Width - (float)(columns - 1) * scaledInnerGap ) / (float) columns;
			float height = ( innerRect.Height - (float)(rows - 1) * scaledInnerGap ) / (float) rows;

			int i = 0;
			foreach ( GraphPane pane in this.paneList )
			{
				float rowNum = (float) ( i / columns );
				float colNum = (float) ( i % columns );

				pane.PaneRect = new RectangleF(
									innerRect.X + colNum * ( width + scaledInnerGap ),
									innerRect.Y + rowNum * ( height + scaledInnerGap ),
									width,
									height );

				i++;
			}
		}
		
		/// <summary>
		/// Find the pane and the object within that pane that lies closest to the specified
		/// mouse (screen) point.
		/// </summary>
		/// <remarks>
		/// This method first finds the <see cref="GraphPane"/> within the list that contains
		/// the specified mouse point.  It then calls the <see cref="GraphPane.FindNearestObject"/>
		/// method to determine which object, if any, was clicked.  With the exception of the
		/// <see paramref="pane"/>, all the parameters in this method are identical to those
		/// in the <see cref="GraphPane.FindNearestObject"/> method.
		/// If the mouse point lies within the <see cref="PaneBase.PaneRect"/> of any 
		/// <see cref="GraphPane"/> item, then that pane will be returned (otherwise it will be
		/// null).  Further, within the selected pane, if the mouse point is within the
		/// bounding box of any of the items (or in the case
		/// of <see cref="ArrowItem"/> and <see cref="CurveItem"/>, within
		/// <see cref="GraphPane.Default.NearestTol"/> pixels), then the object will be returned.
		/// You must check the type of the object to determine what object was
		/// selected (for example, "if ( object is Legend ) ...").  The
		/// <see paramref="index"/> parameter returns the index number of the item
		/// within the selected object (such as the point number within a
		/// <see cref="CurveItem"/> object.
		/// </remarks>
		/// <param name="mousePt">The screen point, in pixel coordinates.</param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that was clicked.</param>
		/// <param name="nearestObj">A reference to the nearest object to the
		/// specified screen point.  This can be any of <see cref="Axis"/>,
		/// <see cref="Legend"/>, <see cref="PaneBase.Title"/>,
		/// <see cref="TextItem"/>, <see cref="ArrowItem"/>, or <see cref="CurveItem"/>.
		/// Note: If the pane title is selected, then the <see cref="GraphPane"/> object
		/// will be returned.
		/// </param>
		/// <param name="index">The index number of the item within the selected object
		/// (where applicable).  For example, for a <see cref="CurveItem"/> object,
		/// <see paramref="index"/> will be the index number of the nearest data point,
		/// accessible via <see cref="CurveItem.Points">CurveItem.Points[index]</see>.
		/// index will be -1 if no data points are available.</param>
		/// <returns>true if a <see cref="GraphPane"/> was found, false otherwise.</returns>
		/// <seealso cref="GraphPane.FindNearestObject"/>
		public bool FindNearestPaneObject( PointF mousePt, Graphics g, out GraphPane pane,
			out object nearestObj, out int index )
		{
			pane = null;
			nearestObj = null;
			index = -1;

			GraphItem	saveGraphItem = null;
			int			saveIndex = -1;
			float		scaleFactor = CalcScaleFactor();

			// See if the point is in a GraphItem
			// If so, just save the object and index so we can see if other overlying objects were
			// intersected as well.
			if ( this.GraphItemList.FindPoint( mousePt, this, g, scaleFactor, out index ) )
			{
				saveGraphItem = this.GraphItemList[index];
				saveIndex = index;

				// If it's an "In-Front" item, then just return it
				if ( saveGraphItem.ZOrder == ZOrder.A_InFront )
				{
					nearestObj = saveGraphItem;
					index = saveIndex;
					return true;
				}
			}

			foreach ( GraphPane tPane in this.paneList )
			{
				if ( tPane.PaneRect.Contains( mousePt ) )
				{
					pane = tPane;
					return tPane.FindNearestObject( mousePt, g, out nearestObj, out index );
				}
			}

			// If no items were found in the GraphPanes, then return the item found on the MasterPane (if any)
			if ( saveGraphItem != null )
			{
				nearestObj = saveGraphItem;
				index = saveIndex;
				return true;
			}

			return false;
		}

	#endregion

	}
}
