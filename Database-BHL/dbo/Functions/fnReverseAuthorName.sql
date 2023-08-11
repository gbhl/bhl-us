CREATE FUNCTION dbo.fnReverseAuthorName (@FullName nvarchar(300))
RETURNS nvarchar(300)
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @FullNameReversed nvarchar(300)
	DECLARE @Comma int
	DECLARE @Length int

	-- Trim off one trailing comma from original string
	SET @Length = LEN(@FullName)
	SET @FullName = CASE WHEN RIGHT(@FullName, 1) = ',' THEN SUBSTRING(@FullName, 1, @Length - 1) ELSE @FullName END

	SET @Comma = CHARINDEX(',', @FullName)
	SET @Length = LEN(@FullName)

	IF @Comma = @Length
	BEGIN
		-- Comma at end of name, so just return
		SET @FullNameReversed = @FullName
	END
	ELSE
	BEGIN
		-- If comma within string (i.e. Last, First), then reverse the string at that point (i.e. First Last)
		SET @FullNameReversed = 
			CASE WHEN @Comma > 0 THEN
				-- Reversed string
				SUBSTRING(@FullName, @Comma + 1, @Length - @Comma + 1) + ' ' + SUBSTRING(@FullName, 1, @Comma - 1)
			ELSE
				-- Original string
				@FullName
			END
	END

	RETURN LTRIM(RTRIM(@FullNameReversed))
END
