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

	SELECT	@IdentifierValue = MIN(si.IdentifierValue)
	FROM	dbo.SegmentIdentifier si INNER JOIN dbo.Identifier i
				ON si.IdentifierID = i.IdentifierID
				AND i.IdentifierName = @IdentifierName
	WHERE	si.SegmentID = @SegmentID
	AND		(si.IsContainerIdentifier = @IsContainerIdentifier OR @IsContainerIdentifier IS NULL)

	RETURN LTRIM(RTRIM(COALESCE(@IdentifierValue, '')))
END


