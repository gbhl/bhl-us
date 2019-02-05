CREATE PROCEDURE dbo.ItemInstitutionSelectByItemInstitutionAndRole

@ItemID int,
@InstitutionCode nvarchar(10),
@InstitutionRoleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	ItemInstitutionID,
		ItemID,
		InstitutionCode,
		InstitutionRoleID,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.ItemInstitution
WHERE	ItemID = @ItemID
AND		InstitutionCode = @InstitutionCode
AND		InstitutionRoleID = @InstitutionRoleID

END
