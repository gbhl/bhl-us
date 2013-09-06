
-- TitleAuthorUpdateAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleAuthor

CREATE PROCEDURE TitleAuthorUpdateAuto

@TitleAuthorID INT,
@TitleID INT,
@AuthorID INT,
@AuthorRoleID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleAuthor]

SET

	[TitleID] = @TitleID,
	[AuthorID] = @AuthorID,
	[AuthorRoleID] = @AuthorRoleID,
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
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[TitleAuthor]
	
	WHERE
		[TitleAuthorID] = @TitleAuthorID
	
	RETURN -- update successful
END


