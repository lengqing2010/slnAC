
Imports System.Data

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load



    End Sub

    ''' <summary>
    ''' Login Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnLogin_Click(sender As Object, e As System.EventArgs) Handles btnLogin.Click
        Dim nm As String = Me.tbxName.Text.Trim.ToLower
        Dim ps As String = Me.btxPass.Text.Trim.ToLower
        Dim msSql As New CMsSql()
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT *")
            .AppendLine("FROM m_user")
            .AppendLine("WHERE")
            .AppendLine("       user_id  = '" & nm & "'")
            .AppendLine("	AND password = '" & ps & "'")
        End With

        Dim dt As DataTable = msSql.ExecSelect(sb.ToString)
        If dt.Rows.Count > 0 Then
            Response.Write("OK")
            Context.Items("user_cd") = nm
            Server.Transfer("Zctrl.aspx")
        Else
            C.SMsg(Page, "User does not exist~!")
        End If
    End Sub


End Class
