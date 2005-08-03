<%@ Page Language="c#" autoeventwireup="false" Inherits="ZG1.graph" CodeBehind="graph.aspx.cs" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>graph</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <ZGW:ZEDGRAPHWEB id="ZedGraphWeb1" runat="server" width="500" Height="375"></ZGW:ZEDGRAPHWEB>
    </form>
</body>
</html>
