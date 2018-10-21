Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  


Public Class DbResult

    Public Data As New Data.DataTable
    Public Message As String
    Public Result As Boolean

End Class


Public Class DefaultDB


    '获得数据库的连接字符串    
    Public strConnection As String
    '设置连接    
    Public conn As SqlConnection
    '定义cmd命令    
    Public cmd As New SqlCommand

    'Public trans As System.Data.SqlClient.SqlTransaction

    Public Result As Boolean = True

    Public errMsg As String

    Public Shared Function SelIt(ByVal sql As String, Optional ByVal connStr As String = "", Optional ByVal CommandTimeout As Integer = 30) As DataSet

        'Dim DbResult As New DbResult

        If connStr = "" Then
            connStr = "Data Source=10.160.192.73; Initial Catalog=ZKAccess;Persist Security Info=True;User ID=SA;Password=Lixil2015"
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
            'DbResult.Data = ds.Tables(0)             'datatable为dataSet的第一个表    
            'DbResult.Result = True

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            conn.Close()                    '关闭连接    
            conn = Nothing                  '不指向原对象
            sqlAdapter = Nothing

            Return ds

        Catch ex As Exception

            cmd.Dispose()                   '销毁    
            cmd = Nothing

            If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
                conn.Close()                    '关闭连接    
                conn = Nothing                  '不指向原对象    
            End If

            'DbResult.Message = ex.Message
            'Return DbResult
        End Try

    End Function


End Class
