update m_hiduke set unyou_date='20180214'
-- ****************************************************************************
-- システム名   : ビバ新ＳＡシステム (VIVA-SA)
-- モジュール名 : NAV42001.SQL
-- 処理名       : 売上オンラインリスト
-- 注意事項     : 
--
-- Date        EDP-No   所属/氏名              内   容
-- ----------------------------------------------------------------------------
-- 2012/08/21  P-39443  (TSOL)/蘇暁男        　新規
-- 2012/11/08  P-39443  (TSOL)/周海軍        　IT0252:カーソルフェッチエラーのログが出力され、印刷データが作成されない
-- 2012/11/26  P-39443  (TSOL)/都書剛        　仕様変更_追加対応一覧のNo266に対応する
-- 2012/12/06  P-39443  (TSOL)/高杉            ST0084:複数日リスト出力対応
-- 2012/12/25  P-39443  (TSOL)/都書剛          ST0167:このリストはレジ別に出力は不要です。合計（レジNo９９９９）のみ出力してください。
-- 2013/01/05  P-39443  (TSOL)/都書剛          ST0XXX(ST障害票（Kagami_201301051051）.xls):売上オンラインリストが出力されない
-- 2013/01/06  P-39443  (TSOL)/高杉            運用日の設定方法を変更
-- 2013/01/21  P-39443  (TSOL)/高杉            ST361:データ抽出条件にデータ作成日(運用日)を追加
-- 2013/01/23  P-39443  (TSOL)/高杉            ST361:データ抽出条件、データ作成日(運用日-1)に変更
-- 2013/02/07  P-39443  (TSOL)/都書剛          ST0393:【並行稼動】未出力店舗数が異常に多い
-- 2013/02/08  P-39443  (TSOL)/高杉            KT0326:save_dateに運用日+システム時刻を設定する
-- 2013/02/15  P-39443  (TSOL)/各務            「印刷データなし」を「対象データ無し」に変更
-- 2013/02/15  P-39443  (TSOL)/高杉            ST0456:帳票SEQNOを連番で付番する
-- 2013/02/22  P-39443  (TSOL)/都書剛          ST0510:【内部並行稼動】現金在高及びその他掛計の金額が合わない
-- 2013/03/15  P-39443  (TSOL)/于磊          ３月修正項目No2：FETCHでロジックエラーが発生してもDB更新内容がROLLBACKされない
-- 2013/03/25  P-39443  (TSOL)/朱巍			   ３月修正項目No6
-- 2017/03/25  李松涛                          ポイント利用対応
-- 2018/02/12  李松涛                          Ｐａｉｄレジ対応
-- ****************************************************************************
SET NOCOUNT ON
-- ******************************
--  作業領域
-- ******************************
DECLARE @error_msg                                                       VARCHAR(100)    = '';          -- エラーメッセージのワーク変数
DECLARE @flg                                                             INT             = 1;           -- TOP @flg
DECLARE @flg_2                                                           INT             = 1;           -- TOP @flg_2
DECLARE @t_print_data_count                                              INT             = 0;           -- 印刷データテーブルINSERT件数
DECLARE @err                                                             INT             = 0;           -- 等しい@error_msg
-- 日付データ
DECLARE @m_hiduke_unyou_date                                             CHAR(8)         = ''           -- 運用日

-- 売上報告_部門別売上げテーブル
DECLARE @t_bumonbetu_uriage_tenpo_cd                                      CHAR(4)        = '';          -- 店コード
DECLARE @t_bumonbetu_uriage_regi_no                                       CHAR(4)        = '';          -- レジNO
DECLARE @t_bumonbetu_uriage_bumon_cd                                      CHAR(2)        = '';          -- 部門コード
DECLARE @t_bumonbetu_uriage_kingaku                                       NUMERIC(9,0)   = 0;           -- 金額
DECLARE @t_bumonbetu_uriage_saisyuu_uriage_date                           CHAR(8)        = '';          -- 最終売上日
DECLARE @t_bumonbetu_uriage_saisyuu_uriage_date_last                      CHAR(8)        = '';          -- 最終売上日


DECLARE @t_bumonbetu_uriage_tenpo_cd_last                                 CHAR(4)        = '';          -- 店コード
DECLARE @t_bumonbetu_uriage_regi_no_last                                  CHAR(4)        = '';          -- レジNO
DECLARE @t_bumonbetu_uriage_bumon_cd_count                                NUMERIC(4,0)   = 0;           -- 部門コードcount
DECLARE @t_bumonbetu_uriage_bumon_cd_count_2                              NUMERIC(4,0)   = 0;           -- 部門コードcount
DECLARE @space_flg                                                        NUMERIC(4,0)   = 0;           -- space flg
DECLARE @t_bumonbetu_uriage_kingaku_sum                                   NUMERIC(9,0)   = 0;           -- 金額sum

-- 売上報告_売場別売上げテーブル
DECLARE @t_uribabetu_uriage_uriage_suuryou_sum                            NUMERIC(9,0)   = 0;           -- 売上数量
DECLARE @t_uribabetu_uriage_kyaku_suu_sum                                 NUMERIC(7,0)   = 0;           -- 客数
--DECLARE @t_uribabetu_uriage_kingaku_sum                                   NUMERIC(9,0)   = 0;           -- 金額 2013/02/22 Del
--DECLARE @t_uribabetu_uriage_kake_kingaku_sum                              NUMERIC(9,0)   = 0;           -- 掛金額 2013/02/22 Del
DECLARE @t_uribabetu_uriage_syouhizei_sum                                 NUMERIC(9,0)   = 0;           -- 消費税
-- 2013/02/22 Add Start
-- 売上報告_売掛金照会テーブル
DECLARE @t_urikakekin_syoukai_genkin_aridaka                              NUMERIC(9,0)   = 0;           -- 現金在高
-- 売上報告_売掛明細テーブル
DECLARE @t_urikake_meisai_urikake_kingaku                                 NUMERIC(9,0)   = 0;           -- 売掛金額
-- 2013/02/22 Add End

