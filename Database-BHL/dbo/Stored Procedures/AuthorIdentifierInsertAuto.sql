
-- AuthorIdentifierInsertAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AuthorIdentifier

CREATE PROCEDURE AuthorIdentifierInsertAuto

@AuthorIdentifierID INT OUTPUT,
@AuthorID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[AuthorIdentifier]
(
	[AuthorID],
	[IdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@AuthorID,
	@IdentifierID,
	@IdentifierValue,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @AuthorIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorIdentifierID],
		[AuthorID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[AuthorIdentifier]
	
	WHERE
		[AuthorIdentifierID] = @AuthorIdentifierID
	
	RETURN -- insert successful
END


