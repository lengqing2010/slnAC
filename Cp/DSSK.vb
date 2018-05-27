Public Class DSSK

    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click

        Dim httpURL As New System.Uri(Me.tbxUrl.Text.Trim)

        wb1.Url = httpURL

        'Webbrosser js error
        'https://www.cnblogs.com/lefour/p/7097906.html
        '64位系统下的32位程序，在HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION 添加自己应用程序DWORD键，值改为2AF9。 



    End Sub
End Class