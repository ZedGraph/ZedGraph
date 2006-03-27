//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2005 John Champion and Jerry Vos
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
using ICollection	= System.Collections.ICollection;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// An implementation of the things necessary for most ZedGraphDemos.
	/// </summary>
	public abstract class DemoBase : ZedGraphDemo
	{
		protected string description;
		protected string title;
		protected ICollection types;
		
		private ZedGraphControl control;

		public DemoBase( string description, string title, DemoType type )
		{
			ArrayList types = new ArrayList();
			types.Add( type );

			Init( description, title, types );
		}
		
		public DemoBase( string description, string title, DemoType type, DemoType type2 )
		{
			ArrayList types = new ArrayList();
			types.Add( type );
			types.Add( type2 );

			Init( description, title, types );
		}
		
		public DemoBase( string description, string title, ICollection types ) 
		{
			Init( description, title, types );
		}
		
		private void Init( string description, string title, ICollection types )
		{
			this.description = description;
			this.title = title;
			this.types = types;

			control = new ZedGraphControl();
		}

		#region ZedGraphDemo Members

		/// <summary>
		/// The graph pane the chart is show in.
		/// </summary>
		public PaneBase Pane { get { return control.GraphPane; } }
		
		/// <summary>
		/// The graph pane the chart is show in.
		/// </summary>
		public MasterPane MasterPane { get { return control.MasterPane; } }

		/// <summary>
		/// The graph pane the chart is show in (same as .Pane).
		/// </summary>
		public GraphPane GraphPane { get { return control.GraphPane; } }

		public string Description { get { return description; } }
		
		public string Title { get { return title; } }

		public ICollection Types { get { return types; } }
		
		/// <summary>
		/// The control the graph pane is in.
		/// </summary>
		public ZedGraphControl ZedGraphControl
		{
			get { return control; }
		}

		#endregion

	}
}
