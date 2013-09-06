CREATE FUNCTION [dbo].[fnKeywordStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(1024)
AS
BEGIN
	DECLARE @KeywordString nvarchar(1024)

	SELECT	@KeywordString = COALESCE(@KeywordString, '') + Keyword + '|'
	FROM	(
			-- Get all tags tied directly to this title
			SELECT DISTINCT 
					Keyword 
			FROM	TitleKeywordView 
			WHERE	TitleID = @TitleID
			) X
	ORDER BY 
			Keyword ASC

	RETURN LTRIM(RTRIM(COALESCE(@KeywordString, '')))
END




