CREATE FUNCTION import.fnContributorStringForImportRecord
(
	@ImportRecordID int,
	@Delimiter varchar(10)
)
RETURNS nvarchar(1024)
AS
BEGIN
	DECLARE @ContributorString nvarchar(1024)

	SELECT	@ContributorString = COALESCE(@ContributorString, '') + InstitutionCode + ' ' + @Delimiter + ' '
	FROM	import.ImportRecordContributor
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY ImportRecordContributorID ASC

	SET @ContributorString = CASE WHEN LEN(@ContributorString) >= LEN(@Delimiter) 
								THEN SUBSTRING(@ContributorString, 1, LEN(@ContributorString) - LEN(@Delimiter) - 1) 
								ELSE @ContributorString 
							END

	RETURN LTRIM(RTRIM(COALESCE(@ContributorString, '')))
END

GO
