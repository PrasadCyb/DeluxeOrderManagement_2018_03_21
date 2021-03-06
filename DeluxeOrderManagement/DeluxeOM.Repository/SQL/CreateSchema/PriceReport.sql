CREATE TABLE [dbo].[PriceReport](
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[VendorId] [nvarchar](100) NULL,
	[AppleId] [nvarchar](50) NULL,
	[TerritoryId] [int] NULL,
	[LiveVODHD] [nvarchar](50) NULL,
	[LiveVODSD] [nvarchar](50) NULL,
	[LiveESTHD] [nvarchar](50) NULL,
	[LiveESTSD] [nvarchar](50) NULL,
	[PreOrderDate] [datetime] NULL,
	[ClearedforSaleEST] [bit] NULL,
	[EnableESTHD] [bit] NULL,
	[ESTStartDate] [datetime] NULL,
	[ESTEndDate] [datetime] NULL,
	[ClearedforVOD] [bit] NULL,
	[EnableVODHD] [bit] NULL,
	[AvailForVOD] [datetime] NULL,
	[UnAvailForVOD] [datetime] NULL,
	[CustomerId] [int] NULL,
)


