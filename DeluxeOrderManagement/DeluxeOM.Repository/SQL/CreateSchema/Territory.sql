--DROP TABLE [Territory]
CREATE TABLE [dbo].[Territory]
(
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	WBTerritory	NVARCHAR(255)  NULL,
	CountryCode NVARCHAR(50)  NULL,
	AppleTerritory	NVARCHAR(255)  NULL,
	[RegionId] [int]  NULL,
	[PACKAGE] [nvarchar](255)  NULL,
	[GROUP] [nvarchar](255) NULL
)


--Insert Territory


INSERT INTO [dbo].[Territory]
        (
		   [WBTerritory]
           ,[CountryCode]
           ,[AppleTerritory]
           ,[RegionId]
           ,[PACKAGE]
           ,[GROUP]
		)
     SELECT 
			CAST(TS.[WB Territory] AS NVARCHAR(255)),
			CAST(TS.[ISO-3166 Country Code] AS NVARCHAR(50)),
			CAST(TS.[Apple Territory] AS NVARCHAR(255)),
			R.Id,
			CAST(TS.[Package] AS NVARCHAR(255)),
			CAST(TS.[Group] AS NVARCHAR(255))
		   FROM TerritoryMapping_Staging TS LEFT JOIN Region R ON TS.[Region] = R.[Name]
