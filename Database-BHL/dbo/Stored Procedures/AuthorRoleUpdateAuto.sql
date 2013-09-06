
-- AuthorRoleUpdateAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for AuthorRole

CREATE PROCEDURE AuthorRoleUpdateAuto

@AuthorRoleID INT,
@RoleDescription NVARCHAR(255),
@MARCDataFieldTag NVARCHAR(3),
@LastModifedDate DATETIME,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[AuthorRole]

SET

	[AuthorRoleID] = @AuthorRoleID,
	[RoleDescription] = @RoleDescription,
	[MARCDataFieldTag] = @MARCDataFieldTag,
	[LastModifedDate] = @LastModifedDate,
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[AuthorRoleID] = @AuthorRoleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorRoleUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END


