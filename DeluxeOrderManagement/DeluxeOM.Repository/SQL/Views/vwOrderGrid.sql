USE [DeluxeOrderManagement]
GO

/****** Object:  View [dbo].[vwOrderGrid]    Script Date: 12/6/2017 7:11:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vwOrderGrid]
AS 
SELECT  
		ROW_NUMBER() OVER (ORDER BY Title) AS RowNum,
        Title,
		[Video Version] AS VideoVersion,
		(CASE WHEN [Local Edit] IS NULL OR [Local Edit] = '' THEN 'No' ELSE [Local Edit] END) AS LocalEdit,
		[Version EIDR] AS VersionEIDR,
		[Vendor ID] AS VendorId,
		Category,
		[Region],
		Territory,
		[Language],
		[Language Type] AS LanguageType,
		[Vendor],
		[EST UPC] AS ESTUPC,
		[IVOD UPC] AS IVODUPC,
		[File Type] AS FileType,
		[Request Number] AS RequestNumber,
		[MPO],
		[HAL ID] AS HALID,
		[Order Status] AS OrderStatus,
		[Order Request Date] AS OrderRequestDate,
		[Delivery Due Date] AS DeliveryDueDate,
		[Actual Delivery Date] AS ActualDeliveryDate,
		[First Start Date] AS FirstStartDate,
		[Greenlight Due Date] AS GreenlightDueDate,
		[Greenlight Validated by DMDM] AS GreenlightValidatedbyDMDM,
		[Greenlight Sent to Packaging] AS GreenlightSenttoPackaging,
		[Notes],
		[CONCATENATE]
FROM
(SELECT  DISTINCT
	Title,
	[Video Version],
	[Local Edit],
	[Version EIDR],
	[Vendor ID],
	Category,
	[Region],
	Territory,
	[Language],
	[Language Type],
	[Vendor],
	[EST UPC],
	[IVOD UPC],
	[File Type],
	[Request Number],
	[MPO],
	[HAL ID],
	[Order Status],
	[Order Request Date],
	[Delivery Due Date],
	[Actual Delivery Date],
	[First Start Date],
	[Greenlight Due Date],
	[Greenlight Validated by DMDM],
	[Greenlight Sent to Packaging],
	[Notes],
	[CONCATENATE] 
FROM AppleGrid_STAGING) STG


GO


