USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Get_Titles]    Script Date: 12/20/2017 11:43:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[usp_Get_Titles] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF OBJECT_ID(N'tempdb..#TMP_RepT_CF') IS NOT NULL
		DROP TABLE #TMP_RepT_CF
	
	--Get Min/Max Client Start/End dates into tmp table
	--This is important for PIVOT statement
	SELECT CF.* 
	INTO #TMP_RepT_CF
	FROM
		(
			SELECT	
				C.AnnouncementId,
				(CASE WHEN C.Channel = 'EST' THEN 'MinEST' WHEN C.Channel = 'POEST' THEN 'MinPOEST' WHEN  C.Channel = 'VOD' THEN 'MinVOD' ELSE C.Channel END) AS Channel,
				MIN(C.ClientStartDate) AS ClientDate
			FROM Announceemnt_Channel_Format C
			GROUP BY C.AnnouncementId, C.Channel
			UNION 
			SELECT	
				C.AnnouncementId,
				CASE WHEN C.Channel = 'EST' THEN 'MaxEST' WHEN C.Channel = 'POEST' THEN 'MaxPOEST' WHEN  C.Channel = 'VOD' THEN 'MaxVOD' ELSE C.Channel END,
				MAX(C.ClientEndDate) 
			FROM Announceemnt_Channel_Format C
			GROUP BY C.AnnouncementId, C.Channel
		) AS CF

		IF OBJECT_ID(N'tempdb..#Temp_Titles') IS NOT NULL
			DROP TABLE #Temp_Titles

		SELECT 
			AnnouncementId,
			Title,
			VideoVersion,
			LocalEdit,
			MinEST, MaxEST, MinPOEST, MaxPOEST, MinVOD, MaxVOD
		INTO #Temp_Titles
		FROM
		(	
			SELECT	
				C.AnnouncementId,
				AG.Title,
				AG.VideoVersion,
				AG.LocalEdit,
				C.Channel,
				C.ClientDate
			FROM #TMP_RepT_CF C
				INNER JOIN AnnouncementGrid AG ON AG.ID = C.AnnouncementId
				INNER JOIN OrderGrid O ON O.AnnouncementId = C.AnnouncementId
		) AS C
		PIVOT(Min(ClientDate) FOR Channel IN(MinEST, MaxEST, MinPOEST, MaxPOEST, MinVOD, MaxVOD)) AS PivotTable1

		--SELECT * FROM #Temp_Titles

		IF OBJECT_ID(N'tempdb..#AnnouncementOrder') IS NOT NULL
			DROP TABLE #AnnouncementOrder
		
		SELECT 
			A.Id As AnnouncementId,
			O.Id AS OrderId, 
			A.VideoVersion , 
			(CASE WHEN A.LocalEdit IS NULL OR A.LocalEdit = '' OR A.LocalEdit = 'No' THEN 'No' ELSE 'Yes' END) AS LocalEdit, 
			A.LanguageId ,
			L.Name As Language,
			A.TerritoryId,
			T.WbTerritory AS Territory,
			O.LanguageType,
			O.Category
		INTO #AnnouncementOrder
		From OrderGrid  O INNER JOIN AnnouncementGrid A ON A.Id = O.AnnouncementId
		--INNER JOIN Announceemnt_Channel_Format C ON C.AnnouncementId = A.Id
		INNER JOIN [Language] L ON A.LanguageId = L.Id
		INNER JOIN Territory T ON T.Id = A.TerritoryId

		IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'dbo.[TitlesReport]') and OBJECTPROPERTY(id, N'IsTable') = 1)
			DROP TABLE dbo.[TitlesReport]
		SELECT * INTO TitlesReport FROM(
		SELECT 
			VQT.TitleName,
			VQT.VendorId,
			VQT.VIDStatus,
			VQT.AppleTerritory,
			VQT.POESTStartDate,
			VQT.ESTStartDate,
			VQT.ESTEndDate,
			VQT.LiveESTHD,
			VQT.LiveESTSD,
			VQT.VODStartDate,
			VQT.VODEndDate,
			VQT.LiveVODHD,
			VQT.LiveVODSD,
			VQT.MPM, 
			MinPOEST, 
			MinEST, 
			MinVOD,
			MaxPOEST, 
			MaxEST, 
			MaxVOD, 
			VQT.ComponentType,
			VQT.ComponentQuality,
			VQT.LanguageName,
			VQT.QAStatus AS [LanguageStatus],
			AG.LanguageType,
			VQT.VideoVersion,
			AG.Category
	FROM #Temp_Titles TL INNER JOIN #AnnouncementOrder AG ON AG.AnnouncementId = TL.AnnouncementId
		INNER JOIN [vw_VID_QCReport_Title] VQT 
		ON VQT.VideoVersion = ISNULL(AG.VideoVersion, 'NULLVAL') 
					AND VQT.LocalEdit = AG.LocalEdit
					AND VQT.LanguageId = AG.LanguageId  
					AND VQT.TerritoryId = AG.TerritoryId
					AND dbo.fnGetLanguageType(VQT.ComponentType, VQT.LanguageName, VQT.AppleTerritory)  = AG.LanguageType
	 UNION
	 SELECT 
			VQT.TitleName,
			VQT.VendorId,
			VQT.VIDStatus,
			VQT.AppleTerritory,
			VQT.POESTStartDate,
			VQT.ESTStartDate,
			VQT.ESTEndDate,
			VQT.LiveESTHD,
			VQT.LiveESTSD,
			VQT.VODStartDate,
			VQT.VODEndDate,
			VQT.LiveVODHD,
			VQT.LiveVODSD,
			VQT.MPM, 
			NULL, 
			NULL, 
			NULL,
			NULL, 
			NULL, 
			NULL, 
			VQT.ComponentType,
			VQT.ComponentQuality,
			VQT.LanguageName,
			VQT.QAStatus AS [LanguageStatus],
			NULL,
			VQT.VideoVersion,
			NULL
	 FROM [vw_VID_QCReport_Title] VQT
	 WHERE VQT.ComponentType = 'VIDEO') AS tmp
END












GO

