USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[VID_Staging]    Script Date: 23-11-2017 15:46:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VID_Staging](
	[VID Status] [nvarchar](255) NULL,
	[Bundle] [bit] NULL,
	[Extras] [bit] NULL,
	[Worldwide Title] [nvarchar](255) NULL,
	[MPM] [nvarchar](255) NULL,
	[Video Version] [nvarchar](255) NULL,
	[Edit Name] [nvarchar](255) NULL,
	[Version EIDR] [nvarchar](255) NULL,
	[Apple ID] [nvarchar](255) NULL,
	[Vendor ID] [nvarchar](255) NULL,
	[Title Category] [nvarchar](255) NULL
) ON [PRIMARY]

GO


