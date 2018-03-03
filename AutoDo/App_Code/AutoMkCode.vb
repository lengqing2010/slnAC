Imports Microsoft.VisualBasic
Imports System.Data

Public Class AutoMkCode
    Enum ParamType
        SqlParam = 1
        NoParam = 2
    End Enum

    Public Const conByvalParamStr = "@@@byvalParam@@@"
    Public Const conEmabParam = "@@@emabParam@@@"
    Public Const conTblNameBunkatu = "|"
    Public Shared Function vbtabSuu(ByVal suu As Integer) As String
        Dim rt As String = ""
        For i As Integer = 0 To suu
            If i > 0 Then
                rt = rt & "    "
            End If
        Next
        Return rt
    End Function

    Public Shared Function GetByvalParam(ByVal active_database_dt As DataTable, ByVal VSType As String) As String

        Dim sbByval As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1


            If j > 0 Then
                sbByval.Append(vbtabSuu(8) & ", ")
            End If

            If VSType = "2005" Then
                'Byval と EMABの PARAM
                sbByval.Append("Byval " & MakeStrFirstCharUpper(active_database_dt.Rows(j).Item("columns_name")) & " AS " & "")
            Else
                'Byval と EMABの PARAM
                sbByval.Append(" " & MakeStrFirstCharUpper(active_database_dt.Rows(j).Item("columns_name")) & " AS " & "")
            End If

            sbByval.Append(GetTypeFromDBType(active_database_dt.Rows(j).Item("columns_type").ToString.ToLower))

            'param
            If j <> active_database_dt.Rows.Count - 1 Then
                sbByval.Append(" _" & vbNewLine)
            End If

        Next

        Return sbByval.ToString

    End Function


    Public Shared Function GetEmabParam(ByVal active_database_dt As DataTable, Optional ByVal firstKama As Boolean = True, Optional ByVal VSType As String = "2005") As String
        Dim emabParam As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1
            'Byval と EMABの PARAM
            If VSType = "2005" Then
                If j = 0 Then
                    If firstKama Then
                        emabParam.Append(vbtabSuu(10) & ", ")
                    End If
                Else
                    emabParam.Append(vbtabSuu(10) & ", ")
                End If
            Else
                If j = 0 Then
                Else
                    emabParam.Append(vbtabSuu(5) & ", ")
                End If

            End If


            emabParam.Append(MakeStrFirstCharUpper(active_database_dt.Rows(j).Item("columns_name")))
            'param
            If j <> active_database_dt.Rows.Count - 1 Then
                emabParam.Append(" _" & vbNewLine)
            End If
        Next
        emabParam.Append(")")
        Return emabParam.ToString
    End Function


    Public Shared Function GetSbDAImports(ByVal VSType As String) As String
        Dim sbDAImports As New StringBuilder
        If VSType = "2005" Then
            sbDAImports.AppendLine("Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager")
            sbDAImports.AppendLine("Imports MyMethod = System.Reflection.MethodBase")
            sbDAImports.AppendLine("Imports Itis.ApplicationBlocks.Data.SQLHelper")
            sbDAImports.AppendLine("Imports System.Text")
            sbDAImports.AppendLine("Imports System.Data.SqlClient")
            sbDAImports.AppendLine("Imports System.Transactions")
            sbDAImports.AppendLine("Imports System.Configuration.ConfigurationSettings")
        Else
            sbDAImports.AppendLine("Imports System.Text")
            sbDAImports.AppendLine("Imports Elixil")
            sbDAImports.AppendLine("Imports Elixil.SqlAccess")
        End If

        Return sbDAImports.ToString
    End Function

    Public Shared Function GetSbBCImports(ByVal VSType As String) As String
        Dim sbBCImports As New StringBuilder
        If VSType = "2005" Then
            sbBCImports.AppendLine("Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager")
            sbBCImports.AppendLine("Imports MyMethod = System.Reflection.MethodBase")
            sbBCImports.AppendLine("Imports Itis.vurikk.DataAccess")
            sbBCImports.AppendLine("Imports System.Transactions")
        Else
            sbBCImports.AppendLine("Imports Lixil.STAR.DataAccess")
            sbBCImports.AppendLine("Imports Lixil.STAR.Utilities")

        End If

        Return sbBCImports.ToString
    End Function

    ''' <summary>
    ''' 变量名生成
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MakeStrFirstCharUpper(ByVal str As String) As String

        Dim sb As New System.Text.StringBuilder
        For i As Integer = 0 To str.Length - 1

            Dim chr As String = str.Substring(i, 1)

            Dim chrPre As String = ""

            If i > 0 Then
                chrPre = str.Substring(i - 1, 1)
            End If

            If chrPre <> "_" Then
                chr = (chr.ToLower)
            Else
                chr = (chr.ToUpper)
            End If

            If chr <> "_" Then
                sb.Append(chr)
            End If

        Next

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' 变量类型
    ''' </summary>
    ''' <param name="Dbtype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypeFromDBType(ByVal Dbtype As String) As String

        If "varchar,char,datetime,datetime2,nvarchar,nchar,text".IndexOf(Dbtype) >= 0 Then
            Return "String"

        ElseIf "int,tinyint".IndexOf(Dbtype) >= 0 Then
            Return "Integer"

        ElseIf "numeric,float,money,smallmoney,decimal".IndexOf(Dbtype) >= 0 Then
            Return "Decimal"
        Else
            Return Dbtype
        End If

        Return ""

    End Function

    Public Shared Function GetSelectKmStr(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String, Optional ByVal noteKbn As Boolean = True) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder

        For j As Integer = 0 To dt.Rows.Count - 1

            Dim columns_name As String = dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = dt.Rows(j).Item("columns_length").ToString

            'Byval と EMABの PARAM
            sb.Append(vbtabSuu(1) & "sb.AppendLine(""")
            If j > 0 Then
                sb.Append(vbtabSuu(1) & " ,")
            Else
                sb.Append(vbtabSuu(1) & "  ")
            End If
            sb.Append(vbtabSuu(1) & dt.Rows(j).Item("table_name"))
            sb.Append("." & dt.Rows(j).Item("columns_name"))
            sb.Append(""")")
            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "'"))
            End If
            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function

    Public Shared Function GetINSKmPARAMStr(ByVal active_database_dt As DataTable, ByVal db_name As String, ByVal table_name As String, _
                                       ByVal noteKbn As Boolean, _
                                       ByVal paramFlg As ParamType) As String
        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)
        Dim sb As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1

            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = active_database_dt.Rows(j).Item("columns_length").ToString

            'Byval と EMABの PARAM

            sb.Append(vbtabSuu(1) & "sb.AppendLine(""")

            If j > 0 Then
                sb.Append(vbtabSuu(1) & ",")
            Else
                sb.Append(vbtabSuu(1) & " ")
            End If


            If paramFlg = ParamType.SqlParam Then
                sb.Append(vbtabSuu(1) & "@" & columns_name)


            ElseIf paramFlg = ParamType.NoParam Then

                Dim tmType As String = GetTypeFromDBType(columns_type)

                If tmType = "Integer" Or tmType = "Decimal" Then
                    sb.Append(vbtabSuu(1) & """ & " & MakeStrFirstCharUpper(columns_name) & " & """)
                Else
                    sb.Append(vbtabSuu(1) & "'"" & " & MakeStrFirstCharUpper(columns_name) & " & ""' ")
                End If

            End If

            sb.Append(""")")
            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "' "))
            End If

            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' 固定LongのString作成
    ''' </summary>
    ''' <param name="inStr"></param>
    ''' <param name="note"></param>
    ''' <param name="noteSign"></param>
    ''' <param name="bunkatuLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AddNote(ByVal inStr As String, ByVal note As String, Optional ByVal noteSign As String = "'", Optional ByVal bunkatuLength As Integer = 32) As String

        Dim arr() As String = inStr.Split(vbNewLine)
        Dim str As String = RTrim(arr(arr.Length - 1)).Replace(vbCr, "").Replace(vbLf, "")

        Dim i As Integer
        Dim length As String = 0
        Dim s As String
        For i = 1 To Len(str)
            s = Mid(str, i, 1)

            If s = vbTab Then
                length += 4
            ElseIf Asc(s) >= 0 Then
                length += 1
            ElseIf Asc(s) < 0 Then
                length += 2
            Else
                length += Len(s)
            End If
        Next

        Dim modLenth As Integer = (length Mod bunkatuLength)

        If modLenth = 0 Then
            Return noteSign & note
        Else
            Dim midlength As Integer = bunkatuLength - (length Mod bunkatuLength)
            Return "".ToString.PadLeft(midlength) & noteSign & " " & note
        End If


    End Function

    ''' <summary>
    ''' VB　SQL PARAM
    ''' </summary>
    ''' <param name="active_database_dt "></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSQLParam(ByVal active_database_dt As DataTable, ByVal db_name As String, ByVal table_name As String) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sbParam As New StringBuilder
        sbParam.AppendLine(vbtabSuu(1) & "'バラメタ格納")
        sbParam.AppendLine(vbtabSuu(1) & "Dim paramList As New List(Of SqlParameter)")

        For j As Integer = 0 To active_database_dt.Rows.Count - 1

            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = active_database_dt.Rows(j).Item("columns_length").ToString

            'Param
            sbParam.Append(vbtabSuu(1) & "paramList.Add(MakeParam(""@")
            sbParam.Append(columns_name)
            sbParam.Append(""", SqlDbType.")
            If active_database_dt.Rows(j).Item("columns_type").ToString.ToLower = "varchar" Then
                sbParam.Append("VarChar,")
                sbParam.Append(" " & active_database_dt.Rows(j).Item("columns_length").ToString)
            ElseIf active_database_dt.Rows(j).Item("columns_type").ToString.ToLower = "datetime" Then
                sbParam.Append("DateTime,")
                sbParam.Append(" 24")
            ElseIf active_database_dt.Rows(j).Item("columns_type").ToString.ToLower = "tinyint" Then
                sbParam.Append("Int,")
                sbParam.Append(" " & active_database_dt.Rows(j).Item("columns_length").ToString)
            ElseIf active_database_dt.Rows(j).Item("columns_type").ToString.ToLower = "int" Then
                sbParam.Append("Int,")
                sbParam.Append(" " & active_database_dt.Rows(j).Item("columns_length").ToString)
            ElseIf active_database_dt.Rows(j).Item("columns_type").ToString.ToLower = "char" Then
                sbParam.Append("Char,")
                sbParam.Append(" " & active_database_dt.Rows(j).Item("columns_length").ToString)
            ElseIf active_database_dt.Rows(j).Item("columns_type").ToString.ToLower = "numeric" Then
                sbParam.Append("Decimal,")
                Dim keta As String = active_database_dt.Rows(j).Item("columns_length").ToString
                Dim kt As Int32
                If keta.Split(",").Length = 2 Then
                    Dim k1 As String = keta.Split(",")(0)
                    Dim k2 As String = keta.Split(",")(1)
                    If k2.Trim = "" Then k2 = "0"
                    kt = CInt(k1)
                    sbParam.Append(" " & kt.ToString)
                Else
                    sbParam.Append(" " & active_database_dt.Rows(j).Item("columns_length").ToString)

                End If

            Else
                sbParam.Append(active_database_dt.Rows(j).Item("columns_type").ToString & ",")
                sbParam.Append(" " & active_database_dt.Rows(j).Item("columns_length").ToString)

            End If
            sbParam.Append(", " & MakeStrFirstCharUpper(active_database_dt.Rows(j).Item("columns_name")) & "))")

            sbParam.Append(AddNote(sbParam.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "'"))

            sbParam.Append(vbNewLine)
        Next
        Return sbParam.ToString
    End Function



    Public Shared Function GetUpdWhereKmStr(ByVal active_database_dt As DataTable, _
                                            ByVal auto_code_info_datatable As DataTable, _
                                            ByVal db_name As String, _
                                            ByVal table_name As String, _
                                            ByVal noteKbn As Boolean, _
                                            ByVal ParamFlg As ParamType) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1

            'Byval と EMABの PARAM
            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString

            sb.Append(vbtabSuu(1) & "sb.AppendLine(""")

            If j > 0 Then
                sb.Append(vbtabSuu(1) & " ,")
            Else
                sb.Append(vbtabSuu(1) & "  ")
            End If
            sb.Append(vbtabSuu(1) & active_database_dt.Rows(j).Item("columns_name"))
            sb.Append(" = ")
            'If ParamFlg Then
            '    sb.Append("@" & active_database_dt.Rows(j).Item("columns_name"))
            'Else
            '    If "numeric,int".IndexOf(active_database_dt.Rows(j).Item("columns_type").ToString.ToLower) >= 0 Then
            '        sb.Append(""" & " & MakeStrFirstCharUpper(active_database_dt.Rows(j).Item("columns_name")) & " & """)
            '    Else
            '        sb.Append("'"" & " & MakeStrFirstCharUpper(active_database_dt.Rows(j).Item("columns_name")) & " & ""' ")
            '    End If
            'End If

            If ParamFlg = ParamType.SqlParam Then
                sb.Append(vbtabSuu(1) & "@" & columns_name)


            ElseIf ParamFlg = ParamType.NoParam Then

                Dim tmType As String = GetTypeFromDBType(columns_type)

                If tmType = "Integer" Or tmType = "Decimal" Then
                    sb.Append(vbtabSuu(1) & """ & " & MakeStrFirstCharUpper(columns_name) & " & """)
                Else
                    sb.Append(vbtabSuu(1) & "'"" & " & MakeStrFirstCharUpper(columns_name) & " & ""' ")
                End If

            End If


            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "--", 16))
            End If
            sb.Append(""")")
            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "'"))
            End If
            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="active_database_dt"></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <param name="noteKbn"></param>
    ''' <param name="ParamFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSelectWhereKmStr(ByVal active_database_dt As DataTable, _
                                               ByVal db_name As String, _
                                               ByVal table_name As String, _
                                               ByVal noteKbn As Boolean, _
                                               ByVal paramFlg As ParamType) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1


            'Byval と EMABの PARAM
            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString


            'Byval と EMABの PARAM

            sb.Append(vbtabSuu(1) & "sb.AppendLine(""")

            If j > 0 Then
                sb.Append(vbtabSuu(1) & " AND")
            Else
                sb.Append(vbtabSuu(1) & "  ")
            End If
            sb.Append(vbtabSuu(1) & table_name)
            sb.Append("." & columns_name)
            sb.Append(" = ")

            'SQL PARAM
            If paramFlg = ParamType.SqlParam Then
                sb.Append(vbtabSuu(1) & "@" & columns_name)


            ElseIf paramFlg = ParamType.NoParam Then

                Dim tmType As String = GetTypeFromDBType(columns_type)

                If tmType = "Integer" Or tmType = "Decimal" Then
                    sb.Append(vbtabSuu(1) & """ & " & MakeStrFirstCharUpper(columns_name) & " & """)
                Else
                    sb.Append(vbtabSuu(1) & "'"" & " & MakeStrFirstCharUpper(columns_name) & " & ""' ")
                End If

            End If

            sb.Append(""")")

            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "' "))
            End If

            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function



    ''' <summary>
    ''' 注释
    ''' </summary>
    ''' <param name="active_database_dt "></param>
    ''' <param name="fncFirstStr"></param>
    ''' <param name="dtKjName"></param>
    ''' <param name="dtEnglishName"></param>
    ''' <param name="dousa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFncNotes(ByVal fncType As String, _
                                       ByVal active_database_dt As DataTable, _
                                       ByVal auto_code_info_datatable As DataTable, _
                                       ByVal fncFirstStr As String, _
                                       ByVal dtKjName As String, _
                                       ByVal dtEnglishName As String,
                                       ByVal dousa As String, _
                                       ByVal db_name As String, _
                                       ByVal table_name As String, _
                                       ByVal actionType As String, _
                                       ByVal noteKbn As Boolean, _
                                       ByVal paramFlg As ParamType) As String

        'active_database_dt :アクセスDBの情報
        'auto_code_info_datatable:auto_code情報

        Dim sb As New StringBuilder
        Dim rtv As String = ""

        'DA
        If fncType = "DA" Then
            sb.AppendLine("'DAソース作成")
        Else
            sb.AppendLine("'BCソース作成")
        End If

        If actionType <> "select" Then

            sb.AppendLine("''' <summary>")
            sb.AppendLine("''' " & dtKjName & "情報を" & dousa & "取得する")
            sb.AppendLine("''' </summary>")
            sb.Append(GetTitleParam(active_database_dt, auto_code_info_datatable, db_name, table_name))
            sb.AppendLine("''' <returns>" & dtKjName & "情報</returns>")
            sb.AppendLine("''' <remarks></remarks>")
            sb.AppendLine("''' <history>")
            sb.AppendLine("''' <para>" & Now.ToString("yyyy/MM/dd") & " P-99999 ??さん 新規作成 </para>")
            sb.AppendLine("''' </history>")

            sb.AppendLine("Public Function " & GetFunctionName(fncFirstStr, dtEnglishName) & "(" & conByvalParamStr & ") As Data.DataTable")
            sb.AppendLine(vbtabSuu(1) & "'EMAB障害対応情報の格納処理")
            sb.AppendLine(vbtabSuu(1) & "EMAB.AddMethodEntrance(MyClass.GetType.FullName & ""."" & MyMethod.GetCurrentMethod.Name _" & vbNewLine & conEmabParam)
            sb.AppendLine(vbtabSuu(1) & "'SQLコメント")
            sb.AppendLine(vbtabSuu(1) & "'--**テーブル：" & dtEnglishName)
            sb.AppendLine(vbtabSuu(1) & "Dim sb As New StringBuilder")
            sb.AppendLine("'SQL文")

        End If

        If actionType = "insert" Then
            Dim tblNameArr() As String = dtEnglishName.Split(conTblNameBunkatu)
            Dim dtKjNameArr() As String = dtKjName.Split(conTblNameBunkatu)
            sb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""INSERT INTO " & tblNameArr(0) & "("")")
            sb.AppendLine(GetSelectKmStr(active_database_dt, db_name, table_name, True))
            sb.AppendLine(vbtabSuu(1) & "sb.AppendLine("")"")")
            sb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""VALUES ("")")
            sb.AppendLine(GetINSKmPARAMStr(active_database_dt, db_name, table_name, noteKbn, paramFlg))
            sb.AppendLine(vbtabSuu(1) & "sb.AppendLine("")"")")


        ElseIf actionType = "update" Then

            sb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""UPDATE " & dtEnglishName & " SET "")")
            sb.AppendLine(GetUpdWhereKmStr(active_database_dt, auto_code_info_datatable, db_name, table_name, noteKbn, paramFlg))

        ElseIf actionType = "select" Then
            sb.AppendLine(GetDAStringSelect(active_database_dt, auto_code_info_datatable, dtKjName, dtEnglishName, "2005", db_name, table_name, actionType, noteKbn, paramFlg))


        End If



        If actionType <> "select" Then

            'PARAM
            If paramFlg = ParamType.SqlParam Then
                sb.AppendLine(GetSQLParam(active_database_dt, db_name, table_name))
                sb.AppendLine(vbtabSuu(1) & "Return SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0")
            ElseIf paramFlg = ParamType.NoParam Then
                sb.AppendLine(vbtabSuu(1) & "Return SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sqlBuffer.ToString()) > 0")


            End If

            sb.AppendLine("End Function")
        End If

        Return sb.ToString

    End Function


    Public Shared Function GetDAStringSelect(ByVal active_database_dt As DataTable, _
                                                ByVal auto_code_info_datatable As DataTable, _
                                                ByVal dtKjName As String, _
                                                ByVal dtEnglishName As String, _
                                                ByVal VSType As String, _
                                                ByVal db_name As String, _
                                                ByVal table_name As String, _
                                                ByVal actionType As String, _
                                                ByVal noteKbn As Boolean, _
                                                ByVal paramFlg As ParamType) As String
        Dim daSb As New StringBuilder
        'BC

        '        ''' <summary>
        '        ''' 案件情報を取得する
        '        ''' </summary>
        '        ''' <returns>案件情報</returns>
        '        ''' <remarks></remarks>
        '        ''' <history>
        '        ''' <para>2017/06/14 P-99999 ??さん 新規作成 </para>
        '        ''' </history>
        'Public Function SelmAnkan(ByVal edpNo As String _
        '                 , ByVal pjNameKanji As String _
        '                 , ByVal userId As String _
        '                 , ByVal dbConnectStr1 As String _
        '                 , ByVal dbConnectStr2 As String _
        '                 , ByVal dbConnectStr3 As String) As Data.DataTable
        '    'EMAB障害対応情報の格納処理
        '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
        '                   , edpNo _
        '                   , pjNameKanji _
        '                   , userId _
        '                   , dbConnectStr1 _
        '                   , dbConnectStr2 _
        '                   , dbConnectStr3)
        'DA


        Dim firstFncStr As String = ""
        Dim dousa As String = ""
        If actionType = "insert" Then
            firstFncStr = "Set"
            dousa = "登録"
        ElseIf actionType = "update" Then
            firstFncStr = "Upd"
            dousa = "更新"
        ElseIf actionType = "select" Then
            firstFncStr = "Sel"
            dousa = "検索"
        End If

        daSb.AppendLine(GetSELECTFunctionTitle(active_database_dt, _
                                       auto_code_info_datatable, _
                                       firstFncStr, _
                                       dtKjName.Replace(conTblNameBunkatu, ""), _
                                       dtEnglishName.Replace(conTblNameBunkatu, ""), _
                                       dousa, _
                                       VSType, _
                                       db_name, _
                                       table_name))

        daSb.AppendLine(vbtabSuu(1) & "'戻りデータセット")
        daSb.AppendLine(vbtabSuu(1) & "Dim dsInfo As New Data.DataSet")
        daSb.AppendLine(vbtabSuu(1) & "'SQLコメント")
        daSb.AppendLine("'--**テーブル：" & dtEnglishName.Replace(conTblNameBunkatu, ""))
        daSb.AppendLine(vbtabSuu(1) & "Dim sb As New StringBuilder")
        daSb.AppendLine("'SQL文")
        daSb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""SELECT"")")
        daSb.AppendLine(GetSelectKmStr(active_database_dt, db_name, table_name, True))

        Dim tblNameArr() As String = dtEnglishName.Split(conTblNameBunkatu)
        Dim dtKjNameArr() As String = dtKjName.Split(conTblNameBunkatu)

        For i As Integer = 0 To tblNameArr.Length - 1

            If i = 0 Then
                daSb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""FROM " & tblNameArr(i) & """)" & vbtabSuu(1) & vbtabSuu(1) & "' " & dtKjNameArr(i))
            Else
                daSb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""LEFT JOIN " & tblNameArr(i) & """)" & vbtabSuu(1) & vbtabSuu(1) & "' " & dtKjNameArr(i))
                daSb.AppendLine(vbtabSuu(1) & "sb.AppendLine("" ON  " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")" & vbtabSuu(1) & vbtabSuu(1) & "' " & dtKjNameArr(i))
                daSb.AppendLine(vbtabSuu(1) & "sb.AppendLine("" AND " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")" & vbtabSuu(1) & vbtabSuu(1) & "' " & dtKjNameArr(i))
            End If

        Next
        daSb.AppendLine(vbtabSuu(1) & "sb.AppendLine(""WHERE"")")
        daSb.AppendLine(GetSelectWhereKmStr(active_database_dt, db_name, table_name, noteKbn, paramFlg))

        daSb.AppendLine()
        daSb.AppendLine(GetSQLParam(active_database_dt, db_name, table_name))
        daSb.AppendLine(vbtabSuu(1) & "FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, """ & dtEnglishName & """, paramList.ToArray)")
        daSb.AppendLine()
        daSb.AppendLine(vbtabSuu(1) & "Return dsInfo.Tables(""" & dtEnglishName & """)")
        daSb.AppendLine()
        daSb.AppendLine("End Function")

        Return daSb.ToString
    End Function




    ''' <summary>
    ''' 注释
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTitleParam(ByVal dt As DataTable, ByVal auto_code_info_datatable As DataTable, ByVal db_name As String, ByVal table_name As String) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)
        ' Dim active_database_dt  As DataTable = AutoCodeDbClass.active_database_dt 

        Dim sbByval As New StringBuilder

        For i As Integer = 0 To auto_code_info_datatable.Rows.Count - 1
            'Dim table_name As String = dt.Rows(i).Item("table_name").ToString
            Dim columns_name As String = dt.Rows(i).Item("columns_name").ToString
            Dim columns_type As String = dt.Rows(i).Item("columns_type").ToString
            Dim columns_length As Integer = dt.Rows(i).Item("columns_length").ToString

            sbByval.Append("''' <param name=""" & MakeStrFirstCharUpper(columns_name) & """>" & "")
            sbByval.Append(MakeStrFirstCharUpper(AutoCodeDbClass.Get_name_jp(columns_name)) & "</param>" & "")
            sbByval.Append(vbNewLine)

        Next
        Return sbByval.ToString
    End Function

    ''' <summary>
    ''' Function 名
    ''' </summary>
    ''' <param name="fncFirstStr"></param>
    ''' <param name="dtEnglishName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFunctionName(ByVal fncFirstStr As String, ByVal dtEnglishName As String) As String
        Return fncFirstStr & MakeStrFirstCharUpper(dtEnglishName)
    End Function

    ''' <summary>
    ''' Dim XX As String
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDimString(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String) As String

        Dim sbByval As New StringBuilder

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        For i As Integer = 0 To dt.Rows.Count - 1

            'Dim table_name As String = dt.Rows(i).Item("table_name").ToString
            Dim columns_name As String = dt.Rows(i).Item("columns_name").ToString
            Dim columns_type As String = dt.Rows(i).Item("columns_type").ToString
            Dim columns_length As Integer = dt.Rows(i).Item("columns_length").ToString

            '1.注释
            '名　Length
            sbByval.Append("'")
            sbByval.Append("" & AutoCodeDbClass.Get_name_jp(columns_name))
            sbByval.Append("(" & columns_length & ")")
            sbByval.Append(vbNewLine)

            '2.Dim
            sbByval.Append("Dim")
            sbByval.Append(" " & MakeStrFirstCharUpper(columns_name))
            sbByval.Append(" " & "AS")
            sbByval.Append(" " & GetTypeFromDBType(columns_type))

            sbByval.Append(vbNewLine)
        Next

        Return sbByval.ToString

    End Function

    ''' <summary>
    ''' DA String
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDaFuncString(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String, ByVal actionType As String, _
                                                ByVal noteKbn As Boolean, _
                                                ByVal paramFlg As ParamType) As String

        Dim sbByval As New StringBuilder

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim tblKjName As String = AutoCodeDbClass.Get_Table_name_jp(table_name)
        Dim tblEnName As String = table_name


        Dim firstFncStr As String = ""
        Dim dousa As String = ""
        If actionType = "insert" Then
            firstFncStr = "Ins"
            dousa = "登録"
        ElseIf actionType = "update" Then
            firstFncStr = "Upd"
            dousa = "更新"
        ElseIf actionType = "select" Then
            firstFncStr = "Sel"
            dousa = "検索"
        End If




        Dim str As String = GetFncNotes("DA", dt, AutoCodeDbClass.active_database_dt, firstFncStr, tblKjName, tblEnName, dousa, db_name, table_name, actionType, noteKbn, paramFlg)

        str = str.Replace(conEmabParam, GetEmabParam(dt))
        str = str.Replace(conByvalParamStr, GetByvalParam(dt, ""))

        Return (GetSbDAImports("VS2005") & str.ToString)

        Return str

    End Function

    ''' <summary>
    ''' BC String
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBcFuncString(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String, ByVal actionType As String) As String

        Dim sbByval As New StringBuilder

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)
        Dim tblKjName As String = AutoCodeDbClass.Get_Table_name_jp(table_name)
        Dim tblEnName As String = table_name

        Dim str As String
        str = GetBCStringSelect(dt, AutoCodeDbClass.active_database_dt, tblKjName, tblEnName, "", db_name, table_name, actionType)

        str = str.Replace(conEmabParam, GetEmabParam(dt))
        str = str.Replace(conByvalParamStr, GetByvalParam(dt, ""))

        Return (GetSbBCImports("VS2005") & str.ToString)

        Return str

    End Function





    Public Shared Function GetBCStringSelect(ByVal active_database_dt As DataTable, _
                                                ByVal auto_code_info_datatable As DataTable, _
                                                ByVal dtKjName As String, _
                                                ByVal dtEnglishName As String, _
                                                ByVal VSType As String, _
                                                ByVal db_name As String, _
                                                ByVal table_name As String, _
                                                ByVal actionType As String) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim bcSb As New StringBuilder
        'BC
        bcSb.AppendLine("'BCソース作成")

        Dim firstFncStr As String = ""
        Dim dousa As String = ""
        If actionType = "insert" Then
            firstFncStr = "Set"
            dousa = "登録"
        ElseIf actionType = "update" Then
            firstFncStr = "Upd"
            dousa = "更新"
        ElseIf actionType = "select" Then
            firstFncStr = "Get"
            dousa = "検索"
        End If



        bcSb.AppendLine(GetSELECTFunctionTitle(active_database_dt, _
                                               auto_code_info_datatable, _
                                               firstFncStr, _
                                               dtKjName.Replace(conTblNameBunkatu, ""), _
                                               dtEnglishName.Replace(conTblNameBunkatu, ""), _
                                               dousa, _
                                               VSType, _
                                               db_name, _
                                               table_name))




        bcSb.AppendLine(vbtabSuu(1) & "'EMAB障害対応情報の格納処理")
        bcSb.AppendLine(vbtabSuu(1) & "EMAB.AddMethodEntrance(MyClass.GetType.FullName & ""."" & MyMethod.GetCurrentMethod.Name _" & vbNewLine & conEmabParam)

        bcSb.AppendLine("   Using scope As New TransactionScope(TransactionScopeOption.Required)")

        bcSb.Append("       If (return DA.")
        bcSb.Append(GetFunctionName("Sel", dtEnglishName.Replace(conTblNameBunkatu, "")) & "(" & GetEmabParam(active_database_dt, False))
        bcSb.Append("   ) Then " & vbCrLf)

        bcSb.AppendLine("               scope.Complete()")
        bcSb.AppendLine("               Return True")
        bcSb.AppendLine("           ELSE")
        bcSb.AppendLine("               scope.Dispose()")
        bcSb.AppendLine("               Return False")
        bcSb.AppendLine("       End If")
        bcSb.AppendLine("   End Using")
        bcSb.AppendLine("End Function")


        Return bcSb.ToString

    End Function

    Public Shared Function GetSELECTFunctionTitle(ByVal active_database_dt As DataTable, ByVal auto_code_info_datatable As DataTable, _
                                                  ByVal fncFirstStr As String, ByVal dtKjName As String, _
                                                  ByVal dtEnglishName As String, ByVal dousa As String, ByVal VSType As String, _
                                                  ByVal db_name As String, ByVal table_name As String) As String

        Dim sb As New StringBuilder
        Dim rtv As String = ""

        'DA
        sb.AppendLine("''' <summary>")
        sb.AppendLine("''' " & dtKjName & "情報を" & dousa & "取得する")
        sb.AppendLine("''' </summary>")

        'Note部のParam
        sb.Append(GetTitleParam(active_database_dt, auto_code_info_datatable, db_name, table_name))

        sb.AppendLine("''' <returns>" & dtKjName & "情報</returns>")
        sb.AppendLine("''' <remarks></remarks>")
        sb.AppendLine("''' <history>")
        sb.AppendLine("''' <para>" & Now.ToString("yyyy/MM/dd") & " P-99999 ??さん 新規作成 </para>")
        sb.AppendLine("''' </history>")

        sb.AppendLine("Public Function " & GetFunctionName(fncFirstStr, dtEnglishName) & "(" & conByvalParamStr & ") As Data.DataTable")

        Return sb.ToString

    End Function

    'BlukCopy
    Public Shared Function GetVBNETMakeBlukcopy(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)


        Dim sb As New StringBuilder

        Dim sbRtv As New StringBuilder

        For i As Integer = 0 To dt.Rows.Count - 1

            Dim columns_name As String = dt.Rows(i).Item("columns_name").ToString
            Dim columns_type As String = dt.Rows(i).Item("columns_type").ToString
            Dim columns_length As Integer = dt.Rows(i).Item("columns_length").ToString

            Dim sbByval As New StringBuilder

            Dim dtEnglishName As String = columns_name
            Dim dtKjName As String = AutoCodeDbClass.Get_name_jp(columns_name)

            sb.AppendLine("    Private connStr As String = DataAccessManager.Connection")
            sb.AppendLine("''' <summary>")
            sb.AppendLine("''' " & dtKjName & "　登録")
            sb.AppendLine("''' </summary>")

            sb.AppendLine("''' <returns>" & dtKjName & "情報登録結果</returns>")
            sb.AppendLine("''' <remarks></remarks>")
            sb.AppendLine("''' <history>")
            sb.AppendLine("''' <para>" & Now.ToString("yyyy/MM/dd") & " P-99999 ??さん 新規作成 </para>")
            sb.AppendLine("''' </history>")
            sb.AppendLine("Public Function BulkCopy" & dtEnglishName & "(ByVal dt" & dtEnglishName & " As DataTable) As Boolean")
            sb.AppendLine(vbtabSuu(1) & "'EMAB障害対応情報の格納処理")
            sb.AppendLine(vbtabSuu(1) & "EMAB.AddMethodEntrance(MyClass.GetType.FullName & ""."" & MyMethod.GetCurrentMethod.Name,""dt" & dtEnglishName & """")
            sb.AppendLine("")
            sb.AppendLine("        Dim retFlg As Boolean")
            sb.AppendLine("")
            sb.AppendLine("        Dim dataAdatpter As System.Data.SqlClient.SqlDataAdapter = Nothing")
            sb.AppendLine("        Using conn As New SqlClient.SqlConnection(connStr)")
            sb.AppendLine("            conn.Open()")
            sb.AppendLine("")
            sb.AppendLine("            Dim bCopy As New SqlClient.SqlBulkCopy(connStr)")
            sb.AppendLine("            Dim transaction As SqlClient.SqlTransaction")
            sb.AppendLine("            transaction = conn.BeginTransaction()")
            sb.AppendLine("            Try")
            sb.AppendLine("                bCopy.BulkCopyTimeout = 3000")
            sb.AppendLine("                '印刷データテーブル")
            sb.AppendLine("                bCopy.DestinationTableName = """ & dtEnglishName & """")
            sb.AppendLine("                bCopy.WriteToServer(dtPrintData)")
            sb.AppendLine("                retFlg = True")
            sb.AppendLine("                transaction.Commit()")
            sb.AppendLine("            Catch ex As Exception")
            sb.AppendLine("                retFlg = False")
            sb.AppendLine("                transaction.Rollback()")
            sb.AppendLine("            Finally")
            sb.AppendLine("                bCopy.Close()")
            sb.AppendLine("                conn.Close()")
            sb.AppendLine("            End Try")
            sb.AppendLine("        End Using")
            sb.AppendLine("")
            sb.AppendLine("        '戻る値")
            sb.AppendLine("        Return retFlg")
            sb.AppendLine("    End Function")
            sbRtv.AppendLine(sb.ToString)
            sb.Length = 0
        Next

        Return sbRtv.ToString

    End Function



End Class


Public Class AutoCodeSqlServer


    Public Const conByvalParamStr = "@@@byvalParam@@@"
    Public Const conEmabParam = "@@@emabParam@@@"
    Public Const conTblNameBunkatu = "|"

    Public Shared Function vbtabSuu(ByVal suu As Integer) As String
        Dim rt As String = ""
        For i As Integer = 0 To suu
            If i > 0 Then
                rt = rt & "    "
            End If
        Next
        Return rt
    End Function

    Enum ParamType
        SqlParam = 1
        NoParam = 2
    End Enum

    Public Shared Function GetSelect(ByVal active_database_dt As DataTable, _
                                            ByVal auto_code_info_datatable As DataTable, _
                                            ByVal db_name As String, _
                                            ByVal table_name As String) As String
        Dim daSb As New StringBuilder
        daSb.AppendLine(GetSQLParam(active_database_dt, db_name, table_name))




        daSb.AppendLine("SELECT")
        daSb.AppendLine(GetSelectKmStr(active_database_dt, db_name, table_name, True))

        Dim tblNameArr() As String = table_name.Split(conTblNameBunkatu)
        'Dim dtKjNameArr() As String = dtKjName.Split(conTblNameBunkatu)

        For i As Integer = 0 To tblNameArr.Length - 1

            If i = 0 Then
                daSb.AppendLine("FROM " & tblNameArr(i))
            Else
                daSb.AppendLine(vbtabSuu(1) & "LEFT JOIN " & tblNameArr(i))
                daSb.AppendLine(vbtabSuu(1) & " ON  " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")")
                daSb.AppendLine(vbtabSuu(1) & " AND " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")")
            End If

        Next
        daSb.AppendLine("WHERE")
        daSb.AppendLine(GetSelectWhereKmStr(active_database_dt, db_name, table_name, True))

        Return daSb.ToString

    End Function


    Public Shared Function GetUpdate(ByVal active_database_dt As DataTable, _
                                        ByVal auto_code_info_datatable As DataTable, _
                                        ByVal db_name As String, _
                                        ByVal table_name As String) As String
        Dim daSb As New StringBuilder
        ' daSb.AppendLine(GetSQLParam(active_database_dt, db_name, table_name))




        daSb.AppendLine("UPDATE " & table_name & " SET ")
        daSb.AppendLine(GetUpdStr(active_database_dt, db_name, table_name, True))
        daSb.AppendLine("WHERE")
        daSb.AppendLine(GetSelectWhereKmStr(active_database_dt, db_name, table_name, True))
        '  daSb.AppendLine(GetSelectKmStr(active_database_dt, db_name, table_name, True))

        'Dim tblNameArr() As String = table_name.Split(conTblNameBunkatu)
        ''Dim dtKjNameArr() As String = dtKjName.Split(conTblNameBunkatu)

        'For i As Integer = 0 To tblNameArr.Length - 1

        '    If i = 0 Then
        '        daSb.AppendLine("FROM " & tblNameArr(i))
        '    Else
        '        daSb.AppendLine(vbtabSuu(1) & "LEFT JOIN " & tblNameArr(i))
        '        daSb.AppendLine(vbtabSuu(1) & " ON  " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")")
        '        daSb.AppendLine(vbtabSuu(1) & " AND " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")")
        '    End If

        'Next
        'daSb.AppendLine("WHERE")
        'daSb.AppendLine(GetSelectWhereKmStr(active_database_dt, db_name, table_name, True))

        Return daSb.ToString

    End Function

    Public Shared Function GetINSERT(ByVal active_database_dt As DataTable, _
                                            ByVal auto_code_info_datatable As DataTable, _
                                            ByVal db_name As String, _
                                            ByVal table_name As String) As String
        Dim daSb As New StringBuilder
        'daSb.AppendLine(GetSQLParam(active_database_dt, db_name, table_name))


        daSb.AppendLine("INSERT INTO " & table_name)
        daSb.AppendLine("(")
        daSb.AppendLine(GetInsKmStr(active_database_dt, db_name, table_name, True))
        daSb.AppendLine(") VALUES (")

        'Dim dtKjNameArr() As String = dtKjName.Split(conTblNameBunkatu)
        daSb.AppendLine(GetSQLInsertValues(active_database_dt, db_name, table_name))
        daSb.AppendLine(")")
        'Dim tblNameArr() As String = table_name.Split(conTblNameBunkatu)
        'For i As Integer = 0 To tblNameArr.Length - 1

        '    If i = 0 Then
        '        daSb.AppendLine("FROM " & tblNameArr(i))
        '    Else
        '        daSb.AppendLine(vbtabSuu(1) & "LEFT JOIN " & tblNameArr(i))
        '        daSb.AppendLine(vbtabSuu(1) & " ON  " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")")
        '        daSb.AppendLine(vbtabSuu(1) & " AND " & tblNameArr(i) & ".XXXX=XXXX.XXXX"")")
        '    End If

        'Next
        'daSb.AppendLine("WHERE")
        'daSb.AppendLine(GetSelectWhereKmStr(active_database_dt, db_name, table_name, True))

        Return daSb.ToString

    End Function


    ''' <summary>
    ''' VB　SQL PARAM
    ''' </summary>
    ''' <param name="active_database_dt "></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSQLInsertValues(ByVal active_database_dt As DataTable, ByVal db_name As String, ByVal table_name As String) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sbParam As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1

            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = active_database_dt.Rows(j).Item("columns_length").ToString
            If j = 0 Then
                sbParam.Append(vbtabSuu(1) & vbtabSuu(1) & "  @" & columns_name)
            Else
                sbParam.Append(vbtabSuu(1) & " ," & vbtabSuu(1) & "@" & columns_name)
            End If

            sbParam.AppendLine(AddNote(sbParam.ToString, AutoCodeDbClass.Get_name_jp(columns_name) & columns_type & "(" & columns_length & ")", "--"))

            'sbParam.AppendLine(vbNewLine)
        Next
        Return sbParam.ToString
    End Function

    ''' <summary>
    ''' VB　SQL PARAM
    ''' </summary>
    ''' <param name="active_database_dt "></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSQLParam(ByVal active_database_dt As DataTable, ByVal db_name As String, ByVal table_name As String) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sbParam As New StringBuilder
        sbParam.AppendLine("--バラメタ格納")


        For j As Integer = 0 To active_database_dt.Rows.Count - 1

            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = active_database_dt.Rows(j).Item("columns_length").ToString

            sbParam.AppendLine("Declare @" & columns_name & " " & columns_type & "(" & columns_length & ")")

        Next
        Return sbParam.ToString
    End Function


    ''' <summary>
    ''' 变量名生成
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MakeStrFirstCharUpper(ByVal str As String) As String

        Dim sb As New System.Text.StringBuilder
        For i As Integer = 0 To str.Length - 1

            Dim chr As String = str.Substring(i, 1)

            Dim chrPre As String = ""

            If i > 0 Then
                chrPre = str.Substring(i - 1, 1)
            End If

            If chrPre <> "_" Then
                chr = (chr.ToLower)
            Else
                chr = (chr.ToUpper)
            End If

            If chr <> "_" Then
                sb.Append(chr)
            End If

        Next

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' 变量类型
    ''' </summary>
    ''' <param name="Dbtype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypeFromDBType(ByVal Dbtype As String) As String

        If "varchar,char,datetime,datetime2,nvarchar,nchar,text".IndexOf(Dbtype) >= 0 Then
            Return "String"

        ElseIf "int,tinyint".IndexOf(Dbtype) >= 0 Then
            Return "Integer"

        ElseIf "numeric,float,money,smallmoney,decimal".IndexOf(Dbtype) >= 0 Then
            Return "Decimal"
        Else
            Return Dbtype
        End If

        Return ""

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="active_database_dt"></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <param name="noteKbn"></param>
    ''' <param name="ParamFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSelectWhereKmStr(ByVal active_database_dt As DataTable, _
                                               ByVal db_name As String, _
                                               ByVal table_name As String, _
                                               ByVal noteKbn As Boolean) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1


            'Byval と EMABの PARAM
            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString


            'Byval と EMABの PARAM



            If j > 0 Then
                sb.Append(vbtabSuu(1) & " AND")
            Else
                sb.Append(vbtabSuu(1) & "    ")
            End If
            sb.Append(vbtabSuu(1) & table_name)
            sb.Append("." & columns_name)
            sb.Append(" = ")
            sb.Append("@" & columns_name)

            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "--", 40))
            End If

            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="active_database_dt"></param>
    ''' <param name="db_name"></param>
    ''' <param name="table_name"></param>
    ''' <param name="noteKbn"></param>
    ''' <param name="ParamFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUpdStr(ByVal active_database_dt As DataTable, _
                                               ByVal db_name As String, _
                                               ByVal table_name As String, _
                                               ByVal noteKbn As Boolean) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder
        For j As Integer = 0 To active_database_dt.Rows.Count - 1


            'Byval と EMABの PARAM
            Dim columns_name As String = active_database_dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = active_database_dt.Rows(j).Item("columns_type").ToString


            'Byval と EMABの PARAM



            If j > 0 Then
                sb.Append(vbtabSuu(1) & " ,")
            Else
                sb.Append(vbtabSuu(1) & " ")
            End If
            'sb.Append(vbtabSuu(1) & table_name)
            sb.Append(vbtabSuu(1) & columns_name)
            sb.Append(" = ")
            sb.Append("@" & columns_name)

            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "--", 40))
            End If

            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function

    Public Shared Function GetSelectKmStr(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String, Optional ByVal noteKbn As Boolean = True) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder

        For j As Integer = 0 To dt.Rows.Count - 1

            Dim columns_name As String = dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = dt.Rows(j).Item("columns_length").ToString

            'Byval と EMABの PARAM
            If j > 0 Then
                sb.Append(vbtabSuu(1) & " ,")
            Else
                sb.Append(vbtabSuu(1) & "  ")
            End If
            sb.Append(vbtabSuu(1) & dt.Rows(j).Item("table_name"))
            sb.Append("." & dt.Rows(j).Item("columns_name"))
            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "--"))
            End If
            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function


    Public Shared Function GetInsKmStr(ByVal dt As DataTable, ByVal db_name As String, ByVal table_name As String, Optional ByVal noteKbn As Boolean = True) As String

        Dim AutoCodeDbClass As New AutoCodeDbClass(db_name, table_name)

        Dim sb As New StringBuilder

        For j As Integer = 0 To dt.Rows.Count - 1

            Dim columns_name As String = dt.Rows(j).Item("columns_name").ToString
            Dim columns_type As String = dt.Rows(j).Item("columns_type").ToString
            Dim columns_length As Integer = dt.Rows(j).Item("columns_length").ToString

            'Byval と EMABの PARAM
            If j > 0 Then
                sb.Append(vbtabSuu(1) & " ,")
            Else
                sb.Append(vbtabSuu(1) & "  ")
            End If

            sb.Append(vbtabSuu(1) & dt.Rows(j).Item("columns_name"))
            If noteKbn Then
                sb.Append(AddNote(sb.ToString, AutoCodeDbClass.Get_name_jp(columns_name), "--"))
            End If
            sb.Append(vbNewLine)
        Next
        Return sb.ToString
    End Function


    ''' <summary>
    ''' 固定LongのString作成
    ''' </summary>
    ''' <param name="inStr"></param>
    ''' <param name="note"></param>
    ''' <param name="noteSign"></param>
    ''' <param name="bunkatuLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AddNote(ByVal inStr As String, ByVal note As String, Optional ByVal noteSign As String = "--", Optional ByVal bunkatuLength As Integer = 64) As String

        Dim arr() As String = inStr.Split(vbNewLine)
        Dim str As String = RTrim(arr(arr.Length - 1)).Replace(vbCr, "").Replace(vbLf, "")

        Dim i As Integer
        Dim length As String = 0
        Dim s As String
        For i = 1 To Len(str)
            s = Mid(str, i, 1)

            If s = vbTab Then
                length += 4
            ElseIf Asc(s) >= 0 Then
                length += 1
            ElseIf Asc(s) < 0 Then
                length += 2
            Else
                length += Len(s)
            End If
        Next

        Dim modLenth As Integer = (length Mod bunkatuLength)

        If modLenth = 0 Then
            Return noteSign & note
        Else
            Dim midlength As Integer = bunkatuLength - (length Mod bunkatuLength)
            Return "".ToString.PadLeft(midlength) & noteSign & " " & note
        End If


    End Function

End Class

Public Class AutoCodeDbClass

    Public active_database_dt As DataTable


    Sub New(ByVal db_name As String, ByVal table_name As String)
        active_database_dt = GetEnKMDATA(db_name, table_name)
    End Sub

    Public Function Get_Table_name_jp(ByVal table_en As String) As String
        Dim drs() As DataRow = active_database_dt.Select("table_en='" & table_en & "'")
        If drs.Count > 0 Then
            Get_Table_name_jp = drs(0).Item("table_jp")
        Else
            Get_Table_name_jp = ""
        End If

        Return Get_Table_name_jp

    End Function


    Public Function Get_name_jp(ByVal columns_name As String) As String
        Dim drs() As DataRow = active_database_dt.Select("item_en='" & columns_name & "'")
        If drs.Count > 0 Then
            Get_name_jp = drs(0).Item("item_jp")
        Else
            Get_name_jp = columns_name
        End If

        Return Get_name_jp

    End Function

    Public Function Get_name_en(ByVal columns_name As String) As String
        Dim drs() As DataRow = active_database_dt.Select("item_en='" & columns_name & "'")
        If drs.Count > 0 Then
            Get_name_en = drs(0).Item("item_en")
        Else
            Get_name_en = ""
        End If

        Return Get_name_en

    End Function

    ''' <summary>
    ''' 获得项目Datatable
    ''' </summary>
    ''' <param name="db_name"></param>
    ''' <param name="table_en"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEnKMDATA(ByVal db_name As String, ByVal table_en As String) As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("  SELECT distinct * FROM (")
            .AppendLine("  SELECT")
            .AppendLine("	* ")
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[table_en] = '" & table_en & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            '.AppendLine(" AND ([item_jp] = '" & jp & "' OR [item_en] = '" & jp & "')")
            .AppendLine("  ) a ")

        End With

        Dim dt As Data.DataTable
        Dim msg As String
        MSSQL.SEL(COMMON.Init.connCom, sb.ToString, dt:=dt, msg:=msg)
        Return dt
    End Function









End Class

