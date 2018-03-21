CREATE TABLE Jobs_Announcements_Inserted
(
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[AnnouncementGridId] [int] NULL,
	[OrderGridId] [int] NULL,
	[StartDate] [DateTime] NULL,
	[EndDate] [DateTime] NULL
)

