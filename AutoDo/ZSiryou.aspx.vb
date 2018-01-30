
Partial Class ZSiryou
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ViewState("user_id") = C.Client(Page).login_user


        If Not IsPostBack Then
            'If ViewState("user_id") Is Nothing Then
            '    ViewState("user_id") = "lis6"
            'End If
            BindTV()
        End If

    End Sub

    Private Function IsHaveNode(txt) As TreeNode

        For i As Integer = 0 To tv.Nodes.Count - 1
            If tv.Nodes(i).Text = txt Then
                Return tv.Nodes(i)
            End If
        Next

        Dim node As New TreeNode
        node.Text = txt
        'node.Target = ""
        'node.SelectAction = TreeNodeSelectAction.None

        tv.Nodes.Add(node)
        Return node

    End Function

    Public Sub TvBind(ByVal data As Data.DataTable)


        tv.Nodes.Clear()


        Dim oldV As String = ""

        Dim chlNode As TreeNode
        For i As Integer = 0 To data.Rows.Count - 1

            Dim preNode As TreeNode = IsHaveNode(data.Rows(i).Item(0).ToString)

            chlNode = New TreeNode
            chlNode.Text = data.Rows(i).Item(1).ToString
            preNode.ChildNodes.Add(chlNode)

            'If tv.Nodes.Contains(preNode) Then



            'End If


            'If oldV <> data.Rows(i).Item(0).ToString Then

            '    If (i > 0 AndAlso oldV <> data.Rows(i).Item(0).ToString) OrElse i = data.Rows.Count - 1 Then
            '        tv.Nodes.Add(preNode)
            '    End If

            '    preNode = New TreeNode
            '    chlNode = New TreeNode
            '    preNode.Text = data.Rows(i).Item(0).ToString
            '    chlNode.Text = data.Rows(i).Item(1).ToString
            '    preNode.ChildNodes.Add(chlNode)

            '    oldV = data.Rows(i).Item(0).ToString

            'Else
            '    chlNode = New TreeNode
            '    chlNode.Text = data.Rows(i).Item(1).ToString
            '    preNode.ChildNodes.Add(chlNode)

            '    If (i > 0 AndAlso oldV <> data.Rows(i).Item(0).ToString) OrElse i = data.Rows.Count - 1 Then
            '        tv.Nodes.Add(preNode)
            '    End If

            '    oldV = data.Rows(i).Item(0).ToString

            'End If





        Next

        tv.ExpandAll()

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim msg As String = DataUpd(False)
        If msg <> "" Then
            C.Msg(Page, msg)
        End If

        BindTV()

    End Sub

    Protected Sub btnDel_Click(sender As Object, e As System.EventArgs) Handles btnDel.Click
        Dim msg As String = DataUpd(True)
        If msg <> "" Then
            C.Msg(Page, msg)
        End If

        BindTV()
    End Sub



    Public Function DataUpd(ByVal DelOnly As Boolean) As String
        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim file_exp As String = Me.tbxGroupNm.Text & "_" & Me.tbxTitleNm.Text
        Dim groupNm As String = Me.tbxGroupNm.Text
        Dim titleNm As String = Me.tbxTitleNm.Text

        Dim ex_name As String = Me.ddlType.SelectedValue
        Dim type As String = Me.ddlType.SelectedValue

        Dim shareType As String = ddlShareType.SelectedValue
        Dim txt As String = Me.WucEditor1.TEXT


        Dim path As String = HttpRuntime.AppDomainAppPath & "DATA\" & file_exp & "." & ex_name

        C.SaveFile(path, txt)
        txt = txt.Replace("'", "''")
        Return C.CSaveSiryouTrue(edpNo, groupNm, titleNm, type, txt, C.Client(Page).login_user, shareType, DelOnly)

    End Function




    Protected Sub btnExp_Click(sender As Object, e As System.EventArgs) Handles btnExp.Click
        BindTV()
    End Sub
    Public Sub BindTV()
        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT group_nm,file_nm ")
            .AppendLine("FROM [auto_code].[dbo].[m_siryou] WHERE")
            .AppendLine("edp_no = '" & edpNo & "'")
        End With
        Dim msSql As New CMsSql()
        Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)
        TvBind(dt)
    End Sub

    Protected Sub tv_SelectedNodeChanged(sender As Object, e As System.EventArgs) Handles tv.SelectedNodeChanged

        'Dim treeV As TreeNode = sender
        'Dim node = treeV.selectnode

        Dim node = tv.SelectedNode

        If node.Depth = 0 Then
            Me.tbxGroupNm.Text = node.Text
            Exit Sub
        Else

            Dim edpNo As String = Me.WucEdpDb1.EdpNo
            Dim group_nm As String = node.Parent.Text
            Dim file_nm As String = node.Text
            Dim sb As New StringBuilder
            With sb
                sb.AppendLine("SELECT TOP 1 [edp_no]")
                sb.AppendLine("      ,[group_nm]")
                sb.AppendLine("      ,[file_nm]")
                sb.AppendLine("      ,[txt]")
                sb.AppendLine("      ,[user_id]")
                sb.AppendLine("      ,[type]")
                sb.AppendLine("      ,[share_type]")
                sb.AppendLine("      ,[ins_time]")
                sb.AppendLine("  FROM [auto_code].[dbo].[m_siryou]")
                .AppendLine("WHERE    edp_no = '" & edpNo & "'")
                .AppendLine("AND group_nm = '" & group_nm & "'")
                .AppendLine("AND file_nm = '" & file_nm & "'")
            End With
            Dim msSql As New CMsSql()
            Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)

            If dt.Rows.Count > 0 Then
                Me.ddlShareType.SelectedValue = dt.Rows(0).Item("share_type")
                Me.ddlType.SelectedValue = dt.Rows(0).Item("type")

                Me.WucEditor1.TEXT = dt.Rows(0).Item("txt")
                Me.WucEditor1.EditType = dt.Rows(0).Item("type")
                Me.tbxGroupNm.Text = dt.Rows(0).Item("group_nm")
                Me.tbxTitleNm.Text = dt.Rows(0).Item("file_nm")
            End If

        End If

    End Sub



    Protected Sub btnSel_Click(sender As Object, e As System.EventArgs) Handles btnSel1.Click
        Sel(Me.lblMsg1, Me.MS1)
    End Sub



    Sub Sel(ByVal lblMsg As Label, ByVal MS As GridView)
        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim conn As String = Me.WucEdpDb1.DbConnStr
        Dim serverName As String = Me.WucEdpDb1.DbServerName
        Dim dbName As String = Me.WucEdpDb1.DbName

        Dim dt As Data.DataTable
        Dim msg As String

        Dim sql As String = ""
        If Me.WucEditor1.GetSession.Trim = "" Then
            sql = Me.WucEditor1.TEXT
        Else
            sql = Me.WucEditor1.GetSession
        End If

        If MSSQL.SEL(conn, sql, dt, msg) Then

            lblMsg.Text = dt.Rows.Count & "件"
            MS.DataSource = dt
            MS.DataBind()


            Dim sbs As New StringBuilder
            Dim pjKj As String

            If MS.Rows.Count > 0 Then

                Dim TableInfoClass As New TableInfoClass

                For i As Integer = 0 To MS.HeaderRow.Cells.Count - 1

                    Dim dtKM As Data.DataTable = TableInfoClass.GetEnKMDATA(serverName, dbName, MS.HeaderRow.Cells(i).Text, "")

                    Dim jp As String = ""

                    If dtKM.Rows.Count > 0 Then
                        MS.HeaderRow.Cells(i).Text &= "<br>" & dtKM.Rows(0).Item("item_jp")
                        jp = dtKM.Rows(0).Item("item_jp")
                    Else

                    End If

                    If i = 0 Then
                        sbs.Append(dt.Columns(i).ColumnName)
                        pjKj = pjKj & jp
                    Else
                        sbs.Append(vbTab & dt.Columns(i).ColumnName)
                        pjKj = pjKj & vbTab & jp
                    End If

                Next
            End If

            sbs.Append(vbCrLf)
            sbs.Append(pjKj)
            sbs.Append(vbCrLf)
            For i As Integer = 0 To MS.Rows.Count - 1

                For j As Integer = 0 To MS.HeaderRow.Cells.Count - 1

                    If j = 0 Then
                        sbs.Append(MS.Rows(i).Cells(j).Text)
                    Else
                        sbs.Append(vbTab & MS.Rows(i).Cells(j).Text)
                    End If

                Next

                sbs.Append(vbCrLf)

            Next


            tbxData.Text = sbs.ToString



        Else
            lblMsg.Text = msg
            MS.DataSource = Nothing
            MS.DataBind()

        End If






    End Sub



End Class
