CREATE PROCEDURE [dbo].[NameResolvedSearchForPages]

@ResolvedNameString nvarchar(100),
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'ShortTitle',
@SortDirection nvarchar(4) = 'ASC'

AS 

SET NOCOUNT ON

DECLARE @TotalPages INT

-- Use the Canonical form of the Resolved name to search for pages
DECLARE @CanonicalNameString nvarchar(100)
SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE ResolvedNameString = @ResolvedNameString

-- If no name found, see if the argument matches a Canonical form
IF (@CanonicalNameString IS NULL) SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE CanonicalNameString = @ResolvedNameString

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	TitleID INT NOT NULL,
	BookID INT NOT NULL,
	ItemID INT NOT NULL,
	PageID INT NOT NULL,
	BibliographicLevelLabel nvarchar(50) NOT NULL,
	ShortTitle nvarchar(255) NULL,
	SortTitle nvarchar(60) NULL,
	PartNumber nvarchar(255) NULL,
	PartName nvarchar(255) NULL,
	Volume nvarchar(100) NULL,
	[Date] nvarchar(20) NULL,
	ItemSequence INT NOT NULL,
	SequenceOrder INT NULL
	)

-- Build the appropriate index on the temp table
IF (@SortColumn = 'BibliographicLevelName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (BibliographicLevelLabel, SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'BibliographicLevelName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (BibliographicLevelLabel desc, SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'ShortTitle' AND LOWER(@SortDirection) = 'asc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelLabel, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'ShortTitle' AND LOWER(@SortDirection) = 'desc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (SortTitle desc, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelLabel, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'Date' AND LOWER(@SortDirection) = 'asc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 ([Date], SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelLabel, ShortTitle, PartNumber, PartName, Volume)
END
IF (@SortColumn = 'Date' AND LOWER(@SortDirection) = 'desc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 ([Date] desc, SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelLabel, ShortTitle, PartNumber, PartName, Volume)
END

-- Get the initial data set
INSERT #Step1
-- "Regular" items
SELECT DISTINCT
		t.TitleID,
		b.BookID,
		b.ItemID,
		p.PageID,
		ISNULL(bl.BibliographicLevelLabel, '') AS BibliographicLevelLabel,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		b.Volume,
		COALESCE(NULLIF(p.Year, ''), NULLIF(b.StartYear, ''), NULLIF(CONVERT(nvarchar(20), t.StartYear), '')) AS [Date],
		it.ItemSequence,
		ip.SequenceOrder
FROM	dbo.NameResolved nr
		INNER JOIN dbo.Name n ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN dbo.NamePage np ON n.NameID = np.NameID
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.itemid = i.itemid
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON it.ItemID = i.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.titleid = t.titleid
		LEFT JOIN dbo.BibliographicLevel bl ON t.BibliographicLevelID = bl.BibliographicLevelID
WHERE	nr.CanonicalNameString = @CanonicalNameString
AND		n.IsActive = 1
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
UNION
-- Virtual items
SELECT DISTINCT
		t.TitleID,
		b.BookID,
		b.ItemID,
		p.PageID,
		ISNULL(bl.BibliographicLevelLabel, '') AS BibliographicLevelLabel,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		b.Volume,
		COALESCE(NULLIF(p.Year, ''), NULLIF(b.StartYear, ''), NULLIF(CONVERT(nvarchar(20), t.StartYear), '')) AS [Date],
		it.ItemSequence,
		ip.SequenceOrder
FROM	dbo.NameResolved nr
		INNER JOIN dbo.Name n ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN dbo.NamePage np ON n.NameID = np.NameID
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.itemid = i.itemid
		INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
		INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
		INNER JOIN dbo.Item bi ON b.ItemID = bi.ItemID AND bi.ItemStatusID = 40
		INNER JOIN dbo.ItemTitle it ON it.ItemID = b.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.titleid = t.titleid
		LEFT JOIN dbo.BibliographicLevel bl ON t.BibliographicLevelID = bl.BibliographicLevelID
WHERE	nr.CanonicalNameString = @CanonicalNameString
AND		n.IsActive = 1
AND		i.ItemStatusID IN (30, 40)
AND		t.PublishReady = 1

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	TitleID INT NOT NULL,
	BookID INT NOT NULL,
	ItemID INT NOT NULL,
	PageID INT NOT NULL,
	BibliographicLevelLabel nvarchar(50) NOT NULL,
	ShortTitle nvarchar(255) NULL,
	SortTitle nvarchar(60) NULL,
	PartNumber nvarchar(255) NULL,
	PartName nvarchar(255) NULL,
	Volume nvarchar(100) NULL,
	[Date] nvarchar(20) NULL,
	ItemSequence INT NOT NULL,
	SequenceOrder INT NULL,
	RowNumber INT NOT NULL
	)

CREATE INDEX IX_Step2 ON #Step2 (RowNumber)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'BibliographicLevelName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY BibliographicLevelLabel, SortTitle, ItemSequence, SequenceOrder) AS RowNumber
	FROM	#Step1
END
IF (@SortColumn = 'BibliographicLevelName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY BibliographicLevelLabel DESC, SortTitle, ItemSequence, SequenceOrder) AS RowNumber
	FROM	#Step1
END
IF (@SortColumn = 'ShortTitle' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY SortTitle, ItemSequence, SequenceOrder) AS RowNumber
	FROM	#Step1
END
IF (@SortColumn = 'ShortTitle' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY SortTitle DESC, ItemSequence, SequenceOrder)
	FROM	#Step1
END
IF (@SortColumn = 'Date' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY [Date], SortTitle, ItemSequence, SequenceOrder)
	FROM	#Step1
END
IF (@SortColumn = 'Date' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY [Date] DESC, SortTitle, ItemSequence, SequenceOrder)
	FROM	#Step1
END

-- Count the total number of pages
SELECT @TotalPages = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows)
		p.TitleID,
		p.BookID AS ItemID,
		p.PageID,
		BibliographicLevelLabel,
		t.FullTitle,
		t.ShortTitle,
		t.PartNumber,
		t.PartName,
		sc.Authors,
		p.Volume,
		[Date],
		LTRIM(RTRIM(ISNULL(ip.PagePrefix, '') +  ' ' + ISNULL(ip.PageNumber, ''))) AS IndicatedPages,
		@TotalPages AS TotalPages
FROM	#Step2 p LEFT JOIN dbo.IndicatedPage ip ON p.PageID = ip.PageID AND ip.Sequence = 1
		INNER JOIN dbo.SearchCatalog sc ON p.BookID = sc.ItemID AND p.TitleID = sc.TitleID
		INNER JOIN dbo.Title t ON sc.TitleID = t.TitleID
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

GO
