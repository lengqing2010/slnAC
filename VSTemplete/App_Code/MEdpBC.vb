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

Public Class MEdpBC
Public DA AS NEW MEdpDA

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

Public Function SelMEdp(           Byval edpNo_key AS String) As Data.DataTable
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           edpNo_key)
    'SQLコメント
    Return DA.SelMEdp( _
           edpNo_key)
End Function

''' <summary>
''' aaaa
''' EDP情報情報を更新する
''' </summary>
'''<param name="edpNo_key">EDP NUMBER</param>
    '''<param name="edpNo">EDP NUMBER</param>
    '''<param name="edpMei">EDP 名</param>
    '''<param name="edpExp">EDP 説明</param>
    '''<param name="idx">INDEX</param>
    '''<param name="status">ステータス       </param>
    ''' <returns>EDP情報情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/30  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function UpdMEdp(ByVal edpNo_key As String, _
               ByVal edpNo As String, _
               ByVal edpMei As String, _
               ByVal edpExp As String, _
               ByVal idx As Integer, _
               ByVal status As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               edpNo_key, _
               edpNo, _
               edpMei, _
               edpExp, _
               idx, _
               status)
        'SQLコメント
        '--**テーブル：EDP情報 : m_edp
        Return DA.UpdMEdp( _
               edpNo_key, _
               edpNo, _
               edpMei, _
               edpExp, _
               idx, _
               status)

    End Function

    ''' <summary>
    ''' aaaa
    ''' EDP情報情報を登録する
    ''' </summary>
    '''<param name="edpNo">EDP NUMBER</param>
    '''<param name="edpMei">EDP 名</param>
    '''<param name="edpExp">EDP 説明</param>
    '''<param name="idx">INDEX</param>
    '''<param name="status">ステータス       </param>
    ''' <returns>EDP情報情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/30  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function InsMEdp(ByVal edpNo As String, _
               ByVal edpMei As String, _
               ByVal edpExp As String, _
               ByVal idx As Integer, _
               ByVal status As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               edpNo, _
               edpMei, _
               edpExp, _
               idx, _
               status)
        'SQLコメント
        '--**テーブル：EDP情報 : m_edp
        Return DA.InsMEdp( _
               edpNo, _
               edpMei, _
               edpExp, _
               idx, _
               status)

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

Public Function DelMEdp(           Byval edpNo_key AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           edpNo_key)
    'SQLコメント
    '--**テーブル：EDP情報 : m_edp
    Return DA.DelMEdp( _
           edpNo_key)


End Function

End Class
