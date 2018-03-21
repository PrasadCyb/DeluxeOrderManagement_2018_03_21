CREATE PROCEDURE [dbo].[usp_Insert_Announcements] 
	@JobID INT
-- EXEC [usp_Insert_Announcements]
-- PROCEDURE WILL BE USED TO POPULATE Announcements AND UPDATE AnnouncementGrid TABLE
AS
BEGIN
			
		/*Insert into VID which are not exisst in VID table*/
		INSERT INTO VID
		(
			VIDStatus,
			TitleName,
			MPM,
			VideoVersion,
			EditName,
			VersionEIDR,
			CreatedOn,
			CreatedBy,
			ModifiedOn,
			ModifiedBy
		)
		SELECT DISTINCT 
		MAX('PRIMARY') AS VIDSTatus,
		MAX(ANN.[Global Title]) AS Title, 
		MAX(ANN.[Title - MPM]) AS MPM, 
		AV.[Title - Video Version], 
		AV.[Local Edit Required], 
		MAX(ANN.[Title - Abstract EIDR]) AS TitleAbstractEIDR,
		GETDATE(),
		'Announcement',
		GETDATE(),
		'Announcement'
		FROM Announcements_Staging ANN INNER JOIN 
			(SELECT DISTINCT A.[Title - Video Version], 
				(CASE WHEN A.[Local Edit Required] = '' OR A.[Local Edit Required] IS NULL THEN 'No' ELSE A.[Local Edit Required] END) AS [Local Edit Required]
			FROM Announcements_Staging A 
			EXCEPT 
			(SELECT DISTINCT
				VideoVersion, 
				(CASE WHEN (EditName IS NULL OR EditName = '' OR EditName = 'No') THEN 'No' ELSE 'Yes' END)
			FROM VID)) AV
		ON ANN.[Title - Video Version] = AV.[Title - Video Version] 
		AND  (CASE WHEN ANN.[Local Edit Required] = '' OR ANN.[Local Edit Required] IS NULL THEN 'No' ELSE ANN.[Local Edit Required] END)= AV.[Local Edit Required]
		GROUP BY AV.[Title - Video Version], 
		AV.[Local Edit Required]


		/* Language */
		--SELECT count(1) from Language
		INSERT INTO Language
		(
			Name
		)
		SELECT DISTINCT [Language] FROM announcements_staging 
		WHERE [Language] NOT IN (SELECT Name FROM Language)
		
		
		/* Territory */
		
		INSERT INTO Territory
		(
			WBTerritory,
			AppleTerritory
		)
			SELECT 
			DISTINCT AGS.Country, AGS.Country
			FROM announcements_staging AGS 
			WHERE AGS.Country NOT IN (SELECT WBTerritory FROM Territory UNION SELECT AppleTerritory FROM Territory) 

			
			-- Announcements TABLE WILL BE TRUNCATED AND POPULATED EVERYTIME FROM  [announcements_staging] TABLE -- WE EXPECT FULL REPLACE DATA IN RAW FILES PROVIDED FOR Announcements
             Truncate table [Announcements]
              --select * from [Announcements]
              INSERT INTO [Announcements]
              (
              ChangeStatus
              ,ChangedFields
              ,ChangeContext
              ,LocalTitle
              ,GlobalTitle
              ,TitleContentType
              ,Country
              ,Language
              ,LanguageType
              ,SalesChannel
              ,Format
              ,HDR
              ,ClientStart
              ,ClientEnd
              ,ClientAvailStatus
              ,ECAvailable
              ,SuppressionEndDate
              ,TitleCategory
              ,PriceType
              ,PriceTier
              ,WSCost
              ,StorefrontCurrency
              ,LocalDataTheatricalReleaseDt
              ,LocalDVDReleaseDate
              ,LocalDataRating
              ,RatingReason
              ,LocalDataDVS
              ,TitleMPM
              ,TitleVideoVersion
              ,LocalEditRequired
              ,TitleAbstractEIDR
              ,LocalDataEditEIDR
              ,LocalDataUVPublishDate
              ,LocalDataALID
              ,LocalDataCID
              ,ClientAvailID
              ,AccountName
              ,Company
              ,AvailNotes
              ,ChannelFormat
              )
              SELECT 
                     [Change Status]
                     ,[Changed Fields]
                     ,[Change Context]
                     ,[Local Title: Local Title]
                     ,[Global Title]
                     ,[Title - Content Type]
                     ,T.Id
                     ,L.Id
                  ,[Language Type]
                     ,[Sales Channel]
                     ,REPLACE ([Format] , ' ', '' )
                     ,[HDR]
                     ,[Client Start]
                     ,[Client End]
                     ,[Client Avail Status]
       ,[EC Available]
                     ,[Suppression End Date]
                     ,[Title Category]
                     ,[PriceType]
                     ,[Price Tier]
                     ,[WS Cost]
                     ,[Storefront - Currency]
                     ,[Local Data -Theatrical Release Dt]
                     ,[Local DVD Release Date]
                     ,[Local Data - Rating]
                     ,[RatingReason]
                     ,[Local Data - DVS]
                     ,[Title - MPM]
                     ,[Title - Video Version]
                     ,CASE WHEN (([Local Edit Required] IS NULL) OR ([Local Edit Required]  = '')) THEN 'No' ELSE [Local Edit Required] END AS [Local Edit Required]
                     ,[Title - Abstract EIDR]
                     ,[Local Data - Edit EIDR]
                     ,[Local Data - UV Publish Date]
                     ,[Local Data - ALID]
                     ,[Local Data - CID]
                     ,[Client Avail ID]
                     ,[Account: Account Name]
                     ,C.Id
                     ,[Avail Notes]
                     ,REPLACE ([Sales Channel] + [Format] , ' ', '' )
              FROM [announcements_staging] A
              INNER JOIN Territory T ON A.Country = T.WBTerritory
              INNER JOIN Language L ON A.Language = L.Name
              INNER JOIN Customer C ON C.Name = 'Warner Bros.'
			  WHERE A.[Language Type] IN ('Sub', 'Audio')
              

			  INSERT INTO [Announcements]
              (
              ChangeStatus
              ,ChangedFields
              ,ChangeContext
              ,LocalTitle
              ,GlobalTitle
              ,TitleContentType
              ,Country
              ,Language
              ,LanguageType
              ,SalesChannel
              ,Format
              ,HDR
              ,ClientStart
              ,ClientEnd
              ,ClientAvailStatus
              ,ECAvailable
              ,SuppressionEndDate
              ,TitleCategory
              ,PriceType
              ,PriceTier
              ,WSCost
              ,StorefrontCurrency
              ,LocalDataTheatricalReleaseDt
              ,LocalDVDReleaseDate
              ,LocalDataRating
              ,RatingReason
              ,LocalDataDVS
              ,TitleMPM
              ,TitleVideoVersion
              ,LocalEditRequired
              ,TitleAbstractEIDR
              ,LocalDataEditEIDR
              ,LocalDataUVPublishDate
              ,LocalDataALID
              ,LocalDataCID
              ,ClientAvailID
              ,AccountName
              ,Company
              ,AvailNotes
              ,ChannelFormat
              )
              SELECT 
                     [Change Status]
                     ,[Changed Fields]
                     ,[Change Context]
                     ,[Local Title: Local Title]
                     ,[Global Title]
                     ,[Title - Content Type]
                     ,T.Id
                     ,L.Id
                     ,'Sub'
                     ,[Sales Channel]
                     ,REPLACE ([Format] , ' ', '' )
                     ,[HDR]
                     ,[Client Start]
                     ,[Client End]
                     ,[Client Avail Status]
                     ,[EC Available]
                     ,[Suppression End Date]
                     ,[Title Category]
                     ,[PriceType]
                     ,[Price Tier]
                     ,[WS Cost]
                     ,[Storefront - Currency]
                     ,[Local Data -Theatrical Release Dt]
                     ,[Local DVD Release Date]
                     ,[Local Data - Rating]
                     ,[RatingReason]
                     ,[Local Data - DVS]
                     ,[Title - MPM]
                     ,[Title - Video Version]
                     ,CASE WHEN (([Local Edit Required] IS NULL) OR ([Local Edit Required]  = '')) THEN 'No' ELSE [Local Edit Required] END AS [Local Edit Required]
                     ,[Title - Abstract EIDR]
                     ,[Local Data - Edit EIDR]
                     ,[Local Data - UV Publish Date]
                     ,[Local Data - ALID]
                     ,[Local Data - CID]
                     ,[Client Avail ID]
                     ,[Account: Account Name]
                     ,C.Id
                     ,[Avail Notes]
                     ,REPLACE ([Sales Channel] + [Format] , ' ', '' )
              FROM [announcements_staging] A
              INNER JOIN Territory T ON A.Country = T.WBTerritory
              INNER JOIN Language L ON A.Language = L.Name
              INNER JOIN Customer C ON C.Name = 'Warner Bros.'
			  WHERE A.[Language Type] IN ('Sub & Audio')


			  INSERT INTO [Announcements]
              (
              ChangeStatus
              ,ChangedFields
              ,ChangeContext
              ,LocalTitle
              ,GlobalTitle
              ,TitleContentType
              ,Country
              ,Language
              ,LanguageType
              ,SalesChannel
              ,Format
              ,HDR
              ,ClientStart
              ,ClientEnd
              ,ClientAvailStatus
              ,ECAvailable
              ,SuppressionEndDate
              ,TitleCategory
              ,PriceType
              ,PriceTier
              ,WSCost
              ,StorefrontCurrency
              ,LocalDataTheatricalReleaseDt
              ,LocalDVDReleaseDate
              ,LocalDataRating
              ,RatingReason
              ,LocalDataDVS
              ,TitleMPM
              ,TitleVideoVersion
              ,LocalEditRequired
              ,TitleAbstractEIDR
              ,LocalDataEditEIDR
              ,LocalDataUVPublishDate
              ,LocalDataALID
              ,LocalDataCID
              ,ClientAvailID
              ,AccountName
              ,Company
              ,AvailNotes
              ,ChannelFormat
              )
              SELECT 
                     [Change Status]
                     ,[Changed Fields]
                     ,[Change Context]
                     ,[Local Title: Local Title]
                     ,[Global Title]
                     ,[Title - Content Type]
                     ,T.Id
                     ,L.Id
                     ,'Audio'
                     ,[Sales Channel]
                     ,REPLACE ([Format] , ' ', '' )
                     ,[HDR]
                     ,[Client Start]
                     ,[Client End]
                     ,[Client Avail Status]
                     ,[EC Available]
                     ,[Suppression End Date]
                     ,[Title Category]
                     ,[PriceType]
                     ,[Price Tier]
                     ,[WS Cost]
                     ,[Storefront - Currency]
                     ,[Local Data -Theatrical Release Dt]
                     ,[Local DVD Release Date]
                     ,[Local Data - Rating]
                     ,[RatingReason]
                     ,[Local Data - DVS]
                     ,[Title - MPM]
                     ,[Title - Video Version]
                     ,CASE WHEN (([Local Edit Required] IS NULL) OR ([Local Edit Required]  = '')) THEN 'No' ELSE [Local Edit Required] END AS [Local Edit Required]
                     ,[Title - Abstract EIDR]
                     ,[Local Data - Edit EIDR]
                     ,[Local Data - UV Publish Date]
                     ,[Local Data - ALID]
                     ,[Local Data - CID]
         ,[Client Avail ID]
                     ,[Account: Account Name]
                     ,C.Id
                     ,[Avail Notes]
                     ,REPLACE ([Sales Channel] + [Format] , ' ', '' ) 
              FROM [announcements_staging] A
              INNER JOIN Territory T ON A.Country = T.WBTerritory
              INNER JOIN Language L ON A.Language = L.Name
  INNER JOIN Customer C ON C.Name = 'Warner Bros.'
			  WHERE A.[Language Type] IN ('Sub & Audio')



              -- A TEMP TABLE WILL BE USED TO HOLD AND UPDATE RESPECTIVE AnnouncementGrid TABLE COLUMNS.

              IF OBJECT_ID(N'tempdb..#AllAnnouncements') IS NOT NULL
              DROP TABLE #AllAnnouncements

              SELECT t.ChangeStatus,
				t.ChangedFields,
				t.ChangeContext,
				t.LocalTitle,
				t.GlobalTitle,
				t.TitleContentType,
				t.Country,
				t.Language,
				t.LanguageType,
				t.SalesChannel,
				t.Format,
				t.HDR,
				t.ClientStart,
				t.ClientEnd,
				t.ClientAvailStatus,
				t.ECAvailable,
				t.SuppressionEndDate,
				t.TitleCategory,
				t.PriceType,
				t.PriceTier,
				t.WSCost,
				t.StorefrontCurrency,
				t.LocalDataTheatricalReleaseDt,
				t.LocalDVDReleaseDate,
				t.LocalDataRating,
				t.RatingReason,
				t.LocalDataDVS,
				t.TitleMPM,
				t.TitleVideoVersion,
				t.LocalEditRequired,
				t.TitleAbstractEIDR,
				t.LocalDataEditEIDR,
				t.LocalDataUVPublishDate,
				t.LocalDataALID,
				t.LocalDataCID,
				t.ClientAvailID,
				t.AccountName,
				t.Company,
				t.AvailNotes,
				REPLACE ([SalesChannel] + [Format] , ' ', '' )  AS ChannelFormat
              INTO #AllAnnouncements
              FROM Announcements t
                     LEFT JOIN AnnouncementGrid u ON -- CASE WHEN (u.Title = t.GlobalTitle) THEN t.GlobalTitle ELSE and
                     u.VideoVersion = t.TitleVideoVersion
                     and u.LocalEdit = t.LocalEditRequired
                     and u.TerritoryId = t.Country
                     and u.LanguageId = t.Language 
              WHERE u.VideoVersion IS NULL

             
              -- INSERTING MISSING DATA FROM Announcements INTO AnnouncementGrid TABLE

			  --UPDATE AnnouncementGrid SET JobAction = NULL

			  --Update OrderGrid SET JobAction = NULL
			  
              INSERT INTO AnnouncementGrid 
			  (
					Title,
					VideoVersion,
					LocalEdit,
					TerritoryId,
					LanguageId,
					CustomerId,
					JObId,
					FirstAnnouncedDate,
					LastAnnouncedDate
			  )
              SELECT 
				MAX(GlobalTitle) AS GlobalTitle, 
				TitleVideoVersion, 
				LocalEditRequired, 
				Country, 
				Language, 
				c.id,
				@JobId,
				GETDATE(), 
				GETDATE() 
              FROM  #AllAnnouncements
                     left join Customer C ON C.name = 'Warner Bros.'
              GROUP BY TitleVideoVersion, LocalEditRequired, Country, Language, c.id

			  --Update title with GlobalTitles
			  UPDATE AG
				SET Title = A.GlobalTitle
				FROM AnnouncementGrid AG
				INNER JOIN (SELECT DISTINCT GlobalTitle, TitleVideoVersion, LocalEditRequired, Country, Language FROM Announcements) A ON A.TitleVideoVersion = AG.VideoVersion
					AND (CASE WHEN A.LocalEditRequired IS NULL OR A.LocalEditRequired = '' OR A.LocalEditRequired = 'No' THEN 'No' ELSE 'Yes' END) = (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '' OR AG.LocalEdit = 'No' THEN 'No' ELSE 'Yes' END) 
					AND A.Country = AG.TerritoryId
					AND A.Language = AG.LanguageId

			--UPDATE data from AnnouncementGrid TABLE'S Channel/Format Statusses and Dates
            --EXEC [usp_UPD_AnnouncementGrid] @JobId
			EXEC usp_Insert_Announcement_Channel_Format @JobId

			/* Insert Into OrderGrid For New VideoVersion, LocalEdit, Territory, Language */
			--Not Necessary as below query will manage Inserted/Updated in AnnouncementGrid
			Exec usp_Insert_OrderGrid @JobId

			 --Update status as Ordered once entry exists in Pipeline Order
			--EXEC [usp_UpdateOG_StatusOrdered]

			 --Update status as Delivered once entry exists in AppleQC
			EXEC [usp_UpdateOG_StatusDelivered] @JobId

END















