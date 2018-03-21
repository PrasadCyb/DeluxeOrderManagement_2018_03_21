Create Function fnLookup(@Type NVARCHAR(100) )
RETURNS  @lookup TABLE 
(
    -- columns returned by the function
    ID INT NOT NULL,
    Name nvarchar(255) NOT NULL
)
AS
Begin
		INSERT INTO @lookup SELECT ID, Name 
			From Lookup 
			WHERE TypeId IN (SELECT TOP 1 Id FROM LookupType WHERE Name = @Type)

			RETURN
END


SELECT * FROM fnLookup('gridCategory')