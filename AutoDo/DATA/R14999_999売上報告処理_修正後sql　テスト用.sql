-- *********************************************************************************************************************
-- システム名   : ビバ新ＳＡシステム (VIVA-SA)
-- モジュール名 : NAV75908.SQL
-- 処理名       : 売上報告処理（TS）
-- 注意事項     : 
--
-- Date        EDP-No   所属/氏名              内   容
-- --------------------------------------------------------------------------------------------------------------------
-- 2016/07/02  P-xxxxx  大連/李松涛            新規作成
-- yyyy/mm/dd  P-xxxxx  開発X部/○○           変更
-- *********************************************************************************************************************
DECLARE @programid                         VARCHAR(20)    = 'NAV75908';     -- バッチID
DECLARE @error_msg                         VARCHAR(100)   = ''
DECLARE @tenpocd                           VARCHAR(4)     -- 店舗番号 
DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
DECLARE @t_ts_uriage_houkoku_data_kakutei_if_count    INT  = 0           -- TS売上報告テーブルに情報を更新した件数
DECLARE @t_bumonbetu_uriage_insert_count              INT  = 0           --売上報告_部門別売上データ登録件数
DECLARE @t_bumonbetu_uriage_delete_count              INT  = 0           --売上報告_部門別売上データ削除件数
DECLARE @t_uribabetu_uriage_insert_count              INT  = 0           --売上報告_売場別売上データ登録件数
DECLARE @t_uribabetu_uriage_delete_count              INT  = 0           --売上報告_売場別売上データ削除件数
DECLARE @t_urikakekin_syoukai_delete_count            INT  = 0           --売上報告_売掛金照会テーブル削除件数
DECLARE @t_urikakekin_syoukai_insert_count            INT  = 0           --売上報告_売掛金照会テーブル登録件数
DECLARE @t_syouhinken_delete_count                    INT  = 0           --売上報告_商品券テーブル削除件数
DECLARE @t_syouhinken_insert_count                    INT  = 0           --売上報告_商品券テーブル登録件数
DECLARE @t_waribiki_insert_count                      INT  = 0           -- 売上報告_割引テーブル登録件数
DECLARE @t_waribiki_delete_count                      INT  = 0           -- 売上報告_割引テーブル削除件数
DECLARE @t_waribiki_saimoku_cd             VARCHAR(5)     = '83526'      -- 売上報告_割引テーブルの細目コード
DECLARE @t_urikake_meisai_insert_count     INT            = 0            -- 売上報告_売掛金明細テーブル登録件数
DECLARE @t_urikake_meisai_delete_count     INT            = 0            -- 売上報告_売掛金明細テーブル削除件数



DECLARE @err                               INT            = 0;               -- エラーフラグ
DECLARE @m_hiduke_unyou_date				CHAR(8)     = '';

BEGIN TRANSACTION NAV75908

SELECT @tenpocd = '9991';             --2013/02/18 Add

SELECT @tenpo_eigyou_date=REPLACE(jikkou_date,'/','') 
FROM t_ts_sonota_uriage_kanryou_data_record_if 
WHERE file_mei LIKE '%LVURI102%'


-- ******************************
-- TS売上報告テーブルデータを削除
-- ******************************
DELETE t_uriage_houkoku

