Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_ankan_kinou_info
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
            .AppendLine("edp_no ")
            .AppendLine(",kinou_no ")
            .AppendLine(",kinou_mei ")
            .AppendLine(",kinou_kbn ")
            .AppendLine(",yotei_kousuu ")
            .AppendLine(",yotei_start_date ")
            .AppendLine(",yotei_end_date ")
            .AppendLine("FROM m_ankan_kinou_info")
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
   'edp_no nvarchar(100)
   tbxEdpNo.Text = row.Cells(1).Text
   'kinou_no nvarchar(100)
   tbxKinouNo.Text = row.Cells(2).Text
   'kinou_mei nvarchar(1000)
   tbxKinouMei.Text = row.Cells(3).Text
   'kinou_kbn nvarchar(2)
   tbxKinouKbn.Text = row.Cells(4).Text
   'yotei_kousuu numeric(5)
   tbxYoteiKousuu.Text = row.Cells(5).Text
   'yotei_start_date datetime(8)
   tbxYoteiStartDate.Text = row.Cells(6).Text
   'yotei_end_date datetime(8)
   tbxYoteiEndDate.Text = row.Cells(7).Text
       
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
            .AppendLine("UPDATE m_ankan_kinou_info")
            .AppendLine("SET")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine(",kinou_no = '" & tbxKinouNo.Text & "'   ")
            .AppendLine(",kinou_mei = '" & tbxKinouMei.Text & "'   ")
            .AppendLine(",kinou_kbn = '" & tbxKinouKbn.Text & "'   ")
            .AppendLine(",yotei_kousuu = '" & tbxYoteiKousuu.Text & "'   ")
            .AppendLine(",yotei_start_date = '" & tbxYoteiStartDate.Text & "'   ")
            .AppendLine(",yotei_end_date = '" & tbxYoteiEndDate.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine("AND kinou_no = '" & tbxKinouNo.Text & "'   ")
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
            .AppendLine("INSERT INTO m_ankan_kinou_info")
            .AppendLine("(")
            .AppendLine("edp_no ")
            .AppendLine(",kinou_no ")
            .AppendLine(",kinou_mei ")
            .AppendLine(",kinou_kbn ")
            .AppendLine(",yotei_kousuu ")
            .AppendLine(",yotei_start_date ")
            .AppendLine(",yotei_end_date ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxEdpNo.Text & "'   ")
            .AppendLine(",  N'" & tbxKinouNo.Text & "'   ")
            .AppendLine(",  N'" & tbxKinouMei.Text & "'   ")
            .AppendLine(",  N'" & tbxKinouKbn.Text & "'   ")
            .AppendLine(",  N'" & tbxYoteiKousuu.Text & "'   ")
            .AppendLine(",  N'" & tbxYoteiStartDate.Text & "'   ")
            .AppendLine(",  N'" & tbxYoteiEndDate.Text & "'   ")
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
            .AppendLine("DELETE FROM m_ankan_kinou_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine("AND kinou_no = '" & tbxKinouNo.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
End Class
