ALTER PROCEDURE usp_InsertDataToAnnouncementStaging @Path nvarchar(250) 
AS
BEGIN
	IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'dbo.[Announcements_STAGING]') and OBJECTPROPERTY(id, N'IsTable') = 1)
		DROP TABLE dbo.[Announcements_STAGING]
	ELSE
	DECLARE @sql NVARCHAR(2000)
	SET @sql = N'Select * into Announcements_STAGING FROM OPENROWSET(''Microsoft.ACE.OLEDB.12.0'',''Excel 12.0;Database=' + @Path + '; TypeGuessRows=0; HDR=YES; IMEX=1'',''SELECT * FROM [Sheet1$]'')'
	EXECUTE sp_executesql @sql
END