
CREATE TABLE [LookupType]
(
	[Id] [int] NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] [nvarchar](50) NOT NULL,
	Description nvarchar(256) NULL
)

--Insert

INSERT INTO LookupType
(
	Name
)
VALUES
('poOrderStatus'),
('aplQCComponentType'),
('aplQCComponentQuality'),
('aplQCQAStatus'),
('Provider'),
('aplPriceLive'),
('annChnageStatus'),
('annChangeContext'),
('annContentType'),
('LanguageType'),
('SalesChannel'),
('Format'),
('annClientAvailStatus'),
('Distributor'),
('Provider'),
('poFileType'),
('vidStatus'),
('vidTitleCategory'),
('gridCategory'),
('gridTitleStatus')