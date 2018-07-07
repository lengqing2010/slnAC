Imports System.Text
Imports System.Configuration
Imports Microsoft.Office.Interop
'Imports System.Configuration.ConfigurationManager
Imports System.Data



Public Class ExcelCopy

    Public cmdStr As New System.Text.StringBuilder
    Public conSplitSign As String = "|"

    ''' <summary>
    ''' 実行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRun_Click(sender As System.Object, e As System.EventArgs) Handles btnRun.Click

        cmdStr.Length = 0
        cmdStr.Append(Me.tbxCmd.Text)

        ' 命令行取得
        Dim cmdlines() As String = GetCmdLines()

        For i As Integer = 0 To cmdlines.Length - 1

            Dim cmd As String = cmdlines(i).Trim
            RunCmd(cmd)

        Next

        MsgBox(Now.ToString("yyyyMMdd HHmmss"))

    End Sub

    ''' <summary>
    ''' 命令行取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCmdLines() As String()

        Return cmdStr.ToString.Split(vbLf)

    End Function


    ''' <summary>
    ''' 命令执行
    ''' </summary>
    ''' <param name="cmdStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunCmd(ByVal cmdStr As String) As Boolean

        Dim cmd As String = GetCmd(cmdStr)
        Dim params As List(Of String) = GetParam(cmdStr)

        If cmd = "[OpenExcel]" Then

            OpenExcel(params)
        ElseIf cmd = "[CopyRange]" Then
            CopyRange(params)

        ElseIf cmd = "[ClearCells]" Then
            ClearCells(params)

        ElseIf cmd = "[Paste]" Then
            Paste(params)

        End If

        Return True

    End Function


    Public DicExcel As New Dictionary(Of String, Excel.Application)
    Public DicWb As New Dictionary(Of String, Excel.Workbook)


    Public Function OpenExcel(params As List(Of String)) As Boolean

        Dim filePath As String = ReplaceRegx(params(0))
        Dim excelName As String = ReplaceRegx(params(1))

        DicExcel.Add(excelName, NewExcelApp())

        'Open EXCEL
        Try

            DicWb.Add(excelName, DicExcel.Item(excelName).Workbooks.Open(filePath))

        Catch ex As Exception
            ErrorSyori(ex.Message)
        End Try

        Return True

    End Function



    Public Function ClearCells(params As List(Of String)) As Boolean

        Dim excelName As String = ReplaceRegx(params(0))
        Dim sheetName As String = ReplaceRegx(params(1))

        Dim startCellRow As String = params(2).Split(",")(0)
        Dim startCellCol As String = params(2).Split(",")(1)
        Dim endCellRow As String = params(3).Split(",")(0)
        Dim endCellCol As String = params(3).Split(",")(1)

        Dim ClearOp As String = ReplaceRegx(params(4))


        DicExcel.Item(excelName).Sheets.Item(sheetName).Activate()

        With DicExcel(excelName).Sheets.Item(sheetName)



            If endCellRow = "[max]" Then
                endCellRow = .Cells(1048576, CInt(startCellCol)).End(Excel.XlDirection.xlUp).Row.ToString
            End If



            Dim int_startCellRow As Integer = CInt(startCellRow)
            Dim int_startCellCol As Integer = CInt(startCellCol)
            Dim int_endCellRow As Integer = CInt(endCellRow)
            Dim int_endCellCol As Integer = CInt(endCellCol)


            If int_endCellRow >= int_startCellRow Then


                If ClearOp = "Clear" Then
                    .Range(.cells(int_startCellRow, int_startCellCol), .cells(int_endCellRow, int_endCellCol)).Clear()
                Else
                    .Range(.cells(int_startCellRow, int_startCellCol), .cells(int_endCellRow, int_endCellCol)).ClearContents()
                End If
            End If


        End With

        Return True

    End Function




    Public Function CopyRange(params As List(Of String)) As Boolean

        Dim excelName As String = ReplaceRegx(params(0))
        Dim sheetName As String = ReplaceRegx(params(1))

        Dim startCellRow As String = params(2).Split(",")(0)
        Dim startCellCol As String = params(2).Split(",")(1)
        Dim endCellRow As String = params(3).Split(",")(0)
        Dim endCellCol As String = params(3).Split(",")(1)




        DicExcel.Item(excelName).Sheets.Item(sheetName).Activate()

        With DicExcel(excelName).Sheets.Item(sheetName)



            If endCellRow = "[max]" Then
                endCellRow = .Cells(1048576, CInt(startCellCol)).End(Excel.XlDirection.xlUp).Row.ToString
            End If


            Dim int_startCellRow As Integer = CInt(startCellRow)
            Dim int_startCellCol As Integer = CInt(startCellCol)
            Dim int_endCellRow As Integer = CInt(endCellRow)
            Dim int_endCellCol As Integer = CInt(endCellCol)

            '.Range(.cells(int_startCellRow, int_startCellCol), .cells(int_endCellRow, int_endCellCol)).Select()
            .Range(.cells(int_startCellRow, int_startCellCol), .cells(int_endCellRow, int_endCellCol)).copy()


        End With

        Return True

    End Function





    Public Function Paste(params As List(Of String)) As Boolean

        Dim excelName As String = ReplaceRegx(params(0))
        Dim sheetName As String = ReplaceRegx(params(1))

        Dim startCellRow As String = params(2).Split(",")(0)
        Dim startCellCol As String = params(2).Split(",")(1)


        Dim PasteType As String = ReplaceRegx(params(3))

        DicWb(excelName).Activate()

        DicWb(excelName).Sheets.Item(sheetName).Activate()

        With DicWb(excelName).Sheets.Item(sheetName)


            If startCellRow = "[max]" Then
                startCellRow = CInt(.Cells(1048576, CInt(startCellCol)).End(Excel.XlDirection.xlUp).Row.ToString) + 1
            End If


            Dim int_startCellRow As Integer = CInt(startCellRow)
            Dim int_startCellCol As Integer = CInt(startCellCol)

            .cells(int_startCellRow, int_startCellCol).select()

            DicExcel(excelName).ScreenUpdating = False

            Dim ThisWorkSheet As Excel.Worksheet = DicWb(excelName).Sheets.Item(sheetName)
            ThisWorkSheet.Activate()
            Dim cel As Excel.Range = .cells(int_startCellRow, int_startCellCol)
            cel.Select()

            If PasteType = "PasteNum" Then
                ' .Selection.PasteSpecial(Paste:=Excel.XlPasteType.xlPasteValues, Operation:=Excel.XlPasteSpecialOperation.xlNone, SkipBlanks:=False, Transpose:=False)
                cel.PasteSpecial(Excel.XlPasteType.xlPasteValues, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone)
                '.paste()
            Else
                cel.paste()
            End If

            DicExcel(excelName).ScreenUpdating = True

        End With

        Return True

    End Function















    Public Function NewExcelApp() As Excel.Application
        Dim ExcelApplication As New Excel.Application
        ExcelApplication.EnableEvents = True
        ExcelApplication.Visible = True
        ExcelApplication.DisplayAlerts = False
        ExcelApplication.UserControl = False
        Return (ExcelApplication)
    End Function





















    Private Function GetCmd(ByVal cmdStr As String) As String
        Dim cmdstrs() As String = cmdStr.Split(conSplitSign)
        Return cmdstrs(0)
    End Function
    Private Function GetParam(ByVal cmdStr As String) As List(Of String)
        Dim cmdstrs() As String = cmdStr.Split(conSplitSign)
        Dim params As New List(Of String)
        For i As Integer = 1 To cmdstrs.Length - 1
            params.Add(cmdstrs(i))
        Next
        Return params
    End Function



    Public Function ReplaceRegx(ByVal str As String)

        str = str.Replace("[yyMM]", Now.ToString("yyMM"))
        str = str.Replace("[yyMM-1]", DateAdd(DateInterval.Month, -1, Now).ToString("yyMM"))
        str = str.Replace("[yyMM-2]", DateAdd(DateInterval.Month, -2, Now).ToString("yyMM"))
        str = str.Replace("[yyMM+1]", DateAdd(DateInterval.Month, 1, Now).ToString("yyMM"))
        str = str.Replace("[yyMM+2]", DateAdd(DateInterval.Month, 2, Now).ToString("yyMM"))
        Return str
    End Function




    Public Sub ErrorSyori(ByVal msg As String)


    End Sub

End Class
