
Partial Class userctrl_WucLink
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property user_cd() As String
        Set(ByVal value As String)
            ViewState("user_cd") = value
        End Set
    End Property

    Protected Sub lbtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn1.Click
        Context.Items("user_cd") = ViewState("user_cd")
        Server.Transfer("Zsql.aspx")
    End Sub

    Protected Sub lbtn1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn2.Click
        Context.Items("user_cd") = ViewState("user_cd")
        Server.Transfer("Zctrl.aspx")
    End Sub

    Protected Sub lbtn3_Click(sender As Object, e As System.EventArgs) Handles lbtn3.Click
        Context.Items("user_cd") = ViewState("user_cd")
        Server.Transfer("ZTables.aspx")
    End Sub
End Class
