CREATE PROCEDURE dbo.TitleAuthorUpdateAuto

@TitleAuthorID INT,
@TitleID INT,
@AuthorID INT,
@AuthorRoleID INT,
@Relationship NVARCHAR(150),
@TitleOfWork NVARCHAR(500),
@LastModifiedUserID INT,
@SequenceOrder SMALLINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleAuthor]
SET
	[TitleID] = @TitleID,
	[AuthorID] = @AuthorID,
	[AuthorRoleID] = @AuthorRoleID,
	[Relationship] = @Relationship,
	[TitleOfWork] = @TitleOfWork,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[SequenceOrder] = @SequenceOrder
WHERE
	[TitleAuthorID] = @TitleAuthorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleAuthorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
