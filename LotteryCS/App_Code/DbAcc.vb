Imports Microsoft.VisualBasic
Imports System.Transactions

Public Class DbAcc

    Private Const connStr As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\案件\AutoMakeCode\AutoCode\slnAC\AutoWeb\DATA\Database.mdf;Integrated Security=True"


    Private Shared Function GetConn() As System.Data.SqlClient.SqlConnection

        'Dim conn As String = "Data Source=(LocalDB)\v11.0;Integrated Security=True;User Instance=True;"
        Dim conn As String = "Data Source=(LocalDB)\v11.0;Integrated Security=True;"

        If System.Net.Dns.GetHostName = "WIN7U-20150705K" Then
            'Return connStrHome
        ElseIf System.Net.Dns.GetHostName = "ADP1QD9478YL0O2" Then
            'Return connStrDell
        ElseIf System.Net.Dns.GetHostName = "AGOBW-707150707" Then
            'Return connStrWanguo
            conn &= "AttachDbFilename=D:\ILIKEMAKE\slnAC1\Lottery\App_Data\Lottery.mdf;"
        Else
            'Company
            conn &= "AttachDbFilename=D:\ILIKEMAKE\slnAC1\LotteryCS\App_Data\Lottery.mdf;"
        End If

        Return New System.Data.SqlClient.SqlConnection(conn)

    End Function

    Private Shared InsPrintDataConnect As System.Data.SqlClient.SqlConnection

    Private Shared Sub OpenConn()
        Try
            InsPrintDataConnect = GetConn()
            If InsPrintDataConnect.State = Data.ConnectionState.Broken Then
                InsPrintDataConnect.Close()
                InsPrintDataConnect.Open()
            Else
                InsPrintDataConnect.Open()
            End If

        Catch ex As Exception
            InsPrintDataConnect = New System.Data.SqlClient.SqlConnection(connStr)
            If InsPrintDataConnect.State = Data.ConnectionState.Broken Then
                InsPrintDataConnect.Close()
                InsPrintDataConnect.Open()
            Else
                InsPrintDataConnect.Open()
            End If
        End Try

    End Sub

    Private Shared Sub CloseConn()

        InsPrintDataConnect.Close()
        InsPrintDataConnect.Dispose()

    End Sub


    Public Shared Function RunSql(ByVal sql As String) As Boolean



        Dim SQLCommand As System.Data.SqlClient.SqlCommand

        Dim tmout As New TimeSpan(0, 45, 0)
        Dim options As New Transactions.TransactionOptions
        '分離レベルスナップショットに明示的に指定
        'options.IsolationLevel = IsolationLevel.Snapshot
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.RequiresNew, tmout)
            Try

                OpenConn()

                SQLCommand = New System.Data.SqlClient.SqlCommand(sql.ToString, InsPrintDataConnect)
                SQLCommand.CommandTimeout = 0
                SQLCommand.ExecuteNonQuery()
                SQLCommand.Dispose()

                CloseConn()

                '成功の場合
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

        Return True

    End Function



End Class
