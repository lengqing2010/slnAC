
Partial Class AnkannKanri
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'edp_no
            Dim CDB As New CDB
            Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
            Me.ucEdpLst.DataSource = dbEdpLst
            If Context.Items("edp_no") IsNot Nothing Then
                Me.ucEdpLst.Value0 = Context.Items("edp_no")
                Me.ucEdpLst.Text0 = Context.Items("edp_txt")
            End If


            'GetEdpData()


            KinouSantaku()

        End If

        ucEdpLst.OnClick = "EdpSentaku"
        Me.ucKinouLst.OnClick = "KinouSantaku"

    End Sub


    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdpData() As Data.DataTable

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
            sb.AppendLine("WHERE")
            sb.AppendLine("          m_ankan_kihon_info.edp_no =     '" & ucEdpLst.Value0 & "'")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        Dim dt As Data.DataTable = DbResult.Data
        For idx As Integer = 0 To dt.Rows.Count - 1

            'edp_no
            ucEdpLst.Value0 = IsNullEmpty(dt.Rows(idx).Item("edp_no").ToString())

        Next

        'Return DbResult.Data
    End Function


    'EDP 選択
    Public Sub EdpSentaku()

        Dim dtKinou As Data.DataTable = GetKinouData()
        Me.ucKinouLst.DataSource = dtKinou


        KinouSantaku()

    End Sub


    Private Function GetKinouData() As Data.DataTable

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
            sb.AppendLine("WHERE")
            sb.AppendLine("          m_ankan_kinou_info.edp_no =     '" & ucEdpLst.Value0 & "'")

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)




        Return DbResult.Data


    End Function


    Public Sub KinouSantaku()

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
            sb.AppendLine("WHERE")
            sb.AppendLine("          m_ankan_kinou_info.edp_no =     '" & ucEdpLst.Value0 & "'")
            sb.AppendLine("      AND m_ankan_kinou_info.kinou_no =     '" & Me.ucKinouLst.Value0 & "'")



        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        If DbResult.Data.Rows.Count > 0 Then
            If DbResult.Data.Rows(0).Item("kinou_kbn") = "1" Then
                Me.rbSinki.Checked = False
                Me.rbSyusei.Checked = True
            Else
                Me.rbSinki.Checked = True
                Me.rbSyusei.Checked = False
            End If

        End If

        GetPgmMstData1()

        GetPgmMstData2()

    End Sub


    Function IsNullEmpty(ByVal v As Object) As String
        If v Is DBNull.Value Then
            Return ""
        Else
            Return v
        End If
    End Function


    Private Function GetPgmMstData1() As Data.DataTable

        ' PLPgm.Controls.Clear()

        If ucEdpLst.Value0 = "" OrElse ucKinouLst.Value0 = "" Then
            Return Nothing

        End If

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("a.pgm_bunrui_cd ")
            .AppendLine(",a.pgm_bunrui_name ")
            .AppendLine(",a.pgm_id ")
            .AppendLine(",a.pgm_name ")
            .AppendLine(",a.pgm_level ")
            .AppendLine(",a.pgm_demo_path ")
            .AppendLine(",isnull(b.pgm_santaku_flg,'') as pgm_santaku_flg ")
            .AppendLine("FROM m_ankan_pgm a")
            .AppendLine("LEFT JOIN m_ankan_pgm_info b")
            .AppendLine("ON right(a.pgm_id,9) = right(b.pgm_id,9) ")
            sb.AppendLine("      AND edp_no =     '" & ucEdpLst.Value0 & "'")
            sb.AppendLine("      AND kinou_no =     '" & Me.ucKinouLst.Value0 & "'")


            .AppendLine("ORDER BY pgm_bunrui_cd,pgm_id")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)


        gvPgm.DataSource = DbResult.Data
        gvPgm.DataBind()


        For i As Integer = 0 To DbResult.Data.Rows.Count - 1
            Dim c As CheckBox = gvPgm.Rows(i).FindControl("cbPgm")
            c.Checked = (DbResult.Data.Rows(i).Item("pgm_santaku_flg") = "1")
        Next




        'Dim pgm_bunrui_cd As String = ""

        'If DbResult.Data.Rows.Count > 0 Then

        '    For i As Integer = 0 To DbResult.Data.Rows.Count - 1


        '        If pgm_bunrui_cd <> DbResult.Data.Rows(i).Item("pgm_bunrui_cd") Then

        '            pgm_bunrui_cd = DbResult.Data.Rows(i).Item("pgm_bunrui_cd")

        '            If i > 0 Then
        '                Dim lit2 As New Literal()
        '                lit2.Text = ("<br>")
        '                PLPgm.Controls.Add(lit2)
        '            End If

        '            'PLPgm
        '            Dim lbl As New Label
        '            lbl.Text = DbResult.Data.Rows(i).Item("pgm_bunrui_name")
        '            PLPgm.Controls.Add(lbl)

        '            Dim lit As New Literal()
        '            lit.Text = ("<HR>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
        '            PLPgm.Controls.Add(lit)



        '        End If

        '        Dim lit3 As New Literal()
        '        lit3.Text = ("&nbsp;&nbsp;&nbsp;&nbsp;")
        '        PLPgm.Controls.Add(lit3)


        '        Dim pgm As New CheckBox
        '        pgm.ID = "cbxpgm" & i
        '        pgm.Text = DbResult.Data.Rows(i).Item("pgm_name")
        '        pgm.Attributes.Item("pgm_bunrui_cd") = DbResult.Data.Rows(i).Item("pgm_bunrui_cd")
        '        pgm.Attributes.Item("pgm_id") = DbResult.Data.Rows(i).Item("pgm_id")
        '        pgm.Checked = (DbResult.Data.Rows(i).Item("pgm_santaku_flg") = "1")



        '        PLPgm.Controls.Add(pgm)
        '    Next


        'End If

        Return DbResult.Data

    End Function


    Private Function GetPgmMstData2() As Data.DataTable

        If ucEdpLst.Value0 = "" OrElse ucKinouLst.Value0 = "" Then
            Return Nothing
        End If

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("a.pgm_bunrui_cd ")
            .AppendLine(",a.pgm_bunrui_name ")
            .AppendLine(",a.pgm_id ")
            .AppendLine(",a.pgm_name ")
            .AppendLine(",a.pgm_level ")
            .AppendLine(",a.pgm_demo_path ")
            .AppendLine(",isnull(b.pgm_santaku_flg,'') as pgm_santaku_flg ")
            .AppendLine(",b.pgm_sinntyoku_retu ")
            .AppendLine("FROM m_ankan_pgm_info b")
            .AppendLine("LEFT JOIN m_ankan_pgm a")
            .AppendLine("ON right(a.pgm_id,9) = right(b.pgm_id,9) ")
            sb.AppendLine("      AND b.edp_no =     '" & ucEdpLst.Value0 & "'")
            sb.AppendLine("      AND b.kinou_no =     '" & Me.ucKinouLst.Value0 & "'")

            sb.AppendLine("WHERE")
            sb.AppendLine("          b.edp_no =     '" & ucEdpLst.Value0 & "'")
            sb.AppendLine("      AND b.kinou_no =     '" & Me.ucKinouLst.Value0 & "'")
            sb.AppendLine("      AND isnull(b.pgm_santaku_flg,'') = '1'")

            .AppendLine("ORDER BY a.pgm_bunrui_cd,a.pgm_id")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)


        Me.gvPgmInfo.DataSource = DbResult.Data
        gvPgmInfo.DataBind()

        Return DbResult.Data

    End Function


    Private Function GetPgmMstData() As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("a.pgm_bunrui_cd ")
            .AppendLine(",a.pgm_bunrui_name ")
            .AppendLine(",a.pgm_id ")
            .AppendLine(",a.pgm_name ")
            .AppendLine(",a.pgm_level ")
            .AppendLine(",a.pgm_demo_path ")
            .AppendLine(",isnull(b.pgm_santaku_flg,'') as pgm_santaku_flg ")
            .AppendLine("FROM m_ankan_pgm a")
            .AppendLine("LEFT JOIN m_ankan_pgm_info b")
            .AppendLine("ON a.pgm_id = b.pgm_id ")


            .AppendLine("ORDER BY pgm_bunrui_cd,pgm_id")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Dim pgm_bunrui_cd As String = ""

        Return DbResult.Data

    End Function




    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0
        Server.Transfer("P_TableEditor_m_ankan_kihon_info.aspx")
    End Sub

    Protected Sub btnUpdateKinou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateKinou.Click
        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0
        Server.Transfer("P_TableEditor_m_ankan_kinou_info.aspx")
    End Sub

    Protected Sub btnPgmUpd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPgmUpd.Click
        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0
        Server.Transfer("P_TableEditor_m_ankan_pgm_info.aspx")
    End Sub

    Protected Sub btnPgmIns_Click(sender As Object, e As System.EventArgs) Handles btnPgmIns.Click
        InsPgm()
    End Sub

    Private Sub InsPgm()


        Dim sb As New StringBuilder
        With sb
            .AppendLine("INSERT INTO m_ankan_pgm_info")
            .AppendLine("SELECT ")
            .AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
            .AppendLine("  ,N'" & ucKinouLst.Value0 & "'   ")

            .AppendLine(",pgm_id ")
            .AppendLine(",pgm_name ")
            .AppendLine(",pgm_level ")

            .AppendLine(",0 ") 'pgm_santaku_flg
            .AppendLine(",0 ") 'pgm_sinntyoku_retu
            .AppendLine(",getdate() ")
            .AppendLine(",0 ")
            .AppendLine("FROM m_ankan_pgm")
        End With

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If


    End Sub

    Protected Sub btnPgmSave_Click(sender As Object, e As System.EventArgs) Handles btnPgmSave.Click

        For i As Integer = 0 To Me.gvPgm.Rows.Count - 1


            Dim c As CheckBox = gvPgm.Rows(i).FindControl("cbPgm")




            Dim sb As New StringBuilder
            With sb
                .AppendLine("UPDATE m_ankan_pgm_info SET ")

                If CType(c, CheckBox).Checked Then
                    .AppendLine("pgm_santaku_flg = '1' ")
                Else
                    .AppendLine("pgm_santaku_flg = '0' ")
                End If
                .AppendLine(",pgm_last_upd_date = getdate() ")


                sb.AppendLine("WHERE 1=1")
                sb.AppendLine("      AND edp_no =     '" & ucEdpLst.Value0 & "'")
                sb.AppendLine("      AND kinou_no =     '" & Me.ucKinouLst.Value0 & "'")
                sb.AppendLine("      AND pgm_id =     '" & c.ToolTip.Trim & "'")

                '.AppendLine("SELECT ")
                '.AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
                '.AppendLine("  ,N'" & ucKinouLst.Value0 & "'   ")

                '.AppendLine(",pgm_id ")
                '.AppendLine(",pgm_name ")
                '.AppendLine(",pgm_level ")

                '.AppendLine(",0 ") 'pgm_santaku_flg
                '.AppendLine(",0 ") 'pgm_sinntyoku_retu
                '.AppendLine(",getdate() ")
                '.AppendLine(",0 ")
                '.AppendLine("FROM m_ankan_pgm")
            End With

            Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)


        Next

    End Sub
End Class
