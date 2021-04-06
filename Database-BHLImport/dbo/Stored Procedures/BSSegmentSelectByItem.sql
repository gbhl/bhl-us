CREATE PROCEDURE [dbo].[BSSegmentSelectByItem]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	SegmentID,
		ItemID,
		ss.StatusLabel,
		BioStorReferenceID,
		SequenceOrder,
		Genre,
		Title,
		ContainerTitle,
		Volume,
		Series,
		Issue,
		Date,
		ISSN,
		DOI,
		StartPageNumber,
		EndPageNumber,
		StartPageID,
		ContributorCreationDate,
		ContributorLastModifiedDate,
		BHLSegmentID,
		s.CreationDate,
		s.LastModifiedDate
FROM	dbo.BSSegment s
		INNER JOIN dbo.BSSegmentStatus ss ON s.SegmentStatusID = ss.SegmentStatusID
WHERE	ItemID = @ItemID
ORDER BY
		SequenceOrder

END
