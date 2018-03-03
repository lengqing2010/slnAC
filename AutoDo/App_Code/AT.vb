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

End Class
