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
	/// A collection class containing a list of <see cref="YAxis"/> objects.
	/// </summary>
	/// 
	/// <author>John Champion</author>
	/// <version> $Revision: 3.2 $ $Date: 2006-02-14 06:14:22 $ </version>
	[Serializable]
	public class YAxisList : AxisList, ICloneable
	{

	#region Constructors

		/// <summary>
		/// Default constructor for the collection class.
		/// </summary>
		public YAxisList()
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="YAxisList"/> object from which to copy</param>
		public YAxisList( YAxisList rhs )
		{
			foreach ( YAxis item in rhs )
			{
				this.Add( new YAxis( item ) );
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
		public YAxisList Clone()
		{
			return new YAxisList( this );
		}

	#endregion

	#region List Methods

		/// <summary>
		/// Add a <see cref="YAxis"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="axis">A reference to the <see cref="YAxis"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public int Add( YAxis axis )
		{
			return List.Add( axis );
		}

		/// <summary>
		/// Insert a <see cref="YAxis"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="axis">A reference to the <see cref="YAxis"/> object that is to be
		/// inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, YAxis axis )
		{
			List.Insert( index, axis );
		}

	#endregion

	}
}
