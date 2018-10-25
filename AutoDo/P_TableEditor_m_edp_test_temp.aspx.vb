Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_edp_test_temp
    Inherits System.Web.UI.Page

    Public BC As New MEdpTestBC
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblMsg.Text = ""
        If Not IsPostBack Then
            '明細設定
            MsInit()
        End If


    End Sub
    Public Sub MsInit()

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
        Return BC.GetmEdpTest(tbxEdpNo.Text, tbxEdpMei.Text, tbxEdpExp.Text, tbxIdx.Text, tbxStatus.Text, tbxStatus2.Text)
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        BC.UpdmEdpTest(tbxEdpNo.Text, tbxEdpMei.Text, tbxEdpExp.Text, tbxIdx.Text, tbxStatus.Text, tbxStatus2.Text)
        MsInit()
    End Sub
    ''' <summary>
    ''' 登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click
        BC.InsmEdpTest(tbxEdpNo.Text, tbxEdpMei.Text, tbxEdpExp.Text, tbxIdx.Text, tbxStatus.Text, tbxStatus2.Text)
        MsInit()
    End Sub
    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        BC.DelmEdpTest(tbxEdpNo.Text, tbxEdpMei.Text, tbxEdpExp.Text, tbxIdx.Text, tbxStatus.Text, tbxStatus2.Text)
        MsInit()
    End Sub
End Class
