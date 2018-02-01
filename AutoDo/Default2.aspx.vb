
Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        Response.Write(CType(sender, TreeView).SelectedValue)


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim node As New TreeNode
            node.Text = "ffff"
            node.Value = "ffff11"
            Me.TreeView1.Nodes.Add(node)


            Dim node2 As New TreeNode
            node2.Text = "fff22222f"
            node2.Value = "ffff22"
            Me.TreeView1.Nodes.Add(node2)
        End If

    End Sub
End Class
