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

Public Class MEdpTestDA
'DAソース作成
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
Public Function SelmEdpTest(Byval edpNo AS String _
                                , Byval edpMei AS String _
                                , Byval edpExp AS String _
                                , Byval idx AS Integer _
                                , Byval status AS String _
                                , Byval status2 AS String) As Data.DataTable

    '戻りデータセット
    Dim dsInfo As New Data.DataSet
    'SQLコメント
'--**テーブル：m_edp_test
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("SELECT")
    sb.AppendLine("          m_edp_test.edp_no")                ' EDP NUMBER
    sb.AppendLine("     ,    m_edp_test.edp_mei")               ' EDP 名
    sb.AppendLine("     ,    m_edp_test.edp_exp")               ' EDP 説明
    sb.AppendLine("     ,    m_edp_test.idx")                   ' ｉｎｄｅｘ
    sb.AppendLine("     ,    m_edp_test.status")                ' ステータス       
    sb.AppendLine("     ,    m_edp_test.status2")               ' －ＳＴＡＴＵＳ２

    sb.AppendLine("FROM m_edp_test")        ' EDP情報
    sb.AppendLine("WHERE")
    sb.AppendLine("          m_edp_test.edp_no =     @edp_no")  '  EDP NUMBER
    sb.AppendLine("     AND    m_edp_test.edp_mei =     @edp_mei")                              '  EDP 名
    sb.AppendLine("     AND    m_edp_test.edp_exp =     @edp_exp")                              '  EDP 説明
    sb.AppendLine("     AND    m_edp_test.idx =     @idx")      '  ｉｎｄｅｘ
    sb.AppendLine("     AND    m_edp_test.status =     @status")' ステータス       
    sb.AppendLine("     AND    m_edp_test.status2 =     @status2")                              '  －ＳＴＡＴＵＳ２


    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@edp_no", SqlDbType.nvarchar, 200, edpNo))                         ' EDP NUMBER
    paramList.Add(MakeParam("@edp_mei", SqlDbType.nvarchar, 800, edpMei))                       ' EDP 名
    paramList.Add(MakeParam("@edp_exp", SqlDbType.nvarchar, 4000, edpExp))                      ' EDP 説明
    paramList.Add(MakeParam("@idx", SqlDbType.Int, 4, idx))     ' ｉｎｄｅｘ
    paramList.Add(MakeParam("@status", SqlDbType.nchar, 4, status))                             ' ステータス       
    paramList.Add(MakeParam("@status2", SqlDbType.nchar, 6, status2))                           ' －ＳＴＡＴＵＳ２

    FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_edp_test", paramList.ToArray)

    Return dsInfo.Tables("m_edp_test")

End Function


'DAソース作成
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
    'SQLコメント
    '--**テーブル：m_edp_test
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("INSERT INTO m_edp_test(")
    sb.AppendLine("          m_edp_test.edp_no")                ' EDP NUMBER
    sb.AppendLine("     ,    m_edp_test.edp_mei")               ' EDP 名
    sb.AppendLine("     ,    m_edp_test.edp_exp")               ' EDP 説明
    sb.AppendLine("     ,    m_edp_test.idx")                   ' ｉｎｄｅｘ
    sb.AppendLine("     ,    m_edp_test.status")                ' ステータス       
    sb.AppendLine("     ,    m_edp_test.status2")               ' －ＳＴＡＴＵＳ２

    sb.AppendLine(")")
    sb.AppendLine("VALUES (")
    sb.AppendLine("         @edp_no")                           '  EDP NUMBER
    sb.AppendLine("    ,    @edp_mei")                          '  EDP 名
    sb.AppendLine("    ,    @edp_exp")                          '  EDP 説明
    sb.AppendLine("    ,    @idx")                              '  ｉｎｄｅｘ
    sb.AppendLine("    ,    @status")                           '  ステータス       
    sb.AppendLine("    ,    @status2")                          '  －ＳＴＡＴＵＳ２

    sb.AppendLine(")")
    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@edp_no", SqlDbType.nvarchar, 200, edpNo))                         ' EDP NUMBER
    paramList.Add(MakeParam("@edp_mei", SqlDbType.nvarchar, 800, edpMei))                       ' EDP 名
    paramList.Add(MakeParam("@edp_exp", SqlDbType.nvarchar, 4000, edpExp))                      ' EDP 説明
    paramList.Add(MakeParam("@idx", SqlDbType.Int, 4, idx))     ' ｉｎｄｅｘ
    paramList.Add(MakeParam("@status", SqlDbType.nchar, 4, status))                             ' ステータス       
    paramList.Add(MakeParam("@status2", SqlDbType.nchar, 6, status2))                           ' －ＳＴＡＴＵＳ２

    SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray) 
    Return True
