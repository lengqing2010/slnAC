Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_ankan_pgm_info
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
            .AppendLine(",pgm_id ")
            .AppendLine(",pgm_name ")
            .AppendLine(",pgm_level ")
            .AppendLine(",pgm_santaku_flg ")
            .AppendLine(",pgm_sinntyoku_retu ")
            .AppendLine(",pgm_last_upd_date ")
            .AppendLine(",pgm_staus ")
            .AppendLine("FROM m_ankan_pgm_info")
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
   'pgm_id nvarchar(100)
   tbxPgmId.Text = row.Cells(3).Text
   'pgm_name nvarchar(1000)
   tbxPgmName.Text = row.Cells(4).Text
   'pgm_level nvarchar(2)
   tbxPgmLevel.Text = row.Cells(5).Text
   'pgm_santaku_flg nvarchar(2)
   tbxPgmSantakuFlg.Text = row.Cells(6).Text
   'pgm_sinntyoku_retu numeric(5)
   tbxPgmSinntyokuRetu.Text = row.Cells(7).Text
   'pgm_last_upd_date nvarchar(2)
   tbxPgmLastUpdDate.Text = row.Cells(8).Text
   'pgm_staus datetime(8)
   tbxPgmStaus.Text = row.Cells(9).Text
       
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
            .AppendLine("UPDATE m_ankan_pgm_info")
            .AppendLine("SET")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine(",kinou_no = '" & tbxKinouNo.Text & "'   ")
            .AppendLine(",pgm_id = '" & tbxPgmId.Text & "'   ")
            .AppendLine(",pgm_name = '" & tbxPgmName.Text & "'   ")
            .AppendLine(",pgm_level = '" & tbxPgmLevel.Text & "'   ")
            .AppendLine(",pgm_santaku_flg = '" & tbxPgmSantakuFlg.Text & "'   ")
            .AppendLine(",pgm_sinntyoku_retu = '" & tbxPgmSinntyokuRetu.Text & "'   ")
            .AppendLine(",pgm_last_upd_date = '" & tbxPgmLastUpdDate.Text & "'   ")
            .AppendLine(",pgm_staus = '" & tbxPgmStaus.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine("AND kinou_no = '" & tbxKinouNo.Text & "'   ")
            .AppendLine("AND pgm_id = '" & tbxPgmId.Text & "'   ")
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
            .AppendLine("INSERT INTO m_ankan_pgm_info")
            .AppendLine("(")
            .AppendLine("edp_no ")
            .AppendLine(",kinou_no ")
            .AppendLine(",pgm_id ")
            .AppendLine(",pgm_name ")
            .AppendLine(",pgm_level ")
            .AppendLine(",pgm_santaku_flg ")
            .AppendLine(",pgm_sinntyoku_retu ")
            .AppendLine(",pgm_last_upd_date ")
            .AppendLine(",pgm_staus ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxEdpNo.Text & "'   ")
            .AppendLine(",  N'" & tbxKinouNo.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmId.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmName.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmLevel.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmSantakuFlg.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmSinntyokuRetu.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmLastUpdDate.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmStaus.Text & "'   ")
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
            .AppendLine("DELETE FROM m_ankan_pgm_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine("AND kinou_no = '" & tbxKinouNo.Text & "'   ")
            .AppendLine("AND pgm_id = '" & tbxPgmId.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
End Class
