USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[Announcements_STAGING]    Script Date: 23-11-2017 15:42:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Announcements_STAGING](
	[Change Status] [nvarchar](255) NULL,
	[Changed Fields] [nvarchar](255) NULL,
	[Change Context] [nvarchar](255) NULL,
	[Local Title: Local Title] [nvarchar](255) NULL,
	[Global Title] [nvarchar](255) NULL,
	[Title - Content Type] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Language] [nvarchar](255) NULL,
	[Language Type] [nvarchar](255) NULL,
	[Sales Channel] [nvarchar](255) NULL,
	[Format] [nvarchar](255) NULL,
	[HDR] [nvarchar](255) NULL,
	[Client Start] [datetime] NULL,
	[Client End] [datetime] NULL,
	[Client Avail Status] [nvarchar](255) NULL,
	[EC Available] [nvarchar](255) NULL,
	[Suppression End Date] [nvarchar](255) NULL,
	[Title Category] [nvarchar](255) NULL,
	[PriceType] [nvarchar](255) NULL,
	[Price Tier] [nvarchar](255) NULL,
	[WS Cost] [nvarchar](255) NULL,
	[Storefront - Currency] [nvarchar](255) NULL,
	[Local Data -Theatrical Release Dt] [nvarchar](255) NULL,
	[Local DVD Release Date] [nvarchar](255) NULL,
	[Local Data - Rating] [nvarchar](255) NULL,
	[RatingReason] [nvarchar](255) NULL,
	[Local Data - DVS] [nvarchar](255) NULL,
	[Title - MPM] [nvarchar](255) NULL,
	[Title - Video Version] [nvarchar](255) NULL,
	[Local Edit Required] [nvarchar](255) NULL,
	[Title - Abstract EIDR] [nvarchar](255) NULL,
	[Local Data - Edit EIDR] [nvarchar](255) NULL,
	[Local Data - UV Publish Date] [nvarchar](255) NULL,
	[Local Data - ALID] [nvarchar](255) NULL,
	[Local Data - CID] [nvarchar](255) NULL,
	[Client Avail ID] [nvarchar](255) NULL,
	[Account: Account Name] [nvarchar](255) NULL,
	[Company] [nvarchar](255) NULL,
	[Avail Notes] [nvarchar](255) NULL
) ON [PRIMARY]

GO


