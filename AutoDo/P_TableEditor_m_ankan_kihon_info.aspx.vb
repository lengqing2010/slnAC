Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_ankan_kihon_info
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
           Me.lblMsg.Text = ""
        If Not IsPostBack Then


            ViewState("edp_txt") = Context.Items("edp_txt")
            ViewState("edp_no") = Context.Items("edp_no")

            ViewState("kinou_txt") = Context.Items("kinou_txt")
            ViewState("kinou_no") = Context.Items("kinou_no")


            If Not IsPostBack Then
                Dim CDB As New CDB
                Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
                Me.ucEdpLst.DataSource = dbEdpLst
            End If

            If Context.Items("edp_no") IsNot Nothing Then
                Me.ucEdpLst.Text0 = Context.Items("edp_txt")
                Me.ucEdpLst.Value0 = Context.Items("edp_no")
            End If

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
            .AppendLine(",server_siryou_path ")
            .AppendLine(",client_siryou_path ")
            .AppendLine(",code_path1 ")
            .AppendLine(",code_path2 ")
            .AppendLine(",code_path3 ")
            .AppendLine("FROM m_ankan_kihon_info")
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
        ucEdpLst.Value0 = row.Cells(1).Text.Replace("&nbsp;", "")
   'server_siryou_path nvarchar(1000)
        tbxServerSiryouPath.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'client_siryou_path nvarchar(1000)
        tbxClientSiryouPath.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'code_path1 nvarchar(1000)
        tbxCodePath1.Text = row.Cells(4).Text.Replace("&nbsp;", "")
   'code_path2 nvarchar(1000)
        tbxCodePath2.Text = row.Cells(5).Text.Replace("&nbsp;", "")
   'code_path3 nvarchar(1000)
        tbxCodePath3.Text = row.Cells(6).Text.Replace("&nbsp;", "")
       
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
            .AppendLine("UPDATE m_ankan_kihon_info")
            .AppendLine("SET")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            .AppendLine(",server_siryou_path = '" & tbxServerSiryouPath.Text & "'   ")
            .AppendLine(",client_siryou_path = '" & tbxClientSiryouPath.Text & "'   ")
            .AppendLine(",code_path1 = '" & tbxCodePath1.Text & "'   ")
            .AppendLine(",code_path2 = '" & tbxCodePath2.Text & "'   ")
            .AppendLine(",code_path3 = '" & tbxCodePath3.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
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
            .AppendLine("DELETE FROM m_ankan_kihon_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub


    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0

        Context.Items("edp_txt") = ViewState("edp_txt")
        Context.Items("edp_no") = ViewState("edp_no")

        Context.Items("kinou_txt") = ViewState("kinou_txt")
        Context.Items("kinou_no") = ViewState("kinou_no")

        Server.Transfer("AnkannKanri.aspx")
    End Sub
End Class
