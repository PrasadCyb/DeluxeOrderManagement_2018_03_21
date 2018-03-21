
CREATE TABLE [dbo].[JOBS](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[JobType] [varchar](250) NULL,
	[Status] [bit] NULL,
	[Description] [varchar](max) NULL,
	[TriggeredBy] [varchar](50) NULL
	)
