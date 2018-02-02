select 
    CONVERT(char(10), CONVERT(DateTime,tsj.nouhin_yotei_date), 111) as nouhin_yotei_date 
    ,tsj.truck_no
    ,tsj.juuki_no
    ,tsj.tc_center_cd
    ,mj.tenpo_mei_kanji
	,case when data_sakusei_kbn = '01' then 
		substring(tsj.konpou_no, 12,5) + '-' + substring(tsj.konpou_no,18,4)  
	else 
		substring(tsj.konpou_no,1,4) + '-' + substring(tsj.konpou_no,5,2) + '-' + substring(tsj.konpou_no,7,4)
	end konpou_no
FROM t_syukka_jisseki tsj
inner join m_jigyousyo mj
ON mj.tenpo_cd = tsj.tc_center_cd 
where
    tsj.tenpo_cd = '1030'
    AND ltrim(tsj.truck_no) in ('201711140653681')
ORDER BY
tsj.nouhin_yotei_date
,tsj.truck_no
,tsj.juuki_no  -- 什器No ,カゴ車／パレットNo
,tsj.tc_center_cd
,tsj.konpou_no

