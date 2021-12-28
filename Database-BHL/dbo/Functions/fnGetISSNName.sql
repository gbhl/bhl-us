CREATE FUNCTION dbo.fnGetISSNName 
(
	@input nvarchar(125)
)
RETURNS nvarchar(40)
AS

BEGIN
	RETURN CASE WHEN @input LIKE '%Internet%' OR @input LIKE '%online%' THEN 'eISSN' ELSE 'ISSN' END
END

GO
