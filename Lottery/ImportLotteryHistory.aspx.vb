
Imports shell32
Imports mshtml
Imports Microsoft.Win32
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports System.Collections.Generic

Partial Class ImportLotteryHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
     
    End Sub


    Sub InsLotteryHistory()

        'DbAcc.RunSql("insert into m_lottery_history select 'c','1','12','2015-01-01 00:00:00.000','1','1','1','1','1','1','11.000','1.000','1.000','1','1','1'")

        Dim league_name As String
        Dim round As String
        Dim game_idx As String
        Dim game_date As String
        Dim home_team_name As String
        Dim home_team_harf_score As String
        Dim home_team_whole_score As String
        Dim vist_team_name As String
        Dim vist_team_harf_score As String
        Dim vist_team_whole_score As String
        Dim pl_win As String
        Dim pl_ping As String
        Dim pl_lose As String
        Dim half_result As String
        Dim whole_result As String
        Dim weather As String

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

        Dim sb As New StringBuilder
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
        sb.AppendLine("	,'" & league_name & "'")
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
        sb.AppendLine("	,'" & half_result & "'")
        sb.AppendLine("	,'" & whole_result & "'")
        sb.AppendLine("	,'" & weather & "'")

        DbAcc.RunSql(sb.ToString)

    End Sub

End Class
