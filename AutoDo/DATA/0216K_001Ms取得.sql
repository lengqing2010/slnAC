SELECT 
	tenpo_cd 
	,truck_no 
--	,truck_no_nyuuryoku_date
	,MAX(ISNULL(kousin_kbn,'0')) AS kousin_kbn 
FROM 
	t_syukka_jisseki WITH(READCOMMITTED) 
WHERE 
	tenpo_cd = '1030'
	AND 
	ISNULL(truck_no,'') <> '' 
GROUP BY 
	tenpo_cd 
	,truck_no 
HAVING 
	tenpo_cd = '1030'
	AND 
	( 
		MAX(kousin_kbn) = 0 
		OR 
		MAX(truck_no_nyuuryoku_date) = '20171111' 
	) 
ORDER BY 
	tenpo_cd 
	,truck_no 





