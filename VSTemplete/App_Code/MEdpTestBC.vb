Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MEdpTestBC
Public DA AS NEW MEdpTestDA

''' <summary>
''' aaaa
''' EDP情報情報を検索する
''' </summary>
'''<param name="edpNo_key">EDP NUMBER</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/30  李松涛さん 新規作成 </para>
''' </history>

Public Function SelMEdpTest(           Byval edpNo_key AS String) As Data.DataTable
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           edpNo_key)
    'SQLコメント
    Return DA.SelMEdpTest( _
           edpNo_key)
End Function

''' <summary>
''' aaaa
''' EDP情報情報を更新する
''' </summary>
'''<param name="edpNo_key">EDP NUMBER</param>
'''<param name="idx">ｉｎｄｅｘ</param>
'''<param name="edpNo">EDP NUMBER</param>
'''<param name="edpMei">EDP 名</param>
'''<param name="edpExp">EDP 説明</param>
'''<param name="status">ステータス       </param>
'''<param name="status2">ＳＴＡＴＵＳ２</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/30  李松涛さん 新規作成 </para>
''' </history>

Public Function UpdMEdpTest(           Byval edpNo_key AS String, _
           Byval idx AS Integer, _
           Byval edpNo AS String, _
           Byval edpMei AS String, _
           Byval edpExp AS String, _
           Byval status AS String, _
           Byval status2 AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           edpNo_key, _
           idx, _
           edpNo, _
           edpMei, _
           edpExp, _
           status, _
           status2)
    'SQLコメント
    '--**テーブル：EDP情報 : m_edp_test
    Return DA.UpdMEdpTest( _
           edpNo_key, _
           idx, _
           edpNo, _
           edpMei, _
           edpExp, _
           status, _
           status2)

End Function

''' <summary>
''' aaaa
''' EDP情報情報を登録する
''' </summary>
'''<param name="idx">ｉｎｄｅｘ</param>
'''<param name="edpNo">EDP NUMBER</param>
'''<param name="edpMei">EDP 名</param>
'''<param name="edpExp">EDP 説明</param>
'''<param name="status">ステータス       </param>
'''<param name="status2">ＳＴＡＴＵＳ２</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/30  李松涛さん 新規作成 </para>
''' </history>

Public Function InsMEdpTest(           Byval idx AS Integer, _
           Byval edpNo AS String, _
           Byval edpMei AS String, _
           Byval edpExp AS String, _
           Byval status AS String, _
           Byval status2 AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           idx, _
           edpNo, _
           edpMei, _
           edpExp, _
           status, _
           status2)
    'SQLコメント
    '--**テーブル：EDP情報 : m_edp_test
    Return DA.InsMEdpTest( _
           idx, _
           edpNo, _
           edpMei, _
           edpExp, _
           status, _
           status2)

End Function

''' <summary>
''' aaaa
''' EDP情報情報を削除する
''' </summary>
'''<param name="edpNo_key">EDP NUMBER</param>
''' <returns>EDP情報情報</returns>
''' <remarks></remarks>
''' <history>
''' <para>2018/10/30  李松涛さん 新規作成 </para>
''' </history>

Public Function DelMEdpTest(           Byval edpNo_key AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           edpNo_key)
    'SQLコメント
    '--**テーブル：EDP情報 : m_edp_test
    Return DA.DelMEdpTest( _
           edpNo_key)


End Function

End Class
