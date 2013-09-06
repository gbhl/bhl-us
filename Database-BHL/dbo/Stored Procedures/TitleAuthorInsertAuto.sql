
-- TitleAuthorInsertAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAuthor

CREATE PROCEDURE TitleAuthorInsertAuto

@TitleAuthorID INT OUTPUT,
@TitleID INT,
@AuthorID INT,
@AuthorRoleID INT = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAuthor]
(
	[TitleID],
	[AuthorID],
	[AuthorRoleID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleID,
	@AuthorID,
	@AuthorRoleID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleAuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleAuthorID],
		[TitleID],
		[AuthorID],
		[AuthorRoleID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[TitleAuthor]
	
	WHERE
		[TitleAuthorID] = @TitleAuthorID
	
	RETURN -- insert successful
END


