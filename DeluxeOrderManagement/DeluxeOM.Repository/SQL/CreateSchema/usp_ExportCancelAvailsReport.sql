USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_ExportCancelAvailsReport]    Script Date: 11/29/2017 6:54:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Pritam Mandlik
-- Create date:9-11-2017
-- Description:	Get Cancel Avails report
-- =============================================
CREATE PROCEDURE [dbo].[usp_ExportCancelAvailsReport]
	@whereClause  AS NVARCHAR(4000)
AS
BEGIN
DECLARE @sqlCommand NVARCHAR(max)

IF OBJECT_ID(N'Tmp_CANCELREP') IS NOT NULL
DROP TABLE Tmp_CANCELREP
SET @sqlCommand ='
				SELECT
						AnnGrid.Title,
						AnnGrid.LocalEdit AS [Local Edit],
						AnnGrid.VideoVersion AS [Video Version],
						ordGrid.FirstStartDate AS [First Start],
						AnnGrid.ESTSDStatus AS [EST SD Status],
						AnnGrid.ESTHDStatus AS [EST HD Status],
						AnnGrid.EST4KUHDStatus AS [EST 4K Status],
						AnnGrid.VODSDStatus AS [VOD SD Status],
						AnnGrid.VODHDStatus AS [VOD HD Status],
						AnnGrid.VOD4KUHDStatus AS [VOD 4K Status],
						AnnGrid.POESTSDStatus AS [Pre Order EST SD Status],
						AnnGrid.POESTHDStatus AS [Pre Order EST HD Status],
						AnnGrid.POEST4KUHDStatus AS [Pre Order EST 4K Status],
						AnnGrid.ESTSDDate AS [EST SD Client Start],
						AnnGrid.ESTHDDate AS [EST HD Client Start],
						AnnGrid.EST4KUHDDate AS [EST 4K Client Start],
						AnnGrid.VODSDDate AS [VOD SD Client Start],
						AnnGrid.VODHDDate AS [VOD HD Client Start],
						AnnGrid.VOD4KUHDDate AS [VOD 4K Client Start],
						AnnGrid.POESTSDDate AS [Pre Order EST SD Client Start],
						AnnGrid.POESTHDDate AS [Pre Order EST HD Client Start],
						AnnGrid.POEST4KUHDDate AS [Pre Order EST 4K Client Start]
						INTO Tmp_CANCELREP
				FROM AnnouncementGrid AnnGrid
				Left join OrderGrid ordGrid on ordGrid.announcementid=AnnGrid.ID 
			    WHERE ordGrid.OrderStatus=''Cancelled'' 		
				'+  @whereClause +''
		
		
		EXECUTE sp_executesql @sqlCommand;
		
		IF EXISTS(SELECT 1 from sys.objects where name = 'Tmp_CANCELREP') 
		BEGIN
		INSERT INTO OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;Database=D:\DeluxeOrderManagement\Reports\Cancel Avails Report.xlsx; TypeGuessRows=0; HDR=YES;',
		'SELECT * FROM [Sheet1$]') 
		SELECT * FROM Tmp_CANCELREP

		DROP TABLE Tmp_CANCELREP
		END
END









GO


