CREATE FUNCTION [dbo].[fnInstitutionCodesForItem]
(
	@ItemID int,
	@InstitutionRoleID smallint
)
RETURNS nvarchar(MAX)
AS
BEGIN
	DECLARE @InstitutionCodes nvarchar(MAX)

	SELECT	@InstitutionCodes = COALESCE(@InstitutionCodes, '') + i.InstitutionCode + '|'
	FROM	dbo.ItemInstitution ii
			INNER JOIN Institution i ON ii.InstitutionCode = i.InstitutionCode
	WHERE	ii.ItemID = @ItemID
	AND		ii.InstitutionRoleID = @InstitutionRoleID
	ORDER BY
			i.InstitutionName ASC

	SET @InstitutionCodes = LTRIM(RTRIM(COALESCE(@InstitutionCodes, '')))
	SET @InstitutionCodes = CASE WHEN LEN(@InstitutionCodes) > 0 
								THEN LEFT(@InstitutionCodes, LEN(@InstitutionCodes) - 1) 
								ELSE @InstitutionCodes END

	RETURN @InstitutionCodes
END

GO
