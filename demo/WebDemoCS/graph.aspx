<%@ Page language="c#" codebehind="graph.aspx.cs" AutoEventWireup="false" Inherits="ZG1.graph" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>graph</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<zgw:ZedGraphWeb id="ZedGraphWeb1" runat="server"></zgw:ZedGraphWeb>
		</form>
	</body>
</HTML>
