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
		
		//private GraphPane graphPane;
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

			//graphPane	= new GraphPane();
			control = new ZedGraphControl();
			//control.GraphPane = graphPane;
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
