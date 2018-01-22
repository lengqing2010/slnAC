
declare @tenpo_cd varchar(4)
declare @tenpo_mei varchar(20)

select 
    top 1
	*
from t_syukka_jisseki
inner join m_jigyousyo
    on m_jigyousyo.tenpo_cd = t_syukka_jisseki.tc_center_cd
left join m_syouhin_cd_kanri
    on m_syouhin_cd_kanri.syouhin_cd_tokubai	= t_syukka_jisseki.syouhin_cd_jisya
    AND m_syouhin_cd_kanri.daihyou_jan_kbn='1'
inner join m_syouhin_jyouhou
    on  
        m_syouhin_jyouhou.syouhin_cd_jisya = 
            case when left(t_syukka_jisseki.syouhin_cd_jisya,1) = '9' then 
                m_syouhin_cd_kanri.syouhin_cd_jisya
            else
                t_syukka_jisseki.syouhin_cd_jisya
            end 
inner join m_uriba_daittyou
    on  m_uriba_daittyou.tenpo_cd = t_syukka_jisseki.tenpo_cd
        AND m_uriba_daittyou.syouhin_cd_jisya = 
            case when left(t_syukka_jisseki.syouhin_cd_jisya,1) = '9' then 
                m_syouhin_cd_kanri.syouhin_cd_jisya
            else
                t_syukka_jisseki.syouhin_cd_jisya
            end 
inner join t_hibetu_jisseki
    on t_hibetu_jisseki.tenpo_cd = t_syukka_jisseki.tenpo_cd
        AND t_hibetu_jisseki.syouhin_cd_jisya = 
            case when left(t_syukka_jisseki.syouhin_cd_jisya,1) = '9' then 
                m_syouhin_cd_kanri.syouhin_cd_jisya
            else
                t_syukka_jisseki.syouhin_cd_jisya
            end 
        AND t_hibetu_jisseki.sakusei_date = t_syukka_jisseki.nouhin_yotei_date
inner join m_mise_betu_jyouhou
    on  m_mise_betu_jyouhou.tenpo_cd = t_syukka_jisseki.tenpo_cd
        AND m_mise_betu_jyouhou.syouhin_cd_jisya = 
            case when left(t_syukka_jisseki.syouhin_cd_jisya,1) = '9' then 
                m_syouhin_cd_kanri.syouhin_cd_jisya
            else
                t_syukka_jisseki.syouhin_cd_jisya
            end 
--where
--    t_syukka_jisseki.truck_no in ('')

order by 
    t_syukka_jisseki.truck_no

