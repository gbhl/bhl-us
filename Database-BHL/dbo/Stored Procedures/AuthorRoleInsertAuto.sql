
-- AuthorRoleInsertAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AuthorRole

CREATE PROCEDURE AuthorRoleInsertAuto

@AuthorRoleID INT,
@RoleDescription NVARCHAR(255),
@MARCDataFieldTag NVARCHAR(3),
@LastModifedDate DATETIME,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[AuthorRole]
(
	[AuthorRoleID],
	[RoleDescription],
	[MARCDataFieldTag],
	[CreationDate],
	[LastModifedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@AuthorRoleID,
	@RoleDescription,
	@MARCDataFieldTag,
	getdate(),
	@LastModifedDate,
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorRoleInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END


