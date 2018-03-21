
CREATE TABLE [dbo].[PipelineOrder](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[HALOrderID] [nvarchar](50) NULL,
	[TitleName] [nvarchar](100) NULL,
	[OrderStatus] nvarchar(50) NULL,
	[ContractDueDate] [nvarchar](50) NULL,
	[TargetDueDate] [nvarchar](50) NULL,
	[PurchaseOrder] [nvarchar](50) NULL,
	[WorkOrder] [nvarchar](50) NULL,
	[CableLabsAssetID] [nvarchar](50) NULL,
	[CableLabsPackageAssetID] [nvarchar](50) NULL,
	[Workflow] nvarchar(50) NULL,
	[GatorPO] [nvarchar](50) NULL,
	[WBAnnouncement] [nvarchar](50) NULL,
	[IngredientAssetType] nvarchar(50) NULL,
	[IngredientLanguageId] [int] NULL,
	[IngredientStatus] nvarchar(50) NULL,
	[CustomerId] [int] NULL
	)