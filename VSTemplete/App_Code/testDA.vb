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

Public Class TUrikakeMeisaiDA

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

Public Function SelTUrikakeMeisai(           Byval renNo_key AS Decimal, _
           Byval tenpoCd_key AS String, _
           Byval saisyuuUriageDate_key AS String, _
           Byval torikesiKbn_key AS String, _
           Byval regiNo_key AS String, _
           Byval onlineKbn_key AS String, _
           Byval dataSakuseiDate_key AS String) As Data.DataTable
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           renNo_key, _
           tenpoCd_key, _
           saisyuuUriageDate_key, _
           torikesiKbn_key, _
           regiNo_key, _
           onlineKbn_key, _
           dataSakuseiDate_key)
    'SQLコメント
    '--**テーブル：asdf : t_urikake_meisai
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("SELECT")
    sb.AppendLine("touroku_date")   '登録日時
    sb.AppendLine(", kousin_date")   '更新日時
    sb.AppendLine(", ren_no")   '連番
    sb.AppendLine(", urikake_kingaku")   '－ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ
    sb.AppendLine(", kingaku")   '金額
    sb.AppendLine(", syouhizei")   '消費税
    sb.AppendLine(", utikin")   '－ＵＴＩＫＩＮ
    sb.AppendLine(", cat_kensuu")   'ＣＡＴ件数
    sb.AppendLine(", tokuisaki_kbn")   '－ＴＯＫＵＩＳＡＫＩ－ＫＢＮ
    sb.AppendLine(", kokyku_mei_kana")   '－ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ
    sb.AppendLine(", touroku_user")   '登録者
    sb.AppendLine(", kousin_user")   '更新者
    sb.AppendLine(", tenpo_cd")   '店コード
    sb.AppendLine(", saisyuu_uriage_date")   '最終売上日
    sb.AppendLine(", torikesi_kbn")   '取消区分
    sb.AppendLine(", regi_no")   'レジNO
    sb.AppendLine(", online_kbn")   'オンライン区分
    sb.AppendLine(", urikake_kbn")   '－ＵＲＩＫＡＫＥ－ＫＢＮ
    sb.AppendLine(", toriatukai_kbn")   '－ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ
    sb.AppendLine(", bunkatu_kaisuu")   '－ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ
    sb.AppendLine(", g_cat_flg")   '－Ｇ－ＣＡＴ－ＦＬＧ
    sb.AppendLine(", data_sakusei_date")   'データ作成日

    sb.AppendLine("FROM t_urikake_meisai")
    sb.AppendLine("WHERE")
        sb.AppendLine("ren_no=@ren_no")   '連番
    sb.AppendLine("AND tenpo_cd=@tenpo_cd")   '店コード
    sb.AppendLine("AND saisyuu_uriage_date=@saisyuu_uriage_date")   '最終売上日
    sb.AppendLine("AND torikesi_kbn=@torikesi_kbn")   '取消区分
    sb.AppendLine("AND regi_no=@regi_no")   'レジNO
    sb.AppendLine("AND online_kbn=@online_kbn")   'オンライン区分
    sb.AppendLine("AND data_sakusei_date=@data_sakusei_date")   'データ作成日

    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@ren_no", SqlDbType.Decimal, 5, renNo_key))
    paramList.Add(MakeParam("@tenpo_cd", SqlDbType.Char, 4, tenpoCd_key))
    paramList.Add(MakeParam("@saisyuu_uriage_date", SqlDbType.Char, 8, saisyuuUriageDate_key))
    paramList.Add(MakeParam("@torikesi_kbn", SqlDbType.Char, 1, torikesiKbn_key))
    paramList.Add(MakeParam("@regi_no", SqlDbType.Char, 4, regiNo_key))
    paramList.Add(MakeParam("@online_kbn", SqlDbType.Char, 1, onlineKbn_key))
    paramList.Add(MakeParam("@data_sakusei_date", SqlDbType.Char, 8, dataSakuseiDate_key))

    Dim dsInfo As New Data.DataSet
    FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "", paramList.ToArray)

    Return dsInfo.Tables("t_urikake_meisai")

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

