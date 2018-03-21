USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Get_Orders]    Script Date: 12/7/2017 9:59:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Get_Orders] 
@whereClause  AS NVARCHAR(4000) 
	
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)
	SET NOCOUNT ON;
	SET @sqlCommand='SELECT 
	   O.OrderStatus,
       O.Category,
       R.Name AS Region,
       T.WBTerritory AS Territory,
       L.Name AS Language,
       O.RequestNumber,
       O.MPO,
       O.HALID,
       V.VendorId,
	   V.Id AS VId,
	   O.Id OrderID,
	   V.TitleName,
	   AG.Id AS AnnouncementId,
       AG.FirstAnnouncedDate,
       O.DeliveryDueDate,
       O.GreenlightDueDate,
       O.GreenlightSenttoPackaging,
       O.GreenlightValidatedbyDMDM,
       O.LanguageType, 
       NULL As OrderType,
       O.ESTUPC,
       O.IVODUPC
FROM OrderGrid O INNER JOIN AnnouncementGrid AG ON O.AnnouncementId = AG.Id
       INNER JOIN Territory T ON T.Id = AG.TerritoryId
       INNER JOIN Region R ON R.Id = T.RegionId
       INNER JOIN Language L ON L.Id = AG.LanguageId
       LEFT JOIN 
              (
                     SELECT DISTINCT VideoVersion,Id, (CASE WHEN EditName IS NULL OR EditName = '''' OR EditName = ''No'' THEN ''No'' ELSE ''Yes'' END) AS LocalEdit, VendorId , MAX(TitleName) AS TitleName
                     FROM VID WHERE VideoVersion IS NOT NULL AND VideoVersion <> ''''
					 group by Id, VideoVersion,(CASE WHEN EditName IS NULL OR EditName = '''' OR EditName = ''No'' THEN ''No'' ELSE ''Yes'' END), VendorId 
              ) V 
       ON (V.VideoVersion = AG.VideoVersion 
                                  AND V.LocalEdit = (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '''' THEN ''No''  ELSE AG.LocalEdit END)
                                  AND O.VidId IS NULL) OR (V.Id = O.VidId )'
	   --INNER JOIN PriceReport P ON (P.VendorId=V.VendorId AND P.TerritoryId=AG.TerritoryId)' 
	   + @whereClause+''

	   EXECUTE sp_executesql @sqlCommand;
	   --print @sqlCommand



END


GO


