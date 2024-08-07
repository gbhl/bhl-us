SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiItemSelectByItemID]

@ItemID int

AS 

SET NOCOUNT ON

DECLARE @RedirItemID int
SELECT @RedirItemID = RedirectBookID FROM dbo.Book WHERE BookID = @ItemID

IF (@RedirItemID IS NOT NULL)
	exec dbo.ApiItemSelectByItemID @RedirItemID
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
			i.CreationDate
	FROM	dbo.Item i 
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.vwItemPrimarytitle pt ON i.ItemID = pt.ItemID
			LEFT JOIN dbo.[Language] l ON b.LanguageCode = l.LanguageCode
			INNER JOIN dbo.ItemSource s ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.SearchCatalog c ON c.ItemID = b.BookID AND c.TitleID = pt.TitleID
	WHERE	b.BookID = @ItemId
	AND		i.ItemStatusID = 40

GO

