Imports Microsoft.VisualBasic
Imports System.IO

''' <summary>
''' ファイル操作クラス
''' </summary>
''' <remarks></remarks>
Public Class Cfile



    ''' <summary>
    ''' フォルダCOPY
    ''' </summary>
    ''' <param name="DirectorySrc"></param>
    ''' <param name="DirectoryDes"></param>
    ''' <remarks></remarks>
    Public Sub CopyDerictory(ByVal DirectorySrc As DirectoryInfo, ByVal DirectoryDes As DirectoryInfo)
        Dim strDirectoryDesPath As String = DirectoryDes.FullName & "\" & DirectorySrc.Name
        If Not Directory.Exists(strDirectoryDesPath) Then
            Directory.CreateDirectory(strDirectoryDesPath)
        End If
        Dim f, fs() As FileInfo
        fs = DirectorySrc.GetFiles()
        For Each f In fs
            Try
                File.Copy(f.FullName, strDirectoryDesPath & "\" & f.Name, False)
            Catch ex As Exception

            End Try

        Next
        Dim DirSrc, Dirs() As DirectoryInfo
        Dirs = DirectorySrc.GetDirectories()
        '递归调用自身
        For Each DirSrc In Dirs
            Dim DirDes As New DirectoryInfo(strDirectoryDesPath)
            CopyDerictory(DirSrc, DirDes)
        Next
    End Sub


    ''' <summary>
    ''' エラーメッセージ（SaveFile用）
    ''' </summary>
    ''' <remarks></remarks>
    Public PubErrMsg As String

    ''' <summary>
    ''' PathのType
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PathType
        File = 1
        Dictionay = 2
        NoType = 0
    End Enum

    ''' <summary>
    ''' 保存ファイル状態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SaveFileStatus
        OK = 1
        DirectoryError = 2
        FileError = 0
    End Enum '

    ''' <summary>
    ''' PathのType　チェックする
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsPathType(ByVal path As String) As PathType
        Dim pathSpliter() As String = path.Split("/")
        Dim length As Integer = pathSpliter.Length
        Dim lastString As String = pathSpliter(length - 1)
        If lastString.Contains(".") Then
            Return PathType.File
        Else
            Return PathType.Dictionay
        End If
    End Function

    ''' <summary>
    ''' Get Path From Path
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDirectoryPath(ByVal path As String) As String
        If IsPathType(path) = PathType.Dictionay Then
            Return path
        ElseIf IsPathType(path) = PathType.File Then
            Dim pathSpliter() As String = path.Split("/")
            Dim tmp As String = ""
            For i As Integer = 0 To pathSpliter.Length - 2
                tmp &= pathSpliter(i)
            Next
        End If

        Return ""

    End Function

    ''' <summary>
    ''' SAVE FILE
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="txt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFile(ByVal path As String, ByVal txt As String) As SaveFileStatus
        Try
            If CreateDirectory(path) Then
                System.IO.File.WriteAllText(path, txt)
                Return SaveFileStatus.OK
            Else
                Return SaveFileStatus.DirectoryError
            End If
        Catch ex As Exception
            PubErrMsg = ex.Message
            Return SaveFileStatus.FileError
        End Try
    End Function

    ''' <summary>
    ''' Create Dirctory
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDirectory(ByVal path As String) As Boolean
        Dim tmp As String = GetDirectoryPath(path)
        If tmp <> "" Then
            If Not Directory.Exists(tmp) Then
                Directory.CreateDirectory(tmp)
            End If
        Else
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' SAVEFILE 機能用
    ''' </summary>
    ''' <param name="edpNo"></param>
    ''' <param name="title"></param>
    ''' <param name="type"></param>
    ''' <param name="txt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFile(ByVal edpNo As String _
                                    , ByVal title As String _
                                    , ByVal type As String _
                                    , ByVal txt As String) As Boolean

        Dim tmpFileInfo As FileInfoCls = InitFileInfoCls(edpNo, title, type)
        If CreateDirectory(tmpFileInfo.DataEdpDirectoryPath) Then
            '存在する場合、BKファイルを作成する
            If File.Exists(tmpFileInfo.CompleteFilePath) Then
                Rename(tmpFileInfo.CompleteFilePath, tmpFileInfo.CompleteFilePath & "." & Now.ToString("yyyyMMddHHmmssffff"))
            End If
            'ファイル保存する
            If Not SaveFile(tmpFileInfo.CompleteFilePath, txt) Then
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' SaveファイルINFO
    ''' </summary>
    ''' <param name="edpNo"></param>
    ''' <param name="title"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitFileInfoCls(ByVal edpNo As String _
                                    , ByVal title As String _
                                    , ByVal type As String) As FileInfoCls

        Dim FileInfoCls As New FileInfoCls
        FileInfoCls.DataDirectoryPath = HttpRuntime.AppDomainAppPath & "DATA\"
        FileInfoCls.DataEdpDirectoryPath = FileInfoCls.DataDirectoryPath & edpNo & "\"
        FileInfoCls.CompleteFilePath = FileInfoCls.DataEdpDirectoryPath & title & "." & type
        Return FileInfoCls

    End Function


    Private paths As StringBuilder
    Private directs As StringBuilder
    ''' <summary>
    ''' 搜索所有目录下的文件 
    ''' </summary>
    ''' <param name="strDirect"></param>
    ''' <remarks></remarks>
    Public Function GetAllFilesFromDirect(ByVal strDirect As String) As String
        paths = New StringBuilder
        directs = New StringBuilder
        GetAllFiles(strDirect)
        Return paths.ToString
    End Function
    Public Function GetAllDirects(ByVal strDirect As String) As String
        paths = New StringBuilder
        directs = New StringBuilder
        GetAllFiles(strDirect)
        Return paths.ToString
    End Function

    Private Sub GetAllFiles(ByVal strDirect As String)  ' 
        If Not (strDirect Is Nothing) Then
            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(strDirect)
            Try
                For Each mFileInfo In mDirInfo.GetFiles("*.*")
                    'Debug.Print(mFileInfo.FullName)  
                    paths.AppendLine(mFileInfo.FullName)
                    'GetIncludeInfo(mFileInfo.FullName)
                Next

                'For Each mFileInfo In mDirInfo.GetFiles("*.h")
                '    GetIncludeInfo(mFileInfo.FullName)
                'Next

                For Each mDir In mDirInfo.GetDirectories
                    'Debug.Print("******目录回调*******")  
                    directs.AppendLine(mDir.FullName)
                    GetAllFiles(mDir.FullName)
                Next
            Catch ex As System.IO.DirectoryNotFoundException
                'Debug.Print("目录没找到：" + ex.Message)
            End Try
        End If
    End Sub
End Class

''' <summary>
''' SaveファイルINFO
''' </summary>
''' <remarks></remarks>
Public Class FileInfoCls
    Public CompleteFilePath As String
    Public DataDirectoryPath As String
    Public DataEdpDirectoryPath As String
    'Public DataTitleDirectoryPath As String
End Class

''' <summary>
''' 全部関数戻り値
''' </summary>
''' <remarks></remarks>
Public Class RtvInfo
    ''' <summary>
    ''' 保存ファイル状態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum StatusType
        OK = 1
        NG = 0
    End Enum '

    Public msg As String
    Public status As StatusType

End Class