CREATE TABLE VID_DETAILS
(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	VidId INT ,
	VIDStatus INT,
	Bundle BIT,
	Extras BIT,
	MPM NVARCHAR(100),
	AppleId NVARCHAR(100),
	VendorId NVARCHAR(100),
	TitleCategoryId INT
)

INSERT INTO [VID_Details]
           (
			VidId,
			VIDStatus,
			Bundle,
			Extras,
			MPM,
			AppleId,
			VendorId,
			TitleCategoryId
		)
     SELECT 
			V.Id AS VidId,
			VidStatus.Id AS VIDStatusId,
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
			CAST(STG.MPM AS NVARCHAR(50)) AS MPM,
			CAST(STG.[Apple ID] AS NVARCHAR(50)) AS AppleId,
			CAST(STG.[Vendor ID] AS NVARCHAR(50)) AS VendorId,
			TitleCat.Id AS TitleCategoryId
		FROM VID_STAGING STG 
		LEFT JOIN VID V ON STG.[Worldwide Title] = V.TitleName AND STG.[Video Version] = V.VideoVersion AND STG.[Edit Name] = V.EditName
		LEFT JOIN  (SELECT L.ID, L.NAme FROM Lookup L INNER JOIN LookupType LT ON L.TypeId = LT.Id AND LT.Name = 'vidStatus') VidStatus
				ON VidStatus.Name = STG.[VID Status]
			LEFT JOIN (SELECT L.ID, L.NAme FROM Lookup L INNER JOIN LookupType LT ON L.TypeId = LT.Id AND LT.Name = 'vidTitleCategory') TitleCat
				ON TitleCat.Name = STG.[Title Category]
		WHERE STG.[VID Status] IN ('PRIMARY', 'ACTIVE')