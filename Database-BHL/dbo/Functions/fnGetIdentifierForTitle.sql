
CREATE FUNCTION [dbo].[fnGetIdentifierForTitle] 
(
	@TitleID int,
	@IdentifierName nvarchar(40)
)
RETURNS nvarchar(125)
AS 

BEGIN
	DECLARE @IdentifierValue nvarchar(125)
	SET @IdentifierValue = NULL

	SELECT	@IdentifierValue = MIN(ti.IdentifierValue)
	FROM	dbo.Title_Identifier ti INNER JOIN dbo.Identifier i
				ON ti.IdentifierID = i.IdentifierID
				AND i.IdentifierName = @IdentifierName
	WHERE	ti.TitleID = @TitleID

	RETURN LTRIM(RTRIM(COALESCE(@IdentifierValue, '')))
END

