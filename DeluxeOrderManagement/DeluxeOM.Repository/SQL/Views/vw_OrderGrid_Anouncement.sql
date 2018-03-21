CREATE VIEW [dbo].[vw_OrderGrid_Anouncement]
AS
		SELECT 
			A.Id As AnnouncementId,
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
GO