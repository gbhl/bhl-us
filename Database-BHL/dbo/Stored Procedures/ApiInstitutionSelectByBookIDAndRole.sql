CREATE PROCEDURE [dbo].[ApiInstitutionSelectByBookIDAndRole]

@BookID int,
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
		INNER JOIN dbo.Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Book b ON ii.ItemID = b.ItemID
WHERE	b.BookID = @BookID
AND		r.InstitutionRoleName = @InstitutionRoleName

END

GO
