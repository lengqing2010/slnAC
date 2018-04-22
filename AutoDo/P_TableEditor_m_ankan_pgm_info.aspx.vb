Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_ankan_pgm_info
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
           Me.lblMsg.Text = ""
        If Not IsPostBack Then

            ViewState("edp_txt") = Context.Items("edp_txt")
            ViewState("edp_no") = Context.Items("edp_no")

            ViewState("kinou_txt") = Context.Items("kinou_txt")
            ViewState("kinou_no") = Context.Items("kinou_no")

            Dim CDB As New CDB
            Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
            Me.ucEdpLst.DataSource = dbEdpLst

            If Context.Items("edp_no") IsNot Nothing Then
                Me.ucEdpLst.Text0 = Context.Items("edp_txt")
                Me.ucEdpLst.Value0 = Context.Items("edp_no")
            End If

            EdpSantaku()
            KinouSantaku()

            Me.ucEdpLst.OnClick = "EdpSantaku"
            Me.ucKinouLst.OnClick = "KinouSantaku"


            Me.tbxYoteiStartDate.Attributes.Add("onfocus", "this.select()")
            Me.tbxYoteiStartDate.Attributes.Add("onblur", "GetDateFormat(this)")

            Me.tbxYoteiEndDate.Attributes.Add("onfocus", "this.select()")
            Me.tbxYoteiEndDate.Attributes.Add("onblur", "GetDateFormat(this)")


        End If
  

    End Sub


    'EDP選択
    Public Sub EdpSantaku()
        Me.ucKinouLst.DataSource = GetKinouData(ucEdpLst.Value0)
    End Sub

    Public Sub KinouSantaku()
        Me.tbxPgmId.Text = GetMxPgmId(ucEdpLst.Value0, ucKinouLst.Value0)
        '明細設定
        MsInit()
    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKinouData(ByVal edp_no As String) As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("kinou_no as value ,kinou_no+' '+kinou_mei as text")
            .AppendLine(",edp_no ")
            .AppendLine(",kinou_no ")
            .AppendLine(",kinou_mei ")
            .AppendLine(",kinou_kbn ")
            .AppendLine(",yotei_kousuu ")
            .AppendLine(",yotei_start_date ")
            .AppendLine(",yotei_end_date ")


            .AppendLine("FROM m_ankan_kinou_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & edp_no & "'   ")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        Return DbResult.Data

    End Function

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMxPgmId(edp_no, kinou_no) As String

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("count(edp_no) ")

            .AppendLine("FROM m_ankan_pgm_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & edp_no & "'   ")
            .AppendLine("AND kinou_no = '" & kinou_no & "'   ")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Dim cnt As Integer = CInt(DbResult.Data.Rows(0).Item(0)) + 1

        Return kinou_no & "_" & Right("0000000000" & cnt, 9)

    End Function


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
            .AppendLine("m_ankan_pgm_info.edp_no ")
            .AppendLine(",m_ankan_pgm_info.kinou_no ")
            .AppendLine(",m_ankan_pgm_info.pgm_id ")
            .AppendLine(",m_ankan_pgm_info.pgm_name ")
            .AppendLine(",m_ankan_pgm_info.pgm_level ")
            .AppendLine(",m_ankan_pgm_info.pgm_santaku_flg ")
            .AppendLine(",m_ankan_pgm_info.pgm_sinntyoku_retu ")
            .AppendLine(",m_ankan_pgm_info.pgm_last_upd_date ")
            .AppendLine(",m_ankan_pgm_info.pgm_staus ")
            .AppendLine(",ISNULL(m_ankan_pgm_info.yotei_start_date,m_ankan_kinou_info.yotei_start_date) AS  yotei_start_date")
            .AppendLine(",ISNULL(m_ankan_pgm_info.yotei_end_date,m_ankan_kinou_info.yotei_end_date) AS  yotei_end_date")

            .AppendLine(",m_ankan_pgm_info.tantousya ")

            .AppendLine("FROM m_ankan_pgm_info")
            sb.AppendLine("INNER JOIN   m_ankan_kinou_info ")
            sb.AppendLine("	ON m_ankan_kinou_info.edp_no = m_ankan_pgm_info.edp_no ")
            sb.AppendLine("	AND m_ankan_kinou_info.kinou_no = m_ankan_pgm_info.kinou_no ")

            .AppendLine("WHERE")
            .AppendLine("m_ankan_pgm_info.edp_no = '" & ucEdpLst.Value0 & "'   ")

            If ucKinouLst.Value0 <> "" Then
                .AppendLine("AND m_ankan_pgm_info.kinou_no = '" & ucKinouLst.Value0 & "'   ")
            End If

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

        ucEdpLst.Value0 = row.Cells(1).Text
        'tbxEdpNo.Text = row.Cells(1).Text
   'kinou_no nvarchar(100)
        Me.ucKinouLst.Value0 = row.Cells(2).Text.Replace("&nbsp;", "")
   'pgm_id nvarchar(100)
        tbxPgmId.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'pgm_name nvarchar(1000)
        tbxPgmName.Text = row.Cells(4).Text.Replace("&nbsp;", "")
   'pgm_level nvarchar(2)
        tbxPgmLevel.Text = row.Cells(5).Text.Replace("&nbsp;", "")
   'pgm_santaku_flg nvarchar(2)
        tbxPgmSantakuFlg.Text = row.Cells(6).Text.Replace("&nbsp;", "")
   'pgm_sinntyoku_retu numeric(5)
        tbxPgmSinntyokuRetu.Text = row.Cells(7).Text.Replace("&nbsp;", "")
   'pgm_last_upd_date nvarchar(2)
        tbxPgmLastUpdDate.Text = row.Cells(8).Text.Replace("&nbsp;", "")
   'pgm_staus datetime(8)
        tbxPgmStaus.Text = row.Cells(9).Text.Replace("&nbsp;", "")

        'Me.tbxYoteiStartDate.Attributes.Add("onblur", "GetDateFormat(this)")
        'Me.tbxYoteiEndDate.Attributes.Add("onfocus", "this.select()")
        If row.Cells(10).Text.Replace("&nbsp;", "") = "" Then
            tbxYoteiStartDate.Text = Now.ToString("yyyy/MM/dd")
        Else
            tbxYoteiStartDate.Text = row.Cells(10).Text.Replace("&nbsp;", "")
        End If

        If row.Cells(11).Text.Replace("&nbsp;", "") = "" Then
            tbxYoteiEndDate.Text = Now.ToString("yyyy/MM/dd")
        Else
            tbxYoteiEndDate.Text = row.Cells(11).Text.Replace("&nbsp;", "")
        End If

        'tbxYoteiEndDate.Text = row.Cells(11).Text.Replace("&nbsp;", "")

        If row.Cells(12).Text.Replace("&nbsp;", "") = "" Then
            tantousya.Text = C.Client(Page).login_user
        Else
            tantousya.Text = row.Cells(12).Text.Replace("&nbsp;", "")
        End If




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
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            .AppendLine(",kinou_no = '" & Me.ucKinouLst.Value0 & "'   ")
            .AppendLine(",pgm_id = '" & tbxPgmId.Text & "'   ")
            .AppendLine(",pgm_name = '" & tbxPgmName.Text & "'   ")
            .AppendLine(",pgm_level = '" & tbxPgmLevel.Text & "'   ")
            .AppendLine(",pgm_santaku_flg = '" & tbxPgmSantakuFlg.Text & "'   ")
            .AppendLine(",pgm_sinntyoku_retu = '" & tbxPgmSinntyokuRetu.Text & "'   ")
            .AppendLine(",pgm_last_upd_date = '" & tbxPgmLastUpdDate.Text & "'   ")
            .AppendLine(",pgm_staus = '" & tbxPgmStaus.Text & "'   ")

            .AppendLine(",yotei_start_date  = '" & tbxYoteiStartDate.Text & "'   ")
            .AppendLine(",yotei_end_date  = '" & tbxYoteiEndDate.Text & "'   ")
            .AppendLine(",tantousya  = '" & tantousya.Text & "'   ")

            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            .AppendLine("AND kinou_no = '" & Me.ucKinouLst.Value0 & "'   ")
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
            .AppendLine(",yotei_start_date ")
            .AppendLine(",yotei_end_date ")
            .AppendLine(",tantousya ")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
            .AppendLine(",  N'" & Me.ucKinouLst.Value0 & "'   ")
            .AppendLine(",  N'" & tbxPgmId.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmName.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmLevel.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmSantakuFlg.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmSinntyokuRetu.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmLastUpdDate.Text & "'   ")
            .AppendLine(",  N'" & tbxPgmStaus.Text & "'   ")
            .AppendLine(",  N'" & tbxYoteiStartDate.Text & "'   ")
            .AppendLine(",  N'" & tbxYoteiEndDate.Text & "'   ")
            .AppendLine(",  N'" & tantousya.Text & "'   ")
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
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            .AppendLine("AND kinou_no = '" & Me.ucKinouLst.Value0 & "'   ")
            .AppendLine("AND pgm_id = '" & tbxPgmId.Text & "'   ")
        End With
        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If
        MsInit()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0


        Context.Items("edp_txt") = ViewState("edp_txt")
        Context.Items("edp_no") = ViewState("edp_no")

        Context.Items("kinou_txt") = ViewState("kinou_txt")
        Context.Items("kinou_no") = ViewState("kinou_no")

        Server.Transfer("AnkannKanri.aspx")
    End Sub
End Class
