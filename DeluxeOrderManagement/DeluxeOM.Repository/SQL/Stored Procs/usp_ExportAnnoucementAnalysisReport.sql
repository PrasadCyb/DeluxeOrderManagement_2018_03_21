CREATE PROCEDURE [dbo].[usp_ExportAnnoucementAnalysisReport]
		@whereClause  AS NVARCHAR(4000)
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)

	/*
		IF OBJECT_ID(N'tmp_AnalysisRep') IS NOT NULL
		DROP TABLE tmp_AnalysisRep
	*/
	/*	
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_NAME = N'tmp_AnalysisRep')
	BEGIN
		DROP TABLE tmp_AnalysisRep
	END
	*/

	TRUNCATE TABLE tmp_AnalysisRep

	SET @sqlCommand ='
		INSERT INTO [tmp_AnalysisRep]
           ([Title]
           ,[MPM]
           ,[VideoVersion]
           ,[LocalEdit]
           ,[Category]
           ,[VendorId]
           ,[Territory]
           ,[Language]
           ,[LanguageType]
           ,[OrderStatus]
           ,[POESTStart]
           ,[StartDate])
		SELECT  
			A.Title,
			V.MPM,
			A.VideoVersion, 
			A.LocalEdit,
			A.Category,
			V.VendorId, 
			T.WBTerritory AS Territory, 
			L.Name AS Language,
			A.LanguageTYpe AS LanguageType,
			A.OrderStatus ,
			A.MinPOEST AS POESTStart,
			A.ESTVODStartDate AS StartDate -- , Count(1) AllCnt
		--INTO tmp_AnalysisRep
		FROM   
			(
				SELECT  AG.Title, AG.VideoVersion, 
					(CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '''' OR AG.LocalEdit = ''No'' THEN ''No'' ELSE ''Yes'' END) AS LocalEdit, 
					AG.TerritoryId,
					AG.[LanguageId] ,
					O.Category,
					O.LanguageTYpe,
					MIN(CPOEST.ClientStartDate) AS MinPOEST,
					MIN(CESTVOD.ClientStartDate) AS ESTVODStartDate,
					O.OrderStatus,
					AG.JObId
				FROM AnnouncementGrid AG 
					INNER JOIN (SELECT AnnouncementId, Category, LanguageTYpe, OrderStatus, JobId FROM OrderGrid WHERE JobId IS NOT NULL ) O ON AG.Id = O.AnnouncementId
					LEFT JOIN (SELECT AnnouncementId, ClientStartDate, JobId FROM Announceemnt_Channel_Format WHERE Channel = ''POEST'' AND JobId IS NOT NULL ) CPOEST ON AG.Id = CPOEST.AnnouncementId
					LEFT JOIN (SELECT AnnouncementId, ClientStartDate, JobId FROM Announceemnt_Channel_Format WHERE Channel <> ''POEST'' AND JobId IS NOT NULL) CESTVOD ON AG.Id = CESTVOD.AnnouncementId
				WHERE (ISNULL(O.JobId, '''') = @JobId OR ISNULL(CPOEST.JObId, '''') =@JobId OR ISNULL(CESTVOD.JobId, '''') =@JobId)
				AND COALESCE(O.OrderStatus, ''NULLVAL'') <> ''Complete''
				GROUP BY AG.Title, AG.VideoVersion, 
					(CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '''' OR AG.LocalEdit = ''No'' THEN ''No'' ELSE ''Yes'' END) , 
					AG.TerritoryId,
					AG.[LanguageId] ,
					O.Category,
					O.LanguageTYpe,
					O.OrderStatus,
					AG.JObId
			) A INNER JOIN
		(SELECT DISTINCT VideoVersion, MPM,
			 (CASE WHEN EditName IS NULL OR EditName = '''' OR EditName = ''No'' Then ''No'' ELSE ''Yes'' END) AS LocalEdit,
			VendorId
			FROM VID WHERE VideoVersion IS NOT NULL AND VideoVersion <> '''' AND VIDStatus = ''PRIMARY'' 
		) V   
		ON A.VideoVersion = V.VideoVersion AND A.LocalEdit = V.LocalEdit
		INNER JOIN Territory T ON T.Id = A.TerritoryId
		INNER JOIN [Language] L ON A.LanguageId = L.Id
		WHERE ISNULL(A.OrderStatus, '''') NOT IN (''Complete'', ''Cancelled'') 
		'+  @whereClause +'
		 AND ISNULL(A.OrderStatus, '''') NOT IN (''Complete'', ''Cancelled'')
		ORDER BY A.VideoVersion'

		IF OBJECT_ID(N'tempdb..#AllData') IS NOT NULL
			DROP TABLE #AllData

		SELECT Title, MPM, VideoVersion, LocalEdit, Category, VendorId, Territory, Language, LanguageType, OrderStatus, POESTStart, StartDate, Count(1) cnt
		INTO #AllData
		FROM tmp_AnalysisRep
		GROUP BY Title, MPM, VideoVersion, LocalEdit, Category, VendorId, Territory, Language, LanguageType, OrderStatus, POESTStart, StartDate

		IF OBJECT_ID(N'tempdb..#AllDuplicate') IS NOT NULL
		DROP TABLE #AllDuplicate

		SELECT Title, MPM, VideoVersion, LocalEdit, Category, VendorId, Territory, Language, 'Sub & Audio' AS LanguageType, OrderStatus, POESTStart, StartDate, Count(1) cnt
		INTO #AllDuplicate
		FROM tmp_AnalysisRep
		GROUP BY Title, MPM, VideoVersion, LocalEdit, Category, VendorId, Territory, Language, OrderStatus, POESTStart, StartDate
		HAVING COUNT(1) > 1

		UPDATE a
		SET a.cnt = b.cnt
		FROM #AllData a INNER JOIN #AllDuplicate b
		ON a.Title = b.Title and a.MPM = b.MPM and a.VideoVersion = b.VideoVersion and a.LocalEdit = b.LocalEdit and ISNULL(a.Category,'') = ISNULL(b.Category,'') and ISNULL(a.VendorId,'') = ISNULL(b.VendorId,'') 
		and a.Territory = b.Territory and a.Language = b.Language and ISNULL(a.OrderStatus,'') = ISNULL(b.OrderStatus,'') and ISNULL(a.POESTStart,'') = ISNULL(b.POESTStart,'') 
		and ISNULL(a.StartDate,'') = ISNULL(b.StartDate ,'')

		Update #AllData
		SET LanguageType = 'Sub & Audio'
		WHERE cnt = 2

		select DISTINCT Title, MPM, VideoVersion, LocalEdit, Category, VendorId, Territory, Language, LanguageType, OrderStatus, POESTStart, StartDate from #AllData ORDER BY VideoVersion
		-- GROUP BY Title, MPM, VideoVersion, LocalEdit, Category, VendorId, Territory, Language, LanguageType, OrderStatus, POESTStart, StartDate
			 --print @sqlCommand
		
				EXECUTE sp_executesql @sqlCommand;
		
				IF EXISTS(SELECT 1 from sys.objects where name = 'tmp_AnalysisRep') 
				BEGIN
				insert into OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;Database=D:\DeluxeOrderManagement\Reports\Annoucement Analysis Report.xlsx; TypeGuessRows=0; HDR=YES;',
				'SELECT * FROM [Sheet1$]') 
				select * from tmp_AnalysisRep

				--commented as not working with dybamic qry
				--drop table tmp_AnalysisRep
				END

END








GO


