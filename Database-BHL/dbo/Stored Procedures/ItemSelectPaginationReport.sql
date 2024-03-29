SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectPaginationReport]

@PublishedOnly int = 0,
@Institutioncode nvarchar(10) = '',
@StatusIDList AS dbo.IDListInt READONLY,
@StartDate datetime = '1/1/1980',
@EndDate datetime = '12/31/2099',
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'ItemID',
@SortDirection nvarchar(4) = 'ASC'

AS 

SET NOCOUNT ON

DECLARE @TotalItems INT

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	PrimaryTitleID int NOT NULL,
	FullTitle nvarchar(2000) NOT NULL,
	SortTitle nvarchar(2000) NOT NULL,
	BibliographicLevel nvarchar(50) NOT NULL,
	ItemTypeID int NOT NULL,
	ID int NOT NULL,
	ItemID int NOT NULL,
	BarCode nvarchar(200) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
	ItemStatusName nvarchar(50) NOT NULL,
	ScanningDate datetime NULL,
	InstitutionName nvarchar(255) NOT NULL,
	PaginationStatusDate datetime NOT NULL,
	PaginationUserName nvarchar(65) NOT NULL,
	PaginationStatusName nvarchar(50) NOT NULL
	)

-- Get the initial data set
INSERT	#Step1
-- Books
SELECT	t.TitleID AS PrimaryTitleID,
		ISNULL(t.FullTitle, '') AS FullTitle,
		ISNULL(t.SortTitle, '') AS SortTitle,
		ISNULL(bl.BibliographicLevelLabel, '') AS BibliographicLevel,
		i.ItemTypeID,
		b.BookID,
		b.ItemID,
		b.BarCode,
		ISNULL(b.Volume, '') AS Volume,
		ISNULL(b.[StartYear], '') AS [Year],
		s.ItemStatusName,
		b.ScanningDate,
		inst.InstitutionName,
		ISNULL(b.PaginationStatusDate, i.CreationDate) AS PaginationStatusDate,
		ISNULL(u.LastName + ', ' + u.FirstName, '') AS PaginationUserName,
		ps.PaginationStatusName
FROM	dbo.Item i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN @StatusIDList list ON b.PaginationStatusID = list.ID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel bl on t.BibliographicLevelID = bl.BibliographicLevelID
		INNER JOIN dbo.PaginationStatus ps ON ps.PaginationStatusID = b.PaginationStatusID
		LEFT JOIN dbo.AspNetUsers u ON u.Id = b.PaginationStatusUserID
		LEFT JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		LEFT JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID 
		LEFT JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemStatus s ON i.ItemStatusID = s.ItemStatusID
WHERE	(@PublishedOnly <> 1 OR i.ItemStatusID = 40)
AND		(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		(r.InstitutionRoleName = 'Holding Institution' OR r.InstitutionRoleName IS NULL)
AND		ISNULL(b.PaginationStatusDate, i.CreationDate) BETWEEN @StartDate AND @EndDate
UNION
-- Segments
SELECT	t.TitleID AS PrimaryTitleID,
		ISNULL(vs.Title, '') COLLATE SQL_Latin1_General_CP1_CI_AI AS FullTitle,
		ISNULL(vs.SortTitle, '') COLLATE SQL_Latin1_General_CP1_CI_AI AS SortTitle,
		ISNULL(g.GenreName, '') AS BibliographicLevel,
		i.ItemTypeID,
		vs.SegmentID,
		i.ItemID,
		vs.BarCode,
		ISNULL(vs.Volume, '') COLLATE SQL_Latin1_General_CP1_CI_AI AS Volume,
		ISNULL(vs.Date, '') AS [Year],
		st.ItemStatusName,
		s.ScanningDate,
		inst.InstitutionName,
		ISNULL(s.PaginationStatusDate, i.CreationDate) AS PaginationStatusDate,
		ISNULL(u.LastName + ', ' + u.FirstName, '') AS PaginationUserName,
		ps.PaginationStatusName
FROM	dbo.Item i
		INNER JOIN dbo.vwSegment vs ON i.ItemID = vs.ItemID
		INNER JOIN dbo.Segment s ON vs.SegmentID = s.SegmentID
		INNER JOIN dbo.ItemRelationship ir ON i.ItemID = ir.ChildID
		INNER JOIN @StatusIDList list ON s.PaginationStatusID = list.ID
		INNER JOIN dbo.ItemTitle it ON ir.ParentID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.SegmentGenre g ON vs.SegmentGenreID = g.SegmentGenreID
		INNER JOIN dbo.PaginationStatus ps ON ps.PaginationStatusID = s.PaginationStatusID
		LEFT JOIN dbo.AspNetUsers u ON u.Id = s.PaginationStatusUserID
		LEFT JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		LEFT JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID 
		LEFT JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemStatus st ON i.ItemStatusID = st.ItemStatusID
WHERE	(@PublishedOnly <> 1 OR i.ItemStatusID IN (30, 40))
AND		(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		(r.InstitutionRoleName = 'Contributor' OR r.InstitutionRoleName IS NULL)
AND		ISNULL(s.PaginationStatusDate, i.CreationDate) BETWEEN @StartDate AND @EndDate

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	PrimaryTitleID int NOT NULL,
	FullTitle nvarchar(2000) NOT NULL,
	SortTitle nvarchar(2000) NOT NULL,
	BibliographicLevel nvarchar(50) NOT NULL,
	ItemTypeID int NOT NULL,
	ID int NOT NULL,
	ItemID int NOT NULL,
	BarCode nvarchar(200) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
	ItemStatusName nvarchar(50) NOT NULL,
	ScanningDate datetime NULL,
	InstitutionName nvarchar(255) NOT NULL,
	PaginationStatusDate datetime NOT NULL,
	PaginationUserName nvarchar(65) NOT NULL,
	PaginationStatusName nvarchar(50) NOT NULL,
	RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
DECLARE @SQL nvarchar(MAX) = 'SELECT *, ROW_NUMBER() OVER(ORDER BY ' + @SortColumn + ' ' + @SortDirection + ') FROM #Step1'
INSERT #Step2 exec sp_executesql @SQL

-- Count the total number of items
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		PrimaryTitleID,
		FullTitle,
		BibliographicLevel,
		ItemTypeID,
		ID AS ItemID,
		BarCode,
		Volume,
		[Year] AS StartYear,
		ItemStatusName,
		ScanningDate,
		InstitutionName AS ContributorTextString,
		PaginationStatusName,
		PaginationStatusDate,
		PaginationUserName,
		(SELECT COUNT(PageID) FROM dbo.ItemPage WHERE ItemID = #Step2.ItemID) AS NumberOfPages,
		@TotalItems AS TotalItems
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

GO
