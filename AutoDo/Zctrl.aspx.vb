Imports System.Data

Partial Class Zctrl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            ViewState("user_cd") = Context.Items("user_cd")

            If ViewState("user_cd") Is Nothing Then
                ViewState("user_cd") = "lis6"
            End If

            WucLink1.user_cd = ViewState("user_cd").ToString

        End If

    End Sub

    Public Sub BindEDP()

        'ddlEdp

        Dim msSql As New CMsSql()
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT *")
            .AppendLine("FROM [m_edp]")
            .AppendLine("WHERE")
        End With


        Dim dt As DataTable = msSql.ExecSelect(sb.ToString)
        If dt.Rows.Count > 0 Then
        Else
            C.SMsg(Page, "User does not exist~!")
        End If
    End Sub

    Protected Sub btnChToEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChToEng.Click

        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName
        Dim txtL As String = Me.WucEditor1.TEXT

        Dim tablesNameStr As String = C.GetTablesList(Me.tbxTableNames.Text)

        Me.WucEditor2.TEXT = Trans(data_source, db_name, txtL, tablesNameStr)




    End Sub

    Public Function Trans(ByVal data_source As String, ByVal db_name As String, ByVal transL As String, ByVal contactTable As String) As String

        Dim sql As String = transL

        'Dim arr() As String = C.GetArrBySplit(sql, vbLf)
        'arr = C.GetArrBySplit(arr, vbCr)
        'arr = C.GetArrBySplit(arr, vbTab)
        'arr = C.GetArrBySplit(arr, ",")
        'arr = C.GetArrBySplit(arr, "[")
        'arr = C.GetArrBySplit(arr, "]")
        'arr = C.GetArrBySplit(arr, "!=")
        'arr = C.GetArrBySplit(arr, "<>")
        'arr = C.GetArrBySplit(arr, "=")
        'arr = C.GetArrBySplit(arr, "(")
        'arr = C.GetArrBySplit(arr, ")")
        'arr = C.GetArrBySplit(arr, "'")
        'arr = C.GetArrBySplit(arr, ".")
        'arr = C.GetArrBySplit(arr, " ")
        'arr = C.GetArrBySplit(arr, " ") 'uncode

        Dim arr() As String = C.GetKmItem(sql)

        Dim myTableInfoClass As New TableInfoClass

        Dim jp As String

        WucEditor2.EditType = "sql"
        WucEditor2.EditorInitRun(Page)

        For i As Integer = 0 To arr.Length - 1
            If Not IsNumeric(arr(i)) AndAlso arr(i) <> vbLf AndAlso arr(i).Trim <> "" AndAlso (",'()").IndexOf(arr(i).Trim) < 0 AndAlso C.sqlGuanjianzi.IndexOf(arr(i).Trim.ToUpper) < 0 Then
                Dim myMSSQL As New MSSQL
                jp = arr(i).Trim
                Dim rtv As String = myTableInfoClass.GetEnByJp(myMSSQL, data_source, db_name, jp, contactTable)

                If rtv <> "" Then
                    arr(i) = rtv
                End If
                myMSSQL.Close()
            End If

        Next

        Return String.Concat(arr)

    End Function

    Protected Sub btnDim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDim.Click

        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName
        Dim txtL As String = Me.WucEditor1.TEXT
        Dim arr() As String = C.GetKmItem(txtL)
        Dim tablesNameStr As String = C.GetTablesList(Me.tbxTableNames.Text)

        Dim myTableInfoClass As New TableInfoClass

        WucEditor2.EditType = "vbscript"
        WucEditor2.EditorInitRun(Page)

        For i As Integer = 0 To arr.Length - 1
            If Not IsNumeric(arr(i)) AndAlso arr(i) <> vbLf AndAlso arr(i).Trim <> "" AndAlso (",'()").IndexOf(arr(i).Trim) < 0 AndAlso C.sqlGuanjianzi.IndexOf(arr(i).Trim.ToUpper) < 0 Then
                Dim myMSSQL As New MSSQL
                Dim dt As DataTable = myTableInfoClass.GetEnKMDATA(myMSSQL, data_source, db_name, arr(i).Trim, tablesNameStr)
                Me.WucEditor2.TEXT = C.GetDimParam(dt)
                myMSSQL.Close()
            End If

        Next

    End Sub



    Protected Sub btnControls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnControls.Click
        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName
        Dim txtL As String = Me.WucEditor1.TEXT
        Dim arr() As String = C.GetKmItem(txtL)
        Dim tablesNameStr As String = C.GetTablesList(Me.tbxTableNames.Text)

        Dim myTableInfoClass As New TableInfoClass

        WucEditor2.EditType = "vbscript"
        WucEditor2.EditorInitRun(Page)

        For i As Integer = 0 To arr.Length - 1
            If Not IsNumeric(arr(i)) AndAlso arr(i) <> vbLf AndAlso arr(i).Trim <> "" AndAlso (",'()").IndexOf(arr(i).Trim) < 0 AndAlso C.sqlGuanjianzi.IndexOf(arr(i).Trim.ToUpper) < 0 Then
                Dim myMSSQL As New MSSQL
                Dim dt As DataTable = myTableInfoClass.GetEnKMDATA(myMSSQL, data_source, db_name, arr(i).Trim, tablesNameStr)
                Me.WucEditor2.TEXT = C.GetNetControls(dt)
                myMSSQL.Close()
            End If

        Next
    End Sub


End Class
