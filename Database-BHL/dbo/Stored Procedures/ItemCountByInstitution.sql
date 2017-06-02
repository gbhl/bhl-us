CREATE PROCEDURE [dbo].[ItemCountByInstitution]

@InstitutionCode nvarchar(10),
@RoleName nvarchar(100) = 'Holding Institution'

AS

BEGIN

SET NOCOUNT ON

SELECT	COUNT(*)
FROM	dbo.Item i
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r 
			ON ii.InstitutionRoleID = r.InstitutionRoleID
			AND	r.InstitutionRoleName = @RoleName
WHERE	i.ItemStatusID = 40
AND		ii.InstitutionCode = @InstitutionCode

END
