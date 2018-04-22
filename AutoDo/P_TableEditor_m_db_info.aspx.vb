Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_db_info
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
            .AppendLine("data_source ")
            .AppendLine(",db_name ")
            .AppendLine(",db_type ")
            .AppendLine(",db_user_id ")
            .AppendLine(",db_password ")
            .AppendLine(",db_enlist ")
            .AppendLine(",db_conn ")
            .AppendLine(",db_exp ")
            .AppendLine("FROM m_db_info")
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
   'data_source varchar(100)
        tbxDataSource.Text = row.Cells(1).Text.Replace("&nbsp;", "")
   'db_name varchar(100)
        tbxDbName.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'db_type varchar(20)
        tbxDbType.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'db_user_id varchar(100)
        tbxDbUserId.Text = row.Cells(4).Text.Replace("&nbsp;", "")
   'db_password varchar(100)
        tbxDbPassword.Text = row.Cells(5).Text.Replace("&nbsp;", "")
   'db_enlist varchar(5)
        tbxDbEnlist.Text = row.Cells(6).Text.Replace("&nbsp;", "")
   'db_conn varchar(500)
        tbxDbConn.Text = row.Cells(7).Text.Replace("&nbsp;", "")
   'db_exp varchar(1000)
        tbxDbExp.Text = row.Cells(8).Text.Replace("&nbsp;", "")
       
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
            .AppendLine("UPDATE m_db_info")
            .AppendLine("SET")
            .AppendLine("data_source = '" & tbxDataSource.Text & "'   ")
            .AppendLine(",db_name = '" & tbxDbName.Text & "'   ")
            .AppendLine(",db_type = '" & tbxDbType.Text & "'   ")
            .AppendLine(",db_user_id = '" & tbxDbUserId.Text & "'   ")
            .AppendLine(",db_password = '" & tbxDbPassword.Text & "'   ")
            .AppendLine(",db_enlist = '" & tbxDbEnlist.Text & "'   ")
            .AppendLine(",db_conn = '" & tbxDbConn.Text & "'   ")
            .AppendLine(",db_exp = '" & tbxDbExp.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("data_source = '" & tbxDataSource.Text & "'   ")
            .AppendLine("AND db_name = '" & tbxDbName.Text & "'   ")
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
            .AppendLine("INSERT INTO m_db_info")
            .AppendLine("(")
            .AppendLine("data_source ")
            .AppendLine(",db_name ")
            .AppendLine(",db_type ")
            .AppendLine(",db_user_id ")
            .AppendLine(",db_password ")
            .AppendLine(",db_enlist ")
            .AppendLine(",db_conn ")
            .AppendLine(",db_exp ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxDataSource.Text & "'   ")
            .AppendLine(",  N'" & tbxDbName.Text & "'   ")
            .AppendLine(",  N'" & tbxDbType.Text & "'   ")
            .AppendLine(",  N'" & tbxDbUserId.Text & "'   ")
            .AppendLine(",  N'" & tbxDbPassword.Text & "'   ")
            .AppendLine(",  N'" & tbxDbEnlist.Text & "'   ")
            .AppendLine(",  N'" & tbxDbConn.Text & "'   ")
            .AppendLine(",  N'" & tbxDbExp.Text & "'   ")
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
            .AppendLine("DELETE FROM m_db_info")
            .AppendLine("WHERE")
            .AppendLine("data_source = '" & tbxDataSource.Text & "'   ")
            .AppendLine("AND db_name = '" & tbxDbName.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
End Class
