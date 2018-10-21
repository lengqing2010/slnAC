Imports System.Data
Imports System.IO
Imports System.IO.Directory
'Imports Microsoft.Office.Interop

Partial Class AnkannKanri
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' PAGE　LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            '全画面戻る時、Key値あるので、画面自動設定します
            ViewState("edp_txt") = Context.Items("edp_txt")
            ViewState("edp_no") = Context.Items("edp_no")

            ViewState("kinou_txt") = Context.Items("kinou_txt")
            ViewState("kinou_no") = Context.Items("kinou_no")

            'EDPのNoの値を設定する
            Me.ucEdpLst.DataSource = (New CDB).GetEdpList

            If Context.Items("edp_no") Is Nothing Then
                '画面1回目開く場合




            Else
                '全画面戻りの場合

                'EDPのInfoを設定する（１．Dropdownlist選択）
                SetPageEdpControls(Context.Items("edp_no"), Context.Items("edp_txt"))

                '機能のInfoを設定する
                SetPageKinouControls(Context.Items("edp_no"), IsNullEmpty(ViewState("kinou_no")), IsNullEmpty(ViewState("kinou_txt")))

                KinonbetuMs()

                '機能選択した場合、明細を設定する
                If ViewState("kinou_no") IsNot Nothing Then

                    SetMs()

                End If

            End If

        End If

        Me.ucEdpLst.OnClick = "EdpSentaku"

        Me.ucKinouLst.OnClick = "KinouSantaku"

        btnToday.Attributes.Item("onclick") = "window.open('AnkannTodayDo.aspx?userid=" & C.Client(Page).login_user_id & "'); return false;"


    End Sub

    ''' <summary>
    ''' EDPデータで検索して、機能データを設定する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPageEdpControls(ByVal edp_no As String, ByVal edp_text As String) As Boolean

        Me.ucEdpLst.Value0 = edp_no
        Me.ucEdpLst.Text0 = edp_text

        SetPageLinks(edp_no)

        Return True

    End Function

    Public Sub SetPageLinks(ByVal edp_no As String)

        Dim dt As Data.DataTable = GetEdpInfo(edp_no)

        lbtnSer.Visible = False
        lbtnCli.Visible = False
        lbtnQA.Visible = False
        lbtnKfcgw.Visible = False
        lbtnPzgl.Visible = False

        Dim listPath As New List(Of String)
        Dim listFiles As New List(Of String)
        Dim QAPath As String = ""
        Dim QASiryouPath As String = ""
        Dim QADirPath As String = ""

        For idx As Integer = 0 To dt.Rows.Count - 1

            lbtnSer.Visible = True
            lbtnCli.Visible = True
            lbtnQA.Visible = True
            lbtnKfcgw.Visible = True
            lbtnPzgl.Visible = True

            'edp_no
            'ucEdpLst.Value0 = IsNullEmpty(dt.Rows(idx).Item("edp_no").ToString())

            Dim server_siryou_path As String = GetForudaPath(IsNullEmpty(dt.Rows(idx).Item("server_siryou_path").ToString()))
            Dim client_siryou_path As String = GetForudaPath(IsNullEmpty(dt.Rows(idx).Item("client_siryou_path").ToString()))

            lbtnSer.Attributes.Item("href") = server_siryou_path
            lbtnCli.Attributes.Item("href") = client_siryou_path

            lbtnQA.Attributes.Item("href") = server_siryou_path & "03_QA管理\"
            lbtnKfcgw.Attributes.Item("href") = server_siryou_path & "04_開発成果物\"
            lbtnPzgl.Attributes.Item("href") = server_siryou_path & "05_品質管理\"

            lbtnSer.Attributes.Item("onclick") = "return true;"
            lbtnCli.Attributes.Item("onclick") = "return true;"
            lbtnQA.Attributes.Item("onclick") = "return true;"
            lbtnKfcgw.Attributes.Item("onclick") = "return true;"
            lbtnPzgl.Attributes.Item("onclick") = "return true;"
            'QA
            'listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "03_QA管理\" & "," & client_siryou_path & "03_QA管理\")
            ' listFiles.Add(HttpContext.Current.Request.PhysicalApplicationPath &   "AnnkenSample\03_QA管理\ＱＡ一覧表.xls" &  & "," & client_siryou_path & "03_QA管理\ＱＡ一覧表.xls")

            listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "02_LIS提示\" & "," & client_siryou_path)
            listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "04_開発成果物\" & "," & client_siryou_path)
            listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "05_品質管理\" & "," & client_siryou_path)
            listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "06_納品管理\" & "," & client_siryou_path)
            listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "99_その他\" & "," & client_siryou_path)
            'listPath.Add(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "04\" & "," & client_siryou_path & "04\")

            QAPath = client_siryou_path & "03_QA管理\ＱＡ一覧表.xls"
            QASiryouPath = client_siryou_path & "03_QA管理\詳細資料\"
            QADirPath = client_siryou_path & "03_QA管理"

        Next

        'QA Edit
        'EditQA(QADirPath, QAPath, QASiryouPath)

        Try
            '標準Directory作成
            CreateDiretoryNotExists(listPath)
        Catch ex As Exception

        End Try



        ' CopyFiles(listFiles)
    End Sub

    'Public Function EditQA(ByVal QADirPath As String, ByVal QAPath As String, ByVal QASiryouPath As String)

    '    If Not System.IO.Directory.Exists(QADirPath) Then

    '        My.Computer.FileSystem.CopyDirectory(HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\" & "03_QA管理\", QADirPath, False)

    '        If File.Exists(QAPath) Then
    '            '*****Excel Object
    '            Dim ThisApplication As Excel.Application = Me.NewExcelApp()
    '            Dim ThisWorkbook As Excel.Workbook
    '            '*****Open  Excel
    '            ThisWorkbook = ThisApplication.Workbooks.Open(QAPath, , False)

    '            Dim xlSheet = ThisWorkbook.Sheets("ＱＡ一覧表")
    '            xlSheet.cells(4, 6).value = QASiryouPath
    '            ThisWorkbook.Save()
    '            ThisWorkbook.Close()

    '            xlSheet = Nothing

    '            '*****Close Excel
    '            Try
    '                ThisWorkbook = Nothing
    '                NAR(ThisWorkbook)
    '                ThisApplication.Quit()
    '                NAR(ThisApplication)
    '                ThisApplication = Nothing
    '                GC.Collect()
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    End If
    'End Function


    'Public Function NewExcelApp() As Excel.Application
    '    Dim ExcelApplication As New Excel.Application
    '    ExcelApplication.EnableEvents = False
    '    ExcelApplication.Visible = False         'Excel 表示
    '    ExcelApplication.DisplayAlerts = False
    '    ExcelApplication.UserControl = False
    '    Return (ExcelApplication)
    'End Function

    'Private Sub NAR(ByVal o As Object)
    '    Try
    '        While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
    '        End While
    '    Catch
    '    Finally
    '        o = Nothing
    '    End Try
    'End Sub


    Public Sub CreateDiretoryNotExists(ByVal paths As List(Of String))

        For i As Integer = 0 To paths.Count - 1
            Dim path As String = paths(i)
            If Not System.IO.Directory.Exists(path.Split(",")(1)) Then
                System.IO.Directory.CreateDirectory(path)
                'My.Computer.FileSystem.CopyDirectory(path.Split(",")(0), path.Split(",")(1), True)
            End If


            Dim pathGen As New DirectoryInfo(path.Split(",")(0))
            Dim pathSaki As New DirectoryInfo(path.Split(",")(1))
            Dim Cfile As New Cfile

            'Cfile.CopyDerictory(pathGen, pathSaki)
        Next

    End Sub

    Public Sub CopyFiles(ByVal paths As List(Of String))

        For i As Integer = 0 To paths.Count - 1
            Dim path As String = paths(i)
            If Not File.Exists(path) Then
                File.Copy(path.Split(",")(0), path.Split(",")(1), True)
            End If
        Next

    End Sub

    ''' <summary>
    ''' EDP情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdpInfo(ByVal edp_no As String) As Data.DataTable
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
            .AppendLine("WHERE")
            .AppendLine("          m_ankan_kihon_info.edp_no =     '" & ucEdpLst.Value0 & "'")
        End With
        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        Return DbResult.Data
    End Function

    ''' <summary>
    ''' 画面機能部分を設定する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPageKinouControls(ByVal edp_no As String, ByVal kinou_no As String, ByVal kinou_txt As String) As Boolean

        Me.ucKinouLst.Value0 = kinou_no
        Me.ucKinouLst.Text0 = kinou_txt

        Dim dtKinou As Data.DataTable = GetKinouDropdownListData(edp_no)
        Me.ucKinouLst.DataSource = dtKinou

        SetKinouKbnInit(edp_no, kinou_no)

        Return True
    End Function

    ''' <summary>
    ''' 機能のDropdownlistデータを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKinouDropdownListData(ByVal edp_no As String) As Data.DataTable

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
            sb.AppendLine("          m_ankan_kinou_info.edp_no =     '" & edp_no & "'")

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Return DbResult.Data

    End Function


