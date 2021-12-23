CREATE PROCEDURE [dbo].[ItemIdentifierSelectByNameAndID]

@IdentifierName nvarchar(40),
@SegmentID int

AS 

SET NOCOUNT ON

SELECT	ii.ItemIdentifierID,
		ii.ItemID,
		ii.IdentifierID,
		i.[IdentifierName],
		i.[IdentifierLabel],
		i.[Prefix],
		ii.[IdentifierValue]
FROM	[dbo].[ItemIdentifier] ii 
		INNER JOIN [dbo].[Identifier] i ON ii.IdentifierID = i.IdentifierID
		INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID
WHERE	s.SegmentID = @SegmentID
AND		i.IdentifierName = @IdentifierName

GO
