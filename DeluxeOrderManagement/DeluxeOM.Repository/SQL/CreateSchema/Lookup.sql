CREATE TABLE Lookup
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	LookupId	int not null ,
	Name	nvarchar(50) not null,
	TypeId	int not null
)



INSERT INTO [Lookup]
(
	LookupId,
	Name,
	TypeId
)
VALUES
(
	1,
	'N/A',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poOrderStatus')
),
(
	2,
	'Request Received',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poOrderStatus')
),
(
	3,
	'Processing',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poOrderStatus')
),
(
	4,
	'Complete',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poOrderStatus')
),
(
	5,
	'Cancelled',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poOrderStatus')
),


(
	1,
	'Audio',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	2,
	'Audio Description',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	3,
	'Captions',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	4,
	'Chapters',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	5,
	'Dub Credit',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	6,
	'Forced Subtitles',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	7,
	'Preview Film',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	8,
	'Subtitles',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),
(
	9,
	'Video',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentType')
),


(
	1,
	'2',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentQuality')
),
(
	2,
	'2.0,5.1',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentQuality')
),
(
	3,
	'SD',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentQuality')
),
(
	4,
	'SD,HD',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCComponentQuality')
),


(
	1,
	'Approved',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCQAStatus')
),
(
	2,
	'Empty',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCQAStatus')
),
(
	3,
	'In Review',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCQAStatus')
),
(
	4,
	'Rejected',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplQCQAStatus')
),


(
	1,
	'Live',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplPriceLive')
),
(
	2,
	'Not Live',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplPriceLive')
),
(
	3,
	'Ready',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'aplPriceLive')
),


(
	1,
	'Change',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChnageStatus')
),
(
	2,
	'New',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChnageStatus')
),
(
	3,
	'No Change',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChnageStatus')
),
(
	4,
	'Price Change',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChnageStatus')
),
(
	5,
	'Price End',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChnageStatus')
),
(
	6,
	'Price Start',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChnageStatus')
),


(
	1,
	'Re-Avail',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChangeContext')
),
(
	2,
	'Rights Expiration',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annChangeContext')
),	


(
	1,
	'Feature',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annContentType')
),


(
	1,
	'Audio',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'LanguageType')
),
(
	2,
	'Sub',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'LanguageType')
),
(
	3,
	'Sub & Audio',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'LanguageType')
),


(
	1,
	'EST',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'SalesChannel')
),
(
	2,
	'POEST',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'SalesChannel')
),
(
	3,
	'VOD',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'SalesChannel')
),


(
	1,
	'HD',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'Format')
),
(
	2,
	'SD',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'Format')
),


(
	1,
	'Cancelled',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annClientAvailStatus')
),
(
	2,
	'Confirmed',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annClientAvailStatus')
),
(
	3,
	'Tentative',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'annClientAvailStatus')
),


(
	1,
	'Apple iTunes',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'Distributor')
),


(
	1,
	'AOU',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	2,
	'AOU & MOU',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	3,
	'AOU/MOU',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	4,
	'APO',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	5,
	'APO MOU',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	6,
	'ML',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	7,
	'MOC',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	8,
	'MOU',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	9,
	'N/A',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	10,
	'None',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	11,
	'SINGLE',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	12,
	'XAOU',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'poFileType')
),
(
	1,
	'ACTIVE',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'vidStatus')
),
(
	2,
	'PRIMARY',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'vidStatus')
),
(
	3,
	'PRIMARY/Not Active',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'vidStatus')
),
(
	4,
	'RETIRED',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'vidStatus')
),
(
	1,
	'Catalog',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'vidTitleCategory')
),
(
	2,
	'Franchise',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'vidTitleCategory')
),
(
	1,
	'Catalog',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridCategory')
),
(
	2,
	'Catalog Promo',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridCategory')
),
(
	3,
	'Franchise',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridCategory')
),
(
	4,
	'New Release',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridCategory')
),
(
	1,
	'Cancelled',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridTitleStatus')
),
(
	2,
	'Confirmed',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridTitleStatus')
),
(
	3,
	'Tentative',
	(SELECT TOP 1 Id FROM LookupType WHERE Name = 'gridTitleStatus')
)