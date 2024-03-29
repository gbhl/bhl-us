SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiSegmentIdentifierSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.IdentifierName,
		ii.IdentifierValue
FROM	dbo.Segment s 
		INNER JOIN dbo.Item itm ON s.ItemID = itm.ItemID
		INNER JOIN dbo.ItemIdentifier ii ON itm.ItemID = ii.ItemID
		INNER JOIN dbo.Identifier i ON ii.IdentifierID = i.IdentifierID
WHERE	itm.ItemStatusID IN (30, 40)
AND		i.Display = 1
AND		s.SegmentID = @SegmentID
ORDER BY i.IdentifierName

END


GO