DECLARE @t_uribabetu_uriage_uriage_suuryou_sum_sum                        NUMERIC(9,0)   = 0;           -- 店計　点数
DECLARE @t_uribabetu_uriage_uriba_cd_count                                NUMERIC(3,0)   = 0;           -- RECORD数量
DECLARE @t_uribabetu_uriage_kyaku_suu_sum_sum                             NUMERIC(9,0)   = 0;           -- 店計　客数

-- 売上報告_割引テーブル
DECLARE @t_waribiki_kingaku_sum                                           NUMERIC(9,0)   = 0;           -- 金額sum

-- 売上報告_商品券テーブル
DECLARE @t_syouhinken_kingaku1                                            NUMERIC(9,0)   = 0;           -- D ビバ商品券
DECLARE @t_syouhinken_kingaku2                                            NUMERIC(9,0)   = 0;           -- E ビバ買物券
DECLARE @t_syouhinken_kingaku3                                            NUMERIC(9,0)   = 0;           -- F 他社商品券
DECLARE @t_syouhinken_kingaku7                                            NUMERIC(9,0)   = 0;           -- G クラブ買物券

-- 2017/03/25  李松涛  ポイント利用対応
DECLARE @t_syouhinken_kingaku8                                            NUMERIC(9,0)   = 0;           -- H ポイント利用対応

DECLARE @t_syouhinken_kingaku6                                            NUMERIC(9,0)   = 0;           -- I 金券釣
DECLARE @t_syouhinken_kingaku4                                            NUMERIC(9,0)   = 0;           -- K クレジット計   L
DECLARE @t_syouhinken_kaisuu4                                             NUMERIC(9,0)   = 0;           -- K クレジット件数 L
DECLARE @t_syouhinken_kingaku5                                            NUMERIC(9,0)   = 0;           -- L デビット計   M
DECLARE @t_syouhinken_kaisuu5                                             NUMERIC(9,0)   = 0;           -- デビット件数   M
--2018/02/09 李松涛 PAID掛を追加↓
DECLARE @t_syouhinken_kaisuu9                                             NUMERIC(9,0)   = 0;           -- K PAID掛 件数
DECLARE @t_syouhinken_kingaku9                                            NUMERIC(9,0)   = 0;           -- K PAID掛 金額
--2018/02/09 李松涛 PAID掛を追加↑



--売場データ
DECLARE @m_uriba_uriba_cd                                                 CHAR(2)        = '';          -- 売場コード
DECLARE @m_uriba_uriba_mei                                                VARCHAR(40)    = '';          -- 売場名

-- 事業所データ
DECLARE @m_jigyousyo_tenpo_mei_kanji                                      VARCHAR(40)    = '';          -- 店名（漢字）

-- 大分類データ
DECLARE @m_daibunrui_daibunrui_mei                                        VARCHAR(40)    = '';          -- 大分類名

-- 印刷データ
DECLARE @tyouhyou_id                                                      VARCHAR(8)     = 'FUA005';    -- 帳票ID
DECLARE @param_code                                                       VARCHAR(20)    = 'NAV4200A';  -- バッチID
--DECLARE @save_date                                                        DATETIME       = GETDATE();   -- 保存日時 2013/02/08 DEL
DECLARE @save_date                                                        DATETIME       ;   -- 保存日時  -- 2013/02/08 ADD
DECLARE @seq_no                                                           NUMERIC(6,0)   = 1;           -- SEQ
DECLARE @print_flg                                                        CHAR(1)        = '0';         -- 印刷フラグ
DECLARE @tenpo_cd                                                         CHAR(4)        = '' ;         -- 店コード
DECLARE @print_data_nasi_count                                            INT            = 0  ;         --印刷データなし挿入件数

BEGIN TRANSACTION NAV42001

-- ******************************
-- -- 日付データテーブルより運用日を取り出す。
-- ******************************
SELECT @m_hiduke_unyou_date = unyou_date FROM m_hiduke
--2013/03/25 Add Start
IF (@@error <> 0 OR ISDATE(@m_hiduke_unyou_date) <> 1)
BEGIN
	SET @error_msg = '運用日の取得でエラー'
	GOTO ERR_RTN;
END
--2013/03/25 Add End
SET    @t_bumonbetu_uriage_saisyuu_uriage_date = @m_hiduke_unyou_date;   --2013/01/06 MOD
--SET    @t_bumonbetu_uriage_saisyuu_uriage_date = '20121027';