-- ******************************
-- 1.TS売上報告テーブル登録処理
-- ******************************
INSERT INTO t_uriage_houkoku(uriage_date                     -- 売上日付
                            ,tenpo_cd                        -- 店コード
                            ,regi_no                         -- レジNO
                            ,bumon_cd_01                     -- 部門コード1
                            ,bumon_uriage_kingaku_01         -- 部門売上金額1
                            ,bumon_cd_02                     -- 部門コード2
                            ,bumon_uriage_kingaku_02         -- 部門売上金額2
                            ,bumon_cd_03                     -- 部門コード3
                            ,bumon_uriage_kingaku_03         -- 部門売上金額3
                            ,bumon_cd_04                     -- 部門コード4
                            ,bumon_uriage_kingaku_04         -- 部門売上金額4
                            ,bumon_cd_05                     -- 部門コード5
                            ,bumon_uriage_kingaku_05         -- 部門売上金額5
                            ,bumon_cd_06                     -- 部門コード6
                            ,bumon_uriage_kingaku_06                    -- 部門売上金額6
                            ,bumon_cd_07                    -- 部門コード7
                            ,bumon_uriage_kingaku_07                    -- 部門売上金額7
                            ,bumon_cd_08                    -- 部門コード8
                            ,bumon_uriage_kingaku_08                    -- 部門売上金額8
                            ,bumon_cd_09                    -- 部門コード9
                            ,bumon_uriage_kingaku_09                    -- 部門売上金額9
                            ,bumon_cd_10                    -- 部門コード10
                            ,bumon_uriage_kingaku_10                    -- 部門売上金額10
                            ,bumon_cd_11                    -- 部門コード11
                            ,bumon_uriage_kingaku_11                    -- 部門売上金額11
                            ,bumon_cd_12                    -- 部門コード12
                            ,bumon_uriage_kingaku_12                    -- 部門売上金額12
                            ,bumon_cd_13                    -- 部門コード13
                            ,bumon_uriage_kingaku_13                    -- 部門売上金額13
                            ,bumon_cd_14                    -- 部門コード14
                            ,bumon_uriage_kingaku_14                    -- 部門売上金額14
                            ,bumon_cd_15                    -- 部門コード15
                            ,bumon_uriage_kingaku_15                    -- 部門売上金額15
                            ,bumon_cd_16                    -- 部門コード16
                            ,bumon_uriage_kingaku_16                    -- 部門売上金額16
                            ,bumon_cd_17                    -- 部門コード17
                            ,bumon_uriage_kingaku_17                    -- 部門売上金額17
                            ,bumon_cd_18                    -- 部門コード18
                            ,bumon_uriage_kingaku_18                    -- 部門売上金額18
                            ,bumon_cd_19                    -- 部門コード19
                            ,bumon_uriage_kingaku_19                    -- 部門売上金額19
                            ,bumon_cd_20                    -- 部門コード20
                            ,bumon_uriage_kingaku_20                    -- 部門売上金額20
                            ,bumon_cd_21                    -- 部門コード21
                            ,bumon_uriage_kingaku_21                    -- 部門売上金額21
                            ,bumon_cd_22                    -- 部門コード22
                            ,bumon_uriage_kingaku_22                    -- 部門売上金額22
                            ,bumon_cd_23                    -- 部門コード23
                            ,bumon_uriage_kingaku_23                    -- 部門売上金額23
                            ,bumon_cd_24                    -- 部門コード24
                            ,bumon_uriage_kingaku_24                    -- 部門売上金額24
                            ,bumon_cd_25                    -- 部門コード25
                            ,bumon_uriage_kingaku_25                    -- 部門売上金額25
                            ,bumon_cd_26                    -- 部門コード26
                            ,bumon_uriage_kingaku_26                    -- 部門売上金額26
                            ,bumon_cd_27                    -- 部門コード27
                            ,bumon_uriage_kingaku_27                    -- 部門売上金額27
                            ,bumon_cd_28                    -- 部門コード28
                            ,bumon_uriage_kingaku_28                    -- 部門売上金額28
                            ,bumon_cd_29                    -- 部門コード29
                            ,bumon_uriage_kingaku_29                    -- 部門売上金額29
                            ,bumon_cd_30                    -- 部門コード30
                            ,bumon_uriage_kingaku_30                    -- 部門売上金額30
                            ,bumon_cd_31                    -- 部門コード31
                            ,bumon_uriage_kingaku_31                    -- 部門売上金額31
                            ,bumon_cd_32                    -- 部門コード32
                            ,bumon_uriage_kingaku_32                    -- 部門売上金額32
                            ,bumon_cd_33                    -- 部門コード33
                            ,bumon_uriage_kingaku_33                    -- 部門売上金額33
                            ,bumon_cd_34                    -- 部門コード34
                            ,bumon_uriage_kingaku_34                    -- 部門売上金額34
                            ,bumon_cd_35                    -- 部門コード35
                            ,bumon_uriage_kingaku_35                    -- 部門売上金額35
                            ,bumon_cd_36                    -- 部門コード36
                            ,bumon_uriage_kingaku_36                    -- 部門売上金額36
                            ,bumon_cd_37                    -- 部門コード37
                            ,bumon_uriage_kingaku_37                    -- 部門売上金額37
                            ,bumon_cd_38                    -- 部門コード38
                            ,bumon_uriage_kingaku_38                    -- 部門売上金額38
                            ,bumon_cd_39                    -- 部門コード39
                            ,bumon_uriage_kingaku_39                    -- 部門売上金額39
                            ,bumon_cd_40                    -- 部門コード40
                            ,bumon_uriage_kingaku_40                    -- 部門売上金額40
                            ,bumon_cd_41                    -- 部門コード41
                            ,bumon_uriage_kingaku_41                    -- 部門売上金額41
                            ,bumon_cd_42                    -- 部門コード42
                            ,bumon_uriage_kingaku_42                    -- 部門売上金額42
                            ,bumon_cd_43                    -- 部門コード43
                            ,bumon_uriage_kingaku_43                    -- 部門売上金額43
                            ,bumon_cd_44                    -- 部門コード44
                            ,bumon_uriage_kingaku_44                    -- 部門売上金額44
                            ,bumon_cd_45                    -- 部門コード45
                            ,bumon_uriage_kingaku_45                    -- 部門売上金額45
                            ,bumon_cd_46                    -- 部門コード46
                            ,bumon_uriage_kingaku_46                    -- 部門売上金額46
                            ,bumon_cd_47                    -- 部門コード47
                            ,bumon_uriage_kingaku_47                    -- 部門売上金額47
                            ,bumon_cd_48                    -- 部門コード48
                            ,bumon_uriage_kingaku_48                    -- 部門売上金額48
                            ,bumon_cd_49                    -- 部門コード49
                            ,bumon_uriage_kingaku_49                    -- 部門売上金額49
                            ,bumon_cd_50                    -- 部門コード50
                            ,bumon_uriage_kingaku_50                    -- 部門売上金額50
                            ,bumon_cd_51                    -- 部門コード51
                            ,bumon_uriage_kingaku_51                    -- 部門売上金額51
                            ,bumon_cd_52                    -- 部門コード52
                            ,bumon_uriage_kingaku_52                    -- 部門売上金額52
                            ,bumon_cd_53                    -- 部門コード53
                            ,bumon_uriage_kingaku_53                    -- 部門売上金額53
                            ,bumon_cd_54                    -- 部門コード54
                            ,bumon_uriage_kingaku_54                    -- 部門売上金額54
                            ,bumon_cd_55                    -- 部門コード55
                            ,bumon_uriage_kingaku_55                    -- 部門売上金額55
                            ,bumon_cd_56                    -- 部門コード56
                            ,bumon_uriage_kingaku_56                    -- 部門売上金額56
                            ,bumon_cd_57                    -- 部門コード57
                            ,bumon_uriage_kingaku_57                    -- 部門売上金額57
                            ,bumon_cd_58                    -- 部門コード58
                            ,bumon_uriage_kingaku_58                    -- 部門売上金額58
                            ,bumon_cd_59                    -- 部門コード59
                            ,bumon_uriage_kingaku_59                    -- 部門売上金額59
                            ,bumon_cd_60                    -- 部門コード60
                            ,bumon_uriage_kingaku_60                    -- 部門売上金額60
                            ,bumon_cd_61                    -- 部門コード61
                            ,bumon_uriage_kingaku_61                    -- 部門売上金額61
                            ,bumon_cd_62                    -- 部門コード62
                            ,bumon_uriage_kingaku_62                    -- 部門売上金額62
                            ,bumon_cd_63                    -- 部門コード63
                            ,bumon_uriage_kingaku_63                    -- 部門売上金額63
                            ,bumon_cd_64                    -- 部門コード64
                            ,bumon_uriage_kingaku_64                    -- 部門売上金額64
                            ,bumon_cd_65                    -- 部門コード65
                            ,bumon_uriage_kingaku_65                    -- 部門売上金額65
                            ,bumon_cd_66                    -- 部門コード66
                            ,bumon_uriage_kingaku_66                    -- 部門売上金額66
                            ,bumon_cd_67                    -- 部門コード67
                            ,bumon_uriage_kingaku_67                    -- 部門売上金額67
                            ,bumon_cd_68                    -- 部門コード68
                            ,bumon_uriage_kingaku_68                    -- 部門売上金額68
                            ,bumon_cd_69                    -- 部門コード69
                            ,bumon_uriage_kingaku_69                    -- 部門売上金額69
                            ,bumon_cd_70                    -- 部門コード70
                            ,bumon_uriage_kingaku_70                    -- 部門売上金額70
                            ,bumon_cd_71                    -- 部門コード71
                            ,bumon_uriage_kingaku_71                    -- 部門売上金額71
                            ,bumon_cd_72                    -- 部門コード72
                            ,bumon_uriage_kingaku_72                    -- 部門売上金額72
                            ,bumon_cd_73                    -- 部門コード73
                            ,bumon_uriage_kingaku_73                    -- 部門売上金額73
                            ,bumon_cd_74                    -- 部門コード74
                            ,bumon_uriage_kingaku_74                    -- 部門売上金額74
                            ,bumon_cd_75                    -- 部門コード75
                            ,bumon_uriage_kingaku_75                    -- 部門売上金額75
                            ,bumon_cd_76                    -- 部門コード76
                            ,bumon_uriage_kingaku_76                    -- 部門売上金額76
                            ,bumon_cd_77                    -- 部門コード77
                            ,bumon_uriage_kingaku_77                    -- 部門売上金額77
                            ,bumon_cd_78                    -- 部門コード78
                            ,bumon_uriage_kingaku_78                    -- 部門売上金額78
                            ,bumon_cd_79                    -- 部門コード79
                            ,bumon_uriage_kingaku_79                    -- 部門売上金額79
                            ,bumon_cd_80                    -- 部門コード80
                            ,bumon_uriage_kingaku_80                    -- 部門売上金額80
                            ,bumon_cd_81                    -- 部門コード81
                            ,bumon_uriage_kingaku_81                    -- 部門売上金額81
                            ,bumon_cd_82                    -- 部門コード82
                            ,bumon_uriage_kingaku_82                    -- 部門売上金額82
                            ,bumon_cd_83                    -- 部門コード83
                            ,bumon_uriage_kingaku_83                    -- 部門売上金額83
                            ,bumon_cd_84                    -- 部門コード84
                            ,bumon_uriage_kingaku_84                    -- 部門売上金額84
                            ,bumon_cd_85                    -- 部門コード85
                            ,bumon_uriage_kingaku_85                    -- 部門売上金額85
                            ,bumon_cd_86                    -- 部門コード86
                            ,bumon_uriage_kingaku_86                    -- 部門売上金額86
                            ,bumon_cd_87                    -- 部門コード87
                            ,bumon_uriage_kingaku_87                    -- 部門売上金額87
                            ,bumon_cd_88                    -- 部門コード88
                            ,bumon_uriage_kingaku_88                    -- 部門売上金額88
                            ,bumon_cd_89                    -- 部門コード89
                            ,bumon_uriage_kingaku_89                    -- 部門売上金額89
                            ,bumon_cd_90                    -- 部門コード90
                            ,bumon_uriage_kingaku_90                    -- 部門売上金額90
                            ,bumon_cd_91                    -- 部門コード91
                            ,bumon_uriage_kingaku_91                    -- 部門売上金額91
                            ,bumon_cd_92                    -- 部門コード92
                            ,bumon_uriage_kingaku_92                    -- 部門売上金額92
                            ,bumon_cd_93                    -- 部門コード93
                            ,bumon_uriage_kingaku_93                    -- 部門売上金額93
                            ,bumon_cd_94                    -- 部門コード94
                            ,bumon_uriage_kingaku_94                    -- 部門売上金額94
                            ,bumon_cd_95                    -- 部門コード95
                            ,bumon_uriage_kingaku_95                    -- 部門売上金額95
                            ,bumon_cd_96                    -- 部門コード96
                            ,bumon_uriage_kingaku_96                    -- 部門売上金額96
                            ,bumon_cd_97                    -- 部門コード97
                            ,bumon_uriage_kingaku_97                    -- 部門売上金額97
                            ,bumon_cd_98                    -- 部門コード98
                            ,bumon_uriage_kingaku_98                    -- 部門売上金額98
                            ,bumon_cd_99                    -- 部門コード99
                            ,bumon_uriage_kingaku_99                    -- 部門売上金額99
                            ,uriba_cd_01                    -- 売場コード1
                            ,uriba_uriage_kyakusuu_01                    -- 売場売上客数1
                            ,uriba_uriage_tensuu_01                    -- 売場売上点数1
                            ,uriba_uriage_kingaku_01                    -- 売場売上金額1
                            ,uriba_cd_02                    -- 売場コード2
                            ,uriba_uriage_kyakusuu_02                    -- 売場売上客数2
                            ,uriba_uriage_tensuu_02                    -- 売場売上点数2
                            ,uriba_uriage_kingaku_02                    -- 売場売上金額2
                            ,uriba_cd_03                    -- 売場コード3
                            ,uriba_uriage_kyakusuu_03                    -- 売場売上客数3
                            ,uriba_uriage_tensuu_03                    -- 売場売上点数3
                            ,uriba_uriage_kingaku_03                    -- 売場売上金額3
                            ,uriba_cd_04                    -- 売場コード4
                            ,uriba_uriage_kyakusuu_04                    -- 売場売上客数4
                            ,uriba_uriage_tensuu_04                    -- 売場売上点数4
                            ,uriba_uriage_kingaku_04                    -- 売場売上金額4
                            ,uriba_cd_05                    -- 売場コード5
                            ,uriba_uriage_kyakusuu_05                    -- 売場売上客数5
                            ,uriba_uriage_tensuu_05                    -- 売場売上点数5
                            ,uriba_uriage_kingaku_05                    -- 売場売上金額5
                            ,uriba_cd_06                    -- 売場コード6
                            ,uriba_uriage_kyakusuu_06                    -- 売場売上客数6
                            ,uriba_uriage_tensuu_06                    -- 売場売上点数6
                            ,uriba_uriage_kingaku_06                    -- 売場売上金額6
                            ,uriba_cd_07                    -- 売場コード7
                            ,uriba_uriage_kyakusuu_07                    -- 売場売上客数7
                            ,uriba_uriage_tensuu_07                    -- 売場売上点数7
                            ,uriba_uriage_kingaku_07                    -- 売場売上金額7
                            ,uriba_cd_08                    -- 売場コード8
                            ,uriba_uriage_kyakusuu_08                    -- 売場売上客数8
                            ,uriba_uriage_tensuu_08                    -- 売場売上点数8
                            ,uriba_uriage_kingaku_08                    -- 売場売上金額8
                            ,uriba_cd_09                    -- 売場コード9
                            ,uriba_uriage_kyakusuu_09                    -- 売場売上客数9
                            ,uriba_uriage_tensuu_09                    -- 売場売上点数9
                            ,uriba_uriage_kingaku_09                    -- 売場売上金額9
                            ,uriba_cd_10                    -- 売場コード10
                            ,uriba_uriage_kyakusuu_10                    -- 売場売上客数10
                            ,uriba_uriage_tensuu_10                    -- 売場売上点数10
                            ,uriba_uriage_kingaku_10                    -- 売場売上金額10
                            ,uriba_cd_11                    -- 売場コード11
                            ,uriba_uriage_kyakusuu_11                    -- 売場売上客数11
                            ,uriba_uriage_tensuu_11                    -- 売場売上点数11
                            ,uriba_uriage_kingaku_11                    -- 売場売上金額11
                            ,uriba_cd_12                    -- 売場コード12
                            ,uriba_uriage_kyakusuu_12                    -- 売場売上客数12
                            ,uriba_uriage_tensuu_12                    -- 売場売上点数12
                            ,uriba_uriage_kingaku_12                    -- 売場売上金額12
                            ,uriba_cd_13                    -- 売場コード13
                            ,uriba_uriage_kyakusuu_13                    -- 売場売上客数13
                            ,uriba_uriage_tensuu_13                    -- 売場売上点数13
                            ,uriba_uriage_kingaku_13                    -- 売場売上金額13
                            ,uriba_cd_14                    -- 売場コード14
                            ,uriba_uriage_kyakusuu_14                    -- 売場売上客数14
                            ,uriba_uriage_tensuu_14                    -- 売場売上点数14
                            ,uriba_uriage_kingaku_14                    -- 売場売上金額14
                            ,uriba_cd_15                    -- 売場コード15
                            ,uriba_uriage_kyakusuu_15                    -- 売場売上客数15
                            ,uriba_uriage_tensuu_15                    -- 売場売上点数15
                            ,uriba_uriage_kingaku_15                    -- 売場売上金額15
                            ,uriba_cd_16                    -- 売場コード16
                            ,uriba_uriage_kyakusuu_16                    -- 売場売上客数16
                            ,uriba_uriage_tensuu_16                    -- 売場売上点数16
                            ,uriba_uriage_kingaku_16                    -- 売場売上金額16
                            ,uriba_cd_17                    -- 売場コード17
                            ,uriba_uriage_kyakusuu_17                    -- 売場売上客数17
                            ,uriba_uriage_tensuu_17                    -- 売場売上点数17
                            ,uriba_uriage_kingaku_17                    -- 売場売上金額17
                            ,uriba_cd_18                    -- 売場コード18
                            ,uriba_uriage_kyakusuu_18                    -- 売場売上客数18
                            ,uriba_uriage_tensuu_18                    -- 売場売上点数18
                            ,uriba_uriage_kingaku_18                    -- 売場売上金額18
                            ,uriba_cd_19                    -- 売場コード19
                            ,uriba_uriage_kyakusuu_19                    -- 売場売上客数19
                            ,uriba_uriage_tensuu_19                    -- 売場売上点数19
                            ,uriba_uriage_kingaku_19                    -- 売場売上金額19
                            ,uriba_cd_20                    -- 売場コード20
                            ,uriba_uriage_kyakusuu_20                    -- 売場売上客数20
                            ,uriba_uriage_tensuu_20                    -- 売場売上点数20
                            ,uriba_uriage_kingaku_20                    -- 売場売上金額20
                            ,uriba_cd_21                    -- 売場コード21
                            ,uriba_uriage_kyakusuu_21                    -- 売場売上客数21
                            ,uriba_uriage_tensuu_21                    -- 売場売上点数21
                            ,uriba_uriage_kingaku_21                    -- 売場売上金額21
                            ,uriba_cd_22                    -- 売場コード22
                            ,uriba_uriage_kyakusuu_22                    -- 売場売上客数22
                            ,uriba_uriage_tensuu_22                    -- 売場売上点数22
                            ,uriba_uriage_kingaku_22                    -- 売場売上金額22
                            ,uriba_cd_23                    -- 売場コード23
                            ,uriba_uriage_kyakusuu_23                    -- 売場売上客数23
                            ,uriba_uriage_tensuu_23                    -- 売場売上点数23
                            ,uriba_uriage_kingaku_23                    -- 売場売上金額23
                            ,uriba_cd_24                    -- 売場コード24
                            ,uriba_uriage_kyakusuu_24                    -- 売場売上客数24
                            ,uriba_uriage_tensuu_24                    -- 売場売上点数24
                            ,uriba_uriage_kingaku_24                    -- 売場売上金額24
                            ,uriba_cd_25                    -- 売場コード25
                            ,uriba_uriage_kyakusuu_25                    -- 売場売上客数25
                            ,uriba_uriage_tensuu_25                    -- 売場売上点数25
                            ,uriba_uriage_kingaku_25                    -- 売場売上金額25
                            ,uriba_cd_26                    -- 売場コード26
                            ,uriba_uriage_kyakusuu_26                    -- 売場売上客数26
                            ,uriba_uriage_tensuu_26                    -- 売場売上点数26
                            ,uriba_uriage_kingaku_26                    -- 売場売上金額26
                            ,uriba_cd_27                    -- 売場コード27
                            ,uriba_uriage_kyakusuu_27                    -- 売場売上客数27
                            ,uriba_uriage_tensuu_27                    -- 売場売上点数27
                            ,uriba_uriage_kingaku_27                    -- 売場売上金額27
                            ,uriba_cd_28                    -- 売場コード28
                            ,uriba_uriage_kyakusuu_28                    -- 売場売上客数28
                            ,uriba_uriage_tensuu_28                    -- 売場売上点数28
                            ,uriba_uriage_kingaku_28                    -- 売場売上金額28
                            ,uriba_cd_29                    -- 売場コード29
                            ,uriba_uriage_kyakusuu_29                    -- 売場売上客数29
                            ,uriba_uriage_tensuu_29                    -- 売場売上点数29
                            ,uriba_uriage_kingaku_29                    -- 売場売上金額29
                            ,uriba_cd_30                    -- 売場コード30
                            ,uriba_uriage_kyakusuu_30                    -- 売場売上客数30
                            ,uriba_uriage_tensuu_30                    -- 売場売上点数30
                            ,uriba_uriage_kingaku_30                    -- 売場売上金額30
                            ,genkinkei_kaisuu                    -- 現金計（回数）
                            ,genkinkei_kingaku                    -- 現金計（金額）
                            ,kaisuu_01                    -- 回数1(釣銭機ｴﾗｰ金入力用)
                            ,kingaku_01                    -- 金額1(釣銭機ｴﾗｰ金入力用)
                            ,kaisuu_02                    -- 回数2(その他掛計)
                            ,kingaku_02                    -- 金額2(その他掛計)
                            ,kaisuu_03                    -- 回数3(ビバ商品券)
                            ,kingaku_03                    -- 金額3(ビバ商品券)
                            ,kaisuu_04                    -- 回数4(ビバお買物券)
                            ,kingaku_04                    -- 金額4(ビバお買物券)
                            ,kaisuu_05                    -- 回数5(ＪＣＢギフト券)
                            ,kingaku_05                    -- 金額5(ＪＣＢギフト券)
                            ,kaisuu_06                    -- 回数6
                            ,kingaku_06                    -- 金額6
                            ,kaisuu_07                    -- 回数7
                            ,kingaku_07                    -- 金額7
                            ,kaisuu_08                    -- 回数8
                            ,kingaku_08                    -- 金額8
                            ,kaisuu_09                    -- 回数9
                            ,kingaku_09                    -- 金額9
                            ,kaisuu_10                    -- 回数10
                            ,kingaku_10                    -- 金額10
                            ,kaisuu_11                    -- 回数11
                            ,kingaku_11                    -- 金額11
                            ,kaisuu_12                    -- 回数12
                            ,kingaku_12                    -- 金額12
                            ,kaisuu_13                    -- 回数13
                            ,kingaku_13                    -- 金額13
                            ,kaisuu_14                    -- 回数14
                            ,kingaku_14                    -- 金額14
                            ,kaisuu_15                    -- 回数15
                            ,kingaku_15                    -- 金額15
                            ,kaisuu_16                    -- 回数16
                            ,kingaku_16                    -- 金額16
                            ,kaisuu_17                    -- 回数17（クラブ買物券）
                            ,kingaku_17                    -- 金額17（クラブ買物券）
                            ,kaisuu_credit                     -- 回数（クレジット）
                            ,kingaku_credit                    -- 金額（クレジット
                            ,kaisuu_debit                      -- 回数（デビット）
                            ,kingaku_debit                     -- 金額（デビット）
                            ,genkin_aridaka                    -- 現金在高
                            ,genkin_gaikei                     -- 現金外計
                            ,sotozei_kei                       -- 外税計
                            ,utizei_kei                        -- 内税計
                            ,zei_goukei                        -- 税合計
                            ,sou_uriage_tensuu                 -- 総売上点数
                            ,sou_uriage_kingaku                -- 総売上金額
                            ,sou_uri_kyakusuu                  -- 総売客数
                            ,nebiki_kingaku                    -- 値引金額
                            ,m_m_nebiki_kingaku                -- M&M値引金額
                            ,set_hanbai_nebiki_gaku            -- セット販売値引額
                            ,baihen_nebiki_gaku                -- 売変値引額
                            ,nebi_01                    -- 値引き1
                            ,nebi_02                    -- 値引き2
                            ,nebi_03                    -- 値引き3
                            ,nebi_04                    -- 値引き4
                            ,nebi_05                    -- 値引き5
                            ,ryousyuusyo_hakkou_kaisuu                    -- 領収書発行回数
                            ,ryousyuusyo_hakkou_kingaku                    -- 領収書発行金額
                            ,insizei_kaisuu_01                     -- 印紙税回数1
                            ,insizei_kingaku_01                    -- 印紙税金額1
                            ,insizei_kaisuu_02                     -- 印紙税回数2
                            ,insizei_kingaku_02                    -- 印紙税金額2
                            ,insizei_kaisuu_03                     -- 印紙税回数3
                            ,insizei_kingaku_03                    -- 印紙税金額3
                            ,insizei_kaisuu_04                     -- 印紙税回数4
                            ,insizei_kingaku_04                    -- 印紙税金額4
                            ,insizei_kaisuu_05                     -- 印紙税回数5
                            ,insizei_kingaku_05                    -- 印紙税金額5
                            ,insizei_kaisuu                        -- 印紙税回数
                            ,nyuukin_kaisuu                        -- 入金回数
                            ,nyuukin_kingaku                       -- 入金金額
                            ,siharai_kaisuu                        -- 支払回数
                            ,siharai_kingaku                       -- 支払金額
                            ,kaisyuu_kaisuu                        -- 回収回数
                            ,kaisyuu_kingaku                       -- 回収金額
                            ,turisen_jyunbikin_kaisuu              -- 釣銭準備金回数
                            ,turisen_jyunbikin_kingaku             -- 釣銭準備金金額
                            ,syouhinken_turi_kaisuu                -- 商品券釣回数
                            ,syouhinken_turi_kingaku               -- 商品券釣金額
                            ,media_turi_kaisuu                     -- メディア釣回数
                            ,media_turi_kingaku                    -- メディア釣金額
                            ,temoti_aridaka_kingaku                -- 手持在高金額
                            ,manken_maisuu                   -- 万券枚数
                            ,touroku_user                    -- 登録者
                            ,touroku_date                    -- 登録日時
                            ,kousin_user                     -- 更新者
                            ,kousin_date                     -- 更新日時
                            )
