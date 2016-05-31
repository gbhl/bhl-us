CREATE PROCEDURE [dbo].[InstitutionSelectByTitleIDAndRole]

@TitleID int,
@InstitutionRoleName nvarchar(100)

AS

BEGIN

SET NOCOUNT ON

SELECT	DISTINCT
		i.InstitutionCode,
		i.InstitutionName,
		i.Note,
		i.InstitutionUrl,
		i.BHLMemberLibrary,
		r.InstitutionRoleName,
		r.InstitutionRoleLabel,
		i.CreationDate,
		i.LastModifiedDate,
		i.CreationUserID,
		i.LastModifiedUserID
FROM	dbo.Title t
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item itm ON ti.ItemID = itm.ItemID
		INNER JOIN dbo.ItemInstitution ii ON itm.ItemID = ii.ItemID
		INNER JOIN dbo.Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	t.TitleID = @TitleID
AND		itm.ItemStatusID = 40
AND		r.InstitutionRoleName = @InstitutionRoleName

END
