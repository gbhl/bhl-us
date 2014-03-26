
-- TitleAuthorInsertAuto PROCEDURE
-- Generated 3/27/2014 11:56:11 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAuthor

CREATE PROCEDURE TitleAuthorInsertAuto

@TitleAuthorID INT OUTPUT,
@TitleID INT,
@AuthorID INT,
@AuthorRoleID INT = null,
@Relationship NVARCHAR(150),
@TitleOfWork NVARCHAR(500),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAuthor]
(
	[TitleID],
	[AuthorID],
	[AuthorRoleID],
	[Relationship],
	[TitleOfWork],
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
	@Relationship,
	@TitleOfWork,
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
		[Relationship],
		[TitleOfWork],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[TitleAuthor]
	
	WHERE
		[TitleAuthorID] = @TitleAuthorID
	
	RETURN -- insert successful
END

