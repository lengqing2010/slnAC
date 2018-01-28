
Partial Class SqlPanel
    Inherits System.Web.UI.Page

    Protected Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSel.Click
      
        Sel()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Context.Items("EdpNo") = Me.WucEdpDb1.EdpNo
        'Context.Items("DbConnStr") = Me.WucEdpDb1.DbConnStr
        'Context.Items("SQL") = Me.WucEditor1.TEXT


    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            Me.WucEdpDb1.EdpNo = Context.Items("EdpNo")
            Me.WucEdpDb1.DbConnStr = Context.Items("DbConnStr")
            Me.WucEditor1.TEXT = Context.Items("SQL")
            Sel()
        End If

    End Sub

    Sub Sel()
        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim conn As String = Me.WucEdpDb1.DbConnStr

        'Dim file_exp As String = Me.tbxTitle.Text

        'Dim MSSQL As New MSSQL(Me.WucEdpDb1.DbConnStr, 30)
        'Dim dt As Data.DataTable = MSSQL.ExecSelect(Me.WucEditor1.TEXT)

        'If dt.Rows.Count > 0 Then
        '    gv.datasource = dt
        '    gv.DataBind()
        'End If

        Dim dt As Data.DataTable
        Dim msg As String

        Dim sql As String = ""
        If Me.WucEditor1.GetSession.Trim = "" Then
            sql = Me.WucEditor1.TEXT
        Else
            sql = Me.WucEditor1.GetSession

        End If

        If MSSQL.SEL(conn, sql, dt, msg) Then
            lblMsg.Text = dt.Rows.Count & "件"
            MS.DataSource = dt
            MS.DataBind()

            Dim TableInfoClass As New TableInfoClass
            For i As Integer = 0 To MS.HeaderRow.Cells.Count - 1

                Dim dtKM As Data.DataTable = TableInfoClass.GetEnKMDATA(COMMON.Init.connCom, "auto_code", MS.HeaderRow.Cells(i).Text, "")

                If dtKM.Rows.Count > 0 Then
                    MS.HeaderRow.Cells(i).Text &= "<br>" & dtKM.Rows(0).Item("item_jp")
                End If

            Next

            'dt.Rows.Clear()
            'gvTitle.DataSource = dt
            'gvTitle.DataBind()

        Else
            lblMsg.Text = msg
            MS.DataSource = Nothing
            MS.DataBind()

        End If
    End Sub
End Class
