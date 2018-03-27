Imports System.Web.Services

Partial Class ZSiryouAJAX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function SaveData(ByVal edpNo As String, ByVal groupName As String, ByVal title As String) As String



        Return ""

    End Function
End Class
