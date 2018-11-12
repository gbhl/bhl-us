CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByCitationDetail]

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

exec [OpenUrlCitationSelectByCitationDetail] 0, 0, 
	'genera of european and northamerican bryineae', '',
	'kindberg',	'n. c',
	'',	'',	'',	''

exec [OpenUrlCitationSelectByCitationDetail] 0, 0, 
	'danmarks fauna, biller.', '',
	'forening',	'dansk naturhistorisk',
	'',	'',	'1908',	''

exec [OpenUrlCitationSelectByCitationDetail] 0, 0,
	'list of the coleoptera of southern california,', '',
	'fall',	'henry clinton',
	'',	'',	'',	''

*/

SET NOCOUNT ON

-- ***************************************************************
-- Set up parameters

DECLARE @ItemCount int
SET @ItemCount = 0

-- Need to check for both "Last, First" and "First Last"
DECLARE @Author nvarchar(255)
DECLARE @AuthorAlt nvarchar(255)
SET @Author = ''
SET @AuthorAlt = ''

IF (@AuthorFirst = '' AND @AuthorLast <> '') SET @Author = @AuthorLast + '%'
IF (@AuthorFirst <> '' AND @AuthorLast = '') SET @Author = @AuthorFirst + '%'
IF (@AuthorFirst <> '' AND @AuthorLast <> '') 
BEGIN
	SET @Author = @AuthorLast + ', ' + @AuthorFirst + '%'
	SET @AuthorAlt = @AuthorFirst + ' ' + @AuthorLast + '%'
END
IF @AuthorAlt = '' SET @AuthorAlt = @Author

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

