CREATE FUNCTION dbo.fnGetLCCNValue
(
	@input nvarchar(125)
)
RETURNS nvarchar(125)
AS

BEGIN
	DECLARE @inputClean nvarchar(125)
	SET @inputClean = @input

	-- Removing suffixes (like '//r45')
	SET	@inputClean = SUBSTRING(@inputClean, 1, CASE 
													WHEN CHARINDEX('/', @inputClean) > 0 
													THEN CHARINDEX('/', @inputClean) - 1 
													ELSE LEN(@inputClean) 
													END)

	-- Remove other invalid characters
	SET	@inputClean = 
			REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
				@inputClean
			, ' ', ''), '*', ''), '^', ''), '?', ''), '.', ''), 'revised2', ''), 'revised', ''), 'rev2', ''), 'rev', '')

	-- Reformat values that look like 12-12345 to instead look like 12012345
	IF (CHARINDEX('-', @inputClean) > 0
		AND	ISNUMERIC(SUBSTRING(@inputClean, 1, CHARINDEX('-', @inputClean) - 1)) = 1
		AND	ISNUMERIC(SUBSTRING(@inputClean, CHARINDEX('-', @inputClean) + 1, LEN(@inputClean) - CHARINDEX('-', @inputClean))) = 1)
	BEGIN
		SET	@inputClean =	RIGHT('00' + SUBSTRING(@inputClean, 1, CHARINDEX('-', @inputClean) - 1), 2) +
							RIGHT('000000' + SUBSTRING(@inputClean, CHARINDEX('-', @inputClean) + 1, LEN(@inputClean) - CHARINDEX('-', @inputClean)), 6)
	END

	-- Reformat values that look like abc12-12345 to instead look like abc12012345
	IF (CHARINDEX('-', @inputClean) BETWEEN 1 AND 6
		AND	ISNUMERIC(SUBSTRING(@inputClean, CHARINDEX('-', @inputClean) + 1, LEN(@inputClean) - CHARINDEX('-', @inputClean))) = 1)
	BEGIN
		SET	@inputClean =	SUBSTRING(@inputClean, 1, CHARINDEX('-', @inputClean) - 1) +
							RIGHT('000000' + SUBSTRING(@inputClean, CHARINDEX('-', @inputClean) + 1, LEN(@inputClean) - CHARINDEX('-', @inputClean)), 6)
	END

	-- Flag the valid and invalid values
	DECLARE @isValid int
	SET		@isValid = 
			CASE 
				--     00000000
				WHEN @inputClean NOT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
				--    a00000000
				AND @inputClean NOT LIKE '[a-z][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
				--   aa00000000
				AND @inputClean NOT LIKE '[a-z][a-z][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
				--  aaa00000000
				AND @inputClean NOT LIKE '[a-z][a-z][a-z][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
				--   0000000000
				AND @inputClean NOT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
				--  a0000000000
				AND @inputClean NOT LIKE '[a-z][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'	
				-- aa0000000000
				AND @inputClean NOT LIKE '[a-z][a-z][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
				THEN 0 
				ELSE 1 
			END

	-- If the cleaned value is a valid LCCN identifier format, return it.  Otherwise, return the unmodified value.
	RETURN CASE WHEN @isValid = 1 THEN @inputClean ELSE @input END
END

GO
