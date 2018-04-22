
Partial Class AnkannTodayDo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            lblYMD.Text = Now
            'Me.gvMs.DataSource = GetMsData()
            'Me.gvMs.DataBind()

            'For i As Integer = 0 To gvMs.Rows.Count - 1
            '    If i = 18 Then
            '        For j As Integer = 0 To gvMs.Rows(0).Cells.Count - 1
            '            'Me.gvMs.Rows(i).Cells(j).BackColor = Drawing.Color.Red
            '        Next
            '    End If
            '    'Me.gvMs.Rows(i).Cells(18).BackColor = Drawing.Color.Red
            'Next

            'Me.gvMs.Rows(0).Cells(18).Text = "紧急"
            'Me.gvMs.Rows(18).Cells(0).Text = "重要"

            'Me.gvMs.Rows(18).Cells(0).ForeColor = Drawing.Color.Red
            'Me.gvMs.Rows(0).Cells(18).ForeColor = Drawing.Color.Red
            If Request.QueryString("userid") Is Nothing Then
                Me.hidUser.Value = C.Client(Page).login_user_id

            Else

                Me.hidUser.Value = Request.QueryString("userid")
            End If
            If Me.hidUser.Value.Trim = "" Then
                Me.hidUser.Value = "lis6"
            End If

            Panel()

        End If

    End Sub


    Private Sub Panel()


        Dim user As String = Me.hidUser.Value
        Dim sb As New StringBuilder
        sb.Length = 0
        With sb

            sb.AppendLine("  select * from [t_today_do] where [user_id]='" & user & "'")

        End With

        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)


        Dim csScript As New StringBuilder
        With csScript


            For i As Integer = 0 To DbResult.Data.Rows.Count - 1
                Dim txt As String = DbResult.Data.Rows(i).Item("txt")
                Dim x As String = DbResult.Data.Rows(i).Item("x")
                Dim y As String = DbResult.Data.Rows(i).Item("y")
                Dim pkey As String = DbResult.Data.Rows(i).Item("pkey")
                .AppendLine("$(document).ready(function () {")

                .AppendLine(" CreateDiv('" & pkey & "','" & user & "','" & txt.Replace(vbLf, "\n") & "', '" & x & "', '" & y & "');")
                .AppendLine(" });")
            Next

        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "EditorInit", csScript.ToString, True)

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
