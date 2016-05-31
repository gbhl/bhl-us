CREATE PROCEDURE [dbo].[InstitutionSelectBySegmentIDAndRole]

@SegmentID int,
@InstitutionRoleName nvarchar(100)

AS

BEGIN

SET NOCOUNT ON

SELECT	i.InstitutionCode,
		i.InstitutionName,
		i.Note,
		i.InstitutionUrl,
		i.BHLMemberLibrary,
		si.SegmentInstitutionID,
		r.InstitutionRoleName,
		r.InstitutionRoleLabel,
		i.CreationDate,
		i.LastModifiedDate,
		i.CreationUserID,
		i.LastModifiedUserID
FROM	dbo.SegmentInstitution si
		INNER JOIN Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	si.SegmentID = @SegmentID
AND		r.InstitutionRoleName = @InstitutionRoleName

END
