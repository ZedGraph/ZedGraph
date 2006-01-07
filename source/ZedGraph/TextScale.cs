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

using System;
using System.Collections;
using System.Text;
using System.Drawing;

namespace ZedGraph
{
	/// <summary>
	/// The TextScale class inherits from the <see cref="Scale" /> class, and implements
	/// the features specific to <see cref="AxisType.Text" />.
	/// </summary>
	/// <remarks>
	/// TextScale is an ordinal axis with user-defined text labels.  An ordinal axis means that
	/// all data points are evenly spaced at integral values, and the actual coordinate values
	/// for points corresponding to that axis are ignored.  That is, if the X axis is an
	/// ordinal type, then all X values associated with the curves are ignored.
	/// </remarks>
	/// 
	/// <author> John Champion  </author>
	/// <version> $Revision: 1.2 $ $Date: 2006-01-07 19:15:15 $ </version>
	class TextScale : Scale
	{


	#region constructors

		public TextScale( Axis parentAxis )
			: base( parentAxis )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="TextScale" /> object from which to copy</param>
		public TextScale( Scale rhs )
			: base( rhs )
		{
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="TextScale" /> object.</returns>
		public object Clone()
		{
			return new TextScale( this ); 
		}

	#endregion

	#region properties

		public override AxisType Type
		{
			get { return AxisType.Text; }
		}

	#endregion

	#region methods

		/// <summary>
		/// Internal routine to determine the ordinals of the first minor tic mark
		/// </summary>
		/// <param name="baseVal">
		/// The value of the first major tic for the axis.
		/// </param>
		/// <returns>
		/// The ordinal position of the first minor tic, relative to the first major tic.
		/// This value can be negative (e.g., -3 means the first minor tic is 3 minor step
		/// increments before the first major tic.
		/// </returns>
		override internal int CalcMinorStart( double baseVal )
		{
			// This should never happen (no minor tics for text labels)
			return 0;
		}

		/// <summary>
		/// Determine the value for the first major tic.
		/// </summary>
		/// <remarks>
		/// This is done by finding the first possible value that is an integral multiple of
		/// the step size, taking into account the date/time units if appropriate.
		/// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <returns>
		/// First major tic value (floating point double).
		/// </returns>
		override internal double CalcBaseTic()
		{
			if ( this.baseTic != PointPair.Missing )
				return this.baseTic;
			else
				return 1.0;

		}
		
		/// <summary>
		/// Internal routine to determine the ordinals of the first and last major axis label.
		/// </summary>
		/// <returns>
		/// This is the total number of major tics for this axis.
		/// </returns>
		override internal int CalcNumTics()
		{
			int nTics = 1;

			// If no array of labels is available, just assume 10 labels so we don't blow up.
			if ( this.textLabels == null )
				nTics = 10;
			else
				nTics = this.textLabels.Length;

			if ( nTics < 1 )
				nTics = 1;
			else if ( nTics > 500 )
				nTics = 500;

			return nTics;
		}

		/// <summary>
		/// Select a reasonable text axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Text"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  This is an ordinal
		/// type, such that the labeled values start at 1.0 and increment by 1.0 for
		/// each successive label.  The maximum number of labels on the graph is
		/// determined by <see cref="Scale.Default.MaxTextLabels"/>.  If necessary, this method will
		/// set the <see cref="Scale.Step"/> value to greater than 1.0 in order to keep the total
		/// labels displayed below <see cref="Scale.Default.MaxTextLabels"/>.  For example, a
		/// <see cref="Scale.Step"/> size of 2.0 would only display every other label on the
		/// axis.  The <see cref="Scale.Step"/> value calculated by this routine is always
		/// an integral value.  This
		/// method honors the <see cref="Scale.MinAuto"/>, <see cref="Scale.MaxAuto"/>,
		/// and <see cref="Scale.StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Scale.Min"/>, <see cref="Scale.Max"/>, or <see cref="Scale.Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.
		/// <para>On Exit:</para>
		/// <para><see cref="Scale.Min"/> is set to scale minimum (if <see cref="Scale.MinAuto"/> = true)</para>
		/// <para><see cref="Scale.Max"/> is set to scale maximum (if <see cref="Scale.MaxAuto"/> = true)</para>
		/// <para><see cref="Scale.Step"/> is set to scale step size (if <see cref="Scale.StepAuto"/> = true)</para>
		/// <para><see cref="Scale.MinorStep"/> is set to scale minor step size (if <see cref="Scale.MinorStepAuto"/> = true)</para>
		/// <para><see cref="Scale.ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="Scale.ScaleFormat"/> is set to the display format for the values (this controls the
		/// number of decimal places, whether there are thousands separators, currency types, etc.)</para>
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Text"/>
		override public void PickScale( GraphPane pane, Graphics g, float scaleFactor )
		{
			// call the base class first
			base.PickScale( pane, g, scaleFactor );

			// if text labels are provided, then autorange to the number of labels
			if ( this.textLabels != null )
			{
				if ( this.minAuto )
					this.min = 0.5;
				if ( this.maxAuto )
					this.max = this.textLabels.Length + 0.5;
			}
			else
			{
				if ( this.minAuto )
					this.min -= 0.5;
				if ( this.maxAuto )
					this.max += 0.5;
			}
			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < .1 )
			{
				if ( this.maxAuto )
					this.max = this.min + 10.0;
				else
					this.min = this.max - 10.0;
			}

			if ( this.stepAuto )
			{
				if ( !this.isPreventLabelOverlap )
				{
					this.step = 1;
				}
				else if ( this.textLabels != null )
				{
					// Calculate the maximum number of labels
					double maxLabels = (double) this.CalcMaxLabels( g, pane, scaleFactor );

					// Calculate a step size based on the width of the labels
					double tmpStep = Math.Ceiling( ( this.max - this.min ) / maxLabels );

					// Use the lesser of the two step sizes
					//if ( tmpStep < this.step )
					this.step = tmpStep;
				}
				else
					this.step = (int) ( ( this.max - this.min - 1.0 ) / Default.MaxTextLabels ) + 1.0;

			}
			else
			{
				this.step = (int) this.step;
				if ( this.step <= 0 )
					this.step = 1.0;
			}

			if ( this.minorStepAuto )
				this.minorStep = 1;
			//this.numDec = 0;
			this.scaleMag = 0;
		}

		/// <summary>
		/// Make a value label for an <see cref="AxisType.Text" /> <see cref="Axis" />.
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="index">
		/// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
		/// cause the third value label on the axis to be generated.
		/// </param>
		/// <param name="dVal">
		/// The numeric value associated with the label.  This value is ignored for log (<see cref="Axis.IsLog"/>)
		/// and text (<see cref="Axis.IsText"/>) type axes.
		/// </param>
		/// <param name="label">
		/// Output only.  The resulting value label.
		/// </param>
		override internal void MakeLabel( GraphPane pane, int index, double dVal, out string label )
		{
			if ( this.scaleFormat == null )
				this.scaleFormat = Scale.Default.ScaleFormat;

			index *= (int) this.step;
			if ( this.textLabels == null || index < 0 || index >= textLabels.Length )
				label = "";
			else
				label = textLabels[index];
		}


	#endregion

	}
}
