CREATE PROCEDURE [dbo].[InstitutionSelectByItemIDAndRole]

@ItemID int,
@InstitutionRoleName nvarchar(100)

AS

BEGIN

SET NOCOUNT ON

SELECT	i.InstitutionCode,
		i.InstitutionName,
		i.Note,
		i.InstitutionUrl,
		i.BHLMemberLibrary,
		ii.ItemInstitutionID,
		r.InstitutionRoleName,
		r.InstitutionRoleLabel,
		i.CreationDate,
		i.LastModifiedDate,
		i.CreationUserID,
		i.LastModifiedUserID
FROM	dbo.ItemInstitution ii
		INNER JOIN Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	ii.ItemID = @ItemID
AND		r.InstitutionRoleName = @InstitutionRoleName

END
