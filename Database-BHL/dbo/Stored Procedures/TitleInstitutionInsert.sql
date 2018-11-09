CREATE PROCEDURE [dbo].[TitleInstitutionInsert]

@TitleID int,
@InstitutionCode nvarchar(10),
@InstitutionRoleName nvarchar(100),
@Url nvarchar(500),
@UserID int = 1

AS

BEGIN

SET NOCOUNT ON

DECLARE @TitleInstitutionID int
DECLARE @InstitutionRoleID int

SELECT	@InstitutionRoleID = InstitutionRoleID 
FROM	dbo.InstitutionRole 
WHERE	InstitutionRoleName = @InstitutionRoleName

INSERT	dbo.TitleInstitution (TitleID, InstitutionCode, InstitutionRoleID, Url, CreationUserID, LastModifiedUserID)
VALUES	(@TitleID, @InstitutionCode, @InstitutionRoleID, @Url, @UserID, @UserID)

SET @TitleInstitutionID = SCOPE_IDENTITY()

-- Return the newly inserted record
SELECT	TitleInstitutionID,
		TitleID,
		InstitutionCode,
		InstitutionRoleID,
		Url,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.TitleInstitution
WHERE	TitleInstitutionID = @TitleInstitutionID

END
