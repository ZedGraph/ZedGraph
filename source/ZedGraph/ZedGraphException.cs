using System;

namespace ZedGraph
{
	/// <summary>
	/// An exception thrown by ZedGraph.  A child class of <see cref="ApplicationException"/>.
	/// </summary>
	///
	/// <author> Jerry Vos modified by John Champion</author>
	/// <version> $Revision: 2.0 $ $Date: 2004-09-02 06:24:59 $ </version>
	public class ZedGraphException : System.ApplicationException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ZedGraphException"/>
		/// class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/>
		/// instance that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/>
		/// instance that contains contextual information about the source or destination.</param>
		protected ZedGraphException( System.Runtime.Serialization.SerializationInfo info, 
										System.Runtime.Serialization.StreamingContext context )
			: base ( info, context )
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class with a specified
		/// error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.
		/// If the innerException parameter is not a null reference, the current exception is raised
		/// in a catch block that handles the inner exception.</param>
		public ZedGraphException( System.String message, System.Exception innerException )
			: base ( message, innerException )
		{

		}
	
		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public ZedGraphException ( System.String message ) 
			: base( message )
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class.
		/// </summary>
		public ZedGraphException() 
			: base()
		{
		}
	}
}
