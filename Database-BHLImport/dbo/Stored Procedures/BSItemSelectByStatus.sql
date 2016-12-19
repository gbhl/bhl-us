CREATE PROCEDURE [dbo].[BSItemSelectByStatus]

@ItemStatusID int,
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'CreationDate',
@SortDirection nvarchar(4) = 'DESC'

AS 

SET NOCOUNT ON

DECLARE @TotalItems INT

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	ItemID int NOT NULL,
	BHLItemID int NOT NULL,
	Title nvarchar(255) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	CreationDate nvarchar(20) NOT NULL
	)

-- Get the initial data set
INSERT #Step1
SELECT	b.ItemID,
		b.BHLItemID,
		t.ShortTitle,
		ISNULL(i.Volume, ''),
		CONVERT(VARCHAR(30), b.CreationDate, 120)
FROM	dbo.BSItem b 
		INNER JOIN dbo.BHLItem i ON b.BHLItemID = i.ItemID
		INNER JOIN dbo.BHLTitle t ON i.PrimaryTitleID = t.TitleID
WHERE	b.ItemStatusID = @ItemStatusID

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	ItemID int NOT NULL,
	BHLItemID int NOT NULL,
	Title nvarchar(255) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	CreationDate nvarchar(20) NOT NULL,
	RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'Title' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY Title)
	FROM	#Step1
END
IF (@SortColumn = 'Title' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY Title DESC)
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

-- Create a temp table for the third step
CREATE TABLE #Step3
	(
	ItemID int NOT NULL,
	BHLItemID int NOT NULL,
	Title nvarchar(255) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	CreationDate nvarchar(20) NOT NULL,
	RowNumber INT NOT NULL,
	TotalItems INT NOT NULL
	)

-- Count the total number of items
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Get just the rows that we'll return, and include the total number of items in the result set
INSERT	#Step3
SELECT TOP (@NumRows) 
		ItemID,
		BHLItemID,
		Title,
		Volume,
		CreationDate,
		RowNumber,
		@TotalItems AS TotalItems
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

-- Return the final result set, including the number of segments for each item
SELECT	ItemID,
		BHLItemID,
		Title,
		Volume,
		(SELECT COUNT(*) FROM BSSegment WHERE ItemID = t.ItemID) AS TotalSegments,
		CONVERT(DATETIME, CreationDate) AS CreationDate,
		TotalItems
FROM	#Step3 t
ORDER BY
		RowNumber

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemSelectByStatus. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
