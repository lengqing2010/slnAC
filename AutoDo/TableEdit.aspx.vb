
Partial Class TableEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Dim SqlDataSource As New SqlDataSource
        'SqlDataSource.ConnectionString = C.conn
        'SqlDataSource.SelectCommand = "SELECT *  FROM [auto_code].[dbo].[m_edp]"
        'GV.DataSource = SqlDataSource
        'GV.DataBind()
        'GV.DataKeyNames = "edp_no,file_exp"




    End Sub

    Protected Sub btnEdpAdd_Click(sender As Object, e As System.EventArgs) Handles btnEdpAdd.Click
        Dim msg As String = _
        C.DelIns_m_edp(Me.edp_no.Text, Me.edp_mei.Text, Me.edp_exp.Text)

        If msg <> "" Then
            C.Msg(Page, msg)
        Else
            C.Msg(Page, "OK")
        End If

    End Sub

    Protected Sub btnm_db_infoAdd_Click(sender As Object, e As System.EventArgs) Handles btnm_db_infoAdd.Click

        Dim msg As String = _
        C.DelIns_m_db_info(Me.data_source.Text, Me.db_name.Text, Me.db_type.Text, Me.db_user_id.Text, Me.db_password.Text, Me.db_enlist.Text, Me.db_conn.Text, Me.db_exp.Text)

        If msg <> "" Then
            C.Msg(Page, msg)
        Else
            C.Msg(Page, "OK")
        End If

    End Sub
End Class
