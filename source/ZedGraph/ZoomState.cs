//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright © 2004  John Champion
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
using System.Collections.Generic;
using System.Text;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A class that captures an <see cref="Axis"/> scale range.
	/// </summary>
	/// <remarks>This structure is used by the <see cref="ZoomState"/> class to store
	/// <see cref="Axis"/> scale range settings in a collection for later retrieval.
	/// The class stores the <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
	/// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/> properties, along with
	/// the corresponding auto-scale settings: <see cref="Scale.MinAuto"/>,
	/// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
	/// and <see cref="Scale.MajorStepAuto"/>.</remarks>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.10.2.2 $ $Date: 2006-04-07 06:14:10 $ </version>
	public class ScaleState : ICloneable
	{
		/// <summary>
		/// The axis range data for <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
		/// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/>
		/// </summary>
		private double	_min, _minorStep, _majorStep, _max;
		/// <summary>
		/// The status of <see cref="Scale.MinAuto"/>,
		/// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
		/// and <see cref="Scale.MajorStepAuto"/>
		/// </summary>
		private bool	_minAuto, _minorStepAuto,
							_majorStepAuto, _maxAuto,
							_formatAuto, _magAuto;

		/// <summary>
		/// The status of <see cref="Scale.MajorUnit"/> and <see cref="Scale.MinorUnit"/>
		/// </summary>
		private DateUnit _minorUnit, _majorUnit;

		private string	_format;
		private int		_mag;

		/// <summary>
		/// Construct a <see cref="ScaleState"/> from the specified <see cref="Axis"/>
		/// </summary>
		/// <param name="axis">The <see cref="Axis"/> from which to collect the scale
		/// range settings.</param>
		public ScaleState( Axis axis )
		{
			this._min = axis._scale._min;
			this._minorStep = axis._scale._minorStep;
			this._majorStep = axis._scale._majorStep;
			this._max = axis._scale._max;
			this._majorUnit = axis._scale._majorUnit;
			this._minorUnit = axis._scale._minorUnit;

			this._format = axis._scale._format;
			this._mag = axis._scale._mag;
			//this.numDec = axis.NumDec;

			this._minAuto = axis._scale._minAuto;
			this._majorStepAuto = axis._scale._majorStepAuto;
			this._minorStepAuto = axis._scale._minorStepAuto;
			this._maxAuto = axis._scale._maxAuto;

			this._formatAuto = axis._scale._formatAuto;
			this._magAuto = axis._scale._magAuto;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ScaleState"/> object from which to copy</param>
		public ScaleState( ScaleState rhs )
		{
			this._min = rhs._min;
			this._majorStep = rhs._majorStep;
			this._minorStep = rhs._minorStep;
			this._max = rhs._max;
			this._majorUnit = rhs._majorUnit;
			this._minorUnit = rhs._minorUnit;

			this._format = rhs._format;
			this._mag = rhs._mag;

			this._minAuto = rhs._minAuto;
			this._majorStepAuto = rhs._majorStepAuto;
			this._minorStepAuto = rhs._minorStepAuto;
			this._maxAuto = rhs._maxAuto;

			this._formatAuto = rhs._formatAuto;
			this._magAuto = rhs._magAuto;
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
			axis._scale._min = this._min;
			axis._scale._majorStep = this._majorStep;
			axis._scale._minorStep = this._minorStep;
			axis._scale._max = this._max;
			axis._scale._majorUnit = this._majorUnit;
			axis._scale._minorUnit = this._minorUnit;

			axis._scale._format = this._format;
			axis._scale._mag = this._mag;

			// The auto settings must be made after the min/step/max settings, since setting those
			// properties actually affects the auto settings.
			axis._scale._minAuto = this._minAuto;
			axis._scale._minorStepAuto = this._minorStepAuto;
			axis._scale._majorStepAuto = this._majorStepAuto;
			axis._scale._maxAuto = this._maxAuto;

			axis._scale._formatAuto = this._formatAuto;
			axis._scale._magAuto = this._magAuto;

		}

		/// <summary>
		/// Determine if the state contained in this <see cref="ScaleState"/> object is different from
		/// the state of the specified <see cref="Axis"/>.
		/// </summary>
		/// <param name="axis">The <see cref="Axis"/> object with which to compare states.</param>
		/// <returns>true if the states are different, false otherwise</returns>
		public bool IsChanged( Axis axis )
		{
			return axis._scale._min != this._min ||
					axis._scale._majorStep != this._majorStep ||
					axis._scale._minorStep != this._minorStep ||
					axis._scale._max != this._max ||
					axis._scale._minorUnit != this._minorUnit ||
					axis._scale._majorUnit != this._majorUnit ||
					axis._scale._minAuto != this._minAuto ||
					axis._scale._minorStepAuto != this._minorStepAuto ||
					axis._scale._majorStepAuto != this._majorStepAuto ||
					axis._scale._maxAuto != this._maxAuto;
		}

	}

	/// <summary>
	/// A collection class that maintains a list of <see cref="ScaleState" />
	/// objects, corresponding to the list of <see cref="Axis" /> objects
	/// from <see cref="GraphPane.YAxisList" /> or <see cref="GraphPane.Y2AxisList" />.
	/// </summary>
	public class ScaleStateList : List<ScaleState>, ICloneable
	{
		/// <summary>
		/// Construct a new <see cref="ScaleStateList" /> automatically from an
		/// existing <see cref="YAxisList" />.
		/// </summary>
		/// <param name="list">The <see cref="YAxisList" /> (a list of Y axes),
		/// from which to retrieve the state and create the <see cref="ScaleState" />
		/// objects.</param>
		public ScaleStateList( YAxisList list )
		{
			foreach ( Axis axis in list )
				this.Add( new ScaleState( axis ) );
		}

		/// <summary>
		/// Construct a new <see cref="ScaleStateList" /> automatically from an
		/// existing <see cref="Y2AxisList" />.
		/// </summary>
		/// <param name="list">The <see cref="Y2AxisList" /> (a list of Y axes),
		/// from which to retrieve the state and create the <see cref="ScaleState" />
		/// objects.</param>
		public ScaleStateList( Y2AxisList list )
		{
			foreach ( Axis axis in list )
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
		/// to the state of the specified <see cref="YAxisList" /> <see cref="Axis" />
		/// objects.
		/// </summary>
		/// <param name="list">A <see cref="YAxisList" /> object specifying a list of
		/// <see cref="Axis" /> objects to be compared with this <see cref="ScaleStateList" />.
		/// </param>
		/// <returns>true if a difference is found, false otherwise</returns>
		public bool IsChanged( YAxisList list )
		{
			int count = Math.Min( list.Count, this.Count );
			for ( int i = 0; i < count; i++ )
				if ( this[i].IsChanged( list[i] ) )
					return true;

			return false;
		}

		/// <summary>
		/// Iterate through the list of <see cref="ScaleState" /> objects, comparing them
		/// to the state of the specified <see cref="Y2AxisList" /> <see cref="Axis" />
		/// objects.
		/// </summary>
		/// <param name="list">A <see cref="Y2AxisList" /> object specifying a list of
		/// <see cref="Axis" /> objects to be compared with this <see cref="ScaleStateList" />.
		/// </param>
		/// <returns>true if a difference is found, false otherwise</returns>
		public bool IsChanged( Y2AxisList list )
		{
			int count = Math.Min( list.Count, this.Count );
			for ( int i = 0; i < count; i++ )
				if ( this[i].IsChanged( list[i] ) )
					return true;

			return false;
		}
		/*
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
		*/

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		public void ApplyScale( YAxisList list )
		{
			int count = Math.Min( list.Count, this.Count );
			for ( int i = 0; i < count; i++ )
				this[i].ApplyScale( list[i] );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		public void ApplyScale( Y2AxisList list )
		{
			int count = Math.Min( list.Count, this.Count );
			for ( int i = 0; i < count; i++ )
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
	/// <version> $Revision: 3.10.2.2 $ $Date: 2006-04-07 06:14:10 $ </version>
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
		private ScaleState	_xAxis;
		private ScaleStateList _yAxis, _y2Axis;
		/// <summary>
		/// An enum value indicating the type of adjustment being made to the
		/// scale range state.
		/// </summary>
		private StateType	_type;

		/// <summary>
		/// Gets a <see cref="StateType" /> value indicating the type of action (zoom or pan)
		/// saved by this <see cref="ZoomState" />.
		/// </summary>
		public StateType Type
		{
			get { return _type; }
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
				switch ( _type )
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

			this._xAxis = new ScaleState( pane.XAxis );
			this._yAxis = new ScaleStateList( pane.YAxisList );
			this._y2Axis = new ScaleStateList( pane.Y2AxisList );
			this._type = type;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ZoomState"/> object from which to copy</param>
		public ZoomState( ZoomState rhs )
		{
			this._xAxis = new ScaleState( rhs._xAxis );
			this._yAxis = new ScaleStateList( rhs._yAxis );
			this._y2Axis = new ScaleStateList( rhs._y2Axis );
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
			this._xAxis.ApplyScale( pane.XAxis );
			this._yAxis.ApplyScale( pane.YAxisList );
			this._y2Axis.ApplyScale( pane.Y2AxisList );
		}

		/// <summary>
		/// Determine if the state contained in this <see cref="ZoomState"/> object is different from
		/// the state of the specified <see cref="GraphPane"/>.
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane"/> object with which to compare states.</param>
		/// <returns>true if the states are different, false otherwise</returns>
		public bool IsChanged( GraphPane pane )
		{
			return	this._xAxis.IsChanged( pane.XAxis ) ||
					this._yAxis.IsChanged( pane.YAxisList ) ||
					this._y2Axis.IsChanged( pane.Y2AxisList );
		}

	}

	/// <summary>
	/// A LIFO stack of prior <see cref="ZoomState"/> objects, used to allow zooming out to prior
	/// states (of scale range settings).
	/// </summary>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.10.2.2 $ $Date: 2006-04-07 06:14:10 $ </version>
	public class ZoomStateStack : List<ZoomState>, ICloneable
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
				Add( new ZoomState( state ) );
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
			get { return this.Count == 0; }
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
			this.Add( state );
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
			this.Add( state );
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
				ZoomState state = (ZoomState) this[ this.Count - 1 ];
				this.RemoveAt( this.Count - 1 );

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
				ZoomState state = (ZoomState) this[ 0 ];
				this.Clear();

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
					return (ZoomState) this[ this.Count - 1 ];
				else
					return null;
			}
		}
	}
}
