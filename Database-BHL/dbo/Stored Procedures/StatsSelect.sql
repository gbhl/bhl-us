SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[StatsSelect]

@Expanded bit = 0,
@Names bit = 0,
@UniqueNames bit = 0,
@VerifiedNames bit = 0,
@EOLNames bit = 0,
@EOLPages bit = 0

AS 
BEGIN
SET NOCOUNT ON

DECLARE @TitleCount int,
	@VolumeCount int,
	@PageCount int,
	@TitleTotal int,
	@VolumeTotal int,
	@PageTotal int,
	@SegmentCount int,
	@SegmentTotal int,
	@ItemSegmentCount int,
	@ItemSegmentTotal int,
	@NameCount int,
	@NameTotal int,
	@UniqueNameCount int,
	@UniqueNameTotal int,
	@VerifiedNameCount int,
	@VerifiedNameTotal int,
	@EOLNameCount int,
	@EOLNameTotal int,
	@EOLPageCount int,
	@EOLPageTotal int

-- Get the items with page content (ignore segments that "share" page records with scanned books)
SELECT	i.ItemID
INTO	#ItemsWithPages
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID AND b.IsVirtual = 0
WHERE	i.ItemStatusID = 40
UNION
SELECT	s.ItemID
FROM	dbo.vwSegment s WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) ON s.BookID = b.BookID AND b.IsVirtual = 1
WHERE	s.SegmentStatusID IN (30, 40)

-- Get basic Title/Item/Page/Segment stats
IF (@Names = 0 AND @UniqueNames = 0 AND @VerifiedNames = 0 AND @EOLNames = 0 AND @EOLPages = 0)
BEGIN
	SELECT @TitleCount = COUNT(*) FROM dbo.Title WITH (NOLOCK) WHERE Title.PublishReady = 1

	SELECT	@VolumeCount = COUNT(*) 
	FROM	dbo.Book b WITH (NOLOCK) 
			INNER JOIN dbo.Item i WITH (NOLOCK) ON b.ItemID = i.ItemID 
	WHERE	i.ItemStatusID = 40

	SELECT	@PageCount = COUNT(*)
	FROM	#ItemsWithPages iwp
			INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON iwp.ItemID = ip.ItemID
			INNER JOIN dbo.Page p WITH (NOLOCK) ON ip.PageID = p.PageID
	WHERE	p.Active = 1

	SELECT	@SegmentCount = COUNT(*) 
	FROM	dbo.Segment s WITH (NOLOCK) 
			INNER JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID 
	WHERE	i.ItemStatusID IN (30, 40)

	SELECT	@ItemSegmentCount = COUNT(DISTINCT i.ItemID) 
	FROM	dbo.vwSegment s WITH (NOLOCK) 
			INNER JOIN dbo.ItemRelationship r WITH (NOLOCK) ON s.ItemID = r.ChildID
			INNER JOIN dbo.Item i ON r.ParentID = i.ItemID 
	WHERE	i.ItemStatusID = 40
	AND		s.SegmentStatusID IN (30, 40)

	IF (@Expanded = 1)
	BEGIN
		SELECT @TitleTotal = count(*) FROM dbo.Title WITH (NOLOCK)
		SELECT @VolumeTotal = count(*) FROM dbo.Book WITH (NOLOCK)
		SELECT @PageTotal = count(*) FROM dbo.Page WITH (NOLOCK)
		SELECT @SegmentTotal = count(*) FROM dbo.Segment WITH (NOLOCK)

		SELECT @ItemSegmentTotal = COUNT(DISTINCT i.ItemID) 
		FROM	dbo.Segment s WITH (NOLOCK) 
				INNER JOIN dbo.ItemRelationship r WITH (NOLOCK) ON s.ItemID = r.ChildID
				INNER JOIN dbo.Item i ON r.ParentID = i.ItemID 
	END
END

-- Get Name string stats
IF (@Names = 1)
BEGIN
	SELECT @NameCount = COUNT(DISTINCT np.NamePageID) 
	FROM	dbo.NamePage np WITH (NOLOCK)
			INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
			INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
	WHERE	p.Active = 1
	
	IF (@Expanded = 1) SELECT @NameTotal = COUNT(*) FROM dbo.NamePage WITH (NOLOCK)
END

