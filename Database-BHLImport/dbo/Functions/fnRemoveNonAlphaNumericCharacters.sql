CREATE FUNCTION [dbo].[fnRemoveNonAlphaNumericCharacters]
(
	@strText VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
    WHILE PATINDEX('%[^0-9^a-z^a-Z]%', @strText) > 0
    BEGIN
        SET @strText = STUFF(@strText, PATINDEX('%[^0-9^a-z^a-Z]%', @strText), 1, '')
    END
    RETURN @strText
END
