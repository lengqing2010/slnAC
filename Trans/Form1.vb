Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim web As New System.Net.WebClient()
        web.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
        Dim d As Byte() = System.Text.Encoding.ASCII.GetBytes("before=aaaaaaaaaa")
        Dim res As Byte() = web.UploadData("https://www.excite.co.jp/world/chinese/", "POST", d)
        'MsgBox(System.Text.Encoding.ASCII.GetString(res))
        'Me.WebBrowser1.Document.Body.InnerHtml = System.Text.Encoding.ASCII.GetString(res)

        Me.RichTextBox1.Text = System.Text.Encoding.ASCII.GetString(res)
    End Sub
End Class
