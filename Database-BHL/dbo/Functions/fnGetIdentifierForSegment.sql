SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnGetIdentifierForSegment] 
(
	@SegmentID int,
	@IdentifierName nvarchar(40),
	@IsContainerIdentifier smallint = 0
)
RETURNS nvarchar(125)
AS 

BEGIN
	DECLARE @IdentifierValue nvarchar(125)
	SET @IdentifierValue = NULL

	SELECT	@IdentifierValue = MIN(ii.IdentifierValue)
	FROM	dbo.Segment s
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID
			INNER JOIN dbo.Identifier i ON ii.IdentifierID = i.IdentifierID AND i.IdentifierName = @IdentifierName
	WHERE	s.SegmentID = @SegmentID

	RETURN LTRIM(RTRIM(COALESCE(@IdentifierValue, '')))
END


GO
