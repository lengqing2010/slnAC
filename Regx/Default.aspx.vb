Imports System.IO

Partial Class _Default
    Inherits System.Web.UI.Page


    Public dic As New Dictionary(Of String, List(Of List(Of String)))

    Protected Sub btnSerch_Click(sender As Object, e As System.EventArgs) Handles btnSerch.Click
        Dim sb As New StringBuilder
        Dim path As String = Me.tbxPath.Text.Trim

        If File.Exists(path) Then


            Dim lst As List(Of List(Of String)) = ReadRowsByKey(path)
            'Dim lst1 As List(Of List(Of String)) = GetTxtByKey(0, Me.tbxKey1.Text, lst)
            'Dim lst2 As List(Of List(Of String)) = GetTxtByKey(0, Me.tbxKey2.Text, lst1)
            'Dim lst3 As List(Of List(Of String)) = GetTxtByKey(0, Me.tbxKey3.Text, lst2)

            Dim lsttmp As List(Of List(Of String)) = lst

            For j As Integer = 1 To 30

                If Me.FindControl("tbxKey" & j.ToString) Is Nothing Then
                    Continue For
                End If

                If CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(0) = "root" Then
                    lsttmp = lst
                ElseIf CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(0) = "in" Then

                    Dim key1 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(1)
                    Dim key2 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(2)
                    Dim key3 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(3)
                    Dim mylst1 = dic(key1)
                    Dim mylst2 = dic(key2)

                    Dim ls As New List(Of List(Of String))
                    For x As Integer = 0 To mylst1.Count - 1
                        For y As Integer = 0 To mylst2.Count - 1
                            If mylst1(x)(1) = mylst2(y)(1) Then
                                ls.Add(GetTowCellLst(mylst1(x)(0), mylst1(x)(1)))


                            End If
                        Next
                    Next
                    dic.Add(key3, ls)
                ElseIf CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(0) = "show" Then

                    sb.AppendLine("'-------------------------------------------------------------------")
                    sb.AppendLine("'" & path)
                    sb.AppendLine("'-------------------------------------------------------------------")
                    Dim key1 As String = CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text.Split("|")(1)
                    Dim lstmp As List(Of List(Of String)) = dic(key1)
                    For i As Integer = 0 To lstmp.Count - 1

                        If i > 0 Then
                            If lstmp(i)(0) = lstmp(i - 1)(0) Then
                                Continue For
                            End If
                        End If

                        sb.AppendLine(lstmp(i)(0) & "行")
                        sb.AppendLine(lstmp(i)(1))
                        If CInt(lstmp(i)(0)) > 0 Then
                            sb.AppendLine(lst(CInt(lstmp(i)(0)) - 1)(1))
                        End If

                        sb.AppendLine(lst(CInt(lstmp(i)(0)))(1))
                        Try
                            sb.AppendLine(lst(CInt(lstmp(i)(0)) + 1)(1))
                            sb.AppendLine(lst(CInt(lstmp(i)(0)) + 2)(1))
                        Catch ex As Exception

                        End Try
       
                        sb.AppendLine("'↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑")
                    Next

                Else
                    lsttmp = GetTxtByKey(0, CType(Me.FindControl("tbxKey" & j.ToString), TextBox).Text, lsttmp)
                End If


            Next




            'For Each kvp As KeyValuePair(Of String, List(Of List(Of String))) In dic
            '    Dim lstmp As List(Of List(Of String)) = kvp.Value
            '    For i As Integer = 0 To lstmp.Count - 1
            '        sb.AppendLine(lstmp(i)(0))
            '        sb.AppendLine(lstmp(i)(1))

            '    Next
            'Next







            'For i As Integer = 0 To lst.Count - 1
            '    Dim txt As String = GetTxtByKey(0, Me.tbxKey1.Text, lst)
            'Next
            WucEditor.TEXT = sb.ToString

        ElseIf Directory.Exists(path) Then

        Else

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

    Private Function GetTxtByKey(ByVal keyIdx As Integer, ByVal key As String, ByVal lst As List(Of List(Of String))) As List(Of List(Of String))

        If key = "" Then
            Return lst
        End If
        Try
            Dim cmdBefore() As String = key.Split("|")(0).Split(",")
            Dim cmdAfter() As String = key.Split("|")(1).Split(",")
            Dim tmpkey As String = key.Split("|")(2)


            Dim outLst As New List(Of List(Of String))

            For i As Integer = 0 To lst.Count - 1
                Dim idx As String = lst(i)(0)
                Dim txt As String = ""
                txt = UserBeforeKey(cmdBefore, lst(i)(1))
                txt = UserAfterKey(cmdAfter, tmpkey, txt)
                If txt = "" Then
                Else
                    outLst.Add(GetTowCellLst(idx, txt))


                End If
            Next

            If cmdBefore(0) = "save" Then
                dic.Add(cmdAfter(0), outLst)
            End If


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
