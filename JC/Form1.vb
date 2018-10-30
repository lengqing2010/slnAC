Imports System.Threading

Public Class Form1


    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData(ByRef li As String, ByRef gu As String) As String

        Dim sb As New System.Text.StringBuilder
        Dim sbRtv As New System.Text.StringBuilder
        With sb
            .AppendLine("SELECT top 20")
            .AppendLine("	 Case when [pin] = '3003499' THEN 'LLL' ELSE 'SSS' END NM")

            .AppendLine("	 ,CONVERT(varchar(100), [time], 120)  + ' : ' + LEFT([device_name],3)+right([device_name],1) DEV")
            .AppendLine("	 ,right([device_name],1) + LEFT([device_name],2) dousa")
            .AppendLine("FROM [ZKAccess].[dbo].[acc_monitor_log]")
            .AppendLine("WHERE pin='3006456' OR pin='3003499'")
            .AppendLine("order by time desc")
        End With


        Dim DbResult As DataTable = DefaultDB.SelIt(sb.ToString).Tables(0)

        If DbResult.Select("NM='LLL'").Count > 0 Then
            li = DbResult.Select("NM='LLL'")(0).Item("dousa")
        End If

        If DbResult.Select("NM='SSS'").Count > 0 Then
            gu = DbResult.Select("NM='SSS'")(0).Item("dousa")
        End If


        For i As Integer = 0 To DbResult.Rows.Count - 1
            sbRtv.Append(DbResult.Rows(i).Item("NM").ToString & "：" & DbResult.Rows(i).Item("DEV").ToString & "|")
        Next
        GetMsData = sbRtv.ToString

        sb.Clear()
        sb = Nothing
        DbResult = Nothing
        sbRtv.Length = 0
        sbRtv = Nothing

    End Function

    Public mythread1 As Thread

    Public Delegate Sub ToThread(ByVal setValue As String, ByVal dousaLi As String, ByVal dousaGu As String)

    Public Sub DoCheck()

        Dim dousaLi, dousaGu

        Dim str As String = GetMsData(dousaLi, dousaGu)
        Dim str1 As String

        Dim ivo2 As New ToThread(AddressOf UpdateUI)
        Invoke(ivo2, str, dousaLi, dousaGu)
        ivo2 = Nothing

        For i = 1 To 999999

            Try

                str1 = GetMsData(dousaLi, dousaGu)
                If str1.Split("|"c)(0) <> str.Split("|"c)(0) Then
                    Dim ivo As New ToThread(AddressOf UpdateUI)
                    Invoke(ivo, str1, dousaLi, dousaGu)
                    ivo = Nothing
                    COM.mail(str1, True)
                    str = str1
                End If

                For j = 1 To 1000
                    Application.DoEvents()

                    If Now.Hour > 1 And Now.Hour < 6 Then
                        System.Threading.Thread.Sleep(30)
                    Else
                        System.Threading.Thread.Sleep(2)
                    End If

                Next

                Application.DoEvents()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Next

    End Sub

    Private Sub UpdateUI(ByVal value As String, ByVal dousaLi As String, ByVal dousaGu As String)
        Me.Text = value
        Dim arr() As String
        arr = value.Split("|")

        Dim Lrtv, Strv

        Lrtv = ""
        Strv = ""

        Me.TextBox1.Text = "LL：" & dousaLi
        Me.TextBox2.Text = "SS：" & dousaGu
        Me.TextBox3.Text = dousaLi & "L:::S" & dousaGu

        If Me.TextBox1.Text.IndexOf("入") >= 0 Then
            Me.TextBox1.BackColor = Color.LawnGreen
        Else
            Me.TextBox1.BackColor = Color.Red
        End If
        If Me.TextBox2.Text.IndexOf("入") >= 0 Then
            Me.TextBox2.BackColor = Color.LawnGreen
        Else
            Me.TextBox2.BackColor = Color.Red
        End If

        If dousaLi = dousaGu Then
            Me.TextBox3.BackColor = Color.LawnGreen
        Else
            Me.TextBox3.BackColor = Color.Red
        End If

        Me.ListBox1.Items.Clear()
        For i As Integer = 0 To arr.Count - 1
            Me.ListBox1.Items.Add(arr(i))

            'If arr(i).IndexOf("SSS") > 0 Then
            '    If Strv = "" Then
            '        Strv &= "SSS"
            '        If arr(i).Substring(0, 1) = "入" Then
            '            Strv &= "SSS"
            '        Else

            '        End If
            '    End If

            'ElseIf arr(i).IndexOf("LLL") > 0 Then

            'End If
        Next

        'For i As Integer = 1 To Me.ListBox1.Items.Count - 1

        '    Me.ListBox1.Items(0).


        '    arr(i) = Trim(arr(i))
        '    If arr(i).IndexOf("SSS") > 0 Then
        '        Exit For
        '    End If
        'Next



        'For i As Integer = 0 To arr.Count - 1
        '    arr(i) = Trim(arr(i))
        '    If arr(i).IndexOf("SSS") > 0 AndAlso arr(i).Substring(0, 1) = "入" Then
        '        Me.ListBox1.BackColor = Color.LawnGreen
        '        Exit For
        '    ElseIf arr(i).IndexOf("SSS") > 0 AndAlso arr(i).Substring(0, 1) = "入" Then
        '        Me.ListBox1.BackColor = Color.Red
        '        Exit For
        '    Else
        '        Me.ListBox1.BackColor = Color.White
        '    End If
        'Next


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mythread1 = New Thread(AddressOf DoCheck)
        mythread1.Start()
    End Sub

    Private Sub Form1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        mythread1.Abort()

    End Sub


End Class
