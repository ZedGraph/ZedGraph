using System;

namespace ZedGraph
{
	/// <summary>
	/// An exception thrown by ZedGraph.
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 1.1 $ $Date: 2004-08-30 17:37:45 $ </version>
	public class ZedGraphException : System.ApplicationException
	{
		protected ZedGraphException( System.Runtime.Serialization.SerializationInfo info, 
										System.Runtime.Serialization.StreamingContext context )
			: base ( info, context )
		{

		}
		
		public ZedGraphException( System.String message, System.Exception innerException )
			: base ( message, innerException )
		{

		}
	
		public ZedGraphException ( System.String message ) 
			: base( message )
		{
		}
		
		public ZedGraphException() 
			: base()
		{
		}
	}
}
