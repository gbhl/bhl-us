CREATE FUNCTION dbo.fnGetISSNValue 
(
	@input nvarchar(125)
)
RETURNS nvarchar(125)
AS

BEGIN
	RETURN 	RTRIM(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(
				REPLACE(@input,	'(Internet)', ''),
								'(online version)', ''),
								'(online)', ''),
								'(printed version)', ''),
								'(print)', '')
			)
END

GO
