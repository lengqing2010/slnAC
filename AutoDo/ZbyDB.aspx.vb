Imports System.Data
Partial Class ZbyDB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim CDB As New CDB
            Dim dbServLst As Data.DataTable = CDB.GetDbServerList
            ucDbServLst.DataSource = dbServLst
            ucDbServLst.OnClick = "test"
        End If

        ''ddlEdp
        'Dim edpNo As String = Me.WucEdpDb1.EdpNo

        'Dim file_exp As String = Me.tbxTitle.Text

        'Try
        '    Dim MSSQL As New MSSQL(Me.WucEdpDb1.DbConnStr, 1)

        '    Dim sql As String = ""
        '    If Me.WucEditor1.GetSession.Trim = "" Then
        '        sql = Me.WucEditor1.TEXT
        '    Else
        '        sql = Me.WucEditor1.GetSession

        '    End If

        '    MSSQL.ExecuteNonQuery(sql)
        '    If MSSQL.Result Then
        '        MSSQL.CloseCommit()
        '        C.Msg(Page, "OK")
        '    Else
        '        MSSQL.CloseRollback()
        '        C.Msg(Page, MSSQL.errMsg)
        '    End If
        '    MSSQL.Close()
        'Catch ex As Exception
        '    C.Msg(Page, ex.Message)
        'End Try

    End Sub

    Public Sub test()

        Dim conn As String = Me.ucDbServLst.Value0
        Dim dt As New Data.DataTable
        Dim msg As String

        MSSQL.SEL(conn, "select name as text from sys.tables ", dt, msg)

        Dim CDB As New CDB

        Me.ucTableLst.DataSource = dt

    End Sub

End Class
