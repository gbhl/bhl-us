CREATE PROCEDURE [import].[SegmentResolve]

@DOIName nvarchar(50) = '',
@StartPageID int = 0

AS 

BEGIN

DECLARE @SegmentID int
SET @SegmentID = NULL

-- Get the IDs for DOI type and statuses
DECLARE @DOIEntityTypeSegment int
SELECT @DOIEntityTypeSegment = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'
DECLARE @DOIStatusApproved int
SELECT @DOIStatusApproved = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'DOI Approved'
DECLARE @DOIStatusExternal int
SELECT @DOIStatusExternal = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'External DOI'

-- Look for an existing valid segment DOI
SELECT	@SegmentID = EntityID
FROM	dbo.DOI
WHERE	DOIName = @DOIName
AND		DOIEntityTypeID = @DOIEntityTypeSegment
AND		DOIStatusID IN (@DOIStatusApproved, @DOIStatusExternal)
AND		IsValid = 1

IF (@SegmentID IS NULL)
BEGIN
	-- No DOIs found, look for a segment with a matching start page
	SELECT	@SegmentID = MIN(SegmentID)
	FROM	dbo.SegmentPage
	WHERE	SequenceOrder = 1
	AND		PageID = @StartPageID

	/*
	-- No DOIs found, look for a segment with a matching start and end page

	-- Get all pages in all segments to which the start page belongs
	SELECT	sp.SegmentID, sp2.PageID, sp2.SequenceOrder
	INTO	#Pages
	FROM	dbo.SegmentPage sp
			INNER JOIN dbo.SegmentPage sp2 ON sp.SegmentID = sp2.SegmentID
	WHERE	sp.PageID = @StartPageID
	ORDER BY sp2.SequenceOrder

	-- Get the start pages
	SELECT	SegmentID, PageID
	INTO	#StartPage
	FROM	#Pages
	WHERE	SequenceOrder = 1

	-- Get the end pages
	SELECT	SegmentID, MAX(SequenceOrder) AS SequenceOrder
	INTO	#EndSeq
	FROM	#Pages 
	GROUP BY SegmentID

	SELECT	p.SegmentID, s.PageID AS StartPageID, p.PageID AS EndPageID
	INTO	#Segments
	FROM	#Pages p 
			INNER JOIN #EndSeq e ON p.SegmentID = e.SegmentID AND p.SequenceOrder = e.SequenceOrder
			INNER JOIN #StartPage s ON p.SegmentID = s.SegmentID

	-- Compare start and end pages
	SELECT	@SegmentID = SegmentID
	FROM	#Segments
	WHERE	StartPageID = @StartPageID
	AND		EndPageID = @EndPageID
	*/
END

SELECT SegmentID FROM dbo.Segment WHERE SegmentID = @SegmentID

END
