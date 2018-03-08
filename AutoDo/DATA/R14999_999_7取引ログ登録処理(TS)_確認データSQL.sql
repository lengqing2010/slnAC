DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_uriage_houkoku where uriage_date=@tenpo_eigyou_date



DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_bumonbetu_uriage where saisyuu_uriage_date=@tenpo_eigyou_date




DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_uribabetu_uriage where saisyuu_uriage_date=@tenpo_eigyou_date


DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_urikakekin_syoukai where saisyuu_uriage_date=@tenpo_eigyou_date

DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_syouhinken where saisyuu_uriage_date=@tenpo_eigyou_date

DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_syouhinken where saisyuu_uriage_date=@tenpo_eigyou_date


DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_waribiki where saisyuu_uriage_date=@tenpo_eigyou_date


DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_urikake_meisai where saisyuu_uriage_date=@tenpo_eigyou_date


DECLARE @tenpo_eigyou_date                 VARCHAR(8)     -- 店舗営業日
set @tenpo_eigyou_date='20171204'
select * from t_point_riyou where saisyuu_uriage_date=@tenpo_eigyou_date


