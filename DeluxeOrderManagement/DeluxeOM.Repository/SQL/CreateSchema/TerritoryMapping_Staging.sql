USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[TerritoryMapping_Staging]    Script Date: 23-11-2017 15:45:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TerritoryMapping_Staging](
	[WB Territory] [nvarchar](255) NULL,
	[ISO-3166 Country Code] [nvarchar](255) NULL,
	[Apple Territory] [nvarchar](255) NULL,
	[Region] [nvarchar](255) NULL,
	[PACKAGE] [nvarchar](255) NULL,
	[GROUP] [nvarchar](255) NULL,
	[Primary Language] [nvarchar](255) NULL,
	[Primary Language Code] [nvarchar](255) NULL,
	[Secondary Language] [nvarchar](255) NULL,
	[Secondary Language Code] [nvarchar](255) NULL,
	[Tertiary Language] [nvarchar](255) NULL,
	[Tertiary Language Code] [nvarchar](255) NULL
) ON [PRIMARY]

GO