Public Function UpdTUrikakeMeisai(           Byval renNo_key AS Decimal, _
           Byval tenpoCd_key AS String, _
           Byval saisyuuUriageDate_key AS String, _
           Byval torikesiKbn_key AS String, _
           Byval regiNo_key AS String, _
           Byval onlineKbn_key AS String, _
           Byval dataSakuseiDate_key AS String, _
           Byval tourokuDate AS String, _
           Byval kousinDate AS String, _
           Byval renNo AS Decimal, _
           Byval urikakeKingaku AS Decimal, _
           Byval kingaku AS Decimal, _
           Byval syouhizei AS Decimal, _
           Byval utikin AS Decimal, _
           Byval catKensuu AS Decimal, _
           Byval tokuisakiKbn AS String, _
           Byval kokykuMeiKana AS String, _
           Byval tourokuUser AS String, _
           Byval kousinUser AS String, _
           Byval tenpoCd AS String, _
           Byval saisyuuUriageDate AS String, _
           Byval torikesiKbn AS String, _
           Byval regiNo AS String, _
           Byval onlineKbn AS String, _
           Byval urikakeKbn AS String, _
           Byval toriatukaiKbn AS String, _
           Byval bunkatuKaisuu AS String, _
           Byval gCatFlg AS String, _
           Byval dataSakuseiDate AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
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
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("UPDATE t_urikake_meisai")
    sb.AppendLine("SET")
    sb.AppendLine("touroku_date=@touroku_date")   '登録日時
    sb.AppendLine(", kousin_date=@kousin_date")   '更新日時
    sb.AppendLine(", ren_no=@ren_no")   '連番
    sb.AppendLine(", urikake_kingaku=@urikake_kingaku")   '－ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ
    sb.AppendLine(", kingaku=@kingaku")   '金額
    sb.AppendLine(", syouhizei=@syouhizei")   '消費税
    sb.AppendLine(", utikin=@utikin")   '－ＵＴＩＫＩＮ
    sb.AppendLine(", cat_kensuu=@cat_kensuu")   'ＣＡＴ件数
    sb.AppendLine(", tokuisaki_kbn=@tokuisaki_kbn")   '－ＴＯＫＵＩＳＡＫＩ－ＫＢＮ
    sb.AppendLine(", kokyku_mei_kana=@kokyku_mei_kana")   '－ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ
    sb.AppendLine(", touroku_user=@touroku_user")   '登録者
    sb.AppendLine(", kousin_user=@kousin_user")   '更新者
    sb.AppendLine(", tenpo_cd=@tenpo_cd")   '店コード
    sb.AppendLine(", saisyuu_uriage_date=@saisyuu_uriage_date")   '最終売上日
    sb.AppendLine(", torikesi_kbn=@torikesi_kbn")   '取消区分
    sb.AppendLine(", regi_no=@regi_no")   'レジNO
    sb.AppendLine(", online_kbn=@online_kbn")   'オンライン区分
    sb.AppendLine(", urikake_kbn=@urikake_kbn")   '－ＵＲＩＫＡＫＥ－ＫＢＮ
    sb.AppendLine(", toriatukai_kbn=@toriatukai_kbn")   '－ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ
    sb.AppendLine(", bunkatu_kaisuu=@bunkatu_kaisuu")   '－ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ
    sb.AppendLine(", g_cat_flg=@g_cat_flg")   '－Ｇ－ＣＡＴ－ＦＬＧ
    sb.AppendLine(", data_sakusei_date=@data_sakusei_date")   'データ作成日

    sb.AppendLine("FROM t_urikake_meisai")
    sb.AppendLine("WHERE")
        sb.AppendLine("ren_no=@ren_no")   '連番
    sb.AppendLine("AND tenpo_cd=@tenpo_cd")   '店コード
    sb.AppendLine("AND saisyuu_uriage_date=@saisyuu_uriage_date")   '最終売上日
    sb.AppendLine("AND torikesi_kbn=@torikesi_kbn")   '取消区分
    sb.AppendLine("AND regi_no=@regi_no")   'レジNO
    sb.AppendLine("AND online_kbn=@online_kbn")   'オンライン区分
    sb.AppendLine("AND data_sakusei_date=@data_sakusei_date")   'データ作成日

    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@ren_no", SqlDbType.Decimal, 5, renNo_key))
    paramList.Add(MakeParam("@tenpo_cd", SqlDbType.Char, 4, tenpoCd_key))
    paramList.Add(MakeParam("@saisyuu_uriage_date", SqlDbType.Char, 8, saisyuuUriageDate_key))
    paramList.Add(MakeParam("@torikesi_kbn", SqlDbType.Char, 1, torikesiKbn_key))
    paramList.Add(MakeParam("@regi_no", SqlDbType.Char, 4, regiNo_key))
    paramList.Add(MakeParam("@online_kbn", SqlDbType.Char, 1, onlineKbn_key))
    paramList.Add(MakeParam("@data_sakusei_date", SqlDbType.Char, 8, dataSakuseiDate_key))

    paramList.Add(MakeParam("@touroku_date", SqlDbType.Date, 24, tourokuDate))
    paramList.Add(MakeParam("@kousin_date", SqlDbType.Date, 24, kousinDate))
    paramList.Add(MakeParam("@ren_no", SqlDbType.Decimal, 5, renNo))
    paramList.Add(MakeParam("@urikake_kingaku", SqlDbType.Decimal, 5, urikakeKingaku))
    paramList.Add(MakeParam("@kingaku", SqlDbType.Decimal, 5, kingaku))
    paramList.Add(MakeParam("@syouhizei", SqlDbType.Decimal, 5, syouhizei))
    paramList.Add(MakeParam("@utikin", SqlDbType.Decimal, 5, utikin))
    paramList.Add(MakeParam("@cat_kensuu", SqlDbType.Decimal, 5, catKensuu))
    paramList.Add(MakeParam("@tokuisaki_kbn", SqlDbType.VarChar, 4, tokuisakiKbn))
    paramList.Add(MakeParam("@kokyku_mei_kana", SqlDbType.VarChar, 12, kokykuMeiKana))
    paramList.Add(MakeParam("@touroku_user", SqlDbType.VarChar, 20, tourokuUser))
    paramList.Add(MakeParam("@kousin_user", SqlDbType.VarChar, 20, kousinUser))
    paramList.Add(MakeParam("@tenpo_cd", SqlDbType.Char, 4, tenpoCd))
    paramList.Add(MakeParam("@saisyuu_uriage_date", SqlDbType.Char, 8, saisyuuUriageDate))
    paramList.Add(MakeParam("@torikesi_kbn", SqlDbType.Char, 1, torikesiKbn))
    paramList.Add(MakeParam("@regi_no", SqlDbType.Char, 4, regiNo))
    paramList.Add(MakeParam("@online_kbn", SqlDbType.Char, 1, onlineKbn))
    paramList.Add(MakeParam("@urikake_kbn", SqlDbType.Char, 1, urikakeKbn))
    paramList.Add(MakeParam("@toriatukai_kbn", SqlDbType.Char, 1, toriatukaiKbn))
    paramList.Add(MakeParam("@bunkatu_kaisuu", SqlDbType.Char, 2, bunkatuKaisuu))
    paramList.Add(MakeParam("@g_cat_flg", SqlDbType.Char, 1, gCatFlg))
    paramList.Add(MakeParam("@data_sakusei_date", SqlDbType.Char, 8, dataSakuseiDate))


    SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray) 

    Return True 

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

