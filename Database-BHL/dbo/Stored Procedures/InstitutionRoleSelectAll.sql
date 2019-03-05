CREATE PROCEDURE dbo.InstitutionRoleSelectAll

AS 

SET NOCOUNT ON

SELECT	InstitutionRoleID,
		InstitutionRoleName,
		InstitutionRoleLabel,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.InstitutionRole
ORDER BY
		InstitutionRoleName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionRoleSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
