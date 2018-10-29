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

Public Class TUrikakeMeisaiBC
    Public DA As New TUrikakeMeisaiDA

    ''' <summary>
    ''' aaaa
    ''' asdf情報を検索する
    ''' </summary>
    '''<param name="renNo_key">連番</param>
    '''<param name="tenpoCd_key">店コード</param>
    '''<param name="saisyuuUriageDate_key">最終売上日</param>
    '''<param name="torikesiKbn_key">取消区分</param>
    '''<param name="regiNo_key">レジNO</param>
    '''<param name="onlineKbn_key">オンライン区分</param>
    '''<param name="dataSakuseiDate_key">データ作成日</param>
    ''' <returns>asdf情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/29  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function SelTUrikakeMeisai(ByVal renNo_key As Decimal, _
               ByVal tenpoCd_key As String, _
               ByVal saisyuuUriageDate_key As String, _
               ByVal torikesiKbn_key As String, _
               ByVal regiNo_key As String, _
               ByVal onlineKbn_key As String, _
               ByVal dataSakuseiDate_key As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               renNo_key, _
               tenpoCd_key, _
               saisyuuUriageDate_key, _
               torikesiKbn_key, _
               regiNo_key, _
               onlineKbn_key, _
               dataSakuseiDate_key)
        'SQLコメント
        Return DA.SelTUrikakeMeisai( _
               renNo_key, _
               tenpoCd_key, _
               saisyuuUriageDate_key, _
               torikesiKbn_key, _
               regiNo_key, _
               onlineKbn_key, _
               dataSakuseiDate_key)
    End Function

    ''' <summary>
    ''' aaaa
    ''' asdf情報を更新する
    ''' </summary>
    '''<param name="renNo_key">連番</param>
    '''<param name="tenpoCd_key">店コード</param>
    '''<param name="saisyuuUriageDate_key">最終売上日</param>
    '''<param name="torikesiKbn_key">取消区分</param>
    '''<param name="regiNo_key">レジNO</param>
    '''<param name="onlineKbn_key">オンライン区分</param>
    '''<param name="dataSakuseiDate_key">データ作成日</param>
    '''<param name="tourokuDate">登録日時</param>
    '''<param name="kousinDate">更新日時</param>
    '''<param name="renNo">連番</param>
    '''<param name="urikakeKingaku">－ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ</param>
    '''<param name="kingaku">金額</param>
    '''<param name="syouhizei">消費税</param>
    '''<param name="utikin">－ＵＴＩＫＩＮ</param>
    '''<param name="catKensuu">ＣＡＴ件数</param>
    '''<param name="tokuisakiKbn">－ＴＯＫＵＩＳＡＫＩ－ＫＢＮ</param>
    '''<param name="kokykuMeiKana">－ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ</param>
    '''<param name="tourokuUser">登録者</param>
    '''<param name="kousinUser">更新者</param>
    '''<param name="tenpoCd">店コード</param>
    '''<param name="saisyuuUriageDate">最終売上日</param>
    '''<param name="torikesiKbn">取消区分</param>
    '''<param name="regiNo">レジNO</param>
    '''<param name="onlineKbn">オンライン区分</param>
    '''<param name="urikakeKbn">－ＵＲＩＫＡＫＥ－ＫＢＮ</param>
    '''<param name="toriatukaiKbn">－ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ</param>
    '''<param name="bunkatuKaisuu">－ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ</param>
    '''<param name="gCatFlg">－Ｇ－ＣＡＴ－ＦＬＧ</param>
    '''<param name="dataSakuseiDate">データ作成日</param>
    ''' <returns>asdf情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/29  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function UpdTUrikakeMeisai(ByVal renNo_key As Decimal, _
               ByVal tenpoCd_key As String, _
               ByVal saisyuuUriageDate_key As String, _
               ByVal torikesiKbn_key As String, _
               ByVal regiNo_key As String, _
               ByVal onlineKbn_key As String, _
               ByVal dataSakuseiDate_key As String, _
               ByVal tourokuDate As String, _
               ByVal kousinDate As String, _
               ByVal renNo As Decimal, _
               ByVal urikakeKingaku As Decimal, _
               ByVal kingaku As Decimal, _
               ByVal syouhizei As Decimal, _
               ByVal utikin As Decimal, _
               ByVal catKensuu As Decimal, _
               ByVal tokuisakiKbn As String, _
               ByVal kokykuMeiKana As String, _
               ByVal tourokuUser As String, _
               ByVal kousinUser As String, _
               ByVal tenpoCd As String, _
               ByVal saisyuuUriageDate As String, _
               ByVal torikesiKbn As String, _
               ByVal regiNo As String, _
               ByVal onlineKbn As String, _
               ByVal urikakeKbn As String, _
               ByVal toriatukaiKbn As String, _
               ByVal bunkatuKaisuu As String, _
               ByVal gCatFlg As String, _
               ByVal dataSakuseiDate As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               renNo_key, _
               tenpoCd_key, _
               saisyuuUriageDate_key, _
               torikesiKbn_key, _
               regiNo_key, _
               onlineKbn_key, _
               dataSakuseiDate_key, _
               tourokuDate, _
               kousinDate, _
               renNo, _
               urikakeKingaku, _
               kingaku, _
               syouhizei, _
               utikin, _
               catKensuu, _
               tokuisakiKbn, _
               kokykuMeiKana, _
               tourokuUser, _
               kousinUser, _
               tenpoCd, _
               saisyuuUriageDate, _
               torikesiKbn, _
               regiNo, _
               onlineKbn, _
               urikakeKbn, _
               toriatukaiKbn, _
               bunkatuKaisuu, _
               gCatFlg, _
               dataSakuseiDate)
        'SQLコメント
        '--**テーブル：asdf : t_urikake_meisai
        Return DA.UpdTUrikakeMeisai( _
               renNo_key, _
               tenpoCd_key, _
               saisyuuUriageDate_key, _
               torikesiKbn_key, _
               regiNo_key, _
               onlineKbn_key, _
               dataSakuseiDate_key, _
               tourokuDate, _
               kousinDate, _
               renNo, _
               urikakeKingaku, _
               kingaku, _
               syouhizei, _
               utikin, _
               catKensuu, _
               tokuisakiKbn, _
               kokykuMeiKana, _
               tourokuUser, _
               kousinUser, _
               tenpoCd, _
               saisyuuUriageDate, _
               torikesiKbn, _
               regiNo, _
               onlineKbn, _
               urikakeKbn, _
               toriatukaiKbn, _
               bunkatuKaisuu, _
               gCatFlg, _
               dataSakuseiDate)

    End Function

    ''' <summary>
    ''' aaaa
    ''' asdf情報を登録する
    ''' </summary>
    '''<param name="tourokuDate">登録日時</param>
    '''<param name="kousinDate">更新日時</param>
    '''<param name="renNo">連番</param>
    '''<param name="urikakeKingaku">－ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ</param>
    '''<param name="kingaku">金額</param>
    '''<param name="syouhizei">消費税</param>
    '''<param name="utikin">－ＵＴＩＫＩＮ</param>
    '''<param name="catKensuu">ＣＡＴ件数</param>
    '''<param name="tokuisakiKbn">－ＴＯＫＵＩＳＡＫＩ－ＫＢＮ</param>
    '''<param name="kokykuMeiKana">－ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ</param>
    '''<param name="tourokuUser">登録者</param>
    '''<param name="kousinUser">更新者</param>
    '''<param name="tenpoCd">店コード</param>
    '''<param name="saisyuuUriageDate">最終売上日</param>
    '''<param name="torikesiKbn">取消区分</param>
    '''<param name="regiNo">レジNO</param>
    '''<param name="onlineKbn">オンライン区分</param>
    '''<param name="urikakeKbn">－ＵＲＩＫＡＫＥ－ＫＢＮ</param>
    '''<param name="toriatukaiKbn">－ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ</param>
    '''<param name="bunkatuKaisuu">－ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ</param>
    '''<param name="gCatFlg">－Ｇ－ＣＡＴ－ＦＬＧ</param>
    '''<param name="dataSakuseiDate">データ作成日</param>
    ''' <returns>asdf情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/29  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function InsTUrikakeMeisai(ByVal tourokuDate As String, _
               ByVal kousinDate As String, _
               ByVal renNo As Decimal, _
               ByVal urikakeKingaku As Decimal, _
               ByVal kingaku As Decimal, _
               ByVal syouhizei As Decimal, _
               ByVal utikin As Decimal, _
               ByVal catKensuu As Decimal, _
               ByVal tokuisakiKbn As String, _
               ByVal kokykuMeiKana As String, _
               ByVal tourokuUser As String, _
               ByVal kousinUser As String, _
               ByVal tenpoCd As String, _
               ByVal saisyuuUriageDate As String, _
               ByVal torikesiKbn As String, _
               ByVal regiNo As String, _
               ByVal onlineKbn As String, _
               ByVal urikakeKbn As String, _
               ByVal toriatukaiKbn As String, _
               ByVal bunkatuKaisuu As String, _
               ByVal gCatFlg As String, _
               ByVal dataSakuseiDate As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               tourokuDate, _
               kousinDate, _
               renNo, _
               urikakeKingaku, _
               kingaku, _
               syouhizei, _
               utikin, _
               catKensuu, _
               tokuisakiKbn, _
               kokykuMeiKana, _
               tourokuUser, _
               kousinUser, _
               tenpoCd, _
               saisyuuUriageDate, _
               torikesiKbn, _
               regiNo, _
               onlineKbn, _
               urikakeKbn, _
               toriatukaiKbn, _
               bunkatuKaisuu, _
               gCatFlg, _
               dataSakuseiDate)
        'SQLコメント
        '--**テーブル：asdf : t_urikake_meisai
        Return DA.InsTUrikakeMeisai( _
               tourokuDate, _
               kousinDate, _
               renNo, _
               urikakeKingaku, _
               kingaku, _
               syouhizei, _
               utikin, _
               catKensuu, _
               tokuisakiKbn, _
               kokykuMeiKana, _
               tourokuUser, _
               kousinUser, _
               tenpoCd, _
               saisyuuUriageDate, _
               torikesiKbn, _
               regiNo, _
               onlineKbn, _
               urikakeKbn, _
               toriatukaiKbn, _
               bunkatuKaisuu, _
               gCatFlg, _
               dataSakuseiDate)

    End Function

    ''' <summary>
    ''' aaaa
    ''' asdf情報を削除する
    ''' </summary>
    '''<param name="renNo_key">連番</param>
    '''<param name="tenpoCd_key">店コード</param>
    '''<param name="saisyuuUriageDate_key">最終売上日</param>
    '''<param name="torikesiKbn_key">取消区分</param>
    '''<param name="regiNo_key">レジNO</param>
    '''<param name="onlineKbn_key">オンライン区分</param>
    '''<param name="dataSakuseiDate_key">データ作成日</param>
    ''' <returns>asdf情報</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2018/10/29  李松涛さん 新規作成 </para>
    ''' </history>

    Public Function DelTUrikakeMeisai(ByVal renNo_key As Decimal, _
               ByVal tenpoCd_key As String, _
               ByVal saisyuuUriageDate_key As String, _
               ByVal torikesiKbn_key As String, _
               ByVal regiNo_key As String, _
               ByVal onlineKbn_key As String, _
               ByVal dataSakuseiDate_key As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
               renNo_key, _
               tenpoCd_key, _
               saisyuuUriageDate_key, _
               torikesiKbn_key, _
               regiNo_key, _
               onlineKbn_key, _
               dataSakuseiDate_key)
        'SQLコメント
        '--**テーブル：asdf : t_urikake_meisai
        Return DA.DelTUrikakeMeisai( _
               renNo_key, _
               tenpoCd_key, _
               saisyuuUriageDate_key, _
               torikesiKbn_key, _
               regiNo_key, _
               onlineKbn_key, _
               dataSakuseiDate_key)


    End Function

End Class
