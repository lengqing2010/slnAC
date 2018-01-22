
Partial Class userctrl_WucEditor
    Inherits System.Web.UI.UserControl


    Public WriteOnly Property width As String
        Set(ByVal value As String)
            code1.Style.Item("width") = value & "px"
            tbxLeftArea.Style.Item("width") = value & "px"
        End Set
    End Property

    Public Property theme As String
        Get
            If ViewState("theme") Is Nothing Then
                ViewState("theme") = "twilight"
            End If
            Return ViewState("theme").ToString
        End Get
        Set(ByVal value As String)
            If value.Trim = "" Then value = "twilight"
            ViewState("theme") = value
        End Set
    End Property

    Public Property EditType As String
        Get
            Return ViewState("EditType").ToString
        End Get
        Set(ByVal value As String)
            If value.Trim = "" Then value = "txt"
            ViewState("EditType") = value
        End Set
    End Property

    Public Property TEXT As String
        Get
            Return Me.hidEditTxt.Value
        End Get
        Set(ByVal value As String)
            Me.hidEditTxt.Value = value
            Me.tbxLeftArea.InnerText = value
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        EditorInit()
        EditorInitRun()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
  

        Me.tbxLeftArea.InnerText = Me.hidEditTxt.Value

    End Sub

    Private Sub EditorInit()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("            function EditorInit(id, languageStr,theme) {")
            .AppendLine("                //初始化对象")
            .AppendLine("                editor = ace.edit(id);")
            .AppendLine("                //设置风格和语言（更多风格和语言，请到github上相应目录查看）")

            '.AppendLine("                theme = 'twilight'")
            '.AppendLine("                theme = 'clouds'")

            .AppendLine("                language = languageStr")
            .AppendLine("                editor.setTheme('ace/theme/' + theme);")
            .AppendLine("                editor.session.setMode('ace/mode/' + language);")
            .AppendLine("                //字体大小")
            .AppendLine("                editor.setFontSize(12);")
            .AppendLine("                //设置只读（true时只读，用于展示代码）")
            .AppendLine("                editor.setReadOnly(false);")
            .AppendLine("                //自动换行,设置为off关闭")
            .AppendLine("                //editor.setOption('wrap', 'free');")
            .AppendLine("                editor.setOption('off', 'free');")
            .AppendLine("                //启用提示菜单")
            .AppendLine("                ace.require('ace/ext/language_tools');")
            .AppendLine("                editor.setOptions({")
            .AppendLine("                    enableBasicAutocompletion: true,")
            .AppendLine("                    enableSnippets: true,")
            .AppendLine("                    enableLiveAutocompletion: true")
            .AppendLine("                });")
            .AppendLine("                return editor;")
            .AppendLine("            }")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        Me.Parent.Page.ClientScript.RegisterStartupScript(Me.GetType(), "EditorInit", csScript.ToString, True)

    End Sub


    Public Sub EditorInitRun()

        Dim objId As String = "editor" & Me.ClientID
        Dim csScript As New StringBuilder
        With csScript

            .AppendLine("       $(document).ready(function () {")

            .AppendLine("           var " & objId & " = EditorInit( '" & code1.ClientID & "', '" & EditType & "', '" & theme & "');")
            .AppendLine("            $(" & objId & ").blur(function(){")
            '.AppendLine("            $('#" & tbxLeftArea.ClientID & "').blur(function(){")
            .AppendLine("                $('#" & Me.hidEditTxt.ClientID & "').val(" & objId & ".getValue());")

            .AppendLine("            });")
            .AppendLine("        });")

        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        Me.Parent.Page.ClientScript.RegisterStartupScript(Me.GetType(), "EditorInitRun" & Math.Round(Rnd() * 100) & Math.Round(Rnd() * 100) & Now.ToString("hhmmssffff"), csScript.ToString, True)

    End Sub

    Public Sub EditorInitRun(ByVal page As Page)

        Dim objId As String = "editor" & Me.ClientID
        Dim csScript As New StringBuilder
        With csScript

            .AppendLine("       $(document).ready(function () {")

            .AppendLine("           var " & objId & " = EditorInit( '" & code1.ClientID & "', '" & EditType & "', '" & theme & "');")
            .AppendLine("            $(" & objId & ").blur(function(){")
            '.AppendLine("            $('#" & tbxLeftArea.ClientID & "').blur(function(){")
            .AppendLine("                $('#" & Me.hidEditTxt.ClientID & "').val(" & objId & ".getValue());")

            .AppendLine("            });")
            .AppendLine("        });")

        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        page.ClientScript.RegisterStartupScript(Me.GetType(), "EditorInitRun" & Math.Round(Rnd() * 100) & Math.Round(Rnd() * 100) & Now.ToString("hhmmssffff"), csScript.ToString, True)

    End Sub



End Class
