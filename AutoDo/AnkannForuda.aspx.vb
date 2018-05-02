Imports System.IO
Imports System.Data

Partial Class AnkannForuda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load



        Dim filesKihon As DataTable = SetFilesDt()
        Dim filesClient As DataTable = SetFilesDt()
        Dim filesServer As DataTable = SetFilesDt()

        '基本情報のパス
        Dim kihonPath As String = HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\"
        GetAllFiles(kihonPath, kihonPath, filesKihon)

        Dim clientPath As String = "E:\案件\test\"
        GetAllFiles(clientPath, clientPath, filesClient)


        Dim joinDt As DataTable = JoinTable(filesKihon, filesClient)

        gv.DataSource = joinDt

        gv.DataBind()





    End Sub

    Private Function CreateDirs(ByVal joinDt As DataTable)

        For i As Integer = 0 To joinDt.Rows.Count - 1
            If joinDt.Rows(i).Item("Type") = "dir" Then
                Dim name As String = joinDt.Rows(i).Item("Name1")
                System.IO.Directory.CreateDirectory(name)

            End If

        Next

    End Function

    Private Function SetFilesDt() As DataTable

        Dim filesDt As New DataTable

        filesDt.Columns.Add("KeyPath")
        filesDt.Columns.Add("FullName")
        filesDt.Columns.Add("Name")
        filesDt.Columns.Add("Type")
        filesDt.Columns.Add("Level")
        filesDt.Columns.Add("LastWriteTime")

        Return filesDt

    End Function

    Private Function SetJoinFilesDt() As DataTable

        Dim filesDt As New DataTable

        filesDt.Columns.Add("KeyPath")
        filesDt.Columns.Add("FullName1")
        filesDt.Columns.Add("Name1")
        filesDt.Columns.Add("LastWriteTime1")
        filesDt.Columns.Add("FullName2")
        filesDt.Columns.Add("Name2")
        filesDt.Columns.Add("LastWriteTime2")
        filesDt.Columns.Add("Level")
        filesDt.Columns.Add("Type")


        Return filesDt

    End Function

    Private Function JoinTable(ByVal dt1 As DataTable, ByVal dt2 As DataTable) As DataTable

        Dim joinDt As DataTable = SetJoinFilesDt()

        For i As Integer = 0 To dt1.Rows.Count - 1

            Dim KeyPath As String = dt1.Rows(i).Item("KeyPath")
            Dim Name As String = dt1.Rows(i).Item("Name")
            Dim Level As String = dt1.Rows(i).Item("Level")
            Dim Type As String = dt1.Rows(i).Item("Type")

            Dim dr As DataRow = joinDt.NewRow

            dr.Item("KeyPath") = KeyPath
            dr.Item("FullName1") = dt1.Rows(i).Item("FullName")
            dr.Item("Name1") = dt1.Rows(i).Item("Name")
            dr.Item("LastWriteTime1") = dt1.Rows(i).Item("LastWriteTime")
            dr.Item("Level") = dt1.Rows(i).Item("Level")
            dr.Item("Type") = dt1.Rows(i).Item("Type")

            Dim drs() As DataRow = dt2.Select("KeyPath='" & KeyPath & "' AND Name='" & Name & "' AND Level='" & Level & "' AND Type='" & Type & "'")


            If drs.Length > 0 Then

                dr.Item("FullName2") = drs(0).Item("FullName")
                dr.Item("Name2") = drs(0).Item("Name")
                dr.Item("LastWriteTime2") = drs(0).Item("LastWriteTime")

            End If

            joinDt.Rows.Add(dr)

        Next

        Return joinDt

    End Function

    Private Sub GetAllFiles(ByVal strDirect As String _
                            , ByVal KihonPath As String _
                            , ByVal filesDt As DataTable)  '搜索所有目录下的文件

        Dim key As String = strDirect.Substring(KihonPath.Length)
        Dim level As String = key.Split("\\").Length.ToString

        If key = "" Then
            level = "0"
        End If

        If Not (strDirect Is Nothing) Then

            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(strDirect)
            Try
                For Each mFileInfo In mDirInfo.GetFiles("*.*")

                    Dim dr As DataRow = filesDt.NewRow
                    dr.Item("KeyPath") = key
                    dr.Item("FullName") = mFileInfo.FullName
                    dr.Item("Name") = mFileInfo.Name
                    dr.Item("Type") = "file"
                    dr.Item("Level") = level
                    dr.Item("LastWriteTime") = mFileInfo.LastWriteTimeUtc.ToString("yyyy/MM/dd HH:mm:ss")
                    filesDt.Rows.Add(dr)

                Next

                For Each mDir In mDirInfo.GetDirectories

                    Dim dr As DataRow = filesDt.NewRow
                    dr.Item("KeyPath") = key
                    dr.Item("FullName") = mDir.FullName
                    dr.Item("Name") = mDir.Name
                    dr.Item("Level") = level
                    dr.Item("Type") = "dir"
                    dr.Item("LastWriteTime") = mDir.LastWriteTimeUtc.ToString("yyyy/MM/dd HH:mm:ss")
                    filesDt.Rows.Add(dr)
                    'Debug.Print("******目录回调*******")  
                    GetAllFiles(mDir.FullName, KihonPath, filesDt)
                Next
            Catch ex As System.IO.DirectoryNotFoundException
                'Debug.Print("目录没找到：" + ex.Message)
            End Try
        End If

    End Sub



End Class
