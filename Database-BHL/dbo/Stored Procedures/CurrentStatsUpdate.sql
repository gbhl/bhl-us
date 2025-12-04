CREATE PROCEDURE dbo.CurrentStatsUpdate

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TitleActive int,
		@TitleTotal int,
		@BookActive int,
		@BookTotal int,
		@PageActive int,
		@PageTotal int,
		@SegmentActive int,
		@SegmentTotal int,
		@BookSegmentActive int,
		@BookSegmentTotal int,
		@NameActive int,
		@NameTotal int,
		@UniqueNameActive int,
		@UniqueNameTotal int,
		@VerifiedNameActive int,
		@VerifiedNameTotal int

-- Get the items with page content (ignore segments that "share" page records with scanned books)
SELECT	i.ItemID
INTO	#ItemsWithPages
FROM	dbo.Item i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID AND b.IsVirtual = 0
WHERE	i.ItemStatusID = 40
UNION
SELECT	s.ItemID
FROM	dbo.vwSegment s
		INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
WHERE	s.SegmentStatusID IN (30, 40)

-- Get basic Title/Item/Page/Segment stats
SELECT @TitleActive = COUNT(*) FROM dbo.Title WHERE Title.PublishReady = 1
SELECT @TitleTotal = COUNT(*) FROM dbo.Title

SELECT	@BookActive = COUNT(*) 
FROM	dbo.Book b 
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID 
WHERE	i.ItemStatusID = 40

SELECT @BookTotal = COUNT(*) FROM dbo.Book

SELECT	@PageActive = COUNT(*)
FROM	#ItemsWithPages iwp
		INNER JOIN dbo.ItemPage ip ON iwp.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
WHERE	p.Active = 1

SELECT @PageTotal = COUNT(*) FROM dbo.Page

SELECT	@SegmentActive = COUNT(*) 
FROM	dbo.Segment s 
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID 
WHERE	i.ItemStatusID IN (30, 40)

SELECT @SegmentTotal = COUNT(*) FROM dbo.Segment

SELECT	@BookSegmentActive = COUNT(DISTINCT ib.ItemID)
FROM	dbo.Segment s
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemRelationship r ON i.ItemID = r.ChildID
		INNER JOIN dbo.Item ib ON r.ParentID = ib.ItemID 
WHERE	ib.ItemStatusID = 40
AND		i.ItemStatusID IN (30, 40)

SELECT @BookSegmentTotal = COUNT(DISTINCT i.ItemID) 
FROM	dbo.Segment s 
		INNER JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID
		INNER JOIN dbo.Item i ON r.ParentID = i.ItemID 

-- Get Name string stats
SELECT @NameActive = COUNT(DISTINCT np.NamePageID) 
FROM	dbo.NamePage np
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
WHERE	p.Active = 1
	
SELECT @NameTotal = COUNT(*) FROM dbo.NamePage

-- Get unique Name string stats
SELECT	@UniqueNameActive = COUNT(DISTINCT np.NameID) 
FROM	dbo.NamePage np
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
WHERE	p.Active = 1
		
SELECT @UniqueNameTotal = COUNT(*) FROM dbo.Name

-- Get verified Name string stats
SELECT	@VerifiedNameActive = COUNT (DISTINCT n.NameID)
FROM	dbo.Name n
		INNER JOIN dbo.NameResolved r ON n.NameResolvedID = r.NameResolvedID 
		INNER JOIN dbo.NamePage np ON n.NameID = np.NameID
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
WHERE	p.Active = 1
	
SELECT	@VerifiedNameTotal = COUNT (DISTINCT n.NameID)
FROM	dbo.Name n
		INNER JOIN dbo.NameResolved r ON n.NameResolvedID = r.NameResolvedID

BEGIN TRY
	BEGIN TRAN

	UPDATE	dbo.CurrentStats
	SET		TitleActive = @TitleActive,
			TitleTotal = @TitleTotal,
			BookActive = @BookActive,
			BookTotal = @BookTotal,
			PageActive = @PageActive,
			PageTotal = @PageTotal,
			SegmentActive = @SegmentActive,
			SegmentTotal = @SegmentTotal,
			BookSegmentActive = @BookSegmentActive,
			BookSegmentTotal = @BookSegmentTotal,
			NameActive = @NameActive,
			NameTotal = @NameTotal,
			UniqueNameActive = @UniqueNameActive,
			UniqueNameTotal = @UniqueNameTotal,
			VerifiedNameActive = @VerifiedNameActive,
			VerifiedNameTotal = @VerifiedNameTotal,
			LastModifiedDate = GETDATE()

	IF NOT EXISTS (SELECT TitleActive FROM dbo.CurrentStats)
	BEGIN
		INSERT	dbo.CurrentStats (TitleActive, TitleTotal, BookActive, BookTotal, PageActive, PageTotal, SegmentActive, 
					SegmentTotal, BookSegmentActive, BookSegmentTotal, NameActive, NameTotal, UniqueNameActive, UniqueNameTotal,
					VerifiedNameActive, VerifiedNameTotal)
		VALUES	(@TitleActive, @TitleTotal, @BookActive, @BookTotal, @PageActive, @PageTotal, @SegmentActive, @SegmentTotal,
				@BookSegmentActive, @BookSegmentTotal, @NameActive, @NameTotal, @UniqueNameActive, @UniqueNameTotal,
				@VerifiedNameActive, @VerifiedNameTotal)
	END

	COMMIT TRAN
END TRY
BEGIN CATCH
	ROLLBACK TRAN
END CATCH

END

GO
