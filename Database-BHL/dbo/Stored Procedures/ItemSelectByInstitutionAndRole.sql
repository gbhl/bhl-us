CREATE PROCEDURE [dbo].[ItemSelectByInstitutionAndRole]

@InstitutionCode nvarchar(10),
@InstitutionRoleID int,
@Barcode nvarchar(200) = '',
@TitleID int = NULL,
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'CreationDate',
@SortDirection nvarchar(4) = 'desc'

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TotalItems INT
DECLARE @HoldingInstitutionID INT
DECLARE @RightsHolderID INT
DECLARE @ScanningInstitutionID INT

SELECT @RightsHolderID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Rights Holder'
SELECT @ScanningInstitutionID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Scanning Institution'
SELECT @HoldingInstitutionID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Holding Institution'

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	BookID int NOT NULL,
	ItemID int NOT NULL,
	BarCode nvarchar(200) NOT NULL,
	PrimaryTitleID int NOT NULL,
	TitleName nvarchar(2000) NOT NULL,
	SortTitle nvarchar(60) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	StartVolume nvarchar(10) NOT NULL,
	StartIssue nvarchar(10) NOT NULL,
	StartNumber nvarchar(10) NOT NULL,
	StartSeries nvarchar(10) NOT NULL,
	Year nvarchar(20) NOT NULL,
	CreatorTextString nvarchar(max) NOT NULL,
	CopyrightIndicator nvarchar(100) NOT NULL,
	CopyrightStatus nvarchar(max) NOT NULL,
	Rights nvarchar(max) NOT NULL,
	LicenseUrl nvarchar(max) NOT NULL,
	DueDiligence nvarchar(max) NOT NULL,
	CreationDate datetime NOT NULL,
	LastModifiedDate datetime NOT NULL
	)
	
-- Get the initial data set
IF (@InstitutionCode = '')
BEGIN
	-- Look for items where NO institution has been assigned to the specified role
	INSERT #Step1
	SELECT	b.BookID,
			i.ItemID,
			b.BarCode,
			t.TitleID AS PrimaryTitleID,
			t.FullTitle AS TitleName, 
			t.SortTitle,
			ISNULL(b.Volume, ''),
			b.StartVolume,
			b.StartIssue,
			b.StartNumber,
			b.StartSeries,
			CASE WHEN ISNULL(b.StartYear, '') = '' THEN ISNULL(CONVERT(nvarchar(20), t.StartYear), '') ELSE b.StartYear END,
			c.Authors AS CreatorTextString,
			b.CopyrightIndicator,
			ISNULL(b.CopyrightStatus, ''),
			ISNULL(b.Rights, ''),
			ISNULL(b.LicenseUrl, ''),
			ISNULL(b.DueDiligence, ''),
			b.CreationDate,
			b.LastModifiedDate
	FROM	dbo.Item i
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
			INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
			INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
			LEFT JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID AND ii.InstitutionRoleID = @InstitutionRoleID
	WHERE	ii.InstitutionRoleID IS NULL
	AND		@InstitutionRoleID IN (SELECT InstitutionRoleID FROM dbo.InstitutionRole)
	AND		(b.Barcode LIKE '%' + @Barcode + '%' OR @Barcode = '')
	AND		(t.TitleID = @TitleID OR @TitleID IS NULL)
END
ELSE
BEGIN
	-- Look for items where the specified institution has been assigned to the specified role
	INSERT #Step1
	SELECT	b.BookID,
			i.ItemID,
			b.BarCode,
			t.TitleID AS PrimaryTitleID,
			t.FullTitle AS TitleName, 
			t.SortTitle,
			ISNULL(b.Volume, ''),
			b.StartVolume,
			b.StartIssue,
			b.StartNumber,
			b.StartSeries,
			CASE WHEN ISNULL(b.StartYear, '') = '' THEN ISNULL(CONVERT(nvarchar(20), t.StartYear), '') ELSE b.StartYear END,
			c.Authors AS CreatorTextString,
			b.CopyrightIndicator,
			ISNULL(b.CopyrightStatus, ''),
			ISNULL(b.Rights, ''),
			ISNULL(b.LicenseUrl, ''),
			ISNULL(b.DueDiligence, ''),
			b.CreationDate,
			b.LastModifiedDate
	FROM	dbo.Item i 
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
			INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
			INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
			INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID AND ii.InstitutionRoleID = @InstitutionRoleID
	WHERE	(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '_A_L_L_')
	AND		(b.Barcode LIKE '%' + @Barcode + '%' OR @Barcode = '')
	AND		(t.TitleID = @TitleID OR @TitleID IS NULL)
