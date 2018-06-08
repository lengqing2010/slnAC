Imports System.Data
Imports System.Text
Imports System.IO

Partial Class P_TableEditor_m_ankan_kinou_info
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

            Me.ucEdpLst.OnClick = "EdpSantaku"

            '明細設定
            MsInit()


            'Me.tbxYoteiStartDate.Attributes.Add("onkeydown", "setValueKeydown(this);if(event.keyCode==13){event.keyCode=9}")
            'Me.tbxYoteiStartDate.Attributes.Add("onmouseup ", "setPos(this)")
            'Me.tbxYoteiStartDate.Attributes.Add("onkeyup", "setvalue(this)")
            'Me.tbxYoteiStartDate.Attributes.Add("onfocus", "getDateValue(this)")


            'Me.tbxYoteiEndDate.Attributes.Add("onkeydown", "setValueKeydown(this);if(event.keyCode==13){event.keyCode=9}")
            'Me.tbxYoteiEndDate.Attributes.Add("onmouseup ", "setPos(this)")
            'Me.tbxYoteiEndDate.Attributes.Add("onkeyup", "setvalue(this)")
            'Me.tbxYoteiEndDate.Attributes.Add("onfocus", "getDateValue(this)")

            Me.tbxYoteiStartDate.Attributes.Add("onfocus", "this.select()")
            Me.tbxYoteiStartDate.Attributes.Add("onblur", "GetDateFormat(this)")

            Me.tbxYoteiEndDate.Attributes.Add("onfocus", "this.select()")
            Me.tbxYoteiEndDate.Attributes.Add("onblur", "GetDateFormat(this)")

        End If

    End Sub

    'EDP選択
    Public Sub EdpSantaku()

        Me.tbxKinouNo.Text = GetMxKinouNo(ucEdpLst.Value0)
        '明細設定
        MsInit()

    End Sub

    '最大機能No.を取得する
    Public Function GetMxKinouNo(ByVal edp_no As String) As String

        'EDP情報を取得する
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("count(edp_no) ")
            .AppendLine("FROM m_ankan_kinou_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & edp_no & "'   ")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Dim cnt As Integer = CInt(DbResult.Data.Rows(0).Item(0)) + 1

        Return edp_no & "_" & Right("0000000000" & cnt, 9)

    End Function




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

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("edp_no ")
            .AppendLine(",kinou_no ")
            .AppendLine(",kinou_mei ")
            .AppendLine(",kinou_kbn ")
            .AppendLine(",yotei_kousuu ")
            .AppendLine(",CONVERT(varchar(10), yotei_start_date, 111 ) as yotei_start_date ")
            .AppendLine(",CONVERT(varchar(10), yotei_end_date, 111 ) as yotei_end_date  ")
            .AppendLine("FROM m_ankan_kinou_info")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
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
   'kinou_no nvarchar(100)
        tbxKinouNo.Text = row.Cells(2).Text.Replace("&nbsp;", "")
   'kinou_mei nvarchar(1000)
        tbxKinouMei.Text = row.Cells(3).Text.Replace("&nbsp;", "")
   'kinou_kbn nvarchar(2)
        tbxKinouKbn.Text = row.Cells(4).Text.Replace("&nbsp;", "")
   'yotei_kousuu numeric(5)
        tbxYoteiKousuu.Text = row.Cells(5).Text.Replace("&nbsp;", "")
   'yotei_start_date datetime(8)
        tbxYoteiStartDate.Text = row.Cells(6).Text.Replace("&nbsp;", "")
   'yotei_end_date datetime(8)
        tbxYoteiEndDate.Text = row.Cells(7).Text.Replace("&nbsp;", "")
       
    End Sub

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click

        If tbxYoteiStartDate.Text = "1900/01/01 0:00:00" OrElse tbxYoteiStartDate.Text.Trim = "" Then
            tbxYoteiStartDate.Text = Now.ToString("yyyy/MM/dd")
        End If

        If tbxYoteiEndDate.Text = "1900/01/01 0:00:00" OrElse tbxYoteiEndDate.Text.Trim = "" Then
            tbxYoteiEndDate.Text = DateAdd(DateInterval.Day, 1, Now).ToString("yyyy/MM/dd")
        End If




        Dim sb As New StringBuilder
        With sb
            .AppendLine("UPDATE m_ankan_kinou_info")
            .AppendLine("SET")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            .AppendLine(",kinou_no = '" & tbxKinouNo.Text & "'   ")
            .AppendLine(",kinou_mei = '" & tbxKinouMei.Text & "'   ")
            .AppendLine(",kinou_kbn = '" & tbxKinouKbn.Text & "'   ")
            .AppendLine(",yotei_kousuu = '" & tbxYoteiKousuu.Text & "'   ")
            .AppendLine(",yotei_start_date = '" & tbxYoteiStartDate.Text & "'   ")
            .AppendLine(",yotei_end_date = '" & tbxYoteiEndDate.Text & "'   ")
            .AppendLine("WHERE")
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
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
            .AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
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
            .AppendLine("edp_no = '" & ucEdpLst.Value0 & "'   ")
            .AppendLine("AND kinou_no = '" & tbxKinouNo.Text & "'   ")
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
