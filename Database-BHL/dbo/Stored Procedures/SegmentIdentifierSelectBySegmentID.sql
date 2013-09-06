
CREATE PROCEDURE [dbo].[SegmentIdentifierSelectBySegmentID]

@SegmentID int

AS

BEGIN

SELECT	si.SegmentIdentifierID,
		si.SegmentID,
		si.IdentifierID,
		i.IdentifierName,
		i.IdentifierLabel,
		si.IdentifierValue,
		si.IsContainerIdentifier,
		si.CreationDate,
		si.LastModifiedDate,
		si.CreationUserID,
		si.LastModifiedUserID
FROM	dbo.SegmentIdentifier si INNER JOIN dbo.Identifier i
			ON si.IdentifierID = i.IdentifierID
WHERE	SegmentID = @SegmentID
ORDER BY 
		i.IdentifierLabel, si.IdentifierValue

END


