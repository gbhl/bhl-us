SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiNameSegmentSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

DECLARE @NameBankID int
DECLARE @EOLID int
SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
SELECT @EOLID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

-- Get names from NamePage
SELECT	ISNULL(n.NameString, '') AS NameString,
		ISNULL(nr.ResolvedNameString, '') AS ResolvedNameString,
		ISNULL(nr.CanonicalNameString, '') AS CanonicalNameString,
		ISNULL(ni1.IdentifierValue, '') AS NameBankID,
		ISNULL(MIN(ni2.IdentifierValue), '') AS EOLID
FROM	dbo.Segment s 
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemPage ip ON i.itemID = ip.ItemID
		INNER JOIN Page p ON ip.PageID = p.PageID
		INNER JOIN NamePage np ON p.PageID = np.PageID
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni1 ON nr.NameResolvedID = ni1.NameResolvedID AND ni1.IdentifierID = @NameBankID
		LEFT JOIN dbo.NameIdentifier ni2 ON nr.NameResolvedID = ni2.NameResolvedID AND ni2.IdentifierID = @EOLID
WHERE	s.SegmentID = @SegmentID
AND		i.ItemStatusID IN (30,40)
AND		p.Active = 1
AND		n.IsActive = 1
GROUP BY
		n.NameString,
		nr.ResolvedNameString,
		nr.CanonicalNameString,
		ni1.IdentifierValue
ORDER BY n.NameString


GO
