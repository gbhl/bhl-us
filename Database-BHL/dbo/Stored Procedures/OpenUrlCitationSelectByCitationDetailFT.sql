CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByCitationDetailFT]

@TitleID int = 0,
@ItemID int = 0,
@DOIName nvarchar(50) = '',
@Title nvarchar(2000) = '',
@ArticleTitle nvarchar(2000) = '',
@AuthorLast nvarchar(150) = '',
@AuthorFirst nvarchar(100) = '',
@Volume nvarchar(100) = '',
@Issue nvarchar(20) = '',
@Year nvarchar(20) = '',
@StartPage nvarchar(20) = ''

AS

BEGIN

/*

TEST CASE (WEB)

/openurl?url_ver=Z39.88-2004&ctx_ver=Z39.88-2004&rft_val_fmt=info%3Aofi%2Ffmt%3Akev%3Amtx%3Abook&rft.genre=book&rft.btitle=List%20of%20the%20Coleoptera%20of%20southern%20California%2C&rft.aufirst=Henry%20Clinton&rft.aulast=Fall&rft.date=1901

/openurl?url_ver=Z39.88-2004
&ctx_ver=Z39.88-2004
&rft_val_fmt=info%3Aofi%2Ffmt%3Akev%3Amtx%3Abook
&rft.genre=book
&rft.btitle=List%20of%20the%20Coleoptera%20of%20southern%20California%2C
&rft.aufirst=Henry%20Clinton
&rft.aulast=Fall
&rft.date=1901

TEST CASES (SQL)

exec [OpenUrlCitationSelectByCitationDetailFT] 0, 0, 
	'genera of european and northamerican bryineae', '',
	'kindberg',	'n. c',
	'',	'',	'',	''

exec [OpenUrlCitationSelectByCitationDetailFT] 0, 0, 
	'danmarks fauna, biller.', '',
	'forening',	'dansk naturhistorisk',
	'',	'',	'1908',	''

exec [OpenUrlCitationSelectByCitationDetailFT] 0, 0,
	'list of the coleoptera of southern california,', '',
	'fall',	'henry clinton',
	'',	'',	'',	''

*/

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.OpenUrlCitationSelectByCitationDetail @TitleID, @ItemID, @Title, @AuthorLast, 
			@AuthorFirst, @Volume, @Issue, @Year, @StartPage
	RETURN
END

-- ***************************************************************
-- Set up parameters

DECLARE @ItemCount int
SET @ItemCount = 0

-- Transform the search terms into full-text search phrases
DECLARE @SearchVolume nvarchar(4000)
SELECT @SearchVolume = dbo.fnGetFullTextSearchString(@Volume)

-- ***************************************************************
-- Build the temp table

CREATE TABLE #tmpOpenUrlCitation
	(
	PageID int NULL DEFAULT(0),
	ItemID int NULL DEFAULT(0),
	TitleID int NULL DEFAULT(0),
	SegmentID int NULL DEFAULT(0),
	FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
	SegmentTitle nvarchar(2000) NOT NULL DEFAULT(''),
	ContainerTitle nvarchar(2000) NOT NULL DEFAULT(''),
	PublisherPlace nvarchar(150) NOT NULL DEFAULT(''),
	PublisherName nvarchar(255) NOT NULL DEFAULT(''),
	Date nvarchar(20) NOT NULL DEFAULT(''),
	LanguageName nvarchar(20) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	EditionStatement nvarchar(450) NOT NULL DEFAULT(''),
	CurrentPublicationFrequency nvarchar(100) NOT NULL DEFAULT(''),
	Genre nvarchar(20) NOT NULL DEFAULT(''),
	Authors nvarchar(2000) NOT NULL DEFAULT(''),
	Subjects nvarchar(1000) NOT NULL DEFAULT(''),
	StartPage nvarchar(40) NOT NULL DEFAULT(''),
	EndPage nvarchar(40) NOT NULL DEFAULT(''),
	Pages nvarchar(40) NOT NULL DEFAULT(''),
	ISSN nvarchar(125) NOT NULL DEFAULT(''),
	ISBN nvarchar(125) NOT NULL DEFAULT(''),
	LCCN nvarchar(125) NOT NULL DEFAULT(''),
	OCLC nvarchar(125) NOT NULL DEFAULT(''),
	Abbreviation nvarchar(125) NOT NULL DEFAULT('')
	)


-- ***************************************************************
-- Try searching by DOI

IF (@DOIName <> '') INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByDOI @DOIName

-- ***************************************************************
-- Try searching by ItemID

