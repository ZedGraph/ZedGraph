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
			// Put user code to initialize the page here
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

		private void OnRenderGraph(System.Drawing.Graphics g, ZedGraph.GraphPane pane)
		{
			string val = (string)Page.Request.Params["graph"];
			if ( val == null ) val = string.Empty;
			val = val.Trim();

			if ( val == "2" )
			{
				ZedGraph.ZedGraphWeb.RenderDemo(g,pane);
				pane.Title = "Graph Number 2";				
			}
			else
			{
				ZedGraph.ZedGraphWeb.RenderDemo(g,pane);
				pane.Title = "Graph Number 1";
			}
		}
	}
}