#Region "EDP選択 1.機能Dropdowlist init 2."

    'EDP選択　機能Dropdowlist init,
    Public Sub EdpSentaku()

        Dim edp_no As String = ucEdpLst.Value0
        Dim kinou_no As String = ucKinouLst.Value0


        'SetPageLinks(edp_no)


        Dim dtKinou As Data.DataTable = GetKinouDropdownListData(ucEdpLst.Value0)
        Me.ucKinouLst.DataSource = dtKinou

        SetKinouKbnInit(edp_no, kinou_no)

        SetMs()

        '機能別明細設定
        KinonbetuMs()

        Me.gvKokinou2.DataSource = Nothing
        Me.gvSintyouku3.DataSource = Nothing

        Me.gvKokinou2.DataBind()
        Me.gvSintyouku3.DataBind()

        SetPageLinks(edp_no)




    End Sub

#End Region



#Region "機能選択"

    Public Sub KinouSantaku()
        SetMs()

    End Sub

    ''' <summary>
    ''' 選択機能情報検索して、
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetKinouKbnInit(ByVal edp_no As String, ByVal kinou_no As String)

        Dim dt As DataTable = GetKinouSantakuInfo(edp_no, kinou_no)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("kinou_kbn") = "1" Then
                Me.rbSinki.Checked = False
                Me.rbSyusei.Checked = True
            Else
                Me.rbSinki.Checked = True
                Me.rbSyusei.Checked = False
            End If
        Else
            Me.rbSinki.Checked = False
            Me.rbSyusei.Checked = False
        End If

    End Sub


    Public Function GetKinouSantakuInfo(ByVal edp_no As String, ByVal kinou_no As String) As DataTable
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
            .AppendLine("          m_ankan_kinou_info.edp_no =     '" & edp_no & "'")
            .AppendLine("      AND m_ankan_kinou_info.kinou_no =     '" & kinou_no & "'")

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)

        Return DbResult.Data

    End Function