IF NOT EXISTS(Select TitleID FROM #tmpOpenUrlCitation) AND (@ItemID <> 0) 
BEGIN
	INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByItemID @ItemID
END

-- ***************************************************************
-- Try searching by TitleID

IF NOT EXISTS(SELECT TitleID FROM #tmpOpenUrlCitation) AND (@TitleID <> 0) 
BEGIN
	INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByTitleID @TitleID
END

-- ***************************************************************
-- Still nothing?  Try using title and author information
IF NOT EXISTS(SELECT TitleID FROM #tmpOpenUrlCitation)
BEGIN
	IF (@ArticleTitle <> '')
	BEGIN
		-- Article title specified, search to see if it has been categorized as a monograph
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByTitleDetailFT @ArticleTitle, @AuthorLast, @AuthorFirst
	END
	ELSE
	BEGIN
		-- Search for monograph/journal with the specified title/author
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByTitleDetailFT @Title, @AuthorLast, @AuthorFirst
	END
END


-- ***************************************************************
-- If we have one or more title citations, find requested pages

IF EXISTS(SELECT TitleID FROM #tmpOpenUrlCitation)
BEGIN
	CREATE TABLE #items
	(
		ItemID int
	)

	CREATE TABLE #pages
	(
		PageID int,
		ItemID int,
		Issue nvarchar(20),
		EndIssue nvarchar(20),
		Year nvarchar(20),
		Volume nvarchar(20),
		PagePrefix nvarchar(40),
		PageNumber nvarchar(20)
	)

	-- If we have an item already, add it to the table
	INSERT INTO #items SELECT ItemID FROM #tmpOpenUrlCitation WHERE ISNULL(ItemID, 0) <> 0

	-- If we don't already have an item, see if we can find one
	IF @Volume <> '' AND NOT EXISTS(SELECT ItemID FROM #items)
	BEGIN
		-- Check the Volume field in the Page table
		-- First try an exact match
		INSERT INTO #items 
		SELECT DISTINCT
				b.BookID
		FROM	#tmpOpenUrlCitation ou
				INNER JOIN dbo.Title t ON ou.TitleID = t.TitleID
				INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
				INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	t.PublishReady = 1 
		AND		i.ItemStatusID = 40
		AND		p.Active = 1
		AND		p.Volume = @Volume		

		SELECT @ItemCount = COUNT(*) FROM #items
		IF @ItemCount = 0
		BEGIN
			-- If no items found, check the Volume field in the Item table
			INSERT INTO #items 
			SELECT DISTINCT 
					b.BookID
			FROM	#tmpOpenUrlCitation ou
					INNER JOIN dbo.Title t ON ou.TitleID = t.TitleID
					INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
					INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		(b.Volume = @Volume OR
					b.StartVolume = @Volume OR
					@Volume BETWEEN b.StartVolume and b.EndVolume)

			SELECT @ItemCount = COUNT(*) FROM #items
		END

		-- If we don't have any volumes, then try "LIKE" queries
		-- Full-text search queries for numeric values (like "2", "4", etc) proved inconsisent/unpredictable
		-- First try the Page table
		IF @ItemCount = 0
		BEGIN
			INSERT INTO #items 
			SELECT DISTINCT
					b.BookID
			FROM	#tmpOpenUrlCitation ou
					INNER JOIN dbo.Title t ON ou.TitleID = t.TitleID
					INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
					INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
					INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
					INNER JOIN dbo.Page p ON ip.PageID = p.PageID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		p.Active = 1
			AND		((ISNUMERIC(@Volume) = 1 AND 
						(p.Volume LIKE '%[^0-9]' + @Volume + '[^0-9]%' OR p.Volume LIKE @Volume + '[^0-9]%')) 
			OR		 (ISNUMERIC(@Volume) = 0 AND 
						p.Volume LIKE '%' + @Volume + '%'))

			SELECT @ItemCount = COUNT(*) FROM #items
		END
	END
	ELSE
	BEGIN
		SELECT @ItemCount = COUNT(*) FROM #items
	END

	-- Do the initial population of the #pages table
	IF @StartPage <> ''
	BEGIN
		IF @ItemCount > 0
		BEGIN
			INSERT INTO #pages
			SELECT	p.PageID, b.BookID, NULLIF(COALESCE(p.Issue, b.StartIssue), ''), NULLIF(b.EndIssue, ''), 
					p.Year, p.Volume, ISNULL(ipg.PagePrefix, ''), ISNULL(ipg.PageNumber, '')
			FROM	#items it 
					INNER JOIN dbo.Book b ON it.ItemID = b.BookID
					INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
					INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.itemID
					INNER JOIN dbo.Page p ON ip.PageID = p.PageID
					INNER JOIN IndicatedPage ipg ON p.PageID = ipg.PageID
			WHERE	ipg.PageNumber = @StartPage 
			AND		p.Active = 1
		END
		ELSE
		BEGIN
			---- If we had a volume to search for and didn't find any items, then don't look for pages
			INSERT INTO #pages
			SELECT	p.PageID, b.BookID, NULLIF(COALESCE(p.Issue, b.StartIssue), ''), NULLIF(b.EndIssue, ''), 
					p.Year, p.Volume, ISNULL(ipg.PagePrefix, ''), ISNULL(ipg.PageNumber, '')
			FROM	#tmpOpenUrlCitation ou
					INNER JOIN dbo.ItemTitle it ON ou.TitleID = it.TitleID
					INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
					INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.itemID
					INNER JOIN dbo.Page p ON ip.PageID = p.PageID
					INNER JOIN dbo.IndicatedPage ipg ON p.PageID = ipg.PageID
			WHERE	ipg.PageNumber = @StartPage 
			AND		p.Active = 1
			AND		i.ItemStatusID = 40
		END
	END

	-- If no pages were inserted based on start page, then look for a title page
	IF (SELECT COUNT(*) FROM #pages) = 0
	BEGIN
		IF @ItemCount > 0
		BEGIN
			INSERT INTO #pages
			SELECT	p.PageID, b.BookID, NULLIF(COALESCE(p.Issue, b.StartIssue), ''), NULLIF(b.EndIssue, ''), 
					p.Year, p.Volume, ISNULL(ipg.PagePrefix, ''), ISNULL(ipg.PageNumber, '')
			FROM	#items it 
					INNER JOIN dbo.Book b ON it.ItemID = b.BookID
					INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
					INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.itemID
					INNER JOIN dbo.Page p ON ip.PageID = p.PageID
					LEFT JOIN dbo.IndicatedPage ipg ON p.PageID = ipg.PageID
					INNER JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID
			WHERE	ppt.PageTypeID = 1 -- title page
			AND		p.Active = 1
		END
		ELSE
		BEGIN
			---- If we had a volume to search for and didn't find any items, then don't look for pages
			INSERT INTO #pages
			SELECT	p.PageID, b.BookID, NULLIF(COALESCE(p.Issue, b.StartIssue), ''), NULLIF(b.EndIssue, ''), 
					p.Year, p.Volume, ISNULL(ipg.PagePrefix, ''), ISNULL(ipg.PageNumber, '')
			FROM	#tmpOpenUrlCitation ou 
					INNER JOIN dbo.ItemTitle it ON ou.TitleID = it.TitleID
					INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
					INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.itemID
					INNER JOIN dbo.Page p ON ip.PageID = p.PageID
					INNER JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID
					LEFT JOIN dbo.IndicatedPage ipg ON p.PageID = ipg.PageID
			WHERE	ppt.PageTypeID = 1 -- title page
			AND		p.Active = 1
			AND		i.ItemStatusID = 40
		END
	END

	-- If an issue was specified, drop any rows from our #pages table that don't match
	IF (@Issue <> '') 
	BEGIN
		DELETE FROM #pages 
		WHERE	(Issue IS NOT NULL AND EndIssue IS NOT NULL AND @Issue NOT BETWEEN Issue and EndIssue)
		OR		(Issue IS NOT NULL AND EndIssue IS NULL AND @Issue <> Issue)
		OR		(Issue IS NULL AND EndIssue IS NOT NULL AND @Issue <> EndIssue)
	END

	-- Populate the final result set
	IF (SELECT COUNT(*) FROM #pages) > 0
	BEGIN
		-- Clear out the title citations
		TRUNCATE TABLE #tmpOpenUrlCitation

		-- Add page citations
		INSERT INTO #tmpOpenUrlCitation	(
			PageID, ItemID, TitleID, FullTitle, PublisherPlace, PublisherName, Date, LanguageName,
			Volume, EditionStatement, CurrentPublicationFrequency, Genre, Authors, Subjects,
			StartPage
			)
		SELECT	p.PageID,
				b.BookID,
				t.TitleID,
				ISNULL(t.FullTitle, ''),
				ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
				ISNULL(t.Datafield_260_b, '') AS PublisherName,
				ISNULL(p.Year, ISNULL(b.StartYear, CONVERT(nvarchar(20), ISNULL(t.StartYear, '')))) AS [Date],
				ISNULL(l.LanguageName, ''),
				CASE WHEN ISNULL(p.Volume COLLATE SQL_Latin1_General_CP1_CI_AI, '') = '' THEN ISNULL(b.Volume, '') ELSE p.Volume COLLATE SQL_Latin1_General_CP1_CI_AI END,
				ISNULL(t.EditionStatement, ''),
				ISNULL(t.CurrentPublicationFrequency, ''),
				CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
				c.Authors,
				c.Subjects,
				LTRIM(p.PagePrefix + ' ' + p.PageNumber) AS StartPage
		FROM	#pages p 
				INNER JOIN dbo.Book b ON p.ItemID = b.BookID
				INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
				INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
				INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
				LEFT JOIN dbo.Language l ON b.LanguageCode = l.LanguageCode
				INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
		WHERE	t.PublishReady = 1
	END
	ELSE 
	BEGIN 
		IF (SELECT COUNT(*) FROM #items) > 0
		BEGIN
			-- Clear out the title citations
			TRUNCATE TABLE #tmpOpenUrlCitation

			--- Add item citations
			INSERT INTO #tmpOpenUrlCitation	(
				PageID, ItemID, TitleID, FullTitle, PublisherPlace, PublisherName, Date, LanguageName,
				Volume, EditionStatement, CurrentPublicationFrequency, Genre, Authors, Subjects,
				StartPage
				)
			SELECT	0,
					b.BookID,
					t.TitleID,
					ISNULL(t.FullTitle, ''),
					ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
					ISNULL(t.Datafield_260_b, '') AS PublisherName,
					ISNULL(b.StartYear, CONVERT(nvarchar(20), ISNULL(t.StartYear, ''))) AS [Date],
					ISNULL(l.LanguageName, ''),
					ISNULL(b.Volume, ''),
					ISNULL(t.EditionStatement, ''),
					ISNULL(t.CurrentPublicationFrequency, ''),
					CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
					c.Authors,
					c.Subjects,
					'' AS StartPage
			FROM	#items itm
					INNER JOIN dbo.Book b ON itm.ItemID = b.BookID
					INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
					INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
					INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
					LEFT JOIN dbo.Language l ON b.LanguageCode = l.LanguageCode
					INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
			WHERE	t.PublishReady = 1
		END
	END

	-- If a year was specified, drop any rows that don't match
	IF @Year <> ''
	BEGIN
		--if deleting based on year would wipe out all of our results, then don't filter on year
		IF (SELECT COUNT(*) FROM #tmpOpenUrlCitation WHERE [Date] = @Year) > 0
			DELETE FROM #tmpOpenUrlCitation WHERE [Date] <> @Year
	END
END


-- ***************************************************************
-- If we weren't looking for a specific Title or Item, search for matching segments
IF (@TitleID = 0 AND @ItemID = 0)
BEGIN
	IF (@ArticleTitle <> '')
	BEGIN
		-- Article title specified, search to see if it has been categorized as a monograph
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectBySegmentDetailFT 
			@ArticleTitle, @Title, @AuthorLast, @AuthorFirst, @Volume, @Issue, @Year, @StartPage
	END
	ELSE
	BEGIN
		-- No article title specified, use the "@Title" value instead
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectBySegmentDetailFT 
			@Title, '', @AuthorLast, @AuthorFirst, @Volume, @Issue, @Year, @StartPage
	END
END


-- ***************************************************************
-- Add the title identifiers to the final result set
UPDATE	#tmpOpenUrlCitation
SET		ISSN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'ISSN'

UPDATE	#tmpOpenUrlCitation
SET		ISSN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'eISSN'
WHERE	t.ISSN = ''

UPDATE	#tmpOpenUrlCitation
SET		ISBN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'ISBN'

UPDATE	#tmpOpenUrlCitation
SET		LCCN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'DLC'

UPDATE	#tmpOpenUrlCitation
SET		OCLC = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'OCLC'

UPDATE	#tmpOpenUrlCitation
SET		Abbreviation = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'Abbreviation'


-- ***************************************************************
-- Add the segment identifiers to the final result set
UPDATE	#tmpOpenUrlCitation
SET		ISSN = si.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN Segment s ON t.SegmentID = s.SegmentID
		INNER JOIN ItemIdentifier si ON s.ItemID = si.ItemID
		INNER JOIN dbo.Identifier i
			ON si.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'ISSN'
AND		t.ISSN = ''

UPDATE	#tmpOpenUrlCitation
SET		ISSN = si.IdentifierValue
FROM	#tmpOpenUrlCitation t 
		INNER JOIN Segment s ON t.SegmentID = s.SegmentID
		INNER JOIN ItemIdentifier si ON s.ItemID = si.ItemID
		INNER JOIN dbo.Identifier i
			ON si.IdentifierID = i.IdentifierID
			AND i.IdentifierName = 'eISSN'
AND		t.ISSN = ''


-- ***************************************************************
-- Return the final result set

SELECT DISTINCT t.*, title.SortTitle, it.ItemSequence, s.SortTitle AS SegmentSortTitle
FROM	#tmpOpenUrlCitation t 
		LEFT JOIN dbo.Title ON t.TitleID = title.TitleID
		LEFT JOIN dbo.Book b ON t.ItemID = b.BookID
		LEFT JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND b.ItemID = it.ItemID
		LEFT JOIN dbo.Segment s ON t.SegmentID = s.SegmentID
ORDER BY s.SortTitle, title.SortTitle, it.ItemSequence, t.Date, t.StartPage

END

GO
