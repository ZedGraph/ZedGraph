<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<%@ Page language="c#" Codebehind="Graph2.aspx.cs" AutoEventWireup="false" Inherits="ZG1.Graph2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Graph2</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<zgw:ZedGraphWeb id="ZedGraphWeb1" runat="server" Height="400" Width="600" BarType="Overlay" BarBase="X"
				title="Sample Pie Chart">
				<AxisFill RangeMax="0" Type="Brush" AlignH="Center" IsScaled="True" Color="Salmon" RangeMin="0"
					IsVisible="True" AlignV="Center"></AxisFill>
				<Y2Axis IsOmitMag="False" MinGrace="0.1" MinSpace="0" CrossAuto="False" IsUseTenPower="False"
					IsShowTitle="True" MinorStepAuto="True" MinorGridDashOff="10" MaxAuto="True" MinorGridPenWidth="1"
					IsTic="True" MinorGridColor="Gray" IsReverse="False" IsMinorTic="True" IsMinorInsideTic="True"
					Cross="0" IsTicsBetweenLabels="True" ScaleMag="0" IsOppositeTic="True" TicPenWidth="1"
					GridDashOff="5" IsShowMinorGrid="False" NumDec="0" ScaleAlign="Center" MinAuto="True"
					Type="Linear" IsInsideTic="True" MinorGridDashOn="1" IsVisible="False" ScaleFormatAuto="False"
					IsShowGrid="False" Title="" MaxGrace="0.1" Color="Black" GridPenWidth="1" ScaleMagAuto="True"
					IsPreventLabelOverlap="False" IsMinorOppositeTic="True" ScaleFormat="g" StepAuto="True"
					TicSize="5" IsZeroLine="True" GridDashOn="1" GridColor="Black" NumDecAuto="True" MinorTicSize="2.5">
					<ScaleFontSpec Size="14" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="False">
						<Fill RangeMax="0" Type="None" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</ScaleFontSpec>
					<TitleFontSpec Size="14" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="True">
						<Fill RangeMax="0" Type="None" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</TitleFontSpec>
				</Y2Axis>
				<PaneBorder IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></PaneBorder>
				<FontSpec Size="16" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
					StringAlignment="Center" IsBold="True">
					<Fill RangeMax="0" Type="Solid" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
						IsVisible="True" AlignV="Center"></Fill>
					<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
				</FontSpec>
				<AxisBorder IsVisible="True" PenWidth="1" InflateFactor="0" Color="Black"></AxisBorder>
				<Legend IsHStack="True" Position="Top" IsVisible="False">
					<Fill RangeMax="0" Type="Brush" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
						IsVisible="True" AlignV="Center"></Fill>
					<Rect Y="0" Height="0" Width="0" X="0"></Rect>
					<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					<FontSpec Size="12" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="False">
						<Fill RangeMax="0" Type="Solid" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</FontSpec>
					<Location CoordinateFrame="AxisFraction" Height="0" AlignH="Left" X1="0" Y1="0" X="0" Y="0"
						Width="0" AlignV="Center">
						<BottomRight Y="0" X="0"></BottomRight>
						<Rect Y="0" Height="0" Width="0" X="0"></Rect>
						<TopLeft Y="0" X="0"></TopLeft>
					</Location>
				</Legend>
				<YAxis IsOmitMag="False" MinGrace="0.1" MinSpace="0" CrossAuto="False" IsUseTenPower="False"
					IsShowTitle="True" MinorStepAuto="True" MinorGridDashOff="10" MaxAuto="True" MinorGridPenWidth="1"
					IsTic="True" MinorGridColor="Gray" IsReverse="False" IsMinorTic="True" IsMinorInsideTic="True"
					Cross="0" IsTicsBetweenLabels="True" ScaleMag="0" IsOppositeTic="True" TicPenWidth="1"
					GridDashOff="5" IsShowMinorGrid="False" NumDec="0" ScaleAlign="Center" MinAuto="True"
					Type="Linear" IsInsideTic="True" MinorGridDashOn="1" IsVisible="False" ScaleFormatAuto="False"
					IsShowGrid="False" Title="" MaxGrace="0.1" Color="Black" GridPenWidth="1" ScaleMagAuto="True"
					IsPreventLabelOverlap="False" IsMinorOppositeTic="True" ScaleFormat="g" StepAuto="True"
					TicSize="5" IsZeroLine="True" GridDashOn="1" GridColor="Black" NumDecAuto="True" MinorTicSize="2.5">
					<ScaleFontSpec Size="14" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="False">
						<Fill RangeMax="0" Type="None" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</ScaleFontSpec>
					<TitleFontSpec Size="14" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="True">
						<Fill RangeMax="0" Type="None" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</TitleFontSpec>
				</YAxis>
				<AxisRect Y="0" Height="0" Width="0" X="0"></AxisRect>
				<PaneFill RangeMax="0" Type="Solid" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
					IsVisible="True" AlignV="Center"></PaneFill>
				<CurveList>
					<zgw:ZedGraphWebPieItem Displacement="0.3" DataMember="" IsLegendLabelVisible="True" Value="80" PercentDecimalDigits="0"
						LabelType="Name_Percent" Label="Red" Color="Red" ValueDecimalDigits="0" IsY2Axis="False" IsVisible="True">
						<Border IsVisible="True" PenWidth="1" InflateFactor="0" Color="Black"></Border>
						<LabelDetail IsVisible="True" ZOrder="A_InFront" Text="">
							<Location CoordinateFrame="AxisFraction" Height="0" AlignH="Left" X1="0" Y1="0" X="0" Y="0"
								Width="0" AlignV="Center">
								<BottomRight Y="0" X="0"></BottomRight>
								<Rect Y="0" Height="0" Width="0" X="0"></Rect>
								<TopLeft Y="0" X="0"></TopLeft>
							</Location>
							<FontSpec Size="10" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
								StringAlignment="Center" IsBold="False">
								<Fill RangeMax="0" Type="Solid" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
									IsVisible="True" AlignV="Center"></Fill>
								<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
							</FontSpec>
							<LayoutArea Height="0" Width="0"></LayoutArea>
						</LabelDetail>
					</zgw:ZedGraphWebPieItem>
					<zgw:ZedGraphWebPieItem Displacement="0" DataMember="" IsLegendLabelVisible="True" Value="20" PercentDecimalDigits="0"
						LabelType="Name_Percent" Label="Blue" Color="RoyalBlue" ValueDecimalDigits="0" IsY2Axis="False" IsVisible="True">
						<Border IsVisible="True" PenWidth="1" InflateFactor="0" Color="Black"></Border>
						<LabelDetail IsVisible="True" ZOrder="A_InFront" Text="">
							<Location CoordinateFrame="AxisFraction" Height="0" AlignH="Left" X1="0" Y1="0" X="0" Y="0"
								Width="0" AlignV="Center">
								<BottomRight Y="0" X="0"></BottomRight>
								<Rect Y="0" Height="0" Width="0" X="0"></Rect>
								<TopLeft Y="0" X="0"></TopLeft>
							</Location>
							<FontSpec Size="10" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
								StringAlignment="Center" IsBold="False">
								<Fill RangeMax="0" Type="Solid" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
									IsVisible="True" AlignV="Center"></Fill>
								<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
							</FontSpec>
							<LayoutArea Height="0" Width="0"></LayoutArea>
						</LabelDetail>
					</zgw:ZedGraphWebPieItem>
				</CurveList>
				<XAxis IsOmitMag="False" MinGrace="0.1" MinSpace="0" CrossAuto="False" IsUseTenPower="False"
					IsShowTitle="False" MinorStepAuto="True" MinorGridDashOff="10" MaxAuto="True" MinorGridPenWidth="1"
					IsTic="False" MinorGridColor="Gray" IsReverse="False" IsMinorTic="True" IsMinorInsideTic="True"
					Cross="0" IsTicsBetweenLabels="False" ScaleMag="0" IsOppositeTic="True" TicPenWidth="1"
					GridDashOff="5" IsShowMinorGrid="False" NumDec="0" ScaleAlign="Center" MinAuto="True"
					Type="Linear" IsInsideTic="False" MinorGridDashOn="1" IsVisible="False" ScaleFormatAuto="False"
					IsShowGrid="False" Title="" MaxGrace="0.1" Color="Black" GridPenWidth="1" ScaleMagAuto="True"
					IsPreventLabelOverlap="False" IsMinorOppositeTic="True" ScaleFormat="g" StepAuto="True"
					TicSize="5" IsZeroLine="False" GridDashOn="1" GridColor="Black" NumDecAuto="True" MinorTicSize="2.5">
					<ScaleFontSpec Size="14" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="False">
						<Fill RangeMax="0" Type="None" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</ScaleFontSpec>
					<TitleFontSpec Size="14" IsUnderline="False" IsItalic="False" Family="Arial" Angle="0" FontColor="Black"
						StringAlignment="Center" IsBold="True">
						<Fill RangeMax="0" Type="None" AlignH="Center" IsScaled="True" Color="White" RangeMin="0"
							IsVisible="True" AlignV="Center"></Fill>
						<Border IsVisible="False" PenWidth="1" InflateFactor="0" Color="Black"></Border>
					</TitleFontSpec>
				</XAxis>
				<PieRect Y="0" Height="0" Width="0" X="0"></PieRect>
			</zgw:ZedGraphWeb>
		</form>
	</body>
</HTML>
