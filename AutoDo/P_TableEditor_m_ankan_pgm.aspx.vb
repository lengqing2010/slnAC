Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_ankan_pgm
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
            .AppendLine("pgm_bunrui_cd ")
            .AppendLine(",pgm_bunrui_name ")
            .AppendLine(",pgm_id ")
            .AppendLine(",pgm_name ")
            .AppendLine(",pgm_level ")
            .AppendLine(",pgm_demo_path ")
            .AppendLine("FROM m_ankan_pgm")
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
   'pgm_bunrui_cd nvarchar(100)
        tbxPgmBunruiCd.Text = row.Cells(1).Text.Replace("&nbsp;", "")
   'pgm_bunrui_name nvarchar(1000)
        tbxPgmBunruiName.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'pgm_id nvarchar(100)
        tbxPgmId.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'pgm_name nvarchar(1000)
        tbxPgmName.Text = row.Cells(4).Text.Replace("&nbsp;", "")
   'pgm_level nvarchar(2)
        tbxPgmLevel.Text = row.Cells(5).Text.Replace("&nbsp;", "")
   'pgm_demo_path nvarchar(1000)
        tbxPgmDemoPath.Text = row.Cells(6).Text.Replace("&nbsp;", "")
       
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
            .AppendLine("UPDATE m_ankan_pgm")
            .AppendLine("SET")
            .AppendLine("pgm_bunrui_cd = '" & tbxPgmBunruiCd.Text & "'   ")
            .AppendLine(",pgm_bunrui_name = '" & tbxPgmBunruiName.Text & "'   ")
            .AppendLine(",pgm_id = '" & tbxPgmId.Text & "'   ")
            .AppendLine(",pgm_name = '" & tbxPgmName.Text & "'   ")
            .AppendLine(",pgm_level = '" & tbxPgmLevel.Text & "'   ")
            .AppendLine(",pgm_demo_path = '" & tbxPgmDemoPath.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("pgm_bunrui_cd = '" & tbxPgmBunruiCd.Text & "'   ")
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
            .AppendLine("INSERT INTO m_ankan_pgm")
            .AppendLine("(")
            .AppendLine("pgm_bunrui_cd ")
            .AppendLine(",pgm_bunrui_name ")
            .AppendLine(",pgm_id ")
            .AppendLine(",pgm_name ")
            .AppendLine(",pgm_level ")
            .AppendLine(",pgm_demo_path ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxPgmBunruiCd.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmBunruiName.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmId.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmName.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmLevel.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmDemoPath.Text & "'   ")
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
            .AppendLine("DELETE FROM m_ankan_pgm")
            .AppendLine("WHERE")
            .AppendLine("pgm_bunrui_cd = '" & tbxPgmBunruiCd.Text & "'   ")
            .AppendLine("AND pgm_id = '" & tbxPgmId.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub
End Class
