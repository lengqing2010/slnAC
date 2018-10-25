Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text
Imports System.IO
Public Class CAutoMKPage

    Public FilePath As String
    Public FileExtension As String
    Public FileName As String
    Public FileNameNoEx As String
    Public DirectoryName As String
    Public TableNameEnglish As String


    Sub New(ByVal filePath As String, ByVal tableNmE As String)
        Me.FilePath = filePath
        FileExtension = System.IO.Path.GetExtension(filePath) '获取扩展名 
        FileName = System.IO.Path.GetFileName(filePath) '获取文件名  
        DirectoryName = System.IO.Path.GetDirectoryName(filePath) '获取文件夹 
        FileNameNoEx = FileName.Split(".")(0)
        TableNameEnglish = tableNmE
    End Sub


    Public Function MakeAspxPage(ByVal acTableData As DataTable, ByVal mTableData As DataTable, ByVal AutoCodeDbClass As AutoCodeDbClass) As String

        Dim t As System.IO.StreamWriter = New System.IO.StreamWriter(FilePath, False, System.Text.Encoding.UTF8)
        t.Write(GetAspxPageCode(acTableData, mTableData, AutoCodeDbClass))
        t.Close()



        Dim t2 As System.IO.StreamWriter = New System.IO.StreamWriter(FilePath & ".vb", False, System.Text.Encoding.UTF8)
        t2.Write(GetAspxVBPageCode(acTableData, mTableData, AutoCodeDbClass))
        t2.Close()



    End Function


    Public Function GetAspxPageCode(ByVal acTableData As DataTable, ByVal mTableData As DataTable, ByVal AutoCodeDbClass As AutoCodeDbClass) As String



        Dim sb As New StringBuilder
        With sb

            .AppendLine("<%@ Page Language=""VB"" AutoEventWireup=""false"" CodeFile=""" & FileNameNoEx & ".aspx.vb"" Inherits=""" & FileNameNoEx & """ %>")
            .AppendLine("")
            .AppendLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">")
            .AppendLine("")
            .AppendLine("<html xmlns=""http://www.w3.org/1999/xhtml"">")
            .AppendLine("<head runat=""server"">")
            .AppendLine("<link href=""tmp.css"" rel=""stylesheet"" type=""text/css"" />")
            'head
            .AppendLine("    <title></title>")
            .AppendLine("<script language=""javascript"" type=""text/javascript"" src=""js/jquery-1.4.1.min.js""></script>")
            .AppendLine("<script language=""javascript"" type=""text/javascript"" src=""JidouTemp.js""></script>")
            .AppendLine("</head>")
            .AppendLine("<body>")
            'body
            .AppendLine("    <form id=""form1"" runat=""server"">")
            .AppendLine("    <div>")
            .AppendLine("	<asp:Label ID=""lblMsg"" runat=""server"" ForeColor=""Red""></asp:Label>")


            Dim tableWidth As Integer = 0
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                Dim columns_width As Integer = GetWidthByColumnLength(columns_length)
                tableWidth += (columns_width + 10) + 2
            Next



            'TITLE
            .AppendLine("<table class='ms_title' style=""width:" & tableWidth & "px"" cellpadding=""0"" cellspacing=""0"">")
            .AppendLine("   <tr>")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                Dim columns_width As Integer = GetWidthByColumnLength(columns_length)
                Dim style_width As String = ""
                If i < acTableData.Rows.Count - 1 Then
                    style_width = "width:" & (columns_width + 10) & "px;"
                End If
                .AppendLine("       <td style=""" & style_width & """>")
                .AppendLine("           " & AutoCodeDbClass.Get_name_jp(columns_name) & "")
                .AppendLine("       </td>")
            Next
            .AppendLine("   </tr>")
            '.AppendLine("</table>")


            '.AppendLine("<table  class='table_title_tbx_row' style=""table-layout:fixed;width:" & tableWidth & "px"">")
            .AppendLine("   <tr>")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                Dim columns_width As Integer = GetWidthByColumnLength(columns_length)
                Dim style_width As String = ""
                If i < acTableData.Rows.Count - 1 Then
                    style_width = "width:" & (columns_width + 10) & "px;"
                End If

                Dim pk As Boolean = IsKey(acTableData.Rows(i).Item("pk").ToString)

                Dim clsName As String = "jq_" & columns_name & "_ipt"

                .AppendLine("       <td style=""" & style_width & """>")
                If pk Then
                    .AppendLine("<asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ class=""" & clsName & """ runat=""server"" style=""width:" & (columns_width) & "px;background-color: #FFAA00;""></asp:TextBox>")
                Else
                    .AppendLine("<asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ class=""" & clsName & """ runat=""server"" style=""width:" & (columns_width) & "px;""></asp:TextBox>")
                End If
                .AppendLine("       </td>")

            Next
            .AppendLine("   </tr>")
            .AppendLine("</table>")


            .AppendLine("<div id=""divGvw"" class='jq_ms_div' runat =""server"" style=""overflow:auto ; height:294px;margin-left:0px; width:" & (tableWidth + 20) & "px; margin-top :0px; border-collapse :collapse ;"">")
            .AppendLine("   <asp:GridView CssClass =""jq_ms"" Width=""" & (tableWidth) & "px""  runat=""server"" ID=""gvMs"" EnableTheming=""True"" ShowHeader=""False"" AutoGenerateColumns=""False"" BorderColor=""black"" CellPadding=""0"" CellSpacing =""0"" style="" margin-top :-1px; "" TabIndex=""-1"" >")
            .AppendLine("<Columns>")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                Dim columns_width As Integer = GetWidthByColumnLength(columns_length)
                Dim style_width As String = ""
                If i < acTableData.Rows.Count - 1 Then
                    style_width = "Width=""" & (columns_width + 10) & "px"""
                End If
                Dim pk As Boolean = IsKey(acTableData.Rows(i).Item("pk").ToString)
                Dim clsName As String = "jq_" & columns_name & ""
                .AppendLine("<asp:TemplateField><ItemTemplate ><%#Eval(""" & columns_name & """)%></ItemTemplate><ItemStyle Height=""20px"" " & style_width & " HorizontalAlign=""Left"" CssClass=""" & clsName & """ /></asp:TemplateField>")

            Next
            .AppendLine("</Columns>")
            .AppendLine("   </asp:GridView>")
            .AppendLine("</div>")


            'hid
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                Dim columns_width As Integer = GetWidthByColumnLength(columns_length)
                Dim clsName As String = "jq_" & columns_name & "_ipt"

                '.AppendLine("<asp:HiddenField ID=""hid" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server"" class=""" & clsName & """ />")
                .AppendLine("<asp:TextBox ID=""hid" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server"" class=""" & clsName & """ style="" visibility:hidden;""></asp:TextBox>")
            Next

            '.AppendLine("<table style=""table-layout:fixed;"">")
            '.AppendLine("   <tr class='table_tite'>")
            'For i As Integer = 0 To acTableData.Rows.Count - 1
            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
            '    Dim columns_width As Integer = GetWidthByColumnLength(columns_length)
            '    Dim pk As Boolean = IsKey(acTableData.Rows(i).Item("pk").ToString)
            '    .AppendLine("       <td style=""width:" & (columns_width + 10) & "px;"">")
            '    If pk Then
            '        .AppendLine("                    <asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server"" style=""width:" & (columns_width) & "px;background-color: #FFAA00;""></asp:TextBox>")
            '    Else
            '        .AppendLine("                    <asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server"" style=""width:" & (columns_width) & "px;""></asp:TextBox>")
            '    End If
            '    .AppendLine("       </td>")
            'Next
            '.AppendLine("   </tr>")
            '.AppendLine("</table>")



            .AppendLine("        <asp:Button ID=""btnUpdate"" runat=""server"" Text=""Update"" CssClass=""jq_upd"" />")
            .AppendLine("        <asp:Button ID=""btnInsert"" runat=""server"" Text=""Insert"" CssClass=""jq_ins"" />")
            .AppendLine("        <asp:Button ID=""btnDelete"" runat=""server"" Text=""Delete"" CssClass=""jq_del"" />")
            '.AppendLine("        <asp:GridView ID=""gvMs"" runat=""server""")
            '.AppendLine("        autogenerateselectbutton=""True""")
            '.AppendLine("        >")
            '.AppendLine("            <SelectedRowStyle BackColor=""#FFFF99"" />")
            '.AppendLine("        </asp:GridView>")
            .AppendLine("    </div>")
            .AppendLine("    </form>")
            .AppendLine("</body>")
            .AppendLine("</html>")

        End With


        Return sb.ToString

    End Function

    Public Function GetWidthByColumnLength(ByVal length As String) As Integer
        Dim rtv As Integer
        length = Trim(length)
        If length = "" Then
            rtv = 100
        Else
            rtv = CInt(CInt(length) * 1.6)
        End If

        If rtv > 200 Then
            Return 200
        ElseIf rtv < 20 Then
            Return 20
        End If

        Return rtv

    End Function


    Public Function IsKey(ByVal key As String) As Boolean
        If key <> "" AndAlso key <> "0" Then
            Return True
        Else
            Return False
        End If
    End Function



    Function GetKmStr(ByVal idx As Integer, ByVal value As String) As String
        If idx = 0 Then
            Return value
        Else
            Return "," & value
        End If
    End Function

    Function GetWhereStr(ByVal idx As Integer, ByVal value As String) As String
        If idx = 0 Then
            Return value
        Else
            Return "AND " & value
        End If
    End Function

    Function GetAspxVBPageCode(ByVal acTableData As DataTable, ByVal mTableData As DataTable, ByVal AutoCodeDbClass As AutoCodeDbClass) As String

        Dim AutoMkCode As New AutoMkCode

        Dim sb As New StringBuilder
        With sb
            .AppendLine("Imports System.Data")
            .AppendLine("Imports System.Text")
            .AppendLine("Imports System.IO")
            .AppendLine("")
            .AppendLine("Partial Class " & FileNameNoEx & "")
            .AppendLine("    Inherits System.Web.UI.Page")
            .AppendLine("")

            .AppendLine("   Public BC AS NEW " & AT.MakeStrFirstCharUpper(acTableData.TableName) & "BC")

            .AppendLine("    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load")

            .AppendLine("           Me.lblMsg.Text = """"")

            .AppendLine("        If Not IsPostBack Then")
            .AppendLine("            '明細設定")
            .AppendLine("            MsInit()")

            .AppendLine("        End If")
            .AppendLine("  ")
            .AppendLine("")
            .AppendLine("    End Sub")


            .AppendLine("    public Sub MsInit()")
            .AppendLine("")

            .AppendLine("            '明細設定")
            .AppendLine("            Dim dt As DataTable = GetMsData()")
            .AppendLine("            Me.gvMs.DataSource = dt")
            .AppendLine("            Me.gvMs.DataBind()")

            .AppendLine("  ")
            .AppendLine("")
            .AppendLine("    End Sub")




            .AppendLine("")
            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 明細データ取得")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <returns></returns>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Private Function GetMsData() As Data.DataTable")

            .Append("       Return BC." & AutoMkCode.GetFunctionName("Get", acTableData.TableName) & "(")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                If i = 0 Then
                    .Append("tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                Else
                    .Append(", tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                End If


            Next
            .Append(")")
            .AppendLine()


            '.AppendLine("")
            '.AppendLine("        Dim sb As New StringBuilder")
            '.AppendLine("        With sb")
            '.AppendLine("            .AppendLine(""SELECT TOP 1000"")")
            'For i As Integer = 0 To acTableData.Rows.Count - 1

            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

            '    .AppendLine("            .AppendLine(""" & GetKmStr(i, columns_name) & " "")")
            'Next

            ''GetKmStr
            '.AppendLine("            .AppendLine(""FROM " & acTableData.TableName & """)")

            '.AppendLine("        End With")
            '.AppendLine("")
            ''.AppendLine("        Dim DbResult As DbResult()")
            '.AppendLine("        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)")
            '.AppendLine("        Return DbResult.Data")

            .AppendLine("    End Function")
            .AppendLine("")
            '.AppendLine("")
            '.AppendLine("    ''' <summary>")
            '.AppendLine("    ''' 行選択")
            '.AppendLine("    ''' </summary>")
            '.AppendLine("    ''' <param name=""sender""></param>")
            '.AppendLine("    ''' <param name=""e""></param>")
            '.AppendLine("    ''' <remarks></remarks>")
            '.AppendLine("    Protected Sub gvMs_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvMs.SelectedIndexChanged")
            '.AppendLine("")
            '.AppendLine("        Dim row As GridViewRow = gvMs.SelectedRow")
            'For i As Integer = 0 To acTableData.Rows.Count - 1

            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString


            '    .AppendLine("   '" & AutoCodeDbClass.Get_name_jp(columns_name) & " " & columns_type & "(" & columns_length & ")")

            '    .AppendLine("   tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text = row.Cells(" & (i + 1) & ").Text.Replace(""&nbsp;"", """")")


            'Next
            '.AppendLine("       ")

            '.AppendLine("    End Sub")
            '.AppendLine("")
            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 更新")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click")



            .Append("        BC." & AutoMkCode.GetFunctionName("Upd", acTableData.TableName) & "(")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                If i = 0 Then
                    .Append("tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                Else
                    .Append(", tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                End If


            Next
            .Append(")")
            .AppendLine()



            '.AppendLine("")
            '.AppendLine("        Dim sb As New StringBuilder")
            '.AppendLine("        With sb")


            'Dim table_name As String = acTableData.TableName
            '.AppendLine("            .AppendLine(""UPDATE " & table_name & """)")
            '.AppendLine("            .AppendLine(""SET"")")
            'For i As Integer = 0 To acTableData.Rows.Count - 1
            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
            '    .AppendLine("            .AppendLine(""" & GetKmStr(i, columns_name & " = '"" & tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
            'Next
            '.AppendLine("            .AppendLine(""WHERE"")")
            'For i As Integer = 0 To acTableData.Rows.Count - 1
            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

            '    'Dim pk As Integer = acTableData.Rows(i).Item("pk").ToString
            '    Dim pk As Boolean = IsKey(acTableData.Rows(i).Item("pk").ToString)
            '    If pk Then
            '        .AppendLine("            .AppendLine(""" & GetWhereStr(i, columns_name & " = '"" & hid" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
            '    End If

            'Next

            '.AppendLine("        End With")

            '.AppendLine("        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)")

            '.AppendLine("        If Not DbResult.Result Then")
            '.AppendLine("            Me.lblMsg.Text = DbResult.Message")
            '.AppendLine("        End If")

            .AppendLine("        MsInit()")
            .AppendLine("    End Sub")
            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 登録")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click")



            .Append("        BC." & AutoMkCode.GetFunctionName("Ins", acTableData.TableName) & "(")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                If i = 0 Then
                    .Append("tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                Else
                    .Append(", tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                End If


            Next
            .Append(")")
            .AppendLine()

            '.AppendLine("        Dim sb As New StringBuilder")
            '.AppendLine("        With sb")
            '.AppendLine("            .AppendLine(""INSERT INTO " & acTableData.TableName & """)")

            '.AppendLine("            .AppendLine(""("")")
            'For i As Integer = 0 To acTableData.Rows.Count - 1
            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
            '    .AppendLine("            .AppendLine(""" & GetKmStr(i, columns_name) & " "")")
            'Next
            '.AppendLine("            .AppendLine("")"")")
            '.AppendLine("            .AppendLine(""VALUES"")")
            '.AppendLine("            .AppendLine(""("")")
            'For i As Integer = 0 To acTableData.Rows.Count - 1
            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

            '    .AppendLine("            .AppendLine(""" & GetKmStr(i, "  N'"" & tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
            'Next
            '.AppendLine("            .AppendLine("")"")")
            '.AppendLine("        End With")
            '.AppendLine("        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)")

            '.AppendLine("        If Not DbResult.Result Then")
            '.AppendLine("            Me.lblMsg.Text = DbResult.Message")
            '.AppendLine("        End If")

            .AppendLine("        MsInit()")

            .AppendLine("    End Sub")




            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 削除")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click")




            .Append("        BC." & AutoMkCode.GetFunctionName("Del", acTableData.TableName) & "(")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                If i = 0 Then
                    .Append("tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                Else
                    .Append(", tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text")
                End If


            Next
            .Append(")")
            .AppendLine()

            '.AppendLine("        Dim sb As New StringBuilder")
            '.AppendLine("        With sb")
            '.AppendLine("            .AppendLine(""DELETE FROM " & acTableData.TableName & """)")
            '.AppendLine("            .AppendLine(""WHERE"")")
            'For i As Integer = 0 To acTableData.Rows.Count - 1
            '    Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
            '    Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
            '    Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
            '    'Dim pk As Integer = acTableData.Rows(i).Item("pk").ToString
            '    Dim pk As Boolean = IsKey(acTableData.Rows(i).Item("pk").ToString)
            '    If pk Then
            '        .AppendLine("            .AppendLine(""" & GetWhereStr(i, columns_name & " = '"" & hid" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
            '    End If

            'Next
            '.AppendLine("        End With")
            '.AppendLine("        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)")

            '.AppendLine("        If Not DbResult.Result Then")
            '.AppendLine("            Me.lblMsg.Text = DbResult.Message")
            '.AppendLine("        End If")
            .AppendLine("        MsInit()")
            .AppendLine("    End Sub")
            .AppendLine("End Class")

        End With


        Return sb.ToString
    End Function



End Class
