
CREATE TABLE [dbo].[Jobs_Items](
	[Id] [int] PRIMARY KEY  IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[TaskName] [varchar](250) NULL,
	[Status] [bit] NULL,
	[Description] [varchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL

	)

