
Partial Class AnkannTodayDo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Me.gvMs.DataSource = GetMsData()
            Me.gvMs.DataBind()

            For i As Integer = 0 To gvMs.Rows.Count - 1
                If i = 18 Then
                    For j As Integer = 0 To gvMs.Rows(0).Cells.Count - 1
                        'Me.gvMs.Rows(i).Cells(j).BackColor = Drawing.Color.Red
                    Next
                End If
                'Me.gvMs.Rows(i).Cells(18).BackColor = Drawing.Color.Red
            Next

            Me.gvMs.Rows(0).Cells(18).Text = "紧急"
            Me.gvMs.Rows(18).Cells(0).Text = "重要"

            Me.gvMs.Rows(18).Cells(0).ForeColor = Drawing.Color.Red
            Me.gvMs.Rows(0).Cells(18).ForeColor = Drawing.Color.Red

        End If

    End Sub


    Public Function GetMsData() As Data.DataTable

        Dim dt As New Data.DataTable

        For i = 0 To 36
            dt.Columns.Add("c" & i)
        Next

        For i = 0 To 36
            Dim dr As Data.DataRow
            dr = dt.NewRow

            dr.Item(18) = "↑"
            dt.Rows.Add(dr)

            If i = 18 Then
                For j As Integer = 0 To dt.Columns.Count - 1
                    dr.Item(j) = "←"
                Next
            End If
        Next

        Return dt

    End Function
End Class
