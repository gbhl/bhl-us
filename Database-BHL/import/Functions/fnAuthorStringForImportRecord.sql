CREATE FUNCTION [import].[fnAuthorStringForImportRecord] 
(
	@ImportRecordID int,
	@Delimiter varchar(10)
)
RETURNS nvarchar(1024)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(max)

	SELECT @AuthorString = COALESCE(@AuthorString, '') + 
				RTRIM(CASE WHEN FullName <> '' THEN FullName + ' ' 
					WHEN LastName <> '' AND FirstName <> '' THEN LastName + ', ' + FirstName + ' '
					WHEN LastName <> '' THEN LastName + ' '
					WHEN FirstName <> '' THEN FirstName + ' '
				ELSE '' END + 
				StartYear + 
				CASE WHEN StartYear <> '' THEN '-' ELSE '' END + EndYear) + ' ' + @Delimiter + ' '
	FROM	import.ImportRecordCreator
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY FullName, LastName, FirstName

	SET @AuthorString = CASE WHEN LEN(@AuthorString) >= LEN(@Delimiter) THEN SUBSTRING(@AuthorString, 1, LEN(@AuthorString) - LEN(@Delimiter) - 1) ELSE @AuthorString END

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END


