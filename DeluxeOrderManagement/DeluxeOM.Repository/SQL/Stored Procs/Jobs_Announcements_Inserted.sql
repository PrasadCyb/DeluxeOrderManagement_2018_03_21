ALter Table AnnouncementGrid
Add ChannelFormat nvarchar(256)


Jobs_Announcements_Inserted

CREATE TABLE Jobs_Announcements_Inserted
(
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[AnnouncementGridId] [int] NULL,
	[OrderGridId] [int] NULL,
	[StartDate] [DateTime] NULL,
	[EndDate] [DateTime] NULL
)

Id
JobId
AnnouncementGridId
OrderGridId

Date From To
