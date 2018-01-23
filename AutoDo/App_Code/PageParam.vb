Imports Microsoft.VisualBasic
Imports System.Web.UI

Public Class PageParam

    Public page As Page

    Public http As System.Web.HttpContext

    Public viewstate As System.Web.UI.StateBag

    Public Property user_id As String
        Get
            If viewstate("user_id") Is Nothing Then
                viewstate("user_id") = "lis6"
            End If

            Return viewstate("user_id").ToString

        End Get
        Set(ByVal value As String)
            viewstate("user_id") = value

        End Set
    End Property

    Public Sub New(ByRef page As Page, ByRef http As System.Web.HttpContext, ByRef viewstate As System.Web.UI.StateBag)
        Me.page = page
        Me.http = http
        Me.viewstate = viewstate
        SetViewstateContext("user_id")

    End Sub

    Private Sub SetViewstateContext(ByVal name As String)
        If http.Items(name) IsNot Nothing Then
            viewstate(name) = http.Items(name)
        Else
            http.Items(name) = viewstate(name)
        End If

    End Sub

End Class
