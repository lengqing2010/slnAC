Option Explicit Off
Option Strict Off

Imports Microsoft.VisualBasic
Imports System.Configuration.ConfigurationManager

'サーバとClient側のファイル同期
Public Class ServerClientFileSynchro

    Private pub_serverRootPath
    Private pubFileList() As String
    Private iCount As Integer = 0

    ''' <summary>
    ''' Folder Path 
    ''' 右 \ 追加
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2018/10/10 李松涛（大連） 新規作成 </para>																															
    ''' </history>	
    Private Function RexFolderPath(ByVal path As String) As String
        If Right(path, 1) = "\" Then
            Return path
        Else
            Return path & "\"
        End If
    End Function

    ''' <summary>
    ''' Folder Path 
    ''' 右 \ 削除
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2018/10/10 李松涛（大連） 新規作成 </para>																															
    ''' </history>
    Private Function RemoveRightMk(ByVal path As String) As String
        If Right(path, 1) = "\" Then
            Return path.Substring(0, path.Length - 1)
        Else
            Return path
        End If
    End Function

    ''' <summary>
    ''' サーバファイルRead
    ''' </summary>
    ''' <param name="strDirect"></param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2018/10/10 李松涛（大連） 新規作成 </para>																															
    ''' </history>
    Private Sub GetServerAllFiles(ByVal strDirect As String, ByVal fileExtension As String)

        strDirect = RexFolderPath(strDirect)

        If Not (strDirect Is Nothing) Then
            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(strDirect)
            Try

                Dim exs() As String = Split(fileExtension, ",")

                For i As Integer = 0 To exs.Length - 1
                    For Each mFileInfo In mDirInfo.GetFiles(exs(i))
                        Dim flName As String = Replace(mFileInfo.FullName, pub_serverRootPath, "")
                        Dim flType As String = "file"
                        Dim lastWriteTime As String = mFileInfo.LastWriteTime.ToString("yyyyMMddHHmm")
                        ReDim Preserve pubFileList(iCount)
                        pubFileList(iCount) = flName
                        pubFileList(iCount) &= "," & flType
                        pubFileList(iCount) &= "," & lastWriteTime
                        iCount += 1
                    Next
                Next

                For Each mDir In mDirInfo.GetDirectories

                    Dim flName As String = Replace(mDir.FullName, pub_serverRootPath, "")
                    Dim flType As String = "folder"
                    Dim lastWriteTime As String = mDir.LastWriteTime.ToString("yyyyMMddHHmm")

                    ReDim Preserve pubFileList(iCount)
                    pubFileList(iCount) = flName
                    pubFileList(iCount) &= "," & flType
                    pubFileList(iCount) &= "," & lastWriteTime
                    iCount += 1

                    GetServerAllFiles(mDir.FullName, fileExtension)
                Next
            Catch ex As System.IO.DirectoryNotFoundException
            End Try
        End If

    End Sub

    ''' <summary>
    ''' Server Client同期
    ''' </summary>
    ''' <param name="page"></param>
    ''' <history>																
    ''' <para>2018/10/10 李松涛（大連） 新規作成 </para>																															
    ''' </history>
    Public Sub Synchro(ByVal page As Page)


        Dim serverPath As String = AppSettings("conServerDownLoadPath").ToString
        Dim clientPath As String = AppSettings("conClientDownLoadPath").ToString
        Dim fileExtension As String = AppSettings("conDownLoadFileExtension").ToString

        If clientPath.Contains("Program") OrElse clientPath.Contains("Windows") OrElse clientPath.Contains("Users") Then
            Throw New Exception
            Exit Sub
        End If

        Dim inServerPath As String = serverPath
        Dim inClientPath As String = RemoveRightMk(clientPath)
        serverPath = page.Server.MapPath(serverPath) & "\"
        pub_serverRootPath = serverPath

        clientPath = RexFolderPath(clientPath)
        GetServerAllFiles(serverPath, fileExtension)
        Dim files() As String = pubFileList


        Dim sb As New StringBuilder

        With sb
            .AppendLine("<script language='vbscript' type='text/vbscript'>")
            .AppendLine("'Common 関数")
            .AppendLine("Dim serverFileLst(),clientFileLst()")
            .AppendLine("Dim pub_IsDownloadIt,pub_WebDownLoadUrl")

            .AppendLine("'Wait time by sec")
            .AppendLine("Sub WaitSec(sec)")
            .AppendLine("    t = Timer")
            .AppendLine("    While Timer < t + sec")
            .AppendLine("    Wend")
            .AppendLine("End Sub")

            .AppendLine("'Array 判定用")
            .AppendLine("Function abc(arr)")
            .AppendLine("    Dim i")
            .AppendLine("    On Error Resume Next")
            .AppendLine("    abc=false")
            .AppendLine("    i = UBOUND(arr)")
            .AppendLine("    If Err = 0 Then abc = True")
            .AppendLine("End Function")

            'Server List 作成
            .AppendLine("Sub ReadServerFileList()")
            .AppendLine("Dim iCount")
            .AppendLine("iCount = 0")
            For i As Integer = 0 To files.Length - 1
                .AppendLine("   ReDim Preserve serverFileLst(iCount)")
                .AppendLine("   serverFileLst(iCount) = """ & files(i) & """ : iCount=iCount+1")
            Next
            .AppendLine("End Sub")



            'Client List 作成
            .AppendLine("   iCount = 0")
            .AppendLine("Function ReadClientFileList(sPath)")
            .AppendLine("	On Error Resume Next")

            .AppendLine("	Dim flName,flType,lastWriteTime,fileExtension")
            .AppendLine("    Set oFso = CreateObject(""Scripting.FileSystemObject"")  ")
            .AppendLine("    Set oFolder = oFso.GetFolder(sPath)  ")
            .AppendLine("    Set oSubFolders = oFolder.SubFolders  ")
            .AppendLine("    Set oFiles = oFolder.Files  ")
            .AppendLine("    For Each oFile In oFiles")
            .AppendLine("    	fileExtension = oFso.GetExtensionName(oFile)")
            .AppendLine("		If InStr(1,""" & fileExtension & ","", fileExtension & "","",1 ) > 0 Then")
            .AppendLine("		")
            .AppendLine("			flName = oFile.path ")
            .AppendLine("			flName = Replace(flName, """ & clientPath & """, """")")
            .AppendLine("			flType = ""file""")
            .AppendLine("			lastWriteTime = _")
            .AppendLine("				Right(""0000"" & CStr(YEAR(oFile.DateLastModified)), 4) & _")
            .AppendLine("				Right(""00"" & CStr(MONTH(oFile.DateLastModified)), 2) & _")
            .AppendLine("				Right(""00"" & Cstr(DAY(oFile.DateLastModified)), 2) & _")
            .AppendLine("				Right(""00"" & CStr(Hour(oFile.DateLastModified)), 2)  & _")
            .AppendLine("				Right(""00"" & CStr(Minute(oFile.DateLastModified)), 2)")
            .AppendLine("				")
            .AppendLine("				ReDim Preserve clientFileLst(iCount)")
            .AppendLine("				clientFileLst(iCount) = flName")
            .AppendLine("				clientFileLst(iCount) = clientFileLst(iCount) & "","" & flType")
            .AppendLine("				clientFileLst(iCount) =clientFileLst(iCount) & "","" & lastWriteTime")
            .AppendLine("				iCount = iCount + 1")
            .AppendLine("				")
            .AppendLine("		End If")
            .AppendLine("    Next  ")
            .AppendLine("      ")
            .AppendLine("    For Each oSubFolder In oSubFolders  ")
            .AppendLine("		flName = oSubFolder.path ")
            .AppendLine("		flName = Replace(flName, """ & clientPath & """, """")")
            .AppendLine("		flType = ""folder""")
            .AppendLine("		lastWriteTime = _")
            .AppendLine("			Right(""0000"" & CStr(YEAR(oSubFolder.DateLastModified)), 4) & _")
            .AppendLine("			Right(""00"" & CStr(MONTH(oSubFolder.DateLastModified)), 2) & _")
            .AppendLine("			Right(""00"" & Cstr(DAY(oSubFolder.DateLastModified)), 2) & _")
            .AppendLine("			Right(""00"" & CStr(Hour(oSubFolder.DateLastModified)), 2)  & _")
            .AppendLine("			Right(""00"" & CStr(Minute(oSubFolder.DateLastModified)), 2)")
            .AppendLine("			")
            .AppendLine("			ReDim Preserve clientFileLst(iCount)")
            .AppendLine("			clientFileLst(iCount) = flName")
            .AppendLine("			clientFileLst(iCount) = clientFileLst(iCount) & "","" & flType")
            .AppendLine("			clientFileLst(iCount) =clientFileLst(iCount) & "","" & lastWriteTime")
            .AppendLine("			iCount = iCount + 1")
            .AppendLine("        ReadClientFileList(oSubFolder.Path)")
            .AppendLine("    Next")
            .AppendLine("    Set oFolder = Nothing  ")
            .AppendLine("    Set oSubFolders = Nothing  ")
            .AppendLine("    Set oFso = Nothing  ")
            .AppendLine("    ")
            .AppendLine("End Function  ")

            'Server と Clientのファイル比較
            .AppendLine("'Server と Clientのファイル比較")
            .AppendLine("Sub HikakuServerClientFiles()")
            .AppendLine("    Dim isHaveFile,i,j,k")
            .AppendLine("    pub_IsDownloadIt = false")
            .AppendLine("    If abc(clientFileLst) = false then")
            .AppendLine("       pub_IsDownloadIt = true")
            .AppendLine("    ElseIf abc(serverFileLst) = false then")
            .AppendLine("       pub_IsDownloadIt = false")
            .AppendLine("    ElseIf UBOUND(serverFileLst) = UBOUND(clientFileLst) then")
            .AppendLine("       For i = 0 To UBOUND(serverFileLst) ")
            .AppendLine("           isHaveFile = false")
            .AppendLine("           For j = 0 To UBOUND(clientFileLst) ")
            .AppendLine("               If split(serverFileLst(i),"","")(0) = split(clientFileLst(j),"","")(0) then")
            .AppendLine("                   isHaveFile = true")
            .AppendLine("                   If split(serverFileLst(i),"","")(2) > split(clientFileLst(j),"","")(2) then")
            .AppendLine("                       pub_IsDownloadIt = true")
            .AppendLine("                       Exit For")
            .AppendLine("                   End If")
            .AppendLine("               End If")
            .AppendLine("           Next")
            .AppendLine("           If isHaveFile = false then")
            .AppendLine("               pub_IsDownloadIt = true")
            .AppendLine("           End If")
            .AppendLine("           If pub_IsDownloadIt = true then")
            .AppendLine("               Exit For")
            .AppendLine("           End If")
            .AppendLine("       Next")
            .AppendLine("    Else")
            .AppendLine("       pub_IsDownloadIt = true")
            .AppendLine("    End If")
            .AppendLine("End Sub")

            .AppendLine("Sub Setpub_WebDownLoadUrl()")
            .AppendLine("    Dim url,urlPath")
            .AppendLine("    url = window.location.href")
            .AppendLine("    urlPath = Mid(url, 1, InStrRev(url, ""/""))")
            .AppendLine("    pub_WebDownLoadUrl = urlPath & """ & inServerPath & "/""")
            .AppendLine("End Sub")

            .AppendLine("Sub NewRootFolder()")
            ' .AppendLine("	On Error Resume Next")
            .AppendLine("	Dim fso,path,i")
            .AppendLine("	Dim obj")
            .AppendLine("	Set fso = CreateObject(""Scripting.FileSystemObject"")")
            .AppendLine("	path=""" & inClientPath & """")

            .AppendLine("	    If fso.FolderExists(path) then")
            .AppendLine("	        Call fso.DeleteFolder(path,true)")
            .AppendLine("           Call WaitSec(1)")
            .AppendLine("       End If")

            .AppendLine("	    	Call fso.CreateFolder(path)")
            .AppendLine("           For i = 0 To UBOUND(serverFileLst) ")
            .AppendLine("               If split(serverFileLst(i),"","")(1) = ""folder"" then")
            .AppendLine("                   fso.CreateFolder(""" & clientPath & """ & split(serverFileLst(i),"","")(0))")
            .AppendLine("	            end if")
            .AppendLine("           Next  ")
            .AppendLine("	Set fso = Nothing")
            .AppendLine("End Sub")

            .AppendLine("Sub CreateFolder(path)")
            .AppendLine("	Dim i,fso,arrSplitPath,mPath,strDisk")
            .AppendLine("	Set fso = CreateObject(""Scripting.FileSystemObject"")")
            .AppendLine("   arrSplitPath=split(path,""\"")")
            .AppendLine("   strDisk = arrSplitPath(0)")

            .AppendLine("   For i = 1 to UBOUND(arrSplitPath)")
            .AppendLine("	    mPath = mPath & ""\"" & arrSplitPath(i)")
            .AppendLine("	    If Not fso.FolderExists(strDisk & mPath) then")
            .AppendLine("	    	fso.CreateFolder(strDisk & mPath)")
            .AppendLine("	    End if")
            .AppendLine("   Next  ")
            .AppendLine("   Set fso = Nothing  ")
            .AppendLine("End Sub")


            .AppendLine("Sub CreateVbsForDownloadFile()")
            .AppendLine("    const forwriting=2")
            .AppendLine("    Const ForAppending = 8 ")
            .AppendLine("    Set objFSO = CreateObject(""Scripting.FileSystemObject"") ")
            .AppendLine("    Set objTextFile = objFSO.OpenTextFile (""" & clientPath & "TempDownload.vbs"", forwriting, True) ")
            .AppendLine("    objTextFile.WriteLine(""Sub DownloadFile(url,target)"")")
            .AppendLine("    objTextFile.WriteLine(""	Const adTypeBinary = 1"")")
            .AppendLine("    objTextFile.WriteLine(""	Const adSaveCreateOverWrite = 2"")")
            .AppendLine("    objTextFile.WriteLine(""	Dim http"")")
            .AppendLine("    objTextFile.WriteLine(""	Dim ado"")")
            .AppendLine("    objTextFile.WriteLine(""	str=""""set http=CreateObject( """" & chr(34)  & """"Msx"""" & chr(109) & """"l2.X"""" & chr(77) & """"LHTTP"""" & chr(34)&"""")"""" "")")
            .AppendLine("    objTextFile.WriteLine(""	Execute str "")")
            .AppendLine("    objTextFile.WriteLine(""	http.open """"GET"""",url,False"")")
            .AppendLine("    objTextFile.WriteLine(""	http.send"")")
            .AppendLine("    objTextFile.WriteLine(""	str=""""set ado=CreateObject( """" & chr(34)  & chr(65) & """"DOD"""" & chr(66) & """".S"""" & chr(116) & """"ream"""" & chr(34)&"""")"""" "")")
            .AppendLine("    objTextFile.WriteLine(""	Execute str "")")
            .AppendLine("    objTextFile.WriteLine(""	ado.Type = adTypeBinary"")")
            .AppendLine("    objTextFile.WriteLine(""	ado.Open"")")
            .AppendLine("    objTextFile.WriteLine(""	ado.Write http.responseBody"")")
            .AppendLine("    objTextFile.WriteLine(""	ado.SaveToFile target"")")
            .AppendLine("    objTextFile.WriteLine(""	ado.Close"")")
            .AppendLine("    objTextFile.WriteLine(""End Sub"")")
            .AppendLine("    objTextFile.WriteLine(""Sub DelFiles(target)  "")")
            .AppendLine("    objTextFile.WriteLine(""	On Error Resume Next"")")
            .AppendLine("    objTextFile.WriteLine(""	Dim fso"")")
            .AppendLine("    objTextFile.WriteLine(""	Dim obj"")")
            .AppendLine("    objTextFile.WriteLine(""	str=""""set fso=CreateObject( """" & chr(34) & """"scrip"""" & chr(116)& """"ing.FileSystemObject""""&chr(34)&"""")"""" "")")
            .AppendLine("    objTextFile.WriteLine(""	Execute str "")")
            .AppendLine("    objTextFile.WriteLine(""	fso.deleteFile target"")")
            .AppendLine("    objTextFile.WriteLine(""	Set fso = Nothing"")")
            .AppendLine("    objTextFile.WriteLine(""End Sub"")")
            .AppendLine("    objTextFile.WriteLine(""Sub ReDownLoad(flnm)"")")
            .AppendLine("    objTextFile.WriteLine(""	Dim url,target"")")
            .AppendLine("    objTextFile.WriteLine(""	url = """""" & pub_WebDownLoadUrl & """""" & flnm"")")
            .AppendLine("    objTextFile.WriteLine(""	target = """"" & clientPath & """"" & flnm"")")
            .AppendLine("    objTextFile.WriteLine(""	Call DownloadFile(url,target)"")")
            .AppendLine("    objTextFile.WriteLine(""End Sub"")")
            .AppendLine("    objTextFile.WriteLine("""")")
            .AppendLine("    For i = 0 To UBOUND(serverFileLst) ")
            .AppendLine("       If split(serverFileLst(i),"","")(1) <> ""folder"" then")
            .AppendLine("           objTextFile.WriteLine(""Call ReDownLoad ("""""" & Split(serverFileLst(i), "","")(0) & """""")"")")
            .AppendLine("	    End If")
            .AppendLine("    Next  ")
            .AppendLine("    objTextFile.Close ")
            .AppendLine("    Set objFSO = nothing ")
            .AppendLine("End Sub")


            .AppendLine("Sub DelFilesVBS()")
            .AppendLine("	On Error Resume Next")
            .AppendLine("	Dim fso")
            .AppendLine("	Dim obj")
            .AppendLine("	Set fso = CreateObject(""Scripting.FileSystemObject"")")
            .AppendLine("	fso.deleteFile """ & clientPath & "TempDownload.vbs""")
            .AppendLine("	Set fso = Nothing")
            .AppendLine("End Sub")


            .AppendLine("Call ReadServerFileList()")
            .AppendLine("Call ReadClientFileList(""" & clientPath & """)")
            .AppendLine("Call HikakuServerClientFiles()")
            .AppendLine("Call Setpub_WebDownLoadUrl()")


            .AppendLine("</script>")


            .AppendLine("<script language=""javascript"" type=""text/javascript"">")

            .AppendLine("function BatRun(strPath) { ")
            .AppendLine("   try { ")
            .AppendLine("       var rtn;")
            .AppendLine("       var objShell = new ActiveXObject('wscript.shell'); ")
            .AppendLine("       rtn = objShell.Run(strPath,0,true); ")
            .AppendLine("       objShell = null; ")
            .AppendLine("       return rtn;")
            .AppendLine("   } ")
            .AppendLine("   catch (e1){  ")
            .AppendLine("       alert('エラーが発生しました。BatRun'+e1.message);")
            .AppendLine("   } ")
            .AppendLine("}")
            .AppendLine("function DisabledPage() { ")
            .AppendLine("   var i,j; ")
            .AppendLine("    for( i = 0;i < document.forms.length;i++){ ")
            .AppendLine("	     c_form = document.forms[i]; ")
            .AppendLine("	     for (var j = 0;j < c_form.elements.length;j++){ ")
            .AppendLine("	         if(c_form.elements[j].type == 'submit' || c_form.elements[j].type == 'button' || c_form.elements[j].type == 'select-one' || c_form.elements[j].type == 'file'){ ")
            .AppendLine("	             c_form.elements[j].disabled = true; ")
            .AppendLine("	          } ")
            .AppendLine("	     } ")
            .AppendLine("    } ")
            .AppendLine("}")

            .AppendLine("   try { ")
            .AppendLine("       if (pub_IsDownloadIt != true){")
            .AppendLine("       }else{")
            .AppendLine("         NewRootFolder();")
            .AppendLine("         CreateVbsForDownloadFile()")
            .AppendLine("         BatRun(""" & Replace(clientPath, "\", "\\") & "TempDownload.vbs"");")
            .AppendLine("         DelFilesVBS()")
            '.AppendLine("       alert('OK');")
            .AppendLine("       }")
            .AppendLine("   } ")
            .AppendLine("   catch (e1){  ")
            .AppendLine("       alert('ダウンロードエラー：'+e1.message);")
            .AppendLine("       DisabledPage() ")

            .AppendLine("   } ")
            .AppendLine("    ")
            .AppendLine("    </script>")

        End With


        Dim csType As Type = page.GetType
        Dim csName As String = "Synchro"
        page.ClientScript.RegisterStartupScript(csType, csName, sb.ToString)

    End Sub

End Class
