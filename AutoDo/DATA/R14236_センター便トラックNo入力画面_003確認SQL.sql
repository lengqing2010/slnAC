declare @tenpo_cd varchar(10)
set @tenpo_cd = '1045'
declare @syouhin_cd_jisya varchar(10)
set @syouhin_cd_jisya = '10010004'


select * from t_syukka_jisseki tsj
WHERE 
 tsj.tenpo_cd = @tenpo_cd 
  AND syouhin_cd_jisya = @syouhin_cd_jisya

 SELECT * FROM t_niuke where ten_cd = @tenpo_cd
 AND syouhin_cd_jisya = @syouhin_cd_jisya

 SELECT * FROM m_uriba_daittyou where  tenpo_cd = '1045'
AND syouhin_cd_jisya = @syouhin_cd_jisya

