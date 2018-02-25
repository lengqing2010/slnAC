Imports Microsoft.VisualBasic
Imports System.Data

Public Class CDB


    Public Function GetDbServerList() As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT distinct data_source+':'+db_name as text,db_conn as value")
            .AppendLine("FROM [m_db_info]")
        End With

        Dim msSql As New CMsSql()
        Dim dt As DataTable = msSql.ExecSelect(sb.ToString)

        Return dt

    End Function


    Public Function GetTableNames() As Data.DataTable

    End Function


End Class
