CREATE VIEW vw_Announcement_QCReport
AS
	SELECT  V.VideoVersion, 
		V.LocalEdit, 
		--L.Locale, 
		A.[Language] AS LanguageId, 
		--T.WBTerritory, 
		--T.Id AS TerritoryId,
		A.TerritoryId ,
		QC.ComponentType,
		QC.QAStatus
		FROM   
			(
				SELECT DISTINCT TitleVideoVersion AS VideoVersion, 
					(CASE WHEN LocalEditRequired IS NULL OR LocalEditRequired = '' OR LocalEditRequired = 'No' THEN 'No' ELSE 'Yes' END) AS LocalEdit, 
					Country AS TerritoryId,
					[Language], 
					(CASE WHEN LanguageType = 'Sub & Audio'  THEN 'SUBTITLES,AUDIO'
					WHEN LanguageType = 'Sub' THEN 'SUBTITLES'
					ELSE LanguageType  END) AS LanguageType 
				FROM Announcements
			) A INNER JOIN
		(SELECT DISTINCT VideoVersion, (CASE WHEN EditName IS NULL OR EditName = '' OR EditName = 'No' Then 'No' ELSE 'Yes' END) AS LocalEdit,
			VendorId,
			AppleId
			FROM VID 
			WHERE (VendorId IS NOT NULL OR VendorId <> '') AND (AppleId IS NOT NULL OR AppleId <> '')
		) V  
		ON A.VideoVersion = V.VideoVersion AND A.LocalEdit = V.LocalEdit
		INNER JOIN Territory T ON T.Id = A.TerritoryId
		INNER JOIN [Language] L ON A.Language = L.Id
		INNER JOIN QCReport  QC  ON V.VendorId = QC.VendorId AND V.AppleId = QC.AppleId
			AND A.LanguageType LIKE '%' + QC.ComponentType + '%' --Need to split for BOTH
			--AND QC.TERRITORY like  '%' + T.CountryCode + '%' 
			AND L.Id = QC.LanguageId
			INNER JOIN PriceReport PR ON PR.AppleId = QC.AppleId AND PR.VendorId = QC.VendorId  AND PR.TerritoryId = A.TerritoryId