Imports System.Data

Partial Class AnkanSinntyoku
    Inherits System.Web.UI.Page

    Public sintyoukuSb As New StringBuilder
    Public miKinouStartDate As String = ""
    Public mxKinouEndDate As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim edp_no As String = "R14239"

        If Not IsPostBack Then

            Dim CDB As New CDB
            Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
            Me.ucEdpLst.DataSource = dbEdpLst

            If Request.QueryString("edp_no") Is Nothing Then

            Else
                edp_no = Request.QueryString("edp_no")
                Me.ucEdpLst.Value0 = edp_no
                Me.ucEdpLst.Text0 = Request.QueryString("edp_txt")
                ' BandMs(edp_no)

                MS(edp_no)

            End If


            Me.ucEdpLst.OnClick = "EdpSantaku"

        End If

        btnToday.Attributes.Item("onclick") = "window.open('AnkannTodayDo.aspx?userid=" & C.Client(Page).login_user_id & "'); return false;"

    End Sub



    Sub MS(ByVal edp_no As String)

        Dim sintyoukuData As Data.DataTable = Sinntyouku(edp_no)
        miKinouStartDate = GetMiKinouStartDate(edp_no)
        mxKinouEndDate = GetMxKinouEndDate(edp_no)


        For i = 0 To sintyoukuData.Rows.Count - 1

            If i = 0 Then

                sintyoukuSb.Append("arrMsMain.push(new Array(")
                sintyoukuSb.Append("""" & 999999 & """")
                sintyoukuSb.Append(",""" & 999999 & """")
                sintyoukuSb.Append(",""0""")
                sintyoukuSb.Append(",""全体""")
                sintyoukuSb.Append(",""""")
                sintyoukuSb.Append(",""""")
                sintyoukuSb.Append(",""" & miKinouStartDate & """")
                sintyoukuSb.Append(",""" & mxKinouEndDate & """")
                sintyoukuSb.Append(",""""")
                sintyoukuSb.Append(",""""")
                sintyoukuSb.Append(",""""")
                sintyoukuSb.Append("));" & vbCrLf)
            End If

            sintyoukuSb.Append("arrMsMain.push(new Array(")
            sintyoukuSb.Append("""" & sintyoukuData.Rows(i).Item("kinou_no") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("pgm_id") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("pgm_sinntyoku_retu") & """")


            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("kinou_mei") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("pgm_bunrui_name") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("pgm_name") & """")

            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("kinou_start_date") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("kinou_end_date") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("pgm_start_date") & """")
            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("pgm_end_date") & """")

            sintyoukuSb.Append(",""" & sintyoukuData.Rows(i).Item("tantousya") & """")
            sintyoukuSb.Append("));" & vbCrLf)

        Next


    End Sub

    Public Sub EdpSantaku()
        Server.Transfer("AnkanSinntyoku.aspx?edp_no=" & ucEdpLst.Value0 & "&edp_txt=" & ucEdpLst.Text0)
    End Sub



    Public Function GetMxKinouEndDate(ByVal edp_no As String) As String


        Dim sb As New StringBuilder
        sb.Length = 0
        With sb

            sb.AppendLine("SELECT")
            sb.AppendLine(" MAX(CONVERT(varchar(10), m_ankan_kinou_info.yotei_end_date, 111 )) AS yotei_end_date ")
            sb.AppendLine("FROM   m_ankan_kinou_info ")
            sb.AppendLine("INNER JOIN m_ankan_pgm_info ")
            sb.AppendLine("	ON m_ankan_kinou_info.edp_no = m_ankan_pgm_info.edp_no ")
            sb.AppendLine("	AND m_ankan_kinou_info.kinou_no = m_ankan_pgm_info.kinou_no ")
            sb.AppendLine("INNER JOIN m_ankan_pgm")
            .AppendLine("ON right(m_ankan_pgm.pgm_id,9) = right(m_ankan_pgm_info.pgm_id,9) ")
            sb.AppendLine("WHERE m_ankan_pgm_info.pgm_santaku_flg = '1'")
            sb.AppendLine("        AND  m_ankan_pgm_info.edp_no =     '" & edp_no & "'")

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        If DbResult.Data.Rows.Count > 0 AndAlso IsNullEmpty(DbResult.Data.Rows(0).Item(0)) <> "" Then
            'Return DbResult.Data.Rows(0).Item(0)
            Return DateAdd(DateInterval.Day, 8, DbResult.Data.Rows(0).Item(0)).ToString("yyyy/MM/dd")
        Else
            Return DateAdd(DateInterval.Day, 8, Now).ToString("yyyy/MM/dd")

        End If

    End Function

    Public Function GetMiKinouStartDate(ByVal edp_no As String) As String


        Dim sb As New StringBuilder
        sb.Length = 0
        With sb

            sb.AppendLine("SELECT")
            sb.AppendLine(" MIN(CONVERT(varchar(10), m_ankan_kinou_info.yotei_start_date, 111 )) AS kinou_start_date ")
            sb.AppendLine("FROM   m_ankan_kinou_info ")
            sb.AppendLine("INNER JOIN m_ankan_pgm_info ")
            sb.AppendLine("	ON m_ankan_kinou_info.edp_no = m_ankan_pgm_info.edp_no ")
            sb.AppendLine("	AND m_ankan_kinou_info.kinou_no = m_ankan_pgm_info.kinou_no ")
            sb.AppendLine("INNER JOIN m_ankan_pgm")
            .AppendLine("ON right(m_ankan_pgm.pgm_id,9) = right(m_ankan_pgm_info.pgm_id,9) ")
            sb.AppendLine("WHERE m_ankan_pgm_info.pgm_santaku_flg = '1'")
            sb.AppendLine("        AND   m_ankan_pgm_info.edp_no =     '" & edp_no & "'")

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        If DbResult.Data.Rows.Count > 0 AndAlso IsNullEmpty(DbResult.Data.Rows(0).Item(0)) <> "" Then
            Return DateAdd(DateInterval.Day, -8, DbResult.Data.Rows(0).Item(0)).ToString("yyyy/MM/dd")
            'Return DbResult.Data.Rows(0).Item(0)
        Else
            Return DateAdd(DateInterval.Day, -8, Now).ToString("yyyy/MM/dd")

        End If

    End Function

    Function IsNullEmpty(ByVal v As Object) As String
        If v Is DBNull.Value OrElse v Is Nothing Then
            Return ""
        Else
            Return v
        End If
    End Function

    Public Function Sinntyouku(ByVal edp_no As String) As Data.DataTable


        Dim sb As New StringBuilder
        sb.Length = 0
        With sb

            sb.AppendLine("SELECT")
            sb.AppendLine("		m_ankan_kinou_info.kinou_mei,")
            sb.AppendLine("		m_ankan_pgm.pgm_bunrui_cd,")
            sb.AppendLine("		m_ankan_pgm.pgm_bunrui_name,")
            sb.AppendLine("		m_ankan_pgm_info.kinou_no, ")
            sb.AppendLine("		m_ankan_pgm_info.pgm_id, ")
            sb.AppendLine("		m_ankan_pgm_info.pgm_name,")
            sb.AppendLine("        m_ankan_pgm_info.pgm_staus, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_pgm_info.yotei_start_date, 111 ) AS kinou_start_date, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_pgm_info.yotei_end_date, 111 )   AS kinou_end_date, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_pgm_info.jisseki_start_date, 111 ) AS pgm_start_date, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_pgm_info.jisseki_end_date, 111 ) AS pgm_end_date,")
            sb.AppendLine("        m_ankan_pgm_info.pgm_sinntyoku_retu, ")
            sb.AppendLine("        m_ankan_pgm_info.tantousya ")

            sb.AppendLine("FROM   m_ankan_kinou_info ")
            sb.AppendLine("INNER JOIN m_ankan_pgm_info ")
            sb.AppendLine("	ON m_ankan_kinou_info.edp_no = m_ankan_pgm_info.edp_no ")
            sb.AppendLine("	AND m_ankan_kinou_info.kinou_no = m_ankan_pgm_info.kinou_no ")
            sb.AppendLine("INNER JOIN m_ankan_pgm")
            .AppendLine("ON right(m_ankan_pgm.pgm_id,9) = right(m_ankan_pgm_info.pgm_id,9) ")

            ' sb.AppendLine("	ON m_ankan_pgm.pgm_id = m_ankan_pgm_info.pgm_id ")
            sb.AppendLine("WHERE m_ankan_pgm_info.pgm_santaku_flg = '1'")
            sb.AppendLine("       AND    m_ankan_pgm_info.edp_no =     '" & edp_no & "'")
            'sb.AppendLine("      AND m_ankan_pgm_info.kinou_no =     '" & kinou_no & "'")

            sb.AppendLine("ORDER BY m_ankan_kinou_info.kinou_no,m_ankan_pgm.pgm_bunrui_cd,m_ankan_pgm_info.pgm_id")



            '.AppendLine("SELECT ")
            '.AppendLine("a.pgm_bunrui_cd ")
            '.AppendLine(",a.pgm_bunrui_name ")
            '.AppendLine(",a.pgm_id ")
            '.AppendLine(",a.pgm_name ")
            '.AppendLine(",a.pgm_level ")
            '.AppendLine(",a.pgm_demo_path ")
            '.AppendLine(",isnull(b.pgm_santaku_flg,'') as pgm_santaku_flg ")
            '.AppendLine(",b.pgm_sinntyoku_retu ")
            '.AppendLine("FROM m_ankan_pgm_info b")
            '.AppendLine("LEFT JOIN m_ankan_pgm a")
            '.AppendLine("ON right(a.pgm_id,9) = right(b.pgm_id,9) ")
            'sb.AppendLine("      AND b.edp_no =     '" & edp_no & "'")
            'sb.AppendLine("      AND b.kinou_no =     '" & kinou_no & "'")

            'sb.AppendLine("WHERE")
            'sb.AppendLine("          b.edp_no =     '" & edp_no & "'")
            'sb.AppendLine("      AND b.kinou_no =     '" & kinou_no & "'")
            'sb.AppendLine("      AND isnull(b.pgm_santaku_flg,'') = '1'")

            '.AppendLine("ORDER BY a.pgm_bunrui_cd,a.pgm_id")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Return DbResult.Data

    End Function





    Protected Sub btnSintyoku_Click(sender As Object, e As System.EventArgs) Handles btnSintyoku.Click

        Context.Items("edp_txt") = ucEdpLst.Text0
        Context.Items("edp_no") = ucEdpLst.Value0
        Server.Transfer("AnkannKanri.aspx")


    End Sub

    Protected Sub btnToday_Click(sender As Object, e As System.EventArgs) Handles btnToday.Click

    End Sub
End Class
