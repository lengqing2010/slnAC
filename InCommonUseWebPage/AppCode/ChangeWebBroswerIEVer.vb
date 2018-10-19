Imports System.Configuration
Imports Microsoft.VisualBasic
Imports Microsoft.Win32
Imports System.Threading

Public Class ChangeWebBroswerIEVer
    Function SetWebBrowserFeatures(ieVersion As Integer)

        ' don't change the registry if running in-proc inside Visual Studio  
        If (System.ComponentModel.LicenseManager.UsageMode <> System.ComponentModel.LicenseUsageMode.Runtime) Then
            Return False
        End If

        '获取程序及名称  
        Dim appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
        '得到浏览器的模式的值  
        Dim ieMode As UInt32 = GeoEmulationModee(ieVersion)
        Dim featureControlRegKey = "HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\"
        '设置浏览器对应用程序（appName）以什么模式（ieMode）运行  
        Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
            appName, ieMode, RegistryValueKind.DWord)
        ' enable the features which are "On" for the full Internet Explorer browser  
        '不晓得设置有什么用  
        Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
            appName, 1, RegistryValueKind.DWord)


        'Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS",  
        '    appName, 1, RegistryValueKind.DWord)  


        'Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING",  
        '    appName, 1, RegistryValueKind.DWord)  


        'Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM",  
        '    appName, 1, RegistryValueKind.DWord)  


        'Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE",  
        '    appName, 0, RegistryValueKind.DWord)  
        Return True

    End Function

    Function GetBrowserVersion() As Integer

        Dim browserVersion = 0
        Dim ieKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer", _
                  RegistryKeyPermissionCheck.ReadSubTree, _
                  System.Security.AccessControl.RegistryRights.QueryValues)

        Dim Version = ieKey.GetValue("svcVersion")
        If (Nothing = Version) Then



            Version = ieKey.GetValue("Version")
            If (Nothing = Version) Then
                Throw New ApplicationException("Microsoft Internet Explorer is required!")
            End If

        End If
        'Return Integer.TryParse(version.ToString().Split("."c)(0), out browserVersion)

        '如果小于7  
        If (browserVersion < 7) Then
            Throw New ApplicationException("不支持的浏览器版本!")
        End If
        Return browserVersion
    End Function

    Function GeoEmulationModee(ByVal browserVersion) As UInt32



        Dim mode As UInt32 = 11000 ' Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode.   
        Select Case (browserVersion)
            Case 7
                mode = 7000 ' Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.   
            Case 8
                mode = 8000 ' Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.   

            Case 9
                mode = 9000 ' Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                      

            Case 10
                mode = 10000 ' Internet Explorer 10.  

            Case 11
                mode = 11000 ' Internet Explorer 11  

        End Select
        Return mode

    End Function
End Class
