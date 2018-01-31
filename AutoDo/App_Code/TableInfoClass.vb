Imports Microsoft.VisualBasic

Public Class TableInfoClass

    Public Function GetEnByJp(ByVal data_source As String, ByVal db_name As String, ByVal jpIn As String, ByVal contactTable As String) As String

        Dim jp As String = jpIn.Trim

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

        Dim dt As Data.DataTable
        Dim msg As String
        MSSQL.SEL(COMMON.Init.connCom, sb.ToString, dt:=dt, msg:=msg)
        If dt.Rows.Count = 0 Then
            Return jpIn
        Else
            Return dt.Rows(0).Item("en").ToString()
        End If

    End Function



    Public Function GetEnKMDATA(ByVal data_source As String, ByVal db_name As String, ByVal jpIn As String, ByVal contactTable As String) As Data.DataTable
        Dim jp As String = jpIn.Trim
        Dim sb As New StringBuilder
        With sb
            .AppendLine("  SELECT distinct * FROM (")

            .AppendLine("  SELECT")
            .AppendLine("	 item_en,item_type,item_jp,item_keta ")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND ([item_jp] = '" & jp & "' OR [item_en] = '" & jp & "')")
            .AppendLine("  union")
            .AppendLine("  SELECT")
            .AppendLine("	 item_en,item_type,item_jp,item_keta ")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND ([item_jp] like '%" & jp & "%' OR [item_en] like '%" & jp & "%')")
            If contactTable <> "" Then .AppendLine(" AND ([table_jp] in (" & contactTable & ") OR [table_en] in (" & contactTable & "))")
            .AppendLine("  ) a ")

        End With

        Dim dt As Data.DataTable
        Dim msg As String
        MSSQL.SEL(COMMON.Init.connCom, sb.ToString, dt:=dt, msg:=msg)
        Return dt
    End Function


    Public Function GetEnTblMs(ByVal data_source As String, ByVal db_name As String, ByVal tableName As String) As Data.DataTable
        Dim sb As New StringBuilder
        With sb
            .AppendLine("  SELECT distinct * FROM (")
            .AppendLine("  SELECT")
            .AppendLine("	 item_en,item_type,item_jp,item_keta ")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND [table_en] = '" & tableName & "' ")

            .AppendLine("  ) a ")
        End With
        Dim dt As Data.DataTable
        Dim msg As String
        MSSQL.SEL(COMMON.Init.connCom, sb.ToString, dt:=dt, msg:=msg)
        Return dt
    End Function






    Public Function GetTableNameInfo(ByVal data_source As String, ByVal db_name As String) As Data.DataTable
        Dim sb As New StringBuilder
        With sb
            .AppendLine(" SELECT")
            .AppendLine("	distinct table_en,table_jp")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" ORDER BY table_en")
        End With
        Dim dt As Data.DataTable
        Dim msg As String
        MSSQL.SEL(COMMON.Init.connCom, sb.ToString, dt:=dt, msg:=msg)
        Return dt
    End Function

    Public Function GetSelTables(ByVal user_id As String, ByVal edp_no As String, ByVal db_conn As String) As Data.DataTable
        Dim sb As New StringBuilder
        With sb
            .AppendLine(" SELECT")
            .AppendLine("	*")
            .AppendLine(" FROM [auto_code].[dbo].[m_main_use_table]")
            .AppendLine(" WHERE ")
            .AppendLine("	[user_id] = '" & user_id & "'")
            .AppendLine(" AND [edp_no] = '" & edp_no & "'")
            .AppendLine(" AND [db_conn] = '" & db_conn & "'")

        End With
        Dim dt As Data.DataTable
        Dim msg As String
        MSSQL.SEL(COMMON.Init.connCom, sb.ToString, dt:=dt, msg:=msg)
        Return dt
    End Function

End Class
