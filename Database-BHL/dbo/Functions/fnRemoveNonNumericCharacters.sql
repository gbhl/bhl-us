CREATE FUNCTION dbo.fnRemoveNonNumericCharacters
(
	@strText VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
    WHILE PATINDEX('%[^0-9]%', @strText) > 0
    BEGIN
        SET @strText = STUFF(@strText, PATINDEX('%[^0-9]%', @strText), 1, '')
    END
    RETURN @strText
END
