USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Insert_PipelineOrder]    Script Date: 12/18/2017 2:38:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[usp_Insert_PipelineOrder]
AS
BEGIN
		
	--Check if already records are exists then truncate the table
	--If((select Count(id) from PipelineOrder)>0)
	

		/* Language */
		--SELECT count(1) from Language
		INSERT INTO Language
		(
			Name
		)
		SELECT DISTINCT [Ingredient Language] 
		FROM PipelineOrder_Staging 
		WHERE ([Ingredient Language] IS NOT NULL OR [Ingredient Language] <> '') AND [Ingredient Language] NOT IN 
		(SELECT Name FROM Language)

		--Reload PipelineOrder
		Truncate table PipelineOrder

		INSERT INTO PipelineOrder
		(
			HALOrderID,
			TitleName,
			OrderStatus,
			ContractDueDate,
			TargetDueDate,
			PurchaseOrder,
			WorkOrder,
			CableLabsAssetID,
			CableLabsPackageAssetID,
			Workflow,
			GatorPO,
			WBAnnouncement,
			IngredientAssetType,
			IngredientLanguageId,
			IngredientStatus,
			CustomerId
		)
		SELECT 
			CAST(LTRIM(STR(STG.[HAL Order ID],50)) AS NVARCHAR(50)),
			STG.Name,
			STG.[Order Status],
			STG.[Contract Due Date],
			STG.[Target Due Date],
			STG.[Purchase Order],
			STG.[Work Order],
			STG.[CableLabs Asset ID],
			STG.[CableLabs Package Asset ID],
			STG.[Workflow],
			STG.[Gator PO],
			STG.[WB Announcement],
			STG.[Ingredient Asset Type],
			Lang.ID AS LanguageId,
			STG.[Ingredient Status],
			--Customer
			1 AS CustomerId
		FROM PipelineOrder_Staging STG
			LEFT JOIN Customer Cust ON Cust.Name = STG.[Customer] --OR Cust.Name='Warner Bros.'
			INNER JOIN Language Lang ON Lang.Name = STG.[Ingredient Language]
							   
END

GO