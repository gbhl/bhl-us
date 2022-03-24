CREATE FUNCTION dbo.fnAddAuthorNameSpaces(@name nvarchar(max))
RETURNS nvarchar(max)
AS
BEGIN
	RETURN COALESCE(STUFF(@name, PATINDEX('%[,][^ ]%', @name), 1, ', '), @name)
END
GO
