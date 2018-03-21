CREATE PROCEDURE [dbo].[usp_Insert_Announcement_Channel_Format]
	 @JobId INT
AS
BEGIN
		SET NOCOUNT ON;

		--Insert first into CHannel_Format table where Key (AnnouncementId + Channel + Format) doesnot exist
		INSERT INTO Announceemnt_Channel_Format
		(
			AnnouncementId,
			Channel,
			Format,
			CustomerId,
			JObId,
			ChannelFormat
		)
		SELECT DISTINCT 
			ACF.AnnouncementId,
			ACF.SalesChannel, 
			ACF.Format,
			1,
			@JobId,
			(ACF.SalesChannel +  ACF.Format)
		FROM AnnouncementGrid A INNER JOIN
		(
			SELECT AG.Id AS AnnouncementId,
				AN.SalesChannel,
				AN.Format
			FROM AnnouncementGrid AG 
			INNER JOIN 
				(SELECT DISTINCT TitleVideoVersion, LocalEditRequired, Country, Language, SalesChannel, Format, ClientAvailStatus, ClientStart 
					FROM Announcements
				) AN   
				ON AG.VideoVersion = AN.TitleVideoVersion
					AND (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '' THEN 'No' ELSE AG.LocalEdit END) = AN.LocalEditRequired 
					AND AG.TerritoryId = AN.Country
					AND AG.LanguageId = AN.Language 
			LEFT JOIN Announceemnt_Channel_Format F ON  F.AnnouncementId = AG.Id
			WHERE F.AnnouncementId IS NULL
		) ACF ON ACF.AnnouncementId = A.Id 
	
	
		--Update status and Dates in Channel_Format table
		UPDATE ACF
		SET ACF.ClientAvailStatus = AN.ClientAvailSTatus,
			ACF.ClientStartDate = AN.ClientStart,
			JobId = @JobId
		FROM Announceemnt_Channel_Format ACF
		INNER JOIN AnnouncementGrid AG ON ACF.AnnouncementId = AG.Id
		INNER JOIN Announcements AN 
				ON AG.VideoVersion = AN.TitleVideoVersion
				AND (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '' THEN 'No' ELSE AG.LocalEdit END) = AN.LocalEditRequired 
				AND AG.TerritoryId = AN.Country
				AND AG.LanguageId = AN.Language
				AND ACF.Channel = AN.SalesChannel
				AND ACF.Format = AN.Format
		
			


END









