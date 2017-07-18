CREATE FUNCTION [dbo].[fnGetIdentifierStringForTitle] 
(
	@TitleID int,
	@IdentifierName nvarchar(40)
)
RETURNS nvarchar(1024)
AS 

BEGIN
	DECLARE @IdentifierString nvarchar(125)

	SELECT	@IdentifierString = COALESCE(@IdentifierString, '') + IdentifierValue + '|'
	FROM	(
			SELECT DISTINCT ti.IdentifierValue
			FROM	dbo.Title_Identifier ti INNER JOIN dbo.Identifier i
						ON ti.IdentifierID = i.IdentifierID
						AND i.IdentifierName = @IdentifierName
			WHERE	ti.TitleID = @TitleID
			) X
	ORDER BY IdentifierValue ASC

	RETURN LTRIM(RTRIM(COALESCE(@IdentifierString, '')))
END
