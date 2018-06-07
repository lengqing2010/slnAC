
Partial Class AnkanSinntyokuJs
    Inherits System.Web.UI.Page

    Public sintyoukuSb As New StringBuilder
    Public miKinouStartDate As String
    Public mxKinouEndDate As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim edp_no As String = "R14239"

        If Not IsPostBack Then

            Dim sintyoukuData As Data.DataTable = Sinntyouku(edp_no)
            miKinouStartDate = GetMiKinouStartDate(edp_no)
            mxKinouEndDate = GetMxKinouEndDate(edp_no)


            For i = 0 To sintyoukuData.Rows.Count - 1
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



        End If

    
    End Sub


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

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Return DbResult.Data

    End Function


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
            Return DbResult.Data.Rows(0).Item(0)
        Else
            Return DateAdd(DateInterval.Day, 0, Now).ToString("yyyy/MM/dd")

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
            Return DbResult.Data.Rows(0).Item(0)
        Else
            Return DateAdd(DateInterval.Day, 0, Now).ToString("yyyy/MM/dd")

        End If

    End Function

End Class
