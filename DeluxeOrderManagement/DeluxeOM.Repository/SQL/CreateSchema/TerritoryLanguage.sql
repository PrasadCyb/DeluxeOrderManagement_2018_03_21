CREATE TABLE [dbo].[TerritoryLanguage]
(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[TerritoryId] [int] NOT NULL,
	[LanguageSeq] [int] NOT NULL,
	[LanguageId] [int] NOT NULL

)

GO
--Primary Language


INSERT INTO [TerritoryLanguage]
(
	TerritoryId,
	LanguageSeq,
	LanguageId
)
SELECT T.Id AS TerritoryId,
		1 As Sequence,
		L.Id AS LanguageId
FROM [TerritoryMapping_Staging] STG 
	INNER JOIN [Territory] T ON (T.[WBTerritory] = STG.[WB Territory] AND T.[AppleTerritory] = STG.[Apple Territory])
	INNER JOIN [Language] L ON (STG.[Primary Language] = L.[Name] AND STG.[Primary Language Code] = L.[Locale])

Go

--Secondary Language 
INSERT INTO [TerritoryLanguage]
(
	TerritoryId,
	LanguageSeq,
	LanguageId
)
SELECT T.Id AS TerritoryId,
		2 As Sequence,
		L.Id AS LanguageId
FROM [TerritoryMapping_Staging] STG 
	INNER JOIN Territory T ON (T.WBTerritory = STG.[WB Territory] AND T.AppleTerritory = STG.[Apple Territory])
	INNER JOIN Language L ON (STG.[Secondary Language] = L.Name AND STG.[Secondary Language Code] = L.Locale AND STG.[Secondary Language] IS NOT NULL)
	
GO

--Tertiary language

INSERT INTO [TerritoryLanguage]
(
	TerritoryId,
	LanguageSeq,
	LanguageId
)
SELECT T.Id AS TerritoryId,
		3 As Sequence,
		L.Id AS LanguageId
FROM [TerritoryMapping_Staging] STG 
	INNER JOIN Territory T ON (T.WBTerritory = STG.[WB Territory] AND T.AppleTerritory = STG.[Apple Territory])
	INNER JOIN Language L ON (STG.[Tertiary Language] = L.Name AND STG.[Tertiary Language Code] = L.Locale AND STG.[Tertiary Language] IS NOT NULL)