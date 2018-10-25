Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings


'---10---+---20---+---30---+---40---+---50---+---60---+---70---+---80---+---90---+--100---+

Public Class MEdpTestBC
   Public DA AS NEW MEdpTestDA
'BCソース作成
''' <summary>
''' EDP情報情報を検索取得する
''' </summary>
''' <param name="edpNo">edp number</param>
''' <param name="edpMei">edp 名</param>
''' <param name="edpExp">edp 説明</param>
''' <param name="idx">ｉｎｄｅｘ</param>
''' <param name="status">ステータス       </param>
''' <param name="status2">－ｓｔａｔｕｓ２</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/25 P-99999 ??さん 新規作成 </para>
''' </history>
Public Function GetmEdpTest(Byval edpNo AS String _
                                , Byval edpMei AS String _
                                , Byval edpExp AS String _
                                , Byval idx AS Integer _
                                , Byval status AS String _
                                , Byval status2 AS String) As Data.DataTable

    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                        , edpNo _
                                        , edpMei _
                                        , edpExp _
                                        , idx _
                                        , status _
                                        , status2)
return DA.SelmEdpTest(edpNo _
                                        , edpMei _
                                        , edpExp _
                                        , idx _
                                        , status _
                                        , status2)
End Function

'BCソース作成
''' <summary>
''' EDP情報情報を登録取得する
''' </summary>
''' <param name="edpNo">edp number</param>
''' <param name="edpMei">edp 名</param>
''' <param name="edpExp">edp 説明</param>
''' <param name="idx">ｉｎｄｅｘ</param>
''' <param name="status">ステータス       </param>
''' <param name="status2">－ｓｔａｔｕｓ２</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/25 P-99999 ??さん 新規作成 </para>
''' </history>
Public Function InsmEdpTest(Byval edpNo AS String _
                                , Byval edpMei AS String _
                                , Byval edpExp AS String _
                                , Byval idx AS Integer _
                                , Byval status AS String _
                                , Byval status2 AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                        , edpNo _
                                        , edpMei _
                                        , edpExp _
                                        , idx _
                                        , status _
                                        , status2)
   Using scope As New TransactionScope(TransactionScopeOption.Required)
       If (DA.InsmEdpTest(edpNo _
                                        , edpMei _
                                        , edpExp _
                                        , idx _
                                        , status _
                                        , status2)   ) Then 
               scope.Complete()
               Return True
           ELSE
               scope.Dispose()
               Return False
       End If
   End Using
End Function

'BCソース作成
''' <summary>
''' EDP情報情報を更新取得する
''' </summary>
''' <param name="edpNo">edp number</param>
''' <param name="edpMei">edp 名</param>
''' <param name="edpExp">edp 説明</param>
''' <param name="idx">ｉｎｄｅｘ</param>
''' <param name="status">ステータス       </param>
''' <param name="status2">－ｓｔａｔｕｓ２</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/25 P-99999 ??さん 新規作成 </para>
''' </history>
Public Function UpdmEdpTest(Byval edpNo AS String _
                                , Byval edpMei AS String _
                                , Byval edpExp AS String _
                                , Byval idx AS Integer _
                                , Byval status AS String _
                                , Byval status2 AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                        , edpNo _
                                        , edpMei _
                                        , edpExp _
                                        , idx _
                                        , status _
                                        , status2)
   Using scope As New TransactionScope(TransactionScopeOption.Required)
       If (DA.UpdmEdpTest(edpNo _
                                        , edpMei _
                                        , edpExp _
                                        , idx _
                                        , status _
                                        , status2)   ) Then 
               scope.Complete()
               Return True
           ELSE
               scope.Dispose()
               Return False
       End If
   End Using
End Function

    'BCソース作成
    ''' <summary>
    ''' EDP情報情報を削除取得する
    ''' </summary>
    ''' <param name="edpNo">edp number</param>
    ''' <param name="edpMei">edp 名</param>
    ''' <param name="edpExp">edp 説明</param>
    ''' <param name="idx">ｉｎｄｅｘ</param>
    ''' <param name="status">ステータス       </param>
    ''' <param name="status2">－ｓｔａｔｕｓ２</param>
    ''' <returns>EDP情報情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/25 P-99999 ??さん 新規作成 </para>
    ''' </history>
    Public Function DelmEdpTest(ByVal edpNo As String _
                                    , ByVal edpMei As String _
                                    , ByVal edpExp As String _
                                    , ByVal idx As Integer _
                                    , ByVal status As String _
                                    , ByVal status2 As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                            , edpNo _
                                            , edpMei _
                                            , edpExp _
                                            , idx _
                                            , status _
                                            , status2)
        Using scope As New TransactionScope(TransactionScopeOption.Required)
            If (DA.DelmEdpTest(edpNo _
                                             , edpMei _
                                             , edpExp _
                                             , idx _
                                             , status _
                                             , status2)) Then
                scope.Complete()
                Return True
            Else
                scope.Dispose()
                Return False
            End If
        End Using
    End Function

End Class
