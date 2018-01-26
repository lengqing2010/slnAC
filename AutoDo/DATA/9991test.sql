
declare @unyou_date varchar(8)
declare @tenpo_cd varchar(4)
set @tenpo_cd = '1030'

select @unyou_date = unyou_date from m_hiduke
print @unyou_date

--select * from t_syukka_jisseki 
--where tenpo_cd = @tenpo_cd
----and nouhin_yotei_date = CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,@unyou_date)),112)
--and truck_no in('201711100653685','201711140653681')
--and denpyou_no='452861' and left(konpou_no,1) = '9' and right(konpou_no,1) = '5'

--order by truck_no,nouhin_yotei_date



select * from t_syukka_jisseki 
where tenpo_cd = @tenpo_cd
--and nouhin_yotei_date = CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,@unyou_date)),112)
and truck_no in('201711100653685','201711140653681')
and data_sakusei_kbn<>'01'

order by truck_no,nouhin_yotei_date



--update t_syukka_jisseki set konpou_no = '123456789'+convert(varchar,gyou_no),data_sakusei_kbn='02'
--where tenpo_cd = @tenpo_cd
----and nouhin_yotei_date = CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,@unyou_date)),112)
--and truck_no in('201711100653685','201711140653681')
--and denpyou_no='452861' and left(konpou_no,1) = '9' and right(konpou_no,1) = '5'


--update t_syukka_jisseki set konpou_no = '123456789'+convert(varchar,gyou_no),data_sakusei_kbn='03'
--where tenpo_cd = @tenpo_cd
----and nouhin_yotei_date = CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,@unyou_date)),112)
--and truck_no in('201711100653685','201711140653681')
--and denpyou_no='452861' and left(konpou_no,1) = '9'


