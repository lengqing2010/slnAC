Imports Microsoft.VisualBasic

Public Class TableInfoClass

    Public Function GetEnByJp(ByVal myMSSQL As MSSQL, ByVal data_source As String, ByVal db_name As String, ByVal jp As String, ByVal contactTable As String) As String

        Dim sb As New StringBuilder
        With sb
            .AppendLine(" SELECT")
            .AppendLine("	'1' as idx")
            .AppendLine("	,[table_en] as en")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND [table_jp] = '" & jp & "'")
            If contactTable <> "" Then .AppendLine(" AND ([table_jp] in (" & contactTable & ") OR [table_en] in (" & contactTable & "))")

            .AppendLine("  union")
            .AppendLine("  SELECT")
            .AppendLine("	'2' as idx")
            .AppendLine("	,[item_en] as en")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND [item_jp] = '" & jp & "'")
            If contactTable <> "" Then .AppendLine(" AND ([table_jp] in (" & contactTable & ") OR [table_en] in (" & contactTable & "))")

            .AppendLine("  union")
            .AppendLine("   SELECT")
            .AppendLine("	'3' as idx")
            .AppendLine("	,[table_en] as en")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND [table_jp] like '%" & jp & "%'")
            If contactTable <> "" Then .AppendLine(" AND ([table_jp] in (" & contactTable & ") OR [table_en] in (" & contactTable & "))")

            .AppendLine("  union")
            .AppendLine("  SELECT")
            .AppendLine("	'2' as idx")
            .AppendLine("	,[item_en] as en")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND [item_jp] like '%" & jp & "%'")
            If contactTable <> "" Then .AppendLine(" AND ([table_jp] in (" & contactTable & ") OR [table_en] in (" & contactTable & "))")

        End With

        Dim dt As Data.DataTable = myMSSQL.ExecSelect(sb.ToString)

        If dt.Rows.Count = 0 Then
            Return ""
        Else
            Return dt.Rows(0).Item("en").ToString()
        End If
    End Function



    Public Function GetEnKMDATA(ByVal myMSSQL As MSSQL, ByVal data_source As String, ByVal db_name As String, ByVal jp As String, ByVal contactTable As String) As Data.DataTable

        Dim sb As New StringBuilder
        With sb

            .AppendLine("  SELECT")
            .AppendLine("	1,*")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND ([item_jp] = '" & jp & "' OR [item_en] = '" & jp & "')")
            .AppendLine("  union")
            .AppendLine("  SELECT")
            .AppendLine("	2,*")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND ([item_jp] like '%" & jp & "%' OR [item_en] like '%" & jp & "%')")
            If contactTable <> "" Then .AppendLine(" AND ([table_jp] in (" & contactTable & ") OR [table_en] in (" & contactTable & "))")

        End With

        Return myMSSQL.ExecSelect(sb.ToString)

    End Function

End Class
