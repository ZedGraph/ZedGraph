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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZedGraph;

namespace ZG1
{

	/// <summary>
	/// Summary description for graph.
	/// </summary>
	public class graph : System.Web.UI.Page
	{
		protected ZedGraph.ZedGraphWeb ZedGraphWeb1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.ContentType = "image/png";
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ZedGraphWeb1.RenderGraph += new ZedGraph.ZedGraphWebControlEventHandler(this.OnRenderGraph);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void OnRenderGraph(System.Drawing.Graphics g, ZedGraph.MasterPane mPane)
		{
			//mPane[0] = new GraphPane();
		      
			GraphPane pane = mPane[0];

			string val = (string)Page.Request.Params["graph"];
			if ( val == null ) val = string.Empty;
			val = val.Trim();

			if (val == "2")
			{
				// Use the Horizontal Stacked Bar Demo

				// Set the title and axis labels
				pane.Title = "Cat Stats";
				pane.YAxis.Title = "Big Cats";
				pane.XAxis.Title = "Population";

				// Make up some data points
				string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
				double[] x = { 100, 115, 75, 22, 98, 40 };
				double[] x2 = { 120, 175, 95, 57, 113, 110 };
				double[] x3 = { 204, 192, 119, 80, 134, 156 };

				// Generate a red bar with "Curve 1" in the legend
				BarItem myCurve = pane.AddBar("Here", x, null, Color.Red);
				// Fill the bar with a red-white-red color gradient for a 3d look
				myCurve.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red, 90f);

				// Generate a blue bar with "Curve 2" in the legend
				myCurve = pane.AddBar("There", x2, null, Color.Blue);
				// Fill the bar with a Blue-white-Blue color gradient for a 3d look
				myCurve.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue, 90f);

				// Generate a green bar with "Curve 3" in the legend
				myCurve = pane.AddBar("Elsewhere", x3, null, Color.Green);
				// Fill the bar with a Green-white-Green color gradient for a 3d look
				myCurve.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green, 90f);

				// Draw the Y tics between the labels instead of at the labels
				pane.YAxis.IsTicsBetweenLabels = true;

				// Set the YAxis labels
				pane.YAxis.TextLabels = labels;
				// Set the YAxis to Text type
				pane.YAxis.Type = AxisType.Text;

				// Set the bar type to stack, which stacks the bars by automatically accumulating the values
				pane.BarType = BarType.Stack;

				// Make the bars horizontal by setting the BarBase to "Y"
				pane.BarBase = BarBase.Y;

				// Fill the axis background with a color gradient
				pane.AxisFill = new Fill(Color.White,
					Color.FromArgb(255, 255, 166), 45.0F);

				pane.Title = "Graph Number 2";

			}
			else
			{
				// Use the standard, built-in demo
				ZedGraph.ZedGraphWeb.RenderDemo(g, pane);
				pane.Title = "Graph Number 1";
			}

			pane.AxisChange(g);
		}
	}
}
