
Partial Class ZSiryou
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If ViewState("user_id") Is Nothing Then
                ViewState("user_id") = "lis6"
            End If
        End If

    End Sub

    Public Sub TvBind(ByVal data As Data.DataTable)

        Dim oldV As String = ""
        Dim preNode As TreeNode
        Dim chlNode As TreeNode
        For i As Integer = 0 To data.Rows.Count - 1

            If oldV <> data.Rows(i).Item(0).ToString Then



                preNode = New TreeNode
                chlNode = New TreeNode
                preNode.Text = data.Rows(i).Item(0).ToString
                chlNode.Text = data.Rows(i).Item(1).ToString
                preNode.ChildNodes.Add(chlNode)


            Else
                chlNode = New TreeNode
                chlNode.Text = data.Rows(i).Item(1).ToString
                preNode.ChildNodes.Add(chlNode)

            End If

            If (i > 0 AndAlso oldV <> data.Rows(i).Item(0).ToString) OrElse i = data.Rows.Count - 1 Then
                tv.Nodes.Add(preNode)
            End If

            oldV = data.Rows(i).Item(0).ToString

        Next





    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim edpNo As String = Me.WucEdpDb1.EdpNo

        Dim file_exp As String = Me.tbxGroupNm.Text & "_" & Me.tbxTitleNm.Text
        Dim ex_name As String = Me.ddlType.SelectedValue

        Dim txt As String = Me.WucEditor1.TEXT

        Dim data_source As String = Me.WucEdpDb1.DbServerName

        Dim path As String = HttpRuntime.AppDomainAppPath & "DATA\" & file_exp & "." & ex_name

        C.SaveFile(path, txt)
        txt = txt.Replace("'", "''")
        Dim msg As String = C.CSaveSIryoiu(edpNo, file_exp, txt, data_source, ViewState("user_id").ToString, path)

        If msg <> "" Then
            C.Msg(Page, msg)
        End If

    End Sub
End Class
