SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [import].[SegmentResolve]

@DOIName nvarchar(50) = '',
@StartPageID int = 0

AS 

BEGIN

DECLARE @SegmentID int
SET @SegmentID = NULL

-- Get the IDs for DOI type and statuses
DECLARE @DOIEntityTypeSegment int, @DOIStatusApproved int, @DOIStatusExternal int
SELECT @DOIEntityTypeSegment = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'
SELECT @DOIStatusApproved = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'DOI Approved'
SELECT @DOIStatusExternal = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'External DOI'

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

-- Look for an existing valid segment DOI
SELECT	@SegmentID = s.SegmentID
FROM	dbo.ItemIdentifier ii
		INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID
WHERE	ii.IdentifierValue = @DOIName
AND		ii.IdentifierID = @DOIIdentifierID

IF (@SegmentID IS NULL)
BEGIN
	-- No DOIs found, look for a segment with a matching start page
	SELECT	@SegmentID = MIN(SegmentID)
	FROM	dbo.Segment s
			INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
	WHERE	ip.SequenceOrder = 1
	AND		ip.PageID = @StartPageID
END

SELECT SegmentID FROM dbo.Segment WHERE SegmentID = @SegmentID

END

GO
