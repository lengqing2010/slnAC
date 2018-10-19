Imports System.Configuration
Imports Microsoft.VisualBasic
Imports Microsoft.Win32
Imports System.Threading

Public Class MainFraN



    Private Function GetNewWebBroswer(url As String _
                                      , name As String _
                                      , Width As Integer _
                                      , Height As Integer ) As WebBrowser
        Dim wb As New WebBrowser
        wb.Url = New System.Uri(url)
        wb.Name = name
        wb.Width = Width
        wb.Height = Height
        wb.ScrollBarsEnabled = True
        wb.ScriptErrorsSuppressed = True

        'If myDelegate Is Nothing Then
        '    'AddHandler wb.DocumentCompleted, AddressOf Me.WebTabPage_Complete

        '    'AddHandler wb.DocumentCompleted, AddressOf myDelegate
        'End If

        Return wb

    End Function

    Private Sub MainFra_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim ChangeWebBroswerIEVer As New ChangeWebBroswerIEVer
        ChangeWebBroswerIEVer.SetWebBrowserFeatures(11)

        Me.Width = 1024
        Me.Height = 768



        TabCtrl.TabPages.Clear()
        Me.TabFile.TabPages.Clear()


        'Dim list As New List(Of List(Of String))

        Dim idx As Integer = 0
        For i As Integer = 0 To System.Configuration.ConfigurationManager.AppSettings.Count - 1
            Dim key As String = _
                System.Configuration.ConfigurationManager.AppSettings.AllKeys(i)
            If key.Substring(0, 4) = "URL_" Then
                Dim url As String = _
                    System.Configuration.ConfigurationManager.AppSettings(key)
                Dim tabName As String = key.Substring(4, key.Length - 4)

                TabCtrl.TabPages.Add(tabName)
                Dim wb As WebBrowser = GetNewWebBroswer(url, "wb_URL_" & idx, Me.Width, Me.Height)
                AddHandler wb.DocumentCompleted, AddressOf Me.WebTabPage_Complete
                TabCtrl.TabPages(idx).Controls.Add(wb)


                idx += 1
            End If
        Next

        idx = 0
        For i As Integer = 0 To System.Configuration.ConfigurationManager.AppSettings.Count - 1
            Dim key As String = _
                System.Configuration.ConfigurationManager.AppSettings.AllKeys(i)
            If key.Substring(0, 4) = "FIL_" Then
                Dim url As String = _
                    System.Configuration.ConfigurationManager.AppSettings(key)
                Dim tabName As String = key.Substring(4, key.Length - 4)

                TabFile.TabPages.Add(tabName)

                'Dim wb As New WebBrowser
                'wb.Url = New System.Uri(url)
                'wb.Name = "wb_FIL_" & idx
                'wb.Width = Me.Width
                'wb.Height = Me.Height
                'wb.ScrollBarsEnabled = True
                'wb.ScriptErrorsSuppressed = True
                Dim wb As WebBrowser = GetNewWebBroswer(url, "wb_FIL_" & idx, Me.Width, Me.Height)
                AddHandler wb.DocumentCompleted, AddressOf Me.WebTabPage_Complete
                TabFile.TabPages(idx).Controls.Add(wb)
                idx += 1
            End If
        Next



        If TabMain.SelectedTab IsNot Nothing Then
            Dim tb As TabControl = TabMain.SelectedTab.Controls(0)
            Dim wb As WebBrowser = tb.SelectedTab.Controls(0)
            Me.url_tbx_url.Text = wb.Url.ToString
        End If

        ''获取Configuration对象
        'Dim config As Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        ''根据Key读取<add>元素的Value
        'Dim name As String = config.AppSettings.Settings("name").Value
        ''写入<add>元素的Value
        'config.AppSettings.Settings("name").Value = "xieyc"
        ''增加<add>元素
        'config.AppSettings.Settings.Add("url", "http://www.xieyc.com")
        ''删除<add>元素
        'config.AppSettings.Settings.Remove("name")
        ''一定要记得保存，写不带参数的config.Save()也可以
        'config.Save(ConfigurationSaveMode.Modified)
        ''刷新，否则程序读取的还是之前的值（可能已装入内存）
        'System.Configuration.ConfigurationManager.RefreshSection("appSettings")

        'TabCtrl
    End Sub

    Private Sub MainFra_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        TabCtrl.Width = Me.Width
        TabCtrl.Height = Me.Height
        TabFile.Width = Me.Width
        TabFile.Height = Me.Height
        TabMain.Width = Me.Width
        TabMain.Height = Me.Height


        For Each tp As TabPage In TabCtrl.TabPages
            For Each ctrl As Control In tp.Controls
                If ctrl.Name.Contains("wb_") Then
                    ctrl.Width = Me.Width - 20
                    ctrl.Height = Me.Height - 20
                End If
            Next
        Next

        For Each tp As TabPage In TabFile.TabPages
            For Each ctrl As Control In tp.Controls
                If ctrl.Name.Contains("wb_") Then
                    ctrl.Width = Me.Width - 100
                    ctrl.Height = Me.Height - 100
                End If
            Next
        Next

    End Sub







    Private Sub WebTabPage_Complete(sender As Object, e As EventArgs)

        Dim wb As WebBrowser = sender
        Me.url_tbx_url.Text = wb.Url.ToString

    End Sub

    Private Sub TabCtrl_MarginChanged(sender As Object, e As EventArgs) Handles TabCtrl.MarginChanged

    End Sub

    Private Sub TabCtrl_Selected(sender As Object, e As TabControlEventArgs) Handles TabCtrl.Selected

        If TabCtrl.SelectedTab IsNot Nothing Then

            For Each ctrl As Control In TabCtrl.SelectedTab.Controls
                If ctrl.Name.Contains("wb_") Then
                    Me.url_tbx_url.Text = CType(ctrl, WebBrowser).Url.ToString
                End If
            Next

        End If

    End Sub





    Private Sub TabFile_Selected(sender As Object, e As TabControlEventArgs) Handles TabFile.Selected
        If TabFile.SelectedTab IsNot Nothing Then

            For Each ctrl As Control In TabFile.SelectedTab.Controls
                If ctrl.Name.Contains("wb_") Then
                    Me.url_tbx_url.Text = CType(ctrl, WebBrowser).Url.ToString
                End If
            Next

        End If
    End Sub


    Private Sub TabMain_Selected(sender As Object, e As TabControlEventArgs) Handles TabMain.Selected
        If TabMain.SelectedTab IsNot Nothing Then
            Dim tb As TabControl = TabMain.SelectedTab.Controls(0)
            Dim wb As WebBrowser = tb.SelectedTab.Controls(0)
            Me.url_tbx_url.Text = wb.Url.ToString
        End If
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If TabMain.SelectedTab IsNot Nothing Then
            Dim tb As TabControl = TabMain.SelectedTab.Controls(0)
            Dim wb As WebBrowser = tb.SelectedTab.Controls(0)
            If wb.CanGoBack Then
                wb.GoBack()
                'While wb.ReadyState <> WebBrowserReadyState.Complete
                '    Thread.Sleep(100)
                'End While
                Me.url_tbx_url.Text = wb.Url.ToString
            End If
        End If
    End Sub




End Class
