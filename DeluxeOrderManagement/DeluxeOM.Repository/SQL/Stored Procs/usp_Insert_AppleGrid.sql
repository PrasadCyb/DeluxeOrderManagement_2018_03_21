USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Insert_AppleGrid]    Script Date: 12/7/2017 5:12:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[usp_Insert_AppleGrid]
AS	
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	/*Insert Key Columns - VideoVersion, EditName and VendorId - IN VID which are not exisst in VID table */
	
	INSERT INTO VID
		(
			VideoVersion,
			EditName,
			VendorId,
			CreatedOn,
			CreatedBy,
			ModifiedOn,
			ModifiedBy
		)
		SELECT 
			V.[Video Version],
			V.LocalEdit,
			V.[Vendor ID],
			GETDATE(),
			'APPLEGRID',
			GETDATE(),
			'APPLEGRID'
		FROM
		(
			SELECT DISTINCT A.[Video Version], (CASE WHEN A.[Local Edit] IS NULL OR A.[Local Edit] = '' THEN 'No' ELSE A.[Local Edit] END) AS LocalEdit, A.[Vendor ID]
			FROM vwAnnouncementGridStg A 
			WHERE A.[Video Version] IS NOT NULL AND A.[Video Version] <> '' AND A.[Vendor ID] IS NOT NULL AND A.[Vendor ID] <> '' AND A.[Vendor ID] <> 'N/A'
			EXCEPT (SELECT VideoVersion, (CASE WHEN (EditName IS NULL OR EditName = '' OR EditName = 'No') THEN 'No' ELSE 'Yes' END), VendorId FROM VID)
		) V


		/* Update rest of the VID columns */
		UPDATE VID
		SET VIDStatus = 'ACTIVE',
			TitleName = A.Title,
			MPM = A.MPM,
			VersionEIDR = A.[Version EIDR],
			ModifiedOn = GETDATE(),
			ModifiedBy = 'APPLEGRID'
		FROM (	SELECT DISTINCT Title, MPM, [Video Version], [Local Edit], [Version EIDR], [Vendor ID]
				FROM vwAnnouncementGridStg
			) A 
		INNER JOIN VID ON 
			A.[Video Version] = VID.VideoVersion
			AND (CASE WHEN (VID.EditName IS NULL OR VID.EditName = '' OR VID.EditName = 'No') THEN 'No' ELSE 'Yes' END) = (CASE WHEN A.[Local Edit] IS NULL OR A.[Local Edit] = '' THEN 'No' ELSE A.[Local Edit] END)
		WHERE (VID.TitleName IS NULL OR VID.TitleName = '')
		
		

		/* Language */
		--SELECT count(1) from Language
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
		--Not necessarily to insert now as it is related to Apple
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

		TRUNCATE TABLE AnnouncementGrid
		--GO
		INSERT INTO AnnouncementGrid
			(
				Title,
				VideoVersion,
				LocalEdit,
				TerritoryId,
				LanguageId,
				FirstAnnouncedDate,
				LastAnnouncedDate,
				CustomerId
			)
		SELECT MAX(Title),
			[Video Version],
			[Local Edit],
			T.Id,
			L.Id, 
			MAX(CONVERT(DATETIME,CASE WHEN AGS.[First Announced Date]='Pre-CAS' THEN '1900-01-02' WHEN AGS.[First Announced Date]='N/A' THEN '1900-01-01' ELSE AGS.[First Announced Date]  END)) AS [First Announced Date],
			MAX(CONVERT(DATETIME,CASE WHEN AGS.[Last Announcement Date]='Pre-CAS' THEN '1900-01-02' WHEN AGS.[Last Announcement Date]='N/A' THEN '1900-01-01' ELSE AGS.[Last Announcement Date]  END)) AS [Last Announcement Date],
			--MAX(CONVERT(DATETIME, AGS.[Last Announcement Date]) ),
			C.Id
		FROM vwAnnouncementGridStg AGS
		LEFT JOIN Territory T ON T.WBTerritory = AGS.Territory
		LEFT JOIN [Language] L ON L.Name = AGS.[Language]
		LEFT JOIN Customer C ON C.Name = 'Warner Bros.'
		WHERE AGS.[Video Version] IS NOT NULL AND AGS.[Video Version] <> ''
		GROUP BY  
			--Title,
			[Video Version],
			[Local Edit],
			T.Id,
			L.Id, C.Id



		/* Insert Announceemnt_Channel_Fomrat */
		IF OBJECT_ID(N'tempdb..#AnnouncementGrid_Key') IS NOT NULL
		DROP TABLE #AnnouncementGrid_Key

		TRUNCATE TABLE Announceemnt_Channel_Format
		--drop table AnnouncementGrid_Key
			SELECT 
				STG.*, 
				A.Id AS AnnouncementId,
				L.Id AS LanguageId,
				T.Id AS TerritoryId
			INTO #AnnouncementGrid_Key
			FROM 
			(SELECT [Video Version], [Local Edit], Territory, Language, 
					MAX([Pre-Order SD Title Status]) AS [Pre-Order SD Title Status],
					MAX([Pre-Order HD Title Status]) AS [Pre-Order HD Title Status],
					MAX([EST SD Title Status]) AS [EST SD Title Status],
					MAX([EST HD Title Status]) AS [EST HD Title Status],
					MAX([VOD SD Title Status]) AS [VOD SD Title Status],
					MAX([VOD HD Title Status]) AS [VOD HD Title Status],
					MAX([Pre-Order 4k Title Status]) AS [Pre-Order 4k Title Status],
					MAX([EST 4k Title Status]) AS [EST 4k Title Status],
					MAX([VOD 4k Title Status]) AS [VOD 4k Title Status],
					MAX([Announcement Pre Order SD Date]) AS [Announcement Pre Order SD Date],
					MAX([Announcement Pre Order HD Date]) AS [Announcement Pre Order HD Date],
					MAX([Announcement EST SD Start]) AS [Announcement EST SD Start],
					MAX([Announcement EST HD Start]) AS [Announcement EST HD Start],
					MAX([Announcement VOD SD Start]) AS [Announcement VOD SD Start],
					MAX([Announcement VOD HD Start]) AS [Announcement VOD HD Start],
					MAX([Announcement Pre Order 4k Date]) AS [Announcement Pre Order 4k Date],
					MAX([Announcement EST 4k Start]) AS [Announcement EST 4k Start],
					MAX([Announcement VOD 4K Start]) AS [Announcement VOD 4K Start]
				FROM vwAnnouncementGridStg
				GROUP BY [Video Version], [Local Edit], Territory, Language) STG
				INNER JOIN AnnouncementGrid A ON STG.[Video Version] = A.VideoVersion and STG.[Local Edit] = A.LocalEdit  
				INNER JOIN Language L ON STG.Language = L.Name and L.Id = A.LanguageId
				INNER JOIN Territory T ON STG.Territory = T.WBTerritory and A.TerritoryId = T.Id

			TRUNCATE TABLE Announceemnt_Channel_Format
			--POEST/SD
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'POEST',
				'SD',
				STG.[Pre-Order SD Title Status],
				STG.[Announcement Pre Order SD Date],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId
			
			----POEST/HD	
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'POEST',
				'HD',
				STG.[Pre-Order HD Title Status],
				STG.[Announcement Pre Order HD Date],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId


				----EST/SD	
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'EST',
				'SD',
				STG.[EST SD Title Status],
				STG.[Announcement EST SD Start],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId

				----EST/HD	
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'EST',
				'HD',
				STG.[EST HD Title Status],
				STG.[Announcement EST HD Start],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId

				--VOD/SD
				INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'VOD',
				'SD',
				STG.[VOD SD Title Status],
				STG.[Announcement VOD SD Start],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId

			--VOD/HD
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'VOD',
				'HD',
				STG.[VOD HD Title Status],
				STG.[Announcement VOD HD Start],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId

			--POEST/4K
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'POEST',
				'4kUHD',
				STG.[Pre-Order 4k Title Status],
				STG.[Announcement Pre Order 4k Date],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId


			--EST/4K
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'EST',
				'4kUHD',
				STG.[EST 4k Title Status],
				STG.[Announcement EST 4k Start],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId

			--VOD/4K
			INSERT INTO Announceemnt_Channel_Format
			(
				AnnouncementId,
				Channel,
				Format,
				ClientAvailStatus,
				ClientStartDate,
				CustomerId
			)
			SELECT 
				A.Id,
				'VOD',
				'4kUHD',
				STG.[VOD 4k Title Status],
				STG.[Announcement VOD 4K Start],
				1
			FROM #AnnouncementGrid_Key STG
				INNER JOIN AnnouncementGrid A ON A.Id = STG.AnnouncementId

			--Update ChannelFormat column
			UPDATE Announceemnt_Channel_Format SET ChannelFormat = (Channel + Format)
		/**/

		--select count(1) from AnnouncementGrid
		/* INSERT ORDER GRID */
		TRUNCATE TABLE OrderGrid
		--GO
		INSERT INTO OrderGrid
		(
			AnnouncementId,
			LanguageType,
			VIDId,
			Category,
			ESTUPC,
			IVODUPC,
			--Vendor,
			FileType,
			RequestNumber,
			MPO,
			HALID,
			OrderStatus,
			OrderRequestDate,
			DeliveryDueDate,
			ActualDeliveryDate,
			--FirstStartDate,
			GreenlightDueDate,
			GreenlightValidatedbyDMDM,
			GreenlightSenttoPackaging,
			Notes,
			CustomerId
		)
		SELECT A.ID,
				AGS.LanguageType,
				V.Id,
				AGS.Category,
				AGS.ESTUPC,
				AGS.IVODUPC,
				--AGS.Vendor,
				AGS.[FileType],
				AGS.[RequestNumber],
				AGS.[MPO],
				AGS.[HALID],
				AGS.[OrderStatus],
				AGS.[OrderRequestDate],
				--CONVERT(DATETIME,CASE WHEN AGS.[OrderRequestDate]='Pre-CAS' THEN '1900-01-02' WHEN AGS.[OrderRequestDate]='N/A' THEN '1900-01-01' ELSE AGS.[OrderRequestDate]  END) AS [OrderRequestDate],
				--[DeliveryDueDate],
				CONVERT(DATETIME,CASE WHEN [DeliveryDueDate]='Pre-CAS' THEN '1900-01-02' WHEN [DeliveryDueDate]='N/A' THEN '1900-01-01' ELSE [DeliveryDueDate]  END) AS [DeliveryDueDate],
				--[ActualDeliveryDate],
				CONVERT(DATETIME,CASE WHEN [ActualDeliveryDate]='Pre-CAS' THEN '1900-01-02' WHEN [ActualDeliveryDate]='N/A' THEN '1900-01-01' ELSE [ActualDeliveryDate]  END) AS [ActualDeliveryDate],
				--AGS.[FirstStartDate],
				--[GreenlightDueDate] ,
				CONVERT(DATETIME,CASE WHEN [GreenlightDueDate]='Pre-CAS' THEN '1900-01-02' WHEN [GreenlightDueDate]='N/A' THEN '1900-01-01' ELSE [GreenlightDueDate]  END) AS [GreenlightDueDate],
				--CONVERT(DATETIME,(CASE WHEN [GreenlightValidatedbyDMDM] IN ('N/A') THEN NULL ELSE CONVERT(Varchar(50),AGS.[GreenlightValidatedbyDMDM], 113) END)) AS [GreenlightValidatedbyDMDM],
				CONVERT(DATETIME,CASE WHEN GreenlightValidatedbyDMDM='Pre-CAS' THEN '1900-01-02' WHEN GreenlightValidatedbyDMDM='N/A' THEN '1900-01-01' ELSE GreenlightValidatedbyDMDM  END) AS GreenlightValidatedbyDMDM,
				--GreenlightValidatedbyDMDM,
				--[GreenlightSenttoPackaging] ,
				CONVERT(DATETIME,CASE WHEN [GreenlightSenttoPackaging]='Pre-CAS' THEN '1900-01-02' WHEN [GreenlightSenttoPackaging]='N/A' THEN '1900-01-01' ELSE [GreenlightSenttoPackaging]  END) AS [GreenlightSenttoPackaging],
				--null,
				AGS.Notes
				,C.Id AS CustomerId
			FROM vwOrderGrid AGS 
			INNER JOIN AnnouncementGrid A ON AGS.Title = A.Title and AGS.VideoVersion = A.VideoVersion 
				AND AGS.LocalEdit  = (CASE WHEN A.LocalEdit IS NULL OR A.LocalEdit = '' THEN 'No' ELSE A.LocalEdit END)
			INNER JOIN  VID V ON V.VendorId = AGS.VendorId AND V.VideoVersion = AGS.VideoVersion AND (CASE WHEN V.EditName IS NULL OR V.EditName = '' OR V.EditName = 'No' THEN 'No' ELSE 'Yes' END) = AGS.LocalEdit
			INNER JOIN Language L ON AGS.Language = L.Name  and L.Id = A.LanguageId
			INNER JOIN Territory T ON AGS.Territory = T.WBTerritory and A.TerritoryId = T.Id
			LEFT JOIN Customer C ON C.Name = 'Warner Bros.' 
			ORDER BY A.ID
	
			--Update 'Both' to 'Sub & Audio'
			UPDATE OrderGrid SET LanguageType = 'Sub & Audio' WHERE LanguageType = 'Both'
			UPDATE OrderGrid SET LanguageType = 'Audio' WHERE LanguageType = 'Dub'
			--Rename existing 'Sub & Audio' to 'Audio'

			INSERT INTO OrderGrid
			(
				AnnouncementId,
				LanguageType,
				VIDId,
				Category,
				ESTUPC,
				IVODUPC,
				--Vendor,
				FileType,
				RequestNumber,
				MPO,
				HALID,
				OrderStatus,
				OrderRequestDate,
				DeliveryDueDate,
				ActualDeliveryDate,
				--FirstStartDate,
				GreenlightDueDate,
				GreenlightValidatedbyDMDM,
				GreenlightSenttoPackaging,
				Notes,
				CustomerId,
				--CancellationDate,
				JObId
				--JobAction
			)
			SELECT 
				AnnouncementId,
				'Sub' AS LanguageType,
				VidId,
				Category,
				ESTUPC,
				IVODUPC,
				--Vendor,
				FileType,
				RequestNumber,
				MPO,
				HALID,
				OrderStatus,
				OrderRequestDate,
				--CONVERT(DATETIME,CASE WHEN OrderRequestDate='Pre-CAS' THEN '1900-01-02' WHEN OrderRequestDate='N/A' THEN '1900-01-01' ELSE OrderRequestDate  END) AS OrderRequestDate,
				DeliveryDueDate,
				--CONVERT(DATETIME,CASE WHEN DeliveryDueDate='Pre-CAS' THEN '1900-01-02' WHEN DeliveryDueDate='N/A' THEN '1900-01-01' ELSE DeliveryDueDate  END) AS DeliveryDueDate,
				ActualDeliveryDate,
				--CONVERT(DATETIME,CASE WHEN ActualDeliveryDate='Pre-CAS' THEN '1900-01-02' WHEN ActualDeliveryDate='N/A' THEN '1900-01-01' ELSE ActualDeliveryDate  END) AS ActualDeliveryDate,
				--FirstStartDate,
				GreenlightDueDate,
				--CONVERT(DATETIME,CASE WHEN GreenlightDueDate='Pre-CAS' THEN '1900-01-02' WHEN GreenlightDueDate='N/A' THEN '1900-01-01' ELSE GreenlightDueDate  END) AS GreenlightDueDate,
				GreenlightValidatedbyDMDM,
				--CONVERT(DATETIME,CASE WHEN GreenlightValidatedbyDMDM='Pre-CAS' THEN '1900-01-02' WHEN GreenlightValidatedbyDMDM='N/A' THEN '1900-01-01' ELSE GreenlightValidatedbyDMDM  END) AS GreenlightValidatedbyDMDM,
				GreenlightSenttoPackaging,
				--CONVERT(DATETIME,CASE WHEN GreenlightSenttoPackaging='Pre-CAS' THEN '1900-01-02' WHEN GreenlightSenttoPackaging='N/A' THEN '1900-01-01' ELSE GreenlightSenttoPackaging  END) AS GreenlightSenttoPackaging,
				Notes,
				CustomerId,
				--CancellationDate,
				JObId
				--JobAction
			FROM OrderGrid
			WHERE LanguageType = 'Sub & Audio'


			--Now OrderGrid.LanguageType = 'Sub & Audio' split into 'sub' and 'audio'
			UPDATE OrderGrid SET LanguageType = 'Audio' WHERE LanguageType = 'Sub & Audio'
			


END













GO
