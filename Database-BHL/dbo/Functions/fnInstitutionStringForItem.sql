CREATE FUNCTION [dbo].[fnInstitutionStringForItem] 
(
	@ItemID int,
	@InstitutionRoleID smallint
)
RETURNS nvarchar(MAX)
AS
BEGIN
	DECLARE @InstitutionString nvarchar(MAX)

	SELECT	@InstitutionString = COALESCE(@InstitutionString, '') + i.InstitutionName + '|'
	FROM	dbo.ItemInstitution ii
			INNER JOIN Institution i ON ii.InstitutionCode = i.InstitutionCode
	WHERE	ii.ItemID = @ItemID
	AND		ii.InstitutionRoleID = @InstitutionRoleID
	ORDER BY
			i.InstitutionName ASC

	SET @InstitutionString = LTRIM(RTRIM(COALESCE(@InstitutionString, '')))
	SET @InstitutionString = CASE WHEN LEN(@InstitutionString) > 0 
								THEN LEFT(@InstitutionString, LEN(@InstitutionString) - 1) 
								ELSE @InstitutionString END

	RETURN @InstitutionString
END

GO
