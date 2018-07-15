
Imports shell32
Imports mshtml

Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports System.Collections.Generic

Public Class Form1


    'Public shell As New Shell32.Shell
    Public webApp As SHDocVw.InternetExplorer




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        RunSql("insert into cpm_cp select 'b','1','12','2015-01-01 00:00:00.000','1','1','1','1','1','1','11.000','1.000','1.000','1','1','1'")

        ''IE 操作
        ''引用了下面3个 包
        ''Microsoft Internet Controls                Interop.SHDocVw.dll
        ''Microsoft HTML Object Library              Microsoft.mshtml.dll
        ''Microsoft Shell Controls And Automation

        'Dim url As String = "http://www.baidu.com/"

        'webApp = New SHDocVw.InternetExplorer



        'Dim wbDocument As mshtml.HTMLDocument = webApp.Document
        'wbDocument.activeElement.innerText = "111"

        'rtbxMsg.Text = a.ActiveElement.OuterHtml


    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '打开主页面
        OpenAndWaitComplete("http://www.okooo.com/soccer/league/20/schedule/13006/1-5-1/")
        Dim wbDocument As mshtml.HTMLDocument = webApp.Document
        rtbxMsg.Text = wbDocument.body.innerHTML

        Dim divLeagueAndYm As List(Of mshtml.HTMLLIElement) = GetElementsByTagAndClass(webApp, "div", "clearfix NewLotteryLeftNav")
        If divLeagueAndYm.Count = 0 Then
            MsgBox("没有找到比分元素")
        End If

        Dim LeagueAndYYYY As String = divLeagueAndYm(0).Children(2).InnerText
        Dim LeagueName As String = LeagueAndYYYY.Split(" ")(0).Trim
        Dim YYYY As String = LeagueAndYYYY.Split(" ")(1).Trim

        '比分的Table取得
        Dim table As mshtml.IHTMLElement = webApp.Document.GetElementById("team_fight_table")
        Dim tbody As mshtml.IHTMLElement = table.children(0)
        Dim trs As mshtml.IHTMLElementCollection = table.children(0).Children

        Dim trCnt As Integer = trs.length
        Dim dt As New DataBase.cpm_cpDataTable
        For i As Integer = 1 To trCnt - 1 '1行 是标题

            Dim tds As mshtml.IHTMLElementCollection = trs(i).Children
            Dim dr As DataBase.cpm_cpRow = dt.Newcpm_cpRow

            dr.league_name = LeagueName
            dr.game_date = GetYMDHMS(YYYY & "-" & tds(0).InnerText)
            dr.round = tds(1).InnerText.Trim
            dr.game_idx = trs(i).GetAttribute("matchid")

            '比分
            Dim wholeScroe As String = tds(3).InnerText.Trim

            'tds(3).Children(0).InvokeMember("click")


            'Dim harfInfo As String = GetHarfDataByUrl(tds(3).Children(0).GetAttribute("href"))
            '' Return True
            Dim linkUrl As String = tds(3).Children(0).GetAttribute("href")
            tds(3).Children(0).click()

            Dim newWebApp As SHDocVw.InternetExplorer = GetNewTab(linkUrl)

            'jifen_dashi
            Dim harfEle As List(Of mshtml.HTMLLIElement) = GetElementsByTagAndClass(newWebApp, "div", "jifen_dashi")

            Dim harfScore As String = harfEle(0).InnerText.Replace("半:", "").Trim()

            Dim weather As List(Of mshtml.HTMLLIElement) = GetElementsByTagAndClass(newWebApp, "div", "qbx_2")
            Dim weatherTxt As String = Trim(weather(0).Children(2).InnerText)


            'Dim vistScroe As String = harfInfo.Split("|")(0)

            'dr.home_team_whole_score = GetScore(wholeScroe, "home")
            'dr.vist_team_whole_score = GetScore(wholeScroe, "vist")
            'dr.home_team_harf_score = GetScore(vistScroe, "home")
            'dr.vist_team_harf_score = GetScore(vistScroe, "vist")

            ''队伍名
            'dr.home_team_name = tds(2).InnerText.Trim
            'dr.vist_team_name = tds(4).InnerText.Trim

            'dr.weather = vistScroe.Split("|")(1)


        Next


    End Sub


    Public Function GetNewTab(ByVal linkUrl As String) As SHDocVw.InternetExplorer
        Dim mShellWindow As New SHDocVw.ShellWindows
        For Each iw As SHDocVw.InternetExplorer In mShellWindow
            If System.IO.Path.GetFileNameWithoutExtension(iw.FullName).ToLower().Equals("iexplore") Then
                WaitComplete(iw)
                ' Dim title As String = iw.Document.Title
                If CType(iw.Document, mshtml.HTMLDocument).url.Contains(linkUrl) Then

                    Return iw
                End If
            End If
        Next
        Return Nothing
    End Function


    Public Function GetElementsByTagAndClass(ByVal wb As SHDocVw.InternetExplorer, ByVal tagName As String, ByVal className As String) As List(Of mshtml.HTMLLIElement)

        Dim wbDocument As mshtml.HTMLDocument = wb.Document

        Dim HtmlElements As mshtml.IHTMLElementCollection = wbDocument.getElementsByTagName(tagName)

        Dim rtvHtmlElements As New List(Of mshtml.HTMLLIElement)

        For i As Integer = 0 To HtmlElements.length - 1
            If CType(HtmlElements.item(i), mshtml.IHTMLElement).className Is Nothing Then
                Continue For

            ElseIf CType(HtmlElements.item(i), mshtml.IHTMLElement).className.Trim = "" Then
                Continue For
            Else
                Dim csNames As List(Of String) = ArrToList(HtmlElements.item(i).className.Split(" "))
                Dim csName As String = CType(HtmlElements.item(i), mshtml.IHTMLElement).className

                Dim classNameKeys As List(Of String) = ArrToList(className.Split(" "))

                If csName = className OrElse ArrContainsValue(csNames, classNameKeys) Then
                    rtvHtmlElements.Add(HtmlElements.item(i))
                End If
            End If
        Next

        Return rtvHtmlElements

    End Function




    Public Function ArrContainsValue(ByVal arr As List(Of String), ByVal keys As List(Of String)) As Boolean
        For i As Integer = 0 To keys.Count - 1
            If Not arr.Contains(keys(i)) Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function ArrToList(ByVal arr As String()) As List(Of String)
        Dim lst As New List(Of String)
        For i As Integer = 0 To arr.Length - 1
            lst.Add(arr(i).ToLower)
        Next
        Return lst
    End Function

    Public Function GetYMDHMS(ByVal s As String) As String

        If s.Trim = "" Then
            Return ""
        Else
            Return CDate(s.Trim).ToString("yyyy/MM/dd HH:mm:ss")
        End If
    End Function

    Public Function GetScore(ByVal s As String, ByVal flg As String) As String

        If s.Trim() = "-" Then
            Return ""
        End If



        If flg = "home" Then
            Return s.Split("-")(0)
        Else
            Return s.Split("-")(1)
        End If


    End Function


    Public Function OpenAndWaitComplete(ByVal url As String)
        If webApp Is Nothing Then
            webApp = New SHDocVw.InternetExplorer
        End If
        webApp.Navigate(url)
        webApp.Visible = True
        WaitComplete(webApp)
    End Function


    Sub WaitComplete(ByRef webApp As Object)

        Do Until webApp.ReadyState = 4 And Not webApp.Busy
            System.Windows.Forms.Application.DoEvents()
            System.Threading.Thread.Sleep(500)
        Loop
        System.Threading.Thread.Sleep(100)

    End Sub

    Sub ClickHtmlElement(ByRef webApp As Object, ByVal id As String)

        WaitComplete(webApp)
        webApp.Document.getElementById(id).Click()
        'Call webApp.Document.GetElementById(id).InvokeMember("click")

        WaitComplete(webApp)

    End Sub





    Private connStr As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DATA\Database.mdf;Integrated Security=True"
    Private InsPrintDataConnect As System.Data.SqlClient.SqlConnection
    Private SQLCommand As System.Data.SqlClient.SqlCommand
    Public Function RunSql(ByVal sql As String) As Boolean

        Dim tmout As New TimeSpan(0, 45, 0)
        Dim options As New Transactions.TransactionOptions
        '分離レベルスナップショットに明示的に指定
        'options.IsolationLevel = IsolationLevel.Snapshot
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.RequiresNew, tmout)
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

End Class
