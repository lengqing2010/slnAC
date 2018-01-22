
Partial Class userctrl_WucEdpDb
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property EDP() As DropDownList
        Get
            Return Me.WucEdpList1.DDL
        End Get
    End Property
    Public ReadOnly Property DB() As DropDownList
        Get
            Return Me.WucDbList1.DDL
        End Get
    End Property

    Public Property EdpNo() As String
        Get
            Return EDP.SelectedValue
        End Get
        Set(ByVal value As String)
            EDP.SelectedValue = value
        End Set
    End Property

    Public ReadOnly Property DbConnStr() As String
        Get
            Return DB.SelectedValue
        End Get
    End Property
    Public ReadOnly Property DbType() As String
        Get
            Return DB.Items(DB.SelectedIndex).Text.Split(":")(0).Trim
        End Get
    End Property
    Public ReadOnly Property DbServerName() As String
        Get
            Return DB.Items(DB.SelectedIndex).Text.Split(":")(1).Trim
        End Get
    End Property
    Public ReadOnly Property DbName() As String
        Get
            Return DB.Items(DB.SelectedIndex).Text.Split(":")(2).Trim
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class
