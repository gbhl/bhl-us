
-- TitleAuthorUpdateAuto PROCEDURE
-- Generated 3/27/2014 11:56:11 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleAuthor

CREATE PROCEDURE TitleAuthorUpdateAuto

@TitleAuthorID INT,
@TitleID INT,
@AuthorID INT,
@AuthorRoleID INT,
@Relationship NVARCHAR(150),
@TitleOfWork NVARCHAR(500),
@LastModifiedUserID INT

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
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleAuthorID] = @TitleAuthorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAuthorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

