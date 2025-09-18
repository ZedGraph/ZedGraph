<%@ Page Language="vb" AutoEventWireup="false" Codebehind="graph2.aspx.vb" Inherits="ZGBasic.Graph2"%>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Graph2</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <ZGW:ZEDGRAPHWEB id="ZedGraphWeb1" title="Sample Pie Chart" runat="server" BarBase="X" BarType="Overlay" Height="375" width="500" AxisChanged="True" >
            <AXISFILL alignv="Center" isvisible="True" rangemin="0" color="Salmon" isscaled="True" alignh="Center" type="Brush" rangemax="0"></AXISFILL>
            <Y2AXIS title="" isvisible="False" color="Black" type="Linear" minorticsize="2.5" gridcolor="Black" griddashon="1" iszeroline="True" ticsize="5" stepauto="True" scaleformat="g" isminoroppositetic="True" ispreventlabeloverlap="False" scalemagauto="True" gridpenwidth="1" maxgrace="0.1" isshowgrid="False" scaleformatauto="False" minorgriddashon="1" isinsidetic="True" minauto="True" scalealign="Center" isshowminorgrid="False" griddashoff="5" ticpenwidth="1" isoppositetic="True" scalemag="0" isticsbetweenlabels="True" cross="0" isminorinsidetic="True" isminortic="True" isreverse="False" minorgridcolor="Gray" istic="True" minorgridpenwidth="1" maxauto="True" minorgriddashoff="10" minorstepauto="True" isshowtitle="True" isusetenpower="False" crossauto="False" minspace="0" mingrace="0.1" isomitmag="False">
            </Y2AXIS>
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
            <YAXIS title="" isvisible="False" color="Black" type="Linear" minorticsize="2.5" gridcolor="Black" griddashon="1" iszeroline="True" ticsize="5" stepauto="True" scaleformat="g" isminoroppositetic="True" ispreventlabeloverlap="False" scalemagauto="True" gridpenwidth="1" maxgrace="0.1" isshowgrid="False" scaleformatauto="False" minorgriddashon="1" isinsidetic="True" minauto="True" scalealign="Center" isshowminorgrid="False" griddashoff="5" ticpenwidth="1" isoppositetic="True" scalemag="0" isticsbetweenlabels="True" cross="0" isminorinsidetic="True" isminortic="True" isreverse="False" minorgridcolor="Gray" istic="True" minorgridpenwidth="1" maxauto="True" minorgriddashoff="10" minorstepauto="True" isshowtitle="True" isusetenpower="False" crossauto="False" minspace="0" mingrace="0.1" isomitmag="False">
            </YAXIS>
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
            <XAXIS title="" isvisible="False" color="Black" type="Linear" minorticsize="2.5" gridcolor="Black" griddashon="1" iszeroline="False" ticsize="5" stepauto="True" scaleformat="g" isminoroppositetic="True" ispreventlabeloverlap="False" scalemagauto="True" gridpenwidth="1" maxgrace="0.1" isshowgrid="False" scaleformatauto="False" minorgriddashon="1" isinsidetic="False" minauto="True" scalealign="Center" isshowminorgrid="False" griddashoff="5" ticpenwidth="1" isoppositetic="True" scalemag="0" isticsbetweenlabels="False" cross="0" isminorinsidetic="True" isminortic="True" isreverse="False" minorgridcolor="Gray" istic="False" minorgridpenwidth="1" maxauto="True" minorgriddashoff="10" minorstepauto="True" isshowtitle="False" isusetenpower="False" crossauto="False" minspace="0" mingrace="0.1" isomitmag="False">
            </XAXIS>
            <PIERECT height="0" x="0" width="0" y="0"></PIERECT>
        </ZGW:ZEDGRAPHWEB>
    </form>
</body>
</html>
