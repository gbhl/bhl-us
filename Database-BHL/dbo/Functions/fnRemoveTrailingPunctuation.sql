CREATE FUNCTION dbo.fnRemoveTrailingPunctuation
(
	@input nvarchar(max), @validPattern nvarchar(200) = '[a-zA-Z0-9)\]?!>*%"'']%'
)
RETURNS nvarchar(max)
AS
BEGIN
	WHILE (	-- Remove invalid characters from the end of the input
			REVERSE(RTRIM(@input)) NOT LIKE @validPattern ESCAPE '\'
			-- Don't remove characters outside of the basic ascii table, so
			-- that valid non-english letters are not removed
			AND UNICODE(LEFT(REVERSE(@input), 1)) <= 127)
	BEGIN
		SET @input = RTRIM(LEFT(@input, LEN(@input) - 1))
	END

	RETURN @input
END
GO

