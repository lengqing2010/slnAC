--select unyou_date from m_hiduke

/*
SELECT
    nouhin_yotei_date,
    truck_no,
    *
FROM
    t_syukka_jisseki
WHERE
	t_syukka_jisseki.tenpo_cd	=	'1030' 
    AND ISNULL(t_syukka_jisseki.truck_no,'')<>''
*/

--9始め
select * from t_syukka_jisseki
where tenpo_cd = '1030'
and nouhin_yotei_date = '20171113'
and truck_no='201711100653685'
--9始め
select * from t_syukka_jisseki
where tenpo_cd = '1030'
and nouhin_yotei_date = '20171113'
and truck_no='201711140653681'














