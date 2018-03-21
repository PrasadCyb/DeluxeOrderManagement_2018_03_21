USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Insert_PriceReport]    Script Date: 12/7/2017 3:56:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_Insert_PriceReport]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--Insert Territory
		INSERT INTO Territory
		(
			WBTerritory,
			AppleTerritory
		)
		SELECT DISTINCT TERRITORY, TERRITORY FROM PriceReport_Staging 
		WHERE TERRITORY IS NOT NULL AND TERRITORY  <> '' 
		AND TERRITORY NOT IN 
			(SELECT DISTINCT AppleTerritory FROM Territory UNION SELECT WBTerritory FROM Territory)

		--Reload PriceReport
		TRUNCATE TABLE PriceReport

		INSERT INTO PriceReport
		(
			Title,
			VendorId,
			AppleId,
			TerritoryId,
			LiveVODHD,
			LiveVODSD,
			LiveESTHD,
			LiveESTSD,
			PreOrderDate,
			ClearedforSaleEST,
			EnableESTHD,
			ESTStartDate,
			ESTEndDate,
			ClearedforVOD,
			EnableVODHD,
			AvailForVOD,
			UnAvailForVOD,
			CustomerId
		)
		SELECT distinct 
			Title,
			[Vendor ID],
			CAST(LTRIM(STR([Apple ID],100)) AS NVARCHAR(100)),
			T.Id AS TerritoryId,
			[Live VOD HD],
			[Live VOD SD],
			[Live EST HD],
			[Live EST SD],
			[Pre-Order Date],
			CASE	WHEN [Cleared for Sale EST] = 'No' THEN 0 
					WHEN [Cleared for Sale EST]  = 'Yes' THEN 1
					ELSE [Cleared for Sale EST]  END AS [Cleared for Sale EST],
			CASE	WHEN [Enable EST HD] = 'No' THEN 0 
					WHEN [Enable EST HD]  = 'Yes' THEN 1
					ELSE [Enable EST HD]  END AS [Enable EST HD],
			CAST ([EST Start Date] AS DATETIME) AS [EST Start Date],
			CAST ([EST End Date] AS DATETIME) AS [EST End Date],
			CASE	WHEN [Cleared for VOD] = 'No' THEN 0 
					WHEN [Cleared for VOD]  = 'Yes' THEN 1
					ELSE [Cleared for VOD]  END AS [Cleared for VOD],
			CASE	WHEN [Enable VOD HD] = 'No' THEN 0 
					WHEN [Enable VOD HD]  = 'Yes' THEN 1
					ELSE [Enable VOD HD]  END AS [Enable VOD HD],
			CAST([Avail For VOD] AS DATETIME) AS [Avail For VOD],
			CAST([Unavail For VOD] AS DATETIME) AS [UnAvail For VOD],
			--[Content Provider]
			1
		from PriceReport_Staging PR 
		INNER JOIN Territory T ON (PR.TERRITORY = T.AppleTerritory )
				--INNER JOIN TerritoryLanguage TL ON TL.TerritoryId = T.Id
				--INNER JOIN Language L ON L.Id = TL.LanguageId

END

GO


