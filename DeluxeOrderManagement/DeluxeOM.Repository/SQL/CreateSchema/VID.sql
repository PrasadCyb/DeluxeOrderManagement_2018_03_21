
CREATE TABLE [dbo].[VID]
(
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL ,
	[VIDStatus] NVARCHAR(50)  NULL,
	[Bundle] bit NULL,
	[Extras] [bit] NULL,
	TitleName NVARCHAR(255) NULL,
	[MPM] [nvarchar](50) NULL,
	[VideoVersion] [nvarchar](50)  NULL,
	[EditName] NVARCHAR(255) NULL,
	[VersionEIDR] [nvarchar](255)  NULL,
	AppleId	NVARCHAR(50) NULL,
	VendorId NVARCHAR(50) NULL,
	[TitleCategory] NVARCHAR(50) NULL,
	CreatedOn DATETIME NULL,
	CreatedBy NVARCHAR(100),
	ModifiedOn	DATETIME,
	ModifiedBy NVARCHAR(100)
)




 INSERT INTO [VID]
           (
		   [VIDStatus]
           ,[Bundle]
           ,[Extras]
           ,[TitleName]
           ,[MPM]
           ,[VideoVersion]
           ,[EditName]
           ,[VersionEIDR]
           ,[AppleId]
           ,[VendorId]
           ,[TitleCategory]
		   ,CreatedOn
			,CreatedBy
			,ModifiedOn
			,ModifiedBy
		)
     SELECT 
		   STG.[VID Status] AS VIDStatus,
           CASE STG.Bundle
                WHEN 'FALSE' THEN 0
                WHEN 'TRUE' THEN 1
				ELSE NULL
            END AS Bundle,
			CASE STG.Extras
                WHEN 'FALSE' THEN 0
                WHEN 'TRUE' THEN 1
				ELSE NULL
            END AS Extras,
			CAST(STG.[Worldwide Title] AS NVARCHAR(255)) AS Title,
			CAST(STG.MPM AS NVARCHAR(50)) AS MPM,
			CAST(ISNULL(STG.[Video Version], '') AS NVARCHAR(50)) AS VideoVersion,
			--CASE WHEN (STG.[Edit Name] IS NULL OR STG.[Edit Name] = '') THEN 0 ELSE 1 END AS LocalEdit,
			STG.[Edit Name] AS LocalEdit,
			CAST(ISNULL(STG.[Version EIDR], '') AS NVARCHAR(255)) AS VersionEidr,
			CAST(STG.[Apple ID] AS NVARCHAR(50)) AS AppleId,
			CAST(STG.[Vendor ID] AS NVARCHAR(50)) AS VendorId,
			STG.[Title Category] AS TitleCategory,
			GETDATE(),
			'IMPORT',
			GETDATE(),
			'IMPORT'
		FROM VID_STAGING STG 
		--LEFT JOIN  (SELECT L.ID, L.NAme FROM Lookup L INNER JOIN LookupType LT ON L.TypeId = LT.Id AND LT.Name = 'vidStatus') VidStatus ON VidStatus.Name = STG.[VID Status]
		--LEFT JOIN (SELECT L.ID, L.NAme FROM Lookup L INNER JOIN LookupType LT ON L.TypeId = LT.Id AND LT.Name = 'vidTitleCategory') TitleCat ON TitleCat.Name = STG.[Title Category]
		WHERE STG.[VID Status] IN ('PRIMARY', 'ACTIVE')