SELECT     replace(uriage_date,'/','')            -- 売上日付
          ,RIGHT(tenpo_cd,4) AS tenpo_cd            -- 店コード
          ,regi_no            -- レジＮｏ．
          ,daibunrui_cd01            -- 大分類コード01
          ,CAST(daibunrui_uriage_kingaku01 AS numeric(9)) AS daibunrui_uriage_kingaku01    -- 大分類売上金額01
          ,daibunrui_cd02            -- 大分類コード02
          ,CAST(daibunrui_uriage_kingaku02 AS numeric(9)) AS daibunrui_uriage_kingaku02    -- 大分類売上金額02
          ,daibunrui_cd03            -- 大分類コード03
          ,CAST(daibunrui_uriage_kingaku03 AS numeric(9)) AS daibunrui_uriage_kingaku03    -- 大分類売上金額03
          ,daibunrui_cd04            -- 大分類コード04
          ,CAST(daibunrui_uriage_kingaku04 AS numeric(9)) AS daibunrui_uriage_kingaku04    -- 大分類売上金額04
          ,daibunrui_cd05            -- 大分類コード05
          ,CAST(daibunrui_uriage_kingaku05 AS numeric(9)) AS daibunrui_uriage_kingaku05    -- 大分類売上金額05
          ,daibunrui_cd06            -- 大分類コード06
          ,CAST(daibunrui_uriage_kingaku06 AS numeric(9)) AS daibunrui_uriage_kingaku06    -- 大分類売上金額06
          ,daibunrui_cd07            -- 大分類コード07
          ,CAST(daibunrui_uriage_kingaku07 AS numeric(9)) AS daibunrui_uriage_kingaku07    -- 大分類売上金額07
          ,daibunrui_cd08            -- 大分類コード08
          ,CAST(daibunrui_uriage_kingaku08 AS numeric(9)) AS daibunrui_uriage_kingaku08    -- 大分類売上金額08
          ,daibunrui_cd09            -- 大分類コード09
          ,CAST(daibunrui_uriage_kingaku09 AS numeric(9)) AS daibunrui_uriage_kingaku09    -- 大分類売上金額09
          ,daibunrui_cd10            -- 大分類コード10
          ,CAST(daibunrui_uriage_kingaku10 AS numeric(9)) AS daibunrui_uriage_kingaku10    -- 大分類売上金額10
          ,daibunrui_cd11            -- 大分類コード11
          ,CAST(daibunrui_uriage_kingaku11 AS numeric(9)) AS daibunrui_uriage_kingaku11    -- 大分類売上金額11
          ,daibunrui_cd12            -- 大分類コード12
          ,CAST(daibunrui_uriage_kingaku12 AS numeric(9)) AS daibunrui_uriage_kingaku12    -- 大分類売上金額12
          ,daibunrui_cd13            -- 大分類コード13
          ,CAST(daibunrui_uriage_kingaku13 AS numeric(9)) AS daibunrui_uriage_kingaku13    -- 大分類売上金額13
          ,daibunrui_cd14            -- 大分類コード14
          ,CAST(daibunrui_uriage_kingaku14 AS numeric(9)) AS daibunrui_uriage_kingaku14    -- 大分類売上金額14
          ,daibunrui_cd15            -- 大分類コード15
          ,CAST(daibunrui_uriage_kingaku15 AS numeric(9)) AS daibunrui_uriage_kingaku15    -- 大分類売上金額15
          ,daibunrui_cd16            -- 大分類コード16
          ,CAST(daibunrui_uriage_kingaku16 AS numeric(9)) AS daibunrui_uriage_kingaku16    -- 大分類売上金額16
          ,daibunrui_cd17            -- 大分類コード17
          ,CAST(daibunrui_uriage_kingaku17 AS numeric(9)) AS daibunrui_uriage_kingaku17    -- 大分類売上金額17
          ,daibunrui_cd18            -- 大分類コード18
          ,CAST(daibunrui_uriage_kingaku18 AS numeric(9)) AS daibunrui_uriage_kingaku18    -- 大分類売上金額18
          ,daibunrui_cd19            -- 大分類コード19
          ,CAST(daibunrui_uriage_kingaku19 AS numeric(9)) AS daibunrui_uriage_kingaku19    -- 大分類売上金額19
          ,daibunrui_cd20            -- 大分類コード20
          ,CAST(daibunrui_uriage_kingaku20 AS numeric(9)) AS daibunrui_uriage_kingaku20    -- 大分類売上金額20
          ,daibunrui_cd21            -- 大分類コード21
          ,CAST(daibunrui_uriage_kingaku21 AS numeric(9)) AS daibunrui_uriage_kingaku21    -- 大分類売上金額21
          ,daibunrui_cd22            -- 大分類コード22
          ,CAST(daibunrui_uriage_kingaku22 AS numeric(9)) AS daibunrui_uriage_kingaku22    -- 大分類売上金額22
          ,daibunrui_cd23            -- 大分類コード23
          ,CAST(daibunrui_uriage_kingaku23 AS numeric(9)) AS daibunrui_uriage_kingaku23    -- 大分類売上金額23
          ,daibunrui_cd24            -- 大分類コード24
          ,CAST(daibunrui_uriage_kingaku24 AS numeric(9)) AS daibunrui_uriage_kingaku24    -- 大分類売上金額24
          ,daibunrui_cd25            -- 大分類コード25
          ,CAST(daibunrui_uriage_kingaku25 AS numeric(9)) AS daibunrui_uriage_kingaku25    -- 大分類売上金額25
          ,daibunrui_cd26            -- 大分類コード26
          ,CAST(daibunrui_uriage_kingaku26 AS numeric(9)) AS daibunrui_uriage_kingaku26    -- 大分類売上金額26
          ,daibunrui_cd27            -- 大分類コード27
          ,CAST(daibunrui_uriage_kingaku27 AS numeric(9)) AS daibunrui_uriage_kingaku27    -- 大分類売上金額27
          ,daibunrui_cd28            -- 大分類コード28
          ,CAST(daibunrui_uriage_kingaku28 AS numeric(9)) AS daibunrui_uriage_kingaku28    -- 大分類売上金額28
          ,daibunrui_cd29            -- 大分類コード29
          ,CAST(daibunrui_uriage_kingaku29 AS numeric(9)) AS daibunrui_uriage_kingaku29    -- 大分類売上金額29
          ,daibunrui_cd30            -- 大分類コード30
          ,CAST(daibunrui_uriage_kingaku30 AS numeric(9)) AS daibunrui_uriage_kingaku30    -- 大分類売上金額30
          ,daibunrui_cd31            -- 大分類コード31
          ,CAST(daibunrui_uriage_kingaku31 AS numeric(9)) AS daibunrui_uriage_kingaku31    -- 大分類売上金額31
          ,daibunrui_cd32            -- 大分類コード32
          ,CAST(daibunrui_uriage_kingaku32 AS numeric(9)) AS daibunrui_uriage_kingaku32    -- 大分類売上金額32
          ,daibunrui_cd33            -- 大分類コード33
          ,CAST(daibunrui_uriage_kingaku33 AS numeric(9)) AS daibunrui_uriage_kingaku33    -- 大分類売上金額33
          ,daibunrui_cd34            -- 大分類コード34
          ,CAST(daibunrui_uriage_kingaku34 AS numeric(9)) AS daibunrui_uriage_kingaku34    -- 大分類売上金額34
          ,daibunrui_cd35            -- 大分類コード35
          ,CAST(daibunrui_uriage_kingaku35 AS numeric(9)) AS daibunrui_uriage_kingaku35    -- 大分類売上金額35
          ,daibunrui_cd36            -- 大分類コード36
          ,CAST(daibunrui_uriage_kingaku36 AS numeric(9)) AS daibunrui_uriage_kingaku36    -- 大分類売上金額36
          ,daibunrui_cd37            -- 大分類コード37
          ,CAST(daibunrui_uriage_kingaku37 AS numeric(9)) AS daibunrui_uriage_kingaku37    -- 大分類売上金額37
          ,daibunrui_cd38            -- 大分類コード38
          ,CAST(daibunrui_uriage_kingaku38 AS numeric(9)) AS daibunrui_uriage_kingaku38    -- 大分類売上金額38
          ,daibunrui_cd39            -- 大分類コード39
          ,CAST(daibunrui_uriage_kingaku39 AS numeric(9)) AS daibunrui_uriage_kingaku39    -- 大分類売上金額39
          ,daibunrui_cd40            -- 大分類コード40
          ,CAST(daibunrui_uriage_kingaku40 AS numeric(9)) AS daibunrui_uriage_kingaku40    -- 大分類売上金額40
          ,daibunrui_cd41            -- 大分類コード41
          ,CAST(daibunrui_uriage_kingaku41 AS numeric(9)) AS daibunrui_uriage_kingaku41    -- 大分類売上金額41
          ,daibunrui_cd42            -- 大分類コード42
          ,CAST(daibunrui_uriage_kingaku42 AS numeric(9)) AS daibunrui_uriage_kingaku42    -- 大分類売上金額42
          ,daibunrui_cd43            -- 大分類コード43
          ,CAST(daibunrui_uriage_kingaku43 AS numeric(9)) AS daibunrui_uriage_kingaku43    -- 大分類売上金額43
          ,daibunrui_cd44            -- 大分類コード44
          ,CAST(daibunrui_uriage_kingaku44 AS numeric(9)) AS daibunrui_uriage_kingaku44    -- 大分類売上金額44
          ,daibunrui_cd45            -- 大分類コード45
          ,CAST(daibunrui_uriage_kingaku45 AS numeric(9)) AS daibunrui_uriage_kingaku45    -- 大分類売上金額45
          ,daibunrui_cd46            -- 大分類コード46
          ,CAST(daibunrui_uriage_kingaku46 AS numeric(9)) AS daibunrui_uriage_kingaku46    -- 大分類売上金額46
          ,daibunrui_cd47            -- 大分類コード47
          ,CAST(daibunrui_uriage_kingaku47 AS numeric(9)) AS daibunrui_uriage_kingaku47    -- 大分類売上金額47
          ,daibunrui_cd48            -- 大分類コード48
          ,CAST(daibunrui_uriage_kingaku48 AS numeric(9)) AS daibunrui_uriage_kingaku48    -- 大分類売上金額48
          ,daibunrui_cd49            -- 大分類コード49
          ,CAST(daibunrui_uriage_kingaku49 AS numeric(9)) AS daibunrui_uriage_kingaku49    -- 大分類売上金額49
          ,daibunrui_cd50            -- 大分類コード50
          ,CAST(daibunrui_uriage_kingaku50 AS numeric(9)) AS daibunrui_uriage_kingaku50    -- 大分類売上金額50
          ,daibunrui_cd51            -- 大分類コード51
          ,CAST(daibunrui_uriage_kingaku51 AS numeric(9)) AS daibunrui_uriage_kingaku51    -- 大分類売上金額51
          ,daibunrui_cd52            -- 大分類コード52
          ,CAST(daibunrui_uriage_kingaku52 AS numeric(9)) AS daibunrui_uriage_kingaku52    -- 大分類売上金額52
          ,daibunrui_cd53            -- 大分類コード53
          ,CAST(daibunrui_uriage_kingaku53 AS numeric(9)) AS daibunrui_uriage_kingaku53    -- 大分類売上金額53
          ,daibunrui_cd54            -- 大分類コード54
          ,CAST(daibunrui_uriage_kingaku54 AS numeric(9)) AS daibunrui_uriage_kingaku54    -- 大分類売上金額54
          ,daibunrui_cd55            -- 大分類コード55
          ,CAST(daibunrui_uriage_kingaku55 AS numeric(9)) AS daibunrui_uriage_kingaku55    -- 大分類売上金額55
          ,daibunrui_cd56            -- 大分類コード56
          ,CAST(daibunrui_uriage_kingaku56 AS numeric(9)) AS daibunrui_uriage_kingaku56    -- 大分類売上金額56
          ,daibunrui_cd57            -- 大分類コード57
          ,CAST(daibunrui_uriage_kingaku57 AS numeric(9)) AS daibunrui_uriage_kingaku57    -- 大分類売上金額57
          ,daibunrui_cd58            -- 大分類コード58
          ,CAST(daibunrui_uriage_kingaku58 AS numeric(9)) AS daibunrui_uriage_kingaku58    -- 大分類売上金額58
          ,daibunrui_cd59            -- 大分類コード59
          ,CAST(daibunrui_uriage_kingaku59 AS numeric(9)) AS daibunrui_uriage_kingaku59    -- 大分類売上金額59
          ,daibunrui_cd60            -- 大分類コード60
          ,CAST(daibunrui_uriage_kingaku60 AS numeric(9)) AS daibunrui_uriage_kingaku60    -- 大分類売上金額60
          ,daibunrui_cd61            -- 大分類コード61
          ,CAST(daibunrui_uriage_kingaku61 AS numeric(9)) AS daibunrui_uriage_kingaku61    -- 大分類売上金額61
          ,daibunrui_cd62            -- 大分類コード62
          ,CAST(daibunrui_uriage_kingaku62 AS numeric(9)) AS daibunrui_uriage_kingaku62    -- 大分類売上金額62
          ,daibunrui_cd63            -- 大分類コード63
          ,CAST(daibunrui_uriage_kingaku63 AS numeric(9)) AS daibunrui_uriage_kingaku63    -- 大分類売上金額63
          ,daibunrui_cd64            -- 大分類コード64
          ,CAST(daibunrui_uriage_kingaku64 AS numeric(9)) AS daibunrui_uriage_kingaku64    -- 大分類売上金額64
          ,daibunrui_cd65            -- 大分類コード65
          ,CAST(daibunrui_uriage_kingaku65 AS numeric(9)) AS daibunrui_uriage_kingaku65    -- 大分類売上金額65
          ,daibunrui_cd66            -- 大分類コード66
          ,CAST(daibunrui_uriage_kingaku66 AS numeric(9)) AS daibunrui_uriage_kingaku66    -- 大分類売上金額66
          ,daibunrui_cd67            -- 大分類コード67
          ,CAST(daibunrui_uriage_kingaku67 AS numeric(9)) AS daibunrui_uriage_kingaku67    -- 大分類売上金額67
          ,daibunrui_cd68            -- 大分類コード68
          ,CAST(daibunrui_uriage_kingaku68 AS numeric(9)) AS daibunrui_uriage_kingaku68    -- 大分類売上金額68
          ,daibunrui_cd69            -- 大分類コード69
          ,CAST(daibunrui_uriage_kingaku69 AS numeric(9)) AS daibunrui_uriage_kingaku69    -- 大分類売上金額69
          ,daibunrui_cd70            -- 大分類コード70
          ,CAST(daibunrui_uriage_kingaku70 AS numeric(9)) AS daibunrui_uriage_kingaku70    -- 大分類売上金額70
          ,daibunrui_cd71            -- 大分類コード71
          ,CAST(daibunrui_uriage_kingaku71 AS numeric(9)) AS daibunrui_uriage_kingaku71    -- 大分類売上金額71
          ,daibunrui_cd72            -- 大分類コード72
          ,CAST(daibunrui_uriage_kingaku72 AS numeric(9)) AS daibunrui_uriage_kingaku72    -- 大分類売上金額72
          ,daibunrui_cd73            -- 大分類コード73
          ,CAST(daibunrui_uriage_kingaku73 AS numeric(9)) AS daibunrui_uriage_kingaku73    -- 大分類売上金額73
          ,daibunrui_cd74            -- 大分類コード74
          ,CAST(daibunrui_uriage_kingaku74 AS numeric(9)) AS daibunrui_uriage_kingaku74    -- 大分類売上金額74
          ,daibunrui_cd75            -- 大分類コード75
          ,CAST(daibunrui_uriage_kingaku75 AS numeric(9)) AS daibunrui_uriage_kingaku75    -- 大分類売上金額75
          ,daibunrui_cd76            -- 大分類コード76
          ,CAST(daibunrui_uriage_kingaku76 AS numeric(9)) AS daibunrui_uriage_kingaku76    -- 大分類売上金額76
          ,daibunrui_cd77            -- 大分類コード77
          ,CAST(daibunrui_uriage_kingaku77 AS numeric(9)) AS daibunrui_uriage_kingaku77    -- 大分類売上金額77
          ,daibunrui_cd78            -- 大分類コード78
          ,CAST(daibunrui_uriage_kingaku78 AS numeric(9)) AS daibunrui_uriage_kingaku78    -- 大分類売上金額78
          ,daibunrui_cd79            -- 大分類コード79
          ,CAST(daibunrui_uriage_kingaku79 AS numeric(9)) AS daibunrui_uriage_kingaku79    -- 大分類売上金額79
          ,daibunrui_cd80            -- 大分類コード80
          ,CAST(daibunrui_uriage_kingaku80 AS numeric(9)) AS daibunrui_uriage_kingaku80    -- 大分類売上金額80
          ,daibunrui_cd81            -- 大分類コード81
          ,CAST(daibunrui_uriage_kingaku81 AS numeric(9)) AS daibunrui_uriage_kingaku81    -- 大分類売上金額81
          ,daibunrui_cd82            -- 大分類コード82
          ,CAST(daibunrui_uriage_kingaku82 AS numeric(9)) AS daibunrui_uriage_kingaku82    -- 大分類売上金額82
          ,daibunrui_cd83            -- 大分類コード83
          ,CAST(daibunrui_uriage_kingaku83 AS numeric(9)) AS daibunrui_uriage_kingaku83    -- 大分類売上金額83
          ,daibunrui_cd84            -- 大分類コード84
          ,CAST(daibunrui_uriage_kingaku84 AS numeric(9)) AS daibunrui_uriage_kingaku84    -- 大分類売上金額84
          ,daibunrui_cd85            -- 大分類コード85
          ,CAST(daibunrui_uriage_kingaku85 AS numeric(9)) AS daibunrui_uriage_kingaku85    -- 大分類売上金額85
          ,daibunrui_cd86            -- 大分類コード86
          ,CAST(daibunrui_uriage_kingaku86 AS numeric(9)) AS daibunrui_uriage_kingaku86    -- 大分類売上金額86
          ,daibunrui_cd87            -- 大分類コード87
          ,CAST(daibunrui_uriage_kingaku87 AS numeric(9)) AS daibunrui_uriage_kingaku87    -- 大分類売上金額87
          ,daibunrui_cd88            -- 大分類コード88
          ,CAST(daibunrui_uriage_kingaku88 AS numeric(9)) AS daibunrui_uriage_kingaku88    -- 大分類売上金額88
          ,daibunrui_cd89            -- 大分類コード89
          ,CAST(daibunrui_uriage_kingaku89 AS numeric(9)) AS daibunrui_uriage_kingaku89    -- 大分類売上金額89
          ,daibunrui_cd90            -- 大分類コード90
          ,CAST(daibunrui_uriage_kingaku90 AS numeric(9)) AS daibunrui_uriage_kingaku90    -- 大分類売上金額90
          ,daibunrui_cd91            -- 大分類コード91
          ,CAST(daibunrui_uriage_kingaku91 AS numeric(9)) AS daibunrui_uriage_kingaku91    -- 大分類売上金額91
          ,daibunrui_cd92            -- 大分類コード92
          ,CAST(daibunrui_uriage_kingaku92 AS numeric(9)) AS daibunrui_uriage_kingaku92    -- 大分類売上金額92
          ,daibunrui_cd93            -- 大分類コード93
          ,CAST(daibunrui_uriage_kingaku93 AS numeric(9)) AS daibunrui_uriage_kingaku93    -- 大分類売上金額93
          ,daibunrui_cd94            -- 大分類コード94
          ,CAST(daibunrui_uriage_kingaku94 AS numeric(9)) AS daibunrui_uriage_kingaku94    -- 大分類売上金額94
          ,daibunrui_cd95            -- 大分類コード95
          ,CAST(daibunrui_uriage_kingaku95 AS numeric(9)) AS daibunrui_uriage_kingaku95    -- 大分類売上金額95
          ,daibunrui_cd96            -- 大分類コード96
          ,CAST(daibunrui_uriage_kingaku96 AS numeric(9)) AS daibunrui_uriage_kingaku96    -- 大分類売上金額96
          ,daibunrui_cd97            -- 大分類コード97
          ,CAST(daibunrui_uriage_kingaku97 AS numeric(9)) AS daibunrui_uriage_kingaku97    -- 大分類売上金額97
          ,daibunrui_cd98            -- 大分類コード98
          ,CAST(daibunrui_uriage_kingaku98 AS numeric(9)) AS daibunrui_uriage_kingaku98    -- 大分類売上金額98
          ,daibunrui_cd99            -- 大分類コード99
          ,CAST(daibunrui_uriage_kingaku99 AS numeric(9)) AS daibunrui_uriage_kingaku99    -- 大分類売上金額99
          ,uriba_tani_gp_cd01            -- 売場単位GPコード01
          ,CAST(uriba_tani_uriage_kyakusuu01 AS numeric(9)) AS uriba_tani_uriage_kyakusuu01    -- 売場単位売上客数01
          ,CAST(uriba_tani_uriage_tensuu01 AS numeric(9)) AS uriba_tani_uriage_tensuu01    -- 売場単位売上点数01
          ,CAST(uriba_tani_uriage_kingaku01 AS numeric(9)) AS uriba_tani_uriage_kingaku01    -- 売場単位売上金額01
          ,uriba_tani_gp_cd02            -- 売場単位GPコード02
          ,CAST(uriba_tani_uriage_kyakusuu02 AS numeric(9)) AS uriba_tani_uriage_kyakusuu02    -- 売場単位売上客数02
          ,CAST(uriba_tani_uriage_tensuu02 AS numeric(9)) AS uriba_tani_uriage_tensuu02    -- 売場単位売上点数02
          ,CAST(uriba_tani_uriage_kingaku02 AS numeric(9)) AS uriba_tani_uriage_kingaku02    -- 売場単位売上金額02
          ,uriba_tani_gp_cd03            -- 売場単位GPコード03
          ,CAST(uriba_tani_uriage_kyakusuu03 AS numeric(9)) AS uriba_tani_uriage_kyakusuu03    -- 売場単位売上客数03
          ,CAST(uriba_tani_uriage_tensuu03 AS numeric(9)) AS uriba_tani_uriage_tensuu03    -- 売場単位売上点数03
          ,CAST(uriba_tani_uriage_kingaku03 AS numeric(9)) AS uriba_tani_uriage_kingaku03    -- 売場単位売上金額03
          ,uriba_tani_gp_cd04            -- 売場単位GPコード04
          ,CAST(uriba_tani_uriage_kyakusuu04 AS numeric(9)) AS uriba_tani_uriage_kyakusuu04    -- 売場単位売上客数04
          ,CAST(uriba_tani_uriage_tensuu04 AS numeric(9)) AS uriba_tani_uriage_tensuu04    -- 売場単位売上点数04
          ,CAST(uriba_tani_uriage_kingaku04 AS numeric(9)) AS uriba_tani_uriage_kingaku04    -- 売場単位売上金額04
          ,uriba_tani_gp_cd05            -- 売場単位GPコード05
          ,CAST(uriba_tani_uriage_kyakusuu05 AS numeric(9)) AS uriba_tani_uriage_kyakusuu05    -- 売場単位売上客数05
          ,CAST(uriba_tani_uriage_tensuu05 AS numeric(9)) AS uriba_tani_uriage_tensuu05    -- 売場単位売上点数05
          ,CAST(uriba_tani_uriage_kingaku05 AS numeric(9)) AS uriba_tani_uriage_kingaku05    -- 売場単位売上金額05
          ,uriba_tani_gp_cd06            -- 売場単位GPコード06
          ,CAST(uriba_tani_uriage_kyakusuu06 AS numeric(9)) AS uriba_tani_uriage_kyakusuu06    -- 売場単位売上客数06
          ,CAST(uriba_tani_uriage_tensuu06 AS numeric(9)) AS uriba_tani_uriage_tensuu06    -- 売場単位売上点数06
          ,CAST(uriba_tani_uriage_kingaku06 AS numeric(9)) AS uriba_tani_uriage_kingaku06    -- 売場単位売上金額06
          ,uriba_tani_gp_cd07            -- 売場単位GPコード07
          ,CAST(uriba_tani_uriage_kyakusuu07 AS numeric(9)) AS uriba_tani_uriage_kyakusuu07    -- 売場単位売上客数07
          ,CAST(uriba_tani_uriage_tensuu07 AS numeric(9)) AS uriba_tani_uriage_tensuu07    -- 売場単位売上点数07
          ,CAST(uriba_tani_uriage_kingaku07 AS numeric(9)) AS uriba_tani_uriage_kingaku07    -- 売場単位売上金額07
          ,uriba_tani_gp_cd08            -- 売場単位GPコード08
          ,CAST(uriba_tani_uriage_kyakusuu08 AS numeric(9)) AS uriba_tani_uriage_kyakusuu08    -- 売場単位売上客数08
          ,CAST(uriba_tani_uriage_tensuu08 AS numeric(9)) AS uriba_tani_uriage_tensuu08    -- 売場単位売上点数08
          ,CAST(uriba_tani_uriage_kingaku08 AS numeric(9)) AS uriba_tani_uriage_kingaku08    -- 売場単位売上金額08
          ,uriba_tani_gp_cd09            -- 売場単位GPコード09
          ,CAST(uriba_tani_uriage_kyakusuu09 AS numeric(9)) AS uriba_tani_uriage_kyakusuu09    -- 売場単位売上客数09
          ,CAST(uriba_tani_uriage_tensuu09 AS numeric(9)) AS uriba_tani_uriage_tensuu09    -- 売場単位売上点数09
          ,CAST(uriba_tani_uriage_kingaku09 AS numeric(9)) AS uriba_tani_uriage_kingaku09    -- 売場単位売上金額09
          ,uriba_tani_gp_cd10            -- 売場単位GPコード10
          ,CAST(uriba_tani_uriage_kyakusuu10 AS numeric(9)) AS uriba_tani_uriage_kyakusuu10    -- 売場単位売上客数10
          ,CAST(uriba_tani_uriage_tensuu10 AS numeric(9)) AS uriba_tani_uriage_tensuu10    -- 売場単位売上点数10
          ,CAST(uriba_tani_uriage_kingaku10 AS numeric(9)) AS uriba_tani_uriage_kingaku10    -- 売場単位売上金額10
          ,uriba_tani_gp_cd11            -- 売場単位GPコード11
          ,CAST(uriba_tani_uriage_kyakusuu11 AS numeric(9)) AS uriba_tani_uriage_kyakusuu11    -- 売場単位売上客数11
          ,CAST(uriba_tani_uriage_tensuu11 AS numeric(9)) AS uriba_tani_uriage_tensuu11    -- 売場単位売上点数11
          ,CAST(uriba_tani_uriage_kingaku11 AS numeric(9)) AS uriba_tani_uriage_kingaku11    -- 売場単位売上金額11
          ,uriba_tani_gp_cd12            -- 売場単位GPコード12
          ,CAST(uriba_tani_uriage_kyakusuu12 AS numeric(9)) AS uriba_tani_uriage_kyakusuu12    -- 売場単位売上客数12
          ,CAST(uriba_tani_uriage_tensuu12 AS numeric(9)) AS uriba_tani_uriage_tensuu12    -- 売場単位売上点数12
          ,CAST(uriba_tani_uriage_kingaku12 AS numeric(9)) AS uriba_tani_uriage_kingaku12    -- 売場単位売上金額12
          ,uriba_tani_gp_cd13            -- 売場単位GPコード13
          ,CAST(uriba_tani_uriage_kyakusuu13 AS numeric(9)) AS uriba_tani_uriage_kyakusuu13    -- 売場単位売上客数13
          ,CAST(uriba_tani_uriage_tensuu13 AS numeric(9)) AS uriba_tani_uriage_tensuu13    -- 売場単位売上点数13
          ,CAST(uriba_tani_uriage_kingaku13 AS numeric(9)) AS uriba_tani_uriage_kingaku13    -- 売場単位売上金額13
          ,uriba_tani_gp_cd14            -- 売場単位GPコード14
          ,CAST(uriba_tani_uriage_kyakusuu14 AS numeric(9)) AS uriba_tani_uriage_kyakusuu14    -- 売場単位売上客数14
          ,CAST(uriba_tani_uriage_tensuu14 AS numeric(9)) AS uriba_tani_uriage_tensuu14    -- 売場単位売上点数14
          ,CAST(uriba_tani_uriage_kingaku14 AS numeric(9)) AS uriba_tani_uriage_kingaku14    -- 売場単位売上金額14
          ,uriba_tani_gp_cd15            -- 売場単位GPコード15
          ,CAST(uriba_tani_uriage_kyakusuu15 AS numeric(9)) AS uriba_tani_uriage_kyakusuu15    -- 売場単位売上客数15
          ,CAST(uriba_tani_uriage_tensuu15 AS numeric(9)) AS uriba_tani_uriage_tensuu15    -- 売場単位売上点数15
          ,CAST(uriba_tani_uriage_kingaku15 AS numeric(9)) AS uriba_tani_uriage_kingaku15    -- 売場単位売上金額15
          ,uriba_tani_gp_cd16            -- 売場単位GPコード16
          ,CAST(uriba_tani_uriage_kyakusuu16 AS numeric(9)) AS uriba_tani_uriage_kyakusuu16    -- 売場単位売上客数16
          ,CAST(uriba_tani_uriage_tensuu16 AS numeric(9)) AS uriba_tani_uriage_tensuu16    -- 売場単位売上点数16
          ,CAST(uriba_tani_uriage_kingaku16 AS numeric(9)) AS uriba_tani_uriage_kingaku16    -- 売場単位売上金額16
          ,uriba_tani_gp_cd17            -- 売場単位GPコード17
          ,CAST(uriba_tani_uriage_kyakusuu17 AS numeric(9)) AS uriba_tani_uriage_kyakusuu17    -- 売場単位売上客数17
          ,CAST(uriba_tani_uriage_tensuu17 AS numeric(9)) AS uriba_tani_uriage_tensuu17    -- 売場単位売上点数17
          ,CAST(uriba_tani_uriage_kingaku17 AS numeric(9)) AS uriba_tani_uriage_kingaku17    -- 売場単位売上金額17
          ,uriba_tani_gp_cd18            -- 売場単位GPコード18
          ,CAST(uriba_tani_uriage_kyakusuu18 AS numeric(9)) AS uriba_tani_uriage_kyakusuu18    -- 売場単位売上客数18
          ,CAST(uriba_tani_uriage_tensuu18 AS numeric(9)) AS uriba_tani_uriage_tensuu18    -- 売場単位売上点数18
          ,CAST(uriba_tani_uriage_kingaku18 AS numeric(9)) AS uriba_tani_uriage_kingaku18    -- 売場単位売上金額18
          ,uriba_tani_gp_cd19            -- 売場単位GPコード19
          ,CAST(uriba_tani_uriage_kyakusuu19 AS numeric(9)) AS uriba_tani_uriage_kyakusuu19    -- 売場単位売上客数19
          ,CAST(uriba_tani_uriage_tensuu19 AS numeric(9)) AS uriba_tani_uriage_tensuu19    -- 売場単位売上点数19
          ,CAST(uriba_tani_uriage_kingaku19 AS numeric(9)) AS uriba_tani_uriage_kingaku19    -- 売場単位売上金額19
          ,uriba_tani_gp_cd20            -- 売場単位GPコード20
          ,CAST(uriba_tani_uriage_kyakusuu20 AS numeric(9)) AS uriba_tani_uriage_kyakusuu20    -- 売場単位売上客数20
          ,CAST(uriba_tani_uriage_tensuu20 AS numeric(9)) AS uriba_tani_uriage_tensuu20    -- 売場単位売上点数20
          ,CAST(uriba_tani_uriage_kingaku20 AS numeric(9)) AS uriba_tani_uriage_kingaku20    -- 売場単位売上金額20
          ,uriba_tani_gp_cd21            -- 売場単位GPコード21
          ,CAST(uriba_tani_uriage_kyakusuu21 AS numeric(9)) AS uriba_tani_uriage_kyakusuu21    -- 売場単位売上客数21
          ,CAST(uriba_tani_uriage_tensuu21 AS numeric(9)) AS uriba_tani_uriage_tensuu21    -- 売場単位売上点数21
          ,CAST(uriba_tani_uriage_kingaku21 AS numeric(9)) AS uriba_tani_uriage_kingaku21    -- 売場単位売上金額21
          ,uriba_tani_gp_cd22            -- 売場単位GPコード22
          ,CAST(uriba_tani_uriage_kyakusuu22 AS numeric(9)) AS uriba_tani_uriage_kyakusuu22    -- 売場単位売上客数22
          ,CAST(uriba_tani_uriage_tensuu22 AS numeric(9)) AS uriba_tani_uriage_tensuu22    -- 売場単位売上点数22
          ,CAST(uriba_tani_uriage_kingaku22 AS numeric(9)) AS uriba_tani_uriage_kingaku22    -- 売場単位売上金額22
          ,uriba_tani_gp_cd23            -- 売場単位GPコード23
          ,CAST(uriba_tani_uriage_kyakusuu23 AS numeric(9)) AS uriba_tani_uriage_kyakusuu23    -- 売場単位売上客数23
          ,CAST(uriba_tani_uriage_tensuu23 AS numeric(9)) AS uriba_tani_uriage_tensuu23    -- 売場単位売上点数23
          ,CAST(uriba_tani_uriage_kingaku23 AS numeric(9)) AS uriba_tani_uriage_kingaku23    -- 売場単位売上金額23
          ,uriba_tani_gp_cd24            -- 売場単位GPコード24
          ,CAST(uriba_tani_uriage_kyakusuu24 AS numeric(9)) AS uriba_tani_uriage_kyakusuu24    -- 売場単位売上客数24
          ,CAST(uriba_tani_uriage_tensuu24 AS numeric(9)) AS uriba_tani_uriage_tensuu24    -- 売場単位売上点数24
          ,CAST(uriba_tani_uriage_kingaku24 AS numeric(9)) AS uriba_tani_uriage_kingaku24    -- 売場単位売上金額24
          ,uriba_tani_gp_cd25            -- 売場単位GPコード25
          ,CAST(uriba_tani_uriage_kyakusuu25 AS numeric(9)) AS uriba_tani_uriage_kyakusuu25    -- 売場単位売上客数25
          ,CAST(uriba_tani_uriage_tensuu25 AS numeric(9)) AS uriba_tani_uriage_tensuu25    -- 売場単位売上点数25
          ,CAST(uriba_tani_uriage_kingaku25 AS numeric(9)) AS uriba_tani_uriage_kingaku25    -- 売場単位売上金額25
          ,uriba_tani_gp_cd26            -- 売場単位GPコード26
          ,CAST(uriba_tani_uriage_kyakusuu26 AS numeric(9)) AS uriba_tani_uriage_kyakusuu26    -- 売場単位売上客数26
          ,CAST(uriba_tani_uriage_tensuu26 AS numeric(9)) AS uriba_tani_uriage_tensuu26    -- 売場単位売上点数26
          ,CAST(uriba_tani_uriage_kingaku26 AS numeric(9)) AS uriba_tani_uriage_kingaku26    -- 売場単位売上金額26
          ,uriba_tani_gp_cd27            -- 売場単位GPコード27
          ,CAST(uriba_tani_uriage_kyakusuu27 AS numeric(9)) AS uriba_tani_uriage_kyakusuu27    -- 売場単位売上客数27
          ,CAST(uriba_tani_uriage_tensuu27 AS numeric(9)) AS uriba_tani_uriage_tensuu27    -- 売場単位売上点数27
          ,CAST(uriba_tani_uriage_kingaku27 AS numeric(9)) AS uriba_tani_uriage_kingaku27    -- 売場単位売上金額27
          ,uriba_tani_gp_cd28            -- 売場単位GPコード28
          ,CAST(uriba_tani_uriage_kyakusuu28 AS numeric(9)) AS uriba_tani_uriage_kyakusuu28    -- 売場単位売上客数28
          ,CAST(uriba_tani_uriage_tensuu28 AS numeric(9)) AS uriba_tani_uriage_tensuu28    -- 売場単位売上点数28
          ,CAST(uriba_tani_uriage_kingaku28 AS numeric(9)) AS uriba_tani_uriage_kingaku28    -- 売場単位売上金額28
          ,uriba_tani_gp_cd29            -- 売場単位GPコード29
          ,CAST(uriba_tani_uriage_kyakusuu29 AS numeric(9)) AS uriba_tani_uriage_kyakusuu29    -- 売場単位売上客数29
          ,CAST(uriba_tani_uriage_tensuu29 AS numeric(9)) AS uriba_tani_uriage_tensuu29    -- 売場単位売上点数29
          ,CAST(uriba_tani_uriage_kingaku29 AS numeric(9)) AS uriba_tani_uriage_kingaku29    -- 売場単位売上金額29
          ,uriba_tani_gp_cd30            -- 売場単位GPコード30
          ,CAST(uriba_tani_uriage_kyakusuu30 AS numeric(9)) AS uriba_tani_uriage_kyakusuu30    -- 売場単位売上客数30
          ,CAST(uriba_tani_uriage_tensuu30 AS numeric(9)) AS uriba_tani_uriage_tensuu30    -- 売場単位売上点数30
          ,CAST(uriba_tani_uriage_kingaku30 AS numeric(9)) AS uriba_tani_uriage_kingaku30    -- 売場単位売上金額30
          ,CAST(kinsyubetu_kei_kaisu01 AS numeric(9)) AS kinsyubetu_kei_kaisu01    -- 金種別計_回数01
          ,CAST(kinsyubetu_kei_kingaku01 AS numeric(9)) AS kinsyubetu_kei_kingaku01    -- 金種別計_金額01
          ,CAST(kinsyubetu_kei_kaisu02 AS numeric(9)) AS kinsyubetu_kei_kaisu02    -- 金種別計_回数02
          ,CAST(kinsyubetu_kei_kingaku02 AS numeric(9)) AS kinsyubetu_kei_kingaku02    -- 金種別計_金額02
          ,CAST(kinsyubetu_kei_kaisu03 AS numeric(9)) AS kinsyubetu_kei_kaisu03    -- 金種別計_回数03
          ,CAST(kinsyubetu_kei_kingaku03 AS numeric(9)) AS kinsyubetu_kei_kingaku03    -- 金種別計_金額03
          ,CAST(kinsyubetu_kei_kaisu04 AS numeric(9)) AS kinsyubetu_kei_kaisu04    -- 金種別計_回数04
          ,CAST(kinsyubetu_kei_kingaku04 AS numeric(9)) AS kinsyubetu_kei_kingaku04    -- 金種別計_金額04
          ,CAST(kinsyubetu_kei_kaisu05 AS numeric(9)) AS kinsyubetu_kei_kaisu05    -- 金種別計_回数05
          ,CAST(kinsyubetu_kei_kingaku05 AS numeric(9)) AS kinsyubetu_kei_kingaku05    -- 金種別計_金額05
          ,CAST(kinsyubetu_kei_kaisu06 AS numeric(9)) AS kinsyubetu_kei_kaisu06    -- 金種別計_回数06
          ,CAST(kinsyubetu_kei_kingaku06 AS numeric(9)) AS kinsyubetu_kei_kingaku06    -- 金種別計_金額06
          ,CAST(kinsyubetu_kei_kaisu07 AS numeric(9)) AS kinsyubetu_kei_kaisu07    -- 金種別計_回数07
          ,CAST(kinsyubetu_kei_kingaku07 AS numeric(9)) AS kinsyubetu_kei_kingaku07    -- 金種別計_金額07
          ,CAST(kinsyubetu_kei_kaisu08 AS numeric(9)) AS kinsyubetu_kei_kaisu08    -- 金種別計_回数08
          ,CAST(kinsyubetu_kei_kingaku08 AS numeric(9)) AS kinsyubetu_kei_kingaku08    -- 金種別計_金額08
          ,CAST(kinsyubetu_kei_kaisu09 AS numeric(9)) AS kinsyubetu_kei_kaisu09    -- 金種別計_回数09
          ,CAST(kinsyubetu_kei_kingaku09 AS numeric(9)) AS kinsyubetu_kei_kingaku09    -- 金種別計_金額09
          ,CAST(kinsyubetu_kei_kaisu10 AS numeric(9)) AS kinsyubetu_kei_kaisu10    -- 金種別計_回数10
          ,CAST(kinsyubetu_kei_kingaku10 AS numeric(9)) AS kinsyubetu_kei_kingaku10    -- 金種別計_金額10
          ,CAST(kinsyubetu_kei_kaisu11 AS numeric(9)) AS kinsyubetu_kei_kaisu11    -- 金種別計_回数11
          ,CAST(kinsyubetu_kei_kingaku11 AS numeric(9)) AS kinsyubetu_kei_kingaku11    -- 金種別計_金額11
          ,CAST(kinsyubetu_kei_kaisu12 AS numeric(9)) AS kinsyubetu_kei_kaisu12    -- 金種別計_回数12
          ,CAST(kinsyubetu_kei_kingaku12 AS numeric(9)) AS kinsyubetu_kei_kingaku12    -- 金種別計_金額12
          ,CAST(kinsyubetu_kei_kaisu13 AS numeric(9)) AS kinsyubetu_kei_kaisu13    -- 金種別計_回数13
          ,CAST(kinsyubetu_kei_kingaku13 AS numeric(9)) AS kinsyubetu_kei_kingaku13    -- 金種別計_金額13
          ,CAST(kinsyubetu_kei_kaisu14 AS numeric(9)) AS kinsyubetu_kei_kaisu14    -- 金種別計_回数14
          ,CAST(kinsyubetu_kei_kingaku14 AS numeric(9)) AS kinsyubetu_kei_kingaku14    -- 金種別計_金額14
          ,CAST(kinsyubetu_kei_kaisu15 AS numeric(9)) AS kinsyubetu_kei_kaisu15    -- 金種別計_回数15
          ,CAST(kinsyubetu_kei_kingaku15 AS numeric(9)) AS kinsyubetu_kei_kingaku15    -- 金種別計_金額15
          ,CAST(kinsyubetu_kei_kaisu16 AS numeric(9)) AS kinsyubetu_kei_kaisu16    -- 金種別計_回数16
          ,CAST(kinsyubetu_kei_kingaku16 AS numeric(9)) AS kinsyubetu_kei_kingaku16    -- 金種別計_金額16
          ,CAST(kinsyubetu_kei_kaisu17 AS numeric(9)) AS kinsyubetu_kei_kaisu17    -- 金種別計_回数17
          ,CAST(kinsyubetu_kei_kingaku17 AS numeric(9)) AS kinsyubetu_kei_kingaku17    -- 金種別計_金額17
          ,CAST(kinsyubetu_kei_kaisu18 AS numeric(9)) AS kinsyubetu_kei_kaisu18    -- 金種別計_回数18
          ,CAST(kinsyubetu_kei_kingaku18 AS numeric(9)) AS kinsyubetu_kei_kingaku18    -- 金種別計_金額18
          ,CAST(kinsyubetu_kei_kaisu19 AS numeric(9)) AS kinsyubetu_kei_kaisu19    -- 金種別計_回数19
          ,CAST(kinsyubetu_kei_kingaku19 AS numeric(9)) AS kinsyubetu_kei_kingaku19    -- 金種別計_金額19
          ,CAST(kinsyubetu_kei_kaisu20 AS numeric(9)) AS kinsyubetu_kei_kaisu20    -- 金種別計_回数19
          ,CAST(kinsyubetu_kei_kingaku20 AS numeric(9)) AS kinsyubetu_kei_kingaku20    -- 金種別計_金額19
          ,CAST(genkin_aridaka AS numeric(9)) AS genkin_aridaka    -- 現金在高
          ,CAST(genkin_sotokei AS numeric(9)) AS genkin_sotokei    -- 現金外計
          ,CAST(sotozei_kei AS numeric(9)) AS sotozei_kei    -- 外税計
          ,CAST(uchizei_kei AS numeric(9)) AS uchizei_kei    -- 内税計
          ,CAST(zei_goukei AS numeric(9)) AS zei_goukei    -- 税合計
          ,CAST(souuriage_tensuu AS numeric(9)) AS souuriage_tensuu    -- 総売上点数
          ,CAST(uriage_kingaku_sum AS numeric(9)) AS uriage_kingaku_sum    -- 総売上金額
          ,CAST(sou_uri_kyakusuu AS numeric(9)) AS sou_uri_kyakusuu    -- 総売客数
          ,CAST(nebiki_kingaku AS numeric(9)) AS nebiki_kingaku    -- 値引金額
          ,CAST([m&m_nebiki_gaku] AS numeric(9)) AS [m&m_nebiki_gaku]    -- M&M値引額
          ,CAST(set_hanbai_nebiki_gaku AS numeric(9)) AS set_hanbai_nebiki_gaku    -- セット販売値引額
          ,CAST(baihen_nebiki_gaku AS numeric(9)) AS baihen_nebiki_gaku    -- 売変値引額
          ,CAST(waribiki1 AS numeric(9)) AS waribiki1    -- 割引１
          ,CAST(waribiki2 AS numeric(9)) AS waribiki2    -- 割引２
          ,CAST(waribiki3 AS numeric(9)) AS waribiki3    -- 割引３
          ,CAST(waribiki4 AS numeric(9)) AS waribiki4    -- 割引４
          ,CAST(waribiki5 AS numeric(9)) AS waribiki5    -- 割引５
          ,CAST(ryousyuusyo_hakkou_kaisuu AS numeric(9)) AS ryousyuusyo_hakkou_kaisuu    -- 領収書発行回数
          ,CAST(ryousyuusyo_hakkou_kingaku AS numeric(9)) AS ryousyuusyo_hakkou_kingaku    -- 領収書発行金額
          ,CAST(insi_zei1_kaisu AS numeric(9)) AS insi_zei1_kaisu        -- 印紙税1回数
          ,CAST(insi_zei1_kingaku AS numeric(9)) AS insi_zei1_kingaku    -- 印紙税1金額
          ,CAST(insi_zei2_kaisu AS numeric(9)) AS insi_zei2_kaisu        -- 印紙税2回数
          ,CAST(insi_zei2_kingaku AS numeric(9)) AS insi_zei2_kingaku    -- 印紙税2金額
          ,CAST(insi_zei3_kaisu AS numeric(9)) AS insi_zei3_kaisu        -- 印紙税3回数
          ,CAST(insi_zei3_kingaku AS numeric(9)) AS insi_zei3_kingaku    -- 印紙税3金額
          ,CAST(insi_zei4_kaisu AS numeric(9)) AS insi_zei4_kaisu        -- 印紙税4回数
          ,CAST(insi_zei4_kingaku AS numeric(9)) AS insi_zei4_kingaku    -- 印紙税4金額
          ,CAST(insi_zei5_kaisu AS numeric(9)) AS insi_zei5_kaisu        -- 印紙税5回数
          ,CAST(insi_zei5_kingaku AS numeric(9)) AS insi_zei5_kingaku    -- 印紙税5金額
          ,CAST(insi_zei6_kaisu AS numeric(9)) AS insi_zei6_kaisu        -- 印紙税6回数
          ,CAST(nyuukin_kaisuu AS numeric(9)) AS nyuukin_kaisuu          -- 入金回数
          ,CAST(nyuukin_kingaku AS numeric(9)) AS nyuukin_kingaku        -- 入金金額
          ,CAST(siharai_kaisuu AS numeric(9)) AS siharai_kaisuu          -- 支払回数
          ,CAST(siharai_kibngaku AS numeric(9)) AS siharai_kibngaku      -- 支払金額
          ,CAST(kaisyuu_kaisuu AS numeric(9)) AS kaisyuu_kaisuu          -- 回収回数
          ,CAST(kaisyuu_kingaku AS numeric(9)) AS kaisyuu_kingaku        -- 回収金額
          ,CAST(turisen_junbikin_kaisuu AS numeric(9)) AS turisen_junbikin_kaisuu    -- 釣銭準備金回数
          ,CAST(turisen_junbikin_kingaku AS numeric(9)) AS turisen_junbikin_kingaku  -- 釣銭準備金金額
          ,CAST(syouhinken_turi_kaisuu AS numeric(9)) AS syouhinken_turi_kaisuu      -- 商品券釣回数
          ,CAST(syouhinken_turi_kingaku AS numeric(9)) AS syouhinken_turi_kingaku    -- 商品券釣金額
          ,CAST(media_turi_kaisuu AS numeric(9)) AS media_turi_kaisuu                -- メディア釣回数
          ,CAST(media_turi_kingaku AS numeric(9)) AS media_turi_kingaku              -- メディア釣金額
          ,CAST(temoti_aridaka_kingaku AS numeric(9)) AS temoti_aridaka_kingaku      -- 手持在高金額
          ,CAST(manken_maisuu AS numeric(9)) AS manken_maisuu    -- 万券枚数
          ,'NAV75908'            --登録者
          ,GETDATE()            --登録日時
          ,'NAV75908'            --更新者
          ,GETDATE()            --更新日時
