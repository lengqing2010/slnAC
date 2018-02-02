IF EXISTS(SELECT * 
          FROM   tempdb..sysobjects 
          WHERE  id = Object_id('tempdb..#tmp0216k1647160553')) 
  BEGIN 
      DROP TABLE #tmp0216k1647160553 
  END 

SELECT  tsj.tenpo_cd 
       ,tsj.nouhin_yotei_date 
       ,tsj.truck_no 
       ,tsj.tc_center_cd 
       ,mj.tenpo_mei_kanji 
       ,tsj.torihikisaki_cd 
	,case when data_sakusei_kbn = '01' then 
		substring(tsj.konpou_no, 12,5) + '-' + substring(tsj.konpou_no,18,4)  
	else 
		substring(tsj.konpou_no,1,4) + '-' + substring(tsj.konpou_no,5,2) + '-' + substring(tsj.konpou_no,7,4)
	end konpou_no
       ,tsj.denpyou_no 
       ,tsj.gyou_no 
       ,tsj.syouhin_cd_jisya 
       ,tsj.syouhin_cd_jan_1 
       ,tsj.hattyuu_suu 
       ,tsj.syukka_suuryou 
       ,CASE 
          WHEN LEFT(tsj.syouhin_cd_jisya, 1) = '9' THEN msck.syouhin_cd_jisya 
          ELSE tsj.syouhin_cd_jisya 
        END AS syouhin_cd_jisya_key 
       ,tsj.data_sakusei_kbn 
       ,tsj.juuki_no 
INTO   #tmp0216k1647160553 
FROM   t_syukka_jisseki tsj 
       INNER JOIN m_jigyousyo mj 
               ON mj.tenpo_cd = tsj.tc_center_cd 
       LEFT JOIN m_syouhin_cd_kanri msck 
              ON msck.syouhin_cd_tokubai = tsj.syouhin_cd_jisya 
                 AND msck.daihyou_jan_kbn = '1' 
WHERE  ltrim(tsj.truck_no) in('201711100653685','201711150653681')  
   AND tsj.tenpo_cd = '1030'
SELECT main.tenpo_cd 
       ,CONVERT(char(10), CONVERT(DateTime,main.nouhin_yotei_date), 111) as nouhin_yotei_date 
       ,main.truck_no 
       ,main.tc_center_cd 
       ,main.tenpo_mei_kanji 
       ,main.torihikisaki_cd 
       ,main.konpou_no 
       ,main.denpyou_no 
       ,main.gyou_no 
       ,main.syouhin_cd_jisya_key as syouhin_cd_jisya 
       ,main.syouhin_cd_jan_1 
       ,msj.hyoujiyou_syouhin_mei_kanji 
       ,main.hattyuu_suu 
       ,main.syukka_suuryou 
       ,thj.zaiko 
       ,mud.uriba_address 
       ,mmbj.hattyuu_check_kbn 
       ,main.data_sakusei_kbn 
       ,main.juuki_no 
FROM   #tmp0216k1647160553 main 
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
       INNER JOIN m_uriba_daittyou mud 
               ON mud.tenpo_cd = main.tenpo_cd 
                  AND mud.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
ORDER BY main.truck_no,main.nouhin_yotei_date,main.torihikisaki_cd,main.konpou_no,main.denpyou_no,main.gyou_no
