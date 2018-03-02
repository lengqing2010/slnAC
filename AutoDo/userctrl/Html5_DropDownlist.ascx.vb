
Partial Class userctrl_Html5_DropDownlist
    Inherits System.Web.UI.UserControl


    Private _DataSource As Data.DataTable
    Public Property DataSource As Data.DataTable
        Get
            Return _DataSource
        End Get


        Set(ByVal value As Data.DataTable)

            h5_DropDownlist_Datalist.InnerHtml = ""

            Dim sb As New StringBuilder
            For i As Integer = 0 To value.Rows.Count - 1
                sb.AppendLine("<option value=""" & value.Rows(i).Item("value").ToString & """")
                sb.AppendLine(" data-value=""" & value.Rows(i).Item("value").ToString & """>")

                sb.AppendLine("1</option>")

            Next
            h5_DropDownlist_Datalist.InnerHtml = sb.ToString

        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.h5_DropDownlist.Attributes.Item("list") = Me.h5_DropDownlist_Datalist.ClientID
    End Sub


End Class
