SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiItemSelectByTitleId]

@TitleID int

AS 

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectTitleID FROM dbo.Title WHERE TitleID = @TitleID

IF (@RedirID IS NOT NULL)
	exec dbo.ApiItemSelectByTitleId @RedirID
ELSE
	SELECT	b.BookID AS ItemID,
			pt.TitleID AS PrimaryTitleID,
			c.FirstPageID AS ThumbnailPageID,
			s.SourceName,
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
			ISNULL(b.ExternalUrl, '') AS ExternalUrl
	FROM	dbo.Item i 
			INNER JOIN dbo.vwItemPrimarytitle pt ON i.ItemID = pt.ItemID
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			LEFT JOIN dbo.[Language] l ON b.LanguageCode = l.LanguageCode
			INNER JOIN [dbo].[ItemTitle] ti ON i.ItemID = ti.ItemID
			INNER JOIN [dbo].[Title] t ON ti.[TitleID] = t.[TitleID]
			INNER JOIN dbo.ItemSource s ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = b.BookID
	WHERE	t.TitleId = @TitleId 
	AND		i.ItemStatusID = 40
	AND		t.PublishReady = 1
	ORDER BY ti.ItemSequence
	

GO
