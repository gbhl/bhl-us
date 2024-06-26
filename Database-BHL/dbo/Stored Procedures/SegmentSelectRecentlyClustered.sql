SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentSelectRecentlyClustered]

@NumClusters int = 150

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT DISTINCT
		scs.SegmentClusterID, ct.SegmentClusterTypeLabel, x.CreationDate, 
		scs.CreationUserID, s.SegmentID, s.BookID,
		g.GenreName, s.Title, s.ContainerTitle, s.Volume, s.Date, 
		cat.Authors, ii.IdentifierValue AS DOIName, s.StartPageNumber, s.EndPageNumber, s.PageRange,
		s.StartPageID
FROM	dbo.SegmentClusterSegment scs 
		INNER JOIN dbo.vwSegment s ON scs.SegmentID = s.SegmentID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		INNER JOIN dbo.SegmentCluster sc ON scs.SegmentClusterID = sc.SegmentClusterID
		INNER JOIN dbo.SegmentClusterType ct ON sc.SegmentClusterTypeID = ct.SegmentClusterTypeID
		LEFT JOIN dbo.SearchCatalogSegment cat ON s.SegmentID = cat.SegmentID
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @DOIIdentifierID
		INNER JOIN (
			-- Select the most recently modified clusters
			SELECT TOP (@NumClusters )
					SegmentClusterID, MIN(CreationDate) AS CreationDate
			FROM	dbo.SegmentClusterSegment
			GROUP BY 
					SegmentClusterID
			ORDER BY
					CreationDate DESC
			) x
			ON scs.SegmentClusterID = x.SegmentClusterID
ORDER BY
		x.CreationDate DESC,
		scs.SegmentClusterID,
		s.BookID,
		s.StartPageNumber,
		s.PageRange

END

GO

