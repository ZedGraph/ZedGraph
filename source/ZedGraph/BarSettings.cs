//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2006  John Champion
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
using System.Drawing;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// Class that handles the global settings for bar charts
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.1.2.1 $ $Date: 2006-03-27 01:31:37 $ </version>
	public class BarSettings : ICloneable, ISerializable
	{
	#region Fields

		/// <summary>Private field that determines the size of the gap between bar clusters
		/// for bar charts.  This gap is expressed as a fraction of the bar size (1.0 means
		/// leave a 1-barwidth gap between clusters).
		/// Use the public property <see cref="MinClusterGap"/> to access this value. </summary>
		private float _minClusterGap;
		/// <summary>Private field that determines the size of the gap between individual bars
		/// within a bar cluster for bar charts.  This gap is expressed as a fraction of the
		/// bar size (1.0 means leave a 1-barwidth gap between each bar).
		/// Use the public property <see cref="MinBarGap"/> to access this value. </summary>
		private float _minBarGap;
		/// <summary>Private field that determines the base axis from which <see cref="Bar"/>
		/// graphs will be displayed.  The base axis is the axis from which the bars grow with
		/// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
		/// To access this value, use the public property <see cref="Base"/>.
		/// </summary>
		/// <seealso cref="Default.Base"/>
		private BarBase _base;
		/// <summary>Private field that determines how the <see cref="BarItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
		/// for the individual types available.
		/// To access this value, use the public property <see cref="Type"/>.
		/// </summary>
		/// <seealso cref="Default.Type"/>
		private BarType _type;
		/// <summary>Private field that determines the width of a bar cluster (for bar charts)
		/// in user scale units.  Normally, this value is 1.0 because bar charts are typically
		/// <see cref="AxisType.Ordinal"/> or <see cref="AxisType.Text"/>, and the bars are
		/// defined at ordinal values (1.0 scale units apart).  For <see cref="AxisType.Linear"/>
		/// or other scale types, you can use this value to scale the bars to an arbitrary
		/// user scale. Use the public property <see cref="ClusterScaleWidth"/> to access this
		/// value. </summary>
		private double _clusterScaleWidth;

	#endregion

	#region Constructors

		/// <summary>
		/// Constructor to build a <see cref="BarSettings" /> instance from the defaults.
		/// </summary>
		public BarSettings()
		{
			this._minClusterGap = Default.MinClusterGap;
			this._minBarGap = Default.MinBarGap;
			this._clusterScaleWidth = Default.ClusterScaleWidth;
			this._base = Default.Base;
			this._type = Default.Type;

		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="rhs">the <see cref="BarSettings" /> instance to be copied.</param>
		public BarSettings( BarSettings rhs )
		{
			this._minClusterGap = rhs._minClusterGap;
			this._minBarGap = rhs._minBarGap;
			this._clusterScaleWidth = rhs._clusterScaleWidth;
			this._base = rhs._base;
			this._type = rhs._type;
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
		public BarSettings Clone()
		{
			return new BarSettings( this );
		}

	#endregion

	#region Bar Properties

		/// <summary>
		/// The minimum space between <see cref="Bar"/> clusters, expressed as a
		/// fraction of the bar size.
		/// </summary>
		/// <seealso cref="Default.MinClusterGap"/>
		/// <seealso cref="MinBarGap"/>
		/// <seealso cref="ClusterScaleWidth"/>
		public float MinClusterGap
		{
			get { return _minClusterGap; }
			set { _minClusterGap = value; }
		}
		/// <summary>
		/// The minimum space between individual <see cref="Bar">Bars</see>
		/// within a cluster, expressed as a
		/// fraction of the bar size.
		/// </summary>
		/// <seealso cref="Default.MinBarGap"/>
		/// <seealso cref="MinClusterGap"/>
		/// <seealso cref="ClusterScaleWidth"/>
		public float MinBarGap
		{
			get { return _minBarGap; }
			set { _minBarGap = value; }
		}
		/// <summary>Determines the base axis from which <see cref="Bar"/>
		/// graphs will be displayed.
		/// </summary>
		/// <remarks>The base axis is the axis from which the bars grow with
		/// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
		/// </remarks>
		/// <seealso cref="Default.Base"/>
		public BarBase Base
		{
			get { return _base; }
			set { _base = value; }
		}
		/// <summary>Determines how the <see cref="BarItem"/>
		/// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
		/// for the individual types available.
		/// </summary>
		/// <seealso cref="Default.Type"/>
		public BarType Type
		{
			get { return _type; }
			set { _type = value; }
		}
		/// <summary>
		/// The width of an individual bar cluster on a <see cref="Bar"/> graph.
		/// This value only applies to bar graphs plotted on non-ordinal X axis
		/// types (<see cref="AxisType.Linear"/>, <see cref="AxisType.Log"/>, and
		/// <see cref="AxisType.Date"/>.
		/// </summary>
		/// <seealso cref="Default.ClusterScaleWidth"/>
		/// <seealso cref="MinBarGap"/>
		/// <seealso cref="MinClusterGap"/>
		public double ClusterScaleWidth
		{
			get { return _clusterScaleWidth; }
			set { _clusterScaleWidth = value; }
		}
	#endregion

	#region Serialization

		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected BarSettings( SerializationInfo info, StreamingContext context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			this._minClusterGap = info.GetSingle( "minClusterGap" );
			this._minBarGap = info.GetSingle( "minBarGap" );
			this._clusterScaleWidth = info.GetDouble( "clusterScaleWidth" );
			this._base = (BarBase)info.GetValue( "base", typeof(BarBase) );
			this._type = (BarType)info.GetValue( "type", typeof( BarType ) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute( SecurityAction.Demand, SerializationFormatter = true )]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "schema", schema );

			info.AddValue( "minClusterGap", _minClusterGap );
			info.AddValue( "minBarGap", _minBarGap );
			info.AddValue( "clusterScaleWidth", _clusterScaleWidth );
			info.AddValue( "base", _base );
			info.AddValue( "type", _type );
		}

	#endregion


	#region Defaults

		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="BarSettings"/> class.
		/// </summary>
		public struct Default
		{
			/// <summary>
			/// The default dimension gap between clusters of bars on a
			/// <see cref="Bar"/> graph.
			/// This dimension is expressed in terms of the normal bar width.
			/// </summary>
			/// <seealso cref="Default.MinBarGap"/>
			/// <seealso cref="BarSettings.MinClusterGap"/>
			public static float MinClusterGap = 1.0F;
			/// <summary>
			/// The default dimension gap between each individual bar within a bar cluster
			/// on a <see cref="Bar"/> graph.
			/// This dimension is expressed in terms of the normal bar width.
			/// </summary>
			/// <seealso cref="Default.MinClusterGap"/>
			/// <seealso cref="BarSettings.MinBarGap"/>
			public static float MinBarGap = 0.2F;
			/// <summary>The default value for the <see cref="BarSettings.Base"/>, which determines the base
			/// <see cref="Axis"/> from which the <see cref="Bar"/> graphs will be displayed.
			/// </summary>
			/// <seealso cref="BarSettings.Base"/>
			public static BarBase Base = BarBase.X;
			/// <summary>The default value for the <see cref="BarSettings.Type"/> property, which
			/// determines if the bars are drawn overlapping eachother in a "stacked" format,
			/// or side-by-side in a "cluster" format.  See the <see cref="ZedGraph.BarType"/>
			/// for more information.
			/// </summary>
			/// <seealso cref="BarSettings.Type"/>
			public static BarType Type = BarType.Cluster;

			/// <summary>
			/// The default width of a bar cluster 
			/// on a <see cref="Bar"/> graph.  This value only applies to
			/// <see cref="Bar"/> graphs, and only when the
			/// <see cref="Axis.Type"/> is <see cref="AxisType.Linear"/>,
			/// <see cref="AxisType.Log"/> or <see cref="AxisType.Date"/>.
			/// This dimension is expressed in terms of X scale user units.
			/// </summary>
			/// <seealso cref="Default.MinClusterGap"/>
			/// <seealso cref="BarSettings.MinBarGap"/>
			public static double ClusterScaleWidth = 1.0;
		}
	#endregion


	}
}
