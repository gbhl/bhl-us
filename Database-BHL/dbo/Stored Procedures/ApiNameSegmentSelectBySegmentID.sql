
CREATE PROCEDURE [dbo].[ApiNameSegmentSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

DECLARE @NameBankID int
DECLARE @EOLID int
SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
SELECT @EOLID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

SELECT	ns.NameSegmentID,
		ns.NameID,
		ns.SegmentID,
		ns.NameSourceID,
		src.SourceName,
		n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue AS NameBankID,
		ISNULL(MIN(ni2.IdentifierValue), '') AS EOLID,
		n.IsActive,
		ns.IsFirstOccurrence,
		ns.CreationDate,
		ns.LastModifiedDate,
		ns.CreationUserID,
		ns.LastModifiedUserID
FROM	dbo.NameSegment ns
		INNER JOIN dbo.Name n ON ns.NameID = n.NameID
		INNER JOIN dbo.NameSource src ON ns.NameSourceID = src.NameSourceID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni1 ON nr.NameResolvedID = ni1.NameResolvedID AND ni1.IdentifierID = @NameBankID
		LEFT JOIN dbo.NameIdentifier ni2 ON nr.NameResolvedID = ni2.NameResolvedID AND ni2.IdentifierID = @EOLID
WHERE	ns.SegmentID = @SegmentID
GROUP BY
		ns.NameSegmentID,
		ns.NameID,
		ns.SegmentID,
		ns.NameSourceID,
		src.SourceName,
		n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue,
		n.IsActive,
		ns.IsFirstOccurrence,
		ns.CreationDate,
		ns.LastModifiedDate,
		ns.CreationUserID,
		ns.LastModifiedUserID
ORDER BY n.NameString



