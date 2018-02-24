Imports System.Data

Partial Class userctrl_WucEdpList
    Inherits System.Web.UI.UserControl


    Public ReadOnly Property DDL() As DropDownList
        Get
            Return Me.ddlEdp
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sb As New StringBuilder
            With sb
                .AppendLine("SELECT edp_no,edp_no+' '+edp_mei")
                .AppendLine("FROM [m_edp]")
                .AppendLine("ORDER BY [edp_no] desc")
            End With
            C.BindDropDownList(Me.ddlEdp, sb.ToString)
        End If
    End Sub
End Class
