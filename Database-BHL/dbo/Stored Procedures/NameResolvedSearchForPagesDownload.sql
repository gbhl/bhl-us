CREATE PROCEDURE [dbo].[NameResolvedSearchForPagesDownload]

@ResolvedNameString nvarchar(100)

AS 

SET NOCOUNT ON

-- Use the Canonical form of the Resolved name to search for pages
DECLARE @CanonicalNameString nvarchar(100)
SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE ResolvedNameString = @ResolvedNameString

-- If no name found, see if the argument matches a Canonical form
IF (@CanonicalNameString IS NULL) SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE CanonicalNameString = @ResolvedNameString

CREATE TABLE #tmp
	(
	TitleID INT NOT NULL,
	ItemID INT NOT NULL,
	PageID INT NOT NULL,
	BibliographicLevelID int NULL,
	BibliographicLevelLabel nvarchar(50) NULL,
	FullTitle nvarchar(2000) NULL,
	ShortTitle nvarchar(255) NULL,
	SortTitle nvarchar(60) NULL,
	PartNumber nvarchar(255) NULL,
	PartName nvarchar(255) NULL,
	PublisherPlace nvarchar(150) NULL,
	Publisher nvarchar(255) NULL,
	[Date] nvarchar(20) NULL,
	Authors nvarchar(MAX) NULL,
	Volume nvarchar(100) NULL,
	IndicatedPages nvarchar(70) NULL,
	CallNumber nvarchar(100) NULL,
	LanguageCode nvarchar(10) NULL,
	LanguageName nvarchar(20) NULL,
	ItemSequence INT NULL,
	SequenceOrder INT NULL
	)

CREATE INDEX IX_tmp ON #tmp (SortTitle ASC, ItemSequence ASC, SequenceOrder ASC)

-- Initial data set
INSERT #tmp
		(
		TitleID,
		ItemID,
		PageID,
		BibliographicLevelID,
		BibliographicLevelLabel,
		FullTitle,
		ShortTitle,
		SortTitle,
		PartNumber,
		PartName,
		PublisherPlace,
		Publisher,
		[Date],
		Authors,
		Volume,
		IndicatedPages,
		CallNumber,
		LanguageCode,
		LanguageName,
		ItemSequence,
		SequenceOrder
		)
SELECT DISTINCT
		t.TitleID,
		b.BookID AS ItemID,
		p.PageID,
		t.BibliographicLevelID,
		'' AS BibliographicLevelLabel,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(COALESCE(NULLIF(p.Year, ''), NULLIF(b.StartYear, ''), NULLIF(CONVERT(nvarchar(20), t.StartYear), '')), '') AS [Date],
		'' AS Authors,
		ISNULL(b.Volume, '') AS Volume,
		'' AS IndicatedPages,
		CASE WHEN ISNULL(b.CallNumber, '') = '' THEN t.CallNumber ELSE b.CallNumber END AS CallNumber,
		t.LanguageCode,
		'' AS LanguageName,
		it.ItemSequence,
		ip.SequenceOrder
FROM	dbo.NameResolved nr
		INNER JOIN dbo.Name nm ON nr.NameResolvedID = nm.NameResolvedID
		INNER JOIN dbo.NamePage np ON nm.NameID = np.NameID
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.itemid = i.itemid
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON it.ItemID = i.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.titleid = t.titleid
WHERE	nr.CanonicalNameString = @CanonicalNameString
		AND nm.IsActive = 1
		AND i.ItemStatusID = 40
		AND t.PublishReady = 1
UNION
SELECT DISTINCT
		t.TitleID,
		b.BookID AS ItemID,
		p.PageID,
		t.BibliographicLevelID,
		'' AS BibliographicLevelLabel,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(COALESCE(NULLIF(p.Year, ''), NULLIF(b.StartYear, ''), NULLIF(CONVERT(nvarchar(20), t.StartYear), '')), '') AS [Date],
		'' AS Authors,
		ISNULL(b.Volume, '') AS Volume,
		'' AS IndicatedPages,
		CASE WHEN ISNULL(b.CallNumber, '') = '' THEN t.CallNumber ELSE b.CallNumber END AS CallNumber,
		t.LanguageCode,
		'' AS LanguageName,
		it.ItemSequence,
		ip.SequenceOrder
FROM	dbo.NameResolved nr
		INNER JOIN dbo.Name nm ON nr.NameResolvedID = nm.NameResolvedID
		INNER JOIN dbo.NamePage np ON nm.NameID = np.NameID
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.itemid = i.itemid AND i.ItemStatusID IN (30, 40)
		INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
		INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
		INNER JOIN dbo.Item bi ON b.ItemID = bi.ItemID AND bi.ItemStatusID = 40
		INNER JOIN dbo.ItemTitle it ON it.ItemID = b.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.titleid = t.titleid
WHERE	nr.CanonicalNameString = @CanonicalNameString
		AND nm.IsActive = 1
		AND i.ItemStatusID = 40
		AND t.PublishReady = 1

-- Getting these values after the primary select cuts execution time by at least half
UPDATE	#tmp
SET		BibliographicLevelLabel = b.BibliographicLevelLabel
FROM	#tmp t
		INNER JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID

UPDATE	#tmp
SET		LanguageName = l.LanguageName
FROM	#tmp t
		INNER JOIN dbo.Language l ON t.LanguageCode = l.LanguageCode

UPDATE	#tmp
SET		IndicatedPages = LTRIM(RTRIM(ISNULL(ip.PagePrefix, '') +  ' ' + ISNULL(ip.PageNumber, '')))
FROM	#tmp t
		INNER JOIN dbo.IndicatedPage ip ON t.PageID = ip.PageID AND ip.Sequence = 1

UPDATE	#tmp
SET		Authors = ISNULL(n.FullName + ' ', '') +  
				ISNULL(a.Numeration + ' ', '') + ISNULL(a.Unit + ' ', '') +
				ISNULL(a.Title + ' ', '') + ISNULL(a.Location + ' ', '') + 
				ISNULL(n.FullerForm + ' ', '') + ISNULL(a.StartDate  + ' ', '') + 
				CASE WHEN ISNULL(a.StartDate, '') <> '' THEN '-' ELSE '' END + 
				ISNULL(a.EndDate, '')
FROM	#tmp t
		INNER JOIN (	SELECT	ta.TitleID, MIN(ta.AuthorID) AS AuthorID
						FROM	dbo.TitleAuthor ta INNER JOIN dbo.AuthorRole r
									ON ta.AuthorRoleID = r.AuthorRoleID 
									AND r.MarcDataFieldTag IN ('100', '700', '110', '710')
						GROUP BY ta.TitleID ) ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.Author a ON ta.AuthorID = a.AuthorID AND a.IsActive = 1
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1

-- Return final result set
SELECT	TitleID,
		ItemID,
		PageID,
		BibliographicLevelLabel,
		FullTitle,
		ShortTitle,
		PartNumber,
		PartName,
		PublisherPlace,
		Publisher,
		[Date],
		Authors,
		Volume,
		IndicatedPages,
		CallNumber,
		LanguageName
FROM	#tmp 
ORDER BY SortTitle, ItemSequence, SequenceOrder

GO
