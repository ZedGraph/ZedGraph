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
using System.Collections;
using System.Text;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A class that captures an <see cref="Axis"/> scale range.
	/// </summary>
	/// <remarks>This structure is used by the <see cref="ZoomState"/> class to store
	/// <see cref="Axis"/> scale range settings in a collection for later retrieval.
	/// The class stores the <see cref="Axis.Min"/>, <see cref="Axis.Max"/>,
	/// <see cref="Axis.MinorStep"/>, and <see cref="Axis.Step"/> properties, along with
	/// the corresponding auto-scale settings: <see cref="Axis.MinAuto"/>,
	/// <see cref="Axis.MaxAuto"/>, <see cref="Axis.MinorStepAuto"/>,
	/// and <see cref="Axis.StepAuto"/>.</remarks>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2005-02-23 05:49:26 $ </version>
	public class ScaleState
	{
		/// <summary>
		/// The axis range data for <see cref="Axis.Min"/>, <see cref="Axis.Max"/>,
		/// <see cref="Axis.MinorStep"/>, and <see cref="Axis.Step"/>
		/// </summary>
		private double	min, minorStep, step, max;
		/// <summary>
		/// The status of <see cref="Axis.MinAuto"/>,
		/// <see cref="Axis.MaxAuto"/>, <see cref="Axis.MinorStepAuto"/>,
		/// and <see cref="Axis.StepAuto"/>
		/// </summary>
		private bool	minAuto, minorStepAuto, stepAuto, maxAuto;

		/// <summary>
		/// Construct a <see cref="ScaleState"/> from the specified <see cref="Axis"/>
		/// </summary>
		/// <param name="axis">The <see cref="Axis"/> from which to collect the scale
		/// range settings.</param>
		public ScaleState( Axis axis )
		{
			this.min = axis.Min;
			this.minorStep = axis.MinorStep;
			this.step = axis.Step;
			this.max = axis.Max;

			this.minAuto = axis.MinAuto;
			this.stepAuto = axis.StepAuto;
			this.minorStepAuto = axis.MinorStepAuto;
			this.maxAuto = axis.MaxAuto;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ScaleState"/> object from which to copy</param>
		public ScaleState( ScaleState rhs )
		{
			this.min = rhs.min;
			this.step = rhs.step;
			this.minorStep = rhs.minorStep;
			this.max = rhs.max;

			this.minAuto = rhs.minAuto;
			this.stepAuto = rhs.stepAuto;
			this.minorStepAuto = rhs.minorStepAuto;
			this.maxAuto = rhs.maxAuto;
		}

		/// <summary>
		/// Copy the properties from this <see cref="ScaleState"/> out to the specified <see cref="Axis"/>.
		/// </summary>
		/// <param name="axis">The <see cref="Axis"/> reference to which the properties should be
		/// copied</param>
		public void ApplyScale( Axis axis )
		{
			axis.Min = this.min;
			axis.Step = this.step;
			axis.MinorStep = this.minorStep;
			axis.Max = this.max;

			// The auto settings must be made after the min/step/max settings, since setting those
			// properties actually affects the auto settings.
			axis.MinAuto = this.minAuto;
			axis.MinorStepAuto = this.minorStepAuto;
			axis.StepAuto = this.stepAuto;
			axis.MaxAuto = this.maxAuto;
		}

		/// <summary>
		/// Determine if the state contained in this <see cref="ScaleState"/> object is different from
		/// the state of the specified <see cref="Axis"/>.
		/// </summary>
		/// <param name="axis">The <see cref="Axis"/> object with which to compare states.</param>
		/// <returns>true if the states are different, false otherwise</returns>
		public bool IsChanged( Axis axis )
		{
			return	axis.Min != this.min ||
					axis.Step != this.step ||
					axis.MinorStep != this.minorStep ||
					axis.Max != this.max ||
					axis.MinAuto != this.minAuto ||
					axis.MinorStepAuto != this.minorStepAuto ||
					axis.StepAuto != this.stepAuto ||
					axis.MaxAuto != this.maxAuto;
		}

	}

	/// <summary>
	/// A class that captures all the scale range settings for a <see cref="GraphPane"/>.
	/// </summary>
	/// <remarks>
	/// This class is used to store scale ranges in order to allow zooming out to
	/// prior scale range states.  <see cref="ZoomState"/> objects are maintained in the
	/// <see cref="ZoomStateStack"/> collection.  The <see cref="ZoomState"/> object holds
	/// a <see cref="ScaleState"/> object for each of the three axes; the <see cref="XAxis"/>,
	/// the <see cref="YAxis"/>, and the <see cref="Y2Axis"/>.
	/// </remarks>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2005-02-23 05:49:26 $ </version>
	public class ZoomState
	{
		public enum StateType { Zoom, Pan }

		/// <summary>
		/// <see cref="ScaleState"/> objects to store the state data from the axes.
		/// </summary>
		private ScaleState	xAxis, yAxis, y2Axis;
		/// <summary>
		/// An enum value indicating the type of adjustment being made to the
		/// scale range state.
		/// </summary>
		private StateType	type;

		/// <summary>
		/// Gets a string representing the type of adjustment that was made when this scale
		/// state was saved.
		/// </summary>
		/// <value>A string representation for the state change type; typically
		/// "Pan" or "Zoom".</value>
		public string TypeString
		{
			get { return type == StateType.Pan ? "Pan" : "Zoom"; }
		}

		/// <summary>
		/// Construct a <see cref="ZoomState"/> object from the scale ranges settings contained
		/// in the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> from which to obtain the scale
		/// range values.
		/// </param>
		public ZoomState( GraphPane pane, StateType type )
		{
			this.xAxis = new ScaleState( pane.XAxis );
			this.yAxis = new ScaleState( pane.YAxis );
			this.y2Axis = new ScaleState( pane.Y2Axis );
			this.type = type;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ZoomState"/> object from which to copy</param>
		public ZoomState( ZoomState rhs )
		{
			this.xAxis = new ScaleState( rhs.xAxis );
			this.yAxis = new ScaleState( rhs.yAxis );
			this.y2Axis = new ScaleState( rhs.y2Axis );
		}

		/// <summary>
		/// Copy the properties from this <see cref="ZoomState"/> out to the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> to which the scale range properties should be
		/// copied.</param>
		public void ApplyState( GraphPane pane )
		{
			this.xAxis.ApplyScale( pane.XAxis );
			this.yAxis.ApplyScale( pane.YAxis );
			this.y2Axis.ApplyScale( pane.Y2Axis );
		}

		/// <summary>
		/// Determine if the state contained in this <see cref="ZoomState"/> object is different from
		/// the state of the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> object with which to compare states.</param>
		/// <returns>true if the states are different, false otherwise</returns>
		public bool IsChanged( GraphPane pane )
		{
			return	this.xAxis.IsChanged( pane.XAxis ) ||
					this.yAxis.IsChanged( pane.YAxis ) ||
					this.y2Axis.IsChanged( pane.Y2Axis );
		}

	}

	/// <summary>
	/// A LIFO stack of prior <see cref="ZoomState"/> objects, used to allow zooming out to prior
	/// states (of scale range settings).
	/// </summary>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2005-02-23 05:49:26 $ </version>
	public class ZoomStateStack : CollectionBase
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ZoomStateStack()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ZoomStateStack"/> object from which to copy</param>
		public ZoomStateStack( ZoomStateStack rhs )
		{
			foreach ( ZoomState state in rhs )
			{
				this.List.Add( new ZoomState( state ) );
			}
		}

		/// <summary>
		/// Public readonly property that indicates if the stack is empty
		/// </summary>
		/// <value>true for an empty stack, false otherwise</value>
		public bool IsEmpty
		{
			get { return this.List.Count == 0; }
		}

		/// <summary>
		/// Add the scale range information from the specified <see cref="GraphPane"/> object as a
		/// new <see cref="ZoomState"/> entry on the stack.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> object from which the scale range
		/// information should be copied.</param>
		public void Push( GraphPane pane, ZoomState.StateType type )
		{
			ZoomState state = new ZoomState( pane, type );
			this.List.Add( state );
		}

		/// <summary>
		/// Add the scale range information from the specified <see cref="ZoomState"/> object as a
		/// new <see cref="ZoomState"/> entry on the stack.
		/// </summary>
		/// <param name="pane">The <see cref="ZoomState"/> object to be placed on the stack.</param>
		public void Push( ZoomState state )
		{
			this.List.Add( state );
		}

		/// <summary>
		/// Pop a <see cref="ZoomState"/> entry from the top of the stack, and apply the properties
		/// to the specified <see cref="GraphPane"/> object.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> object to which the scale range
		/// information should be copied.</param>
		public void Pop( GraphPane pane )
		{
			if ( !this.IsEmpty )
			{
				ZoomState state = (ZoomState) this.List[ this.List.Count - 1 ];
				this.List.RemoveAt( this.List.Count - 1 );

				state.ApplyState( pane );
			}
		}

		/// <summary>
		/// Gets a reference to the <see cref="ZoomState"/> object at the top of the stack,
		/// without actually removing it from the stack.
		/// </summary>
		/// <value>A <see cref="ZoomState"/> object reference, or null if the stack is empty.</value>
		public ZoomState Top
		{
			get
			{
				if ( !this.IsEmpty )
					return (ZoomState) this.List[ this.List.Count - 1 ];
				else
					return null;
			}
		}
	}
}
