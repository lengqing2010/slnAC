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

Public Class MEdpDA

    ''' <summary>
    ''' 
    ''' 情報を検索する
    ''' </summary>
    '''<param name="edpNo_key">edp_no</param>
    ''' <returns>情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/30  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function SelMEdp(ByVal edpNo_key As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               edpNo_key)
        'SQLコメント
        '--**テーブル： : m_edp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("edp_no")   'edp_no
        sb.AppendLine(", edp_mei")   'edp_mei
        sb.AppendLine(", edp_exp")   'edp_exp

        sb.AppendLine("FROM m_edp")
        sb.AppendLine("WHERE 1=1")
        If edpNo_key <> "" Then
            sb.AppendLine("AND edp_no=@edp_no_key")   'edp_no
        End If

        'バラメタ格納
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(MakeParam("@edp_no_key", SqlDbType.VarChar, 20, edpNo_key))

        Dim dsInfo As New Data.DataSet
        FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_edp", paramList.ToArray)

        Return dsInfo.Tables("m_edp")

    End Function

    ''' <summary>
    ''' 
    ''' 情報を更新する
    ''' </summary>
    '''<param name="edpNo_key">edp_no</param>
    '''<param name="edpNo">edp_no</param>
    '''<param name="edpMei">edp_mei</param>
    '''<param name="edpExp">edp_exp</param>
    ''' <returns>情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/30  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function UpdMEdp(ByVal edpNo_key As String, _
               ByVal edpNo As String, _
               ByVal edpMei As String, _
               ByVal edpExp As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               edpNo_key, _
               edpNo, _
               edpMei, _
               edpExp)
        'SQLコメント
        '--**テーブル： : m_edp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_edp")
        sb.AppendLine("SET")
        sb.AppendLine("edp_no=@edp_no")   'edp_no
        sb.AppendLine(", edp_mei=@edp_mei")   'edp_mei
        sb.AppendLine(", edp_exp=@edp_exp")   'edp_exp

        sb.AppendLine("FROM m_edp")
        sb.AppendLine("WHERE 1=1")
        If edpNo_key <> "" Then
            sb.AppendLine("AND edp_no=@edp_no_key")   'edp_no
        End If

        'バラメタ格納
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(MakeParam("@edp_no_key", SqlDbType.VarChar, 20, edpNo_key))

        paramList.Add(MakeParam("@edp_no", SqlDbType.VarChar, 20, edpNo))
        paramList.Add(MakeParam("@edp_mei", SqlDbType.VarChar, 200, edpMei))
        paramList.Add(MakeParam("@edp_exp", SqlDbType.VarChar, 1000, edpExp))


        SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 情報を登録する
    ''' </summary>
    '''<param name="edpNo">edp_no</param>
    '''<param name="edpMei">edp_mei</param>
    '''<param name="edpExp">edp_exp</param>
    ''' <returns>情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/30  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function InsMEdp(ByVal edpNo As String, _
               ByVal edpMei As String, _
               ByVal edpExp As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               edpNo, _
               edpMei, _
               edpExp)
        'SQLコメント
        '--**テーブル： : m_edp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_edp")
        sb.AppendLine("(")
        sb.AppendLine("edp_no")   'edp_no
        sb.AppendLine(", edp_mei")   'edp_mei
        sb.AppendLine(", edp_exp")   'edp_exp

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@edp_no")   'edp_no
        sb.AppendLine(", @edp_mei")   'edp_mei
        sb.AppendLine(", @edp_exp")   'edp_exp

        sb.AppendLine(")")
        'バラメタ格納
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(MakeParam("@edp_no", SqlDbType.VarChar, 20, edpNo))
        paramList.Add(MakeParam("@edp_mei", SqlDbType.VarChar, 200, edpMei))
        paramList.Add(MakeParam("@edp_exp", SqlDbType.VarChar, 1000, edpExp))


        SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 情報を削除する
    ''' </summary>
    '''<param name="edpNo_key">edp_no</param>
    ''' <returns>情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/30  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function DelMEdp(ByVal edpNo_key As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               edpNo_key)
        'SQLコメント
        '--**テーブル： : m_edp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_edp")
        sb.AppendLine("WHERE 1=1")
        If edpNo_key <> "" Then
            sb.AppendLine("AND edp_no=@edp_no_key")   'edp_no
        End If

        'バラメタ格納
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(MakeParam("@edp_no_key", SqlDbType.VarChar, 20, edpNo_key))


        SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

End Class
