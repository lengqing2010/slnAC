
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

        If Not IsPostBack Then


        End If



    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDb.Click
        If DB.SelectedIndex <> -1 Then
            Dim TableInfoClass As New TableInfoClass
            Dim dt As Data.DataTable = TableInfoClass.GetTableNameInfo(DbServerName, DbName)
            Me.GVDB.DataSource = dt
            Me.GVDB.DataBind()
            divDB.Visible = True
        Else
            divDB.Visible = False
        End If

    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        divDB.Visible = False
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click

        divTable.Controls.Clear()

        Dim TableInfoClass As New TableInfoClass


        For i As Integer = 0 To Me.GVDB.Rows.Count - 1

            divTable.Visible = True

            If CType(GVDB.Rows(i).FindControl("cbx"), CheckBox).Checked Then

                Dim en As String = CType(GVDB.Rows(i).FindControl("LEN"), Label).Text.Trim
                Dim jp As String = CType(GVDB.Rows(i).FindControl("LJP"), Label).Text.Trim



                Dim tblPanel As UserControl = LoadControl("SigleTable.ascx")
                Dim ms As GridView = tblPanel.FindControl("GvTable")
                Dim lblName As Label = tblPanel.FindControl("lblTableName")
                lblName.Text = en & jp
                Dim dt As Data.DataTable = TableInfoClass.GetEnTblMs(DbServerName, DbName, en)
                ms.DataSource = dt
                ms.DataBind()

                divTable.Controls.Add(tblPanel)





            End If

        Next



    End Sub
End Class
