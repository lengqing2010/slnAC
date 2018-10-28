Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  
Imports COMMON

Public Class Clis6Sql

    Public Shared Function FillDs(ByVal sql As String, Optional ByVal connStr As String = "", Optional ByVal commandTimeout As Integer = 30) As DataSet
        If connStr = "" Then connStr = COMMON.Init.connCom
        Dim connection As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        Dim sqlAdapter As SqlDataAdapter
        connection.Open()
        cmd.Connection = connection
        cmd.CommandTimeout = commandTimeout
        cmd.CommandText = sql
        cmd.CommandType = CommandType.Text
        cmd.Connection = connection
        sqlAdapter = New SqlDataAdapter(cmd)
        Dim rtvDataDs As New DataSet
        sqlAdapter.Fill(rtvDataDs)
        cmd.Dispose()
        cmd = Nothing
        connection.Close()
        connection = Nothing
        Return rtvDataDs
    End Function

    Public Shared Function ExecuteNonQuery(ByVal sql As String, Optional ByVal connStr As String = "", Optional ByVal commandTimeout As Integer = 30) As Boolean
        If connStr = "" Then connStr = COMMON.Init.connCom
        Dim conn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        conn = New SqlConnection(connStr)
        conn.Open()
        cmd.Connection = conn
        cmd.CommandTimeout = commandTimeout
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn = Nothing
        Return True
    End Function

    Public Shared Function ExecuteNonQuery(ByVal sql As List(Of String), Optional ByVal connStr As String = "", Optional ByVal commandTimeout As Integer = 60) As Boolean
        If connStr = "" Then connStr = COMMON.Init.connCom
        Dim conn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        Dim trans As System.Data.SqlClient.SqlTransaction


        conn = New SqlConnection(connStr)
        conn.Open()
        cmd.Connection = conn
        trans = conn.BeginTransaction()

        Try
            For i As Integer = 0 To sql.Count - 1
                cmd.CommandTimeout = commandTimeout
                cmd.CommandType = CommandType.Text
                cmd.CommandText = sql(i)
                cmd.ExecuteNonQuery()
            Next
            trans.Commit()
            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn = Nothing
            Return True

        Catch ex As Exception
            trans.Rollback()
            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn = Nothing
            Return False
        End Try
    End Function



End Class
