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
	/// <version> $Revision: 3.12 $ $Date: 2006-03-27 03:35:43 $ </version>
	public class ScaleState : ICloneable
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
		private bool	minAuto, minorStepAuto, stepAuto, maxAuto,
						scaleFormatAuto, scaleMagAuto;

		/// <summary>
		/// The status of <see cref="Axis.MajorUnit"/> and <see cref="Axis.MinorUnit"/>
		/// </summary>
		private DateUnit minorUnit, majorUnit;

		private string	scaleFormat;
		private int		scaleMag;

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
			this.majorUnit = axis.MajorUnit;
			this.minorUnit = axis.MinorUnit;

			this.scaleFormat = axis.ScaleFormat;
			this.scaleMag = axis.ScaleMag;
			//this.numDec = axis.NumDec;

			this.minAuto = axis.MinAuto;
			this.stepAuto = axis.StepAuto;
			this.minorStepAuto = axis.MinorStepAuto;
			this.maxAuto = axis.MaxAuto;

			this.scaleFormatAuto = axis.ScaleFormatAuto;
			this.scaleMagAuto = axis.ScaleMagAuto;
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
			this.majorUnit = rhs.majorUnit;
			this.minorUnit = rhs.minorUnit;

			this.scaleFormat = rhs.scaleFormat;
			this.scaleMag = rhs.scaleMag;

			this.minAuto = rhs.minAuto;
			this.stepAuto = rhs.stepAuto;
			this.minorStepAuto = rhs.minorStepAuto;
			this.maxAuto = rhs.maxAuto;

			this.scaleFormatAuto = rhs.scaleFormatAuto;
			this.scaleMagAuto = rhs.scaleMagAuto;
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
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
		public ScaleState Clone()
		{
			return new ScaleState( this );
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
			axis.MajorUnit = this.majorUnit;
			axis.MinorUnit = this.minorUnit;

			axis.ScaleFormat = this.scaleFormat;
			axis.ScaleMag = this.scaleMag;

			// The auto settings must be made after the min/step/max settings, since setting those
			// properties actually affects the auto settings.
			axis.MinAuto = this.minAuto;
			axis.MinorStepAuto = this.minorStepAuto;
			axis.StepAuto = this.stepAuto;
			axis.MaxAuto = this.maxAuto;

			axis.ScaleFormatAuto = this.scaleFormatAuto;
			axis.ScaleMagAuto = this.scaleMagAuto;

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
					axis.MinorUnit != this.minorUnit ||
					axis.MajorUnit != this.majorUnit ||
					axis.MinAuto != this.minAuto ||
					axis.MinorStepAuto != this.minorStepAuto ||
					axis.StepAuto != this.stepAuto ||
					axis.MaxAuto != this.maxAuto;
		}

	}

	/// <summary>
	/// A collection class that maintains a list of <see cref="ScaleState" />
	/// objects, corresponding to the list of <see cref="Axis" /> objects
	/// from <see cref="GraphPane.YAxisList" /> or <see cref="GraphPane.Y2AxisList" />.
	/// </summary>
	public class ScaleStateList : CollectionPlus, ICloneable
	{
		/// <summary>
		/// Construct a new <see cref="ScaleStateList" /> automatically from an
		/// existing <see cref="AxisList" />.
		/// </summary>
		/// <param name="list">The <see cref="AxisList" /> (a list of Y axes),
		/// from which to retrieve the state and create the <see cref="ScaleState" />
		/// objects.</param>
		public ScaleStateList( AxisList list )
		{
			foreach( Axis axis in list )
				this.Add( new ScaleState( axis ) );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ScaleStateList"/> object from which to copy</param>
		public ScaleStateList( ScaleStateList rhs )
		{
			foreach ( ScaleState item in rhs )
			{
				this.Add( item.Clone() );
			}
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
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
		public ScaleStateList Clone()
		{
			return new ScaleStateList( this );
		}

		/// <summary>
		/// Iterate through the list of <see cref="ScaleState" /> objects, comparing them
		/// to the state of the specified <see cref="AxisList" /> <see cref="Axis" />
		/// objects.
		/// </summary>
		/// <param name="list">An <see cref="AxisList" /> object specifying a list of
		/// <see cref="Axis" /> objects to be compared with this <see cref="ScaleStateList" />.
		/// </param>
		/// <returns>true if a difference is found, false otherwise</returns>
		public bool IsChanged( AxisList list )
		{
			int count = Math.Min( list.Count, this.Count );
			for ( int i=0; i<count; i++ )
				if ( this[i].IsChanged( list[i] ) )
					return true;

			return false;
		}

		/// <summary>
		/// Indexer to access the specified <see cref="ScaleState"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="ScaleState"/> object to be accessed.</param>
		/// <value>A <see cref="ScaleState"/> object reference.</value>
		public ScaleState this[ int index ]  
		{
			get { return (ScaleState) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add a <see cref="ScaleState"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="state">A reference to the <see cref="ScaleState"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public void Add( ScaleState state )
		{
			List.Add( state );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		public void ApplyScale( AxisList list )
		{
			int count = Math.Min( list.Count, this.Count );
			for ( int i=0; i<count; i++ )
				this[i].ApplyScale( list[i] );
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
	/// <version> $Revision: 3.12 $ $Date: 2006-03-27 03:35:43 $ </version>
	public class ZoomState : ICloneable
	{
		/// <summary>
		/// An enumeration that describes whether a given state is the result of a Pan or Zoom
		/// operation.
		/// </summary>
		public enum StateType
		{
			/// <summary>
			/// Indicates the <see cref="ZoomState"/> object is from a Zoom operation
			/// </summary>
			Zoom,
			/// <summary>
			/// Indicates the <see cref="ZoomState"/> object is from a Wheel Zoom operation
			/// </summary>
			WheelZoom,
			/// <summary>
			/// Indicates the <see cref="ZoomState"/> object is from a Pan operation
			/// </summary>
			Pan,
			/// <summary>
			/// Indicates the <see cref="ZoomState"/> object is from a Scroll operation
			/// </summary>
			Scroll
		}

		/// <summary>
		/// <see cref="ScaleState"/> objects to store the state data from the axes.
		/// </summary>
		private ScaleState	xAxis;
		private ScaleStateList yAxis, y2Axis;
		/// <summary>
		/// An enum value indicating the type of adjustment being made to the
		/// scale range state.
		/// </summary>
		private StateType	type;

		/// <summary>
		/// Gets a <see cref="StateType" /> value indicating the type of action (zoom or pan)
		/// saved by this <see cref="ZoomState" />.
		/// </summary>
		public StateType Type
		{
			get { return type; }
		}

		/// <summary>
		/// Gets a string representing the type of adjustment that was made when this scale
		/// state was saved.
		/// </summary>
		/// <value>A string representation for the state change type; typically
		/// "Pan", "Zoom", or "Scroll".</value>
		public string TypeString
		{
			get
			{
				switch ( type )
				{
					case StateType.Pan:
						return "Pan";
					case StateType.WheelZoom:
						return "WheelZoom";
					case StateType.Zoom:
					default:
						return "Zoom";
					case StateType.Scroll:
						return "Scroll";
				}
			}
		}

		/// <summary>
		/// Construct a <see cref="ZoomState"/> object from the scale ranges settings contained
		/// in the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> from which to obtain the scale
		/// range values.
		/// </param>
		/// <param name="type">A <see cref="StateType"/> enumeration that indicates whether
		/// this saved state is from a pan or zoom.</param>
		public ZoomState( GraphPane pane, StateType type )
		{

			this.xAxis = new ScaleState( pane.XAxis );
			this.yAxis = new ScaleStateList( pane.YAxisList );
			this.y2Axis = new ScaleStateList( pane.Y2AxisList );
			this.type = type;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ZoomState"/> object from which to copy</param>
		public ZoomState( ZoomState rhs )
		{
			this.xAxis = new ScaleState( rhs.xAxis );
			this.yAxis = new ScaleStateList( rhs.yAxis );
			this.y2Axis = new ScaleStateList( rhs.y2Axis );
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
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
		public ZoomState Clone()
		{
			return new ZoomState( this );
		}


		/// <summary>
		/// Copy the properties from this <see cref="ZoomState"/> out to the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> to which the scale range properties should be
		/// copied.</param>
		public void ApplyState( GraphPane pane )
		{
			this.xAxis.ApplyScale( pane.XAxis );
			this.yAxis.ApplyScale( pane.YAxisList );
			this.y2Axis.ApplyScale( pane.Y2AxisList );
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
					this.yAxis.IsChanged( pane.YAxisList ) ||
					this.y2Axis.IsChanged( pane.Y2AxisList );
		}

	}

	/// <summary>
	/// A LIFO stack of prior <see cref="ZoomState"/> objects, used to allow zooming out to prior
	/// states (of scale range settings).
	/// </summary>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.12 $ $Date: 2006-03-27 03:35:43 $ </version>
	public class ZoomStateStack : CollectionBase, ICloneable
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
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
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
		public ZoomStateStack Clone()
		{
			return new ZoomStateStack( this );
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
		/// <param name="type">A <see cref="ZoomState.StateType"/> enumeration that indicates whether this
		/// state is the result of a zoom or pan operation.</param>
		/// <returns>The resultant <see cref="ZoomState"/> object that was pushed on the stack.</returns>
		public ZoomState Push( GraphPane pane, ZoomState.StateType type )
		{
			ZoomState state = new ZoomState( pane, type );
			this.List.Add( state );
			return state;
		}

		/// <summary>
		/// Add the scale range information from the specified <see cref="ZoomState"/> object as a
		/// new <see cref="ZoomState"/> entry on the stack.
		/// </summary>
		/// <param name="state">The <see cref="ZoomState"/> object to be placed on the stack.</param>
		/// <returns>The <see cref="ZoomState"/> object (same as the <see paramref="state"/>
		/// parameter).</returns>
		public ZoomState Push( ZoomState state )
		{
			this.List.Add( state );
			return state;
		}

		/// <summary>
		/// Pop a <see cref="ZoomState"/> entry from the top of the stack, and apply the properties
		/// to the specified <see cref="GraphPane"/> object.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> object to which the scale range
		/// information should be copied.</param>
		/// <returns>The <see cref="ZoomState"/> object that was "popped" from the stack and applied
		/// to the specified <see cref="GraphPane"/>.  null if no <see cref="ZoomState"/> was
		/// available (the stack was empty).</returns>
		public ZoomState Pop( GraphPane pane )
		{
			if ( !this.IsEmpty )
			{
				ZoomState state = (ZoomState) this.List[ this.List.Count - 1 ];
				this.List.RemoveAt( this.List.Count - 1 );

				state.ApplyState( pane );
				return state;
			}
			else
				return null;
		}

		/// <summary>
		/// Pop the <see cref="ZoomState"/> entry from the bottom of the stack, and apply the properties
		/// to the specified <see cref="GraphPane"/> object.  Clear the stack completely.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> object to which the scale range
		/// information should be copied.</param>
		/// <returns>The <see cref="ZoomState"/> object at the bottom of the stack that was applied
		/// to the specified <see cref="GraphPane"/>.  null if no <see cref="ZoomState"/> was
		/// available (the stack was empty).</returns>
		public ZoomState PopAll( GraphPane pane )
		{
			if ( !this.IsEmpty )
			{
				ZoomState state = (ZoomState) this.List[ 0 ];
				this.List.Clear();

				state.ApplyState( pane );

				return state;
			}
			else
				return null;
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
