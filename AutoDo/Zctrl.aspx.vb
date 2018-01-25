Imports System.Data

Partial Class Zctrl
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PageParam As New PageParam(Page, Context, ViewState)

        If Not IsPostBack Then

            'WucLink1.user_cd = PageParam.user_id
            If ViewState("user_id") Is Nothing Then
                ViewState("user_id") = "lis6"
            End If
        End If

    End Sub

    'Public Sub BindEDP()

    '    'ddlEdp

    '    Dim msSql As New CMsSql()
    '    Dim sb As New StringBuilder
    '    With sb
    '        .AppendLine("SELECT *")
    '        .AppendLine("FROM [m_edp]")
    '        .AppendLine("WHERE")
    '    End With


    '    Dim dt As DataTable = msSql.ExecSelect(sb.ToString)
    '    If dt.Rows.Count > 0 Then
    '    Else
    '        C.SMsg(Page, "User does not exist~!")
    '    End If
    'End Sub

    ''' <summary>
    ''' 漢字　CHANGE　TO　ENGLISH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChToEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChToEng.Click

        'KEY
        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName
        Dim transleteForm As String = Me.WucEditor1.TEXT
        Dim tablesNameStr As String = C.GetTablesList(Me.tbxTableNames.Text)

        '通訳先Editor設定
        WucEditor2.EditType = EditorType.SqlType
        WucEditor2.EditorInitRun(Page)

        Dim arr() As String = C.GetKmItem(transleteForm)

        Dim myTableInfoClass As New TableInfoClass
        For i As Integer = 0 To arr.Length - 1
            If Translate.IsTranslateStr(arr(i)) Then
                arr(i) = myTableInfoClass.GetEnByJp(data_source, db_name, arr(i), tablesNameStr)
            End If
        Next

        Me.WucEditor2.TEXT = String.Concat(arr)

    End Sub

    ''' <summary>
    ''' 変数作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDim.Click

        'KEY
        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName
        Dim transleteForm As String = Me.WucEditor1.TEXT
        Dim tablesNameStr As String = C.GetTablesList(Me.tbxTableNames.Text)

        '通訳先Editor設定
        With WucEditor2
            .EditType = EditorType.VbscriptType
            .EditorInitRun(Page)
            .TEXT = CTranDim.TranDim(data_source, db_name, tablesNameStr, transleteForm)
        End With

        'Dim arr() As String = C.GetKmItem(Me.WucEditor1.TEXT)
        'Dim myTableInfoClass As New TableInfoClass
        'Dim rtv As New StringBuilder
        'For i As Integer = 0 To arr.Length - 1
        '    If Translate.IsTranslateStr(arr(i)) < 0 Then
        '        Dim dt As DataTable = myTableInfoClass.GetEnKMDATA(data_source, db_name, arr(i).Trim, tablesNameStr)
        '        rtv.AppendLine(CTranDim.GetDimParam(dt))
        '    End If
        'Next
        'Me.WucEditor2.TEXT = rtv.ToString

    End Sub


    ''' <summary>
    ''' Controlsのソース作成する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnControls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnControls.Click

        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName
        Dim txtL As String = Me.WucEditor1.TEXT
        Dim tablesNameStr As String = C.GetTablesList(Me.tbxTableNames.Text)

        With WucEditor2
            .EditType = EditorType.VbscriptType   
            .EditorInitRun(Page)
            .TEXT = CTranControls.CTranControls(data_source, db_name, tablesNameStr, txtL)
        End With



    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

    End Sub
End Class
