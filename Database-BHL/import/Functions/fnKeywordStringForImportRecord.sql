CREATE FUNCTION [import].[fnKeywordStringForImportRecord] 
(
	@ImportRecordID int,
	@Delimiter varchar(10)
)
RETURNS nvarchar(1024)
AS
BEGIN
	DECLARE @KeywordString nvarchar(1024)

	SELECT	@KeywordString = COALESCE(@KeywordString, '') + Keyword + ' ' + @Delimiter + ' '
	FROM	import.ImportRecordKeyword
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY Keyword ASC

	SET @KeywordString = CASE WHEN LEN(@KeywordString) >= LEN(@Delimiter) THEN SUBSTRING(@KeywordString, 1, LEN(@KeywordString) - LEN(@Delimiter) - 1) ELSE @KeywordString END

	RETURN LTRIM(RTRIM(COALESCE(@KeywordString, '')))
END

