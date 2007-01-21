//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright © 2007  John Champion and JCarpenter
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
using System.Collections.Generic;

namespace ZedGraph
{
	/// <summary>
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// 
	/// <author> John Champion and JCarpenter </author>
	/// <version> $Revision: 3.1 $ $Date: 2007-01-21 07:49:05 $ </version>
	public class Selection : List<CurveItem>
	{
		// Revision: JCarpenter 10/06
		/// <summary>
		/// Subscribe to this event to receive notice 
		/// that the list of selected CurveItems has changed
		/// </summary>
		public event EventHandler SelectionChangedEvent;

	#region static properties

		/// <summary>
		/// The <see cref="Border" /> type to be used for drawing "selected"
		/// <see cref="PieItem" />, <see cref="BarItem" />, <see cref="HiLowBarItem" />,
		/// <see cref="CandleStickItem" />, and <see cref="JapaneseCandleStickItem" /> item types.
		/// </summary>
		public static Border Border = new Border( Color.Gray, 1.0f );
		/// <summary>
		/// The <see cref="Fill" /> type to be used for drawing "selected"
		/// <see cref="PieItem" />, <see cref="BarItem" />, <see cref="HiLowBarItem" />,
		/// and <see cref="JapaneseCandleStickItem" /> item types.
		/// </summary>
		public static Fill Fill = new Fill( Color.Gray );
		/// <summary>
		/// The <see cref="Line" /> type to be used for drawing "selected"
		/// <see cref="LineItem" /> and <see cref="StickItem" /> types
		/// </summary>
		public static Line Line = new Line( Color.Gray );
		//			public static ErrorBar ErrorBar = new ErrorBar( Color.Gray );
		/// <summary>
		/// The <see cref="Symbol" /> type to be used for drawing "selected"
		/// <see cref="LineItem" /> and <see cref="ErrorBarItem" /> types.
		/// </summary>
		public static Symbol Symbol = new Symbol( SymbolType.Circle, Color.Gray );

		//public static Color SelectedSymbolColor = Color.Gray;

	#endregion

	#region Methods

		public void Select( MasterPane master, CurveItem ci )
		{
			//Clear the selection, but don't send the event,
			//the event will be sent in "AddToSelection" by calling "UpdateSelection"
			ClearSelection( master, false );

			AddToSelection( master, ci );
		}

		public void Select( MasterPane master, List<CurveItem> ciList )
		{
			//Clear the selection, but don't send the event,
			//the event will be sent in "AddToSelection" by calling "UpdateSelection"
			ClearSelection( master, false );

			AddToSelection( master, ciList );
		}

		public void AddToSelection( MasterPane master, CurveItem ci )
		{
			if ( this.Contains( ci ) == false )
				Add( ci );

			UpdateSelection( master );
		}

		public void AddToSelection( MasterPane master, List<CurveItem> ciList )
		{
			foreach ( CurveItem ci in ciList )
			{
				if ( this.Contains( ci ) == false )
					this.Add( ci );
			}

			UpdateSelection( master );
		}

		public void RemoveFromSelection( MasterPane master, CurveItem ci )
		{
			if ( this.Contains( ci ) )
				this.Remove( ci );

			UpdateSelection( master );

		}

		public void ClearSelection( MasterPane master )
		{
			ClearSelection( master, true );
		}

		public void ClearSelection( MasterPane master, bool sendEvent )
		{
			this.Clear();

			foreach ( GraphPane pane in master.PaneList )
			{
				foreach ( CurveItem ci in pane.CurveList )
				{
					ci.IsSelected = false;
				}
			}

			if ( sendEvent )
			{
				if ( SelectionChangedEvent != null )
					SelectionChangedEvent( this, new EventArgs() );
			}
		}

		public void UpdateSelection( MasterPane master )
		{
			if ( Count <= 0 )
			{
				ClearSelection( master );
				return;
			}

			foreach ( GraphPane pane in master.PaneList )
			{
				foreach ( CurveItem ci in pane.CurveList )
				{
					//Make it Inactive
					ci.IsSelected = false;
				}

			}
			foreach ( CurveItem ci in  this )
			{
				//Make Active
				ci.IsSelected = true;

				//If it is a line / scatterplot, the selected Curve may be occluded by an unselected Curve
				//So, move it to the top of the ZOrder by removing it, and re-adding it.

				//Why only do this for Lines? ...Bar and Pie Curves are less likely to overlap, 
				//and adding and removing Pie elements changes thier display order
				if ( ci.IsLine )
				{
					//I don't know how to get a Pane, from a CurveItem, so I can only do it 
					//if there is one and only one Pane, based on the assumption that the 
					//Curve's Pane is MasterPane[0]

					//If there is only one Pane
					if ( master.PaneList.Count == 1 )
					{
						GraphPane pane = master.PaneList[0];
						pane.CurveList.Remove( ci );
						pane.CurveList.Insert( 0, ci );
					}

				}
			}

			//Send Selection Changed Event
			if ( SelectionChangedEvent != null )
				SelectionChangedEvent( this, new EventArgs() );

		}

		#endregion


	}
}
