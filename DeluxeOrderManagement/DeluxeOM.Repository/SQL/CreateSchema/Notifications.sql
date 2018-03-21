CREATE TABLE [dbo].[Notifications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Enabled] [bit] NOT NULL,
	[FromEmailAddress] [nvarchar](512) NULL,
	[ToEmailAddress] [nvarchar](512) NULL,
	[SuccessSubject] [nvarchar](255) NULL,
	[FailureSubject] [nvarchar](255) NULL,
	[SuccessBody] [nvarchar](max) NULL,
	[FailureBody] [nvarchar](max) NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

============================================CYBAGE DEV==================================================================================================

INSERT INTO [dbo].[Notifications]
           (
				Name,
				Type,
				Enabled,
				FromEmailAddress,
				ToEmailAddress,
				SuccessSubject,
				FailureSubject,
				SuccessBody,
				FailureBody,
				Description
			)
     VALUES
           (
				'Avail Announcement File Import Status'
			   ,'LoadAnnounceemnt'
			   ,1
			   ,'amoljadh@evolvingsols.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Avail Announcement File Successful'
			   ,'Import of Avail Announcement File Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Apple Price Report Import Status'
			   ,'LoadPriceReport'
			   ,1
			   ,'amoljadh@evolvingsols.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Apple Price Report Successful'
			   ,'Import of Apple Price Report Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Apple QC Report Import Status'
			   ,'LoadQCReport'
			   ,1
			   ,'amoljadh@evolvingsols.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Apple QC Report Successful'
			   ,'Import of Apple QC Report Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Pipeline Orders File Import Status'
			   ,'LoadPipelineOrder'
			   ,1
			   ,'amoljadh@evolvingsols.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Pipeline Orders File Successful'
			   ,'Import of Pipeline Orders File Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'New Title in Avail Announcement'
			   ,'NewTitles'
			   ,1
			   ,'amoljadh@evolvingsols.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'New Title in Announcement'
			   ,''
			   ,'<html><body>Dear User,<br/><br/>Event: New titles has been identified <br/><br/>---	<br/>Below is a list of new titles found in Announcement file {0}  <br/>{1}<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,''
			   ,NULL
			)
			
GO


============================================DELUXE QA==================================================================================================

INSERT INTO [dbo].[Notifications]
           (
				Name,
				Type,
				Enabled,
				FromEmailAddress,
				ToEmailAddress,
				SuccessSubject,
				FailureSubject,
				SuccessBody,
				FailureBody,
				Description
			)
     VALUES
           (
				'Avail Announcement File Import Status'
			   ,'LoadAnnounceemnt'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Avail Announcement File Successful'
			   ,'Import of Avail Announcement File Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Apple Price Report Import Status'
			   ,'LoadPriceReport'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Apple Price Report Successful'
			   ,'Import of Apple Price Report Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Apple QC Report Import Status'
			   ,'LoadQCReport'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Apple QC Report Successful'
			   ,'Import of Apple QC Report Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Pipeline Orders File Import Status'
			   ,'LoadPipelineOrder'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'Import of Pipeline Orders File Successful'
			   ,'Import of Pipeline Orders File Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'New Title in Avail Announcement'
			   ,'NewTitles'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com'
			   ,'New Title in Announcement'
			   ,''
			   ,'<html><body>Dear User,<br/><br/>Event: New titles has been identified <br/><br/>---	<br/>Below is a list of new titles found in Announcement file {0}  <br/>{1}<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,''
			   ,NULL
			)
			
GO



============================================DELUXE PROD==================================================================================================

INSERT INTO [dbo].[Notifications]
           (
				Name,
				Type,
				Enabled,
				FromEmailAddress,
				ToEmailAddress,
				SuccessSubject,
				FailureSubject,
				SuccessBody,
				FailureBody,
				Description
			)
     VALUES
           (
				'Avail Announcement File Import Status'
			   ,'LoadAnnounceemnt'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com,Andy.nair@bydeluxe.com,Leon.ollison@bydeluxe.com,Henry.showers@bydeluxe.com,Dwight.sityar@bydeluxe.com,John.tuttle@bydeluxe.com,Bryan.bolint@bydeluxe.com'
			   ,'Import of Avail Announcement File Successful'
			   ,'Import of Avail Announcement File Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Apple Price Report Import Status'
			   ,'LoadPriceReport'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com,Andy.nair@bydeluxe.com,Leon.ollison@bydeluxe.com,Henry.showers@bydeluxe.com,Dwight.sityar@bydeluxe.com,John.tuttle@bydeluxe.com,Bryan.bolint@bydeluxe.com'
			   ,'Import of Apple Price Report Successful'
			   ,'Import of Apple Price Report Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Apple QC Report Import Status'
			   ,'LoadQCReport'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com,Andy.nair@bydeluxe.com,Leon.ollison@bydeluxe.com,Henry.showers@bydeluxe.com,Dwight.sityar@bydeluxe.com,John.tuttle@bydeluxe.com,Bryan.bolint@bydeluxe.com'
			   ,'Import of Apple QC Report Successful'
			   ,'Import of Apple QC Report Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'Pipeline Orders File Import Status'
			   ,'LoadPipelineOrder'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com,Andy.nair@bydeluxe.com,Leon.ollison@bydeluxe.com,Henry.showers@bydeluxe.com,Dwight.sityar@bydeluxe.com,John.tuttle@bydeluxe.com,Bryan.bolint@bydeluxe.com'
			   ,'Import of Pipeline Orders File Successful'
			   ,'Import of Pipeline Orders File Failed'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} completed successfully <br/><br/>---	<br/>Number of records imported ={1}. <br/>---<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,'<html><body>Dear User,<br/><br/>Event: File Import of {0} failed with Errors <br/>{1}<br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,NULL
			),
			(
				'New Title in Avail Announcement'
			   ,'NewTitles'
			   ,1
			   ,'warnerbros-itunesDL@bydeluxe.com'
			   ,'prasadja@cybage.com,amoljadh@cybage.com,johnsonk@cybage.com,shaileshso@cybage.com,Andy.nair@bydeluxe.com,Leon.ollison@bydeluxe.com,Henry.showers@bydeluxe.com,Dwight.sityar@bydeluxe.com,John.tuttle@bydeluxe.com,Bryan.bolint@bydeluxe.com'
			   ,'New Title in Announcement'
			   ,''
			   ,'<html><body>Dear User,<br/><br/>Event: New titles has been identified <br/><br/>---	<br/>Below is a list of new titles found in Announcement file {0}  <br/>{1}<br/><br/>Automated message from Apple Grid Order Management.<br/><br/>Sent at {2}</html></body>'
			   ,''
			   ,NULL
			)
			
GO