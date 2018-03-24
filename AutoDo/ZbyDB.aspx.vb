Imports System.Data
Partial Class ZbyDB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim CDB As New CDB

            Dim dbEdpLst As Data.DataTable = CDB.GetEdpList
            Dim dbServLst As Data.DataTable = CDB.GetDbServerList

            Me.ucEdpLst.DataSource = dbEdpLst

            ucDbServLst.DataSource = dbServLst
            ucDbServLst.OnClick = "DbServLstSentaku"



        End If

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            DbServLstSentaku()
        End If

    End Sub

    Public Sub DbServLstSentaku()

        Dim conn As String = Me.ucDbServLst.Value0
        Dim dt As New Data.DataTable
        Dim msg As String

        MSSQL.SEL(conn, "select name as text from sys.tables ", dt, msg)

        Dim CDB As New CDB

        Me.ucTableLst.DataSource = dt

    End Sub






    Public Function GetAcDbDt() As DataTable
        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0

        Dim sb As New StringBuilder
        With sb

            .AppendLine("SELECT a.NAME AS table_name, b.NAME AS columns_name, c.NAME AS columns_type, b.length AS columns_length")
            .AppendLine("	, CASE ")
            .AppendLine("		WHEN d.TABLE_NAME IS NULL THEN 0")
            .AppendLine("		ELSE 1")
            .AppendLine("	END AS pk")
            .AppendLine("FROM sysobjects a")
            .AppendLine("	INNER JOIN syscolumns b ON a.id = b.id")
            .AppendLine("	INNER JOIN systypes c ON b.xtype = c.xtype")
            .AppendLine("	LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE d")
            .AppendLine("	ON d.TABLE_NAME = a.NAME")
            .AppendLine("		AND d.COLUMN_NAME = b.NAME")
            .AppendLine("WHERE a.xtype = 'U'")
            .AppendLine("       AND a.NAME = '" & tblName & "'  ")
            .AppendLine("and c.NAME <> 'sysname'")

            .AppendLine("ORDER BY b.colorder")

        End With
        'sb.AppendLine("SELECT a.NAME   table_name,")
        'sb.AppendLine("       b.NAME   columns_name,")
        'sb.AppendLine("       c.NAME   columns_type,")
        'sb.AppendLine("       b.length columns_length,")

        'sb.AppendLine("       case when d.TABLE_NAME is null then 0")
        'sb.AppendLine("       else ")
        'sb.AppendLine("		1")
        'sb.AppendLine("	   end pk")

        'sb.AppendLine("FROM   sysobjects a,")
        'sb.AppendLine("       syscolumns b,")
        'sb.AppendLine("       systypes c,")
        'sb.AppendLine("       INFORMATION_SCHEMA.KEY_COLUMN_USAGE d")
        'sb.AppendLine("WHERE  a.id = b.id")
        'sb.AppendLine("       AND a.xtype = 'U'")
        'sb.AppendLine("       AND b.xtype = c.xtype")
        'sb.AppendLine("       AND a.NAME = '" & tblName & "'  ")
        'sb.AppendLine("       AND d.TABLE_NAME=a.NAME")
        'sb.AppendLine("       AND d.COLUMN_NAME=b.NAME")
        'sb.AppendLine("Order by b.colorder")


        Dim conn As String = Me.ucDbServLst.Value0
        Dim dt As New Data.DataTable
        Dim msg As String = ""
        MSSQL.SEL(conn, sb.ToString, dt, msg)

        Return dt

    End Function


    Protected Sub btnMkInsSql_Click(sender As Object, e As System.EventArgs) Handles btnMkInsSql.Click

          Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()


        Dim rtv As String

        Dim actionType As String



        actionType = "insert"
        rtv &= vbNewLine & AutoMkCode.GetBcFuncString(dt, dbName, tblName, actionType)

        Dim paraTP As AutoMkCode.ParamType = GetParamType()
        Dim noteKbn As Boolean = Me.cbNote.Checked
        rtv &= vbNewLine & AutoMkCode.GetDaFuncString(dt, dbName, tblName, actionType, noteKbn, paraTP)

        WucEditor1.TEXT = rtv



    End Sub


    Public Function GetParamType() As AutoMkCode.ParamType
        Dim tmpParamType As AutoMkCode.ParamType
        If Me.ddlParamType.SelectedValue = "SqlParam" Then
            tmpParamType = AutoMkCode.ParamType.SqlParam
        ElseIf Me.ddlParamType.SelectedValue = "NoParam" Then
            tmpParamType = AutoMkCode.ParamType.NoParam
        End If
        Return tmpParamType
    End Function

    ''' <summary>
    ''' 検索
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMkSelSql_Click(sender As Object, e As System.EventArgs) Handles btnMkSelSql.Click
        'Editor設定
        With WucEditor1
            .EditType = EditorType.VbscriptType
            .EditorInitRun(Page)
        End With
        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()


        Dim rtv As String '= AutoMkCode.GetDimString(dt, dbName, tblName)

        Dim actionType As String

        Dim paraTP As AutoMkCode.ParamType = GetParamType()
        Dim noteKbn As Boolean = Me.cbNote.Checked


        actionType = "select"
        rtv &= vbNewLine & AutoMkCode.GetBcFuncString(dt, dbName, tblName, actionType)

        rtv &= vbNewLine & AutoMkCode.GetDaFuncString(dt, dbName, tblName, actionType, noteKbn, paraTP)

        WucEditor1.TEXT = rtv
    End Sub

    Protected Sub btnMkUpdSql_Click(sender As Object, e As System.EventArgs) Handles btnMkUpdSql.Click
        'Editor設定
        With WucEditor1
            .EditType = EditorType.VbscriptType
            .EditorInitRun(Page)
        End With
        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim rtv As String '= AutoMkCode.GetDimString(dt, dbName, tblName)
        Dim actionType As String
        actionType = "update"
        rtv &= vbNewLine & AutoMkCode.GetBcFuncString(dt, dbName, tblName, actionType)

        Dim paraTP As AutoMkCode.ParamType = GetParamType()
        Dim noteKbn As Boolean = Me.cbNote.Checked
        rtv &= vbNewLine & AutoMkCode.GetDaFuncString(dt, dbName, tblName, actionType, noteKbn, paraTP)


        WucEditor1.TEXT = rtv
    End Sub


    Protected Sub btnMkSelDim_Click(sender As Object, e As System.EventArgs) Handles btnMkSelDim.Click
        'Editor設定
        With WucEditor1
            .EditType = EditorType.VbscriptType
            .EditorInitRun(Page)
        End With

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim rtv As String = AutoMkCode.GetDimString(dt, dbName, tblName)

        WucEditor1.TEXT = rtv
    End Sub

    Protected Sub btnMkBulkcopy_Click(sender As Object, e As System.EventArgs) Handles btnMkBulkcopy.Click


        'Editor設定
        With WucEditor1
            .EditType = EditorType.VbscriptType
            .EditorInitRun(Page)
        End With

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim rtv As String = AutoMkCode.GetVBNETMakeBlukcopy(dt, dbName, tblName)

        WucEditor1.TEXT = rtv
    End Sub


    Protected Sub btnSelectSql_Click(sender As Object, e As System.EventArgs) Handles btnSelectSql.Click

        'Editor設定
        With WucEditor1
            .EditType = EditorType.SqlType
            .EditorInitRun(Page)
        End With

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim AutoCodeDbClass As New AutoCodeDbClass(dbName, tblName)
        Dim AutoCodeSqlServer As New AutoCodeSqlServer
        Dim rtv As String = AutoCodeSqlServer.GetSelect(dt, AutoCodeDbClass.active_database_dt, dbName, tblName)
        WucEditor1.TEXT = rtv

    End Sub

    Protected Sub btnInsSql_Click(sender As Object, e As System.EventArgs) Handles btnInsSql.Click


        'Editor設定
        With WucEditor1
            .EditType = EditorType.SqlType
            .EditorInitRun(Page)
        End With

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim AutoCodeDbClass As New AutoCodeDbClass(dbName, tblName)
        Dim AutoCodeSqlServer As New AutoCodeSqlServer
        Dim rtv As String = AutoCodeSqlServer.GetINSERT(dt, AutoCodeDbClass.active_database_dt, dbName, tblName)
        WucEditor1.TEXT = rtv

    End Sub

    Protected Sub btnUpdSql_Click(sender As Object, e As System.EventArgs) Handles btnUpdSql.Click
        'Editor設定
        With WucEditor1
            .EditType = EditorType.SqlType
            .EditorInitRun(Page)
        End With

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim AutoCodeDbClass As New AutoCodeDbClass(dbName, tblName)
        Dim AutoCodeSqlServer As New AutoCodeSqlServer
        Dim rtv As String = AutoCodeSqlServer.GetUpdate(dt, AutoCodeDbClass.active_database_dt, dbName, tblName)
        WucEditor1.TEXT = rtv
    End Sub



    ''' <summary>
    ''' Asp.net Page 做成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMKPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMKPage.Click

        ' "P_TableEditor_m_edp.aspx"

        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0

        ' Dim directoryName As String = "F:\ILIKEMAKE2017\AutoMakeCode\AutoCode\slnAC\AutoDo\"
        Dim directoryName As String = "E:\案件\AutoMakeCode\AutoCode\slnAC\AutoDo\"

        Dim path As String = directoryName & "P_TableEditor_" & tblName & ".aspx"



        Dim AutoCodeDbClass As New AutoCodeDbClass(dbName, tblName)
        Dim AutoCodeSqlServer As New AutoCodeSqlServer

        Dim acTableData As DataTable = GetAcDbDt()
        acTableData.TableName = tblName
        Dim mTableData As DataTable = AutoCodeDbClass.active_database_dt

        Dim CAutoMKPage As New CAutoMKPage(path, tblName)
        CAutoMKPage.MakeAspxPage(acTableData, mTableData, AutoCodeDbClass)




    End Sub

    Protected Sub btnAspControls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAspControls.Click
        'Editor設定
        With WucEditor1
            .EditType = EditorType.VbscriptType
            .EditorInitRun(Page)
        End With

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim AutoCodeDbClass As New AutoCodeDbClass(dbName, tblName)
        Dim AutoCodeSqlServer As New AutoCodeSqlServer

        Dim rtv As String = AutoMkCode.GetNetControls(dt, AutoCodeDbClass.active_database_dt, dbName, tblName, True)

        WucEditor1.TEXT = rtv

    End Sub
End Class
