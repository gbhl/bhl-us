SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiItemSelectByBarcode]

@Barcode nvarchar(200)

AS 

SET NOCOUNT ON

DECLARE @RedirBarcode nvarchar(200)
SELECT @RedirBarcode = Barcode FROM Book 
WHERE BookID IN (SELECT RedirectBookID FROM dbo.Item WHERE Barcode = @Barcode)

IF (@RedirBarcode IS NOT NULL)
	exec dbo.ApiItemSelectByBarcode @RedirBarcode
ELSE
	SELECT	b.BookID AS ItemID,
			pt.TitleID AS PrimaryTitleID,
			c.FirstPageID AS ThumbnailPageID,
			s.SourceName,
			b.IsVirtual,
			b.Barcode,
			b.Volume,
			b.StartYear AS [Year],
			b.EndYear,
			i.ItemDescription,
			c.ItemContributors AS InstitutionName,
			b.Sponsor,
			ISNULL(l.LanguageName, '') AS Language,
			ISNULL(b.LicenseUrl, '') AS LicenseUrl,
			ISNULL(b.Rights, '') AS Rights,
			ISNULL(b.DueDiligence, '') AS DueDiligence,
			ISNULL(b.CopyrightStatus, '') AS CopyrightStatus,
			'' AS CopyrightRegion,
			ISNULL(b.ExternalUrl, '') AS ExternalUrl,
			b.CreationDate
	FROM	dbo.Item i 
			INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			LEFT JOIN dbo.Language l ON b.LanguageCode = l.LanguageCode
			INNER JOIN dbo.ItemSource s ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.SearchCatalog c ON c.ItemID = b.BookID AND c.TitleID = pt.TitleID
	WHERE	b.Barcode = @Barcode
	AND		i.ItemStatusID = 40

GO
