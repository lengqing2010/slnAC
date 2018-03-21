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
            .AppendLine("<link rel=""stylesheet"" type=""text/css"" href=""css/new_common.css"">")
            'head
            .AppendLine("    <title></title>")
            .AppendLine("</head>")
            .AppendLine("<body>")
            'body
            .AppendLine("    <form id=""form1"" runat=""server"">")
            .AppendLine("    <div>")
            .AppendLine("	<asp:Label ID=""lblMsg"" runat=""server"" ForeColor=""Red""></asp:Label>")

            .AppendLine("        <table style=""width: 1000px;"">")

            For i As Integer = 0 To acTableData.Rows.Count - 1

                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

                .AppendLine("            <tr>")
                .AppendLine("                <td Width=""200px"">")
                .AppendLine("                    " & AutoCodeDbClass.Get_name_jp(columns_name) & "")
                .AppendLine("                </td>")
                .AppendLine("                <td Width=""600px"">")

                'Enabled="false"
                Dim pk As Integer = acTableData.Rows(i).Item("pk").ToString
                If pk = 1 Then
                    .AppendLine("                    <asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server"" Width=""600"" BackColor=""Yellow""></asp:TextBox>")
                Else
                    .AppendLine("                    <asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server"" Width=""600""></asp:TextBox>")
                End If

                .AppendLine("                </td>")
                .AppendLine("                <td >")
                .AppendLine("                    " & columns_type & "(" & columns_length & ")")
                .AppendLine("                </td>")
                .AppendLine("            </tr>")

            Next
            .AppendLine("        </table>")
            .AppendLine("        <asp:Button ID=""btnUpdate"" runat=""server"" Text=""Update"" />")
            .AppendLine("        <asp:Button ID=""btnInsert"" runat=""server"" Text=""Insert"" />")
            .AppendLine("        <asp:Button ID=""btnDelete"" runat=""server"" Text=""Delete"" />")
            .AppendLine("        <asp:GridView ID=""gvMs"" runat=""server""")
            .AppendLine("        autogenerateselectbutton=""True""")
            .AppendLine("        >")
            .AppendLine("            <SelectedRowStyle BackColor=""#FFFF99"" />")
            .AppendLine("        </asp:GridView>")
            .AppendLine("    </div>")
            .AppendLine("    </form>")
            .AppendLine("</body>")
            .AppendLine("</html>")

        End With


        Return sb.ToString

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



        Dim sb As New StringBuilder
        With sb
            .AppendLine("Imports System.Data")
            .AppendLine("Imports System.Text")
            .AppendLine("Imports System.IO")
            .AppendLine("")
            .AppendLine("Partial Class " & FileNameNoEx & "")
            .AppendLine("    Inherits System.Web.UI.Page")
            .AppendLine("")
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
            .AppendLine("")
            .AppendLine("        Dim sb As New StringBuilder")
            .AppendLine("        With sb")
            .AppendLine("            .AppendLine(""SELECT "")")
            For i As Integer = 0 To acTableData.Rows.Count - 1

                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

                .AppendLine("            .AppendLine(""" & GetKmStr(i, columns_name) & " "")")
            Next

            'GetKmStr
            .AppendLine("            .AppendLine(""FROM " & acTableData.TableName & """)")

            .AppendLine("        End With")
            .AppendLine("")
            '.AppendLine("        Dim DbResult As DbResult()")
            .AppendLine("        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)")
            .AppendLine("        Return DbResult.Data")

            .AppendLine("    End Function")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 行選択")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub gvMs_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvMs.SelectedIndexChanged")
            .AppendLine("")
            .AppendLine("        Dim row As GridViewRow = gvMs.SelectedRow")
            For i As Integer = 0 To acTableData.Rows.Count - 1

                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

            
                .AppendLine("   '" & AutoCodeDbClass.Get_name_jp(columns_name) & " " & columns_type & "(" & columns_length & ")")
              
                .AppendLine("   tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text = row.Cells(" & (i + 1) & ").Text.Replace(""&nbsp;"", """")")
              
              
            Next
            .AppendLine("       ")

            .AppendLine("    End Sub")
            .AppendLine("")
            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 更新")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click")
            .AppendLine("")
            .AppendLine("        Dim sb As New StringBuilder")
            .AppendLine("        With sb")


            Dim table_name As String = acTableData.TableName
            .AppendLine("            .AppendLine(""UPDATE " & table_name & """)")
            .AppendLine("            .AppendLine(""SET"")")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                .AppendLine("            .AppendLine(""" & GetKmStr(i, columns_name & " = '"" & tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
            Next
            .AppendLine("            .AppendLine(""WHERE"")")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

                Dim pk As Integer = acTableData.Rows(i).Item("pk").ToString
                If pk = 1 Then
                    .AppendLine("            .AppendLine(""" & GetWhereStr(i, columns_name & " = '"" & tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
                End If

            Next

            .AppendLine("        End With")

            .AppendLine("        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)")

            .AppendLine("        If Not DbResult.Result Then")
            .AppendLine("            Me.lblMsg.Text = DbResult.Message")
            .AppendLine("        End If")

            .AppendLine("        MsInit()")
            .AppendLine("    End Sub")
            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 登録")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click")

            .AppendLine("        Dim sb As New StringBuilder")
            .AppendLine("        With sb")
            .AppendLine("            .AppendLine(""INSERT INTO " & table_name & """)")

            .AppendLine("            .AppendLine(""("")")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                .AppendLine("            .AppendLine(""" & GetKmStr(i, columns_name) & " "")")
            Next
            .AppendLine("            .AppendLine("")"")")
            .AppendLine("            .AppendLine(""VALUES"")")
            .AppendLine("            .AppendLine(""("")")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

                .AppendLine("            .AppendLine(""" & GetKmStr(i, "  N'"" & tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
            Next
            .AppendLine("            .AppendLine("")"")")
            .AppendLine("        End With")
            .AppendLine("        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)")

            .AppendLine("        If Not DbResult.Result Then")
            .AppendLine("            Me.lblMsg.Text = DbResult.Message")
            .AppendLine("        End If")

            .AppendLine("        MsInit()")

            .AppendLine("    End Sub")




            .AppendLine("    ''' <summary>")
            .AppendLine("    ''' 削除")
            .AppendLine("    ''' </summary>")
            .AppendLine("    ''' <param name=""sender""></param>")
            .AppendLine("    ''' <param name=""e""></param>")
            .AppendLine("    ''' <remarks></remarks>")
            .AppendLine("    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click")
            .AppendLine("        Dim sb As New StringBuilder")
            .AppendLine("        With sb")
            .AppendLine("            .AppendLine(""DELETE FROM " & table_name & """)")
            .AppendLine("            .AppendLine(""WHERE"")")
            For i As Integer = 0 To acTableData.Rows.Count - 1
                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString
                Dim pk As Integer = acTableData.Rows(i).Item("pk").ToString
                If pk = 1 Then
                    .AppendLine("            .AppendLine(""" & GetWhereStr(i, columns_name & " = '"" & tbx" & AT.MakeStrFirstCharUpper(columns_name) & ".Text & ""'  ") & " "")")
                End If

            Next
            .AppendLine("        End With")
            .AppendLine("        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)")

            .AppendLine("        If Not DbResult.Result Then")
            .AppendLine("            Me.lblMsg.Text = DbResult.Message")
            .AppendLine("        End If")
            .AppendLine("        MsInit()")
            .AppendLine("    End Sub")
            .AppendLine("End Class")

        End With


        Return sb.ToString
    End Function



End Class
