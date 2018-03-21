USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Search_Title]    Script Date: 12/7/2017 1:29:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Search_Title] 
@whereClause  AS NVARCHAR(4000) 
	
AS
BEGIN
	DECLARE @sqlCommand NVARCHAR(max)
	SET NOCOUNT ON;
	SET @sqlCommand='SELECT DISTINCT 
	   TitleName,
	   VendorId,
	   VIDStatus,
	   AppleTerritory,
	   POESTStartDate,
	   ESTStartDate,
	   POESTHDDate,
	   POESTSDDate,
	   ESTSDDate,
	   ESTHDDate,
	   VODSDDate,
	   VODHDDate,
	   EST4KUHDDate,
	   POEST4KUHDDate,
	   VOD4KUHDDate,
	   ComponentType,
	   ComponentQuality,
	   LanguageName,
	   LanguageType
	   FROM TitlesReport '
	    + @whereClause + ''
	   EXECUTE sp_executesql @sqlCommand;
	   --print @sqlCommand
	
END


GO


