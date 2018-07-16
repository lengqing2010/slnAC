
Imports Shell32
Imports mshtml
Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports System.Collections.Generic

Public Class Form1


    Public webApp As SHDocVw.InternetExplorer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

        Dim LeagueAndYYYY As String = divLeagueAndYm(0).children(2).innerText
        Dim LeagueName As String = LeagueAndYYYY.Split(" ")(0).Trim
        Dim YYYY As String = LeagueAndYYYY.Split(" ")(1).Trim

        '比分的Table取得
        Dim table As mshtml.IHTMLElement = webApp.Document.getElementById("team_fight_table")
        Dim tbody As mshtml.IHTMLElement = table.children(0)
        Dim trs As mshtml.IHTMLElementCollection = table.children(0).children

        Dim trCnt As Integer = trs.length

        Dim league_name As String = LeagueName
        webApp.Document.getElementById("btnSearchForm").click()

        For i As Integer = 1 To trCnt - 1 '1行 是标题

            Dim tds As mshtml.IHTMLElementCollection = trs(i).children

            '比分
            Dim wholeScroe As String = tds(3).innerText.Trim

            Dim linkUrl As String = tds(3).children(0).getAttribute("href")
            CType(tds(3).children(0), HTMLAnchorElement).click()
            tds(3).children(0).click()
            'CType(tds(3).children(0), HTMLAnchorElement).InvokeMember("href")

            'CType(tds(3).children(0), HTMLAnchorElement).InvokeMember("click")
            '  CType(tds(3).children(0), IHTMLElement).click()

            'CType(tds(3).children(0), mshtml.HTMLInputElement).click()



            Exit Sub

            System.Threading.Thread.Sleep(500)
            Dim newWebApp As SHDocVw.InternetExplorer = GetNewTab(linkUrl)

            'jifen_dashi
            Dim harfEle As List(Of mshtml.HTMLLIElement) = GetElementsByTagAndClass(newWebApp, "div", "jifen_dashi")

            Dim harfScore As String = harfEle(0).innerText.Replace("半:", "").Trim()

            Dim round As String = tds(1).innerText.Trim
            Dim game_idx As String = trs(i).getAttribute("matchid")
            Dim game_date As String = GetYMDHMS(YYYY & "-" & tds(0).innerText)
            Dim home_team_name As String = tds(2).innerText.Trim
            Dim home_team_harf_score As String = GetHomeScore(harfScore)
            Dim home_team_whole_score As String = GetHomeScore(wholeScroe)
            Dim vist_team_name As String = tds(4).innerText.Trim
            Dim vist_team_harf_score As String = GetVistScore(harfScore)
            Dim vist_team_whole_score As String = GetVistScore(wholeScroe)
            Dim pl_win As String = tds(5).innerText.Trim
            Dim pl_ping As String = tds(6).innerText.Trim
            Dim pl_lose As String = tds(7).innerText.Trim
            Dim half_result As String = GetResult（home_team_harf_score， vist_team_harf_score）
            Dim whole_result As String = GetResult（home_team_whole_score， vist_team_whole_score）

            Dim weatherEle As List(Of mshtml.HTMLLIElement) = GetElementsByTagAndClass(newWebApp, "div", "qbx_2")
            Dim weather As String = Trim(weatherEle(0).children(2).innerText)

            Call InsLotteryHistory(league_name _
                        , round _
                        , game_idx _
                        , game_date _
                        , home_team_name _
                        , home_team_harf_score _
                        , home_team_whole_score _
                        , vist_team_name _
                        , vist_team_harf_score _
                        , vist_team_whole_score _
                        , pl_win _
                        , pl_ping _
                        , pl_lose _
                        , half_result _
                        , whole_result _
                        , weather)

        Next
    End Sub


    ''' <summary>
    ''' データ登録
    ''' </summary>
    ''' <param name="league_name"></param>
    ''' <param name="round"></param>
    ''' <param name="game_idx"></param>
    ''' <param name="game_date"></param>
    ''' <param name="home_team_name"></param>
    ''' <param name="home_team_harf_score"></param>
    ''' <param name="home_team_whole_score"></param>
    ''' <param name="vist_team_name"></param>
    ''' <param name="vist_team_harf_score"></param>
    ''' <param name="vist_team_whole_score"></param>
    ''' <param name="pl_win"></param>
    ''' <param name="pl_ping"></param>
    ''' <param name="pl_lose"></param>
    ''' <param name="half_result"></param>
    ''' <param name="whole_result"></param>
    ''' <param name="weather"></param>
    ''' <remarks></remarks>
    Sub InsLotteryHistory(league_name As String _
                        , round As String _
                        , game_idx As String _
                        , game_date As String _
                        , home_team_name As String _
                        , home_team_harf_score As String _
                        , home_team_whole_score As String _
                        , vist_team_name As String _
                        , vist_team_harf_score As String _
                        , vist_team_whole_score As String _
                        , pl_win As String _
                        , pl_ping As String _
                        , pl_lose As String _
                        , half_result As String _
                        , whole_result As String _
                        , weather As String)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("INSERT INTO")
        sb.AppendLine("m_lottery_history(")
        sb.AppendLine("	,league_name")
        sb.AppendLine("	,round")
        sb.AppendLine("	,game_idx")
        sb.AppendLine("	,game_date")
        sb.AppendLine("	,home_team_name")
        sb.AppendLine("	,home_team_harf_score")
        sb.AppendLine("	,home_team_whole_score")
        sb.AppendLine("	,vist_team_name")
        sb.AppendLine("	,vist_team_harf_score")
        sb.AppendLine("	,vist_team_whole_score")
        sb.AppendLine("	,pl_win")
        sb.AppendLine("	,pl_ping")
        sb.AppendLine("	,pl_lose")
        sb.AppendLine("	,half_result")
        sb.AppendLine("	,whole_result")
        sb.AppendLine("	,weather")
        sb.AppendLine(")VALUES(")
        sb.AppendLine("	,N'" & league_name & "'")
        sb.AppendLine("	,'" & round & "'")
        sb.AppendLine("	,'" & game_idx & "'")
        sb.AppendLine("	,'" & game_date & "'")
        sb.AppendLine("	,'" & home_team_name & "'")
        sb.AppendLine("	,'" & home_team_harf_score & "'")
        sb.AppendLine("	,'" & home_team_whole_score & "'")
        sb.AppendLine("	,'" & vist_team_name & "'")
        sb.AppendLine("	,'" & vist_team_harf_score & "'")
        sb.AppendLine("	,'" & vist_team_whole_score & "'")
        sb.AppendLine("	,'" & pl_win & "'")
        sb.AppendLine("	,'" & pl_ping & "'")
        sb.AppendLine("	,'" & pl_lose & "'")
        sb.AppendLine("	,N'" & half_result & "'")
        sb.AppendLine("	,N'" & whole_result & "'")
        sb.AppendLine("	,N'" & weather & "'")

        DbAcc.RunSql(sb.ToString)

    End Sub



    Protected Sub Unnamed1_Click(sender As Object, e As EventArgs)

    End Sub
    Public Function GetResult(ByVal homeScore As String, ByVal VistScore As String) As String
        If homeScore > VistScore Then
            Return "胜"
        ElseIf homeScore > VistScore Then
            Return "负"
        Else
            Return "平"
        End If
    End Function
    Public Function GetHomeScore(ByVal v As String) As String
        If v.Split("-").Count = 2 Then
            Return v.Split("-")(0).Trim
        Else
            Return ""
        End If
    End Function

    Public Function GetVistScore(ByVal v As String) As String
        If v.Split("-").Count = 2 Then
            Return v.Split("-")(1).Trim
        Else
            Return ""
        End If
    End Function


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
                Dim csNames As List(Of String) = ArrToList(CType(HtmlElements.item(i), mshtml.IHTMLElement).className.Split(" "))
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


End Class