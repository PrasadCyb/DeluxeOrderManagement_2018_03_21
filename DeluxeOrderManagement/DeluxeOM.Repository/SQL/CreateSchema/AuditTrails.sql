CREATE TABLE AuditTrails
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	JObId INT NOT NULL,
	TableName NVARCHAR(100),
	ColumnName	NVARCHAR(100),
	OLDVal NVARCHAR(255),
	NewVal NVARCHAR(255)
)


