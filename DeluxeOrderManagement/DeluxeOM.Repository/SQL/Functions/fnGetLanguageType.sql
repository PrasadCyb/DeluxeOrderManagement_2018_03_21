USE [DeluxeOrderManagement]
GO

/****** Object:  UserDefinedFunction [dbo].[fnGetLanguageType]    Script Date: 11/27/2017 7:19:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnGetLanguageType] (@languageType NVARCHAR(50), @Language NVARCHAR(50), @Territory NVARCHAR(50))  
RETURNS NVARCHAR(50)  
AS  
BEGIN  
		DECLARE @langtype NVARCHAR(50);  

		SET @langtype = @languageType

		IF @Territory = 'US' AND @Language = 'English' AND (@languageType IN ('Audio' ,'Audio_Descripion'))
			SET @langtype = 'Audio'
		ELSE IF @Language IN ('Parisian French', 'German') AND @languageType IN ('Audio', 'DUB_CREDIT')
			SET @langtype = 'Audio'
		ELSE IF @languageType IN ('Audio', 'Forced_Subtitles')
			SET @langtype = 'Audio'
		ELSE IF @Language = 'English' AND @languageType = 'CAPTIONS'
			SET @langtype = 'Sub'
		ELSE IF @languageType = 'SUBTITLES'
			SET @langtype = 'Sub'

     RETURN(@langtype);  
END;  
GO


