Imports Microsoft.VisualBasic

Public Class AT

    Enum ParamType
        SqlParam = 1
        NoParam = 2
    End Enum


    Public Shared Function vbtabSuu(ByVal suu As Integer) As String
        Dim rt As String = ""
        For i As Integer = 0 To suu
            If i > 0 Then
                rt = rt & vbTab
            End If
        Next
        Return rt
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
            If i = 0 Then
                chr = (chr.ToUpper)
            ElseIf chrPre <> "_" Then
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

End Class
