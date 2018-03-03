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
            'head
            .AppendLine("    <title></title>")
            .AppendLine("</head>")
            .AppendLine("<body>")
            'body
            .AppendLine("    <form id=""form1"" runat=""server"">")
            .AppendLine("    <div>")

            .AppendLine("        <table style=""width: 100%;"">")

            For i As Integer = 0 To acTableData.Rows.Count - 1

                Dim columns_name As String = acTableData.Rows(i).Item("columns_name").ToString
                Dim columns_type As String = acTableData.Rows(i).Item("columns_type").ToString
                Dim columns_length As Integer = acTableData.Rows(i).Item("columns_length").ToString

                .AppendLine("            <tr>")
                .AppendLine("                <td>")
                .AppendLine("                    " & AutoCodeDbClass.Get_name_jp(columns_name) & "")
                .AppendLine("                </td>")
                .AppendLine("                <td>")
                .AppendLine("                    <asp:TextBox ID=""tbx" & AT.MakeStrFirstCharUpper(columns_name) & """ runat=""server""></asp:TextBox>")
                .AppendLine("                </td>")
                .AppendLine("                <td>")
                .AppendLine("                    " & columns_type & "(" & columns_length & ")")
                .AppendLine("                </td>")
                .AppendLine("            </tr>")

            Next
            .AppendLine("        </table>")

            .AppendLine("    </div>")
            .AppendLine("    </form>")
            .AppendLine("</body>")
            .AppendLine("</html>")

        End With


        Return sb.ToString

    End Function

End Class
