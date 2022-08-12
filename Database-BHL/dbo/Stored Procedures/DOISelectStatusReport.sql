CREATE PROCEDURE dbo.DOISelectStatusReport

@CreationUserID int,
@DOIStatusID int,
@DOIEntityTypeID int,
@EntityID int = NULL,
@StartDate datetime,
@EndDate datetime,
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'LastModifiedDate',
@SortDirection nvarchar(4) = 'DESC'

AS 

SET NOCOUNT ON

DECLARE @TotalDOIs INT
DECLARE @IdentifierID int
SELECT @IdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI';

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	DOIID int NOT NULL,
	DOIStatusID int NOT NULL,
	DOIStatusName nvarchar(30) NOT NULL,
	DOIEntityTypeName nvarchar(50) NULL,
	[Action] nvarchar(10) NOT NULL,
	EntityID int NULL,
	EntityDetail nvarchar(2500) NULL,
	ContainerTitleID int NULL,
	SortTitle nvarchar(60) NULL,
	DOIBatchID nvarchar(50) NULL,
	DOIName nvarchar(50) NULL,
	StatusMessage nvarchar(1000) NULL,
	CreationDate datetime NOT NULL,
	LastModifiedDate datetime NOT NULL,
	CreationUserID int NOT NULL,
	CreationUserName nvarchar(max) NOT NULL,
	LastModifiedUserID int NOT NULL
	)

-- Get the initial data set
INSERT #Step1
SELECT	d.DOIID,
		d.DOIStatusID,
		st.DOIStatusName,
		et.DOIEntityTypeName,
		CASE 
		-- No Title or Item Identifier records, so definitely "New"
		WHEN COALESCE(ti.TitleIdentifierID, ii.ItemIdentifierID) IS NULL THEN 'NEW'
		ELSE 
			-- If the DOI Creation Date is after the Title/Item Identifier creation, this is an "Update", otherwise it is a "New" addition
			CASE WHEN d.CreationDate > ISNULL(COALESCE(ti.CreationDate, ii.CreationDate), '1/1/1980') THEN 'UPDATE' ELSE 'NEW' END 
		END AS [Action],
		d.EntityID,
		'',
		NULL,
		'',
		d.DOIBatchID,
		d.DOIName,
		d.StatusMessage,
		d.CreationDate,
		d.LastModifiedDate,
		d.CreationUserID,
		LTRIM(RTRIM(ISNULL(u.FirstName, '') + ' ' + ISNULL(u.LastName, ''))) AS CreationUserName,
		d.LastModifiedUserID
FROM	dbo.DOI d 
		INNER JOIN dbo.DOIEntityType et	ON d.DOIEntityTypeID = et.DOIEntityTypeID
		INNER JOIN dbo.DOIStatus st ON d.DOIStatusID = st.DOIStatusID
		LEFT JOIN dbo.AspNetUsers u ON d.CreationUserID = u.id
		LEFT JOIN dbo.Title_Identifier ti ON d.EntityID = ti.TitleID AND d.DOIEntityTypeID = 10 AND ti.IdentifierID = @IdentifierID
		LEFT JOIN dbo.Segment s ON d.EntityID = s.segmentid AND d.DOIEntityTypeID = 40
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierID
WHERE	(d.DOIStatusID = @DOIStatusID OR @DOIStatusID = 0)
AND		(d.CreationUserID = @CreationUserID OR @CreationUserID = 0)
AND		(d.DOIEntityTypeID = @DOIEntityTypeID OR @DOIEntityTypeID = 0)
AND		(d.EntityID = @EntityID OR @EntityID IS NULL)
AND		d.CreationDate BETWEEN @StartDate AND @EndDate

UPDATE	s1
SET		ContainerTitleID = it.TitleID
FROM	#Step1 s1
		INNER JOIN dbo.Segment s ON s1.EntityID = s.SegmentID
		INNER JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID
		INNER JOIN dbo.ItemTitle it ON r.ParentID = it.ItemID AND it.IsPrimary = 1
WHERE	s1.DOIEntityTypeName = 'Segment'

UPDATE	#Step1
SET		EntityDetail = t.FullTitle,
		SortTitle = t.SortTitle
FROM	#Step1 s INNER JOIN dbo.Title t
			ON s.EntityID = t.TitleID
			AND s.DOIEntityTypeName = 'Title'
			
UPDATE	#Step1
SET		EntityDetail = t.FullTitle + ' ' + b.Volume,
		SortTitle = t.SortTitle
FROM	#Step1 s INNER JOIN dbo.Book b
			ON s.EntityID = b.BookID
			AND s.DOIEntityTypeName = 'Item'
		INNER JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID
		INNER JOIN dbo.Title t ON pt.TitleID = t.TitleID

UPDATE	#Step1
SET		EntityDetail = t.FullTitle + ' ' + b.Volume + ' ' + ipg.PagePrefix + ' ' + ipg.PageNumber,
		SortTitle = t.SortTitle
FROM	#Step1 s INNER JOIN dbo.Page p
			ON s.EntityID = p.PageID
			AND s.DOIEntityTypeName = 'Page'
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON ip.ItemID = pt.ItemID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
		INNER JOIN dbo.Title t ON pt.TitleID = t.TitleID
		LEFT JOIN dbo.IndicatedPage ipg
			ON p.PageID = ipg.PageID
			AND	ipg.Sequence = 1

UPDATE	#Step1
SET		EntityDetail = seg.Title,
		SortTitle = LEFT(seg.SortTitle, 60)
FROM	#Step1 s INNER JOIN dbo.Segment seg
			ON s.EntityID = seg.SegmentID
			AND s.DOIEntityTypeName = 'Segment'

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	DOIID int NOT NULL,
	DOIStatusID int NOT NULL,
	DOIStatusName nvarchar(30) NOT NULL,
	DOIEntityTypeName nvarchar(50) NULL,
	[Action] nvarchar(10) NOT NULL,
	EntityID int NULL,
	EntityDetail nvarchar(2500) NULL,
	ContainerTitleID int NULL,
	SortTitle nvarchar(60) NULL,
	DOIBatchID nvarchar(50) NULL,
	DOIName nvarchar(50) NULL,
	StatusMessage nvarchar(1000) NULL,
	CreationDate datetime NOT NULL,
	LastModifiedDate datetime NOT NULL,
	CreationUserID int NOT NULL,
	CreationUserName nvarchar(max) NOT NULL,
	LastModifiedUserID int NOT NULL,
	RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'StatusName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIStatusName)
	FROM	#Step1
END
IF (@SortColumn = 'StatusName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY DOIStatusName DESC)
	FROM	#Step1
END
IF (@SortColumn = 'Action' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY Action)
	FROM	#Step1
END
IF (@SortColumn = 'Action' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY Action DESC)
	FROM	#Step1
END
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
IF (@SortColumn = 'ContainerTitleID' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ContainerTitleID)
	FROM	#Step1
END
IF (@SortColumn = 'ContainerTitleID' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY ContainerTitleID DESC)
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

IF (@SortColumn = 'CreationUserName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY CreationUserName)
	FROM	#Step1
END
IF (@SortColumn = 'CreationUserName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY CreationUserName DESC)
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
		DOIStatusName,
		DOIEntityTypeName,
		[Action],
		EntityID,
		EntityDetail,
		ContainerTitleID,
		DOIBatchID,
		DOIName,
		StatusMessage,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		CreationUserName,
		LastModifiedUserID,
		@TotalDOIs AS TotalDOIs
FROM	#Step2 t
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

GO
