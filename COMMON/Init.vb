Imports System
Imports System.Net
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Sockets
Imports System.ComponentModel
Imports Microsoft.VisualBasic
Imports MyMethod = System.Reflection.MethodBase

Public Class Init
    '
    Private Const connStrHome As String = "Data Source=WIN7U-20150705K\R2; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=1983313a"
    Private Const connStrCompaney As String = "Data Source=10.160.200.39; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=lixil@2014"
    Private Const connStrDell As String = "Data Source=ADP1QD9478YL0O2\ILIKE; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=19833130"
    Private Const connStrWanguo As String = "Data Source=AGOBW-707150707\SQLEXPRESS2008; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=19833130"

    Public Shared Function connCom() As String
        If System.Net.Dns.GetHostName = "WIN7U-20150705K" Then
            Return connStrHome
        ElseIf System.Net.Dns.GetHostName = "ADP1QD9478YL0O2" Then
            Return connStrDell
        ElseIf System.Net.Dns.GetHostName = "AGOBW-707150707" Then
            Return connStrWanguo
        Else
            Return connStrCompaney
        End If
    End Function

    ''' <summary>
    ''' Sql Server 链接取得
    ''' </summary>
    ''' <param name="connectString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetConnect(ByVal connectString As String) As SqlConnection
        If connectString = "" Then
            Return New SqlConnection(COMMON.Init.connCom())
        Else
            Return New SqlConnection(connectString)
        End If
    End Function

End Class


Public Class NewMsSql

    Public Shared Property OpenDBOnce As Boolean = False
    Public Shared SqlConnection As SqlConnection
    Public Shared cmd As SqlCommand

    ''' <summary>
    ''' 初始化SQL对象
    ''' 需要只初始化一次链接
    ''' 数据库连接字符串，空的时候默认-
    ''' </summary>
    ''' <param name="OpenDBOnceFlg"></param>
    ''' <param name="connectString"></param>
    ''' <remarks></remarks>
    Sub New(Optional ByVal OpenDBOnceFlg As Boolean = False, Optional ByVal connectString As String = "")
        Me.OpenDBOnce = OpenDBOnceFlg
        SqlConnection = Init.GetConnect(connectString)
    End Sub

    ''' <summary>    
    ''' 执行查询的操作,(无参)    
    ''' </summary>    
    ''' <param name="sql">需要执行语句,一般是Sql语句,也有存储过程</param>      
    ''' <returns>dataTable,查询到的表格</returns>    
    ''' <remarks></remarks>    
    Public Shared Function CSel(ByVal sql As String, Optional ByVal connectString As String = "", Optional ByVal EMAB As String = "") As DataTable

        'EMAB障害対応情報の格納処理
        'Dim s As String = (MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        If Not OpenDBOnce Then
            SqlConnection = Init.GetConnect(connectString)
            cmd = New SqlCommand
        End If

        '定义cmd命令    

        Dim sqlAdapter As SqlDataAdapter
        Dim ds As New DataSet
        Try
            '还是给cmd赋值    
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Connection = SqlConnection
            sqlAdapter = New SqlDataAdapter(cmd)    '实例化adapter    
            sqlAdapter.Fill(ds)                     '用adapter将dataSet填充   

            If Not OpenDBOnce Then
                CloseSqlCommand(cmd)
                CloseSqlConnection(SqlConnection)
            End If

            Return ds.Tables(0)                     'datatable为dataSet的第一个表   
        Catch ex As Exception
            If Not OpenDBOnce Then
                CloseSqlCommand(cmd)
                CloseSqlConnection(SqlConnection)
            End If
            Dim myException As New Exception(EMAB & vbNewLine & ex.Message)
            Throw myException
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Select 以外的SQL执行
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="connectString"></param>
    ''' <param name="CommandTimeout"></param>
    ''' <param name="EMAB"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Run(ByVal sql As String, Optional ByVal connectString As String = "", Optional ByVal CommandTimeout As Integer = 30, Optional ByVal EMAB As String = "") As Boolean

        If Not OpenDBOnce Then
            SqlConnection = Init.GetConnect(connectString)
            cmd = New SqlCommand
        End If

        Dim trans As System.Data.SqlClient.SqlTransaction = Nothing
        Try
            SqlConnection.Open()
            trans = SqlConnection.BeginTransaction()
            cmd.Connection = SqlConnection                  '设置连接,全局变量    
            cmd.CommandTimeout = CommandTimeout
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text              '设置一个值,解释cmdText    
            cmd.CommandText = sql                           '设置查询的语句  
            cmd.ExecuteNonQuery()                           '执行增删改操作 
            trans.Commit()
            If Not OpenDBOnce Then
                CloseSqlCommand(cmd)
                CloseSqlConnection(SqlConnection)
            End If
            Return True
        Catch ex As Exception
            trans.Dispose()
            If Not OpenDBOnce Then
                CloseSqlCommand(cmd)
                CloseSqlConnection(SqlConnection)
            End If
            Dim myException As New Exception(EMAB & vbNewLine & ex.Message)
            Throw myException
            Return False
        End Try

    End Function

    ''' <summary>    
    ''' 关闭命令    
    ''' </summary>    
    ''' <remarks></remarks> 
    Private Shared Sub CloseSqlCommand(ByRef cmd As SqlCommand)
        If Not IsNothing(cmd) Then          '如果cmd命令存在    
            cmd.Dispose()                   '销毁    
            cmd = Nothing
        End If
    End Sub

    ''' <summary>    
    ''' 关闭命令    
    ''' </summary>    
    ''' <remarks></remarks>    
    Private Shared Sub CloseSqlConnection(ByRef conn As SqlConnection)
        Try
            If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
                conn.Close()                    '关闭连接    
                conn = Nothing                  '不指向原对象    
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' 关闭所有链接
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub CloseAll()
        CloseSqlCommand(cmd)
        CloseSqlConnection(SqlConnection)
    End Sub

End Class
