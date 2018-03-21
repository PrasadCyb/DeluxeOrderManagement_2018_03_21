CREATE PROCEDURE [dbo].[usp_Insert_OrderGrid]
	-- Add the parameters for the stored procedure here
	@JobID INT
AS
BEGIN
			-- SET NOCOUNT ON added to prevent extra result sets from
			-- interfering with SELECT statements.
			SET NOCOUNT ON;


			/* Insert 'Sub' and 'Audio' records only */
			INSERT INTO OrderGrid
			(
				AnnouncementId,
				LanguageType,
				OrderStatus,
				Category,
				CustomerId,
				JObId
			)
			SELECT 
				AOG.AnnouncementId,
				AOG.LanguageType,
				'New',
				AOG.Category,
				1 As CustomerId,
				@JobId AS JobId
			FROM AnnouncementGrid A INNER JOIN 
				(
					SELECT AG.ID AS AnnouncementId, 
						ANN.LanguageType,
						ANN.Category
					FROM AnnouncementGrid AG 
					INNER JOIN (SELECT TitleVideoVersion, LocalEditRequired, Country, Language, LanguageType, MAX(TitleCategory)  AS Category
								FROM Announcements
								GROUP BY TitleVideoVersion, LocalEditRequired, Country, Language, LanguageType
								) ANN
						ON AG.VideoVersion = ANN.TitleVideoVersion
						AND (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '' THEN 'No' ELSE AG.LocalEdit END) = ANN.LocalEditRequired 
						AND AG.TerritoryId = ANN.Country
						AND AG.LanguageId = ANN.Language
					LEFT JOIN  OrderGrid OG ON AG.Id = OG.AnnouncementId AND ANN.LanguageType = OG.LanguageType
					WHERE OG.AnnouncementId IS NULL
					--order by AG.Id 
				) AOG ON AOG.AnnouncementId = A.Id 

			/*Cancel any Order if All statusses either blan/Null OR Cancelled*/
			--First Insert into tmp table

			IF OBJECT_ID(N'Tmp_CANCELREP') IS NOT NULL
				DROP TABLE Tmp_CANCELREP

			SELECT ACF.AnnouncementId
			INTO #Tmp_Cancel
			FROM 
				(
					SELECT AnnouncementId, COUNT(AnnouncementId) AS Count
					FROM (SELECT DISTINCT AnnouncementId, Channel, Format 
							FROM Announceemnt_Channel_Format 
					WHERE ClientAvailStatus = 'Cancelled' OR ClientAvailStatus = '' OR ClientAvailStatus IS NULL) ACF
					GROUP BY AnnouncementId
				) ACF INNER JOIN 
				(
					SELECT AnnouncementId, COUNT(AnnouncementId) AS Count
					FROM (SELECT DISTINCT AnnouncementId, Channel, Format FROM Announceemnt_Channel_Format ) ACF
					GROUP BY AnnouncementId
				) CF ON ACF.AnnouncementId = CF.AnnouncementId AND CF.Count = ACF.Count
				

			UPDATE OG
			SET OG.OrderSTatus = 'Cancelled',
					--CancellationDate = GetDate(),
					JobId = @JobId
			FROM OrderGrid OG INNER JOIN AnnouncementGrid AG ON AG.Id = OG.AnnouncementId
				INNER JOIN Announceemnt_Channel_Format ACF ON AG.Id = ACF.AnnouncementId
			WHERE ACF.JobId = @JobId AND ACF.AnnouncementId IN 
			(
				SELECT AnnouncementId FROM #Tmp_Cancel
			)

			--Set Cancellation date
			UPDATE AG
			SET AG.CancellationDate = GetDate(),
					JobId = @JobId
			FROM AnnouncementGrid AG  INNER JOIN OrderGrid OG ON AG.Id = OG.AnnouncementId
			WHERE OG.JobId = @JobId AND AG.Id IN 
			(
				SELECT AnnouncementId FROM #Tmp_Cancel
			)
			
			
END







