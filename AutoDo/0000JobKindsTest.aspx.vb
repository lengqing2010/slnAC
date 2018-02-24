Imports System.Data
Partial Class userctrl_0000JobKindsTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Me.tbxUserName.Text = C.Client(Page).login_user

            'Job Edp
            Dim dt As Data.DataTable = GetJopData()

            Dim jobEdp As String = Right("0000000000000000000" & (dt.Rows.Count + 1).ToString, 20)
            'Me.tbxJobEdp.Text = jobEdp

            '
            'Me.tbxJobSerPath.Text = "\\10.160.192.25\大連情報システム部（itis共有）\開発資料\★_事業会社システムグループ\流通チーム\"
            'Me.tbxJobClientPath.Text = "\\ot5600\案件\"
            'Me.tbxJobBackupPath.Text = "\\ot2414\案件\★_事業会社システムグループ\流通チーム\"

            '
            MsInit()

            ddlJob.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1

                ddlJob.Items.Add(dt.Rows(i).Item("job_edp"))

                Dim JobSerPath As String = dt.Rows(i).Item("main_job_path_server").ToString.Trim
                Dim JobClientPath As String = dt.Rows(i).Item("main_job_path_client").ToString.Trim
                Dim JobBackupPath As String = dt.Rows(i).Item("main_job_path_backup").ToString.Trim

                ddlJob.Items(ddlJob.Items.Count - 1).Value = JobSerPath & "," & JobClientPath & "," & JobBackupPath

            Next

            ServerDirectoryInit()

        End If

    End Sub


    Public Function ServerDirectoryInit() As DataTable

        Dim key As String = ddlJob.Items(ddlJob.SelectedIndex).Value

        Dim JobSerPath As String = key.Split(",")(0).ToString.Trim
        Dim JobClientPath As String = key.Split(",")(1).ToString.Trim
        Dim JobBackupPath As String = key.Split(",")(2).ToString.Trim



        Dim dtDirectories As New Data.DataTable
        dtDirectories.Columns.Add("JobSerPath")
        dtDirectories.Columns.Add("JobClientPath")
        dtDirectories.Columns.Add("JobBackupPath")
        dtDirectories.Columns.Add("ser_name")
        dtDirectories.Columns.Add("visibility")
        dtDirectories.Columns.Add("upd_time")




        Dim dtClient As New Data.DataTable
        dtClient.Columns.Add("path")
        dtClient.Columns.Add("name")
        Dim mDirInfoClient As New System.IO.DirectoryInfo(JobClientPath)
        For Each mDir In mDirInfoClient.GetDirectories
            Dim dr As Data.DataRow
            dr = dtClient.NewRow
            Dim path As String = mDir.FullName
            Dim lastName As String = mDir.Name
            dr.Item("path") = path
            dr.Item("name") = lastName
            dtClient.Rows.Add(dr)
        Next


        Dim dtEdp As New Data.DataTable
        dtEdp.Columns.Add("edp_no")
        dtEdp.Columns.Add("edp_mei")
        dtEdp.Columns.Add("edp_exp")

        Dim mDirInfo As New System.IO.DirectoryInfo(JobSerPath)
        For Each mDir In mDirInfo.GetDirectories
            Dim dr As Data.DataRow
            dr = dtDirectories.NewRow

            Dim path As String = mDir.FullName
            Dim lastName As String = mDir.Name

            dr.Item("JobSerPath") = path
            dr.Item("ser_name") = lastName
            dr.Item("upd_time") = mDir.LastAccessTime

            Dim drs() As DataRow
            drs = dtClient.Select("name='" & lastName & "'")
            If drs.Length > 0 Then
                dr.Item("JobClientPath") = drs(0).Item("path")
                dr.Item("visibility") = "inherit"
            Else
                dr.Item("visibility") = "hidden"
            End If

            dtDirectories.Rows.Add(dr)


        Next




        Dim dtDirectoriesView As DataView = dtDirectories.DefaultView
        dtDirectoriesView.Sort = "upd_time desc"
        dtDirectories = dtDirectoriesView.ToTable

        For i As Integer = 0 To dtDirectories.Rows.Count - 1
            Dim lastName As String = dtDirectories.Rows(i).Item("ser_name")
            Dim arrEdpNm() As String = lastName.Split("_")
            If arrEdpNm.Length > 1 AndAlso arrEdpNm(0).Length < 20 Then
                Dim drEdp As DataRow = dtEdp.NewRow
                drEdp.Item("edp_no") = arrEdpNm(0)
                drEdp.Item("edp_mei") = arrEdpNm(1)
                drEdp.Item("edp_exp") = lastName
                dtEdp.Rows.Add(drEdp)
            End If
        Next
        ddlEdp.Items.Clear()
        For i As Integer = 0 To dtEdp.Rows.Count - 1
            ddlEdp.Items.Add(dtEdp.Rows(i).Item("edp_no") & "" & " " & "" & dtEdp.Rows(i).Item("edp_mei"))
            ddlEdp.Items(ddlEdp.Items.Count - 1).Value = dtEdp.Rows(i).Item("edp_no")
        Next


        gvSerPaths.DataSource = dtDirectories
        gvSerPaths.DataBind()

    End Function

    Function GetIsAnbled() As String

    End Function


    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsUpd_Click(sender As Object, e As System.EventArgs) Handles btnInsUpd.Click

        C.CSaveJobKinds(Me.tbxUserName.Text _
                        , Me.tbxJobEdp.Text _
                        , Me.tbxJobSerPath.Text _
                        , Me.tbxJobClientPath.Text _
                        , Me.tbxJobBackupPath.Text)

        MsInit()

    End Sub

    Public Sub MsInit()

        Dim dt As Data.DataTable = GetJopData()

        For i As Integer = 0 To dt.Rows.Count - 1
            For j As Integer = 0 To dt.Columns.Count - 1
                dt.Rows(i).Item(j) = dt.Rows(i).Item(j).ToString.Replace("\", "|")
            Next
        Next

        Me.GvMs.DataSource = dt
        Me.GvMs.DataBind()

    End Sub

    Protected Sub btnSel_Click(sender As Object, e As System.EventArgs) Handles btnSel.Click
        MsInit()
    End Sub

    Public Function SetPathValue(ByVal v As String) As String
        Return v.Replace("|", "\")
    End Function

    Protected Sub btnDel_Click(sender As Object, e As System.EventArgs) Handles btnDel.Click

        C.CDelJobKinds(Me.tbxUserName.Text _
                , Me.tbxJobEdp.Text)

        MsInit()

    End Sub

    Public Function GetJopData() As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT * ")
            .AppendLine("FROM [m_job_kinds] WHERE")
            .AppendLine("user_id = '" & Me.tbxUserName.Text & "'")
        End With
        Dim msSql As New CMsSql()
        Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)

        Return dt

    End Function

    Protected Sub ddlJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlJob.SelectedIndexChanged
        ServerDirectoryInit()
    End Sub
End Class
