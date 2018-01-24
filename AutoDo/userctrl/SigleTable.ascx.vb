
Partial Class userctrl_SigleTable
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property DataSource As Data.DataTable
        Set(ByVal value As Data.DataTable)

            Me.GvTable.DataSource = value
            Me.GvTable.DataBind()

        End Set
    End Property


End Class