Public Function InsTUrikakeMeisai(           Byval tourokuDate AS String, _
           Byval kousinDate AS String, _
           Byval renNo AS Decimal, _
           Byval urikakeKingaku AS Decimal, _
           Byval kingaku AS Decimal, _
           Byval syouhizei AS Decimal, _
           Byval utikin AS Decimal, _
           Byval catKensuu AS Decimal, _
           Byval tokuisakiKbn AS String, _
           Byval kokykuMeiKana AS String, _
           Byval tourokuUser AS String, _
           Byval kousinUser AS String, _
           Byval tenpoCd AS String, _
           Byval saisyuuUriageDate AS String, _
           Byval torikesiKbn AS String, _
           Byval regiNo AS String, _
           Byval onlineKbn AS String, _
           Byval urikakeKbn AS String, _
           Byval toriatukaiKbn AS String, _
           Byval bunkatuKaisuu AS String, _
           Byval gCatFlg AS String, _
           Byval dataSakuseiDate AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
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
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("INSERT INTO  t_urikake_meisai")
    sb.AppendLine("(")
    sb.AppendLine("touroku_date")   '登録日時
    sb.AppendLine(", kousin_date")   '更新日時
    sb.AppendLine(", ren_no")   '連番
    sb.AppendLine(", urikake_kingaku")   '－ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ
    sb.AppendLine(", kingaku")   '金額
    sb.AppendLine(", syouhizei")   '消費税
    sb.AppendLine(", utikin")   '－ＵＴＩＫＩＮ
    sb.AppendLine(", cat_kensuu")   'ＣＡＴ件数
    sb.AppendLine(", tokuisaki_kbn")   '－ＴＯＫＵＩＳＡＫＩ－ＫＢＮ
    sb.AppendLine(", kokyku_mei_kana")   '－ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ
    sb.AppendLine(", touroku_user")   '登録者
    sb.AppendLine(", kousin_user")   '更新者
    sb.AppendLine(", tenpo_cd")   '店コード
    sb.AppendLine(", saisyuu_uriage_date")   '最終売上日
    sb.AppendLine(", torikesi_kbn")   '取消区分
    sb.AppendLine(", regi_no")   'レジNO
    sb.AppendLine(", online_kbn")   'オンライン区分
    sb.AppendLine(", urikake_kbn")   '－ＵＲＩＫＡＫＥ－ＫＢＮ
    sb.AppendLine(", toriatukai_kbn")   '－ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ
    sb.AppendLine(", bunkatu_kaisuu")   '－ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ
    sb.AppendLine(", g_cat_flg")   '－Ｇ－ＣＡＴ－ＦＬＧ
    sb.AppendLine(", data_sakusei_date")   'データ作成日

    sb.AppendLine(")")
    sb.AppendLine("VALUES(")
    sb.AppendLine("@touroku_date")   '登録日時
    sb.AppendLine(", @kousin_date")   '更新日時
    sb.AppendLine(", @ren_no")   '連番
    sb.AppendLine(", @urikake_kingaku")   '－ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ
    sb.AppendLine(", @kingaku")   '金額
    sb.AppendLine(", @syouhizei")   '消費税
    sb.AppendLine(", @utikin")   '－ＵＴＩＫＩＮ
    sb.AppendLine(", @cat_kensuu")   'ＣＡＴ件数
    sb.AppendLine(", @tokuisaki_kbn")   '－ＴＯＫＵＩＳＡＫＩ－ＫＢＮ
    sb.AppendLine(", @kokyku_mei_kana")   '－ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ
    sb.AppendLine(", @touroku_user")   '登録者
    sb.AppendLine(", @kousin_user")   '更新者
    sb.AppendLine(", @tenpo_cd")   '店コード
    sb.AppendLine(", @saisyuu_uriage_date")   '最終売上日
    sb.AppendLine(", @torikesi_kbn")   '取消区分
    sb.AppendLine(", @regi_no")   'レジNO
    sb.AppendLine(", @online_kbn")   'オンライン区分
    sb.AppendLine(", @urikake_kbn")   '－ＵＲＩＫＡＫＥ－ＫＢＮ
    sb.AppendLine(", @toriatukai_kbn")   '－ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ
    sb.AppendLine(", @bunkatu_kaisuu")   '－ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ
    sb.AppendLine(", @g_cat_flg")   '－Ｇ－ＣＡＴ－ＦＬＧ
    sb.AppendLine(", @data_sakusei_date")   'データ作成日

    sb.AppendLine(")")
    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@touroku_date", SqlDbType.Date, 24, tourokuDate))
    paramList.Add(MakeParam("@kousin_date", SqlDbType.Date, 24, kousinDate))
    paramList.Add(MakeParam("@ren_no", SqlDbType.Decimal, 5, renNo))
    paramList.Add(MakeParam("@urikake_kingaku", SqlDbType.Decimal, 5, urikakeKingaku))
    paramList.Add(MakeParam("@kingaku", SqlDbType.Decimal, 5, kingaku))
    paramList.Add(MakeParam("@syouhizei", SqlDbType.Decimal, 5, syouhizei))
    paramList.Add(MakeParam("@utikin", SqlDbType.Decimal, 5, utikin))
    paramList.Add(MakeParam("@cat_kensuu", SqlDbType.Decimal, 5, catKensuu))
    paramList.Add(MakeParam("@tokuisaki_kbn", SqlDbType.VarChar, 4, tokuisakiKbn))
    paramList.Add(MakeParam("@kokyku_mei_kana", SqlDbType.VarChar, 12, kokykuMeiKana))
    paramList.Add(MakeParam("@touroku_user", SqlDbType.VarChar, 20, tourokuUser))
    paramList.Add(MakeParam("@kousin_user", SqlDbType.VarChar, 20, kousinUser))
    paramList.Add(MakeParam("@tenpo_cd", SqlDbType.Char, 4, tenpoCd))
    paramList.Add(MakeParam("@saisyuu_uriage_date", SqlDbType.Char, 8, saisyuuUriageDate))
    paramList.Add(MakeParam("@torikesi_kbn", SqlDbType.Char, 1, torikesiKbn))
    paramList.Add(MakeParam("@regi_no", SqlDbType.Char, 4, regiNo))
    paramList.Add(MakeParam("@online_kbn", SqlDbType.Char, 1, onlineKbn))
    paramList.Add(MakeParam("@urikake_kbn", SqlDbType.Char, 1, urikakeKbn))
    paramList.Add(MakeParam("@toriatukai_kbn", SqlDbType.Char, 1, toriatukaiKbn))
    paramList.Add(MakeParam("@bunkatu_kaisuu", SqlDbType.Char, 2, bunkatuKaisuu))
    paramList.Add(MakeParam("@g_cat_flg", SqlDbType.Char, 1, gCatFlg))
    paramList.Add(MakeParam("@data_sakusei_date", SqlDbType.Char, 8, dataSakuseiDate))


    SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray) 

    Return True 

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

