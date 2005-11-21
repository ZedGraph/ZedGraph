<%@ Page Language="c#" codebehind="Default.aspx.cs" autoeventwireup="false" Inherits="ZG1._Default" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Default</title>
    <style>
    * { margin: 0; paddin: 0: }
    body { font-size: 14px; font-face: verdana; line-height: 20px }
    img { margin-left: 5em }
    </style>
</head>
<body>
		Demo graph, using Raw mode.<br>
		In raw mode the image is generated and sent to the browser on the fly (right click the image, select properties and look at the URL).<br><br>
        <img src="graph.aspx?graph=1" />
        <br /><br>

        Default render mode is now ImageTag.<br>
        In this mode an IMG tag is generated in-place, and the image is generated and saved in the specified folder (RenderedImagePath property, default to ~/ZedGraphImages/). Combined with the CacheDuration property it becomes powerful.<br>
        Note: the path must already exist, be under the website root and be writable.<br>
        <br>
        <ZGW:ZEDGRAPHWEB id="ZedGraphWeb1" runat="server" width="500" Height="375"></ZGW:ZEDGRAPHWEB>
        <br><br>

		Other demo graphs using Raw mode.<br>
		<br>
        <img src="graph.aspx?graph=2" />
        <br />
        <img src="graph2.aspx" />
        <br />
        <img src="graph3.aspx" />
</body>
</html>
