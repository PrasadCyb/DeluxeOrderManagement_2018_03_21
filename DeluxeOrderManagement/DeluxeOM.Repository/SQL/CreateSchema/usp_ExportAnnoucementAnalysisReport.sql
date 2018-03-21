USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_ExportAnnoucementAnalysisReport]    Script Date: 11/29/2017 5:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ExportAnnoucementAnalysisReport]
		@whereClause  AS NVARCHAR(4000)
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)

IF OBJECT_ID(N'Tmp_ANNREP') IS NOT NULL
	DROP TABLE Tmp_ANNREP

 SET @sqlCommand ='
				SELECT  
					  A.Title AS [Title],
					  V.MPM AS [MPM],
					  A.VideoVersion AS [Video Version], 
					  A.LocalEdit AS [Local Edit],
					  A.Category AS [Category],
					  V.VendorId AS [Vendor ID], 
					  T.WBTerritory AS [Territory], 
					  L.Name AS [Language],
					  A.LanguageTYpe AS [Language Type],
					  A.OrderStatus  AS [Order Status],
					  A.MinPOEST AS [POESTStart],
					  A.ESTVODStartDate AS [Start Date]
					  INTO Tmp_ANNREP
					  FROM   
							 (
								   SELECT DISTINCT AG.Title, AG.VideoVersion, 
										  (CASE WHEN AG.LocalEdit IS NULL OR AG.LocalEdit = '''' OR AG.LocalEdit = ''No'' THEN ''No'' ELSE ''Yes'' END) AS LocalEdit, 
										  AG.TerritoryId,
										  AG.[LanguageId] ,
										  O.Category,
										  O.LanguageTYpe,
										  (SELECT MIN(d) MinValue FROM (VALUES (POESTSDDate), (POESTHDDate), (POEST4KUHDDate)) AS a(d)) AS MinPOEST,
										  (SELECT MIN(d) MinValue FROM (VALUES (ESTSDDate), (ESTHDDate), (EST4KUHDDate), (VODSDDate), (VODHDDate), (VOD4KUHDDate)) AS a(d)) AS ESTVODStartDate,
										  O.OrderStatus,
										  J.JobStartDate,
										  J.JobEndDate
								   FROM AnnouncementGrid AG 
										  INNER JOIN OrderGrid O ON AG.Id = O.AnnouncementId
										  INNER JOIN 
										  (
												 SELECT JobId AS JobId, MIN(StartDate) AS JobStartDate, MAX(EndDate) AS JobEndDate
												 FROM Jobs_Items
												 GROUP BY JobId
										  ) J ON (AG.JObId = J.JobId OR O.JObId = J.JobId)
								   WHERE AG.JobAction = ''Inserted'' OR O.JobAction = ''Inserted''
								  AND COALESCE(O.OrderStatus, ''NULLVAL'') NOT IN (''Complete'', ''Completed'')
							 ) A INNER JOIN
					  (SELECT DISTINCT VideoVersion, MPM,
							 (CASE WHEN EditName IS NULL OR EditName = '''' OR EditName = ''No'' Then ''No'' ELSE ''Yes'' END) AS LocalEdit,
							 VendorId
							 FROM VID WHERE VIDStatus = ''PRIMARY'' AND VideoVersion IS NOT NULL AND VideoVersion <> ''''
					  ) V   
					  ON A.VideoVersion = V.VideoVersion AND A.LocalEdit = V.LocalEdit
					  INNER JOIN Territory T ON T.Id = A.TerritoryId
					  INNER JOIN [Language] L ON A.LanguageId = L.Id
					  WHERE ISNULL(A.OrderStatus, '''') NOT IN (''Complete'', ''Completed'', ''Cancelled'')
					  '+  @whereClause +'
					  ORDER BY A.VideoVersion'
		-- print @sqlCommand
		
		EXECUTE sp_executesql @sqlCommand;
		
		IF EXISTS(SELECT 1 from sys.objects where name = 'Tmp_ANNREP') 
		BEGIN
		insert into OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;Database=D:\DeluxeOrderManagement\Reports\Annoucement Analysis Report.xlsx; TypeGuessRows=0; HDR=YES;',
		'SELECT * FROM [Sheet1$]') 
		select * from Tmp_ANNREP

		drop table Tmp_ANNREP
		END

END








GO


