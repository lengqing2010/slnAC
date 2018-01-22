Imports System.Configuration
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Text

Public Class ExcelIM

    Public Import1ExcelPath As String
    Public Import2Datasource As String
    Public Import3DbName As String

    Private Sub ExcelIM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub



    Public IexcelPath As String
    Private Sub ExcelImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IexcelPath = tbxExcelPath.text()



    End Sub


    '使用可シート判定
    Private Function IsImportSheet(ByVal thisSheet As Excel.Worksheet) As Boolean
        Dim sheetName As String = thisSheet.Name
        Return "変更履歴\ENV\構成定義\(古)\(旧)".IndexOf(sheetName) < 0
    End Function
    '明細SheetのStart indexを取得する
    Private Function GetMsStartIndex(ByVal thisSheet As Excel.Worksheet) As Integer
        If C.GetObjValue(thisSheet.Cells(6, 1).value).IndexOf("テーブル名") <> -1 Then
            Return 9
        ElseIf C.GetObjValue(thisSheet.Cells(7, 1)).IndexOf("テーブル名") <> -1 Then
            Return 10
        Else
            Return -1
        End If
    End Function

    Private Function IsCanBeReadRow(ByVal cell As Excel.Range) As Boolean

        '灰色
        If cell.Interior.Color = 8421504 OrElse cell.Interior.Color = 12632256 Then
            Return False
        End If

        '取消线文字
        If cell.Font.Strikethrough() Then
            Return False
        End If

        Return True

    End Function


    Function InsExcelToTable(ByVal ThisWorkbook As Excel.Workbook) As String

        Import1ExcelPath = Me.tbxExcelPath.Text
        Import2Datasource = Me.tbxDataSource.Text
        Import3DbName = Me.tbxDbName.Text


        Dim rtv As New StringBuilder

        For sheetIdx As Integer = 1 To ThisWorkbook.Sheets.Count

            Dim thisSheet As Excel.Worksheet = ThisWorkbook.Sheets(sheetIdx)

            '変更履歴\ENV\構成定義\(古)\(旧) 以外のSHEET Import
            If IsImportSheet(thisSheet) Then

                '明細SheetのStart indexを取得する
                Dim startMSRow As Integer = GetMsStartIndex(thisSheet)
                If startMSRow = -1 Then
                    Continue For
                End If

                Dim sbSql As New System.Text.StringBuilder
                Dim tableNameStartIdx As Integer = 5
                Dim userName As String = ""

                Dim DBrtv As Boolean = True


                For kmIdx As Integer = startMSRow To 700

                    If thisSheet.Cells(kmIdx, 2).value = "" Then
                        Exit For
                    End If

                    '灰色删除的背景色 取消线
                    If Not IsCanBeReadRow(thisSheet.Cells(kmIdx, 2)) Then
                        Continue For
                    End If


                    '削除
                    If kmIdx = startMSRow Then
                        sbSql.AppendLine("  DELETE")
                        sbSql.AppendLine("  FROM [auto_code].[dbo].[t_table_info]")
                        sbSql.AppendLine("  WHERE")
                        sbSql.AppendLine("	[data_source] = '" & Me.Import2Datasource & "' ")
                        sbSql.AppendLine("	AND [db_name] = '" & Me.Import3DbName & "' ")
                        sbSql.AppendLine("	AND [table_en] = '" & thisSheet.Cells(tableNameStartIdx + 1, 3).value & "' ")

                    End If

                    Dim pj As String
                    pj = GetSQLKMValue(thisSheet.Cells(2, 3).value)

                    Dim kmEnglish As String
                    kmEnglish = GetSQLKMValue(thisSheet.Cells(kmIdx, 3).value)

                    Dim kmJP As String
                    kmJP = GetSQLKMValue(thisSheet.Cells(kmIdx, 2).value)

                    Dim kmType As String
                    kmType = GetSQLKMValue(thisSheet.Cells(kmIdx, 4).value)

                    Dim kmKeta As String
                    kmKeta = GetSQLKMValue(thisSheet.Cells(kmIdx, 5).value)
  
                    Dim kmKey As String
                    kmKey = GetSQLKMValue(thisSheet.Cells(kmIdx, 6).value)
  
                    Dim kmNULL As String
                    kmNULL = GetSQLKMValue(thisSheet.Cells(kmIdx, 7).value)

                    sbSql.AppendLine("INSERT INTO [auto_code].[dbo].[t_table_info] SELECT")
                    sbSql.AppendLine("'" & Me.Import2Datasource & "'")
                    sbSql.AppendLine(",'" & Me.Import3DbName & "'")
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(tableNameStartIdx + 1, 3).value) & "'") 'テーブル名E table_en
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(tableNameStartIdx, 3).value) & "'")     'テーブル名称 table_jp
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(tableNameStartIdx, 6).value) & thisSheet.Cells(tableNameStartIdx + 1, 6).value & "'") 'テーブル説明'table_exp
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(kmIdx, 1).value) & "'")    '項目No
                    sbSql.AppendLine(",'" & kmEnglish & "'")                                         '項目English 名
                    sbSql.AppendLine(",'" & kmJP & "'")    '項目名
                    sbSql.AppendLine(",'" & kmType & "'")
                    sbSql.AppendLine(",'" & kmKeta & "'")                                            '桁数
                    sbSql.AppendLine(",'" & IIf(IsNotEmpty(kmKey), "1", "0").ToString & "'") 'item_key
                    sbSql.AppendLine(",'" & IIf(IsNotEmpty(kmNULL), "1", "0").ToString & "'") 'item_not_null
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(kmIdx, 8).value) & "'") 'item_index
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(kmIdx, 9).value) & "'") 'item_syoki
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(kmIdx, 11).value) & "'") 'item_exp
                    sbSql.AppendLine(",'" & GetSQLKMValue(thisSheet.Cells(kmIdx, 11).value) & "'") 'item_exp

                Next

                Dim CMsSql As New CMsSql
         
                CMsSql.ExecuteNonQuery(sbSql.ToString)
      
                If CMsSql.Result Then
                    CMsSql.CloseCommit()
                    CMsSql.CloseAll()
                    rtv.AppendLine(ThisWorkbook.Name & ".⇒" & thisSheet.Name & ":OK")
                Else
                    CMsSql.CloseRollback()
                    CMsSql.CloseAll()
                    rtv.AppendLine(ThisWorkbook.Name & ".⇒" & thisSheet.Name & ":NG")
                    rtv.AppendLine(CMsSql.errMsg)
                End If

                sbSql.Length = 0

            End If

        Next

        Return rtv.ToString

    End Function

    Public Function GetSQLKMValue(ByVal v As Object) As String

        If v Is Nothing Then
            Return ""
        End If

        Dim str As String = v.ToString.Replace("'", "’")

        Return str

    End Function


    Private Sub btnImportTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportTable.Click


        Dim path As String = Me.tbxExcelPath.Text.Trim



        Dim LogName As String = "コンペア結果ログ" & Now.ToString("yyyymmddHHmmss") & ".txt"

        'ファイル
        If File.Exists(path) Then

            '*****Excel Object
            Dim ThisApplication As Excel.Application = Me.NewExcelApp()
            Dim ThisWorkbook As Excel.Workbook
            '*****Open  Excel
            ThisWorkbook = ThisApplication.Workbooks.Open(path)
            InsExcelToTable(ThisWorkbook)
            System.IO.File.WriteAllText(LogName, InsExcelToTable(ThisWorkbook))
            ThisWorkbook.Close(False)
            System.Diagnostics.Process.Start(LogName)


            '*****Close Excel
            Try
                NAR(ThisWorkbook)
                ThisApplication.Quit()
                NAR(ThisApplication)
                GC.Collect()
            Catch ex As Exception
            End Try
            System.Diagnostics.Process.Start(LogName)
        ElseIf Directory.Exists(path) Then

            '*****Excel Object
            Dim ThisApplication As Excel.Application = Me.NewExcelApp()
            Dim ThisWorkbook As Excel.Workbook

            Dim strFile As String() = System.IO.Directory.GetFiles(path)
            Dim rtvText As New StringBuilder

            If strFile.Length > 0 Then
                For i As Integer = 0 To strFile.Length - 1
                    '*****Open  Excel
                    If Microsoft.VisualBasic.Right(strFile(i), 4).ToLower = ".xls" OrElse Microsoft.VisualBasic.Right(strFile(i), 5).ToLower = ".xlsx" Then
                        ThisWorkbook = ThisApplication.Workbooks.Open(strFile(i))
                        rtvText.AppendLine(InsExcelToTable(ThisWorkbook))
                        System.IO.File.WriteAllText(LogName, rtvText.ToString)
                        ThisWorkbook.Close(False)
                    End If


                Next
            End If

            '*****Close Excel
            Try
                NAR(ThisWorkbook)
                ThisApplication.Quit()
                NAR(ThisApplication)
                GC.Collect()
            Catch ex As Exception
            End Try
            System.Diagnostics.Process.Start(LogName)
        Else
            MessageBox.Show("パスがありません")
        End If




        '*****Read  Excel

    End Sub

    Public Function NewExcelApp() As Excel.Application
        Dim ExcelApplication As New Excel.Application
        ExcelApplication.EnableEvents = False
        ExcelApplication.Visible = False         'Excel 表示
        ExcelApplication.DisplayAlerts = False
        ExcelApplication.UserControl = False
        Return (ExcelApplication)
    End Function

    Private Sub NAR(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Function IsNotEmpty(ByVal v As Object) As Boolean
        If v Is Nothing Then
            Return False
        ElseIf v.ToString.Trim = "" Then
            Return False
        End If
        Return True
    End Function


End Class