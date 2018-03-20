
Partial Class AnkannKanri
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim CDB As New CDB
            Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
            Me.ucEdpLst.DataSource = dbEdpLst

            ucEdpLst.OnClick = "EdpSentaku"

        End If

    End Sub

    Public Sub EdpSentaku()
        GetMsData()
    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("edp_no ")
            .AppendLine(",server_siryou_path ")
            .AppendLine(",client_siryou_path ")
            .AppendLine(",code_path1 ")
            .AppendLine(",code_path2 ")
            .AppendLine(",code_path3 ")
            .AppendLine("FROM m_ankan_kihon_info")
            sb.AppendLine("WHERE")
            sb.AppendLine("          m_ankan_kihon_info.edp_no =     '" & ucEdpLst.Value0 & "'")
        End With



        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        Dim dt As Data.DataTable = DbResult.Data
        For idx As Integer = 0 To dt.Rows.Count - 1

            'edp_no
            'ucEdpLst.Value0 = IsNullEmpty(dt.Rows(idx).Item("edp_no").ToString())
            'server_siryou_path
            Me.tbxServerSiryouPath.Text = IsNullEmpty(dt.Rows(idx).Item("server_siryou_path").ToString())
            'client_siryou_path
            Me.tbxClientSiryouPath.Text = IsNullEmpty(dt.Rows(idx).Item("client_siryou_path").ToString())
            'code_path1
            Me.tbxCodePath1.Text = IsNullEmpty(dt.Rows(idx).Item("code_path1").ToString())
            'code_path2
            Me.tbxCodePath2.Text = IsNullEmpty(dt.Rows(idx).Item("code_path2").ToString()) 
            'code_path3
            Me.tbxCodePath3.Text = IsNullEmpty(dt.Rows(idx).Item("code_path3").ToString())



        Next



        'Return DbResult.Data
    End Function

    Function IsNullEmpty(ByVal v As Object) As String
        If v Is DBNull.Value Then
            Return ""
        Else
            Return v
        End If
    End Function





    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click

        Dim sb As New StringBuilder
        With sb
            '.AppendLine("UPDATE m_ankan_kihon_info")
            '.AppendLine("SET")
            '.AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            '.AppendLine(",server_siryou_path = '" & tbxServerSiryouPath.Text & "'   ")
            '.AppendLine(",client_siryou_path = '" & tbxClientSiryouPath.Text & "'   ")
            '.AppendLine(",code_path1 = '" & tbxCodePath1.Text & "'   ")
            '.AppendLine(",code_path2 = '" & tbxCodePath2.Text & "'   ")
            '.AppendLine(",code_path3 = '" & tbxCodePath3.Text & "'   ")
            '.AppendLine("WHERE")
            '.AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")

            .AppendLine("DELETE FROM m_ankan_kihon_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")


            .AppendLine("INSERT INTO m_ankan_kihon_info")
            .AppendLine("(")
            .AppendLine("edp_no ")
            .AppendLine(",server_siryou_path ")
            .AppendLine(",client_siryou_path ")
            .AppendLine(",code_path1 ")
            .AppendLine(",code_path2 ")
            .AppendLine(",code_path3 ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
            .AppendLine(",  N'" & tbxServerSiryouPath.Text & "'   ")
            .AppendLine(",  N'" & tbxClientSiryouPath.Text & "'   ")
            .AppendLine(",  N'" & tbxCodePath1.Text & "'   ")
            .AppendLine(",  N'" & tbxCodePath2.Text & "'   ")
            .AppendLine(",  N'" & tbxCodePath3.Text & "'   ")
            .AppendLine(")")

        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
    End Sub

End Class
