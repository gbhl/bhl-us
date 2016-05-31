CREATE PROCEDURE [dbo].[SegmentInstitutionInsert]

@SegmentID int,
@InstitutionCode nvarchar(10),
@InstitutionRoleName nvarchar(100),
@UserID int = 1

AS

BEGIN

SET NOCOUNT ON

DECLARE @SegmentInstitutionID int
DECLARE @InstitutionRoleID int

SELECT	@InstitutionRoleID = InstitutionRoleID 
FROM	dbo.InstitutionRole 
WHERE	InstitutionRoleName = @InstitutionRoleName

INSERT	dbo.SegmentInstitution (SegmentID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
VALUES	(@SegmentID, @InstitutionCode, @InstitutionRoleID, @UserID, @UserID)

SET @SegmentInstitutionID = SCOPE_IDENTITY()

-- Return the newly inserted record
SELECT	SegmentInstitutionID,
		SegmentID,
		InstitutionCode,
		InstitutionRoleID,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.SegmentInstitution
WHERE	SegmentInstitutionID = @SegmentInstitutionID

END
