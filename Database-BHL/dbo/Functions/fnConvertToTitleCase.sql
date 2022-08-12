CREATE FUNCTION dbo.fnConvertToTitleCase ( @value nvarchar(max) )
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @pos int = 1, @pos2 int

	IF (@value <> '')
	BEGIN
		SET @value = LOWER(RTRIM(@value))
		WHILE (1 = 1)
		BEGIN
			SET @value = STUFF(@value, @pos, 1, UPPER(SUBSTRING(@value, @pos, 1)))
			SET @pos2 = PATINDEX('%[- ''.,)(]%', SUBSTRING(@value, @pos, LEN(@value)))
			SET @pos += @pos2
			IF (ISNULL(@pos2, 0) = 0 or @pos > LEN(@value))
				BREAK
		END
	END

	RETURN @value
END
GO
