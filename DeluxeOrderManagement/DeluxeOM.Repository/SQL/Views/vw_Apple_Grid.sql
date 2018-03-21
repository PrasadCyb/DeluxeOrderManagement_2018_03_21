CREATE VIEW [dbo].[vw_Apple_Grid]
AS
		SELECT 
			A.Id As AnnouncementId,
			O.Id AS OrderId, 
			A.VideoVersion , 
			(CASE WHEN A.LocalEdit IS NULL OR A.LocalEdit = '' OR A.LocalEdit = 'No' THEN 'No' ELSE 'Yes' END) AS LocalEdit, 
			A.LanguageId , 
			L.Name As Language,
			A.TerritoryId, 
			T.WbTerritory AS Territory,
			O.LanguageType
		From OrderGrid  O INNER JOIN AnnouncementGrid A ON A.Id = O.AnnouncementId
		INNER JOIN Announceemnt_Channel_Format C ON C.AnnouncementId = A.Id
		INNER JOIN [Language] L ON A.LanguageId = L.Id
		INNER JOIN Territory T ON T.Id = A.TerritoryId

GO