FROM t_ts_uriage_houkoku_data_kakutei_if    --TS売上報告データ（確定）IF

       SELECT @t_ts_uriage_houkoku_data_kakutei_if_count += @@ROWCOUNT, @err = @@error;
       IF (@err <> 0)
           BEGIN
               SET @error_msg = @programid+'（TS売上報告テーブル）t_ts_uriage_houkoku_data_kakutei_ifに情報を更新でエラー'
               GOTO END_RTN;
           END
           

-- **************************************
-- 日付データテーブルより運用日を取り出す。
-- **************************************
SELECT @m_hiduke_unyou_date = unyou_date FROM m_hiduke;

IF (@@error <>  0 OR @m_hiduke_unyou_date ='')
BEGIN
    SET @error_msg = 'm_hidukeのSELECTでエラー'
    GOTO END_RTN;
END

-- ******************************
-- 売上報告_部門別売上データを削除
-- ******************************
    DELETE t_bumonbetu_uriage
    WHERE tenpo_cd            = @tenpocd                      --店コード
    AND saisyuu_uriage_date in (SELECT DISTINCT                --データ作成日
                                   uriage_date
                            FROM  t_uriage_houkoku
                            WHERE tenpo_cd = @tenpocd)
    AND torikesi_kbn        = '0'                           --取消区分
    AND online_kbn          = '1';                          --オンライン区分

    SELECT @t_bumonbetu_uriage_delete_count = @t_bumonbetu_uriage_delete_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = 't_bumonbetu_uriageのDELETEエラー。'
           GOTO END_RTN;
       END

    --売上報告_部門別売上データを登録
    INSERT INTO t_bumonbetu_uriage
    (
        tenpo_cd                  -- 店コード
        ,saisyuu_uriage_date       -- 最終売上日
        ,torikesi_kbn              -- 取消区分
        ,regi_no                   -- レジNO
        ,bumon_cd                  -- 部門コード
        ,online_kbn                -- オンライン区分
        ,kingaku                   -- 金額
        ,data_sakusei_date         -- データ作成日
        ,touroku_user              -- 登録者
        ,touroku_date              -- 登録日時
        ,kousin_user               -- 更新者
        ,kousin_date               -- 更新日時
    )
    SELECT tuh.tenpo_cd                  --店コード
          ,tuh.uriage_date               --最終売上日
          ,0                             --取消区分
          ,tuh.regi_no                   --レジNO
          ,RIGHT(tuh.bumon_cd, 2)        --部門コード
          ,'1'                           --オンライン区分 
          ,tuh.bumon_uriage_kingaku      --金額
          ,@m_hiduke_unyou_date          --「＠運用日」
          ,'NAV75908'                    --登録者
          ,GETDATE()                     --登録日時
          ,'NAV75908'                    --更新者
          ,GETDATE()                     --更新日時
    FROM v_uriage_houkoku_bumon AS tuh   --売上報告_部門別売上ビュー
    WHERE tuh.tenpo_cd = @tenpocd;

    SELECT @t_bumonbetu_uriage_insert_count += @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = @programid+'（売上報告_部門別売上データ）t_bumonbetu_uriageに情報を更新でエラー'
           GOTO END_RTN;
       END


