Imports System.Data

Partial Class AnkanSinntyoku
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim edp_no As String = "R14239"

        Dim sinntyoukuData As Data.DataTable = Sinntyouku(edp_no)
        Dim miKinouStartDate As String = GetMiKinouStartDate(edp_no)
        Dim mxKinouEndDate As String = GetMxKinouEndDate(edp_no)

        Dim data As DataTable = GetSinTyoukuDataTable(miKinouStartDate, mxKinouEndDate)

        For i As Integer = 0 To sinntyoukuData.Rows.Count - 1
            Dim dr As DataRow
            dr = data.NewRow
            'pgm_start_date
            'pgm_end_date
            dr.Item("kinou_mei") = sinntyoukuData.Rows(i).Item("kinou_mei")
            dr.Item("pgm_bunrui_name") = sinntyoukuData.Rows(i).Item("pgm_bunrui_name")
            dr.Item("pgm_name") = sinntyoukuData.Rows(i).Item("pgm_name")

            Dim kinou_start_date As Object = sinntyoukuData.Rows(i).Item("kinou_start_date").ToString.Replace("/", "")
            Dim kinou_end_date As Object = sinntyoukuData.Rows(i).Item("kinou_end_date").ToString.Replace("/", "")

            Dim StartDate As Object = sinntyoukuData.Rows(i).Item("pgm_start_date").ToString.Replace("/", "")
            Dim EndDate As Object = sinntyoukuData.Rows(i).Item("pgm_end_date").ToString.Replace("/", "")

            For j As Integer = 3 To data.Columns.Count - 1


                If C.IsNullEmpty(kinou_start_date) <> "" AndAlso C.IsNullEmpty(kinou_end_date) <> "" Then
                    If data.Columns(j).ColumnName >= kinou_start_date _
                        AndAlso data.Columns(j).ColumnName <= kinou_end_date Then
                        dr.Item(j) = "囗"
                    End If
                End If


                If C.IsNullEmpty(StartDate) <> "" AndAlso C.IsNullEmpty(EndDate) <> "" Then
                    If data.Columns(j).ColumnName >= StartDate _
                        AndAlso data.Columns(j).ColumnName <= EndDate Then
                        dr.Item(j) = "■"
                    End If
                End If

            Next

            data.Rows.Add(dr)

        Next

        gvSintyoku.DataSource = data
        gvSintyoku.DataBind()

        SetGvGroup(Me.gvSintyoku, 0)
        SetGvGroup(Me.gvSintyoku, 1)

        'gvSintyoku.HeaderRow.Height = 200
        For j As Integer = 3 To data.Columns.Count - 1
            'gvSintyoku.HeaderRow.Cells(j).Height = 80
            gvSintyoku.HeaderRow.Cells(j).Width = 10
            'gvSintyoku.HeaderRow.Cells(j).Style.Item("white-space") = "normal"
            gvSintyoku.HeaderRow.Cells(j).Style.Item("word-wrap") = "break-word"
        Next

    End Sub






    Public Sub SetGvGroup(ByVal gv As GridView, ByVal cellIdx As Integer)

        Dim oldV As String = ""
        For i As Integer = 0 To gv.Rows.Count - 1
            If oldV <> gv.Rows(i).Cells(cellIdx).Text Then

                oldV = gv.Rows(i).Cells(cellIdx).Text
            Else
                gv.Rows(i).Cells(cellIdx).Text = ""
                'gv.Rows(i).Cells(cellIdx).RowSpan = 1
                'gv.Rows(i).Cells(cellIdx).Visible = False
            End If
        Next

    End Sub

    Public Function GetSinTyoukuDr(ByRef dr As DataRow, _
                                   ByVal miKinouStartDate As Object, ByVal mxKinouEndDate As Object, _
                                   ByVal StartDate As Object, ByVal EndDate As Object) As DataTable

        If C.IsNullEmpty(StartDate) <> "" AndAlso C.IsNullEmpty(EndDate) <> "" Then

            For i As Integer = 3 To dr.ItemArray.Count - 1
                '■囗

                dr.Item(i) = "■"
            Next

        End If


    End Function

    Public Function GetSinTyoukuDataTable(ByVal miKinouStartDate As String, ByVal mxKinouEndDate As String) As DataTable

        Dim daysLength As Integer = DateDiff(DateInterval.Day, CDate(miKinouStartDate), CDate(mxKinouEndDate))
        Dim startDate As Date = CDate(miKinouStartDate)
        Dim endDate As Date = CDate(mxKinouEndDate)

        Dim data As New DataTable

        data.Columns.Add("kinou_mei")
        data.Columns.Add("pgm_bunrui_name")
        data.Columns.Add("pgm_name")


        For i As Integer = 0 To daysLength

            data.Columns.Add(DateAdd(DateInterval.Day, i, startDate).ToString("yyyyMMdd"))

        Next

        Return data

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

        Return DbResult.Data.Rows(0).Item(0)

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

        Return DbResult.Data.Rows(0).Item(0)

    End Function

    Public Function Sinntyouku(ByVal edp_no As String) As Data.DataTable


        Dim sb As New StringBuilder
        sb.Length = 0
        With sb

            sb.AppendLine("SELECT")
            sb.AppendLine("		m_ankan_kinou_info.kinou_mei,")
            sb.AppendLine("		m_ankan_pgm.pgm_bunrui_cd,")
            sb.AppendLine("		m_ankan_pgm.pgm_bunrui_name,")
            sb.AppendLine("		m_ankan_pgm_info.pgm_id, ")
            sb.AppendLine("		m_ankan_pgm_info.pgm_name,")
            sb.AppendLine("        m_ankan_pgm_info.pgm_staus, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_kinou_info.yotei_start_date, 111 ) AS kinou_start_date, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_kinou_info.yotei_end_date, 111 )   AS kinou_end_date, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_pgm_info.yotei_start_date, 111 ) AS pgm_start_date, ")
            sb.AppendLine("        CONVERT(varchar(10), m_ankan_pgm_info.yotei_end_date, 111 ) AS pgm_end_date")
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
End Class
