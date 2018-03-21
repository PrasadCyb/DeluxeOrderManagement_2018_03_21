CREATE TABLE SalesChannel
(
[ID] [int] PRIMARY KEY NOT NULL IDENTITY(1,1),
[Name] NVARCHAR(50)
)

INSERT INTO SalesChannel
(Name)
VALUES
(
	'VOD'
),
(
	'EST'
),
(
	'POEST'
)