-- ******************************
-- 売上報告_売場別売上テーブルデータを削除
-- ******************************
    DELETE t_uribabetu_uriage 
    WHERE tenpo_cd            = @tenpocd                      --店コード日
      AND saisyuu_uriage_date in (SELECT DISTINCT              --最終売上日
                                         uriage_date
                                   FROM  t_uriage_houkoku
                                   WHERE tenpo_cd = @tenpocd)
      AND torikesi_kbn        = '0'                           --取消区分
      AND online_kbn          = '1'                           --オンライン区分
    SELECT @t_uribabetu_uriage_delete_count = @t_uribabetu_uriage_delete_count + @@ROWCOUNT, @err = @@error;
    IF (@err<> 0)
       BEGIN
           SET @error_msg = 't_uribabetu_uriageのDELETEエラー(1)。'
           GOTO END_RTN;
       END;
    ----売上報告_売場別売上データ登録
    INSERT INTO t_uribabetu_uriage
    (
        tenpo_cd
       ,saisyuu_uriage_date
       ,torikesi_kbn
       ,regi_no
       ,data_syubetu
       ,uriba_cd
       ,online_kbn
       ,uriage_suuryou
       ,kyaku_suu
       ,kingaku
       ,kake_kingaku
       ,syouhizei
       ,data_sakusei_date
       ,touroku_user
       ,touroku_date
       ,kousin_user
       ,kousin_date
    )
     SELECT tuh.tenpo_cd                            --店コード
           ,tuh.uriage_date                         --最終売上日
           ,0                                       --取消区分
           ,tuh.regi_no                             --レジNO
           ,'02'                                    --データ種別
           ,RIGHT(tuh.uriba_cd, 2)                  --売場コード
           ,'1'                                     --オンライン区分
           ,ISNULL(tuh.uriba_uriage_tensuu, 0)      --売上数量 = 売場売上点数
           ,ISNULL(tuh.uriba_uriage_kyakusuu, 0)    --客数 = 売場売上客数
           ,0                                       --金額
           ,0                                       --掛金額
           ,0                                       --消費税
           ,@m_hiduke_unyou_date                    --「＠運用日」
           ,'NAV75908'                              --登録者
           ,GETDATE()                               --登録日時
           ,'NAV75908'                              --更新者
           ,GETDATE()                               --更新日時
     FROM v_uriage_houkoku_uriba AS tuh             --売上報告_売場別売上ビュー
     WHERE tuh.tenpo_cd = @tenpocd
    SELECT @t_uribabetu_uriage_insert_count = @t_uribabetu_uriage_insert_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = 't_uribabetu_uriageのINSERTエラー(1)。'
           GOTO END_RTN;
       END            
    --⑤-2売上報告_売場別売上テーブル登録（総売上）
    INSERT INTO t_uribabetu_uriage
    (
        tenpo_cd
       ,saisyuu_uriage_date
       ,torikesi_kbn
       ,regi_no
       ,data_syubetu
       ,uriba_cd
       ,online_kbn
       ,uriage_suuryou
       ,kyaku_suu
       ,kingaku
       ,kake_kingaku
       ,syouhizei
       ,data_sakusei_date
       ,touroku_user
       ,touroku_date
       ,kousin_user
       ,kousin_date
    )
    SELECT tuh.tenpo_cd      --店コード
          ,tuh.uriage_date   --最終売上日
          ,'0'               --取消区分
          ,tuh.regi_no       --レジNO
          ,'03'              --データ種別
          ,'00'              --売場コード
          ,'1'               --オンライン区分
          ,sum(uriage_suuryou)
          ,ISNULL(tuh.sou_uri_kyakusuu,0)   --総売客数(客数)
          ,(ISNULL(tuh.genkin_aridaka,0) + ISNULL(tuh.kingaku_03,0) +ISNULL(tuh.kingaku_04,0)+ISNULL(tuh.kingaku_05,0)+ISNULL(tuh.kingaku_17,0)-ISNULL(tuh.media_turi_kingaku,0)) AS genkingaku
          ,(ISNULL(tuh.kingaku_02,0) + ISNULL(tuh.kingaku_credit,0) + ISNULL(tuh.kingaku_debit,0) + ISNULL(tuh.kingaku_09,0)) AS kake_kingaku
          ,ISNULL(tuh.zei_goukei,0)    --消費税
          ,@m_hiduke_unyou_date        --「＠運用日」
          ,'NAV75908'
          ,GETDATE()
          ,'NAV75908'
          ,GETDATE()
    FROM t_uriage_houkoku AS tuh                 --売上報告テーブル
    INNER JOIN t_uribabetu_uriage AS tu          --売上報告_売場別売上テーブル
    ON (tuh.tenpo_cd = tu.tenpo_cd and tuh.uriage_date = tu.saisyuu_uriage_date and tuh.regi_no = tu.regi_no)
    WHERE tuh.tenpo_cd    = @tenpocd
    AND tu.data_sakusei_date = @m_hiduke_unyou_date
    GROUP BY tuh.tenpo_cd,tuh.uriage_date,tu.torikesi_kbn,tuh.regi_no,tuh.genkin_aridaka,tuh.kingaku_02,
    tuh.kingaku_03,tuh.kingaku_04,tuh.kingaku_05,tuh.media_turi_kingaku,tuh.kingaku_17,tuh.sou_uri_kyakusuu,
    tuh.genkin_gaikei,tuh.kingaku_credit,tuh.kingaku_debit,tuh.kingaku_09,tuh.zei_goukei

    SELECT @t_uribabetu_uriage_insert_count += @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = @programid+'（売上報告_売場別売上テーブル）t_uribabetu_uriageに情報を更新でエラー'
           GOTO END_RTN;
       END

