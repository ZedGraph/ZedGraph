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
	/// The ExponentScale class inherits from the <see cref="Scale" /> class, and implements
	/// the features specific to <see cref="AxisType.Exponent" />.
	/// </summary>
	/// <remarks>
	/// ExponentScale is a non-linear axis in which the values are scaled using an exponential function
	/// with the <see cref="Scale.Exponent" /> property.
	/// </remarks>
	/// 
	/// <author> John Champion with contributions by jackply </author>
	/// <version> $Revision: 1.1 $ $Date: 2005-12-26 11:34:14 $ </version>
	class ExponentScale : Scale
	{

	#region constructors

		public ExponentScale( Axis parentAxis )
			: base( parentAxis )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="ExponentScale" /> object from which to copy</param>
		public ExponentScale( Scale rhs )
			: base( rhs )
		{
		}
		
		/// <summary>
		/// Deep-copy clone routine
		/// </summary>
		/// <returns>A new, independent copy of the <see cref="ExponentScale" /> object.</returns>
		public object Clone()
		{
			return new ExponentScale( this ); 
		}

	#endregion

	#region properties

		public override AxisType Type
		{
			get { return AxisType.Exponent; }
		}

	#endregion

	#region methods

		/// <summary>
		/// Setup some temporary transform values in preparation for rendering the <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// This method is typically called by the parent <see cref="GraphPane"/>
		/// object as part of the <see cref="GraphPane.Draw"/> method.  It is also
		/// called by <see cref="GraphPane.GeneralTransform"/> and
		/// <see cref="GraphPane.ReverseTransform( PointF, out double, out double, out double )"/>
		/// methods to setup for coordinate transformations.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="axis">
		/// The parent <see cref="Axis" /> for this <see cref="Scale" />
		/// </param>
		override public void SetupScaleData( GraphPane pane, Axis axis )
		{
			base.SetupScaleData( pane, axis );

			if (  this.exponent > 0 )
			{
				this.minScale = Math.Pow( this.min, exponent );
				this.maxScale = Math.Pow( this.max, exponent );
			}
			else if ( this.exponent < 0 )
			{
				this.minScale = Math.Pow( this.max, exponent );
				this.maxScale = Math.Pow( this.min, exponent );
			}
		}

		/// <summary>
		/// Determine the value for any major tic.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <param name="baseVal">
		/// The value of the first major tic (floating point double)
		/// </param>
		/// <param name="tic">
		/// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
		/// </param>
		/// <returns>
		/// The specified major tic value (floating point double).
		/// </returns>
		override internal double CalcMajorTicValue( double baseVal, double tic )
		{
			if ( this.exponent > 0.0 )
			{
				//return baseVal + Math.Pow ( (double) this.step * tic, exp );
				//baseVal is got from CalBase..., and it is exp..
				return Math.Pow( Math.Pow( baseVal, 1 / exponent ) + this.step * tic, exponent );
			}
			else if ( this.exponent < 0.0 )
			{
				//baseVal is got from CalBase..., and it is exp..
				return Math.Pow( Math.Pow( baseVal, 1 / exponent ) + this.step * tic, exponent );
			}

			return 1.0;
		}

		/// <summary>
		/// Determine the value for any minor tic.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <param name="baseVal">
		/// The value of the first major tic (floating point double).  This tic value is the base
		/// reference for all tics (including minor ones).
		/// </param>
		/// <param name="iTic">
		/// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
		/// </param>
		/// <returns>
		/// The specified minor tic value (floating point double).
		/// </returns>
		override internal double CalcMinorTicValue( double baseVal, int iTic )
		{
			return baseVal + Math.Pow( (double) this.step * (double) iTic, exponent );
		}

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
			return (int) ( ( Math.Pow( this.min, exponent ) - baseVal ) / Math.Pow( this.minorStep, exponent ) );
		}

		/// <summary>
		/// Select a reasonable exponential axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Exponent"/> type axes, and it
		/// is called by the general <see cref="Scale.PickScale"/> method.  The exponential scale
		/// relies on the <see cref="Scale.Exponent" /> property to set the scaling exponent.  This
		/// method honors the <see cref="Scale.MinAuto"/>, <see cref="Scale.MaxAuto"/>,
		/// and <see cref="Scale.StepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Scale.Min"/>, <see cref="Scale.Max"/>, or <see cref="Scale.Step"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  For log axes, the MinorStep
		/// value is not used.
		/// <para>On Exit:</para>
		/// <para><see cref="Scale.Min"/> is set to scale minimum (if <see cref="Scale.MinAuto"/> = true)</para>
		/// <para><see cref="Scale.Max"/> is set to scale maximum (if <see cref="Scale.MaxAuto"/> = true)</para>
		/// <para><see cref="Scale.Step"/> is set to scale step size (if <see cref="Scale.StepAuto"/> = true)</para>
		/// <para><see cref="Scale.ScaleMag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="Scale.ScaleFormat"/> is set to the display format for the values (this controls the
		/// number of decimal places, whether there are thousands separators, currency types, etc.)</para>
		/// </remarks>
		/// <seealso cref="Scale.PickScale"/>
		/// <seealso cref="AxisType.Exponent"/>
		override public void PickScale( GraphPane pane, Graphics g, float scaleFactor )
		{
			// call the base class first
			base.PickScale( pane, g, scaleFactor );

			// Test for trivial condition of range = 0 and pick a suitable default
			if ( this.max - this.min < 1.0e-20 )
			{
				if ( this.maxAuto )
					this.max = this.max + 0.2 * ( this.max == 0 ? 1.0 : Math.Abs( this.max ) );
				if ( this.minAuto )
					this.min = this.min - 0.2 * ( this.min == 0 ? 1.0 : Math.Abs( this.min ) );
			}

			// This is the zero-lever test.  If minVal is within the zero lever fraction
			// of the data range, then use zero.

			if ( this.minAuto && this.min > 0 &&
				this.min / ( this.max - this.min ) < Default.ZeroLever )
				this.min = 0;

			// Repeat the zero-lever test for cases where the maxVal is less than zero
			if ( this.maxAuto && this.max < 0 &&
				Math.Abs( this.max / ( this.max - this.min ) ) <
				Default.ZeroLever )
				this.max = 0;

			// Calculate the new step size
			if ( this.stepAuto )
			{
				double targetSteps = ( parentAxis is XAxis ) ? Default.TargetXSteps : Default.TargetYSteps;

				// Calculate the step size based on target steps
				this.step = CalcStepSize( this.max - this.min, targetSteps );

				if ( this.isPreventLabelOverlap )
				{
					// Calculate the maximum number of labels
					double maxLabels = (double) this.CalcMaxLabels( g, pane, scaleFactor );

					if ( maxLabels < ( this.max - this.min ) / this.step )
						this.step = CalcBoundedStepSize( this.max - this.min, maxLabels );
				}
			}

			// Calculate the new step size
			if ( this.minorStepAuto )
				this.minorStep = CalcStepSize( this.step,
					( parentAxis is XAxis ) ? Default.TargetMinorXSteps : Default.TargetMinorYSteps );

			// Calculate the scale minimum
			if ( this.minAuto )
				this.min = this.min - MyMod( this.min, this.step );

			// Calculate the scale maximum
			if ( this.maxAuto )
				this.max = MyMod( this.max, this.step ) == 0.0 ? this.max :
					this.max + this.step - MyMod( this.max, this.step );

			// set the scale magnitude if required
			if ( this.scaleMagAuto )
			{
				// Find the optimal scale display multiple
				double mag = 0;
				double mag2 = 0;

				if ( Math.Abs( this.min ) > 1.0e-10 )
					mag = Math.Floor( Math.Log10( Math.Abs( this.min ) ) );
				if ( Math.Abs( this.max ) > 1.0e-10 )
					mag2 = Math.Floor( Math.Log10( Math.Abs( this.max ) ) );
				if ( Math.Abs( mag2 ) > Math.Abs( mag ) )
					mag = mag2;

				// Do not use scale multiples for magnitudes below 4
				if ( Math.Abs( mag ) <= 3 )
					mag = 0;

				// Use a power of 10 that is a multiple of 3 (engineering scale)
				this.scaleMag = (int) ( Math.Floor( mag / 3.0 ) * 3.0 );
			}

			// Calculate the appropriate number of dec places to display if required
			if ( this.scaleFormatAuto )
			{
				int numDec = 0 - (int) ( Math.Floor( Math.Log10( this.step ) ) - this.scaleMag );
				if ( numDec < 0 )
					numDec = 0;
				this.scaleFormat = "f" + numDec.ToString();
			}
		}

		/// <summary>
		/// Make a value label for an <see cref="AxisType.Exponent" /> <see cref="Axis" />.
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
		/// The numeric value associated with the label.  This value is ignored for log (<see cref="IsLog"/>)
		/// and text (<see cref="IsText"/>) type axes.
		/// </param>
		/// <param name="label">
		/// Output only.  The resulting value label.
		/// </param>
		override internal void MakeLabel( GraphPane pane, int index, double dVal, out string label )
		{
			if ( this.scaleFormat == null )
				this.scaleFormat = Scale.Default.ScaleFormat;

			double scaleMult = Math.Pow( (double) 10.0, this.scaleMag );
			double val = Math.Pow( dVal, 1 / exponent ) / scaleMult;
			label = val.ToString( this.scaleFormat );
		}


	#endregion

	}
}
