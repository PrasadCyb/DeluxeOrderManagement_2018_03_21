USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[PriceReport_Staging]    Script Date: 23-11-2017 15:44:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceReport_Staging](
	[Content Provider] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Vendor ID] [nvarchar](255) NULL,
	[Apple ID] [float] NULL,
	[Film Type] [nvarchar](255) NULL,
	[Territory] [nvarchar](255) NULL,
	[Live VOD HD] [nvarchar](255) NULL,
	[Live VOD SD] [nvarchar](255) NULL,
	[Live EST HD] [nvarchar](255) NULL,
	[Live EST SD] [nvarchar](255) NULL,
	[Allow Pre-Order] [nvarchar](255) NULL,
	[Cleared for MA] [nvarchar](255) NULL,
	[Pre-Order Date] [datetime] NULL,
	[SD Wholesale Price Tier] [float] NULL,
	[SD Wholesale Price Tier Start Date] [datetime] NULL,
	[SD Wholesale Price Tier End Date] [datetime] NULL,
	[HD Wholesale Price Tier] [float] NULL,
	[HD Wholesale Price Tier Start Date] [datetime] NULL,
	[HD Wholesale Price Tier End Date] [datetime] NULL,
	[Cleared for Sale EST] [nvarchar](255) NULL,
	[Enable EST HD] [nvarchar](255) NULL,
	[EST Start Date] [datetime] NULL,
	[EST End Date] [datetime] NULL,
	[VOD Type] [nvarchar](255) NULL,
	[Cleared for VOD] [nvarchar](255) NULL,
	[Enable VOD HD] [nvarchar](255) NULL,
	[Avail For VOD] [datetime] NULL,
	[Unavail For VOD] [datetime] NULL,
	[Physical Release Date] [datetime] NULL,
	[HD Physical Release date] [datetime] NULL,
	[HD EST Early Start date] [datetime] NULL,
	[Genres] [nvarchar](255) NULL,
	[Rating] [nvarchar](255) NULL,
	[Rating Reason] [nvarchar](255) NULL,
	[Production Company] [nvarchar](255) NULL,
	[Copyright] [nvarchar](255) NULL,
	[Custom Page] [nvarchar](255) NULL
) ON [PRIMARY]

GO


