Imports System.IO
Imports System.Data

Partial Class SA2MsgCheck
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim path As String = "H:\WEBSHARE\sa_janpan\SAWebUI"

        Dim dt As DataTable = SetFilesDt()

        GetAllFiles(path, dt)

        gv.DataSource = dt

        gv.DataBind()



    End Sub

    Private Function SetFilesDt() As DataTable

        Dim filesDt As New DataTable
        filesDt.Columns.Add("no")
        filesDt.Columns.Add("FullName")
        filesDt.Columns.Add("firstSameStr")
        filesDt.Columns.Add("txt")
        filesDt.Columns.Add("key")
        Return filesDt

    End Function



    Private Sub GetAllFiles(ByVal path As String _
                            , ByRef filesDt As DataTable)  '搜索所有目录下的文件

        If Not (path Is Nothing) Then

            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(path)
            Try

                For Each mFileInfo In mDirInfo.GetFiles("*.*")

                    If ChkFile(mFileInfo.FullName, filesDt) Then

                    End If
   

                Next


                For Each mDir In mDirInfo.GetDirectories

                    GetAllFiles(mDir.FullName, filesDt)
                Next
            Catch ex As System.IO.DirectoryNotFoundException
                'Debug.Print("目录没找到：" + ex.Message)
            End Try
        End If

    End Sub

    Public Function ChkFile(ByVal filePath As String, ByRef dt As DataTable) As Boolean

        Dim sr As IO.StreamReader = New IO.StreamReader(filePath, System.Text.Encoding.Default)
        'Dim MyTxtReader = TxtReader.ReadToEnd.ToLower

        Dim onchageflg As Boolean = False
        Dim onkeydownflg As Boolean = False
        Dim ls As New List(Of String)

        Dim line As String
        Dim line0 As String
        Dim no As Integer = 0
        Dim filesDt As DataTable = SetFilesDt()

        Do
            line0 = sr.ReadLine()
            line = Trim(line0) & ""
            no += 1

            If InStr(line, "onchange") Then
                ls.Add(line.Trim)
                onchageflg = True
                Dim dr As DataRow = filesDt.NewRow
                dr.Item("no") = no
                dr.Item("FullName") = filePath
                dr.Item("firstSameStr") = Left(line, line.IndexOf("onchange"))
                dr.Item("txt") = line
                dr.Item("key") = "onchange"
                filesDt.Rows.Add(dr)

            End If

            If InStr(line, "onkeydown") Then
                ls.Add(line.Trim)
                onkeydownflg = True
                Dim dr As DataRow = filesDt.NewRow
                dr.Item("no") = no
                dr.Item("FullName") = filePath
                dr.Item("firstSameStr") = Left(line, line.IndexOf("onkeydown"))
                dr.Item("txt") = line
                dr.Item("key") = "onkeydown"
                filesDt.Rows.Add(dr)
            End If

        Loop Until line0 Is Nothing


        For i As Integer = 0 To filesDt.Rows.Count - 1
            If filesDt.Rows(i).Item("key") = "onchange" Then
                Dim fss As String = filesDt.Rows(i).Item("firstSameStr").ToString.Replace("'", "''")

                Try
                    Dim drs() As DataRow = filesDt.Select("key='onkeydown' AND firstSameStr ='" & fss & "'")

                    If drs.Length >= 2 Then
                        For j As Integer = 0 To 0


                            Dim dr As DataRow = dt.NewRow
                            dr.Item("no") = drs(j).Item("no")
                            dr.Item("FullName") = drs(j).Item("FullName")
                            dr.Item("firstSameStr") = drs(j).Item("firstSameStr")
                            dr.Item("txt") = drs(j).Item("txt")
                            dr.Item("key") = "onchange/onkeydown"
                            dt.Rows.Add(dr)
                        Next
                    End If
                Catch ex As Exception
                    Dim dr As DataRow = dt.NewRow

                    dr.Item("FullName") = filePath

                    dr.Item("txt") = ex.Message

                    dt.Rows.Add(dr)
                End Try


            End If
        Next



        If onchageflg AndAlso onkeydownflg Then
            Return True
        End If

        Return False

    End Function


End Class
