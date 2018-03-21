USE [DeluxeOrderManagement]
GO

/****** Object:  View [dbo].[vw_VID_QCReport_Title]    Script Date: 12/20/2017 12:12:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









ALTER VIEW [dbo].[vw_VID_QCReport_Title]
AS
SELECT  V.TitleName,
		V.VendorId,
		V.VIDStatus,
		V.MPM,
		PR.AppleTerritory,
		QC.LanguageName, 
		V.VideoVersion, 
		V.LocalEdit, 
		QC.LanguageId,
		PR.TerritoryId,
		PR.POESTStartDate,
		PR.ESTStartDate,
		PR.ESTEndDate,
		PR.LiveESTHD,
		PR.LiveESTSD,
		PR.VODStartDate,
		PR.VODEndDate, 
		PR.LiveVODHD,
		PR.LiveVODSD,
		QC.ComponentType,
		QC.ComponentQuality,
		QC.QAStatus
		
FROM   
		(SELECT DISTINCT VideoVersion, 
			(CASE WHEN EditName IS NULL OR EditName = '' OR EditName = 'No' Then 'No' ELSE 'Yes' END) AS LocalEdit,
			VendorId,
			VIDStatus,
			TitleName,
			MPM
			FROM VID 
			WHERE (VendorId IS NOT NULL OR VendorId <> '') --AND (AppleId IS NOT NULL OR AppleId <> '')
		) V  
		INNER JOIN (SELECT DISTINCT
						Q.VendorId, 
						Q.AppleId, 
						Q.LanguageId, 
						L.Name AS LanguageName,
						Q.ComponentType,
						Q.ComponentQuality,
						Q.QAStatus
					FROM QCReport Q INNER JOIN [Language] L ON L.Id = Q.LanguageId
					WHERE Q.ComponentType IN ('SUBTITLES', 'AUDIO', 'Forced_Subtitles', 'Audio_Descripion', 'DUB_CREDIT', 'CAPTIONS','VIDEO') /*AND Q.QAStatus NOT LIKE '%Reject%'*/) QC
		ON V.VendorId = QC.VendorId 
		INNER JOIN 
		(
			SELECT	P.VendorId,
					P.AppleId ,
					P.TerritoryId,
					P.PreOrderDate AS POESTStartDate,
					P.ESTStartDate,
					p.ESTEndDate,
					P.AvailForVOD AS VODStartDate,
					P.UnAvailForVOD AS VODEndDate,
					T.AppleTerritory,
					P.LiveESTHD,
					P.LiveESTSD,
					P.LiveVODHD,
					P.LiveVODSD
			FROM PriceReport P INNER JOIN Territory T ON P.TerritoryId = T.Id
		) PR ON PR.VendorId = QC.VendorId  















GO


