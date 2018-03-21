CREATE TABLE [dbo].[AnnouncementGrid](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[VideoVersion] [nvarchar](50) NULL,
	[LocalEdit] [nvarchar](100) NULL,
	[TerritoryId] [int] NULL,
	[LanguageId] [int] NULL,
	[CustomerId] [int] NOT NULL,
	[JObId] [int] NULL,
	[FirstAnnouncedDate] [datetime] NULL,
	[LastAnnouncedDate] [datetime] NULL,
	[CancellationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
