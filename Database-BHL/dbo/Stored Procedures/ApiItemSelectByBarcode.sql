CREATE PROCEDURE [dbo].[ApiItemSelectByBarcode]

@Barcode nvarchar(40)

AS 

SET NOCOUNT ON

DECLARE @RedirBarcode nvarchar(40)
SELECT @RedirBarcode = Barcode FROM Item 
WHERE ItemID IN (SELECT RedirectItemID FROM dbo.Item WHERE Barcode = @Barcode)

IF (@RedirBarcode IS NOT NULL)
	exec dbo.ApiItemSelectByBarcode @RedirBarcode
ELSE
	SELECT	i.ItemID,
			i.PrimaryTitleID,
			c.FirstPageID AS ThumbnailPageID,
			s.SourceName,
			i.Barcode,
			i.Volume,
			i.Year,
			i.ItemDescription,
			c.ItemContributors AS InstitutionName,
			i.Sponsor,
			ISNULL(l.LanguageName, '') AS Language,
			ISNULL(i.LicenseUrl, '') AS LicenseUrl,
			ISNULL(i.Rights, '') AS Rights,
			ISNULL(i.DueDiligence, '') AS DueDiligence,
			ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
			ISNULL(i.CopyrightRegion, '') AS CopyrightRegion,
			ISNULL(i.ExternalUrl, '') AS ExternalUrl
	FROM	dbo.Item i 
			LEFT JOIN dbo.Language l ON i.LanguageCode = l.LanguageCode
			INNER JOIN dbo.ItemSource s ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.SearchCatalog c ON c.ItemID = i.ItemID AND c.TitleID = i.PrimaryTitleID
	WHERE	i.Barcode = @Barcode
	AND		i.ItemStatusID = 40
