using System;
using System.Collections;
using System.Text;

namespace ZedGraph
{
	/// <summary>
	/// enumeration used to indicate which type of data will be plotted.
	/// </summary>
	public enum SampleType { Time, Position, VelocityInst, TimeDiff, VelocityAvg };

	public class Sample
	{
		public DateTime	Time;
		public double		Position;
		public double		Velocity;
	}

	public class SamplePointList : IPointList
	{
		// Determines what data type gets plotted for the X values
		public SampleType XType;
		// Determines what data type gets plotted for the Y values
		public SampleType YType;

		// Stores the collection of samples
		private ArrayList list;

		// Indexer: get the Sample instance at the specified ordinal position in the list
		public PointPair this[int index]
		{
			get
			{
				PointPair pt = new PointPair();
				Sample sample = (Sample) list[index];
				pt.X = GetValue( sample, XType );
				pt.Y = GetValue( sample, YType );
				return pt;
			}
		}

		public int Count
		{
			get { return list.Count; }
		}

		// Get the specified data type from the specified sample
		public double GetValue( Sample sample, SampleType type )
		{
			switch ( type )
			{
				case SampleType.Position:
					return sample.Position;
				case SampleType.Time:
					return sample.Time.ToOADate();
				case SampleType.TimeDiff:
					return sample.Time.ToOADate() - ( (Sample)list[0] ).Time.ToOADate();
				case SampleType.VelocityAvg:
					double timeDiff = sample.Time.ToOADate() - ( (Sample)list[0] ).Time.ToOADate();
					if ( timeDiff <= 0 )
						return PointPair.Missing;
					else
						return ( sample.Position - ( (Sample)list[0] ).Position ) / timeDiff;
				case SampleType.VelocityInst:
					return sample.Velocity;
				default:
					return PointPair.Missing;
			}
		}

		// Add a sample to the collection
		public int Add( Sample sample )
		{
			return list.Add( sample );
		}

		// generic Clone: just call the typesafe version
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// typesafe clone method
		public SamplePointList Clone()
		{
			return new SamplePointList( this );
		}

		// default constructor
		public SamplePointList()
		{
			XType = SampleType.Time;
			YType = SampleType.Position;
			list = new ArrayList();
		}

		// copy constructor
		public SamplePointList( SamplePointList rhs )
		{
			XType = rhs.XType;
			YType = rhs.YType;

			// Don't duplicate the data values, just copy the reference to the ArrayList
			this.list = rhs.list;

			//foreach ( Sample sample in rhs )
			//	list.Add( sample );
		}

	}
}
