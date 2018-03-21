CREATE TABLE [dbo].[Customer](
	[ID] [int] PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [bit] NOT NULL
	)
	
GO

INSERT INTO [dbo].[Customer]
           (
			   [Name]
			   ,[Type]
			   ,[Status]
			)
     VALUES
           (
           'Warner Bros.'
           ,1 --provider
           ,1
		),
		(
           'Apple'
           ,2 --Distributor
           ,1
		)
GO