END

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	BookID int NOT NULL,
	ItemID int NOT NULL,
	BarCode nvarchar(200) NOT NULL,
	PrimaryTitleID int NOT NULL,
	TitleName nvarchar(2000) NOT NULL,
	SortTitle nvarchar(60) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	StartVolume nvarchar(10) NOT NULL,
	StartIssue nvarchar(10) NOT NULL,
	StartNumber nvarchar(10) NOT NULL,
	StartSeries nvarchar(10) NOT NULL,
	Year nvarchar(20) NOT NULL,
	CreatorTextString nvarchar(max) NOT NULL,
	CopyrightIndicator nvarchar(100) NOT NULL,
	CopyrightStatus nvarchar(max) NOT NULL,
	Rights nvarchar(max) NOT NULL,
	LicenseUrl nvarchar(max) NOT NULL,
	DueDiligence nvarchar(max) NOT NULL,
	CreationDate datetime NOT NULL,
	LastModifiedDate datetime NOT NULL,
	RowNumber int NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'TitleName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*,
			ROW_NUMBER() OVER (
				ORDER BY 
					SortTitle, 
					Year,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartVolume) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartVolume)) 
						ELSE StartVolume END, 10),
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartIssue) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartIssue)) 
						ELSE StartIssue END, 10),
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartNumber) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartNumber)) 
						ELSE StartNumber END, 10),
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartSeries) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartSeries)) 
						ELSE StartSeries END, 10)
				)
	FROM	#Step1
END
IF (@SortColumn = 'TitleName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, 
			ROW_NUMBER() OVER (
				ORDER BY 
					SortTitle DESC, 
					Year,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartVolume) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartVolume)) 
						ELSE StartVolume END, 10) DESC,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartIssue) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartIssue)) 
						ELSE StartIssue END, 10) DESC,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartNumber) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartNumber)) 
						ELSE StartNumber END, 10) DESC,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartSeries) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartSeries)) 
						ELSE StartSeries END, 10) DESC
				)
	FROM	#Step1
END
IF (@SortColumn = 'CreationDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY CreationDate)
	FROM	#Step1
END
IF (@SortColumn = 'CreationDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY CreationDate DESC)
	FROM	#Step1
END
IF (LOWER(@SortColumn) = 'barcode' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER(ORDER BY Barcode)
	FROM	#Step1
END
IF (LOWER(@SortColumn) = 'barcode' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER(ORDER BY Barcode DESC)
	FROM	#Step1
END
IF (LOWER(@SortColumn) = 'year' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER(ORDER BY [Year])
	FROM	#Step1
END
IF (LOWER(@SortColumn) = 'year' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER(ORDER BY [Year] DESC)
	FROM	#Step1
END


-- Count the total number of pages
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		BookID,
		ItemID,
		BarCode,
		PrimaryTitleID,
		TitleName,
		Volume,
		Year AS StartYear,
		CreatorTextString,
		CopyrightIndicator,
		CopyrightStatus,
		Rights,
		LicenseUrl,
		DueDiligence,
		dbo.fnInstitutionStringForItem(ItemID, @HoldingInstitutionID) AS ContributorTextString,
		dbo.fnInstitutionStringForItem(ItemID, @RightsHolderID) AS RightsHolderTextString,
		dbo.fnInstitutionStringForItem(ItemID, @ScanningInstitutionID) AS ScanningInstitutionTextString,
		CreationDate,
		LastModifiedDate,
		@TotalItems AS TotalItems
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByInstitutionAndRole. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END

GO
