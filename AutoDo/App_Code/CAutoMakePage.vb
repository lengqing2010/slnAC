Imports Microsoft.VisualBasic
Imports System.Data

Public Class CAutoMakePage

    Public CAutoMakePageDA As New CAutoMakePageDA
    Public RealDB_TableInfo As DataTable
    Public RealDB_TableInfo_PK As DataTable
    Public RealDB_TableInfo_NotPK As DataTable

    Sub New(ByVal tableName As String)

        RealDB_TableInfo = CAutoMakePageDA.RealDB_GetTableInfo(tableName)
        RealDB_TableInfo_PK = RealDB_TableInfo.Clone
        RealDB_TableInfo_NotPK = RealDB_TableInfo.Clone
        For Each dr As DataRow In RealDB_TableInfo.Select("PK='P'")
            RealDB_TableInfo_PK.Rows.Add(dr.ItemArray)
        Next

        For Each dr As DataRow In RealDB_TableInfo.Select("PK<>'P'")
            RealDB_TableInfo_NotPK.Rows.Add(dr.ItemArray)
        Next
    End Sub




End Class

Public Class CAutoMakePageDA

    Public ClassEX As Exception

    ''' <summary>
    ''' Real DB から Table情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RealDB_GetTableInfo(ByVal tableName As String) As DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT a.NAME AS table_name, b.NAME AS columns_name, c.NAME AS columns_type, b.length AS columns_length")
            .AppendLine(" , CASE ")
            .AppendLine("     WHEN d.TABLE_NAME IS NULL THEN ''")
            .AppendLine("     ELSE 'P'")
            .AppendLine(" END AS PK")
            .AppendLine(" ,SM.TEXT AS DefaultValue")
            .AppendLine(" ,b.IsNullAble") '0:false 1 true
            .AppendLine("FROM sysobjects a")
            .AppendLine(" INNER JOIN syscolumns b ON a.id = b.id")
            .AppendLine(" INNER JOIN systypes c ON b.xtype = c.xtype")
            .AppendLine(" LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE d")
            .AppendLine(" ON d.TABLE_NAME = a.NAME")
            .AppendLine("     AND d.COLUMN_NAME = b.NAME")
            .AppendLine(" LEFT JOIN dbo.syscomments SM ON b.cdefault = SM.id")
            .AppendLine("WHERE a.xtype = 'U'")
            .AppendLine("       AND a.NAME = '" & tableName & "'  ")
            .AppendLine("and c.NAME <> 'sysname'")
        End With
        Try

            Dim dt As DataTable = Clis6Sql.FillDs(sb.ToString).Tables(0)
            Dim pkIdx As Integer = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                'Length の 再設定する
                Dim cty As String = dt.Rows(i).Item("columns_type")
                If Left(cty, 1) = "n" AndAlso cty <> "numeric" Then
                    dt.Rows(i).Item("columns_length") = CInt(dt.Rows(i).Item("columns_length")) / 2
                End If
            Next
            Return dt
        Catch ex As Exception
            ClassEX = ex
            Return Nothing
        End Try

    End Function

End Class


