USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[PipelineOrder_Staging]    Script Date: 23-11-2017 15:44:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PipelineOrder_Staging](
	[HAL Order ID] [float] NULL,
	[Name] [nvarchar](255) NULL,
	[Customer] [nvarchar](255) NULL,
	[Order Status] [nvarchar](255) NULL,
	[Contract Due Date] [nvarchar](255) NULL,
	[Target Due Date] [nvarchar](255) NULL,
	[Purchase Order] [nvarchar](255) NULL,
	[Work Order] [nvarchar](255) NULL,
	[CableLabs Asset ID] [nvarchar](255) NULL,
	[CableLabs Package Asset ID] [nvarchar](255) NULL,
	[Workflow] [nvarchar](255) NULL,
	[Gator PO] [nvarchar](255) NULL,
	[WB Announcement] [nvarchar](255) NULL,
	[Ingredient Asset Type] [nvarchar](255) NULL,
	[Ingredient Language] [nvarchar](255) NULL,
	[Ingredient Status] [nvarchar](255) NULL
) ON [PRIMARY]

GO