End Function

'DAソース作成
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
    'SQLコメント
    '--**テーブル：m_edp_test
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("UPDATE m_edp_test SET ")
    sb.AppendLine("          edp_no =     @edp_no               -- EDP NUMBER")                 ' EDP NUMBER
    sb.AppendLine("     ,    edp_mei =     @edp_mei             -- EDP 名")                     ' EDP 名
    sb.AppendLine("     ,    edp_exp =     @edp_exp             -- EDP 説明")                   ' EDP 説明
    sb.AppendLine("     ,    idx =     @idx     -- ｉｎｄｅｘ") ' ｉｎｄｅｘ
    sb.AppendLine("     ,    status =     @status               -- ステータス       ")          ' ステータス       
    sb.AppendLine("     ,    status2 =     @status2             -- －ＳＴＡＴＵＳ２")           ' －ＳＴＡＴＵＳ２

    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@edp_no", SqlDbType.nvarchar, 200, edpNo))                         ' EDP NUMBER
    paramList.Add(MakeParam("@edp_mei", SqlDbType.nvarchar, 800, edpMei))                       ' EDP 名
    paramList.Add(MakeParam("@edp_exp", SqlDbType.nvarchar, 4000, edpExp))                      ' EDP 説明
    paramList.Add(MakeParam("@idx", SqlDbType.Int, 4, idx))     ' ｉｎｄｅｘ
    paramList.Add(MakeParam("@status", SqlDbType.nchar, 4, status))                             ' ステータス       
    paramList.Add(MakeParam("@status2", SqlDbType.nchar, 6, status2))                           ' －ＳＴＡＴＵＳ２

    SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray) 
    Return True
End Function

'DAソース作成
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
Public Function DelmEdpTest(Byval edpNo AS String _
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
    'SQLコメント
    '--**テーブル：m_edp_test
    Dim sb As New StringBuilder
'SQL文
'SQL文
    sb.AppendLine("DELETE")
    sb.AppendLine("FROM m_edp_test")        ' EDP情報
    sb.AppendLine("WHERE")
    sb.AppendLine("          m_edp_test.edp_no =     @edp_no")  '  EDP NUMBER
    sb.AppendLine("     AND    m_edp_test.edp_mei =     @edp_mei")                              '  EDP 名
    sb.AppendLine("     AND    m_edp_test.edp_exp =     @edp_exp")                              '  EDP 説明
    sb.AppendLine("     AND    m_edp_test.idx =     @idx")      '  ｉｎｄｅｘ
    sb.AppendLine("     AND    m_edp_test.status =     @status")' ステータス       
    sb.AppendLine("     AND    m_edp_test.status2 =     @status2")                              '  －ＳＴＡＴＵＳ２


    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@edp_no", SqlDbType.nvarchar, 200, edpNo))                         ' EDP NUMBER
    paramList.Add(MakeParam("@edp_mei", SqlDbType.nvarchar, 800, edpMei))                       ' EDP 名
    paramList.Add(MakeParam("@edp_exp", SqlDbType.nvarchar, 4000, edpExp))                      ' EDP 説明
    paramList.Add(MakeParam("@idx", SqlDbType.Int, 4, idx))     ' ｉｎｄｅｘ
    paramList.Add(MakeParam("@status", SqlDbType.nchar, 4, status))                             ' ステータス       
    paramList.Add(MakeParam("@status2", SqlDbType.nchar, 6, status2))                           ' －ＳＴＡＴＵＳ２

    SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray) 
    Return True
End Function

End Class
