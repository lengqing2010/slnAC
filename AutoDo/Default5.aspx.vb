Imports System.Data
Imports System.Text
Imports System.IO

Partial Class Default5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable

        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT edp_no,edp_no+' '+edp_mei")
            .AppendLine("FROM [m_edp]")
            .AppendLine("ORDER BY [edp_no] desc")
        End With

        Dim msSql As New CMsSql()
        Dim dt As DataTable = msSql.ExecSelect(sb.ToString)

    End Function



End Class
