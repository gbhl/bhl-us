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
			SELECT DISTINCT k.Keyword 
			FROM	dbo.Title t
					INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
					INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
			WHERE	t.TitleID = @TitleID
			AND		LTRIM(RTRIM(k.Keyword)) <> ''
			) X
	ORDER BY 
			Keyword ASC

	RETURN LTRIM(RTRIM(COALESCE(@KeywordString, '')))
END

