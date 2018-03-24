declare @tenpo_cd varchar(10)
set @tenpo_cd = '1045'

declare @kousin_kbn varchar(10)
set @kousin_kbn = '0'

SELECT 
	tsj.tenpo_cd 
	,tsj.denpyou_no 
	,tsj.gyou_no 
	,tsj.torihikisaki_cd 
	,tsj.truck_no 
	,tsj.konpou_no 
	,tsj.syukka_suuryou
	,ISNULL(msk.syouhin_cd_jisya, tsj.syouhin_cd_jisya) as syouhin_cd_jisya
	,tsj.tenpo_baika 
	,tsj.data_sakusei_kbn 
	,tsj.henpin_kbn 
	,tsj.tc_center_cd 
	,vud.tanpin_kbn 
	,vud.tenpo_baika as tenpo_baika_mud
	,tn.syouhin_cd_jisya as syouhin_cd_jisya_tn
	,ttnd.syouhin_cd_jisya as syouhin_cd_jisya_ttnd
	,vud.tenpo_cd as vud_tenpo_cd
	,vud.syouhin_cd_jisya as vud_syouhin_cd_jisya
FROM t_syukka_jisseki tsj 
	   
LEFT JOIN m_syouhin_cd_kanri msk 
        ON msk.syouhin_cd_tokubai = tsj.syouhin_cd_jisya 
            AND msk.daihyou_jan_kbn = '1' 

LEFT JOIN t_niuke tn
		ON tn.ten_cd = tsj.tenpo_cd 
			AND tn.denpyou_no = tsj.denpyou_no 
			AND tn.gyou_no = tsj.gyou_no 
			AND tn.torihikisaki_cd = tsj.torihikisaki_cd 
LEFT JOIN v_uriba_daittyou vud 
        ON vud.tenpo_cd = tsj.tenpo_cd 
            AND vud.syouhin_cd_jisya = 
                ISNULL(msk.syouhin_cd_jisya, ISNULL(CASE WHEN tsj.syouhin_cd_jisya = '' THEN NULL ELSE tsj.syouhin_cd_jisya END ,tn.syouhin_cd_jisya))

LEFT JOIN t_tenburi_nyuuko_denpyou ttnd
        ON ttnd.tenpo_cd = tsj.tenpo_cd 
            AND ttnd.nyuuryoku_kbn = '2' 
            AND ttnd.torikesi_kbn = CASE 
                                    WHEN ISNULL(tsj.henpin_kbn,'') = '01' THEN '1' 
                                    ELSE '0' 
                                    END 
            AND ttnd.denpyou_no = tsj.denpyou_no 
            AND ttnd.gyou_no = tsj.gyou_no 
            AND rtrim(ltrim(ttnd.konpou_no)) = rtrim(ltrim(tsj.konpou_no))
            AND ttnd.syukko_tenpo_cd = tsj.tc_center_cd 
WHERE 
 tsj.tenpo_cd = @tenpo_cd 
 AND ( 
	tsj.truck_no = '123456789012345' 
 ) 
			AND 
			tsj.kousin_kbn = @kousin_kbn 
UNION ALL 
SELECT 
	tsj.tenpo_cd 
	,tsj.denpyou_no 
	,tsj.gyou_no 
	,tsj.torihikisaki_cd 
	,tsj.truck_no 
	,tsj.konpou_no 
	,tsj.syukka_suuryou
	,ISNULL(msk.syouhin_cd_jisya, tsj.syouhin_cd_jisya) as syouhin_cd_jisya
	,tsj.tenpo_baika 
	,tsj.data_sakusei_kbn 
	,tsj.henpin_kbn 
	,tsj.tc_center_cd 
	,vud.tanpin_kbn 
	,vud.tenpo_baika as tenpo_baika_mud
	,tn.syouhin_cd_jisya as syouhin_cd_jisya_tn
	,ttnd.syouhin_cd_jisya as syouhin_cd_jisya_ttnd
	,vud.tenpo_cd as vud_tenpo_cd
	,vud.syouhin_cd_jisya as vud_syouhin_cd_jisya
FROM t_syukka_jisseki tsj 
	   
LEFT JOIN m_syouhin_cd_kanri msk 
        ON msk.syouhin_cd_tokubai = tsj.syouhin_cd_jisya 
            AND msk.daihyou_jan_kbn = '1' 

LEFT JOIN t_niuke tn
		ON tn.ten_cd = tsj.tenpo_cd 
			AND tn.denpyou_no = tsj.denpyou_no 
			AND tn.gyou_no = tsj.gyou_no 
			AND tn.torihikisaki_cd = tsj.torihikisaki_cd 
LEFT JOIN v_uriba_daittyou vud 
        ON vud.tenpo_cd = tsj.tenpo_cd 
            AND vud.syouhin_cd_jisya = 
                ISNULL(msk.syouhin_cd_jisya, ISNULL(CASE WHEN tsj.syouhin_cd_jisya = '' THEN NULL ELSE tsj.syouhin_cd_jisya END ,tn.syouhin_cd_jisya))


LEFT JOIN t_tenburi_nyuuko_denpyou ttnd
        ON ttnd.tenpo_cd = tsj.tenpo_cd 
            AND ttnd.nyuuryoku_kbn = '2' 
            AND ttnd.torikesi_kbn = CASE 
                                    WHEN ISNULL(tsj.henpin_kbn,'') = '01' THEN '1' 
                                    ELSE '0' 
                                    END 
            AND ttnd.denpyou_no = tsj.denpyou_no 
            AND ttnd.gyou_no = tsj.gyou_no 
            AND rtrim(ltrim(ttnd.konpou_no)) = rtrim(ltrim(tsj.konpou_no))
            AND ttnd.syukko_tenpo_cd = tsj.tc_center_cd 

INNER JOIN (
	SELECT tenpo_cd,torihikisaki_cd FROM t_syukka_jisseki
	WHERE tenpo_cd = @tenpo_cd
 AND ( 
	truck_no = '123456789012345' 
 ) 
	GROUP BY tenpo_cd,torihikisaki_cd
) nabu1 ON
	nabu1.tenpo_cd = tsj.tenpo_cd 
	AND nabu1.torihikisaki_cd = tsj.torihikisaki_cd
WHERE 
 tsj.tenpo_cd = @tenpo_cd 
 AND RTRIM(LTRIM(ISNULL(tsj.truck_no,''))) = '' 
			AND 
			tsj.kousin_kbn = @kousin_kbn 
			AND 
			tsj.truck_no_nyuuryoku_date in (SELECT unyou_date FROM m_hiduke)  

