USE [DeluxeOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[usp_ExportFinanceReport]    Script Date: 11/29/2017 1:05:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_ExportFinanceReport]
		@whereClause  AS NVARCHAR(4000)
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)

IF OBJECT_ID(N'Tmp_FINREP') IS NOT NULL
DROP TABLE Tmp_FINREP

 SET @sqlCommand =' SELECT 
						   O.MPO AS [PO Number],
						   A.Title AS [Title],
						   O.LanguageType As [Language Type],
						   O.OrderStatus As [Order Status],
						   T.[Group] As [Group],
						   VQ.ImportedDate [Import Date]
					INTO Tmp_FINREP
				FROM AnnouncementGrid A 
						   INNER JOIN OrderGrid O ON A.Id= O.AnnouncementId
						   INNER JOIN 
								  (
									 SELECT DISTINCT 
											   VideoVersion, 
											   (CASE WHEN EditName IS NULL OR EditName = '''' OR EditName = ''No'' THEN ''No'' ELSE ''Yes'' END) AS LocalEdit, 
											   MPM, 
											   VersionEIDR, 
											   VendorId,
											   (CASE WHEN VIDStatus = ''PRIMARY'' THEN ''Yes'' ELSE ''No'' END) AS IsPrimary
										 FROM VID
								  ) V ON A.VideoVersion = V.VideoVersion AND A.LocalEdit = V.LocalEdit
								   INNER JOIN Territory T ON T.Id = A.TerritoryId
								   INNER JOIN Language L ON L.Id = A.LanguageId
								  INNER JOIN vw_VID_QCReport VQ ON VQ.VideoVersion = ISNULL(A.VideoVersion, ''NULLVAL'') 
                                  AND VQ.LocalEdit = A.LocalEdit
                                  AND VQ.LanguageId = A.LanguageId 
                                  AND VQ.TerritoryId = A.TerritoryId

								  WHERE  VQ.ImportedDate IS NOT NULL  '+  @whereClause +'
						GROUP BY O.MPO,A.Title,O.LanguageType,O.OrderStatus,T.[GROUP],VQ.ImportedDate	 
					    HAVING(COUNT(*))>1
					  	ORDER BY A.Title'
					  
		--print @sqlCommand
		
		EXECUTE sp_executesql @sqlCommand;
		
		IF EXISTS(SELECT 1 from sys.objects where name = 'Tmp_FINREP') 
		BEGIN
		insert into OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;Database=D:\DeluxeOrderManagement\Reports\Finance Report.xlsx; TypeGuessRows=0; HDR=YES;',
		'SELECT  * FROM [Sheet1$]') 
		select * from Tmp_FINREP
		drop table Tmp_FINREP
		END

END



