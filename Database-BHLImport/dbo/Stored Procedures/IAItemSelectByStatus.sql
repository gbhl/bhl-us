CREATE PROCEDURE [dbo].[IAItemSelectByStatus]

@ItemStatusID int,
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'IAIdentifier',
@SortDirection nvarchar(4) = 'ASC'

AS 

SET NOCOUNT ON

DECLARE @TotalItems INT

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	ItemID int NOT NULL,
	IAIdentifier nvarchar(50) NOT NULL,
	Sponsor nvarchar(100) NOT NULL,
	ScanningCenter nvarchar(50) NOT NULL,
	Volume nvarchar(50) NOT NULL,
	ScanDate nvarchar(50) NOT NULL,
	ExternalStatus nvarchar(50) NOT NULL
	)

-- Get the initial data set
INSERT #Step1
SELECT	ItemID,
		IAIdentifier,
		Sponsor,
		ScanningCenter,
		Volume,
		ScanDate,
		ExternalStatus
FROM	dbo.IAItem
WHERE	ItemStatusID = @ItemStatusID

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	ItemID int NOT NULL,
	IAIdentifier nvarchar(50) NOT NULL,
	Sponsor nvarchar(100) NOT NULL,
	ScanningCenter nvarchar(50) NOT NULL,
	Volume nvarchar(50) NOT NULL,
	ScanDate nvarchar(50) NOT NULL,
	ExternalStatus nvarchar(50) NOT NULL,
	RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'IAIdentifier' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY IAIdentifier)
	FROM	#Step1
END
IF (@SortColumn = 'IAIdentifier' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY IAIdentifier DESC)
	FROM	#Step1
END
IF (@SortColumn = 'Sponsor' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY Sponsor)
	FROM	#Step1
END
IF (@SortColumn = 'Sponsor' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY Sponsor DESC)
	FROM	#Step1
END
IF (@SortColumn = 'ScanningCenter' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ScanningCenter)
	FROM	#Step1
END
IF (@SortColumn = 'ScanningCenter' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ScanningCenter DESC)
	FROM	#Step1
END
IF (@SortColumn = 'ScanDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ScanDate)
	FROM	#Step1
END
IF (@SortColumn = 'ScanDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ScanDate DESC)
	FROM	#Step1
END

IF (@SortColumn = 'ExternalStatus' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ExternalStatus)
	FROM	#Step1
END
IF (@SortColumn = 'ExternalStatus' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ExternalStatus DESC)
	FROM	#Step1
END

-- Count the total number of pages
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		ItemID,
		IAIdentifier,
		Sponsor,
		ScanningCenter,
		Volume,
		ScanDate,
		ExternalStatus,
		@TotalItems AS TotalItems
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectByStatus. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


