
CREATE PROCEDURE [dbo].[NameResolvedSearchForPages]

@ResolvedNameString nvarchar(100),
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'ShortTitle',
@SortDirection nvarchar(4) = 'ASC'

AS 

SET NOCOUNT ON

DECLARE @TotalPages INT

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	TitleID INT NOT NULL,
	ItemID INT NOT NULL,
	PageID INT NOT NULL,
	BibliographicLevelName nvarchar(50) NOT NULL,
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
	CREATE INDEX IX_Step1 ON #Step1 (BibliographicLevelName, SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'BibliographicLevelName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (BibliographicLevelName desc, SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'ShortTitle' AND LOWER(@SortDirection) = 'asc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelName, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'ShortTitle' AND LOWER(@SortDirection) = 'desc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 (SortTitle desc, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelName, ShortTitle, PartNumber, PartName, Volume, [Date])
END
IF (@SortColumn = 'Date' AND LOWER(@SortDirection) = 'asc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 ([Date], SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelName, ShortTitle, PartNumber, PartName, Volume)
END
IF (@SortColumn = 'Date' AND LOWER(@SortDirection) = 'desc')
BEGIN
	CREATE INDEX IX_Step1 ON #Step1 ([Date] desc, SortTitle, ItemSequence, SequenceOrder)
	INCLUDE (TitleID, ItemID, PageID, BibliographicLevelName, ShortTitle, PartNumber, PartName, Volume)
END

-- Get the initial data set
INSERT #Step1
SELECT	t.TitleID,
		i.ItemID,
		p.PageID,
		ISNULL(bl.BibliographicLevelName, '') AS BibliographicLevelName,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		i.Volume,
		COALESCE(NULLIF(p.Year, ''), NULLIF(i.Year, ''), NULLIF(CONVERT(nvarchar(20), StartYear), '')) AS [Date],
		ti.ItemSequence,
		p.SequenceOrder
FROM	dbo.NameResolved nr WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.itemid = i.itemid
		INNER JOIN dbo.Title t WITH (NOLOCK) ON i.primarytitleid = t.titleid
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ti.ItemID = i.ItemID AND ti.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel bl WITH (NOLOCK) ON t.BibliographicLevelID = bl.BibliographicLevelID
WHERE	nr.ResolvedNameString = @ResolvedNameString
		AND n.IsActive = 1
		AND i.ItemStatusID = 40
		AND t.PublishReady = 1

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	TitleID INT NOT NULL,
	ItemID INT NOT NULL,
	PageID INT NOT NULL,
	BibliographicLevelName nvarchar(50) NOT NULL,
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
	SELECT	*, ROW_NUMBER() OVER (ORDER BY BibliographicLevelName, SortTitle, ItemSequence, SequenceOrder) AS RowNumber
	FROM	#Step1
END
IF (@SortColumn = 'BibliographicLevelName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY BibliographicLevelName DESC, SortTitle, ItemSequence, SequenceOrder) AS RowNumber
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
		p.ItemID,
		p.PageID,
		BibliographicLevelName,
		sc.FullTitle,
		ShortTitle,
		PartNumber,
		PartName,
		sc.Authors,
		p.Volume,
		[Date],
		--dbo.fnIndicatedPageStringForPage(PageID) AS IndicatedPages,
		LTRIM(RTRIM(ISNULL(ip.PagePrefix, '') +  ' ' + ISNULL(ip.PageNumber, ''))) AS IndicatedPages,
		@TotalPages AS TotalPages
FROM	#Step2 p LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID AND ip.Sequence = 1
		INNER JOIN dbo.SearchCatalog sc ON p.ItemID = sc.ItemID AND p.TitleID = sc.TitleID
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

