Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports System.Collections.Generic
Public Class ImportCpData



    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click

        '挪超2017
        'http://www.okooo.com/soccer/league/20/schedule/13006/1-5-1/

        Dim url As String = "http://www.okooo.com/soccer/league/20/schedule/13006/1-5-1/"

        GetCpDataByUrl(url)

    End Sub


    ''' <summary>
    ''' 获得彩票数据
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCpDataByUrl(ByVal url As String) As Boolean

        '打开网页
        GetSouceUntilComplate(url, wb1)
        '等待
        WatiWebbrowserComplate(WB1, 10)

        Dim divLeagueAndYm As List(Of HtmlElement) = GetElementsByTagAndClass(WB1, "div", "clearfix NewLotteryLeftNav")

        Dim LeagueAndYYYY As String = divLeagueAndYm(0).Children(2).InnerText
        Dim LeagueName As String = LeagueAndYYYY.Split(" ")(0).Trim
        Dim YYYY As String = LeagueAndYYYY.Split(" ")(1).Trim

        '比分的Table取得
        Dim table As HtmlElement = WB1.Document.GetElementById("team_fight_table")
        Dim tbody As HtmlElement = table.Children(0)
        Dim trs As HtmlElementCollection = table.Children(0).Children

        Dim trCnt As Integer = trs.Count

        Dim dt As New DataBase.cpm_cpDataTable

        For i As Integer = 1 To trCnt - 1 '1行 是标题

            Dim tds As HtmlElementCollection = trs(i).Children
            Dim dr As DataBase.cpm_cpRow = dt.Newcpm_cpRow

            dr.league_name = LeagueName
            dr.game_date = GetYMDHMS(YYYY & "-" & tds(0).InnerText)
            dr.round = tds(1).InnerText.Trim
            dr.game_idx = trs(i).GetAttribute("matchid")

            '比分
            Dim wholeScroe As String = tds(3).InnerText.Trim

            tds(3).Children(0).InvokeMember("onclick")

            Dim harfInfo As String = GetHarfDataByUrl(tds(3).Children(0).GetAttribute("href"))
            Return True

            Dim vistScroe As String = harfInfo.Split("|")(0)

            dr.home_team_whole_score = GetScore(wholeScroe, "home")
            dr.vist_team_whole_score = GetScore(wholeScroe, "vist")
            dr.home_team_harf_score = GetScore(vistScroe, "home")
            dr.vist_team_harf_score = GetScore(vistScroe, "vist")

            '队伍名
            dr.home_team_name = tds(2).InnerText.Trim
            dr.vist_team_name = tds(4).InnerText.Trim

            dr.weather = vistScroe.Split("|")(1)


        Next

        'Dim dt As DataBase.cpm_cpDataTable

        'For i = 1 To 1000

        '    dt = GetCpPageData()

        '    If IsExistData(dt) Then

        '        InsDataTable(dt)

        '        If cbReadAg.Checked Then
        '        Else
        '            Exit For
        '        End If


        '    Else

        '        InsDataTable(dt)

        '    End If

        '    If Not NextPage() Then
        '        Return True
        '    End If

        'Next

        Return True

    End Function

    Public Function GetHarfDataByUrl(ByVal url As String) As String

        Dim wb As WebBrowser = WB2

        '打开网页
        GetSouceUntilComplate(url, wb)
        '等待
        WatiWebbrowserComplate(wb, 10)

        Return ""

        'jifen_dashi
        Dim harfEle As List(Of HtmlElement) = GetElementsByTagAndClass(wb, "div", "jifen_dashi")

        Dim harfScore As String = harfEle(0).InnerText.Replace("半:", "").Trim()

        Dim weather As List(Of HtmlElement) = GetElementsByTagAndClass(wb, "div", "qbx_2")
        Dim weatherTxt As String = Trim(weather(0).Children(2).InnerText)

        Return harfScore & "|" & weatherTxt

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCpPageData() As DataTable
        Dim cls As System.Windows.Forms.HtmlElementCollection = wb1.Document.GetElementById("ended").GetElementsByTagName("tr")
        Dim dt As New DataBase.cpm_cpDataTable
        For i As Integer = 1 To cls.Count - 1

            Dim dr As DataBase.cpm_cpRow
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

            End With

            dt.Rows.Add(dr)

        Next

        Return dt

    End Function


    ''' <summary>
    ''' 获得网页源代码
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="wb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSouceUntilComplate(ByVal url As String, ByVal wb As WebBrowser) As String
        wb.Url = (New System.Uri(url))
        If WatiWebbrowserComplate(wb, 10) Then
            Return wb.Document.Body.InnerHtml
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' 等待
    ''' </summary>
    ''' <param name="Second"></param>
    ''' <remarks></remarks>
    Public Sub Delay(Second As Double)
        Dim tempTime As DateTime = DateTime.Now
        While (tempTime.AddMilliseconds(Second).CompareTo(DateTime.Now) > 0)
            Application.DoEvents()
        End While
    End Sub

    ''' <summary>
    ''' 等待浏览器执行完毕
    ''' </summary>
    ''' <param name="Second"></param>
    ''' <remarks></remarks>
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


    Public Function GetElementsByTagAndClass(ByVal wb As WebBrowser, ByVal tagName As String, ByVal className As String) As List(Of HtmlElement)

        Dim HtmlElements As HtmlElementCollection = wb.Document.GetElementsByTagName(tagName)
        Dim rtvHtmlElements As New List(Of HtmlElement)

        For i As Integer = 0 To HtmlElements.Count - 1

            If HtmlElements.Item(i).GetAttribute("classname").Trim = "" Then
                Continue For
            Else
                Dim csNames As List(Of String) = ArrToList(HtmlElements.Item(i).GetAttribute("classname").Split(" "))

                Dim classNameKeys As List(Of String) = ArrToList(className.Split(" "))

                If ArrContainsValue(csNames, classNameKeys) Then
                    rtvHtmlElements.Add(HtmlElements(i))
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

    Private Sub WB2_ProgressChanged(sender As Object, e As WebBrowserProgressChangedEventArgs) Handles WB2.ProgressChanged

    End Sub

    Private Sub Window_Error(ByVal sender As Object, ByVal e As HtmlElementErrorEventArgs)
        e.Handled = True
    End Sub

End Class