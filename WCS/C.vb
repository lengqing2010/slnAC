Public Class C
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
End Class
