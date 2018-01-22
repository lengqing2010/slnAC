
Partial Class userctrl_WucDbList
    Inherits System.Web.UI.UserControl


    Public ReadOnly Property DDL() As DropDownList
        Get
            Return Me.ddlDbInfo
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sb As New StringBuilder
            With sb
                .AppendLine("SELECT db_conn,'('+db_type+') : ' + data_source+' : '+db_name")
                .AppendLine("FROM [m_db_info]")
            End With
            C.BindDropDownList(Me.ddlDbInfo, sb.ToString)
        End If
    End Sub

End Class
