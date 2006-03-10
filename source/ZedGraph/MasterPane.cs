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
	/// <version> $Revision: 3.17 $ $Date: 2006-03-10 07:26:50 $ </version>
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
		/// Private field that sets the amount of space between the GraphPanes.  Use the public property
		/// <see cref="InnerPaneGap"/> to access this value;
		/// </summary>
		private float innerPaneGap;

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

		/// <summary>
		/// private field that stores the row/column size proportional values as specified
		/// to the <see cref="AutoPaneLayout(Graphics,bool,int[],float[])"/> method.  This
		/// value will be null if <see cref="AutoPaneLayout(Graphics,bool,int[],float[])"/>
		/// was never called.  
		/// </summary>
		private float[] prop;

		/// <summary>
		///Private field that stores a boolean value which signifies whether all 
		///<see cref="ZedGraph.GraphPane"/>s in the chart use the same entries in their 
		///<see cref="Legend"/>  If set to true, only one set of entries will be displayed in 
		///this <see cref="Legend"/> instance.  If set to false, this instance will display all 
		///entries from all <see cref="ZedGraph.GraphPane"/>s.
		/// </summary>
		private bool hasUniformLegendEntries;



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
			/// This is the size of the margin between adjacent <see cref="GraphPane"/>
			/// objects, in units of points (1/72 inch).
			/// </summary>
			/// <seealso cref="MasterPane.InnerPaneGap"/>
			public static float InnerPaneGap = 10;
			
			/// <summary>
			/// The default value for the <see cref="Legend.IsVisible"/> property for
			/// the <see cref="MasterPane"/> class.
			/// </summary>
			public static bool IsShowLegend = false;
			/// <summary>
			/// The default value for the <see cref="MasterPane.hasUniformLegendEntries"/> property.
			/// </summary>
			public static bool hasUniformLegendEntries = false;
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

		/// <summary>
		/// Gets or sets the size of the margin between adjacent <see cref="GraphPane"/>
		/// objects.
		/// </summary>
		/// <remarks>This property is scaled according to <see cref="PaneBase.CalcScaleFactor"/>,
		/// based on <see cref="PaneBase.BaseDimension"/>.  The default value comes from
		/// <see cref="Default.InnerPaneGap"/>.
		/// </remarks>
		/// <value>The value is in points (1/72nd inch).</value>
		public float InnerPaneGap
		{
			get { return innerPaneGap; }
			set { innerPaneGap = value; }
		}
		/// <summary>
		/// Gets or set the value of the	 <see cref="MasterPane.hasUniformLegendEntries"/>
		/// </summary>
		public bool HasUniformLegendEntries
		{
			get { return (this.hasUniformLegendEntries); }
			set { this.hasUniformLegendEntries = value; } 
		}
		#endregion
	
	#region Constructors

		/// <summary>
		/// Default constructor for the class.  Sets the <see cref="PaneBase.PaneRect"/> to (0, 0, 500, 375).
		/// </summary>
		public MasterPane() : this( "", new RectangleF( 0, 0, 500, 375 ) )
		{
		}
		
		/// <summary>
		/// Default constructor for the class.  Specifies the <see cref="PaneBase.Title"/> of
		/// the <see cref="MasterPane"/>, and the size of the <see cref="PaneBase.PaneRect"/>.
		/// </summary>
		public MasterPane( string title, RectangleF paneRect ) : base( title, paneRect )
		{
			this.paneLayout = Default.PaneLayout;
			this.innerPaneGap = Default.InnerPaneGap;
			this.rows = -1;
			this.columns = -1;
			this.isColumnSpecified = false;
			this.countList = null;
			this.prop = null;

			this.hasUniformLegendEntries = Default.hasUniformLegendEntries ;

			this.paneList = new PaneList();

			this.legend.IsVisible = Default.IsShowLegend;
		}

		/// <summary>
		/// The Copy Constructor - Make a deep-copy clone of this class instance.
		/// </summary>
		/// <param name="rhs">The <see cref="MasterPane"/> object from which to copy</param>
		public MasterPane( MasterPane rhs ) : base( rhs )
		{
			// copy all the value types
			this.paneLayout = rhs.paneLayout;
			this.innerPaneGap = rhs.innerPaneGap;
			this.rows = rhs.rows;
			this.columns = rhs.columns;
			this.isColumnSpecified = rhs.isColumnSpecified;
			this.countList = rhs.countList;
			this.prop = rhs.prop;
			this.hasUniformLegendEntries = rhs.hasUniformLegendEntries ;

			// Then, fill in all the reference types with deep copies
			this.paneList = rhs.paneList.Clone();

		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" /> to make a deep copy.
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
		public MasterPane Clone()
		{
			return new MasterPane( this );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		// schema changed to 2 with addition of 'prop'
		public const int schema2 = 2;

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
			this.innerPaneGap = info.GetSingle( "innerPaneGap" );

			this.rows = info.GetInt32( "rows" );
			this.columns = info.GetInt32( "columns" );
			this.isColumnSpecified = info.GetBoolean( "isColumnSpecified" );
			this.countList = (int[]) info.GetValue( "countList", typeof(int[]) );
			this.hasUniformLegendEntries = info.GetBoolean( "hasUniformLegendEntries" );

			if ( sch >= 2 )
				this.prop = (float[]) info.GetValue( "prop", typeof(float[]) );
			else
				this.prop = null;
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
			info.AddValue( "innerPaneGap", innerPaneGap );

			info.AddValue( "rows", rows );
			info.AddValue( "columns", columns );
			info.AddValue( "isColumnSpecified", isColumnSpecified );
			info.AddValue( "countList", countList );
			info.AddValue( "hasUniformLegendEntries", hasUniformLegendEntries );

			info.AddValue( "prop", prop );
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
				AutoPaneLayout( g, this.isColumnSpecified, this.countList, this.prop );
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

			if ( paneRect.Width <= 1 || paneRect.Height <= 1 )
				return;

			float scaleFactor = CalcScaleFactor();

			// Clip everything to the paneRect
			g.SetClip( this.paneRect );

			// For the MasterPane, All GraphItems go behind the GraphPanes, except those that
			// are explicity declared as ZOrder.A_InFront
			graphItemList.Draw( g, this, scaleFactor, ZOrder.F_BehindAxisFill );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.E_BehindAxis );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.D_BehindCurves );
			graphItemList.Draw( g, this, scaleFactor, ZOrder.C_BehindAxisBorder );

			// Reset the clipping
			g.ResetClip();

			foreach ( GraphPane pane in paneList )
				pane.Draw( g );

			// Clip everything to the paneRect
			g.SetClip( this.paneRect );

			graphItemList.Draw( g, this, scaleFactor, ZOrder.B_BehindLegend );
			
			// Recalculate the legend rect, just in case it has not yet been done
			// innerRect is the area for the GraphPane's
			RectangleF innerRect = CalcClientRect( g, scaleFactor );
			this.legend.CalcRect( g, this, scaleFactor, ref innerRect );
			//this.legend.SetLocation( this, 
			
			this.legend.Draw( g, this, scaleFactor );

			graphItemList.Draw( g, this, scaleFactor, ZOrder.A_InFront );
			
			// Reset the clipping
			g.ResetClip();
		}

		/// <summary>
		/// Automatically set all of the <see cref="GraphPane"/> <see cref="PaneBase.PaneRect"/>'s in
		/// the list to a default layout configuration.
		/// </summary>
		/// <remarks>This method uses a default <see cref="PaneLayout"/> enumeration of
		/// <see cref="PaneLayout.SquareColPreferred" />.  Other pane layout options are available
		/// including an explicit (row,column) overload and a more general overload.</remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		public void AutoPaneLayout( Graphics g )
		{
			AutoPaneLayout( g, PaneLayout.SquareColPreferred );
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
			AutoPaneLayout( g, isColumnSpecified, countList, null );
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
		/// <param name="proportion">An array of float values specifying proportional sizes for each
		/// row or column.  Note that these proportions apply to the non-specified dimension -- that is,
		/// if <see paramref="isColumnSpecified"/> is true, then these proportions apply to the row
		/// heights, and if <see paramref="isColumnSpecified"/> is false, then these proportions apply
		/// to the column widths.  The values in this array are arbitrary floats -- the dimension of
		/// any given row or column is that particular proportional value divided by the sum of all
		/// the values.  For example, let <see paramref="isColumnSpecified"/> be true, and
		/// <see paramref="proportion"/> is an array with values of { 1.0, 2.0, 3.0 }.  The sum of
		/// those values is 6.0.  Therefore, the first row is 1/6th of the available height, the
		/// second row is 2/6th's of the available height, and the third row is 3/6th's of the
		/// available height.
		/// </param>
		public void AutoPaneLayout( Graphics g, bool isColumnSpecified, int[] countList,
						float[] proportion )
		{
			// save the layout settings for future reference
			this.isColumnSpecified = isColumnSpecified;
			this.countList = countList;
			this.rows = -1;
			this.columns = -1;
			this.prop = null;

			// punt if there are no entries in the countList
			if ( countList.Length <= 0 )
				return;

			this.prop = new float[countList.Length];

			// Sum up the total proportional factors
			float sumProp = 0.0f;
			for ( int i=0; i<countList.Length; i++ )
			{
				this.prop[i] = ( proportion == null || proportion.Length <= i || proportion[i] < 1e-10 ) ?
											1.0f : proportion[i];
				sumProp += this.prop[i];
			}

			// calculate scaleFactor on "normal" pane size (BaseDimension)
			float scaleFactor = this.CalcScaleFactor();

			// innerRect is the area for the GraphPane's
			RectangleF innerRect = CalcClientRect( g, scaleFactor );			
			this.legend.CalcRect( g, this, scaleFactor, ref innerRect );

			// scaled InnerGap is the area between the GraphPane.PaneRect's
			float scaledInnerGap = (float) ( this.innerPaneGap * scaleFactor );

			int iPane = 0;

			if ( isColumnSpecified )
			{
				int rows = countList.Length;

				float y = 0.0f;

				for ( int rowNum=0; rowNum<rows; rowNum++ )
				{
					float height = ( innerRect.Height - (float)( rows - 1 ) * scaledInnerGap ) *
									this.prop[rowNum] / sumProp;

					int columns = countList[rowNum];
					if ( columns <= 0 )
						columns = 1;
					float width = ( innerRect.Width - (float)(columns - 1) * scaledInnerGap ) /
									(float) columns;

					if ( iPane >= this.paneList.Count )
						return;

					for ( int colNum=0; colNum<columns; colNum++ )
					{
						this[iPane].PaneRect = new RectangleF(
											innerRect.X + colNum * ( width + scaledInnerGap ),
											innerRect.Y + y,
											width,
											height );
						iPane++;
					}

					y += height + scaledInnerGap;
				}
			}
			else
			{
				int columns = countList.Length;

				float x = 0.0f;

				for ( int colNum=0; colNum<columns; colNum++ )
				{
					float width = ( innerRect.Width - (float)( columns - 1 ) * scaledInnerGap ) *
									this.prop[colNum] / sumProp;

					int rows = countList[colNum];
					if ( rows <= 0 )
						rows = 1;
					float height = ( innerRect.Height - (float)(rows - 1) * scaledInnerGap ) / (float) rows;

					for ( int rowNum=0; rowNum<rows; rowNum++ )
					{
						if ( iPane >= this.paneList.Count )
							return;

						this[iPane].PaneRect = new RectangleF(
											innerRect.X + x,
											innerRect.Y + rowNum * ( height + scaledInnerGap ),
											width,
											height );
						iPane++;
					}

					x += width + scaledInnerGap;
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
			RectangleF innerRect = CalcClientRect( g, scaleFactor );			
			this.legend.CalcRect( g, this, scaleFactor, ref innerRect );

			// scaled InnerGap is the area between the GraphPane.PaneRect's
			float scaledInnerGap = (float) ( this.innerPaneGap * scaleFactor );

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

		/// <summary>
		/// Find the <see cref="GraphPane"/> within the <see cref="PaneList"/> that contains the
		/// <see paramref="mousePt"/> within its <see cref="PaneBase.PaneRect"/>.
		/// </summary>
		/// <param name="mousePt">The mouse point location where you want to search</param>
		/// <returns>A <see cref="GraphPane"/> object that contains the mouse point, or
		/// null if no <see cref="GraphPane"/> was found.</returns>
		public GraphPane FindPane( PointF mousePt )
		{
			foreach ( GraphPane pane in this.paneList )
			{
				if ( pane.PaneRect.Contains( mousePt ) )
					return pane;
			}
			
			return null;
		}

		/// <summary>
		/// Find the <see cref="GraphPane"/> within the <see cref="PaneList"/> that contains the
		/// <see paramref="mousePt"/> within its <see cref="GraphPane.AxisRect"/>.
		/// </summary>
		/// <param name="mousePt">The mouse point location where you want to search</param>
		/// <returns>A <see cref="GraphPane"/> object that contains the mouse point, or
		/// null if no <see cref="GraphPane"/> was found.</returns>
		public GraphPane FindAxisRect( PointF mousePt )
		{
			foreach ( GraphPane pane in this.paneList )
			{
				if ( pane.AxisRect.Contains( mousePt ) )
					return pane;
			}
			
			return null;
		}

	#endregion

	}
}
