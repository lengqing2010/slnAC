
Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim ServerClientFileSynchro As New ServerClientFileSynchro

        'このページ
        'Root/
        'Root/data
        'Dim serverPath As String = "data"
        'Dim clientPath As String = "C:\VIVA-SA\"
        ''Dim fileExtension As String = "*.exe,*.bat,*.ini"
        'Dim fileExtension As String = "*.*"

        Call (New ServerClientFileSynchro).Synchro(Page)


    End Sub

End Class
