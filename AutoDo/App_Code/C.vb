Imports Microsoft.VisualBasic
Imports System.Data
Imports COMMON
Imports System.Collections.Generic

Public Class Client

    Private _login_user As String
    Public Property login_user As String
        Get
            Return _login_user
        End Get
        Set(ByVal value As String)
            _login_user = value
        End Set
    End Property

    Private _login_user_id As String
    Public Property login_user_id As String
        Get
            Return _login_user_id
        End Get
        Set(ByVal value As String)
            _login_user_id = value
        End Set
    End Property
End Class


Public Class C
    Public Shared Function IsNullEmpty(ByVal v As Object) As String
        If v Is DBNull.Value OrElse v Is Nothing Then
            Return ""
        Else
            Return v
        End If
    End Function


    Public Shared sqlGuanjianzi As String = "'IN,ADD,EXCEPT,PERCENT,ALL,EXEC,PLAN,ALTER,EXECUTE,PRECISION,AND,EXISTS,PRIMARY,ANY,EXIT,PRINT,AS,FETCH,PROC,ASC,FILE,PROCEDURE,AUTHORIZATION,FILLFACTOR,PUBLIC,BACKUP,FOR,RAISERROR,BEGIN,FOREIGN,READ,BETWEEN,FREETEXT,READTEXT,BREAK,FREETEXTTABLE,RECONFIGURE,BROWSE,FROM,REFERENCES,BULK,FULL,REPLICATION,BY,FUNCTION,RESTORE,CASCADE,GOTO,RESTRICT,CASE,GRANT,RETURN,CHECK,GROUP,REVOKE,CHECKPOINT,HAVING,RIGHT,CLOSE,HOLDLOCK,ROLLBACK,CLUSTERED,IDENTITY,ROWCOUNT,COALESCE,IDENTITY_INSERT,ROWGUIDCOL,COLLATE,IDENTITYCOL,RULE,COLUMN,IF,SAVE,COMMIT,IN,SCHEMA,COMPUTE,INDEX,SELECT,CONSTRAINT,INNER,SESSION_USER,CONTAINS,INSERT,SET,CONTAINSTABLE,INTERSECT,SETUSER,CONTINUE,INTO,SHUTDOWN,CONVERT,IS,SOME,CREATE,JOIN,STATISTICS,CROSS,KEY,SYSTEM_USER,CURRENT,KILL,TABLE,CURRENT_DATE,LEFT,TEXTSIZE,CURRENT_TIME,LIKE,THEN,CURRENT_TIMESTAMP,LINENO,TO,CURRENT_USER,LOAD,TOP,CURSOR,NATIONAL,TRAN,DATABASE,NOCHECK,TRANSACTION,DBCC,NONCLUSTERED,TRIGGER,DEALLOCATE,NOT,TRUNCATE,DECLARE,NULL,TSEQUAL,DEFAULT,NULLIF,UNION,DELETE,OF,UNIQUE,DENY,OFF,UPDATE,DESC,OFFSETS,UPDATETEXT,DISK,ON,USE,DISTINCT,OPEN,USER,DISTRIBUTED,OPENDATASOURCE,VALUES,DOUBLE,OPENQUERY,VARYING,DROP,OPENROWSET,VIEW,DUMMY,OPENXML,WAITFOR,DUMP,OPTION,WHEN,ELSE,OR,WHERE,END,ORDER,WHILE,ERRLVL,OUTER,WITH,ESCAPE,OVER,WRITETEXT,"

    Public Shared conn As String = COMMON.Init.connCom

    Public Shared Function Client(ByVal page As Page) As Client
        Dim ci As New Client
        ci.login_user = page.Request.ServerVariables("LOGON_USER")

        ci.login_user_id = ci.login_user.Split("\")(ci.login_user.Split("\").Length - 1)

        Return ci
    End Function


    Public Shared Sub SMsg(ByVal page As System.Web.UI.Page, ByVal msg As String, Optional ByVal id As String = "")

        Dim csScript As New StringBuilder

        With csScript
            '.AppendLine("<script language=""javascript"" type=""text/javascript"">")
            .AppendLine("        $(document).ready(function () {")
            If id = "" Then
                .AppendLine("                    $alert(""Please input user password"",null );")
            Else
                .AppendLine("                    $alert(""Please input user password"", $(""" & id & """));")
            End If

            .AppendLine("        });")
            '.AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        page.ClientScript.RegisterStartupScript(page.GetType(), "SMsg", csScript.ToString, True)

    End Sub

    Public Shared Sub Msg(ByVal page As System.Web.UI.Page, ByVal msg As String)

        Dim csScript As New StringBuilder

        With csScript
            '.AppendLine("<script language=""javascript"" type=""text/javascript"">")
            .AppendLine("        $(document).ready(function () {")
            .AppendLine("                    alert(""" & msg & """);")


            .AppendLine("        });")
            '.AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        page.ClientScript.RegisterStartupScript(page.GetType(), "SMsg", csScript.ToString, True)

    End Sub


    Public Shared Sub BindListBox(ByRef ddl As ListBox, ByVal sql As String)

        Dim msSql As New CMsSql()
        Dim dt As DataTable = msSql.ExecSelect(sql)
        ddl.Items.Clear()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddl.Items.Add(dt.Rows(i).Item(1).ToString.Trim)
                ddl.Items(ddl.Items.Count - 1).Value = dt.Rows(i).Item(0).ToString.Trim
            Next
        End If

    End Sub

    Public Shared Sub BindDropDownList(ByRef ddl As DropDownList, ByVal sql As String)

        Dim msSql As New CMsSql()
        Dim dt As DataTable = msSql.ExecSelect(sql)
        ddl.Items.Clear()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddl.Items.Add(dt.Rows(i).Item(1).ToString.Trim)
                ddl.Items(ddl.Items.Count - 1).Value = dt.Rows(i).Item(0).ToString.Trim
            Next
        End If

    End Sub

    Public Shared Function GetArrBySplit(ByVal inArr As String(), ByVal sptor As String) As String()

        Dim lst As New List(Of String)

        For i As Integer = 0 To inArr.Length - 1
            Dim str() As String = inArr(i).Split(sptor)

            For j As Integer = 0 To str.Length - 1
                lst.Add(str(j))
                If j <> str.Length - 1 Then
                    lst.Add(sptor)
                End If
            Next
        Next
        Return lst.ToArray
    End Function

    Public Shared Function GetArrBySplit(ByVal inArr As String, ByVal sptor As String) As String()

        Dim lst As New List(Of String)


        Dim str() As String = inArr.Split(sptor)

        For j As Integer = 0 To str.Length - 1
            lst.Add(str(j))
            If j <> str.Length - 1 Then
                lst.Add(sptor)
            End If
        Next

        Return lst.ToArray
    End Function

    Public Shared Function GetTablesList(ByVal str As String) As String
        Dim tbls As String = str.Replace(vbCr, " ").Replace(vbLf, " ")
        For i As Integer = 0 To 100
            tbls = tbls.Replace("  ", " ")
        Next
        Dim arrTbl() As String = C.GetArrBySplit(tbls, " ")
        arrTbl = C.GetArrBySplit(arrTbl, ",")

        Dim lstTbl As New StringBuilder
        For i As Integer = 0 To arrTbl.Length - 1
            arrTbl(i) = arrTbl(i).Trim
            If arrTbl(i) <> "" Then
                If lstTbl.Length <> 0 Then
                    lstTbl.Append(",'" & arrTbl(i) & "','" & arrTbl(i) & "テーブル'")
                Else
                    lstTbl.Append("'" & arrTbl(i) & "','" & arrTbl(i) & "テーブル'")
                End If

            End If
        Next


        Return lstTbl.ToString

    End Function






    'Public Shared Function MakeStrFirstCharUpper(ByVal str As String) As String

    '    Dim sb As New System.Text.StringBuilder
    '    For i As Integer = 0 To str.Length - 1

    '        Dim chr As String = str.Substring(i, 1)

    '        Dim chrPre As String = ""

    '        If i > 0 Then
    '            chrPre = str.Substring(i - 1, 1)
    '        End If

    '        If chrPre <> "_" Then
    '            chr = (chr.ToLower)
    '        Else
    '            chr = (chr.ToUpper)
    '        End If

    '        If chr <> "_" Then
    '            sb.Append(chr)
    '        End If

    '    Next

    '    Return (sb.ToString)

    'End Function

    Public Shared Function GetKmItem(ByVal inText As String) As String()

        Dim arr() As String = C.GetArrBySplit(inText, vbLf)
        arr = C.GetArrBySplit(arr, vbCr)
        arr = C.GetArrBySplit(arr, vbTab)
        arr = C.GetArrBySplit(arr, ",")
        arr = C.GetArrBySplit(arr, "[")
        arr = C.GetArrBySplit(arr, "]")
        arr = C.GetArrBySplit(arr, "!=")
        arr = C.GetArrBySplit(arr, "<>")
        arr = C.GetArrBySplit(arr, "=")
        arr = C.GetArrBySplit(arr, "(")
        arr = C.GetArrBySplit(arr, ")")
        arr = C.GetArrBySplit(arr, "'")
        arr = C.GetArrBySplit(arr, ".")
        arr = C.GetArrBySplit(arr, " ")
        arr = C.GetArrBySplit(arr, " ")
        Return arr
    End Function

    Public Shared Function GetNetControls(ByVal kmDt As DataTable) As String

        Dim sb, setValueSb As New StringBuilder
        Dim type As String = ""
        Dim kmBind As Boolean = True

        Dim oldv As String = ""

        For j As Integer = 0 To kmDt.Rows.Count - 1

            If oldv <> kmDt.Rows(j).Item("item_en") Then


                Dim ketaSuu As Integer = GetKetaSuuFromTypeAndKeta(kmDt.Rows(j).Item("item_type"), kmDt.Rows(j).Item("item_keta"))
                Dim kmIsNumberType As Boolean = IsNumberType(kmDt.Rows(j).Item("item_type").ToString.ToLower)
                sb.Append(vbtabSuu(1) & "<!--" & kmDt.Rows(j).Item("item_jp") & " -->" & vbNewLine)

                setValueSb.Append(vbtabSuu(1) & "'" & kmDt.Rows(j).Item("item_jp") & "" & vbNewLine)



                If type = "textbox" OrElse type = "" OrElse C.GetObjValue(kmDt.Rows(j).Item("tbx")) = "1" Then
                    sb.Append(vbtabSuu(1) & "<asp:TextBox ")
                    sb.Append("ID=""" & C.MakeStrFirstCharUpper("tbx_" & kmDt.Rows(j).Item("item_en")) & """ ")
                    sb.Append("runat=""server"" ")
                    sb.Append("TabIndex="""" ")
                    sb.Append("AutoCompleteType=""Disabled"" ")
                    sb.Append("MaxLength=""" & ketaSuu & """ ")
                    sb.Append("CssClass=""TextBox"" ")
                    sb.Append("style = "" ")
                    If kmIsNumberType Then
                        sb.Append(vbtabSuu(1) & "ime-mode:disabled;")
                    End If
                    sb.Append("width:" & ketaSuu * 10 & "px;")
                    sb.Append(""" ")
                    If kmBind Then
                        sb.Append("Text='<%#Eval(""" & kmDt.Rows(j).Item("item_en") & """).ToString")
                    Else
                        sb.Append("Text=""""")
                    End If

                    'If C.GetObjValue(kmDt.Rows(j).Item("tbx")) = "1" Then
                    '    sb.Append(" OnClientClick=""""")


                    '    sb.Append(""""" ")
                    'End If


                    sb.Append("></asp:TextBox>")
                    sb.Append(vbNewLine)

                    setValueSb.AppendLine(vbtabSuu(1) & "me." & C.MakeStrFirstCharUpper("tbx_" & kmDt.Rows(j).Item("item_en")) & ".Text = IsNullEmpty(dt.Rows(idx).item(""" & kmDt.Rows(j).Item("item_en").ToString & """).toString())")

                End If

                If type = "label" OrElse type = "" OrElse C.GetObjValue(kmDt.Rows(j).Item("lbl")) = "1" Then
                    sb.Append(vbtabSuu(1) & "<asp:Label ")
                    sb.Append("ID=""" & C.MakeStrFirstCharUpper("lbl_" & kmDt.Rows(j).Item("item_en")) & """ ")
                    sb.Append("runat=""server"" ")

                    sb.Append("CssClass=""Label"" ")
                    sb.Append("style = "" ")

                    sb.Append("width:" & ketaSuu * 10 & "px;")
                    sb.Append(""" ")
                    If kmBind Then
                        sb.Append("Text='<%#Eval(""" & kmDt.Rows(j).Item("item_en") & """).ToString")
                    Else
                        sb.Append("Text=""""")
                    End If
                    sb.Append("></asp:Label>")
                    sb.Append(vbNewLine)

                    setValueSb.AppendLine(vbtabSuu(1) & "me." & C.MakeStrFirstCharUpper("lbl_" & kmDt.Rows(j).Item("item_en")) & ".Text = IsNullEmpty(dt.Rows(idx).item(""" & kmDt.Rows(j).Item("item_en").ToString & """).toString())")
                End If

                If type = "hidden" OrElse type = "" OrElse C.GetObjValue(kmDt.Rows(j).Item("hid")) = "1" Then
                    sb.Append(vbtabSuu(1) & "<asp:HiddenField ")
                    sb.Append("ID=""" & C.MakeStrFirstCharUpper("hid_" & kmDt.Rows(j).Item("item_en")) & """ ")
                    sb.Append("runat=""server"" ")
                    If kmBind Then
                        sb.Append("Value='<%#Eval(""" & kmDt.Rows(j).Item("item_en") & """).ToString")
                    Else
                        sb.Append("Value=""""")
                    End If
                    sb.Append("></asp:HiddenField>")
                    sb.Append(vbNewLine)

                    setValueSb.AppendLine(vbtabSuu(1) & "me." & C.MakeStrFirstCharUpper("hid_" & kmDt.Rows(j).Item("item_en")) & ".Value = IsNullEmpty(dt.Rows(idx).item(""" & kmDt.Rows(j).Item("item_en").ToString & """).toString())")

                End If


                If type = "checkbox" OrElse type = "" Then
                    sb.Append(vbtabSuu(1) & "<asp:CheckBox ")
                    sb.Append("ID=""" & C.MakeStrFirstCharUpper("cbx_" & kmDt.Rows(j).Item("item_en")) & """ ")
                    sb.Append("runat=""server"" ")
                    sb.Append("TabIndex="""" ")
                    sb.Append("CssClass=""CheckBox"" ")
                    sb.Append("></asp:CheckBox>")
                    sb.Append(vbNewLine)

                    setValueSb.AppendLine(vbtabSuu(1) & "me." & C.MakeStrFirstCharUpper("cbx_" & kmDt.Rows(j).Item("item_en")) & ".checked = IsNullEmpty(dt.Rows(idx).item(""" & kmDt.Rows(j).Item("item_en").ToString & """).toString())=""1""")

                End If



                If type = "radio" OrElse type = "" Then
                    sb.Append(vbtabSuu(1) & "<asp:RadioButton ")
                    sb.Append("ID=""" & C.MakeStrFirstCharUpper("rdo_" & kmDt.Rows(j).Item("item_en")) & """ ")
                    sb.Append("runat=""server"" ")
                    sb.Append("TabIndex="""" ")
                    sb.Append("CssClass=""Radio"" ")
                    sb.Append("GroupName=""" & C.MakeStrFirstCharUpper("_" & kmDt.Rows(j).Item("item_en")) & """ ")
                    sb.Append("></asp:RadioButton>")
                    sb.Append(vbNewLine)

                    setValueSb.AppendLine(vbtabSuu(1) & "me." & C.MakeStrFirstCharUpper("rdo_" & kmDt.Rows(j).Item("item_en")) & ".selected = IsNullEmpty(dt.Rows(idx).item(""" & kmDt.Rows(j).Item("item_en").ToString & """).toString())=""1""")

                End If

                oldv = kmDt.Rows(j).Item("item_en")
            Else
                oldv = kmDt.Rows(j).Item("item_en")
            End If
        Next

        Return setValueSb.ToString & vbNewLine & sb.ToString

    End Function

    Public Shared Function GetKetaSuuFromTypeAndKeta(ByVal type As String, ByVal ketaSuu As String) As Integer

        Dim length As Integer
        Try
            If "int,tinyint".IndexOf(type.ToLower) >= 0 Then
                length = CInt(ketaSuu)
            ElseIf "numeric".IndexOf(type.ToLower) >= 0 Then
                If ketaSuu.IndexOf(",") < 0 Then
                    length = CInt(ketaSuu) * 10
                Else
                    length = CInt(ketaSuu.Split(",")(0) + 2)
                End If
            ElseIf "datetime".IndexOf(type.ToLower) >= 0 Then
                length = 10
            Else
                length = CInt(ketaSuu)
            End If

            Return length * 10 / 10
        Catch ex As Exception
            Return -1
        End Try

    End Function
    Public Shared Function vbtabSuu(ByVal suu As Integer) As String
        Dim rt As String = ""
        For i As Integer = 0 To suu
            If i > 0 Then
                rt = rt & vbTab
            End If
        Next
        Return rt
    End Function
    Public Shared Function IsNumberType(ByVal type As String) As Boolean
        If "int,tinyint,numeric".IndexOf(type.ToLower) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function


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


    Public Shared Function AddNote(ByVal inStr As String, ByVal note As String, Optional ByVal noteSign As String = "'", Optional ByVal bunkatuLength As Integer = 32) As String

        Dim arr() As String = inStr.Split(vbNewLine)
        Dim str As String = RTrim(arr(arr.Length - 1))

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
            Return "".ToString.PadLeft(midlength) & noteSign & note
        End If

    End Function


    Public Shared Function GetTypeFromDBType(ByVal Dbtype As String) As String

        If "varchar,char,datetime,nvarchar,nchar".IndexOf(Dbtype) >= 0 Then
            Return "String"
        ElseIf "int,tinyint,datetime".IndexOf(Dbtype) >= 0 Then
            Return "Integer"
        ElseIf "numeric,float,money,smallmoney,decimal".IndexOf(Dbtype) >= 0 Then
            Return "Decimal"
        ElseIf "numeric,float,money,smallmoney,decimal".IndexOf(Dbtype) >= 0 Then
            Return Dbtype
        End If

        Return ""

    End Function


    ''' <summary>
    ''' Obj
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetObjValue(ByVal v As Object) As String
        If v Is Nothing OrElse v Is DBNull.Value Then
            Return ""
        Else
            Return v.ToString()
        End If
    End Function

    ''' <summary>
    ''' Obj
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetObjIntValue(ByVal v As Object) As Integer
        If v Is Nothing OrElse v Is DBNull.Value OrElse v.ToString.Trim() = "" Then
            Return 0
        Else
            Return CInt(v)
        End If
    End Function

    Public Shared Function SaveFile(ByVal path As String, ByVal txt As String) As Boolean

        Try
            System.IO.File.WriteAllText(path, txt)

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function





    Public Shared Function CSaveSIryoiu(ByVal edpNo As String, ByVal file_exp As String, ByVal txt As String, ByVal data_source As String, ByVal user_cd As String _
                                , ByVal path As String) As String



        Dim sb As New StringBuilder
        With sb
            .AppendLine("DELETE FROM [auto_code].[dbo].[m_siryoiu] WHERE")
            .AppendLine("edp_no = '" & edpNo & "'")
            .AppendLine("AND file_exp = '" & file_exp & "'")

            .AppendLine("INSERT INTO [auto_code].[dbo].[m_siryoiu]")
            .AppendLine("SELECT")
            .AppendLine("'" & edpNo & "'")
            .AppendLine(",N'" & file_exp & "'")
            .AppendLine(",N'" & txt & "'")
            .AppendLine(",'" & user_cd & "'")
            .AppendLine(",'SQL'")
            .AppendLine(",getdate()")

            .AppendLine("INSERT INTO [auto_code].[dbo].[m_siryoiu_rireki]")
            .AppendLine("SELECT")
            .AppendLine("'" & edpNo & "'")
            .AppendLine(",N'" & file_exp & "'")
            .AppendLine(",N'" & txt & "'")
            .AppendLine(",'" & user_cd & "'")
            .AppendLine(",'SQL'")
            .AppendLine(",getdate()")
        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
            'C.Msg(Page, "OK")
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function


    Public Shared Function CDelSIryoiu(ByVal edpNo As String, ByVal file_exp As String) As String

        Dim sb As New StringBuilder
        With sb
            .AppendLine("DELETE FROM [auto_code].[dbo].[m_siryoiu] WHERE")
            .AppendLine("edp_no = '" & edpNo & "'")
            .AppendLine("AND file_exp = '" & file_exp & "'")
        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
            'C.Msg(Page, "OK")
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function





    Public Shared Function CSaveSiryouTrue(ByVal edpNo As String _
                                           , ByVal group_nm As String _
                                           , ByVal file_nm As String _
                                           , ByVal type As String _
                                           , ByVal txt As String _
                                           , ByVal user_cd As String _
                                           , ByVal share_type As String _
                                           , Optional ByVal DelOnly As Boolean = False _
                                           , Optional ByVal RirekiUpd As Boolean = True) As String

        Dim sb As New StringBuilder
        With sb


            .AppendLine("DELETE FROM [auto_code].[dbo].[m_siryou] WHERE")
            .AppendLine("   edp_no = '" & edpNo & "'")
            .AppendLine("AND group_nm = '" & group_nm & "'")
            .AppendLine("AND file_nm = '" & file_nm & "'")

            If Not DelOnly Then
                .AppendLine("INSERT INTO [auto_code].[dbo].[m_siryou]")
                .AppendLine("SELECT")
                .AppendLine("'" & edpNo & "'")
                .AppendLine(",N'" & group_nm & "'")
                .AppendLine(",N'" & file_nm & "'")
                .AppendLine(",N'" & txt & "'")
                .AppendLine(",N'" & user_cd & "'")
                .AppendLine(",N'" & type & "'")
                .AppendLine(",N'" & share_type & "'")
                .AppendLine(",getdate()")
            End If

            If RirekiUpd Then
                .AppendLine("INSERT INTO [auto_code].[dbo].[m_siryou_rireki]")
                .AppendLine("SELECT")
                .AppendLine("'" & edpNo & "'")
                .AppendLine(",N'" & group_nm & "'")
                .AppendLine(",N'" & file_nm & "'")
                .AppendLine(",N'" & txt & "'")
                .AppendLine(",N'" & user_cd & "'")
                .AppendLine(",N'" & type & "'")
                .AppendLine(",N'" & share_type & "'")
                .AppendLine(",getdate()")

            End If

        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
            'C.Msg(Page, "OK")
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function



    Public Shared Function ChangeGroupName(ByVal edpNo As String _
                                           , ByVal old_group_nm As String _
                                           , ByVal new_group_nm As String) As String

        Dim sb As New StringBuilder
        With sb


            .AppendLine("UPDATE [auto_code].[dbo].[m_siryou] ")

            .AppendLine("SET group_nm = '" & new_group_nm & "'")

            .AppendLine("WHERE")
            .AppendLine("   edp_no = '" & edpNo & "'")
            .AppendLine("AND group_nm = '" & old_group_nm & "'")



        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
            'C.Msg(Page, "OK")
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function


    Public Shared Function DelIns_m_edp(ByVal edpNo As String _
                                           , ByVal edp_mei As String _
                                           , ByVal edp_exp As String) As String

        Dim sb As New StringBuilder
        With sb
            .AppendLine("DELETE FROM m_edp WHERE")
            .AppendLine("   edp_no = '" & edpNo & "'")

            .AppendLine("INSERT INTO m_edp ")
            .AppendLine("SELECT")
            .AppendLine("'" & edpNo & "'")
            .AppendLine(",N'" & edp_mei & "'")
            .AppendLine(",N'" & edp_exp & "'")

        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function


    Public Shared Function DelIns_m_db_info(ByVal data_source As String _
                                       , ByVal db_name As String _
                                       , ByVal db_type As String _
                                       , ByVal db_user_id As String _
                                       , ByVal db_password As String _
                                       , ByVal db_enlist As String _
                                       , ByVal db_conn As String _
                                       , ByVal db_exp As String) As String

        Dim sb As New StringBuilder
        With sb
            .AppendLine("DELETE FROM m_db_info WHERE")
            .AppendLine("   data_source = '" & data_source & "'")
            .AppendLine("AND   db_name = '" & db_name & "'")

            .AppendLine("INSERT INTO m_db_info ")
            .AppendLine("SELECT")
            .AppendLine("'N" & data_source & "'")
            .AppendLine("'N" & db_name & "'")
            .AppendLine("'N" & db_type & "'")
            .AppendLine("'N" & db_user_id & "'")
            .AppendLine("'N" & db_password & "'")
            .AppendLine("'N" & db_enlist & "'")
            .AppendLine("'N" & db_conn & "'")
            .AppendLine("'N" & db_exp & "'")

        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function


    Public Shared Function DelIns_m_main_use_table(ByVal user_id As String _
                                               , ByVal edp_no As String _
                                               , ByVal db_conn As String _
                                               , ByVal table_ens As String) As String

        Dim sb As New StringBuilder
        With sb
            .AppendLine("DELETE FROM m_main_use_table WHERE")
            .AppendLine("   user_id = '" & user_id & "'")
            .AppendLine("  AND edp_no = '" & edp_no & "'")
            .AppendLine("  AND db_conn = '" & db_conn & "'")

            .AppendLine("INSERT INTO m_main_use_table ")
            .AppendLine("SELECT")
            .AppendLine("'" & user_id & "'")
            .AppendLine(",N'" & edp_no & "'")
            .AppendLine(",N'" & db_conn & "'")
            .AppendLine(",N'" & table_ens & "'")

        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function





    Public Shared Function CSaveJobKinds(ByVal UserNM As String _
                                           , ByVal JobEdp As String _
                                           , ByVal tbxJobSerPath As String _
                                           , ByVal tbxJobClientPath As String _
                                           , ByVal tbxJobBackupPath As String) As String

        Dim sb As New StringBuilder
        With sb


            .AppendLine("DELETE FROM m_job_kinds WHERE")
            .AppendLine("   [user_id] = '" & UserNM & "'")
            .AppendLine("AND [job_edp] = '" & JobEdp & "'")

            .AppendLine("INSERT INTO m_job_kinds")
            .AppendLine("SELECT")
            .AppendLine(" N'" & UserNM & "'")
            .AppendLine(",N'" & JobEdp & "'")
            .AppendLine(",N'" & tbxJobSerPath & "'")
            .AppendLine(",N'" & tbxJobClientPath & "'")
            .AppendLine(",N'" & tbxJobBackupPath & "'")

        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
            'C.Msg(Page, "OK")
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function


    Public Shared Function CDelJobKinds(ByVal UserNM As String _
                                       , ByVal JobEdp As String) As String

        Dim sb As New StringBuilder
        With sb


            .AppendLine("DELETE FROM m_job_kinds WHERE")
            .AppendLine("   [user_id] = '" & UserNM & "'")
            .AppendLine("AND [job_edp] = '" & JobEdp & "'")

        End With

        Dim MSSQL As New MSSQL
        MSSQL.ExecuteNonQuery(sb.ToString)
        If MSSQL.Result Then
            MSSQL.CloseCommit()
            'C.Msg(Page, "OK")
        Else
            MSSQL.CloseRollback()
            Return MSSQL.errMsg
        End If
        MSSQL.Close()

        Return ""
    End Function

End Class
