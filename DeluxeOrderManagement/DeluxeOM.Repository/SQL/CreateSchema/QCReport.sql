CREATE TABLE [dbo].[QCReport](
		Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		Title NVARCHAR(255),
		AppleId NVARCHAR(50),
		VendorId NVARCHAR(100),
		[ImportedDate] [nvarchar](50) NULL,
		[ComponentType]  nvarchar(50)  NULL,
		LanguageId INT  NULL,
		Territory NVARCHAR(512) NULL,
		[ComponentQuality] nvarchar(50) NULL,
		[QAStatus] nvarchar(50) NULL,
		CustomerId Int Not null
)