Imports System.IO
Imports System.Data

Partial Class AnkannForuda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ViewState("IranFlg") = ""

            '全画面戻る時、Key値あるので、画面自動設定します
            ViewState("edp_txt") = Context.Items("edp_txt")
            ViewState("edp_no") = Context.Items("edp_no")

            ViewState("kinou_txt") = Context.Items("kinou_txt")
            ViewState("kinou_no") = Context.Items("kinou_no")

            'EDPのNoの値を設定する
            Me.ucEdpLst.DataSource = (New CDB).GetEdpList

            If Context.Items("edp_no") Is Nothing Then
                '画面1回目開く場合

            Else
                '全画面戻りの場合

                Me.ucEdpLst.Value0 = ViewState("edp_no")
                Me.ucEdpLst.Text0 = ViewState("edp_txt")

            End If


            If Request.QueryString("edp_no") Is Nothing Then

            Else
                Dim edp_no As String
                edp_no = Request.QueryString("edp_no")
                Me.ucEdpLst.Value0 = edp_no
                Me.ucEdpLst.Text0 = Request.QueryString("edp_txt")
                EdpSentaku()

            End If


        End If


        Me.ucEdpLst.OnClick = "EdpSentaku"

    End Sub

    'EDP選択　機能Dropdowlist init,
    Public Sub EdpSentaku()
        Dim dtKinou As Data.DataTable = GetEdpInfo()

        If dtKinou.Rows.Count > 0 Then
            Me.tbxClientPath.Text = dtKinou.Rows(0).Item("client_siryou_path")
            Me.tbxClientToServer.Text = dtKinou.Rows(0).Item("server_siryou_path")
        End If

    End Sub


    ''' <summary>
    ''' EDP情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdpInfo() As Data.DataTable
        Dim sb As New StringBuilder
        With sb
            .AppendLine("SELECT ")
            .AppendLine("edp_no ")
            .AppendLine(",server_siryou_path ")
            .AppendLine(",client_siryou_path ")
            .AppendLine(",code_path1 ")
            .AppendLine(",code_path2 ")
            .AppendLine(",code_path3 ")
            .AppendLine("FROM m_ankan_kihon_info")
            .AppendLine("WHERE")
            .AppendLine("          m_ankan_kihon_info.edp_no =     '" & ucEdpLst.Value0 & "'")
        End With
        Dim DbResult As DbResult = DefaultDB.SelIt(sb.ToString)
        Return DbResult.Data
    End Function


    Private Function CreateDirs(ByVal joinDt As DataTable, ByVal leftPath As String, ByVal rightPath As String) As Boolean
        For i As Integer = 0 To joinDt.Rows.Count - 1
            If joinDt.Rows(i).Item("Type") = "dir" Then
                Dim path As String = joinDt.Rows(i).Item("FullName1").ToString.Replace(leftPath, rightPath)
                If Not System.IO.Directory.Exists(path) Then
                    System.IO.Directory.CreateDirectory(path)
                    My.Computer.FileSystem.CopyDirectory(joinDt.Rows(i).Item("FullName1").ToString, (path))
                End If
            Else


            End If

        Next

        Return True

    End Function

    Private Function CopyFiles(ByVal joinDt As DataTable, ByVal leftPath As String, ByVal rightPath As String) As Boolean
        For i As Integer = 0 To joinDt.Rows.Count - 1
            If joinDt.Rows(i).Item("Type") = "dir" Then

            Else
                Dim path As String = joinDt.Rows(i).Item("FullName1").ToString.Replace(leftPath, rightPath)
                If Not System.IO.File.Exists(path) Then
                    File.Copy(joinDt.Rows(i).Item("FullName1").ToString, path, True)
                ElseIf joinDt.Rows(i).Item("LastWriteTime1").ToString > joinDt.Rows(i).Item("LastWriteTime2").ToString Then
                    File.Copy(joinDt.Rows(i).Item("FullName1").ToString, path, True)
                End If
            End If
        Next
        Return True
    End Function


    Private Function GetPreDir(ByVal path As String) As String

        Dim arr() As String = path.Split("\")
        Dim lst As New StringBuilder

        For i As Integer = 0 To arr.Length - 2
            lst.Append(arr(i) & "\")
        Next

        Return lst.ToString


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
                            , ByRef filesDt As DataTable)  '搜索所有目录下的文件

        Dim key As String = strDirect.Substring(KihonPath.Length)
        Dim level As String = key.Split("\\").Length.ToString

        If key = "" Then
            level = "0"
        End If



        If CInt(level) > CInt(ddlLevel.SelectedValue) Then
            Exit Sub
        End If

        If Not (strDirect Is Nothing) Then

            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(strDirect)
            Try
                If cbForuda.Checked = False Then
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
                End If




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



    Protected Sub btnKihonToClient_Click(sender As Object, e As System.EventArgs) Handles btnKihonToClient.Click

        Dim filesKihon As DataTable = SetFilesDt()
        Dim filesClient As DataTable = SetFilesDt()
        Dim filesServer As DataTable = SetFilesDt()
        '基本情報のパス
        Dim kihonPath As String = HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\"
        Dim clientPath As String = MkPathStr(Me.tbxClientPath.Text)
        Dim serverPath As String = MkPathStr(Me.tbxClientToServer.Text)

        Dim path As String = clientPath
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If


        Dim joinDt As DataTable

        'Kihon ⇒　Client
        GetAllFiles(kihonPath, kihonPath, filesKihon)
        GetAllFiles(clientPath, clientPath, filesClient)
        joinDt = JoinTable(filesKihon, filesClient)
        CreateDirs(joinDt, kihonPath, clientPath)
        joinDt = JoinTable(filesKihon, filesClient)
        CopyFiles(joinDt, kihonPath, clientPath)

        filesKihon.Clear()
        filesClient.Clear()
        GetAllFiles(kihonPath, kihonPath, filesKihon)
        GetAllFiles(clientPath, clientPath, filesClient)
        joinDt = JoinTable(filesKihon, filesClient)

        gv.DataSource = AddLinkColDt(joinDt, kihonPath, clientPath)
        gv.DataBind()

    End Sub

    Protected Sub btnClientToServer_Click(sender As Object, e As System.EventArgs) Handles btnClientToServer.Click

        Dim filesKihon As DataTable = SetFilesDt()
        Dim filesClient As DataTable = SetFilesDt()
        Dim filesServer As DataTable = SetFilesDt()
        '基本情報のパス
        Dim kihonPath As String = HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\"
        Dim clientPath As String = MkPathStr(Me.tbxClientPath.Text)
        Dim serverPath As String = MkPathStr(Me.tbxClientToServer.Text)


        Dim path As String = clientPath
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If

        Dim joinDt As DataTable

        'Client ⇒　Server
        GetAllFiles(clientPath, clientPath, filesClient)
        GetAllFiles(serverPath, serverPath, filesServer)
        joinDt = JoinTable(filesClient, filesServer)
        CreateDirs(joinDt, clientPath, serverPath)
        joinDt = JoinTable(filesClient, filesServer)
        CopyFiles(joinDt, clientPath, serverPath)

        filesClient.Clear()
        filesServer.Clear()

        GetAllFiles(clientPath, clientPath, filesClient)
        GetAllFiles(serverPath, serverPath, filesServer)
        joinDt = JoinTable(filesClient, filesServer)

        gv.DataSource = AddLinkColDt(joinDt, clientPath, serverPath)
        gv.DataBind()

    End Sub

    Public Function AddLinkColDt(ByRef dt As DataTable, ByVal FromPath As String, ByVal ToPath As String) As DataTable

        Dim length As Integer = FromPath.Split("\").Length

        dt.Columns.Add("link_mae")
        dt.Columns.Add("link_pic")
        dt.Columns.Add("link_1")
        dt.Columns.Add("link_2")
        dt.Columns.Add("fileCompare")
        dt.Columns.Add("copy_btn_vis")
        dt.Columns.Add("tongbuFnc")
        dt.Columns.Add("from_path")
        dt.Columns.Add("to_path")

        For i As Integer = 0 To dt.Rows.Count - 1

            Dim len As Integer = dt.Rows(i).Item("FullName1").ToString.Split("\").Length - length
            Dim maeStr As String = ""
            For j As Integer = 0 To len - 1
                maeStr &= "&nbsp;&nbsp;"
            Next

            If dt.Rows(i).Item("Type").ToString = "dir" Then
                If len = 0 Then
                    dt.Rows(i).Item("link_pic") = "img/dir_0.jpg"
                Else
                    dt.Rows(i).Item("link_pic") = "img/dir.jpg"
                End If

            Else
                dt.Rows(i).Item("link_pic") = "img/file.jpg"
            End If

            dt.Rows(i).Item("link_1") = GetLinkA(dt.Rows(i).Item("Name1").ToString, dt.Rows(i).Item("FullName1").ToString)

            dt.Rows(i).Item("link_2") = GetLinkA(dt.Rows(i).Item("Name2").ToString, dt.Rows(i).Item("FullName2").ToString)



            dt.Rows(i).Item("copy_btn_vis") = "False"

            dt.Rows(i).Item("tongbuFnc") = "Tongbu(" & i.ToString & ");return false;"


            If ViewState("IranFlg") = "kihonToClient" Then

                If dt.Rows(i).Item("LastWriteTime1").ToString <> "" AndAlso dt.Rows(i).Item("LastWriteTime2").ToString <> "" Then
                    dt.Rows(i).Item("copy_btn_vis") = "False"
                ElseIf dt.Rows(i).Item("LastWriteTime1").ToString = "" AndAlso dt.Rows(i).Item("LastWriteTime2").ToString <> "" Then
                    dt.Rows(i).Item("copy_btn_vis") = "False"
                    dt.Rows(i).Item("fileCompare") = "無新"
                Else
                    dt.Rows(i).Item("copy_btn_vis") = "True"
                    dt.Rows(i).Item("fileCompare") = "新無"
                    dt.Rows(i).Item("from_path") = dt.Rows(i).Item("FullName1").ToString
                    dt.Rows(i).Item("to_path") = ToPath & dt.Rows(i).Item("FullName1").ToString.Replace(FromPath, "")

                End If

            ElseIf ViewState("IranFlg") = "ClientToServer" Then

                If dt.Rows(i).Item("Type").ToString = "dir" Then
                    If dt.Rows(i).Item("LastWriteTime1").ToString <> "" AndAlso dt.Rows(i).Item("LastWriteTime2").ToString <> "" Then
                        dt.Rows(i).Item("copy_btn_vis") = "False"
                        dt.Rows(i).Item("fileCompare") = "同じ"
                    ElseIf dt.Rows(i).Item("LastWriteTime1").ToString = "" AndAlso dt.Rows(i).Item("LastWriteTime2").ToString <> "" Then
                        dt.Rows(i).Item("copy_btn_vis") = "True"
                        dt.Rows(i).Item("fileCompare") = "旧新"
                        dt.Rows(i).Item("from_path") = dt.Rows(i).Item("FullName2").ToString
                        dt.Rows(i).Item("to_path") = dt.Rows(i).Item("FullName1").ToString

                    Else
                        dt.Rows(i).Item("copy_btn_vis") = "True"
                        dt.Rows(i).Item("fileCompare") = "新旧"
                        dt.Rows(i).Item("from_path") = dt.Rows(i).Item("FullName1").ToString
                        dt.Rows(i).Item("to_path") = dt.Rows(i).Item("FullName2").ToString
                    End If
                Else
                    If dt.Rows(i).Item("LastWriteTime1").ToString = dt.Rows(i).Item("LastWriteTime2").ToString Then
                        dt.Rows(i).Item("copy_btn_vis") = "False"
                        dt.Rows(i).Item("fileCompare") = "同じ"
                    ElseIf dt.Rows(i).Item("LastWriteTime1").ToString <> "" AndAlso dt.Rows(i).Item("LastWriteTime2").ToString = "" Then
                    dt.Rows(i).Item("copy_btn_vis") = "True"
                        dt.Rows(i).Item("fileCompare") = "新無"
                        dt.Rows(i).Item("from_path") = dt.Rows(i).Item("FullName1").ToString
                        dt.Rows(i).Item("to_path") = ToPath & dt.Rows(i).Item("FullName1").ToString.Replace(FromPath, "")


                    ElseIf dt.Rows(i).Item("LastWriteTime1").ToString > dt.Rows(i).Item("LastWriteTime2").ToString Then
                        dt.Rows(i).Item("copy_btn_vis") = "True"
                        dt.Rows(i).Item("fileCompare") = "新旧"
                        dt.Rows(i).Item("from_path") = dt.Rows(i).Item("FullName1").ToString
                        dt.Rows(i).Item("to_path") = dt.Rows(i).Item("FullName2").ToString
                    Else
                        dt.Rows(i).Item("copy_btn_vis") = "True"
                        dt.Rows(i).Item("fileCompare") = "旧新"
                        dt.Rows(i).Item("from_path") = dt.Rows(i).Item("FullName2").ToString
                        dt.Rows(i).Item("to_path") = dt.Rows(i).Item("FullName1").ToString
                    End If
                End If

            End If

            dt.Rows(i).Item("link_mae") = maeStr

        Next
        Return dt
    End Function

    Public Function GetLinkA(ByVal name As String, ByVal link As String) As String
        Dim str As String
        If Left(link, 2) = "\\" Then
            str = "<a href=""file:" & link & """>" & name & "</a>"
        ElseIf link.Trim <> "" Then
            str = "<a>" & name & "</a>"
        Else
            str = "<a>" & name & "</a>"

        End If
        Return str

    End Function
    Public Function MkPathStr(ByVal path As String) As String

        If Right(path, 1) = "\" Then
            Return path
        Else
            Return path & "\"
        End If

    End Function

    Public Function GetFileLinkName(ByVal path As String) As String

    End Function

    Protected Sub btnKihonCompClient_Click(sender As Object, e As System.EventArgs) Handles btnKihonCompClient.Click

        ViewState("IranFlg") = "kihonToClient"

        Me.btnClientCompServer.Height = 20
        Me.btnKihonCompClient.Height = 40



        Dim filesKihon As DataTable = SetFilesDt()
        Dim filesClient As DataTable = SetFilesDt()
        Dim filesServer As DataTable = SetFilesDt()
        '基本情報のパス
        Dim kihonPath As String = HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\"
        Dim clientPath As String = MkPathStr(Me.tbxClientPath.Text)
        Dim serverPath As String = MkPathStr(Me.tbxClientToServer.Text)

        Dim path As String = clientPath
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If


        Dim joinDt As DataTable

        'Kihon ⇒　Client
        GetAllFiles(kihonPath, kihonPath, filesKihon)
        GetAllFiles(clientPath, clientPath, filesClient)
        joinDt = JoinTable(filesKihon, filesClient)

        gv.DataSource = AddLinkColDt(joinDt, kihonPath, clientPath)
        gv.DataBind()

        Me.gv.HeaderRow.Cells(0).Text = "基本"
        Me.gv.HeaderRow.Cells(1).Text = "クライアント"

    End Sub

    Protected Sub btnClientCompServer_Click(sender As Object, e As System.EventArgs) Handles btnClientCompServer.Click

        ViewState("IranFlg") = "ClientToServer"


        Me.btnClientCompServer.Height = 40
        Me.btnKihonCompClient.Height = 20

        Dim filesKihon As DataTable = SetFilesDt()
        Dim filesClient As DataTable = SetFilesDt()
        Dim filesServer As DataTable = SetFilesDt()
        '基本情報のパス
        Dim kihonPath As String = HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\"
        Dim clientPath As String = MkPathStr(Me.tbxClientPath.Text)
        Dim serverPath As String = MkPathStr(Me.tbxClientToServer.Text)


        Dim path As String = clientPath
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If

        Dim joinDt As DataTable

        'Client ⇒　Server
        GetAllFiles(clientPath, clientPath, filesClient)
        GetAllFiles(serverPath, serverPath, filesServer)
        joinDt = JoinTable(filesClient, filesServer)
        gv.DataSource = AddLinkColDt(joinDt, clientPath, serverPath)
        gv.DataBind()

        Me.gv.HeaderRow.Cells(0).Text = "クライアント"
        Me.gv.HeaderRow.Cells(1).Text = "サーバ"
    End Sub

    Protected Sub btnTongbu_Click(sender As Object, e As System.EventArgs) Handles btnTongbu.Click


        Dim idx As Integer = CInt(Me.hidIdx.Value)




        Dim from_path As String = CType(Me.gv.Rows(idx).FindControl("hid_from_path"), HiddenField).Value
        Dim to_path As String = CType(Me.gv.Rows(idx).FindControl("hid_to_path"), HiddenField).Value
        Dim type As String = CType(Me.gv.Rows(idx).FindControl("hidType"), HiddenField).Value

        If type = "dir" Then

            If Not System.IO.Directory.Exists(to_path) Then
                System.IO.Directory.CreateDirectory(to_path)
                My.Computer.FileSystem.CopyDirectory(from_path, (to_path))
            End If

        Else

            Dim predir As String = GetPreDir(to_path)
            If Not System.IO.Directory.Exists(predir) Then
                System.IO.Directory.CreateDirectory(predir)

            End If

            File.Copy(from_path, to_path, True)

        End If



        Dim filesKihon As DataTable = SetFilesDt()
        Dim filesClient As DataTable = SetFilesDt()
        Dim filesServer As DataTable = SetFilesDt()
        '基本情報のパス
        Dim kihonPath As String = HttpContext.Current.Request.PhysicalApplicationPath & "AnnkenSample\"
        Dim clientPath As String = MkPathStr(Me.tbxClientPath.Text)
        Dim serverPath As String = MkPathStr(Me.tbxClientToServer.Text)

        Dim path As String = clientPath
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If


        Dim joinDt As DataTable



        If ViewState("IranFlg") = "kihonToClient" Then
            'Kihon ⇒　Client
            GetAllFiles(kihonPath, kihonPath, filesKihon)
            GetAllFiles(clientPath, clientPath, filesClient)
            joinDt = JoinTable(filesKihon, filesClient)
            gv.DataSource = AddLinkColDt(joinDt, kihonPath, clientPath)
            gv.DataBind()

        Else
            'Client ⇒　Server
            GetAllFiles(clientPath, clientPath, filesClient)
            GetAllFiles(serverPath, serverPath, filesServer)
            joinDt = JoinTable(filesClient, filesServer)
            gv.DataSource = AddLinkColDt(joinDt, clientPath, serverPath)
            gv.DataBind()

        End If




    End Sub
End Class
