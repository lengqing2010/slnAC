Imports System.Data
Imports System.Text
Imports System.IO

Partial Class test
    Inherits System.Web.UI.Page

   Public BC AS NEW TUrikakeMeisaiBC
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
       Return BC.SelTUrikakeMeisai(tbxrenNo.Text, tbxtenpoCd.Text, tbxsaisyuuUriageDate.Text, tbxtorikesiKbn.Text, tbxregiNo.Text, tbxonlineKbn.Text, tbxdataSakuseiDate.Text)
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
       BC.UpdTUrikakeMeisai(tbxrenNo.Text, tbxtenpoCd.Text, tbxsaisyuuUriageDate.Text, tbxtorikesiKbn.Text, tbxregiNo.Text, tbxonlineKbn.Text, tbxdataSakuseiDate.Text,tbxtourokuDate.Text, tbxkousinDate.Text, tbxrenNo.Text, tbxurikakeKingaku.Text, tbxkingaku.Text, tbxsyouhizei.Text, tbxutikin.Text, tbxcatKensuu.Text, tbxtokuisakiKbn.Text, tbxkokykuMeiKana.Text, tbxtourokuUser.Text, tbxkousinUser.Text, tbxtenpoCd.Text, tbxsaisyuuUriageDate.Text, tbxtorikesiKbn.Text, tbxregiNo.Text, tbxonlineKbn.Text, tbxurikakeKbn.Text, tbxtoriatukaiKbn.Text, tbxbunkatuKaisuu.Text, tbxgCatFlg.Text, tbxdataSakuseiDate.Text)
        MsInit()
    End Sub
    ''' <summary>
    ''' 登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click
       BC.InsTUrikakeMeisai(tbxtourokuDate.Text, tbxkousinDate.Text, tbxrenNo.Text, tbxurikakeKingaku.Text, tbxkingaku.Text, tbxsyouhizei.Text, tbxutikin.Text, tbxcatKensuu.Text, tbxtokuisakiKbn.Text, tbxkokykuMeiKana.Text, tbxtourokuUser.Text, tbxkousinUser.Text, tbxtenpoCd.Text, tbxsaisyuuUriageDate.Text, tbxtorikesiKbn.Text, tbxregiNo.Text, tbxonlineKbn.Text, tbxurikakeKbn.Text, tbxtoriatukaiKbn.Text, tbxbunkatuKaisuu.Text, tbxgCatFlg.Text, tbxdataSakuseiDate.Text)
        MsInit()
    End Sub
    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
       BC.DelTUrikakeMeisai(tbxrenNo.Text, tbxtenpoCd.Text, tbxsaisyuuUriageDate.Text, tbxtorikesiKbn.Text, tbxregiNo.Text, tbxonlineKbn.Text, tbxdataSakuseiDate.Text)
        MsInit()
    End Sub
End Class
