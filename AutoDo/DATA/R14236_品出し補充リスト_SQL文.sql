
IF EXISTS(SELECT * 
          FROM   tempdb..sysobjects 
          WHERE  id = Object_id('tempdb..#tmp0216k1532143823')) 
  BEGIN 
      DROP TABLE #tmp0216k1532143823 
  END 

SELECT tsj.tenpo_cd 
       ,tsj.nouhin_yotei_date 
       ,tsj.truck_no 
       ,tsj.juuki_no 
       ,tsj.tc_center_cd 
       ,mj.tenpo_mei_kanji 
	,case when data_sakusei_kbn = '01' then 
		substring(tsj.konpou_no, 12,5) + '-' + substring(tsj.konpou_no,18,4)  
	else 
		substring(tsj.konpou_no,1,4) + '-' + substring(tsj.konpou_no,5,2) + '-' + substring(tsj.konpou_no,7,4)
	end konpou_no
       ,tsj.syouhin_cd_jisya 
       ,tsj.syouhin_cd_jan_1 
       ,tsj.syukka_suuryou 
       ,tsj.torihikisaki_cd 
       ,CASE 
          WHEN LEFT(tsj.syouhin_cd_jisya, 1) = '9' THEN msck.syouhin_cd_jisya 
          ELSE tsj.syouhin_cd_jisya 
        END AS syouhin_cd_jisya_key 
       ,tsj.data_sakusei_kbn 
       ,tsj.denpyou_no 
       ,tsj.gyou_no 
INTO   #tmp0216k1532143823 
FROM   t_syukka_jisseki tsj 
       INNER JOIN m_jigyousyo mj 
               ON mj.tenpo_cd = tsj.tc_center_cd 
       LEFT JOIN m_syouhin_cd_kanri msck 
              ON msck.syouhin_cd_tokubai = tsj.syouhin_cd_jisya 
                 AND msck.daihyou_jan_kbn = '1' 
WHERE  ltrim(tsj.truck_no) in('201711140653681')  
         AND tsj.tenpo_cd = '1030'
SELECT main.tenpo_cd 
       ,CONVERT(char(10), CONVERT(DateTime,main.nouhin_yotei_date), 111) as nouhin_yotei_date 
       ,main.truck_no 
       ,msj.uriba_cd 
       ,mud.uriba_mei 
       ,msj.daibunrui_cd 
       ,mud.daibunrui_mei 
       ,main.juuki_no 
       ,main.tc_center_cd 
       ,main.tenpo_mei_kanji 
       ,main.konpou_no 
       ,mud.uriba_address 
       ,main.syouhin_cd_jisya_key as syouhin_cd_jisya 
       ,main.syouhin_cd_jan_1 
       ,msj.hyoujiyou_syouhin_mei_kanji 
       ,main.syukka_suuryou 
       ,mud.tyoubo_zaiko 
       ,vrz.real_zaiko 
       ,mud.saidai_zaiko_ryou 
       ,mud.hattyuu_ten 
       ,mmbj.hattyuu_check_kbn 
       ,main.data_sakusei_kbn 
FROM   #tmp0216k1532143823 main 
       INNER JOIN m_syouhin_jyouhou msj 
               ON msj.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
       INNER JOIN t_hibetu_jisseki thj 
               ON thj.tenpo_cd = main.tenpo_cd 
                  AND thj.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
                  AND thj.sakusei_date = main.nouhin_yotei_date 
       --(この条件） 
       LEFT  JOIN m_mise_betu_jyouhou mmbj 
               ON mmbj.tenpo_cd = main.tenpo_cd 
                  AND mmbj.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
       INNER JOIN v_uriba_daittyou mud 
               ON mud.tenpo_cd = main.tenpo_cd 
                  AND mud.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
       INNER JOIN v_real_zaiko vrz 
               ON vrz.tenpo_cd = main.tenpo_cd 
                  AND vrz.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
WHERE
 (mud.tyoubo_zaiko<=0 OR vrz.real_zaiko<=0 )
AND msj.uriba_cd='11' 
AND msj.daibunrui_cd='04'
 
ORDER BY main.truck_no,main.nouhin_yotei_date,msj.uriba_cd,msj.daibunrui_cd,main.juuki_no,main.tc_center_cd,main.konpou_no
