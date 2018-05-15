Imports System.IO

Public Class Sparam
    Public key1() As String
    Public key2() As String
    Public key3() As String
    Public key4() As String
    Public searchWord As String
End Class

Partial Class _Default
    Inherits System.Web.UI.Page


    Public dic As New Dictionary(Of String, List(Of List(Of String)))
    Public sb As New StringBuilder

    Public Function GetSparam(ByVal str As String) As Sparam
        Dim spa As New Sparam
        Dim arr() As String = str.Split("|")
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
        Return spa
    End Function

    Public Function GetSparams() As List(Of Sparam)
        Dim keyTextboxs As New List(Of TextBox)
        Dim Sparams As New List(Of Sparam)
        For j As Integer = 1 To 30
            Dim tbx As TextBox = Me.FindControl("tbxKey" & j.ToString)
            If tbx IsNot Nothing AndAlso tbx.Text <> "" Then
                Sparams.Add(GetSparam(tbx.Text))
            End If
        Next
        Return Sparams
    End Function

    Public rootLst As List(Of List(Of String))

    Public Function SearchFile(ByVal path As String) As Boolean

        If File.Exists(path) Then

            rootLst = ReadRowsByKey(path)
            Dim lsttmp As List(Of List(Of String)) = rootLst

            Dim Sparams As List(Of Sparam) = GetSparams()
            For i As Integer = 0 To Sparams.Count - 1
                Dim spa As Sparam = Sparams(i)
                If spa.key1(0) = "root" Then
                    lsttmp = rootLst
                ElseIf spa.key1(0) = "in" Then
                    CmdIn(spa)
                ElseIf spa.key1(0) = "show" Then
                    CmdShow(path, spa.key2(0))
                ElseIf spa.key1(0) = "save" Then
                    dic.Add(spa.key2(0), lsttmp)
                Else
                    lsttmp = GetTxtByKey(spa, lsttmp)
                End If
            Next
            Me.WucEditor.TEXT = sb.ToString
            Exit Function

            '    For j As Integer = 1 To 30

            '        If Me.FindControl("tbxKey" & j.ToString) Is Nothing Then
            '            Continue For
            '        End If

            '        If CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(0) = "root" Then
            '            lsttmp = rootLst
            '        ElseIf CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(0) = "in" Then

            '            Dim key1 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(1)
            '            Dim key2 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(2)
            '            Dim key3 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(3)
            '            Dim mylst1 = dic(key1)
            '            Dim mylst2 = dic(key2)

            '            Dim ls As New List(Of List(Of String))
            '            For x As Integer = 0 To mylst1.Count - 1
            '                For y As Integer = 0 To mylst2.Count - 1
            '                    If mylst1(x)(1) = mylst2(y)(1) Then
            '                        ls.Add(GetTowCellLst(mylst1(x)(0), mylst1(x)(1)))


            '                    End If
            '                Next
            '            Next
            '            dic.Add(key3, ls)
            '        ElseIf CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(0) = "show" Then

            '            sb.AppendLine("'-------------------------------------------------------------------")
            '            sb.AppendLine("'" & path)
            '            sb.AppendLine("'-------------------------------------------------------------------")
            '            Dim key1 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(1)
            '            Dim lstmp As List(Of List(Of String)) = dic(key1)
            '            For i As Integer = 0 To lstmp.Count - 1

            '                If i > 0 Then
            '                    If lstmp(i)(0) = lstmp(i - 1)(0) Then
            '                        Continue For
            '                    End If
            '                End If

            '                sb.AppendLine(lstmp(i)(0) & "行")
            '                sb.AppendLine(lstmp(i)(1))
            '                If CInt(lstmp(i)(0)) > 0 Then
            '                    sb.AppendLine(rootLst(CInt(lstmp(i)(0)) - 1)(1))
            '                End If

            '                sb.AppendLine(rootLst(CInt(lstmp(i)(0)))(1))
            '                Try
            '                    sb.AppendLine(rootLst(CInt(lstmp(i)(0)) + 1)(1))
            '                    sb.AppendLine(rootLst(CInt(lstmp(i)(0)) + 2)(1))
            '                Catch ex As Exception

            '                End Try

            '                sb.AppendLine("'↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑")
            '            Next

            '        Else
            '            lsttmp = GetTxtByKey(0, CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text, lsttmp)
            '        End If


            '    Next

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

    Public Sub CmdShow(ByVal path As String, ByVal key2 As String)
        sb.AppendLine("'-------------------------------------------------------------------")
        sb.AppendLine("'" & path)
        sb.AppendLine("'-------------------------------------------------------------------")
        Dim lstmp As List(Of List(Of String)) = dic(key2)
        For i As Integer = 0 To lstmp.Count - 1
            'If i > 0 Then
            '    If lstmp(i)(0) = lstmp(i - 1)(0) Then
            '        Continue For
            '    End If
            'End If

            sb.AppendLine(lstmp(i)(0) & "行")
            sb.AppendLine(lstmp(i)(1))
            If CInt(lstmp(i)(0)) > 0 Then
                sb.AppendLine(rootLst(CInt(lstmp(i)(0)) - 1)(1))
            End If

            sb.AppendLine(rootLst(CInt(lstmp(i)(0)))(1))
            Try
                sb.AppendLine(rootLst(CInt(lstmp(i)(0)) + 1)(1))
                sb.AppendLine(rootLst(CInt(lstmp(i)(0)) + 2)(1))
            Catch ex As Exception

            End Try

            sb.AppendLine("'↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑")
        Next
    End Sub


    Protected Sub btnSerch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSerch.Click

        Dim path As String = Me.tbxPath.Text.Trim
        SearchFile(path)

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

        If spa.key1.Length = 1 AndAlso spa.key1(0) = "" Then
            Return lst
        End If


        Try

            'Dim tmpkey As String = key.Split("|")(2)


            Dim outLst As New List(Of List(Of String))

            For i As Integer = 0 To lst.Count - 1
                Dim idx As String = lst(i)(0)
                Dim txt As String = ""
                txt = UserBeforeKey(spa.key1, lst(i)(1))
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
    Private Function UserBeforeKey(ByVal key() As String, ByVal txt As String) As String

        For i As Integer = 0 To key.Count - 1
            If key(i) = "trim" Then
                txt = Trim(txt)
            ElseIf key(i) = "upper" Then
                txt = txt.ToUpper
            ElseIf key(i) = "lower" Then
                txt = txt.ToLower
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

End Class
