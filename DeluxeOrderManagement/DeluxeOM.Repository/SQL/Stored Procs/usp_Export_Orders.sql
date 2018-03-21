USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Export_Orders]    Script Date: 12/20/2017 12:29:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO















-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Export_Orders] 
@whereClause  AS NVARCHAR(4000),
@channel AS NVARCHAR(4000) 
	
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)
	SET NOCOUNT ON;

	IF OBJECT_ID(N'TempOrder') IS NOT NULL BEGIN
	DROP TABLE TempOrder END
	SET @sqlCommand='SELECT 
	   O.OrderStatus AS [Order Status],
       O.Category AS [Category],
       R.Name AS [Region],
       T.WBTerritory AS [Territory],
       L.Name AS [Language],
       O.RequestNumber AS [Request Number],
       O.MPO AS [MPO],
       O.HALID AS [HAL ID],
       V.VendorId AS [Vendor ID],
	   --CASE WHEN (AG.FirstAnnouncedDate IN (''N/A'', ''Pre-CAS'')) THEN NULL ELSE (CONVERT(NVARCHAR(50),AG.FirstAnnouncedDate)) END AS [First Start Date],
       FORMAT(AG.FirstAnnouncedDate, ''MM-dd-yyyy'') AS [First Start Date],
	   FORMAT(O.DeliveryDueDate, ''MM-dd-yyyy'') AS [Due Date],
	   FORMAT(O.GreenlightSenttoPackaging, ''MM-dd-yyyy'') AS [Greenlight Sent],
	   --CASE WHEN (O.GreenlightValidatedbyDMDM IN (''N/A'')) THEN NULL ELSE (CONVERT(NVARCHAR(50),O.GreenlightValidatedbyDMDM)) END AS [Greenlight Received],
       FORMAT(O.GreenlightValidatedbyDMDM, ''MM-dd-yyyy'') AS [Greenlight Received],
       O.LanguageType AS [Asset Required], 
       NULL As [Order Type],
       O.ESTUPC AS [EST UPC],
       O.IVODUPC AS [VOD UPC]
	   INTO TempOrder
FROM OrderGrid O INNER JOIN AnnouncementGrid AG ON O.AnnouncementId = AG.Id
		INNER JOIN 
		(
			SELECT C.AnnouncementId,MIN(C.ClientStartDate) AS MinClientStartDate, MAX(C.ClientEndDate) AS MaxClientEndDate
			FROM Announceemnt_Channel_Format C 
				INNER JOIN AnnouncementGrid AG ON AG.Id = C.AnnouncementId
			'+' '+@channel+'
			GROUP BY C.AnnouncementId
		) CF ON CF.AnnouncementId = AG.Id
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
	   IF EXISTS(SELECT 1 from sys.objects where name = 'TempOrder') 
		BEGIN
		insert into OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;Database=D:\ExportReport\Order Report.xlsx; TypeGuessRows=0; HDR=YES;','SELECT * FROM [Sheet1$]')
		select * from TempOrder
		drop table TempOrder
		END

	
END









GO


