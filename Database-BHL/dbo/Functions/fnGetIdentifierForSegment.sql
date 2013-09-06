CREATE FUNCTION [dbo].[fnGetIdentifierForSegment] 
(
	@SegmentID int,
	@IdentifierName nvarchar(40)
)
RETURNS nvarchar(125)
AS 

BEGIN
	DECLARE @IdentifierValue nvarchar(125)
	SET @IdentifierValue = NULL

	SELECT	@IdentifierValue = MIN(si.IdentifierValue)
	FROM	dbo.SegmentIdentifier si INNER JOIN dbo.Identifier i
				ON si.IdentifierID = i.IdentifierID
				AND i.IdentifierName = @IdentifierName
	WHERE	si.SegmentID = @SegmentID

	RETURN LTRIM(RTRIM(COALESCE(@IdentifierValue, '')))
END


