Imports System.Threading

Public Class Form1


    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As String

        Dim sb As New System.Text.StringBuilder
        With sb
            .AppendLine("SELECT top 1")
            .AppendLine("    [time]")
            .AppendLine("    ,[device_name]")
            .AppendLine("FROM [ZKAccess].[dbo].[acc_monitor_log]")
            .AppendLine("WHERE pin='3006456'")
            .AppendLine("order by time desc")
        End With

        Dim DbResult As DataTable = DefaultDB.SelIt(sb.ToString).Tables(0)

        GetMsData = DbResult.Rows(0).Item(0).ToString & DbResult.Rows(0).Item(1).ToString

        sb.Clear()
        sb = Nothing
        DbResult = Nothing

    End Function

    Public mythread1 As Thread

    Public Delegate Sub ToThread(ByVal setValue As String)

    Public Sub DoCheck()

        Dim str As String = GetMsData()

        'COM.SendMail(str)
        For i = 1 To 999999

            Try
                Dim str1 = GetMsData()
                'Me.Label1.Text = str1

                Dim ivo As New ToThread(AddressOf UpdateUI)
                Invoke(ivo, str)
                ivo = Nothing

                If str1 <> str Then
                    COM.mail(str, True)
                    str = str1
                End If

                For j = 1 To 1000
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(5)
                Next


                Application.DoEvents()


            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Next

    End Sub

    Private Sub UpdateUI(ByVal value As String)
        Me.Text = value

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mythread1 = New Thread(AddressOf DoCheck)
        mythread1.Start()
    End Sub

    Private Sub Form1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        mythread1.Abort()

    End Sub


End Class
