CREATE PROCEDURE [dbo].[usp_UpdateOG_StatusDelivered] 
	@JobId INT	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE OG
		SET OG.OrderStatus = 'Complete',
		OG.JobId = @JobId 
		FROM OrderGrid OG
		WHERE OG.ID IN 
		(
			SELECT DISTINCT AG.OrderId
			FROM 
			(
				SELECT 
					A.Id,
					O.Id AS OrderId, 
					A.VideoVersion , 
					(CASE WHEN A.LocalEdit IS NULL OR A.LocalEdit = '' OR A.LocalEdit = 'No' THEN 'No' ELSE 'Yes' END) AS LocalEdit, 
					A.LanguageId , 
					A.TerritoryId, 
					O.LanguageType
				From OrderGrid  O LEFT JOIN  AnnouncementGrid A ON A.Id = O.AnnouncementId
				INNER JOIN Announcements AN 
					ON AN.TitleVideoVersion = A.VideoVersion
					AND AN.LocalEditRequired = (CASE WHEN A.LocalEdit IS NULL OR A.LocalEdit = '' OR A.LocalEdit = 'No' THEN 'No' ELSE 'Yes' END)
					AND AN.Country = A.TerritoryId
					AND AN.Language = A.LanguageId
					AND AN.LanguageType = O.LanguageType
				WHERE ISNULL(O.OrderStatus, 'OS') <> 'Complete'
			 ) AG 
			INNER JOIN vw_VID_QCReport VQ 
					ON VQ.VideoVersion = ISNULL(AG.VideoVersion, 'NULLVAL') 
					AND VQ.LocalEdit = AG.LocalEdit
					AND VQ.LanguageId = AG.LanguageId 
					AND VQ.TerritoryId = AG.TerritoryId
					AND dbo.fnGetLanguageType(VQ.ComponentType, VQ.LanguageName, VQ.AppleTerritory)  = AG.LanguageType
					
		)
END




