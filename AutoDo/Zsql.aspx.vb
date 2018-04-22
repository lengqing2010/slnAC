
Partial Class Zsql
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PageParam As New PageParam(Page, Context, ViewState)

        lblMsg.Text = ""

        If Not IsPostBack Then

            'WucLink1.user_cd = PageParam.user_id
            If ViewState("user_id") Is Nothing Then
                ViewState("user_id") = "lis6"
            End If
            BindFileList()

        End If




    End Sub


    Public Sub BindFileList()

        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim sb As New StringBuilder

        With sb
            .AppendLine("SELECT file_exp,file_exp ")
            .AppendLine("FROM [auto_code].[dbo].[m_siryoiu] WHERE")
            .AppendLine("edp_no = '" & edpNo & "'")
        End With
        C.BindListBox(Me.lstFile, sb.ToString)

        Try
            If Me.tbxTitle.Text <> "" Then
                lstFile.SelectedValue = Me.tbxTitle.Text
            End If
        Catch ex As Exception

        End Try


    End Sub

    Sub CSave()

        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim file_exp As String = Me.tbxTitle.Text
        Dim txt As String = Me.WucEditor1.TEXT
        Dim data_source As String = Me.WucEdpDb1.DbServerName

        Dim path As String = HttpRuntime.AppDomainAppPath & "DATA\" & Me.tbxTitle.Text & ".sql"

        C.SaveFile(path, txt)
        txt = txt.Replace("'", "''")
        Dim msg As String = C.CSaveSIryoiu(edpNo, file_exp, txt, data_source, ViewState("user_id").ToString, path)

        If msg <> "" Then
            C.Msg(Page, msg)
        End If


    End Sub



    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        CSave()


    End Sub



    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        BindFileList()
    End Sub

    Protected Sub lstFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFile.SelectedIndexChanged
        CSave()

        Me.tbxTitle.Text = Me.lstFile.SelectedValue

        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim file_exp As String = Me.tbxTitle.Text

        Dim sb As New StringBuilder

        With sb
            .AppendLine("SELECT txt ")
            .AppendLine("FROM [auto_code].[dbo].[m_siryoiu] WHERE")
            .AppendLine("edp_no = '" & edpNo & "'")
            .AppendLine("AND file_exp = '" & file_exp & "'")
        End With

        Dim MSSQL As New MSSQL
        Dim dt As Data.DataTable = MSSQL.ExecSelect(sb.ToString)

        If dt.Rows.Count > 0 Then
            Me.WucEditor1.TEXT = dt.Rows(0).Item("txt").ToString
        End If

    End Sub

    Protected Sub btnSQLRUN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSQLRUN.Click
        CSave()
        BindFileList()
        Me.tbxTitle.Text = Me.lstFile.SelectedValue



        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim file_exp As String = Me.tbxTitle.Text

        Try
            Dim MSSQL As New MSSQL(Me.WucEdpDb1.DbConnStr, 1)

            Dim sql As String = ""
            If Me.WucEditor1.GetSession.Trim = "" Then
                sql = Me.WucEditor1.TEXT
            Else
                sql = Me.WucEditor1.GetSession

            End If

            MSSQL.ExecuteNonQuery(sql)
            If MSSQL.Result Then
                MSSQL.CloseCommit()
                C.Msg(Page, "OK")
            Else
                MSSQL.CloseRollback()
                C.Msg(Page, MSSQL.errMsg)
            End If
            MSSQL.Close()
        Catch ex As Exception
            C.Msg(Page, ex.Message)
        End Try



    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Context.Items("EdpNo") = Me.WucEdpDb1.EdpNo
        Context.Items("DbConnStr") = Me.WucEdpDb1.DbConnStr
        Context.Items("SQL") = Me.WucEditor1.TEXT
        Context.Items("FILENAME") = Me.tbxTitle.Text
        Server.Transfer("SqlPanel.aspx")
        Exit Sub


        'CSave()
        'BindFileList()
        'Me.tbxTitle.Text = Me.lstFile.SelectedValue

        ''ddlEdp
        ''Dim edpNo As String = Me.WucEdpDb1.EdpNo
        'Dim conn As String = Me.WucEdpDb1.DbConnStr

        ''Dim file_exp As String = Me.tbxTitle.Text

        ''Dim MSSQL As New MSSQL(Me.WucEdpDb1.DbConnStr, 30)
        ''Dim dt As Data.DataTable = MSSQL.ExecSelect(Me.WucEditor1.TEXT)

        ''If dt.Rows.Count > 0 Then
        ''    gv.datasource = dt
        ''    gv.DataBind()
        ''End If

        'Dim dt As Data.DataTable = Nothing
        'Dim msg As String = ""

        'Dim sql As String = ""
        'If Me.WucEditor1.GetSession.Trim = "" Then
        '    sql = Me.WucEditor1.TEXT
        'Else
        '    sql = Me.WucEditor1.GetSession

        'End If

        'If MSSQL.SEL(conn, sql, dt, msg) Then
        '    lblMsg.Text = dt.Rows.Count & "件"
        '    gv.DataSource = dt
        '    gv.DataBind()

        'Else
        '    lblMsg.Text = msg
        '    gv.DataSource = Nothing
        '    gv.DataBind()
        'End If



    End Sub



    Protected Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
        If Me.tbxTitle.Text = "" Then
            C.Msg(Page, "Please input TITLE")

            Exit Sub
        End If
        Me.WucEditor1.TEXT = ""
        CSave()
        BindFileList()
    End Sub

    Protected Sub btnDel_Click(sender As Object, e As System.EventArgs) Handles btnDel.Click

        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim file_exp As String = Me.tbxTitle.Text
        Dim msg As String = C.CDelSIryoiu(edpNo, file_exp)
        If msg <> "" Then
            C.Msg(Page, msg)
        End If
        BindFileList()
    End Sub
End Class
