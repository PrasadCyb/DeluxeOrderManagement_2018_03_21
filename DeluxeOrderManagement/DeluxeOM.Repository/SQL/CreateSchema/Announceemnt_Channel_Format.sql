CREATE TABLE [dbo].[Announceemnt_Channel_Format](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AnnouncementId] [int] NOT NULL,
	[Channel] [nvarchar](50) NULL,
	[Format] [nvarchar](50) NULL,
	[ClientAvailStatus] [nvarchar](50) NULL,
	[ClientStartDate] [datetime] NULL,
	[CustomerId] [int] NOT NULL,
	[JObId] [int] NULL,
	[ChannelFormat] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]