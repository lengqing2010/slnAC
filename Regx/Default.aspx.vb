Imports System.IO

Public Class Sparam
    Public key1() As String
    Public key2() As String
    Public key3() As String
    Public key4() As String
    Public searchWord As String
    Public dousa As String
    Public mark As String
End Class

Partial Class _Default
    Inherits System.Web.UI.Page


    Public dic As New Dictionary(Of String, List(Of List(Of String)))
    Public sb As New StringBuilder

    Public Function GetSparam(ByVal str As String) As Sparam

        If str.Trim = "" Then

        End If

        Dim spa As New Sparam

        If Left(LTrim(str), 3) = "do " Then
            str = LTrim(str)
            str = Right(str, str.Length - 3)

            Dim arr() As String = str.Split("|")
            spa.dousa = "do"
            For i As Integer = 0 To arr.Length - 1
                If i = 0 Then
                    spa.key1 = arr(i).Split(",")
                ElseIf i = 1 Then
                    spa.key2 = arr(i).Split(",")
                ElseIf i = 2 Then
                    spa.key3 = arr(i).Split(",")
                    spa.searchWord = arr(i)
                ElseIf i = 3 Then
                    spa.key4 = arr(i).Split(",")
                End If
            Next

            If spa.key1 Is Nothing OrElse spa.key1.Length = 0 Then spa.key1 = ("").Split(",")
            If spa.key2 Is Nothing OrElse spa.key2.Length = 0 Then spa.key2 = ("").Split(",")
            If spa.key3 Is Nothing OrElse spa.key3.Length = 0 Then spa.key3 = ("").Split(",")
            If spa.key4 Is Nothing OrElse spa.key4.Length = 0 Then spa.key4 = ("").Split(",")

        Else
            spa.dousa = "print"
            spa.mark = str
        End If


        Return spa
    End Function

    Public Function GetSparams() As List(Of Sparam)
        'Dim keyTextboxs As New List(Of TextBox)
        Dim Sparams As New List(Of Sparam)
        'For j As Integer = 1 To 30
        '    Dim tbx As TextBox = Me.FindControl("tbxKey" & j.ToString)
        '    If tbx IsNot Nothing AndAlso tbx.Text <> "" Then
        '        Sparams.Add(GetSparam(tbx.Text))
        '    End If
        'Next
        'Return Sparams

        Dim txt As String = Me.WucKey.TEXT
        'Dim arr() As String = txt.Split(vbCrLf)
        'For i As Integer = 0 To arr.Count - 1
        '    If arr(i).Trim <> "" Then
        '        Sparams.Add(GetSparam(arr(i)))
        '    End If

        'Next



        ' Dim path As String = HttpRuntime.AppDomainAppPath & "Serch.txt"

        Dim path As String = "D:\Serch.log"

        File.WriteAllText(path, txt, System.Text.Encoding.Default)

        Dim sr As IO.StreamReader = New IO.StreamReader(path, System.Text.Encoding.Default)
        ' Dim idx As Integer = 0
        Dim line0 As String
        Dim rs As New List(Of List(Of String))
        Do
            ' idx += 1
            line0 = sr.ReadLine()
            If Trim(line0) <> "" Then
                Sparams.Add(GetSparam(line0))
            End If
        Loop Until line0 Is Nothing
        sr.Close()

        File.Delete(path)
        Return Sparams



        'Dim sr As IO.StreamReader = New IO.StreamReader(Path, System.Text.Encoding.Default)
        'Dim idx As Integer = 0
        'Dim line0 As String
        'Dim rs As New List(Of List(Of String))
        'Do
        '    idx += 1
        '    line0 = sr.ReadLine()
        '    rs.Add(GetTowCellLst(idx, line0))
        'Loop Until line0 Is Nothing
        'Return rs


    End Function

    Public rootLst As List(Of List(Of String))
    Public Sparams As List(Of Sparam)

    Public Function SearchFile(ByVal path As String) As Boolean

        If File.Exists(path) Then

            rootLst = ReadRowsByKey(path)
            Dim lsttmp As List(Of List(Of String)) = rootLst



            For i As Integer = 0 To Sparams.Count - 1
                Dim spa As Sparam = Sparams(i)
                If spa.dousa = "do" Then
                    If spa.key1(0) = "root" Then
                        lsttmp = rootLst
                    ElseIf spa.key1(0) = "in" Then
                        CmdIn(spa)
                    ElseIf spa.key1(0) = "show" Then
                        CmdShow(path, spa.key2(0), spa.key3(0))
                    ElseIf spa.key1(0) = "save" Then
                        Try
                            dic.Add(spa.key2(0), lsttmp)
                        Catch ex As Exception
                            MsgBox(spa.key2(0) & "二回使用(SAVE)しました")
                            Throw New Exception
                        End Try
                    ElseIf spa.key1(0) = "joincmd" Then
                        Dim tmp As New List(Of List(Of String))
                        Dim str As String = ""
                        Dim zenGyouJoinFlg As Boolean = False
                        For j As Integer = 0 To lsttmp.Count - 1

                            If zenGyouJoinFlg Then
                                tmp(tmp.Count - 1)(1) = tmp(tmp.Count - 1)(1) & " " & lsttmp(j)(1)
                            Else
                                Dim r As New List(Of String)
                                r.Add(lsttmp(j)(0))
                                r.Add(lsttmp(j)(1))
                                tmp.Add(r)
                            End If



                            If (Right(lsttmp(j)(1), 1) = "_") Then
                                zenGyouJoinFlg = True
                            Else
                                zenGyouJoinFlg = False
                            End If
                        Next
                        lsttmp = tmp
                    Else
                        lsttmp = GetTxtByKey(spa, lsttmp)
                    End If
                ElseIf spa.dousa = "print" Then
                    'sb.AppendLine(spa.mark)--
                End If
                If i = Sparams.Count - 1 Then
                    dic.Clear()
                End If
            Next


        End If

    End Function
    Public Sub CmdIn(ByVal spa As Sparam)
        Dim mylst1 = dic(spa.key2(0))
        Dim mylst2 = dic(spa.key3(0))
        Dim ls As New List(Of List(Of String))
        For x As Integer = 0 To mylst1.Count - 1
            For y As Integer = 0 To mylst2.Count - 1
                If mylst1(x)(1) = mylst2(y)(1) Then
                    ls.Add(GetTowCellLst(mylst1(x)(0), mylst1(x)(1)))
                    ls.Add(GetTowCellLst(mylst2(y)(0), mylst2(y)(1)))
                End If
            Next
        Next
        dic.Add(spa.key4(0), ls)
    End Sub

    Public Sub CmdShow(ByVal path As String, ByVal key2 As String, ByVal key3 As String)


        Dim lstmp As List(Of List(Of String)) = dic(key2)

        If lstmp.Count > 0 Then
            sb.AppendLine("'FILE:" & path & "(" & key2 & ":" & key3 & ")")
        End If

        If cbFilenomi.Checked = False Then
            For i As Integer = 0 To lstmp.Count - 1
                'If i > 0 Then
                '    If lstmp(i)(0) = lstmp(i - 1)(0) Then
                '        Continue For
                '    End If
                'End If

                'sb.AppendLine(vbTab & lstmp(i)(0) & "行")
                'sb.AppendLine(vbTab & lstmp(i)(1))

                'If CInt(lstmp(i)(0)) > 0 Then
                '    sb.AppendLine(vbTab & rootLst(CInt(lstmp(i)(0)) - 1)(1))
                'End If

                sb.AppendLine(vbTab & lstmp(i)(0) & "行:" & rootLst(CInt(lstmp(i)(0)) - 1)(1))
                'Try
                '    sb.AppendLine(vbTab & rootLst(CInt(lstmp(i)(0)) + 1)(1))
                '    sb.AppendLine(vbTab & rootLst(CInt(lstmp(i)(0)) + 2)(1))
                'Catch ex As Exception

                'End Try

                'sb.AppendLine(vbTab & "'↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑")
            Next
        End If


    End Sub


    Protected Sub btnSerch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSerch.Click

        Dim path As String = Me.tbxPath.Text.Trim
        Sparams = GetSparams()

        If File.Exists(path) Then
            SearchFile(path)
            Me.WucEditor.TEXT = sb.ToString
        ElseIf Directory.Exists(path) Then
            GetAllSearchFile(path)
            Me.WucEditor.TEXT = sb.ToString
        End If

    End Sub

    Private Sub GetAllSearchFile(ByVal path As String)  '搜索所有目录下的文件

        If Not (path Is Nothing) Then

            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(path)
            Try

                For Each mFileInfo In mDirInfo.GetFiles("*.*")
                    SearchFile(mFileInfo.FullName)
                Next


                For Each mDir In mDirInfo.GetDirectories

                    GetAllSearchFile(mDir.FullName)
                Next
            Catch ex As System.IO.DirectoryNotFoundException
                'Debug.Print("目录没找到：" + ex.Message)
            End Try
        End If

    End Sub


    Private Function ReadTxt(ByVal path As String) As String
        If System.IO.File.Exists(path) Then
            Return File.ReadAllText(path)
        Else
            Return ""
        End If
    End Function


    Private Function ReadRowsByKey(ByVal path As String) As List(Of List(Of String))
        Dim sr As IO.StreamReader = New IO.StreamReader(path, System.Text.Encoding.Default)
        Dim idx As Integer = 0
        Dim line0 As String
        Dim rs As New List(Of List(Of String))
        Do
            idx += 1
            line0 = sr.ReadLine()
            rs.Add(GetTowCellLst(idx, line0))
        Loop Until line0 Is Nothing
        Return rs
    End Function

    Public Function GetTowCellLst(ByVal idx As String, ByVal txt As String) As List(Of String)
        Dim c As New List(Of String)
        c.Add(idx)
        c.Add(txt)
        Return c
    End Function

    Private Function GetTxtByKey(ByVal spa As Sparam, ByVal lst As List(Of List(Of String))) As List(Of List(Of String))

        If spa.key1(0) = "" AndAlso spa.key2(0) = "" Then
            Return lst
        End If


        Try

            'Dim tmpkey As String = key.Split("|")(2)


            Dim outLst As New List(Of List(Of String))

            For i As Integer = 0 To lst.Count - 1
                Dim idx As String = lst(i)(0)
                Dim txt As String = ""
                txt = UserBeforeKey(spa, lst(i)(1))
                txt = UserAfterKey(spa.key2, spa.searchWord, txt)
                If txt <> "" Then
                    outLst.Add(GetTowCellLst(idx, txt))
                End If
            Next

            'If spa.key1(0) = "save" Then
            '    dic.Add(spa.key2(0), outLst)
            'End If


            Return outLst
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    'trim,upper,lower
    Private Function UserBeforeKey(ByVal spa As Sparam, ByVal txt As String) As String

        For i As Integer = 0 To spa.key1.Count - 1
            If spa.key1(i) = "trim" Then
                txt = Trim(txt)
            ElseIf spa.key1(i) = "upper" Then
                txt = txt.ToUpper
            ElseIf spa.key1(i) = "lower" Then
                txt = txt.ToLower
            ElseIf spa.key1(i) = "replace" Then
                txt = txt.Replace(spa.key2(0), spa.key3(0))
            ElseIf spa.key1(i) = "isnotfirst" Then
                For j As Integer = 0 To spa.key2.Length - 1
                    If Left(LTrim(txt), spa.key2(j).Length) = spa.key2(j) Then
                        Return ""
                    End If
                Next
            End If
        Next


        Return txt

    End Function

    'all,0tokey,keytoend,0tokeycontainskey,keytoendcontainskey
    Private Function UserAfterKey(ByVal key() As String, ByVal srKey As String, ByVal txt As String) As String

        For i As Integer = 0 To key.Count - 1
            If key(i) = "all" Then
                Dim idx As Integer = txt.IndexOf(srKey)
                If idx >= 0 Then

                Else
                    txt = ""
                End If
            ElseIf key(i) = "0tokey" Then
                Dim idx As Integer = txt.IndexOf(srKey)
                If idx >= 0 Then
                    txt = Left(txt, idx)
                Else
                    txt = ""
                End If

            ElseIf key(i) = "keytoend" Then
                Dim idx As Integer = txt.IndexOf(srKey)
                If idx >= 0 Then
                    txt = txt.Substring(idx + srKey.Length, txt.Length - srKey.Length - idx - 1)
                Else
                    txt = ""
                End If
            ElseIf key(i) = "0tokeycontainskey" Then
                Dim idx As Integer = txt.IndexOf(srKey)
                If idx >= 0 Then
                    txt = Left(txt, idx + srKey.Length - 1)
                Else
                    txt = ""
                End If

            ElseIf key(i) = "keytoendcontainskey" Then
                Dim idx As Integer = txt.IndexOf(srKey)
                If idx >= 0 Then
                    txt = txt.Substring(idx, txt.Length - idx)
                Else
                    txt = ""
                End If
            ElseIf key(i) = "no" Then
                txt = Replace(txt, srKey, "")
            End If
        Next

        Return txt

    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


    End Sub
End Class
