
Imports shell32
Imports mshtml


Public Class Form1


    'Public shell As New Shell32.Shell
    Public webApp As SHDocVw.InternetExplorer




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'IE 操作
        '引用了下面3个 包
        'Microsoft Internet Controls                Interop.SHDocVw.dll
        'Microsoft HTML Object Library              Microsoft.mshtml.dll
        'Microsoft Shell Controls And Automation

        Dim url As String = "http://www.baidu.com/"

        webApp = New SHDocVw.InternetExplorer

        webApp.Navigate(url)
        webApp.Visible = True
        'Login In
        webApp.Navigate(url)
        WaitComplete(webApp)

        Dim wbDocument As mshtml.HTMLDocument = webApp.Document
        wbDocument.activeElement.innerText = "111"

        'rtbxMsg.Text = a.ActiveElement.OuterHtml


    End Sub


    Sub WaitComplete(ByRef webApp As Object)

        Do Until webApp.ReadyState = 4 And Not webApp.Busy
            System.Windows.Forms.Application.DoEvents()
            System.Threading.Thread.Sleep(500)
        Loop
        System.Threading.Thread.Sleep(100)

    End Sub

    Sub ClickHtmlElement(ByRef webApp As Object, ByVal id As String)

        WaitComplete(webApp)
        webApp.Document.getElementById(id).Click()
        'Call webApp.Document.GetElementById(id).InvokeMember("click")

        WaitComplete(webApp)

    End Sub

End Class
