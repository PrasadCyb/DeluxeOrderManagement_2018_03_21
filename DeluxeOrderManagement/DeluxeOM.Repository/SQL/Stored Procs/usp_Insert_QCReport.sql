CREATE PROCEDURE [dbo].[usp_Insert_QCReport]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    /*USP_INSERT_APPLEQC*/
	/* Not necessary as CountryCode is not important for required ComponentType
		--Insert Territory
		INSERT INTO Territory
		(
			CountryCode
		)
		SELECT TERRITORY FROM QCReport_Staging 
		WHERE TERRITORY IS NOT NULL AND TERRITORY  <> '' AND TERRITORY NOT IN 
		(SELECT CountryCode FROM Territory)
	

		--Insert Language
		INSERT INTO Language 
		(
			Locale
		)
		SELECT LOCALE_CODE FROM QCReport_Staging
		WHERE LOCALE_CODE  IS NOT NULL AND LOCALE_CODE <> '' AND LOCALE_CODE NOT IN
		(SELECT Locale FROM Language)
		*/
		--Need to Reload QCReport
		TRUNCATE TABLe QCReport

		/*
		INSERT INTO QCReport
		(
			Title,
			AppleId,
			VendorId,
			ImportedDate,
			ComponentType,
			LanguageId,
			TerritoryId,
			ComponentQuality,
			QAStatus,
			CustomerId
		)
		SELECT DISTINCT
			QC.TITLE, 
			QC.APPLE_ID, 
			QC.VENDOR_ID,
			QC.IMPORTED_DATE,
			QC.COMPONENT_TYPE,
			L.Id AS LanguageId,
			T.Id AS TerritoryId,
			QC.COMPONENT_QUALITY,
			QC_STATUS,
			1
		from QCReport_Staging QC 
		INNER JOIN (SELECT distinct TitleName, VideoVersion, (CASE WHEN EditName IS NULL OR EditName = '' OR EditName = 'No' Then 'No' ELSE 'Yes' END) AS LocalEdit,
						VendorId,
						AppleId
						FROM VID 
						WHERE (VendorId IS NOT NULL OR VendorId <> '') AND (AppleId IS NOT NULL OR AppleId <> '')
					) V 
					ON QC.VENDOR_ID = V.VendorId AND QC.APPLE_ID = V.AppleId
				INNER JOIN Territory T ON QC.TERRITORY like  '%' + t.CountryCode + '%' 
				INNER JOIN Language L ON SUBSTRING(L.Locale, 1, 2) = SUBSTRING(QC.LOCALE_CODE, 1, 2)
		WHERE QC.QC_STATUS LIKE '%Approved%' AND QC.COMPONENT_TYPE IN ('AUDIO', 'SUBTITLES')

		*/

		
		INSERT INTO QCReport
		(
			Title,
			AppleId,
			VendorId,
			ImportedDate,
			ComponentType,
			LanguageId,
			Territory,
			ComponentQuality,
			QAStatus,
			CustomerId
		)
		SELECT distinct 
			QC.TITLE, 
			CAST(LTRIM(STR(QC.Apple_ID,100)) AS NVARCHAR(100)),
			QC.VENDOR_ID,
			QC.IMPORTED_DATE,
			QC.COMPONENT_TYPE,
			L.Id AS LanguageId,
			QC.TERRITORY,
			QC.COMPONENT_QUALITY,
			QC_STATUS,
			1
		from QCReport_Staging QC 
		--LEFT JOIN Territory T ON QC.TERRITORY like  '%' + t.CountryCode + '%' 
		INNER JOIN Language L ON SUBSTRING(L.Locale, 1, 2) = SUBSTRING(QC.LOCALE_CODE, 1, 2)

		END