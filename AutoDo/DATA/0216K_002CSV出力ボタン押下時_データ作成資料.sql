--１．　Get 可以?接的数据
select * from 
t_syukka_jisseki tsj
left join t_hibetu_jisseki thj
on thj.tenpo_cd = tsj.tenpo_cd
AND thj.syouhin_cd_jisya = tsj.syouhin_cd_jisya
AND thj.sakusei_date = tsj.nouhin_yotei_date
where tsj.tenpo_cd = '1030'
--and tsj.truck_no='201711110653687','4977449616825'

--２．　做成 
--update  t_syukka_jisseki 
--set nouhin_yotei_date = '20160930'
--WHERE  truck_no = '       00000000' 
--and tenpo_cd = '1030'











