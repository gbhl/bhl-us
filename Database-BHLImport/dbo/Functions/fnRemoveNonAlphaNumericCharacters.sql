CREATE FUNCTION [dbo].[fnRemoveNonAlphaNumericCharacters]
(
	@strText NVARCHAR(MAX)
)
RETURNS NVARCHAR(425)
WITH SCHEMABINDING
AS
BEGIN
    WHILE PATINDEX('%[^0-9^a-z^a-Z]%', @strText) > 0
    BEGIN
        SET @strText = STUFF(@strText, PATINDEX('%[^0-9^a-z^a-Z]%', @strText), 1, '')
    END
	-- The return value is limited to 440 unicode characters so that it can be used in a clustered index key (which is limited to 900 bytes)
    RETURN LEFT(@strText, 425)
END
