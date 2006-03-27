//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005  John Champion
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
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
	/// <summary>
	/// A collection class containing a list of <see cref="Axis"/> objects.
	/// </summary>
	/// <remarks>
	/// This abstract base class is inherited by <see cref="YAxisList" /> and
	/// <see cref="Y2AxisList" />
	/// </remarks>
	/// 
	/// <author>John Champion</author>
	/// <version> $Revision: 3.4 $ $Date: 2006-03-27 03:35:43 $ </version>
	[Serializable]
	abstract public class AxisList : CollectionPlus
	{

	#region Constructors

		/// <summary>
		/// Default constructor for the collection class.
		/// </summary>
		public AxisList()
		{
		}

	#endregion

	#region List Methods

		/// <summary>
		/// Indexer to access the specified <see cref="Axis"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="Axis"/> object to be accessed.</param>
		/// <value>An <see cref="Axis"/> object reference.</value>
		public Axis this[ int index ]  
		{
			get { return( (Axis) ( ( index < 0 || index >= this.Count ) ? null : List[index] ) ); }
		}

		/// <summary>
		/// Indexer to access the specified <see cref="Axis"/> object by
		/// its <see cref="Axis.Title"/> string.
		/// </summary>
		/// <param name="title">The string title of the
		/// <see cref="Axis"/> object to be accessed.</param>
		/// <value>A <see cref="Axis"/> object reference.</value>
		public Axis this[ string title ]  
		{
			get
			{
				int index = IndexOf( title );
				if ( index >= 0 )
					return( (Axis) List[index]  );
				else
					return null;
			}
		}

		/// <summary>
		/// Remove a <see cref="Axis"/> object from the collection based on an object reference.
		/// </summary>
		/// <seealso cref="IList.Remove"/>
		public void Remove( Axis axis )
		{
			List.Remove( axis );
		}

		/// <summary>
		/// Return the zero-based position index of the
		/// <see cref="Axis"/> with the specified <see cref="Axis.Title"/>.
		/// </summary>
		/// <remarks>The comparison of titles is not case sensitive, but it must include
		/// all characters including punctuation, spaces, etc.</remarks>
		/// <param name="title">The <see cref="String"/> label that is in the
		/// <see cref="Axis.Title"/> attribute of the item to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="Axis"/>,
		/// or -1 if the <see cref="Axis.Title"/> was not found in the list</returns>
		/// <seealso cref="IList.IndexOf"/>
		/// <seealso cref="IndexOfTag"/>
		public int IndexOf( string title )
		{
			int index = 0;
			foreach ( Axis axis in this )
			{
				if ( String.Compare( axis.Title, title, true ) == 0 )
					return index;
				index++;
			}

			return -1;
		}

		/// <summary>
		/// Return the zero-based position index of the
		/// <see cref="Axis"/> with the specified <see cref="Axis.Tag" />.
		/// </summary>
		/// <remarks>In order for this method to work, the <see cref="Axis.Tag" />
		/// property must be of type <see cref="String"/>.</remarks>
		/// <param name="tagStr">The <see cref="String"/> tag that is in the
		/// <see cref="Axis.Tag" /> attribute of the item to be found.
		/// </param>
		/// <returns>The zero-based index of the specified <see cref="Axis" />,
		/// or -1 if the <see cref="Axis.Tag" /> string is not in the list</returns>
		/// <seealso cref="IList.IndexOf" />
		/// <seealso cref="IndexOf" />
		public int IndexOfTag( string tagStr )
		{
			int index = 0;
			foreach ( Axis axis in this )
			{
				if ( axis.Tag is string &&
					String.Compare( (string) axis.Tag, tagStr, true ) == 0 )
					return index;
				index++;
			}

			return -1;
		}

	#endregion

	}
}