-- ******************************
-- 売上報告_売掛金照会テーブルデータを削除
-- ******************************
    DELETE t_urikakekin_syoukai 
     WHERE tenpo_cd            = @tenpocd                      --店コード
       AND saisyuu_uriage_date in (SELECT DISTINCT              --最終売上日
                                         uriage_date
                                   FROM  t_uriage_houkoku
                                   WHERE tenpo_cd = @tenpocd)
      AND torikesi_kbn         = '0'                          --取消区分
      AND online_kbn           = '1'                          --オンライン区分
    SELECT @t_urikakekin_syoukai_delete_count = @t_urikakekin_syoukai_delete_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
        BEGIN
            SET @error_msg = 't_urikakekin_syoukaiのDELETEエラー。'
            GOTO END_RTN;
       END;

    --⑥売上報告_売掛金照会テーブル登録
    INSERT INTO t_urikakekin_syoukai
    SELECT tuh.tenpo_cd                           --店舗コード
          ,tuh.uriage_date                        --売上日
          ,'0'                                    --取消区分
          ,tuh.regi_no                            --レジNO
          ,'1'                                    --オンライン区分
          ,(ISNULL(tuh.genkin_gaikei,0) - ISNULL(tuh.kingaku_03,0) -ISNULL(tuh.kingaku_04,0)-ISNULL(tuh.kingaku_05,0)+ISNULL(tuh.media_turi_kingaku,0)-ISNULL(tuh.kingaku_17,0)) AS kakekinkaku
          ,0                                      --消費税(売)
          ,ISNULL(bumon_uriage_kingaku_52 ,0)     --部門売上金額52
          ,0                                      --掛金額(掛)
          ,0                                      --消費税(掛)
          ,0                                      --内金(掛)
          ,(ISNULL(tuh.genkin_aridaka,0) + ISNULL(tuh.kingaku_03,0) +ISNULL(tuh.kingaku_04,0)+ISNULL(tuh.kingaku_05,0)-ISNULL(tuh.media_turi_kingaku,0)+ISNULL(tuh.kingaku_17,0)) AS genkingaku
          ,ISNULL(tuh.genkin_aridaka,0)           --現金在高
          ,ISNULL(tuh.syouhinken_turi_kingaku,0)  --商品券釣金額
          ,@m_hiduke_unyou_date                   --[運用日]
          ,'NAV75908'
          ,GETDATE()
          ,'NAV75908'
          ,GETDATE()
    FROM t_uriage_houkoku AS tuh 
    WHERE  tuh.tenpo_cd    = @tenpocd;

    SELECT @t_urikakekin_syoukai_insert_count += @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = @programid+'（売上報告_売掛金照会テーブル）t_urikakekin_syoukaiに情報を更新でエラー'
           GOTO END_RTN;
       END

