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
	/// A class that captures all the scale range settings for a group of
	/// <see cref="GraphPane"/> objects contained within a <see cref="MasterPane" />.
	/// </summary>
	/// <remarks>
	/// This class is used to store scale ranges in order to allow zooming out to
	/// prior scale range states.  The <see cref="ZoomStateGroup" /> object inherits from
	/// <see cref="ZoomState" />, which is made for a single <see cref="GraphPane" />.
	/// <see cref="ZoomState"/> objects are maintained in the
	/// <see cref="ZoomStateStack"/> collection.  The <see cref="ZoomState"/> object holds
	/// a <see cref="ScaleState"/> object for the <see cref="XAxis"/>, and
	/// <see cref="ScaleStateList" /> objects for
	/// the <see cref="YAxis"/>, and the <see cref="Y2Axis"/>.
	/// </remarks>
	/// <author> John Champion </author>
	/// <version> $Revision: 3.1 $ $Date: 2006-06-24 20:26:44 $ </version>
	public class ZoomStateGroup : ZoomState, ICloneable
	{
		private ZoomStateStack _stack;

		/// <summary>
		/// Construct a <see cref="ZoomStateGroup"/> object from the scale ranges settings contained
		/// in each <see cref="GraphPane" /> for the specified <see cref="MasterPane"/>.
		/// </summary>
		/// <param name="masterPane">The <see cref="MasterPane"/> from which to obtain the scale
		/// range values.
		/// </param>
		/// <param name="type">A <see cref="ZoomState.StateType"/> enumeration that indicates whether
		/// this saved state is from a pan, zoom, or scroll.</param>
		public ZoomStateGroup( MasterPane masterPane, StateType type ) : base( type )
		{
			_stack = new ZoomStateStack();

			foreach ( GraphPane pane in masterPane._paneList )
				_stack.Add( new ZoomState( pane, type ) );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ZoomStateGroup"/> object from which to copy</param>
		public ZoomStateGroup( ZoomStateGroup rhs ) : base ( rhs.Type )
		{
			_stack = new ZoomStateStack( rhs._stack );
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
		new public ZoomStateGroup Clone()
		{
			return new ZoomStateGroup( this );
		}

		/// <summary>
		/// Since this is a ZoomStateGroup, it should never be applied to a single GraphPane.
		/// This should never happen, but just make sure it doesn't.
		/// </summary>
		/// <param name="pane">The GraphPane of interest.</param>
		override public void ApplyState( GraphPane pane )
		{
			throw new NotImplementedException( "Error: Can't apply a ZoomStateGroup to a single pane" );
		}

		/// <summary>
		/// Copy the properties from this <see cref="ZoomStateGroup"/> out to the specified
		/// <see cref="MasterPane"/>.
		/// </summary>
		/// <param name="masterPane">The <see cref="MasterPane"/> to which the scale range
		/// properties should be copied.
		/// </param>
		public void ApplyState( MasterPane masterPane )
		{
			for ( int i = 0; i < _stack.Count; i++ )
			{
				if ( masterPane._paneList.Count > i )
					_stack[i].ApplyState( masterPane._paneList[i] );
			}
		}

		/// <summary>
		/// Determine if the state contained in this <see cref="ZoomStateGroup"/> object is different from
		/// the state of the specified <see cref="MasterPane"/>.
		/// </summary>
		/// <param name="masterPane">The <see cref="MasterPane"/> object with which to compare states.</param>
		/// <returns>true if the states are different, false otherwise</returns>
		public bool IsChanged( MasterPane masterPane )
		{
			for ( int i = 0; i < _stack.Count; i++ )
			{
				if ( masterPane._paneList.Count > i && _stack[i].IsChanged( masterPane._paneList[i] ) )
					return true;
			}

			return false;
		}

	}
}
