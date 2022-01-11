CREATE FUNCTION dbo.fnGetISSNID 
(
	@input nvarchar(125)
)
RETURNS int
AS

BEGIN
	DECLARE @ID int

	IF (@input LIKE '%Internet%' OR @input LIKE '%online%') 
		SELECT @ID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'eISSN'
	ELSE 
		SELECT @ID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'

	RETURN @ID
END

GO