-- 2012/11/26 Add Start
-- ******************************
-- 印刷データ_保存日時格納方法:運用日＋システム時刻
-- ******************************
SET @save_date = CONVERT(DATETIME,CONVERT(VARCHAR(8),@m_hiduke_unyou_date,105)+' '+CONVERT(VARCHAR(12),GETDATE(),114))
-- 2012/11/26 Add End
-- ******************************
-- 売上報告_部門別売上げテーブルよりデータを取得する。
-- ******************************
DECLARE cusr CURSOR FOR
    SELECT   tbu.tenpo_cd
            ,tbu.saisyuu_uriage_date
            ,tbu.regi_no
            ,tbu.bumon_cd
            ,tbu.kingaku
            ,ISNULL(md.daibunrui_mei,'')
            ,mj.tenpo_mei_kanji
    FROM     t_bumonbetu_uriage AS tbu
    LEFT OUTER JOIN  m_daibunrui AS md
    ON      (tbu.bumon_cd = md.daibunrui_cd)
    INNER JOIN m_jigyousyo AS mj
    ON      (tbu.tenpo_cd = mj.tenpo_cd)
    WHERE    --tbu.saisyuu_uriage_date <= @t_bumonbetu_uriage_saisyuu_uriage_date
             --tbu.data_sakusei_date = @m_hiduke_unyou_date   -- 2013/01/21 ADD
               tbu.data_sakusei_date = CONVERT(CHAR(8),DATEADD(D,-1,CONVERT(DATETIME,@m_hiduke_unyou_date)),112) --2013/01/23 ADD
         AND tbu.torikesi_kbn='0'
         AND tbu.regi_no = '9999' --2012/12/25 Add
--    ORDER BY tbu.saisyuu_uriage_date     --2013/02/15 DEL
--            ,tbu.tenpo_cd                --2013/02/15 DEL
    ORDER BY tbu.tenpo_cd                  --2013/02/15 ADD
            ,tbu.saisyuu_uriage_date       --2013/02/15 ADD
            ,tbu.regi_no
            ,tbu.bumon_cd
            ,tbu.kingaku;
            
