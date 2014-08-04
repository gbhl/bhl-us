CREATE PROCEDURE [dbo].[DOISelectByStatus]

@DOIStatusID int,
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'LastModifiedDate',
@SortDirection nvarchar(4) = 'DESC'

AS 

SET NOCOUNT ON

DECLARE @TotalDOIs INT

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	DOIID int NOT NULL,
	DOIEntityTypeName nvarchar(50) NULL,
	EntityID int NULL,
	EntityDetail nvarchar(2500) NULL,
	SortTitle nvarchar(60) NULL,
	DOIBatchID nvarchar(50) NULL,
	DOIName nvarchar(50) NULL,
	StatusMessage nvarchar(1000) NULL,
	CreationDate datetime NOT NULL,
	LastModifiedDate datetime NOT NULL
	)

-- Get the initial data set
INSERT #Step1
SELECT	d.DOIID,
		et.DOIEntityTypeName,
		d.EntityID,
		'',
		'',
		d.DOIBatchID,
		d.DOIName,
		d.StatusMessage,
		d.CreationDate,
		d.LastModifiedDate
FROM	dbo.DOI d INNER JOIN dbo.DOIEntityType et
			ON d.DOIEntityTypeID = et.DOIEntityTypeID
WHERE	DOIStatusID = @DOIStatusID

UPDATE	#Step1
SET		EntityDetail = t.FullTitle,
		SortTitle = t.SortTitle
FROM	#Step1 s INNER JOIN dbo.Title t
			ON s.EntityID = t.TitleID
			AND s.DOIEntityTypeName = 'Title'
			
UPDATE	#Step1
SET		EntityDetail = t.FullTitle + ' ' + i.Volume,
		SortTitle = t.SortTitle
FROM	#Step1 s INNER JOIN dbo.Item i
			ON s.EntityID = i.ItemID
			AND s.DOIEntityTypeName = 'Item'
		INNER JOIN dbo.Title t
			ON i.PrimaryTitleID = t.TitleID

UPDATE	#Step1
SET		EntityDetail = t.FullTitle + ' ' + i.Volume + ' ' + ip.PagePrefix + ' ' + ip.PageNumber,
		SortTitle = t.SortTitle
FROM	#Step1 s INNER JOIN dbo.Page p
			ON s.EntityID = p.PageID
			AND s.DOIEntityTypeName = 'Page'
		INNER JOIN dbo.Item i
			ON p.ItemID = i.ItemID
		INNER JOIN dbo.Title t
			ON i.PrimaryTitleID = t.TitleID
		LEFT JOIN dbo.IndicatedPage ip
			ON p.PageID = ip.PageID
			AND	ip.Sequence = 1

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	DOIID int NOT NULL,
	DOIEntityTypeName nvarchar(50) NULL,
	EntityID int NULL,
	EntityDetail nvarchar(2500) NULL,
	SortTitle nvarchar(60) NULL,
	DOIBatchID nvarchar(50) NULL,
	DOIName nvarchar(50) NULL,
	StatusMessage nvarchar(1000) NULL,
	CreationDate datetime NOT NULL,
	LastModifiedDate datetime NOT NULL,
	RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'Entity' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIEntityTypeName, EntityID)
	FROM	#Step1
END
IF (@SortColumn = 'Entity' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIEntityTypeName DESC, EntityID DESC)
	FROM	#Step1
END
IF (@SortColumn = 'EntityDetail' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY SortTitle, EntityDetail)
	FROM	#Step1
END
IF (@SortColumn = 'EntityDetail' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY SortTitle DESC, EntityDetail DESC)
	FROM	#Step1
END
IF (@SortColumn = 'DOIBatchID' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIBatchID)
	FROM	#Step1
END
IF (@SortColumn = 'DOIBatchID' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIBatchID DESC)
	FROM	#Step1
END
IF (@SortColumn = 'DOI' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIName)
	FROM	#Step1
END
IF (@SortColumn = 'DOI' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIName DESC)
	FROM	#Step1
END
IF (@SortColumn = 'StatusMessage' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY StatusMessage)
	FROM	#Step1
END
IF (@SortColumn = 'StatusMessage' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY StatusMessage DESC)
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

IF (@SortColumn = 'LastModifiedDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY LastModifiedDate)
	FROM	#Step1
END
IF (@SortColumn = 'LastModifiedDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY LastModifiedDate DESC)
	FROM	#Step1
END

-- Count the total number of pages
SELECT @TotalDOIs = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		DOIID,
		DOIEntityTypeName,
		EntityID,
		EntityDetail,
		DOIBatchID,
		DOIName,
		StatusMessage,
		CreationDate,
		LastModifiedDate,
		@TotalDOIs AS TotalDOIs
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DOISelectByStatus. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
