--CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,syukka_date)),112)

declare @unyou_date varchar(8)
declare @unyou_date_add1 varchar(8)
declare @unyou_date_bef7 varchar(8)
declare @tenpo_cd varchar(4)
declare @dousa varchar(1)
--test
set @tenpo_cd = '1045'-- 画面引渡し項目．店コード
set @dousa = '3' -- 1:csv 2:納品書

select @unyou_date = unyou_date from m_hiduke
set @unyou_date_add1 = CONVERT(VARCHAR(8),DATEADD(DAY,1,CONVERT(DATETIME,@unyou_date)),112)
set @unyou_date_bef7 = CONVERT(VARCHAR(8),DATEADD(DAY,-7,CONVERT(DATETIME,@unyou_date)),112)

if @dousa = '1'  
begin
    SELECT
        truck_no
    FROM
        t_syukka_jisseki
    WHERE
    	t_syukka_jisseki.tenpo_cd	=	@tenpo_cd 
        AND ISNULL(t_syukka_jisseki.truck_no,'')<>''
        AND nouhin_yotei_date > @unyou_date_bef7
    ORDER BY truck_no ASC
end 
else if @dousa = '2'  
begin
    SELECT
        truck_no
    FROM
        t_syukka_jisseki
    WHERE
    	t_syukka_jisseki.tenpo_cd	=	@tenpo_cd 
        AND ISNULL(t_syukka_jisseki.truck_no,'')<>''
        AND nouhin_yotei_date in (@unyou_date,@unyou_date_add1)
    ORDER BY truck_no ASC
end 
else  
begin
    SELECT
        truck_no
    FROM
        t_syukka_jisseki
    WHERE
    	t_syukka_jisseki.tenpo_cd	=	@tenpo_cd 
        AND ISNULL(t_syukka_jisseki.truck_no,'')<>''
    ORDER BY truck_no ASC
end 