-- ******************************
-- 売上報告_商品券テーブルデータを削除
-- ******************************
    DELETE t_syouhinken  
     WHERE tenpo_cd            = @tenpocd                      --店コード
       AND saisyuu_uriage_date in (SELECT DISTINCT              --最終売上日
                                         uriage_date
                                   FROM  t_uriage_houkoku
                                   WHERE tenpo_cd = @tenpocd)
       AND torikesi_kbn        = '0'                           --取消区分
       AND online_kbn          = '1';                          --オンライン区分
    SELECT @t_syouhinken_delete_count = @t_syouhinken_delete_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
        BEGIN
            SET @error_msg = 't_syouhinkenのDELETEエラー。'
            GOTO END_RTN;
       END;
    WITH THU AS (
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,1 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_03 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_03, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,2 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_04 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_04, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,3 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_05 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_05, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,4 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_credit , 0) as kaisuu         --回数
              ,ISNULL(tuh.kingaku_credit, 0) as kingaku        --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,5 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_debit , 0) as kaisuu          --回数
              ,ISNULL(tuh.kingaku_debit, 0) as kingaku         --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,6 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.media_turi_kaisuu , 0) as kaisuu     --回数
              ,ISNULL(tuh.media_turi_kingaku, 0) as kingaku    --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,7 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_17 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_17, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
         -- 2018/02/09 李松涛 PAID掛を追加↓
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,8 as gyo_no                                     --行番号
              ,1 as online_kbn                                 --オンライン区分
              ,ISNULL(tuh.kaisuu_09 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_09, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd    = @tenpocd
         --2018/02/09 李松涛 PAID掛を追加↑
    )
     INSERT INTO t_syouhinken
     (
         tenpo_cd
        ,saisyuu_uriage_date
        ,torikesi_kbn
        ,regi_no
        ,gyou_no
        ,online_kbn
        ,kaisuu
        ,kingaku
        ,data_sakusei_date
        ,touroku_user
        ,touroku_date
        ,kousin_user
        ,kousin_date
      )
      SELECT tenpo_cd       --店コード
            ,uriage_date    --最終売上日
            ,torikesi_kbn   --取消区分
            ,regi_no        --レジNO
            ,gyo_no         --行番号
            ,online_kbn     --オンライン区分
            ,kaisuu         --回数
            ,kingaku        --金額
            ,@m_hiduke_unyou_date      --[@運用日]
            ,'NAV75908'
            ,GETDATE()
            ,'NAV75908'
            ,GETDATE()
       FROM THU;

    SELECT @t_syouhinken_insert_count += @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = @programid+'（売上報告_商品券テーブルデータ）t_syouhinkenに情報を更新でエラー'
           GOTO END_RTN;
       END



