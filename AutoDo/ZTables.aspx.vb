
Partial Class ZTables
    Inherits System.Web.UI.Page

    Protected Sub btnChToEng_Click(sender As Object, e As System.EventArgs) Handles btnChToEng.Click


        Dim data_source As String = Me.WucEdpDb1.DbServerName
        Dim db_name As String = Me.WucEdpDb1.DbName

        Dim jp As String = C.GetTablesList(Me.tbxTableNames.Text)
        Dim jp2 As String = Me.tbxTableNames.Text
        '  Me.WucEditor2.TEXT = Trans(data_source, db_name, txtL, tablesNameStr)


        Dim sb As New StringBuilder
        With sb

            Dim col As String = "[no],[table_en],[table_jp],[item_en],[item_jp],[item_type],[item_keta],[item_key],[item_not_null],[item_index],[item_syoki],[item_exp],[item_err],[table_exp]"
            .AppendLine("  SELECT")
            .AppendLine("'km' as kind," & col)
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND (")
            .AppendLine("  ([item_jp] in( " & jp & " ) OR [item_en]  in( " & jp & " ) )")

            .AppendLine("  OR ([item_jp] like( '%" & jp2 & "%' ) OR [item_en]  like( '%" & jp2 & "%' ) )")
            .AppendLine(" )")

            .AppendLine("  union")
            .AppendLine("  SELECT")
            .AppendLine("'tbl' as kind," & col)
            .AppendLine(" FROM [auto_code].[dbo].[t_table_info]")
            .AppendLine(" WHERE ")
            .AppendLine("	[data_source] = '" & data_source & "'")
            .AppendLine(" AND [db_name] = '" & db_name & "'")
            .AppendLine(" AND (")
            .AppendLine("  ([table_jp] in( " & jp & " ) OR [table_en] in( " & jp & " ) )")
            .AppendLine("  OR ([table_jp] like( '%" & jp2 & "%' ) OR [table_en] like( '%" & jp2 & "%' ) )")
            .AppendLine(" )")
        End With


        Dim dt As Data.DataTable = Nothing
        Dim msg As String = ""

        If MSSQL.SEL("", sb.ToString, dt, msg) Then
            gv.DataSource = dt
            gv.DataBind()

        Else
            lblMsg.Text = msg
            gv.DataSource = Nothing
            gv.DataBind()
        End If



    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
    End Sub

    Protected Sub gv_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Header Then
            For i As Integer = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).Wrap = False
            Next
        End If
    End Sub

    Protected Sub gv_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gv.SelectedIndexChanged

    End Sub
End Class
