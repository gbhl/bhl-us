SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentIdentifierSelectBySegmentID]

@SegmentID int,
@Display SMALLINT = NULL

AS

BEGIN

SELECT	ii.ItemIdentifierID,
		s.SegmentID,
		ii.ItemID,
		ii.IdentifierID,
		i.IdentifierName,
		i.IdentifierLabel,
		i.Prefix,
		ii.IdentifierValue,
		0 AS IsContainerIdentifier,
		ii.CreationDate,
		ii.LastModifiedDate,
		ii.CreationUserID,
		ii.LastModifiedUserID
FROM	dbo.Segment s 
		INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID
		INNER JOIN dbo.Identifier i ON ii.IdentifierID = i.IdentifierID
WHERE	s.SegmentID = @SegmentID
AND		i.Display = ISNULL(@Display, i.Display)
ORDER BY 
		i.IdentifierLabel, ii.IdentifierValue

END

GO
