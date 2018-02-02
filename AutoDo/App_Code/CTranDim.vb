Imports Microsoft.VisualBasic
Imports System.Data
Imports COMMON
Imports System.Collections.Generic

Public Class CTranDim

    Public Shared Function TranDim(ByVal data_source As String, ByVal db_name As String, ByVal tablesNameStr As String, ByVal tranStr As String) As String



        Dim arr() As String = C.GetKmItem(tranStr)
        Dim myTableInfoClass As New TableInfoClass

        Dim rtv As New StringBuilder

        For i As Integer = 0 To arr.Length - 1
            If Translate.IsTranslateStr(arr(i)) < 0 Then
                Dim dt As DataTable = myTableInfoClass.GetEnKMDATA(data_source, db_name, arr(i).Trim, tablesNameStr)
                rtv.AppendLine(GetDimParam(dt))
            End If
        Next

        Return rtv.ToString

    End Function


    Private Shared Function GetDimParam(ByVal kmDt As DataTable) As String

        Dim sbByval As New StringBuilder
        sbByval.AppendLine("'DIM ")

        Dim oldv As String = ""

        Dim dc As New Dictionary(Of String, String)

        'item_en,item_type,item_jp

        For j As Integer = 0 To kmDt.Rows.Count - 1

            Dim item_en As String = kmDt.Rows(j).Item("item_en").ToString

            If Not dc.ContainsKey(item_en) Then
                dc.Add(item_en, "")
                sbByval.Append("Dim " & C.MakeStrFirstCharUpper(item_en) & " AS " & "")                 'DIM A
                sbByval.Append(C.GetTypeFromDBType(kmDt.Rows(j).Item("item_type").ToString.ToLower))    'AS STRING
                sbByval.Append(C.AddNote(sbByval.ToString, kmDt.Rows(j).Item("item_jp"), "'"))          ''説明
                sbByval.Append(vbNewLine)
            End If

        Next
        Return sbByval.ToString

    End Function


End Class