Public Function DelTUrikakeMeisai(           Byval renNo_key AS Decimal, _
           Byval tenpoCd_key AS String, _
           Byval saisyuuUriageDate_key AS String, _
           Byval torikesiKbn_key AS String, _
           Byval regiNo_key AS String, _
           Byval onlineKbn_key AS String, _
           Byval dataSakuseiDate_key AS String) As Boolean
    'EMAB障害対応情報の格納処理
    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name , _
           renNo_key, _
           tenpoCd_key, _
           saisyuuUriageDate_key, _
           torikesiKbn_key, _
           regiNo_key, _
           onlineKbn_key, _
           dataSakuseiDate_key)
    'SQLコメント
    '--**テーブル：asdf : t_urikake_meisai
    Dim sb As New StringBuilder
'SQL文
    sb.AppendLine("DELETE FROM t_urikake_meisai")
    sb.AppendLine("WHERE")
        sb.AppendLine("ren_no=@ren_no")   '連番
    sb.AppendLine("AND tenpo_cd=@tenpo_cd")   '店コード
    sb.AppendLine("AND saisyuu_uriage_date=@saisyuu_uriage_date")   '最終売上日
    sb.AppendLine("AND torikesi_kbn=@torikesi_kbn")   '取消区分
    sb.AppendLine("AND regi_no=@regi_no")   'レジNO
    sb.AppendLine("AND online_kbn=@online_kbn")   'オンライン区分
    sb.AppendLine("AND data_sakusei_date=@data_sakusei_date")   'データ作成日

    'バラメタ格納
    Dim paramList As New List(Of SqlParameter)
    paramList.Add(MakeParam("@ren_no", SqlDbType.Decimal, 5, renNo_key))
    paramList.Add(MakeParam("@tenpo_cd", SqlDbType.Char, 4, tenpoCd_key))
    paramList.Add(MakeParam("@saisyuu_uriage_date", SqlDbType.Char, 8, saisyuuUriageDate_key))
    paramList.Add(MakeParam("@torikesi_kbn", SqlDbType.Char, 1, torikesiKbn_key))
    paramList.Add(MakeParam("@regi_no", SqlDbType.Char, 4, regiNo_key))
    paramList.Add(MakeParam("@online_kbn", SqlDbType.Char, 1, onlineKbn_key))
    paramList.Add(MakeParam("@data_sakusei_date", SqlDbType.Char, 8, dataSakuseiDate_key))


    SQLHelper.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray) 

    Return True 

End Function

End Class
