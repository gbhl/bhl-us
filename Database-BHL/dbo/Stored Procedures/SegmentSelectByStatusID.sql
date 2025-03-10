SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentSelectByStatusID]

@SegmentStatusID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	BookID int NULL,
	ItemID int NULL,
	Title nvarchar(2000) NOT NULL,
	Date nvarchar(20) NOT NULL,
	Authors nvarchar(max) NULL,
	SegmentClusterID int NULL,
	DOIName nvarchar(50) NULL
	)

INSERT	#tmpSegment
SELECT DISTINCT
		s.SegmentID,
		s.BookID,
		s.ItemID,
		s.Title,
		s.Date,
		REPLACE(cat.Authors, '|', ' '),
		NULL AS SegmentClusterID,
		ii.IdentifierValue AS DOIName
FROM	dbo.vwSegment s
		LEFT JOIN dbo.SearchCatalogSegment cat ON s.SegmentID = cat.SegmentID
		LEFT JOIN dbo.ItemIdentifier ii WITH (NOLOCK) ON s.ItemID = ii.ItemID AND ii.IdentifierID = @DOIIdentifierID
WHERE	SegmentStatusID = @SegmentStatusID

-- If we didn't get author information from the search catalog, attempt to compile it on-the-fly.
-- This allows us to pick up author names for segments not yet indexed for searching.
UPDATE	#tmpSegment
SET		Authors = ISNULL(dbo.fnAuthorStringForSegment(SegmentID, ' ') + ' ', '')
WHERE	Authors IS NULL

UPDATE	#tmpSegment
SET		SegmentClusterID = scs.SegmentClusterID
FROM	#tmpSegment t INNER JOIN SegmentClusterSegment scs ON t.SegmentID = scs.SegmentID

SELECT	*
FROM	#tmpSegment

END

GO
