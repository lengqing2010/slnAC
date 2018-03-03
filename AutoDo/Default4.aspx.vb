
Partial Class Default4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim CDB As New CDB
        Dim dbEdpLst As Data.DataTable = CDB.GetEdpList

        ucEdpLst.DataSource = dbEdpLst

    End Sub
End Class
