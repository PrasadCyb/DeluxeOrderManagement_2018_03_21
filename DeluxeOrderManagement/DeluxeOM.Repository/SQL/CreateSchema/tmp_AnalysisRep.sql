CREATE TABLE [dbo].[tmp_AnalysisRep](
	[Title] [nvarchar](255) NULL,
	[MPM] [nvarchar](50) NULL,
	[VideoVersion] [nvarchar](50) NULL,
	[LocalEdit] [varchar](3) NOT NULL,
	[Category] [nvarchar](100) NULL,
	[VendorId] [nvarchar](50) NULL,
	[Territory] [nvarchar](255) NULL,
	[Language] [nvarchar](50) NOT NULL,
	[LanguageType] [nvarchar](50) NULL,
	[OrderStatus] [nvarchar](50) NULL,
	[POESTStart] [datetime] NULL,
	[StartDate] [datetime] NULL
) ON [PRIMARY]

GO