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
				CONVERT(nvarchar(10), c.ImportRecordCreatorID) + 
				'|' + RTRIM(CASE WHEN c.FullName <> '' THEN c.FullName + ' ' 
					WHEN c.LastName <> '' AND c.FirstName <> '' THEN c.LastName + ', ' + c.FirstName + ' '
					WHEN c.LastName <> '' THEN c.LastName + ' '
					WHEN c.FirstName <> '' THEN c.FirstName + ' '
					ELSE '' END + 
					c.StartYear + 
					CASE WHEN c.StartYear <> '' THEN '-' ELSE '' END + c.EndYear
					) + 
				'|' + ISNULL(CONVERT(nvarchar(10), c.AuthorID), '') +
				'|' + RTRIM(
					ISNULL(n.FullName, '') + ' ' + 
					ISNULL(a.Numeration, '') + ' ' + 
					ISNULL(a.Unit, '') + ' ' + 
					ISNULL(a.Title, '') + ' ' + 
					ISNULL(a.Location, '') + ' ' + 
					ISNULL(n.FullerForm, '') + ' ' + 
					CASE WHEN ISNULL(a.StartDate, '') <> '' THEN a.StartDate + '-' ELSE '' END + ISNULL(a.EndDate, '')
					)
				+ ' ' + @Delimiter + ' '
	FROM	import.ImportRecordCreator c
			LEFT JOIN dbo.Author a ON c.AuthorID = a.AuthorID AND a.IsActive = 1
			LEFT JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY c.ImportRecordCreatorID

	SET @AuthorString = CASE WHEN LEN(@AuthorString) >= LEN(@Delimiter) THEN SUBSTRING(@AuthorString, 1, LEN(@AuthorString) - LEN(@Delimiter) - 1) ELSE @AuthorString END

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
