CREATE PROCEDURE dbo.BSSegmentSelectByItem

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	SegmentID,
		ItemID,
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
		CreationDate,
		LastModifiedDate
FROM	dbo.BSSegment
WHERE	ItemID = @ItemID
ORDER BY
		SequenceOrder

END