-- カーソルオープン
OPEN cusr
--RAISERROR ('エラーテスト：t_bumonbetu_uriageのカーソル作成でエラー', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_bumonbetu_uriageのカーソル作成でエラー'
        GOTO END_RTN;
    END

-- 結果データの取得(先頭行の取得)
FETCH NEXT FROM cusr
           INTO @t_bumonbetu_uriage_tenpo_cd
               ,@t_bumonbetu_uriage_saisyuu_uriage_date
               ,@t_bumonbetu_uriage_regi_no
               ,@t_bumonbetu_uriage_bumon_cd
               ,@t_bumonbetu_uriage_kingaku
               ,@m_daibunrui_daibunrui_mei
               ,@m_jigyousyo_tenpo_mei_kanji 
    --IF (@@fetch_status = -2)  2013/03/15 Del 于磊 三月修正項目No.2
    IF (@@fetch_status <> 0 AND @@error <> 0) --2013/03/15 Add 于磊 三月修正項目No.2
    BEGIN
        SET @error_msg = '結果データの取得(先頭行の取得)でエラー'
        GOTO END_RTN;
    END
-- ******************************
-- 取得したデータより印刷データテーブルへ出力する。
-- ******************************
WHILE (@@fetch_status = 0)
BEGIN

/* 2013/02/15 DEL start
IF(@t_bumonbetu_uriage_saisyuu_uriage_date_last<>@t_bumonbetu_uriage_saisyuu_uriage_date)
BEGIN
   --SET @save_date = CONVERT(DATETIME,CONVERT(VARCHAR(8),@t_bumonbetu_uriage_saisyuu_uriage_date,105)+' '+CONVERT(VARCHAR(12),GETDATE(),114)); --2013/01/05 Del
   SET @save_date = CONVERT(DATETIME,CONVERT(VARCHAR(8),@m_hiduke_unyou_date,105)+' '+CONVERT(VARCHAR(12),GETDATE(),114)); --2013/01/05 Add
   SET @seq_no = 1;
END
  2013/02/15 DEL end*/

IF(@t_bumonbetu_uriage_tenpo_cd_last<>@t_bumonbetu_uriage_tenpo_cd)
BEGIN
   SET @seq_no = 1;
END


--IF(@t_bumonbetu_uriage_tenpo_cd_last<>@t_bumonbetu_uriage_tenpo_cd
  IF(@t_bumonbetu_uriage_saisyuu_uriage_date_last<>@t_bumonbetu_uriage_saisyuu_uriage_date
   OR @t_bumonbetu_uriage_tenpo_cd_last<>@t_bumonbetu_uriage_tenpo_cd
   OR @t_bumonbetu_uriage_regi_no_last<>@t_bumonbetu_uriage_regi_no)
BEGIN
   SET @flg=1;
   SET @flg_2=1;
   SET @t_bumonbetu_uriage_bumon_cd_count_2=0

--売上報告_割引 金額 取得 
       SELECT @t_waribiki_kingaku_sum=ISNULL(tw.kingaku,0)
       FROM   t_waribiki AS tw
       WHERE  tw.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND tw.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND tw.torikesi_kbn='0'
              AND tw.regi_no=@t_bumonbetu_uriage_regi_no
              AND tw.gyou_no=1;
--RAISERROR ('エラーテスト：t_waribikiのSELECTエラー', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_waribikiのSELECTエラー'
        GOTO END_RTN;
    END

--売上報告_売場別売上げテーブル 消費税,金額,掛金額 取得
       SELECT @t_uribabetu_uriage_syouhizei_sum=ISNULL(SUM(tuu.syouhizei),0)
             --,@t_uribabetu_uriage_kingaku_sum=ISNULL(SUM(tuu.kingaku),0) --2013/02/22 Del
             --,@t_uribabetu_uriage_kake_kingaku_sum=ISNULL(SUM(tuu.kake_kingaku),0) --2013/02/22 Del
       FROM   t_uribabetu_uriage AS tuu
       WHERE  tuu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND tuu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND tuu.torikesi_kbn='0'
              AND tuu.regi_no=@t_bumonbetu_uriage_regi_no
       GROUP BY tuu.saisyuu_uriage_date;
--RAISERROR ('エラーテスト：t_uribabetu_uriageのSELECTエラー (01)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_uribabetu_uriageのSELECTエラー (01)'
        GOTO END_RTN;
    END
-- 2013/02/22 Add Start
-- 売上報告_売掛金照会テーブル 現金在高 取得
       SELECT @t_urikakekin_syoukai_genkin_aridaka=ISNULL(SUM(tus.genkin_aridaka),0)
       FROM   t_urikakekin_syoukai AS tus
       WHERE  tus.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND tus.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND tus.torikesi_kbn='0'
              AND tus.regi_no=@t_bumonbetu_uriage_regi_no
       GROUP BY tus.saisyuu_uriage_date;
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_urikakekin_syoukaiのSELECTエラー (01)'
        GOTO END_RTN;
    END
-- 売上報告_売掛明細テーブル 売掛金額 取得
       SELECT @t_urikake_meisai_urikake_kingaku=ISNULL(SUM(tum.urikake_kingaku),0)
       FROM   t_urikake_meisai AS tum
       WHERE  tum.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND tum.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND tum.torikesi_kbn='0'
              AND tum.regi_no=@t_bumonbetu_uriage_regi_no
       GROUP BY tum.saisyuu_uriage_date;
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_urikake_meisaiのSELECTエラー (01)'
        GOTO END_RTN;
    END
-- 2013/02/22 Add End

--売上報告_商品券テーブル 金額 取得 行番号=1のレコード
       SELECT @t_syouhinken_kingaku1=ISNULL(ts.kingaku,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=1;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (01)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (01)'
        GOTO END_RTN;
    END

--売上報告_商品券テーブル 金額 取得 行番号=2のレコード
       SELECT @t_syouhinken_kingaku2=ISNULL(ts.kingaku,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=2;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (02)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (02)'
        GOTO END_RTN;
    END
--売上報告_商品券テーブル 金額 取得 行番号=3のレコード
       SELECT @t_syouhinken_kingaku3=ISNULL(ts.kingaku,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=3;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (03)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (03)'
        GOTO END_RTN;
    END

--売上報告_商品券テーブル 金額 取得 行番号=7のレコード
       SELECT @t_syouhinken_kingaku7=ISNULL(ts.kingaku,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=7;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (04)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (04)'
        GOTO END_RTN;
    END


-- 2017/03/25  李松涛  ポイント利用対応 追加
--売上報告_ポイント利用テーブル 金額 取得 行番号=8のレコード
       SELECT @t_syouhinken_kingaku8=ISNULL(tpr.kingaku,0)
       FROM   t_point_riyou AS tpr
       WHERE  tpr.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND tpr.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND tpr.torikesi_kbn='0'
              AND tpr.regi_no=@t_bumonbetu_uriage_regi_no
              AND tpr.gyou_no=1;
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_point_riyouのSELECTエラー (08)'
        GOTO END_RTN;
    END





--売上報告_商品券テーブル 金額 取得 行番号=6のレコード
       SELECT @t_syouhinken_kingaku6=ISNULL(ts.kingaku,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=6;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (05)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (05)'
        GOTO END_RTN;
    END

--売上報告_商品券テーブル 金額 取得 行番号=4のレコード
       SELECT @t_syouhinken_kingaku4=ISNULL(ts.kingaku,0)
             ,@t_syouhinken_kaisuu4=ISNULL(ts.kaisuu,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=4;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (06)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (06)'
        GOTO END_RTN;
    END

--売上報告_商品券テーブル 金額 取得 行番号=5のレコード
       SELECT @t_syouhinken_kingaku5=ISNULL(ts.kingaku,0)
             ,@t_syouhinken_kaisuu5=ISNULL(ts.kaisuu,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=5;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (07)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (07)'
        GOTO END_RTN;
    END

--売上報告_部門別売上げテーブル 店計金額
    SELECT   @t_bumonbetu_uriage_kingaku_sum=ISNULL(SUM(tbu.kingaku),0)
            ,@t_bumonbetu_uriage_bumon_cd_count=COUNT(tbu.bumon_cd)
    FROM     t_bumonbetu_uriage AS tbu
    WHERE    tbu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
             AND tbu.torikesi_kbn='0'
             AND tbu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
             AND tbu.regi_no=@t_bumonbetu_uriage_regi_no
    GROUP BY tbu.saisyuu_uriage_date;
--RAISERROR ('エラーテスト：t_bumonbetu_uriageのSELECTエラー', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_bumonbetu_uriageのSELECTエラー'
        GOTO END_RTN;
    END
    
--2018/02/09 李松涛 PAID掛を追加↓
--PAID 金額 取得 行番号=8のレコード
       SELECT @t_syouhinken_kingaku9=ISNULL(ts.kingaku,0)
             ,@t_syouhinken_kaisuu9=ISNULL(ts.kaisuu,0)
       FROM   t_syouhinken AS ts
       WHERE  ts.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
              AND ts.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
              AND ts.torikesi_kbn='0'
              AND ts.regi_no=@t_bumonbetu_uriage_regi_no
              AND ts.gyou_no=8;
--RAISERROR ('エラーテスト：t_syouhinkenのSELECTエラー (06)', 16, 1); 
IF (@@error <> 0)
    BEGIN
        SET @error_msg = 't_syouhinkenのSELECTエラー (06)'
        GOTO END_RTN;
    END
--2018/02/09 李松涛 PAID掛を追加↑
-- 印刷空行数
   SET @space_flg=108-(@t_bumonbetu_uriage_bumon_cd_count+1);            -- 108=(46+46+17)

IF (@seq_no = 1
    AND EXISTS(SELECT 1
        FROM   [t_print_data]
        WHERE  [tenpo_cd] = @t_bumonbetu_uriage_tenpo_cd
               AND    [tyouhyou_id] = @tyouhyou_id
               AND    [save_date] = @save_date))
 BEGIN
    SET @save_date = DATEADD(S, 1, @save_date)
 END

--出力 ヘッダ部 1
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- [Fixed Data Section]
                     VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no
                           ,''
                           ,'[Fixed Data Section]'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 店コード,
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 1
                           ,'店コード'
                           ,@t_bumonbetu_uriage_tenpo_cd 
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 店名,
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 2
                           ,'店名'
                           ,@m_jigyousyo_tenpo_mei_kanji
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 入力者
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 3
                           ,'入力者'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- R,
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 4
                           ,'R'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),  
-- データ日付(運用日)
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 5
                           ,'データ日付'
                           ,@m_hiduke_unyou_date
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 売上日,
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 6
                           ,'売上日'
                           ,@t_bumonbetu_uriage_saisyuu_uriage_date
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- レジNo
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 7
                           ,'レジNo'
                           ,@t_bumonbetu_uriage_regi_no
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- A TM割
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+8
                           ,'ＴＭ割_金額'
                           ,CONVERT(VARCHAR,@t_waribiki_kingaku_sum)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--B 税合計
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+9
                           ,'税合計_金額'
                           ,CONVERT(VARCHAR,@t_uribabetu_uriage_syouhizei_sum)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--C 現金在高
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+10
                           ,'現金在高_金額'
                           --,CONVERT(VARCHAR,@t_uribabetu_uriage_kingaku_sum) --2013/02/22 Del
                           ,CONVERT(VARCHAR,@t_urikakekin_syoukai_genkin_aridaka) --2013/02/22 Add
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--D ビバ商品券
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+11
                           ,'ビバ商品券_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku1)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--E ビバ買物券
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+12
                           ,'ビバ買物券_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku2)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--F 他社商品券
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+13
                           ,'他社商品券_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku3)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--G クラブ買物券
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+14
                           ,'クラブ買物券_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku7)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
                           
--I 金券釣
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+15
                           ,'金券釣_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku6)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--I その他掛計
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+16
                           ,'その他掛計_金額'
                           --,CONVERT(VARCHAR,@t_uribabetu_uriage_kake_kingaku_sum) --2013/02/22 Del
                           ,CONVERT(VARCHAR,@t_urikake_meisai_urikake_kingaku) --2013/02/22 Add
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--K クレジット計
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+17
                           ,'クレジット計_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku4)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--L デビット計
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+18
                           ,'デビット計_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku5)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--クレジット件数
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+19
                           ,'クレジット計_回数'
                           ,CONVERT(VARCHAR,@t_syouhinken_kaisuu4)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--デビット件数
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+20
                           ,'デビット計_回数'
                           ,CONVERT(VARCHAR,@t_syouhinken_kaisuu5)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
--H ポイント利用対応
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+21
                           ,'ポイント利用_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku8)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+22
                           ,'Ｐａｉｄ計_金額'
                           ,CONVERT(VARCHAR,@t_syouhinken_kingaku9)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+23
                           ,'Ｐａｉｄ計_回数'
                           ,CONVERT(VARCHAR,@t_syouhinken_kaisuu9)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
--RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (01)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (01)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 24;


--売上報告_売場別売上げテーブル 店計客数 
         SELECT @t_uribabetu_uriage_uriba_cd_count=COUNT(DISTINCT(tuu.uriba_cd))
                --,@t_uribabetu_uriage_kyaku_suu_sum_sum=ISNULL(SUM(tuu.kyaku_suu),0) --2012/12/25 Del
                ,@t_uribabetu_uriage_uriage_suuryou_sum_sum=ISNULL(SUM(tuu.uriage_suuryou),0)
         FROM   t_uribabetu_uriage AS tuu
               ,m_uriba AS mu --2012/12/25 Add
         WHERE  tuu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
                AND tuu.uriba_cd=mu.uriba_cd --2012/12/25 Add
                AND tuu.uriba_cd < '17' --2012/12/25 Add
                AND tuu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
                AND tuu.torikesi_kbn='0'
                AND tuu.regi_no=@t_bumonbetu_uriage_regi_no;
--RAISERROR ('エラーテスト：t_uribabetu_uriageのSELECTエラー (02)', 16, 1); 
                IF (@@error <> 0)
                    BEGIN
                        SET @error_msg = 't_uribabetu_uriageのSELECTエラー (02)'
                        GOTO END_RTN;
                    END
                    
--2012/12/25 Add Start
--売上報告_売場別売上げテーブル 店計客数 
         SELECT 
                @t_uribabetu_uriage_kyaku_suu_sum_sum=tuu.kyaku_suu
         FROM   t_uribabetu_uriage AS tuu
         WHERE  tuu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
                AND tuu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
                AND tuu.torikesi_kbn='0'
                AND tuu.data_syubetu='03'
                AND tuu.uriba_cd='00'
                AND tuu.online_kbn='1'
                AND tuu.regi_no=@t_bumonbetu_uriage_regi_no;
--RAISERROR ('エラーテスト：t_uribabetu_uriageのSELECTエラー (02)', 16, 1); 
                IF (@@error <> 0)
                    BEGIN
                        SET @error_msg = 't_uribabetu_uriageのSELECTエラー (02)'
                        GOTO END_RTN;
                    END
--2012/12/25 Add End

WHILE(@t_uribabetu_uriage_uriba_cd_count>0)
BEGIN

--売上報告_売場別売上げテーブル 客数 
   SELECT TOP (@flg) @t_uribabetu_uriage_kyaku_suu_sum=ISNULL(SUM(tuu.kyaku_suu),0)
   FROM   t_uribabetu_uriage AS tuu
         ,m_uriba AS mu --2012/12/25 Add
   WHERE  tuu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
          AND tuu.uriba_cd=mu.uriba_cd --2012/12/25 Add
          AND tuu.uriba_cd < '17' --2012/12/25 Add
          AND tuu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
          AND tuu.torikesi_kbn='0'
          AND tuu.regi_no=@t_bumonbetu_uriage_regi_no
   GROUP BY  tuu.uriba_cd
   ORDER BY  tuu.uriba_cd;
--RAISERROR ('エラーテスト：t_uribabetu_uriageのSELECTエラー (03)', 16, 1); 
                IF (@@error <> 0)
                    BEGIN
                        SET @error_msg = 't_uribabetu_uriageのSELECTエラー (03)'
                        GOTO END_RTN;
                    END

--出力 ヘッダ部 2
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- 客数
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,'客数'+CONVERT(VARCHAR,@flg)
                           ,@t_uribabetu_uriage_kyaku_suu_sum
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (02)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (02)'
                        GOTO END_RTN;
                    END
            SET @seq_no = @seq_no + 1;
            SET @flg=@flg+1;
            IF(@t_uribabetu_uriage_uriba_cd_count=1)
              BEGIN
--出力 ヘッダ部 3
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- 店計客数
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,'店計客数'
                           ,@t_uribabetu_uriage_kyaku_suu_sum_sum
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 店計点数
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+1 
                           ,'店計点数'
                           ,@t_uribabetu_uriage_uriage_suuryou_sum_sum
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());

         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (03)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (03)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 2;
END
                SET @t_uribabetu_uriage_uriba_cd_count=@t_uribabetu_uriage_uriba_cd_count-1;

END --(客数)
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- PENDE
                     VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no
                           ,'PENDE'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- ;
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+1 
                           ,''
                           ,';'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- [Table Data Section]
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no +2
                           ,''
                           ,'[Table Data Section]'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (04)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;
                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (04)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 3;
END --(ヘッダ部)

--（明細）
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時

-- 明細1(（大分類）ＤＰ,（大分類名）ＤＰ名,金額)
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,''
                           ,@t_bumonbetu_uriage_bumon_cd+
                           ','+@m_daibunrui_daibunrui_mei+
                           ','+CONVERT(VARCHAR,@t_bumonbetu_uriage_kingaku)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (05)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (05)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 1;
                SET @t_bumonbetu_uriage_bumon_cd_count_2=@t_bumonbetu_uriage_bumon_cd_count_2+1;
--
IF(@t_bumonbetu_uriage_bumon_cd_count>45 AND @t_bumonbetu_uriage_bumon_cd_count_2=45)
   BEGIN
--
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- ',,'
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,''
                           ,',,'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (09)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (09)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 1;
   END
   
 ELSE IF(@t_bumonbetu_uriage_bumon_cd_count=45 AND @t_bumonbetu_uriage_bumon_cd_count_2=45)
         BEGIN
           SET @space_flg=@space_flg+1;
         END
 ELSE IF(@t_bumonbetu_uriage_bumon_cd_count<45 AND @t_bumonbetu_uriage_bumon_cd_count_2=@t_bumonbetu_uriage_bumon_cd_count)
   BEGIN
     SET @space_flg=@space_flg+1;
   END  
--店計 金額
IF(@t_bumonbetu_uriage_bumon_cd_count_2 = @t_bumonbetu_uriage_bumon_cd_count)
   BEGIN
--明細2(店計 金額)
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- 店計 金額
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,''
                           ,','+'店計'+','+CONVERT(VARCHAR,@t_bumonbetu_uriage_kingaku_sum)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (06)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (06)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 1;


WHILE(@space_flg>0)
  BEGIN
--明細3(space)
                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
-- ,,
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,''
                           ,',,'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (07)', 16, 1);
         SELECT @err=@@error
               ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (07)'
                        GOTO END_RTN;
                    END
                SET @seq_no = @seq_no + 1;
                SET @space_flg=@space_flg-1;
END
--（明細）4
--売上報告_売場別売上げテーブル 店計 点数
         SELECT @t_uribabetu_uriage_uriba_cd_count=COUNT(DISTINCT(tuu.uriba_cd))
         FROM   t_uribabetu_uriage AS tuu
               ,m_uriba AS mu --2012/12/25 Add
         WHERE  tuu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
                AND tuu.uriba_cd=mu.uriba_cd --2012/12/25 Add
                AND tuu.uriba_cd < '17' --2012/12/25 Add
                AND tuu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
                AND tuu.torikesi_kbn='0'
                AND tuu.regi_no=@t_bumonbetu_uriage_regi_no;
         --RAISERROR ('エラーテスト：t_uribabetu_uriageののSELECTエラー (04)', 16, 1); 
                IF (@@error <> 0)
                    BEGIN
                        SET @error_msg = 't_uribabetu_uriageのSELECTエラー (04)'
                        GOTO END_RTN;
                    END

WHILE(@t_uribabetu_uriage_uriba_cd_count>0)
BEGIN
--売上報告_売場別売上げテーブル点数
   SELECT TOP (@flg_2) @m_uriba_uriba_cd=ISNULL(tuu.uriba_cd,'')
         , @m_uriba_uriba_mei=ISNULL(mu.uriba_mei,'')
         , @t_uribabetu_uriage_uriage_suuryou_sum=ISNULL(SUM(tuu.uriage_suuryou),0)
   FROM   t_uribabetu_uriage AS tuu
         ,m_uriba AS mu
   WHERE  tuu.tenpo_cd=@t_bumonbetu_uriage_tenpo_cd
          AND tuu.saisyuu_uriage_date=@t_bumonbetu_uriage_saisyuu_uriage_date
          AND tuu.torikesi_kbn='0'
          AND tuu.regi_no=@t_bumonbetu_uriage_regi_no
          AND tuu.uriba_cd=mu.uriba_cd
          AND tuu.uriba_cd < '17' --2012/12/25 Add
   GROUP BY tuu.uriba_cd
           ,mu.uriba_mei
   ORDER BY tuu.uriba_cd;
         --RAISERROR ('エラーテスト：t_uribabetu_uriageのSELECTエラー (05)', 16, 1); 
                IF (@@error <> 0)
                    BEGIN
                        SET @error_msg = 't_uribabetu_uriageのSELECTエラー (05)'
                        GOTO END_RTN;
                    END

                INSERT INTO [t_print_data]                              
                           ([tenpo_cd]                                  -- 店コド
                           ,[tyouhyou_id]                               -- 帳票ID
                           ,[save_date]                                 -- 保存日時
                           ,[seq_no]                                    -- SEQ
                           ,[item]                                      -- アイテム
                           ,[print_image]                               -- 印刷イメージ
                           ,[print_flg]                                 -- 印刷フラグ
                           ,[touroku_user]                              -- 登録者
                           ,[touroku_date]                              -- 登録日時
                           ,[kousin_user]                               -- 更新者
                           ,[kousin_date])                              -- 更新日時
--明細4(点数)
                      VALUES
                           (@t_bumonbetu_uriage_tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no 
                           ,''
                           ,@m_uriba_uriba_cd+
                           ','+@m_uriba_uriba_mei+
                           ','+CONVERT(VARCHAR,@t_uribabetu_uriage_uriage_suuryou_sum)
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE());
         --RAISERROR ('エラーテスト：t_print_dataのINSERTでエラー (08)', 16, 1);
                  SELECT @err=@@error
                        ,@t_print_data_count =@t_print_data_count + @@ROWCOUNT;

                IF (@err <> 0)
                    BEGIN
                        SET @error_msg = 't_print_dataのINSERTでエラー (08)'
                        GOTO END_RTN;
                    END
            SET @seq_no = @seq_no + 1;
            SET @flg_2=@flg_2+1;
                SET @t_uribabetu_uriage_uriba_cd_count=@t_uribabetu_uriage_uriba_cd_count-1;
END
END
                SET @t_bumonbetu_uriage_saisyuu_uriage_date_last = @t_bumonbetu_uriage_saisyuu_uriage_date;
                SET @t_bumonbetu_uriage_tenpo_cd_last = @t_bumonbetu_uriage_tenpo_cd;
                SET @t_bumonbetu_uriage_regi_no_last = @t_bumonbetu_uriage_regi_no;

-- 結果データの取得(次行の取得)
FETCH NEXT FROM cusr
           INTO @t_bumonbetu_uriage_tenpo_cd
               ,@t_bumonbetu_uriage_saisyuu_uriage_date
               ,@t_bumonbetu_uriage_regi_no
               ,@t_bumonbetu_uriage_bumon_cd
               ,@t_bumonbetu_uriage_kingaku
               ,@m_daibunrui_daibunrui_mei
               ,@m_jigyousyo_tenpo_mei_kanji           --2012/11/08 Add
            --IF (@@fetch_status = -2)  2013/03/15 Del 于磊 三月修正項目No.2
            IF (@@fetch_status <> 0 AND @@error <> 0) --2013/03/15 Add 于磊 三月修正項目No.2
            BEGIN
                SET @error_msg = '結果データの取得(次行の取得)でエラー'
                GOTO END_RTN;
            END

END --(WHILE END)

-- 印刷データなし
IF (EXISTS(SELECT 1
           FROM   [m_tyouhyou]
           WHERE  [tyouhyou_id] = @tyouhyou_id
           AND    [zeroken_tyouhyou_kbn] = '1'))
    BEGIN
        DECLARE @print_data TABLE (
                [tenpo_cd] [char](4) NOT NULL,
                [tyouhyou_id] [varchar](8) NOT NULL,
                [save_date] [datetime] NOT NULL,
                [seq_no] [numeric](6, 0) NOT NULL,
                [item] [varchar](30) NOT NULL,
                [print_image] [varchar](2048) NULL,
                [print_flg] [char](1) NULL,
                [touroku_user] [varchar](20) NOT NULL,
                [touroku_date] [datetime] NOT NULL,
                [kousin_user] [varchar](20) NOT NULL,
                [kousin_date] [datetime] NOT NULL
            )
    -- （タイトル部）
            SET @seq_no = 1
            INSERT INTO @print_data
                       ([tenpo_cd]                                  -- 店コード
                       ,[tyouhyou_id]                               -- 帳票ID
                       ,[save_date]                                 -- 保存日時
                       ,[seq_no]                                    -- SEQ
                       ,[item]                                      -- アイテム
                       ,[print_image]                               -- 印刷イメージ
                       ,[print_flg]                                 -- 印刷フラグ
                       ,[touroku_user]                              -- 登録者
                       ,[touroku_date]                              -- 登録日時
                       ,[kousin_user]                               -- 更新者
                       ,[kousin_date])                              -- 更新日時
                 VALUES
-- [Fixed Data Section]
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no
                           ,''
                           ,'[Fixed Data Section]'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- データ日付(運用日)
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 1
                           ,'データ日付'
                           ,@m_hiduke_unyou_date
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 入力者
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 2
                           ,'入力者'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),             
-- R,
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 3
                           ,'R'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),  
-- 店コード,
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 4
                           ,'店コード'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 店名,
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 5
                           ,'店名'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 売上日,
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 6
                           ,'売上日'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- レジNo
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no + 7
                           ,'レジNo'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- PENDE
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+8
                           ,'PENDE'
                           ,''
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- ;
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no+9 
                           ,''
                           ,';'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- [Table Data Section]
                           (@tenpo_cd
                           ,@tyouhyou_id
                           ,@save_date
                           ,@seq_no +10
                           ,''
                           ,'[Table Data Section]'
                           ,@print_flg
                           ,@param_code
                           ,GETDATE()
                           ,@param_code
                           ,GETDATE()),
-- 印刷データなし
                       (@tenpo_cd
                       ,@tyouhyou_id
                       ,@save_date
                       ,@seq_no + 11
                       ,''
--                       ,',対象データなし'                       --2013/02/15 DEL
                       ,',対象データ無し'                         --2013/02/15 ADD
                       ,'0'
                       ,@param_code
                       ,GETDATE()
                       ,@param_code
                       ,GETDATE());
            IF (@@ERROR <> 0)
                BEGIN
                    SET @error_msg = '@print_dataの印刷データなしのINSERTでエラー'
                    GOTO END_RTN;
                END
            INSERT INTO [t_print_data]                              
                       ([tenpo_cd]                                  -- 店コード
                       ,[tyouhyou_id]                               -- 帳票ID
                       ,[save_date]                                 -- 保存日時
                       ,[seq_no]                                    -- SEQ
                       ,[item]                                      -- アイテム
                       ,[print_image]                               -- 印刷イメージ
                       ,[print_flg]                                 -- 印刷フラグ
                       ,[touroku_user]                              -- 登録者
                       ,[touroku_date]                              -- 登録日時
                       ,[kousin_user]                               -- 更新者
                       ,[kousin_date])                              -- 更新日時
                SELECT  mj.tenpo_cd
                       ,@tyouhyou_id
                       ,@save_date
                       ,pd.seq_no
                       ,pd.item
                       ,CASE WHEN pd.item = '店コード' THEN mj.tenpo_cd
                             WHEN pd.item = '店名'     THEN mj.tenpo_mei_kanji
                             ELSE pd.print_image END
                       ,@print_flg
                       ,@param_code
                       ,GETDATE()
                       ,@param_code
                       ,GETDATE()
                FROM   m_jigyousyo AS mj
                       LEFT OUTER JOIN @print_data AS pd ON(1 = 1)
                WHERE   mj.system_riyou_kaisi_date    <= @m_hiduke_unyou_date
                    /* 2013/02/07 Del Start
                    AND ((
                        mj.system_riyou_syuuryou_date <> '00000000'
                    AND mj.system_riyou_syuuryou_date >= @m_hiduke_unyou_date
                        )
                     OR mj.system_riyou_syuuryou_date =  '00000000'
                        )
                    2013/02/07 Del End */
                    AND mj.system_riyou_syuuryou_date >= @m_hiduke_unyou_date --2013/02/07 Add
                    AND mj.jigyousyo_hanbetu_kbn      =  '1'
                    AND NOT EXISTS(
                        SELECT tp.tenpo_cd
                          FROM t_print_data AS tp                                 --印刷データ
                         WHERE tp.tenpo_cd    = mj.tenpo_cd
                           AND tp.tyouhyou_id = @tyouhyou_id
                           AND convert(varchar(8),tp.save_date,112)   = CONVERT(CHAR(8), @save_date, 112)
                        )
            SELECT @print_data_nasi_count = @print_data_nasi_count + @@ROWCOUNT, @err = @@ERROR 
            IF (@err <> 0)
                BEGIN
                    SET @error_msg = 't_print_dataの印刷データなしのINSERTでエラー'
                    GOTO END_RTN;
                END
    END
-- 終了処理
END_RTN:
IF (@error_msg = '')
    BEGIN
        CLOSE cusr
        DEALLOCATE cusr
        COMMIT TRANSACTION NAV42001
        PRINT '印刷データテーブルINSERT件数' + CONVERT(VARCHAR,@t_print_data_count) + '件'
        PRINT '印刷データ無し挿入件数 ' + CONVERT(VARCHAR,@print_data_nasi_count) + '件'
        PRINT 'OSQL Success'
    END
ELSE
    BEGIN
        CLOSE cusr
        DEALLOCATE cusr
        ERR_RTN:--2013/03/25 Add
        ROLLBACK TRANSACTION NAV42001
        PRINT 'OSQL Failure'
        PRINT @error_msg
    END


update m_hiduke set unyou_date='20180213'