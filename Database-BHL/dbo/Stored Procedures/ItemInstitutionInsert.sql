CREATE PROCEDURE [dbo].[ItemInstitutionInsert]

@ItemID int,
@InstitutionCode nvarchar(10),
@InstitutionRoleName nvarchar(100),
@UserID int = 1

AS

BEGIN

SET NOCOUNT ON

DECLARE @ItemInstitutionID int
DECLARE @InstitutionRoleID int

SELECT	@InstitutionRoleID = InstitutionRoleID 
FROM	dbo.InstitutionRole 
WHERE	InstitutionRoleName = @InstitutionRoleName

INSERT	dbo.ItemInstitution (ItemID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
VALUES	(@ItemID, @InstitutionCode, @InstitutionRoleID, @UserID, @UserID)

SET @ItemInstitutionID = SCOPE_IDENTITY()

-- Return the newly inserted record
SELECT	ItemInstitutionID,
		ItemID,
		InstitutionCode,
		InstitutionRoleID,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.ItemInstitution
WHERE	ItemInstitutionID = @ItemInstitutionID

END
