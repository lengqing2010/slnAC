
Partial Class userctrl_WucLink
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property user_cd() As String
        Set(ByVal value As String)
            ViewState("user_cd") = value
        End Set
    End Property

    Public Sub SetSelectThis(ByVal lbl As LinkButton, ByVal url As String)
        SetSelectStyle(lbl)
        Context.Items("user_cd") = ViewState("user_cd")
        Context.Items("selKey") = lbl.ID
        Server.Transfer(url)
    End Sub



    Public Sub SetSelectStyle(ByVal lbl As LinkButton)

        lbl.BackColor = Drawing.Color.White
        lbl.ForeColor = Drawing.Color.Black
    End Sub

    Public Sub SetUnSelectStyle(ByVal lbl As LinkButton)

        lbl.BackColor = Drawing.Color.Black
        lbl.ForeColor = Drawing.Color.White



    End Sub


    Protected Sub lbtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn1.Click
        SetSelectThis(sender, "Zsql.aspx")
    End Sub

    Protected Sub lbtn1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn2.Click
        SetSelectThis(sender, "Zctrl.aspx")
    End Sub

    Protected Sub lbtn3_Click(sender As Object, e As System.EventArgs) Handles lbtn3.Click
        SetSelectThis(sender, "ZTables.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            For i As Integer = 0 To Panel1.Controls.Count - 1
                If Panel1.Controls(i).GetType.Name = "LinkButton" Then
                    SetUnSelectStyle(Panel1.Controls(i))
                End If

            Next

            If Context.Items("selKey") IsNot Nothing Then
                SetSelectStyle(Panel1.FindControl(Context.Items("selKey")))
            End If

        End If

    End Sub
End Class
