Imports Microsoft.VisualBasic

Public Class Translate

    Public Shared Function IsTranslateStr(ByVal chr As String) As Boolean

        If Not IsNumeric(chr) AndAlso chr <> vbLf AndAlso chr.Trim <> "" AndAlso (",'()").IndexOf(chr.Trim) < 0 AndAlso C.sqlGuanjianzi.IndexOf(chr.Trim.ToUpper) < 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
