CREATE PROCEDURE [dbo].[ItemSelectPaginationReport]

@PaginationStatusID int = 20,
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
	SortTitle nvarchar(60) NOT NULL,
	BibliographicLevel nvarchar(50) NOT NULL,
	ItemID int NOT NULL,
	BarCode nvarchar(50) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
	ScanningDate datetime NULL,
	InstitutionName nvarchar(255) NOT NULL,
	PaginationStatusDate datetime NOT NULL,
	PaginationUserName nvarchar(65) NOT NULL,
	PaginationStatusName nvarchar(50) NOT NULL
	)

-- Get the initial data set
INSERT	#Step1
SELECT	i.PrimaryTitleID,
		ISNULL(t.FullTitle, '') AS FullTitle,
		ISNULL(t.SortTitle, '') AS SortTitle,
		ISNULL(b.BibliographicLevelLabel, '') AS BibliographicLevel,
		i.ItemID,
		i.BarCode,
		ISNULL(i.Volume, '') AS Volume,
		ISNULL(i.[Year], '') AS [Year],
		i.ScanningDate,
		inst.InstitutionName,
		ISNULL(i.PaginationStatusDate, i.CreationDate) AS PaginationStatusDate,
		ISNULL(u.LastName + ', ' + u.FirstName, '') AS PaginationUserName,
		ps.PaginationStatusName
FROM	dbo.Item i
		INNER JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b on t.BibliographicLevelID = b.BibliographicLevelID
		INNER JOIN dbo.PaginationStatus ps ON ps.PaginationStatusID = i.PaginationStatusID
		LEFT JOIN dbo.AspNetUsers u ON u.Id = i.PaginationStatusUserID
		LEFT JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		LEFT JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID 
		LEFT JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
WHERE	(i.PaginationStatusID = @PaginationStatusID OR @PaginationStatusID = 0)
AND		(r.InstitutionRoleName = 'Holding Institution' OR r.InstitutionRoleName IS NULL)
AND		ISNULL(i.PaginationStatusDate, i.CreationDate) BETWEEN @StartDate AND @EndDate

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	PrimaryTitleID int NOT NULL,
	FullTitle nvarchar(2000) NOT NULL,
	SortTitle nvarchar(60) NOT NULL,
	BibliographicLevel nvarchar(50) NOT NULL,
	ItemID int NOT NULL,
	BarCode nvarchar(50) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
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
		ItemID,
		BarCode,
		Volume,
		[Year],
		ScanningDate,
		InstitutionName AS ContributorTextString,
		PaginationStatusName,
		PaginationStatusDate,
		PaginationUserName,
		(SELECT COUNT(PageID) FROM dbo.Page WHERE ItemID = #Step2.ItemID) AS NumberOfPages,
		@TotalItems AS TotalItems
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber
