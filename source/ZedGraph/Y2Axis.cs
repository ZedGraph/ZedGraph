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

namespace ZedGraph
{
	/// <summary>
	/// <see cref="Y2Axis"/> inherits from <see cref="Axis"/>, and defines the
	/// special characteristics of a vertical axis, specifically located on
	/// the right side of the <see cref="GraphPane.AxisRect"/> of the <see cref="GraphPane"/>
	/// object
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.0 $ $Date: 2004-09-22 02:18:08 $ </version>
	public class Y2Axis : Axis, ICloneable
	{
	#region Defaults
		/// <summary>
		/// A simple subclass of the <see cref="Default"/> class that defines the
		/// default property values for the <see cref="Y2Axis"/> class.
		/// </summary>
		public new struct Default
		{
			// Default Y2 Axis properties
			/// <summary>
			/// The default display mode for the <see cref="Y2Axis"/>
			/// (<see cref="Axis.IsVisible"/> property). true to display the scale
			/// values, title, tic marks, false to hide the axis entirely.
			/// </summary>
			public static bool IsVisible = false;
		}
	#endregion

	#region Constructors
		/// <summary>
		/// Default constructor that sets all <see cref="Y2Axis"/> properties to
		/// default values as defined in the <see cref="Default"/> class, except
		/// for the axis title
		/// </summary>
		/// <param name="title">The <see cref="Axis.Title"/> for this axis</param>
		public Y2Axis( string title )
		{
			this.Title = title;
			this.IsVisible = Default.IsVisible;
			this.ScaleFontSpec.Angle = -90.0F;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Y2Axis object from which to copy</param>
		public Y2Axis( Y2Axis rhs ) : base( rhs )
		{
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the Y2Axis</returns>
		public object Clone()
		{ 
			return new Y2Axis( this ); 
		}
	#endregion
		
	#region Methods
		/// <summary>
		/// Setup the Transform Matrix to handle drawing of this <see cref="Y2Axis"/>
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
		override public void SetTransformMatrix( Graphics g, GraphPane pane, double scaleFactor )
		{
			// Move the origin to the BottomRight of the axisRect, which is the left
			// side of the Y2 axis (facing from the label side)
			g.TranslateTransform( pane.AxisRect.Right, pane.AxisRect.Bottom );
			// rotate so this axis is in the left-right direction
			g.RotateTransform( -90 );
		}
	#endregion
	}
}

