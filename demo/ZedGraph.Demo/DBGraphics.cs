using System;
using System.Drawing;

namespace GDIDB
{
	/// <summary>
	/// Class to implement Double Buffering 
	/// NT Almond 
	/// 24 July 2003
	/// </summary>
	/// 
	public class DBGraphics
	{
		private	Graphics	graphics;
		private Bitmap		memoryBitmap;
		private	int			width;
		private	int			height;

		/// <summary>
		/// Default constructor
		/// </summary>
		public DBGraphics()
		{
			width	= 0;
			height	= 0;
		}

		/// <summary>
		/// Creates double buffer object
		/// </summary>
		/// <param name="g">Window forms Graphics Object</param>
		/// <param name="width">width of paint area</param>
		/// <param name="height">height of paint area</param>
		/// <returns>true/false if double buffer is created</returns>
		public bool CreateDoubleBuffer(Graphics g, int width, int height)
		{

			if (memoryBitmap != null)
			{
				memoryBitmap.Dispose();
				memoryBitmap = null;
			}

			if (graphics != null)
			{
				graphics.Dispose();
				graphics = null;
			}

			if (width <= 0 || height <= 0)
				return false;


			if ((width != this.width) || (height != this.height))
			{
				this.width = width;
				this.height = height;

				memoryBitmap	= new Bitmap(width, height);
				graphics		= Graphics.FromImage(memoryBitmap);
			}

			return true;
		}


		/// <summary>
		/// Renders the double buffer to the screen
		/// </summary>
		/// <param name="g">Window forms Graphics Object</param>
		public void Render(Graphics g)
		{
			if (memoryBitmap != null)
				g.DrawImage(memoryBitmap, new Rectangle(0,0, width, height),0,0, width, height, GraphicsUnit.Pixel);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>true if double buffering can be achieved</returns>
		public bool CanDoubleBuffer()
		{
			return graphics != null;
		}

		/// <summary>
		/// Accessor for memory graphics object
		/// </summary>
		public Graphics g 
		{
			get 
			{ 
				return graphics; 
			}
		}		
	}
}
