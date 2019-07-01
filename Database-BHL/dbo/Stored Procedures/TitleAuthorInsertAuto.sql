CREATE PROCEDURE dbo.TitleAuthorInsertAuto

@TitleAuthorID INT OUTPUT,
@TitleID INT,
@AuthorID INT,
@AuthorRoleID INT = null,
@Relationship NVARCHAR(150),
@TitleOfWork NVARCHAR(500),
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@SequenceOrder SMALLINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAuthor]
( 	[TitleID],
	[AuthorID],
	[AuthorRoleID],
	[Relationship],
	[TitleOfWork],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[SequenceOrder] )
VALUES
( 	@TitleID,
	@AuthorID,
	@AuthorRoleID,
	@Relationship,
	@TitleOfWork,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@SequenceOrder )

SET @TitleAuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleAuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
		[LastModifiedUserID],
		[SequenceOrder]	
	FROM [dbo].[TitleAuthor]
	WHERE
		[TitleAuthorID] = @TitleAuthorID
	
	RETURN -- insert successful
END
