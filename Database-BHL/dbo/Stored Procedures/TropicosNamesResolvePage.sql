SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TropicosNamesResolvePage]

@NameID int,
@TitleID int

AS

BEGIN

SET NOCOUNT ON

-- ***************************************************************
-- Set up parameters

DECLARE @ItemCount int
SET @ItemCount = 0

DECLARE @Volume nvarchar(100)
DECLARE @Issue nvarchar(20)
DECLARE @StartPage nvarchar(20)

SELECT	@Volume = ISNULL(Volume, ''),
		@Issue = ISNULL(Issue, ''),
		@StartPage = ISNULL(Page, '')
FROM	dbo.TropicosNames
WHERE	NameID = @NameID
AND		BHLTitleID = @TitleID

-- ***************************************************************
-- Build the temp table

CREATE TABLE #tmpOpenUrlCitation
	(
	NameID int NULL,
	PageID int NULL,
	ItemID int NULL,
	TitleID int NULL,
	FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	StartPage nvarchar(40) NOT NULL DEFAULT('')
	)

-- ***************************************************************
-- Try searching by TitleID
IF (@TitleID <> 0) 
BEGIN
	INSERT INTO #tmpOpenUrlCitation	(
		NameID, PageID, ItemID, TitleID, FullTitle, Volume, StartPage
		)
	SELECT	@NameID,
			0,
			0,
			t.TitleID,
			ISNULL(t.FullTitle, ''),
			'' AS Volume,
			'' AS StartPage
	FROM	dbo.Title t
	WHERE	t.PublishReady = 1
	AND		t.TitleID = @TitleID
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
		Year nvarchar(20),
		Volume nvarchar(20),
		PagePrefix nvarchar(40),
		PageNumber nvarchar(20)
	)

	-- See if we can narrow it down to an item
	IF @Volume <> ''
	BEGIN
		-- Check the Volume field in the Page table
		-- First try an exact match
		INSERT INTO #items 
		SELECT DISTINCT
				p.ItemID
		FROM	dbo.Page p 
				INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
				INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
				INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
				INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
				INNER JOIN #tmpOpenUrlCitation ou ON t.TitleID = ou.TitleID
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
			FROM	dbo.Item i 
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
					INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
					INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
					INNER JOIN #tmpOpenUrlCitation ou ON t.TitleID = ou.TitleID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		b.Volume = @Volume

			SELECT @ItemCount = COUNT(*) FROM #items
		END

		-- If we don't have any volumes, then try "LIKE" queries
		-- First try the Page table
		IF @ItemCount = 0
		BEGIN
			INSERT INTO #items 
			SELECT DISTINCT
					p.ItemID
			FROM	dbo.Page p 
					INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
					INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
					INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
					INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
					INNER JOIN #tmpOpenUrlCitation ou ON t.TitleID = ou.TitleID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		p.Active = 1
			AND		(p.Volume LIKE @Volume + '%'
			OR		p.Volume LIKE '%[^0-9]' + @Volume + '%')

			SELECT @ItemCount = COUNT(*) FROM #items
		END

		IF @ItemCount = 0
		BEGIN
			-- If still no items found, again check the Volume field in the Item table
			INSERT INTO #items 
			SELECT DISTINCT 
					i.ItemID
			FROM	dbo.Item i 
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
					INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
					INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
					INNER JOIN #tmpOpenUrlCitation ou ON t.TitleID = ou.TitleID
			WHERE	t.PublishReady = 1 
			AND		i.ItemStatusID = 40
			AND		(b.Volume LIKE @Volume + '%'
			OR		b.Volume LIKE '%[^0-9]' + @Volume + '%')

			SELECT @ItemCount = COUNT(*) FROM #items
		END
	END

	-- Do the initial population of the #pages table
	IF @StartPage <> ''
	BEGIN
		IF @ItemCount > 0
		BEGIN
			INSERT INTO #pages
			SELECT	p.PageID, it.ItemID, p.Issue, p.Year, p.Volume, ISNULL(ipg.PagePrefix, ''), ISNULL(ipg.PageNumber, '')
			FROM	#items it 
					INNER JOIN dbo.ItemPage ip ON it.ItemID = ip.ItemID
					INNER JOIN dbo.Page p ON it.PageID = p.PageID
					INNER JOIN dbo.IndicatedPageg ip ON p.PageID = ipg.PageID
			WHERE	ipg.PageNumber = @StartPage 
			AND		p.Active = 1
		END
		ELSE
		BEGIN
			---- If we had a volume to search for and didn't find any items, then don't look for pages
			INSERT INTO #pages
			SELECT	p.PageID, i.ItemID, p.Issue, p.Year, p.Volume, ISNULL(ipg.PagePrefix, ''), ISNULL(ipg.PageNumber, '')
			FROM	dbo.Item i 
					INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
					INNER JOIN dbo.Page p ON ip.ItemID = p.ItemID
					INNER JOIN dbo.IndicatedPage ipg ON p.PageID = ipg.PageID
					INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
					INNER JOIN #tmpOpenUrlCitation ou ON it.TitleID = ou.TitleID
			WHERE	ipg.PageNumber = @StartPage 
			AND		p.Active = 1
			AND		i.ItemStatusID = 40
		END
	END

	-- If an issue was specified, drop any rows from our #pages table that don't match
	IF (@Issue <> '') DELETE FROM #pages WHERE Issue <> @Issue

	-- Populate the final result set
	IF (SELECT COUNT(*) FROM #pages) > 0
	BEGIN
		-- Clear out the title citations
		TRUNCATE TABLE #tmpOpenUrlCitation

		-- Add page citations
		INSERT INTO #tmpOpenUrlCitation	(
			NameID, PageID, ItemID, TitleID, FullTitle, Volume, StartPage
			)
		SELECT	@NameID,
				p.PageID,
				i.ItemID,
				t.TitleID,
				ISNULL(t.FullTitle, ''),
				CASE WHEN ISNULL(p.Volume, '') = '' THEN ISNULL(b.Volume, '') ELSE p.Volume END,
				LTRIM(p.PagePrefix + ' ' + p.PageNumber) AS StartPage
		FROM	#pages p 
				INNER JOIN dbo.Item i ON p.ItemID = i.ItemID
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
				INNER JOIN dbo.Title t ON pt.TitleID = t.TitleID
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
				NameID, PageID, ItemID, TitleID, FullTitle, Volume, StartPage
				)
			SELECT	@NameID,
					0,
					i.ItemID,
					t.TitleID,
					ISNULL(t.FullTitle, ''),
					ISNULL(b.Volume, ''),
					'' AS StartPage
			FROM	#items it 
					INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
					INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
					INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
					INNER JOIN dbo.Title t ON pt.TitleID = t.TitleID
			WHERE	t.PublishReady = 1
		END
	END
END

-- ***************************************************************
-- Return the final result set
IF (SELECT COUNT(*) FROM #tmpOpenUrlCitation) = 1
BEGIN
	UPDATE	dbo.TropicosNames
	SET		BHLItemID = b.BookID,
			BHLPageID = t.PageID,
			BHLTitle = t.FullTitle,
			BHLVolume = t.Volume,
			BHLStartPage = t.StartPage
	FROM	#tmpOpenUrlCitation t 
			INNER JOIN dbo.TropicosNames n ON t.TitleID = n.BHLTitleID AND t.NameID = n.NameID
			INNER JOIN dbo.Book b ON t.ItemID = b.ItemID
END

END


GO
