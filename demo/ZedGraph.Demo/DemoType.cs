using System;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Types of demos, these are used by ZedGraphDemos to describe what kind
	/// of demo they are.<p/>
	/// 
	/// For new types to work in the ChartTabForm the name of the type has to 
	/// be added to ChartTabForm.TypeToName(...)
	/// </summary>
	public enum DemoType
	{
		General,
		Bar,
		Line,
		Pie,
		Special,
		Tutorial
	}
}