#End Region






    Public Function GetForudaPath(ByVal v As String) As String
        If Right(v, 1) = "\" Then
            Return v
        Else
            Return v & "\"
        End If
    End Function




    Public Sub KinonbetuMs()
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("a.kinou_mei as pgm_bunrui_name ")

            .AppendLine(",cast((sum(b.pgm_sinntyoku_retu)/ count(b.pgm_sinntyoku_retu)) as int) as pgm_bunrui_retu  ")
            .AppendLine(",a.yotei_start_date as yotei_start_date ")
            .AppendLine(",a.yotei_end_date as yotei_end_date ")
            .AppendLine("FROM m_ankan_pgm_info b")

            .AppendLine("LEFT JOIN m_ankan_kinou_info a")
            .AppendLine("ON 1=1 ")
            sb.AppendLine("      AND b.edp_no =     a.edp_no")
            sb.AppendLine("      AND b.kinou_no =     a.kinou_no")


            sb.AppendLine("WHERE")
            sb.AppendLine("          b.edp_no =     '" & ucEdpLst.Value0 & "'")
            sb.AppendLine("      AND isnull(b.pgm_santaku_flg,'') = '1'")

            .AppendLine("GROUP BY b.kinou_no,a.kinou_mei,a.yotei_start_date,a.yotei_end_date")

            '.AppendLine("ORDER BY a.pgm_bunrui_cd,a.pgm_id")
        End With

        Dim DbResult1 As DbResult = DefaultDB.SelIt(sb.ToString)

        Dim drAl As Data.DataRow
        drAl = DbResult1.Data.NewRow
        drAl.Item(0) = "総"

        Dim sumAll As Integer = 0

        For i As Integer = 0 To DbResult1.Data.Rows.Count - 1
            sumAll += CInt(DbResult1.Data.Rows(i).Item("pgm_bunrui_retu").ToString)

        Next

        If DbResult1.Data.Rows.Count > 0 Then
            drAl.Item("pgm_bunrui_retu") = CInt(sumAll / (DbResult1.Data.Rows.Count))
        End If

        DbResult1.Data.Rows.Add(drAl)

        Me.gvKinoubetu1.DataSource = DbResult1.Data
        gvKinoubetu1.DataBind()



    End Sub







    Public Sub SetMs()

        GetPgmMstData1()

        GetPgmMstData2()

        Mark()

        SetGvGroup(Me.gvPgm0, 0)
        SetGvGroup(Me.gvSintyouku3, 0)
    End Sub


    Public Sub SetGvGroup(ByVal gv As GridView, ByVal cellIdx As Integer)

        Dim oldV As String = ""
        For i As Integer = 0 To gv.Rows.Count - 1
            If oldV <> gv.Rows(i).Cells(cellIdx).Text Then

                oldV = gv.Rows(i).Cells(cellIdx).Text
            Else
                gv.Rows(i).Cells(cellIdx).Text = ""
            End If
        Next

    End Sub


    Function IsNullEmpty(ByVal v As Object) As String
        If v Is DBNull.Value OrElse v Is Nothing Then
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
            .AppendLine(",b.tantousya ")

            .AppendLine("FROM m_ankan_pgm a")
            .AppendLine("LEFT JOIN m_ankan_pgm_info b")
            .AppendLine("ON right(a.pgm_id,9) = right(b.pgm_id,9) ")
            sb.AppendLine("      AND edp_no =     '" & ucEdpLst.Value0 & "'")
            sb.AppendLine("      AND kinou_no =     '" & Me.ucKinouLst.Value0 & "'")


            .AppendLine("ORDER BY pgm_bunrui_cd,pgm_id")
        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)


        gvPgm0.DataSource = DbResult.Data
        gvPgm0.DataBind()


        For i As Integer = 0 To DbResult.Data.Rows.Count - 1
            Dim c As CheckBox = gvPgm0.Rows(i).FindControl("cbPgm")
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

        'dt.Columns.Add("pgm_bunrui_name")
        'dt.Columns.Add("pgm_bunrui_retu")


        Dim sb As New StringBuilder
        sb.Length = 0
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




        Me.gvSintyouku3.DataSource = DbResult.Data
        gvSintyouku3.DataBind()


        Dim dt As New Data.DataTable
        dt.Columns.Add("pgm_bunrui_name")
        dt.Columns.Add("pgm_bunrui_retu")

        Dim drkj As Data.DataRow
        drkj = dt.NewRow
        drkj.Item(0) = "基準"
        drkj.Item(1) = "100"

        dt.Rows.Add(drkj)

        Dim pgm_bunrui_cd As String = ""
        Dim sumAll As Integer = 0

        For i As Integer = 0 To DbResult.Data.Rows.Count - 1

            If pgm_bunrui_cd <> DbResult.Data.Rows(i).Item("pgm_bunrui_cd").ToString Then

                Dim drs() As Data.DataRow = DbResult.Data.Select("pgm_bunrui_cd='" & DbResult.Data.Rows(i).Item("pgm_bunrui_cd").ToString & "'")

                Dim dr As Data.DataRow
                dr = dt.NewRow

                dr.Item(0) = DbResult.Data.Rows(i).Item("pgm_bunrui_name").ToString

                Dim sumGrp As Integer = 0

                For j As Integer = 0 To drs.Length - 1
                    sumGrp += CInt(drs(j).Item("pgm_sinntyoku_retu").ToString)
                    'sumAll += CInt(drs(j).Item("pgm_sinntyoku_retu").ToString)
                Next

                dr.Item(1) = CInt(sumGrp / drs.Length)
                'sumAll += CInt(sumGrp / drs.Length)

                dt.Rows.Add(dr)

                pgm_bunrui_cd = DbResult.Data.Rows(i).Item(0).ToString

            End If
            sumAll += CInt(DbResult.Data.Rows(i).Item("pgm_sinntyoku_retu").ToString)


        Next

        Dim drAl As Data.DataRow
        drAl = dt.NewRow
        drAl.Item(0) = "総"

        If DbResult.Data.Rows.Count > 0 Then
            drAl.Item(1) = CInt(sumAll / (DbResult.Data.Rows.Count))
        End If

        dt.Rows.Add(drAl)
        gvKokinou2.DataSource = dt
        gvKokinou2.DataBind()



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

        Context.Items("kinou_txt") = Me.ucKinouLst.Text0
        Context.Items("kinou_no") = Me.ucKinouLst.Value0

        Server.Transfer("P_TableEditor_m_ankan_kihon_info.aspx")
    End Sub

    Protected Sub btnUpdateKinou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateKinou.Click

        Context.Items("kinou_txt") = Me.ucKinouLst.Text0
        Context.Items("kinou_no") = Me.ucKinouLst.Value0

        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0
        Server.Transfer("P_TableEditor_m_ankan_kinou_info.aspx")
    End Sub

    Protected Sub btnPgmUpd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPgmUpd.Click

        Context.Items("kinou_txt") = Me.ucKinouLst.Text0
        Context.Items("kinou_no") = Me.ucKinouLst.Value0

        Context.Items("edp_txt") = Me.ucEdpLst.Text0
        Context.Items("edp_no") = Me.ucEdpLst.Value0
        Server.Transfer("P_TableEditor_m_ankan_pgm_info.aspx")
    End Sub

    Protected Sub btnPgmIns_Click(sender As Object, e As System.EventArgs) Handles btnPgmIns.Click
        InsPgm()
        SetMs()
        KinonbetuMs()
    End Sub

    Private Sub InsPgm()


        Dim sb As New StringBuilder
        With sb
            '.AppendLine("INSERT INTO m_ankan_pgm_info")
            '.AppendLine("SELECT ")
            '.AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
            '.AppendLine("  ,N'" & ucKinouLst.Value0 & "'   ")
            '.AppendLine(",b.pgm_id ")
            '.AppendLine(",b.pgm_name ")
            '.AppendLine(",b.pgm_level ")
            '.AppendLine(",0 ") 'pgm_santaku_flg
            '.AppendLine(",0 ") 'pgm_sinntyoku_retu
            '.AppendLine(",getdate() ")
            '.AppendLine(",0 ")
            '.AppendLine(",getdate() ")
            '.AppendLine(",getdate() ")
            '.AppendLine(",'" & C.Client(Page).login_user & "' ")
            '.AppendLine("FROM m_ankan_pgm b")
            '.AppendLine("LEFT JOIN m_ankan_pgm_info a")
            '.AppendLine("ON right(a.pgm_id,9) = right(b.pgm_id,9) ")
            '.AppendLine("WHERE a.pgm_id is null")

            .AppendLine("INSERT INTO m_ankan_pgm_info")
            .AppendLine("SELECT ")
            .AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
            .AppendLine("  ,N'" & ucKinouLst.Value0 & "'   ")
            .AppendLine(",b.pgm_id ")
            .AppendLine(",b.pgm_name ")
            .AppendLine(",b.pgm_level ")
            .AppendLine(",0 ") 'pgm_santaku_flg
            .AppendLine(",0 ") 'pgm_sinntyoku_retu
            .AppendLine(",getdate() ")
            .AppendLine(",0 ")
            .AppendLine(",getdate() ")
            .AppendLine(",getdate() ")
            .AppendLine(",getdate() ")
            .AppendLine(",getdate() ")
            .AppendLine(",'" & C.Client(Page).login_user & "' ")
            .AppendLine("FROM m_ankan_pgm b")
            .AppendLine("WHERE")
            .AppendLine("right(b.pgm_id,9) not in (select right(pgm_id,9) from m_ankan_pgm_info")
            .AppendLine("WHERE 1=1")
            .AppendLine("      AND edp_no =     '" & ucEdpLst.Value0 & "'")
            .AppendLine("      AND kinou_no =     '" & Me.ucKinouLst.Value0 & "'")
            .AppendLine(") ")



        End With

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        If Not DbResult.Result Then
            Me.lblMsg.Text = DbResult.Message
        End If


    End Sub

    Protected Sub btnPgmSave_Click(sender As Object, e As System.EventArgs) Handles btnPgmSave.Click

        Dim user As String = C.Client(Page).login_user
        Dim sb11 As New StringBuilder
        With sb11
            .AppendLine("INSERT INTO m_ankan_pgm_info")
            .AppendLine("SELECT ")
            .AppendLine("  N'" & ucEdpLst.Value0 & "'   ")
            .AppendLine("  ,N'" & ucKinouLst.Value0 & "'   ")
            .AppendLine(",b.pgm_id ")
            .AppendLine(",b.pgm_name ")
            .AppendLine(",b.pgm_level ")
            .AppendLine(",0 ") 'pgm_santaku_flg
            .AppendLine(",0 ") 'pgm_sinntyoku_retu
            .AppendLine(",getdate() ")
            .AppendLine(",0 ")
            .AppendLine(",getdate() ")
            .AppendLine(",getdate() ")
            .AppendLine(",getdate() ")
            .AppendLine(",getdate() ")
            .AppendLine(",'" & user & "' ")
            .AppendLine("FROM m_ankan_pgm b")
            .AppendLine("WHERE")
            .AppendLine("right(b.pgm_id,9) not in (select right(pgm_id,9) from m_ankan_pgm_info")
            .AppendLine("WHERE 1=1")
            .AppendLine("      AND edp_no =     '" & ucEdpLst.Value0 & "'")
            .AppendLine("      AND kinou_no =     '" & Me.ucKinouLst.Value0 & "'")
            .AppendLine(") ")
        End With

        Dim DbResult11 As DbResult = DefaultDB.RunIt(sb11.ToString)

        For i As Integer = 0 To Me.gvPgm0.Rows.Count - 1
            Dim c As CheckBox = gvPgm0.Rows(i).FindControl("cbPgm")
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
            End With
            Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        Next

        For i As Integer = 0 To Me.gvSintyouku3.Rows.Count - 1
            Dim c As TextBox = gvSintyouku3.Rows(i).FindControl("tbxRetu")
            Dim retu As Integer
            If c.Text.Trim = "" Then
                retu = "0"
            Else
                retu = CInt(c.Text)
            End If
            Dim sb As New StringBuilder
            With sb
                .AppendLine("UPDATE m_ankan_pgm_info SET ")

                .AppendLine("pgm_sinntyoku_retu = '" & retu & "' ")

                .AppendLine(",pgm_last_upd_date = getdate() ")
                sb.AppendLine("WHERE 1=1")
                sb.AppendLine("      AND edp_no =     '" & ucEdpLst.Value0 & "'")
                sb.AppendLine("      AND kinou_no =     '" & Me.ucKinouLst.Value0 & "'")
                sb.AppendLine("      AND pgm_id =     '" & c.Attributes.Item("pgm_id").Trim & "'")
            End With
            Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)
        Next


        SetMs()

        KinonbetuMs()

    End Sub

    Public Function GetRetuBgColor(ByVal retu As Object, Optional ByVal mei As Object = "") As String

        'Return "orange"
        If retu Is DBNull.Value Then
            Return "red"
        End If

        If mei Is DBNull.Value Then
            Return "red"
        End If

        Dim tmpRetu As String = Convert.ToString(retu)


        If mei = "総" Then

            Return "#666"

            If retu = "100" Then
                Return "green" '绿色
            Else
                Return "red"
            End If

        End If



        If tmpRetu.Trim = "" Then
            Return "red"
        End If
        If tmpRetu.Trim = "100" Then
            Return "#66CDAA" '绿色

        ElseIf CInt(tmpRetu.Trim) < 30 Then
            Return "#FF8C69"
        ElseIf CInt(tmpRetu.Trim) >= 80 Then
            Return "orange"
        Else
            Return "#EEB422"
        End If
    End Function


    Public conFileTitle As String = "mark"
    Public conGroupName As String = "機能資料"

    Protected Sub btnMarkSave_Click(sender As Object, e As System.EventArgs) Handles btnMarkSave.Click

        Dim edpNo As String = Me.ucEdpLst.Value0

        Dim groupNm As String = Me.ucKinouLst.Text0 & conGroupName

        Dim title As String = conFileTitle


        Dim file_exp As String = groupNm & "_" & title

        Dim titleNm As String = title
        Dim ex_name As String = "txt"
        Dim type As String = "txt"
        Dim shareType As String = "0"

        If shareType = "PERSON" Then
            edpNo = "PERSON" & C.Client(Page).login_user.Replace("\", "").Replace("/", "")
        End If

        Dim txt As String

        txt = Me.kindEdiorHTML.Value

        Dim path As String = HttpRuntime.AppDomainAppPath & "DATA\" & edpNo & "_" & file_exp & "." & ex_name
        C.SaveFile(path, txt)

        txt = txt.Replace("'", "''")
        C.CSaveSiryouTrue(edpNo, groupNm, titleNm, type, txt, C.Client(Page).login_user, shareType, False, True)

    End Sub



    Protected Sub Mark()

        Dim edpNo As String = Me.ucEdpLst.Value0

        Dim groupNm As String = Me.ucKinouLst.Text0 & conGroupName

        Dim group_nm As String = groupNm

        Dim file_nm As String = conFileTitle
        Dim sb As New StringBuilder
        With sb
            sb.AppendLine("SELECT TOP 1 [edp_no]")
            sb.AppendLine("      ,[group_nm]")
            sb.AppendLine("      ,[file_nm]")
            sb.AppendLine("      ,[txt]")
            sb.AppendLine("      ,[user_id]")
            sb.AppendLine("      ,[type]")
            sb.AppendLine("      ,[share_type]")
            sb.AppendLine("      ,[ins_time]")
            sb.AppendLine("  FROM [auto_code].[dbo].[m_siryou]")
            .AppendLine("WHERE    edp_no = '" & edpNo & "'")
            .AppendLine("AND group_nm = '" & group_nm & "'")
            .AppendLine("AND file_nm = '" & file_nm & "'")
        End With
        Dim msSql As New CMsSql()
        Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)

        If dt.Rows.Count > 0 Then
            Me.kindEdiorHTML.Value = dt.Rows(0).Item("txt")
        Else
            Me.kindEdiorHTML.Value = ""
        End If


    End Sub

    Public Function GetYYMMDDDiff(ByVal obj1 As Object, ByVal obj2 As Object, ByVal retu As Object) As String

        Dim st, ed As Date

        If obj1 IsNot DBNull.Value Then
            st = CDate(obj1)
        End If

        If obj2 IsNot DBNull.Value Then
            ed = CDate(obj2)
        End If
        retu = IsNullEmpty(retu)

        Dim mark As String
        If retu = "100" Then
            mark = "完了"

        Else
            If Now.ToString("yyyy/MM/dd") > ed.ToString("yyyy/MM/dd") Then
                mark = "着手中" & "　延期"
            Else
                mark = "着手中"
            End If

        End If


        If Me.IsNullEmpty(obj1) <> "" AndAlso Me.IsNullEmpty(obj1) <> "" Then

            Return st.ToString("yyyy/MM/dd") & "～" & ed.ToString("yyyy/MM/dd") & "(" & Right("___" & DateDiff(DateInterval.Day, st, ed).ToString, 3) & ")人日 " & mark
        ElseIf Me.IsNullEmpty(obj1) = "" AndAlso Me.IsNullEmpty(obj2) = "" Then
            Return ""
        Else
            Return st.ToString("yyyy/MM/dd") & "～" & ed.ToString("yyyy/MM/dd")
        End If


    End Function


    Protected Sub btnSintyoku_Click(sender As Object, e As System.EventArgs) Handles btnSintyoku.Click
        Server.Transfer("AnkanSinntyoku.aspx?edp_no=" & ucEdpLst.Value0 & "&edp_txt=" & ucEdpLst.Text0)
    End Sub

    Protected Sub btnEdp_Click(sender As Object, e As System.EventArgs) Handles btnEdp.Click
        Server.Transfer("P_TableEditor_m_edp.aspx")
    End Sub
End Class
