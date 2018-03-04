﻿Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_edp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '明細設定
            Dim dt As DataTable = GetMsData()
            Me.gvMs.DataSource = dt
            Me.gvMs.DataBind()
        End If
  

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
            .AppendLine("FROM Table")
        End With

        Dim msSql As New CMsSql()
        Dim dt As DataTable = msSql.ExecSelect(sb.ToString)
        Return dt
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
   tbxEdpNo.Text = row.Cells(1).Text
   'edp_mei varchar(200)
   tbxEdpMei.Text = row.Cells(2).Text
   'edp_exp varchar(1000)
   tbxEdpExp.Text = row.Cells(3).Text
       
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
            .AppendLine("SELECT edp_no,edp_no+' '+edp_mei")
            .AppendLine("FROM [m_edp]")
            .AppendLine("ORDER BY [edp_no] desc")
        End With

        Dim msSql As New CMsSql()
        msSql.ExecuteNonQuery(sb.ToString)

    End Sub
    ''' <summary>
    ''' 登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click

    End Sub
    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click

    End Sub
End Class
