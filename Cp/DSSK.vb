Imports Microsoft.Win32
Imports System.Windows.Forms

Public Class DSSK

    ''' <summary>
    ''' LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DSSK_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Webbrowser IE 11 で　開く (11001 (0x2EDF) Internet Explorer 11)
        MakeWebbrowserDefaultIe11("Cp", 11001)

    End Sub

    ''' <summary>
    ''' Read Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click

        'https://www.dszuqiu.com/league/198
        'Dim httpURL As New System.Uri(Me.tbxUrl.Text.Trim)
        'wb1.Url = httpURL

        GetSouceUntilComplate(Me.tbxUrl.Text.Trim)

        MsgBox("OK")

    End Sub



    Public Function GetSouceUntilComplate(ByVal url As String) As String
        wb1.Url = (New System.Uri(url))
        If WatiWebbrowserComplate(wb1, 10) Then
            Return wb1.Document.Body.InnerHtml
        Else
            Return ""
        End If
    End Function

    Public Sub Delay(Second As Double)
        Dim tempTime As DateTime = DateTime.Now
        While (tempTime.AddMilliseconds(Second).CompareTo(DateTime.Now) > 0)
            Application.DoEvents()
        End While
    End Sub

    Public Function WatiWebbrowserComplate(ByVal wb As Windows.Forms.WebBrowser, Optional ByVal watiTime As Integer = 10) As Boolean

        Dim i As Integer = 0

        Dim sUrl As String

        Dim waitTm As Integer = 0

        Do While True

            'Threading.Thread.Sleep(100)
            Delay(100)

            waitTm = waitTm + 1 * 1000

            If watiTime * 1000 <= waitTm Then
                Return False
            End If

            If wb.ReadyState = Windows.Forms.WebBrowserReadyState.Complete Then

                If wb.IsBusy = False Then

                    i = i + 1

                    If (i = 2) Then
                        sUrl = wb.Url.ToString
                        If sUrl.Contains("res") Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If

                i = 0

            End If

        Loop

    End Function





    ''' <summary>
    ''' WebbrowserのIeのVer設定する
    '''exeFirstName:
    ''' 
    '''VER:
    '''  11001 (0x2EDF) ：Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive
    '''  11000 (0x2AF8) ：Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode
    '''  10000 (0x2710) ：Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
    '''  10001 (0x2AF7) ：Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.
    '''  9999  (0x270F) ：Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
    '''  9000  (0x2328) ：Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
    '''  8888  (0x22B8) ：Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.
    '''  8000  (0x1F40) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.
    '''  7000  (0x1B58) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.
    ''' Webbrowser
    ''' </summary>
    ''' <param name="exeFirstName"></param>
    ''' <remarks></remarks>
    Public Sub MakeWebbrowserDefaultIe11(ByVal exeFirstName As String, ByVal ver As Integer)

        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", True)
        If (key Is Nothing) Then
            key.SetValue(exeFirstName & ".exe", ver, RegistryValueKind.DWord)
            key.SetValue(exeFirstName & ".vshost.exe", ver, RegistryValueKind.DWord) '调试运行需要加上，否则不起作用
        End If

        key = Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", True)
        If (key Is Nothing) Then
            key.SetValue(exeFirstName & ".exe", ver, RegistryValueKind.DWord)
            key.SetValue(exeFirstName & ".vshost.exe", ver, RegistryValueKind.DWord) '调试运行需要加上，否则不起作用
        End If

        '11001 (0x2EDF) Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive

        '11000 (0x2AF8) ：Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode

        '10000 (0x2710) ：Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.

        '10001 (0x2AF7) ：Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.

        '9999 (0x270F) ：Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.

        '9000 (0x2328) ：Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.

        '8888 (0x22B8) ：Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.

        '8000 (0x1F40) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.

        '7000 (0x1B58) ：Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.

    End Sub

End Class