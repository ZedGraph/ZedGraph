<%@ Page Language="c#" autoeventwireup="false" Inherits="ZG1.Graph2" CodeBehind="Graph2.aspx.cs" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<ZGW:ZEDGRAPHWEB id="ZedGraphWeb1" RenderMode="RawImage" title="Sample Pie Chart" runat="server" BarBase="X" BarType="Overlay" Height="375" width="500" AxisChanged="True" >
    <AXISFILL alignv="Center" isvisible="True" rangemin="0" color="Salmon" isscaled="True" alignh="Center" type="Brush" rangemax="0"></AXISFILL>
    <PANEBORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></PANEBORDER>
    <FONTSPEC isbold="True" stringalignment="Center" fontcolor="Black" angle="0" family="Arial" isitalic="False" isunderline="False" size="16">
        <FILL alignv="Center" isvisible="True" rangemin="0" color="White" isscaled="True" alignh="Center" type="Solid" rangemax="0"></FILL>
        <BORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></BORDER>
    </FONTSPEC>
    <AXISBORDER isvisible="True" color="Black" inflatefactor="0" penwidth="1"></AXISBORDER>
    <legend isvisible="False" position="Top" ishstack="True">
        <FILL alignv="Center" isvisible="True" rangemin="0" color="White" isscaled="True" alignh="Center" type="Brush" rangemax="0"></FILL>
        <RECT height="0" x="0" width="0" y="0"></RECT>
        <BORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></BORDER>
        <FONTSPEC isbold="False" stringalignment="Center" fontcolor="Black" angle="0" family="Arial" isitalic="False" isunderline="False" size="12">
            <FILL alignv="Center" isvisible="True" rangemin="0" color="White" isscaled="True" alignh="Center" type="Solid" rangemax="0"></FILL>
            <BORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></BORDER>
        </FONTSPEC>
        <LOCATION height="0" alignv="Center" alignh="Left" x="0" width="0" y="0" y1="0" x1="0" coordinateframe="AxisFraction">
            <BOTTOMRIGHT x="0" y="0"></BOTTOMRIGHT>
            <RECT height="0" x="0" width="0" y="0"></RECT>
            <TOPLEFT x="0" y="0"></TOPLEFT>
        </LOCATION>
    </legend>
    <AXISRECT height="0" x="0" width="0" y="0"></AXISRECT>
    <PANEFILL alignv="Center" isvisible="True" rangemin="0" color="White" isscaled="True" alignh="Center" type="Solid" rangemax="0"></PANEFILL>
    <CURVELIST>
        <ZGW:ZEDGRAPHWEBPIEITEM IsVisible="True" Color="Red" IsY2Axis="False" ValueDecimalDigits="0" Label="Red" LabelType="Name_Percent" PercentDecimalDigits="0" Value="80" IsLegendLabelVisible="True" DataMember="" Displacement="0.3">
            <BORDER isvisible="True" color="Black" inflatefactor="0" penwidth="1"></BORDER>
            <LABELDETAIL isvisible="True" text="" zorder="A_InFront">
                <LOCATION height="0" alignv="Center" alignh="Left" x="0" width="0" y="0" y1="0" x1="0" coordinateframe="AxisFraction">
                    <BOTTOMRIGHT x="0" y="0"></BOTTOMRIGHT>
                    <RECT height="0" x="0" width="0" y="0"></RECT>
                    <TOPLEFT x="0" y="0"></TOPLEFT>
                </LOCATION>
                <FONTSPEC isbold="False" stringalignment="Center" fontcolor="Black" angle="0" family="Arial" isitalic="False" isunderline="False" size="10">
                    <FILL alignv="Center" isvisible="True" rangemin="0" color="White" isscaled="True" alignh="Center" type="Solid" rangemax="0"></FILL>
                    <BORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></BORDER>
                </FONTSPEC>
                <LAYOUTAREA height="0" width="0"></LAYOUTAREA>
            </LABELDETAIL>
        </ZGW:ZEDGRAPHWEBPIEITEM>
        <ZGW:ZEDGRAPHWEBPIEITEM IsVisible="True" Color="RoyalBlue" IsY2Axis="False" ValueDecimalDigits="0" Label="Blue" LabelType="Name_Percent" PercentDecimalDigits="0" Value="20" IsLegendLabelVisible="True" DataMember="" Displacement="0">
            <BORDER isvisible="True" color="Black" inflatefactor="0" penwidth="1"></BORDER>
            <LABELDETAIL isvisible="True" text="" zorder="A_InFront">
                <LOCATION height="0" alignv="Center" alignh="Left" x="0" width="0" y="0" y1="0" x1="0" coordinateframe="AxisFraction">
                    <BOTTOMRIGHT x="0" y="0"></BOTTOMRIGHT>
                    <RECT height="0" x="0" width="0" y="0"></RECT>
                    <TOPLEFT x="0" y="0"></TOPLEFT>
                </LOCATION>
                <FONTSPEC isbold="False" stringalignment="Center" fontcolor="Black" angle="0" family="Arial" isitalic="False" isunderline="False" size="10">
                    <FILL alignv="Center" isvisible="True" rangemin="0" color="White" isscaled="True" alignh="Center" type="Solid" rangemax="0"></FILL>
                    <BORDER isvisible="False" color="Black" inflatefactor="0" penwidth="1"></BORDER>
                </FONTSPEC>
                <LAYOUTAREA height="0" width="0"></LAYOUTAREA>
            </LABELDETAIL>
        </ZGW:ZEDGRAPHWEBPIEITEM>
    </CURVELIST>
    <PIERECT height="0" x="0" width="0" y="0"></PIERECT>
</ZGW:ZEDGRAPHWEB>
