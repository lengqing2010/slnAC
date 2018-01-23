IF EXISTS(SELECT * 
          FROM   tempdb..sysobjects 
          WHERE  id = Object_id('tempdb..#tmp0216k')) 
  BEGIN 
      DROP TABLE #tmp0216k 
  END 

SELECT tsj.tenpo_cd 
       ,tsj.nouhin_yotei_date 
       ,tsj.truck_no 
       ,tsj.tc_center_cd 
       ,mj.tenpo_mei_kanji 
       ,tsj.torihikisaki_cd 
       ,tsj.konpou_no 
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
INTO   #tmp0216k 
FROM   t_syukka_jisseki tsj 
       INNER JOIN m_jigyousyo mj 
               ON mj.tenpo_cd = tsj.tc_center_cd 
       LEFT JOIN m_syouhin_cd_kanri msck 
              ON msck.syouhin_cd_tokubai = tsj.syouhin_cd_jisya 
                 AND msck.daihyou_jan_kbn = '1' 
WHERE  tsj.truck_no = '201711140653681' 

SELECT main.tenpo_cd 
       ,main.nouhin_yotei_date 
       ,main.truck_no 
       ,main.tc_center_cd 
       ,main.tenpo_mei_kanji 
       ,main.torihikisaki_cd 
       ,main.konpou_no 
       ,main.denpyou_no 
       ,main.gyou_no 
       ,main.syouhin_cd_jisya 
       ,main.syouhin_cd_jan_1 
       ,msj.hyoujiyou_syouhin_mei_kanji 
       ,main.hattyuu_suu 
       ,main.syukka_suuryou 
       ,thj.zaiko 
       ,mud.uriba_address 
       ,mmbj.hattyuu_check_kbn 
FROM   #tmp0216k main 
       INNER JOIN m_syouhin_jyouhou msj 
               ON msj.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
       INNER JOIN t_hibetu_jisseki thj 
               ON thj.tenpo_cd = main.tenpo_cd 
                  AND thj.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
                  AND thj.sakusei_date = main.nouhin_yotei_date 
       --(この条件） 
       INNER JOIN m_mise_betu_jyouhou mmbj 
               ON mmbj.tenpo_cd = main.tenpo_cd 
                  AND mmbj.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
       INNER JOIN m_uriba_daittyou mud 
               ON mud.tenpo_cd = main.tenpo_cd 
                  AND mud.syouhin_cd_jisya = main.syouhin_cd_jisya_key 
ORDER BY main.truck_no











