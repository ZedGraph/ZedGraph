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
