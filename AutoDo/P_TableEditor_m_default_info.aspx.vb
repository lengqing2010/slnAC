Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_default_info
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
           Me.lblMsg.Text = ""
        If Not IsPostBack Then
            '明細設定
            MsInit()
        End If
  

    End Sub
    public Sub MsInit()

            '明細設定
            Dim dt As DataTable = GetMsData()
            Me.gvMs.DataSource = dt
            Me.gvMs.DataBind()
  

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
            .AppendLine("user_id ")
            .AppendLine(",data_source ")
            .AppendLine(",db_name ")
            .AppendLine(",edp_no ")
            .AppendLine("FROM m_default_info")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        Return DbResult.Data
    End Function


    ''' <summary>
    ''' 行選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMs_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvMs.SelectedIndexChanged

        Dim row As GridViewRow = gvMs.SelectedRow
   'user_id varchar(100)
        tbxUserId.Text = row.Cells(1).Text.Replace("&nbsp;", "")
   'data_source varchar(100)
        tbxDataSource.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'db_name varchar(100)
        tbxDbName.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'edp_no varchar(20)
        tbxEdpNo.Text = row.Cells(4).Text.Replace("&nbsp;", "")
       
    End Sub

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click

        Dim sb As New StringBuilder
        With sb
            .AppendLine("UPDATE m_default_info")
            .AppendLine("SET")
            .AppendLine("user_id = '" & tbxUserId.Text & "'   ")
            .AppendLine(",data_source = '" & tbxDataSource.Text & "'   ")
            .AppendLine(",db_name = '" & tbxDbName.Text & "'   ")
            .AppendLine(",edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("user_id = '" & tbxUserId.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
    ''' <summary>
    ''' 登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click
        Dim sb As New StringBuilder
        With sb
            .AppendLine("INSERT INTO m_default_info")
            .AppendLine("(")
            .AppendLine("user_id ")
            .AppendLine(",data_source ")
            .AppendLine(",db_name ")
            .AppendLine(",edp_no ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxUserId.Text & "'   ")
            .AppendLine(",  N'" & tbxDataSource.Text & "'   ")
            .AppendLine(",  N'" & tbxDbName.Text & "'   ")
            .AppendLine(",  N'" & tbxEdpNo.Text & "'   ")
            .AppendLine(")")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim sb As New StringBuilder
        With sb
            .AppendLine("DELETE FROM m_default_info")
            .AppendLine("WHERE")
            .AppendLine("user_id = '" & tbxUserId.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
End Class
