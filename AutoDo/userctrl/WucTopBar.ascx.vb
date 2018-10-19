
Partial Class userctrl_WucTopBar
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetViewstate()

            If (C.Client(Page).login_user_id = "lis6" OrElse C.Client(Page).login_user_id = "Administrator") Then
                Me.lbtnAnnkenForuda.Enabled = True
            ElseIf Request.QueryString("user") Is Nothing AndAlso Request.QueryString("user") = "lis6" Then
                Me.lbtnAnnkenForuda.Enabled = True
            Else
                Me.lbtnAnnkenForuda.Enabled = False

            End If
            ' Me.lbtnAnnkenForuda.Enabled = (C.Client(Page).login_user_id = "lis6" OrElse C.Client(Page).login_user_id = "Administrator")




        End If
    End Sub


    Public Sub SetContext()
        Context.Items("edp_txt") = ViewState("edp_txt")
        Context.Items("edp_no") = ViewState("edp_no")

    End Sub

    Public Sub SetViewstate()
        ViewState("edp_txt") = Context.Items("edp_txt")
        ViewState("edp_no") = Context.Items("edp_no")

    End Sub


    Protected Sub lbtnSintyoku_Click(sender As Object, e As System.EventArgs) Handles lbtnSintyoku.Click
        SetContext()
        Server.Transfer("AnkanSinntyoku.aspx")

    End Sub

    Protected Sub lbtnAnnkenKanri_Click(sender As Object, e As System.EventArgs) Handles lbtnAnnkenKanri.Click
        SetContext()
        Server.Transfer("AnkannKanri.aspx")

    End Sub

    Protected Sub lbtnToday_Click(sender As Object, e As System.EventArgs) Handles lbtnToday.Click
        SetContext()
        Server.Transfer("AnkannTodayDo.aspx")
    End Sub

    Protected Sub lbtnKanjiToEng_Click(sender As Object, e As System.EventArgs) Handles lbtnKanjiToEng.Click
        SetContext()
        Server.Transfer("Zctrl.aspx")
    End Sub

    Protected Sub lbtnAnkanSiryou_Click(sender As Object, e As System.EventArgs) Handles lbtnAnkanSiryou.Click
        SetContext()
        Server.Transfer("ZSiryou.aspx")
    End Sub

    Protected Sub lbtnAutoCode_Click(sender As Object, e As System.EventArgs) Handles lbtnAutoCode.Click
        SetContext()
        Server.Transfer("ZbyDB.aspx")
    End Sub

    Protected Sub lbtnAnnkenForuda_Click(sender As Object, e As System.EventArgs) Handles lbtnAnnkenForuda.Click
        SetContext()
        Server.Transfer("AnkannForuda.aspx")
    End Sub
End Class
