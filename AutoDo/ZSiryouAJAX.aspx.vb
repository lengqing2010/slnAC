Imports System.Web.Services

Partial Class ZSiryouAJAX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub


    ''' <summary>
    ''' SaveData
    ''' </summary>

    'Public Function SaveData(ByVal edpNo As String _
    '                        , ByVal group_nm As String _
    '                        , ByVal file_nm As String _
    '                        , ByVal type As String _
    '                        , ByVal txt As String _
    '                        , ByVal user_cd As String _
    '                        , ByVal share_type As String) As String
    <System.Web.Services.WebMethod()>
    Public Shared Function SaveData(ByVal edpNo As String) As String



        Dim a As Integer

        'Return Context.Request.Params(edpNo)

    End Function
End Class
