CREATE PROCEDURE [dbo].[DOISelectValidForSegment]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	ii.ItemIdentifierID,
		ii.IdentifierValue,
		ii.CreationDate,
		ii.LastModifiedDate,
		ii.CreationUserID,
		ii.LastModifiedUserID
FROM	dbo.ItemIdentifier ii
		INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID AND ii.IdentifierID = @DOIIdentifierID
WHERE	s.SegmentID = @SegmentID

END

GO
