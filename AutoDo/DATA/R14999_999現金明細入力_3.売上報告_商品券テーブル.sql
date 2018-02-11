    declare @tenpocd varchar(4)
	    declare @uriagedate varchar(8)
    declare @unyoudate varchar(8)
    declare @job_id varchar(20)

	set @tenpocd = '1030'
	set @uriagedate = '20180118'
	set @unyoudate = '20180124'
	set @job_id = '00000001'
	
	WITH THU AS (
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,1 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_03 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_03, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,2 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_04 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_04, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,3 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_05 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_05, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,4 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_credit , 0) as kaisuu         --回数
              ,ISNULL(tuh.kingaku_credit, 0) as kingaku        --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,5 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_debit , 0) as kaisuu          --回数
              ,ISNULL(tuh.kingaku_debit, 0) as kingaku         --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,6 as gyo_no                                     --行番号
              ,ISNULL(tuh.media_turi_kaisuu , 0) as kaisuu     --回数
              ,ISNULL(tuh.media_turi_kingaku, 0) as kingaku    --金額
        FROM t_uriage_houkoku AS tuh                           --売上報告テーブル
        WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,7 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_17 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_17, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate 
        UNION ALL
        SELECT tuh.tenpo_cd                                    --店コード
              ,tuh.uriage_date                                 --最終売上日
              ,0 as torikesi_kbn                               --取消区分
              ,tuh.regi_no                                     --レジNO
              ,8 as gyo_no                                     --行番号
              ,ISNULL(tuh.kaisuu_9 , 0) as kaisuu             --回数
              ,ISNULL(tuh.kingaku_9, 0) as kingaku            --金額
         FROM t_uriage_houkoku AS tuh                          --売上報告テーブル
         WHERE tuh.tenpo_cd = @tenpocd
          AND  tuh.uriage_date=@uriagedate )

     INSERT INTO t_syouhinken(
         tenpo_cd
        ,saisyuu_uriage_date
        ,torikesi_kbn
        ,regi_no
        ,gyou_no
        ,online_kbn   
        ,kaisuu
        ,kingaku
       ,data_sakusei_date
        ,touroku_user
        ,touroku_date
        ,kousin_user
        ,kousin_date)
      SELECT tenpo_cd       --店コード
            ,uriage_date    --最終売上日
            ,'0'   --取消区分
            ,regi_no        --レジNO
            ,gyo_no         --行番号
            ,'1'            --オンライン区分
            ,kaisuu         --回数
            ,kingaku        --金額
           ,@unyoudate                     --データ作成日
            ,@job_id
            ,GETDATE()
            ,@job_id
            ,GETDATE()
       FROM THU

