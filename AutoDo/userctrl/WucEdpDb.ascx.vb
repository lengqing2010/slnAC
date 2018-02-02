Imports System.Collections.Generic

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

    Public Property DbConnStr() As String
        Get
            Return DB.SelectedValue
        End Get
        Set(ByVal value As String)
            DB.SelectedValue = value
        End Set
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

    Public ReadOnly Property table_ens() As String
        Get
            Return ViewState("table_ens")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            SetTables()

        End If

    End Sub

    Public Sub SetTables()

        Dim TableInfoClass As New TableInfoClass
        Dim dt As Data.DataTable = TableInfoClass.GetSelTables(C.Client(Page).login_user, EdpNo, Me.DbConnStr)

        If dt.Rows.Count > 0 Then

            Me.tbxNR.Text = dt.Rows(0).Item("table_ens")
            ViewState("table_ens") = dt.Rows(0).Item("table_ens")
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

            SetTables()

            Dim TableInfoClass As New TableInfoClass
            Dim dt As Data.DataTable = TableInfoClass.GetTableNameInfo(DbServerName, DbName)
            Me.GVDB.DataSource = dt
            Me.GVDB.DataBind()

            For i As Integer = 0 To Me.GVDB.Rows.Count - 1

                Dim en As String = CType(GVDB.Rows(i).FindControl("LEN"), Label).Text.Trim
                '  Dim jp As String = CType(GVDB.Rows(i).FindControl("LJP"), Label).Text.Trim
                Dim cb As CheckBox = GVDB.Rows(i).FindControl("cbx")
                If ("," & table_ens & ",").IndexOf("," & en & ",") >= 0 Then
                    cb.Checked = True
                End If

            Next

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

        Dim keyKJ As String = Me.tbxNR.Text.Trim.Replace(")", "").Replace("(", "").Replace("[", "").Replace("]", "").Replace(",", "")
        Dim arr() As String = C.GetKmItem(keyKJ)
        Dim dicKey As New Dictionary(Of String, String)
        For i As Integer = 0 To arr.Length - 1

            arr(i) = arr(i).Replace(")", "").Replace("(", "").Replace("[", "").Replace("]", "").Replace(",", "").Replace(vbTab, "").Trim
            arr(i) = arr(i).Replace(vbNewLine, "")
            arr(i) = arr(i).Replace(vbLf, "")
            arr(i) = arr(i).Replace(vbCr, "")
            arr(i) = arr(i).Trim

            If arr(i).Trim <> "" Then
                If Not dicKey.ContainsKey(arr(i).ToString) Then
                    dicKey.Add(arr(i), arr(i))
                End If

            End If


        Next



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

        If divTable.Visible Then



            For i As Integer = 0 To divTable.Controls.Count - 1

                'Dim tblPanel As UserControl = LoadControl("SigleTable.ascx")
                Dim tblPanel As UserControl = divTable.Controls(i)
                Dim ms As GridView = tblPanel.FindControl("GvTable")

                For j As Integer = 0 To ms.Rows.Count - 1
                    Dim cb As CheckBox = ms.Rows(j).FindControl("cbx")

                    If cb.Text.IndexOf("|") = -1 Then
                    Else

                        Dim keyEn As String = cb.Text.Split("|")(0)
                        Dim keyJP As String = cb.Text.Split("|")(1)


                        If dicKey.ContainsKey(keyEn) _
                            OrElse dicKey.ContainsKey(keyJP) Then
                            cb.Checked = True
                        End If
                    End If


                Next

            Next

        End If

    End Sub

    'Protected Sub btnSelTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelTable.Click

    '    Dim keyKJ As String = Me.tbxNR.Text.Trim
    '    Dim arr() As String = C.GetKmItem(keyKJ)

    '    Dim dicKey As New Dictionary(Of String, String)

    '    For i As Integer = 0 To arr.Count - 1
    '        If Not dicKey.Keys.Contains(arr(i).ToString) Then
    '            dicKey.Add(arr(i), arr(i))
    '        End If
    '    Next

    '    For i As Integer = 0 To Me.GVDB.Rows.Count - 1

    '        Dim cb As CheckBox = Me.GVDB.Rows(i).FindControl("cbx")
    '        Dim lbl1 As Label = Me.GVDB.Rows(i).FindControl("LEN")
    '        Dim lbl2 As Label = Me.GVDB.Rows(i).FindControl("LJP")

    '        If dicKey.Keys.Contains(lbl1.Text) _
    '            OrElse dicKey.Keys.Contains(lbl2.Text) Then
    '            cb.Checked = True
    '        End If

    '    Next

    '    If divTable.Visible Then

    '        For i As Integer = 0 To divTable.Controls.Count - 1

    '            'Dim tblPanel As UserControl = LoadControl("SigleTable.ascx")
    '            Dim tblPanel As UserControl = divTable.Controls(i)
    '            Dim ms As GridView = tblPanel.FindControl("GvTable")

    '            For j As Integer = 0 To ms.Rows.Count - 1
    '                Dim cb As CheckBox = Me.GVDB.Rows(i).FindControl("cbx")
    '                Dim keyEn As String = cb.Text.Split(" ")(0)
    '            Next

    '        Next

    '    End If

    'End Sub

    Protected Sub btnSaveTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveTable.Click

        Dim sb As String

        For i As Integer = 0 To Me.GVDB.Rows.Count - 1

            Dim cb As CheckBox = Me.GVDB.Rows(i).FindControl("cbx")
            Dim lbl1 As Label = Me.GVDB.Rows(i).FindControl("LEN")
            Dim lbl2 As Label = Me.GVDB.Rows(i).FindControl("LJP")

            If cb.Checked Then
                If sb = "" Then
                    sb &= lbl1.Text
                Else
                    sb &= "," & lbl1.Text
                End If
            End If

        Next

        Dim msg As String = _
        C.DelIns_m_main_use_table(C.Client(Page).login_user, EdpNo, Me.DbConnStr, sb)
        If msg <> "" Then
            C.Msg(Page, msg)
        Else

        End If
    End Sub

    Protected Sub btnE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnE.Click
        Server.Transfer("TableEdit.aspx")
    End Sub
End Class
