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
using System.Drawing.Drawing2D;

namespace ZedGraph
{
	/// <summary>
	/// A class representing all the characteristics of the <see cref="Bar"/>
	/// segments that make up a curve on the graph.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.3 $ $Date: 2004-08-23 20:27:45 $ </version>
	public class Bar
	{
		/// <summary>
		/// Private field that stores the color of the frame around this
		/// <see cref="Bar"/>.  Use the public
		/// property <see cref="FrameColor"/> to access this value.  This property is
		/// only applicable if the <see cref="IsFramed"/> property is true.
		/// </summary>
		private Color	frameColor;
		/// <summary>
		/// Private field that stores the fill color of this
		/// <see cref="Bar"/>.  Use the public
		/// property <see cref="FillColor"/> to access this value.  This property is
		/// only applicable if the <see cref="IsFilled"/> property is true.
		/// </summary>
		private Color	fillColor;
		/// <summary>
		/// Private field that determines if a frame will be drawn around this
		/// <see cref="Bar"/>.  Use the public
		/// property <see cref="IsFramed"/> to access this value.  The pen width
		/// for the frame is determined by the property <see cref="FrameWidth"/>.
		/// </summary>
		private bool	isFramed;
		/// <summary>
		/// Private field that determines if this
		/// <see cref="Bar"/> will be filled with color.  Use the public
		/// property <see cref="IsFilled"/> to access this value.  The fill color
		/// is determined by the property <see cref="FillColor"/>.
		/// </summary>
		private bool	isFilled;
		/// <summary>
		/// Private field that determines the pen width for the frame around this
		/// <see cref="Bar"/>, in pixel units.  Use the public
		/// property <see cref="FrameWidth"/> to access this value.
		/// </summary>
		private float	frameWidth;

		/// <summary>
		/// Default constructor that sets all <see cref="Bar"/> properties to default
		/// values as defined in the <see cref="Def"/> class.
		/// </summary>
		public Bar()
		{
			this.frameColor = Def.Br.FrameColor;
			this.fillColor = Def.Br.FillColor;
			this.isFramed = Def.Br.IsFramed;
			this.isFilled = Def.Br.IsFilled;
			this.frameWidth = Def.Br.FrameWidth;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Bar object from which to copy</param>
		public Bar( Bar rhs )
		{
			this.frameColor = rhs.FrameColor;
			this.fillColor = rhs.FillColor;
			this.isFramed = rhs.IsFramed;
			this.isFilled = rhs.IsFilled;
			this.frameWidth = rhs.FrameWidth;
		}

		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Bar</returns>
		public object Clone()
		{ 
			return new Bar( this ); 
		}

		/// <summary>
		/// The color of the frame around the <see cref="Bar"/>.
		/// </summary>
		/// <seealso cref="IsFramed"/>
		/// <seealso cref="FrameWidth"/>
		/// <seealso cref="Def.Br.FrameColor"/>
		public Color FrameColor
		{
			get { return frameColor; }
			set { frameColor = value; }
		}
		/// <summary>
		/// The fill color of the <see cref="Bar"/>.
		/// </summary>
		/// <seealso cref="IsFilled"/>
		/// <seealso cref="Def.Br.FillColor"/>
		public Color FillColor
		{
			get { return fillColor; }
			set { fillColor = value; }
		}

		/// <summary>
		/// Determines if the <see cref="Bar"/> has a frame around it.
		/// </summary>
		/// <seealso cref="FrameColor"/>
		/// <seealso cref="FrameWidth"/>
		/// <seealso cref="Def.Br.IsFramed"/>
		public bool IsFramed
		{
			get { return isFramed; }
			set { isFramed = value; }
		}
		/// <summary>
		/// Determines if the <see cref="Bar"/> is filled with color.
		/// </summary>
		/// <seealso cref="FillColor"/>
		/// <seealso cref="Def.Br.IsFilled"/>
		public bool IsFilled
		{
			get { return isFilled; }
			set { isFilled = value; }
		}
		/// <summary>
		/// Determines the pen width for the frame around the <see cref="Bar"/>,
		/// in pixel units.
		/// </summary>
		/// <seealso cref="IsFramed"/>
		/// <seealso cref="FrameColor"/>
		/// <seealso cref="Def.Br.FrameWidth"/>
		public float FrameWidth
		{
			get { return frameWidth; }
			set { frameWidth = value; }
		}

		/// <summary>
		/// Draw the <see cref="Bar"/> to the specified <see cref="Graphics"/> device
		/// at the specified location.  This routine draws a single bar.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="left">The x position of the left side of the bar in
		/// screen pixel units</param>
		/// <param name="right">The x position of the right side of the bar in
		/// screen pixel units</param>
		/// <param name="top">The y position of the top of the bar in
		/// screen pixel units</param>
		/// <param name="bottom">The y position of the bottom of the bar in
		/// screen pixel units</param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="GraphPane.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="GraphPane.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <param name="fullFrame">true to draw the bottom portion of the frame around the
		/// bar (this is for legend entries)</param>	
		public void Draw( Graphics g, float left, float right, float top,
						float bottom, double scaleFactor, bool fullFrame )
		{
			if ( top > bottom )
			{
				float junk = top;
				top = bottom;
				bottom = junk;
			}

			if ( this.isFilled )
			{
				SolidBrush	brush = new SolidBrush( this.fillColor );

				g.FillRectangle( brush, left, top, right-left, bottom-top );
			}

			if ( this.isFramed )
			{
				Pen pen = new Pen( this.frameColor, this.frameWidth );

				g.DrawLine( pen, left, bottom, left, top );
				g.DrawLine( pen, left, top, right, top );
				g.DrawLine( pen, right, top, right, bottom );
				if ( fullFrame )
					g.DrawLine( pen, right, bottom, left, bottom );
			}
		}
	}
}
