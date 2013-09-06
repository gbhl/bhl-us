
-- AuthorNameInsertAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AuthorName

CREATE PROCEDURE AuthorNameInsertAuto

@AuthorNameID INT OUTPUT,
@AuthorID INT,
@FullName NVARCHAR(300),
@LastName NVARCHAR(150),
@FirstName NVARCHAR(150),
@FullerForm NVARCHAR(150),
@IsPreferredName SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[AuthorName]
(
	[AuthorID],
	[FullName],
	[LastName],
	[FirstName],
	[FullerForm],
	[IsPreferredName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@AuthorID,
	@FullName,
	@LastName,
	@FirstName,
	@FullerForm,
	@IsPreferredName,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @AuthorNameID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorNameInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorNameID],
		[AuthorID],
		[FullName],
		[LastName],
		[FirstName],
		[FullerForm],
		[IsPreferredName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[AuthorName]
	
	WHERE
		[AuthorNameID] = @AuthorNameID
	
	RETURN -- insert successful
END


