Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports COMMON

Public Class DefaultDB


    '获得数据库的连接字符串    
    Public strConnection As String
    '设置连接    
    Public conn As SqlConnection
    '定义cmd命令    
    Public cmd As New SqlCommand

    Public trans As System.Data.SqlClient.SqlTransaction

    Public Result As Boolean = True

    Public errMsg As String

    Public Shared Function SelIt(ByVal sql As String, Optional ByVal connStr As String = "", Optional ByVal CommandTimeout As Integer = 30) As DbResult

        Dim DbResult As New DbResult

        If connStr = "" Then
            connStr = COMMON.Init.connCom
        End If

        Dim conn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand

        Try


            conn.Open()
            cmd.Connection = conn                '设置连接,全局变量    
            cmd.CommandTimeout = CommandTimeout

            Dim sqlAdapter As SqlDataAdapter
            Dim ds As New DataSet
            '还是给cmd赋值    
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            sqlAdapter = New SqlDataAdapter(cmd)  '实例化adapter    

            sqlAdapter.Fill(ds)           '用adapter将dataSet填充     
            DbResult.Data = ds.Tables(0)             'datatable为dataSet的第一个表    
            DbResult.Result = True

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            conn.Close()                    '关闭连接    
            conn = Nothing                  '不指向原对象

            Return DbResult

        Catch ex As Exception

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
                conn.Close()                    '关闭连接    
                conn = Nothing                  '不指向原对象    
            End If

            DbResult.Message = ex.Message
            Return DbResult
        End Try

    End Function

    Public Shared Function RunIt(ByVal sql As String, Optional ByVal connStr As String = "", Optional ByVal CommandTimeout As Integer = 30) As DbResult

        If connStr = "" Then
            connStr = COMMON.Init.connCom
        End If

        Dim DbResult As New DbResult
        Dim conn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        Dim trans As System.Data.SqlClient.SqlTransaction
        conn.Open()
        trans = conn.BeginTransaction()

        Try


            cmd.Connection = conn                '设置连接,全局变量    
            cmd.CommandTimeout = CommandTimeout
            cmd.Transaction = trans

            cmd.CommandType = CommandType.Text             '设置一个值,解释cmdText    
            cmd.CommandText = sql            '设置查询的语句  
            cmd.ExecuteNonQuery()     '执行增删改操作   

            trans.Commit()

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            conn.Close()                    '关闭连接    
            conn = Nothing                  '不指向原对象

            DbResult.Result = True

            Return DbResult

        Catch ex As Exception

            DbResult.Message = ex.Message

            trans.Dispose()

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
                conn.Close()                    '关闭连接    
                conn = Nothing                  '不指向原对象    
            End If

            DbResult.Result = False
            Return DbResult

        End Try

    End Function

End Class
