
Partial Class Cp
    Inherits System.Web.UI.Page
	'Team 信息
    Public Function GetTeamInfo(ByVal league_name As String, ByVal team_name As String, Optional ByVal top As String = "", Optional ByVal ZKQ As String = "全")
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            If top = "" Then
            Else
                .AppendLine("TOP " & top)
            End If
            .AppendLine("   a.[league_name]")
            .AppendLine("      ,[round]")
            .AppendLine("      ,a.[game_idx]")
            .AppendLine("      ,[game_date]")
            .AppendLine("      ,[home_team_name]")
            .AppendLine("      ,[home_team_harf_score]")
            .AppendLine("      ,[home_team_whole_score]")
            .AppendLine("      ,[home_team_ranking]")
            .AppendLine("      ,[vist_team_name]")
            .AppendLine("      ,[vist_team_harf_score]")
            .AppendLine("      ,[vist_team_whole_score]")
            .AppendLine("      ,[vist_team_ranking]")
            .AppendLine("	  ,[home_team_harf_score] - [vist_team_harf_score] as '净胜球上半'")
            .AppendLine("	  ,[home_team_harf_score] - [vist_team_harf_score] as '净胜球全'")
            .AppendLine("	  ,case when [home_team_harf_score]>[vist_team_harf_score] then N'胜'")
            .AppendLine("	   when [home_team_harf_score]=[vist_team_harf_score] then N'平'")
            .AppendLine("	   else N'负'")
            .AppendLine("	   end  as '半胜平负'")
            .AppendLine("	  ,case when [home_team_whole_score]>[vist_team_whole_score] then N'胜'")
            .AppendLine("	   when [home_team_whole_score]=[vist_team_whole_score] then N'平'")
            .AppendLine("	   else N'负'")
            .AppendLine("	   end  as '全胜平负'")
            .AppendLine("	  ,[home_team_harf_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 0 then 0")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 1 then 1")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 2 then 1.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 3 then 2.4")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 4 then 2.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 5 then 3.2")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] > 0 then 3.5")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -1 then -1")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -2 then -1.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -3 then -2.4")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -4 then -2.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -5 then -3.2")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] <  0 then -3.5")
            .AppendLine("	   end as '半能力'")
            .AppendLine("")
            .AppendLine("	  ,[home_team_whole_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 0 then 0")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 1 then 1")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 2 then 1.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 3 then 2.4")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 4 then 2.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 5 then 3.2")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] > 0 then 3.5")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -1 then -1")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -2 then -1.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -3 then -2.4")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -4 then -2.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -5 then -3.2")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] <  0 then -3.5")
            .AppendLine("	   end as '全能力'")
            .AppendLine("	  ,[vist_team_harf_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 0 then 0")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 1 then -1")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 2 then -1.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 3 then -2.4")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 4 then -2.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 5 then -3.2")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] > 0 then -3.5")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -1 then 1")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -2 then 1.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -3 then 2.4")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -4 then 2.8")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -5 then 3.2")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] <  0 then 3.5")
            .AppendLine("	   end as '客半能力'")
            .AppendLine("	  ,[vist_team_whole_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 0 then 0")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 1 then -1")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 2 then -1.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 3 then -2.4")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 4 then -2.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 5 then -3.2")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] > 0 then -3.5")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -1 then 1")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -2 then 1.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -3 then 2.4")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -4 then 2.8")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -5 then 3.2")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] <  0 then 3.5")
            .AppendLine("	   end as '客全能力'")

            .AppendLine("	   ,0.0001 as homeharfScore")
            .AppendLine("	   ,0.0001 as homewholeScore")
            .AppendLine("	   ,0.0001 as vistharfScore")
            .AppendLine("	   ,0.0001 as vistwholeScore")

            .AppendLine("	   ,''  as '预测半胜平负'")
            .AppendLine("	   ,''  as '预测全胜平负'")
            .AppendLine("	   ,''  as '预测半结果'")
            .AppendLine("	   ,''  as '预测全结果'")
            .AppendLine("  FROM [cpm_cp] a")
            .AppendLine("  LEFT JOIN cpm_pl b")
            .AppendLine("  on a.[league_name] = b.[league_name]")
            .AppendLine("  AND a.[game_idx] = b.[game_idx]")
            .AppendLine("  ")
            .AppendLine("  where a.[league_name]=N'" & league_name & "'")

            If team_name <> "" Then


                If ZKQ = "全" Then
                    .AppendLine("  and ([home_team_name]=N'" & team_name & "' or [vist_team_name]=N'" & team_name & "')")
                ElseIf ZKQ = "主" Then
                    .AppendLine("  and ([home_team_name]=N'" & team_name & "')")
                ElseIf ZKQ = "客" Then
                    .AppendLine("  and ([vist_team_name]=N'" & team_name & "')")
                Else
                End If
            End If
                .AppendLine("  order by game_date desc")
        End With
        Return COMMON.NewMsSql.CSel(sb.ToString)
    End Function


    Public Function GetTeamInfoKeisan(ByVal league_name As String, ByVal team_name As String, ByVal arr As List(Of Decimal), Optional ByVal top As String = "", Optional ByVal ZKQ As String = "全") As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            If top = "" Then
            Else
                .AppendLine("TOP " & top)
            End If
            .AppendLine("   a.[league_name]")
            .AppendLine("      ,[round]")
            .AppendLine("      ,a.[game_idx]")
            .AppendLine("      ,[game_date]")
            .AppendLine("      ,[home_team_name]")
            .AppendLine("      ,[home_team_harf_score]")
            .AppendLine("      ,[home_team_whole_score]")
            .AppendLine("      ,[home_team_ranking]")
            .AppendLine("      ,[vist_team_name]")
            .AppendLine("      ,[vist_team_harf_score]")
            .AppendLine("      ,[vist_team_whole_score]")
            .AppendLine("      ,[vist_team_ranking]")
            .AppendLine("	  ,[home_team_harf_score] - [vist_team_harf_score] as '净胜球上半'")
            .AppendLine("	  ,[home_team_harf_score] - [vist_team_harf_score] as '净胜球全'")
            .AppendLine("	  ,case when [home_team_harf_score]>[vist_team_harf_score] then N'胜'")
            .AppendLine("	   when [home_team_harf_score]=[vist_team_harf_score] then N'平'")
            .AppendLine("	   else N'负'")
            .AppendLine("	   end  as '半胜平负'")
            .AppendLine("	  ,case when [home_team_whole_score]>[vist_team_whole_score] then N'胜'")
            .AppendLine("	   when [home_team_whole_score]=[vist_team_whole_score] then N'平'")
            .AppendLine("	   else N'负'")
            .AppendLine("	   end  as '全胜平负'")
            .AppendLine("	  ,[home_team_harf_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 0 then " & arr(0))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 1 then " & arr(7))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 2 then " & arr(8))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 3 then " & arr(9))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 4 then " & arr(10))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 5 then " & arr(11))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] > 0 then " & arr(12))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -1 then " & arr(1))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -2 then " & arr(2))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -3 then " & arr(3))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -4 then " & arr(4))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -5 then " & arr(5))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] <  0 then " & arr(6))
            .AppendLine("	   end as '半能力'")
            .AppendLine("")
            .AppendLine("	  ,[home_team_whole_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 0 then " & arr(0))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 1 then " & arr(7))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 2 then " & arr(8))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 3 then " & arr(9))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 4 then " & arr(10))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 5 then " & arr(11))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] > 0 then " & arr(12))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -1 then " & arr(1))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -2 then " & arr(2))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -3 then " & arr(3))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -4 then " & arr(4))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -5 then " & arr(5))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] <  0 then " & arr(6))
            .AppendLine("	   end as '全能力'")
            .AppendLine("	  ,[vist_team_harf_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 0 then " & arr(0))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 1 then " & arr(1))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 2 then " & arr(2))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 3 then " & arr(3))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 4 then " & arr(4))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = 5 then " & arr(5))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] > 0 then " & arr(6))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -1 then " & arr(7))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -2 then " & arr(8))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -3 then " & arr(9))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -4 then " & arr(10))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] = -5 then " & arr(11))
            .AppendLine("	   when [home_team_harf_score] - [vist_team_harf_score] <  0 then " & arr(12))
            .AppendLine("	   end as '客半能力'")
            .AppendLine("	  ,[vist_team_whole_score] + ")
            .AppendLine("	   case ")
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 0 then " & arr(0))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 1 then " & arr(1))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 2 then " & arr(2))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 3 then " & arr(3))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 4 then " & arr(4))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = 5 then " & arr(5))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] > 0 then " & arr(6))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -1 then " & arr(7))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -2 then " & arr(8))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -3 then " & arr(9))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -4 then " & arr(10))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] = -5 then " & arr(11))
            .AppendLine("	   when [home_team_whole_score] - [vist_team_whole_score] <  0 then " & arr(12))
            .AppendLine("	   end as '客全能力'")

            .AppendLine("	   ,0.0001 as homeharfScore")
            .AppendLine("	   ,0.0001 as homewholeScore")
            .AppendLine("	   ,0.0001 as vistharfScore")
            .AppendLine("	   ,0.0001 as vistwholeScore")

            .AppendLine("	   ,''  as '预测半胜平负'")
            .AppendLine("	   ,''  as '预测全胜平负'")
            .AppendLine("	   ,''  as '预测半结果'")
            .AppendLine("	   ,''  as '预测全结果'")
            .AppendLine("  FROM [cpm_cp] a")
            .AppendLine("  LEFT JOIN cpm_pl b")
            .AppendLine("  on a.[league_name] = b.[league_name]")
            .AppendLine("  AND a.[game_idx] = b.[game_idx]")
            .AppendLine("  ")
            .AppendLine("  where a.[league_name]=N'" & league_name & "'")

            If team_name <> "" Then


                If ZKQ = "全" Then
                    .AppendLine("  and ([home_team_name]=N'" & team_name & "' or [vist_team_name]=N'" & team_name & "')")
                ElseIf ZKQ = "主" Then
                    .AppendLine("  and ([home_team_name]=N'" & team_name & "')")
                ElseIf ZKQ = "客" Then
                    .AppendLine("  and ([vist_team_name]=N'" & team_name & "')")
                Else
                End If
            End If
            .AppendLine("  order by game_date desc")
        End With
        Return COMMON.NewMsSql.CSel(sb.ToString)
    End Function



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
    '单个Team
    Protected Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSel.Click
        Dim teamName As String = tbxTeamName.Text.Trim
        Dim zkq As String = Me.ddlZKQ.Items(Me.ddlZKQ.SelectedIndex).Text
        Dim top As String = Me.ddlTop.Items(Me.ddlTop.SelectedIndex).Text
        Dim league_name As String = Me.ddlLeague_name.Items(Me.ddlLeague_name.SelectedIndex).Text

        Dim GameResultTmp1 As GameResult = GetData(league_name, teamName, zkq, top, gvHome)
        lblHalf.Text = GameResultTmp1.str 
    End Sub
    '双Team
    Protected Sub btnSelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelAll.Click
        'Dim teamName As String = tbxTeamName.Text.Trim
        Dim zkq As String = Me.ddlZKQ.Items(Me.ddlZKQ.SelectedIndex).Text
        Dim top As String = Me.ddlTop.Items(Me.ddlTop.SelectedIndex).Text
        Dim league_name As String = Me.ddlLeague_name.Items(Me.ddlLeague_name.SelectedIndex).Text

        Dim GameResultTmp1 As GameResult = GetData(league_name, tbxTeamName.Text.Trim, "主", top, gvHome)
        Dim GameResultTmp2 As GameResult = GetData(league_name, tbxVistName.Text.Trim, "客", top, gvVist)

        lblHalf.Text = GameResultTmp1.str
        lblWhole.Text = GameResultTmp2.str
    End Sub
    
    Public Function GetData(ByVal league_name As String, ByVal teamName As String, ByVal zkq As String, ByVal top As String, ByVal gv As GridView) As GameResult
        ' Dim league_name As String = Me.ddlLeague_name.Items(Me.ddlLeague_name.SelectedIndex).Text
        Dim dt As Data.DataTable = GetTeamInfo(league_name, teamName, top, zkq)

        If gv IsNot Nothing Then
            gv.DataSource = dt
            gv.DataBind()
        End If

        Return MakeGameResult(dt, teamName)
        'lblWhole.Text = (wholeF / dt.Rows.Count).ToString("0#.###")
    End Function

    Private Function MakeGameResult(ByVal dt As Data.DataTable, ByVal teamName As String) As GameResult


        'Dim halfF As Decimal = 0
        'Dim wholeF As Decimal = 0


        Dim GameResultTmp As New GameResult
        GameResultTmp.data = dt

        If teamName = "" Then
            Return GameResultTmp
        End If

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("home_team_name") = teamName Then
                GameResultTmp.homeCnt += 1
                GameResultTmp.harfScore += CDec(dt.Rows(i).Item("半能力"))
                GameResultTmp.homeharfScore += CDec(dt.Rows(i).Item("半能力"))

                GameResultTmp.wholeScore += CDec(dt.Rows(i).Item("全能力"))
                GameResultTmp.homewholeScore += CDec(dt.Rows(i).Item("全能力"))

                'halfF += CDec(dt.Rows(i).Item("半能力"))
                'wholeF += CDec(dt.Rows(i).Item("全能力"))
            ElseIf dt.Rows(i).Item("vist_team_name") = teamName Then

                GameResultTmp.vistCnt += 1

                GameResultTmp.harfScore += CDec(dt.Rows(i).Item("半能力"))
                GameResultTmp.vistharfScore += CDec(dt.Rows(i).Item("客半能力"))

                GameResultTmp.wholeScore += CDec(dt.Rows(i).Item("全能力"))
                GameResultTmp.vistwholeScore += CDec(dt.Rows(i).Item("客全能力"))
                'halfF += CDec(dt.Rows(i).Item("客半能力"))
                'wholeF += CDec(dt.Rows(i).Item("客全能力"))
            End If
        Next

        GameResultTmp.suu_harf_score = CHU(GameResultTmp.harfScore, (GameResultTmp.homeCnt + GameResultTmp.vistCnt))
        GameResultTmp.suu_whole_score = CHU(GameResultTmp.wholeScore, (GameResultTmp.homeCnt + GameResultTmp.vistCnt))

        GameResultTmp.suu_home_harf_score = CHU(GameResultTmp.homeharfScore, GameResultTmp.homeCnt)
        GameResultTmp.suu_home_whole_score = CHU(GameResultTmp.homewholeScore, GameResultTmp.homeCnt)

        GameResultTmp.suu_vist_harf_score = CHU(GameResultTmp.vistharfScore, GameResultTmp.vistCnt)
        GameResultTmp.suu_vist_whole_score = CHU(GameResultTmp.vistwholeScore, GameResultTmp.vistCnt)


        Dim str As String = ""
        str = str & "主客 半能力:"
        str = str & GameResultTmp.suu_harf_score.ToString("0#.###")

        str = str & "主客 全能力:"
        str = str & GameResultTmp.suu_whole_score.ToString("0#.###")

        str = str & "主 半能力:"
        str = str & GameResultTmp.suu_home_harf_score.ToString("0#.###")

        str = str & "主 全能力:"
        str = str & GameResultTmp.suu_home_whole_score.ToString("0#.###")

        str = str & "主客 半能力:"
        str = str & GameResultTmp.suu_vist_harf_score.ToString("0#.###")

        str = str & "主客 全能力:"
        str = str & GameResultTmp.suu_vist_whole_score.ToString("0#.###")



        Return GameResultTmp

    End Function

    Public Function CHU(ByVal v1 As Decimal, ByVal v2 As Decimal) As Decimal
        If v2 = 0 Then
            Return 0

        Else
            Return (v1 / v2).ToString("#0.00")
        End If
    End Function


    Dim sb As StringBuilder

    Protected Sub btnKeisan_Click(sender As Object, e As EventArgs) Handles btnKeisan.Click
        Dim league_name As String = Me.ddlLeague_name.Items(Me.ddlLeague_name.SelectedIndex).Text
        sb = New StringBuilder

        Dim lstS As New List(Of Decimal)
        lstS.Add(0)

        lstS.Add(-1)
        lstS.Add(-1.8)
        lstS.Add(-2.4)
        lstS.Add(-2.8)
        lstS.Add(-3.2)
        lstS.Add(-3.5)
        lstS.Add(1)
        lstS.Add(1.8)
        lstS.Add(2.4)
        lstS.Add(2.8)
        lstS.Add(3.2)
        lstS.Add(3.5)

        Dim dtAll As Data.DataTable = GetTeamInfoKeisan(league_name, "", lstS, "", "")


        keisan(dtAll)

        lblKeisanResult.Text = sb.ToString

        'Public homeharfScore As Decimal
        'Public homewholeScore As Decimal
        'Public vistharfScore As Decimal
        'Public vistwholeScore As Decimal

    End Sub

    Sub keisan(ByVal dtAll As Data.DataTable)

        Dim rightHalfSuu As Integer = 0
        Dim rightSuu As Integer = 0
        Dim rightZhuKeSuu As Integer = 0

        For i As Integer = 0 To dtAll.Rows.Count - 1

            Dim home_team_name As String = dtAll.Rows(i).Item("home_team_name")
            Dim vist_team_name As String = dtAll.Rows(i).Item("vist_team_name")


            Dim tmpHomeDt As Data.DataTable = GetNextRows(dtAll, i + 1, home_team_name, "", 200)
            Dim tmpVistDt As Data.DataTable = GetNextRows(dtAll, i + 1, "", vist_team_name, 200)

            Dim GameResultHome As GameResult = MakeGameResult(tmpHomeDt, home_team_name)
            Dim GameResultVist As GameResult = MakeGameResult(tmpVistDt, vist_team_name)
            'Public suu_home_harf_score As Decimal
            'Public suu_home_whole_score As Decimal

            'Public suu_vist_harf_score As Decimal
            'Public suu_vist_whole_score As Decimal
            dtAll.Rows(i).Item("homeharfScore") = GameResultHome.suu_home_harf_score
            dtAll.Rows(i).Item("homewholeScore") = GameResultHome.suu_home_whole_score

            dtAll.Rows(i).Item("vistharfScore") = GameResultVist.suu_vist_harf_score
            dtAll.Rows(i).Item("vistwholeScore") = GameResultVist.suu_vist_whole_score

            If GameResultHome.suu_home_harf_score - GameResultVist.suu_vist_harf_score > 1 Then
                dtAll.Rows(i).Item("预测半胜平负") = "胜"
            ElseIf GameResultHome.suu_home_harf_score - GameResultVist.suu_vist_harf_score < -1 Then
                dtAll.Rows(i).Item("预测半胜平负") = "负"
            Else
                dtAll.Rows(i).Item("预测半胜平负") = "平"
            End If

            If GameResultHome.suu_home_whole_score - GameResultVist.suu_vist_whole_score > 1 Then
                dtAll.Rows(i).Item("预测全胜平负") = "胜"
            ElseIf GameResultHome.suu_home_whole_score - GameResultVist.suu_vist_whole_score < -1 Then
                dtAll.Rows(i).Item("预测全胜平负") = "负"
            Else
                dtAll.Rows(i).Item("预测全胜平负") = "平"
            End If

            If dtAll.Rows(i).Item("半胜平负") = dtAll.Rows(i).Item("预测半胜平负") Then
                dtAll.Rows(i).Item("预测半结果") = "○"
                rightHalfSuu += 1
            Else
                dtAll.Rows(i).Item("预测半结果") = "☓"
            End If


            If dtAll.Rows(i).Item("全胜平负") = dtAll.Rows(i).Item("预测全胜平负") Then
                dtAll.Rows(i).Item("预测全结果") = "○"
                rightSuu += 1
            Else
                dtAll.Rows(i).Item("预测全结果") = "☓"

            End If


            If dtAll.Rows(i).Item("半胜平负") = dtAll.Rows(i).Item("预测半胜平负") AndAlso dtAll.Rows(i).Item("全胜平负") = dtAll.Rows(i).Item("预测全胜平负") Then
                rightZhuKeSuu += 1
            End If


            '预测半结果

            '预测半胜平负
        Next

        sb.AppendLine("上半：" & rightHalfSuu & "/" & dtAll.Rows.Count & "---" & (rightHalfSuu / dtAll.Rows.Count).ToString("##.###"))
        sb.AppendLine("下半：" & rightSuu & "/" & dtAll.Rows.Count & "---" & (rightSuu / dtAll.Rows.Count).ToString("##.###"))
        sb.AppendLine("上下：" & rightZhuKeSuu & "/" & dtAll.Rows.Count & "---" & (rightZhuKeSuu / dtAll.Rows.Count).ToString("##.###"))

        'lblKeisanResult.Text = "上半：" & rightHalfSuu & "/" & dtAll.Rows.Count & "---" & (rightHalfSuu / dtAll.Rows.Count).ToString("##.###")
        'lblKeisanResult.Text &= "下半" & rightSuu & "/" & dtAll.Rows.Count & "---" & (rightSuu / dtAll.Rows.Count).ToString("##.###")
        'lblKeisanResult.Text &= "上下：" & rightZhuKeSuu & "/" & dtAll.Rows.Count & "---" & (rightZhuKeSuu / dtAll.Rows.Count).ToString("##.###")



        'gvAll.DataSource = dtAll
        'gvAll.DataBind()
    End Sub

    Public Function GetNextRows(ByVal dt As Data.DataTable, ByVal startIdx As Integer, ByVal home_team_name As String, ByVal vist_team_name As String, ByVal megreCountSuu As Integer) As Data.DataTable



        Dim tmp As Data.DataTable = dt.Clone
        tmp.Clear()



        For i As Integer = startIdx To dt.Rows.Count - 1

            If home_team_name <> "" Then
                If home_team_name = dt.Rows(i).Item("home_team_name") Then
                    tmp.Rows.Add(dt.Rows(i).ItemArray)
                End If
            Else
                If vist_team_name = dt.Rows(i).Item("vist_team_name") Then
                    tmp.Rows.Add(dt.Rows(i).ItemArray)
                End If
            End If

            If tmp.Rows.Count = megreCountSuu Then
                Return tmp
            End If

        Next

        Return tmp

    End Function


End Class

Public Class GameResult
    Public harfScore As Decimal
    Public wholeScore As Decimal


    Public homeharfScore As Decimal
    Public homewholeScore As Decimal
    Public homeCnt As Integer = 0

    Public vistharfScore As Decimal
    Public vistwholeScore As Decimal
    Public vistCnt As Integer = 0

    Public suu_harf_score As Decimal
    Public suu_whole_score As Decimal

    Public suu_home_harf_score As Decimal
    Public suu_home_whole_score As Decimal

    Public suu_vist_harf_score As Decimal
    Public suu_vist_whole_score As Decimal


    Public str As String
    Public data As Data.DataTable
End Class
’PhotoZoom Pro 是一款无损图片放大工具