CREATE PROCEDURE dbo.ItemInstitutionSelectByItemAndRole

@ItemID int,
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
AND		InstitutionRoleID = @InstitutionRoleID

END