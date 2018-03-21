
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_UpdateOG_StatusOrdered
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	UPDATE OG
	SET OG.OrderStatus = 'Processing'
	FROM OrderGrid OG WHERE OG.AnnouncementId IN
	(
		SELECT AG.Id FROM AnnouncementGrid AG 
		INNER JOIN 
		(
			SELECT V.VideoVersion, V.LocalEdit 
			FROM 
				(SELECT VideoVersion, 
					(CASE WHEN EditName IS NULL OR EditName = '' OR EditName = 'No' THEN 'No' ELSE 'Yes' END) AS  LocalEdit
				FROM VID WHERE VendorId IS NULL) V 
			INNER JOIN 
				(SELECT DISTINCT 
					Title,
					VideoVersion, 
					LocalEdit, 
					LanguageId, 
					LanguageType 
				FROM AnnouncementGrid AG INNER JOIN OrderGrid O ON AG.Id = O.AnnouncementId) A ON A.VideoVersion = V.VideoVersion AND A.LocalEdit = V.LocalEdit
				INNER JOIN PipelineOrder PO 
				ON PO.TitleName = A.Title AND PO.IngredientLanguageId = A.LanguageId 
				AND CASE WHEN PO.IngredientAssetType = 'Subtitle' THEN 'Sub'
						WHEN PO.IngredientAssetType = 'Audio Bundle' THEN 'Audio'
						ELSE PO.IngredientAssetType END = A.LanguageType
		) PiPo ON AG.VideoVersion = PiPo.VideoVersion AND (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '' OR AG.LocalEdit = 'No' Then 'No' ELSE 'Yes' END) = PiPo.LocalEdit
	)

END
GO