-- Get unique Name string stats
IF (@UniqueNames = 1)
BEGIN
	SELECT	@UniqueNameCount = COUNT(DISTINCT np.NameID) 
	FROM	dbo.NamePage np WITH (NOLOCK)
			INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
			INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
	WHERE	p.Active = 1
		
	IF (@Expanded = 1) SELECT @UniqueNameTotal = COUNT(*) FROM dbo.Name WITH (NOLOCK)
END

-- Get verified Name string stats
IF (@VerifiedNames = 1)
BEGIN
	SELECT	@VerifiedNameCount = COUNT (DISTINCT n.NameID)
	FROM	dbo.Name n WITH (NOLOCK)
			INNER JOIN dbo.NameResolved r WITH (NOLOCK) ON n.NameResolvedID = r.NameResolvedID 
			INNER JOIN dbo.NamePage np WITH (NOLOCK)ON n.NameID = np.NameID
			INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
			INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
	WHERE	p.Active = 1
	
	IF (@Expanded = 1) 
	BEGIN
		SELECT	@VerifiedNameTotal = COUNT (DISTINCT n.NameID)
		FROM	dbo.Name n WITH (NOLOCK)
				INNER JOIN dbo.NameResolved r WITH (NOLOCK) ON n.NameResolvedID = r.NameResolvedID 
	END
END

DECLARE @EOL int
SELECT @EOL = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

-- Get Name strings associated with EOL pages stats
IF (@EOLNames = 1)
BEGIN
	SELECT  @EOLNameCount = COUNT(*)
	FROM	dbo.NameResolved nr WITH (NOLOCK) 
			INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @EOL
			INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
			INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
			INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
			INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
	WHERE	n.IsActive = 1
	AND		p.Active = 1

	IF (@Expanded = 1)
	BEGIN
		SELECT  @EOLNameTotal = COUNT(*)
		FROM	dbo.NameResolved nr WITH (NOLOCK)
				INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @EOL
				INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
				INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
	END	
END

-- Get Pages with name strings associated with EOL pages stats
IF (@EOLPages = 1)
BEGIN
	SELECT  @EOLPageCount = COUNT(DISTINCT np.PageID)
	FROM	dbo.NameResolved nr 
			WITH (NOLOCK) INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @EOL
			INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
			INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
			INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
			INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN #ItemsWithPages iwp ON ip.ItemID = iwp.ItemID
	WHERE	n.IsActive = 1
	AND		p.Active = 1

	IF (@Expanded = 1)
	BEGIN
		SELECT  @EOLPageTotal = COUNT(DISTINCT np.PageID)
		FROM	dbo.NameResolved nr WITH (NOLOCK) 
				INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @EOL
				INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
				INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
	END	
END

SELECT ISNULL(@TitleCount, 0) AS TitleCount,
	ISNULL(@VolumeCount, 0) AS VolumeCount,
	ISNULL(@PageCount, 0) AS [PageCount],
	ISNULL(@TitleTotal, 0) AS TitleTotal,
	ISNULL(@VolumeTotal, 0) AS VolumeTotal,
	ISNULL(@PageTotal, 0) AS PageTotal,
	ISNULL(@SegmentCount, 0) AS SegmentCount,
	ISNULL(@SegmentTotal, 0) AS SegmentTotal,
	ISNULL(@ItemSegmentCount, 0) AS ItemSegmentCount,
	ISNULL(@ItemSegmentTotal, 0) AS ItemSegmentTotal,
	ISNULL(@NameCount, 0) AS NameCount,
	ISNULL(@NameTotal, 0) AS NameTotal,
	ISNULL(@UniqueNameCount, 0) AS UniqueNameCount,
	ISNULL(@UniqueNameTotal, 0) AS UniqueNameTotal,
	ISNULL(@VerifiedNameCount, 0) AS VerifiedNameCount,
	ISNULL(@VerifiedNameTotal, 0) AS VerifiedNameTotal,
	ISNULL(@EOLNameCount, 0) AS EOLNameCount,
	ISNULL(@EOLNameTotal, 0) AS EOLNameTotal,
	ISNULL(@EOLPageCount, 0) AS EOLPageCount,
	ISNULL(@EOLPageTotal, 0) AS EOLPageTotal
END

GO
