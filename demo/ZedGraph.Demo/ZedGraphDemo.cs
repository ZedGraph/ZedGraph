//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2005 Jerry Vos
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
using Panel	= System.Windows.Forms;

using ZedGraphControl = ZedGraph.ZedGraphControl;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Interface for demos.  This allows the demos to be self-contained, 
	/// simplifying reading their code and allowing for quick switches 
	/// between demos.
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 1.2 $ $Date: 2005-03-02 19:18:44 $ </version>
	public interface ZedGraphDemo
	{
		/// <summary>
		/// A description of what this demo is showing.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// The demo's title.
		/// </summary>
		string Title { get; }

		/// <summary>
		/// The control used to display this demo.
		/// </summary>
		ZedGraphControl ZedGraphControl { get; }

		/// <summary>
		/// A collection of DemoType objects that this demo applies to.
		/// </summary>
		System.Collections.ICollection Types { get; }
		// string Source { get; }
		// string SourceFileName { get; }
	}
}
