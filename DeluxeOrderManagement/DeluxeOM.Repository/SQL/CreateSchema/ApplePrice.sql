CREATE TABLE [dbo].[ApplePrice](
	Id INT PRIMARY KEY NOT NULL,
	[VidId] [int] NOT NULL,
	[TerritoryId] [int] NOT NULL,
	[LiveVODHD] [int] NOT NULL,
	[LiveVODSD] [int] NOT NULL,
	[LiveESTHD] [int] NOT NULL,
	[LiveESTSD] [int] NOT NULL,
	[PreOrderDate] [datetime] NULL,
	[ClearedforSaleEST] [bit] NOT NULL,
	[EnableESTHD] [bit] NOT NULL,
	[ESTStartDate] [datetime] NULL,
	[ClearedforVOD] [bit] NOT NULL,
	[EnableVODHD] [bit] NOT NULL,
	[AvailForVOD] [datetime] NULL
)

GO



--Staging


CREATE TABLE ApplePrice_STAGING
(
ContentProvider	nvarchar(255)	,
Title	nvarchar(255)	,
VendorID	nvarchar(255)	,
AppleID	nvarchar(255)	,
FilmType	nvarchar(255)	,
Territory	nvarchar(255)	,
LiveVODHD	nvarchar(255)	,
LiveVODSD	nvarchar(255)	,
LiveESTHD	nvarchar(255)	,
LiveESTSD	nvarchar(255)	,
AllowPre_Order	nvarchar(255)	,
Pre_OrderDate	nvarchar(255)	,
SDWholesalePriceTier	nvarchar(255)	,
SDWholesalePriceTierStartDate	nvarchar(255)	,
SDWholesalePriceTierEndDate	nvarchar(255)	,
HDWholesalePriceTier	nvarchar(255)	,
HDWholesalePriceTierStartDate	nvarchar(255)	,
HDWholesalePriceTierEndDate	nvarchar(255)	,
ClearedforSaleEST	nvarchar(255)	,
EnableESTHD	nvarchar(255)	,
ESTStartDate	nvarchar(255)	,
ESTEndDate	nvarchar(255)	,
VODType	nvarchar(255)	,
ClearedforVOD	nvarchar(255)	,
EnableVODHD	nvarchar(255)	,
AvailForVOD	nvarchar(255)	,
UnavailForVOD	nvarchar(255)	,
PhysicalReleaseDate	nvarchar(255)	,
HDPhysicalReleasedate	nvarchar(255)	,
HDESTEarlyStartdate	nvarchar(255)	,
Genres	nvarchar(255)	,
Rating	nvarchar(255)	,
RatingReason	nvarchar(255)	,
ProductionCompany	nvarchar(255)	,
Copyright	nvarchar(255)	,
CustomPage	nvarchar(255)		
)