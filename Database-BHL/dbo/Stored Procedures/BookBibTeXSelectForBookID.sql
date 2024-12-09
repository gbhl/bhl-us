CREATE PROCEDURE [dbo].[BookBibTeXSelectForBookID]

@BookID INT,
@TitleID INT = NULL

AS
BEGIN

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectBookID FROM dbo.Book WHERE BookID = @BookID

IF (@RedirID IS NOT NULL)
	-- If redirected, don't limit by TitleID (default to primary title)
	exec dbo.BookBibTeXSelectForBookID @RedirID, NULL
ELSE
	SELECT	'bhlitem' + CONVERT(NVARCHAR(10), b.BookID) AS CitationKey,
			'https://www.biodiversitylibrary.org/item/' + CONVERT(NVARCHAR(10), b.BookID) AS Url,
			'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Note,
			t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
			ISNULL(t.PublicationDetails, '') AS Publisher,
			CASE WHEN b.StartYear IS NULL THEN ISNULL(t.Datafield_260_c, '') ELSE b.StartYear END AS [Year],
			ISNULL(b.Volume, '') AS Volume , ISNULL(b.CopyrightStatus, '') AS CopyrightStatus,
			c.Authors,
			(	SELECT	COUNT(pg.PageID)
				FROM	dbo.Page pg
						INNER JOIN dbo.ItemPage ipg ON pg.PageID = ipg.PageID
						INNER JOIN dbo.Book bk ON ipg.ItemID = bk.ItemID
				WHERE	bk.BookID = b.BookID
			) AS Pages,
			c.Subjects AS Keywords
	FROM	dbo.Title t
			INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND ((it.IsPrimary = 1 AND @TitleID IS NULL) OR it.TitleID = @TitleID)
			INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
	WHERE	t.PublishReady = 1
	AND		i.ItemStatusID = 40
	AND		b.BookID = @BookID
	ORDER BY it.ItemSequence
			
END

GO
