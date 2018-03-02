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

        Dim dr As DataRow
        dr = dt.NewRow
        dt.Rows.InsertAt(dr, 0)

        Return dt

    End Function

    Public Function GetEdpList() As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT edp_no as value ,edp_no+' '+edp_mei as text")
            .AppendLine("FROM [m_edp]")
            .AppendLine("ORDER BY [edp_no] desc")
        End With

        Dim msSql As New CMsSql()
        Dim dt As DataTable = msSql.ExecSelect(sb.ToString)

        Return dt

    End Function


    Public Function GetTableNames() As Data.DataTable

    End Function


End Class
