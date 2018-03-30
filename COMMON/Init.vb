Imports System
Imports System.Net
Imports System.Data
'Imports System.Drawing
Imports System.Net.Sockets
Imports System.ComponentModel

Public Class Init

    '
    Public Const connStrHome As String = "Data Source=WIN7U-20150705K\R2; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=1983313a"
    Public Const connStrCompaney As String = "Data Source=10.160.200.39; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=lixil@2014"
    Public Const connStrDell As String = "Data Source=ADP1QD9478YL0O2\ILIKE; Initial Catalog=auto_code;Persist Security Info=True;User ID=sa;Password=19833130"

    'Public Shared connCom As String = connStrCompaney

    Public Shared Function connCom() As String

        If System.Net.Dns.GetHostName = "WIN7U-20150705K" Then
            Return connStrHome
        ElseIf System.Net.Dns.GetHostName = "ADP1QD9478YL0O2" Then
            Return connStrDell
        Else
            Return connStrCompaney
        End If
    End Function



End Class
