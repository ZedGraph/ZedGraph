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
using System.Web.UI;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace ZedGraph
{
	public class ZedGraphWebCurveItem : GenericItem
	{
		public override string ToString()
		{
			return Label;
		}

		public ZedGraphWebCurveItem() : base()
		{
		}

		public ZedGraphWebCurveItem(string label) : base()
		{
			Label = label;
		}
		
		[NotifyParentProperty(true)]
		public string Label
		{
			get 
			{ 
				object x = ViewState["Label"]; 
				return (null == x) ? String.Empty : (string)x;
			}
			set { ViewState["Label"] = value; }
		}
	}

	public class ZedGraphWebBar : ZedGraphWebCurveItem
	{
	}

	public class ZedGraphWebErrorBar : ZedGraphWebCurveItem
	{
	}

	public class ZedGraphWebHiLowBar : ZedGraphWebCurveItem
	{
	}

	public class ZedGraphWebLine : ZedGraphWebCurveItem
	{
	}

	public class ZedGraphWebCurveCollection : GenericCollection
	{	
		public override string ToString(){return String.Empty;}		

		public ZedGraphWebCurveCollection() : base()
		{
			Schema = new GenericCollectionItemSchema[4];
			Schema[0].code = 'b';
			Schema[0].type = typeof(ZedGraphWebBar);
			Schema[1].code = 'l';
			Schema[1].type = typeof(ZedGraphWebLine);
			Schema[2].code = 'e';
			Schema[2].type = typeof(ZedGraphWebErrorBar);
			Schema[3].code = 'h';
			Schema[3].type = typeof(ZedGraphWebHiLowBar);
		}

		public void Add(ZedGraphWebCurveItem item)
		{
			if ( null != item )
				ListAdd( item );
			else
				throw new ArgumentException("parameter cannot be null","item");
		}	

		[NotifyParentProperty(true)]
		public ZedGraphWebCurveItem this [int index]
		{
			get 
			{
				return (ZedGraphWebCurveItem)ListGet(index);
			}
			set
			{
				ListInsert(index,value);
			}
		}			
	}

}
