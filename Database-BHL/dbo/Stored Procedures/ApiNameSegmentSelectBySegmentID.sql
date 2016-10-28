
CREATE PROCEDURE [dbo].[ApiNameSegmentSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

DECLARE @NameBankID int
DECLARE @EOLID int
SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
SELECT @EOLID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

-- Get names from NameSegment
SELECT	n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue AS NameBankID,
		ISNULL(MIN(ni2.IdentifierValue), '') AS EOLID
FROM	dbo.NameSegment ns
		INNER JOIN dbo.Name n ON ns.NameID = n.NameID
		INNER JOIN dbo.NameSource src ON ns.NameSourceID = src.NameSourceID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni1 ON nr.NameResolvedID = ni1.NameResolvedID AND ni1.IdentifierID = @NameBankID
		LEFT JOIN dbo.NameIdentifier ni2 ON nr.NameResolvedID = ni2.NameResolvedID AND ni2.IdentifierID = @EOLID
WHERE	ns.SegmentID = @SegmentID
GROUP BY
		n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue
UNION
-- Get names from NamePage
SELECT	ISNULL(n.NameString, '') AS NameString,
		ISNULL(nr.ResolvedNameString, '') AS ResolvedNameString,
		ISNULL(ni1.IdentifierValue, '') AS NameBankID,
		ISNULL(MIN(ni2.IdentifierValue), '') AS EOLID
FROM	dbo.Segment s 
		INNER JOIN SegmentPage sp ON s.SegmentID = sp.SegmentID
		INNER JOIN Page p ON sp.PageID = p.PageID
		INNER JOIN NamePage np ON p.PageID = np.PageID
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni1 ON nr.NameResolvedID = ni1.NameResolvedID AND ni1.IdentifierID = @NameBankID
		LEFT JOIN dbo.NameIdentifier ni2 ON nr.NameResolvedID = ni2.NameResolvedID AND ni2.IdentifierID = @EOLID
WHERE	s.SegmentID = @SegmentID
AND		s.SegmentStatusID IN (10,20)
AND		p.Active = 1
AND		n.IsActive = 1
GROUP BY
		n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue
ORDER BY n.NameString
