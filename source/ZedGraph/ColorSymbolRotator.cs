//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2004  John Champion
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
using Color		 = System.Drawing.Color;

namespace ZedGraph
{
	/// <summary>
	/// Class used to get the next color/symbol for GraphPane.AddCurve methods.
	/// </summary>
	/// 
	/// <author> Jerry Vos </author>
	/// <version> $Revision: 2.0 $ $Date: 2004-09-02 06:24:58 $ </version>
	public class ColorSymbolRotator
	{
	#region Static fields
		/// <summary>
		/// The <see cref="Color"/>s <see cref="ColorSymbolRotator"/> 
		/// rotates through.
		/// </summary>
		public static readonly Color[] COLORS = new Color[]
		{
			Color.Red,
			Color.Blue,
			Color.Green,
			Color.Purple,
			Color.Orange,
			Color.LightBlue,
		};

		/// <summary>
		/// The <see cref="SymbolType"/>s <see cref="ColorSymbolRotator"/> 
		/// rotates through.
		/// </summary>
		public static readonly SymbolType[] SYMBOLS = new SymbolType[]
		{
			SymbolType.Circle,
			SymbolType.Diamond,
			SymbolType.Plus,
			SymbolType.Square,
			SymbolType.Star,
			SymbolType.Triangle,
			SymbolType.TriangleDown,
			SymbolType.XCross,
		};		

		private static ColorSymbolRotator staticInstance;
	#endregion
	
	#region Fields
		/// <summary>
		/// The index of the next color to be used. Note: may be 
		/// > COLORS.Length, it is reset to 0 on the next call if it is.
		/// </summary>
		protected int colorIndex = 0;

		/// <summary>
		/// The index of the next symbol to be used. Note: may be 
		/// > SYMBOLS.Length, it is reset to 0 on the next call if it is.
		/// </summary>
		protected int symbolIndex = 0;
	#endregion

	#region Properties
		/// <summary>
		/// Retrieves the next color in the rotation.
		/// <seealso cref="NextSymbol"/>
		/// <seealso cref="NextColorIndex"/>
		/// </summary>
		public Color NextColor
		{
			get { return COLORS[NextColorIndex]; }
		}

		/// <summary>
		/// Retrieves the index of the next color to be used.  Calling this
		/// method has the side effect of incrimenting the index of the 
		/// color index.
		/// </summary>
		public int NextColorIndex
		{
			get
			{
				if (colorIndex >= COLORS.Length)
					colorIndex = 0;

				return colorIndex++;
			}
			set
			{
				colorIndex = value;
			}
		}

		/// <summary>
		/// Retrieves the next color in the rotation.
		/// <seealso cref="NextColor"/>
		/// <seealso cref="NextSymbolIndex"/>
		/// </summary>
		public SymbolType NextSymbol
		{
			get { return SYMBOLS[NextColorIndex]; }
		}

		/// <summary>
		/// Retrieves the index of the next symbol to be used.  Calling this
		/// method has the side effect of incrimenting the index of the 
		/// symbol index.
		/// </summary>
		public int NextSymbolIndex
		{
			get
			{
				if (symbolIndex >= SYMBOLS.Length)
					symbolIndex = 0;

				return symbolIndex++;
			}
			set
			{
				symbolIndex = value;
			}
		}

		/// <summary>
		/// Retrieves the <see cref="ColorSymbolRotator"/> instance used by the
		/// static methods.
		/// <seealso cref="StaticNextColor"/>
		/// <seealso cref="StaticNextSymbol"/>
		/// </summary>
		public static ColorSymbolRotator StaticInstance
		{
			get
			{
				if (staticInstance == null)
					staticInstance = new ColorSymbolRotator();

				return staticInstance;
			}
		}
		
		/// <summary>
		/// Retrieves the next color from this class's static 
		/// <see cref="ColorSymbolRotator"/> instance
		/// <seealso cref="StaticInstance"/>
		/// <seealso cref="StaticNextSymbol"/>
		/// </summary>
		public static Color StaticNextColor
		{
			get { return StaticInstance.NextColor; } 
		}

		/// <summary>
		/// Retrieves the next symbol type from this class's static 
		/// <see cref="ColorSymbolRotator"/> instance
		/// <seealso cref="StaticInstance"/>
		/// <seealso cref="StaticNextColor"/>
		/// </summary>
		public static SymbolType StaticNextSymbol
		{
			get { return StaticInstance.NextSymbol; } 
		}
	#endregion
	}
}