-- ******************************
-- 売上報告_割引テーブルデータを削除
-- ******************************
    DELETE t_waribiki 
     WHERE tenpo_cd            = @tenpocd                      --店コード
       AND saisyuu_uriage_date in (SELECT DISTINCT              --最終売上日
                                         uriage_date
                                   FROM  t_uriage_houkoku
                                   WHERE tenpo_cd = @tenpocd)
       AND torikesi_kbn        = '0'                           --取消区分
       AND online_kbn          = '1';                          --オンライン区分

    SELECT @t_waribiki_delete_count = @t_waribiki_delete_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
        BEGIN
            SET @error_msg = 't_waribikiのDELETEエラー。'
            GOTO END_RTN;
        END;

    --売上報告_割引テーブル登録
    INSERT INTO t_waribiki
    (
        tenpo_cd
       ,saisyuu_uriage_date
       ,torikesi_kbn
       ,regi_no
       ,gyou_no
       ,online_kbn
       ,saimoku_cd
       ,kingaku
       ,data_sakusei_date
       ,touroku_user
       ,touroku_date
       ,kousin_user
       ,kousin_date
    )
     SELECT  tuh.tenpo_cd           --店コード
            ,tuh.uriage_date        --最終売上日
            ,'0'                    --取消区分
            ,tuh.regi_no            --レジNO
            ,'1'                    --行NO
            ,'1'                    --オンライン区分
            ,@t_waribiki_saimoku_cd --細目コード
            ,ISNULL(tuh.nebi_04,0)  --金額
            ,@m_hiduke_unyou_date   --[@運用日]
            ,'NAV75908'
            ,GETDATE()              --登録日時
            ,'NAV75908'
            ,GETDATE()              --更新日時
      FROM t_uriage_houkoku AS tuh 
      WHERE tuh.tenpo_cd = @tenpocd;
    SELECT @t_waribiki_insert_count = @t_waribiki_insert_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = 't_waribikiのINSERTエラー。'
           GOTO END_RTN;
       END;


    SELECT @t_waribiki_insert_count += @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = @programid+'（売上報告_割引テーブル）t_waribikiに情報を更新でエラー'
           GOTO END_RTN;
       END


-- ******************************
-- 売上報告_売掛金明細データを削除
-- ******************************
   DELETE t_urikake_meisai
    WHERE tenpo_cd             =  @tenpocd                    --店コード
       AND saisyuu_uriage_date in (SELECT DISTINCT             --最終売上日
                                         uriage_date
                                   FROM  t_uriage_houkoku
                                   WHERE tenpo_cd = @tenpocd)
       AND torikesi_kbn        = '0'                          --取消区分
       AND online_kbn          = '1';                         --オンライン区分

    SELECT @t_urikake_meisai_delete_count = @t_urikake_meisai_delete_count + @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
        BEGIN
            SET @error_msg = 't_urikake_meisaiのDELETEエラー。'
            GOTO END_RTN;
        END;

    INSERT INTO t_urikake_meisai
    SELECT tuh.tenpo_cd                          --店コード
          ,tuh.uriage_date                       --売上日
          ,'0'                                   --取消区分
          ,tuh.regi_no                           --レジNo
          ,'1'                                   --連番
          ,'1'                                   --オンライン区分
          ,''                                    --売掛区分
          ,''                                    --得意先区分
          ,''                                    --顧客氏名
          ,kingaku_02                            --売掛金額
          ,0                                     --金額
          ,0                                     --消費税
          ,0                                     --内金
          ,''                                    --取扱区分
          ,''                                    --分割回数
          ,0                                     --CAT件数
          ,''                                    --G-ACTフラグ
          ,@m_hiduke_unyou_date                  --[@運用日]
          ,'NAV75908'
          ,GETDATE()                             --登録日時
          ,'NAV75908'
          ,GETDATE()                             --更新日時
    FROM t_uriage_houkoku AS tuh                 --売上報告テーブル
    WHERE  tuh.tenpo_cd    = @tenpocd;

    SELECT @t_urikake_meisai_insert_count += @@ROWCOUNT, @err = @@error;
    IF (@err <> 0)
       BEGIN
           SET @error_msg = @programid+'（売上報告_売掛金明細テーブル）t_urikake_meisaiに情報を更新でエラー'
           GOTO END_RTN;
       END
       
END_RTN:

IF (@error_msg = '')
    BEGIN
        COMMIT TRANSACTION NAV75908
        PRINT '営業日' + CONVERT(VARCHAR,@tenpo_eigyou_date)
        PRINT ' 1.売上報告テーブル更新した件数' + CONVERT(VARCHAR,@t_ts_uriage_houkoku_data_kakutei_if_count) + '件'
        PRINT ' 2.売上報告_部門別売上テーブル更新した件数' + CONVERT(VARCHAR,@t_bumonbetu_uriage_insert_count) + '件'
        PRINT ' 3.売上報告_売場別売上テーブルに情報を更新した件数' + CONVERT(VARCHAR,@t_uribabetu_uriage_insert_count) + '件'
        PRINT ' 4.売上報告_売掛金照会テーブルに情報を更新した件数' + CONVERT(VARCHAR,@t_urikakekin_syoukai_insert_count) + '件'
        PRINT ' 5.売上報告_商品券テーブルデータに情報を更新した件数' + CONVERT(VARCHAR,@t_syouhinken_insert_count) + '件'
        PRINT ' 6.売上報告_割引テーブルに情報を更新した件数' + CONVERT(VARCHAR,@t_waribiki_insert_count) + '件'
        PRINT ' 7.売上報告_売掛金明細テーブルに情報を更新した件数' + CONVERT(VARCHAR,@t_urikake_meisai_insert_count) + '件'

        PRINT 'OSQL Success'
    END
ELSE
    BEGIN
        ROLLBACK TRANSACTION NAV75908
        PRINT 'OSQL Failure'
        PRINT @error_msg
    END
