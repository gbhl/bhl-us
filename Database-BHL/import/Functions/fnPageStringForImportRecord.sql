CREATE FUNCTION import.fnPageStringForImportRecord 
(
	@ImportRecordID int,
	@Delimiter varchar(10)
)
RETURNS nvarchar(max)
AS 

BEGIN
	DECLARE @PageString nvarchar(max)

	SELECT @PageString = COALESCE(@PageString, '') + 
				ISNULL(CONVERT(nvarchar(10), p.PageID), '') + ' ' + @Delimiter + ' '
	FROM	import.ImportRecordPage p
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY p.SequenceOrder

	SET @PageString = CASE WHEN LEN(@PageString) >= LEN(@Delimiter) THEN SUBSTRING(@PageString, 1, LEN(@PageString) - LEN(@Delimiter) - 1) ELSE @PageString END

	RETURN LTRIM(RTRIM(COALESCE(@PageString, '')))
END

GO
