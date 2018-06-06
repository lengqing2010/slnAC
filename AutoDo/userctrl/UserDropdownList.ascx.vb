
Partial Class userctrl_UserDropdownList
    Inherits System.Web.UI.UserControl

    Private _Width As Integer = 100
    Public Property Width As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)   
            _Width = value
            Me.Text.Width = value
            Me.List.Width = (value + 4)
            Me.divList.Style.Item("width") = (value + 22) & "px"
            'Me.divPanel.Style.Item("width") = (value + 22) & "px"

        End Set
    End Property

    Private _Height As Integer = 100
    Public Property Height As Integer
        Get
            Return _Height
        End Get
        Set(ByVal value As Integer)
            Me.Text.Height = value
            _Height = value
        End Set
    End Property


    Public Property Value0 As String
        Get
            Return Me.hidValue.Value
        End Get
        Set(ByVal value As String)

            Me.hidValue.Value = value

            If ViewState("subName") IsNot Nothing AndAlso ViewState("subName").ToString.Trim <> String.Empty Then
                RunSub(Me.Parent.Page, ViewState("subName").ToString)
            End If

        End Set
    End Property

    Public Property Text0 As String
        Get
            Return Me.Text.Text
        End Get
        Set(ByVal value As String)
            Me.Text.Text = value
        End Set
    End Property



    Private _JqName As String = ""
    Public Property JqName As String
        Get
            Return _JqName
        End Get
        Set(ByVal value As String)
            Me.Text.CssClass = AddClassName(Me.Text.CssClass, value & "_text")
            Me.List.CssClass = AddClassName(Me.List.CssClass, value & "_list")
            _JqName = value
        End Set
    End Property

    Private _FirstBlank As Boolean = False

    Public Property FirstBlank As String
        Get
            If ViewState("firstBlank") Is Nothing Then
                ViewState("firstBlank") = "false"
            Else
                ViewState("firstBlank") = "true"
            End If
            Return (ViewState("firstBlank")).ToString.ToLower
        End Get
        Set(ByVal value As String)
            ViewState("firstBlank") = value.ToLower
        End Set
    End Property



    Private _DataSource As Data.DataTable
    Public Property DataSource As Data.DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As Data.DataTable)



            If FirstBlank = "true" Then
                Dim dr As Data.DataRow
                dr = value.NewRow
                value.Rows.InsertAt(dr, 0)
            End If

            Me.List.DataSource = value
            Me.List.DataBind()

            For i As Integer = 0 To value.Rows.Count - 1

                'Me.List.Rows(i).CssClass = "list_row"

                'Me.List.Rows(i).Style.Item("background-color") = "#fff"
                If value.Columns.Contains("status") Then
                    If value.Rows(i).Item("status").ToString = "9" Then
                        Me.List.Rows(i).Style.Item("background-color") = "#aaa"

                    ElseIf value.Rows(i).Item("status").ToString = "8" Then
                        Me.List.Rows(i).Style.Item("background-color") = "#bbb"
                    End If
                End If
            Next

            If value.Rows.Count > 15 Then
                Me.divList.Style.Item("height") = "330px"
            End If

            If value.Rows.Count > 0 Then
                If value.Columns.Contains("text") Then
                    If value.Rows(0).Item("text") Is DBNull.Value Then
                        value.Rows(0).Item("text") = ""
                    End If
                    Text0 = value.Rows(0).Item("text").ToString
                End If
                If value.Columns.Contains("value") Then
                    If value.Rows(0).Item("value") Is DBNull.Value Then
                        value.Rows(0).Item("value") = ""
                    End If
                    Value0 = value.Rows(0).Item("value").ToString
                End If


            End If

            If value.Columns.Count > 1 Then
                For i As Integer = 0 To value.Rows.Count - 1
                    List.Rows(i).Attributes.Item("value") = value.Rows(i).Item("value").ToString
                Next
            End If



        End Set
    End Property

    Public Function RowsValue(ByVal rIdx As Integer) As String
        Return List.Rows(rIdx).Attributes.Item("value").ToString
    End Function

    Public Function RowsText(ByVal rIdx As Integer) As String
        Return List.Rows(rIdx).Cells(0).Text.ToString
    End Function

    ''' <summary>
    ''' OnClick
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OnClick() As String
        Set(ByVal value As String)
            ViewState("subName") = value

        End Set
    End Property

    Public Function AddClassName(ByVal cssClass As String, ByVal v As String) As String
        If cssClass.Trim = "" Then
            Return v
        Else
            Return cssClass.Trim & " " & v
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ''Dim lst As New List(Of String)
            ''For i = 0 To 62
            ''    lst.Add("aaaaaaaaaaaaaaaaaaaaaaaaa" & i)
            ''Next

            'Dim dt As New Data.DataTable
            'dt.Columns.Add("Text")
            'dt.Columns.Add("Value")
            'For i = 0 To 62
            '    Dim dr As Data.DataRow
            '    dr = dt.NewRow
            '    dr.Item("Text") = "Text" & i
            '    dr.Item("Value") = "Value" & i
            '    dt.Rows.Add(dr)
            'Next

            'Me.DataSource = dt


            ''Me.List.DataSource = lst
            ''Me.List.DataBind()
            If ViewState("subName") IsNot Nothing AndAlso ViewState("subName").ToString.Trim <> String.Empty Then

            Else
                btnRun.Enabled = False
                Me.btnRun.Attributes.Item("onclick") = "return false;"
            End If
        End If

        BindJs()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init


    End Sub


    Public Sub BindJs()

        Dim csScript As New StringBuilder

        With csScript
            '动态加载JS
            .AppendLine("$.getScript('js/jquery-1.4.1.min.js')")

            '动态加载Style
            .AppendLine("$('<link>')")
            .AppendLine(".attr({ rel: 'stylesheet',")
            .AppendLine("type: 'text/css',")
            .AppendLine("href: 'css/userctrl_UserDropdownList.css'")
            .AppendLine("})")
            .AppendLine(".appendTo('head');")




            .AppendLine("    function DropdownListClick () {")
            .AppendLine("        cancelBubble();")
            .AppendLine("    }")
            .AppendLine("")
            .AppendLine("    //检查输入值是否存在， 不存在 清空")
            .AppendLine("")
            .AppendLine("    function ChkInputText(e) {")
            .AppendLine("        var list;")
            .AppendLine("        var text;")
            .AppendLine("        text = $(e);")
            .AppendLine("        list = $(e).next();")
            .AppendLine("        var key = $.trim($(e).val());")
            .AppendLine("        var isKeyAru;")
            .AppendLine("        isKeyAru = false;")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("        $(list).find('tr').each(function () {")
            .AppendLine("            if ($.trim($(this).text()) == key) {")
            .AppendLine("                isKeyAru = true;")
            .AppendLine("                return false;")
            .AppendLine("            }")
            .AppendLine("        });")
            .AppendLine("")
            .AppendLine("        if (!isKeyAru) {")
            .AppendLine("            text.val('');")
            .AppendLine("        }")
            .AppendLine("")
            .AppendLine("        return false;")
            .AppendLine("    }")
            .AppendLine("    //下拉框筛选 Oninput")
            .AppendLine("")
            .AppendLine("    function IputText(e) {")
            .AppendLine("        var list;")
            .AppendLine("        var text;")
            .AppendLine("        text = $(e);")
            .AppendLine("        list = $(e).next();")
            .AppendLine("        var key = $.trim($(e).val());")
            .AppendLine("")
            .AppendLine("        if (key == '') {")
            .AppendLine("            $(list).find('tr').show();")
            .AppendLine("")
            .AppendLine("        } else {")
            .AppendLine("            $(list).find('tr').show();")
            .AppendLine("            $(list).find('tr').each(function () {")
            .AppendLine("                if ($(this).text().indexOf(key) == -1) {")
            .AppendLine("                    $(this).hide();")
            .AppendLine("                }")
            .AppendLine("            });")
            .AppendLine("        }")
            .AppendLine("    }")
            .AppendLine("")
            .AppendLine("    // 下拉框隐藏 Tab 键")
            .AppendLine("")
            .AppendLine("    function UnInitDropdowlistByKey(e) {")
            .AppendLine("        var keynum = (event.keyCode ? event.keyCode : event.which);")
            .AppendLine("        if (keynum == '9' || keynum == '13') {")
            .AppendLine("            var list;")
            .AppendLine("            var text;")
            .AppendLine("            text = $(e);")
            .AppendLine("            list = $(e).next();")
            .AppendLine("            var key = $.trim($(e).val());")

            .AppendLine("            $(list).find('tr').each(function () {")
            .AppendLine("                if ($(this).text().indexOf(key) != -1) {")
            .AppendLine("                    $(e).val($.trim($(this).text()));")
            .AppendLine("                    return true;")
            .AppendLine("                }")
            .AppendLine("            });")

            .AppendLine("            $(list).hide();")
            .AppendLine("            $(document).unbind('click', myFun1);")
            .AppendLine("            $(list).find('tr').unbind();")
            .AppendLine("            //cancelBubble();")
            .AppendLine("        }")
            .AppendLine("    }")
            .AppendLine("")
            .AppendLine("    function UnInitDropdowlistByBlur(e) {")
            .AppendLine("        setTimeout(function () {")
            .AppendLine("            var list;")
            .AppendLine("            var text;")
            .AppendLine("            text = $(e);")
            .AppendLine("            list = $(e).next();")
            .AppendLine("            $(list).hide();")
            .AppendLine("            $(document).unbind('click', myFun1);")
            .AppendLine("            $(list).find('tr').unbind();")
            .AppendLine("        }, 100);")
            .AppendLine("    }")
            .AppendLine("")
            .AppendLine("    // 下拉框加载")
            .AppendLine("")
            .AppendLine("    function InitDropdownList(e) {")

            'FOCUS時　SELECT
            .AppendLine("        e.select();")

            .AppendLine("        var list;")
            .AppendLine("        var text;")
            .AppendLine("        var value;")
            .AppendLine("        var btn;")
            .AppendLine("        text = $(e);")
            .AppendLine("        list = $(e).next();")
            .AppendLine("        value = $(e).next().next();")
            .AppendLine("        btn = $(e).next().next().next();")
            .AppendLine("")
            .AppendLine("        //相对位置")
            .AppendLine("        //        var X = $(text).offset().top;")
            .AppendLine("        //        var Y = $(text).offset().left;")
            .AppendLine("        //绝对位置")
            .AppendLine("        var T = $(text).position().top;")
            .AppendLine("        var L = $(text).position().left;")
            .AppendLine("")
            .AppendLine("        // $(list).offset({ top: T + $(text).height(), left: L });")
            .AppendLine("        $(list).css('top', T + $(text).height() + 4);")
            .AppendLine("        $(list).css('left', L);")
            ' .AppendLine("        $(list).find('tr').css('background', '#fff');")
            .AppendLine("        $(list).find('tr').show();")
            .AppendLine("")
            .AppendLine("        //表示")
            .AppendLine("        list.show();")
            .AppendLine("")
            .AppendLine("        //阻止事件冒泡")
            .AppendLine("        //event.stopPropagation();")
            .AppendLine("        // cancelBubble(); //为了兼容火狐")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("        cancelBubble();")
            .AppendLine("")
            .AppendLine("        //绑定隐藏事件")
            .AppendLine("        setTimeout(function () {")
            .AppendLine("")
            .AppendLine("            $(document).bind('mousedown', myFun1 = function () {")
            .AppendLine("")
            .AppendLine("                //setTimeout(function () {")
            .AppendLine("                $(list).hide();")
            .AppendLine("                $(document).unbind('click', myFun1);")
            .AppendLine("                $(list).find('tr').unbind();")
            .AppendLine("                //}, 200);")
            .AppendLine("            });")
            .AppendLine("")
            .AppendLine("            //行选择")
            .AppendLine("            $(list).find('tr').mousedown(function () {")
            .AppendLine("                text.val($.trim($(this).text()));")
            .AppendLine("                value.val($.trim($(this).attr('value')));")
            '.AppendLine("                alert($(value).val());")
            .AppendLine("                $(list).hide();")
            .AppendLine("                $(document).unbind('click', myFun1);")
            .AppendLine("                $(list).find('tr').unbind();")

            'If ViewState("subName") IsNot Nothing AndAlso ViewState("subName").ToString.Trim <> String.Empty Then
            .AppendLine("           $(btn).click();")
            'End If


            .AppendLine("            });")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("")
            '.AppendLine("            var oldRowBgColor;")
            .AppendLine("            //行变色")
            .AppendLine("            $(list).find('tr').mouseenter(function () {")
            '.AppendLine("                oldRowBgColor=$(this).css('background-color');")
            '.AppendLine("                $(this).css('background-color', '#5CACEE');")
            .AppendLine("            });")
            .AppendLine("            //行变色 取消")
            .AppendLine("            $(list).find('tr').mouseout(function () {")
            '.AppendLine("                $(this).css('background', '#fff');")
            '.AppendLine("                $(this).css('background-color', oldRowBgColor);")
            .AppendLine("            });")
            .AppendLine("")
            .AppendLine("        }, 111);")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("        //        $(document).one('click', function () {")
            .AppendLine("        //            ")
            .AppendLine("        //        })")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("        //绑定live事件(live事件只在jquery1.9以下才支持，高版本不支持)。")
            .AppendLine("        //        $(document).live('click', function () {")
            .AppendLine("        //            $(list).hide();")
            .AppendLine("        //        })")
            .AppendLine("")
            .AppendLine("    }")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("    //得到事件")
            .AppendLine("")
            .AppendLine("    function getEvent() {")
            .AppendLine("        if (window.event) {")
            .AppendLine("            return window.event;")
            .AppendLine("        }")
            .AppendLine("        func = getEvent.caller;")
            .AppendLine("        while (func != null) {")
            .AppendLine("            var arg0 = func.arguments[0];")
            .AppendLine("            if (arg0) {")
            .AppendLine("                if ((arg0.constructor == Event || arg0.constructor == MouseEvent || arg0.constructor == KeyboardEvent) || (typeof (arg0) == 'object' && arg0.preventDefault && arg0.stopPropagation)) {")
            .AppendLine("                    return arg0;")
            .AppendLine("                }")
            .AppendLine("            }")
            .AppendLine("            func = func.caller;")
            .AppendLine("        }")
            .AppendLine("        return null;")
            .AppendLine("    }")
            .AppendLine("    //阻止冒泡")
            .AppendLine("")
            .AppendLine("    function cancelBubble() {")
            .AppendLine("        var e = getEvent();")
            .AppendLine("        if (window.event) {")
            .AppendLine("            //e.returnValue=false;//阻止自身行为")
            .AppendLine("            e.cancelBubble = true; //阻止冒泡")
            .AppendLine("        } else if (e.preventDefault) {")
            .AppendLine("            //e.preventDefault();//阻止自身行为")
            .AppendLine("            e.stopPropagation(); //阻止冒泡")
            .AppendLine("        }")
            .AppendLine("    }")

            .AppendLine("$(document).ready(function(){")

            '.AppendLine("")
            '.AppendLine("  $("".jq_dropdownlist_wuc"").on(""click,focus"",function(){")
            '.AppendLine("")
            '.AppendLine("    InitDropdownList(this[0]);")
            '.AppendLine("")
            '.AppendLine("  });")
            '.AppendLine("  $("".jq_dropdownlist_wuc"").on(""keydown"",function(){")
            '.AppendLine("")
            '.AppendLine("    UnInitDropdowlistByKey(this[0]);")
            '.AppendLine("")
            '.AppendLine("  });")
            '.AppendLine("")
            '.AppendLine("  $("".jq_dropdownlist_wuc"").on(""propertychange,input"",function(){")
            '.AppendLine("")
            '.AppendLine("    IputText(this[0]);")
            '.AppendLine("")
            '.AppendLine("  });")
            '.AppendLine("")
            '.AppendLine("  $("".jq_dropdownlist_wuc"").on(""blur"",function(){")
            '.AppendLine("")
            '.AppendLine("    ChkInputText(this[0]);")
            '.AppendLine("")
            '.AppendLine("  });")


            .AppendLine("  $('.jq_dropdownlist_wuc').click(function(){")
            .AppendLine("  	InitDropdownList(this);")
            .AppendLine("  });")
            .AppendLine("  $('.jq_dropdownlist_wuc').focus(function(){")
            .AppendLine("  	InitDropdownList(this);")
            .AppendLine("  });")
            .AppendLine("  $('.jq_dropdownlist_wuc').keydown(function(){")
            .AppendLine("  	UnInitDropdowlistByKey(this);")
            .AppendLine("  });")
            .AppendLine("  $('.jq_dropdownlist_wuc').bind('input propertychange', function() {")
            .AppendLine("  	IputText(this);")
            .AppendLine("  });")

            .AppendLine("  $('.jq_dropdownlist_wuc').blur(function(){")
            .AppendLine("  	ChkInputText(this);")
            .AppendLine("  })")


            .AppendLine("});")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します

        Me.Parent.Page.ClientScript.RegisterStartupScript(Me.Parent.Page.GetType(), "userctrl_UserDropdownList", csScript.ToString, True)

    End Sub

    Protected Sub btnRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRun.Click
        If ViewState("subName") IsNot Nothing AndAlso ViewState("subName").ToString.Trim <> String.Empty Then
            RunSub(Me.Parent.Page, ViewState("subName").ToString)
        End If

    End Sub

    Public Function RunSub(ByVal page As Page, ByVal functionName As String) As Boolean
        Dim csScript As New StringBuilder
        Dim pPage As Page = page
        Dim pType As Type = pPage.GetType
        Dim fname As String = functionName.Replace("(", String.Empty).Replace(")", String.Empty).Replace(";", String.Empty).Trim
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod(fname)

        If Not methodInfo Is Nothing Then
            Return Convert.ToBoolean(methodInfo.Invoke(pPage, New Object() {}))
        End If
        Return False
    End Function


End Class
