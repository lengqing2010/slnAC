Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports Common
Public Class MSSQL

    '获得数据库的连接字符串    
    Public strConnection As String '= "Data Source=10.160.200.39; Initial Catalog=LIS_DB;Persist Security Info=True;User ID=sa;Password=lixil@2014"
    '设置连接    
    Public conn As SqlConnection '= New SqlConnection(strConnection)
    '定义cmd命令    
    Public cmd As New SqlCommand

    Public trans As System.Data.SqlClient.SqlTransaction

    Public Result As Boolean = True

    Public errMsg As String




    Public Shared Function SEL(ByVal connStr As String, ByVal sql As String, ByRef dt As DataTable, ByRef msg As String) As Boolean

        Try
            If connStr = "" Then
                connStr = COMMON.Init.connCom
            End If

            Dim conn As New SqlConnection(connStr)
            Dim cmd As New SqlCommand

            conn.Open()
            cmd.Connection = conn                '设置连接,全局变量    
            cmd.CommandTimeout = 30

            Dim sqlAdapter As SqlDataAdapter
            Dim ds As New DataSet
            '还是给cmd赋值    
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            sqlAdapter = New SqlDataAdapter(cmd)  '实例化adapter    

            sqlAdapter.Fill(ds)           '用adapter将dataSet填充     
            dt = ds.Tables(0)             'datatable为dataSet的第一个表    

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            conn.Close()                    '关闭连接    
            conn = Nothing                  '不指向原对象

            Return True

        Catch ex As Exception
            msg = ex.Message
            Return False

        End Try

    End Function

    Public Shared Function RUN(ByVal connStr As String, ByVal sql As String, ByRef msg As String) As Boolean

        If connStr = "" Then
            connStr = COMMON.Init.connCom
        End If

        Dim conn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        Dim trans As System.Data.SqlClient.SqlTransaction

        Try
            conn = New SqlConnection(connStr)
            conn.Open()
            trans = conn.BeginTransaction()

            cmd.Connection = conn                '设置连接,全局变量    
            cmd.CommandTimeout = 30
            cmd.Transaction = trans

            cmd.CommandType = CommandType.Text             '设置一个值,解释cmdText    
            cmd.CommandText = sql            '设置查询的语句  
            cmd.ExecuteNonQuery()     '执行增删改操作    

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            conn.Close()                    '关闭连接    
            conn = Nothing                  '不指向原对象

            Return True

        Catch ex As Exception

            msg = ex.Message

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
                conn.Close()                    '关闭连接    
                conn = Nothing                  '不指向原对象    
            End If

            Return False

        End Try




    End Function



    Sub New(Optional ByVal connStr As String = "", Optional ByVal CommandTimeout As Integer = 10000000)
        If connStr = "" Then
            connStr = COMMON.Init.connCom
        End If
        conn = New SqlConnection(connStr)
        conn.Open()
        trans = conn.BeginTransaction()

        cmd.Connection = conn                '设置连接,全局变量    
        cmd.CommandTimeout = CommandTimeout
        cmd.Transaction = trans
    End Sub



    ''' <summary>    
    ''' 执行查询的操作,(无参)    
    ''' </summary>    
    ''' <param name="cmdText">需要执行语句,一般是Sql语句,也有存储过程</param>      
    ''' <returns>dataTable,查询到的表格</returns>    
    ''' <remarks></remarks>    
    Public Function ExecSelect(ByVal cmdText As String) As DataTable
        Dim sqlAdapter As SqlDataAdapter
        Dim ds As New DataSet

        '还是给cmd赋值    
        cmd.CommandText = cmdText
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        sqlAdapter = New SqlDataAdapter(cmd)  '实例化adapter   

        Try
            sqlAdapter.Fill(ds)           '用adapter将dataSet填充     

        Catch ex As Exception
            Return (New DataTable)

            'Finally                           '最后一定要销毁cmd    
            '    cmd.Dispose()                 '销毁    
            '    cmd = Nothing
        End Try

        Return ds.Tables(0)           'datatable为dataSet的第一个表  

    End Function



    Public Function ExecuteNonQuery(ByVal cmdText As String) As Integer

        '将传入的值,分别为cmd的属性赋值    

        cmd.CommandType = CommandType.Text             '设置一个值,解释cmdText    

        cmd.CommandText = cmdText            '设置查询的语句  

        Try
            ExecuteNonQuery = cmd.ExecuteNonQuery()     '执行增删改操作    
            cmd.Parameters.Clear()           '清除参数    
        Catch ex As Exception
            errMsg = ex.Message
            Result = False
            ExecuteNonQuery = 0
        Finally

        End Try
        Result = True

    End Function


    Public Sub CloseCommit()
        trans.Commit()
        Close()

    End Sub

    Public Sub CloseRollback()
        trans.Rollback()
        Close()

    End Sub

    ''' <summary>    
    ''' 关闭命令    
    ''' </summary>    
    ''' <remarks></remarks>    
    Public Sub Close()

        If Not IsNothing(cmd) Then          '如果cmd命令存在    
            cmd.Dispose()                   '销毁    
            cmd = Nothing
        End If
        Try
            If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
                conn.Close()                    '关闭连接    
                conn = Nothing                  '不指向原对象    
            End If

        Catch ex As Exception

        End Try

    End Sub


End Class
