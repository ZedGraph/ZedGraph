using System;
using System.Drawing;
using System.Collections;
using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for SimpleDemo.
	/// </summary>
	public class MasterPaneDemo : DemoBase
	{

		public MasterPaneDemo() : base( "A Demo of the MasterPane",
									"MasterPane Demo", DemoType.General, DemoType.Special )
		{
			base.MasterPane.Title = "MasterPane Test";
			base.MasterPane.PaneFill = new Fill( Color.White, Color.MediumSlateBlue, 45.0F );
			base.MasterPane.IsShowTitle = true;
			//base.MasterPane.HasMasterLegend = false ;
			
			base.MasterPane.Tag = "This is my tag";
			base.MasterPane.MarginAll = 10;
			base.MasterPane.InnerPaneGap = 10;
			base.MasterPane.Legend.IsVisible = true;
			base.MasterPane.Legend.Position = LegendPos.TopCenter;

			Bitmap bm = new Bitmap( @"c:\windows\winnt256.bmp" );
			Image image = Image.FromHbitmap( bm.GetHbitmap() );
			ImageItem imageItem = new ImageItem( image,
				new RectangleF( 0.1F, 0.03F, 0.1F, 0.06F ),
				CoordType.PaneFraction, AlignH.Left, AlignV.Top );
			//imageItem.IsScaled = false;
			imageItem.ZOrder = ZOrder.G_BehindAll;
			base.MasterPane.GraphItemList.Add( imageItem );

			TextItem text = new TextItem( "Confidential", 0.80F, 0.12F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;

			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;

			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			base.MasterPane.GraphItemList.Add( text );

			text = new TextItem("DRAFT", 0.5F, 0.5F );
			text.Location.CoordinateFrame = CoordType.PaneFraction;

			text.FontSpec.Angle = 30.0F;
			text.FontSpec.FontColor = Color.FromArgb( 70, 255, 100, 100 );
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 100;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Fill.IsVisible = false;

			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Center;
			text.ZOrder = ZOrder.A_InFront;
			
			base.MasterPane.GraphItemList.Add( text );
			Graphics g = this.ZedGraphControl.CreateGraphics();
			base.MasterPane.PaneList.Remove( 0 );
			
			for ( int j=0; j<6; j++ )
			{
				// Create a new graph with topLeft at (40,40) and size 600x400
				GraphPane myPane = new GraphPane( new Rectangle( 40, 40, 600, 400 ),
					"My Test Graph #" + (j+1).ToString(),
					"X Axis",
					"Y Axis" );

				myPane.PaneFill = new Fill( Color.White, Color.LightYellow, 45.0F );
				myPane.BaseDimension = 6.0F;
				myPane.Tag = "MyTag" + j;

				// Make up some data arrays based on the Sine function
				double x, y;
				PointPairList list = new PointPairList();
				for ( int i=0; i<36; i++ )
				{
					x = (double) i + 5;
					y = 3.0 * ( 1.5 + Math.Sin( (double) i * 0.2 + (double) j ) );
					list.Add( x, y );
				}

				// Generate a red curve with diamond
				// symbols, and "Porsche" in the legend
				LineItem myCurve = myPane.AddCurve( "label" + j.ToString(),
					list, Color.Red, SymbolType.Diamond );

				// Tell ZedGraph to refigure the
				// axes since the data have changed
				//myPane.AxisChange( this.CreateGraphics() );

				base.MasterPane.Add( myPane );
			}

			base.MasterPane.AutoPaneLayout( g, PaneLayout.SquareColPreferred );

			base.ZedGraphControl.AxisChange();
			g.Dispose();
		}
	}
}
