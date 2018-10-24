Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_edp_test_temp
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
            .AppendLine("SELECT TOP 1000")
            .AppendLine("edp_no ")
            .AppendLine(",edp_mei ")
            .AppendLine(",edp_exp ")
            .AppendLine(",idx ")
            .AppendLine(",status ")
            .AppendLine(",status2 ")
            .AppendLine("FROM m_edp_test")
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
   'EDP NUMBER nvarchar(200)
   tbxEdpNo.Text = row.Cells(1).Text.Replace("&nbsp;", "")
   'EDP 名 nvarchar(800)
   tbxEdpMei.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'EDP 説明 nvarchar(4000)
   tbxEdpExp.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'ｉｎｄｅｘ int(4)
   tbxIdx.Text = row.Cells(4).Text.Replace("&nbsp;", "")
   'ステータス        nchar(4)
   tbxStatus.Text = row.Cells(5).Text.Replace("&nbsp;", "")
   '－ＳＴＡＴＵＳ２ nchar(6)
   tbxStatus2.Text = row.Cells(6).Text.Replace("&nbsp;", "")
       
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
            .AppendLine("UPDATE m_edp_test")
            .AppendLine("SET")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine(",edp_mei = '" & tbxEdpMei.Text & "'   ")
            .AppendLine(",edp_exp = '" & tbxEdpExp.Text & "'   ")
            .AppendLine(",idx = '" & tbxIdx.Text & "'   ")
            .AppendLine(",status = '" & tbxStatus.Text & "'   ")
            .AppendLine(",status2 = '" & tbxStatus2.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & hidEdpNo.Text & "'   ")
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
            .AppendLine("INSERT INTO m_edp_test")
            .AppendLine("(")
            .AppendLine("edp_no ")
            .AppendLine(",edp_mei ")
            .AppendLine(",edp_exp ")
            .AppendLine(",idx ")
            .AppendLine(",status ")
            .AppendLine(",status2 ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxEdpNo.Text & "'   ")
            .AppendLine(",  N'" & tbxEdpMei.Text & "'   ")
            .AppendLine(",  N'" & tbxEdpExp.Text & "'   ")
            .AppendLine(",  N'" & tbxIdx.Text & "'   ")
            .AppendLine(",  N'" & tbxStatus.Text & "'   ")
            .AppendLine(",  N'" & tbxStatus2.Text & "'   ")
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
            .AppendLine("DELETE FROM m_edp_test")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & hidEdpNo.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
End Class
