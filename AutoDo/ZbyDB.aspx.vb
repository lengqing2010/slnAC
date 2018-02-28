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
        sb.AppendLine("SELECT a.NAME   table_name,")
        sb.AppendLine("       b.NAME   columns_name,")
        sb.AppendLine("       c.NAME   columns_type,")
        sb.AppendLine("       b.length columns_length")
        sb.AppendLine("FROM   sysobjects a,")
        sb.AppendLine("       syscolumns b,")
        sb.AppendLine("       systypes c")
        sb.AppendLine("WHERE  a.id = b.id")
        sb.AppendLine("       AND a.xtype = 'U'")
        sb.AppendLine("       AND b.xtype = c.xtype")
        sb.AppendLine("       AND a.NAME = '" & tblName & "'  ")
        sb.AppendLine("Order by b.colorder")


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

        rtv &= vbNewLine & AutoMkCode.GetDaFuncString(dt, dbName, tblName, actionType)

        WucEditor1.TEXT = rtv



    End Sub




    Protected Sub btnMkSelSql_Click(sender As Object, e As System.EventArgs) Handles btnMkSelSql.Click

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()


        Dim rtv As String '= AutoMkCode.GetDimString(dt, dbName, tblName)

        Dim actionType As String

        actionType = "select"
        rtv &= vbNewLine & AutoMkCode.GetBcFuncString(dt, dbName, tblName, actionType)

        rtv &= vbNewLine & AutoMkCode.GetDaFuncString(dt, dbName, tblName, actionType)


        WucEditor1.TEXT = rtv
    End Sub

    Protected Sub btnMkUpdSql_Click(sender As Object, e As System.EventArgs) Handles btnMkUpdSql.Click

        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim rtv As String '= AutoMkCode.GetDimString(dt, dbName, tblName)
        Dim actionType As String
        actionType = "update"
        rtv &= vbNewLine & AutoMkCode.GetBcFuncString(dt, dbName, tblName, actionType)

        rtv &= vbNewLine & AutoMkCode.GetDaFuncString(dt, dbName, tblName, actionType)


        WucEditor1.TEXT = rtv
    End Sub


    Protected Sub btnMkSelDim_Click(sender As Object, e As System.EventArgs) Handles btnMkSelDim.Click
        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim rtv As String = AutoMkCode.GetDimString(dt, dbName, tblName)

        WucEditor1.TEXT = rtv
    End Sub

    Protected Sub btnMkBulkcopy_Click(sender As Object, e As System.EventArgs) Handles btnMkBulkcopy.Click
        Dim dbSerName As String = Me.ucDbServLst.Text0.Split(":")(0)
        Dim dbName As String = Me.ucDbServLst.Text0.Split(":")(1)
        Dim tblName As String = Me.ucTableLst.Text0
        Dim dt As DataTable = GetAcDbDt()

        Dim rtv As String = AutoMkCode.GetVBNETMakeBlukcopy(dt, dbName, tblName)

        WucEditor1.TEXT = rtv
    End Sub


End Class
