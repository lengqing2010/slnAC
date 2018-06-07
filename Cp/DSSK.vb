Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  


Public Class DSSK

    ''' <summary>
    ''' LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DSSK_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Webbrowser IE 11 で　開く (11001 (0x2EDF) Internet Explorer 11)
        MakeWebbrowserDefaultIe11("Cp", 11001)

        'https://www.dszuqiu.com/league/198
        'https://www.dszuqiu.com/league/252
        '巴西
        'https://www.dszuqiu.com/league/251

        'https://www.dszuqiu.com/league/34
        ' GetCpDataByUrl("https://www.dszuqiu.com/league/34")

        '前8分钟 1=0.8
        '1:1 , 2:1 , 3:0.9
        '>3 0.5
        '>4 0.3
        '进球差 1:1.0 , 2: 0.8  3: 0.7 4:0.6 5:0.5 6:0.4 7:0.3 8:0.2 sonota:0.1 计算综合实力
        '上半   （1:1.0 , 2: 0.8  3: 0.7 4:0.6 5:0.5 6:0.4 7:0.3 8:0.2）+ 0.2
        '下半   （1:1.0 , 2: 0.8  3: 0.7 4:0.6 5:0.5 6:0.4 7:0.3 8:0.2）

        MsgBox("ok")

    End Sub


    ''' <summary>
    ''' Read Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click

        'https://www.dszuqiu.com/league/198
        'Dim httpURL As New System.Uri(Me.tbxUrl.Text.Trim)
        'wb1.Url = httpURL

        GetCpDataByUrl(Me.tbxUrl.Text.Trim)

        MsgBox("OK")

    End Sub


    Public Function GetCpDataByUrl(ByVal url As String) As Boolean

        GetSouceUntilComplate(url, wb1)

        WatiWebbrowserComplate(wb1, 10)

        Dim dt As dsCp.cpm_cpDataTable

        For i = 1 To 1000

            dt = GetCpPageData()

            If IsExistData(dt) Then

                InsDataTable(dt)

                If cbReadAg.Checked Then
                Else
                    Exit For
                End If


            Else

                InsDataTable(dt)

            End If

            If Not NextPage() Then
                Return True
            End If

        Next

        Return True

    End Function


    Public Function NextPage() As Boolean

        Dim cls As System.Windows.Forms.HtmlElementCollection = wb1.Document.GetElementById("pager").GetElementsByTagName("a")
        For i As Integer = 0 To cls.Count - 1
            If cls(i).InnerText.Trim = "下一页" Then
                If cls(i).GetAttribute("data-url") Is Nothing OrElse cls(i).GetAttribute("data-url") = "" Then
                    Return False
                End If
                cls(i).InvokeMember("click")
                WatiWebbrowserComplate(wb1, 10)
            End If
        Next
        Return True


    End Function




    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCpPageData() As DataTable
        Dim cls As System.Windows.Forms.HtmlElementCollection = wb1.Document.GetElementById("ended").GetElementsByTagName("tr")
        Dim dt As New dsCp.cpm_cpDataTable
        For i As Integer = 1 To cls.Count - 1

            Dim dr As dsCp.cpm_cpRow
            dr = dt.Newcpm_cpRow

            Dim xi_href As String
            With wb1.Document.GetElementById("ended").GetElementsByTagName("tr")(i)

                dr.league_name = .Children(0).InnerText.Trim
                Dim ymd As String = .Children(2).InnerText.Trim
                xi_href = .Children(11).GetElementsByTagName("a")(0).GetAttribute("href")
                Dim idx As String = xi_href.Split("/")(xi_href.Split("/").Length - 1)

                dr.round = idx
                dr.game_idx = idx
                dr.game_date = CDate(ymd)

                dr.home_team_name = .Children(3).GetElementsByTagName("a")(0).InnerText.Trim
                dr.vist_team_name = .Children(5).GetElementsByTagName("a")(0).InnerText.Trim

                dr.home_team_whole_score = .Children(4).InnerText.Trim.Split(":")(0)
                dr.vist_team_whole_score = .Children(4).InnerText.Trim.Split(":")(1)


                dr.home_team_harf_score = .Children(6).InnerText.Trim.Split(":")(0)
                dr.vist_team_harf_score = .Children(6).InnerText.Trim.Split(":")(1)

                dr.home_team_ranking = 0
                dr.vist_team_ranking = 0

            End With

            dt.Rows.Add(dr)

            If cbPL.Checked Then
                Dim dtPL As dsCp.cpm_plDataTable = GetPlData(xi_href, dr.league_name, dr.game_idx)
                InsDataTable(dtPL)
            End If

        Next

        Return dt

    End Function

    Public Function GetPlData(ByVal url As String, ByVal league_name As String, ByVal game_idx As String) As dsCp.cpm_plDataTable

        GetSouceUntilComplate(url, wb2)
        WatiWebbrowserComplate(wb2, 10)

        Dim dt As New dsCp.cpm_plDataTable
        Dim dr As dsCp.cpm_plRow = dt.Newcpm_plRow

        dr.league_name = league_name
        dr.game_idx = game_idx
        dr.company_name = ""
        dr.st_ed_flg = ""
        dr.pl_win = ""
        dr.pl_draw = ""
        dr.pl_loss = ""

        dt.Rows.Add(dr)

        Return dt

    End Function

    'Public Function SetPageToDB() As Boolean



    '    SetPageToDB = IsExistData(dt)

    '    InsDataTable(dt)

    'End Function






    Public Function GetSouceUntilComplate(ByVal url As String, ByVal wb As WebBrowser) As String
        wb.Url = (New System.Uri(url))
        If WatiWebbrowserComplate(wb, 10) Then
            Return wb.Document.Body.InnerHtml
        Else
            Return ""
        End If
    End Function

    Public Sub Delay(Second As Double)
        Dim tempTime As DateTime = DateTime.Now
        While (tempTime.AddMilliseconds(Second).CompareTo(DateTime.Now) > 0)
            Application.DoEvents()
        End While
    End Sub

    Public Function WatiWebbrowserComplate(ByVal wb As Windows.Forms.WebBrowser, Optional ByVal watiTime As Integer = 10) As Boolean

        Dim i As Integer = 0

        Dim sUrl As String

        Dim waitTm As Integer = 0

        Do While True

            'Threading.Thread.Sleep(100)
            Delay(100)

            waitTm = waitTm + 1 * 1000

            If watiTime * 1000 <= waitTm Then
                Return False
            End If

            If wb.ReadyState = Windows.Forms.WebBrowserReadyState.Complete Then

                If wb.IsBusy = False Then

                    i = i + 1

                    If (i = 2) Then
                        sUrl = wb.Url.ToString
                        If sUrl.Contains("res") Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If

                i = 0

            End If

        Loop

    End Function





    ''' <summary>
    ''' WebbrowserのIeのVer設定する
    '''exeFirstName:
    ''' 
    '''VER:
    '''  11001 (0x2EDF) ：Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive
    '''  11000 (0x2AF8) ：Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode
    '''  10000 (0x2710) ：Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
    '''  10001 (0x2AF7) ：Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.
    '''  9999  (0x270F) ：Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
    '''  9000  (0x2328) ：Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
    '''  8888  (0x22B8) ：Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.
    '''  8000  (0x1F40) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.
    '''  7000  (0x1B58) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.
    ''' Webbrowser
    ''' </summary>
    ''' <param name="exeFirstName"></param>
    ''' <remarks></remarks>
    Public Sub MakeWebbrowserDefaultIe11(ByVal exeFirstName As String, ByVal ver As Integer)

        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", True)
        If (key Is Nothing) Then
            key.SetValue(exeFirstName & ".exe", ver, RegistryValueKind.DWord)
            key.SetValue(exeFirstName & ".vshost.exe", ver, RegistryValueKind.DWord) '调试运行需要加上，否则不起作用
        End If

        key = Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", True)
        If (key Is Nothing) Then
            key.SetValue(exeFirstName & ".exe", ver, RegistryValueKind.DWord)
            key.SetValue(exeFirstName & ".vshost.exe", ver, RegistryValueKind.DWord) '调试运行需要加上，否则不起作用
        End If

        '11001 (0x2EDF) Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive

        '11000 (0x2AF8) ：Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode

        '10000 (0x2710) ：Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.

        '10001 (0x2AF7) ：Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.

        '9999 (0x270F) ：Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.

        '9000 (0x2328) ：Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.

        '8888 (0x22B8) ：Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.

        '8000 (0x1F40) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.

        '7000 (0x1B58) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.

    End Sub



    Public Function IsExistData(ByVal dt As DataTable) As Boolean

        IsExistData = False

        Dim sbDel As New System.Text.StringBuilder

        sbDel.AppendLine("SELECT TOP 1 * FROM " & dt.TableName & " WHERE  ")

        For i As Integer = 0 To dt.Rows.Count - 1

            If i = 0 Then
                sbDel.AppendLine(" ( 1=1 ")
            Else
                sbDel.AppendLine("OR ( 1=1 ")
            End If


            For j = 0 To dt.Columns.Count - 1

                Dim keyKbn As Boolean = False

                For Each pk In dt.PrimaryKey
                    If pk.ToString() = dt.Columns(j).ColumnName Then
                        keyKbn = True
                    End If
                Next

                If keyKbn Then
                    If dt.Columns(j).DataType Is System.Type.GetType("System.DateTime") Then
                        sbDel.AppendLine("AND datediff(day,'" & dt.Columns(j).ColumnName & "' , '" & dt.Rows(i).Item(j).ToString & "') = 0")
                    Else
                        sbDel.AppendLine("AND " & dt.Columns(j).ColumnName & " = '" & dt.Rows(i).Item(j).ToString & "'")
                    End If
                End If

            Next

            sbDel.AppendLine(") ")

            If (i > 0 AndAlso i Mod 50 = 0) OrElse i = dt.Rows.Count - 1 Then
                Dim tmp As DataTable = ExecSelect(sbDel.ToString)
                sbDel.Length = 0

                If tmp.Rows.Count > 0 Then
                    Return True
                    IsExistData = True
                End If

            End If

        Next


    End Function

    ''' <summary>
    ''' Insert DB
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsDataTable(ByVal dt As DataTable) As Boolean

        Dim sbIns As New System.Text.StringBuilder
        Dim sbDel As New System.Text.StringBuilder

        For i As Integer = 0 To dt.Rows.Count - 1

            'DELETE
            sbDel.AppendLine("DELETE FROM " & dt.TableName & " WHERE 1=1 ")
            For j = 0 To dt.Columns.Count - 1
                Dim keyKbn As Boolean = False

                For Each pk In dt.PrimaryKey
                    If pk.ToString() = dt.Columns(j).ColumnName Then
                        keyKbn = True
                    End If
                Next

                If keyKbn Then
                    If dt.Columns(j).DataType Is System.Type.GetType("System.DateTime") Then
                        sbDel.AppendLine("AND datediff(day,'" & dt.Columns(j).ColumnName & "' , '" & dt.Rows(i).Item(j).ToString & "') = 0")
                    Else
                        sbDel.AppendLine("AND " & dt.Columns(j).ColumnName & " = '" & dt.Rows(i).Item(j).ToString & "'")
                    End If
                End If
            Next

            'INSERT
            sbIns.AppendLine("INSERT INTO " & dt.TableName & "(")
            For j = 0 To dt.Columns.Count - 1
                sbIns.AppendLine(IIf(j = 0, "", ",") & dt.Columns(j).ColumnName)
            Next
            sbIns.AppendLine(") VALUES (")
            For j = 0 To dt.Columns.Count - 1
                sbIns.AppendLine(IIf(j = 0, "", ",") & "N'" & dt.Rows(i).Item(j).ToString & "'")
            Next
            sbIns.AppendLine(")")

            If (i > 0 AndAlso i Mod 50 = 0) OrElse i = dt.Rows.Count - 1 Then
                RunSql(sbDel.ToString & vbNewLine & sbIns.ToString)
                sbDel.Length = 0
                sbIns.Length = 0
            End If

        Next

        Return True

    End Function


    Private connStr As String = Init.connCom
    Private InsPrintDataConnect As System.Data.SqlClient.SqlConnection
    Private SQLCommand As System.Data.SqlClient.SqlCommand
    Public Function RunSql(ByVal sql As String) As Boolean

        Dim tmout As New TimeSpan(0, 45, 0)
        Dim options As New TransactionOptions
        '分離レベルスナップショットに明示的に指定
        'options.IsolationLevel = IsolationLevel.Snapshot
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.RequiresNew, tmout)
            Try
                Try
                    InsPrintDataConnect = New System.Data.SqlClient.SqlConnection(connStr)
                    If InsPrintDataConnect.State = ConnectionState.Broken Then
                        InsPrintDataConnect.Close()
                        InsPrintDataConnect.Open()
                    Else
                        InsPrintDataConnect.Open()
                    End If

                Catch ex As Exception
                    InsPrintDataConnect = New System.Data.SqlClient.SqlConnection(connStr)
                    If InsPrintDataConnect.State = ConnectionState.Broken Then
                        InsPrintDataConnect.Close()
                        InsPrintDataConnect.Open()
                    Else
                        InsPrintDataConnect.Open()
                    End If
                End Try

                SQLCommand = New System.Data.SqlClient.SqlCommand(sql.ToString, InsPrintDataConnect)
                SQLCommand.CommandTimeout = 0
                SQLCommand.ExecuteNonQuery()

                SQLCommand.Dispose()
                InsPrintDataConnect.Close()
                InsPrintDataConnect.Dispose()
                '成功の場合
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

        Return True

    End Function

    ''' <summary>    
    ''' 执行查询的操作,(无参)    
    ''' </summary>    
    ''' <param name="cmdText">需要执行语句,一般是Sql语句,也有存储过程</param>      
    ''' <returns>dataTable,查询到的表格</returns>    
    ''' <remarks></remarks>    
    Public Function ExecSelect(ByVal cmdText As String) As DataTable
        '定义cmd命令    
        Dim cmd As New SqlCommand
        '设置连接    
        Dim conn As SqlConnection = New System.Data.SqlClient.SqlConnection(connStr)
        Dim sqlAdapter As SqlDataAdapter
        Dim ds As New DataSet
        '还是给cmd赋值    
        cmd.CommandText = cmdText
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        sqlAdapter = New SqlDataAdapter(cmd)  '实例化adapter    
        Try
            sqlAdapter.Fill(ds)           '用adapter将dataSet填充     
            Return ds.Tables(0)             'datatable为dataSet的第一个表    
        Catch ex As Exception
            Return Nothing
        Finally                            '最后一定要销毁cmd    
            If Not IsNothing(cmd) Then          '如果cmd命令存在    
                cmd.Dispose()                   '销毁    
                cmd = Nothing
            End If
        End Try
    End Function

End Class

Public Class Init

    '
    Public Const connStrHome As String = "Data Source=WIN7U-20150705K\R2; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=1983313a"
    Public Const connStrCompaney As String = "Data Source=10.160.200.39; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=lixil@2014"
    Public Const connStrDell As String = "Data Source=ADP1QD9478YL0O2\ILIKE; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=19833130"
    Public Const connStrWanguo As String = "Data Source=AGOBW-707150707\SQLEXPRESS2008; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=19833130"

    'Public Shared connCom As String = connStrCompaney

    Public Shared Function connCom() As String

        If System.Net.Dns.GetHostName = "WIN7U-20150705K" Then
            Return connStrHome
        ElseIf System.Net.Dns.GetHostName = "ADP1QD9478YL0O2" Then
            Return connStrDell
        ElseIf System.Net.Dns.GetHostName = "AGOBW-707150707" Then
            Return connStrWanguo

        Else
            Return connStrCompaney
        End If
    End Function



End Class
