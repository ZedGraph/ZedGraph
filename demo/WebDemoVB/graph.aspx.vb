Public Class graph
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ZedGraphWeb1 As ZedGraph.ZedGraphWeb

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub ZedGraphWeb1_RenderGraph(ByVal g As System.Drawing.Graphics, ByVal pane As ZedGraph.GraphPane) Handles ZedGraphWeb1.RenderGraph
        Dim Val As String
        Val = Page.Request.Params.Get("graph")
        If (Val = "2") Then
            ZedGraph.ZedGraphWeb.RenderDemo(g, pane)
            pane.Title = "Graph Number 2"
        Else
            ZedGraph.ZedGraphWeb.RenderDemo(g, pane)
            pane.Title = "Graph Number 1"
        End If
    End Sub
End Class
