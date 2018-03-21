
CREATE Table Language
(
	Id	INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name [nvarchar](50) NOT NULL,
	Locale [nvarchar](50)  NULL
)

GO

--Insert Primary Language
INSERT INTO Language 
(
	Name,
	Locale
)
SELECT DISTINCT [Primary Language], [Primary Language Code] FROM TerritoryMapping_Staging WHERE [Primary Language] IS NOT NULL AND [Primary Language Code] IS NOT NULL

GO
--Insert Secondary Language

INSERT INTO Language 
(
	Name,
	Locale
)
SELECT DISTINCT [Secondary Language], [Secondary Language Code] FROM TerritoryMapping_Staging 
WHERE ([Secondary Language] IS NOT NULL AND [Secondary Language Code] IS NOT NULL)
	AND [Secondary Language] NOT IN (SELECT Name FROM Language)
	--AND [Secondary Language Code] NOT IN (SELECT Locale FROM Language)

GO

--Insert Tertiary Language

INSERT INTO Language 
(
	Name,
	Locale
)
SELECT DISTINCT [Tertiary Language], [Tertiary Language Code] FROM TerritoryMapping_Staging 
WHERE ([Tertiary Language] IS NOT NULL AND [Tertiary Language Code] IS NOT NULL)
	AND [Tertiary Language] NOT IN (SELECT Name FROM Language)
	--AND [Tertiary Language Code] NOT IN (SELECT Locale FROM Language)


