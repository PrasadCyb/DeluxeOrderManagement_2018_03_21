
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UPD_AnnouncementGrid] @JobId INT
	-- EXEC [usp_UPD_AnnouncementGrid]
	-- PROCEDURE WILL BE USED TO UPDATE AnnouncementGrid TABLE FROM PIVOTED DATA FROM Announcements
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- A TEMP TABLE TO PIVOT DATA FROM ROWS TO COLUMN FOR RESPECTIVE SALESCHANNEL+FORMAT ROW TO AnnouncementGrid COLUMNS

		IF OBJECT_ID(N'tempdb..#Temp') IS NOT NULL
		DROP TABLE #Temp

		SELECT Country, Language, TitleVideoVersion,  LocalEditRequired,  MAX(POESTHD) AS POESTHDStatus, MAX(POESTSD) AS POESTSDStatus, 
		MAX([ESTSD]) AS ESTSDStatus, MAX([ESTHD]) AS ESTHDStatus, MAX([VODSD]) AS VODSDStatus, MAX([VODHD]) AS VODHDStatus, 
		MAX(EST4KUHD) AS EST4KUHDStatus, MAX(POEST4KUHD) As POEST4KUHDStatus, MAX(VOD4KUHD) AS VOD4KUHDStatus,
		ChannelFormat_U, ClientStart, ClientEnd, MAX(SalesChannel) AS SalesChannel, MAX([Format]) AS [Format]
		INTO #Temp
		FROM
		(
			SELECT GlobalTitle,
		Country,
		Language, 
		TitleVideoVersion, 
		LocalEditRequired, ChannelFormat, ClientAvailStatus, ChannelFormat as ChannelFormat_U, ClientStart, ClientEnd, SalesChannel, [Format]
			  FROM [dbo].[Announcements]
		) AS SourceTable PIVOT(max(ClientAvailStatus) FOR ChannelFormat IN([VODSD], [VODHD], [POESTSD], [POESTHD], [ESTHD], [ESTSD], [EST4KUHD], [VOD4KUHD], [POEST4KUHD])) AS PivotTable1
		GROUP BY Country, Language, TitleVideoVersion,  LocalEditRequired, ChannelFormat_U, ClientStart, ClientEnd

		-- select * from #Temp where TitleVideoVersion = '6000000370' and country = 6 and Language =11 

		IF OBJECT_ID('tempdb..#UniqueData') IS NOT NULL
		DROP TABLE #UniqueData

		SELECT  
		Country,
		Language, 
		TitleVideoVersion, 
		LocalEditRequired, MAX(POESTHDStatus) AS POESTHDStatus, MAX(POESTSDStatus) AS POESTSDStatus, MAX(ESTSDStatus) AS ESTSDStatus,
		MAX(ESTHDStatus) AS ESTHDStatus, MAX(VODSDStatus) AS VODSDStatus, MAX(VODHDStatus) AS VODHDStatus, 
		MAX(EST4KUHDStatus) AS EST4KUHDStatus, MAX(POEST4KUHDStatus) As POEST4KUHDStatus, MAX(VOD4KUHDStatus) AS VOD4KUHDStatus,
		MAX(ClientStart) AS ClientStart, MAX(ClientEnd) AS ClientEnd,
		MAX(CASE WHEN SalesChannel = 'POEST' and [Format] = 'SD' THEN ClientStart ELSE NULL END) AS POESTSDDate,
		MAX(CASE WHEN SalesChannel = 'POEST' and [Format] = 'HD' THEN ClientStart ELSE NULL END) AS POESTHDDate,
		MAX(CASE WHEN SalesChannel = 'EST' and [Format] = 'SD' THEN ClientStart ELSE NULL END) AS ESTSDDate,
		MAX(CASE WHEN SalesChannel = 'EST' and [Format] = 'HD' THEN ClientStart ELSE NULL END) AS ESTHDDate,
		MAX(CASE WHEN SalesChannel = 'VOD' and [Format] = 'SD' THEN ClientStart ELSE NULL END) AS VODSDDate,
		MAX(CASE WHEN SalesChannel = 'VOD' and [Format] = 'HD' THEN ClientStart ELSE NULL END) As VODHDDate,
		MAX(CASE WHEN SalesChannel = 'POEST' and [Format] = '4KUHD' THEN ClientStart ELSE NULL END) As POEST4KUHDDate,
		MAX(CASE WHEN SalesChannel = 'EST' and [Format] = '4KUHD' THEN ClientStart ELSE NULL END) As EST4KUHDDate,
		MAX(CASE WHEN SalesChannel = 'VOD' and [Format] = '4KUHD' THEN ClientStart ELSE NULL END) As VOD4KUHDDate
		INTO #UniqueData
		FROM #Temp t
		GROUP BY Country, Language, TitleVideoVersion,  LocalEditRequired

		--select * from #UniqueData where TitleVideoVersion = '6000000370' and Country = 6
		--select * from #Temp where TitleVideoVersion = '6000107445' and Country = 104

		UPDATE u
		SET 
			u.POESTSDStatus = t.POESTSDStatus,
			u.POESTHDStatus = t.POESTHDStatus,
			u.ESTSDStatus = t.ESTSDStatus,
			u.ESTHDStatus = t.ESTHDStatus ,
			u.VODSDStatus = t.VODSDStatus,
			u.VODHDStatus = t.VODHDStatus
			,u.POESTSDDate = t.POESTSDDate,
			u.POESTHDDate = t.POESTHDDate,
			u.ESTSDDate = t.ESTSDDate,
			u.ESTHDDate = t.ESTHDDate,
			u.VODSDDate = t.VODSDDate,
			u.VODHDDate = t.VODHDDate,
			u.POEST4KUHDDate = t.POEST4KUHDDate, 
			u.EST4KUHDDate = t.EST4KUHDDate, 
			u.VOD4KUHDDate = t.VOD4KUHDDate,
			LastAnnouncementDate = GETDATE()
			,u.JobID = @JobId
		--u.JobAction = CASE WHEN u.StatusFlag = 'Inserted' THEN u.StatusFlag ELSE 'Updated' END
		FROM AnnouncementGrid u 
			INNER JOIN #UniqueData t
			ON u.VideoVersion = t.TitleVideoVersion
				and u.LocalEdit = t.LocalEditRequired
				and u.TerritoryId = t.Country
				and u.LanguageId = t.Language

		/*
		UPDATE u
		SET 
		u.AnnouncementPreOrderSDDate = CASE WHEN t.SalesChannel = 'POEST' and t.[Format] = 'SD' THEN t.ClientStart ELSE u.AnnouncementPreOrderSDDate END,
		u.AnnouncementPreOrderHDDate = CASE WHEN t.SalesChannel = 'POEST' and t.[Format] = 'HD' THEN t.ClientStart ELSE u.AnnouncementPreOrderHDDate END,
		u.AnnouncementESTSDStart = CASE WHEN t.SalesChannel = 'EST' and t.[Format] = 'SD' THEN t.ClientStart ELSE u.AnnouncementESTSDStart END,
		u.AnnouncementESTHDStart = CASE WHEN t.SalesChannel = 'EST' and t.[Format] = 'HD' THEN t.ClientStart ELSE u.AnnouncementESTHDStart END,
		u.AnnouncementVODSDStart = CASE WHEN t.SalesChannel = 'VOD' and t.[Format] = 'SD' THEN t.ClientStart ELSE u.AnnouncementVODSDStart END,
		u.AnnouncementVODHDStart = CASE WHEN t.SalesChannel = 'VOD' and t.[Format] = 'HD' THEN t.ClientStart ELSE u.AnnouncementVODHDStart END
		FROM AnnouncementGrid u INNER JOIN Announcements t
		ON u.Title = t.GlobalTitle
		and u.VideoVersion = t.TitleVideoVersion
		and u.LocalEdit = t.LocalEditRequired
		and u.TerritoryId = t.Country
		and u.LanguageId = t.Language 
		*/
		-- UPDATE STATEMENT TO UPDATE AnnouncementGird TABLE WITH PIVOTED DATA FROM Announcements

		UPDATE u
		SET 
			u.POESTSDStatus = CASE WHEN t.ChannelFormat_U = 'POESTSD' THEN t.POESTSDStatus ELSE u.POESTSDStatus END,
			u.POESTHDStatus = CASE WHEN t.ChannelFormat_U = 'POESTHD' THEN t.POESTHDStatus ELSE u.POESTHDStatus END,
			u.ESTSDStatus = CASE WHEN t.ChannelFormat_U = 'ESTSD' THEN t.ESTSDStatus ELSE u.ESTSDStatus END,
			u.ESTHDStatus = CASE WHEN t.ChannelFormat_U = 'ESTHD' THEN t.ESTHDStatus ELSE u.ESTHDStatus END ,
			u.VODSDStatus = CASE WHEN t.ChannelFormat_U = 'VODSD' THEN t.VODSDStatus ELSE u.VODSDStatus END,
			u.VODHDStatus = CASE WHEN t.ChannelFormat_U = 'VODHD' THEN t.VODHDStatus ELSE u.VODHDStatus END,
			u.POEST4KUHDStatus = CASE WHEN t.ChannelFormat_U = 'POEST4KUHD' THEN t.POEST4KUHDStatus ELSE u.POEST4KUHDStatus END,
			u.EST4KUHDStatus = CASE WHEN t.ChannelFormat_U = 'EST4KUHD' THEN t.EST4KUHDStatus ELSE u.EST4KUHDStatus END,
			u.VOD4KUHDStatus = CASE WHEN t.ChannelFormat_U = 'VOD4KUHD' THEN t.VOD4KUHDStatus ELSE u.VOD4KUHDStatus END,
			u.POESTSDDate = CASE WHEN t.ChannelFormat_U = 'POESTSD' THEN t.ClientStart ELSE u.POESTSDDate END,
			u.POESTHDDate = CASE WHEN t.ChannelFormat_U = 'POESTHD' THEN t.ClientStart ELSE u.POESTHDDate END,
			u.POEST4KUHDDate = CASE WHEN t.ChannelFormat_U = 'POEST4KUHD' THEN t.ClientStart ELSE u.POEST4KUHDDate END,
			u.ESTSDDate = CASE WHEN t.ChannelFormat_U = 'ESTSD' THEN t.ClientStart ELSE u.ESTSDDate END,
			u.ESTHDDate = CASE WHEN t.ChannelFormat_U = 'ESTHD' THEN t.ClientStart ELSE u.ESTHDDate END,
			u.EST4KUHDDate = CASE WHEN t.ChannelFormat_U = 'EST4KUHD' THEN t.ClientStart ELSE u.EST4KUHDDate END,
			u.VODSDDate = CASE WHEN t.ChannelFormat_U = 'VODSD' THEN t.ClientStart ELSE u.VODSDDate END,
			u.VODHDDate = CASE WHEN t.ChannelFormat_U = 'VODHD' THEN t.ClientStart ELSE u.VODHDDate END,
			u.VOD4KUHDDate = CASE WHEN t.ChannelFormat_U = 'VOD4KUHD' THEN t.ClientStart ELSE u.VOD4KUHDDate END,
			LastAnnouncementDate = GETDATE()
			,u.JobID = @JobId
			--u.StatusFlag = CASE WHEN u.StatusFlag = 'Inserted' THEN u.StatusFlag ELSE 'Updated' END
			FROM AnnouncementGrid u INNER JOIN #Temp t
			ON u.VideoVersion = t.TitleVideoVersion
			and u.LocalEdit = t.LocalEditRequired
			and u.TerritoryId = t.Country
			and u.LanguageId = t.Language 
			and u.ChannelFormat = t.ChannelFormat_U


END





