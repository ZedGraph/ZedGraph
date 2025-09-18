//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2006 John Champion and Jerry Vos
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
using System.Drawing.Drawing2D;
using System.Reflection;
using System.IO;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class ImageForSymbolDemo : DemoBase
	{

		public ImageForSymbolDemo()
			: base( "Demonstration of using an image for a Chart Symbol",
									"Image for Symbol Demo", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			myPane.Title.Text = "Demonstration Chart with an Image for Symbols";
			myPane.XAxis.Title.Text = "Some Independent Value";
			myPane.YAxis.Title.Text = "The Dependent Axis";

			PointPairList list = new PointPairList();
			for ( int i = 0; i < 20; i++ )
			{
				double x = (double)i + 5;
				double y = 3.0 * ( 1.5 + Math.Sin( (double)i * 0.4 ) );
				list.Add( x, y );
			}
			LineItem myCurve = myPane.AddCurve( "Smile! It's only data", list, Color.Black, SymbolType.Square );

			//string[] resNames = a.GetManifestResourceNames();

			//Image image = Resources.
			//Bitmap bm = new Bitmap( @"..\teeth.png" );
			//Image image = Image.FromHbitmap( bm.GetHbitmap() );

			Assembly a = Assembly.GetExecutingAssembly();
			Stream imgStream = a.GetManifestResourceStream( "ZedGraph.Demo.Resources.teeth.png" );
			Image image = Bitmap.FromStream( imgStream ) as Bitmap;

			myCurve.Symbol.Type = SymbolType.Circle;
			myCurve.Symbol.Size = 18;
			myCurve.Symbol.Border.IsVisible = false;
			myCurve.Symbol.Fill = new Fill( image, WrapMode.Clamp );

			myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0f );
			myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 255, 255 ), 45.0f );

			base.ZedGraphControl.AxisChange();
		}
	}
}
