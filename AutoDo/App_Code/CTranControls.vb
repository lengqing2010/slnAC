Imports Microsoft.VisualBasic
Imports System.Data
Imports COMMON

Public Class CTranControls

    Public Shared Function CTranControls(ByVal data_source As String, ByVal db_name As String, ByVal tablesNameStr As String, ByVal tranStr As String) As String

        Dim arr() As String = C.GetKmItem(tranStr)
        Dim sb As New StringBuilder


        Dim myTableInfoClass As New TableInfoClass
        For i As Integer = 0 To arr.Length - 1
            If Not IsNumeric(arr(i)) AndAlso arr(i) <> vbLf AndAlso arr(i).Trim <> "" AndAlso (",'()").IndexOf(arr(i).Trim) < 0 AndAlso C.sqlGuanjianzi.IndexOf(arr(i).Trim.ToUpper) < 0 Then

                Dim dt As DataTable = myTableInfoClass.GetEnKMDATA(data_source, db_name, arr(i).Trim, tablesNameStr)


                sb.AppendLine(C.GetNetControls(dt))

            End If

        Next

        Return sb.ToString

    End Function


End Class
