USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Export_Title]    Script Date: 12/20/2017 12:32:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[usp_Export_Title] 
@whereClause  AS NVARCHAR(4000) 
	
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)
	SET NOCOUNT ON;

	IF OBJECT_ID(N'TempReport') IS NOT NULL BEGIN
	DROP TABLE TempReport END
	SET @sqlCommand='SELECT DISTINCT 
	   TitleName,
	   VendorId,
	   VIDStatus,
	   AppleTerritory,
	   CAST(POESTStartDate AS DATE) AS POESTStartDate,
	   FORMAT(ESTStartDate, ''MM-dd-yyyy'') AS ESTStartDate,
	   --CAST(ESTStartDate AS DATE) AS ESTStartDate,
	   FORMAT(ESTEndDate, ''MM-dd-yyyy'') AS ESTEndDate,
	   --CAST(ESTEndDate AS DATE) AS ESTEndDate,
	   LiveESTHD,
	   LiveESTSD,
	   FORMAT(VODStartDate, ''MM-dd-yyyy'') AS VODStartDate,
	   --CAST(VODStartDate AS DATE) AS VODStartDate,
	   FORMAT(VODEndDate, ''MM-dd-yyyy'') AS VODEndDate,
	   --CAST(VODEndDate AS DATE) AS VODEndDate,
	   LiveVODHD,
	   LiveVODSD,
	   FORMAT(MinPOEST, ''MM-dd-yyyy'') AS AnnPOESTStartDate,
	   --CAST(MinPOEST AS DATE) AS AnnPOESTStartDate,
	   FORMAT(MinEST, ''MM-dd-yyyy'') AS AnnESTStartDate,
	   --CAST(MinEST AS DATE) AS AnnESTStartDate,
	   FORMAT(MaxEST, ''MM-dd-yyyy'') AS AnnESTEndDate,
	   --CAST(MaxEST AS DATE) AS AnnESTEndDate,
	   FORMAT(MinVOD, ''MM-dd-yyyy'') AS AnnVODStartDate,
	   --CAST(MinVOD AS DATE) AS AnnVODStartDate,
	   FORMAT(MaxVOD, ''MM-dd-yyyy'') AS AnnVODEndDate,
	   --CAST(MaxVOD AS DATE) AS AnnVODEndDate,
	   ComponentType,
	   ComponentQuality,
	   LanguageName,
	   LanguageType,
	   LanguageStatus
	   INTO TempReport
		from TitlesReport '
	    + @whereClause + ''
		EXECUTE sp_executesql @sqlCommand;
	   --print @sqlCommand
	   IF EXISTS(SELECT 1 from sys.objects where name = 'TempReport') 
		BEGIN
		insert into OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;Database=D:\DeluxeOrderManagement\Reports\Title Report.xlsx; TypeGuessRows=0; HDR=YES;','SELECT * FROM [Sheet1$]')
		select * from TempReport
		drop table TempReport
		END

	
END


GO


