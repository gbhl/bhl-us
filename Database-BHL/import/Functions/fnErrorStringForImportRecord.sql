﻿CREATE FUNCTION [import].[fnErrorStringForImportRecord] 
(
	@ImportRecordID int,
	@Delimiter varchar(10)
)
RETURNS nvarchar(max)
AS 

BEGIN
	DECLARE @ErrorString nvarchar(max)

	SELECT @ErrorString = COALESCE(@ErrorString, '') + ErrorMessage + ' ' + @Delimiter + ' '
	FROM	import.ImportRecordErrorLog
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY ImportRecordErrorLogID

	SET @ErrorString = CASE WHEN LEN(@ErrorString) >= LEN(@Delimiter) THEN SUBSTRING(@ErrorString, 1, LEN(@ErrorString) - LEN(@Delimiter) - 1) ELSE @ErrorString END

	RETURN LTRIM(RTRIM(COALESCE(@ErrorString, '')))
END
