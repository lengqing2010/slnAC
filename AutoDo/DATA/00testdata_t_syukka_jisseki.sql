declare @unyou_date varchar(8)
declare @unyou_date_add1 varchar(8)
declare @unyou_date_bef6 varchar(8)
declare @tenpo_cd varchar(4)
declare @dousa varchar(1)
--test
set @tenpo_cd = '1030'-- 画面引渡し項目．店コード

select @unyou_date = unyou_date from m_hiduke
print @unyou_date
set @unyou_date_add1 = CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,@unyou_date)),112)
set @unyou_date_bef6 = CONVERT(VARCHAR(8),DATEADD(DAY,-6,CONVERT(DATETIME,@unyou_date)),112)
select * 
  FROM [dbo].[t_syukka_jisseki]
where tenpo_cd = '1030'
and nouhin_yotei_date = '20171113'
and truck_no='201711100653685'

--9始め

--insert into [t_syukka_jisseki]
--SELECT [tenpo_cd]
--      ,[denpyou_no]
--      ,[gyou_no]
--      ,[bunrui_cd]
--      ,[syouhin_cd_jisya]
--      ,[syouhin_cd_jan_1]
--      ,[torihikisaki_cd]
--      ,[truck_no]
--      ,left([konpou_no],26)+'99'

--      ,[hattyuu_date]
--      ,nouhin_yotei_date--@unyou_date --[nouhin_yotei_date]
--      ,[irisuu]
--      ,[syukka_suuryou]
--      ,[gen_tanka]
--      ,[tenpo_baika]
--      ,[syouhin_cd_jan_2]
--      ,[denpyou_kbn]
--      ,[tokubai_kbn]
--      ,[sakusei_date]
--      ,[tc_center_cd]
--      ,[syukka_date]
--      ,[syukka_time]
--      ,[hattyuu_suu]
--      ,[record_kbn]
--      ,[data_sakusei_kbn]
--      ,[juuki_no]
--      ,[henpin_kbn]
--      ,[kousin_kbn]
--      ,[truck_no_nyuuryoku_date]
--      ,[touroku_user]
--      ,[touroku_date]
--      ,[kousin_user]
--      ,[kousin_date]
--  FROM [dbo].[t_syukka_jisseki]
--where tenpo_cd = '1030'
--and nouhin_yotei_date = '20171113'
--and truck_no='201711100653685'

--insert into [t_syukka_jisseki]
--SELECT [tenpo_cd]
--      ,[denpyou_no]
--      ,[gyou_no]
--      ,[bunrui_cd]
--      ,[syouhin_cd_jisya]
--      ,[syouhin_cd_jan_1]
--      ,[torihikisaki_cd]
--      ,[truck_no]
--      ,'8'+right([konpou_no],27)
--      ,[hattyuu_date]
--      ,@unyou_date_add1 --[nouhin_yotei_date]
--      ,[irisuu]
--      ,[syukka_suuryou]
--      ,[gen_tanka]
--      ,[tenpo_baika]
--      ,[syouhin_cd_jan_2]
--      ,[denpyou_kbn]
--      ,[tokubai_kbn]
--      ,[sakusei_date]
--      ,[tc_center_cd]
--      ,[syukka_date]
--      ,[syukka_time]
--      ,[hattyuu_suu]
--      ,[record_kbn]
--      ,[data_sakusei_kbn]
--      ,[juuki_no]
--      ,[henpin_kbn]
--      ,[kousin_kbn]
--      ,[truck_no_nyuuryoku_date]
--      ,[touroku_user]
--      ,[touroku_date]
--      ,[kousin_user]
--      ,[kousin_date]
--  FROM [dbo].[t_syukka_jisseki]
--where tenpo_cd = '1030'
--and nouhin_yotei_date = '20171113'
--and truck_no='201711100653685'

--insert into [t_syukka_jisseki]
--SELECT [tenpo_cd]
--      ,[denpyou_no]
--      ,[gyou_no]
--      ,[bunrui_cd]
--      ,[syouhin_cd_jisya]
--      ,[syouhin_cd_jan_1]
--      ,[torihikisaki_cd]
--      ,[truck_no]
--      ,'8'+right([konpou_no],27)
--      ,[hattyuu_date]
--      ,@unyou_date_bef6 --[nouhin_yotei_date]
--      ,[irisuu]
--      ,[syukka_suuryou]
--      ,[gen_tanka]
--      ,[tenpo_baika]
--      ,[syouhin_cd_jan_2]
--      ,[denpyou_kbn]
--      ,[tokubai_kbn]
--      ,[sakusei_date]
--      ,[tc_center_cd]
--      ,[syukka_date]
--      ,[syukka_time]
--      ,[hattyuu_suu]
--      ,[record_kbn]
--      ,[data_sakusei_kbn]
--      ,[juuki_no]
--      ,[henpin_kbn]
--      ,[kousin_kbn]
--      ,[truck_no_nyuuryoku_date]
--      ,[touroku_user]
--      ,[touroku_date]
--      ,[kousin_user]
--      ,[kousin_date]
--  FROM [dbo].[t_syukka_jisseki]
--where tenpo_cd = '1030'
--and nouhin_yotei_date = '20171113'
--and truck_no='201711100653685'