IF NOT EXISTS(SELECT TitleID FROM #tmpOpenUrlCitation) AND (@ItemID <> 0) 
BEGIN
	INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByItem @ItemID
END

-- ***************************************************************
-- Try searching by TitleID

IF NOT EXISTS(SELECT TitleID FROM #tmpOpenUrlCitation) AND (@TitleID <> 0) 
BEGIN
	INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByTitle @TitleID
END

-- ***************************************************************
-- Still nothing?  Try using title and author information
IF NOT EXISTS(SELECT TitleID FROM #tmpOpenUrlCitation)
BEGIN
	IF (@ArticleTitle <> '')
	BEGIN
		-- Article title specified, search to see if it has been categorized as a monograph
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByTitleDetail @ArticleTitle, @AuthorLast, @AuthorFirst
	END
	ELSE
	BEGIN
		-- Search for monograph/journal with the specified title/author
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectByTitleDetail @Title, @AuthorLast, @AuthorFirst
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
				p.ItemID
		FROM	#tmpOpenUrlCitation ou
				INNER JOIN Title t WITH (NOLOCK) ON ou.TitleID = t.TitleID
				INNER JOIN TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
				INNER JOIN Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
				INNER JOIN Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
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
					i.ItemID
			FROM	#tmpOpenUrlCitation ou
					INNER JOIN Title t WITH (NOLOCK) ON ou.TitleID = t.TitleID
					INNER JOIN TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
					INNER JOIN Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		(i.Volume = @Volume OR
					i.StartVolume = @Volume OR
					@Volume BETWEEN i.StartVolume and i.EndVolume)

			SELECT @ItemCount = COUNT(*) FROM #items
		END

		-- If we don't have any volumes, then try "LIKE" queries
		-- First try the Page table
		IF @ItemCount = 0
		BEGIN
			INSERT INTO #items 
			SELECT DISTINCT
					p.ItemID
			FROM	#tmpOpenUrlCitation ou
					INNER JOIN Title t WITH (NOLOCK) ON ou.TitleID = t.TitleID
					INNER JOIN TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
					INNER JOIN Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
					INNER JOIN Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		p.Active = 1
			--AND		p.Volume LIKE '%' + @Volume + '%'
			AND		(p.Volume LIKE @Volume + '%'
			OR		p.Volume LIKE '%[^0-9]' + @Volume + '%')

			SELECT @ItemCount = COUNT(*) FROM #items
		END

		/*
		IF @ItemCount = 0
		BEGIN
			-- If still no items found, again check the Volume field in the Item table
			INSERT INTO #items 
			SELECT DISTINCT 
					i.ItemID
			FROM	#tmpOpenUrlCitation ou
					INNER JOIN Title t WITH (NOLOCK) ON ou.TitleID = t.TitleID
					INNER JOIN TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
					INNER JOIN Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			--AND		i.Volume LIKE '%' + @Volume + '%'
			AND		(i.Volume LIKE @Volume + '%'
			OR		i.Volume LIKE '%[^0-9]' + @Volume + '%')

			SELECT @ItemCount = COUNT(*) FROM #items
		END
		*/
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
			SELECT	p.PageID, it.ItemID, NULLIF(COALESCE(p.Issue, i.StartIssue), ''), NULLIF(i.EndIssue, ''), 
					p.Year, p.Volume, ISNULL(ip.PagePrefix, ''), ISNULL(ip.PageNumber, '')
			FROM	#items it 
					INNER JOIN Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
					INNER JOIN Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
					INNER JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			WHERE	ip.PageNumber = @StartPage 
			AND		p.Active = 1
		END
		ELSE
		BEGIN
			---- If we had a volume to search for and didn't find any items, then don't look for pages
			--IF (@Volume = '')
			--BEGIN
				INSERT INTO #pages
				SELECT	p.PageID, i.ItemID, NULLIF(COALESCE(p.Issue, i.StartIssue), ''), NULLIF(i.EndIssue, ''), 
						p.Year, p.Volume, ISNULL(ip.PagePrefix, ''), ISNULL(ip.PageNumber, '')
				FROM	#tmpOpenUrlCitation ou
						INNER JOIN TitleItem ti WITH (NOLOCK) ON ou.TitleID = ti.TitleID
						INNER JOIN Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
						INNER JOIN Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
						INNER JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
				WHERE	ip.PageNumber = @StartPage 
				AND		p.Active = 1
				AND		i.ItemStatusID = 40
			--END
		END
	END

	-- If no pages were inserted based on start page, then look for a title page
	IF (SELECT COUNT(*) FROM #pages) = 0
	BEGIN
		IF @ItemCount > 0
		BEGIN
			INSERT INTO #pages
			SELECT	p.PageID, it.ItemID, NULLIF(COALESCE(p.Issue, i.StartIssue), ''), NULLIF(i.EndIssue, ''), 
					p.Year, p.Volume, ISNULL(ip.PagePrefix, ''), ISNULL(ip.PageNumber, '')
			FROM	#items it 
					INNER JOIN Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
					INNER JOIN Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
					LEFT JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
					INNER JOIN Page_PageType ppt WITH (NOLOCK) ON p.PageID = ppt.PageID
			WHERE	ppt.PageTypeID = 1 -- title page
			AND		p.Active = 1
		END
		ELSE
		BEGIN
			---- If we had a volume to search for and didn't find any items, then don't look for pages
			--IF (@Volume = '')
			--BEGIN
				INSERT INTO #pages
				SELECT	p.PageID, i.ItemID, NULLIF(COALESCE(p.Issue, i.StartIssue), ''), NULLIF(i.EndIssue, ''), 
						p.Year, p.Volume, ISNULL(ip.PagePrefix, ''), ISNULL(ip.PageNumber, '')
				FROM	#tmpOpenUrlCitation ou 
						INNER JOIN TitleItem ti WITH (NOLOCK) ON ou.TitleID = ti.TitleID
						INNER JOIN Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
						INNER JOIN Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
						INNER JOIN Page_PageType ppt WITH (NOLOCK) ON p.PageID = ppt.PageID
						LEFT JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
				WHERE	ppt.PageTypeID = 1 -- title page
				AND		p.Active = 1
				AND		i.ItemStatusID = 40
			--END
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
				i.ItemID,
				t.TitleID,
				ISNULL(t.FullTitle, ''),
				ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
				ISNULL(t.Datafield_260_b, '') AS PublisherName,
				ISNULL(p.Year, ISNULL(i.Year, CONVERT(nvarchar(20), ISNULL(t.StartYear, '')))) AS [Date],
				ISNULL(l.LanguageName, ''),
				CASE WHEN ISNULL(p.Volume COLLATE SQL_Latin1_General_CP1_CI_AI, '') = '' THEN ISNULL(i.Volume, '') ELSE p.Volume COLLATE SQL_Latin1_General_CP1_CI_AI END,
				ISNULL(t.EditionStatement, ''),
				ISNULL(t.CurrentPublicationFrequency, ''),
				CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
				c.Authors,
				c.Subjects,
				LTRIM(p.PagePrefix + ' ' + p.PageNumber) AS StartPage
		FROM	#pages p INNER JOIN dbo.Item i WITH (NOLOCK)
					ON p.ItemID = i.ItemID
				INNER JOIN dbo.Title t WITH (NOLOCK)
					ON i.PrimaryTitleID = t.TitleID
				LEFT JOIN dbo.Language l WITH (NOLOCK)
					ON i.LanguageCode = l.LanguageCode
				INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
					ON t.TitleID = c.TitleID
					AND i.ItemID = c.ItemID
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
					i.ItemID,
					t.TitleID,
					ISNULL(t.FullTitle, ''),
					ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
					ISNULL(t.Datafield_260_b, '') AS PublisherName,
					ISNULL(i.Year, CONVERT(nvarchar(20), ISNULL(t.StartYear, ''))) AS [Date],
					ISNULL(l.LanguageName, ''),
					ISNULL(i.Volume, ''),
					ISNULL(t.EditionStatement, ''),
					ISNULL(t.CurrentPublicationFrequency, ''),
					CASE WHEN SUBSTRING(t.MarcBibID, 8, 1) IN ('m', 'a') THEN 'Book' ELSE 'Journal' END AS Genre,
					c.Authors,
					c.Subjects,
					'' AS StartPage
			FROM	#items it INNER JOIN dbo.Item i WITH (NOLOCK)
						ON it.ItemID = i.ItemID
					INNER JOIN dbo.Title t WITH (NOLOCK)
						ON i.PrimaryTitleID = t.TitleID
					LEFT JOIN dbo.Language l WITH (NOLOCK)
						ON i.LanguageCode = l.LanguageCode
					INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
						ON t.TitleID = c.TitleID
						AND i.ItemID = c.ItemID
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
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectBySegmentDetail 
			@ArticleTitle, @Title, @AuthorLast, @AuthorFirst, @Volume, @Issue, @Year, @StartPage
	END
	ELSE
	BEGIN
		-- No article title specified, use the "@Title" value instead
		INSERT INTO #tmpOpenUrlCitation EXEC dbo.OpenUrlCitationSelectBySegmentDetail 
			@Title, '', @AuthorLast, @AuthorFirst, @Volume, @Issue, @Year, @StartPage
	END
END


-- ***************************************************************
-- Add the title identifiers to the final result set
UPDATE	#tmpOpenUrlCitation
SET		ISSN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 2

UPDATE	#tmpOpenUrlCitation
SET		ISBN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 3

UPDATE	#tmpOpenUrlCitation
SET		LCCN = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 5

UPDATE	#tmpOpenUrlCitation
SET		OCLC = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 1

UPDATE	#tmpOpenUrlCitation
SET		Abbreviation = ti.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN Title_Identifier ti WITH (NOLOCK)
			ON t.TitleID = ti.TitleID
			AND ti.IdentifierID = 6


-- ***************************************************************
-- Add the segment identifiers to the final result set
UPDATE	#tmpOpenUrlCitation
SET		ISSN = si.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN SegmentIdentifier si WITH (NOLOCK)
			ON t.SegmentID = si.SegmentID
			AND si.IdentifierID = 2


-- ***************************************************************
-- Return the final result set

SELECT DISTINCT t.*, title.SortTitle, ti.ItemSequence, s.SortTitle AS SegmentSortTitle
FROM #tmpOpenUrlCitation t LEFT JOIN dbo.TitleItem ti WITH (NOLOCK)
		ON t.TitleID = ti.TitleID
		AND t.ItemID = ti.ItemID
	LEFT JOIN dbo.Title WITH (NOLOCK)
		ON t.TitleID = title.TitleID
	LEFT JOIN dbo.Segment s WITH (NOLOCK)
		ON t.SegmentID = s.SegmentID
ORDER BY s.SortTitle, title.SortTitle, ti.ItemSequence, t.Date, t.StartPage

END
