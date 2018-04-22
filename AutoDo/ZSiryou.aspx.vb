
Partial Class ZSiryou
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ViewState("user_id") = C.Client(Page).login_user


        If Not IsPostBack Then
            'If ViewState("user_id") Is Nothing Then
            '    ViewState("user_id") = "lis6"
            'End If
            BindTV()
            BindTvJibun()
        End If

    End Sub

    Private Function IsHaveNode(ByVal tree As TreeView, ByVal txt As String, ByVal edp As String) As TreeNode

        For i As Integer = 0 To tree.Nodes.Count - 1
            If tree.Nodes(i).Text = txt Then
                Return tree.Nodes(i)
            End If
        Next

        Dim node As New TreeNode
        node.Text = txt
        node.Value = txt
        'node.Value = edp
        'node.Target = ""
        'node.SelectAction = TreeNodeSelectAction.None

        tree.Nodes.Add(node)
        Return node

    End Function

    Public Sub TvBind(ByVal tree As TreeView, ByVal data As Data.DataTable)


        tree.Nodes.Clear()

        Dim oldV As String = ""

        Dim chlNode As TreeNode
        For i As Integer = 0 To data.Rows.Count - 1


            Dim edp_no, group_nm, file_nm As String
            edp_no = data.Rows(i).Item("edp_no").ToString
            group_nm = data.Rows(i).Item("group_nm").ToString
            file_nm = data.Rows(i).Item("file_nm").ToString

            Dim preNode As TreeNode = IsHaveNode(tree, data.Rows(i).Item("group_nm").ToString, edp_no)

            chlNode = New TreeNode
            chlNode.Text = file_nm
            'chlNode.Value = edp_no

            chlNode.Value = file_nm

            preNode.ChildNodes.Add(chlNode)

            If Me.tbxGroupNm.Text = preNode.Text Then
                preNode.Expand()

                If chlNode.Value = Me.tbxTitleNm.Text Then
                    chlNode.Selected = True
                End If

            End If

        Next

        'tree.ExpandAll()

    End Sub

    ''' <summary>
    ''' 追加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim msg As String = DataUpd(False)
        If msg <> "" Then
            C.Msg(Page, msg)
        End If

        Dim shareType As String = ddlShareType.SelectedValue

        If shareType = "PERSON" Then
            BindTvJibun()
        Else
            BindTV()
        End If


    End Sub

    Protected Sub btnDel_Click(sender As Object, e As System.EventArgs) Handles btnDel.Click
        Dim msg As String = DataUpd(True)
        If msg <> "" Then
            C.Msg(Page, msg)
        End If

        Dim shareType As String = ddlShareType.SelectedValue
        If shareType = "PERSON" Then
            BindTvJibun()
        Else
            BindTV()
        End If

    End Sub


    ''' <summary>
    ''' データ追加
    ''' </summary>
    ''' <param name="DelOnly"></param>
    ''' <param name="RirekiUpd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DataUpd(ByVal DelOnly As Boolean, Optional ByVal RirekiUpd As Boolean = True) As String

        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim file_exp As String = Me.tbxGroupNm.Text & "_" & Me.tbxTitleNm.Text
        Dim groupNm As String = Me.tbxGroupNm.Text
        Dim titleNm As String = Me.tbxTitleNm.Text

        Dim ex_name As String = Me.ddlType.SelectedValue
        Dim type As String = Me.ddlType.SelectedValue

        Dim shareType As String = ddlShareType.SelectedValue

        If shareType = "PERSON" Then
            edpNo = "PERSON" & C.Client(Page).login_user.Replace("\", "").Replace("/", "")
        End If

        Dim txt As String

        If type = "text" Then
            txt = Me.kindEdiorHTML.Value
        Else
            txt = Me.WucEditor1.TEXT
            Me.kindEdiorHTML.Value = ""
        End If

        Dim path As String = HttpRuntime.AppDomainAppPath & "DATA\" & edpNo & "_" & file_exp & "." & ex_name
        C.SaveFile(path, txt)


        txt = txt.Replace("'", "''")
        Return C.CSaveSiryouTrue(edpNo, groupNm, titleNm, type, txt, C.Client(Page).login_user, shareType, DelOnly, RirekiUpd)

    End Function


    Protected Sub btnChange_Click(sender As Object, e As System.EventArgs) Handles btnChange.Click


        Dim msg As String = DataChange()
        If msg <> "" Then
            C.Msg(Page, msg)
        End If

        Dim shareType As String = ddlShareType.SelectedValue

        If shareType = "PERSON" Then
            BindTvJibun()
        Else
            BindTV()
        End If

    End Sub
    Public Function DataChange() As String

        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim file_exp As String = Me.tbxGroupNm.Text & "_" & Me.tbxTitleNm.Text
        Dim newGroupNm As String = Me.tbxGroupNm.Text
        Dim oldGroupNm As String = ViewState("group_nm").ToString

        Return C.ChangeGroupName(edpNo, oldGroupNm, newGroupNm)

    End Function





    Protected Sub btnExp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExp1.Click
        BindTV()
    End Sub

    Public Sub BindTV()
        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT edp_no,group_nm,file_nm ")
            .AppendLine("FROM [auto_code].[dbo].[m_siryou] WHERE")
            .AppendLine("edp_no <> '" & "PERSON" & C.Client(Page).login_user.Replace("\", "").Replace("/", "") & "'")
            .AppendLine("AND edp_no = '" & edpNo & "'")
        End With
        Dim msSql As New CMsSql()
        Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)
        TvBind(Me.tv, dt)
    End Sub

    Protected Sub tv_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tv.SelectedNodeChanged

        'Dim treeV As TreeNode = sender
        'Dim node = treeV.selectnode

        'Dim tv As TreeView = sender

        'ActiveTab("tabs_L", "0")

        Dim node = tv.SelectedNode

        If tv.SelectedNode.Depth = 0 Then
            btnChange.Enabled = True
        Else
            btnChange.Enabled = False
        End If


        If node.Depth = 0 Then
            Me.tbxGroupNm.Text = node.Text
            ViewState("group_nm") = Me.tbxGroupNm.Text
            Exit Sub
        Else

            Dim edpNo As String = Me.WucEdpDb1.EdpNo  '= node.Value
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

                If dt.Rows(0).Item("type") = "text" Then
                    Me.kindEdiorHTML.Value = dt.Rows(0).Item("txt")
                    Me.WucEditor1.TEXT = dt.Rows(0).Item("txt")
                Else
                    Me.WucEditor1.TEXT = dt.Rows(0).Item("txt")
                End If


                Me.WucEditor1.EditType = dt.Rows(0).Item("type")
                Me.tbxGroupNm.Text = dt.Rows(0).Item("group_nm")
                ViewState("group_nm") = dt.Rows(0).Item("group_nm")
                Me.tbxTitleNm.Text = dt.Rows(0).Item("file_nm")
            End If

        End If

    End Sub

    Protected Sub tv2_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tv2.SelectedNodeChanged

        'ActiveTab("tabs_L", "1")

        Dim node = tv2.SelectedNode

        If node.Depth = 0 Then
            Me.tbxGroupNm.Text = node.Text
            ViewState("group_nm") = Me.tbxGroupNm.Text
            Exit Sub
        Else

            Dim edpNo As String = node.Value
            Dim group_nm As String = node.Parent.Text
            Dim file_nm As String = node.Text
            Dim user_id As String = C.Client(Page).login_user
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
                .AppendLine("WHERE    edp_no = '" & "PERSON" & C.Client(Page).login_user.Replace("\", "").Replace("/", "") & "'")
                .AppendLine("AND    user_id = '" & user_id & "'")
                .AppendLine("AND group_nm = '" & group_nm & "'")
                .AppendLine("AND file_nm = '" & file_nm & "'")
            End With
            Dim msSql As New CMsSql()
            Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)

            If dt.Rows.Count > 0 Then
                Me.ddlShareType.SelectedValue = dt.Rows(0).Item("share_type")
                Me.ddlType.SelectedValue = dt.Rows(0).Item("type")

                If dt.Rows(0).Item("type") = "text" Then
                    Me.kindEdiorHTML.Value = dt.Rows(0).Item("txt")
                Else
                    Me.WucEditor1.TEXT = dt.Rows(0).Item("txt")
                End If

                Me.WucEditor1.EditType = dt.Rows(0).Item("type")
                Me.tbxGroupNm.Text = dt.Rows(0).Item("group_nm")
                ViewState("group_nm") = Me.tbxGroupNm.Text
                Me.tbxTitleNm.Text = dt.Rows(0).Item("file_nm")
            End If

        End If



    End Sub

    Public Sub ActiveTab(ByVal id As String, ByVal idx As String)

        Dim csScript As New StringBuilder

        With csScript
            '.AppendLine("<script language=""javascript"" type=""text/javascript"">")
            .AppendLine("        $(document).ready(function () {")
            .AppendLine("                    $('#" & id & "').tabs({active: " & idx & "});")
            .AppendLine("        });")
            '.AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        'ClientScript.RegisterStartupScript(Page.GetType(), "aaaaa", csScript.ToString, True)

    End Sub


    Protected Sub btnSel_Click(sender As Object, e As System.EventArgs) Handles btnSel1.Click

        Dim msg As String = DataUpd(False, False)
        If msg <> "" Then
            C.Msg(Page, msg)
        End If

        Sel(Me.lblMsg1, Me.MS1)

    End Sub



    Sub Sel(ByVal lblMsg As Label, ByVal MS As GridView)
        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim conn As String = Me.WucEdpDb1.DbConnStr
        Dim serverName As String = Me.WucEdpDb1.DbServerName
        Dim dbName As String = Me.WucEdpDb1.DbName

        Dim dt As Data.DataTable = Nothing
        Dim msg As String = ""

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
            Dim pjKj As String = ""

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


            tbxData.Text = sbs.ToString.Replace("&nbsp;", "")



        Else
            lblMsg.Text = msg
            MS.DataSource = Nothing
            MS.DataBind()

        End If






    End Sub

    ''' <summary>
    ''' Exp Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnExp2_Click(sender As Object, e As System.EventArgs) Handles btnExp2.Click
        BindTvJibun()
    End Sub

    Public Sub BindTvJibun()
        'ddlEdp
        Dim edpNo As String = Me.WucEdpDb1.EdpNo
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT edp_no,group_nm,file_nm ")
            .AppendLine("FROM [auto_code].[dbo].[m_siryou] WHERE")
            .AppendLine(" edp_no ='" & "PERSON" & C.Client(Page).login_user.Replace("\", "").Replace("/", "") & "'")
            .AppendLine("AND user_id = '" & C.Client(Page).login_user & "'")
            .AppendLine("AND share_type ='PERSON'")

        End With
        Dim msSql As New CMsSql()
        Dim dt As Data.DataTable = msSql.ExecSelect(sb.ToString)
        TvBind(tv2, dt)
    End Sub



End Class
