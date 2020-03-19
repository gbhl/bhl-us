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
	ItemID int NOT NULL,
	BarCode nvarchar(50) NOT NULL,
	InstitutionName nvarchar(255) NOT NULL,
	PaginationStatusDate datetime NOT NULL,
	PaginationUserName nvarchar(65) NOT NULL,
	PaginationStatusName nvarchar(50) NOT NULL
	)

-- Get the initial data set
INSERT	#Step1
SELECT	i.ItemID,
		i.BarCode,
		inst.InstitutionName,
		ISNULL(i.PaginationStatusDate, i.CreationDate) AS PaginationStatusDate,
		ISNULL(u.LastName + ', ' + u.FirstName, '') AS PaginationUserName,
		ps.PaginationStatusName
FROM	[dbo].[Item] i
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
	ItemID int NOT NULL,
	BarCode nvarchar(50) NOT NULL,
	InstitutionName nvarchar(255) NOT NULL,
	PaginationStatusDate datetime NOT NULL,
	PaginationUserName nvarchar(65) NOT NULL,
	PaginationStatusName nvarchar(50) NOT NULL,
	RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'ItemID' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ItemID)
	FROM	#Step1
END
IF (@SortColumn = 'ItemID' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ItemID DESC)
	FROM	#Step1
END

IF (@SortColumn = 'BarCode' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY BarCode)
	FROM	#Step1
END
IF (@SortColumn = 'BarCode' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY BarCode DESC)
	FROM	#Step1
END

IF (@SortColumn = 'InstitutionStrings' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY InstitutionName)
	FROM	#Step1
END
IF (@SortColumn = 'InstitutionStrings' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY InstitutionName DESC)
	FROM	#Step1
END

IF (@SortColumn = 'PaginationStatusDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY PaginationStatusDate)
	FROM	#Step1
END
IF (@SortColumn = 'PaginationStatusDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY PaginationStatusDate DESC)
	FROM	#Step1
END

IF (@SortColumn = 'PaginationUserName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY PaginationUserName)
	FROM	#Step1
END
IF (@SortColumn = 'PaginationUserName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY PaginationUserName DESC)
	FROM	#Step1
END

-- Count the total number of items
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		ItemID,
		BarCode,
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
