--update t_ts_uriage_houkoku_data_kakutei_if
-- set 
-- kinsyubetu_kei_kaisu10 = '+000000000000010'   --kaisuu_09 
-- ,kinsyubetu_kei_kingaku10 = '+000000000001000' --kingaku_09 
-- where tenpo_cd = '09991'

 	
--実行前データ元
select * from t_ts_sonota_uriage_kanryou_data_record_if
WHERE file_mei LIKE '%LVURI102%'

select  * from t_ts_uriage_houkoku_data_kakutei_if

-- select * from v_uriage_houkoku_bumon where tenpo_cd = '9991'

--実行後データ
print '＊　売上報告_変更 '
select * from t_uriage_houkoku where tenpo_cd = '9991'  and uriage_date='20171204'

print '＊　売上報告_部門別売上 '
select * from t_bumonbetu_uriage where tenpo_cd = '9991' and saisyuu_uriage_date='20171204'

print '＊　今回変更　売上報告_売場別売上 '
select * from t_uribabetu_uriage where tenpo_cd = '9991' and saisyuu_uriage_date='20171204' --***
print '＊　売上報告_売掛金照会 '
select * from t_urikakekin_syoukai where tenpo_cd = '9991' and saisyuu_uriage_date='20171204' 
print '＊　今回変更　売上報告_商品券 '
select * from t_syouhinken where tenpo_cd = '9991' and saisyuu_uriage_date='20171204' --***
print '＊　売上報告_割引 '
select * from t_waribiki where tenpo_cd = '9991' and saisyuu_uriage_date='20171204' 
print '＊　売上報告_売掛金明細 '
select * from t_urikake_meisai where tenpo_cd = '9991' and saisyuu_uriage_date='20171204' 