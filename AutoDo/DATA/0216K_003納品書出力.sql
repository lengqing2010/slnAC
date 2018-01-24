select top 100
    tsj.nouhin_yotei_date
    ,tsj.truck_no
    ,tsj.juuki_no
    ,tsj.tc_center_cd
    ,mj.tenpo_mei_kanji
    ,tsj.konpou_no
FROM t_syukka_jisseki tsj
inner join m_jigyousyo mj
ON mj.tenpo_cd = tsj.tc_center_cd 

where
    tsj.truck_no in ('201711100653685')
ORDER BY
nouhin_yotei_date
,truck_no
,juuki_no  -- 什器No ,カゴ車／パレットNo
,tc_center_cd
,konpou_no





