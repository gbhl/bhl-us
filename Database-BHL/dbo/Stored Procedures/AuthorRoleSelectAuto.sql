
-- AuthorRoleSelectAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for AuthorRole

CREATE PROCEDURE AuthorRoleSelectAuto

@AuthorRoleID INT

AS 

SET NOCOUNT ON

SELECT 

	[AuthorRoleID],
	[RoleDescription],
	[MARCDataFieldTag],
	[CreationDate],
	[LastModifedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[AuthorRole]

WHERE
	[AuthorRoleID] = @AuthorRoleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorRoleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


