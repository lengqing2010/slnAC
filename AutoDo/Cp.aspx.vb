
Partial Class Cp
    Inherits System.Web.UI.Page




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
            .AppendLine("  FROM [cpm_cp] a")
            .AppendLine("  LEFT JOIN cpm_pl b")
            .AppendLine("  on a.[league_name] = b.[league_name]")
            .AppendLine("  AND a.[game_idx] = b.[game_idx]")
            .AppendLine("  ")
            .AppendLine("  where a.[league_name]=N'" & league_name & "'")
            If ZKQ = "全" Then
                .AppendLine("  and ([home_team_name]=N'" & team_name & "' or [vist_team_name]=N'" & team_name & "')")
            ElseIf ZKQ = "主" Then
                .AppendLine("  and ([home_team_name]=N'" & team_name & "')")
            Else
                .AppendLine("  and ([vist_team_name]=N'" & team_name & "')")
            End If
            .AppendLine("  order by game_date desc")
        End With
        'ByVal league_name As String, ByVal team_name As String
        Dim DbResult1 As DbResult = DefaultDB.SelIt(sb.ToString)

        If DbResult1.Message <> "" Then
            MsgBox(DbResult1.Message)
            Throw New Exception
        Else
            Return DbResult1.Data
        End If



    End Function


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

      

    End Sub

    Protected Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSel.Click


        Dim teamName As String = tbxTeamName.Text.Trim
        Dim zkq As String = Me.ddlZKQ.Items(Me.ddlZKQ.SelectedIndex).Text
        Dim top As String = Me.ddlTop.Items(Me.ddlTop.SelectedIndex).Text
        Dim str As String = GetData(teamName, zkq, top, gvHome)
        lblHalf.Text = str

    End Sub


    Public Function GetData(ByVal teamName As String, ByVal zkq As String, ByVal top As String, ByVal gv As GridView) As String


        Dim league_name As String = Me.ddlLeague_name.Items(Me.ddlLeague_name.SelectedIndex).Text



        Dim dt As Data.DataTable = GetTeamInfo(league_name, teamName, top, zkq)

        gv.DataSource = dt
        gv.DataBind()

        Dim halfF As Decimal = 0
        Dim wholeF As Decimal = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("home_team_name") = teamName Then
                halfF += CDec(dt.Rows(i).Item("半能力"))
                wholeF += CDec(dt.Rows(i).Item("全能力"))
            Else
                halfF += CDec(dt.Rows(i).Item("客半能力"))
                wholeF += CDec(dt.Rows(i).Item("客全能力"))
            End If
        Next
        Dim str As String
        str = " 半能力:"
        str = str & (halfF / dt.Rows.Count).ToString("0#.###")
        str = str & " 全能力:"
        str = str & (wholeF / dt.Rows.Count).ToString("0#.###")

        Return str

        'lblWhole.Text = (wholeF / dt.Rows.Count).ToString("0#.###")

    End Function

    Protected Sub btnSelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelAll.Click

        'Dim teamName As String = tbxTeamName.Text.Trim
        Dim zkq As String = Me.ddlZKQ.Items(Me.ddlZKQ.SelectedIndex).Text
        Dim top As String = Me.ddlTop.Items(Me.ddlTop.SelectedIndex).Text

        Dim str1 As String = GetData(tbxTeamName.Text.Trim, "主", top, gvHome)
        lblHalf.Text = str1

        Dim str2 As String = GetData(tbxVistName.Text.Trim, "客", top, gvVist)
        lblWhole.Text = str2



    End Sub
End Class
