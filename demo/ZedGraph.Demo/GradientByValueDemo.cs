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
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class GradientByValueDemo : DemoBase
	{

		public GradientByValueDemo() : base( "A demo demonstrating GradientByZ coloration\n" +
				"You can use the X, Y, or Z data value to control the fill color for the points",
				"Gradient By Z", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			// Set the title and axis labels
			myPane.Title.Text = "PVT Properties";
			myPane.XAxis.Title.Text = "Pressure (atm)";
			myPane.YAxis.Title.Text = "Temperature (C)";
			
			// Enter some calculated data constants
			double[] x = new double[84];
			double[] y = new double[84];
			double[] z = new double[84];

			x[0]=59.26;  y[0]=13.03;  z[0]=31.67;
			x[1]=75.62;  y[1]=18.73;  z[1]=23.34;
			x[2]=11.09;  y[2]=16.94;  z[2]=21.04;
			x[3]=29.68;  y[3]=11.66;  z[3]=32.19;
			x[4]=27.73;  y[4]=12.94;  z[4]=28.94;
			x[5]=54.71;  y[5]=18.04;  z[5]=22.76;
			x[6]=66.92;  y[6]=17.36;  z[6]=24.51;
			x[7]=27.65;  y[7]=13.19;  z[7]=28.39;
			x[8]=41.93;  y[8]=12.79;  z[8]=30.60;
			x[9]=52.21;  y[9]=19.66;  z[9]=20.79;
			x[10]=41.82; y[10]=17.17; z[10]=22.98;
			x[11]=25.59; y[11]=12.57; z[11]=29.53;
			x[12]=45.86; y[12]=18.74; z[12]=21.36;
			x[13]=8.73;  y[13]=12.66; z[13]=27.71;
			x[14]=26.49; y[14]=13.29; z[14]=28.07;
			x[15]=22.58; y[15]=12.47; z[15]=29.48;
			x[16]=38.87; y[16]=14.76; z[16]=26.36;
			x[17]=75.93; y[17]=22.54; z[17]=19.50;
			x[18]=72.00; y[18]=14.38; z[18]=29.88;
			x[19]=12.95; y[19]=12.01; z[19]=29.58;
			x[20]=8.99;  y[20]=14.96; z[20]=23.60;
			x[21]=88.99; y[21]=17.58; z[21]=25.76;
			x[22]=34.66; y[22]=15.40; z[22]=24.97;
			x[23]=19.74; y[23]=15.39; z[23]=23.79;
			x[24]=43.63; y[24]=19.88; z[24]=20.03;
			x[25]=26.17; y[25]=12.42; z[25]=29.93;
			x[26]=21.17; y[26]=12.91; z[26]=28.37;
			x[27]=62.67; y[27]=16.48; z[27]=25.47;
			x[28]=21.33; y[28]=12.05; z[28]=30.36;
			x[29]=95.91; y[29]=17.40; z[29]=26.52;
			x[30]=63.01; y[30]=17.92; z[30]=23.51;
			x[31]=21.08; y[31]=15.74; z[31]=23.40;
			x[32]=20.99; y[32]=14.57; z[32]=25.21;
			x[33]=30.12; y[33]=16.08; z[33]=23.58;
			x[34]=31.23; y[34]=17.44; z[34]=21.90;
			x[35]=44.70; y[35]=15.79; z[35]=25.19;
			x[36]=42.40; y[36]=13.67; z[36]=28.74;
			x[37]=55.96; y[37]=15.98; z[37]=25.74;
			x[38]=33.06; y[38]=15.41; z[38]=24.83;
			x[39]=41.31; y[39]=14.37; z[39]=27.28;
			x[40]=59.27; y[40]=13.99; z[40]=29.58;
			x[41]=47.13; y[41]=11.43; z[41]=34.65;
			x[42]=46.20; y[42]=15.21; z[42]=26.21;
			x[43]=59.66; y[43]=20.08; z[43]=20.82;
			x[44]=42.42; y[44]=16.45; z[44]=24.00;
			x[45]=6.28;  y[45]=11.76; z[45]=29.51;
			x[46]=62.34; y[46]=18.66; z[46]=22.56;
			x[47]=32.14; y[47]=14.91; z[47]=25.58;
			x[48]=50.85; y[48]=14.20; z[48]=28.43;
			x[49]=46.38; y[49]=14.99; z[49]=26.60;
			x[50]=80.77; y[50]=23.55; z[50]=18.93;
			x[51]=21.14; y[51]=13.40; z[51]=27.34;
			x[52]=33.16; y[52]=15.39; z[52]=24.88;
			x[53]=38.22; y[53]=20.40; z[53]=19.21;
			x[54]=58.78; y[54]=13.63; z[54]=30.28;
			x[55]=70.03; y[55]=13.23; z[55]=32.20;
			x[56]=39.34; y[56]=15.25; z[56]=25.62;
			x[57]=13.68; y[57]=14.92; z[57]=24.04;
			x[58]=85.41; y[58]=23.81; z[58]=18.93;
			x[59]=28.86; y[59]=18.50; z[59]=20.48;
			x[60]=98.51; y[60]=14.11; z[60]=32.72;
			x[61]=94.70; y[61]=17.22; z[61]=26.68;
			x[62]=87.21; y[62]=22.94; z[62]=19.74;
			x[63]=72.77; y[63]=12.38; z[63]=34.61;
			x[64]=71.69; y[64]=13.26; z[64]=32.26;
			x[65]=5.37;  y[65]=15.07; z[65]=23.11;
			x[66]=70.10; y[66]=16.20; z[66]=26.45;
			x[67]=16.61; y[67]=12.81; z[67]=28.14;
			x[68]=10.06; y[68]=11.63; z[68]=30.23;
			x[69]=85.76; y[69]=13.84; z[69]=32.23;
			x[70]=6.93;  y[70]=14.80; z[70]=23.67;
			x[71]=26.68; y[71]=13.60; z[71]=27.50;
			x[72]=63.22; y[72]=13.65; z[72]=30.64;
			x[73]=97.20; y[73]=24.85; z[73]=18.77;
			x[74]=44.99; y[74]=13.80; z[74]=28.71;
			x[75]=96.99; y[75]=20.02; z[75]=23.19;
			x[76]=50.47; y[76]=16.91; z[76]=23.94;
			x[77]=32.23; y[77]=11.94; z[77]=31.72;
			x[78]=77.09; y[78]=19.97; z[78]=22.00;
			x[79]=98.48; y[79]=14.21; z[79]=32.49;
			x[80]=38.76; y[80]=17.66; z[80]=22.15;
			x[81]=42.50; y[81]=17.23; z[81]=22.96;
			x[82]=8.97;  y[82]=14.43; z[82]=24.43;
			x[83]=60.76; y[83]=14.94; z[83]=27.87;

			PointPairList pp = new PointPairList( y, x, z );

			// Generate a red curve with diamond symbols, and "Gas Data" in the legend
			LineItem myCurve = myPane.AddCurve( "Gas Data", pp, Color.Red,
										SymbolType.Diamond );
			myCurve.Symbol.Size = 12;
			// Set up a red-blue color gradient to be used for the fill
			myCurve.Symbol.Fill = new Fill( Color.Red, Color.Blue );
			// Turn off the symbol borders
			myCurve.Symbol.Border.IsVisible = false;
			// Instruct ZedGraph to fill the symbols by selecting a color out of the
			// red-blue gradient based on the Z value.  A value of 19 or less will be red,
			// a value of 34 or more will be blue, and values in between will be a
			// linearly apportioned color between red and blue.
			myCurve.Symbol.Fill.Type = FillType.GradientByZ;
			myCurve.Symbol.Fill.RangeMin = 19;
			myCurve.Symbol.Fill.RangeMax = 34;
			//myCurve.Symbol.Fill.RangeDefault = 19;

			// Turn off the line, so the curve will by symbols only
			myCurve.Line.IsVisible = false;

			// Display a text item with "MW = 34" on the graph
			TextObj text = new TextObj( "MW = 34", 12.9F, 110, CoordType.AxisXYScale );
			text.FontSpec.FontColor = Color.Blue;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Size = 14;
			myPane.GraphObjList.Add( text );

			// Display a text item with "MW = 19" on the graph
			text = new TextObj( "MW = 19", 25, 110, CoordType.AxisXYScale );
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Size = 14;
			myPane.GraphObjList.Add( text );
			
			// Show the X and Y grids
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;

			// Set the x and y scale and title font sizes to 14
			myPane.XAxis.Scale.FontSpec.Size = 14;
			myPane.XAxis.Title.FontSpec.Size = 14;
			myPane.YAxis.Scale.FontSpec.Size = 14;
			myPane.YAxis.Title.FontSpec.Size = 14;
			// Set the GraphPane title font size to 16
			myPane.Title.FontSpec.Size = 16;
			// Turn off the legend
			myPane.Legend.IsVisible = false;
			
			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.FromArgb( 255, 255, 166), 90F );

			base.ZedGraphControl.AxisChange();
		}
	}
}
