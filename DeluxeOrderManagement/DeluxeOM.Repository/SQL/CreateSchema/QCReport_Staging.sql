USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[QCReport_Staging]    Script Date: 23-11-2017 15:44:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QCReport_Staging](
	[APPLE_ID] [float] NULL,
	[TITLE] [nvarchar](255) NULL,
	[FILM_SUBTYPE] [nvarchar](255) NULL,
	[VENDOR_ID] [nvarchar](255) NULL,
	[IMPORTED_DATE] [datetime] NULL,
	[COMPONENT_TYPE] [nvarchar](255) NULL,
	[LOCALE_CODE] [nvarchar](255) NULL,
	[TERRITORY] [nvarchar](255) NULL,
	[COMPONENT_QUALITY] [nvarchar](255) NULL,
	[QC_STATUS] [nvarchar](255) NULL,
	[EXCLUSIONS] [nvarchar](255) NULL
) ON [PRIMARY]

GO


