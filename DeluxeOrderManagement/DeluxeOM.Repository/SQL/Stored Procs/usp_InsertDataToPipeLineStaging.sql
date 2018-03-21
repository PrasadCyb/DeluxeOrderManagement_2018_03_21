ALTER PROCEDURE usp_InsertDataToPipeLineStaging @Path nvarchar(250) 
AS
BEGIN
	IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'dbo.[PipelineOrder_Staging]') and OBJECTPROPERTY(id, N'IsTable') = 1)
		DROP TABLE dbo.[PipelineOrder_Staging]
	ELSE
	DECLARE @sql NVARCHAR(2000)
	SET @sql = N'Select * into PipelineOrder_Staging FROM OPENROWSET(''Microsoft.ACE.OLEDB.12.0'',''Excel 12.0;Database=' + @Path + '; TypeGuessRows=0; HDR=YES; IMEX=1'',''SELECT * FROM [Sheet1$]'')'
	EXECUTE sp_executesql @sql
END
