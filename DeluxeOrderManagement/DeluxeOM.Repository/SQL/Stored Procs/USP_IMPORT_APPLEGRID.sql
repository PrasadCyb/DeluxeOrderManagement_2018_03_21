
CREATE PROCEDURE USP_IMPORT_APPLEGRID
AS	
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
			   WHERE TABLE_NAME = N'AppleGrid_STAGING')
	BEGIN
		DROP TABLE AppleGrid_STAGING  
	END

	/* VID */
	INSERT INTO VID 
	(
		VIDStatusId,
		Bundle,
		Extras,
		TitleName,
		MPM,
		VideoVersion,
		EditName,
		VersionEIDR,
		AppleId,
		VendorId,
		TitleCategoryId
	)
	SELECT DISTINCT L.ID, NULL, NULL, AGS.Title, AGS.MPM, AGS.[Video Version], (CASE WHEN (AGS.[Local Edit] IS NULL OR AGS.[Local Edit] = '') THEN 'No' ELSE AGS.[Local Edit] END), AGS.[Version EIDR], NULL, AGS.[Vendor Id], NULL
		FROM vwAnnouncementGridStg AGS 
		LEFT JOIN fnLookup('vidStatus') L ON L.Name IN ('PRIMARY')
		WHERE (AGS.[Version EIDR] IS NOT NULL AND AGS.[Version EIDR] <> '') 
		EXCEPt (SELECT L.ID, NULL, NULL, V.[TitleName], V.MPM, V.VideoVersion,(CASE WHEN (V.EditName IS NULL OR V.EditName = '') THEN 'No' ELSE 'Yes' END), V.VersionEIDR, NULL, V.VendorId, NULL FROM VID V
		LEFT JOIN fnLookup('vidStatus') L ON L.Name IN ('PRIMARY') )

		/* Language */
		SELECT count(1) from Language
			INSERT INTO Language
			(
				Name
			)
			SELECT DISTINCT [Language] FROM vwAnnouncementGridStg WHERE [Language] NOT IN (SELECT Name FROM Language)
		
		
		/* Region */
		INSERt inTO REGION 
		(
			Name
		)
		SELECT DISTINCT Region FROM vwAnnouncementGridStg WHERE Region NOT IN (SELECT Name FROM Region)
		
		
		/* Territory */

		INSERT INTO Territory
		(
			WBTerritory,
			RegionId
		)
			SELECT 
			DISTINCT AGS.Territory,
			R.Id
			FROM vwAnnouncementGridStg AGS INNER JOIN Region R ON AGS.Region = R.Name
			WHERE AGS.Territory NOT IN (SELECT WBTerritory FROM Territory) 

		--TerritoryLanguage 
		/*
		INSERT INTO TerritoryLanguage
		(
			TerritoryId,
			LanguageSeq,
			LanguageId
		) 
		(SELECT T.Id As TerritoryId, 1, L.Id As LanguageId
					FROM [AppleGrid_STAGING] AGS INNER JOIN Territory T ON AGS.Territory = T.WBTerritory
					INNER JOIN Language L ON AGS.Language = L.Name
					WHERE (T.Id + '-' +  L.Id) NOT IN (SELECT TerritoryId + '-' + LanguageId FROM TerritoryLanguage)) 
		*/

		/*	AnnouncementGrid */
		/* Insert Into AnnouncementGrid */
		--TRUNCATE TABLE AnnouncementGrid
		INSERT INTO AnnouncementGrid
			(
				VIDId,
				TerritoryId,
				LanguageId,
				LanguageTypeId,
				MPM,
				CategoryId,
				PrimaryVID,
				PreOrderSDTitleStatus,
				PreOrderHDTitleStatus,
				ESTSDTitleStatus,
				ESTHDTitleStatus,
				VODSDTitleStatus,
				VODHDTitleStatus,
				AnnouncementPreOrderSDDae,
				AnnouncementPreOrderHDDate,
				AnnouncementESTSDStart,
				AnnouncementESTHDStart,
				AnnouncementVODSDStart,
				AnnouncementVODHDStart,
				FirstAnnouncedDate,
				LastAnnouncementDate,
				CustomerId
			)
		SELECT 
				--AGS.rownum,
				V.Id AS VidId, 
				T.Id AS TerritoryId, 
				L.Id As LanguageId,
				LTy.Id AS LanguageTypeId,
				AGS.MPM, 
				Cat.Id AS CategoryId, 
				CASE WHEN (AGS.[Primary VID] = 'No' OR AGS.[Primary VID] = '') THEN 0 ELSE 1 END,
				SDTitle.Id AS STTitleSTatusId,
				HDTitle.Id AS HDTitleStatusId,
				ESTSDTitle.Id AS ESTSGTitleStatusId, 
				ESTHDTitle.Id As ESTHDTitleStatusId,
				VODSDTitle.Id As VODSDTitleStatusId,
				VODHDTitle.Id As VODHDTitleStatusId,
				CAST(AGS.[Announcement Pre Order SD Date] AS DATETIME),
				CAST(AGS.[Announcement Pre Order HD Date] AS DATETIME),
				CAST(AGS.[Announcement EST SD Start] AS DATETIME),
				CAST(AGS.[Announcement EST HD Start] AS DATETIME),
				CAST(AGS.[Announcement VOD SD Start] AS DATETIME),
				CAST(AGS.[Announcement VOD HD Start] AS DATETIME),
				AGS.[First Announced Date] ,
				AGS.[Last Announcement Date] ,
				1 --Warner Bros
				--INTO AnnouncementGrid
FROM vwAnnouncementGridStg AGS LEFT JOIN VID V 
	ON AGS.[Video Version] = V.VideoVersion 
	AND (CASE WHEN (AGS.[Local Edit] IS NULL OR AGS.[Local Edit] = '') THEN 'No' ELSE AGS.[Local Edit] END) = (CASE WHEN (V.EditName IS NULL OR V.EditName = '') THEN 'No' ELSE 'Yes' END)
	AND AGS.[Version EIDR] = V.VersionEIDR AND AGS.[Vendor ID] = V.VendorId
	LEFT JOIN Territory T ON T.WBTerritory = AGS.Territory
	LEFT JOIN [Language] L ON L.Name = AGS.[Language]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('LanguageType')) LTy ON LTy.Name = AGS.[Language Type]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridCategory')) Cat ON Cat.Name = AGS.Category
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridTitleStatus')) SDTitle ON SDTitle.Name = AGS.[Pre-Order SD Title Status]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridTitleStatus')) HDTitle ON HDTitle.Name = AGS.[Pre-Order HD Title Status]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridTitleStatus')) ESTSDTitle ON ESTSDTitle.Name = AGS.[EST SD Title Status]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridTitleStatus')) ESTHDTitle ON ESTHDTitle.Name = AGS.[EST HD Title Status]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridTitleStatus')) VODSDTitle ON VODSDTitle.Name = AGS.[VOD SD Title Status]
	LEFT JOIN (SELECT ID, Name FROM fnLookup('gridTitleStatus')) VODHDTitle ON VODHDTitle.Name = AGS.[VOD HD Title Status]
--WHERE ((AGS.[Video Version] IS NOT NULL) AND (AGS.[Video Version] <> '' ))

		--SELECT * FROM TerritoryLanguage order by TerritoryId
		--SELECT * from [AppleGrid_STAGING] WHERE 
		--Select * from Region order by name
		--Select * from [dbo].[TerritoryMapping_Staging] ORDER BY SecondaryLanguage
		--SELECT * from VID where VersionEIDR is null or VersionEIDR = ''
END
GO


Select * from [AppleGrid_STAGING]
select count(id) from VID