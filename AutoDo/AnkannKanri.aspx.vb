
Partial Class AnkannKanri
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim CDB As New CDB
            Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
            Me.ucEdpLst.DataSource = dbEdpLst
            GetEdpData()


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

        If DbResult.Data.Rows.Count > 0 Then

        End If


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

        GetPgmMstData()

    End Sub


    Function IsNullEmpty(ByVal v As Object) As String
        If v Is DBNull.Value Then
            Return ""
        Else
            Return v
        End If
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

        If DbResult.Data.Rows.Count > 0 Then

            For i As Integer = 0 To DbResult.Data.Rows.Count - 1


                If pgm_bunrui_cd <> DbResult.Data.Rows(i).Item("pgm_bunrui_cd") Then

                    pgm_bunrui_cd = DbResult.Data.Rows(i).Item("pgm_bunrui_cd")

                    If i > 0 Then
                        Dim lit2 As New Literal()
                        lit2.Text = ("<br>")
                        PLPgm.Controls.Add(lit2)
                    End If

                    'PLPgm
                    Dim lbl As New Label
                    lbl.Text = DbResult.Data.Rows(i).Item("pgm_bunrui_name")
                    PLPgm.Controls.Add(lbl)

                    Dim lit As New Literal()
                    lit.Text = ("<HR>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
                    PLPgm.Controls.Add(lit)



                End If

                Dim lit3 As New Literal()
                lit3.Text = ("&nbsp;&nbsp;&nbsp;&nbsp;")
                PLPgm.Controls.Add(lit3)


                Dim pgm As New CheckBox
                pgm.Text = DbResult.Data.Rows(i).Item("pgm_name")

                pgm.Checked = (DbResult.Data.Rows(i).Item("pgm_santaku_flg") = "1")



                PLPgm.Controls.Add(pgm)
            Next


        End If





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
        Server.Transfer("P_TableEditor_m_ankan_kihon_info.aspx")
    End Sub

    Protected Sub btnUpdateKinou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateKinou.Click
        Server.Transfer("P_TableEditor_m_ankan_kinou_info.aspx")
    End Sub

    Protected Sub btnPgmUpd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPgmUpd.Click
        Server.Transfer("P_TableEditor_m_ankan_pgm_info.aspx")
    End Sub
End Class
