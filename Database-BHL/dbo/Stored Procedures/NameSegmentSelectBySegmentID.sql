
CREATE PROCEDURE [dbo].[NameSegmentSelectBySegmentID]

@SegmentID INT

AS

BEGIN

SET NOCOUNT ON

SELECT	ns.NameSegmentID, 
		ns.NameID,
		ns.SegmentID,
		ns.IsFirstOccurrence,
		nr.NameResolvedID, 
		n.NameString, 
		ISNULL(nr.ResolvedNameString, '') AS ResolvedNameString, 
		ISNULL(nr.CanonicalNameString, '') AS CanonicalNameString,
		ns.CreationDate,
		ns.LastModifiedDate,
		ns.CreationUserID,
		ns.LastModifiedUserID
FROM	dbo.NameSegment ns 
		INNER JOIN dbo.Name n ON ns.NameID = n.NameID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID AND nr.IsPreferred = 1
WHERE	SegmentID = @SegmentID

END



