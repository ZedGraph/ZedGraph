<%@ Page language="c#" codebehind="graph3.aspx.cs" AutoEventWireup="false" Inherits="ZG1.Graph3" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Graph2</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<zgw:ZedGraphWeb id="ZedGraphWeb1" runat="server" Height="375" Width="500" title="Simple Bar Demo">
				<AxisFill Type="Brush" IsScaled="True" Color="LightGray" IsVisible="True"></AxisFill>
				<PaneBorder IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></PaneBorder>
				<FontSpec Size="16" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
					StringAlignment="Center" IsBold="True">
					<Fill RangeMax="0" Type="Solid" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
						IsVisible="True" AlignV="Center"></Fill>
					<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
				</FontSpec>
				<PaneFill Type="Solid" Color="White"></PaneFill>
				<CurveList>
					<zgw:ZedGraphWebBarItem Label="Some Data" Color="Green" IsVisible="True">
					    <Points>
					        <zgw:ZedGraphWebPointPair X="10" Y="40"></zgw:ZedGraphWebPointPair>
					        <zgw:ZedGraphWebPointPair X="20" Y="50"></zgw:ZedGraphWebPointPair>
					        <zgw:ZedGraphWebPointPair X="30" Y="30"></zgw:ZedGraphWebPointPair>
					        <zgw:ZedGraphWebPointPair X="40" Y="80"></zgw:ZedGraphWebPointPair>
					        <zgw:ZedGraphWebPointPair X="50" Y="60"></zgw:ZedGraphWebPointPair>
					        <zgw:ZedGraphWebPointPair X="60" Y="10"></zgw:ZedGraphWebPointPair>
					    </Points>
					    <Fill Color="Green"></Fill>
					</zgw:ZedGraphWebBarItem>
				</CurveList>
				<XAxis IsTicsBetweenLabels="True" Type="Ordinal" Title="Run Number"></XAxis>
				<YAxis Title="Quality Coefficient"></YAxis>
			</zgw:ZedGraphWeb>
		</form>
	</body>
</HTML>
