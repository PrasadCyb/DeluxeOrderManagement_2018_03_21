USE [DeluxeOrderManagement]
GO

/****** Object:  Table [dbo].[OrderGrid]    Script Date: 12/6/2017 7:10:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderGrid](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AnnouncementId] [int] NOT NULL,
	[VIDId] [int] NULL,
	[LanguageType] [nvarchar](50) NULL,
	[Category] [nvarchar](100) NULL,
	[ESTUPC] [nvarchar](100) NULL,
	[IVODUPC] [nvarchar](100) NULL,
	[FileType] [nvarchar](50) NULL,
	[RequestNumber] [nvarchar](50) NULL,
	[MPO] [nvarchar](50) NULL,
	[HALID] [nvarchar](50) NULL,
	[OrderStatus] [nvarchar](50) NULL,
	[OrderRequestDate] [datetime] NULL,
	[DeliveryDueDate] [datetime] NULL,
	[ActualDeliveryDate] [datetime] NULL,
	[GreenlightDueDate] [datetime] NULL,
	[GreenlightValidatedbyDMDM] [datetime] NULL,
	[GreenlightSenttoPackaging] [datetime] NULL,
	[Notes] [nvarchar](255) NULL,
	[CustomerId] [int] NOT NULL,
	[JObId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


