<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<%@ Page Language="c#" autoeventwireup="false" Inherits="ZG1.Graph3" CodeBehind="Graph3.aspx.cs" %>
<ZGW:ZEDGRAPHWEB id="ZedGraphWeb1" RenderMode="RawImage" title="Simple Bar Demo" runat="server" Width="500" Height="375">
	<AXISFILL isvisible="True" color="LightGray" isscaled="True" type="Brush"></AXISFILL>
	<PANEBORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></PANEBORDER>
	<FONTSPEC isbold="True" stringalignment="Center" fontcolor="Black" angle="0" family="Arial"
		isitalic="False" isunderline="False" size="16">
		<FILL isvisible="True" color="White" isscaled="True" type="Solid" alignv="Center" rangemin="0"
			alignh="Center" rangemax="0"></FILL>
		<BORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></BORDER>
	</FONTSPEC>
	<PANEFILL color="White" type="Solid"></PANEFILL>
	<CURVELIST>
		<ZGW:ZEDGRAPHWEBBARITEM IsVisible="True" Color="Green" Label="Some Data">
			<POINTS>
				<ZGW:ZEDGRAPHWEBPOINTPAIR Y="40" X="10"></ZGW:ZEDGRAPHWEBPOINTPAIR>
				<ZGW:ZEDGRAPHWEBPOINTPAIR Y="50" X="20"></ZGW:ZEDGRAPHWEBPOINTPAIR>
				<ZGW:ZEDGRAPHWEBPOINTPAIR Y="30" X="30"></ZGW:ZEDGRAPHWEBPOINTPAIR>
				<ZGW:ZEDGRAPHWEBPOINTPAIR Y="80" X="40"></ZGW:ZEDGRAPHWEBPOINTPAIR>
				<ZGW:ZEDGRAPHWEBPOINTPAIR Y="60" X="50"></ZGW:ZEDGRAPHWEBPOINTPAIR>
				<ZGW:ZEDGRAPHWEBPOINTPAIR Y="10" X="60"></ZGW:ZEDGRAPHWEBPOINTPAIR>
			</POINTS>
			<FILL color="Green"></FILL>
		</ZGW:ZEDGRAPHWEBBARITEM>
	</CURVELIST>
	<XAXIS title="Run Number" type="Ordinal" isticsbetweenlabels="True"></XAXIS>
	<YAXIS title="Quality Coefficient"></YAXIS>
</ZGW:ZEDGRAPHWEB>
