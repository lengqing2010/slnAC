declare @unyou_date varchar(8)
declare @tenpo_cd varchar(4)
declare @dousa varchar(1)
--test
set @tenpo_cd = '1045'-- 画面引渡し項目．店コード
set @dousa = '1' -- 1:csv 2:納品書

select @unyou_date = unyou_date from m_hiduke

select CONVERT(VARCHAR(8),DATEADD(DAY,-7,CONVERT(DATETIME,@unyou_date)),112)



