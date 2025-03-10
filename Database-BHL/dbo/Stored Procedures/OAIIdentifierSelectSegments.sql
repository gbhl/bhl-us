SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OAIIdentifierSelectSegments]

@MaxIdentifiers int = 100,
@StartID int = 1,
@FromDate DATETIME = null,
@UntilDate DATETIME = null,
@IncludeLocalContent smallint = 1,
@IncludeExternalContent smallint = 0

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT TOP(@MaxIdentifiers) 
		s.SegmentID AS ID, 
		'part' AS SetSpec, 
		CASE WHEN i.LastModifiedDate > s.LastModifiedDate THEN i.LastModifiedDate ELSE s.LastModifiedDate END AS LastModifiedDate
FROM	dbo.Segment s 
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
WHERE	(s.LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(s.LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
AND		s.SegmentID > @StartID
AND		i.ItemStatusID IN (30, 40)
AND		(s.ItemID IS NOT NULL OR ISNULL(s.Url, '') <> '')
AND		((c.HasLocalContent = 1 AND @IncludeLocalContent = 1)
OR		(c.HasExternalContent = 1 AND @IncludeExternalContent = 1))
ORDER BY s.SegmentID

END


GO
