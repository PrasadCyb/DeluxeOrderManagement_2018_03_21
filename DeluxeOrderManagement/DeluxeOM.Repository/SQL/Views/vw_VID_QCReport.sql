CREATE VIEW [dbo].[vw_VID_QCReport]
AS
SELECT  V.VideoVersion, 
		V.LocalEdit, 
		V.VendorId,
		QC.AppleId,
		QC.LanguageId, 
		QC.LanguageName, 
		PR.TerritoryId ,
		PR.AppleTerritory, 
		QC.ComponentType,
		QC.ImportedDate
FROM   
		(SELECT DISTINCT VideoVersion, 
			(CASE WHEN EditName IS NULL OR EditName = '' OR EditName = 'No' Then 'No' ELSE 'Yes' END) AS LocalEdit,
			VendorId
			--AppleId
			FROM VID 
			WHERE (VendorId IS NOT NULL OR VendorId <> '') --AND (AppleId IS NOT NULL OR AppleId <> '')
		) V  
		INNER JOIN (SELECT DISTINCT
						Q.VendorId, 
						Q.AppleId, 
						Q.LanguageId, 
						L.Name AS LanguageName,
						Q.ComponentType,
						Q.ImportedDate
					FROM QCReport Q INNER JOIN [Language] L ON L.Id = Q.LanguageId
					WHERE Q.ComponentType IN ('SUBTITLES', 'AUDIO', 'Forced_Subtitles', 'Audio_Descripion', 'DUB_CREDIT', 'CAPTIONS') AND Q.QAStatus NOT LIKE '%Reject%') QC
		ON V.VendorId = QC.VendorId -- AND V.AppleId = QC.AppleId
		INNER JOIN 
		(
			SELECT	P.VendorId,
					P.AppleId ,
					P.TerritoryId,
					T.AppleTerritory
			FROM PriceReport P INNER JOIN Territory T ON P.TerritoryId = T.Id
		) PR ON PR.AppleId = QC.AppleId AND PR.VendorId = QC.VendorId  


		/*
		ON A.VideoVersion = V.VideoVersion AND A.LocalEdit = V.LocalEdit
		INNER JOIN Territory T ON T.Id = A.TerritoryId
		INNER JOIN [Language] L ON A.Language = L.Id
		INNER JOIN QCReport  QC  ON V.VendorId = QC.VendorId AND V.AppleId = QC.AppleId
			AND A.LanguageType LIKE '%' + QC.ComponentType + '%' --Need to split for BOTH
			--AND QC.TERRITORY like  '%' + T.CountryCode + '%' 
			AND L.Id = QC.LanguageId
			
			*/





GO