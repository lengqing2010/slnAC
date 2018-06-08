Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_edp
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
            .AppendLine(",edp_mei ")
            .AppendLine(",edp_exp ")
            .AppendLine("FROM m_edp")
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
   'edp_no varchar(20)
        tbxEdpNo.Text = row.Cells(1).Text.Replace("&nbsp;", "")
   'edp_mei varchar(200)
        tbxEdpMei.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'edp_exp varchar(1000)
        tbxEdpExp.Text = row.Cells(3).Text.Replace("&nbsp;", "")
       
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
            .AppendLine("UPDATE m_edp")
            .AppendLine("SET")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
            .AppendLine(",edp_mei = '" & tbxEdpMei.Text & "'   ")
            .AppendLine(",edp_exp = '" & tbxEdpExp.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
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
            .AppendLine("INSERT INTO m_edp")
            .AppendLine("(")
            .AppendLine("edp_no ")
            .AppendLine(",edp_mei ")
            .AppendLine(",edp_exp ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & tbxEdpNo.Text & "'   ")
            .AppendLine(",  N'" & tbxEdpMei.Text & "'   ")
            .AppendLine(",  N'" & tbxEdpExp.Text & "'   ")
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
            .AppendLine("DELETE FROM m_edp")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & tbxEdpNo.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub


    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Context.Items("edp_no") = Me.tbxEdpNo.Text

        Server.Transfer("AnkannKanri.aspx")
    End Sub

End Class
