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
	BibliographicLevelName nvarchar(50) NULL,
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
		BibliographicLevelName,
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
SELECT	t.TitleID,
		i.ItemID,
		p.PageID,
		t.BibliographicLevelID,
		'' AS BibliographicLevelName,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(COALESCE(NULLIF(p.Year, ''), NULLIF(i.Year, ''), NULLIF(CONVERT(nvarchar(20), StartYear), '')), '') AS [Date],
		'' AS Authors,
		ISNULL(i.Volume, '') AS Volume,
		'' AS IndicatedPages,
		CASE WHEN ISNULL(i.CallNumber, '') = '' THEN t.CallNumber ELSE i.CallNumber END AS CallNumber,
		t.LanguageCode,
		'' AS LanguageName,
		ti.ItemSequence,
		p.SequenceOrder
FROM	dbo.NameResolved nr WITH (NOLOCK)
		INNER JOIN dbo.Name nm WITH (NOLOCK) ON nr.NameResolvedID = nm.NameResolvedID
		INNER JOIN dbo.NamePage np WITH (NOLOCK) ON nm.NameID = np.NameID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.itemid = i.itemid
		INNER JOIN dbo.Title t WITH (NOLOCK) ON i.primarytitleid = t.titleid
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ti.ItemID = i.ItemID AND ti.TitleID = t.TitleID
WHERE	nr.CanonicalNameString = @CanonicalNameString
		AND nm.IsActive = 1
		AND i.ItemStatusID = 40
		AND t.PublishReady = 1

-- Getting these values after the primary select cuts execution time by at least half
UPDATE	#tmp
SET		BibliographicLevelName = b.BibliographicLevelName
FROM	#tmp t
		INNER JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID

UPDATE	#tmp
SET		LanguageName = l.LanguageName
FROM	#tmp t
		INNER JOIN dbo.Language l WITH (NOLOCK) ON t.LanguageCode = l.LanguageCode

UPDATE	#tmp
SET		IndicatedPages = LTRIM(RTRIM(ISNULL(ip.PagePrefix, '') +  ' ' + ISNULL(ip.PageNumber, '')))
FROM	#tmp t
		INNER JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON t.PageID = ip.PageID AND ip.Sequence = 1

UPDATE	#tmp
SET		Authors = ISNULL(n.FullName + ' ', '') +  
				ISNULL(a.Numeration + ' ', '') + ISNULL(a.Unit + ' ', '') +
				ISNULL(a.Title + ' ', '') + ISNULL(a.Location + ' ', '') + 
				ISNULL(n.FullerForm + ' ', '') + ISNULL(a.StartDate  + ' ', '') + 
				CASE WHEN ISNULL(a.StartDate, '') <> '' THEN '-' ELSE '' END + 
				ISNULL(a.EndDate, '')
FROM	#tmp t
		INNER JOIN (	SELECT	ta.TitleID, MIN(ta.AuthorID) AS AuthorID
						FROM	dbo.TitleAuthor ta WITH (NOLOCK) INNER JOIN dbo.AuthorRole r WITH (NOLOCK)
									ON ta.AuthorRoleID = r.AuthorRoleID 
									AND r.MarcDataFieldTag IN ('100', '700', '110', '710')
						GROUP BY ta.TitleID ) ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.Author a WITH (NOLOCK) ON ta.AuthorID = a.AuthorID AND a.IsActive = 1
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1

-- Return final result set
SELECT	TitleID,
		ItemID,
		PageID,
		BibliographicLevelName,
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