--9始め無し


--insert into [t_syukka_jisseki]
--SELECT [tenpo_cd]
--      ,[denpyou_no]
--      ,[gyou_no]
--      ,[bunrui_cd]
--      ,[syouhin_cd_jisya]
--      ,[syouhin_cd_jan_1]
--      ,[torihikisaki_cd]
--      ,[truck_no]
--      ,'8'+right([konpou_no],27)
--      ,[hattyuu_date]
--      ,@unyou_date --[nouhin_yotei_date]
--      ,[irisuu]
--      ,[syukka_suuryou]
--      ,[gen_tanka]
--      ,[tenpo_baika]
--      ,[syouhin_cd_jan_2]
--      ,[denpyou_kbn]
--      ,[tokubai_kbn]
--      ,[sakusei_date]
--      ,[tc_center_cd]
--      ,[syukka_date]
--      ,[syukka_time]
--      ,[hattyuu_suu]
--      ,[record_kbn]
--      ,[data_sakusei_kbn]
--      ,[juuki_no]
--      ,[henpin_kbn]
--      ,[kousin_kbn]
--      ,[truck_no_nyuuryoku_date]
--      ,[touroku_user]
--      ,[touroku_date]
--      ,[kousin_user]
--      ,[kousin_date]
--  FROM [dbo].[t_syukka_jisseki]
--where tenpo_cd = '1030'
--and nouhin_yotei_date = '20160930'
--and truck_no='201711140653681'
--insert into [t_syukka_jisseki]
--SELECT [tenpo_cd]
--      ,[denpyou_no]
--      ,[gyou_no]
--      ,[bunrui_cd]
--      ,[syouhin_cd_jisya]
--      ,[syouhin_cd_jan_1]
--      ,[torihikisaki_cd]
--      ,[truck_no]
--      ,'7'+right([konpou_no],27)
--      ,[hattyuu_date]
--      ,@unyou_date_add1 --[nouhin_yotei_date]
--      ,[irisuu]
--      ,[syukka_suuryou]
--      ,[gen_tanka]
--      ,[tenpo_baika]
--      ,[syouhin_cd_jan_2]
--      ,[denpyou_kbn]
--      ,[tokubai_kbn]
--      ,[sakusei_date]
--      ,[tc_center_cd]
--      ,[syukka_date]
--      ,[syukka_time]
--      ,[hattyuu_suu]
--      ,[record_kbn]
--      ,[data_sakusei_kbn]
--      ,[juuki_no]
--      ,[henpin_kbn]
--      ,[kousin_kbn]
--      ,[truck_no_nyuuryoku_date]
--      ,[touroku_user]
--      ,[touroku_date]
--      ,[kousin_user]
--      ,[kousin_date]
--  FROM [dbo].[t_syukka_jisseki]
--where tenpo_cd = '1030'
--and nouhin_yotei_date = '20160930'
--and truck_no='201711140653681'

--insert into [t_syukka_jisseki]
--SELECT [tenpo_cd]
--      ,[denpyou_no]
--      ,[gyou_no]
--      ,[bunrui_cd]
--      ,[syouhin_cd_jisya]
--      ,[syouhin_cd_jan_1]
--      ,[torihikisaki_cd]
--      ,[truck_no]
--      ,'6'+right([konpou_no],27)
--      ,[hattyuu_date]
--      ,@unyou_date_bef6 --[nouhin_yotei_date]
--      ,[irisuu]
--      ,[syukka_suuryou]
--      ,[gen_tanka]
--      ,[tenpo_baika]
--      ,[syouhin_cd_jan_2]
--      ,[denpyou_kbn]
--      ,[tokubai_kbn]
--      ,[sakusei_date]
--      ,[tc_center_cd]
--      ,[syukka_date]
--      ,[syukka_time]
--      ,[hattyuu_suu]
--      ,[record_kbn]
--      ,[data_sakusei_kbn]
--      ,[juuki_no]
--      ,[henpin_kbn]
--      ,[kousin_kbn]
--      ,[truck_no_nyuuryoku_date]
--      ,[touroku_user]
--      ,[touroku_date]
--      ,[kousin_user]
--      ,[kousin_date]
--  FROM [dbo].[t_syukka_jisseki]
--where tenpo_cd = '1030'
--and nouhin_yotei_date = '20160930'
--and truck_no='201711140653681'

--?除??数据
/*
delete
  FROM [dbo].[t_syukka_jisseki]
where tenpo_cd = '1030'
*/
and nouhin_yotei_date > '20180117'